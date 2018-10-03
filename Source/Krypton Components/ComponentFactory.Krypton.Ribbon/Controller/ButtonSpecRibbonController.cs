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
	/// Process mouse events for a ribbon based button spec button.
	/// </summary>
    internal class ButtonSpecRibbonController : ButtonController
    {
        #region Instance Fields
        private bool _hasFocus;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecRibbonController class.
		/// </summary>
		/// <param name="target">Target for state changes.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ButtonSpecRibbonController(ViewBase target,
                                          NeedPaintHandler needPaint)
            : base(target, needPaint)
		{
        }
		#endregion

        #region Key Notifications
        /// <summary>
        /// Key has been pressed down.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public override void KeyDown(Control c, KeyEventArgs e)
        {
            ViewBase newView = null;
            KryptonRibbon ribbon = (KryptonRibbon)c;

            // Get the button spec associated with this controller
            ViewDrawButton viewButton = (ViewDrawButton)Target;
            ButtonSpec buttonSpec = ribbon.TabsArea.ButtonSpecManager.GetButtonSpecFromView(viewButton);

            // Note if we are on the near edge
            bool isNear = (buttonSpec.Edge == PaletteRelativeEdgeAlign.Near);

            switch (e.KeyData)
            {
                case Keys.Tab:
                case Keys.Right:
                    // Logic depends on the edge this button is on
                    if (isNear)
                    {
                        // Try getting the previous near edge button (previous on near gets the next right hand side!)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetPreviousVisibleViewButton(PaletteRelativeEdgeAlign.Near, viewButton);

                        if (newView == null)
                        {
                            if ((e.KeyData == Keys.Tab) && (ribbon.SelectedTab != null))
                            {
                                // Get the currently selected tab page
                                newView = ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab);
                            }
                            else
                            {
                                // Get the first visible tab page
                                newView = ribbon.TabsArea.LayoutTabs.GetViewForFirstRibbonTab();
                            }
                        }

                        // Get the first far edge button
                        if (newView == null)
                            newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                        // Get the first inherit edge button
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
                    }
                    else
                    {
                        // Try using the next far edge button
                        newView = ribbon.TabsArea.ButtonSpecManager.GetNextVisibleViewButton(PaletteRelativeEdgeAlign.Far, viewButton);

                        // Try using the next inherit edge button
                        if (newView == null)
                            newView = ribbon.TabsArea.ButtonSpecManager.GetNextVisibleViewButton(PaletteRelativeEdgeAlign.Inherit, viewButton);

                        // Rotate around to application button
                        if (newView == null)
                        {
                            if (ribbon.TabsArea.LayoutAppButton.Visible)
                                newView = ribbon.TabsArea.LayoutAppButton.AppButton;
                            else if (ribbon.TabsArea.LayoutAppTab.Visible)
                                newView = ribbon.TabsArea.LayoutAppTab.AppTab;
                        }
                    }
                    break;
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    // Logic depends on the edge this button is on
                    if (isNear)
                    {
                        // Try using the previous near edge button (next for a near edge is the left hand side!)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetNextVisibleViewButton(PaletteRelativeEdgeAlign.Near, viewButton);

                        // Get the last qat button
                        if (newView == null)
                            newView = ribbon.GetLastQATView();

                        // Rotate around to application button
                        if (newView == null)
                        {
                            if (ribbon.TabsArea.LayoutAppButton.Visible)
                                newView = ribbon.TabsArea.LayoutAppButton.AppButton;
                            else if (ribbon.TabsArea.LayoutAppTab.Visible)
                                newView = ribbon.TabsArea.LayoutAppTab.AppTab;
                        }
                    }
                    else
                    {
                        // Try getting the previous far edge button
                        newView = ribbon.TabsArea.ButtonSpecManager.GetPreviousVisibleViewButton(PaletteRelativeEdgeAlign.Far, viewButton);

                        // Try getting the previous inherit edge button
                        if (newView == null)
                            newView = ribbon.TabsArea.ButtonSpecManager.GetPreviousVisibleViewButton(PaletteRelativeEdgeAlign.Inherit, viewButton);

                        if (newView == null)
                        {
                            if (e.KeyData != Keys.Left)
                            {
                                // Get the last control on the selected tab
                                newView = ribbon.GroupsArea.ViewGroups.GetLastFocusItem();

                                // Get the currently selected tab page
                                if (newView == null)
                                {
                                    if (ribbon.SelectedTab != null)
                                        newView = ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab);
                                    else
                                        newView = ribbon.TabsArea.LayoutTabs.GetViewForLastRibbonTab();
                                }
                            }
                            else
                            {
                                // Get the last visible tab page
                                newView = ribbon.TabsArea.LayoutTabs.GetViewForLastRibbonTab();
                            }
                        }

                        // Get the last near edge button
                        if (newView == null)
                            newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Near);

                        // Get the last qat button
                        if (newView == null)
                            newView = ribbon.GetLastQATView();

                        // Rotate around to application button
                        if (newView == null)
                        {
                            if (ribbon.TabsArea.LayoutAppButton.Visible)
                                newView = ribbon.TabsArea.LayoutAppButton.AppButton;
                            else if (ribbon.TabsArea.LayoutAppTab.Visible)
                                newView = ribbon.TabsArea.LayoutAppTab.AppTab;
                        }
                    }
                    break;
                case Keys.Space:
                case Keys.Enter:
                    // Exit keyboard mode when you click the button spec
                    ribbon.KillKeyboardMode();

                    // Generate a click event
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
            }

            // If we have a new view to focus and it is not ourself...
            if ((newView != null) && (newView != Target))
            {
                // If the new view is a tab then select that tab unless in minimized mode
                if ((newView is ViewDrawRibbonTab) && !ribbon.RealMinimizedMode)
                    ribbon.SelectedTab = ((ViewDrawRibbonTab)newView).RibbonTab;

                // Finally we switch focus to new view
                ribbon.FocusView = newView;
            }
        }

        /// <summary>
        /// Key has been pressed.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        public override void KeyPress(Control c, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// Key has been released.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public override bool KeyUp(Control c, KeyEventArgs e)
        {
            return false;
        }
        #endregion

        #region Source Notifications
        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public override void GotFocus(Control c)
        {
            _hasFocus = true;

            // Update the visual state
            UpdateTargetState(c);
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public override void LostFocus(Control c)
        {
            _hasFocus = false;

            // Update the visual state
            UpdateTargetState(c);
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
                    OnNeedPaint(true);
                }
            }
            else
                base.UpdateTargetState(pt);
        }
        #endregion
    }
}
