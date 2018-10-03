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
    /// Draw element for a context menu month calendar.
    /// </summary>
    public class ViewDrawMenuMonthCalendar : ViewComposite,
                                             IKryptonMonthCalendar
    {
        #region Instance Fields
        private KryptonContextMenuMonthCalendar _monthCalendar;
        private IContextMenuProvider _provider;
        private ViewLayoutMonths _layoutMonths;
        private DateTime _minDate;
        private DateTime _maxDate;
        private DateTime _todayDate;
        private string _todayFormat;
        private Day _firstDayOfWeek;
        private Size _dimensions;
        private bool _itemEnabled;
        private int _maxSelectionCount;
        private int _scrollChange;
        private string _todayText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuMonthCalendar class.
		/// </summary>
        /// <param name="provider">Reference to provider.</param>
        /// <param name="monthCalendar">Reference to owning month calendar entry.</param>
        public ViewDrawMenuMonthCalendar(IContextMenuProvider provider,
                                         KryptonContextMenuMonthCalendar monthCalendar)
		{
            _provider = provider;
            _monthCalendar = monthCalendar;
            _firstDayOfWeek = _monthCalendar.FirstDayOfWeek;
            _minDate = _monthCalendar.MinDate;
            _maxDate = _monthCalendar.MaxDate;
            _todayDate = _monthCalendar.TodayDate;
            _maxSelectionCount = _monthCalendar.MaxSelectionCount;
            _scrollChange = _monthCalendar.ScrollChange;
            _todayText = _monthCalendar.TodayText;
            _todayFormat = _monthCalendar.TodayFormat;
            _dimensions = _monthCalendar.CalendarDimensions;

            // Decide on the enabled state of the display
            _itemEnabled = provider.ProviderEnabled && _monthCalendar.Enabled;

            // Give the item object the redirector to use when inheriting values
            _monthCalendar.SetPaletteRedirect(provider.ProviderRedirector);

            // Create view that is used by standalone control as well as this context menu element
            _layoutMonths = new ViewLayoutMonths(provider, monthCalendar, provider.ProviderViewManager, this, provider.ProviderRedirector, provider.ProviderNeedPaintDelegate);
            _layoutMonths.CloseOnTodayClick = _monthCalendar.CloseOnTodayClick;
            _layoutMonths.ShowWeekNumbers = _monthCalendar.ShowWeekNumbers;
            _layoutMonths.ShowTodayCircle = _monthCalendar.ShowTodayCircle;
            _layoutMonths.ShowToday = _monthCalendar.ShowToday;
            _layoutMonths.Enabled = _itemEnabled;

            Add(_layoutMonths);
        }

		/// <summary>
		/// Release unmanaged and optionally managed resources.
		/// </summary>
		/// <param name="disposing">Called from Dispose method.</param>
        protected override void Dispose(bool disposing)
        {
            // Prevent memory leak
            base.Dispose(disposing);
        }

        /// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMenuMonthCalendar:" + Id;
		}
		#endregion

        #region Layout
        /// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            base.Layout(context);
        }
        #endregion

        #region IKryptonMonthCalendar
        /// <summary>
        /// Gets access to the owning control
        /// </summary>
        public Control CalendarControl 
        {
            get { return _provider.ProviderViewManager.Control; }
        }

        /// <summary>
        /// Gets if the control is in design mode.
        /// </summary>
        public bool InDesignMode 
        {
            get { return false; }
        }

        /// <summary>
        /// Get the renderer.
        /// </summary>
        /// <returns>Render instance.</returns>
        public IRenderer GetRenderer()
        {
            VisualContextMenu contextMenu = (VisualContextMenu)_provider.ProviderViewManager.Control;
            return contextMenu.Renderer;
        }

        /// <summary>
        /// Gets a delegate for creating tool strip renderers.
        /// </summary>
        public GetToolStripRenderer GetToolStripDelegate
        {
            get 
            {
                VisualContextMenu contextMenu = (VisualContextMenu)_provider.ProviderViewManager.Control;
                return new GetToolStripRenderer(contextMenu.CreateToolStripRenderer);
            }
        }

        /// <summary>
        /// Gets the number of columns and rows of months displayed.
        /// </summary>
        public Size CalendarDimensions 
        {
            get { return _dimensions; }
        }

        /// <summary>
        /// First day of the week.
        /// </summary>
        public Day FirstDayOfWeek 
        {
            get { return _firstDayOfWeek; }
        }

        /// <summary>
        /// First date allowed to be drawn/selected.
        /// </summary>
        public DateTime MinDate 
        {
            get { return _minDate; }
        }

        /// <summary>
        /// Last date allowed to be drawn/selected.
        /// </summary>
        public DateTime MaxDate
        {
            get { return _maxDate; }
        }

        /// <summary>
        /// Today's date.
        /// </summary>
        public DateTime TodayDate
        {
            get { return _todayDate; }
        }

        /// <summary>
        /// Today's date format.
        /// </summary>
        public string TodayFormat
        {
            get { return _todayFormat; } 
        }

        /// <summary>
        /// Gets the focus day.
        /// </summary>
        public DateTime? FocusDay
        {
            get { return _monthCalendar.FocusDay; }
            set { _monthCalendar.FocusDay = value; }
        }

        /// <summary>
        /// Number of days allowed to be selected at a time.
        /// </summary>
        public int MaxSelectionCount 
        {
            get { return _maxSelectionCount; } 
        }

        /// <summary>
        /// Gets the text used as a today label.
        /// </summary>
        public string TodayText
        {
            get { return _todayText; }
        }

        /// <summary>
        /// Gets the number of months to move for next/prev buttons.
        /// </summary>
        public int ScrollChange 
        {
            get { return _scrollChange; }
        }

        /// <summary>
        /// Start of selected range.
        /// </summary>
        public DateTime SelectionStart 
        {
            get { return _monthCalendar.SelectionStart; }
        }

        /// <summary>
        /// End of selected range.
        /// </summary>
        public DateTime SelectionEnd 
        {
            get { return _monthCalendar.SelectionEnd; }
        }

        /// <summary>
        /// Gets access to the month calendar common appearance entries.
        /// </summary>
        public PaletteMonthCalendarRedirect StateCommon
        {
            get { return _monthCalendar.StateCommon; }
        }

        /// <summary>
        /// Gets access to the month calendar normal appearance entries.
        /// </summary>
        public PaletteMonthCalendarDoubleState StateNormal 
        {
            get { return _monthCalendar.StateNormal; }
        }

        /// <summary>
        /// Gets access to the month calendar disabled appearance entries.
        /// </summary>
        public PaletteMonthCalendarDoubleState StateDisabled
        {
            get { return _monthCalendar.StateDisabled; }
        }

        /// <summary>
        /// Gets access to the month calendar tracking appearance entries.
        /// </summary>
        public PaletteMonthCalendarState StateTracking
        {
            get { return _monthCalendar.StateTracking; }
        }

        /// <summary>
        /// Gets access to the month calendar pressed appearance entries.
        /// </summary>
        public PaletteMonthCalendarState StatePressed
        {
            get { return _monthCalendar.StatePressed; }
        }

        /// <summary>
        /// Gets access to the month calendar checked normal appearance entries.
        /// </summary>
        public PaletteMonthCalendarState StateCheckedNormal
        {
            get { return _monthCalendar.StateCheckedNormal; }
        }

        /// <summary>
        /// Gets access to the month calendar checked tracking appearance entries.
        /// </summary>
        public PaletteMonthCalendarState StateCheckedTracking
        {
            get { return _monthCalendar.StateCheckedTracking; }
        }

        /// <summary>
        /// Gets access to the month calendar checked pressed appearance entries.
        /// </summary>
        public PaletteMonthCalendarState StateCheckedPressed
        {
            get { return _monthCalendar.StateCheckedPressed; }
        }

        /// <summary>
        /// Gets access to the override for disabled day.
        /// </summary>
        public PaletteTripleOverride OverrideDisabled
        {
            get { return _monthCalendar.OverrideDisabled; }
        }

        /// <summary>
        /// Gets access to the override for normal day.
        /// </summary>
        public PaletteTripleOverride OverrideNormal
        {
            get { return _monthCalendar.OverrideNormal; }
        }

        /// <summary>
        /// Gets access to the override for tracking day.
        /// </summary>
        public PaletteTripleOverride OverrideTracking
        {
            get { return _monthCalendar.OverrideTracking; }
        }

        /// <summary>
        /// Gets access to the override for pressed day.
        /// </summary>
        public PaletteTripleOverride OverridePressed
        {
            get { return _monthCalendar.OverridePressed; }
        }

        /// <summary>
        /// Gets access to the override for checked normal day.
        /// </summary>
        public PaletteTripleOverride OverrideCheckedNormal
        {
            get { return _monthCalendar.OverrideCheckedNormal; }
        }

        /// <summary>
        /// Gets access to the override for checked tracking day.
        /// </summary>
        public PaletteTripleOverride OverrideCheckedTracking
        {
            get { return _monthCalendar.OverrideCheckedTracking; }
        }

        /// <summary>
        /// Gets access to the override for checked pressed day.
        /// </summary>
        public PaletteTripleOverride OverrideCheckedPressed
        {
            get { return _monthCalendar.OverrideCheckedPressed; }
        }

        /// <summary>
        /// Dates to be bolded.
        /// </summary>
        public DateTimeList BoldedDatesList
        {
            get { return _monthCalendar.BoldedDatesList; }
        }

        /// <summary>
        /// Monthly days to be bolded.
        /// </summary>
        public int MonthlyBoldedDatesMask
        {
            get { return _monthCalendar.MonthlyBoldedDatesMask; }
        }

        /// <summary>
        /// Array of annual days per month to be bolded.
        /// </summary>
        public int[] AnnuallyBoldedDatesMask
        {
            get { return _monthCalendar.AnnuallyBoldedDatesMask; }
        }

        /// <summary>
        /// Set the selection range.
        /// </summary>
        /// <param name="start">New starting date.</param>
        /// <param name="end">New ending date.</param>
        public void SetSelectionRange(DateTime start, DateTime end)
        {
            _monthCalendar.SetSelectionRange(start, end);
        }

        /// <summary>
        /// Update usage of bolded overrides.
        /// </summary>
        /// <param name="bolded">New bolded state.</param>
        public void SetBoldedOverride(bool bolded)
        {
            _monthCalendar.SetBoldedOverride(bolded);
        }

        /// <summary>
        /// Update usage of today overrides.
        /// </summary>
        /// <param name="today">New today state.</param>
        public void SetTodayOverride(bool today)
        {
            _monthCalendar.SetTodayOverride(today);
        }

        /// <summary>
        /// Update usage of focus overrides.
        /// </summary>
        /// <param name="focus">Should show focus.</param>
        public void SetFocusOverride(bool focus)
        {
            _monthCalendar.SetFocusOverride(focus);
        }
        #endregion
    }
}
