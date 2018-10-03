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
using System.Globalization;
using ComponentFactory.Krypton.Toolkit;

namespace KryptonMaskedTextBoxExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            maskedTextBox1Blue.ValidatingType = typeof(DateTime);
            maskedTextBox3Blue.ValidatingType = typeof(DateTime);
            maskedTextBox4System.ValidatingType = typeof(DateTime);
            maskedTextBox6System.ValidatingType = typeof(DateTime);

            // Setup the property grid to edit this masked text box
            propertyGrid.SelectedObject = new KryptonMaskedTextBoxProxy(maskedTextBox1Blue);
        }

        private void maskedTextBox1Blue_Enter(object sender, EventArgs e)
        {
            // Setup the property grid to edit this masked text box
            propertyGrid.SelectedObject = new KryptonMaskedTextBoxProxy(sender as KryptonMaskedTextBox);
        }

        private void buttonSpecAny1_Click(object sender, EventArgs e)
        {
            maskedTextBox2Blue.Text = string.Empty;
        }

        private void buttonSpecAny3_Click(object sender, EventArgs e)
        {
            maskedTextBox5System.Text = string.Empty;
        }

        private void fixedText1_Click(object sender, EventArgs e)
        {
            maskedTextBox9Custom.Text = DateTime.Now.ToShortDateString();
            maskedTextBox9Custom.MaskedTextBox.Focus();
        }

        private void fixedText2_Click(object sender, EventArgs e)
        {
            maskedTextBox9Custom.Text = "01/01/1950";
            maskedTextBox9Custom.MaskedTextBox.Focus();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            maskedTextBox9Custom.Text = string.Empty;
            maskedTextBox9Custom.MaskedTextBox.Focus();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class KryptonMaskedTextBoxProxy
    {
        private KryptonMaskedTextBox _maskedTextBox;

        public KryptonMaskedTextBoxProxy(KryptonMaskedTextBox maskedTextBox)
        {
            _maskedTextBox = maskedTextBox;
        }

        [Category("Appearance")]
        [Description("Text for display inside the control.")]
        public string Text
        {
            get { return _maskedTextBox.Text; }
            set { _maskedTextBox.Text = value; }
        }

        [Category("Visuals")]
        [Description("Determines if the control is always active or only when the mouse is over the control or has focus.")]
        public bool AlwaysActive
        {
            get { return _maskedTextBox.AlwaysActive; }
            set { _maskedTextBox.AlwaysActive = value; }
        }

        [Category("Appearance")]
        [Description("Indicates how the text should be aligned for edit controls.")]
        public HorizontalAlignment TextAlign
        {
            get { return _maskedTextBox.TextAlign; }
            set { _maskedTextBox.TextAlign = value; }
        }


        [Category("Appearance")]
        [Description("Indicates the character used as the placeholder.")]
        public char PromptChar
        {
            get { return _maskedTextBox.PromptChar; }
            set { _maskedTextBox.PromptChar = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the prompt character is valid as input.")]
        public bool AllowPromptAsInput
        {
            get { return _maskedTextBox.AllowPromptAsInput; }
            set { _maskedTextBox.AllowPromptAsInput = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether only Ascii characters are valid as input.")]
        public bool AsciiOnly
        {
            get { return _maskedTextBox.AsciiOnly; }
            set { _maskedTextBox.AsciiOnly = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the control will beep when an invalid character is typed.")]
        public bool BeepOnError
        {
            get { return _maskedTextBox.BeepOnError; }
            set { _maskedTextBox.BeepOnError = value; }
        }

        [Category("Behavior")]
        [Description("The culture that determines the value of the locaizable mask language separators and placeholders.")]
        public CultureInfo Culture
        {
            get { return _maskedTextBox.Culture; }
            set { _maskedTextBox.Culture = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the text to be copied to the clipboard includes literals and/or prompt characters.")]
        public MaskFormat CutCopyMaskFormat
        {
            get { return _maskedTextBox.CutCopyMaskFormat; }
            set { _maskedTextBox.CutCopyMaskFormat = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether prompt characters are displayed when the control does not have focus.")]
        public bool HidePromptOnLeave
        {
            get { return _maskedTextBox.HidePromptOnLeave; }
            set { _maskedTextBox.HidePromptOnLeave = value; }
        }

        [Category("Behavior")]
        [Description("Indicates the masked text box input character typing mode.")]
        public InsertKeyMode InsertKeyMode
        {
            get { return _maskedTextBox.InsertKeyMode; }
            set { _maskedTextBox.InsertKeyMode = value; }
        }

        [Category("Behavior")]
        [Description("Sets the string governing the input allowed for the control.")]
        public string Mask
        {
            get { return _maskedTextBox.Mask; }
            set { _maskedTextBox.Mask = value; }
        }

        [Category("Behavior")]
        [Description("Indicates that the selection should be hidden when the edit control loses focus.")]
        public bool HideSelection
        {
            get { return _maskedTextBox.HideSelection; }
            set { _maskedTextBox.HideSelection = value; }
        }

        [Category("Behavior")]
        [Description("Controls whether the text in the edit control can be changed or not.")]
        public bool ReadOnly
        {
            get { return _maskedTextBox.ReadOnly; }
            set { _maskedTextBox.ReadOnly = value; }
        }

        [Category("Behavior")]
        [Description("If true, the input is rejected whenever a character fails to comply with the mask; otherwise, characters in the text area are processed one by one as individual inputs.")]
        public bool RejectInputOnFirstFailure
        {
            get { return _maskedTextBox.RejectInputOnFirstFailure; }
            set { _maskedTextBox.RejectInputOnFirstFailure = value; }
        }

        [Category("Behavior")]
        [Description("Specifies whether to reset and skip the current position if editable, when the input characters has the same value as the prompt.")]
        public bool ResetOnPrompt
        {
            get { return _maskedTextBox.ResetOnPrompt; }
            set { _maskedTextBox.ResetOnPrompt = value; }
        }

        [Category("Behavior")]
        [Description("Specifies whether to reset and skip the current position if editable, when the input is the space character.")]
        public bool ResetOnSpace
        {
            get { return _maskedTextBox.ResetOnSpace; }
            set { _maskedTextBox.ResetOnSpace = value; }
        }

        [Category("Behavior")]
        [Description("Specifies whether to skip the current position if non-editable and the input character has the same value as the literal at that position.")]
        public bool SkipLiterals
        {
            get { return _maskedTextBox.SkipLiterals; }
            set { _maskedTextBox.SkipLiterals = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether the string returned from the Text property includes literal and/or prompt characters.")]
        public MaskFormat TextMaskFormat
        {
            get { return _maskedTextBox.TextMaskFormat; }
            set { _maskedTextBox.TextMaskFormat = value; }
        }

        [Category("Behavior")]
        [Description("Indicates whether shortcuts defined for the control are enabled.")]
        public bool ShortcutsEnabled
        {
            get { return _maskedTextBox.ShortcutsEnabled; }
            set { _maskedTextBox.ShortcutsEnabled = value; }
        }
        
        [Category("Behavior")]
        [Description("Indicates the character to display for password input for single-line edit controls.")]
        public char PasswordChar
        {
            get { return _maskedTextBox.PasswordChar; }
            set { _maskedTextBox.PasswordChar = value; }
        }

        [Category("Behavior")]
        [Description("Indicates if the text in the edit control should appear as the default password character.")]
        public bool UseSystemPasswordChar
        {
            get { return _maskedTextBox.UseSystemPasswordChar; }
            set { _maskedTextBox.UseSystemPasswordChar = value; }
        }

        [Category("Visuals")]
        [Description("Input control style.")]
        public InputControlStyle InputControlStyle
        {
            get { return _maskedTextBox.InputControlStyle; }
            set { _maskedTextBox.InputControlStyle = value; }
        }

        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        public bool AllowButtonSpecToolTips
        {
            get { return _maskedTextBox.AllowButtonSpecToolTips; }
            set { _maskedTextBox.AllowButtonSpecToolTips = value; }
        }

        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        public KryptonMaskedTextBox.MaskedTextBoxButtonSpecCollection ButtonSpecs
        {
            get { return _maskedTextBox.ButtonSpecs; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining common textbox appearance that other states can override.")]
        public PaletteInputControlTripleRedirect StateCommon
        {
            get { return _maskedTextBox.StateCommon; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining disabled textbox appearance.")]
        public PaletteInputControlTripleStates StateDisabled
        {
            get { return _maskedTextBox.StateDisabled; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining normal textbox appearance.")]
        public PaletteInputControlTripleStates StateNormal
        {
            get { return _maskedTextBox.StateNormal; }
        }

        [Category("Visuals")]
        [Description("Overrides for defining active textbox appearance.")]
        public PaletteInputControlTripleStates StateActive
        {
            get { return _maskedTextBox.StateActive; }
        }

        [Category("Layout")]
        [Description("The size of the control is pixels.")]
        public Size Size
        {
            get { return _maskedTextBox.Size; }
            set { _maskedTextBox.Size = value; }
        }

        [Category("Layout")]
        [Description("The location of the control in pixels.")]
        public Point Location
        {
            get { return _maskedTextBox.Location; }
            set { _maskedTextBox.Location = value; }
        }
    }
}
