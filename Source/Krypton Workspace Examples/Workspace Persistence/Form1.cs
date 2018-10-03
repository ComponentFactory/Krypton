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
using System.Xml;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace WorkspacePersistence
{
    public partial class Form1 : Form
    {
        private int _count = 1;
        private byte[] _byteArray;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create three cells that each contain two pages
            KryptonWorkspaceCell cell1 = new KryptonWorkspaceCell();
            KryptonWorkspaceCell cell2 = new KryptonWorkspaceCell();
            KryptonWorkspaceCell cell3 = new KryptonWorkspaceCell();
            cell1.Pages.AddRange(new KryptonPage[] { CreatePage(), CreatePage() });
            cell2.Pages.AddRange(new KryptonPage[] { CreatePage(), CreatePage() });
            cell3.Pages.AddRange(new KryptonPage[] { CreatePage(), CreatePage() });

            // Create a vertical sequence that contains two of the pages
            KryptonWorkspaceSequence sequence = new KryptonWorkspaceSequence(Orientation.Vertical);
            sequence.Children.AddRange(new KryptonWorkspaceCell[] { cell2, cell3 });

            // Remove starting contents and add a cell with a sequence
            kryptonWorkspace.Root.Children.Clear();
            kryptonWorkspace.Root.Children.Add(cell1);
            kryptonWorkspace.Root.Children.Add(sequence);
        }

        private void bSaveToArray_Click(object sender, EventArgs e)
        {
            _byteArray = kryptonWorkspace.SaveLayoutToArray();
            bLoadFromArray.Enabled = true;
        }

        private void bLoadFromArray_Click(object sender, EventArgs e)
        {
            kryptonWorkspace.LoadLayoutFromArray(_byteArray);
        }

        private void bSaveToFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                kryptonWorkspace.SaveLayoutToFile(saveFileDialog.FileName);
        }

        private void bLoadFromFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    kryptonWorkspace.LoadLayoutFromFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Loading from File");
                }
            }
        }

        private void kryptonWorkspace_PageSaving(object sender, PageSavingEventArgs e)
        {
            // Get access to the text box inside the page
            KryptonRichTextBox rtb = (KryptonRichTextBox)e.Page.Controls[0];

            // Save the text in the textbox into the per-page storage
            e.XmlWriter.WriteCData(rtb.Text);
        }

        private void kryptonWorkspace_PageLoading(object sender, PageLoadingEventArgs e)
        {
            KryptonRichTextBox rtb;

            // If a new page then it does not have any children...
            if (e.Page.Controls.Count == 0)
            {
                // Add a rich text box as the child of the page
                rtb = new KryptonRichTextBox();
                rtb.Dock = DockStyle.Fill;
                rtb.StateCommon.Border.Draw = InheritBool.False;
                e.Page.Controls.Add(rtb);
                e.Page.Padding = new Padding(5);
            }
            else
                rtb = (KryptonRichTextBox)e.Page.Controls[0];

            // Move past the current xml element to the child CData
            e.XmlReader.Read();

            // Read in the stored text and use it in the rich text box
            rtb.Text = e.XmlReader.ReadContentAsString();
        }

        private void kryptonWorkspace_RecreateLoadingPage(object sender, RecreateLoadingPageEventArgs e)
        {
            e.Page = new KryptonPage();
        }

        private void kryptonWorkspace_PagesUnmatched(object sender, PagesUnmatchedEventArgs e)
        {
            foreach (KryptonPage page in e.Unmatched)
                Console.WriteLine("Unmatched Page {0}", page.Text);
        }

        private void buttonAddPage_Click(object sender, EventArgs e)
        {
            // Add page to the currently active cell
            if (kryptonWorkspace.ActiveCell != null)
            {
                kryptonWorkspace.ActiveCell.Pages.Add(CreatePage());
                kryptonWorkspace.ActiveCell.SelectedIndex = kryptonWorkspace.ActiveCell.Pages.Count - 1;
            }
        }

        private void buttonClearPages_Click(object sender, EventArgs e)
        {
            kryptonWorkspace.Root.Children.Clear();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private KryptonPage CreatePage()
        {
            // Give each page a unique number
            string pageNumber = (_count++).ToString();

            // Create a new page and give it a name and image
            KryptonPage page = new KryptonPage();
            page.Text = "P" + pageNumber;
            page.TextTitle = "P" + pageNumber + " Title";
            page.TextDescription = "P" + pageNumber + " Description";
            page.ImageSmall = imageList.Images[_count % imageList.Images.Count];
            page.MinimumSize = new Size(200, 250);

            // Create a rich text box with some sample text inside
            KryptonRichTextBox rtb = new KryptonRichTextBox();
            rtb.Text = "This page (" + page.Text + ") contains a rich text box control as example content.\n\nTry saving the layout and then dragging the page headers in order to rearrange the workspace layout. Once altered you can use the load button to get back to the original state.";
            rtb.Dock = DockStyle.Fill;
            rtb.StateCommon.Border.Draw = InheritBool.False;

            // Add rich text box as the contents of the page
            page.Padding = new Padding(5);
            page.Controls.Add(rtb);

            return page;
        }
    }
}
