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

namespace SinglelinePlusMultiline
{
    public partial class Form1 : Form
    {
        int _newPage = 7;

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
            // We only show the next/prev buttons when in singleline/expandline modes
            if ((kryptonNavigator1.Bar.BarMultiline == BarMultiline.Singleline) ||
                (kryptonNavigator1.Bar.BarMultiline == BarMultiline.Expandline))
                kryptonNavigator1.Button.ButtonDisplayLogic = ButtonDisplayLogic.NextPrevious;
            else
                kryptonNavigator1.Button.ButtonDisplayLogic = ButtonDisplayLogic.Context;

            // Update BarMultiline setting
            radioSingleline.Checked = (kryptonNavigator1.Bar.BarMultiline == BarMultiline.Singleline);
            radioMultiline.Checked = (kryptonNavigator1.Bar.BarMultiline == BarMultiline.Multiline);
            radioExactline.Checked = (kryptonNavigator1.Bar.BarMultiline == BarMultiline.Exactline);
            radioShrinkline.Checked = (kryptonNavigator1.Bar.BarMultiline == BarMultiline.Shrinkline);
            radioExpandline.Checked = (kryptonNavigator1.Bar.BarMultiline == BarMultiline.Expandline);

            // Update Mode
            radioModeTabs.Checked = (kryptonNavigator1.NavigatorMode == NavigatorMode.BarTabGroup);
            radioModeRibbonTabs.Checked = (kryptonNavigator1.NavigatorMode == NavigatorMode.BarRibbonTabGroup);
            radioModesCheckButton.Checked = (kryptonNavigator1.NavigatorMode == NavigatorMode.BarCheckButtonGroupOutside);

            // Update Bar Orientation
            radioOrientationTop.Checked = (kryptonNavigator1.Bar.BarOrientation == VisualOrientation.Top);
            radioOrientationBottom.Checked = (kryptonNavigator1.Bar.BarOrientation == VisualOrientation.Bottom);
            radioOrientationLeft.Checked = (kryptonNavigator1.Bar.BarOrientation == VisualOrientation.Left);
            radioOrientationRight.Checked = (kryptonNavigator1.Bar.BarOrientation == VisualOrientation.Right);

            // Update Item Orientation
            radioItemAuto.Checked = (kryptonNavigator1.Bar.ItemOrientation == ButtonOrientation.Auto);
            radioItemFixedTop.Checked = (kryptonNavigator1.Bar.ItemOrientation == ButtonOrientation.FixedTop);
            radioItemFixedBottom.Checked = (kryptonNavigator1.Bar.ItemOrientation == ButtonOrientation.FixedBottom);
            radioItemFixedLeft.Checked = (kryptonNavigator1.Bar.ItemOrientation == ButtonOrientation.FixedLeft);
            radioItemFixedRight.Checked = (kryptonNavigator1.Bar.ItemOrientation == ButtonOrientation.FixedRight);

            // Update Item Alignment
            radioItemNear.Checked = (kryptonNavigator1.Bar.ItemAlignment == RelativePositionAlign.Near);
            radioItemCenter.Checked = (kryptonNavigator1.Bar.ItemAlignment == RelativePositionAlign.Center);
            radioItemFar.Checked = (kryptonNavigator1.Bar.ItemAlignment == RelativePositionAlign.Far);

            // Set mode specific properties
            switch (kryptonNavigator1.NavigatorMode)
            {
                case NavigatorMode.BarRibbonTabGroup:
                case NavigatorMode.BarRibbonTabOnly:
                    kryptonNavigator1.PageBackStyle = PaletteBackStyle.ControlRibbon;
                    kryptonNavigator1.Group.GroupBackStyle = PaletteBackStyle.ControlRibbon;
                    kryptonNavigator1.Group.GroupBorderStyle = PaletteBorderStyle.ControlRibbon;
                    break;
                default:
                    kryptonNavigator1.PageBackStyle = PaletteBackStyle.ControlClient;
                    kryptonNavigator1.Group.GroupBackStyle = PaletteBackStyle.ControlClient;
                    kryptonNavigator1.Group.GroupBorderStyle = PaletteBorderStyle.ControlClient;
                    break;
            }
        }

