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

namespace KryptonButtonExamples
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
            propertyGrid.SelectedObject = new KryptonButtonProxy(button1Sparkle);
        }

        private void button_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this button
            propertyGrid.SelectedObject = new KryptonButtonProxy(sender as KryptonButton);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonButtonProxy
    {
        private KryptonButton _button;

        public KryptonButtonProxy(KryptonButton button)
        {
            _button = button;
        }

        [Category("Visuals")]
        [Description("Button style.")]
        [DefaultValue(typeof(ButtonStyle), "Standalone")]
        public ButtonStyle ButtonStyle
        {
            get { return _button.ButtonStyle; }
            set { _button.ButtonStyle = value; }
        }

        [Category("Visuals")]
        [Description("Button values")]
        public ButtonValues Values
        {
            get { return _button.Values; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common button appearance that other states can override.")]
        public PaletteTripleRedirect StateCommon
        {
            get { return _button.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled button appearance.")]
        public PaletteTriple StateDisabled
        {
            get { return _button.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal button appearance.")]
        public PaletteTriple StateNormal
        {
            get { return _button.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining hot tracking button appearance.")]
        public PaletteTriple StateTracking
        {
            get { return _button.StateTracking; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining pressed button appearance.")]
        public PaletteTriple StatePressed
        {
            get { return _button.StatePressed; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal button appearance when default.")]
        public PaletteTripleRedirect OverrideDefault
        {
            get { return _button.OverrideDefault; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining button appearance when it has focus.")]
        public PaletteTripleRedirect OverrideFocus
        {
            get { return _button.OverrideFocus; }
        }

        [Category("Visuals")]
        [Description("Visual orientation of the control.")]
        [DefaultValue(typeof(VisualOrientation), "Top")]
        public VisualOrientation Orientation
        {
            get { return _button.Orientation; }
            set { _button.Orientation = value;  }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public PaletteMode PaletteMode
        {
            get { return _button.PaletteMode; }
            set { _button.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("Specifies whether a control will automatically size itself to fit its contents.")]
        [DefaultValue(false)]
        public bool AutoSize
        {
            get { return _button.AutoSize; }
            set { _button.AutoSize = value; }
        }

        [Category("Layout")]
        [Description("Specifies if the control grows and shrinks to fit the contents exactly.")]
        [DefaultValue(typeof(AutoSizeMode), "GrowOnly")]
        public AutoSizeMode AutoSizeMode
        {
            get { return _button.AutoSizeMode; }
            set { _button.AutoSizeMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _button.Size; }
            set { _button.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _button.Location; }
            set { _button.Location = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the control is enabled.")]
        public bool Enabled
        {
            get { return _button.Enabled; }
            set { _button.Enabled = value; }
        }
    }
}
