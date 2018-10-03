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

namespace KryptonDomainUpDownExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setup the property grid to edit this domain upo-down control
            propertyGrid.SelectedObject = new KryptonDomainUpDownProxy(dud1);
        }

        private void dud_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this domain upo-down control
            propertyGrid.SelectedObject = new KryptonDomainUpDownProxy(sender as KryptonDomainUpDown);
        }

        private void buttonOffice2010Blue_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        private void buttonOffice2007Blue_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
        }

        private void buttonSystem_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.ProfessionalSystem;
        }

        private void buttonSparkleBlue_Click(object sender, EventArgs e)
        {
            kryptonManager1.GlobalPaletteMode = PaletteModeManager.SparkleBlue;
        }

        private void buttonSpecAny1_Click(object sender, EventArgs e)
        {
            dud5.Text = string.Empty;
        }

        private void buttonSpecAny2_Click(object sender, EventArgs e)
        {
            dud6.Text = string.Empty;

        }
        private void buttonSpecAny6_Click(object sender, EventArgs e)
        {
            dud11.Text = string.Empty;
        }

        private void buttonSpecAny4_Click(object sender, EventArgs e)
        {
            dud12.Text = string.Empty;
        }

        private void contextMenuClicked(object sender, EventArgs e)
        {
            KryptonContextMenuItem item = (KryptonContextMenuItem)sender;
            dud6.Text = item.Text;
            dud12.Text = item.Text;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonDomainUpDownProxy
    {
        private KryptonDomainUpDown _domainUpDown;

        public KryptonDomainUpDownProxy(KryptonDomainUpDown domainUpDown)
        {
            _domainUpDown = domainUpDown;
        }

        [Category("Visuals")]
        [Description("Determines if the control is always active or only when the mouse is over the control or has focus.")]
        public bool AlwaysActive
        {
            get { return _domainUpDown.AlwaysActive; }
            set { _domainUpDown.AlwaysActive = value; }
        }

        [Category("Appearance")]
        [Description("Indicates how the text should be aligned for edit controls.")]
        public HorizontalAlignment TextAlign
        {
            get { return _domainUpDown.TextAlign; }
            set { _domainUpDown.TextAlign = value; }
        }


        [Category("Behavior")]
        [Description("Controls whether the text in the edit control can be changed or not.")]
        public bool ReadOnly
        {
            get { return _domainUpDown.ReadOnly; }
            set { _domainUpDown.ReadOnly = value; }
        }

        [Category("Visuals")]
        [Description("Input control style.")]
        public InputControlStyle InputControlStyle
        {
            get { return _domainUpDown.InputControlStyle; }
            set { _domainUpDown.InputControlStyle = value; }
        }

        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        public bool AllowButtonSpecToolTips
        {
            get { return _domainUpDown.AllowButtonSpecToolTips; }
            set { _domainUpDown.AllowButtonSpecToolTips = value; }
        }

        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        public KryptonDomainUpDown.DomainUpDownButtonSpecCollection ButtonSpecs
        {
            get { return _domainUpDown.ButtonSpecs; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common textbox appearance that other states can override.")]
        public PaletteInputControlTripleRedirect StateCommon
        {
            get { return _domainUpDown.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled textbox appearance.")]
        public PaletteInputControlTripleStates StateDisabled
        {
            get { return _domainUpDown.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal textbox appearance.")]
        public PaletteInputControlTripleStates StateNormal
        {
            get { return _domainUpDown.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining active textbox appearance.")]
        public PaletteInputControlTripleStates StateActive
        {
            get { return _domainUpDown.StateActive; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _domainUpDown.Size; }
            set { _domainUpDown.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _domainUpDown.Location; }
            set { _domainUpDown.Location = value; }
        }

        /// <summary>
        /// Gets or sets how the up-down control will position the up down buttons relative to its text box.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates how the up-down control will position the up down buttons relative to its text box.")]
        public LeftRightAlignment UpDownAlign
        {
            get { return _domainUpDown.UpDownAlign; }
            set { _domainUpDown.UpDownAlign = value; }
        }

        /// <summary>
        /// Gets or sets whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the up-down control will increment and decrement the value when the UP ARROW and DOWN ARROW are used.")]
        public bool InterceptArrowKeys
        {
            get { return _domainUpDown.InterceptArrowKeys; }
            set { _domainUpDown.InterceptArrowKeys = value; }
        }

        /// <summary>
        /// Gets and sets the up and down buttons style.
        /// </summary>
        [Category("Visuals")]
        [Description("Up and down buttons style.")]
        public ButtonStyle UpDownButtonStyle
        {
            get { return _domainUpDown.UpDownButtonStyle; }
            set { _domainUpDown.UpDownButtonStyle = value; }
        }

        /// <summary>
        /// Gets or sets the text for the control.
        /// </summary>
        [Category("Appearance")]
        [Description("Domain display text.")]
        public string Text
        {
            get { return _domainUpDown.Text; }
            set { _domainUpDown.Text = value; }
        }

        /// <summary>
        /// Gets or the collection of allowable items of the domain up down.
        /// </summary>
        [Category("Data")]
        [Description("The allowable items of the domain up down.")]
        public DomainUpDown.DomainUpDownItemCollection Items
        {
            get { return _domainUpDown.Items; }
        }
    }
}
