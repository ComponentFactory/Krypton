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

namespace ApplicationMenu
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBoxShowRecentDocs.Checked = kryptonRibbon1.RibbonAppButton.AppButtonShowRecentDocs;
            textBoxDocsTitle.Text = kryptonRibbon1.RibbonStrings.RecentDocuments;
            textBoxMinWidth.Text = kryptonRibbon1.RibbonAppButton.AppButtonMinRecentSize.Width.ToString();
            textBoxMinHeight.Text = kryptonRibbon1.RibbonAppButton.AppButtonMinRecentSize.Height.ToString();
        }

        private void kryptonRibbon1_AppButtonMenuOpening(object sender, CancelEventArgs e)
        {
            kryptonRibbon1.RibbonAppButton.AppButtonShowRecentDocs = checkBoxShowRecentDocs.Checked;
            kryptonRibbon1.RibbonStrings.RecentDocuments = textBoxDocsTitle.Text;

            int minWidth = int.Parse(textBoxMinWidth.Text);
            int minHeight = int.Parse(textBoxMinHeight.Text);
            kryptonRibbon1.RibbonAppButton.AppButtonMinRecentSize = new Size(minWidth, minHeight);
        }

        private void button2010Blue_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = true;
            button2010Silver.Checked = false;
            button2010Black.Checked = false;
            buttonBlue.Checked = false;
            buttonSilver.Checked = false;
            buttonBlack.Checked = false;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        private void button2010Silver_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = false;
            button2010Silver.Checked = true;
            button2010Black.Checked = false;
            buttonBlue.Checked = false;
            buttonSilver.Checked = false;
            buttonBlack.Checked = false;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
        }

        private void button2010Black_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = false;
            button2010Silver.Checked = false;
            button2010Black.Checked = true;
            buttonBlue.Checked = false;
            buttonSilver.Checked = false;
            buttonBlack.Checked = false;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }


        private void buttonBlue_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = false;
            button2010Silver.Checked = false;
            button2010Black.Checked = false;
            buttonBlue.Checked = true;
            buttonSilver.Checked = false;
            buttonBlack.Checked = false;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
        }

        private void buttonSilver_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = false;
            button2010Silver.Checked = false;
            button2010Black.Checked = false;
            buttonBlue.Checked = false;
            buttonSilver.Checked = true;
            buttonBlack.Checked = false;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
        }

        private void buttonBlack_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = false;
            button2010Silver.Checked = false;
            button2010Black.Checked = false;
            buttonBlue.Checked = false;
            buttonSilver.Checked = false;
            buttonBlack.Checked = true;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Black;
        }

        private void button2003_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = false;
            button2010Silver.Checked = false;
            button2010Black.Checked = false;
            buttonBlue.Checked = false;
            buttonSilver.Checked = false;
            buttonBlack.Checked = false;
            button2003.Checked = true;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
        }

        private void buttonSparkleBlue_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = false;
            button2010Silver.Checked = false;
            button2010Black.Checked = false;
            buttonBlue.Checked = false;
            buttonSilver.Checked = false;
            buttonBlack.Checked = false;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = true;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
        }

        private void buttonSparkleOrange_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = false;
            button2010Silver.Checked = false;
            button2010Black.Checked = false;
            buttonBlue.Checked = false;
            buttonSilver.Checked = false;
            buttonBlack.Checked = false;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = true;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
        }

        private void buttonSparklePurple_Click(object sender, EventArgs e)
        {
            button2010Blue.Checked = false;
            button2010Silver.Checked = false;
            button2010Black.Checked = false;
            buttonBlue.Checked = false;
            buttonSilver.Checked = false;
            buttonBlack.Checked = false;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = true;
            buttonSystem.Checked = false;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparklePurple;
        }

        private void buttonSystem_Click(object sender, EventArgs e)
        {
            buttonBlue.Checked = false;
            buttonSilver.Checked = false;
            buttonBlack.Checked = false;
            button2003.Checked = false;
            buttonSparkleBlue.Checked = false;
            buttonSparkleOrange.Checked = false;
            buttonSparklePurple.Checked = false;
            buttonSystem.Checked = true;
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }
    }
}
