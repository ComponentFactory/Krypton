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
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// View element that contains a control that has a view hierarchy of its own.
    /// </summary>
    public class ViewLayoutControl : ViewLeaf
    {
        #region Instance Fields
        private ViewControl _viewControl;
        private ViewBase _viewChild;
        private Point _layoutOffset;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutControl class.
        /// </summary>
        /// <param name="rootControl">Top level visual control.</param>
        /// <param name="viewChild">View used to size and position the child control.</param>
        public ViewLayoutControl(VisualControl rootControl,
                                 ViewBase viewChild)
            : this(new ViewControl(rootControl), rootControl, viewChild)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ViewLayoutControl class.
        /// </summary>
        /// <param name="viewControl">View control to use as child.</param>
        /// <param name="rootControl">Top level visual control.</param>
        /// <param name="viewChild">View used to size and position the child control.</param>
        public ViewLayoutControl(ViewControl viewControl,
                                 VisualControl rootControl,
                                 ViewBase viewChild)
        {
            Debug.Assert(viewControl != null);
            Debug.Assert(rootControl != null);
            Debug.Assert(viewChild != null);

            // Default values
            _layoutOffset = Point.Empty;

            // Remember the view
            _viewChild = viewChild;

            // Ensure the child is hooked into the hierarchy of elements
            _viewChild.Parent = this;

            // Create the view control instance
            _viewControl = viewControl;

            // Back reference hookup
            _viewControl.ViewLayoutControl = this;

            // Start off invisible until first layed out
            _viewControl.Visible = false;

            // Ensure that all view elements inside here use our control
            OwningControl = _viewControl;

            // Add our new control to the provided parent collection
            CommonHelper.AddControlToParent(rootControl, _viewControl);
        }

        /// <summary>
        /// Release unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing">Called from Dispose method.</param>
        protected override void Dispose(bool disposing)
        {
            // If called from explicit call to Dispose
            if (disposing)
            {
                if (_viewControl != null)
                {
                    try
                    {
                        ViewControl vc = _viewControl;
                        _viewControl = null;
                        CommonHelper.RemoveControlFromParent(vc);
                    }
                    catch { }
                }

                if (_viewChild != null)
                {
                    _viewChild.Dispose();
                    _viewChild = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutControl:" + Id + " ClientLocation:" + ClientLocation;
        }
        #endregion

        #region Visible
        /// <summary>
        /// Gets and sets the visible state of the element.
        /// </summary>
        public override bool Visible
        {
            get { return base.Visible; }

            set
            {
                if (base.Visible != value)
                {
                    base.Visible = value;

                    // During disposal the view control will not longer exist
                    if (_viewControl != null)
                    {
                        // Only want the child real control to show when we are
                        _viewControl.Visible = value;
                    }
                }
            }
        }
        #endregion

        #region LayoutOffset
        /// <summary>
        /// Gets and sets the offset to apply the layout of the child view.
        /// </summary>
        public Point LayoutOffset
        {
            get { return _layoutOffset; }
            set { _layoutOffset = value; }
        }
        #endregion

        #region ChildView
        /// <summary>
        /// Gets access to the child view.
        /// </summary>
        public ViewBase ChildView
        {
            get { return _viewChild; }
        }
        #endregion

        #region ChildControl
        /// <summary>
        /// Gets access to the child control.
        /// </summary>
        public ViewControl ChildControl
        {
            get { return _viewControl; }
        }
        #endregion

        #region ChildPaintDelegate
        /// <summary>
        /// Gets access to the child controls paint delegate.
        /// </summary>
        public NeedPaintHandler ChildPaintDelegate
        {
            get { return _viewControl.NeedPaintDelegate; }
        }
        #endregion

        #region ChildTransparentBackground
        /// <summary>
        /// Gets and sets if the background is transparent.
        /// </summary>
        public bool ChildTransparentBackground
        {
            get { return _viewControl.TransparentBackground; }
            set { _viewControl.TransparentBackground = value; }
        }
        #endregion

        #region InDesignMode
        /// <summary>
        /// Gets and sets a value indicating if the control is in design mode.
        /// </summary>
        public bool InDesignMode
        {
            get { return _viewControl.InDesignMode; }
            set { _viewControl.InDesignMode = value; }
        }
        #endregion

        #region MakeParent
        /// <summary>
        /// Reparent the provided control as a child of ourself.
        /// </summary>
        /// <param name="c">Control to reparent.</param>
        public void MakeParent(Control c)
        {
            // Remove control from current collection
            CommonHelper.RemoveControlFromParent(c);

            // Add to our child control
            CommonHelper.AddControlToParent(_viewControl, c);
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // During disposal the view control will not longer exist
            if (_viewControl != null)
            {
                // Ensure the control has the correct parent
                UpdateParent(context.Control);

                // Ensure context has the correct control
                using (CorrectContextControl ccc = new CorrectContextControl(context, _viewControl))
                {
                    // Ask the view for its preferred size
                    if (_viewChild != null)
                        return _viewChild.GetPreferredSize(context);
                }
            }

            return Size.Empty;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // During disposal the view control will not longer exist
            if (_viewControl != null)
            {
                // Ensure context has the correct control
                using (CorrectContextControl ccc = new CorrectContextControl(context, _viewControl))
                {
                    // We take on all the available display area
                    ClientRectangle = context.DisplayRectangle;

                    // Are we allowed to layout child controls?
                    if (!context.ViewManager.DoNotLayoutControls)
                    {
                        // Do we have a control to position?
                        if (_viewControl != null)
                        {
                            // Size and position the child control
                            _viewControl.SetBounds(ClientLocation.X, ClientLocation.Y, ClientWidth, ClientHeight);

                            // Ensure the visible/enabled states are up to date
                            _viewControl.Visible = Visible;
                            _viewControl.Enabled = Enabled;

                            // A layout means something might have changed, so better redraw it
                            _viewControl.Invalidate();
                        }
                    }

                    // Adjust the view location to be at the top left of the child control
                    context.DisplayRectangle = new Rectangle(LayoutOffset, ClientSize);

                    // Do we have a child view to layout?
                    if (_viewChild != null)
                    {
                        // Layout the child view
                        _viewChild.Layout(context);
                    }

                    // Put back the original display value now we have finished
                    context.DisplayRectangle = ClientRectangle;
                }
            }
        }
        #endregion

        #region ViewFromPoint
        /// <summary>
        /// Find the view that contains the specified point.
        /// </summary>
        /// <param name="pt">Point in view coordinates.</param>
        /// <returns>ViewBase if a match is found; otherwise false.</returns>
        public override ViewBase ViewFromPoint(Point pt)
        {
            // If we contain a child view
            if (_viewChild != null)
            {
                // Is the point inside this controls area?
                if (ClientRectangle.Contains(pt))
                {
                    // Convert to contained view coordinates
                    return _viewChild.ViewFromPoint(new Point(pt.X - ClientLocation.X,
                                                              pt.Y - ClientLocation.Y));
                }
            }

            return null;
        }
        #endregion

        #region UpdateParent
        private void UpdateParent(Control parentControl)
        {
            // During disposal the view control will no longer exist
            if (_viewControl != null)
            {
                // If the view control is not inside the correct parent
                if (parentControl != _viewControl.Parent)
                {
                    // Ensure the control is not in the display area when first added
                    _viewControl.Location = new Point(-_viewControl.Width, -_viewControl.Height);

                    // Add our control to the provided parent collection
                    CommonHelper.AddControlToParent(parentControl, _viewControl);

                    // Let the actual control hook into correct parent for view manager processing
                    _viewControl.UpdateParent(parentControl);
                }
            }
        }
        #endregion
    }
}
