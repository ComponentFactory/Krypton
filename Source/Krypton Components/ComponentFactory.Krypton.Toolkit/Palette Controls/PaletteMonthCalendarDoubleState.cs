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
	/// Implement storage for month calendar appearance states.
	/// </summary>
    public class PaletteMonthCalendarDoubleState : PaletteDouble
    {
        #region Instance Fields
        private PaletteTriple _paletteHeader;
        private PaletteTriple _paletteDay;
        private PaletteTriple _paletteDayOfWeek;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarDoubleState class.
		/// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        public PaletteMonthCalendarDoubleState(PaletteMonthCalendarRedirect redirect)
            : this(redirect, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteMonthCalendarDoubleState class.
		/// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteMonthCalendarDoubleState(PaletteMonthCalendarRedirect redirect,
                                               NeedPaintHandler needPaint) 
            : base(redirect, needPaint)
		{
            _paletteHeader = new PaletteTriple(redirect.Header, needPaint);
            _paletteDay = new PaletteTriple(redirect.Day, needPaint);
            _paletteDayOfWeek = new PaletteTriple(redirect.DayOfWeek, needPaint);
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
                        _paletteDay.IsDefault &&
                        _paletteDayOfWeek.IsDefault);
            }
		}
		#endregion

        #region Header
        /// <summary>
        /// Gets access to the month/year header appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining month/year header appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple Header
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
        public PaletteTriple Day
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
        public PaletteTriple DayOfWeek
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
