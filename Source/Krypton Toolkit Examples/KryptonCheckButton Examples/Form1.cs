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

namespace KryptonCheckButtonExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this button
            propertyGrid.SelectedObject = new KryptonCheckButtonProxy(button1Sparkle);
        }

        private void checkButton_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this button
            propertyGrid.SelectedObject = new KryptonCheckButtonProxy(sender as KryptonCheckButton);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonCheckButtonProxy
    {
        private KryptonCheckButton _checkButton;

        public KryptonCheckButtonProxy(KryptonCheckButton checkButton)
        {
            _checkButton = checkButton;
        }

        [Category("Visuals")]
        [Description("Button style.")]
        [DefaultValue(typeof(ButtonStyle), "Standalone")]
        public ButtonStyle ButtonStyle
        {
            get { return _checkButton.ButtonStyle; }
            set { _checkButton.ButtonStyle = value; }
        }

        [Category("Visuals")]
        [Description("Button values")]
        public ButtonValues Values
        {
            get { return _checkButton.Values; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common button appearance that other states can override.")]
        public PaletteTripleRedirect StateCommon
        {
            get { return _checkButton.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled button appearance.")]
        public PaletteTriple StateDisabled
        {
            get { return _checkButton.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal button appearance.")]
        public PaletteTriple StateNormal
        {
            get { return _checkButton.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining hot tracking button appearance.")]
        public PaletteTriple StateTracking
        {
            get { return _checkButton.StateTracking; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining pressed button appearance.")]
        public PaletteTriple StatePressed
        {
            get { return _checkButton.StatePressed; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal checked button appearance.")]
        public PaletteTriple StateCheckedNormal
        {
            get { return _checkButton.StateCheckedNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining hot tracking checked button appearance.")]
        public PaletteTriple StateCheckedTracking
        {
            get { return _checkButton.StateCheckedTracking; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining pressed checked button appearance.")]
        public PaletteTriple StateCheckedPressed
        {
            get { return _checkButton.StateCheckedPressed; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal button appearance when default.")]
        public PaletteTripleRedirect OverrideDefault
        {
            get { return _checkButton.OverrideDefault; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining button appearance when it has focus.")]
        public PaletteTripleRedirect OverrideFocus
        {
            get { return _checkButton.OverrideFocus; }
        }

        [Category("Visuals")]
        [Description("Visual orientation of the control.")]
        [DefaultValue(typeof(VisualOrientation), "Top")]
        public VisualOrientation Orientation
        {
            get { return _checkButton.Orientation; }
            set { _checkButton.Orientation = value; }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public PaletteMode PaletteMode
        {
            get { return _checkButton.PaletteMode; }
            set { _checkButton.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("Specifies whether a control will automatically size itself to fit its contents.")]
        [DefaultValue(false)]
        public bool AutoSize
        {
            get { return _checkButton.AutoSize; }
            set { _checkButton.AutoSize = value; }
        }

        [Category("Layout")]
        [Description("Specifies if the control grows and shrinks to fit the contents exactly.")]
        [DefaultValue(typeof(AutoSizeMode), "GrowOnly")]
        public AutoSizeMode AutoSizeMode
        {
            get { return _checkButton.AutoSizeMode; }
            set { _checkButton.AutoSizeMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _checkButton.Size; }
            set { _checkButton.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _checkButton.Location; }
            set { _checkButton.Location = value; }
        }

        [Category("Appearance")]
        [Description("Indicates whether the control is in the checked state.")]
        public bool Checked
        {
            get { return _checkButton.Checked; }
            set { _checkButton.Checked = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the control is enabled.")]
        public bool Enabled
        {
            get { return _checkButton.Enabled; }
            set { _checkButton.Enabled = value; }
        }
    }
}
