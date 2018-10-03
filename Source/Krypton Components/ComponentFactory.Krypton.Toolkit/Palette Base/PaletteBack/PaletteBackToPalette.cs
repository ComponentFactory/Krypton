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
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Redirect all background requests directly to the palette instance.
	/// </summary>
	public class PaletteBackToPalette : IPaletteBack
    {
        #region Instance Fields
        private IPalette _palette;
        private PaletteBackStyle _style;
        #endregion

        #region Identity
        /// <summary>
		/// Initialize a new instance of the PaletteBack class.
		/// </summary>
        /// <param name="palette">Source for getting all values.</param>
        /// <param name="style">Style of values required.</param>
        public PaletteBackToPalette(IPalette palette, PaletteBackStyle style)
		{
			// Remember source palette
            _palette = palette;
            _style = style;
        }
		#endregion

        #region BackStyle
        /// <summary>
        /// Gets and sets the fixed background style.
        /// </summary>
        public PaletteBackStyle BackStyle
        {
            get { return _style; }
            set { _style = value; }
        }
        #endregion

        #region Draw
        /// <summary>
		/// Gets the actual background draw value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public InheritBool GetBackDraw(PaletteState state)
		{
            return _palette.GetBackDraw(_style, state);
		}
		#endregion

		#region GraphicsHint
		/// <summary>
		/// Gets the actual background graphics hint value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
		public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state)
		{
            return _palette.GetBackGraphicsHint(_style, state);
        }
		#endregion

		#region Color1
		/// <summary>
		/// Gets the first background color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public Color GetBackColor1(PaletteState state)
		{
            return _palette.GetBackColor1(_style, state);
        }
		#endregion

		#region Color2
		/// <summary>
		/// Gets the second back color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public Color GetBackColor2(PaletteState state)
		{
            return _palette.GetBackColor2(_style, state);
        }
		#endregion

		#region ColorStyle
		/// <summary>
		/// Gets the color drawing style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
		public PaletteColorStyle GetBackColorStyle(PaletteState state)
		{
            return _palette.GetBackColorStyle(_style, state);
        }
		#endregion

		#region ColorAlign
		/// <summary>
		/// Gets the color alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
		public PaletteRectangleAlign GetBackColorAlign(PaletteState state)
		{
            return _palette.GetBackColorAlign(_style, state);
        }
		#endregion

		#region ColorAngle
		/// <summary>
		/// Gets the color background angle.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
		public float GetBackColorAngle(PaletteState state)
		{
            return _palette.GetBackColorAngle(_style, state);
        }
		#endregion

		#region Image
		/// <summary>
		/// Gets a background image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
		public Image GetBackImage(PaletteState state)
		{
            return _palette.GetBackImage(_style, state);
        }
		#endregion

		#region ImageStyle
		/// <summary>
		/// Gets the background image style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
		public PaletteImageStyle GetBackImageStyle(PaletteState state)
		{
            return _palette.GetBackImageStyle(_style, state);
        }
		#endregion

		#region ImageAlign
		/// <summary>
		/// Gets the image alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
		public PaletteRectangleAlign GetBackImageAlign(PaletteState state)
		{
            return _palette.GetBackImageAlign(_style, state);
		}
		#endregion
    }
}
