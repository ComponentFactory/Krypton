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
    /// Helper functions for drawing in the glass styles.
    /// </summary>
    internal class RenderGlassHelpers
    {
        #region Static Fields
        private static readonly Color _glassColorTopL = Color.FromArgb(208, Color.White);
        private static readonly Color _glassColorBottomL = Color.FromArgb(96, Color.White);
        private static readonly Color _glassColorTopD = Color.FromArgb(164, Color.White);
        private static readonly Color _glassColorBottomD = Color.FromArgb(64, Color.White);
        private static readonly Color _glassColorLight = Color.FromArgb(96, Color.White);
        private static readonly Color _glassColorTopDD = Color.FromArgb(128, Color.White);
        private static readonly Color _glassColorBottomDD = Color.FromArgb(48, Color.White);
        private static readonly Blend _glassFadeBlend;
        private static readonly float _fullGlassLength = 0.45f;
        private static readonly float _stumpyGlassLength = 0.19f;
        #endregion

        #region Identity
        static RenderGlassHelpers()
        {
            _glassFadeBlend = new Blend();
            _glassFadeBlend.Positions = new float[] { 0.0f, 0.33f, 0.66f, 1.0f };
            _glassFadeBlend.Factors = new float[] { 0.0f, 0.0f, 0.8f, 1.0f };
        }
        #endregion

        #region Static Public
        /// <summary>
        /// Draw a background with glass effect where the fade is from the center.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassCenter(RenderContext context,
                                                      Rectangle rect,
                                                      Color backColor1,
                                                      Color backColor2,
                                                      VisualOrientation orientation,
                                                      GraphicsPath path,
                                                      IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                MementoDouble cache;

                if ((memento == null) || !(memento is MementoDouble))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoDouble();
                    memento = cache;
                }
                else
                    cache = (MementoDouble)memento;

                // Draw the one pixel border around the area
                cache.first = DrawBackLinearRadial(rect, false,
                                                   ControlPaint.LightLight(backColor2),
                                                   ControlPaint.Light(backColor2),
                                                   ControlPaint.LightLight(backColor2),
                                                   orientation, context.Graphics,
                                                   cache.first);

                // Reduce size of the inside area
                rect.Inflate(-1, -1);

                // Draw the inside area as a glass effect
                cache.second = DrawBackGlassCenter(rect, backColor1, backColor2,
                                                   _glassColorTopL, _glassColorBottomL,
                                                   2f, 1f, orientation, context.Graphics,
                                                   _fullGlassLength, cache.second);
            }

            return memento;
        }

        /// <summary>
        /// Draw a background with glass effect where the fade is from the bottom.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassBottom(RenderContext context,
                                                      Rectangle rect,
                                                      Color backColor1,
                                                      Color backColor2,
                                                      VisualOrientation orientation,
                                                      GraphicsPath path,
                                                      IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                MementoDouble cache;

                if ((memento == null) || !(memento is MementoDouble))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoDouble();
                    memento = cache;
                }
                else
                    cache = (MementoDouble)memento;

                // Draw the one pixel border around the area
                cache.first = DrawBackLinear(rect, false,
                                             ControlPaint.Light(backColor1),
                                             ControlPaint.LightLight(backColor1),
                                             orientation, context.Graphics,
                                             cache.first);

                // Reduce size on all but the upper edge
                ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

                // Draw the inside areas as a glass effect
                cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                                                   _glassColorTopD, _glassColorBottomD,
                                                   3f, 1.1f, orientation, context.Graphics,
                                                   _fullGlassLength, cache.second);
            }

            return memento;
        }

        /// <summary>
        /// Draw a background in normal full glass effect but only over 50% of the background.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassFade(RenderContext context,
                                                    Rectangle rect,
                                                    Color backColor1,
                                                    Color backColor2,
                                                    VisualOrientation orientation,
                                                    GraphicsPath path,
                                                    IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                MementoDouble cache;

                if ((memento == null) || !(memento is MementoDouble))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoDouble();
                    memento = cache;
                }
                else
                    cache = (MementoDouble)memento;

                cache.first = DrawBackGlassFade(rect, rect,
                                                backColor1, backColor2,
                                                _glassColorTopL,
                                                _glassColorBottomL,
                                                orientation,
                                                context.Graphics,
                                                cache.first);

                cache.second = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
                                                3, orientation, context.Graphics, 
                                                cache.second);
            }

            return memento;
        }

        /// <summary>
        /// Draw a background in simple glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassSimpleFull(RenderContext context,
                                                          Rectangle rect,
                                                          Color backColor1,
                                                          Color backColor2,
                                                          VisualOrientation orientation,
                                                          GraphicsPath path,
                                                          IDisposable memento)
        {
            return DrawBackGlassSimplePercent(context, rect, 
                                              backColor1, backColor2, 
                                              orientation, path, 
                                              _fullGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in normal full glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassNormalFull(RenderContext context,
                                                          Rectangle rect,
                                                          Color backColor1,
                                                          Color backColor2,
                                                          VisualOrientation orientation,
                                                          GraphicsPath path,
                                                          IDisposable memento)
        {
            return DrawBackGlassNormalPercent(context, rect,
                                              backColor1, backColor2,
                                              orientation, path,
                                              _fullGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in tracking full glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassTrackingFull(RenderContext context,
                                                            Rectangle rect,
                                                            Color backColor1,
                                                            Color backColor2,
                                                            VisualOrientation orientation,
                                                            GraphicsPath path,
                                                            IDisposable memento)
        {
            return DrawBackGlassTrackingPercent(context, rect, 
                                                backColor1, backColor2,
                                                orientation, path,
                                                _fullGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in checked full glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassCheckedFull(RenderContext context,
                                                           Rectangle rect,
                                                           Color backColor1,
                                                           Color backColor2,
                                                           VisualOrientation orientation,
                                                           GraphicsPath path,
                                                           IDisposable memento)
        {
            return DrawBackGlassCheckedPercent(context, rect, 
                                               backColor1, backColor2,
                                               orientation, path, 
                                               _fullGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in checked/tracking full glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassCheckedTrackingFull(RenderContext context,
                                                                   Rectangle rect,
                                                                   Color backColor1,
                                                                   Color backColor2,
                                                                   VisualOrientation orientation,
                                                                   GraphicsPath path,
                                                                   IDisposable memento)
        {
            return DrawBackGlassCheckedTrackingPercent(context, rect, 
                                                       backColor1, backColor2,
                                                       orientation, path, 
                                                       _fullGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in checked/pressed full glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassPressedFull(RenderContext context,
                                                           Rectangle rect,
                                                           Color backColor1,
                                                           Color backColor2,
                                                           VisualOrientation orientation,
                                                           GraphicsPath path,
                                                           IDisposable memento)
        {
            return DrawBackGlassPressedPercent(context, rect, 
                                               backColor1, backColor2,
                                               orientation, path, 
                                               _fullGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in normal stumpy glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassNormalStump(RenderContext context,
                                                           Rectangle rect,
                                                           Color backColor1,
                                                           Color backColor2,
                                                           VisualOrientation orientation,
                                                           GraphicsPath path,
                                                           IDisposable memento)
        {
            return DrawBackGlassNormalPercent(context, rect, 
                                              backColor1, backColor2,
                                              orientation, path, 
                                              _stumpyGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in tracking stumpy glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassTrackingStump(RenderContext context,
                                                             Rectangle rect,
                                                             Color backColor1,
                                                             Color backColor2,
                                                             VisualOrientation orientation,
                                                             GraphicsPath path,
                                                             IDisposable memento)
        {
            return DrawBackGlassTrackingPercent(context, rect, 
                                                backColor1, backColor2,
                                                orientation, path, 
                                                _stumpyGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in checked/pressed stumpy glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassPressedStump(RenderContext context,
                                                            Rectangle rect,
                                                            Color backColor1,
                                                            Color backColor2,
                                                            VisualOrientation orientation,
                                                            GraphicsPath path,
                                                            IDisposable memento)
        {
            return DrawBackGlassPressedPercent(context, rect, 
                                               backColor1, backColor2,
                                               orientation, path, 
                                               _stumpyGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in checked stumpy glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassCheckedStump(RenderContext context,
                                                            Rectangle rect,
                                                            Color backColor1,
                                                            Color backColor2,
                                                            VisualOrientation orientation,
                                                            GraphicsPath path,
                                                            IDisposable memento)
        {
            return DrawBackGlassCheckedPercent(context, rect, 
                                               backColor1, backColor2,
                                               orientation, path, 
                                               _stumpyGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in checked/tracking stumpy glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassCheckedTrackingStump(RenderContext context,
                                                                    Rectangle rect,
                                                                    Color backColor1,
                                                                    Color backColor2,
                                                                    VisualOrientation orientation,
                                                                    GraphicsPath path,
                                                                    IDisposable memento)
        {
            return DrawBackGlassCheckedTrackingPercent(context, rect, 
                                                       backColor1, backColor2,
                                                       orientation, path, 
                                                       _stumpyGlassLength, memento);
        }

        /// <summary>
        /// Draw a background in glass effect with three edges lighter.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassThreeEdge(RenderContext context,
                                                         Rectangle rect,
                                                         Color backColor1,
                                                         Color backColor2,
                                                         VisualOrientation orientation,
                                                         GraphicsPath path,
                                                         IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                bool generate = true;
                MementoBackGlassThreeEdge cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoBackGlassThreeEdge))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoBackGlassThreeEdge(rect, backColor1, backColor2, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoBackGlassThreeEdge)memento;
                    generate = !cache.UseCachedValues(rect, backColor1, backColor2, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // Generate color values
                    cache.colorA1L = CommonHelper.MergeColors(backColor1, 0.7f, Color.White, 0.3f);
                    cache.colorA2L = CommonHelper.MergeColors(backColor2, 0.7f, Color.White, 0.3f);
                    cache.colorA2LL = CommonHelper.MergeColors(cache.colorA2L, 0.8f, Color.White, 0.2f);
                    cache.colorB2LL = CommonHelper.MergeColors(backColor2, 0.8f, Color.White, 0.2f);
                    cache.rectB = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 1, rect.Height - 2);
                }

                // Draw entire area in a lighter version
                cache.first = DrawBackGlassLinear(rect, rect,
                                                  cache.colorA1L, _glassColorLight,
                                                  cache.colorA2L, cache.colorA2LL,
                                                  orientation,
                                                  context.Graphics,
                                                  _fullGlassLength,
                                                  cache.first);

                
                // Draw the inside area in the full color
                cache.second = DrawBackGlassLinear(cache.rectB, cache.rectB,
                                                   backColor1, _glassColorLight,
                                                   backColor2, cache.colorB2LL,
                                                   orientation,
                                                   context.Graphics,
                                                   _fullGlassLength,
                                                   cache.second);

                return cache;
            }
        }

        /// <summary>
        /// Draw a background in normal simple glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassNormalSimple(RenderContext context,
                                                            Rectangle rect,
                                                            Color backColor1,
                                                            Color backColor2,
                                                            VisualOrientation orientation,
                                                            GraphicsPath path, 
                                                            IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw the inside area
                return DrawBackGlassLinear(rect, rect,
                                           backColor1, backColor2,
                                           _glassColorTopL,
                                           _glassColorBottomL,
                                           orientation,
                                           context.Graphics,
                                           _fullGlassLength,
                                           memento);
            }
        }

        /// <summary>
        /// Draw a background in tracking simple glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassTrackingSimple(RenderContext context,
                                                              Rectangle rect,
                                                              Color backColor1,
                                                              Color backColor2,
                                                              VisualOrientation orientation,
                                                              GraphicsPath path,
                                                              IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw the inside area as a glass effect
                return DrawBackGlassRadial(rect, backColor1, backColor2,
                                           _glassColorTopL, _glassColorBottomL,
                                           2f, 1f, orientation, context.Graphics,
                                           _fullGlassLength, memento);
            }
        }

        /// <summary>
        /// Draw a background in checked simple glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassCheckedSimple(RenderContext context,
                                                             Rectangle rect,
                                                             Color backColor1,
                                                             Color backColor2,
                                                             VisualOrientation orientation,
                                                             GraphicsPath path,
                                                             IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw the inside areas as a glass effect
                return DrawBackGlassRadial(rect, backColor1, backColor2,
                                           _glassColorTopL, _glassColorBottomL,
                                           6f, 1.2f, orientation, context.Graphics,
                                           _fullGlassLength, memento);
            }
        }

        /// <summary>
        /// Draw a background in checked/tracking simple glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassCheckedTrackingSimple(RenderContext context,
                                                                     Rectangle rect,
                                                                     Color backColor1,
                                                                     Color backColor2,
                                                                     VisualOrientation orientation,
                                                                     GraphicsPath path,
                                                                     IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw the inside areas as a glass effect
                return DrawBackGlassRadial(rect, backColor1, backColor2,
                                           _glassColorTopD, _glassColorBottomD,
                                           5f, 1.2f, orientation, context.Graphics,
                                           _fullGlassLength, memento);
            }
        }

        /// <summary>
        /// Draw a background in checked/pressed simple glass effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackGlassPressedSimple(RenderContext context,
                                                             Rectangle rect,
                                                             Color backColor1,
                                                             Color backColor2,
                                                             VisualOrientation orientation,
                                                             GraphicsPath path,
                                                             IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw the inside areas as a glass effect
                return DrawBackGlassRadial(rect, backColor1, backColor2,
                                           _glassColorTopD, _glassColorBottomD,
                                           3f, 1.1f, orientation, context.Graphics,
                                           _fullGlassLength, memento);
            }
        }

        #endregion

        #region Implementation
        private static IDisposable DrawBackGlassSimplePercent(RenderContext context,
                                                              Rectangle rect,
                                                              Color backColor1,
                                                              Color backColor2,
                                                              VisualOrientation orientation,
                                                              GraphicsPath path,
                                                              float glassPercent,
                                                              IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                MementoDouble cache;

                if ((memento == null) || !(memento is MementoDouble))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoDouble();
                    memento = cache;
                }
                else
                    cache = (MementoDouble)memento;

                // Get the drawing rectangle for the path
                RectangleF drawRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

                // Draw the border as a lighter version of the inside
                cache.first = DrawBackGlassLinear(drawRect, drawRect,
                                                  backColor2,
                                                  backColor2,
                                                  _glassColorBottomDD,
                                                  _glassColorBottomDD,
                                                  orientation,
                                                  context.Graphics,
                                                  0,
                                                  cache.first);

                // Reduce by 1 pixel on all edges to get the inside
                RectangleF insetRect = drawRect;
                insetRect.Inflate(-1f, -1f);

                // Draw the inside area
                cache.second = DrawBackGlassLinear(insetRect, drawRect,
                                                   backColor1, 
                                                   CommonHelper.MergeColors(backColor1, 0.5f, backColor2, 0.5f),
                                                   _glassColorTopDD,
                                                   _glassColorBottomDD,
                                                   orientation,
                                                   context.Graphics,
                                                   glassPercent,
                                                   cache.second);
            }

            return memento;
        }

        private static IDisposable DrawBackGlassNormalPercent(RenderContext context,
                                                              Rectangle rect,
                                                              Color backColor1,
                                                              Color backColor2,
                                                              VisualOrientation orientation,
                                                              GraphicsPath path,
                                                              float glassPercent,
                                                              IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                MementoDouble cache;

                if ((memento == null) || !(memento is MementoDouble))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoDouble();
                    memento = cache;
                }
                else
                    cache = (MementoDouble)memento;

                // Get the drawing rectangle for the path
                RectangleF drawRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

                // Draw the border as a lighter version of the inside
                cache.first = DrawBackGlassLinear(drawRect, drawRect,
                                                  Color.White,
                                                  Color.White,
                                                  _glassColorTopL,
                                                  _glassColorBottomL,
                                                  orientation,
                                                  context.Graphics,
                                                  glassPercent,
                                                  cache.first);

                // Reduce by 1 pixel on all edges to get the inside
                RectangleF insetRect = drawRect;
                insetRect.Inflate(-1f, -1f);

                // Draw the inside area
                cache.second = DrawBackGlassLinear(insetRect, drawRect,
                                                   backColor1, backColor2,
                                                   _glassColorTopL,
                                                   _glassColorBottomL,
                                                   orientation,
                                                   context.Graphics,
                                                   glassPercent,
                                                   cache.second);
            }

            return memento;
        }

        private static IDisposable DrawBackGlassTrackingPercent(RenderContext context,
                                                                Rectangle rect,
                                                                Color backColor1,
                                                                Color backColor2,
                                                                VisualOrientation orientation,
                                                                GraphicsPath path,
                                                                float glassPercent,
                                                                IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                MementoDouble cache;

                if ((memento == null) || !(memento is MementoDouble))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoDouble();
                    memento = cache;
                }
                else
                    cache = (MementoDouble)memento;

                // Draw the one pixel border around the area
                cache.first = DrawBackLinearRadial(rect, false,
                                                   ControlPaint.LightLight(backColor2),
                                                   ControlPaint.Light(backColor2),
                                                   ControlPaint.LightLight(backColor2),
                                                   orientation, context.Graphics,
                                                   cache.first);

                // Reduce size of the inside area
                rect.Inflate(-1, -1);

                // Draw the inside area as a glass effect
                cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                                                   _glassColorTopL, _glassColorBottomL,
                                                   2f, 1f, orientation, context.Graphics,
                                                   glassPercent, cache.second);
            }

            return memento;
        }

        private static IDisposable DrawBackGlassPressedPercent(RenderContext context,
                                                               Rectangle rect,
                                                               Color backColor1,
                                                               Color backColor2,
                                                               VisualOrientation orientation,
                                                               GraphicsPath path,
                                                               float glassPercent,
                                                               IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                MementoTriple cache;

                if ((memento == null) || !(memento is MementoTriple))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoTriple();
                    memento = cache;
                }
                else
                {
                    cache = (MementoTriple)memento;
                }

                // Draw the one pixel border around the area
                cache.first = DrawBackLinear(rect, false,
                                             ControlPaint.Light(backColor1),
                                             ControlPaint.LightLight(backColor1),
                                             orientation, context.Graphics,
                                             cache.first);

                // Reduce size on all but the upper edge
                ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

                // Draw the inside areas as a glass effect
                cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                                                   _glassColorTopD, _glassColorBottomD,
                                                   3f, 1.1f, orientation, context.Graphics,
                                                   glassPercent, cache.second);

                // Widen back to original
                ModifyRectByEdges(ref rect, -1, 0, -1, 0, orientation);

                cache.third = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
                                               3, orientation, context.Graphics,
                                               cache.third);
            }

            return memento;
        }

        private static IDisposable DrawBackGlassCheckedPercent(RenderContext context,
                                                               Rectangle rect,
                                                               Color backColor1,
                                                               Color backColor2,
                                                               VisualOrientation orientation,
                                                               GraphicsPath path,
                                                               float glassPercent,
                                                               IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                MementoTriple cache;

                if ((memento == null) || !(memento is MementoTriple))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoTriple();
                    memento = cache;
                }
                else
                {
                    cache = (MementoTriple)memento;
                }

                // Draw the one pixel border around the area
                cache.first = DrawBackLinearRadial(rect, false,
                                                   ControlPaint.Light(backColor1),
                                                   ControlPaint.LightLight(backColor1),
                                                   ControlPaint.LightLight(backColor1),
                                                   orientation, context.Graphics,
                                                   cache.first);

                // Reduce size on all but the upper edge
                ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

                // Draw the inside areas as a glass effect
                cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                                                   _glassColorTopL, _glassColorBottomL,
                                                   6f, 1.2f, orientation, context.Graphics,
                                                   glassPercent, cache.second);

                // Widen back to original
                ModifyRectByEdges(ref rect, -1, 0, -1, 0, orientation);

                // Draw a darker area for top edge
                cache.third = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
                                               3, orientation, context.Graphics,
                                               cache.third);
            }

            return memento;
        }

        private static IDisposable DrawBackGlassCheckedTrackingPercent(RenderContext context,
                                                                       Rectangle rect,
                                                                       Color backColor1,
                                                                       Color backColor2,
                                                                       VisualOrientation orientation,
                                                                       GraphicsPath path,
                                                                       float glassPercent,
                                                                       IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                MementoTriple cache;

                if ((memento == null) || !(memento is MementoTriple))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoTriple();
                    memento = cache;
                }
                else
                {
                    cache = (MementoTriple)memento;
                }

                // Draw the one pixel border around the area
                cache.first = DrawBackLinear(rect, true,
                                             backColor2,
                                             ControlPaint.LightLight(backColor2),
                                             orientation,
                                             context.Graphics,
                                             cache.first);

                // Reduce size on all but the upper edge
                ModifyRectByEdges(ref rect, 1, 0, 1, 1, orientation);

                // Draw the inside areas as a glass effect
                cache.second = DrawBackGlassRadial(rect, backColor1, backColor2,
                                                   _glassColorTopD, _glassColorBottomD,
                                                   5f, 1.2f, orientation, context.Graphics,
                                                   glassPercent, cache.second);

                // Widen back to original
                ModifyRectByEdges(ref rect, -1, 0, -1, 0, orientation);

                cache.third = DrawBackDarkEdge(rect, ControlPaint.Dark(backColor1),
                                               3, orientation, context.Graphics,
                                               cache.third);
            }

            return memento;
        }

        private static IDisposable DrawBackLinearRadial(RectangleF drawRect,
                                                        bool sigma,
                                                        Color color1,
                                                        Color color2,
                                                        Color color3,
                                                        VisualOrientation orientation,
                                                        Graphics g,
                                                        IDisposable memento)
        {
            MementoDouble cache;

            if ((memento == null) || !(memento is MementoDouble))
            {
                if (memento != null)
                    memento.Dispose();

                cache = new MementoDouble();
                memento = cache;
            }
            else
            {
                cache = (MementoDouble)memento;
            }

            // Draw entire background in linear gradient effect
            cache.first = DrawBackLinear(drawRect, sigma, color1, color2, orientation, g, cache.first);

            bool generate = true;
            MementoBackLinearRadial cacheThis;

            // Access a cache instance and decide if cache resources need generating
            if ((cache.second == null) || !(cache.second is MementoBackLinearRadial))
            {
                if (cache.second != null)
                    cache.second.Dispose();

                cacheThis = new MementoBackLinearRadial(drawRect, color2, color3, orientation);
                cache.second = cacheThis;
            }
            else
            {
                cacheThis = (MementoBackLinearRadial)cache.second;
                generate = !cacheThis.UseCachedValues(drawRect, color2, color3, orientation);
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                // Dispose of existing values
                cacheThis.Dispose();

                float third;

                // Find the 1/3 height used for the ellipse
                if (VerticalOrientation(orientation))
                    third = drawRect.Height / 3;
                else
                    third = drawRect.Width / 3;

                // Find the bottom area rectangle
                RectangleF ellipseRect;
                PointF centerPoint;

                switch (orientation)
                {
                    case VisualOrientation.Left:
                        ellipseRect = new RectangleF(drawRect.Right - third, drawRect.Y + 1, third, drawRect.Height - 2);
                        centerPoint = new PointF(ellipseRect.Right, ellipseRect.Y + (ellipseRect.Height / 2));
                        break;
                    case VisualOrientation.Right:
                        ellipseRect = new RectangleF(drawRect.X - 1, drawRect.Y + 1, third, drawRect.Height - 2);
                        centerPoint = new PointF(ellipseRect.Left, ellipseRect.Y + (ellipseRect.Height / 2));
                        break;
                    case VisualOrientation.Bottom:
                        ellipseRect = new RectangleF(drawRect.X + 1, drawRect.Y - 1, drawRect.Width - 2, third);
                        centerPoint = new PointF(ellipseRect.X + (ellipseRect.Width / 2), ellipseRect.Top);
                        break;
                    case VisualOrientation.Top:
                    default:
                        ellipseRect = new RectangleF(drawRect.X + 1, drawRect.Bottom - third, drawRect.Width - 2, third);
                        centerPoint = new PointF(ellipseRect.X + (ellipseRect.Width / 2), ellipseRect.Bottom);
                        break;
                }

                cacheThis.ellipseRect = ellipseRect;

                // Cannot draw a path that contains a zero sized element
                if ((ellipseRect.Width > 0) && (ellipseRect.Height > 0))
                {
                    cacheThis.path = new GraphicsPath();
                    cacheThis.path.AddEllipse(ellipseRect);
                    cacheThis.bottomBrush = new PathGradientBrush(cacheThis.path);
                    cacheThis.bottomBrush.CenterColor = ControlPaint.Light(color3);
                    cacheThis.bottomBrush.CenterPoint = centerPoint;
                    cacheThis.bottomBrush.SurroundColors = new Color[] { color2 };
                }
            }

            if (cacheThis.bottomBrush != null)
                g.FillRectangle(cacheThis.bottomBrush, cacheThis.ellipseRect);

            return memento;
        }

        private static IDisposable DrawBackGlassRadial(RectangleF drawRect,
                                                       Color color1,
                                                       Color color2,
                                                       Color glassColor1,
                                                       Color glassColor2,
                                                       float factorX,
                                                       float factorY,
                                                       VisualOrientation orientation,
                                                       Graphics g,
                                                       float glassPercent,
                                                       IDisposable memento)
        {
            MementoDouble cache;

            if ((memento == null) || !(memento is MementoDouble))
            {
                if (memento != null)
                    memento.Dispose();

                cache = new MementoDouble();
                memento = cache;
            }
            else
            {
                cache = (MementoDouble)memento;
            }

            // Draw the gradient effect background
            RectangleF glassRect = DrawBackGlassBasic(drawRect, color1, color2, 
                                                      glassColor1, glassColor2,
                                                      factorX, factorY, 
                                                      orientation, g,
                                                      glassPercent,
                                                      ref cache.first);

            bool generate = true;
            MementoBackGlassRadial cacheThis;

            // Access a cache instance and decide if cache resources need generating
            if ((cache.second == null) || !(cache.second is MementoBackGlassRadial))
            {
                if (cache.second != null)
                    cache.second.Dispose();

                cacheThis = new MementoBackGlassRadial(drawRect, color1, color2, factorX, factorY, orientation);
                cache.second = cacheThis;
            }
            else
            {
                cacheThis = (MementoBackGlassRadial)cache.second;
                generate = !cacheThis.UseCachedValues(drawRect, color1, color2, factorX, factorY, orientation);
            }

            // Do we need to generate the contents of the cache?
            if (generate)
            {
                // Dispose of existing values
                cacheThis.Dispose();

                // Find the bottom area rectangle
                RectangleF mainRect;

                switch (orientation)
                {
                    case VisualOrientation.Right:
                        mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width - glassRect.Width - 1, drawRect.Height);
                        break;
                    case VisualOrientation.Left:
                        mainRect = new RectangleF(glassRect.Right + 1, drawRect.Y, drawRect.Width - glassRect.Width - 1, drawRect.Height);
                        break;
                    case VisualOrientation.Bottom:
                        mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height - glassRect.Height - 1);
                        break;
                    case VisualOrientation.Top:
                    default:
                        mainRect = new RectangleF(drawRect.X, glassRect.Bottom + 1, drawRect.Width, drawRect.Height - glassRect.Height - 1);
                        break;
                }

                RectangleF doubleRect;

                // Find the box that encloses the ellipse (ellipses is sized using the factorX, factorY)
                if (VerticalOrientation(orientation))
                {
                    float mainRectWidth = (mainRect.Width * factorX);
                    float mainRectWidthOffset = (mainRectWidth - mainRect.Width) / 2;
                    float mainRectHeight = (mainRect.Height * factorY);
                    float mainRectHeightOffset;

                    // Find orientation specific ellsipe rectangle
                    if (orientation == VisualOrientation.Top)
                        mainRectHeightOffset = (mainRectHeight - mainRect.Height) / 2;
                    else
                        mainRectHeightOffset = (mainRectHeight + (mainRectHeight - mainRect.Height) / 2);

                    doubleRect = new RectangleF(mainRect.X - mainRectWidthOffset,
                                                mainRect.Y - mainRectHeightOffset,
                                                mainRectWidth, mainRectHeight * 2);
                }
                else
                {
                    float mainRectHeight = (mainRect.Height * factorX);
                    float mainRectHeightOffset = (mainRectHeight - mainRect.Height) / 2;
                    float mainRectWidth = (mainRect.Width * factorY);
                    float mainRectWidthOffset;

                    // Find orientation specific ellsipe rectangle
                    if (orientation == VisualOrientation.Left)
                        mainRectWidthOffset = (mainRectWidth - mainRect.Width) / 2;
                    else
                        mainRectWidthOffset = (mainRectWidth + (mainRectWidth - mainRect.Width) / 2);

                    doubleRect = new RectangleF(mainRect.X - mainRectWidthOffset,
                                                mainRect.Y - mainRectHeightOffset,
                                                mainRectWidth * 2, mainRectHeight);
                }

                // Cannot draw a path that contains a zero sized element
                if ((doubleRect.Width > 0) && (doubleRect.Height > 0))
                {
                    // We use a path to create an ellipse for the light effect in the bottom of the area
                    cacheThis.path = new GraphicsPath();
                    cacheThis.path.AddEllipse(doubleRect);

                    // Create a brush from the path
                    cacheThis.bottomBrush = new PathGradientBrush(cacheThis.path);
                    cacheThis.bottomBrush.CenterColor = color2;
                    cacheThis.bottomBrush.CenterPoint = new PointF(doubleRect.X + (doubleRect.Width / 2), doubleRect.Y + (doubleRect.Height / 2));
                    cacheThis.bottomBrush.SurroundColors = new Color[] { color1 };
                    cacheThis.mainRect = mainRect;
                }
            }

            if (cacheThis.bottomBrush != null)
                g.FillRectangle(cacheThis.bottomBrush, cacheThis.mainRect);

            return memento;
        }

        private static IDisposable DrawBackGlassCenter(RectangleF drawRect,
                                                       Color color1,
                                                       Color color2,
                                                       Color glassColor1,
                                                       Color glassColor2,
                                                       float factorX,
                                                       float factorY,
                                                       VisualOrientation orientation,
                                                       Graphics g,
                                                       float glassPercent,
                                                       IDisposable memento)
        {
            // Cannot draw a path that contains a zero sized element
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
            {
                MementoDouble cache;

                if ((memento == null) || !(memento is MementoDouble))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoDouble();
                    memento = cache;
                }
                else
                {
                    cache = (MementoDouble)memento;
                }

                // Draw the gradient effect background
                DrawBackGlassBasic(drawRect, color1, color2,
                                   glassColor1, glassColor2,
                                   factorX, factorY,
                                   orientation, g,
                                   glassPercent,
                                   ref cache.first);

                bool generate = true;
                MementoBackGlassCenter cacheThis;

                // Access a cache instance and decide if cache resources need generating
                if ((cache.second == null) || !(cache.second is MementoBackGlassCenter))
                {
                    if (cache.second != null)
                        cache.second.Dispose();

                    cacheThis = new MementoBackGlassCenter(drawRect, color2);
                    cache.second = cacheThis;
                }
                else
                {
                    cacheThis = (MementoBackGlassCenter)cache.second;
                    generate = !cacheThis.UseCachedValues(drawRect, color2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cacheThis.Dispose();

                    cacheThis.path = new GraphicsPath();
                    cacheThis.path.AddEllipse(drawRect);
                    cacheThis.bottomBrush = new PathGradientBrush(cacheThis.path);
                    cacheThis.bottomBrush.CenterColor = color2;
                    cacheThis.bottomBrush.CenterPoint = new PointF(drawRect.X + (drawRect.Width / 2), drawRect.Y + (drawRect.Height / 2));
                    cacheThis.bottomBrush.SurroundColors = new Color[] { Color.Transparent };
                }

                g.FillRectangle(cacheThis.bottomBrush, drawRect);
            }

            return memento;
        }

        private static IDisposable DrawBackGlassFade(RectangleF drawRect,
                                                     RectangleF outerRect,
                                                     Color color1,
                                                     Color color2,
                                                     Color glassColor1,
                                                     Color glassColor2,
                                                     VisualOrientation orientation,
                                                     Graphics g,
                                                     IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0) &&
                (outerRect.Width > 0) && (outerRect.Height > 0))
            {
                bool generate = true;
                MementoBackGlassFade cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoBackGlassFade))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoBackGlassFade(drawRect, outerRect, color1, color2, 
                                                     glassColor1, glassColor2, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoBackGlassFade)memento;
                    generate = !cache.UseCachedValues(drawRect, outerRect, color1, color2,
                                                      glassColor1, glassColor2, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // Create gradient rect from the drawing rect
                    RectangleF gradientRect = new RectangleF(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2, drawRect.Height + 2);

                    // Cannot draw a zero sized rectangle
                    if ((gradientRect.Width > 0) && (gradientRect.Height > 0))
                    {
                        // Draw a gradient from first to second over the length, but use the
                        // first color for the first 33% of distance and fade over the rest
                        cache.mainBrush = new LinearGradientBrush(gradientRect, color1, color2, AngleFromOrientation(orientation));
                        cache.mainBrush.Blend = _glassFadeBlend;
                    }

                    float glassLength;

                    // Glass covers 33% of the orienatated length
                    if (VerticalOrientation(orientation))
                        glassLength = (int)(outerRect.Height * 0.33f) + outerRect.Y - drawRect.Y;
                    else
                        glassLength = (int)(outerRect.Width * 0.33f) + outerRect.X - drawRect.X;

                    RectangleF glassRect;
                    RectangleF mainRect;

                    // Create rectangles that cover the glass and main area
                    switch (orientation)
                    {
                        case VisualOrientation.Left:
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, glassLength, drawRect.Height);
                            break;
                        case VisualOrientation.Right:
                            mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width - glassLength, drawRect.Height);
                            glassRect = new RectangleF(mainRect.Right, drawRect.Y, glassLength, drawRect.Height);
                            break;
                        case VisualOrientation.Top:
                        default:
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, glassLength);
                            break;
                        case VisualOrientation.Bottom:
                            mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height - glassLength);
                            glassRect = new RectangleF(drawRect.X, mainRect.Bottom, drawRect.Width, glassLength);
                            break;
                    }

                    // Create gradient rectangles
                    RectangleF glassGradientRect = new RectangleF(glassRect.X - 1, glassRect.Y - 1, glassRect.Width + 2, glassRect.Height + 2);

                    // Cannot draw a zero sized rectangle
                    if ((glassRect.Width > 0) && (glassRect.Height > 0) &&
                        (glassGradientRect.Width > 0) && (glassGradientRect.Height > 0))
                    {
                        // Use semi-transparent white colors to create the glass effect
                        cache.topBrush = new LinearGradientBrush(glassGradientRect, glassColor1, glassColor2, AngleFromOrientation(orientation));
                        cache.glassRect = glassRect;
                    }
                }

                if (cache.mainBrush != null)
                    g.FillRectangle(cache.mainBrush, drawRect);

                if (cache.topBrush != null)
                    g.FillRectangle(cache.topBrush, cache.glassRect);
            }

            return memento;
        }

        private static IDisposable DrawBackGlassLinear(RectangleF drawRect,
                                                       RectangleF outerRect,
                                                       Color color1,
                                                       Color color2,
                                                       Color glassColor1,
                                                       Color glassColor2,
                                                       VisualOrientation orientation,
                                                       Graphics g,
                                                       float glassPercent,
                                                       IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0) &&
                (outerRect.Width > 0) && (outerRect.Height > 0))
            {
                bool generate = true;
                MementoBackGlassLinear cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoBackGlassLinear))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoBackGlassLinear(drawRect, outerRect, color1, color2,
                                                       glassColor1, glassColor2, orientation, glassPercent);
                    memento = cache;
                }
                else
                {
                    cache = (MementoBackGlassLinear)memento;
                    generate = !cache.UseCachedValues(drawRect, outerRect, color1, color2,
                                                      glassColor1, glassColor2, orientation, glassPercent);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    RectangleF glassRect;
                    RectangleF mainRect;
                    float glassLength;

                    // Glass covers specified percentage of the orienatated length
                    if (VerticalOrientation(orientation))
                        glassLength = (int)(outerRect.Height * glassPercent) + outerRect.Y - drawRect.Y;
                    else
                        glassLength = (int)(outerRect.Width * glassPercent) + outerRect.X - drawRect.X;

                    // Create rectangles that cover the glass and main area
                    switch (orientation)
                    {
                        case VisualOrientation.Left:
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, glassLength, drawRect.Height);
                            mainRect = new RectangleF(glassRect.Right + 1, drawRect.Y, drawRect.Width - glassRect.Width - 1, drawRect.Height);
                            break;
                        case VisualOrientation.Right:
                            mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width - glassLength, drawRect.Height);
                            glassRect = new RectangleF(mainRect.Right, drawRect.Y, glassLength, drawRect.Height);
                            break;
                        case VisualOrientation.Top:
                        default:
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, glassLength);
                            mainRect = new RectangleF(drawRect.X, glassRect.Bottom + 1, drawRect.Width, drawRect.Height - glassRect.Height - 1);
                            break;
                        case VisualOrientation.Bottom:
                            mainRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height - glassLength);
                            glassRect = new RectangleF(drawRect.X, mainRect.Bottom, drawRect.Width, glassLength);
                            break;
                    }

                    cache.totalBrush = new SolidBrush(color1);
                    cache.glassRect = glassRect;
                    cache.mainRect = mainRect;

                    // Create gradient rectangles
                    RectangleF glassGradientRect = new RectangleF(cache.glassRect.X - 1, cache.glassRect.Y - 1, cache.glassRect.Width + 2, cache.glassRect.Height + 2);
                    RectangleF mainGradientRect = new RectangleF(cache.mainRect.X - 1, cache.mainRect.Y - 1, cache.mainRect.Width + 2, cache.mainRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if ((cache.glassRect.Width > 0) && (cache.glassRect.Height > 0) &&
                        (cache.mainRect.Width > 0) && (cache.mainRect.Height > 0) &&
                        (glassGradientRect.Width > 0) && (glassGradientRect.Height > 0) &&
                        (mainGradientRect.Width > 0) && (mainGradientRect.Height > 0))
                    {
                        cache.topBrush = new LinearGradientBrush(glassGradientRect, glassColor1, glassColor2, AngleFromOrientation(orientation));
                        cache.bottomBrush = new LinearGradientBrush(mainGradientRect, color1, color2, AngleFromOrientation(orientation));
                    }
                }

                // Draw entire area in a solid color
                g.FillRectangle(cache.totalBrush, drawRect);

                if ((cache.topBrush != null) && (cache.bottomBrush != null))
                {
                    g.FillRectangle(cache.topBrush, cache.glassRect);
                    g.FillRectangle(cache.bottomBrush, cache.mainRect);
                }
            }

            return memento;
        }

        private static RectangleF DrawBackGlassBasic(RectangleF drawRect,
                                                     Color color1,
                                                     Color color2,
                                                     Color glassColor1,
                                                     Color glassColor2,
                                                     float factorX,
                                                     float factorY,
                                                     VisualOrientation orientation,
                                                     Graphics g,
                                                     float glassPercent,
                                                     ref IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
            {
                bool generate = true;
                MementoBackGlassBasic cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoBackGlassBasic))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoBackGlassBasic(drawRect, color1, color2,
                                                      glassColor1, glassColor2,
                                                      factorX, factorY, 
                                                      orientation, glassPercent);
                    memento = cache;
                }
                else
                {
                    cache = (MementoBackGlassBasic)memento;
                    generate = !cache.UseCachedValues(drawRect, color1, color2,
                                                      glassColor1, glassColor2,
                                                      factorX, factorY,
                                                      orientation, glassPercent);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // Draw entire area in a solid color
                    cache.totalBrush = new SolidBrush(color1);

                    int length;

                    if (VerticalOrientation(orientation))
                        length = (int)(drawRect.Height * glassPercent);
                    else
                        length = (int)(drawRect.Width * glassPercent);

                    RectangleF glassRect;

                    // Create rectangles that covers the glassy area
                    switch (orientation)
                    {
                        case VisualOrientation.Left:
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, length, drawRect.Height);
                            break;
                        case VisualOrientation.Right:
                            glassRect = new RectangleF(drawRect.Right - length, drawRect.Y, length, drawRect.Height);
                            break;
                        case VisualOrientation.Bottom:
                            glassRect = new RectangleF(drawRect.X, drawRect.Bottom - length, drawRect.Width, length);
                            break;
                        case VisualOrientation.Top:
                        default:
                            glassRect = new RectangleF(drawRect.X, drawRect.Y, drawRect.Width, length);
                            break;
                    }

                    // Gradient rectangle is always a little bigger to prevent tiling at edges
                    RectangleF glassGradientRect = new RectangleF(glassRect.X - 1, glassRect.Y - 1, glassRect.Width + 2, glassRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if ((glassGradientRect.Width > 0) && (glassGradientRect.Height > 0))
                    {
                        cache.glassBrush = new LinearGradientBrush(glassGradientRect, glassColor1, glassColor2, AngleFromOrientation(orientation));
                        cache.glassRect = glassRect;
                    }
                }

                g.FillRectangle(cache.totalBrush, drawRect);

                if (cache.glassBrush != null)
                {
                    g.FillRectangle(cache.glassBrush, cache.glassRect);
                    return cache.glassRect;
                }
            }

            return RectangleF.Empty;
        }

        private static IDisposable DrawBackLinear(RectangleF drawRect,
                                                  bool sigma,
                                                  Color color1,
                                                  Color color2,
                                                  VisualOrientation orientation,
                                                  Graphics g,
                                                  IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
            {
                bool generate = true;
                MementoBackLinear cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoBackLinear))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoBackLinear(drawRect, sigma, color1, color2, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoBackLinear)memento;
                    generate = !cache.UseCachedValues(drawRect, sigma, color1, color2, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // Create rectangle that covers the enter area
                    RectangleF gradientRect = new RectangleF(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2, drawRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if ((gradientRect.Width > 0) && (gradientRect.Height > 0))
                    {
                        // Draw entire area in a gradient color effect
                        cache.entireBrush = new LinearGradientBrush(gradientRect, color1, color2, AngleFromOrientation(orientation));

                        if (sigma)
                            cache.entireBrush.SetSigmaBellShape(0.5f);
                    }
                }

                if (cache.entireBrush != null)
                    g.FillRectangle(cache.entireBrush, drawRect);
            }

            return memento;
        }

        private static IDisposable DrawBackDarkEdge(RectangleF drawRect,
                                                    Color color1,
                                                    int thickness,
                                                    VisualOrientation orientation,
                                                    Graphics g,
                                                    IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
            {
                bool generate = true;
                MementoBackDarkEdge cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoBackDarkEdge))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoBackDarkEdge(drawRect, color1, thickness, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoBackDarkEdge)memento;
                    generate = !cache.UseCachedValues(drawRect, color1, thickness, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // If we need to scale down the dark thickness
                    if (VerticalOrientation(orientation))
                    {
                        if (drawRect.Height < 30)
                            thickness = (int)drawRect.Height / 10;
                    }
                    else
                    {
                        if (drawRect.Width < 30)
                            thickness = (int)drawRect.Width / 10;
                    }

                    // If there is something to draw
                    if (thickness >= 0)
                    {
                        // Alter rectangle to the drawing edge only
                        switch (orientation)
                        {
                            case VisualOrientation.Top:
                                drawRect.Height = thickness;
                                break;
                            case VisualOrientation.Left:
                                drawRect.Width = thickness;
                                break;
                            case VisualOrientation.Bottom:
                                drawRect.Y = drawRect.Bottom - thickness - 1;
                                drawRect.Height = thickness + 1;
                                break;
                            case VisualOrientation.Right:
                                drawRect.X = drawRect.Right - thickness - 1;
                                drawRect.Width = thickness + 1;
                                break;

                        }

                        // Create rectangle that covers the enter area
                        RectangleF gradientRect = new RectangleF(drawRect.X - 0.5f, drawRect.Y - 0.5f, drawRect.Width + 1, drawRect.Height + 1);

                        // Cannot draw a zero length rectangle
                        if ((gradientRect.Width > 0) && (gradientRect.Height > 0))
                        {
                            // Draw entire area in a gradient color effect
                            cache.entireBrush = new LinearGradientBrush(gradientRect, Color.FromArgb(64, color1), Color.Transparent, AngleFromOrientation(orientation));
                            cache.entireBrush.SetSigmaBellShape(1.0f);
                            cache.entireRect = drawRect;
                        }
                    }
                }

                if (cache.entireBrush != null)
                    g.FillRectangle(cache.entireBrush, cache.entireRect);
            }

            return memento;
        }

        private static bool VerticalOrientation(VisualOrientation orientation)
        {
            return (orientation == VisualOrientation.Top) ||
                   (orientation == VisualOrientation.Bottom);
        }

        private static float AngleFromOrientation(VisualOrientation orientation)
        {
            switch (orientation)
            {
                case VisualOrientation.Bottom:
                    return 270f;
                case VisualOrientation.Left:
                    return 0f;
                case VisualOrientation.Right:
                    return 180;
                case VisualOrientation.Top:
                default:
                    return 90f;
            }
        }

        private static void ModifyRectByEdges(ref Rectangle rect,
                                              int left,
                                              int top,
                                              int right,
                                              int bottom,
                                              VisualOrientation orientation)
        {
            switch (orientation)
            {
                case VisualOrientation.Top:
                    rect.X += left;
                    rect.Width -= (left + right);
                    rect.Y += top;
                    rect.Height -= (top + bottom);
                    break;
                case VisualOrientation.Bottom:
                    rect.X += left;
                    rect.Width -= (left + right);
                    rect.Y += bottom;
                    rect.Height -= (top + bottom);
                    break;
                case VisualOrientation.Left:
                    rect.X += top;
                    rect.Width -= (top + bottom);
                    rect.Y += right;
                    rect.Height -= (left + right);
                    break;
                case VisualOrientation.Right:
                    rect.X += bottom;
                    rect.Width -= (top + bottom);
                    rect.Y += left;
                    rect.Height -= (left + right);
                    break;
            }
        }
        #endregion
    }
}
