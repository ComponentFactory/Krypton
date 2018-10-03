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
	/// View element that draws nothing and will use a padding around the children.
	/// </summary>
	public class ViewLayoutPadding : ViewComposite
    {
        #region Instance Fields
        private Padding _displayPadding;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutPadding class.
        /// </summary>
        public ViewLayoutPadding()
            : this(Padding.Empty, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ViewLayoutPadding class.
        /// </summary>
        /// <param name="displayPadding">Padding to use around area.</param>
        public ViewLayoutPadding(Padding displayPadding)
            : this(displayPadding, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ViewLayoutPadding class.
        /// </summary>
        /// <param name="displayPadding">Padding to use around area.</param>
        /// <param name="child">Child to add into view hierarchy.</param>
        public ViewLayoutPadding(Padding displayPadding, ViewBase child)
        {
            _displayPadding = displayPadding;
            Add(child);
        }

        /// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutPadding:" + Id;
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

			// Ask base class to find preferred size of all children
            Size preferredSize = base.GetPreferredSize(context);

            // Add on the display padding
            preferredSize.Width += _displayPadding.Horizontal;
            preferredSize.Height += _displayPadding.Vertical;

            return preferredSize;
		}

		/// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override void Layout(ViewLayoutContext context)
		{
			Debug.Assert(context != null);

            // Take on the provided space
            ClientRectangle = context.DisplayRectangle;

            // Reduce space by the padding value
            context.DisplayRectangle = CommonHelper.ApplyPadding(Orientation.Horizontal, ClientRectangle, _displayPadding);

			// Layout each of the children with the new size
			foreach (ViewBase child in this)
			{
				// Only layout visible children
				if (child.Visible)
					child.Layout(context);
			}

            // Restore original display rect we were given
            context.DisplayRectangle = ClientRectangle;
		}
		#endregion
	}
}
