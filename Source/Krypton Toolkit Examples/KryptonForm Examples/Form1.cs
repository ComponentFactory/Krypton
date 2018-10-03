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

namespace KryptonFormExamples
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void kryptonCheckSetPalettes_CheckedButtonChanged(object sender, EventArgs e)
        {
            // Recalc the non client size to reflect new border style
            RecalcNonClient();

            switch (kryptonCheckSetPalettes.CheckedIndex)
            {
                case 0:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
                    break;
                case 1:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Black;
                    break;
                case 2:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Silver;
                    break;
                case 3:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                    break;
                case 4:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.ProfessionalOffice2003;
                    break;
                case 5:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
                    break;
                case 6:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.SparkleOrange;
                    break;
                case 7:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Black;
                    break;
                case 8:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
                    break;
                case 9:
                    kryptonManager.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                    break;
            }
        }

        private void kryptonCheckSetStyles_CheckedButtonChanged(object sender, EventArgs e)
        {
            switch (kryptonCheckSetStyles.CheckedIndex)
            {
                case 0:
                    FormBorderStyle = FormBorderStyle.Sizable;
                    break;
                case 1:
                    FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    break;
                case 2:
                    FormBorderStyle = FormBorderStyle.SizableToolWindow;
                    break;
                case 3:
                    FormBorderStyle = FormBorderStyle.FixedDialog;
                    break;
                case 4:
                    FormBorderStyle = FormBorderStyle.Fixed3D;
                    break;
                case 5:
                    FormBorderStyle = FormBorderStyle.FixedSingle;
                    break;
                case 6:
                    FormBorderStyle = FormBorderStyle.None;
                    break;
            }

            // Recalc the non client size to reflect new border style
            RecalcNonClient();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set correct initial checked button
            if (KryptonManager.CurrentGlobalPalette == KryptonManager.PaletteOffice2007Black)
                kryptonCheckSetPalettes.CheckedIndex = 1;

            // Setup the property grid to edit this form
            propertyGrid.SelectedObject = new KryptonFormProxy(this);
        }
    }

    public class KryptonFormProxy
    {
        private KryptonForm _form;

        public KryptonFormProxy(KryptonForm form)
        {
            _form = form;
        }

        [Category("Appearance")]
        [Description("The text associated with the control.")]
        [DefaultValue("")]
        public string Text
        {
            get { return _form.Text; }
            set { _form.Text = value; }
        }

        [Category("Appearance")]
        [Description("The extra text associated with the control.")]
        [DefaultValue("")]
        public string TextExtra
        {
            get { return _form.TextExtra; }
            set { _form.TextExtra = value; }
        }

        [Category("Appearance")]
        [Description("The icon associated with the control.")]
        [DefaultValue("")]
        public Icon Icon
        {
            get { return _form.Icon; }
            set { _form.Icon = value; }
        }

        [Category("Visuals")]
        [Description("Should custom chrome be allowed for this KryptonForm instance.")]
        [DefaultValue(true)]
        public bool AllowFormChrome
        {
            get { return _form.AllowFormChrome; }
            set { _form.AllowFormChrome = value; }
        }

        [Category("Visuals")]
        [Description("Should the form status strip be considered for merging into chrome.")]
        [DefaultValue(true)]
        public bool AllowStatusStripMerge
        {
            get { return _form.AllowStatusStripMerge; }
            set { _form.AllowStatusStripMerge = value; }
        }

        [Category("Visuals")]
        [Description("Header style for a main form.")]
        [DefaultValue(typeof(HeaderStyle), "Form")]
        public HeaderStyle HeaderStyle
        {
            get { return _form.HeaderStyle; }
            set { _form.HeaderStyle = value; }
        }

        [Category("Visuals")]
        [Description("Chrome group border style.")]
        [DefaultValue(typeof(PaletteBorderStyle), "FormMain")]
        public PaletteBorderStyle GroupBorderStyle
        {
            get { return _form.GroupBorderStyle; }
            set { _form.GroupBorderStyle = value; }
        }

        [Category("Visuals")]
        [Description("Chrome group background style.")]
        [DefaultValue(typeof(PaletteBackStyle), "FormMain")]
        public PaletteBackStyle GroupBackStyle
        {
            get { return _form.GroupBackStyle; }
            set { _form.GroupBackStyle = value; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common form appearance that other states can override.")]
        public PaletteFormRedirect StateCommon
        {
            get { return _form.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining inactive form appearance.")]
        public PaletteForm StateInactive
        {
            get { return _form.StateInactive; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining active form appearance.")]
        public PaletteForm StateActive
        {
            get { return _form.StateActive; }
        }

        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        public KryptonForm.FormButtonSpecCollection ButtonSpecs
        {
            get { return _form.ButtonSpecs; }
        }

        [Category("Window Style")]
        [DefaultValue(true)]
        public bool ControlBox
        {
            get { return _form.ControlBox; }
            set { _form.ControlBox = value; }
        }

        [Category("Window Style")]
        [DefaultValue(true)]
        public bool MaximizeBox
        {
            get { return _form.MaximizeBox; }
            set { _form.MaximizeBox = value; }
        }

        [Category("Window Style")]
        [DefaultValue(true)]
        public bool MinimizeBox
        {
            get { return _form.MinimizeBox; }
            set { _form.MinimizeBox = value; }
        }

        [Category("Window Style")]
        [DefaultValue(true)]
        public bool ShowIcon
        {
            get { return _form.ShowIcon; }
            set { _form.ShowIcon = value; }
        }
    }
}
