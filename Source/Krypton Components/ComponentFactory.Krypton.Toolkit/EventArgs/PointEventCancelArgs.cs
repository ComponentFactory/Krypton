﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2017. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.5.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Drawing;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Details for an cancellable event that provides a Point value.
	/// </summary>
    public class PointEventCancelArgs : CancelEventArgs
	{
		#region Instance Fields
        private Point _point;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PointEventCancelArgs class.
		/// </summary>
        /// <param name="point">Point associated with event.</param>
        public PointEventCancelArgs(Point point)
		{
            _point = point;
		}
		#endregion

        #region Point
        /// <summary>
		/// Gets and sets the point.
		/// </summary>
        public Point Point
		{
            get { return _point; }
            set { _point = value; }
		}
		#endregion
    }
}
