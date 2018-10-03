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
using System.IO;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
	/// Event data for listing pages unmatched during the load process.
	/// </summary>
	public class PagesUnmatchedEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonWorkspace _workspace;
        private List<KryptonPage> _unmatched;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PagesUnmatchedEventArgs class.
		/// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="unmatched">List of pages unmatched during the load process.</param>
        public PagesUnmatchedEventArgs(KryptonWorkspace workspace,
                                       List<KryptonPage> unmatched)
		{
            _workspace = workspace;
            _unmatched = unmatched;
		}
		#endregion

		#region Public
		/// <summary>
        /// Gets the workspace reference.
		/// </summary>
        public KryptonWorkspace Workspace
		{
            get { return _workspace; }
		}

        /// <summary>
        /// Gets the xml reader.
        /// </summary>
        public List<KryptonPage> Unmatched
        {
            get { return _unmatched; }
        }
        #endregion
	}
}
