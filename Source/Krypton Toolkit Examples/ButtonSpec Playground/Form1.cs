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

namespace ButtonSpecPlayground
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void kryptonButtonAdd_Click(object sender, EventArgs e)
        {
            // Create a new button spec entry
            ButtonSpecHeaderGroup spec = new ButtonSpecHeaderGroup();
            spec.Type = PaletteButtonSpecStyle.Close;

            // Need to know when button is selected
            spec.Click += new EventHandler(OnButtonSelected);
            
            // Add to end of the collection of button specs
            kryptonHeaderGroup1.ButtonSpecs.Add(spec);

            // Make it the selected button spec
            propertyGrid.SelectedObject = spec;

            UpdateActionButtons();
        }

        private void kryptonButtonRemove_Click(object sender, EventArgs e)
        {
            // Get access to the selected button spec
            ButtonSpecHeaderGroup spec = (ButtonSpecHeaderGroup)propertyGrid.SelectedObject;

            // Remove just the selected button spec
            kryptonHeaderGroup1.ButtonSpecs.Remove(spec);

            // Nothing selected in the property grid
            propertyGrid.SelectedObject = null;

            UpdateActionButtons();
        }

        private void kryptonButtonClear_Click(object sender, EventArgs e)
        {
            // Remove all the button specifications
            kryptonHeaderGroup1.ButtonSpecs.Clear();

            // Nothing selected in the property grid
            propertyGrid.SelectedObject = null;

            UpdateActionButtons();
        }

        private void kryptonButtonTopP_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.HeaderPositionPrimary = VisualOrientation.Top;
        }

        private void kryptonButtonLeftP_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.HeaderPositionPrimary = VisualOrientation.Left;
        }

        private void kryptonButtonRightP_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.HeaderPositionPrimary = VisualOrientation.Right;
        }

        private void kryptonButtonBottomP_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.HeaderPositionPrimary = VisualOrientation.Bottom;
        }

        private void kryptonButtonTopS_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.HeaderPositionSecondary = VisualOrientation.Top;
        }

        private void kryptonButtonLeftS_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.HeaderPositionSecondary = VisualOrientation.Left;
        }

        private void kryptonButtonRightS_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.HeaderPositionSecondary = VisualOrientation.Right;
        }

        private void kryptonButtonBottomS_Click(object sender, EventArgs e)
        {
            kryptonHeaderGroup1.HeaderPositionSecondary = VisualOrientation.Bottom;
        }

        private void OnButtonSelected(object sender, EventArgs e)
        {
            // Cast to correct type
            ButtonSpecHeaderGroup spec = (ButtonSpecHeaderGroup)sender;

            // Make it the selected button spec
            propertyGrid.SelectedObject = spec;

            UpdateActionButtons();
        }
        
        private void UpdateActionButtons()
        {
            kryptonButtonRemove.Enabled = (propertyGrid.SelectedObject != null);
            kryptonButtonClear.Enabled = (kryptonHeaderGroup1.ButtonSpecs.Count > 0);
        }
    }
}
