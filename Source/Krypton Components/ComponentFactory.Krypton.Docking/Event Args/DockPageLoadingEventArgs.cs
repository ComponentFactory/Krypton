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
    /// Event data for loading docking page configuration.
	/// </summary>
    public class DockPageLoadingEventArgs : DockGlobalLoadingEventArgs
	{
		#region Instance Fields
        private KryptonPage _page;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the DockPageLoadingEventArgs class.
		/// </summary>
        /// <param name="manager">Reference to owning docking manager instance.</param>
        /// <param name="xmlReading">Xml reader for persisting custom data.</param>
        /// <param name="page">Reference to page being loaded.</param>
        public DockPageLoadingEventArgs(KryptonDockingManager manager,
                                        XmlReader xmlReading,
                                        KryptonPage page)
            : base(manager, xmlReading)
		{
            _page = page;
		}
		#endregion

		#region Public
		/// <summary>
        /// Gets the loading page reference.
		/// </summary>
        public KryptonPage Page
		{
            get { return _page; }
		}
        #endregion
	}
}
