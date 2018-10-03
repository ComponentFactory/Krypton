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

namespace PaletteDesigner
{
    public partial class MainForm : KryptonForm
    {
        #region Instance Fields
        private bool _dirty;
        private bool _loaded;
        private string _filename;
        private KryptonPalette _palette;
        private FormChromeTMS _chromeTMS;
        private FormChromeRibbon _chromeRibbon;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the Form1 class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Operations
        private void New()
        {
            // If the current palette has been changed
            if (_dirty)
            {
                // Ask user if the current palette should be saved
                switch (MessageBox.Show(this,
                                        "Save changes to the current palette?",
                                        "Palette Changed",
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        // Use existing save method
                        Save();
                        break;
                    case DialogResult.Cancel:
                        // Cancel out entirely
                        return;
                }
            }

            // Generate a fresh palette from scratch
            CreateNewPalette();
        }

        private void Open()
        {
            // If the current palette has been changed
            if (_dirty)
            {
                // Ask user if the current palette should be saved
                switch (MessageBox.Show(this,
                                        "Save changes to the current palette?",
                                        "Palette Changed",
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        // Use existing save method
                        Save();
                        break;
                    case DialogResult.Cancel:
                        // Cancel out entirely
                        return;
                }
            }

            // Create a fresh palette instance for loading into
            KryptonPalette palette = new KryptonPalette();

            // Get the name of the file we imported from
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            string filename = palette.Import();
            Cursor = Cursors.Default;

            // If the load succeeded
            if (filename.Length > 0)
            {
                // Need to unhook from any existing palette
                if (_palette != null)
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

                // Use the new instance instead
                _palette = palette;
                _chromeTMS.Palette = _palette;
                _chromeRibbon.OverridePalette = _palette;

                // We need to know when a change occurs to the palette settings
                _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

                // Hook up the property grid to the palette
                labelGridNormal.SelectedObject = _palette;

                // Use the loaded filename
                _filename = filename;

                // Reset the state flags
                _loaded = true;
                _dirty = false;

                // Apply the new palette to the design controls
                ApplyPalette();

                // Define the initial title bar string
                UpdateTitlebar();
            }
        }

        private void Save()
        {
            // If we already have a file associated with the palette...
            if (_loaded)
            {
                // ...then just save it straight away
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                _palette.Export(_filename, true, false);
                Cursor = Cursors.Default;

                // No longer dirty
                _dirty = false;

                // Define the initial title bar string
                UpdateTitlebar();
            }
            else
            {
                // No association and so perform a save as instead
                SaveAs();
            }
        }

        private void SaveAs()
        {
            // Get back the filename selected by the user
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            string filename = _palette.Export();
            Cursor = Cursors.Default;

            // If the save succeeded
            if (filename.Length > 0)
            {
                // Remember associated file details
                _filename = filename;
                _loaded = true;

                // No longer dirty
                _dirty = false;

                // Define the initial title bar string
                UpdateTitlebar();
            }
        }

        private void Exit()
        {
            // If the current palette has been changed
            if (_dirty)
            {
                // Ask user if the current palette should be saved
                switch (MessageBox.Show(this,
                                        "Save changes to the current palette?",
                                        "Palette Changed",
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Warning))
                {
                    case DialogResult.Yes:
                        // Use existing save method
                        Save();
                        break;
                    case DialogResult.Cancel:
                        // Cancel out entirely
                        return;
                }
            }

            Close();
        }
        #endregion

        #region Palettes
        private void CreateNewPalette()
        {
            // Need to unhook from any existing palette
            if (_palette != null)
                _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            // Create a fresh palette instance
            _palette = new KryptonPalette();
            _chromeTMS.Palette = _palette;
            _chromeRibbon.OverridePalette = _palette;

            // We need to know when a change occurs to the palette settings
            _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPalettePaint);

            // Hook up the property grid to the palette
            labelGridNormal.SelectedObject = _palette;

            // Does not have a filename as yet
            _filename = "(New Palette)";

            // Reset the state flags
            _dirty = false;
            _loaded = false;

            // Apply the new palette to the design controls
            ApplyPalette();

            // Define the initial title bar string
            UpdateTitlebar();
        }

        private void ApplyPalette()
        {
            buttonSpecT1.Palette = _palette;
            buttonSpecT2.Palette = _palette;
            buttonSpecT3.Palette = _palette;
            buttonSpecT4.Palette = _palette;
            buttonSpecG1.Palette = _palette;
            buttonSpecG2.Palette = _palette;
            buttonSpecG3.Palette = _palette;
            buttonSpecG4.Palette = _palette;
            buttonDisabled.Palette = _palette;
            buttonDefaultFocus.Palette = _palette;
            buttonNormal.Palette = _palette;
            buttonTracking.Palette = _palette;
            buttonPressed.Palette = _palette;
            buttonCheckedNormal.Palette = _palette;
            buttonCheckedTracking.Palette = _palette;
            buttonCheckedPressed.Palette = _palette;
            buttonLive.Palette = _palette;
            control1Disabled.Palette = _palette;
            control1Normal.Palette = _palette;
            textBoxDisabled.Palette = _palette;
            textBoxNormal.Palette = _palette;
            textBoxActive.Palette = _palette;
            comboBoxDisabled.Palette = _palette;
            comboBoxNormal.Palette = _palette;
            comboBoxActive.Palette = _palette;
            numericDisabled.Palette = _palette;
            numericNormal.Palette = _palette;
            numericActive.Palette = _palette;
            headerGroup1Disabled.Palette = _palette;
            headerGroup1Normal.Palette = _palette;
            header1Disabled.Palette = _palette;
            header1Normal.Palette = _palette;
            cbLive.Palette = _palette;
            cbFocus.Palette = _palette;
            cbUncheckedDisabled.Palette = _palette;
            cbUncheckedNormal.Palette = _palette;
            cbUncheckedTracking.Palette = _palette;
            cbUncheckedPressed.Palette = _palette;
            cbCheckedDisabled.Palette = _palette;
            cbCheckedNormal.Palette = _palette;
            cbCheckedTracking.Palette = _palette;
            cbCheckedPressed.Palette = _palette;
            cbIndeterminateDisabled.Palette = _palette;
            cbIndeterminateNormal.Palette = _palette;
            cbIndeterminateTracking.Palette = _palette;
            cbIndeterminatePressed.Palette = _palette;
            rbFocus.Palette = _palette;
            rbLive1.Palette = _palette;
            rbLive2.Palette = _palette;
            rbCheckedNormal.Palette = _palette;
            rbCheckedTracking.Palette = _palette;
            rbCheckedPressed.Palette = _palette;
            rbUncheckedDisabled.Palette = _palette;
            rbUncheckedNormal.Palette = _palette;
            rbUncheckedTracking.Palette = _palette;
            rbUncheckedPressed.Palette = _palette;
            label1Disabled.Palette = _palette;
            label1Normal.Palette = _palette;
            label1Visited.Palette = _palette;
            label1NotVisited.Palette = _palette;
            label1Pressed.Palette = _palette;
            label1Live.Palette = _palette;
            panelLabelsBackground.Palette = _palette;
            kryptonNavigatorTabs.Palette = _palette;
            kryptonNavigator.Palette = _palette;
            panel1Disabled.Palette = _palette;
            panel1Normal.Palette = _palette;
            separator1Disabled.Palette = _palette;
            separator1Normal.Palette = _palette;
            separator1Tracking.Palette = _palette;
            separator1Pressed.Palette = _palette;
            separator1Live.Palette = _palette;
            dataGridViewDisabled.Palette = _palette;
            dataGridViewNormal.Palette = _palette;
            monthCalendarEnabled.Palette = _palette;
            monthCalendarDisabled.Palette = _palette;
            kryptonTrackBar1.Palette = _palette;
            kryptonTrackBar2.Palette = _palette;
            kryptonTrackBar3.Palette = _palette;
            kryptonTrackBar4.Palette = _palette;
            kryptonTrackBar5.Palette = _palette;
            kryptonTrackBar6.Palette = _palette;
            kryptonTrackBar7.Palette = _palette;
            kryptonTrackBar8.Palette = _palette;
            UpdateChromeTMS();
        }

        private void UpdateChromeTMS()
        {
            // Get the global renderer
            IRenderer renderer = _palette.GetRenderer();

            // Get the new toolstrip renderer based on the design palette
            _chromeTMS.OverrideToolStripRenderer = renderer.RenderToolStrip(_palette);
        }

        void OnPalettePaint(object sender, PaletteLayoutEventArgs e)
        {
            // Only interested the first time the palette is changed
            if (!_dirty)
            {
                _dirty = true;
                UpdateTitlebar();
            }

            // Do we need to setup a new renderer for the ToolMenuStatus page?
            if (e.NeedColorTable)
                UpdateChromeTMS();
        }
        #endregion

        #region Event Handlers
        private void Form1_Load(object sender, EventArgs e)
        {
            // Populate the sample data set
            dataTable1.Rows.Add("One", "Two", "Three");
            dataTable1.Rows.Add("Uno", "Dos", "Tres");
            dataTable1.Rows.Add("Un", "Deux", "Trios");

            // Add the chrome window to the Chrome + Strips page
            _chromeTMS = new FormChromeTMS();
            _chromeTMS.TopLevel = false;
            _chromeTMS.Parent = pageDesignChromeTMS;
            _chromeTMS.Dock = DockStyle.Fill;
            _chromeTMS.InertForm = true;
            _chromeTMS.Show();

            // Add the chrome window with embedded Ribbon
            _chromeRibbon = new FormChromeRibbon();
            _chromeRibbon.TopLevel = false;
            _chromeRibbon.Parent = pageDesignRibbon;
            _chromeRibbon.Dock = DockStyle.Fill;
            _chromeRibbon.InertForm = true;
            _chromeRibbon.Show();

            // Button fixed states
            buttonDisabled.SetFixedState(PaletteState.Disabled);
            buttonDefaultFocus.SetFixedState(PaletteState.NormalDefaultOverride);
            buttonNormal.SetFixedState(PaletteState.Normal);
            buttonTracking.SetFixedState(PaletteState.Tracking);
            buttonPressed.SetFixedState(PaletteState.Pressed);
            buttonCheckedNormal.SetFixedState(PaletteState.CheckedNormal);
            buttonCheckedTracking.SetFixedState(PaletteState.CheckedTracking);
            buttonCheckedPressed.SetFixedState(PaletteState.CheckedPressed);

            // CheckBox fixed states
            cbFocus.SetFixedState(true, true, false, false);
            cbUncheckedDisabled.SetFixedState(false, false, false, false);
            cbUncheckedNormal.SetFixedState(false, true, false, false);
            cbUncheckedTracking.SetFixedState(false, true, true, false);
            cbUncheckedPressed.SetFixedState(false, true, false, true);
            cbCheckedDisabled.SetFixedState(false, false, false, false);
            cbCheckedNormal.SetFixedState(false, true, false, false);
            cbCheckedTracking.SetFixedState(false, true, true, false);
            cbCheckedPressed.SetFixedState(false, true, false, true);
            cbIndeterminateDisabled.SetFixedState(false, false, false, false);
            cbIndeterminateNormal.SetFixedState(false, true, false, false);
            cbIndeterminateTracking.SetFixedState(false, true, true, false);
            cbIndeterminatePressed.SetFixedState(false, true, false, true);

            // RadioButton fixed states
            rbFocus.SetFixedState(true, true, false, false);
            rbCheckedDisabled.SetFixedState(false, false, false, false);
            rbCheckedNormal.SetFixedState(false, true, false, false);
            rbCheckedTracking.SetFixedState(false, true, true, false);
            rbCheckedPressed.SetFixedState(false, true, false, true);
            rbUncheckedDisabled.SetFixedState(false, false, false, false);
            rbUncheckedNormal.SetFixedState(false, true, false, false);
            rbUncheckedTracking.SetFixedState(false, true, true, false);
            rbUncheckedPressed.SetFixedState(false, true, false, true);

            // Control fixed states
            control1Disabled.SetFixedState(PaletteState.Disabled);
            control1Normal.SetFixedState(PaletteState.Normal);

            // HeaderGroup fixed states
            headerGroup1Disabled.SetFixedState(PaletteState.Disabled);
            headerGroup1Normal.SetFixedState(PaletteState.Normal);

            // Headers fixed states
            header1Disabled.SetFixedState(PaletteState.Disabled);
            header1Normal.SetFixedState(PaletteState.Normal);

            // Input controls fixed states
            textBoxNormal.SetFixedState(false);
            textBoxActive.SetFixedState(true);
            comboBoxNormal.SetFixedState(false);
            comboBoxActive.SetFixedState(true);
            numericNormal.SetFixedState(false);
            numericActive.SetFixedState(true);

            // Labels fixed states
            label1Disabled.SetFixedState(PaletteState.Disabled);
            label1Normal.SetFixedState(PaletteState.Normal);
            label1Visited.SetFixedState(PaletteState.Normal);
            label1NotVisited.SetFixedState(PaletteState.Normal);
            label1Pressed.SetFixedState(PaletteState.Pressed);

            // Panel fixed states
            panel1Disabled.SetFixedState(PaletteState.Disabled);
            panel1Normal.SetFixedState(PaletteState.Normal);

            // Separator fixed states
            separator1Disabled.SetFixedState(PaletteState.Disabled, PaletteState.Disabled);
            separator1Normal.SetFixedState(PaletteState.Normal, PaletteState.Normal);
            separator1Tracking.SetFixedState(PaletteState.Normal, PaletteState.Tracking);
            separator1Pressed.SetFixedState(PaletteState.Normal, PaletteState.Pressed);

            // TrackBar fixed states
            kryptonTrackBar1.SetFixedState(PaletteState.Normal);
            kryptonTrackBar5.SetFixedState(PaletteState.Normal);
            kryptonTrackBar2.SetFixedState(PaletteState.Tracking);
            kryptonTrackBar6.SetFixedState(PaletteState.Tracking);
            kryptonTrackBar3.SetFixedState(PaletteState.Pressed);
            kryptonTrackBar7.SetFixedState(PaletteState.Pressed);
            kryptonTrackBar4.SetFixedState(PaletteState.Disabled);
            kryptonTrackBar8.SetFixedState(PaletteState.Disabled);

            // Remove the context menu from the design navigator, we only show this
            // during design time to make it easier to switch around pages for updating
            // the design. At runtime it should always be in sync with the top navigator.
            kryptonNavigatorDesign.Button.ButtonDisplayLogic = ButtonDisplayLogic.None;

            // Define initial display pages
            kryptonNavigatorTop.SelectedPage = pageTopButtons;
            kryptonNavigatorDesign.SelectedPage = pageDesignButtons;

            CreateNewPalette();
        }

        private void menuNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void kryptonNavigatorTop_SelectedPageChanged(object sender, EventArgs e)
        {
            // Reflect change in the design navigator
            kryptonNavigatorDesign.SelectedIndex = kryptonNavigatorTop.SelectedIndex;
        }

        private void kryptonNavigatorDesign_SelectedPageChanged(object sender, EventArgs e)
        {
            // Reflect change in the top navigator
            kryptonNavigatorTop.SelectedIndex = kryptonNavigatorDesign.SelectedIndex;
        }

        private void kryptonNavigatorDesignButtons_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignButtons.TextTitle = kryptonNavigatorDesignButtons.SelectedPage.Text;
            pageDesignButtons.TextDescription = kryptonNavigatorDesignButtons.SelectedPage.TextDescription;

            ButtonStyle bs;

            // Work out the button style to be used
            switch(kryptonNavigatorDesignButtons.SelectedIndex)
            {
                default:
                case 0:
                    bs = ButtonStyle.Standalone;
                    break;
                case 1:
                    bs = ButtonStyle.Alternate;
                    break;
                case 2:
                    bs = ButtonStyle.LowProfile;
                    break;
                case 3:
                    bs = ButtonStyle.BreadCrumb;
                    break;
                case 4:
                    bs = ButtonStyle.CalendarDay;
                    break;
                case 5:
                    bs = ButtonStyle.ButtonSpec;
                    break;
                case 6:
                    bs = ButtonStyle.Cluster;
                    break;
                case 7:
                    bs = ButtonStyle.NavigatorStack;
                    break;
                case 8:
                    bs = ButtonStyle.NavigatorOverflow;
                    break;
                case 9:
                    bs = ButtonStyle.NavigatorMini;
                    break;
                case 10:
                    bs = ButtonStyle.InputControl;
                    break;
                case 11:
                    bs = ButtonStyle.ListItem;
                    break;
                case 12:
                    bs = ButtonStyle.Gallery;
                    break;
                case 13:
                    bs = ButtonStyle.Form;
                    break;
                case 14:
                    bs = ButtonStyle.FormClose;
                    break;
                case 15:
                    bs = ButtonStyle.Command;
                    break;
                case 16:
                    bs = ButtonStyle.Custom1;
                    break;
                case 17:
                    bs = ButtonStyle.Custom2;
                    break;
                case 18:
                    bs = ButtonStyle.Custom3;
                    break;
            }

            // Update all the displayed buttons with the new style
            buttonDisabled.ButtonStyle = bs;
            buttonDefaultFocus.ButtonStyle = bs;
            buttonNormal.ButtonStyle = bs;
            buttonTracking.ButtonStyle = bs;
            buttonPressed.ButtonStyle = bs;
            buttonCheckedNormal.ButtonStyle = bs;
            buttonCheckedTracking.ButtonStyle = bs;
            buttonCheckedPressed.ButtonStyle = bs;
            buttonLive.ButtonStyle = bs;
        }

