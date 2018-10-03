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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Provide a KryptonPageFlags enumeration with event details.
	/// </summary>
	public class KryptonPageFlagsEventArgs : EventArgs
	{
		#region Instance Fields
		private KryptonPageFlags _flags;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the KryptonPageFlagsEventArgs class.
		/// </summary>
        /// <param name="flags">KryptonPageFlags enumeration.</param>
        public KryptonPageFlagsEventArgs(KryptonPageFlags flags)
		{
			// Remember parameter details
            _flags = flags;
		}
		#endregion

		#region Public
		/// <summary>
        /// Gets the KryptonPageFlags enumeration value.
		/// </summary>
        public KryptonPageFlags Flags
		{
			get { return _flags; }
		}
		#endregion
	}
}
