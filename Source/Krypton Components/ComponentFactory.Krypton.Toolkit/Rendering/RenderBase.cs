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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Provides base class for rendering implementations.
	/// </summary>
	[ToolboxItem(false)]
	public abstract class RenderBase : Component,
									   IRenderer,
									   IRenderBorder,
									   IRenderBack,
									   IRenderContent,
                                       IRenderTabBorder,
                                       IRenderRibbon,
                                       IRenderGlyph
	{
		#region Static Fields
        private static object _threadLock = new object();

        private static readonly ColorMatrix _matrixGrayScale = new ColorMatrix(new float[][]{new float[]{0.3f,0.3f,0.3f,0,0},
															                                 new float[]{0.59f,0.59f,0.59f,0,0},
															                                 new float[]{0.11f,0.11f,0.11f,0,0},
															                                 new float[]{0,0,0,1,0},
															                                 new float[]{0,0,0,0,1}});

        private static readonly ColorMatrix _matrixGrayScaleRed = new ColorMatrix(new float[][]{new float[]{1,0,0,0,0},
																                                new float[]{0,0.59f,0.59f,0,0},
																                                new float[]{0,0.11f,0.11f,0,0},
																                                new float[]{0,0,0,1,0},
																                                new float[]{0,0,0,0,1}});

        private static readonly ColorMatrix _matrixGrayScaleGreen = new ColorMatrix(new float[][]{new float[]{0.3f,0,0.3f,0,0},
															                                      new float[]{0,1,0,0,0},
															                                      new float[]{0.11f,0,0.11f,0,0},
															                                      new float[]{0,0,0,1,0},
															                                      new float[]{0,0,0,0,1}});

        private static readonly ColorMatrix _matrixGrayScaleBlue = new ColorMatrix(new float[][]{new float[]{0.3f,0.3f,0,0,0},
																                                 new float[]{0.59f,0.59f,0,0,0},
																                                 new float[]{0,0,1,0,0},
																                                 new float[]{0,0,0,1,0},
																                                 new float[]{0,0,0,0,1}});

        private static readonly ColorMatrix _matrixLight = new ColorMatrix(new float[][]{new float[]{1,0,0,0,0},
													                                     new float[]{0,1,0,0,0},
													                                     new float[]{0,0,1,0,0},
													                                     new float[]{0,0,0,1,0},
													                                     new float[]{0.1f,0.1f,0.1f,0,1}});

        private static readonly ColorMatrix _matrixLightLight = new ColorMatrix(new float[][]{new float[]{1,0,0,0,0},
														                                      new float[]{0,1,0,0,0},
														                                      new float[]{0,0,1,0,0},
														                                      new float[]{0,0,0,1,0},
														                                      new float[]{0.2f,0.2f,0.2f,0,1}});

        private static readonly ColorMatrix _matrixDark = new ColorMatrix(new float[][]{new float[]{1,0,0,0,0},
													                                    new float[]{0,1,0,0,0},
													                                    new float[]{0,0,1,0,0},
													                                    new float[]{0,0,0,1,0},
													                                    new float[]{-0.1f,-0.1f,-0.1f,0,1}});

        private static readonly ColorMatrix _matrixDarkDark = new ColorMatrix(new float[][]{new float[]{1,0,0,0,0},
														                                    new float[]{0,1,0,0,0},
														                                    new float[]{0,0,1,0,0},
														                                    new float[]{0,0,0,1,0},
														                                    new float[]{-0.25f,-0.25f,-0.25f,0,1}});
		#endregion

		#region IRenderer
		/// <summary>
		/// Gets the standard border renderer.
		/// </summary>
        public IRenderBorder RenderStandardBorder 
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return this; }
		}

		/// <summary>
        /// Gets the standard background renderer.
		/// </summary>
        public IRenderBack RenderStandardBack
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return this; }
		}

		/// <summary>
        /// Gets the standard content renderer.
		/// </summary>
        public IRenderContent RenderStandardContent
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return this; }
		}

        /// <summary>
        /// Gets the tab border renderer.
        /// </summary>
        public IRenderTabBorder RenderTabBorder
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return this; }
		}

        /// <summary>
        /// Gets the ribbon renderer.
        /// </summary>
        public IRenderRibbon RenderRibbon
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this; }
        }

        /// <summary>
        /// Gets the glpyh renderer.
        /// </summary>
        public IRenderGlyph RenderGlyph 
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return this; }
        }

        /// <summary>
        /// Gets a renderer for drawing the toolstrips.
        /// </summary>
        /// <param name="colorPalette">Color palette to use when rendering toolstrip.</param>
        public abstract ToolStripRenderer RenderToolStrip(IPalette colorPalette);
        #endregion

		#region RenderStandardBorder
        /// <summary>
        /// Gets the raw padding used per edge of the border.
        /// </summary>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <returns>Padding structure detailing all four edges.</returns>
        public abstract Padding GetBorderRawPadding(IPaletteBorder palette,
                                                    PaletteState state,
                                                    VisualOrientation orientation);

        /// <summary>
		/// Gets the padding used to position display elements completely inside border drawing.
		/// </summary>
		/// <param name="palette">Palette used for drawing.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <returns>Padding structure detailing all four edges.</returns>
		public abstract Padding GetBorderDisplayPadding(IPaletteBorder palette,
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
        public abstract GraphicsPath GetOutsideBorderPath(RenderContext context,
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
		public abstract GraphicsPath GetBorderPath(RenderContext context, 
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
        public abstract GraphicsPath GetBackPath(RenderContext context,
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
		public abstract void DrawBorder(RenderContext context, 
										Rectangle rect, 
										IPaletteBorder palette,
										VisualOrientation orientation,
										PaletteState state);
		#endregion

		#region RenderStandardBack
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
        public abstract IDisposable DrawBack(RenderContext context, 
									         Rectangle rect,
									         GraphicsPath path, 
									         IPaletteBack palette,
									         VisualOrientation orientation,
									         PaletteState state,
                                             IDisposable memento);
		#endregion

		#region RenderStandardContent
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
		public abstract Size GetContentPreferredSize(ViewLayoutContext context,
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
		public abstract IDisposable LayoutContent(ViewLayoutContext context,
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
        public abstract void DrawContent(RenderContext context,
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
        public abstract bool GetContentImageDisplayed(IDisposable memento);

        /// <summary>
        /// Request the calculated position of the content image.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>Display rectangle for the image content.</returns>
        public abstract Rectangle GetContentImageRectangle(IDisposable memento);

        /// <summary>
        /// Request the calculated display of the short text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>True if the short text is being displayed; otherwise false.</returns>
        public abstract bool GetContentShortTextDisplayed(IDisposable memento);

        /// <summary>
        /// Request the calculated position of the content short text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>Display rectangle for the image content.</returns>
        public abstract Rectangle GetContentShortTextRectangle(IDisposable memento);

        /// <summary>
        /// Request the calculated display of the long text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>True if the long text is being displayed; otherwise false.</returns>
        public abstract bool GetContentLongTextDisplayed(IDisposable memento);

        /// <summary>
        /// Request the calculated position of the content long text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>Display rectangle for the image content.</returns>
        public abstract Rectangle GetContentLongTextRectangle(IDisposable memento);
        #endregion

        #region RenderTabBorder
        /// <summary>
        /// Gets if the tabs should be drawn from left to right for z-ordering.
        /// </summary>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>True for left to right, otherwise draw right to left.</returns>
        public abstract bool GetTabBorderLeftDrawing(TabBorderStyle tabBorderStyle);

        /// <summary>
        /// Gets the spacing used to separate each tab border instance.
        /// </summary>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>Number of pixels to space instances.</returns>
        public abstract int GetTabBorderSpacingGap(TabBorderStyle tabBorderStyle);

        /// <summary>
        /// Gets the padding used to position display elements completely inside border drawing.
        /// </summary>
        /// <param name="context">Layout context.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>Padding structure detailing all four edges.</returns>
        public abstract Padding GetTabBorderDisplayPadding(ViewLayoutContext context,
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
        public abstract GraphicsPath GetTabBorderPath(RenderContext context,
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
        public abstract GraphicsPath GetTabBackPath(RenderContext context,
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
        public abstract void DrawTabBorder(RenderContext context,
                                           Rectangle rect,
                                           IPaletteBorder palette,
                                           VisualOrientation orientation,
                                           PaletteState state,
                                           TabBorderStyle tabBorderStyle);
        #endregion

        #region RenderRibbon
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
        public abstract IDisposable DrawRibbonBack(PaletteRibbonShape shape,
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
        public abstract IDisposable DrawRibbonTabContextTitle(PaletteRibbonShape shape,
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
        /// <param name="memento">Cached storage for drawing objects.</param>
        public abstract IDisposable DrawRibbonApplicationButton(PaletteRibbonShape shape,
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
        public abstract IDisposable DrawRibbonApplicationTab(PaletteRibbonShape shape,
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
        public abstract void DrawRibbonClusterEdge(PaletteRibbonShape shape,
                                                   RenderContext context,
                                                   Rectangle displayRect,
                                                   IPaletteBack paletteBack,
                                                   PaletteState state);
        #endregion

        #region RenderGlyph
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
        public abstract void DrawSeparator(RenderContext context,
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
        public abstract Size GetCheckBoxPreferredSize(ViewLayoutContext context,
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
        public abstract void DrawCheckBox(RenderContext context,
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
        public abstract Size GetRadioButtonPreferredSize(ViewLayoutContext context,
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
        public abstract void DrawRadioButton(RenderContext context,
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
        public abstract Size GetDropDownButtonPreferredSize(ViewLayoutContext context,
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
        public abstract void DrawDropDownButton(RenderContext context,
                                                Rectangle displayRect,
                                                IPalette palette,
                                                PaletteState state,
                                                VisualOrientation orientation);

        /// <summary>
        /// Draw a numeric up button image appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        public abstract void DrawInputControlNumericUpGlyph(RenderContext context,
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
        public abstract void DrawInputControlNumericDownGlyph(RenderContext context,
                                                              Rectangle cellRect,
                                                              IPaletteContent paletteContent,
                                                              PaletteState state);

        /// <summary>
        /// Draw a drop down grid appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        public abstract void DrawInputControlDropDownGlyph(RenderContext context,
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
        public abstract void DrawRibbonDialogBoxLauncher(PaletteRibbonShape shape,
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
        public abstract void DrawRibbonDropArrow(PaletteRibbonShape shape,
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
        public abstract void DrawRibbonContextArrow(PaletteRibbonShape shape,
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
        public abstract void DrawRibbonOverflow(PaletteRibbonShape shape,
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
        public abstract void DrawRibbonGroupSeparator(PaletteRibbonShape shape,
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
        public abstract Rectangle DrawGridSortGlyph(RenderContext context,
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
        public abstract Rectangle DrawGridRowGlyph(RenderContext context,
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
        public abstract Rectangle DrawGridErrorGlyph(RenderContext context,
                                                     Rectangle cellRect,
                                                     PaletteState state,
                                                     bool rtl);

        /// <summary>
        /// Draw a solid area glyph suitable for a drag drop area.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="drawRect">Drawing rectangle space.</param>
        /// <param name="dragDropPalette">Palette source of drawing values.</param>
        public abstract void DrawDragDropSolidGlyph(RenderContext context,
                                                    Rectangle drawRect,
                                                    IPaletteDragDrop dragDropPalette);

        /// <summary>
        /// Measure the drag and drop docking glyphs.
        /// </summary>
        /// <param name="dragData">Set of drag docking data.</param>
        /// <param name="dragDropPalette">Palette source of drawing values.</param>
        /// <param name="feedback">Feedback requested.</param>
        public abstract void MeasureDragDropDockingGlyph(RenderDragDockingData dragData,
                                                         IPaletteDragDrop dragDropPalette,
                                                         PaletteDragFeedback feedback);

        /// <summary>
        /// Draw a solid area glyph suitable for a drag drop area.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="dragData">Set of drag docking data.</param>
        /// <param name="dragDropPalette">Palette source of drawing values.</param>
        /// <param name="feedback">Feedback requested.</param>
        public abstract void DrawDragDropDockingGlyph(RenderContext context,
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
        public abstract void DrawTrackTicksGlyph(RenderContext context,
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
        public abstract void DrawTrackGlyph(RenderContext context,
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
        public abstract void DrawTrackPositionGlyph(RenderContext context,
                                                    PaletteState state,
                                                    IPaletteElementColor elementPalette,
                                                    Rectangle drawRect,
                                                    Orientation orientation,
                                                    TickStyle tickStyle);
        #endregion

        #region EvalTransparentPaint
        /// <summary>
        /// Evaluate if transparent painting is needed for background palette.
        /// </summary>
        /// <param name="paletteBack">Background palette to test.</param>
        /// <param name="state">Element state associated with palette.</param>
        /// <returns>True if transparent painting required.</returns>
        public abstract bool EvalTransparentPaint(IPaletteBack paletteBack,
                                                  PaletteState state);

        /// <summary>
        /// Evaluate if transparent painting is needed for background or border palettes.
        /// </summary>
        /// <param name="paletteBack">Background palette to test.</param>
        /// <param name="paletteBorder">Background palette to test.</param>
        /// <param name="state">Element state associated with palette.</param>
        /// <returns>True if transparent painting required.</returns>
        public abstract bool EvalTransparentPaint(IPaletteBack paletteBack,
                                                  IPaletteBorder paletteBorder,
                                                  PaletteState state);
        #endregion

        #region DrawIconHelper
        /// <summary>
		/// Helper routine to draw an image taking into account various properties.
		/// </summary>
		/// <param name="context">Rendering context.</param>
		/// <param name="icon">Icon to be drawn.</param>
        /// <param name="iconRect">Destination rectangle.</param>
		/// <param name="orientation">Visual orientation.</param>
        protected static void DrawIconHelper(ViewContext context,
								             Icon icon,
								             Rectangle iconRect,
								             VisualOrientation orientation)
		{
            Debug.Assert(context != null);

            // Validate reference parameter
            if (context == null) throw new ArgumentNullException("context");

            try
            {
                // Finally, just draw the icon and let the transforms do the rest
                context.Graphics.DrawIcon(icon, iconRect);
            }
            finally
            {
            }
        }
		#endregion

        #region DrawImageHelper
        /// <summary>
		/// Helper routine to draw an image taking into account various properties.
		/// </summary>
		/// <param name="context">Rendering context.</param>
		/// <param name="image">Image to be drawn.</param>
        /// <param name="remapTransparent">Color that should become transparent.</param>
        /// <param name="imageRect">Destination rectangle.</param>
		/// <param name="orientation">Visual orientation.</param>
		/// <param name="effect">Drawing effect.</param>
        /// <param name="remapColor">Image color to remap.</param>
        /// <param name="remapNew">New color for remap.</param>
        protected static void DrawImageHelper(ViewContext context,
									          Image image,
                                              Color remapTransparent,
									          Rectangle imageRect,
									          VisualOrientation orientation,
									          PaletteImageEffect effect,
                                              Color remapColor,
                                              Color remapNew)
		{
            Debug.Assert(context != null);

            // Prevent problems with multiple threads using the same palette images 
            // by only allowing a single thread to draw the provided image at a time
            lock (_threadLock)
            {
                // Validate reference parameter
                if (context == null) throw new ArgumentNullException("context");

                // Use image attributes class to modify image drawing for effects
                ImageAttributes attribs = new ImageAttributes();

                switch (effect)
                {
                    case PaletteImageEffect.Disabled:
                        attribs.SetColorMatrix(CommonHelper.MatrixDisabled);
                        break;
                    case PaletteImageEffect.GrayScale:
                        attribs.SetColorMatrix(_matrixGrayScale, ColorMatrixFlag.SkipGrays);
                        break;
                    case PaletteImageEffect.GrayScaleRed:
                        attribs.SetColorMatrix(_matrixGrayScaleRed, ColorMatrixFlag.SkipGrays);
                        break;
                    case PaletteImageEffect.GrayScaleGreen:
                        attribs.SetColorMatrix(_matrixGrayScaleGreen, ColorMatrixFlag.SkipGrays);
                        break;
                    case PaletteImageEffect.GrayScaleBlue:
                        attribs.SetColorMatrix(_matrixGrayScaleBlue, ColorMatrixFlag.SkipGrays);
                        break;
                    case PaletteImageEffect.Light:
                        attribs.SetColorMatrix(_matrixLight);
                        break;
                    case PaletteImageEffect.LightLight:
                        attribs.SetColorMatrix(_matrixLightLight);
                        break;
                    case PaletteImageEffect.Dark:
                        attribs.SetColorMatrix(_matrixDark);
                        break;
                    case PaletteImageEffect.DarkDark:
                        attribs.SetColorMatrix(_matrixDarkDark);
                        break;
                    case PaletteImageEffect.Inherit:
                        // Should never happen!
                        Debug.Assert(false);
                        break;
                }

                // Do we need to remap a colors in the bitmap?
                if ((remapTransparent != Color.Empty) ||
                    ((remapColor != Color.Empty) && (remapNew != Color.Empty)))
                {
                    List<ColorMap> colorMaps = new List<ColorMap>();

                    // Create remapping for the transparent color
                    if (remapTransparent != Color.Empty)
                    {
                        ColorMap remap = new ColorMap();
                        remap.OldColor = remapTransparent;
                        remap.NewColor = Color.Transparent;
                        colorMaps.Add(remap);
                    }

                    // Create remapping from source to target colors
                    if ((remapColor != Color.Empty) && (remapNew != Color.Empty))
                    {
                        ColorMap remap = new ColorMap();
                        remap.OldColor = remapColor;
                        remap.NewColor = remapNew;
                        colorMaps.Add(remap);
                    }

                    attribs.SetRemapTable(colorMaps.ToArray(), ColorAdjustType.Bitmap);
                }

                int translateX = 0;
                int translateY = 0;
                float rotation = 0f;

                // Perform any transformations needed for orientation
                switch (orientation)
                {
                    case VisualOrientation.Bottom:
                        // Translate to opposite side of origin, so the rotate can 
                        // then bring it back to original position but mirror image
                        translateX = imageRect.X * 2 + imageRect.Width;
                        translateY = imageRect.Y * 2 + imageRect.Height;
                        rotation = 180f;
                        break;
                    case VisualOrientation.Left:
                        // Invert the dimensions of the rectangle for drawing upwards
                        imageRect = new Rectangle(imageRect.X, imageRect.Y, imageRect.Height, imageRect.Width);

                        // Translate back from a quarter left turn to the original place 
                        translateX = imageRect.X - imageRect.Y;
                        translateY = imageRect.X + imageRect.Y + imageRect.Width;
                        rotation = -90f;
                        break;
                    case VisualOrientation.Right:
                        // Invert the dimensions of the rectangle for drawing upwards
                        imageRect = new Rectangle(imageRect.X, imageRect.Y, imageRect.Height, imageRect.Width);

                        // Translate back from a quarter right turn to the original place 
                        translateX = imageRect.X + imageRect.Y + imageRect.Height;
                        translateY = -(imageRect.X - imageRect.Y);
                        rotation = 90f;
                        break;
                }

                // Apply the transforms if we have any to apply
                if ((translateX != 0) || (translateY != 0))
                    context.Graphics.TranslateTransform(translateX, translateY);

                if (rotation != 0f)
                    context.Graphics.RotateTransform(rotation);

                try
                {
                    // Finally, just draw the image and let the transforms do the rest
                    context.Graphics.DrawImage(image, imageRect, 0, 0, imageRect.Width, imageRect.Height, GraphicsUnit.Pixel, attribs);
                }
                catch (ArgumentException)
                {
                }
                finally
                {
                    if (rotation != 0f)
                        context.Graphics.RotateTransform(-rotation);

                    // Remove the applied transforms
                    if ((translateX != 0) | (translateY != 0))
                        context.Graphics.TranslateTransform(-translateX, -translateY);
                }
            }
        }
		#endregion
	}
}
