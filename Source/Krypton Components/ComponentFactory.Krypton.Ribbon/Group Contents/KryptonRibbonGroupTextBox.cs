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
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Represents a ribbon group text box.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupTextBox), "ToolboxBitmaps.KryptonRibbonGroupTextBox.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTextBoxDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    public class KryptonRibbonGroupTextBox : KryptonRibbonGroupItem
    {
        #region Instance Fields
        private bool _visible;
        private bool _enabled;
        private string _keyTip;
        private Keys _shortcutKeys;
        private GroupItemSize _itemSizeCurrent;
        private NeedPaintHandler _viewPaintDelegate;
        private KryptonTextBox _textBox;
        private KryptonTextBox _lastTextBox;
        private IKryptonDesignObject _designer;
        private Control _lastParentControl;
        private ViewBase _textBoxView;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the Text property changes.
        /// </summary>
        [Description("Occurs when the value of the Text property changes.")]
        [Category("Property Changed")]
        public event EventHandler TextChanged;

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
        /// Occurs when the value of the AcceptsTab property changes.
        /// </summary>
        [Description("Occurs when the value of the AcceptsTab property changes.")]
        [Category("Property Changed")]
        public event EventHandler AcceptsTabChanged;

        /// <summary>
        /// Occurs when the value of the HideSelection property changes.
        /// </summary>
        [Description("Occurs when the value of the HideSelection property changes.")]
        [Category("Property Changed")]
        public event EventHandler HideSelectionChanged;

        /// <summary>
        /// Occurs when the value of the TextAlign property changes.
        /// </summary>
        [Description("Occurs when the value of the TextAlign property changes.")]
        [Category("Property Changed")]
        public event EventHandler TextAlignChanged;

        /// <summary>
        /// Occurs when the value of the Modified property changes.
        /// </summary>
        [Description("Occurs when the value of the Modified property changes.")]
        [Category("Property Changed")]
        public event EventHandler ModifiedChanged;

        /// <summary>
        /// Occurs when the value of the Multiline property changes.
        /// </summary>
        [Description("Occurs when the value of the Multiline property changes.")]
        [Category("Property Changed")]
        public event EventHandler MultilineChanged;

        /// <summary>
        /// Occurs when the value of the ReadOnly property changes.
        /// </summary>
        [Description("Occurs when the value of the ReadOnly property changes.")]
        [Category("Property Changed")]
        public event EventHandler ReadOnlyChanged;

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
        /// Initialise a new instance of the KryptonRibbonGroupTextBox class.
        /// </summary>
        public KryptonRibbonGroupTextBox()
        {
            // Default fields
            _visible = true;
            _enabled = true;
            _itemSizeCurrent = GroupItemSize.Medium;
            _shortcutKeys = Keys.None;
            _keyTip = "X";

            // Create the actual text box control and set initial settings
            _textBox = new KryptonTextBox();
            _textBox.InputControlStyle = InputControlStyle.Ribbon;
            _textBox.AlwaysActive = false;
            _textBox.MinimumSize = new Size(121, 0);
            _textBox.MaximumSize = new Size(121, 0);
            _textBox.TabStop = false;

            // Hook into events to expose via this container
            _textBox.AcceptsTabChanged += new EventHandler(OnTextBoxAcceptsTabChanged);
            _textBox.TextAlignChanged += new EventHandler(OnTextBoxTextAlignChanged);
            _textBox.TextChanged += new EventHandler(OnTextBoxTextChanged);
            _textBox.HideSelectionChanged += new EventHandler(OnTextBoxHideSelectionChanged);
            _textBox.ModifiedChanged += new EventHandler(OnTextBoxModifiedChanged);
            _textBox.MultilineChanged += new EventHandler(OnTextBoxMultilineChanged);
            _textBox.ReadOnlyChanged += new EventHandler(OnTextBoxReadOnlyChanged);
            _textBox.GotFocus += new EventHandler(OnTextBoxGotFocus);
            _textBox.LostFocus += new EventHandler(OnTextBoxLostFocus);
            _textBox.KeyDown += new KeyEventHandler(OnTextBoxKeyDown);
            _textBox.KeyUp += new KeyEventHandler(OnTextBoxKeyUp);
            _textBox.KeyPress += new KeyPressEventHandler(OnTextBoxKeyPress);
            _textBox.PreviewKeyDown += new PreviewKeyDownEventHandler(OnTextBoxPreviewKeyDown);

            // Ensure we can track mouse events on the text box
            MonitorControl(_textBox);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_textBox != null)
                {
                    UnmonitorControl(_textBox);
                    _textBox.Dispose();
                    _textBox = null;
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
                    // Use the same palette in the text box as the ribbon, plus we need
                    // to know when the ribbon palette changes so we can reflect that change
                    _textBox.Palette = Ribbon.GetResolvedPalette();
                    Ribbon.PaletteChanged += new EventHandler(OnRibbonPaletteChanged);
                }
            }
        }

        /// <summary>
        /// Gets and sets the shortcut key combination.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Shortcut key combination to set focus to the text box.")]
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
        /// Access to the actual embedded KryptonTextBox instance.
        /// </summary>
        [Description("Access to the actual embedded KryptonTextBox instance.")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonTextBox TextBox
        {
            get { return _textBox; }
        }

        /// <summary>
        /// Gets and sets the key tip for the ribbon group text box.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group text box key tip.")]
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
        /// Gets and sets the visible state of the text box.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the text box is visible or hidden.")]
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
        /// Make the ribbon group textbox visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon group textbox hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the group text box.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group text box is enabled.")]
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
            get { return _textBox.MinimumSize; }
            set { _textBox.MinimumSize = value; }
        }

        /// <summary>
        /// Gets or sets the maximum size of the control.
        /// </summary>
        [Category("Layout")]
        [Description("Specifies the maximum size of the control.")]
        [DefaultValue(typeof(Size), "121, 0")]
        public Size MaximumSize
        {
            get { return _textBox.MaximumSize; }
            set { _textBox.MaximumSize = value; }
        }

        /// <summary>
        /// Gets and sets the text associated with the control.
        /// </summary>
        [Category("Appearance")]
        [Description("Text associated with the control.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Text
        {
            get { return _textBox.Text; }
            set { _textBox.Text = value; }
        }

        /// <summary>
        /// Gets or sets the lines of text in a multiline edit, as an array of String values.
        /// </summary>
        [Category("Appearance")]
        [Description("The lines of text in a multiline edit, as an array of String values.")]
        [Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MergableProperty(false)]
        [Localizable(true)]
        public string[] Lines
        {
            get { return _textBox.Lines; }
            set { _textBox.Lines = value; }
        }

        /// <summary>
        /// Gets or sets, for multiline edit controls, which scroll bars will be shown for this control.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates, for multiline edit controls, which scroll bars will be shown for this control.")]
        [DefaultValue(typeof(ScrollBars), "None")]
        [Localizable(true)]
        public ScrollBars ScrollBars
        {
            get { return _textBox.ScrollBars; }
            set { _textBox.ScrollBars = value; }
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
            get { return _textBox.TextAlign; }
            set { _textBox.TextAlign = value; }
        }

        /// <summary>
        /// Gets and sets the associated context menu strip.
        /// </summary>
        [Category("Behavior")]
        [Description("The shortcut to display when the user right-clicks the control.")]
        [DefaultValue(null)]
        public ContextMenuStrip ContextMenuStrip
        {
            get { return _textBox.ContextMenuStrip; }
            set { _textBox.ContextMenuStrip = value; }
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu for showing when the text box is right clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("KryptonContextMenu to be shown when the text box is right clicked.")]
        [DefaultValue(null)]
        public KryptonContextMenu KryptonContextMenu
        {
            get { return _textBox.KryptonContextMenu; }
            set { _textBox.KryptonContextMenu = value; }
        }

        /// <summary>
        /// Indicates if lines are automatically word-wrapped for multiline edit controls.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if lines are automatically word-wrapped for multiline edit controls.")]
        [DefaultValue(true)]
        [Localizable(true)]
        public bool WordWrap
        {
            get { return _textBox.WordWrap; }
            set { _textBox.WordWrap = value; }
        }

        /// <summary>
        /// Gets and sets whether the text in the control can span more than one line.
        /// </summary>
        [Category("Behavior")]
        [Description("Control whether the text in the control can span more than one line.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Localizable(true)]
        public bool Multiline
        {
            get { return _textBox.Multiline; }
            set { _textBox.Multiline = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating if return characters are accepted as input for multiline edit controls.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if return characters are accepted as input for multiline edit controls.")]
        [DefaultValue(false)]
        public bool AcceptsReturn
        {
            get { return _textBox.AcceptsReturn; }
            set { _textBox.AcceptsReturn = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating if tab characters are accepted as input for multiline edit controls.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if tab characters are accepted as input for multiline edit controls.")]
        [DefaultValue(false)]
        public bool AcceptsTab
        {
            get { return _textBox.AcceptsTab; }
            set { _textBox.AcceptsTab = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating if all the characters should be left alone or converted to uppercase or lowercase.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if all the characters should be left alone or converted to uppercase or lowercase.")]
        [DefaultValue(typeof(CharacterCasing), "Normal")]
        public CharacterCasing CharacterCasing
        {
            get { return _textBox.CharacterCasing; }
            set { _textBox.CharacterCasing = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating that the selection should be hidden when the edit control loses focus.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates that the selection should be hidden when the edit control loses focus.")]
        [DefaultValue(true)]
        public bool HideSelection
        {
            get { return _textBox.HideSelection; }
            set { _textBox.HideSelection = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be entered into the edit control.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies the maximum number of characters that can be entered into the edit control.")]
        [DefaultValue(32767)]
        [Localizable(true)]
        public int MaxLength
        {
            get { return _textBox.MaxLength; }
            set { _textBox.MaxLength = value; }
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
            get { return _textBox.ReadOnly; }
            set { _textBox.ReadOnly = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether shortcuts defined for the control are enabled.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates whether shortcuts defined for the control are enabled.")]
        [DefaultValue(true)]
        public bool ShortcutsEnabled
        {
            get { return _textBox.ShortcutsEnabled; }
            set { _textBox.ShortcutsEnabled = value; }
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
            get { return _textBox.PasswordChar; }
            set { _textBox.PasswordChar = value; }
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
            get { return _textBox.UseSystemPasswordChar; }
            set { _textBox.UseSystemPasswordChar = value; }
        }

        /// <summary>
        /// Gets or sets the StringCollection to use when the AutoCompleteSource property is set to CustomSource.
        /// </summary>
        [Description("The StringCollection to use when the AutoCompleteSource property is set to CustomSource.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Browsable(true)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return _textBox.AutoCompleteCustomSource; }
            set { _textBox.AutoCompleteCustomSource = value; }
        }

        /// <summary>
        /// Gets or sets the text completion behavior of the textbox.
        /// </summary>
        [Description("Indicates the text completion behavior of the textbox.")]
        [DefaultValue(typeof(AutoCompleteMode), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteMode AutoCompleteMode
        {
            get { return _textBox.AutoCompleteMode; }
            set { _textBox.AutoCompleteMode = value; }
        }

        /// <summary>
        /// Gets or sets the autocomplete source, which can be one of the values from AutoCompleteSource enumeration.
        /// </summary>
        [Description("The autocomplete source, which can be one of the values from AutoCompleteSource enumeration.")]
        [DefaultValue(typeof(AutoCompleteSource), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteSource AutoCompleteSource
        {
            get { return _textBox.AutoCompleteSource; }
            set { _textBox.AutoCompleteSource = value; }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _textBox.AllowButtonSpecToolTips; }
            set { _textBox.AllowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonTextBox.TextBoxButtonSpecCollection ButtonSpecs
        {
            get { return _textBox.ButtonSpecs; }
        }

        /// <summary>
        /// Gets a value indicating whether the user can undo the previous operation in a rich text box control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanUndo
        {
            get { return _textBox.CanUndo; }
        }

        /// <summary>
        /// Gets a value indicating whether the contents have changed since last last.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Modified
        {
            get { return _textBox.Modified; }
        }

        /// <summary>
        /// Gets and sets the selected text within the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get { return _textBox.SelectedText; }
            set { _textBox.SelectedText = value; }
        }

        /// <summary>
        /// Gets and sets the selection length for the selected area.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get { return _textBox.SelectionLength; }
            set { _textBox.SelectionLength = value; }
        }

        /// <summary>
        /// Gets and sets the starting point of text selected in the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get { return _textBox.SelectionStart; }
            set { _textBox.SelectionStart = value; }
        }

        /// <summary>
        /// Gets the length of text in the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TextLength
        {
            get { return _textBox.TextLength; }
        }

        /// <summary>
        /// Appends text to the current text of a rich text box.
        /// </summary>
        /// <param name="text">The text to append to the current contents of the text box.</param>
        public void AppendText(string text)
        {
            _textBox.AppendText(text);
        }

        /// <summary>
        /// Clears all text from the text box control.
        /// </summary>
        public void Clear()
        {
            _textBox.Clear();
        }

        /// <summary>
        /// Clears information about the most recent operation from the undo buffer of the rich text box. 
        /// </summary>
        public void ClearUndo()
        {
            _textBox.ClearUndo();
        }

        /// <summary>
        /// Copies the current selection in the text box to the Clipboard.
        /// </summary>
        public void Copy()
        {
            _textBox.Copy();
        }

        /// <summary>
        /// Moves the current selection in the text box to the Clipboard.
        /// </summary>
        public void Cut()
        {
            _textBox.Cut();
        }

        /// <summary>
        /// Replaces the current selection in the text box with the contents of the Clipboard.
        /// </summary>
        public void Paste()
        {
            _textBox.Paste();
        }

        /// <summary>
        /// Scrolls the contents of the control to the current caret position.
        /// </summary>
        public void ScrollToCaret()
        {
            _textBox.ScrollToCaret();
        }

        /// <summary>
        /// Selects a range of text in the control.
        /// </summary>
        /// <param name="start">The position of the first character in the current text selection within the text box.</param>
        /// <param name="length">The number of characters to select.</param>
        public void Select(int start, int length)
        {
            _textBox.Select(start, length);
        }

        /// <summary>
        /// Selects all text in the control.
        /// </summary>
        public void SelectAll()
        {
            _textBox.SelectAll();
        }

        /// <summary>
        /// Undoes the last edit operation in the text box.
        /// </summary>
        public void Undo()
        {
            _textBox.Undo();
        }

        /// <summary>
        /// Specifies that the value of the SelectionLength property is zero so that no characters are selected in the control.
        /// </summary>
        public void DeselectAll()
        {
            _textBox.DeselectAll();
        }

        /// <summary>
        /// Retrieves the character that is closest to the specified location within the control.
        /// </summary>
        /// <param name="pt">The location from which to seek the nearest character.</param>
        /// <returns>The character at the specified location.</returns>
        public int GetCharFromPosition(Point pt)
        {
            return _textBox.GetCharFromPosition(pt);
        }

        /// <summary>
        /// Retrieves the index of the character nearest to the specified location.
        /// </summary>
        /// <param name="pt">The location to search.</param>
        /// <returns>The zero-based character index at the specified location.</returns>
        public int GetCharIndexFromPosition(Point pt)
        {
            return _textBox.GetCharIndexFromPosition(pt);
        }

        /// <summary>
        /// Retrieves the index of the first character of a given line.
        /// </summary>
        /// <param name="lineNumber">The line for which to get the index of its first character.</param>
        /// <returns>The zero-based character index in the specified line.</returns>
        public int GetFirstCharIndexFromLine(int lineNumber)
        {
            return _textBox.GetFirstCharIndexFromLine(lineNumber);
        }

        /// <summary>
        /// Retrieves the index of the first character of the current line.
        /// </summary>
        /// <returns>The zero-based character index in the current line.</returns>
        public int GetFirstCharIndexOfCurrentLine()
        {
            return _textBox.GetFirstCharIndexOfCurrentLine();
        }

        /// <summary>
        /// Retrieves the line number from the specified character position within the text of the RichTextBox control.
        /// </summary>
        /// <param name="index">The character index position to search.</param>
        /// <returns>The zero-based line number in which the character index is located.</returns>
        public int GetLineFromCharIndex(int index)
        {
            return _textBox.GetLineFromCharIndex(index);
        }

        /// <summary>
        /// Retrieves the location within the control at the specified character index.
        /// </summary>
        /// <param name="index">The index of the character for which to retrieve the location.</param>
        /// <returns>The location of the specified character.</returns>
        public Point GetPositionFromCharIndex(int index)
        {
            return _textBox.GetPositionFromCharIndex(index);
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
        /// Creates an appropriate view element for this item.
        /// </summary>
        /// <param name="ribbon">Reference to the owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying changes in display.</param>
        /// <returns>ViewBase derived instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ViewBase CreateView(KryptonRibbon ribbon, 
                                            NeedPaintHandler needPaint)
        {
            return new ViewDrawRibbonGroupTextBox(ribbon, this, needPaint);
        }

        /// <summary>
        /// Gets and sets the associated designer.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public IKryptonDesignObject TextBoxDesigner
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
        public ViewBase TextBoxView
        {
            get { return _textBoxView; }
            set { _textBoxView = value; }
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
        /// Raises the AcceptsTabChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnAcceptsTabChanged(EventArgs e)
        {
            if (AcceptsTabChanged != null)
                AcceptsTabChanged(this, e);
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
        /// Raises the MultilineChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnMultilineChanged(EventArgs e)
        {
            if (MultilineChanged != null)
                MultilineChanged(this, e);
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

        internal KryptonTextBox LastTextBox
        {
            get { return _lastTextBox; }
            set { _lastTextBox = value; }
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
                        // Can the text box take the focus
                        if ((LastTextBox != null) && (LastTextBox.CanFocus))
                            LastTextBox.TextBox.Focus();

                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region Implementation
        private void MonitorControl(KryptonTextBox c)
        {
            c.MouseEnter += new EventHandler(OnControlEnter);
            c.MouseLeave += new EventHandler(OnControlLeave);
            c.TrackMouseEnter += new EventHandler(OnControlEnter);
            c.TrackMouseLeave += new EventHandler(OnControlLeave);
        }

        private void UnmonitorControl(KryptonTextBox c)
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

        private void OnTextBoxAcceptsTabChanged(object sender, EventArgs e)
        {
            OnAcceptsTabChanged(e);
        }

        private void OnTextBoxTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        private void OnTextBoxTextAlignChanged(object sender, EventArgs e)
        {
            OnTextAlignChanged(e);
        }

        private void OnTextBoxHideSelectionChanged(object sender, EventArgs e)
        {
            OnHideSelectionChanged(e);
        }

        private void OnTextBoxModifiedChanged(object sender, EventArgs e)
        {
            OnModifiedChanged(e);
        }

        private void OnTextBoxMultilineChanged(object sender, EventArgs e)
        {
            OnMultilineChanged(e);
        }

        private void OnTextBoxReadOnlyChanged(object sender, EventArgs e)
        {
            OnReadOnlyChanged(e);
        }

        private void OnTextBoxGotFocus(object sender, EventArgs e)
        {
            OnGotFocus(e);
        }

        private void OnTextBoxLostFocus(object sender, EventArgs e)
        {
            OnLostFocus(e);
        }

        private void OnTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void OnTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void OnTextBoxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OnPreviewKeyDown(e);
        }

        private void OnRibbonPaletteChanged(object sender, EventArgs e)
        {
            _textBox.Palette = Ribbon.GetResolvedPalette();
        }
        #endregion
    }
}
