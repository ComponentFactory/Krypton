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
using System.Windows.Forms.VisualStyles;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Provides the standard renderer that honors all palette properties.
	/// </summary>
	public class RenderStandard : RenderBase
    {
        #region Static Fields
        // Constants
        private static readonly int _dragArrowWidth = 13;
        private static readonly int _dragArrowHeight = 7;
        private static readonly int _dragArrowGap = 4;
        private static readonly int _spacingTabDockOutsize = 3;
        private static readonly int _spacingTabOutsizePadding = 1;
        private static readonly int _spacingTabSquareEqualSmall = -1;
        private static readonly int _spacingTabSquareEqualMedium = 2;
        private static readonly int _spacingTabSquareEqualLarge = 5;
        private static readonly int _spacingTabSquareOutsizeSmall = -5;
        private static readonly int _spacingTabSquareOutsizeMedium = -3;
        private static readonly int _spacingTabSquareOutsizeLarge = 2;
        private static readonly int _spacingTabRoundedEqualSmall = -1;
        private static readonly int _spacingTabRoundedEqualMedium = 2;
        private static readonly int _spacingTabRoundedEqualLarge = 5;
        private static readonly int _spacingTabRoundedOutsizeSmall = -5;
        private static readonly int _spacingTabRoundedOutsizeMedium = -3;
        private static readonly int _spacingTabRoundedOutsizeLarge = 2;
        private static readonly int _spacingTabRoundedCorner = 2;
        private static readonly int _spacingTabSlantEqual = -7;
        private static readonly int _spacingTabSlantEqualBoth = -17;
        private static readonly int _spacingTabSlantOutsize = -11;
        private static readonly int _spacingTabSlantPadding = 12;
        private static readonly int _spacingTabOneNote = -14;
        private static readonly int _spacingTabOneNoteLPI = 12;
        private static readonly int _spacingTabOneNoteRPI = 19;
        private static readonly int _spacingTabOneNoteTPI = 5;
        private static readonly int _spacingTabOneNoteBPI = 0;
        private static readonly int _spacingTabOneNoteLPS = 4;
        private static readonly int _spacingTabOneNoteRPS = 24;
        private static readonly int _spacingTabOneNoteTPS = 3;
        private static readonly int _spacingTabOneNoteBPS = 2;
        private static readonly int _spacingTabSmoothE = -6;
        private static readonly int _spacingTabSmoothO = -14;
        private static readonly int _spacingTabSmoothLRE = 5;
        private static readonly int _spacingTabSmoothTE = 3;
        private static readonly int _spacingTabSmoothLRO = 9;
        private static readonly int _spacingTabSmoothTO = 7;
        private static readonly int _groupFrameTitleHeight = 8;
        private static readonly float _groupGradientTwo = 0.16f;
        private static readonly float _groupGradientFrame = 0.32f;

        private static readonly Color _darken5 = Color.FromArgb(5, Color.Black);
        private static readonly Color _darken8 = Color.FromArgb(8, Color.Black);
        private static readonly Color _darken12 = Color.FromArgb(12, Color.Black);
        private static readonly Color _darken16 = Color.FromArgb(16, Color.Black);
        private static readonly Color _darken18 = Color.FromArgb(18, Color.Black);
        private static readonly Color _darken38 = Color.FromArgb(38, Color.Black);
        private static readonly Color _whiten200 = Color.FromArgb(200, Color.White);
        private static readonly Color _whiten160 = Color.FromArgb(160, Color.White);
        private static readonly Color _whiten128 = Color.FromArgb(128, Color.White);
        private static readonly Color _whiten120 = Color.FromArgb(120, Color.White);
        private static readonly Color _whiten92 = Color.FromArgb(92, Color.White);
        private static readonly Color _whiten80 = Color.FromArgb(80, Color.White);
        private static readonly Color _whiten64 = Color.FromArgb(64, Color.White);
        private static readonly Color _whiten60 = Color.FromArgb(60, Color.White);
        private static readonly Color _whiten50 = Color.FromArgb(50, Color.White);
        private static readonly Color _whiten45 = Color.FromArgb(45, Color.White);
        private static readonly Color _whiten32 = Color.FromArgb(32, Color.White);
        private static readonly Color _whiten30 = Color.FromArgb(30, Color.White);
        private static readonly Color _whiten10 = Color.FromArgb(10, Color.White);
        private static readonly Color _whiten5 = Color.FromArgb(5, Color.White);
        private static readonly Color _242 = Color.FromArgb(242, 242, 242);
        private static readonly Color _218 = Color.FromArgb(218, 218, 218);
        private static readonly Color _190 = Color.FromArgb(190, 190, 190);
        
        // Blends
        private static readonly Blend _linear25Blend, _linear33Blend, _linear40Blend, _linear50Blend;
        private static readonly Blend _switch25Blend, _switch33Blend, _switch50Blend, _switch90Blend;
        private static readonly Blend _halfCutBlend, _quarterPhaseBlend, _oneNoteBlend, _linearShadowBlend;
        private static readonly Blend _rounding2Blend, _rounding3Blend, _rounding4Blend, _rounding5Blend;
        private static readonly Blend _ribbonInBlend, _ribbonOutBlend, _ribbonTopBlend, _ribbonGroupArea3;
        private static readonly Blend _ribbonTabSelected1Blend, _ribbonTabSelected2Blend;
        private static readonly Blend _ribbonGroup1Blend, _ribbonGroup2Blend, _ribbonGroup3Blend;
        private static readonly Blend _ribbonGroup4Blend, _ribbonGroup5Blend, _ribbonGroup6Blend;
        private static readonly Blend _ribbonGroup7Blend, _ribbonGroup8Blend, _ribbonGroup9Blend;
        private static readonly Blend _ribbonTabTopBlend, _ribbonAppButtonBlend;
        private static readonly Blend _dragRoundedInsideBlend;

        // Pens
        private static readonly Pen _paleShadowPen;
        private static readonly Pen _lightShadowPen;
        private static readonly Pen _mediumShadowPen;
        private static readonly Pen _medium2ShadowPen;
        private static readonly Pen _darkShadowPen;
        private static readonly Pen _light1Pen;
        private static readonly Pen _light2Pen;
        private static readonly Pen _whitenMediumPen;
        private static readonly Pen _buttonShadowPen;
        private static readonly Pen _compositionPen;

        // Brushes
        private static readonly SolidBrush _whitenLightBrush;
        private static readonly SolidBrush _whitenLightLBrush;
        private static readonly SolidBrush _compositionBrush;
        private static readonly SolidBrush _buttonBorder1Brush;
        private static readonly SolidBrush _buttonBorder2Brush;

        // Images
        private static readonly ImageList _gridSortOrder;
        private static readonly ImageList _gridRowIndicators;
        private static readonly ImageList _gridErrorIcon;
        #endregion

        #region Identity
        static RenderStandard()
        {
            _linear25Blend = new Blend();
            _linear25Blend.Factors = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            _linear25Blend.Positions = new float[] { 0.0f, 0.25f, 0.25f, 1.0f };

            _linear33Blend = new Blend();
            _linear33Blend.Factors = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            _linear33Blend.Positions = new float[] { 0.0f, 0.33f, 0.33f, 1.0f };

            _linear40Blend = new Blend();
            _linear40Blend.Factors = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            _linear40Blend.Positions = new float[] { 0.0f, 0.40f, 0.40f, 1.0f };

            _linear50Blend = new Blend();
            _linear50Blend.Factors = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            _linear50Blend.Positions = new float[] { 0.0f, 0.50f, 0.50f, 1.0f };

            _linearShadowBlend = new Blend();
            _linearShadowBlend.Factors = new float[] { 0.0f, 1.0f, 1.0f };
            _linearShadowBlend.Positions = new float[] { 0.0f, 0.30f, 1.0f };

            _switch25Blend = new Blend();
            _switch25Blend.Factors = new float[] { 0.0f, 0.0f, 1.0f, 1.0f };
            _switch25Blend.Positions = new float[] { 0.0f, 0.25f, 0.25f, 1.0f };

            _switch33Blend = new Blend();
            _switch33Blend.Factors = new float[] { 0.0f, 0.0f, 1.0f, 1.0f };
            _switch33Blend.Positions = new float[] { 0.0f, 0.33f, 0.33f, 1.0f };

            _switch50Blend = new Blend();
            _switch50Blend.Factors = new float[] { 0.0f, 0.0f, 1.0f, 1.0f };
            _switch50Blend.Positions = new float[] { 0.0f, 0.5f, 0.5f, 1.0f };

            _switch90Blend = new Blend();
            _switch90Blend.Factors = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            _switch90Blend.Positions = new float[] { 0.0f, 0.90f, 0.90f, 1.0f };

            _halfCutBlend = new Blend();
            _halfCutBlend.Factors = new float[] { 0.0f, 0.50f, 1.0f, 0.05f };
            _halfCutBlend.Positions = new float[] { 0.0f, 0.45f, 0.45f, 1.0f };

            _quarterPhaseBlend = new Blend();
            _quarterPhaseBlend.Factors = new float[] { 0.0f, 0.0f, 0.25f, 0.70f, 1.0f, 1.0f };
            _quarterPhaseBlend.Positions = new float[] { 0.0f, 0.10f, 0.20f, 0.30f, 0.5f, 1.0f };

            _oneNoteBlend = new Blend();
            _oneNoteBlend.Factors = new float[] { 0.15f, 0.75f, 1.0f, 1.0f };
            _oneNoteBlend.Positions = new float[] { 0.0f, 0.45f, 0.45f, 1.0f };

            _rounding2Blend = new Blend();
            _rounding2Blend.Factors = new float[] { 0.8f, 0.2f, 0f, 0.07f, 1.0f };
            _rounding2Blend.Positions = new float[] { 0.0f, 0.33f, 0.33f, 0.43f, 1.0f };

            _rounding3Blend = new Blend();
            _rounding3Blend.Factors = new float[] { 1.0f, 0.7f, 0.7f, 0f, 0.1f, 0.55f, 1.0f, 1.0f };
            _rounding3Blend.Positions = new float[] { 0.0f, 0.16f, 0.33f, 0.35f, 0.51f, 0.85f, 0.85f, 1.0f };

            _rounding4Blend = new Blend();
            _rounding4Blend.Factors = new float[] { 1.0f, 0.78f, 0.48f, 1.0f, 1.0f };
            _rounding4Blend.Positions = new float[] { 0.0f, 0.33f, 0.33f, 0.90f, 1.0f };

            _rounding5Blend = new Blend();
            _rounding5Blend.Factors = new float[] { 0.0f, 0.0f, 1.0f, 1.0f };
            _rounding5Blend.Positions = new float[] { 0.0f, 0.20f, 0.84f, 1.0f };

            _ribbonInBlend = new Blend();
            _ribbonInBlend.Factors = new float[] { 0.66f, 1.0f, 0.0f };
            _ribbonInBlend.Positions = new float[] { 0.0f, 0.50f, 1.0f };

            _ribbonOutBlend = new Blend();
            _ribbonOutBlend.Factors = new float[] { 0.2f, 1.0f, 0.0f };
            _ribbonOutBlend.Positions = new float[] { 0.0f, 0.50f, 1.0f };

            _ribbonTopBlend = new Blend();
            _ribbonTopBlend.Factors = new float[] { 0.0f, 1.0f, 1.0f, 0.0f };
            _ribbonTopBlend.Positions = new float[] { 0.0f, 0.2f, 0.8f, 1.0f };

            _ribbonGroup1Blend = new Blend();
            _ribbonGroup1Blend.Factors = new float[] { 0.0f, 0.0f, 0.6f, 1.0f };
            _ribbonGroup1Blend.Positions = new float[] { 0.0f, 0.18f, 0.75f, 1.0f };

            _ribbonGroup2Blend = new Blend();
            _ribbonGroup2Blend.Factors = new float[] { 0.0f, 0.5f, 1.0f, 1.0f };
            _ribbonGroup2Blend.Positions = new float[] { 0.0f, 0.18f, 0.2f, 1.0f };

            _ribbonGroup3Blend = new Blend();
            _ribbonGroup3Blend.Factors = new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f };
            _ribbonGroup3Blend.Positions = new float[] { 0.0f, 0.90f, 0.97f, 0.97f, 1.0f };

            _ribbonGroup4Blend = new Blend();
            _ribbonGroup4Blend.Factors = new float[] { 0.0f, 0.4f, 1.0f, 1.0f, };
            _ribbonGroup4Blend.Positions = new float[] { 0.0f, 0.045f, 0.33f, 1.0f };

            _ribbonGroup5Blend = new Blend();
            _ribbonGroup5Blend.Factors = new float[] { 0.0f, 0.0f, 1.0f };
            _ribbonGroup5Blend.Positions = new float[] { 0.0f, 0.5f, 1.0f };

            _ribbonGroup6Blend = new Blend();
            _ribbonGroup6Blend.Factors = new float[] { 0.0f, 0.0f, 1.0f };
            _ribbonGroup6Blend.Positions = new float[] { 0.0f, 0.4f, 1.0f };

            _ribbonGroup7Blend = new Blend();
            _ribbonGroup7Blend.Factors = new float[] { 0.0f, 1.0f, 1.0f, 0.0f };
            _ribbonGroup7Blend.Positions = new float[] { 0.0f, 0.15f, 0.85f, 1.0f };

            _ribbonGroup8Blend = new Blend();
            _ribbonGroup8Blend.Factors = new float[] { 0.0f, 0.0f, 1.0f};
            _ribbonGroup8Blend.Positions = new float[] { 0.0f, 0.85f, 1.0f };

            _ribbonGroup9Blend = new Blend();
            _ribbonGroup9Blend.Factors = new float[] { 0.0f, 0.5f, 0.75f, 0.9f, 1.0f };
            _ribbonGroup9Blend.Positions = new float[] { 0.0f, 0.25f, 0.50f, 0.75f, 1.0f };

            _ribbonGroupArea3 = new Blend();
            _ribbonGroupArea3.Factors = new float[] { 1.0f, 0.0f, 0.0f, 1.0f };
            _ribbonGroupArea3.Positions = new float[] { 0.0f, 0.1f, 0.85f, 1.0f };
            
            _ribbonTabSelected1Blend = new Blend();
            _ribbonTabSelected1Blend.Factors = new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 1.0f };
            _ribbonTabSelected1Blend.Positions = new float[] { 0.0f, 0.50f, 0.50f, 0.9f, 1.0f };

            _ribbonTabSelected2Blend = new Blend();
            _ribbonTabSelected2Blend.Factors = new float[] { 0.0f, 1.0f, 1.0f };
            _ribbonTabSelected2Blend.Positions = new float[] { 0.0f, 0.75f, 1.0f };

            _ribbonTabTopBlend = new Blend();
            _ribbonTabTopBlend.Factors = new float[] { 0.0f, 1.0f, 1.0f };
            _ribbonTabTopBlend.Positions = new float[] { 0.0f, 0.2f, 1.0f };

            _ribbonAppButtonBlend = new Blend();
            _ribbonAppButtonBlend.Factors = new float[] { 0.0f, 0.0f, 0.5f, 1.0f, 1.0f };
            _ribbonAppButtonBlend.Positions = new float[] { 0.0f, 0.1f, 0.5f, 0.5f, 1.0f };

            _dragRoundedInsideBlend = new Blend();
            _dragRoundedInsideBlend.Factors = new float[] { 0.05f, 0.2f, 0.5f, 1.0f };
            _dragRoundedInsideBlend.Positions = new float[] { 0.0f, 0.5f, 0.5f, 1.0f };

            _paleShadowPen = new Pen(Color.FromArgb(6, Color.Black));
            _lightShadowPen = new Pen(Color.FromArgb(8, Color.Black));
            _mediumShadowPen = new Pen(Color.FromArgb(10, Color.Black));
            _medium2ShadowPen = new Pen(Color.FromArgb(12, Color.Black));
            _darkShadowPen = new Pen(Color.FromArgb(18, Color.Black));
            _light1Pen = new Pen(Color.FromArgb(150, Color.White));
            _light2Pen = new Pen(Color.FromArgb(100, Color.White));
            _whitenMediumPen = new Pen(_whiten128);
            _buttonShadowPen = new Pen(Color.FromArgb(48, Color.Black));
            _compositionPen = new Pen(Color.FromArgb(96, Color.Black));

            _whitenLightBrush = new SolidBrush(_whiten30);
            _whitenLightLBrush = new SolidBrush(_whiten64);
            _compositionBrush = new SolidBrush(Color.FromArgb(32, Color.White));

            _buttonBorder1Brush = new SolidBrush(Color.FromArgb(20, 52, 59, 64));
            _buttonBorder2Brush = new SolidBrush(Color.FromArgb(70, 52, 59, 64));

            _gridSortOrder = new ImageList();
            _gridSortOrder.TransparentColor = Color.Magenta;
            _gridSortOrder.ImageSize = new Size(17, 11);
            _gridSortOrder.Images.AddStrip(Properties.Resources.GridSortOrder);

            _gridRowIndicators = new ImageList();
            _gridRowIndicators.TransparentColor = Color.Magenta;
            _gridRowIndicators.ImageSize = new Size(19, 13);
            _gridRowIndicators.Images.AddStrip(Properties.Resources.GridRowIndicators);

            _gridErrorIcon = new ImageList();
            _gridErrorIcon.TransparentColor = Color.Magenta;
            _gridErrorIcon.ImageSize = new Size(18, 17);
            _gridErrorIcon.Images.AddStrip(Properties.Resources.GridErrorIcon);
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
            KryptonStandardRenderer renderer = new KryptonStandardRenderer(colorPalette.ColorTable);

            // Seup the need to use rounded corners
            renderer.RoundedEdges = (colorPalette.ColorTable.UseRoundedEdges != InheritBool.False);

            return renderer;
        }
        #endregion

        #region RenderStandardBorder Overrides
        /// <summary>
        /// Gets the raw padding used per edge of the border.
        /// </summary>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <returns>Padding structure detailing all four edges.</returns>
        public override Padding GetBorderRawPadding(IPaletteBorder palette,
                                                    PaletteState state,
                                                    VisualOrientation orientation)
        {
            Debug.Assert(palette != null);

            // Validate parameter reference
            if (palette == null) throw new ArgumentNullException("palette");

            PaletteDrawBorders borders = palette.GetBorderDrawBorders(state);

            // If there is at least one border to be drawn
            if (CommonHelper.HasABorder(borders))
            {
                int borderWidth = palette.GetBorderWidth(state);

                switch (borders)
                {
                    case PaletteDrawBorders.Bottom:
                        return new Padding(0, 0, 0, borderWidth);
                    case PaletteDrawBorders.BottomLeft:
                        return new Padding(borderWidth, 0, 0, borderWidth);
                    case PaletteDrawBorders.BottomLeftRight:
                        return new Padding(borderWidth, 0, borderWidth, borderWidth);
                    case PaletteDrawBorders.BottomRight:
                        return new Padding(0, 0, borderWidth, borderWidth);
                    case PaletteDrawBorders.Left:
                        return new Padding(borderWidth, 0, 0, 0);
                    case PaletteDrawBorders.LeftRight:
                        return new Padding(borderWidth, 0, borderWidth, 0);
                    case PaletteDrawBorders.Top:
                        return new Padding(0, borderWidth, 0, 0);
                    case PaletteDrawBorders.Right:
                        return new Padding(0, 0, borderWidth, 0);
                    case PaletteDrawBorders.TopBottom:
                        return new Padding(0, borderWidth, 0, borderWidth);
                    case PaletteDrawBorders.TopBottomLeft:
                        return new Padding(borderWidth, borderWidth, 0, borderWidth);
                    case PaletteDrawBorders.TopBottomRight:
                        return new Padding(0, borderWidth, borderWidth, borderWidth);
                    case PaletteDrawBorders.TopLeft:
                        return new Padding(borderWidth, borderWidth, 0, 0);
                    case PaletteDrawBorders.TopLeftRight:
                        return new Padding(borderWidth, borderWidth, borderWidth, 0);
                    case PaletteDrawBorders.TopRight:
                        return new Padding(0, borderWidth, borderWidth, 0);
                    case PaletteDrawBorders.All:
                        return new Padding(borderWidth);
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        return Padding.Empty;
                }
            }
            else
                return Padding.Empty;
        }

        /// <summary>
		/// Gets the padding used to position display elements completely inside border drawing.
		/// </summary>
		/// <param name="palette">Palette used for drawing.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <returns>Padding structure detailing all four edges.</returns>
		public override Padding GetBorderDisplayPadding(IPaletteBorder palette,
                                                        PaletteState state,
                                                        VisualOrientation orientation)
		{
            Debug.Assert(palette != null);

            // Validate parameter reference
            if (palette == null) throw new ArgumentNullException("palette");

            PaletteDrawBorders borders = palette.GetBorderDrawBorders(state);

            // If there is at least one border to be drawn
            if (CommonHelper.HasABorder(borders))
            {
                int borderWidth = palette.GetBorderWidth(state);

                // Divide the rounding effect by PI to get the actual pixel distance needed 
                // for offseting. But add 2 so it starts indenting on a rounding of just 1.
                int roundPadding = Convert.ToInt16((palette.GetBorderRounding(state) + borderWidth + 2) / Math.PI);
                
                // If not involving rounding then padding for an edge is just the border width
                int squarePadding = borderWidth;

                // Borders thicker than 1 need extra offsetting, by half the extra width
                if (borderWidth > 1)
                {
                    int halfExtra = borderWidth / 2;
                    roundPadding += halfExtra;
                }

                // Enforce the width of the border as the minimum to ensure 
                // it still works as expected for small values of rounding
                if (roundPadding < borderWidth)
                    roundPadding = borderWidth;

                switch (borders)
                {
                    case PaletteDrawBorders.Bottom:
                        return new Padding(0, 0, 0, squarePadding);
                    case PaletteDrawBorders.BottomLeft:
                        return new Padding(roundPadding, 0, 0, roundPadding);
                    case PaletteDrawBorders.BottomLeftRight:
                        return new Padding(roundPadding, 0, roundPadding, roundPadding);
                    case PaletteDrawBorders.BottomRight:
                        return new Padding(0, 0, roundPadding, roundPadding);
                    case PaletteDrawBorders.Left:
                        return new Padding(squarePadding, 0, 0, 0);
                    case PaletteDrawBorders.LeftRight:
                        return new Padding(squarePadding, 0, squarePadding, 0);
                    case PaletteDrawBorders.Top:
                        return new Padding(0, squarePadding, 0, 0);
                    case PaletteDrawBorders.Right:
                        return new Padding(0, 0, squarePadding, 0);
                    case PaletteDrawBorders.TopBottom:
                        return new Padding(0, squarePadding, 0, squarePadding);
                    case PaletteDrawBorders.TopBottomLeft:
                        return new Padding(roundPadding, roundPadding, 0, roundPadding);
                    case PaletteDrawBorders.TopBottomRight:
                        return new Padding(0, roundPadding, roundPadding, roundPadding);
                    case PaletteDrawBorders.TopLeft:
                        return new Padding(roundPadding, roundPadding, 0, 0);
                    case PaletteDrawBorders.TopLeftRight:
                        return new Padding(roundPadding, roundPadding, roundPadding, 0);
                    case PaletteDrawBorders.TopRight:
                        return new Padding(0, roundPadding, roundPadding, 0);
                    case PaletteDrawBorders.All:
                        return new Padding(roundPadding);
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        return Padding.Empty;
                }
            }
            else
                return Padding.Empty;
		}

        /// <summary>
        /// Generate a graphics path that is the outside edge of the border.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <returns>GraphicsPath instance.</returns>
        public override GraphicsPath GetOutsideBorderPath(RenderContext context,
                                                          Rectangle rect,
                                                          IPaletteBorder palette,
                                                          VisualOrientation orientation,
                                                          PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

            // Use helper to create a border path on the outside
            return CreateBorderBackPath(true, false, rect,
                                        CommonHelper.OrientateDrawBorders(palette.GetBorderDrawBorders(state), orientation),
                                        palette.GetBorderWidth(state),
                                        palette.GetBorderRounding(state),
                                        (palette.GetBorderGraphicsHint(state) == PaletteGraphicsHint.AntiAlias),
                                        0);
        }

		/// <summary>
        /// Generate a graphics path that is in the middle of the border.
        /// </summary>
		/// <param name="context">Rendering context.</param>
		/// <param name="rect">Target rectangle.</param>
		/// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
		/// <returns>GraphicsPath instance.</returns>
		public override GraphicsPath GetBorderPath(RenderContext context,
												   Rectangle rect,
												   IPaletteBorder palette,
                                                   VisualOrientation orientation,
                                                   PaletteState state)
		{
			Debug.Assert(context != null);
			Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);
            
            // Use helper to create a border path in middle of the pen
			return CreateBorderBackPath(false,
                                        true,
                                        rect,
                                        CommonHelper.OrientateDrawBorders(palette.GetBorderDrawBorders(state), orientation),
									    palette.GetBorderWidth(state),
									    palette.GetBorderRounding(state),
									    (palette.GetBorderGraphicsHint(state) == PaletteGraphicsHint.AntiAlias),
                                        0);
		}

        /// <summary>
        /// Generate a graphics path that encloses the border and is used when rendering a background to ensure the background does not draw over the border area.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <returns>GraphicsPath instance.</returns>
        public override GraphicsPath GetBackPath(RenderContext context,
                                                 Rectangle rect,
                                                 IPaletteBorder palette,
                                                 VisualOrientation orientation,
                                                 PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

            // Use helper to create a border path in middle of the pen
            return CreateBorderBackPath(false,
                                        true,
                                        rect,
                                        CommonHelper.OrientateDrawBorders(palette.GetBorderDrawBorders(state), orientation),
                                        palette.GetBorderWidth(state),
                                        palette.GetBorderRounding(state),
                                        (palette.GetBorderGraphicsHint(state) == PaletteGraphicsHint.AntiAlias),
                                        0);
        }
        
        /// <summary>
		/// Draw border on the inside edge of the specified rectangle.
		/// </summary>
		/// <param name="context">Rendering context.</param>
		/// <param name="rect">Target rectangle.</param>
		/// <param name="palette">Palette used for drawing.</param>
		/// <param name="orientation">Visual orientation of the border.</param>
		/// <param name="state">State associated with rendering.</param>
		public override void DrawBorder(RenderContext context,
									    Rectangle rect,
									    IPaletteBorder palette,
										VisualOrientation orientation,
										PaletteState state)
		{
            Debug.Assert(context != null);
			Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

            PaletteDrawBorders borders = palette.GetBorderDrawBorders(state);

            // Is there anything to actually draw?
            if ((rect.Width > 0) && (rect.Height > 0) && CommonHelper.HasABorder(borders))
			{
				// Only use anti aliasing if the border is rounded
				SmoothingMode smoothMode = (palette.GetBorderRounding(state) > 0 ? SmoothingMode.AntiAlias : SmoothingMode.Default);

				// We want to draw using anti aliasing for a nice smooth effect
				using (GraphicsHint hint = new GraphicsHint(context.Graphics, palette.GetBorderGraphicsHint(state)))
				{
					// Cache commonly used values
					int borderWidth = palette.GetBorderWidth(state);

                    // Get the orientation correct borders value
                    borders = CommonHelper.OrientateDrawBorders(borders, orientation);

					// Is there any border to actually draw?
					if (borderWidth > 0)
					{
                        using (Clipping clip = new Clipping(context.Graphics, rect))
                        {
                            // We always create the first border path variant
                            GraphicsPath borderPath0 = CreateBorderBackPath(true, true, rect, borders, borderWidth,
                                                                            palette.GetBorderRounding(state),
                                                                            (smoothMode == SmoothingMode.AntiAlias), 0);

                            GraphicsPath borderPath1 = null;

                            // We only need the second border path if the two borders used are opposite each other
                            if ((borders == PaletteDrawBorders.TopBottom) ||
                                (borders == PaletteDrawBorders.LeftRight))
                            {
                                borderPath1 = CreateBorderBackPath(true, true, rect, borders, borderWidth,
                                                                   palette.GetBorderRounding(state),
                                                                   (smoothMode == SmoothingMode.AntiAlias), 1);
                            }

                            // Get the rectangle to use when dealing with gradients
                            Rectangle gradientRect = context.GetAlignedRectangle(palette.GetBorderColorAlign(state), rect);

                            // Use standard helper routine to create appropriate color brush
                            PaletteColorStyle colorStyle = palette.GetBorderColorStyle(state);
                            using (Pen borderPen = new Pen(CreateColorBrush(gradientRect,
                                                                            palette.GetBorderColor1(state),
                                                                            palette.GetBorderColor2(state),
                                                                            colorStyle,
                                                                            palette.GetBorderColorAngle(state),
                                                                            orientation), borderWidth))
                            {
                                if (colorStyle == PaletteColorStyle.Dashed)
                                    borderPen.DashPattern = new float[] { 2, 2 };

                                context.Graphics.DrawPath(borderPen, borderPath0);

                                // Optionally also draw the second path
                                if (borderPath1 != null)
                                    context.Graphics.DrawPath(borderPen, borderPath1);
                            }

                            Image borderImage = palette.GetBorderImage(state);
                            PaletteImageStyle borderImageStyle = palette.GetBorderImageStyle(state);

                            // Do we need to draw the image?
                            if (ShouldDrawImage(borderImage))
                            {
                                // Get the rectangle to use when dealing with gradients
                                Rectangle imageRect = context.GetAlignedRectangle(palette.GetBorderImageAlign(state), rect);

                                // Use standard helper routine to create appropriate image brush
                                using (Pen borderPen = new Pen(CreateImageBrush(imageRect, borderImage, borderImageStyle), borderWidth))
                                {
                                    context.Graphics.DrawPath(borderPen, borderPath0);

                                    // Optionally also draw the second path
                                    if (borderPath1 != null)
                                        context.Graphics.DrawPath(borderPen, borderPath1);
                                }
                            }

                            // Remember to dispose of resources
                            borderPath0.Dispose();

                            if (borderPath1 != null)
                                borderPath1.Dispose();
                        }
                    }
				}
			}
		}
		#endregion

		#region RenderStandardBack Overrides
		/// <summary>
		/// Draw background to fill the specified path.
		/// </summary>
		/// <param name="context">Rendering context.</param>
		/// <param name="rect">Target rectangle that encloses path.</param>
		/// <param name="path">Graphics path.</param>
		/// <param name="palette">Palette used for drawing.</param>
		/// <param name="orientation">Visual orientation of the border.</param>
		/// <param name="state">State associated with rendering.</param>
        /// <param name="memento">Cache used for drawing.</param>
		public override IDisposable DrawBack(RenderContext context,
									         Rectangle rect,
									         GraphicsPath path,
									         IPaletteBack palette,
									         VisualOrientation orientation,
									         PaletteState state,
                                             IDisposable memento)
		{
			Debug.Assert(context != null);
			Debug.Assert(path != null);
			Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

			// Is there anything to actually draw?
			if ((rect.Width > 0) && (rect.Height > 0))
			{
				// We want to draw using anti aliasing for a nice smooth effect
				using (GraphicsHint smooth = new GraphicsHint(context.Graphics, palette.GetBackGraphicsHint(state)))
				{
					// Cache commonly used values
					Image backImage = palette.GetBackImage(state);
                    PaletteImageStyle backImageStyle = palette.GetBackImageStyle(state);
                    PaletteColorStyle backColorStyle = palette.GetBackColorStyle(state);
                    Color backColor1 = palette.GetBackColor1(state);
                    Color backColor2 = palette.GetBackColor2(state);
                    float backColorAngle = palette.GetBackColorAngle(state);

                    // Get the rectangle to use when dealing with gradients
                    Rectangle gradientRect = context.GetAlignedRectangle(palette.GetBackColorAlign(state), rect);

                    switch (backColorStyle)
                    {
                        case PaletteColorStyle.GlassSimpleFull:
                            memento = RenderGlassHelpers.DrawBackGlassSimpleFull(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassNormalFull:
                            memento = RenderGlassHelpers.DrawBackGlassNormalFull(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassTrackingFull:
                            memento = RenderGlassHelpers.DrawBackGlassTrackingFull(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassPressedFull:
                            memento = RenderGlassHelpers.DrawBackGlassPressedFull(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassCheckedFull:
                            memento = RenderGlassHelpers.DrawBackGlassCheckedFull(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassCheckedTrackingFull:
                            memento = RenderGlassHelpers.DrawBackGlassCheckedTrackingFull(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassNormalStump:
                            memento = RenderGlassHelpers.DrawBackGlassNormalStump(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassTrackingStump:
                            memento = RenderGlassHelpers.DrawBackGlassTrackingStump(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassPressedStump:
                            memento = RenderGlassHelpers.DrawBackGlassPressedStump(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassCheckedStump:
                            memento = RenderGlassHelpers.DrawBackGlassCheckedStump(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassCheckedTrackingStump:
                            memento = RenderGlassHelpers.DrawBackGlassCheckedTrackingStump(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassThreeEdge:
                            memento = RenderGlassHelpers.DrawBackGlassThreeEdge(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassNormalSimple:
                            memento = RenderGlassHelpers.DrawBackGlassNormalSimple(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassTrackingSimple:
                            memento = RenderGlassHelpers.DrawBackGlassTrackingSimple(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassPressedSimple:
                            memento = RenderGlassHelpers.DrawBackGlassPressedSimple(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassCheckedSimple:
                            memento = RenderGlassHelpers.DrawBackGlassCheckedSimple(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassCheckedTrackingSimple:
                            memento = RenderGlassHelpers.DrawBackGlassCheckedTrackingSimple(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassCenter:
                            memento = RenderGlassHelpers.DrawBackGlassCenter(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassBottom:
                            memento = RenderGlassHelpers.DrawBackGlassBottom(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.GlassFade:
                            memento = RenderGlassHelpers.DrawBackGlassFade(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.ExpertTracking:
                            memento = RenderExpertHelpers.DrawBackExpertTracking(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.ExpertPressed:
                            memento = RenderExpertHelpers.DrawBackExpertPressed(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.ExpertChecked:
                            memento = RenderExpertHelpers.DrawBackExpertChecked(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.ExpertCheckedTracking:
                            memento = RenderExpertHelpers.DrawBackExpertCheckedTracking(context, rect, backColor1, backColor2, orientation, path, memento);
                            break;
                        case PaletteColorStyle.ExpertSquareHighlight:
                            memento = RenderExpertHelpers.DrawBackExpertSquareHighlight(context, rect, backColor1, backColor2, orientation, path, memento, false);
                            break;
                        case PaletteColorStyle.ExpertSquareHighlight2:
                            memento = RenderExpertHelpers.DrawBackExpertSquareHighlight(context, rect, backColor1, backColor2, orientation, path, memento, true);
                            break;
                        case PaletteColorStyle.SolidInside:
                            DrawBackSolidInside(context, gradientRect, backColor1, backColor2, path);
                            break;
                        case PaletteColorStyle.SolidLeftLine:
                        case PaletteColorStyle.SolidRightLine:
                        case PaletteColorStyle.SolidTopLine:
                        case PaletteColorStyle.SolidBottomLine:
                        case PaletteColorStyle.SolidAllLine:
                            DrawBackSolidLine(context, rect, backColor1, backColor2, backColorStyle, path);
                            break;
                        case PaletteColorStyle.OneNote:
                            DrawBackOneNote(context, gradientRect, backColor1, backColor2, 
                                            backColorStyle, backColorAngle, orientation, path);
                            break;
                        case PaletteColorStyle.RoundedTopLeftWhite:
                            DrawBackRoundedTopLeftWhite(context, rect, gradientRect, backColor1, backColor2,
                                                        backColorStyle, backColorAngle, orientation, path);
                            break;
                        case PaletteColorStyle.RoundedTopLight:
                            DrawBackRoundedTopLight(context, rect, gradientRect, backColor1, backColor2,
                                                    backColorStyle, backColorAngle, orientation, path);
                            break;
                        case PaletteColorStyle.Rounding4:
                            DrawBackRounded4(context, rect, gradientRect, backColor1, backColor2,
                                             backColorStyle, backColorAngle, orientation, path);
                            break;
                        case PaletteColorStyle.Rounding5:
                            DrawBackRounding5(context, rect, gradientRect, backColor1, backColor2, 
                                                  backColorStyle, backColorAngle, orientation, path);
                            break;
                        case PaletteColorStyle.LinearShadow:
                            DrawBackLinearShadow(context, rect, gradientRect, backColor1, backColor2,
                                                 backColorStyle, backColorAngle, orientation, path);
                            break;
                        default:
                            // Use standard helper routine to create appropriate color brush
                            using (Brush backBrush = CreateColorBrush(gradientRect, backColor1, backColor2,
                                                                      backColorStyle, backColorAngle, orientation))
                            {
                                context.Graphics.FillPath(backBrush, path);
                            }
                            break;
                    }

                    // Do we need to draw the image?
                    if (ShouldDrawImage(backImage))
                    {
                        // Get the rectangle to use when dealing with gradients
                        Rectangle imageRect = context.GetAlignedRectangle(palette.GetBackImageAlign(state), rect);

                        // Use standard helper routine to create appropriate image brush
                        using (Brush backBrush = CreateImageBrush(imageRect, backImage, backImageStyle))
                            context.Graphics.FillPath(backBrush, path);
                    }
				}
			}

            return memento;
		}

		#endregion

		#region RenderStandardContent Overrides
		/// <summary>
		/// Get the preferred size for drawing the content.
		/// </summary>
		/// <param name="context">Layout context.</param>
		/// <param name="palette">Content palette details.</param>
		/// <param name="values">Content values.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
		/// <param name="state">State associated with rendering.</param>
        /// <param name="composition">Should draw on a composition element.</param>
        /// <returns>Preferred size.</returns>
		public override Size GetContentPreferredSize(ViewLayoutContext context,
													 IPaletteContent palette,
													 IContentValues values,
													 VisualOrientation orientation,
                                                     PaletteState state,
                                                     bool composition)
		{
            Debug.Assert(context != null);
            Debug.Assert(palette != null);
            Debug.Assert(values != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);
            
            // Provide a maximum sized rectangle for placing content into, in 
			// order to work out how much of the space is actually allocated
			Rectangle displayRect = new Rectangle(Point.Empty, new Size(int.MaxValue, int.MaxValue));

			// Track the allocated space in each grid position
			Size[,] allocation = new Size[3, 3] { { Size.Empty, Size.Empty, Size.Empty }, 
												  { Size.Empty, Size.Empty, Size.Empty }, 
												  { Size.Empty, Size.Empty, Size.Empty } };

			// Create a memento for storing calculations
            using (StandardContentMemento memento = new StandardContentMemento())
            {
                // Cache the size of a spacing gap
                int spacingGap = palette.GetContentAdjacentGap(state);

                // Is the content intended for a vertical drawing orientation?
                bool vertical = (orientation == VisualOrientation.Left) ||
                                (orientation == VisualOrientation.Right);

                // Drawing vertical means we can ignore right to left, otherwise get value from control
                RightToLeft rtl = (vertical ? RightToLeft.No : context.Control.RightToLeft);

                // Allocate space for each required content in turn
                AllocateImageSpace(memento, palette, values, state, displayRect, rtl, ref allocation);
                AllocateShortTextSpace(context, context.Graphics, memento, palette, values, state, displayRect, rtl, spacingGap, ref allocation, composition);
                AllocateLongTextSpace(context, context.Graphics, memento, palette, values, state, displayRect, rtl, spacingGap, ref allocation, composition);

                // Add up total allocated for rows and columns
                int allocatedWidth = AllocatedTotalWidth(allocation, -1, -1, spacingGap);
                int allocatedHeight = AllocatedTotalHeight(allocation);

                // Grab the padding for the content
                Padding borderPadding = palette.GetContentPadding(state);

                // For the form level buttons we have to calculate the correct padding based on caption area
                PaletteContentStyle contentStyle = palette.GetContentStyle();
                if ((contentStyle == PaletteContentStyle.ButtonForm) || 
                    (contentStyle == PaletteContentStyle.ButtonFormClose))
                    borderPadding = ContentPaddingForButtonForm(borderPadding, context, allocatedHeight);

                // The preferred size needed depends on the orientation.
                switch (orientation)
                {
                    case VisualOrientation.Top:
                    case VisualOrientation.Bottom:
                        // Preferred size is the allocated space for the content plus the border padding
                        return new Size(allocatedWidth + borderPadding.Horizontal,
                                        allocatedHeight + borderPadding.Vertical);
                    case VisualOrientation.Left:
                    case VisualOrientation.Right:
                        // Preferred size is the allocated space for the content plus the border padding
                        return new Size(allocatedHeight + borderPadding.Vertical,
                                        allocatedWidth + borderPadding.Horizontal);
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        return Size.Empty;
                }
            }
		}

        /// <summary>
		/// Perform layout calculations on the provided content.
		/// </summary>
		/// <param name="context">Layout context.</param>
        /// <param name="availableRect">Display area available for laying out.</param>
		/// <param name="palette">Content palette details.</param>
		/// <param name="values">Content values.</param>
		/// <param name="orientation">Visual orientation of the content.</param>
		/// <param name="state">State associated with rendering.</param>
        /// <param name="composition">Should draw on a composition element.</param>
        /// <returns>Memento with cached information.</returns>
		public override IDisposable LayoutContent(ViewLayoutContext context,
											      Rectangle availableRect,
											      IPaletteContent palette,
											      IContentValues values,
											      VisualOrientation orientation,
											      PaletteState state,
                                                  bool composition)
		{
            Debug.Assert(context != null);
            Debug.Assert(palette != null);
            Debug.Assert(values != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

			// Remember the original value, for use later
            Rectangle cacheDisplayRect = availableRect;

			// Grab the padding for the content
			Padding borderPadding = palette.GetContentPadding(state);

            // Is the content intended for a vertical drawing orientation?
            bool vertical = (orientation == VisualOrientation.Left) ||
				            (orientation == VisualOrientation.Right);

			// If we need to apply in a vertical orientation
			if (vertical)
			{
				// Our algorithm only works by assuming a left to right horizontal
				// orientation, so we adjust the display rect to that orientation
				// and then at the end adjust the memento produced back to the
				// required orientation again. 'AdjustForOrientation'
                int temp = availableRect.Width;
                availableRect.Width = availableRect.Height;
                availableRect.Height = temp;
			}

			// Apply padding to the rectangle
            availableRect.X += borderPadding.Left;
            availableRect.Y += borderPadding.Top;
            availableRect.Width -= borderPadding.Horizontal;
            availableRect.Height -= borderPadding.Vertical;

			// If we need to apply in a vertical orientation
            if (vertical)
			{
				// This is the display rect we need to use in 'AdjustForOrientation'
				// and cache it for later. The displayRect itself is modified during
				// the below process and so cannot be used directly.
				int temp = cacheDisplayRect.Width;
				cacheDisplayRect.Width = cacheDisplayRect.Height;
				cacheDisplayRect.Height = temp;
			}

			// Track the allocated space in each grid position
			Size[,] allocation = new Size[3, 3] { { Size.Empty, Size.Empty, Size.Empty }, 
												  { Size.Empty, Size.Empty, Size.Empty }, 
												  { Size.Empty, Size.Empty, Size.Empty } };

			// Create a memento to return to caller
			StandardContentMemento memento = new StandardContentMemento();

			// Cache the size of a spacing gap
			int spacingGap = palette.GetContentAdjacentGap(state);

            // Drawing vertical means we can ignore right to left, otherwise get value from control
            RightToLeft rtl = (vertical ? RightToLeft.No : context.Control.RightToLeft);

			// Allocate space for each required content in turn
            AllocateImageSpace(memento, palette, values, state, availableRect, rtl, ref allocation);
            AllocateShortTextSpace(context, context.Graphics, memento, palette, values, state, availableRect, rtl, spacingGap, ref allocation, composition);
            AllocateLongTextSpace(context, context.Graphics, memento, palette, values, state, availableRect, rtl, spacingGap, ref allocation, composition);

			// Find the width of the columns and heights of the rows
			int[] colWidths = AllocatedColumnWidths(allocation, -1);
			int[] rowHeights = AllocatedRowHeights(allocation);

			// Add up total allocated for rows and columns
			int allocatedWidth = AllocatedTotalWidth(allocation, -1, -1, spacingGap);
			int allocatedHeight = AllocatedTotalHeight(allocation);

			// Excess width to allocate?
            if (allocatedWidth < availableRect.Width)
                ApplyExcessSpace(availableRect.Width - allocatedWidth, ref colWidths);
			
			// Excess height to allocate?
            if (allocatedHeight < availableRect.Height)
                rowHeights[1] += (availableRect.Height - allocatedHeight);

			// Find x positions and y positions
            int col0 = availableRect.Left;
			int col1 = col0 + colWidths[0];

			// Do we need to add a spacing gap after the first column?
			if (((colWidths[0] > 0) && (colWidths[1] > 0)) ||
				((colWidths[0] > 0) && (colWidths[1] == 0) && (colWidths[2] > 0)))
				col1 += spacingGap;

			int col2 = col1 + colWidths[1];

			// Do we need to add a spacing gap after the second column?
			if ((colWidths[1] > 0) && (colWidths[2] > 0))
				col2 += spacingGap;

            int row0 = availableRect.Top;
			int row1 = row0 + rowHeights[0];
			int row2 = row1 + rowHeights[1];

			// Decide on the ordering of the alignment to position
			PaletteRelativeAlign aAlign = (rtl == RightToLeft.Yes ? PaletteRelativeAlign.Far : PaletteRelativeAlign.Near);
			PaletteRelativeAlign bAlign = PaletteRelativeAlign.Center;
			PaletteRelativeAlign cAlign = (rtl == RightToLeft.Yes ? PaletteRelativeAlign.Near : PaletteRelativeAlign.Far);

			// Size and position the contents of each aligned cell
            PositionAlignContent(memento, palette, state, rtl, aAlign, PaletteRelativeAlign.Near, col0, row0, colWidths[0], rowHeights[0], spacingGap);
            PositionAlignContent(memento, palette, state, rtl, aAlign, PaletteRelativeAlign.Center, col0, row1, colWidths[0], rowHeights[1], spacingGap);
            PositionAlignContent(memento, palette, state, rtl, aAlign, PaletteRelativeAlign.Far, col0, row2, colWidths[0], rowHeights[2], spacingGap);
            PositionAlignContent(memento, palette, state, rtl, bAlign, PaletteRelativeAlign.Near, col1, row0, colWidths[1], rowHeights[0], spacingGap);
            PositionAlignContent(memento, palette, state, rtl, bAlign, PaletteRelativeAlign.Center, col1, row1, colWidths[1], rowHeights[1], spacingGap);
            PositionAlignContent(memento, palette, state, rtl, bAlign, PaletteRelativeAlign.Far, col1, row2, colWidths[1], rowHeights[2], spacingGap);
            PositionAlignContent(memento, palette, state, rtl, cAlign, PaletteRelativeAlign.Near, col2, row0, colWidths[2], rowHeights[0], spacingGap);
            PositionAlignContent(memento, palette, state, rtl, cAlign, PaletteRelativeAlign.Center, col2, row1, colWidths[2], rowHeights[1], spacingGap);
            PositionAlignContent(memento, palette, state, rtl, cAlign, PaletteRelativeAlign.Far, col2, row2, colWidths[2], rowHeights[2], spacingGap);

			// Ask the memento to adjust itself for the required orientation
			memento.AdjustForOrientation(orientation, cacheDisplayRect);

            return memento;
		}

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
        public override void DrawContent(RenderContext context, 
										 Rectangle displayRect,
										 IPaletteContent palette,
										 IDisposable memento,
										 VisualOrientation orientation,
										 PaletteState state,
                                         bool composition,
                                         bool allowFocusRect)
		{
			Debug.Assert(context != null);
			Debug.Assert(memento != null);
			Debug.Assert(memento is StandardContentMemento);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

            // Cast the incoming memento to the correct type
			StandardContentMemento standard = (StandardContentMemento)memento;

			if (standard.DrawImage)
			{
                DrawImageHelper(context,
                                standard.Image,
                                standard.ImageTransparentColor,
                                standard.ImageRect,
                                orientation,
                                palette.GetContentImageEffect(state),
                                palette.GetContentImageColorMap(state),
                                palette.GetContentImageColorTo(state));
			}

			if (standard.DrawShortText)
			{
				using (GraphicsTextHint hint = new GraphicsTextHint(context.Graphics, standard.ShortTextHint))
				{
					// Get the rectangle to use when dealing with gradients
					Rectangle gradientRect = context.GetAlignedRectangle(palette.GetContentShortTextColorAlign(state), standard.ShortTextRect);

					// Use standard helper routine to create appropriate color brush
                    Color color1 = palette.GetContentShortTextColor1(state);
                    PaletteColorStyle colorStyle = palette.GetContentShortTextColorStyle(state);
					using (Brush colorBrush = CreateColorBrush(gradientRect,
															   color1,
															   palette.GetContentShortTextColor2(state),
                                                               colorStyle,
															   palette.GetContentShortTextColorAngle(state),
															   orientation))
					{
                        if (!AccurateText.DrawString(context.Graphics,
                                                     colorBrush,
                                                     standard.ShortTextRect,
                                                     context.Control.RightToLeft,
                                                     standard.Orientation,
                                                     composition,
                                                     state,
                                                     standard.ShortTextMemento))
                        {
                            // Failed to draw means the font is likely to be invalid, get a fresh font
                            standard.ShortTextMemento.Font = palette.GetContentShortTextNewFont(state);

                            // Try again using the new font
                            AccurateText.DrawString(context.Graphics,
                                                    colorBrush,
                                                    standard.ShortTextRect,
                                                    context.Control.RightToLeft,
                                                    standard.Orientation,
                                                    composition,
                                                    state,
                                                    standard.ShortTextMemento);
                        }
					}

					Image shortImage = palette.GetContentShortTextImage(state);
					PaletteImageStyle shortImageStyle = palette.GetContentShortTextImageStyle(state);

					// Do we need to draw the image?
					if (ShouldDrawImage(shortImage))
					{
						// Get the rectangle to use when dealing with gradients
						Rectangle imageRect = context.GetAlignedRectangle(palette.GetContentShortTextImageAlign(state), standard.ShortTextRect);

						// Use standard helper routine to create appropriate image brush
						using (Brush imageBrush = CreateImageBrush(imageRect, shortImage, shortImageStyle))
						{
                            if (!AccurateText.DrawString(context.Graphics,
                                                         imageBrush,
                                                         standard.ShortTextRect,
                                                         context.Control.RightToLeft,
                                                         standard.Orientation,
                                                         composition,
                                                         state,
                                                         standard.ShortTextMemento))
                            {
                                // Failed to draw means the font is likely to be invalid, get a fresh font
                                standard.ShortTextMemento.Font = palette.GetContentShortTextNewFont(state);

                                AccurateText.DrawString(context.Graphics,
                                                        imageBrush,
                                                        standard.ShortTextRect,
                                                        context.Control.RightToLeft,
                                                        standard.Orientation,
                                                        composition,
                                                        state,
                                                        standard.ShortTextMemento);
                            }
						}
					}
				}
			}

			if (standard.DrawLongText)
			{
				using (GraphicsTextHint hint = new GraphicsTextHint(context.Graphics, standard.LongTextHint))
				{
					// Get the rectangle to use when dealing with gradients
					Rectangle gradientRect = context.GetAlignedRectangle(palette.GetContentLongTextColorAlign(state), standard.LongTextRect);

					// Use standard helper routine to create appropriate color brush
                    Color color1 = palette.GetContentLongTextColor1(state);
                    PaletteColorStyle colorStyle = palette.GetContentLongTextColorStyle(state);
                    using (Brush colorBrush = CreateColorBrush(gradientRect,
															   color1,
															   palette.GetContentLongTextColor2(state),
															   colorStyle,
															   palette.GetContentLongTextColorAngle(state),
															   orientation))
					{
                        if (!AccurateText.DrawString(context.Graphics,
                                                     colorBrush,
                                                     standard.LongTextRect,
                                                     context.Control.RightToLeft,
                                                     standard.Orientation,
                                                     composition,
                                                     state,
                                                     standard.LongTextMemento))
                        {
                            // Failed to draw means the font is likely to be invalid, get a fresh font
                            standard.LongTextMemento.Font = palette.GetContentLongTextNewFont(state);

                            AccurateText.DrawString(context.Graphics,
                                                    colorBrush,
                                                    standard.LongTextRect,
                                                    context.Control.RightToLeft,
                                                    standard.Orientation,
                                                    composition,
                                                    state,
                                                    standard.LongTextMemento);
                        }
					}

					Image longImage = palette.GetContentLongTextImage(state);
					PaletteImageStyle longImageStyle = palette.GetContentLongTextImageStyle(state);

					// Do we need to draw the image?
					if (ShouldDrawImage(longImage))
					{
						// Get the rectangle to use when dealing with gradients
						Rectangle imageRect = context.GetAlignedRectangle(palette.GetContentLongTextImageAlign(state), standard.LongTextRect);

						// Use standard helper routine to create appropriate image brush
						using (Brush imageBrush = CreateImageBrush(imageRect, longImage, longImageStyle))
						{
                            if (!AccurateText.DrawString(context.Graphics,
                                                         imageBrush,
                                                         standard.LongTextRect,
                                                         context.Control.RightToLeft,
                                                         standard.Orientation,
                                                         composition,
                                                         state,
                                                         standard.LongTextMemento))
                            {
                                // Failed to draw means the font is likely to be invalid, get a fresh font
                                standard.LongTextMemento.Font = palette.GetContentLongTextNewFont(state);

                                AccurateText.DrawString(context.Graphics,
                                                        imageBrush,
                                                        standard.LongTextRect,
                                                        context.Control.RightToLeft,
                                                        standard.Orientation,
                                                        composition,
                                                        state,
                                                        standard.LongTextMemento);
                            }
						}
					}
				}
			}

			// Do we need to show this content has the focus?
            if (allowFocusRect && (palette.GetContentDrawFocus(state) == InheritBool.True))
			{
				// Place the rectangle 1 pixel inside the content display area
				displayRect.Inflate(-1, -1);

				// Use window forms provided helper class for drawing
				ControlPaint.DrawFocusRectangle(context.Graphics, displayRect);
			}
		}

        /// <summary>
        /// Request the calculated display of the image.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>True if the image is being displayed; otherwise false.</returns>
        public override bool GetContentImageDisplayed(IDisposable memento)
        {
            if (memento != null)
            {
                StandardContentMemento standard = (StandardContentMemento)memento;
                return standard.DrawImage;
            }
            else
                return false;
        }

        /// <summary>
        /// Request the calculated position of the content image.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>Display rectangle for the image content.</returns>
        public override Rectangle GetContentImageRectangle(IDisposable memento)
        {
            if (memento != null)
            {
                StandardContentMemento standard = (StandardContentMemento)memento;
                return standard.ImageRect;
            }
            else
                return Rectangle.Empty;
        }

        /// <summary>
        /// Request the calculated display of the short text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>True if the short text is being displayed; otherwise false.</returns>
        public override bool GetContentShortTextDisplayed(IDisposable memento)
        {
            if (memento != null)
            {
                StandardContentMemento standard = (StandardContentMemento)memento;
                return standard.DrawShortText;
            }
            else
                return false;
        }

        /// <summary>
        /// Request the calculated position of the content short text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>Display rectangle for the image content.</returns>
        public override Rectangle GetContentShortTextRectangle(IDisposable memento)
        {
            if (memento != null)
            {
                StandardContentMemento standard = (StandardContentMemento)memento;
                return standard.ShortTextRect;
            }
            else
                return Rectangle.Empty;
        }

        /// <summary>
        /// Request the calculated display of the long text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>True if the long text is being displayed; otherwise false.</returns>
        public override bool GetContentLongTextDisplayed(IDisposable memento)
        {
            if (memento != null)
            {
                StandardContentMemento standard = (StandardContentMemento)memento;
                return standard.DrawLongText;
            }
            else
                return false;
        }

        /// <summary>
        /// Request the calculated position of the content long text.
        /// </summary>
        /// <param name="memento">Cached values from layout call.</param>
        /// <returns>Display rectangle for the image content.</returns>
        public override Rectangle GetContentLongTextRectangle(IDisposable memento)
        {
            if (memento != null)
            {
                StandardContentMemento standard = (StandardContentMemento)memento;
                return standard.LongTextRect;
            }
            else
                return Rectangle.Empty;
        }
        #endregion

        #region RenderTabBorder Overrides
        /// <summary>
        /// Gets if the tabs should be drawn from left to right for z-ordering.
        /// </summary>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>True for left to right, otherwise draw right to left.</returns>
        public override bool GetTabBorderLeftDrawing(TabBorderStyle tabBorderStyle)
        {
            switch (tabBorderStyle)
            {
                case TabBorderStyle.OneNote:
                    return false;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Gets the spacing used to separate each tab border instance.
        /// </summary>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>Number of pixels to space instances.</returns>
        public override int GetTabBorderSpacingGap(TabBorderStyle tabBorderStyle)
        {
            switch (tabBorderStyle)
            {
                case TabBorderStyle.DockEqual:
                case TabBorderStyle.SquareEqualSmall:
                    return _spacingTabSquareEqualSmall;
                case TabBorderStyle.SquareEqualMedium:
                    return _spacingTabSquareEqualMedium;
                case TabBorderStyle.SquareEqualLarge:
                    return _spacingTabSquareEqualLarge;
                case TabBorderStyle.DockOutsize:
                case TabBorderStyle.SquareOutsizeSmall:
                    return _spacingTabSquareOutsizeSmall;
                case TabBorderStyle.SquareOutsizeMedium:
                    return _spacingTabSquareOutsizeMedium;
                case TabBorderStyle.SquareOutsizeLarge:
                    return _spacingTabSquareOutsizeLarge;
                case TabBorderStyle.RoundedEqualSmall:
                    return _spacingTabRoundedEqualSmall;
                case TabBorderStyle.RoundedEqualMedium:
                    return _spacingTabRoundedEqualMedium;
                case TabBorderStyle.RoundedEqualLarge:
                    return _spacingTabRoundedEqualLarge;
                case TabBorderStyle.RoundedOutsizeSmall:
                    return _spacingTabRoundedOutsizeSmall;
                case TabBorderStyle.RoundedOutsizeMedium:
                    return _spacingTabRoundedOutsizeMedium;
                case TabBorderStyle.RoundedOutsizeLarge:
                    return _spacingTabRoundedOutsizeLarge;
                case TabBorderStyle.SlantEqualNear:
                case TabBorderStyle.SlantEqualFar:
                    return _spacingTabSlantEqual;
                case TabBorderStyle.SlantOutsizeNear:
                case TabBorderStyle.SlantOutsizeFar:
                    return _spacingTabSlantOutsize;
                case TabBorderStyle.SlantEqualBoth:
                    return _spacingTabSlantEqualBoth;
                case TabBorderStyle.SlantOutsizeBoth:
                    return _spacingTabSlantOutsize * 2;
                case TabBorderStyle.OneNote:
                    return _spacingTabOneNote;
                case TabBorderStyle.SmoothEqual:
                    return _spacingTabSmoothE;
                case TabBorderStyle.SmoothOutsize:
                    return _spacingTabSmoothO;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return 1;
            }
        }

        /// <summary>
        /// Gets the padding used to position display elements completely inside border drawing.
        /// </summary>
        /// <param name="context">View layout context.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        /// <returns>Padding structure detailing all four edges.</returns>
        public override Padding GetTabBorderDisplayPadding(ViewLayoutContext context,
                                                           IPaletteBorder palette,
                                                           PaletteState state,
                                                           VisualOrientation orientation,
                                                           TabBorderStyle tabBorderStyle)
        {
            Debug.Assert(palette != null);

            // Validate parameter reference
            if (palette == null) throw new ArgumentNullException("palette");

            // Get the width of the border
            int borderWidth = palette.GetBorderWidth(state);

            // Cache the right to left setting
            bool rtl = (context.Control.RightToLeft == RightToLeft.Yes);

            Padding ret = Padding.Empty;

            switch (tabBorderStyle)
            {
                case TabBorderStyle.DockEqual:
                case TabBorderStyle.SquareEqualMedium:
                case TabBorderStyle.SquareEqualSmall:
                case TabBorderStyle.SquareEqualLarge:
                case TabBorderStyle.RoundedEqualMedium:
                case TabBorderStyle.RoundedEqualSmall:
                case TabBorderStyle.RoundedEqualLarge:
                    ret = new Padding(borderWidth, borderWidth, borderWidth, 0);
                    break;
                case TabBorderStyle.DockOutsize:
                    ret = new Padding(borderWidth + _spacingTabDockOutsize, borderWidth + _spacingTabSquareOutsizeLarge, borderWidth + _spacingTabDockOutsize, 0);
                    break;
                case TabBorderStyle.SquareOutsizeMedium:
                case TabBorderStyle.SquareOutsizeSmall:
                case TabBorderStyle.SquareOutsizeLarge:
                case TabBorderStyle.RoundedOutsizeMedium:
                case TabBorderStyle.RoundedOutsizeSmall:
                case TabBorderStyle.RoundedOutsizeLarge:
                    ret = new Padding(borderWidth + _spacingTabOutsizePadding, borderWidth + _spacingTabOutsizePadding, borderWidth + _spacingTabOutsizePadding, 0);
                    break;
                case TabBorderStyle.SlantEqualNear:
                case TabBorderStyle.SlantOutsizeNear:
                    // Calculte the extra needed for the outsize variant
                    int x = (tabBorderStyle == TabBorderStyle.SlantOutsizeNear ? _spacingTabOutsizePadding : 0);

                    switch(orientation)
                    {
                        case VisualOrientation.Top:
                            if (rtl)
                                ret = new Padding(borderWidth + x, borderWidth + x, borderWidth + x + _spacingTabSlantPadding - 1, 0);
                            else
                                ret = new Padding(borderWidth + x + _spacingTabSlantPadding - 1, borderWidth + x, borderWidth + x, 0);
                            break;
                        case VisualOrientation.Left:
                            ret = new Padding(borderWidth + x + _spacingTabSlantPadding - 1, borderWidth + x, borderWidth + x, 0);
                            break;
                        case VisualOrientation.Right:
                            ret = new Padding(borderWidth + x, borderWidth + x, borderWidth + x + _spacingTabSlantPadding - 1, 0);
                            break;
                        case VisualOrientation.Bottom:
                            if (rtl)
                                ret = new Padding(borderWidth + x + _spacingTabSlantPadding - 1, borderWidth + x, borderWidth + x, 0);
                            else
                                ret = new Padding(borderWidth + x, borderWidth + x, borderWidth + x + _spacingTabSlantPadding - 1, 0);
                            break;
                    }
                    break;
                case TabBorderStyle.SlantEqualFar:
                case TabBorderStyle.SlantOutsizeFar:
                    // Calculte the extra needed for the outsize variant
                    int y = (tabBorderStyle == TabBorderStyle.SlantOutsizeFar ? _spacingTabOutsizePadding : 0);

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            if (rtl)
                                ret = new Padding(borderWidth + y + _spacingTabSlantPadding - 1, borderWidth + y, borderWidth + y, 0);
                            else
                                ret = new Padding(borderWidth + y, borderWidth + y, borderWidth + y + _spacingTabSlantPadding - 1, 0);
                            break;
                        case VisualOrientation.Left:
                            ret = new Padding(borderWidth + y, borderWidth + y, borderWidth + y + _spacingTabSlantPadding - 1, 0);
                            break;
                        case VisualOrientation.Right:
                            ret = new Padding(borderWidth + y + _spacingTabSlantPadding - 1, borderWidth + y, borderWidth + y, 0);
                            break;
                        case VisualOrientation.Bottom:
                            if (rtl)
                                ret = new Padding(borderWidth + y, borderWidth + y, borderWidth + y + _spacingTabSlantPadding - 1, 0);
                            else
                                ret = new Padding(borderWidth + y + _spacingTabSlantPadding - 1, borderWidth + y, borderWidth + y, 0);
                            break;
                    }
                    break;
                case TabBorderStyle.SlantEqualBoth:
                case TabBorderStyle.SlantOutsizeBoth:
                    // Calculte the extra needed for the outsize variant
                    int z = (tabBorderStyle == TabBorderStyle.SlantOutsizeBoth ? _spacingTabOutsizePadding : 0);

                    ret = new Padding(borderWidth + z + _spacingTabSlantPadding - 1, borderWidth + z,
                                      borderWidth + z + _spacingTabSlantPadding - 1, 0);
                    break;
                case TabBorderStyle.OneNote:
                    // Is the current tab selected?
                    bool selected = (state == PaletteState.CheckedNormal) ||
                                    (state == PaletteState.CheckedPressed) ||
                                    (state == PaletteState.CheckedTracking);

                    // Find the correct edge padding values to use
                    int lp = (selected ? _spacingTabOneNoteLPS : _spacingTabOneNoteLPI);
                    int tp = (selected ? _spacingTabOneNoteTPS : _spacingTabOneNoteTPI);
                    int bp = (selected ? _spacingTabOneNoteBPS : _spacingTabOneNoteBPI);
                    int rp = (selected ? _spacingTabOneNoteRPS : _spacingTabOneNoteRPI);

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            if (rtl)
                                ret = new Padding(borderWidth + rp, borderWidth + tp, borderWidth + lp, bp);
                            else
                                ret = new Padding(borderWidth + lp, borderWidth + tp, borderWidth + rp, bp);
                            break;
                        case VisualOrientation.Left:
                            ret = new Padding(borderWidth + rp, borderWidth + tp, borderWidth + lp, bp);
                            break;
                        case VisualOrientation.Right:
                            ret = new Padding(borderWidth + lp, borderWidth + tp, borderWidth + rp, bp);
                            break;
                        case VisualOrientation.Bottom:
                            if (rtl)
                                ret = new Padding(borderWidth + lp, borderWidth + tp, borderWidth + rp, bp);
                            else
                                ret = new Padding(borderWidth + rp, borderWidth + tp, borderWidth + lp, bp);
                            break;
                    }
                    break;
                case TabBorderStyle.SmoothEqual:
                    ret = new Padding(borderWidth + _spacingTabSmoothLRE, borderWidth + _spacingTabSmoothTE, borderWidth + _spacingTabSmoothLRE, 0);
                    break;
                case TabBorderStyle.SmoothOutsize:
                    ret = new Padding(borderWidth + _spacingTabSmoothLRO, borderWidth + _spacingTabSmoothTO, borderWidth + _spacingTabSmoothLRO, 0);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return ret;
        }

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
        public override GraphicsPath GetTabBorderPath(RenderContext context,
                                                      Rectangle rect,
                                                      IPaletteBorder palette,
                                                      VisualOrientation orientation,
                                                      PaletteState state,
                                                      TabBorderStyle tabBorderStyle)
        {
            Debug.Assert(context != null);
            Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

            // Use helper to create a border path in middle of the pen
            return CreateTabBorderBackPath(context.Control.RightToLeft, state, false, rect, 
                                           palette.GetBorderWidth(state), tabBorderStyle, orientation,
                                           (palette.GetBorderGraphicsHint(state) == PaletteGraphicsHint.AntiAlias));
        }

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
        public override GraphicsPath GetTabBackPath(RenderContext context,
                                                    Rectangle rect,
                                                    IPaletteBorder palette,
                                                    VisualOrientation orientation,
                                                    PaletteState state,
                                                    TabBorderStyle tabBorderStyle)
        {
            Debug.Assert(context != null);
            Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

            // Use helper to create a border path in middle of the pen
            return CreateTabBorderBackPath(context.Control.RightToLeft, state, false, rect, 
                                           palette.GetBorderWidth(state), tabBorderStyle, orientation,
                                           (palette.GetBorderGraphicsHint(state) == PaletteGraphicsHint.AntiAlias));
        }

        /// <summary>
        /// Draw border on the inside edge of the specified rectangle.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="palette">Palette used for drawing.</param>
        /// <param name="orientation">Visual orientation of the border.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="tabBorderStyle">Style of tab border.</param>
        public override void DrawTabBorder(RenderContext context,
                                           Rectangle rect,
                                           IPaletteBorder palette,
                                           VisualOrientation orientation,
                                           PaletteState state,
                                           TabBorderStyle tabBorderStyle)
        {
            Debug.Assert(context != null);
            Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

            // Is there anything to actually draw?
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                // Decide if we need to use anti aliasing for a smoother looking visual
                using (GraphicsHint hint = new GraphicsHint(context.Graphics, palette.GetBorderGraphicsHint(state)))
                {
                    // Cache commonly used values
                    int borderWidth = palette.GetBorderWidth(state);

                    // Is there any border to actually draw?
                    if (borderWidth > 0)
                    {
                        // Create the path that represents the entire tab border
                        using (GraphicsPath borderPath = CreateTabBorderBackPath(context.Control.RightToLeft, state, true, rect, 
                                                                                 borderWidth, tabBorderStyle, orientation,
                                                                                 (palette.GetBorderGraphicsHint(state) == PaletteGraphicsHint.AntiAlias)))
                        {
                            // Get the rectangle to use when dealing with gradients
                            Rectangle gradientRect = context.GetAlignedRectangle(palette.GetBorderColorAlign(state), rect);
                            
                            // Use standard helper routine to create appropriate color brush
                            using(Brush borderBrush = CreateColorBrush(gradientRect,
                                                                       palette.GetBorderColor1(state),
                                                                       palette.GetBorderColor2(state),
                                                                       palette.GetBorderColorStyle(state),
                                                                       palette.GetBorderColorAngle(state),
                                                                       orientation))
                            {
                                using (Pen borderPen = new Pen(borderBrush, borderWidth))
                                    context.Graphics.DrawPath(borderPen, borderPath);
                            }

                            Image borderImage = palette.GetBorderImage(state);

                            // Do we need to draw the image?
                            if (ShouldDrawImage(borderImage))
                            {
                                // Get the rectangle to use when dealing with gradients
                                Rectangle imageRect = context.GetAlignedRectangle(palette.GetBorderImageAlign(state), rect);

                                // Get the image style to use for the image brush
                                PaletteImageStyle borderImageStyle = palette.GetBorderImageStyle(state);

                                // Use standard helper routine to create appropriate image brush
                                using (Pen borderPen = new Pen(CreateImageBrush(imageRect, borderImage, borderImageStyle), borderWidth))
                                {
                                    context.Graphics.DrawPath(borderPen, borderPath);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region RenderRibbon Overrides
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
        public override IDisposable DrawRibbonBack(PaletteRibbonShape shape,
                                                   RenderContext context,
                                                   Rectangle rect,
                                                   PaletteState state,
                                                   IPaletteRibbonBack palette,
                                                   VisualOrientation orientation,
                                                   bool composition,
                                                   IDisposable memento)
        {
            // Note is the incoming state is detailed we are drawing inside a popip
            bool showingInPopup = ((state & PaletteState.FocusOverride) == PaletteState.FocusOverride);
            if (showingInPopup)
                state = state & ~PaletteState.FocusOverride;

            switch (palette.GetRibbonBackColorStyle(state))
            {
                case PaletteRibbonColorStyle.Empty:
                    // Do nothing
                    break;
                case PaletteRibbonColorStyle.Solid:
                    using (SolidBrush backBrush = new SolidBrush(palette.GetRibbonBackColor1(state)))
                        context.Graphics.FillRectangle(backBrush, rect);
                    break;
                case PaletteRibbonColorStyle.Linear:
                    return DrawRibbonLinear(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.LinearBorder:
                    return DrawRibbonLinearBorder(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonAppMenuInner:
                    return DrawRibbonAppMenuInner(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonAppMenuOuter:
                    return DrawRibbonAppMenuOuter(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonQATFullbarRound:
                    return DrawRibbonQATFullbarRound(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonQATFullbarSquare:
                    return DrawRibbonQATFullbarSquare(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonQATMinibarSingle:
                    return DrawRibbonQATMinibarSingle(context, rect, state, palette, composition, memento);
                case PaletteRibbonColorStyle.RibbonQATMinibarDouble:
                    return DrawRibbonQATMinibarDouble(context, rect, state, palette, composition, memento);
                case PaletteRibbonColorStyle.RibbonQATOverflow:
                    return DrawRibbonQATOverflow(shape, context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonGroupGradientOne:
                    return DrawRibbonGroupGradientOne(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonGroupGradientTwo:
                    return DrawRibbonGroupGradientTwo(context, rect, state, palette, _groupGradientTwo, memento);
                case PaletteRibbonColorStyle.RibbonGroupCollapsedBorder:
                    return DrawRibbonGroupCollapsedBorder(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonGroupCollapsedFrameBorder:
                    return DrawRibbonGroupCollapsedFrameBorder(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonGroupCollapsedFrameBack:
                    return DrawRibbonGroupGradientTwo(context, rect, state, palette, _groupGradientFrame, memento);
                case PaletteRibbonColorStyle.RibbonGroupNormalBorder:
                    return DrawRibbonGroupNormalBorder(context, rect, state, palette, false, false, memento);
                case PaletteRibbonColorStyle.RibbonGroupNormalBorderSep:
                    return DrawRibbonGroupNormalBorderSep(showingInPopup, context, rect, state, palette, memento, false, false, false);
                case PaletteRibbonColorStyle.RibbonGroupNormalBorderSepPressedLight:
                    return DrawRibbonGroupNormalBorderSep(showingInPopup, context, rect, state, palette, memento, true, false, false);
                case PaletteRibbonColorStyle.RibbonGroupNormalBorderSepPressedDark:
                    return DrawRibbonGroupNormalBorderSep(showingInPopup, context, rect, state, palette, memento, true, false, true);
                case PaletteRibbonColorStyle.RibbonGroupNormalBorderSepTrackingLight:
                    return DrawRibbonGroupNormalBorderSep(showingInPopup, context, rect, state, palette, memento, false, true, false);
                case PaletteRibbonColorStyle.RibbonGroupNormalBorderSepTrackingDark:
                    return DrawRibbonGroupNormalBorderSep(showingInPopup, context, rect, state, palette, memento, false, true, true);
                case PaletteRibbonColorStyle.RibbonGroupNormalBorderTracking:
                    return DrawRibbonGroupNormalBorder(context, rect, state, palette, true, false, memento);
                case PaletteRibbonColorStyle.RibbonGroupNormalBorderTrackingLight:
                    return DrawRibbonGroupNormalBorder(context, rect, state, palette, true, true, memento);
                case PaletteRibbonColorStyle.RibbonGroupNormalTitle:
                    return DrawRibbonGroupNormalTitle(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonGroupAreaBorder:
                    return DrawRibbonGroupAreaBorder1And2(context, rect, state, palette, false, false, memento);
                case PaletteRibbonColorStyle.RibbonGroupAreaBorder2:
                    return DrawRibbonGroupAreaBorder1And2(context, rect, state, palette, true, false, memento);
                case PaletteRibbonColorStyle.RibbonGroupAreaBorder3:
                    return DrawRibbonGroupAreaBorder3And4(context, rect, state, palette, memento, true);
                case PaletteRibbonColorStyle.RibbonGroupAreaBorder4:
                    return DrawRibbonGroupAreaBorder3And4(context, rect, state, palette, memento, false);
                case PaletteRibbonColorStyle.RibbonGroupAreaBorderContext:
                    return DrawRibbonGroupAreaBorderContext(context, rect, state, palette, memento);
                case PaletteRibbonColorStyle.RibbonTabTracking2007:
                    return DrawRibbonTabTracking2007(shape, context, rect, state, palette, orientation, memento);
                case PaletteRibbonColorStyle.RibbonTabFocus2010:
                    return DrawRibbonTabFocus2010(shape, context, rect, state, palette, orientation, memento);
                case PaletteRibbonColorStyle.RibbonTabTracking2010:
                    return DrawRibbonTabTracking2010(shape, context, rect, state, palette, orientation, memento, true);
                case PaletteRibbonColorStyle.RibbonTabTracking2010Alt:
                    return DrawRibbonTabTracking2010(shape, context, rect, state, palette, orientation, memento, false);
                case PaletteRibbonColorStyle.RibbonTabGlowing:
                    return DrawRibbonTabGlowing(shape, context, rect, state, palette, orientation, memento);
                case PaletteRibbonColorStyle.RibbonTabHighlight:
                    return DrawRibbonTabHighlight(shape, context, rect, state, palette, orientation, memento, false);
                case PaletteRibbonColorStyle.RibbonTabHighlight2:
                    return DrawRibbonTabHighlight(shape, context, rect, state, palette, orientation, memento, true);
                case PaletteRibbonColorStyle.RibbonTabSelected2007:
                    return DrawRibbonTabSelected2007(context, rect, state, palette, orientation, memento);
                case PaletteRibbonColorStyle.RibbonTabSelected2010:
                    return DrawRibbonTabSelected2010(context, rect, state, palette, orientation, memento, true);
                case PaletteRibbonColorStyle.RibbonTabSelected2010Alt:
                    return DrawRibbonTabSelected2010(context, rect, state, palette, orientation, memento, false);
                case PaletteRibbonColorStyle.RibbonTabContextSelected:
                    return DrawRibbonTabContextSelected(shape, context, rect, state, palette, orientation, memento);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return null;
        }

        /// <summary>
        /// Draw a context ribbon tab title.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="paletteGeneral">Palette used for general ribbon settings.</param>
        /// <param name="paletteBack">Palette used for background ribbon settings.</param>
        /// <param name="memento">Cached storage for drawing objects.</param>
        public override IDisposable DrawRibbonTabContextTitle(PaletteRibbonShape shape,
                                                              RenderContext context,
                                                              Rectangle rect,
                                                              IPaletteRibbonGeneral paletteGeneral,
                                                              IPaletteRibbonBack paletteBack,
                                                              IDisposable memento)
        {
            return DrawRibbonTabContext(context, rect, paletteGeneral, paletteBack, memento);
        }

        /// <summary>
        /// Draw the application button.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Rendering context.</param>
        /// <param name="rect">Target rectangle.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="palette">Palette used for sourcing settings.</param>
        /// <param name="memento">Cached storage for drawing objects.</param>
        public override IDisposable DrawRibbonApplicationButton(PaletteRibbonShape shape,
                                                                RenderContext context,
                                                                Rectangle rect,
                                                                PaletteState state,
                                                                IPaletteRibbonBack palette,
                                                                IDisposable memento)
        {
            return DrawRibbonAppButton(shape, context, rect, state, palette, false, memento);
        }

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
        public override IDisposable DrawRibbonApplicationTab(PaletteRibbonShape shape,
                                                             RenderContext context,
                                                             Rectangle rect,
                                                             PaletteState state,
                                                             Color baseColor1,
                                                             Color baseColor2,
                                                             IDisposable memento)
        {
            return DrawRibbonAppTab(shape, context, rect, state, baseColor1, baseColor2, memento);
        }

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

            // Draw inside of the border edge in a lighter version of the border
            using (SolidBrush drawBrush = new SolidBrush(paletteBack.GetBackColor1(state)))
                context.Graphics.FillRectangle(drawBrush, displayRect);
        }
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
            Debug.Assert(context != null);
            Debug.Assert(paletteBack != null);
            Debug.Assert(paletteBorder != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (paletteBack == null) throw new ArgumentNullException("paletteBack");
            if (paletteBorder == null) throw new ArgumentNullException("paletteBorder");

            Debug.Assert(context.Control != null);
            Debug.Assert(!context.Control.IsDisposed);

            // Do we need to draw the background?
            if (paletteBack.GetBackDraw(state) == InheritBool.True)
            {
                // Convert from separator orientation to border orientation value
                VisualOrientation borderOrientation = (orientation == Orientation.Horizontal ? VisualOrientation.Top :
                                                                                               VisualOrientation.Left);

                // Ask the border renderer for a path that encloses the border
                using (GraphicsPath borderPath = context.Renderer.RenderStandardBorder.GetBackPath(context, displayRect, paletteBorder, borderOrientation, state))
                {
                    // Get the padding needed for the drawing area inside the border
                    Padding borderPadding = context.Renderer.RenderStandardBorder.GetBorderRawPadding(paletteBorder, state, borderOrientation);

                    // The area available for border drawing if the client rectangle with padding applied
                    Rectangle enclosingRect = CommonHelper.ApplyPadding(borderOrientation, displayRect, borderPadding);

                    // Convert from the two state orientation to our four state orientation
                    VisualOrientation vo = (orientation == Orientation.Horizontal) ? VisualOrientation.Top : VisualOrientation.Left;

                    // Render the background inside the border path
                    context.Renderer.RenderStandardBack.DrawBack(context, enclosingRect, borderPath, paletteBack, vo, state, null);
                }
            }

            // Do we need to draw the border?
            if (paletteBorder.GetBorderDraw(state) == InheritBool.True)
            {
                // Convert from the two state orientation to our four state orientation
                VisualOrientation vo = (orientation == Orientation.Horizontal) ? VisualOrientation.Top : VisualOrientation.Left;

                // Render the border over the background and children
                context.Renderer.RenderStandardBorder.DrawBorder(context, displayRect, paletteBorder, vo, state);
            }
        }

        /// <summary>
        /// Calculate the requested display size for the check box.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="enabled">Should check box be displayed as enabled.</param>
        /// <param name="checkState">The checked state of the check box.</param>
        /// <param name="tracking">Should check box be displayed as hot tracking.</param>
        /// <param name="pressed">Should check box be displayed as pressed.</param>
        public override Size GetCheckBoxPreferredSize(ViewLayoutContext context,
                                                      IPalette palette,
                                                      bool enabled,
                                                      CheckState checkState,
                                                      bool tracking,
                                                      bool pressed)
        {
            Debug.Assert(context != null);
            Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            // Grab an image appropriate to the state
            Image drawImage = palette.GetCheckBoxImage(enabled, checkState, tracking, pressed);

            // If no image from the palette then get a system check box
            if (drawImage == null)
            {
                // Convert incoming parameters to check box state
                CheckBoxState state = DiscoverCheckBoxState(enabled, checkState, tracking, pressed);

                // Request the drawing size of the check box glyph
                return CheckBoxRenderer.GetGlyphSize(context.Graphics, state);
            }
            else
                return drawImage.Size;
        }

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
        public override void DrawCheckBox(RenderContext context,
                                          Rectangle displayRect,
                                          IPalette palette,
                                          bool enabled,
                                          CheckState checkState,
                                          bool tracking,
                                          bool pressed)
        {
            Debug.Assert(context != null);
            Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            // Grab an image appropriate to the state
            Image drawImage = palette.GetCheckBoxImage(enabled, checkState, tracking, pressed);

            // If no image from the palette then get a system check box
            if (drawImage == null)
            {
                // Convert incoming parameters to check box state
                CheckBoxState state = DiscoverCheckBoxState(enabled, checkState, tracking, pressed);

                // Request the glyph be drawn at the top left of the display rectangle
                CheckBoxRenderer.DrawCheckBox(context.Graphics, displayRect.Location, state);
            }
            else
            {
                // Find the offset to center the image
                int xOffset = (displayRect.Width - drawImage.Width) / 2;
                int yOffset = (displayRect.Height - drawImage.Height) / 2;

                // Draw the image centered
                context.Graphics.DrawImage(drawImage, 
                                           displayRect.X + xOffset, displayRect.Y + yOffset, 
                                           drawImage.Width, drawImage.Height);
            }
        }

        /// <summary>
        /// Calculate the requested display size for the radio button.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="enabled">Should check box be displayed as enabled.</param>
        /// <param name="checkState">Checked state of the radio button.</param>
        /// <param name="tracking">Should check box be displayed as hot tracking.</param>
        /// <param name="pressed">Should check box be displayed as pressed.</param>
        public override Size GetRadioButtonPreferredSize(ViewLayoutContext context,
                                                         IPalette palette,
                                                         bool enabled,
                                                         bool checkState,
                                                         bool tracking,
                                                         bool pressed)
        {
            // Grab an image appropriate to the state
            Image drawImage = palette.GetRadioButtonImage(enabled, checkState, tracking, pressed);

            if (drawImage == null)
            {
                // Convert incoming parameters to radio button state
                RadioButtonState state = DiscoverRadioButtonState(enabled, checkState, tracking, pressed);

                // Request the drawing size of the radio button glyph
                return RadioButtonRenderer.GetGlyphSize(context.Graphics, state);
            }
            else
                return drawImage.Size;
        }

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
        public override void DrawRadioButton(RenderContext context,
                                             Rectangle displayRect,
                                             IPalette palette,
                                             bool enabled,
                                             bool checkState,
                                             bool tracking,
                                             bool pressed)
        {
            Debug.Assert(context != null);
            Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            // Grab an image appropriate to the state
            Image drawImage = palette.GetRadioButtonImage(enabled, checkState, tracking, pressed);

            // If no image from the palette then get a system radio button
            if (drawImage == null)
            {
                // Convert incoming parameters to radio button state
                RadioButtonState state = DiscoverRadioButtonState(enabled, checkState, tracking, pressed);

                // Request the glyph be drawn at the top left of the display rectangle
                RadioButtonRenderer.DrawRadioButton(context.Graphics, displayRect.Location, state);
            }
            else
            {
                // Find the offset to center the image
                int xOffset = (displayRect.Width - drawImage.Width) / 2;
                int yOffset = (displayRect.Height - drawImage.Height) / 2;

                // Draw the image centered
                context.Graphics.DrawImage(drawImage,
                                           displayRect.X + xOffset, displayRect.Y + yOffset,
                                           drawImage.Width, drawImage.Height);
            }
        }

        /// <summary>
        /// Calculate the requested display size for the drop down button.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="state">State for which image size is needed.</param>
        /// <param name="orientation">How to orientate the image.</param>
        public override Size GetDropDownButtonPreferredSize(ViewLayoutContext context,
                                                            IPalette palette,
                                                            PaletteState state,
                                                            VisualOrientation orientation)
        {
            // Grab an image appropriate to the state
            Image drawImage = palette.GetDropDownButtonImage(state);

            // Get the image defined size
            Size imageSize = Size.Empty;
            if (drawImage != null)
                imageSize = drawImage.Size;

            // Alter size for different orientations
            if ((orientation == VisualOrientation.Left) ||
                (orientation == VisualOrientation.Right))
            {
                // Switch dimensions to reflect rotation of 90 or 270 degrees
                imageSize = new Size(imageSize.Height, imageSize.Width);
            }

            return imageSize;
        }

        /// <summary>
        /// Perform drawing of a drop down button.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="palette">Palette for sourcing display values.</param>
        /// <param name="state">State for which image size is needed.</param>
        /// <param name="orientation">How to orientate the image.</param>
        public override void DrawDropDownButton(RenderContext context,
                                                Rectangle displayRect,
                                                IPalette palette,
                                                PaletteState state,
                                                VisualOrientation orientation)
        {
            Debug.Assert(context != null);
            Debug.Assert(palette != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (palette == null) throw new ArgumentNullException("palette");

            // Grab an image appropriate to the state
            Image drawImage = palette.GetDropDownButtonImage(state);
            if (drawImage != null)
                DrawImageHelper(context, drawImage, Color.Empty, 
                                displayRect, orientation, PaletteImageEffect.Normal, 
                                Color.Empty, Color.Empty);
        }

        /// <summary>
        /// Draw a numeric up button image appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawInputControlNumericUpGlyph(RenderContext context,
                                                            Rectangle cellRect,
                                                            IPaletteContent paletteContent,
                                                            PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteContent != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (paletteContent == null) throw new ArgumentNullException("paletteContent");

            Color c1 = paletteContent.GetContentShortTextColor1(state);
            Color c2 = paletteContent.GetContentShortTextColor2(state);

            // Find the top left starting position for drawing lines
            int xStart = cellRect.Left + (cellRect.Right - cellRect.Left - 4) / 2;
            int yStart = cellRect.Top + (cellRect.Bottom - cellRect.Top - 3) / 2;

            using (Pen darkPen = new Pen(c1),
                       lightPen = new Pen(c2))
            {
                context.Graphics.DrawLine(darkPen, xStart, yStart + 3, xStart + 4, yStart + 3);
                context.Graphics.DrawLine(darkPen, xStart + 1, yStart + 2, xStart + 3, yStart + 2);
                context.Graphics.DrawLine(darkPen, xStart + 2, yStart + 2, xStart + 2, yStart + 1);
                context.Graphics.DrawLine(lightPen, xStart + 2, yStart, xStart + 4, yStart + 2);
                context.Graphics.DrawLine(lightPen, xStart + 2, yStart, xStart, yStart + 2);
            }
        }

        /// <summary>
        /// Draw a numeric down button image appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawInputControlNumericDownGlyph(RenderContext context,
                                                              Rectangle cellRect,
                                                              IPaletteContent paletteContent,
                                                              PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteContent != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (paletteContent == null) throw new ArgumentNullException("paletteContent");

            Color c1 = paletteContent.GetContentShortTextColor1(state);
            Color c2 = paletteContent.GetContentShortTextColor2(state);

            // Find the top left starting position for drawing lines
            int xStart = cellRect.Left + (cellRect.Right - cellRect.Left - 4) / 2;
            int yStart = cellRect.Top + (cellRect.Bottom - cellRect.Top - 3) / 2;

            using (Pen darkPen = new Pen(c1),
                       lightPen = new Pen(c2))
            {
                context.Graphics.DrawLine(darkPen, xStart, yStart, xStart + 4, yStart);
                context.Graphics.DrawLine(darkPen, xStart + 1, yStart + 1, xStart + 3, yStart + 1);
                context.Graphics.DrawLine(darkPen, xStart + 2, yStart + 2, xStart + 2, yStart + 1);
                context.Graphics.DrawLine(lightPen, xStart, yStart + 1, xStart + 2, yStart + 3);
                context.Graphics.DrawLine(lightPen, xStart + 2, yStart + 3, xStart + 4, yStart + 1);
            }
        }

        /// <summary>
        /// Draw a drop down grid appropriate for a input control.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="paletteContent">Content palette for getting colors.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawInputControlDropDownGlyph(RenderContext context,
                                                           Rectangle cellRect,
                                                           IPaletteContent paletteContent,
                                                           PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteContent != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (paletteContent == null) throw new ArgumentNullException("paletteContent");

            Color c1 = paletteContent.GetContentShortTextColor1(state);
            Color c2 = paletteContent.GetContentShortTextColor2(state);

            // Find the top left starting position for drawing lines
            int xStart = cellRect.Left + (cellRect.Right - cellRect.Left - 4) / 2;
            int yStart = cellRect.Top + (cellRect.Bottom - cellRect.Top - 3) / 2;

            using (Pen darkPen = new Pen(c1),
                       lightPen = new Pen(c2))
            {
                context.Graphics.DrawLine(darkPen, xStart, yStart, xStart + 4, yStart);
                context.Graphics.DrawLine(darkPen, xStart + 1, yStart + 1, xStart + 3, yStart + 1);
                context.Graphics.DrawLine(darkPen, xStart + 2, yStart + 2, xStart + 2, yStart + 1);
                context.Graphics.DrawLine(lightPen, xStart, yStart + 1, xStart + 2, yStart + 3);
                context.Graphics.DrawLine(lightPen, xStart + 2, yStart + 3, xStart + 4, yStart + 1);
            }
        }

        /// <summary>
        /// Perform drawing of a ribbon dialog box launcher glyph.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawRibbonDialogBoxLauncher(PaletteRibbonShape shape,
                                                         RenderContext context,
                                                         Rectangle displayRect,
                                                         IPaletteRibbonGeneral paletteGeneral,
                                                         PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteGeneral != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (paletteGeneral == null) throw new ArgumentNullException("paletteGeneral");

            switch (shape)
            {
                default:
                case PaletteRibbonShape.Office2007:
                    using (Pen darkPen = new Pen(paletteGeneral.GetRibbonGroupDialogDark(state)),
                               lightPen = new Pen(paletteGeneral.GetRibbonGroupDialogLight(state)))
                    {
                        context.Graphics.DrawLine(darkPen, displayRect.Left, displayRect.Top + 5, displayRect.Left, displayRect.Top);
                        context.Graphics.DrawLine(darkPen, displayRect.Left, displayRect.Top, displayRect.Left + 5, displayRect.Top);
                        context.Graphics.DrawLine(lightPen, displayRect.Left + 1, displayRect.Top + 5, displayRect.Left + 1, displayRect.Top + 1);
                        context.Graphics.DrawLine(lightPen, displayRect.Left + 1, displayRect.Top + 1, displayRect.Left + 5, displayRect.Top + 1);
                        context.Graphics.DrawLine(lightPen, displayRect.Right - 1, displayRect.Bottom - 5, displayRect.Right - 1, displayRect.Bottom - 1);
                        context.Graphics.DrawLine(lightPen, displayRect.Right - 1, displayRect.Bottom - 1, displayRect.Right - 4, displayRect.Bottom - 1);
                        context.Graphics.DrawLine(lightPen, displayRect.Right - 1, displayRect.Bottom - 1, displayRect.Right - 4, displayRect.Bottom - 5);
                        context.Graphics.DrawLine(darkPen, displayRect.Right - 5, displayRect.Bottom - 2, displayRect.Right - 2, displayRect.Bottom - 2);
                        context.Graphics.DrawLine(darkPen, displayRect.Right - 4, displayRect.Bottom - 3, displayRect.Right - 3, displayRect.Bottom - 3);
                        context.Graphics.DrawLine(darkPen, displayRect.Right - 2, displayRect.Bottom - 2, displayRect.Right - 2, displayRect.Bottom - 5);
                        context.Graphics.DrawLine(darkPen, displayRect.Right - 3, displayRect.Bottom - 3, displayRect.Right - 3, displayRect.Bottom - 4);
                        context.Graphics.DrawLine(darkPen, displayRect.Right - 5, displayRect.Bottom - 5, displayRect.Right - 3, displayRect.Bottom - 3);
                    }
                    break;
                case PaletteRibbonShape.Office2010:
                    LinearGradientBrush dialogBrush = new LinearGradientBrush(new RectangleF(displayRect.X - 1, displayRect.Y - 1, displayRect.Width + 2, displayRect.Height + 2),
                                                                              paletteGeneral.GetRibbonGroupDialogLight(state),
                                                                              paletteGeneral.GetRibbonGroupDialogDark(state), 
                                                                              45f);

                    using (Pen dialogPen = new Pen(dialogBrush))
                    {
                        context.Graphics.DrawLine(dialogPen, displayRect.Left, displayRect.Top + 5, displayRect.Left, displayRect.Top);
                        context.Graphics.DrawLine(dialogPen, displayRect.Left, displayRect.Top, displayRect.Left + 5, displayRect.Top);
                        context.Graphics.DrawLine(dialogPen, displayRect.Right - 5, displayRect.Bottom - 2, displayRect.Right - 2, displayRect.Bottom - 2);
                        context.Graphics.DrawLine(dialogPen, displayRect.Right - 4, displayRect.Bottom - 3, displayRect.Right - 3, displayRect.Bottom - 3);
                        context.Graphics.DrawLine(dialogPen, displayRect.Right - 2, displayRect.Bottom - 2, displayRect.Right - 2, displayRect.Bottom - 5);
                        context.Graphics.DrawLine(dialogPen, displayRect.Right - 3, displayRect.Bottom - 3, displayRect.Right - 3, displayRect.Bottom - 4);
                        context.Graphics.DrawLine(dialogPen, displayRect.Right - 5, displayRect.Bottom - 5, displayRect.Right - 3, displayRect.Bottom - 3);
                    }
                    break;
            }
        }

        /// <summary>
        /// Perform drawing of a ribbon drop arrow glyph.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawRibbonDropArrow(PaletteRibbonShape shape,
                                                 RenderContext context,
                                                 Rectangle displayRect,
                                                 IPaletteRibbonGeneral paletteGeneral,
                                                 PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteGeneral != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (paletteGeneral == null) throw new ArgumentNullException("paletteGeneral");

            Color darkColor = (state == PaletteState.Disabled ? paletteGeneral.GetRibbonDisabledDark(state) :
                                                                paletteGeneral.GetRibbonDropArrowDark(state));

            Color lightColor = (state == PaletteState.Disabled ? paletteGeneral.GetRibbonDisabledLight(state) :
                                                                 paletteGeneral.GetRibbonDropArrowLight(state));

            switch (shape)
            {
                default:
                case PaletteRibbonShape.Office2007:
                    using (Pen darkPen = new Pen(darkColor),
                               lightPen = new Pen(lightColor))
                    {
                        context.Graphics.DrawLine(darkPen, displayRect.Left, displayRect.Top, displayRect.Left + 4, displayRect.Top);
                        context.Graphics.DrawLine(darkPen, displayRect.Left + 1, displayRect.Top + 1, displayRect.Left + 3, displayRect.Top + 1);
                        context.Graphics.DrawLine(darkPen, displayRect.Left + 2, displayRect.Top + 1, displayRect.Left + 2, displayRect.Top + 2);
                        context.Graphics.DrawLine(lightPen, displayRect.Left, displayRect.Top + 1, displayRect.Left + 2, displayRect.Top + 3);
                        context.Graphics.DrawLine(lightPen, displayRect.Left + 2, displayRect.Top + 3, displayRect.Left + 4, displayRect.Top + 1);
                    }
                    break;
                case PaletteRibbonShape.Office2010:
                    using (LinearGradientBrush fillBrush = new LinearGradientBrush(new RectangleF(displayRect.X - 1, displayRect.Y - 1, displayRect.Width + 2, displayRect.Height + 2),
                                                                                   lightColor, darkColor, 45f))
                    {
                        context.Graphics.FillPolygon(fillBrush, new Point[]{ new Point(displayRect.Left - 1, displayRect.Top - 1), 
                                                                             new Point(displayRect.Left + 2, displayRect.Top + 3), 
                                                                             new Point(displayRect.Left + 5, displayRect.Top) });
                    }
                    break;
            }
        }

        /// <summary>
        /// Perform drawing of a ribbon context arrow glyph.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawRibbonContextArrow(PaletteRibbonShape shape,
                                                    RenderContext context,
                                                    Rectangle displayRect,
                                                    IPaletteRibbonGeneral paletteGeneral,
                                                    PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteGeneral != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (paletteGeneral == null) throw new ArgumentNullException("paletteGeneral");

            Color c1 = paletteGeneral.GetRibbonQATButtonDark(state);
            Color c2 = paletteGeneral.GetRibbonQATButtonLight(state);

            // If disabled then convert to black and white
            if (state == PaletteState.Disabled)
            {
                c1 = CommonHelper.ColorToBlackAndWhite(c1);
                c2 = CommonHelper.ColorToBlackAndWhite(c2);
            }

            using (Pen darkPen = new Pen(c1),
                       lightPen = new Pen(c2))
            {
                if (shape == PaletteRibbonShape.Office2010)
                {
                    context.Graphics.DrawLine(darkPen, displayRect.Left - 1, displayRect.Top, displayRect.Left + 5, displayRect.Top);
                    context.Graphics.DrawLine(lightPen, displayRect.Left - 1, displayRect.Top + 1, displayRect.Left + 5, displayRect.Top + 1);
                }
                else
                {
                    context.Graphics.DrawLine(darkPen, displayRect.Left, displayRect.Top, displayRect.Left + 4, displayRect.Top);
                    context.Graphics.DrawLine(lightPen, displayRect.Left, displayRect.Top + 1, displayRect.Left + 4, displayRect.Top + 1);
                }

                context.Graphics.DrawLine(darkPen, displayRect.Left, displayRect.Top + 3, displayRect.Left + 4, displayRect.Top + 3);
                context.Graphics.DrawLine(darkPen, displayRect.Left + 1, displayRect.Top + 4, displayRect.Left + 3, displayRect.Top + 4);
                context.Graphics.DrawLine(darkPen, displayRect.Left + 2, displayRect.Top + 4, displayRect.Left + 2, displayRect.Top + 5);
                context.Graphics.DrawLine(lightPen, displayRect.Left, displayRect.Top + 4, displayRect.Left + 2, displayRect.Top + 6);
                context.Graphics.DrawLine(lightPen, displayRect.Left + 2, displayRect.Top + 6, displayRect.Left + 4, displayRect.Top + 4);
            }
        }

        /// <summary>
        /// Perform drawing of a ribbon overflow image.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawRibbonOverflow(PaletteRibbonShape shape,
                                                RenderContext context,
                                                Rectangle displayRect,
                                                IPaletteRibbonGeneral paletteGeneral,
                                                PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteGeneral != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (paletteGeneral == null) throw new ArgumentNullException("paletteGeneral");

            Color c1 = paletteGeneral.GetRibbonQATButtonDark(state);
            Color c2 = paletteGeneral.GetRibbonQATButtonLight(state);

            // Office 2010 uses the same color for both parts
            if (shape == PaletteRibbonShape.Office2010)
                c2 = c1;

            // If disabled then convert to black and white
            if (state == PaletteState.Disabled)
            {
                c1 = CommonHelper.ColorToBlackAndWhite(c1);
                c2 = CommonHelper.ColorToBlackAndWhite(c2);
            }

            using (Pen darkPen = new Pen(c1),
                       lightPen = new Pen(c2))
            {
                context.Graphics.DrawLine(darkPen, displayRect.Left, displayRect.Top + 1, displayRect.Left, displayRect.Top + 3);
                context.Graphics.DrawLine(darkPen, displayRect.Left + 1, displayRect.Top + 2, displayRect.Left, displayRect.Top + 3);
                context.Graphics.DrawLine(lightPen, displayRect.Left, displayRect.Top, displayRect.Left + 2, displayRect.Top + 2);
                context.Graphics.DrawLine(lightPen, displayRect.Left + 1, displayRect.Top + 3, displayRect.Left, displayRect.Top + 4);

                context.Graphics.DrawLine(darkPen, displayRect.Left + 4, displayRect.Top + 1, displayRect.Left + 4, displayRect.Top + 3);
                context.Graphics.DrawLine(darkPen, displayRect.Left + 5, displayRect.Top + 2, displayRect.Left + 4, displayRect.Top + 3);
                context.Graphics.DrawLine(lightPen, displayRect.Left + 4, displayRect.Top, displayRect.Left + 6, displayRect.Top + 2);
                context.Graphics.DrawLine(lightPen, displayRect.Left + 5, displayRect.Top + 3, displayRect.Left + 4, displayRect.Top + 4);
            }
        }

        /// <summary>
        /// Perform drawing of a ribbon group separator.
        /// </summary>
        /// <param name="shape">Ribbon shape.</param>
        /// <param name="context">Render context.</param>
        /// <param name="displayRect">Display area available for drawing.</param>
        /// <param name="paletteGeneral">General ribbon palette details.</param>
        /// <param name="state">State associated with rendering.</param>
        public override void DrawRibbonGroupSeparator(PaletteRibbonShape shape,
                                                      RenderContext context,
                                                      Rectangle displayRect,
                                                      IPaletteRibbonGeneral paletteGeneral,
                                                      PaletteState state)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteGeneral != null);

            // Validate parameter references
            if (context == null) throw new ArgumentNullException("context");
            if (paletteGeneral == null) throw new ArgumentNullException("paletteGeneral");

            int x = displayRect.X + (displayRect.Width - 2) / 2;
            Color darkColor = paletteGeneral.GetRibbonGroupSeparatorDark(state);
            Color lightColor = paletteGeneral.GetRibbonGroupSeparatorLight(state);

            switch (shape)
            {
                default:
                case PaletteRibbonShape.Office2007:
                    using (Pen darkPen = new Pen(darkColor),
                               lightPen = new Pen(lightColor))
                    {
                        context.Graphics.DrawLine(lightPen, x, displayRect.Top + 2, x, displayRect.Bottom - 3);
                        context.Graphics.DrawLine(darkPen, x + 1, displayRect.Top + 2, x + 1, displayRect.Bottom - 3);
                    }
                    break;
                case PaletteRibbonShape.Office2010:
                    using (LinearGradientBrush darkBrush = new LinearGradientBrush(new RectangleF(displayRect.X, displayRect.Y - 1, displayRect.Width, displayRect.Height + 2), Color.FromArgb(72, darkColor), darkColor, 90f),
                                               lightBrush = new LinearGradientBrush(new RectangleF(displayRect.X - 1, displayRect.Y - 1, displayRect.Width + 2, displayRect.Height + 2), Color.FromArgb(128, lightColor), lightColor, 90f))
                    {
                        darkBrush.SetSigmaBellShape(0.5f);
                        lightBrush.SetSigmaBellShape(0.5f);

                        using (Pen darkPen = new Pen(darkBrush))
                        {
                            context.Graphics.FillRectangle(lightBrush, x, displayRect.Top, 3, displayRect.Height);
                            context.Graphics.DrawLine(darkPen, x + 1, displayRect.Top, x + 1, displayRect.Bottom - 1);
                        }
                    }
                    break;
            }
        }

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
        public override Rectangle DrawGridSortGlyph(RenderContext context,
                                                    SortOrder sortOrder,
                                                    Rectangle cellRect,
                                                    IPaletteContent paletteContent,
                                                    PaletteState state,
                                                    bool rtl)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteContent != null);

            // Get the appropriate each to draw
            Image sortImage = _gridSortOrder.Images[(sortOrder == SortOrder.Ascending ? 0 : 1)];

            // Is there enough room to draw the image?
            if ((sortImage.Width < cellRect.Width) && (sortImage.Height < cellRect.Height))
            {
                // Find the drawing location of the image
                int y = cellRect.Top + (cellRect.Height - sortImage.Height) / 2;
                int x = (rtl ? cellRect.X : cellRect.Right - sortImage.Width);

                // Grab the foreground color to use for the image
                Color imageColor = paletteContent.GetContentShortTextColor1(state);

                // Draw the image with remapping the image color to the foreground color
                using (ImageAttributes attribs = new ImageAttributes())
                {
                    ColorMap cm = new ColorMap();
                    cm.OldColor = Color.Black;
                    cm.NewColor = CommonHelper.MergeColors(imageColor, 0.75f, Color.Transparent, 0.25f);
                    attribs.SetRemapTable(new ColorMap[] { cm }, ColorAdjustType.Bitmap);

                    context.Graphics.DrawImage(sortImage,
                                               new Rectangle(x, y, sortImage.Width, sortImage.Height), 
                                               0, 0, sortImage.Width, sortImage.Height, 
                                               GraphicsUnit.Pixel, attribs);
                }

                // Reduce the cell rect by that used up
                cellRect.Width -= sortImage.Width;

                // With rtl we need to move across to the right
                if (rtl)
                    cellRect.X += sortImage.Width;
            }

            return cellRect;
        }

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
        public override Rectangle DrawGridRowGlyph(RenderContext context,
                                                   GridRowGlyph rowGlyph,
                                                   Rectangle cellRect,
                                                   IPaletteContent paletteContent,
                                                   PaletteState state,
                                                   bool rtl)
        {
            Debug.Assert(context != null);
            Debug.Assert(paletteContent != null);

            // Get the appropriate each to draw
            Image rowImage = null;

            switch (rowGlyph)
            {
                case GridRowGlyph.ArrowStar:
                    rowImage = _gridRowIndicators.Images[rtl ? 4 : 0];
                    break;
                case GridRowGlyph.Star:
                    rowImage = _gridRowIndicators.Images[rtl ? 5 : 1];
                    break;
                case GridRowGlyph.Pencil:
                    rowImage = _gridRowIndicators.Images[rtl ? 6 : 2];
                    break;
                case GridRowGlyph.Arrow:
                    rowImage = _gridRowIndicators.Images[rtl ? 7 : 3];
                    break;
            }

            // Is there enough room to draw the image?
            if ((rowImage != null) && 
                (rowImage.Width < cellRect.Width) && 
                (rowImage.Height < cellRect.Height))
            {
                // Find the drawing location of the image
                int y = cellRect.Top + (cellRect.Height - rowImage.Height) / 2;
                int x = (rtl ? cellRect.Right - rowImage.Width : cellRect.Left);

                // Grab the foreground color to use for the image
                Color imageColor = paletteContent.GetContentShortTextColor1(state);

                // Draw the image with remapping the image color to the foreground color
                using (ImageAttributes attribs = new ImageAttributes())
                {
                    ColorMap cm = new ColorMap();
                    cm.OldColor = Color.Black;
                    cm.NewColor = CommonHelper.MergeColors(imageColor, 0.75f, Color.Transparent, 0.25f);
                    attribs.SetRemapTable(new ColorMap[] { cm }, ColorAdjustType.Bitmap);

                    context.Graphics.DrawImage(rowImage,
                                               new Rectangle(x, y, rowImage.Width, rowImage.Height),
                                               0, 0, rowImage.Width, rowImage.Height,
                                               GraphicsUnit.Pixel, attribs);
                }

                // Reduce the cell rect by that used up
                cellRect.Width -= rowImage.Width;

                // With NOT rtl we need to move across to the right
                if (!rtl)
                    cellRect.X += rowImage.Width;
            }

            return cellRect;
        }

        /// <summary>
        /// Draw a grid error glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="cellRect">Available drawing rectangle space.</param>
        /// <param name="state">State associated with rendering.</param>
        /// <param name="rtl">Should be drawn from right to left.</param>
        /// <returns>Remainder space left over for other drawing.</returns>
        public override Rectangle DrawGridErrorGlyph(RenderContext context,
                                                     Rectangle cellRect,
                                                     PaletteState state,
                                                     bool rtl)
        {
            Debug.Assert(context != null);

            // Get the appropriate each to draw
            Image errorImage = _gridErrorIcon.Images[0];

            // Is there enough room to draw the image?
            if ((errorImage.Width < cellRect.Width) && (errorImage.Height < cellRect.Height))
            {
                // Find the drawing location of the image
                int y = cellRect.Top + (cellRect.Height - errorImage.Height) / 2;
                int x = (rtl ? cellRect.Left : cellRect.Right - errorImage.Width);

                if (state == PaletteState.Disabled)
                    ControlPaint.DrawImageDisabled(context.Graphics, errorImage, x, y, Color.Empty);
                else
                    context.Graphics.DrawImage(errorImage, x, y);

                // Reduce the cell rect by that used up
                cellRect.Width -= errorImage.Width;

                // With rtl we need to move across to the right
                if (rtl)
                    cellRect.X += errorImage.Width;
            }

            return cellRect;
        }

        /// <summary>
        /// Draw a solid area glyph suitable for a drag drop area.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="drawRect">Drawing rectangle space.</param>
        /// <param name="dragDropPalette">Palette source of drawing values.</param>
        public override void DrawDragDropSolidGlyph(RenderContext context,
                                                    Rectangle drawRect,
                                                    IPaletteDragDrop dragDropPalette)
        {
            Debug.Assert(context != null);
            Debug.Assert(dragDropPalette != null);

            using(SolidBrush backBrush = new SolidBrush(dragDropPalette.GetDragDropSolidBack()))
                context.Graphics.FillRectangle(backBrush, drawRect);

            using (Pen borderPen = new Pen(dragDropPalette.GetDragDropSolidBorder()))
                context.Graphics.DrawRectangle(borderPen, drawRect);
        }

        /// <summary>
        /// Measure the drag and drop docking glyphs.
        /// </summary>
        /// <param name="dragData">Set of drag docking data.</param>
        /// <param name="dragDropPalette">Palette source of drawing values.</param>
        /// <param name="feedback">Feedback requested.</param>
        public override void MeasureDragDropDockingGlyph(RenderDragDockingData dragData,
                                                         IPaletteDragDrop dragDropPalette,
                                                         PaletteDragFeedback feedback)
        {
            Debug.Assert(dragData != null);
            Debug.Assert(dragDropPalette != null);

            if (feedback == PaletteDragFeedback.Rounded)
                MeasureDragDockingRounded(dragData, dragDropPalette);
            else
                MeasureDragDockingSquares(dragData, dragDropPalette);
        }

        /// <summary>
        /// Draw a solid area glyph suitable for a drag drop area.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="dragData">Set of drag docking data.</param>
        /// <param name="dragDropPalette">Palette source of drawing values.</param>
        /// <param name="feedback">Feedback requested.</param>
        public override void DrawDragDropDockingGlyph(RenderContext context,
                                                      RenderDragDockingData dragData,
                                                      IPaletteDragDrop dragDropPalette,
                                                      PaletteDragFeedback feedback)
        {
            Debug.Assert(context != null);
            Debug.Assert(dragData != null);
            Debug.Assert(dragDropPalette != null);

            if (feedback == PaletteDragFeedback.Rounded)
                DrawDragDockingRounded(context, dragData, dragDropPalette);
            else
                DrawDragDockingSquares(context, dragData, dragDropPalette);
        }
        #endregion

        #region EvalTransparentPaint
        /// <summary>
        /// Evaluate if transparent painting is needed for background palette.
        /// </summary>
        /// <param name="paletteBack">Background palette to test.</param>
        /// <param name="state">Element state associated with palette.</param>
        /// <returns>True if transparent painting required.</returns>
        public override bool EvalTransparentPaint(IPaletteBack paletteBack,
                                                  PaletteState state)
        {
            // If the background is not being painted, then has transparency
            if (paletteBack.GetBackDraw(state) == InheritBool.False)
                return true;
            else
            {
                // If the first color has alpha channel then has transparency
                if (paletteBack.GetBackColor1(state).A < 255)
                    return true;
                else
                {
                    // Does the draw style require use of the second color?
                    if (paletteBack.GetBackColorStyle(state) != PaletteColorStyle.Solid)
                    {
                        // If the second color has alpha channel then has transparency
                        if (paletteBack.GetBackColor2(state).A < 255)
                            return true;
                    }
                }
            }

            // Cannot find any transparent areas
            return false;
        }

        /// <summary>
        /// Evaluate if transparent painting is needed for background or border palettes.
        /// </summary>
        /// <param name="paletteBack">Background palette to test.</param>
        /// <param name="paletteBorder">Background palette to test.</param>
        /// <param name="state">Element state associated with palette.</param>
        /// <returns>True if transparent painting required.</returns>
        public override bool EvalTransparentPaint(IPaletteBack paletteBack,
                                                  IPaletteBorder paletteBorder,
                                                  PaletteState state)
        {
            int rounding = paletteBorder.GetBorderRounding(state);

            // If the border takes up some visual space
            if (paletteBorder.GetBorderWidth(state) > 0)
            {
                // If the border is not being painted then it must be transparent
                if (paletteBorder.GetBorderDraw(state) == InheritBool.False)
                    return true;
                else
                {
                    // If there is rounding causing transparent corners
                    if (paletteBorder.GetBorderRounding(state) > 0)
                        return true;
                    else
                    {
                        // If the first color has alpha channel then has transparency
                        if (paletteBorder.GetBorderColor1(state).A < 255)
                            return true;
                        else
                        {
                            // Does the draw style require use of the second color?
                            if (paletteBorder.GetBorderColorStyle(state) != PaletteColorStyle.Solid)
                            {
                                // If the second color has alpha channel then has transparency
                                if (paletteBorder.GetBorderColor2(state).A < 255)
                                    return true;
                            }
                        }
                    }
                }
            }

            // The border does not cause transparency, check the background
            return EvalTransparentPaint(paletteBack, state);
        }

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
        public override void DrawTrackTicksGlyph(RenderContext context,
                                                 PaletteState state,
                                                 IPaletteElementColor elementPalette,
                                                 Rectangle drawRect,
                                                 Orientation orientation,
                                                 bool topRight,
                                                 Size positionSize,
                                                 int minimum,
                                                 int maximum,
                                                 int frequency)
        {
            // Never want a frequency less than 1
            if (frequency <= 0)
                frequency = 1;

            float range = maximum - minimum;
            using (Pen tickPen = new Pen(elementPalette.GetElementColor1(state)))
            {
                if (orientation == Orientation.Horizontal)
                {
                    // Reduce area by half the position indicator on each side
                    int halfPosition = positionSize.Width / 2;
                    drawRect.X += halfPosition;
                    drawRect.Width -= positionSize.Width;

                    // Draw a marker for each value between min and max
                    float factor = (range == 0) ? float.MinValue : (float)drawRect.Width / range;
                    float top = drawRect.Y + 1;
                    float bottom = drawRect.Bottom - 2;
                    for (int i = minimum, y = 0; i <= maximum; i += frequency, y += frequency)
                    {
                        float offset = drawRect.X + (factor * y);
                        if (!topRight)
                        {
                            top = drawRect.Y + 2;
                            bottom = drawRect.Bottom - 2;
                            if ((i == minimum) || (i == maximum))
                                top -= 1;
                        }
                        else
                        {
                            top = drawRect.Y + 1;
                            bottom = drawRect.Bottom - 3;
                            if ((i == minimum) || (i == maximum))
                                bottom += 1;
                        }

                        context.Graphics.DrawLine(tickPen, offset, top, offset, bottom);
                    }
                }
                else
                {
                    // Reduce area by half the position indicator on each side
                    int halfPosition = positionSize.Height / 2;
                    drawRect.Y += halfPosition;
                    drawRect.Height -= positionSize.Height;

                    // Draw a marker for each value between min and max
                    float factor = (range == 0) ? float.MinValue : (float)drawRect.Height / range;
                    float left = drawRect.X + 1;
                    float right = drawRect.Right - 2;
                    for (int i = minimum, y = 0; i <= maximum; i += frequency, y += frequency)
                    {
                        float offset = drawRect.Y + (factor * y);
                        if (topRight)
                        {
                            left = drawRect.X + 2;
                            right = drawRect.Right - 2;
                            if ((i == minimum) || (i == maximum))
                                left -= 1;
                        }
                        else
                        {
                            left = drawRect.X + 1;
                            right = drawRect.Right - 3;
                            if ((i == minimum) || (i == maximum))
                                right += 1;
                        }

                        context.Graphics.DrawLine(tickPen, left, offset, right, offset);
                    }
                }
            }
        }

        /// <summary>
        /// Draw the track bar track glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="state">Element state.</param>
        /// <param name="elementPalette">Source of palette colors.</param>
        /// <param name="drawRect">Drawing rectangle that should contain the track.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="volumeControl">Drawing as a volume control or standard slider.</param>
        public override void DrawTrackGlyph(RenderContext context,
                                            PaletteState state,
                                            IPaletteElementColor elementPalette,
                                            Rectangle drawRect,
                                            Orientation orientation,
                                            bool volumeControl)
        {
            // The position indicator leavesa gap at the left/right ends for horizontal and top/bottom for vertical
            // so we do not draw that last pixel so that when the indicator is at the end the track does not stick out
            if (orientation == Orientation.Horizontal)
                drawRect.Inflate(-1, 0);
            else
                drawRect.Inflate(0, -1);

            using (Pen border1Pen = new Pen(elementPalette.GetElementColor1(state)),
                       border2Pen = new Pen(elementPalette.GetElementColor2(state)))
            {
                using (SolidBrush insideBrush = new SolidBrush(elementPalette.GetElementColor3(state)))
                {
                    if (!volumeControl)
                    {
                        context.Graphics.FillRectangle(insideBrush, drawRect.X + 1, drawRect.Y + 1, drawRect.Width - 2, drawRect.Height - 2);

                        context.Graphics.DrawLines(border1Pen, new Point[]{ new Point(drawRect.Right - 1, drawRect.Y), 
                                                                            new Point(drawRect.X, drawRect.Y),
                                                                            new Point(drawRect.X, drawRect.Bottom - 1)});

                        context.Graphics.DrawLines(border2Pen, new Point[]{ new Point(drawRect.Right - 1, drawRect.Y + 1), 
                                                                            new Point(drawRect.Right - 1, drawRect.Bottom - 1),
                                                                            new Point(drawRect.X + 1, drawRect.Bottom - 1)});
                    }
                    else
                    {
                        if (orientation == Orientation.Horizontal)
                        {
                            using (AntiAlias aa = new AntiAlias(context.Graphics))
                            {
                                context.Graphics.FillPolygon(insideBrush, new Point[]{ new Point(drawRect.X, drawRect.Bottom - 2),
                                                                                       new Point(drawRect.Right - 1, drawRect.Y),
                                                                                       new Point(drawRect.Right - 1, drawRect.Bottom - 1),
                                                                                       new Point(drawRect.X, drawRect.Bottom - 1),
                                                                                       new Point(drawRect.X, drawRect.Bottom - 2)});

                                context.Graphics.DrawLines(border1Pen, new Point[]{ new Point(drawRect.Right - 1, drawRect.Y), 
                                                                                    new Point(drawRect.Right - 1, drawRect.Bottom - 1),
                                                                                    new Point(drawRect.X, drawRect.Bottom - 1),
                                                                                    new Point(drawRect.X, drawRect.Bottom - 2),
                                                                                    new Point(drawRect.Right - 1, drawRect.Y)});
                            }

                        }
                        else
                        {
                            using (AntiAlias aa = new AntiAlias(context.Graphics))
                            {
                                context.Graphics.FillPolygon(insideBrush, new Point[]{ new Point(drawRect.X + 1, drawRect.Bottom - 1),
                                                                                       new Point(drawRect.Right - 1, drawRect.Y + 1),
                                                                                       new Point(drawRect.X, drawRect.Y + 1),
                                                                                       new Point(drawRect.X, drawRect.Bottom - 1),
                                                                                       new Point(drawRect.X + 1, drawRect.Bottom - 1)});

                                context.Graphics.DrawLines(border1Pen, new Point[]{ new Point(drawRect.Right - 1, drawRect.Y + 1), 
                                                                                    new Point(drawRect.X, drawRect.Y + 1),
                                                                                    new Point(drawRect.X, drawRect.Bottom - 1),
                                                                                    new Point(drawRect.X + 1, drawRect.Bottom - 1),
                                                                                    new Point(drawRect.Right - 1, drawRect.Y + 1)});
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draw the track bar position glyph.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="state">Element state.</param>
        /// <param name="elementPalette">Source of palette colors.</param>
        /// <param name="drawRect">Drawing rectangle that should contain the track.</param>
        /// <param name="orientation">Drawing orientation.</param>
        /// <param name="tickStyle">Tick marks that surround the position.</param>
        public override void DrawTrackPositionGlyph(RenderContext context,
                                                    PaletteState state,
                                                    IPaletteElementColor elementPalette,
                                                    Rectangle drawRect,
                                                    Orientation orientation,
                                                    TickStyle tickStyle)
        {
            GraphicsPath outside = null;
            GraphicsPath border = null;
            GraphicsPath inside = null;

            if (orientation == Orientation.Horizontal)
            {
                switch (tickStyle)
                {
                    case TickStyle.None:
                    case TickStyle.Both:
                        CreatePositionPathsBoth(drawRect, ref outside, ref border, ref inside);
                        break;
                    case TickStyle.TopLeft:
                        CreatePositionPathsTop(drawRect, ref outside, ref border, ref inside);
                        break;
                    case TickStyle.BottomRight:
                        CreatePositionPathsBottom(drawRect, ref outside, ref border, ref inside);
                        break;
                }
            }
            else
            {
                switch (tickStyle)
                {
                    case TickStyle.None:
                    case TickStyle.Both:
                        CreatePositionPathsBoth(drawRect, ref outside, ref border, ref inside);
                        break;
                    case TickStyle.TopLeft:
                        CreatePositionPathsLeft(drawRect, ref outside, ref border, ref inside);
                        break;
                    case TickStyle.BottomRight:
                        CreatePositionPathsRight(drawRect, ref outside, ref border, ref inside);
                        break;
                }
            }

            if ((outside != null) && (border != null) && (inside != null))
            {
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    using(Pen outsidePen = new Pen(elementPalette.GetElementColor1(state)),
                              borderPen = new Pen(elementPalette.GetElementColor2(state)))
                    {
                        context.Graphics.DrawPath(outsidePen, outside);

                        using(SolidBrush insideBrush = new SolidBrush(elementPalette.GetElementColor3(state)))
                            context.Graphics.FillPath(insideBrush, border);

                        context.Graphics.DrawPath(borderPen, border);

                        using (LinearGradientBrush innerBrush = new LinearGradientBrush(inside.GetBounds(),
                                                                                        elementPalette.GetElementColor4(state),
                                                                                        elementPalette.GetElementColor5(state),
                                                                                        90f))
                        {
                            context.Graphics.FillPath(innerBrush, inside);
                        }
                    }
                }

                outside.Dispose();
                border.Dispose();
                inside.Dispose();
            }
        }

        private void CreatePositionPathsBoth(Rectangle drawRect, 
                                             ref GraphicsPath outside, 
                                             ref GraphicsPath border, 
                                             ref GraphicsPath inside)
        {
            outside = CreatePositionPathsBoth(drawRect);
            drawRect.Inflate(-1, -1);
            border = CreatePositionPathsBoth(drawRect);

            inside = new GraphicsPath();
            inside.AddRectangle(new Rectangle(drawRect.X + 2, drawRect.Y + 2, drawRect.Width - 5, drawRect.Height - 5));
        }

        private GraphicsPath CreatePositionPathsBoth(Rectangle drawRect)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLines(new PointF[]{ new PointF(drawRect.X + 0.75f, drawRect.Y),
                                        new PointF(drawRect.Right - 1.75f, drawRect.Y),
                                        new PointF(drawRect.Right - 1.0f, drawRect.Y + 0.75f),
                                        new PointF(drawRect.Right - 1.0f, drawRect.Bottom - 2.0f),
                                        new PointF(drawRect.Right - 2.0f, drawRect.Bottom - 1.0f),
                                        new PointF(drawRect.X + 1.0f, drawRect.Bottom - 1.0f),
                                        new PointF(drawRect.X, drawRect.Bottom - 2.0f),
                                        new PointF(drawRect.X, drawRect.Y + 0.75f),
                                        new PointF(drawRect.X + 0.75f, drawRect.Y)});

            return path;
        }

        private void CreatePositionPathsBottom(Rectangle drawRect,
                                               ref GraphicsPath outside,
                                               ref GraphicsPath border,
                                               ref GraphicsPath inside)
        {
            outside = CreatePositionPathsBottom(drawRect);
            drawRect.Inflate(-1, -1);
            border = CreatePositionPathsBottom(drawRect);
            drawRect.Inflate(-2, -2);
            drawRect.Y += 1;
            drawRect.Height -= 1;
            inside = CreatePositionPathsBottom(drawRect);
        }

        private GraphicsPath CreatePositionPathsBottom(Rectangle drawRect)
        {
            float half = (float)drawRect.Width / 2 - 0.5f;

            GraphicsPath path = new GraphicsPath();
            path.AddLines(new PointF[]{ new PointF(drawRect.X + half, drawRect.Y),
                                        new PointF(drawRect.Right - 1.0f, drawRect.Y +  + half),
                                        new PointF(drawRect.Right - 1.0f, drawRect.Bottom - 2.0f),
                                        new PointF(drawRect.Right - 2.0f, drawRect.Bottom - 1.0f),
                                        new PointF(drawRect.X + 1.0f, drawRect.Bottom - 1.0f),
                                        new PointF(drawRect.X, drawRect.Bottom - 2.0f),
                                        new PointF(drawRect.X, drawRect.Y + half),
                                        new PointF(drawRect.X + half, drawRect.Y)});
            return path;
        }

        private void CreatePositionPathsTop(Rectangle drawRect,
                                            ref GraphicsPath outside,
                                            ref GraphicsPath border,
                                            ref GraphicsPath inside)
        {
            outside = CreatePositionPathsTop(drawRect);
            drawRect.Inflate(-1, -1);
            border = CreatePositionPathsTop(drawRect);
            drawRect.Inflate(-2, -2);
            drawRect.Height -= 1;
            inside = CreatePositionPathsTop(drawRect);
        }

        private GraphicsPath CreatePositionPathsTop(Rectangle drawRect)
        {
            float half = (float)drawRect.Width / 2 - 0.5f;

            GraphicsPath path = new GraphicsPath();
            path.AddLines(new PointF[]{ new PointF(drawRect.X + 0.75f, drawRect.Y),
                                        new PointF(drawRect.Right - 1.75f, drawRect.Y),
                                        new PointF(drawRect.Right - 1.0f, drawRect.Y + 0.75f),
                                        new PointF(drawRect.Right - 1.0f, drawRect.Bottom - half - 1.0f),
                                        new PointF(drawRect.X + half, drawRect.Bottom - 1.0f),
                                        new PointF(drawRect.X, drawRect.Bottom - half - 1.0f),
                                        new PointF(drawRect.X, drawRect.Y + 0.75f),
                                        new PointF(drawRect.X + 0.75f, drawRect.Y)});
            return path;
        }

        private void CreatePositionPathsRight(Rectangle drawRect,
                                              ref GraphicsPath outside,
                                              ref GraphicsPath border,
                                              ref GraphicsPath inside)
        {
            outside = CreatePositionPathsRight(drawRect);
            drawRect.Inflate(-1, -1);
            border = CreatePositionPathsRight(drawRect);
            drawRect.Inflate(-2, -2);
            drawRect.Width -= 1;
            inside = CreatePositionPathsRight(drawRect);
        }

        private GraphicsPath CreatePositionPathsRight(Rectangle drawRect)
        {
            float half = (float)drawRect.Height / 2 - 0.5f;

            GraphicsPath path = new GraphicsPath();
            path.AddLines(new PointF[]{ new PointF(drawRect.X + 0.75f, drawRect.Y),
                                        new PointF(drawRect.Right - half - 1.0f, drawRect.Y),
                                        new PointF(drawRect.Right - 1.0f, drawRect.Y + half),
                                        new PointF(drawRect.Right - half - 1.0f, drawRect.Bottom - 1.0f),
                                        new PointF(drawRect.X + 1.0f, drawRect.Bottom - 1.0f),
                                        new PointF(drawRect.X, drawRect.Bottom - 2.0f),
                                        new PointF(drawRect.X, drawRect.Y + 0.75f),
                                        new PointF(drawRect.X + 0.75f, drawRect.Y)});
            return path;
        }

        private void CreatePositionPathsLeft(Rectangle drawRect,
                                              ref GraphicsPath outside,
                                              ref GraphicsPath border,
                                              ref GraphicsPath inside)
        {
            outside = CreatePositionPathsLeft(drawRect);
            drawRect.Inflate(-1, -1);
            border = CreatePositionPathsLeft(drawRect);
            drawRect.Inflate(-2, -2);
            drawRect.X += 1;
            drawRect.Width -= 1;
            inside = CreatePositionPathsLeft(drawRect);
        }

        private GraphicsPath CreatePositionPathsLeft(Rectangle drawRect)
        {
            float half = (float)drawRect.Height / 2 - 0.5f;

            GraphicsPath path = new GraphicsPath();
            path.AddLines(new PointF[]{ new PointF(drawRect.Right - 1.75f, drawRect.Y),
                                        new PointF(drawRect.Right - 1.0f, drawRect.Y + 0.75f),
                                        new PointF(drawRect.Right - 1.0f, drawRect.Bottom - 2.0f),
                                        new PointF(drawRect.Right - 2.0f, drawRect.Bottom - 1.0f),
                                        new PointF(drawRect.X + half, drawRect.Bottom - 1.0f),
                                        new PointF(drawRect.X, drawRect.Bottom - half - 1.0f),
                                        new PointF(drawRect.X + half, drawRect.Y),
                                        new PointF(drawRect.Right - 1.75f, drawRect.Y)});

            return path;
        }
        #endregion

        #region Implementation Back & Border
        private static GraphicsPath CreateBorderBackPath(bool forBorder,
                                                         bool middle,
                                                         Rectangle rect,
                                                         PaletteDrawBorders borders,
                                                         int borderWidth,
                                                         int borderRounding,
                                                         bool smoothing,
                                                         int variant)
        {
            Rectangle origRect = rect;

            GraphicsPath borderPath = new GraphicsPath();

            // A zero size rectangle cannot be drawn, so return a null path
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                // Only use a rounding that will fit inside the rect
                int rounding = Math.Min(borderRounding, Math.Min(rect.Width / 2, rect.Height / 2) - borderWidth);

                // Shrink the rect by half the width of the pen, because the pen will 
                // draw half the distance overlapping each side of the centre anyway.
                // Unless not drawing into the middle in which case give the outside.
                int halfBorderWidthTL = (middle ? borderWidth / 2 : 0);

                // Only adjust the edges that are being drawn
                if (CommonHelper.HasTopBorder(borders))
                {
                    rect.Y += halfBorderWidthTL;
                    rect.Height -= halfBorderWidthTL;
                }

                if (CommonHelper.HasLeftBorder(borders))
                {
                    rect.X += halfBorderWidthTL;
                    rect.Width -= halfBorderWidthTL;
                }

                if (CommonHelper.HasBottomBorder(borders))
                    rect.Height -= halfBorderWidthTL;

                if (CommonHelper.HasRightBorder(borders))
                    rect.Width -= halfBorderWidthTL;

                // Find the width/height of the arc box
                int arcLength = rounding * 2;
                int arcLength1 = arcLength + 1;

                // If drawing all the four borders use a single routine
                if (CommonHelper.HasAllBorders(borders))
                    CreateAllBorderBackPath(middle, borderPath, rect, borderWidth, rounding, forBorder, arcLength, arcLength1);
                else
                {
                    // Are we calculating just the borders to be drawn?
                    if (forBorder)
                    {
                        // Are we calculating for the outside of the border edge? This is used for a KryptonForm
                        // which needs to create a region that is the outside edge of the borders.
                        if (!middle)
                        {
                            // If rounding is used we need to use a path so that corner rounding is honored but
                            // because this is going to be used as a region we need to close the path as well.
                            if (rounding > 0)
                                CreateBorderBackPathOnlyClosed(middle, borders, borderPath, rect, arcLength, variant);
                            else
                            {
                                // Without rounding we just provide the entire area
                                borderPath.AddRectangle(rect);
                            }
                        }
                        else
                        {
                            // We are calculating the middle of the border as the brush will then draw the entire
                            // border from the middle outwards.

                            if (rounding > 0)
                                CreateBorderBackPathOnly(middle, borders, borderPath, rect, arcLength, variant);
                            else
                                CreateBorderBackPathOnly(borders, borderPath, rect, variant);
                        }
                    }
                    else
                    {
                        // Calculating a complete path for the entire area and not just the specified borders
                        // If there is rounding we need to calculate a path that honors the rounding at corners
                        if (rounding > 0)
                            CreateBorderBackPathComplete(middle, borders, borderPath, rect, arcLength);
                        else
                        {
                            // Without rounding the complete path is always just the entire area
                            borderPath.AddRectangle(rect);
                        }
                    }
                }
            }

            return borderPath;
        }

        private static void CreateAllBorderBackPath(bool middle,
                                                    GraphicsPath borderPath,
                                                    Rectangle rect,
                                                    int width,
                                                    int rounding,
                                                    bool forBorder,
                                                    int arcLength,
                                                    int arcLength1)
        {
            // If there is no room for any rounding effect...
            if (rounding <= 0)
            {
                // If the width is an odd number then need to reduce by 1 in each dimension
                if (forBorder && middle && ((width % 2) == 1))
                {
                    rect.Width -= 1;
                    rect.Height -= 1;
                }

                // Just add a simple rectangle as a quick way of adding four lines
                borderPath.AddRectangle(rect);
            }
            else
            {
                // We create the path using a floating point rectangle
                RectangleF rectF = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

                // If trying to get the outside edge then perform some offsetting so that
                // when converted to a region it draws nicely inside the path outline
                if (!middle && ((width % 2) == 1))
                {
                    rectF.X -= 0.25f;
                    rectF.Y -= 0.25f;
                    rectF.Width += 0.75f;
                    rectF.Height += 0.75f;
                }

                // The border is made of up a quarter of a circle arc, in each corner
                borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                borderPath.AddArc(rectF.Right - arcLength1, rectF.Top, arcLength, arcLength, 270f, 90f);
                borderPath.AddArc(rectF.Right - arcLength1, rectF.Bottom - arcLength1, arcLength, arcLength, 0f, 90f);
                borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength1, arcLength, arcLength, 90f, 90f);

                // Make the last and first arc join up
                borderPath.CloseFigure();
            }
        }

        private static void CreateBorderBackPathOnly(PaletteDrawBorders borders,
                                                     GraphicsPath borderPath,
                                                     Rectangle rect,
                                                     int variant)
        {
            // Reduce the width and height by 1 pixel for drawing into rectangle
            rect.Width -= 1;
            rect.Height -= 1;

            // Add only the border for drawing
            switch (borders)
            {
                case PaletteDrawBorders.None:
                    break;
                case PaletteDrawBorders.Top:
                    borderPath.AddLine(rect.Left - 1, rect.Top, rect.Right + 1, rect.Top);
                    break;
                case PaletteDrawBorders.Bottom:
                    borderPath.AddLine(rect.Left - 1, rect.Bottom, rect.Right + 1, rect.Bottom);
                    break;
                case PaletteDrawBorders.Left:
                    borderPath.AddLine(rect.Left, rect.Top - 1, rect.Left, rect.Bottom + 1);
                    break;
                case PaletteDrawBorders.Right:
                    borderPath.AddLine(rect.Right, rect.Top - 1, rect.Right, rect.Bottom + 1);
                    break;
                case PaletteDrawBorders.TopBottom:
                    if (variant == 0)
                        borderPath.AddLine(rect.Left - 1, rect.Top, rect.Right + 1, rect.Top);
                    else
                        borderPath.AddLine(rect.Left - 1, rect.Bottom, rect.Right + 1, rect.Bottom);
                    break;
                case PaletteDrawBorders.LeftRight:
                    if (variant == 0)
                        borderPath.AddLine(rect.Left, rect.Top - 1, rect.Left, rect.Bottom + 1);
                    else
                        borderPath.AddLine(rect.Right, rect.Top - 1, rect.Right, rect.Bottom + 1);
                    break;
                case PaletteDrawBorders.TopLeft:
                    borderPath.AddLine(rect.Left, rect.Bottom + 1, rect.Left, rect.Top);
                    borderPath.AddLine(rect.Left, rect.Top, rect.Right + 1, rect.Top);
                    break;
                case PaletteDrawBorders.TopRight:
                    borderPath.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);
                    borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom + 1);
                    break;
                case PaletteDrawBorders.BottomRight:
                    borderPath.AddLine(rect.Right, rect.Top - 1, rect.Right, rect.Bottom);
                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Left - 1, rect.Bottom);
                    break;
                case PaletteDrawBorders.BottomLeft:
                    borderPath.AddLine(rect.Right + 1, rect.Bottom, rect.Left, rect.Bottom);
                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top - 1);
                    break;
                case PaletteDrawBorders.TopBottomLeft:
                    borderPath.AddLine(rect.Right + 1, rect.Bottom, rect.Left, rect.Bottom);
                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top);
                    borderPath.AddLine(rect.Left, rect.Top, rect.Right + 1, rect.Top);
                    break;
                case PaletteDrawBorders.TopBottomRight:
                    borderPath.AddLine(rect.Left - 1, rect.Top, rect.Right, rect.Top);
                    borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);
                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Left - 1, rect.Bottom);
                    break;
                case PaletteDrawBorders.TopLeftRight:
                    borderPath.AddLine(rect.Left, rect.Bottom + 1, rect.Left, rect.Top);
                    borderPath.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);
                    borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom + 1);
                    break;
                case PaletteDrawBorders.BottomLeftRight:
                    borderPath.AddLine(rect.Right, rect.Top - 1, rect.Right, rect.Bottom);
                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);
                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top - 1);
                    break;
            }
        }

        private static void CreateBorderBackPathOnly(bool middle,
                                                     PaletteDrawBorders borders,
                                                     GraphicsPath borderPath,
                                                     Rectangle rect,
                                                     int arcLength,
                                                     int variant)
        {
            // Reduce the width and height by 1 pixel for drawing into rectangle
            rect.Width -= 1;
            rect.Height -= 1;

            // We create the path using a floating point rectangle
            RectangleF rectF = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

            // If trying to get the outside edge then perform some offsetting so that
            // when converted to a region it draws nicely inside the path outline
            if (!middle)
            {
                rectF.X -= 0.25f;
                rectF.Y -= 0.25f;
                rectF.Width += 0.75f;
                rectF.Height += 0.75f;
            }

            // Add only the border for drawing
            switch (borders)
            {
                case PaletteDrawBorders.None:
                    break;
                case PaletteDrawBorders.Top:
                    borderPath.AddLine(rect.Left - 1, rect.Top, rect.Right + 1, rect.Top);
                    break;
                case PaletteDrawBorders.Bottom:
                    borderPath.AddLine(rect.Left - 1, rect.Bottom, rect.Right + 1, rect.Bottom);
                    break;
                case PaletteDrawBorders.Left:
                    borderPath.AddLine(rect.Left, rect.Top - 1, rect.Left, rect.Bottom + 1);
                    break;
                case PaletteDrawBorders.Right:
                    borderPath.AddLine(rect.Right, rect.Top - 1, rect.Right, rect.Bottom + 1);
                    break;
                case PaletteDrawBorders.TopBottom:
                    if (variant == 0)
                        borderPath.AddLine(rect.Left - 1, rect.Top, rect.Right + 1, rect.Top);
                    else
                        borderPath.AddLine(rect.Left - 1, rect.Bottom, rect.Right + 1, rect.Bottom);
                    break;
                case PaletteDrawBorders.LeftRight:
                    if (variant == 0)
                        borderPath.AddLine(rect.Left, rect.Top - 1, rect.Left, rect.Bottom + 1);
                    else
                        borderPath.AddLine(rect.Right, rect.Top - 1, rect.Right, rect.Bottom + 1);
                    break;
                case PaletteDrawBorders.TopLeft:
                    borderPath.AddLine(rectF.Left, rectF.Bottom + 1, rectF.Left, rectF.Top + arcLength);
                    borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                    borderPath.AddLine(rectF.Left + arcLength, rectF.Top, rectF.Right + 1, rectF.Top);
                    break;
                case PaletteDrawBorders.TopRight:
                    borderPath.AddLine(rectF.Left - 1, rectF.Top, rectF.Right - arcLength, rectF.Top);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, -90f, 90f);
                    borderPath.AddLine(rectF.Right, rectF.Top + arcLength, rectF.Right, rectF.Bottom + 1);
                    break;
                case PaletteDrawBorders.BottomRight:
                    borderPath.AddLine(rectF.Right, rectF.Top - 1, rectF.Right, rectF.Bottom - arcLength);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                    borderPath.AddLine(rectF.Right - arcLength, rectF.Bottom, rectF.Left - 1, rectF.Bottom);
                    break;
                case PaletteDrawBorders.BottomLeft:
                    borderPath.AddLine(rectF.Right + 1, rectF.Bottom, rectF.Left + arcLength, rectF.Bottom);
                    borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);
                    borderPath.AddLine(rectF.Left, rectF.Bottom - arcLength, rectF.Left, rectF.Top - 1);
                    break;
                case PaletteDrawBorders.TopBottomLeft:
                    borderPath.AddLine(rectF.Right + 1, rectF.Bottom, rectF.Left + arcLength, rectF.Bottom);
                    borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);
                    borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                    borderPath.AddLine(rectF.Left + arcLength, rectF.Top, rectF.Right + 1, rectF.Top);
                    break;
                case PaletteDrawBorders.TopBottomRight:
                    borderPath.AddLine(rectF.Left - 1, rectF.Top, rectF.Right - arcLength, rectF.Top);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, -90f, 90f);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                    borderPath.AddLine(rectF.Right - arcLength, rectF.Bottom, rectF.Left - 1, rectF.Bottom);
                    break;
                case PaletteDrawBorders.TopLeftRight:
                    borderPath.AddLine(rectF.Left, rectF.Bottom + 1, rectF.Left, rectF.Top + arcLength);
                    borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, -90f, 90f);
                    borderPath.AddLine(rectF.Right, rectF.Top + arcLength, rectF.Right, rectF.Bottom + 1);
                    break;
                case PaletteDrawBorders.BottomLeftRight:
                    borderPath.AddLine(rectF.Right, rectF.Top - 1, rectF.Right, rectF.Bottom - arcLength);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                    borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);
                    borderPath.AddLine(rectF.Left, rectF.Bottom - arcLength, rectF.Left, rectF.Top - 1);
                    break;
            }
        }

        private static void CreateBorderBackPathOnlyClosed(bool middle,
                                                           PaletteDrawBorders borders,
                                                           GraphicsPath borderPath,
                                                           Rectangle rect,
                                                           int arcLength,
                                                           int variant)
        {
            // Reduce the width and height by 1 pixel for drawing into rectangle
            rect.Width--;
            rect.Height--;

            // We create the path using a floating point rectangle
            RectangleF rectF = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

            // If trying to get the outside edge then perform some offsetting so that
            // when converted to a region it draws nicely inside the path outline
            if (!middle)
            {
                rectF.X -= 0.25f;
                rectF.Y -= 0.25f;
                rectF.Width += 0.75f;
                rectF.Height += 0.75f;
            }

            // Add only the border for drawing
            switch (borders)
            {
                case PaletteDrawBorders.None:
                    break;
                case PaletteDrawBorders.Top:
                case PaletteDrawBorders.Bottom:
                case PaletteDrawBorders.Left:
                case PaletteDrawBorders.Right:
                case PaletteDrawBorders.TopBottom:
                case PaletteDrawBorders.LeftRight:
                    // When using the entire rectangle we do not need to adjust its size
                    rect.Width ++;
                    rect.Height++;
                    borderPath.AddRectangle(rect);
                    break;
                case PaletteDrawBorders.TopLeft:
                    borderPath.AddLine(rectF.Left, rectF.Bottom + 1, rectF.Left, rectF.Top + arcLength);
                    borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                    borderPath.AddLine(rectF.Left + arcLength, rectF.Top, rectF.Right + 1, rectF.Top);
                    borderPath.AddLine(rectF.Right, rectF.Top, rectF.Right, rectF.Bottom);
                    break;
                case PaletteDrawBorders.TopRight:
                    borderPath.AddLine(rectF.Left - 1, rectF.Top, rectF.Right - arcLength, rectF.Top);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, -90f, 90f);
                    borderPath.AddLine(rectF.Right, rectF.Top + arcLength, rectF.Right, rectF.Bottom + 1);
                    borderPath.AddLine(rectF.Right, rectF.Bottom, rectF.Left, rectF.Bottom);
                    break;
                case PaletteDrawBorders.BottomRight:
                    borderPath.AddLine(rectF.Right, rectF.Top - 1, rectF.Right, rectF.Bottom - arcLength);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                    borderPath.AddLine(rectF.Right - arcLength, rectF.Bottom, rectF.Left - 1, rectF.Bottom);
                    borderPath.AddLine(rectF.Left, rectF.Bottom, rectF.Left, rectF.Top);
                    break;
                case PaletteDrawBorders.BottomLeft:
                    borderPath.AddLine(rectF.Right + 1, rectF.Bottom, rectF.Left + arcLength, rectF.Bottom);
                    borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);
                    borderPath.AddLine(rectF.Left, rectF.Bottom - arcLength, rectF.Left, rectF.Top - 1);
                    borderPath.AddLine(rectF.Left, rectF.Top, rectF.Right, rectF.Top);
                    break;
                case PaletteDrawBorders.TopBottomLeft:
                    borderPath.AddLine(rectF.Right + 1, rectF.Bottom, rectF.Left + arcLength, rectF.Bottom);
                    borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);
                    borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                    borderPath.AddLine(rectF.Left + arcLength, rectF.Top, rectF.Right + 1, rectF.Top);
                    break;
                case PaletteDrawBorders.TopBottomRight:
                    borderPath.AddLine(rectF.Left - 1, rectF.Top, rectF.Right - arcLength, rectF.Top);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, -90f, 90f);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                    borderPath.AddLine(rectF.Right - arcLength, rectF.Bottom, rectF.Left - 1, rectF.Bottom);
                    break;
                case PaletteDrawBorders.TopLeftRight:
                    borderPath.AddLine(rectF.Left, rectF.Bottom + 1, rectF.Left, rectF.Top + arcLength);
                    borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, -90f, 90f);
                    borderPath.AddLine(rectF.Right, rectF.Top + arcLength, rectF.Right, rectF.Bottom + 1);
                    break;
                case PaletteDrawBorders.BottomLeftRight:
                    borderPath.AddLine(rectF.Right, rectF.Top - 1, rectF.Right, rectF.Bottom - arcLength);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                    borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);
                    borderPath.AddLine(rectF.Left, rectF.Bottom - arcLength, rectF.Left, rectF.Top - 1);
                    break;
            }
        }

        private static void CreateBorderBackPathComplete(bool middle,
                                                         PaletteDrawBorders borders,
                                                         GraphicsPath borderPath,
                                                         Rectangle rect,
                                                         int arcLength)
        {
            // We create the path using a floating point rectangle
            RectangleF rectF = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

            // If trying to get the outside edge then perform some offsetting so that
            // when converted to a region it draws nicely inside the path outline
            if (!middle)
            {
                rectF.X -= 0.25f;
                rectF.Y -= 0.25f;
                rectF.Width += 0.75f;
                rectF.Height += 0.75f;
            }
            // Define area that covers the border and the inside
            switch (borders)
            {
                case PaletteDrawBorders.None:
                case PaletteDrawBorders.Top:
                case PaletteDrawBorders.Bottom:
                case PaletteDrawBorders.Left:
                case PaletteDrawBorders.Right:
                case PaletteDrawBorders.TopBottom:
                case PaletteDrawBorders.LeftRight:
                    // Just add a simple rectangle as a quick way of adding four lines
                    borderPath.AddRectangle(rect);
                    break;
                case PaletteDrawBorders.TopLeft:
                    borderPath.AddLine(rectF.Left, rectF.Bottom, rectF.Left, rectF.Top + arcLength);
                    borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                    borderPath.AddLine(rectF.Left + arcLength, rectF.Top, rectF.Right, rectF.Top);
                    borderPath.AddLine(rectF.Right, rectF.Top, rectF.Right, rectF.Bottom);
                    borderPath.AddLine(rectF.Right, rectF.Bottom, rectF.Left, rectF.Bottom);
                    break;
                case PaletteDrawBorders.TopRight:
                    borderPath.AddLine(rectF.Left, rectF.Top, rectF.Right - arcLength, rectF.Top);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, -90f, 90f);
                    borderPath.AddLine(rectF.Right, rectF.Top + arcLength, rectF.Right, rectF.Bottom);
                    borderPath.AddLine(rectF.Right, rectF.Bottom, rectF.Left, rectF.Bottom);
                    borderPath.AddLine(rectF.Left, rectF.Bottom, rectF.Left, rectF.Top);
                    break;
                case PaletteDrawBorders.BottomRight:
                    // Reduce the width and height by 1 pixel for drawing into rectFangle
                    rectF.Width -= 1;
                    rectF.Height -= 1;

                    borderPath.AddLine(rectF.Right, rectF.Top, rectF.Right, rectF.Bottom - arcLength);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                    borderPath.AddLine(rectF.Right - arcLength, rectF.Bottom, rectF.Left, rectF.Bottom);
                    borderPath.AddLine(rectF.Left, rectF.Bottom, rectF.Left, rectF.Top);
                    borderPath.AddLine(rectF.Left, rectF.Top, rectF.Right, rectF.Top);
                    break;
                case PaletteDrawBorders.BottomLeft:
                    // Reduce the width and height by 1 pixel for drawing into rectangle
                    rectF.X++;
                    rectF.Width -= 1;
                    rectF.Height -= 1;

                    borderPath.AddLine(rectF.Right, rectF.Bottom, rectF.Left + arcLength, rectF.Bottom);
                    borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);
                    borderPath.AddLine(rectF.Left, rectF.Bottom - arcLength, rectF.Left, rectF.Top);
                    borderPath.AddLine(rectF.Left, rectF.Top, rectF.Right, rectF.Top);
                    borderPath.AddLine(rectF.Right, rectF.Top, rectF.Right, rectF.Bottom);
                    break;
                case PaletteDrawBorders.TopBottomLeft:
                    borderPath.AddLine(rectF.Right, rectF.Bottom, rectF.Left + arcLength, rectF.Bottom);
                    borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);
                    borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                    borderPath.AddLine(rectF.Left + arcLength, rectF.Top, rectF.Right, rectF.Top);
                    borderPath.AddLine(rectF.Right, rectF.Top, rectF.Right, rectF.Bottom);
                    break;
                case PaletteDrawBorders.TopBottomRight:
                    // Reduce the width and height by 1 pixel for drawing into rectangle
                    rectF.Width -= 1;
                    rectF.Height -= 1;

                    borderPath.AddLine(rectF.Left, rectF.Top, rectF.Right - arcLength, rectF.Top);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, -90f, 90f);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                    borderPath.AddLine(rectF.Right - arcLength, rectF.Bottom, rectF.Left, rectF.Bottom);
                    borderPath.AddLine(rectF.Left, rectF.Bottom, rectF.Left, rectF.Top);
                    break;
                case PaletteDrawBorders.TopLeftRight:
                    borderPath.AddLine(rectF.Left, rectF.Bottom, rectF.Left, rectF.Top + arcLength);
                    borderPath.AddArc(rectF.Left, rectF.Top, arcLength, arcLength, 180f, 90f);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Top, arcLength, arcLength, -90f, 90f);
                    borderPath.AddLine(rectF.Right, rectF.Top + arcLength, rectF.Right, rectF.Bottom);
                    borderPath.AddLine(rectF.Right, rectF.Bottom, rectF.Left, rectF.Bottom);
                    break;
                case PaletteDrawBorders.BottomLeftRight:
                    // Reduce the width and height by 1 pixel for drawing into rectangle
                    rectF.X++;
                    rectF.Width -= 1;
                    rectF.Height -= 1;

                    borderPath.AddLine(rectF.Right, rectF.Top, rectF.Right, rectF.Bottom - arcLength);
                    borderPath.AddArc(rectF.Right - arcLength, rectF.Bottom - arcLength, arcLength, arcLength, 0f, 90f);
                    borderPath.AddArc(rectF.Left, rectF.Bottom - arcLength, arcLength, arcLength, 90f, 90f);
                    borderPath.AddLine(rectF.Left, rectF.Top, rectF.Right, rectF.Top);
                    break;
            }
        }

        private static GraphicsPath CreateTabBorderBackPath(RightToLeft rtl,
                                                            PaletteState state,
                                                            bool forBorder,
                                                            Rectangle rect,
                                                            int borderWidth,
                                                            TabBorderStyle tabBorderStyle,
                                                            VisualOrientation orientation,
                                                            bool smoothing)
        {
            GraphicsPath borderPath = new GraphicsPath();

            // A zero size rectangle cannot be drawn, so return a null path
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                // Shrink the rect by half the width of the pen, because the pen will 
                // draw half the distance overlapping each side of the centre anyway.
                int halfBorderWidth = borderWidth / 2;

                // Adjust rectangle for all except the bottom edges
                rect.Width -= (halfBorderWidth * 2);
                rect.Height -= halfBorderWidth;
                rect.X += halfBorderWidth;
                rect.Y += halfBorderWidth;

                // Populate the graphics path for the border style
                CreateTabBorderPath((rtl == RightToLeft.Yes), state, forBorder, 
                                    borderPath, rect, tabBorderStyle, orientation);
            }

            return borderPath;
        }

        private static void CreateTabBorderPath(bool rtl,
                                                PaletteState state,
                                                bool forBorder,
                                                GraphicsPath borderPath,
                                                Rectangle rect,
                                                TabBorderStyle tabBorderStyle,
                                                VisualOrientation orientation)
        {
            // Correct drawing by reducing drawing rectangle
            switch (orientation)
            {
                case VisualOrientation.Top:
                    rect.Width--;
                    break;
                case VisualOrientation.Bottom:
                    rect.Y--;
                    rect.Width--;
                    break;
                case VisualOrientation.Left:
                    rect.Height--;
                    break;
                case VisualOrientation.Right:
                    rect.X--;
                    rect.Height--;
                    break;
            }

            // Add only the border for drawing
            switch (tabBorderStyle)
            {
                case TabBorderStyle.DockEqual:
                case TabBorderStyle.SquareEqualSmall:
                case TabBorderStyle.SquareEqualMedium:
                case TabBorderStyle.SquareEqualLarge:
                    AddSquarePath(borderPath, orientation, rect, forBorder);
                    break;
                case TabBorderStyle.DockOutsize:
                case TabBorderStyle.SquareOutsizeSmall:
                case TabBorderStyle.SquareOutsizeMedium:
                case TabBorderStyle.SquareOutsizeLarge:
                    rect = AdjustOutsizeTab(state, rect, orientation);
                    AddSquarePath(borderPath, orientation, rect, forBorder);
                    break;
                case TabBorderStyle.RoundedEqualSmall:
                case TabBorderStyle.RoundedEqualMedium:
                case TabBorderStyle.RoundedEqualLarge:
                    AddRoundedPath(borderPath, orientation, rect, forBorder);
                    break;
                case TabBorderStyle.RoundedOutsizeSmall:
                case TabBorderStyle.RoundedOutsizeMedium:
                case TabBorderStyle.RoundedOutsizeLarge:
                    rect = AdjustOutsizeTab(state, rect, orientation);
                    AddRoundedPath(borderPath, orientation, rect, forBorder);
                    break;
                case TabBorderStyle.SlantEqualNear:
                case TabBorderStyle.SlantOutsizeNear:
                    if (tabBorderStyle == TabBorderStyle.SlantOutsizeNear)
                        rect = AdjustOutsizeTab(state, rect, orientation);

                    if (rtl && ((orientation == VisualOrientation.Top) || (orientation == VisualOrientation.Bottom)))
                        AddSlantFarPath(borderPath, orientation, rect, forBorder);
                    else
                        AddSlantNearPath(borderPath, orientation, rect, forBorder);
                    break;
                case TabBorderStyle.SlantEqualFar:
                case TabBorderStyle.SlantOutsizeFar:
                    if (tabBorderStyle == TabBorderStyle.SlantOutsizeFar)
                        rect = AdjustOutsizeTab(state, rect, orientation);

                    if (rtl && ((orientation == VisualOrientation.Top) || (orientation == VisualOrientation.Bottom)))
                        AddSlantNearPath(borderPath, orientation, rect, forBorder);
                    else
                        AddSlantFarPath(borderPath, orientation, rect, forBorder);
                    break;
                case TabBorderStyle.SlantEqualBoth:
                case TabBorderStyle.SlantOutsizeBoth:
                    if (tabBorderStyle == TabBorderStyle.SlantOutsizeBoth)
                        rect = AdjustOutsizeTab(state, rect, orientation);

                    AddSlantBothPath(borderPath, orientation, rect, forBorder);
                    break;
                case TabBorderStyle.OneNote:
                    // Is the current tab selected?
                    bool selected = (state == PaletteState.CheckedNormal) ||
                                    (state == PaletteState.CheckedPressed) ||
                                    (state == PaletteState.CheckedTracking);

                    // The right padding depends on the selected state
                    int rp = (selected ? _spacingTabOneNoteRPS : _spacingTabOneNoteRPI);

                    // If not selected then need to make the tab shorter
                    if (!selected)
                        rect = AdjustOneNoteTab(rect, orientation);

                    if (rtl && ((orientation == VisualOrientation.Top) || (orientation == VisualOrientation.Bottom)))
                        AddOneNoteReversePath(borderPath, orientation, rect, forBorder, rp);
                    else
                        AddOneNotePath(borderPath, orientation, rect, forBorder, rp);
                    break;
                case TabBorderStyle.SmoothEqual:
                case TabBorderStyle.SmoothOutsize:
                    // Adjust the outsize tab variant
                    if (tabBorderStyle == TabBorderStyle.SmoothOutsize)
                        rect = AdjustSmoothTab(state, rect, orientation);

                    AddSmoothPath(borderPath, orientation, rect, forBorder);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }
        }

        private static Rectangle AdjustOutsizeTab(PaletteState state,
                                                  Rectangle rect,
                                                  VisualOrientation orientation)
        {
            // We alter the size of the tab drawn depending on the state
            switch (state)
            {
                case PaletteState.CheckedNormal:
                case PaletteState.CheckedPressed:
                case PaletteState.CheckedTracking:
                    break;
                default:
                    // Reduce the tab except on the connected edge
                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            rect.Height -= 2;
                            rect.Width -= 4;
                            rect.X += 2;
                            rect.Y += 2;
                            break;
                        case VisualOrientation.Bottom:
                            rect.Height -= 2;
                            rect.Width -= 4;
                            rect.X += 2;
                            break;
                        case VisualOrientation.Left:
                            rect.Height -= 4;
                            rect.Width -= 2;
                            rect.X += 2;
                            rect.Y += 2;
                            break;
                        case VisualOrientation.Right:
                            rect.Height -= 4;
                            rect.Width -= 2;
                            rect.Y += 2;
                            break;
                    }
                    break;
            }

            return rect;
        }

        private static Rectangle AdjustOneNoteTab(Rectangle rect,
                                                  VisualOrientation orientation)
        {
            // Reduce the height of the tab
            switch (orientation)
            {
                case VisualOrientation.Top:
                    rect.Height -= _spacingTabOneNoteTPI;
                    rect.Y += _spacingTabOneNoteTPI;
                    break;
                case VisualOrientation.Bottom:
                    rect.Height -= _spacingTabOneNoteTPI;
                    break;
                case VisualOrientation.Left:
                    rect.Width -= _spacingTabOneNoteTPI;
                    rect.X += _spacingTabOneNoteTPI;
                    break;
                case VisualOrientation.Right:
                    rect.Width -= _spacingTabOneNoteTPI;
                    break;
            }

            return rect;
        }

        private static Rectangle AdjustSmoothTab(PaletteState state,
                                                 Rectangle rect,
                                                 VisualOrientation orientation)
        {
            // We alter the size of the tab drawn depending on the state
            switch (state)
            {
                case PaletteState.CheckedNormal:
                case PaletteState.CheckedPressed:
                case PaletteState.CheckedTracking:
                    break;
                default:
                    // Reduce the tab except on the connected edge
                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            rect.Height -= 4;
                            rect.Width -= 8;
                            rect.X += 4;
                            rect.Y += 4;
                            break;
                        case VisualOrientation.Bottom:
                            rect.Height -= 4;
                            rect.Width -= 8;
                            rect.X += 4;
                            break;
                        case VisualOrientation.Left:
                            rect.Height -= 8;
                            rect.Width -= 4;
                            rect.X += 4;
                            rect.Y += 4;
                            break;
                        case VisualOrientation.Right:
                            rect.Height -= 8;
                            rect.Width -= 4;
                            rect.Y += 4;
                            break;
                    }
                    break;
            }

            return rect;
        }

        private static void AddSquarePath(GraphicsPath borderPath,
                                          VisualOrientation orientation,
                                          Rectangle rect,
                                          bool forBorder)
        {
            switch (orientation)
            {
                case VisualOrientation.Top:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top);
                    borderPath.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);
                    borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);
                    break;
                case VisualOrientation.Bottom:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);

                    borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom);
                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Right, rect.Bottom);
                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Top);
                    break;
                case VisualOrientation.Left:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);

                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);
                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top);
                    borderPath.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);
                    break;
                case VisualOrientation.Right:
                    if (!forBorder)
                        borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Right, rect.Bottom);
                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Top);
                    borderPath.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);
                    break;
            }
        }

        private static void AddRoundedPath(GraphicsPath borderPath,
                                           VisualOrientation orientation,
                                           Rectangle rect, 
                                           bool forBorder)
        {
            // Cache the distance to make the corner rounded
            int x = _spacingTabRoundedCorner;

            switch (orientation)
            {
                case VisualOrientation.Top:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + x);
                    borderPath.AddLine(rect.Left, rect.Top + x, rect.Left + x, rect.Top);
                    borderPath.AddLine(rect.Left + x, rect.Top, rect.Right - x, rect.Top);
                    borderPath.AddLine(rect.Right - x, rect.Top, rect.Right, rect.Top + x);
                    borderPath.AddLine(rect.Right, rect.Top + x, rect.Right, rect.Bottom);
                    break;
                case VisualOrientation.Bottom:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);

                    borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom - x);
                    borderPath.AddLine(rect.Left, rect.Bottom - x, rect.Left + x, rect.Bottom);
                    borderPath.AddLine(rect.Left + x, rect.Bottom, rect.Right - x, rect.Bottom);
                    borderPath.AddLine(rect.Right - x, rect.Bottom, rect.Right, rect.Bottom - x);
                    borderPath.AddLine(rect.Right, rect.Bottom - x, rect.Right, rect.Top);
                    break;
                case VisualOrientation.Left:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);

                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Left + x, rect.Bottom);
                    borderPath.AddLine(rect.Left + x, rect.Bottom, rect.Left, rect.Bottom - x);
                    borderPath.AddLine(rect.Left, rect.Bottom - x, rect.Left, rect.Top + x);
                    borderPath.AddLine(rect.Left, rect.Top + x, rect.Left + x, rect.Top);
                    borderPath.AddLine(rect.Left + x, rect.Top, rect.Right, rect.Top);
                    break;
                case VisualOrientation.Right:
                    if (!forBorder)
                        borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Right - x, rect.Bottom);
                    borderPath.AddLine(rect.Right - x, rect.Bottom, rect.Right, rect.Bottom - x);
                    borderPath.AddLine(rect.Right, rect.Bottom - x, rect.Right, rect.Top + x);
                    borderPath.AddLine(rect.Right, rect.Top + x, rect.Right - x, rect.Top);
                    borderPath.AddLine(rect.Right - x, rect.Top, rect.Left, rect.Top);
                    break;
            }
        }

        private static void AddSlantNearPath(GraphicsPath borderPath,
                                             VisualOrientation orientation,
                                             Rectangle rect,
                                             bool forBorder)
        {
            // Cache the distance to use for the slant
            int x = _spacingTabSlantPadding;
            int xW = Math.Min(x, rect.Width);
            int xH = Math.Min(x, rect.Height);

            switch (orientation)
            {
                case VisualOrientation.Top:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left - 1, rect.Bottom, rect.Left + xW, rect.Top);
                    borderPath.AddLine(rect.Left + xW, rect.Top, rect.Right, rect.Top);
                    borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);
                    break;
                case VisualOrientation.Bottom:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);

                    borderPath.AddLine(rect.Left - 1, rect.Top, rect.Left + xW, rect.Bottom);
                    borderPath.AddLine(rect.Left + xW, rect.Bottom, rect.Right, rect.Bottom);
                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Top);
                    break;
                case VisualOrientation.Left:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);

                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom - xH);
                    borderPath.AddLine(rect.Left, rect.Bottom - xH, rect.Left, rect.Top);
                    borderPath.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);
                    break;
                case VisualOrientation.Right:
                    if (!forBorder)
                        borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Right, rect.Bottom - xH);
                    borderPath.AddLine(rect.Right, rect.Bottom - xH, rect.Right, rect.Top);
                    borderPath.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);
                    break;
            }
        }

        private static void AddSlantFarPath(GraphicsPath borderPath,
                                            VisualOrientation orientation,
                                            Rectangle rect,
                                            bool forBorder)
        {
            // Cache the distance to use for the slant
            int x = _spacingTabSlantPadding;
            int xW = Math.Min(x, rect.Width);
            int xH = Math.Min(x, rect.Height);

            switch (orientation)
            {
                case VisualOrientation.Top:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top);
                    borderPath.AddLine(rect.Left, rect.Top, rect.Right - xW, rect.Top);
                    borderPath.AddLine(rect.Right - xW, rect.Top, rect.Right, rect.Bottom);
                    break;
                case VisualOrientation.Bottom:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);

                    borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom);
                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Right - xW, rect.Bottom);
                    borderPath.AddLine(rect.Right - xW, rect.Bottom, rect.Right, rect.Top);
                    break;
                case VisualOrientation.Left:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);

                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);
                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + xH);
                    borderPath.AddLine(rect.Left, rect.Top + xH, rect.Right, rect.Top - 1);
                    break;
                case VisualOrientation.Right:
                    if (!forBorder)
                        borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Right, rect.Bottom);
                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Top + xH);
                    borderPath.AddLine(rect.Right, rect.Top + xH, rect.Left, rect.Top - 1);
                    break;
            }
        }

        private static void AddSlantBothPath(GraphicsPath borderPath,
                                            VisualOrientation orientation,
                                            Rectangle rect,
                                            bool forBorder)
        {
            // Cache the distance to use for the slant
            int x = _spacingTabSlantPadding;
            int xW = Math.Min(x, rect.Width / 2);
            int xH = Math.Min(x, rect.Height / 2);

            switch (orientation)
            {
                case VisualOrientation.Top:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left - 1, rect.Bottom, rect.Left + xW, rect.Top);
                    borderPath.AddLine(rect.Left + xW, rect.Top, rect.Right - xW, rect.Top);
                    borderPath.AddLine(rect.Right - xW, rect.Top, rect.Right, rect.Bottom);
                    break;
                case VisualOrientation.Bottom:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);

                    borderPath.AddLine(rect.Left - 1, rect.Top, rect.Left + xW, rect.Bottom);
                    borderPath.AddLine(rect.Left + xW, rect.Bottom, rect.Right - xW, rect.Bottom);
                    borderPath.AddLine(rect.Right - xW, rect.Bottom, rect.Right, rect.Top);
                    break;
                case VisualOrientation.Left:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);

                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom - xH);
                    borderPath.AddLine(rect.Left, rect.Bottom - xH, rect.Left, rect.Top + xH);
                    borderPath.AddLine(rect.Left, rect.Top + xH, rect.Right, rect.Top - 1);
                    break;
                case VisualOrientation.Right:
                    if (!forBorder)
                        borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Right, rect.Bottom - xH);
                    borderPath.AddLine(rect.Right, rect.Bottom - xH, rect.Right, rect.Top + xH);
                    borderPath.AddLine(rect.Right, rect.Top + xH, rect.Left, rect.Top - 1);
                    break;
            }
        }

        private static void AddOneNotePath(GraphicsPath borderPath,
                                           VisualOrientation orientation,
                                           Rectangle rect,
                                           bool forBorder,
                                           int rp)
        {
            int x = Math.Min(Math.Min(9, rect.Width / 2), rect.Height / 2);
            int rpW = Math.Min(rp, rect.Width / 2);
            int rpH = Math.Min(rp, rect.Height / 2);

            switch (orientation)
            {
                case VisualOrientation.Top:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);

                    borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Bottom - 1);
                    borderPath.AddArc(rect.Left, rect.Top, x, x, 180f, 90f);
                    borderPath.AddLine(rect.Right - rpW, rect.Top, rect.Right, rect.Bottom);
                    break;
                case VisualOrientation.Bottom:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);

                    borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Top + 1);
                    borderPath.AddArc(rect.Left, rect.Bottom - x, x, x, 180f, -90f);
                    borderPath.AddLine(rect.Right - rpW, rect.Bottom, rect.Right, rect.Top);
                    break;
                case VisualOrientation.Left:
                    if (!forBorder)
                        borderPath.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Top);

                    borderPath.AddLine(rect.Right, rect.Top, rect.Right - 1, rect.Top);
                    borderPath.AddArc(rect.Left, rect.Top, x, x, -90f, -90f);
                    borderPath.AddLine(rect.Left, rect.Bottom - rpH, rect.Right, rect.Bottom);
                    break;
                case VisualOrientation.Right:
                    if (!forBorder)
                        borderPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top);

                    borderPath.AddLine(rect.Left, rect.Top, rect.Left + 1, rect.Top);
                    borderPath.AddArc(rect.Right - x, rect.Top, x, x, -90f, 90f);
                    borderPath.AddLine(rect.Right, rect.Bottom - rpH, rect.Left, rect.Bottom);
                    break;
            }
        }

        private static void AddOneNoteReversePath(GraphicsPath borderPath,
                                                  VisualOrientation orientation,
                                                  Rectangle rect,
                                                  bool forBorder,
                                                  int rp)
        {
            int x = Math.Min(Math.Min(9, rect.Width / 2), rect.Height / 2);
            int rpW = Math.Min(rp, rect.Width / 2);

            switch (orientation)
            {
                case VisualOrientation.Top:
                    if (!forBorder)
                        borderPath.AddLine(rect.Left, rect.Bottom, rect.Right, rect.Bottom);

                    borderPath.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Bottom - 1);
                    borderPath.AddArc(rect.Right - x, rect.Top, x, x, 0f, -90f);
                    borderPath.AddLine(rect.Left + rpW, rect.Top, rect.Left, rect.Bottom);
                    break;
                case VisualOrientation.Bottom:
                    if (!forBorder)
                        borderPath.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);

                    borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Top + 1);
                    borderPath.AddArc(rect.Right - x, rect.Bottom - x, x, x, 0f, 90f);
                    borderPath.AddLine(rect.Left + rpW, rect.Bottom, rect.Left, rect.Top);
                    break;
            }
        }

        private static void AddSmoothPath(GraphicsPath borderPath,
                                          VisualOrientation orientation,
                                          Rectangle rect,
                                          bool forBorder)
        {
            // The tension of the lines depends on the width/height
            int minLength = Math.Min(rect.Width, rect.Height);
            int calcLength = Math.Min(minLength, 50);
            float tension = Math.Max(0.5f - (0.5f / 50 * calcLength), 0.05f);
            int indentW = Math.Min(5, rect.Width / 10);
            int indentH = Math.Min(5, rect.Height / 10);

            switch (orientation)
            {
                case VisualOrientation.Top:
                    // If there is not enough room for the rounded style then use the rounded
                    if (rect.Width < 14)
                        AddRoundedPath(borderPath, orientation, rect, forBorder);
                    else
                    {
                        if (!forBorder)
                            borderPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);

                        // Find way points along the width
                        int x2T = rect.Width / 2;
                        int x6T = rect.Width / 6;

                        borderPath.AddCurve(new Point[]{new Point(rect.Left, rect.Bottom),
                                                        new Point(rect.Left + indentW, rect.Top + 5),
                                                        new Point(rect.Left + x6T, rect.Top + 2),
                                                        new Point(rect.Left + x2T, rect.Top),
                                                        new Point(rect.Right - x6T, rect.Top + 2),
                                                        new Point(rect.Right - indentW, rect.Top + 5),
                                                        new Point(rect.Right, rect.Bottom)}, tension);
                    }
                    break;
                case VisualOrientation.Bottom:
                    // If there is not enough room for the rounded style then use the rounded
                    if (rect.Width < 14)
                        AddRoundedPath(borderPath, orientation, rect, forBorder);
                    else
                    {
                        if (!forBorder)
                            borderPath.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);

                        // Find way points along the width
                        int x2B = rect.Width / 2;
                        int x6B = rect.Width / 6;

                        borderPath.AddCurve(new Point[]{new Point(rect.Left, rect.Top),
                                                        new Point(rect.Left + indentW, rect.Bottom - 5),
                                                        new Point(rect.Left + x6B, rect.Bottom - 2),
                                                        new Point(rect.Left + x2B, rect.Bottom),
                                                        new Point(rect.Right - x6B, rect.Bottom - 2),
                                                        new Point(rect.Right - indentW, rect.Bottom - 5),
                                                        new Point(rect.Right, rect.Top)}, tension);
                    }
                    break;
                case VisualOrientation.Left:
                    // If there is not enough room for the rounded style then use the rounded
                    if (rect.Height < 14)
                        AddRoundedPath(borderPath, orientation, rect, forBorder);
                    else
                    {
                        if (!forBorder)
                            borderPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);

                        // Find way points along the width
                        int y2L = rect.Height / 2;
                        int y6L = rect.Height / 6;

                        borderPath.AddCurve(new Point[]{new Point(rect.Right, rect.Bottom),
                                                        new Point(rect.Left + 5, rect.Bottom - indentH),
                                                        new Point(rect.Left + 2, rect.Bottom - y6L),
                                                        new Point(rect.Left, rect.Bottom - y2L),
                                                        new Point(rect.Left + 2, rect.Top + y6L),
                                                        new Point(rect.Left + 5, rect.Top + indentH),
                                                        new Point(rect.Right, rect.Top)}, tension);
                    }
                    break;
                case VisualOrientation.Right:
                    // If there is not enough room for the rounded style then use the rounded
                    if (rect.Height < 14)
                        AddRoundedPath(borderPath, orientation, rect, forBorder);
                    else
                    {
                        if (!forBorder)
                            borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom);

                        // Find way points along the width
                        int y2R = rect.Height / 2;
                        int y6R = rect.Height / 6;

                        borderPath.AddCurve(new Point[]{new Point(rect.Left, rect.Bottom),
                                                        new Point(rect.Right - 5, rect.Bottom - indentH),
                                                        new Point(rect.Right - 2, rect.Bottom - y6R),
                                                        new Point(rect.Right, rect.Bottom - y2R),
                                                        new Point(rect.Right - 2, rect.Top + y6R),
                                                        new Point(rect.Right - 5, rect.Top + indentH),
                                                        new Point(rect.Left, rect.Top)}, tension);
                    }
                    break;
            }
        }

        private static bool ShouldDrawImage(Image image)
        {
            return (image != null);
        }

        private static Brush CreateColorBrush(Rectangle rect,
                                              Color color1,
                                              Color color2,
                                              PaletteColorStyle gradientStyle,
                                              float angle,
                                              VisualOrientation orientation)
        {
            // Should never provide the inherit value
            Debug.Assert(gradientStyle != PaletteColorStyle.Inherit);

            // If the gradient style is a solid colour
            if (gradientStyle == PaletteColorStyle.Solid)
                return new SolidBrush(color1);
            else
            {
                // Adjust angle for the orientation
                switch(orientation)
                {
                    case VisualOrientation.Left:
                        angle -= 90;
                        break;
                    case VisualOrientation.Right:
                        angle += 90;
                        break;
                    case VisualOrientation.Bottom:
                        angle += 180;
                        break;
                }

                // For OneNote we always use white as the first color
                if (gradientStyle == PaletteColorStyle.OneNote)
                    color1 = Color.White;

                // Otherwise we always create a linear brush using provided colors and angle
                LinearGradientBrush brush = new LinearGradientBrush(rect, color1, color2, angle);

                switch (gradientStyle)
                {
                    case PaletteColorStyle.Sigma:
                        brush.SetSigmaBellShape(0.5f);
                        break;
                    case PaletteColorStyle.Rounded:
                        brush.SetSigmaBellShape(1f, 1f);
                        break;
                    case PaletteColorStyle.Switch25:
                        brush.Blend = _switch25Blend;
                        break;
                    case PaletteColorStyle.Switch33:
                        brush.Blend = _switch33Blend;
                        break;
                    case PaletteColorStyle.Switch50:
                        brush.Blend = _switch50Blend;
                        break;
                    case PaletteColorStyle.Switch90:
                        brush.Blend = _switch90Blend;
                        break;
                    case PaletteColorStyle.Linear25:
                        brush.Blend = _linear25Blend;
                        break;
                    case PaletteColorStyle.Linear33:
                        brush.Blend = _linear33Blend;
                        break;
                    case PaletteColorStyle.Linear40:
                        brush.Blend = _linear40Blend;
                        break;
                    case PaletteColorStyle.Linear50:
                        brush.Blend = _linear50Blend;
                        break;
                    case PaletteColorStyle.HalfCut:
                        brush.Blend = _halfCutBlend;
                        break;
                    case PaletteColorStyle.QuarterPhase:
                        brush.Blend = _quarterPhaseBlend;
                        break;
                    case PaletteColorStyle.OneNote:
                        brush.Blend = _oneNoteBlend;
                        break;
                    case PaletteColorStyle.Rounding2:
                        brush.Blend = _rounding2Blend;
                        break;
                    case PaletteColorStyle.Rounding3:
                        brush.Blend = _rounding3Blend;
                        break;
                    case PaletteColorStyle.Rounding4:
                        brush.Blend = _rounding4Blend;
                        break;
                    case PaletteColorStyle.Rounding5:
                        brush.Blend = _rounding5Blend;
                        break;
                }

                return brush;
            }
        }

        private static Brush CreateImageBrush(Rectangle rect,
                                              Image image,
                                              PaletteImageStyle imageStyle)
        {
            // Create brush based on the provided image
            TextureBrush brush = new TextureBrush(image);

            // Create appropriate wrapping mode from image style
            switch (imageStyle)
            {
                case PaletteImageStyle.TopLeft:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Left, rect.Top);
                    break;
                case PaletteImageStyle.TopMiddle:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Left + (rect.Width - image.Width) / 2, rect.Top);
                    break;
                case PaletteImageStyle.TopRight:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Right - image.Width, rect.Top);
                    break;
                case PaletteImageStyle.CenterLeft:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Left, rect.Top + (rect.Height - image.Height) / 2);
                    break;
                case PaletteImageStyle.CenterMiddle:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Left + (rect.Width - image.Width) / 2, rect.Top + (rect.Height - image.Height) / 2);
                    break;
                case PaletteImageStyle.CenterRight:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Right - image.Width, rect.Top + (rect.Height - image.Height) / 2);
                    break;
                case PaletteImageStyle.BottomLeft:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Left, rect.Bottom - image.Height);
                    break;
                case PaletteImageStyle.BottomMiddle:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Left + (rect.Width - image.Width) / 2, rect.Bottom - image.Height);
                    break;
                case PaletteImageStyle.BottomRight:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Right - image.Width, rect.Bottom - image.Height);
                    break;
                case PaletteImageStyle.Stretch:
                    brush.WrapMode = WrapMode.Clamp;
                    brush.TranslateTransform(rect.Left, rect.Top);
                    brush.ScaleTransform((float)rect.Width / (float)image.Width, (float)rect.Height / (float)image.Height);
                    break;
                case PaletteImageStyle.Tile:
                    brush.WrapMode = WrapMode.Tile;
                    brush.TranslateTransform(rect.Left, rect.Top);
                    break;
                case PaletteImageStyle.TileFlipX:
                    brush.WrapMode = WrapMode.TileFlipX;
                    brush.TranslateTransform(rect.Left, rect.Top);
                    break;
                case PaletteImageStyle.TileFlipY:
                    brush.WrapMode = WrapMode.TileFlipY;
                    brush.TranslateTransform(rect.Left, rect.Top);
                    break;
                case PaletteImageStyle.TileFlipXY:
                    brush.WrapMode = WrapMode.TileFlipXY;
                    brush.TranslateTransform(rect.Left, rect.Top);
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    throw new ArgumentOutOfRangeException("imageStyle");
            }

            return brush;
        }

        private void DrawBackSolidInside(RenderContext context,
                                         Rectangle gradientRect,
                                         Color backColor1,
                                         Color backColor2,
                                         GraphicsPath path)
        {
            // Clip to prevent drawing outside the path
            using(Clipping clip = new Clipping(context.Graphics, path))
            {
                // Get the rectangle that encloses the path
                RectangleF rectF = path.GetBounds();

                // Convert to a pixel aligned rectangle
                Rectangle rect;

                // Do we have any non-integeer numbers to convert
                if ((Math.Round(rectF.X) != rectF.X) ||
                    (Math.Round(rectF.Y) != rectF.Y) ||
                    (Math.Round(rectF.Width) != rectF.Width) ||
                    (Math.Round(rectF.Height) != rectF.Height))
                {
                    int x = (int)Math.Round(rectF.X);
                    int y = (int)Math.Round(rectF.Y);
                    int width = (int)Math.Round(rectF.Width + 1 + (rectF.X - x));
                    int height = (int)Math.Round(rectF.Height + 1 + (rectF.Y - y));
                    rect = new Rectangle(x, y, width, height);            
                }
                else
                    rect = new Rectangle((int)rectF.X, (int)rectF.Y, (int)rectF.Width, (int)rectF.Height);

                using (Brush backBrush1 = CreateColorBrush(gradientRect, backColor1, backColor1, PaletteColorStyle.Solid, 0f, VisualOrientation.Top),
                             backBrush2 = CreateColorBrush(gradientRect, backColor2, backColor2, PaletteColorStyle.Solid, 0f, VisualOrientation.Top))
                {
                    // Draw the first color over the entire area
                    context.Graphics.FillRectangle(backBrush1, rect);

                    // Draw the second color on the inside of the area
                    rect.Inflate(-2, -2);
                    context.Graphics.FillRectangle(backBrush2, rect);

                    // Draw the first color inside the rest of the area
                    rect.Inflate(-1, -1);
                    context.Graphics.FillRectangle(backBrush1, rect);
                }
            }
        }

        private void DrawBackOneNote(RenderContext context,
                                     Rectangle gradientRect,
                                     Color backColor1,
                                     Color backColor2,
                                     PaletteColorStyle backColorStyle,
                                     float backColorAngle,
                                     VisualOrientation orientation,
                                     GraphicsPath path)
        {
            // Draw entire background in first color
            using (Brush backBrush = CreateColorBrush(gradientRect,
                                                      backColor1,
                                                      backColor1,
                                                      backColorStyle,
                                                      backColorAngle,
                                                      orientation))
            {
                context.Graphics.FillPath(backBrush, path);
            }

            // Make a copy of the original path, so we can change it
            GraphicsPath insetPath = (GraphicsPath)path.Clone();

            // Offset by 1.5 pixels so the background shows around two of
            // the edges of the background we are about to draw
            switch (orientation)
            {
                case VisualOrientation.Top:
                    insetPath.Transform(new Matrix(1, 0, 0, 1, 1.5f, 1.5f));
                    break;
                case VisualOrientation.Bottom:
                case VisualOrientation.Left:
                    insetPath.Transform(new Matrix(1, 0, 0, 1, 1.5f, -1.5f));
                    break;
                case VisualOrientation.Right:
                    insetPath.Transform(new Matrix(1, 0, 0, 1, -1.5f, 1.5f));
                    break;
            }

            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw the second color as the offset background
                using (Brush backBrush = CreateColorBrush(gradientRect,
                                                          backColor2,
                                                          backColor2,
                                                          backColorStyle,
                                                          backColorAngle,
                                                          orientation))
                {
                    context.Graphics.FillPath(backBrush, insetPath);
                }
            }

            // Dispose of created resources
            insetPath.Dispose();
        }

        private void DrawBackSolidLine(RenderContext context,
                                       Rectangle rect,
                                       Color backColor1,
                                       Color backColor2,
                                       PaletteColorStyle style,
                                       GraphicsPath path)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw entire background in second color
                using (SolidBrush brushColor2 = new SolidBrush(backColor2))
                    context.Graphics.FillRectangle(brushColor2, rect);

                // Reduce area by edge(s) we want to leave alone
                switch (style)
                {
                    case PaletteColorStyle.SolidTopLine:
                        rect.Y++;
                        rect.Height--;
                        break;
                    case PaletteColorStyle.SolidBottomLine:
                        rect.Height--;
                        break;
                    case PaletteColorStyle.SolidLeftLine:
                        rect.X++;
                        rect.Width--;
                        break;
                    case PaletteColorStyle.SolidRightLine:
                        rect.Width--;
                        break;
                    case PaletteColorStyle.SolidAllLine:
                        rect.X++;
                        rect.Y++;
                        rect.Width -= 2;
                        rect.Height -= 2;
                        break;
                }

                // Draw the second color as a solid block
                using (SolidBrush brushColor2 = new SolidBrush(backColor1))
                    context.Graphics.FillRectangle(brushColor2, rect);
            }
        }

        private void DrawBackRoundedTopLeftWhite(RenderContext context,
                                                 Rectangle rect,
                                                 Rectangle gradientRect,
                                                 Color backColor1,
                                                 Color backColor2,
                                                 PaletteColorStyle backColorStyle,
                                                 float backColorAngle,
                                                 VisualOrientation orientation,
                                                 GraphicsPath path)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw entire background in white
                context.Graphics.FillRectangle(Brushes.White, rect);

                // Offset the drawing rectangle for the top and left
                rect.X++;
                rect.Y++;

                // Size is smaller in both directions because of offseting
                rect.Width--;
                rect.Height--;

                // Draw the second color as the offset background
                using (Brush backBrush = CreateColorBrush(gradientRect,
                                                          backColor1,
                                                          backColor2,
                                                          PaletteColorStyle.Rounded,
                                                          backColorAngle,
                                                          orientation))
                {
                    context.Graphics.FillRectangle(backBrush, rect);
                }
            }
        }

        private void DrawBackRoundedTopLight(RenderContext context,
                                             Rectangle rect,
                                             Rectangle gradientRect,
                                             Color backColor1,
                                             Color backColor2,
                                             PaletteColorStyle backColorStyle,
                                             float backColorAngle,
                                             VisualOrientation orientation,
                                             GraphicsPath path)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Draw entire background in white
                using(SolidBrush lightBrush = new SolidBrush(ControlPaint.LightLight(backColor1)))
                    context.Graphics.FillRectangle(lightBrush, rect);

                // Offset the drawing rectangle depending on the orientation
                switch (orientation)
                {
                    case VisualOrientation.Top:
                        rect.Y++;
                        rect.Height--;
                        break;
                    case VisualOrientation.Bottom:
                        rect.Height--;
                        break;
                    case VisualOrientation.Left:
                        rect.X++;
                        rect.Width--;
                        break;
                    case VisualOrientation.Right:
                        rect.Width--;
                        break;
                }

                // Draw the second color as the offset background
                using (Brush backBrush = CreateColorBrush(gradientRect,
                                                          backColor1,
                                                          backColor2,
                                                          PaletteColorStyle.Rounded,
                                                          backColorAngle,
                                                          orientation))
                {
                    context.Graphics.FillRectangle(backBrush, rect);
                }
            }
        }

        private void DrawBackRounded4(RenderContext context,
                                      Rectangle rect,
                                      Rectangle gradientRect,
                                      Color backColor1,
                                      Color backColor2,
                                      PaletteColorStyle backColorStyle,
                                      float backColorAngle,
                                      VisualOrientation orientation,
                                      GraphicsPath path)
        {
            using (Clipping clip = new Clipping(context.Graphics, path))
            {
                // Use standard helper routine to create appropriate color brush
                using (Brush backBrush = CreateColorBrush(gradientRect, backColor1, backColor2,
                                                          backColorStyle, backColorAngle, orientation))
                {
                    context.Graphics.FillPath(backBrush, path);
                }

                using (Pen linePen = new Pen(backColor1))
                {
                    // Adjust angle for the orientation
                    switch (orientation)
                    {
                        case VisualOrientation.Left:
                            context.Graphics.DrawLine(linePen, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 1);
                            break;
                        case VisualOrientation.Right:
                            context.Graphics.DrawLine(linePen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
                            break;
                        case VisualOrientation.Bottom:
                            context.Graphics.DrawLine(linePen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
                            break;
                        case VisualOrientation.Top:
                            context.Graphics.DrawLine(linePen, rect.Left, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
                            break;
                    }
                }
            }
        }

        private void DrawBackRounding5(RenderContext context,
                                       Rectangle rect,
                                       Rectangle gradientRect,
                                       Color backColor1,
                                       Color backColor2,
                                       PaletteColorStyle backColorStyle,
                                       float backColorAngle,
                                       VisualOrientation orientation,
                                       GraphicsPath path)
        {
            // We want to ignore the ourside pixel edge, so inflate inwards
            rect.Inflate(-1, -1);

            // Prevent drawing over that ourside edge
            using (Clipping clip = new Clipping(context.Graphics, rect))
            {
                // Use standard helper routine to create appropriate color brush
                using (Brush backBrush = CreateColorBrush(gradientRect, backColor1, backColor2,
                                                          PaletteColorStyle.Rounding5, backColorAngle, 
                                                          orientation))
                {
                    context.Graphics.FillPath(backBrush, path);
                }
            }
        }

        private void DrawBackLinearShadow(RenderContext context,
                                          Rectangle rect,
                                          Rectangle gradientRect,
                                          Color backColor1,
                                          Color backColor2,
                                          PaletteColorStyle backColorStyle,
                                          float backColorAngle,
                                          VisualOrientation orientation,
                                          GraphicsPath path)
        {
            // Prevent drawing over that ourside edge
            using (Clipping clip = new Clipping(context.Graphics, rect))
            {
                // Use standard helper routine to create appropriate color brush
                using (Brush backBrush = CreateColorBrush(gradientRect, backColor1, backColor2,
                                                          PaletteColorStyle.Linear, backColorAngle,
                                                          orientation))
                {
                    context.Graphics.FillPath(backBrush, path);
                }

                // Use path gradient to give the outside of the area a shadow effect
                using (PathGradientBrush borderBrush = new PathGradientBrush(path))
                {
                    borderBrush.Blend = _linearShadowBlend;
                    borderBrush.CenterColor = backColor1;
                    borderBrush.SurroundColors = new Color[] { backColor2 };
                    context.Graphics.FillPath(borderBrush, path);
                }
            }
        }
        #endregion

        #region Implementation Content
        private static Padding ContentPaddingForButtonForm(Padding original,
                                                           ViewLayoutContext context,
                                                           int allocatedHeight)
        {
            // Get the krypton form that contains this control
            KryptonForm kryptonForm = OwningKryptonForm(context.TopControl);

            // Not interested if not inside a krypton form
            if (kryptonForm != null)
            {
                // Get the padding just for the chrome borders
                Padding chromeBorders = kryptonForm.RealWindowBorders;

                // How much border space to allocate per button edge
                int buttonBorder = (chromeBorders.Top - allocatedHeight - 10) / 2;

                if (buttonBorder > 0)
                    return new Padding(buttonBorder);
                else
                    return Padding.Empty;
            }

            return original;
        }

        private static Font ContentFontForButtonForm(ViewLayoutContext context,
                                                     Font font)
        {
            // Get the krypton form that contains this control
            KryptonForm kryptonForm = OwningKryptonForm(context.TopControl);

            // Not interested if not inside a krypton form
            if (kryptonForm != null)
            {
                // Get the padding just for the chrome borders
                Padding chromeBorders = kryptonForm.RealWindowBorders;

                // How much space is available for the font
                int fontSpace = chromeBorders.Top - 6;

                // If not enough room for the font then create a new smaller font
                if ((font.Height > fontSpace) && (fontSpace > 5))
                {
                    // Find the point size from the pixel height required
                    float point = 72 / context.Graphics.DpiY * (fontSpace / 1.333f);
                    
                    // No point having a font smaller than 3 points
                    if (point > 3)
                        font = new Font(font.FontFamily, point, font.Style);
                }
            }

            return font;
        }

        private static KryptonForm OwningKryptonForm(Control c)
        {
            // Climb chain looking for the Krypton Form instance
            while ((c != null) && !(c is KryptonForm))
                c = c.Parent;

            return c as KryptonForm;
        }

        private static void AllocateImageSpace(StandardContentMemento memento,
                                               IPaletteContent paletteContent,
                                               IContentValues contentValues,
                                               PaletteState state,
                                               Rectangle displayRect,
                                               RightToLeft rtl,
                                               ref Size[,] allocation)
        {
            // By default, we cannot draw the image
            memento.DrawImage = false;

            // Get the image details
            memento.Image = contentValues.GetImage(state);
            memento.ImageTransparentColor = contentValues.GetImageTransparentColor(state);

            // Is there any image to be drawn?
            if (memento.Image != null)
            {
                try
                {
                    // Cache the size of the image
                    memento.ImageRect.Size = memento.Image.Size;

                    // Check for enough space to show all of the image
                    if ((displayRect.Width >= memento.ImageRect.Width) &&
                        (displayRect.Height >= memento.ImageRect.Height))
                    {
                        // Convert from alignment enums to integers
                        int alignHIndex = RightToLeftIndex(rtl, paletteContent.GetContentImageH(state));
                        int alignVIndex = (int)paletteContent.GetContentImageV(state);

                        // Bump the allocated space in the destination grid cell
                        allocation[alignHIndex, alignVIndex].Width += memento.ImageRect.Width;
                        allocation[alignHIndex, alignVIndex].Height += memento.ImageRect.Height;

                        // Yes, we do want to draw the image/icon
                        memento.DrawImage = true;
                    }
                }
                catch
                {
                    // Image is not valid, so do not use it!
                    memento.Image = null;
                    memento.DrawImage = false;
                }
            }
        }

        private static void AllocateShortTextSpace(ViewLayoutContext context,
                                                   Graphics g,
                                                   StandardContentMemento memento,
                                                   IPaletteContent paletteContent,
                                                   IContentValues contentValues,
                                                   PaletteState state,
                                                   Rectangle displayRect,
                                                   RightToLeft rtl,
                                                   int spacingGap,
                                                   ref Size[,] allocation,
                                                   bool composition)
        {
            // By default, we cannot draw the text
            memento.DrawShortText = false;

            // Get the defined text for display
            string shortText = contentValues.GetShortText();

            // Is there any text to be drawn?
            if ((shortText != null) && (shortText.Length > 0))
            {
                // If the text is not allowed to span multiple lines
                if (paletteContent.GetContentShortTextMultiLine(state) == InheritBool.False)
                {
                    // Replace any carriage returns and newlines with just spaces
                    shortText = shortText.Replace("\r\n", " ");
                    shortText = shortText.Replace("\n", " ");
                    shortText = shortText.Replace("\r", " ");
                }

                // Convert from alignment enums to integers
                int alignHIndex = RightToLeftIndex(rtl, paletteContent.GetContentShortTextH(state));
                int alignVIndex = (int)paletteContent.GetContentShortTextV(state);

                // Cache the rendering hint used
                memento.ShortTextHint = CommonHelper.PaletteTextHintToRenderingHint(paletteContent.GetContentShortTextHint(state));
                memento.ShortTextTrimming = paletteContent.GetContentShortTextTrim(state);

                bool fontChanged = false;
                Font textFont = paletteContent.GetContentShortTextFont(state);

                // Get the appropriate font to use in the caption area
                if (paletteContent.GetContentStyle() == PaletteContentStyle.HeaderForm)
                {
                    Font captionFont = ContentFontForButtonForm(context, textFont);
                    fontChanged = (captionFont != textFont);
                    textFont = captionFont;
                }

                // Get a pixel accurate measure of text drawing space needed
                memento.ShortTextMemento = AccurateText.MeasureString(g,
                                                                      rtl,
                                                                      shortText,
                                                                      textFont,
                                                                      memento.ShortTextTrimming,
                                                                      paletteContent.GetContentShortTextMultiLineH(state),
                                                                      paletteContent.GetContentShortTextPrefix(state),
                                                                      memento.ShortTextHint,
                                                                      composition,
                                                                      fontChanged);

                // Space required for short text starts with the text width itself
                Size requiredSpace = memento.ShortTextMemento.Size;

                // Find the space available given our required alignment
                if (AllocateAlignmentSpace(alignHIndex, alignVIndex,
                                           allocation, displayRect,
                                           spacingGap, memento.ShortTextTrimming,
                                           ref requiredSpace))
                {
                    // Allocate the actual space used up
                    // Cache the actual draw size of the text
                    memento.ShortTextRect.Size = requiredSpace;

                    // Mark the memento to draw the short text
                    memento.DrawShortText = true;
                }
            }
        }

        private static void AllocateLongTextSpace(ViewLayoutContext context,
                                                  Graphics g,
                                                  StandardContentMemento memento,
                                                  IPaletteContent paletteContent,
                                                  IContentValues contentValues,
                                                  PaletteState state,
                                                  Rectangle displayRect,
                                                  RightToLeft rtl,
                                                  int spacingGap,
                                                  ref Size[,] allocation,
                                                  bool composition)
        {
            // By default, we cannot draw the text
            memento.DrawLongText = false;

            // Get the defined text for display
            string longText = contentValues.GetLongText();

            // Is there any text to be drawn?
            if ((longText != null) && (longText.Length > 0))
            {
                // If the text is not allowed to span multiple lines
                if (paletteContent.GetContentLongTextMultiLine(state) == InheritBool.False)
                {
                    // Replace any carriage returns and newlines with just spaces
                    longText = longText.Replace("\r\n", " ");
                    longText = longText.Replace("\n", " ");
                    longText = longText.Replace("\r", " ");
                }

                // Convert from alignment enums to integers
                int alignHIndex = RightToLeftIndex(rtl, paletteContent.GetContentLongTextH(state));
                int alignVIndex = (int)paletteContent.GetContentLongTextV(state);

                // Cache the rendering hint used
                memento.LongTextHint = CommonHelper.PaletteTextHintToRenderingHint(paletteContent.GetContentLongTextHint(state));
                memento.LongTextTrimming = paletteContent.GetContentLongTextTrim(state);

                bool fontChanged = false;
                Font textFont = paletteContent.GetContentLongTextFont(state);
                
                // Get the appropriate font to use in the caption area
                if (paletteContent.GetContentStyle() == PaletteContentStyle.HeaderForm)
                {
                    Font captionFont = ContentFontForButtonForm(context, textFont);
                    fontChanged = (captionFont != textFont);
                    textFont = captionFont;
                }

                // Get a pixel accurate measure of text drawing space needed
                memento.LongTextMemento = AccurateText.MeasureString(g,
                                                                     rtl,
                                                                     longText,
                                                                     textFont,
                                                                     memento.LongTextTrimming,
                                                                     paletteContent.GetContentLongTextMultiLineH(state),
                                                                     paletteContent.GetContentLongTextPrefix(state),
                                                                     memento.LongTextHint,
                                                                     composition,
                                                                     fontChanged);

                // Space required for long text starts with the text width itself
                Size requiredSpace = memento.LongTextMemento.Size;

                // Find the space available given our required alignment
                if (AllocateAlignmentSpace(alignHIndex, alignVIndex,
                                           allocation, displayRect,
                                           spacingGap, memento.LongTextTrimming,
                                           ref requiredSpace))
                {
                    // Cache the actual draw size of the text
                    memento.LongTextRect.Size = requiredSpace;

                    // Mark the memento to draw the long text
                    memento.DrawLongText = true;
                }
            }
        }

        private static int RightToLeftIndex(RightToLeft rtl, PaletteRelativeAlign align)
        {
            switch (align)
            {
                case PaletteRelativeAlign.Near:
                    return (rtl == RightToLeft.Yes ? 2 : 0);
                case PaletteRelativeAlign.Center:
                    return 1;
                case PaletteRelativeAlign.Far:
                    return (rtl == RightToLeft.Yes ? 0 : 2);
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    throw new ArgumentOutOfRangeException("align");
            }
        }

        private static int AllocatedTotalWidth(Size[,] allocation, 
                                               int colIndex,
                                               int rowIndex,
                                               int spacingGap)
        {
            // Find the width of each column
            int[] colWidths = AllocatedColumnWidths(allocation, rowIndex);

            // Add each column width together
            int totalWidth = colWidths[0] + colWidths[1] + colWidths[2];

            // If the target column for allocation is empty then an extra
            // spacing gap will be required so add it to the total width,
            // unless there is nothing at all allocated
            if ((totalWidth > 0) && (colIndex >= 0) && (colWidths[colIndex] == 0))
                totalWidth += spacingGap;

            // Add any required spacing gaps between columns
            if ((colWidths[0] > 0) && (colWidths[1] > 0))
                totalWidth += spacingGap;

            if ((colWidths[1] > 0) && (colWidths[2] > 0))
                totalWidth += spacingGap;

            if ((colWidths[0] > 0) && (colWidths[1] == 0) && (colWidths[2] > 0))
                totalWidth += spacingGap;

            return totalWidth;
        }

        private static int AllocatedTotalHeight(Size[,] allocation)
        {
            int[] rowHeights = AllocatedRowHeights(allocation);
            return rowHeights[0] + rowHeights[1] + rowHeights[2];
        }

        private static int[] AllocatedColumnWidths(Size[,] allocation, int rowIndex)
        {
            int[] colWidths = new int[] { 0, 0, 0 };

            for (int col = 0; col < 3; col++)
            {
                if (rowIndex == -1)
                {
                    for (int row = 0; row < 3; row++)
                    {
                        // Store the widest cell in each column
                        if (allocation[col, row].Width > colWidths[col])
                            colWidths[col] = allocation[col, row].Width;
                    }
                }
                else
                {
                    // Store the widest cell in each column
                    if (allocation[col, rowIndex].Width > colWidths[col])
                        colWidths[col] = allocation[col, rowIndex].Width;
                }
            }

            return colWidths;
        }

        private static int[] AllocatedRowHeights(Size[,] allocation)
        {
            int[] rowHeights = new int[] { 0, 0, 0 };

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    // Store the widest cell in each column
                    if (allocation[col, row].Height > rowHeights[row])
                        rowHeights[row] = allocation[col, row].Height;
                }
            }

            return rowHeights;
        }

        private static bool AllocateAlignmentSpace(int alignHIndex,
                                                   int alignVIndex,
                                                   Size[,] allocation,
                                                   Rectangle displayRect,
                                                   int spacingGap,
                                                   PaletteTextTrim trim,
                                                   ref Size requiredSize)
        {
            // Cache the current target value
            Size cacheSize = allocation[alignHIndex, alignVIndex];

            // Track the width needed to show the item
            bool applyGap = false;
            int allocateWidth = requiredSize.Width;

            // If there is already something in the cell
            if (allocation[alignHIndex, alignVIndex].Width > 0)
            {
                // Then add the spacing gap to the required size width
                allocateWidth += spacingGap;
                applyGap = true;
            }

            // Find the current allocated total width
            int totalWidth = AllocatedTotalWidth(allocation, alignHIndex, alignVIndex, spacingGap);

            // How much space is available for allocation?
            int freeSpace = displayRect.Width - totalWidth;

            // If not enough room then we failed
            if (freeSpace < allocateWidth)
            {
                // Should we try and trim the text into the space?
                if (trim != PaletteTextTrim.Hide)
                {
                    // If there is some room available after taking 
                    // into account the need for a spacing gap
                    if ((allocateWidth == requiredSize.Width) ||
                        ((allocateWidth > requiredSize.Width) && applyGap))
                    {
                        // Allocate just the available space
                        allocateWidth = freeSpace;

                        // Reduce the reported size back to the caller
                        if (applyGap)
                            requiredSize.Width = allocateWidth - spacingGap;
                        else
                            requiredSize.Width = allocateWidth;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }

            // There is enough space for all the content, so add into the cell width
            allocation[alignHIndex, alignVIndex].Width += allocateWidth;

            // If the required height is greater than the current cell height
            if (requiredSize.Height > allocation[alignHIndex, alignVIndex].Height)
            {
                // Then use the required height instead
                allocation[alignHIndex, alignVIndex].Height = requiredSize.Height;
            }

            // Find the allocated total height as a result
            int totalHeight = AllocatedTotalHeight(allocation);

            // If not enough height then we failed
            if (totalHeight > displayRect.Height)
            {
                // Restore the original cell
                allocation[alignHIndex, alignVIndex] = cacheSize;
                return false;
            }

            return true;
        }

        private static void ApplyExcessSpace(int excess, ref int[] cells)
        {
            // If there is already some space in the center
            if (cells[1] > 0)
            {
                // Then all the excess to the centre
                cells[1] += excess;
            }
            else if (cells[2] == 0)
            {
                // Only the near cell is used, so give all excess to it
                cells[0] += excess;
            }
            else if (cells[0] == 0)
            {
                // Only the far cell is used, so give all excess to it
                cells[2] += excess;
            }
            else
            {
                int half = excess / 2;

                // Space in the near and the far, so give half to each
                cells[0] += half;
                cells[2] += (excess - half);
            }
        }

        private static void PositionAlignContent(StandardContentMemento memento,
                                                 IPaletteContent paletteContent,
                                                 PaletteState state,
                                                 RightToLeft rtl,
                                                 PaletteRelativeAlign alignH,
                                                 PaletteRelativeAlign alignV,
                                                 int cellX,
                                                 int cellY,
                                                 int cellWidth,
                                                 int cellHeight,
                                                 int spacingGap)
        {
            // Create client rectangle covering cell size
            Rectangle cellRect = new Rectangle(cellX, cellY, cellWidth, cellHeight);

            PaletteRelativeAlign drawHImage = paletteContent.GetContentImageH(state);
            PaletteRelativeAlign drawVImage = paletteContent.GetContentImageV(state);
            PaletteRelativeAlign drawHShort = paletteContent.GetContentShortTextH(state);
            PaletteRelativeAlign drawVShort = paletteContent.GetContentShortTextV(state);
            PaletteRelativeAlign drawHLong = paletteContent.GetContentLongTextH(state);
            PaletteRelativeAlign drawVLong = paletteContent.GetContentLongTextV(state);

            PaletteRelativeAlign posHImage = drawHImage;
            PaletteRelativeAlign posHShort = drawHShort;
            PaletteRelativeAlign posHLong = drawHLong;

            // If positioning in the center, then need extra processing
            if (alignH == PaletteRelativeAlign.Center)
            {
                // Find number of content and width of those in this cell
                int totalWidth = 0;
                int totalItems = 0;

                if (memento.DrawImage && (drawHImage == alignH) && (drawVImage == alignV))
                {
                    totalWidth += memento.ImageRect.Width;
                    totalItems++;
                }

                if (memento.DrawShortText && (drawHShort == alignH) && (drawVShort == alignV))
                {
                    totalWidth += memento.ShortTextRect.Width;
                    totalItems++;
                }

                if (memento.DrawLongText && (drawHLong == alignH) && (drawVLong == alignV))
                {
                    totalWidth += memento.LongTextRect.Width;
                    totalItems++;
                }

                // If more than one item is to be positioned
                if (totalItems > 1)
                {
                    // Add on required number of spacing gaps
                    totalWidth += (totalItems - 1) * spacingGap;

                    // Then center the space for the content
                    int halfWidth = (cellRect.Width - totalWidth) / 2;
                    cellRect.Width -= (halfWidth * 2);
                    cellRect.X += halfWidth;

                    // Ensure all content are placed near, so they fit exactly
                    posHImage = posHShort = posHLong = PaletteRelativeAlign.Near;
                }
            }

            // Do we need to position the image?
            if (memento.DrawImage && (drawHImage == alignH) && (drawVImage == alignV))
                memento.ImageRect.Location = PositionCellContent(rtl, posHImage, drawVImage, memento.ImageRect.Size, spacingGap, ref cellRect);

            // Do we need to position the short text?
            if (memento.DrawShortText && (drawHShort == alignH) && (drawVShort == alignV))
                memento.ShortTextRect.Location = PositionCellContent(rtl, posHShort, drawVShort, memento.ShortTextRect.Size, spacingGap, ref cellRect);

            // Do we need to position the long text?
            if (memento.DrawLongText && (drawHLong == alignH) && (drawVLong == alignV))
                memento.LongTextRect.Location = PositionCellContent(rtl, posHLong, drawVLong, memento.LongTextRect.Size, spacingGap, ref cellRect);
        }

        private static Point PositionCellContent(RightToLeft rtl,
                                                 PaletteRelativeAlign drawH,
                                                 PaletteRelativeAlign drawV,
                                                 Size contentSize,
                                                 int spacingGap,
                                                 ref Rectangle cellRect)
        {
            Point location = Point.Empty;

            // If drawing from right to left...
            if (rtl == RightToLeft.Yes)
            {
                // Then invert the near and far positioning
                if (drawH == PaletteRelativeAlign.Near)
                    drawH = PaletteRelativeAlign.Far;
                else if (drawH == PaletteRelativeAlign.Far)
                    drawH = PaletteRelativeAlign.Near;
            }

            switch (drawH)
            {
                case PaletteRelativeAlign.Near:
                    location.X = cellRect.Left;
                    cellRect.X += (contentSize.Width + spacingGap);
                    cellRect.Width -= (contentSize.Width + spacingGap);
                    break;
                case PaletteRelativeAlign.Center:
                    int halfHorz = (cellRect.Width - contentSize.Width) / 2;
                    location.X = cellRect.Left + halfHorz;
                    break;
                case PaletteRelativeAlign.Far:
                    location.X = cellRect.Right - contentSize.Width;
                    cellRect.Width -= (contentSize.Width + spacingGap);
                    break;
            }

            switch (drawV)
            {
                case PaletteRelativeAlign.Near:
                    location.Y = cellRect.Top;
                    break;
                case PaletteRelativeAlign.Center:
                    int halfVert = (cellRect.Height - contentSize.Height) / 2;
                    location.Y = cellRect.Top + halfVert;
                    break;
                case PaletteRelativeAlign.Far:
                    location.Y = cellRect.Bottom - contentSize.Height;
                    break;
            }

            return location;
        }
        #endregion

        #region Implementation Glyph
        private static CheckBoxState DiscoverCheckBoxState(bool enabled,
                                                           CheckState checkState,
                                                           bool tracking,
                                                           bool pressed)
        {
            switch (checkState)
            {
                default:
                case CheckState.Unchecked:
                    if (!enabled)
                        return CheckBoxState.UncheckedDisabled;
                    else if (pressed)
                        return CheckBoxState.UncheckedPressed;
                    else if (tracking)
                        return CheckBoxState.UncheckedHot;
                    else
                        return CheckBoxState.UncheckedNormal;
                case CheckState.Checked:
                    if (!enabled)
                        return CheckBoxState.CheckedDisabled;
                    else if (pressed)
                        return CheckBoxState.CheckedPressed;
                    else if (tracking)
                        return CheckBoxState.CheckedHot;
                    else
                        return CheckBoxState.CheckedNormal;
                case CheckState.Indeterminate:
                    if (!enabled)
                        return CheckBoxState.MixedDisabled;
                    else if (pressed)
                        return CheckBoxState.MixedPressed;
                    else if (tracking)
                        return CheckBoxState.MixedHot;
                    else
                        return CheckBoxState.MixedNormal;
            }
        }

        RadioButtonState DiscoverRadioButtonState(bool enabled,
                                                  bool checkState,
                                                  bool tracking,
                                                  bool pressed)
        {
            if (checkState)
            {
                if (!enabled)
                    return RadioButtonState.CheckedDisabled;
                else if (pressed)
                    return RadioButtonState.CheckedPressed;
                else if (tracking)
                    return RadioButtonState.CheckedHot;
                else
                    return RadioButtonState.CheckedNormal;
            }
            else
            {
                if (!enabled)
                    return RadioButtonState.UncheckedDisabled;
                else if (pressed)
                    return RadioButtonState.UncheckedPressed;
                else if (tracking)
                    return RadioButtonState.UncheckedHot;
                else
                    return RadioButtonState.UncheckedNormal;
            }
        }

        private void MeasureDragDockingSquares(RenderDragDockingData dragData,
                                               IPaletteDragDrop dragDropPalette)
        {
            dragData.DockWindowSize = new Size(88, 88);

            if (dragData.ShowMiddle)
            {
                dragData.RectLeft = new Rectangle(0, 29, 29, 29);
                dragData.RectRight = new Rectangle(59, 29, 29, 29);
                dragData.RectTop = new Rectangle(29, 0, 29, 29);
                dragData.RectBottom = new Rectangle(29, 59, 29, 29);
                dragData.RectMiddle = new Rectangle(23, 23, 40, 40);
            }
            else
            {
                dragData.RectLeft = new Rectangle(0, 29, 32, 29);
                dragData.RectRight = new Rectangle(56, 29, 32, 29);
                dragData.RectTop = new Rectangle(29, 0, 29, 32);
                dragData.RectBottom = new Rectangle(29, 56, 29, 31);
            }
        }

        private void MeasureDragDockingRounded(RenderDragDockingData dragData,
                                               IPaletteDragDrop dragDropPalette)
        {
            dragData.DockWindowSize = new Size(103, 103);
            dragData.RectLeft = new Rectangle(0, 36, 32, 31);
            dragData.RectRight = new Rectangle(71, 36, 32, 31);
            dragData.RectTop = new Rectangle(36, 0, 31, 32);
            dragData.RectBottom = new Rectangle(36, 71, 31, 32);
            dragData.RectMiddle = new Rectangle(36, 36, 31, 31);
        }

        private void DrawDragDockingRounded(RenderContext context,
                                            RenderDragDockingData dragData,
                                            IPaletteDragDrop dragDropPalette)
        {
            Color back = dragDropPalette.GetDragDropDockBack();
            Color border = dragDropPalette.GetDragDropDockBorder();
            Color active = dragDropPalette.GetDragDropDockActive();
            Color inactive = dragDropPalette.GetDragDropDockInactive();

            DrawDragDockingRoundedBackground(context, back, border, dragData);
            if (dragData.ShowLeft)      DrawDragDockingRoundedLeft(context, back, border, active, inactive, dragData);
            if (dragData.ShowRight)     DrawDragDockingRoundedRight(context, back, border, active, inactive, dragData);
            if (dragData.ShowTop)       DrawDragDockingRoundedTop(context, back, border, active, inactive, dragData);
            if (dragData.ShowBottom)    DrawDragDockingRoundedBottom(context, back, border, active, inactive, dragData);
            if (dragData.ShowMiddle)    DrawDragDockingRoundedMiddle(context, back, border, active, inactive, dragData);
        }

        private void DrawDragDockingRoundedBackground(RenderContext context,
                                                      Color inside,
                                                      Color border,
                                                      RenderDragDockingData dragData)
        {
            if (dragData.ShowBack)
                DrawDragDockingRoundedRect(context, inside, border, new Rectangle(16, 16, 73, 73), 11);
        }

        private void DrawDragDockingRoundedLeft(RenderContext context,
                                                Color inside, Color border,
                                                Color active, Color inactive,
                                                RenderDragDockingData dragData)
        {
            DrawDragDockingRoundedRect(context, (dragData.ActiveLeft ? active : inside), (dragData.ActiveLeft ? active : border), dragData.RectLeft, 3);
            DrawDragDockingArrow(context, active, dragData.RectLeft, VisualOrientation.Left);
        }

        private void DrawDragDockingRoundedRight(RenderContext context,
                                                 Color inside, Color border,
                                                 Color active, Color inactive,
                                                 RenderDragDockingData dragData)
        {
            DrawDragDockingRoundedRect(context, (dragData.ActiveRight ? active : inside), (dragData.ActiveRight ? active : border), dragData.RectRight, 3);
            DrawDragDockingArrow(context, active, dragData.RectRight, VisualOrientation.Right);
        }

        private void DrawDragDockingRoundedTop(RenderContext context,
                                               Color inside, Color border,
                                               Color active, Color inactive,
                                               RenderDragDockingData dragData)
        {
            DrawDragDockingRoundedRect(context, (dragData.ActiveTop ? active : inside), (dragData.ActiveTop ? active : border), dragData.RectTop, 3);
            DrawDragDockingArrow(context, active, dragData.RectTop, VisualOrientation.Top);
        }

        private void DrawDragDockingRoundedBottom(RenderContext context,
                                                  Color inside, Color border,
                                                  Color active, Color inactive,
                                                  RenderDragDockingData dragData)
        {
            DrawDragDockingRoundedRect(context, (dragData.ActiveBottom ? active : inside), (dragData.ActiveBottom ? active : border), dragData.RectBottom, 3);
            DrawDragDockingArrow(context, active, dragData.RectBottom, VisualOrientation.Bottom);
        }

        private void DrawDragDockingRoundedMiddle(RenderContext context,
                                                  Color inside, Color border,
                                                  Color active, Color inactive,
                                                  RenderDragDockingData dragData)
        {
            Color borderColor = (dragData.ActiveMiddle ? active : border);
            Color insideColor = (dragData.ActiveMiddle ? active : inside);
            using (AntiAlias aa = new AntiAlias(context.Graphics))
            {
                using (GraphicsPath borderPath = new GraphicsPath(),
                                    insidePath = new GraphicsPath())
                {
                    // Generate the graphics paths for the border and the inside area which is just inside the border
                    Rectangle rect = dragData.RectMiddle;
                    Rectangle rectInside = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);
                    DrawDragDockingMiddleLines(borderPath, dragData.RectMiddle, 13);
                    DrawDragDockingMiddleLines(insidePath, rectInside, 9);

                    // Fill the entire border area
                    using (SolidBrush borderBrush = new SolidBrush(Color.FromArgb(196, Color.White)))
                        context.Graphics.FillPath(borderBrush, borderPath);

                    // Fill with gradient the area inside the border
                    RectangleF rectBoundsF = new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2);
                    using (LinearGradientBrush insideBrush = new LinearGradientBrush(rectBoundsF, Color.FromArgb(196, Color.White), insideColor, 90))
                    {
                        insideBrush.Blend = _dragRoundedInsideBlend;
                        context.Graphics.FillPath(insideBrush, insidePath);
                    }

                    using (Pen borderPen = new Pen(borderColor))
                    {
                        // Finally draw the actual border
                        context.Graphics.DrawPath(borderPen, borderPath);

                        // Draw the two extra tabs
                        context.Graphics.DrawLine(borderPen, new Point(rect.Right - 2, rect.Bottom - 3), new Point(rect.Right - 2, rect.Bottom - 2));
                        context.Graphics.DrawLine(borderPen, new Point(rect.Right - 10, rect.Bottom - 3), new Point(rect.Right - 10, rect.Bottom - 2));
                        context.Graphics.DrawLine(borderPen, new Point(rect.Right - 3, rect.Bottom - 1), new Point(rect.X + 9, rect.Bottom - 1));
                    }
                }
            }
        }

        private void DrawDragDockingMiddleLines(GraphicsPath path, Rectangle rect, int tabExtend)
        {
            path.AddLines(new Point[] { new Point(rect.X, rect.Bottom - 2),
                                                  new Point(rect.X, rect.Y + 1),
                                                  new Point(rect.X + 1, rect.Y),
                                                  new Point(rect.Right - 1, rect.Y),
                                                  new Point(rect.Right, rect.Y + 1),
                                                  new Point(rect.Right, rect.Bottom - 6),
                                                  new Point(rect.Right - 2, rect.Bottom - 4),
                                                  new Point(rect.X + tabExtend, rect.Bottom - 4),
                                                  new Point(rect.X + tabExtend, rect.Bottom - 2),
                                                  new Point(rect.X + tabExtend - 1, rect.Bottom - 1),
                                                  new Point(rect.X + 1, rect.Bottom - 1),
                                                  new Point(rect.X, rect.Bottom - 2)});
        }

        private void DrawDragDockingRoundedRect(RenderContext context,
                                                Color inside,
                                                Color border,
                                                Rectangle drawRect,
                                                int rounding)
        {
            using (AntiAlias aa = new AntiAlias(context.Graphics))
            {
                RectangleF rectBoundsF = new RectangleF(drawRect.X - 1, drawRect.Y - 1, drawRect.Width + 2, drawRect.Height + 1);
                Rectangle rectInside = new Rectangle(drawRect.X + 2, drawRect.Y + 2, drawRect.Width - 4, drawRect.Height - 4);
                using (GraphicsPath borderPath = CreateBorderBackPath(true, true, drawRect, PaletteDrawBorders.All, 1, rounding, true, 0),
                                    insidePath = CreateBorderBackPath(true, true, rectInside, PaletteDrawBorders.All, 1, rounding - 1, true, 0))
                {
                    using(SolidBrush borderBrush = new SolidBrush(Color.FromArgb(196, Color.White)))
                        context.Graphics.FillPath(borderBrush, borderPath);

                    using (LinearGradientBrush insideBrush = new LinearGradientBrush(rectBoundsF, Color.FromArgb(196, Color.White), inside, 90))
                    {
                        insideBrush.Blend = _dragRoundedInsideBlend;
                        context.Graphics.FillPath(insideBrush, insidePath);
                    }

                    using (Pen borderPen = new Pen(border))
                        context.Graphics.DrawPath(borderPen, borderPath);
                }
            }
        }

        private void DrawDragDockingArrow(RenderContext context,
                                          Color active,
                                          Rectangle rect,
                                          VisualOrientation orientation)
        {
            using (GraphicsPath innerPath = new GraphicsPath())
            {
                float angle = 0f;
                switch (orientation)
                {
                    case VisualOrientation.Left:
                        rect = new Rectangle(rect.Right - _dragArrowHeight - _dragArrowGap,
                                             rect.Y + (rect.Height - _dragArrowWidth) / 2,
                                             _dragArrowHeight, _dragArrowWidth);

                        innerPath.AddLines(new Point[] { new Point(rect.X + 1, rect.Top + 6),
                                                         new Point(rect.Right - 1, rect.Top + 1), 
                                                         new Point(rect.Right - 1, rect.Bottom - 2)});
                        break;
                    case VisualOrientation.Right:
                        rect = new Rectangle(rect.Left + _dragArrowGap,
                                             rect.Y + (rect.Height - _dragArrowWidth) / 2,
                                             _dragArrowHeight, _dragArrowWidth);

                        innerPath.AddLines(new Point[] { new Point(rect.X + 1, rect.Top + 1), 
                                                         new Point(rect.X + 1, rect.Bottom - 2), 
                                                         new Point(rect.Right - 1, rect.Top + 6) });
                        angle = 180f;
                        break;
                    case VisualOrientation.Top:
                        rect = new Rectangle(rect.X + (rect.Width - _dragArrowWidth) / 2,
                                             rect.Bottom - _dragArrowHeight - _dragArrowGap - 1,
                                             _dragArrowWidth, _dragArrowHeight);

                        innerPath.AddLines(new Point[] { new Point(rect.X + 1, rect.Bottom), 
                                                         new Point(rect.Right - 1, rect.Bottom), 
                                                         new Point(rect.X + 6, rect.Top + 1) });
                        angle = 90f;
                        break;
                    case VisualOrientation.Bottom:
                        rect = new Rectangle(rect.X + (rect.Width - _dragArrowWidth) / 2,
                                             rect.Top + _dragArrowGap,
                                             _dragArrowWidth, _dragArrowHeight);

                        innerPath.AddLines(new Point[] { new Point(rect.X + 2, rect.Top + 1), 
                                                         new Point(rect.Right - 2, rect.Top + 1), 
                                                         new Point(rect.X + 6, rect.Bottom - 1) });
                        angle = 270f;
                        break;
                }

                // Draw background in white top highlight the arrow
                using(AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.FillPath(Brushes.White, innerPath);

                // Draw the actual arrow itself
                using(LinearGradientBrush innerBrush = new LinearGradientBrush(rect, ControlPaint.Dark(active), ControlPaint.Light(active), angle))
                    context.Graphics.FillPath(innerBrush, innerPath);
            }
        }

        private void DrawDragDockingSquares(RenderContext context,
                                            RenderDragDockingData dragData,
                                            IPaletteDragDrop dragDropPalette)
        {
            Color back = dragDropPalette.GetDragDropDockBack();
            Color border = dragDropPalette.GetDragDropDockBorder();
            Color active = dragDropPalette.GetDragDropDockActive();
            Color inactive = dragDropPalette.GetDragDropDockInactive();

            DrawDragDockingSquaresBackground(context.Graphics, back, border, dragData);
            if (dragData.ShowLeft) DrawDragDockingSquaresLeft(context.Graphics, active, inactive, dragData);
            if (dragData.ShowRight) DrawDragDockingSquaresRight(context.Graphics, active, inactive, dragData);
            if (dragData.ShowTop) DrawDragDockingSquaresTop(context.Graphics, active, inactive, dragData);
            if (dragData.ShowBottom) DrawDragDockingSquaresBottom(context.Graphics, active, inactive, dragData);
            if (dragData.ShowMiddle) DrawDragDockingSquaresMiddle(context.Graphics, active, inactive, dragData);
        }

        private void DrawDragDockingSquaresBackground(Graphics g, 
                                                      Color inside, 
                                                      Color border,
                                                      RenderDragDockingData dragData)  
        {
            Color start = Color.FromArgb(190, 190, 190);
            using (Pen borderPen = new Pen(border))
            {
                using (SolidBrush insideBrush = new SolidBrush(inside))
                {
                    using (LinearGradientBrush gradientLL = new LinearGradientBrush(new Rectangle(-1, -1, 5, 5), start, inside, 0f),
                                               gradientTL = new LinearGradientBrush(new Rectangle(-1, 23, 5, 5), start, inside, 90f),
                                               gradientCC = new LinearGradientBrush(new Rectangle(24, 25, 5, 5), start, inside, 45f),
                                               gradientLT = new LinearGradientBrush(new Rectangle(28, -1, 5, 5), start, inside, 0f),
                                               gradientML = new LinearGradientBrush(new Rectangle(22, -1, 5, 5), start, inside, 0f),
                                               gradientMT = new LinearGradientBrush(new Rectangle(-1, 22, 5, 5), start, inside, 90f),
                                               gradientTT = new LinearGradientBrush(new Rectangle(-1, -1, 5, 5), start, inside, 90f))
                    {
                        // Draw all the background cross?
                        if (dragData.ShowBack)
                        {
                            // Create points for a polygon
                            Point[] pts = new Point[]{new Point(0,  29), new Point(23, 29),
													  new Point(29, 23), new Point(29, 0),
													  new Point(57, 0),  new Point(57, 23),
													  new Point(63, 29), new Point(87, 29),
													  new Point(87, 57), new Point(63, 57),
													  new Point(57, 63), new Point(57, 87),
													  new Point(29, 87), new Point(29, 63),
													  new Point(23, 57), new Point(0,  57)};

                            // Fill this area with a solid colour
                            g.FillPolygon(insideBrush, pts);

                            // Draw shadow at some of the box edges
                            g.FillPolygon(gradientLL, new Point[] { new Point(1, 57), new Point(1, 30), new Point(4, 33), new Point(4, 57) });
                            g.FillPolygon(gradientTL, new Point[] { new Point(1, 30), new Point(25, 30), new Point(27, 33), new Point(3, 33) });
                            g.FillPolygon(gradientCC, new Point[] { new Point(23, 30), new Point(30, 23), new Point(33, 26), new Point(26, 33) });
                            g.FillPolygon(gradientLT, new Point[] { new Point(30, 1), new Point(30, 24), new Point(33, 26), new Point(33, 4) });
                            g.FillPolygon(gradientTT, new Point[] { new Point(30, 1), new Point(57, 1), new Point(57, 4), new Point(33, 4) });
                            g.FillPolygon(gradientLT, new Point[] { new Point(30, 63), new Point(30, 87), new Point(33, 87), new Point(33, 66) });
                            g.FillPolygon(gradientTL, new Point[] { new Point(63, 30), new Point(87, 30), new Point(87, 33), new Point(66, 33) });

                            // Draw outline in darker colour
                            g.DrawPolygon(borderPen, pts);
                        }
                        else if (dragData.ShowLeft && dragData.ShowRight)
                        {
                            // Create points for a polygon
                            Point[] pts = new Point[]{new Point(0,  29), new Point(23, 29),
													  new Point(29, 23), new Point(57, 23),
													  new Point(63, 29), new Point(87, 29),
													  new Point(87, 57), new Point(63, 57),
													  new Point(57, 63), new Point(29, 63),
													  new Point(23, 57), new Point(0,  57)};

                            // Fill this area with a solid colour
                            g.FillPolygon(insideBrush, pts);

                            // Draw shadow at some of the box edges
                            g.FillPolygon(gradientLL, new Point[] { new Point(1, 57), new Point(1, 30), new Point(4, 33), new Point(4, 57) });
                            g.FillPolygon(gradientTL, new Point[] { new Point(1, 30), new Point(25, 30), new Point(27, 33), new Point(3, 33) });
                            g.FillPolygon(gradientCC, new Point[] { new Point(23, 30), new Point(30, 23), new Point(33, 26), new Point(26, 33) });
                            g.FillPolygon(gradientMT, new Point[] { new Point(30, 24), new Point(57, 24), new Point(60, 27), new Point(33, 27) });
                            g.FillPolygon(gradientTL, new Point[] { new Point(63, 30), new Point(87, 30), new Point(87, 33), new Point(66, 33) });

                            // Draw outline in darker colour
                            g.DrawPolygon(borderPen, pts);
                        }
                        else if (dragData.ShowLeft)
                        {
                            // Only draw the background for the left square
                            g.FillRectangle(insideBrush, 0, 29, 31, 28);
                            g.DrawRectangle(borderPen, 0, 29, 31, 28);
                        }
                        else if (dragData.ShowRight)
                        {
                            // Only draw the background for the right square
                            g.FillRectangle(insideBrush, 56, 29, 31, 28);
                            g.DrawRectangle(borderPen, 56, 29, 31, 28);
                        }
                        else if (dragData.ShowTop && dragData.ShowBottom)
                        {
                            // Create points for a polygon
                            Point[] pts = new Point[]{new Point(23, 29), new Point(29, 23), 
													  new Point(29, 0),  new Point(57, 0),  
													  new Point(57, 23), new Point(63, 29), 
													  new Point(63, 57), new Point(57, 63), 
													  new Point(57, 87), new Point(29, 87), 
													  new Point(29, 63), new Point(23, 57)};

                            // Fill this area with a solid colour
                            g.FillPolygon(insideBrush, pts);

                            g.FillPolygon(gradientLT, new Point[] { new Point(30, 1), new Point(30, 24), new Point(33, 26), new Point(33, 4) });
                            g.FillPolygon(gradientTT, new Point[] { new Point(30, 1), new Point(57, 1), new Point(57, 4), new Point(33, 4) });
                            g.FillPolygon(gradientCC, new Point[] { new Point(23, 30), new Point(30, 23), new Point(33, 26), new Point(26, 33) });
                            g.FillPolygon(gradientML, new Point[] { new Point(24, 57), new Point(24, 30), new Point(27, 33), new Point(27, 60) });
                            g.FillPolygon(gradientLT, new Point[] { new Point(30, 63), new Point(30, 87), new Point(33, 87), new Point(33, 66) });

                            // Draw outline in darker colour
                            g.DrawPolygon(borderPen, pts);
                        }
                        else if (dragData.ShowTop)
                        {
                            // Only draw the background for the top square
                            g.FillRectangle(insideBrush, 29, 0, 28, 31);
                            g.DrawRectangle(borderPen, 29, 0, 28, 31);
                        }
                        else if (dragData.ShowBottom)
                        {
                            // Only draw the background for the bottom square
                            g.FillRectangle(insideBrush, 29, 56, 28, 31);
                            g.DrawRectangle(borderPen, 29, 56, 28, 31);
                        }
                        else if (dragData.ShowMiddle)
                        {
                            // Only draw the background for the middle square
                            Point[] pts = new Point[]{new Point(23, 29), new Point(29, 23), 
													  new Point(57, 23), new Point(63, 29), 
													  new Point(63, 57), new Point(57, 63),
													  new Point(29, 63), new Point(23, 57)};

                            g.FillPolygon(insideBrush, pts);
                            g.DrawPolygon(borderPen, pts);
                        }
                    }
                }
            }
        }

        private void DrawDragDockingSquaresLeft(Graphics g,
                                                Color activeColor,
                                                Color inactiveColor,
                                                RenderDragDockingData dragData)
        {
            Color borderColour = ControlPaint.Dark(activeColor);

            // Draw border around the window square
            using (Pen borderPen = new Pen(borderColour),
                       dashPen = new Pen(borderColour),
                       shadow1Pen = new Pen(_190),
                       shadow2Pen = new Pen(_218))
            {
                // Draw the caption area at top of window
                using (LinearGradientBrush middleBrush = new LinearGradientBrush(new Rectangle(4, 33, 23, 1), ControlPaint.LightLight(inactiveColor), activeColor, 0f),
                                           bottomBrush = new LinearGradientBrush(new Rectangle(4, 34, 23, 1), ControlPaint.Light(activeColor), activeColor, 0f),
                                           positionBrush = new LinearGradientBrush(new Rectangle(4, 35, 11, 1), Color.FromArgb(160, inactiveColor), Color.FromArgb(64, inactiveColor), 0f),
                                           arrowBrush = new LinearGradientBrush(new Rectangle(18, 40, 5, 8), borderColour, Color.FromArgb(175, borderColour), 0f))
                {
                    // Draw border
                    g.DrawLine(borderPen, 4, 33, 4, 53);
                    g.DrawLine(borderPen, 27, 33, 27, 53);
                    g.DrawLine(borderPen, 4, 53, 27, 53);
                    g.DrawLine(borderPen, 4, 33, 27, 33);

                    // Draw shadows around right and bottom edges
                    g.DrawLine(shadow1Pen, 5, 54, 28, 54);
                    g.DrawLine(shadow1Pen, 28, 34, 28, 54);
                    g.DrawLine(shadow2Pen, 6, 55, 29, 55);
                    g.DrawLine(shadow2Pen, 29, 35, 29, 55);

                    // Draw the caption area
                    g.FillRectangle(middleBrush, 5, 34, 22, 1);
                    g.FillRectangle(bottomBrush, 5, 35, 22, 1);

                    // Draw client area
                    g.FillRectangle(SystemBrushes.Window, 5, 36, 22, 17);

                    // Draw docking edge indicator
                    g.FillRectangle(positionBrush, 5, 36, 11, 17);

                    // Draw a dashed line down the middle
                    dashPen.DashStyle = DashStyle.Dot;
                    g.DrawLine(dashPen, 15, 37, 15, 52);

                    // Draw the direction arrow
                    g.FillPolygon(arrowBrush, new Point[] { new Point(19, 44), new Point(23, 40), new Point(23, 48), new Point(19, 44) });

                    // If active, then draw highlighted border
                    if (dragData.ActiveLeft)
                    {
                        g.DrawLine(borderPen, 0, 29, 23, 29);
                        g.DrawLine(borderPen, 0, 57, 23, 57);
                        g.DrawLine(borderPen, 0, 29, 0, 57);
                    }
                }
            }
        }

        private void DrawDragDockingSquaresRight(Graphics g,
                                                 Color activeColor,
                                                 Color inactiveColor,
                                                 RenderDragDockingData dragData)
        {
            Color borderColour = ControlPaint.Dark(activeColor);

            // Draw border around the window square
            using (Pen borderPen = new Pen(borderColour),
                       dashPen = new Pen(borderColour),
                       shadow1Pen = new Pen(_190),
                       shadow2Pen = new Pen(_218))
            {
                // Draw the caption area at top of window
                using (LinearGradientBrush middleBrush = new LinearGradientBrush(new Rectangle(60, 33, 23, 1), ControlPaint.LightLight(inactiveColor), activeColor, 0f),
                                           bottomBrush = new LinearGradientBrush(new Rectangle(60, 34, 23, 1), ControlPaint.Light(activeColor), activeColor, 0f),
                                           positionBrush = new LinearGradientBrush(new Rectangle(71, 35, 11, 1), Color.FromArgb(160, inactiveColor), Color.FromArgb(64, inactiveColor), 180f),
                                           arrowBrush = new LinearGradientBrush(new Rectangle(68, 40, 5, 8), borderColour, Color.FromArgb(175, borderColour), 180f))
                {
                    // Draw border
                    g.DrawLine(borderPen, 60, 33, 60, 53);
                    g.DrawLine(borderPen, 83, 33, 83, 53);
                    g.DrawLine(borderPen, 60, 53, 83, 53);
                    g.DrawLine(borderPen, 60, 33, 83, 33);

                    // Draw shadows around right and bottom edges
                    g.DrawLine(shadow1Pen, 61, 54, 84, 54);
                    g.DrawLine(shadow1Pen, 84, 34, 84, 54);
                    g.DrawLine(shadow2Pen, 62, 55, 85, 55);
                    g.DrawLine(shadow2Pen, 85, 35, 85, 55);

                    // Draw the caption area
                    g.FillRectangle(middleBrush, 61, 34, 22, 1);
                    g.FillRectangle(bottomBrush, 61, 35, 22, 1);

                    // Draw client area
                    g.FillRectangle(SystemBrushes.Window, 61, 36, 22, 17);

                    // Draw docking edge indicator
                    g.FillRectangle(positionBrush, 72, 36, 11, 17);

                    // Draw a dashed line down the middle
                    dashPen.DashStyle = DashStyle.Dot;
                    g.DrawLine(dashPen, 72, 37, 72, 52);

                    // Draw the direction arrow
                    g.FillPolygon(arrowBrush, new Point[] { new Point(69, 44), new Point(65, 40), new Point(65, 48), new Point(69, 44) });

                    // If active, then draw highlighted border
                    if (dragData.ActiveRight)
                    {
                        g.DrawLine(borderPen, 87, 29, 63, 29);
                        g.DrawLine(borderPen, 87, 57, 63, 57);
                        g.DrawLine(borderPen, 87, 29, 87, 57);
                    }
                }
            }
        }

        private void DrawDragDockingSquaresTop(Graphics g,
                                               Color activeColor,
                                               Color inactiveColor,
                                               RenderDragDockingData dragData)
        {
            Color borderColour = ControlPaint.Dark(activeColor);

            // Draw border around the window square
            using (Pen borderPen = new Pen(borderColour),
                       dashPen = new Pen(borderColour),
                       shadow1Pen = new Pen(_190),
                       shadow2Pen = new Pen(_218))
            {
                // Draw the caption area at top of window
                using (LinearGradientBrush middleBrush = new LinearGradientBrush(new Rectangle(33, 5, 20, 1), ControlPaint.LightLight(inactiveColor), activeColor, 0f),
                                           bottomBrush = new LinearGradientBrush(new Rectangle(33, 6, 20, 1), ControlPaint.Light(activeColor), activeColor, 0f),
                                           positionBrush = new LinearGradientBrush(new Rectangle(34, 6, 19, 10), Color.FromArgb(160, inactiveColor), Color.FromArgb(64, inactiveColor), 90f),
                                           arrowBrush = new LinearGradientBrush(new Rectangle(39, 40, 8, 4), borderColour, Color.FromArgb(175, borderColour), 90f))
                {
                    // Draw border
                    g.DrawLine(borderPen, 33, 4, 53, 4);
                    g.DrawLine(borderPen, 53, 4, 53, 27);
                    g.DrawLine(borderPen, 53, 27, 33, 27);
                    g.DrawLine(borderPen, 33, 27, 33, 4);

                    // Draw shadows around right and bottom edges
                    g.DrawLine(shadow1Pen, 34, 28, 54, 28);
                    g.DrawLine(shadow1Pen, 54, 5, 54, 28);
                    g.DrawLine(shadow2Pen, 35, 29, 55, 29);
                    g.DrawLine(shadow2Pen, 55, 6, 55, 29);

                    // Draw the caption area
                    g.FillRectangle(middleBrush, 34, 5, 19, 1);
                    g.FillRectangle(bottomBrush, 34, 6, 19, 1);

                    // Draw client area
                    g.FillRectangle(SystemBrushes.Window, 34, 7, 19, 20);

                    // Draw docking edge indicator
                    g.FillRectangle(positionBrush, 34, 7, 19, 9);

                    // Draw a dashed line down the middle
                    dashPen.DashStyle = DashStyle.Dot;
                    g.DrawLine(dashPen, 35, 15, 53, 15);

                    // Draw the direction arrow
                    g.FillPolygon(arrowBrush, new Point[] { new Point(43, 18), new Point(47, 23), new Point(39, 23), new Point(43, 18) });

                    // If active, then draw highlighted border
                    if (dragData.ActiveTop)
                    {
                        g.DrawLine(borderPen, 29, 0, 29, 23);
                        g.DrawLine(borderPen, 57, 0, 57, 23);
                        g.DrawLine(borderPen, 29, 0, 57, 0);
                    }
                }
            }
        }

        private void DrawDragDockingSquaresBottom(Graphics g,
                                                  Color activeColor,
                                                  Color inactiveColor,
                                                  RenderDragDockingData dragData)
        {
            Color borderColour = ControlPaint.Dark(activeColor);

            // Draw border around the window square
            using (Pen borderPen = new Pen(borderColour),
                       dashPen = new Pen(borderColour),
                       shadow1Pen = new Pen(_190),
                       shadow2Pen = new Pen(_218))
            {
                // Draw the caption area at top of window
                using (LinearGradientBrush middleBrush = new LinearGradientBrush(new Rectangle(33, 61, 20, 1), ControlPaint.LightLight(inactiveColor), activeColor, 0f),
                                           bottomBrush = new LinearGradientBrush(new Rectangle(33, 62, 20, 1), ControlPaint.Light(activeColor), activeColor, 0f),
                                           positionBrush = new LinearGradientBrush(new Rectangle(34, 72, 19, 11), Color.FromArgb(160, inactiveColor), Color.FromArgb(64, inactiveColor), 270f),
                                           arrowBrush = new LinearGradientBrush(new Rectangle(39, 66, 8, 4), borderColour, Color.FromArgb(175, borderColour), 270f))
                {
                    // Draw border
                    g.DrawLine(borderPen, 33, 60, 53, 60);
                    g.DrawLine(borderPen, 53, 60, 53, 83);
                    g.DrawLine(borderPen, 53, 83, 33, 83);
                    g.DrawLine(borderPen, 33, 83, 33, 60);

                    // Draw shadows around right and bottom edges
                    g.DrawLine(shadow1Pen, 34, 84, 54, 84);
                    g.DrawLine(shadow1Pen, 54, 61, 54, 84);
                    g.DrawLine(shadow2Pen, 35, 85, 55, 85);
                    g.DrawLine(shadow2Pen, 55, 61, 55, 85);

                    // Draw the caption area
                    g.FillRectangle(middleBrush, 34, 61, 19, 1);
                    g.FillRectangle(bottomBrush, 34, 62, 19, 1);

                    // Draw client area
                    g.FillRectangle(SystemBrushes.Window, 34, 63, 19, 20);

                    // Draw docking edge indicator
                    g.FillRectangle(positionBrush, 34, 73, 19, 10);

                    // Draw a dashed line down the middle
                    dashPen.DashStyle = DashStyle.Dot;
                    g.DrawLine(dashPen, 35, 73, 53, 73);

                    // Draw the direction arrow
                    g.FillPolygon(arrowBrush, new Point[] { new Point(43, 71), new Point(47, 67), new Point(40, 67), new Point(43, 71) });

                    // If active, then draw highlighted border
                    if (dragData.ActiveBottom)
                    {
                        g.DrawLine(borderPen, 29, 63, 29, 87);
                        g.DrawLine(borderPen, 57, 63, 57, 87);
                        g.DrawLine(borderPen, 29, 87, 57, 87);
                    }
                }
            }
        }

        private void DrawDragDockingSquaresMiddle(Graphics g,
                                                  Color activeColor,
                                                  Color inactiveColor,
                                                  RenderDragDockingData dragData)
        {
            Color borderColour = ControlPaint.Dark(activeColor);

            // Draw border around the window square
            using (Pen borderPen = new Pen(borderColour),
                       dashPen = new Pen(borderColour),
                       shadow1Pen = new Pen(_190),
                       shadow2Pen = new Pen(_218))
            {
                // Draw the caption area at top of window
                using (LinearGradientBrush middleBrush = new LinearGradientBrush(new Rectangle(32, 34, 21, 1), ControlPaint.LightLight(inactiveColor), activeColor, 0f),
                                           bottomBrush = new LinearGradientBrush(new Rectangle(32, 35, 21, 1), ControlPaint.Light(activeColor), activeColor, 0f))
                {
                    // Draw border
                    g.DrawLine(borderPen, 32, 32, 54, 32);
                    g.DrawLine(borderPen, 32, 32, 32, 53);
                    g.DrawLine(borderPen, 32, 53, 33, 54);
                    g.DrawLine(borderPen, 33, 54, 41, 54);
                    g.DrawLine(borderPen, 41, 54, 42, 52);
                    g.DrawLine(borderPen, 42, 52, 42, 50);
                    g.DrawLine(borderPen, 42, 50, 54, 50);
                    g.DrawLine(borderPen, 54, 32, 54, 53);
                    g.DrawLine(borderPen, 54, 53, 53, 54);
                    g.DrawLine(borderPen, 53, 54, 49, 54);
                    g.DrawLine(borderPen, 49, 54, 48, 53);
                    g.DrawLine(borderPen, 48, 53, 48, 50);
                    g.DrawLine(borderPen, 48, 53, 47, 54);
                    g.DrawLine(borderPen, 47, 54, 43, 54);
                    g.DrawLine(borderPen, 43, 54, 42, 53);

                    // Draw the caption area
                    g.FillRectangle(middleBrush, 33, 33, 21, 1);
                    g.FillRectangle(bottomBrush, 33, 34, 21, 1);

                    // Draw the client area
                    g.FillRectangle(SystemBrushes.Window, 33, 35, 21, 15);
                    g.FillRectangle(SystemBrushes.Window, 33, 50, 9, 3);
                    g.FillRectangle(SystemBrushes.Window, 33, 53, 9, 1);
                    g.FillRectangle(SystemBrushes.Window, 43, 51, 5, 3);
                    g.FillRectangle(SystemBrushes.Window, 49, 51, 5, 3);

                    // Fill the inner indicator area
                    using (SolidBrush innerBrush = new SolidBrush(Color.FromArgb(64, inactiveColor)))
                    {
                        g.FillRectangle(innerBrush, 34, 36, 19, 13);
                        g.FillRectangle(innerBrush, 34, 49, 7, 3);
                        g.FillRectangle(innerBrush, 35, 52, 5, 1);
                    }

                    // Draw outline of the indicator area
                    dashPen.DashStyle = DashStyle.Dot;
                    g.DrawLine(dashPen, 34, 37, 34, 52);
                    g.DrawLine(dashPen, 35, 52, 40, 52);
                    g.DrawLine(dashPen, 40, 51, 40, 49);
                    g.DrawLine(dashPen, 40, 51, 40, 48);
                    g.DrawLine(dashPen, 41, 48, 53, 48);
                    g.DrawLine(dashPen, 52, 47, 52, 36);
                    g.DrawLine(dashPen, 35, 36, 52, 36);

                    // Draw right han side shadow
                    g.DrawLine(shadow1Pen, 55, 33, 55, 53);
                    g.DrawLine(shadow2Pen, 56, 34, 56, 53);
                    g.DrawLine(shadow1Pen, 33, 55, 53, 55);
                    g.DrawLine(shadow1Pen, 53, 55, 55, 53);
                    g.DrawLine(shadow2Pen, 34, 56, 53, 56);
                    g.DrawLine(shadow2Pen, 53, 56, 56, 53);
                }

                // If active, then draw highlighted border
                if (dragData.ActiveMiddle)
                {
                    g.DrawLine(borderPen, 23, 29, 29, 23);
                    g.DrawLine(borderPen, 57, 23, 63, 29);
                    g.DrawLine(borderPen, 63, 57, 57, 63);
                    g.DrawLine(borderPen, 23, 57, 29, 63);
                }
            }
        }
        #endregion

        #region Implementation Ribbon
        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupAreaBorder1And2(RenderContext context, 
                                                                     Rectangle rect,
                                                                     PaletteState state,
                                                                     IPaletteRibbonBack palette,
                                                                     bool limited,
                                                                     bool fading,
                                                                     IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                if (fading)
                    c5 = Color.FromArgb(146, c5);

                bool generate = true;
                MementoRibbonGroupAreaBorder cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupAreaBorder))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupAreaBorder(rect, c1, c2, c3, c4, c5);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupAreaBorder)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, c5);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    GraphicsPath outsidePath = new GraphicsPath();
                    GraphicsPath insidePathN = new GraphicsPath();
                    GraphicsPath insidePathL = new GraphicsPath();
                    GraphicsPath shadowPath = new GraphicsPath();

                    // Create path for the entire border
                    outsidePath.AddLine(rect.Left + 2, rect.Top, rect.Right - 3, rect.Top);
                    outsidePath.AddLine(rect.Right - 3, rect.Top, rect.Right - 1, rect.Top + 2);
                    outsidePath.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 3);
                    outsidePath.AddLine(rect.Right - 1, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 1);
                    outsidePath.AddLine(rect.Right - 3, rect.Bottom - 1, rect.Left + 2, rect.Bottom - 1);
                    outsidePath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Left, rect.Bottom - 3);
                    outsidePath.AddLine(rect.Left, rect.Bottom - 3, rect.Left, rect.Top + 2);
                    outsidePath.AddLine(rect.Left, rect.Top + 2, rect.Left + 2, rect.Top);

                    // Create the path for the inside highlight
                    insidePathL.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Right - 3, rect.Bottom - 2);
                    insidePathN.AddLine(rect.Left + 1, rect.Top + 3, rect.Left + 1, rect.Bottom - 3);
                    insidePathN.AddLine(rect.Left + 1, rect.Bottom - 3, rect.Left + 2, rect.Bottom - 2);
                    insidePathN.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Right - 3, rect.Bottom - 2);
                    insidePathN.AddLine(rect.Right - 3, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 3);
                    insidePathN.AddLine(rect.Right - 2, rect.Bottom - 3, rect.Right - 2, rect.Top + 3);

                    // Create the path for the outside shadow
                    shadowPath.AddLine(rect.Left, rect.Bottom - 2, rect.Left + 2, rect.Bottom);
                    shadowPath.AddLine(rect.Left + 2, rect.Bottom, rect.Right - 3, rect.Bottom);
                    shadowPath.AddLine(rect.Right - 4, rect.Bottom, rect.Right, rect.Bottom - 3);
                    shadowPath.AddLine(rect.Right, rect.Bottom - 3, rect.Right, rect.Top + 3);

                    LinearGradientBrush insideBrush = new LinearGradientBrush(rect, Color.Transparent, c2, 95f);
                    cache.insidePen = new Pen(insideBrush);

                    Rectangle rectGradient = new Rectangle(rect.Left - 1, rect.Top, rect.Width + 2, rect.Height + 1);
                    LinearGradientBrush shadowBrushN = new LinearGradientBrush(rectGradient, _darken8, _darken38, 90f);
                    LinearGradientBrush shadowBrushL = new LinearGradientBrush(rectGradient, _darken8, _darken18, 90f);
                    cache.shadowPenN = new Pen(shadowBrushN);
                    cache.shadowPenL = new Pen(shadowBrushL);

                    cache.insidePathN = insidePathN;
                    cache.insidePathL = insidePathL;
                    cache.fillBrush = new LinearGradientBrush(rect, c3, c4, 90f);
                    cache.fillBrush.Blend = _ribbonGroup1Blend;
                    cache.fillTopBrush = new LinearGradientBrush(rect, c5, Color.Transparent, 90f);
                    cache.fillTopBrush.Blend = _ribbonGroup2Blend;
                    cache.outsidePath = outsidePath;
                    cache.shadowPath = shadowPath;
                    cache.outsidePen = new Pen(c1);
                }

                // Fill the inside area with a linear gradient
                context.Graphics.FillPath(cache.fillBrush, cache.outsidePath);

                // Clip drawing to the outside border
                using(Clipping clip = new Clipping(context.Graphics, cache.outsidePath))
                    context.Graphics.FillPath(cache.fillTopBrush, cache.outsidePath);

                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    // Draw the outside of the entire border line
                    context.Graphics.DrawPath(cache.outsidePen, cache.outsidePath);

                    // Draw the highlighting inside border
                    context.Graphics.DrawPath(cache.insidePen, (limited ? cache.insidePathL : cache.insidePathN));
                }

                Pen shadowMedium = (limited ? _lightShadowPen : _medium2ShadowPen);
                Pen shadowDark = (limited ? _medium2ShadowPen : _darkShadowPen);
                context.Graphics.DrawPath(limited ? cache.shadowPenL : cache.shadowPenN, cache.shadowPath);
                context.Graphics.DrawLine(shadowMedium, rect.Left, rect.Bottom, rect.Left, rect.Bottom - 1);
                context.Graphics.DrawLine(shadowMedium, rect.Left, rect.Bottom - 1, rect.Left + 1, rect.Bottom);
                context.Graphics.DrawLine(shadowDark, rect.Right, rect.Bottom - 2, rect.Right - 2, rect.Bottom);
                context.Graphics.DrawLine(shadowMedium, rect.Right, rect.Bottom - 1, rect.Right - 1, rect.Bottom);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupAreaBorder3And4(RenderContext context,
                                                                     Rectangle rect,
                                                                     PaletteState state,
                                                                     IPaletteRibbonBack palette,
                                                                     IDisposable memento,
                                                                     bool gradientTop)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                bool generate = true;
                MementoRibbonGroupAreaBorder3 cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupAreaBorder3))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupAreaBorder3(rect, c1, c2, c3, c4, c5);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupAreaBorder3)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, c5);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    Rectangle innerRect = rect;
                    innerRect.Height -= 3;
                    int halfHeight = innerRect.Height / 2;
                    cache.borderRect = innerRect;
                    cache.borderPoints = new Point[] { new Point(innerRect.X, rect.Y), new Point(innerRect.X, innerRect.Bottom), new Point(innerRect.Right, innerRect.Bottom), new Point(innerRect.Right, innerRect.Top) };
                    cache.backRect1 = new Rectangle(innerRect.X, innerRect.Y, rect.Width, halfHeight);
                    cache.backRect2 = new Rectangle(innerRect.X, innerRect.Y + halfHeight, innerRect.Width, innerRect.Height - halfHeight);
                    cache.backBrush1 = new LinearGradientBrush(new RectangleF(cache.backRect1.X - 1, cache.backRect1.Y - 1, cache.backRect1.Width + 2, cache.backRect1.Height + 1), c3, c4, 90f);
                    cache.backBrush2 = new LinearGradientBrush(new RectangleF(cache.backRect2.X - 1, cache.backRect2.Y - 1, cache.backRect2.Width + 2, cache.backRect2.Height + 1), c4, c5, 90f);
                    cache.backBrush3 = new SolidBrush(c5);
                    cache.gradientBorderBrush = new LinearGradientBrush(new RectangleF(cache.backRect1.X - 1, cache.backRect1.Y - 1, cache.backRect1.Width + 2, 3), c1, c2, 0f);
                    cache.gradientBorderBrush.Blend = _ribbonGroupArea3;
                    cache.gradientBorderPen = (gradientTop ? new Pen(cache.gradientBorderBrush) :  new Pen(c1));
                    cache.solidBorderPen = new Pen(c2);
                    cache.shadowPen1 = new Pen(CommonHelper.MergeColors(c5, 0.4f, c1, 0.6f));
                    cache.shadowPen2 = new Pen(CommonHelper.MergeColors(c5, 0.25f, c1, 0.75f));
                    cache.shadowPen3 = new Pen(CommonHelper.MergeColors(c5, 0.1f, c1, 0.9f));
                }

                // Draw solid background for entire area
                context.Graphics.FillRectangle(cache.backBrush3, rect);

                // Fill area inside the border with a gradient effect
                context.Graphics.FillRectangle(cache.backBrush1, cache.backRect1);
                context.Graphics.FillRectangle(cache.backBrush2, cache.backRect2);

                // Draw the solid border around the edge
                context.Graphics.DrawLine(cache.gradientBorderPen, cache.borderRect.X, cache.borderRect.Y, cache.borderRect.Right, cache.borderRect.Y);
                context.Graphics.DrawLines(cache.solidBorderPen, cache.borderPoints);

                // Draw shadow lines at bottom
                context.Graphics.DrawLine(cache.shadowPen3, rect.X, rect.Bottom - 2, rect.Right, rect.Bottom - 2);
                context.Graphics.DrawLine(cache.shadowPen2, rect.X, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
                context.Graphics.DrawLine(cache.shadowPen1, rect.X, rect.Bottom, rect.Right, rect.Bottom);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupAreaBorderContext(RenderContext context,
                                                                       Rectangle rect,
                                                                       PaletteState state,
                                                                       IPaletteRibbonBack palette,
                                                                       IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);

                bool generate = true;
                MementoRibbonGroupAreaBorderContext cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupAreaBorderContext))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupAreaBorderContext(rect, c1, c2, c3);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupAreaBorderContext)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    GraphicsPath outsidePath = new GraphicsPath();
                    GraphicsPath insidePath = new GraphicsPath();
                    GraphicsPath shadowPath = new GraphicsPath();

                    // Create path for the entire border
                    outsidePath.AddLine(rect.Left + 2, rect.Top, rect.Right - 3, rect.Top);
                    outsidePath.AddLine(rect.Right - 3, rect.Top, rect.Right - 1, rect.Top + 2);
                    outsidePath.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 3);
                    outsidePath.AddLine(rect.Right - 1, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 1);
                    outsidePath.AddLine(rect.Right - 3, rect.Bottom - 1, rect.Left + 2, rect.Bottom - 1);
                    outsidePath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Left, rect.Bottom - 3);
                    outsidePath.AddLine(rect.Left, rect.Bottom - 3, rect.Left, rect.Top + 2);
                    outsidePath.AddLine(rect.Left, rect.Top + 2, rect.Left + 2, rect.Top);

                    // Create the path for the inside highlight
                    insidePath.AddLine(rect.Left + 1, rect.Top + 3, rect.Left + 1, rect.Bottom - 3);
                    insidePath.AddLine(rect.Left + 1, rect.Bottom - 3, rect.Left + 2, rect.Bottom - 2);
                    insidePath.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Right - 3, rect.Bottom - 2);
                    insidePath.AddLine(rect.Right - 3, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 3);
                    insidePath.AddLine(rect.Right - 2, rect.Bottom - 3, rect.Right - 2, rect.Top + 3);

                    // Create the path for the outside shadow
                    shadowPath.AddLine(rect.Left, rect.Bottom - 2, rect.Left + 2, rect.Bottom);
                    shadowPath.AddLine(rect.Left + 2, rect.Bottom, rect.Right - 3, rect.Bottom);
                    shadowPath.AddLine(rect.Right - 4, rect.Bottom, rect.Right, rect.Bottom - 3);
                    shadowPath.AddLine(rect.Right, rect.Bottom - 3, rect.Right, rect.Top + 3);

                    LinearGradientBrush insideBrush = new LinearGradientBrush(rect, Color.Transparent, c2, 95f);
                    cache.insidePen = new Pen(insideBrush);

                    Rectangle rectGradient = new Rectangle(rect.Left - 1, rect.Top, rect.Width + 2, rect.Height + 1);
                    LinearGradientBrush shadowBrush = new LinearGradientBrush(rectGradient, _darken8, _darken38, 90f);
                    cache.shadowPen = new Pen(shadowBrush);

                    cache.fillBrush = new LinearGradientBrush(rect, Color.White, _242, 90f);
                    cache.fillBrush.Blend = _ribbonGroup3Blend;
                    cache.fillTopBrush = new LinearGradientBrush(rect, Color.FromArgb(75, c3), Color.Transparent, 90f);
                    cache.fillTopBrush.Blend = _ribbonGroup4Blend;
                    cache.outsidePath = outsidePath;
                    cache.insidePath = insidePath;
                    cache.shadowPath = shadowPath;
                    cache.outsidePen = new Pen(c1);
                }

                // Fill the inside area with a linear gradient
                context.Graphics.FillPath(cache.fillBrush, cache.outsidePath);

                // Clip drawing to the outside border
                using (Clipping clip = new Clipping(context.Graphics, cache.outsidePath))
                    context.Graphics.FillPath(cache.fillTopBrush, cache.outsidePath);

                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    // Draw the outside of the entire border line
                    context.Graphics.DrawPath(cache.outsidePen, cache.outsidePath);

                    // Draw the highlighting inside border
                    context.Graphics.DrawPath(cache.insidePen, cache.insidePath);
                }

                // Draw the shadow outside the bottom and right edges
                context.Graphics.DrawPath(cache.shadowPen, cache.shadowPath);
                context.Graphics.DrawLine(_medium2ShadowPen, rect.Left, rect.Bottom, rect.Left, rect.Bottom - 1);
                context.Graphics.DrawLine(_medium2ShadowPen, rect.Left, rect.Bottom - 1, rect.Left + 1, rect.Bottom);
                context.Graphics.DrawLine(_darkShadowPen, rect.Right, rect.Bottom - 2, rect.Right - 2, rect.Bottom);
                context.Graphics.DrawLine(_medium2ShadowPen, rect.Right, rect.Bottom - 1, rect.Right - 1, rect.Bottom);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonTabTracking2007(PaletteRibbonShape shape,
                                                                RenderContext context, 
                                                                Rectangle rect,
                                                                PaletteState state,
                                                                IPaletteRibbonBack palette,
                                                                VisualOrientation orientation,
                                                                IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonTabTracking2007 cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabTracking2007))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabTracking2007(rect, c1, c2, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabTracking2007)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabTrackingTop2007(rect, c1, c2, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabTrackingLeft2007(rect, c1, c2, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabTrackingRight2007(rect, c1, c2, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabTrackingBottom2007(rect, c1, c2, cache);
                            break;
                    }
                }

                // Draw the left and right sides with light version of tracking color
                context.Graphics.FillRectangle(cache.half1LeftBrush, cache.half1Rect);
                context.Graphics.FillRectangle(cache.half1RightBrush, cache.half1Rect);

                // Draw over with glassy effect
                context.Graphics.FillRectangle(cache.half1LightBrush, cache.half1Rect);

                //// Use a solid fill for the bottom half
                context.Graphics.FillRectangle(cache.half2Brush, cache.half2Rect);

                // Cannot draw a path that contains a zero sized element
                if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
                {
                    // Draw twice to get a deeper color effect, once is to pale
                    context.Graphics.FillRectangle(cache.ellipseBrush, cache.half2RectF);
                    context.Graphics.FillRectangle(cache.ellipseBrush, cache.half2RectF);
                }

                // Draw the actual border
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.DrawPath(cache.outsidePen, cache.outsidePath);

                switch (orientation)
                {
                    case VisualOrientation.Top:
                        DrawRibbonTabTrackingTopDraw2007(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Left:
                        DrawRibbonTabTrackingLeftDraw2007(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Right:
                        DrawRibbonTabTrackingRightDraw2007(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Bottom:
                        DrawRibbonTabTrackingBottomDraw2007(rect, cache, context.Graphics);
                        break;
                }

                context.Graphics.DrawPath(cache.topPen, cache.topPath);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingTop2007(Rectangle rect, 
                                                            Color c1, Color c2,
                                                            MementoRibbonTabTracking2007 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath topPath = new GraphicsPath();
            GraphicsPath ellipsePath = new GraphicsPath();
            RectangleF half1RectF, vertRectF, horzRectF;
            int full, half1, half2;
            Rectangle fullRect;

            // Create path for a curved border around the tab
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left + 1, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 1.5f, rect.Left + 3, rect.Top);
            outsidePath.AddLine(rect.Left + 3, rect.Top, rect.Right - 4, rect.Top);
            outsidePath.AddLine(rect.Right - 4, rect.Top, rect.Right - 2, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 1.5f, rect.Right - 2, rect.Bottom - 2);

            // Create path for the top hightlight line
            topPath.AddLine(rect.Left + 3, rect.Top + 2, rect.Left + 4, rect.Top + 1);
            topPath.AddLine(rect.Left + 4, rect.Top + 1, rect.Right - 5, rect.Top + 1);
            topPath.AddLine(rect.Right - 5, rect.Top + 1, rect.Right - 4, rect.Top + 2);

            // Create the top and bottom half rectangles
            full = rect.Height - 3;
            half1 = full / 2;
            half2 = full - half1;
            cache.half1Rect = new Rectangle(rect.Left + 3, rect.Top + 2, rect.Width - 6, half1);
            cache.half2Rect = new Rectangle(rect.Left + 3, rect.Top + 2 + half1, rect.Width - 6, half2);
            fullRect = new Rectangle(rect.Left + 3, rect.Top + 2, rect.Width - 6, half1 + half2);
            half1RectF = new RectangleF(cache.half1Rect.Left - 1, cache.half1Rect.Top - 0.5f, cache.half1Rect.Width + 2, cache.half1Rect.Height + 1);
            cache.half2RectF = new RectangleF(cache.half2Rect.Left - 1, cache.half2Rect.Top - 0.5f, cache.half2Rect.Width + 2, cache.half2Rect.Height + 1);

            cache.half1LeftBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(85, c2), Color.Transparent, 0f);
            cache.half1LeftBrush.Blend = _ribbonTabTopBlend;
            cache.half1RightBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(85, c2), Color.Transparent, 180f);
            cache.half1RightBrush.Blend = _ribbonTabTopBlend;
            cache.half1LightBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(28, Color.White), Color.FromArgb(125, Color.White), 90f);
            cache.half2Brush = new SolidBrush(Color.FromArgb(85, c2));

            // Create ellipse information for lightening the bottom half
            cache.ellipseRect = new RectangleF(fullRect.Left - (fullRect.Width / 8), fullRect.Top, fullRect.Width * 1.25f, fullRect.Height);

            // Cannot draw a path that contains a zero sized element
            if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
            {
                ellipsePath.AddEllipse(cache.ellipseRect);
                cache.ellipseBrush = new PathGradientBrush(ellipsePath);
                cache.ellipseBrush.CenterColor = Color.FromArgb(92, Color.White);
                PointF centerPoint = new PointF(cache.ellipseRect.Left + (cache.ellipseRect.Width / 2), cache.ellipseRect.Top + (cache.ellipseRect.Height / 2));
                cache.ellipseBrush.CenterPoint = centerPoint;
                cache.ellipseBrush.SurroundColors = new Color[] { Color.Transparent };
            }

            vertRectF = new RectangleF(rect.Left - 1, rect.Top + 2, rect.Width + 2, rect.Height - 2);
            horzRectF = new RectangleF(rect.Left + 1, rect.Top, rect.Width - 2, rect.Height);
            cache.outsideBrush = new LinearGradientBrush(vertRectF, Color.Transparent, _whiten128, 90f);
            cache.outsideBrush.Blend = _ribbonOutBlend;
            cache.insideBrush = new LinearGradientBrush(vertRectF, Color.Transparent, _whiten200, 90f);
            cache.insideBrush.Blend = _ribbonInBlend;
            cache.topBrush = new LinearGradientBrush(horzRectF, _whiten92, _whiten128, 0f);
            cache.topBrush.Blend = _ribbonTopBlend;
            cache.topPen = new Pen(cache.topBrush);

            cache.outsidePen = new Pen(c1);
            cache.outsidePath = outsidePath;
            cache.topPath = topPath;
            cache.ellipsePath = ellipsePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingTopDraw2007(Rectangle rect,
                                                                MementoRibbonTabTracking2007 cache,
                                                                Graphics g)
        {
            g.FillRectangle(cache.outsideBrush, rect.Left, rect.Top + 3, 1, rect.Height - 4);
            g.FillRectangle(cache.insideBrush, rect.Left + 2, rect.Top + 3, 1, rect.Height - 4);
            g.FillRectangle(cache.outsideBrush, rect.Right - 1, rect.Top + 3, 1, rect.Height - 4);
            g.FillRectangle(cache.insideBrush, rect.Right - 3, rect.Top + 3, 1, rect.Height - 4);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingLeft2007(Rectangle rect,
                                                         Color c1, Color c2,
                                                         MementoRibbonTabTracking2007 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath topPath = new GraphicsPath();
            GraphicsPath ellipsePath = new GraphicsPath();
            RectangleF half1RectF, vertRectF, horzRectF;
            int full, half1, half2;
            Rectangle fullRect;

            // Create path for a curved border around the tab
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 2, rect.Left + 1.5f, rect.Bottom - 2);
            outsidePath.AddLine(rect.Left + 1.5f, rect.Bottom - 2, rect.Left, rect.Bottom - 4);
            outsidePath.AddLine(rect.Left, rect.Bottom - 4, rect.Left, rect.Top + 3);
            outsidePath.AddLine(rect.Left, rect.Top + 3, rect.Left + 1.5f, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 1.5f, rect.Top + 1, rect.Right - 2, rect.Top + 1);

            // Create path for the top hightlight line
            topPath.AddLine(rect.Left + 2, rect.Bottom - 4, rect.Left + 1, rect.Bottom - 5);
            topPath.AddLine(rect.Left + 1, rect.Bottom - 5, rect.Left + 1, rect.Top + 4);
            topPath.AddLine(rect.Left + 1, rect.Top + 4, rect.Left + 2, rect.Top + 3);

            // Create the top and bottom half rectangles
            full = rect.Width - 3;
            half1 = full / 2;
            half2 = full - half1;
            cache.half1Rect = new Rectangle(rect.Left + 2, rect.Top + 3, half1, rect.Height - 6);
            cache.half2Rect = new Rectangle(rect.Left + 2 + half1, rect.Top + 3, half2, rect.Height - 6);
            fullRect = new Rectangle(rect.Left + 2, rect.Top + 3, half1 + half2, rect.Height - 6);
            half1RectF = new RectangleF(cache.half1Rect.Left - 0.5f, cache.half1Rect.Top - 1f, cache.half1Rect.Width + 1, cache.half1Rect.Height + 2);
            cache.half2RectF = new RectangleF(cache.half2Rect.Left - 0.5f, cache.half2Rect.Top - 1f, cache.half2Rect.Width + 1, cache.half2Rect.Height + 2);

            cache.half1LeftBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(85, c2), Color.Transparent, 90f);
            cache.half1LeftBrush.Blend = _ribbonTabTopBlend;
            cache.half1RightBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(85, c2), Color.Transparent, 270f);
            cache.half1RightBrush.Blend = _ribbonTabTopBlend;
            cache.half1LightBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(28, Color.White), Color.FromArgb(125, Color.White), 180f);
            cache.half2Brush = new SolidBrush(Color.FromArgb(85, c2));

            // Create ellipse information for lightening the bottom hald
            cache.ellipseRect = new RectangleF(fullRect.Left, fullRect.Top - (fullRect.Width / 8), fullRect.Width, fullRect.Height * 1.25f);

            // Cannot draw a path that contains a zero sized element
            if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
            {
                ellipsePath.AddEllipse(cache.ellipseRect);
                cache.ellipseBrush = new PathGradientBrush(ellipsePath);
                cache.ellipseBrush.CenterColor = Color.FromArgb(48, Color.White);
                PointF centerPoint = new PointF(cache.ellipseRect.Left + (cache.ellipseRect.Width / 2), cache.ellipseRect.Top + (cache.ellipseRect.Height / 2));
                cache.ellipseBrush.CenterPoint = centerPoint;
                cache.ellipseBrush.SurroundColors = new Color[] { Color.Transparent };
            }

            vertRectF = new RectangleF(rect.Left + 2, rect.Top - 1, rect.Width - 2, rect.Height + 2);
            horzRectF = new RectangleF(rect.Left, rect.Top + 1, rect.Width, rect.Height - 2);
            cache.outsideBrush = new LinearGradientBrush(vertRectF, Color.Transparent, _whiten128, 180f);
            cache.outsideBrush.Blend = _ribbonOutBlend;
            cache.insideBrush = new LinearGradientBrush(vertRectF, Color.Transparent, _whiten200, 180f);
            cache.insideBrush.Blend = _ribbonInBlend;
            cache.topBrush = new LinearGradientBrush(horzRectF, _whiten92, _whiten128, 90f);
            cache.topBrush.Blend = _ribbonTopBlend;
            cache.topPen = new Pen(cache.topBrush);

            cache.outsidePen = new Pen(c1);
            cache.outsidePath = outsidePath;
            cache.topPath = topPath;
            cache.ellipsePath = ellipsePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingLeftDraw2007(Rectangle rect,
                                                                 MementoRibbonTabTracking2007 cache,
                                                                 Graphics g)
        {
            g.FillRectangle(cache.outsideBrush, rect.Left + 3, rect.Top, rect.Width - 4, 1);
            g.FillRectangle(cache.insideBrush, rect.Left + 3, rect.Top + 2, rect.Width - 4, 1);
            g.FillRectangle(cache.outsideBrush, rect.Left + 3, rect.Bottom - 1, rect.Width - 4, 1);
            g.FillRectangle(cache.insideBrush, rect.Left + 3, rect.Bottom - 3, rect.Width - 4, 1);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingRight2007(Rectangle rect,
                                                              Color c1, Color c2,
                                                              MementoRibbonTabTracking2007 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath topPath = new GraphicsPath();
            GraphicsPath ellipsePath = new GraphicsPath();
            RectangleF half1RectF, vertRectF, horzRectF;
            int full, half1, half2;
            Rectangle fullRect;

            // Create path for a curved border around the tab
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Right - 2.5f, rect.Bottom - 2);
            outsidePath.AddLine(rect.Right - 2.5f, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 4);
            outsidePath.AddLine(rect.Right - 1, rect.Bottom - 4, rect.Right - 1, rect.Top + 3);
            outsidePath.AddLine(rect.Right - 1, rect.Top + 3, rect.Right - 2.5f, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 2.5f, rect.Top + 1, rect.Left + 1, rect.Top + 1);

            // Create path for the top hightlight line
            topPath.AddLine(rect.Right - 3, rect.Bottom - 4, rect.Right - 2, rect.Bottom - 5);
            topPath.AddLine(rect.Right - 2, rect.Bottom - 5, rect.Right - 2, rect.Top + 4);
            topPath.AddLine(rect.Right - 2, rect.Top + 4, rect.Right - 3, rect.Top + 3);

            // Create the top and bottom half rectangles
            full = rect.Width - 3;
            half1 = full / 2;
            half2 = full - half1;
            cache.half1Rect = new Rectangle(rect.Right - 2 - half1, rect.Top + 3, half1, rect.Height - 6);
            cache.half2Rect = new Rectangle(rect.Right - 2 - half1 - half2, rect.Top + 3, half2, rect.Height - 6);
            fullRect = new Rectangle(rect.Right - 2 - half1 - half2, rect.Top + 3, half1 + half2, rect.Height - 6);
            half1RectF = new RectangleF(cache.half1Rect.Left - 0.5f, cache.half1Rect.Top - 1f, cache.half1Rect.Width + 1, cache.half1Rect.Height + 2);
            cache.half2RectF = new RectangleF(cache.half2Rect.Left - 0.5f, cache.half2Rect.Top - 1f, cache.half2Rect.Width + 1, cache.half2Rect.Height + 2);

            cache.half1LeftBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(85, c2), Color.Transparent, 270f);
            cache.half1LeftBrush.Blend = _ribbonTabTopBlend;
            cache.half1RightBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(85, c2), Color.Transparent, 90f);
            cache.half1RightBrush.Blend = _ribbonTabTopBlend;
            cache.half1LightBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(28, Color.White), Color.FromArgb(125, Color.White), 0f);
            cache.half2Brush = new SolidBrush(Color.FromArgb(85, c2));

            // Create ellipse information for lightening the bottom hald
            cache.ellipseRect = new RectangleF(fullRect.Left, fullRect.Top - (fullRect.Width / 8), fullRect.Width, fullRect.Height * 1.25f);

            // Cannot draw a path that contains a zero sized element
            if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
            {
                ellipsePath.AddEllipse(cache.ellipseRect);
                cache.ellipseBrush = new PathGradientBrush(ellipsePath);
                cache.ellipseBrush.CenterColor = Color.FromArgb(48, Color.White);
                PointF centerPoint = new PointF(cache.ellipseRect.Left + (cache.ellipseRect.Width / 2), cache.ellipseRect.Top + (cache.ellipseRect.Height / 2));
                cache.ellipseBrush.CenterPoint = centerPoint;
                cache.ellipseBrush.SurroundColors = new Color[] { Color.Transparent };
            }

            vertRectF = new RectangleF(rect.Left, rect.Top - 1, rect.Width - 2, rect.Height + 2);
            horzRectF = new RectangleF(rect.Left, rect.Top + 1, rect.Width, rect.Height - 2);
            cache.outsideBrush = new LinearGradientBrush(vertRectF, Color.Transparent, _whiten128, 0f);
            cache.outsideBrush.Blend = _ribbonOutBlend;
            cache.insideBrush = new LinearGradientBrush(vertRectF, Color.Transparent, _whiten200, 0f);
            cache.insideBrush.Blend = _ribbonInBlend;
            cache.topBrush = new LinearGradientBrush(horzRectF, _whiten92, _whiten128, 270f);
            cache.topBrush.Blend = _ribbonTopBlend;
            cache.topPen = new Pen(cache.topBrush);

            cache.outsidePen = new Pen(c1);
            cache.outsidePath = outsidePath;
            cache.topPath = topPath;
            cache.ellipsePath = ellipsePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingRightDraw2007(Rectangle rect,
                                                                  MementoRibbonTabTracking2007 cache,
                                                                  Graphics g)
        {
            g.FillRectangle(cache.outsideBrush, rect.Left + 1, rect.Top, rect.Width - 4, 1);
            g.FillRectangle(cache.insideBrush, rect.Left + 1, rect.Top + 2, rect.Width - 4, 1);
            g.FillRectangle(cache.outsideBrush, rect.Left + 1, rect.Bottom - 1, rect.Width - 4, 1);
            g.FillRectangle(cache.insideBrush, rect.Left + 1, rect.Bottom - 3, rect.Width - 4, 1);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingBottom2007(Rectangle rect,
                                                               Color c1, Color c2,
                                                               MementoRibbonTabTracking2007 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath topPath = new GraphicsPath();
            GraphicsPath ellipsePath = new GraphicsPath();
            RectangleF half1RectF, vertRectF, horzRectF;
            int full, half1, half2;
            Rectangle fullRect;

            // Create path for a curved border around the tab
            outsidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 2.5f, rect.Left + 3, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 3, rect.Bottom - 1, rect.Right - 4, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 4, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 2.5f, rect.Right - 2, rect.Top + 1);

            // Create path for the bottom hightlight line
            topPath.AddLine(rect.Left + 3, rect.Bottom - 3, rect.Left + 4, rect.Bottom - 2);
            topPath.AddLine(rect.Left + 4, rect.Bottom - 2, rect.Right - 5, rect.Bottom - 2);
            topPath.AddLine(rect.Right - 5, rect.Bottom - 2, rect.Right - 4, rect.Bottom - 3);

            // Create the top and bottom half rectangles
            full = rect.Height - 3;
            half1 = full / 2;
            half2 = full - half1;
            cache.half1Rect = new Rectangle(rect.Left + 3, rect.Bottom - 2 - half1, rect.Width - 6, half1);
            cache.half2Rect = new Rectangle(rect.Left + 3, rect.Bottom - 2 - half1 - half2, rect.Width - 6, half2);
            fullRect = new Rectangle(rect.Left + 3, rect.Bottom - 2 - half1 - half2, rect.Width - 6, half1 + half2);
            half1RectF = new RectangleF(cache.half1Rect.Left - 1, cache.half1Rect.Top - 0.5f, cache.half1Rect.Width + 2, cache.half1Rect.Height + 1);
            cache.half2RectF = new RectangleF(cache.half2Rect.Left - 1, cache.half2Rect.Top - 0.5f, cache.half2Rect.Width + 2, cache.half2Rect.Height + 1);

            cache.half1LeftBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(85, c2), Color.Transparent, 180f);
            cache.half1LeftBrush.Blend = _ribbonTabTopBlend;
            cache.half1RightBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(85, c2), Color.Transparent, 0f);
            cache.half1RightBrush.Blend = _ribbonTabTopBlend;
            cache.half1LightBrush = new LinearGradientBrush(half1RectF, Color.FromArgb(28, Color.White), Color.FromArgb(125, Color.White), 270f);
            cache.half2Brush = new SolidBrush(Color.FromArgb(85, c2));

            // Create ellipse information for lightening the bottom hald
            cache.ellipseRect = new RectangleF(fullRect.Left - (fullRect.Width / 8), fullRect.Top, fullRect.Width * 1.25f, fullRect.Height);

            // Cannot draw a path that contains a zero sized element
            if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
            {
                ellipsePath.AddEllipse(cache.ellipseRect);
                cache.ellipseBrush = new PathGradientBrush(ellipsePath);
                cache.ellipseBrush.CenterColor = Color.FromArgb(92, Color.White);
                PointF centerPoint = new PointF(cache.ellipseRect.Left + (cache.ellipseRect.Width / 2), cache.ellipseRect.Bottom - (cache.ellipseRect.Height / 2));
                cache.ellipseBrush.CenterPoint = centerPoint;
                cache.ellipseBrush.SurroundColors = new Color[] { Color.Transparent };
            }

            vertRectF = new RectangleF(rect.Left - 1, rect.Top, rect.Width + 2, rect.Height - 2);
            horzRectF = new RectangleF(rect.Left + 1, rect.Top, rect.Width - 2, rect.Height);
            cache.outsideBrush = new LinearGradientBrush(vertRectF, Color.Transparent, _whiten128, 270f);
            cache.outsideBrush.Blend = _ribbonOutBlend;
            cache.insideBrush = new LinearGradientBrush(vertRectF, Color.Transparent, _whiten200, 270f);
            cache.insideBrush.Blend = _ribbonInBlend;
            cache.topBrush = new LinearGradientBrush(horzRectF, _whiten92, _whiten128, 180f);
            cache.topBrush.Blend = _ribbonTopBlend;
            cache.topPen = new Pen(cache.topBrush);

            cache.outsidePen = new Pen(c1);
            cache.outsidePath = outsidePath;
            cache.topPath = topPath;
            cache.ellipsePath = ellipsePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingBottomDraw2007(Rectangle rect,
                                                                   MementoRibbonTabTracking2007 cache,
                                                                   Graphics g)
        {
            g.FillRectangle(cache.outsideBrush, rect.Left, rect.Top + 1, 1, rect.Height - 4);
            g.FillRectangle(cache.insideBrush, rect.Left + 2, rect.Top + 1, 1, rect.Height - 4);
            g.FillRectangle(cache.outsideBrush, rect.Right - 1, rect.Top + 1, 1, rect.Height - 4);
            g.FillRectangle(cache.insideBrush, rect.Right - 3, rect.Top + 1, 1, rect.Height - 4);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonTabTracking2010(PaletteRibbonShape shape,
                                                                RenderContext context,
                                                                Rectangle rect,
                                                                PaletteState state,
                                                                IPaletteRibbonBack palette,
                                                                VisualOrientation orientation,
                                                                IDisposable memento,
                                                                bool standard)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                bool generate = true;
                MementoRibbonTabTracking2010 cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabTracking2010))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabTracking2010(rect, c1, c2, c3, c4, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabTracking2010)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // If c5 has a colour then use that to highlight the tab
                    if (c5 != Color.Empty)
                    {
                        if (!standard)
                            c5 = CommonHelper.MergeColors(c5, 0.65f, Color.Black, 0.35f);

                        c1 = c5;
                        c2 = CommonHelper.MergeColors(c2, 0.8f, ControlPaint.Light(c5), 0.2f);
                        c3 = CommonHelper.MergeColors(c3, 0.7f, c5, 0.3f);
                    }

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabTrackingTop2010(rect, c3, c4, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabTrackingLeft2010(rect, c3, c4, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabTrackingRight2010(rect, c3, c4, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabTrackingBottom2010(rect, c3, c4, cache);
                            break;
                    }

                    cache.outsidePen = new Pen(c1);
                    cache.outsideBrush = new SolidBrush(c2);
                }

                // Fill the full background
                context.Graphics.FillPath(cache.outsideBrush, cache.outsidePath);

                // Draw the border
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.DrawPath(cache.outsidePen, cache.borderPath);

                // Fill the inside area
                context.Graphics.FillPath(cache.insideBrush, cache.insidePath);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingTop2010(Rectangle rect,
                                                            Color c3, Color c4,
                                                            MementoRibbonTabTracking2010 cache)
        {
            GraphicsPath borderPath = new GraphicsPath();
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a border pen
            borderPath.AddLine(rect.Left, rect.Bottom - 2, rect.Left, rect.Top + 1.75f);
            borderPath.AddLine(rect.Left, rect.Top + 1.75f, rect.Left + 1, rect.Top);
            borderPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
            borderPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.75f);
            borderPath.AddLine(rect.Right - 1, rect.Top + 1.75f, rect.Right - 1, rect.Bottom - 2);

            // Create path for a inside fill
            outsidePath.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Left, rect.Top + 1.5f, rect.Left + 1, rect.Top);
            outsidePath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
            outsidePath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Right - 1, rect.Top + 1.5f, rect.Right - 1, rect.Bottom - 1);

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Y += 2;
            rect.Width -= 3;
            rect.Height -= 2;

            // Create path for a curved inside fill area
            insidePath.AddLine(rect.Left, rect.Bottom - 1, rect.Left, rect.Top + 1f);
            insidePath.AddLine(rect.Left, rect.Top + 1f, rect.Left + 1, rect.Top);
            insidePath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
            insidePath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 1f);
            insidePath.AddLine(rect.Right - 1, rect.Top + 1f, rect.Right - 1, rect.Bottom - 1);

            cache.borderPath = borderPath;
            cache.outsidePath = outsidePath;
            cache.insidePath = insidePath;
            cache.insideBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2), c4, c3, 270f);
            cache.insideBrush.Blend = _linear50Blend;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingBottom2010(Rectangle rect,
                                                               Color c3, Color c4,
                                                               MementoRibbonTabTracking2010 cache)
        {
            GraphicsPath borderPath = new GraphicsPath();
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a border pen
            borderPath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom - 2.75f);
            borderPath.AddLine(rect.Left, rect.Bottom - 2.75f, rect.Left + 1, rect.Bottom - 1);
            borderPath.AddLine(rect.Left + 1, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 1);
            borderPath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 2.75f);
            borderPath.AddLine(rect.Right - 1, rect.Bottom - 2.75f, rect.Right - 1, rect.Top);

            // Create path for a inside fill
            outsidePath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Left, rect.Bottom - 2.5f, rect.Left + 1, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Right - 1, rect.Bottom - 2.5f, rect.Right - 1, rect.Top);

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Width -= 3;
            rect.Height -= 2;

            // Create path for a curved inside fill area
            insidePath.AddLine(rect.Left, rect.Top, rect.Left, rect.Bottom - 2);
            insidePath.AddLine(rect.Left, rect.Bottom - 2, rect.Left + 2, rect.Bottom);
            insidePath.AddLine(rect.Left + 2, rect.Bottom, rect.Right - 3, rect.Bottom);
            insidePath.AddLine(rect.Right - 3, rect.Bottom, rect.Right - 1, rect.Bottom - 2);
            insidePath.AddLine(rect.Right - 1, rect.Bottom - 2, rect.Right - 1, rect.Top);

            cache.borderPath = borderPath;
            cache.outsidePath = outsidePath;
            cache.insidePath = insidePath;
            cache.insideBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2), c4, c3, 90f);
            cache.insideBrush.Blend = _linear50Blend;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingLeft2010(Rectangle rect,
                                                             Color c3, Color c4,
                                                             MementoRibbonTabTracking2010 cache)
        {
            GraphicsPath borderPath = new GraphicsPath();
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a border pen
            borderPath.AddLine(rect.Right - 1, rect.Top, rect.Left + 1.75f, rect.Top);
            borderPath.AddLine(rect.Left + 1.75f, rect.Top, rect.Left, rect.Top + 1);
            borderPath.AddLine(rect.Left, rect.Top + 1, rect.Left, rect.Bottom - 2.5f);
            borderPath.AddLine(rect.Left, rect.Bottom - 2.5f, rect.Left + 1.75f, rect.Bottom - 1);
            borderPath.AddLine(rect.Left + 1.75f, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);

            // Create path for a inside fill
            outsidePath.AddLine(rect.Right - 1, rect.Top, rect.Left + 1.75f, rect.Top);
            outsidePath.AddLine(rect.Left + 1.75f, rect.Top, rect.Left, rect.Top + 1);
            outsidePath.AddLine(rect.Left, rect.Top + 1, rect.Left, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Left, rect.Bottom - 2.5f, rect.Left + 1.75f, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 1.75f, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Y += 2;
            rect.Width -= 2;
            rect.Height -= 3;

            // Create path for a curved inside fill area
            insidePath.AddLine(rect.Right - 1, rect.Top, rect.Left + 1, rect.Top);
            insidePath.AddLine(rect.Left + 1, rect.Top, rect.Left, rect.Top + 1);
            insidePath.AddLine(rect.Left, rect.Top + 1, rect.Left, rect.Bottom - 2);
            insidePath.AddLine(rect.Left, rect.Bottom - 2.5f, rect.Left + 1.5f, rect.Bottom - 1);
            insidePath.AddLine(rect.Left + 1.5f, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);

            cache.borderPath = borderPath;
            cache.outsidePath = outsidePath;
            cache.insidePath = insidePath;
            cache.insideBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2), c4, c3, 180f);
            cache.insideBrush.Blend = _linear50Blend;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabTrackingRight2010(Rectangle rect,
                                                              Color c3, Color c4,
                                                              MementoRibbonTabTracking2010 cache)
        {
            GraphicsPath borderPath = new GraphicsPath();
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a border pen
            borderPath.AddLine(rect.Left, rect.Top, rect.Right - 2.75f, rect.Top);
            borderPath.AddLine(rect.Right - 2.75f, rect.Top, rect.Right - 1, rect.Top + 1);
            borderPath.AddLine(rect.Right - 1, rect.Top + 1, rect.Right - 1, rect.Bottom - 2.5f);
            borderPath.AddLine(rect.Right - 1, rect.Bottom - 2.5f, rect.Right - 2.75f, rect.Bottom - 1);
            borderPath.AddLine(rect.Right - 2.75f, rect.Bottom - 1, rect.Left, rect.Bottom - 1);

            // Create path for a inside fill
            outsidePath.AddLine(rect.Left, rect.Top, rect.Right - 2.75f, rect.Top);
            outsidePath.AddLine(rect.Right - 2.75f, rect.Top, rect.Right - 1, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 1, rect.Top + 1, rect.Right - 1, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Right - 1, rect.Bottom - 2.5f, rect.Right - 2.75f, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 2.75f, rect.Bottom - 1, rect.Left, rect.Bottom - 1);

            // Reduce rectangle to the inside fill area
            rect.Y += 2;
            rect.Width -= 2;
            rect.Height -= 3;

            // Create path for a curved inside fill area
            insidePath.AddLine(rect.Left, rect.Top, rect.Right - 1, rect.Top);
            insidePath.AddLine(rect.Right - 1, rect.Top, rect.Right, rect.Top + 1);
            insidePath.AddLine(rect.Right, rect.Top + 1, rect.Right, rect.Bottom - 2.5f);
            insidePath.AddLine(rect.Right, rect.Bottom - 2.5f, rect.Right - 3.5f, rect.Bottom - 1);
            insidePath.AddLine(rect.Right - 3.5f, rect.Bottom - 1, rect.Left, rect.Bottom - 1);

            cache.borderPath = borderPath;
            cache.outsidePath = outsidePath;
            cache.insidePath = insidePath;
            cache.insideBrush = new LinearGradientBrush(new RectangleF(rect.X - 2, rect.Y - 1, rect.Width + 2, rect.Height + 2), c4, c3, 0f);
            cache.insideBrush.Blend = _linear50Blend;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonTabFocus2010(PaletteRibbonShape shape,
                                                             RenderContext context,
                                                             Rectangle rect,
                                                             PaletteState state,
                                                             IPaletteRibbonBack palette,
                                                             VisualOrientation orientation,
                                                             IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                bool generate = true;
                MementoRibbonTabTracking2010 cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabTracking2010))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabTracking2010(rect, c1, c2, c3, c4, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabTracking2010)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // If c5 has a colour then use that to highlight the tab
                    if (c5 != Color.Empty)
                    {
                        c1 = c5;
                        c2 = CommonHelper.MergeColors(c2, 0.8f, ControlPaint.Light(c5), 0.2f);
                        c3 = CommonHelper.MergeColors(c3, 0.7f, c5, 0.3f);
                    }

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabFocusTop2010(rect, c3, c4, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabFocusLeft2010(rect, c3, c4, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabFocusRight2010(rect, c3, c4, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabFocusBottom2010(rect, c3, c4, cache);
                            break;
                    }

                    cache.outsidePen = new Pen(c1);
                    cache.outsideBrush = new SolidBrush(c2);
                }

                // Fill the full background
                context.Graphics.FillPath(cache.outsideBrush, cache.outsidePath);

                // Draw the border
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.DrawPath(cache.outsidePen, cache.borderPath);
 
                // Fill the inside area
                context.Graphics.FillPath(cache.insideBrush, cache.insidePath);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabFocusTop2010(Rectangle rect,
                                                         Color c3, Color c4,
                                                         MementoRibbonTabTracking2010 cache)
        {
            GraphicsPath borderPath = new GraphicsPath();
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a border pen
            borderPath.AddLine(rect.Left, rect.Bottom - 1, rect.Left + 1, rect.Bottom - 2);
            borderPath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left + 1, rect.Top + 1.75f);
            borderPath.AddLine(rect.Left + 1, rect.Top + 1.75f, rect.Left + 2, rect.Top);
            borderPath.AddLine(rect.Left + 2, rect.Top, rect.Right - 3, rect.Top);
            borderPath.AddLine(rect.Right - 3, rect.Top, rect.Right - 2, rect.Top + 1.75f);
            borderPath.AddLine(rect.Right - 2, rect.Top + 1.75f, rect.Right - 2, rect.Bottom - 2);
            borderPath.AddLine(rect.Right - 2, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 1);

            // Create path for a inside fill
            outsidePath.AddLine(rect.Left, rect.Bottom, rect.Left + 1, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 1, rect.Left + 1, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 1.5f, rect.Left + 2, rect.Top);
            outsidePath.AddLine(rect.Left + 2, rect.Top, rect.Right - 3, rect.Top);
            outsidePath.AddLine(rect.Right - 3, rect.Top, rect.Right - 2, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 1.5f, rect.Right - 2, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Y += 2;
            rect.Width -= 3;
            rect.Height -= 2;

            // Create path for a curved inside fill area
            insidePath.AddLine(rect.Left - 2, rect.Bottom, rect.Left + 1, rect.Bottom - 2);
            insidePath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left + 1, rect.Top + 1f);
            insidePath.AddLine(rect.Left + 1, rect.Top + 1f, rect.Left + 2, rect.Top);
            insidePath.AddLine(rect.Left + 2, rect.Top, rect.Right - 3, rect.Top);
            insidePath.AddLine(rect.Right - 3, rect.Top, rect.Right - 2, rect.Top + 1f);
            insidePath.AddLine(rect.Right - 2, rect.Top + 1f, rect.Right - 2, rect.Bottom - 2);
            insidePath.AddLine(rect.Right - 2, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 1);

            cache.borderPath = borderPath;
            cache.outsidePath = outsidePath;
            cache.insidePath = insidePath;
            cache.insideBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2), c4, c3, 270f);
            cache.insideBrush.Blend = _linear50Blend;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabFocusBottom2010(Rectangle rect,
                                                            Color c3, Color c4,
                                                            MementoRibbonTabTracking2010 cache)
        {
            GraphicsPath borderPath = new GraphicsPath();
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a border pen
            borderPath.AddLine(rect.Left, rect.Top, rect.Left + 1, rect.Top + 1);
            borderPath.AddLine(rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 2.75f);
            borderPath.AddLine(rect.Left + 1, rect.Bottom - 2.75f, rect.Left + 2, rect.Bottom - 1);
            borderPath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Right - 3, rect.Bottom - 1);
            borderPath.AddLine(rect.Right - 3, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 2.75f);
            borderPath.AddLine(rect.Right - 2, rect.Bottom - 2.75f, rect.Right - 2, rect.Top + 1);
            borderPath.AddLine(rect.Right - 2, rect.Top + 1, rect.Right - 1, rect.Top);

            // Create path for a inside fill
            outsidePath.AddLine(rect.Left, rect.Top, rect.Left + 1, rect.Top);
            outsidePath.AddLine(rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 2.5f, rect.Left + 2, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Right - 1, rect.Bottom - 2.5f, rect.Right - 1, rect.Top);
            outsidePath.AddLine(rect.Right - 1, rect.Top, rect.Right, rect.Top);

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Width -= 3;
            rect.Height -= 2;

            // Create path for a curved inside fill area
            insidePath.AddLine(rect.Left - 2, rect.Top - 1, rect.Left + 1, rect.Top + 1);
            insidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 2);
            insidePath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left + 3, rect.Bottom);
            insidePath.AddLine(rect.Left + 3, rect.Bottom, rect.Right - 4, rect.Bottom);
            insidePath.AddLine(rect.Right - 4, rect.Bottom, rect.Right - 3, rect.Bottom - 1);
            insidePath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 2, rect.Top + 1);
            insidePath.AddLine(rect.Right - 2, rect.Top + 1, rect.Right - 1, rect.Top);

            cache.borderPath = borderPath;
            cache.outsidePath = outsidePath;
            cache.insidePath = insidePath;
            cache.insideBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2), c4, c3, 90f);
            cache.insideBrush.Blend = _linear50Blend;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabFocusLeft2010(Rectangle rect,
                                                          Color c3, Color c4,
                                                          MementoRibbonTabTracking2010 cache)
        {
            GraphicsPath borderPath = new GraphicsPath();
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a border pen
            borderPath.AddLine(rect.Right - 1, rect.Top - 1, rect.Right - 2, rect.Top);
            borderPath.AddLine(rect.Right - 2, rect.Top, rect.Left + 1.75f, rect.Top);
            borderPath.AddLine(rect.Left + 1.75f, rect.Top, rect.Left, rect.Top + 1);
            borderPath.AddLine(rect.Left, rect.Top + 2, rect.Left, rect.Bottom - 3.5f);
            borderPath.AddLine(rect.Left, rect.Bottom - 3.5f, rect.Left + 1.75f, rect.Bottom - 2);
            borderPath.AddLine(rect.Left + 1.75f, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
            borderPath.AddLine(rect.Right - 2, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 1);

            // Create path for a inside fill
            outsidePath.AddLine(rect.Right, rect.Top, rect.Left + 1.75f, rect.Top);
            outsidePath.AddLine(rect.Left + 1.75f, rect.Top, rect.Left, rect.Top + 1);
            outsidePath.AddLine(rect.Left, rect.Top + 1, rect.Left, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Left, rect.Bottom - 2.5f, rect.Left + 1.75f, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 1.75f, rect.Bottom - 1, rect.Right, rect.Bottom - 1);

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Y += 2;
            rect.Width -= 2;
            rect.Height -= 3;

            // Create path for a curved inside fill area
            insidePath.AddLine(rect.Right - 1, rect.Top - 2, rect.Right - 1, rect.Top);
            insidePath.AddLine(rect.Right - 1, rect.Top, rect.Left + 1, rect.Top);
            insidePath.AddLine(rect.Left + 1, rect.Top, rect.Left, rect.Top + 1);
            insidePath.AddLine(rect.Left, rect.Top + 1, rect.Left, rect.Bottom - 4);
            insidePath.AddLine(rect.Left, rect.Bottom - 4, rect.Left + 2, rect.Bottom - 2);
            insidePath.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 2);
            insidePath.AddLine(rect.Right - 1, rect.Bottom - 2, rect.Right, rect.Bottom);

            cache.borderPath = borderPath;
            cache.outsidePath = outsidePath;
            cache.insidePath = insidePath;
            cache.insideBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2), c4, c3, 180f);
            cache.insideBrush.Blend = _linear50Blend;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabFocusRight2010(Rectangle rect,
                                                           Color c3, Color c4,
                                                           MementoRibbonTabTracking2010 cache)
        {
            GraphicsPath borderPath = new GraphicsPath();
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a border pen
            borderPath.AddLine(rect.Left, rect.Top - 1, rect.Left + 1, rect.Top);
            borderPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 2.75f, rect.Top);
            borderPath.AddLine(rect.Right - 2.75f, rect.Top, rect.Right - 1, rect.Top + 1);
            borderPath.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 3.5f);
            borderPath.AddLine(rect.Right - 1, rect.Bottom - 3.5f, rect.Right - 2.75f, rect.Bottom - 2);
            borderPath.AddLine(rect.Right - 2.75f, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 2);
            borderPath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left, rect.Bottom - 1);

            // Create path for a inside fill
            outsidePath.AddLine(rect.Left, rect.Top, rect.Right - 2.75f, rect.Top);
            outsidePath.AddLine(rect.Right - 2.75f, rect.Top, rect.Right - 1, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 1, rect.Top + 1, rect.Right - 1, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Right - 1, rect.Bottom - 2.5f, rect.Right - 2.75f, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 2.75f, rect.Bottom - 1, rect.Left, rect.Bottom - 1);

            // Reduce rectangle to the inside fill area
            rect.Y += 2;
            rect.Width -= 2;
            rect.Height -= 3;

            // Create path for a curved inside fill area
            insidePath.AddLine(rect.Left, rect.Top - 2, rect.Left + 1, rect.Top);
            insidePath.AddLine(rect.Left + 1, rect.Top, rect.Right - 1, rect.Top);
            insidePath.AddLine(rect.Right - 1, rect.Top, rect.Right, rect.Top + 1);
            insidePath.AddLine(rect.Right, rect.Top + 1, rect.Right, rect.Bottom - 4);
            insidePath.AddLine(rect.Right, rect.Bottom - 4, rect.Right - 2, rect.Bottom - 2);
            insidePath.AddLine(rect.Right - 2, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 2);
            insidePath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left, rect.Bottom - 1);

            cache.borderPath = borderPath;
            cache.outsidePath = outsidePath;
            cache.insidePath = insidePath;
            cache.insideBrush = new LinearGradientBrush(new RectangleF(rect.X - 2, rect.Y - 1, rect.Width + 2, rect.Height + 2), c4, c3, 0f);
            cache.insideBrush.Blend = _linear50Blend;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonTabGlowing(PaletteRibbonShape shape,
                                                           RenderContext context,
                                                           Rectangle rect,
                                                           PaletteState state,
                                                           IPaletteRibbonBack palette,
                                                           VisualOrientation orientation,
                                                           IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color insideColor = Color.FromArgb(36, c2);

                bool generate = true;
                MementoRibbonTabGlowing cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabGlowing))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabGlowing(rect, c1, c2, insideColor, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabGlowing)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, insideColor, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabGlowingTop(rect, c1, c2, insideColor, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabGlowingLeft(rect, c1, c2, insideColor, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabGlowingRight(rect, c1, c2, insideColor, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabGlowingBottom(rect, c1, c2, insideColor, cache);
                            break;
                    }
                }

                // Fill the path area with inside color
                context.Graphics.FillPath(cache.insideBrush, cache.outsidePath);

                switch (orientation)
                {
                    case VisualOrientation.Top:
                        // Draw the missing line from the bottom of the inside area
                        context.Graphics.DrawLine(cache.insidePen, rect.Left + 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
                        break;
                    case VisualOrientation.Left:
                        // Draw the missing line from the right of the inside area
                        context.Graphics.DrawLine(cache.insidePen, rect.Right - 2, rect.Top + 1, rect.Right - 2, rect.Bottom - 2);
                        break;
                }

                // Draw the border over the edge of the inside color
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.DrawPath(cache.outsidePen, cache.outsidePath);

                // Draw the top glass effect
                context.Graphics.FillPath(cache.topBrush, cache.topPath);

                // Cannot draw a path that contains a zero sized element
                if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
                    context.Graphics.FillRectangle(cache.ellipseBrush, cache.fullRect);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabGlowingTop(Rectangle rect, 
                                                       Color c1, Color c2, Color insideColor, 
                                                       MementoRibbonTabGlowing cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath topPath = new GraphicsPath();
            GraphicsPath ellipsePath = new GraphicsPath();

            // Create path for a curved border around the tab
            outsidePath.AddLine(rect.Left, rect.Bottom - 2, rect.Left, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Left, rect.Top + 1.5f, rect.Left + 2, rect.Top);
            outsidePath.AddLine(rect.Left + 2, rect.Top, rect.Right - 4, rect.Top);
            outsidePath.AddLine(rect.Right - 4, rect.Top, rect.Right - 2, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 1.5f, rect.Right - 2, rect.Bottom - 2);

            // Create path for the top glassy effect
            int q4 = rect.Height / 4;
            topPath.AddLine(rect.Left + 2, rect.Top + 1, rect.Left + 1, rect.Top + 2);
            topPath.AddLine(rect.Left + 1, rect.Top + 2, rect.Left + 1, rect.Top + 2 + q4);
            topPath.AddLine(rect.Left + 1, rect.Top + 2 + q4, rect.Left + 4, rect.Top + 5 + q4);
            topPath.AddLine(rect.Left + 4, rect.Top + 5 + q4, rect.Right - 5, rect.Top + 5 + q4);
            topPath.AddLine(rect.Right - 5, rect.Top + 5 + q4, rect.Right - 2, rect.Top + 2 + q4);
            topPath.AddLine(rect.Right - 2, rect.Top + 2 + q4, rect.Right - 2, rect.Top + 2);
            topPath.AddLine(rect.Right - 2, rect.Top + 2, rect.Right - 3, rect.Top + 1);

            RectangleF topRectF = new RectangleF(rect.Left, rect.Top, rect.Width, q4 + 5);
            cache.topBrush = new LinearGradientBrush(topRectF, c1, Color.Transparent, 90f);

            int ellipseWidth = (int)(rect.Width * 1.2f);
            int ellipseHeight = (int)(rect.Height * 0.4f);
            cache.fullRect = new RectangleF(rect.Left + 1, rect.Top + 1, rect.Width - 3, rect.Height - 2);
            cache.ellipseRect = new RectangleF(rect.Left - ((ellipseWidth - rect.Width) / 2), rect.Bottom - ellipseHeight, ellipseWidth, ellipseHeight * 2);

            // Cannot draw a path that contains a zero sized element
            if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
            {
                ellipsePath.AddEllipse(cache.ellipseRect);
                cache.ellipseBrush = new PathGradientBrush(ellipsePath);
                cache.ellipseBrush.CenterColor = c2;
                PointF centerPoint = new PointF(cache.ellipseRect.Left + (cache.ellipseRect.Width / 2), cache.ellipseRect.Top + (cache.ellipseRect.Height / 2));
                cache.ellipseBrush.CenterPoint = centerPoint;
                cache.ellipseBrush.SurroundColors = new Color[] { Color.Transparent };
            }

            cache.insideBrush = new SolidBrush(insideColor);
            cache.insidePen = new Pen(insideColor);
            cache.outsidePen = new Pen(c1);
            cache.outsidePath = outsidePath;
            cache.topPath = topPath;
            cache.ellipsePath = ellipsePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabGlowingLeft(Rectangle rect,
                                                        Color c1, Color c2, Color insideColor,
                                                        MementoRibbonTabGlowing cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath topPath = new GraphicsPath();
            GraphicsPath ellipsePath = new GraphicsPath();

            // Create path for a curved border around the tab
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Left + 1.5f, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 1.5f, rect.Bottom - 1, rect.Left, rect.Bottom - 3);
            outsidePath.AddLine(rect.Left, rect.Bottom - 3, rect.Left, rect.Top + 3);
            outsidePath.AddLine(rect.Left, rect.Top + 3, rect.Left + 1.5f, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 1.5f, rect.Top + 1, rect.Right - 2, rect.Top + 1);

            // Create path for the top glassy effect
            int q4 = rect.Height / 4;
            topPath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left + 2, rect.Bottom - 1);
            topPath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Left + 2 + q4, rect.Bottom - 1);
            topPath.AddLine(rect.Left + 2 + q4, rect.Bottom - 1, rect.Left + 5 + q4, rect.Bottom - 4);
            topPath.AddLine(rect.Left + 5 + q4, rect.Bottom - 4, rect.Left + 5 + q4, rect.Top + 4);
            topPath.AddLine(rect.Left + 5 + q4, rect.Top + 4, rect.Left + 2 + q4 , rect.Top + 1);
            topPath.AddLine(rect.Left + 2 + q4, rect.Top + 1, rect.Left + 2, rect.Top + 2);
            topPath.AddLine(rect.Left + 2, rect.Top + 2, rect.Left + 1, rect.Top + 2);

            RectangleF topRectF = new RectangleF(rect.Left, rect.Top, q4 + 5, rect.Height);
            cache.topBrush = new LinearGradientBrush(topRectF, c1, Color.Transparent, 0f);

            int ellipseWidth = (int)(rect.Width * 0.4f);
            int ellipseHeight = (int)(rect.Height * 1.2f);
            cache.fullRect = new RectangleF(rect.Left + 1, rect.Top + 2, rect.Width - 2, rect.Height - 3);
            cache.ellipseRect = new RectangleF(rect.Right - ellipseWidth, rect.Top - ((ellipseHeight - rect.Height) / 2), ellipseWidth * 2, ellipseHeight);

            // Cannot draw a path that contains a zero sized element
            if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
            {
                ellipsePath.AddEllipse(cache.ellipseRect);
                cache.ellipseBrush = new PathGradientBrush(ellipsePath);
                cache.ellipseBrush.CenterColor = c2;
                PointF centerPoint = new PointF(cache.ellipseRect.Left + (cache.ellipseRect.Width / 2), cache.ellipseRect.Top + (cache.ellipseRect.Height / 2));
                cache.ellipseBrush.CenterPoint = centerPoint;
                cache.ellipseBrush.SurroundColors = new Color[] { Color.Transparent };
            }

            cache.insideBrush = new SolidBrush(insideColor);
            cache.insidePen = new Pen(insideColor);
            cache.outsidePen = new Pen(c1);
            cache.outsidePath = outsidePath;
            cache.topPath = topPath;
            cache.ellipsePath = ellipsePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabGlowingRight(Rectangle rect,
                                                         Color c1, Color c2, Color insideColor,
                                                         MementoRibbonTabGlowing cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath topPath = new GraphicsPath();
            GraphicsPath ellipsePath = new GraphicsPath();

            // Create path for a curved border around the tab
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 1, rect.Right - 2.5f, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 2.5f, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 3);
            outsidePath.AddLine(rect.Right - 1, rect.Bottom - 3, rect.Right - 1, rect.Top + 3);
            outsidePath.AddLine(rect.Right - 1, rect.Top + 3, rect.Right - 2.5f, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 2.5f, rect.Top + 1, rect.Left + 1, rect.Top + 1);

            // Create path for the top glassy effect
            int q4 = rect.Height / 4;
            topPath.AddLine(rect.Right - 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 1);
            topPath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 2 - q4, rect.Bottom - 1);
            topPath.AddLine(rect.Right - 2 - q4, rect.Bottom - 1, rect.Right - 5 - q4, rect.Bottom - 4);
            topPath.AddLine(rect.Right - 5 - q4, rect.Bottom - 4, rect.Right - 5 - q4, rect.Top + 4);
            topPath.AddLine(rect.Right - 5 - q4, rect.Top + 4, rect.Right - 2 - q4, rect.Top + 1);
            topPath.AddLine(rect.Right - 2 - q4, rect.Top + 1, rect.Right - 2, rect.Top + 2);
            topPath.AddLine(rect.Right - 2, rect.Top + 2, rect.Right - 1, rect.Top + 2);

            RectangleF topRectF = new RectangleF(rect.Right - q4 - 5, rect.Top, q4 + 5, rect.Height);
            cache.topBrush = new LinearGradientBrush(topRectF, c1, Color.Transparent, 180f);

            int ellipseWidth = (int)(rect.Width * 0.4f);
            int ellipseHeight = (int)(rect.Height * 1.2f);
            cache.fullRect = new RectangleF(rect.Left + 1, rect.Top + 2, rect.Width - 2, rect.Height - 3);
            cache.ellipseRect = new RectangleF(rect.Left - ellipseWidth, rect.Top - ((ellipseHeight - rect.Height) / 2), ellipseWidth * 2, ellipseHeight);

            // Cannot draw a path that contains a zero sized element
            if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
            {
                ellipsePath.AddEllipse(cache.ellipseRect);
                cache.ellipseBrush = new PathGradientBrush(ellipsePath);
                cache.ellipseBrush.CenterColor = c2;
                PointF centerPoint = new PointF(cache.ellipseRect.Left + (cache.ellipseRect.Width / 2), cache.ellipseRect.Top + (cache.ellipseRect.Height / 2));
                cache.ellipseBrush.CenterPoint = centerPoint;
                cache.ellipseBrush.SurroundColors = new Color[] { Color.Transparent };
            }

            cache.insideBrush = new SolidBrush(insideColor);
            cache.insidePen = new Pen(insideColor);
            cache.outsidePen = new Pen(c1);
            cache.outsidePath = outsidePath;
            cache.topPath = topPath;
            cache.ellipsePath = ellipsePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabGlowingBottom(Rectangle rect,
                                                          Color c1, Color c2, Color insideColor,
                                                          MementoRibbonTabGlowing cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath topPath = new GraphicsPath();
            GraphicsPath ellipsePath = new GraphicsPath();

            // Create path for a curved border around the tab
            outsidePath.AddLine(rect.Left, rect.Top + 1, rect.Left, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Left, rect.Bottom - 2.5f, rect.Left + 2, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Right - 4, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 4, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 2.5f, rect.Right - 2, rect.Top + 1);

            // Create path for the bottom glassy effect
            int q4 = rect.Height / 4;
            topPath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Left + 1, rect.Bottom - 2);
            topPath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 2 - q4);
            topPath.AddLine(rect.Left + 1, rect.Bottom - 2 - q4, rect.Left + 4, rect.Bottom - 5 - q4);
            topPath.AddLine(rect.Left + 4, rect.Bottom - 5 - q4, rect.Right - 5, rect.Bottom - 5 - q4);
            topPath.AddLine(rect.Right - 5, rect.Bottom - 5 - q4, rect.Right - 2, rect.Bottom - 2 - q4);
            topPath.AddLine(rect.Right - 2, rect.Bottom - 2 - q4, rect.Right - 2, rect.Bottom - 2);
            topPath.AddLine(rect.Right - 2, rect.Bottom - 2, rect.Right - 3, rect.Bottom - 1);

            RectangleF topRectF = new RectangleF(rect.Left, rect.Bottom - 6 - q4, rect.Width, q4 + 5);
            cache.topBrush = new LinearGradientBrush(topRectF, c1, Color.Transparent, 270f);

            int ellipseWidth = (int)(rect.Width * 1.2f);
            int ellipseHeight = (int)(rect.Height * 0.4f);
            cache.fullRect = new RectangleF(rect.Left + 1, rect.Top + 1, rect.Width - 3, rect.Height - 2);
            cache.ellipseRect = new RectangleF(rect.Left - ((ellipseWidth - rect.Width) / 2), rect.Top - ellipseHeight, ellipseWidth, ellipseHeight * 2);

            // Cannot draw a path that contains a zero sized element
            if ((cache.ellipseRect.Width > 0) && (cache.ellipseRect.Height > 0))
            {
                ellipsePath.AddEllipse(cache.ellipseRect);
                cache.ellipseBrush = new PathGradientBrush(ellipsePath);
                cache.ellipseBrush.CenterColor = c2;
                PointF centerPoint = new PointF(cache.ellipseRect.Left + (cache.ellipseRect.Width / 2), cache.ellipseRect.Bottom - 1 - (cache.ellipseRect.Height / 2));
                cache.ellipseBrush.CenterPoint = centerPoint;
                cache.ellipseBrush.SurroundColors = new Color[] { Color.Transparent };
            }

            cache.insideBrush = new SolidBrush(insideColor);
            cache.insidePen = new Pen(insideColor);
            cache.outsidePen = new Pen(c1);
            cache.outsidePath = outsidePath;
            cache.topPath = topPath;
            cache.ellipsePath = ellipsePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonTabSelected2007(RenderContext context, 
                                                                Rectangle rect,
                                                                PaletteState state,
                                                                IPaletteRibbonBack palette,
                                                                VisualOrientation orientation,
                                                                IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                bool generate = true;
                MementoRibbonTabSelected2007 cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabSelected2007))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabSelected2007(rect, c1, c2, c3, c4, c5, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabSelected2007)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, c5, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabSelectedTop2007(rect, c4, c5, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabSelectedLeft2007(rect, c4, c5, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabSelectedRight2007(rect, c4, c5, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabSelectedBottom2007(rect, c4, c5, cache);
                            break;
                    }

                    cache.insideBrush = new SolidBrush(c3);
                    cache.outsidePen = new Pen(c1);
                    cache.middlePen = new Pen(c2);
                    cache.insidePen = new Pen(c3);
                    cache.centerPen = new Pen(c5);
                }

                // Fill the inside of the border
                context.Graphics.FillPath(cache.insideBrush, cache.outsidePath);

                // Draw the actual border
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.DrawPath(cache.outsidePen, cache.outsidePath);

                switch (orientation)
                {
                    case VisualOrientation.Top:
                        DrawRibbonTabSelectedTopDraw2007(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Left:
                        DrawRibbonTabSelectedLeftDraw2007(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Right:
                        DrawRibbonTabSelectedRightDraw2007(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Bottom:
                        DrawRibbonTabSelectedBottomDraw2007(rect, cache, context.Graphics);
                        break;
                }

                // Fill in the center as a vertical gradient from tto bottom
                context.Graphics.FillRectangle(cache.centerBrush, cache.centerRect);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedTop2007(Rectangle rect,
                                                            Color c4, Color c5,
                                                            MementoRibbonTabSelected2007 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Left, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 3);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 3, rect.Left + 1, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 1.5f, rect.Left + 3, rect.Top);
            outsidePath.AddLine(rect.Left + 3, rect.Top, rect.Right - 4, rect.Top);
            outsidePath.AddLine(rect.Right - 4, rect.Top, rect.Right - 2, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 1.5f, rect.Right - 2, rect.Bottom - 3);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 3, rect.Right - 1, rect.Bottom - 2);

            cache.centerRect = new Rectangle(rect.Left + 4, rect.Top + 4, rect.Width - 8, rect.Height - 4);
            RectangleF centerRectF = new RectangleF(cache.centerRect.Left - 1, cache.centerRect.Top - 1, cache.centerRect.Width + 2, cache.centerRect.Height + 2);
            cache.centerBrush = new LinearGradientBrush(centerRectF, c4, c5, 90f);
            cache.outsidePath = outsidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedTopDraw2007(Rectangle rect,
                                                                MementoRibbonTabSelected2007 cache,
                                                                Graphics g)
        {
            // Fill in the bottom two lines that the 'FillPath' above missed
            g.DrawLine(cache.insidePen, rect.Left + 1, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 1);
            g.DrawLine(cache.insidePen, rect.Left + 2, rect.Bottom - 2, rect.Right - 3, rect.Bottom - 2);
            g.DrawLine(cache.centerPen, rect.Left + 3, rect.Bottom - 1, rect.Right - 4, rect.Bottom - 1);

            using (AntiAlias aa = new AntiAlias(g))
            {
                // Draw a line on the inside of the left and right border edges
                g.DrawLine(cache.middlePen, rect.Left + 0.5f, rect.Bottom - 1, rect.Left + 2, rect.Bottom - 3);
                g.DrawLine(cache.middlePen, rect.Left + 2, rect.Bottom - 3, rect.Left + 2, rect.Top + 2);
                g.DrawLine(cache.middlePen, rect.Right - 1.5f, rect.Bottom - 1, rect.Right - 3, rect.Bottom - 3);
                g.DrawLine(cache.middlePen, rect.Right - 3, rect.Bottom - 3, rect.Right - 3, rect.Top + 2);

                // Draw shadow lines on the outside of the left and right edges
                g.DrawLine(_paleShadowPen, rect.Left - 1, rect.Bottom - 2, rect.Left - 1, rect.Top + 8);
                g.DrawLine(_lightShadowPen, rect.Left, rect.Bottom - 3, rect.Left, rect.Top + 5);
                g.DrawLine(_darkShadowPen, rect.Right - 1, rect.Bottom - 3, rect.Right - 1, rect.Top + 3);
                g.DrawLine(_mediumShadowPen, rect.Right, rect.Bottom - 2, rect.Right, rect.Top + 7);
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedLeft2007(Rectangle rect,
                                                             Color c4, Color c5,
                                                             MementoRibbonTabSelected2007 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 3, rect.Bottom - 2);
            outsidePath.AddLine(rect.Right - 3, rect.Bottom - 2, rect.Left + 1.5f, rect.Bottom - 2);
            outsidePath.AddLine(rect.Left + 1.5f, rect.Bottom - 2, rect.Left, rect.Bottom - 4);
            outsidePath.AddLine(rect.Left, rect.Bottom - 4, rect.Left, rect.Top + 3);
            outsidePath.AddLine(rect.Left, rect.Top + 3, rect.Left + 1.5f, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 1.5f, rect.Top + 1, rect.Right - 3, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 3, rect.Top + 1, rect.Right - 1, rect.Top);

            cache.centerRect = new Rectangle(rect.Left + 4, rect.Top + 4, rect.Width - 4, rect.Height - 8);
            RectangleF centerRectF = new RectangleF(cache.centerRect.Left - 1, cache.centerRect.Top - 1, cache.centerRect.Width + 2, cache.centerRect.Height + 2);
            cache.centerBrush = new LinearGradientBrush(centerRectF, c4, c5, 0f);
            cache.outsidePath = outsidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedLeftDraw2007(Rectangle rect,
                                                                 MementoRibbonTabSelected2007 cache,
                                                                 Graphics g)
        {
            // Fill in the bottom two lines that the 'FillPath' above missed
            g.DrawLine(cache.insidePen, rect.Right - 1, rect.Bottom - 2, rect.Right - 1, rect.Top + 1);
            g.DrawLine(cache.insidePen, rect.Right - 2, rect.Bottom - 3, rect.Right - 2, rect.Top + 2);
            g.DrawLine(cache.centerPen, rect.Right - 1, rect.Bottom - 4, rect.Right - 1, rect.Top + 3);

            using (AntiAlias aa = new AntiAlias(g))
            {
                // Draw a line on the inside of the left and right border edges
                g.DrawLine(cache.middlePen, rect.Right - 1, rect.Bottom - 1.5f, rect.Right - 3, rect.Bottom - 3);
                g.DrawLine(cache.middlePen, rect.Right - 3, rect.Bottom - 3, rect.Left + 2, rect.Bottom - 3);
                g.DrawLine(cache.middlePen, rect.Right - 1, rect.Top + 0.5f, rect.Right - 3, rect.Top + 2);
                g.DrawLine(cache.middlePen, rect.Right - 3, rect.Top + 2, rect.Left + 2, rect.Top + 2);

                // Draw shadow lines on the outside of the left and right edges
                g.DrawLine(_paleShadowPen, rect.Right - 2, rect.Bottom, rect.Left + 8, rect.Bottom);
                g.DrawLine(_lightShadowPen, rect.Right - 3, rect.Bottom - 1, rect.Left + 5, rect.Bottom - 1);
                g.DrawLine(_darkShadowPen, rect.Right - 3, rect.Top, rect.Left + 3, rect.Top);
                g.DrawLine(_mediumShadowPen, rect.Right - 2, rect.Top - 1, rect.Left + 7, rect.Top - 1);
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedRight2007(Rectangle rect,
                                                              Color c4, Color c5,
                                                              MementoRibbonTabSelected2007 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 1, rect.Left + 2, rect.Bottom - 2);
            outsidePath.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Right - 2.5f, rect.Bottom - 2);
            outsidePath.AddLine(rect.Right - 2.5f, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 4);
            outsidePath.AddLine(rect.Right - 1, rect.Bottom - 4, rect.Right - 1, rect.Top + 3);
            outsidePath.AddLine(rect.Right - 1, rect.Top + 3, rect.Right - 2.5f, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 2.5f, rect.Top + 1, rect.Left + 2, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 2, rect.Top + 1, rect.Left, rect.Top);

            cache.centerRect = new Rectangle(rect.Left, rect.Top + 4, rect.Width - 4, rect.Height - 8);
            RectangleF centerRectF = new RectangleF(cache.centerRect.Left - 1, cache.centerRect.Top - 1, cache.centerRect.Width + 2, cache.centerRect.Height + 2);
            cache.centerBrush = new LinearGradientBrush(centerRectF, c4, c5, 180f);
            cache.outsidePath = outsidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedRightDraw2007(Rectangle rect,
                                                                  MementoRibbonTabSelected2007 cache,
                                                                  Graphics g)
        {
            // Fill in the bottom two lines that the 'FillPath' above missed
            g.DrawLine(cache.insidePen, rect.Left, rect.Bottom - 2, rect.Left, rect.Top + 1);
            g.DrawLine(cache.insidePen, rect.Left + 1, rect.Bottom - 3, rect.Left + 1, rect.Top + 2);
            g.DrawLine(cache.centerPen, rect.Left, rect.Bottom - 4, rect.Left, rect.Top + 3);

            using (AntiAlias aa = new AntiAlias(g))
            {
                // Draw a line on the inside of the left and right border edges
                g.DrawLine(cache.middlePen, rect.Left, rect.Bottom - 1.5f, rect.Left + 2, rect.Bottom - 3);
                g.DrawLine(cache.middlePen, rect.Left + 2, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 3);
                g.DrawLine(cache.middlePen, rect.Left, rect.Top + 0.5f, rect.Left + 2, rect.Top + 2);
                g.DrawLine(cache.middlePen, rect.Left + 2, rect.Top + 2, rect.Right - 3, rect.Top + 2);

                // Draw shadow lines on the outside of the left and right edges
                g.DrawLine(_paleShadowPen, rect.Left + 1, rect.Bottom, rect.Right - 9, rect.Bottom);
                g.DrawLine(_lightShadowPen, rect.Left + 2, rect.Bottom - 1, rect.Right - 6, rect.Bottom - 1);
                g.DrawLine(_darkShadowPen, rect.Left + 2, rect.Top, rect.Right - 4, rect.Top);
                g.DrawLine(_mediumShadowPen, rect.Left + 1, rect.Top - 1, rect.Right - 8, rect.Top - 1);
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedBottom2007(Rectangle rect,
                                                               Color c4, Color c5,
                                                               MementoRibbonTabSelected2007 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Left, rect.Top + 1, rect.Left + 1, rect.Top + 2);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 2, rect.Left + 1, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 2.5f, rect.Left + 3, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 3, rect.Bottom - 1, rect.Right - 4, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 4, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 2.5f, rect.Right - 2, rect.Top + 2);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 2, rect.Right - 1, rect.Top + 1);

            cache.centerRect = new Rectangle(rect.Left + 4, rect.Top, rect.Width - 8, rect.Height - 4);
            RectangleF centerRectF = new RectangleF(cache.centerRect.Left - 1, cache.centerRect.Top - 1, cache.centerRect.Width + 2, cache.centerRect.Height + 2);
            cache.centerBrush = new LinearGradientBrush(centerRectF, c4, c5, 270f);
            cache.outsidePath = outsidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedBottomDraw2007(Rectangle rect,
                                                                   MementoRibbonTabSelected2007 cache,
                                                                   Graphics g)
        {
            // Fill in the bottom two lines that the 'FillPath' above missed
            g.DrawLine(cache.insidePen, rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);
            g.DrawLine(cache.insidePen, rect.Left + 2, rect.Top + 1, rect.Right - 3, rect.Top + 1);
            g.DrawLine(cache.centerPen, rect.Left + 3, rect.Top, rect.Right - 4, rect.Top);

            using (AntiAlias aa = new AntiAlias(g))
            {
                // Draw a line on the inside of the left and right border edges
                g.DrawLine(cache.middlePen, rect.Left + 0.5f, rect.Top, rect.Left + 2, rect.Top + 2);
                g.DrawLine(cache.middlePen, rect.Left + 2, rect.Top + 2, rect.Left + 2, rect.Bottom - 3);
                g.DrawLine(cache.middlePen, rect.Right - 1.5f, rect.Top, rect.Right - 3, rect.Top + 2);
                g.DrawLine(cache.middlePen, rect.Right - 3, rect.Top + 2, rect.Right - 3, rect.Bottom - 3);

                // Draw shadow lines on the outside of the left and right edges
                g.DrawLine(_paleShadowPen, rect.Left - 1, rect.Top + 1, rect.Left - 1, rect.Bottom - 9);
                g.DrawLine(_lightShadowPen, rect.Left, rect.Top + 2, rect.Left, rect.Bottom - 6);
                g.DrawLine(_darkShadowPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 4);
                g.DrawLine(_mediumShadowPen, rect.Right, rect.Top + 1, rect.Right, rect.Bottom - 8);
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonTabSelected2010(RenderContext context,
                                                                Rectangle rect,
                                                                PaletteState state,
                                                                IPaletteRibbonBack palette,
                                                                VisualOrientation orientation,
                                                                IDisposable memento,
                                                                bool standard)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                bool generate = true;
                MementoRibbonTabSelected2010 cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabSelected2010))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabSelected2010(rect, c1, c2, c3, c4, c5, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabSelected2010)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, c5, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // If we have a context color to use then modify the drawing colors
                    if (c5 != Color.Empty)
                    {
                        if (!standard)
                            c5 = CommonHelper.MergeColors(c5, 0.65f, Color.Black, 0.35f);

                        c1 = Color.FromArgb(196, c5);
                    }

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabSelectedTop2010(rect, c2, c3, c5, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabSelectedLeft2010(rect, c2, c3, c5, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabSelectedRight2010(rect, c2, c3, c5, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabSelectedBottom2010(rect, c2, c3, c5, cache);
                            break;
                    }

                    cache.outsidePen = new Pen(c1);
                    cache.centerPen = new Pen(c4);
                }

                context.Graphics.FillPath(cache.centerBrush, cache.outsidePath);

                if (c5 != Color.Empty)
                    context.Graphics.FillPath(cache.insideBrush, cache.insidePath);

                using (AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.DrawPath(cache.outsidePen, cache.outsidePath);

                switch (orientation)
                {
                    case VisualOrientation.Top:
                        DrawRibbonTabSelectedTopDraw2010(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Left:
                        DrawRibbonTabSelectedLeftDraw2010(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Right:
                        DrawRibbonTabSelectedRightDraw2010(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Bottom:
                        DrawRibbonTabSelectedBottomDraw2010(rect, cache, context.Graphics);
                        break;
                }
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedTop2010(Rectangle rect,
                                                            Color c2, Color c3, Color c5,
                                                            MementoRibbonTabSelected2010 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Left, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 3);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 3, rect.Left + 1, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Left + 3, rect.Top);
            outsidePath.AddLine(rect.Left + 3, rect.Top, rect.Right - 4, rect.Top);
            outsidePath.AddLine(rect.Right - 4, rect.Top, rect.Right - 2, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 1, rect.Right - 2, rect.Bottom - 3);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 3, rect.Right - 1, rect.Bottom - 2);

            RectangleF centerRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
            cache.centerBrush = new LinearGradientBrush(centerRectF, c2, c3, 90f);
            cache.centerBrush.Blend = _ribbonTabSelected1Blend;
            cache.outsidePath = outsidePath;

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Y += 2;
            rect.Width -= 3;
            rect.Height -= 2;

            // Create path for a curved inside border
            insidePath.AddLine(rect.Left, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 3);
            insidePath.AddLine(rect.Left + 1, rect.Bottom - 3, rect.Left + 1, rect.Top + 1);
            insidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Left + 2, rect.Top);
            insidePath.AddLine(rect.Left + 2, rect.Top, rect.Right - 3, rect.Top);
            insidePath.AddLine(rect.Right - 3, rect.Top, rect.Right - 2, rect.Top + 1);
            insidePath.AddLine(rect.Right - 2, rect.Top + 1, rect.Right - 2, rect.Bottom - 3);
            insidePath.AddLine(rect.Right - 2, rect.Bottom - 3, rect.Right - 1, rect.Bottom - 2);

            RectangleF insideRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
            cache.insideBrush = new LinearGradientBrush(insideRectF, Color.FromArgb(32, c5), Color.Transparent, 90f);
            cache.insideBrush.Blend = _ribbonTabSelected2Blend;
            cache.insidePath = insidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedTopDraw2010(Rectangle rect,
                                                                MementoRibbonTabSelected2010 cache,
                                                                Graphics g)
        {
            // Fill in the bottom two lines that the 'FillPath' above missed
            g.DrawLine(cache.centerPen, rect.Left + 2, rect.Bottom - 2, rect.Right - 3, rect.Bottom - 2);
            g.DrawLine(cache.centerPen, rect.Left + 1, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 1);

            using (AntiAlias aa = new AntiAlias(g))
            {
                // Draw shadow lines on the outside of the left and right edges
                g.DrawLine(_mediumShadowPen, rect.Left, rect.Bottom - 3, rect.Left, rect.Top + 2);
                g.DrawLine(_mediumShadowPen, rect.Right - 1, rect.Bottom - 3, rect.Right - 1, rect.Top + 2);
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedLeft2010(Rectangle rect,
                                                             Color c2, Color c3, Color c5,
                                                             MementoRibbonTabSelected2010 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 2);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 2);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left, rect.Bottom - 4);
            outsidePath.AddLine(rect.Left, rect.Bottom - 4, rect.Left, rect.Top + 3);
            outsidePath.AddLine(rect.Left, rect.Top + 3, rect.Left + 1, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Right - 2, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 1, rect.Right - 2, rect.Top);

            RectangleF centerRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
            cache.centerBrush = new LinearGradientBrush(centerRectF, c2, c3, 0f);
            cache.centerBrush.Blend = _ribbonTabSelected1Blend;
            cache.outsidePath = outsidePath;

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Y += 2;
            rect.Width -= 2;
            rect.Height -= 3;

            // Create path for a curved inside border
            insidePath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 2);
            insidePath.AddLine(rect.Right - 2, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 2);
            insidePath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left, rect.Bottom - 4);
            insidePath.AddLine(rect.Left, rect.Bottom - 4, rect.Left, rect.Top + 2);
            insidePath.AddLine(rect.Left, rect.Top + 2, rect.Left + 1, rect.Top + 1);
            insidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Right - 2, rect.Top + 1);
            insidePath.AddLine(rect.Right - 2, rect.Top + 1, rect.Right - 2, rect.Top);

            RectangleF insideRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
            cache.insideBrush = new LinearGradientBrush(insideRectF, Color.FromArgb(32, c5), Color.Transparent, 0f);
            cache.insideBrush.Blend = _ribbonTabSelected2Blend;
            cache.insidePath = insidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedLeftDraw2010(Rectangle rect,
                                                                 MementoRibbonTabSelected2010 cache,
                                                                 Graphics g)
        {
            // Fill in the bottom two lines that the 'FillPath' above missed
            g.DrawLine(cache.centerPen, rect.Right - 2, rect.Bottom - 3, rect.Right - 2, rect.Top + 2);
            g.DrawLine(cache.centerPen, rect.Right - 1, rect.Bottom - 2, rect.Right - 1, rect.Top + 1);

            using (AntiAlias aa = new AntiAlias(g))
            {
                // Draw shadow lines on the outside of the top and bottom edges
                g.DrawLine(_mediumShadowPen, rect.Right - 3, rect.Bottom - 1, rect.Left + 3, rect.Bottom - 1);
                g.DrawLine(_mediumShadowPen, rect.Right - 3, rect.Top, rect.Left + 3, rect.Top);
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedRight2010(Rectangle rect,
                                                              Color c2, Color c3, Color c5,
                                                              MementoRibbonTabSelected2010 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 1, rect.Left + 1, rect.Bottom - 2);
            outsidePath.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 4);
            outsidePath.AddLine(rect.Right - 1, rect.Bottom - 4, rect.Right - 1, rect.Top + 3);
            outsidePath.AddLine(rect.Right - 1, rect.Top + 3, rect.Right - 2, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 1, rect.Left + 1, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Top);

            RectangleF centerRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
            cache.centerBrush = new LinearGradientBrush(centerRectF, c2, c3, 180f);
            cache.centerBrush.Blend = _ribbonTabSelected1Blend;
            cache.outsidePath = outsidePath;

            // Reduce rectangle to the inside fill area
            rect.Y += 2;
            rect.Width -= 1;
            rect.Height -= 3;

            // Create path for a curved inside border
            insidePath.AddLine(rect.Left + 1, rect.Bottom - 1, rect.Left + 1, rect.Bottom - 2);
            insidePath.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Right - 3, rect.Bottom - 2);
            insidePath.AddLine(rect.Right - 3, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 4);
            insidePath.AddLine(rect.Right - 1, rect.Bottom - 4, rect.Right - 1, rect.Top + 3);
            insidePath.AddLine(rect.Right - 1, rect.Top + 3, rect.Right - 2, rect.Top + 1);
            insidePath.AddLine(rect.Right - 2, rect.Top + 1, rect.Left + 1, rect.Top + 1);
            insidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Top);

            RectangleF insideRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
            cache.insideBrush = new LinearGradientBrush(insideRectF, Color.FromArgb(32, c5), Color.Transparent, 180f);
            cache.insideBrush.Blend = _ribbonTabSelected2Blend;
            cache.insidePath = insidePath;

        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedRightDraw2010(Rectangle rect,
                                                                  MementoRibbonTabSelected2010 cache,
                                                                  Graphics g)
        {
            // Fill in the bottom two lines that the 'FillPath' above missed
            g.DrawLine(cache.centerPen, rect.Left + 1, rect.Bottom - 3, rect.Left + 1, rect.Top + 2);
            g.DrawLine(cache.centerPen, rect.Left, rect.Bottom - 2, rect.Left, rect.Top + 1);

            using (AntiAlias aa = new AntiAlias(g))
            {
                // Draw shadow lines on the outside of the top and bottom edges
                g.DrawLine(_mediumShadowPen, rect.Left + 2, rect.Bottom - 1, rect.Right - 4, rect.Bottom - 1);
                g.DrawLine(_mediumShadowPen, rect.Left + 2, rect.Top, rect.Right - 4, rect.Top);
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedBottom2010(Rectangle rect,
                                                               Color c2, Color c3, Color c5,
                                                               MementoRibbonTabSelected2010 cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();
            GraphicsPath insidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Left, rect.Top + 1, rect.Left + 1, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 2);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 2, rect.Left + 3, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 3, rect.Bottom - 1, rect.Right - 4, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 4, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 3);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 3, rect.Right - 2, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 1, rect.Right - 1, rect.Top + 1);

            RectangleF centerRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
            cache.centerBrush = new LinearGradientBrush(centerRectF, c2, c3, 270f);
            cache.centerBrush.Blend = _ribbonTabSelected1Blend;
            cache.outsidePath = outsidePath;

            // Reduce rectangle to the inside fill area
            rect.X += 2;
            rect.Width -= 3;
            rect.Height -= 1;

            // Create path for a curved inside border
            insidePath.AddLine(rect.Left, rect.Top + 1, rect.Left + 1, rect.Top + 1);
            insidePath.AddLine(rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 3);
            insidePath.AddLine(rect.Left + 1, rect.Bottom - 3, rect.Left + 2, rect.Bottom - 1);
            insidePath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Right - 4, rect.Bottom - 1);
            insidePath.AddLine(rect.Right - 4, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 3);
            insidePath.AddLine(rect.Right - 2, rect.Bottom - 3, rect.Right - 2, rect.Top + 1);
            insidePath.AddLine(rect.Right - 2, rect.Top + 1, rect.Right - 1, rect.Top + 1);

            RectangleF insideRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
            cache.insideBrush = new LinearGradientBrush(insideRectF, Color.FromArgb(32, c5), Color.Transparent, 270f);
            cache.insideBrush.Blend = _ribbonTabSelected2Blend;
            cache.insidePath = insidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabSelectedBottomDraw2010(Rectangle rect,
                                                                   MementoRibbonTabSelected2010 cache,
                                                                   Graphics g)
        {
            // Fill in the bottom two lines that the 'FillPath' above missed
            g.DrawLine(cache.centerPen, rect.Left + 2, rect.Top + 1, rect.Right - 3, rect.Top + 1);
            g.DrawLine(cache.centerPen, rect.Left + 1, rect.Top, rect.Right - 2, rect.Top);

            using (AntiAlias aa = new AntiAlias(g))
            {
                // Draw shadow lines on the outside of the left and right edges
                g.DrawLine(_mediumShadowPen, rect.Left, rect.Top + 2, rect.Left, rect.Bottom - 4);
                g.DrawLine(_mediumShadowPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 4);
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonTabContextSelected(PaletteRibbonShape shape,
                                                                   RenderContext context,
                                                                   Rectangle rect,
                                                                   PaletteState state,
                                                                   IPaletteRibbonBack palette,
                                                                   VisualOrientation orientation,
                                                                   IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonTabContextSelected cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabContextSelected))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabContextSelected(rect, c1, c2, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabContextSelected)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabContextSelectedTop(rect, c2, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabContextSelectedLeft(rect, c2, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabContextSelectedRight(rect, c2, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabContextSelectedBottom(rect, c2, cache);
                            break;
                    }

                    cache.outsidePen = new Pen(c1);
                    cache.l1 = new Pen(Color.FromArgb(100, c2));
                    cache.l2 = new Pen(Color.FromArgb(75, c2));
                    cache.l3 = new Pen(Color.FromArgb(48, c2));
                    cache.bottomInnerPen = new Pen(Color.FromArgb(70, c2));
                    cache.bottomOuterPen = new Pen(Color.FromArgb(100, c2));
                }

                // Fill the interior using a gradient brush
                context.Graphics.FillRectangle(Brushes.White, cache.interiorRect);
                context.Graphics.FillRectangle(cache.insideBrush, cache.interiorRect);

                // Draw the actual border
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.DrawPath(cache.outsidePen, cache.outsidePath);

                switch (orientation)
                {
                    case VisualOrientation.Top:
                        DrawRibbonTabContextSelectedTopDraw(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Left:
                        DrawRibbonTabContextSelectedLeftDraw(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Right:
                        DrawRibbonTabContextSelectedRightDraw(rect, cache, context.Graphics);
                        break;
                    case VisualOrientation.Bottom:
                        DrawRibbonTabContextSelectedBottomDraw(rect, cache, context.Graphics);
                        break;
                }
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabContextSelectedTop(Rectangle rect,
                                                               Color c2,
                                                               MementoRibbonTabContextSelected cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Left, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 3);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 3, rect.Left + 1, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 1.5f, rect.Left + 3, rect.Top);
            outsidePath.AddLine(rect.Left + 3, rect.Top, rect.Right - 4, rect.Top);
            outsidePath.AddLine(rect.Right - 4, rect.Top, rect.Right - 2, rect.Top + 1.5f);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 1.5f, rect.Right - 2, rect.Bottom - 3);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 3, rect.Right - 1, rect.Bottom - 2);

            LinearGradientBrush leftBrush = new LinearGradientBrush(rect, Color.FromArgb(125, c2), Color.FromArgb(67, c2), 90f);
            LinearGradientBrush rightBrush = new LinearGradientBrush(rect, Color.FromArgb(16, c2), Color.FromArgb(67, c2), 90f);
            cache.leftPen = new Pen(leftBrush);
            cache.rightPen = new Pen(rightBrush);

            cache.interiorRect = new Rectangle(rect.Left + 2, rect.Top + 3, rect.Width - 4, rect.Height - 3);
            cache.insideBrush = new LinearGradientBrush(rect, Color.FromArgb(134, c2), Color.FromArgb(50, c2), 90f);
            cache.outsidePath = outsidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabContextSelectedTopDraw(Rectangle rect,
                                                                   MementoRibbonTabContextSelected cache,
                                                                   Graphics g)
        {
            g.DrawLine(Pens.White, rect.Left + 2, rect.Top + 3, rect.Right - 3, rect.Top + 3);
            g.DrawLine(cache.l3, rect.Left + 2, rect.Top + 3, rect.Right - 3, rect.Top + 3);
            g.DrawLine(Pens.White, rect.Left + 2, rect.Top + 2, rect.Right - 3, rect.Top + 2);
            g.DrawLine(cache.l2, rect.Left + 2, rect.Top + 2, rect.Right - 3, rect.Top + 2);
            g.DrawLine(Pens.White, rect.Left + 3, rect.Top + 1, rect.Right - 4, rect.Top + 1);
            g.DrawLine(cache.l1, rect.Left + 3, rect.Top + 1, rect.Right - 4, rect.Top + 1);

            // Draw the inside left, right and then bottom borders
            g.DrawLine(cache.leftPen, rect.Left + 2, rect.Top + 4, rect.Left + 2, rect.Bottom - 3);
            g.DrawLine(cache.rightPen, rect.Right - 3, rect.Top + 2, rect.Right - 3, rect.Bottom - 3);
            g.DrawLine(Pens.White, rect.Left + 2, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 1);
            g.DrawLine(cache.bottomInnerPen, rect.Left + 2, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 1);
            g.DrawLine(Pens.White, rect.Right - 3, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 1);
            g.DrawLine(cache.bottomInnerPen, rect.Right - 3, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 1);
            g.DrawLine(Pens.White, rect.Left + 1, rect.Bottom - 2, rect.Left, rect.Bottom - 1);
            g.DrawLine(cache.bottomOuterPen, rect.Left + 1, rect.Bottom - 2, rect.Left, rect.Bottom - 1);
            g.DrawLine(Pens.White, rect.Right - 2, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 1);
            g.DrawLine(cache.bottomOuterPen, rect.Right - 2, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 1);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabContextSelectedLeft(Rectangle rect,
                                                                Color c2,
                                                                MementoRibbonTabContextSelected cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Right - 3, rect.Bottom - 2);
            outsidePath.AddLine(rect.Right - 3, rect.Bottom - 2, rect.Left + 1.5f, rect.Bottom - 2);
            outsidePath.AddLine(rect.Left + 1.5f, rect.Bottom - 2, rect.Left, rect.Bottom - 4);
            outsidePath.AddLine(rect.Left, rect.Bottom - 4, rect.Left, rect.Top + 3);
            outsidePath.AddLine(rect.Left, rect.Top + 3, rect.Left + 1.5f, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 1.5f, rect.Top + 1, rect.Right - 3, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 3, rect.Top + 1, rect.Right - 2, rect.Top);

            LinearGradientBrush leftBrush = new LinearGradientBrush(rect, Color.FromArgb(125, c2), Color.FromArgb(67, c2), 0f);
            LinearGradientBrush rightBrush = new LinearGradientBrush(rect, Color.FromArgb(16, c2), Color.FromArgb(67, c2), 0f);
            cache.leftPen = new Pen(leftBrush);
            cache.rightPen = new Pen(rightBrush);

            cache.interiorRect = new Rectangle(rect.Left + 3, rect.Top + 2, rect.Width - 3, rect.Height - 4);
            cache.insideBrush = new LinearGradientBrush(rect, Color.FromArgb(134, c2), Color.FromArgb(50, c2), 0f);
            cache.outsidePath = outsidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabContextSelectedLeftDraw(Rectangle rect,
                                                                    MementoRibbonTabContextSelected cache,
                                                                    Graphics g)
        {
            g.DrawLine(Pens.White, rect.Left + 3, rect.Bottom - 3, rect.Left + 3, rect.Top + 2);
            g.DrawLine(cache.l3, rect.Left + 3, rect.Bottom - 3, rect.Left + 3, rect.Top + 2);
            g.DrawLine(Pens.White, rect.Left + 2, rect.Bottom - 3, rect.Left + 2, rect.Top + 2);
            g.DrawLine(cache.l2, rect.Left + 2, rect.Bottom - 3, rect.Left + 2, rect.Top + 2);
            g.DrawLine(Pens.White, rect.Left + 1, rect.Bottom - 4, rect.Left + 1, rect.Top + 3);
            g.DrawLine(cache.l1, rect.Left + 1, rect.Bottom - 4, rect.Left + 1, rect.Top + 3);

            // Draw the inside left, right and then bottom borders
            g.DrawLine(cache.leftPen, rect.Left + 4, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 3);
            g.DrawLine(cache.rightPen, rect.Left + 2, rect.Top + 2, rect.Right - 3, rect.Top + 2);
            g.DrawLine(Pens.White, rect.Right - 2, rect.Bottom - 3, rect.Right - 1, rect.Bottom - 2);
            g.DrawLine(cache.bottomInnerPen, rect.Right - 2, rect.Bottom - 3, rect.Right - 1, rect.Bottom - 2);
            g.DrawLine(Pens.White, rect.Right - 2, rect.Top + 2, rect.Right - 1, rect.Top + 1);
            g.DrawLine(cache.bottomInnerPen, rect.Right - 2, rect.Top + 2, rect.Right - 1, rect.Top + 1);
            g.DrawLine(Pens.White, rect.Right - 2, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 1);
            g.DrawLine(cache.bottomOuterPen, rect.Right - 2, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 1);
            g.DrawLine(Pens.White, rect.Right - 2, rect.Top + 1, rect.Right - 1, rect.Top);
            g.DrawLine(cache.bottomOuterPen, rect.Right - 2, rect.Top + 1, rect.Right - 1, rect.Top);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabContextSelectedRight(Rectangle rect,
                                                                 Color c2,
                                                                 MementoRibbonTabContextSelected cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 1, rect.Left + 2, rect.Bottom - 2);
            outsidePath.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Right - 2.5f, rect.Bottom - 2);
            outsidePath.AddLine(rect.Right - 2.5f, rect.Bottom - 2, rect.Right - 1, rect.Bottom - 4);
            outsidePath.AddLine(rect.Right - 1, rect.Bottom - 4, rect.Right - 1, rect.Top + 3);
            outsidePath.AddLine(rect.Right - 1, rect.Top + 3, rect.Right - 2.5f, rect.Top + 1);
            outsidePath.AddLine(rect.Right - 2.5f, rect.Top + 1, rect.Left + 2, rect.Top + 1);
            outsidePath.AddLine(rect.Left + 2, rect.Top + 1, rect.Left + 1, rect.Top);

            LinearGradientBrush leftBrush = new LinearGradientBrush(rect, Color.FromArgb(125, c2), Color.FromArgb(67, c2), 180f);
            LinearGradientBrush rightBrush = new LinearGradientBrush(rect, Color.FromArgb(16, c2), Color.FromArgb(67, c2), 180f);
            cache.leftPen = new Pen(leftBrush);
            cache.rightPen = new Pen(rightBrush);

            cache.interiorRect = new Rectangle(rect.Left, rect.Top + 2, rect.Width - 3, rect.Height - 4);
            cache.insideBrush = new LinearGradientBrush(rect, Color.FromArgb(134, c2), Color.FromArgb(50, c2), 180f);
            cache.outsidePath = outsidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabContextSelectedRightDraw(Rectangle rect,
                                                                     MementoRibbonTabContextSelected cache,
                                                                     Graphics g)
        {
            g.DrawLine(Pens.White, rect.Right - 4, rect.Bottom - 3, rect.Right - 4, rect.Top + 2);
            g.DrawLine(cache.l3, rect.Right - 4, rect.Bottom - 3, rect.Right - 4, rect.Top + 2);
            g.DrawLine(Pens.White, rect.Right - 3, rect.Bottom - 3, rect.Right - 3, rect.Top + 2);
            g.DrawLine(cache.l2, rect.Right - 3, rect.Bottom - 3, rect.Right - 3, rect.Top + 2);
            g.DrawLine(Pens.White, rect.Right - 2, rect.Bottom - 4, rect.Right - 2, rect.Top + 3);
            g.DrawLine(cache.l1, rect.Right - 2, rect.Bottom - 4, rect.Right - 2, rect.Top + 3);

            // Draw the inside left, right and then bottom borders
            g.DrawLine(cache.leftPen, rect.Right - 5, rect.Bottom - 3, rect.Left + 2, rect.Bottom - 3);
            g.DrawLine(cache.rightPen, rect.Right - 3, rect.Top + 2, rect.Left + 2, rect.Top + 2);
            g.DrawLine(Pens.White, rect.Left + 1, rect.Bottom - 3, rect.Left, rect.Bottom - 2);
            g.DrawLine(cache.bottomInnerPen, rect.Left + 1, rect.Bottom - 3, rect.Left, rect.Bottom - 2);
            g.DrawLine(Pens.White, rect.Left + 1, rect.Top + 2, rect.Left, rect.Top + 1);
            g.DrawLine(cache.bottomInnerPen, rect.Left + 1, rect.Top + 2, rect.Left, rect.Top + 1);
            g.DrawLine(Pens.White, rect.Left + 1, rect.Bottom - 2, rect.Left, rect.Bottom - 1);
            g.DrawLine(cache.bottomOuterPen, rect.Left + 1, rect.Bottom - 2, rect.Left, rect.Bottom - 1);
            g.DrawLine(Pens.White, rect.Left + 1, rect.Top + 1, rect.Left, rect.Top);
            g.DrawLine(cache.bottomOuterPen, rect.Left + 1, rect.Top + 1, rect.Left, rect.Top);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabContextSelectedBottom(Rectangle rect,
                                                                  Color c2,
                                                                  MementoRibbonTabContextSelected cache)
        {
            GraphicsPath outsidePath = new GraphicsPath();

            // Create path for a curved dark border around the tab
            outsidePath.AddLine(rect.Left, rect.Top + 1, rect.Left + 1, rect.Top + 2);
            outsidePath.AddLine(rect.Left + 1, rect.Top + 2, rect.Left + 1, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Left + 1, rect.Bottom - 2.5f, rect.Left + 3, rect.Bottom - 1);
            outsidePath.AddLine(rect.Left + 3, rect.Bottom - 1, rect.Right - 4, rect.Bottom - 1);
            outsidePath.AddLine(rect.Right - 4, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 2.5f);
            outsidePath.AddLine(rect.Right - 2, rect.Bottom - 2.5f, rect.Right - 2, rect.Top + 2);
            outsidePath.AddLine(rect.Right - 2, rect.Top + 2, rect.Right - 1, rect.Top + 1);

            LinearGradientBrush leftBrush = new LinearGradientBrush(rect, Color.FromArgb(125, c2), Color.FromArgb(67, c2), 270f);
            LinearGradientBrush rightBrush = new LinearGradientBrush(rect, Color.FromArgb(16, c2), Color.FromArgb(67, c2), 270f);
            cache.leftPen = new Pen(leftBrush);
            cache.rightPen = new Pen(rightBrush);

            cache.interiorRect = new Rectangle(rect.Left + 2, rect.Top, rect.Width - 4, rect.Height - 3);
            cache.insideBrush = new LinearGradientBrush(rect, Color.FromArgb(134, c2), Color.FromArgb(50, c2), 270f);
            cache.outsidePath = outsidePath;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabContextSelectedBottomDraw(Rectangle rect,
                                                                      MementoRibbonTabContextSelected cache,
                                                                      Graphics g)
        {
            g.DrawLine(Pens.White, rect.Left + 2, rect.Bottom - 4, rect.Right - 3, rect.Bottom - 4);
            g.DrawLine(cache.l3, rect.Left + 2, rect.Bottom - 4, rect.Right - 3, rect.Bottom - 4);
            g.DrawLine(Pens.White, rect.Left + 2, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 3);
            g.DrawLine(cache.l2, rect.Left + 2, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 3);
            g.DrawLine(Pens.White, rect.Left + 3, rect.Bottom - 2, rect.Right - 4, rect.Bottom - 2);
            g.DrawLine(cache.l1, rect.Left + 3, rect.Bottom - 2, rect.Right - 4, rect.Bottom - 2);

            // Draw the inside left, right and then bottom borders
            g.DrawLine(cache.leftPen, rect.Left + 2, rect.Bottom - 5, rect.Left + 2, rect.Top + 2);
            g.DrawLine(cache.rightPen, rect.Right - 3, rect.Bottom - 3, rect.Right - 3, rect.Top + 2);
            g.DrawLine(Pens.White, rect.Left + 2, rect.Top + 1, rect.Left + 1, rect.Top);
            g.DrawLine(cache.bottomInnerPen, rect.Left + 2, rect.Top + 1, rect.Left + 1, rect.Top);
            g.DrawLine(Pens.White, rect.Right - 3, rect.Top + 1, rect.Right - 2, rect.Top);
            g.DrawLine(cache.bottomInnerPen, rect.Right - 3, rect.Top + 1, rect.Right - 2, rect.Top);
            g.DrawLine(Pens.White, rect.Left + 1, rect.Top + 1, rect.Left, rect.Top);
            g.DrawLine(cache.bottomOuterPen, rect.Left + 1, rect.Top + 1, rect.Left, rect.Top);
            g.DrawLine(Pens.White, rect.Right - 2, rect.Top + 1, rect.Right - 1, rect.Top);
            g.DrawLine(cache.bottomOuterPen, rect.Right - 2, rect.Top + 1, rect.Right - 1, rect.Top);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonTabHighlight(PaletteRibbonShape shape,
                                                             RenderContext context, 
                                                             Rectangle rect,
                                                             PaletteState state,
                                                             IPaletteRibbonBack palette,
                                                             VisualOrientation orientation,
                                                             IDisposable memento,
                                                             bool alternate)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                bool generate = true;
                MementoRibbonTabHighlight cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabHighlight))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabHighlight(rect, c1, c2, c3, c4, c5, orientation);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabHighlight)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, c5, orientation);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                            DrawRibbonTabHighlightTop(rect, c4, c5, cache);
                            break;
                        case VisualOrientation.Left:
                            DrawRibbonTabHighlightLeft(rect, c4, c5, cache);
                            break;
                        case VisualOrientation.Right:
                            DrawRibbonTabHighlightRight(rect, c4, c5, cache);
                            break;
                        case VisualOrientation.Bottom:
                            DrawRibbonTabHighlightBottom(rect, c4, c5, cache);
                            break;
                    }       

                    cache.innerVertPen = new Pen(c1);
                    cache.innerHorzPen = new Pen(c2);
                    cache.borderHorzPen = new Pen(c3);
                }

                // First of all draw as selected
                cache.selectedMemento = (MementoRibbonTabSelected2007)DrawRibbonTabSelected2007(context, rect, PaletteState.CheckedNormal, palette, orientation, cache.selectedMemento);

                switch (orientation)
                {
                    case VisualOrientation.Top:
                        DrawRibbonTabHighlightTopDraw(rect, c1, c2, c3, c4, c5, cache, context.Graphics, alternate);
                        break;
                    case VisualOrientation.Left:
                        DrawRibbonTabHighlightLeftDraw(rect, c1, c2, c3, c4, c5, cache, context.Graphics, alternate);
                        break;
                    case VisualOrientation.Right:
                        DrawRibbonTabHighlightRightDraw(rect, c1, c2, c3, c4, c5, cache, context.Graphics, alternate);
                        break;
                    case VisualOrientation.Bottom:
                        DrawRibbonTabHighlightBottomDraw(rect, c1, c2, c3, c4, c5, cache, context.Graphics, alternate);
                        break;
                }
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabHighlightTop(Rectangle rect,
                                                         Color c4, Color c5,
                                                         MementoRibbonTabHighlight cache)
        {
            RectangleF hF = new RectangleF(rect.Left - 2, rect.Top - 1, rect.Width + 4, 6);
            RectangleF vF = new RectangleF(rect.Left - 2, rect.Top + 1, rect.Width + 4, rect.Height - 1);
            cache.topBorderBrush = new LinearGradientBrush(hF, Color.FromArgb(48, c5), Color.FromArgb(64, c5), 90f);
            cache.borderVertBrush = new LinearGradientBrush(vF, c5, c4, 90f);
            cache.outsideVertBrush = new LinearGradientBrush(vF, Color.FromArgb(48, c5), c5, 90f);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabHighlightTopDraw(Rectangle rect,
                                                             Color c1, Color c2,
                                                             Color c3, Color c4,
                                                             Color c5,
                                                             MementoRibbonTabHighlight cache,
                                                             Graphics g,
                                                             bool alternate)
        {
            g.FillRectangle(cache.topBorderBrush, rect.Left - 1, rect.Top - 1, rect.Width + 2, 4);
            g.DrawLine(cache.innerVertPen, rect.Left + 2, rect.Bottom - 2, rect.Left + 2, rect.Top + 3);
            g.DrawLine(cache.innerVertPen, rect.Right - 3, rect.Bottom - 2, rect.Right - 3, rect.Top + 3);
            g.DrawLine(cache.innerHorzPen, rect.Left + 2, rect.Top + 2, rect.Right - 3, rect.Top + 2);

            if (alternate)
            {
                g.DrawLine(cache.innerHorzPen, rect.Left + 2, rect.Top + 1, rect.Right - 3, rect.Top + 1);
                g.DrawLine(cache.borderHorzPen, rect.Left + 3, rect.Top, rect.Right - 4, rect.Top);
            }
            else
            {
                g.DrawLine(cache.innerHorzPen, rect.Left + 3, rect.Top + 1, rect.Right - 4, rect.Top + 1);
                g.DrawLine(cache.borderHorzPen, rect.Left + 4, rect.Top, rect.Right - 5, rect.Top);
            }

            g.FillRectangle(cache.borderVertBrush, rect.Left + 1, rect.Top + 2, 1, rect.Height - 3);
            g.FillRectangle(cache.borderVertBrush, rect.Right - 2, rect.Top + 2, 1, rect.Height - 3);
            g.FillRectangle(cache.outsideVertBrush, rect.Left, rect.Top + 3, 1, rect.Height - 4);
            g.FillRectangle(cache.outsideVertBrush, rect.Left - 1, rect.Top + 3, 1, rect.Height - 4);
            g.FillRectangle(cache.outsideVertBrush, rect.Right - 1, rect.Top + 3, 1, rect.Height - 4);
            g.FillRectangle(cache.outsideVertBrush, rect.Right, rect.Top + 3, 1, rect.Height - 4);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabHighlightLeft(Rectangle rect,
                                                          Color c4, Color c5,
                                                          MementoRibbonTabHighlight cache)
        {
            RectangleF hF = new RectangleF(rect.Left - 1, rect.Top - 2, 6, rect.Height - 4);
            RectangleF vF = new RectangleF(rect.Left + 1, rect.Top - 2, rect.Width - 1, rect.Height - 4);
            cache.topBorderBrush = new LinearGradientBrush(hF, Color.FromArgb(48, c5), Color.FromArgb(64, c5), 0f);
            cache.borderVertBrush = new LinearGradientBrush(vF, c5, c4, 0f);
            cache.outsideVertBrush = new LinearGradientBrush(vF, Color.FromArgb(48, c5), c5, 0f);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabHighlightLeftDraw(Rectangle rect,
                                                              Color c1, Color c2,
                                                              Color c3, Color c4,
                                                              Color c5,
                                                              MementoRibbonTabHighlight cache,
                                                              Graphics g,
                                                              bool alternate)
        {
            g.FillRectangle(cache.topBorderBrush, rect.Left - 1, rect.Top - 1, 4, rect.Height + 2);
            g.DrawLine(cache.innerVertPen, rect.Right - 2, rect.Bottom - 3, rect.Left + 3, rect.Bottom - 3);
            g.DrawLine(cache.innerVertPen, rect.Right - 2, rect.Top + 2, rect.Left + 3, rect.Top + 2);
            g.DrawLine(cache.innerHorzPen, rect.Left + 2, rect.Bottom - 3, rect.Left + 2, rect.Top + 2);

            if (alternate)
            {
                g.DrawLine(cache.innerHorzPen, rect.Left + 1, rect.Bottom - 3, rect.Left + 1, rect.Top + 2);
                g.DrawLine(cache.borderHorzPen, rect.Left, rect.Bottom - 4, rect.Left, rect.Top + 3);
            }
            else
            {
                g.DrawLine(cache.innerHorzPen, rect.Left + 1, rect.Bottom - 4, rect.Left + 1, rect.Top + 3);
                g.DrawLine(cache.borderHorzPen, rect.Left, rect.Bottom - 5, rect.Left, rect.Top + 4);
            }

            g.FillRectangle(cache.borderVertBrush, rect.Left + 2, rect.Bottom - 2, rect.Width - 3, 1);
            g.FillRectangle(cache.borderVertBrush, rect.Left + 2, rect.Top + 1, rect.Width - 3, 1);
            g.FillRectangle(cache.outsideVertBrush, rect.Left + 3, rect.Bottom - 1, rect.Width - 4, 1);
            g.FillRectangle(cache.outsideVertBrush, rect.Left + 3, rect.Bottom, rect.Width - 4, 1);
            g.FillRectangle(cache.outsideVertBrush, rect.Left + 3, rect.Top, rect.Width - 4, 1);
            g.FillRectangle(cache.outsideVertBrush, rect.Left + 3, rect.Top - 1, rect.Width - 4, 1);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabHighlightRight(Rectangle rect,
                                                           Color c4, Color c5,
                                                           MementoRibbonTabHighlight cache)
        {
            RectangleF hF = new RectangleF(rect.Right - 6, rect.Top - 2, 6, rect.Height - 4);
            RectangleF vF = new RectangleF(rect.Left, rect.Top - 2, rect.Width - 1, rect.Height - 4);
            cache.topBorderBrush = new LinearGradientBrush(hF, Color.FromArgb(48, c5), Color.FromArgb(64, c5), 180f);
            cache.borderVertBrush = new LinearGradientBrush(vF, c5, c4, 180f);
            cache.outsideVertBrush = new LinearGradientBrush(vF, Color.FromArgb(48, c5), c5, 180f);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabHighlightRightDraw(Rectangle rect,
                                                               Color c1, Color c2,
                                                               Color c3, Color c4,
                                                               Color c5,
                                                               MementoRibbonTabHighlight cache,
                                                               Graphics g,
                                                               bool alternate)
        {
            g.FillRectangle(cache.topBorderBrush, rect.Right - 4, rect.Top - 1, 4, rect.Height + 2);
            g.DrawLine(cache.innerVertPen, rect.Left + 1, rect.Bottom - 3, rect.Right - 4, rect.Bottom - 3);
            g.DrawLine(cache.innerVertPen, rect.Left + 1, rect.Top + 2, rect.Right - 4, rect.Top + 2);
            g.DrawLine(cache.innerHorzPen, rect.Right - 3, rect.Bottom - 3, rect.Right - 3, rect.Top + 2);

            if (alternate)
            {
                g.DrawLine(cache.innerHorzPen, rect.Right - 2, rect.Bottom - 3, rect.Right - 2, rect.Top + 2);
                g.DrawLine(cache.borderHorzPen, rect.Right - 1, rect.Bottom - 4, rect.Right - 1, rect.Top + 3);
            }
            else
            {
                g.DrawLine(cache.innerHorzPen, rect.Right - 2, rect.Bottom - 4, rect.Right - 2, rect.Top + 3);
                g.DrawLine(cache.borderHorzPen, rect.Right - 1, rect.Bottom - 5, rect.Right - 1, rect.Top + 4);
            }

            g.FillRectangle(cache.borderVertBrush, rect.Left + 1, rect.Bottom - 2, rect.Width - 3, 1);
            g.FillRectangle(cache.borderVertBrush, rect.Left + 1, rect.Top + 1, rect.Width - 3, 1);
            g.FillRectangle(cache.outsideVertBrush, rect.Left + 1, rect.Bottom - 1, rect.Width - 4, 1);
            g.FillRectangle(cache.outsideVertBrush, rect.Left + 1, rect.Bottom, rect.Width - 4, 1);
            g.FillRectangle(cache.outsideVertBrush, rect.Left + 1, rect.Top, rect.Width - 4, 1);
            g.FillRectangle(cache.outsideVertBrush, rect.Left + 1, rect.Top - 1, rect.Width - 4, 1);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabHighlightBottom(Rectangle rect,
                                                            Color c4, Color c5,
                                                            MementoRibbonTabHighlight cache)
        {
            RectangleF hF = new RectangleF(rect.Left - 2, rect.Bottom - 6, rect.Width + 4, 6);
            RectangleF vF = new RectangleF(rect.Left - 2, rect.Top, rect.Width + 4, rect.Height - 1);
            cache.topBorderBrush = new LinearGradientBrush(hF, Color.FromArgb(48, c5), Color.FromArgb(64, c5), 270f);
            cache.borderVertBrush = new LinearGradientBrush(vF, c5, c4, 270f);
            cache.outsideVertBrush = new LinearGradientBrush(vF, Color.FromArgb(48, c5), c5, 270f);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonTabHighlightBottomDraw(Rectangle rect,
                                                        Color c1, Color c2,
                                                        Color c3, Color c4,
                                                        Color c5,
                                                        MementoRibbonTabHighlight cache,
                                                        Graphics g,
                                                        bool alternate)
        {
            g.FillRectangle(cache.topBorderBrush, rect.Left - 1, rect.Bottom - 3, rect.Width + 2, 4);
            g.DrawLine(cache.innerVertPen, rect.Left + 2, rect.Top + 1, rect.Left + 2, rect.Bottom - 4);
            g.DrawLine(cache.innerVertPen, rect.Right - 3, rect.Top + 1, rect.Right - 3, rect.Bottom - 4);
            g.DrawLine(cache.innerHorzPen, rect.Left + 2, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 3);

            if (alternate)
            {
                g.DrawLine(cache.innerHorzPen, rect.Left + 2, rect.Bottom - 2, rect.Right - 3, rect.Bottom - 2);
                g.DrawLine(cache.borderHorzPen, rect.Left + 3, rect.Bottom - 1, rect.Right - 4, rect.Bottom - 1);
            }
            else
            {
                g.DrawLine(cache.innerHorzPen, rect.Left + 3, rect.Bottom - 2, rect.Right - 4, rect.Bottom - 2);
                g.DrawLine(cache.borderHorzPen, rect.Left + 4, rect.Bottom - 1, rect.Right - 5, rect.Bottom - 1);
            }

            g.FillRectangle(cache.borderVertBrush, rect.Left + 1, rect.Top + 1, 1, rect.Height - 3);
            g.FillRectangle(cache.borderVertBrush, rect.Right - 2, rect.Top + 1, 1, rect.Height - 3);
            g.FillRectangle(cache.outsideVertBrush, rect.Left, rect.Top + 1, 1, rect.Height - 4);
            g.FillRectangle(cache.outsideVertBrush, rect.Left - 1, rect.Top + 1, 1, rect.Height - 4);
            g.FillRectangle(cache.outsideVertBrush, rect.Right - 1, rect.Top + 1, 1, rect.Height - 4);
            g.FillRectangle(cache.outsideVertBrush, rect.Right, rect.Top + 1, 1, rect.Height - 4);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonTabContext(RenderContext context,
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
                MementoRibbonTabContext cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonTabContext))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonTabContext(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonTabContext)memento;
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

                    cache.fillBrush = new LinearGradientBrush(borderRect, Color.FromArgb(196, c2), Color.Transparent, 270f);
                    cache.fillBrush.Blend = _ribbonGroup6Blend;
                }

                // Draw the left and right border lines
                context.Graphics.DrawLine(cache.borderPen, rect.X, rect.Y, rect.X, rect.Bottom - 1);
                context.Graphics.DrawLine(cache.borderPen, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 1);

                // Fill the inner area with a gradient context specific color
                context.Graphics.FillRectangle(cache.fillBrush, cache.fillRect);

                // Overdraw the brighter line at bottom
                context.Graphics.DrawLine(cache.underlinePen, rect.X + 1, rect.Bottom - 1, rect.Right - 2, rect.Bottom - 1);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonAppButton(PaletteRibbonShape shape,
                                                          RenderContext context,
                                                          Rectangle rect,
                                                          PaletteState state,
                                                          IPaletteRibbonBack palette,
                                                          bool trackBorderAsPressed,
                                                          IDisposable memento)
        {
            // Reduce the area of the actual button as the extra space is used for shadow
            rect.Width -= 3;
            rect.Height -= 3;

            if ((rect.Width > 0) && (rect.Height > 0))
            {
                // Get the colors from the palette
                Color topLight = palette.GetRibbonBackColor1(state);
                Color topMedium = palette.GetRibbonBackColor2(state);
                Color topDark = palette.GetRibbonBackColor3(state);
                Color bottomLight = palette.GetRibbonBackColor4(state);
                Color bottomMedium = palette.GetRibbonBackColor5(state);
                Color bottomDark = CommonHelper.MergeColors(topDark, 0.78f, Color.Empty, 0.22f);

                bool generate = true;
                MementoRibbonAppButton cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonAppButton))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonAppButton(rect, topLight, topMedium,
                                                       topDark, bottomLight, bottomMedium);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonAppButton)memento;
                    generate = !cache.UseCachedValues(rect, topLight, topMedium,
                                                      topDark, bottomLight, bottomMedium);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.borderShadow1 = new RectangleF(rect.X, rect.Y, rect.Width + 2, rect.Height + 2);
                    cache.borderShadow2 = new RectangleF(rect.X, rect.Y, rect.Width + 1, rect.Height + 1);
                    cache.borderMain1 = new RectangleF(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
                    cache.borderMain2 = new RectangleF(cache.borderMain1.X + 1, cache.borderMain1.Y + 1, cache.borderMain1.Width - 2, cache.borderMain1.Height - 2);
                    cache.borderMain3 = new RectangleF(cache.borderMain1.X + 1, cache.borderMain1.Y + 1, cache.borderMain1.Width - 2, cache.borderMain1.Height - 2);
                    cache.borderMain4 = new RectangleF(cache.borderMain2.X, cache.borderMain2.Y + 1, cache.borderMain2.Width, cache.borderMain2.Height - 2);
                    cache.rectBottomGlow = new RectangleF(0, 0, rect.Width * 0.75f, rect.Height * 0.75f);
                    cache.rectLower = new RectangleF(rect.X, rect.Y - 1, rect.Width, rect.Height + 1);
                    cache.rectUpperGlow = new RectangleF();
                    cache.rectUpperGlow.Width = rect.Width - 4;
                    cache.rectUpperGlow.Height = rect.Height / 8;
                    cache.rectUpperGlow.Y = rect.Y + (rect.Height - cache.rectUpperGlow.Height) / 2;
                    cache.rectUpperGlow.X = rect.X + (rect.Width - cache.rectUpperGlow.Width) / 2;

                    cache.brushUpper1 = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal);
                    cache.brushLower = new LinearGradientBrush(cache.rectLower, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal);
                }

                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    DrawRibbonAppButtonBorder1(context.Graphics, cache);
                    DrawRibbonAppButtonUpperHalf(context.Graphics, cache, state, topDark, bottomDark, topLight, topMedium, trackBorderAsPressed);
                    DrawRibbonAppButtonLowerHalf(context.Graphics, cache, state, bottomDark, bottomLight, bottomMedium);
                    DrawRibbonAppButtonGlowCenter(context.Graphics, cache, state, topLight, bottomLight);
                    DrawRibbonAppButtonGlowUpperBottom(context.Graphics, cache, state, bottomLight, bottomMedium, bottomDark);
                    DrawRibbonAppButtonBorder2(context.Graphics, cache, state, bottomLight, trackBorderAsPressed);
                }
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonAppButtonBorder1(Graphics g,
                                                          MementoRibbonAppButton memento)
        {
            g.FillEllipse(_buttonBorder1Brush, memento.borderShadow1);
            g.FillEllipse(_buttonBorder2Brush, memento.borderShadow2);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonAppButtonUpperHalf(Graphics g,
                                                            MementoRibbonAppButton memento,
                                                            PaletteState state,
                                                            Color topDark,
                                                            Color bottomDark,
                                                            Color topLight,
                                                            Color topMedium,
                                                            bool trackBorderAsPressed)
        {
            bool pressed = (state == PaletteState.Pressed);
            bool tracking = (state == PaletteState.Tracking);

            // Override tracking/pressed states?
            if (tracking && trackBorderAsPressed)
            {
                pressed = true;
                tracking = false;
            }

            if (!pressed)
            {
                Color[] colorsUpperHalf = new Color[] { topDark, topMedium, topLight, topLight, topMedium, topDark };
                float[] posUpperHalf = new float[] { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };

                ColorBlend blendUpperHalf = new ColorBlend();
                blendUpperHalf.Colors = colorsUpperHalf;
                blendUpperHalf.Positions = posUpperHalf;
                memento.brushUpper1.InterpolationColors = blendUpperHalf;

                g.FillPie(memento.brushUpper1, memento.rect.X, memento.rect.Y, memento.rect.Width, memento.rect.Height, 180, 180);
            }

            Color c1 = _whiten10;
            Color c2 = Color.FromArgb(100, topDark);

            if (tracking)
            {
                c1 = _whiten200;
                c2 = Color.FromArgb(200, bottomDark);
            }

            if (pressed)
            {
                c1 = Color.White;
                c2 = topDark;
            }

            using (LinearGradientBrush brushUpper2 = new LinearGradientBrush(memento.rect, c1, c2, LinearGradientMode.Vertical))
                g.FillPie(brushUpper2, memento.rect.X, memento.rect.Y, memento.rect.Width, memento.rect.Height, 180, 180);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonAppButtonLowerHalf(Graphics g,
                                                            MementoRibbonAppButton memento,
                                                            PaletteState state,
                                                            Color bottomDark,
                                                            Color bottomLight,
                                                            Color bottomMedium)
        {
            Color[] colorsLowerHalf = new Color[] { bottomDark, bottomMedium, bottomLight, bottomLight, bottomMedium, bottomDark };
            float[] posLowerHalf;

            if (state == PaletteState.Pressed)
                posLowerHalf = new float[] { 0.0f, 0.3f, 0.5f, 0.5f, 0.7f, 1.0f };
            else
                posLowerHalf = new float[] { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };

            ColorBlend blendLowerHalf = new ColorBlend();
            blendLowerHalf.Colors = colorsLowerHalf;
            blendLowerHalf.Positions = posLowerHalf;
            
            memento.brushLower.InterpolationColors = blendLowerHalf;
            g.FillPie(memento.brushLower, memento.rectLower.X, memento.rectLower.Y, memento.rectLower.Width, memento.rectLower.Height, 0, 180);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonAppButtonGlowCenter(Graphics g,
                                                             MementoRibbonAppButton memento,
                                                             PaletteState state,
                                                             Color topLight,
                                                             Color bottomLight)
        {
            using (LinearGradientBrush brushBottomGlow = new LinearGradientBrush(memento.rectBottomGlow, Color.FromArgb(50, Color.White), Color.FromArgb(30, Color.White), LinearGradientMode.Vertical))
            {
                RectangleF rectBottomGlow = memento.rectBottomGlow;
                rectBottomGlow.X = memento.rect.X + (memento.rect.Width - rectBottomGlow.Width) / 2;
                rectBottomGlow.Y = memento.rect.Y + (memento.rect.Height - rectBottomGlow.Height - 2);

                if (state != PaletteState.Pressed)
                    g.FillPie(brushBottomGlow, rectBottomGlow.X, rectBottomGlow.Y, rectBottomGlow.Width, rectBottomGlow.Height, 0, 360);

                if (state == PaletteState.Pressed)
                {
                    rectBottomGlow.Height = memento.rect.Height * 0.2f;
                    rectBottomGlow.Width = memento.rect.Width * 0.4f;
                    rectBottomGlow.X = memento.rect.X + (memento.rect.Width - rectBottomGlow.Width) / 2;
                    rectBottomGlow.Y = memento.rect.Y + (memento.rect.Height - rectBottomGlow.Height);

                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddEllipse(rectBottomGlow);
                        using (PathGradientBrush pathGradient = new PathGradientBrush(path))
                        {
                            pathGradient.CenterColor = topLight;
                            pathGradient.SurroundColors = new Color[] { Color.FromArgb(100, bottomLight) };
                            g.FillEllipse(pathGradient, rectBottomGlow);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonAppButtonGlowUpperBottom(Graphics g,
                                                                  MementoRibbonAppButton memento,
                                                                  PaletteState state,
                                                                  Color bottomLight,
                                                                  Color bottomMedium,
                                                                  Color bottomDark)
        {
            int lightTransparency = 50;
            int mediumTransparency = 50;

            if (state == PaletteState.Pressed || state == PaletteState.Tracking)
            {
                lightTransparency = 200;
                mediumTransparency = 200;
            }

            using (LinearGradientBrush brushUpperGlow = new LinearGradientBrush(memento.rectUpperGlow, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal))
            {
                Color[] colorsUpperGlow = new Color[] { Color.FromArgb(180, bottomDark), 
                                                        Color.FromArgb(mediumTransparency, bottomMedium), 
                                                        Color.FromArgb(lightTransparency, bottomLight), 
                                                        Color.FromArgb(lightTransparency, bottomLight), 
                                                        Color.FromArgb(mediumTransparency, bottomMedium), 
                                                        Color.FromArgb(180, bottomDark) };

                float[] posUpperGlow = new float[] { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };

                ColorBlend blendUpperGlow = new ColorBlend();
                blendUpperGlow.Colors = colorsUpperGlow;
                blendUpperGlow.Positions = posUpperGlow;
                brushUpperGlow.InterpolationColors = blendUpperGlow;

                g.FillPie(brushUpperGlow, memento.rectUpperGlow.X, memento.rectUpperGlow.Y, memento.rectUpperGlow.Width, memento.rectUpperGlow.Height, 180, 180);
            }
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual void DrawRibbonAppButtonBorder2(Graphics g,
                                                          MementoRibbonAppButton memento,
                                                          PaletteState state,
                                                          Color bottomLight,
                                                          bool trackBorderAsPressed)
        {
            bool pressed = (state == PaletteState.Pressed);
            bool tracking = (state == PaletteState.Tracking);

            // Override tracking/pressed states?
            if (tracking && trackBorderAsPressed)
            {
                pressed = true;
                tracking = false;
            }

            Color borderGlowColor;
            if (pressed)
                borderGlowColor = _whiten80;
            else if (tracking && !pressed)
                borderGlowColor = Color.FromArgb(200, bottomLight);
            else
                borderGlowColor = _whiten120;

            using (Pen p = new Pen(borderGlowColor))
                g.DrawEllipse(p, memento.borderMain1);

            using (Pen p = new Pen(Color.FromArgb(100, 52, 59, 64)))
                g.DrawEllipse(p, memento.rect);

            if (pressed)
            {
                borderGlowColor = _whiten60;
                using (Pen p = new Pen(borderGlowColor))
                    g.DrawEllipse(p, memento.borderMain3);
            }

            borderGlowColor = (pressed ? _whiten50 : _whiten80);
            using (Pen p = new Pen(borderGlowColor))
                g.DrawArc(p, memento.borderMain2, 180, 180);

            if (!pressed)
            {
                borderGlowColor = _whiten30;
                using (Pen p = new Pen(borderGlowColor))
                    g.DrawArc(p, memento.borderMain4, 180, 180);
            }

            if (tracking && !pressed)
                using (Pen p = new Pen(Color.FromArgb(100, borderGlowColor)))
                    g.DrawEllipse(p, memento.rect);
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonAppTab(PaletteRibbonShape shape,
                                                       RenderContext context,
                                                       Rectangle rect,
                                                       PaletteState state,
                                                       Color baseColor1,
                                                       Color baseColor2,
                                                       IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                bool generate = true;
                MementoRibbonAppTab cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonAppTab))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonAppTab(rect, baseColor1, baseColor2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonAppTab)memento;
                    generate = !cache.UseCachedValues(rect, baseColor1, baseColor2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    // Create common paths to all the app tab states
                    cache.GeneratePaths(rect, state);
                    cache.borderPen = new Pen(baseColor1);
                    
                    // Create state specific colors/brushes/pens
                    switch (state)
                    {
                        case PaletteState.Normal:
                            cache.borderBrush = new SolidBrush(CommonHelper.MergeColors(baseColor1, 0.2f, baseColor2, 0.8f));
                            cache.insideFillBrush = new LinearGradientBrush(new RectangleF(rect.X, rect.Y + 1, rect.Width, rect.Height),
                                                                            CommonHelper.MergeColors(baseColor1, 0.3f, baseColor2, 0.7f),
                                                                            CommonHelper.MergeColors(baseColor1, 0.6f, baseColor2, 0.4f),
                                                                            90f);

                            cache.insideFillBrush.SetSigmaBellShape(0.33f);
                            cache.highlightBrush.CenterColor = Color.FromArgb(64, Color.White);
                            break;
                        case PaletteState.Tracking:
                            cache.borderBrush = new SolidBrush(baseColor2);
                            cache.insideFillBrush = new LinearGradientBrush(new RectangleF(rect.X, rect.Y + 1, rect.Width, rect.Height),
                                                                            CommonHelper.MergeColors(baseColor1, 0.3f, baseColor2, 0.7f),
                                                                            CommonHelper.MergeColors(baseColor1, 0.6f, baseColor2, 0.4f),
                                                                            90f);

                            cache.insideFillBrush.SetSigmaBellShape(0.33f);
                            cache.highlightBrush.CenterColor = Color.FromArgb(100, Color.White);
                            break;
                        case PaletteState.Tracking | PaletteState.FocusOverride:
                            cache.borderBrush = new SolidBrush(ControlPaint.LightLight(baseColor2));
                            cache.insideFillBrush = new LinearGradientBrush(new RectangleF(rect.X, rect.Y + 1, rect.Width, rect.Height),
                                                                            CommonHelper.MergeColors(baseColor1, 0.3f, baseColor2, 0.7f),
                                                                            CommonHelper.MergeColors(baseColor1, 0.6f, baseColor2, 0.4f),
                                                                            90f);

                            cache.insideFillBrush.SetSigmaBellShape(0.33f);
                            cache.highlightBrush.CenterColor = ControlPaint.LightLight(baseColor2);
                            break;
                        case PaletteState.Pressed:
                            cache.borderBrush = new SolidBrush(CommonHelper.MergeColors(baseColor1, 0.5f, baseColor2, 0.5f));
                            cache.insideFillBrush = new LinearGradientBrush(new RectangleF(rect.X, rect.Y + 1, rect.Width, rect.Height),
                                                                            CommonHelper.MergeColors(baseColor1, 0.3f, baseColor2, 0.7f),
                                                                            CommonHelper.MergeColors(baseColor1, 0.75f, baseColor2, 0.25f),
                                                                            90f);

                            cache.insideFillBrush.SetSigmaBellShape(0f);
                            cache.highlightBrush.CenterColor = Color.FromArgb(90, Color.White);
                            break;
                    }
                }

                // Fill the entire tab area and then add a border around the edge
                context.Graphics.FillPath(cache.borderBrush, cache.borderFillPath);

                // Draw the outside border
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.DrawPath(cache.borderPen, cache.borderPath);

                // Fill inside area
                context.Graphics.FillPath(cache.insideFillBrush, cache.insideFillPath);

                // Draw highlight over bottom half
                using(Clipping clip = new Clipping(context.Graphics, cache.insideFillPath))
                    context.Graphics.FillPath(cache.highlightBrush, cache.highlightPath);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupNormalBorder(RenderContext context,
                                                                  Rectangle rect,
                                                                  PaletteState state,
                                                                  IPaletteRibbonBack palette,
                                                                  bool tracking,
                                                                  bool lightInside,
                                                                  IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonGroupNormalBorder cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupNormalBorder))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupNormalBorder(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupNormalBorder)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    GraphicsPath solidPath = new GraphicsPath();
                    GraphicsPath insidePath = new GraphicsPath();
                    GraphicsPath outsidePath = new GraphicsPath();
                    GraphicsPath lightPath = new GraphicsPath();

                    // Create the rounded complete border
                    solidPath.AddLine(rect.Left + 2, rect.Top, rect.Right - 4, rect.Top);
                    solidPath.AddLine(rect.Right - 4, rect.Top, rect.Right - 2, rect.Top + 2);
                    solidPath.AddLine(rect.Right - 2, rect.Top + 2, rect.Right - 2, rect.Bottom - 4);
                    solidPath.AddLine(rect.Right - 2, rect.Bottom - 4, rect.Right - 4, rect.Bottom - 2);
                    solidPath.AddLine(rect.Right - 4, rect.Bottom - 2, rect.Left + 2, rect.Bottom - 2);
                    solidPath.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Left, rect.Bottom - 4);
                    solidPath.AddLine(rect.Left, rect.Bottom - 4, rect.Left, rect.Top + 2);
                    solidPath.AddLine(rect.Left, rect.Top + 2, rect.Left + 2, rect.Top);

                    // Create the inside top and left path
                    insidePath.AddLine(rect.Right - 4, rect.Top + 1, rect.Left + 2, rect.Top + 1);
                    insidePath.AddLine(rect.Left + 2, rect.Top + 1, rect.Left + 1, rect.Top + 2);
                    insidePath.AddLine(rect.Left + 1, rect.Top + 2, rect.Left + 1, rect.Bottom - 4);

                    // Create the outside right and bottom path
                    outsidePath.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 3);
                    outsidePath.AddLine(rect.Right - 1, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 1);
                    outsidePath.AddLine(rect.Right - 3, rect.Bottom - 1, rect.Left + 3, rect.Bottom - 1);

                    // Optional path for lightening area
                    lightPath.AddLine(rect.Left + 2, rect.Top + 1, rect.Right - 4, rect.Top + 1);
                    lightPath.AddLine(rect.Right - 4, rect.Top + 1, rect.Right - 3, rect.Top + 2);
                    lightPath.AddLine(rect.Right - 3, rect.Top + 2, rect.Right - 3, rect.Bottom - 4);
                    lightPath.AddLine(rect.Right - 3, rect.Bottom - 4, rect.Right - 4, rect.Bottom - 3);
                    lightPath.AddLine(rect.Right - 4, rect.Bottom - 3, rect.Left + 2, rect.Bottom - 3);
                    lightPath.AddLine(rect.Left + 2, rect.Bottom - 3, rect.Left + 1, rect.Bottom - 4);
                    lightPath.AddLine(rect.Left + 1, rect.Bottom - 4, rect.Left + 1, rect.Top + 2);
                    lightPath.AddLine(rect.Left + 1, rect.Top + 2, rect.Left + 2, rect.Top + 1);

                    RectangleF solidRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
                    LinearGradientBrush solidBrush = new LinearGradientBrush(solidRectF, c1, c2, 90f);
                    cache.solidPen = new Pen(solidBrush);

                    cache.backRect = new Rectangle(rect.Left + 2, rect.Top + 1, rect.Width - 4, rect.Height - 4);
                    cache.solidPath = solidPath;
                    cache.insidePath = insidePath;
                    cache.outsidePath = outsidePath;
                    cache.lightPath = lightPath;
                }

                // If tracking, lighten the background
                if (tracking)
                    context.Graphics.FillRectangle(lightInside ? _whitenLightLBrush : _whitenLightBrush, cache.backRect);

                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    // Draw the solid border
                    context.Graphics.DrawPath(cache.solidPen, cache.solidPath);

                    // Do now draw the inside and outside paths if ligtening the inside anyway
                    if (!lightInside)
                    {
                        // Draw the two areas that make a lighter shadow to the right and bottom of border
                        context.Graphics.DrawPath(_whitenMediumPen, cache.insidePath);
                        context.Graphics.DrawPath(_whitenMediumPen, cache.outsidePath);
                    }
                }

                if (lightInside)
                    context.Graphics.DrawPath(Pens.White, cache.lightPath);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupNormalBorderSep(bool showingInPopup,
                                                                     RenderContext context,
                                                                     Rectangle rect,
                                                                     PaletteState state,
                                                                     IPaletteRibbonBack palette,
                                                                     IDisposable memento,
                                                                     bool pressed,
                                                                     bool tracking,
                                                                     bool dark)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                bool generate = true;
                MementoRibbonGroupNormalBorderSep cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupNormalBorderSep))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupNormalBorderSep(rect, c1, c2, c3, c4, c5, tracking, dark);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupNormalBorderSep)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, c5, tracking, dark);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    RectangleF rectF = new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2);
                    cache.totalBrush = new LinearGradientBrush(rectF, c2, c1, 90f);
                    cache.innerBrush = new LinearGradientBrush(rectF, c4, c3, 90f);
                    cache.trackSepBrush = new LinearGradientBrush(rectF, c5, c2, 90f);
                    cache.totalBrush.Blend = _ribbonGroup9Blend;
                    cache.innerBrush.Blend = _ribbonGroup9Blend;
                    cache.trackSepBrush.Blend = _ribbonGroup9Blend;
                    cache.innerPen = new Pen(cache.innerBrush);
                    cache.trackSepPen = new Pen(cache.trackSepBrush);
                    cache.trackBottomPen = new Pen(c5);

                    GraphicsPath trackingPath = new GraphicsPath();
                    trackingPath.AddEllipse(new Rectangle(rect.X, rect.Y + ( rect.Height / 2), rect.Width - 3, rect.Height));
                    cache.trackHighlightBrush = new PathGradientBrush(trackingPath);
                    cache.trackHighlightBrush.SurroundColors = new Color[] { Color.Transparent };
                    cache.trackHighlightBrush.CenterColor = (dark ? (rect.Width > 50 ? _whiten60 : _whiten45) : _whiten160);
                    cache.trackHighlightBrush.CenterPoint = new PointF(rect.X + (rect.Width - 3) / 2, rect.Height);
                    cache.trackFillBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 1), 
                                                                   (dark ? _whiten5 : _whiten10), 
                                                                   (dark ? _whiten5 : _darken5), 90f);

                    cache.pressedFillBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2),
                                                                    (dark ? Color.Empty : _whiten10),
                                                                    (dark ? _darken38 : _darken16), 90f);
                    cache.trackFillBrush.Blend = _linear50Blend;
                }

                if (!showingInPopup)
                {
                    context.Graphics.FillRectangle(cache.totalBrush, rect.Right - 3, rect.Top, 3, rect.Height);
                    context.Graphics.DrawLine(cache.innerPen, rect.Right - 2, rect.Top, rect.Right - 2, rect.Bottom - 1);
                }

                if (tracking || pressed)
                {

                    if (pressed)
                    {
                        // Lighent the top and darken the bottom of the fill area
                        context.Graphics.FillRectangle(cache.pressedFillBrush, rect.X, rect.Y, rect.Width - 2, rect.Height);
                    }
                    else if (tracking)
                    {
                        // Lighten the top and darken the bottom of the fill area
                        context.Graphics.FillRectangle(cache.trackFillBrush, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

                        // Draw the tracking highlight at bottom of area
                        context.Graphics.FillRectangle(cache.trackHighlightBrush, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                    }

                    if (!showingInPopup && !pressed && !dark)
                    {
                        // Lighten the right inner edge
                        context.Graphics.DrawLine(cache.trackSepPen, rect.Right - 3, rect.Top, rect.Right - 3, rect.Bottom - 1);
                    }

                    if (!showingInPopup && tracking)
                    {
                        // Lighten the bottom inner edge
                        context.Graphics.DrawLine(cache.trackBottomPen, rect.Right - 3, rect.Bottom - 1, rect.Left, rect.Bottom - 1);
                    }
                }
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupNormalTitle(RenderContext context,
                                                                 Rectangle rect,
                                                                 PaletteState state,
                                                                 IPaletteRibbonBack palette,
                                                                 IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonGroupNormalTitle cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupNormalTitle))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupNormalTitle(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupNormalTitle)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    GraphicsPath titlePath = new GraphicsPath();

                    // Create the rounded bottom edge to fit inside a group border
                    titlePath.AddLine(rect.Left, rect.Top, rect.Right - 1, rect.Top);
                    titlePath.AddLine(rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 3);
                    titlePath.AddLine(rect.Right - 1, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 1);
                    titlePath.AddLine(rect.Right - 3, rect.Bottom - 1, rect.Left + 2, rect.Bottom - 1);
                    titlePath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Left, rect.Bottom - 3);
                    titlePath.AddLine(rect.Left, rect.Bottom - 3, rect.Left, rect.Top);

                    RectangleF rectF = new RectangleF(rect.Left - 0.5f, rect.Top - 0.5f, rect.Width + 1, rect.Height + 1);
                    cache.titleBrush = new LinearGradientBrush(rectF, c1, c2, 90f);
                    cache.titlePath = titlePath;
                }

                // Fill path area with a gradient brush
                context.Graphics.FillPath(cache.titleBrush, cache.titlePath);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupCollapsedBorder(RenderContext context,
                                                                     Rectangle rect,
                                                                     PaletteState state,
                                                                     IPaletteRibbonBack palette,
                                                                     IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                // Grab the colors needed for drawing                
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);

                bool generate = true;
                MementoRibbonGroupCollapsedBorder cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupCollapsedBorder))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupCollapsedBorder(rect, c1, c2, c3, c4);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupCollapsedBorder)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    GraphicsPath solidPath = new GraphicsPath();
                    GraphicsPath insidePath = new GraphicsPath();

                    // Create the rounded complete border
                    solidPath.AddLine(rect.Left + 1.25f, rect.Top, rect.Right - 2, rect.Top);
                    solidPath.AddLine(rect.Right - 2, rect.Top, rect.Right - 1, rect.Top + 2);
                    solidPath.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 3);
                    solidPath.AddLine(rect.Right - 1, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 1);
                    solidPath.AddLine(rect.Right - 3, rect.Bottom - 1, rect.Left + 2, rect.Bottom - 1);
                    solidPath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Left, rect.Bottom - 3);
                    solidPath.AddLine(rect.Left, rect.Bottom - 3, rect.Left, rect.Top + 1.25f);
                    solidPath.AddLine(rect.Left, rect.Top + 1.25f, rect.Left + 1.25f, rect.Top);

                    // Create the inside border
                    insidePath.AddLine(rect.Left + 2, rect.Top + 1, rect.Right - 3, rect.Top + 1);
                    insidePath.AddLine(rect.Right - 3, rect.Top + 1, rect.Right - 2, rect.Top + 2);
                    insidePath.AddLine(rect.Right - 2, rect.Top + 2, rect.Right - 2, rect.Bottom - 3);
                    insidePath.AddLine(rect.Right - 2, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 2);
                    insidePath.AddLine(rect.Right - 3, rect.Bottom - 2, rect.Left + 2, rect.Bottom - 2);
                    insidePath.AddLine(rect.Left + 2, rect.Bottom - 2, rect.Left + 1, rect.Bottom - 3);
                    insidePath.AddLine(rect.Left + 1, rect.Bottom - 3, rect.Left + 1, rect.Top + 2);
                    insidePath.AddLine(rect.Left + 1, rect.Top + 2, rect.Left + 2, rect.Top + 1);

                    RectangleF solidRectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
                    RectangleF insideRectF = new RectangleF(rect.Left, rect.Top, rect.Width, rect.Height);

                    LinearGradientBrush solidBrush = new LinearGradientBrush(solidRectF, c1, c2, 90f);
                    LinearGradientBrush insideBrush = new LinearGradientBrush(insideRectF, c3, c4, 90f);

                    cache.solidPath = solidPath;
                    cache.insidePath = insidePath;
                    cache.solidPen = new Pen(solidBrush);
                    cache.insidePen = new Pen(insideBrush);
                }

                // Perform actual drawing using the cache values
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    context.Graphics.DrawPath(cache.solidPen, cache.solidPath);
                    context.Graphics.DrawPath(cache.insidePen, cache.insidePath);
                }
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupCollapsedFrameBorder(RenderContext context,
                                                                          Rectangle rect,
                                                                          PaletteState state,
                                                                          IPaletteRibbonBack palette,
                                                                          IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonGroupCollapsedFrameBorder cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupCollapsedFrameBorder))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupCollapsedFrameBorder(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupCollapsedFrameBorder)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    GraphicsPath solidPath = new GraphicsPath();

                    // Create the rounded complete border
                    solidPath.AddLine(rect.Left + 2, rect.Top, rect.Right - 3, rect.Top);
                    solidPath.AddLine(rect.Right - 3, rect.Top, rect.Right - 1, rect.Top + 2);
                    solidPath.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 3);
                    solidPath.AddLine(rect.Right - 1, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 1);
                    solidPath.AddLine(rect.Right - 3, rect.Bottom - 1, rect.Left + 2, rect.Bottom - 1);
                    solidPath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Left, rect.Bottom - 3);
                    solidPath.AddLine(rect.Left, rect.Bottom - 3, rect.Left, rect.Top + 2);
                    solidPath.AddLine(rect.Left, rect.Top + 2, rect.Left + 2, rect.Top);

                    cache.solidPath = solidPath;
                    cache.titleBrush = new SolidBrush(c2);
                    cache.solidPen = new Pen(c1);
                }

                // Perform actual drawing using the cache values
                Rectangle titleRect = new Rectangle(rect.Left + 1, rect.Bottom - _groupFrameTitleHeight, rect.Width - 2, _groupFrameTitleHeight - 1);
                context.Graphics.FillRectangle(cache.titleBrush, titleRect);

                using (AntiAlias aa = new AntiAlias(context.Graphics))
                    context.Graphics.DrawPath(cache.solidPen, cache.solidPath);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupGradientOne(RenderContext context,
                                                                 Rectangle rect,
                                                                 PaletteState state,
                                                                 IPaletteRibbonBack palette,
                                                                 IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonGroupGradientOne cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupGradientOne))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupGradientOne(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupGradientOne)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    RectangleF rectF = new RectangleF(rect.Left - 1, rect.Top - 1, rect.Width + 2, rect.Height + 2);
                    cache.brush = new LinearGradientBrush(rectF, c1, c2, 90f);
                    cache.brush.Blend = _ribbonGroup8Blend;
                }

                // Perform actual drawing using the cache values
                context.Graphics.FillRectangle(cache.brush, rect);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonGroupGradientTwo(RenderContext context,
                                                                 Rectangle rect,
                                                                 PaletteState state,
                                                                 IPaletteRibbonBack palette,
                                                                 float percent,
                                                                 IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);

                bool generate = true;
                MementoRibbonGroupGradientTwo cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonGroupGradientTwo))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonGroupGradientTwo(rect, c1, c2, c3, c4);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonGroupGradientTwo)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    int topHeight = (int)(rect.Height * percent);
                    Rectangle topRect = new Rectangle(rect.Left, rect.Top, rect.Width, topHeight);
                    Rectangle bottomRect = new Rectangle(rect.Left, topRect.Bottom, rect.Width, rect.Height - topHeight);
                    RectangleF topRectF = new RectangleF(topRect.Left - 1, topRect.Top - 1, topRect.Width + 2, topRect.Height + 2);
                    RectangleF bottomRectF = new RectangleF(bottomRect.Left - 1, bottomRect.Top - 1, bottomRect.Width + 2, bottomRect.Height + 2);

                    cache.topBrush = new LinearGradientBrush(topRectF, c1, c2, 90f);
                    cache.bottomBrush = new LinearGradientBrush(bottomRectF, c3, c4, 90f);
                    cache.topRect = topRect;
                    cache.bottomRect = bottomRect;
                }

                // Perform actual drawing using the cache values
                context.Graphics.FillRectangle(cache.topBrush, cache.topRect);
                context.Graphics.FillRectangle(cache.bottomBrush, cache.bottomRect);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonQATMinibarSingle(RenderContext context,
                                                                 Rectangle rect,
                                                                 PaletteState state,
                                                                 IPaletteRibbonBack palette,
                                                                 bool composition,
                                                                 IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                bool generate = true;
                MementoRibbonQATMinibar cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonQATMinibar))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonQATMinibar(rect, c1, c2, c3, c4, c5);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonQATMinibar)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, c5);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    GraphicsPath borderPath = new GraphicsPath();
                    GraphicsPath topRight1 = new GraphicsPath();
                    GraphicsPath bottomLeft1 = new GraphicsPath();

                    // Find values needed for drawing the main border
                    int left = rect.X + 1;
                    int right = rect.Right - 3;
                    int top = rect.Y + 2;
                    int bottom = rect.Bottom - 3;
                    int middle = top + (bottom - top) / 2;

                    // Construct closed path for the main border
                    borderPath.AddLine(right - 8, bottom, left + 10.75f, bottom);
                    borderPath.AddLine(left + 10.75f, bottom, left + 10, bottom - 8f);
                    borderPath.AddLine(left + 10, bottom - 8f, left + 9, bottom - 11f);
                    borderPath.AddLine(left + 9, bottom - 11f, left + 8, bottom - 13f);
                    borderPath.AddLine(left + 8, bottom - 13f, left + 7, bottom - 15f);
                    borderPath.AddLine(left + 7, bottom - 15f, left + 1, top + 0.25f);
                    borderPath.AddLine(left + 1, top + 0.25f, left - 1, top + 0.25f);
                    borderPath.AddLine(left - 1, top + 0.25f, right - 8, top + 0.25f);
                    borderPath.AddLine(right - 8, top + 0.25f, right - 5, top + 1);
                    borderPath.AddLine(right - 5, top + 1, right - 1, top + 5);
                    borderPath.AddLine(right - 1, top + 5, right, top + 8);
                    borderPath.AddLine(right, top + 8, right + 0.40f, middle);
                    borderPath.AddLine(right + 0.40f, middle, right, bottom - 8.25f);
                    borderPath.AddLine(right, bottom - 8.25f, right - 1, bottom - 5.25f);
                    borderPath.AddLine(right - 1, bottom - 5.25f, right - 5, bottom - 1.25f);
                    borderPath.AddLine(right - 5, bottom - 1.25f, right - 8, bottom);

                    // Create the top right light paths
                    topRight1.AddLine(rect.Left - 1, rect.Top + 1.25f, rect.Right - 11, rect.Top + 1.25f);
                    topRight1.AddLine(rect.Right - 11, rect.Top + 1.5f, rect.Right - 8, rect.Top + 2.25f);
                    topRight1.AddLine(rect.Right - 8, rect.Top + 2.25f, rect.Right - 5, rect.Top + 5.75f);

                    // Create the bottom left light paths
                    bottomLeft1.AddLine(rect.Left + 10.75f, rect.Bottom - 11, rect.Left + 10.75f, rect.Bottom - 5);
                    bottomLeft1.AddLine(rect.Left + 10.75f, rect.Bottom - 5, rect.Left + 13, rect.Bottom - 2);
                    bottomLeft1.AddLine(rect.Left + 13, rect.Bottom - 2, rect.Right - 11, rect.Bottom - 2);
                    bottomLeft1.AddLine(rect.Right - 11, rect.Bottom - 2, rect.Right - 8.5f, rect.Bottom - 3);
                    bottomLeft1.AddLine(rect.Right - 8.5f, rect.Bottom - 3, rect.Right - 4.5f, rect.Bottom - 7);
                    bottomLeft1.AddLine(rect.Right - 4.5f, rect.Bottom - 7, rect.Right - 2.5f, rect.Bottom - 9);
                    bottomLeft1.AddLine(rect.Right - 2.5f, rect.Bottom - 9, rect.Right - 2, rect.Bottom - 11);
                    bottomLeft1.AddLine(rect.Right - 2, rect.Bottom - 11, rect.Right - 2, rect.Bottom - 15);

                    RectangleF gradientRect = rect;
                    gradientRect.Y += 1.5f;
                    gradientRect.Height *= 1.25f;
                    cache.innerBrush = new LinearGradientBrush(gradientRect, c2, c3, 90f);
                    cache.innerBrush.SetSigmaBellShape(0.5f);

                    cache.borderPath = borderPath;
                    cache.topRight1 = topRight1;
                    cache.bottomLeft1 = bottomLeft1;
                    cache.lightPen = new Pen(c4, 2f);
                    cache.borderPen = new Pen(c1);
                    cache.whitenPen = new Pen(c5);
                }

                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    if (!composition)
                    {
                        // Draw the light borders
                        context.Graphics.DrawPath(cache.lightPen, cache.topRight1);
                        context.Graphics.DrawPath(cache.lightPen, cache.bottomLeft1);

                        // Draw the inside background and main border
                        context.Graphics.FillPath(cache.innerBrush, cache.borderPath);
                        context.Graphics.DrawPath(cache.borderPen, cache.borderPath);

                        // Overdraw top for lighter effect
                        context.Graphics.DrawLine(cache.whitenPen, rect.Left + 10, rect.Top + 2, rect.Right - 10, rect.Top + 2);
                        context.Graphics.DrawLine(cache.whitenPen, rect.Left + 12, rect.Top + 3, rect.Right - 8, rect.Top + 3);
                        context.Graphics.DrawLine(cache.whitenPen, rect.Left + 14, rect.Top + 4, rect.Right - 7, rect.Top + 4);
                    }
                    else
                    {
                        // Fill with a semi-transparent background/border
                        context.Graphics.FillPath(_compositionBrush, cache.borderPath);
                        context.Graphics.DrawPath(_compositionPen, cache.borderPath);
                    }
                }
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonQATMinibarDouble(RenderContext context,
                                                                 Rectangle rect,
                                                                 PaletteState state,
                                                                 IPaletteRibbonBack palette,
                                                                 bool composition,
                                                                 IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);
                Color c4 = palette.GetRibbonBackColor4(state);
                Color c5 = palette.GetRibbonBackColor5(state);

                bool generate = true;
                MementoRibbonQATMinibar cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonQATMinibar))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonQATMinibar(rect, c1, c2, c3, c4, c5);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonQATMinibar)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3, c4, c5);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    GraphicsPath borderPath = new GraphicsPath();
                    GraphicsPath topRight1 = new GraphicsPath();
                    GraphicsPath bottomLeft1 = new GraphicsPath();

                    // Find values needed for drawing the main border
                    int left = rect.X + 1;
                    int right = rect.Right - 3;
                    int top = rect.Y + 2;
                    int bottom = rect.Bottom - 3;
                    int middle = top + (bottom - top) / 2;

                    // Construct closed path for the main border
                    borderPath.AddLine(right - 8, top + 0.25f, right - 5, top + 1);
                    borderPath.AddLine(right - 5, top + 1, right - 1, top + 5);
                    borderPath.AddLine(right - 1, top + 5, right, top + 8);
                    borderPath.AddLine(right, top + 8, right + 0.40f, middle);
                    borderPath.AddLine(right + 0.40f, middle, right, bottom - 8.25f);
                    borderPath.AddLine(right, bottom - 8.25f, right - 1, bottom - 5.25f);
                    borderPath.AddLine(right - 1, bottom - 5.25f, right - 5, bottom - 1.25f);
                    borderPath.AddLine(right - 5, bottom - 1.25f, right - 8, bottom);
                    borderPath.AddLine(right - 8, bottom, left + 9, bottom);
                    borderPath.AddLine(left + 9, bottom, left + 6, bottom - 1.25f);
                    borderPath.AddLine(left + 6, bottom - 1.25f, left + 2, bottom - 5.25f);
                    borderPath.AddLine(left + 2, bottom - 5.25f, left + 1, bottom - 8.25f);
                    borderPath.AddLine(left + 1, bottom - 8.25f, left + 0.40f, middle);
                    borderPath.AddLine(left + 0.40f, middle, left + 1, top + 8);
                    borderPath.AddLine(left + 1, top + 8, left + 2, top + 5);
                    borderPath.AddLine(left + 2, top + 5, left + 6, top + 1);
                    borderPath.AddLine(left + 6, top + 1, left + 9, top + 0.25f);
                    borderPath.AddLine(left + 9, top + 0.25f, right - 8, top + 0.25f);
                    
                    // Create the top right light paths
                    topRight1.AddLine(rect.Left + 8, rect.Top + 3.25f, rect.Left + 10, rect.Top + 1.25f);
                    topRight1.AddLine(rect.Left + 10, rect.Top + 1.25f, rect.Right - 11, rect.Top + 1.25f);
                    topRight1.AddLine(rect.Right - 11, rect.Top + 1.5f, rect.Right - 8, rect.Top + 2.25f);
                    topRight1.AddLine(rect.Right - 8, rect.Top + 2.25f, rect.Right - 5, rect.Top + 5.75f);

                    // Create the bottom left light paths
                    bottomLeft1.AddLine(rect.Left + 13, rect.Bottom - 2, rect.Right - 11, rect.Bottom - 2);
                    bottomLeft1.AddLine(rect.Right - 11, rect.Bottom - 2, rect.Right - 8.5f, rect.Bottom - 3);
                    bottomLeft1.AddLine(rect.Right - 8.5f, rect.Bottom - 3, rect.Right - 4.5f, rect.Bottom - 7);
                    bottomLeft1.AddLine(rect.Right - 4.5f, rect.Bottom - 7, rect.Right - 2.5f, rect.Bottom - 9);
                    bottomLeft1.AddLine(rect.Right - 2.5f, rect.Bottom - 9, rect.Right - 2, rect.Bottom - 11);
                    bottomLeft1.AddLine(rect.Right - 2, rect.Bottom - 11, rect.Right - 2, rect.Bottom - 15);

                    RectangleF gradientRect = rect;
                    gradientRect.Y += 1.5f;
                    gradientRect.Height *= 1.25f;
                    cache.innerBrush = new LinearGradientBrush(gradientRect, c2, c3, 90f);
                    cache.innerBrush.SetSigmaBellShape(0.5f);

                    cache.borderPath = borderPath;
                    cache.topRight1 = topRight1;
                    cache.bottomLeft1 = bottomLeft1;
                    cache.lightPen = new Pen(c4, 2f);
                    cache.borderPen = new Pen(c1);
                    cache.whitenPen = new Pen(c5);
                }

                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    if (!composition)
                    {
                        // Draw the light borders
                        context.Graphics.DrawPath(cache.lightPen, cache.topRight1);
                        context.Graphics.DrawPath(cache.lightPen, cache.bottomLeft1);

                        // Draw the inside background and main border
                        context.Graphics.FillPath(cache.innerBrush, cache.borderPath);
                        context.Graphics.DrawPath(cache.borderPen, cache.borderPath);
                    }
                    else
                    {
                        // Fill with a semi-transparent background/border
                        context.Graphics.FillPath(_compositionBrush, cache.borderPath);
                        context.Graphics.DrawPath(_compositionPen, cache.borderPath);
                    }
                }
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonLinear(RenderContext context,
                                                       Rectangle rect,
                                                       PaletteState state,
                                                       IPaletteRibbonBack palette,
                                                       IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonLinear cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonLinear))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonLinear(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonLinear)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.linearBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2), c1, c2, 90f);
                }

                context.Graphics.FillRectangle(cache.linearBrush, rect);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonLinearBorder(RenderContext context,
                                                             Rectangle rect,
                                                             PaletteState state,
                                                             IPaletteRibbonBack palette,
                                                             IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonLinearBorder cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonLinearBorder))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonLinearBorder(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonLinearBorder)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.linearBrush = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2), c1, c2, 90f);
                    cache.linearPen = new Pen(cache.linearBrush);

                    // Create the rounded complete border
                    GraphicsPath borderPath = new GraphicsPath();
                    borderPath.AddLine(rect.Left + 2, rect.Top, rect.Right - 3, rect.Top);
                    borderPath.AddLine(rect.Right - 3, rect.Top, rect.Right - 1, rect.Top + 2);
                    borderPath.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 3);
                    borderPath.AddLine(rect.Right - 1, rect.Bottom - 3, rect.Right - 3, rect.Bottom - 1);
                    borderPath.AddLine(rect.Right - 3, rect.Bottom - 1, rect.Left + 2, rect.Bottom - 1);
                    borderPath.AddLine(rect.Left + 2, rect.Bottom - 1, rect.Left, rect.Bottom - 3);
                    borderPath.AddLine(rect.Left, rect.Bottom - 3, rect.Left, rect.Top + 2);
                    borderPath.AddLine(rect.Left, rect.Top + 2, rect.Left + 2, rect.Top);
                    cache.borderPath = borderPath;

                }

                context.Graphics.DrawPath(cache.linearPen, cache.borderPath);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonAppMenuInner(RenderContext context,
                                                             Rectangle rect,
                                                             PaletteState state,
                                                             IPaletteRibbonBack palette,
                                                             IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonAppButtonInner cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonAppButtonInner))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonAppButtonInner(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonAppButtonInner)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.outsideBrush = new SolidBrush(c1);
                    cache.insideBrush = new SolidBrush(c2);
                }

                // Draw the outside in one color and the inside with another
                context.Graphics.FillRectangle(cache.outsideBrush, rect);
                rect.Inflate(-1, -1);
                context.Graphics.FillRectangle(cache.insideBrush, rect);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonAppMenuOuter(RenderContext context,
                                                             Rectangle rect,
                                                             PaletteState state,
                                                             IPaletteRibbonBack palette,
                                                             IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);

                bool generate = true;
                MementoRibbonAppButtonOuter cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonAppButtonOuter))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonAppButtonOuter(rect, c1, c2, c3);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonAppButtonOuter)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.wholeBrush = new SolidBrush(c1);
                    cache.backPath = new GraphicsPath();
                    cache.backPath.AddLine(rect.X + 1, rect.Y, rect.Right - 1, rect.Y);
                    cache.backPath.AddLine(rect.Right - 1, rect.Y, rect.Right, rect.Y + 1);
                    cache.backPath.AddLine(rect.Right, rect.Y + 1, rect.Right, rect.Bottom - 2);
                    cache.backPath.AddLine(rect.Right, rect.Bottom - 2, rect.Right - 2, rect.Bottom);
                    cache.backPath.AddLine(rect.Right - 2, rect.Bottom, rect.X + 1, rect.Bottom);
                    cache.backPath.AddLine(rect.X + 2, rect.Bottom, rect.X, rect.Bottom - 2);
                    cache.backPath.AddLine(rect.X, rect.Bottom - 2, rect.X, rect.Y + 1);
                    cache.backPath.AddLine(rect.X, rect.Y + 1, rect.X + 1, rect.Y);
                    cache.bottomDarkGradient = new LinearGradientBrush(new Point(rect.X, rect.Bottom - 15), new Point(rect.X, rect.Bottom), c2, c3);
                    cache.topLightenGradient = new LinearGradientBrush(new Point(rect.X, rect.Y - 1), new Point(rect.X, rect.Y + 7), _whiten64, _whiten32);
                }

                // Draw entire background in solid brush
                context.Graphics.FillPath(cache.wholeBrush, cache.backPath);
                context.Graphics.FillRectangle(cache.bottomDarkGradient, new Rectangle(rect.X, rect.Bottom - 14, rect.Width, 13));
                context.Graphics.FillRectangle(cache.topLightenGradient, new Rectangle(rect.X, rect.Y, rect.Width, 6));
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonQATFullbarRound(RenderContext context,
                                                                Rectangle rect,
                                                                PaletteState state,
                                                                IPaletteRibbonBack palette,
                                                                IDisposable memento)
        {
            // We never draw the top line
            rect.Y++;
            rect.Height--;

            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);

                bool generate = true;
                MementoRibbonQATFullbarRound cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonQATFullbarRound))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonQATFullbarRound(rect, c1, c2, c3);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonQATFullbarRound)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.innerRect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
                    cache.innerBrush = new LinearGradientBrush(rect, c1, c2, 90f);
                    cache.darkPen = new Pen(c3);

                    GraphicsPath darkPath = new GraphicsPath();
                    GraphicsPath lightPath1 = new GraphicsPath();
                    GraphicsPath lightPath2 = new GraphicsPath();

                    // Create the dark border
                    darkPath.AddLine(rect.Left, rect.Top + 0.75f, rect.Left + 1, rect.Top);
                    darkPath.AddLine(rect.Left + 1, rect.Top, rect.Right - 3.5f, rect.Top);
                    darkPath.AddLine(rect.Right - 3.5f, rect.Top, rect.Right - 2, rect.Top + 2);
                    darkPath.AddLine(rect.Right - 2, rect.Top + 2, rect.Right - 2, rect.Bottom - 3.25f);
                    darkPath.AddLine(rect.Right - 2, rect.Bottom - 3.25f, rect.Right - 3.25f, rect.Bottom - 2);
                    darkPath.AddLine(rect.Right - 3.25f, rect.Bottom - 2, rect.Left, rect.Bottom - 2);

                    // Create the first light border
                    lightPath1.AddLine(rect.Left, rect.Bottom - 3, rect.Left, rect.Top + 2.5f);
                    lightPath1.AddLine(rect.Left, rect.Top + 2.5f, rect.Left + 1, rect.Top + 1);
                    lightPath1.AddLine(rect.Left + 1, rect.Top + 1, rect.Right - 4, rect.Top + 1);

                    // Create the second light border
                    lightPath2.AddLine(rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 2);
                    lightPath2.AddLine(rect.Right - 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 1);
                    lightPath2.AddLine(rect.Right - 2, rect.Bottom - 1, rect.Left + 1, rect.Bottom - 1);

                    cache.darkPath = darkPath;
                    cache.lightPath1 = lightPath1;
                    cache.lightPath2 = lightPath2;
                }

                // Draw a gradient for the inside of the area
                context.Graphics.FillRectangle(cache.innerBrush, cache.innerRect);

                // Draw the dark/light lines
                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    context.Graphics.DrawPath(cache.darkPen, cache.darkPath);
                    context.Graphics.DrawPath(_light1Pen, cache.lightPath1);
                    context.Graphics.DrawPath(_light2Pen, cache.lightPath2);
                }
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonQATFullbarSquare(RenderContext context,
                                                                 Rectangle rect,
                                                                 PaletteState state,
                                                                 IPaletteRibbonBack palette,
                                                                 IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);
                Color c3 = palette.GetRibbonBackColor3(state);

                bool generate = true;
                MementoRibbonQATFullbarSquare cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonQATFullbarSquare))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonQATFullbarSquare(rect, c1, c2, c3);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonQATFullbarSquare)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2, c3);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.lightPen = new Pen(c1);
                    cache.mediumBrush = new SolidBrush(c2);
                    cache.darkPen = new Pen(c3);
                }

                // Fill entire area in background brush
                context.Graphics.FillRectangle(cache.mediumBrush, rect);

                // Draw the outside border
                context.Graphics.DrawRectangle(cache.darkPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

                // Draw ligher top and bottom lines
                context.Graphics.DrawLine(cache.lightPen, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Y + 1);
                context.Graphics.DrawLine(cache.lightPen, rect.X + 1, rect.Bottom - 2, rect.Width - 2, rect.Bottom - 2);
            }

            return memento;
        }

        /// <summary>
        /// Internal rendering method.
        /// </summary>
        protected virtual IDisposable DrawRibbonQATOverflow(PaletteRibbonShape shape,
                                                            RenderContext context,
                                                            Rectangle rect,
                                                            PaletteState state,
                                                            IPaletteRibbonBack palette,
                                                            IDisposable memento)
        {
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                Color c1 = palette.GetRibbonBackColor1(state);
                Color c2 = palette.GetRibbonBackColor2(state);

                bool generate = true;
                MementoRibbonQATOverflow cache;

                // Access a cache instance and decide if cache resources need generating
                if ((memento == null) || !(memento is MementoRibbonQATOverflow))
                {
                    if (memento != null)
                        memento.Dispose();

                    cache = new MementoRibbonQATOverflow(rect, c1, c2);
                    memento = cache;
                }
                else
                {
                    cache = (MementoRibbonQATOverflow)memento;
                    generate = !cache.UseCachedValues(rect, c1, c2);
                }

                // Do we need to generate the contents of the cache?
                if (generate)
                {
                    // Dispose of existing values
                    cache.Dispose();

                    cache.backBrush = new SolidBrush(c1);
                    cache.borderPen = new Pen(c2);
                }

                // Draw a gradient for the inside of the area
                context.Graphics.FillRectangle(cache.backBrush, rect);

                using (AntiAlias aa = new AntiAlias(context.Graphics))
                {
                    if (shape == PaletteRibbonShape.Office2010)
                    {
                        context.Graphics.DrawPolygon(cache.borderPen, new Point[]{ new Point(rect.Left + 1, rect.Top),
                                                                                   new Point(rect.Right - 2, rect.Top),
                                                                                   new Point(rect.Right - 1, rect.Top + 1),
                                                                                   new Point(rect.Right - 1, rect.Bottom - 2),
                                                                                   new Point(rect.Right - 2, rect.Bottom - 1),
                                                                                   new Point(rect.Left + 1, rect.Bottom - 1),
                                                                                   new Point(rect.Left, rect.Bottom - 2),
                                                                                   new Point(rect.Left, rect.Top + 1) } );
                    }
                    else
                    {
                        context.Graphics.DrawLine(cache.borderPen, rect.Left + 1f, rect.Top, rect.Right - 2f, rect.Top);
                        context.Graphics.DrawLine(cache.borderPen, rect.Right - 2f, rect.Top, rect.Right - 1f, rect.Top + 2f);
                        context.Graphics.DrawLine(cache.borderPen, rect.Right - 1f, rect.Top + 2f, rect.Right - 1f, rect.Bottom - 2f);
                        context.Graphics.DrawLine(cache.borderPen, rect.Right - 1f, rect.Bottom - 2f, rect.Right - 2f, rect.Bottom - 1f);
                        context.Graphics.DrawLine(cache.borderPen, rect.Right - 2f, rect.Bottom - 1f, rect.Left + 1f, rect.Bottom - 1f);
                        context.Graphics.DrawLine(cache.borderPen, rect.Left + 1f, rect.Bottom - 1f, rect.Left, rect.Bottom - 2f);
                        context.Graphics.DrawLine(cache.borderPen, rect.Left, rect.Bottom - 2f, rect.Left, rect.Top + 1f);
                        context.Graphics.DrawLine(cache.borderPen, rect.Left, rect.Top + 1f, rect.Left + 1f, rect.Top);
                    }
                }
            }

            return memento;
        }
        #endregion

        #region StandardContentMemento
        /// <summary>
        /// Internal help class used to store content rendering details.
        /// </summary>
        private class StandardContentMemento : IDisposable
		{
			// Instance fields
			public bool DrawImage;
			public bool DrawShortText;
			public bool DrawLongText;
            public Image Image;
            public Color ImageTransparentColor;
			public Rectangle ImageRect;
            public PaletteTextTrim ShortTextTrimming;
            public AccurateTextMemento ShortTextMemento;
			public Rectangle ShortTextRect;
			public TextRenderingHint ShortTextHint;
            public PaletteTextTrim LongTextTrimming;
            public AccurateTextMemento LongTextMemento;
            public Rectangle LongTextRect;
            public TextRenderingHint LongTextHint;
            public VisualOrientation Orientation;

			/// <summary>
			/// Inititialize a new instance of the StandardContentMemento class.
			/// </summary>
			public StandardContentMemento()
			{
                LongTextTrimming = PaletteTextTrim.EllipsisCharacter;
                ShortTextTrimming = PaletteTextTrim.EllipsisCharacter;
                Orientation = VisualOrientation.Top;
			}

            /// <summary>
            /// Dispose of resources.
            /// </summary>
            public void Dispose()
            {
                if (ShortTextMemento != null)
                {
                    ShortTextMemento.Dispose();
                    ShortTextMemento = null;
                }

                if (LongTextMemento != null)
                {
                    LongTextMemento.Dispose();
                    LongTextMemento = null;
                }
            }

			/// <summary>
			/// Adjust the memento values to apply an orientation.
			/// </summary>
			/// <param name="orientation">Visual orientation of contents.</param>
			/// <param name="displayRect">Rectangle that contains the contents.</param>
			public void AdjustForOrientation(VisualOrientation orientation,
											 Rectangle displayRect)
			{
				switch (orientation)
				{ 
					case VisualOrientation.Top:
						// Do nothing, the contents are in top orientation to start with
						break;
					case VisualOrientation.Bottom:
                        Orientation = VisualOrientation.Bottom;
						
						// Reposition the image relative the display rectangle
						if (DrawImage)
						{
							ImageRect.X = displayRect.Right - ImageRect.Width - (ImageRect.X - displayRect.Left);
							ImageRect.Y = displayRect.Bottom - ImageRect.Height - (ImageRect.Y - displayRect.Top);
						}

						// Reposition the short text relative the display rectangle
						if (DrawShortText)
						{
							ShortTextRect.X = displayRect.Right - ShortTextRect.Width - (ShortTextRect.X - displayRect.Left);
							ShortTextRect.Y = displayRect.Bottom - ShortTextRect.Height - (ShortTextRect.Y - displayRect.Top);
						}

						// Reposition the long text relative the display rectangle
						if (DrawLongText)
						{
							LongTextRect.X = displayRect.Right - LongTextRect.Width - (LongTextRect.X - displayRect.Left);
							LongTextRect.Y = displayRect.Bottom - LongTextRect.Height - (LongTextRect.Y - displayRect.Top);
						}
						break;
					case VisualOrientation.Left:
                        Orientation = VisualOrientation.Left;

						// Reposition the image relative the display rectangle
						if (DrawImage)
						{
							int x = ImageRect.Y - displayRect.Top;
							ImageRect.Y = displayRect.Top + displayRect.Width - ImageRect.Width - (ImageRect.X - displayRect.X);
							ImageRect.X = x + displayRect.Left;
						}

						// Reposition the short text relative the display rectangle
						if (DrawShortText)
						{
							int x = ShortTextRect.Y - displayRect.Top;
							ShortTextRect.Y = displayRect.Top + displayRect.Width - ShortTextRect.Width - (ShortTextRect.X - displayRect.X);
							ShortTextRect.X = x + displayRect.Left;
							SwapRectangleSizes(ref ShortTextRect);
						}

						// Reposition the long text relative the display rectangle
						if (DrawLongText)
						{
							int x = LongTextRect.Y - displayRect.Top;
							LongTextRect.Y = displayRect.Top + displayRect.Width - LongTextRect.Width - (LongTextRect.X - displayRect.X);
							LongTextRect.X = x + displayRect.Left;
							SwapRectangleSizes(ref LongTextRect);
						}
						break;
					case VisualOrientation.Right:
                        Orientation = VisualOrientation.Right;

						// Reposition the image relative the display rectangle
						if (DrawImage)
						{
							int y = ImageRect.X - displayRect.Left;
							ImageRect.X = displayRect.Left + displayRect.Bottom - ImageRect.Bottom;
							ImageRect.Y = y + displayRect.Top;
						}

						// Reposition the short text relative the display rectangle
						if (DrawShortText)
						{
							int y = ShortTextRect.X - displayRect.Left;
							ShortTextRect.X = displayRect.Left + displayRect.Bottom - ShortTextRect.Bottom;
							ShortTextRect.Y = y + displayRect.Top;
							SwapRectangleSizes(ref ShortTextRect);
						}

						// Reposition the long text relative the display rectangle
						if (DrawLongText)
						{
							int y = LongTextRect.X - displayRect.Left;
							LongTextRect.X = displayRect.Left + displayRect.Bottom - LongTextRect.Bottom;
							LongTextRect.Y = y + displayRect.Top;
							SwapRectangleSizes(ref LongTextRect);
						}
						break;
				}
			}

			private static void SwapRectangleSizes(ref Rectangle rect)
			{
				int temp = rect.Width;
				rect.Width = rect.Height;
				rect.Height = temp;
			}
        }
		#endregion
	}
}
