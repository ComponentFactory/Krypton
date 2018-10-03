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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Aggregates information needed for rendering drag and drop indicators.
    /// </summary>
    public class RenderDragDockingData
    {
        #region Instance Fields
        private int _showTotal;
        private BoolFlags31 _flags;
        private Rectangle[] _rects;
        private Size _windowSize;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteDragData class.
        /// </summary>
        /// <param name="showLeft">Should the left docking indicator be shown.</param>
        /// <param name="showRight">Should the right docking indicator be shown.</param>
        /// <param name="showTop">Should the top docking indicator be shown.</param>
        /// <param name="showBottom">Should the bottom docking indicator be shown.</param>
        /// <param name="showMiddle">Should the middle docking indicator be shown.</param>
        public RenderDragDockingData(bool showLeft, bool showRight, 
                                     bool showTop, bool showBottom, 
                                     bool showMiddle)
        {
            _flags = new BoolFlags31();

            // Set initial settings (ShowBack is auto calculated from other flags)
            ShowLeft = showLeft;
            ShowRight = showRight;
            ShowTop = showTop;
            ShowBottom = showBottom;
            ShowMiddle = showMiddle;

            // Default valies
            _windowSize = Size.Empty;
            _rects = new Rectangle[5];
            for (int i = 0; i < _rects.Length; i++)
                _rects[i] = Rectangle.Empty;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the visible state of the background.
        /// </summary>
        public bool ShowBack
        {
            get { return (_showTotal > 1); }
        }

        /// <summary>
        /// Gets and sets the visible state of the left indicator.
        /// </summary>
        public bool ShowLeft
        {
            get { return _flags.AreFlagsSet(0x0002); }
            set { UpdateShowFlag(value, 0x0002); }
        }

        /// <summary>
        /// Gets and sets the visible state of the right indicator.
        /// </summary>
        public bool ShowRight
        {
            get { return _flags.AreFlagsSet(0x0004); }
            set { UpdateShowFlag(value, 0x0004); }
        }

        /// <summary>
        /// Gets and sets the visible state of the top indicator.
        /// </summary>
        public bool ShowTop
        {
            get { return _flags.AreFlagsSet(0x0008); }
            set { UpdateShowFlag(value, 0x0008); }
        }

        /// <summary>
        /// Gets and sets the visible state of the bottom indicator.
        /// </summary>
        public bool ShowBottom
        {
            get { return _flags.AreFlagsSet(0x0010); }
            set { UpdateShowFlag(value, 0x0010); }
        }

        /// <summary>
        /// Gets and sets the visible state of the middle indicator.
        /// </summary>
        public bool ShowMiddle
        {
            get { return _flags.AreFlagsSet(0x0020); }
            set { UpdateShowFlag(value, 0x0020); }
        }

        /// <summary>
        /// Gets the set of flags associated with active
        /// </summary>
        public int ActiveFlags
        {
            get { return _flags.Flags & 0x07C0; }
        }

        /// <summary>
        /// Gets and sets the active state of left indicator.
        /// </summary>
        public bool ActiveLeft
        {
            get { return _flags.AreFlagsSet(0x0040); }
            set { UpdateFlag(value, 0x0040); }
        }

        /// <summary>
        /// Gets and sets the active state of right indicator.
        /// </summary>
        public bool ActiveRight
        {
            get { return _flags.AreFlagsSet(0x0080); }
            set { UpdateFlag(value, 0x0080); }
        }

        /// <summary>
        /// Gets and sets the active state of top indicator.
        /// </summary>
        public bool ActiveTop
        {
            get { return _flags.AreFlagsSet(0x0100); }
            set { UpdateFlag(value, 0x0100); }
        }

        /// <summary>
        /// Gets and sets the active state of bottom indicator.
        /// </summary>
        public bool ActiveBottom
        {
            get { return _flags.AreFlagsSet(0x0200); }
            set { UpdateFlag(value, 0x0200); }
        }

        /// <summary>
        /// Gets and sets the active state of middle indicator.
        /// </summary>
        public bool ActiveMiddle
        {
            get { return _flags.AreFlagsSet(0x0400); }
            set { UpdateFlag(value, 0x0400); }
        }

        /// <summary>
        /// Gets if any of the docking indicators are active.
        /// </summary>
        public bool AnyActive
        {
            get { return (ActiveFlags != 0); }
        }

        /// <summary>
        /// Clear all the active flags.
        /// </summary>
        public void ClearActive()
        {
            _flags.ClearFlags(0x07C0);
        }

        /// <summary>
        /// Gets and sets the hot rectangle of the left docking indicator.
        /// </summary>
        public Rectangle RectLeft
        {
            get { return _rects[0]; }
            set { _rects[0] = value; }
        }

        /// <summary>
        /// Gets and sets the hot rectangle of the right docking indicator.
        /// </summary>
        public Rectangle RectRight
        {
            get { return _rects[1]; }
            set { _rects[1] = value; }
        }

        /// <summary>
        /// Gets and sets the hot rectangle of the top docking indicator.
        /// </summary>
        public Rectangle RectTop
        {
            get { return _rects[2]; }
            set { _rects[2] = value; }
        }

        /// <summary>
        /// Gets and sets the hot rectangle of the bottom docking indicator.
        /// </summary>
        public Rectangle RectBottom
        {
            get { return _rects[3]; }
            set { _rects[3] = value; }
        }

        /// <summary>
        /// Gets and sets the hot rectangle of the middle docking indicator.
        /// </summary>
        public Rectangle RectMiddle
        {
            get { return _rects[4]; }
            set { _rects[4] = value; }
        }

        /// <summary>
        /// Gets and sets size of the docking window required.
        /// </summary>
        public Size DockWindowSize
        {
            get { return _windowSize; }
            set { _windowSize = value; }
        }
        #endregion

        #region Implementation
        private void UpdateFlag(bool value, int flag)
        {
            if (value)
                _flags.SetFlags(flag);
            else
                _flags.ClearFlags(flag);
        }

        private void UpdateShowFlag(bool value, int flag)
        {
            if (value != _flags.AreFlagsSet(flag))
            {
                if (value)
                {
                    _flags.SetFlags(flag);
                    _showTotal++;
                }
                else
                {
                    _flags.ClearFlags(flag);
                    _showTotal--;
                }
            }
        }
        #endregion
    }
}
