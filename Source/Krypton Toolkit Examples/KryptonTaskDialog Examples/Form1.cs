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

namespace KryptonTaskDialogExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxIcon.Text = "Information";
            comboBoxFooterIcon.Text = "Warning";
        }

        private void palette2010Blue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        private void palette2010Silver_CheckedChanged(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
        }

        private void palette2010Black_CheckedChanged(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }

        private void palette2007Blue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
        }

        private void paletteSparkleOrange_CheckedChanged(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
        }

        private void paletteProfessional_CheckedChanged(object sender, EventArgs e)
        {
            kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void buttonShowTaskDialog_Click(object sender, EventArgs e)
        {
            TaskDialogButtons commonButtons = TaskDialogButtons.None;
            if (checkBoxOK.Checked) commonButtons |= TaskDialogButtons.OK;
            if (checkBoxYes.Checked) commonButtons |= TaskDialogButtons.Yes;
            if (checkBoxNo.Checked) commonButtons |= TaskDialogButtons.No;
            if (checkBoxCancel.Checked) commonButtons |= TaskDialogButtons.Cancel;
            if (checkBoxClose.Checked) commonButtons |= TaskDialogButtons.Close;
            if (checkBoxRetry.Checked) commonButtons |= TaskDialogButtons.Retry;

            kryptonTaskDialog.RadioButtons.Clear();
            if (checkBoxRadioButtons.Checked)
                kryptonTaskDialog.RadioButtons.AddRange(new KryptonTaskDialogCommand[] { kryptonTaskDialogCommand1, kryptonTaskDialogCommand2, kryptonTaskDialogCommand3 });

            kryptonTaskDialog.CommandButtons.Clear();
            if (checkBoxCommandButtons.Checked)
                kryptonTaskDialog.CommandButtons.AddRange(new KryptonTaskDialogCommand[] { kryptonTaskDialogCommand4, kryptonTaskDialogCommand5, kryptonTaskDialogCommand6 });

            kryptonTaskDialog.WindowTitle = textBoxCaption.Text;
            kryptonTaskDialog.MainInstruction = textBoxMainInstructions.Text;
            kryptonTaskDialog.Content = textBoxContent.Text;
            kryptonTaskDialog.Icon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), comboBoxIcon.Text);
            kryptonTaskDialog.CommonButtons = commonButtons;
            kryptonTaskDialog.CheckboxText = textBoxCheckBoxText.Text;
            kryptonTaskDialog.CheckboxState = checkBoxInitialState.Checked;
            kryptonTaskDialog.FooterText = textBoxFooterText.Text;
            kryptonTaskDialog.FooterHyperlink = textBoxFooterHyperlink.Text;
            kryptonTaskDialog.FooterIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), comboBoxFooterIcon.Text);
            kryptonTaskDialog.ShowDialog(this);
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            var s  = "The quick brown fox jumps over the lazy dog";
            var e1 = new Random().Next(50, 400);
            var sb = new StringBuilder().Append(s);
            for (var i = 0; i < e1; ++i)
            {
                sb.AppendFormat("\r\n\r\n{0}", s);
                if (i % 10 == 0)
                {
                    var e2 = new Random().Next(1, 10);
                    for (var j=0; j < e2; ++j)
                      sb.AppendFormat(". {0}", s);
                    sb.Append("\r\n");
                }
            }
            textBoxContent.Text = sb.ToString();
        }
    }
}
