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
    /// Draws a drop down button using the provided renderer.
    /// </summary>
    public class ViewDrawDropDownButton : ViewLeaf
    {
        #region Instance Fields
        private IPalette _palette;
        private VisualOrientation _orientation;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawDropDownButton class.
		/// </summary>
        public ViewDrawDropDownButton()
		{
            _orientation = VisualOrientation.Top;
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawDropDownButton:" + Id;
		}
		#endregion

        #region Palette
        /// <summary>
        /// Gets and sets the palette to use.
        /// </summary>
        public IPalette Palette
        {
            get { return _palette; }
            set { _palette = value; }
        }
        #endregion

        #region Orientation
        /// <summary>
        /// Gets and sets the orientation of the drop down button.
        /// </summary>
        public VisualOrientation Orientation
        {
            get { return _orientation; }
            set { _orientation = value; }
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

            // Ask the renderer for the required size of the drop down button
            return context.Renderer.RenderGlyph.GetDropDownButtonPreferredSize(context, _palette, State, Orientation);
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
            context.Renderer.RenderGlyph.DrawDropDownButton(context, 
                                                            ClientRectangle, 
                                                            _palette, 
                                                            State,
                                                            Orientation);
        }
        #endregion
    }
}
