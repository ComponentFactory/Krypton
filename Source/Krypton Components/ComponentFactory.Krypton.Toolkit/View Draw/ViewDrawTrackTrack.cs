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
    /// Draw the track for the track bar.
    /// </summary>
    public class ViewDrawTrackTrack : ViewLeaf
    {
        #region Instance Fields
        private ViewDrawTrackBar _drawTrackBar;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawTrackTrack class.
		/// </summary>
        /// <param name="drawTrackBar">Reference to owning track bar.</param>
        public ViewDrawTrackTrack(ViewDrawTrackBar drawTrackBar)
		{
            _drawTrackBar = drawTrackBar;
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawTrackTrack:" + Id;
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
            return _drawTrackBar.TrackSize;
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
                elementColors = _drawTrackBar.StateNormal.Track;
            else
                elementColors = _drawTrackBar.StateDisabled.Track;

            context.Renderer.RenderGlyph.DrawTrackGlyph(context, State, elementColors, ClientRectangle, _drawTrackBar.Orientation, _drawTrackBar.VolumeControl);
        }
        #endregion
    }
}
