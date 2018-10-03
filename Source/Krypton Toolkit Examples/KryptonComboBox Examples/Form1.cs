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

namespace KryptonComboBoxExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this combo box
            propertyGrid.SelectedObject = new KryptonComboBoxProxy(kryptonComboBox1Blue);
        }

        private void kryptonComboBox1Blue_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this combo box
            propertyGrid.SelectedObject = new KryptonComboBoxProxy(sender as KryptonComboBox);
        }

        private void buttonSpecAny1_Click(object sender, EventArgs e)
        {
            kryptonComboBox2Blue.Text = string.Empty;
            kryptonComboBox2Blue.ComboBox.Focus();
        }

        private void buttonSpecAny2_Click(object sender, EventArgs e)
        {
            kryptonComboBox5System.Text = string.Empty;
            kryptonComboBox5System.ComboBox.Focus();
        }

        private void buttonSpecAny3_Click(object sender, EventArgs e)
        {
            if (kryptonComboBox8Custom.SelectedIndex > 0)
            {
                kryptonComboBox8Custom.SelectedIndex -= 1;
                kryptonComboBox8Custom.ComboBox.Focus();
            }
        }

        private void buttonSpecAny4_Click(object sender, EventArgs e)
        {
            if (kryptonComboBox8Custom.SelectedIndex < (kryptonComboBox8Custom.Items.Count - 1))
            {
                kryptonComboBox8Custom.SelectedIndex += 1;
                kryptonComboBox8Custom.ComboBox.Focus();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonComboBoxProxy
    {
        private KryptonComboBox _comboBox;

        public KryptonComboBoxProxy(KryptonComboBox comboBox)
        {
            _comboBox = comboBox;
        }

        [Category("Appearance")]
        [Description("Text for display inside the control.")]
        public string Text
        {
            get { return _comboBox.Text; }
            set { _comboBox.Text = value; }
        }

        [Category("Visuals")]
        [Description("Determines if the control is always active or only when the mouse is over the control or has focus.")]
        public bool AlwaysActive
        {
            get { return _comboBox.AlwaysActive; }
            set { _comboBox.AlwaysActive = value; }
        }

        [Category("Appearance")]
        [Description("Controls the appearance and functionality of the KryptonComboBox.")]
        public ComboBoxStyle DropDownStyle
        {
            get { return _comboBox.DropDownStyle; }
            set { _comboBox.DropDownStyle = value; }
        }

        [Category("Behavior")]
        [Description("The height, in pixels, of the drop down box in a KryptonComboBox.")]
        public int DropDownHeight
        {
            get { return _comboBox.DropDownHeight; }
            set { _comboBox.DropDownHeight = value; }
        }

        [Category("Behavior")]
        [Description("The width, in pixels, of the drop down box in a KryptonComboBox.")]
        public int DropDownWidth
        {
            get { return _comboBox.DropDownWidth; }
            set { _comboBox.DropDownWidth = value; }
        }

        [Category("Behavior")]
        [Description("The height, in pixels, of items in an owner-draw KryptomComboBox.")]
        public int ItemHeight
        {
            get { return _comboBox.ItemHeight; }
            set { _comboBox.ItemHeight = value; }
        }

        [Category("Behavior")]
        [Description("The maximum number of entries to display in the drop-down list.")]
        public int MaxDropDownItems
        {
            get { return _comboBox.MaxDropDownItems; }
            set { _comboBox.MaxDropDownItems = value; }
        }

        [Category("Behavior")]
        [Description("Specifies the maximum number of characters that can be entered into the edit control.")]
        public int MaxLength
        {
            get { return _comboBox.MaxLength; }
            set { _comboBox.MaxLength = value; }
        }

        [Category("Behavior")]
        [Description("Specifies whether the items in the list portion of the KryptonComboBox are sorted.")]
        public bool Sorted
        {
            get { return _comboBox.Sorted; }
            set { _comboBox.Sorted = value; }
        }

        [Category("Data")]
        [Description("The items in the KryptonComboBox.")]
        public ComboBox.ObjectCollection Items
        {
            get { return _comboBox.Items; }
        }

        [Category("Visuals")]
        [Description("Input control style.")]
        public InputControlStyle InputControlStyle
        {
            get { return _comboBox.InputControlStyle; }
            set { _comboBox.InputControlStyle = value; }
        }

        [Category("Visuals")]
        [Description("DropButton style.")]
        public ButtonStyle DropButtonStyle
        {
            get { return _comboBox.DropButtonStyle; }
            set { _comboBox.DropButtonStyle = value; }
        }

        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        public bool AllowButtonSpecToolTips
        {
            get { return _comboBox.AllowButtonSpecToolTips; }
            set { _comboBox.AllowButtonSpecToolTips = value; }
        }

        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        public KryptonComboBox.ComboBoxButtonSpecCollection ButtonSpecs
        {
            get { return _comboBox.ButtonSpecs; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common combobox appearance that other states can override.")]
        public PaletteComboBoxRedirect StateCommon
        {
            get { return _comboBox.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled combobox appearance.")]
        public PaletteComboBoxStates StateDisabled
        {
            get { return _comboBox.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal combobox appearance.")]
        public PaletteComboBoxStates StateNormal
        {
            get { return _comboBox.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining active combobox appearance.")]
        public PaletteComboBoxJustComboStates StateActive
        {
            get { return _comboBox.StateActive; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _comboBox.Size; }
            set { _comboBox.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _comboBox.Location; }
            set { _comboBox.Location = value; }
        }
    }
}
