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
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Override the contained child to present a fixed size.
	/// </summary>
    public class ViewDecoratorFixedSize : ViewDecorator
	{
		#region Instance Fields
		private Size _fixedSize;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the ViewBase class.
		/// </summary>
        public ViewDecoratorFixedSize(ViewBase child, Size fixedSize)
            : base(child)
		{
            _fixedSize = fixedSize;

		}

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDecoratorFixedSize:" + Id;
		}
		#endregion

        #region FixedSize
        /// <summary>
        /// Gets and sets the fixed size for laying out the contained child element.
        /// </summary>
        public Size FixedSize
        {
            get { return _fixedSize; }
            set { _fixedSize = value; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            // Always provide the requested fixed size
            return _fixedSize;
        }
        #endregion
    }
}
