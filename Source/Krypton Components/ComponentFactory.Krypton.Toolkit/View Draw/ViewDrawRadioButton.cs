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
    /// Draws a radio button using the provided renderer.
    /// </summary>
    public class ViewDrawRadioButton : ViewLeaf
    {
        #region Instance Fields
        private IPalette _palette;
        private bool _checkState;
        private bool _tracking;
        private bool _pressed;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRadioButton class.
		/// </summary>
        /// <param name="palette">Palette for source of drawing values.</param>
        public ViewDrawRadioButton(IPalette palette)
		{
            Debug.Assert(palette != null);
            _palette = palette;
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRadioButton:" + Id;
		}
		#endregion

        #region CheckState
        /// <summary>
        /// Gets and sets the check state of the check box.
        /// </summary>
        public bool CheckState
        {
            get { return _checkState; }
            set { _checkState = value; }
        }
        #endregion

        #region Tracking
        /// <summary>
        /// Gets and sets the tracking state of the check box.
        /// </summary>
        public bool Tracking
        {
            get { return _tracking; }
            set { _tracking = value; }
        }
        #endregion

        #region Pressed
        /// <summary>
        /// Gets and sets the pressed state of the check box.
        /// </summary>
        public bool Pressed
        {
            get { return _pressed; }
            set { _pressed = value; }
        }
        #endregion

        #region Layout
        /// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Ask the renderer for the required size of the check box
            return context.Renderer.RenderGlyph.GetRadioButtonPreferredSize(context, _palette, 
                                                                            Enabled, _checkState, 
                                                                            _tracking, _pressed);
        }

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
        }
        #endregion

		#region Paint
		/// <summary>
		/// Perform rendering before child elements are rendered.
		/// </summary>
		/// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context)
        {
            context.Renderer.RenderGlyph.DrawRadioButton(context, ClientRectangle, 
                                                         _palette, Enabled, 
                                                         _checkState, _tracking, 
                                                         _pressed);
        }
        #endregion
    }
}
