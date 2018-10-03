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

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Provide inheritance of palette content properties.
	/// </summary>
    public abstract class PaletteContentInherit : GlobalId,
                                                  IPaletteContent
	{
        #region IPaletteContent
		/// <summary>
		/// Gets a value indicating if content should be drawn.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public abstract InheritBool GetContentDraw(PaletteState state);

		/// <summary>
		/// Gets a value indicating if content should be drawn with focus indication.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public abstract InheritBool GetContentDrawFocus(PaletteState state);

		/// <summary>
		/// Gets the horizontal relative alignment of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public abstract PaletteRelativeAlign GetContentImageH(PaletteState state);

		/// <summary>
		/// Gets the vertical relative alignment of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public abstract PaletteRelativeAlign GetContentImageV(PaletteState state);

		/// <summary>
		/// Gets the effect applied to drawing of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteImageEffect value.</returns>
		public abstract PaletteImageEffect GetContentImageEffect(PaletteState state);

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetContentImageColorMap(PaletteState state);

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetContentImageColorTo(PaletteState state);

		/// <summary>
		/// Gets the font for the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public abstract Font GetContentShortTextFont(PaletteState state);

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public abstract Font GetContentShortTextNewFont(PaletteState state);

		/// <summary>
		/// Gets the rendering hint for the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public abstract PaletteTextHint GetContentShortTextHint(PaletteState state);

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public abstract PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state);
        
        /// <summary>
		/// Gets the flag indicating if multiline text is allowed for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public abstract InheritBool GetContentShortTextMultiLine(PaletteState state);

		/// <summary>
		/// Gets the text trimming to use for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public abstract PaletteTextTrim GetContentShortTextTrim(PaletteState state);

		/// <summary>
		/// Gets the horizontal relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public abstract PaletteRelativeAlign GetContentShortTextH(PaletteState state);

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public abstract PaletteRelativeAlign GetContentShortTextV(PaletteState state);

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public abstract PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state);

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetContentShortTextColor1(PaletteState state);

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetContentShortTextColor2(PaletteState state);

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public abstract PaletteColorStyle GetContentShortTextColorStyle(PaletteState state);

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public abstract PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state);

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public abstract float GetContentShortTextColorAngle(PaletteState state);

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public abstract Image GetContentShortTextImage(PaletteState state);

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public abstract PaletteImageStyle GetContentShortTextImageStyle(PaletteState state);

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public abstract PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state);

		/// <summary>
		/// Gets the font for the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public abstract Font GetContentLongTextFont(PaletteState state);

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public abstract Font GetContentLongTextNewFont(PaletteState state);
        
        /// <summary>
		/// Gets the rendering hint for the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public abstract PaletteTextHint GetContentLongTextHint(PaletteState state);

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public abstract PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state);
        
        /// <summary>
		/// Gets the flag indicating if multiline text is allowed for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public abstract InheritBool GetContentLongTextMultiLine(PaletteState state);

		/// <summary>
		/// Gets the text trimming to use for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public abstract PaletteTextTrim GetContentLongTextTrim(PaletteState state);

		/// <summary>
		/// Gets the horizontal relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public abstract PaletteRelativeAlign GetContentLongTextH(PaletteState state);

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public abstract PaletteRelativeAlign GetContentLongTextV(PaletteState state);

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public abstract PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state);

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetContentLongTextColor1(PaletteState state);

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetContentLongTextColor2(PaletteState state);

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public abstract PaletteColorStyle GetContentLongTextColorStyle(PaletteState state);

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public abstract PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state);

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public abstract float GetContentLongTextColorAngle(PaletteState state);

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public abstract Image GetContentLongTextImage(PaletteState state);

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public abstract PaletteImageStyle GetContentLongTextImageStyle(PaletteState state);

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public abstract PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state);

        /// <summary>
		/// Gets the padding between the border and content drawing.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Padding value.</returns>
		public abstract Padding GetContentPadding(PaletteState state);

		/// <summary>
		/// Gets the padding between adjacent content items.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer value.</returns>
		public abstract int GetContentAdjacentGap(PaletteState state);

        /// <summary>
        /// Gets the style appropriate for this content.
        /// </summary>
        /// <returns>Content style.</returns>
        public abstract PaletteContentStyle GetContentStyle();
        #endregion
	}
}
