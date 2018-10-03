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

namespace KryptonGalleryExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioSmallList_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSmallList.Checked)
                kryptonGallery1.ImageList = imageListSmall;
        }

        private void radioMediumList_CheckedChanged(object sender, EventArgs e)
        {
            if (radioMediumList.Checked)
                kryptonGallery1.ImageList = imageListMedium;
        }

        private void radioLargeList_CheckedChanged(object sender, EventArgs e)
        {
            if (radioLargeList.Checked)
                kryptonGallery1.ImageList = imageListLarge;
        }

        private void numericWidth_ValueChanged(object sender, EventArgs e)
        {
            kryptonGallery1.PreferredItemSize = new Size(Convert.ToInt32(numericWidth.Value), kryptonGallery1.PreferredItemSize.Height);
        }

        private void numericHeight_ValueChanged(object sender, EventArgs e)
        {
            kryptonGallery1.PreferredItemSize = new Size(kryptonGallery1.PreferredItemSize.Width, Convert.ToInt32(numericHeight.Value));
        }

        private void checkBoxGroupImages_CheckedChanged(object sender, EventArgs e)
        {
            kryptonGallery1.DropButtonRanges.Clear();
            if (checkBoxGroupImages.Checked)
            {
                kryptonGallery1.DropButtonRanges.Add(kryptonGalleryRange1);
                kryptonGallery1.DropButtonRanges.Add(kryptonGalleryRange2);
                kryptonGallery1.DropButtonRanges.Add(kryptonGalleryRange3);
            }
        }

        private void kryptonGallery1_GalleryDropMenu(object sender, GalleryDropMenuEventArgs e)
        {
            if (checkBoxAddCustomItems.Checked)
            {
                KryptonContextMenuHeading h = new KryptonContextMenuHeading();
                h.Text = "Customize Drop Menu";
                
                KryptonContextMenuItems items1 = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Custom Entry 1";
                KryptonContextMenuItem item2 = new KryptonContextMenuItem();
                item2.Text = "Custom Entry 2";
                item2.Checked = true;
                items1.Items.Add(item1);
                items1.Items.Add(item2);

                KryptonContextMenuItems items2 = new KryptonContextMenuItems();
                KryptonContextMenuItem item3 = new KryptonContextMenuItem();
                item3.Text = "Custom Entry 3";
                KryptonContextMenuItem item4 = new KryptonContextMenuItem();
                item4.Text = "Custom Entry 4";
                item4.CheckState = CheckState.Indeterminate;
                items2.Items.Add(item3);
                items2.Items.Add(item4);

                e.KryptonContextMenu.Items.Insert(0, new KryptonContextMenuSeparator());
                e.KryptonContextMenu.Items.Insert(0, items1);
                e.KryptonContextMenu.Items.Insert(0, h);
                e.KryptonContextMenu.Items.Add(new KryptonContextMenuSeparator());
                e.KryptonContextMenu.Items.Add(items2);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
