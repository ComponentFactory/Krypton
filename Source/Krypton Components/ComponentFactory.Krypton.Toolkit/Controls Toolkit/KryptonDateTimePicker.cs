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
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Represents a Windows control that allows the user to select a date and a time and to display the date and time with a specified format.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonDateTimePicker), "ToolboxBitmaps.KryptonDateTimePicker.bmp")]
    [DefaultEvent("ValueChanged")]
	[DefaultProperty("Value")]
    [DefaultBindingProperty("Value")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonDateTimePickerDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Enables the user to select a date and time, and to display that date and time in a specified format.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonDateTimePicker : VisualControlBase, 
                                         IContentValues
	{
        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAny instances.
        /// </summary>
        public class DateTimePickerButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the DateTimePickerButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public DateTimePickerButtonSpecCollection(KryptonDateTimePicker owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Static Fields
        private static readonly string _defaultToday = "Today:";
        #endregion
        
        #region Instance Fields
        private ViewDrawDocker _drawDockerOuter;
        private ViewLayoutDocker _drawDockerInner;
        private ViewLayoutStretch _dropStretch;
        private ViewLayoutFit _upDownFit;
        private PaletteTripleToPalette _paletteDropDown;
        private PaletteTripleToPalette _paletteUpDown;
        private ViewDrawDateTimeButton _buttonDropDown;
        private ViewDrawDateTimeButton _buttonUp;
        private ViewDrawDateTimeButton _buttonDown;
        private ViewDrawDateTimeText _drawText;
        private ViewDrawCheckBox _drawCheckBox;
        private ViewLayoutCenter _layoutCheckBox;
        private CheckBoxImages _checkBoxImages;
        private PaletteInputControlTripleRedirect _stateCommon;
        private PaletteInputControlTripleStates _stateDisabled;
        private PaletteInputControlTripleStates _stateNormal;
        private PaletteInputControlTripleStates _stateActive;
        private DateTimePickerButtonSpecCollection _buttonSpecs;
        private ButtonSpecManagerDraw _buttonManager;
        private VisualPopupToolTip _visualPopupToolTip;
        private ToolTipManager _toolTipManager;
        private KryptonContextMenuMonthCalendar _kmc;
        private InputControlStyle _inputControlStyle;
        private ButtonStyle _upDownButtonStyle;
        private ButtonStyle _dropButtonStyle;
        private HeaderStyle _headerStyle;
        private ButtonStyle _dayStyle;
        private ButtonStyle _dayOfWeekStyle;
        private Nullable<bool> _fixedActive;
        private DateTimePickerFormat _format;
        private LeftRightAlignment _dropDownAlign;
        private DateTime _maxDateTime;
        private DateTime _minDateTime;
        private DateTime _dateTime;
        private DateTime _todayDate;
        private Size _dimensions;
        private DateTimeList _annualDates;
        private DateTimeList _monthlyDates;
        private DateTimeList _dates;
        private string _customFormat;
        private string _todayFormat;
        private string _today;
        private string _customNullText;
        private string _lastActiveFragment;
        private bool _inRibbonDesignMode;
        private bool _allowButtonSpecToolTips;
        private bool _autoShift;
        private bool _showAdornments;
        private bool _showUpDown;
        private bool _showCheckBox;
        private bool _showWeekNumbers;
        private bool _showTodayCircle;
        private bool _showToday;
        private bool _mouseOver;
        private bool _dropped;
        private bool _alwaysActive;
        private bool _userSetDateTime;
        private bool _dropDownMonthChanged;
        private bool _closeOnTodayClick;
        private object _rawDateTime;
        private Day _firstDayOfWeek;
        private int _cachedHeight;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the Value property has changed value.
        /// </summary>
        [Category("Action")]
        [Description("Event raised when the value of the Value property is changed on KryptonDateTimePicker.")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Occurs when the ValueNullable property has changed value.
        /// </summary>
        [Category("Action")]
        [Description("Event raised when the value of the ValueNullable property is changed on KryptonDateTimePicker.")]
        public event EventHandler ValueNullableChanged;

        /// <summary>
        /// Occurs when the Value property has changed value.
        /// </summary>
        [Category("Action")]
        [Description("Event raised when the value of the ActiveFragment property is changed on KryptonDateTimePicker.")]
        public event EventHandler ActiveFragmentChanged;

        /// <summary>
        /// Occurs when the drop down is shown.
        /// </summary>
        [Category("Action")]
        [Description("Event raised when the drop down is shown.")]
        public event EventHandler<DateTimePickerDropArgs> DropDown;

        /// <summary>
        /// Occurs when the drop down has been closed.
        /// </summary>
        [Category("Action")]
        [Description("Event raised when the drop down has been closed.")]
        public event EventHandler<DateTimePickerCloseArgs> CloseUp;

        /// <summary>
        /// Occurs when the month calendar date changed whilst dropped down.
        /// </summary>
        [Category("Action")]
        [Description("Event raised to indicate the month calendar date changed whilst dropped down.")]
        public event EventHandler CloseUpMonthCalendarChanged;

        /// <summary>
        /// Occurs when auto shifting to the next field but overflowing the end.
        /// </summary>
        [Category("Action")]
        [Description("Event raised when auto shifting to the next field but overflowing the end.")]
        public event CancelEventHandler AutoShiftOverflow;

        /// <summary>
        /// Occurs when the Checked property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Event raised when the value of the Checked property is changed on KryptonDateTimePicker.")]
        public event EventHandler CheckedChanged;

        /// <summary>
        /// Occurs when the Format property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Event raised when the value of the Format property is changed on KryptonDateTimePicker.")]
        public event EventHandler FormatChanged;

        /// <summary>
        /// Occurs when the RightToLeftLayout property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Event raised when the value of the RightToLeftLayout property is changed on KryptonDateTimePicker.")]
        public event EventHandler RightToLeftLayoutChanged;
        #endregion

        #region Identity
        /// <summary>
		/// Initialize a new instance of the KryptonHeader class.
		/// </summary>
		public KryptonDateTimePicker()
		{
            // We are a fixed height determined by the text and button specs
            SetStyle(ControlStyles.FixedHeight, true);

            // Set default values
            _cachedHeight = -1;
            _alwaysActive = true;
            _showUpDown = false;
            _autoShift = false;
            _showAdornments = true;
            _showCheckBox = false;
            _dropped = false;
            _mouseOver = false;
            _allowButtonSpecToolTips = false;
            _showToday = true;
            _showTodayCircle = true;
            _closeOnTodayClick = false;
            _userSetDateTime = false;
            _customFormat = string.Empty;
            _customNullText = string.Empty;
            _todayFormat = "d";
            _dateTime = DateTime.Now;
            _rawDateTime = _dateTime;
            _todayDate = DateTime.Now.Date;
            _maxDateTime = DateTime.MaxValue;
            _minDateTime = DateTime.MinValue;
            _dropDownAlign = LeftRightAlignment.Left;
            _format = DateTimePickerFormat.Long;
            _inputControlStyle = InputControlStyle.Standalone;
            _upDownButtonStyle = ButtonStyle.InputControl;
            _dropButtonStyle = ButtonStyle.InputControl;
            _headerStyle = HeaderStyle.Calendar;
            _dayStyle = ButtonStyle.CalendarDay;
            _dayOfWeekStyle = ButtonStyle.CalendarDay;
            _dimensions = new Size(1, 1);
            _today = _defaultToday;
            _firstDayOfWeek = Day.Default;
            _annualDates = new DateTimeList();
            _monthlyDates = new DateTimeList();
            _dates = new DateTimeList();
            
			// Create storage objects
            _buttonSpecs = new DateTimePickerButtonSpecCollection(this);

			// Create the palette storage
            _stateCommon = new PaletteInputControlTripleRedirect(Redirector, PaletteBackStyle.InputControlStandalone, PaletteBorderStyle.InputControlStandalone, PaletteContentStyle.InputControlStandalone, NeedPaintDelegate);
            _stateDisabled = new PaletteInputControlTripleStates(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteInputControlTripleStates(_stateCommon, NeedPaintDelegate);
            _stateActive = new PaletteInputControlTripleStates(_stateCommon, NeedPaintDelegate);

            // Add a checkbox to the left of the text area
            _checkBoxImages = new CheckBoxImages(NeedPaintDelegate);
            PaletteRedirectCheckBox paletteCheckBoxImages = new PaletteRedirectCheckBox(Redirector, _checkBoxImages);
            _drawCheckBox = new ViewDrawCheckBox(paletteCheckBoxImages);
            _drawCheckBox.CheckState = CheckState.Checked;
            _layoutCheckBox = new ViewLayoutCenter();
            _layoutCheckBox.Add(new ViewLayoutPadding(new Padding(1, 1, 4, 1), _drawCheckBox));
            _layoutCheckBox.Visible = false;

            // Need a controller for handling check box mouse input
            CheckBoxController controller = new CheckBoxController(_drawCheckBox, _drawCheckBox, NeedPaintDelegate);
            controller.Click += new EventHandler(OnCheckBoxClick);
            controller.Enabled = true;
            _drawCheckBox.MouseController = controller;
            _drawCheckBox.KeyController = controller;

            // Draws the text content of the control
            _drawText = new ViewDrawDateTimeText(this, NeedPaintDelegate);

            // Create inner view for placing inside the drawing docker
            _drawDockerInner = new ViewLayoutDocker();
            _drawDockerInner.IgnoreRightToLeftLayout = true;
            _drawDockerInner.Add(_layoutCheckBox, ViewDockStyle.Left);
            _drawDockerInner.Add(_drawText, ViewDockStyle.Fill);

            // Get button triple values directly from the redirector
            _paletteDropDown = new PaletteTripleToPalette(Redirector, PaletteBackStyle.ButtonInputControl, PaletteBorderStyle.ButtonInputControl, PaletteContentStyle.ButtonInputControl);
            _paletteUpDown = new PaletteTripleToPalette(Redirector, PaletteBackStyle.ButtonInputControl, PaletteBorderStyle.ButtonInputControl, PaletteContentStyle.ButtonInputControl);

            // Create buttons for drawing the drop down and up/down buttons
            _buttonDropDown = new ViewDrawDateTimeButton(this, _paletteDropDown, new PaletteMetricRedirect(Redirector), this, ViewDrawDateTimeButton.DrawDateTimeGlyph.DropDownButton, NeedPaintDelegate, false);
            _buttonUp = new ViewDrawDateTimeButton(this, _paletteUpDown, new PaletteMetricRedirect(Redirector), this, ViewDrawDateTimeButton.DrawDateTimeGlyph.UpButton, NeedPaintDelegate, true);
            _buttonDown = new ViewDrawDateTimeButton(this, _paletteUpDown, new PaletteMetricRedirect(Redirector), this, ViewDrawDateTimeButton.DrawDateTimeGlyph.DownButton, NeedPaintDelegate, true);
            _buttonDropDown.Click += new EventHandler(OnDropDownClick);
            _buttonUp.Click += new EventHandler(OnUpClick);
            _buttonDown.Click += new EventHandler(OnDownClick);

            // Stretch the drop down button to be the height of the available area
            _dropStretch = new ViewLayoutStretch(Orientation.Vertical);
            _dropStretch.Add(_buttonDropDown);
            _drawDockerInner.Add(_dropStretch, ViewDockStyle.Right);

            // Fit the up/down buttons so they have an equal amount of the vertical space
            _upDownFit = new ViewLayoutFit(Orientation.Vertical);
            _upDownFit.Add(_buttonUp);
            _upDownFit.Add(_buttonDown);
            _upDownFit.Visible = false;
            _drawDockerInner.Add(_upDownFit, ViewDockStyle.Right);

            // Create view for the control border and background
            _drawDockerOuter = new ViewDrawDocker(_stateNormal.Back, _stateNormal.Border);
            _drawDockerOuter.Add(new ViewLayoutPadding(new Padding(2, 0, 1, 0), _drawDockerInner), ViewDockStyle.Fill);

			// Create the view manager instance
            ViewManager = new ViewManager(this, _drawDockerOuter);

            // Create button specification collection manager
            _buttonManager = new ButtonSpecManagerDraw(this, Redirector, _buttonSpecs, null,
                                                       new ViewDrawDocker[] { _drawDockerOuter },
                                                       new IPaletteMetric[] { _stateCommon },
                                                       new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetPrimary },
                                                       new PaletteMetricPadding[] { PaletteMetricPadding.HeaderButtonPaddingPrimary },
                                                       new GetToolStripRenderer(CreateToolStripRenderer),
                                                       NeedPaintDelegate);

            // Create the manager for handling tooltips
            _toolTipManager = new ToolTipManager();
            _toolTipManager.ShowToolTip += new EventHandler<ToolTipEventArgs>(OnShowToolTip);
            _toolTipManager.CancelToolTip += new EventHandler(OnCancelToolTip);
            _buttonManager.ToolTipManager = _toolTipManager;

            // Update alignment to match current RightToLeft settings
            UpdateForRightToLeft();
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remove any showing tooltip
                OnCancelToolTip(this, EventArgs.Empty);

                // Remember to pull down the manager instance
                _buttonManager.Destruct();
            }

            base.Dispose(disposing);
        }
		#endregion
         
		#region Public
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// Gets or sets the foreground color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public override string Text
        {
            get 
            {
                if ((ValueNullable == null) || (ValueNullable == DBNull.Value))
                    return string.Empty;
                else
                    return _drawText.ToString();  
            }

            set { }
        }

        /// <summary>
        /// Gets or sets the number of columns and rows of months displayed. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Specifies the number of rows and columns of months displayed.")]
        [DefaultValue(typeof(Size), "1,1")]
        [Localizable(true)]
        public Size CalendarDimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }

        /// <summary>
        /// Gets or sets the label text for todays text. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Text used as label for todays date.")]
        [DefaultValue("Today:")]
        [Localizable(true)]
        public string CalendarTodayText
        {
            get { return _today; }

            set
            {
                if (value == null)
                    value = _defaultToday;

                _today = value;
            }
        }

        /// <summary>
        /// Reset the value of the CalendarTodayText property.
        /// </summary>
        public void ResetCalendarTodayText()
        {
            CalendarTodayText = _defaultToday;
        }

        /// <summary>
        /// First day of the week.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("First day of the week.")]
        [DefaultValue(typeof(Day), "Default")]
        [Localizable(true)]
        public Day CalendarFirstDayOfWeek
        {
            get { return _firstDayOfWeek; }
            set { _firstDayOfWeek = value; }
        }

        /// <summary>
        /// Gets and sets if the control will display todays date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will display todays date.")]
        [DefaultValue(true)]
        public bool CalendarShowToday
        {
            get { return _showToday; }
            set { _showToday = value; }
        }

        /// <summary>
        /// Gets and sets if clicking the Today button closes the drop down menu.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates if clicking the Today button closes the drop down menu.")]
        [DefaultValue(false)]
        public bool CalendarCloseOnTodayClick
        {
            get { return _closeOnTodayClick; }
            set { _closeOnTodayClick = value; }
        }

        /// <summary>
        /// Gets and sets if the control will circle the today date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will circle the today date.")]
        [DefaultValue(true)]
        public bool CalendarShowTodayCircle
        {
            get { return _showTodayCircle; }
            set { _showTodayCircle = value; }
        }

        /// <summary>
        /// Gets and sets if week numbers to the left of each row.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will display week numbers to the left of each row.")]
        [DefaultValue(false)]
        public bool CalendarShowWeekNumbers
        {
            get { return _showWeekNumbers; }
            set { _showWeekNumbers = value; }
        }

        /// <summary>
        /// Gets or sets today's date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Today's date.")]
        public DateTime CalendarTodayDate
        {
            get { return _todayDate; }

            set
            {
                if (value == null)
                    value = DateTime.Now.Date;

                _todayDate = value;
            }
        }

        private void ResetCalendarTodayDate()
        {
            CalendarTodayDate = DateTime.Now.Date;
        }

        private bool ShouldSerializeCalendarTodayDate()
        {
            return (CalendarTodayDate != DateTime.Now.Date);
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which annual days are displayed in bold.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which annual dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarAnnuallyBoldedDates
        {
            get { return _annualDates.ToArray(); }

            set
            {
                if (value == null)
                    value = new DateTime[0];

                _annualDates.Clear();
                _annualDates.AddRange(value);
            }
        }

        /// <summary>
        /// Should the CalendarAnnuallyBoldedDates property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeCalendarAnnuallyBoldedDates()
        {
            return (_annualDates.Count > 0);
        }

        private void ResetCalendarAnnuallyBoldedDates()
        {
            CalendarAnnuallyBoldedDates = null;
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which monthly dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarMonthlyBoldedDates
        {
            get { return _monthlyDates.ToArray(); }

            set
            {
                if (value == null)
                    value = new DateTime[0];

                _monthlyDates.Clear();
                _monthlyDates.AddRange(value);
            }
        }

        /// <summary>
        /// Should the CalendarMonthlyBoldedDates property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeCalendarMonthlyBoldedDates()
        {
            return (_monthlyDates.Count > 0);
        }

        private void ResetCalendarMonthlyBoldedDates()
        {
            CalendarMonthlyBoldedDates = null;
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are displayed in bold.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarBoldedDates
        {
            get { return _dates.ToArray(); }

            set
            {
                if (value == null)
                    value = new DateTime[0];

                _dates.Clear();
                _dates.AddRange(value);
            }
        }

        /// <summary>
        /// Should the CalendarBoldedDates property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeCalendarBoldedDates()
        {
            return (_dates.Count > 0);
        }

        private void ResetCalendarBoldedDates()
        {
            CalendarBoldedDates = null;
        }

        /// <summary>
        /// Gets or sets the alignment of the drop-down calendar on the DateTimePicker control.
        /// </summary>
        [Category("Appearance")]
        [Description("Alignment of the drop-down calendar on the KryptonDateTimePicker control.")]
        [DefaultValue(typeof(LeftRightAlignment), "Left")]
        [Localizable(true)]
        public LeftRightAlignment DropDownAlign
        {
            get { return _dropDownAlign; }
            set { _dropDownAlign = value; }
        }

        /// <summary>
        /// Gets or sets the date/time value assigned to the control that can be null.
        /// </summary>
        [Category("Appearance")]
        [Description("Property for the date/time that can be null.")]
        [TypeConverter(typeof(DateTimeNullableConverter))]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        public object ValueNullable
        {
            get { return _rawDateTime; }

            set
            {
                // We only allow a null/DBNull (as a way of setting no value) or a DateTime value
                if ((value == null) || (value is DBNull) || (value is DateTime))
                {
                    // Only interested in changes of value
                    if (_rawDateTime != value)
                    {
                        _rawDateTime = value;
                        _userSetDateTime = true;

                        // Update the checkbox to reflect the value
                        if ((_rawDateTime == null) || (_rawDateTime is DBNull))
                        {
                            _rawDateTime = DBNull.Value;
                            _drawCheckBox.CheckState = CheckState.Unchecked;
                        }
                        else
                            _drawCheckBox.CheckState = CheckState.Checked;

                        // Do we need to update the date time value?
                        if ((value is DateTime) && (_dateTime != (DateTime)value))
                        {
                            _dateTime = (DateTime)value;
                            OnValueChanged(EventArgs.Empty);
                        }

                        OnValueNullableChanged(EventArgs.Empty);
                        PerformNeedPaint(true);
                    }
                }
                else
                    throw new ArgumentException("Value can only accept 'null', 'DBNull' or 'DateTime' values.");
            }
        }

        /// <summary>
        /// Should the ValueNullable property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeValueNullable()
        {
            return _userSetDateTime;
        }

        /// <summary>
        /// Reset value of the ValueNullable property.
        /// </summary>
        public void ResetValueNullable()
        {
            // Setting an explicit value means the check box should be set
            _drawCheckBox.CheckState = CheckState.Checked;

            // Set new values
            _userSetDateTime = false;
            _dateTime = DateTime.Now;
            _rawDateTime = _dateTime;

            // Generate events always
            OnValueChanged(EventArgs.Empty);
            OnValueNullableChanged(EventArgs.Empty);
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Gets or sets the date/time value assigned to the control..
        /// </summary>
        [Category("Appearance")]
        [Description("Property for the date/time.")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true)]
        public DateTime Value
        {
            get { return _dateTime; }

            set
            {
                // Setting an explicit value means the check box should be set
                _drawCheckBox.CheckState = CheckState.Checked;

                // Even if the value is the same as the current value we need to note
                // that it has been explicitly defined so we know it needs serializing
                _userSetDateTime = true;

                // Need to check if the raw value is DBNull as that indicates the control is currently
                // nulled and so setting this property means we always revert back to having a value
                if ((_dateTime != value) || (_rawDateTime == DBNull.Value))
                {
                    _dateTime = value;
                    _rawDateTime = value;
                    OnValueChanged(EventArgs.Empty);
                    OnValueNullableChanged(EventArgs.Empty);
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Should the Value property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeValue()
        {
            return false;
        }

        /// <summary>
        /// Reset value of the Value property.
        /// </summary>
        public void ResetValue()
        {
            // Setting an explicit value means the check box should be set
            _drawCheckBox.CheckState = CheckState.Checked;

            // Set new values
            _userSetDateTime = false;
            _dateTime = DateTime.Now;
            _rawDateTime = _dateTime;

            // Generate events always
            OnValueChanged(EventArgs.Empty);
            OnValueNullableChanged(EventArgs.Empty);
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Gets or sets the format of the date and time displayed in the control.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines whether dates and times are displayed using standard or custom formatting.")]
        [DefaultValue(typeof(DateTimePickerFormat), "Long")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public DateTimePickerFormat Format
        {
            get { return _format; }

            set
            {
                if (_format != value)
                {
                    _format = value;
                    PerformNeedPaint(true);
                    OnFormatChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the format of the date and time displayed in the control.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates whether the control layout is right-to-left when the RightToLeft property is True.")]
        [DefaultValue(false)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool RightToLeftLayout
        {
            get { return _drawText.RightToLeftLayout; }

            set
            {
                if (_drawText.RightToLeftLayout != value)
                {
                    _drawText.RightToLeftLayout = value;
                    UpdateForRightToLeft();
                    PerformNeedPaint(true);
                    OnRightToLeftLayoutChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value determining if keyboard input will automatically shift to the next input field.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if keyboard input will automatically shift to the next input field.")]
        [DefaultValue(false)]
        public bool AutoShift
        {
            get { return _autoShift; }
            set { _autoShift = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a spin button control (also known as an up-down control) is used to adjust the date/time value.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates whether a spin box rather than a drop-down calendar is displayed for modifying the control value.")]
        [DefaultValue(false)]
        public bool ShowUpDown
        {
            get { return _showUpDown; }

            set
            {
                if (_showUpDown != value)
                {
                    _showUpDown = value;
                    _upDownFit.Visible = value && _showAdornments;
                    _dropStretch.Visible = !value && _showAdornments;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Specifies whether to show the check box in the exception message box.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines whether a check box is displayed in the control. When the box is unchecked, no value is selected.")]
        [DefaultValue(false)]
        public bool ShowCheckBox
        {
            get { return _showCheckBox; }

            set
            {
                if (_showCheckBox != value)
                {
                    _showCheckBox = value;
                    _layoutCheckBox.Visible = value && _showAdornments;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether mnemonics will fire button spec buttons.
        /// </summary>
        [Category("Appearance")]
        [Description("Defines if mnemonic characters generate click events for button specs.")]
        [DefaultValue(true)]
        public bool UseMnemonic
        {
            get { return _buttonManager.UseMnemonic; }

            set
            {
                if (_buttonManager.UseMnemonic != value)
                {
                    _buttonManager.UseMnemonic = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum date and time that can be selected in the control.
        /// </summary>
        [Category("Behavior")]
        [Description("Maximum allowable date.")]
        public DateTime MaxDate
        {
            get { return EffectiveMaxDate(_maxDateTime); }

            set
            {
                if (value != _maxDateTime)
                {
                    if (value < EffectiveMinDate(_minDateTime))
                        throw new ArgumentOutOfRangeException("Date provided is less than the minimum supported date.");

                    if (value > DateTimePicker.MaximumDateTime)
                        throw new ArgumentOutOfRangeException("Date provided is greater than the maximum supported date.");

                    _maxDateTime = value;

                    if (_dateTime > _maxDateTime)
                    {
                        _dateTime = _maxDateTime;
                        OnValueChanged(EventArgs.Empty);

                        if (_rawDateTime is DateTime)
                        {
                            _rawDateTime = _maxDateTime;
                            OnValueNullableChanged(EventArgs.Empty);
                        }

                        PerformNeedPaint(true);
                    }
                }
            }
        }

        /// <summary>
        /// Should the MaxDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMaxDate()
        {
            return (_maxDateTime != DateTimePicker.MaximumDateTime) && (_maxDateTime != DateTime.MaxValue);
        }

        private void ResetMaxDate()
        {
            MaxDate = DateTime.MaxValue;
        }

        /// <summary>
        /// Gets or sets the minimum date and time that can be selected in the control.
        /// </summary>
        [Category("Behavior")]
        [Description("Minimum allowable date.")]
        public DateTime MinDate
        {
            get { return EffectiveMinDate(_minDateTime); }

            set
            {
                if (value != _minDateTime)
                {
                    if (value > EffectiveMaxDate(_maxDateTime))
                        throw new ArgumentOutOfRangeException("Date provided is greater than the maximum supported date.");

                    if (value < DateTimePicker.MinimumDateTime)
                        throw new ArgumentOutOfRangeException("Date provided is less than the minimum supported date.");

                    _minDateTime = value;

                    if (_dateTime < _minDateTime)
                    {
                        _dateTime = _minDateTime;
                        OnValueChanged(EventArgs.Empty);

                        if (_rawDateTime is DateTime)
                        {
                            _rawDateTime = _minDateTime;
                            OnValueNullableChanged(EventArgs.Empty);
                        }

                        PerformNeedPaint(true);
                    }
                }
            }
        }

        /// <summary>
        /// Should the MinDate property be serialized.
        /// </summary>
        /// <returns>True if property needs to be serialized.</returns>
        public bool ShouldSerializeMinDate()
        {
            return (_minDateTime != DateTimePicker.MinimumDateTime) && (_minDateTime != DateTime.MinValue);
        }

        private void ResetMinDate()
        {
            MinDate = DateTime.MinValue;
        }

        /// <summary>
        /// Gets or sets a value indicating if the check box is checked and if the ValueNullable is DBNull or a DateTime value.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if the check box is checked and if the ValueNullable is DBNull or a DateTime value.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Bindable(true)]
        public bool Checked
        {
            get { return (_drawCheckBox.CheckState == CheckState.Checked); }

            set
            {
                if (Checked != value)
                {
                    if (value)
                    {
                        _drawCheckBox.CheckState = CheckState.Checked;
                        _rawDateTime = _dateTime;
                    }
                    else
                    {
                        _drawCheckBox.CheckState = CheckState.Unchecked;
                        _rawDateTime = DBNull.Value;
                    }

                    PerformNeedPaint(true);
                    OnCheckedChanged(EventArgs.Empty);
                    OnValueChanged(EventArgs.Empty);
                    OnValueNullableChanged(EventArgs.Empty);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the custom date/time format string.
        /// </summary>
        [Category("Behavior")]
        [Description("The custom format string used to format the date and/or time displayed in the control.")]
        [DefaultValue("")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Localizable(true)]
        public string CustomFormat
        {
            get { return _customFormat; }

            set
            {
                if ((_customFormat != value) && (value != null))
                {
                    _customFormat = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the custom text to show when control is not checked.
        /// </summary>
        [Category("Behavior")]
        [Description("The custom text to draw when the control is not checked. Provide an empty string for default action of showing the defined date.")]
        [DefaultValue("")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Localizable(true)]
        public string CustomNullText
        {
            get { return _customNullText; }

            set
            {
                if (_customNullText != value)
                {
                    _customNullText = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the today date format string.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("The today format string used to format the date displayed in the today button.")]
        [DefaultValue("d")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Localizable(true)]
        public string CalendarTodayFormat
        {
            get { return _todayFormat; }
            set { _todayFormat = value; }
        }

        /// <summary>
        /// Gets and sets the header style for the month calendar.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Header style for the month calendar.")]
        public HeaderStyle CalendarHeaderStyle
        {
            get { return _headerStyle; }
            set { _headerStyle = value; }
        }

        private void ResetCalendarHeaderStyle()
        {
            CalendarHeaderStyle = HeaderStyle.Calendar;
        }

        private bool ShouldSerializeCalendarHeaderStyle()
        {
            return (CalendarHeaderStyle != HeaderStyle.Calendar);
        }

        /// <summary>
        /// Gets and sets the content style for the day entries.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Content style for the day entries.")]
        public ButtonStyle CalendarDayStyle
        {
            get { return _dayStyle; }
            set { _dayStyle = value; }
        }

        private void ResetCalendarDayStyle()
        {
            CalendarDayStyle = ButtonStyle.CalendarDay;
        }

        private bool ShouldSerializeCalendarDayStyle()
        {
            return (CalendarDayStyle != ButtonStyle.CalendarDay);
        }

        /// <summary>
        /// Gets and sets the content style for the day of week labels.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Content style for the day of week labels.")]
        public ButtonStyle CalendarDayOfWeekStyle
        {
            get { return _dayOfWeekStyle; }
            set { _dayOfWeekStyle = value; }
        }

        private void ResetCalendarDayOfWeekStyle()
        {
            CalendarDayOfWeekStyle = ButtonStyle.CalendarDay;
        }

        private bool ShouldSerializeCalendarDayOfWeekStyle()
        {
            return (CalendarDayOfWeekStyle != ButtonStyle.CalendarDay);
        }

		/// <summary>
		/// Gets or sets the palette to be applied.
		/// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public new PaletteMode PaletteMode
        {
            get { return base.PaletteMode; }
            set { base.PaletteMode = value; }
        }

		/// <summary>
		/// Gets and sets the custom palette implementation.
		/// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Custom palette applied to drawing.")]
        [DefaultValue(null)]
        public new IPalette Palette
        {
            get { return base.Palette; }
            set { base.Palette = value; }
        }

        /// <summary>
        /// Gets and sets Determines if the control is always active or only when the mouse is over the control or has focus.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Determines if the control is always active or only when the mouse is over the control or has focus.")]
        [DefaultValue(true)]
        public bool AlwaysActive
        {
            get { return _alwaysActive; }

            set
            {
                if (_alwaysActive != value)
                {
                    _alwaysActive = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets access to the checkbox value overrides.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("CheckBox image overrides.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CheckBoxImages Images
        {
            get { return _checkBoxImages; }
        }

        private bool ShouldSerializeImages()
        {
            return !_checkBoxImages.IsDefault;
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Input control style.")]
        public InputControlStyle InputControlStyle
        {
            get
            {
                return _inputControlStyle;
            }

            set
            {
                if (_inputControlStyle != value)
                {
                    _inputControlStyle = value;
                    _stateCommon.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetInputControlStyle()
        {
            InputControlStyle = InputControlStyle.Standalone;
        }

        private bool ShouldSerializeInputControlStyle()
        {
            return (InputControlStyle != InputControlStyle.Standalone);
        }

        /// <summary>
        /// Gets and sets the up and down buttons style.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Up and down buttons style.")]
        public ButtonStyle UpDownButtonStyle
        {
            get { return _upDownButtonStyle; }

            set
            {
                if (_upDownButtonStyle != value)
                {
                    _upDownButtonStyle = value;
                    _paletteUpDown.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetUpDownButtonStyle()
        {
            UpDownButtonStyle = ButtonStyle.InputControl;
        }

        private bool ShouldSerializeUpDownButtonStyle()
        {
            return (UpDownButtonStyle != ButtonStyle.InputControl);
        }

        /// <summary>
        /// Gets and sets the drop button style.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("DropButton style.")]
        public ButtonStyle DropButtonStyle
        {
            get { return _dropButtonStyle; }

            set
            {
                if (_dropButtonStyle != value)
                {
                    _dropButtonStyle = value;
                    _paletteDropDown.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetDropButtonStyle()
        {
            DropButtonStyle = ButtonStyle.InputControl;
        }

        private bool ShouldSerializeDropButtonStyle()
        {
            return (DropButtonStyle != ButtonStyle.InputControl);
        }

        /// <summary>
		/// Gets the collection of button specifications.
		/// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DateTimePickerButtonSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _allowButtonSpecToolTips; }
            set { _allowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Gets access to the common date time picker appearance entries that other states can override.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Overrides for defining common date time picker appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled date time picker appearance entries.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Overrides for defining disabled date time picker appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleStates StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal date time picker appearance entries.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Overrides for defining normal date time picker appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleStates StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the active date time picker appearance entries.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Overrides for defining active date time picker appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleStates StateActive
        {
            get { return _stateActive; }
        }

        private bool ShouldSerializeStateActive()
        {
            return !_stateActive.IsDefault;
        }

        /// <summary>
        /// Gets and sets the active fragment.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ActiveFragment
        {
            get { return _drawText.ActiveFragment; }

            set
            {
                if (_drawText.ActiveFragment != value)
                {
                    _drawText.ActiveFragment = value;
                     if (_drawText.ActiveFragment == value)
                    {
                        PerformNeedPaint(true);
                        CheckActiveFragment(); 
                     }
                }
            }
        }

        /// <summary>
        /// Move selection to the first input fragment.
        /// </summary>
        public void SelectFirstFragment()
        {
            _drawText.MoveFirstFragment();
            PerformNeedPaint(true);
            CheckActiveFragment(); 
        }

        /// <summary>
        /// Move selection to the next input fragment.
        /// </summary>
        public void SelectNextFragment()
        {
            _drawText.MoveNextFragment();
            PerformNeedPaint(true);
            CheckActiveFragment();
        }

        /// <summary>
        /// Move selection to the previous input fragment.
        /// </summary>
        public void SelectPreviousFragment()
        {
            _drawText.MovePreviousFragment();
            PerformNeedPaint(true);
            CheckActiveFragment();
        }

        /// <summary>
        /// Move selection to the last input fragment.
        /// </summary>
        public void SelectLastFragment()
        {
            _drawText.MoveLastFragment();
            PerformNeedPaint(true);
            CheckActiveFragment();
        }

        /// <summary>
        /// Sets the fixed state of the control.
        /// </summary>
        /// <param name="active">Should the control be fixed as active.</param>
        public void SetFixedState(bool active)
        {
            _fixedActive = active;
        }

        /// <summary>
        /// Gets access to the ToolTipManager used for displaying tool tips.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTipManager ToolTipManager
        {
            get { return _toolTipManager; }
        }

        /// <summary>
        /// Gets a value indicating if the input control is active.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsActive
        {
            get
            {
                if (_fixedActive != null)
                    return _fixedActive.Value;
                else
                    return (DesignMode || AlwaysActive || ContainsFocus || _mouseOver);
            }
        }

        /// <summary>
        /// Gets a value indicating if the mouse is over the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsMouseOver
        {
            get { return _mouseOver; }
        }

        /// <summary>
        /// Gets a value indicating if the drop down calendar is showing.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsDropped
        {
            get { return _dropped; }
        }

        /// <summary>
        /// Gets the image used for the ribbon tab.
        /// </summary>
        /// <param name="state">Tab state.</param>
        /// <returns>Image.</returns>
        public Image GetImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the image color that should be interpreted as transparent.
        /// </summary>
        /// <param name="state">Tab state.</param>
        /// <returns>Transparent Color.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the short text used as the main ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetShortText()
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets the long text used as the secondary ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetLongText()
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets and sets if the control is in the ribbon design mode.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool InRibbonDesignMode
        {
            get { return _inRibbonDesignMode; }
            set { _inRibbonDesignMode = value; }
        }

        /// <summary>
        /// Get the preferred size of the control based on a proposed size.
        /// </summary>
        /// <param name="proposedSize">Starting size proposed by the caller.</param>
        /// <returns>Calculated preferred size.</returns>
        public override Size GetPreferredSize(Size proposedSize)
        {
            // Do we have a manager to ask for a preferred size?
            if (ViewManager != null)
            {
                // Ask the view to peform a layout
                Size retSize = ViewManager.GetPreferredSize(Renderer, proposedSize);

                // Apply the maximum sizing
                if (MaximumSize.Width > 0)  retSize.Width = Math.Min(MaximumSize.Width, retSize.Width);
                if (MaximumSize.Height > 0) retSize.Height = Math.Min(MaximumSize.Height, retSize.Width);

                // Apply the minimum sizing
                if (MinimumSize.Width > 0)  retSize.Width = Math.Max(MinimumSize.Width, retSize.Width);
                if (MinimumSize.Height > 0) retSize.Height = Math.Max(MinimumSize.Height, retSize.Height);

                return retSize;
            }
            else
            {
                // Fall back on default control processing
                return base.GetPreferredSize(proposedSize);
            }
        }

        /// <summary>
        /// Sets if the adornments are displayed (checkbox/drop button/up down buttons)
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowAdornments
        {
            set 
            {
                if (_showAdornments != value)
                {
                    _showAdornments = value;
                    _layoutCheckBox.Visible = ShowCheckBox && _showAdornments;
                    _upDownFit.Visible = ShowUpDown && _showAdornments;
                    _dropStretch.Visible = !ShowUpDown && _showAdornments;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Sets if the adornments are displayed (checkbox/drop button/up down buttons)
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowBorder
        {
            set { _drawDockerOuter.IgnoreAllBorderAndPadding = !value; }
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
            if ((_buttonManager != null) && _buttonManager.DesignerGetHitTest(pt))
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

        #region Protected Virtual
        /// <summary>
        /// Raises the RightToLeftLayoutChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
        {
            if (RightToLeftLayoutChanged != null)
                RightToLeftLayoutChanged(this, e);
        }

        /// <summary>
        /// Raises the FormatChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFormatChanged(EventArgs e)
        {
            if (FormatChanged != null)
                FormatChanged(this, e);
        }

        /// <summary>
        /// Raises the CheckedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanged != null)
                CheckedChanged(this, e);
        }

        /// <summary>
        /// Raises the DropDown event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDropDown(DateTimePickerDropArgs e)
        {
            if (DropDown != null)
                DropDown(this, e);
        }

        /// <summary>
        /// Raises the CloseUp event.
        /// </summary>
        /// <param name="e">An DateTimePickerCloseArgs containing the event data.</param>
        protected virtual void OnCloseUp(DateTimePickerCloseArgs e)
        {
            if (CloseUp != null)
                CloseUp(this, e);
        }

        /// <summary>
        /// Raises the CloseUpMonthCalendarChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCloseUpMonthCalendarChanged(EventArgs e)
        {
            if (CloseUpMonthCalendarChanged != null)
                CloseUpMonthCalendarChanged(this, e);
        }

        /// <summary>
        /// Raises the AutoShiftOverflow event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        internal protected virtual void OnAutoShiftOverflow(CancelEventArgs e)
        {
            if (AutoShiftOverflow != null)
                AutoShiftOverflow(this, e);
        }

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
        /// Raises the ValueNullableChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnValueNullableChanged(EventArgs e)
        {
            if (ValueNullableChanged != null)
                ValueNullableChanged(this, e);
        }

        /// <summary>
        /// Raises the ActiveFragmentChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnActiveFragmentChanged(EventArgs e)
        {
            if (ActiveFragmentChanged != null)
                ActiveFragmentChanged(this, e);
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows Message to process. </param>
        protected override void WndProc(ref Message m)
        {
            // At design time inside the ribbon we are transparent to the mouse
            if ((m.Msg == (int)PI.WM_NCHITTEST) && _inRibbonDesignMode)
            {
                // Allow actions to occur to window beneath us
                m.Result = (IntPtr)PI.HTTRANSPARENT;
            }
            else
                base.WndProc(ref m);
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
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The mnemonic character entered.</param>
        /// <returns>true if the mnemonic was processsed; otherwise, false.</returns>
        protected override bool ProcessMnemonic(char charCode)
        {
            // If the button manager wants to process mnemonic characters and
            // this control is currently visible and enabled then...
            if (UseMnemonic && CanProcessMnemonic())
            {
                // Pass request onto the button spec manager
                if (_buttonManager.ProcessMnemonic(charCode))
                    return true;
            }

            // No match found, let base class do standard processing
            return base.ProcessMnemonic(charCode);
        }

        /// <summary>
        /// Raises the KeyDown event.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !_inRibbonDesignMode)
            {
                // Special case pressing the spacebar
                if (e.KeyCode == Keys.Space)
                {
                    // If focus is on the checkbox then invert checked state
                    if (_drawCheckBox.ForcedTracking)
                        Checked = !Checked;
                }
                else
                {
                    _drawText.PerformKeyDown(e);
                    CheckActiveFragment();
                }
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Raises the KeyPress event.
        /// </summary>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !_inRibbonDesignMode)
            {
                _drawText.PerformKeyPress(e);
                CheckActiveFragment();
            }

         	base.OnKeyPress(e);
        }

        /// <summary>
        /// Raises the MouseWheel event.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !_inRibbonDesignMode)
            {
                // We treat positive numbers as moving upwards
                KeyEventArgs kpea = new KeyEventArgs((e.Delta < 0) ? Keys.Down : Keys.Up);

                // Simulate the up/down key the correct number of times
                int detents = Math.Abs(e.Delta) / SystemInformation.MouseWheelScrollDelta;
                for(int i=0; i<detents; i++)
                    _drawText.PerformKeyDown(kpea);

                CheckActiveFragment();
            }
                
            base.OnMouseWheel(e);
        }

        /// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			// Change in enabled state requires a layout and repaint
            UpdateStateAndPalettes();

            _drawText.Enabled = Enabled;
            _drawCheckBox.Enabled = Enabled;
            _buttonDropDown.Enabled = Enabled;
            _buttonDown.Enabled = Enabled;
            _buttonUp.Enabled = Enabled;
            _drawDockerInner.Enabled = Enabled;
            _drawDockerOuter.Enabled = Enabled;

            // Update state to reflect change in enabled state
            _buttonManager.RefreshButtons();

			// Change in enabled state requires a layout and repaint
			PerformNeedPaint(true);

			// Let base class fire standard event
			base.OnEnabledChanged(e);
		}

        /// <summary>
        /// Raises the MouseDown event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            bool rtl = (_drawText.RightToLeftLayout && (RightToLeft == RightToLeft.Yes));

            // If the point is before the drop buttons...
            if ((!ShowUpDown && !rtl && (e.X < _buttonDropDown.ClientLocation.X)) ||
                (!ShowUpDown && rtl && (e.X > _buttonDropDown.ClientRectangle.Right)) ||
                (ShowUpDown && !rtl && (e.X < _buttonUp.ClientLocation.X)) ||
                (ShowUpDown && rtl && (e.X < _buttonUp.ClientRectangle.Right)))
            {
                // If the point is after any optional checkbox
                if (!ShowCheckBox || 
                    (ShowCheckBox && !rtl && Checked && (e.X > _drawCheckBox.ClientRectangle.Right)) ||
                    (ShowCheckBox && rtl && Checked && (e.X < _drawCheckBox.ClientRectangle.Left)))
                {
                    // Ask the draw text to set the active fragment to the mouse click
                    _drawCheckBox.ForcedTracking = false;
                    _drawText.SelectFragment(new Point(e.X, e.Y), e.Button);
                    CheckActiveFragment();
                }
                else
                {
                    // Make the checkbox the active element
                    _drawCheckBox.ForcedTracking = true;
                    _drawText.ClearActiveFragment();
                    CheckActiveFragment();
                }

                PerformNeedPaint(true);
            }

            // We always take the focus on a mouse down
            if (!ContainsFocus)
                Focus();

            // Let base class perform standard processing
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the HandleCreated event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            // Let base class do standard stuff
            base.OnHandleCreated(e);

            // Force the font to be set into the text box child control
            PerformNeedPaint(false);

            // We need a layout to occur before any painting
            InvokeLayout();

            // We need to recalculate the correct height
            Height = PreferredHeight;
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">An EventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (!IsDisposed)
            {
                // Update to match the new palette settings
                Height = PreferredHeight;

                // Let base class calulcate fill rectangle
                base.OnLayout(levent);
            }
        }

        /// <summary>
        /// Raises the MouseEnter event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _mouseOver = true;
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Raises the MouseLeave event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _mouseOver = false;
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            _drawText.HasFocus = true;

            // If a fragment is active then give it the focus
            if (!_drawText.HasActiveFragment)
            {
                // If we have a checbox then give it the focus
                if (ShowCheckBox)
                    _drawCheckBox.ForcedTracking = true;
                else
                {
                    _drawText.MoveFirstFragment();
                    CheckActiveFragment();
                }
            }

            UpdateStateAndPalettes();
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            _drawCheckBox.ForcedTracking = false;
            _drawText.HasFocus = false;
            _drawText.ClearActiveFragment();
            CheckActiveFragment();
            UpdateStateAndPalettes();
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new Left property value of the control.</param>
        /// <param name="y">The new Top property value of the control.</param>
        /// <param name="width">The new Width property value of the control.</param>
        /// <param name="height">The new Height property value of the control.</param>
        /// <param name="specified">A bitwise combination of the BoundsSpecified values.</param>
        protected override void SetBoundsCore(int x, int y,
                                              int width, int height,
                                              BoundsSpecified specified)
        {
            // If setting the actual height
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
            {
                // First time the height is set, remember it
                if (_cachedHeight == -1)
                    _cachedHeight = height;

                // Override the actual height used
                height = PreferredHeight;
            }

            // If setting the actual height then cache it for later
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
                _cachedHeight = height;

            base.SetBoundsCore(x, y, width, height, specified);
        }

        /// <summary>
        /// Raises the RightToLeftChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            UpdateForRightToLeft();
            base.OnRightToLeftChanged(e);
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(240, PreferredHeight); }
        }

        /// <summary>
        /// Processes a notification from palette storage of a button spec change.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnButtonSpecChanged(object sender, EventArgs e)
        {
            // Recreate all the button specs with new values
            _buttonManager.RecreateButtons();

            // Let base class perform standard processing
            base.OnButtonSpecChanged(sender, e);
        }
		#endregion

        #region Internal
        internal DateTime InternalDateTime()
        {
            return _dateTime;
        }

        internal bool InternalDateTimeNull()
        {
            return (_rawDateTime == DBNull.Value);
        }
        
        internal ViewDrawCheckBox InternalViewDrawCheckBox
        {
            get { return _drawCheckBox; }
        }

        internal bool IsFixedActive
        {
            get { return (_fixedActive != null); }
        }

        internal DateTime EffectiveMaxDate(DateTime maxDate)
        {
            DateTime maximumDateTime = DateTimePicker.MaximumDateTime;
            if (maxDate > maximumDateTime)
                return maximumDateTime;
            else
                return maxDate;
        }

        internal DateTime EffectiveMinDate(DateTime minDate)
        {
            DateTime minimumDateTime = DateTimePicker.MinimumDateTime;
            if (minDate < minimumDateTime)
                return minimumDateTime;
            else
                return minDate;
        }
        #endregion

        #region Implementation
        private int PreferredHeight
        {
            get
            {
                // Get the preferred size of the entire control
                Size preferredSize = GetPreferredSize(new Size(int.MaxValue, int.MaxValue));

                // We only need to the height
                return preferredSize.Height;
            }
        }

        private void UpdateStateAndPalettes()
        {
            // Get the correct palette settings to use
            IPaletteTriple tripleState = GetTripleState();
            _drawDockerOuter.SetPalettes(tripleState.PaletteBack, tripleState.PaletteBorder);

            // Update enabled state
            _drawDockerOuter.Enabled = Enabled;

            // Find the new state of the main view element
            PaletteState state;
            if (IsActive)
                state = PaletteState.Tracking;
            else
                state = PaletteState.Normal;

            _drawDockerOuter.ElementState = state;
        }

        private IPaletteTriple GetTripleState()
        {
            if (Enabled)
            {
                if (IsActive)
                    return _stateActive;
                else
                    return _stateNormal;
            }
            else
                return _stateDisabled;
        }

        private void CheckActiveFragment()
        {
            if (_lastActiveFragment != ActiveFragment)
            {
                _lastActiveFragment = ActiveFragment;
                OnActiveFragmentChanged(EventArgs.Empty);
            }
        }

        private void UpdateForRightToLeft()
        {
            if (_drawText.RightToLeftLayout && (RightToLeft == RightToLeft.Yes))
            {
                _drawDockerInner.SetDock(_dropStretch, ViewDockStyle.Left);
                _drawDockerInner.SetDock(_upDownFit, ViewDockStyle.Left);
                _drawDockerInner.SetDock(_layoutCheckBox, ViewDockStyle.Right);
            }
            else
            {
                _drawDockerInner.SetDock(_dropStretch, ViewDockStyle.Right);
                _drawDockerInner.SetDock(_upDownFit, ViewDockStyle.Right);
                _drawDockerInner.SetDock(_layoutCheckBox, ViewDockStyle.Left);
            }
        }

        private void OnShowToolTip(object sender, ToolTipEventArgs e)
        {
            if (!IsDisposed)
            {
                // Do not show tooltips when the form we are in does not have focus
                Form topForm = FindForm();
                if ((topForm != null) && !topForm.ContainsFocus)
                    return;

                // Never show tooltips are design time
                if (!DesignMode)
                {
                    IContentValues sourceContent = null;
                    LabelStyle toolTipStyle = LabelStyle.ToolTip;

                    // Find the button spec associated with the tooltip request
                    ButtonSpec buttonSpec = _buttonManager.ButtonSpecFromView(e.Target);

                    // If the tooltip is for a button spec
                    if (buttonSpec != null)
                    {
                        // Are we allowed to show page related tooltips
                        if (AllowButtonSpecToolTips)
                        {
                            // Create a helper object to provide tooltip values
                            ButtonSpecToContent buttonSpecMapping = new ButtonSpecToContent(Redirector, buttonSpec);

                            // Is there actually anything to show for the tooltip
                            if (buttonSpecMapping.HasContent)
                            {
                                sourceContent = buttonSpecMapping;
                                toolTipStyle = buttonSpec.ToolTipStyle;
                            }
                        }
                    }

                    if (sourceContent != null)
                    {
                        // Remove any currently showing tooltip
                        if (_visualPopupToolTip != null)
                            _visualPopupToolTip.Dispose();

                        // Create the actual tooltip popup object
                        _visualPopupToolTip = new VisualPopupToolTip(Redirector,
                                                                     sourceContent,
                                                                     Renderer,
                                                                     PaletteBackStyle.ControlToolTip,
                                                                     PaletteBorderStyle.ControlToolTip,
                                                                     CommonHelper.ContentStyleFromLabelStyle(toolTipStyle));

                        _visualPopupToolTip.Disposed += new EventHandler(OnVisualPopupToolTipDisposed);

                        // Show relative to the provided screen rectangle
                        _visualPopupToolTip.ShowCalculatingSize(RectangleToScreen(e.Target.ClientRectangle));
                    }
                }
            }
        }

        private void OnCheckBoxClick(object sender, EventArgs e)
        {
            // Invert the current checked state
            Checked = !Checked;
        }

        private void OnDropDownClick(object sender, EventArgs e)
        {
            // Never shown the calendar at design time
            if (!_inRibbonDesignMode)
            {
                // Just in case the user is inputting characters, end it
                _drawText.EndInputDigits();

                // Reset the cacheed value indicating if a date is selected in the month calendar
                _dropDownMonthChanged = false;

                // Create a new krypton context menu each time we drop the menu
                DTPContextMenu kcm = new DTPContextMenu(RectangleToScreen(_buttonDropDown.ClientRectangle));

                // Add and setup a month calendar element
                _kmc = new KryptonContextMenuMonthCalendar();
                _kmc.CalendarDimensions = CalendarDimensions;
                _kmc.TodayText = CalendarTodayText;
                _kmc.TodayFormat = CalendarTodayFormat;
                _kmc.FirstDayOfWeek = CalendarFirstDayOfWeek;
                _kmc.MaxDate = MaxDate.Date;
                _kmc.MaxSelectionCount = 1;
                _kmc.MinDate = MinDate.Date;
                _kmc.SelectionStart = Value.Date;
                _kmc.ShowToday = CalendarShowToday;
                _kmc.ShowTodayCircle = CalendarShowTodayCircle;
                _kmc.ShowWeekNumbers = CalendarShowWeekNumbers;
                _kmc.CloseOnTodayClick = CalendarCloseOnTodayClick;
                _kmc.TodayDate = CalendarTodayDate;
                _kmc.AnnuallyBoldedDates = CalendarAnnuallyBoldedDates;
                _kmc.MonthlyBoldedDates = CalendarMonthlyBoldedDates;
                _kmc.BoldedDates = CalendarBoldedDates;
                _kmc.DayOfWeekStyle = CalendarDayOfWeekStyle;
                _kmc.DayStyle = CalendarDayStyle;
                _kmc.HeaderStyle = CalendarHeaderStyle;
                _kmc.DateChanged += new DateRangeEventHandler(OnMonthCalendarDateChanged);
                kcm.Items.Add(_kmc);

                // Update the krypton menu with this controls palette state
                if (PaletteMode != PaletteMode.Custom)
                    kcm.PaletteMode = PaletteMode;
                else
                    kcm.Palette = Palette;

                // Give user a change to modify the context menu or even cancel the menu entirely
                DateTimePickerDropArgs dtpda = new DateTimePickerDropArgs(kcm,
                                                                          (DropDownAlign == LeftRightAlignment.Left ? KryptonContextMenuPositionH.Left : KryptonContextMenuPositionH.Right),
                                                                          KryptonContextMenuPositionV.Below);
                // Let user examine and later values
                OnDropDown(dtpda);

                // If we still want to show a context menu
                if (!dtpda.Cancel)
                {
                    if (dtpda.KryptonContextMenu != null)
                    {
                        // If showing a menu then we automatically ensure the control is checked
                        Checked = true;

                        // Convert the client rect to screen coords
                        Rectangle screenRect = RectangleToScreen(ClientRectangle);
                        if (CommonHelper.ValidKryptonContextMenu(dtpda.KryptonContextMenu))
                        {
                            // Modify the screen rect so that we have a pixel gap between control and menu
                            switch (dtpda.PositionV)
                            {
                                case KryptonContextMenuPositionV.Above:
                                    screenRect.Y -= 1;
                                    break;
                                case KryptonContextMenuPositionV.Below:
                                    screenRect.Height += 1;
                                    break;
                            }

                            switch (dtpda.PositionH)
                            {
                                case KryptonContextMenuPositionH.Before:
                                    screenRect.X -= 1;
                                    break;
                                case KryptonContextMenuPositionH.After:
                                    screenRect.Width += 1;
                                    break;
                            }


                            // Show relative to the screen rectangle
                            dtpda.KryptonContextMenu.Closed += new ToolStripDropDownClosedEventHandler(OnKryptonContextMenuClosed);
                            dtpda.KryptonContextMenu.Show(this, screenRect, dtpda.PositionH, dtpda.PositionV);
                            return;
                        }
                    }
                }

                kcm.Dispose();
            }
            
            // Did not show a context menu so we remove the fixed appearance of button
            _buttonDropDown.RemoveFixed();
        }

        private void OnMonthCalendarDateChanged(object sender, DateRangeEventArgs e)
        {
            // Use the newly selected date but the exising time
            DateTime newDt = new DateTime(e.Start.Year, e.Start.Month, e.Start.Day, 
                                          _dateTime.Hour, _dateTime.Minute, _dateTime.Second, _dateTime.Millisecond);

            // Range check in case the min/max have time portions and not just full days
            if (newDt > MaxDate)
                newDt = MaxDate;

            if (newDt < MinDate)
                newDt = MinDate;

            // Use new value
            Value = newDt;

            // Remember that the date was changed from the drop down month calendar
            _dropDownMonthChanged = true;
        }

        private void OnKryptonContextMenuClosed(object sender, EventArgs e)
        {
            // Must unhook from menu so it can be garbage collected
            KryptonContextMenu kcm = (KryptonContextMenu)sender;
            kcm.Closed -= new ToolStripDropDownClosedEventHandler(OnKryptonContextMenuClosed);

            // Unhook from month calendar events
            if (_kmc != null)
            {
                _kmc.DateChanged -= new DateRangeEventHandler(OnMonthCalendarDateChanged);
                _kmc = null;
            }

            // Generate the close up event and provide the menu so handlers can examine state that might have changed
            DateTimePickerCloseArgs dtca = new DateTimePickerCloseArgs(kcm);
            OnCloseUp(dtca);

            // Notify that the month calendar changed value whilst the dropped down.
            if (_dropDownMonthChanged)
                OnCloseUpMonthCalendarChanged(EventArgs.Empty);

            // Did not show a context menu so we remove the fixed appearance of button
            _buttonDropDown.RemoveFixed();
            kcm.Dispose();
        }

        private void OnUpClick(object sender, EventArgs e)
        {
            // Never operate the control at design time
            if (!_inRibbonDesignMode)
            {
                _drawText.PerformKeyDown(new KeyEventArgs(Keys.Up));
                CheckActiveFragment();
            }
            
            _buttonUp.RemoveFixed();
        }
        
        private void OnDownClick(object sender, EventArgs e)
        {
            // Never operate the control at design time
            if (!_inRibbonDesignMode)
            {
                _drawText.PerformKeyDown(new KeyEventArgs(Keys.Down));
                CheckActiveFragment();
            }
            
            _buttonDown.RemoveFixed();
        }

        private void OnCancelToolTip(object sender, EventArgs e)
        {
            // Remove any currently showing tooltip
            if (_visualPopupToolTip != null)
                _visualPopupToolTip.Dispose();
        }

        private void OnVisualPopupToolTipDisposed(object sender, EventArgs e)
        {
            // Unhook events from the specific instance that generated event
            VisualPopupToolTip popupToolTip = (VisualPopupToolTip)sender;
            popupToolTip.Disposed -= new EventHandler(OnVisualPopupToolTipDisposed);

            // Not showing a popup page any more
            _visualPopupToolTip = null;
        }
        #endregion
    }

    /// <summary>
    /// Specialized implementation of the KryptonContextMenu for use with the KryptonDateTimePicker.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class DTPContextMenu : KryptonContextMenu
    {
        #region Instance Fields
        private Rectangle _dropScreenRect;
        #endregion

        #region Identity
        /// <summary>
        ///  Initialize a new instance of the DTPContextMenu class.
        /// </summary>
        /// <param name="dropScreenRect">Screen rectangle of the drop down button on the KryptonDateTimePicker.</param>
        public DTPContextMenu(Rectangle dropScreenRect)
        {
            _dropScreenRect = dropScreenRect;
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Create a new visual context menu for showing the defined items.
        /// </summary>
        /// <param name="kcm">Owning KryptonContextMenu instance.</param>
        /// <param name="palette">Drawing palette.</param>
        /// <param name="paletteMode">Drawing palette mode.</param>
        /// <param name="redirector">Redirector for sourcing base values.</param>
        /// <param name="redirectorImages">Redirector for sourcing base images.</param>
        /// <param name="items">Colletion of menu items.</param>
        /// <param name="enabled">Enabled state of the menu.</param>
        /// <param name="keyboardActivated">True is menu was keyboard initiated.</param>
        /// <returns>VisualContextMenu reference.</returns>
        protected override VisualContextMenu CreateContextMenu(KryptonContextMenu kcm,
                                                               IPalette palette,
                                                               PaletteMode paletteMode,
                                                               PaletteRedirect redirector,
                                                               PaletteRedirectContextMenu redirectorImages,
                                                               KryptonContextMenuCollection items,
                                                               Boolean enabled,
                                                               bool keyboardActivated)
        {
            return new VisualContextMenuDTP(kcm, palette, paletteMode, redirector, redirectorImages, items, enabled, keyboardActivated, _dropScreenRect);
        }
        #endregion
    }
}
