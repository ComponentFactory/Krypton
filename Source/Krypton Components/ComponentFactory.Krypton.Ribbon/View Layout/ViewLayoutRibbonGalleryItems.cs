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
	/// <summary>
	/// View element that creates and lays out the gallery items.
	/// </summary>
    internal class ViewLayoutRibbonGalleryItems : ViewComposite
    {
        #region Static Fields
        private static readonly int SCROLL_MOVE = 10;
        #endregion

        #region Instance Fields
        private ViewDrawRibbonGalleryButton _buttonUp;
        private ViewDrawRibbonGalleryButton _buttonDown;
        private ViewDrawRibbonGalleryButton _buttonContext;
        private NeedPaintHandler _needPaint;
        private PaletteTripleToPalette _triple;
        private KryptonGallery _gallery;
        private ButtonStyle _style;
        private Timer _scrollTimer;
        private Size _itemSize;
        private int _lineItems;
        private int _displayLines;
        private int _layoutLines;
        private int _topLine;
        private int _endLine;
        private int _offset;
        private int _beginLine;
        private int _bringIntoView;
        private bool _scrollIntoView;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonGalleryItems class.
        /// </summary>
        /// <param name="palette">Reference to palette for display values.</param>
        /// <param name="gallery">Reference to owning gallery.</param>
        /// <param name="needPaint">Delegate for requesting paints.</param>
        /// <param name="buttonUp">Reference to the up button.</param>
        /// <param name="buttonDown">Reference to the down button.</param>
        /// <param name="buttonContext">Reference to the context button.</param>
        public ViewLayoutRibbonGalleryItems(IPalette palette,
                                            KryptonGallery gallery,
                                            NeedPaintHandler needPaint,
                                            ViewDrawRibbonGalleryButton buttonUp,
                                            ViewDrawRibbonGalleryButton buttonDown,
                                            ViewDrawRibbonGalleryButton buttonContext)
        {
            Debug.Assert(palette != null);
            Debug.Assert(gallery != null);
            Debug.Assert(needPaint != null);
            Debug.Assert(buttonUp != null);
            Debug.Assert(buttonDown != null);
            Debug.Assert(buttonContext != null);

            _gallery = gallery;
            _needPaint = needPaint;
            _buttonUp = buttonUp;
            _buttonDown = buttonDown;
            _buttonContext = buttonContext;
            _bringIntoView = -1;
            _scrollIntoView = true;

            // Need to know when any button is clicked
            _buttonUp.Click += new MouseEventHandler(OnButtonUp);
            _buttonDown.Click += new MouseEventHandler(OnButtonDown);
            _buttonContext.Click += new MouseEventHandler(OnButtonContext);

            // Create triple that can be used by the draw button
            _style = ButtonStyle.LowProfile;
            _triple = new PaletteTripleToPalette(palette,
                                                 PaletteBackStyle.ButtonLowProfile,
                                                 PaletteBorderStyle.ButtonLowProfile,
                                                 PaletteContentStyle.ButtonLowProfile);

            // Setup timer to use for scrolling lines
            _scrollTimer = new Timer();
            _scrollTimer.Interval = 40;
            _scrollTimer.Tick += new EventHandler(OnScrollTick);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutRibbonGalleryItems:" + Id;
		}
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the scrolling into view setting.
        /// </summary>
        public bool ScrollIntoView
        {
            get { return _scrollIntoView; }
            set { _scrollIntoView = value; }
        }

        /// <summary>
        /// Gets the number of items currently displayed on a line.
        /// </summary>
        public int ActualLineItems
        {
            get { return Math.Max(1, _lineItems); }
        }

        /// <summary>
        /// Move tracking to the first item.
        /// </summary>
        public void TrackMoveHome()
        {
            if (Count > 0)
                _gallery.SetTrackingIndex(0, true);
        }

        /// <summary>
        /// Move tracking to the last item.
        /// </summary>
        public void TrackMoveEnd()
        {
            if (Count > 0)
                _gallery.SetTrackingIndex(Count - 1, true);
        }

        /// <summary>
        /// Move tracking upwards by a whole page.
        /// </summary>
        public void TrackMovePageUp()
        {
            if (Count > 0)
            {
                // Move previously by the number of display items
                int trackingIndex = _gallery.TrackingIndex;
                trackingIndex -= (_displayLines * _lineItems);

                // Limit check and use new index
                trackingIndex = Math.Max(0, trackingIndex);
                _gallery.SetTrackingIndex(trackingIndex, true);
            }
        }

        /// <summary>
        /// Move tracking downwards by a whole page.
        /// </summary>
        public void TrackMovePageDown()
        {
            if (Count > 0)
            {
                // Move next by the number of display items
                int trackingIndex = _gallery.TrackingIndex;
                trackingIndex += (_displayLines * _lineItems);

                // Limit check and use new index
                trackingIndex = Math.Min(trackingIndex, Count - 1);
                _gallery.SetTrackingIndex(trackingIndex, true);
            }
        }

        /// <summary>
        /// Move tracking up one line.
        /// </summary>
        public void TrackMoveUp()
        {
            if (Count > 0)
            {
                // Can only move up if not on the top line of items
                int trackingIndex = _gallery.TrackingIndex;
                if (trackingIndex >= _lineItems)
                {
                    // Move up a whole line of items
                    trackingIndex -= _lineItems;

                    // Limit check and use new index
                    trackingIndex = Math.Max(0, trackingIndex);
                    _gallery.SetTrackingIndex(trackingIndex, true);
                }
            }
        }

        /// <summary>
        /// Move tracking down one line.
        /// </summary>
        public void TrackMoveDown()
        {
            if (Count > 0)
            {
                if ((_gallery.TrackingIndex + _lineItems) < Count)
                {
                    // Move down a whole line of items
                    int trackingIndex = _gallery.TrackingIndex;
                    trackingIndex += _lineItems;

                    // Limit check and use new index
                    trackingIndex = Math.Min(trackingIndex, Count - 1);
                    _gallery.SetTrackingIndex(trackingIndex, true);
                }
            }
        }

        /// <summary>
        /// Move tracking down left one item.
        /// </summary>
        public void TrackMoveLeft()
        {
            if (Count > 0)
            {
                // Are there more items on the left of the current line
                int trackingIndex = _gallery.TrackingIndex;
                if ((trackingIndex % _lineItems) > 0)
                {
                    trackingIndex--;

                    // Limit check and use new index
                    trackingIndex = Math.Max(0, trackingIndex);
                    _gallery.SetTrackingIndex(trackingIndex, true);
                }
            }
        }

        /// <summary>
        /// Move tracking down right one item.
        /// </summary>
        public void TrackMoveRight()
        {
            if (Count > 0)
            {
                // Are there more items on the right of the current line
                int trackingIndex = _gallery.TrackingIndex;
                if ((trackingIndex % _lineItems) < (_lineItems - 1))
                {
                    trackingIndex++;

                    // Limit check and use new index
                    trackingIndex = Math.Min(trackingIndex, Count - 1);
                    _gallery.SetTrackingIndex(trackingIndex, true);
                }
            }
        }

        /// <summary>
        /// Is there a next line that can be displayed.
        /// </summary>
        public bool CanNextLine
        {
            get { return (_topLine < _endLine); }
        }

        /// <summary>
        /// Is there a previous line that can be displayed.
        /// </summary>
        public bool CanPrevLine
        {
            get { return (_topLine > 0); }
        }

        /// <summary>
        /// Scroll to make the next line visible.
        /// </summary>
        public void NextLine()
        {
            // New top line is one further down
            int prevTopLine = _topLine;
            _topLine = Math.Min(_topLine + 1, _endLine);

            if (ScrollIntoView)
            {
                // Offset backwards so previous top line is starting position
                _offset -= _itemSize.Height;

                // If offset is still negative then need to check the begin line
                if (_offset < 0)
                {
                    // Ensure the old top line can be displayed during scrolling
                    if ((_beginLine == -1) || (_beginLine > prevTopLine))
                        _beginLine = prevTopLine;
                }

                // Start the scrolling
                _scrollTimer.Start();
            }
        }

        /// <summary>
        /// Scroll to make the previous line visible.
        /// </summary>
        public void PrevLine()
        {
            // New top line is one further up
            int prevTopLine = _topLine;
            _topLine = Math.Max(_topLine - 1, 0);

            if (ScrollIntoView)
            {
                // Offset forwards so previous top line is starting position
                _offset += _itemSize.Height;

                // If offset is still positive then need to check the begin line
                if (_offset > 0)
                {
                    // Ensure the old top line can be displayed during scrolling
                    if ((_beginLine == -1) || (_beginLine < prevTopLine))
                        _beginLine = prevTopLine;
                }

                // Start the scrolling
                _scrollTimer.Start();
            }
        }

        /// <summary>
        /// Gets and sets the button style used for each image item.
        /// </summary>
        public ButtonStyle ButtonStyle
        {
            get { return _style; }

            set
            {
                if (_style != value)
                {
                    _style = value;
                    _triple.SetStyles(_style);
                    _needPaint(this, new NeedLayoutEventArgs(true));
                }
            }
        }

        /// <summary>
        /// Bring the specified image index into view.
        /// </summary>
        /// <param name="index">Index to bring into view.</param>
        public void BringIntoView(int index)
        {
            _bringIntoView = index;
            _gallery.PerformNeedPaint(true);
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

            // Ensure that the correct number of children are created
            SyncChildren();

            Size preferredSize = Size.Empty;

            // Find size of the first item, if there is one
            if (Count > 0)
            {
                // Ask child for it's own preferred size
                preferredSize = this[0].GetPreferredSize(context);

                // Find preferred size from the preferred item size
                preferredSize.Width *= _gallery.PreferredItemSize.Width;
                preferredSize.Height *= _gallery.PreferredItemSize.Height;
            }

            // Add on the requests padding
            preferredSize.Width += _gallery.Padding.Horizontal;
            preferredSize.Height += _gallery.Padding.Vertical;

            return preferredSize;
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
            
            // We take on all the available display area
			ClientRectangle = context.DisplayRectangle;

            // Ensure that the correct number of children are created
            SyncChildren();

            // Is there anything to layout?
            if (Count > 0)
            {
                // Reduce the client area by the requested padding before the internal children
                Rectangle displayRect = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _gallery.Padding);

                // Get size of the first child, assume all others are same size
                _itemSize = this[0].GetPreferredSize(context);

                // Number of items that can be placed on a single line
                _lineItems = Math.Max(1, displayRect.Width / _itemSize.Width);

                // Number of lines needed to show all the items
                _layoutLines = Math.Max(1, (Count + _lineItems - 1) / _lineItems);

                // Number of display lines that can be shown at a time
                _displayLines = Math.Max(1, Math.Min(_layoutLines, displayRect.Height / _itemSize.Height));

                // Index of last line that can be the top line
                _endLine = _layoutLines - _displayLines;

                // Update topline and offset to reflect any outstanding bring into view request
                ProcessBringIntoView();

                // Limit check the top line is within the valid range
                _topLine = Math.Max(0, Math.Min(_topLine, _endLine));

                // Update the enabled state of the buttons
                _buttonUp.Enabled = _gallery.Enabled && CanPrevLine;
                _buttonDown.Enabled = _gallery.Enabled && CanNextLine;
                _buttonContext.Enabled = _gallery.Enabled && (Count > 0);

                // Calculate position of first item as the left edge but starting downwards
                // and equal amount of the spare space after drawing the display lines.
                Point nextPoint = displayRect.Location;
                nextPoint.Y += (displayRect.Height - (_displayLines * _itemSize.Height)) / 2;

                // Stating item is from the top line and last item is number of display items onwards
                int start = (_topLine * _lineItems);
                int end = start + (_displayLines * _lineItems);

                // Do we need to handle scroll offsetting?
                int offset = _offset;
                if (offset != 0)
                {
                    if (offset < 0)
                    {
                        // How many extra full lines needed by the scrolling
                        int extraLines = _topLine - _beginLine;

                        // Limit check the number of previous lines to show
                        if (_topLine - extraLines < 0)
                            extraLines = _topLine;

                        // Move start to ensure that the previous lines are visible
                        start -= (extraLines * _lineItems);

                        // Adjust offset to reflect change in start
                        offset += (extraLines * _itemSize.Height);
                    }
                    else
                    {
                        // How many extra full lines needed by the scrolling
                        int extraLines = _beginLine - _topLine;

                        // Move start to ensure that the previous lines are visible
                        end += (extraLines * _lineItems);

                        // Limit check the end item to stop it overflowing number of items
                        if (end > Count)
                            end = Count;
                    }
                }

                // Add scrolling offset
                nextPoint.Y -= offset;

                // Position all children on single line from left to right
                for (int i = 0; i < Count; i++)
                {
                    ViewBase childItem = this[i];

                    // Should this item be visible
                    if ((i < start) || (i >= end))
                        childItem.Visible = false;
                    else
                    {
                        childItem.Visible = true;

                        // Find rectangle for the child
                        context.DisplayRectangle = new Rectangle(nextPoint, _itemSize);

                        // Layout the child
                        childItem.Layout(context);

                        // Move across to next position
                        nextPoint.X += _itemSize.Width;

                        // If there is not enough room for another item on this line
                        if ((nextPoint.X + _itemSize.Width) > displayRect.Right)
                        {
                            // Move down to next line
                            nextPoint.X = displayRect.X;
                            nextPoint.Y += _itemSize.Height;
                        }
                    }
                }
            }
            else
            {
                // No children means no items and so need for enabled buttons
                _buttonUp.Enabled = false;
                _buttonDown.Enabled = false;
                _buttonContext.Enabled = false;
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
		#endregion

        #region Private
        public void SyncChildren()
        {
            int required = 0;
            int selectedIndex = _gallery.SelectedIndex;
            ImageList imageList = _gallery.ImageList;

            // Find out how many children we need
            if (imageList != null)
                required = _gallery.ImageList.Images.Count;

            // If we do not have enough already
            if (Count < required)
            {
                // Create and add the number extra needed
                int create = required - Count;
                for (int i = 0; i < create; i++)
                    Add(new ViewDrawRibbonGalleryItem(_gallery, _triple, this, _needPaint));
            }
            else if (Count > required)
            {
                // Destroy the extra ones no longer needed
                int remove = Count - required;
                for (int i = 0; i < remove; i++)
                    RemoveAt(0);
            }

            // Tell each item the image it should be displaying
            for (int i = 0; i < required; i++)
            {
                ViewDrawRibbonGalleryItem item = (ViewDrawRibbonGalleryItem)this[i];
                item.ImageList = imageList;
                item.ImageIndex = i;
                item.Checked = (selectedIndex == i);
            }
        }

        private void OnButtonUp(object sender, MouseEventArgs e)
        {
            PrevLine();
            _gallery.PerformNeedPaint(true);
        }

        private void OnButtonDown(object sender, MouseEventArgs e)
        {
            NextLine();
            _gallery.PerformNeedPaint(true);
        }

        private void OnButtonContext(object sender, MouseEventArgs e)
        {
            _buttonContext.ForceLeave();
            _gallery.OnDropButton();
        }

        private void OnScrollTick(object sender, EventArgs e)
        {
            // Update the offset by scroll move amount
            if (_offset != 0)
            {
                if (_offset > 0)
                    _offset = Math.Max(0, _offset - SCROLL_MOVE);
                else
                    _offset = Math.Min(0, _offset + SCROLL_MOVE);
            }

            // If we have finished the scrolling
            if (_offset == 0)
            {
                _beginLine = -1;
                _scrollTimer.Stop();
            }

            // Need to repaint to show changes
            _needPaint(this, new NeedLayoutEventArgs(true));
        }

        private void ProcessBringIntoView()
        {
            // Do we need to process a 'BringIntoView' request?
            if (_bringIntoView >= 0)
            {
                // If there are any lines to actually work against
                if (_lineItems > 0)
                {
                    // Find target line for bringing into view
                    int line = _bringIntoView / _lineItems;
                    int itemLine = line;

                    // Limit check to the last line for display purposes
                    if (line > _endLine)
                        line = _endLine;

                    // Cache top line before any changes made to it
                    int prevTopLine = _topLine;

                    // Is that line before the current top line?
                    if (line < _topLine)
                    {
                        // How many lines do we need to scroll upwards
                        int diffLines = _topLine - line;

                        // Shift topline to target immediately
                        _topLine = line;

                        // If we are supposed to scroll to the target position
                        if (ScrollIntoView)
                        {
                            // Modify the offset to reflect change in number of lines
                            _offset += _itemSize.Height * diffLines;
                            _scrollTimer.Start();
                        }
                        else
                        {
                            _offset = 0;
                            _scrollTimer.Stop();
                        }
                    }
                    else if (itemLine >= (_topLine + _displayLines))
                    {
                        // How many lines do we need to scroll upwards
                        int diffLines = itemLine - (_topLine + (_displayLines - 1));

                        // Shift topline to target immediately
                        _topLine = itemLine - (_displayLines - 1);

                        if (ScrollIntoView)
                        {
                            // Modify the offset to reflect change in number of lines
                            _offset -= _itemSize.Height * diffLines;
                            _scrollTimer.Start();
                        }
                        else
                        {
                            _offset = 0;
                            _scrollTimer.Stop();
                        }
                    }

                    // Update the begin line
                    if (_offset < 0)
                    {
                        // Ensure the old top line can be displayed during scrolling
                        if ((_beginLine == -1) || (_beginLine > prevTopLine))
                            _beginLine = prevTopLine;
                    }
                    else if (_offset > 0)
                    {
                        // Ensure the old top line can be displayed during scrolling
                        if ((_beginLine == -1) || (_beginLine < prevTopLine))
                            _beginLine = prevTopLine;
                    }
                }

                // Reset the request
                _bringIntoView = -1;
            }
        }
        #endregion
    }
}
