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
    internal class VisualPopupQATOverflow : VisualPopup
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private ViewDrawRibbonQATOverflow _viewQAT;
        private ViewLayoutRibbonQATContents _viewQATContents;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the VisualPopupQATOverflow class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="contents">Reference to original contents which has overflow items.</param>
        /// <param name="renderer">Drawing renderer.</param>
        public VisualPopupQATOverflow(KryptonRibbon ribbon,
                                      ViewLayoutRibbonQATContents contents,
                                      IRenderer renderer)
            : base(renderer, true)
        {
            Debug.Assert(ribbon != null);

            // Remember references needed later
            _ribbon = ribbon;

            // Create a view element for drawing the group
            _viewQAT = new ViewDrawRibbonQATOverflow(ribbon, NeedPaintDelegate);

            // Create and add the element used to synch and draw the actual contents
            _viewQATContents = new ViewLayoutRibbonQATFromOverflow(this, ribbon, 
                                                                   NeedPaintDelegate, 
                                                                   true, contents);
            _viewQAT.Add(_viewQATContents);

            // Attach the root to the view manager instance
            ViewManager = new ViewRibbonQATOverflowManager(ribbon, this, _viewQATContents, _viewQAT);
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

                // Remove all child controls so they do not become disposed
                for (int i = Controls.Count - 1; i >= 0; i--)
                    Controls.RemoveAt(0);

                // If this group is being dismissed with key tips showing
                if (_ribbon.InKeyboardMode && _ribbon.KeyTipMode == KeyTipMode.PopupQATOverflow)
                {
                    // Revert back to key tips for selected tab
                    _ribbon.KeyTipMode = KeyTipMode.Root;
                    _ribbon.SetKeyTips(_ribbon.GenerateKeyTipsAtTopLevel(), KeyTipMode.Root);
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region ViewOverflowManager
        /// <summary>
        /// Gets the qat overflow manager.
        /// </summary>
        public ViewRibbonQATOverflowManager ViewOverflowManager
        {
            get { return ViewManager as ViewRibbonQATOverflowManager; }
        }
        #endregion

        #region ViewQATContents
        /// <summary>
        /// Gets access to the quick access toolbar contents view.
        /// </summary>
        public ViewLayoutRibbonQATContents ViewQATContents
        {
            get { return _viewQATContents; }
        }
        #endregion

        #region SetFirstFocusItem
        /// <summary>
        /// Set focus to the first focus item inside the popup group.
        /// </summary>
        /// <returns>ViewBase of item; otherwise false.</returns>
        public void SetFirstFocusItem()
        {
            ViewOverflowManager.FocusView = _viewQATContents.GetFirstQATView();
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
            ViewOverflowManager.FocusView = _viewQATContents.GetLastQATView();
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
            ViewBase view = _viewQATContents.GetNextQATView(ViewOverflowManager.FocusView);

            // Rotate around to the first item
            if (view == null)
                SetFirstFocusItem();
            else
            {
                ViewOverflowManager.FocusView = view;
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
            ViewBase view = _viewQATContents.GetPreviousQATView(ViewOverflowManager.FocusView);

            // Rotate around to the last item
            if (view == null)
                SetLastFocusItem();
            else
            {
                ViewOverflowManager.FocusView = view;
                PerformNeedPaint(false);
            }
        }
        #endregion

        #region ShowCalculatingSize
        /// <summary>
        /// Show the quick access toolbar popup relative to the parent area.
        /// </summary>
        /// <param name="parentScreenRect">Screen rectangle of the parent.</param>
        /// <param name="finishDelegate">Delegate fired when popup dismissed.</param>
        public void ShowCalculatingSize(Rectangle parentScreenRect,
                                        EventHandler finishDelegate)
        {
            Size popupSize;

            // Find the size the quick access toolbar requests to be
            using (ViewLayoutContext context = new ViewLayoutContext(this, Renderer))
                popupSize = _viewQAT.GetPreferredSize(context);

            DismissedDelegate = finishDelegate;

            // Request we be shown below the parent screen rect
            Show(parentScreenRect, popupSize);
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
        #endregion
    }
}
