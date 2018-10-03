// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2017. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Process mouse events for a ribbon group textbox.
	/// </summary>
    internal class TextBoxController : GlobalId,
                                       ISourceController,
                                       IKeyController,
                                       IRibbonKeyTipTarget
	{
		#region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroupTextBox _textBox;
        private ViewDrawRibbonGroupTextBox _target;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the TextBoxController class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        /// <param name="textBox">Source definition.</param>
        /// <param name="target">Target view element.</param>
        public TextBoxController(KryptonRibbon ribbon,
                                 KryptonRibbonGroupTextBox textBox,
                                 ViewDrawRibbonGroupTextBox target)
		{
            Debug.Assert(ribbon != null);
            Debug.Assert(textBox != null);
            Debug.Assert(target != null);

            _ribbon = ribbon;
            _textBox = textBox;
            _target = target;
        }
		#endregion

        #region Focus Notifications
        /// <summary>
        /// Source control has got the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public void GotFocus(Control c)
        {
            if ((_textBox.LastTextBox != null) &&
                (_textBox.LastTextBox.TextBox != null) &&
                (_textBox.LastTextBox.TextBox.CanFocus))
            {
                _ribbon.LostFocusLosesKeyboard = false;
                _textBox.LastTextBox.TextBox.Focus();
            }
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public void LostFocus(Control c)
        {
        }
        #endregion

        #region Key Notifications
        /// <summary>
        /// Key has been pressed down.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public void KeyDown(Control c, KeyEventArgs e)
        {
            // Get the root control that owns the provided control
            c = _ribbon.GetControllerControl(c);

            if (c is KryptonRibbon)
                KeyDownRibbon(c as KryptonRibbon, e);
            else if (c is VisualPopupGroup)
                KeyDownPopupGroup(c as VisualPopupGroup, e);
            else if (c is VisualPopupMinimized)
                KeyDownPopupMinimized(c as VisualPopupMinimized, e);
        }

        /// <summary>
        /// Key has been pressed.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        public void KeyPress(Control c, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// Key has been released.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public bool KeyUp(Control c, KeyEventArgs e)
        {
            return false;
        }
        #endregion

        #region KeyTipSelect
        /// <summary>
        /// Perform actual selection of the item.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        public void KeyTipSelect(KryptonRibbon ribbon)
        {
            // Can the textbox take the focus
            if (_textBox.LastTextBox.CanFocus)
            {
                // Prevent the ribbon from killing keyboard mode when it loses the focus,
                // as this causes the tracking windows to be killed and we want them kept
                ribbon.LostFocusLosesKeyboard = false;

                // Prevent the restore of focus when we fill the keyboard mode, as the focus
                // has been placed on the textbox and so focus is allowed to change
                ribbon.IgnoreRestoreFocus = true;

                // Exit the use of keyboard mode
                ribbon.KillKeyboardMode();

                // Push focus to the specified target control
                _textBox.LastTextBox.TextBox.Focus();

                // If the textbox is inside a popup window
                if (_textBox.LastParentControl is VisualPopupGroup)
                {
                    // Ensure that the previous ribbon focus is restored when the popup window is dismissed
                    VisualPopupGroup popupGroup = (VisualPopupGroup)_textBox.LastParentControl;
                    popupGroup.RestorePreviousFocus = true;
                }
            }
        }
        #endregion

        #region Implementation
        private void KeyDownRibbon(KryptonRibbon ribbon, KeyEventArgs e)
        {
            ViewBase newView = null;

            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    // Get the previous focus item for the currently selected page
                    newView = ribbon.GroupsArea.ViewGroups.GetPreviousFocusItem(_target);

                    // Got to the actual tab header
                    if (newView == null)
                        newView = ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab);
                    break;
                case Keys.Tab:
                case Keys.Right:
                    // Get the next focus item for the currently selected page
                    newView = ribbon.GroupsArea.ViewGroups.GetNextFocusItem(_target);

                    // Move across to any far defined buttons
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                    // Move across to any inherit defined buttons
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

                    // Rotate around to application button
                    if (newView == null)
                    {
                        if (ribbon.TabsArea.LayoutAppButton.Visible)
                            newView = ribbon.TabsArea.LayoutAppButton.AppButton;
                        else if (ribbon.TabsArea.LayoutAppTab.Visible)
                            newView = ribbon.TabsArea.LayoutAppTab.AppTab;
                    }                        
                    break;
            }

            // If we have a new view to focus and it is not ourself...
            if ((newView != null) && (newView != _target))
            {
                // If the new view is a tab then select that tab unless in minimized mode
                if ((newView is ViewDrawRibbonTab) && !ribbon.RealMinimizedMode)
                    ribbon.SelectedTab = ((ViewDrawRibbonTab)newView).RibbonTab;

                // Finally we switch focus to new view
                ribbon.FocusView = newView;
            }
        }

        private void KeyDownPopupGroup(VisualPopupGroup popupGroup, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    popupGroup.SetPreviousFocusItem();
                    break;
                case Keys.Tab:
                case Keys.Right:
                    popupGroup.SetNextFocusItem();
                    break;
            }
        }

        private void KeyDownPopupMinimized(VisualPopupMinimized popupMinimized, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    popupMinimized.SetPreviousFocusItem();
                    break;
                case Keys.Tab:
                case Keys.Right:
                    popupMinimized.SetNextFocusItem();
                    break;
            }
        }
        #endregion
    }
}
