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
using System.Reflection;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonProfessionalRenderer : ToolStripProfessionalRenderer
    {
        #region Instance Fields
        private KryptonColorTable _kct;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonProfessionalRenderer class.
        /// </summary>
        /// <param name="kct">Source for text colors.</param>
        public KryptonProfessionalRenderer(KryptonColorTable kct)
            : base(kct)
        {
            Debug.Assert(kct != null);
            _kct = kct;
        }
        #endregion

        #region KCT
        /// <summary>
        /// Gets access to the KryptonColorTable instance.
        /// </summary>
        public KryptonColorTable KCT
        {
            get { return _kct; }
        }
        #endregion

        #region OnRenderItemImage
        /// <summary>
        /// Raises the RenderItemImage event. 
        /// </summary>
        /// <param name="e">An ToolStripItemImageRenderEventArgs containing the event data.</param>
        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            // Is this a min/restore/close pendant button
            if (e.Item.GetType().ToString() == "System.Windows.Forms.MdiControlStrip+ControlBoxMenuItem")
            {
                // Get access to the owning form of the mdi control strip
                Form f = e.ToolStrip.Parent.TopLevelControl as Form;
                if (f != null)
                {
                    // Get the mdi control strip instance
                    PropertyInfo piMCS = typeof(Form).GetProperty("MdiControlStrip", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
                    if (piMCS != null)
                    {
                        object mcs = piMCS.GetValue(f, null);
                        if (mcs != null)
                        {
                            // Get the min/restore/close internal menu items
                            Type mcsType = mcs.GetType();
                            FieldInfo fiM = mcsType.GetField("minimize", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
                            FieldInfo fiR = mcsType.GetField("restore", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
                            FieldInfo fiC = mcsType.GetField("close", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
                            if ((fiM != null) && (fiR != null) && (fiC != null))
                            {
                                ToolStripMenuItem m = fiM.GetValue(mcs) as ToolStripMenuItem;
                                ToolStripMenuItem r = fiR.GetValue(mcs) as ToolStripMenuItem;
                                ToolStripMenuItem c = fiC.GetValue(mcs) as ToolStripMenuItem;
                                if ((m != null) && (r != null) && (c != null))
                                {
                                    // Compare the event provided image with the internal cached ones to discover the type of pendant button we are drawing
                                    PaletteButtonSpecStyle specStyle = PaletteButtonSpecStyle.Generic;
                                    if (m.Image == e.Image)
                                        specStyle = PaletteButtonSpecStyle.PendantMin;
                                    else if (r.Image == e.Image)
                                        specStyle = PaletteButtonSpecStyle.PendantRestore;
                                    else if (c.Image == e.Image)
                                        specStyle = PaletteButtonSpecStyle.PendantClose;

                                    // A match, means we have a known pendant button
                                    if (specStyle != PaletteButtonSpecStyle.Generic)
                                    {
                                        // Grab the palette pendant details needed for drawing
                                        Image paletteImage = KCT.Palette.GetButtonSpecImage(specStyle, PaletteState.Normal);
                                        Color transparentColor = KCT.Palette.GetButtonSpecImageTransparentColor(specStyle);

                                        // Finally we actually have an image to draw!
                                        if (paletteImage != null)
                                        {
                                            using (ImageAttributes attribs = new ImageAttributes())
                                            {
                                                // Setup mapping to make required color transparent
                                                ColorMap remap = new ColorMap();
                                                remap.OldColor = transparentColor;
                                                remap.NewColor = Color.Transparent;
                                                attribs.SetRemapTable(new ColorMap[] { remap });

                                                // Phew, actually draw the darn thing
                                                e.Graphics.DrawImage(paletteImage, e.ImageRectangle,
                                                                     0, 0, e.Image.Width, e.Image.Height,
                                                                     GraphicsUnit.Pixel, attribs);

                                                // Do not let base class draw system defined image
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            base.OnRenderItemImage(e);
        }
        #endregion

        #region OnRenderToolStripBorder
        /// <summary>
        /// Raises the RenderToolStripBorder event. 
        /// </summary>
        /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // D0 not draw the annoying status strip single line that is not needed
            if (!(e.ToolStrip is StatusStrip))
                base.OnRenderToolStripBorder(e);
        }
        #endregion
    }
}
