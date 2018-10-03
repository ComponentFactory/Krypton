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
using System.Runtime.InteropServices;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace WorkspaceCellLayout
{
    public partial class Form1 : Form
    {
        private int _count = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initial appearance is same as clicking the 'New Three Cells' button
            buttonNewThreeCells_Click(buttonNewThreeCells, e);
        }

        private void kryptonWorkspace_WorkspaceCellAdding(object sender, WorkspaceCellEventArgs e)
        {
            // Remove the context menu from the tabs bar, as it is not relevant to this sample
            e.Cell.Button.ContextButtonDisplay = ButtonDisplay.Hide;
        }

        private void buttonNewSingleCell_Click(object sender, EventArgs e)
        {
            // Remove all existing workspace content
            kryptonWorkspace.Root.Children.Clear();

            // Add a single cell that has three pages
            kryptonWorkspace.Root.Children.Add(CreateCell(6));
        }

        private void buttonNewThreeCells_Click(object sender, EventArgs e)
        {
            // Remove all existing workspace content
            kryptonWorkspace.Root.Children.Clear();

            // Add three cells that have two pages each
            kryptonWorkspace.Root.Children.Add(CreateCell(2));
            kryptonWorkspace.Root.Children.Add(CreateCell(2));
            kryptonWorkspace.Root.Children.Add(CreateCell(2));

            // We want the root cells to be layed out horizontally
            kryptonWorkspace.Root.Orientation = Orientation.Horizontal;
        }

        private void buttonNewSequences_Click(object sender, EventArgs e)
        {
            // Remove all existing workspace content
            kryptonWorkspace.Root.Children.Clear();

            // Create a horizontal sequence with two cells
            KryptonWorkspaceSequence sequence2 = new KryptonWorkspaceSequence(Orientation.Horizontal);
            sequence2.Children.Add(CreateCell());
            sequence2.Children.Add(CreateCell());
            sequence2.Children.Add(CreateCell());

            // Create a vertical sequence with two cells and the horizontal sequence
            KryptonWorkspaceSequence sequence1 = new KryptonWorkspaceSequence(Orientation.Vertical);
            sequence1.Children.Add(CreateCell(2, "25*,25*"));
            sequence1.Children.Add(CreateCell(2, "25*,25*"));
            sequence1.Children.Add(sequence2);

            // Root contains two cells and the vertical sequence
            kryptonWorkspace.Root.Children.Add(CreateCell(1, "25*,25*"));
            kryptonWorkspace.Root.Children.Add(CreateCell(1, "25*,25*"));
            kryptonWorkspace.Root.Children.Add(sequence1);

            // We want the root cells to be layed out horizontally
            kryptonWorkspace.Root.Orientation = Orientation.Horizontal;
        }

        private void buttonApplySingleCell_Click(object sender, EventArgs e)
        {
            // Move all pages into a single cell
            kryptonWorkspace.ApplySingleCell();
        }

        private void buttonApplySingleRow_Click(object sender, EventArgs e)
        {
            kryptonWorkspace.ApplyGridPages(false, Orientation.Horizontal, 1);
        }

        private void buttonApplySingleColumn_Click(object sender, EventArgs e)
        {
            kryptonWorkspace.ApplyGridPages(false, Orientation.Vertical, 1);
        }

        private void buttonApplyGrid_Click(object sender, EventArgs e)
        {
            // Create a grid with one cell per page
            kryptonWorkspace.ApplyGridPages();
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Remove everything from the workspace
            kryptonWorkspace.Root.Children.Clear();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private KryptonWorkspaceCell CreateCell()
        {
            return CreateCell(1);
        }

        private KryptonWorkspaceCell CreateCell(int numPages)
        {
            return CreateCell(numPages, string.Empty);
        }

        private KryptonWorkspaceCell CreateCell(int numPages, string starSize)
        {
            // Create new cell instance
            KryptonWorkspaceCell cell = new KryptonWorkspaceCell();

            // Do we need to set the star sizing value?
            if (!string.IsNullOrEmpty(starSize))
                cell.StarSize = starSize;

            // Add requested number of pages
            for(int i=0; i<numPages; i++)
                cell.Pages.Add(CreatePage());

            return cell;
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
            rtb.Text = "This page (" + page.Text + ") contains a rich text box control as example content. Your application could place anything you like here such as data entry controls, charts, data grids etc.\n\nTry dragging the page headers in order to rearrange the workspace layout.";
            rtb.Dock = DockStyle.Fill;
            rtb.StateCommon.Border.Draw = InheritBool.False;

            // Add rich text box as the contents of the page
            page.Padding = new Padding(5);
            page.Controls.Add(rtb);

            return page;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a grid with one cell per page
            kryptonWorkspace.ApplyGridPages(true, Orientation.Horizontal);
        }
    }
}
