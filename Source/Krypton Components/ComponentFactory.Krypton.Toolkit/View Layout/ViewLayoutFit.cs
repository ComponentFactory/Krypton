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
	/// View element that draws nothing and will split the space equally between the children.
	/// </summary>
	public class ViewLayoutFit : ViewComposite
    {
        #region Instance Fields
        private Orientation _orientation;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutFit class.
        /// </summary>
        /// <param name="orientation">Direction to fit.</param>
        public ViewLayoutFit(Orientation orientation)
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
            return "ViewLayoutFit:" + Id;
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
            int offset = 0;
            int space = (_orientation == Orientation.Vertical ? ClientHeight : ClientWidth);
            for(int i=0; i<Count; i++)
            {
                ViewBase child = this[i];

                // Find length of this item
                int length = 0;

                // If this is the last item then it takes the remaining space
                if (i == (Count - 1))
                    length = space;
                else
                {
                    // Give this item an equal portion of the remainder
                    length = space / (Count - i);
                }

                // Ask child for it's own preferred size
                Size childPreferred = child.GetPreferredSize(context);

                // Size child to our relevant dimension
                if (_orientation == Orientation.Vertical)
                    context.DisplayRectangle = new Rectangle(ClientRectangle.X,
                                                             ClientRectangle.Y + offset,
                                                             childPreferred.Width,
                                                             length);
                else
                    context.DisplayRectangle = new Rectangle(ClientRectangle.X + offset,
                                                             ClientRectangle.Y,
                                                             length,
                                                             ClientRectangle.Height);

                // Ask the child to layout
                child.Layout(context);

                // Adjust running values
                offset += length;
                space -= length;
            }

            // Put back the original display value now we have finished
            context.DisplayRectangle = original;
        }
		#endregion
	}
}
