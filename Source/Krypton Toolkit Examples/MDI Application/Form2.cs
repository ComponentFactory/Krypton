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

namespace MDIApplication
{
    public partial class Form2 : KryptonForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remember to unhook from static event, otherwise 
                // this object cannot be garbage collected later on
                KryptonManager.GlobalPaletteChanged -= new EventHandler(OnPaletteChanged);

                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Set correct initial radio button setting
            UpdateRadioButtons();

            // Hook into changes in the global palette
            KryptonManager.GlobalPaletteChanged += new EventHandler(OnPaletteChanged);
        }

        private void radio2010Blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radio2010Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        private void radio2010Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (radio2010Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
        }

        private void radio2010Black_CheckedChanged(object sender, EventArgs e)
        {
            if (radio2010Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }

        private void radio2007Blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radio2007Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
        }

        private void radio2007Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (radio2007Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
        }

        private void radio2007Black_CheckedChanged(object sender, EventArgs e)
        {
            if (radio2007Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
        }

        private void radioSparkleBlue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSparkleBlue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
        }

        private void radioSparkleOrange_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSparkleOrange.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
        }

        private void radioSparklePurple_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSparklePurple.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparklePurple;
        }

        private void radioOffice2003_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2003.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
        }

        private void radioSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSystem.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void OnPaletteChanged(object sender, EventArgs e)
        {
            // Update buttons to reflect the new palette setting
            UpdateRadioButtons();
        }

        private void UpdateRadioButtons()
        {
            switch (kryptonManager.GlobalPaletteMode)
            {
                case PaletteModeManager.Office2010Blue:
                    radio2010Blue.Checked = true;
                    break;
                case PaletteModeManager.Office2010Silver:
                    radio2010Silver.Checked = true;
                    break;
                case PaletteModeManager.Office2010Black:
                    radio2010Black.Checked = true;
                    break;
                case PaletteModeManager.Office2007Blue:
                    radio2007Blue.Checked = true;
                    break;
                case PaletteModeManager.Office2007Silver:
                    radio2007Silver.Checked = true;
                    break;
                case PaletteModeManager.Office2007Black:
                    radio2007Black.Checked = true;
                    break;
                case PaletteModeManager.SparkleBlue:
                    radioSparkleBlue.Checked = true;
                    break;
                case PaletteModeManager.SparkleOrange:
                    radioSparkleOrange.Checked = true;
                    break;
                case PaletteModeManager.SparklePurple:
                    radioSparklePurple.Checked = true;
                    break;
                case PaletteModeManager.ProfessionalOffice2003:
                    radioOffice2003.Checked = true;
                    break;
                case PaletteModeManager.ProfessionalSystem:
                    radioSystem.Checked = true;
                    break;
            }
        }
    }
}
