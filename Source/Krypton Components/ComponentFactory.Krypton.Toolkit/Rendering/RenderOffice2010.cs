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
	/// Extends the professional renderer to provide Office2010 style additions.
	/// </summary>
    public class RenderOffice2010 : RenderProfessional
    {
        #region Static Fields
        private static readonly float BORDER_PERCENT = 0.6f;
        private static readonly float WHITE_PERCENT = 0.4f;
        private static readonly Blend _ribbonGroup5Blend;
        private static readonly Blend _ribbonGroup6Blend;
        private static readonly Blend _ribbonGroup7Blend;
        #endregion

        #region Identity
        static RenderOffice2010()
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
            KryptonOffice2010Renderer renderer = new KryptonOffice2010Renderer(colorPalette.ColorTable);

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
                MementoRibbonTabContextOffice2010 cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabContextOffice2010))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabContextOffice2010(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabContextOffice2010)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.borderOuterPen = new Pen(c1);
                    cache.borderInnerPen = new Pen(CommonHelper.MergeColors(Color.Black, 0.1f, c2, 0.9f));
                    cache.topBrush = new SolidBrush(c2);
                    Color lightC2 = ControlPaint.Light(c2);
                    cache.bottomBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y, rect.Width + 2, rect.Height + 1),
                                                                Color.FromArgb(128, lightC2), Color.FromArgb(64, lightC2), 90f);
                }

                // Draw the left and right borders
                context.Graphics.DrawLine(cache.borderOuterPen, rect.X, rect.Y, rect.X, rect.Bottom);
                context.Graphics.DrawLine(cache.borderInnerPen, rect.X + 1, rect.Y, rect.X + 1, rect.Bottom - 1);
                context.Graphics.DrawLine(cache.borderOuterPen, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 1);
                context.Graphics.DrawLine(cache.borderInnerPen, rect.Right - 2, rect.Y, rect.Right - 2, rect.Bottom - 1);
            
                // Draw the solid block of colour at the top
                context.Graphics.FillRectangle(cache.topBrush, rect.X + 2, rect.Y, rect.Width - 4, 4);

                // Draw the gradient to the bottom
                context.Graphics.FillRectangle(cache.bottomBrush, rect.X + 2, rect.Y + 4, rect.Width - 4, rect.Height - 4);
            }

            return memento;
        }
        #endregion
    }
}
