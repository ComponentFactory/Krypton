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
    internal class RenderExpertHelpers
    {
        #region Static Fields
        private static readonly Blend _rounded1Blend;
        private static readonly Blend _rounded2Blend;
        private static readonly float _itemCut = 1.7f;
        #endregion

        #region Identity
        static RenderExpertHelpers()
        {
            _rounded1Blend = new Blend();
            _rounded1Blend.Positions = new float[] { 0.0f, 0.1f, 1.0f };
            _rounded1Blend.Factors = new float[] { 0.0f, 1.0f, 1.0f };

            _rounded2Blend = new Blend();
            _rounded2Blend.Positions = new float[] { 0.0f, 0.50f, 0.75f, 1.0f };
            _rounded2Blend.Factors = new float[] { 0.0f, 1.0f, 1.0f, 1.0f };
        }
        #endregion

        #region Static Public
        /// <summary>
        /// Draw a background for an expert style button with tracking effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackExpertTracking(RenderContext context,
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

                cache.first = DrawBackExpert(rect, 
                                             CommonHelper.MergeColors(backColor1, 0.35f, Color.White, 0.65f),
                                             CommonHelper.MergeColors(backColor2, 0.53f, Color.White, 0.65f), 
                                             orientation, context.Graphics, memento, true, true);
                
                cache.second = DrawBackExpert(rect, backColor1, backColor2, orientation, context.Graphics, memento, false, true);

                return cache;
            }
        }

        /// <summary>
        /// Draw a background for an expert style button with pressed effect.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackExpertPressed(RenderContext context,
                                                        Rectangle rect,
                                                        Color backColor1,
                                                        Color backColor2,
                                                        VisualOrientation orientation,
                                                        GraphicsPath path,
                                                        IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Cannot draw a zero length rectangle
                if ((rect.Width > 0) && (rect.Height > 0))
                {
                    bool generate = true;
                    MementoBackExpertShadow cache;

                    // Access a cache instance and decide if cache resources need generating
                    if ((memento == null) || !(memento is MementoBackExpertShadow))
                    {
                        if (memento != null)
                            memento.Dispose();

                        cache = new MementoBackExpertShadow(rect, backColor1, backColor2);
                        memento = cache;
                    }
                    else
                    {
                        cache = (MementoBackExpertShadow)memento;
                        generate = !cache.UseCachedValues(rect, backColor1, backColor2);
                    }

                    // Do we need to generate the contents of the cache?
                    if (generate)
                    {
                        rect.X -= 1;
                        rect.Y -= 1;
                        rect.Width += 2;
                        rect.Height += 2;

                        // Dispose of existing values
                        cache.Dispose();
                        cache.path1 = CreateBorderPath(rect, _itemCut);
                        cache.path2 = CreateBorderPath(new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2), _itemCut);
                        cache.path3 = CreateBorderPath(new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4), _itemCut);
                        cache.brush1 = new SolidBrush(CommonHelper.MergeColors(backColor2, 0.4f, backColor1, 0.6f));
                        cache.brush2 = new SolidBrush(CommonHelper.MergeColors(backColor2, 0.2f, backColor1, 0.8f));
                        cache.brush3 = new SolidBrush(backColor1);
                    }

                    using (AntiAlias aa = new AntiAlias(context.Graphics))
                    {
                        context.Graphics.FillRectangle(cache.brush3, rect);
                        context.Graphics.FillPath(cache.brush1, cache.path1);
                        context.Graphics.FillPath(cache.brush2, cache.path2);
                        context.Graphics.FillPath(cache.brush3, cache.path3);
                    }
                }

                return memento;
            }
        }

        /// <summary>
        /// Draw a background for an expert style button that is checked.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackExpertChecked(RenderContext context,
                                                        Rectangle rect,
                                                        Color backColor1,
                                                        Color backColor2,
                                                        VisualOrientation orientation,
                                                        GraphicsPath path,
                                                        IDisposable memento)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw the expert background which is gradient with highlight at bottom
                return DrawBackExpert(rect, backColor1, backColor2, orientation, context.Graphics, memento, true, false);
            }
        }

        /// <summary>
        /// Draw a background for an expert style button that is checked and tracking.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        public static IDisposable DrawBackExpertCheckedTracking(RenderContext context,
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

                cache.first = DrawBackExpert(rect,
                                             CommonHelper.MergeColors(backColor1, 0.5f, Color.White, 0.5f),
                                             CommonHelper.MergeColors(backColor2, 0.5f, Color.White, 0.5f),
                                             orientation, context.Graphics, memento, true, false);

                cache.second = DrawBackExpert(rect, backColor1, backColor2, orientation, context.Graphics, memento, false, false);

                return cache;
            }
        }

        /// <summary>
        /// Draw a background for an expert style button has a square inside with highlight.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Rectangle to draw.</param>
        /// <param name="backColor1">First color.</param>
        /// <param name="backColor2">Second color.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="path">Clipping path.</param>
        /// <param name="memento">Cache used for drawing.</param>
        /// <param name="light">Use the 'light' variation.</param>
        public static IDisposable DrawBackExpertSquareHighlight(RenderContext context,
                                                                Rectangle rect,
                                                                Color backColor1,
                                                                Color backColor2,
                                                                VisualOrientation orientation,
                                                                GraphicsPath path,
                                                                IDisposable memento,
                                                                bool light)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Cannot draw a zero length rectangle
                if ((rect.Width > 0) && (rect.Height > 0))
                {
                    bool generate = true;
                    MementoBackExpertSquareHighlight cache;

                    // Access a cache instance and decide if cache resources need generating
                    if ((memento == null) || !(memento is MementoBackExpertSquareHighlight))
                    {
                        if (memento != null)
                            memento.Dispose();

                        cache = new MementoBackExpertSquareHighlight(rect, backColor1, backColor2, orientation);
                        memento = cache;
                    }
                    else
                    {
                        cache = (MementoBackExpertSquareHighlight)memento;
                        generate = !cache.UseCachedValues(rect, backColor1, backColor2, orientation);
                    }

                    // Do we need to generate the contents of the cache?
                    if (generate)
                    {
                        // Dispose of existing values
                        cache.Dispose();

                        cache.backBrush = new SolidBrush(CommonHelper.WhitenColor(backColor1, 0.8f, 0.8f, 0.8f));
                        cache.innerRect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

                        RectangleF ellipseRect;
                        PointF ellipseCenter;
                        int ellipseWidth = Math.Max(1, rect.Width / 8);
                        int ellipseHeight = Math.Max(1, rect.Height / 8);

                        switch (orientation)
                        {
                            default:
                            case VisualOrientation.Top:
                                cache.innerBrush = new LinearGradientBrush(cache.innerRect, backColor1, backColor2, 90f);
                                ellipseRect = new RectangleF(rect.Left, rect.Top + (ellipseHeight * 2), rect.Width, ellipseHeight * 12);
                                ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Bottom);
                                break;
                            case VisualOrientation.Bottom:
                                cache.innerBrush = new LinearGradientBrush(cache.innerRect, backColor1, backColor2, 270f);
                                ellipseRect = new RectangleF(rect.Left, rect.Top - (ellipseHeight * 6), rect.Width, ellipseHeight * 12);
                                ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Top);
                                break;
                            case VisualOrientation.Left:
                                cache.innerBrush = new LinearGradientBrush(cache.innerRect, backColor1, backColor2, 180f);
                                ellipseRect = new RectangleF(rect.Left + (ellipseHeight * 2), rect.Top, ellipseWidth * 12, rect.Height);
                                ellipseCenter = new PointF(ellipseRect.Right, ellipseRect.Top + (ellipseRect.Height / 2));
                                break;
                            case VisualOrientation.Right:
                                cache.innerBrush = new LinearGradientBrush(rect, backColor1, backColor2, 0f);
                                ellipseRect = new RectangleF(rect.Left - (ellipseHeight * 6), rect.Top, ellipseWidth * 12, rect.Height);
                                ellipseCenter = new PointF(ellipseRect.Left, ellipseRect.Top + (ellipseRect.Height / 2));
                                break;
                        }

                        cache.innerBrush.SetSigmaBellShape(0.5f);
                        cache.ellipsePath = new GraphicsPath();
                        cache.ellipsePath.AddEllipse(ellipseRect);
                        cache.insideLighten = new PathGradientBrush(cache.ellipsePath);
                        cache.insideLighten.CenterPoint = ellipseCenter;
                        cache.insideLighten.CenterColor = (light ? Color.FromArgb(64, Color.White) : Color.FromArgb(128, Color.White));
                        cache.insideLighten.Blend = _rounded2Blend;
                        cache.insideLighten.SurroundColors = new Color[] { Color.Transparent };
                    }

                    context.Graphics.FillRectangle(cache.backBrush, rect);
                    context.Graphics.FillRectangle(cache.innerBrush, cache.innerRect);
                    context.Graphics.FillRectangle(cache.insideLighten, cache.innerRect);
                }

                return memento;
            }
        }
        #endregion

        #region Implementation
        private static IDisposable DrawBackSolid(RectangleF drawRect,
                                                 Color color1,
                                                 Graphics g,
                                                 IDisposable memento)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
            {
                bool generate = true;
                MementoBackSolid cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoBackSolid))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoBackSolid(drawRect, color1);
                    memento = cache;
                }
                else
                {
                    cache = (MementoBackSolid)memento;
                    generate = !cache.UseCachedValues(drawRect, color1);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();
                    cache.solidBrush = new SolidBrush(color1);
                }

                if (cache.solidBrush != null)
                    g.FillRectangle(cache.solidBrush, drawRect);
            }

            return memento;
        }

        private static IDisposable DrawBackExpert(Rectangle drawRect,
                                                  Color color1,
                                                  Color color2,
                                                  VisualOrientation orientation,
                                                  Graphics g,
                                                  IDisposable memento,
                                                  bool total,
                                                  bool tracking)
        {
            // Cannot draw a zero length rectangle
            if ((drawRect.Width > 0) && (drawRect.Height > 0))
            {
                bool generate = true;
                MementoBackExpertChecked cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoBackExpertChecked))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoBackExpertChecked(drawRect, color1, color2, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoBackExpertChecked)memento;
                    generate = !cache.UseCachedValues(drawRect, color1, color2, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // If not drawing total area... 
                    if (!total)
                    {
                        // Update to draw the inside area instead
                        drawRect.Inflate(-1, -1);

                        cache.drawRect = drawRect;
                        cache.clipPath = new GraphicsPath();
                        cache.clipPath.AddLine(drawRect.X + 1, drawRect.Y, drawRect.Right - 1, drawRect.Y);
                        cache.clipPath.AddLine(drawRect.Right - 1, drawRect.Y, drawRect.Right, drawRect.Y + 1);
                        cache.clipPath.AddLine(drawRect.Right, drawRect.Y + 1, drawRect.Right, drawRect.Bottom - 2);
                        cache.clipPath.AddLine(drawRect.Right, drawRect.Bottom - 2, drawRect.Right - 2, drawRect.Bottom);
                        cache.clipPath.AddLine(drawRect.Right - 2, drawRect.Bottom, drawRect.Left + 1, drawRect.Bottom);
                        cache.clipPath.AddLine(drawRect.Left + 1, drawRect.Bottom, drawRect.Left, drawRect.Bottom - 2);
                        cache.clipPath.AddLine(drawRect.Left, drawRect.Bottom - 2, drawRect.Left, drawRect.Y + 1);
                        cache.clipPath.AddLine(drawRect.Left, drawRect.Y + 1, drawRect.X + 1, drawRect.Y);
                    }
                    else
                    {
                        cache.clipPath = new GraphicsPath();
                        cache.clipPath.AddRectangle(drawRect);
                    }

                    // Create rectangle that covers the enter area
                    RectangleF gradientRect = new RectangleF(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2, drawRect.Height + 2);

                    // Cannot draw a zero length rectangle
                    if ((gradientRect.Width > 0) && (gradientRect.Height > 0))
                    {
                        // Draw entire area in a gradient color effect
                        cache.entireBrush = new LinearGradientBrush(gradientRect, CommonHelper.WhitenColor(color1, 0.92f, 0.92f, 0.92f), color1, AngleFromOrientation(orientation));
                        cache.entireBrush.Blend = _rounded1Blend;
                    }

                    RectangleF ellipseRect;
                    PointF ellipseCenter;
                    int ellipseHeight = Math.Max(1, drawRect.Height / 4);
                    int ellipseWidth = Math.Max(1, (tracking ? drawRect.Width : drawRect.Width / 4));

                    // Ellipse is based on the orientation
                    switch (orientation)
                    {
                        default:
                        case VisualOrientation.Top:
                            ellipseRect = new RectangleF(drawRect.Left - ellipseWidth, drawRect.Bottom - ellipseHeight, drawRect.Width + ellipseWidth * 2, ellipseHeight * 2);
                            ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Bottom);
                            break;
                        case VisualOrientation.Bottom:
                            ellipseRect = new RectangleF(drawRect.Left - ellipseWidth, drawRect.Top - ellipseHeight, drawRect.Width + ellipseWidth * 2, ellipseHeight * 2);
                            ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Top);
                            break;
                        case VisualOrientation.Left:
                            ellipseRect = new RectangleF(drawRect.Right - ellipseWidth, drawRect.Top - ellipseHeight, ellipseWidth * 2, drawRect.Height + ellipseHeight * 2);
                            ellipseCenter = new PointF(ellipseRect.Right, ellipseRect.Top + (ellipseRect.Height / 2));
                            break;
                        case VisualOrientation.Right:
                            ellipseRect = new RectangleF(drawRect.Left - ellipseWidth, drawRect.Top - ellipseHeight, ellipseWidth * 2, drawRect.Height + ellipseHeight * 2);
                            ellipseCenter = new PointF(ellipseRect.Left, ellipseRect.Top + (ellipseRect.Height / 2));
                            break;
                    }

                    cache.ellipsePath = new GraphicsPath();
                    cache.ellipsePath.AddEllipse(ellipseRect);
                    cache.insideLighten = new PathGradientBrush(cache.ellipsePath);
                    cache.insideLighten.CenterPoint = ellipseCenter;
                    cache.insideLighten.CenterColor = color2;
                    cache.insideLighten.Blend = _rounded2Blend;
                    cache.insideLighten.SurroundColors = new Color[] { Color.Transparent };
                }

                if (cache.entireBrush != null)
                {
                    using(Clipping clip = new Clipping(g, cache.clipPath))
                    {
                        g.FillRectangle(cache.entireBrush, cache.drawRect);
                        g.FillPath(cache.insideLighten, cache.ellipsePath);
                    }
                }
            }

            return memento;
        }  

        private static GraphicsPath CreateBorderPath(Rectangle rect, float cut)
        {
            // Drawing lines requires we draw inside the area we want
            rect.Width--;
            rect.Height--;

            // Create path using a simple set of lines that cut the corner
            GraphicsPath path = new GraphicsPath();
            path.AddLine(rect.Left + cut, rect.Top, rect.Right - cut, rect.Top);
            path.AddLine(rect.Right - cut, rect.Top, rect.Right, rect.Top + cut);
            path.AddLine(rect.Right, rect.Top + cut, rect.Right, rect.Bottom - cut);
            path.AddLine(rect.Right, rect.Bottom - cut, rect.Right - cut, rect.Bottom);
            path.AddLine(rect.Right - cut, rect.Bottom, rect.Left + cut, rect.Bottom);
            path.AddLine(rect.Left + cut, rect.Bottom, rect.Left, rect.Bottom - cut);
            path.AddLine(rect.Left, rect.Bottom - cut, rect.Left, rect.Top + cut);
            path.AddLine(rect.Left, rect.Top + cut, rect.Left + cut, rect.Top);
            return path;
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
        #endregion
    }
}
