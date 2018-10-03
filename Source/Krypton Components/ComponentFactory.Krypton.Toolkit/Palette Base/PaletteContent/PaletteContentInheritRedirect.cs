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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Provide inheritance of palette content properties from source redirector.
	/// </summary>
	public class PaletteContentInheritRedirect : PaletteContentInherit
	{
		#region Instance Fields
		private PaletteRedirect _redirect;
		private PaletteContentStyle _style;
		#endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContentInheritRedirect class.
        /// </summary>
        /// <param name="style">Style used in requests.</param>
        public PaletteContentInheritRedirect(PaletteContentStyle style)
            : this(null, style)
        {
        }
        
        /// <summary>
		/// Initialize a new instance of the PaletteContentInheritRedirect class.
		/// </summary>
		/// <param name="redirect">Source for inherit requests.</param>
        public PaletteContentInheritRedirect(PaletteRedirect redirect)
            : this(redirect, PaletteContentStyle.ButtonStandalone)
        {
        }

        /// <summary>
		/// Initialize a new instance of the PaletteContentInheritRedirect class.
		/// </summary>
		/// <param name="redirect">Source for inherit requests.</param>
		/// <param name="style">Style used in requests.</param>
		public PaletteContentInheritRedirect(PaletteRedirect redirect,
											 PaletteContentStyle style)
		{
			_redirect = redirect;
			_style = style;
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
		public PaletteContentStyle Style
		{
			get { return _style; }
            set { _style = value; }
		}
		#endregion

		#region IPaletteContent
		/// <summary>
		/// Gets a value indicating if content should be drawn.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentDraw(PaletteState state)
		{
            return _redirect.GetContentDraw(_style, state);
		}

		/// <summary>
		/// Gets a value indicating if content should be drawn with focus indication.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentDrawFocus(PaletteState state)
		{
			return _redirect.GetContentDrawFocus(_style, state);
		}

		/// <summary>
		/// Gets the horizontal relative alignment of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentImageH(PaletteState state)
		{
			return _redirect.GetContentImageH(_style, state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentImageV(PaletteState state)
		{
			return _redirect.GetContentImageV(_style, state);
		}

		/// <summary>
		/// Gets the effect applied to drawing of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteImageEffect value.</returns>
		public override PaletteImageEffect GetContentImageEffect(PaletteState state)
		{
			return _redirect.GetContentImageEffect(_style, state);
		}

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorMap(PaletteState state)
        {
            return _redirect.GetContentImageColorMap(_style, state);
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTo(PaletteState state)
        {
            return _redirect.GetContentImageColorTo(_style, state);
        }

		/// <summary>
		/// Gets the font for the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public override Font GetContentShortTextFont(PaletteState state)
		{
			return _redirect.GetContentShortTextFont(_style, state);
		}

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextNewFont(PaletteState state)
		{
            return _redirect.GetContentShortTextNewFont(_style, state);
		}

		/// <summary>
		/// Gets the rendering hint for the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public override PaletteTextHint GetContentShortTextHint(PaletteState state)
		{
			return _redirect.GetContentShortTextHint(_style, state);
		}

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
        {
            return _redirect.GetContentShortTextPrefix(_style, state);
        }
        
        /// <summary>
		/// Gets the flag indicating if multiline text is allowed for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentShortTextMultiLine(PaletteState state)
		{
			return _redirect.GetContentShortTextMultiLine(_style, state);
		}

		/// <summary>
		/// Gets the text trimming to use for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public override PaletteTextTrim GetContentShortTextTrim(PaletteState state)
		{
			return _redirect.GetContentShortTextTrim(_style, state);
		}

		/// <summary>
		/// Gets the horizontal relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextH(PaletteState state)
		{
			return _redirect.GetContentShortTextH(_style, state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextV(PaletteState state)
		{
			return _redirect.GetContentShortTextV(_style, state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state)
		{
			return _redirect.GetContentShortTextMultiLineH(_style, state);
		}

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state)
        {
            return _redirect.GetContentShortTextColor1(_style, state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state)
        {
            return _redirect.GetContentShortTextColor2(_style, state);
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentShortTextColorStyle(PaletteState state)
        {
            return _redirect.GetContentShortTextColorStyle(_style, state);
        }

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state)
        {
            return _redirect.GetContentShortTextColorAlign(_style, state);
        }

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentShortTextColorAngle(PaletteState state)
        {
            return _redirect.GetContentShortTextColorAngle(_style, state);
        }

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentShortTextImage(PaletteState state)
        {
            return _redirect.GetContentShortTextImage(_style, state);
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentShortTextImageStyle(PaletteState state)
        {
            return _redirect.GetContentShortTextImageStyle(_style, state);
        }

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state)
        {
            return _redirect.GetContentShortTextImageAlign(_style, state);
        }

		/// <summary>
		/// Gets the font for the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public override Font GetContentLongTextFont(PaletteState state)
		{
			return _redirect.GetContentLongTextFont(_style, state);
		}

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentLongTextNewFont(PaletteState state)
        {
            return _redirect.GetContentLongTextNewFont(_style, state);
        }

		/// <summary>
		/// Gets the rendering hint for the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public override PaletteTextHint GetContentLongTextHint(PaletteState state)
		{
			return _redirect.GetContentLongTextHint(_style, state);
		}

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state)
        {
            return _redirect.GetContentLongTextPrefix(_style, state);
        }
        
        /// <summary>
		/// Gets the flag indicating if multiline text is allowed for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentLongTextMultiLine(PaletteState state)
		{
			return _redirect.GetContentLongTextMultiLine(_style, state);
		}

		/// <summary>
		/// Gets the text trimming to use for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public override PaletteTextTrim GetContentLongTextTrim(PaletteState state)
		{
			return _redirect.GetContentLongTextTrim(_style, state);
		}

		/// <summary>
		/// Gets the horizontal relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextH(PaletteState state)
		{
			return _redirect.GetContentLongTextH(_style, state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextV(PaletteState state)
		{
			return _redirect.GetContentLongTextV(_style, state);
		}

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state)
		{
			return _redirect.GetContentLongTextMultiLineH(_style, state);
		}

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state)
        {
            return _redirect.GetContentLongTextColor1(_style, state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state)
        {
            return _redirect.GetContentLongTextColor2(_style, state);
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentLongTextColorStyle(PaletteState state)
        {
            return _redirect.GetContentLongTextColorStyle(_style, state);
        }

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state)
        {
            return _redirect.GetContentLongTextColorAlign(_style, state);
        }

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentLongTextColorAngle(PaletteState state)
        {
            return _redirect.GetContentLongTextColorAngle(_style, state);
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentLongTextImage(PaletteState state)
        {
            return _redirect.GetContentLongTextImage(_style, state);
        }

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentLongTextImageStyle(PaletteState state)
        {
            return _redirect.GetContentLongTextImageStyle(_style, state);
        }

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state)
        {
            return _redirect.GetContentLongTextImageAlign(_style, state);
        }
        
        /// <summary>
		/// Gets the padding between the border and content drawing.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Padding value.</returns>
		public override Padding GetContentPadding(PaletteState state)
		{
			return _redirect.GetContentPadding(_style, state);
		}

		/// <summary>
		/// Gets the padding between adjacent content items.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer value.</returns>
		public override int GetContentAdjacentGap(PaletteState state)
		{
			return _redirect.GetContentAdjacentGap(_style, state);
		}

        /// <summary>
        /// Gets the style appropriate for this content.
        /// </summary>
        /// <returns>Content style.</returns>
        public override PaletteContentStyle GetContentStyle()
        {
            return _style;
        }
        #endregion
	}
}
