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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Redirect ribbon tab values based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectRibbonTabContent : PaletteRedirect
    {
        #region Instance Fields
        private IPaletteRibbonBack _disabledBack;
        private IPaletteRibbonBack _normalBack;
        private IPaletteRibbonBack _pressedBack;
        private IPaletteRibbonBack _trackingBack;
        private IPaletteRibbonBack _selectedBack;
        private IPaletteRibbonBack _focusOverrideBack;
        private IPaletteRibbonText _disabledText;
        private IPaletteRibbonText _normalText;
        private IPaletteRibbonText _pressedText;
        private IPaletteRibbonText _trackingText;
        private IPaletteRibbonText _selectedText;
        private IPaletteRibbonText _focusOverrideText;
        private IPaletteContent _disabledContent;
        private IPaletteContent _normalContent;
        private IPaletteContent _pressedContent;
        private IPaletteContent _trackingContent;
        private IPaletteContent _selectedContent;
        private IPaletteContent _focusOverrideContent;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectRibbonDouble class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectRibbonTabContent(IPalette target)
            : this(target, 
                   null, null, null, null, null, null,
                   null, null, null, null, null, null,
                   null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectRibbonDouble class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabledBack">Redirection for back disabled state requests.</param>
        /// <param name="normalBack">Redirection for back normal state requests.</param>
        /// <param name="pressedBack">Redirection for back pressed state requests.</param>
        /// <param name="trackingBack">Redirection for back tracking state requests.</param>
        /// <param name="selectedBack">Redirection for selected states requests.</param>
        /// <param name="focusOverrideBack">Redirection for back focus override state requests.</param>
        /// <param name="disabledText">Redirection for text disabled state requests.</param>
        /// <param name="normalText">Redirection for text normal state requests.</param>
        /// <param name="pressedText">Redirection for text pressed state requests.</param>
        /// <param name="trackingText">Redirection for text tracking state requests.</param>
        /// <param name="selectedText">Redirection for text selected states requests.</param>
        /// <param name="focusOverrideText">Redirection for text focus override state requests.</param>
        /// <param name="disabledContent">Redirection for content disabled state requests.</param>
        /// <param name="normalContent">Redirection for content normal state requests.</param>
        /// <param name="pressedContent">Redirection for content pressed state requests.</param>
        /// <param name="trackingContent">Redirection for content tracking state requests.</param>
        /// <param name="selectedContent">Redirection for content selected states requests.</param>
        /// <param name="focusOverrideContent">Redirection for content focus override state requests.</param>
        public PaletteRedirectRibbonTabContent(IPalette target,
                                               IPaletteRibbonBack disabledBack,
                                               IPaletteRibbonBack normalBack,
                                               IPaletteRibbonBack pressedBack,
                                               IPaletteRibbonBack trackingBack,
                                               IPaletteRibbonBack selectedBack,
                                               IPaletteRibbonBack focusOverrideBack,
                                               IPaletteRibbonText disabledText,
                                               IPaletteRibbonText normalText,
                                               IPaletteRibbonText pressedText,
                                               IPaletteRibbonText trackingText,
                                               IPaletteRibbonText selectedText,
                                               IPaletteRibbonText focusOverrideText,
                                               IPaletteContent disabledContent,
                                               IPaletteContent normalContent,
                                               IPaletteContent pressedContent,
                                               IPaletteContent trackingContent,
                                               IPaletteContent selectedContent,
                                               IPaletteContent focusOverrideContent)
            : base(target)
		{
            // Remember state specific inheritance
            _disabledBack = disabledBack;
            _normalBack = normalBack;
            _pressedBack = pressedBack;
            _trackingBack = trackingBack;
            _selectedBack = selectedBack;
            _focusOverrideBack = focusOverrideBack;
            _disabledText = disabledText;
            _normalText = normalText;
            _pressedText = pressedText;
            _trackingText = trackingText;
            _selectedText = selectedText;
            _focusOverrideText = focusOverrideText;
            _disabledContent = disabledContent;
            _normalContent = normalContent;
            _pressedContent = pressedContent;
            _trackingContent = trackingContent;
            _selectedContent = selectedContent;
            _focusOverrideContent = focusOverrideContent;
        }
		#endregion

        #region SetRedirectStates
        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        /// <param name="selected">Redirection for selected states requests.</param>
        /// <param name="focusOverride">Redirection for focus override state requests.</param>
        public virtual void SetRedirectStates(PaletteRibbonTabContent disabled,
                                              PaletteRibbonTabContent normal,
                                              PaletteRibbonTabContent pressed,
                                              PaletteRibbonTabContent tracking,
                                              PaletteRibbonTabContent selected,
                                              PaletteRibbonTabContentRedirect focusOverride)
        {
            _disabledBack = disabled.TabDraw;
            _disabledText = disabled.TabDraw;
            _normalBack = normal.TabDraw;
            _normalText = normal.TabDraw;
            _pressedBack = pressed.TabDraw;
            _pressedText = pressed.TabDraw;
            _trackingBack = tracking.TabDraw;
            _trackingText = tracking.TabDraw;
            _selectedBack = selected.TabDraw;
            _selectedText = selected.TabDraw;
            _focusOverrideBack = focusOverride.TabDraw;
            _focusOverrideText = focusOverride.TabDraw;
            _disabledContent = disabled.Content;
            _normalContent = normal.Content;
            _pressedContent = pressed.Content;
            _trackingContent = tracking.Content;
            _selectedContent = selected.Content;
            _focusOverrideContent = focusOverride.Content;
        }
        #endregion

        #region ResetRedirectStates
        /// <summary>
        /// Reset the redirection states to null.
        /// </summary>
        public virtual void ResetRedirectStates()
        {
            _disabledBack = null;
            _normalBack = null;
            _pressedBack = null;
            _trackingBack = null;
            _selectedBack = null;
            _focusOverrideBack = null;
            _disabledText = null;
            _normalText = null;
            _pressedText = null;
            _trackingText = null;
            _selectedText = null;
            _focusOverrideText = null;
            _disabledContent = null;
            _normalContent = null;
            _pressedContent = null;
            _trackingContent = null;
            _selectedContent = null;
            _focusOverrideContent = null;
        }
        #endregion

        #region RibbonBack
        /// <summary>
        /// Gets the background drawing style for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon back style requested.</param>
        /// <returns>Color value.</returns>
        public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColorStyle(state);
            else
                return Target.GetRibbonBackColorStyle(style, state);
        }

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor1(state);
            else
                return Target.GetRibbonBackColor1(style, state);
        }

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor2(state);
            else
                return Target.GetRibbonBackColor2(style, state);
        }

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor3(state);
            else
                return Target.GetRibbonBackColor3(style, state);
        }

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor4(state);
            else
                return Target.GetRibbonBackColor4(style, state);
        }

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor5(state);
            else
                return Target.GetRibbonBackColor5(style, state);
        }
        #endregion

        #region RibbonText
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state)
        {
            IPaletteRibbonText inherit = GetTextInherit(state);

            if (inherit != null)
                return inherit.GetRibbonTextColor(state);
            else
                return Target.GetRibbonTextColor(style, state);
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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

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
            IPaletteContent inherit = GetContentInherit(state);

            if (inherit != null)
                return inherit.GetContentAdjacentGap(state);
            else
                return Target.GetContentAdjacentGap(style, state);
        }
        #endregion

        #region Implementation
        private IPaletteRibbonBack GetBackInherit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return _disabledBack;
                case PaletteState.Normal:
                    return _normalBack;
                case PaletteState.Pressed:
                    return _pressedBack;
                case PaletteState.Tracking:
                    return _trackingBack;
                case PaletteState.CheckedNormal:
                case PaletteState.CheckedPressed:
                case PaletteState.CheckedTracking:
                    return _selectedBack;
                case PaletteState.FocusOverride:
                    return _focusOverrideBack;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonText GetTextInherit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return _disabledText;
                case PaletteState.Normal:
                    return _normalText;
                case PaletteState.Pressed:
                    return _pressedText;
                case PaletteState.Tracking:
                    return _trackingText;
                case PaletteState.CheckedNormal:
                case PaletteState.CheckedPressed:
                case PaletteState.CheckedTracking:
                    return _selectedText;
                case PaletteState.FocusOverride:
                    return _focusOverrideText;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteContent GetContentInherit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return _disabledContent;
                case PaletteState.Normal:
                    return _normalContent;
                case PaletteState.Pressed:
                    return _pressedContent;
                case PaletteState.Tracking:
                    return _trackingContent;
                case PaletteState.CheckedNormal:
                case PaletteState.CheckedPressed:
                case PaletteState.CheckedTracking:
                    return _selectedContent;
                case PaletteState.FocusOverride:
                    return _focusOverrideContent;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }
        #endregion
    }
}
