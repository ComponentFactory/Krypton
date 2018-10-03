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
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Details for an event that provides a button drag offset value.
	/// </summary>
	public class ButtonDragOffsetEventArgs : EventArgs
	{
		#region Instance Fields
        private Point _offset;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ButtonDragOffsetEventArgs class.
		/// </summary>
        /// <param name="offset">Mouse offset for button dragging.</param>
        public ButtonDragOffsetEventArgs(Point offset)
		{
            _offset = offset;
		}
        #endregion

        #region Point
        /// <summary>
        /// Gets access to the left mouse dragging offer.
        /// </summary>
        public Point PointOffset
        {
            get { return _offset; }
        }
        #endregion
    }
}
