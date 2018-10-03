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
    /// Draw the tick marks for the track bar.
    /// </summary>
    public class ViewDrawTrackTicks : ViewLeaf
    {
        #region Instance Fields
        private ViewDrawTrackBar _drawTrackBar;
        private bool _topRight;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawTrackTicks class.
		/// </summary>
        /// <param name="drawTrackBar">Reference to owning track bar.</param>
        /// <param name="topRight">Showing ticks to the top/right or bottom/left.</param>
        public ViewDrawTrackTicks(ViewDrawTrackBar drawTrackBar, bool topRight)
		{
            _drawTrackBar = drawTrackBar;
            _topRight = topRight;
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawTrackTicks:" + Id;
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
            return _drawTrackBar.TickSize;
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
            IPaletteElementColor elementColors;
            if (Enabled)
                elementColors =_drawTrackBar.StateNormal.Tick;
            else
                elementColors = _drawTrackBar.StateDisabled.Tick;

            context.Renderer.RenderGlyph.DrawTrackTicksGlyph(context, State, elementColors, ClientRectangle, 
                                                             _drawTrackBar.Orientation, _topRight, 
                                                             _drawTrackBar.PositionSize,
                                                             _drawTrackBar.Minimum,
                                                             _drawTrackBar.Maximum,
                                                             _drawTrackBar.TickFrequency);
        }
        #endregion
    }
}
