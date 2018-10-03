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

namespace KryptonCheckedListBoxExamples
{
    public partial class Form1 : Form
    {
        private int _next = 1;
        private Random _rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = kryptonCheckedListBox;

            // Add some initial entries
            kryptonCheckedListBox.Items.Add(CreateNewItem());
            kryptonCheckedListBox.Items.Add(CreateNewItem());
            kryptonCheckedListBox.Items.Add(CreateNewItem());
            kryptonCheckedListBox.Items.Add(CreateNewItem());
            kryptonCheckedListBox.Items.Add(CreateNewItem());

            // Select the first entry
            kryptonCheckedListBox.SelectedIndex = 0;
        }

        private object CreateNewItem()
        {
            KryptonListItem item = new KryptonListItem();
            item.ShortText = "Item " + (_next++).ToString();
            item.LongText = "(" + _rand.Next(Int32.MaxValue).ToString() + ")";
            item.Image = imageList.Images[_rand.Next(imageList.Images.Count - 1)];
            return item;
        }

        private void kryptonListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonInsert.Enabled = (kryptonCheckedListBox.SelectedIndex >= 0);
            buttonRemove.Enabled = (kryptonCheckedListBox.SelectedIndex >= 0);
        }

        private void buttonAppend_Click(object sender, EventArgs e)
        {
            kryptonCheckedListBox.Items.Add(CreateNewItem());

            // If nothing currently selected, then select the new one
            if (kryptonCheckedListBox.SelectedIndex == -1)
                kryptonCheckedListBox.SelectedIndex = 0;
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            // Can only insert if something is already selected
            if (kryptonCheckedListBox.SelectedIndex >= 0)
                kryptonCheckedListBox.Items.Insert(kryptonCheckedListBox.SelectedIndex, CreateNewItem());
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            // Can only remove if something is selected
            if (kryptonCheckedListBox.SelectedIndex >= 0)
            {
                // Find the new index to select
                int index = kryptonCheckedListBox.SelectedIndex;
                if (index == (kryptonCheckedListBox.Items.Count - 1))
                    index--;

                // Remove entry
                kryptonCheckedListBox.Items.RemoveAt(kryptonCheckedListBox.SelectedIndex);

                // Select the new item
                if (index < kryptonCheckedListBox.Items.Count)
                    kryptonCheckedListBox.SelectedIndex = index;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            kryptonCheckedListBox.Items.Clear();
        }

        private void kryptonCheckSet_CheckedButtonChanged(object sender, EventArgs e)
        {
            if (kryptonCheckSet.CheckedButton == check2007Blue)
                kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
            else if (kryptonCheckSet.CheckedButton == check2010Blue)
                kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
            else if (kryptonCheckSet.CheckedButton == checkSparkle)
                kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
            else if (kryptonCheckSet.CheckedButton == checkSystem)
                kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
