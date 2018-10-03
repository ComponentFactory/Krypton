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

namespace KryptonLinkLabelExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this label
            propertyGrid.SelectedObject = new KryptonLabelProxy(label2Professional);
        }

        private void kryptonLabel_MouseDown(object sender, MouseEventArgs e)
        {
            // Setup the property grid to edit this label
            propertyGrid.SelectedObject = new KryptonLabelProxy(sender as KryptonLabel);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonLabelProxy
    {
        private KryptonLabel _label;

        public KryptonLabelProxy(KryptonLabel label)
        {
            _label = label;
        }

        [Category("Visuals")]
        [Description("Label style.")]
        public LabelStyle LabelStyle
        {
            get { return _label.LabelStyle; }
            set { _label.LabelStyle = value; }
        }

        [Category("Visuals")]
        [Description("Header values")]
        public LabelValues Values
        {
            get { return _label.Values; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common label appearance that other states can override.")]
        public PaletteContent StateCommon
        {
            get { return _label.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled label appearance.")]
        public PaletteContent StateDisabled
        {
            get { return _label.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal label appearance.")]
        public PaletteContent StateNormal
        {
            get { return _label.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Visual orientation of the control.")]
        public VisualOrientation Orientation
        {
            get { return _label.Orientation; }
            set { _label.Orientation = value; }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        public PaletteMode PaletteMode
        {
            get { return _label.PaletteMode; }
            set { _label.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("Specifies whether a control will automatically size itself to fit its contents.")]
        public bool AutoSize
        {
            get { return _label.AutoSize; }
            set { _label.AutoSize = value; }
        }

        [Category("Layout")]
        [Description("Specifies if the control grows and shrinks to fit the contents exactly.")]
        public AutoSizeMode AutoSizeMode
        {
            get { return _label.AutoSizeMode; }
            set { _label.AutoSizeMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _label.Size; }
            set { _label.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _label.Location; }
            set { _label.Location = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the control is enabled.")]
        public bool Enabled
        {
            get { return _label.Enabled; }
            set { _label.Enabled = value; }
        }
    }
}
