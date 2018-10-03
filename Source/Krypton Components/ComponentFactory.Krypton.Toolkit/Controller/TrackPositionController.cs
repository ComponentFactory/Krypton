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
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Process mouse events a track bar position indicator.
	/// </summary>
    public class TrackPositionController : GlobalId,
                                           IMouseController
	{
		#region Instance Fields
        private ViewDrawTP _drawTB;
        private Point _lastMovePt;
        private bool _captured;
        private bool _mouseOver;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the TrackPositionController class.
		/// </summary>
        /// <param name="drawTB">Associated drawing element.</param>
        public TrackPositionController(ViewDrawTP drawTB)
		{
            _drawTB = drawTB;
        }
		#endregion

        #region Mouse Notifications
        /// <summary>
		/// Mouse has entered the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void MouseEnter(Control c)
		{
            _mouseOver = true;
            UpdateTargetState();
		}

		/// <summary>
		/// Mouse has moved inside the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
		{
            if (_captured)
            {
                // Ignore multiple calls with the same point
                if ((_lastMovePt == null) || (_lastMovePt != pt))
                {
                    _lastMovePt = pt;

                    // Find the new target value given mouse position
                    int newTargetValue = _drawTB.NearestValueFromPoint(pt);

                    // If this is a change in value then update now
                    if (_drawTB.ViewDrawTrackBar.Value != newTargetValue)
                        _drawTB.ViewDrawTrackBar.ScrollValue = Math.Max(_drawTB.ViewDrawTrackBar.Minimum, Math.Min(newTargetValue, _drawTB.ViewDrawTrackBar.Maximum));
                }
            }
		}

		/// <summary>
		/// Mouse button has been pressed in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button pressed down.</param>
		/// <returns>True if capturing input; otherwise false.</returns>
        public virtual bool MouseDown(Control c, Point pt, MouseButtons button)
		{
            if (button == MouseButtons.Left)
            {
                _captured = true;
                UpdateTargetState();
            }

			return _captured;
		}

		/// <summary>
		/// Mouse button has been released in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button released.</param>
        public virtual void MouseUp(Control c, Point pt, MouseButtons button)
		{
            if (_captured)
            {
                _captured = false;
                UpdateTargetState();
            }
		}

		/// <summary>
		/// Mouse has left the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        public virtual void MouseLeave(Control c, ViewBase next)
		{
            _mouseOver = false;
            _lastMovePt = Point.Empty;
            UpdateTargetState();
		}

        /// <summary>
        /// Left mouse button double click.
        /// </summary>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void DoubleClick(Point pt)
        {
            // Do nothing
        }

        /// <summary>
        /// Should the left mouse down be ignored when present on a visual form border area.
        /// </summary>
        public virtual bool IgnoreVisualFormLeftButtonDown
        {
            get { return false; }
        }
        #endregion

        #region Implementation
        private void UpdateTargetState()
        {
            PaletteState newState = PaletteState.Normal;
            
            if (_mouseOver)
            {
                if (_captured)
                    newState = PaletteState.Pressed;
                else
                    newState = PaletteState.Tracking;
            }

            if (_drawTB.ViewDrawTrackPosition.ElementState != newState)
            {
                _drawTB.ViewDrawTrackPosition.ElementState = newState;
                _drawTB.ViewDrawTrackBar.PerformNeedPaint(true);
            }
        }
        #endregion
    }
}
