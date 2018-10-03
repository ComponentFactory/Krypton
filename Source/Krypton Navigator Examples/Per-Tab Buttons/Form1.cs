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

namespace PerTabButtons
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            addContext_Click(this, EventArgs.Empty);
            kryptonNavigator.SelectedIndex = 1;
            addArrow_Click(this, EventArgs.Empty);
            kryptonNavigator.SelectedIndex = 2;
            addText_Click(this, EventArgs.Empty);
            kryptonNavigator.SelectedIndex = 0;

            UpdateControlsFromNavigator();
        }

        private void UpdateControlsFromNavigator()
        {
            // Update Mode
            radioModeTabs.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.BarTabGroup);
            radioModeRibbonTabs.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.BarRibbonTabGroup);
            radioModesCheckButton.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.BarCheckButtonGroupOutside);
            radioModesStack.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.StackCheckButtonGroup);
            radioModesOutlook.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.OutlookFull);

            // Set mode specific properties
            switch (kryptonNavigator.NavigatorMode)
            {
                case NavigatorMode.BarRibbonTabGroup:
                case NavigatorMode.BarRibbonTabOnly:
                    kryptonNavigator.PageBackStyle = PaletteBackStyle.ControlRibbon;
                    kryptonNavigator.Group.GroupBackStyle = PaletteBackStyle.ControlRibbon;
                    kryptonNavigator.Group.GroupBorderStyle = PaletteBorderStyle.ControlRibbon;
                    break;
                default:
                    kryptonNavigator.PageBackStyle = PaletteBackStyle.ControlClient;
                    kryptonNavigator.Group.GroupBackStyle = PaletteBackStyle.ControlClient;
                    kryptonNavigator.Group.GroupBorderStyle = PaletteBorderStyle.ControlClient;
                    break;
            }
        }

        private void radioModeTabs_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModeTabs.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.BarTabGroup;
                UpdateControlsFromNavigator();
            }
        }

        private void radioModeRibbonTabs_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModeRibbonTabs.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.BarRibbonTabGroup;
                UpdateControlsFromNavigator();
            }
        }

        private void radioModesCheckButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModesCheckButton.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.BarCheckButtonGroupOutside;
                UpdateControlsFromNavigator();
            }
        }

        private void radioModesStack_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModesStack.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.StackCheckButtonGroup;
                UpdateControlsFromNavigator();
            }
        }

        private void radioModesOutlook_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModesOutlook.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.OutlookFull;
                UpdateControlsFromNavigator();
            }
        }

        private void addContext_Click(object sender, EventArgs e)
        {
            if (kryptonNavigator.SelectedPage != null)
            {
                ButtonSpecAny bsa = new ButtonSpecAny();
                bsa.Type = PaletteButtonSpecStyle.Context;
                bsa.Style = PaletteButtonStyle.Standalone;
                bsa.KryptonContextMenu = kryptonContextMenu;
                bsa.Tag = kryptonNavigator.SelectedPage;
                kryptonNavigator.SelectedPage.ButtonSpecs.Add(bsa);
            }
        }

        private void addText_Click(object sender, EventArgs e)
        {
            if (kryptonNavigator.SelectedPage != null)
            {
                ButtonSpecAny bsa = new ButtonSpecAny();
                bsa.Style = PaletteButtonStyle.Standalone;
                bsa.Text = DateTime.Now.Millisecond.ToString();
                bsa.Tag = kryptonNavigator.SelectedPage;
                kryptonNavigator.SelectedPage.ButtonSpecs.Add(bsa);
            }
        }

        private void addArrow_Click(object sender, EventArgs e)
        {
            if (kryptonNavigator.SelectedPage != null)
            {
                ButtonSpecAny bsa = new ButtonSpecAny();
                bsa.Style = PaletteButtonStyle.Alternate;
                bsa.Type = PaletteButtonSpecStyle.ArrowRight;
                bsa.Tag = kryptonNavigator.SelectedPage;
                kryptonNavigator.SelectedPage.ButtonSpecs.Add(bsa);
            }
        }

        private void clearButtons_Click(object sender, EventArgs e)
        {
            if (kryptonNavigator.SelectedPage != null)
                kryptonNavigator.SelectedPage.ButtonSpecs.Clear();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
