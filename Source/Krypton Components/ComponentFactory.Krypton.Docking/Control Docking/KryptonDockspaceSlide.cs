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
    /// Extends the KryptonWorkspace to work within the docking edge of a control.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockspaceSlide : KryptonDockspace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockspaceSlide class.
        /// </summary>
        public KryptonDockspaceSlide()
        {
            // Cannot drag pages inside the sliding dockspace
            AllowPageDrag = false;
        }
        #endregion

        #region Protectect
        /// <summary>
        /// Initialize a new cell.
        /// </summary>
        /// <param name="cell">Cell being added to the control.</param>
        protected override void NewCellInitialize(KryptonWorkspaceCell cell)
        {
            // Let base class perform event hooking and customizations
            base.NewCellInitialize(cell);

            // We only ever show a single page in the dockspace, so remove default 
            // tabbed appearance and instead use a header group mode instead
            cell.NavigatorMode = NavigatorMode.HeaderGroup;
        }
        #endregion
    }
}
