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

namespace PopupPages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateControlsFromNavigator();
        }

        private void UpdateControlsFromNavigator()
        {
            // Update mode control
            radioBarTabOnly.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.BarTabOnly);
            radioBarRibbonTabOnly.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.BarRibbonTabOnly);
            radioBarCheckButtonGroupOnly.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.BarCheckButtonGroupOnly);
            radioBarCheckButtonOnly.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.BarCheckButtonOnly);
            radioHeaderBarCheckButtonOnly.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.HeaderBarCheckButtonOnly);
            radioOutlookMini.Checked = (kryptonNavigator.NavigatorMode == NavigatorMode.OutlookMini);

            // Update bar orientation controls
            radioOrientationTop.Checked = (kryptonNavigator.Bar.BarOrientation == VisualOrientation.Top);
            radioOrientationBottom.Checked = (kryptonNavigator.Bar.BarOrientation == VisualOrientation.Bottom);
            radioOrientationLeft.Checked = (kryptonNavigator.Bar.BarOrientation == VisualOrientation.Left);
            radioOrientationRight.Checked = (kryptonNavigator.Bar.BarOrientation == VisualOrientation.Right);

            // Update popup page controls
            numericBorder.Value = kryptonNavigator.PopupPages.Border;
            numericGap.Value = kryptonNavigator.PopupPages.Gap;
            comboBoxElement.Text = kryptonNavigator.PopupPages.Element.ToString();
            comboBoxPosition.Text = kryptonNavigator.PopupPages.Position.ToString();
        }

        private void radioBarTabOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBarTabOnly.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.BarTabOnly;
                UpdateControlsFromNavigator();
            }
        }

        private void radioBarRibbonTabOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBarRibbonTabOnly.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.BarRibbonTabOnly;
                UpdateControlsFromNavigator();
            }
        }

        private void radioBarCheckButtonGroupOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBarCheckButtonGroupOnly.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.BarCheckButtonGroupOnly;
                UpdateControlsFromNavigator();
            }
        }

        private void radioBarCheckButtonOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBarCheckButtonOnly.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.BarCheckButtonOnly;
                UpdateControlsFromNavigator();
            }
        }

        private void radioHeaderBarCheckButtonOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (radioHeaderBarCheckButtonOnly.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.HeaderBarCheckButtonOnly;
                UpdateControlsFromNavigator();
            }
        }

        private void radioOutlookMini_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOutlookMini.Checked)
            {
                kryptonNavigator.NavigatorMode = NavigatorMode.OutlookMini;
                UpdateControlsFromNavigator();
            }
        }

        private void radioOrientationTop_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOrientationTop.Checked)
            {
                kryptonNavigator.Bar.BarOrientation = VisualOrientation.Top;
                kryptonNavigator.Header.HeaderPositionBar = VisualOrientation.Top;
                kryptonNavigator.Dock = DockStyle.Top;
                UpdateControlsFromNavigator();
            }
        }

        private void radioOrientationBottom_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOrientationBottom.Checked)
            {
                kryptonNavigator.Bar.BarOrientation = VisualOrientation.Bottom;
                kryptonNavigator.Header.HeaderPositionBar = VisualOrientation.Bottom;
                kryptonNavigator.Dock = DockStyle.Bottom;
                UpdateControlsFromNavigator();
            }
        }

        private void radioOrientationLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOrientationLeft.Checked)
            {
                kryptonNavigator.Bar.BarOrientation = VisualOrientation.Left;
                kryptonNavigator.Header.HeaderPositionBar = VisualOrientation.Left;
                kryptonNavigator.Dock = DockStyle.Left;
                UpdateControlsFromNavigator();
            }
        }

        private void radioOrientationRight_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOrientationRight.Checked)
            {
                kryptonNavigator.Bar.BarOrientation = VisualOrientation.Right;
                kryptonNavigator.Header.HeaderPositionBar = VisualOrientation.Right;
                kryptonNavigator.Dock = DockStyle.Right;
                UpdateControlsFromNavigator();
            }
        }

        private void numericBorder_ValueChanged(object sender, EventArgs e)
        {
            kryptonNavigator.PopupPages.Border = Convert.ToInt32(numericBorder.Value);
            UpdateControlsFromNavigator();
        }

        private void numericGap_ValueChanged(object sender, EventArgs e)
        {
            kryptonNavigator.PopupPages.Gap = Convert.ToInt32(numericGap.Value);
            UpdateControlsFromNavigator();
        }

        private void comboBoxElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonNavigator.PopupPages.Element = (PopupPageElement)Enum.Parse(typeof(PopupPageElement), comboBoxElement.Text);
            UpdateControlsFromNavigator();
        }

        private void comboBoxPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            kryptonNavigator.PopupPages.Position = (PopupPagePosition)Enum.Parse(typeof(PopupPagePosition), comboBoxPosition.Text);
            UpdateControlsFromNavigator();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
