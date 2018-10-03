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

namespace KryptonPanelExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this panel
            propertyGrid.SelectedObject = new KryptonPanelProxy(panel1Office);
        }

        private void kryptonPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // Setup the property grid to edit this panel
            propertyGrid.SelectedObject = new KryptonPanelProxy(sender as KryptonPanel);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonPanelProxy
    {
        private KryptonPanel _panel;

        public KryptonPanelProxy(KryptonPanel panel)
        {
            _panel = panel;
        }

        [Category("Visuals")]
        [Description("Panel style.")]
        [DefaultValue(typeof(PaletteBackStyle), "PanelClient")]
        public PaletteBackStyle PanelBackStyle
        {
            get { return _panel.PanelBackStyle; }
            set { _panel.PanelBackStyle = value; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common panel appearance that other states can override.")]
        public PaletteBack StateCommon
        {
            get { return _panel.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled panel appearance.")]
        public PaletteBack StateDisabled
        {
            get { return _panel.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal panel appearance.")]
        public PaletteBack StateNormal
        {
            get { return _panel.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public PaletteMode PaletteMode
        {
            get { return _panel.PaletteMode; }
            set { _panel.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _panel.Size; }
            set { _panel.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _panel.Location; }
            set { _panel.Location = value; }
        }
    }
}
