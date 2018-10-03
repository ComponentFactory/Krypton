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
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Extends the ViewComposite by creating/destroying month instances in a grid.
	/// </summary>
	public class ViewLayoutMonths : ViewComposite, 
                                    IContentValues
    {
        #region Type Definitions
        #endregion

        #region Static Fields
        internal static readonly int GAP = 2;
        #endregion

        #region Instance Fields
        private IContextMenuProvider _provider;
        private IKryptonMonthCalendar _calendar;
        private ViewDrawDocker _drawHeader;
        private PaletteBorderInheritForced _borderForced;
        private MonthCalendarButtonSpecCollection _buttonSpecs;
        private ButtonSpecManagerDraw _buttonManager;
        private VisualPopupToolTip _visualPopupToolTip;
        private ViewDrawToday _drawToday;
        private ButtonSpecRemapByContentView _remapPalette;
        private ViewDrawEmptyContent _emptyContent;
        private PaletteTripleRedirect _palette;
        private ToolTipManager _toolTipManager;
        private CultureInfo _lastCultureInfo;
        private DateTime _displayMonth;
        private DayOfWeek _displayDayOfWeek;
        private string[] _dayNames;
        private string _dayOfWeekMeasure;
        private string _dayMeasure;
        private string _shortText;
        private Size _sizeDayOfWeek;
        private Size _sizeDay;
        private DateTime _oldSelectionStart;
        private DateTime _oldSelectionEnd;
        private DateTime? _oldFocusDay;
        private DateTime? _trackingDay;
        private DateTime? _anchorDay;
        private NeedPaintHandler _needPaintDelegate;
        private PaletteRedirect _redirector;
        private bool _showWeekNumbers;
        private bool _showTodayCircle;
        private bool _showToday;
        private bool _closeOnTodayClick;
        private bool _allowButtonSpecToolTips;
        private bool _firstTimeSync;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewLayoutMonths class.
		/// </summary>
        /// <param name="provider">Provider of context menu information.</param>
        /// <param name="monthCalendar">Reference to owning month calendar entry.</param>
        /// <param name="viewManager">Owning view manager instance.</param>
        /// <param name="calendar">Reference to calendar provider.</param>
        /// <param name="redirector">Redirector for getting values.</param>
        /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
        public ViewLayoutMonths(IContextMenuProvider provider,
                                KryptonContextMenuMonthCalendar monthCalendar,
                                ViewContextMenuManager viewManager,
                                IKryptonMonthCalendar calendar,
                                PaletteRedirect redirector,
                                NeedPaintHandler needPaintDelegate)
        {
            _provider = provider;
            _calendar = calendar;
            _oldSelectionStart = _calendar.SelectionStart;
            _oldSelectionEnd = _calendar.SelectionEnd;
            _displayMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _redirector = redirector;
            _needPaintDelegate = needPaintDelegate;
            _showToday = true;
            _showTodayCircle = true;
            _closeOnTodayClick = false;
            _firstTimeSync = true;
            _allowButtonSpecToolTips = false;

            // Use a controller that can work against all the displayed months
            MonthCalendarController controller = new MonthCalendarController(monthCalendar, viewManager, this, _needPaintDelegate);
            MouseController = controller;
            SourceController = controller;
            KeyController = controller;

            _borderForced = new PaletteBorderInheritForced(_calendar.StateNormal.Header.Border);
            _borderForced.ForceBorderEdges(PaletteDrawBorders.None);
            _drawHeader = new ViewDrawDocker(_calendar.StateNormal.Header.Back, _borderForced, null);
            _emptyContent = new ViewDrawEmptyContent(_calendar.StateDisabled.Header.Content, _calendar.StateNormal.Header.Content);
            _drawHeader.Add(_emptyContent, ViewDockStyle.Fill);
            Add(_drawHeader);

            // Using a button spec manager to add the buttons to the header
            _buttonSpecs = new MonthCalendarButtonSpecCollection(this);
            _buttonManager = new ButtonSpecManagerDraw(_calendar.CalendarControl, redirector, _buttonSpecs, null,
                                                       new ViewDrawDocker[] { _drawHeader },
                                                       new IPaletteMetric[] { _calendar.StateCommon },
                                                       new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetCalendar },
                                                       new PaletteMetricPadding[] { PaletteMetricPadding.None },
                                                       _calendar.GetToolStripDelegate, _needPaintDelegate);

            // Create the manager for handling tooltips
            _toolTipManager = new ToolTipManager();
            _toolTipManager.ShowToolTip += new EventHandler<ToolTipEventArgs>(OnShowToolTip);
            _toolTipManager.CancelToolTip += new EventHandler(OnCancelToolTip);
            _buttonManager.ToolTipManager = _toolTipManager;

            // Create the bottom header used for showing 'today' and defined button specs
            _remapPalette = (ButtonSpecRemapByContentView)_buttonManager.CreateButtonSpecRemap(redirector, new ButtonSpecAny());
            _remapPalette.Foreground = _emptyContent;

            // Use a redirector to get button values directly from palette
            _palette = new PaletteTripleRedirect(_remapPalette,
                                                 PaletteBackStyle.ButtonButtonSpec,
                                                 PaletteBorderStyle.ButtonButtonSpec,
                                                 PaletteContentStyle.ButtonButtonSpec,
                                                 _needPaintDelegate);

            _drawToday = new ViewDrawToday(_calendar, _palette, _palette, _palette, _palette, _needPaintDelegate);
            _drawToday.Click += new EventHandler(OnTodayClick);
            _drawHeader.Add(_drawToday, ViewDockStyle.Left);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutMonths:" + Id;
		}
		#endregion

        #region Public
        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        public bool AllowButtonSpecToolTips
        {
            get { return _allowButtonSpecToolTips; }
            set { _allowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Gets access to the button manager.
        /// </summary>
        public ButtonSpecManagerDraw ButtonManager
        {
            get { return _buttonManager; }
        }

        /// <summary>
        /// Gets access to the collection of button spec definitions.
        /// </summary>
        public MonthCalendarButtonSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Recreate the set of button spec instances.
        /// </summary>
        public void RecreateButtons()
        {
            _buttonManager.RecreateButtons();
        }

        /// <summary>
        /// Gets access to the month calendar.
        /// </summary>
        public IKryptonMonthCalendar Calendar
        {
            get { return _calendar; }
        }

        /// <summary>
        /// Gets access to the optional context menu provider.
        /// </summary>
        public IContextMenuProvider Provider
        {
            get { return _provider; }
        }

        /// <summary>
        /// Gets and sets the day that is currently being tracked.
        /// </summary>
        public DateTime? TrackingDay
        {
            get { return _trackingDay; }

            set
            {
                if (value != _trackingDay)
                {
                    _needPaintDelegate(this, new NeedLayoutEventArgs(false));
                    _trackingDay = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the day that is currently showing focus.
        /// </summary>
        public DateTime? FocusDay
        {
            get { return _calendar.FocusDay; }
            set { _calendar.FocusDay = value; }
        }

        /// <summary>
        /// Gets and sets the day that is the anchor for shift changes.
        /// </summary>
        public DateTime? AnchorDay
        {
            get { return _anchorDay; }

            set
            {
                if (value != _anchorDay)
                {
                    _needPaintDelegate(this, new NeedLayoutEventArgs(true));
                    _anchorDay = value;
                }
            }
        }

        /// <summary>
        /// Gets and set the display of a circle for todays date.
        /// </summary>
        public bool ShowTodayCircle
        {
            get { return _showTodayCircle; }

            set
            {
                if (value != _showTodayCircle)
                {
                    _needPaintDelegate(this, new NeedLayoutEventArgs(true));
                    _showTodayCircle = value;
                }
            }
        }

        /// <summary>
        /// Gets and set the display of todays date.
        /// </summary>
        public bool ShowToday
        {
            get { return _showToday; }

            set
            {
                if (value != _showToday)
                {
                    _needPaintDelegate(this, new NeedLayoutEventArgs(true));
                    _showToday = value;
                }
            }
        }

        /// <summary>
        /// Gets and set if the menu is closed when the today button is pressed.
        /// </summary>
        public bool CloseOnTodayClick
        {
            get { return _closeOnTodayClick; }
            set { _closeOnTodayClick = value; }
        }

        /// <summary>
        /// Gets and sets the showing of week numbers.
        /// </summary>
        public bool ShowWeekNumbers
        {
            get { return _showWeekNumbers; }

            set
            {
                if (value != _showWeekNumbers)
                {
                    _needPaintDelegate(this, new NeedLayoutEventArgs(true));
                    _showWeekNumbers = value;
                }
            }
        }

        /// <summary>
        /// Gets the number of display months.
        /// </summary>
        public int Months
        {
            get
            {
                return _calendar.CalendarDimensions.Width *
                       _calendar.CalendarDimensions.Height;
            }
        }

        /// <summary>
        /// Process a key down by finding the correct month and calling the associated key controller.
        /// </summary>
        /// <param name="c">Owning control.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <returns>True if the key was processed; otherwise false.</returns>
        public bool ProcessKeyDown(Control c, KeyEventArgs e)
        {
            // We must have a focused day
            if (FocusDay != null)
            {
                KeyController.KeyDown(c, e);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Gets the button for the day that is nearest (date wise) to the point provided.
        /// </summary>
        /// <param name="pt">Point to lookup.</param>
        /// <returns>DateTime for nearest matching day.</returns>
        public DateTime DayNearPoint(Point pt)
        {
            // Search for an exact matching month view
            foreach (ViewBase view in this)
            {
                ViewDrawMonth month = view as ViewDrawMonth;
                if ((month != null) && month.ClientRectangle.Contains(pt))
                    return month.ViewDrawMonthDays.DayNearPoint(pt);
            }

            int cols = _calendar.CalendarDimensions.Width;
            int rows = _calendar.CalendarDimensions.Height;
            int ptCol = cols - 1;
            int ptRow = rows - 1;

            // Find the column to be used in lookup
            for (int col = 0; col < cols; col++)
                if (pt.X < this[col + 1].ClientRectangle.Right)
                {
                    ptCol = col;
                    break;
                }

            // Find the row to be used in lookup
            for (int row = 0; row < rows; row++)
                if (pt.Y < this[(row * cols) + 1].ClientRectangle.Bottom)
                {
                    ptRow = row;
                    break;
                }

            ViewDrawMonth target = ((ViewDrawMonth)this[(ptCol + (ptRow * cols)) + 1]);
            return target.ViewDrawMonthDays.DayNearPoint(pt);
        }
    
        /// <summary>
        /// Gets the button for the day that is under the provided point.
        /// </summary>
        /// <param name="pt">Point to lookup.</param>
        /// <param name="exact">Exact requires that the day must be with the month range.</param>
        /// <returns>DateTime for matching day; otherwise null.</returns>
        public DateTime? DayFromPoint(Point pt, bool exact)
        {
            // Get the bottom most view element matching the point
            ViewBase view = ViewFromPoint(pt);

            // Climb view hierarchy looking for the days view 
            while (view != null)
            {
                ViewDrawMonthDays month = view as ViewDrawMonthDays;
                if ((month != null) && month.ClientRectangle.Contains(pt))
                    return month.DayFromPoint(pt, exact);

                view = view.Parent;
            }

            return null;
        }

        /// <summary>
        /// Move to the next month.
        /// </summary>
        public void NextMonth()
        {
            // Get the number of months to move
            int move = _calendar.ScrollChange;
            if (move == 0)
                move = 1;

            // Calculate the next set of months shown
            DateTime nextMonth = _displayMonth.AddMonths(move);
            DateTime lastDate = nextMonth.AddMonths(_calendar.CalendarDimensions.Width * 
                                                    _calendar.CalendarDimensions.Height);

            DateTime ld = lastDate.AddDays(-1);
            DateTime ldofm = LastDayOfMonth(_calendar.MaxDate);

            // We do not move the month if doing so moves it past the maximum date
            if (lastDate.AddDays(-1) <= LastDayOfMonth(_calendar.MaxDate))
            {
                // Use the newly calculated month
                _displayMonth = nextMonth;

                // If the end of the selection is no longer visible
                if (_calendar.SelectionEnd < _displayMonth)
                {
                    // Find new selection dates
                    DateTime newSelStart = _calendar.SelectionStart.AddMonths(move);
                    DateTime newSelEnd = _calendar.SelectionEnd.AddMonths(move);

                    // Impose the min/max dates
                    if (newSelStart > _calendar.MaxDate) newSelStart = _calendar.MaxDate;
                    if (newSelEnd > _calendar.MaxDate) newSelEnd = _calendar.MaxDate;

                    // Shift selection onwards
                    _calendar.SetSelectionRange(newSelStart, newSelEnd);
                }

                _needPaintDelegate(this, new NeedLayoutEventArgs(true));
            }
        }

        /// <summary>
        /// Move to the previous month.
        /// </summary>
        public void PrevMonth()
        {
            // Get the number of months to move
            int move = _calendar.ScrollChange;
            if (move == 0)
                move = 1;

            // Calculate the next set of months shown
            DateTime prevMonth = _displayMonth.AddMonths(-move);

            // We do not move the month if doing so moves it past the maximum date
            if (prevMonth >= FirstDayOfMonth(_calendar.MinDate))
            {
                // Use the newly calculated month
                _displayMonth = prevMonth;

                DateTime lastDate = _displayMonth.AddMonths(_calendar.CalendarDimensions.Width *
                                                            _calendar.CalendarDimensions.Height);

                // If the start of the selection is no longer visible
                if (_calendar.SelectionStart >= lastDate)
                {
                    // Find new selection dates
                    DateTime newSelStart = _calendar.SelectionStart.AddMonths(-move);
                    DateTime newSelEnd = _calendar.SelectionEnd.AddMonths(-move);

                    // Impose the min/max dates
                    if (newSelStart < _calendar.MinDate)    newSelStart = _calendar.MinDate;
                    if (newSelEnd < _calendar.MinDate)      newSelEnd = _calendar.MinDate;

                    // Shift selection backwards
                    _calendar.SetSelectionRange(newSelStart, newSelEnd);
                }

                _needPaintDelegate(this, new NeedLayoutEventArgs(true));
            }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Gets the size required to draw a single month.
        /// </summary>
		/// <param name="context">Layout context.</param>
        public Size GetSingleMonthSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            SyncData(context);
            SyncMonths();

            return this[1].GetPreferredSize(context);
        }

        /// <summary>
        /// Gets the size required to draw extra elements such as headers.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public Size GetExtraSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            if (_drawHeader.Visible)
            {
                Size retSize = _drawHeader.GetPreferredSize(context);
                retSize.Width = 0;
                retSize.Height += GAP * 2;
                return retSize;
            }
            else
                return Size.Empty;
        }

        /// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override Size GetPreferredSize(ViewLayoutContext context)
		{
			Debug.Assert(context != null);

            SyncData(context);
            SyncMonths();

            Size preferredSize = Size.Empty;

            // Is there a today header to be measured?
            if (_drawHeader.Visible)
            {
                // Measure size of the header
                Size headerSize = _drawHeader.GetPreferredSize(context);

                // Only use the height as the width is based on the months only
                preferredSize.Height = headerSize.Height + GAP * 2;
            }

            // Are there any months to be measured?
            if (Count > 1)
            {
                // Only need to measure the first child as all children must be the same size
                Size monthSize = this[1].GetPreferredSize(context);

                // Find total width based on requested dimensions and add a single pixel space around and between months
                preferredSize.Width += monthSize.Width * _calendar.CalendarDimensions.Width + (GAP * _calendar.CalendarDimensions.Width) + GAP;
                preferredSize.Height += monthSize.Height * _calendar.CalendarDimensions.Height + (GAP * _calendar.CalendarDimensions.Height) + GAP;
            }

            return preferredSize;
		}

		/// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override void Layout(ViewLayoutContext context)
		{
			Debug.Assert(context != null);

            SyncData(context);
            SyncMonths();
            
            // We take on all the available display area
			ClientRectangle = context.DisplayRectangle;

            // Is there a today header to layout?
            if (_drawHeader.Visible)
            {
                // Measure the required size of the header
                Size headerSize = _drawHeader.GetPreferredSize(context);

                // Position the header a the bottom of the area
                context.DisplayRectangle = new Rectangle(ClientLocation.X + GAP, ClientRectangle.Bottom - GAP - headerSize.Height,
                                                         ClientSize.Width - GAP * 2, headerSize.Height);

                _drawHeader.Layout(context);
            }

            // Are there any month views to layout?
            if (Count > 1)
            {
                // Only need to measure the first child as all children must be the same size
                Size monthSize = this[1].GetPreferredSize(context);

                // Position each child within the required grid
                Size dimensions = _calendar.CalendarDimensions;
                for (int y = 0, index = 1; y < dimensions.Height; y++)
                {
                    for (int x = 0; x < dimensions.Width; x++)
                    {
                        context.DisplayRectangle = new Rectangle(ClientLocation.X + (x * monthSize.Width) + (GAP * (x + 1)),
                                                                 ClientLocation.Y + (y * monthSize.Height) + (GAP * (y + 1)),
                                                                 monthSize.Width, monthSize.Height);

                        this[index++].Layout(context);
                    }
                }
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
		#endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText()
        {
            return _shortText;
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText()
        {
            return string.Empty;
        }
        #endregion

        #region Internal
        internal Size SizeDays
        {
            get { return _sizeDayOfWeek; }
        }

        internal Size SizeDay
        {
            get { return _sizeDay; }
        }

        internal DayOfWeek DisplayDayOfWeek
        {
            get { return _displayDayOfWeek; }
        }

        internal string[] DayNames
        {
            get { return _dayNames; }
        }
        #endregion

        #region Private
        private DateTime JustDay(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }

        private void OnTodayClick(object sender, EventArgs e)
        {
            // Remove any time information as selecting a date is based only on the day
            DateTime today = _calendar.TodayDate;

            // Can only set a date that is within the valid min/max range
            if ((today >= _calendar.MinDate) && (today <= _calendar.MaxDate))
            {
                _calendar.SetSelectionRange(today, today);
                _needPaintDelegate(this, new NeedLayoutEventArgs(true));
            }

            // Is the menu capable of being closed?
            if (CloseOnTodayClick && (Provider != null) && Provider.ProviderCanCloseMenu)
            {
                // Ask the original context menu definition, if we can close
                CancelEventArgs cea = new CancelEventArgs();
                Provider.OnClosing(cea);

                if (!cea.Cancel)
                {
                    // Close the menu from display and pass in the item clicked as the reason
                    Provider.OnClose(new CloseReasonEventArgs(ToolStripDropDownCloseReason.ItemClicked));
                }
            }
        }

        private void SyncData(ViewLayoutContext context)
        {
            // A change in culture means we need to recache information
            if ((_lastCultureInfo == null) || (_lastCultureInfo != CultureInfo.CurrentCulture))
            {
                _lastCultureInfo = CultureInfo.CurrentCulture;
                _needPaintDelegate(this, new NeedLayoutEventArgs(true));
                _dayNames = null;
            }

            if (_dayNames == null)
            {
                // Grab the names for each day of the week
                _dayNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
                _dayOfWeekMeasure = new string('W', Math.Max(3, _dayNames[0].Length));
                _dayMeasure = "WW";
            }

            if (_calendar.FirstDayOfWeek == Day.Default)
                _displayDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            else
                _displayDayOfWeek = (DayOfWeek)((((int)_calendar.FirstDayOfWeek) + 1) % 6);

            // Find the grid cell sizes needed for day names and day entries
            _sizeDayOfWeek = MaxGridCellDayOfWeek(context);
            _sizeDay = MaxGridCellDay(context);
        }

        private void SyncMonths()
        {
            // We need the today header if we show the today button or a button spec
            this[0].Visible = _showToday || (_buttonSpecs.Count > 0);
            this[0].Enabled = Enabled;
            _drawToday.Visible = _showToday;

            // How many month children instances do we need?
            int months = Months;

            // Do we need to create more month view?
            if (Count < (months + 1))
            {
                for (int i = Count - 1; i < months; i++)
                    Add(new ViewDrawMonth(_calendar, this, _redirector, _needPaintDelegate));
            }
            else if (Count > (months + 1))
            {
                // Remove excess month view instances
                for (int i = Count - 1; i > months; i--)
                {
                    this[i].Dispose();
                    this.RemoveAt(i);
                }
            }

            // Is there a change in the selection range?
            if ((_oldSelectionStart != _calendar.SelectionStart) || 
                (_oldSelectionEnd != _calendar.SelectionEnd) ||
                (_oldFocusDay != FocusDay) ||
                _firstTimeSync)
            {
                _firstTimeSync = false;
                _oldSelectionStart = _calendar.SelectionStart;
                _oldSelectionEnd = _calendar.SelectionEnd;
                _oldFocusDay = FocusDay;

                // If we have a day with the focus
                if (FocusDay != null)
                {
                    // If focus day is before the first month
                    if (FocusDay.Value < _displayMonth)
                        _displayMonth = new DateTime(FocusDay.Value.Year, FocusDay.Value.Month, 1);
                    else
                    {
                        // If focus day is after the last month
                        DateTime endDate = _displayMonth.AddMonths(months);
                        if (FocusDay.Value >= endDate)
                            _displayMonth = new DateTime(FocusDay.Value.Year, FocusDay.Value.Month, 1).AddMonths(-(months - 1));
                    }
                }
                else
                {
                    // Bring the selection into the display range
                    DateTime endMonth = _displayMonth.AddMonths(months - 1);
                    DateTime oldSelEndDate = _oldSelectionEnd.Date;
                    DateTime oldSelEndMonth = new DateTime(oldSelEndDate.Year, oldSelEndDate.Month, 1);
                    if (oldSelEndMonth >= endMonth)
                        _displayMonth = oldSelEndMonth.AddMonths(-(months - 1));

                    if (_oldSelectionStart < _displayMonth)
                        _displayMonth = new DateTime(_calendar.SelectionStart.Year, _calendar.SelectionStart.Month, 1);
                }
            }

            DateTime currentMonth = _displayMonth;

            // Inform each view which month it should be drawing
            for (int i = 1; i < Count; i++)
            {
                ViewDrawMonth viewMonth = (ViewDrawMonth)this[i];
                viewMonth.Enabled = Enabled;
                viewMonth.Month = currentMonth;
                viewMonth.FirstMonth = (i == 1);
                viewMonth.LastMonth = (i == (Count - 1));
                viewMonth.UpdateButtons((i == 1), ((i - 1) == (_calendar.CalendarDimensions.Width - 1)));

                // Move forward to next month
                currentMonth = currentMonth.AddMonths(1);
            }
        }

        private void OnShowToolTip(object sender, ToolTipEventArgs e)
        {
            if (!IsDisposed)
            {
                // Do not show tooltips when the form we are in does not have focus
                Form topForm = _calendar.CalendarControl.FindForm();
                if ((topForm != null) && !topForm.ContainsFocus)
                    return;

                // Never show tooltips are design time
                if (!_calendar.InDesignMode)
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
                            ButtonSpecToContent buttonSpecMapping = new ButtonSpecToContent(_redirector, buttonSpec);

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
                        _visualPopupToolTip = new VisualPopupToolTip(_redirector,
                                                                     sourceContent,
                                                                     _calendar.GetRenderer(),
                                                                     PaletteBackStyle.ControlToolTip,
                                                                     PaletteBorderStyle.ControlToolTip,
                                                                     CommonHelper.ContentStyleFromLabelStyle(toolTipStyle));

                        _visualPopupToolTip.Disposed += new EventHandler(OnVisualPopupToolTipDisposed);

                        // Show relative to the provided screen rectangle
                        _visualPopupToolTip.ShowCalculatingSize(_calendar.CalendarControl.RectangleToScreen(e.Target.ClientRectangle));
                    }
                }
            }
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

        private Size MaxGridCellDay(ViewLayoutContext context)
        {
            _shortText = _dayMeasure;

            // Find sizes required for the different 
            Size normalSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateNormal.Day.Content, this, VisualOrientation.Top, PaletteState.Normal, false);
            Size disabledSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateDisabled.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled, false);
            Size trackingSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateTracking.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled, false);
            Size pressedSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StatePressed.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled, false);
            Size checkedNormalSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateCheckedNormal.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled, false);
            Size checkedTrackingSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateCheckedTracking.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled, false);
            Size checkedPressedSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateCheckedPressed.Day.Content, this, VisualOrientation.Top, PaletteState.Disabled, false);

            // Find largest size required
            normalSize.Width = Math.Max(normalSize.Width, Math.Max(disabledSize.Width, Math.Max(trackingSize.Width, Math.Max(pressedSize.Width, Math.Max(checkedNormalSize.Width, Math.Max(checkedTrackingSize.Width, checkedPressedSize.Width))))));
            normalSize.Height = Math.Max(normalSize.Height, Math.Max(disabledSize.Height, Math.Max(trackingSize.Height, Math.Max(pressedSize.Height, Math.Max(checkedNormalSize.Height, Math.Max(checkedTrackingSize.Height, checkedPressedSize.Height))))));

            return normalSize;
        }

        private Size MaxGridCellDayOfWeek(ViewLayoutContext context)
        {
            _shortText = "A";

            // Find sizes required for the different 
            Size shortNormalSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateNormal.DayOfWeek.Content, this, VisualOrientation.Top, PaletteState.Normal, false);
            Size shortDisabledSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateDisabled.DayOfWeek.Content, this, VisualOrientation.Top, PaletteState.Disabled, false);

            _shortText = "A" + _dayOfWeekMeasure;

            // Find sizes required for the different 
            Size fullNormalSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateNormal.DayOfWeek.Content, this, VisualOrientation.Top,  PaletteState.Normal, false);
            Size fullDisabledSize = context.Renderer.RenderStandardContent.GetContentPreferredSize(context, _calendar.StateDisabled.DayOfWeek.Content, this, VisualOrientation.Top, PaletteState.Disabled, false);

            // Find largest size required (subtract a fudge factor of 3 pixels as Graphics.MeasureString is always too big)
            fullNormalSize.Width = Math.Max(fullNormalSize.Width - shortNormalSize.Width - 3, fullDisabledSize.Width - shortDisabledSize.Width - 3);
            fullNormalSize.Height = Math.Max(fullNormalSize.Height, fullDisabledSize.Height);

            return fullNormalSize;
        }

        private DateTime FirstDayOfMonth(DateTime dt)
        {
            dt = dt.AddDays(- (dt.Day - 1));
            return JustDay(dt);
        }

        private DateTime LastDayOfMonth(DateTime dt)
        {
            dt = dt.AddMonths(1);
            dt = dt.AddDays(-dt.Day);
            return JustDay(dt);
        }
        #endregion
    }
}
