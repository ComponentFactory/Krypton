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

namespace KryptonRichTextBoxExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load the RTF into the controls
            kryptonRichTextBox3Blue.Rtf = Properties.Resources.Document;
            kryptonRichTextBox6System.Rtf = Properties.Resources.Document;
        
            // Setup the property grid to edit this rich text box
            propertyGrid.SelectedObject = new KryptonRichTextBoxProxy(kryptonRichTextBox1Blue);
        }

        private void kryptonRichTextBox1Blue_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this rich text box
            propertyGrid.SelectedObject = new KryptonRichTextBoxProxy(sender as KryptonRichTextBox);
        }

        private void buttonSpecAny1_Click(object sender, EventArgs e)
        {
            kryptonRichTextBox2Blue.Text = string.Empty;
        }

        private void buttonSpecAny3_Click(object sender, EventArgs e)
        {
            kryptonRichTextBox5System.Text = string.Empty;
        }

        private void fixedText1_Click(object sender, EventArgs e)
        {
            kryptonRichTextBox9Custom.Text = "Fixed Text 1";
            kryptonRichTextBox9Custom.RichTextBox.Focus();
        }

        private void fixedText2_Click(object sender, EventArgs e)
        {
            kryptonRichTextBox9Custom.Text = "Fixed Text 2";
            kryptonRichTextBox9Custom.RichTextBox.Focus();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            kryptonRichTextBox9Custom.Clear();
            kryptonRichTextBox9Custom.RichTextBox.Focus();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonRichTextBoxProxy
    {
        private KryptonRichTextBox _richTextBox;

        public KryptonRichTextBoxProxy(KryptonRichTextBox textBox)
        {
            _richTextBox = textBox;
        }

        [Category("Appearance")]
        [Description("Text for display inside the control.")]
        public string Text
        {
            get { return _richTextBox.Text; }
            set { _richTextBox.Text = value; }
        }

        [Category("Visuals")]
        [Description("Determines if the control is always active or only when the mouse is over the control or has focus.")]
        public bool AlwaysActive
        {
            get { return _richTextBox.AlwaysActive; }
            set { _richTextBox.AlwaysActive = value; }
        }

        [Category("Appearance")]
        [Description("Indicates, for multiline edit controls, which scroll bars will be shown for this control.")]
        public RichTextBoxScrollBars ScrollBars
        {
            get { return _richTextBox.ScrollBars; }
            set { _richTextBox.ScrollBars = value; }
        }

        [Category("Behavior")]
        [Description("Indicates if lines are automatically word-wrapped for multiline edit controls.")]
        public bool WordWrap
        {
            get { return _richTextBox.WordWrap; }
            set { _richTextBox.WordWrap = value; }
        }

        [Category("Behavior")]
        [Description("Control whether the text in the control can span more than one line.")]
        public bool Multiline
        {
            get { return _richTextBox.Multiline; }
            set { _richTextBox.Multiline = value; }
        }

        [Category("Behavior")]
        [Description("Defines the right margin dimensions.")]
        public int RightMargin
        {
            get { return _richTextBox.RightMargin; }
            set { _richTextBox.RightMargin = value; }
        }

        [Category("Behavior")]
        [Description("Turns on/off the selection margin.")]
        public bool ShowSelectionMargin
        {
            get { return _richTextBox.ShowSelectionMargin; }
            set { _richTextBox.ShowSelectionMargin = value; }
        }

        [Category("Behavior")]
        [Description("Defines the current scaling factor of the KryptonRichTextBox display; 1.0 is normal viewing.")]
        public float ZoomFactor
        {
            get { return _richTextBox.ZoomFactor; }
            set { _richTextBox.ZoomFactor = value; }
        }

        [Category("Behavior")]
        [Description("Indicates if tab characters are accepted as input for multiline edit controls.")]
        public bool AcceptsTab
        {
            get { return _richTextBox.AcceptsTab; }
            set { _richTextBox.AcceptsTab = value; }
        }

        [Category("Behavior")]
        [Description("Indicates that the selection should be hidden when the edit control loses focus.")]
        public bool HideSelection
        {
            get { return _richTextBox.HideSelection; }
            set { _richTextBox.HideSelection = value; }
        }

        [Category("Behavior")]
        [Description("Specifies the maximum number of characters that can be entered into the edit control.")]
        public int MaxLength
        {
            get { return _richTextBox.MaxLength; }
            set { _richTextBox.MaxLength = value; }
        }

        [Category("Behavior")]
        [Description("Turns on/off automatic word selection.")]
        public bool AutoWordSelection
        {
            get { return _richTextBox.AutoWordSelection; }
            set { _richTextBox.AutoWordSelection = value; }
        }

        [Category("Behavior")]
        [Description("Defines the indent for bullets in the control.")]
        public int BulletIndent
        {
            get { return _richTextBox.BulletIndent; }
            set { _richTextBox.BulletIndent = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether URLs are automatically formatted as links.")]
        public bool DetectUrls
        {
            get { return _richTextBox.DetectUrls; }
            set { _richTextBox.DetectUrls = value; }
        }

        [Category("Behavior")]
        [Description("Controls whether the text in the edit control can be changed or not.")]
        public bool ReadOnly
        {
            get { return _richTextBox.ReadOnly; }
            set { _richTextBox.ReadOnly = value; }
        }

        [Category("Visuals")]
        [Description("Input control style.")]
        public InputControlStyle InputControlStyle
        {
            get { return _richTextBox.InputControlStyle; }
            set { _richTextBox.InputControlStyle = value; }
        }

        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        public bool AllowButtonSpecToolTips
        {
            get { return _richTextBox.AllowButtonSpecToolTips; }
            set { _richTextBox.AllowButtonSpecToolTips = value; }
        }

        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        public KryptonRichTextBox.RichTextBoxButtonSpecCollection ButtonSpecs
        {
            get { return _richTextBox.ButtonSpecs; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common textbox appearance that other states can override.")]
        public PaletteInputControlTripleRedirect StateCommon
        {
            get { return _richTextBox.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled textbox appearance.")]
        public PaletteInputControlTripleStates StateDisabled
        {
            get { return _richTextBox.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal textbox appearance.")]
        public PaletteInputControlTripleStates StateNormal
        {
            get { return _richTextBox.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining active textbox appearance.")]
        public PaletteInputControlTripleStates StateActive
        {
            get { return _richTextBox.StateActive; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _richTextBox.Size; }
            set { _richTextBox.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _richTextBox.Location; }
            set { _richTextBox.Location = value; }
        }
    }
}
