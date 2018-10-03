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
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class PaletteGalleryBackBorder : IPaletteBack,
                                              IPaletteBorder
    {
        #region Instance Fields
        private PaletteGalleryState _state;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteGalleryBackBorder class.
        /// </summary>
        /// <param name="state">Initial state for background/border.</param>
        public PaletteGalleryBackBorder(PaletteGalleryState state)
        {
            Debug.Assert(state != null);
            _state = state;
        }
        #endregion

        #region SetState
        /// <summary>
        /// Define the new state to use for sourcing values.
        /// </summary>
        /// <param name="state">New state for background/border.</param>
        public void SetState(PaletteGalleryState state)
        {
            Debug.Assert(state != null);
            _state = state;
        }
        #endregion

        #region IPaletteBack
        /// <summary>
        /// Gets the actual background draw value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetBackDraw(PaletteState state)
        {
            return InheritBool.True;
        }

        /// <summary>
        /// Gets the actual background graphics hint value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public PaletteGraphicsHint GetBackGraphicsHint(PaletteState state)
        {
            return PaletteGraphicsHint.AntiAlias;
        }

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor1(PaletteState state)
        {
            return _state.RibbonGalleryBack.GetRibbonBackColor1(state);
        }

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBackColor2(PaletteState state)
        {
            return _state.RibbonGalleryBack.GetRibbonBackColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetBackColorStyle(PaletteState state)
        {
            return PaletteColorStyle.Solid;
        }

        /// <summary>
        /// Gets the color alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetBackColorAlign(PaletteState state)
        {
            return PaletteRectangleAlign.Local;
        }

        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetBackColorAngle(PaletteState state)
        {
            return 0f;
        }

        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetBackImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetBackImageStyle(PaletteState state)
        {
            return PaletteImageStyle.Stretch;
        }

        /// <summary>
        /// Gets the image alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetBackImageAlign(PaletteState state)
        {
            return PaletteRectangleAlign.Local;
        }
        #endregion

        #region IPaletteBorder
        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetBorderDraw(PaletteState state)
        {
            return InheritBool.True;
        }

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public PaletteDrawBorders GetBorderDrawBorders(PaletteState state)
        {
            return PaletteDrawBorders.TopBottomLeft;
        }

        /// <summary>
        /// Gets the graphics drawing hint.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state)
        {
            return PaletteGraphicsHint.AntiAlias;
        }

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBorderColor1(PaletteState state)
        {
            return _state.RibbonGalleryBorder.GetRibbonBackColor1(state);
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetBorderColor2(PaletteState state)
        {
            return _state.RibbonGalleryBorder.GetRibbonBackColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public PaletteColorStyle GetBorderColorStyle(PaletteState state)
        {
            return PaletteColorStyle.Solid;
        }

        /// <summary>
        /// Gets the color alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public PaletteRectangleAlign GetBorderColorAlign(PaletteState state)
        {
            return PaletteRectangleAlign.Local;
        }

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public float GetBorderColorAngle(PaletteState state)
        {
            return 0f;
        }

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Border width.</returns>
        public int GetBorderWidth(PaletteState state)
        {
            return 1;
        }

        /// <summary>
        /// Gets the border rounding.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Border rounding.</returns>
        public int GetBorderRounding(PaletteState state)
        {
            return 0;
        }

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public Image GetBorderImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public PaletteImageStyle GetBorderImageStyle(PaletteState state)
        {
            return PaletteImageStyle.Stretch;
        }

        /// <summary>
        /// Gets the image alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public PaletteRectangleAlign GetBorderImageAlign(PaletteState state)
        {
            return PaletteRectangleAlign.Local;
        }
        #endregion
    }
}
