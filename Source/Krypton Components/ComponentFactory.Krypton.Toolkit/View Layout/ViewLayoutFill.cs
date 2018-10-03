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
	/// View element that is used to fill a docker area and positions a control to the same size.
	/// </summary>
    public class ViewLayoutFill : ViewLayoutNull
    {
        #region Instance Fields
        private Control _control;
        private Rectangle _fillRect;
        private Padding _displayPadding;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutNull class.
        /// </summary>
        public ViewLayoutFill()
            : this(null)
        {
            _displayPadding = Padding.Empty;
        }

        /// <summary>
        /// Initialize a new instance of the ViewLayoutNull class.
		/// </summary>
        /// <param name="control">Control to positon in fill location.</param>
        public ViewLayoutFill(Control control)
		{
            _control = control;
		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutFill:" + Id;
		}
		#endregion

        #region DisplayPadding
        /// <summary>
        /// Gets and sets the padding used around the control.
        /// </summary>
        public Padding DisplayPadding
        {
            get { return _displayPadding; }
            set { _displayPadding = value; }
        }
        #endregion

        #region FillRect
        /// <summary>
        /// Gets the latest calculated fill rectangle.
        /// </summary>
        public Rectangle FillRect
        {
            get { return _fillRect; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Get requested size of the control
            Size size = (_control != null ? _control.GetPreferredSize(context.DisplayRectangle.Size) : Size.Empty);

            // Return size with padding added on
            return new Size(size.Width + DisplayPadding.Horizontal,
                            size.Height + DisplayPadding.Vertical);
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

            // Cache the fill rectangle
            _fillRect = ClientRectangle;

            // Reduce the fill rectangle to account for the display padding
            _fillRect = CommonHelper.ApplyPadding(Orientation.Horizontal, _fillRect, DisplayPadding);

            // We let the OnLayout override for the control perform the
            // actually positioning of the fill contents.
        }
        #endregion
    }
}
