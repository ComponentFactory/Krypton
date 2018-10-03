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
    /// Event arguments for a DockableWorkspaceCellAdding/DockableWorkspaceCellRemoving events.
	/// </summary>
	public class DockableWorkspaceCellEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonDockableWorkspace _workspace;
        private KryptonDockingWorkspace _element;
        private KryptonWorkspaceCell _cell;
        #endregion
		
        #region Identity
		/// <summary>
        /// Initialize a new instance of the DockableWorkspaceCellEventArgs class.
		/// </summary>
        /// <param name="workspace">Reference to existing dockable workspace control instance.</param>
        /// <param name="element">Reference to docking workspace element that is managing the dockable workspace control.</param>
        /// <param name="cell">Reference to workspace control cell instance.</param>
        public DockableWorkspaceCellEventArgs(KryptonDockableWorkspace workspace,
                                              KryptonDockingWorkspace element,
                                              KryptonWorkspaceCell cell)
		{
            _workspace = workspace;
            _element = element;
            _cell = cell;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets a reference to the KryptonDockableWorkspace that contains the cell.
        /// </summary>
        public KryptonDockableWorkspace DockableWorkspaceControl
        {
            get { return _workspace; }
        }

        /// <summary>
        /// Gets a reference to the KryptonDockingWorkspace that is managing the dockable workspace.
        /// </summary>
        public KryptonDockingWorkspace WorkspaceElement
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
