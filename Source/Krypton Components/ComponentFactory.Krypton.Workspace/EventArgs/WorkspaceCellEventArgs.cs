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

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
	/// Workspace cell event data.
	/// </summary>
	public class WorkspaceCellEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonWorkspaceCell _cell;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the WorkspaceCellEventArgs class.
		/// </summary>
        /// <param name="cell">Workspace cell associated with the event.</param>
        public WorkspaceCellEventArgs(KryptonWorkspaceCell cell)
		{
            _cell = cell;
		}
		#endregion

		#region Public
		/// <summary>
        /// Gets the cell reference.
		/// </summary>
        public KryptonWorkspaceCell Cell
		{
            get { return _cell; }
		}
		#endregion
	}
}
