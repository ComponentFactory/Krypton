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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// View element that provides scrollbars around a viewport filler.
    /// </summary>
    public class ViewLayoutScrollViewport : ViewLayoutDocker
    {
        #region Instance Fields
        private ViewLayoutControl _viewControl;
        private ViewLayoutViewport _viewport;
        private ViewDrawScrollBar _scrollbarV;
        private ViewDrawScrollBar _scrollbarH;
        private ViewDrawBorderEdge _borderEdgeV;
        private ViewDrawBorderEdge _borderEdgeH;
        private NeedPaintHandler _needPaintDelegate;
        private bool _viewportVertical;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when animation has moved another step.
        /// </summary>
        public event EventHandler AnimateStep;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutScrollViewport class.
		/// </summary>
        /// <param name="rootControl">Top level visual control.</param>
        /// <param name="viewportFiller">View element to place inside viewport.</param>
        /// <param name="paletteBorderEdge">Palette for use with the border edge.</param>
        /// <param name="paletteMetrics">Palette source for metrics.</param>
        /// <param name="metricPadding">Metric used to get view padding.</param>
        /// <param name="metricOvers">Metric used to get overposition.</param>
        /// <param name="orientation">Orientation for the viewport children.</param>
        /// <param name="alignment">Alignment of the children within the viewport.</param>
        /// <param name="animateChange">Animate changes in the viewport.</param>
        /// <param name="vertical">Is the viewport vertical.</param>
        /// <param name="needPaintDelegate">Delegate for notifying paint requests.</param>
        public ViewLayoutScrollViewport(VisualControl rootControl,
                                        ViewBase viewportFiller,
                                        PaletteBorderEdge paletteBorderEdge,
                                        IPaletteMetric paletteMetrics,
                                        PaletteMetricPadding metricPadding,
                                        PaletteMetricInt metricOvers,
                                        VisualOrientation orientation,
                                        RelativePositionAlign alignment,
                                        bool animateChange,
                                        bool vertical,
                                        NeedPaintHandler needPaintDelegate)
        {
            Debug.Assert(rootControl != null);
            Debug.Assert(viewportFiller != null);
            Debug.Assert(needPaintDelegate != null);

            // We need a way to notify changes in layout
            _needPaintDelegate = needPaintDelegate;

            // By default we are showing the contained viewport in vertical scrolling
            _viewportVertical = vertical;

            // Our initial visual orientation should match the parameter
            Orientation = orientation;

            // Create the child viewport
            _viewport = new ViewLayoutViewport(paletteMetrics, metricPadding,
                                               metricOvers, ViewportOrientation(_viewportVertical),
                                               alignment, animateChange);

            // Default to same alignment for both directions
            _viewport.CounterAlignment = alignment;

            // We always want the viewport to fill any remainder space
            _viewport.FillSpace = true;

            // Put the provided element inside the viewport
            _viewport.Add(viewportFiller);

            // Hook into animation step events
            _viewport.AnimateStep += new EventHandler(OnAnimateStep);

            // To prevent the contents of the viewport from being able to draw outside
            // the viewport (such as having child controls) we use a ViewLayoutControl
            // that uses a child control to restrict the drawing region.
            _viewControl = new ViewLayoutControl(rootControl, _viewport);
            _viewControl.InDesignMode = rootControl.InDesignMode;

            // Create the scrollbar and matching border edge
            _scrollbarV = new ViewDrawScrollBar(true);
            _scrollbarH = new ViewDrawScrollBar(false);
            _borderEdgeV = new ViewDrawBorderEdge(paletteBorderEdge, System.Windows.Forms.Orientation.Vertical);
            _borderEdgeH = new ViewDrawBorderEdge(paletteBorderEdge, System.Windows.Forms.Orientation.Horizontal);

            // Hook into scroll position changes
            _scrollbarV.ScrollChanged += new EventHandler(OnScrollVChanged);
            _scrollbarH.ScrollChanged += new EventHandler(OnScrollHChanged);

            // Add with appropriate docking style
            Add(_viewControl, ViewDockStyle.Fill);
            Add(_borderEdgeV, ViewDockStyle.Right);
            Add(_borderEdgeH, ViewDockStyle.Bottom);
            Add(_scrollbarV, ViewDockStyle.Right);
            Add(_scrollbarH, ViewDockStyle.Bottom);
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
                // Unhook from events
                Viewport.AnimateStep -= new EventHandler(OnAnimateStep);
                ScrollbarV.ScrollChanged -= new EventHandler(OnScrollVChanged);
                ScrollbarH.ScrollChanged -= new EventHandler(OnScrollHChanged);
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
            return "ViewLayoutScrollViewport:" + Id;
        }
        #endregion

        #region MakeParent
        /// <summary>
        /// Make the provided control parented to ourself.
        /// </summary>
        /// <param name="c">Control to reparent.</param>
        public void MakeParent(Control c)
        {
            // Ask the view control to perform reparenting
            ViewControl.MakeParent(c);
        }
        #endregion

        #region RevertParent
        /// <summary>
        /// Revert the provided control back to a different control.
        /// </summary>
        /// <param name="newParent">Control to become parent.</param>
        /// <param name="c">Control to reparent.</param>
        public void RevertParent(Control newParent, Control c)
        {
            // Remove control from current collection
            CommonHelper.RemoveControlFromParent(c);

            // Add to our child control
            CommonHelper.AddControlToParent(newParent, c);
        }
        #endregion

        #region VerticalViewport
        /// <summary>
        /// Gets and sets the visual orientation.
        /// </summary>
        public bool VerticalViewport
        {
            set 
            {
                if (_viewportVertical != value)
                {
                    // Use new value
                    _viewportVertical = value;

                    // Update the orientation of the scrollbar
                    Viewport.Orientation = ViewportOrientation(value);
                }
            }

            get { return _viewportVertical; }
        }
        #endregion

        #region SetPalettes
        /// <summary>
        /// Update the palettes being used by the view.
        /// </summary>
        /// <param name="borderEdge">Palette for the border edge.</param>
        public void SetPalettes(PaletteBorderEdge borderEdge)
        {
            BorderEdgeV.SetPalettes(borderEdge);
            BorderEdgeH.SetPalettes(borderEdge);
        }
        #endregion

        #region AnimateChange
        /// <summary>
        /// Gets and sets the use of animation when bringing into view.
        /// </summary>
        public bool AnimateChange
        {
            get { return Viewport.AnimateChange; }
            set { Viewport.AnimateChange = value; }
        }
        #endregion

        #region BringIntoView
        /// <summary>
        /// Move viewport to display the requested part of area.
        /// </summary>
        /// <param name="rect">Rectangle to display.</param>
        public void BringIntoView(Rectangle rect)
        {
            if (VerticalViewport)
                rect.Width = Viewport.ClientWidth;
            else
                rect.Height = Viewport.ClientHeight;

            // Ask the actual viewport to perform the action
            Viewport.BringIntoView(rect);
        }
        #endregion

        #region Layout
        /// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override void Layout(ViewLayoutContext context)
		{
            bool relayout;
            bool canScrollV;
            bool canScrollH;

            // Update the enabled state of the scrollbars and contained control
            ViewControl.Enabled = this.Enabled;
            ScrollbarV.Enabled = this.Enabled;
            ScrollbarH.Enabled = this.Enabled;
            BorderEdgeV.Enabled = this.Enabled;
            BorderEdgeH.Enabled = this.Enabled;

            // Cache the starting viewport offsets
            Point originalOffset = Viewport.Offset;

            // Hide both scrollbars, in case having them both hidden
            // always enough content to be seen that none or only one
            // of them is required.
            BorderEdgeV.Visible = ScrollbarV.Visible = false;
            BorderEdgeH.Visible = ScrollbarH.Visible = false;

            // Do not actually change the layout of any child controls
            context.ViewManager.DoNotLayoutControls = true;

            do
            {
                // Do we need to layout again?
                relayout = false;

                // Always reinstate the cached offset, so that if one of the cycles
                // around limits the offset to a different value then subsequent cycles
                // will not remember that artificial limitation
                Viewport.Offset = originalOffset;

                // Make sure the viewport has extents calculated
                Viewport.GetPreferredSize(context);

                // Let base class perform a layout calculation
                base.Layout(context);

                // Find the latest scrolling requirement
                canScrollV = Viewport.CanScrollV;
                canScrollH = Viewport.CanScrollH;

                // Is there a change in vertical scrolling?
                if (canScrollV != ScrollbarV.Visible)
                {
                    // Update the view elements
                    ScrollbarV.Visible = canScrollV;
                    BorderEdgeV.Visible = canScrollV;
                    relayout = true;
                }

                // Is there a change in horizontally scrolling?
                if (canScrollH != ScrollbarH.Visible)
                {
                    // Update the view elements
                    ScrollbarH.Visible = canScrollH;
                    BorderEdgeH.Visible = canScrollH;
                    relayout = true;
                }

                // We short size the horizontal scrollbar if both bars are showing
                bool needShortSize = (ScrollbarV.Visible && ScrollbarH.Visible);

                if (ScrollbarH.ShortSize != needShortSize)
                {
                    // Update the scrollbar view and need layout to reflect resizing
                    ScrollbarH.ShortSize = needShortSize;
                    relayout = true;
                }

            } while (relayout);

            // Now all layouts have occured we can actually move child controls
            context.ViewManager.DoNotLayoutControls = false;

            // Perform actual layout of child controls
            foreach (ViewBase child in this)
            {
                context.DisplayRectangle = child.ClientRectangle;
                child.Layout(context);
            }

            // Do we need to update the vertical scrolling values?
            if (canScrollV)
                ScrollbarV.SetScrollValues(0, Viewport.ScrollExtent.Height - 1,
                                           1, Viewport.ClientSize.Height,
                                           Viewport.ScrollOffset.Y);

            // Do we need to update the horizontal scrolling values?
            if (canScrollH)
                ScrollbarH.SetScrollValues(0, Viewport.ScrollExtent.Width - 1,
                                           1, Viewport.ClientSize.Width,
                                           Viewport.ScrollOffset.X);
        }
		#endregion

        #region Accessors
        /// <summary>
        /// Gets access to the view control instance.
        /// </summary>
        public ViewLayoutControl ViewControl
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _viewControl; }
        }

        /// <summary>
        /// Gets access to the viewport view instance.
        /// </summary>
        public ViewLayoutViewport Viewport
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _viewport; }
        }

        /// <summary>
        /// Gets access to the vertical scrollbar view.
        /// </summary>
        public ViewDrawScrollBar ScrollbarV
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _scrollbarV; }
        }

        /// <summary>
        /// Gets access to the horizontl scrollbar view.
        /// </summary>
        public ViewDrawScrollBar ScrollbarH
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _scrollbarH; }
        }

        /// <summary>
        /// Gets access to the vertical border edge view.
        /// </summary>
        public ViewDrawBorderEdge BorderEdgeV
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _borderEdgeV; }
        }

        /// <summary>
        /// Gets access to the horizontal border edge view.
        /// </summary>
        public ViewDrawBorderEdge BorderEdgeH
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _borderEdgeH; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Ask the base docker element to perform a layout.
        /// </summary>
        protected void DockerLayout(ViewLayoutContext context)
        {
            // Get base class to perform actual layout
            base.Layout(context);
        }

        /// <summary>
        /// Requests a paint and optional layout of the control.
        /// </summary>
        /// <param name="needLayout">Is a layout required.</param>
        protected void NeedPaint(bool needLayout)
        {
            // Request a layout be performed immediately
            if (_needPaintDelegate != null)
                _needPaintDelegate(this, new NeedLayoutEventArgs(needLayout));
        }
        #endregion

        #region Implementation
        private VisualOrientation ViewportOrientation(bool vertical)
        {
            if (vertical)
                return VisualOrientation.Left;
            else
                return VisualOrientation.Top;
        }

        private void OnScrollVChanged(object sender, EventArgs e)
        {
            // Update viewport with the new scroll offset
            Viewport.SetOffsetV(ScrollbarV.ScrollPosition);

            // Must generate another layout
            if (_needPaintDelegate != null)
            {
                // Request a layout be performed immediately
                NeedPaint(true);

                // Make sure the child control is redraw to keep in sync with new scroll position
                ViewControl.ChildControl.Refresh();
            }
        }

        private void OnScrollHChanged(object sender, EventArgs e)
        {
            // Update viewport with the new scroll offset
            Viewport.SetOffsetH(ScrollbarH.ScrollPosition);

            // Must generate another layout
            if (_needPaintDelegate != null)
            {
                // Request a layout be performed immediately
                NeedPaint(true);

                // Make sure the child control is redraw to keep in sync with new scroll position
                ViewControl.ChildControl.Refresh();
            }
        }

        private void OnAnimateStep(object sender, EventArgs e)
        {
            if (AnimateStep != null)
                AnimateStep(sender, e);
        }
        #endregion
    }
}
