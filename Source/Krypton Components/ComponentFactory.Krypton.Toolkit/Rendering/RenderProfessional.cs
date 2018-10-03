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
	/// Extends the standard renderer to provide Professional style additions.
	/// </summary>
    public class RenderProfessional : RenderStandard
    {
        #region Static Fields
        private static readonly int _grabSquareLength = 2;
        private static readonly int _grabSquareOffset = 1;
        private static readonly int _grabSquareTotal = 3;
        private static readonly int _grabSquareGap = 1;
        private static readonly int _grabSquareMinSpace = 5;
        private static readonly int _grabSquareCount = 5;
        private static readonly Color _grabHandleLight = Color.FromArgb(228, 255, 255, 255);
        private static readonly Color _grabHandleDark = Color.FromArgb(144, 0, 0, 0);
        #endregion

        #region RenderGlyph Overrides
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
        public override void DrawSeparator(RenderContext context,
                                           Rectangle displayRect,
                                           IPaletteBack paletteBack,
                                           IPaletteBorder paletteBorder,
                                           Orientation orientation,
                                           PaletteState state,
                                           bool canMove)
        {
            // Let base class perform standard processing
            base.DrawSeparator(context,
                               displayRect,
                               paletteBack,
                               paletteBorder,
                               orientation,
                               state,
                               canMove);

            // If we are drawing the background then draw grab handles on top
            if (paletteBack.GetBackDraw(state) == InheritBool.True)
            {
                // Only draw grab handle if the user can move the separator
                if (canMove)
                    DrawGrabHandleGlyph(context, displayRect, orientation, state);
            }
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawGrabHandleGlyph(RenderContext context,
                                                   Rectangle displayRect,
                                                   Orientation orientation,
                                                   PaletteState state)
        {
            // Is there enough room to draw the at least one grab handle?
            if ((displayRect.Height >= _grabSquareMinSpace) && (displayRect.Width >= _grabSquareMinSpace))
            {
                // Reduce rectangle to remove the border around the display area edges
                displayRect.Inflate(-_grabSquareGap, -_grabSquareGap);

                // Find how much space is available for drawing grab handles in the orientation
                int orientationSpace = (orientation == Orientation.Horizontal ? displayRect.Width : displayRect.Height);

                // Try to display the maximum allowed number of handles, but show less if not possible
                for (int i = _grabSquareCount; i > 0; i--)
                {
                    // Calculate how much space this number of grab handles takes up
                    int requiredSpace = (i * _grabSquareTotal) + (i > 1 ? (i - 1) * _grabSquareGap : 0);

                    // Is there enough space all the grab handles?
                    if (requiredSpace <= orientationSpace)
                    {
                        // Find offset before showing the first handle
                        int offset = (orientationSpace - requiredSpace) / 2;

                        Point draw;

                        // Find location of first handle
                        if (orientation == Orientation.Horizontal)
                            draw = new Point(displayRect.X + offset, displayRect.Y + (displayRect.Height - _grabSquareTotal) / 2);
                        else
                            draw = new Point(displayRect.X + (displayRect.Width - _grabSquareTotal) / 2, displayRect.Y + offset);

                        using (Brush lightBrush = new SolidBrush(_grabHandleLight),
                                     darkBrush = new SolidBrush(_grabHandleDark))
                        {
                            // Draw each grab handle in turn
                            for (int j = 0; j < i; j++)
                            {
                                // Draw the light colored square 
                                context.Graphics.FillRectangle(lightBrush,
                                                               draw.X + _grabSquareOffset,
                                                               draw.Y + _grabSquareOffset,
                                                               _grabSquareLength,
                                                               _grabSquareLength);

                                // Draw the dark colored square overlapping the dark
                                context.Graphics.FillRectangle(darkBrush,
                                                               draw.X,
                                                               draw.Y,
                                                               _grabSquareLength,
                                                               _grabSquareLength);

                                // Move to the next handle position
                                if (orientation == Orientation.Horizontal)
                                    draw.X += _grabSquareTotal + _grabSquareGap;
                                else
                                    draw.Y += _grabSquareTotal + _grabSquareGap;
                            }
                        }

                        // Finished
                        break;
                    }
                }
            }
        }

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
