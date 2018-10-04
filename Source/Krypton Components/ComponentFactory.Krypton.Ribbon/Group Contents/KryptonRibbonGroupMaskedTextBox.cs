// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2017. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.IO;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Represents a ribbon group masked text box.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupMaskedTextBox), "ToolboxBitmaps.KryptonRibbonGroupMaskedTextBox.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupMaskedTextBoxDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Mask")]
    public class KryptonRibbonGroupMaskedTextBox : KryptonRibbonGroupItem
    {
        #region Instance Fields
        private bool _visible;
        private bool _enabled;
        private string _keyTip;
        private Keys _shortcutKeys;
        private GroupItemSize _itemSizeCurrent;
        private NeedPaintHandler _viewPaintDelegate;
        private KryptonMaskedTextBox _maskedTextBox;
        private KryptonMaskedTextBox _lastMaskedTextBox;
        private IKryptonDesignObject _designer;
        private Control _lastParentControl;
        private ViewBase _maskedTextBoxView;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the control receives focus.
        /// </summary>
        [Browsable(false)]
        public event EventHandler GotFocus;

        /// <summary>
        /// Occurs when the control loses focus.
        /// </summary>
        [Browsable(false)]
        public event EventHandler LostFocus;

        /// <summary>
        /// Occurs when the value of the Text property changes.
        /// </summary>
        [Description("Occurs when the value of the Text property changes.")]
        [Category("Property Changed")]
        public event EventHandler TextChanged;

        /// <summary>
        /// Occurs when a key is pressed while the control has focus. 
        /// </summary>
        [Description("Occurs when a key is pressed while the control has focus.")]
        [Category("Key")]
        public event KeyPressEventHandler KeyPress;

        /// <summary>
        /// Occurs when a key is released while the control has focus. 
        /// </summary>
        [Description("Occurs when a key is released while the control has focus.")]
        [Category("Key")]
        public event KeyEventHandler KeyUp;

        /// <summary>
        /// Occurs when a key is pressed while the control has focus.
        /// </summary>
        [Description("Occurs when a key is pressed while the control has focus.")]
        [Category("Key")]
        public event KeyEventHandler KeyDown;

        /// <summary>
        /// Occurs before the KeyDown event when a key is pressed while focus is on this control.
        /// </summary>
        [Description("Occurs before the KeyDown event when a key is pressed while focus is on this control.")]
        [Category("Key")]
        public event PreviewKeyDownEventHandler PreviewKeyDown;

        /// <summary>
        /// Occurs when the value of the HideSelection property changes.
        /// </summary>
        [Description("Occurs when the value of the HideSelection property changes.")]
        [Category("Property Changed")]
        public event EventHandler HideSelectionChanged;

        /// <summary>
        /// Occurs when the value of the Modified property changes.
        /// </summary>
        [Description("Occurs when the value of the Modified property changes.")]
        [Category("Property Changed")]
        public event EventHandler ModifiedChanged;

        /// <summary>
        /// Occurs when the value of the ReadOnly property changes.
        /// </summary>
        [Description("Occurs when the value of the ReadOnly property changes.")]
        [Category("Property Changed")]
        public event EventHandler ReadOnlyChanged;

        /// <summary>
        /// Occurs when the value of the TextAlign property changes.
        /// </summary>
        [Description("Occurs when the value of the TextAlign property changes.")]
        [Category("Property Changed")]
        public event EventHandler TextAlignChanged;

        /// <summary>
        /// Occurs when the value of the Mask property changes.
        /// </summary>
        [Description("Occurs when the value of the Mask property changes.")]
        [Category("Property Changed")]
        public event EventHandler MaskChanged;

        /// <summary>
        /// Occurs when the value of the IsOverwriteMode property changes.
        /// </summary>
        [Description("Occurs when the value of the IsOverwriteMode property changes.")]
        [Category("Property Changed")]
        public event EventHandler IsOverwriteModeChanged;

        /// <summary>
        /// Occurs when the input character or text does not comply with the mask specification.
        /// </summary>
        [Description("Occurs when the input character or text does not comply with the mask specification.")]
        [Category("Behavior")]
        public event MaskInputRejectedEventHandler MaskInputRejected;

        /// <summary>
        /// Occurs when the validating type object has completed parsing the input text.
        /// </summary>
        [Description("Occurs when the validating type object has completed parsing the input text.")]
        [Category("Focus")]
        public event TypeValidationEventHandler TypeValidationCompleted;

        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs after the value of a property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the design time context menu is requested.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event MouseEventHandler DesignTimeContextMenu;

        internal event EventHandler MouseEnterControl;
        internal event EventHandler MouseLeaveControl;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupMaskedTextBox class.
        /// </summary>
        public KryptonRibbonGroupMaskedTextBox()
        {
            // Default fields
            _visible = true;
            _enabled = true;
            _itemSizeCurrent = GroupItemSize.Medium;
            _shortcutKeys = Keys.None;
            _keyTip = "X";

            // Create the actual masked text box control and set initial settings
            _maskedTextBox = new KryptonMaskedTextBox();
            _maskedTextBox.InputControlStyle = InputControlStyle.Ribbon;
            _maskedTextBox.AlwaysActive = false;
            _maskedTextBox.MinimumSize = new Size(121, 0);
            _maskedTextBox.MaximumSize = new Size(121, 0);
            _maskedTextBox.TabStop = false;

            // Hook into events to expose via our container
            _maskedTextBox.TextAlignChanged += new EventHandler(OnMaskedTextBoxTextAlignChanged);
            _maskedTextBox.TextChanged += new EventHandler(OnMaskedTextBoxTextChanged);
            _maskedTextBox.HideSelectionChanged += new EventHandler(OnMaskedTextBoxHideSelectionChanged);
            _maskedTextBox.ModifiedChanged += new EventHandler(OnMaskedTextBoxModifiedChanged);
            _maskedTextBox.ReadOnlyChanged += new EventHandler(OnMaskedTextBoxReadOnlyChanged);
            _maskedTextBox.MaskChanged += new EventHandler(OnMaskedMaskChanged);
            _maskedTextBox.IsOverwriteModeChanged += new EventHandler(OnMaskedIsOverwriteModeChanged);
            _maskedTextBox.MaskInputRejected += new MaskInputRejectedEventHandler(OnMaskedMaskInputRejected);
            _maskedTextBox.TypeValidationCompleted += new TypeValidationEventHandler(OnMaskedTypeValidationCompleted);
            _maskedTextBox.GotFocus += new EventHandler(OnMaskedTextBoxGotFocus);
            _maskedTextBox.LostFocus += new EventHandler(OnMaskedTextBoxLostFocus);
            _maskedTextBox.KeyDown += new KeyEventHandler(OnMaskedTextBoxKeyDown);
            _maskedTextBox.KeyUp += new KeyEventHandler(OnMaskedTextBoxKeyUp);
            _maskedTextBox.KeyPress += new KeyPressEventHandler(OnMaskedTextBoxKeyPress);
            _maskedTextBox.PreviewKeyDown += new PreviewKeyDownEventHandler(OnMaskedTextBoxPreviewKeyDown);

            // Ensure we can track mouse events on the masked text box
            MonitorControl(_maskedTextBox);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_maskedTextBox != null)
                {
                    UnmonitorControl(_maskedTextBox);
                    _maskedTextBox.Dispose();
                    _maskedTextBox = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the owning ribbon control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override KryptonRibbon Ribbon
        {
            set 
            { 
                base.Ribbon = value;

                if (value != null)
                {
                    // Use the same palette in the masked text box as the ribbon, plus we need
                    // to know when the ribbon palette changes so we can reflect that change
                    _maskedTextBox.Palette = Ribbon.GetResolvedPalette();
                    Ribbon.PaletteChanged += new EventHandler(OnRibbonPaletteChanged);
                }
            }
        }

        /// <summary>
        /// Gets and sets the shortcut key combination.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Shortcut key combination to set focus to the masked text box.")]
        public Keys ShortcutKeys
        {
            get { return _shortcutKeys; }
            set { _shortcutKeys = value; }
        }

        private bool ShouldSerializeShortcutKeys()
        {
            return (ShortcutKeys != Keys.None);
        }

        /// <summary>
        /// Resets the ShortcutKeys property to its default value.
        /// </summary>
        public void ResetShortcutKeys()
        {
            ShortcutKeys = Keys.None;
        }

        /// <summary>
        /// Access to the actual embedded KryptonMaskedTextBox instance.
        /// </summary>
        [Description("Access to the actual embedded KryptonMaskedTextBox instance.")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonMaskedTextBox MaskedTextBox
        {
            get { return _maskedTextBox; }
        }

        /// <summary>
        /// Gets and sets the key tip for the ribbon group masked text box.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group masked text box key tip.")]
        [DefaultValue("X")]
        public string KeyTip
        {
            get { return _keyTip; }

            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "X";

                _keyTip = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the masked text box.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the masked text box is visible or hidden.")]
        [DefaultValue(true)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool Visible
        {
            get { return _visible; }

            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        /// <summary>
        /// Make the ribbon group masked text box visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon group masked text box hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the group masked text box.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group masked text box is enabled.")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return _enabled; }

            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the minimum size of the control.
        /// </summary>
        [Category("Layout")]
        [Description("Specifies the minimum size of the control.")]
        [DefaultValue(typeof(Size), "121, 0")]
        public Size MinimumSize
        {
            get { return _maskedTextBox.MinimumSize; }
            set { _maskedTextBox.MinimumSize = value; }
        }

        /// <summary>
        /// Gets or sets the maximum size of the control.
        /// </summary>
        [Category("Layout")]
        [Description("Specifies the maximum size of the control.")]
        [DefaultValue(typeof(Size), "121, 0")]
        public Size MaximumSize
        {
            get { return _maskedTextBox.MaximumSize; }
            set { _maskedTextBox.MaximumSize = value; }
        }

        /// <summary>
        /// Gets and sets the maximum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeMaximum
        {
            get { return GroupItemSize.Large; }
            set { }
        }

        /// <summary>
        /// Gets and sets the minimum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeMinimum
        {
            get { return GroupItemSize.Small; }
            set { }
        }

        /// <summary>
        /// Gets and sets the current item size.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeCurrent
        {
            get { return _itemSizeCurrent; }

            set
            {
                if (_itemSizeCurrent != value)
                {
                    _itemSizeCurrent = value;
                    OnPropertyChanged("ItemSizeCurrent");
                }
            }
        }

        /// <summary>
        /// Gets and sets the text associated with the control.
        /// </summary>
        [Category("Appearance")]
        [Editor("System.Windows.Forms.Design.MaskedTextBoxTextEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [RefreshProperties(RefreshProperties.All)]
        public string Text
        {
            get { return _maskedTextBox.Text; }
            set { _maskedTextBox.Text = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the contents have changed since last last.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get { return _maskedTextBox.Modified; }
        }

        /// <summary>
        /// Gets and sets the selected text within the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get { return _maskedTextBox.SelectedText; }
            set { _maskedTextBox.SelectedText = value; }
        }

        /// <summary>
        /// Gets and sets the selection length for the selected area.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get { return _maskedTextBox.SelectionLength; }
            set { _maskedTextBox.SelectionLength = value; }
        }

        /// <summary>
        /// Gets and sets the starting point of text selected in the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get { return _maskedTextBox.SelectionStart; }
            set { _maskedTextBox.SelectionStart = value; }
        }

        /// <summary>
        /// Gets the length of text in the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TextLength
        {
            get { return _maskedTextBox.TextLength; }
        }

        /// <summary>
        /// Gets a value that specifies whether new user input overwrites existing input.
        /// </summary>
        [Browsable(false)]
        public bool IsOverwriteMode
        {
            get { return _maskedTextBox.IsOverwriteMode; }
        }

        /// <summary>
        /// Gets a value indicating whether all required inputs have been entered into the input mask.
        /// </summary>
        [Browsable(false)]
        public bool MaskCompleted
        {
            get { return _maskedTextBox.MaskCompleted; }
        }

        /// <summary>
        /// Gets a clone of the mask provider associated with this instance of the masked text box control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MaskedTextProvider MaskedTextProvider
        {
            get { return _maskedTextBox.MaskedTextProvider; }
        }

        /// <summary>
        /// Gets a value indicating whether all required and optional inputs have been entered into the input mask.
        /// </summary>
        [Browsable(false)]
        public bool MaskFull
        {
            get { return _maskedTextBox.MaskFull; }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be entered into the edit control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaxLength
        {
            get { return _maskedTextBox.MaxLength; }
            set { _maskedTextBox.MaxLength = value; }
        }

        /// <summary>
        /// Gets or sets the data type used to verify the data input by the user.
        /// </summary>
        [Browsable(false)]
        [DefaultValue(null)]
        public Type ValidatingType
        {
            get { return _maskedTextBox.ValidatingType; }
            set { _maskedTextBox.ValidatingType = value; }
        }

        /// <summary>
        /// Gets or sets how the text should be aligned for edit controls.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates how the text should be aligned for edit controls.")]
        [DefaultValue(typeof(HorizontalAlignment), "Left")]
        [Localizable(true)]
        public HorizontalAlignment TextAlign
        {
            get { return _maskedTextBox.TextAlign; }
            set { _maskedTextBox.TextAlign = value; }
        }

        /// <summary>
        /// Indicates the character used as the placeholder.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the character used as the placeholder.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue("_")]
        [Localizable(true)]
        public char PromptChar
        {
            get { return _maskedTextBox.PromptChar; }
            set { _maskedTextBox.PromptChar = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether PromptChar can be entered as valid data by the user.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the prompt character is valid as input.")]
        [DefaultValue(true)]
        public bool AllowPromptAsInput
        {
            get { return _maskedTextBox.AllowPromptAsInput; }
            set { _maskedTextBox.AllowPromptAsInput = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the MaskedTextBox control accepts characters outside of the ASCII character set.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether only Ascii characters are valid as input.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        public bool AsciiOnly
        {
            get { return _maskedTextBox.AsciiOnly; }
            set { _maskedTextBox.AsciiOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the masked text box control raises the system beep for each user key stroke that it rejects.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the control will beep when an invalid character is typed.")]
        [DefaultValue(false)]
        public bool BeepOnError
        {
            get { return _maskedTextBox.BeepOnError; }
            set { _maskedTextBox.BeepOnError = value; }
        }

        /// <summary>
        /// Gets or sets the culture information associated with the masked text box.
        /// </summary>
        [Category("Behavior")]
        [Description("The culture that determines the value of the locaizable mask language separators and placeholders.")]
        [RefreshProperties(RefreshProperties.All)]
        public CultureInfo Culture
        {
            get { return _maskedTextBox.Culture; }
            set { _maskedTextBox.Culture = value; }
        }

        private bool ShouldSerializeCulture()
        {
            return !CultureInfo.CurrentCulture.Equals(Culture);
        }

        /// <summary>
        /// Gets or sets a value that determines whether literals and prompt characters are copied to the clipboard.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the text to be copied to the clipboard includes literals and/or prompt characters.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(typeof(MaskFormat), "IncludeLiterals")]
        public MaskFormat CutCopyMaskFormat
        {
            get { return _maskedTextBox.CutCopyMaskFormat; }
            set { _maskedTextBox.CutCopyMaskFormat = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prompt characters in the input mask are hidden when the masked text box loses focus.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether prompt characters are displayed when the control does not have focus.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        public bool HidePromptOnLeave
        {
            get { return _maskedTextBox.HidePromptOnLeave; }
            set { _maskedTextBox.HidePromptOnLeave = value; }
        }

        /// <summary>
        /// Gets or sets the text insertion mode of the masked text box control.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates the masked text box input character typing mode.")]
        [DefaultValue(typeof(InsertKeyMode), "Default")]
        public InsertKeyMode InsertKeyMode
        {
            get { return _maskedTextBox.InsertKeyMode; }
            set { _maskedTextBox.InsertKeyMode = value; }
        }

        /// <summary>
        /// Gets or sets the input mask to use at run time. 
        /// </summary>
        [Category("Behavior")]
        [Description("Sets the string governing the input allowed for the control.")]
        [RefreshProperties(RefreshProperties.All)]
        [MergableProperty(false)]
        [DefaultValue("")]
        [Localizable(true)]
        public string Mask
        {
            get { return _maskedTextBox.Mask; }
            set { _maskedTextBox.Mask = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates that the selection should be hidden when the edit control loses focus.")]
        [DefaultValue(true)]
        public bool HideSelection
        {
            get { return _maskedTextBox.HideSelection; }
            set { _maskedTextBox.HideSelection = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text in the edit control can be changed or not.
        /// </summary>
        [Category("Behavior")]
        [Description("Controls whether the text in the edit control can be changed or not.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return _maskedTextBox.ReadOnly; }
            set { _maskedTextBox.ReadOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the parsing of user input should stop after the first invalid character is reached.
        /// </summary>
        [Category("Behavior")]
        [Description("If true, the input is rejected whenever a character fails to comply with the mask; otherwise, characters in the text area are processed one by one as individual inputs.")]
        [DefaultValue(false)]
        public bool RejectInputOnFirstFailure
        {
            get { return _maskedTextBox.RejectInputOnFirstFailure; }
            set { _maskedTextBox.RejectInputOnFirstFailure = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines how an input character that matches the prompt character should be handled.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether to reset and skip the current position if editable, when the input characters has the same value as the prompt.")]
        [DefaultValue(true)]
        public bool ResetOnPrompt
        {
            get { return _maskedTextBox.ResetOnPrompt; }
            set { _maskedTextBox.ResetOnPrompt = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines how a space input character should be handled.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether to reset and skip the current position if editable, when the input is the space character.")]
        [DefaultValue(true)]
        public bool ResetOnSpace
        {
            get { return _maskedTextBox.ResetOnSpace; }
            set { _maskedTextBox.ResetOnSpace = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is allowed to reenter literal values.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether to skip the current position if non-editable and the input character has the same value as the literal at that position.")]
        [DefaultValue(true)]
        public bool SkipLiterals
        {
            get { return _maskedTextBox.SkipLiterals; }
            set { _maskedTextBox.SkipLiterals = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether literals and prompt characters are included in the formatted string.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether the string returned from the Text property includes literal and/or prompt characters.")]
        [DefaultValue(typeof(MaskFormat), "IncludeLiterals")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public MaskFormat TextMaskFormat
        {
            get { return _maskedTextBox.TextMaskFormat; }
            set { _maskedTextBox.TextMaskFormat = value; }
        }

        /// <summary>
        /// Gets or sets a the character to display for password input for single-line edit controls.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates the character to display for password input for single-line edit controls.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue('\0')]
        [Localizable(true)]
        public char PasswordChar
        {
            get { return _maskedTextBox.PasswordChar; }
            set { _maskedTextBox.PasswordChar = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating if the text in the edit control should appear as the default password character.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if the text in the edit control should appear as the default password character.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get { return _maskedTextBox.UseSystemPasswordChar; }
            set { _maskedTextBox.UseSystemPasswordChar = value; }
        }

        /// <summary>
        /// Gets and sets the associated context menu strip.
        /// </summary>
        [Category("Behavior")]
        [Description("The shortcut to display when the user right-clicks the control.")]
        [DefaultValue(null)]
        public ContextMenuStrip ContextMenuStrip
        {
            get { return _maskedTextBox.ContextMenuStrip; }
            set { _maskedTextBox.ContextMenuStrip = value; }
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu for showing when the masked textbox is right clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("KryptonContextMenu to be shown when the masked textbox is right clicked.")]
        [DefaultValue(null)]
        public KryptonContextMenu KryptonContextMenu
        {
            get { return _maskedTextBox.KryptonContextMenu; }
            set { _maskedTextBox.KryptonContextMenu = value; }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _maskedTextBox.AllowButtonSpecToolTips; }
            set { _maskedTextBox.AllowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonMaskedTextBox.MaskedTextBoxButtonSpecCollection ButtonSpecs
        {
            get { return _maskedTextBox.ButtonSpecs; }
        }

        /// <summary>
        /// Creates an appropriate view element for this item.
        /// </summary>
        /// <param name="ribbon">Reference to the owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying changes in display.</param>
        /// <returns>ViewBase derived instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ViewBase CreateView(KryptonRibbon ribbon, 
                                            NeedPaintHandler needPaint)
        {
            return new ViewDrawRibbonGroupMaskedTextBox(ribbon, this, needPaint);
        }

        /// <summary>
        /// Gets and sets the associated designer.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public IKryptonDesignObject MaskedTextBoxDesigner
        {
            get { return _designer; }
            set { _designer = value; }
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase MaskedTextBoxView
        {
            get { return _maskedTextBoxView; }
            set { _maskedTextBoxView = value; }
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the TextChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnGotFocus(EventArgs e)
        {
            if (GotFocus != null)
                GotFocus(this, e);
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnLostFocus(EventArgs e)
        {
            if (LostFocus != null)
                LostFocus(this, e);
        }

        /// <summary>
        /// Raises the KeyDown event.
        /// </summary>
        /// <param name="e">An KeyEventArgs containing the event data.</param>
        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            if (KeyDown != null)
                KeyDown(this, e);
        }

        /// <summary>
        /// Raises the KeyUp event.
        /// </summary>
        /// <param name="e">An KeyEventArgs containing the event data.</param>
        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            if (KeyUp != null)
                KeyUp(this, e);
        }

        /// <summary>
        /// Raises the KeyPress event.
        /// </summary>
        /// <param name="e">An KeyPressEventArgs containing the event data.</param>
        protected virtual void OnKeyPress(KeyPressEventArgs e)
        {
            if (KeyPress != null)
                KeyPress(this, e);
        }

        /// <summary>
        /// Raises the PreviewKeyDown event.
        /// </summary>
        /// <param name="e">An PreviewKeyDownEventArgs containing the event data.</param>
        protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (PreviewKeyDown != null)
                PreviewKeyDown(this, e);
        }

        /// <summary>
        /// Raises the HideSelectionChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnHideSelectionChanged(EventArgs e)
        {
            if (HideSelectionChanged != null)
                HideSelectionChanged(this, e);
        }

        /// <summary>
        /// Raises the ModifiedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnModifiedChanged(EventArgs e)
        {
            if (ModifiedChanged != null)
                ModifiedChanged(this, e);
        }

        /// <summary>
        /// Raises the ReadOnlyChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnReadOnlyChanged(EventArgs e)
        {
            if (ReadOnlyChanged != null)
                ReadOnlyChanged(this, e);
        }

        /// <summary>
        /// Raises the MaskChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnMaskChanged(EventArgs e)
        {
            if (MaskChanged != null)
                MaskChanged(this, e);
        }

        /// <summary>
        /// Raises the TextAlignChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnTextAlignChanged(EventArgs e)
        {
            if (TextAlignChanged != null)
                TextAlignChanged(this, e);
        }

        /// <summary>
        /// Raises the IsOverwriteModeChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnIsOverwriteModeChanged(EventArgs e)
        {
            if (IsOverwriteModeChanged != null)
                IsOverwriteModeChanged(this, e);
        }

        /// <summary>
        /// Raises the MaskInputRejected event.
        /// </summary>
        /// <param name="e">An MaskInputRejectedEventArgs that contains the event data.</param>
        protected virtual void OnMaskInputRejected(MaskInputRejectedEventArgs e)
        {
            if (MaskInputRejected != null)
                MaskInputRejected(this, e);
        }

        /// <summary>
        /// Raises the TypeValidationCompleted event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnTypeValidationCompleted(TypeValidationEventArgs e)
        {
            if (TypeValidationCompleted != null)
                TypeValidationCompleted(this, e);
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of property that has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Internal
        internal Control LastParentControl
        {
            get { return _lastParentControl; }
            set { _lastParentControl = value; }
        }

        internal KryptonMaskedTextBox LastMaskedTextBox
        {
            get { return _lastMaskedTextBox; }
            set { _lastMaskedTextBox = value; }
        }

        internal NeedPaintHandler ViewPaintDelegate
        {
            get { return _viewPaintDelegate; }
            set { _viewPaintDelegate = value; }
        }

        internal void OnDesignTimeContextMenu(MouseEventArgs e)
        {
            if (DesignTimeContextMenu != null)
                DesignTimeContextMenu(this, e);
        }

        internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Only interested in key processing if this control definition 
            // is enabled and itself and all parents are also visible
            if (Enabled && ChainVisible)
            {
                // Do we have a shortcut definition for ourself?
                if (ShortcutKeys != Keys.None)
                {
                    // Does it match the incoming key combination?
                    if (ShortcutKeys == keyData)
                    {
                        // Can the masked text box take the focus
                        if ((LastMaskedTextBox != null) && (LastMaskedTextBox.CanFocus))
                            LastMaskedTextBox.MaskedTextBox.Focus();

                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region Implementation
        private void MonitorControl(KryptonMaskedTextBox c)
        {
            c.MouseEnter += new EventHandler(OnControlEnter);
            c.MouseLeave += new EventHandler(OnControlLeave);
            c.TrackMouseEnter += new EventHandler(OnControlEnter);
            c.TrackMouseLeave += new EventHandler(OnControlLeave);
        }

        private void UnmonitorControl(KryptonMaskedTextBox c)
        {
            c.MouseEnter -= new EventHandler(OnControlEnter);
            c.MouseLeave -= new EventHandler(OnControlLeave);
            c.TrackMouseEnter -= new EventHandler(OnControlEnter);
            c.TrackMouseLeave -= new EventHandler(OnControlLeave);
        }

        private void OnControlEnter(object sender, EventArgs e)
        {
            if (MouseEnterControl != null)
                MouseEnterControl(this, e);
        }

        private void OnControlLeave(object sender, EventArgs e)
        {
            if (MouseLeaveControl != null)
                MouseLeaveControl(this, e);
        }

        private void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // Pass request onto the view provided paint delegate
            if (_viewPaintDelegate != null)
                _viewPaintDelegate(this, e);
        }

        private void OnMaskedTextBoxTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        private void OnMaskedTextBoxTextAlignChanged(object sender, EventArgs e)
        {
            OnTextAlignChanged(e);
        }

        private void OnMaskedMaskChanged(object sender, EventArgs e)
        {
            OnMaskChanged(e);
        }

        private void OnMaskedIsOverwriteModeChanged(object sender, EventArgs e)
        {
            OnIsOverwriteModeChanged(e);
        }

        private void OnMaskedMaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            OnMaskInputRejected(e);
        }

        private void OnMaskedTypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            OnTypeValidationCompleted(e);
        }

        private void OnMaskedTextBoxHideSelectionChanged(object sender, EventArgs e)
        {
            OnHideSelectionChanged(e);
        }

        private void OnMaskedTextBoxModifiedChanged(object sender, EventArgs e)
        {
            OnModifiedChanged(e);
        }

        private void OnMaskedTextBoxReadOnlyChanged(object sender, EventArgs e)
        {
            OnReadOnlyChanged(e);
        }

        private void OnMaskedTextBoxGotFocus(object sender, EventArgs e)
        {
            OnGotFocus(e);
        }

        private void OnMaskedTextBoxLostFocus(object sender, EventArgs e)
        {
            OnLostFocus(e);
        }

        private void OnMaskedTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void OnMaskedTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void OnMaskedTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void OnMaskedTextBoxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OnPreviewKeyDown(e);
        }

        private void OnRibbonPaletteChanged(object sender, EventArgs e)
        {
            _maskedTextBox.Palette = Ribbon.GetResolvedPalette();
        }
        #endregion
    }
}
