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
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
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
            Dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public string Dir { get; }

        private void run(string filename)
        {
            var file = Path.Combine(Dir, filename);
            if (!File.Exists(file))
              throw new FileNotFoundException(string.Format("File not found: {0}", file));

            try { Process.Start(file); }
            catch (Exception e)
            {
              KryptonMessageBox.Show(string.Format("Error running {0}\n{1}", filename, e.Message));
            }
        }

        private void linkKryptonBorderEdge_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonBorderEdgeExamples.exe");
        }

        private void linkKryptonButton_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonButtonExamples.exe");
        }

        private void linkKryptonCheckBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonCheckBoxExamples.exe");
        }

        private void linkKryptonCheckButton_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonCheckButtonExamples.exe");
        }

        private void linkKryptonDropButton_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonDropButtonExamples.exe");
        }

        private void linkKryptonColorButton_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonColorButtonExamples.exe");
        }

        private void linkKryptonCheckSet_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonCheckSetExamples.exe");
        }

        private void linkKryptonContextMenu_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonContextMenuExamples.exe");
        }

        private void linkKryptonDataGridView_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonDataGridViewExamples.exe");
        }

        private void linkKryptonForm_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonFormExamples.exe");
        }

        private void linkKryptonGroup_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonGroupExamples.exe");
        }

        private void linkKryptonGroupBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonGroupBoxExamples.exe");
        }

        private void linkKryptonHeader_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonHeaderExamples.exe");
        }

        private void linkKryptonHeaderGroup_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonHeaderGroupExamples.exe");
        }

        private void linkKryptonLabel_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonLabelExamples.exe");
        }

        private void linkKryptonWrapLabel_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonWrapLabelExamples.exe");
        }

        private void linkKryptonCommand_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonCommandExamples.exe");
        }

        private void linkKryptonLinkLabel_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonLinkLabelExamples.exe");
        }

        private void linkKryptonListBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonListBoxExamples.exe");
        }

        private void linkKryptonCheckedListBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonCheckedListBoxExamples.exe");
        }

        private void linkKryptonMaskedTextBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonMaskedTextBoxExamples.exe");
        }

        private void linkKryptonPalette_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonPaletteExamples.exe");
        }

        private void linkKryptonPanel_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonPanelExamples.exe");
        }

        private void linkKryptonSeparator_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonSeparatorExamples.exe");
        }

        private void linkKryptonRadioButton_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonRadioButtonExamples.exe");
        }

        private void linkKryptobTrackBar_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonTrackBarExamples.exe");
        }

        private void linkKryptonSplitContainer_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonSplitContainerExamples.exe");
        }

        private void linkKryptonComboBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonComboBoxExamples.exe");
        }

        private void linkKryptonTextBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonTextBoxExamples.exe");
        }

        private void linkKryptonRichTextBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonRichTextBoxExamples.exe");
        }

        private void linkKryptonNumericUpDown_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonNumericUpDownExamples.exe");
        }

        private void linkKryptonDomainUpDown_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonDomainUpDownExamples.exe");
        }

        private void linkKryptonBreadCrumb_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonBreadCrumbExamples.exe");
        }

        private void linkKryptonDateTimePicker_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonDateTimePickerExamples.exe");
        }

        private void linkKryptonMonthCalendar_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonMonthCalendarExamples.exe");
        }

        private void linkKryptonInputBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonInputBoxExamples.exe");
        }

        private void linkKryptonMessageBox_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonMessageBoxExamples.exe");
        }

        private void linkKryptonTaskDialog_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonTaskDialogExamples.exe");
        }

        private void linkKryptonTreeView_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonTreeViewExamples.exe");
        }

        private void linkInputForm_LinkClicked(object sender, EventArgs e)
        {
            run("InputForm.exe");
        }

        private void linkThreePaneApplicationBasic_LinkClicked(object sender, EventArgs e)
        {
            run("ThreePaneApplicationBasic.exe");
        }

        private void linkThreePaneApplicationExtended_LinkClicked(object sender, EventArgs e)
        {
            run("ThreePaneApplicationExtended.exe");
        }

        private void linkMDIApplication_LinkClicked(object sender, EventArgs e)
        {
            run("MDIApplication.exe");
        }

        private void linkCustomControlUsingPalettes_LinkClicked(object sender, EventArgs e)
        {
            run("CustomControlUsingPalettes.exe");
        }

        private void linkCustomControlUsingRenderers_LinkClicked(object sender, EventArgs e)
        {
            run("CustomControlUsingRenderers.exe");
        }

        private void linkExpandingSplitters_LinkClicked(object sender, EventArgs e)
        {
            run("ExpandingHeaderGroupsSplitters.exe");
        }

        private void linkExpandingDockStyle_LinkClicked(object sender, EventArgs e)
        {
            run("ExpandingHeaderGroupsDockStyle.exe");
        }

        private void linkExpandingHeaderStack_LinkClicked(object sender, EventArgs e)
        {
            run("ExpandingHeaderGroupsStack.exe");
        }

        private void linkChildControlStack_LinkClicked(object sender, EventArgs e)
        {
            run("ChildControlStack.exe");
        }

        private void linkLabelButtonSpecPlayground_LinkClicked(object sender, EventArgs e)
        {
            run("ButtonSpecPlayground.exe");
        }

        private void linkNavigatorModes_LinkClicked(object sender, EventArgs e)
        {
            run("NavigatorModes.exe");
        }

        private void linkNavigatorPalettes_LinkClicked(object sender, EventArgs e)
        {
            run("NavigatorPalettes.exe");
        }

        private void linkOrientationAndAlignment_LinkClicked(object sender, EventArgs e)
        {
            run("OrientationPlusAlignment.exe");
        }

        private void linkSinglelineAndMultiline_LinkClicked(object sender, EventArgs e)
        {
            run("SinglelinePlusMultiline.exe");
        }

        private void linkTabBorderStyles_LinkClicked(object sender, EventArgs e)
        {
            run("TabBorderStyles.exe");
        }

        private void linkNavigatorPopupPages_LinkClicked(object sender, EventArgs e)
        {
            run("PopupPages.exe");
        }

        private void linkNavigatorPerTabButtons_LinkClicked(object sender, EventArgs e)
        {
            run("PerTabButtons.exe");
        }

        private void linkNavigatorTooltips_LinkClicked(object sender, EventArgs e)
        {
            run("NavigatorToolTips.exe");
        }

        private void linkNavigatorContextMenus_LinkClicked(object sender, EventArgs e)
        {
            run("NavigatorContextMenus.exe");
        }

        private void linkNavigatorPlayground_LinkClicked(object sender, EventArgs e)
        {
            run("NavigatorPlayground.exe");
        }

        private void linkContextualTabs_LinkClicked(object sender, EventArgs e)
        {
            run("ContextualTabs.exe");
        }

        private void linkKeyTipsTabs_LinkClicked(object sender, EventArgs e)
        {
            run("KeyTipsAndKeyboardAccess.exe");
        }

        private void labelAutoShrinkingGroups_LinkClicked(object sender, EventArgs e)
        {
            run("AutoShrinkingGroups.exe");
        }

        private void labelQuickAccessToolbar_LinkClicked(object sender, EventArgs e)
        {
            run("QuickAccessToolbar.exe");
        }

        private void linkRibbonGallery_LinkClicked(object sender, EventArgs e)
        {
            run("RibbonGallery.exe");
        }

        private void linkRibbonToolTips_LinkClicked(object sender, EventArgs e)
        {
            run("RibbonToolTips.exe");
        }

        private void linkRibbonControls_LinkClicked(object sender, EventArgs e)
        {
            run("RibbonControls.exe");
        }

        private void linkKryptonGallery_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonGalleryExamples.exe");
        }

        private void linkApplicationMenu_LinkClicked(object sender, EventArgs e)
        {
            run("ApplicationMenu.exe");
        }

        private void linkOutlookMailClone_LinkClicked(object sender, EventArgs e)
        {
            run("OutlookMailClone.exe");
        }

        private void linkRibbonAndNavigator_LinkClicked(object sender, EventArgs e)
        {
            run("RibbonAndNavigatorAndWorkspace.exe");
        }

        private void linkMDIRibbon_LinkClicked(object sender, EventArgs e)
        {
            run("MDIRibbon.exe");
        }

        private void linkExpandingPages_LinkClicked(object sender, EventArgs e)
        {
            run("ExpandingPages.exe");
        }

        private void linkNavigatorBasicEvents_LinkClicked(object sender, EventArgs e)
        {
            run("BasicEvents.exe");
        }

        private void linkNavigatorUserPageCreation_LinkClicked(object sender, EventArgs e)
        {
            run("UserPageCreation.exe");
        }

        private void linkNavigatorOneNoteTabs_LinkClicked(object sender, EventArgs e)
        {
            run("OneNoteTabs.exe");
        }

        private void linkNavigatorOutlookMockup_LinkClicked(object sender, EventArgs e)
        {
            run("OutlookMockup.exe");
        }

        private void linkWorkspaceCellModes_LinkClicked(object sender, EventArgs e)
        {
            run("WorkspaceCellModes.exe");
        }

        private void linkWorkspaceCellLayout_LinkClicked(object sender, EventArgs e)
        {
            run("WorkspaceCellLayout.exe");
        }

        private void linkWorkspacePersistence_LinkClicked(object sender, EventArgs e)
        {
            run("WorkspacePersistence.exe");
        }

        private void linkCellMaximizeAndRestore_LinkClicked(object sender, EventArgs e)
        {
            run("CellMaximizeAndRestore.exe");
        }

        private void linkBasicPageDragAndDrop_LinkClicked(object sender, EventArgs e)
        {
            run("BasicPageDragAndDrop.exe");
        }

        private void linkAdvancedPageDragAndDrop_LinkClicked(object sender, EventArgs e)
        {
            run("AdvancedPageDragAndDrop.exe");
        }

        private void memoEditor_Clicked(object sender, EventArgs e)
        {
            run("MemoEditor.exe");
        }

        private void linkStandardDocking_LinkClicked(object sender, EventArgs e)
        {
            run("StandardDocking.exe");
        }

        private void linkMultiControlDocking_LinkClicked(object sender, EventArgs e)
        {
            run("MultiControlDocking.exe");
        }

        private void linkExternalDragToDocking_LinkClicked(object sender, EventArgs e)
        {
            run("ExternalDragToDocking.exe");
        }


        private void linkNavigatorAndFloatingWindows_LinkClicked(object sender, EventArgs e)
        {
            run("NavigatorAndFloatingWindows.exe");
        }

        private void linkDockingPersistence_LinkClicked(object sender, EventArgs e)
        {
            run("DockingPersistence.exe");
        }

        private void linkDockingFlags_LinkClicked(object sender, EventArgs e)
        {
            run("DockingFlags.exe");
        }

        private void linkDockingCustomized_LinkClicked(object sender, EventArgs e)
        {
            run("DockingCustomized.exe");
        }

        private void linkPaletteDesigner_LinkClicked(object sender, EventArgs e)
        {
            run("PaletteDesigner.exe");
        }

        private void linkSerialKeys_LinkClicked(object sender, EventArgs e)
        {
            run("KryptonSerialKeys.exe");
        }

        private void linkToolkitChangeList_LinkClicked(object sender, EventArgs e)
        {
            run(@"..\Documents\Krypton Toolkit Change List.doc");
        }

        private void linkDockingChangeList_LinkClicked(object sender, EventArgs e)
        {
            run(@"..\Documents\Krypton Docking Change List.doc");
        }

        private void linkRibbonChangeList_LinkClicked(object sender, EventArgs e)
        {
            run(@"..\Documents\Krypton Ribbon Change List.doc");
        }

        private void linkNavigatorChangeList_LinkClicked(object sender, EventArgs e)
        {
            run(@"..\Documents\Krypton Navigator Change List.doc");
        }

        private void linkWorkspaceChangeList_LinkClicked(object sender, EventArgs e)
        {
            run(@"..\Documents\Krypton Workspace Change List.doc");
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
