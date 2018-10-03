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
using ComponentFactory.Krypton.Ribbon;
using ComponentFactory.Krypton.Toolkit;

namespace KeyTipsAndKeyboardAccess
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkSetPalette.CheckedButton = this.buttonOffice2007Black;
        }

        private void checkSetPalette_CheckedButtonChanged(object sender, EventArgs e)
        {
            if (checkSetPalette.CheckedButton == buttonOffice2007Blue)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
            else if (checkSetPalette.CheckedButton == buttonOffice2007Silver)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
            else if (checkSetPalette.CheckedButton == buttonOffice2007Black)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
            else if (checkSetPalette.CheckedButton == buttonProfessional2003)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
            else if (checkSetPalette.CheckedButton == buttonProfessionalSystem)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
            else if (checkSetPalette.CheckedButton == buttonSparkleBlue)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
            else if (checkSetPalette.CheckedButton == buttonSparkleOrange)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
            else if (checkSetPalette.CheckedButton == buttonSparklePurple)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparklePurple;
            else if (checkSetPalette.CheckedButton == buttonOffice2010Blue)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
            else if (checkSetPalette.CheckedButton == buttonOffice2010Silver)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
            else if (checkSetPalette.CheckedButton == buttonOffice2010Black)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }

        private void OnDialogBoxLauncherClick(object sender, EventArgs e)
        {
            KryptonRibbonGroup group = (KryptonRibbonGroup)sender;

            MessageBox.Show(this,
                            "You selected the '" + group.TextLine1 + "' group dialog box launcher.",
                            "Dialog Box Launcher");
        }

        private void OnQATButtonClick(object sender, EventArgs e)
        {
            KryptonRibbonQATButton button = (KryptonRibbonQATButton)sender;

            MessageBox.Show(this,
                            "You selected the '" + button.Text + "' quick access toolbar button.",
                            "QAT Button");
        }

        private void OnGroupButtonClick(object sender, EventArgs e)
        {
            KryptonRibbonGroupButton button = (KryptonRibbonGroupButton)sender;

            string text = button.TextLine1;
            if (!string.IsNullOrEmpty(button.TextLine2))
                text += " " + button.TextLine2;

            MessageBox.Show(this,
                            "You selected the '" + text + "' button.",
                            "Button");
        }

        private void OnGroupClusterButtonClick(object sender, EventArgs e)
        {
            KryptonRibbonGroupClusterButton button = (KryptonRibbonGroupClusterButton)sender;

            MessageBox.Show(this,
                            "You selected the '" + button.TextLine + "' button.",
                            "Button");
        }

        private void appMenu_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
