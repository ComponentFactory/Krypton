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
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonNavigator to work within the docking framework.
    /// </summary>
    [ToolboxBitmap(typeof(KryptonDockableWorkspace), "ToolboxBitmaps.KryptonDockableNavigator.bmp")]
    public class KryptonDockableNavigator : KryptonNavigator
    {
        #region Events
        /// <summary>
        /// Occurs when a page is being inserted into the navigator.
        /// </summary>
        [Category("DockableNavigator")]
        [Description("Occurs when a page is added to a workspace cell.")]
        public event EventHandler<KryptonPageEventArgs> CellPageInserting;

        /// <summary>
        /// Occurs when a page requests that a drop down menu be shown.
        /// </summary>
        [Category("DockableNavigator")]
        [Description("Occurs when a page requests that a drop down menu be shown.")]
        public event EventHandler<CancelDropDownEventArgs> PageDropDownClicked;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockableNavigator class.
        /// </summary>
        public KryptonDockableNavigator()
        {
            // Hook into cell specific events
            ShowContextMenu += new EventHandler<ShowContextMenuArgs>(OnShowContextMenu);
            Pages.Inserting += new TypedHandler<KryptonPage>(OnPagesInserting);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the CellPageInserting event.
        /// </summary>
        /// <param name="e">An KryptonPageEventArgs containing the event data.</param>
        protected virtual void OnCellPageInserting(KryptonPageEventArgs e)
        {
            if (CellPageInserting != null)
                CellPageInserting(this, e);
        }

        /// <summary>
        /// Raises the PageDropDownClicked event.
        /// </summary>
        /// <param name="e">An CancelDropDownEventArgs containing the event data.</param>
        protected virtual void OnPageDropDownClicked(CancelDropDownEventArgs e)
        {
            if (PageDropDownClicked != null)
                PageDropDownClicked(this, e);
        }
        #endregion

        #region Private
        private void OnPagesInserting(object sender, TypedCollectionEventArgs<KryptonPage> e)
        {
            // Generate event so the docking element can decide on extra actions to be taken
            OnCellPageInserting(new KryptonPageEventArgs(e.Item, e.Index));
        }

        private void OnShowContextMenu(object sender, ShowContextMenuArgs e)
        {
            // Make sure we have a menu for displaying
            if (e.KryptonContextMenu == null)
                e.KryptonContextMenu = new KryptonContextMenu();

            // Use event to allow customization of the context menu
            CancelDropDownEventArgs args = new CancelDropDownEventArgs(e.KryptonContextMenu, e.Item);
            args.Cancel = e.Cancel;
            OnPageDropDownClicked(args);
            e.Cancel = args.Cancel;
        }
        #endregion
    }
}
