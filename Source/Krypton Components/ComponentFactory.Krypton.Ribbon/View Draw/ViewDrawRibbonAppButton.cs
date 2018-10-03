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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws half of an application button.
	/// </summary>
    internal class ViewDrawRibbonAppButton : ViewLeaf
    {
        #region Static Fields
        private static readonly Size SIZE_FULL = new Size(39, 39);
        private static readonly Size SIZE_TOP = new Size(39, 22);
        private static readonly Size SIZE_BOTTOM = new Size(39, 17);
        #endregion

        #region Instance Fields
        private IDisposable[] _mementos;
        private KryptonRibbon _ribbon;
        private bool _bottomHalf;
        private Rectangle _clipRect;
        private Size _size;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonAppButton class.
		/// </summary>
        /// <param name="ribbon">Owning control instance.</param>
        /// <param name="bottomHalf">Scroller orientation.</param>
        public ViewDrawRibbonAppButton(KryptonRibbon ribbon,
                                       bool bottomHalf)
        {
            Debug.Assert(ribbon != null);

            _ribbon = ribbon;
            _bottomHalf = bottomHalf;
            _size = (_bottomHalf ? SIZE_BOTTOM : SIZE_TOP);
            _mementos = new IDisposable[3];
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonAppButton:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_mementos != null)
                {
                    foreach (IDisposable memento in _mementos)
                        if (memento != null)
                            memento.Dispose();

                    _mementos = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Visible
        /// <summary>
        /// Gets and sets the visible state of the element.
        /// </summary>
        public override bool Visible
        {
            get { return (base.Visible && ((Parent == null) ? true : Parent.Visible)); }
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
            return _size;
        }

		/// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;
            _clipRect = ClientRectangle;

            // Update to reflect full size of actual button
            if (_bottomHalf)
            {
                Rectangle client = ClientRectangle;
                client.Y = client.Y - (SIZE_FULL.Height - SIZE_BOTTOM.Height);
                ClientRectangle = client;
            }

            ClientHeight = SIZE_FULL.Height;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            // New clipping region is at most our own client size
            using (Region combineRegion = new Region(_clipRect))
            {
                // Remember the current clipping region
                Region clipRegion = context.Graphics.Clip.Clone();

                // Reduce clipping region down by the existing clipping region
                combineRegion.Intersect(clipRegion);

                // Use new region that restricts drawing to our client size only
                context.Graphics.Clip = combineRegion;

                IPaletteRibbonBack palette;
                int memento;

                // Find the correct palette to use that matches the button state
                switch (State)
                {
                    default:
                    case PaletteState.Normal:
                        palette = _ribbon.StateNormal.RibbonAppButton;
                        memento = 0;
                        break;
                    case PaletteState.Tracking:
                        palette = _ribbon.StateTracking.RibbonAppButton;
                        memento = 1;
                        break;
                    case PaletteState.Pressed:
                        palette = _ribbon.StatePressed.RibbonAppButton;
                        memento = 2;
                        break;
                }

                // Draw the background
                _mementos[memento] = context.Renderer.RenderRibbon.DrawRibbonApplicationButton(_ribbon.RibbonShape, context, ClientRectangle, State, palette, _mementos[memento]);

                // If there is an application button to be drawn
                if (_ribbon.RibbonAppButton.AppButtonImage != null)
                {
                    // We always draw the image a 24x24 image
                    Rectangle imageRect = new Rectangle(ClientLocation.X + 7, ClientLocation.Y + 6, 24, 24);

                    if (_ribbon.Enabled)
                        context.Graphics.DrawImage(_ribbon.RibbonAppButton.AppButtonImage, imageRect);
                    else
                    {
                        // Use a color matrix to convert to black and white
                        using (ImageAttributes attribs = new ImageAttributes())
                        {
                            attribs.SetColorMatrix(CommonHelper.MatrixDisabled);

                            context.Graphics.DrawImage(_ribbon.RibbonAppButton.AppButtonImage,
                                                       imageRect, 0, 0,
                                                       _ribbon.RibbonAppButton.AppButtonImage.Width,
                                                       _ribbon.RibbonAppButton.AppButtonImage.Height,
                                                       GraphicsUnit.Pixel, attribs);
                        }
                    }
                }

                // Put clipping region back to original setting
                context.Graphics.Clip = clipRegion;
            }
        }
        #endregion
    }
}
