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
	/// Allocate space for the week number corner in the month calendar.
	/// </summary>
	public class ViewLayoutWeekCorner : ViewLeaf
    {
        #region Instance Fields
        private IKryptonMonthCalendar _calendar;
        private ViewLayoutMonths _months;
        private PaletteBorder _palette;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewLayoutWeekCorner class.
		/// </summary>
        /// <param name="calendar">Reference to calendar provider.</param>
        /// <param name="months">Reference to months instance.</param>
        /// <param name="palette">Reference to border palette.</param>
        public ViewLayoutWeekCorner(IKryptonMonthCalendar calendar, 
                                    ViewLayoutMonths months,
                                    PaletteBorder palette)
        {
            _calendar = calendar;
            _months = months;
            _palette = palette;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutWeekCorner:" + Id;
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

            // Start with size needed to draw a week number
            Size retSize = new Size(_months.SizeDay.Width, _months.SizeDays.Height);

            // Add the width of the vertical border
            retSize.Width += _palette.GetBorderWidth(State);

            return retSize;
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
        }
        #endregion
    }
}
