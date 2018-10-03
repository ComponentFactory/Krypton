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
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Redirect back/border/content based on the incoming grid state and style.
    /// </summary>
    public class PaletteRedirectGrids : PaletteRedirect
    {
        #region Instance Fields
        private KryptonPaletteGrid _grid;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectTriple class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="grid">Grid reference for directing palette requests.</param>
        public PaletteRedirectGrids(IPalette target, KryptonPaletteGrid grid)
            : base(target)
        {
            Debug.Assert(grid != null);
            _grid = grid;
        }
        #endregion

        #region Back
        /// <summary>
        /// Gets a value indicating if background should be drawn.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackDraw(state);
            else
                return Target.GetBackDraw(style, state);
        }

        /// <summary>
        /// Gets the graphics drawing hint for the background.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackGraphicsHint(state);
            else
                return Target.GetBackGraphicsHint(style, state);
        }

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackColor1(state);
            else
                return Target.GetBackColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackColor2(state);
            else
                return Target.GetBackColor2(style, state);
        }

        /// <summary>
        /// Gets the color background drawing style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackColorStyle(state);
            else
                return Target.GetBackColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackColorAlign(state);
            else
                return Target.GetBackColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackColorAngle(state);
            else
                return Target.GetBackColorAngle(style, state);
        }

        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBackImage(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackImage(state);
            else
                return Target.GetBackImage(style, state);
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackImageStyle(state);
            else
                return Target.GetBackImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInheritBack(style, state);

            if (inherit != null)
                return inherit.GetBackImageAlign(state);
            else
                return Target.GetBackImageAlign(style, state);
        }
        #endregion

        #region Border
        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderDraw(state);
            else
                return Target.GetBorderDraw(style, state);
        }

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderDrawBorders(state);
            else
                return Target.GetBorderDrawBorders(style, state);
        }

        /// <summary>
        /// Gets the graphics drawing hint for the border.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderGraphicsHint(state);
            else
                return Target.GetBorderGraphicsHint(style, state);
        }

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderColor1(state);
            else
                return Target.GetBorderColor1(style, state);
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderColor2(state);
            else
                return Target.GetBorderColor2(style, state);
        }

        /// <summary>
        /// Gets the color border drawing style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderColorStyle(state);
            else
                return Target.GetBorderColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderColorAlign(state);
            else
                return Target.GetBorderColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderColorAngle(state);
            else
                return Target.GetBorderColorAngle(style, state);
        }

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer width.</returns>
        public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderWidth(state);
            else
                return Target.GetBorderWidth(style, state);
        }

        /// <summary>
        /// Gets the border corner rounding.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer rounding.</returns>
        public override int GetBorderRounding(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderRounding(state);
            else
                return Target.GetBorderRounding(style, state);
        }

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBorderImage(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderImage(state);
            else
                return Target.GetBorderImage(style, state);
        }

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderImageStyle(state);
            else
                return Target.GetBorderImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInheritBorder(style, state);

            if (inherit != null)
                return inherit.GetBorderImageAlign(state);
            else
                return Target.GetBorderImageAlign(style, state);
        }
        #endregion

        #region Content
        /// <summary>
        /// Gets a value indicating if content should be drawn.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentDraw(state);
            else
                return Target.GetContentDraw(style, state);
        }

        /// <summary>
        /// Gets a value indicating if content should be drawn with focus indication.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentDrawFocus(state);
            else
                return Target.GetContentDrawFocus(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentImageH(state);
            else
                return Target.GetContentImageH(style, state);
        }

        /// <summary>
        /// Gets the vertical relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentImageV(state);
            else
                return Target.GetContentImageV(style, state);
        }

        /// <summary>
        /// Gets the effect applied to drawing of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteImageEffect value.</returns>
        public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentImageEffect(state);
            else
                return Target.GetContentImageEffect(style, state);
        }

        /// <summary>
        /// Gets the font for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextFont(state);
            else
                return Target.GetContentShortTextFont(style, state);
        }

        /// <summary>
        /// Gets the rendering hint for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextHint(state);
            else
                return Target.GetContentShortTextHint(style, state);
        }

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextPrefix(state);
            else
                return Target.GetContentShortTextPrefix(style, state);
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextMultiLine(state);
            else
                return Target.GetContentShortTextMultiLine(style, state);
        }

        /// <summary>
        /// Gets the text trimming to use for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextTrim(state);
            else
                return Target.GetContentShortTextTrim(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextH(state);
            else
                return Target.GetContentShortTextH(style, state);
        }

        /// <summary>
        /// Gets the vertical relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextV(state);
            else
                return Target.GetContentShortTextV(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of multiline short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextMultiLineH(state);
            else
                return Target.GetContentShortTextMultiLineH(style, state);
        }

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextColor1(state);
            else
                return Target.GetContentShortTextColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextColor2(state);
            else
                return Target.GetContentShortTextColor2(style, state);
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextColorStyle(state);
            else
                return Target.GetContentShortTextColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextColorAlign(state);
            else
                return Target.GetContentShortTextColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextColorAngle(state);
            else
                return Target.GetContentShortTextColorAngle(style, state);
        }

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextImage(state);
            else
                return Target.GetContentShortTextImage(style, state);
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextImageStyle(state);
            else
                return Target.GetContentShortTextImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentShortTextImageAlign(state);
            else
                return Target.GetContentShortTextImageAlign(style, state);
        }

        /// <summary>
        /// Gets the font for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextFont(state);
            else
                return Target.GetContentLongTextFont(style, state);
        }

        /// <summary>
        /// Gets the rendering hint for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextHint(state);
            else
                return Target.GetContentLongTextHint(style, state);
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextMultiLine(state);
            else
                return Target.GetContentLongTextMultiLine(style, state);
        }

        /// <summary>
        /// Gets the text trimming to use for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextTrim(state);
            else
                return Target.GetContentLongTextTrim(style, state);
        }

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextPrefix(state);
            else
                return Target.GetContentLongTextPrefix(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextH(state);
            else
                return Target.GetContentLongTextH(style, state);
        }

        /// <summary>
        /// Gets the vertical relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextV(state);
            else
                return Target.GetContentLongTextV(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of multiline long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextMultiLineH(state);
            else
                return Target.GetContentLongTextMultiLineH(style, state);
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextColor1(state);
            else
                return Target.GetContentLongTextColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextColor2(state);
            else
                return Target.GetContentLongTextColor2(style, state);
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextColorStyle(state);
            else
                return Target.GetContentLongTextColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextColorAlign(state);
            else
                return Target.GetContentLongTextColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextColorAngle(state);
            else
                return Target.GetContentLongTextColorAngle(style, state);
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextImage(state);
            else
                return Target.GetContentLongTextImage(style, state);
        }

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextImageStyle(state);
            else
                return Target.GetContentLongTextImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentLongTextImageAlign(state);
            else
                return Target.GetContentLongTextImageAlign(style, state);
        }

        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetContentPadding(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentPadding(state);
            else
                return Target.GetContentPadding(style, state);
        }

        /// <summary>
        /// Gets the padding between adjacent content items.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer value.</returns>
        public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInheritContent(style, state);

            if (inherit != null)
                return inherit.GetContentAdjacentGap(state);
            else
                return Target.GetContentAdjacentGap(style, state);
        }
        #endregion

        #region Implementation
        private IPaletteBack GetInheritBack(PaletteBackStyle style, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    switch (style)
                    {
                        case PaletteBackStyle.GridBackgroundList:
                        case PaletteBackStyle.GridBackgroundSheet:
                        case PaletteBackStyle.GridBackgroundCustom1:
                            return _grid.StateDisabled.Background;
                        case PaletteBackStyle.GridDataCellList:
                        case PaletteBackStyle.GridDataCellSheet:
                        case PaletteBackStyle.GridDataCellCustom1:
                            return _grid.StateDisabled.DataCell.Back;
                        case PaletteBackStyle.GridHeaderColumnList:
                        case PaletteBackStyle.GridHeaderColumnSheet:
                        case PaletteBackStyle.GridHeaderColumnCustom1:
                            return _grid.StateDisabled.HeaderColumn.Back;
                        case PaletteBackStyle.GridHeaderRowList:
                        case PaletteBackStyle.GridHeaderRowSheet:
                        case PaletteBackStyle.GridHeaderRowCustom1:
                            return _grid.StateDisabled.HeaderRow.Back;
                    }
                    break;
                case PaletteState.Normal:
                    switch (style)
                    {
                        case PaletteBackStyle.GridBackgroundList:
                        case PaletteBackStyle.GridBackgroundSheet:
                        case PaletteBackStyle.GridBackgroundCustom1:
                            return _grid.StateNormal.Background;
                        case PaletteBackStyle.GridDataCellList:
                        case PaletteBackStyle.GridDataCellSheet:
                        case PaletteBackStyle.GridDataCellCustom1:
                            return _grid.StateNormal.DataCell.Back;
                        case PaletteBackStyle.GridHeaderColumnList:
                        case PaletteBackStyle.GridHeaderColumnSheet:
                        case PaletteBackStyle.GridHeaderColumnCustom1:
                            return _grid.StateNormal.HeaderColumn.Back;
                        case PaletteBackStyle.GridHeaderRowList:
                        case PaletteBackStyle.GridHeaderRowSheet:
                        case PaletteBackStyle.GridHeaderRowCustom1:
                            return _grid.StateNormal.HeaderRow.Back;
                    }
                    break;
                case PaletteState.Pressed:
                    switch (style)
                    {
                        case PaletteBackStyle.GridHeaderColumnList:
                        case PaletteBackStyle.GridHeaderColumnSheet:
                        case PaletteBackStyle.GridHeaderColumnCustom1:
                            return _grid.StatePressed.HeaderColumn.Back;
                        case PaletteBackStyle.GridHeaderRowList:
                        case PaletteBackStyle.GridHeaderRowSheet:
                        case PaletteBackStyle.GridHeaderRowCustom1:
                            return _grid.StatePressed.HeaderRow.Back;
                    }
                    break;
                case PaletteState.Tracking:
                    switch (style)
                    {
                        case PaletteBackStyle.GridHeaderColumnList:
                        case PaletteBackStyle.GridHeaderColumnSheet:
                        case PaletteBackStyle.GridHeaderColumnCustom1:
                            return _grid.StateTracking.HeaderColumn.Back;
                        case PaletteBackStyle.GridHeaderRowList:
                        case PaletteBackStyle.GridHeaderRowSheet:
                        case PaletteBackStyle.GridHeaderRowCustom1:
                            return _grid.StateTracking.HeaderRow.Back;
                    }
                    break;
                case PaletteState.CheckedNormal:
                    switch (style)
                    {
                        case PaletteBackStyle.GridDataCellList:
                        case PaletteBackStyle.GridDataCellSheet:
                        case PaletteBackStyle.GridDataCellCustom1:
                            return _grid.StateSelected.DataCell.Back;
                        case PaletteBackStyle.GridHeaderColumnList:
                        case PaletteBackStyle.GridHeaderColumnSheet:
                        case PaletteBackStyle.GridHeaderColumnCustom1:
                            return _grid.StateSelected.HeaderColumn.Back;
                        case PaletteBackStyle.GridHeaderRowList:
                        case PaletteBackStyle.GridHeaderRowSheet:
                        case PaletteBackStyle.GridHeaderRowCustom1:
                            return _grid.StateSelected.HeaderRow.Back;
                    }
                    break;
            }

            // Should never happen!
            Debug.Assert(false);
            return null;
        }

        private IPaletteBorder GetInheritBorder(PaletteBorderStyle style, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    switch (style)
                    {
                        case PaletteBorderStyle.GridDataCellList:
                        case PaletteBorderStyle.GridDataCellSheet:
                        case PaletteBorderStyle.GridDataCellCustom1:
                            return _grid.StateDisabled.DataCell.Border;
                        case PaletteBorderStyle.GridHeaderColumnList:
                        case PaletteBorderStyle.GridHeaderColumnSheet:
                        case PaletteBorderStyle.GridHeaderColumnCustom1:
                            return _grid.StateDisabled.HeaderColumn.Border;
                        case PaletteBorderStyle.GridHeaderRowList:
                        case PaletteBorderStyle.GridHeaderRowSheet:
                        case PaletteBorderStyle.GridHeaderRowCustom1:
                            return _grid.StateDisabled.HeaderRow.Border;
                    }
                    break;
                case PaletteState.Normal:
                    switch (style)
                    {
                        case PaletteBorderStyle.GridDataCellList:
                        case PaletteBorderStyle.GridDataCellSheet:
                        case PaletteBorderStyle.GridDataCellCustom1:
                            return _grid.StateNormal.DataCell.Border;
                        case PaletteBorderStyle.GridHeaderColumnList:
                        case PaletteBorderStyle.GridHeaderColumnSheet:
                        case PaletteBorderStyle.GridHeaderColumnCustom1:
                            return _grid.StateNormal.HeaderColumn.Border;
                        case PaletteBorderStyle.GridHeaderRowList:
                        case PaletteBorderStyle.GridHeaderRowSheet:
                        case PaletteBorderStyle.GridHeaderRowCustom1:
                            return _grid.StateNormal.HeaderRow.Border;
                    }
                    break;
                case PaletteState.Pressed:
                    switch (style)
                    {
                        case PaletteBorderStyle.GridHeaderColumnList:
                        case PaletteBorderStyle.GridHeaderColumnSheet:
                        case PaletteBorderStyle.GridHeaderColumnCustom1:
                            return _grid.StatePressed.HeaderColumn.Border;
                        case PaletteBorderStyle.GridHeaderRowList:
                        case PaletteBorderStyle.GridHeaderRowSheet:
                        case PaletteBorderStyle.GridHeaderRowCustom1:
                            return _grid.StatePressed.HeaderRow.Border;
                    }
                    break;
                case PaletteState.Tracking:
                    switch (style)
                    {
                        case PaletteBorderStyle.GridHeaderColumnList:
                        case PaletteBorderStyle.GridHeaderColumnSheet:
                        case PaletteBorderStyle.GridHeaderColumnCustom1:
                            return _grid.StateTracking.HeaderColumn.Border;
                        case PaletteBorderStyle.GridHeaderRowList:
                        case PaletteBorderStyle.GridHeaderRowSheet:
                        case PaletteBorderStyle.GridHeaderRowCustom1:
                            return _grid.StateTracking.HeaderRow.Border;
                    }
                    break;
                case PaletteState.CheckedNormal:
                    switch (style)
                    {
                        case PaletteBorderStyle.GridDataCellList:
                        case PaletteBorderStyle.GridDataCellSheet:
                        case PaletteBorderStyle.GridDataCellCustom1:
                            return _grid.StateSelected.DataCell.Border;
                        case PaletteBorderStyle.GridHeaderColumnList:
                        case PaletteBorderStyle.GridHeaderColumnSheet:
                        case PaletteBorderStyle.GridHeaderColumnCustom1:
                            return _grid.StateSelected.HeaderColumn.Border;
                        case PaletteBorderStyle.GridHeaderRowList:
                        case PaletteBorderStyle.GridHeaderRowSheet:
                        case PaletteBorderStyle.GridHeaderRowCustom1:
                            return _grid.StateSelected.HeaderRow.Border;
                    }
                    break;
            }

            // Should never happen!
            Debug.Assert(false);
            return null;
        }

        private IPaletteContent GetInheritContent(PaletteContentStyle style, PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    switch (style)
                    {
                        case PaletteContentStyle.GridDataCellList:
                        case PaletteContentStyle.GridDataCellSheet:
                        case PaletteContentStyle.GridDataCellCustom1:
                            return _grid.StateDisabled.DataCell.Content;
                        case PaletteContentStyle.GridHeaderColumnList:
                        case PaletteContentStyle.GridHeaderColumnSheet:
                        case PaletteContentStyle.GridHeaderColumnCustom1:
                            return _grid.StateDisabled.HeaderColumn.Content;
                        case PaletteContentStyle.GridHeaderRowList:
                        case PaletteContentStyle.GridHeaderRowSheet:
                        case PaletteContentStyle.GridHeaderRowCustom1:
                            return _grid.StateDisabled.HeaderRow.Content;
                    }
                    break;
                case PaletteState.Normal:
                    switch (style)
                    {
                        case PaletteContentStyle.GridDataCellList:
                        case PaletteContentStyle.GridDataCellSheet:
                        case PaletteContentStyle.GridDataCellCustom1:
                            return _grid.StateNormal.DataCell.Content;
                        case PaletteContentStyle.GridHeaderColumnList:
                        case PaletteContentStyle.GridHeaderColumnSheet:
                        case PaletteContentStyle.GridHeaderColumnCustom1:
                            return _grid.StateNormal.HeaderColumn.Content;
                        case PaletteContentStyle.GridHeaderRowList:
                        case PaletteContentStyle.GridHeaderRowSheet:
                        case PaletteContentStyle.GridHeaderRowCustom1:
                            return _grid.StateNormal.HeaderRow.Content;
                    }
                    break;
                case PaletteState.Pressed:
                    switch (style)
                    {
                        case PaletteContentStyle.GridHeaderColumnList:
                        case PaletteContentStyle.GridHeaderColumnSheet:
                        case PaletteContentStyle.GridHeaderColumnCustom1:
                            return _grid.StatePressed.HeaderColumn.Content;
                        case PaletteContentStyle.GridHeaderRowList:
                        case PaletteContentStyle.GridHeaderRowSheet:
                        case PaletteContentStyle.GridHeaderRowCustom1:
                            return _grid.StatePressed.HeaderRow.Content;
                    }
                    break;
                case PaletteState.Tracking:
                    switch (style)
                    {
                        case PaletteContentStyle.GridHeaderColumnList:
                        case PaletteContentStyle.GridHeaderColumnSheet:
                        case PaletteContentStyle.GridHeaderColumnCustom1:
                            return _grid.StateTracking.HeaderColumn.Content;
                        case PaletteContentStyle.GridHeaderRowList:
                        case PaletteContentStyle.GridHeaderRowSheet:
                        case PaletteContentStyle.GridHeaderRowCustom1:
                            return _grid.StateTracking.HeaderRow.Content;
                    }
                    break;
                case PaletteState.CheckedNormal:
                    switch (style)
                    {
                        case PaletteContentStyle.GridDataCellList:
                        case PaletteContentStyle.GridDataCellSheet:
                        case PaletteContentStyle.GridDataCellCustom1:
                            return _grid.StateSelected.DataCell.Content;
                        case PaletteContentStyle.GridHeaderColumnList:
                        case PaletteContentStyle.GridHeaderColumnSheet:
                        case PaletteContentStyle.GridHeaderColumnCustom1:
                            return _grid.StateSelected.HeaderColumn.Content;
                        case PaletteContentStyle.GridHeaderRowList:
                        case PaletteContentStyle.GridHeaderRowSheet:
                        case PaletteContentStyle.GridHeaderRowCustom1:
                            return _grid.StateSelected.HeaderRow.Content;
                    }
                    break;
            }

            // Should never happen!
            Debug.Assert(false);
            return null;
        }
        #endregion
    }
}
