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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// View element adds padding to the provided drawing area.
	/// </summary>
    internal class ViewLayoutRibbonPadding : ViewComposite
    {
        #region Instance Fields
        private Padding _preferredPadding;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonPadding class.
        /// </summary>
        /// <param name="preferredPadding">Padding to use when calculating space.</param>
        public ViewLayoutRibbonPadding(Padding preferredPadding)
        {
            _preferredPadding = preferredPadding;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutRibbonPadding:" + Id;
		}
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Get the preferred size of the contained content
            Size preferredSize = base.GetPreferredSize(context);

            // Add on the padding we need around edges
            return new Size(preferredSize.Width + _preferredPadding.Horizontal,
                            preferredSize.Height + _preferredPadding.Vertical);
        }

        /// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override void Layout(ViewLayoutContext context)
		{
            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");
            
            // We take on all the available display area
			ClientRectangle = context.DisplayRectangle;

            // Find the rectangle for the child elements by applying padding
            context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _preferredPadding);

            // Let base perform actual layout process of child elements
            base.Layout(context);

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
	}
}
