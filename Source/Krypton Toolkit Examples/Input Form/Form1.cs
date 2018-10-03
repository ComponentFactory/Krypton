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

namespace InputForm
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void office2010_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
            toolStripOffice2010.Checked = true;
            toolStripOffice2007.Checked = false;
            toolStripSystem.Checked = false;
            toolStripSparkle.Checked = false;
            office2010MenuItem.Checked = true;
            office2007MenuItem.Checked = false;
            systemMenuItem.Checked = false;
            sparkleMenuItem.Checked = false;
        }

        private void office2007_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
            toolStripOffice2010.Checked = false;
            toolStripOffice2007.Checked = true;
            toolStripSystem.Checked = false;
            toolStripSparkle.Checked = false;
            office2010MenuItem.Checked = false;
            office2007MenuItem.Checked = true;
            systemMenuItem.Checked = false;
            sparkleMenuItem.Checked = false;
        }

        private void sparkle_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
            toolStripOffice2010.Checked = false;
            toolStripOffice2007.Checked = false;
            toolStripSystem.Checked = false;
            toolStripSparkle.Checked = true;
            office2010MenuItem.Checked = false;
            office2007MenuItem.Checked = false;
            systemMenuItem.Checked = false;
            sparkleMenuItem.Checked = true;
        }

        private void system_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
            toolStripOffice2010.Checked = false;
            toolStripOffice2007.Checked = false;
            toolStripSystem.Checked = true;
            toolStripSparkle.Checked = false;
            office2010MenuItem.Checked = false;
            office2007MenuItem.Checked = false;
            systemMenuItem.Checked = true;
            sparkleMenuItem.Checked = false;
        }

        private void clearTelephone_Click(object sender, EventArgs e)
        {
            maskedTextBoxTelephone.Text = string.Empty;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
