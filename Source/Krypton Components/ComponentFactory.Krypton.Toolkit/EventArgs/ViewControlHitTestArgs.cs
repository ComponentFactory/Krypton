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
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Details for context menu related events.
	/// </summary>
	public class ViewControlHitTestArgs : CancelEventArgs
	{
		#region Instance Fields
        private Point _pt;
        private IntPtr _result;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the ViewControlHitTestArgs class.
        /// </summary>
        /// <param name="pt">Point associated with hit test message.</param>
        public ViewControlHitTestArgs(Point pt)
            : base(true)
        {
            _pt = pt;
            _result = IntPtr.Zero;
        }
        #endregion

		#region Public
		/// <summary>
		/// Gets access to the point.
		/// </summary>
        public Point Point
		{
			get { return _pt; }
		}

        /// <summary>
        /// Gets and sets the result.
        /// </summary>
        public IntPtr Result
        {
            get { return _result; }
            set { _result = value; }
        }
        #endregion
	}
}
