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
    internal class KryptonOffice2007Renderer : KryptonProfessionalRenderer
    {
        #region GradientItemColors
        private class GradientItemColors
        {
            #region Public Fields
            public Color InsideTop1;
            public Color InsideTop2;
            public Color InsideBottom1;
            public Color InsideBottom2;
            public Color FillTop1;
            public Color FillTop2;
            public Color FillBottom1;
            public Color FillBottom2;
            public Color Border1;
            public Color Border2;
            #endregion

            #region Identity
            public GradientItemColors()
            {
            }

            public GradientItemColors(Color insideTop1, Color insideTop2,
                                      Color insideBottom1, Color insideBottom2,
                                      Color fillTop1, Color fillTop2,
                                      Color fillBottom1, Color fillBottom2,
                                      Color border1, Color border2)
            {
                InsideTop1 = insideTop1;
                InsideTop2 = insideTop2;
                InsideBottom1 = insideBottom1;
                InsideBottom2 = insideBottom2;
                FillTop1 = fillTop1;
                FillTop2 = fillTop2;
                FillBottom1 = fillBottom1;
                FillBottom2 = fillBottom2;
                Border1 = border1;
                Border2 = border2;
            }
            #endregion
        }

        private class GradientItemColorsItem : GradientItemColors
        {
            /// <summary>
            /// Initialize a new instance of the GradientItemColorsItem class.
            /// </summary>
            /// <param name="border">Base border color.</param>
            /// <param name="begin">Beginning background color.</param>
            /// <param name="end">Ending background color.</param>
            public GradientItemColorsItem(Color border,
                                          Color begin,
                                          Color end)
            {
                // Calculate all colors from the provided parameters
                Border1 = CommonHelper.WhitenColor(border, 1.17f, 1.11f, 0.99f);
                Border2 = CommonHelper.WhitenColor(border, 1.32f, 1.35f, 1.26f);
                FillTop1 = CommonHelper.WhitenColor(begin, 0.71f, 0.93f, 0.72f);
                FillTop2 = begin;
                FillBottom1 = end;
                FillBottom2 = CommonHelper.WhitenColor(end, 0.71f, 0.93f, 0.71f);
                begin = CommonHelper.WhitenColor(begin, 0.94f, 0.94f, 0.73f);
                end = CommonHelper.WhitenColor(end, 0.88f, 0.88f, 0.51f);
                InsideTop1 = CommonHelper.WhitenColor(begin, 0.71f, 0.93f, 0.90f);
                InsideTop2 = begin;
                InsideBottom1 = end;
                InsideBottom2 = CommonHelper.WhitenColor(end, 0.71f, 0.97f, 1.11f);
            }
        }

        private class GradientItemColorsTracking : GradientItemColors
        {
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
                Border1 = CommonHelper.WhitenColor(border, 0.85f, 0.85f, 0.85f);
                Border2 = border;
                FillTop1 = CommonHelper.WhitenColor(begin, 0.71f, 0.93f, 0.72f);
                FillTop2 = begin;
                FillBottom1 = end;
                FillBottom2 = CommonHelper.WhitenColor(end, 0.71f, 0.93f, 0.71f);
                begin = CommonHelper.WhitenColor(begin, 0.94f, 0.94f, 0.73f);
                end = CommonHelper.WhitenColor(end, 0.88f, 0.88f, 0.51f);
                InsideTop1 = CommonHelper.WhitenColor(begin, 0.71f, 0.93f, 0.90f);
                InsideTop2 = begin;
                InsideBottom1 = end;
                InsideBottom2 = CommonHelper.WhitenColor(end, 0.71f, 0.97f, 1.11f);
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
                Border1 = CommonHelper.WhitenColor(border, 0.85f, 0.85f, 0.85f);
                Border2 = border;
                FillTop1 = CommonHelper.WhitenColor(begin, 0.99f, 0.89f, 0.89f);
                FillTop2 = begin;
                FillBottom1 = end;
                FillBottom2 = CommonHelper.WhitenColor(end, 0.98f, 0.68f, 0.45f);
                begin = CommonHelper.WhitenColor(begin, 1.02f, 1.00f, 2.48f);
                end = CommonHelper.WhitenColor(end, 1.02f, 0.91f, 2.54f);
                InsideTop1 = CommonHelper.WhitenColor(begin, 1.06f, 0.97f, 0.40f);
                InsideTop2 = begin;
                InsideBottom1 = end;
                InsideBottom2 = CommonHelper.WhitenColor(end, 0.97f, 0.90f, 1.40f);
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
                Border1 = CommonHelper.WhitenColor(border, 0.85f, 0.85f, 0.85f);
                Border2 = border;
                FillTop1 = CommonHelper.WhitenColor(begin, 0.99f, 0.84f, 0.59f);
                FillTop2 = begin;
                FillBottom1 = end;
                FillBottom2 = CommonHelper.WhitenColor(end, 0.99f, 0.67f, 0.31f);
                begin = CommonHelper.WhitenColor(begin, 1.01f, 0.92f, 1.07f);
                end = CommonHelper.WhitenColor(end, 1.01f, 0.84f, 0.66f);
                InsideTop1 = CommonHelper.WhitenColor(begin, 1.00f, 1.01f, 0.90f);
                InsideTop2 = begin;
                InsideBottom1 = end;
                InsideBottom2 = CommonHelper.WhitenColor(end, 0.97f, 0.91f, 1.65f);
            }
        }

        private class GradientItemColorsCheckedTracking : GradientItemColors
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
            {
                // Calculate all colors from the provided parameters
                Border1 = CommonHelper.WhitenColor(border, 0.85f, 0.85f, 0.85f);
                Border2 = border;
                FillTop1 = CommonHelper.WhitenColor(begin, 0.99f, 0.88f, 0.89f);
                FillTop2 = begin;
                FillBottom1 = end;
                FillBottom2 = CommonHelper.WhitenColor(end, 0.99f, 0.67f, 0.31f);
                begin = CommonHelper.WhitenColor(begin, 1.01f, 0.80f, 0.94f);
                end = CommonHelper.WhitenColor(end, 1.01f, 0.80f, 0.59f);
                InsideTop1 = CommonHelper.WhitenColor(begin, 1.00f, 1.01f, 0.91f);
                InsideTop2 = begin;
                InsideBottom1 = end;
                InsideBottom2 = CommonHelper.WhitenColor(end, 0.97f, 0.91f, 1.54f);
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
        private static readonly float _cutMenuItemBack = 1.2f;
        private static readonly float _cutToolItemMenu = 1.0f;
        private static readonly Blend _statusStripBlend;
        private static readonly Color _disabled = Color.FromArgb(167, 167, 167);
        private static GradientItemColors _disabledItem = new GradientItemColors(Color.FromArgb(250, 250, 250), Color.FromArgb(243, 243, 243), 
                                                                                 Color.FromArgb(236, 236, 236), Color.FromArgb(230, 230, 230), 
                                                                                 Color.FromArgb(243, 243, 243), Color.FromArgb(224, 224, 224), 
                                                                                 Color.FromArgb(200, 200, 200), Color.FromArgb(210, 210, 210), 
                                                                                 Color.FromArgb(212, 212, 212), Color.FromArgb(195, 195, 195));
        #endregion

        #region Instance Fields
        private GradientItemColorsItem _gradientItem;
        private GradientItemColorsTracking _gradientTracking;
        private GradientItemColorsPressed _gradientPressed;
        private GradientItemColorsChecked _gradientChecked;
        private GradientItemColorsCheckedTracking _gradientCheckedTracking;
        #endregion

        #region Identity
        static KryptonOffice2007Renderer()
        {
            // One time creation of the blend for the status strip gradient brush
            _statusStripBlend = new Blend();
            _statusStripBlend.Positions = new float[] { 0.0f, 0.25f, 0.25f, 0.57f, 0.86f, 1.0f };
            _statusStripBlend.Factors = new float[] { 0.1f, 0.6f, 1.0f, 0.4f, 0.0f, 0.95f };
        }

        /// <summary>
        /// Initialise a new instance of the KryptonOffice2007Renderer class.
        /// </summary>
        /// <param name="kct">Source for text colors.</param>
        public KryptonOffice2007Renderer(KryptonColorTable kct)
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

                    Color color1 = (e.Item.Enabled ? KCT.ToolStripText : _disabled);
                    Color color2 = (e.Item.Enabled ? CommonHelper.WhitenColor(KCT.ToolStripText, 0.7f, 0.7f, 0.7f) : _disabled);

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
                using (GraphicsPath borderPath = CreateBorderPath(checkBox, _cutMenuItemBack))
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
                    else if ((e.ToolStrip is StatusStrip) && !e.Item.Pressed && !e.Item.Selected)
                        e.TextColor = KCT.StatusStripText;
                    else if ((e.ToolStrip is ContextMenuStrip) && !e.Item.Pressed && !e.Item.Selected)
                        e.TextColor = KCT.MenuItemText;
                    else if ((e.ToolStrip is ToolStripDropDownMenu) && !e.Item.Pressed && !e.Item.Selected)
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
                                DrawGradientContextMenuItem(e.Graphics, e.Item, _gradientItem);
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
                int y = e.AffectedBounds.Bottom - _gripSize * 2 + 1;

                // Draw three lines of grips
                for (int i = _gripLines; i >= 1; i--)
                {
                    // Find the rightmost grip position on the line
                    int x = (rtl ? e.AffectedBounds.Left + 1 :
                                   e.AffectedBounds.Right - _gripSize * 2 + 1);

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
                // Create the light and dark line pens
                using (Pen lightPen = new Pen(CommonHelper.WhitenColor(KCT.ToolStripDropDownBackground, 1.02f, 1.02f, 1.02f)),
                            darkPen = new Pen(CommonHelper.WhitenColor(KCT.ToolStripDropDownBackground, 1.26f, 1.26f, 1.26f)))
                {
                    DrawSeparator(e.Graphics, e.Vertical, e.Item.Bounds,
                                  lightPen, darkPen, _separatorInset,
                                  (e.ToolStrip.RightToLeft == RightToLeft.Yes));
                }
            }
            else if (e.ToolStrip is StatusStrip)
            {
                // Create the light and dark line pens
                using (Pen lightPen = new Pen(KCT.SeparatorLight),
                            darkPen = new Pen(KCT.SeparatorDark))
                {
                    DrawSeparator(e.Graphics, e.Vertical, e.Item.Bounds,
                                  lightPen, darkPen, 0, false);
                }
            }
            else
            {
                base.OnRenderSeparator(e);
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

                // We do not paint the top two pixel lines, so are drawn by the status strip border render method
                RectangleF backRect = new RectangleF(0, 1.5f, e.ToolStrip.Width, e.ToolStrip.Height - 2);

                Form owner = e.ToolStrip.FindForm();

                // Check if the status strip is inside a KryptonForm and using the Office 2007 renderer, in 
                // which case we want to extend the drawing down into the border area for an integrated look
                if ((owner != null) && 
                    (owner is KryptonForm) &&
                    e.ToolStrip.Visible &&
                    (e.ToolStrip.Dock == DockStyle.Bottom) &&
                    (e.ToolStrip.Bottom == owner.ClientSize.Height) &&
                    (e.ToolStrip.RenderMode == ToolStripRenderMode.ManagerRenderMode) &&
                    (ToolStripManager.Renderer is KryptonOffice2007Renderer))
                {
                    // Get the window borders
                    KryptonForm kryptonForm = (KryptonForm)owner;

                    // Finally check that the actual form is using custom chrome
                    if (kryptonForm.ApplyCustomChrome)
                    {
                        // Extend down into the bottom border
                        backRect.Height += kryptonForm.RealWindowBorders.Bottom;
                    }
                }

                // Cannot paint a zero sized area
                if ((backRect.Width > 0) && (backRect.Height > 0))
                {
                    using (LinearGradientBrush backBrush = new LinearGradientBrush(backRect,
                                                                                   KCT.StatusStripGradientBegin,
                                                                                   KCT.StatusStripGradientEnd,
                                                                                   90f))
                    {
                        backBrush.Blend = _statusStripBlend;
                        e.Graphics.FillRectangle(backBrush, backRect);
                    }
                }
            }
            else
            {
                if (e.ToolStrip is MenuStrip)
                {
                    // Make sure the font is current
                    if (e.ToolStrip.Font != KCT.MenuStripFont)
                        e.ToolStrip.Font = KCT.MenuStripFont;
                }
                else
                {
                    // Make sure the font is current
                    if (e.ToolStrip.Font != KCT.ToolStripFont)
                        e.ToolStrip.Font = KCT.ToolStripFont;
                }

                base.OnRenderToolStripBackground(e);
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

                // Draw the entire margine area in a solid color
                using (SolidBrush backBrush = new SolidBrush(KCT.ImageMarginGradientBegin))
                    e.Graphics.FillRectangle(backBrush, marginRect);

                // Create the light and dark line pens
                using (Pen lightPen = new Pen(CommonHelper.WhitenColor(KCT.ToolStripDropDownBackground, 1.02f, 1.02f, 1.02f)),
                            darkPen = new Pen(CommonHelper.WhitenColor(KCT.ToolStripDropDownBackground, 1.26f, 1.26f, 1.26f)))
                {
                    if (!rtl)
                    {
                        // Draw the light and dark lines on the right hand side
                        e.Graphics.DrawLine(lightPen, marginRect.Right, marginRect.Top, marginRect.Right, marginRect.Bottom);
                        e.Graphics.DrawLine(darkPen, marginRect.Right - 1, marginRect.Top, marginRect.Right - 1, marginRect.Bottom);
                    }
                    else
                    {
                        // Draw the light and dark lines on the left hand side
                        e.Graphics.DrawLine(lightPen, marginRect.Left - 1, marginRect.Top, marginRect.Left - 1, marginRect.Bottom);
                        e.Graphics.DrawLine(darkPen, marginRect.Left, marginRect.Top, marginRect.Left, marginRect.Bottom);
                    }
                }
            }
            else
            {
                base.OnRenderImageMargin(e);
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
                               insidePen = new Pen(CommonHelper.WhitenColor(KCT.ToolStripDropDownBackground, 1.02f, 1.02f, 1.02f)))
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
                using (Pen darkBorder = new Pen(CommonHelper.WhitenColor(KCT.StatusStripGradientEnd, 1.6f, 1.6f, 1.6f)),
                          lightBorder = new Pen(ControlPaint.LightLight(KCT.StatusStripGradientBegin)))
                {
                    e.Graphics.DrawLine(darkBorder, 0, 0, e.ToolStrip.Width, 0);
                    e.Graphics.DrawLine(lightBorder, 0, 1, e.ToolStrip.Width, 1);
                }
            }
            else
            {
                base.OnRenderToolStripBorder(e);
            }
        }
        #endregion

        #region Implementation
        private void UpdateCache()
        {
            // Only need to create the cache objects first time around
            if (_gradientItem == null)
            {
                _gradientItem = new GradientItemColorsItem(KCT.CheckBackground,
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
                using (GraphicsPath borderPath = CreateBorderPath(backRect, _cutMenuItemBack))
                {
                    // Draw the normal button area background
                    DrawGradientBack(g, backRectButton, colorsButton);

                    // Draw the drop button area background
                    DrawGradientBack(g, backRectDrop, colorsDrop);

                    // Draw the split line between the areas
                    using (LinearGradientBrush splitBrush = new LinearGradientBrush(new Rectangle(backRect.X + splitOffset, backRect.Top, 1, backRect.Height + 1),
                                                                                    colorsSplit.Border1, colorsSplit.Border2, 90f))
                    {
                        // Sigma curve, so go from color1 to color2 and back to color1 again
                        splitBrush.SetSigmaBellShape(0.5f);

                        // Convert the brush to a pen for DrawPath call
                        using (Pen splitPen = new Pen(splitBrush))
                            g.DrawLine(splitPen, backRect.X + splitOffset, backRect.Top + 1, backRect.X + splitOffset, backRect.Bottom - 1);
                    }

                    // Draw the border of the entire item
                    DrawGradientBorder(g, backRect, colorsButton);
                }
            }
        }

        private void DrawContextMenuHeader(Graphics g, ToolStripItem item)
        {
            // Get the rectangle that is the items area
            Rectangle itemRect = new Rectangle(Point.Empty, item.Bounds.Size);

            // Create border and clipping paths
            using (GraphicsPath borderPath = CreateBorderPath(itemRect, _cutToolItemMenu),
                                insidePath = CreateInsideBorderPath(itemRect, _cutToolItemMenu),
                                  clipPath = CreateClipBorderPath(itemRect, _cutToolItemMenu))
            {
                // Clip all drawing to within the border path
                using (Clipping clipping = new Clipping(g, clipPath))
                {
                    // Draw the entire background area first
                    using (SolidBrush backBrush = new SolidBrush(CommonHelper.WhitenColor(KCT.ToolStripDropDownBackground, 1.02f, 1.02f, 1.02f)))
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
            DrawGradientItem(g, new Rectangle(Point.Empty, item.Bounds.Size), colors);
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
                        DrawGradientToolSplitItem(g, splitButton, _gradientPressed, _gradientTracking, _gradientItem);
                    else if (splitButton.Pressed && !splitButton.ButtonPressed)
                        DrawContextMenuHeader(g, splitButton);
                    else
                        DrawGradientToolSplitItem(g, splitButton, _gradientTracking, _gradientTracking, _gradientItem);
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
            DrawGradientItem(g, backRect, colors);
        }

        private static void DrawGradientItem(Graphics g,
                                             Rectangle backRect,
                                             GradientItemColors colors)
        {
            // Cannot paint a zero sized area
            if ((backRect.Width > 0) && (backRect.Height > 0))
            {
                // Draw the background of the entire item
                DrawGradientBack(g, backRect, colors);

                // Draw the border of the entire item
                DrawGradientBorder(g, backRect, colors);
            }
        }

        private static void DrawGradientBack(Graphics g,
                                             Rectangle backRect,
                                             GradientItemColors colors)
        {
            // Reduce rect draw drawing inside the border
            backRect.Inflate(-1, -1);

            int y2 = backRect.Height / 2;
            Rectangle backRect1 = new Rectangle(backRect.X, backRect.Y, backRect.Width, y2);
            Rectangle backRect2 = new Rectangle(backRect.X, backRect.Y + y2, backRect.Width, backRect.Height - y2);
            Rectangle backRect1I = backRect1;
            Rectangle backRect2I = backRect2;
            backRect1I.Inflate(1, 1);
            backRect2I.Inflate(1, 1);

            using (LinearGradientBrush insideBrush1 = new LinearGradientBrush(backRect1I, colors.InsideTop1, colors.InsideTop2, 90f),
                                       insideBrush2 = new LinearGradientBrush(backRect2I, colors.InsideBottom1, colors.InsideBottom2, 90f))
            {
                g.FillRectangle(insideBrush1, backRect1);
                g.FillRectangle(insideBrush2, backRect2);
            }

            y2 = backRect.Height / 2;
            backRect1 = new Rectangle(backRect.X, backRect.Y, backRect.Width, y2);
            backRect2 = new Rectangle(backRect.X, backRect.Y + y2, backRect.Width, backRect.Height - y2);
            backRect1I = backRect1;
            backRect2I = backRect2;
            backRect1I.Inflate(1, 1);
            backRect2I.Inflate(1, 1);

            using (LinearGradientBrush fillBrush1 = new LinearGradientBrush(backRect1I, colors.FillTop1, colors.FillTop2, 90f),
                                       fillBrush2 = new LinearGradientBrush(backRect2I, colors.FillBottom1, colors.FillBottom2, 90f))
            {
                // Reduce rect one more time for the innermost drawing
                backRect.Inflate(-1, -1);

                y2 = backRect.Height / 2;
                backRect1 = new Rectangle(backRect.X, backRect.Y, backRect.Width, y2);
                backRect2 = new Rectangle(backRect.X, backRect.Y + y2, backRect.Width, backRect.Height - y2);

                g.FillRectangle(fillBrush1, backRect1);
                g.FillRectangle(fillBrush2, backRect2);
            }
        }

        private static void DrawGradientBorder(Graphics g,
                                               Rectangle backRect,
                                               GradientItemColors colors)
        {
            // Drawing with anti aliasing to create smoother appearance
            using (AntiAlias aa = new AntiAlias(g))
            {
                Rectangle backRectI = backRect;
                backRectI.Inflate(1, 1);

                // Finally draw the border around the menu item
                using (LinearGradientBrush borderBrush = new LinearGradientBrush(backRectI, colors.Border1, colors.Border2, 90f))
                {
                    // Sigma curve, so go from color1 to color2 and back to color1 again
                    borderBrush.SetSigmaBellShape(0.5f);

                    // Convert the brush to a pen for DrawPath call
                    using (Pen borderPen = new Pen(borderBrush))
                    {
                        // Create border path around the entire item
                        using (GraphicsPath borderPath = CreateBorderPath(backRect, _cutMenuItemBack))
                            g.DrawPath(borderPen, borderPath);
                    }
                }
            }
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

        private void DrawSeparator(Graphics g,
                                   bool vertical,
                                   Rectangle rect,
                                   Pen lightPen,
                                   Pen darkPen,
                                   int horizontalInset,
                                   bool rtl)
        {
            if (vertical)
            {
                int l = rect.Width / 2;
                int t = rect.Y;
                int b = rect.Bottom;

                // Draw vertical lines centered
                g.DrawLine(darkPen, l, t, l, b);
                g.DrawLine(lightPen, l + 1, t, l + 1, b);
            }
            else
            {
                int y = rect.Height / 2;
                int l = rect.X + (rtl ? 0 : horizontalInset);
                int r = rect.Right - (rtl ? horizontalInset : 0);

                // Draw horizontal lines centered
                g.DrawLine(darkPen, l, y, r, y);
                g.DrawLine(lightPen, l, y + 1, r, y + 1);
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
