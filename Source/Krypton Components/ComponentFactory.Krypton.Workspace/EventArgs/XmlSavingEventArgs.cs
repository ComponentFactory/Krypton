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

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
	/// Event data for persisting extra data for a workspace.
	/// </summary>
	public class XmlSavingEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonWorkspace _workspace;
        private XmlWriter _xmlWriter;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the XmlSavingEventArgs class.
		/// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="xmlWriter">Xml writer for persisting custom data.</param>
        public XmlSavingEventArgs(KryptonWorkspace workspace,
                                  XmlWriter xmlWriter)
		{
            _workspace = workspace;
            _xmlWriter = xmlWriter;
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
        /// Gets the xml writer.
        /// </summary>
        public XmlWriter XmlWriter
        {
            get { return _xmlWriter; }
        }
        #endregion
	}
}
