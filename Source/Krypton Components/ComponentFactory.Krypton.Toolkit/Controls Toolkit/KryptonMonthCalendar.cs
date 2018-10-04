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
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Enables the user to select a date using a visual monthly calendar display.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonMonthCalendar), "ToolboxBitmaps.KryptonMonthCalendar.bmp")]
    [DefaultEvent("DateChanged")]
    [DefaultProperty("SelectionRange")]
    [DefaultBindingProperty("SelectionRange")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonMonthCalendarDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Select a date using a visual monthly calendar display.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonMonthCalendar : VisualSimpleBase,
                                        IKryptonMonthCalendar
    {
        #region Instance Fields
        private ViewDrawDocker _drawDocker;
        private ViewLayoutMonths _drawMonths;
        private PaletteMonthCalendarRedirect _stateCommon;
        private PaletteMonthCalendarStateRedirect _stateFocus, _stateBolded, _stateToday;
        private PaletteMonthCalendarDoubleState _stateDisabled, _stateNormal;
        private PaletteMonthCalendarState _stateTracking, _statePressed;
        private PaletteMonthCalendarState _stateCheckedNormal, _stateCheckedTracking, _stateCheckedPressed;
        private PaletteTripleOverride _boldedDisabled, _boldedNormal, _boldedTracking, _boldedPressed;
        private PaletteTripleOverride _boldedCheckedNormal, _boldedCheckedTracking, _boldedCheckedPressed;
        private PaletteTripleOverride _todayDisabled, _todayNormal, _todayTracking, _todayPressed;
        private PaletteTripleOverride _todayCheckedNormal, _todayCheckedTracking, _todayCheckedPressed;
        private PaletteTripleOverride _overrideDisabled, _overrideNormal, _overrideTracking, _overridePressed;
        private PaletteTripleOverride _overrideCheckedNormal, _overrideCheckedTracking, _overrideCheckedPressed;
        private HeaderStyle _headerStyle;
        private ButtonStyle _dayStyle;
        private ButtonStyle _dayOfWeekStyle;
        private DateTime _selectionStart;
        private DateTime _selectionEnd;
        private DateTime _minDate;
        private DateTime _maxDate;
        private DateTime _todayDate;
        private DateTimeList _annualDates;
        private DateTimeList _monthlyDates;
        private DateTimeList _dates;
        private Day _firstDayOfWeek;
        private Size _dimensions;
        private string _todayFormat;
        private int _maxSelectionCount;
        private int _monthlyDays;
        private int _scrollChange;
        private int[] _annualDays;
        private bool _hasFocus;
        private DateTime? _focusDay;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the selected date changes.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the selected date changes.")]
        public event DateRangeEventHandler DateChanged;

        /// <summary>
        /// Occurs when the selected start date changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the selected start date changes.")]
        public event EventHandler SelectionStartChanged;

        /// <summary>
        /// Occurs when the selected end date changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the selected end date changes.")]
        public event EventHandler SelectionEndChanged;

        /// <summary>
        /// Occurs when the control is clicked.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler Click;

        /// <summary>
        /// Occurs when the control is double clicked.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler DoubleClick;

        /// <summary>
        /// Occurs when the text value changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler TextChanged;

        /// <summary>
        /// Occurs when the foreground color value changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler ForeColorChanged;

        /// <summary>
        /// Occurs when the font value changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler FontChanged;

        /// <summary>
        /// Occurs when the background image value changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageChanged;

        /// <summary>
        /// Occurs when the background image layout value changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackgroundImageLayoutChanged;

        /// <summary>
        /// Occurs when the background color value changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler BackColorChanged;

        /// <summary>
        /// Occurs when the padding value changes.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler PaddingChanged;

        /// <summary>
        /// Occurs when the control needs to paint.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event PaintEventHandler Paint;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonMonthCalendar class.
		/// </summary>
        public KryptonMonthCalendar()
		{
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // Create the palette storage
            _stateCommon = new PaletteMonthCalendarRedirect(Redirector, NeedPaintDelegate);
            _stateFocus = new PaletteMonthCalendarStateRedirect(Redirector, NeedPaintDelegate);
            _stateBolded = new PaletteMonthCalendarStateRedirect(Redirector, NeedPaintDelegate);
            _stateToday = new PaletteMonthCalendarStateRedirect(Redirector, NeedPaintDelegate);

            // Basic state storage
            _stateDisabled = new PaletteMonthCalendarDoubleState(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteMonthCalendarDoubleState(_stateCommon, NeedPaintDelegate);
            _stateTracking = new PaletteMonthCalendarState(_stateCommon, NeedPaintDelegate);
            _statePressed = new PaletteMonthCalendarState(_stateCommon, NeedPaintDelegate);
            _stateCheckedNormal = new PaletteMonthCalendarState(_stateCommon, NeedPaintDelegate);
            _stateCheckedTracking = new PaletteMonthCalendarState(_stateCommon, NeedPaintDelegate);
            _stateCheckedPressed = new PaletteMonthCalendarState(_stateCommon, NeedPaintDelegate);

            // Bold overrides
            _boldedDisabled = new PaletteTripleOverride(_stateBolded.Day, _stateDisabled.Day, PaletteState.BoldedOverride);
            _boldedNormal = new PaletteTripleOverride(_stateBolded.Day, _stateNormal.Day, PaletteState.BoldedOverride);
            _boldedTracking = new PaletteTripleOverride(_stateBolded.Day, _stateTracking.Day, PaletteState.BoldedOverride);
            _boldedPressed = new PaletteTripleOverride(_stateBolded.Day, _statePressed.Day, PaletteState.BoldedOverride);
            _boldedCheckedNormal = new PaletteTripleOverride(_stateBolded.Day, _stateCheckedNormal.Day, PaletteState.BoldedOverride);
            _boldedCheckedTracking = new PaletteTripleOverride(_stateBolded.Day, _stateCheckedTracking.Day, PaletteState.BoldedOverride);
            _boldedCheckedPressed = new PaletteTripleOverride(_stateBolded.Day, _stateCheckedPressed.Day, PaletteState.BoldedOverride);

            // Today overrides
            _todayDisabled = new PaletteTripleOverride(_stateToday.Day, _boldedDisabled, PaletteState.TodayOverride);
            _todayNormal = new PaletteTripleOverride(_stateToday.Day, _boldedNormal, PaletteState.TodayOverride);
            _todayTracking = new PaletteTripleOverride(_stateToday.Day, _boldedTracking, PaletteState.TodayOverride);
            _todayPressed = new PaletteTripleOverride(_stateToday.Day, _boldedPressed, PaletteState.TodayOverride);
            _todayCheckedNormal = new PaletteTripleOverride(_stateToday.Day, _boldedCheckedNormal, PaletteState.TodayOverride);
            _todayCheckedTracking = new PaletteTripleOverride(_stateToday.Day, _boldedCheckedTracking, PaletteState.TodayOverride);
            _todayCheckedPressed = new PaletteTripleOverride(_stateToday.Day, _boldedCheckedPressed, PaletteState.TodayOverride);

            // Focus overrides added to bold overrides
            _overrideDisabled = new PaletteTripleOverride(_stateFocus.Day, _todayDisabled, PaletteState.FocusOverride);
            _overrideNormal = new PaletteTripleOverride(_stateFocus.Day, _todayNormal, PaletteState.FocusOverride);
            _overrideTracking = new PaletteTripleOverride(_stateFocus.Day, _todayTracking, PaletteState.FocusOverride);
            _overridePressed = new PaletteTripleOverride(_stateFocus.Day, _todayPressed, PaletteState.FocusOverride);
            _overrideCheckedNormal = new PaletteTripleOverride(_stateFocus.Day, _todayCheckedNormal, PaletteState.FocusOverride);
            _overrideCheckedTracking = new PaletteTripleOverride(_stateFocus.Day, _todayCheckedTracking, PaletteState.FocusOverride);
            _overrideCheckedPressed = new PaletteTripleOverride(_stateFocus.Day, _todayCheckedPressed, PaletteState.FocusOverride);

            // Create view that is used by standalone control as well as this context menu element
            _drawMonths = new ViewLayoutMonths(null, null, null, this, Redirector, NeedPaintDelegate);

            // Place the months layout view inside a standard docker which provides the control border
            _drawDocker = new ViewDrawDocker(_stateNormal.Back, _stateNormal.Border, null);
            _drawDocker.Add(_drawMonths, ViewDockStyle.Fill);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawDocker);

            // Set default property values 
            _dimensions = new Size(1, 1);
            _firstDayOfWeek = Day.Default;
            _headerStyle = HeaderStyle.Calendar;
            _dayStyle = ButtonStyle.CalendarDay;
            _dayOfWeekStyle = ButtonStyle.CalendarDay;
            _selectionStart = DateTime.Now.Date;
            _selectionEnd = _selectionStart;
            _todayDate = _selectionStart;
            _minDate = DateTimePicker.MinimumDateTime;
            _maxDate = DateTimePicker.MaximumDateTime;
            _maxSelectionCount = 7;
            _annualDays = new int[12];
            _annualDates = new DateTimeList();
            _monthlyDates = new DateTimeList();
            _dates = new DateTimeList();
            _scrollChange = 0;
            _todayFormat = "d";
        }
		#endregion

        #region Public
        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public override AutoSizeMode  AutoSizeMode
        {
	        get { return base.AutoSizeMode; }	  
            set { base.AutoSizeMode = value; }
        }

        /// <summary>
        /// Gets or sets the Input Method Editor (IME) mode of the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImeMode ImeMode 
        {
            get { return base.ImeMode; }
            set { base.ImeMode = value; }
        }

        /// <summary>
        /// Gets or sets the padding internal to the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        /// <summary>
        /// Gets or sets the minimum allowable date.
        /// </summary>
        [Category("Behavior")]
        [Description("Minimum allowable date.")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime MinDate
        {
            get { return EffectiveMinDate(_minDate); }
            
            set 
            {
                if (value != _minDate)
                {
                    if (value > EffectiveMaxDate(_maxDate))
                        throw new ArgumentOutOfRangeException("Date provided is greater than the maximum supported date.");

                    if (value < DateTimePicker.MinimumDateTime)
                        throw new ArgumentOutOfRangeException("Date provided is less than the minimum supported date.");

                    _minDate = value;

                    // Update selection dates to be valid with new min date
                    SetRange();
                }
            }
        }

        private void ResetMinDate()
        {
            MinDate = DateTimePicker.MinimumDateTime;
        }

        private bool ShouldSerializeMinDate()
        {
            return (_minDate != DateTimePicker.MinimumDateTime);
        }

        /// <summary>
        /// Gets or sets the today date format string.
        /// </summary>
        [Category("Behavior")]
        [Description("The today format string used to format the date displayed in the today button.")]
        [DefaultValue("d")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Localizable(true)]
        public string TodayFormat
        {
            get { return _todayFormat; }

            set
            {
                if ((_todayFormat != value) && (value != null))
                {
                    _todayFormat = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of months to scroll when next/prev buttons are used.
        /// </summary>
        [Category("Behavior")]
        [Description("Number of months to scroll when next/prev buttons are used.")]
        [DefaultValue(0)]
        public int ScrollChange
        {
            get { return _scrollChange; }

            set
            {
                if (value < 0)
                    value = 0;

                _scrollChange = value;
                PerformNeedPaint(true);
            }
        }

        /// <summary>
        /// Gets or sets today's date.
        /// </summary>
        [Category("Behavior")]
        [Description("Today's date.")]
        public DateTime TodayDate
        {
            get { return _todayDate; }

            set
            {
                if (value == null)
                    value = DateTime.Now.Date;

                _todayDate = value;
                PerformNeedPaint(true);
            }
        }

        private void ResetTodayDate()
        {
            TodayDate = DateTime.Now.Date;
        }

        private bool ShouldSerializeTodayDate()
        {
            return (TodayDate != DateTime.Now.Date);
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which annual days are displayed in bold.
        /// </summary>
        [Localizable(true)]
        [Description("Indicates which annual dates should be boldface.")]
        public DateTime[] AnnuallyBoldedDates
        {
            get { return _annualDates.ToArray(); }

            set
            {
                if (value == null)
                    value = new DateTime[0];
                
                _annualDates.Clear();
                _annualDates.AddRange(value);

                for (int i = 0; i < 12; i++)
                    _annualDays[i] = 0;

                // Set bitmap matching the days of month to be bolded
                foreach (DateTime dt in value)
                    _annualDays[dt.Month - 1] |= 1 << (dt.Day - 1);

                PerformNeedPaint(true);
            }
        }

        private void ResetAnnuallyBoldedDates()
        {
            AnnuallyBoldedDates = null;
        }

        private bool ShouldSerializeAnnuallyBoldedDates()
        {
            return (_annualDates.Count > 0);
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
        /// </summary>
        [Localizable(true)]
        [Description("Indicates which monthly dates should be boldface.")]
        public DateTime[] MonthlyBoldedDates
        {
            get { return _monthlyDates.ToArray(); }

            set
            {
                if (value == null)
                    value = new DateTime[0];

                _monthlyDates.Clear();
                _monthlyDates.AddRange(value);

                // Set bitmap matching the days of month to be bolded
                _monthlyDays = 0;
                foreach (DateTime dt in value)
                    _monthlyDays |= 1 << (dt.Day - 1);

                PerformNeedPaint(true);
            }
        }

        private void ResetMonthlyBoldedDates()
        {
            MonthlyBoldedDates = null;
        }

        private bool ShouldSerializeMonthlyBoldedDates()
        {
            return (_monthlyDates.Count > 0);
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are displayed in bold.
        /// </summary>
        [Localizable(true)]
        [Description("Indicates which dates should be boldface.")]
        public DateTime[] BoldedDates
        {
            get { return _dates.ToArray(); }

            set
            {
                if (value == null)
                    value = new DateTime[0];

                _dates.Clear();
                _dates.AddRange(value);
                PerformNeedPaint(true);
            }
        }

        private void ResetBoldedDates()
        {
            BoldedDates = null;
        }

        private bool ShouldSerializeBoldedDates()
        {
            return (_dates.Count > 0);
        }

        /// <summary>
        /// Gets or sets the maximum allowable date.
        /// </summary>
        [Category("Behavior")]
        [Description("Maximum allowable date.")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime MaxDate
        {
            get { return EffectiveMaxDate(_maxDate); }

            set 
            {
                if (value != _maxDate)
                {
                    if (value < EffectiveMinDate(_minDate))
                        throw new ArgumentOutOfRangeException("Date provided is less than the minimum supported date.");

                    if (value > DateTimePicker.MaximumDateTime)
                        throw new ArgumentOutOfRangeException("Date provided is greater than the maximum supported date.");

                    _maxDate = value;

                    // Update selection dates to be valid with new max date
                    SetRange();
                }
            }
        }

        private void ResetMaxDate()
        {
            MaxDate = DateTime.MaxValue;
        }

        private bool ShouldSerializeMaxDate()
        {
            return (_maxDate != DateTimePicker.MaximumDateTime) && (_maxDate != DateTime.MaxValue);
        }

        /// <summary>
        /// Gets or sets the maximum number of days that can be selected in a month calendar control.
        /// </summary>
        [Category("Behavior")]
        [Description("Maximum number of days that can be selected.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(7)]
        public int MaxSelectionCount
        {
            get { return _maxSelectionCount; }
            
            set 
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("MaxSelectionCount cannot be less than zero.");

                if (value != _maxSelectionCount)
                {
                    _maxSelectionCount = value;
                    SetSelectionRange(_selectionStart, _selectionEnd);
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the start date of the selected range of dates.
        /// </summary>
        [Category("Behavior")]
        [Description("Start date of the selected range of dates.")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        public DateTime SelectionStart
        {
            get { return _selectionStart; }

            set 
            {
                if (value != _selectionStart)
                {
                    if (value > _maxDate)
                        throw new ArgumentOutOfRangeException("Date provided is greater than the maximum date.");

                    if (value < _minDate)
                        throw new ArgumentOutOfRangeException("Date provided is less than the minimum date.");

                    DateTime endDate = _selectionEnd;

                    // End date cannot be before the start date
                    if (endDate < value)
                        endDate = value;

                    // Limit the selection range to the maximum selection count
                    TimeSpan range = endDate - value;
                    if (range.Days >= _maxSelectionCount)
                        endDate = value.AddDays(_maxSelectionCount - 1);

                    // Update selection dates and generate event if required
                    SetSelRange(value, endDate);
                }
            }
        }

        private void ResetSelectionStart()
        {
            SelectionStart = DateTime.Now.Date;
        }

        private bool ShouldSerializeSelectionStart()
        {
            return (SelectionStart != DateTime.Now.Date);
        }

        /// <summary>
        /// Gets or sets the end date of the selected range of dates.
        /// </summary>
        [Category("Behavior")]
        [Description("End date of the selected range of dates.")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        public DateTime SelectionEnd
        {
            get { return _selectionEnd; }

            set 
            {
                if (value != _selectionEnd)
                {
                    if (value > _maxDate)
                        throw new ArgumentOutOfRangeException("Date provided is greater than the maximum date.");

                    if (value < _minDate)
                        throw new ArgumentOutOfRangeException("Date provided is less than the minimum date.");

                    DateTime startDate = _selectionStart;

                    // Start date cannot be after the end date
                    if (startDate > value)
                        startDate = value;

                    // Limit the selection range to the maximum selection count
                    TimeSpan range = value - startDate;
                    if (range.Days >= _maxSelectionCount)
                        startDate = value.AddDays(1 - _maxSelectionCount);

                    // Update selection dates and generate event if required
                    SetSelRange(startDate, value);
                }
            }
        }

        private void ResetSelectionEnd()
        {
            SelectionEnd = DateTime.Now.Date;
        }

        private bool ShouldSerializeSelectionEnd()
        {
            return (SelectionEnd != DateTime.Now.Date);
        }

        /// <summary>
        /// Gets or sets the selected range of dates for a month calendar control.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies the selected range of dates.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        public SelectionRange SelectionRange
        {
            get { return new SelectionRange(SelectionStart, SelectionEnd); }
            set { SetSelectionRange(value.Start, value.End); }
        }

        private void ResetSelectionRange()
        {
            ResetSelectionStart();
            ResetSelectionEnd();
        }

        private bool ShouldSerializeSelectionRange()
        {
            return false;
        }

        /// <summary>
        /// Gets or sets the number of columns and rows of months displayed. 
        /// </summary>
        [Category("Appearance")]
        [Description("Specifies the number of rows and columns of months displayed.")]
        [DefaultValue(typeof(Size), "1,1")]
        [Localizable(true)]
        public Size CalendarDimensions
        {
            get { return _dimensions; }

            set
            {
                if (!_dimensions.Equals(value))
                {
                    if (value.Width < 1)
                        throw new ArgumentOutOfRangeException("CalendarDimension Width must be greater than 0");

                    if (value.Height < 1)
                        throw new ArgumentOutOfRangeException("CalendarDimension Height must be greater than 0");


                    _dimensions = value;

                    // Must update the size to get the new size we require, just calling the perform need 
                    // paint will cause the dimensions to be reset to that matching the current size.
                    Size = GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// First day of the week.
        /// </summary>
        [Category("Behavior")]
        [Description("First day of the week.")]
        [DefaultValue(typeof(Day), "Default")]
        [Localizable(true)]
        public Day FirstDayOfWeek 
        { 
            get { return _firstDayOfWeek; }

            set
            {
                if (_firstDayOfWeek != value)
                {
                    _firstDayOfWeek = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the background style for the month calendar.
        /// </summary>
        [Category("Visuals")]
        [Description("Background style for the month calendar.")]
        public PaletteBackStyle ControlBackStyle
        {
            get { return _stateCommon.BackStyle; }

            set
            {
                if (_stateCommon.BackStyle != value)
                {
                    _stateCommon.BackStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeControlBackStyle()
        {
            return (ControlBackStyle != PaletteBackStyle.ControlClient);
        }

        private void ResetControlBackStyle()
        {
            ControlBackStyle = PaletteBackStyle.ControlClient;
        }

        /// <summary>
        /// Gets and sets the border style for the month calendar.
        /// </summary>
        [Category("Visuals")]
        [Description("Border style for the month calendar.")]
        public PaletteBorderStyle ControlBorderStyle
        {
            get { return _stateCommon.BorderStyle; }

            set
            {
                if (_stateCommon.BorderStyle != value)
                {
                    _stateCommon.BorderStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeControlBorderStyle()
        {
            return (ControlBorderStyle != PaletteBorderStyle.ControlClient);
        }

        private void ResetControlBorderStyle()
        {
            ControlBorderStyle = PaletteBorderStyle.ControlClient;
        }

        /// <summary>
        /// Gets and sets the header style for the month calendar.
        /// </summary>
        [Category("Visuals")]
        [Description("Header style for the month calendar.")]
        public HeaderStyle HeaderStyle
        {
            get { return _headerStyle; }

            set
            {
                if (_headerStyle != value)
                {
                    _headerStyle = value;
                    _stateCommon.Header.SetStyles(_headerStyle);
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeHeaderStyle()
        {
            return (_headerStyle != HeaderStyle.Calendar);
        }

        private void ResetHeaderStyle()
        {
            HeaderStyle = HeaderStyle.Calendar;
        }

        /// <summary>
        /// Gets and sets the content style for the day entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Content style for the day entries.")]
        public ButtonStyle DayStyle
        {
            get { return _dayStyle; }

            set
            {
                if (_dayStyle != value)
                {
                    _dayStyle = value;
                    _stateCommon.DayStyle = value;
                    _stateBolded.DayStyle = value;
                    _stateFocus.DayStyle = value;
                    _stateToday.DayStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeDayStyle()
        {
            return (_dayStyle != ButtonStyle.CalendarDay);
        }

        private void ResetDayStyle()
        {
            DayOfWeekStyle = ButtonStyle.CalendarDay;
        }

        /// <summary>
        /// Gets and sets the content style for the day of week labels.
        /// </summary>
        [Category("Visuals")]
        [Description("Content style for the day of week labels.")]
        public ButtonStyle DayOfWeekStyle
        {
            get { return _dayOfWeekStyle; }

            set
            {
                if (_dayOfWeekStyle != value)
                {
                    _dayOfWeekStyle = value;
                    _stateCommon.DayOfWeekStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeDayOfWeekStyle()
        {
            return (_dayOfWeekStyle != ButtonStyle.CalendarDay);
        }


        private void ResetDayOfWeekStyle()
        {
            DayOfWeekStyle = ButtonStyle.CalendarDay;
        }

        /// <summary>
        /// Gets and sets if the control will display todays date.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether this month calendar will display todays date.")]
        [DefaultValue(true)]
        public bool ShowToday
        {
            get { return _drawMonths.ShowToday; }

            set
            {
                if (_drawMonths.ShowToday != value)
                {
                    _drawMonths.ShowToday = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets if the control will circle the today date.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether this month calendar will circle the today date.")]
        [DefaultValue(true)]
        public bool ShowTodayCircle
        {
            get { return _drawMonths.ShowTodayCircle; }

            set
            {
                if (_drawMonths.ShowTodayCircle != value)
                {
                    _drawMonths.ShowTodayCircle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets if week numbers to the left of each row.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether this month calendar will display week numbers to the left of each row.")]
        [DefaultValue(false)]
        public bool ShowWeekNumbers
        {
            get { return _drawMonths.ShowWeekNumbers; }

            set
            {
                if (_drawMonths.ShowWeekNumbers != value)
                {
                    _drawMonths.ShowWeekNumbers = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets access to the day appearance when it has focus.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining month calendar appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarStateRedirect OverrideFocus
        {
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_stateFocus.IsDefault;
        }

        /// <summary>
        /// Gets access to the day appearance when it is bolded.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining month calendar appearance when it is bolded.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarStateRedirect OverrideBolded
        {
            get { return _stateBolded; }
        }

        private bool ShouldSerializeOverrideBolded()
        {
            return !_stateBolded.IsDefault;
        }

        /// <summary>
        /// Gets access to the day appearance when it is todays.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining month calendar appearance when it is today.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarStateRedirect OverrideToday
        {
            get { return _stateToday; }
        }

        private bool ShouldSerializeOverrideToday()
        {
            return !_stateToday.IsDefault;
        }

        /// <summary>
        /// Gets access to the common month calendar appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common month calendar appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the month calendar disabled appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining month calendar disabled appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarDoubleState StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the month calendar normal appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining month calendar normal appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarDoubleState StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the tracking month calendar appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tracking month calendar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarState StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed month calendar appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed month calendar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarState StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the checked normal month calendar appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining checked normal month calendar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarState StateCheckedNormal
        {
            get { return _stateCheckedNormal; }
        }

        private bool ShouldSerializeStateCheckedNormal()
        {
            return !_stateCheckedNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the checked tracking month calendar appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining checked tracking month calendar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarState StateCheckedTracking
        {
            get { return _stateCheckedTracking; }
        }

        private bool ShouldSerializeStateCheckedTracking()
        {
            return !_stateCheckedTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the checked pressed month calendar appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining checked pressed month calendar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMonthCalendarState StateCheckedPressed
        {
            get { return _stateCheckedPressed; }
        }

        private bool ShouldSerializeStateCheckedPressed()
        {
            return !_stateCheckedPressed.IsDefault;
        }

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MonthCalendarButtonSpecCollection ButtonSpecs
        {
            get { return _drawMonths.ButtonSpecs; }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _drawMonths.AllowButtonSpecToolTips; }
            set { _drawMonths.AllowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Adds a day that is displayed in bold on an annual basis in the month calendar.
        /// </summary>
        /// <param name="date">The date to be displayed in bold.</param>
        public void AddAnnuallyBoldedDate(DateTime date)
        {
            if (!_annualDates.Contains(date))
            {
                _annualDates.Add(date);
                _annualDays[date.Month - 1] |= 1 << (date.Day - 1);
                PerformNeedPaint(true);
            }
        }

        /// <summary>
        /// Adds a day to be displayed in bold in the month calendar.
        /// </summary>
        /// <param name="date">The date to be displayed in bold.</param>
        public void AddBoldedDate(DateTime date)
        {
            if (!_dates.Contains(date))
            {
                _dates.Add(date);
                PerformNeedPaint(true);
            }
        }

        /// <summary>
        /// Adds a day that is displayed in bold on a monthly basis in the month calendar.
        /// </summary>
        /// <param name="date">The date to be displayed in bold.</param>
        public void AddMonthlyBoldedDate(DateTime date)
        {
            if (!_monthlyDates.Contains(date))
            {
                _monthlyDates.Add(date);
                _monthlyDays |= 1 << (date.Day - 1);
                PerformNeedPaint(true);
            }
        }

        /// <summary>
        /// Removes all the annually bold dates.
        /// </summary>
        public void RemoveAllAnnuallyBoldedDates()
        {
            _annualDates.Clear();
            for (int i = 0; i < 12; i++)
                _annualDays[i] = 0;
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Removes all the nonrecurring bold dates.
        /// </summary>
        public void RemoveAllBoldedDates()
        {
            _dates.Clear();
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Removes all the monthly bold dates.
        /// </summary>
        public void RemoveAllMonthlyBoldedDates()
        {
            _monthlyDates.Clear();
            _monthlyDays = 0;
            PerformNeedPaint(true);
        }


        /// <summary>
        /// Gets access to the owning control
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Control CalendarControl 
        {
            get { return this; }
        }

        /// <summary>
        /// Gets if the control is in design mode.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool InDesignMode
        {
            get { return DesignMode; }
        }

        /// <summary>
        /// Get the renderer.
        /// </summary>
        /// <returns>Render instance.</returns>
        public IRenderer GetRenderer()
        {
            return Renderer;
        }

        /// <summary>
        /// Gets access to the override for disabled day.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PaletteTripleOverride OverrideDisabled
        {
            get { return _overrideDisabled; }
        }

        /// <summary>
        /// Gets access to the override for disabled day.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PaletteTripleOverride OverrideNormal
        {
            get { return _overrideNormal; }
        }

        /// <summary>
        /// Gets access to the override for tracking day.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PaletteTripleOverride OverrideTracking
        {
            get { return _overrideTracking; }
        }

        /// <summary>
        /// Gets access to the override for pressed day.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PaletteTripleOverride OverridePressed
        {
            get { return _overridePressed; }
        }

        /// <summary>
        /// Gets access to the override for checked normal day.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PaletteTripleOverride OverrideCheckedNormal
        {
            get { return _overrideCheckedNormal; }
        }

        /// <summary>
        /// Gets access to the override for checked tracking day.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PaletteTripleOverride OverrideCheckedTracking
        {
            get { return _overrideCheckedTracking; }
        }

        /// <summary>
        /// Gets access to the override for checked pressed day.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PaletteTripleOverride OverrideCheckedPressed
        {
            get { return _overrideCheckedPressed; }
        }

        /// <summary>
        /// Dates to be bolded.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTimeList BoldedDatesList 
        {
            get { return _dates; }
        }

        /// <summary>
        /// Monthly days to be bolded.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int MonthlyBoldedDatesMask 
        {
            get { return _monthlyDays; }
        }

        /// <summary>
        /// Array of annual days per month to be bolded.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int[] AnnuallyBoldedDatesMask
        {
            get { return _annualDays; }
        }

        /// <summary>
        /// Set the selection range.
        /// </summary>
        /// <param name="start">New starting date.</param>
        /// <param name="end">New ending date.</param>
        public void SetSelectionRange(DateTime start, DateTime end)
        {
            if (start.Ticks > _maxDate.Ticks)
                throw new ArgumentOutOfRangeException("Start date provided is greater than the maximum date.");

            if (start.Ticks < _minDate.Ticks)
                throw new ArgumentOutOfRangeException("Start date provided is less than the minimum date.");

            if (end.Ticks > _maxDate.Ticks)
                throw new ArgumentOutOfRangeException("End date provided is greater than the maximum date.");

            if (end.Ticks < _minDate.Ticks)
                throw new ArgumentOutOfRangeException("End date provided is less than the minimum date.");

            if (start > end)
                end = start;

            TimeSpan span = end - start;
            if (span.Days >= _maxSelectionCount)
            {
                if (start.Ticks == _selectionStart.Ticks)
                    start = end.AddDays(1 - _maxSelectionCount);
                else
                    end = start.AddDays(_maxSelectionCount - 1);
            }

            SetSelRange(start, end);
        }

        /// <summary>
        /// Gets the focus day.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? FocusDay 
        {
            get { return _focusDay; }
            set { _focusDay = value; }
        }

        /// <summary>
        /// Update usage of bolded overrides.
        /// </summary>
        /// <param name="bolded">New bolded state.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetBoldedOverride(bool bolded)
        {
            _boldedDisabled.Apply = bolded;
            _boldedNormal.Apply = bolded;
            _boldedTracking.Apply = bolded;
            _boldedPressed.Apply = bolded;
            _boldedCheckedNormal.Apply = bolded;
            _boldedCheckedTracking.Apply = bolded;
            _boldedCheckedPressed.Apply = bolded;
        }

        /// <summary>
        /// Update usage of today overrides.
        /// </summary>
        /// <param name="today">New today state.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetTodayOverride(bool today)
        {
            _todayDisabled.Apply = today;
            _todayNormal.Apply = today;
            _todayTracking.Apply = today;
            _todayPressed.Apply = today;
            _todayCheckedNormal.Apply = today;
            _todayCheckedTracking.Apply = today;
            _todayCheckedPressed.Apply = today;
        }

        /// <summary>
        /// Update usage of focus overrides.
        /// </summary>
        /// <param name="focus">Should show focus.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetFocusOverride(bool focus)
        {
            _overrideDisabled.Apply = _hasFocus && focus;
            _overrideNormal.Apply = _hasFocus && focus;
            _overrideTracking.Apply = _hasFocus && focus;
            _overridePressed.Apply = _hasFocus && focus;
            _overrideCheckedNormal.Apply = _hasFocus && focus;
            _overrideCheckedTracking.Apply = _hasFocus && focus;
            _overrideCheckedPressed.Apply = _hasFocus && focus;
        }

        /// <summary>
        /// Gets a delegate for creating tool strip renderers.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public GetToolStripRenderer GetToolStripDelegate 
        {
            get { return new GetToolStripRenderer(CreateToolStripRenderer); }
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool DesignerGetHitTest(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return false;

            // Check if any of the button specs want the point
            if ((_drawMonths != null) && _drawMonths.ButtonManager.DesignerGetHitTest(pt))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public Component DesignerComponentFromPoint(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return null;

            // Ask the current view for a decision
            return ViewManager.ComponentFromPoint(pt);
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public void DesignerMouseLeave()
        {
            // Simulate the mouse leaving the control so that the tracking
            // element that thinks it has the focus is informed it does not
            OnMouseLeave(EventArgs.Empty);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Processes a notification from palette storage of a button spec change.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnButtonSpecChanged(object sender, EventArgs e)
        {
            // Recreate all the button specs with new values
            _drawMonths.RecreateButtons();

            // Let base class perform standard processing
            base.OnButtonSpecChanged(sender, e);
        }

        /// <summary>
        /// Determines if a character is an input character that the control recognizes.
        /// </summary>
        /// <param name="charCode">The character to test.</param>
        /// <returns>true if the character should be sent directly to the control and not preprocessed; otherwise, false.</returns>
        protected override bool IsInputChar(char charCode)
        {
            // We take all regular input characters
            return char.IsLetterOrDigit(charCode);
        }
        
        /// <summary>
        /// Determines whether the specified key is a regular input key or a special key that requires preprocessing.
        /// </summary>
        /// <param name="keyData">One of the Keys values.</param>
        /// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData & ~Keys.Shift)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }

            return base.IsInputKey(keyData);
        }

        /// <summary>
        /// Raises the KeyDown event.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing)
                if (_drawMonths.ProcessKeyDown(this, e))
                    return;

            // Let base class fire events
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Raises when the DateChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnDateChanged(DateRangeEventArgs e)
        {
            if (DateChanged != null)
                DateChanged(this, e);
        }

        /// <summary>
        /// Raises when the SelectionStartChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnSelectionStartChanged(EventArgs e)
        {
            if (SelectionStartChanged != null)
                SelectionStartChanged(this, e);
        }

        /// <summary>
        /// Raises when the SelectionEndChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnSelectionEndChanged(EventArgs e)
        {
            if (SelectionEndChanged != null)
                SelectionEndChanged(this, e);
        }

        /// <summary>
        /// Raises when the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            // Ensure there is a defined focus day
            SetFocusDay();

            // Apply the focus overrides
            UpdateFocusOverride(true);

            // Change in focus requires a repaint
            PerformNeedPaint(false);

            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises when the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            // Apply the focus overrides
            UpdateFocusOverride(false);

            // Change in focus requires a repaint
            PerformNeedPaint(false);

            base.OnLostFocus(e);
        }

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">An PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Paint != null)
                Paint(this, e);

            base.OnPaint(e);
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            if (Click != null)
                Click(this, e);

            base.OnClick(e);
        }

        /// <summary>
        /// Raises the DoubleClick event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnDoubleClick(EventArgs e)
        {
            if (DoubleClick != null)
                DoubleClick(this, e);

            base.OnDoubleClick(e);
        }

        /// <summary>
        /// Raises the TextChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);

            base.OnTextChanged(e);
        }

        /// <summary>
        /// Raises the ForeColorChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnForeColorChanged(EventArgs e)
        {
            if (ForeColorChanged != null)
                ForeColorChanged(this, e);

            base.OnForeColorChanged(e);
        }

        /// <summary>
        /// Raises the FontChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnFontChanged(EventArgs e)
        {
            if (FontChanged != null)
                FontChanged(this, e);

            base.OnFontChanged(e);
        }

        /// <summary>
        /// Raises the BackgroundImageChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            if (BackgroundImageChanged != null)
                BackgroundImageChanged(this, e);

            base.OnBackgroundImageChanged(e);
        }

        /// <summary>
        /// Raises the BackgroundImageLayoutChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnBackgroundImageLayoutChanged(EventArgs e)
        {
            if (BackgroundImageLayoutChanged != null)
                BackgroundImageLayoutChanged(this, e);

            base.OnBackgroundImageLayoutChanged(e);
        }

        /// <summary>
        /// Raises the BackColorChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            if (BackColorChanged != null)
                BackColorChanged(this, e);

            base.OnBackColorChanged(e);
        }

        /// <summary>
        /// Raises the PaddingChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnPaddingChanged(EventArgs e)
        {
            if (PaddingChanged != null)
                PaddingChanged(this, e);

            base.OnPaddingChanged(e);
        }

        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Update view elements
            _drawDocker.Enabled = Enabled;
            _drawMonths.Enabled = Enabled;

            // Change in enabled state requires a layout and repaint
            PerformNeedPaint(true);

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing)
            {
                // Find the correct size for the control
                int width = Width;
                int height = Height;
                AdjustSize(ref width, ref height);

                // If the current size is not correct then change now
                if ((width != Width) || (height != Height))
                    Size = new Size(width, height);
            }

            // Let base class layout child controls
            base.OnLayout(levent);
        }

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new Left property value of the control.</param>
        /// <param name="y">The new Top property value of the control.</param>
        /// <param name="width">The new Width property value of the control.</param>
        /// <param name="height">The new Height property value of the control.</param>
        /// <param name="specified">A bitwise combination of the BoundsSpecified values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Rectangle bounds = base.Bounds;
            AdjustSize(ref width, ref height);
            base.SetBoundsCore(x, y, width, height, specified);
        }
        #endregion

        #region Private
        private DateTime EffectiveMaxDate(DateTime maxDate)
        {
            DateTime maximumDateTime = DateTimePicker.MaximumDateTime;
            if (maxDate > maximumDateTime)
                return maximumDateTime;
            else
                return maxDate;
        }

        private DateTime EffectiveMinDate(DateTime minDate)
        {
            DateTime minimumDateTime = DateTimePicker.MinimumDateTime;
            if (minDate < minimumDateTime)
                return minimumDateTime;
            else
                return minDate;
        }

        private void AdjustSize(ref int width, ref int height)
        {
            using (ViewLayoutContext context = new ViewLayoutContext(this, Renderer))
            {
                // Ask back/border the size it requires
                Size backBorderSize = _drawDocker.GetNonChildSize(context);

                // Ask for the size needed to draw a single month
                Size singleMonthSize = _drawMonths.GetSingleMonthSize(context);

                // How many full months can be fit in each dimension (with a minimum of 1 month showing)
                int gap = ViewLayoutMonths.GAP;
                int widthMonths = Math.Max(1, (width - backBorderSize.Width - gap) / (singleMonthSize.Width + gap));
                int heightMonths = Math.Max(1, (height - backBorderSize.Height - gap) / (singleMonthSize.Height + gap));

                // Calculate new sizes based on showing only full months
                width = backBorderSize.Width + (widthMonths * singleMonthSize.Width) + (gap * (widthMonths + 1));
                height = backBorderSize.Height + (heightMonths * singleMonthSize.Height) + (gap * (heightMonths + 1));

                // Ask the month layout for size of extra areas such as headers etc
                Size extraSize = _drawMonths.GetExtraSize(context);
                width += extraSize.Width;
                height += extraSize.Height;

                // Update the calendar dimensions to match the actual size
                CalendarDimensions = new Size(widthMonths, heightMonths);
            }
        }

        private void SetRange()
        {
            bool startChanged = false;
            bool endChanged = false;

            DateTime minDate = EffectiveMinDate(_minDate);
            DateTime maxDate = EffectiveMaxDate(_maxDate);

            if (_selectionStart < minDate)
            {
                _selectionStart = minDate.Date;
                startChanged = true;
            }

            if (_selectionStart > maxDate)
            {
                _selectionStart = maxDate.Date;
                startChanged = true;
            }

            if (_selectionEnd < minDate)
            {
                _selectionEnd = minDate.Date;
                endChanged = true;
            }

            if (_selectionEnd > maxDate)
            {
                _selectionEnd = maxDate.Date;
                endChanged = true;
            }

            PerformNeedPaint(true);

            if (startChanged)
                OnSelectionStartChanged(EventArgs.Empty);

            if (endChanged)
                OnSelectionEndChanged(EventArgs.Empty);

            if (startChanged || endChanged)
                OnDateChanged(new DateRangeEventArgs(_selectionStart, _selectionEnd));

            SetFocusDay();
        }

        private void SetSelRange(DateTime lower, DateTime upper)
        {
            bool startChanged = false;
            bool endChanged = false;

            if (lower != _selectionStart)
            {
                _selectionStart = lower;
                startChanged = true;
            }

            if (upper != _selectionEnd)
            {
                _selectionEnd = upper;
                endChanged = true;
            }

            PerformNeedPaint(true);

            if (startChanged)
                OnSelectionStartChanged(EventArgs.Empty);

            if (endChanged)
                OnSelectionEndChanged(EventArgs.Empty);

            if (startChanged || endChanged)
                OnDateChanged(new DateRangeEventArgs(_selectionStart, _selectionEnd));

            SetFocusDay();
        }

        private void SetFocusDay()
        {
            if (_focusDay == null)
                _focusDay = SelectionStart.Date;
            else
            {
                if (_focusDay.Value < SelectionStart)
                    _focusDay = SelectionStart.Date;
                else if (_focusDay.Value > SelectionStart)
                    _focusDay = SelectionEnd.Date;
            }
        }

        private void UpdateFocusOverride(bool focus)
        {
            _hasFocus = focus;
        }
        #endregion
    }
}
