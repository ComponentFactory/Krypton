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
    /// View element that draws a scrollbar.
    /// </summary>
    public class ViewDrawScrollBar : ViewLeaf
    {
        #region Instance Fields
        private ScrollBar _scrollBar;
        private bool _vertical;
        private bool _removing;
        private bool _shortSize;
        private int _min;
        private int _max;
        private int _largeChange;
        private int _smallChange;
        private int _offset;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the scroll position has changed.
        /// </summary>
        public event EventHandler ScrollChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawScrollBar class.
        /// </summary>
        /// <param name="vertical">Is this a vertical scrollbar.</param>
        public ViewDrawScrollBar(bool vertical)
        {
            // Remember the vertical/horizontal setting
            _vertical = vertical;

            //Default other properties
            _removing = false;
            _shortSize = false;
            _min = 0;
            _max = 100;
            _largeChange = 20;
            _smallChange = 1;
            _offset = 0;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawScrollBar:" + Id;
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
                // Must remove the scroll bar resource
                if (_scrollBar != null)
                {
                    // Should never be called from garbage collector
                    Debug.Assert(!_scrollBar.InvokeRequired);

                    // Cannot destroy control in a different thread
                    if (!_scrollBar.InvokeRequired)
                        RemoveScrollBar();
                }
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Vertical
        /// <summary>
        /// Gets and sets a value indicating if the scrollbar is vertical.
        /// </summary>
        public bool Vertical
        {
            get { return _vertical; }

            set
            {
                // Only interested in changes
                if (_vertical != value)
                {
                    // Remember new orientation
                    _vertical = value;

                    // Remove the scrollbar as a child control
                    RemoveScrollBar();
                }
            }
        }
        #endregion

        #region ShortSize
        /// <summary>
        /// Gets and sets a value indicating if the scroll should short size.
        /// </summary>
        public bool ShortSize
        {
            get { return _shortSize; }
            set { _shortSize = value; }
        }
        #endregion

        #region SetScrollValues
        /// <summary>
        /// Update the scrollbar with the latest scrolling values.
        /// </summary>
        /// <param name="min">Scroll minimum value.</param>
        /// <param name="max">Scroll maximum value.</param>
        /// <param name="smallChange">Size of a small change.</param>
        /// <param name="largeChange">Size of a large change.</param>
        /// <param name="offset">Current scrolling offset.</param>
        public void SetScrollValues(int min, 
                                    int max, 
                                    int smallChange,
                                    int largeChange,
                                    int offset)
        {
            // Update cached values (applying limit checking)
            _min = Math.Max(min, 0);
            _max = Math.Max(max, 0);
            _smallChange = Math.Max(smallChange, 0);
            _largeChange = Math.Max(largeChange, 0);
            _offset = Math.Max(offset, 0);

            if (_scrollBar != null)
            {
                // Set the scrolling values
                _scrollBar.Minimum = _min;
                _scrollBar.Maximum = _max;
                _scrollBar.SmallChange = _smallChange;
                _scrollBar.LargeChange = _largeChange;
                _scrollBar.Value = Math.Max(_min, Math.Min(_max, _offset));
            }
        }
        #endregion

        #region ScrollPosition
        /// <summary>
        /// Gets the current scroll position.
        /// </summary>
        public int ScrollPosition
        {
            get
            {
                if (_scrollBar != null)
                    return _scrollBar.Value;
                else
                    return 0;
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
            Debug.Assert(context != null);

            // The minimum size is the minimum system size of a scrollbar
            return new Size(SystemInformation.VerticalScrollBarWidth,
                            SystemInformation.HorizontalScrollBarHeight);
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Prevent recreate of the control
            if (!IsDisposed && !_removing)
            {
                // We take on all the available display area
                ClientRectangle = context.DisplayRectangle;

                // Are we allowed to layout child controls?
                if (!context.ViewManager.DoNotLayoutControls)
                {
                    // Make sure the scrollbar has actually been created
                    CreateScrollBar(context.Control);

                    // If we need to hide/disable the control then do it before position changes
                    if (!Visible) _scrollBar.Hide();
                    if (!Enabled) _scrollBar.Enabled = false;

                    // Should the scrollbar is shorter than then the entire client area?
                    if (ShortSize)
                    {
                        if (Vertical)
                            _scrollBar.SetBounds(ClientLocation.X, ClientLocation.Y,
                                                 ClientWidth, ClientHeight - SystemInformation.HorizontalScrollBarHeight);
                        else
                            _scrollBar.SetBounds(ClientLocation.X, ClientLocation.Y,
                                                 ClientWidth - SystemInformation.VerticalScrollBarWidth, ClientHeight);
                    }
                    else
                    {
                        // Position the ScrollBar in the entire requested area
                        _scrollBar.SetBounds(ClientLocation.X, ClientLocation.Y,
                                             ClientWidth, ClientHeight);
                    }

                    // If we need to show/enable control then do it after position changes
                    if (Visible) _scrollBar.Show();
                    if (Enabled) _scrollBar.Enabled = true;
                }
            }
        }
        #endregion

        #region Implementation
        private void CreateScrollBar(Control parent)
        {
            // Do we need to create a scrollbar?
            if (_scrollBar == null)
            {
                // Create the correct type of control
                if (Vertical)
                    _scrollBar = new VScrollBar();
                else
                    _scrollBar = new HScrollBar();

                // Hook into scroll position changes
                _scrollBar.Scroll += new ScrollEventHandler(OnScrollBarChange);

                // Create it hidden
                _scrollBar.Hide();
                
                // Set the scrolling values
                _scrollBar.Minimum = _min;
                _scrollBar.Maximum = _max;
                _scrollBar.SmallChange = _smallChange;
                _scrollBar.LargeChange = _largeChange;
                _scrollBar.Value = _offset;

                // Add our new control to the provided parent collection
                CommonHelper.AddControlToParent(parent, _scrollBar);
            }
        }

        private void RemoveScrollBar()
        {
            // Do we need to remove the scrollbar?
            if ((_scrollBar != null) && !_removing)
            {
                // Prevent recreate of the control during removal, as removing it
                // will cause more layout cycles to occur. We put it back to false
                // at the end of the remove process.
                _removing = true;

                // Unhook from events
                _scrollBar.Scroll -= new ScrollEventHandler(OnScrollBarChange);

                // Hide the scrollbar from view
                _scrollBar.Hide();

                // Remove scrollbar from containing collection
                CommonHelper.RemoveControlFromParent(_scrollBar);

                // Destroy the current scrollbar
                _scrollBar.Dispose();
                _scrollBar = null;

                _removing = false;
            }
        }

        private void OnScrollBarChange(object sender, ScrollEventArgs e)
        {
            // Update with the new scroll value
            _scrollBar.Value = e.NewValue;

            if (ScrollChanged != null)
                ScrollChanged(this, EventArgs.Empty);
        }
        #endregion
    }
}
