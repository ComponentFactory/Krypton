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

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Allow the background values to be forced to fixed values.
	/// </summary>
    public class PaletteBackInheritForced : PaletteBackInherit
	{
        #region Instance Fields
        private IPaletteBack _inherit;
        private PaletteGraphicsHint _forceGraphicsHint;
        private InheritBool _forceDraw;
        private bool _borderIgnoreNormal;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBackInheritForced class.
        /// </summary>
        /// <param name="inherit">Background palette to inherit from.</param>
        public PaletteBackInheritForced(IPaletteBack inherit)
        {
            Debug.Assert(inherit != null);

            // Remember inheritance border
            _inherit = inherit;

            // Default values
            _borderIgnoreNormal = false;
            _forceGraphicsHint = PaletteGraphicsHint.Inherit;
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Gets and sets the palette to inherit from.
        /// </summary>
        public void SetInherit(IPaletteBack paletteBack)
        {
            Debug.Assert(paletteBack != null);
            _inherit = paletteBack;
        }
        #endregion

        #region BorderIgnoreNormal
        /// <summary>
        /// Gets and sets the ignoring of normal borders.
        /// </summary>
        public bool BorderIgnoreNormal
        {
            get { return _borderIgnoreNormal; }
            set { _borderIgnoreNormal = value; }
        }
        #endregion

        #region ForceGraphicsHint
        /// <summary>
        /// Gets and sets the forced value for the graphics hint.
        /// </summary>
        public PaletteGraphicsHint ForceGraphicsHint
        {
            get { return _forceGraphicsHint; }
            set { _forceGraphicsHint = value; }
        }
        #endregion

        #region ForceDraw
        /// <summary>
        /// Gets and sets the forced value for the draw property.
        /// </summary>
        public InheritBool ForceDraw
        {
            get { return _forceDraw; }
            set { _forceDraw = value; }
        }
        #endregion

        #region IPaletteBack
		/// <summary>
		/// Gets a value indicating if background should be drawn.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
        public override InheritBool GetBackDraw(PaletteState state)
        {
            if (_forceDraw != InheritBool.Inherit)
                return _forceDraw;
            else
            {
                if (_borderIgnoreNormal && (state == PaletteState.Normal))
                    return InheritBool.False;
                else
                    return _inherit.GetBackDraw(state);
            }
        }

		/// <summary>
		/// Gets the graphics drawing hint.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBackGraphicsHint(PaletteState state)
        {
            if (_forceGraphicsHint != PaletteGraphicsHint.Inherit)
                return _forceGraphicsHint;
            else
                return _inherit.GetBackGraphicsHint(state);
        }

		/// <summary>
		/// Gets the first background color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
        public override Color GetBackColor1(PaletteState state)
        {
            return _inherit.GetBackColor1(state);
        }

		/// <summary>
		/// Gets the second back color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
        public override Color GetBackColor2(PaletteState state)
        {
            return _inherit.GetBackColor2(state);
        }

		/// <summary>
		/// Gets the color drawing style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBackColorStyle(PaletteState state)
        {
            return _inherit.GetBackColorStyle(state);
        }

		/// <summary>
		/// Gets the color alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBackColorAlign(PaletteState state)
        {
            return _inherit.GetBackColorAlign(state);
        }

		/// <summary>
		/// Gets the color background angle.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
        public override float GetBackColorAngle(PaletteState state)
        {
            return _inherit.GetBackColorAngle(state);
        }

		/// <summary>
		/// Gets a background image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
        public override Image GetBackImage(PaletteState state)
        {
            return _inherit.GetBackImage(state);
        }

		/// <summary>
		/// Gets the background image style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBackImageStyle(PaletteState state)
        {
            return _inherit.GetBackImageStyle(state);
        }

		/// <summary>
		/// Gets the image alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBackImageAlign(PaletteState state)
        {
            return _inherit.GetBackImageAlign(state);
        }
        #endregion
	}
}
