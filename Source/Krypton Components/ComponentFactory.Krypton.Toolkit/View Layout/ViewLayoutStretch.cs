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
	/// View element that draws nothing and will stretch children to fill one dimension.
	/// </summary>
	public class ViewLayoutStretch : ViewComposite
    {
        #region Instance Fields
        private Orientation _orientation;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutCenter class.
        /// </summary>
        /// <param name="orientation">Direction to stretch.</param>
        public ViewLayoutStretch(Orientation orientation)
        {
            _orientation = orientation;
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutStretch:" + Id;
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
            Rectangle original = context.DisplayRectangle;
            ClientRectangle = original;

            // Layout each child
            foreach (ViewBase child in this)
            {
                // Only layout visible children
                if (child.Visible)
                {
                    // Ask child for it's own preferred size
                    Size childPreferred = child.GetPreferredSize(context);

                    // Size child to our relevant dimension
                    if (_orientation == Orientation.Vertical)
                        context.DisplayRectangle = new Rectangle(ClientRectangle.X,
                                                                 ClientRectangle.Y,
                                                                 childPreferred.Width,
                                                                 ClientRectangle.Height);
                    else
                        context.DisplayRectangle = new Rectangle(ClientRectangle.X,
                                                                 ClientRectangle.Y,
                                                                 childPreferred.Width,
                                                                 ClientRectangle.Height);

                    // Finally ask the child to layout
                    child.Layout(context);
                }
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = original;
        }
		#endregion
	}
}
