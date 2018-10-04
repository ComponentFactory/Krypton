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
    /// Represents a ribbon group combo box.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupComboBox), "ToolboxBitmaps.KryptonRibbonGroupComboBox.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupComboBoxDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("SelectedTextChanged")]
    [DefaultProperty("Text")]
    public class KryptonRibbonGroupComboBox : KryptonRibbonGroupItem
    {
        #region Instance Fields
        private bool _visible;
        private bool _enabled;
        private string _keyTip;
        private Keys _shortcutKeys;
        private GroupItemSize _itemSizeCurrent;
        private NeedPaintHandler _viewPaintDelegate;
        private KryptonComboBox _comboBox;
        private KryptonComboBox _lastComboBox;
        private IKryptonDesignObject _designer;
        private Control _lastParentControl;
        private ViewBase _comboBoxView;
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
        /// Occurs when the drop-down portion of the KryptonComboBox is shown.
        /// </summary>
        [Description("Occurs when the drop-down portion of the KryptonComboBox is shown.")]
        [Category("Behavior")]
        public event EventHandler DropDown;

        /// <summary>
        /// Indicates that the drop-down portion of the KryptonComboBox has closed.
        /// </summary>
        [Description("Indicates that the drop-down portion of the KryptonComboBox has closed.")]
        [Category("Behavior")]
        public event EventHandler DropDownClosed;

        /// <summary>
        /// Occurs when the value of the DropDownStyle property changed.
        /// </summary>
        [Description("Occurs when the value of the DropDownStyle property changed.")]
        [Category("Behavior")]
        public event EventHandler DropDownStyleChanged;

        /// <summary>
        /// Occurs when the value of the SelectedIndex property changes.
        /// </summary>
        [Description("Occurs when the value of the SelectedIndex property changes.")]
        [Category("Behavior")]
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Occurs when an item is chosen from the drop-down list and the drop-down list is closed.
        /// </summary>
        [Description("Occurs when an item is chosen from the drop-down list and the drop-down list is closed.")]
        [Category("Behavior")]
        public event EventHandler SelectionChangeCommitted;

        /// <summary>
        /// Occurs when the value of the DataSource property changed.
        /// </summary>
        [Description("Occurs when the value of the DataSource property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler DataSourceChanged;

        /// <summary>
        /// Occurs when the value of the DisplayMember property changed.
        /// </summary>
        [Description("Occurs when the value of the DisplayMember property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler DisplayMemberChanged;

        /// <summary>
        /// Occurs when the list format has changed.
        /// </summary>
        [Description("Occurs when the list format has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler Format;

        /// <summary>
        /// Occurs when the value of the FormatInfo property changed.
        /// </summary>
        [Description("Occurs when the value of the FormatInfo property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler FormatInfoChanged;

        /// <summary>
        /// Occurs when the value of the FormatString property changed.
        /// </summary>
        [Description("Occurs when the value of the FormatString property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler FormatStringChanged;

        /// <summary>
        /// Occurs when the value of the FormattingEnabled property changed.
        /// </summary>
        [Description("Occurs when the value of the FormattingEnabled property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler FormattingEnabledChanged;

        /// <summary>
        /// Occurs when the value of the SelectedValue property changed.
        /// </summary>
        [Description("Occurs when the value of the SelectedValue property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler SelectedValueChanged;

        /// <summary>
        /// Occurs when the value of the ValueMember property changed.
        /// </summary>
        [Description("Occurs when the value of the ValueMember property changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ValueMemberChanged;

        /// <summary>
        /// Occurs when the KryptonComboBox text has changed.
        /// </summary>
        [Description("Occurs when the KryptonComboBox text has changed.")]
        [Category("Behavior")]
        public event EventHandler TextUpdate;

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
        /// Initialise a new instance of the KryptonRibbonGroupComboBox class.
        /// </summary>
        public KryptonRibbonGroupComboBox()
        {
            // Default fields
            _visible = true;
            _enabled = true;
            _itemSizeCurrent = GroupItemSize.Medium;
            _shortcutKeys = Keys.None;
            _keyTip = "X";

            // Create the actual combo box control and set initial settings
            _comboBox = new KryptonComboBox();
            _comboBox.InputControlStyle = InputControlStyle.Ribbon;
            _comboBox.AlwaysActive = false;
            _comboBox.MinimumSize = new Size(121, 0);
            _comboBox.MaximumSize = new Size(121, 0);
            _comboBox.TabStop = false;

            // Hook into the events that are then exposed via ourself
            _comboBox.DropDown += new EventHandler(OnComboBoxDropDown);
            _comboBox.DropDownClosed += new EventHandler(OnComboBoxDropDownClosed);
            _comboBox.DropDownStyleChanged += new EventHandler(OnComboBoxDropDownStyleChanged);
            _comboBox.SelectedIndexChanged += new EventHandler(OnComboBoxSelectedIndexChanged);
            _comboBox.SelectionChangeCommitted += new EventHandler(OnComboBoxSelectionChangeCommitted);
            _comboBox.TextUpdate += new EventHandler(OnComboBoxTextUpdate);
            _comboBox.GotFocus += new EventHandler(OnComboBoxGotFocus);
            _comboBox.LostFocus += new EventHandler(OnComboBoxLostFocus);
            _comboBox.KeyDown += new KeyEventHandler(OnComboBoxKeyDown);
            _comboBox.KeyUp += new KeyEventHandler(OnComboBoxKeyUp);
            _comboBox.KeyPress += new KeyPressEventHandler(OnComboBoxKeyPress);
            _comboBox.PreviewKeyDown += new PreviewKeyDownEventHandler(OnComboBoxPreviewKeyDown);
            _comboBox.DataSourceChanged += new EventHandler(OnComboBoxDataSourceChanged);
            _comboBox.DisplayMemberChanged += new EventHandler(OnComboBoxDisplayMemberChanged);
            _comboBox.Format += new ListControlConvertEventHandler(OnComboBoxFormat);
            _comboBox.FormatInfoChanged += new EventHandler(OnComboBoxFormatInfoChanged);
            _comboBox.FormatStringChanged += new EventHandler(OnComboBoxFormatStringChanged);
            _comboBox.FormattingEnabledChanged += new EventHandler(OnComboBoxFormattingEnabledChanged);
            _comboBox.SelectedValueChanged += new EventHandler(OnComboBoxSelectedValueChanged);
            _comboBox.ValueMemberChanged += new EventHandler(OnComboBoxValueMemberChanged);

            // Ensure we can track mouse events on the text box
            MonitorControl(_comboBox);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_comboBox != null)
                {
                    UnmonitorControl(_comboBox);
                    _comboBox.Dispose();
                    _comboBox = null;
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
                    // Use the same palette in the combo box as the ribbon, plus we need
                    // to know when the ribbon palette changes so we can reflect that change
                    _comboBox.Palette = Ribbon.GetResolvedPalette();
                    Ribbon.PaletteChanged += new EventHandler(OnRibbonPaletteChanged);
                }
            }
        }
        /// <summary>
        /// Gets and sets the shortcut key combination.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Shortcut key combination to set focus to the combo box.")]
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
        /// Access to the actual embedded KryptonComboBox instance.
        /// </summary>
        [Description("Access to the actual embedded KryptonComboBox instance.")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonComboBox ComboBox
        {
            get { return _comboBox; }
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
        /// Gets and sets the visible state of the rich text.
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
        /// Make the ribbon group visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon group hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the group combo box.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group combo box is enabled.")]
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
            get { return _comboBox.MinimumSize; }
            set { _comboBox.MinimumSize = value; }
        }

        /// <summary>
        /// Gets or sets the maximum size of the control.
        /// </summary>
        [Category("Layout")]
        [Description("Specifies the maximum size of the control.")]
        [DefaultValue(typeof(Size), "121, 0")]
        public Size MaximumSize
        {
            get { return _comboBox.MaximumSize; }
            set { _comboBox.MaximumSize = value; }
        }

        /// <summary>
        /// Gets and sets the text associated with the control.
        /// </summary>
        [Category("Appearance")]
        [Description("Text associated with the control.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Text
        {
            get { return _comboBox.Text; }
            set { _comboBox.Text = value; }
        }

        /// <summary>
        /// Gets and sets the associated context menu strip.
        /// </summary>
        [Category("Behavior")]
        [Description("The shortcut to display when the user right-clicks the control.")]
        [DefaultValue(null)]
        public ContextMenuStrip ContextMenuStrip
        {
            get { return _comboBox.ContextMenuStrip; }
            set { _comboBox.ContextMenuStrip = value; }
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu for showing when the combobox is right clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("KryptonContextMenu to be shown when the combobox is right clicked.")]
        [DefaultValue(null)]
        public KryptonContextMenu KryptonContextMenu
        {
            get { return _comboBox.KryptonContextMenu; }
            set { _comboBox.KryptonContextMenu = value; }
        }

        /// <summary>
        /// Gets and sets the value member.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the property to use as the actual value of the items in the control.")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string ValueMember
        {
            get { return _comboBox.ValueMember; }
            set { _comboBox.ValueMember = value; }
        }

        /// <summary>
        /// Gets and sets the list that this control will use to gets its items.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the list that this control will use to gets its items.")]
        [AttributeProvider(typeof(IListSource))]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DefaultValue((string)null)]
        public object DataSource
        {
            get { return _comboBox.DataSource; }
            set { _comboBox.DataSource = value; }
        }

        /// <summary>
        /// Gets and sets the property to display for the items in this control.
        /// </summary>
        [Category("Data")]
        [Description("Indicates the property to display for the items in this control.")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string DisplayMember
        {
            get { return _comboBox.DisplayMember; }
            set { _comboBox.DisplayMember = value; }
        }

        /// <summary>
        /// Gets and sets the appearance and functionality of the KryptonComboBox.
        /// </summary>
        [Category("Appearance")]
        [Description("Controls the appearance and functionality of the KryptonComboBox.")]
        [DefaultValue(typeof(ComboBoxStyle), "DropDown")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ComboBoxStyle DropDownStyle
        {
            get { return _comboBox.DropDownStyle; }
            set { _comboBox.DropDownStyle = value; }
        }

        /// <summary>
        /// Gets and sets the height, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("The height, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(200)]
        [Browsable(true)]
        public int DropDownHeight
        {
            get { return _comboBox.DropDownHeight; }
            set { _comboBox.DropDownHeight = value; }
        }

        /// <summary>
        /// Gets and sets the width, in pixels, of the drop down box in a KryptonComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("The width, in pixels, of the drop down box in a KryptonComboBox.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(143)]
        [Browsable(true)]
        public int DropDownWidth
        {
            get { return _comboBox.DropDownWidth; }
            set { _comboBox.DropDownWidth = value; }
        }

        /// <summary>
        /// Gets and sets the height, in pixels, of items in an owner-draw KryptomComboBox.
        /// </summary>
        [Category("Behavior")]
        [Description("The height, in pixels, of items in an owner-draw KryptomComboBox.")]
        [Localizable(true)]
        public int ItemHeight
        {
            get { return _comboBox.ItemHeight; }
            set { _comboBox.ItemHeight = value; }
        }

        /// <summary>
        /// Gets and sets the maximum number of entries to display in the drop-down list.
        /// </summary>
        [Category("Behavior")]
        [Description("The maximum number of entries to display in the drop-down list.")]
        [Localizable(true)]
        [DefaultValue(8)]
        public int MaxDropDownItems
        {
            get { return _comboBox.MaxDropDownItems; }
            set { _comboBox.MaxDropDownItems = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be entered into the edit control.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies the maximum number of characters that can be entered into the edit control.")]
        [DefaultValue(0)]
        [Localizable(true)]
        public int MaxLength
        {
            get { return _comboBox.MaxLength; }
            set { _comboBox.MaxLength = value; }
        }

        /// <summary>
        /// Gets or sets whether the items in the list portion of the KryptonComboBox are sorted.
        /// </summary>
        [Category("Behavior")]
        [Description("Specifies whether the items in the list portion of the KryptonComboBox are sorted.")]
        [DefaultValue(false)]
        public bool Sorted
        {
            get { return _comboBox.Sorted; }
            set { _comboBox.Sorted = value; }
        }

        /// <summary>
        /// Gets or sets the items in the KryptonComboBox.
        /// </summary>
        [Category("Data")]
        [Description("The items in the KryptonComboBox.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [MergableProperty(false)]
        [Localizable(true)]
        public ComboBox.ObjectCollection Items
        {
            get { return _comboBox.Items; }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _comboBox.AllowButtonSpecToolTips; }
            set { _comboBox.AllowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonComboBox.ComboBoxButtonSpecCollection ButtonSpecs
        {
            get { return _comboBox.ButtonSpecs; }
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
            get { return _comboBox.AutoCompleteCustomSource; }
            set { _comboBox.AutoCompleteCustomSource = value; }
        }

        /// <summary>
        /// Gets or sets the text completion behavior of the combobox.
        /// </summary>
        [Description("Indicates the text completion behavior of the combobox.")]
        [DefaultValue(typeof(AutoCompleteMode), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public AutoCompleteMode AutoCompleteMode
        {
            get { return _comboBox.AutoCompleteMode; }
            set { _comboBox.AutoCompleteMode = value; }
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
            get { return _comboBox.AutoCompleteSource; }
            set { _comboBox.AutoCompleteSource = value; }
        }

        /// <summary>
        /// Gets or sets the format specifier characters that indicate how a value is to be displayed.
        /// </summary>
        [Description("The format specifier characters that indicate how a value is to be displayed.")]
        [Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [MergableProperty(false)]
        [DefaultValue("")]
        public string FormatString
        {
            get { return _comboBox.FormatString; }
            set { _comboBox.FormatString = value; }
        }

        /// <summary>
        /// Gets or sets if this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be displayed.
        /// </summary>
        [Description("If this property is true, the value of FormatString is used to convert the value of DisplayMember into a value that can be displayed.")]
        [DefaultValue(true)]
        public bool FormattingEnabled
        {
            get { return _comboBox.FormattingEnabled; }
            set { _comboBox.FormattingEnabled = value; }
        }

        /// <summary>
        /// Gets and sets the formatting provider.
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IFormatProvider FormatInfo
        {
            get { return _comboBox.FormatInfo; }
            set { _comboBox.FormatInfo = value; }
        }

        /// <summary>
        /// Gets and sets the number of characters selected in the editable portion of the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get { return _comboBox.SelectionLength; }
            set { _comboBox.SelectionLength = value; }
        }

        /// <summary>
        /// Gets and sets the starting index of selected text in the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get { return _comboBox.SelectionStart; }
            set { _comboBox.SelectionStart = value; }
        }

        /// <summary>
        /// Gets and sets the selected item.
        /// </summary>
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get { return _comboBox.SelectedItem; }
            set { _comboBox.SelectedItem = value; }
        }

        /// <summary>
        /// Gets and sets the text that is selected in the editable portion of the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get { return _comboBox.SelectedText; }
            set { _comboBox.SelectedText = value; }
        }

        /// <summary>
        /// Gets and sets the selected index.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return _comboBox.SelectedIndex; }
            set { _comboBox.SelectedIndex = value; }
        }

        /// <summary>
        /// Gets and sets the selected value.
        /// </summary>
        [Bindable(true)]
        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get { return _comboBox.SelectedValue; }
            set { _comboBox.SelectedValue = value; }
        }

        /// <summary>
        /// Gets and sets a value indicating whether the control is displaying its drop-down portion.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DroppedDown
        {
            get { return _comboBox.DroppedDown; }
            set { _comboBox.DroppedDown = value; }
        }

        /// <summary>
        /// Finds the first item in the combo box that starts with the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
        public int FindString(string str)
        {
            return _comboBox.FindString(str);
        }

        /// <summary>
        /// Finds the first item after the given index which starts with the given string. The search is not case sensitive.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
        public int FindString(string str, int startIndex)
        {
            return _comboBox.FindString(str, startIndex);
        }

        /// <summary>
        /// Finds the first item in the combo box that matches the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
        public int FindStringExact(string str)
        {
            return _comboBox.FindStringExact(str);
        }

        /// <summary>
        /// Finds the first item after the specified index that matches the specified string.
        /// </summary>
        /// <param name="str">The String to search for.</param>
        /// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
        /// <returns>The zero-based index of the first item found; returns -1 if no match is found, or 0 if the s parameter specifies Empty.</returns>
        public int FindStringExact(string str, int startIndex)
        {
            return _comboBox.FindStringExact(str, startIndex);
        }

        /// <summary>
        /// Returns the height of an item in the ComboBox.
        /// </summary>
        /// <param name="index">The index of the item to return the height of.</param>
        /// <returns>The height, in pixels, of the item at the specified index.</returns>
        public int GetItemHeight(int index)
        {
            return _comboBox.GetItemHeight(index);
        }

        /// <summary>
        /// Returns the text representation of the specified item.
        /// </summary>
        /// <param name="item">The object from which to get the contents to display.</param>
        /// <returns>If the DisplayMember property is not specified, the value returned by GetItemText is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the DisplayMember property for the object specified in the item parameter.</returns>
        public string GetItemText(object item)
        {
            return _comboBox.GetItemText(item);
        }

        /// <summary>
        /// Selects a range of text in the control.
        /// </summary>
        /// <param name="start">The position of the first character in the current text selection within the text box.</param>
        /// <param name="length">The number of characters to select.</param>
        public void Select(int start, int length)
        {
            _comboBox.Select(start, length);
        }

        /// <summary>
        /// Selects all text in the control.
        /// </summary>
        public void SelectAll()
        {
            _comboBox.SelectAll();
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
            return new ViewDrawRibbonGroupComboBox(ribbon, this, needPaint);
        }

        /// <summary>
        /// Gets and sets the associated designer.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public IKryptonDesignObject ComboBoxDesigner
        {
            get { return _designer; }
            set { _designer = value; }
        }

        private bool ShouldSerializeComboBoxDesigner()
        {
            return false;
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase ComboBoxView
        {
            get { return _comboBoxView; }
            set { _comboBoxView = value; }
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the TextUpdate event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnTextUpdate(EventArgs e)
        {
            if (TextUpdate != null)
                TextUpdate(this, e);
        }

        /// <summary>
        /// Raises the SelectionChangeCommitted event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSelectionChangeCommitted(EventArgs e)
        {
            if (SelectionChangeCommitted != null)
                SelectionChangeCommitted(this, e);
        }

        /// <summary>
        /// Raises the SelectedIndexChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }

        /// <summary>
        /// Raises the DropDownStyleChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDropDownStyleChanged(EventArgs e)
        {
            if (DropDownStyleChanged != null)
                DropDownStyleChanged(this, e);
        }

        /// <summary>
        /// Raises the DataSourceChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDataSourceChanged(EventArgs e)
        {
            if (DataSourceChanged != null)
                DataSourceChanged(this, e);
        }

        /// <summary>
        /// Raises the DisplayMemberChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDisplayMemberChanged(EventArgs e)
        {
            if (DisplayMemberChanged != null)
                DisplayMemberChanged(this, e);
        }

        /// <summary>
        /// Raises the Format event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFormat(EventArgs e)
        {
            if (Format != null)
                Format(this, e);
        }

        /// <summary>
        /// Raises the FormatInfoChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFormatInfoChanged(EventArgs e)
        {
            if (FormatInfoChanged != null)
                FormatInfoChanged(this, e);
        }

        /// <summary>
        /// Raises the FormatStringChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFormatStringChanged(EventArgs e)
        {
            if (FormatStringChanged != null)
                FormatStringChanged(this, e);
        }

        /// <summary>
        /// Raises the FormattingEnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFormattingEnabledChanged(EventArgs e)
        {
            if (FormattingEnabledChanged != null)
                FormattingEnabledChanged(this, e);
        }

        /// <summary>
        /// Raises the SelectedValueChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSelectedValueChanged(EventArgs e)
        {
            if (SelectedValueChanged != null)
                SelectedValueChanged(this, e);
        }

        /// <summary>
        /// Raises the ValueMemberChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnValueMemberChanged(EventArgs e)
        {
            if (ValueMemberChanged != null)
                ValueMemberChanged(this, e);
        }

        /// <summary>
        /// Raises the DropDownClosed event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDropDownClosed(EventArgs e)
        {
            if (DropDownClosed != null)
                DropDownClosed(this, e);
        }

        /// <summary>
        /// Raises the DropDown event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnDropDown(EventArgs e)
        {
            if (DropDown != null)
                DropDown(this, e);
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

        internal KryptonComboBox LastComboBox
        {
            get { return _lastComboBox; }
            set { _lastComboBox = value; }
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
                        // Can the combo box take the focus
                        if ((LastComboBox != null) && (LastComboBox.CanFocus))
                            LastComboBox.ComboBox.Focus();

                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region Implementation
        private void MonitorControl(KryptonComboBox c)
        {
            c.MouseEnter += new EventHandler(OnControlEnter);
            c.MouseLeave += new EventHandler(OnControlLeave);
            c.TrackMouseEnter += new EventHandler(OnControlEnter);
            c.TrackMouseLeave += new EventHandler(OnControlLeave);
        }

        private void UnmonitorControl(KryptonComboBox c)
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

        private void OnComboBoxGotFocus(object sender, EventArgs e)
        {
            OnGotFocus(e);
        }

        private void OnComboBoxLostFocus(object sender, EventArgs e)
        {
            OnLostFocus(e);
        }

        private void OnComboBoxTextUpdate(object sender, EventArgs e)
        {
            OnTextUpdate(e);
        }

        private void OnComboBoxSelectionChangeCommitted(object sender, EventArgs e)
        {
            OnSelectionChangeCommitted(e);
        }

        private void OnComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(e);
        }

        private void OnComboBoxDropDownStyleChanged(object sender, EventArgs e)
        {
            OnDropDownStyleChanged(e);
        }

        private void OnComboBoxDataSourceChanged(object sender, EventArgs e)
        {
            OnDataSourceChanged(e);
        }

        private void OnComboBoxDisplayMemberChanged(object sender, EventArgs e)
        {
            OnDisplayMemberChanged(e);
        }

        private void OnComboBoxDropDownClosed(object sender, EventArgs e)
        {
            OnDropDownClosed(e);
        }

        private void OnComboBoxDropDown(object sender, EventArgs e)
        {
            OnDropDown(e);
        }

        private void OnComboBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void OnComboBoxKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void OnComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void OnComboBoxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            OnPreviewKeyDown(e);
        }

        private void OnComboBoxFormat(object sender, ListControlConvertEventArgs e)
        {
            OnFormat(e);
        }

        private void OnComboBoxFormatInfoChanged(object sender, EventArgs e)
        {
            OnFormatInfoChanged(e);
        }

        private void OnComboBoxFormatStringChanged(object sender, EventArgs e)
        {
            OnFormatStringChanged(e);
        }

        private void OnComboBoxFormattingEnabledChanged(object sender, EventArgs e)
        {
            OnFormattingEnabledChanged(e);
        }

        private void OnComboBoxSelectedValueChanged(object sender, EventArgs e)
        {
            OnSelectedValueChanged(e);
        }

        private void OnComboBoxValueMemberChanged(object sender, EventArgs e)
        {
            OnValueMemberChanged(e);
        }

        private void OnRibbonPaletteChanged(object sender, EventArgs e)
        {
            _comboBox.Palette = Ribbon.GetResolvedPalette();
        }
        #endregion
    }
}
