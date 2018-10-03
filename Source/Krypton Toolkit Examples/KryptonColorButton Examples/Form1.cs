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

namespace KryptonColorButtonExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this color button
            propertyGrid.SelectedObject = new KryptonColorButtonProxy(blueSplitter);
        }

        private void colorButtonEnter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this color button
            propertyGrid.SelectedObject = new KryptonColorButtonProxy(sender as KryptonColorButton);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonColorButtonProxy
    {
        private KryptonColorButton _colorButton;

        public KryptonColorButtonProxy(KryptonColorButton comboBox)
        {
            _colorButton = comboBox;
        }

        [Category("Appearance")]
        [Description("Text for display inside the control.")]
        public string Text
        {
            get { return _colorButton.Text; }
            set { _colorButton.Text = value; }
        }


        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _colorButton.Size; }
            set { _colorButton.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _colorButton.Location; }
            set { _colorButton.Location = value; }
        }

        [Category("Behavior")]
        [Description("Determine the maximum number of recent colors to store and display.")]
        public int MaxRecentColors
        {
            get { return _colorButton.MaxRecentColors; }
            set { _colorButton.MaxRecentColors = value; }
        }

        [Category("Behavior")]
        [Description("Determine the visible state of the themes color set.")]
        public bool VisibleThemes
        {
            get { return _colorButton.VisibleThemes; }
            set { _colorButton.VisibleThemes = value; }
        }

        [Category("Behavior")]
        [Description("Determine the visible state of the standard color set.")]
        public bool VisibleStandard
        {
            get { return _colorButton.VisibleStandard; }
            set { _colorButton.VisibleStandard = value; }
        }

        [Category("Behavior")]
        [Description("Determine the visible state of the recent color set.")]
        public bool VisibleRecent
        {
            get { return _colorButton.VisibleRecent; }
            set { _colorButton.VisibleRecent = value; }
        }

        [Category("Behavior")]
        [Description("Determine if the 'No Color' menu item is used.")]
        public bool VisibleNoColor
        {
            get { return _colorButton.VisibleNoColor; }
            set { _colorButton.VisibleNoColor = value; }
        }

        [Category("Behavior")]
        [Description("Determine if the 'More Colors...' menu item is used.")]
        public bool VisibleMoreColors
        {
            get { return _colorButton.VisibleMoreColors; }
            set { _colorButton.VisibleMoreColors = value; }
        }

        [Category("Behavior")]
        [Description("Should recent colors be automatically updated.")]
        public bool AutoRecentColors
        {
            get { return _colorButton.AutoRecentColors; }
            set { _colorButton.AutoRecentColors = value; }
        }

        [Category("Behavior")]
        [Description("Color scheme to use for the themes color set.")]
        public ColorScheme SchemeThemes
        {
            get { return _colorButton.SchemeThemes; }
            set { _colorButton.SchemeThemes = value; }
        }

        [Category("Behavior")]
        [Description("Color scheme to use for the standard color set.")]
        public ColorScheme SchemeStandard
        {
            get { return _colorButton.SchemeStandard; }
            set { _colorButton.SchemeStandard = value; }
        }

        [Category("Appearance")]
        [Description("Selected color.")]
        public Color SelectedColor
        {
            get { return _colorButton.SelectedColor; }
            set { _colorButton.SelectedColor = value; }
        }

        [Category("Appearance")]
        [Description("Border color of selected block when selected color is empty.")]
        public Color EmptyBorderColor
        {
            get { return _colorButton.EmptyBorderColor; }
            set { _colorButton.EmptyBorderColor = value; }
        }

        [Category("Appearance")]
        [Description("Selected color drawing rectangle.")]
        public Rectangle SelectedRect
        {
            get { return _colorButton.SelectedRect; }
            set { _colorButton.SelectedRect = value; }
        }

        [Category("Visuals")]
        [Description("Context menu display strings.")]
        public PaletteColorButtonStrings Strings
        {
            get { return _colorButton.Strings; }
        }

        [Category("Visuals")]
        [Description("Visual orientation of the control.")]
        public VisualOrientation ButtonOrientation
        {
            get { return _colorButton.ButtonOrientation; }
            set { _colorButton.ButtonOrientation = value; }
        }

        [Category("Visuals")]
        [Description("Position of the drop arrow within the button.")]
        public VisualOrientation DropDownPosition
        {
            get { return _colorButton.DropDownPosition; }
            set { _colorButton.DropDownPosition = value; }
        }

        [Category("Visuals")]
        [Description("Orientation of the drop arrow within the button.")]
        public VisualOrientation DropDownOrientation
        {
            get { return _colorButton.DropDownOrientation; }
            set { _colorButton.DropDownOrientation = value; }
        }

        [Category("Visuals")]
        [Description("Determine if button acts as a splitter or just a drop down.")]
        public bool Splitter
        {
            get { return _colorButton.Splitter; }
            set { _colorButton.Splitter = value; }
        }

        [Category("Visuals")]
        [Description("Button style.")]
        public ButtonStyle ButtonStyle
        {
            get { return _colorButton.ButtonStyle; }
            set { _colorButton.ButtonStyle = value; }
        }

        [Category("Visuals")]
        [Description("Button values")]
        public ColorButtonValues Values
        {
            get { return _colorButton.Values; }
        }

        [Category("Visuals")]
        [Description("Image value overrides.")]
        public DropDownButtonImages Images
        {
            get { return _colorButton.Images; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common button appearance that other states can override.")]
        public PaletteTripleRedirect StateCommon
        {
            get { return _colorButton.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled button appearance.")]
        public PaletteTriple StateDisabled
        {
            get { return _colorButton.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal button appearance.")]
        public PaletteTriple StateNormal
        {
            get { return _colorButton.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining hot tracking button appearance.")]
        public PaletteTriple StateTracking
        {
            get { return _colorButton.StateTracking; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining pressed button appearance.")]
        public PaletteTriple StatePressed
        {
            get { return _colorButton.StatePressed; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal button appearance when default.")]
        public PaletteTripleRedirect OverrideDefault
        {
            get { return _colorButton.OverrideDefault; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining button appearance when it has focus.")]
        public PaletteTripleRedirect OverrideFocus
        {
            get { return _colorButton.OverrideFocus; }
        }
    }
}
