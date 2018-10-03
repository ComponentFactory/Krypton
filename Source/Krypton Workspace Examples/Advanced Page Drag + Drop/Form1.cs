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

namespace AdvancedPageDragAndDrop
{
    public partial class Form1 : Form
    {
        private DragManager _dm;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _dm = new DragManager();
            _dm.StateCommon.Feedback = PaletteDragFeedback.Rounded;

            // Add controls that are drop targets
            _dm.DragTargetProviders.Add(dragTreeView);
            _dm.DragTargetProviders.Add(kryptonNavigator);
            _dm.DragTargetProviders.Add(kryptonWorkspace);

            // Controls that can begin drag operations
            dragTreeView.DragPageNotify = _dm;
            kryptonNavigator.DragPageNotify = _dm;
            kryptonWorkspace.DragPageNotify = _dm;

            // Add initial pages to the navigator and workspace
            kryptonNavigator.Pages.AddRange(new KryptonPage[] { CreatePage("Canberra", 7), CreatePage("Nicosia", 8) });
            kryptonWorkspace.Root.Children.Clear();
            kryptonWorkspace.Root.Children.AddRange(new KryptonWorkspaceCell[] { CreateCell("Dublin", 9, "Oslo", 10), CreateCell("Budapest", 11, "Tokyo", 12) });
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

        private void workspaceCellAdding(object sender, WorkspaceCellEventArgs e)
        {
            // Hide the default close and context buttons, they are not relevant for this demo
            e.Cell.Button.CloseButtonAction = CloseButtonAction.None;
            e.Cell.Button.CloseButtonDisplay = ButtonDisplay.Hide;
            e.Cell.Button.ContextButtonDisplay = ButtonDisplay.Hide;
        }

        private KryptonWorkspaceCell CreateCell(string title, int imageIndex)
        {
            KryptonWorkspaceCell cell = new KryptonWorkspaceCell();
            cell.Pages.Add(CreatePage(title, imageIndex));
            return cell;
        }

        private KryptonWorkspaceCell CreateCell(string title1, int imageIndex1,
                                                string title2, int imageIndex2)
        {
            KryptonWorkspaceCell cell = new KryptonWorkspaceCell();
            cell.Pages.Add(CreatePage(title1, imageIndex1));
            cell.Pages.Add(CreatePage(title2, imageIndex2));
            return cell;
        }

        private KryptonPage CreatePage(string title, int imageIndex)
        {
            // Create a new page and give it a name and image
            KryptonPage page = new KryptonPage();
            page.Text = title;
            page.TextTitle = title + " Title";
            page.TextDescription = title + " Description";
            page.ImageSmall = imageList.Images[imageIndex];
            page.Tag = imageIndex.ToString();

            // Create a rich text box with some sample text inside
            KryptonRichTextBox rtb = new KryptonRichTextBox();
            rtb.Text = "This page (" + page.Text + ") contains a rich text box control as example content.";
            rtb.Dock = DockStyle.Fill;
            rtb.StateCommon.Border.Draw = InheritBool.False;

            // Add rich text box as the contents of the page
            page.Padding = new Padding(5);
            page.Controls.Add(rtb);

            return page;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
