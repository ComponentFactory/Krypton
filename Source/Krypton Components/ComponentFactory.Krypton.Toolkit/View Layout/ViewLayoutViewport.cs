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
	/// View element that allows scrolling around a contained view element.
	/// </summary>
	public class ViewLayoutViewport : ViewComposite
    {
        #region Static Fields
        private const float _scrollPercentage = 0.66f;
        private const int _scrollMinimum = 3;
        private const int _scrollOvers = 15;
        private const int _animationInterval = 22;
        private const int _animationMinimum = 8;
        #endregion

        #region Instance Fields
        private Timer _animationTimer;
        private IPaletteMetric _paletteMetrics;
        private PaletteMetricPadding _metricPadding;
        private PaletteMetricInt _metricOvers;
        private VisualOrientation _orientation;
        private RelativePositionAlign _alignment;
        private RelativePositionAlign _counterAlignment;
        private RightToLeft _rightToLeft;
        private bool _rightToLeftLayout;
        private bool _fillSpace;
        private bool _animateChange;
        private Point _offset;
        private Point _limit;
        private Size _extent;
        private Point _animationOffset;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when animation has moved another step.
        /// </summary>
        public event EventHandler AnimateStep;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutViewport class.
		/// </summary>
        /// <param name="paletteMetrics">Palette source for metrics.</param>
        /// <param name="metricPadding">Metric used to get view padding.</param>
        /// <param name="metricOvers">Metric used to get overposition.</param>
        /// <param name="orientation">Orientation for the viewport children.</param>
        /// <param name="alignment">Alignment of the children within the viewport.</param>
        /// <param name="animateChange">Animate changes in the viewport.</param>
        public ViewLayoutViewport(IPaletteMetric paletteMetrics,
                                  PaletteMetricPadding metricPadding,
                                  PaletteMetricInt metricOvers,
                                  VisualOrientation orientation,
                                  RelativePositionAlign alignment,
                                  bool animateChange)
		{
            // Remember the source information
            _paletteMetrics = paletteMetrics;
            _metricPadding = metricPadding;
            _metricOvers = metricOvers;
            _orientation = orientation;
            _alignment = alignment;
            _animateChange = animateChange;

            // Default other state
            _offset = Point.Empty;
            _extent = Size.Empty;
            _rightToLeft = RightToLeft.No;
            _rightToLeftLayout = false;
            _fillSpace = false;
            _counterAlignment = RelativePositionAlign.Far;

            // Create a timer for animation effect
            _animationTimer = new Timer();
            _animationTimer.Interval = _animationInterval;
            _animationTimer.Tick += new EventHandler(OnAnimationTick);
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Stop and dispose the timer
                _animationTimer.Stop();
                _animationTimer.Dispose();
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
            return "ViewLayoutViewport:" + Id;
		}
		#endregion

        #region SetMetrics
        /// <summary>
        /// Updates the metrics source and metric to use.
        /// </summary>
        /// <param name="paletteMetric">Source for aquiring metrics.</param>
        public void SetMetrics(IPaletteMetric paletteMetric)
        {
            _paletteMetrics = paletteMetric;
        }

        /// <summary>
        /// Updates the metrics source and metric to use.
        /// </summary>
        /// <param name="paletteMetric">Source for aquiring metrics.</param>
        /// <param name="metricPadding">Actual padding metric to use.</param>
        /// <param name="metricOvers">Actual overs metric to use.</param>
        public void SetMetrics(IPaletteMetric paletteMetric,
                               PaletteMetricPadding metricPadding,
                               PaletteMetricInt metricOvers)
        {
            _paletteMetrics = paletteMetric;
            _metricPadding = metricPadding;
            _metricOvers = metricOvers;
        }
        #endregion

        #region AnimateChange
        /// <summary>
        /// Gets and sets the use of animation for offset changes.
        /// </summary>
        public bool AnimateChange
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _animateChange; }
            set { _animateChange = value; }
        }
        #endregion

        #region Orientation
        /// <summary>
        /// Gets and sets the bar orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _orientation; }
            set { _orientation = value; }
        }
        #endregion

        #region Alignment
        /// <summary>
        /// Gets and sets the alignment of the children within the viewport.
        /// </summary>
        public RelativePositionAlign Alignment
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _alignment; }
            set { _alignment = value; }
        }
        #endregion

        #region CounterAlignment
        /// <summary>
        /// Gets and sets the counter alignment of the children within the viewport.
        /// </summary>
        public RelativePositionAlign CounterAlignment
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _counterAlignment; }
            set { _counterAlignment = value; }
        }
        #endregion

        #region FillSpace
        /// <summary>
        /// Gets and sets a value indicating if children should be made bigger to fill any left over space.
        /// </summary>
        public bool FillSpace
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _fillSpace; }
            set { _fillSpace = value; }
        }
        #endregion

        #region Offset
        /// <summary>
        /// Gets a scrolling offset within the viewport.
        /// </summary>
        public Point Offset
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _offset; }
            set { _offset = value; }
        }
        #endregion

        #region CanScrollV
        /// <summary>
        /// Gets a value indicating if the viewport can be scrolled vertically.
        /// </summary>
        public bool CanScrollV
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return (_limit.Y != 0); }
        }
        #endregion

        #region CanScrollH
        /// <summary>
        /// Gets a value indicating if the viewport can be scrolled horizontally.
        /// </summary>
        public bool CanScrollH
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return (_limit.X != 0); }
        }
        #endregion

        #region ScrollExtent
        /// <summary>
        /// Gets the total extent of the scrolling view.
        /// </summary>
        public Size ScrollExtent
        {
            get
            {
                return new Size(Math.Abs(_extent.Width),
                                Math.Abs(_extent.Height));
            }
        }
        #endregion

        #region ScrollOffset
        /// <summary>
        /// Gets a scrolling offset within the viewport.
        /// </summary>
        public Point ScrollOffset
        {
            get
            {
                return new Point(Math.Abs(_offset.X),
                                 Math.Abs(_offset.Y));
            }
        }
        #endregion

        #region SetOffsetV
        /// <summary>
        /// Update the vertical scrolling offset.
        /// </summary>
        /// <param name="offset">New offset to use.</param>
        public void SetOffsetV(int offset)
        {
            switch (AlignmentRTL)
            {
                case RelativePositionAlign.Near:
                case RelativePositionAlign.Center:
                    _offset.Y = -offset;
                    break;
                case RelativePositionAlign.Far:
                    _offset.Y = offset;
                    break;
            }
        }
        #endregion

        #region SetOffsetH
        /// <summary>
        /// Update the horizontal scrolling offset.
        /// </summary>
        /// <param name="offset">New offset to use.</param>
        public void SetOffsetH(int offset)
        {
            switch (AlignmentRTL)
            {
                case RelativePositionAlign.Near:
                case RelativePositionAlign.Center:
                    _offset.X = -offset;
                    break;
                case RelativePositionAlign.Far:
                    _offset.X = offset;
                    break;
            }
        }
        #endregion

        #region CanScrollNext
        /// <summary>
        /// Gets a value indicating if the viewport can be scrolled next.
        /// </summary>
        public bool CanScrollNext
        {
            get 
            {
                int limit = 0;
                int offset = 0;

                // Orientation determines which axis of values to use
                switch (Orientation)
                {
                    case VisualOrientation.Top:
                    case VisualOrientation.Bottom:
                        limit = _limit.X;
                        offset = _offset.X;
                        break;
                    case VisualOrientation.Left:
                    case VisualOrientation.Right:
                        limit = _limit.Y;
                        offset = _offset.Y;
                        break;
                }

                switch (AlignmentRTL)
                {
                    case RelativePositionAlign.Near:
                    case RelativePositionAlign.Center:
                        return (offset > limit);
                    case RelativePositionAlign.Far:
                        return (offset < 0);
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        return false;
                }
            }
        }
        #endregion

        #region CanScrollPrevious
        /// <summary>
        /// Gets a value indicating if the viewport can be scrolled previous.
        /// </summary>
        public bool CanScrollPrevious
        {
            get
            {
                int limit = 0;
                int offset = 0;

                // Orientation determines which axis of values to use
                switch (Orientation)
                {
                    case VisualOrientation.Top:
                    case VisualOrientation.Bottom:
                        limit = _limit.X;
                        offset = _offset.X;
                        break;
                    case VisualOrientation.Left:
                    case VisualOrientation.Right:
                        limit = _limit.Y;
                        offset = _offset.Y;
                        break;
                }

                switch (AlignmentRTL)
                {
                    case RelativePositionAlign.Near:
                    case RelativePositionAlign.Center:
                        return (offset < 0);
                    case RelativePositionAlign.Far:
                        return (offset > limit);
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        return false;
                }
            }
        }
        #endregion

        #region MoveNext
        /// <summary>
        /// Move the viewport to show the next part of area.
        /// </summary>
        public void MoveNext()
        {
            MoveDirection(true);
        }
        #endregion

        #region MovePrevious
        /// <summary>
        /// Move the viewport to show the previous part of area.
        /// </summary>
        public void MovePrevious()
        {
            MoveDirection(false);
        }
        #endregion

        #region NeedScrolling
        /// <summary>
        /// Is scrolling required because the viewport extents beyong available space.
        /// </summary>
        public bool NeedScrolling
        {
            get { return CanScrollNext || CanScrollPrevious; }
        }
        #endregion

        #region BringIntoView
        /// <summary>
        /// Move viewport to display the requested part of area.
        /// </summary>
        /// <param name="rect">Rectangle to display.</param>
        public void BringIntoView(Rectangle rect)
        {
            // Find the offset to bring the child rect into view
            Point offset = OffsetForChildRect(rect);

            // If not allowed to animate, or there is no animation needed
            if (!_animateChange || _offset.Equals(offset))
            {
                // Stop any running animation
                _animationTimer.Stop();

                // Use the new offset immediately
                _offset = offset;
            }
            else
            {
                // Use a timer to scroll to new offset
                _animationTimer.Stop();
                _animationOffset = offset;
                _animationTimer.Start();
            }
        }
        #endregion

        #region Layout
        /// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Remember the original display rect for restoring late
            Rectangle originalRect = context.DisplayRectangle;

            // Reduce available display rect by the required border sizing
            if (_paletteMetrics != null)
                context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation, originalRect, _paletteMetrics.GetMetricPadding(State, _metricPadding));

            // Cache the maximum extent of all the children
            _extent = base.GetPreferredSize(context);

            // Do we have a metric source for additional padding?
            if (_paletteMetrics == null)
                return _extent;

            // Restore the original display rectangle
            context.DisplayRectangle = originalRect;

            // Apply padding needed outside the border of the canvas
            return CommonHelper.ApplyPadding(Orientation, _extent, _paletteMetrics.GetMetricPadding(State, _metricPadding));
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

            // Cache the right to left setting at layout time
            _rightToLeft = context.Control.RightToLeft;
            _rightToLeftLayout = CommonHelper.GetRightToLeftLayout(context.Control);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Available space for positioning starts with entire client area
            Rectangle positionRectangle = ClientRectangle;

            // Do we have a metric source for additional padding?
            if (_paletteMetrics != null)
            {
                // Get the padding to be applied
                Padding outerPadding = _paletteMetrics.GetMetricPadding(State, _metricPadding);

                // Reduce space for children by the padding area
                positionRectangle = ApplyPadding(positionRectangle, outerPadding);
            }

            // Do we need to fill any remainder space?
            if (FillSpace)
            {
                // Ensure the extent reflects the maximum size we want, the whole area
                if (_extent.Width < positionRectangle.Width)    
                    _extent.Width = positionRectangle.Width;

                if (_extent.Height < positionRectangle.Height)  
                    _extent.Height = positionRectangle.Height;
            }

            // Find the limits allowed for the offset given current extent and display rect
            _limit = new Point(Math.Min(positionRectangle.Width - _extent.Width, 0),
                               Math.Min(positionRectangle.Height - _extent.Height, 0));

            // Enforce the offset back to the limits
            if (_offset.X < _limit.X) _offset.X = _limit.X;
            if (_offset.Y < _limit.Y) _offset.Y = _limit.Y;

            // Calculate the offset given the current alignment and counter alignment
            Point childOffset;
            int childOffsetX;
            int childOffsetY;

            // Find the final child offset
            if (Horizontal)
            {
                childOffsetX = CalculateAlignedOffset(AlignmentRTL, positionRectangle.X, positionRectangle.Width, _offset.X, _extent.Width, _limit.X);
                childOffsetY = CalculateAlignedOffset(CounterAlignmentRTL, positionRectangle.Y, positionRectangle.Height, _offset.Y, _extent.Height, _limit.Y);
            }
            else
            {
                childOffsetX = CalculateAlignedOffset(CounterAlignmentRTL, positionRectangle.X, positionRectangle.Width, _offset.X, _extent.Width, _limit.X);
                childOffsetY = CalculateAlignedOffset(AlignmentRTL, positionRectangle.Y, positionRectangle.Height, _offset.Y, _extent.Height, _limit.Y);
            }

            childOffset = new Point(childOffsetX, childOffsetY);

            // Ask each child to layout in turn
            foreach (ViewBase child in this)
            {
                // Only layout visible children
                if (child.Visible)
                {
                    // Give the child the available positioning size
                    context.DisplayRectangle = positionRectangle;

                    // Ask the child how much space they would like
                    Size childSize = child.GetPreferredSize(context);

                    // Do we need to fill any remainder space?
                    if (FillSpace)
                    {
                        // Ensure the child reflect the size we want, the whole area
                        if (childSize.Width < positionRectangle.Width)      
                            childSize.Width = positionRectangle.Width;

                        if (childSize.Height < positionRectangle.Height)    
                            childSize.Height = positionRectangle.Height;
                    }

                    // Give the child all the space it requires
                    context.DisplayRectangle = new Rectangle(childOffset, childSize);

                    // Let the child layout using the provided space
                    child.Layout(context);
                }
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform a render of the elements.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void Render(RenderContext context)
        {
            // Clipping area starts as the whole client area
            Rectangle clipRectangle = ClientRectangle;

            // Do we have a metric source for additional padding?
            if (_paletteMetrics != null)
            {
                // Get the padding to be applied
                Padding outerPadding = _paletteMetrics.GetMetricPadding(State, _metricPadding);

                // Reduce the clipping area by the padding
                clipRectangle = ApplyPadding(clipRectangle, outerPadding);
            }

            // New clipping region is at most our own client size
            using (Region combineRegion = new Region(clipRectangle))
            {
                // Remember the current clipping region
                Region clipRegion = context.Graphics.Clip.Clone();

                // Reduce clipping region down by the existing clipping region
                combineRegion.Intersect(clipRegion);

                // Use new region that restricts drawing to our client size only
                context.Graphics.Clip = combineRegion;

                // Perform rendering before any children
                RenderBefore(context);

                // Ask each child to render in turn
                foreach (ViewBase child in this)
                {
                    // Only render visible children that are inside the clipping rectangle
                    if (child.Visible)
                        child.Render(context);
                }

                // Perform rendering after that of children
                RenderAfter(context);

                // Put clipping region back to original setting
                context.Graphics.Clip = clipRegion;
            }
        }
        #endregion

        #region Implementation
        private bool Horizontal
        {
            get
            {
                return ((Orientation == VisualOrientation.Top) ||
                        (Orientation == VisualOrientation.Bottom));
            }
        }

        private RelativePositionAlign AlignmentRTL
        {
            get 
            {
                // Do we need to reverse the alignment or account for right to left?
                if (Horizontal && (_rightToLeft == RightToLeft.Yes))
                {
                    switch (Alignment)
                    {
                        case RelativePositionAlign.Near:
                            return RelativePositionAlign.Far;
                        case RelativePositionAlign.Far:
                            return RelativePositionAlign.Near;
                    }
                }

                return Alignment;
            }
        }

        private RelativePositionAlign CounterAlignmentRTL
        {
            get 
            {
                // Do we need to reverse the alignment or account for right to left?
                if (!Horizontal && (_rightToLeft == RightToLeft.Yes) && _rightToLeftLayout)
                {
                    switch (CounterAlignment)
                    {
                        case RelativePositionAlign.Near:
                            return RelativePositionAlign.Far;
                        case RelativePositionAlign.Far:
                            return RelativePositionAlign.Near;
                    }
                }

                return CounterAlignment;
            }
        }

        private int CalculateAlignedOffset(RelativePositionAlign alignment,
                                           int posRect,
                                           int posRectLength,
                                           int offset,
                                           int extent,
                                           int limit)
        {
            switch (alignment)
            {
                case RelativePositionAlign.Near:
                    // Position at the near side of the viewport
                    return posRect + offset;
                case RelativePositionAlign.Center:
                    // If there is no need for any scrolling then center, otherwise place near
                    if (limit == 0)
                        return posRect + (posRectLength - extent) / 2;
                    else
                        return posRect + offset;
                case RelativePositionAlign.Far:
                    // Position against the far side
                    return posRect + posRectLength - extent - offset;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return 0;
            }
        }

        private Rectangle ApplyPadding(Rectangle rect, Padding padding)
        {
            // Ignore an empty padding value
            if (!padding.Equals(CommonHelper.InheritPadding))
            {
                // Get the orientation to use for applying the padding
                VisualOrientation orientation = Orientation;

                // Do we need to apply right to left?
                if (_rightToLeftLayout && (_rightToLeft == RightToLeft.Yes))
                {
                    // Reverse the left and right only
                    switch (orientation)
                    {
                        case VisualOrientation.Left:
                            orientation = VisualOrientation.Right;
                            break;
                        case VisualOrientation.Right:
                            orientation = VisualOrientation.Left;
                            break;
                    }
                }

                // The orientation determines how the border padding is 
                // used to reduce the space available for children
                switch (orientation)
                {
                    case VisualOrientation.Top:
                        rect = new Rectangle(rect.X + padding.Left, rect.Y + padding.Top,
                                             rect.Width - padding.Horizontal, rect.Height - padding.Vertical);
                        break;
                    case VisualOrientation.Bottom:
                        rect = new Rectangle(rect.X + padding.Left, rect.Y + padding.Bottom,
                                             rect.Width - padding.Horizontal, rect.Height - padding.Vertical);
                        break;
                    case VisualOrientation.Left:
                        rect = new Rectangle(rect.X + padding.Top, rect.Y + padding.Left,
                                             rect.Width - padding.Vertical, rect.Height - padding.Horizontal);
                        break;
                    case VisualOrientation.Right:
                        rect = new Rectangle(rect.X + padding.Bottom, rect.Y + padding.Left,
                                             rect.Width - padding.Vertical, rect.Height - padding.Horizontal);
                        break;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        break;
                }
            }

            return rect;
        }

        private void MoveDirection(bool next)
        {
            // Begin by using the current offset
            Point offset = _offset;

            // Are we scrolling in the horizontal?
            if ((Orientation == VisualOrientation.Top) ||
                (Orientation == VisualOrientation.Bottom))
            {
                // Find the distance to move horizontally
                int change = Math.Max((int)(ClientSize.Width * _scrollPercentage), _scrollMinimum);

                switch (AlignmentRTL)
                {
                    case RelativePositionAlign.Near:
                    case RelativePositionAlign.Center:
                        if (next)
                            offset.X -= change;
                        else
                            offset.X += change;
                        break;
                    case RelativePositionAlign.Far:
                        if (next)
                            offset.X += change;
                        else
                            offset.X -= change;
                        break;
                }
            }
            else
            {
                // Find the distance to move vertically
                int change = Math.Max((int)(ClientSize.Height * _scrollPercentage), _scrollMinimum);

                switch (AlignmentRTL)
                {
                    case RelativePositionAlign.Near:
                    case RelativePositionAlign.Center:
                        if (next)
                            offset.Y -= change;
                        else
                            offset.Y += change;
                        break;
                    case RelativePositionAlign.Far:
                        if (next)
                            offset.Y += change;
                        else
                            offset.Y -= change;
                        break;
                }
            }

            // Enforce the offset back to the limits
            offset.X = Math.Min(Math.Max(offset.X, _limit.X), 0);
            offset.Y = Math.Min(Math.Max(offset.Y, _limit.Y), 0);

            // If not allowed to animate, or there is no animation needed
            if (!_animateChange || _offset.Equals(offset))
            {
                // Use the new offset
                _offset = offset;
            }
            else
            {
                // Use a timer to scroll to new offset
                _animationTimer.Stop();
                _animationOffset = offset;
                _animationTimer.Start();
            }
        }
        
        private Point OffsetForChildRect(Rectangle rect)
        {
            // Begin by using the current offset
            Point offset = _offset;

            // Find how far to over position the viewport
            int overs = 0;
            
            // We might not be provided with metrics, so only use if reference provided
            if (_paletteMetrics != null)
                overs = _paletteMetrics.GetMetricInt(State, _metricOvers) + _scrollOvers;

            // Move the required rectangle more than exactly into view in order to make it
            // easier for users to see extra pages before and after it for easy selection
            if ((Orientation == VisualOrientation.Top) ||
                (Orientation == VisualOrientation.Bottom))
            {
                rect.X -= overs;
                rect.Width += overs * 2;
            }
            else
            {
                rect.Y -= overs;
                rect.Height += overs * 2;
            }

            //If all the children are completely visible then nothing to do
            if ((_limit.X != 0) || (_limit.Y != 0))
            {
                // Is part of the required rectangle not currently visible
                if (!ClientRectangle.Contains(rect))
                {
                    // Correct the alignmnet to take right to left into account
                    RelativePositionAlign alignment = AlignmentRTL;

                    // Center alignment needs changing to near or far for scrolling
                    if (alignment == RelativePositionAlign.Center)
                        alignment = RelativePositionAlign.Near;

                    // How to scroll into view depends on the alignmnent of items
                    switch (alignment)
                    {
                        case RelativePositionAlign.Near:
                            if (rect.Right > ClientRectangle.Right)     offset.X += (ClientRectangle.Right - rect.Right);
                            if (rect.Left < ClientRectangle.Left)       offset.X += (ClientRectangle.Left - rect.Left);
                            if (rect.Bottom > ClientRectangle.Bottom)   offset.Y += (ClientRectangle.Bottom - rect.Bottom);
                            if (rect.Top < ClientRectangle.Top)         offset.Y += (ClientRectangle.Top - rect.Top);
                            break;
                        case RelativePositionAlign.Far:
                            if (rect.Right > ClientRectangle.Right)     offset.X -= (ClientRectangle.Right - rect.Right);
                            if (rect.Left < ClientRectangle.Left)       offset.X -= (ClientRectangle.Left - rect.Left);
                            if (rect.Bottom > ClientRectangle.Bottom)   offset.Y -= (ClientRectangle.Bottom - rect.Bottom);
                            if (rect.Top < ClientRectangle.Top)         offset.Y -= (ClientRectangle.Top - rect.Top);
                            break;
                    }
                }
            }

            // Enforce the offset back to the limits
            offset.X = Math.Min(Math.Max(offset.X, _limit.X), 0);
            offset.Y = Math.Min(Math.Max(offset.Y, _limit.Y), 0);

            return offset;
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            // Limit check the animation offset, incase the limits have changed
            _animationOffset.X = Math.Min(Math.Max(_animationOffset.X, _limit.X), 0);
            _animationOffset.Y = Math.Min(Math.Max(_animationOffset.Y, _limit.Y), 0);

            // Find distance half way to the destination
            int distanceX = (_animationOffset.X - _offset.X) / 2;
            int distanceY = (_animationOffset.Y - _offset.Y) / 2;

            // Enfore a minimum distance to move towards destination in order
            // to prevent small moves at the end of the animation duration
            if (_animationOffset.X < _offset.X)
            {
                distanceX = Math.Min(distanceX, -_animationMinimum);
                _offset.X = Math.Max(_animationOffset.X, _offset.X + distanceX);
            }
            else
            {
                distanceX = Math.Max(distanceX, _animationMinimum);
                _offset.X = Math.Min(_animationOffset.X, _offset.X + distanceX);
            }

            if (_animationOffset.Y < _offset.Y)
            {
                distanceY = Math.Min(distanceY, -_animationMinimum);
                _offset.Y = Math.Max(_animationOffset.Y, _offset.Y + distanceY);
            }
            else
            {
                distanceY = Math.Max(distanceY, _animationMinimum);
                _offset.Y = Math.Min(_animationOffset.Y, _offset.Y + distanceY);
            }

            // If new offset is same as the target
            if ((_offset.X == _animationOffset.X) &&
                (_offset.Y == _animationOffset.Y))
            {
                // Then all done, cancel the timer
                _animationTimer.Stop();
            }

            // Enfore limits against the offset
            _offset.X = Math.Min(Math.Max(_offset.X, _limit.X), 0);
            _offset.Y = Math.Min(Math.Max(_offset.Y, _limit.Y), 0);

            // Request the layout and paint to reflect change
            if (AnimateStep != null)
                AnimateStep.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
