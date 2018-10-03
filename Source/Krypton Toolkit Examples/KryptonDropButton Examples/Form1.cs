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

namespace KryptonDropButtonExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this drop down
            propertyGrid.SelectedObject = new KryptonDropButtonProxy(splitterPosRight);
        }

        private void dropButtonEnter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this drop down
            propertyGrid.SelectedObject = new KryptonDropButtonProxy(sender as KryptonDropButton);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonDropButtonProxy
    {
        private KryptonDropButton _dropDown;

        public KryptonDropButtonProxy(KryptonDropButton comboBox)
        {
            _dropDown = comboBox;
        }

        [Category("Appearance")]
        [Description("Text for display inside the control.")]
        public string Text
        {
            get { return _dropDown.Text; }
            set { _dropDown.Text = value; }
        }


        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _dropDown.Size; }
            set { _dropDown.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _dropDown.Location; }
            set { _dropDown.Location = value; }
        }

        [Category("Behavior")]
        [Description("The shortcut menu to show when the user right-clicks the page.")]
        public KryptonContextMenu KryptonContextMenu
        {
            get { return _dropDown.KryptonContextMenu; }
        }

        [Category("Visuals")]
        [Description("Visual orientation of the control.")]
        public VisualOrientation ButtonOrientation
        {
            get { return _dropDown.ButtonOrientation; }
            set { _dropDown.ButtonOrientation = value; }
        }

        [Category("Visuals")]
        [Description("Position of the drop arrow within the button.")]
        public VisualOrientation DropDownPosition
        {
            get { return _dropDown.DropDownPosition; }
            set { _dropDown.DropDownPosition = value; }
        }

        [Category("Visuals")]
        [Description("Orientation of the drop arrow within the button.")]
        public VisualOrientation DropDownOrientation
        {
            get { return _dropDown.DropDownOrientation; }
            set { _dropDown.DropDownOrientation = value; }
        }

        [Category("Visuals")]
        [Description("Determine if button acts as a splitter or just a drop down.")]
        public bool Splitter
        {
            get { return _dropDown.Splitter; }
            set { _dropDown.Splitter = value; }
        }

        [Category("Visuals")]
        [Description("Button style.")]
        public ButtonStyle ButtonStyle
        {
            get { return _dropDown.ButtonStyle; }
            set { _dropDown.ButtonStyle = value; }
        }

        [Category("Visuals")]
        [Description("Button values")]
        public ButtonValues Values
        {
            get { return _dropDown.Values; }
        }

        [Category("Visuals")]
        [Description("Image value overrides.")]
        public DropDownButtonImages Images
        {
            get { return _dropDown.Images; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common button appearance that other states can override.")]
        public PaletteTripleRedirect StateCommon
        {
            get { return _dropDown.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled button appearance.")]
        public PaletteTriple StateDisabled
        {
            get { return _dropDown.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal button appearance.")]
        public PaletteTriple StateNormal
        {
            get { return _dropDown.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining hot tracking button appearance.")]
        public PaletteTriple StateTracking
        {
            get { return _dropDown.StateTracking; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining pressed button appearance.")]
        public PaletteTriple StatePressed
        {
            get { return _dropDown.StatePressed; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal button appearance when default.")]
        public PaletteTripleRedirect OverrideDefault
        {
            get { return _dropDown.OverrideDefault; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining button appearance when it has focus.")]
        public PaletteTripleRedirect OverrideFocus
        {
            get { return _dropDown.OverrideFocus; }
        }
    }
}
