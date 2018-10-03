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
    /// Draw and operate a track bar.
    /// </summary>
    public class ViewDrawTrackBar : ViewDrawPanel
    {
        #region Static Fields
        private static readonly Size _positionSizeSmallH = new Size(11, 15);
        private static readonly Size _positionSizeSmallV = new Size(15, 11);
        private static readonly Size _positionSizeMediumH = new Size(13, 21);
        private static readonly Size _positionSizeMediumV = new Size(21, 13);
        private static readonly Size _positionSizeLargeH = new Size(17, 27);
        private static readonly Size _positionSizeLargeV = new Size(27, 17);
        private static readonly Size _trackSizeSmall = new Size(2, 2);
        private static readonly Size _trackSizeSmallV = new Size(6, 6);
        private static readonly Size _trackSizeMedium = new Size(4, 4);
        private static readonly Size _trackSizeMediumV = new Size(11, 11);
        private static readonly Size _trackSizeLarge = new Size(5, 5);
        private static readonly Size _trackSizeLargeV = new Size(16, 16);
        private static readonly Size _tickSizeSmall = new Size(5, 5);
        private static readonly Size _tickSizeMedium = new Size(6, 6);
        private static readonly Size _tickSizeLarge = new Size(7, 7);
        #endregion

        #region Instance Fields
        private PaletteTrackBarStates _stateDisabled;
        private PaletteTrackBarStatesOverride _stateNormal;
        private PaletteTrackBarPositionStatesOverride _stateTracking;
        private PaletteTrackBarPositionStatesOverride _statePressed;
        private Padding _padding;
        private Orientation _orientation;
        private TickStyle _tickStyle;
        private int _tickFreq;
        private int _value;
        private int _minimum;
        private int _maximum;
        private int _smallChange;
        private int _largeChange;
        private bool _volumeControl;
        private ViewLayoutDocker _layoutTop;
        private ViewDrawTP _trackPosition;
        private ViewDrawTrackTicks _ticksTop;
        private ViewDrawTrackTicks _ticksBottom;
        private RightToLeft _rightToLeft;
        private PaletteTrackBarSize _trackBarSize;
        private NeedPaintHandler _needPaint;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the Value property changes.
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Occurs when the value has changed because of a user change.
        /// </summary>
        public event EventHandler Scroll;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawTrackBar class.
		/// </summary>
        /// <param name="stateNormal">Referenece to normal state values.</param>
        /// <param name="stateDisabled">Referenece to disabled state values.</param>
        /// <param name="stateTracking">Referenece to tracking state values.</param>
        /// <param name="statePressed">Referenece to pressed state values.</param>
        /// <param name="needPaint">Delegate used to request repainting.</param>
        public ViewDrawTrackBar(PaletteTrackBarStatesOverride stateNormal,
                                PaletteTrackBarStates stateDisabled,
                                PaletteTrackBarPositionStatesOverride stateTracking,
                                PaletteTrackBarPositionStatesOverride statePressed,
                                NeedPaintHandler needPaint)
            : base(stateNormal.Back)
		{
            // Default state
            _stateNormal = stateNormal;
            _stateDisabled = stateDisabled;
            _stateTracking = stateTracking;
            _statePressed = statePressed;
            _padding = Padding.Empty;
            _orientation = Orientation.Horizontal;
            _value = 0;
            _minimum = 0;
            _maximum = 10;
            _smallChange = 1;
            _largeChange = 5;
            _tickFreq = 1;
            _tickStyle = TickStyle.BottomRight;
            _trackBarSize = PaletteTrackBarSize.Medium;
            _volumeControl = false;
            _needPaint = needPaint;

            // Create drawing/layout elements
            _trackPosition = new ViewDrawTP(this);
            _ticksTop = new ViewDrawTrackTicks(this, true);
            _ticksBottom = new ViewDrawTrackTicks(this, false);
            _ticksTop.Visible = false;
            _ticksBottom.Visible = true;

            // Connect up layout structure
            _layoutTop = new ViewLayoutDocker();
            _layoutTop.Add(_ticksTop, ViewDockStyle.Top);
            _layoutTop.Add(_trackPosition, ViewDockStyle.Top);
            _layoutTop.Add(_ticksBottom, ViewDockStyle.Top);
            _layoutTop.Padding = Padding;
            Add(_layoutTop);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawTrackBar:" + Id;
		}
		#endregion

        #region Public
        /// <summary>
        /// Gets the track position element.
        /// </summary>
        public ViewDrawTP TrackPosition
        {
            get { return _trackPosition; }
        }

        /// <summary>
        /// Gets and sets the track bar size.
        /// </summary>
        public PaletteTrackBarSize TrackBarSize
        {
            get { return _trackBarSize; }
            set { _trackBarSize = value; }
        }

        /// <summary>
        /// Gets and sets if the track bar displays like a volume control.
        /// </summary>
        public bool VolumeControl
        {
            get { return _volumeControl; }
            set { _volumeControl = value; }
        }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        public Padding Padding
        {
            get { return _padding; }
            set { _padding = value; }
        }

        /// <summary>
        /// Gets and sets the right to left setting.
        /// </summary>
        public RightToLeft RightToLeft
        {
            get { return _rightToLeft; }
            set { _rightToLeft = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating how to display the tick marks on the track bar.
        /// </summary>
        public TickStyle TickStyle
        {
            get { return _tickStyle; }

            set
            {
                if (value != _tickStyle)
                {
                    _tickStyle = value;

                    // Decide which of the tick tracks are needed
                    bool topLeft = false;
                    bool bottomRight = false;
                    switch (_tickStyle)
                    {
                        case TickStyle.TopLeft:
                            topLeft = true;
                            break;
                        case TickStyle.BottomRight:
                            bottomRight = true;
                            break;
                        case TickStyle.Both:
                            topLeft = true;
                            bottomRight = true;
                            break;
                    }

                    _ticksTop.Visible = topLeft;
                    _ticksBottom.Visible = bottomRight;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies the delta between ticks drawn on the control.
        /// </summary>
        public int TickFrequency
        {
            get { return _tickFreq; }

            set
            {
                if (value != _tickFreq)
                    _tickFreq = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the horizontal or vertical orientation of the track bar.
        /// </summary>
        public Orientation Orientation
        {
            get { return _orientation; }

            set
            {
                if (value != _orientation)
                {
                    _orientation = value;
                    VisualOrientation visual = (_orientation == Orientation.Horizontal ? VisualOrientation.Top : VisualOrientation.Right);
                    _layoutTop.Orientation = visual;
                }
            }
        }

        /// <summary>
        /// Gets or sets the upper limit of the range this trackbar is working with.
        /// </summary>
        public int Maximum
        {
            get { return _maximum; }

            set
            {
                if (value != _maximum)
                {
                    if (value < _minimum)
                        _minimum = value;

                    SetRange(Minimum, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the lower limit of the range this trackbar is working with.
        /// </summary>
        public int Minimum
        {
            get { return _minimum; }

            set
            {
                if (value != _minimum)
                {
                    if (value > _maximum)
                        _maximum = value;

                    SetRange(value, Maximum);
                }
            }
        }

        /// <summary>
        /// Gets or sets a numeric value that represents the current position of the scroll box on the track bar.
        /// </summary>
        public int Value
        {
            get { return _value; }

            set
            {
                if (value != _value)
                {
                    if ((value < Minimum) || (value > Maximum))
                        throw new ArgumentOutOfRangeException("Value", "Provided value is out of the Minimum to Maximum range of values.");

                    _value = value;
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Sets a numeric value that represents the current position because of a user change.
        /// </summary>
        public int ScrollValue
        {
            set
            {
                if (value != _value)
                {
                    if ((value < Minimum) || (value > Maximum))
                        throw new ArgumentOutOfRangeException("Value", "Provided value is out of the Minimum to Maximum range of values.");

                    _value = value;
                    OnScroll(EventArgs.Empty);
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value added to or subtracted from the Value property when the scroll box is moved a small distance.
        /// </summary>
        public int SmallChange
        {
            get { return _smallChange; }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("SmallChange", "SmallChange cannot be less than zero.");

                _smallChange = value;
            }
        }

        /// <summary>
        /// Gets or sets a value to be added to or subtracted from the Value property when the scroll box is moved a large distance.
        /// </summary>
        public int LargeChange
        {
            get { return _largeChange; }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("LargeChange", "LargeChange cannot be less than zero.");

                _largeChange = value;
            }
        }

        /// <summary>
        /// Sets the minimum and maximum values for a TrackBar.
        /// </summary>
        /// <param name="minValue">The lower limit of the range of the track bar.</param>
        /// <param name="maxValue">The upper limit of the range of the track bar.</param>
        public void SetRange(int minValue, int maxValue)
        {
            if ((Minimum != minValue) || (Maximum != maxValue))
            {
                if (minValue > maxValue)
                    minValue = maxValue;

                _minimum = minValue;
                _maximum = maxValue;

                int beforeValue = _value;
                if (_value < _minimum)
                    _value = _minimum;

                if (_value > _maximum)
                    _value = _maximum;

                if (beforeValue != _value)
                    OnValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            if ((state == PaletteState.Normal) || (state == PaletteState.Disabled))
            {
                _ticksTop.FixedState = state;
                _ticksBottom.FixedState = state;
            }

            _trackPosition.SetFixedState(state);
        }

        /// <summary>
        /// Gets and sets the enabled state of the element.
        /// </summary>
        public override bool Enabled
        {
            get { return base.Enabled; }
            
            set
            {
                base.Enabled = value;

                // Update with latest enabled state
                _layoutTop.Enabled = value;
                _trackPosition.Enabled = value;
                _ticksTop.Enabled = value;
                _ticksBottom.Enabled = value;
            }
        }

        /// <summary>
        /// Processes the MouseWheel event.
        /// </summary>
        /// <param name="e">Event arguments for the event.</param>
        public void OnMouseWheel(MouseEventArgs e)
        {
            int change = (e.Delta > 0) ? -SmallChange : SmallChange;
            int detents = Math.Abs(e.Delta) / SystemInformation.MouseWheelScrollDelta;
            for (int i = 0; i < detents; i++)
                ScrollValue = Math.Max(Minimum, Math.Min(Value - change, Maximum));
        }

        /// <summary>
        /// Gets the size of the position indicator.
        /// </summary>
        public Size PositionSize
        {
            get
            {
                switch (_trackBarSize)
                {
                    case PaletteTrackBarSize.Small:
                        return (_orientation == Orientation.Horizontal ? _positionSizeSmallH : _positionSizeSmallV);
                    default:
                    case PaletteTrackBarSize.Medium:
                        return (_orientation == Orientation.Horizontal ? _positionSizeMediumH : _positionSizeMediumV);
                    case PaletteTrackBarSize.Large:
                        return (_orientation == Orientation.Horizontal ? _positionSizeLargeH : _positionSizeLargeV);
                }
            }
        }

        /// <summary>
        /// Gets the size of the track.
        /// </summary>
        public Size TrackSize
        {
            get
            {
                switch (_trackBarSize)
                {
                    case PaletteTrackBarSize.Small:
                        return VolumeControl ? _trackSizeSmallV : _trackSizeSmall;
                    default:
                    case PaletteTrackBarSize.Medium:
                        return VolumeControl ? _trackSizeMediumV : _trackSizeMedium;
                    case PaletteTrackBarSize.Large:
                        return VolumeControl ? _trackSizeLargeV : _trackSizeLarge;
                }
            }
        }

        /// <summary>
        /// Gets the size of the tick area.
        /// </summary>
        public Size TickSize
        {
            get
            {
                switch (_trackBarSize)
                {
                    case PaletteTrackBarSize.Small:
                        return _tickSizeSmall;
                    default:
                    case PaletteTrackBarSize.Medium:
                        return _tickSizeMedium;
                    case PaletteTrackBarSize.Large:
                        return _tickSizeLarge;
                }
            }
        }

        /// <summary>
        /// Gets access to the normal state.
        /// </summary>
        public PaletteTrackBarStatesOverride StateNormal
        {
            get { return _stateNormal; }
        }

        /// <summary>
        /// Gets access to the disabled state.
        /// </summary>
        public PaletteTrackBarStates StateDisabled
        {
            get { return _stateDisabled; }
        }

        /// <summary>
        /// Gets access to the tracking state.
        /// </summary>
        public PaletteTrackBarPositionStatesOverride StateTracking
        {
            get { return _stateTracking; }
        }

        /// <summary>
        /// Gets access to the pressed state.
        /// </summary>
        public PaletteTrackBarPositionStatesOverride StatePressed
        {
            get { return _statePressed; }
        }
    
        /// <summary>
        /// Raises a need paint event.
        /// </summary>
        /// <param name="needLayout">Does the layout need recalculating.</param>
        public void PerformNeedPaint(bool needLayout)
        {
            if (_needPaint != null)
                _needPaint(this, new NeedLayoutEventArgs(needLayout));
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the ValueChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        /// <summary>
        /// Raises the Scroll event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnScroll(EventArgs e)
        {
            if (Scroll != null)
                Scroll(this, e);
        }
        #endregion
    }
}
