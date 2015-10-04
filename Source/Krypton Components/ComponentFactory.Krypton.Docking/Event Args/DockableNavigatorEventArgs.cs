// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 4.4.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Drawing;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for a DockableNavigatorEventArgs event.
	/// </summary>
	public class DockableNavigatorEventArgs : EventArgs
	{
		#region Instance Fields
        private KryptonDockableNavigator _navigator;
        private KryptonDockingNavigator _element;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the DockableNavigatorEventArgs class.
		/// </summary>
        /// <param name="navigator">Reference to dockable navigator control instance.</param>
        /// <param name="element">Reference to docking navigator element that is managing the dockable workspace control.</param>
        public DockableNavigatorEventArgs(KryptonDockableNavigator navigator,
                                          KryptonDockingNavigator element)
		{
            _navigator = navigator;
            _element = element;
		}
        #endregion

		#region Public
        /// <summary>
        /// Gets a reference to the KryptonDockableNavigator control.
        /// </summary>
        public KryptonDockableNavigator DockableNavigatorControl
        {
            get { return _navigator; }
        }

        /// <summary>
        /// Gets a reference to the KryptonDockingNavigator that is managing the dockable workspace control.
        /// </summary>
        public KryptonDockingNavigator DockingNavigatorElement
        {
            get { return _element; }
        }
        #endregion
	}
}
