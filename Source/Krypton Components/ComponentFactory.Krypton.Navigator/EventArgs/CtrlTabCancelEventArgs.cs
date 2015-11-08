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
