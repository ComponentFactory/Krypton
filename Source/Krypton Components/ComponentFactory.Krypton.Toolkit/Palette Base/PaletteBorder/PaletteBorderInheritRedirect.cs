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
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Provide inheritance of palette border properties from source redirector.
	/// </summary>
	public class PaletteBorderInheritRedirect : PaletteBorderInherit
	{
		#region Instance Fields
		private PaletteRedirect _redirect;
		private PaletteBorderStyle _style;
        private bool _overrideBorderToFalse;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the PaletteBorderInheritRedirect class.
		/// </summary>
		/// <param name="redirect">Source for inherit requests.</param>
        public PaletteBorderInheritRedirect(PaletteRedirect redirect)
            : this(redirect, PaletteBorderStyle.ButtonStandalone)
        {
        }

        /// <summary>
		/// Initialize a new instance of the PaletteBorderInheritRedirect class.
		/// </summary>
		/// <param name="redirect">Source for inherit requests.</param>
		/// <param name="style">Style used in requests.</param>
		public PaletteBorderInheritRedirect(PaletteRedirect redirect,
											PaletteBorderStyle style)
		{
			_redirect = redirect;
			_style = style;
		}
		#endregion

        #region OverrideBorderToFalse
        /// <summary>
        /// Gets and sets the overriding of the border draw to always be false.
        /// </summary>
        public bool OverrideBorderToFalse
        {
            get { return _overrideBorderToFalse; }
            set { _overrideBorderToFalse = value; }
        }
        #endregion

        #region GetRedirector
        /// <summary>
        /// Gets the redirector instance.
        /// </summary>
        /// <returns>Return the currently used redirector.</returns>
        public PaletteRedirect GetRedirector()
        {
            return _redirect;
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _redirect = redirect;
        }
        #endregion

		#region Style
		/// <summary>
		/// Gets and sets the style to use when inheriting.
		/// </summary>
		public PaletteBorderStyle Style
		{
			get { return _style; }
			set { _style = value; }
		}
		#endregion

		#region IPaletteBorder
		/// <summary>
		/// Gets a value indicating if border should be drawn.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetBorderDraw(PaletteState state)
		{
            if (_overrideBorderToFalse)
                return InheritBool.False;
            else
			    return _redirect.GetBorderDraw(_style, state);
		}

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteState state)
        {
            return _redirect.GetBorderDrawBorders(_style, state);
        }

		/// <summary>
		/// Gets the graphics drawing hint.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
		public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state)
		{
			return _redirect.GetBorderGraphicsHint(_style, state);
		}

		/// <summary>
		/// Gets the first border color from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public override Color GetBorderColor1(PaletteState state)
		{
			return _redirect.GetBorderColor1(_style, state);
		}

		/// <summary>
		/// Gets the second border color from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		public override Color GetBorderColor2(PaletteState state)
		{
			return _redirect.GetBorderColor2(_style, state);
		}

		/// <summary>
		/// Gets the color drawing style from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
		public override PaletteColorStyle GetBorderColorStyle(PaletteState state)
		{
			return _redirect.GetBorderColorStyle(_style, state);
		}

		/// <summary>
		/// Gets the color alignment style from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
		public override PaletteRectangleAlign GetBorderColorAlign(PaletteState state)
		{
			return _redirect.GetBorderColorAlign(_style, state);
		}

		/// <summary>
		/// Gets the color border angle from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
		public override float GetBorderColorAngle(PaletteState state)
		{
			return _redirect.GetBorderColorAngle(_style, state);
		}

		/// <summary>
		/// Gets the border width from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Border width.</returns>
		public override int GetBorderWidth(PaletteState state)
		{
			return _redirect.GetBorderWidth(_style, state);
		}

		/// <summary>
		/// Gets the border rounding from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Border rounding.</returns>
		public override int GetBorderRounding(PaletteState state)
		{
			return _redirect.GetBorderRounding(_style, state);
		}

		/// <summary>
		/// Gets a border image from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
		public override Image GetBorderImage(PaletteState state)
		{
			return _redirect.GetBorderImage(_style, state);
		}

		/// <summary>
		/// Gets the border image style from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
		public override PaletteImageStyle GetBorderImageStyle(PaletteState state)
		{
			return _redirect.GetBorderImageStyle(_style, state);
		}

		/// <summary>
		/// Gets the image alignment style from the redirector.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
		public override PaletteRectangleAlign GetBorderImageAlign(PaletteState state)
		{
			return _redirect.GetBorderImageAlign(_style, state);
		}
        #endregion
	}
}
