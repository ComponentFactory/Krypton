﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.5.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace KryptonWrapLabelExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = kryptonWrapLabel;
        }

        private void buttonNormal_Click(object sender, EventArgs e)
        {
            kryptonWrapLabel.LabelStyle = LabelStyle.NormalControl;
            propertyGrid.SelectedObject = kryptonWrapLabel;
        }

        private void buttonTitle_Click(object sender, EventArgs e)
        {
            kryptonWrapLabel.LabelStyle = LabelStyle.TitleControl;
            propertyGrid.SelectedObject = kryptonWrapLabel;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
