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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Encapsulates context for view render operations.
	/// </summary>
	public class RenderContext : ViewContext
    {
        #region Instance Fields
        private Rectangle _clipRect;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewContext class.
        /// </summary>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="graphics">Graphics instance for drawing.</param>
        /// <param name="clipRect">Rectangle that needs rendering.</param>
        /// <param name="renderer">Rendering provider.</param>
        public RenderContext(Control control,
                             Graphics graphics,
                             Rectangle clipRect,
                             IRenderer renderer)
            : this(null, control, control, graphics, clipRect, renderer)
        {
        }

        /// <summary>
		/// Initialize a new instance of the ViewContext class.
		/// </summary>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="alignControl">Control used to align elements.</param>
        /// <param name="graphics">Graphics instance for drawing.</param>
        /// <param name="clipRect">Rectangle that needs rendering.</param>
		/// <param name="renderer">Rendering provider.</param>
        public RenderContext(Control control,
                             Control alignControl,
                             Graphics graphics,
                             Rectangle clipRect,
                             IRenderer renderer)
            : this(null, control, alignControl, graphics, clipRect, renderer)
        {
        }

        /// <summary>
		/// Initialize a new instance of the ViewContext class.
		/// </summary>
        /// <param name="manager">Reference to the view manager.</param>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="alignControl">Control used to align elements.</param>
        /// <param name="graphics">Graphics instance for drawing.</param>
        /// <param name="clipRect">Rectangle that needs rendering.</param>
		/// <param name="renderer">Rendering provider.</param>
        public RenderContext(ViewManager manager,
                             Control control, 
                             Control alignControl,
							 Graphics graphics,
                             Rectangle clipRect,
							 IRenderer renderer)
            : base(manager, control, alignControl, graphics, renderer)
		{
            _clipRect = clipRect;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets the rectangle that needs rendering.
        /// </summary>
        public Rectangle ClipRect
        {
            get { return _clipRect; }
        }

		/// <summary>
		/// Calculate a rectangle in control coodinates that is aligned for gradient drawing.
		/// </summary>
		/// <param name="align">How to align the gradient.</param>
		/// <param name="local">Rectangle of the local element.</param>
		/// <returns></returns>
		public Rectangle GetAlignedRectangle(PaletteRectangleAlign align, Rectangle local)
		{
			switch (align)
			{
				case PaletteRectangleAlign.Local:
					// Gradient should cover just the local view element itself
					local.Inflate(2, 2);
                    return local;
				case PaletteRectangleAlign.Control:
                    Rectangle clientRect = Rectangle.Empty;
                    if (AlignControl == Control)
                         clientRect = Control.ClientRectangle;
                    else
                        clientRect = Control.RectangleToClient(AlignControl.RectangleToScreen(AlignControl.ClientRectangle));
                    clientRect.Inflate(2, 2);
                    return clientRect;
				case PaletteRectangleAlign.Form:
					// Gradient should cover the owning control (most likely a Form)
                    Rectangle formRect = Control.RectangleToClient(TopControl.RectangleToScreen(AlignControl.ClientRectangle));
                    formRect.Inflate(2, 2);
                    return formRect;
				case PaletteRectangleAlign.Inherit:
				default:
					// Should never call this routine with inherit value
					Debug.Assert(false);
					throw new ArgumentOutOfRangeException("align");
			}
		}
		#endregion
	}
}
