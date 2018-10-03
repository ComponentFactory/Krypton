// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace KryptonDateTimePickerExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this date time picker control
            propertyGrid.SelectedObject = new KryptonDateTimePickerProxy(dtpNormalLong);
        }
        
        private void dtp_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this date time picker control
            propertyGrid.SelectedObject = new KryptonDateTimePickerProxy(sender as KryptonDateTimePicker);
        }

        private void rbOffice2010Blue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2010Blue;
        }

        private void rbOffice2010Silver_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2010Silver;
        }

        private void rbOffice2010Black_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2010Black;
        }

        private void rbOffice2007Blue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2007Blue;
        }

        private void rbOffice2007Silver_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2007Silver;
        }

        private void rbOffice2007Black_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2007Black;
        }

        private void rbSparkleBlue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.SparkleBlue;
        }

        private void rbSparkleOrange_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.SparkleOrange;
        }

        private void rbSparklePurple_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.SparklePurple;
        }

        private void rbOffice2003_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.ProfessionalOffice2003;
        }

        private void rbSystem_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.ProfessionalSystem;
        }

        private void buttonSpecAny1_Click(object sender, EventArgs e)
        {
            dtpNormalTime.Value = DateTime.Now;
        }

        private void buttonSpecAny2_Click(object sender, EventArgs e)
        {
            dtpRibbonTime.Value = DateTime.Now;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonDateTimePickerProxy
    {
        private KryptonDateTimePicker _dateTimePicker;

        public KryptonDateTimePickerProxy(KryptonDateTimePicker dateTimePicker)
        {
            _dateTimePicker = dateTimePicker;
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _dateTimePicker.Size; }
            set { _dateTimePicker.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _dateTimePicker.Location; }
            set { _dateTimePicker.Location = value; }
        }

        /// <summary>
        /// Gets or sets the number of columns and rows of months displayed. 
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Specifies the number of rows and columns of months displayed.")]
        public Size CalendarDimensions
        {
            get { return _dateTimePicker.CalendarDimensions; }
            set { _dateTimePicker.CalendarDimensions = value; }
        }

        /// <summary>
        /// Gets or sets the label text for todays text. 
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Text used as label for todays date.")]
        public string CalendarTodayText
        {
            get { return _dateTimePicker.CalendarTodayText; }
            set { _dateTimePicker.CalendarTodayText = value; }
        }

        /// <summary>
        /// First day of the week.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("First day of the week.")]
        public Day CalendarFirstDayOfWeek
        {
            get { return _dateTimePicker.CalendarFirstDayOfWeek; }
            set { _dateTimePicker.CalendarFirstDayOfWeek = value; }
        }

        /// <summary>
        /// Gets and sets if the control will display todays date.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Indicates whether this month calendar will display todays date.")]
        public bool CalendarShowToday
        {
            get { return _dateTimePicker.CalendarShowToday; }
            set { _dateTimePicker.CalendarShowToday = value; }
        }

        /// <summary>
        /// Gets and sets if the control will circle the today date.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Indicates whether this month calendar will circle the today date.")]
        public bool CalendarShowTodayCircle
        {
            get { return _dateTimePicker.CalendarShowTodayCircle; }
            set { _dateTimePicker.CalendarShowTodayCircle = value; }
        }

        /// <summary>
        /// Gets and sets if week numbers to the left of each row.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Indicates whether this month calendar will display week numbers to the left of each row.")]
        public bool CalendarShowWeekNumbers
        {
            get { return _dateTimePicker.CalendarShowWeekNumbers; }
            set { _dateTimePicker.CalendarShowWeekNumbers = value; }
        }

        /// <summary>
        /// Gets or sets today's display format.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Today's display format.")]
        public string CalendarTodayFormat
        {
            get { return _dateTimePicker.CalendarTodayFormat; }
            set { _dateTimePicker.CalendarTodayFormat = value; }
        }

        /// <summary>
        /// Gets or sets today's date.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Today's date.")]
        public DateTime CalendarTodayDate
        {
            get { return _dateTimePicker.CalendarTodayDate; }
            set { _dateTimePicker.CalendarTodayDate = value; }
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which annual days are displayed in bold.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Indicates which annual dates should be boldface.")]
        public DateTime[] CalendarAnnuallyBoldedDates
        {
            get { return _dateTimePicker.CalendarAnnuallyBoldedDates; }
            set { _dateTimePicker.CalendarAnnuallyBoldedDates = value; }
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determine which monthly days to bold. 
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Indicates which monthly dates should be boldface.")]
        public DateTime[] CalendarMonthlyBoldedDates
        {
            get { return _dateTimePicker.CalendarMonthlyBoldedDates; }
            set { _dateTimePicker.CalendarMonthlyBoldedDates = value; }
        }

        /// <summary>
        /// Gets or sets the array of DateTime objects that determines which nonrecurring dates are displayed in bold.
        /// </summary>
        [Category("Visuals - MonthCalendar")]
        [Description("Indicates which dates should be boldface.")]
        public DateTime[] CalendarBoldedDates
        {
            get { return _dateTimePicker.CalendarBoldedDates; }
            set { _dateTimePicker.CalendarBoldedDates = value; }
        }

        /// <summary>
        /// Gets or sets the alignment of the drop-down calendar on the DateTimePicker control.
        /// </summary>
        [Category("Appearance")]
        [Description("Alignment of the drop-down calendar on the KryptonDateTimePicker control.")]
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
        [RefreshProperties(RefreshProperties.All)]
        public object ValueNullable
        {
            get { return _dateTimePicker.ValueNullable; }
            set { _dateTimePicker.ValueNullable = value; }
        }

        /// <summary>
        /// Gets or sets the date/time value assigned to the control..
        /// </summary>
        [Category("Appearance")]
        [Description("Property for the date/time.")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime Value
        {
            get { return _dateTimePicker.Value; }
            set { _dateTimePicker.Value = value; }
        }

        /// <summary>
        /// Gets or sets the format of the date and time displayed in the control.
        /// </summary>
        [Category("Appearance")]
        [Description("Determines whether dates and times are displayed using standard or custom formatting.")]
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

        /// <summary>
        /// Gets or sets a value indicating if the check box is checked and if the ValueNullable is DBNull or a DateTime value.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if the check box is checked and if the ValueNullable is DBNull or a DateTime value.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
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
        public string CustomFormat
        {
            get { return _dateTimePicker.CustomFormat; }
            set { _dateTimePicker.CustomFormat = value; }
        }

        /// <summary>
        /// Gets or sets the custom text used when the checked property is cleared.
        /// </summary>
        [Category("Behavior")]
        [Description("The custom text used when the checked property is cleared.")]
        [DefaultValue("")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public string CustomNullText
        {
            get { return _dateTimePicker.CustomNullText; }
            set { _dateTimePicker.CustomNullText = value; }
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

        /// <summary>
        /// Gets or sets the palette to be applied.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public PaletteMode PaletteMode
        {
            get { return _dateTimePicker.PaletteMode; }
            set { _dateTimePicker.PaletteMode = value; }
        }

        /// <summary>
        /// Gets and sets the custom palette implementation.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Custom palette applied to drawing.")]
        [DefaultValue(null)]
        public IPalette Palette
        {
            get { return _dateTimePicker.Palette; }
            set { _dateTimePicker.Palette = value; }
        }

        /// <summary>
        /// Gets and sets Determines if the control is always active or only when the mouse is over the control or has focus.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Determines if the control is always active or only when the mouse is over the control or has focus.")]
        [DefaultValue(true)]
        public bool AlwaysActive
        {
            get { return _dateTimePicker.AlwaysActive; }
            set { _dateTimePicker.AlwaysActive = value; }
        }

        /// <summary>
        /// Gets access to the checkbox value overrides.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("CheckBox image overrides.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CheckBoxImages Images
        {
            get { return _dateTimePicker.Images; }
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Input control style.")]
        public InputControlStyle InputControlStyle
        {
            get { return _dateTimePicker.InputControlStyle; }
            set { _dateTimePicker.InputControlStyle = value; }
        }

        /// <summary>
        /// Gets and sets the up and down buttons style.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Up and down buttons style.")]
        public ButtonStyle UpDownButtonStyle
        {
            get { return _dateTimePicker.UpDownButtonStyle; }
            set { _dateTimePicker.UpDownButtonStyle = value; }
        }

        /// <summary>
        /// Gets and sets the drop button style.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("DropButton style.")]
        public ButtonStyle DropButtonStyle
        {
            get { return _dateTimePicker.DropButtonStyle; }
            set { _dateTimePicker.DropButtonStyle = value; }
        }

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Collection of button specifications.")]
        public KryptonDateTimePicker.DateTimePickerButtonSpecCollection ButtonSpecs
        {
            get { return _dateTimePicker.ButtonSpecs; }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _dateTimePicker.AllowButtonSpecToolTips; }
            set { _dateTimePicker.AllowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Gets access to the common date time picker appearance entries that other states can override.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Overrides for defining common date time picker appearance that other states can override.")]
        public PaletteInputControlTripleRedirect StateCommon
        {
            get { return _dateTimePicker.StateCommon; }
        }

        /// <summary>
        /// Gets access to the disabled date time picker appearance entries.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Overrides for defining disabled date time picker appearance.")]
        public PaletteInputControlTripleStates StateDisabled
        {
            get { return _dateTimePicker.StateDisabled; }
        }

        /// <summary>
        /// Gets access to the normal date time picker appearance entries.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Overrides for defining normal date time picker appearance.")]
        public PaletteInputControlTripleStates StateNormal
        {
            get { return _dateTimePicker.StateNormal; }
        }

        /// <summary>
        /// Gets access to the active date time picker appearance entries.
        /// </summary>
        [Category("Visuals - DateTimePicker")]
        [Description("Overrides for defining active date time picker appearance.")]
        public PaletteInputControlTripleStates StateActive
        {
            get { return _dateTimePicker.StateActive; }
        }
    }
}
