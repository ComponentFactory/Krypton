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
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for events that need a screen point and element offset.
	/// </summary>
	public class ScreenAndOffsetEventArgs : EventArgs
	{
		#region Instance Fields
        private Point _screenPoint;
        private Point _elementOffset;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ScreenAndOffsetEventArgs class.
		/// </summary>
        /// <param name="screenPoint">Screen point.</param>
        /// <param name="elementOffset">Element offset.</param>
        public ScreenAndOffsetEventArgs(Point screenPoint, Point elementOffset)
		{
            _screenPoint = screenPoint;
            _elementOffset = elementOffset;
        }
        #endregion

		#region Public
        /// <summary>
        /// Gets the screen point.
        /// </summary>
        public Point ScreenPoint
        {
            get { return _screenPoint; }
        }

        /// <summary>
        /// Gets the element offset.
        /// </summary>
        public Point ElementOffset
        {
            get { return _elementOffset; }
        }
        #endregion
	}
}
