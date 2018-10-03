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
    /// Extends the KryptonWorkspace to work within the docking framework.
    /// </summary>
    [ToolboxBitmap(typeof(KryptonDockableWorkspace), "ToolboxBitmaps.KryptonDockableWorkspace.bmp")]
    public class KryptonDockableWorkspace : KryptonSpace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockableWorkspace class.
        /// </summary>
        public KryptonDockableWorkspace()
            : base("Workspace")
        {
            // Override the base class and allow the workspace context menu for the tab to be shown
            ContextMenus.ShowContextMenu = true;
        }

        /// <summary>
        /// Gets a string representation of the instance.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            return "KryptonDockableWorkspace " + Dock.ToString();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets a value indicating if docking specific appearance should be applied.
        /// </summary>
        protected override bool ApplyDockingAppearance
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating if docking specific close action should be applied.
        /// </summary>
        protected override bool ApplyDockingCloseAction
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating if docking specific pin actions should be applied.
        /// </summary>
        protected override bool ApplyDockingPinAction
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating if docking specific drop down actions should be applied.
        /// </summary>
        protected override bool ApplyDockingDropDownAction
        {
            get { return false; }
        }

        /// <summary>
        /// Initialize a new cell.
        /// </summary>
        /// <param name="cell">Cell being added to the control.</param>
        protected override void NewCellInitialize(KryptonWorkspaceCell cell)
        {
            // Let base class perform event hooking and customizations
            base.NewCellInitialize(cell);

            // By default the new cell does not have focus and so should have standard looking tabs
            cell.Bar.TabStyle = TabStyle.StandardProfile;
            cell.CloseAction += new EventHandler<CloseActionEventArgs>(OnCellCloseAction);
        }

        /// <summary>
        /// Raises the ActiveCellChanged event.
        /// </summary>
        /// <param name="e">An ActiveCellChangedEventArgs containing the event data.</param>
        protected override void OnActiveCellChanged(ActiveCellChangedEventArgs e)
        {
            // Ensure all but the newly selected cell have a lower profile appearance
            KryptonWorkspaceCell cell = FirstCell();
            while (cell != null)
            {
                if (e.NewCell != cell)
                    cell.Bar.TabStyle = TabStyle.StandardProfile;
                cell = NextCell(cell);
            }

            // Ensure the newly selected cell has a higher profile appearance
            if (e.NewCell != null)
                e.NewCell.Bar.TabStyle = TabStyle.HighProfile;

            base.OnActiveCellChanged(e);
        }
        #endregion   

        #region Implementation
        private void OnCellCloseAction(object sender, CloseActionEventArgs e)
        {
            OnPageCloseClicked(new UniqueNameEventArgs(e.Item.UniqueName));
        }
        #endregion
    }
}
