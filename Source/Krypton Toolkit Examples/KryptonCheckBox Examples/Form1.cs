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

namespace KryptonCheckBoxExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = new KryptonCheckBoxProxy(kryptonCheckBox13);
        }

        private void CheckButtonEnter(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = new KryptonCheckBoxProxy(sender as KryptonCheckBox);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonCheckBoxProxy
    {
        private KryptonCheckBox _checkBox;

        public KryptonCheckBoxProxy(KryptonCheckBox button)
        {
            _checkBox = button;
        }

        [Category("Appearance")]
        [Description("Indicates if the component is in the checked state.")]
        public bool Checked
        {
            get { return _checkBox.Checked; }
            set { _checkBox.Checked = value; }
        }

        [Category("Appearance")]
        [Description("Indicates the checked state of the component.")]
        public CheckState CheckState
        {
            get { return _checkBox.CheckState; }
            set { _checkBox.CheckState = value; }
        }

        [Category("Behavior")]
        [Description("Causes the check box to automatically change state when clicked.")]
        public bool AutoCheck
        {
            get { return _checkBox.AutoCheck; }
            set { _checkBox.AutoCheck = value; }
        }

        [Category("Behavior")]
        [Description("Indicates if the component allows three states instead of two.")]
        public bool ThreeState
        {
            get { return _checkBox.ThreeState; }
            set { _checkBox.ThreeState = value; }
        }

        [Category("Visuals")]
        [Description("Visual position of the check box.")]
        public virtual VisualOrientation CheckPosition
        {
            get { return _checkBox.CheckPosition; }
            set { _checkBox.CheckPosition = value; }
        }

        [Category("Visuals")]
        [Description("Image value overrides.")]
        public CheckBoxImages Images
        {
            get { return _checkBox.Images; }
        }

		[Category("Visuals")]
		[Description("Label style.")]
		public LabelStyle LabelStyle
        {
            get { return _checkBox.LabelStyle; }
            set { _checkBox.LabelStyle = value; }
        }

        [Category("Visuals")]
        [Description("Label values")]
        public LabelValues Values
        {
            get { return _checkBox.Values; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common label appearance that other states can override.")]
        public PaletteContent StateCommon
        {
            get { return _checkBox.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled label appearance.")]
        public PaletteContent StateDisabled
        {
            get { return _checkBox.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal label appearance.")]
        public PaletteContent StateNormal
        {
            get { return _checkBox.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining label appearance when it has focus.")]
        public PaletteContent OverrideFocus
        {
            get { return _checkBox.OverrideFocus; }
        }

        [Category("Visuals")]
        [Description("Visual orientation of the control.")]
        [DefaultValue(typeof(VisualOrientation), "Top")]
        public VisualOrientation Orientation
        {
            get { return _checkBox.Orientation; }
            set { _checkBox.Orientation = value; }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public PaletteMode PaletteMode
        {
            get { return _checkBox.PaletteMode; }
            set { _checkBox.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("Specifies whether a control will automatically size itself to fit its contents.")]
        [DefaultValue(false)]
        public bool AutoSize
        {
            get { return _checkBox.AutoSize; }
            set { _checkBox.AutoSize = value; }
        }

        [Category("Layout")]
        [Description("Specifies if the control grows and shrinks to fit the contents exactly.")]
        [DefaultValue(typeof(AutoSizeMode), "GrowOnly")]
        public AutoSizeMode AutoSizeMode
        {
            get { return _checkBox.AutoSizeMode; }
            set { _checkBox.AutoSizeMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _checkBox.Size; }
            set { _checkBox.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _checkBox.Location; }
            set { _checkBox.Location = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the control is enabled.")]
        public bool Enabled
        {
            get { return _checkBox.Enabled; }
            set { _checkBox.Enabled = value; }
        }
    }
}
