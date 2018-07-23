using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace KryptonTreeViewExamples
{
    public partial class Form1 : Form
    {
        private int _next = 1;
        private Random _rand = new Random();

        public Form1()
        {
            InitializeComponent();

            buttonAppend_Click(null, EventArgs.Empty);
            buttonInsert_Click(null, EventArgs.Empty);
            buttonInsert_Click(null, EventArgs.Empty);
            buttonInsert_Click(null, EventArgs.Empty);
            kryptonTreeView.SelectedNode = null;
            buttonAppend_Click(null, EventArgs.Empty);
            buttonInsert_Click(null, EventArgs.Empty);
            buttonInsert_Click(null, EventArgs.Empty);
            kryptonTreeView.SelectedNode = null;
            buttonAppend_Click(null, EventArgs.Empty);
            buttonInsert_Click(null, EventArgs.Empty);
            kryptonTreeView.SelectedNode = null;
            buttonAppend_Click(null, EventArgs.Empty);
            buttonAppend_Click(null, EventArgs.Empty);
        }

        private KryptonTreeNode CreateNewItem()
        {
            KryptonTreeNode item = new KryptonTreeNode();
            item.Text = "Item " + (_next++).ToString();
            item.ImageIndex = _rand.Next(imageList.Images.Count - 1);
            item.SelectedImageIndex = item.ImageIndex;
            return item;
        }

        private void buttonAppend_Click(object sender, EventArgs e)
        {
            TreeNode node = CreateNewItem();
            kryptonTreeView.Nodes.Add(node);

            // If nothing currently selected, then select the new one
            if (kryptonTreeView.SelectedNode == null)
                kryptonTreeView.SelectedNode = node;
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            // Can only insert if something is already selected
            if (kryptonTreeView.SelectedNode != null)
            {
                kryptonTreeView.SelectedNode.Nodes.Add(CreateNewItem());
                kryptonTreeView.SelectedNode.Expand();
            }
            else
                buttonAppend_Click(null, EventArgs.Empty);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            // Can only remove if something is selected
            if (kryptonTreeView.SelectedNode != null)
            {
                if (kryptonTreeView.SelectedNode.Parent != null)
                    kryptonTreeView.SelectedNode.Parent.Nodes.Remove(kryptonTreeView.SelectedNode);
                else
                    kryptonTreeView.Nodes.Remove(kryptonTreeView.SelectedNode);
            }
        }
        
        private void buttonClear_Click(object sender, EventArgs e)
        {
            kryptonTreeView.Nodes.Clear();
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
