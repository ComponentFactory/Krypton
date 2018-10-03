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

namespace KryptonBorderEdgeExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this border edge
            propertyGrid.SelectedObject = new KryptonBorderEdgeProxy(borderEdgeH1);
        }

        private void buttonOffice2010Blue_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2010Blue;
            propertyGrid.SelectedObject = kryptonPaletteOffice2010Blue;
        }

        private void buttonOffice2007Blue_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteOffice2007Blue;
            propertyGrid.SelectedObject = kryptonPaletteOffice2007Blue;
        }

        private void buttonSparkle_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSparkle;
            propertyGrid.SelectedObject = kryptonPaletteSparkle;
        }

        private void buttonSystem_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteSystem;
            propertyGrid.SelectedObject = kryptonPaletteSystem;
        }

        private void buttonCustom_Click(object sender, EventArgs e)
        {
            kryptonManager.GlobalPalette = kryptonPaletteCustom;
            propertyGrid.SelectedObject = kryptonPaletteCustom;
        }

        private void button_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this button
            propertyGrid.SelectedObject = new KryptonButtonProxy(sender as KryptonButton);
        }

        private void checkButton_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this check button
            propertyGrid.SelectedObject = new KryptonCheckButtonProxy(sender as KryptonCheckButton);
        }

        private void borderEdge_MouseDown(object sender, MouseEventArgs e)
        {
            // Setup the property grid to edit this border edge
            propertyGrid.SelectedObject = new KryptonBorderEdgeProxy(sender as KryptonBorderEdge);
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
            set { _button.Orientation = value; }
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

    public class KryptonBorderEdgeProxy
    {
        private KryptonBorderEdge _borderEdge;

        public KryptonBorderEdgeProxy(KryptonBorderEdge borderEdge)
        {
            _borderEdge = borderEdge;
        }

        [Category("Visuals")]
        [Description("Border style.")]
        [DefaultValue(typeof(PaletteBorderStyle), "ControlClient")]
        public PaletteBorderStyle BorderStyle
        {
            get { return _borderEdge.BorderStyle; }
            set { _borderEdge.BorderStyle = value; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common border edge appearance that other states can override.")]
        public PaletteBorderEdgeRedirect StateCommon
        {
            get { return _borderEdge.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled border edge appearance.")]
        public PaletteBorderEdge StateDisabled
        {
            get { return _borderEdge.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal border edge appearance.")]
        public PaletteBorderEdge StateNormal
        {
            get { return _borderEdge.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Visual orientation of the control.")]
        [DefaultValue(typeof(Orientation), "Horizontal")]
        public Orientation Orientation
        {
            get { return _borderEdge.Orientation; }
            set { _borderEdge.Orientation = value; }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public PaletteMode PaletteMode
        {
            get { return _borderEdge.PaletteMode; }
            set { _borderEdge.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("Specifies whether a control will automatically size itself to fit its contents.")]
        [DefaultValue(false)]
        public bool AutoSize
        {
            get { return _borderEdge.AutoSize; }
            set { _borderEdge.AutoSize = value; }
        }

        [Category("Layout")]
        [Description("Specifies if the control grows and shrinks to fit the contents exactly.")]
        [DefaultValue(typeof(AutoSizeMode), "GrowOnly")]
        public AutoSizeMode AutoSizeMode
        {
            get { return _borderEdge.AutoSizeMode; }
            set { _borderEdge.AutoSizeMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _borderEdge.Size; }
            set { _borderEdge.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _borderEdge.Location; }
            set { _borderEdge.Location = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the control is enabled.")]
        public bool Enabled
        {
            get { return _borderEdge.Enabled; }
            set { _borderEdge.Enabled = value; }
        }
    }
}