        private void kryptonNavigatorDesignControls_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignControls.TextTitle = kryptonNavigatorDesignControls.SelectedPage.Text;
            pageDesignControls.TextDescription = kryptonNavigatorDesignControls.SelectedPage.TextDescription;

            PaletteBackStyle backStyle;
            PaletteBorderStyle borderStyle;
            
            // Work out the group styles to be used
            switch (kryptonNavigatorDesignControls.SelectedIndex)
            {
                default:
                case 0:
                    backStyle = PaletteBackStyle.ControlClient;
                    borderStyle = PaletteBorderStyle.ControlClient;
                    break;
                case 1:
                    backStyle = PaletteBackStyle.ControlAlternate;
                    borderStyle = PaletteBorderStyle.ControlAlternate;
                    break;
                case 2:
                    backStyle = PaletteBackStyle.ControlGroupBox;
                    borderStyle = PaletteBorderStyle.ControlGroupBox;
                    break;
                case 3:
                    backStyle = PaletteBackStyle.ControlToolTip;
                    borderStyle = PaletteBorderStyle.ControlToolTip;
                    break;
                case 4:
                    backStyle = PaletteBackStyle.ControlRibbon;
                    borderStyle = PaletteBorderStyle.ControlRibbon;
                    break;
                case 5:
                    backStyle = PaletteBackStyle.ControlCustom1;
                    borderStyle = PaletteBorderStyle.ControlCustom1;
                    break;
            }

