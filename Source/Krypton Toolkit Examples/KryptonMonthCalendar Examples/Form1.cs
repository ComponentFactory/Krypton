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

namespace KryptonMonthCalendarExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void rbOffice2010Blue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2010Blue;
        }

        private void rbOffice2010Silver_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2010Silver;
        }

        private void rbOffice2010Black_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2010Black;
        }

        private void rbOffice2007Blue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2007Blue;
        }

        private void rbOffice2007Silver_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2007Silver;
        }

        private void rbOffice2007Black_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.Office2007Black;
        }

        private void rbSparkleBlue_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.SparkleBlue;
        }

        private void rbSparkleOrange_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.SparkleOrange;
        }

        private void rbSparklePurple_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.SparklePurple;
        }
        
        private void rbOffice2003_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.ProfessionalOffice2003;
        }

        private void rbSystem_CheckedChanged(object sender, EventArgs e)
        {
            kryptonPalette.BasePaletteMode = PaletteMode.ProfessionalSystem;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
