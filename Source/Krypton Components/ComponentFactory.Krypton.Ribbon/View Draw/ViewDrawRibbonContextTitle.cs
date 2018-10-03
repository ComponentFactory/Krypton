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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws a context title.
	/// </summary>
    internal class ViewDrawRibbonContextTitle : ViewLeaf,
                                                IPaletteRibbonBack,
                                                IContentValues
    {
        #region Static Fields
        private static readonly int TEXT_SIDE_GAP = 4;
        private static readonly int TEXT_SIDE_GAP_COMPOSITION = 2;
        private static readonly int TEXT_BOTTOM_GAP = 3;
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private ContextTabSet _context;
        private IPaletteRibbonBack _inherit;
        private ContextToContent _contentProvider;
        private IDisposable _mementoBack;
        private IDisposable _mementoContentText;
        private IDisposable _mementoContentShadow1;
        private IDisposable _mementoContentShadow2;
        private Rectangle _textRect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonContextTitle class.
        /// </summary>
        /// <param name="ribbon">Source ribbon control.</param>
        /// <param name="inherit">Source for inheriting the ribbon bacgkground colors.</param>
        public ViewDrawRibbonContextTitle(KryptonRibbon ribbon,
                                          IPaletteRibbonBack inherit)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(inherit != null);

            // Remember incoming references
            _inherit = inherit;
            _ribbon = ribbon;

            // Use a class to convert from ribbon tab to content interface
            _contentProvider = new ContextToContent(ribbon.StateCommon.RibbonGeneral);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonContextTitle:" + Id;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_mementoBack != null)
                {
                    _mementoBack.Dispose();
                    _mementoBack = null;
                }

                if (_mementoContentText != null)
                {
                    _mementoContentText.Dispose();
                    _mementoContentText = null;
                }

                if (_mementoContentShadow1 != null)
                {
                    _mementoContentShadow1.Dispose();
                    _mementoContentShadow1 = null;
                }

                if (_mementoContentShadow2 != null)
                {
                    _mementoContentShadow2.Dispose();
                    _mementoContentShadow2 = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region ContextTabSet
        /// <summary>
        /// Gets and sets the context to display.
        /// </summary>
        public ContextTabSet ContextTabSet
        {
            get { return _context; }
            
            set 
            { 
                _context = value;

                // Update the component we are associated with
                if (_context != null)
                    Component = _context.Context;
                else
                    Component = null;
            }
        }
        #endregion

        #region Visible
        /// <summary>
        /// Gets and sets the visible state of the element.
        /// </summary>
        public override bool Visible
        {
            get { return (_ribbon.Visible && base.Visible); }
            set { base.Visible = value; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // We have no preference, just set our size to whatever is needed
            return Size.Empty;
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            ClientRectangle = context.DisplayRectangle;

            // We always extend an extra pixel downwards to draw over the containers border
            Rectangle adjustRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientWidth, ClientHeight + 1);

            // Get the client rect of the parent
            Rectangle parentRect = Parent.ClientRectangle;

            // If we are only partially visible on the right hand side
            if ((adjustRect.X < parentRect.Right) && (adjustRect.Right >= parentRect.Right))
            {
                // Truncate on the right hand side to the parent
                adjustRect.Width = parentRect.Right - adjustRect.X;
            }

            // If we are only partially visible on the left hand side
            if ((adjustRect.Right > parentRect.X) && (adjustRect.X < parentRect.X))
            {
                // Truncate on the left hand side to the parent
                adjustRect.Width = adjustRect.Right - parentRect.X;
                adjustRect.X = parentRect.X;
            }

            // Use adjusted rectangle as our client rectangle
            ClientRectangle = adjustRect;

            // Use the font height to decide on the text rectangle
            int fontHeight = _ribbon.CalculatedValues.DrawFontHeight;
            _textRect = new Rectangle(ClientLocation.X + TEXT_SIDE_GAP, 
                                      ClientLocation.Y + (ClientHeight - fontHeight - TEXT_BOTTOM_GAP), 
                                      ClientWidth - (TEXT_SIDE_GAP * 2),
                                      fontHeight);

            // Remember to dispose of old memento
            if (_mementoContentText != null)
            {
                _mementoContentText.Dispose();
                _mementoContentText = null;
            }

            if (_mementoContentShadow1 != null)
            {
                _mementoContentShadow1.Dispose();
                _mementoContentShadow1 = null;
            }

            if (_mementoContentShadow2 != null)
            {
                _mementoContentShadow2.Dispose();
                _mementoContentShadow2 = null;
            }

            // Office 2010 draws a shadow effect of the text
            if (_ribbon.RibbonShape == PaletteRibbonShape.Office2010)
            {
                Rectangle shadowTextRect1 = new Rectangle(_textRect.X - 1, _textRect.Y + 1, _textRect.Width, _textRect.Height);
                Rectangle shadowTextRect2 = new Rectangle(_textRect.X + 1, _textRect.Y + 1, _textRect.Width, _textRect.Height);

                _contentProvider.OverrideTextColor = Color.FromArgb(128, ControlPaint.Dark(GetRibbonBackColor1(PaletteState.Normal)));

                if (DrawOnComposition)
                    _contentProvider.OverrideTextHint = PaletteTextHint.SingleBitPerPixelGridFit;

                _mementoContentShadow1 = context.Renderer.RenderStandardContent.LayoutContent(context, shadowTextRect1,
                                                                                             _contentProvider, this,
                                                                                             VisualOrientation.Top, 
                                                                                             PaletteState.Normal, false);

                _mementoContentShadow2 = context.Renderer.RenderStandardContent.LayoutContent(context, shadowTextRect2,
                                                                                             _contentProvider, this,
                                                                                             VisualOrientation.Top,
                                                                                             PaletteState.Normal, false);
                _contentProvider.OverrideTextColor = Color.Empty;
            }

            // Use the renderer to layout the text
            _mementoContentText = context.Renderer.RenderStandardContent.LayoutContent(context, _textRect, 
                                                                                       _contentProvider, this, 
                                                                                       VisualOrientation.Top, 
                                                                                       PaletteState.Normal, false);

            _contentProvider.OverrideTextHint = PaletteTextHint.Inherit;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            // Office 2010 draws a shadow effect of the text
            if ((_ribbon.RibbonShape == PaletteRibbonShape.Office2010) && (_mementoContentShadow1 != null))
            {
                PaletteState state = (_ribbon.Enabled ? PaletteState.Normal : PaletteState.Disabled);

                // Use renderer to draw the tab background
                _mementoBack = context.Renderer.RenderRibbon.DrawRibbonTabContextTitle(_ribbon.RibbonShape, context, ClientRectangle, _ribbon.StateCommon.RibbonGeneral, this, _mementoBack);

                Rectangle shadowTextRect1 = new Rectangle(_textRect.X - 1, _textRect.Y + 1, _textRect.Width, _textRect.Height);
                Rectangle shadowTextRect2 = new Rectangle(_textRect.X + 1, _textRect.Y + 1, _textRect.Width, _textRect.Height);

                _contentProvider.OverrideTextColor = Color.FromArgb(128, ControlPaint.Dark(GetRibbonBackColor1(PaletteState.Normal)));

                if (DrawOnComposition)
                    _contentProvider.OverrideTextHint = PaletteTextHint.SingleBitPerPixelGridFit;

                context.Renderer.RenderStandardContent.DrawContent(context, shadowTextRect1,
                                                                   _contentProvider, _mementoContentShadow1,
                                                                   VisualOrientation.Top,
                                                                   state, false, true);

                context.Renderer.RenderStandardContent.DrawContent(context, shadowTextRect1,
                                                                   _contentProvider, _mementoContentShadow2,
                                                                   VisualOrientation.Top,
                                                                   state, false, true);

                _contentProvider.OverrideTextColor = Color.Empty;

                // Use renderer to draw the text content
                if (_mementoContentText != null)
                    context.Renderer.RenderStandardContent.DrawContent(context, _textRect,
                                                                       _contentProvider, _mementoContentText,
                                                                       VisualOrientation.Top,
                                                                       state, false, true);

                _contentProvider.OverrideTextHint = PaletteTextHint.Inherit;
            }
            else
            {
                if (DrawOnComposition)
                    RenderOnComposition(context);
                else
                {
                    PaletteState state = (_ribbon.Enabled ? PaletteState.Normal : PaletteState.Disabled);

                    // Use renderer to draw the tab background
                    _mementoBack = context.Renderer.RenderRibbon.DrawRibbonTabContextTitle(_ribbon.RibbonShape, context, ClientRectangle, _ribbon.StateCommon.RibbonGeneral, this, _mementoBack);

                    // Use renderer to draw the text content
                    if (_mementoContentText != null)
                        context.Renderer.RenderStandardContent.DrawContent(context, _textRect,
                                                                           _contentProvider, _mementoContentText,
                                                                           VisualOrientation.Top,
                                                                           state, DrawOnComposition, true);
                }
            }
        }
        #endregion

        #region IPaletteRibbonBack
        /// <summary>
        /// Gets the background drawing style for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state)
        {
            return PaletteRibbonColorStyle.RibbonGroupAreaBorder;
        }

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor1(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor1(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor2(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor2(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor3(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor3(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor4(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor4(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public Color GetRibbonBackColor5(PaletteState state)
        {
            Color retColor = _inherit.GetRibbonBackColor5(state);

            // If empty then try and recover the context specific color
            if (retColor == Color.Empty)
                retColor = CheckForContextColor(state);

            return retColor;
        }
        #endregion

        #region Implementation
        private void RenderOnComposition(RenderContext context)
        {
            // Convert the clipping rectangle from floating to int version
            RectangleF rectClipF = context.Graphics.ClipBounds;
            Rectangle rectClip = new Rectangle((int)rectClipF.X, (int)rectClipF.Y,
                                               (int)rectClipF.Width, (int)rectClipF.Height);

            // No point drawing unless some of the client fits into the clipping area
            if (rectClip.IntersectsWith(ClientRectangle))
            {
                // Get the hDC for the graphics instance and create a memory DC
			    IntPtr gDC = context.Graphics.GetHdc();
			    IntPtr mDC = PI.CreateCompatibleDC(gDC);

                PI.BITMAPINFO bmi = new PI.BITMAPINFO();
                bmi.biSize = Marshal.SizeOf(bmi);
                bmi.biWidth = ClientWidth;
                bmi.biHeight = -ClientHeight;
                bmi.biCompression = 0;
                bmi.biBitCount = 32;
                bmi.biPlanes = 1;

                // Create a device independant bitmp and select into the memory DC
                IntPtr hDIB = PI.CreateDIBSection(gDC, bmi, 0, 0, IntPtr.Zero, 0);
                PI.SelectObject(mDC, hDIB);

                // To call the renderer we need to convert from Win32 HDC to Graphics object
                using (Graphics bitmapG = Graphics.FromHdc(mDC))
                {
                    Rectangle renderClientRect = new Rectangle(0, 0, ClientWidth, ClientHeight);

                    // Create new render context that uses the bitmap graphics instance
                    using (RenderContext bitmapContext = new RenderContext(context.Control, 
                                                                           bitmapG, 
                                                                           renderClientRect, 
                                                                           context.Renderer))
                    {
                        // Finally we get the renderer to draw the background for the bitmap
                        _mementoBack = context.Renderer.RenderRibbon.DrawRibbonTabContextTitle(_ribbon.RibbonShape, bitmapContext, renderClientRect, _ribbon.StateCommon.RibbonGeneral, this, _mementoBack);
                    }
                }

			    // Select the font for use when drawing
                IntPtr hFont = _contentProvider.GetContentShortTextFont(State).ToHfont();
                PI.SelectObject(mDC, hFont);

                // Get renderer for the correct state
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);

                // Create structures needed for theme drawing call
                PI.RECT textBounds = new PI.RECT();
                textBounds.left = TEXT_SIDE_GAP_COMPOSITION;
                textBounds.top = 0;
                textBounds.right = ClientWidth - (TEXT_SIDE_GAP_COMPOSITION * 2);
                textBounds.bottom = ClientHeight;
                PI.DTTOPTS dttOpts = new PI.DTTOPTS();
                dttOpts.dwSize = Marshal.SizeOf(typeof(PI.DTTOPTS));
			    dttOpts.dwFlags = PI.DTT_COMPOSITED | PI.DTT_GLOWSIZE | PI.DTT_TEXTCOLOR;
                dttOpts.crText = ColorTranslator.ToWin32(SystemColors.ActiveCaptionText);
			    dttOpts.iGlowSize = (_ribbon.Enabled ? 12 : 2);

			    // Always draw text centered
                TextFormatFlags textFormat = TextFormatFlags.SingleLine | 
                                             TextFormatFlags.HorizontalCenter | 
                                             TextFormatFlags.VerticalCenter |
                                             TextFormatFlags.EndEllipsis;

                // Perform actual drawing
                PI.DrawThemeTextEx(renderer.Handle,
                                   mDC, 0, 0,
                                   GetShortText(), -1, (int)textFormat,
                                   ref textBounds, ref dttOpts);

			    // Copy to foreground
                PI.BitBlt(gDC,
                          ClientLocation.X, ClientLocation.Y,
                          ClientWidth, ClientHeight,
                          mDC, 0, 0, 0x00CC0020);

			    // Dispose of allocated objects
                PI.DeleteObject(hFont);
                PI.DeleteObject(hDIB);
                PI.DeleteDC(mDC);

                // Must remember to release the hDC
                context.Graphics.ReleaseHdc(gDC);
            }
        }

        private bool DrawOnComposition
        {
            get
            {
                if (_ribbon != null)
                    return _ribbon.CaptionArea.DrawCaptionOnComposition;
                else
                    return false;
            }
        }

        private Color CheckForContextColor(PaletteState state)
        {
            // We need an associated context
            if (_context != null)
                return _context.ContextColor;
            else
                return Color.Empty;
        }
        #endregion    
    
        #region IContentValues
        /// <summary>
        /// Gets the image used for the ribbon tab.
        /// </summary>
        /// <param name="state">Tab state.</param>
        /// <returns>Image.</returns>
        public Image GetImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the image color that should be interpreted as transparent.
        /// </summary>
        /// <param name="state">Tab state.</param>
        /// <returns>Transparent Color.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the short text used as the main ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetShortText()
        {
            if ((_context != null) && (_context.ContextTitle != null))
                return _context.ContextTitle;
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets the long text used as the secondary ribbon title.
        /// </summary>
        /// <returns>Title string.</returns>
        public string GetLongText()
        {
            return string.Empty;
        }
        #endregion
    }
}