            // Update all the displayed controls with the new styles
            control1Disabled.GroupBackStyle = backStyle;
            control1Disabled.GroupBorderStyle = borderStyle;
            control1Normal.GroupBackStyle = backStyle;
            control1Normal.GroupBorderStyle = borderStyle;
        }

        private void kryptonNavigatorDesignInputControls_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignInputControls.TextTitle = kryptonNavigatorDesignInputControls.SelectedPage.Text;
            pageDesignInputControls.TextDescription = kryptonNavigatorDesignInputControls.SelectedPage.TextDescription;

            InputControlStyle inputControlStyle;
            bool alwaysActive = true;

            // Work out the input control style to be used
            switch (kryptonNavigatorDesignInputControls.SelectedIndex)
            {
                default:
                case 0:
                    inputControlStyle = InputControlStyle.Standalone;
                    break;
                case 1:
                    inputControlStyle = InputControlStyle.Ribbon;
                    alwaysActive = false;
                    break;
                case 2:
                    inputControlStyle = InputControlStyle.Custom1;
                    break;
            }

            // Update all the displayed controls with the new styles
            textBoxDisabled.InputControlStyle = inputControlStyle;
            textBoxNormal.InputControlStyle = inputControlStyle;
            textBoxActive.InputControlStyle = inputControlStyle;
            comboBoxDisabled.InputControlStyle = inputControlStyle;
            comboBoxNormal.InputControlStyle = inputControlStyle;
            comboBoxActive.InputControlStyle = inputControlStyle;
            numericDisabled.InputControlStyle = inputControlStyle;
            numericNormal.InputControlStyle = inputControlStyle;
            numericActive.InputControlStyle = inputControlStyle;
            textBoxDisabled.AlwaysActive = alwaysActive;
            textBoxNormal.AlwaysActive = alwaysActive;
            textBoxActive.AlwaysActive = alwaysActive;
            comboBoxDisabled.AlwaysActive = alwaysActive;
            comboBoxNormal.AlwaysActive = alwaysActive;
            comboBoxActive.AlwaysActive = alwaysActive;
            numericDisabled.AlwaysActive = alwaysActive;
            numericNormal.AlwaysActive = alwaysActive;
            numericActive.AlwaysActive = alwaysActive;
        }

        private void kryptonNavigatorDesignHeaders_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignHeaders.TextTitle = kryptonNavigatorDesignHeaders.SelectedPage.Text;
            pageDesignHeaders.TextDescription = kryptonNavigatorDesignHeaders.SelectedPage.TextDescription;

            HeaderStyle hs;

            // Work out the header style to be used
            switch (kryptonNavigatorDesignHeaders.SelectedIndex)
            {
                default:
                case 0:
                    hs = HeaderStyle.Primary;
                    break;
                case 1:
                    hs = HeaderStyle.Secondary;
                    break;
                case 2:
                    hs = HeaderStyle.DockActive;
                    break;
                case 3:
                    hs = HeaderStyle.DockInactive;
                    break;
                case 4:
                    hs = HeaderStyle.Calendar;
                    break;
                case 5:
                    hs = HeaderStyle.Form;
                    break;
                case 6:
                    hs = HeaderStyle.Custom1;
                    break;
                case 7:
                    hs = HeaderStyle.Custom2;
                    break;
            }

            // Update all the displayed controls with the new styles
            header1Disabled.HeaderStyle = hs;
            header1Normal.HeaderStyle = hs;
        }

        private void kryptonNavigatorDesignLabels_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignLabels.TextTitle = kryptonNavigatorDesignLabels.SelectedPage.Text;
            pageDesignLabels.TextDescription = kryptonNavigatorDesignLabels.SelectedPage.TextDescription;

            LabelStyle ls;

            // Work out the label style to be used
            switch (kryptonNavigatorDesignLabels.SelectedIndex)
            {
                default:
                case 0:
                    ls = LabelStyle.NormalControl;
                    break;
                case 1:
                    ls = LabelStyle.BoldControl;
                    break;
                case 2:
                    ls = LabelStyle.ItalicControl;
                    break;
                case 3:
                    ls = LabelStyle.TitleControl;
                    break;
                case 4:
                    ls = LabelStyle.NormalPanel;
                    break;
                case 5:
                    ls = LabelStyle.BoldPanel;
                    break;
                case 6:
                    ls = LabelStyle.ItalicPanel;
                    break;
                case 7:
                    ls = LabelStyle.TitlePanel;
                    break;
                case 8:
                    ls = LabelStyle.GroupBoxCaption;
                    break;
                case 9:
                    ls = LabelStyle.ToolTip;
                    break;
                case 10:
                    ls = LabelStyle.SuperTip;
                    break;
                case 11:
                    ls = LabelStyle.KeyTip;
                    break;
                case 12:
                    ls = LabelStyle.Custom1;
                    break;
                case 13:
                    ls = LabelStyle.Custom2;
                    break;
                case 14:
                    ls = LabelStyle.Custom3;
                    break;
            }

            // Update all the displayed controls with the new styles
            label1Disabled.LabelStyle = ls;
            label1Normal.LabelStyle = ls;
            label1Visited.LabelStyle = ls;
            label1NotVisited.LabelStyle = ls;
            label1Pressed.LabelStyle = ls;
            label1Live.LabelStyle = ls;
        }

        private void kryptonCheckSetLabels_CheckedButtonChanged(object sender, EventArgs e)
        {
            switch (kryptonCheckSetLabels.CheckedIndex)
            {
                case 0:
                    panelLabelsBackground.PanelBackStyle = PaletteBackStyle.PanelClient;
                    break;
                case 1:
                    panelLabelsBackground.PanelBackStyle = PaletteBackStyle.PanelAlternate;
                    break;
                case 2:
                    panelLabelsBackground.PanelBackStyle = PaletteBackStyle.PanelCustom1;
                    break;
                case 3:
                    panelLabelsBackground.PanelBackStyle = PaletteBackStyle.ControlClient;
                    break;
                case 4:
                    panelLabelsBackground.PanelBackStyle = PaletteBackStyle.ControlAlternate;
                    break;
                case 5:
                    panelLabelsBackground.PanelBackStyle = PaletteBackStyle.ControlCustom1;
                    break;
                case 6:
                    panelLabelsBackground.PanelBackStyle = PaletteBackStyle.ControlToolTip;
                    break;
            }

            panelLabelsBackground.Refresh();
        }

        private void kryptonNavigatorDesignTabs_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignTabs.TextTitle = kryptonNavigatorDesignTabs.SelectedPage.Text;
            pageDesignTabs.TextDescription = kryptonNavigatorDesignTabs.SelectedPage.TextDescription;

            // Work out the tab style to show in the navigator
            switch (kryptonNavigatorDesignTabs.SelectedIndex)
            {
                default:
                case 0:
                    kryptonNavigatorTabs.Bar.TabStyle = TabStyle.HighProfile;
                    break;
                case 1:
                    kryptonNavigatorTabs.Bar.TabStyle = TabStyle.StandardProfile;
                    break;
                case 2:
                    kryptonNavigatorTabs.Bar.TabStyle = TabStyle.LowProfile;
                    break;
                case 3:
                    kryptonNavigatorTabs.Bar.TabStyle = TabStyle.OneNote;
                    break;
                case 4:
                    kryptonNavigatorTabs.Bar.TabStyle = TabStyle.Dock;
                    break;
                case 5:
                    kryptonNavigatorTabs.Bar.TabStyle = TabStyle.DockAutoHidden;
                    break;
                case 6:
                    kryptonNavigatorTabs.Bar.TabStyle = TabStyle.Custom1;
                    break;
                case 7:
                    kryptonNavigatorTabs.Bar.TabStyle = TabStyle.Custom2;
                    break;
                case 8:
                    kryptonNavigatorTabs.Bar.TabStyle = TabStyle.Custom3;
                    break;
            }
        }

        private void kryptonNavigatorDesignNavigator_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignNavigator.TextTitle = kryptonNavigatorDesignNavigator.SelectedPage.Text;
            pageDesignNavigator.TextDescription = kryptonNavigatorDesignNavigator.SelectedPage.TextDescription;

            // Work out the navigator mode required
            switch (kryptonNavigatorDesignNavigator.SelectedIndex)
            {
                default:
                case 0:
                    kryptonNavigator.NavigatorMode = NavigatorMode.BarCheckButtonGroupOutside;
                    break;
                case 1:
                    kryptonNavigator.NavigatorMode = NavigatorMode.BarCheckButtonGroupInside;
                    break;
                case 2:
                    kryptonNavigator.NavigatorMode = NavigatorMode.BarCheckButtonGroupOnly;
                    break;
            }
        }

        private void kryptonNavigatorDesignPanels_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignPanels.TextTitle = kryptonNavigatorDesignPanels.SelectedPage.Text;
            pageDesignPanels.TextDescription = kryptonNavigatorDesignPanels.SelectedPage.TextDescription;

            PaletteBackStyle backStyle;

            // Work out the panel style to be used
            switch (kryptonNavigatorDesignPanels.SelectedIndex)
            {
                default:
                case 0:
                    backStyle = PaletteBackStyle.PanelClient;
                    break;
                case 1:
                    backStyle = PaletteBackStyle.PanelAlternate;
                    break;
                case 2:
                    backStyle = PaletteBackStyle.PanelRibbonInactive;
                    break;
                case 3:
                    backStyle = PaletteBackStyle.PanelCustom1;
                    break;
            }

            // Update all the displayed controls with the new styles
            panel1Disabled.PanelBackStyle = backStyle;
            panel1Normal.PanelBackStyle = backStyle;
        }

        private void kryptonNavigatorDesignSeparators_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignSeparators.TextTitle = kryptonNavigatorDesignSeparators.SelectedPage.Text;
            pageDesignSeparators.TextDescription = kryptonNavigatorDesignSeparators.SelectedPage.TextDescription;

            SeparatorStyle separatorStyle;

            // Work out the navigator mode required
            switch (kryptonNavigatorDesignSeparators.SelectedIndex)
            {
                default:
                case 0:
                    separatorStyle = SeparatorStyle.LowProfile;
                    break;
                case 1:
                    separatorStyle = SeparatorStyle.HighProfile;
                    break;
                case 2:
                    separatorStyle = SeparatorStyle.HighInternalProfile;
                    break;
                case 3:
                    separatorStyle = SeparatorStyle.Custom1;
                    break;
            }

            // Update all the displayed controls with the new styles
            separator1Disabled.SeparatorStyle = separatorStyle;
            separator1Normal.SeparatorStyle = separatorStyle;
            separator1Tracking.SeparatorStyle = separatorStyle;
            separator1Pressed.SeparatorStyle = separatorStyle;
            separator1Live.SeparatorStyle = separatorStyle;
        }

        private void kryptonNavigatorDesignGrids_SelectedPageChanged(object sender, EventArgs e)
        {
            // Update the design page text with the selected style information
            pageDesignGrid.TextTitle = kryptonNavigatorDesignGrids.SelectedPage.Text;
            pageDesignGrid.TextDescription = kryptonNavigatorDesignGrids.SelectedPage.TextDescription;

            DataGridViewStyle gridStyle;

            // Work out the navigator mode required
            switch (kryptonNavigatorDesignGrids.SelectedIndex)
            {
                default:
                case 0:
                    gridStyle = DataGridViewStyle.List;
                    break;
                case 1:
                    gridStyle = DataGridViewStyle.Sheet;
                    break;
                case 2:
                    gridStyle = DataGridViewStyle.Custom1;
                    break;
            }

            // Update all the displayed controls with the new styles
            dataGridViewDisabled.GridStyles.Style = gridStyle;
            dataGridViewNormal.GridStyles.Style = gridStyle;
        }
        #endregion

        #region Implementation
        private void UpdateTitlebar()
        {
            // Mark a changed file with a star
            Text = "Palette Designer - " + _filename + (_dirty ? "*" : string.Empty);
        }
        #endregion
    }
}
