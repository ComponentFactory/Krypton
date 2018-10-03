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

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for events that need to provide a store page reference.
	/// </summary>
	public class StorePageEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonStorePage _storePage;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the StorePageEventArgs class.
		/// </summary>
        /// <param name="storePage">Reference to store page that is associated with the event.</param>
        public StorePageEventArgs(KryptonStorePage storePage)
		{
            _storePage = storePage;
		}
        #endregion

		#region Public
        /// <summary>
        /// Gets a reference to store page that is associated with the event.
        /// </summary>
        public KryptonStorePage StorePage
        {
            get { return _storePage; }
        }
        #endregion
	}
}
