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

namespace ExpandingHeaderGroupsSplitters
{
    public partial class Form1 : KryptonForm
    {
        private int _heightUpDown;
        private int _widthLeftRight;

        public Form1()
        {
            InitializeComponent();

            // Hook into the click events on the header buttons
            kryptonHeaderGroupRightBottom.ButtonSpecs[0].Click += new EventHandler(OnUpDown);
            kryptonHeaderGroupLeft.ButtonSpecs[0].Click += new EventHandler(OnLeftRight);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set position of caret in the text boxes, so it looks nicer at runtime!
            textBoxLeft.SelectionStart = textBoxRightTop.SelectionStart = textBoxRightBottom.SelectionStart = 0;
            textBoxLeft.SelectionLength = textBoxRightTop.SelectionLength = textBoxRightBottom.SelectionLength = 0;
        }

        private void OnUpDown(object sender, EventArgs e)
        {
            // Suspend layout changes until all splitter properties have been updated
            kryptonSplitContainerVertical.SuspendLayout();

            // Is the bottom right header group currently expanded?
            if (kryptonSplitContainerVertical.FixedPanel == FixedPanel.None)
            {
                // Make the bottom panel of the splitter fixed in size
                kryptonSplitContainerVertical.FixedPanel = FixedPanel.Panel2;
                kryptonSplitContainerVertical.IsSplitterFixed = true;

                // Remember the current height of the header group (to restore later)
                _heightUpDown = kryptonHeaderGroupRightBottom.Height;

                // Find the new height to use for the header group
                int newHeight = kryptonHeaderGroupRightBottom.PreferredSize.Height;

                // Make the header group fixed to the new height
                kryptonSplitContainerVertical.Panel2MinSize = newHeight;
                kryptonSplitContainerVertical.SplitterDistance = kryptonSplitContainerVertical.Height;
            }
            else
            {
                // Make the bottom panel not-fixed in size anymore
                kryptonSplitContainerVertical.FixedPanel = FixedPanel.None;
                kryptonSplitContainerVertical.IsSplitterFixed = false;

                // Put back the minimise size to the original
                kryptonSplitContainerVertical.Panel2MinSize = 100;

                // Calculate the correct splitter we want to put back
                kryptonSplitContainerVertical.SplitterDistance = kryptonSplitContainerVertical.Height - _heightUpDown - kryptonSplitContainerVertical.SplitterWidth;
            }

            kryptonSplitContainerVertical.ResumeLayout();
        }

        private void OnLeftRight(object sender, EventArgs e)
        {
            // Suspend layout changes until all splitter properties have been updated
            kryptonSplitContainerHorizontal.SuspendLayout();

            // Is the left header group currently expanded?
            if (kryptonSplitContainerHorizontal.FixedPanel == FixedPanel.None)
            {
                // Make the left panel of the splitter fixed in size
                kryptonSplitContainerHorizontal.FixedPanel = FixedPanel.Panel1;
                kryptonSplitContainerHorizontal.IsSplitterFixed = true;

                // Remember the current height of the header group
                _widthLeftRight = kryptonHeaderGroupLeft.Width;

                // We have not changed the orientation of the header yet, so the width of 
                // the splitter panel is going to be the height of the collapsed header group
                int newWidth = kryptonHeaderGroupLeft.PreferredSize.Height;

                // Make the header group fixed just as the new height
                kryptonSplitContainerHorizontal.Panel1MinSize = newWidth;
                kryptonSplitContainerHorizontal.SplitterDistance = newWidth;

                // Change header to be vertical and button to near edge
                kryptonHeaderGroupLeft.HeaderPositionPrimary = VisualOrientation.Right;
                kryptonHeaderGroupLeft.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Near;
            }
            else
            {
                // Make the bottom panel not-fixed in size anymore
                kryptonSplitContainerHorizontal.FixedPanel = FixedPanel.None;
                kryptonSplitContainerHorizontal.IsSplitterFixed = false;

                // Put back the minimise size to the original
                kryptonSplitContainerHorizontal.Panel1MinSize = 100;

                // Calculate the correct splitter we want to put back
                kryptonSplitContainerHorizontal.SplitterDistance = _widthLeftRight;

                // Change header to be horizontal and button to far edge
                kryptonHeaderGroupLeft.HeaderPositionPrimary = VisualOrientation.Top;
                kryptonHeaderGroupLeft.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Far;
            }

            kryptonSplitContainerHorizontal.ResumeLayout();
        }

        private void UpdateCollapsedSizing()
        {
            // Is the bottom right header group currently collapsed?
            if (kryptonSplitContainerVertical.FixedPanel == FixedPanel.Panel2)
            {
                // Suspend layout changes until all splitter properties have been updated
                kryptonSplitContainerHorizontal.SuspendLayout();

                // Get the new preferred height of the header group and apply it
                int newHeight = kryptonHeaderGroupRightBottom.PreferredSize.Height;
                kryptonSplitContainerVertical.Panel2MinSize = newHeight;
                kryptonSplitContainerVertical.SplitterDistance = kryptonSplitContainerVertical.Height;

                kryptonSplitContainerHorizontal.ResumeLayout();
            }

            // Is the left header group currently collapsed?
            if (kryptonSplitContainerHorizontal.FixedPanel == FixedPanel.Panel1)
            {
                // Suspend layout changes until all splitter properties have been updated
                kryptonSplitContainerVertical.SuspendLayout();

                // Get the new preferred width of the header group and apply it
                int newWidth = kryptonHeaderGroupLeft.PreferredSize.Width;
                kryptonSplitContainerHorizontal.Panel1MinSize = newWidth;
                kryptonSplitContainerHorizontal.SplitterDistance = newWidth;

                kryptonSplitContainerVertical.ResumeLayout();
            }
        }

        private void toolOffice2010_Click(object sender, EventArgs e)
        {
            if (!toolOffice2010.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                toolOffice2010.Checked = menuOffice2010.Checked = true;
                toolOffice2007.Checked = menuOffice2007.Checked = false;
                toolSystem.Checked = menuSystem.Checked = false;
                toolSparkle.Checked = menuSparkle.Checked = false;
                UpdateCollapsedSizing();
            }
        }

        private void toolOffice2007_Click(object sender, EventArgs e)
        {
            if (!toolOffice2007.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                toolOffice2010.Checked = menuOffice2010.Checked = false;
                toolOffice2007.Checked = menuOffice2007.Checked = true;
                toolSystem.Checked = menuSystem.Checked = false;
                toolSparkle.Checked = menuSparkle.Checked = false;
                UpdateCollapsedSizing();
            }
        }

        private void toolSystem_Click(object sender, EventArgs e)
        {
            if (!toolSystem.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
                toolOffice2010.Checked = menuOffice2010.Checked = false;
                toolOffice2007.Checked = menuOffice2007.Checked = false;
                toolSystem.Checked = menuSystem.Checked = true;
                toolSparkle.Checked = menuSparkle.Checked = false;
                UpdateCollapsedSizing();
            }
        }

        private void toolSparkle_Click(object sender, EventArgs e)
        {
            if (!toolSparkle.Checked)
            {
                kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                toolOffice2010.Checked = menuOffice2010.Checked = false;
                toolOffice2007.Checked = menuOffice2007.Checked = false;
                toolSystem.Checked = menuSystem.Checked = false;
                toolSparkle.Checked = menuSparkle.Checked = true;
                UpdateCollapsedSizing();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
