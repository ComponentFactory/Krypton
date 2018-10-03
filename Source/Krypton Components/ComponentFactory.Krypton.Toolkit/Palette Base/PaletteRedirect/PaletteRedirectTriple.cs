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
    /// Redirect back/border/content based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectTriple : PaletteRedirect
    {
        #region Instance Fields
        private IPaletteTriple _disabled;
        private IPaletteTriple _normal;
        private IPaletteTriple _pressed;
        private IPaletteTriple _tracking;
        private IPaletteTriple _checkedNormal;
        private IPaletteTriple _checkedPressed;
        private IPaletteTriple _checkedTracking;
        private IPaletteTriple _focusOverride;
        private IPaletteTriple _normalDefaultOverride;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectTriple class.
        /// </summary>
        public PaletteRedirectTriple()
            : this(null, null, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectTriple class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectTriple(IPalette target)
            : this(target, null, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectTriple class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        public PaletteRedirectTriple(IPalette target,
                                     IPaletteTriple disabled,
                                     IPaletteTriple normal)
            : this(target, disabled, normal, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectTriple class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        public PaletteRedirectTriple(IPalette target,
                                     IPaletteTriple disabled,
                                     IPaletteTriple normal,
                                     IPaletteTriple tracking)
            : this(target, disabled, normal, null, tracking, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectTriple class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        /// <param name="selected">Redirection for all checked states.</param>
        /// <param name="focusOverride">Redirection for focus override state requests.</param>
        public PaletteRedirectTriple(IPalette target,
                                     IPaletteTriple disabled,
                                     IPaletteTriple normal,
                                     IPaletteTriple pressed,
                                     IPaletteTriple tracking,
                                     IPaletteTriple selected,
                                     IPaletteTriple focusOverride)
            : this(target, disabled, normal, pressed, tracking, selected, 
                   selected, selected, focusOverride, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectTriple class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        /// <param name="checkedNormal">Redirection for checked normal state requests.</param>
        /// <param name="checkedPressed">Redirection for checked pressed state requests.</param>
        /// <param name="checkedTracking">Redirection for checked tracking state requests.</param>
        /// <param name="focusOverride">Redirection for focus override state requests.</param>
        /// <param name="normalDefaultOverride">Redirection for normal default override state requests.</param>
        public PaletteRedirectTriple(IPalette target,
                                     IPaletteTriple disabled,
                                     IPaletteTriple normal,
                                     IPaletteTriple pressed,
                                     IPaletteTriple tracking,
                                     IPaletteTriple checkedNormal,
                                     IPaletteTriple checkedPressed,
                                     IPaletteTriple checkedTracking,
                                     IPaletteTriple focusOverride,
                                     IPaletteTriple normalDefaultOverride)
            : base(target)
		{
            // Remember state specific inheritance
            _disabled = disabled;
            _normal = normal;
            _pressed = pressed;
            _tracking = tracking;
            _checkedNormal = checkedNormal;
            _checkedPressed = checkedPressed;
            _checkedTracking = checkedTracking;
            _focusOverride = focusOverride;
            _normalDefaultOverride = normalDefaultOverride;
        }
		#endregion

        #region SetRedirectStates
        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        public virtual void SetRedirectStates(IPaletteTriple disabled,
                                              IPaletteTriple normal)
        {
            _disabled = disabled;
            _normal = normal;
        }

        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        /// <param name="checkedNormal">Redirection for checked normal state requests.</param>
        /// <param name="checkedPressed">Redirection for checked pressed state requests.</param>
        /// <param name="checkedTracking">Redirection for checked tracking state requests.</param>
        /// <param name="focusOverride">Redirection for focus override state requests.</param>
        /// <param name="normalDefaultOverride">Redirection for normal default override state requests.</param>
        public virtual void SetRedirectStates(IPaletteTriple disabled,
                                              IPaletteTriple normal,
                                              IPaletteTriple pressed,
                                              IPaletteTriple tracking,
                                              IPaletteTriple checkedNormal,
                                              IPaletteTriple checkedPressed,
                                              IPaletteTriple checkedTracking,
                                              IPaletteTriple focusOverride,
                                              IPaletteTriple normalDefaultOverride)
        {
            _disabled = disabled;
            _normal = normal;
            _pressed = pressed;
            _tracking = tracking;
            _checkedNormal = checkedNormal;
            _checkedPressed = checkedPressed;
            _checkedTracking = checkedTracking;
            _focusOverride = focusOverride;
            _normalDefaultOverride = normalDefaultOverride;
        }
        #endregion

        #region ResetRedirectStates
        /// <summary>
        /// Reset the redirection states to null.
        /// </summary>
        public virtual void ResetRedirectStates()
        {
            _disabled = null;
            _normal = null;
            _pressed = null;
            _tracking = null;
            _checkedNormal = null;
            _checkedPressed = null;
            _checkedTracking = null;
            _focusOverride = null;
            _normalDefaultOverride = null;
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackGraphicsHint(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColor1(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColor2(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColorStyle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColorAlign(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColorAngle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackImage(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackImageStyle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackImageAlign(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderDraw(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderDrawBorders(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderGraphicsHint(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColor1(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColor2(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColorStyle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColorAlign(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColorAngle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderWidth(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderRounding(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderImage(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderImageStyle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderImageAlign(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentDraw(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentDrawFocus(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentImageH(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentImageV(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentImageEffect(state);
            else
                return Target.GetContentImageEffect(style, state);
        }

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentImageColorMap(state);
            else
                return Target.GetContentImageColorMap(style, state);
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
        {
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentImageColorTo(state);
            else
                return Target.GetContentImageColorTo(style, state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextHint(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextPrefix(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextMultiLine(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextTrim(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextH(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextV(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextMultiLineH(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColor1(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColor2(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColorStyle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColorAlign(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextColorAngle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextImage(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextImageStyle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentShortTextImageAlign(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextFont(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextHint(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextMultiLine(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextTrim(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextPrefix(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextH(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextV(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextMultiLineH(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColor1(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColor2(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColorStyle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColorAlign(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextColorAngle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextImage(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextImageStyle(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentLongTextImageAlign(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentPadding(state);
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
            IPaletteTriple inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteContent.GetContentAdjacentGap(state);
            else
                return Target.GetContentAdjacentGap(style, state);
        }
        #endregion

        #region Implementation
        private IPaletteTriple GetInherit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return _disabled;
                case PaletteState.Normal:
                    return _normal;
                case PaletteState.Pressed:
                    return _pressed;
                case PaletteState.Tracking:
                    return _tracking;
                case PaletteState.CheckedNormal:
                    return _checkedNormal;
                case PaletteState.CheckedPressed:
                    return _checkedPressed;
                case PaletteState.CheckedTracking:
                    return _checkedTracking;
                case PaletteState.FocusOverride:
                    return _focusOverride;
                case PaletteState.NormalDefaultOverride:
                    return _normalDefaultOverride;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }
        #endregion
    }
}
