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
	/// View element that draws nothing and will center all children within itself.
	/// </summary>
    internal class ViewLayoutRibbonCenter : ViewComposite
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonCenterButton class.
        /// </summary>
        public ViewLayoutRibbonCenter()
        {
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutRibbonCenter:" + Id;
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

            // Layout each child centered within this space
            foreach (ViewBase child in this)
            {
                // Only layout visible children
                if (child.Visible)
                {
                    // Ask child for it's own preferred size
                    Size childPreferred = child.GetPreferredSize(context);

                    // Make sure the child is never bigger than the available space
                    if (childPreferred.Width > ClientWidth) childPreferred.Width = ClientWidth;
                    if (childPreferred.Height > ClientHeight) childPreferred.Height = ClientHeight;

                    // Find vertical and horizontal offsets for centering
                    int xOffset = (ClientWidth - childPreferred.Width) / 2;
                    int yOffset = (ClientHeight - childPreferred.Height) / 2;

                    // Create the rectangle that centers the child in our space
                    context.DisplayRectangle = new Rectangle(ClientRectangle.X + xOffset,
                                                             ClientRectangle.Y + yOffset,
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
