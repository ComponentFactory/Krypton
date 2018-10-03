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
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class DesignTimeDraw
    {
        #region Statis Fields
        private static readonly int DESIGN_FLAP_WIDTH = 12;
        private static readonly int DESIGN_SEP_WIDTH = 6;
        #endregion

        #region FlapWidth
        /// <summary>
        /// Gets the width of the design time flap.
        /// </summary>
        public static int FlapWidth
        {
            get { return DESIGN_FLAP_WIDTH; }
        }
        #endregion

        #region SepWidth
        /// <summary>
        /// Gets the width of the design time separation.
        /// </summary>
        public static int SepWidth
        {
            get { return DESIGN_SEP_WIDTH; }
        }
        #endregion

        #region DrawArea
        /// <summary>
        /// Draw a design area with a flap on the left hand edge.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="clientRect">Client rectangle of the source view.</param>
        /// <param name="state">State of element.</param>
        public static void DrawArea(KryptonRibbon ribbon,
                                    RenderContext context,
                                    Rectangle clientRect,
                                    PaletteState state)
        {
            Color c;

            if (state == PaletteState.Normal)
                c = ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorDark(PaletteState.Normal);
            else
                c = ribbon.StateCommon.RibbonGroupButton.Back.GetBackColor1(PaletteState.Tracking);

            // Draw entire area in color
            using (SolidBrush darkBrush = new SolidBrush(c))
                context.Graphics.FillRectangle(darkBrush, clientRect);
        }
        #endregion
 
        #region DrawFlapArea
        /// <summary>
        /// Draw a design area with a flap on the left hand edge.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="clientRect">Client rectangle of the source view.</param>
        /// <param name="state">State of element.</param>
        public static void DrawFlapArea(KryptonRibbon ribbon,
                                        RenderContext context,
                                        Rectangle clientRect,
                                        PaletteState state)
        {
            Color c;

            if (state == PaletteState.Normal)
                c = ControlPaint.Dark(ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorDark(PaletteState.Normal));
            else
                c = ribbon.StateCommon.RibbonGroupButton.Back.GetBackColor1(PaletteState.Tracking);

            // Draw border around entire area
            Rectangle drawRect = clientRect;
            drawRect.Width -= DESIGN_SEP_WIDTH;
            drawRect.Height--;
            drawRect.X++;
            using (Pen darkPen = new Pen(c))
                context.Graphics.DrawRectangle(darkPen, drawRect);

            // Draw the flap in the dark color
            drawRect.Width = DESIGN_FLAP_WIDTH - 2;
            using (SolidBrush darkBrush = new SolidBrush(c))
                context.Graphics.FillRectangle(darkBrush, drawRect);
        }
        #endregion
    }
}
