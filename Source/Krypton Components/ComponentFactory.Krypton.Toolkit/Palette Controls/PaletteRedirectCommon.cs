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
    /// Redirect back/border/content based on the enabled/disabled state.
    /// </summary>
    public class PaletteRedirectCommon : PaletteRedirect
    {
        #region Instance Fields
        private IPaletteTriple _disabled;
        private IPaletteTriple _others;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectCommon class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="others">Redirection for all other state requests.</param>
        public PaletteRedirectCommon(IPalette target,
                                     IPaletteTriple disabled,
                                     IPaletteTriple others)
            : base(target)
		{
            Debug.Assert(disabled != null);
            Debug.Assert(others != null);

            // Remember state specific inheritance
            _disabled = disabled;
            _others = others;
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
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackDraw(state);
            else
                return base.GetBackDraw(style, state);
        }

        /// <summary>
        /// Gets the graphics drawing hint for the background.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackGraphicsHint(state);
            else
                return base.GetBackGraphicsHint(style, state);
        }

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackColor1(state);
            else
                return base.GetBackColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackColor2(state);
            else
                return base.GetBackColor2(style, state);
        }

        /// <summary>
        /// Gets the color background drawing style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackColorStyle(state);
            else
                return base.GetBackColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackColorAlign(state);
            else
                return base.GetBackColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackColorAngle(state);
            else
                return base.GetBackColorAngle(style, state);
        }

        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBackImage(PaletteBackStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackImage(state);
            else
                return base.GetBackImage(style, state);
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackImageStyle(state);
            else
                return base.GetBackImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBack.GetBackImageAlign(state);
            else
                return base.GetBackImageAlign(style, state);
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
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderDraw(state);
            else
                return base.GetBorderDraw(style, state); 
        }

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderDrawBorders(state);
            else
                return base.GetBorderDrawBorders(style, state);
        }

        /// <summary>
        /// Gets the graphics drawing hint for the border.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderGraphicsHint(state);
            else
                return base.GetBorderGraphicsHint(style, state);
        }

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColor1(state);
            else
                return base.GetBorderColor1(style, state);
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColor2(state);
            else
                return base.GetBorderColor2(style, state);
        }

        /// <summary>
        /// Gets the color border drawing style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColorStyle(state);
            else
                return base.GetBorderColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColorAlign(state);
            else
                return base.GetBorderColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColorAngle(state);
            else
                return base.GetBorderColorAngle(style, state);
        }

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer width.</returns>
        public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderWidth(state);
            else
                return base.GetBorderWidth(style, state);
        }

        /// <summary>
        /// Gets the border corner rounding.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer rounding.</returns>
        public override int GetBorderRounding(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderRounding(state);
            else
                return base.GetBorderRounding(style, state);
        }

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBorderImage(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderImage(state);
            else
                return base.GetBorderImage(style, state);
        }

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderImageStyle(state);
            else
                return base.GetBorderImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteBorder.GetBorderImageAlign(state);
            else
                return base.GetBorderImageAlign(style, state);
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
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentDraw(state);
            else
                return base.GetContentDraw(style, state);
        }

        /// <summary>
        /// Gets a value indicating if content should be drawn with focus indication.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentDrawFocus(state);
            else
                return base.GetContentDrawFocus(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentImageH(state);
            else
                return base.GetContentImageH(style, state);
        }

        /// <summary>
        /// Gets the vertical relative alignment of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentImageV(state);
            else
                return base.GetContentImageV(style, state);
        }

        /// <summary>
        /// Gets the effect applied to drawing of the image.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteImageEffect value.</returns>
        public override PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentImageEffect(state);
            else
                return base.GetContentImageEffect(style, state);
        }

        /// <summary>
        /// Gets the font for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextFont(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextFont(state);
            else
                return base.GetContentShortTextFont(style, state);
        }

        /// <summary>
        /// Gets the rendering hint for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextHint(state);
            else
                return base.GetContentShortTextHint(style, state);
        }

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextPrefix(state);
            else
                return base.GetContentShortTextPrefix(style, state);
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextMultiLine(state);
            else
                return base.GetContentShortTextMultiLine(style, state);
        }

        /// <summary>
        /// Gets the text trimming to use for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextTrim(state);
            else
                return base.GetContentShortTextTrim(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextH(state);
            else
                return base.GetContentShortTextH(style, state);
        }

        /// <summary>
        /// Gets the vertical relative alignment of the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextV(state);
            else
                return base.GetContentShortTextV(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of multiline short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextMultiLineH(state);
            else
                return base.GetContentShortTextMultiLineH(style, state);
        }

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColor1(state);
            else
                return base.GetContentShortTextColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColor2(state);
            else
                return base.GetContentShortTextColor2(style, state);
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColorStyle(state);
            else
                return base.GetContentShortTextColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColorAlign(state);
            else
                return base.GetContentShortTextColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColorAngle(state);
            else
                return base.GetContentShortTextColorAngle(style, state);
        }

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentShortTextImage(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextImage(state);
            else
                return base.GetContentShortTextImage(style, state);
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextImageStyle(state);
            else
                return base.GetContentShortTextImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextImageAlign(state);
            else
                return base.GetContentShortTextImageAlign(style, state);
        }

        /// <summary>
        /// Gets the font for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentLongTextFont(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextFont(state);
            else
                return base.GetContentLongTextFont(style, state);
        }

        /// <summary>
        /// Gets the rendering hint for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextHint(state);
            else
                return base.GetContentLongTextHint(style, state);
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextMultiLine(state);
            else
                return base.GetContentLongTextMultiLine(style, state);
        }

        /// <summary>
        /// Gets the text trimming to use for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextTrim(state);
            else
                return base.GetContentLongTextTrim(style, state);
        }

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextPrefix(state);
            else
                return base.GetContentLongTextPrefix(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextH(state);
            else
                return base.GetContentLongTextH(style, state);
        }

        /// <summary>
        /// Gets the vertical relative alignment of the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextV(state);
            else
                return base.GetContentLongTextV(style, state);
        }

        /// <summary>
        /// Gets the horizontal relative alignment of multiline long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextMultiLineH(state);
            else
                return base.GetContentLongTextMultiLineH(style, state);
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColor1(state);
            else
                return base.GetContentLongTextColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColor2(state);
            else
                return base.GetContentLongTextColor2(style, state);
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColorStyle(state);
            else
                return base.GetContentLongTextColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColorAlign(state);
            else
                return base.GetContentLongTextColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColorAngle(state);
            else
                return base.GetContentLongTextColorAngle(style, state);
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextImage(state);
            else
                return base.GetContentLongTextImage(style, state);
        }

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextImageStyle(state);
            else
                return base.GetContentLongTextImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextImageAlign(state);
            else
                return base.GetContentLongTextImageAlign(style, state);
        }

        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetContentPadding(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentPadding(state);
            else
                return base.GetContentPadding(style, state);
        }

        /// <summary>
        /// Gets the padding between adjacent content items.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer value.</returns>
        public override int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);
            if (inherit != null)
                return inherit.PaletteContent.GetContentAdjacentGap(state);
            else
                return base.GetContentAdjacentGap(style, state);
        }
        #endregion

        #region Implementation
        private IPaletteTriple GetInherit(PaletteState state)
        {
            // Do not inherit the override states
            if (CommonHelper.IsOverrideState(state))
                return null;

            switch (state)
            {
                case PaletteState.Disabled:
                    Debug.Assert(_disabled != null);
                    return _disabled;
                default:
                    Debug.Assert(_others != null);
                    return _others;
            }
        }
        #endregion
    }
}
