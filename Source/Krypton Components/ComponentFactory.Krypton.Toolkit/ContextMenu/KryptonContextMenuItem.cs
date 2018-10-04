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
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Provide a standard menu item.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonContextMenuItem), "ToolboxBitmaps.KryptonContextMenuItem.bmp")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    [DefaultEvent("Click")]
    public class KryptonContextMenuItem : KryptonContextMenuItemBase
    {
        #region Instance Fields
        private bool _enabled;
        private bool _splitSubMenu;
        private bool _checkOnClick;
        private bool _showShortcutKeys;
        private bool _autoClose;
        private bool _largeKryptonCommandImage;
        private string _text;
        private string _extraText;
        private string _shortcutKeyDisplayString;
        private Image _image;
        private Color _imageTransparentColor;
        private CheckState _checkState;
        private Keys _shortcutKeys;
        private KryptonContextMenuCollection _items;
        private PaletteContextMenuItemStateRedirect _stateRedirect;
        private PaletteContextMenuItemState _stateNormal;
        private PaletteContextMenuItemState _stateDisabled;
        private PaletteContextMenuItemStateHighlight _stateHighlight;
        private PaletteContextMenuItemStateChecked _stateChecked;
        private KryptonCommand _command;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the menu item is clicked.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the menu item is clicked.")]
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the menu item is clicked.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the checked property changes.")]
        public event EventHandler CheckedChanged;

        /// <summary>
        /// Occurs when the menu item is clicked.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the check state property changes.")]
        public event EventHandler CheckStateChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItem class.
        /// </summary>
        public KryptonContextMenuItem()
            : this("Menu Item", null, null, Keys.None)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItem class.
        /// </summary>
        /// <param name="initialText">Initial text string.</param>
        public KryptonContextMenuItem(string initialText)
            : this(initialText, null, null, Keys.None)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItem class.
        /// </summary>
        /// <param name="initialText">Initial text string.</param>
        /// <param name="clickHandler">Click handler.</param>
        public KryptonContextMenuItem(string initialText,
                                      EventHandler clickHandler)
            : this(initialText, null, clickHandler, Keys.None)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItem class.
        /// </summary>
        /// <param name="initialText">Initial text string.</param>
        /// <param name="clickHandler">Click handler.</param>
        /// <param name="shortcut">Shortcut key combination.</param>
        public KryptonContextMenuItem(string initialText,
                                      EventHandler clickHandler,
                                      Keys shortcut)
            : this(initialText, null, clickHandler, shortcut)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItem class.
        /// </summary>
        /// <param name="initialText">Initial text string.</param>
        /// <param name="initialImage">Initial image.</param>
        /// <param name="clickHandler">Click handler.</param>
        public KryptonContextMenuItem(string initialText,
                                      Image initialImage,
                                      EventHandler clickHandler)
            : this(initialText, initialImage, clickHandler, Keys.None)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuItem class.
        /// </summary>
        /// <param name="initialText">Initial text string.</param>
        /// <param name="initialImage">Initial image.</param>
        /// <param name="clickHandler">Click handler.</param>
        /// <param name="shortcut">Shortcut key combination.</param>
        public KryptonContextMenuItem(string initialText,
                                      Image initialImage,
                                      EventHandler clickHandler,
                                      Keys shortcut)
        {
            // Initial values
            _text = initialText;
            _image = initialImage;

            // Initial click handler
            if (clickHandler != null)
                Click += clickHandler;

            // Default fields
            _enabled = true;
            _autoClose = true;
            _splitSubMenu = false;
            _checkOnClick = false;
            _showShortcutKeys = true;
            _largeKryptonCommandImage = false;
            _extraText = string.Empty;
            _imageTransparentColor = Color.Empty;
            _shortcutKeys = shortcut;
            _shortcutKeyDisplayString = string.Empty;
            _checkState = CheckState.Unchecked;
            _items = new KryptonContextMenuCollection();

            // Create the common storage for palette override values
            _stateRedirect = new PaletteContextMenuItemStateRedirect();
            _stateNormal = new PaletteContextMenuItemState(_stateRedirect);
            _stateDisabled = new PaletteContextMenuItemState(_stateRedirect);
            _stateHighlight = new PaletteContextMenuItemStateHighlight(_stateRedirect);
            _stateChecked = new PaletteContextMenuItemStateChecked(_stateRedirect);
        }

        /// <summary>
        /// Returns a description of the instance.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return Text;
        }
        #endregion

        #region Public
        /// <summary>
        /// Returns the number of child menu items.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int ItemChildCount
        {
            get { return 0; }
        }

        /// <summary>
        /// Returns the indexed child menu item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override KryptonContextMenuItemBase this[int index]
        {
            get { return null; }
        }

        /// <summary>
        /// Test for the provided shortcut and perform relevant action if a match is found.
        /// </summary>
        /// <param name="keyData">Key data to check against shorcut definitions.</param>
        /// <returns>True if shortcut was handled, otherwise false.</returns>
        public override bool ProcessShortcut(Keys keyData)
        {
            if (_shortcutKeys == keyData)
            {
                PerformClick();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns a view appropriate for this item based on the object it is inside.
        /// </summary>
        /// <param name="provider">Provider of context menu information.</param>
        /// <param name="parent">Owning object reference.</param>
        /// <param name="columns">Containing columns.</param>
        /// <param name="standardStyle">Draw items with standard or alternate style.</param>
        /// <param name="imageColumn">Draw an image background for the item images.</param>
        /// <returns>ViewBase that is the root of the view hierachy being added.</returns>
        public override ViewBase GenerateView(IContextMenuProvider provider,
                                              object parent,
                                              ViewLayoutStack columns,
                                              bool standardStyle,
                                              bool imageColumn)
        {
            return new ViewDrawMenuItem(provider, this, columns, standardStyle, imageColumn);
        }

        /// <summary>
        /// Gets and sets the standard menu item text.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Standard menu item text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("MenuItem")]
        [Localizable(true)]
        [Bindable(true)]
        public string Text
        {
            get { return _text; }
            
            set 
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the standard menu item extra text.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Standard menu item extra text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Localizable(true)]
        [Bindable(true)]
        public string ExtraText
        {
            get { return _extraText; }
            
            set 
            {
                if (_extraText != value)
                {
                    _extraText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ExtraText"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the standard menu item image.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Standard menu item image.")]
        [DefaultValue(null)]
        [Localizable(true)]
        [Bindable(true)]
        public Image Image
        {
            get { return _image; }
            
            set 
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Image"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the heading image color to make transparent.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Heading image color to make transparent.")]
        [Localizable(true)]
        [Bindable(true)]
        public Color ImageTransparentColor
        {
            get { return _imageTransparentColor; }
            
            set 
            {
                if (_imageTransparentColor != value)
                {
                    _imageTransparentColor = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ImageTransparentColor"));
                }
            }
        }

        private bool ShouldSerializeImageTransparentColor()
        {
            return (_imageTransparentColor == null) || !_imageTransparentColor.Equals(Color.Empty);
        }

        /// <summary>
        /// Gets and sets the shortcut key combination associated with the menu item.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("The shortcut key combination associated with the menu item.")]
        [DefaultValue(typeof(Keys), "None")]
        [Localizable(true)]
        public Keys ShortcutKeys
        {
            get { return _shortcutKeys; }
            
            set 
            {
                if (_shortcutKeys != value)
                {
                    _shortcutKeys = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ShortcutKeys"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if clicking the menu item automatically closes the context menu.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates if clicking the menu item automatically closes the context menu.")]
        [DefaultValue(true)]
        public bool AutoClose
        {
            get { return _autoClose; }
            
            set 
            {
                if (_autoClose != value)
                {
                    _autoClose = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("AutoClose"));
                }
            }
        }

        /// <summary>
        /// Gets and sets whether the menu item toggles checked state when clicked.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates whether the menu item toggles checked state when clicked.")]
        [DefaultValue(false)]
        public bool SplitSubMenu
        {
            get { return _splitSubMenu; }
            
            set 
            {
                if (_splitSubMenu != value)
                {
                    _splitSubMenu = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("SplitSubMenu"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if the shortcut display text is shown.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Determines if the shortcut display text is shown.")]
        [DefaultValue(false)]
        public bool CheckOnClick
        {
            get { return _checkOnClick; }
            
            set 
            {
                if (_checkOnClick != value)
                {
                    _checkOnClick = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CheckOnClick"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if the shortcut display text is shown.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Determines if the shortcut display text is shown.")]
        [DefaultValue(true)]
        [Localizable(true)]
        public bool ShowShortcutKeys
        {
            get { return _showShortcutKeys; }
            
            set 
            {
                if (_showShortcutKeys != value)
                {
                    _showShortcutKeys = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ShowShortcutKeys"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if the large image is used from the attached KryptonCommand.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Determines if the large image is used from the attached KryptonCommand.")]
        [DefaultValue(false)]
        public bool LargeKryptonCommandImage
        {
            get { return _largeKryptonCommandImage; }
            
            set 
            {
                if (_largeKryptonCommandImage != value)
                {
                    _largeKryptonCommandImage = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LargeKryptonCommandImage"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the display text to use in preference to the shortcut key setting.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Display text to use in preference to the shortcut key setting.")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ShortcutKeyDisplayString
        {
            get { return _shortcutKeyDisplayString; }
            
            set 
            {
                if (_shortcutKeyDisplayString != value)
                {
                    _shortcutKeyDisplayString = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ShortcutKeyDisplayString"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if the menu item is in the checked state.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Indicates if the menu item is in the checked state.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Bindable(true)]
        public bool Checked
        {
            get { return (CheckState != CheckState.Unchecked); }
            
            set 
            {
                // Are we currently checked?
                bool areChecked = (CheckState != CheckState.Unchecked);

                // Only interested in a change of value
                if (areChecked != value)
                {
                    // Work out if the check state has changed, and update to new value
                    CheckState newCheckState = (value ? CheckState.Checked : CheckState.Unchecked);
                    bool checkStateChanged = (newCheckState != _checkState);
                    _checkState = newCheckState;

                    // Checked value has always changed
                    OnCheckedChanged(EventArgs.Empty);

                    // CheckState might have changed
                    if (checkStateChanged)
                        OnCheckStateChanged(EventArgs.Empty);

                    OnPropertyChanged(new PropertyChangedEventArgs("Checked"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the checked state of the menu item.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Indicates the checked state of the menu item.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(typeof(CheckState), "Unchecked")]
        [Bindable(true)]
        public CheckState CheckState
        {
            get { return _checkState; }
            
            set 
            {
                if (_checkState != value)
                {
                    bool oldChecked = Checked;
                    _checkState = value;

                    // Checked might have changed
                    if (Checked != oldChecked)
                        OnCheckedChanged(EventArgs.Empty);

                    // CheckState value has always changed
                    OnCheckStateChanged(EventArgs.Empty);
                    OnPropertyChanged(new PropertyChangedEventArgs("CheckState"));
                }
            }
        }

        /// <summary>
        /// Collection of sub-menu items for display.
        /// </summary>
        [Category("Data")]
        [Description("Collection of sub-menu items.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("ComponentFactory.Krypton.Toolkit.KryptonContextMenuCollectionEditor, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e", typeof(UITypeEditor))]
        public KryptonContextMenuCollection Items
        {
            get { return _items; }
        }

        /// <summary>
        /// Gets and sets if the menu item is enabled.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates whether the menu item is enabled.")]
        [DefaultValue(true)]
        [Bindable(true)]
        public bool Enabled
        {
            get { return _enabled; }
            
            set 
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Enabled"));
                }
            }
        }

        /// <summary>
        /// Gets access to the menu item disabled appearance values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining menu item disabled appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemState StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the menu item normal appearance values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining menu item normal appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemState StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the menu item normal appearance values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining menu item checked appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemStateChecked StateChecked
        {
            get { return _stateChecked; }
        }

        private bool ShouldSerializeStateChecked()
        {
            return !_stateChecked.IsDefault;
        }

        /// <summary>
        /// Gets access to the menu item highlight appearance values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining menu item highlight appearance values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemStateHighlight StateHighlight
        {
            get { return _stateHighlight; }
        }

        private bool ShouldSerializeStateHighlight()
        {
            return !_stateHighlight.IsDefault;
        }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Command associated with the menu item.")]
        [DefaultValue(null)]
        public virtual KryptonCommand KryptonCommand
        {
            get { return _command; }

            set
            {
                if (_command != value)
                {
                    _command = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("KryptonCommand"));
                }
            }
        }

        /// <summary>
        /// Generates a Click event for the component.
        /// </summary>
        public void PerformClick()
        {
            // Do we toggle the checked state when clicked>
            if (CheckOnClick)
            {
                // Grab current state from command or ourself
                CheckState state = (KryptonCommand == null ? CheckState : KryptonCommand.CheckState);
                
                // Find new state
                switch (state)
                {
                    case CheckState.Unchecked:
                        state = CheckState.Checked;
                        break;
                    case CheckState.Indeterminate:
                    case CheckState.Checked:
                        state = CheckState.Unchecked;
                        break;
                }

                // Update correct property
                if (KryptonCommand != null)
                    KryptonCommand.CheckState = state;
                else
                    CheckState = state;
            }

            OnClick(EventArgs.Empty);

            // If we have an attached command then execute it
            if (KryptonCommand != null)
                KryptonCommand.PerformExecute();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnClick(EventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }

        /// <summary>
        /// Raises the CheckedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanged != null)
                CheckedChanged(this, e);
        }

        /// <summary>
        /// Raises the CheckStateChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnCheckStateChanged(EventArgs e)
        {
            if (CheckStateChanged != null)
                CheckStateChanged(this, e);
        }
        #endregion

        #region Internal
        internal void SetPaletteRedirect(IContextMenuProvider provider)
        {
            _stateRedirect.SetRedirector(provider);
        }
        #endregion
    }
}
