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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class VisualPopupGroup : VisualPopup
    {
        #region Static Fields
        private static readonly int BOTTOMRIGHT_GAP = 4;
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private KryptonRibbonGroup _ribbonGroup;
        private ViewDrawRibbonGroupsBorder _viewBackground;
        private ViewDrawRibbonGroup _viewGroup;
        private bool _restorePreviousFocus;
        private Button _hiddenFocusTarget;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the VisualPopupGroup class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="ribbonGroup">Reference to ribbon group for display.</param>
        /// <param name="renderer">Drawing renderer.</param>
        public VisualPopupGroup(KryptonRibbon ribbon,
                                KryptonRibbonGroup ribbonGroup,
                                IRenderer renderer)
            : base(renderer, true)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(ribbonGroup != null);

            // Remember references needed later
            _ribbon = ribbon;
            _ribbonGroup = ribbonGroup;

            // Create a view element for drawing the group
            _viewGroup = new ViewDrawRibbonGroup(ribbon, ribbonGroup, NeedPaintDelegate);
            _viewGroup.Collapsed = false;

            // Create the background that will contain the actual group instance
            _viewBackground = new ViewDrawRibbonGroupsBorder(ribbon, true, NeedPaintDelegate);
            _viewBackground.Add(_viewGroup);

            // Attach the root to the view manager instance
            ViewManager = new ViewRibbonPopupGroupManager(this, ribbon, _viewBackground, _viewGroup, NeedPaintDelegate);

            // Create and add a hidden button to act as the focus target
            _hiddenFocusTarget = new Button();
            _hiddenFocusTarget.TabStop = false;
            _hiddenFocusTarget.Location = new Point(-_hiddenFocusTarget.Width, -_hiddenFocusTarget.Height);
            CommonHelper.AddControlToParent(this, _hiddenFocusTarget);
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
                ViewManager.MouseLeave(EventArgs.Empty);

                // Do we need to restore the previous focus from the ribbon
                if (_restorePreviousFocus)
                    _ribbon.RestorePreviousFocus();

                // Mark the group as no longer showing as a popup
                _ribbonGroup.ShowingAsPopup = false;

                // Remove all child controls so they do not become disposed
                for (int i = Controls.Count - 1; i >= 0; i--)
                    Controls.RemoveAt(0);

                // If this group is being dismissed with key tips showing
                if (_ribbon.InKeyboardMode &&_ribbon.KeyTipMode == KeyTipMode.PopupGroup)
                {
                    // Revert back to key tips for selected tab
                    KeyTipMode mode = (_ribbon.RealMinimizedMode ? KeyTipMode.PopupMinimized : KeyTipMode.SelectedGroups);
                    _ribbon.KeyTipMode = mode;
                    _ribbon.SetKeyTips(_ribbon.GenerateKeyTipsForSelectedTab(), mode);
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region ViewGroup
        /// <summary>
        /// Gets the view for the popup group.
        /// </summary>
        public ViewDrawRibbonGroup ViewGroup
        {
            get { return _viewGroup; }
        }
        #endregion

        #region RestorePreviousFocus
        /// <summary>
        /// Gets and sets a flag indicating if previous ribbon focus should be restored on dispose.
        /// </summary>
        public bool RestorePreviousFocus
        {
            get { return _restorePreviousFocus; }
            set { _restorePreviousFocus = value; }
        }
        #endregion

        #region SetFirstFocusItem
        /// <summary>
        /// Set focus to the first focus item inside the popup group.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public void SetFirstFocusItem()
        {
            ViewPopupManager.FocusView = _viewGroup.GetFirstFocusItem();
            PerformNeedPaint(false);
        }
        #endregion

        #region SetLastFocusItem
        /// <summary>
        /// Set focus to the last focus item inside the popup group.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public void SetLastFocusItem()
        {
            ViewPopupManager.FocusView = _viewGroup.GetLastFocusItem();
            PerformNeedPaint(false);
        }
        #endregion

        #region SetNextFocusItem
        /// <summary>
        /// Set focus to the next focus item inside the popup group.
        /// </summary>
        public void SetNextFocusItem()
        {
            // Find the next item in sequence
            bool matched = false;
            ViewBase view = _viewGroup.GetNextFocusItem(ViewPopupManager.FocusView, ref matched);

            // Rotate around to the first item
            if (view == null)
                SetFirstFocusItem();
            else
            {
                ViewPopupManager.FocusView = view;
                PerformNeedPaint(false);
            }
        }
        #endregion

        #region SetPreviousFocusItem
        /// <summary>
        /// Set focus to the previous focus item inside the popup group.
        /// </summary>
        public void SetPreviousFocusItem()
        {
            // Find the previous item in sequence
            bool matched = false;
            ViewBase view = _viewGroup.GetPreviousFocusItem(ViewPopupManager.FocusView, ref matched);

            // Rotate around to the last item
            if (view == null)
                SetLastFocusItem();
            else
            {
                ViewPopupManager.FocusView = view;
                PerformNeedPaint(false);
            }
        }
        #endregion        

        #region ShowCalculatingSize
        /// <summary>
        /// Show the group popup relative to the parent group instance.
        /// </summary>
        /// <param name="parentGroup">Parent group instance.</param>
        /// <param name="parentScreenRect">Screen rectangle of the parent.</param>
        public void ShowCalculatingSize(ViewDrawRibbonGroup parentGroup,
                                        Rectangle parentScreenRect)
        {
            Size popupSize;

            // Prevent ribbon from laying out the same group as we are
            // about to get the preferred size from. This reentrancy can
            // happen if the group has a custom control that is then moved
            // to be reparented to the popup group and so therefore cause
            // a layout of the main ribbon.
            _ribbon.SuspendLayout();
            SuspendLayout();

            try
            {
                // Find the size the group requests to be
                using (ViewLayoutContext context = new ViewLayoutContext(this, Renderer))
                    popupSize = _viewGroup.GetPreferredSize(context);

                // Override the height to enforce the correct group height
                popupSize.Height = _ribbon.CalculatedValues.GroupHeight;

                // Mark the group as showing as a popup
                _ribbonGroup.ShowingAsPopup = true;

                // Request we be shown below the parent screen rect
                Show(CalculateBelowPopupRect(parentScreenRect, popupSize));
            }
            finally
            {
                // Reverse the suspend call
                _ribbon.ResumeLayout();
                ResumeLayout();
            }
        }
        #endregion

        #region HideFocus
        /// <summary>
        /// Hide focus by giving it to the hidden control.
        /// </summary>
        public void HideFocus()
        {
            _hiddenFocusTarget.Focus();
        }
        #endregion

        #region Implementation
        private Rectangle CalculateBelowPopupRect(Rectangle parentScreenRect, Size popupSize)
        {
            // Get the screen that the parent rectangle is mostly within, this is the
            // screen we will attempt to place the entire popup within
            Screen screen = Screen.FromRectangle(parentScreenRect);
            Rectangle workingArea = screen.WorkingArea;
            workingArea.Width -= BOTTOMRIGHT_GAP;
            workingArea.Height -= BOTTOMRIGHT_GAP;

            Point popupLocation = new Point(parentScreenRect.X, parentScreenRect.Bottom);

            // Is there enough room below the parent for the entire popup height?
            if ((parentScreenRect.Bottom + popupSize.Height) <= workingArea.Bottom)
            {
                // Place the popup below the parent
                popupLocation.Y = parentScreenRect.Bottom;
            }
            else
            {
                // Is there enough room above the parent for the enture popup height?
                if ((parentScreenRect.Top - popupSize.Height) >= workingArea.Top)
                {
                    // Place the popup above the parent
                    popupLocation.Y = parentScreenRect.Top - popupSize.Height;
                }
                else
                {
                    // Cannot show entire popup above or below, find which has most space
                    int spareAbove = parentScreenRect.Top - workingArea.Top;
                    int spareBelow = workingArea.Bottom - parentScreenRect.Bottom;

                    // Place it in the area with the most space
                    if (spareAbove > spareBelow)
                        popupLocation.Y = workingArea.Top;
                    else
                        popupLocation.Y = parentScreenRect.Bottom;
                }
            }

            // Prevent the popup from being off the left side of the screen
            if (popupLocation.X < workingArea.Left)
                popupLocation.X = workingArea.Left;

            // Preven the popup from being off the right size of the screen
            if ((popupLocation.X + popupSize.Width) > workingArea.Right)
                popupLocation.X = workingArea.Right - popupSize.Width;

            return new Rectangle(popupLocation, popupSize);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the popup group specific view manager.
        /// </summary>
        protected ViewRibbonPopupGroupManager ViewPopupManager
        {
            get { return ViewManager as ViewRibbonPopupGroupManager; }
        }

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
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">An EventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Let base class calulcate fill rectangle
            base.OnLayout(levent);

            // Ribbon shape determines the border rounding required
            int borderRounding;
            switch (_ribbon.RibbonShape)
            {
                default:
                case PaletteRibbonShape.Office2007:
                    borderRounding = 2;
                    break;
                case PaletteRibbonShape.Office2010:
                    borderRounding = 1;
                    break;
            }

            // Update the region of the popup to be the border path
            using (GraphicsPath roundPath = CommonHelper.RoundedRectanglePath(ClientRectangle, borderRounding))
                Region = new Region(roundPath);
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

        /// <summary>
        /// Processes a dialog key.
        /// </summary>
        /// <param name="keyData">One of the Keys values that represents the key to process.</param>
        /// <returns>True is handled; otherwise false.</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Grab the controlling control that is a parent
            Control c = _ribbon.GetControllerControl(this);

            // Grab the view manager handling the focus view
            ViewBase focusView = ((ViewRibbonPopupGroupManager)GetViewManager()).FocusView;

            // When in keyboard mode...
            if (focusView != null)
            {
                // We pass movements keys onto the view
                switch (keyData)
                {
                    case Keys.Tab | Keys.Shift:
                    case Keys.Tab:
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Space:
                    case Keys.Enter:
                        _ribbon.KillKeyboardKeyTips();
                        focusView.KeyDown(new KeyEventArgs(keyData));
                        return true;
                }
            }

            return base.ProcessDialogKey(keyData);
        }
        #endregion
    }
}
