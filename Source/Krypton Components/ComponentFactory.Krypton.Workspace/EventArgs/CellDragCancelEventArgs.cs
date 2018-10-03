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
using System.Drawing;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
	/// Details for an cancellable event that provides pages and cell associated with a page dragging event.
	/// </summary>
    public class CellDragCancelEventArgs : PageDragCancelEventArgs
	{
		#region Instance Fields
        private KryptonWorkspaceCell _cell;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the CellDragCancelEventArgs class.
		/// </summary>
        /// <param name="screenPoint">Screen point of the mouse.</param>
        /// <param name="screenOffset">Screen offset of the mouse to the source element.</param>
        /// <param name="c">Control that started the drag operation.</param>
        /// <param name="pages">Array of event associated pages.</param>
        /// <param name="cell">Workspace cell associated with pages.</param>
        public CellDragCancelEventArgs(Point screenPoint,
                                       Point screenOffset,
                                       Control c,
                                       KryptonPage[] pages,
                                       KryptonWorkspaceCell cell)
            : base(screenPoint, screenOffset, c, pages)
		{
            _cell = cell;
		}

        /// <summary>
        /// Initialize a new instance of the CellDragCancelEventArgs class.
        /// </summary>
        /// <param name="e">Event to upgrade to this event.</param>
        /// <param name="cell">Workspace cell associated with pages.</param>
        public CellDragCancelEventArgs(PageDragCancelEventArgs e,
                                       KryptonWorkspaceCell cell)
            : base(e.ScreenPoint, e.ElementOffset, e.Control, e.Pages)
        {
            _cell = cell;
        }
        #endregion

        #region Cell
        /// <summary>
        /// Gets access to associated workspace cell.
        /// </summary>
        public KryptonWorkspaceCell Cell
        {
            get { return _cell; }
        }
        #endregion
    }
}
