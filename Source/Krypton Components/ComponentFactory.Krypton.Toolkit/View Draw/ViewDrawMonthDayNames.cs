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
	/// Draws the month day names.
	/// </summary>
	public class ViewDrawMonthDayNames : ViewLeaf,
                                         IContentValues
	{
		#region Instance Fields
        private IKryptonMonthCalendar _calendar;
        private ViewLayoutMonths _months;
        private IDisposable[] _dayMementos;
        private string _drawText;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewDrawMonthDayNames class.
		/// </summary>
        /// <param name="calendar">Reference to calendar provider.</param>
        /// <param name="months">Reference to months instance.</param>
        public ViewDrawMonthDayNames(IKryptonMonthCalendar calendar, ViewLayoutMonths months)
        {
            _calendar = calendar;
            _months = months;

            // 7 day mementos
            _dayMementos = new IDisposable[7];
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMonthDayNames:" + Id;
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

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            Size preferredSize = Size.Empty;

            // Width is 7 days times the width of a day name
            preferredSize.Width = _months.SizeDays.Width * 7;

            // Height is the height of the day name
            preferredSize.Height = _months.SizeDays.Height;

            return preferredSize;
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

            // Content palette depends on enabled state of the control
            PaletteState state = (Enabled ? PaletteState.Normal : PaletteState.Disabled);

            // Layout the 7 day names
            Rectangle layoutRect = new Rectangle(ClientLocation, _months.SizeDays);
            for (int i = 0, day=(int)_months.DisplayDayOfWeek; i < 7; i++, day++)
            {
                // Define text to be drawn
                _drawText = _months.DayNames[day % 7];

                if (_dayMementos[i] != null)
                    _dayMementos[i].Dispose();

                _dayMementos[i] = context.Renderer.RenderStandardContent.LayoutContent(context, layoutRect, _calendar.StateNormal.DayOfWeek.Content, this, 
                                                                                       VisualOrientation.Top, state, false);

                // Move across to next day
                layoutRect.X += _months.SizeDays.Width;
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

            // Content palette depends on enabled state of the control
            PaletteState state = (Enabled ? PaletteState.Normal : PaletteState.Disabled);

            // Draw the 7 day names
            Rectangle drawRect = new Rectangle(ClientLocation, _months.SizeDays);
            for(int i=0, day=(int)_months.DisplayDayOfWeek; i<7; i++, day++)
            {
                // Draw using memento cached from the layout call
                if (_dayMementos[day % 7] != null)
                    context.Renderer.RenderStandardContent.DrawContent(context, drawRect, _calendar.StateNormal.DayOfWeek.Content, _dayMementos[day % 7], 
                                                                       VisualOrientation.Top, state, false, true);
                
                // Move across to next day
                drawRect.X += _months.SizeDays.Width;
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
    }
}
