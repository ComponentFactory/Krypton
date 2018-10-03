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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class VisualPopupMinimized : VisualPopup
    {
        #region Static Fields
        private static readonly int MINIMUM_WIDTH = 100;
        private static readonly int BOTTOMRIGHT_GAP = 4;
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private ViewDrawRibbonCaptionArea _captionArea;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the VisualPopupMinimized class.
		/// </summary>
        /// <param name="ribbon">Owning ribbon control instance.</param>
        /// <param name="viewManager">View manager instance for managing view display.</param>
        /// <param name="captionArea">View element that manages the custom chrome injection.</param>
        /// <param name="renderer">Drawing renderer.</param>
        public VisualPopupMinimized(KryptonRibbon ribbon,
                                    ViewManager viewManager,
                                    ViewDrawRibbonCaptionArea captionArea,
                                    IRenderer renderer)
            : base(viewManager, renderer, true)
		{
            Debug.Assert(ribbon != null);
            Debug.Assert(captionArea != null);

            // Remember incoming references
            _ribbon = ribbon;
            _captionArea = captionArea;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Ensure the manager believes the mouse has left the area
                ViewRibbonManager.MouseLeave(EventArgs.Empty);

                // If this group is being dismissed with key tips showing
                if (_ribbon.InKeyboardMode && _ribbon.KeyTipMode == KeyTipMode.PopupMinimized)
                {
                    // Revert back to key tips for selected tab
                    _ribbon.KeyTipMode = KeyTipMode.Root;
                    _ribbon.SetKeyTips(_ribbon.GenerateKeyTipsAtTopLevel(), KeyTipMode.Root);
                }

                // Remove the collection of children elements, so they are not disposed,
                // disposing them would cause the ones that have real Win32 controls to
                // also dispose and we do not want that!
                if (ViewManager != null)
                {
                    ViewManager.ActiveView = null;
                    ViewManager.Root = new ViewLayoutNull();
                }

                // Remove all child controls so they do not become disposed
                for (int i = Controls.Count - 1; i >= 0; i--)
                    Controls.RemoveAt(0);
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the view for the popup group.
        /// </summary>
        public ViewRibbonMinimizedManager ViewRibbonManager
        {
            get { return ViewManager as ViewRibbonMinimizedManager; }
        }

        /// <summary>
        /// Sets focus to the first focus item inside the selected tab.
        /// </summary>
        public void SetFirstFocusItem()
        {
            ViewBase newView = _ribbon.GroupsArea.ViewGroups.GetFirstFocusItem();

            // Make the item the new focus for the popup
            if (newView != null)
            {
                ViewRibbonManager.FocusView = newView;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Set focus to the last focus item inside the popup group.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public void SetLastFocusItem()
        {
            ViewBase newView = _ribbon.GroupsArea.ViewGroups.GetLastFocusItem();

            // Make the item the new focus for the popup
            if (newView != null)
            {
                ViewRibbonManager.FocusView = newView;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Set focus to the next focus item inside the popup group.
        /// </summary>
        public void SetNextFocusItem()
        {
            // Find the next item in sequence
            ViewBase newView = _ribbon.GroupsArea.ViewGroups.GetNextFocusItem(ViewRibbonManager.FocusView);

            // Rotate around to the first item
            if (newView == null)
                SetFirstFocusItem();
            else
            {
                ViewRibbonManager.FocusView = newView;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Set focus to the previous focus item inside the popup group.
        /// </summary>
        public void SetPreviousFocusItem()
        {
            // Find the previous item in sequence
            ViewBase newView = _ribbon.GroupsArea.ViewGroups.GetPreviousFocusItem(ViewRibbonManager.FocusView);

            // Rotate around to the last item
            if (newView == null)
                SetLastFocusItem();
            else
            {
                ViewRibbonManager.FocusView = newView;
                PerformNeedPaint(false);
            }
        }

        /// <summary>
        /// Should a mouse down at the provided point cause an end to popup tracking.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to end tracking; otherwise false.</returns>
        public override bool DoesCurrentMouseDownEndAllTracking(Message m, Point pt)
        {
            // Convert point to the ribbon control coordinates
            Point screenPt = PointToScreen(pt);
            Point ribbonPt = _ribbon.PointToClient(screenPt);

            // If the base class wants to end tracking and not inside the ribbon control
            return base.DoesCurrentMouseDownEndAllTracking(m, pt) &&
                   !_ribbon.ClientRectangleWithoutComposition.Contains(ribbonPt) &&
                   _captionArea.DoesCurrentMouseDownEndAllTracking(screenPt);
        }

        /// <summary>
        /// Should the mouse move at provided screen point be allowed.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to alow; otherwise false.</returns>
        public override bool AllowMouseMove(Message m, Point pt)
        {
            // Convert point to the ribbon control coordinates
            Point ribbonPt = _ribbon.PointToClient(pt);

            // We allow mouse moves over the ribbon so that the tabs can hot track but
            // only when the minimized popup is the current tracking popup
            if ((this == VisualPopupManager.Singleton.CurrentPopup) &&
                _ribbon.ClientRectangle.Contains(ribbonPt))
                return true;

            // Let base class perform standard processing
            return base.AllowMouseMove(m, pt);
        }

        /// <summary>
        /// Show the minimized popup relative to the tabs area of the ribbon.
        /// </summary>
        /// <param name="tabsArea">Tabs area of the </param>
        /// <param name="drawMinimizedPanel"></param>
        public void Show(ViewLayoutRibbonTabsArea tabsArea, 
                         ViewDrawPanel drawMinimizedPanel)
        {
            // Show at the calculated position
            Show(CalculatePopupRect(tabsArea, drawMinimizedPanel));
        }

        /// <summary>
        /// Update the displayed position to reflect a change in selected tab.
        /// </summary>
        /// <param name="tabsArea">Tabs area of the </param>
        /// <param name="drawMinimizedPanel"></param>
        public void UpdatePosition(ViewLayoutRibbonTabsArea tabsArea, 
                                   ViewDrawPanel drawMinimizedPanel)
        {
            // Move to the newly calculated position
            Rectangle popupRect = CalculatePopupRect(tabsArea, drawMinimizedPanel);
            SetBounds(popupRect.X, popupRect.Y, popupRect.Width, popupRect.Height);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the creation parameters.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= PI.WS_CLIPCHILDREN;
                return cp;
            }
        }

        /// <summary>
        /// Raises the KeyPress event.
        /// </summary>
        /// <param name="e">An KeyPressEventArgs that contains the event data.</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // If in keyboard mode then pass character onto the key tips
            if (_ribbon.InKeyboardMode && _ribbon.InKeyTipsMode)
                _ribbon.AppendKeyTipPress(char.ToUpper(e.KeyChar));

            base.OnKeyPress(e);
        }
        #endregion

        #region Implementation
        private Rectangle CalculatePopupRect(ViewLayoutRibbonTabsArea tabsArea,
                                             ViewDrawPanel drawMinimizedPanel)
        {
            Size popupSize;

            // Get the preferred size of the groups area, we only really want the height
            using (ViewLayoutContext context = new ViewLayoutContext(_ribbon, Renderer))
                popupSize = drawMinimizedPanel.GetPreferredSize(context);

            // The width should default to being the same width as the ribbon control
            popupSize.Width = _ribbon.Width;

            // Get the screen rectangles for the ribbon and the tabs area of the ribbon
            Rectangle parentRibbonRect = _ribbon.RectangleToScreen(_ribbon.ClientRectangle);
            Rectangle parentTabsRect = _ribbon.RectangleToScreen(tabsArea.ClientRectangle);

            // Default popup is placed below the ribbon
            Rectangle popupRect = new Rectangle(parentRibbonRect.X, parentTabsRect.Bottom - 1, popupSize.Width, popupSize.Height);

            // Get the view element for the currently selected tab
            ViewDrawRibbonTab viewTab = tabsArea.LayoutTabs.GetViewForRibbonTab(_ribbon.SelectedTab);

            // Convert the view tab client area to screen coordinates
            Rectangle viewTabRect = _ribbon.RectangleToScreen(viewTab.ClientRectangle);

            // Get the screen that the tab is mostly within
            Screen screen = Screen.FromRectangle(viewTabRect);
            Rectangle workingArea = screen.WorkingArea;
            workingArea.Width -= BOTTOMRIGHT_GAP;
            workingArea.Height -= BOTTOMRIGHT_GAP;

            // Is the right side of the popup extending over the right edge of the screen...
            if (popupRect.Right > workingArea.Right)
            {
                // Reduce width to bring it back onto the screen
                popupRect.Width -= (popupRect.Right - workingArea.Right);

                // Enforce a minimum useful width for the popup
                if (popupRect.Width < MINIMUM_WIDTH)
                {
                    popupRect.X -= (MINIMUM_WIDTH - popupRect.Width);
                    popupRect.Width = MINIMUM_WIDTH;
                }
            }
            else
            {
                // Is the left side of the popup extending over left edge of screen...
                if (popupRect.Left < workingArea.Left)
                {
                    // Reduce left side of popup to start at screen left edge
                    int reduce = (workingArea.Left - popupRect.Left);
                    popupRect.Width -= reduce;
                    popupRect.X += reduce;

                    // Enforce a minimum useful width for the popup
                    if (popupRect.Width < MINIMUM_WIDTH)
                        popupRect.Width = MINIMUM_WIDTH;
                }
            }

            // If there is not enough room to place the popup below the tabs area
            if (popupRect.Bottom > workingArea.Bottom)
            {
                // Is there enough room above the parent for the entire popup height?
                if ((parentTabsRect.Top - popupRect.Height) >= workingArea.Top)
                {
                    // Place the popup above the parent
                    popupRect.Y = parentTabsRect.Top - popupSize.Height;
                }
                else
                {
                    // Cannot show entire popup above or below, find which has most space
                    int spareAbove = parentTabsRect.Top - workingArea.Top;
                    int spareBelow = workingArea.Bottom - parentTabsRect.Bottom;

                    // Place it in the area with the most space
                    if (spareAbove > spareBelow)
                        popupRect.Y = workingArea.Top;
                    else
                        popupRect.Y = parentTabsRect.Bottom;
                }
            }

            return popupRect;
        }
        #endregion
    }
}
