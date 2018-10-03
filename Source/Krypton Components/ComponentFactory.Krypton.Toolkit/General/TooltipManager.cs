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
using System.ComponentModel;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Manages when tooltips are displayed in response to provided messages.
    /// </summary>
    public class ToolTipManager
    {
        #region Instance Fields
        private Timer _startTimer;
        private Timer _stopTimer;
        private ViewBase _startTarget;
        private ViewBase _currentTarget;
        private bool _showingToolTips;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a tooltip is required to be shown.
        /// </summary>
        public event EventHandler<ToolTipEventArgs> ShowToolTip;
        
        /// <summary>
        /// Ocurrs when the showing tooltip is no longer required.
        /// </summary>
        public event EventHandler CancelToolTip;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the TooltipManager class.
        /// </summary>
        public ToolTipManager()
        {
            _startTimer = new Timer();
            _startTimer.Interval = 1200;
            _startTimer.Tick += new EventHandler(OnStartTimerTick);

            _stopTimer = new Timer();
            _stopTimer.Interval = 100;
            _stopTimer.Tick += new EventHandler(OnStopTimerTick);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the interval before a tooltip is shown.
        /// </summary>
        public int ShowInterval
        {
            get { return _startTimer.Interval; }

            set
            {
                // Cannot have an interval less than 1ms
                if (value < 0)
                    value = 1;

                _startTimer.Interval = value;
            }
        }

        /// <summary>
        /// Gets and sets the interval before a tooltip is closed.
        /// </summary>
        public int CloseInterval
        {
            get { return _stopTimer.Interval; }

            set
            {
                // Cannot have an interval less than 1ms
                if (value < 0)
                    value = 1;

                _stopTimer.Interval = value;
            }
        }
        #endregion

        #region IMouseController Snooped Messages
        /// <summary>
        /// Mouse has entered the view.
        /// </summary>
        /// <param name="targetElement">Target element for the mouse message.</param>
        /// <param name="c">Reference to the source control instance.</param>
        public void MouseEnter(ViewBase targetElement, Control c)
        {
            // Remember the current target
            _currentTarget = targetElement;

            // If not currently showing a tooltip
            if (!_showingToolTips)
            {
                try
                {
                    // If there is no start timer running
                    if (_startTarget == null)
                    {
                        // Start the timer and associate it with the target
                        _startTarget = targetElement;
                        _startTimer.Start();
                    }
                    else
                    {
                        // If the running start timer is not for the new target
                        if (_startTarget != targetElement)
                        {
                            // Stop the currently running start timer
                            _startTimer.Stop();

                            // Restart the timer and associate it with the target
                            _startTarget = targetElement;
                            _startTimer.Start();
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Mouse has moved inside the view.
        /// </summary>
        /// <param name="targetElement">Target element for the mouse message.</param>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public void MouseMove(ViewBase targetElement, Control c, Point pt)
        {
        }

        /// <summary>
        /// Mouse button has been pressed in the view.
        /// </summary>
        /// <param name="targetElement">Target element for the mouse message.</param>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button pressed down.</param>
        public void MouseDown(ViewBase targetElement, 
                              Control c, 
                              Point pt, 
                              MouseButtons button)
        {
            // Stop any timers
            _startTimer.Stop();
            _stopTimer.Stop();

            // Remove tracking of any elements
            _currentTarget = null;
            _startTarget = null;

            // Pressing the mouse down kills any toolkit
            if (_showingToolTips)
            {
                _showingToolTips = false;
                OnCancelToolTip();
            }
        }

        /// <summary>
        /// Mouse button has been released in the view.
        /// </summary>
        /// <param name="targetElement">Target element for the mouse message.</param>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button released.</param>
        public void MouseUp(ViewBase targetElement, 
                            Control c, 
                            Point pt, 
                            MouseButtons button)
        {
        }

        /// <summary>
        /// Mouse has left the view.
        /// </summary>
        /// <param name="targetElement">Target element for the mouse message.</param>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        public void MouseLeave(ViewBase targetElement, Control c, ViewBase next)
        {
            // No longer have a current target
            _currentTarget = null;

            // If currently showing a tooltip for the current target
            if (_showingToolTips)
            {
                try
                {
                    // Restart the stop timer
                    _stopTimer.Stop();
                    _stopTimer.Start();
                }
                catch { }
            }
        }

        /// <summary>
        /// Left mouse button double click.
        /// </summary>
        /// <param name="targetElement">Target element for the mouse message.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public void DoubleClick(ViewBase targetElement, Point pt)
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the ShowTooltip event.
        /// </summary>
        /// <param name="e">A TooltipEventArgs that contains the event data.</param>
        protected virtual void OnShowToolTip(ToolTipEventArgs e)
        {
            if (ShowToolTip != null)
                ShowToolTip(this, e);
        }

        /// <summary>
        /// Raises the CancelTooltip event.
        /// </summary>
        protected virtual void OnCancelToolTip()
        {
            if (CancelToolTip != null)
                CancelToolTip(this, EventArgs.Empty);
        }
        #endregion

        #region Implementation
        private void OnStartTimerTick(object sender, EventArgs e)
        {
            // One tick timer, so always stop
            _startTimer.Stop();

            // Is the target the same as when the timer was kicked off?
            if (_currentTarget == _startTarget)
            {
                // Enter showing tooltips mode
                _showingToolTips = true;

                // Raise event requesting the tooltip be shown
                OnShowToolTip(new ToolTipEventArgs(_startTarget, Control.MousePosition));
            }
            else
            {
                // Timer no longer valid, so reset the associated target
                _startTarget = null;
            }
        }

        private void OnStopTimerTick(object sender, EventArgs e)
        {
            // One tick timer, so always stop
            _stopTimer.Stop();

            // Is the target is not the same as the currently showing tooltip
            if (_currentTarget != _startTarget)
            {
                // Leave tooltips mode
                _startTarget = null;
                _showingToolTips = false;

                // Raises event indicating the tooltip should be removed
                OnCancelToolTip();

                // If we are over a new target then show straight away
                if (_currentTarget != null)
                {
                    // Enter showing tooltips mode
                    _showingToolTips = true;

                    // Target is the current element
                    _startTarget = _currentTarget;

                    // Raise event requesting the tooltip be shown
                    OnShowToolTip(new ToolTipEventArgs(_startTarget, Control.MousePosition));
                }
            }
        }
        #endregion
    }
}
