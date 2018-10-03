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
	/// View element that can draw a panel (background but no border)
	/// </summary>
	public class ViewDrawPanel : ViewComposite
	{
		#region Instance Fields
		internal IPaletteBack _paletteBack;
        private IDisposable _memento;
        private VisualOrientation _orientation;
        private bool _ignoreRender;
		#endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawPanel class.
        /// </summary>
        public ViewDrawPanel()
        {
            _orientation = VisualOrientation.Top;
            _ignoreRender = false;
        }
        
        /// <summary>
        /// Initialize a new instance of the ViewDrawPanel class.
		/// </summary>
		/// <param name="paletteBack">Palette source for the background.</param>		
		public ViewDrawPanel(IPaletteBack paletteBack)
		{
			Debug.Assert(paletteBack != null);
			_paletteBack = paletteBack;
            _orientation = VisualOrientation.Top;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
			return "ViewDrawPanel:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_memento != null)
                {
                    _memento.Dispose();
                    _memento = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion
        
        #region IgnoreRender
        /// <summary>
        /// Gets and sets the rendering status.
        /// </summary>
        public bool IgnoreRender
        {
            get { return _ignoreRender; }
            set { _ignoreRender = value; }
        }
        #endregion

        #region Orientation
        /// <summary>
        /// Gets and sets the orientation of the panel.
        /// </summary>
        public VisualOrientation VisualOrientation
        {
            get { return _orientation; }
            set { _orientation = value; }
        }
        #endregion

        #region SetPalettes
        /// <summary>
		/// Update the source palettes for drawing.
		/// </summary>
		/// <param name="paletteBack">Palette source for the background.</param>		
		public void SetPalettes(IPaletteBack paletteBack)
		{
			Debug.Assert(paletteBack != null);

			// Use newly provided palettes
			_paletteBack = paletteBack;
		}

        /// <summary>
        /// Gets the palette used for drawing the panel.
        /// </summary>
        /// <returns></returns>
        public IPaletteBack GetPalette()
        {
            return _paletteBack;
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
            Debug.Assert(context != null);

            // Ask the renderer to evaluate the given palette
            return context.Renderer.EvalTransparentPaint(_paletteBack, State);
        }
        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Let child elements layout
            base.Layout(context);
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

            if (!IgnoreRender)
            {
                // Do we need to draw the background?
                if (_paletteBack.GetBackDraw(State) == InheritBool.True)
                {
                    // Render the background
                    using (GraphicsPath panelPath = new GraphicsPath())
                    {
                        // The path encloses the entire panel area
                        panelPath.AddRectangle(ClientRectangle);

                        // Perform actual panel drawing
                        _memento = context.Renderer.RenderStandardBack.DrawBack(context, ClientRectangle, panelPath, _paletteBack, VisualOrientation, State, _memento);
                    }
                }
            }
		}
		#endregion
	}
}