        private void radioSingleline_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSingleline.Checked)
            {
                kryptonNavigator1.Bar.BarMultiline = BarMultiline.Singleline;
                UpdateControlsFromNavigator();
            }
        }

        private void radioMultiline_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMultiline.Checked)
            {
                kryptonNavigator1.Bar.BarMultiline = BarMultiline.Multiline;
                UpdateControlsFromNavigator();
            }
        }

        private void radioExactline_CheckedChanged(object sender, EventArgs e)
        {
            if (radioExactline.Checked)
            {
                kryptonNavigator1.Bar.BarMultiline = BarMultiline.Exactline;
                UpdateControlsFromNavigator();
            }
        }

        private void radioShrinkline_CheckedChanged(object sender, EventArgs e)
        {
            if (radioShrinkline.Checked)
            {
                kryptonNavigator1.Bar.BarMultiline = BarMultiline.Shrinkline;
                UpdateControlsFromNavigator();
            }
        }

        private void radioExpandline_CheckedChanged(object sender, EventArgs e)
        {
            if (radioExpandline.Checked)
            {
                kryptonNavigator1.Bar.BarMultiline = BarMultiline.Expandline;
                UpdateControlsFromNavigator();
            }
        }

        private void radioModeTabs_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModeTabs.Checked)
            {
                kryptonNavigator1.NavigatorMode = NavigatorMode.BarTabGroup;
                UpdateControlsFromNavigator();
            }
        }

        private void radioModeRibbonTabs_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModeRibbonTabs.Checked)
            {
                kryptonNavigator1.NavigatorMode = NavigatorMode.BarRibbonTabGroup;
                UpdateControlsFromNavigator();
            }
        }

        private void radioModesCheckButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModesCheckButton.Checked)
            {
                kryptonNavigator1.NavigatorMode = NavigatorMode.BarCheckButtonGroupOutside;
                UpdateControlsFromNavigator();
            }
        }

        private void radioOrientationTop_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOrientationTop.Checked)
            {
                kryptonNavigator1.Bar.BarOrientation = VisualOrientation.Top;
                UpdateControlsFromNavigator();
            }
        }

        private void radioOrientationBottom_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOrientationBottom.Checked)
            {
                kryptonNavigator1.Bar.BarOrientation = VisualOrientation.Bottom;
                UpdateControlsFromNavigator();
            }
        }

        private void radioOrientationLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOrientationLeft.Checked)
            {
                kryptonNavigator1.Bar.BarOrientation = VisualOrientation.Left;
                UpdateControlsFromNavigator();
            }
        }

        private void radioOrientationRight_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOrientationRight.Checked)
            {
                kryptonNavigator1.Bar.BarOrientation = VisualOrientation.Right;
                UpdateControlsFromNavigator();
            }
        }

        private void radioItemAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (radioItemAuto.Checked)
            {
                kryptonNavigator1.Bar.ItemOrientation = ButtonOrientation.Auto;
                UpdateControlsFromNavigator();
            }
        }

        private void radioItemFixedTop_CheckedChanged(object sender, EventArgs e)
        {
            if (radioItemFixedTop.Checked)
            {
                kryptonNavigator1.Bar.ItemOrientation = ButtonOrientation.FixedTop;
                UpdateControlsFromNavigator();
            }
        }

        private void radioItemFixedBottom_CheckedChanged(object sender, EventArgs e)
        {
            if (radioItemFixedBottom.Checked)
            {
                kryptonNavigator1.Bar.ItemOrientation = ButtonOrientation.FixedBottom;
                UpdateControlsFromNavigator();
            }
        }

        private void radioItemFixedLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (radioItemFixedLeft.Checked)
            {
                kryptonNavigator1.Bar.ItemOrientation = ButtonOrientation.FixedLeft;
                UpdateControlsFromNavigator();
            }
        }

        private void radioItemFixedRight_CheckedChanged(object sender, EventArgs e)
        {
            if (radioItemFixedRight.Checked)
            {
                kryptonNavigator1.Bar.ItemOrientation = ButtonOrientation.FixedRight;
                UpdateControlsFromNavigator();
            }
        }

        private void radioItemNear_CheckedChanged(object sender, EventArgs e)
        {
            if (radioItemNear.Checked)
            {
                kryptonNavigator1.Bar.ItemAlignment = RelativePositionAlign.Near;
                UpdateControlsFromNavigator();
            }
        }

        private void radioItemCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (radioItemCenter.Checked)
            {
                kryptonNavigator1.Bar.ItemAlignment = RelativePositionAlign.Center;
                UpdateControlsFromNavigator();
            }
        }

        private void radioItemFar_CheckedChanged(object sender, EventArgs e)
        {
            if (radioItemFar.Checked)
            {
                kryptonNavigator1.Bar.ItemAlignment = RelativePositionAlign.Far;
                UpdateControlsFromNavigator();
            }
        }

        private void buttonAddPage_Click(object sender, EventArgs e)
        {
            KryptonPage newPage = new KryptonPage();
            newPage.Text = "Page " + _newPage.ToString();
            newPage.ImageSmall = imageList1.Images[_newPage++ % imageList1.Images.Count];
            kryptonNavigator1.Pages.Add(newPage);
        }

        private void buttonClearAllPages_Click(object sender, EventArgs e)
        {
            kryptonNavigator1.Pages.Clear();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
