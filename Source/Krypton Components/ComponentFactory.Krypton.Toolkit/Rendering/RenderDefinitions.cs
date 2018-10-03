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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Toolkit
{
	#region IRenderer
	/// <summary>
	/// Exposes access to specialized renderers.
	/// </summary>
	public interface IRenderer
	{
		/// <summary>
        /// Gets the standard border renderer.
		/// </summary>
		IRenderBorder RenderStandardBorder { get; }

		/// <summary>
        /// Gets the standard background renderer.
		/// </summary>
		IRenderBack RenderStandardBack { get; }

		/// <summary>
        /// Gets the standard content renderer.
		/// </summary>
		IRenderContent RenderStandardContent { get; }

        /// <summary>
        /// Gets the tab border renderer.
        /// </summary>
        IRenderTabBorder RenderTabBorder { get; }

        /// <summary>
        /// Gets the ribbon renderer.
        /// </summary>
        IRenderRibbon RenderRibbon { get; }

        /// <summary>
        /// Gets the glpyh renderer.
        /// </summary>
        IRenderGlyph RenderGlyph { get; }

        /// <summary>
        /// Evaluate if transparent painting is needed for background palette.
        /// </summary>
        /// <param name="paletteBack">Background palette to test.</param>
        /// <param name="state">Element state associated with palette.</param>
        /// <returns>True if transparent painting required.</returns>
        bool EvalTransparentPaint(IPaletteBack paletteBack, 
                                  PaletteState state);

        /// <summary>
        /// Evaluate if transparent painting is needed for background or border palettes.
        /// </summary>
        /// <param name="paletteBack">Background palette to test.</param>
        /// <param name="paletteBorder">Background palette to test.</param>
        /// <param name="state">Element state associated with palette.</param>
        /// <returns>True if transparent painting required.</returns>
        bool EvalTransparentPaint(IPaletteBack paletteBack, 
                                  IPaletteBorder paletteBorder, 
                                  PaletteState state);

        /// <summary>
        /// Gets a renderer for drawing the toolstrips.
        /// </summary>
        /// <param name="colorPalette">Color palette to use when rendering toolstrip.</param>
        ToolStripRenderer RenderToolStrip(IPalette colorPalette);
	}
	#endregion

	#region IRenderBorder
	/// <summary>
	/// Exposes methods for drawing borders.
	/// </summary>
	public interface IRenderBorder
	{
        /// <summary>
        /// Gets the raw padding used per edge of the border.
        /// </summary>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <returns>Padding structure detailing all four edges.</returns>
        Padding GetBorderRawPadding(IPaletteBorder palette,
                                    PaletteState state,
                                    VisualOrientation orientation);

		/// <summary>
		/// Gets the padding used to position display elements completely inside border drawing.
		/// </summary>
		/// <param name="palette">Palette used for drawing.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <returns>Padding structure detailing all four edges.</returns>
		Padding GetBorderDisplayPadding(IPaletteBorder palette, 
										PaletteState state,
                                        VisualOrientation orientation);

        /// <summary>
        /// Generate a graphics path that is the outside edge of the border.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <returns>GraphicsPath instance.</returns>
        GraphicsPath GetOutsideBorderPath(RenderContext context,
                                          Rectangle rect,
                                          IPaletteBorder palette,
                                          VisualOrientation orientation,
                                          PaletteState state);

		/// <summary>
		/// Generate a graphics path that is in the middle of the border.
		/// </summary>
		/// <param name="context">Rendering context.</param>
		/// <param name="rect">Target rectangle.</param>
		/// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
		/// <returns>GraphicsPath instance.</returns>
		GraphicsPath GetBorderPath(RenderContext context, 
								   Rectangle rect, 
								   IPaletteBorder palette,
                                   VisualOrientation orientation,
								   PaletteState state);

        /// <summary>
        /// Generate a graphics path that encloses the border and is used when rendering a background to ensure the background does not draw over the border area.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <returns>GraphicsPath instance.</returns>
        GraphicsPath GetBackPath(RenderContext context,
                                 Rectangle rect,
                                 IPaletteBorder palette,
                                 VisualOrientation orientation,
                                 PaletteState state);

		/// <summary>
		/// Draw border on the inside edge of the specified rectangle.
		/// </summary>
		/// <param name="context">Rendering context.</param>
		/// <param name="rect">Target rectangle.</param>
		/// <param name="palette">Palette used for drawing.</param>
		/// <param name="orientation">Visual orientation of the border.</param>
		/// <param name="state">State associated with rendering.</param>
		void DrawBorder(RenderContext context, 
						Rectangle rect, 
						IPaletteBorder palette,
						VisualOrientation orientation,
						PaletteState state);
	}
	#endregion

	#region IRenderBack
	/// <summary>
	/// Exposes methods for drawing backgrounds.
	/// </summary>
	public interface IRenderBack
	{
		/// <summary>
		/// Draw background to fill the specified path.
		/// </summary>
		/// <param name="context">Rendering context.</param>
		/// <param name="rect">Target rectangle that encloses path.</param>
		/// <param name="path">Graphics path.</param>
		/// <param name="palette">Palette used for drawing.</param>
		/// <param name="orientation">Visual orientation of the background.</param>
		/// <param name="state">State associated with rendering.</param>
        /// <param name="memento">Cache used for drawing.</param>
        IDisposable DrawBack(RenderContext context, 
					         Rectangle rect, 
					         GraphicsPath path, 
					         IPaletteBack palette,
					         VisualOrientation orientation,
					         PaletteState state,
                             IDisposable memento);
	}
	#endregion

	#region IRenderContent
	/// <summary>
	/// Exposes methods for drawing content.
	/// </summary>
	public interface IRenderContent
	{
		/// <summary>
		/// Get the preferred size for drawing the content.
		/// </summary>
		/// <param name="context">Layout context.</param>
		/// <param name="palette">Content palette details.</param>
		/// <param name="values">Content values.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="composition">Drawing onto a composition element.</param>
        /// <returns>Preferred size.</returns>
		Size GetContentPreferredSize(ViewLayoutContext context,
									 IPaletteContent palette,
									 IContentValues values,
									 VisualOrientation orientation,
                                     PaletteState state,
                                     bool composition);

		/// <summary>
		/// Perform layout calculations on the provided content.
		/// </summary>
		/// <param name="context">Layout context.</param>
		/// <param name="availableRect">Space available for laying out.</param>
		/// <param name="palette">Content palette details.</param>
		/// <param name="values">Content values.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
		/// <param name="state">State associated with rendering.</param>
        /// <param name="composition">Drawing onto a composition element.</param>
        /// <returns>Memento with cached information.</returns>
		IDisposable LayoutContent(ViewLayoutContext context,
							      Rectangle availableRect,
							      IPaletteContent palette,
							      IContentValues values,
							      VisualOrientation orientation,
                                  PaletteState state,
                                  bool composition);

		/// <summary>
		/// Perform draw of content using provided memento.
		/// </summary>
		/// <param name="context">Render context.</param>
		/// <param name="displayRect">Display area available for drawing.</param>
		/// <param name="palette">Content palette details.</param>
		/// <param name="memento">Cached values from layout call.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
		/// <param name="state">State associated with rendering.</param>
        /// <param name="composition">Drawing onto a composition element.</param>
        /// <param name="allowFocusRect">Allow drawing of focus rectangle.</param>
        void DrawContent(RenderContext context,
						 Rectangle displayRect,
						 IPaletteContent palette,
                         IDisposable memento,
						 VisualOrientation orientation,
						 PaletteState state,
                         bool composition,
                         bool allowFocusRect);

        /// <summary>
        /// Request the calculated display of the image.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>True if the image is being displayed; otherwise false.</returns>
        bool GetContentImageDisplayed(IDisposable memento);

        /// <summary>
        /// Request the calculated position of the content image.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>Display rectangle for the image content.</returns>
        Rectangle GetContentImageRectangle(IDisposable memento);

        /// <summary>
        /// Request the calculated display of the short text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>True if the short text is being displayed; otherwise false.</returns>
        bool GetContentShortTextDisplayed(IDisposable memento);

        /// <summary>
        /// Request the calculated position of the content short text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>Display rectangle for the image content.</returns>
        Rectangle GetContentShortTextRectangle(IDisposable memento);

        /// <summary>
        /// Request the calculated display of the long text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>True if the long text is being displayed; otherwise false.</returns>
        bool GetContentLongTextDisplayed(IDisposable memento);

        /// <summary>
        /// Request the calculated position of the content long text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>Display rectangle for the image content.</returns>
        Rectangle GetContentLongTextRectangle(IDisposable memento);
    }
	#endregion

    #region IRenderTabBorder
    /// <summary>
    /// Exposes methods for drawing tab borders.
    /// </summary>
    public interface IRenderTabBorder
    {
        /// <summary>
        /// Gets if the tabs should be drawn from left to right for z-ordering.
        /// </summary>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>True for left to right, otherwise draw right to left.</returns>
        bool GetTabBorderLeftDrawing(TabBorderStyle tabBorderStyle);

        /// <summary>
        /// Gets the spacing used to separate each tab border instance.
        /// </summary>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>Number of pixels to space instances.</returns>
        int GetTabBorderSpacingGap(TabBorderStyle tabBorderStyle);

        /// <summary>
        /// Gets the padding used to position display elements completely inside border drawing.
        /// </summary>
        /// <param name="context">Layout context.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>Padding structure detailing all four edges.</returns>
        Padding GetTabBorderDisplayPadding(ViewLayoutContext context,
                                           IPaletteBorder palette,
                                           PaletteState state,
                                           VisualOrientation orientation,
                                           TabBorderStyle tabBorderStyle);

        /// <summary>
        /// Generate a graphics path that encloses the border itself.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>GraphicsPath instance.</returns>
        GraphicsPath GetTabBorderPath(RenderContext context,
                                      Rectangle rect,
                                      IPaletteBorder palette,
                                      VisualOrientation orientation,
                                      PaletteState state,
                                      TabBorderStyle tabBorderStyle);

        /// <summary>
        /// Generate a graphics path that encloses the border and is used when rendering a background to ensure the background does not draw over the border area.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>GraphicsPath instance.</returns>
        GraphicsPath GetTabBackPath(RenderContext context,
                                    Rectangle rect,
                                    IPaletteBorder palette,
                                    VisualOrientation orientation,
                                    PaletteState state,
                                    TabBorderStyle tabBorderStyle);

        /// <summary>
        /// Draw border on the inside edge of the specified rectangle.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        void DrawTabBorder(RenderContext context,
                           Rectangle rect,
                           IPaletteBorder palette,
                           VisualOrientation orientation,
                           PaletteState state,
                           TabBorderStyle tabBorderStyle);
    }
    #endregion

    #region IRenderRibbon
    /// <summary>
    /// Exposes methods for drawing ribbon elements.
    /// </summary>
    public interface IRenderRibbon
    {
        /// <summary>
        /// Draw the background of a ribbon element.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="palette">Palette used for sourcing settings.</param>
        /// <param name="orientation">Orientation for drawing.</param>
        /// <param name="composition">Drawing onto a composition element.</param>
        /// <param name="memento">Cached values to use when drawing.</param>
        IDisposable DrawRibbonBack(PaletteRibbonShape shape,
                                   RenderContext context,
                                   Rectangle rect,
                                   PaletteState state,
                                   IPaletteRibbonBack palette,
                                   VisualOrientation orientation,
                                   bool composition,
                                   IDisposable memento);

        /// <summary>
        /// Draw a context ribbon tab title.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="paletteGeneral">Palette used for general ribbon settings.</param>
        /// <param name="paletteBack">Palette used for background ribbon settings.</param>
        /// <param name="memento">Cached values to use when drawing.</param>
        IDisposable DrawRibbonTabContextTitle(PaletteRibbonShape shape,
                                              RenderContext context,
                                              Rectangle rect,
                                              IPaletteRibbonGeneral paletteGeneral,
                                              IPaletteRibbonBack paletteBack,
                                              IDisposable memento);
        /// <summary>
        /// Draw the application button.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="palette">Palette used for sourcing settings.</param>
        /// <param name="memento">Cached values to use when drawing.</param>
        IDisposable DrawRibbonApplicationButton(PaletteRibbonShape shape,
                                                RenderContext context,
                                                Rectangle rect,
                                                PaletteState state,
                                                IPaletteRibbonBack palette,
                                                IDisposable memento);

        /// <summary>
        /// Draw the application tab.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="baseColor1">Base color1 used for drawing the ribbon tab.</param>
        /// <param name="baseColor2">Base color2 used for drawing the ribbon tab.</param>
        /// <param name="memento">Cached values to use when drawing.</param>
        IDisposable DrawRibbonApplicationTab(PaletteRibbonShape shape,
                                             RenderContext context,
                                             Rectangle rect,
                                             PaletteState state,
                                             Color baseColor1,
                                             Color baseColor2,
                                             IDisposable memento);

        /// <summary>
        /// Perform drawing of a ribbon cluster edge.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteBack">Palette used for recovering drawing details.</param>
        /// <param name="state">State associated with rendering.</param>
        void DrawRibbonClusterEdge(PaletteRibbonShape shape,
                                   RenderContext context,
                                   Rectangle displayRect,
                                   IPaletteBack paletteBack,
                                   PaletteState state);
    }
    #endregion

    #region IRenderGlyph
    /// <summary>
    /// Exposes methods for drawing glyph elements.
    /// </summary>
    public interface IRenderGlyph
    {
        /// <summary>
        /// Perform drawing of a separator glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteBack">Background palette details.</param>
        /// <param name="paletteBorder">Border palette details.</param>
        /// <param name="orientation">Visual orientation of the content.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="canMove">Can the separator be moved.</param>
        void DrawSeparator(RenderContext context,
                           Rectangle displayRect,
                           IPaletteBack paletteBack,
                           IPaletteBorder paletteBorder,
                           Orientation orientation,
                           PaletteState state,
                           bool canMove);

        /// <summary>
        /// Calculate the requested display size for the check box.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="enabled">Should check box be displayed as enabled.</param>
        /// <param name="checkState">The checked state of the check box.</param>
        /// <param name="tracking">Should check box be displayed as hot tracking.</param>
        /// <param name="pressed">Should check box be displayed as pressed.</param>
        Size GetCheckBoxPreferredSize(ViewLayoutContext context,
                                      IPalette palette,
                                      bool enabled,
                                      CheckState checkState,
                                      bool tracking,
                                      bool pressed);

        /// <summary>
        /// Perform drawing of a check box.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="enabled">Should check box be displayed as enabled.</param>
        /// <param name="checkState">The checked state of the check box.</param>
        /// <param name="tracking">Should check box be displayed as hot tracking.</param>
        /// <param name="pressed">Should check box be displayed as pressed.</param>
        void DrawCheckBox(RenderContext context,
                          Rectangle displayRect,
                          IPalette palette,
                          bool enabled,
                          CheckState checkState,
                          bool tracking,
                          bool pressed);

        /// <summary>
        /// Calculate the requested display size for the radio button.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="enabled">Should check box be displayed as enabled.</param>
        /// <param name="checkState">Checked state of the radio button.</param>
        /// <param name="tracking">Should check box be displayed as hot tracking.</param>
        /// <param name="pressed">Should check box be displayed as pressed.</param>
        Size GetRadioButtonPreferredSize(ViewLayoutContext context,
                                         IPalette palette,
                                         bool enabled,
                                         bool checkState,
                                         bool tracking,
                                         bool pressed);

        /// <summary>
        /// Perform drawing of a radio button.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="enabled">Should radio button be displayed as enabled.</param>
        /// <param name="checkState">Checked state of the radio button.</param>
        /// <param name="tracking">Should radio button be displayed as hot tracking.</param>
        /// <param name="pressed">Should radio button be displayed as pressed.</param>
        void DrawRadioButton(RenderContext context,
                             Rectangle displayRect,
                             IPalette palette,
                             bool enabled,
                             bool checkState,
                             bool tracking,
                             bool pressed);

        /// <summary>
        /// Calculate the requested display size for the drop down button.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="state">State for which image size is needed.</param>
        /// <param name="orientation">How to orientate the image.</param>
        Size GetDropDownButtonPreferredSize(ViewLayoutContext context,
                                            IPalette palette,
                                            PaletteState state,
                                            VisualOrientation orientation);

        /// <summary>
        /// Perform drawing of a drop down button.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="state">State for which image size is needed.</param>
        /// <param name="orientation">How to orientate the image.</param>
        void DrawDropDownButton(RenderContext context,
                                Rectangle displayRect,
                                IPalette palette,
                                PaletteState state,
                                VisualOrientation orientation);

        /// <summary>
        /// Draw a drop down button image appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        void DrawInputControlDropDownGlyph(RenderContext context,
                                           Rectangle cellRect,
                                           IPaletteContent paletteContent,
                                           PaletteState state);

        /// <summary>
        /// Draw a numeric up button image appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        void DrawInputControlNumericUpGlyph(RenderContext context,
                                            Rectangle cellRect,
                                            IPaletteContent paletteContent,
                                            PaletteState state);

        /// <summary>
        /// Draw a numeric down button image appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        void DrawInputControlNumericDownGlyph(RenderContext context,
                                              Rectangle cellRect,
                                              IPaletteContent paletteContent,
                                              PaletteState state);

        /// <summary>
        /// Perform drawing of a ribbon dialog box launcher glyph.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        void DrawRibbonDialogBoxLauncher(PaletteRibbonShape shape,
                                         RenderContext context,
                                         Rectangle displayRect,
                                         IPaletteRibbonGeneral paletteGeneral,
                                         PaletteState state);

        /// <summary>
        /// Perform drawing of a ribbon drop arrow glyph.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        void DrawRibbonDropArrow(PaletteRibbonShape shape,
                                 RenderContext context,
                                 Rectangle displayRect,
                                 IPaletteRibbonGeneral paletteGeneral,
                                 PaletteState state);

        /// <summary>
        /// Perform drawing of a ribbon context arrow glyph.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        void DrawRibbonContextArrow(PaletteRibbonShape shape,
                                    RenderContext context,
                                    Rectangle displayRect,
                                    IPaletteRibbonGeneral paletteGeneral,
                                    PaletteState state);

        /// <summary>
        /// Perform drawing of a ribbon overflow image.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        void DrawRibbonOverflow(PaletteRibbonShape shape,
                                RenderContext context,
                                Rectangle displayRect,
                                IPaletteRibbonGeneral paletteGeneral,
                                PaletteState state);

        /// <summary>
        /// Perform drawing of a ribbon group separator.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        void DrawRibbonGroupSeparator(PaletteRibbonShape shape,
                                      RenderContext context,
                                      Rectangle displayRect,
                                      IPaletteRibbonGeneral paletteGeneral,
                                      PaletteState state);

        /// <summary>
        /// Draw a grid sorting direction glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="sortOrder">Sorting order of the glyph.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Palette to use for sourcing values.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="rtl">Should be drawn from right to left.</param>
        /// <returns>Remainder space left over for other drawing.</returns>
        Rectangle DrawGridSortGlyph(RenderContext context,
                                    SortOrder sortOrder,
                                    Rectangle cellRect,
                                    IPaletteContent paletteContent,
                                    PaletteState state,
                                    bool rtl);

        /// <summary>
        /// Draw a grid row glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="rowGlyph">Row glyph.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Palette to use for sourcing values.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="rtl">Should be drawn from right to left.</param>
        /// <returns>Remainder space left over for other drawing.</returns>
        Rectangle DrawGridRowGlyph(RenderContext context,
                                   GridRowGlyph rowGlyph,
                                   Rectangle cellRect,
                                   IPaletteContent paletteContent,
                                   PaletteState state,
                                   bool rtl);

        /// <summary>
        /// Draw a grid error glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="rtl">Should be drawn from right to left.</param>
        /// <returns>Remainder space left over for other drawing.</returns>
        Rectangle DrawGridErrorGlyph(RenderContext context,
                                     Rectangle cellRect,
                                     PaletteState state,
                                     bool rtl);

        /// <summary>
        /// Draw a solid area glyph suitable for a drag drop area.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="drawRect">Drawing rectangle space.</param>
        /// <param name="dragDropPalette">Palette source of drawing values.</param>
        void DrawDragDropSolidGlyph(RenderContext context,
                                    Rectangle drawRect,
                                    IPaletteDragDrop dragDropPalette);

        /// <summary>
        /// Measure the drag and drop docking glyphs.
        /// </summary>
        /// <param name="dragData">Set of drag docking data.</param>
        /// <param name="dragDropPalette">Palette source of drawing values.</param>
        /// <param name="feedback">Feedback requested.</param>
        void MeasureDragDropDockingGlyph(RenderDragDockingData dragData,
                                         IPaletteDragDrop dragDropPalette,
                                         PaletteDragFeedback feedback);

        /// <summary>
        /// Draw a solid area glyph suitable for a drag drop area.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="dragData">Set of drag docking data.</param>
        /// <param name="dragDropPalette">Palette source of drawing values.</param>
        /// <param name="feedback">Feedback requested.</param>
        void DrawDragDropDockingGlyph(RenderContext context,
                                      RenderDragDockingData dragData,
                                      IPaletteDragDrop dragDropPalette,
                                      PaletteDragFeedback feedback);

        /// <summary>
        /// Draw the track bar ticks glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="state">Element state.</param>
        /// <param name="elementPalette">Source of palette colors.</param>
        /// <param name="drawRect">Drawing rectangle that should contain ticks.</param>
        /// <param name="orientation">Orientation of the drawing area.</param>
        /// <param name="topRight">Drawing on the topRight or the bottomLeft.</param>
        /// <param name="positionSize">Size of the position indicator.</param>
        /// <param name="minimum">First value.</param>
        /// <param name="maximum">Last value.</param>
        /// <param name="frequency">How often ticks are drawn.</param>
        void DrawTrackTicksGlyph(RenderContext context,
                                 PaletteState state,
                                 IPaletteElementColor elementPalette,
                                 Rectangle drawRect,
                                 Orientation orientation,
                                 bool topRight,
                                 Size positionSize,
                                 int minimum,
                                 int maximum,
                                 int frequency);

        /// <summary>
        /// Draw the track bar track glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="state">Element state.</param>
        /// <param name="elementPalette">Source of palette colors.</param>
        /// <param name="drawRect">Drawing rectangle that should contain the track.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="volumeControl">Drawing as a volume control or standard slider.</param>
        void DrawTrackGlyph(RenderContext context,
                            PaletteState state,
                            IPaletteElementColor elementPalette,
                            Rectangle drawRect,
                            Orientation orientation,
                            bool volumeControl);

        /// <summary>
        /// Draw the track bar position glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="state">Element state.</param>
        /// <param name="elementPalette">Source of palette colors.</param>
        /// <param name="drawRect">Drawing rectangle that should contain the track.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="tickStyle">Tick marks that surround the position.</param>
        void DrawTrackPositionGlyph(RenderContext context,
                                    PaletteState state,
                                    IPaletteElementColor elementPalette,
                                    Rectangle drawRect,
                                    Orientation orientation,
                                    TickStyle tickStyle);
    }
    #endregion

	#region Enum RenderMode
	/// <summary>
	/// Specifies the renderer to use when painting.
	/// </summary>
	public enum RendererMode
	{
        /// <summary>
        /// Specifies the renderer be inherited from the base palette.
        /// </summary>
        Inherit,

        /// <summary>
        /// Specifies the RenderSparkle be used.
        /// </summary>
        Sparkle,

        /// <summary>
        /// Specifies the RenderOffice2007 be used.
        /// </summary>
        Office2007,

        /// <summary>
		/// Specifies the RenderProfessional be used.
		/// </summary>
		Professional,

        /// <summary>
        /// Specifies the RenderStandard be used.
        /// </summary>
        Standard,

        /// <summary>
		/// Specifies a custom renderer be used.
		/// </summary>
		Custom
	}
    #endregion
}
