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
using System.Globalization;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Draw todays date as a button.
    /// </summary>
    public class ViewDrawToday : ViewDrawButton,
                                 IContentValues
    {
        #region Events
        /// <summary>
        /// Occurs when the today button is clicked.
        /// </summary>
        public event EventHandler Click;
        #endregion

        #region Instance Fields
        private IKryptonMonthCalendar _calendar;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawToday class.
		/// </summary>
        /// <param name="calendar">Provider of month calendar values.</param>
        /// <param name="paletteDisabled">Palette source for the disabled state.</param>
        /// <param name="paletteNormal">Palette source for the normal state.</param>
		/// <param name="paletteTracking">Palette source for the tracking state.</param>
		/// <param name="palettePressed">Palette source for the pressed state.</param>
        /// <param name="needPaintHandler">Delegate for requested repainting.</param>
        public ViewDrawToday(IKryptonMonthCalendar calendar,
                             IPaletteTriple paletteDisabled,
                             IPaletteTriple paletteNormal,
                             IPaletteTriple paletteTracking,
                             IPaletteTriple palettePressed,
                             NeedPaintHandler needPaintHandler)
            : base(paletteDisabled, paletteNormal, paletteTracking, palettePressed,
                   paletteNormal, paletteTracking, palettePressed, null,
                   null, VisualOrientation.Top, false)
        {
            _calendar = calendar;

            // We provide the content values for display
            base.ButtonValues = this;

            // Define a controller so the button can be clicked
            ButtonController controller = new ButtonController(this, needPaintHandler);
            controller.Click += new MouseEventHandler(OnClick);
            MouseController = controller;
            SourceController = controller;
            KeyController = controller;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawToday:" + Id;
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
            return KryptonManager.Strings.Today + " " + _calendar.TodayDate.ToString(_calendar.TodayFormat);
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
        private void OnClick(object sender, MouseEventArgs e)
        {
            if (Click != null)
                Click(this, EventArgs.Empty);
        }
        #endregion

		/// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);
            base.Layout(context);
        }
    }
}
