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
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Allow user to select a date using a visual monthly calendar display.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonContextMenuMonthCalendar), "ToolboxBitmaps.KryptonMonthCalendar.bmp")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("DateChanged")]
    [DefaultProperty("SelectionRange")]
    public class KryptonContextMenuMonthCalendar : KryptonContextMenuItemBase
    {
        #region Static Fields
        private static readonly string _defaultToday = "Today:";
        #endregion

        #region Instance Fields
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
        private Day _firstDayOfWeek;
        private Size _dimensions;
        private string _todayFormat;
        private bool _autoClose;
        private bool _enabled;
        private bool _hasFocus;
        private bool _showWeekNumbers;
        private bool _showTodayCircle;
        private bool _showToday;
        private bool _closeOnTodayClick;
        private DateTime _selectionStart;
        private DateTime _selectionEnd;
        private DateTime _minDate;
        private DateTime _maxDate;
        private DateTime _todayDate;
        private DateTime? _focusDay;
        private int _maxSelectionCount;
        private DateTimeList _annualDates;
        private DateTimeList _monthlyDates;
        private DateTimeList _dates;
        private int _monthlyDays;
        private int[] _annualDays;
        private string _today;
        private int _scrollChange;
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
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuMonthCalendar class.
        /// </summary>
        public KryptonContextMenuMonthCalendar()
        {
            // Default fields
            _autoClose = true;
            _enabled = true;
            _showToday = true;
            _showTodayCircle = true;
            _closeOnTodayClick = false;
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
            _today = _defaultToday;
            _todayFormat = "d";

            // Create the common/override state storage
            _stateCommon = new PaletteMonthCalendarRedirect();
            _stateFocus = new PaletteMonthCalendarStateRedirect();
            _stateBolded = new PaletteMonthCalendarStateRedirect();
            _stateToday = new PaletteMonthCalendarStateRedirect();

            // Basic state storage
            _stateDisabled = new PaletteMonthCalendarDoubleState(_stateCommon);
            _stateNormal = new PaletteMonthCalendarDoubleState(_stateCommon);
            _stateTracking = new PaletteMonthCalendarState(_stateCommon);
            _statePressed = new PaletteMonthCalendarState(_stateCommon);
            _stateCheckedNormal = new PaletteMonthCalendarState(_stateCommon);
            _stateCheckedTracking = new PaletteMonthCalendarState(_stateCommon);
            _stateCheckedPressed = new PaletteMonthCalendarState(_stateCommon);

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
        }

        /// <summary>
        /// Returns a description of the instance.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return "(Month Calendar)";
        }
        #endregion

        #region Public
        /// <summary>
        /// Returns the number of child menu items.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int ItemChildCount 
        {
            get { return 0; }
        }

        /// <summary>
        /// Returns the indexed child menu item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override KryptonContextMenuItemBase this[int index]
        {
            get { return null; }
        }

        /// <summary>
        /// Test for the provided shortcut and perform relevant action if a match is found.
        /// </summary>
        /// <param name="keyData">Key data to check against shorcut definitions.</param>
        /// <returns>True if shortcut was handled, otherwise false.</returns>
        public override bool ProcessShortcut(Keys keyData)
        {
            return false;
        }

        /// <summary>
        /// Returns a view appropriate for this item based on the object it is inside.
        /// </summary>
        /// <param name="provider">Provider of context menu information.</param>
        /// <param name="parent">Owning object reference.</param>
        /// <param name="columns">Containing columns.</param>
        /// <param name="standardStyle">Draw items with standard or alternate style.</param>
        /// <param name="imageColumn">Draw an image background for the item images.</param>
        /// <returns>ViewBase that is the root of the view hierachy being added.</returns>
        public override ViewBase GenerateView(IContextMenuProvider provider,
                                              object parent,
                                              ViewLayoutStack columns,
                                              bool standardStyle,
                                              bool imageColumn)
        {
            return new ViewDrawMenuMonthCalendar(provider, this);
        }

        /// <summary>
        /// Gets and sets if selecting a day automatically closes the context menu.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates if selecting a day closes the context menu.")]
        [DefaultValue(true)]
        public bool AutoClose
        {
            get { return _autoClose; }

            set
            {
                if (_autoClose != value)
                {
                    _autoClose = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("AutoClose"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if clicking the Today button closes the drop down menu.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates if clicking the Today button closes the drop down menu.")]
        [DefaultValue(false)]
        public bool CloseOnTodayClick
        {
            get { return _closeOnTodayClick; }

            set
            {
                if (_closeOnTodayClick != value)
                {
                    _closeOnTodayClick = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CloseOnTodayClick"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if the radio button is enabled.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates whether the month calendar is enabled.")]
        [DefaultValue(true)]
        [Bindable(true)]
        public bool Enabled
        {
            get { return _enabled; }

            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Enabled"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of months to scroll when next/prev buttons are used.
        /// </summary>
        [KryptonPersist]
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
                OnPropertyChanged(new PropertyChangedEventArgs("ScrollChange"));
            }
        }

        /// <summary>
        /// Gets or sets today's date.
        /// </summary>
        [KryptonPersist]
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
                OnPropertyChanged(new PropertyChangedEventArgs("TodayDate"));
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
        [KryptonPersist]
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

                OnPropertyChanged(new PropertyChangedEventArgs("AnnuallyBoldedDates"));
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
        [KryptonPersist]
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

                OnPropertyChanged(new PropertyChangedEventArgs("MonthlyBoldedDates"));
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
        [KryptonPersist]
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
                OnPropertyChanged(new PropertyChangedEventArgs("BoldedDates"));
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
        /// Gets or sets the minimum allowable date.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Minimum allowable date.")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime MinDate
        {
            get { return _minDate; }

            set
            {
                if (value != _minDate)
                {
                    if (value > DateTimePicker.MaximumDateTime)
                        throw new ArgumentOutOfRangeException("Date provided is greater than the maximum culture supported date.");

                    if (value < DateTimePicker.MinimumDateTime)
                        throw new ArgumentOutOfRangeException("Date provided is less than the minimum culture supported date.");
                }

                _minDate = value;
                SetRange();
                OnPropertyChanged(new PropertyChangedEventArgs("MinDate"));
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
        /// Gets or sets the maximum allowable date.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Maximum allowable date.")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime MaxDate
        {
            get { return _maxDate; }

            set
            {
                if (value != _maxDate)
                {
                    if (value > DateTimePicker.MaximumDateTime)
                        throw new ArgumentOutOfRangeException("Date provided is greater than the maximum culture supported date.");

                    if (value < DateTimePicker.MinimumDateTime)
                        throw new ArgumentOutOfRangeException("Date provided is less than the minimum culture supported date.");
                }

                _maxDate = value;
                SetRange();
                OnPropertyChanged(new PropertyChangedEventArgs("MaxDate"));
            }
        }

        private void ResetMaxDate()
        {
            MaxDate = DateTimePicker.MaximumDateTime;
        }

        private bool ShouldSerializeMaxDate()
        {
            return (_maxDate != DateTimePicker.MaximumDateTime);
        }

        /// <summary>
        /// Gets or sets the maximum number of days that can be selected in a month calendar control.
        /// </summary>
        [KryptonPersist]
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
                    OnPropertyChanged(new PropertyChangedEventArgs("MaxSelectionCount"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the start date of the selected range of dates.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Start date of the selected range of dates.")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
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

                    // End date cannot be before the start date
                    if (_selectionEnd < value)
                        _selectionEnd = value;

                    // Limit the selection range to the maximum selection count
                    TimeSpan range = _selectionEnd - value;
                    if (range.Days >= _maxSelectionCount)
                        _selectionEnd = value.AddDays(_maxSelectionCount - 1);

                    // Update selection dates and generate event if required
                    SetSelRange(value, _selectionEnd);
                    OnPropertyChanged(new PropertyChangedEventArgs("SelectionStart"));
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
        [KryptonPersist]
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

                    // Start date cannot be after the end date
                    if (_selectionStart > value)
                        _selectionStart = value;

                    // Limit the selection range to the maximum selection count
                    TimeSpan range = value - _selectionStart;
                    if (range.Days >= _maxSelectionCount)
                        _selectionStart = value.AddDays(1 - _maxSelectionCount);

                    // Update selection dates and generate event if required
                    SetSelRange(_selectionStart, value);
                    OnPropertyChanged(new PropertyChangedEventArgs("SelectionEnd"));
                }
            }
        }

        private void ResetSelectionEnd()
        {
            SelectionEnd = DateTime.Now.Date;
        }

        private bool ShouldSerializeSelectionEnd()
        {
            return (SelectionStart != DateTime.Now.Date);
        }

        /// <summary>
        /// Gets or sets the selected range of dates for a month calendar control.
        /// </summary>
        [KryptonPersist]
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
                    OnPropertyChanged(new PropertyChangedEventArgs("TodayFormat"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the label text for todays text. 
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Text used as label for todays date.")]
        [DefaultValue("Today:")]
        [Localizable(true)]
        public string TodayText
        {
            get { return _today; }

            set
            {
                if (value == null)
                    value = _defaultToday;

                _today = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TodayText"));
            }
        }

        private void ResetTodayText()
        {
            TodayText = _defaultToday;
        }

        /// <summary>
        /// Gets or sets the number of columns and rows of months displayed. 
        /// </summary>
        [KryptonPersist]
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
                    OnPropertyChanged(new PropertyChangedEventArgs("CalendarDimensions"));
                }
            }
        }

        /// <summary>
        /// First day of the week.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("First day of the week.")]
        [Localizable(true)]
        public Day FirstDayOfWeek
        {
            get { return _firstDayOfWeek; }

            set
            {
                if (_firstDayOfWeek != value)
                {
                    _firstDayOfWeek = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("FirstDayOfWeek"));
                }
            }
        }

        private void ResetFirstDayOfWeek()
        {
            FirstDayOfWeek = Day.Default;
        }

        private bool ShouldSerializeFirstDayOfWeek()
        {
            return (FirstDayOfWeek != Day.Default);
        }

        /// <summary>
        /// Gets and sets if the control will display todays date.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates whether this month calendar will display todays date.")]
        [DefaultValue(true)]
        public bool ShowToday
        {
            get { return _showToday; }

            set
            {
                if (_showToday != value)
                {
                    _showToday = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ShowToday"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if the control will circle the today date.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates whether this month calendar will circle the today date.")]
        [DefaultValue(true)]
        public bool ShowTodayCircle
        {
            get { return _showTodayCircle; }

            set
            {
                if (_showTodayCircle != value)
                {
                    _showTodayCircle = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ShowTodayCircle"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if week numbers to the left of each row.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates whether this month calendar will display week numbers to the left of each row.")]
        [DefaultValue(false)]
        public bool ShowWeekNumbers
        {
            get { return _showWeekNumbers; }

            set
            {
                if (_showWeekNumbers != value)
                {
                    _showWeekNumbers = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ShowWeekNumbers"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the header style for the month calendar.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Header style for the month calendar.")]
        [DefaultValue(typeof(HeaderStyle), "Calendar")]
        public HeaderStyle HeaderStyle
        {
            get { return _headerStyle; }

            set
            {
                if (_headerStyle != value)
                {
                    _headerStyle = value;
                    _stateCommon.Header.SetStyles(_headerStyle);
                    OnPropertyChanged(new PropertyChangedEventArgs("HeaderStyle"));
                }
            }
        }

        private bool ShouldSerializeHeaderStyle()
        {
            return (_headerStyle != HeaderStyle.Calendar);
        }

        /// <summary>
        /// Gets and sets the content style for the day entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Content style for the day entries.")]
        [DefaultValue(typeof(ButtonStyle), "Calendar Day")]
        public ButtonStyle DayStyle
        {
            get { return _dayStyle; }

            set
            {
                if (_dayStyle != value)
                {
                    _dayStyle = value;
                    _stateCommon.DayStyle = value;
                    _stateFocus.DayStyle = value;
                    _stateBolded.DayStyle = value;
                    _stateToday.DayStyle = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("DayStyle"));
                }
            }
        }

        private bool ShouldSerializeDayStyle()
        {
            return (_dayStyle != ButtonStyle.CalendarDay);
        }

        private void ResetDayStyle()
        {
            DayStyle = ButtonStyle.CalendarDay;
        }

        /// <summary>
        /// Gets and sets the content style for the day of week labels.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Content style for the day of week labels.")]
        [DefaultValue(typeof(ButtonStyle), "CalendarDay")]
        public ButtonStyle DayOfWeekStyle
        {
            get { return _dayOfWeekStyle; }

            set
            {
                if (_dayOfWeekStyle != value)
                {
                    _dayOfWeekStyle = value;
                    _stateCommon.DayOfWeekStyle = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("DayOfWeekStyle"));
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
        /// Gets access to the day appearance when it has focus.
        /// </summary>
        [KryptonPersist]
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
        [KryptonPersist]
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
        [KryptonPersist]
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
        [KryptonPersist]
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
        [KryptonPersist]
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
        [KryptonPersist]
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
        [KryptonPersist]
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
        [KryptonPersist]
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
        [KryptonPersist]
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
        [KryptonPersist]
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
        [KryptonPersist]
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
        /// Adds a day that is displayed in bold on an annual basis in the month calendar.
        /// </summary>
        /// <param name="date">The date to be displayed in bold.</param>
        public void AddAnnuallyBoldedDate(DateTime date)
        {
            if (!_annualDates.Contains(date))
            {
                _annualDates.Add(date);
                _annualDays[date.Month - 1] |= 1 << (date.Day - 1);
                OnPropertyChanged(new PropertyChangedEventArgs("AnnuallyBoldedDates"));
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
                OnPropertyChanged(new PropertyChangedEventArgs("BoldedDates"));
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
                OnPropertyChanged(new PropertyChangedEventArgs("MonthlyBoldedDates"));
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
            OnPropertyChanged(new PropertyChangedEventArgs("AnnuallyBoldedDates"));
        }

        /// <summary>
        /// Removes all the nonrecurring bold dates.
        /// </summary>
        public void RemoveAllBoldedDates()
        {
            _dates.Clear();
            OnPropertyChanged(new PropertyChangedEventArgs("BoldedDates"));
        }

        /// <summary>
        /// Removes all the monthly bold dates.
        /// </summary>
        public void RemoveAllMonthlyBoldedDates()
        {
            _monthlyDates.Clear();
            _monthlyDays = 0;
            OnPropertyChanged(new PropertyChangedEventArgs("MonthlyBoldedDates"));
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
        /// Sets the use of the focus override.
        /// </summary>
        /// <param name="focus">New focus state.</param>
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
        /// Gets and sets if the item has the focus.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasFocus
        {
            get { return _hasFocus; }
            set { _hasFocus = value; }
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
        #endregion

        #region Protected
        /// <summary>
        /// Raises when the DateChanged event.
        /// </summary>
        /// <param name="e">An DateRangeEventArgs that contains the event data.</param>
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
        #endregion

        #region Internal
        internal void SetPaletteRedirect(PaletteRedirect redirector)
        {
            _stateCommon.SetRedirector(redirector);
            _stateFocus.SetRedirector(redirector);
            _stateBolded.SetRedirector(redirector);
            _stateToday.SetRedirector(redirector);
        }
        #endregion

        #region Implementation
        private void SetRange()
        {
            bool startChanged = false;
            bool endChanged = false;

            if (_selectionStart < _minDate)
            {
                _selectionStart = _minDate.Date;
                startChanged = true;
            }

            if (_selectionStart > _maxDate)
            {
                _selectionStart = _maxDate.Date;
                startChanged = true;
            }

            if (_selectionEnd < _minDate)
            {
                _selectionEnd = _minDate.Date;
                endChanged = true;
            }

            if (_selectionEnd > _maxDate)
            {
                _selectionEnd = _maxDate.Date;
                endChanged = true;
            }

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
        #endregion
    }
}
