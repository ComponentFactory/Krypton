// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.5.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Win32;

namespace KryptonExplorer
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();

            kryptonNavigator.SelectedIndex = 0;
            kryptonNavigatorToolkit.SelectedIndex = 0;
        }

        private void linkKryptonBorderEdge_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonBorderEdgeExamples.exe"); }
            catch { }
        }

        private void linkKryptonButton_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonButtonExamples.exe"); }
            catch { }
        }

        private void linkKryptonCheckBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonCheckBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonCheckButton_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonCheckButtonExamples.exe"); }
            catch { }
        }

        private void linkKryptonDropButton_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonDropButtonExamples.exe"); }
            catch { }
        }

        private void linkKryptonColorButton_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonColorButtonExamples.exe"); }
            catch { }
        }

        private void linkKryptonCheckSet_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonCheckSetExamples.exe"); }
            catch { }
        }

        private void linkKryptonContextMenu_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonContextMenuExamples.exe"); }
            catch { }
        }

        private void linkKryptonDataGridView_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonDataGridViewExamples.exe"); }
            catch { }
        }

        private void linkKryptonForm_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonFormExamples.exe"); }
            catch { }
        }

        private void linkKryptonGroup_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonGroupExamples.exe"); }
            catch { }
        }

        private void linkKryptonGroupBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonGroupBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonHeader_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonHeaderExamples.exe"); }
            catch { }
        }

        private void linkKryptonHeaderGroup_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonHeaderGroupExamples.exe"); }
            catch { }
        }

        private void linkKryptonLabel_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonLabelExamples.exe"); }
            catch { }
        }

        private void linkKryptonWrapLabel_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonWrapLabelExamples.exe"); }
            catch { }
        }

        private void linkKryptonCommand_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonCommandExamples.exe"); }
            catch { }
        }

        private void linkKryptonLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonLinkLabelExamples.exe"); }
            catch { }
        }

        private void linkKryptonListBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonListBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonCheckedListBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonCheckedListBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonMaskedTextBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonMaskedTextBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonPalette_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonPaletteExamples.exe"); }
            catch { }
        }

        private void linkKryptonPanel_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonPanelExamples.exe"); }
            catch { }
        }

        private void linkKryptonSeparator_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonSeparatorExamples.exe"); }
            catch { }
        }

        private void linkKryptonRadioButton_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonRadioButtonExamples.exe"); }
            catch { }
        }

        private void linkKryptobTrackBar_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonTrackBarExamples.exe"); }
            catch { }
        }

        private void linkKryptonSplitContainer_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonSplitContainerExamples.exe"); }
            catch { }
        }

        private void linkKryptonComboBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonComboBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonTextBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonTextBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonRichTextBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonRichTextBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonNumericUpDown_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonNumericUpDownExamples.exe"); }
            catch { }
        }

        private void linkKryptonDomainUpDown_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonDomainUpDownExamples.exe"); }
            catch { }
        }

        private void linkKryptonBreadCrumb_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonBreadCrumbExamples.exe"); }
            catch { }
        }

        private void linkKryptonDateTimePicker_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonDateTimePickerExamples.exe"); }
            catch { }
        }

        private void linkKryptonMonthCalendar_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonMonthCalendarExamples.exe"); }
            catch { }
        }

        private void linkKryptonInputBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonInputBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonMessageBox_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonMessageBoxExamples.exe"); }
            catch { }
        }

        private void linkKryptonTaskDialog_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonTaskDialogExamples.exe"); }
            catch { }
        }

        private void linkKryptonTreeView_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonTreeViewExamples.exe"); }
            catch { }
        }

        private void linkInputForm_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\InputForm.exe"); }
            catch { }
        }

        private void linkThreePaneApplicationBasic_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ThreePaneApplicationBasic.exe"); }
            catch { }
        }

        private void linkThreePaneApplicationExtended_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ThreePaneApplicationExtended.exe"); }
            catch { }
        }

        private void linkMDIApplication_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\MDIApplication.exe"); }
            catch { }
        }

        private void linkCustomControlUsingPalettes_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\CustomControlUsingPalettes.exe"); }
            catch { }
        }

        private void linkCustomControlUsingRenderers_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\CustomControlUsingRenderers.exe"); }
            catch { }
        }

        private void linkExpandingSplitters_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ExpandingHeaderGroupsSplitters.exe"); }
            catch { }
        }

        private void linkExpandingDockStyle_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ExpandingHeaderGroupsDockStyle.exe"); }
            catch { }
        }

        private void linkExpandingHeaderStack_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ExpandingHeaderGroupsStack.exe"); }
            catch { }
        }

        private void linkChildControlStack_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ChildControlStack.exe"); }
            catch { }
        }

        private void linkLabelButtonSpecPlayground_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ButtonSpecPlayground.exe"); }
            catch { }
        }

        private void linkNavigatorModes_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\NavigatorModes.exe"); }
            catch { }
        }

        private void linkNavigatorPalettes_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\NavigatorPalettes.exe"); }
            catch { }
        }

        private void linkOrientationAndAlignment_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\OrientationPlusAlignment.exe"); }
            catch { }
        }

        private void linkSinglelineAndMultiline_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\SinglelinePlusMultiline.exe"); }
            catch { }
        }

        private void linkTabBorderStyles_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\TabBorderStyles.exe"); }
            catch { }
        }

        private void linkNavigatorPopupPages_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\PopupPages.exe"); }
            catch { }
        }

        private void linkNavigatorPerTabButtons_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\PerTabButtons.exe"); }
            catch { }
        }

        private void linkNavigatorTooltips_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\NavigatorToolTips.exe"); }
            catch { }
        }

        private void linkNavigatorContextMenus_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\NavigatorContextMenus.exe"); }
            catch { }
        }

        private void linkNavigatorPlayground_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\NavigatorPlayground.exe"); }
            catch { }
        }

        private void linkContextualTabs_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ContextualTabs.exe"); }
            catch { }
        }

        private void linkKeyTipsTabs_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KeyTipsAndKeyboardAccess.exe"); }
            catch { }
        }

        private void labelAutoShrinkingGroups_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\AutoShrinkingGroups.exe"); }
            catch { }
        }

        private void labelQuickAccessToolbar_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\QuickAccessToolbar.exe"); }
            catch { }
        }

        private void linkRibbonGallery_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\RibbonGallery.exe"); }
            catch { }
        }

        private void linkRibbonToolTips_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\RibbonToolTips.exe"); }
            catch { }
        }

        private void linkRibbonControls_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\RibbonControls.exe"); }
            catch { }
        }

        private void linkKryptonGallery_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonGalleryExamples.exe"); }
            catch { }
        }

        private void linkApplicationMenu_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ApplicationMenu.exe"); }
            catch { }
        }

        private void linkOutlookMailClone_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\OutlookMailClone.exe"); }
            catch { }
        }

        private void linkRibbonAndNavigator_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\RibbonAndNavigatorAndWorkspace.exe"); }
            catch { }
        }

        private void linkMDIRibbon_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\MDIRibbon.exe"); }
            catch { }
        }

        private void linkExpandingPages_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ExpandingPages.exe"); }
            catch { }
        }

        private void linkNavigatorBasicEvents_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\BasicEvents.exe"); }
            catch { }
        }

        private void linkNavigatorUserPageCreation_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\UserPageCreation.exe"); }
            catch { }
        }

        private void linkNavigatorOneNoteTabs_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\OneNoteTabs.exe"); }
            catch { }
        }

        private void linkNavigatorOutlookMockup_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\OutlookMockup.exe"); }
            catch { }
        }

        private void linkWorkspaceCellModes_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\WorkspaceCellModes.exe"); }
            catch { }
        }

        private void linkWorkspaceCellLayout_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\WorkspaceCellLayout.exe"); }
            catch { }
        }

        private void linkWorkspacePersistence_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\WorkspacePersistence.exe"); }
            catch { }
        }

        private void linkCellMaximizeAndRestore_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\CellMaximizeAndRestore.exe"); }
            catch { }
        }

        private void linkBasicPageDragAndDrop_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\BasicPageDragAndDrop.exe"); }
            catch { }
        }

        private void linkAdvancedPageDragAndDrop_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\AdvancedPageDragAndDrop.exe"); }
            catch { }
        }

        private void memoEditor_Clicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\MemoEditor.exe"); }
            catch { }
        }

        private void linkStandardDocking_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\StandardDocking.exe"); }
            catch { }
        }

        private void linkMultiControlDocking_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\MultiControlDocking.exe"); }
            catch { }
        }

        private void linkExternalDragToDocking_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\ExternalDragToDocking.exe"); }
            catch { }
        }


        private void linkNavigatorAndFloatingWindows_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\NavigatorAndFloatingWindows.exe"); }
            catch { }
        }

        private void linkDockingPersistence_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\DockingPersistence.exe"); }
            catch { }
        }

        private void linkDockingFlags_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\DockingFlags.exe"); }
            catch { }
        }

        private void linkDockingCustomized_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\DockingCustomized.exe"); }
            catch { }
        }

        private void linkPaletteDesigner_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\PaletteDesigner.exe"); }
            catch { }
        }

        private void linkPaletteUpgradeTool_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\PaletteUpgradeTool.exe"); }
            catch { }
        }

        private void linkSerialKeys_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@".\KryptonSerialKeys.exe"); }
            catch { }
        }

        private void linkToolkitChangeList_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@"..\Documents\Krypton Toolkit Change List.doc"); }
            catch { }
        }

        private void linkDockingChangeList_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@"..\Documents\Krypton Docking Change List.doc"); }
            catch { }
        }

        private void linkRibbonChangeList_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@"..\Documents\Krypton Ribbon Change List.doc"); }
            catch { }
        }

        private void linkNavigatorChangeList_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@"..\Documents\Krypton Navigator Change List.doc"); }
            catch { }
        }

        private void linkWorkspaceChangeList_LinkClicked(object sender, EventArgs e)
        {
            try { Process.Start(@"..\Documents\Krypton Workspace Change List.doc"); }
            catch { }
        }

        private void linkDocumentation_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                // Is the Microsoft Help Viewer installed (usually with Visual Studio 2010)
                RegistryKey keyMHV = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Help\v1.0");
                if (keyMHV != null)
                {
                    // Get the directory of the help viewer application
                    string appRoot = (string)keyMHV.GetValue("AppRoot");

                    // Look for the standalone help browser
                    if (File.Exists(appRoot + "HlpViewer.exe"))
                    {
                        Process.Start(appRoot + "HlpViewer.exe");
                        return;
                    }
                }

                // Find the Visual Studio 9 document explorer entry
                RegistryKey keyDExplore = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\CLSID\{1F69F884-285E-418E-9715-B9EEE402DD5F}\LocalServer32");

                // Fall back on the Visual Studio 8 document explorer entry
                if (keyDExplore == null)
                    keyDExplore = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\CLSID\{639F725F-1B2D-4831-A9FD-874847682010}\LocalServer32");

                // Assuming we found it on this machine
                if (keyDExplore != null)
                {
                    // Get the full path to the executable
                    string path = (string)keyDExplore.GetValue(string.Empty);

                    // If the path is valid then use it to show the krypton help collection
                    if (path.Length > 0)
                    {
                        Process.Start(path, @"/helpcol ms-help://Krypton");
                        return;
                    }
                }

                KryptonMessageBox.Show("Could not find the Microsoft Helper Viewer that is installed with Visual Studio 2010 SP1 or the Microsoft Document Explorer that is installed with Visual Studio 2005/2008.", 
                                       "Krypton Documentation", 
                                       MessageBoxButtons.OK);
            }
            catch { }
        }

        private void linkToolkitExamplesSource2010_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Toolkit Examples\Krypton Toolkit Examples 2010.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkToolkitExamples2Source2010_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Toolkit Examples\Krypton Toolkit Examples II 2010.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkDockingExamplesSource2010_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Docking Examples\Krypton Docking Examples 2010.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkWorkspaceExamplesSource2010_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Workspace Examples\Krypton Workspace Examples 2010.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkNavigatorExamplesSource2010_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Navigator Examples\Krypton Navigator Examples 2010.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkRibbonExamplesSource2010_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Ribbon Examples\Krypton Ribbon Examples 2010.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkToolkitExamplesSource2008_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Toolkit Examples\Krypton Toolkit Examples 2008.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkToolkitExamples2Source2008_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Toolkit Examples\Krypton Toolkit Examples II 2008.sln";

                Process.Start(path);
            }
            catch { }

        }

        private void linkDockingExamplesSource2008_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Docking Examples\Krypton Docking Examples 2008.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkRibbonExamplesSource2008_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Ribbon Examples\Krypton Ribbon Examples 2008.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkNavigatorExamplesSource2008_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Navigator Examples\Krypton Navigator Examples 2008.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkWorkspaceExamplesSource2008_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Workspace Examples\Krypton Workspace Examples 2008.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkToolkitExamplesSource2005_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Toolkit Examples\Krypton Toolkit Examples 2005.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkToolkitExamples2Source2005_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Toolkit Examples\Krypton Toolkit Examples II 2005.sln";

                Process.Start(path);
            }
            catch { }
        }


        private void linkDockingExamplesSource2005_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Docking Examples\Krypton Docking Examples 2005.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkRibbonExamplesSource2005_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Ribbon Examples\Krypton Ribbon Examples 2005.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkNavigatorExamplesSource2005_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Navigator Examples\Krypton Navigator Examples 2005.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkWorkspaceExamplesSource2005_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                              @"\Component Factory\Krypton\Source\Krypton Workspace Examples\Krypton Workspace Examples 2005.sln";

                Process.Start(path);
            }
            catch { }
        }

        private void linkBlog_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"http://www.componentfactory.com/blog/index.php");
            }
            catch { }
        }

        private void linkWebsite_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"http://www.componentfactory.com");
            }
            catch { }
        }

        private void linkScreencasts_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"http://www.componentfactory.com/screencasts.php");
            }
            catch { }
        }

        private void linkLabelForums_LinkClicked(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"http://www.componentfactory.com/forums/index.php");
            }
            catch { }
        }

        private void linkLabelPurchaseSuite_LinkClicked(object sender, EventArgs e)
        {
            try
            {
#if COMPONENTSOURCE
                Process.Start(@"http://www.componentsource.com/products/krypton-suite/index.html");
#else
                Process.Start(@"http://www.componentfactory.com/purchase.php");
#endif
            }
            catch { }
        }

        private void kryptonButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
