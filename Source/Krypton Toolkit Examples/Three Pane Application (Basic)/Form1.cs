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
using ComponentFactory.Krypton.Toolkit;

namespace ThreePaneApplication
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void kryptonOffice2010Blue_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonOffice2010Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        private void kryptonOffice2010Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonOffice2010Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
        }

        private void kryptonOffice2010Black_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonOffice2010Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }

        private void kryptonOffice2007Blue_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonOffice2007Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
        }

        private void kryptonOffice2007Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonOffice2007Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
        }

        private void kryptonOffice2007Black_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonOffice2007Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
        }

        private void kryptonOffice2003_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonOffice2003.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
        }

        private void kryptonSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonSystem.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void kryptonSparkleBlue_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonSparkleBlue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
        }

        private void kryptonSparkleOrange_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonSparkleOrange.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
        }

        private void kryptonSparklePurple_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonSparklePurple.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparklePurple;
        }

        private void kryptonCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonCustom.Checked)
                kryptonManager.GlobalPalette = kryptonPaletteCustom;
        }
    }
}
