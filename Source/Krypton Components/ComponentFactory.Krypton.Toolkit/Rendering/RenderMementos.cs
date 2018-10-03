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
    #region MementoDisposable
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoDisposable : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        ~MementoDisposable()
        {
            // If not already disposed manually, do it now
            if (!_disposed)
                Dispose(false);
        }

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        public void Dispose()
        {
            // Only need to dispose of resources once
            if (!_disposed)
                Dispose(true);
        }

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            // If called manully, no need to use a finalize call to dispose
            if (disposing)
                GC.SuppressFinalize(this);

            _disposed = true;
        }
    }
    #endregion

    #region MementoDouble
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoDouble : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public IDisposable first;
        /// <summary>For internal use only.</summary>
        public IDisposable second;

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (first != null)
            {
                first.Dispose();
                first = null;
            }

            if (second != null)
            {
                second.Dispose();
                second = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoTriple
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoTriple : MementoDouble
    {
        /// <summary>For internal use only.</summary>
        public IDisposable third;

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (third != null)
            {
                third.Dispose();
                third = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRectOneColor
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRectOneColor : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public Rectangle rect;
        /// <summary>For internal use only.</summary>
        public Color c1;

        /// <summary>For internal use only.</summary>
        public MementoRectOneColor(Rectangle r, Color color1)
        {
            rect = r;
            c1 = color1;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r, Color color1)
        {
            bool ret = rect.Equals(r) && c1.Equals(color1);

            rect = r;
            c1 = color1;

            return ret;
        }
    }
    #endregion

    #region MementoRectTwoColor
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRectTwoColor : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public Rectangle rect;
        /// <summary>For internal use only.</summary>
        public Color c1, c2;

        /// <summary>For internal use only.</summary>
        public MementoRectTwoColor(Rectangle r, Color color1, Color color2)
        {
            rect = r;
            c1 = color1;
            c2 = color2;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r, Color color1, Color color2)
        {
            bool ret = rect.Equals(r) && 
                       c1.Equals(color1) && 
                       c2.Equals(color2);

            rect = r;
            c1 = color1;
            c2 = color2;

            return ret;
        }
    }
    #endregion

    #region MementoRectThreeColor
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRectThreeColor : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public Color c3;

        /// <summary>For internal use only.</summary>
        public MementoRectThreeColor(Rectangle r,
                                     Color color1, Color color2,
                                     Color color3)
            : base(r, color1, color2)
        {
            c3 = color3;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color color1, Color color2,
                                    Color color3)
        {
            bool ret = (base.UseCachedValues(r, color1, color2) && 
                        c3.Equals(color3));

            c3 = color3;

            return ret;
        }
    }
    #endregion

    #region MementoRectFourColor
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRectFourColor : MementoRectThreeColor
    {
        /// <summary>For internal use only.</summary>
        public Color c4;

        /// <summary>For internal use only.</summary>
        public MementoRectFourColor(Rectangle r, 
                                    Color color1, Color color2,
                                    Color color3, Color color4)
            : base(r, color1, color2, color3)
        {
            c4 = color4;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r, 
                                    Color color1, Color color2,
                                    Color color3, Color color4)
        {
            bool ret = (base.UseCachedValues(r, color1, color2, color3) &&
                        c4.Equals(color4));

            c4 = color4;

            return ret;
        }
    }
    #endregion

    #region MementoRectFiveColor
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRectFiveColor : MementoRectFourColor
    {
        /// <summary>For internal use only.</summary>
        public Color c5;

        /// <summary>For internal use only.</summary>
        public MementoRectFiveColor(Rectangle r,
                                    Color color1, Color color2,
                                    Color color3, Color color4,
                                    Color color5)
            : base(r, color1, color2, color3, color4)
        {
            c5 = color5;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color color1, Color color2,
                                    Color color3, Color color4,
                                    Color color5)
        {
            bool ret = (base.UseCachedValues(r, color1, color2, color3, color4) &&
                        c5.Equals(color5));

            c5 = color5;

            return ret;
        }
    }
    #endregion

    #region MementoRibbonLinear
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonLinear : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush linearBrush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonLinear(Rectangle r,
                                   Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (linearBrush != null)
            {
                linearBrush.Dispose();
                linearBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonLinearBorder
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonLinearBorder : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush linearBrush;
        /// <summary>For internal use only.</summary>
        public Pen linearPen;
        /// <summary>For internal use only.</summary>
        public GraphicsPath borderPath;

        /// <summary>For internal use only.</summary>
        public MementoRibbonLinearBorder(Rectangle r,
                                         Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (linearBrush != null)
            {
                linearBrush.Dispose();
                borderPath.Dispose();
                linearPen.Dispose();

                linearBrush = null;
                borderPath = null;
                linearPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonAppButtonInner
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonAppButtonInner : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public SolidBrush outsideBrush;
        /// <summary>For internal use only.</summary>
        public SolidBrush insideBrush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonAppButtonInner(Rectangle r,
                                           Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (outsideBrush != null)
            {
                outsideBrush.Dispose();
                outsideBrush = null;
                insideBrush.Dispose();
                insideBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonAppButtonOuter
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonAppButtonOuter : MementoRectThreeColor
    {
        /// <summary>For internal use only.</summary>
        public SolidBrush wholeBrush;
        /// <summary>For internal use only.</summary>
        public GraphicsPath backPath;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush bottomDarkGradient;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush topLightenGradient;

        /// <summary>For internal use only.</summary>
        public MementoRibbonAppButtonOuter(Rectangle r,
                                           Color color1, Color color2,
                                           Color color3)
            : base(r, color1, color2, color3)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (wholeBrush != null)
            {
                wholeBrush.Dispose();
                wholeBrush = null;
                backPath.Dispose();
                backPath = null;
                bottomDarkGradient.Dispose();
                bottomDarkGradient = null;
                topLightenGradient.Dispose();
                topLightenGradient = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonAppTab
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonAppTab : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public GraphicsPath borderPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath borderFillPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath insideFillPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath highlightPath;
        /// <summary>For internal use only.</summary>
        public PathGradientBrush highlightBrush;
        /// <summary>For internal use only.</summary>
        public Rectangle highlightRect;
        /// <summary>For internal use only.</summary>
        public Pen borderPen;
        /// <summary>For internal use only.</summary>
        public Brush borderBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush insideFillBrush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonAppTab(Rectangle r, Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public void GeneratePaths(Rectangle rect, PaletteState state)
        {
            // Create the border path
            borderPath = new GraphicsPath();
            borderPath.AddLine(rect.Left, rect.Bottom - 2, rect.Left, rect.Top + 1.75f);
            borderPath.AddLine(rect.Left, rect.Top + 1.75f, rect.Left + 1, rect.Top);
            borderPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
            borderPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);
            borderPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 2);

            // Create border path for filling
            borderFillPath = new GraphicsPath();
            borderFillPath.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top + 1.75f);
            borderFillPath.AddLine(rect.Left, rect.Top + 1.75f, rect.Left + 1, rect.Top);
            borderFillPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
            borderFillPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);
            borderFillPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 1);

            // Path for the highlight at bottom center
            highlightRect = new Rectangle(rect.Left - (rect.Width / 8), rect.Top + (rect.Height / 2) - 2, rect.Width + (rect.Width / 5), rect.Height + 4);
            highlightPath = new GraphicsPath();
            highlightPath.AddEllipse(highlightRect);
            highlightBrush = new PathGradientBrush(highlightPath);
            highlightBrush.CenterPoint = new PointF(highlightRect.Left + (highlightRect.Width / 2), highlightRect.Top + (highlightRect.Height / 2));
            highlightBrush.SurroundColors = new Color[] { Color.Transparent };

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Y += 2;
            rect.Width -= 3;
            rect.Height -= 2;

            // Create inside path for filling
            insideFillPath = new GraphicsPath();
            insideFillPath.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top + 1f);
            insideFillPath.AddLine(rect.Left, rect.Top + 1f, rect.Left + 1, rect.Top);
            insideFillPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
            insideFillPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);
            insideFillPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 1);
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (borderPath != null)
            {
                borderPath.Dispose();
                borderFillPath.Dispose();
                insideFillPath.Dispose();
                borderPen.Dispose();
                borderBrush.Dispose();
                insideFillBrush.Dispose();

                borderPath = null;
                borderFillPath = null;
                insideFillPath = null;
                borderPen = null;
                borderBrush = null;
                insideFillBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupGradientOne
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupGradientOne : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush brush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupGradientOne(Rectangle r, Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (brush != null)
            {
                brush.Dispose();
                brush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupGradientTwo
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupGradientTwo : MementoRectFourColor
    {
        /// <summary>For internal use only.</summary>
        public Rectangle topRect, bottomRect;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush topBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush bottomBrush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupGradientTwo(Rectangle r, 
                                             Color color1, Color color2,
                                             Color color3, Color color4)
            : base(r, color1, color2, color3, color4)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (topBrush != null)
            {
                topBrush.Dispose();
                bottomBrush.Dispose();

                topBrush = null;
                bottomBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupCollapsedBorder
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupCollapsedBorder : MementoRectFourColor
    {
        /// <summary>For internal use only.</summary>
        public GraphicsPath solidPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath insidePath;
        /// <summary>For internal use only.</summary>
        public Pen solidPen;
        /// <summary>For internal use only.</summary>
        public Pen insidePen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupCollapsedBorder(Rectangle r,
                                                 Color color1, Color color2,
                                                 Color color3, Color color4)
            : base(r, color1, color2, color3, color4)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (solidPath != null)
            {
                solidPath.Dispose();
                insidePath.Dispose();
                solidPen.Dispose();
                insidePen.Dispose();

                solidPath = null;
                insidePath = null;
                solidPen = null;
                insidePen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupCollapsedFrameBorder
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupCollapsedFrameBorder : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public GraphicsPath solidPath;
        /// <summary>For internal use only.</summary>
        public SolidBrush titleBrush;
        /// <summary>For internal use only.</summary>
        public Pen solidPen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupCollapsedFrameBorder(Rectangle r, Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (solidPath != null)
            {
                solidPath.Dispose();
                titleBrush.Dispose();
                solidPen.Dispose();

                solidPath = null;
                titleBrush = null;
                solidPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupNormalBorder
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupNormalBorder : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public Rectangle backRect;
        /// <summary>For internal use only.</summary>
        public GraphicsPath solidPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath insidePath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath outsidePath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath lightPath;
        /// <summary>For internal use only.</summary>
        public Pen solidPen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupNormalBorder(Rectangle r, Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (solidPath != null)
            {
                solidPath.Dispose();
                insidePath.Dispose();
                outsidePath.Dispose();
                lightPath.Dispose();
                solidPen.Dispose();

                solidPath = null;
                insidePath = null;
                outsidePath = null;
                lightPath = null;
                solidPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupNormalBorderSep
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupNormalBorderSep : MementoRectFiveColor
    {
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush totalBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush innerBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush trackSepBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush trackFillBrush;
        /// <summary>For internal use only.</summary>
        public PathGradientBrush trackHighlightBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush pressedFillBrush;
        /// <summary>For internal use only.</summary>
        public Pen innerPen;
        /// <summary>For internal use only.</summary>
        public Pen trackSepPen;
        /// <summary>For internal use only.</summary>
        public Pen trackBottomPen;

        private bool _tracking;
        private bool _dark;


        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupNormalBorderSep(Rectangle r, 
                                                 Color color1, Color color2, 
                                                 Color color3, Color color4, 
                                                 Color color5, 
                                                 bool tracking, bool dark)
            : base(r, color1, color2, color3, color4, color5)
        {
            _tracking = tracking;
            _dark = dark;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color color1, Color color2,
                                    Color color3, Color color4,
                                    Color color5,
                                    bool tracking, bool dark)
        {
            bool ret = base.UseCachedValues(r, color1, color2, color3, color4, color5) &&
                       (_tracking == tracking) && 
                       (_dark == dark);

            _tracking = tracking;
            _dark = dark;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (totalBrush != null)
            {
                totalBrush.Dispose();
                innerBrush.Dispose();
                trackSepBrush.Dispose();
                trackFillBrush.Dispose();
                pressedFillBrush.Dispose();
                trackHighlightBrush.Dispose();
                innerPen.Dispose();
                trackSepPen.Dispose();
                trackBottomPen.Dispose();

                totalBrush = null;
                innerBrush = null;
                trackSepBrush = null;
                trackFillBrush = null;
                pressedFillBrush = null;
                trackHighlightBrush = null;
                innerPen = null;
                trackSepPen = null;
                trackBottomPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupNormalTitle
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupNormalTitle : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public GraphicsPath titlePath;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush titleBrush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupNormalTitle(Rectangle r, Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (titlePath != null)
            {
                titlePath.Dispose();
                titleBrush.Dispose();

                titlePath = null;
                titleBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupAreaBorder
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupAreaBorder : MementoRectFiveColor
    {
        /// <summary>For internal use only.</summary>
        public GraphicsPath outsidePath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath insidePathN;
        /// <summary>For internal use only.</summary>
        public GraphicsPath insidePathL;
        /// <summary>For internal use only.</summary>
        public GraphicsPath shadowPath;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush fillBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush fillTopBrush;
        /// <summary>For internal use only.</summary>
        public Pen shadowPenN;
        /// <summary>For internal use only.</summary>
        public Pen shadowPenL;
        /// <summary>For internal use only.</summary>
        public Pen outsidePen;
        /// <summary>For internal use only.</summary>
        public Pen insidePen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupAreaBorder(Rectangle r, 
                                            Color color1, Color color2,
                                            Color color3, Color color4,
                                            Color color5)
            : base(r, color1, color2, color3, color4, color5)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (outsidePath != null)
            {
                outsidePath.Dispose();
                insidePathN.Dispose();
                insidePathL.Dispose();
                shadowPath.Dispose();
                fillBrush.Dispose();
                fillTopBrush.Dispose();
                shadowPenN.Dispose();
                shadowPenL.Dispose();
                outsidePen.Dispose();
                insidePen.Dispose();

                outsidePath = null;
                insidePathN = null;
                insidePathL = null;
                shadowPath = null;
                fillBrush = null;
                fillTopBrush = null;
                shadowPenN = null;
                shadowPenL = null;
                outsidePen = null;
                insidePen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupAreaBorder3
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupAreaBorder3 : MementoRectFiveColor
    {
        /// <summary>For internal use only.</summary>
        public Rectangle borderRect;
        /// <summary>For internal use only.</summary>
        public Point[] borderPoints;
        /// <summary>For internal use only.</summary>
        public Rectangle backRect1;
        /// <summary>For internal use only.</summary>
        public Rectangle backRect2;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush backBrush1;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush backBrush2;
        /// <summary>For internal use only.</summary>
        public SolidBrush backBrush3;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush gradientBorderBrush;
        /// <summary>For internal use only.</summary>
        public Pen gradientBorderPen;
        /// <summary>For internal use only.</summary>
        public Pen solidBorderPen;
        /// <summary>For internal use only.</summary>
        public Pen shadowPen1;
        /// <summary>For internal use only.</summary>
        public Pen shadowPen2;
        /// <summary>For internal use only.</summary>
        public Pen shadowPen3;

        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupAreaBorder3(Rectangle r,
                                             Color color1, Color color2,
                                             Color color3, Color color4,
                                             Color color5)
            : base(r, color1, color2, color3, color4, color5)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (backBrush1 != null)
            {
                backBrush1.Dispose();
                backBrush2.Dispose();
                backBrush3.Dispose();
                gradientBorderBrush.Dispose();
                gradientBorderPen.Dispose();
                solidBorderPen.Dispose();
                shadowPen1.Dispose();
                shadowPen2.Dispose();
                shadowPen3.Dispose();

                backBrush1 = null;
                backBrush2 = null;
                backBrush3 = null; 
                gradientBorderBrush = null;
                gradientBorderPen = null;
                solidBorderPen = null;
                shadowPen1 = null;
                shadowPen2 = null;
                shadowPen3 = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonGroupAreaBorderContext
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonGroupAreaBorderContext : MementoRectThreeColor
    {
        /// <summary>For internal use only.</summary>
        public GraphicsPath outsidePath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath insidePath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath shadowPath;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush fillBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush fillTopBrush;
        /// <summary>For internal use only.</summary>
        public Pen shadowPen;
        /// <summary>For internal use only.</summary>
        public Pen outsidePen;
        /// <summary>For internal use only.</summary>
        public Pen insidePen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonGroupAreaBorderContext(Rectangle r,
                                                   Color color1, Color color2,
                                                   Color color3)
            : base(r, color1, color2, color3)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (outsidePath != null)
            {
                outsidePath.Dispose();
                insidePath.Dispose();
                shadowPath.Dispose();
                fillBrush.Dispose();
                fillTopBrush.Dispose();
                shadowPen.Dispose();
                outsidePen.Dispose();
                insidePen.Dispose();

                outsidePath = null;
                insidePath = null;
                shadowPath = null;
                fillBrush = null;
                fillTopBrush = null;
                shadowPen = null;
                outsidePen = null;
                insidePen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabTracking2007
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabTracking2007 : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public Rectangle half1Rect;
        /// <summary>For internal use only.</summary>
        public Rectangle half2Rect;
        /// <summary>For internal use only.</summary>
        public RectangleF half2RectF;
        /// <summary>For internal use only.</summary>
        public RectangleF ellipseRect;
        /// <summary>For internal use only.</summary>
        public GraphicsPath outsidePath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath topPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath ellipsePath;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush half1LeftBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush half1RightBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush half1LightBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush outsideBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush insideBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush topBrush;
        /// <summary>For internal use only.</summary>
        public PathGradientBrush ellipseBrush;
        /// <summary>For internal use only.</summary>
        public SolidBrush half2Brush;
        /// <summary>For internal use only.</summary>
        public Pen outsidePen;
        /// <summary>For internal use only.</summary>
        public Pen topPen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabTracking2007(Rectangle r, 
                                        Color color1, Color color2,
                                        VisualOrientation orient)
            : base(r, color1, color2)
        {
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r, 
                                    Color color1, Color color2,
                                    VisualOrientation orient)
        {
            bool ret = rect.Equals(r) &&
                       c1.Equals(color1) &&
                       c2.Equals(color2) &&
                       (orient == orientation);

            rect = r;
            c1 = color1;
            c2 = color2;
            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (outsidePath != null)
            {
                outsidePath.Dispose();
                topPath.Dispose();
                ellipsePath.Dispose();
                half1LeftBrush.Dispose();
                half1RightBrush.Dispose();
                half1LightBrush.Dispose();
                outsideBrush.Dispose();
                insideBrush.Dispose();
                topBrush.Dispose();
                half2Brush.Dispose();
                outsidePen.Dispose();
                topPen.Dispose();

                outsidePath = null;
                topPath = null;
                ellipsePath = null;
                half1LeftBrush = null;
                half1RightBrush = null;
                half1LightBrush = null;
                outsideBrush = null;
                insideBrush = null;
                topBrush = null;
                half2Brush = null;
                outsidePen = null;
                topPen = null;

                if (ellipseBrush != null)
                {
                    ellipseBrush.Dispose();
                    ellipseBrush = null;
                }
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabTracking2010
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabTracking2010 : MementoRectFourColor
    {
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public GraphicsPath borderPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath outsidePath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath insidePath;
        /// <summary>For internal use only.</summary>
        public SolidBrush outsideBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush insideBrush;
        /// <summary>For internal use only.</summary>
        public Pen outsidePen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabTracking2010(Rectangle r,
                                            Color color1, Color color2,
                                            Color color3, Color color4,
                                            VisualOrientation orient)
            : base(r, color1, color2, color3, color4)
        {
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color color1, Color color2,
                                    Color color3, Color color4,
                                    VisualOrientation orient)
        {
            bool ret = rect.Equals(r) &&
                       c1.Equals(color1) &&
                       c2.Equals(color2) &&
                       c3.Equals(color1) &&
                       c4.Equals(color2) &&
                       (orient == orientation);

            rect = r;
            c1 = color1;
            c2 = color2;
            c3 = color3;
            c4 = color4;
            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (outsidePath != null)
            {
                borderPath.Dispose();
                outsidePath.Dispose();
                insidePath.Dispose();
                outsideBrush.Dispose();
                insideBrush.Dispose();
                outsidePen.Dispose();

                borderPath = null;
                outsidePath = null;
                insidePath = null;
                outsideBrush = null;
                insideBrush = null;
                outsidePen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabSelected2007
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabSelected2007 : MementoRectFiveColor
    {
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public Rectangle centerRect;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush centerBrush;
        /// <summary>For internal use only.</summary>
        public GraphicsPath outsidePath;
        /// <summary>For internal use only.</summary>
        public SolidBrush insideBrush;
        /// <summary>For internal use only.</summary>
        public Pen outsidePen;
        /// <summary>For internal use only.</summary>
        public Pen middlePen;
        /// <summary>For internal use only.</summary>
        public Pen insidePen;
        /// <summary>For internal use only.</summary>
        public Pen centerPen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabSelected2007(Rectangle r,
                                        Color color1, Color color2,
                                        Color color3, Color color4,
                                        Color color5,
                                        VisualOrientation orient)
            : base(r, color1, color2, color3, color4, color5)
        {
            orient = orientation;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color color1, Color color2,
                                    Color color3, Color color4,
                                    Color color5,
                                    VisualOrientation orient)
        {
            bool ret = (base.UseCachedValues(r, color1, color2, color3, color4, color5) &&
                        (orient == orientation));

            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (centerBrush != null)
            {
                centerBrush.Dispose();
                outsidePath.Dispose();
                insideBrush.Dispose();
                outsidePen.Dispose();
                middlePen.Dispose();
                insidePen.Dispose();
                centerPen.Dispose();

                centerBrush = null;
                outsidePath = null;
                insideBrush = null;
                outsidePen = null;
                middlePen = null;
                insidePen = null;
                centerPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabSelected2010
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabSelected2010 : MementoRectFiveColor
    {
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush centerBrush;
        /// <summary>For internal use only.</summary>
        public GraphicsPath outsidePath;
        /// <summary>For internal use only.</summary>
        public Pen outsidePen;
        /// <summary>For internal use only.</summary>
        public Pen centerPen;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush insideBrush;
        /// <summary>For internal use only.</summary>
        public GraphicsPath insidePath;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabSelected2010(Rectangle r,
                                            Color color1, Color color2,
                                            Color color3, Color color4,
                                            Color color5,
                                            VisualOrientation orient)
            : base(r, color1, color2, color3, color4, color5)
        {
            orient = orientation;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color color1, Color color2,
                                    Color color3, Color color4,
                                    Color color5,
                                    VisualOrientation orient)
        {
            bool ret = (base.UseCachedValues(r, color1, color2, color3, color4, color5) &&
                        (orient == orientation));

            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (centerBrush != null)
            {
                centerBrush.Dispose();
                outsidePath.Dispose();
                outsidePen.Dispose();
                centerPen.Dispose();
                insideBrush.Dispose();
                insidePath.Dispose();

                centerBrush = null;
                outsidePath = null;
                outsidePen = null;
                centerPen = null;
                insideBrush = null;
                insidePath = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabContextSelected
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabContextSelected : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public Rectangle interiorRect;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush insideBrush;
        /// <summary>For internal use only.</summary>
        public GraphicsPath outsidePath;
        /// <summary>For internal use only.</summary>
        public Pen outsidePen;
        /// <summary>For internal use only.</summary>
        public Pen l1, l2, l3;
        /// <summary>For internal use only.</summary>
        public Pen leftPen;
        /// <summary>For internal use only.</summary>
        public Pen rightPen;
        /// <summary>For internal use only.</summary>
        public Pen bottomInnerPen;
        /// <summary>For internal use only.</summary>
        public Pen bottomOuterPen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabContextSelected(Rectangle r,
                                               Color color1, Color color2,
                                               VisualOrientation orient)
            : base(r, color1, color2)
        {
            orient = orientation;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color color1, Color color2,
                                    VisualOrientation orient)
        {
            bool ret = (base.UseCachedValues(r, color1, color2) &&
                        (orient == orientation));

            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (outsidePath != null)
            {
                outsidePath.Dispose();
                insideBrush.Dispose();
                outsidePen.Dispose();
                l1.Dispose();
                l2.Dispose();
                l3.Dispose();
                leftPen.Dispose();
                rightPen.Dispose();
                bottomInnerPen.Dispose();
                bottomOuterPen.Dispose();

                outsidePath = null;
                insideBrush = null;
                outsidePen = null;
                l1 = null;
                l2 = null;
                l3 = null;
                leftPen = null;
                rightPen = null;
                bottomInnerPen = null;
                bottomOuterPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabHighlight
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabHighlight : MementoRectFiveColor
    {
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush topBorderBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush borderVertBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush outsideVertBrush;
        /// <summary>For internal use only.</summary>
        public MementoRibbonTabSelected2007 selectedMemento;
        /// <summary>For internal use only.</summary>
        public Pen innerVertPen;
        /// <summary>For internal use only.</summary>
        public Pen innerHorzPen;
        /// <summary>For internal use only.</summary>
        public Pen borderHorzPen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabHighlight(Rectangle r,
                                         Color color1, Color color2,
                                         Color color3, Color color4,
                                         Color color5,
                                         VisualOrientation orient)
            : base(r, color1, color2, color3, color4, color5)
        {
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color color1, Color color2,
                                    Color color3, Color color4,
                                    Color color5,
                                    VisualOrientation orient)
        {
            bool ret = (base.UseCachedValues(r, color1, color2, color3, color4, color5) &&
                        (orient == orientation));

            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (selectedMemento != null)
            {
                selectedMemento.Dispose();
                selectedMemento = null;
            }

            if (topBorderBrush != null)
            {
                topBorderBrush.Dispose();
                borderVertBrush.Dispose();
                outsideVertBrush.Dispose();
                innerVertPen.Dispose();
                innerHorzPen.Dispose();
                borderHorzPen.Dispose();

                topBorderBrush = null;
                borderVertBrush = null;
                outsideVertBrush = null;
                innerVertPen = null;
                innerHorzPen = null;
                borderHorzPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabGlowing
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabGlowing : MementoRectThreeColor
    {
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public RectangleF fullRect;
        /// <summary>For internal use only.</summary>
        public RectangleF ellipseRect;
        /// <summary>For internal use only.</summary>
        public GraphicsPath outsidePath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath topPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath ellipsePath;
        /// <summary>For internal use only.</summary>
        public SolidBrush insideBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush topBrush;
        /// <summary>For internal use only.</summary>
        public PathGradientBrush ellipseBrush;
        /// <summary>For internal use only.</summary>
        public Pen insidePen;
        /// <summary>For internal use only.</summary>
        public Pen outsidePen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabGlowing(Rectangle r, 
                                       Color color1, Color color2,
                                       Color color3,
                                       VisualOrientation orient)
            : base(r, color1, color2, color3)
        {
            orient = orientation;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color color1, Color color2,
                                    Color color3,
                                    VisualOrientation orient)
        {
            bool ret = (base.UseCachedValues(r, color1, color2, color3) &&
                        (orient == orientation));

            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (outsidePath != null)
            {
                outsidePath.Dispose();
                topPath.Dispose();
                ellipsePath.Dispose();
                insideBrush.Dispose();
                topBrush.Dispose();
                insidePen.Dispose();
                outsidePen.Dispose();

                outsidePath = null;
                topPath = null;
                ellipsePath = null;
                insideBrush = null;
                topBrush = null;
                insidePen = null;
                outsidePen = null;

                if (ellipseBrush != null)
                {
                    ellipseBrush.Dispose();
                    ellipseBrush = null;
                }
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabContext
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabContext : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public Rectangle fillRect;
        /// <summary>For internal use only.</summary>
        public Pen borderPen;
        /// <summary>For internal use only.</summary>
        public Pen underlinePen;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush fillBrush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabContext(Rectangle r, Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (borderPen != null)
            {
                borderPen.Dispose();
                fillBrush.Dispose();
                underlinePen.Dispose();

                borderPen = null;
                fillBrush = null;
                underlinePen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabContextOffice
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabContextOffice : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public Rectangle fillRect;
        /// <summary>For internal use only.</summary>
        public Pen borderPen;
        /// <summary>For internal use only.</summary>
        public Pen underlinePen;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush fillBrush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabContextOffice(Rectangle r, Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (borderPen != null)
            {
                borderPen.Dispose();
                fillBrush.Dispose();
                underlinePen.Dispose();

                borderPen = null;
                fillBrush = null;
                underlinePen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonTabContextOffice2010
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonTabContextOffice2010 : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public Pen borderInnerPen;
        /// <summary>For internal use only.</summary>
        public Pen borderOuterPen;
        /// <summary>For internal use only.</summary>
        public SolidBrush topBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush bottomBrush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonTabContextOffice2010(Rectangle r, Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (borderInnerPen != null)
            {
                borderInnerPen.Dispose();
                borderOuterPen.Dispose();
                topBrush.Dispose();
                bottomBrush.Dispose();

                borderInnerPen = null;
                borderOuterPen = null;
                topBrush = null;
                bottomBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonQATMinibar
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonQATMinibar : MementoRectFiveColor
    {
        /// <summary>For internal use only.</summary>
        public Pen lightPen;
        /// <summary>For internal use only.</summary>
        public Pen borderPen;
        /// <summary>For internal use only.</summary>
        public Pen whitenPen;
        /// <summary>For internal use only.</summary>
        public GraphicsPath borderPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath topRight1;
        /// <summary>For internal use only.</summary>
        public GraphicsPath bottomLeft1;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush innerBrush;

        /// <summary>For internal use only.</summary>
        public MementoRibbonQATMinibar(Rectangle r,
                                       Color color1, Color color2,
                                       Color color3, Color color4,
                                       Color color5)
            : base(r, color1, color2, color3, color4, color5)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (lightPen != null)
            {
                lightPen.Dispose();
                borderPen.Dispose();
                whitenPen.Dispose();
                borderPath.Dispose();
                topRight1.Dispose();
                bottomLeft1.Dispose();
                innerBrush.Dispose();

                lightPen = null;
                borderPen = null;
                whitenPen = null;
                borderPath = null;
                topRight1 = null;
                bottomLeft1 = null;
                innerBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonQATFullbarRound
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonQATFullbarRound : MementoRectThreeColor
    {
        /// <summary>For internal use only.</summary>
        public Rectangle innerRect;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush innerBrush;
        /// <summary>For internal use only.</summary>
        public GraphicsPath darkPath;
        /// <summary>For internal use only.</summary>
        public GraphicsPath lightPath1;
        /// <summary>For internal use only.</summary>
        public GraphicsPath lightPath2;
        /// <summary>For internal use only.</summary>
        public Pen darkPen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonQATFullbarRound(Rectangle r,
                                            Color color1, Color color2,
                                            Color color3)
            : base(r, color1, color2, color3)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (innerBrush != null)
            {
                innerBrush.Dispose();
                darkPath.Dispose();
                lightPath1.Dispose();
                lightPath2.Dispose();
                darkPen.Dispose();

                innerBrush = null;
                darkPath = null;
                lightPath1 = null;
                lightPath2 = null;
                darkPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonQATFullbarSquare
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonQATFullbarSquare : MementoRectThreeColor
    {
        /// <summary>For internal use only.</summary>
        public Pen lightPen;
        /// <summary>For internal use only.</summary>
        public SolidBrush mediumBrush;
        /// <summary>For internal use only.</summary>
        public Pen darkPen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonQATFullbarSquare(Rectangle r,
                                             Color color1, Color color2,
                                             Color color3)
            : base(r, color1, color2, color3)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (lightPen != null)
            {
                lightPen.Dispose();
                mediumBrush.Dispose();
                darkPen.Dispose();

                lightPen = null;
                mediumBrush = null;
                darkPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonQATOverflow
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonQATOverflow : MementoRectTwoColor
    {
        /// <summary>For internal use only.</summary>
        public SolidBrush backBrush;
        /// <summary>For internal use only.</summary>
        public Pen borderPen;

        /// <summary>For internal use only.</summary>
        public MementoRibbonQATOverflow(Rectangle r, Color color1, Color color2)
            : base(r, color1, color2)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (backBrush != null)
            {
                backBrush.Dispose();
                borderPen.Dispose();

                backBrush = null;
                borderPen = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoRibbonAppButton
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoRibbonAppButton : MementoRectFiveColor
    {
        /// <summary>For internal use only.</summary>
        public RectangleF borderShadow1;
        /// <summary>For internal use only.</summary>
        public RectangleF borderShadow2;
        /// <summary>For internal use only.</summary>
        public RectangleF borderMain1;
        /// <summary>For internal use only.</summary>
        public RectangleF borderMain2;
        /// <summary>For internal use only.</summary>
        public RectangleF borderMain3;
        /// <summary>For internal use only.</summary>
        public RectangleF borderMain4;
        /// <summary>For internal use only.</summary>
        public RectangleF rectLower;
        /// <summary>For internal use only.</summary>
        public RectangleF rectBottomGlow;
        /// <summary>For internal use only.</summary>
        public RectangleF rectUpperGlow;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush brushUpper1;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush brushLower;

        /// <summary>For internal use only.</summary>
        public MementoRibbonAppButton(Rectangle r,
                                      Color color1, Color color2,
                                      Color color3, Color color4,
                                      Color color5)
            : base(r, color1, color2, color3, color4, color5)
        {
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (brushUpper1 != null)
            {
                brushUpper1.Dispose();
                brushUpper1 = null;

                brushLower.Dispose();
                brushLower = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackSolid
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackSolid : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public SolidBrush solidBrush;

        /// <summary>For internal use only.</summary>
        public MementoBackSolid(RectangleF dR, Color c1)
        {
            drawRect = dR;
            color1 = c1;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR, Color c1)
        {
            bool ret = (drawRect.Equals(dR) && color1.Equals(c1));

            drawRect = dR;
            color1 = c1;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (solidBrush != null)
            {
                solidBrush.Dispose();
                solidBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackLinear
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackLinear : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public bool sigma;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush entireBrush;

        /// <summary>For internal use only.</summary>
        public MementoBackLinear(RectangleF dR,
                                 bool sig,
                                 Color c1,
                                 Color c2,
                                 VisualOrientation orient)
        {
            drawRect = dR;
            sigma = sig;
            color1 = c1;
            color2 = c2;
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR,
                                    bool sig,
                                    Color c1,
                                    Color c2,
                                    VisualOrientation orient)
        {
            bool ret = (drawRect.Equals(dR) &&
                        (sigma == sig) &&
                        color1.Equals(c1) &&
                        color2.Equals(c2) &&
                        (orientation == orient));

            drawRect = dR;
            sigma = sig;
            color1 = c1;
            color2 = c2;
            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (entireBrush != null)
            {
                entireBrush.Dispose();
                entireBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackLinearRadial
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackLinearRadial : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public Color color3;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public RectangleF ellipseRect;
        /// <summary>For internal use only.</summary>
        public GraphicsPath path;
        /// <summary>For internal use only.</summary>
        public PathGradientBrush bottomBrush;

        /// <summary>For internal use only.</summary>
        public MementoBackLinearRadial(RectangleF dR,
                                       Color c2,
                                       Color c3,
                                       VisualOrientation orient)
        {
            drawRect = dR;
            color2 = c2;
            color3 = c3;
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR,
                                    Color c2,
                                    Color c3,
                                    VisualOrientation orient)
        {
            bool ret = (drawRect.Equals(dR) &&
                        color2.Equals(c2) &&
                        color3.Equals(c3) &&
                        (orientation == orient));

            drawRect = dR;
            color2 = c2;
            color3 = c3;
            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (path != null)
            {
                path.Dispose();
                bottomBrush.Dispose();

                path = null;
                bottomBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackGlassBasic
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackGlassBasic : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public Color glassColor1;
        /// <summary>For internal use only.</summary>
        public Color glassColor2;
        /// <summary>For internal use only.</summary>
        public float factorX;
        /// <summary>For internal use only.</summary>
        public float factorY;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public float glassPercent;
        /// <summary>For internal use only.</summary>
        public RectangleF glassRect;
        /// <summary>For internal use only.</summary>
        public SolidBrush totalBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush glassBrush;

        /// <summary>For internal use only.</summary>
        public MementoBackGlassBasic(RectangleF dR,
                                     Color c1, Color c2,
                                     Color gC1, Color gC2,
                                     float fX, float fY,
                                     VisualOrientation orient,
                                     float gP)
        {
            drawRect = dR;
            color1 = c1;
            color2 = c2;
            glassColor1 = gC1;
            glassColor2 = gC2;
            factorX = fX;
            factorY = fY;
            orientation = orient;
            glassPercent = gP;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR,
                                    Color c1, Color c2,
                                    Color gC1, Color gC2,
                                    float fX, float fY,
                                    VisualOrientation orient,
                                    float gP)
        {
            bool ret = (drawRect.Equals(dR) &&
                        color1.Equals(c1) && color2.Equals(c2) &&
                        glassColor1.Equals(gC1) && glassColor2.Equals(gC2) &&
                        (factorX == fX) && (factorY == fY) &&
                        (orientation == orient) &&
                        (glassPercent == gP));

            drawRect = dR;
            color1 = c1;
            color2 = c2;
            glassColor1 = gC1;
            glassColor2 = gC2;
            factorX = fX;
            factorY = fY;
            orientation = orient;
            glassPercent = gP;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (totalBrush != null)
            {
                totalBrush.Dispose();
                glassBrush.Dispose();

                totalBrush = null;
                glassBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackGlassLinear
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackGlassLinear : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public RectangleF outerRect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public Color glassColor1;
        /// <summary>For internal use only.</summary>
        public Color glassColor2;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public float glassPercent;
        /// <summary>For internal use only.</summary>
        public RectangleF glassRect;
        /// <summary>For internal use only.</summary>
        public RectangleF mainRect;
        /// <summary>For internal use only.</summary>
        public SolidBrush totalBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush topBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush bottomBrush;

        /// <summary>For internal use only.</summary>
        public MementoBackGlassLinear(RectangleF dR, RectangleF oR,
                                      Color c1, Color c2,
                                      Color gC1, Color gC2,
                                      VisualOrientation orient,
                                      float gP)
        {
            drawRect = dR;
            outerRect = oR;
            color1 = c1;
            color2 = c2;
            glassColor1 = gC1;
            glassColor2 = gC2;
            orientation = orient;
            glassPercent = gP;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR, RectangleF oR,
                                    Color c1, Color c2,
                                    Color gC1, Color gC2,
                                    VisualOrientation orient,
                                    float gP)
        {
            bool ret = (drawRect.Equals(dR) &&
                        outerRect.Equals(oR) &&
                        color1.Equals(c1) &&
                        color2.Equals(c2) &&
                        glassColor1.Equals(gC1) &&
                        glassColor2.Equals(gC2) &&
                        (orientation == orient) &&
                        (glassPercent == gP));

            drawRect = dR;
            outerRect = oR;
            color1 = c1;
            color2 = c2;
            glassColor1 = gC1;
            glassColor2 = gC2;
            orientation = orient;
            glassPercent = gP;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (totalBrush != null)
            {
                totalBrush.Dispose();
                totalBrush = null;

                if (topBrush != null)
                {
                    topBrush.Dispose();
                    bottomBrush.Dispose();
                 
                    topBrush = null;
                    bottomBrush = null;
                }
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackGlassCenter
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackGlassCenter : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public GraphicsPath path;
        /// <summary>For internal use only.</summary>
        public PathGradientBrush bottomBrush;

        /// <summary>For internal use only.</summary>
        public MementoBackGlassCenter(RectangleF dR, Color c2)
        {
            drawRect = dR;
            color2 = c2;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR, Color c2)
        {
            bool ret = (drawRect.Equals(dR) && color2.Equals(c2));

            drawRect = dR;
            color2 = c2;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (path != null)
            {
                path.Dispose();
                bottomBrush.Dispose();

                path = null;
                bottomBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackGlassRadial
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackGlassRadial : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public float factorX;
        /// <summary>For internal use only.</summary>
        public float factorY;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public RectangleF mainRect;
        /// <summary>For internal use only.</summary>
        public GraphicsPath path;
        /// <summary>For internal use only.</summary>
        public PathGradientBrush bottomBrush;

        /// <summary>For internal use only.</summary>
        public MementoBackGlassRadial(RectangleF dR,
                                      Color c1, Color c2,
                                      float fX, float fY,
                                      VisualOrientation orient)
        {
            drawRect = dR;
            color1 = c1;
            color2 = c2;
            factorX = fX;
            factorY = fY;
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR,
                                    Color c1, Color c2,
                                    float fX, float fY,
                                    VisualOrientation orient)
        {
            bool ret = (drawRect.Equals(dR) &&
                        color1.Equals(c1) &&
                        color2.Equals(c2) &&
                        (factorX == fX) &&
                        (factorY == fY) &&
                        (orientation == orient));

            drawRect = dR;
            color1 = c1;
            color2 = c2;
            factorX = fX;
            factorY = fY;
            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (path != null)
            {
                path.Dispose();
                path = null;
            }

            if (bottomBrush != null)
            {
                bottomBrush.Dispose();
                bottomBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackGlassFade
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackGlassFade : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public RectangleF outerRect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public Color glassColor1;
        /// <summary>For internal use only.</summary>
        public Color glassColor2;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public RectangleF glassRect;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush mainBrush;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush topBrush;

        /// <summary>For internal use only.</summary>
        public MementoBackGlassFade(RectangleF dR, RectangleF oR,
                                    Color c1, Color c2,
                                    Color gC1, Color gC2,
                                    VisualOrientation orient)
        {
            drawRect = dR;
            outerRect = oR;
            color1 = c1;
            color2 = c2;
            glassColor1 = gC1;
            glassColor2 = gC2;
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR, RectangleF oR,
                                    Color c1, Color c2,
                                    Color gC1, Color gC2,
                                    VisualOrientation orient)
        {
            bool ret = (drawRect.Equals(dR) && outerRect.Equals(oR) &&
                        color1.Equals(c1) && color2.Equals(c2) &&
                        glassColor1.Equals(gC1) && glassColor2.Equals(gC2) &&
                        (orientation == orient));

            drawRect = dR;
            outerRect = oR;
            color1 = c1;
            color2 = c2;
            glassColor1 = gC1;
            glassColor2 = gC2;
            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (topBrush != null)
            {
                topBrush.Dispose();
                mainBrush.Dispose();

                topBrush = null;
                mainBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackGlassThreeEdge
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackGlassThreeEdge : MementoDouble
    {
        /// <summary>For internal use only.</summary>
        public Rectangle rect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public Color colorA1L;
        /// <summary>For internal use only.</summary>
        public Color colorA2L;
        /// <summary>For internal use only.</summary>
        public Color colorA2LL;
        /// <summary>For internal use only.</summary>
        public Color colorB2LL;
        /// <summary>For internal use only.</summary>
        public Rectangle rectB;

        /// <summary>For internal use only.</summary>
        public MementoBackGlassThreeEdge(Rectangle r,
                                         Color c1, 
                                         Color c2,
                                         VisualOrientation orient)
        {
            rect = r;
            color1 = c1;
            color2 = c2;
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(Rectangle r,
                                    Color c1, 
                                    Color c2,
                                    VisualOrientation orient)
        {
            bool ret = (rect.Equals(r) &&
                        color1.Equals(c1) && 
                        color2.Equals(c2) &&
                        (orientation == orient));

            rect =r ;
            color1 = c1;
            color2 = c2;
            orientation = orient;

            return ret;
        }
    }
    #endregion

    #region MementoBackDarkEdge
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackDarkEdge : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public int thickness;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public RectangleF entireRect;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush entireBrush;

        /// <summary>For internal use only.</summary>
        public MementoBackDarkEdge(RectangleF dR, 
                                   Color c1, 
                                   int thick,
                                   VisualOrientation orient)
        {
            drawRect = dR;
            color1 = c1;
            thickness = thick;
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR,
                                    Color c1,
                                    int thick,
                                    VisualOrientation orient)
        {
            bool ret = (drawRect.Equals(dR) &&
                        color1.Equals(c1) &&
                        (thickness == thick) &&
                        (orientation == orient));

            drawRect = dR;
            color1 = c1;
            thickness = thick;
            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (entireBrush != null)
            {
                entireBrush.Dispose();
                entireBrush = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackExpertChecked
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackExpertChecked : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush entireBrush;
        /// <summary>For internal use only.</summary>
        public GraphicsPath ellipsePath;
        /// <summary>For internal use only.</summary>
        public PathGradientBrush insideLighten;
        /// <summary>For internal use only.</summary>
        public GraphicsPath clipPath;

        /// <summary>For internal use only.</summary>
        public MementoBackExpertChecked(RectangleF dR,
                                        Color c1, Color c2,
                                        VisualOrientation orient)
        {
            drawRect = dR;
            color1 = c1;
            color2 = c2;
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR,
                                    Color c1, Color c2,
                                    VisualOrientation orient)
        {
            bool ret = (drawRect.Equals(dR) &&
                        color1.Equals(c1) &&
                        color2.Equals(c2) &&
                        (orientation == orient));

            drawRect = dR;
            color1 = c1;
            color2 = c2;
            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (entireBrush != null)
            {
                entireBrush.Dispose();
                ellipsePath.Dispose();
                insideLighten.Dispose();
                clipPath.Dispose();
                
                entireBrush = null;
                ellipsePath = null;
                insideLighten = null;
                clipPath = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackExpertShadow
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackExpertShadow : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public GraphicsPath path1;
        /// <summary>For internal use only.</summary>
        public GraphicsPath path2;
        /// <summary>For internal use only.</summary>
        public GraphicsPath path3;
        /// <summary>For internal use only.</summary>
        public SolidBrush brush1;
        /// <summary>For internal use only.</summary>
        public SolidBrush brush2;
        /// <summary>For internal use only.</summary>
        public SolidBrush brush3;

        /// <summary>For internal use only.</summary>
        public MementoBackExpertShadow(RectangleF dR, Color c1, Color c2)
        {
            drawRect = dR;
            color1 = c1;
            color2 = c2;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR, Color c1, Color c2)
        {
            bool ret = (drawRect.Equals(dR) &&
                        color1.Equals(c1) &&
                        color2.Equals(c2));

            drawRect = dR;
            color1 = c1;
            color2 = c2;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (path1 != null)
            {
                path1.Dispose();
                path2.Dispose();
                path3.Dispose();
                brush1.Dispose();
                brush2.Dispose();
                brush3.Dispose();

                path1 = null;
                path2 = null;
                path3 = null;
                brush1 = null;
                brush2 = null;
                brush3 = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion

    #region MementoBackExpertChecked
    /// <summary>
    /// Memento used to cache drawing details.
    /// </summary>
    public class MementoBackExpertSquareHighlight : MementoDisposable
    {
        /// <summary>For internal use only.</summary>
        public RectangleF drawRect;
        /// <summary>For internal use only.</summary>
        public Color color1;
        /// <summary>For internal use only.</summary>
        public Color color2;
        /// <summary>For internal use only.</summary>
        public VisualOrientation orientation;
        /// <summary>For internal use only.</summary>
        public SolidBrush backBrush;
        /// <summary>For internal use only.</summary>
        public Rectangle innerRect;
        /// <summary>For internal use only.</summary>
        public LinearGradientBrush innerBrush;
        /// <summary>For internal use only.</summary>
        public GraphicsPath ellipsePath;
        /// <summary>For internal use only.</summary>
        public PathGradientBrush insideLighten;

        /// <summary>For internal use only.</summary>
        public MementoBackExpertSquareHighlight(RectangleF dR,
                                                Color c1, Color c2,
                                                VisualOrientation orient)
        {
            drawRect = dR;
            color1 = c1;
            color2 = c2;
            orientation = orient;
        }

        /// <summary>For internal use only.</summary>
        public bool UseCachedValues(RectangleF dR,
                                    Color c1, Color c2,
                                    VisualOrientation orient)
        {
            bool ret = (drawRect.Equals(dR) &&
                        color1.Equals(c1) &&
                        color2.Equals(c2) &&
                        (orientation == orient));

            drawRect = dR;
            color1 = c1;
            color2 = c2;
            orientation = orient;

            return ret;
        }

        /// <summary>For internal use only.</summary>
        public override void Dispose(bool disposing)
        {
            if (backBrush != null)
            {
                backBrush.Dispose();
                innerBrush.Dispose();
                ellipsePath.Dispose();
                insideLighten.Dispose();

                backBrush = null;
                innerBrush = null;
                ellipsePath = null;
                insideLighten = null;
            }

            base.Dispose(disposing);
        }
    }
    #endregion
}
