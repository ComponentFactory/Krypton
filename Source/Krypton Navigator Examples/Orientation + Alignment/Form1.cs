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

namespace OrientationPlusAlignment
{
    public partial class Form1 : Form
    {
        private bool _updating;

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
            _updating = true;

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

            // Update Item Sizing
            radioSizingIndividual.Checked = (kryptonNavigator1.Bar.ItemSizing == BarItemSizing.Individual);
            radioSizingSameHeight.Checked = (kryptonNavigator1.Bar.ItemSizing == BarItemSizing.SameHeight);
            radioSizingSameWidth.Checked = (kryptonNavigator1.Bar.ItemSizing == BarItemSizing.SameWidth);
            radioSizingSameWidthHeight.Checked = (kryptonNavigator1.Bar.ItemSizing == BarItemSizing.SameWidthAndHeight);

            // Update sizing values
            numericUpDownBarFirstItemInset.Value = kryptonNavigator1.Bar.BarFirstItemInset;
            numericUpDownBarMinHeight.Value = kryptonNavigator1.Bar.BarMinimumHeight;
            numericUpDownMinItemSizeX.Value = kryptonNavigator1.Bar.ItemMinimumSize.Width;
            numericUpDownMinItemSizeY.Value = kryptonNavigator1.Bar.ItemMinimumSize.Height;
            numericUpDownMaxItemSizeX.Value = kryptonNavigator1.Bar.ItemMaximumSize.Width;
            numericUpDownMaxItemSizeY.Value = kryptonNavigator1.Bar.ItemMaximumSize.Height;

            // Set mode specific properties
            switch (kryptonNavigator1.NavigatorMode)
            {
                case NavigatorMode.BarRibbonTabGroup:
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

            _updating = false;
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

        private void radioSizingIndividual_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSizingIndividual.Checked)
            {
                kryptonNavigator1.Bar.ItemSizing = BarItemSizing.Individual;
                UpdateControlsFromNavigator();
            }
        }

        private void radioSizingSameHeight_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSizingSameHeight.Checked)
            {
                kryptonNavigator1.Bar.ItemSizing = BarItemSizing.SameHeight;
                UpdateControlsFromNavigator();
            }
        }

        private void radioSizingSameWidth_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSizingSameWidth.Checked)
            {
                kryptonNavigator1.Bar.ItemSizing = BarItemSizing.SameWidth;
                UpdateControlsFromNavigator();
            }
        }

        private void radioSizingSameWidthHeight_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSizingSameWidthHeight.Checked)
            {
                kryptonNavigator1.Bar.ItemSizing = BarItemSizing.SameWidthAndHeight;
                UpdateControlsFromNavigator();
            }
        }

        private void numericUpDownBarMinHeight_ValueChanged(object sender, EventArgs e)
        {
            if (!_updating)
                kryptonNavigator1.Bar.BarMinimumHeight = Convert.ToInt32(numericUpDownBarMinHeight.Value);
        }

        private void numericUpDownMinItemSize(object sender, EventArgs e)
        {
            if (!_updating)
                kryptonNavigator1.Bar.ItemMinimumSize = new Size(Convert.ToInt32(numericUpDownMinItemSizeX.Value),
                                                                 Convert.ToInt32(numericUpDownMinItemSizeY.Value));
        }

        private void numericUpDownMaxItemSize(object sender, EventArgs e)
        {
            if (!_updating)
                kryptonNavigator1.Bar.ItemMaximumSize = new Size(Convert.ToInt32(numericUpDownMaxItemSizeX.Value),
                                                                 Convert.ToInt32(numericUpDownMaxItemSizeY.Value));
        }

        private void numericUpDownBarFirstItemInset_ValueChanged(object sender, EventArgs e)
        {
            if (!_updating)
                kryptonNavigator1.Bar.BarFirstItemInset = Convert.ToInt32(numericUpDownBarFirstItemInset.Value);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
