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

namespace BasicPageDragAndDrop
{
    public partial class Form1 : Form
    {
        private int _count;
        private DragManager _dm;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _dm = new DragManager();
            _dm.StateCommon.Feedback = PaletteDragFeedback.Rounded;
            
            // Both navigators and workspaces are drag targets
            _dm.DragTargetProviders.Add(kryptonNavigator1);
            _dm.DragTargetProviders.Add(kryptonNavigator2);
            _dm.DragTargetProviders.Add(kryptonWorkspace1);
            _dm.DragTargetProviders.Add(kryptonWorkspace2);

            // Both navigators and workspaces can begin drag operations
            kryptonNavigator1.DragPageNotify = _dm;
            kryptonNavigator2.DragPageNotify = _dm;
            kryptonWorkspace1.DragPageNotify = _dm;
            kryptonWorkspace2.DragPageNotify = _dm;

            // Add initial pages to the navigators and workspaces
            kryptonNavigator1.Pages.AddRange(new KryptonPage[] { CreatePage(), CreatePage(), CreatePage() });
            kryptonNavigator2.Pages.AddRange(new KryptonPage[] { CreatePage(), CreatePage(), CreatePage() });
            kryptonWorkspace1.Root.Children.AddRange(new KryptonWorkspaceCell[] { CreateCell(2), CreateCell(2) });
            kryptonWorkspace2.Root.Children.AddRange(new KryptonWorkspaceCell[] { CreateCell(2), CreateCell(2) });
        }

        private void kryptonWorkspace1_WorkspaceCellAdding(object sender, WorkspaceCellEventArgs e)
        {
            // Hide the buttons we do not need for this sample
            e.Cell.Button.CloseButtonDisplay = ButtonDisplay.Hide;
            e.Cell.Button.ContextButtonDisplay = ButtonDisplay.Hide;
        }

        private void kryptonWorkspace2_WorkspaceCellAdding(object sender, WorkspaceCellEventArgs e)
        {
            // Hide the buttons we do not need for this sample
            e.Cell.Button.CloseButtonDisplay = ButtonDisplay.Hide;
            e.Cell.Button.ContextButtonDisplay = ButtonDisplay.Hide;

            // We want to provide header groups for the second workspace
            e.Cell.NavigatorMode = NavigatorMode.HeaderBarCheckButtonHeaderGroup;
            e.Cell.Header.HeaderVisibleSecondary = false;
        }

        private void radioBlock_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBlock.Checked)
                _dm.StateCommon.Feedback = PaletteDragFeedback.Block;
        }

        private void radioSquares_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSquares.Checked)
                _dm.StateCommon.Feedback = PaletteDragFeedback.Square;
        }

        private void radioRounded_CheckedChanged(object sender, EventArgs e)
        {
            if (radioRounded.Checked)
                _dm.StateCommon.Feedback = PaletteDragFeedback.Rounded;
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
            for (int i = 0; i < numPages; i++)
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

            // Create a rich text box with some sample text inside
            KryptonRichTextBox rtb = new KryptonRichTextBox();
            rtb.Text = "This page (" + page.Text + ") contains a rich text box control as example content. Your application could place anything you like here such as data entry controls, charts, data grids etc.";
            rtb.Dock = DockStyle.Fill;
            rtb.StateCommon.Border.Draw = InheritBool.False;

            // Add rich text box as the contents of the page
            page.Padding = new Padding(5);
            page.Controls.Add(rtb);

            return page;
        }
    }
}
