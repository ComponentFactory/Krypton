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
using System.Diagnostics;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event data for saving docking page configuration.
	/// </summary>
    public class DockPageSavingEventArgs : DockGlobalSavingEventArgs
	{
		#region Instance Fields
        private KryptonPage _page;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the DockPageSavingEventArgs class.
		/// </summary>
        /// <param name="manager">Reference to owning docking manager instance.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        /// <param name="page">Reference to page being saved.</param>
        public DockPageSavingEventArgs(KryptonDockingManager manager,
                                       XmlWriter xmlWriter,
                                       KryptonPage page)
            : base(manager, xmlWriter)
		{
            _page = page;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets the saving page reference.
        /// </summary>
        public KryptonPage Page
        {
            get { return _page; }
        }
        #endregion
	}
}
