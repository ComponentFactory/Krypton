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
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.Win32;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace CellMaximizeAndRestore
{
    public partial class Form1 : Form
    {
        private string _pageText = @"{\rtf1\ansi\ansicpg1252\deff0\deflang3081{\fonttbl{\f0\fnil\fcharset0 Segoe UI;}{\f1\fswiss\fcharset0 Arial;}}
{\colortbl ;\red0\green128\blue128;}\viewkind4\uc1\pard\cf1\f0\fs24\b\fs22 Double Click\cf0\b0\fs18\par
Use the mouse to double click the tab headers and toggle between the maximized and restored mode for the cell that contains that clicked page.\par\par
\cf1\b\fs22 Context Menu\cf0\b0\fs18\par
Right-click the tab header and use the context menu option to toggle between maximized modes.\par\par
\cf1\b\fs22 Keyboard Shortcut\cf0\b0\fs18\par
Use the keyboard shortcut \i Ctrl + Shift + M\i0  to toggle maximized mode.\par\par
\cf1\b\fs22 Maximize/Restore Buttons\cf0\b0\fs18\par
A custom \i ButtonSpec \i0 has been added to the tabs area that can be clicked to toggle maximized modes.\f1\fs20\par}";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Define help text within each page
            richTextBox1.Rtf = _pageText;
            richTextBox2.Rtf = _pageText;
            richTextBox3.Rtf = _pageText;
            richTextBox4.Rtf = _pageText;
            richTextBox5.Rtf = _pageText;
            richTextBox6.Rtf = _pageText;
        }

        private void kryptonWorkspace_WorkspaceCellAdding(object sender, WorkspaceCellEventArgs e)
        {
            // When adding a new cell we need to default the tabs style
            e.Cell.Bar.TabStyle = TabStyle.OneNote;

            // Remove the standard buttons bar buttons
            e.Cell.Button.CloseButtonDisplay = ButtonDisplay.Hide;
            e.Cell.Button.ButtonDisplayLogic = ButtonDisplayLogic.None;

            // Generate event whenever a tab is double clicked
            e.Cell.TabDoubleClicked += new EventHandler<KryptonPageEventArgs>(OnTabDoubleClicked);
        }

        private void kryptonWorkspace_ActiveCellChanged(object sender, ActiveCellChangedEventArgs e)
        {
            if (e.OldCell != null)
                e.OldCell.Bar.TabStyle = TabStyle.OneNote;

            if (e.NewCell != null)
                e.NewCell.Bar.TabStyle = TabStyle.HighProfile;
        }

        private void OnTabDoubleClicked(object sender, KryptonPageEventArgs e)
        {
            // Find the cell that has this page
            KryptonWorkspaceCell cell = kryptonWorkspace.CellForPage(e.Item);

            // Toggle maximized state
            if (kryptonWorkspace.MaximizedCell == null)
                kryptonWorkspace.MaximizedCell = cell;
            else
                kryptonWorkspace.MaximizedCell = null;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
