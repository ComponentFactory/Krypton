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
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for a DockspaceCellAdding/DockspaceCellRemoving events.
	/// </summary>
	public class DockspaceCellEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonDockspace _dockspace;
        private KryptonDockingDockspace _element;
        private KryptonWorkspaceCell _cell;
        #endregion
		
        #region Identity
		/// <summary>
        /// Initialize a new instance of the DockspaceCellEventArgs class.
		/// </summary>
        /// <param name="dockspace">Reference to existing dockspace control instance.</param>
        /// <param name="element">Reference to docking dockspace element that is managing the dockspace control.</param>
        /// <param name="cell">Reference to dockspace control cell instance.</param>
        public DockspaceCellEventArgs(KryptonDockspace dockspace,
                                      KryptonDockingDockspace element,
                                      KryptonWorkspaceCell cell)
		{
            _dockspace = dockspace;
            _element = element;
            _cell = cell;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets a reference to the KryptonDockspace that contains the cell.
        /// </summary>
        public KryptonDockspace DockspaceControl
        {
            get { return _dockspace; }
        }

        /// <summary>
        /// Gets a reference to the KryptonDockingDockspace that is managing the dockspace.
        /// </summary>
        public KryptonDockingDockspace DockspaceElement
        {
            get { return _element; }
        }

        /// <summary>
        /// Gets a reference to the KryptonWorkspaceCell control.
        /// </summary>
        public KryptonWorkspaceCell CellControl
        {
            get { return _cell; }
        }
        #endregion
	}
}
