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
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Event arguments for the drop down menu of a gallery.
	/// </summary>
	public class GalleryDropMenuEventArgs : CancelEventArgs
	{
		#region Instance Fields
        private KryptonContextMenu _contextMenu;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the GalleryDropMenuEventArgs class.
		/// </summary>
        /// <param name="contextMenu">Context menu.</param>
        public GalleryDropMenuEventArgs(KryptonContextMenu contextMenu)
		{
            _contextMenu = contextMenu;
		}
		#endregion

		#region Public
		/// <summary>
		/// KryptonContextMenu for display.
		/// </summary>
        public KryptonContextMenu KryptonContextMenu
		{
            get { return _contextMenu; }
		}
		#endregion
	}
}
