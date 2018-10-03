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
    internal class KryptonOffice2010Renderer : KryptonProfessionalRenderer
    {
        #region GradientItemColors
        private abstract class GradientItemColors
        {
            #region Public Fields
            public Color Border1;
            public Color Border2;
            public Color Back1;
            public Color Back2;
            #endregion

            #region Identity
            public GradientItemColors()
            {
            }

            public GradientItemColors(Color border1, Color border2,
                                      Color back1, Color back2)
            {
                Border1 = border1;
                Border2 = border2;
                Back1 = back1;
                Back2 = back2;
            }
            #endregion

            #region Public
            public virtual void DrawItem(Graphics g, Rectangle rect)
            {
                // Cannot paint a zero sized area
                if ((rect.Width > 0) && (rect.Height > 0))
                {
                    // Draw the background of the entire item
                    DrawBack(g, rect);

                    // Draw the border of the entire item
                    DrawBorder(g, rect);
                }
            }

            public virtual void DrawBorder(Graphics g, Rectangle rect)
            {
                // Drawing with anti aliasing to create smoother appearance
                using (AntiAlias aa = new AntiAlias(g))
                {
                    Rectangle backRectI = rect;
                    backRectI.Inflate(1, 1);

                    // Finally draw the border around the menu item
                    using (LinearGradientBrush borderBrush = new LinearGradientBrush(backRectI, Border1, Border2, 90f))
                    {
                        // Convert the brush to a pen for DrawPath call
                        using (Pen borderPen = new Pen(borderBrush))
                        {
                            // Create border path around the entire item
                            using (GraphicsPath borderPath = CreateBorderPath(rect, _cutItemMenu))
                                g.DrawPath(borderPen, borderPath);
                        }
                    }
                }
            }

            public abstract void DrawBack(Graphics g, Rectangle rect);
            #endregion
        }

        private class GradientItemColorsSplit : GradientItemColors
        {
            /// <summary>
            /// Initialize a new instance of the GradientItemColorsSplit class.
            /// </summary>
            /// <param name="border">Base border color.</param>
            /// <param name="begin">Beginning background color.</param>
            /// <param name="end">Ending background color.</param>
            public GradientItemColorsSplit(Color border,
                                           Color begin,
                                           Color end)
            {
                // Calculate all colors from the provided parameters
                Border1 = border;
                Border2 = CommonHelper.WhitenColor(border, 0.979f, 0.943f, 1.20f);
            }

            public override void DrawBack(Graphics g, Rectangle rect)
            {
            }
        }

        private class GradientItemColorsTracking : GradientItemColors
        {
            public Color Back1B;
            public Color Back2B;

            /// <summary>
            /// Initialize a new instance of the GradientItemColorsTracking class.
            /// </summary>
            /// <param name="border">Base border color.</param>
            /// <param name="begin">Beginning background color.</param>
            /// <param name="end">Ending background color.</param>
            public GradientItemColorsTracking(Color border,
                                              Color begin,
                                              Color end)
            {
                // Calculate all colors from the provided parameters
                Border1 = border;
                Border2 = CommonHelper.WhitenColor(border, 0.979f, 0.943f, 1.20f);
                Back1 = begin;
                Back1B = CommonHelper.WhitenColor(begin, 1.0f, 0.975f, 0.930f);
                Back2 = end;
                Back2B = CommonHelper.WhitenColor(end, 1.0f, 0.953f, 0.758f);
            }

            public override void DrawBack(Graphics g, Rectangle rect)
            {
                Rectangle inset = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
                Rectangle insetB = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 3, rect.Height - 3);
                Rectangle insetC = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);

                using (LinearGradientBrush insideBrush1 = new LinearGradientBrush(rect, Back1B, Back1, 90f),
                                           insideBrush2 = new LinearGradientBrush(insetB, Back2B, Back2, 90f))
                {
                    insideBrush1.SetSigmaBellShape(0.5f);
                    insideBrush2.SetSigmaBellShape(0.5f);

                    g.FillRectangle(insideBrush1, inset);
                    using (GraphicsPath borderPath = CreateBorderPath(insetC, _cutInnerItemMenu),
                                        clipPath = CreateBorderPath(insetB, _cutInnerItemMenu))
                    {
                        using (Pen insidePen = new Pen(insideBrush2))
                            g.DrawPath(insidePen, borderPath);

                        g.FillPath(insideBrush2, borderPath);

                        using (Clipping clipping = new Clipping(g, clipPath))
                        {
                            using (GraphicsPath ellipsePath = new GraphicsPath())
                            {
                                RectangleF ellipseRect = new RectangleF(-(rect.Width / 2), rect.Bottom - 9, rect.Width * 2, 18);
                                PointF ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Top + (ellipseRect.Height / 2));
                                ellipsePath.AddEllipse(ellipseRect);

                                using (PathGradientBrush insideLighten = new PathGradientBrush(ellipsePath))
                                {
                                    insideLighten.CenterPoint = ellipseCenter;
                                    insideLighten.CenterColor = Color.White;
                                    insideLighten.SurroundColors = new Color[] { Color.Transparent };
                                    g.FillPath(insideLighten, ellipsePath);
                                }
                            }
                        }
                    }
                }
            }
        }

        private class GradientItemColorsDisabled : GradientItemColorsTracking
        {
            /// <summary>
            /// Initialize a new instance of the GradientItemColorsDisabled class.
            /// </summary>
            /// <param name="border">Base border color.</param>
            /// <param name="begin">Beginning background color.</param>
            /// <param name="end">Ending background color.</param>
            public GradientItemColorsDisabled(Color border,
                                              Color begin,
                                              Color end)
                : base(border, begin, end)
            {
                // Convert all colors to back and white
                Border1 = CommonHelper.ColorToBlackAndWhite(Border1);
                Border2 = CommonHelper.ColorToBlackAndWhite(Border2);
                Back1 = CommonHelper.ColorToBlackAndWhite(Back1);
                Back1B = CommonHelper.ColorToBlackAndWhite(Back1B);
                Back2 = CommonHelper.ColorToBlackAndWhite(Back2);
                Back2B = CommonHelper.ColorToBlackAndWhite(Back2B);
            }
        }

        private class GradientItemColorsPressed : GradientItemColors
        {
            /// <summary>
            /// Initialize a new instance of the GradientItemColorsPressed class.
            /// </summary>
            /// <param name="border">Base border color.</param>
            /// <param name="begin">Beginning background color.</param>
            /// <param name="end">Ending background color.</param>
            public GradientItemColorsPressed(Color border,
                                             Color begin,
                                             Color end)
            {
                // Calculate all colors from the provided parameters
                Border1 = CommonHelper.WhitenColor(border, 1.21f, 1.68f, 2.02f);
                Border2 = CommonHelper.WhitenColor(border, 1.21f, 1.25f, 1.22f);
                Back1 = begin;
            }

            public override void DrawBack(Graphics g, Rectangle rect)
            {
                Rectangle rect2 = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 1);
                Rectangle rect3 = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 3);

                using (AntiAlias aa = new AntiAlias(g))
                {
                    using (GraphicsPath path1 = CreateBorderPath(rect, _cutItemMenu),
                                        path2 = CreateBorderPath(rect2, _cutItemMenu),
                                        path3 = CreateBorderPath(rect3, _cutItemMenu))
                    {
                        using(SolidBrush brush1 = new SolidBrush(CommonHelper.MergeColors(Border1, 0.4f, Back1, 0.6f)),
                                         brush2 = new SolidBrush(CommonHelper.MergeColors(Border1, 0.2f, Back1, 0.8f)),
                                         brush3 = new SolidBrush(Back1))
                         {
                             g.FillPath(brush1, path1);
                             g.FillPath(brush2, path2);
                             g.FillPath(brush3, path3);
                         }
                    }
                }
            }
        }

        private class GradientItemColorsChecked : GradientItemColors
        {
            /// <summary>
            /// Initialize a new instance of the GradientItemColorsChecked class.
            /// </summary>
            /// <param name="border">Base border color.</param>
            /// <param name="begin">Beginning background color.</param>
            /// <param name="end">Ending background color.</param>
            public GradientItemColorsChecked(Color border,
                                             Color begin,
                                             Color end)
            {
                // Calculate all colors from the provided parameters
                Border1 = CommonHelper.WhitenColor(border, 1.21f, 1.44f, 1.81f);
                Border2 = CommonHelper.WhitenColor(border, 1.21f, 1.21f, 1.12f);
                Back1 = begin;
                Back2 = CommonHelper.WhitenColor(begin, 1.0f, 0.943f, .914f);
            }

            public override void DrawBack(Graphics g, Rectangle rect)
            {
                Rectangle inset = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
                Rectangle insetB = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 3, rect.Height - 3);

                using (LinearGradientBrush insideBrush = new LinearGradientBrush(rect, Back2, Back1, 90f))
                {
                    insideBrush.SetSigmaBellShape(0.5f);
                    g.FillRectangle(insideBrush, inset);

                    using (GraphicsPath borderPath = CreateBorderPath(inset, _cutInnerItemMenu))
                    {
                        using (GraphicsPath ellipsePath = new GraphicsPath())
                        {
                            RectangleF ellipseRect = new RectangleF(rect.Left, rect.Bottom - 8, rect.Width, 8);
                            PointF ellipseCenter = new PointF(ellipseRect.Left + (ellipseRect.Width / 2), ellipseRect.Top + (ellipseRect.Height / 2));
                            ellipsePath.AddEllipse(ellipseRect);

                            using (PathGradientBrush insideLighten = new PathGradientBrush(ellipsePath))
                            {
                                insideLighten.CenterPoint = ellipseCenter;
                                insideLighten.CenterColor = Color.FromArgb(96, Color.White);
                                insideLighten.SurroundColors = new Color[] { Color.Transparent };
                                g.FillPath(insideLighten, ellipsePath);
                            }
                        }
                    }
                }
            }
        }

        private class GradientItemColorsCheckedTracking : GradientItemColorsTracking
        {
            /// <summary>
            /// Initialize a new instance of the GradientItemColorsCheckedTracking class.
            /// </summary>
            /// <param name="border">Base border color.</param>
            /// <param name="begin">Beginning background color.</param>
            /// <param name="end">Ending background color.</param>
            public GradientItemColorsCheckedTracking(Color border,
                                                     Color begin,
                                                     Color end)
                : base(border, begin, end)
            {
                // Calculate all colors from the provided parameters
                Border1 = CommonHelper.WhitenColor(border, 1.21f, 1.44f, 1.81f);
                Border2 = CommonHelper.WhitenColor(border, 1.21f, 1.21f, 1.12f);
                Back1 = CommonHelper.WhitenColor(begin, 1.0f, 0.953f, 0.822f);
                Back1B = CommonHelper.WhitenColor(begin, 1.0f, 0.923f, 0.669f);
                Back2 = CommonHelper.WhitenColor(end, 1.0f, 0.964f, 1.06f);
                Back2B = CommonHelper.WhitenColor(end, 1.0f, 0.911f, 0.685f);
            }
        }
        #endregion

        #region Static Fields
        private static readonly int _gripOffset = 1;
        private static readonly int _gripSquare = 2;
        private static readonly int _gripSize = 3;
        private static readonly int _gripMove = 4;
        private static readonly int _gripLines = 3;
        private static readonly int _marginInset = 2;
        private static readonly int _checkInset = 1;
        private static readonly int _separatorInset = 31;
        private static readonly float _contextCheckTickThickness = 1.6f;
        private static readonly float _cutContextMenu = 0f;
        private static readonly float _cutItemMenu = 1.7f;
        private static readonly float _cutInnerItemMenu = 1.0f;
        private static readonly float _cutHeaderMenu = 1.0f;
        private static readonly Blend _stripBlend;
        private static readonly Blend _separatorLightBlend;
        private static readonly Blend _separatorDarkBlend;
        private static readonly Color _disabled = Color.FromArgb(167, 167, 167);
        private static GradientItemColors _disabledItem = new GradientItemColorsDisabled(Color.FromArgb(236, 199, 87), Color.FromArgb(251, 242, 215), Color.FromArgb(247, 224, 137));
        #endregion

        #region Instance Fields
        private GradientItemColorsSplit _gradientSplit;
        private GradientItemColorsTracking _gradientTracking;
        private GradientItemColorsPressed _gradientPressed;
        private GradientItemColorsChecked _gradientChecked;
        private GradientItemColorsCheckedTracking _gradientCheckedTracking;
        #endregion

        #region Identity
        static KryptonOffice2010Renderer()
        {
            _stripBlend = new Blend();
            _stripBlend.Positions = new float[] { 0.0f, 0.33f, 0.66f, 1.0f };
            _stripBlend.Factors = new float[] { 0.0f, 0.5f, 0.8f, 1.0f };

            _separatorDarkBlend = new Blend();
            _separatorDarkBlend.Positions = new float[] { 0.0f, 0.5f, 1.0f };
            _separatorDarkBlend.Factors = new float[] { 0.2f, 1f, 0.2f };

            _separatorLightBlend = new Blend();
            _separatorLightBlend.Positions = new float[] { 0.0f, 0.5f, 1.0f };
            _separatorLightBlend.Factors = new float[] { 0.1f, 0.6f, 0.1f };
        }

        /// <summary>
        /// Initialise a new instance of the KryptonOffice2010Renderer class.
        /// </summary>
        /// <param name="kct">Source for text colors.</param>
        public KryptonOffice2010Renderer(KryptonColorTable kct)
            : base(kct)
        {
        }
        #endregion

        #region OnRenderArrow
        /// <summary>
        /// Raises the RenderArrow event. 
        /// </summary>
        /// <param name="e">An ToolStripArrowRenderEventArgs containing the event data.</param>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            // Cannot paint a zero sized area
            if ((e.ArrowRectangle.Width > 0) &&
                (e.ArrowRectangle.Height > 0))
            {
                // Create a path that is used to fill the arrow
                using (GraphicsPath arrowPath = CreateArrowPath(e.Item,
                                                                e.ArrowRectangle,
                                                                e.Direction))
                {
                    // Get the rectangle that encloses the arrow and expand slightly
                    // so that the gradient is always within the expanding bounds
                    RectangleF boundsF = arrowPath.GetBounds();
                    boundsF.Inflate(1f, 1f);

                    // Set correct color of the arrow
                    Color color1;
                    Color color2;
                    if (!e.Item.Enabled)
                    {
                        color1 = _disabled;
                        color2 = _disabled;
                    }
                    else
                    {
                        if (e.Item.Pressed || e.Item.Selected || (e.Item is ToolStripMenuItem))
                            color1 = KCT.MenuItemText;
                        else
                            color1 = KCT.ToolStripText;

                        color2 = CommonHelper.WhitenColor(color1, 0.7f, 0.7f, 0.7f);
                    }

                    float angle = 0;

                    // Use gradient angle to match the arrow direction
                    switch (e.Direction)
                    {
                        case ArrowDirection.Right:
                            angle = 0;
                            break;
                        case ArrowDirection.Left:
                            angle = 180f;
                            break;
                        case ArrowDirection.Down:
                            angle = 90f;
                            break;
                        case ArrowDirection.Up:
                            angle = 270f;
                            break;
                    }

                    // Draw the actual arrow using a gradient
                    using (LinearGradientBrush arrowBrush = new LinearGradientBrush(boundsF, color1, color2, angle))
                        e.Graphics.FillPath(arrowBrush, arrowPath);
                }
            }
        }
        #endregion
        
        #region OnRenderButtonBackground
        /// <summary>
        /// Raises the RenderButtonBackground event. 
        /// </summary>
        /// <param name="e">An ToolStripItemRenderEventArgs containing the event data.</param>
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            // Cast to correct type
            ToolStripButton button = (ToolStripButton)e.Item;

            if (button.Selected || button.Pressed || button.Checked)
                RenderToolButtonBackground(e.Graphics, button, e.ToolStrip);
        }
        #endregion

        #region OnRenderDropDownButtonBackground
        /// <summary>
        /// Raises the RenderDropDownButtonBackground event. 
        /// </summary>
        /// <param name="e">An ToolStripItemRenderEventArgs containing the event data.</param>
        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
                RenderToolDropButtonBackground(e.Graphics, e.Item, e.ToolStrip);
        }
        #endregion

        #region OnRenderItemCheck
        /// <summary>
        /// Raises the RenderItemCheck event. 
        /// </summary>
        /// <param name="e">An ToolStripItemImageRenderEventArgs containing the event data.</param>
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            // Staring size of the checkbox is the image rectangle
            Rectangle checkBox = e.ImageRectangle;

            // Make the border of the check box 1 pixel bigger on all sides, as a minimum
            checkBox.Inflate(1, 1);

            // Can we extend upwards?
            if (checkBox.Top > _checkInset)
            {
                int diff = checkBox.Top - _checkInset;
                checkBox.Y -= diff;
                checkBox.Height += diff;
            }

            // Can we extend downwards?
            if (checkBox.Height <= (e.Item.Bounds.Height - (_checkInset * 2)))
            {
                int diff = e.Item.Bounds.Height - (_checkInset * 2) - checkBox.Height;
                checkBox.Height += diff;
            }

            // Drawing with anti aliasing to create smoother appearance
            using (AntiAlias aa = new AntiAlias(e.Graphics))
            {
                // Create border path for the check box
                using (GraphicsPath borderPath = CreateBorderPath(checkBox, _cutItemMenu))
                {
                    // Fill the background in a solid color
                    using (SolidBrush fillBrush = new SolidBrush(KCT.CheckBackground))
                        e.Graphics.FillPath(fillBrush, borderPath);

                    // Draw the border around the check box
                    using (Pen borderPen = new Pen(CommonHelper.WhitenColor(KCT.CheckBackground, 1.05f, 1.52f, 2.75f)))
                        e.Graphics.DrawPath(borderPen, borderPath);

                    // If there is not an image, then we can draw the tick, square etc...
                    if (e.Item.Image == null)
                    {
                        CheckState checkState = CheckState.Unchecked;

                        // Extract the check state from the item
                        if (e.Item is ToolStripMenuItem)
                        {
                            ToolStripMenuItem item = (ToolStripMenuItem)e.Item;
                            checkState = item.CheckState;
                        }

                        // Decide what graphic to draw
                        switch (checkState)
                        {
                            case CheckState.Checked:
                                // Create a path for the tick
                                using (GraphicsPath tickPath = CreateTickPath(checkBox))
                                {
                                    // Draw the tick with a thickish brush
                                    using (Pen tickPen = new Pen(CommonHelper.WhitenColor(KCT.CheckBackground, 3.86f, 3.02f, 1.07f), _contextCheckTickThickness))
                                        e.Graphics.DrawPath(tickPen, tickPath);
                                }
                                break;
                            case CheckState.Indeterminate:
                                // Create a path for the indeterminate diamond
                                using (GraphicsPath tickPath = CreateIndeterminatePath(checkBox))
                                {
                                    // Draw the tick with a thickish brush
                                    using (SolidBrush tickBrush = new SolidBrush(CommonHelper.WhitenColor(KCT.CheckBackground, 3.86f, 3.02f, 1.07f)))
                                        e.Graphics.FillPath(tickBrush, tickPath);
                                }
                                break;
                        }
                    }
                }
            }
        }
        #endregion

        #region OnRenderItemText
        /// <summary>
        /// Raises the RenderItemText event. 
        /// </summary>
        /// <param name="e">A ToolStripItemTextRenderEventArgs that contains the event data.</param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if ((e.ToolStrip is ToolStrip) || 
                (e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                if (!e.Item.Enabled)
                    e.TextColor = _disabled;
                else
                {
                    if ((e.ToolStrip is MenuStrip) && !e.Item.Pressed && !e.Item.Selected)
                        e.TextColor = KCT.MenuStripText;
                    else if (e.ToolStrip is MenuStrip)
                        e.TextColor = KCT.MenuItemText;
                    else if ((e.ToolStrip is StatusStrip) && !e.Item.Pressed && !e.Item.Selected)
                        e.TextColor = KCT.StatusStripText;
                    else if ((e.ToolStrip is StatusStrip) && !e.Item.Pressed && e.Item.Selected)
                        e.TextColor = KCT.MenuItemText;
                    else if ((e.ToolStrip is ToolStrip) && !e.Item.Pressed && e.Item.Selected)
                        e.TextColor = KCT.MenuItemText;
                    else if ((e.ToolStrip is ContextMenuStrip) && !e.Item.Pressed && !e.Item.Selected)
                        e.TextColor = KCT.MenuItemText;
                    else if (e.ToolStrip is ToolStripDropDownMenu)
                        e.TextColor = KCT.MenuItemText;
                    else if ((e.Item is ToolStripButton) && (((ToolStripButton)e.Item).Checked))
                        e.TextColor = KCT.MenuItemText;
                    else
                        e.TextColor = KCT.ToolStripText;
                }

                // Status strips under XP cannot use clear type because it ends up being cut off at edges
                if ((e.ToolStrip is StatusStrip) && (Environment.OSVersion.Version.Major < 6))
                    base.OnRenderItemText(e);
                else
                {
                    using (GraphicsTextHint clearTypeGridFit = new GraphicsTextHint(e.Graphics, TextRenderingHint.ClearTypeGridFit))
                        base.OnRenderItemText(e);
                }
            }
            else
                base.OnRenderItemText(e);
        }
        #endregion

        #region OnRenderItemImage
        /// <summary>
        /// Raises the RenderItemImage event. 
        /// </summary>
        /// <param name="e">An ToolStripItemImageRenderEventArgs containing the event data.</param>
        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            // We only override the image drawing for context menus
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                if (e.Image != null)
                {
                    if (e.Item.Enabled)
                        e.Graphics.DrawImage(e.Image, e.ImageRectangle);
                    else
                    {
                        using (ImageAttributes attribs = new ImageAttributes())
                        {
                            attribs.SetColorMatrix(CommonHelper.MatrixDisabled);

                            // Draw using the disabled matrix to make it look disabled
                            e.Graphics.DrawImage(e.Image, e.ImageRectangle,
                                                 0, 0, e.Image.Width, e.Image.Height,
                                                 GraphicsUnit.Pixel, attribs);
                        }
                    }
                }
            }
            else
            {
                base.OnRenderItemImage(e);
            }
        }
        #endregion

        #region OnRenderMenuItemBackground
        /// <summary>
        /// Raises the RenderMenuItemBackground event. 
        /// </summary>
        /// <param name="e">An ToolStripItemRenderEventArgs containing the event data.</param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if ((e.ToolStrip is MenuStrip) ||
                (e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                if ((e.Item.Pressed) && (e.ToolStrip is MenuStrip))
                {
                    // Draw the menu/tool strip as a header for a context menu item
                    DrawContextMenuHeader(e.Graphics, e.Item);
                }
                else
                {
                    // We only draw a background if the item is selected and enabled
                    if (e.Item.Selected)
                    {
                        if (e.Item.Enabled)
                        {
                            // Ensure we have cached the objects we need
                            UpdateCache();

                            // Do we draw as a menu strip or context menu item?
                            if (e.ToolStrip is MenuStrip)
                                DrawGradientToolItem(e.Graphics, e.Item, _gradientTracking);
                            else
                                DrawGradientContextMenuItem(e.Graphics, e.Item, _gradientTracking);
                        }
                        else
                        {
                            // Get the mouse position in tool strip coordinates
                            Point mousePos = e.ToolStrip.PointToClient(Control.MousePosition);

                            // If the mouse is not in the item area, then draw disabled
                            if (!e.Item.Bounds.Contains(mousePos))
                            {
                                // Do we draw as a menu strip or context menu item?
                                if (e.ToolStrip is MenuStrip)
                                    DrawGradientToolItem(e.Graphics, e.Item, _disabledItem);
                                else
                                    DrawGradientContextMenuItem(e.Graphics, e.Item, _disabledItem);
                            }
                        }
                    }
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }
        #endregion

        #region OnRenderSeparator
        /// <summary>
        /// Raises the RenderSeparator event. 
        /// </summary>
        /// <param name="e">An ToolStripSeparatorRenderEventArgs containing the event data.</param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                DrawContextMenuSeparator(e.Graphics, e.Vertical, e.Item.Bounds, _separatorInset,
                                         (e.ToolStrip.RightToLeft == RightToLeft.Yes));
            }
            else
            {
                DrawToolStripSeparator(e.Graphics, e.Vertical, e.Item.Bounds,
                                       KCT.SeparatorLight, KCT.SeparatorDark, 0, false);
            }
        }
        #endregion

        #region OnRenderSplitButtonBackground
        /// <summary>
        /// Raises the RenderSplitButtonBackground event. 
        /// </summary>
        /// <param name="e">An ToolStripItemRenderEventArgs containing the event data.</param>
        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                // Cast to correct type
                ToolStripSplitButton splitButton = (ToolStripSplitButton)e.Item;

                // Draw the border and background
                RenderToolSplitButtonBackground(e.Graphics, splitButton, e.ToolStrip);

                // Get the rectangle that needs to show the arrow
                Rectangle arrowBounds = splitButton.DropDownButtonBounds;

                // Draw the arrow on top of the background
                OnRenderArrow(new ToolStripArrowRenderEventArgs(e.Graphics,
                                                                splitButton,
                                                                arrowBounds,
                                                                SystemColors.ControlText,
                                                                ArrowDirection.Down));
            }
            else
            {
                base.OnRenderSplitButtonBackground(e);
            }
        }
        #endregion

        #region OnRenderStatusStripSizingGrip
        /// <summary>
        /// Raises the RenderStatusStripSizingGrip event. 
        /// </summary>
        /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            using (SolidBrush darkBrush = new SolidBrush(KCT.GripDark),
                              lightBrush = new SolidBrush(KCT.GripLight))
            {
                // Do we need to invert the drawing edge?
                bool rtl = (e.ToolStrip.RightToLeft == RightToLeft.Yes);

                // Find vertical position of the lowest grip line
                int y = e.AffectedBounds.Bottom - _gripSize * 2;

                // Draw three lines of grips
                for (int i = _gripLines; i >= 1; i--)
                {
                    // Find the rightmost grip position on the line
                    int x = (rtl ? e.AffectedBounds.Left + 1 :
                                   e.AffectedBounds.Right - _gripSize * 2);

                    // Draw grips from right to left on line
                    for (int j = 0; j < i; j++)
                    {
                        // Just the single grip glyph
                        DrawGripGlyph(e.Graphics, x, y, darkBrush, lightBrush);

                        // Move left to next grip position
                        x -= (rtl ? -_gripMove : _gripMove);
                    }

                    // Move upwards to next grip line
                    y -= _gripMove;
                }
            }
        }
        #endregion

        #region OnRenderToolStripContentPanelBackground
        /// <summary>
        /// Raises the RenderToolStripContentPanelBackground event. 
        /// </summary>
        /// <param name="e">An ToolStripContentPanelRenderEventArgs containing the event data.</param>
        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        {
            // Must call base class, otherwise the subsequent drawing does not appear!
            base.OnRenderToolStripContentPanelBackground(e);

            // Cannot paint a zero sized area
            if ((e.ToolStripContentPanel.Width > 0) &&
                (e.ToolStripContentPanel.Height > 0))
            {
                using (LinearGradientBrush backBrush = new LinearGradientBrush(e.ToolStripContentPanel.ClientRectangle,
                                                                               KCT.ToolStripContentPanelGradientEnd,
                                                                               KCT.ToolStripContentPanelGradientBegin,
                                                                               90f))
                {
                    e.Graphics.FillRectangle(backBrush, e.ToolStripContentPanel.ClientRectangle);
                }
            }
        }
        #endregion

        #region OnRenderToolStripBackground
        /// <summary>
        /// Raises the RenderToolStripBackground event. 
        /// </summary>
        /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                // Make sure the font is current
                if (e.ToolStrip.Font != KCT.MenuStripFont)
                    e.ToolStrip.Font = KCT.MenuStripFont;

                // Create border and clipping paths
                using (GraphicsPath borderPath = CreateBorderPath(e.AffectedBounds, _cutContextMenu),
                                      clipPath = CreateClipBorderPath(e.AffectedBounds, _cutContextMenu))
                {
                    // Clip all drawing to within the border path
                    using (Clipping clipping = new Clipping(e.Graphics, clipPath))
                    {
                        // Create the background brush
                        using (SolidBrush backBrush = new SolidBrush(KCT.ToolStripDropDownBackground))
                            e.Graphics.FillPath(backBrush, borderPath);
                    }
                }
            }
            else if (e.ToolStrip is StatusStrip)
            {
                // Make sure the font is current
                if (e.ToolStrip.Font != KCT.StatusStripFont)
                    e.ToolStrip.Font = KCT.StatusStripFont;

                // We do not paint the top two pixel lines, as they are drawn by the status strip border render method
                RectangleF backRect = new RectangleF(0, 1.5f, e.ToolStrip.Width, e.ToolStrip.Height - 2);

                // Cannot paint a zero sized area
                if ((backRect.Width > 0) && (backRect.Height > 0))
                {
                    using (LinearGradientBrush backBrush = new LinearGradientBrush(backRect,
                                                                                   KCT.StatusStripGradientBegin,
                                                                                   KCT.StatusStripGradientEnd,
                                                                                   90f))
                    {
                        backBrush.Blend = _stripBlend;
                        e.Graphics.FillRectangle(backBrush, backRect);
                    }
                }
            }
            else
            {
                // Make sure the font is current
                if (e.ToolStrip is MenuStrip)
                {
                    if (e.ToolStrip.Font != KCT.MenuStripFont)
                        e.ToolStrip.Font = KCT.MenuStripFont;

                    base.OnRenderToolStripBackground(e);
                }
                else
                {
                    if (e.ToolStrip.Font != KCT.ToolStripFont)
                        e.ToolStrip.Font = KCT.ToolStripFont;

                    // Cannot paint a zero sized area
                    RectangleF backRect = new RectangleF(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
                    if ((backRect.Width > 0) && (backRect.Height > 0))
                    {
                        if (e.ToolStrip.Orientation == Orientation.Horizontal)
                        {
                            using (LinearGradientBrush backBrush = new LinearGradientBrush(backRect,
                                                                                           KCT.ToolStripGradientBegin,
                                                                                           KCT.ToolStripGradientEnd,
                                                                                           90f))
                            {
                                backBrush.Blend = _stripBlend;
                                e.Graphics.FillRectangle(backBrush, backRect);
                            }

                            using (Pen darkBorder = new Pen(KCT.ToolStripBorder),
                                       lightBorder = new Pen(KCT.ToolStripGradientBegin))
                            {
                                e.Graphics.DrawLine(lightBorder, 0, 2, 0, e.ToolStrip.Height - 2);
                                e.Graphics.DrawLine(lightBorder, e.ToolStrip.Width - 2, 0, e.ToolStrip.Width - 2, e.ToolStrip.Height - 2);
                                e.Graphics.DrawLine(darkBorder, e.ToolStrip.Width - 1, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                            }
                        }
                        else
                        {
                            using (LinearGradientBrush backBrush = new LinearGradientBrush(backRect,
                                                                                           KCT.ToolStripGradientBegin,
                                                                                           KCT.ToolStripGradientEnd,
                                                                                           0f))
                            {
                                backBrush.Blend = _stripBlend;
                                e.Graphics.FillRectangle(backBrush, backRect);
                            }

                            using (Pen darkBorder = new Pen(KCT.ToolStripBorder),
                                       lightBorder = new Pen(KCT.ToolStripGradientBegin))
                            {
                                e.Graphics.DrawLine(lightBorder, 1, 0, e.ToolStrip.Width - 2, 0);
                                e.Graphics.DrawLine(lightBorder, 1, e.ToolStrip.Height - 2, e.ToolStrip.Width - 2, e.ToolStrip.Height - 2);
                                e.Graphics.DrawLine(darkBorder, e.ToolStrip.Width - 1, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region OnRenderToolStripBorder
        /// <summary>
        /// Raises the RenderToolStripBorder event. 
        /// </summary>
        /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                // If there is a connected area to be drawn
                if (!e.ConnectedArea.IsEmpty)
                    using (SolidBrush excludeBrush = new SolidBrush(KCT.ToolStripDropDownBackground))
                        e.Graphics.FillRectangle(excludeBrush, e.ConnectedArea);

                // Create border and clipping paths
                using (GraphicsPath borderPath = CreateBorderPath(e.AffectedBounds, e.ConnectedArea, _cutContextMenu),
                                    insidePath = CreateInsideBorderPath(e.AffectedBounds, e.ConnectedArea, _cutContextMenu),
                                      clipPath = CreateClipBorderPath(e.AffectedBounds, e.ConnectedArea, _cutContextMenu))
                {
                    // Create the different pen colors we need
                    using (Pen borderPen = new Pen(KCT.MenuBorder),
                               insidePen = new Pen(KCT.ToolStripDropDownBackground))
                    {
                        // Clip all drawing to within the border path
                        using (Clipping clipping = new Clipping(e.Graphics, clipPath))
                        {
                            // Drawing with anti aliasing to create smoother appearance
                            using (AntiAlias aa = new AntiAlias(e.Graphics))
                            {
                                // Draw the inside area first
                                e.Graphics.DrawPath(insidePen, insidePath);

                                // Draw the border area second, so any overlapping gives it priority
                                e.Graphics.DrawPath(borderPen, borderPath);
                            }

                            // Draw the pixel at the bottom right of the context menu
                            e.Graphics.DrawLine(borderPen, e.AffectedBounds.Right, e.AffectedBounds.Bottom,
                                                           e.AffectedBounds.Right - 1, e.AffectedBounds.Bottom - 1);
                        }
                    }
                }
            }
            else if (e.ToolStrip is StatusStrip)
            {
                // Draw two lines at top of the status strip
                using (Pen darkBorder = new Pen(KCT.ToolStripBorder),
                           lightBorder = new Pen(KCT.SeparatorLight))
                {
                    e.Graphics.DrawLine(darkBorder, 0, 0, e.ToolStrip.Width - 1, 0);
                    e.Graphics.DrawLine(lightBorder, 0, 1, e.ToolStrip.Width - 1, 1);
                }
            }
            else
            {
                base.OnRenderToolStripBorder(e);
            }
        }
        #endregion
        
        #region OnRenderImageMargin
        /// <summary>
        /// Raises the RenderImageMargin event. 
        /// </summary>
        /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            if ((e.ToolStrip is ContextMenuStrip) ||
                (e.ToolStrip is ToolStripDropDownMenu))
            {
                // Start with the total margin area
                Rectangle marginRect = e.AffectedBounds;

                // Do we need to draw with separator on the opposite edge?
                bool rtl = (e.ToolStrip.RightToLeft == RightToLeft.Yes);

                marginRect.Y += _marginInset;
                marginRect.Height -= _marginInset * 2;

                // Reduce so it is inside the border
                if (!rtl)
                    marginRect.X += _marginInset;
                else
                    marginRect.X += _marginInset / 2;

                using (Pen marginPen = new Pen(Color.FromArgb(80, KCT.MenuBorder)))
                {
                    if (!rtl)
                        e.Graphics.DrawLine(marginPen, marginRect.Right, marginRect.Top, marginRect.Right, marginRect.Bottom);
                    else
                        e.Graphics.DrawLine(marginPen, marginRect.Left - 1, marginRect.Top, marginRect.Left - 1, marginRect.Bottom);
                }
            }
            else
            {
                base.OnRenderImageMargin(e);
            }
        }
        #endregion

        #region Implementation
        private void UpdateCache()
        {
            // Only need to create the cache objects first time around
            if (_gradientSplit == null)
            {
                _gradientSplit = new GradientItemColorsSplit(KCT.ButtonSelectedBorder,
                                                             KCT.ButtonSelectedGradientBegin,
                                                             KCT.ButtonSelectedGradientEnd);

                _gradientTracking = new GradientItemColorsTracking(KCT.ButtonSelectedBorder,
                                                                   KCT.ButtonSelectedGradientBegin,
                                                                   KCT.ButtonSelectedGradientEnd);

                _gradientPressed = new GradientItemColorsPressed(KCT.ButtonPressedBorder,
                                                                 KCT.ButtonPressedGradientBegin,
                                                                 KCT.ButtonPressedGradientEnd);

                _gradientChecked = new GradientItemColorsChecked(KCT.ButtonPressedBorder,
                                                                 KCT.ButtonCheckedGradientBegin,
                                                                 KCT.ButtonCheckedGradientEnd);

                _gradientCheckedTracking = new GradientItemColorsCheckedTracking(KCT.ButtonSelectedBorder,
                                                                                 KCT.ButtonPressedGradientBegin,
                                                                                 KCT.ButtonCheckedGradientEnd);
            }
        }

        private void RenderToolButtonBackground(Graphics g,
                                                ToolStripButton button,
                                                ToolStrip toolstrip)
        {
            // We only draw a background if the item is selected or being pressed
            if (button.Enabled)
            {
                // Ensure we have cached the objects we need
                UpdateCache();

                if (button.Checked)
                {
                    if (button.Pressed)
                        DrawGradientToolItem(g, button, _gradientPressed);
                    else if (button.Selected)
                        DrawGradientToolItem(g, button, _gradientCheckedTracking);
                    else
                        DrawGradientToolItem(g, button, _gradientChecked);
                }
                else
                {
                    if (button.Pressed)
                        DrawGradientToolItem(g, button, _gradientPressed);
                    else if (button.Selected)
                        DrawGradientToolItem(g, button, _gradientTracking);
                }
            }
            else
            {
                if (button.Selected)
                {
                    // Get the mouse position in tool strip coordinates
                    Point mousePos = toolstrip.PointToClient(Control.MousePosition);

                    // If the mouse is not in the item area, then draw disabled
                    if (!button.Bounds.Contains(mousePos))
                        DrawGradientToolItem(g, button, _disabledItem);
                }
            }
        }

        private void RenderToolDropButtonBackground(Graphics g,
                                                    ToolStripItem item,
                                                    ToolStrip toolstrip)
        {
            // We only draw a background if the item is selected or being pressed
            if (item.Selected || item.Pressed)
            {
                if (item.Enabled)
                {
                    if (item.Pressed)
                        DrawContextMenuHeader(g, item);
                    else
                    {
                        // Ensure we have cached the objects we need
                        UpdateCache();

                        DrawGradientToolItem(g, item, _gradientTracking);
                    }
                }
                else
                {
                    // Get the mouse position in tool strip coordinates
                    Point mousePos = toolstrip.PointToClient(Control.MousePosition);

                    // If the mouse is not in the item area, then draw disabled
                    if (!item.Bounds.Contains(mousePos))
                        DrawGradientToolItem(g, item, _disabledItem);
                }
            }
        }

        private void DrawGradientToolSplitItem(Graphics g,
                                               ToolStripSplitButton splitButton,
                                               GradientItemColors colorsButton,
                                               GradientItemColors colorsDrop,
                                               GradientItemColors colorsSplit)
        {
            // Create entire area and just the drop button area rectangles
            Rectangle backRect = new Rectangle(Point.Empty, splitButton.Bounds.Size);
            Rectangle backRectDrop = splitButton.DropDownButtonBounds;

            // Cannot paint zero sized areas
            if ((backRect.Width > 0) && (backRectDrop.Width > 0) &&
                (backRect.Height > 0) && (backRectDrop.Height > 0))
            {
                // Area that is the normal button starts as everything
                Rectangle backRectButton = backRect;

                // The X offset to draw the split line
                int splitOffset;

                // Is the drop button on the right hand side of entire area?
                if (backRectDrop.X > 0)
                {
                    backRectButton.Width = backRectDrop.Left;
                    backRectDrop.X -= 1;
                    backRectDrop.Width++;
                    splitOffset = backRectDrop.X;
                }
                else
                {
                    backRectButton.Width -= backRectDrop.Width - 2;
                    backRectButton.X = backRectDrop.Right - 1;
                    backRectDrop.Width++;
                    splitOffset = backRectDrop.Right - 1;
                }

                // Create border path around the item
                using (GraphicsPath borderPath = CreateBorderPath(backRect, _cutItemMenu))
                {
                    // Draw the normal button area background
                    colorsButton.DrawBack(g, backRectButton);

                    // Draw the drop button area background
                    colorsDrop.DrawBack(g, backRectDrop);

                    // Draw the split line between the areas
                    using (LinearGradientBrush splitBrush = new LinearGradientBrush(new Rectangle(backRect.X + splitOffset, backRect.Top, 1, backRect.Height + 1),
                                                                                    colorsSplit.Border1, colorsSplit.Border2, 90f))
                    {
                        // Convert the brush to a pen for DrawPath call
                        using (Pen splitPen = new Pen(splitBrush))
                            g.DrawLine(splitPen, backRect.X + splitOffset, backRect.Top + 1, backRect.X + splitOffset, backRect.Bottom - 1);
                    }

                    // Draw the border of the entire item
                    colorsButton.DrawBorder(g, backRect);
                }
            }
        }

        private void DrawContextMenuHeader(Graphics g, ToolStripItem item)
        {
            // Get the rectangle that is the items area
            Rectangle itemRect = new Rectangle(Point.Empty, item.Bounds.Size);

            // Create border and clipping paths
            using (GraphicsPath borderPath = CreateBorderPath(itemRect, _cutHeaderMenu),
                                insidePath = CreateInsideBorderPath(itemRect, _cutHeaderMenu),
                                  clipPath = CreateClipBorderPath(itemRect, _cutHeaderMenu))
            {
                // Clip all drawing to within the border path
                using (Clipping clipping = new Clipping(g, clipPath))
                {
                    // Draw the entire background area first
                    using (SolidBrush backBrush = new SolidBrush(KCT.ToolStripDropDownBackground))
                        g.FillPath(backBrush, borderPath);

                    // Draw the border
                    using (Pen borderPen = new Pen(KCT.MenuBorder))
                        g.DrawPath(borderPen, borderPath);
                }
            }
        }

        private void DrawGradientToolItem(Graphics g,
                                          ToolStripItem item,
                                          GradientItemColors colors)
        {
            // Perform drawing into the entire background of the item
            colors.DrawItem(g, new Rectangle(Point.Empty, item.Bounds.Size));
        }

        private void RenderToolSplitButtonBackground(Graphics g,
                                                     ToolStripSplitButton splitButton,
                                                     ToolStrip toolstrip)
        {
            // We only draw a background if the item is selected or being pressed
            if (splitButton.Selected || splitButton.Pressed)
            {
                if (splitButton.Enabled)
                {
                    // Ensure we have cached the objects we need
                    UpdateCache();

                    if (!splitButton.Pressed && splitButton.ButtonPressed)
                        DrawGradientToolSplitItem(g, splitButton, _gradientPressed, _gradientTracking, _gradientSplit);
                    else if (splitButton.Pressed && !splitButton.ButtonPressed)
                        DrawContextMenuHeader(g, splitButton);
                    else
                        DrawGradientToolSplitItem(g, splitButton, _gradientTracking, _gradientTracking, _gradientSplit);
                }
                else
                {
                    // Get the mouse position in tool strip coordinates
                    Point mousePos = toolstrip.PointToClient(Control.MousePosition);

                    // If the mouse is not in the item area, then draw disabled
                    if (!splitButton.Bounds.Contains(mousePos))
                        DrawGradientToolItem(g, splitButton, _disabledItem);
                }
            }

        }

        private void DrawGradientContextMenuItem(Graphics g,
                                                 ToolStripItem item,
                                                 GradientItemColors colors)
        {
            // Do we need to draw with separator on the opposite edge?
            Rectangle backRect = new Rectangle(2, 0, item.Bounds.Width - 3, item.Bounds.Height);

            // Perform actual drawing into the background
            colors.DrawItem(g, backRect);
        }

        private void DrawGripGlyph(Graphics g,
                                   int x,
                                   int y,
                                   Brush darkBrush,
                                   Brush lightBrush)
        {
            g.FillRectangle(lightBrush, x + _gripOffset, y + _gripOffset, _gripSquare, _gripSquare);
            g.FillRectangle(darkBrush, x, y, _gripSquare, _gripSquare);
        }

        private void DrawContextMenuSeparator(Graphics g,
                                              bool vertical,
                                              Rectangle rect,
                                              int horizontalInset,
                                              bool rtl)
        {
            if (vertical)
            {
                int l = rect.Width / 2;
                int t = rect.Y;
                int b = rect.Bottom;

                using (Pen marginPen = new Pen(Color.FromArgb(80, KCT.MenuBorder)))
                {
                    marginPen.DashPattern = new float[] { 2, 2 };
                    g.DrawLine(marginPen, l, t, l, b);
                }
            }
            else
            {
                int y = rect.Height / 2;
                int l = rect.X + (rtl ? 0 : horizontalInset);
                int r = rect.Right - (rtl ? horizontalInset : 0);

                using (Pen marginPen = new Pen(Color.FromArgb(80, KCT.MenuBorder)))
                {
                    marginPen.DashPattern = new float[] { 2, 2 };
                    g.DrawLine(marginPen, l, y, r, y);
                }
            }
        }

        private void DrawToolStripSeparator(Graphics g,
                                            bool vertical,
                                            Rectangle rect,
                                            Color lightColor,
                                            Color darkColor,
                                            int horizontalInset,
                                            bool rtl)
        {
            RectangleF boundsF = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

            if (vertical)
            {
                int l = rect.Width / 2;
                int t = rect.Y;

                // Draw vertical lines centered
                using (LinearGradientBrush lightBrush = new LinearGradientBrush(boundsF, Color.Transparent, lightColor, 90),
                                           darkBrush = new LinearGradientBrush(boundsF, Color.Transparent, darkColor, 90))
                {
                    lightBrush.Blend = _separatorLightBlend;
                    darkBrush.Blend = _separatorDarkBlend;
                    g.FillRectangle(lightBrush, l - 1, t, 3, rect.Height);
                    g.FillRectangle(darkBrush, l, t, 1, rect.Height);
                }
            }
            else
            {
                int l = rect.X;
                int t = rect.Height / 2;

                // Draw horizontal lines centered
                using (LinearGradientBrush lightBrush = new LinearGradientBrush(boundsF, Color.Transparent, lightColor, 0f),
                                           darkBrush = new LinearGradientBrush(boundsF, Color.Transparent, darkColor, 0f))
                {
                    lightBrush.Blend = _separatorLightBlend;
                    darkBrush.Blend = _separatorDarkBlend;
                    g.FillRectangle(lightBrush, l, t - 1, rect.Width, 3);
                    g.FillRectangle(darkBrush, l, t, rect.Width, 1);
                }
            }
        }

        private static GraphicsPath CreateBorderPath(Rectangle rect,
                                                     Rectangle exclude,
                                                     float cut)
        {
            // If nothing to exclude, then use quicker method
            if (exclude.IsEmpty)
                return CreateBorderPath(rect, cut);

            // Drawing lines requires we draw inside the area we want
            rect.Width--;
            rect.Height--;

            // Create an array of points to draw lines between
            List<PointF> pts = new List<PointF>();

            float l = rect.X;
            float t = rect.Y;
            float r = rect.Right;
            float b = rect.Bottom;
            float x0 = rect.X + cut;
            float x3 = rect.Right - cut;
            float y0 = rect.Y + cut;
            float y3 = rect.Bottom - cut;
            float cutBack = (cut == 0f ? 1 : cut);

            // Does the exclude intercept the top line
            if ((rect.Y >= exclude.Top) && (rect.Y <= exclude.Bottom))
            {
                float x1 = exclude.X - 1 - cut;
                float x2 = exclude.Right + cut;

                if (x0 <= x1)
                {
                    pts.Add(new PointF(x0, t));
                    pts.Add(new PointF(x1, t));
                    pts.Add(new PointF(x1 + cut, t - cutBack));
                }
                else
                {
                    x1 = exclude.X - 1;
                    pts.Add(new PointF(x1, t));
                    pts.Add(new PointF(x1, t - cutBack));
                }

                if (x3 > x2)
                {
                    pts.Add(new PointF(x2 - cut, t - cutBack));
                    pts.Add(new PointF(x2, t));
                    pts.Add(new PointF(x3, t));
                }
                else
                {
                    x2 = exclude.Right;
                    pts.Add(new PointF(x2, t - cutBack));
                    pts.Add(new PointF(x2, t));
                }
            }
            else
            {
                pts.Add(new PointF(x0, t));
                pts.Add(new PointF(x3, t));
            }

            pts.Add(new PointF(r, y0));
            pts.Add(new PointF(r, y3));
            pts.Add(new PointF(x3, b));
            pts.Add(new PointF(x0, b));
            pts.Add(new PointF(l, y3));
            pts.Add(new PointF(l, y0));

            // Create path using a simple set of lines that cut the corner
            GraphicsPath path = new GraphicsPath();

            // Add a line between each set of points
            for (int i = 1; i < pts.Count; i++)
                path.AddLine(pts[i - 1], pts[i]);

            // Add a line to join the last to the first
            path.AddLine(pts[pts.Count - 1], pts[0]);

            return path;
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

        private GraphicsPath CreateInsideBorderPath(Rectangle rect, float cut)
        {
            // Adjust rectangle to be 1 pixel inside the original area
            rect.Inflate(-1, -1);

            // Now create a path based on this inner rectangle
            return CreateBorderPath(rect, cut);
        }

        private GraphicsPath CreateInsideBorderPath(Rectangle rect,
                                                    Rectangle exclude,
                                                    float cut)
        {
            // Adjust rectangle to be 1 pixel inside the original area
            rect.Inflate(-1, -1);

            // Now create a path based on this inner rectangle
            return CreateBorderPath(rect, exclude, cut);
        }

        private GraphicsPath CreateClipBorderPath(Rectangle rect, float cut)
        {
            // Clipping happens inside the rect, so make 1 wider and taller
            rect.Width++;
            rect.Height++;

            // Now create a path based on this inner rectangle
            return CreateBorderPath(rect, cut);
        }

        private GraphicsPath CreateClipBorderPath(Rectangle rect,
                                                  Rectangle exclude,
                                                  float cut)
        {
            // Clipping happens inside the rect, so make 1 wider and taller
            rect.Width++;
            rect.Height++;

            // Now create a path based on this inner rectangle
            return CreateBorderPath(rect, exclude, cut);
        }

        private GraphicsPath CreateArrowPath(ToolStripItem item,
                                             Rectangle rect,
                                             ArrowDirection direction)
        {
            int x, y;

            // Find the correct starting position, which depends on direction
            if ((direction == ArrowDirection.Left) ||
                (direction == ArrowDirection.Right))
            {
                x = rect.Right - (rect.Width - 4) / 2;
                y = rect.Y + rect.Height / 2;
            }
            else
            {
                x = rect.X + rect.Width / 2;
                y = rect.Bottom - (rect.Height - 3) / 2;

                // The drop down button is position 1 pixel incorrectly when in RTL
                if ((item is ToolStripDropDownButton) &&
                    (item.RightToLeft == RightToLeft.Yes))
                    x++;
            }

            // Create triangle using a series of lines
            GraphicsPath path = new GraphicsPath();

            switch (direction)
            {
                case ArrowDirection.Right:
                    path.AddLine(x, y, x - 4, y - 4);
                    path.AddLine(x - 4, y - 4, x - 4, y + 4);
                    path.AddLine(x - 4, y + 4, x, y);
                    break;
                case ArrowDirection.Left:
                    path.AddLine(x - 4, y, x, y - 4);
                    path.AddLine(x, y - 4, x, y + 4);
                    path.AddLine(x, y + 4, x - 4, y);
                    break;
                case ArrowDirection.Down:
                    path.AddLine(x + 3f, y - 3f, x - 2f, y - 3f);
                    path.AddLine(x - 2f, y - 3f, x, y);
                    path.AddLine(x, y, x + 3f, y - 3f);
                    break;
                case ArrowDirection.Up:
                    path.AddLine(x + 3f, y, x - 3f, y);
                    path.AddLine(x - 3f, y, x, y - 4f);
                    path.AddLine(x, y - 4f, x + 3f, y);
                    break;
            }

            return path;
        }

        private GraphicsPath CreateTickPath(Rectangle rect)
        {
            // Get the center point of the rect
            int x = rect.X + rect.Width / 2;
            int y = rect.Y + rect.Height / 2;

            GraphicsPath path = new GraphicsPath();
            path.AddLine(x - 4, y, x - 2, y + 4);
            path.AddLine(x - 2, y + 4, x + 3, y - 5);
            return path;
        }

        private GraphicsPath CreateIndeterminatePath(Rectangle rect)
        {
            // Get the center point of the rect
            int x = rect.X + rect.Width / 2;
            int y = rect.Y + rect.Height / 2;

            GraphicsPath path = new GraphicsPath();
            path.AddLine(x - 3, y, x, y - 3);
            path.AddLine(x, y - 3, x + 3, y);
            path.AddLine(x + 3, y, x, y + 3);
            path.AddLine(x, y + 3, x - 3, y);
            return path;
        }
        #endregion
    }
}
