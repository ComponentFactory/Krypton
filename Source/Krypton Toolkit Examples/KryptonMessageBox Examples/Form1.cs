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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace KryptonMessageBoxExamples
{
    public partial class Form1 : Form
    {
        private MessageBoxIcon _mbIcon = MessageBoxIcon.Warning;
        private MessageBoxButtons _mbButtons = MessageBoxButtons.OKCancel;

        public Form1()
        {
            InitializeComponent();
        }

        private void palette_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOffice2010Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
            else if (radioButtonOffice2010Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
            else if (radioButtonOffice2010Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
            else if (radioButtonOffice2007Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
            else if (radioButtonOffice2007Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
            else if (radioButtonOffice2007Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
            else if (radioButtonSparkleBlue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
            else if (radioButtonSparkleOrange.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
            else if (radioButtonSparklePurple.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparklePurple;
            else if (radioButtonOffice2003.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
            else if (radioButtonSystem.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void icon_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNone.Checked)
                _mbIcon = MessageBoxIcon.None;
            else if (radioButtonError.Checked)
                _mbIcon = MessageBoxIcon.Error;
            else if (radioButtonQuestion.Checked)
                _mbIcon = MessageBoxIcon.Question;
            else if (radioButtonWarning.Checked)
                _mbIcon = MessageBoxIcon.Warning;
            else if (radioButtonInformation.Checked)
                _mbIcon = MessageBoxIcon.Information;
        }

        private void buttons_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOK.Checked)
                _mbButtons = MessageBoxButtons.OK;
            else if (radioButtonOKCancel.Checked)
                _mbButtons = MessageBoxButtons.OKCancel;
            else if (radioButtonRetryCancel.Checked)
                _mbButtons = MessageBoxButtons.RetryCancel;
            else if (radioButtonAbortRetryIgnore.Checked)
                _mbButtons = MessageBoxButtons.AbortRetryIgnore;
            else if (radioButtonYesNo.Checked)
                _mbButtons = MessageBoxButtons.YesNo;
            else if (radioButtonYesNoCancel.Checked)
                _mbButtons = MessageBoxButtons.YesNoCancel;
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            KryptonMessageBox.Show(textBoxMessage.Text, textBoxCaption.Text, _mbButtons, _mbIcon);
        }
    }
}
