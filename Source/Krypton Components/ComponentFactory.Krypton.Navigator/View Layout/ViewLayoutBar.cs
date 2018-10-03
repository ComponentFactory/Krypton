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
	/// View element that can size and position each page entry on the bar.
	/// </summary>
	internal class ViewLayoutBar : ViewComposite
    {
        #region LineDetails
        private struct LineDetails
        {
            public int InlineLength;
            public int CrossLength;
            public int StartIndex;
            public int ItemCount;

            public LineDetails(int inlineLength,
                               int crossLength,
                               int startIndex,
                               int itemCount)
            {
                InlineLength = inlineLength;
                CrossLength = crossLength;
                StartIndex = startIndex;
                ItemCount = itemCount;
            }
        }
        #endregion

        #region Instance Fields
        private TabBorderStyle _tabBorderStyle;
        private IPaletteMetric _paletteMetric;
        private PaletteMetricInt _metricGap;
        private VisualOrientation _orientation;
        private VisualOrientation _itemOrientation;
        private RelativePositionAlign _itemAlignment;
        private BarItemSizing _itemSizing;
        private List<LineDetails> _lineDetails;
        private Size[] _childSizes;
        private Size _itemMinimumSize;
        private Size _itemMaximumSize;
        private Size _maximumItem;
        private int _barMinimumHeight;
        private int _preferredOrientLength;
        private BarMultiline _barMultiline;
        private bool _reorderSelectedLine;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutBar class.
        /// </summary>
        /// <param name="itemSizing">Method used to calculate item size.</param>
        /// <param name="itemAlignment">Method used to align items within lines.</param>
        /// <param name="barMultiline">Multline showing of items.</param>
        /// <param name="itemMinimumSize">Maximum allowed item size.</param>
        /// <param name="itemMaximumSize">Minimum allowed item size.</param>
        /// <param name="barMinimumHeight">Minimum height of the bar.</param>
        /// <param name="tabBorderStyle">Tab border style.</param>
        /// <param name="reorderSelectedLine">Should line with selection be reordered.</param>
        public ViewLayoutBar(BarItemSizing itemSizing,
                             RelativePositionAlign itemAlignment,
                             BarMultiline barMultiline,
                             Size itemMinimumSize,
                             Size itemMaximumSize,
                             int barMinimumHeight,
                             TabBorderStyle tabBorderStyle,
                             bool reorderSelectedLine)
            : this(null, PaletteMetricInt.None, itemSizing, 
                   itemAlignment, barMultiline, itemMinimumSize, 
                   itemMaximumSize, barMinimumHeight, tabBorderStyle,
                   reorderSelectedLine)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ViewLayoutBar class.
        /// </summary>
        /// <param name="paletteMetric">Palette source for metric values.</param>
        /// <param name="metricGap">Metric for gap between each child item.</param>
        /// <param name="itemSizing">Method used to calculate item size.</param>
        /// <param name="itemAlignment">Method used to align items within lines.</param>
        /// <param name="barMultiline">Multline showing of items.</param>
        /// <param name="itemMinimumSize">Maximum allowed item size.</param>
        /// <param name="itemMaximumSize">Minimum allowed item size.</param>
        /// <param name="barMinimumHeight">Minimum height of the bar.</param>
        /// <param name="reorderSelectedLine">Should line with selection be reordered.</param>
        public ViewLayoutBar(IPaletteMetric paletteMetric,
                             PaletteMetricInt metricGap,
                             BarItemSizing itemSizing,
                             RelativePositionAlign itemAlignment,
                             BarMultiline barMultiline,
                             Size itemMinimumSize,
                             Size itemMaximumSize,
                             int barMinimumHeight,
                             bool reorderSelectedLine)
            : this(paletteMetric, metricGap, itemSizing, 
                   itemAlignment, barMultiline, itemMinimumSize, 
                   itemMaximumSize, barMinimumHeight, 
                   TabBorderStyle.RoundedOutsizeMedium,
                   reorderSelectedLine)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ViewLayoutBar class.
        /// </summary>
        /// <param name="paletteMetric">Palette source for metric values.</param>
        /// <param name="metricGap">Metric for gap between each child item.</param>
        /// <param name="itemSizing">Method used to calculate item size.</param>
        /// <param name="itemAlignment">Method used to align items within lines.</param>
        /// <param name="barMultiline">Multline showing of items.</param>
        /// <param name="itemMinimumSize">Maximum allowed item size.</param>
        /// <param name="itemMaximumSize">Minimum allowed item size.</param>
        /// <param name="barMinimumHeight">Minimum height of the bar.</param>
        /// <param name="tabBorderStyle">Tab border style.</param>
        /// <param name="reorderSelectedLine">Should line with selection be reordered.</param>
        public ViewLayoutBar(IPaletteMetric paletteMetric,
                             PaletteMetricInt metricGap,
                             BarItemSizing itemSizing,
                             RelativePositionAlign itemAlignment,
                             BarMultiline barMultiline,
                             Size itemMinimumSize,
                             Size itemMaximumSize,
                             int barMinimumHeight,
                             TabBorderStyle tabBorderStyle,
                             bool reorderSelectedLine)
        {
            // Remember the source information
            _paletteMetric = paletteMetric;
            _metricGap = metricGap;
            _itemSizing = itemSizing;
            _itemAlignment = itemAlignment;
            _itemMinimumSize = itemMinimumSize;
            _itemMaximumSize = itemMaximumSize;
            _barMinimumHeight = barMinimumHeight;
            _tabBorderStyle = tabBorderStyle;
            _barMultiline = barMultiline;
            _reorderSelectedLine = reorderSelectedLine;

            // Default other state
            _orientation = VisualOrientation.Top;
            _itemOrientation = VisualOrientation.Top;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutBar:" + Id;
        }
        #endregion

        #region ReorderSelectedLine
        /// <summary>
        /// Gets and sets the need to reorder the line with the selection.
        /// </summary>
        public bool ReorderSelectedLine
        {
            get { return _reorderSelectedLine; }
            set { _reorderSelectedLine = value; }
        }
        #endregion

        #region BarItemSizing
        /// <summary>
        /// Gets and sets the method used to size bar items.
        /// </summary>
        public BarItemSizing BarItemSizing
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _itemSizing; }
            set { _itemSizing = value; }
        }
        #endregion

        #region BarMinimumHeight
        /// <summary>
        /// Gets and sets the minimum height of the bar.
        /// </summary>
        public int BarMinimumHeight
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _barMinimumHeight; }
            set { _barMinimumHeight = value; }
        }
        #endregion

        #region ItemMinimumSize
        /// <summary>
        /// Gets and sets the minimum size of item allowed.
        /// </summary>
        public Size ItemMinimumSize
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _itemMinimumSize; }
            set { _itemMinimumSize = value; }
        }
        #endregion

        #region ItemMinimumSize
        /// <summary>
        /// Gets and sets the maximum size of item allowed.
        /// </summary>
        public Size ItemMaximumSize
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _itemMaximumSize; }
            set { _itemMaximumSize = value; }
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

        #region ItemOrientation
        /// <summary>
        /// Gets and sets the item orientation.
        /// </summary>
        public VisualOrientation ItemOrientation
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _itemOrientation; }
            set { _itemOrientation = value; }
        }
        #endregion

        #region ItemAlignment
        /// <summary>
        /// Gets and sets the item alignment.
        /// </summary>
        public RelativePositionAlign ItemAlignment
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _itemAlignment; }
            set { _itemAlignment = value; }
        }
        #endregion

        #region BarMultiline
        /// <summary>
        /// Gets and sets a value indicating if multiple lines are allowed.
        /// </summary>
        public BarMultiline BarMultiline
        {
            get { return _barMultiline; }
            set { _barMultiline = value; }
        }
        #endregion

        #region TabBorderStyle
        /// <summary>
        /// Gets and sets the tab border style to use when calculating item gaps.
        /// </summary>
        public TabBorderStyle TabBorderStyle
        {
            get { return _tabBorderStyle; }
            set { _tabBorderStyle = value; }
        }
        #endregion

        #region SetMetrics
        /// <summary>
        /// Updates the metrics source and metric to use.
        /// </summary>
        /// <param name="paletteMetric">Source for aquiring metrics.</param>
        public void SetMetrics(IPaletteMetric paletteMetric)
        {
            _paletteMetric = paletteMetric;
        }

        /// <summary>
        /// Updates the metrics source and metric to use.
        /// </summary>
        /// <param name="paletteMetric">Palette source for metric values.</param>
        /// <param name="metricGap">Metric for gap between each child item.</param>
        public void SetMetrics(IPaletteMetric paletteMetric,
                               PaletteMetricInt metricGap)
        {
            _paletteMetric = paletteMetric;
            _metricGap = metricGap;
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

            // Reset the largest child size to empty
            _maximumItem = Size.Empty;

            // Keep track of the total preferred size
            Size preferredSize = Size.Empty;

            // Nothing to calculate if there are no children
            if (Count > 0)
            {
                // Default to no space between each child item
                int gap = 0;

                // If we have a metric provider then get the child gap to use
                if (_paletteMetric != null)
                    gap = _paletteMetric.GetMetricInt(State, _metricGap);
                else
                    gap = context.Renderer.RenderTabBorder.GetTabBorderSpacingGap(_tabBorderStyle);

                // Line spacing gap can never be less than zero
                int lineGap = (gap < 0 ? 0 : gap);
                
                bool reversed = false;

                // Do we need to apply right to left by positioning children in reverse order?
                if (IsOneLine && !BarVertical && (context.Control.RightToLeft == RightToLeft.Yes))
                    reversed = true;

                // Allocate caching for size of each child element
                _childSizes = new Size[Count];

                // Find the child index of the selected page
                int selectedChildIndex = -1;

                // Find the size of each child in turn
                for(int i=0; i<Count; i++)
                {
                    // Get access to the indexed child
                    ViewBase child = this[reversed ? (Count - i - 1) : i];
                    INavCheckItem checkItem = (INavCheckItem)child;

                    // Only examine visible children
                    if (child.Visible)
                    {
                        // Cache child index of the selected page
                        if (checkItem.Navigator.SelectedPage == checkItem.Page)
                            selectedChildIndex = i;

                        // Ask child for it's own preferred size
                        _childSizes[i] = child.GetPreferredSize(context);

                        // Enfore the minimum and maximum sizes
                        if (ItemVertical)
                        {
                            _childSizes[i].Width = Math.Max(Math.Min(_childSizes[i].Width, _itemMaximumSize.Height), _itemMinimumSize.Height);
                            _childSizes[i].Height = Math.Max(Math.Min(_childSizes[i].Height, _itemMaximumSize.Width), _itemMinimumSize.Width);
                        }
                        else
                        {
                            _childSizes[i].Width = Math.Max(Math.Min(_childSizes[i].Width, _itemMaximumSize.Width), _itemMinimumSize.Width);
                            _childSizes[i].Height = Math.Max(Math.Min(_childSizes[i].Height, _itemMaximumSize.Height), _itemMinimumSize.Height);
                        }

                        // Remember the largest child encountered
                        _maximumItem.Width = Math.Max(_childSizes[i].Width, _maximumItem.Width);
                        _maximumItem.Height = Math.Max(_childSizes[i].Height, _maximumItem.Height);
                    }
                }

                // Apply the item sizing method
                switch (BarItemSizing)
                {
                    case BarItemSizing.Individual:
                        // Do nothing, each item can be its own size
                        break;
                    case BarItemSizing.SameHeight:
                        if (!BarVertical)
                        {
                            for (int i = 0; i < _childSizes.Length; i++)
                                if (!_childSizes[i].IsEmpty)
                                    _childSizes[i].Height = _maximumItem.Height;
                        }
                        else
                        {
                            for (int i = 0; i < _childSizes.Length; i++)
                                if (!_childSizes[i].IsEmpty)
                                    _childSizes[i].Width = _maximumItem.Width;
                        }
                        break;
                    case BarItemSizing.SameWidth:
                        if (!BarVertical)
                        {
                            for (int i = 0; i < _childSizes.Length; i++)
                                if (!_childSizes[i].IsEmpty)
                                    _childSizes[i].Width = _maximumItem.Width;
                        }
                        else
                        {
                            for (int i = 0; i < _childSizes.Length; i++)
                                if (!_childSizes[i].IsEmpty)
                                    _childSizes[i].Height = _maximumItem.Height;
                        }
                        break;
                    case BarItemSizing.SameWidthAndHeight:
                        for (int i = 0; i < _childSizes.Length; i++)
                            if (!_childSizes[i].IsEmpty)
                                _childSizes[i] = _maximumItem;
                        break;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        break;
                }

                // Store a list of the individual line vectors (height or width depending on orientation)
                _lineDetails = new List<LineDetails>();

                int itemCount = 0;
                int startIndex = 0;
                int visibleItems = 0;

                if (BarVertical)
                {
                    int yPos = 0;
                    int yMaxPos = 0;
                    int lineWidth = 0;
                    _preferredOrientLength = 0;

                    for (int i = 0; i < _childSizes.Length; i++)
                    {
                        // Ignore invisible items, which are zero sized
                        if (!_childSizes[i].IsEmpty)
                        {
                            // If not the first visible item on line, then need a spacing gap
                            int yAdd = (visibleItems > 0) ? gap : 0;

                            // Add on the heght of the child
                            yAdd += _childSizes[i].Height;

                            // Does this item extend beyond visible line? 
                            // (unless first item, we always have at least one item on a line)
                            if (!IsOneLine && (yPos > 0) && ((yPos + yAdd) > context.DisplayRectangle.Height))
                            {
                                // Remember the line metrics
                                _lineDetails.Add(new LineDetails(yPos, lineWidth, startIndex, itemCount));

                                // Track the widest line encountered
                                yMaxPos = Math.Max(yPos, yMaxPos);

                                // Reset back to start of the next line
                                yPos = 0;
                                itemCount = 0;
                                startIndex = i;
                                _preferredOrientLength += (lineGap + lineWidth);
                                lineWidth = 0;

                                // First item on new line does not need a spacing gap
                                yAdd = _childSizes[i].Height;
                            }

                            // Add on to the current line
                            yPos += yAdd;
                            visibleItems++;

                            // Track the tallest item on this line
                            if (lineWidth < _childSizes[i].Width)
                                lineWidth = _childSizes[i].Width;
                        }

                        // Visible and Invisible items are added to the item count
                        itemCount++;
                    }

                    // Add the last line to the height
                    _preferredOrientLength += lineWidth;

                    // Check if the last line is the tallest line
                    yMaxPos = Math.Max(yPos, yMaxPos);

                    // If we extended past end of the line
                    if (yMaxPos > context.DisplayRectangle.Height)
                    {
                        // If the mode requires we do not extend over the line
                        if ((BarMultiline == BarMultiline.Shrinkline) ||
                            (BarMultiline == BarMultiline.Exactline))
                        {
                            bool changed;

                            // Keep looping to reduce item sizes until all are zero sized or finished removing extra space
                            do
                            {
                                changed = false;

                                // Are there any items avaiable for reducing?
                                if (visibleItems > 0)
                                {
                                    // How much do we need to shrink each item by?
                                    int shrink = Math.Max(1, (yMaxPos - context.DisplayRectangle.Height) / visibleItems);

                                    // Reduce size of each item
                                    for (int i = 0; i < _childSizes.Length; i++)
                                    {
                                        // Cannot make smaller then zero height
                                        if (_childSizes[i].Height > 0)
                                        {
                                            // Reduce size
                                            int tempHeight = _childSizes[i].Height;
                                            _childSizes[i].Height -= shrink;

                                            // Prevent going smaller then zero
                                            if (_childSizes[i].Height <= 0)
                                            {
                                                _childSizes[i].Height = 0;
                                                visibleItems--;
                                            }

                                            // Reduce total width by the height removed from item
                                            yMaxPos -= (tempHeight - _childSizes[i].Height);

                                            // All reduction made, exit the loop
                                            if (yMaxPos <= context.DisplayRectangle.Height)
                                                break;

                                            changed = true;
                                        }
                                    }
                                }
                            } while (changed && (yMaxPos > context.DisplayRectangle.Height));
                        }
                    }

                    // If we are shorter than the available height
                    if (yMaxPos < context.DisplayRectangle.Height)
                    {
                        // If the mode requires we extend to the end of the line
                        if ((BarMultiline == BarMultiline.Expandline) ||
                            (BarMultiline == BarMultiline.Exactline))
                        {
                            bool changed;

                            // Keep looping to expand item sizes until all extra space is allocated
                            do
                            {
                                changed = false;

                                // Are there any items avaiable for expanding?
                                if (visibleItems > 0)
                                {
                                    // How much do we need to expand each item by?
                                    int expand = Math.Max(1, (context.DisplayRectangle.Height - yMaxPos) / visibleItems);

                                    // Expand size of each item
                                    for (int i = 0; i < _childSizes.Length; i++)
                                    {
                                        // Expand size
                                        _childSizes[i].Height += expand;

                                        // Reduce free space by that allocated
                                        yMaxPos += expand;

                                        changed = true;

                                        // All expansion made, exit the loop
                                        if (yMaxPos >= context.DisplayRectangle.Height)
                                            break;
                                    }
                                }
                            } while (changed && (yMaxPos < context.DisplayRectangle.Height));
                        }
                    }

                    // Remember the line metrics
                    _lineDetails.Add(new LineDetails(yMaxPos, lineWidth, startIndex, itemCount));

                    // Our preferred size is tall enough to show the longest line and total width
                    preferredSize.Width = _preferredOrientLength;
                    preferredSize.Height = yMaxPos;
                }
                else
                {
                    int xPos = 0;
                    int xMaxPos = 0;
                    int lineHeight = 0;
                    _preferredOrientLength = 0;

                    for (int i = 0; i < _childSizes.Length; i++)
                    {
                        // Ignore invisible items, which are zero sized
                        if (!_childSizes[i].IsEmpty)
                        {
                            // If not the first item on line, then need a spacing gap
                            int xAdd = (visibleItems > 0) ? gap : 0;

                            // Add on the width of the child
                            xAdd += _childSizes[i].Width;

                            // Does this item extend beyond visible line? 
                            // (unless first item, we always have at least one item on a line)
                            if (!IsOneLine && (xPos > 0) && ((xPos + xAdd) > context.DisplayRectangle.Width))
                            {
                                // Remember the line metrics
                                _lineDetails.Add(new LineDetails(xPos, lineHeight, startIndex, itemCount));

                                // Track the widest line encountered
                                xMaxPos = Math.Max(xPos, xMaxPos);

                                // Reset back to start of the next line
                                xPos = 0;
                                itemCount = 0;
                                startIndex = i;
                                _preferredOrientLength += (lineGap + lineHeight);
                                lineHeight = 0;

                                // First item on new line does not need a spacing gap
                                xAdd = _childSizes[i].Width;
                            }

                            // Add on to the current line
                            xPos += xAdd;
                            visibleItems++;

                            // Track the tallest item on this line
                            if (lineHeight < _childSizes[i].Height)
                                lineHeight = _childSizes[i].Height;
                        }

                        // Visible and Invisible items are added to the item count
                        itemCount++;
                    }

                    // Add the last line to the height
                    _preferredOrientLength += lineHeight;

                    // Check if the last line is the widest line
                    xMaxPos = Math.Max(xPos, xMaxPos);

                    // If we extended past end of the line
                    if (xMaxPos > context.DisplayRectangle.Width)
                    {
                        // If the mode requires we do not extend over the line
                        if ((BarMultiline == BarMultiline.Shrinkline) ||
                            (BarMultiline == BarMultiline.Exactline))
                        {
                            bool changed;

                            // Keep looping to reduce item sizes until all are zero sized or finished removing extra space
                            do
                            {
                                changed = false;

                                // Are there any items avaiable for reducing?
                                if (visibleItems > 0)
                                {
                                    // How much do we need to shrink each item by?
                                    int shrink = Math.Max(1, (xMaxPos - context.DisplayRectangle.Width) / visibleItems);

                                    // Reduce size of each item
                                    for (int i = 0; i < _childSizes.Length; i++)
                                    {
                                        // Cannot make smaller then zero width
                                        if (_childSizes[i].Width > 0)
                                        {
                                            // Reduce size
                                            int tempWidth = _childSizes[i].Width;
                                            _childSizes[i].Width -= shrink;

                                            // Prevent going smaller then zero
                                            if (_childSizes[i].Width <= 0)
                                            {
                                                _childSizes[i].Width = 0;
                                                visibleItems--;
                                            }

                                            // Reduce total width by the width removed from item
                                            xMaxPos -= (tempWidth - _childSizes[i].Width);

                                            // All reduction made, exit the loop
                                            if (xMaxPos <= context.DisplayRectangle.Width)
                                                break;

                                            changed = true;
                                        }
                                    }
                                }
                            } while (changed && (xMaxPos > context.DisplayRectangle.Width));
                        }
                    }

                    // If we are shorter than the line width
                    if (xMaxPos < context.DisplayRectangle.Width)
                    {
                        // If the mode requires we extend to the end of the line
                        if ((BarMultiline == BarMultiline.Expandline) ||
                            (BarMultiline == BarMultiline.Exactline))
                        {
                            bool changed;

                            // Keep looping to expand item sizes until all the extra space is removed
                            do
                            {
                                changed = false;

                                // Are there any items avaiable for reducing?
                                if (visibleItems > 0)
                                {
                                    // How much do we need to expand each item by?
                                    int expand = Math.Max(1, (context.DisplayRectangle.Width - xMaxPos) / visibleItems);

                                    // Expand size of each item
                                    for (int i = 0; i < _childSizes.Length; i++)
                                    {
                                        // Expand size
                                        _childSizes[i].Width += expand;

                                        // Increase total width taken up by items
                                        xMaxPos += expand;

                                        changed = true;

                                        // All expansion made, exit the loop
                                        if (xMaxPos >= context.DisplayRectangle.Width)
                                            break;
                                    }
                                }
                            } while (changed && (xMaxPos < context.DisplayRectangle.Width));
                        }
                    }

                    // Remember the line metrics
                    _lineDetails.Add(new LineDetails(xMaxPos, lineHeight, startIndex, itemCount));

                    // Our preferred size is tall enough to show the widest line and total height
                    preferredSize.Width = xMaxPos;
                    preferredSize.Height = _preferredOrientLength;
                }

                // Reverse the order of the lines when at top or left edge, as the 
                // items should be positioned from the inside edge moving outwards
                if ((Orientation == VisualOrientation.Top) ||
                    (Orientation == VisualOrientation.Left))
                {
                    _lineDetails.Reverse();
                }

                // If we are using tabs then we need to move the line with the selection
                if (ReorderSelectedLine)
                {
                    // Did we find a selected child index?
                    if (selectedChildIndex >= 0)
                    {
                        // Find the line details that contains this child index
                        for(int i=0; i<_lineDetails.Count; i++)
                        {
                            // Is the selected item in the range of items for this line?
                            if ((selectedChildIndex >= _lineDetails[i].StartIndex) && 
                                (selectedChildIndex < (_lineDetails[i].StartIndex + _lineDetails[i].ItemCount)))
                            {
                                // Remove the line details
                                LineDetails ld = _lineDetails[i];
                                _lineDetails.RemoveAt(i);

                                if ((Orientation == VisualOrientation.Top) ||
                                    (Orientation == VisualOrientation.Left))
                                {
                                    // Move to end of the list
                                    _lineDetails.Add(ld);
                                }
                                else
                                {
                                    // Move to start of the list
                                    _lineDetails.Insert(0, ld);
                                }
                            }
                        }
                    }
                }
            }

            // Enfore the minimum height of the bar
            if (BarVertical)
                preferredSize.Width = Math.Max(preferredSize.Width, _barMinimumHeight);
            else
                preferredSize.Height = Math.Max(preferredSize.Height, _barMinimumHeight);

            return preferredSize;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Start laying out children from the top left
            Point pt = ClientLocation;

            // Nothing to calculate if there are no children
            if (Count > 0)
            {
                // Default to no space between each child item
                int gap = 0;

                // If we have a metric provider then get the child gap to use
                if (_paletteMetric != null)
                    gap = _paletteMetric.GetMetricInt(State, _metricGap);
                else
                    gap = context.Renderer.RenderTabBorder.GetTabBorderSpacingGap(_tabBorderStyle);

                // Line spacing gap can never be less than zero
                int lineGap = (gap < 0 ? 0 : gap);

                bool reverseAccess = false;
                bool reversePosition = false;

                // Do we need to apply right to left by positioning children in reverse order?
                if (!BarVertical && (context.Control.RightToLeft == RightToLeft.Yes))
                {
                    if (IsOneLine)
                        reverseAccess = true;
                    else
                        reversePosition = true;
                }

                if (BarVertical)
                {
                    int xPos;

                    // Ensure the left orientation is aligned towards right of bar area
                    if (Orientation == VisualOrientation.Left)
                        xPos = ClientLocation.X + Math.Max(0, ClientWidth - _preferredOrientLength);
                    else
                        xPos = ClientLocation.X;

                    // Layout each line of buttons in turn
                    foreach (LineDetails lineDetails in _lineDetails)
                    {
                        // Get starting position for first button on the line
                        int yPos = FindStartingYPosition(context, lineDetails, reversePosition);

                        // Layout each button on the line
                        for (int i = 0; i < lineDetails.ItemCount; i++)
                        {
                            // Get the actual child index to use
                            int itemIndex = lineDetails.StartIndex + i;

                            // Ignore invisible items, which are zero sized
                            if (!_childSizes[itemIndex].IsEmpty)
                            {
                                // Get access to the indexed child
                                ViewBase child = this[(reverseAccess ? lineDetails.StartIndex + lineDetails.ItemCount - 1 - i :
                                                                       lineDetails.StartIndex + i)];

                                // Add on the height of the child
                                int yAdd = _childSizes[itemIndex].Height;

                                int xPosition = xPos;
                                int yPosition = (reversePosition ? yPos - _childSizes[itemIndex].Height : yPos);

                                // At the left edge, we need to ensure buttons are align by there right edges
                                if (Orientation == VisualOrientation.Left)
                                    xPosition = xPos + lineDetails.CrossLength - _childSizes[itemIndex].Width;

                                // Create the rectangle that shows all of the check button
                                context.DisplayRectangle = new Rectangle(new Point(xPosition, yPosition), _childSizes[itemIndex]);

                                // Ask the child to layout
                                child.Layout(context);

                                // Move to next child position
                                if (reversePosition)
                                    yPos -= (yAdd + gap);
                                else
                                    yPos += (yAdd + gap);
                            }
                        }

                        // Move across to the next line
                        xPos += (lineGap + lineDetails.CrossLength);
                    }
                }
                else
                {
                    int yPos;

                    // Ensure the top orientation is aligned towards bottom of bar area
                    if (Orientation == VisualOrientation.Top)
                        yPos = ClientLocation.Y + Math.Max(0, ClientHeight - _preferredOrientLength);
                    else
                        yPos = ClientLocation.Y;

                    // Layout each line of buttons in turn
                    foreach (LineDetails lineDetails in _lineDetails)
                    {
                        // Get starting position for first button on the line
                        int xPos = FindStartingXPosition(context, lineDetails, reversePosition);

                        // Layout each button on the line
                        for (int i = 0; i < lineDetails.ItemCount; i++)
                        {
                            // Get the actual child index to use
                            int itemIndex = lineDetails.StartIndex + i;

                            // Ignore invisible items, which are zero sized
                            if (!_childSizes[itemIndex].IsEmpty)
                            {
                                // Get access to the indexed child
                                ViewBase child = this[(reverseAccess ? lineDetails.StartIndex + lineDetails.ItemCount - 1 - i :
                                                                       lineDetails.StartIndex + i)];

                                // Add on the width of the child
                                int xAdd = _childSizes[itemIndex].Width;

                                int yPosition = yPos;
                                int xPosition = (reversePosition ? xPos - _childSizes[itemIndex].Width : xPos);

                                // At the top edge, we need to ensure buttons are align by there bottom edges
                                if (Orientation == VisualOrientation.Top)
                                    yPosition = yPos + lineDetails.CrossLength - _childSizes[itemIndex].Height;

                                // Create the rectangle that shows all of the check button
                                context.DisplayRectangle = new Rectangle(new Point(xPosition, yPosition), _childSizes[itemIndex]);

                                // Ask the child to layout
                                child.Layout(context);

                                // Move to next child position
                                if (reversePosition)
                                    xPos -= (xAdd + gap);
                                else
                                    xPos += (xAdd + gap);
                            }
                        }

                        // Move down to the next line
                        yPos += (lineGap + lineDetails.CrossLength);
                    }
                }
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion

        #region Implementation
        private bool BarVertical
        {
            get
            {
                return ((Orientation == VisualOrientation.Left) ||
                        (Orientation == VisualOrientation.Right));
            }
        }

        private bool ItemVertical
        {
            get
            {
                return ((ItemOrientation == VisualOrientation.Left) ||
                        (ItemOrientation == VisualOrientation.Right));
            }
        }

        private bool IsOneLine
        {
            get { return ((BarMultiline == BarMultiline.Singleline) ||
                          (BarMultiline == BarMultiline.Shrinkline) ||
                          (BarMultiline == BarMultiline.Expandline) ||
                          (BarMultiline == BarMultiline.Exactline));
        }
        }

        private int FindStartingXPosition(ViewLayoutContext context, 
                                          LineDetails lineDetails,
                                          bool reversePosition)
        {
            RelativePositionAlign align = ItemAlignment;

            // Do we need to apply right to left by aligning in opposite direction?
            if (IsOneLine && !BarVertical && (context.Control.RightToLeft == RightToLeft.Yes))
            {
                if (align == RelativePositionAlign.Near)
                    align = RelativePositionAlign.Far;
                else if (align == RelativePositionAlign.Far)
                    align = RelativePositionAlign.Near;
            }

            switch (align)
            {
                case RelativePositionAlign.Near:
                    if (reversePosition)
                        return ClientRectangle.Right;
                    else
                        return ClientLocation.X;
                case RelativePositionAlign.Center:
                    if (reversePosition)
                        return ClientRectangle.Right - (ClientRectangle.Width - lineDetails.InlineLength) / 2;
                    else
                        return ClientLocation.X + (ClientRectangle.Width - lineDetails.InlineLength) / 2;
                case RelativePositionAlign.Far:
                    if (reversePosition)
                        return ClientRectangle.Right - (ClientRectangle.Width - lineDetails.InlineLength);
                    else
                        return ClientLocation.X + (ClientRectangle.Width - lineDetails.InlineLength);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return ClientLocation.X;
            }
        }

        private int FindStartingYPosition(ViewLayoutContext context,
                                          LineDetails lineDetails,
                                          bool reversePosition)
        {
            RelativePositionAlign align = ItemAlignment;

            // Do we need to apply right to left by aligning in opposite direction?
            if (IsOneLine && !BarVertical && (context.Control.RightToLeft == RightToLeft.Yes))
            {
                if (align == RelativePositionAlign.Near)
                    align = RelativePositionAlign.Far;
                else if (align == RelativePositionAlign.Far)
                    align = RelativePositionAlign.Near;
            }

            switch (align)
            {
                case RelativePositionAlign.Near:
                    if (reversePosition)
                        return ClientRectangle.Bottom;
                    else
                        return ClientLocation.Y;
                case RelativePositionAlign.Center:
                    if (reversePosition)
                        return ClientRectangle.Bottom - (ClientRectangle.Height - lineDetails.InlineLength) / 2;
                    else
                        return ClientLocation.Y + (ClientRectangle.Height - lineDetails.InlineLength) / 2;
                case RelativePositionAlign.Far:
                    if (reversePosition)
                        return ClientRectangle.Bottom - (ClientRectangle.Height - lineDetails.InlineLength);
                    else
                        return ClientLocation.Y + (ClientRectangle.Height - lineDetails.InlineLength);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return ClientLocation.Y;
            }
        }
        #endregion
    }
}
