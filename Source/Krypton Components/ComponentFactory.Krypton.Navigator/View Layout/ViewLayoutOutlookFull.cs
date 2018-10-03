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

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// View element that knows how to hide and show stacked items depending on available space.
    /// </summary>
    internal class ViewLayoutOutlookFull : ViewLayoutScrollViewport
    {
        #region Instance Fields
        private ViewBuilderOutlookBase _viewBuilder; 
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutOutlookFull class.
		/// </summary>
        /// <param name="viewBuilder">View builder reference.</param>
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
        public ViewLayoutOutlookFull(ViewBuilderOutlookBase viewBuilder,
                                     VisualControl rootControl,
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
            : base(rootControl, viewportFiller, paletteBorderEdge, paletteMetrics, 
                   metricPadding, metricOvers, orientation, alignment, animateChange, 
                   vertical, needPaintDelegate)
        {
            Debug.Assert(viewBuilder != null);
            _viewBuilder = viewBuilder;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutOutlookFull:" + Id;
        }
        #endregion

        #region ViewBuilder
        /// <summary>
        /// Gets access to the associated view builder.
        /// </summary>
        public ViewBuilderOutlookBase ViewBuilder
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _viewBuilder; }
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

            // Get the the visible state before processing
            string beforeOverflowState = _viewBuilder.GetOverflowButtonStates();

            // Make all stacking items visible so all that can be shown will be
            ViewBuilder.UnshrinkAppropriatePages();

            // Do not actually change the layout of any child controls
            context.ViewManager.DoNotLayoutControls = true;

            do
            {
                // Do we need to layout again?
                relayout = false;

                // Always reinstate the cached offset, so that if one of the cycles
                // limits the offset to a different value then subsequent cycles
                // will not remember that artificial limitation
                Viewport.Offset = originalOffset;

                // Make sure the viewport has extents calculated
                Viewport.GetPreferredSize(context);

                // Let base class perform a layout calculation
                DockerLayout(context);

                // Find the latest scrolling requirement
                canScrollV = Viewport.CanScrollV;
                canScrollH = Viewport.CanScrollH;

                // If we need to use vertical scrolling...
                if (canScrollV)
                {
                    // Ask the view builder to try and hide stacking items to free up some vertical
                    // space. We provide the amount of space required to remove the vertical scroll
                    // bar from view, so only the minimum number of stacking items are removed.
                    relayout = ViewBuilder.ShrinkVertical(Viewport.ScrollExtent.Height - Viewport.ClientSize.Height);
                }

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

            // If visible state of an overflow button has changed, need to relayout
            if (!beforeOverflowState.Equals(_viewBuilder.GetOverflowButtonStates()))
                NeedPaint(true);
        }
        #endregion
    }
}
