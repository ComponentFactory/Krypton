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
using System.Windows.Forms;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
	#region IPalette
	/// <summary>
	/// Exposes a palette for drawing.
	/// </summary>
	public interface IPalette
	{
        #region Events
        /// <summary>
        /// Occurs when a palette change requires a repaint.
        /// </summary>
        event EventHandler<PaletteLayoutEventArgs> PalettePaint;

        /// <summary>
        /// Occurs when the AllowFormChrome setting changes.
        /// </summary>
        event EventHandler AllowFormChromeChanged;

        /// <summary>
        /// Occurs when the BasePalette/BasePaletteMode setting changes.
        /// </summary>
        event EventHandler BasePaletteChanged;

        /// <summary>
        /// Occurs when the BaseRenderer/BaseRendererMode setting changes.
        /// </summary>
        event EventHandler BaseRendererChanged;

        /// <summary>
        /// Occurs when a button spec change occurs.
        /// </summary>
        event EventHandler ButtonSpecChanged;
        #endregion

        #region AllowFormChrome
        /// <summary>
        /// Gets a value indicating if KryptonForm instances should show custom chrome.
        /// </summary>
        /// <returns>InheritBool value.</returns>
        InheritBool GetAllowFormChrome();
        #endregion

        #region Renderer
        /// <summary>
        /// Gets the renderer to use for this palette.
        /// </summary>
        /// <returns>Renderer to use for drawing palette settings.</returns>
        IRenderer GetRenderer();
        #endregion

        #region Back
        /// <summary>
		/// Gets a value indicating if background should be drawn.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state);

		/// <summary>
		/// Gets the graphics drawing hint for the background.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
		PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state);

		/// <summary>
		/// Gets the first background color.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		Color GetBackColor1(PaletteBackStyle style, PaletteState state);

		/// <summary>
		/// Gets the second back color.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		Color GetBackColor2(PaletteBackStyle style, PaletteState state);

		/// <summary>
		/// Gets the color background drawing style.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
		PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state);

		/// <summary>
		/// Gets the color alignment.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
		PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state);

		/// <summary>
		/// Gets the color background angle.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
		float GetBackColorAngle(PaletteBackStyle style, PaletteState state);

		/// <summary>
		/// Gets a background image.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
		Image GetBackImage(PaletteBackStyle style, PaletteState state);

		/// <summary>
		/// Gets the background image style.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
		PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state);

		/// <summary>
		/// Gets the image alignment.
		/// </summary>
		/// <param name="style">Background style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
		PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state);
        #endregion

		#region Border
		/// <summary>
		/// Gets a value indicating if border should be drawn.
		/// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state);

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the graphics drawing hint for the border.
		/// </summary>
        /// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteGraphicsHint value.</returns>
		PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the first border color.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		Color GetBorderColor1(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the second border color.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color value.</returns>
		Color GetBorderColor2(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the color border drawing style.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color drawing style.</returns>
		PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the color border alignment.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Color alignment style.</returns>
		PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the color border angle.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Angle used for color drawing.</returns>
		float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the border width.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer width.</returns>
		int GetBorderWidth(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the border corner rounding.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer rounding.</returns>
		int GetBorderRounding(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets a border image.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image instance.</returns>
		Image GetBorderImage(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the border image style.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image style value.</returns>
		PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state);

		/// <summary>
		/// Gets the image border alignment.
		/// </summary>
		/// <param name="style">Border style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Image alignment style.</returns>
		PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state);
        #endregion

		#region Content
		/// <summary>
		/// Gets a value indicating if content should be drawn.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		InheritBool GetContentDraw(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets a value indicating if content should be drawn with focus indication.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		InheritBool GetContentDrawFocus(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the horizontal relative alignment of the image.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		PaletteRelativeAlign GetContentImageH(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the vertical relative alignment of the image.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		PaletteRelativeAlign GetContentImageV(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the effect applied to drawing of the image.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteImageEffect value.</returns>
		PaletteImageEffect GetContentImageEffect(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the font for the short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		Font GetContentShortTextFont(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetContentShortTextNewFont(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the rendering hint for the short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextHint value.</returns>
		PaletteTextHint GetContentShortTextHint(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the flag indicating if multiline text is allowed for short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		InheritBool GetContentShortTextMultiLine(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the text trimming to use for short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		PaletteTextTrim GetContentShortTextTrim(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteContentStyle style, PaletteState state);
        
        /// <summary>
		/// Gets the horizontal relative alignment of the short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		PaletteRelativeAlign GetContentShortTextH(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the vertical relative alignment of the short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		PaletteRelativeAlign GetContentShortTextV(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the horizontal relative alignment of multiline short text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        PaletteColorStyle GetContentShortTextColorStyle(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        PaletteRectangleAlign GetContentShortTextColorAlign(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        float GetContentShortTextColorAngle(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        Image GetContentShortTextImage(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        PaletteImageStyle GetContentShortTextImageStyle(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        PaletteRectangleAlign GetContentShortTextImageAlign(PaletteContentStyle style, PaletteState state);
        
        /// <summary>
		/// Gets the font for the long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Font value.</returns>
		Font GetContentLongTextFont(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetContentLongTextNewFont(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the rendering hint for the long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>TextRenderingHint value.</returns>
		PaletteTextHint GetContentLongTextHint(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteContentStyle style, PaletteState state);
        
        /// <summary>
		/// Gets the flag indicating if multiline text is allowed for long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>InheritBool value.</returns>
		InheritBool GetContentLongTextMultiLine(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the text trimming to use for long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>PaletteTextTrim value.</returns>
		PaletteTextTrim GetContentLongTextTrim(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the horizontal relative alignment of the long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		PaletteRelativeAlign GetContentLongTextH(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the vertical relative alignment of the long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		PaletteRelativeAlign GetContentLongTextV(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the horizontal relative alignment of multiline long text.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>RelativeAlignment value.</returns>
		PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        PaletteColorStyle GetContentLongTextColorStyle(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        PaletteRectangleAlign GetContentLongTextColorAlign(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        float GetContentLongTextColorAngle(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        Image GetContentLongTextImage(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        PaletteImageStyle GetContentLongTextImageStyle(PaletteContentStyle style, PaletteState state);

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        PaletteRectangleAlign GetContentLongTextImageAlign(PaletteContentStyle style, PaletteState state);

        /// <summary>
		/// Gets the padding between the border and content drawing.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Padding value.</returns>
		Padding GetContentPadding(PaletteContentStyle style, PaletteState state);

		/// <summary>
		/// Gets the padding between adjacent content items.
		/// </summary>
		/// <param name="style">Content style.</param>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <returns>Integer value.</returns>
		int GetContentAdjacentGap(PaletteContentStyle style, PaletteState state);
		#endregion

		#region Metric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        int GetMetricInt(PaletteState state, PaletteMetricInt metric);
        
        /// <summary>
		/// Gets a boolean metric value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <param name="metric">Requested metric.</param>
		/// <returns>InheritBool value.</returns>
		InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric);

		/// <summary>
		/// Gets a padding metric value.
		/// </summary>
		/// <param name="state">Palette value should be applicable to this state.</param>
		/// <param name="metric">Requested metric.</param>
		/// <returns>Padding value.</returns>
		Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric);
		#endregion

        #region Images
        /// <summary>
        /// Gets a tree view image appropriate for the provided state.
        /// </summary>
        /// <param name="expanded">Is the node expanded</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        Image GetTreeViewImage(bool expanded);

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the check box enabled.</param>
        /// <param name="checkState">Is the check box checked/unchecked/indeterminate.</param>
        /// <param name="tracking">Is the check box being hot tracked.</param>
        /// <param name="pressed">Is the check box being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        Image GetCheckBoxImage(bool enabled, CheckState checkState, bool tracking, bool pressed);

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="enabled">Is the radio button enabled.</param>
        /// <param name="checkState">Is the radio button checked.</param>
        /// <param name="tracking">Is the radio button being hot tracked.</param>
        /// <param name="pressed">Is the radio button being pressed.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        Image GetRadioButtonImage(bool enabled, bool checkState, bool tracking, bool pressed);

        /// <summary>
        /// Gets a drop down button image appropriate for the provided state.
        /// </summary>
        /// <param name="state">PaletteState for which image is required.</param>
        Image GetDropDownButtonImage(PaletteState state);

        /// <summary>
        /// Gets a checked image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        Image GetContextMenuCheckedImage();

        /// <summary>
        /// Gets a indeterminate image appropriate for a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        Image GetContextMenuIndeterminateImage();

        /// <summary>
        /// Gets an image indicating a sub-menu on a context menu item.
        /// </summary>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        Image GetContextMenuSubMenuImage();

        /// <summary>
        /// Gets a check box image appropriate for the provided state.
        /// </summary>
        /// <param name="button">Enum of the button to fetch.</param>
        /// <param name="state">State of the button to fetch.</param>
        /// <returns>Appropriate image for drawing; otherwise null.</returns>
        Image GetGalleryButtonImage(PaletteRibbonGalleryButton button, PaletteState state);
        #endregion

        #region ButtonSpec
        /// <summary>
        /// Gets the icon to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Icon value.</returns>
        Icon GetButtonSpecIcon(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the image to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <param name="state">State for which image is required.</param>
        /// <returns>Image value.</returns>
        Image GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state);

        /// <summary>
        /// Gets the image transparent color.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the short text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        string GetButtonSpecShortText(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the long text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        string GetButtonSpecLongText(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the tooltip title text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the color to remap from the image to the container foreground.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        Color GetButtonSpecColorMap(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the button style used for drawing the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonStyle value.</returns>
        PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style);

        /// <summary>
        /// Get the location for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>HeaderLocation value.</returns>
        HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the edge to positon the button against.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteRelativeEdgeAlign value.</returns>
        PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the button orientation.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonOrientation value.</returns>
        PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style);
        #endregion

        #region RibbonGeneral
        /// <summary>
        /// Gets the ribbon shape that should be used.
        /// </summary>
        /// <returns>Ribbon shape value.</returns>
        PaletteRibbonShape GetRibbonShape();

        /// <summary>
        /// Gets the text alignment for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state);

        /// <summary>
        /// Gets the font for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetRibbonContextTextFont(PaletteState state);

        /// <summary>
        /// Gets the color for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Color GetRibbonContextTextColor(PaletteState state);

        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonDisabledDark(PaletteState state);

        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonDisabledLight(PaletteState state);

        /// <summary>
        /// Gets the color for the drop arrow light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonDropArrowLight(PaletteState state);

        /// <summary>
        /// Gets the color for the drop arrow dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonDropArrowDark(PaletteState state);

        /// <summary>
        /// Gets the color for the group dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonGroupDialogDark(PaletteState state);

        /// <summary>
        /// Gets the color for the group dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonGroupDialogLight(PaletteState state);

        /// <summary>
        /// Gets the color for the group separator dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonGroupSeparatorDark(PaletteState state);

        /// <summary>
        /// Gets the color for the group separator light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonGroupSeparatorLight(PaletteState state);

        /// <summary>
        /// Gets the color for the minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonMinimizeBarDark(PaletteState state);

        /// <summary>
        /// Gets the color for the minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonMinimizeBarLight(PaletteState state);

        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonTabSeparatorColor(PaletteState state);

        /// <summary>
        /// Gets the color for the tab context separators.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonTabSeparatorContextColor(PaletteState state);

        /// <summary>
        /// Gets the font for the ribbon text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetRibbonTextFont(PaletteState state);

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        PaletteTextHint GetRibbonTextHint(PaletteState state);

        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonQATButtonDark(PaletteState state);

        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonQATButtonLight(PaletteState state);
        #endregion

        #region RibbonBack
        /// <summary>
        /// Gets the method used to draw the background of a ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteRibbonBackStyle value.</returns>
        PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state);

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state);

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state);

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state);

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state);

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state);
        #endregion

        #region RibbonText
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="style">Text style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state);
        #endregion

        #region ElementColor
        /// <summary>
        /// Gets the first element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor1(PaletteElement element, PaletteState state);

        /// <summary>
        /// Gets the second element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor2(PaletteElement element, PaletteState state);

        /// <summary>
        /// Gets the third element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor3(PaletteElement element, PaletteState state);

        /// <summary>
        /// Gets the fourth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor4(PaletteElement element, PaletteState state);

        /// <summary>
        /// Gets the fifth element color.
        /// </summary>
        /// <param name="element">Element for which color is required.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor5(PaletteElement element, PaletteState state);
        #endregion

        #region DragDrop
        /// <summary>
        /// Gets the feedback drawing method used.
        /// </summary>
        /// <returns>Feedback enumeration value.</returns>
        PaletteDragFeedback GetDragDropFeedback();

        /// <summary>
        /// Gets the background color for a solid drag drop area.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropSolidBack();

        /// <summary>
        /// Gets the border color for a solid drag drop area.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropSolidBorder();

        /// <summary>
        /// Gets the opacity of the solid area.
        /// </summary>
        /// <returns>Opacity ranging from 0 to 1.</returns>
        float GetDragDropSolidOpacity();

        /// <summary>
        /// Gets the background color for the docking indicators area.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropDockBack();

        /// <summary>
        /// Gets the border color for the docking indicators area.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropDockBorder();

        /// <summary>
        /// Gets the active color for docking indicators.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropDockActive();

        /// <summary>
        /// Gets the inactive color for docking indicators.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropDockInactive();
        #endregion

        #region ColorTable
        /// <summary>
        /// Gets access to the color table instance.
        /// </summary>
        KryptonColorTable ColorTable { get; }
        #endregion
    }
	#endregion

    #region IPaletteBack
    /// <summary>
    /// Exposes a palette source for drawing a background.
    /// </summary>
    public interface IPaletteBack
    {
        /// <summary>
        /// Gets a value indicating if background should be drawn.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        InheritBool GetBackDraw(PaletteState state);

        /// <summary>
        /// Gets the graphics drawing hint.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        PaletteGraphicsHint GetBackGraphicsHint(PaletteState state);

        /// <summary>
        /// Gets the first back color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetBackColor1(PaletteState state);

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetBackColor2(PaletteState state);

        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        PaletteColorStyle GetBackColorStyle(PaletteState state);

        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        PaletteRectangleAlign GetBackColorAlign(PaletteState state);

        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        float GetBackColorAngle(PaletteState state);

        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        Image GetBackImage(PaletteState state);

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        PaletteImageStyle GetBackImageStyle(PaletteState state);

        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        PaletteRectangleAlign GetBackImageAlign(PaletteState state);
    }
    #endregion

    #region IPaletteBorder
    /// <summary>
    /// Exposes a palette source for drawing a border.
    /// </summary>
    public interface IPaletteBorder
    {
        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        InheritBool GetBorderDraw(PaletteState state);

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        PaletteDrawBorders GetBorderDrawBorders(PaletteState state);

        /// <summary>
        /// Gets the graphics drawing hint.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state);

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetBorderColor1(PaletteState state);

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetBorderColor2(PaletteState state);

        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        PaletteColorStyle GetBorderColorStyle(PaletteState state);

        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        PaletteRectangleAlign GetBorderColorAlign(PaletteState state);

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        float GetBorderColorAngle(PaletteState state);

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer width.</returns>
        int GetBorderWidth(PaletteState state);

        /// <summary>
        /// Gets the border corner rounding.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer rounding.</returns>
        int GetBorderRounding(PaletteState state);

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        Image GetBorderImage(PaletteState state);

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        PaletteImageStyle GetBorderImageStyle(PaletteState state);

        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        PaletteRectangleAlign GetBorderImageAlign(PaletteState state);
    }
    #endregion

    #region IPaletteContent
    /// <summary>
    /// Exposes a palette source for drawing content.
    /// </summary>
    public interface IPaletteContent
    {
        /// <summary>
        /// Gets a value indicating if content should be drawn.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        InheritBool GetContentDraw(PaletteState state);

        /// <summary>
        /// Gets a value indicating if content should be drawn with focus indication.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        InheritBool GetContentDrawFocus(PaletteState state);

        /// <summary>
        /// Gets the horizontal relative alignment of the image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        PaletteRelativeAlign GetContentImageH(PaletteState state);

        /// <summary>
        /// Gets the vertical relative alignment of the image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        PaletteRelativeAlign GetContentImageV(PaletteState state);

        /// <summary>
        /// Gets the effect applied to drawing of the image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteImageEffect value.</returns>
        PaletteImageEffect GetContentImageEffect(PaletteState state);

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentImageColorMap(PaletteState state);

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentImageColorTo(PaletteState state);

        /// <summary>
        /// Gets the font for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetContentShortTextFont(PaletteState state);

        /// <summary>
        /// Gets the font for the short text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetContentShortTextNewFont(PaletteState state);

        /// <summary>
        /// Gets the rendering hint for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        PaletteTextHint GetContentShortTextHint(PaletteState state);

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state);

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        InheritBool GetContentShortTextMultiLine(PaletteState state);

        /// <summary>
        /// Gets the text trimming to use for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        PaletteTextTrim GetContentShortTextTrim(PaletteState state);

        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        PaletteRelativeAlign GetContentShortTextH(PaletteState state);

        /// <summary>
        /// Gets the vertical relative alignment of the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        PaletteRelativeAlign GetContentShortTextV(PaletteState state);

        /// <summary>
        /// Gets the horizontal relative alignment of multiline short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state);

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentShortTextColor1(PaletteState state);

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentShortTextColor2(PaletteState state);

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        PaletteColorStyle GetContentShortTextColorStyle(PaletteState state);

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state);

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        float GetContentShortTextColorAngle(PaletteState state);

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        Image GetContentShortTextImage(PaletteState state);

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        PaletteImageStyle GetContentShortTextImageStyle(PaletteState state);

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state);

        /// <summary>
        /// Gets the font for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetContentLongTextFont(PaletteState state);

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetContentLongTextNewFont(PaletteState state);

        /// <summary>
        /// Gets the rendering hint for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        PaletteTextHint GetContentLongTextHint(PaletteState state);

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        InheritBool GetContentLongTextMultiLine(PaletteState state);

        /// <summary>
        /// Gets the text trimming to use for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        PaletteTextTrim GetContentLongTextTrim(PaletteState state);

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state);

        /// <summary>
        /// Gets the horizontal relative alignment of the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        PaletteRelativeAlign GetContentLongTextH(PaletteState state);

        /// <summary>
        /// Gets the vertical relative alignment of the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        PaletteRelativeAlign GetContentLongTextV(PaletteState state);

        /// <summary>
        /// Gets the horizontal relative alignment of multiline long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state);

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentLongTextColor1(PaletteState state);

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetContentLongTextColor2(PaletteState state);

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        PaletteColorStyle GetContentLongTextColorStyle(PaletteState state);

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state);

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        float GetContentLongTextColorAngle(PaletteState state);

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        Image GetContentLongTextImage(PaletteState state);

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        PaletteImageStyle GetContentLongTextImageStyle(PaletteState state);

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state);

        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Padding value.</returns>
        Padding GetContentPadding(PaletteState state);

        /// <summary>
        /// Gets the padding between adjacent content items.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer value.</returns>
        int GetContentAdjacentGap(PaletteState state);

        /// <summary>
        /// Gets the style appropriate for this content.
        /// </summary>
        /// <returns>Content style.</returns>
        PaletteContentStyle GetContentStyle();
    }
    #endregion

    #region IPaletteMetric
    /// <summary>
    /// Exposes a palette source for acquiring metrics.
    /// </summary>
    public interface IPaletteMetric
    {
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        int GetMetricInt(PaletteState state, PaletteMetricInt metric);

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric);

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric);
    }
    #endregion

    #region IPaletteDouble
    /// <summary>
    /// Access to the double of back and border palettes.
    /// </summary>
    public interface IPaletteDouble
    {
        /// <summary>
        /// Gets the background palette.
        /// </summary>
        IPaletteBack PaletteBack { get; }

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        IPaletteBorder PaletteBorder { get; }
    }
    #endregion

    #region IPaletteSeparator
    /// <summary>
    /// Access to the back and border palettes plus metrics.
    /// </summary>
    public interface IPaletteSeparator : IPaletteDouble
    {
        /// <summary>
        /// Gets the palette source for acquiring metrics.
        /// </summary>
        IPaletteMetric PaletteMetric { get; }
    }
    #endregion

    #region IPaletteTriple
    /// <summary>
    /// Access to the triple of back, border and content palettes.
    /// </summary>
    public interface IPaletteTriple
    {
        /// <summary>
        /// Gets the background palette.
        /// </summary>
        IPaletteBack PaletteBack { get; }

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        IPaletteBorder PaletteBorder { get; }

        /// <summary>
        /// Gets the content palette.
        /// </summary>
        IPaletteContent PaletteContent { get; }
    }
    #endregion

    #region IPaletteButtonSpec
    /// <summary>
    /// Exposes a palette source button specifications.
    /// </summary>
    public interface IPaletteButtonSpec
    {
        /// <summary>
        /// Gets the icon to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Icon value.</returns>
        Icon GetButtonSpecIcon(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the image to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <param name="state">State for which image is required.</param>
        /// <returns>Image value.</returns>
        Image GetButtonSpecImage(PaletteButtonSpecStyle style, PaletteState state);

        /// <summary>
        /// Gets the image transparent color.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        Color GetButtonSpecImageTransparentColor(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the short text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        string GetButtonSpecShortText(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the long text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        string GetButtonSpecLongText(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the tooltip title text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the color to remap from the image to the container foreground.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        Color GetButtonSpecColorMap(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the button style used for drawing the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonStyle value.</returns>
        PaletteButtonStyle GetButtonSpecStyle(PaletteButtonSpecStyle style);

        /// <summary>
        /// Get the location for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>HeaderLocation value.</returns>
        HeaderLocation GetButtonSpecLocation(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the edge to positon the button against.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteRelativeEdgeAlign value.</returns>
        PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style);

        /// <summary>
        /// Gets the button orientation.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>PaletteButtonOrientation value.</returns>
        PaletteButtonOrientation GetButtonSpecOrientation(PaletteButtonSpecStyle style);
    }
    #endregion

    #region IPaletteRibbonGeneral
    /// <summary>
    /// Exposes a palette source for general ribbon specifications.
    /// </summary>
    public interface IPaletteRibbonGeneral
    {
        /// <summary>
        /// Gets the ribbon shape that should be used.
        /// </summary>
        /// <returns>Ribbon shape value.</returns>
        PaletteRibbonShape GetRibbonShape();

        /// <summary>
        /// Gets the text alignment for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        PaletteRelativeAlign GetRibbonContextTextAlign(PaletteState state);

        /// <summary>
        /// Gets the font for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetRibbonContextTextFont(PaletteState state);

        /// <summary>
        /// Gets the color for the ribbon context text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Color GetRibbonContextTextColor(PaletteState state);

        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonDisabledDark(PaletteState state);

        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonDisabledLight(PaletteState state);

        /// <summary>
        /// Gets the color for the group dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonGroupDialogDark(PaletteState state);

        /// <summary>
        /// Gets the color for the group dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonGroupDialogLight(PaletteState state);

        /// <summary>
        /// Gets the color for the drop arrow dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonDropArrowDark(PaletteState state);

        /// <summary>
        /// Gets the color for the drop arrow light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonDropArrowLight(PaletteState state);

        /// <summary>
        /// Gets the color for the group separator dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonGroupSeparatorDark(PaletteState state);

        /// <summary>
        /// Gets the color for the group separator light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonGroupSeparatorLight(PaletteState state);

        /// <summary>
        /// Gets the color for the minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonMinimizeBarDark(PaletteState state);

        /// <summary>
        /// Gets the color for the minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonMinimizeBarLight(PaletteState state);

        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonTabSeparatorColor(PaletteState state);

        /// <summary>
        /// Gets the color for the tab context separators.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonTabSeparatorContextColor(PaletteState state);

        /// <summary>
        /// Gets the font for the ribbon text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        Font GetRibbonTextFont(PaletteState state);

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        PaletteTextHint GetRibbonTextHint(PaletteState state);

        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonQATButtonDark(PaletteState state);

        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonQATButtonLight(PaletteState state);
    }
    #endregion

    #region IPaletteRibbonBack
    /// <summary>
    /// Exposes a palette source for ribbon background specifications.
    /// </summary>
    public interface IPaletteRibbonBack
    {
        /// <summary>
        /// Gets the method used to draw the background of a ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteRibbonBackStyle value.</returns>
        PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state);

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor1(PaletteState state);

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor2(PaletteState state);

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor3(PaletteState state);

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor4(PaletteState state);

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonBackColor5(PaletteState state);
    }
    #endregion

    #region IPaletteRibbonText
    /// <summary>
    /// Exposes a palette source for ribbon text specifications.
    /// </summary>
    public interface IPaletteRibbonText
    {
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetRibbonTextColor(PaletteState state);
    }
    #endregion

    #region IPaletteDragDrop
    /// <summary>
    /// Access to drag the drop settings.
    /// </summary>
    public interface IPaletteDragDrop
    {
        /// <summary>
        /// Gets the background color for a solid drag drop area.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropSolidBack();

        /// <summary>
        /// Gets the border color for a solid drag drop area.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropSolidBorder();

        /// <summary>
        /// Gets the opacity of the solid area.
        /// </summary>
        /// <returns>Opacity ranging from 0 to 1.</returns>
        float GetDragDropSolidOpacity();

        /// <summary>
        /// Gets the background color for the docking indicators area.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropDockBack();

        /// <summary>
        /// Gets the border color for the docking indicators area.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropDockBorder();

        /// <summary>
        /// Gets the active color for docking indicators.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropDockActive();

        /// <summary>
        /// Gets the inactive color for docking indicators.
        /// </summary>
        /// <returns>Color value.</returns>
        Color GetDragDropDockInactive();
    }
    #endregion

    #region IPaletteElementColor
    /// <summary>
    /// Exposes a palette source for element colors.
    /// </summary>
    public interface IPaletteElementColor
    {
        /// <summary>
        /// Gets the first color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor1(PaletteState state);

        /// <summary>
        /// Gets the second color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor2(PaletteState state);

        /// <summary>
        /// Gets the third color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor3(PaletteState state);

        /// <summary>
        /// Gets the fourth color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor4(PaletteState state);

        /// <summary>
        /// Gets the fifth color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        Color GetElementColor5(PaletteState state);
    }
    #endregion

	#region Enum InheritBool
	/// <summary>
	/// Specifies a boolean that can inherit its value.
	/// </summary>
	public enum InheritBool
	{
		/// <summary>
		/// Specifies the value should be inherited.
		/// </summary>
		Inherit,

		/// <summary>
		/// Specifies a boolean true.
		/// </summary>
		True,

		/// <summary>
		/// Specifies a boolean false.
		/// </summary>
		False
	}
	#endregion

	#region Enum PaletteMode
	/// <summary>
	/// Specifies the palette applied when drawing.
	/// </summary>
    [TypeConverter(typeof(PaletteModeConverter))]
    public enum PaletteMode
	{
        /// <summary>
        /// Specifies the renderer defined by the KryptonManager be used.
        /// </summary>
        Global,
        
        /// <summary>
        /// Specifies a professional appearance based on system settings.
		/// </summary>
		ProfessionalSystem,

        /// <summary>
        /// Specifies a professional appearance with a preference to use theme colors.
        /// </summary>
        ProfessionalOffice2003,

        /// <summary>
        /// Specifies the Blue color variant of the Office 2007 appearance.
        /// </summary>
        Office2007Blue,

        /// <summary>
        /// Specifies the Silver color variant of the Office 2007 appearance.
        /// </summary>
        Office2007Silver,

        /// <summary>
        /// Specifies the Black color variant of the Office 2007 appearance.
        /// </summary>
        Office2007Black,

        /// <summary>
        /// Specifies the Blue color variant of the Office 2010 appearance.
        /// </summary>
        Office2010Blue,

        /// <summary>
        /// Specifies the Silver color variant of the Office 2010 appearance.
        /// </summary>
        Office2010Silver,

        /// <summary>
        /// Specifies the Black color variant of the Office 2010 appearance.
        /// </summary>
        Office2010Black,

        /// <summary>
        /// Specifies the Blue color variant on the Sparkle palette theme.
        /// </summary>
        SparkleBlue,

        /// <summary>
        /// Specifies the Orange color variant on the Sparkle palette theme.
        /// </summary>
        SparkleOrange,

        /// <summary>
        /// Specifies the Purple color variant on the Sparkle palette theme.
        /// </summary>
        SparklePurple,

        /// <summary>
		/// Specifies a custom palette be used.
		/// </summary>
		Custom
	}

    /// <summary>
    /// Specifies the palette requested at the global level.
    /// </summary>
    [TypeConverter(typeof(PaletteModeManagerConverter))]
    public enum PaletteModeManager
    {
        /// <summary>
        /// Specifies a professional appearance based on system settings.
        /// </summary>
        ProfessionalSystem,

        /// <summary>
        /// Specifies a professional appearance with a preference to use theme colors.
        /// </summary>
        ProfessionalOffice2003,

        /// <summary>
        /// Specifies the Blue color variant of the Office 2007 appearance.
        /// </summary>
        Office2007Blue,

        /// <summary>
        /// Specifies the Silver color variant of the Office 2007 appearance.
        /// </summary>
        Office2007Silver,

        /// <summary>
        /// Specifies the Black color variant of the Office 2007 appearance.
        /// </summary>
        Office2007Black,

        /// <summary>
        /// Specifies the Blue color variant of the Office 2010 appearance.
        /// </summary>
        Office2010Blue,

        /// <summary>
        /// Specifies the Silver color variant of the Office 2010 appearance.
        /// </summary>
        Office2010Silver,

        /// <summary>
        /// Specifies the Black color variant of the Office 2010 appearance.
        /// </summary>
        Office2010Black,

        /// <summary>
        /// Specifies the Blue color variant on the Sparkle palette theme.
        /// </summary>
        SparkleBlue,

        /// <summary>
        /// Specifies the Orange color variant on the Sparkle palette theme.
        /// </summary>
        SparkleOrange,

        /// <summary>
        /// Specifies the Purple color variant on the Sparkle palette theme.
        /// </summary>
        SparklePurple,

        /// <summary>
        /// Specifies a custom palette be used.
        /// </summary>
        Custom
    }
    #endregion

	#region Enum PaletteState
	/// <summary>
	/// Specifies the state of the element.
	/// </summary>
    [Flags()]
	public enum PaletteState
	{
        /// <summary>
		/// Specifies the element is in the disabled state.
		/// </summary>
		Disabled = 0x000001,

        /// <summary>
        /// Specifies the element is in the normal state.
        /// </summary>
        Normal = 0x000002,

        /// <summary>
        /// Specifies the element is in the hot tracking state.
        /// </summary>
        Tracking = 0x000004,

        /// <summary>
        /// Specifies the element is in the pressed state.
        /// </summary>
        Pressed = 0x000008,

        /// <summary>
        /// Specifies the bit that is set for all checked states.
        /// </summary>
        Checked = 0x001000,

        /// <summary>
        /// Specifies the element is in the normal state.
        /// </summary>
        CheckedNormal = 0x001002,

        /// <summary>
        /// Specifies the checked element is in the hot tracking state.
        /// </summary>
        CheckedTracking = 0x001004,

        /// <summary>
        /// Specifies the checked element is in the pressed state.
        /// </summary>
        CheckedPressed = 0x001008,

        /// <summary>
        /// Specifies the bit that is set for all context states.
        /// </summary>
        Context = 0x002000,

        /// <summary>
        /// Specifies the context element is in the normal state.
        /// </summary>
        ContextNormal = 0x002002,

        /// <summary>
        /// Specifies the context element is in the hot tracking state.
        /// </summary>
        ContextTracking = 0x002004,

        /// <summary>
        /// Specifies the context element is in the hot tracking state.
        /// </summary>
        ContextPressed = 0x002008,

        /// <summary>
        /// Specifies the context element is in the check normal state.
        /// </summary>
        ContextCheckedNormal = 0x002010,

        /// <summary>
        /// Specifies the context element is in the check tracking state.
        /// </summary>
        ContextCheckedTracking = 0x002020,

        /// <summary>
        /// Specifies the bit that is set for all override states.
        /// </summary>
        Override = 0x100000,

        /// <summary>
        /// Specifies values to override when in any state but with the focus.
        /// </summary>
        FocusOverride = 0x100001,

        /// <summary>
		/// Specifies values to override when in normal state but the default element.
		/// </summary>
        NormalDefaultOverride = 0x100002,

        /// <summary>
        /// Specifies values to override when a link has been visited.
        /// </summary>
        LinkVisitedOverride = 0x100004,

        /// <summary>
        /// Specifies values to override when a link has not been visited.
        /// </summary>
        LinkNotVisitedOverride = 0x100008,

        /// <summary>
        /// Specifies values to override when a link is pressed.
        /// </summary>
        LinkPressedOverride = 0x100010,

        /// <summary>
        /// Specifies values to override bolded dates in calendars.
        /// </summary>
        BoldedOverride = 0x100020,

        /// <summary>
        /// Specifies values to override today date in calendars.
        /// </summary>
        TodayOverride = 0x100040
    }
	#endregion

    #region Enum PaletteMetricInt
    /// <summary>
    /// Specifies a integer type metric.
    /// </summary>
    public enum PaletteMetricInt
    {
        /// <summary>
        /// Specifies that no integer metric is required.
        /// </summary>
        None,

        /// <summary>
        /// Specifies how far to inset a button on a primary header.
        /// </summary>
        HeaderButtonEdgeInsetPrimary,

        /// <summary>
        /// Specifies how far to inset a button on a secondary header.
        /// </summary>
        HeaderButtonEdgeInsetSecondary,

        /// <summary>
        /// Specifies how far to inset a button on an inactive dock header.
        /// </summary>
        HeaderButtonEdgeInsetDockInactive,

        /// <summary>
        /// Specifies how far to inset a button on an active dock header.
        /// </summary>
        HeaderButtonEdgeInsetDockActive,

        /// <summary>
        /// Specifies how far to inset a button on a main form header.
        /// </summary>
        HeaderButtonEdgeInsetForm,

        /// <summary>
        /// Specifies how far to inset a button on a calendar header.
        /// </summary>
        HeaderButtonEdgeInsetCalendar,

        /// <summary>
        /// Specifies how far to inset a button on a input control.
        /// </summary>
        HeaderButtonEdgeInsetInputControl,

        /// <summary>
        /// Specifies how far to inset a button on a custom1 header.
        /// </summary>
        HeaderButtonEdgeInsetCustom1,

        /// <summary>
        /// Specifies how far to inset a button on a custom2 header.
        /// </summary>
        HeaderButtonEdgeInsetCustom2,

        /// <summary>
        /// Specifies the padding from buttons to the outside control edge.
        /// </summary>
        BarButtonEdgeOutside,

        /// <summary>
        /// Specifies the padding for buttons to the bar.
        /// </summary>
        BarButtonEdgeInside,

        /// <summary>
        /// Specifies the padding from buttons to the page header content.
        /// </summary>
        PageButtonInset,

        /// <summary>
        /// Specifies the spacing gap been each check button.
        /// </summary>
        CheckButtonGap,

        /// <summary>
        /// Specifies the spacing gap been each ribbon tab.
        /// </summary>
        RibbonTabGap,
    }
    #endregion

	#region Enum PaletteMetricBool
	/// <summary>
	/// Specifies a bool type metric.
	/// </summary>
	public enum PaletteMetricBool
	{
        /// <summary>
        /// Specifies that no bool metric is required.
        /// </summary>
        None,
        
        /// <summary>
		/// Specifies when the border is drawn for the header group control.
		/// </summary>
		HeaderGroupOverlay,

        /// <summary>
		/// Specifies that split area controls use faded appearance for non-active area.
		/// </summary>
		SplitWithFading,

        /// <summary>
        /// Specifies that the spare tabs area be treated as the application caption area.
        /// </summary>
        RibbonTabsSpareCaption,

        /// <summary>
        /// Specifies if lines are drawn between nodes in the KryptonTreeView.
        /// </summary>
        TreeViewLines
    }
	#endregion

	#region Enum PaletteMetricPadding
	/// <summary>
	/// Specifies a padding type metric.
	/// </summary>
	public enum PaletteMetricPadding
	{
        /// <summary>
        /// Specifies that no padding metric is required.
        /// </summary>
        None,
        
        /// <summary>
        /// Specifies the padding for the primary header inside a header group.
		/// </summary>
		HeaderGroupPaddingPrimary,

		/// <summary>
		/// Specifies the padding for the second header inside a header group.
		/// </summary>
		HeaderGroupPaddingSecondary,

        /// <summary>
        /// Specifies the padding for the inactive dock header inside a header group.
        /// </summary>
        HeaderGroupPaddingDockInactive,

        /// <summary>
        /// Specifies the padding for the active dock header inside a header group.
        /// </summary>
        HeaderGroupPaddingDockActive,

        /// <summary>
        /// Specifies the padding for buttons on a ribbon.
        /// </summary>
        RibbonButtonPadding,

        /// <summary>
        /// Specifies the padding for buttons on a primary header.
        /// </summary>
        HeaderButtonPaddingPrimary,

        /// <summary>
        /// Specifies the padding for buttons on a secondary header.
        /// </summary>
        HeaderButtonPaddingSecondary,

        /// <summary>
        /// Specifies the padding for the dock inside an inactive header group.
        /// </summary>
        HeaderButtonPaddingDockInactive,

        /// <summary>
        /// Specifies the padding for the dock inside an active header group.
        /// </summary>
        HeaderButtonPaddingDockActive,

        /// <summary>
        /// Specifies the padding for buttons on a main form header.
        /// </summary>
        HeaderButtonPaddingForm,

        /// <summary>
        /// Specifies the padding for buttons on a calendar header.
        /// </summary>
        HeaderButtonPaddingCalendar,

        /// <summary>
        /// Specifies the padding for buttons on a input control.
        /// </summary>
        HeaderButtonPaddingInputControl,

        /// <summary>
        /// Specifies the padding for buttons on a custom1 header.
        /// </summary>
        HeaderButtonPaddingCustom1,

        /// <summary>
        /// Specifies the padding for buttons on a custom1 header.
        /// </summary>
        HeaderButtonPaddingCustom2,

        /// <summary>
        /// Specifies the padding for the navigator bar when showing tabs.
        /// </summary>
        BarPaddingTabs,

        /// <summary>
        /// Specifies the padding for the navigator bar when inside.
        /// </summary>
        BarPaddingInside,

        /// <summary>
        /// Specifies the padding for the navigator bar when outside.
        /// </summary>
        BarPaddingOutside,

        /// <summary>
        /// Specifies the padding for the navigator bar when on its own.
        /// </summary>
        BarPaddingOnly,

        /// <summary>
        /// Specifies the padding for buttons on a navigator bar.
        /// </summary>
        BarButtonPadding,

        /// <summary>
        /// Specifies the padding for buttons on a navigator page header.
        /// </summary>
        PageButtonPadding,
        
        /// <summary>
		/// Specifies the padding for the low profile separator.
		/// </summary>
		SeparatorPaddingLowProfile,

        /// <summary>
        /// Specifies the padding for the high profile separator.
        /// </summary>
        SeparatorPaddingHighProfile,

        /// <summary>
        /// Specifies the padding for the high profile for internal separator.
        /// </summary>
        SeparatorPaddingHighInternalProfile,

        /// <summary>
        /// Specifies the padding for the first custom separator.
        /// </summary>
        SeparatorPaddingCustom1,

        /// <summary>
        /// Specifies the padding outside of each context menu item highlight.
        /// </summary>
        ContextMenuItemHighlight,

        /// <summary>
        /// Specifies the padding outside of each context menu items collection.
        /// </summary>
        ContextMenuItemsCollection,

        /// <summary>
        /// Specifies the padding inside of context menu outside.
        /// </summary>
        ContextMenuItemOuter,

        /// <summary>
        /// Specifies the padding outside each application button spec.
        /// </summary>
        RibbonAppButton
    }
	#endregion

    #region Enum PaletteButtonStyle
    /// <summary>
    /// Specifies the button style.
    /// </summary>
    [TypeConverter(typeof(PaletteButtonStyleConverter))]
    public enum PaletteButtonStyle
    {
        /// <summary>
        /// Specifies button style should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies a standalone button style.
        /// </summary>
        Standalone,

        /// <summary>
        /// Specifies an alternative standalone button style.
        /// </summary>
        Alternate,

        /// <summary>
        /// Specifies a low profile button style.
        /// </summary>
        LowProfile,

        /// <summary>
        /// Specifies a button spec usage style.
        /// </summary>
        ButtonSpec,

        /// <summary>
        /// Specifies a bread crumb usage style.
        /// </summary>
        BreadCrumb,

        /// <summary>
        /// Specifies a ribbon cluster button usage style.
        /// </summary>
        Cluster,

        /// <summary>
        /// Specifies a navigator stack usage style.
        /// </summary>
        NavigatorStack,

        /// <summary>
        /// Specifies a navigator outlook overflow usage style.
        /// </summary>
        NavigatorOverflow,

        /// <summary>
        /// Specifies a navigator mini usage style.
        /// </summary>
        NavigatorMini,

        /// <summary>
        /// Specifies an input control usage style.
        /// </summary>
        InputControl,

        /// <summary>
        /// Specifies a list item usage style.
        /// </summary>
        ListItem,

        /// <summary>
        /// Specifies a form level button style.
        /// </summary>
        Form,

        /// <summary>
        /// Specifies a form close level button style.
        /// </summary>
        FormClose,

        /// <summary>
        /// Specifies a command button style.
        /// </summary>
        Command,

        /// <summary>
        /// Specifies the first custom button style.
        /// </summary>
        Custom1,

        /// <summary>
        /// Specifies the second custom button style.
        /// </summary>
        Custom2,

        /// <summary>
        /// Specifies the third custom button style.
        /// </summary>
        Custom3
    }
    #endregion

	#region Enum PaletteBackStyle
	/// <summary>
	/// Specifies the style of background.
	/// </summary>
    [TypeConverter(typeof(PaletteBackStyleConverter))]
    public enum PaletteBackStyle
	{
        /// <summary>
        /// Specifies a background style appropriate for a standalone button style.
		/// </summary>
		ButtonStandalone,

        /// <summary>
        /// Specifies a background style appropriate for an alternate standalone button style.
        /// </summary>
        ButtonAlternate,
        
        /// <summary>
        /// Specifies a background style appropriate for a low profile button style.
		/// </summary>
		ButtonLowProfile,

        /// <summary>
        /// Specifies a background style appropriate for a button spec.
        /// </summary>
        ButtonButtonSpec,

        /// <summary>
        /// Specifies a background style appropriate for a bread crumb.
        /// </summary>
        ButtonBreadCrumb,

        /// <summary>
        /// Specifies a background style appropriate for a calendar day.
        /// </summary>
        ButtonCalendarDay,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon cluster button.
        /// </summary>
        ButtonCluster,

        /// <summary>
        /// Specifies a background style appropriate for a gallery button style.
        /// </summary>
        ButtonGallery,

        /// <summary>
        /// Specifies a background style appropriate for a navigator stack.
        /// </summary>
        ButtonNavigatorStack,

        /// <summary>
        /// Specifies a background style appropriate for a navigator overflow button.
        /// </summary>
        ButtonNavigatorOverflow,

        /// <summary>
        /// Specifies a background style appropriate for a navigator mini button.
        /// </summary>
        ButtonNavigatorMini,

        /// <summary>
        /// Specifies a background style appropriate for an input control button.
        /// </summary>
        ButtonInputControl,

        /// <summary>
        /// Specifies a background style appropriate for a list item button.
        /// </summary>
        ButtonListItem,

        /// <summary>
        /// Specifies a background style appropriate for a form level button.
        /// </summary>
        ButtonForm,

        /// <summary>
        /// Specifies a background style appropriate for a form level close button.
        /// </summary>
        ButtonFormClose,

        /// <summary>
        /// Specifies a background style appropriate for a command button.
        /// </summary>
        ButtonCommand,

        /// <summary>
        /// Specifies a background style appropriate for the first custom button style.
        /// </summary>
        ButtonCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the second custom button style.
        /// </summary>
        ButtonCustom2,

        /// <summary>
        /// Specifies a background style appropriate for the third custom button style.
        /// </summary>
        ButtonCustom3,
        
        /// <summary>
		/// Specifies a background style appropriate for a client control style.
		/// </summary>
		ControlClient,

		/// <summary>
        /// Specifies a background style appropriate for an alternate control style.
		/// </summary>
		ControlAlternate,

        /// <summary>
        /// Specifies a background style appropriate for a group box control style.
        /// </summary>
        ControlGroupBox,

        /// <summary>
        /// Specifies a background style appropriate for a tool tip popup.
        /// </summary>
        ControlToolTip,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon style control.
        /// </summary>
        ControlRibbon,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon application button menu control.
        /// </summary>
        ControlRibbonAppMenu,

        /// <summary>
        /// Specifies a background style appropriate for the first custom control style.
        /// </summary>
        ControlCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the outer part of a context menu control.
        /// </summary>
        ContextMenuOuter,

        /// <summary>
        /// Specifies a background style appropriate for the inner part of a context menu control.
        /// </summary>
        ContextMenuInner,

        /// <summary>
        /// Specifies a background style appropriate for a context menu heading.
        /// </summary>
        ContextMenuHeading,

        /// <summary>
        /// Specifies a background style appropriate for a context menu separator.
        /// </summary>
        ContextMenuSeparator,

        /// <summary>
        /// Specifies a background style appropriate for a context menu image.
        /// </summary>
        ContextMenuItemImage,

        /// <summary>
        /// Specifies a background style appropriate for the vertical split of a context menu item.
        /// </summary>
        ContextMenuItemSplit,

        /// <summary>
        /// Specifies a background style appropriate for a context menu image column.
        /// </summary>
        ContextMenuItemImageColumn,

        /// <summary>
        /// Specifies a background style appropriate for a context menu highlight column.
        /// </summary>
        ContextMenuItemHighlight,

        /// <summary>
        /// Specifies a background style appropriate for a standalone input control.
        /// </summary>
        InputControlStandalone,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon style input control.
        /// </summary>
        InputControlRibbon,

        /// <summary>
        /// Specifies a background style appropriate for the first custom input control style.
        /// </summary>
        InputControlCustom1,

        /// <summary>
        /// Specifies a background style appropriate for column headers in a list style grid.
        /// </summary>
        GridHeaderColumnList,

        /// <summary>
        /// Specifies a background style appropriate for row headers in a list style grid.
        /// </summary>
        GridHeaderRowList,

        /// <summary>
        /// Specifies a background style appropriate for data cells in a list style grid.
        /// </summary>
        GridDataCellList,

        /// <summary>
        /// Specifies a background style appropriate for blank areas in a list style grid.
        /// </summary>
        GridBackgroundList,

        /// <summary>
        /// Specifies a background style appropriate for column headers in a sheet style grid.
        /// </summary>
        GridHeaderColumnSheet,

        /// <summary>
        /// Specifies a background style appropriate for row headers in a sheet style grid.
        /// </summary>
        GridHeaderRowSheet,

        /// <summary>
        /// Specifies a background style appropriate for data cells in a sheet style grid.
        /// </summary>
        GridDataCellSheet,

        /// <summary>
        /// Specifies a background style appropriate for blank areas in a sheet style grid.
        /// </summary>
        GridBackgroundSheet,

        /// <summary>
        /// Specifies a background style appropriate for column headers in a custom grid style.
        /// </summary>
        GridHeaderColumnCustom1,

        /// <summary>
        /// Specifies a background style appropriate for row headers in a custom grid style.
        /// </summary>
        GridHeaderRowCustom1,

        /// <summary>
        /// Specifies a background style appropriate for data cells in a custom grid style.
        /// </summary>
        GridDataCellCustom1,

        /// <summary>
        /// Specifies a background style appropriate for blank areas in a custom grid style.
        /// </summary>
        GridBackgroundCustom1,

        /// <summary>
        /// Specifies a background style appropriate for a primary header style.
		/// </summary>
		HeaderPrimary,

		/// <summary>
        /// Specifies a background style appropriate for a secondary header style.
		/// </summary>
		HeaderSecondary,

        /// <summary>
        /// Specifies a background style appropriate for an inactive docking header.
        /// </summary>
        HeaderDockInactive,

        /// <summary>
        /// Specifies a background style appropriate for an active docking header.
        /// </summary>
        HeaderDockActive,

        /// <summary>
        /// Specifies a background style appropriate for a main form header style.
        /// </summary>
        HeaderForm,

        /// <summary>
        /// Specifies a background style appropriate for a calendar title area.
        /// </summary>
        HeaderCalendar,

        /// <summary>
        /// Specifies a background style appropriate for the first custom header style.
        /// </summary>
        HeaderCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the second custom header style.
        /// </summary>
        HeaderCustom2,

        /// <summary>
        /// Specifies a background style appropriate for a client panel style.
        /// </summary>
        PanelClient,

        /// <summary>
        /// Specifies a background style appropriate for an alternate panel style.
        /// </summary>
        PanelAlternate,

        /// <summary>
        /// Specifies a background style appropriate for an inactive ribbon.
        /// </summary>
        PanelRibbonInactive,

        /// <summary>
        /// Specifies a background style appropriate for the first custom panel style.
        /// </summary>
        PanelCustom1,

        /// <summary>
        /// Specifies a background style appropriate for a low profile separator style.
        /// </summary>
        SeparatorLowProfile,

        /// <summary>
        /// Specifies a background style appropriate for a high profile separator style.
        /// </summary>
        SeparatorHighProfile,

        /// <summary>
        /// Specifies a background style appropriate for a high profile for internal separator style.
        /// </summary>
        SeparatorHighInternalProfile,

        /// <summary>
        /// Specifies a background style appropriate for the first custom separator style.
        /// </summary>
        SeparatorCustom1,

        /// <summary>
        /// Specifies a background style appropriate for a high profile tab.
        /// </summary>
        TabHighProfile,

        /// <summary>
        /// Specifies a background style appropriate for a standard profile tab.
        /// </summary>
        TabStandardProfile,

        /// <summary>
        /// Specifies a background style appropriate for a low profile tab.
        /// </summary>
        TabLowProfile,

        /// <summary>
        /// Specifies a background style appropriate for a OneNote tab.
        /// </summary>
        TabOneNote,

        /// <summary>
        /// Specifies a background style appropriate for a docking tab.
        /// </summary>
        TabDock,

        /// <summary>
        /// Specifies a background style appropriate for a auto hidden docking tab.
        /// </summary>
        TabDockAutoHidden,

        /// <summary>
        /// Specifies a background style appropriate for the first custom tab style.
        /// </summary>
        TabCustom1,

        /// <summary>
        /// Specifies a background style appropriate for the second custom tab style.
        /// </summary>
        TabCustom2,

        /// <summary>
        /// Specifies a background style appropriate for the third custom tab style.
        /// </summary>
        TabCustom3,

        /// <summary>
        /// Specifies a background style appropriate for a main form.
        /// </summary>
        FormMain,

        /// <summary>
        /// Specifies a background style appropriate for the first custom form style.
        /// </summary>
        FormCustom1,
    }
	#endregion

	#region Enum PaletteBorderStyle
	/// <summary>
	/// Specifies the style of border.
	/// </summary>
    [TypeConverter(typeof(PaletteBorderStyleConverter))]
    public enum PaletteBorderStyle
	{
        /// <summary>
        /// Specifies a border style appropriate for a standalone button style.
        /// </summary>
        ButtonStandalone,

        /// <summary>
        /// Specifies a border style appropriate for an alternate standalone button style.
        /// </summary>
        ButtonAlternate,

        /// <summary>
        /// Specifies a border style appropriate for a low profile button style.
        /// </summary>
        ButtonLowProfile,

        /// <summary>
        /// Specifies a border style appropriate for a button spec style.
        /// </summary>
        ButtonButtonSpec,

        /// <summary>
        /// Specifies a border style appropriate for a bread crumb.
        /// </summary>
        ButtonBreadCrumb,

        /// <summary>
        /// Specifies a border style appropriate for a calendar day.
        /// </summary>
        ButtonCalendarDay,

        /// <summary>
        /// Specifies a border style appropriate for a ribbon cluster button.
        /// </summary>
        ButtonCluster,

        /// <summary>
        /// Specifies a border style appropriate for a gallery button style.
        /// </summary>
        ButtonGallery,

        /// <summary>
        /// Specifies a border style appropriate for a navigator stack.
        /// </summary>
        ButtonNavigatorStack,

        /// <summary>
        /// Specifies a border style appropriate for a navigator overflow button.
        /// </summary>
        ButtonNavigatorOverflow,

        /// <summary>
        /// Specifies a border style appropriate for a navigator mini button.
        /// </summary>
        ButtonNavigatorMini,

        /// <summary>
        /// Specifies a border style appropriate for an input control button.
        /// </summary>
        ButtonInputControl,

        /// <summary>
        /// Specifies a border style appropriate for a list item button.
        /// </summary>
        ButtonListItem,

        /// <summary>
        /// Specifies a border style appropriate for a form level button.
        /// </summary>
        ButtonForm,

        /// <summary>
        /// Specifies a border style appropriate for a form level close button.
        /// </summary>
        ButtonFormClose,

        /// <summary>
        /// Specifies a border style appropriate for a command button.
        /// </summary>
        ButtonCommand,

        /// <summary>
        /// Specifies a border style appropriate for the first custom button style.
        /// </summary>
        ButtonCustom1,

        /// <summary>
        /// Specifies a border style appropriate for the second custom button style.
        /// </summary>
        ButtonCustom2,

        /// <summary>
        /// Specifies a border style appropriate for the third custom button style.
        /// </summary>
        ButtonCustom3,
        
        /// <summary>
        /// Specifies a border style appropriate for a client control style.
		/// </summary>
		ControlClient,

		/// <summary>
        /// Specifies a border style appropriate for an alternate control style.
		/// </summary>
		ControlAlternate,

        /// <summary>
        /// Specifies a border style appropriate for a group box.
        /// </summary>
        ControlGroupBox,

        /// <summary>
        /// Specifies a border style appropriate for an a tool tip popup.
        /// </summary>
        ControlToolTip,

        /// <summary>
        /// Specifies a border style appropriate for a ribbon style control.
        /// </summary>
        ControlRibbon,

        /// <summary>
        /// Specifies a border style appropriate for a ribbon application button menu control.
        /// </summary>
        ControlRibbonAppMenu,

        /// <summary>
        /// Specifies a border style appropriate for the first custom control style.
        /// </summary>
        ControlCustom1,

        /// <summary>
        /// Specifies a border style appropriate for the outer part of a context menu control.
        /// </summary>
        ContextMenuOuter,

        /// <summary>
        /// Specifies a border style appropriate for the inner part of a context menu control.
        /// </summary>
        ContextMenuInner,

        /// <summary>
        /// Specifies a border style appropriate for a context menu heading.
        /// </summary>
        ContextMenuHeading,

        /// <summary>
        /// Specifies a border style appropriate for a context menu separator.
        /// </summary>
        ContextMenuSeparator,

        /// <summary>
        /// Specifies a border style appropriate for a context menu image.
        /// </summary>
        ContextMenuItemImage,

        /// <summary>
        /// Specifies a border style appropriate for the vertical split of a context menu item.
        /// </summary>
        ContextMenuItemSplit,

        /// <summary>
        /// Specifies a border style appropriate for a context menu image column.
        /// </summary>
        ContextMenuItemImageColumn,

        /// <summary>
        /// Specifies a border style appropriate for a context menu highlight column.
        /// </summary>
        ContextMenuItemHighlight,

        /// <summary>
        /// Specifies a border style appropriate for a standalone input control.
        /// </summary>
        InputControlStandalone,

        /// <summary>
        /// Specifies a border style appropriate for a ribbon style input control.
        /// </summary>
        InputControlRibbon,

        /// <summary>
        /// Specifies a border style appropriate for the first custom input control style.
        /// </summary>
        InputControlCustom1,

        /// <summary>
        /// Specifies a border style appropriate for column headers in a list style grid.
        /// </summary>
        GridHeaderColumnList,

        /// <summary>
        /// Specifies a border style appropriate for row headers in a list style grid.
        /// </summary>
        GridHeaderRowList,

        /// <summary>
        /// Specifies a border style appropriate for data cells in a list style grid.
        /// </summary>
        GridDataCellList,

        /// <summary>
        /// Specifies a border style appropriate for column headers in a sheet style grid.
        /// </summary>
        GridHeaderColumnSheet,

        /// <summary>
        /// Specifies a border style appropriate for row headers in a sheet style grid.
        /// </summary>
        GridHeaderRowSheet,

        /// <summary>
        /// Specifies a border style appropriate for data cells in a sheet style grid.
        /// </summary>
        GridDataCellSheet,

        /// <summary>
        /// Specifies a border style appropriate for column headers in a custom grid style.
        /// </summary>
        GridHeaderColumnCustom1,

        /// <summary>
        /// Specifies a border style appropriate for row headers in a custom grid style.
        /// </summary>
        GridHeaderRowCustom1,

        /// <summary>
        /// Specifies a border style appropriate for data cells in a custom grid style.
        /// </summary>
        GridDataCellCustom1,

        /// <summary>
        /// Specifies a border style appropriate for a primary header style.
        /// </summary>
        HeaderPrimary,

        /// <summary>
        /// Specifies a border style appropriate for a secondary header style.
        /// </summary>
        HeaderSecondary,

        /// <summary>
        /// Specifies a border style appropriate for an inactive docking header.
        /// </summary>
        HeaderDockInactive,

        /// <summary>
        /// Specifies a border style appropriate for an active docking header.
        /// </summary>
        HeaderDockActive,

        /// <summary>
        /// Specifies a border style appropriate for a main form header style.
        /// </summary>
        HeaderForm,

        /// <summary>
        /// Specifies a border style appropriate for a calendar title area.
        /// </summary>
        HeaderCalendar,

        /// <summary>
        /// Specifies a border style appropriate for the first custom header style.
        /// </summary>
        HeaderCustom1,

        /// <summary>
        /// Specifies a border style appropriate for the second custom header style.
        /// </summary>
        HeaderCustom2,

        /// <summary>
        /// Specifies a border style appropriate for a low profile separator style.
        /// </summary>
        SeparatorLowProfile,

        /// <summary>
        /// Specifies a border style appropriate for a high profile separator style.
        /// </summary>
        SeparatorHighProfile,

        /// <summary>
        /// Specifies a border style appropriate for a high profile for internal separator style.
        /// </summary>
        SeparatorHighInternalProfile,

        /// <summary>
        /// Specifies a border style appropriate for the first custom separator style.
        /// </summary>
        SeparatorCustom1,

        /// <summary>
        /// Specifies a border style appropriate for a high profile tab.
        /// </summary>
        TabHighProfile,

        /// <summary>
        /// Specifies a border style appropriate for a standard profile tab.
        /// </summary>
        TabStandardProfile,

        /// <summary>
        /// Specifies a border style appropriate for a low profile tab.
        /// </summary>
        TabLowProfile,

        /// <summary>
        /// Specifies a border style appropriate for a OneNote tab.
        /// </summary>
        TabOneNote,

        /// <summary>
        /// Specifies a border style appropriate for a docking tab.
        /// </summary>
        TabDock,

        /// <summary>
        /// Specifies a border style appropriate for a auto hidden docking tab.
        /// </summary>
        TabDockAutoHidden,
        
        /// <summary>
        /// Specifies a border style appropriate for the first custom tab style.
        /// </summary>
        TabCustom1,

        /// <summary>
        /// Specifies a border style appropriate for the second custom tab style.
        /// </summary>
        TabCustom2,

        /// <summary>
        /// Specifies a border style appropriate for the third custom tab style.
        /// </summary>
        TabCustom3,

        /// <summary>
        /// Specifies a border style appropriate for a main form.
        /// </summary>
        FormMain,

        /// <summary>
        /// Specifies a border style appropriate for the first custom form style.
        /// </summary>
        FormCustom1,
    }
	#endregion

	#region Enum PaletteContentStyle
	/// <summary>
	/// Specifies the style of content.
	/// </summary>
    [TypeConverter(typeof(PaletteContentStyleConverter))]
    public enum PaletteContentStyle
	{
        /// <summary>
        /// Specifies a content style appropriate for a standalone button style.
        /// </summary>
        ButtonStandalone,

        /// <summary>
        /// Specifies a content style appropriate for an alternate standalone button style.
        /// </summary>
        ButtonAlternate,

        /// <summary>
        /// Specifies a content style appropriate for a low profile button style.
        /// </summary>
        ButtonLowProfile,

        /// <summary>
        /// Specifies a content style appropriate for a button spec.
        /// </summary>
        ButtonButtonSpec,

        /// <summary>
        /// Specifies a content style appropriate for a bread crumb.
        /// </summary>
        ButtonBreadCrumb,

        /// <summary>
        /// Specifies a content style appropriate for a calendar day.
        /// </summary>
        ButtonCalendarDay,

        /// <summary>
        /// Specifies a content style appropriate for a ribbon cluster button.
        /// </summary>
        ButtonCluster,

        /// <summary>
        /// Specifies a content style appropriate for a ribbon gallery button.
        /// </summary>
        ButtonGallery,

        /// <summary>
        /// Specifies a content style appropriate for a navigator stack.
        /// </summary>
        ButtonNavigatorStack,

        /// <summary>
        /// Specifies a content style appropriate for a navigator overflow button.
        /// </summary>
        ButtonNavigatorOverflow,

        /// <summary>
        /// Specifies a content style appropriate for a navigator mini button.
        /// </summary>
        ButtonNavigatorMini,

        /// <summary>
        /// Specifies a content style appropriate for an input control button.
        /// </summary>
        ButtonInputControl,

        /// <summary>
        /// Specifies a content style appropriate for a list item button.
        /// </summary>
        ButtonListItem,

        /// <summary>
        /// Specifies a content style appropriate for a form level button.
        /// </summary>
        ButtonForm,

        /// <summary>
        /// Specifies a content style appropriate for a form level close button.
        /// </summary>
        ButtonFormClose,

        /// <summary>
        /// Specifies a content style appropriate for a command button.
        /// </summary>
        ButtonCommand,

        /// <summary>
        /// Specifies a content style appropriate for the first custom button style.
        /// </summary>
        ButtonCustom1,

        /// <summary>
        /// Specifies a content style appropriate for the second custom button style.
        /// </summary>
        ButtonCustom2,

        /// <summary>
        /// Specifies a content style appropriate for the third custom button style.
        /// </summary>
        ButtonCustom3,

        /// <summary>
        /// Specifies a content style appropriate for a context menu heading.
        /// </summary>
        ContextMenuHeading,

        /// <summary>
        /// Specifies a content style appropriate for the image of a context menu item.
        /// </summary>
        ContextMenuItemImage,

        /// <summary>
        /// Specifies a content style appropriate for the text/extra text of a standard context menu item.
        /// </summary>
        ContextMenuItemTextStandard,

        /// <summary>
        /// Specifies a content style appropriate for the text/extra text of a alternate context menu item.
        /// </summary>
        ContextMenuItemTextAlternate,

        /// <summary>
        /// Specifies a content style appropriate for the shortcut text of a context menu item.
        /// </summary>
        ContextMenuItemShortcutText,

        /// <summary>
        /// Specifies a border style appropriate for column headers in a list style grid.
        /// </summary>
        GridHeaderColumnList,

        /// <summary>
        /// Specifies a border style appropriate for column rows in a list style grid.
        /// </summary>
        GridHeaderRowList,

        /// <summary>
        /// Specifies a border style appropriate for data cells in a list style grid.
        /// </summary>
        GridDataCellList,

        /// <summary>
        /// Specifies a border style appropriate for column headers in a sheet style grid.
        /// </summary>
        GridHeaderColumnSheet,

        /// <summary>
        /// Specifies a border style appropriate for column rows in a sheet style grid.
        /// </summary>
        GridHeaderRowSheet,

        /// <summary>
        /// Specifies a border style appropriate for data cells in a sheet style grid.
        /// </summary>
        GridDataCellSheet,

        /// <summary>
        /// Specifies a border style appropriate for column headers in a custom grid style.
        /// </summary>
        GridHeaderColumnCustom1,

        /// <summary>
        /// Specifies a border style appropriate for column rows in a custom grid style.
        /// </summary>
        GridHeaderRowCustom1,

        /// <summary>
        /// Specifies a border style appropriate for data cells in a custom grid style.
        /// </summary>
        GridDataCellCustom1,

        /// <summary>
        /// Specifies a content style appropriate for a primary Header.
        /// </summary>
        HeaderPrimary,

        /// <summary>
        /// Specifies a content style appropriate for a secondary Header.
        /// </summary>
        HeaderSecondary,

        /// <summary>
        /// Specifies a content style appropriate for an inactive docking header.
        /// </summary>
        HeaderDockInactive,

        /// <summary>
        /// Specifies a content style appropriate for an active docking header.
        /// </summary>
        HeaderDockActive,

        /// <summary>
        /// Specifies a content style appropriate for a main form header style.
        /// </summary>
        HeaderForm,

        /// <summary>
        /// Specifies a content style appropriate for a calendar title area.
        /// </summary>
        HeaderCalendar,

        /// <summary>
        /// Specifies a content style appropriate for the first custom header style.
        /// </summary>
        HeaderCustom1,

        /// <summary>
        /// Specifies a content style appropriate for the second custom header style.
        /// </summary>
        HeaderCustom2,

        /// <summary>
        /// Specifies a normal label for use on a control style background.
        /// </summary>
        LabelNormalControl,

        /// <summary>
        /// Specifies a bold label for use on a control style background.
        /// </summary>
        LabelBoldControl,

        /// <summary>
        /// Specifies an italic label for use on a control style background.
        /// </summary>
        LabelItalicControl,
        
        /// <summary>
        /// Specifies a label appropriate for titles for use on a control style background.
        /// </summary>
        LabelTitleControl,

        /// <summary>
        /// Specifies a normal label for use on a panel style background.
        /// </summary>
        LabelNormalPanel,

        /// <summary>
        /// Specifies a bold label for use on a panel style background.
        /// </summary>
        LabelBoldPanel,

        /// <summary>
        /// Specifies a italic label for use on a panel style background.
        /// </summary>
        LabelItalicPanel,

        /// <summary>
        /// Specifies a label appropriate for titles for use on a panel style background.
        /// </summary>
        LabelTitlePanel,

        /// <summary>
        /// Specifies a normal label for use on a group box panel style background.
        /// </summary>
        LabelGroupBoxCaption,

        /// <summary>
        /// Specifies a label style appropriate for a tooltip popup.
        /// </summary>
        LabelToolTip,

        /// <summary>
        /// Specifies a label style appropriate for a super tooltip popup.
        /// </summary>
        LabelSuperTip,

        /// <summary>
        /// Specifies a label style appropriate for a key tooltip popup.
        /// </summary>
        LabelKeyTip,

        /// <summary>
        /// Specifies the first custom label style.
        /// </summary>
        LabelCustom1,

        /// <summary>
        /// Specifies the second custom label style.
        /// </summary>
        LabelCustom2,

        /// <summary>
        /// Specifies the third custom label style.
        /// </summary>
        LabelCustom3,

        /// <summary>
        /// Specifies a content style appropriate for a high profile tab.
        /// </summary>
        TabHighProfile,

        /// <summary>
        /// Specifies a content style appropriate for a standard profile tab.
        /// </summary>
        TabStandardProfile,

        /// <summary>
        /// Specifies a content style appropriate for a low profile tab.
        /// </summary>
        TabLowProfile,

        /// <summary>
        /// Specifies a content style appropriate for a OneNote tab.
        /// </summary>
        TabOneNote,

        /// <summary>
        /// Specifies a content style appropriate for a docking tab.
        /// </summary>
        TabDock,

        /// <summary>
        /// Specifies a content style appropriate for a auto hidden docking tab.
        /// </summary>
        TabDockAutoHidden,

        /// <summary>
        /// Specifies a content style appropriate for the first custom tab style.
        /// </summary>
        TabCustom1,

        /// <summary>
        /// Specifies a content style appropriate for the second custom tab style.
        /// </summary>
        TabCustom2,

        /// <summary>
        /// Specifies a content style appropriate for the third custom tab style.
        /// </summary>
        TabCustom3,

        /// <summary>
        /// Specifies a content style appropriate for a standalone input control.
        /// </summary>
        InputControlStandalone,

        /// <summary>
        /// Specifies a content style appropriate for a ribbon style input control.
        /// </summary>
        InputControlRibbon,

        /// <summary>
        /// Specifies a content style appropriate for the first custom input control style.
        /// </summary>
        InputControlCustom1,
    }
	#endregion

	#region Enum PaletteColorStyle
	/// <summary>
	/// Specifies the color drawing style.
	/// </summary>
	public enum PaletteColorStyle
	{
		/// <summary>
		/// Specifies color should be inherited.
		/// </summary>
		Inherit,

        /// <summary>
        /// Specifies drawing as a series of dashes.
        /// </summary>
        Dashed,
        
        /// <summary>
		/// Specifies solid drawing instead of a gradient.
		/// </summary>
		Solid,

        /// <summary>
        /// Specifies solid block using the first color but with a line of second color one pixel inside.
        /// </summary>
        SolidInside,

        /// <summary>
        /// Specifies solid block using the first color and a single line of second color on right edge.
        /// </summary>
        SolidRightLine,

        /// <summary>
        /// Specifies solid block using the first color and a single line of second color on left edge.
        /// </summary>
        SolidLeftLine,

        /// <summary>
        /// Specifies solid block using the first color and a single line of second color on top edge.
        /// </summary>
        SolidTopLine,

        /// <summary>
        /// Specifies solid block using the first color and a single line of second color on bottom edge.
        /// </summary>
        SolidBottomLine,

        /// <summary>
        /// Specifies solid block using the first color and a rectangle of second color around all edges.
        /// </summary>
        SolidAllLine,

        /// <summary>
        /// Specifies a switch between the first and second colors at 25 percent of distance.
        /// </summary>
        Switch25,

        /// <summary>
        /// Specifies a switch between the first and second colors at 33 percent of distance.
        /// </summary>
        Switch33,

        /// <summary>
        /// Specifies a switch between the first and second colors at 50 percent of distance.
        /// </summary>
        Switch50,

        /// <summary>
        /// Specifies a switch between the first and second colors at 90 percent of distance.
        /// </summary>
        Switch90,

        /// <summary>
        /// Specifies a straight line gradient.
        /// </summary>
        Linear,

        /// <summary>
        /// Specifies the the first 25 percent is color 1 then it linear gradients into color 2.
        /// </summary>
        Linear25,

        /// <summary>
        /// Specifies the the first 33 percent is color 1 then it linear gradients into color 2.
        /// </summary>
        Linear33,

        /// <summary>
        /// Specifies the the first 40 percent is color 1 then it linear gradients into color 2.
        /// </summary>
        Linear40,

        /// <summary>
        /// Specifies the the first 50 percent is color 1 then it linear gradients into color 2.
        /// </summary>
        Linear50,

        /// <summary>
        /// Specifies a straight line gradient with shadow around the inner edge.
        /// </summary>
        LinearShadow,

        /// <summary>
        /// Specifies a rounded gradient by using a non-linear falloff.
        /// </summary>
        Rounded,

        /// <summary>
        /// Specifies a rounded look using a second variant blend of the two colors.
        /// </summary>
        Rounding2,

        /// <summary>
        /// Specifies a rounded look using a third variant blend of the two colors.
        /// </summary>
        Rounding3,

        /// <summary>
        /// Specifies a rounded look using a fourth variant blend of the two colors.
        /// </summary>
        Rounding4,

        /// <summary>
        /// Specifies a rounded look using a fifth variant blend of the two colors.
        /// </summary>
        Rounding5,

        /// <summary>
        /// Specifies a rounded gradient by using a non-linear falloff but with the top edge having light version of Color1.
        /// </summary>
        RoundedTopLight,

        /// <summary>
        /// Specifies a rounded gradient by using a non-linear falloff but with the top and left edges having a white border.
        /// </summary>
        RoundedTopLeftWhite,

        /// <summary>
        /// Specifies a sigma curve that peeks in the center.
        /// </summary>
        Sigma,

		/// <summary>
		/// Specifies a gradient effect in the first and second halfs of the area.
		/// </summary>
		HalfCut,

        /// <summary>
        /// Specifies first color fades into second color mostly within the first quarter of area.
        /// </summary>
        QuarterPhase,

        /// <summary>
        /// Specifies color transition similar to Microsoft OneNote.
        /// </summary>
        OneNote,

        /// <summary>
        /// Specifies a simple glass effect with three edges lighter.
        /// </summary>
        GlassThreeEdge,

        /// <summary>
        /// Specifies a simple glass effect.
        /// </summary>
        GlassSimpleFull,

        /// <summary>
        /// Specifies a full glass effect appropariate for a normal state.
        /// </summary>
        GlassNormalFull,

        /// <summary>
        /// Specifies a full glass effect appropariate for a tracking state.
        /// </summary>
        GlassTrackingFull,

        /// <summary>
        /// Specifies a full glass effect appropariate for a pressed state.
        /// </summary>
        GlassPressedFull,

        /// <summary>
        /// Specifies a full glass effect appropariate for a checked state.
        /// </summary>
        GlassCheckedFull,

        /// <summary>
        /// Specifies a full glass effect appropariate for a checked/tracking state.
        /// </summary>
        GlassCheckedTrackingFull,

        /// <summary>
        /// Specifies a stumpy glass effect appropariate for a normal state.
        /// </summary>
        GlassNormalStump,

        /// <summary>
        /// Specifies a stumpy glass effect appropariate for a tracking state.
        /// </summary>
        GlassTrackingStump,

        /// <summary>
        /// Specifies a stumpy glass effect appropariate for a pressed state.
        /// </summary>
        GlassPressedStump,

        /// <summary>
        /// Specifies a stumpy glass effect appropariate for a checked state.
        /// </summary>
        GlassCheckedStump,

        /// <summary>
        /// Specifies a stumpy glass effect appropariate for a checked/tracking state.
        /// </summary>
        GlassCheckedTrackingStump,

        /// <summary>
        /// Specifies a simple glass effect appropariate for a normal state.
        /// </summary>
        GlassNormalSimple,

        /// <summary>
        /// Specifies a simple glass effect appropariate for a tracking state.
        /// </summary>
        GlassTrackingSimple,

        /// <summary>
        /// Specifies a simple glass effect appropariate for a pressed state.
        /// </summary>
        GlassPressedSimple,

        /// <summary>
        /// Specifies a simple glass effect appropariate for a checked state.
        /// </summary>
        GlassCheckedSimple,

        /// <summary>
        /// Specifies a simple glass effect appropariate for a checked/tracking state.
        /// </summary>
        GlassCheckedTrackingSimple,

        /// <summary>
        /// Specifies a glass effect with fading from the center.
        /// </summary>
        GlassCenter,

        /// <summary>
        /// Specifies a glass effect with fading from the bottom.
        /// </summary>
        GlassBottom,

        /// <summary>
        /// Specifies a simple glass effect that fades away to nothing by end of the area.
        /// </summary>
        GlassFade,

        /// <summary>
        /// Specifies an expert style button with tracking effect.
        /// </summary>
        ExpertTracking,

        /// <summary>
        /// Specifies an expert style button with pressed effect.
        /// </summary>
        ExpertPressed,

        /// <summary>
        /// Specifies an expert style button that is checked.
        /// </summary>
        ExpertChecked,

        /// <summary>
        /// Specifies an expert style button that is checked with tracking effect.
        /// </summary>
        ExpertCheckedTracking,

        /// <summary>
        /// Specifies an expert style button that has a square inner area with highlighting.
        /// </summary>
        ExpertSquareHighlight,

        /// <summary>
        /// Specifies an expert style button that has a square inner area with highlighting variation 2.
        /// </summary>
        ExpertSquareHighlight2,
    }
	#endregion

	#region Enum PaletteImageStyle
	/// <summary>
	/// Specifies the an image is aligned.
	/// </summary>
    [TypeConverter(typeof(PaletteImageStyleConverter))]
    public enum PaletteImageStyle
	{
		/// <summary>
		/// Specifies image style should be inherited.
		/// </summary>
		Inherit,

		/// <summary>
		/// Specifies the image is placed in the top left.
		/// </summary>
		TopLeft,

		/// <summary>
		/// Specifies the image is placed in the center at the top.
		/// </summary>
		TopMiddle,

		/// <summary>
		/// Specifies the image is placed in the top right.
		/// </summary>
		TopRight,

		/// <summary>
		/// Specifies the image is placed in the center at the left.
		/// </summary>
		CenterLeft,

		/// <summary>
		/// Specifies the image is placed in the center.
		/// </summary>
		CenterMiddle,

		/// <summary>
		/// Specifies the image is placed in the center at the right.
		/// </summary>
		CenterRight,

		/// <summary>
		/// Specifies the image is placed in the bottom left.
		/// </summary>
		BottomLeft,

		/// <summary>
		/// Specifies the image is placed in the center at the bottom.
		/// </summary>
		BottomMiddle,

		/// <summary>
		/// Specifies the image is placed in the bottom right.
		/// </summary>
		BottomRight,

		/// <summary>
		/// Specifies image should be stretch to fix area.
		/// </summary>
		Stretch,

		/// <summary>
		/// Specifies the image is tiled without flipping.
		/// </summary>
		Tile,

		/// <summary>
		/// Specifies the image is tiled with flip horizontally.
		/// </summary>
		TileFlipX,

		/// <summary>
		/// Specifies the image is tiled with flip vertically.
		/// </summary>
		TileFlipY,

		/// <summary>
		/// Specifies the image is tiled with flip horizontally and vertically.
		/// </summary>
		TileFlipXY
	}
	#endregion

    #region Enum PaletteDrawBorders
	/// <summary>
	/// Specifies the an image is aligned.
	/// </summary>
    [Flags()]
    [TypeConverter(typeof(PaletteDrawBordersConverter))]
    public enum PaletteDrawBorders
    {
        /// <summary>
        /// Specifies borders to draw should be inherited.
        /// </summary>
        Inherit = 0x10,

        /// <summary>
        /// Specifies that no borders are required.
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Specifies the top border should be drawn.
        /// </summary>
        Top = 0x01,

        /// <summary>
        /// Specifies the bottom border should be drawn.
        /// </summary>
        Bottom = 0x02,

        /// <summary>
        /// Specifies the top and bottom border.
        /// </summary>
        TopBottom = 0x03,

        /// <summary>
        /// Specifies the left border should be drawn.
        /// </summary>
        Left = 0x04,

        /// <summary>
        /// Specifies the top and bottom border.
        /// </summary>
        TopLeft = 0x05,

        /// <summary>
        /// Specifies the left and bottom borders.
        /// </summary>
        BottomLeft = 0x06,

        /// <summary>
        /// Specifies the bottom and right borders.
        /// </summary>
        TopBottomLeft = 0x07,

        /// <summary>
        /// Specifies the right border should be drawn.
        /// </summary>
        Right = 0x08,

        /// <summary>
        /// Specifies the top and bottom border.
        /// </summary>
        TopRight = 0x09,

        /// <summary>
        /// Specifies the bottom and right borders.
        /// </summary>
        BottomRight = 0x0A,

        /// <summary>
        /// Specifies the bottom and right borders.
        /// </summary>
        TopBottomRight = 0x0B,

        /// <summary>
        /// Specifies the left and right borders.
        /// </summary>
        LeftRight = 0x0C,

        /// <summary>
        /// Specifies the bottom and right borders.
        /// </summary>
        TopLeftRight = 0x0D,

        /// <summary>
        /// Specifies the bottom and right borders.
        /// </summary>
        BottomLeftRight = 0x0E,

        /// <summary>
        /// Specifies that all borders be drawn.
        /// </summary>
        All = 0x0F,
    }
    #endregion

    #region Enum PaletteImageEffect
    /// <summary>
	/// Specifies how an image is drawn.
	/// </summary>
    [TypeConverter(typeof(PaletteImageEffectConverter))]
    public enum PaletteImageEffect
	{
		/// <summary>
		/// Specifies effect should be inherited.
		/// </summary>
		Inherit,

		/// <summary>
		/// Specifies image is drawn without modification.
		/// </summary>
		Normal,

		/// <summary>
		/// Specifies image is drawn to look disabled.
		/// </summary>
		Disabled,

		/// <summary>
		/// Specifies image is drawn converted to a grayscale.
		/// </summary>
		GrayScale,

		/// <summary>
		/// Specifies image is drawn converted to a grayscale except for red.
		/// </summary>
		GrayScaleRed,

		/// <summary>
		/// Specifies image is drawn converted to a grayscale except for green.
		/// </summary>
		GrayScaleGreen,

		/// <summary>
		/// Specifies image is drawn converted to a grayscale except for blue.
		/// </summary>
		GrayScaleBlue,
		
		/// <summary>
		/// Specifies image is drawn slightly lighter.
		/// </summary>
		Light,

		/// <summary>
		/// Specifies image is drawn much lighter.
		/// </summary>
		LightLight,

		/// <summary>
		/// Specifies image is drawn slightly darker.
		/// </summary>
		Dark,

		/// <summary>
		/// Specifies image is drawn much darker.
		/// </summary>
		DarkDark,
	}
	#endregion

    #region Enum PaletteButtonSpecStyle
    /// <summary>
    /// Specifies the style of button spec.
    /// </summary>
    [TypeConverter(typeof(PaletteButtonSpecStyleConverter))]
    public enum PaletteButtonSpecStyle
    {
        /// <summary>
        /// Specifies a general purpose button specification.
        /// </summary>
        Generic,

        /// <summary>
        /// Specifies a close button specification.
        /// </summary>
        Close,

        /// <summary>
        /// Specifies a context button specification.
        /// </summary>
        Context,

        /// <summary>
        /// Specifies a next button specification.
        /// </summary>
        Next,

        /// <summary>
        /// Specifies a previous button specification.
        /// </summary>
        Previous,

        /// <summary>
        /// Specifies a left pointing arrow button specification.
        /// </summary>
        ArrowLeft,

            /// <summary>
        /// Specifies a right pointing arrow button specification.
        /// </summary>
        ArrowRight,

            /// <summary>
        /// Specifies an upwards pointing arrow button specification.
        /// </summary>
        ArrowUp,

            /// <summary>
        /// Specifies a downwards pointing arrow button specification.
        /// </summary>
        ArrowDown,

        /// <summary>
        /// Specifies a drop down button specification.
        /// </summary>
        DropDown,

        /// <summary>
        /// Specifies a vertical pin specification.
        /// </summary>
        PinVertical,

        /// <summary>
        /// Specifies a horizontal pin specification.
        /// </summary>
        PinHorizontal,

        /// <summary>
        /// Specifies a form close button specification.
        /// </summary>
        FormClose,

        /// <summary>
        /// Specifies a form minimize button specification.
        /// </summary>
        FormMin,

        /// <summary>
        /// Specifies a form maximize button specification.
        /// </summary>
        FormMax,

        /// <summary>
        /// Specifies a form restore button specification.
        /// </summary>
        FormRestore,

        /// <summary>
        /// Specifies a pendant close button specification.
        /// </summary>
        PendantClose,

        /// <summary>
        /// Specifies a pendant minimize button specification.
        /// </summary>
        PendantMin,

        /// <summary>
        /// Specifies a pendant restore button specification.
        /// </summary>
        PendantRestore,

        /// <summary>
        /// Specifies a workspace maximize button specification.
        /// </summary>
        WorkspaceMaximize,

        /// <summary>
        /// Specifies a workspace maximize button specification.
        /// </summary>
        WorkspaceRestore,

        /// <summary>
        /// Specifies a ribbon minimize button specification.
        /// </summary>
        RibbonMinimize,

        /// <summary>
        /// Specifies a ribbon expand button specification.
        /// </summary>
        RibbonExpand,
    }
    #endregion

    #region Enum PaletteButtonEnabled
    /// <summary>
    /// Specifies the enabled state of a button specification.
    /// </summary>
    public enum PaletteButtonEnabled
    {
        /// <summary>
        /// Specifies enabled state should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies button should take enabled state from container control state.
        /// </summary>
        Container,

        /// <summary>
        /// Specifies button should be enabled.
        /// </summary>
        True,

        /// <summary>
        /// Specifies button should be disabled.
        /// </summary>
        False,
    }
    #endregion

    #region Enum PaletteButtonOrientation
    /// <summary>
    /// Specifies the orientation of a button specification.
    /// </summary>
    [TypeConverter(typeof(PaletteButtonOrientationConverter))]
    public enum PaletteButtonOrientation
    {
        /// <summary>
        /// Specifies orientation should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies orientation should automatically match the concept of use.
        /// </summary>
        Auto,

        /// <summary>
        /// Specifies the button is orientated in a vertical top down manner.
        /// </summary>
        FixedTop,

        /// <summary>
        /// Specifies the button is orientated in a vertical bottom upwards manner.
        /// </summary>
        FixedBottom,

        /// <summary>
        /// Specifies the button is orientated in a horizontal left to right manner.
        /// </summary>
        FixedLeft,

        /// <summary>
        /// Specifies the button is orientated in a horizontal right to left manner.
        /// </summary>
        FixedRight
    }
    #endregion

	#region Enum PaletteRectangleAlign
	/// <summary>
	/// Specifies how a display rectangle aligns.
	/// </summary>
	public enum PaletteRectangleAlign
	{
		/// <summary>
		/// Specifies alignment should be inherited.
		/// </summary>
		Inherit,

		/// <summary>
		/// Specifies the client area of the rendering item.
		/// </summary>
		Local,

		/// <summary>
		/// Specifies the client area of the Control.
		/// </summary>
		Control,

		/// <summary>
		/// Specifies the client area of the owning Form.
		/// </summary>
		Form
	}
	#endregion

	#region Enum PaletteRelativeAlign
	/// <summary>
	/// Specifies a relative alignment position.
	/// </summary>
	public enum PaletteRelativeAlign
	{
		/// <summary>
		/// Specifies relative alignment should be inherited.
		/// </summary>
		Inherit = -1,

		/// <summary>
		/// Specifies a relative alignment of near.
		/// </summary>
		Near = 0,

		/// <summary>
		/// Specifies a relative alignment of center.
		/// </summary>
		Center = 1,

		/// <summary>
		/// Specifies a relative alignment of far.
		/// </summary>
		Far = 2
	}
	#endregion

    #region Enum PaletteRelativeEdgeAlign
    /// <summary>
    /// Specifies a relative button alignment position.
    /// </summary>
    public enum PaletteRelativeEdgeAlign
    {
        /// <summary>
        /// Specifies relative alignment should be inherited.
        /// </summary>
        Inherit = -1,

        /// <summary>
        /// Specifies a relative alignment of near.
        /// </summary>
        Near = 0,

        /// <summary>
        /// Specifies a relative alignment of far.
        /// </summary>
        Far = 2
    }
    #endregion

	#region Enum PaletteGraphicsHint
	/// <summary>
	/// Specifies a graphics rendering hint.
	/// </summary>
	public enum PaletteGraphicsHint
	{
		/// <summary>
		/// Specifies graphics hint should be inherited.
		/// </summary>
		Inherit = -1,

		/// <summary>
		/// Specifies no smoothing for graphics rendering.
		/// </summary>
		None,

		/// <summary>
		/// Specifies anti aliasing for graphics rendering.
		/// </summary>
		AntiAlias,
	}
	#endregion

	#region Enum PaletteTextHint
	/// <summary>
	/// Specifies a text rendering hint.
	/// </summary>
	public enum PaletteTextHint
	{
		/// <summary>
		/// Specifies text hint should be inherited.
		/// </summary>
		Inherit = -1,

		/// <summary>
		/// Specifies anti aliasing for text rendering.
		/// </summary>
		AntiAlias,

		/// <summary>
		/// Specifies anti aliasing with grid fit for text rendering.
		/// </summary>
		AntiAliasGridFit,

		/// <summary>
		/// Specifies clear type with grid fit for text rendering.
		/// </summary>
		ClearTypeGridFit,

		/// <summary>
		/// Specifies single bit per pixel for text rendering.
		/// </summary>
		SingleBitPerPixel,

		/// <summary>
		/// Specifies single bit for pixel with grid fit for text rendering.
		/// </summary>
		SingleBitPerPixelGridFit,

		/// <summary>
		/// Specifies system default setting for text rendering.
		/// </summary>
		SystemDefault
	}
	#endregion

	#region Enum PaletteTextTrim
	/// <summary>
	/// Specifies how to trim text.
	/// </summary>
    [TypeConverter(typeof(PaletteTextTrimConverter))]
    public enum PaletteTextTrim
	{
		/// <summary>
		/// Specifies text trim should be inherited.
		/// </summary>
		Inherit = -1,

        /// <summary>
		/// Specifies text is not drawn if it needs trimming.
		/// </summary>
		Hide,

        /// <summary>
        /// Specifies text is trimmed by removing end characters.
        /// </summary>
        Character,

        /// <summary>
        /// Specifies text is trimmed by removing end words.
        /// </summary>
        Word,
        
        /// <summary>
		/// Specifies text is trimmed by using ellipses and removing end characters.
		/// </summary>
		EllipsisCharacter,

        /// <summary>
        /// Specifies text is trimmed by using ellipses and removing end words.
        /// </summary>
        EllipsisWord,

        /// <summary>
        /// Specifies text is trimmed by using ellipses and removing from middle.
        /// </summary>
        EllipsisPath
	}
	#endregion

    #region Enum PaletteTextHotkeyPrefix
    /// <summary>
    /// Specifies how to show hotkey prefix characters.
    /// </summary>
    public enum PaletteTextHotkeyPrefix
    {
        /// <summary>
        /// Specifies text prefix should be inherited.
        /// </summary>
        Inherit = -1,

        /// <summary>Turns off processing of prefix characters.</summary>
        None,

        /// <summary>Turns on processing of prefix characters.</summary>
        Show,

        /// <summary>Ignores the ampersand prefix character in text.</summary>
        Hide
    }
    #endregion

    #region Enum PaletteColorIndex
    internal enum PaletteColorIndex : int
    {
        ButtonCheckedGradientBegin = 0,
        ButtonCheckedGradientEnd,
        ButtonCheckedGradientMiddle,
        ButtonCheckedHighlight,
        ButtonCheckedHighlightBorder,
        ButtonPressedBorder,
        ButtonPressedGradientBegin,
        ButtonPressedGradientEnd,
        ButtonPressedGradientMiddle,
        ButtonPressedHighlight,
        ButtonPressedHighlightBorder,
        ButtonSelectedBorder,
        ButtonSelectedGradientBegin,
        ButtonSelectedGradientEnd,
        ButtonSelectedGradientMiddle,
        ButtonSelectedHighlight,
        ButtonSelectedHighlightBorder,
        CheckBackground,
        CheckPressedBackground,
        CheckSelectedBackground,
        GripDark,
        GripLight,
        ImageMarginGradientBegin,
        ImageMarginGradientEnd,
        ImageMarginGradientMiddle,
        ImageMarginRevealedGradientBegin,
        ImageMarginRevealedGradientEnd,
        ImageMarginRevealedGradientMiddle,
        MenuBorder,
        MenuItemText,
        MenuItemBorder,
        MenuItemPressedGradientBegin,
        MenuItemPressedGradientEnd,
        MenuItemPressedGradientMiddle,
        MenuItemSelected,
        MenuItemSelectedGradientBegin,
        MenuItemSelectedGradientEnd,
        MenuStripText,
        MenuStripGradientBegin,
        MenuStripGradientEnd,
        OverflowButtonGradientBegin,
        OverflowButtonGradientEnd,
        OverflowButtonGradientMiddle,
        RaftingContainerGradientBegin,
        RaftingContainerGradientEnd,
        SeparatorDark,
        SeparatorLight,
        StatusStripText,
        StatusStripGradientBegin,
        StatusStripGradientEnd,
        ToolStripText,
        ToolStripBorder,
        ToolStripContentPanelGradientBegin,
        ToolStripContentPanelGradientEnd,
        ToolStripDropDownBackground,
        ToolStripGradientBegin,
        ToolStripGradientEnd,
        ToolStripGradientMiddle,
        ToolStripPanelGradientBegin,
        ToolStripPanelGradientEnd,
        Count
    }
    #endregion

    #region Enum PaletteRibbonBackStyle
    /// <summary>
    /// Specifies the style of ribbon background.
    /// </summary>
    public enum PaletteRibbonBackStyle
    {
        /// <summary>
        /// Specifies a background style appropriate for an application button.
        /// </summary>
        RibbonAppButton,

        /// <summary>
        /// Specifies a background style appropriate for an application menu inner area.
        /// </summary>
        RibbonAppMenuInner,

        /// <summary>
        /// Specifies a background style appropriate for an application menu outer area.
        /// </summary>
        RibbonAppMenuOuter,

        /// <summary>
        /// Specifies a background style appropriate for an application menu recent documents area.
        /// </summary>
        RibbonAppMenuDocs,

        /// <summary>
        /// Specifies a background style appropriate for a group area.
        /// </summary>
        RibbonGroupArea,

        /// <summary>
        /// Specifies a background style appropriate for a normal group border.
        /// </summary>
        RibbonGroupNormalBorder,

        /// <summary>
        /// Specifies a background style appropriate for a normal group title.
        /// </summary>
        RibbonGroupNormalTitle,

        /// <summary>
        /// Specifies a background style appropriate for a collapsed group border.
        /// </summary>
        RibbonGroupCollapsedBack,

        /// <summary>
        /// Specifies a border style appropriate for a collapsed group border.
        /// </summary>
        RibbonGroupCollapsedBorder,

        /// <summary>
        /// Specifies a background style appropriate for a collapsed group frame border.
        /// </summary>
        RibbonGroupCollapsedFrameBack,

        /// <summary>
        /// Specifies a border style appropriate for a collapsed group frame border.
        /// </summary>
        RibbonGroupCollapsedFrameBorder,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon tab.
        /// </summary>
        RibbonTab,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon quick access toolbar in full mode.
        /// </summary>
        RibbonQATFullbar,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon quick access toolbar in mini mode.
        /// </summary>
        RibbonQATMinibar,

        /// <summary>
        /// Specifies a background style appropriate for a ribbon quick access toolbar in overflow.
        /// </summary>
        RibbonQATOverflow,

        /// <summary>
        /// Specifies a background style appropriate for a gallery.
        /// </summary>
        RibbonGalleryBack,

        /// <summary>
        /// Specifies a border style appropriate for a gallery.
        /// </summary>
        RibbonGalleryBorder,
    }
    #endregion

    #region Enum PaletteRibbonTextStyle
    /// <summary>
    /// Specifies the style of ribbon text.
    /// </summary>
    public enum PaletteRibbonTextStyle
    {
        /// <summary>
        /// Specifies a text style appropriate for a normal group title.
        /// </summary>
        RibbonGroupNormalTitle,

        /// <summary>
        /// Specifies a text style appropriate for a collapsed group text.
        /// </summary>
        RibbonGroupCollapsedText,

        /// <summary>
        /// Specifies a text style appropriate for a group button text.
        /// </summary>
        RibbonGroupButtonText,

        /// <summary>
        /// Specifies a text style appropriate for a group label text.
        /// </summary>
        RibbonGroupLabelText,

        /// <summary>
        /// Specifies a text style appropriate for a group check box button text.
        /// </summary>
        RibbonGroupCheckBoxText,

        /// <summary>
        /// Specifies a text style appropriate for a group radio button text.
        /// </summary>
        RibbonGroupRadioButtonText,

        /// <summary>
        /// Specifies a text style appropriate for a ribbon tab.
        /// </summary>
        RibbonTab,

        /// <summary>
        /// Specifies a text style appropriate for a app menu recent documents title.
        /// </summary>
        RibbonAppMenuDocsTitle,

        /// <summary>
        /// Specifies a text style appropriate for a app menu recent documents entry.
        /// </summary>
        RibbonAppMenuDocsEntry
    }
    #endregion

    #region Enum PaletteRibbonColorStyle
    /// <summary>
    /// Specifies the color drawing style for ribbon elements.
    /// </summary>
    public enum PaletteRibbonColorStyle
    {
        /// <summary>
        /// Specifies color style should be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies that no drawing take place.
        /// </summary>
        Empty,

        /// <summary>
        /// Specifies solid drawing using the first color.
        /// </summary>
        Solid,

        /// <summary>
        /// Specifies linear gradient from first to second color.
        /// </summary>
        Linear,

        /// <summary>
        /// Specifies linear gradient border from first to second color.
        /// </summary>
        LinearBorder,

        /// <summary>
        /// Specifies using colors to draw a application menu inner area.
        /// </summary>
        RibbonAppMenuInner,

        /// <summary>
        /// Specifies using colors to draw a application menu inner area.
        /// </summary>
        RibbonAppMenuOuter,

        /// <summary>
        /// Specifies using colors to draw a tracking ribbon tab appropriate for Office 2007.
        /// </summary>
        RibbonTabTracking2007,

        /// <summary>
        /// Specifies using colors to draw a focused ribbon tab appropriate for Office 2010.
        /// </summary>
        RibbonTabFocus2010,

        /// <summary>
        /// Specifies using colors to draw a tracking ribbon tab appropriate for Office 2010.
        /// </summary>
        RibbonTabTracking2010,

        /// <summary>
        /// Specifies alternate drawing of the RibbonTabTracking2010 enumeration.
        /// </summary>
        RibbonTabTracking2010Alt,

        /// <summary>
        /// Specifies using colors to draw a glowing ribbon tab.
        /// </summary>
        RibbonTabGlowing,

        /// <summary>
        /// Specifies using colors to draw a selected ribbon tab appropriate for Office 2007.
        /// </summary>
        RibbonTabSelected2007,

        /// <summary>
        /// Specifies using colors to draw a selected ribbon tab appropriate for Office 2010.
        /// </summary>
        RibbonTabSelected2010,

        /// <summary>
        /// Specifies alternate drawing of the RibbonTabSelected2010 enumeration.
        /// </summary>
        RibbonTabSelected2010Alt,

        /// <summary>
        /// Specifies using colors to draw a selected and tracking ribbon tab.
        /// </summary>
        RibbonTabHighlight,

        /// <summary>
        /// Specifies using colors for an alternative way of drawing a selected and tracking ribbon tab.
        /// </summary>
        RibbonTabHighlight2,

        /// <summary>
        /// Specifies using colors to draw a context selected ribbon tab for Office 2007.
        /// </summary>
        RibbonTabContextSelected,

        /// <summary>
        /// Specifies using colors to draw a groups area border.
        /// </summary>
        RibbonGroupAreaBorder,

        /// <summary>
        /// Specifies using colors to draw a groups area border, variantion 2.
        /// </summary>
        RibbonGroupAreaBorder2,

        /// <summary>
        /// Specifies using colors to draw a groups area border, variantion 3.
        /// </summary>
        RibbonGroupAreaBorder3,

        /// <summary>
        /// Specifies using colors to draw a groups area border, variantion 4.
        /// </summary>
        RibbonGroupAreaBorder4,

        /// <summary>
        /// Specifies using colors to draw a groups area border for a context selected tab.
        /// </summary>
        RibbonGroupAreaBorderContext,

        /// <summary>
        /// Specifies using colors to draw a group normal border.
        /// </summary>
        RibbonGroupNormalBorder,

        /// <summary>
        /// Specifies using colors to draw a group normal border as a vertical separator.
        /// </summary>
        RibbonGroupNormalBorderSep,

        /// <summary>
        /// Specifies using colors to draw a group pressed border as a vertical separator, variantion based on light background.
        /// </summary>
        RibbonGroupNormalBorderSepPressedLight,

        /// <summary>
        /// Specifies using colors to draw a group pressed border as a vertical separator, variantion based on dark background.
        /// </summary>
        RibbonGroupNormalBorderSepPressedDark,

        /// <summary>
        /// Specifies using colors to draw a group tracking border as a vertical separator, variantion based on light background.
        /// </summary>
        RibbonGroupNormalBorderSepTrackingLight,

        /// <summary>
        /// Specifies using colors to draw a group tracking border as a vertical separator, variantion based on dark background.
        /// </summary>
        RibbonGroupNormalBorderSepTrackingDark,

        /// <summary>
        /// Specifies using colors to draw a tracking group normal border.
        /// </summary>
        RibbonGroupNormalBorderTracking,

        /// <summary>
        /// Specifies using colors to draw a tracking group normal border with light inside edge.
        /// </summary>
        RibbonGroupNormalBorderTrackingLight,

        /// <summary>
        /// Specifies using colors to draw a group normal title.
        /// </summary>
        RibbonGroupNormalTitle,

        /// <summary>
        /// Specifies using colors to draw a group collapsed border.
        /// </summary>
        RibbonGroupCollapsedBorder,

        /// <summary>
        /// Specifies using colors to draw a group collapsed frame border.
        /// </summary>
        RibbonGroupCollapsedFrameBorder,

        /// <summary>
        /// Specifies using colors to draw a group collapsed frame back.
        /// </summary>
        RibbonGroupCollapsedFrameBack,

        /// <summary>
        /// Specifies using colors to draw a one tone gradient in the groups area.
        /// </summary>
        RibbonGroupGradientOne,

        /// <summary>
        /// Specifies using colors to draw a two tone gradient in the groups area.
        /// </summary>
        RibbonGroupGradientTwo,

        /// <summary>
        /// Specifies using colors to draw a rounded quick access toolbar mini area with single rounded end.
        /// </summary>
        RibbonQATMinibarSingle,

        /// <summary>
        /// Specifies using colors to draw a rounded quick access toolbar mini area with double rounded end.
        /// </summary>
        RibbonQATMinibarDouble,

        /// <summary>
        /// Specifies using colors to draw a rounded quick access toolbar full area.
        /// </summary>
        RibbonQATFullbarRound,

        /// <summary>
        /// Specifies using colors to draw a square quick access toolbar full area.
        /// </summary>
        RibbonQATFullbarSquare,

        /// <summary>
        /// Specifies using colors to draw a rounded quick access toolbar overflow.
        /// </summary>
        RibbonQATOverflow
    }
    #endregion

    #region Enum PaletteRibbonGalleryButton
    /// <summary>
    /// Specifies a ribbon gallery button.
    /// </summary>
    public enum PaletteRibbonGalleryButton
    {
        /// <summary>
        /// Specifies the up gallery button.
        /// </summary>
        Up,

        /// <summary>
        /// Specifies the down gallery button.
        /// </summary>
        Down,

        /// <summary>
        /// Specifies the drop down gallery button.
        /// </summary>
        DropDown,
    }
    #endregion

    #region Enum PaletteRibbonShape
    /// <summary>
    /// Specifies a ribbon shape.
    /// </summary>
    public enum PaletteRibbonShape
    {
        /// <summary>
        /// Specifies the up gallery button.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies the Office 2007 ribbon shape.
        /// </summary>
        Office2007,

        /// <summary>
        /// Specifies the Office 2010 ribbon shape.
        /// </summary>
        Office2010,
    }
    #endregion

    #region Enum PaletteTrackBarSize
    /// <summary>
    /// Specifies the track bar size.
    /// </summary>
    public enum PaletteTrackBarSize
    {
        /// <summary>
        /// Specifies a small track bar.
        /// </summary>
        Small,

        /// <summary>
        /// Specifies a medium track bar.
        /// </summary>
        Medium,

        /// <summary>
        /// Specifies a large track bar.
        /// </summary>
        Large,
    }
    #endregion

    #region Enum PaletteElement
    /// <summary>
    /// Specifies a palette element.
    /// </summary>
    public enum PaletteElement
    {
        /// <summary>
        /// Specifies the track of a track bar.
        /// </summary>
        TrackBarTrack,

        /// <summary>
        /// Specifies the tick of a track bar.
        /// </summary>
        TrackBarTick,

        /// <summary>
        /// Specifies the position marker of a track bar.
        /// </summary>
        TrackBarPosition,
    }
    #endregion

    #region Enum PaletteDragFeedback
    /// <summary>
    /// Specifies how drag feedback is presented.
    /// </summary>
    public enum PaletteDragFeedback
    {
        /// <summary>
        /// Draw drag drop feedback as just blocks that are highlighted based on hot areas. 
        /// </summary>
        Block,

        /// <summary>
        /// Draw drag drop feedback as square indicators.
        /// </summary>
        Square,

        /// <summary>
        /// Draw drag drop feedback as rounded indicators.
        /// </summary>
        Rounded,

        /// <summary>
        /// Draw drag drop feedback using the inherited value.
        /// </summary>
        Inherit
    }
    #endregion

    #region Delegates
    /// <summary>
    /// Signature of methods that return an integer metric.
    /// </summary>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <param name="metric">Metric value required.</param>
    /// <returns>Integer value.</returns>
    public delegate int GetIntMetric(PaletteState state, PaletteMetricInt metric);
    
    /// <summary>
	/// Signature of methods that return a bool metric.
	/// </summary>
	/// <param name="state">Palette value should be applicable to this state.</param>
	/// <param name="metric">Metric value required.</param>
	/// <returns>InheritBool value.</returns>
	public delegate InheritBool GetBoolMetric(PaletteState state, PaletteMetricBool metric);

	/// <summary>
	/// Signature of methods that return a padding metric.
	/// </summary>
	/// <param name="state">Palette value should be applicable to this state.</param>
	/// <param name="metric">Metric value required.</param>
	/// <returns>Padding value.</returns>
	public delegate Padding GetPaddingMetric(PaletteState state, PaletteMetricPadding metric);
	#endregion
}
