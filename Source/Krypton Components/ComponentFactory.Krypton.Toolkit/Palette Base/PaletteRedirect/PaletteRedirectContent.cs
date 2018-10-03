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
    /// Redirect content based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectContent : PaletteRedirect
    {
        #region Instance Fields
        private IPaletteContent _disabled;
        private IPaletteContent _normal;
        private IPaletteContent _pressed;
        private IPaletteContent _tracking;
        private IPaletteContent _checkedNormal;
        private IPaletteContent _checkedPressed;
        private IPaletteContent _checkedTracking;
        private IPaletteContent _focusOverride;
        private IPaletteContent _normalDefaultOverride;
        private IPaletteContent _linkVisitedOverride;
        private IPaletteContent _linkNotVisitedOverride;
        private IPaletteContent _linkPressedOverride;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectContent class.
        /// </summary>
        public PaletteRedirectContent()
            : this(null, null, null, null, null, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectContent class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        public PaletteRedirectContent(IPalette target,
                                      IPaletteContent disabled,
                                      IPaletteContent normal)
            : this(target, disabled, normal, null, null, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectContent class.
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
        /// <param name="linkVisitedOverride">Redirection for link visited override state requests.</param>
        /// <param name="linkNotVisitedOverride">Redirection for link not visited override state requests.</param>
        /// <param name="linkPressedOverride">Redirection for link pressed override state requests.</param>
        public PaletteRedirectContent(IPalette target,
                                      IPaletteContent disabled,
                                      IPaletteContent normal,
                                      IPaletteContent pressed,
                                      IPaletteContent tracking,
                                      IPaletteContent checkedNormal,
                                      IPaletteContent checkedPressed,
                                      IPaletteContent checkedTracking,
                                      IPaletteContent focusOverride,
                                      IPaletteContent normalDefaultOverride,
                                      IPaletteContent linkVisitedOverride,
                                      IPaletteContent linkNotVisitedOverride,
                                      IPaletteContent linkPressedOverride)
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
            _linkVisitedOverride = linkVisitedOverride;
            _linkNotVisitedOverride = linkNotVisitedOverride;
            _linkPressedOverride = linkPressedOverride;
        }
		#endregion

        #region SetRedirectStates
        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        public virtual void SetRedirectStates(IPaletteContent disabled,
                                              IPaletteContent normal)
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
        /// <param name="linkVisitedOverride">Redirection for link visited override state requests.</param>
        /// <param name="linkNotVisitedOverride">Redirection for link not visited override state requests.</param>
        /// <param name="linkPressedOverride">Redirection for link pressed override state requests.</param>
        public virtual void SetRedirectStates(IPaletteContent disabled,
                                              IPaletteContent normal,
                                              IPaletteContent pressed,
                                              IPaletteContent tracking,
                                              IPaletteContent checkedNormal,
                                              IPaletteContent checkedPressed,
                                              IPaletteContent checkedTracking,
                                              IPaletteContent focusOverride,
                                              IPaletteContent normalDefaultOverride,
                                              IPaletteContent linkVisitedOverride,
                                              IPaletteContent linkNotVisitedOverride,
                                              IPaletteContent linkPressedOverride)
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
            _linkVisitedOverride = linkVisitedOverride;
            _linkNotVisitedOverride = linkNotVisitedOverride;
            _linkPressedOverride = linkPressedOverride;
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
            _linkVisitedOverride = null;
            _linkNotVisitedOverride = null;
            _linkPressedOverride = null;
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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetContentLongTextColorAlign(state);
            else
                return Target.GetContentLongTextColorAlign(style, state);
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentLongTextImage(PaletteContentStyle style, PaletteState state)
        {
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

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
            IPaletteContent inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetContentAdjacentGap(state);
            else
                return Target.GetContentAdjacentGap(style, state);
        }
        #endregion

        #region Implementation
        private IPaletteContent GetInherit(PaletteState state)
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
                case PaletteState.LinkVisitedOverride:
                    return _linkVisitedOverride;
                case PaletteState.LinkNotVisitedOverride:
                    return _linkNotVisitedOverride;
                case PaletteState.LinkPressedOverride:
                    return _linkPressedOverride;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }
        #endregion
    }
}
