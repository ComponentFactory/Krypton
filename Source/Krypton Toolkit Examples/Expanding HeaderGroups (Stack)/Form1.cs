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

namespace ExpandingHeaderGroupsStack
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void kryptonHeaderTop_CollapsedChanged(object sender, EventArgs e)
        {
            kryptonBorderEdgeTop.Visible = !kryptonHeaderTop.Collapsed;
        }

        private void kryptonHeaderBottom_CollapsedChanged(object sender, EventArgs e)
        {
            kryptonBorderEdgeBottom.Visible = !kryptonHeaderBottom.Collapsed;
        }

        private void toolOffice2010_Click(object sender, EventArgs e)
        {
            if (!toolOffice2010.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                toolOffice2010.Checked = menuOffice2010.Checked = true;
                toolOffice2007.Checked = menuOffice2007.Checked = false;
                toolSparkle.Checked = menuSparkle.Checked = false;
                toolSystem.Checked = menuSystem.Checked = false;
            }
        }

        private void toolOffice2007_Click(object sender, EventArgs e)
        {
            if (!toolOffice2007.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                toolOffice2010.Checked = menuOffice2010.Checked = false;
                toolOffice2007.Checked = menuOffice2007.Checked = true;
                toolSparkle.Checked = menuSparkle.Checked = false;
                toolSystem.Checked = menuSystem.Checked = false;
            }
        }

        private void toolSparkle_Click(object sender, EventArgs e)
        {
            if (!toolSparkle.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                toolOffice2010.Checked = menuOffice2010.Checked = false;
                toolOffice2007.Checked = menuOffice2007.Checked = false;
                toolSparkle.Checked = menuSparkle.Checked = true;
                toolSystem.Checked = menuSystem.Checked = false;
            }
        }

        private void toolSystem_Click(object sender, EventArgs e)
        {
            if (!toolSystem.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
                toolOffice2010.Checked = menuOffice2010.Checked = false;
                toolOffice2007.Checked = menuOffice2007.Checked = false;
                toolSparkle.Checked = menuSparkle.Checked = false;
                toolSystem.Checked = menuSystem.Checked = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
