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
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
    /// <summary>
    /// Target a transfer to the target workspace cell.
    /// </summary>
    public class DragTargetWorkspaceCellTransfer : DragTargetWorkspace
    {
        #region Instance Fields
        private KryptonWorkspaceCell _cell;
        private int _notDraggedPagesFromCell;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DragTargetWorkspaceCellTransfer class.
        /// </summary>
        /// <param name="screenRect">Rectangle for screen area.</param>
        /// <param name="hotRect">Rectangle for hot area.</param>
        /// <param name="drawRect">Rectangle for draw area.</param>
        /// <param name="workspace">Control instance for drop.</param>
        /// <param name="cell">Workspace cell as target for drop.</param>
        /// <param name="allowFlags">Only drop pages that have one of these flags defined.</param>
        public DragTargetWorkspaceCellTransfer(Rectangle screenRect,
                                               Rectangle hotRect,
                                               Rectangle drawRect,
                                               KryptonWorkspace workspace,
                                               KryptonWorkspaceCell cell,
                                               KryptonPageFlags allowFlags)
            : base(screenRect, hotRect, drawRect, DragTargetHint.Transfer, workspace, allowFlags)
        {
            _cell = cell;
            _notDraggedPagesFromCell = -1;
        }

        /// <summary>
        /// Release unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing">Called from Dispose method.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _cell = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Is this target a match for the provided screen position.
        /// </summary>
        /// <param name="screenPt">Position in screen coordinates.</param>
        /// <param name="dragEndData">Data to be dropped at destination.</param>
        /// <returns>True if a match; otherwise false.</returns>
        public override bool IsMatch(Point screenPt, PageDragEndData dragEndData)
        {
            // First time around...
            if (_notDraggedPagesFromCell == -1)
            {
                // Search for any pages that are not from this cell
                _notDraggedPagesFromCell = 0;
                foreach (KryptonPage page in dragEndData.Pages)
                    if (!_cell.Pages.Contains(page))
                    {
                        _notDraggedPagesFromCell = 1;
                        break;
                    }
            }

            // If 1 or more pages are not from this cell then allow transfer into the target
            if (_notDraggedPagesFromCell > 0)
                return base.IsMatch(screenPt, dragEndData);
            else
                return false;
        }

        /// <summary>
        /// Perform the drop action associated with the target.
        /// </summary>
        /// <param name="screenPt">Position in screen coordinates.</param>
        /// <param name="data">Data to pass to the target to process drop.</param>
        /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
        public override bool PerformDrop(Point screenPt, PageDragEndData data)
        {
            // Transfer the dragged pages into the existing cell
            KryptonPage page = ProcessDragEndData(Workspace, _cell, data);

            // Make the last page transfer the newly selected page of the cell
            if (page != null)
            {
                // Does the cell allow the selection of tabs?
                if (_cell.AllowTabSelect)
                    _cell.SelectedPage = page;

                if (!_cell.IsDisposed)
                {
                    // Without this DoEvents() call the dropping of multiple pages in a complex arrangement causes an exception for
                    // a complex reason that is hard to work out (i.e. I'm not entirely sure). Something to do with using select to
                    // change activation is causing the source workspace control to dispose to earlier.
                    Application.DoEvents();
                    _cell.Select();
                }
            }

            return true;
        }
        #endregion
    }
}
