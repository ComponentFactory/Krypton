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
	/// Positions a separator to take up space without drawing.
	/// </summary>
    public class ViewLayoutSeparator : ViewLeaf
    {
        #region Instance Fields
        private int _width;
        private int _height;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonSeparator class.
		/// </summary>
        /// <param name="length">Length of the separator.</param>
        public ViewLayoutSeparator(int length)
            : this(length, length)
        {
        }

        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonSeparator class.
        /// </summary>
        /// <param name="width">Width of the separator.</param>
        /// <param name="height">Height of the separator.</param>
        public ViewLayoutSeparator(int width,
                                   int height)
        {
            _width = width;
            _height = height;
        }
        
        /// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewLayoutSeparator:" + Id;
		}
		#endregion

        #region SeparatorSize
        /// <summary>
        /// Gets and sets the separator size.
        /// </summary>
        public Size SeparatorSize
        {
            get { return new Size(_width, _height); }
            
            set
            {
                _width = value.Width;
                _height = value.Height;
            }
        }
        #endregion

        #region Layout
        /// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override Size GetPreferredSize(ViewLayoutContext context)
		{
            // Always return the same minimum size
            return new Size(_width, _height);
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
    }
}
