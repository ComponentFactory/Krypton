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

namespace ThreePaneApplication
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Appointments
            dataTable.Rows.Add("Julius Ceaser", "End of career review for roman work", "Leadership skills are exceptional.\nGood work surpressing the Gauls.\nTends to make political enemies.\nSticky end predicted.", "Appointments", "Staff reviews", true);
            dataTable.Rows.Add("Winston Churchhill", "End of war review for military campaign", "Very popular with the common people.\nTends towards being stubborn.\nEasy going after two whiskys.\nReward with box of good cigars.\nToo old for new projects.", "Appointments", "Staff reviews", false);
            dataTable.Rows.Add("Ghengis Khan", "End of year appraisal", "Good with animals.\nTends towards violent outbursts.\nCould do better.", "Appointments", "Staff reviews", false);
            dataTable.Rows.Add("Steve Jobs", "Interview for positon as junior tester", "Enjoys working with shiny objects.\nPrefers working in white rooms.\nEasily distracted by noises.\nRejected", "Appointments", "Job interviews", true);
            dataTable.Rows.Add("Larry Ellison", "Interview for marketing position", "Likes large marketing budgets.\nSpends all budget on new plane.\nLast heard he was in Hawaii.\nRejected", "Appointments", "Job interviews", false);
            dataTable.Rows.Add("Project Orcas", "Milestone review of work completed", "Project currently on target.\nNeed to improve average.\nMust order new project mugs.", "Appointments", "Project meetings", true);
            dataTable.Rows.Add("Project Zebra", "Kick off meeing for Mac OS work", "Project in stealth mode.\nEnsure official name is more sexy.", "Appointments", "Project meetings", true);

            // Employees
            dataTable.Rows.Add("Holly Hunter", "Secretary", "New member of staff.\nVery quick typist.\nJust one task at a time.", "Employees", "Administration", false);
            dataTable.Rows.Add("Paula Abdul", "Meet and greet", "Good singing voice.\nKeep away from accounts.", "Employees", "Administration", true);
            dataTable.Rows.Add("Zak Wolfson", "Junior developer", "Good degree from MIT.\nIrrational need to use Linux.\nGood when closely supervised.", "Employees", "Programmers", false);
            dataTable.Rows.Add("Simon Cowell", "Senior architect", "Poor understanding of concepts.\nPoor mentoring of junior staff.\nLooks good on televison.\nFire at first opportunity.", "Employees", "Programmers", true);
            dataTable.Rows.Add("Peter Andre", "President", "Always appears at press conferences.\nPicture of cover of accounts.\nDrives a nice car.\nComplete egomaniac.", "Employees", "Managers", false);

            // Use the filtered view of the data table
            kryptonDataGridView.DataMember = string.Empty;
            kryptonDataGridView.DataSource = dataTable.DefaultView;

            // Set correct initial checked button
            if (KryptonManager.CurrentGlobalPalette == KryptonManager.PaletteOffice2007Black)
                toolStripOffice2007Black_Click(this, EventArgs.Empty);

            // Expand all the nodes to show entire tree structure
            foreach(TreeNode n in treeView.Nodes)
                n.ExpandAll();

            // Hook into the up and down buttons on the details heading
            kryptonHeaderGroupDetails.ButtonSpecs[0].Click += new EventHandler(OnPrevious);
            kryptonHeaderGroupDetails.ButtonSpecs[1].Click += new EventHandler(OnNext);
        }

        private void Form1_SystemColorsChanged(object sender, EventArgs e)
        {
            // If the system colors change that might change the palette background
            // if the palette is calculating it from the system colors and so update
            // the control colors just in case.
            UpdateOnPaletteChanged();
        }

        private void toolStripLoadPalette_Click(object sender, EventArgs e)
        {
            loadPaletteToolStripMenuItem_Click(this, EventArgs.Empty);
        }

        private void toolStripOffice2010Blue_Click(object sender, EventArgs e)
        {
            if (!toolStripOffice2010Blue.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = true;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripOffice2010Silver_Click(object sender, EventArgs e)
        {
            if (!toolStripOffice2010Silver.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = true;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripOffice2010Black_Click(object sender, EventArgs e)
        {
            if (!toolStripOffice2010Black.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = true;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripOffice2007Blue_Click(object sender, EventArgs e)
        {
            if (!toolStripOffice2007Blue.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = true;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripOffice2007Silver_Click(object sender, EventArgs e)
        {
            if (!toolStripOffice2007Silver.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = true;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripOffice2007Black_Click(object sender, EventArgs e)
        {
            if (!toolStripOffice2007Black.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = true;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripSparkleBlue_Click(object sender, EventArgs e)
        {
            if (!toolStripSparkleBlue.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = true;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripSparkleOrange_Click(object sender, EventArgs e)
        {
            if (!toolStripSparkleOrange.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = true;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripSparklePurple_Click(object sender, EventArgs e)
        {
            if (!toolStripSparklePurple.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparklePurple;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = true;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripSystem_Click(object sender, EventArgs e)
        {
            if (!toolStripSystem.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = true;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = false;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripCustom_Click(object sender, EventArgs e)
        {
            if (!toolStripCustom.Checked)
            {
                kryptonManager.GlobalPalette = kryptonPaletteCustom;
                toolStripOffice2010Blue.Checked = office2010BlueToolStripMenuItem.Checked = false;
                toolStripOffice2010Silver.Checked = office2010SilverToolStripMenuItem.Checked = false;
                toolStripOffice2010Black.Checked = office2010BlackToolStripMenuItem.Checked = false;
                toolStripOffice2007Blue.Checked = office2007BlueToolStripMenuItem.Checked = false;
                toolStripOffice2007Silver.Checked = office2007SilverToolStripMenuItem.Checked = false;
                toolStripOffice2007Black.Checked = office2007BlackToolStripMenuItem.Checked = false;
                toolStripSparkleBlue.Checked = sparkleBlueToolStripMenuItem.Checked = false;
                toolStripSparkleOrange.Checked = sparkleOrangeToolStripMenuItem.Checked = false;
                toolStripSparklePurple.Checked = sparklePurpleToolStripMenuItem.Checked = false;
                toolStripSystem.Checked = systemToolStripMenuItem.Checked = false;
                toolStripCustom.Checked = customToolStripMenuItem.Checked = true;
                UpdateOnPaletteChanged();
            }
        }

        private void toolStripReadingPane_CheckedChanged(object sender, EventArgs e)
        {
            // Show/Hide the reading pane depending on new setting
            kryptonSplitContainerDetails.Panel2Collapsed = toolStripReadingPane.Checked;
            readingPaneToolStripMenuItem.Checked = toolStripReadingPane.Checked;
        }

        private void readingPaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripReadingPane.Checked = !toolStripReadingPane.Checked;
        }

        private void toolStripPosition_CheckedChanged(object sender, EventArgs e)
        {
            // Vertical/Horizontal alignment depending on new setting
            kryptonSplitContainerDetails.Orientation = (toolStripPosition.Checked ? Orientation.Vertical : Orientation.Horizontal);
            panePositonToolStripMenuItem.Checked = toolStripPosition.Checked;
        }

        private void panePositonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripPosition.Checked = !toolStripPosition.Checked;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                // Update the details header with selected node values
                kryptonHeaderGroupDetails.ValuesPrimary.Heading = e.Node.Text;
                kryptonHeaderGroupDetails.ValuesPrimary.Image = imageList.Images[e.Node.ImageIndex];

                // Change list of displayed items based on the new tree selection
                FilterDataTable(e.Node);
            }
            else
            {
                // Should never happen, but just in case!
                kryptonHeaderGroupDetails.ValuesPrimary.Heading = "Details";
                kryptonHeaderGroupDetails.ValuesPrimary.Image = null;
            }
        }

        private void kryptonDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (kryptonDataGridView.SelectedRows.Count == 1)
            {
                string details = (string)kryptonDataGridView.SelectedRows[0].Cells[2].Value;
                kryptonReadingLabel.Values.Text = details;
            }
            else
                kryptonReadingLabel.Values.Text = string.Empty;
        }

        private void loadPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a new palette for the importing
            KryptonPalette newPalette = new KryptonPalette();

            // If the user managed to load a palette file
            if (newPalette.Import().Length > 0)
            {
                // Then switch the using the new one
                kryptonPaletteCustom = newPalette;

                if (!toolStripCustom.Checked)
                {
                    // Then use existing method to switch to using the custom palette
                    toolStripCustom_Click(null, EventArgs.Empty);
                }
                else
                {
                    // Use the custom palette
                    kryptonManager.GlobalPalette = kryptonPaletteCustom;
                    UpdateOnPaletteChanged();
                }

                // Change of palette means a change in colors that need 
                // applying to the background of the standard controls
                UpdateOnPaletteChanged();
            }
        }

        private void OnNext(object sender, EventArgs e)
        {
            // If nothing is selected
            if (kryptonDataGridView.SelectedRows.Count == 0)
            {
                // If there are rows in the grid
                if (kryptonDataGridView.Rows.Count > 0)
                {
                    // Select the first row
                    kryptonDataGridView.Rows[0].Selected = true;
                }
            }
            else
            {
                // Find index of next row
                int index = kryptonDataGridView.SelectedRows[0].Index + 1;

                // If past end of list then go back to the start
                if (index >= kryptonDataGridView.Rows.Count)
                    index = 0;

                // Select the row
                kryptonDataGridView.Rows[index].Selected = true;
            }

            kryptonDataGridView.Refresh();
        }

        private void OnPrevious(object sender, EventArgs e)
        {
            // If nothing is selected
            if (kryptonDataGridView.SelectedRows.Count == 0)
            {
                // If there are rows in the grid
                if (kryptonDataGridView.Rows.Count > 0)
                {
                    // Select the last row
                    kryptonDataGridView.Rows[kryptonDataGridView.Rows.Count - 1].Selected = true;
                }
            }
            else
            {
                // Find index of previous row
                int index = kryptonDataGridView.SelectedRows[0].Index - 1;

                // If past start of list then go back to the end
                if (index < 0)
                    index = kryptonDataGridView.Rows.Count - 1;

                // Select the row
                kryptonDataGridView.Rows[index].Selected = true;
            }

            kryptonDataGridView.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FilterDataTable(TreeNode node)
        {
            // Add entries for the current node
            if (!string.IsNullOrEmpty(node.Text))
            {
                if ((node.Text == "Appointments") ||
                    (node.Text == "Employees"))
                {
                    dataTable.DefaultView.RowFilter = "Department = '" + node.Text + "'";
                }
                else
                {
                    dataTable.DefaultView.RowFilter = "Category = '" + node.Text + "'";
                }
            }
        }

        private void UpdateOnPaletteChanged()
        {
            // Get the new control background color
            Color backColor = kryptonManager.GlobalPalette.GetBackColor1(PaletteBackStyle.ControlClient,
                                                                         PaletteState.Normal);

            // Update the tree and listview controls with new color
            treeView.BackColor = backColor;
        }
    }
}
