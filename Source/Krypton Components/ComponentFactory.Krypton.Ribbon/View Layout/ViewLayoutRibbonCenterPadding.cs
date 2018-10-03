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
	/// View element adds padding to the contained elements and positions all elements centered.
	/// </summary>
    internal class ViewLayoutRibbonCenterPadding : ViewLayoutRibbonCenter
    {
        #region Instance Fields
        private Padding _preferredPadding;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonGroupImage class.
        /// </summary>
        /// <param name="preferredPadding">Padding to use when calculating space.</param>
        public ViewLayoutRibbonCenterPadding(Padding preferredPadding)
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
            return "ViewLayoutRibbonCenterPadding:" + Id;
		}
        #endregion

        #region PreferredPadding
        /// <summary>
        /// Gets and sets the preferred padding.
        /// </summary>
        public Padding PreferredPadding
        {
            get { return _preferredPadding; }
            set { _preferredPadding = value; }
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
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Reduce by the padding
            Rectangle innerRectangle = ClientRectangle;
            innerRectangle.X += PreferredPadding.Left;
            innerRectangle.Y += PreferredPadding.Top;
            innerRectangle.Width -= PreferredPadding.Horizontal;
            innerRectangle.Height -= PreferredPadding.Vertical;

            // Layout each child centered within this space
            foreach (ViewBase child in this)
            {
                // Only layout visible children
                if (child.Visible)
                {
                    // Ask child for it's own preferred size
                    Size childPreferred = child.GetPreferredSize(context);

                    // Make sure the child is never bigger than the available space
                    if (childPreferred.Width > ClientRectangle.Width) childPreferred.Width = ClientWidth;
                    if (childPreferred.Height > ClientRectangle.Height) childPreferred.Height = ClientHeight;

                    // Find vertical and horizontal offsets for centering
                    int xOffset = (innerRectangle.Width - childPreferred.Width) / 2;
                    int yOffset = (innerRectangle.Height - childPreferred.Height) / 2;

                    // Create the rectangle that centers the child in our space
                    context.DisplayRectangle = new Rectangle(innerRectangle.X + xOffset,
                                                             innerRectangle.Y + yOffset,
                                                             childPreferred.Width,
                                                             childPreferred.Height);

                    // Finally ask the child to layout
                    child.Layout(context);
                }
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = ClientRectangle;
        }
        #endregion
	}
}
