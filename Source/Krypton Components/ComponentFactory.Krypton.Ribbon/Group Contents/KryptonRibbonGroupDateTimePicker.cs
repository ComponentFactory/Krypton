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
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Represents a ribbon group date time picker.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupDateTimePicker), "ToolboxBitmaps.KryptonRibbonGroupDateTimePicker.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupDateTimePickerDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [DefaultBindingProperty("Value")]
    public class KryptonRibbonGroupDateTimePicker : KryptonRibbonGroupItem
    {
        #region Instance Fields
        private bool _visible;
        private string _keyTip;
        private Keys _shortcutKeys;
        private GroupItemSize _itemSizeCurrent;
        private NeedPaintHandler _viewPaintDelegate;
        private KryptonDateTimePicker _dateTimePicker;
        private KryptonDateTimePicker _lastDateTimePicker;
        private IKryptonDesignObject _designer;
        private Control _lastParentControl;
        private ViewBase _dateTimePickerView;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the control receives focus.
        /// </summary>
        [Browsable(false)]
        public event EventHandler GotFocus;

        /// <summary>
        /// Occurs when the control loses focus.
        /// </summary>
        [Browsable(false)]
        public event EventHandler LostFocus;

        /// <summary>
        /// Occurs when the Value property has changed value.
        /// </summary>
        [Category("Action")]
        [Description("Event raised when the value of the Value property is changed.")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Occurs when the ValueNullable property has changed value.
        /// </summary>
        [Category("Action")]
        [Description("Event raised when the value of the ValueNullable property is changed.")]
        public event EventHandler ValueNullableChanged;

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
        /// Occurs when the Checked property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Event raised when the value of the Checked property is changed.")]
        public event EventHandler CheckedChanged;

        /// <summary>
        /// Occurs when the Format property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Event raised when the value of the Format property is changed.")]
        public event EventHandler FormatChanged;

        /// <summary>
        /// Occurs when a key is pressed while the control has focus. 
        /// </summary>
        [Description("Occurs when a key is pressed while the control has focus.")]
        [Category("Key")]
        public event KeyPressEventHandler KeyPress;

        /// <summary>
        /// Occurs when a key is released while the control has focus. 
        /// </summary>
        [Description("Occurs when a key is released while the control has focus.")]
        [Category("Key")]
        public event KeyEventHandler KeyUp;

        /// <summary>
        /// Occurs when a key is pressed while the control has focus.
        /// </summary>
        [Description("Occurs when a key is pressed while the control has focus.")]
        [Category("Key")]
        public event KeyEventHandler KeyDown;

        /// <summary>
        /// Occurs before the KeyDown event when a key is pressed while focus is on this control.
        /// </summary>
        [Description("Occurs before the KeyDown event when a key is pressed while focus is on this control.")]
        [Category("Key")]
        public event PreviewKeyDownEventHandler PreviewKeyDown;

        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs after the value of a property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the design time context menu is requested.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event MouseEventHandler DesignTimeContextMenu;

        internal event EventHandler MouseEnterControl;
        internal event EventHandler MouseLeaveControl;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupDateTimePicker class.
        /// </summary>
        public KryptonRibbonGroupDateTimePicker()
        {
            // Default fields
            _visible = true;
            _itemSizeCurrent = GroupItemSize.Medium;
            _shortcutKeys = Keys.None;
            _keyTip = "X";

            // Create the actual date time picker control and set initial settings
            _dateTimePicker = new KryptonDateTimePicker();
            _dateTimePicker.InputControlStyle = InputControlStyle.Ribbon;
            _dateTimePicker.AlwaysActive = false;
            _dateTimePicker.MinimumSize = new Size(180, 0);
            _dateTimePicker.MaximumSize = new Size(180, 0);
            _dateTimePicker.TabStop = false;

            // Hook into events to expose via this container
            _dateTimePicker.ValueChanged += new EventHandler(OnDateTimePickerValueChanged);
            _dateTimePicker.ValueNullableChanged += new EventHandler(OnDateTimePickerValueNullableChanged);
            _dateTimePicker.DropDown += new EventHandler<DateTimePickerDropArgs>(OnDateTimePickerDropDown);
            _dateTimePicker.CloseUp += new EventHandler<DateTimePickerCloseArgs>(OnDateTimePickerCloseUp);
            _dateTimePicker.CheckedChanged += new EventHandler(OnDateTimePickerCheckedChanged);
            _dateTimePicker.FormatChanged += new EventHandler(OnDateTimePickerFormatChanged);
            _dateTimePicker.GotFocus += new EventHandler(OnDateTimePickerGotFocus);
            _dateTimePicker.LostFocus += new EventHandler(OnDateTimePickerLostFocus);
            _dateTimePicker.KeyDown += new KeyEventHandler(OnDateTimePickerKeyDown);
            _dateTimePicker.KeyUp += new KeyEventHandler(OnDateTimePickerKeyUp);
            _dateTimePicker.KeyPress += new KeyPressEventHandler(OnDateTimePickerKeyPress);
            _dateTimePicker.PreviewKeyDown += new PreviewKeyDownEventHandler(OnDateTimePickerKeyDown);

            // Ensure we can track mouse events on the date time picker
            MonitorControl(_dateTimePicker);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dateTimePicker != null)
                {
                    UnmonitorControl(_dateTimePicker);
                    _dateTimePicker.Dispose();
                    _dateTimePicker = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the owning ribbon control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override KryptonRibbon Ribbon
        {
            set
            {
                base.Ribbon = value;

                if (value != null)
                {
                    // Use the same palette in the date time picker as the ribbon, plus we need
                    // to know when the ribbon palette changes so we can reflect that change
                    _dateTimePicker.Palette = Ribbon.GetResolvedPalette();
                    Ribbon.PaletteChanged += new EventHandler(OnRibbonPaletteChanged);
                }
            }
        }

        /// <summary>
        /// Gets and sets the shortcut key combination.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Shortcut key combination to set focus to the date time picker.")]
        public Keys ShortcutKeys
        {
            get { return _shortcutKeys; }
            set { _shortcutKeys = value; }
        }

        private bool ShouldSerializeShortcutKeys()
        {
            return (ShortcutKeys != Keys.None);
        }

        /// <summary>
        /// Resets the ShortcutKeys property to its default value.
        /// </summary>
        public void ResetShortcutKeys()
        {
            ShortcutKeys = Keys.None;
        }

        /// <summary>
        /// Access to the actual embedded KryptonDateTimePicker instance.
        /// </summary>
        [Description("Access to the actual embedded KryptonDateTimePicker instance.")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonDateTimePicker DateTimePicker
        {
            get { return _dateTimePicker; }
        }

        /// <summary>
        /// Gets and sets the key tip for the ribbon group date time picker.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group date time picker key tip.")]
        [DefaultValue("X")]
        public string KeyTip
        {
            get { return _keyTip; }

            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "X";

                _keyTip = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the date time picker.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the date time picker is visible or hidden.")]
        [DefaultValue(true)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool Visible
        {
            get { return _visible; }

            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        /// <summary>
        /// Make the ribbon group date time picker visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon group date time picker hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the group date time picker.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group date time picker is enabled.")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return _dateTimePicker.Enabled; }
            set { _dateTimePicker.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets the minimum size of the control.
        /// </summary>
        [Category("Layout")]
        [Description("Specifies the minimum size of the control.")]
        [DefaultValue(typeof(Size), "180, 0")]
        public Size MinimumSize
        {
            get { return _dateTimePicker.MinimumSize; }
            set { _dateTimePicker.MinimumSize = value; }
        }

        /// <summary>
        /// Gets or sets the maximum size of the control.
        /// </summary>
        [Category("Layout")]
        [Description("Specifies the maximum size of the control.")]
        [DefaultValue(typeof(Size), "180, 0")]
        public Size MaximumSize
        {
            get { return _dateTimePicker.MaximumSize; }
            set { _dateTimePicker.MaximumSize = value; }
        }

        /// <summary>
        /// Gets and sets the associated context menu strip.
        /// </summary>
        [Category("Behavior")]
        [Description("The shortcut to display when the user right-clicks the control.")]
        [DefaultValue(null)]
        public ContextMenuStrip ContextMenuStrip
        {
            get { return _dateTimePicker.ContextMenuStrip; }
            set { _dateTimePicker.ContextMenuStrip = value; }
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu for showing when the date time picker is right clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("KryptonContextMenu to be shown when the date time picker is right clicked.")]
        [DefaultValue(null)]
        public KryptonContextMenu KryptonContextMenu
        {
            get { return _dateTimePicker.KryptonContextMenu; }
            set { _dateTimePicker.KryptonContextMenu = value; }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _dateTimePicker.AllowButtonSpecToolTips; }
            set { _dateTimePicker.AllowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonDateTimePicker.DateTimePickerButtonSpecCollection ButtonSpecs
        {
            get { return _dateTimePicker.ButtonSpecs; }
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
            get { return _dateTimePicker.CalendarDimensions; }
            set { _dateTimePicker.CalendarDimensions = value; }
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
            get { return _dateTimePicker.CalendarTodayText; }
            set { _dateTimePicker.CalendarTodayText = value; }
        }

        private void ResetCalendarTodayText()
        {
            _dateTimePicker.ResetCalendarTodayText();
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
            get { return _dateTimePicker.CalendarFirstDayOfWeek; }
            set { _dateTimePicker.CalendarFirstDayOfWeek = value; }
        }

        /// <summary>
        /// Gets and sets if clicking the Today button closes the drop down menu.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates if clicking the Today button closes the drop down menu.")]
        [DefaultValue(false)]
        public bool CalendarCloseOnTodayClick
        {
            get { return _dateTimePicker.CalendarCloseOnTodayClick; }
            set { _dateTimePicker.CalendarCloseOnTodayClick = value; }
        }

        /// <summary>
        /// Gets and sets if the control will display todays date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will display todays date.")]
        [DefaultValue(true)]
        public bool CalendarShowToday
        {
            get { return _dateTimePicker.CalendarShowToday; }
            set { _dateTimePicker.CalendarShowToday = value; }
        }

        /// <summary>
        /// Gets and sets if the control will circle the today date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will circle the today date.")]
        [DefaultValue(true)]
        public bool CalendarShowTodayCircle
        {
            get { return _dateTimePicker.CalendarShowTodayCircle; }
            set { _dateTimePicker.CalendarShowTodayCircle = value; }
        }

        /// <summary>
        /// Gets and sets if week numbers to the left of each row.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates whether this month calendar will display week numbers to the left of each row.")]
        [DefaultValue(false)]
        public bool CalendarShowWeekNumbers
        {
            get { return _dateTimePicker.CalendarShowWeekNumbers; }
            set { _dateTimePicker.CalendarShowWeekNumbers = value; }
        }

        /// <summary>
        /// Gets or sets today's date.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Today's date.")]
        public DateTime CalendarTodayDate
        {
            get { return _dateTimePicker.CalendarTodayDate; }
            set { _dateTimePicker.CalendarTodayDate = value; }
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
            get { return _dateTimePicker.CalendarAnnuallyBoldedDates; }
            set { _dateTimePicker.CalendarAnnuallyBoldedDates = value; }
        }

        private void ResetCalendarAnnuallyBoldedDates()
        {
            CalendarAnnuallyBoldedDates = null;
        }

        private bool ShouldSerializeCalendarAnnuallyBoldedDates()
        {
            return _dateTimePicker.ShouldSerializeCalendarAnnuallyBoldedDates();
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which monthly dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarMonthlyBoldedDates
        {
            get { return _dateTimePicker.CalendarMonthlyBoldedDates; }
            set { _dateTimePicker.CalendarMonthlyBoldedDates = value; }
        }

        private void ResetCalendarMonthlyBoldedDates()
        {
            CalendarMonthlyBoldedDates = null;
        }

        private bool ShouldSerializeCalendarMonthlyBoldedDates()
        {
            return _dateTimePicker.ShouldSerializeCalendarMonthlyBoldedDates();
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are displayed in bold.
        /// </summary>
        [Category("MonthCalendar")]
        [Description("Indicates which dates should be boldface.")]
        [Localizable(true)]
        public DateTime[] CalendarBoldedDates
        {
            get { return _dateTimePicker.CalendarBoldedDates; }
            set { _dateTimePicker.CalendarBoldedDates = value; }
        }

        private void ResetCalendarBoldedDates()
        {
            CalendarBoldedDates = null;
        }

        private bool ShouldSerializeCalendarBoldedDates()
        {
            return _dateTimePicker.ShouldSerializeCalendarBoldedDates();
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
            get { return _dateTimePicker.DropDownAlign; }
            set { _dateTimePicker.DropDownAlign = value; }
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
            get { return _dateTimePicker.ValueNullable; }
            set { _dateTimePicker.ValueNullable = value; }
        }

        private void ResetValueNullable()
        {
            _dateTimePicker.ResetValueNullable();
        }

        private bool ShouldSerializeValueNullable()
        {
            return _dateTimePicker.ShouldSerializeValueNullable();
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
            get { return _dateTimePicker.Value; }
            set { _dateTimePicker.Value = value; }
        }

        private void ResetValue()
        {
            _dateTimePicker.ResetValue();
        }

        private bool ShouldSerializeValue()
        {
            return _dateTimePicker.ShouldSerializeValue();
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
            get { return _dateTimePicker.Format; }
            set { _dateTimePicker.Format = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a spin button control (also known as an up-down control) is used to adjust the date/time value.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates whether a spin box rather than a drop-down calendar is displayed for modifying the control value.")]
        [DefaultValue(false)]
        public bool ShowUpDown
        {
            get { return _dateTimePicker.ShowUpDown; }
            set { _dateTimePicker.ShowUpDown = value; }
        }

        /// <summary>
        /// Specifies whether to show the check box in the exception message box.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines whether a check box is displayed in the control. When the box is unchecked, no value is selected.")]
        [DefaultValue(false)]
        public bool ShowCheckBox
        {
            get { return _dateTimePicker.ShowCheckBox; }
            set { _dateTimePicker.ShowCheckBox = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether mnemonics will fire button spec buttons.
        /// </summary>
        [Category("Appearance")]
        [Description("Defines if mnemonic characters generate click events for button specs.")]
        [DefaultValue(true)]
        public bool UseMnemonic
        {
            get { return _dateTimePicker.UseMnemonic; }
            set { _dateTimePicker.UseMnemonic = value; }
        }

        /// <summary>
        /// Gets or sets the maximum date and time that can be selected in the control.
        /// </summary>
        [Category("Behavior")]
        [Description("Maximum allowable date.")]
        public DateTime MaxDate
        {
            get { return _dateTimePicker.MaxDate; }
            set { _dateTimePicker.MaxDate = value; }
        }

        private void ResetMaxDate()
        {
            MaxDate = DateTime.MaxValue;
        }

        private bool ShouldSerializeMaxDate()
        {
            return _dateTimePicker.ShouldSerializeMaxDate();
        }

        /// <summary>
        /// Gets or sets the minimum date and time that can be selected in the control.
        /// </summary>
        [Category("Behavior")]
        [Description("Minimum allowable date.")]
        public DateTime MinDate
        {
            get { return _dateTimePicker.MinDate; }
            set { _dateTimePicker.MinDate = value; }
        }

        private void ResetMinDate()
        {
            MinDate = DateTime.MinValue;
        }

        private bool ShouldSerializeMinDate()
        {
            return _dateTimePicker.ShouldSerializeMinDate();
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
            get { return _dateTimePicker.Checked; }
            set { _dateTimePicker.Checked = value; }
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
            get { return _dateTimePicker.CustomFormat; }
            set { _dateTimePicker.CustomFormat = value; }
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
            get { return _dateTimePicker.CustomNullText; }
            set { _dateTimePicker.CustomNullText = value; }
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
            get { return _dateTimePicker.CalendarTodayFormat; }
            set { _dateTimePicker.CalendarTodayFormat = value; }
        }

        /// <summary>
        /// Gets and sets the header style for the month calendar.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Header style for the month calendar.")]
        public HeaderStyle CalendarHeaderStyle
        {
            get { return _dateTimePicker.CalendarHeaderStyle; }
            set { _dateTimePicker.CalendarHeaderStyle = value; }
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
            get { return _dateTimePicker.CalendarDayStyle; }
            set { _dateTimePicker.CalendarDayStyle = value; }
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
            get { return _dateTimePicker.CalendarDayOfWeekStyle; }
            set { _dateTimePicker.CalendarDayOfWeekStyle = value; }
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
        /// Gets and sets the maximum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeMaximum
        {
            get { return GroupItemSize.Large; }
            set { }
        }

        /// <summary>
        /// Gets and sets the minimum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeMinimum
        {
            get { return GroupItemSize.Small; }
            set { }
        }

        /// <summary>
        /// Gets and sets the current item size.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeCurrent
        {
            get { return _itemSizeCurrent; }

            set
            {
                if (_itemSizeCurrent != value)
                {
                    _itemSizeCurrent = value;
                    OnPropertyChanged("ItemSizeCurrent");
                }
            }
        }

        /// <summary>
        /// Creates an appropriate view element for this item.
        /// </summary>
        /// <param name="ribbon">Reference to the owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying changes in display.</param>
        /// <returns>ViewBase derived instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ViewBase CreateView(KryptonRibbon ribbon, 
                                            NeedPaintHandler needPaint)
        {
            return new ViewDrawRibbonGroupDateTimePicker(ribbon, this, needPaint);
        }

        /// <summary>
        /// Gets and sets the associated designer.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public IKryptonDesignObject DateTimePickerDesigner
        {
            get { return _designer; }
            set { _designer = value; }
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase DateTimePickerView
        {
            get { return _dateTimePickerView; }
            set { _dateTimePickerView = value; }
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnGotFocus(EventArgs e)
        {
            if (GotFocus != null)
                GotFocus(this, e);
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnLostFocus(EventArgs e)
        {
            if (LostFocus != null)
                LostFocus(this, e);
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
        /// Raises the CloseUp event.
        /// </summary>
        /// <param name="e">An DateTimePickerCloseArgs containing the event data.</param>
        protected virtual void OnCloseUp(DateTimePickerCloseArgs e)
        {
            if (CloseUp != null)
                CloseUp(this, e);
        }

        /// <summary>
        /// Raises the DropDown event.
        /// </summary>
        /// <param name="e">An DateTimePickerDropArgs containing the event data.</param>
        protected virtual void OnDropDown(DateTimePickerDropArgs e)
        {
            if (DropDown != null)
                DropDown(this, e);
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
        /// Raises the ValueChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        /// <summary>
        /// Raises the KeyDown event.
        /// </summary>
        /// <param name="e">An KeyEventArgs containing the event data.</param>
        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            if (KeyDown != null)
                KeyDown(this, e);
        }

        /// <summary>
        /// Raises the KeyUp event.
        /// </summary>
        /// <param name="e">An KeyEventArgs containing the event data.</param>
        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            if (KeyUp != null)
                KeyUp(this, e);
        }

        /// <summary>
        /// Raises the KeyPress event.
        /// </summary>
        /// <param name="e">An KeyPressEventArgs containing the event data.</param>
        protected virtual void OnKeyPress(KeyPressEventArgs e)
        {
            if (KeyPress != null)
                KeyPress(this, e);
        }

        /// <summary>
        /// Raises the PreviewKeyDown event.
        /// </summary>
        /// <param name="e">An PreviewKeyDownEventArgs containing the event data.</param>
        protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (PreviewKeyDown != null)
                PreviewKeyDown(this, e);
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of property that has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Internal
        internal Control LastParentControl
        {
            get { return _lastParentControl; }
            set { _lastParentControl = value; }
        }

        internal KryptonDateTimePicker LastDateTimePicker
        {
            get { return _lastDateTimePicker; }
            set { _lastDateTimePicker = value; }
        }

        internal NeedPaintHandler ViewPaintDelegate
        {
            get { return _viewPaintDelegate; }
            set { _viewPaintDelegate = value; }
        }

        internal void OnDesignTimeContextMenu(MouseEventArgs e)
        {
            if (DesignTimeContextMenu != null)
                DesignTimeContextMenu(this, e);
        }

        internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Only interested in key processing if this control definition 
            // is enabled and itself and all parents are also visible
            if (Enabled && ChainVisible)
            {
                // Do we have a shortcut definition for ourself?
                if (ShortcutKeys != Keys.None)
                {
                    // Does it match the incoming key combination?
                    if (ShortcutKeys == keyData)
                    {
                        // Can the date time picker take the focus
                        if ((LastDateTimePicker != null) && (LastDateTimePicker.CanFocus))
                            LastDateTimePicker.Focus();

                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region Implementation
        private void MonitorControl(KryptonDateTimePicker c)
        {
            c.MouseEnter += new EventHandler(OnControlEnter);
            c.MouseLeave += new EventHandler(OnControlLeave);
        }

        private void UnmonitorControl(KryptonDateTimePicker c)
        {
            c.MouseEnter -= new EventHandler(OnControlEnter);
            c.MouseLeave -= new EventHandler(OnControlLeave);
        }

        private void OnControlEnter(object sender, EventArgs e)
        {
            if (MouseEnterControl != null)
                MouseEnterControl(this, e);
        }

        private void OnControlLeave(object sender, EventArgs e)
        {
            if (MouseLeaveControl != null)
                MouseLeaveControl(this, e);
        }

        private void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // Pass request onto the view provided paint delegate
            if (_viewPaintDelegate != null)
                _viewPaintDelegate(this, e);
        }

        private void OnDateTimePickerGotFocus(object sender, EventArgs e)
        {
            OnGotFocus(e);
        }

        private void OnDateTimePickerLostFocus(object sender, EventArgs e)
        {
            OnLostFocus(e);
        }

        private void OnDateTimePickerFormatChanged(object sender, EventArgs e)
        {
            OnFormatChanged(e);
        }

        private void OnDateTimePickerCheckedChanged(object sender, EventArgs e)
        {
            OnCheckedChanged(e);
        }

        private void OnDateTimePickerCloseUp(object sender, DateTimePickerCloseArgs e)
        {
            OnCloseUp(e);
        }

        private void OnDateTimePickerDropDown(object sender, DateTimePickerDropArgs e)
        {
            OnDropDown(e);
        }

        private void OnDateTimePickerValueNullableChanged(object sender, EventArgs e)
        {
            OnValueNullableChanged(e);
        }

        private void OnDateTimePickerValueChanged(object sender, EventArgs e)
        {
            OnValueChanged(e);
        }

        private void OnDateTimePickerKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void OnDateTimePickerKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void OnDateTimePickerKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void OnDateTimePickerKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OnPreviewKeyDown(e);
        }

        private void OnRibbonPaletteChanged(object sender, EventArgs e)
        {
            _dateTimePicker.Palette = Ribbon.GetResolvedPalette();
        }
        #endregion
    }
}
