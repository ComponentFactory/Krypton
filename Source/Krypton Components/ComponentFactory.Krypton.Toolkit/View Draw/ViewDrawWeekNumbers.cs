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
using System.Globalization;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Draws all the month grid entries including the column names and day values
	/// </summary>
	public class ViewDrawWeekNumbers : ViewLeaf,
                                       IContentValues
    {
        #region Static Fields
        private static readonly int WEEKS = 6;
        private static readonly TimeSpan TIMESPAN_1DAY = new TimeSpan(1, 0, 0, 0);
        private static readonly TimeSpan TIMESPAN_6DAYS = new TimeSpan(6, 0, 0, 0);
        private static readonly TimeSpan TIMESPAN_1WEEK = new TimeSpan(7, 0, 0, 0);
        #endregion

        #region Instance Fields
        private IKryptonMonthCalendar _calendar;
        private ViewLayoutMonths _months;
        private IDisposable[] _dayMementos;
        private string _drawText;
        private DateTime _firstDay;
        private DateTime _weekDay;
        private DateTime _month;
        private bool _firstMonth;
        private bool _lastMonth;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewDrawWeekNumbers class.
		/// </summary>
        /// <param name="calendar">Reference to calendar provider.</param>
        /// <param name="months">Reference to months instance.</param>
        public ViewDrawWeekNumbers(IKryptonMonthCalendar calendar, ViewLayoutMonths months)
        {
            _drawText = string.Empty;
            _calendar = calendar;
            _months = months;
            _dayMementos = new IDisposable[WEEKS];
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawWeekNumbers:" + Id;
		}

        /// <summary>
        /// Release unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing">Called from Dispose method.</param>
        protected override void Dispose(bool disposing)
        {
            // Dispose of the mementos to prevent memory leak
            for(int i=0; i<_dayMementos.Length; i++)
                if (_dayMementos[i] != null)
                {
                    _dayMementos[i].Dispose();
                    _dayMementos[i] = null;
                }

            base.Dispose(disposing);
        }
		#endregion

        #region Public
        /// <summary>
        /// Is this the first month in the group.
        /// </summary>
        public bool FirstMonth
        {
            set { _firstMonth = value; }
        }

        /// <summary>
        /// Is this the last month in the group.
        /// </summary>
        public bool LastMonth
        {
            set { _lastMonth = value; }
        }

        /// <summary>
        /// Sets the date this month is used to represent.
        /// </summary>
        public DateTime Month
        {
            set
            {
                _month = value;

                // Begin with the day before the required month
                _firstDay = new DateTime(value.Year, value.Month, 1);
                _firstDay -= TIMESPAN_1DAY;

                // Move backwards until we hit the starting day of the week
                while (_firstDay.DayOfWeek != _months.DisplayDayOfWeek)
                    _firstDay -= TIMESPAN_1DAY;

                // Find the first day of the year
                DateTime yearDay = new DateTime(value.Year, 1, 1);
                _weekDay = _firstDay;

                // Move forewards until we hit the starting day of the year
                while (_weekDay.DayOfWeek != yearDay.DayOfWeek)
                    _weekDay += TIMESPAN_1DAY;
            }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);
            return new Size(_months.SizeDay.Width, _months.SizeDays.Height * WEEKS);
        }
        
        /// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;
    
            // Do not draw week numbers in bold or focused
            _calendar.SetFocusOverride(false);
            _calendar.SetBoldedOverride(false);

            // Layout each week number
            Rectangle layoutRectWeek = new Rectangle(ClientLocation.X, ClientLocation.Y, _months.SizeDay.Width, _months.SizeDays.Height);
            DateTime weekDate = _weekDay;
            DateTime displayDate = _firstDay;
            for (int j = 0; j < WEEKS; j++)
            {
                // Should we draw a week number for a week starting on this date
                DateTime weekNumberDate = weekDate;
                if (DisplayWeekNumber(displayDate, ref weekNumberDate))
                {
                    // Define text to be drawn
                    _drawText = GetWeekNumber(weekNumberDate).ToString();

                    if (_dayMementos[j] != null)
                    {
                        _dayMementos[j].Dispose();
                        _dayMementos[j] = null;
                    }

                    PaletteState paletteState = (Enabled ? PaletteState.Normal : PaletteState.Disabled);
                    IPaletteTriple paletteTriple = (Enabled ? _calendar.StateNormal.Day : _calendar.StateDisabled.Day);

                    _dayMementos[j] = context.Renderer.RenderStandardContent.LayoutContent(context, layoutRectWeek, paletteTriple.PaletteContent,
                                                                                           this, VisualOrientation.Top, paletteState, false);
                }

                // Move to next week
                weekDate += TIMESPAN_1WEEK;
                displayDate += TIMESPAN_1WEEK;
                layoutRectWeek.Y += _months.SizeDays.Height;
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            Debug.Assert(context != null);

            // Do not draw week numbers in bold or focused
            _calendar.SetFocusOverride(false);
            _calendar.SetBoldedOverride(false);

            // Layout each week number
            Rectangle drawRectWeek = new Rectangle(ClientLocation.X, ClientLocation.Y, _months.SizeDay.Width, _months.SizeDays.Height);
            DateTime weekDate = _weekDay;
            DateTime displayDate = _firstDay;
            for (int j = 0; j < WEEKS; j++)
            {
                // Should we draw a week number for a week starting on this date
                DateTime weekNumberDate = weekDate;
                if (DisplayWeekNumber(displayDate, ref weekNumberDate))
                {
                    // Define text to be drawn
                    _drawText = GetWeekNumber(weekNumberDate).ToString();

                    PaletteState paletteState = (Enabled ? PaletteState.Normal : PaletteState.Disabled);
                    IPaletteTriple paletteTriple = (Enabled ? _calendar.StateNormal.Day : _calendar.StateDisabled.Day);

                    // Do we need to draw the background?
                    if (paletteTriple.PaletteBack.GetBackDraw(paletteState) == InheritBool.True)
                    {
                        using (GraphicsPath path = context.Renderer.RenderStandardBorder.GetBackPath(context, drawRectWeek, paletteTriple.PaletteBorder,
                                                                                                     VisualOrientation.Top, paletteState))
                        {
                            context.Renderer.RenderStandardBack.DrawBack(context, drawRectWeek, path, paletteTriple.PaletteBack, VisualOrientation.Top, paletteState, null);
                        }
                    }

                    // Do we need to draw the border?
                    if (paletteTriple.PaletteBorder.GetBorderDraw(paletteState) == InheritBool.True)
                    {
                        context.Renderer.RenderStandardBorder.DrawBorder(context, drawRectWeek, paletteTriple.PaletteBorder, VisualOrientation.Top, paletteState);
                    }

                    // Do we need to draw the content?
                    if (paletteTriple.PaletteContent.GetContentDraw(paletteState) == InheritBool.True)
                    {
                        context.Renderer.RenderStandardContent.DrawContent(context, drawRectWeek, paletteTriple.PaletteContent, _dayMementos[j],
                                                                           VisualOrientation.Top, paletteState, false, true);
                    }
                }

                // Move to next week
                weekDate += TIMESPAN_1WEEK;
                displayDate += TIMESPAN_1WEEK;
                drawRectWeek.Y += _months.SizeDays.Height;
            }
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
            return _drawText;
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

        #region Implementation
        private int GetWeekNumber(DateTime dt)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dt, 
                                                                     CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, 
                                                                     _months.DisplayDayOfWeek);
        }

        private bool DisplayWeekNumber(DateTime displayDate, ref DateTime weekDate)
        {
            if (displayDate < _month)
            {
                displayDate += TIMESPAN_6DAYS;
                if (displayDate < _month)
                    return _firstMonth;
                else
                    weekDate = _month;
            }
            else
            {
                DateTime nextMonth = _month.AddMonths(1);
                DateTime displayDate2 = displayDate + TIMESPAN_6DAYS;
                if (displayDate2 >= nextMonth)
                {
                    if (displayDate >= nextMonth)
                        return _lastMonth;
                    else
                        weekDate = nextMonth.AddDays(-1);
                }
            }

            return true;
        }
        #endregion
    }
}
