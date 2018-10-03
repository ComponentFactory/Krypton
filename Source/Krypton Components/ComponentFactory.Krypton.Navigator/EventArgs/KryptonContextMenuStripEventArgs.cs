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
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Details providing a KryptonContextMenu instance.
	/// </summary>
    public class KryptonContextMenuEventArgs : KryptonPageEventArgs
	{
		#region Instance Fields
        private KryptonContextMenu _contextMenu;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the KryptonContextMenuEventArgs class.
		/// </summary>
		/// <param name="page">Page effected by event.</param>
		/// <param name="index">Index of page in the owning collection.</param>
        /// <param name="contextMenu">Prepopulated context menu ready for display.</param>
        public KryptonContextMenuEventArgs(KryptonPage page, 
                                           int index,
                                           KryptonContextMenu contextMenu)
			: base(page, index)
		{
            _contextMenu = contextMenu;
		}
		#endregion

        #region KryptonContextMenu
        /// <summary>
        /// Gets access to the KryptonContextMenu that is to be shown.
        /// </summary>
        public KryptonContextMenu KryptonContextMenu
        {
            get { return _contextMenu; }
        }
        #endregion
    }
}
