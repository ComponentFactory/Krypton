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

namespace KryptonPaletteExamples
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2010Blue;
            propertyGrid.SelectedObject = kryptonPaletteOffice2010Blue;
        }

        private void buttonOffice2010Blue_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2010Blue;
            propertyGrid.SelectedObject = kryptonPaletteOffice2010Blue;
        }

        private void buttonOffice2010Silver_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2010Silver;
            propertyGrid.SelectedObject = kryptonPaletteOffice2010Silver;
        }

        private void buttonOffice2010Black_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2010Black;
            propertyGrid.SelectedObject = kryptonPaletteOffice2010Black;
        }

        private void buttonOffice2007Blue_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2007Blue;
            propertyGrid.SelectedObject = kryptonPaletteOffice2007Blue;
        }

        private void buttonOffice2007Silver_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2007Silver;
            propertyGrid.SelectedObject = kryptonPaletteOffice2007Silver;
        }

        private void buttonOffice2007Black_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2007Black;
            propertyGrid.SelectedObject = kryptonPaletteOffice2007Black;
        }

        private void buttonOffice2003_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2003;
            propertyGrid.SelectedObject = kryptonPaletteOffice2003;
        }

        private void buttonSystem_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSystem;
            propertyGrid.SelectedObject = kryptonPaletteSystem;
        }

        private void buttonSparkleBlue_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSparkleBlue;
            propertyGrid.SelectedObject = kryptonPaletteSparkleBlue;
        }

        private void buttonSparkleOrange_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSparkleOrange;
            propertyGrid.SelectedObject = kryptonPaletteSparkleOrange;
        }

        private void buttonSparklePurple_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSparklePurple;
            propertyGrid.SelectedObject = kryptonPaletteSparklePurple;
        }

        private void buttonCustom_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteCustom;
            propertyGrid.SelectedObject = kryptonPaletteCustom;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
