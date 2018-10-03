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
using System.ComponentModel; 
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
    /// Event arguments for events that need to request a KryptonPage from a provided unique name.
	/// </summary>
	public class RecreateLoadingPageEventArgs : CancelEventArgs
	{
		#region Instance Fields
        private KryptonPage _page;
        private string _uniqueName;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the RecreateLoadingPageEventArgs class.
		/// </summary>
        /// <param name="uniqueName">Unique name of the page that needs creating.</param>
        public RecreateLoadingPageEventArgs(string uniqueName)
            : base(false)
		{
            _uniqueName = uniqueName;
		}
        #endregion

		#region Public
        /// <summary>
        /// Gets and sets the page to be used for the requested unique name.
        /// </summary>
        public KryptonPage Page
        {
            get { return _page; }
            set { _page = value; }
        }

        /// <summary>
        /// Gets the unique name of the page requested to be recreated.
        /// </summary>
        public string UniqueName
        {
            get { return _uniqueName; }
        }
        #endregion
	}
}
