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
    /// Event arguments for a FloatspaceCellAdding/FloatingCellRemoving events.
	/// </summary>
	public class FloatspaceCellEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonFloatspace _floatspace;
        private KryptonDockingFloatspace _element;
        private KryptonWorkspaceCell _cell;
        #endregion
		
        #region Identity
		/// <summary>
        /// Initialize a new instance of the FloatspaceCellEventArgs class.
		/// </summary>
        /// <param name="floatspace">Reference to existing floatspace control instance.</param>
        /// <param name="element">Reference to docking floatspace element that is managing the floatspace control.</param>
        /// <param name="cell">Reference tofloatspace control cell instance.</param>
        public FloatspaceCellEventArgs(KryptonFloatspace floatspace,
                                       KryptonDockingFloatspace element,
                                       KryptonWorkspaceCell cell)
		{
            _floatspace = floatspace;
            _element = element;
            _cell = cell;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets a reference to the KryptonFloatspace control.
        /// </summary>
        public KryptonFloatspace FloatspaceControl
        {
            get { return _floatspace; }
        }

        /// <summary>
        /// Gets a reference to the KryptonDockingFloatspace that is managing the floatspace.
        /// </summary>
        public KryptonDockingFloatspace FloatspaceElement
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
