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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws a separator between ribbon tabs.
	/// </summary>
    internal class ViewDrawRibbonTabSep : ViewLayoutRibbonSeparator
    {
        #region Static Fields
        private static readonly int SEP_WIDTH = 4;
        private static readonly Color _lighten1 = Color.FromArgb(128, Color.White);
        private static readonly Blend _fadeBlend;
        #endregion

        #region Instance Fields
        private IPaletteRibbonGeneral _palette;
        private bool _draw;
        #endregion

        #region Identity
        static ViewDrawRibbonTabSep()
        {
            _fadeBlend = new Blend();
            _fadeBlend.Factors = new float[] { 0.0f, 1.0f, 1.0f };
            _fadeBlend.Positions = new float[] { 0.0f, 0.33f, 1.0f };
        }

        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonTabSep class.
		/// </summary>
        /// <param name="palette">Source for palette values.</param>
        public ViewDrawRibbonTabSep(IPaletteRibbonGeneral palette)
            : base(SEP_WIDTH, true)
        {
            Debug.Assert(palette != null);
            _palette = palette;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonTabSep:" + Id;
		}
		#endregion

        #region Draw
        /// <summary>
        /// Gets and sets a value indicating if the tab separator should draw.
        /// </summary>
        public bool Draw
        {
            get { return _draw; }
            set { _draw = value; }
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            if (Draw)
            {
                RectangleF rectF = new RectangleF(ClientLocation.X, ClientLocation.Y - 0.5f, ClientWidth, ClientHeight + 1);
                using (LinearGradientBrush sepBrush = new LinearGradientBrush(rectF, Color.Transparent, _palette.GetRibbonTabSeparatorColor(PaletteState.Normal), 90f))
                {
                    sepBrush.Blend = _fadeBlend;

                    switch (_palette.GetRibbonShape())
                    {
                        default:
                        case PaletteRibbonShape.Office2007:
                            context.Graphics.FillRectangle(sepBrush, ClientLocation.X + 2, ClientLocation.Y, 1, ClientHeight - 1);
                            break;
                        case PaletteRibbonShape.Office2010:
                            context.Graphics.FillRectangle(sepBrush, ClientLocation.X + 1, ClientLocation.Y, 1, ClientHeight - 1);

                            using (LinearGradientBrush sepLightBrush = new LinearGradientBrush(rectF, Color.Transparent, _lighten1, 90f))
                                context.Graphics.FillRectangle(sepLightBrush, ClientLocation.X + 2, ClientLocation.Y, 1, ClientHeight - 1);
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
