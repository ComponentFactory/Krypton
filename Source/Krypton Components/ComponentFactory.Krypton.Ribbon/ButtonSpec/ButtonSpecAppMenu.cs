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
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Button specification appropriate for an application menu
    /// </summary>
    public class ButtonSpecAppMenu : ButtonSpecAny
    {
        #region Protected
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // Only if associated view is enabled to we perform an action
            if (GetViewEnabled())
            {
                if ((KryptonContextMenu == null) &&
                    (ContextMenuStrip == null))
                {
                    // Remove the popup app menu that is showing
                    VisualPopupManager.Singleton.EndAllTracking();
                }

                // If a checked style button
                if (Checked != ButtonCheckState.NotCheckButton)
                {
                    // Then invert the checked state
                    if (Checked == ButtonCheckState.Unchecked)
                        Checked = ButtonCheckState.Checked;
                    else
                        Checked = ButtonCheckState.Unchecked;
                }

                GenerateClick(e);
            }
        }
        #endregion
    }
}
