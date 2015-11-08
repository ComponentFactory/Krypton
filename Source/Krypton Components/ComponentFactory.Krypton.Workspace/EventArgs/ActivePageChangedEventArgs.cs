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
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
	/// Data associated with a change in the active page.
	/// </summary>
	public class ActivePageChangedEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonPage _oldPage;
        private KryptonPage _newPage;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ActivePageChangedEventArgs class.
		/// </summary>
        /// <param name="oldPage">Previous active page value.</param>
        /// <param name="newPage">New active page value.</param>
        public ActivePageChangedEventArgs(KryptonPage oldPage,
                                          KryptonPage newPage)
		{
            _oldPage = oldPage;
            _newPage = newPage;
        }
		#endregion

		#region Public
		/// <summary>
        /// Gets the old page reference.
		/// </summary>
        public KryptonPage OldPage
		{
            get { return _oldPage; }
		}

        /// <summary>
        /// Gets the new page reference.
        /// </summary>
        public KryptonPage NewPage
        {
            get { return _newPage; }
        }
        #endregion
	}
}
