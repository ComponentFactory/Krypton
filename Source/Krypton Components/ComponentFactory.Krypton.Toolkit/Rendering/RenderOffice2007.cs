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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Extends the professional renderer to provide Office2007 style additions.
	/// </summary>
    public class RenderOffice2007 : RenderProfessional
    {
        #region Static Fields
        private static readonly float BORDER_PERCENT = 0.6f;
        private static readonly float WHITE_PERCENT = 0.4f;
        private static readonly Blend _ribbonGroup5Blend;
        private static readonly Blend _ribbonGroup6Blend;
        private static readonly Blend _ribbonGroup7Blend;
        #endregion

        #region Identity
        static RenderOffice2007()
        {
            _ribbonGroup5Blend = new Blend();
            _ribbonGroup5Blend.Factors = new float[] { 0.0f, 0.0f, 1.0f };
            _ribbonGroup5Blend.Positions = new float[] { 0.0f, 0.5f, 1.0f };

            _ribbonGroup6Blend = new Blend();
            _ribbonGroup6Blend.Factors = new float[] { 0.0f, 0.0f, 0.75f, 1.0f };
            _ribbonGroup6Blend.Positions = new float[] { 0.0f, 0.1f, 0.45f, 1.0f };

            _ribbonGroup7Blend = new Blend();
            _ribbonGroup7Blend.Factors = new float[] { 0.0f, 1.0f, 1.0f, 0.0f };
            _ribbonGroup7Blend.Positions = new float[] { 0.0f, 0.15f, 0.85f, 1.0f };
        }
        #endregion

        #region RenderRibbon Overrides
        /// <summary>
        /// Perform drawing of a ribbon cluster edge.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteBack">Palette used for recovering drawing details.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawRibbonClusterEdge(PaletteRibbonShape shape,
                                                   RenderContext context,
                                                   Rectangle displayRect,
                                                   IPaletteBack paletteBack,
                                                   PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteBack != null);

            // Get the first border color
            Color borderColor = paletteBack.GetBackColor1(state);

            // We want to lighten it by merging with white
            Color lightColor = CommonHelper.MergeColors(borderColor, BORDER_PERCENT,
                                                        Color.White, WHITE_PERCENT);

            // Draw inside of the border edge in a lighter version of the border
            using (SolidBrush drawBrush = new SolidBrush(lightColor))
                context.Graphics.FillRectangle(drawBrush, displayRect);
        }

        #endregion

        #region IRenderer Overrides
        /// <summary>
        /// Gets a renderer for drawing the toolstrips.
        /// </summary>
        /// <param name="colorPalette">Color palette to use when rendering toolstrip.</param>
        public override ToolStripRenderer RenderToolStrip(IPalette colorPalette)
        {
            Debug.Assert(colorPalette != null);

            // Validate incoming parameter
            if (colorPalette == null) throw new ArgumentNullException("colorPalette");

            // Use the professional renderer but pull colors from the palette
            KryptonOffice2007Renderer renderer = new KryptonOffice2007Renderer(colorPalette.ColorTable);

            // Seup the need to use rounded corners
            renderer.RoundedEdges = (colorPalette.ColorTable.UseRoundedEdges != InheritBool.False);

            return renderer;
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected override IDisposable DrawRibbonTabContext(RenderContext context,
                                                            Rectangle rect,
                                                            IPaletteRibbonGeneral paletteGeneral,
                                                            IPaletteRibbonBack paletteBack,
                                                            IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = paletteGeneral.GetRibbonTabSeparatorContextColor(PaletteState.Normal);
                Color c2 = paletteBack.GetRibbonBackColor5(PaletteState.ContextCheckedNormal);

                bool generate = true;
                MementoRibbonTabContextOffice cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabContextOffice))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabContextOffice(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabContextOffice)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    Rectangle borderRect = new Rectangle(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2);
                    cache.fillRect = new Rectangle(rect.X + 1, rect.Y, rect.Width - 2, rect.Height - 1);

                    LinearGradientBrush borderBrush = new LinearGradientBrush(borderRect, c1, Color.Transparent, 270f);
                    borderBrush.Blend = _ribbonGroup5Blend;
                    cache.borderPen = new Pen(borderBrush);

                    LinearGradientBrush underlineBrush = new LinearGradientBrush(borderRect, Color.Transparent, Color.FromArgb(200, c2), 0f);
                    underlineBrush.Blend = _ribbonGroup7Blend;
                    cache.underlinePen = new Pen(underlineBrush);

                    cache.fillBrush = new LinearGradientBrush(borderRect, Color.FromArgb(106, c2), Color.Transparent, 270f);
                    cache.fillBrush.Blend = _ribbonGroup6Blend;
                }

                // Draw the left and right border lines
                context.Graphics.DrawLine(cache.borderPen, rect.X, rect.Y, rect.X, rect.Bottom - 1);
                context.Graphics.DrawLine(cache.borderPen, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 1);

                // Fill the inner area with a gradient context specific color
                context.Graphics.FillRectangle(cache.fillBrush, cache.fillRect);

                // Overdraw the brighter line at bottom
                context.Graphics.DrawLine(cache.underlinePen, rect.X + 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
            }

            return memento;
        }
        #endregion
    }
}
