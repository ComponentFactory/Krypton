// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;
using ComponentFactory.Krypton.Docking;

namespace DockingFlags
{
    public partial class ContentFlags : UserControl
    {
        private KryptonPage _page;

        public ContentFlags()
            : this(null)
        {
        }

        public ContentFlags(KryptonPage page)
        {
            _page = page;
            InitializeComponent();
        }

        private void kryptonPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // Only interested in left mouse down
            if (e.Button == MouseButtons.Left)
            {
                // If the content does not have the focus then give it focus now
                if (!ContainsFocus)
                    kryptonPanel.SelectNextControl(this, true, true, true, false);
            }
        }

        private void ContentFlags_Load(object sender, EventArgs e)
        {
            // Set checkbox controls to reflect current page flag settings
            cbDocked.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowDocked);
            cbAutoHidden.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowAutoHidden);
            cbFloating.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowFloating);
            cbWorkspace.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowWorkspace);
            cbNavigator.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowNavigator);
            cbDropDown.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowDropDown);
            cbClose.Checked = _page.AreFlagsSet(KryptonPageFlags.DockingAllowClose);
        }

        private void cbDocked_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDocked.Checked)
                _page.SetFlags(KryptonPageFlags.DockingAllowDocked);
            else
                _page.ClearFlags(KryptonPageFlags.DockingAllowDocked);
        }

        private void cbAutoHidden_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoHidden.Checked)
                _page.SetFlags(KryptonPageFlags.DockingAllowAutoHidden);
            else
                _page.ClearFlags(KryptonPageFlags.DockingAllowAutoHidden);
        }

        private void cbFloating_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFloating.Checked)
                _page.SetFlags(KryptonPageFlags.DockingAllowFloating);
            else
                _page.ClearFlags(KryptonPageFlags.DockingAllowFloating);
        }

        private void cbWorkspace_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWorkspace.Checked)
                _page.SetFlags(KryptonPageFlags.DockingAllowWorkspace);
            else
                _page.ClearFlags(KryptonPageFlags.DockingAllowWorkspace);
        }

        private void cbNavigator_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNavigator.Checked)
                _page.SetFlags(KryptonPageFlags.DockingAllowNavigator);
            else
                _page.ClearFlags(KryptonPageFlags.DockingAllowNavigator);
        }

        private void cbDropDown_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDropDown.Checked)
                _page.SetFlags(KryptonPageFlags.DockingAllowDropDown);
            else
                _page.ClearFlags(KryptonPageFlags.DockingAllowDropDown);
        }

        private void cbClose_CheckedChanged(object sender, EventArgs e)
        {
            if (cbClose.Checked)
                _page.SetFlags(KryptonPageFlags.DockingAllowClose);
            else
                _page.ClearFlags(KryptonPageFlags.DockingAllowClose);
        }
    }
}
