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
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Draws a short vertical cluster separator.
	/// </summary>
    internal class ViewDrawRibbonGroupClusterSeparator : ViewLeaf
    {
        #region Static Fields
        private static readonly Size _preferredSize = new Size(1, 4);
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private bool _start;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonGroupClusterSeparator class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="start">Is this is cluster start separator.</param>
        public ViewDrawRibbonGroupClusterSeparator(KryptonRibbon ribbon, bool start)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;
            _start = start;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawRibbonGroupClusterSeparator:" + Id;
		}
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            return _preferredSize;
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
        }
        #endregion

        #region Paint
        /// <summary>
        /// Perform rendering before child elements are rendered.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        public override void RenderBefore(RenderContext context) 
        {
            Rectangle drawRect = ClientRectangle;

            if (_start)
                drawRect.X -= 4;

            drawRect.Width += 4;

            context.Renderer.RenderGlyph.DrawRibbonGroupSeparator(_ribbon.RibbonShape, 
                                                                  context,
                                                                  drawRect, 
                                                                  _ribbon.StateCommon.RibbonGeneral, 
                                                                  State);
        }
        #endregion
    }
}
