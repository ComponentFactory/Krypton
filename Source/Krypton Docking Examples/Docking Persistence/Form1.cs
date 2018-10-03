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
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;
using ComponentFactory.Krypton.Docking;

namespace DockingPersistence
{
    public partial class Form1 : KryptonForm
    {
        private int _count = 1;
        private byte[] _array1;
        private byte[] _array2;
        private byte[] _array3;

        public Form1()
        {
            InitializeComponent();
        }

        private KryptonPage NewDocument()
        {
            KryptonPage page = NewPage("Document ", 0, new ContentDocument());

            // Document pages cannot be docked or auto hidden
            page.ClearFlags(KryptonPageFlags.DockingAllowAutoHidden | KryptonPageFlags.DockingAllowDocked);

            return page;
        }

        private KryptonPage NewInput()
        {
            return NewPage("Input ", 1, new ContentInput());
        }

        private KryptonPage NewPropertyGrid()
        {
            return NewPage("Properties ", 2, new ContentPropertyGrid());
        }

        private KryptonPage NewPage(string name, int image, Control content)
        {
            // Create new page with title and image
            KryptonPage p = new KryptonPage();
            p.Text = name + _count.ToString();
            p.TextTitle = name + _count.ToString();
            p.TextDescription = name + _count.ToString();
            p.UniqueName = p.Text;
            p.ImageSmall = imageListSmall.Images[image];

            // Add the control for display inside the page
            content.Dock = DockStyle.Fill;
            p.Controls.Add(content);

            _count++;
            return p;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup docking functionality
            KryptonDockingWorkspace w = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl(kryptonPanel, w);
            kryptonDockingManager.ManageFloating(this);

            // Add docking pages
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Left, new KryptonPage[] { NewPropertyGrid() });
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Bottom, new KryptonPage[] { NewInput(), NewInput() });
            kryptonDockingManager.AddAutoHiddenGroup("Control", DockingEdge.Right, new KryptonPage[] { NewPropertyGrid() });
            kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { NewDocument(), NewDocument(), NewDocument() });
        }

        private void buttonSaveArray1_Click(object sender, EventArgs e)
        {
            _array1 = kryptonDockingManager.SaveConfigToArray();
            buttonLoadArray1.Enabled = true;
        }

        private void buttonSaveArray2_Click(object sender, EventArgs e)
        {
            _array2 = kryptonDockingManager.SaveConfigToArray();
            buttonLoadArray2.Enabled = true;
        }

        private void buttonSaveArray3_Click(object sender, EventArgs e)
        {
            _array3 = kryptonDockingManager.SaveConfigToArray();
            buttonLoadArray3.Enabled = true;
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                kryptonDockingManager.SaveConfigToFile(saveFileDialog.FileName);
        }

        private void buttonLoadArray1_Click(object sender, EventArgs e)
        {
            kryptonDockingManager.LoadConfigFromArray(_array1);
        }

        private void buttonLoadArray2_Click(object sender, EventArgs e)
        {
            kryptonDockingManager.LoadConfigFromArray(_array2);
        }

        private void buttonLoadArray3_Click(object sender, EventArgs e)
        {
            kryptonDockingManager.LoadConfigFromArray(_array3);
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                kryptonDockingManager.LoadConfigFromFile(openFileDialog.FileName);
        }


        private void buttonHideAll_Click(object sender, EventArgs e)
        {
            kryptonDockingManager.HideAllPages();
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            kryptonDockingManager.ShowAllPages();
        }

        private void kryptonDockingManager_GlobalSaving(object sender, DockGlobalSavingEventArgs e)
        {
            // Example code showing how to save extra data into the global config
            e.XmlWriter.WriteStartElement("CustomGlobalData");
            e.XmlWriter.WriteAttributeString("SavedTime", DateTime.Now.ToString());
            e.XmlWriter.WriteEndElement();
        }

        private void kryptonDockingManager_GlobalLoading(object sender, DockGlobalLoadingEventArgs e)
        {
            // Example code showing how to reload the extra data that was saved into the global config
            e.XmlReader.Read();
            Console.WriteLine("GlobalConfig was saved at {0}", e.XmlReader.GetAttribute("SavedTime"));
            e.XmlReader.Read();
        }

        private void kryptonDockingManager_PageSaving(object sender, DockPageSavingEventArgs e)
        {
            // Example code showing how to save extra data into the page config
            e.XmlWriter.WriteStartElement("CustomPageData");
            e.XmlWriter.WriteAttributeString("SavedMilliseconds", DateTime.Now.Millisecond.ToString());
            e.XmlWriter.WriteEndElement();
        }

        private void kryptonDockingManager_PageLoading(object sender, DockPageLoadingEventArgs e)
        {
            // Example code showing how to reload the extra data that was saved into the page config
            e.XmlReader.Read();
            Console.WriteLine("PageConfig was saved at {0}", e.XmlReader.GetAttribute("SavedMilliseconds"));
            e.XmlReader.Read();
        }

        private void kryptonContextMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
