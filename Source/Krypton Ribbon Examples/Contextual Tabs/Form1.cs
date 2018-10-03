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

namespace ContextualTabs
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSelectedApply_Click(object sender, EventArgs e)
        {
            // Update ribbon with new context value
            kryptonRibbon.SelectedContext = textBoxSelectedContexts.Text;
        }

        private void textBoxSelectedContexts_KeyDown(object sender, KeyEventArgs e)
        {
            // Pressing enter in text box is same as pressing the apply button
            if (e.KeyCode == Keys.Enter)
                buttonSelectedApply_Click(buttonSelectedApply, EventArgs.Empty);
        }

        private void buttonEditColor_Click(object sender, EventArgs e)
        {
            // Let user change the color definition
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Update the displayed color feedback
                panelContextColor.StateCommon.Color1 = colorDialog.Color;
            }
        }

        private void buttonAddContext_Click(object sender, EventArgs e)
        {
            // Create a new context that uses the information specified
            KryptonRibbonContext newContext = new KryptonRibbonContext();
            newContext.ContextName = textBoxContextName.Text;
            newContext.ContextTitle = textBoxContextTitle.Text;
            newContext.ContextColor = panelContextColor.StateCommon.Color1;
            kryptonRibbon.RibbonContexts.Add(newContext);

            // Create a new ribbon page that specifies the new context name
            KryptonRibbonTab newTab = new KryptonRibbonTab();
            newTab.ContextName = newContext.ContextName;
            kryptonRibbon.RibbonTabs.Add(newTab);

            // Update the selected context name on the form and control so it shows
            string newSelectedContext = textBoxSelectedContexts.Text;
            if (newSelectedContext.Length > 0)
                newSelectedContext += ",";
            newSelectedContext += newContext.ContextName;
            textBoxSelectedContexts.Text = newSelectedContext;
            kryptonRibbon.SelectedContext = newSelectedContext;
        }

        private void radioOffice2010Blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2010Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        private void radioOffice2010Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2010Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
        }

        private void radioOffice2010Black_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2010Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }

        private void radioOffice2007Blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2007Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
        }

        private void radioOffice2007Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2007Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
        }

        private void radioOffice2007Black_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2007Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
        }

        private void radioOffice2003_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2003.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
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

        private void radioSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSystem.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void appMenu_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
