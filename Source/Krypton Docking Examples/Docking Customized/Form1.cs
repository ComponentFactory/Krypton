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
using ComponentFactory.Krypton.Ribbon;

namespace DockingCustomized
{
    public partial class Form1 : KryptonForm
    {
        private int _count = 1;
        private Random _random = new Random(DateTime.Now.Millisecond);
        private NavigatorMode _mode = NavigatorMode.HeaderBarCheckButtonHeaderGroup;
        private PaletteButtonSpecStyle[] _buttonSpecStyles = new PaletteButtonSpecStyle[]{ PaletteButtonSpecStyle.ArrowDown, PaletteButtonSpecStyle.ArrowLeft,
                                                                                           PaletteButtonSpecStyle.ArrowRight, PaletteButtonSpecStyle.ArrowUp,
                                                                                           PaletteButtonSpecStyle.Close, PaletteButtonSpecStyle.Context,
                                                                                           PaletteButtonSpecStyle.DropDown };

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

            // Add initial docking pages
            kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { NewDocument(), NewDocument() });
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Right, new KryptonPage[] { NewPropertyGrid(), NewInput(), NewPropertyGrid(), NewInput() });
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Bottom, new KryptonPage[] { NewInput(), NewPropertyGrid(), NewInput(), NewPropertyGrid() });

            UpdateModeButtons();
        }

        private void kryptonDockingManager_DockspaceAdding(object sender, DockspaceEventArgs e)
        {
            // Set all new dockspace controls to a larger than default size
            e.DockspaceControl.Size = new Size(250, 250);
        }

        private void kryptonDockingManager_FloatingWindowAdding(object sender, FloatingWindowEventArgs e)
        {
            // Set all new floating windows to a larger than default size
            e.FloatingWindow.ClientSize = new Size(400, 400);
        }

        private void kryptonDockingManager_DockspaceCellAdding(object sender, DockspaceCellEventArgs e)
        {
            Console.WriteLine("DockspaceCellAdding");

            // Set the correct appearance of the dockspace cell based on current settings
            UpdateCell(e.CellControl);
        }

        private void kryptonDockingManager_FloatspaceCellAdding(object sender, FloatspaceCellEventArgs e)
        {
            // Set the correct appearance of the floatspace cell based on current settings
            UpdateCell(e.CellControl);
        }

        private void kryptonDockingManager_ShowPageContextMenu(object sender, ContextPageEventArgs e)
        {
            // Create a set of custom menu items
            KryptonContextMenuItems customItems = new KryptonContextMenuItems();
            KryptonContextMenuSeparator customSeparator = new KryptonContextMenuSeparator();
            KryptonContextMenuItem customItem1 = new KryptonContextMenuItem("Custom Item 1", new EventHandler(OnCustomMenuItem));
            KryptonContextMenuItem customItem2 = new KryptonContextMenuItem("Custom Item 2", new EventHandler(OnCustomMenuItem));
            customItem1.Tag = e.Page;
            customItem2.Tag = e.Page;
            customItems.Items.AddRange(new KryptonContextMenuItemBase[] { customSeparator, customItem1, customItem2 });

            // Add set of custom items into the provided menu
            e.KryptonContextMenu.Items.Add(customItems);
        }

        private void kryptonDockingManager_ShowWorkspacePageContextMenu(object sender, ContextPageEventArgs e)
        {
            // Create a set of custom menu items
            KryptonContextMenuItems customItems = new KryptonContextMenuItems();
            KryptonContextMenuSeparator customSeparator = new KryptonContextMenuSeparator();
            KryptonContextMenuItem customItem1 = new KryptonContextMenuItem("Custom Item 3", new EventHandler(OnCustomMenuItem));
            KryptonContextMenuItem customItem2 = new KryptonContextMenuItem("Custom Item 4", new EventHandler(OnCustomMenuItem));
            customItem1.Tag = e.Page;
            customItem2.Tag = e.Page;
            customItems.Items.AddRange(new KryptonContextMenuItemBase[] { customSeparator, customItem1, customItem2 });

            // Add set of custom items into the provided menu
            e.KryptonContextMenu.Items.Add(customItems);
        }

        private void OnCustomMenuItem(object sender, EventArgs e)
        {
            KryptonContextMenuItem menuItem = (KryptonContextMenuItem)sender;
            KryptonPage page = (KryptonPage)menuItem.Tag;
            MessageBox.Show("Clicked menu option '" + menuItem.Text + "' for the page '" + page.Text + "'.", "Page Context Menu");
        }

        private void colorsRandom_Click(object sender, EventArgs e)
        {
            foreach (KryptonPage page in kryptonDockingManager.Pages)
            {
                page.StateNormal.Tab.Content.ShortText.Color1 = RandomColor();
                page.StateNormal.Tab.Back.Color1 = RandomColor();
                page.StateNormal.Tab.Back.ColorStyle = PaletteColorStyle.Solid;

                page.StateNormal.RibbonTab.TabDraw.TextColor = RandomColor();
                page.StateNormal.RibbonTab.TabDraw.BackColor1 = RandomColor();
                page.StateNormal.RibbonTab.TabDraw.BackColor2 = RandomColor();

                page.StateNormal.CheckButton.Content.ShortText.Color1 = RandomColor();
                page.StateNormal.CheckButton.Back.Color1 = RandomColor();
                page.StateNormal.CheckButton.Back.ColorStyle = PaletteColorStyle.Solid;
            }
        }

        private void colorsReset_Click(object sender, EventArgs e)
        {
            foreach (KryptonPage page in kryptonDockingManager.Pages)
            {
                page.StateNormal.Tab.Content.ShortText.Color1 = Color.Empty;
                page.StateNormal.Tab.Back.Color1 = Color.Empty;
                page.StateNormal.Tab.Back.ColorStyle = PaletteColorStyle.Inherit;

                page.StateNormal.RibbonTab.TabDraw.TextColor = Color.Empty;
                page.StateNormal.RibbonTab.TabDraw.BackColor1 = Color.Empty;
                page.StateNormal.RibbonTab.TabDraw.BackColor2 = Color.Empty;

                page.StateNormal.CheckButton.Content.ShortText.Color1 = Color.Empty;
                page.StateNormal.CheckButton.Back.Color1 = Color.Empty;
                page.StateNormal.CheckButton.Back.ColorStyle = PaletteColorStyle.Inherit;
            }
        }

        private void buttonSpecsAdd_Click(object sender, EventArgs e)
        {
            foreach (KryptonPage page in kryptonDockingManager.Pages)
            {
                // Create a button spec and make it a random style so we get a random image
                ButtonSpecAny bs = new ButtonSpecAny();
                bs.Type = _buttonSpecStyles[_random.Next(_buttonSpecStyles.Length)];
                page.ButtonSpecs.Add(bs);
            }
        }

        private void buttonSpecsClear_Click(object sender, EventArgs e)
        {
            foreach (KryptonPage page in kryptonDockingManager.Pages)
                page.ButtonSpecs.Clear();
        }

        private void kryptonRibbonModeButton_Click(object sender, EventArgs e)
        {
            // Extract the navigator mode from the tag field of the ribbon button
            KryptonRibbonGroupButton button = (KryptonRibbonGroupButton)sender;
            _mode = (NavigatorMode)Enum.Parse(typeof(NavigatorMode), (string)button.Tag);

            UpdateModeButtons();
            UpdateAllCells();
        }

        private void UpdateModeButtons()
        {
            modeHeaderGroupTab.Checked = (_mode == NavigatorMode.HeaderGroupTab);
            modeHeaderBarHeaderGroup.Checked = (_mode == NavigatorMode.HeaderBarCheckButtonHeaderGroup);
            modeHeaderBarGroup.Checked = (_mode == NavigatorMode.HeaderBarCheckButtonGroup);
            modeTabGroup.Checked = (_mode == NavigatorMode.BarTabGroup);
            modeBarGroupInside.Checked = (_mode == NavigatorMode.BarCheckButtonGroupInside);
            modeBarGroupOutside.Checked = (_mode == NavigatorMode.BarCheckButtonGroupOutside);
            modeBarRibbonTabGroup.Checked = (_mode == NavigatorMode.BarRibbonTabGroup);
            modeStackGroup.Checked = (_mode == NavigatorMode.StackCheckButtonGroup);
            modeStackHeaderGroup.Checked = (_mode == NavigatorMode.StackCheckButtonHeaderGroup);
        }

        private void UpdateAllCells()
        {
            foreach (KryptonWorkspaceCell cell in kryptonDockingManager.CellsDocked)
                UpdateCell(cell);

            foreach (KryptonWorkspaceCell cell in kryptonDockingManager.CellsFloating)
                UpdateCell(cell);
        }

        private void UpdateCell(KryptonWorkspaceCell cell)
        {
            cell.NavigatorMode = _mode;
        }

        private Color RandomColor()
        {
            return Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255));
        }
    }
}
