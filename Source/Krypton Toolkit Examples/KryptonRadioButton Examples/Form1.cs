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

namespace KryptonRadioButtonExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = new KryptonRadioButtonProxy(kryptonRadioButton12);
        }

        private void RadioButtonEnter(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = new KryptonRadioButtonProxy(sender as KryptonRadioButton);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonRadioButtonProxy
    {
        private KryptonRadioButton _radioButton;

        public KryptonRadioButtonProxy(KryptonRadioButton checkBox)
        {
            _radioButton = checkBox;
        }

        [Category("Appearance")]
        [Description("Indicates if the component is in the checked state.")]
        public bool Checked
        {
            get { return _radioButton.Checked; }
            set { _radioButton.Checked = value; }
        }

        [Category("Behavior")]
        [Description("Causes the check box to automatically change state when clicked.")]
        public bool AutoCheck
        {
            get { return _radioButton.AutoCheck; }
            set { _radioButton.AutoCheck = value; }
        }

        [Category("Visuals")]
        [Description("Visual position of the check box.")]
        public virtual VisualOrientation CheckPosition
        {
            get { return _radioButton.CheckPosition; }
            set { _radioButton.CheckPosition = value; }
        }

        [Category("Visuals")]
        [Description("Image value overrides.")]
        public RadioButtonImages Images
        {
            get { return _radioButton.Images; }
        }

        [Category("Visuals")]
        [Description("Label style.")]
        public LabelStyle LabelStyle
        {
            get { return _radioButton.LabelStyle; }
            set { _radioButton.LabelStyle = value; }
        }

        [Category("Visuals")]
        [Description("Label values")]
        public LabelValues Values
        {
            get { return _radioButton.Values; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common label appearance that other states can override.")]
        public PaletteContent StateCommon
        {
            get { return _radioButton.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled label appearance.")]
        public PaletteContent StateDisabled
        {
            get { return _radioButton.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal label appearance.")]
        public PaletteContent StateNormal
        {
            get { return _radioButton.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining label appearance when it has focus.")]
        public PaletteContent OverrideFocus
        {
            get { return _radioButton.OverrideFocus; }
        }

        [Category("Visuals")]
        [Description("Visual orientation of the control.")]
        [DefaultValue(typeof(VisualOrientation), "Top")]
        public VisualOrientation Orientation
        {
            get { return _radioButton.Orientation; }
            set { _radioButton.Orientation = value; }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public PaletteMode PaletteMode
        {
            get { return _radioButton.PaletteMode; }
            set { _radioButton.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("Specifies whether a control will automatically size itself to fit its contents.")]
        [DefaultValue(false)]
        public bool AutoSize
        {
            get { return _radioButton.AutoSize; }
            set { _radioButton.AutoSize = value; }
        }

        [Category("Layout")]
        [Description("Specifies if the control grows and shrinks to fit the contents exactly.")]
        [DefaultValue(typeof(AutoSizeMode), "GrowOnly")]
        public AutoSizeMode AutoSizeMode
        {
            get { return _radioButton.AutoSizeMode; }
            set { _radioButton.AutoSizeMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _radioButton.Size; }
            set { _radioButton.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _radioButton.Location; }
            set { _radioButton.Location = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the control is enabled.")]
        public bool Enabled
        {
            get { return _radioButton.Enabled; }
            set { _radioButton.Enabled = value; }
        }
    }
}
