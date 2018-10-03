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
using System.ComponentModel;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Details for control tabbing events.
	/// </summary>
	public class CtrlTabCancelEventArgs : CancelEventArgs
	{
		#region Instance Fields
        private bool _forward;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the CtrlTabCancelEventArgs class.
		/// </summary>
        /// <param name="forward">Tabbing in forward or backwards direction.</param>
        public CtrlTabCancelEventArgs(bool forward)
		{
            _forward = forward;
		}
		#endregion

		#region Forward
		/// <summary>
		/// Gets a value indicating if control tabbing forward.
		/// </summary>
		public bool Forward
		{
            get { return _forward; }
		}
		#endregion
    }
}
