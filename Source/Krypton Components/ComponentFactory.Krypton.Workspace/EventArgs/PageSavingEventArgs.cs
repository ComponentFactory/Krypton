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
using System.IO;
using System.Xml;
using System.Drawing;
using System.Diagnostics;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
	/// Event data for persisting extra data for a workspace cell page.
	/// </summary>
	public class PageSavingEventArgs : XmlSavingEventArgs
	{
		#region Instance Fields
        private KryptonPage _page;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PageSavingEventArgs class.
		/// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="page">Reference to owning workspace cell page.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        public PageSavingEventArgs(KryptonWorkspace workspace,
                                   KryptonPage page,
                                   XmlWriter xmlWriter)
            : base(workspace, xmlWriter)
		{
            _page = page;
		}
		#endregion

		#region Public
		/// <summary>
        /// Gets the workspace cell page reference.
		/// </summary>
        public KryptonPage Page
		{
            get { return _page; }
		}
        #endregion
	}
}
