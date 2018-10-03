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
	/// Extends the ViewComposite by organising and drawing an individual month.
	/// </summary>
	public class ViewDrawMonth : ViewLayoutStack,
                                 IContentValues
	{
        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecCalendar instances.
        /// </summary>
        public class CalendarButtonSpecCollection : ButtonSpecCollection<ButtonSpecCalendar>
        {
            #region Identity
            /// <summary>
            /// Initialize a new instance of the CalendarButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public CalendarButtonSpecCollection(ViewDrawMonth owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

		#region Instance Fields
        private IKryptonMonthCalendar _calendar;
        private ViewLayoutMonths _months;
        private ViewDrawDocker _drawHeader;
        private PaletteBorderInheritForced _borderForced;
        private ViewDrawContent _drawContent;
        private ViewDrawMonthDayNames _drawMonthDayNames;
        private ViewDrawBorderEdge _drawBorderEdge;
        private ViewDrawMonthDays _drawMonthDays;
        private ViewLayoutWeekCorner _drawWeekCorner;
        private ViewDrawWeekNumbers _drawWeekNumbers;
        private ViewLayoutStack _numberStack;
        private PaletteBorderEdgeRedirect _borderEdgeRedirect;
        private PaletteBorderEdge _borderEdge;
        private ButtonSpecManagerDraw _buttonManager;
        private CalendarButtonSpecCollection _buttonSpecs;
        private ButtonSpecCalendar _arrowPrev;
        private ButtonSpecCalendar _arrowNext;
        private string _header;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewDrawMonth class.
		/// </summary>
        /// <param name="calendar">Reference to calendar provider.</param>
        /// <param name="months">Reference to months instance.</param>
        /// <param name="redirector">Redirector for getting values.</param>
        /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
        public ViewDrawMonth(IKryptonMonthCalendar calendar, 
                             ViewLayoutMonths months,
                             PaletteRedirect redirector,
                             NeedPaintHandler needPaintDelegate)
            : base(false)
        {
            _calendar = calendar;
            _months = months;

            // Add a header for showing the month/year value
            _drawContent = new ViewDrawContent(_calendar.StateNormal.Header.Content, this, VisualOrientation.Top);
            _borderForced = new PaletteBorderInheritForced(_calendar.StateNormal.Header.Border);
            _borderForced.ForceBorderEdges(PaletteDrawBorders.None);
            _drawHeader = new ViewDrawDocker(_calendar.StateNormal.Header.Back, _borderForced, null);
            _drawHeader.Add(_drawContent, ViewDockStyle.Fill);
            Add(_drawHeader);

            // Create the left/right arrows for moving the months
            _arrowPrev = new ButtonSpecCalendar(this, PaletteButtonSpecStyle.Previous, RelativeEdgeAlign.Near);
            _arrowNext = new ButtonSpecCalendar(this, PaletteButtonSpecStyle.Next, RelativeEdgeAlign.Far);
            _arrowPrev.Click += new EventHandler(OnPrevMonth);
            _arrowNext.Click += new EventHandler(OnNextMonth);
            _buttonSpecs = new CalendarButtonSpecCollection(this);
            _buttonSpecs.Add(_arrowPrev);
            _buttonSpecs.Add(_arrowNext);

            // Using a button spec manager to add the buttons to the header
            _buttonManager = new ButtonSpecManagerDraw(_calendar.CalendarControl, redirector, null, _buttonSpecs,
                                                       new ViewDrawDocker[] { _drawHeader },
                                                       new IPaletteMetric[] { _calendar.StateCommon },
                                                       new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetCalendar },
                                                       new PaletteMetricPadding[] { PaletteMetricPadding.None },
                                                       _calendar.GetToolStripDelegate, needPaintDelegate);

            // Create stacks for holding display items
            ViewLayoutStack namesStack = new ViewLayoutStack(true);
            ViewLayoutStack weeksStack = new ViewLayoutStack(true);
            ViewLayoutStack daysStack = new ViewLayoutStack(false);
            _numberStack = new ViewLayoutStack(false);
            weeksStack.Add(_numberStack);
            weeksStack.Add(daysStack);

            // Add day names
            _drawMonthDayNames = new ViewDrawMonthDayNames(_calendar, _months);
            _drawWeekCorner = new ViewLayoutWeekCorner(_calendar, _months, _calendar.StateNormal.Header.Border);
            namesStack.Add(_drawWeekCorner);
            namesStack.Add(_drawMonthDayNames);
            Add(namesStack);
            Add(weeksStack);

            // Add border between week numbers and days area
            _borderEdgeRedirect = new PaletteBorderEdgeRedirect(_calendar.StateNormal.Header.Border, null);
            _borderEdge = new PaletteBorderEdge(_borderEdgeRedirect, null);
            _drawBorderEdge = new ViewDrawBorderEdge(_borderEdge, Orientation.Vertical);
            _drawWeekNumbers = new ViewDrawWeekNumbers(_calendar, _months);
            ViewLayoutDocker borderLeftDock = new ViewLayoutDocker();
            borderLeftDock.Add(_drawWeekNumbers, ViewDockStyle.Left);
            borderLeftDock.Add(new ViewLayoutSeparator(0, 4), ViewDockStyle.Top);
            borderLeftDock.Add(_drawBorderEdge, ViewDockStyle.Fill);
            borderLeftDock.Add(new ViewLayoutSeparator(0, 4), ViewDockStyle.Bottom);
            _numberStack.Add(borderLeftDock);

            // Add border between day names and individual days
            PaletteBorderEdgeRedirect borderEdgeRedirect = new PaletteBorderEdgeRedirect(_calendar.StateNormal.Header.Border, null);
            PaletteBorderEdge borderEdge = new PaletteBorderEdge(borderEdgeRedirect, null);
            ViewDrawBorderEdge drawBorderEdge = new ViewDrawBorderEdge(borderEdge, Orientation.Horizontal);
            ViewLayoutDocker borderTopDock = new ViewLayoutDocker();
            borderTopDock.Add(new ViewLayoutSeparator(4, 1), ViewDockStyle.Left);
            borderTopDock.Add(drawBorderEdge, ViewDockStyle.Fill);
            borderTopDock.Add(new ViewLayoutSeparator(4, 1), ViewDockStyle.Right);
            borderTopDock.Add(new ViewLayoutSeparator(1, 3), ViewDockStyle.Bottom);
            daysStack.Add(borderTopDock);

            // Add the actual individual days
            _drawMonthDays = new ViewDrawMonthDays(_calendar, _months);
            daysStack.Add(_drawMonthDays);

            // Adding buttons manually means we have to ask for buttons to be created
            _buttonManager.RecreateButtons();
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMonth:" + Id;
		}
		#endregion

        #region Public
        /// <summary>
        /// Gets access to the days draw element.
        /// </summary>
        public ViewDrawMonthDays ViewDrawMonthDays
        {
            get { return _drawMonthDays; }
        }

        /// <summary>
        /// Gets and sets the enabled state of the view.
        /// </summary>
        public override bool Enabled
        {
            get { return base.Enabled; }
            
            set
            {
                _drawContent.Enabled = value;
                _drawHeader.Enabled = value;
                _drawMonthDayNames.Enabled = value;
                _drawBorderEdge.Enabled = value;
                _drawMonthDays.Enabled = value;
                base.Enabled = value;
            }
        }

        /// <summary>
        /// Is this the first month in the group.
        /// </summary>
        public bool FirstMonth
        {
            set 
            { 
                _drawMonthDays.FirstMonth = value;
                _drawWeekNumbers.FirstMonth = value;
            }
        }

        /// <summary>
        /// Is this the last month in the group.
        /// </summary>
        public bool LastMonth
        {
            set 
            {
                _drawMonthDays.LastMonth = value;
                _drawWeekNumbers.LastMonth = value;
            }
        }

        /// <summary>
        /// Gets and sets the month that this view element is used to draw.
        /// </summary>
        public DateTime Month
        {
            set
            {
                _header = value.ToString(CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern);
                _drawMonthDays.Month = value;
                _drawWeekNumbers.Month = value;
            }
        }

        /// <summary>
        /// Update the visible state of the navigation buttons.
        /// </summary>
        /// <param name="prev">Show the previous button.</param>
        /// <param name="next">Show the next button.</param>
        public void UpdateButtons(bool prev, bool next)
        {
            _arrowPrev.Visible = prev;
            _arrowNext.Visible = next;
            _buttonManager.RefreshButtons();
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            UpdateWeekNumberViews();
            return base.GetPreferredSize(context);
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            UpdateWeekNumberViews();
            base.Layout(context);
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
            return _header;
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
        private void UpdateWeekNumberViews()
        {
            // Update display of week numbers views
            bool showWeekNumbers = _months.ShowWeekNumbers;
            _drawWeekCorner.Visible = showWeekNumbers;
            _numberStack.Visible = showWeekNumbers;
        }

        private void OnNextMonth(object sender, EventArgs e)
        {
            _months.NextMonth();
        }

        private void OnPrevMonth(object sender, EventArgs e)
        {
            _months.PrevMonth();
        }
        #endregion
    }
}
