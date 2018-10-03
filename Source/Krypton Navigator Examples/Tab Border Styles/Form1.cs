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

namespace TabBorderStyles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tabBorderStyles_CheckedChanged(object sender, EventArgs e)
        {
            // Cast to correct type
            RadioButton rb = (RadioButton)sender;

            if (rb.Checked)
            {
                TabBorderStyle enumVal = (TabBorderStyle)Enum.Parse(typeof(TabBorderStyle), rb.Tag.ToString());
                kryptonNavigator.Bar.TabBorderStyle = enumVal;
            }
        }

        private void tabStyles_CheckedChanged(object sender, EventArgs e)
        {
            // Cast to correct type
            RadioButton rb = (RadioButton)sender;

            if (rb.Checked)
            {
                TabStyle enumVal = (TabStyle)Enum.Parse(typeof(TabStyle), rb.Tag.ToString());
                kryptonNavigator.Bar.TabStyle = enumVal;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
