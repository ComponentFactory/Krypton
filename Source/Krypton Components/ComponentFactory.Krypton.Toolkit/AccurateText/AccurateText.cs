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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Provide accurate text measuring and drawing capability.
	/// </summary>
    public class AccurateText : GlobalId
    {
        #region Static Fields
        private static readonly int GLOW_EXTRA_WIDTH = 14;
        private static readonly int GLOW_EXTRA_HEIGHT = 3;
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Pixel accurate measure of the specified string when drawn with the specified Font object.
        /// </summary>
        /// <param name="g">Graphics instance used to measure text.</param>
        /// <param name="rtl">Right to left setting for control.</param>
        /// <param name="text">String to measure.</param>
        /// <param name="font">Font object that defines the text format of the string.</param>
        /// <param name="trim">How to trim excess text.</param>
        /// <param name="align">How to align multine text.</param>
        /// <param name="prefix">How to process prefix characters.</param>
        /// <param name="hint">Rendering hint.</param>
        /// <param name="composition">Should draw on a composition element.</param>
        /// <param name="disposeFont">Dispose of font when finished with it.</param>
        /// <returns>A memento used to draw the text.</returns>
        public static AccurateTextMemento MeasureString(Graphics g,
                                                        RightToLeft rtl,
                                                        string text,
                                                        Font font,
                                                        PaletteTextTrim trim,
                                                        PaletteRelativeAlign align,
                                                        PaletteTextHotkeyPrefix prefix,
                                                        TextRenderingHint hint,
                                                        bool composition,
                                                        bool disposeFont)
        {
            Debug.Assert(g != null);
            Debug.Assert(text != null);
            Debug.Assert(font != null);

            if (g == null) throw new ArgumentNullException("g");
            if (text == null) throw new ArgumentNullException("text");
            if (font == null) throw new ArgumentNullException("font");

            // An empty string cannot be drawn, so uses the empty memento
            if (text.Length == 0)
                return AccurateTextMemento.Empty;

            // Create the format object used when measuring and drawing
            StringFormat format = new StringFormat();
            format.FormatFlags = StringFormatFlags.NoClip;

            // Ensure that text reflects reversed RTL setting
            if (rtl == RightToLeft.Yes)
                format.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // How do we position text horizontally?
            switch (align)
            {
                case PaletteRelativeAlign.Near:
                    format.Alignment = (rtl == RightToLeft.Yes) ? StringAlignment.Far : StringAlignment.Near;
                    break;
                case PaletteRelativeAlign.Center:
                    format.Alignment = StringAlignment.Center;
                    break;
                case PaletteRelativeAlign.Far:
                    format.Alignment = (rtl == RightToLeft.Yes) ? StringAlignment.Near : StringAlignment.Far;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            // Do we need to trim text that is too big?
            switch (trim)
            {
                case PaletteTextTrim.Character:
                    format.Trimming = StringTrimming.Character;
                    break;
                case PaletteTextTrim.EllipsisCharacter:
                    format.Trimming = StringTrimming.EllipsisCharacter;
                    break;
                case PaletteTextTrim.EllipsisPath:
                    format.Trimming = StringTrimming.EllipsisPath;
                    break;
                case PaletteTextTrim.EllipsisWord:
                    format.Trimming = StringTrimming.EllipsisWord;
                    break;
                case PaletteTextTrim.Word:
                    format.Trimming = StringTrimming.Word;
                    break;
                case PaletteTextTrim.Hide:
                    format.Trimming = StringTrimming.None;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            // Setup the correct prefix processing
            switch (prefix)
            {
                case PaletteTextHotkeyPrefix.None:
                    format.HotkeyPrefix = HotkeyPrefix.None;
                    break;
                case PaletteTextHotkeyPrefix.Hide:
                    format.HotkeyPrefix = HotkeyPrefix.Hide;
                    break;
                case PaletteTextHotkeyPrefix.Show:
                    format.HotkeyPrefix = HotkeyPrefix.Show;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            // Replace tab characters with a fixed four spaces
            text = text.Replace("\t", "    ");

            // Perform actual measure of the text
            using (GraphicsTextHint graphicsHint = new GraphicsTextHint(g, hint))
            {
                SizeF textSize = Size.Empty;

                try
                {
                    textSize = g.MeasureString(text, font, int.MaxValue, format);

                    if (composition)
                        textSize.Width += GLOW_EXTRA_WIDTH;
                }
                catch {}

                // Return a memento with drawing details
                return new AccurateTextMemento(text, font, textSize, format, hint, disposeFont);
            }
        }

        /// <summary>
        /// Pixel accurate drawing of the requested text memento information.
        /// </summary>
        /// <param name="g">Graphics object used for drawing.</param>
        /// <param name="brush">Brush for drawing text with.</param>
        /// <param name="rect">Rectangle to draw text inside.</param>
        /// <param name="rtl">Right to left setting for control.</param>
        /// <param name="orientation">Orientation for drawing text.</param>
        /// <param name="memento">Memento containing text context.</param>
        /// <param name="state">State of the source element.</param>
        /// <param name="composition">Should draw on a composition element.</param>
        /// <returns>True if draw succeeded; False is draw produced an error.</returns>
        public static bool DrawString(Graphics g,
                                      Brush brush,
                                      Rectangle rect,
                                      RightToLeft rtl,
                                      VisualOrientation orientation,
                                      bool composition,
                                      PaletteState state,
                                      AccurateTextMemento memento)
        {
            Debug.Assert(g != null);
            Debug.Assert(memento != null);

            // Cannot draw with a null graphics instance
            if (g == null)
                throw new ArgumentNullException("g");

            // Cannot draw with a null memento instance
            if (memento == null)
                throw new ArgumentNullException("memento");

            bool ret = true;

            // Is there a valid place to be drawn into
            if ((rect.Width > 0) && (rect.Height > 0))
            {
                // Does the memento contain something to draw?
                if (!memento.IsEmpty)
                {
                    int translateX = 0;
                    int translateY = 0;
                    float rotation = 0f;

                    // Perform any transformations needed for orientation
                    switch (orientation)
                    {
                        case VisualOrientation.Bottom:
                            // Translate to opposite side of origin, so the rotate can 
                            // then bring it back to original position but mirror image
                            translateX = rect.X * 2 + rect.Width;
                            translateY = rect.Y * 2 + rect.Height;
                            rotation = 180f;
                            break;
                        case VisualOrientation.Left:
                            // Invert the dimensions of the rectangle for drawing upwards
                            rect = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);

                            // Translate back from a quater left turn to the original place 
                            translateX = rect.X - rect.Y - 1;
                            translateY = rect.X + rect.Y + rect.Width;
                            rotation = 270;
                            break;
                        case VisualOrientation.Right:
                            // Invert the dimensions of the rectangle for drawing upwards
                            rect = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);

                            // Translate back from a quater right turn to the original place 
                            translateX = rect.X + rect.Y + rect.Height + 1;
                            translateY = -(rect.X - rect.Y);
                            rotation = 90f;
                            break;
                    }

                    // Apply the transforms if we have any to apply
                    if ((translateX != 0) || (translateY != 0))
                        g.TranslateTransform(translateX, translateY);

                    if (rotation != 0f)
                        g.RotateTransform(rotation);

                    try
                    {
                        if (composition)
                            DrawCompositionGlowingText(g, memento.Text, memento.Font, rect, state,
                                                       SystemColors.ActiveCaptionText, true);
                        else
                            g.DrawString(memento.Text, memento.Font, brush, rect, memento.Format);
                    }
                    catch
                    {
                        // Ignore any error from the DrawString, usually because the display settings
                        // have changed causing Fonts to be invalid. Our controls will notice the change
                        // and refresh the fonts but sometimes the draw happens before the fonts are
                        // regenerated. Just ignore message and everything will sort itself out. Trust me!
                        ret = false;
                    }
                    finally
                    {
                        // Remove the applied transforms
                        if (rotation != 0f)
                            g.RotateTransform(-rotation);

                        if ((translateX != 0) || (translateY != 0))
                            g.TranslateTransform(-translateX, -translateY);
                    }
                }
            }

            return ret;
        }
		#endregion

        #region Implementation
        /// <summary>
        /// Draw text with a glowing background, for use on a composition element.
        /// </summary>
        /// <param name="g">Graphics reference.</param>
        /// <param name="text">Text to be drawn.</param>
        /// <param name="font">Font to use for text.</param>
        /// <param name="bounds">Bounding area for the text.</param>
        /// <param name="state">State of the source element.</param>
        /// <param name="color">Color of the text.</param>
        /// <param name="copyBackground">Should existing background be copied into the bitmap.</param>
        public static void DrawCompositionGlowingText(Graphics g, 
                                                      string text, 
                                                      Font font, 
                                                      Rectangle bounds, 
                                                      PaletteState state,
                                                      Color color,
                                                      bool copyBackground)
		{
            try
            {
                // Get the hDC for the graphics instance and create a memory DC
                IntPtr gDC = g.GetHdc();
                IntPtr mDC = PI.CreateCompatibleDC(gDC);

                PI.BITMAPINFO bmi = new PI.BITMAPINFO();
                bmi.biSize = Marshal.SizeOf(bmi);
                bmi.biWidth = bounds.Width;
                bmi.biHeight = -(bounds.Height + GLOW_EXTRA_HEIGHT * 2);
                bmi.biCompression = 0;
                bmi.biBitCount = 32;
                bmi.biPlanes = 1;

                // Create a device independant bitmp and select into the memory DC
                IntPtr hDIB = PI.CreateDIBSection(gDC, bmi, 0, 0, IntPtr.Zero, 0);
                PI.SelectObject(mDC, hDIB);

                if (copyBackground)
                {
                    // Copy existing background into the bitmap
                    PI.BitBlt(mDC, 0, 0, bounds.Width, bounds.Height + GLOW_EXTRA_HEIGHT * 2,
                              gDC, bounds.X, bounds.Y - GLOW_EXTRA_HEIGHT, 0x00CC0020);
                }

                // Select the font for use when drawing
                IntPtr hFont = font.ToHfont();
                PI.SelectObject(mDC, hFont);

                // Get renderer for the correct state
                VisualStyleRenderer renderer = new VisualStyleRenderer(state == PaletteState.Normal ? VisualStyleElement.Window.Caption.Active :
                                                                                                      VisualStyleElement.Window.Caption.Inactive);

                // Create structures needed for theme drawing call
                PI.RECT textBounds = new PI.RECT();
                textBounds.left = 0;
                textBounds.top = 0;
                textBounds.right = (bounds.Right - bounds.Left);
                textBounds.bottom = (bounds.Bottom - bounds.Top) + (GLOW_EXTRA_HEIGHT * 2);
                PI.DTTOPTS dttOpts = new PI.DTTOPTS();
                dttOpts.dwSize = Marshal.SizeOf(typeof(PI.DTTOPTS));
                dttOpts.dwFlags = PI.DTT_COMPOSITED | PI.DTT_GLOWSIZE | PI.DTT_TEXTCOLOR;
                dttOpts.crText = ColorTranslator.ToWin32(color);
                dttOpts.iGlowSize = 11;

                // Always draw text centered
                TextFormatFlags textFormat = TextFormatFlags.SingleLine |
                                             TextFormatFlags.HorizontalCenter |
                                             TextFormatFlags.VerticalCenter |
                                             TextFormatFlags.EndEllipsis;

                // Perform actual drawing
                PI.DrawThemeTextEx(renderer.Handle,
                                   mDC, 0, 0,
                                   text, -1, (int)textFormat,
                                   ref textBounds, ref dttOpts);

                // Copy to foreground
                PI.BitBlt(gDC,
                          bounds.Left, bounds.Top - GLOW_EXTRA_HEIGHT,
                          bounds.Width, bounds.Height + (GLOW_EXTRA_HEIGHT * 2),
                          mDC, 0, 0, 0x00CC0020);

                // Dispose of allocated objects
                PI.DeleteObject(hFont);
                PI.DeleteObject(hDIB);
                PI.DeleteDC(mDC);

                // Must remember to release the hDC
                g.ReleaseHdc(gDC);
            }
            catch 
            {
            }
        }
        #endregion
    }
}
