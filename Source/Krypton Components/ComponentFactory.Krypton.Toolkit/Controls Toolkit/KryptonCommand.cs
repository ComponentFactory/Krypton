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
using System.Xml;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Defines state and events for a single command.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonCommand), "ToolboxBitmaps.KryptonCommand.bmp")]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [DesignerCategory("code")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonCommandDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [Description("Defines state and events for a single command.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonCommand : Component, IKryptonCommand, INotifyPropertyChanged
    {
        #region Instance Fields
        private bool _enabled;
        private bool _checked;
        private CheckState _checkState;
        private string _text;
        private string _extraText;
        private string _textLine1;
        private string _textLine2;
        private Image _imageSmall;
        private Image _imageLarge;
        private Color _imageTransparentColor;
        private object _tag;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the command needs executing.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the command needs executing.")]
        public event EventHandler Execute;

        /// <summary>
        /// Occurs when a property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCommand class.
        /// </summary>
        public KryptonCommand()
        {
            _enabled = true;
            _checked = false;
            _checkState = CheckState.Unchecked;
            _text = string.Empty;
            _extraText = string.Empty;
            _textLine1 = string.Empty;
            _textLine2 = string.Empty;
            _imageSmall = null;
            _imageLarge = null;
            _imageTransparentColor = Color.Empty;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the enabled state of the command.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Indicates whether the command is enabled.")]
        [DefaultValue(true)]
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
        /// Gets and sets the checked state of the command.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Indicates whether the command is in the checked state.")]
        [DefaultValue(false)]
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
                    OnPropertyChanged(new PropertyChangedEventArgs("Checked"));
                    OnPropertyChanged(new PropertyChangedEventArgs("CheckState"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the check state of the command.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Indicates the checked state of the command.")]
        [DefaultValue(typeof(CheckState), "Unchecked")]
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
                        OnPropertyChanged(new PropertyChangedEventArgs("Checked"));

                    OnPropertyChanged(new PropertyChangedEventArgs("CheckState"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the command text.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Command text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

        private void ResetText()
        {
            Text = string.Empty;
        }

        private bool ShouldSerializeText()
        {
            return !string.IsNullOrEmpty(Text);
        }

        /// <summary>
        /// Gets and sets the command extra text.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Command extra text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

        private void ResetExtraText()
        {
            ExtraText = string.Empty;
        }

        private bool ShouldSerializeExtraText()
        {
            return !string.IsNullOrEmpty(ExtraText);
        }

        /// <summary>
        /// Gets and sets the command text line 1 for use in KryptonRibbon.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Command text line 1 for use in KryptonRibbon.")]
        public string TextLine1
        {
            get { return _textLine1; }

            set
            {
                if (_textLine1 != value)
                {
                    _textLine1 = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("TextLine1"));
                }
            }
        }

        private void ResetTextLine1()
        {
            TextLine1 = string.Empty;
        }

        private bool ShouldSerializeTextLine1()
        {
            return !string.IsNullOrEmpty(TextLine1);
        }

        /// <summary>
        /// Gets and sets the command text line 2 for use in KryptonRibbon.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Command text line 2 for use in KryptonRibbon.")]
        public string TextLine2
        {
            get { return _textLine2; }

            set
            {
                if (_textLine2 != value)
                {
                    _textLine2 = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("TextLine2"));
                }
            }
        }

        private void ResetTextLine2()
        {
            TextLine2 = string.Empty;
        }

        private bool ShouldSerializeTextLine2()
        {
            return !string.IsNullOrEmpty(TextLine2);
        }

        /// <summary>
        /// Gets and sets the command small image.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Command small image.")]
        public Image ImageSmall
        {
            get { return _imageSmall; }

            set
            {
                if (_imageSmall != value)
                {
                    _imageSmall = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ImageSmall"));
                }
            }
        }

        private void ResetImageSmall()
        {
            ImageSmall = null;
        }

        private bool ShouldSerializeImageSmall()
        {
            return (ImageSmall != null);
        }

        /// <summary>
        /// Gets and sets the command large image.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Command large image.")]
        public Image ImageLarge
        {
            get { return _imageLarge; }

            set
            {
                if (_imageLarge != value)
                {
                    _imageLarge = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ImageLarge"));
                }
            }
        }

        private void ResetImageLarge()
        {
            ImageLarge = null;
        }

        private bool ShouldSerializeImageLarge()
        {
            return (ImageLarge != null);
        }

        /// <summary>
        /// Gets and sets the command image transparent color.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Command image transparent color.")]
        [KryptonDefaultColorAttribute()]
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

        /// <summary>
        /// Gets and sets user-defined data associated with the object.
        /// </summary>
        [Category("Data")]
        [Description("User-defined data associated with the object.")]
        [TypeConverter(typeof(StringConverter))]
        [DefaultValue(null)]
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// Generates a Execute event for a button.
        /// </summary>
        public void PerformExecute()
        {
            OnExecute(EventArgs.Empty);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Execute event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnExecute(EventArgs e)
        {
            if (Execute != null)
                Execute(this, e);
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">A PropertyChangedEventArgs containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #endregion
    }

    /// <summary>
    /// Manages a collection of KryptonCommand instances.
    /// </summary>
    public class KryptonCommandCollection : TypedCollection<KryptonCommand>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided name.
        /// </summary>
        /// <param name="name">Name to find.</param>
        /// <returns>Item with matching name.</returns>
        public override KryptonCommand this[string name]
        {
            get
            {
                if (!string.IsNullOrEmpty(name))
                {
                    foreach (KryptonCommand item in this)
                    {
                        string text = item.Text;
                        if (!string.IsNullOrEmpty(text) && (text == name))
                            return item;

                        text = item.ExtraText;
                        if (!string.IsNullOrEmpty(text) && (text == name))
                            return item;
                    }
                }

                return null;
            }
        }
        #endregion
    };
}
