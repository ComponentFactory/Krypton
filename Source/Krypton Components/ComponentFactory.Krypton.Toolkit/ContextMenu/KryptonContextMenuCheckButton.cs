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
    /// Provide a context menu check button.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonContextMenuCheckButton), "ToolboxBitmaps.KryptonCheckButton.bmp")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    [DefaultEvent("CheckedChanged")]
    public class KryptonContextMenuCheckButton : KryptonContextMenuItemBase
    {
        #region Instance Fields
        private bool _autoCheck;
        private bool _autoClose;
        private bool _checked;
        private bool _enabled;
        private string _text;
        private string _extraText;
        private Image _image;
        private Color _imageTransparentColor;
        private ButtonStyle _style;
        private PaletteTripleRedirect _stateCommon;
        private PaletteTripleRedirect _stateFocus;
        private PaletteTriple _stateDisabled;
        private PaletteTriple _stateNormal;
        private PaletteTriple _stateTracking;
        private PaletteTriple _statePressed;
        private PaletteTriple _stateCheckedNormal;
        private PaletteTriple _stateCheckedTracking;
        private PaletteTriple _stateCheckedPressed;
        private PaletteTripleOverride _overrideCheckedNormal;
        private PaletteTripleOverride _overrideCheckedTracking;
        private PaletteTripleOverride _overrideCheckedPressed;
        private PaletteTripleOverride _overrideNormal;
        private PaletteTripleOverride _overrideTracking;
        private PaletteTripleOverride _overridePressed;
        private PaletteTripleOverride _overrideDisabled;
        private KryptonCommand _command;
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
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuCheckButton class.
        /// </summary>
        public KryptonContextMenuCheckButton()
            : this("CheckButton")
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonContextMenuCheckButton class.
        /// </summary>
        /// <param name="initialText">Initial text for display.</param>
        public KryptonContextMenuCheckButton(string initialText)
        {
            // Default fields
            _enabled = true;
            _autoClose = false;
            _text = initialText;
            _extraText = string.Empty;
            _image = null;
            _imageTransparentColor = Color.Empty;
            _checked = false;
            _autoCheck = false;
            _style = ButtonStyle.Standalone;

            // Create the redirectors
            _stateCommon = new PaletteTripleRedirect(PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);
            _stateFocus = new PaletteTripleRedirect(PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone);

            // Create the palette storage
            _stateDisabled = new PaletteTriple(_stateCommon);
            _stateNormal = new PaletteTriple(_stateCommon);
            _stateTracking = new PaletteTriple(_stateCommon);
            _statePressed = new PaletteTriple(_stateCommon);
            _stateCheckedNormal = new PaletteTriple(_stateCommon);
            _stateCheckedTracking = new PaletteTriple(_stateCommon);
            _stateCheckedPressed = new PaletteTriple(_stateCommon);

            // Create the override handling classes
            _overrideDisabled = new PaletteTripleOverride(_stateFocus, _stateDisabled, PaletteState.FocusOverride);
            _overrideNormal = new PaletteTripleOverride(_stateFocus, _stateNormal, PaletteState.FocusOverride);
            _overrideTracking = new PaletteTripleOverride(_stateFocus, _stateTracking, PaletteState.FocusOverride);
            _overridePressed = new PaletteTripleOverride(_stateFocus, _statePressed, PaletteState.FocusOverride);
            _overrideCheckedNormal = new PaletteTripleOverride(_stateFocus, _stateCheckedNormal, PaletteState.FocusOverride);
            _overrideCheckedTracking = new PaletteTripleOverride(_stateFocus, _stateCheckedTracking, PaletteState.FocusOverride);
            _overrideCheckedPressed = new PaletteTripleOverride(_stateFocus, _stateCheckedPressed, PaletteState.FocusOverride);
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
            return new ViewDrawMenuCheckButton(provider, this);
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
        [DefaultValue("")]
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
        /// Gets and sets the check button style.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Check button style.")]
        [DefaultValue(typeof(ButtonStyle), "Standalone")]
        public ButtonStyle ButtonStyle
        {
            get { return _style; }

            set
            {
                if (_style != value)
                {
                    _style = value;
                    SetCheckButtonStyle(_style);
                    OnPropertyChanged(new PropertyChangedEventArgs("ButtonStyle"));
                }
            }
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
                    _checked = value;
                    OnCheckedChanged(EventArgs.Empty);
                    OnPropertyChanged(new PropertyChangedEventArgs("Checked"));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the check box is automatically changed state when clicked. 
        /// </summary>
        [KryptonPersist]
        [Category("Behavior")]
        [Description("Causes the check box to automatically change state when clicked.")]
        [DefaultValue(false)]
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
        /// Gets access to the common button appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common button appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the tracking button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tracking button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal checked button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal checked button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateCheckedNormal
        {
            get { return _stateCheckedNormal; }
        }

        private bool ShouldSerializeStateCheckedNormal()
        {
            return !_stateCheckedNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the hot tracking checked button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking checked button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateCheckedTracking
        {
            get { return _stateCheckedTracking; }
        }

        private bool ShouldSerializeStateCheckedTracking()
        {
            return !_stateCheckedTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed checked button appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed checked button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateCheckedPressed
        {
            get { return _stateCheckedPressed; }
        }

        private bool ShouldSerializeStateCheckedPressed()
        {
            return !_stateCheckedPressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the button appearance when it has focus.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining button appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect OverrideFocus
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
        [Description("Command associated with the menu check button.")]
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
        #endregion

        #region Internal
        internal PaletteTripleOverride OverrideCheckedNormal
        {
            get { return _overrideCheckedNormal; }
        }

        internal PaletteTripleOverride OverrideCheckedTracking
        {
            get { return _overrideCheckedTracking; }
        }

        internal PaletteTripleOverride OverrideCheckedPressed
        {
            get { return _overrideCheckedPressed; }
        }

        internal PaletteTripleOverride OverrideDisabled
        {
            get { return _overrideDisabled; }
        }

        internal PaletteTripleOverride OverrideNormal
        {
            get { return _overrideNormal; }
        }

        internal PaletteTripleOverride OverrideTracking
        {
            get { return _overrideTracking; }
        }

        internal PaletteTripleOverride OverridePressed
        {
            get { return _overridePressed; }
        }

        internal void SetPaletteRedirect(PaletteRedirect redirector)
        {
            _stateCommon.SetRedirector(redirector);
            _stateFocus.SetRedirector(redirector);
        }
        #endregion

        #region Private
        private void SetCheckButtonStyle(ButtonStyle style)
        {
            _stateCommon.SetStyles(style);
            _stateFocus.SetStyles(style);
        }
        #endregion
    }
}
