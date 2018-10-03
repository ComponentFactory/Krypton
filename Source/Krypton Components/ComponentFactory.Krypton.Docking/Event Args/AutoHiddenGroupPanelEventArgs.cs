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
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for a AutoHiddenGroupPanelAdding/AutoHiddenGroupPanelRemoved events.
	/// </summary>
	public class AutoHiddenGroupPanelEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonAutoHiddenPanel _autoHiddenPanel;
        private KryptonDockingEdgeAutoHidden _element;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the AutoHiddenGroupPanelEventArgs class.
		/// </summary>
        /// <param name="autoHiddenPanel">Reference to auto hidden panel control instance.</param>
        /// <param name="element">Reference to docking auto hidden edge element that is managing the panel.</param>
        public AutoHiddenGroupPanelEventArgs(KryptonAutoHiddenPanel autoHiddenPanel,
                                             KryptonDockingEdgeAutoHidden element)
		{
            _autoHiddenPanel = autoHiddenPanel;
            _element = element;
		}
		#endregion

		#region Public
        /// <summary>
        /// Gets a reference to the KryptonAutoHiddenPanel control.
        /// </summary>
        public KryptonAutoHiddenPanel AutoHiddenPanelControl
        {
            get { return _autoHiddenPanel; }
        }

        /// <summary>
        /// Gets a reference to the KryptonDockingEdgeAutoHidden that is managing the edge.
        /// </summary>
        public KryptonDockingEdgeAutoHidden EdgeAutoHiddenElement
        {
            get { return _element; }
        }
        #endregion
	}
}
