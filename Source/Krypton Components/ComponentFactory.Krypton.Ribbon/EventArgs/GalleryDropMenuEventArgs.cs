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
