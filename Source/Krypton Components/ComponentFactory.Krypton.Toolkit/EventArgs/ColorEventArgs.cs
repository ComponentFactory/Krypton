// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 4.5.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Drawing;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Color event data.
	/// </summary>
	public class ColorEventArgs : EventArgs
	{
		#region Instance Fields
        private Color _color;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ColorEventArgs class.
		/// </summary>
        /// <param name="color">Color associated with the event.</param>
        public ColorEventArgs(Color color)
		{
            _color = color;
		}
		#endregion

		#region Public
		/// <summary>
		/// Gets the color.
		/// </summary>
        public Color Color
		{
            get { return _color; }
		}
		#endregion
	}
}
