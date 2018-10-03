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
	/// Provide button functionality for the group dialog launcher.
	/// </summary>
    internal class DialogLauncherButtonController : LeftUpButtonController,
                                                    ISourceController,
                                                    IKeyController,
                                                    IRibbonKeyTipTarget
	{
        #region Instance Fields
        private bool _hasFocus;
        #endregion
        
        #region Identity
		/// <summary>
        /// Initialize a new instance of the DialogLauncherButtonController class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        /// <param name="target">Target for state changes.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public DialogLauncherButtonController(KryptonRibbon ribbon,
                                              ViewBase target,
                                              NeedPaintHandler needPaint)
            : base(ribbon, target, needPaint)
		{
        }
		#endregion

        #region Focus Notifications
        /// <summary>
        /// Source control has got the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void GotFocus(Control c)
        {
            _hasFocus = true;

            // Redraw to show the change in visual state
            UpdateTargetState(Point.Empty);
            OnNeedPaint(false);
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void LostFocus(Control c)
        {
            _hasFocus = false;

            // Redraw to show the change in visual state
            UpdateTargetState(Point.Empty);
            OnNeedPaint(false);
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
            c = Ribbon.GetControllerControl(c);

            if (c is KryptonRibbon)
                KeyDownRibbon(e);
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
            // Exit keyboard mode when you click the button spec
            Ribbon.KillKeyboardMode();

            // Generate a click event
            OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }
        #endregion

        #region Protected
        /// <summary>
        /// Set the correct visual state of the target.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        protected override void UpdateTargetState(Point pt)
        {
            if (_hasFocus)
            {
                if (Target.ElementState != PaletteState.Tracking)
                {
                    // Update target to reflect new state
                    Target.ElementState = PaletteState.Tracking;

                    // Redraw to show the change in visual state
                    OnNeedPaint(false);
                }
            }
            else
                base.UpdateTargetState(pt);
        }
        #endregion

        #region Implementation
        private void KeyDownRibbon(KeyEventArgs e)
        {
            ViewBase newView = null;

            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    // Get the previous focus item for the currently selected page
                    newView = Ribbon.GroupsArea.ViewGroups.GetPreviousFocusItem(Target);

                    // Got to the actual tab header
                    if (newView == null)
                        newView = Ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(Ribbon.SelectedTab);
                    break;
                case Keys.Tab:
                case Keys.Right:
                    // Get the next focus item for the currently selected page
                    newView = Ribbon.GroupsArea.ViewGroups.GetNextFocusItem(Target);

                    // Move across to any far defined buttons
                    if (newView == null)
                        newView = Ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                    // Move across to any inherit defined buttons
                    if (newView == null)
                        newView = Ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

                    // Rotate around to application button
                    if (newView == null)
                    {
                        if (Ribbon.TabsArea.LayoutAppButton.Visible)
                            newView = Ribbon.TabsArea.LayoutAppButton.AppButton;
                        else if (Ribbon.TabsArea.LayoutAppTab.Visible)
                            newView = Ribbon.TabsArea.LayoutAppTab.AppTab;
                    }                        
                    break;
                case Keys.Space:
                case Keys.Enter:
                    // Exit keyboard mode when you click the button spec
                    Ribbon.KillKeyboardMode();

                    // Generate a click event
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
            }

            // If we have a new view to focus and it is not ourself...
            if ((newView != null) && (newView != Target))
            {
                // If the new view is a tab then select that tab unless in minimized mode
                if ((newView is ViewDrawRibbonTab) && !Ribbon.RealMinimizedMode)
                    Ribbon.SelectedTab = ((ViewDrawRibbonTab)newView).RibbonTab;

                // Finally we switch focus to new view
                Ribbon.FocusView = newView;
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
                case Keys.Space:
                case Keys.Enter:
                    // Exit keyboard mode when you click the button spec
                    Ribbon.KillKeyboardMode();

                    // Generate a click event
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
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
                case Keys.Space:
                case Keys.Enter:
                    // Exit keyboard mode when you click the button spec
                    Ribbon.KillKeyboardMode();

                    // Generate a click event
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
            }
        }
        #endregion
    }
}
