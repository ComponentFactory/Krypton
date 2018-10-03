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

namespace KryptonSplitContainerExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this split container
            propertyGrid.SelectedObject = new KryptonSplitContainerProxy(splitContainer1Office);
        }

        private void splitContainer_MouseDown(object sender, MouseEventArgs e)
        {
            // Setup the property grid to edit this split container
            propertyGrid.SelectedObject = new KryptonSplitContainerProxy(sender as KryptonSplitContainer);
        }

        private void splitContainerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            KryptonPanel panel = (KryptonPanel)sender;

            // Setup the property grid to edit this panels parent split container
            propertyGrid.SelectedObject = new KryptonSplitContainerProxy(panel.Parent as KryptonSplitContainer);
        }

        private void splitContainerLabel_MouseDown(object sender, MouseEventArgs e)
        {
            KryptonLabel label = (KryptonLabel)sender;
            KryptonPanel panel = (KryptonPanel)label.Parent;

            // Setup the property grid to edit this panels parent split container
            propertyGrid.SelectedObject = new KryptonSplitContainerProxy(panel.Parent as KryptonSplitContainer);
        }

        private void splitContainerGroupLabel_MouseDown(object sender, MouseEventArgs e)
        {
            KryptonLabel label = (KryptonLabel)sender;
            KryptonPanel panel = (KryptonPanel)label.Parent;

            // Setup the property grid to edit this panels parent split container
            propertyGrid.SelectedObject = new KryptonSplitContainerProxy(panel.Parent.Parent.Parent as KryptonSplitContainer);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonSplitContainerProxy
    {
        private KryptonSplitContainer _splitContainer;

        public KryptonSplitContainerProxy(KryptonSplitContainer splitContainer)
        {
            _splitContainer = splitContainer;
        }

        [Category("Visuals")]
        [Description("Container background style.")]
        [DefaultValue(typeof(PaletteBackStyle), "PanelClient")]
        public PaletteBackStyle ContainerBackStyle
        {
            get { return _splitContainer.ContainerBackStyle; }
            set { _splitContainer.ContainerBackStyle = value; }
        }

        [Category("Visuals")]
        [Description("Separator style.")]
        [DefaultValue(typeof(SeparatorStyle), "LowProfile")]
        public SeparatorStyle SeparatorStyle
        {
            get { return _splitContainer.SeparatorStyle; }
            set { _splitContainer.SeparatorStyle = value; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common split container appearance that other states can override.")]
        public PaletteSplitContainerRedirect StateCommon
        {
            get { return _splitContainer.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled split container appearance.")]
        public PaletteSplitContainer StateDisabled
        {
            get { return _splitContainer.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal split container appearance.")]
        public PaletteSplitContainer StateNormal
        {
            get { return _splitContainer.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining hot tracking separator appearance.")]
        public PaletteSeparatorPadding StateTracking
        {
            get { return _splitContainer.StateTracking; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining pressed separator appearance.")]
        public PaletteSeparatorPadding StatePressed
        {
            get { return _splitContainer.StatePressed; }
        }

        [Category("Layout")]
        [Description("Determines the minimum distance of pixels of the splitter from the left or top edge of Panel1.")]
        [Localizable(true)]
        [DefaultValue(typeof(int), "25")]
        public int Panel1MinSize
        {
            get { return _splitContainer.Panel1MinSize; }
            set { _splitContainer.Panel1MinSize = value; }
        }

        [Category("Layout")]
        [Description("Determines if Panel1 is collapsed.")]
        [DefaultValue(false)]
        public bool Panel1Collapsed
        {
            get { return _splitContainer.Panel1Collapsed; }
            set { _splitContainer.Panel1Collapsed = value; }
        }

        [Category("Layout")]
        [Description("Determines the minimum distance of pixels of the splitter from the right or bottom edge of Panel2.")]
        [Localizable(true)]
        [DefaultValue(typeof(int), "25")]
        public int Panel2MinSize
        {
            get { return _splitContainer.Panel2MinSize; }
            set { _splitContainer.Panel2MinSize = value; }
        }

        [Category("Layout")]
        [Description("Determines if Panel2 is collapsed.")]
        [DefaultValue(false)]
        public bool Panel2Collapsed
        {
            get { return _splitContainer.Panel2Collapsed; }
            set { _splitContainer.Panel2Collapsed = value; }
        }

        [Category("Layout")]
        [Description("Determines if the splitter is fixed.")]
        [Localizable(true)]
        [DefaultValue(false)]
        public bool IsSplitterFixed
        {
            get { return _splitContainer.IsSplitterFixed; }
            set { _splitContainer.IsSplitterFixed = value; }
        }

        [Category("Layout")]
        [Description("Indicates the panel to keep the same size when resizing.")]
        [DefaultValue(typeof(FixedPanel), "None")]
        public FixedPanel FixedPanel
        {
            get { return _splitContainer.FixedPanel; }
            set { _splitContainer.FixedPanel = value; }
        }

        [Category("Layout")]
        [Description("Determines pixel distance of the splitter from the left or top edge.")]
        [Localizable(true)]
        [SettingsBindable(true)]
        [DefaultValue(typeof(int), "50")]
        public int SplitterDistance
        {
            get { return _splitContainer.SplitterDistance; }
            set { _splitContainer.SplitterDistance = value; }
        }

        [Category("Layout")]
        [Description("Determines the thickness of the splitter.")]
        [Localizable(true)]
        [DefaultValue(typeof(int), "4")]
        public int SplitterWidth
        {
            get { return _splitContainer.SplitterWidth; }
            set { _splitContainer.SplitterWidth = value; }
        }

        [Category("Layout")]
        [Description("Determines the number of pixels the splitter moves in increments.")]
        [Localizable(true)]
        [DefaultValue(typeof(int), "1")]
        public int SplitterIncrement
        {
            get { return _splitContainer.SplitterIncrement; }
            set { _splitContainer.SplitterIncrement = value; }
        }

        [Category("Behavior")]
        [Description("Determines if the splitter is vertical or horizontal.")]
        [Localizable(true)]
        [DefaultValue(typeof(Orientation), "Vertical")]
        public Orientation Orientation
        {
            get { return _splitContainer.Orientation; }
            set { _splitContainer.Orientation = value; }
        }

        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        [DefaultValue(typeof(PaletteMode), "Global")]
        public PaletteMode PaletteMode
        {
            get { return _splitContainer.PaletteMode; }
            set { _splitContainer.PaletteMode = value; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _splitContainer.Size; }
            set { _splitContainer.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _splitContainer.Location; }
            set { _splitContainer.Location = value; }
        }
    }
}
