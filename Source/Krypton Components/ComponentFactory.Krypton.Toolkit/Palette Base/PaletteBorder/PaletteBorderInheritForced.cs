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
    public class PaletteBorderInheritForced : PaletteBorderInherit
    {
        #region Instance Fields
        private IPaletteBorder _inherit;
        private PaletteDrawBorders _maxBorderEdges;
        private PaletteDrawBorders _forceBorderEdges;
        private PaletteGraphicsHint _forceGraphicsHint;
        private bool _forceBorders;
        private bool _borderIgnoreNormal;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBorderInheritForced class.
        /// </summary>
        /// <param name="inherit">Border palette to inherit from.</param>
        public PaletteBorderInheritForced(IPaletteBorder inherit)
        {
            // Remember inheritance border
            _inherit = inherit;

            // Default values
            _maxBorderEdges = PaletteDrawBorders.All;
            _forceGraphicsHint = PaletteGraphicsHint.Inherit;
            _borderIgnoreNormal = false;
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Gets and sets the palette to inherit from.
        /// </summary>
        public void SetInherit(IPaletteBorder paletteBorder)
        {
            Debug.Assert(paletteBorder != null);
            _inherit = paletteBorder; 
        }
        #endregion

        #region ForceBorderEdges
        /// <summary>
        /// Force the border edges to a particular value.
        /// </summary>
        /// <param name="forceBorderEdges">Borders to force.</param>
        public void ForceBorderEdges(PaletteDrawBorders forceBorderEdges)
        {
            _forceBorderEdges = forceBorderEdges;
            _forceBorders = true;
        }
        #endregion

        #region MaxBorderEdges
        /// <summary>
        /// Gets and sets the maximum edges allowed.
        /// </summary>
        public PaletteDrawBorders MaxBorderEdges
        {
            get { return _maxBorderEdges; }
            set { _maxBorderEdges = value; }
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

        #region IPaletteBorder
		/// <summary>
		/// Gets a value indicating if border should be drawn.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
        public override InheritBool GetBorderDraw(PaletteState state)
        {
            return _inherit.GetBorderDraw(state);
        }

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteState state)
        {
            if (_forceBorders)
                return _forceBorderEdges;
            else
            {
                // If no border edges are allowed then provide none
                if ((_maxBorderEdges == PaletteDrawBorders.None) || (_borderIgnoreNormal && (state == PaletteState.Normal)))
                    return PaletteDrawBorders.None;
                else
                {
                    // Get the requested set of edges
                    PaletteDrawBorders inheritEdges = _inherit.GetBorderDrawBorders(state);

                    // Limit the edges to those allowed
                    return (inheritEdges & _maxBorderEdges);
                }
            }
        }

		/// <summary>
		/// Gets the graphics drawing hint.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state)
        {
            if (_forceGraphicsHint != PaletteGraphicsHint.Inherit)
                return _forceGraphicsHint;
            else
                return _inherit.GetBorderGraphicsHint(state);
        }

		/// <summary>
		/// Gets the first border color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
        public override Color GetBorderColor1(PaletteState state)
        {
            return _inherit.GetBorderColor1(state);
        }

		/// <summary>
		/// Gets the second border color.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
        public override Color GetBorderColor2(PaletteState state)
        {
            return _inherit.GetBorderColor2(state);
        }

		/// <summary>
		/// Gets the color drawing style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBorderColorStyle(PaletteState state)
        {
            return _inherit.GetBorderColorStyle(state);
        }

		/// <summary>
		/// Gets the color alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBorderColorAlign(PaletteState state)
        {
            return _inherit.GetBorderColorAlign(state);
        }

		/// <summary>
		/// Gets the color border angle.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
        public override float GetBorderColorAngle(PaletteState state)
        {
            return _inherit.GetBorderColorAngle(state);
        }

		/// <summary>
		/// Gets the border width.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Border width.</returns>
        public override int GetBorderWidth(PaletteState state)
        {
            return _inherit.GetBorderWidth(state);
        }

		/// <summary>
		/// Gets the border rounding.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Border rounding.</returns>
        public override int GetBorderRounding(PaletteState state)
        {
            return _inherit.GetBorderRounding(state);
        }

		/// <summary>
		/// Gets a border image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
        public override Image GetBorderImage(PaletteState state)
        {
            return _inherit.GetBorderImage(state);
        }

		/// <summary>
		/// Gets the border image style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBorderImageStyle(PaletteState state)
        {
            return _inherit.GetBorderImageStyle(state);
        }

		/// <summary>
		/// Gets the image alignment style.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBorderImageAlign(PaletteState state)
        {
            return _inherit.GetBorderImageAlign(state);
        }
        #endregion
	}
}
