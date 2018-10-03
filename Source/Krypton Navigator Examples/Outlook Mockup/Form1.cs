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
using ComponentFactory.Krypton.Navigator;

namespace OutlookMockup
{
    public partial class Form1 : KryptonForm
    {
        // Cache the full expanded size of the outlook navigator
        private int _widthLeftRight;

        // Set of fake notes entries
        private object[] _notes = new object[]{ new string[] { "Bug Reports v1.2", "Featuer Requests v1.3", "Wish List" },
                                                new string[] { "Xmas List", "Birthday Dates" },
                                                new string[] { "Season Schedule", "Party Invites", "Jokes", "Diary" } };


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Populate the different data table
            dtDrafts.Rows.Add("Ray Clemence", "Need to improve handling", "Mon 02/09/2006", "10 KB");
            dtDrafts.Rows.Add("Garth Crooks", "Excellent team performance", "Tue 03/09/2006", "5 KB");
            dtDrafts.Rows.Add("Peter Shilten", "Good defence is important", "Wed 04/09/2006", "21 KB");
            dtDrafts.Rows.Add("Steve McClaren", "Nice work, keep it going", "Tue 05/09/2006", "11 KB");
            dtFamily.Rows.Add("John Smith", "Did you get my new pics?", "Mon 02/09/2006", "11 KB");
            dtFamily.Rows.Add("Mike Smith", "Remember to get Emma a present", "Mon 02/09/2006", "17 KB");
            dtFamily.Rows.Add("Susan Smith", "What did you get Emma?", "Tue 03/09/2006", "10 KB");
            dtFamily.Rows.Add("Emma Smith", "RE: Happy Birthday!", "Wed 04/09/2006", "6 KB");
            dtFamily.Rows.Add("Emma Smith", "Have you seen Dad recently?", "Thu 05/09/2006", "32 KB");
            dtFamily.Rows.Add("Percy Smith", "Great holiday, see you soon", "Thu 05/09/2006", "2 KB");
            dtFriends.Rows.Add("Dirk Huber", "Arsenal 0 - Liverpool 3 Nice!!", "Tue 03/09/2006", "2 KB");
            dtFriends.Rows.Add("Jimmy Jones", "Are you going to the big game?", "Wed 04/09/2006", "32 KB");
            dtFriends.Rows.Add("Nick Robinson", "Just noticed you have a blog", "Thu 05/09/2006", "2 KB");
            dtWork.Rows.Add("Your Boss", "Take the week off", "Wed 04/09/2006", "2 KB");
            dtOutbox.Rows.Add("Wayne Rooney", "Need to control your emotions", "Thu 01/09/2006", "17 KB");
            dtSentItems.Rows.Add("Sven Ericcson", "Poor managing performance", "Fri 07/09/2006", "5 KB");
            dtSentItems.Rows.Add("David Beckham", "You have been dropped", "Thu 05/09/2006", "12 KB");

            // Set the initial main and detail pages
            kryptonNavigatorMain.SelectedIndex = 0;
            kryptonNavigatorDetails.SelectedIndex = 0;

            // Start with all folders expanded
            treeViewMailFolders.ExpandAll();

            // Set initial focus to the tree view for mail
            treeViewMailFolders.Focus();
            treeViewMailFolders.SelectedNode = treeViewMailFolders.Nodes[2].Nodes[0];

            // Set the initial set of notes entries
            radioNotes_CheckedChanged(radioProject, EventArgs.Empty);
        }

        private void buttonSpecExpandCollapse_Click(object sender, EventArgs e)
        {
            kryptonSplitContainerMain.SuspendLayout();
            kryptonNavigatorMain.SuspendLayout();

            // Is the navigator currently in full mode?
            if (kryptonNavigatorMain.NavigatorMode == NavigatorMode.OutlookFull)
            {
                // Make the left panel of the splitter fixed in size
                kryptonSplitContainerMain.FixedPanel = FixedPanel.Panel1;
                kryptonSplitContainerMain.IsSplitterFixed = true;

                // Remember the current height of the header group
                _widthLeftRight = kryptonNavigatorMain.Width;

                // Switch to the mini mode
                kryptonNavigatorMain.NavigatorMode = NavigatorMode.OutlookMini;

                // Discover the new width required to display the mini mode
                int newWidth = kryptonNavigatorMain.PreferredSize.Width;

                // Make the header group fixed just as the new height
                kryptonSplitContainerMain.Panel1MinSize = newWidth;
                kryptonSplitContainerMain.SplitterDistance = newWidth;

                // Switch the arrow to point the opposite way
                buttonSpecExpandCollapse.TypeRestricted = PaletteNavButtonSpecStyle.ArrowRight;
            }
            else
            {
                // Switch to the full mode
                kryptonNavigatorMain.NavigatorMode = NavigatorMode.OutlookFull;

                // Make the bottom panel not-fixed in size anymore
                kryptonSplitContainerMain.FixedPanel = FixedPanel.None;
                kryptonSplitContainerMain.IsSplitterFixed = false;

                // Put back the minimum size to the original
                kryptonSplitContainerMain.Panel1MinSize = 100;

                // Calculate the correct splitter we want to put back
                kryptonSplitContainerMain.SplitterDistance = _widthLeftRight;

                // Switch the arrow to point the opposite way
                buttonSpecExpandCollapse.TypeRestricted = PaletteNavButtonSpecStyle.ArrowLeft;
            }

            kryptonSplitContainerMain.ResumeLayout();
            kryptonNavigatorMain.ResumeLayout();
        }


        private void kryptonNavigatorMain_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the details page to match the main pages
            kryptonNavigatorDetails.SelectedIndex = kryptonNavigatorMain.SelectedIndex;
        }

        private void treeViewMail_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Remove selection from the other tree
            if (sender == treeViewMailFavs)
                treeViewMailFolders.SelectedNode = null;
            else
                treeViewMailFavs.SelectedNode = null;

            // Cast event source to the correct type
            TreeView tv = (TreeView)sender;

            // Update the mail heading entries
            if (tv.SelectedNode != null)
            {
                // Update the title to match the folder
                kryptonPageMailDetails.TextTitle = tv.SelectedNode.Text;

                // Set the data grid to show details from this table
                kryptonDataGridView1.DataSource = dataSet.Tables[tv.SelectedNode.Text];
            }
            else
            {
                // Update the title to a generic title
                kryptonPageMailDetails.TextTitle = "Mail";

                // Nothing selected so remove any source from the data grid
                kryptonDataGridView1.DataSource = null;
            }
        }

        private void radioNotes_CheckedChanged(object sender, EventArgs e)
        {
            int index = 0;

            // Find index of note names
            if (radioFamily.Checked)
                index = 1;
            else if (radioFriends.Checked)
                index = 2;

            // Remove all existing notes
            listViewNotes.Items.Clear();

            // Get the set of strings that contain the note names
            string[] group = (string[])_notes[index];

            // Add each mail entry as an item
            foreach (string entry in group)
                listViewNotes.Items.Add(new ListViewItem(entry, 0));
        }

        private void radioOffice2010Blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2010Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        private void radioOffice2010Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2010Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
        }

        private void radioOffice2010Black_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2010Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }

        private void radioOffice2007Blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2007Blue.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
        }

        private void radioOffice2007Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2007Silver.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
        }

        private void radioOffice2007Black_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2007Black.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
        }

        private void radioOffice2003_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOffice2003.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
        }

        private void radioSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSystem.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void radioSparkle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSparkle.Checked)
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
        }
    }
}
