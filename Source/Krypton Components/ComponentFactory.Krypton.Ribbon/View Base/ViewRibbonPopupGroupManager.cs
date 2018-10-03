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
    internal class ViewRibbonPopupGroupManager : ViewManager
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private ViewDrawRibbonGroup _viewGroup;
        private NeedPaintHandler _needPaintDelegate;
        private ViewBase _focusView;
        private bool _layingOut;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewRibbonPopupGroupManager class.
		/// </summary>
        /// <param name="control">Owning control.</param>
        /// <param name="ribbon">Owning ribbon control instance.</param>
        /// <param name="root">View for group we are tracking.</param>
        /// <param name="viewGroup">Group to track.</param>
        /// <param name="needPaintDelegate">Delegate for performing painting.</param>
        public ViewRibbonPopupGroupManager(Control control,
                                           KryptonRibbon ribbon,
                                           ViewBase root,
                                           ViewDrawRibbonGroup viewGroup,
                                           NeedPaintHandler needPaintDelegate)
            : base(control, root)
		{
            Debug.Assert(ribbon != null);
            Debug.Assert(viewGroup != null);
            Debug.Assert(needPaintDelegate != null);

            _ribbon = ribbon;
            _viewGroup = viewGroup;
            _needPaintDelegate = needPaintDelegate;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public override void Dispose()
        {
            // Remove focus from current view
            FocusView = null;

            base.Dispose();
        }
        #endregion

        #region GetPreferredSize
        /// <summary>
        /// Discover the preferred size of the view.
        /// </summary>
        /// <param name="renderer">Renderer provider.</param>
        /// <param name="proposedSize">The custom-sized area for a control.</param>
        public override Size GetPreferredSize(IRenderer renderer,
                                              Size proposedSize)
        {
            // Update the calculated values used during layout calls
            _ribbon.CalculatedValues.Recalculate();

            // Let base class perform standard preferred sizing actions
            return base.GetPreferredSize(renderer, proposedSize);
        }
        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the view.
        /// </summary>
        /// <param name="context">View context for layout operation.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Prevent reentrancy
            if (!_layingOut)
            {
                _layingOut = true;

                // Update the calculated values used during layout calls
                _ribbon.CalculatedValues.Recalculate();

                // Let base class perform standard layout actions
                base.Layout(context);

                _layingOut = false;
            }
        }
        #endregion

        #region Mouse
        /// <summary>
        /// Perform mouse movement handling.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        /// <param name="rawPt">The actual point provided from the windows message.</param>
        public override void MouseMove(MouseEventArgs e, Point rawPt)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");

            // Should the group be active
            bool tracking = _viewGroup.ClientRectangle.Contains(new Point(e.X, e.Y));

            // Is there a change in active group?
            if (tracking != _viewGroup.Tracking)
            {
                _viewGroup.Tracking = tracking;
                _viewGroup.PerformNeedPaint(false, _viewGroup.ClientRectangle);
            }

            // Remember to call base class for standard mouse processing
            base.MouseMove(e, rawPt);
        }

        /// <summary>
        /// Perform mouse leave processing.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        public override void MouseLeave(EventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");

            // Do we need to remove tracking
            if (_viewGroup.Tracking)
            {
                _viewGroup.Tracking = false;
                _viewGroup.PerformNeedPaint(false, _viewGroup.ClientRectangle);
            }

            // Remember to call base class for standard mouse processing
            base.MouseLeave(e);
        }
        #endregion

        #region Key
        /// <summary>
        /// Perform key down handling.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public override void KeyDown(KeyEventArgs e)
        {
            // Tell current view of key event
            if (FocusView != null)
                FocusView.KeyDown(e);
        }

        /// <summary>
        /// Perform key press handling.
        /// </summary>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        public override void KeyPress(KeyPressEventArgs e)
        {
            // Tell current view of key event
            if (FocusView != null)
                FocusView.KeyPress(e);
        }

        /// <summary>
        /// Perform key up handling.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public override void KeyUp(KeyEventArgs e)
        {
            // Tell current view of key event
            if (FocusView != null)
                MouseCaptured = FocusView.KeyUp(e);
        }
        #endregion

        #region FocusView
        /// <summary>
        /// Gets and sets the view that has the focus.
        /// </summary>
        public ViewBase FocusView
        {
            get { return _focusView; }

            set
            {
                // Only interested in changes of focus
                if (_focusView != value)
                {
                    // Remove focus from existing view
                    if (_focusView != null)
                        _focusView.LostFocus(Root.OwningControl);

                    _focusView = value;

                    // Add focus to the new view
                    if (_focusView != null)
                        _focusView.GotFocus(Root.OwningControl);
                }
            }
        }
        #endregion

        #region Implementation
        private void PerformNeedPaint(bool needLayout, Rectangle invalidRect)
        {
            if (_needPaintDelegate != null)
                _needPaintDelegate(this, new NeedLayoutEventArgs(needLayout, invalidRect));
        }
        #endregion
    }
}
