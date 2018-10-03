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
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
    internal class KryptonWorkspaceCellDesigner : KryptonNavigatorDesigner
    {
        #region Public
        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        public override SelectionRules SelectionRules
        {
            get { return SelectionRules.None; }
        }
        #endregion
        
        #region Implementation
        /// <summary>
        /// Occurs when the component is being removed from the designer.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A ComponentEventArgs containing event data.</param>
        protected override void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our control is being removed
            if (e.Component == Navigator)
            {
                // If this workspace cell is inside a parent
                KryptonWorkspaceCell cell = (KryptonWorkspaceCell)Navigator;
                if (cell.WorkspaceParent != null)
                {
                    // Cell an only be inside a workspace sequence
                    KryptonWorkspaceSequence sequence = (KryptonWorkspaceSequence)cell.WorkspaceParent;
                    if (sequence != null)
                    {
                        // Remove the cell from the parent
                        sequence.Children.Remove(cell);
                    }
                }
            }

            base.OnComponentRemoving(sender, e);
        }
        #endregion
    }
}
