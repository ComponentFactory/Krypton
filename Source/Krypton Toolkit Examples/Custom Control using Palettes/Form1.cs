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

namespace CustomControlUsingPalettes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void kryptonCheckSet_CheckedButtonChanged(object sender, EventArgs e)
        {
            // Switch to using a different global palette
            switch (kryptonCheckSet.CheckedIndex)
            {
                case 0:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                    break;
                case 1:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
                    break;
                case 2:
                    kryptonManager.GlobalPalette = kryptonPaletteCustom;
                    break;
                case 3:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                    break;
                case 4:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
            }
        }

        private void checkBoxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle the enabled state of the custom control instance
            myUserControl1.Enabled = !myUserControl1.Enabled;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
