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
    /// Provide a context menu check box.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonContextMenuCheckBox), "ToolboxBitmaps.KryptonCheckBox.bmp")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    [DefaultEvent("CheckedChanged")]
    public class KryptonContextMenuCheckBox : KryptonContextMenuItemBase
    {
        #region Instance Fields
        private bool _threeState;
        private bool _autoCheck;
        private bool _autoClose;
        private bool _checked;
        private bool _enabled;
        private string _text;
        private string _extraText;
        private Image _image;
        private Color _imageTransparentColor;
        private CheckState _checkState;
        private PaletteContentInheritRedirect _stateCommonRedirect;
        private PaletteContent _stateCommon;
        private PaletteContent _stateDisabled;
        private PaletteContent _stateNormal;
        private PaletteContent _stateFocus;
        private PaletteContentInheritOverride _overrideNormal;
        private PaletteContentInheritOverride _overrideDisabled;
        private PaletteRedirectCheckBox _stateCheckBoxImages;
        private KryptonCommand _command;
        private CheckBoxImages _images;
        private LabelStyle _style;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the check box item is clicked.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the check box item is clicked.")]
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the value of the Checked property has changed.
        /// </summary>
        [Category("Misc")]
        [Description("Occurs whenever the Checked property has changed.")]
        public event EventHandler CheckedChanged;

        /// <summary>
        /// Occurs when the value of the CheckState property has changed.
        /// </summary>
        [Category("Misc")]
        [Description("Occurs whenever the CheckState property has changed.")]
        public event EventHandler CheckStateChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuCheckBox class.
        /// </summary>
        public KryptonContextMenuCheckBox()
            : this("CheckBox")
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuCheckBox class.
        /// </summary>
        /// <param name="initialText">Initial text for display.</param>
        public KryptonContextMenuCheckBox(string initialText)
        {
            // Default fields
            _enabled = true;
            _autoClose = false;
            _text = initialText;
            _extraText = string.Empty;
            _image = null;
            _imageTransparentColor = Color.Empty;
            _checkState = CheckState.Unchecked;
            _checked = false;
            _threeState = false;
            _autoCheck = true;
            _style = LabelStyle.NormalControl;
            _images = new CheckBoxImages();

            // Create the redirectors
            _stateCommonRedirect = new PaletteContentInheritRedirect(PaletteContentStyle.LabelNormalControl);
            _stateCheckBoxImages = new PaletteRedirectCheckBox(_images);

            // Create the states
            _stateCommon = new PaletteContent(_stateCommonRedirect);
            _stateDisabled = new PaletteContent(_stateCommon);
            _stateNormal = new PaletteContent(_stateCommon);
            _stateFocus = new PaletteContent(_stateCommonRedirect);

            // Override the normal/disabled values with the focus, when the control has focus
            _overrideNormal = new PaletteContentInheritOverride(_stateFocus, _stateNormal, PaletteState.FocusOverride, false);
            _overrideDisabled = new PaletteContentInheritOverride(_stateFocus, _stateDisabled, PaletteState.FocusOverride, false);
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
            return new ViewDrawMenuCheckBox(provider, this);
        }

        /// <summary>
        /// Gets and sets if clicking the check box automatically closes the context menu.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates if clicking the check box automatically closes the context menu.")]
        [DefaultValue(false)]
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
        /// Gets and sets the check box text.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Main check box text.")]
        [DefaultValue("CheckBox")]
        [Localizable(true)]
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
        /// Gets and sets the check box extra text.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Check box extra text.")]
        [DefaultValue(null)]
        [Localizable(true)]
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
        /// Gets and sets the check box image.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Check box image.")]
        [DefaultValue(null)]
        [Localizable(true)]
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
        /// Gets and sets the check box image color to make transparent.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Check box image color to make transparent.")]
        [Localizable(true)]
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
        /// Gets and sets the check box label style.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Check box label style.")]
        [DefaultValue(typeof(LabelStyle), "NormalControl")]
        public LabelStyle LabelStyle
        {
            get { return _style; }

            set
            {
                if (_style != value)
                {
                    _style = value;
                    SetCheckBoxStyle(_style);
                    OnPropertyChanged(new PropertyChangedEventArgs("LabelStyle"));
                }
            }
        }

        /// <summary>
        /// Gets access to the image value overrides.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Image value overrides.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CheckBoxImages Images
        {
            get { return _images; }
        }

        private bool ShouldSerializeImages()
        {
            return !_images.IsDefault;
        }

        /// <summary>
        /// Gets and sets if the check box is enabled.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates whether the check box is enabled.")]
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
        /// Gets or sets a value indicating if the component is in the checked state.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Indicates if the component is in the checked state.")]
        [DefaultValue(false)]
        [Bindable(true)]
        public bool Checked
        {
            get { return _checked; }

            set
            {
                if (_checked != value)
                {
                    // Store new values
                    _checked = value;
                    _checkState = (_checked ? CheckState.Checked : CheckState.Unchecked);

                    // Generate events
                    OnCheckedChanged(EventArgs.Empty);
                    OnCheckStateChanged(EventArgs.Empty);
                    OnPropertyChanged(new PropertyChangedEventArgs("Checked"));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the checked state of the component.
        /// </summary>
        [KryptonPersist]
        [Category("Appearance")]
        [Description("Indicates the checked state of the component.")]
        [DefaultValue(typeof(CheckState), "Unchecked")]
        [Bindable(true)]
        public CheckState CheckState
        {
            get { return _checkState; }

            set
            {
                if (_checkState != value)
                {
                    // Store new values
                    _checkState = value;
                    bool newChecked = (_checkState != CheckState.Unchecked);
                    bool checkedChanged = (_checked != newChecked);
                    _checked = newChecked;

                    // Generate events
                    if (checkedChanged)
                        OnCheckedChanged(EventArgs.Empty);

                    OnCheckStateChanged(EventArgs.Empty);
                    OnPropertyChanged(new PropertyChangedEventArgs("CheckState"));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the check box is automatically changed state when clicked. 
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Causes the check box to automatically change state when clicked.")]
        [DefaultValue(true)]
        public bool AutoCheck
        {
            get { return _autoCheck; }
            
            set 
            {
                if (_autoCheck != value)
                {
                    _autoCheck = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("AutoCheck"));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the component allows three states instead of two.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Indicates if the component allows three states instead of two.")]
        [DefaultValue(false)]
        public bool ThreeState
        {
            get { return _threeState; }
            
            set 
            {
                if (_threeState != value)
                {
                    _threeState = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ThreeState"));
                }
            }
        }

        /// <summary>
        /// Gets access to the common check box appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common check box appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled check box appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled check box appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal check box appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal check box appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the check box appearance when it has focus.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining check box appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideFocus
        {
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_stateFocus.IsDefault;
        }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Command associated with the menu check box.")]
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
            OnClick(EventArgs.Empty);
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

            // If we have an attached command then execute it
            if (KryptonCommand != null)
                KryptonCommand.PerformExecute();
        }

        /// <summary>
        /// Raises the CheckedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanged != null)
                CheckedChanged(this, e);
        }

        /// <summary>
        /// Raises the CheckStateChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCheckStateChanged(EventArgs e)
        {
            if (CheckStateChanged != null)
                CheckStateChanged(this, e);
        }
        #endregion

        #region Internal
        internal PaletteContentInheritOverride OverrideNormal
        {
            get { return _overrideNormal; }
        }

        internal PaletteContentInheritOverride OverrideDisabled
        {
            get { return _overrideDisabled; }
        }

        internal PaletteRedirectCheckBox StateCheckBoxImages
        {
            get { return _stateCheckBoxImages; }
        }

        internal void SetPaletteRedirect(PaletteRedirect redirector)
        {
            _stateCommonRedirect.SetRedirector(redirector);
            _stateCheckBoxImages.Target = redirector;
        }
        #endregion

        #region Private
        private void SetCheckBoxStyle(LabelStyle style)
        {
            _stateCommonRedirect.Style = CommonHelper.ContentStyleFromLabelStyle(style);
        }
        #endregion
    }
}
