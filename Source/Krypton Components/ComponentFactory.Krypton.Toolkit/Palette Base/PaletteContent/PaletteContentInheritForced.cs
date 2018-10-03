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
    /// Allow the content values to be forced to fixed values.
	/// </summary>
    public class PaletteContentInheritForced : PaletteContentInherit
	{
        #region Instance Fields
        private IPaletteContent _inherit;
        private bool _forceShortTextHCenter;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContentInheritForced class.
        /// </summary>
        /// <param name="inherit">Border palette to inherit from.</param>
        public PaletteContentInheritForced(IPaletteContent inherit)
        {
            // Remember inheritance border
            _inherit = inherit;

            // Default settings
            _forceShortTextHCenter = false;
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Gets and sets the palette to inherit from.
        /// </summary>
        public void SetInherit(IPaletteContent paletteContent)
        {
            Debug.Assert(paletteContent != null);
            _inherit = paletteContent;
        }
        #endregion

        #region ForceShortTextHCenter
        /// <summary>
        /// Gets and sets if the short text is centered horizontally.
        /// </summary>
        public bool ForceShortTextHCenter
        {
            get { return _forceShortTextHCenter; }
            set { _forceShortTextHCenter = value; }
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
            return _inherit.GetContentDraw(state);
        }

		/// <summary>
		/// Gets a value indicating if content should be drawn with focus indication.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
        public override InheritBool GetContentDrawFocus(PaletteState state)
        {
            return _inherit.GetContentDrawFocus(state);
        }

		/// <summary>
		/// Gets the horizontal relative alignment of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageH(PaletteState state)
        {
            return _inherit.GetContentImageH(state);
        }

		/// <summary>
		/// Gets the vertical relative alignment of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentImageV(PaletteState state)
        {
            return _inherit.GetContentImageV(state);
        }

		/// <summary>
		/// Gets the effect applied to drawing of the image.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteImageEffect value.</returns>
        public override PaletteImageEffect GetContentImageEffect(PaletteState state)
        {
            return _inherit.GetContentImageEffect(state);
        }

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorMap(PaletteState state)
        {
            return _inherit.GetContentImageColorMap(state);
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTo(PaletteState state)
        {
            return _inherit.GetContentImageColorTo(state);
        }

		/// <summary>
		/// Gets the font for the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
        public override Font GetContentShortTextFont(PaletteState state)
        {
            return _inherit.GetContentShortTextFont(state);
        }

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextNewFont(PaletteState state)
        {
            return _inherit.GetContentShortTextNewFont(state);
        }

		/// <summary>
		/// Gets the rendering hint for the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetContentShortTextHint(PaletteState state)
        {
            return _inherit.GetContentShortTextHint(state);
        }

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
        {
            return _inherit.GetContentShortTextPrefix(state);
        }
        
        /// <summary>
		/// Gets the flag indicating if multiline text is allowed for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
        public override InheritBool GetContentShortTextMultiLine(PaletteState state)
        {
            return _inherit.GetContentShortTextMultiLine(state);
        }

		/// <summary>
		/// Gets the text trimming to use for short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public override PaletteTextTrim GetContentShortTextTrim(PaletteState state)
        {
            return _inherit.GetContentShortTextTrim(state);
        }

		/// <summary>
		/// Gets the horizontal relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextH(PaletteState state)
        {
            if (ForceShortTextHCenter)
                return PaletteRelativeAlign.Center;
            else
                return _inherit.GetContentShortTextH(state);
        }

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextV(PaletteState state)
        {
            return _inherit.GetContentShortTextV(state);
        }

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state)
        {
            return _inherit.GetContentShortTextMultiLineH(state);
        }

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state)
        {
            return _inherit.GetContentShortTextColor1(state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state)
        {
            return _inherit.GetContentShortTextColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentShortTextColorStyle(PaletteState state)
        {
            return _inherit.GetContentShortTextColorStyle(state);
        }

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state)
        {
            return _inherit.GetContentShortTextColorAlign(state);
        }

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentShortTextColorAngle(PaletteState state)
        {
            return _inherit.GetContentShortTextColorAngle(state);
        }

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentShortTextImage(PaletteState state)
        {
            return _inherit.GetContentShortTextImage(state);
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentShortTextImageStyle(PaletteState state)
        {
            return _inherit.GetContentShortTextImageStyle(state);
        }

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state)
        {
            return _inherit.GetContentShortTextImageAlign(state);
        }

		/// <summary>
		/// Gets the font for the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		public override Font GetContentLongTextFont(PaletteState state)
        {
            return _inherit.GetContentLongTextFont(state);
        }

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentLongTextNewFont(PaletteState state)
        {
            return _inherit.GetContentLongTextNewFont(state);
        }
        
        /// <summary>
		/// Gets the rendering hint for the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		public override PaletteTextHint GetContentLongTextHint(PaletteState state)
        {
            return _inherit.GetContentLongTextHint(state);
        }

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state)
        {
            return _inherit.GetContentLongTextPrefix(state);
        }
        
        /// <summary>
		/// Gets the flag indicating if multiline text is allowed for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		public override InheritBool GetContentLongTextMultiLine(PaletteState state)
        {
            return _inherit.GetContentLongTextMultiLine(state);
        }

		/// <summary>
		/// Gets the text trimming to use for long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		public override PaletteTextTrim GetContentLongTextTrim(PaletteState state)
        {
            return _inherit.GetContentLongTextTrim(state);
        }

		/// <summary>
		/// Gets the horizontal relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextH(PaletteState state)
        {
            return _inherit.GetContentLongTextH(state);
        }

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextV(PaletteState state)
        {
            return _inherit.GetContentLongTextV(state);
        }

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		public override PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state)
        {
            return _inherit.GetContentLongTextMultiLineH(state);
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state)
        {
            return _inherit.GetContentLongTextColor1(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state)
        {
            return _inherit.GetContentLongTextColor2(state);
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetContentLongTextColorStyle(PaletteState state)
        {
            return _inherit.GetContentLongTextColorStyle(state);
        }

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state)
        {
            return _inherit.GetContentLongTextColorAlign(state);
        }

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetContentLongTextColorAngle(PaletteState state)
        {
            return _inherit.GetContentLongTextColorAngle(state);
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetContentLongTextImage(PaletteState state)
        {
            return _inherit.GetContentLongTextImage(state);
        }

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetContentLongTextImageStyle(PaletteState state)
        {
            return _inherit.GetContentLongTextImageStyle(state);
        }

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state)
        {
            return _inherit.GetContentLongTextImageAlign(state);
        }

        /// <summary>
		/// Gets the padding between the border and content drawing.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Padding value.</returns>
		public override Padding GetContentPadding(PaletteState state)
        {
            return _inherit.GetContentPadding(state);
        }

		/// <summary>
		/// Gets the padding between adjacent content items.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer value.</returns>
		public override int GetContentAdjacentGap(PaletteState state)
        {
            return _inherit.GetContentAdjacentGap(state);
        }

        /// <summary>
        /// Gets the style appropriate for this content.
        /// </summary>
        /// <returns>Content style.</returns>
        public override PaletteContentStyle GetContentStyle()
        {
            return _inherit.GetContentStyle();
        }
        #endregion
	}
}
