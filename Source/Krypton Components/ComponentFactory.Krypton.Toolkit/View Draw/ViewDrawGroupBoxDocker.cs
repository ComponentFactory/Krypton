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

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Extends the ViewDrawDocker for use in the KryptonGroupBox.
	/// </summary>
    public class ViewDrawGroupBoxDocker : ViewDrawDocker
    {
        #region Instance Fields
        private double _overlap;
        private Rectangle _cacheClientRect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawGroupBoxDocker class.
        /// </summary>
        /// <param name="paletteBack">Palette source for the background.</param>		
        /// <param name="paletteBorder">Palette source for the border.</param>
        public ViewDrawGroupBoxDocker(IPaletteBack paletteBack,
                                      IPaletteBorder paletteBorder)
            : base(paletteBack, paletteBorder)
        {
            _overlap = 0.5;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawGroupBoxDocker:" + Id;
		}
		#endregion

        #region CaptionOverlap
        /// <summary>
        /// Gets and the sets the percentage of overlap for the caption and group area.
        /// </summary>
        public double CaptionOverlap
        {
            get { return _overlap; }
            set { _overlap = value; }
        }
        #endregion

        #region DrawBorderAfter
        /// <summary>
        /// Gets the drawing of the border before or after children.
        /// </summary>
        public override bool DrawBorderLast
        {
            get { return false; }
        }
        #endregion

        #region Eval
        /// <summary>
        /// Evaluate the need for drawing transparent areas.
        /// </summary>
        /// <param name="context">Evaluation context.</param>
        /// <returns>True if transparent areas exist; otherwise false.</returns>
        public override bool EvalTransparentPaint(ViewContext context)
        {
            return true;
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            if (this[0].Visible)
            {
                // The first and only child is the caption content
                ViewDrawContent caption = (ViewDrawContent)this[0];

                // Cache the origina client rectangle before we modify it
                _cacheClientRect = ClientRectangle;

                // Update canvas drawing area by the overlapping caption area
                Rectangle captionRect = caption.ClientRectangle;
                switch (GetDock(caption))
                {
                    case ViewDockStyle.Top:
                        if (captionRect.Height > 0)
                        {
                            int reduce = (int)(captionRect.Height * CaptionOverlap);
                            ClientRectangle = new Rectangle(_cacheClientRect.X, _cacheClientRect.Y + reduce, _cacheClientRect.Width, _cacheClientRect.Height - reduce);
                        }
                        break;
                    case ViewDockStyle.Left:
                        if (captionRect.Width > 0)
                        {
                            int reduce = (int)(captionRect.Width * CaptionOverlap);
                            ClientRectangle = new Rectangle(_cacheClientRect.X + reduce, _cacheClientRect.Y, _cacheClientRect.Width - reduce, _cacheClientRect.Height);
                        }
                        break;
                    case ViewDockStyle.Bottom:
                        if (captionRect.Height > 0)
                        {
                            int reduce = (int)(captionRect.Height * CaptionOverlap);
                            ClientRectangle = new Rectangle(_cacheClientRect.X, _cacheClientRect.Y, _cacheClientRect.Width, _cacheClientRect.Height - reduce);
                        }
                        break;
                    case ViewDockStyle.Right:
                        if (captionRect.Width > 0)
                        {
                            int reduce = (int)(captionRect.Width * CaptionOverlap);
                            ClientRectangle = new Rectangle(_cacheClientRect.X, _cacheClientRect.Y, _cacheClientRect.Width - reduce, _cacheClientRect.Height);
                        }
                        break;
                }
            }

            base.RenderBefore(context);
        }

        /// <summary>
        /// Perform rendering after child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderAfter(RenderContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            base.RenderAfter(context);

            // Restore original client rectangle
            if (this[0].Visible)
                ClientRectangle = _cacheClientRect;
        }

        /// <summary>
        /// Draw the canvas border.
        /// </summary>
        /// <param name="context"></param>
        public override void RenderBorder(RenderContext context)
        {
            // The first and only child is the caption content
            ViewDrawContent caption = (ViewDrawContent)this[0];

            // Remember current clipping region so we can put it back later
            Region clipRegion = context.Graphics.Clip.Clone();

            // Exclude the image/short/long text rectangles from being drawn
            Region excludeRegion = clipRegion.Clone();
            Rectangle imageRect = caption.ImageRectangle(context);
            Rectangle shortRect = caption.ShortTextRect(context);
            Rectangle longRect = caption.LongTextRect(context);
            imageRect.Inflate(1, 1);
            shortRect.Inflate(1, 1);
            longRect.Inflate(1, 1);
            excludeRegion.Exclude(imageRect);
            excludeRegion.Exclude(shortRect);
            excludeRegion.Exclude(longRect);

            // Draw border and restore original clipping region
            context.Graphics.Clip = excludeRegion;
            base.RenderBorder(context);
            context.Graphics.Clip = clipRegion;
        }
        #endregion
    }
}
