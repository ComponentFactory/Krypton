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
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Implement redirected storage for common month calendar appearance.
	/// </summary>
    public class PaletteMonthCalendarRedirect : PaletteDoubleMetricRedirect
	{
        #region Instance Fields
        private PaletteTripleRedirect _paletteHeader;
        private PaletteTripleRedirect _paletteDayOfWeek;
        private PaletteTripleRedirect _paletteDay;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarRedirect class.
        /// </summary>
        public PaletteMonthCalendarRedirect()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarRedirect class.
		/// </summary>
        /// <param name="redirect">Inheritence redirection for bread crumb level.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteMonthCalendarRedirect(PaletteRedirect redirect,
                                            NeedPaintHandler needPaint)
            : base(redirect, PaletteBackStyle.ControlClient, 
                             PaletteBorderStyle.ControlClient)
		{
            _paletteHeader = new PaletteTripleRedirect(redirect, PaletteBackStyle.HeaderCalendar, PaletteBorderStyle.HeaderCalendar, PaletteContentStyle.HeaderCalendar, needPaint);
            _paletteDayOfWeek = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
            _paletteDay = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
        }
		#endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault
        {
            get
            {
                return (base.IsDefault && 
                        _paletteHeader.IsDefault &&
                        _paletteDayOfWeek.IsDefault &&
                        _paletteDay.IsDefault);
            }
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public override void SetRedirector(PaletteRedirect redirect)
        {
            base.SetRedirector(redirect);
            _paletteHeader.SetRedirector(redirect);
            _paletteDayOfWeek.SetRedirector(redirect);
            _paletteDay.SetRedirector(redirect);
        }
        #endregion

        #region Styles
        internal ButtonStyle DayStyle
        {
            set { _paletteDay.SetStyles(value); }
        }

        internal ButtonStyle DayOfWeekStyle
        {
            set { _paletteDayOfWeek.SetStyles(value); }
        }
        #endregion

        #region Header
        /// <summary>
        /// Gets access to the month/year header appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining month/year header appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Header
        {
            get { return _paletteHeader; }
        }

        private bool ShouldSerializeHeader()
        {
            return !_paletteHeader.IsDefault;
        }
        #endregion

        #region Day
        /// <summary>
        /// Gets access to the day appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining day appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Day
        {
            get { return _paletteDay; }
        }

        private bool ShouldSerializeDay()
        {
            return !_paletteDay.IsDefault;
        }
        #endregion

        #region DayOfWeek
        /// <summary>
        /// Gets access to the day of week appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining day of week appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect DayOfWeek
        {
            get { return _paletteDayOfWeek; }
        }

        private bool ShouldSerializeDayOfWeek()
        {
            return !_paletteDayOfWeek.IsDefault;
        }
        #endregion
    }
}
