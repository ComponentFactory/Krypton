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
    /// Defines state and events for a single task dialog command.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonCommand), "ToolboxBitmaps.KryptonTaskDialogCommand.bmp")]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [DesignerCategory("code")]
    [Description("Defines state and events for a single task dialog command.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonTaskDialogCommand : Component, IKryptonCommand, INotifyPropertyChanged
    {
        #region Instance Fields
        private bool _enabled;
        private string _text;
        private string _extraText;
        private Image _image;
        private Color _imageTransparentColor;
        private DialogResult _dialogResult;
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
        public KryptonTaskDialogCommand()
        {
            _enabled = true;
            _text = string.Empty;
            _extraText = string.Empty;
            _image = null;
            _imageTransparentColor = Color.Empty;
            _dialogResult = DialogResult.OK;
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
        /// Gets and sets DialogResult to use when the command is pressed.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("DialogResult to use when the command is pressed.")]
        [DefaultValue(true)]
        public DialogResult DialogResult
        {
            get { return _dialogResult; }

            set
            {
                if (_dialogResult != value)
                {
                    _dialogResult = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("DialogResult"));
                }
            }
        }

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
        /// Gets and sets the command small image.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Command small image.")]
        public Image Image
        {
            get { return _image; }

            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ImageSmall"));
                }
            }
        }

        private void ResetImage()
        {
            Image = null;
        }

        private bool ShouldSerializeImage()
        {
            return (Image != null);
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

        #region Private
        /// <summary>
        /// Gets and sets the command small image.
        /// </summary>
        Image IKryptonCommand.ImageSmall 
        {
            get { return Image; }
            set { Image = value; }
        }

        /// <summary>
        /// Gets and sets the command large image.
        /// </summary>
        Image IKryptonCommand.ImageLarge
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// Gets and sets the text line 1 property.
        /// </summary>
        string IKryptonCommand.TextLine1
        {
            get { return string.Empty; }
            set { }
        }

        /// <summary>
        /// Gets and sets the text line 2 property.
        /// </summary>
        string IKryptonCommand.TextLine2
        {
            get { return string.Empty; }
            set { }
        }

        /// <summary>
        /// Gets and sets the checked state of the command.
        /// </summary>
        bool IKryptonCommand.Checked
        {
            get { return true; }
            set { }
        }

        /// <summary>
        /// Gets and sets the check state of the command.
        /// </summary>
        CheckState IKryptonCommand.CheckState
        {
            get { return CheckState.Unchecked; }
            set { }
        }
        #endregion
    }

    /// <summary>
    /// Manages a collection of KryptonTaskDialogCommand instances.
    /// </summary>
    public class KryptonTaskDialogCommandCollection : TypedCollection<KryptonTaskDialogCommand>
    {
        #region Public
        /// <summary>
        /// Gets the item with the provided name.
        /// </summary>
        /// <param name="name">Name to find.</param>
        /// <returns>Item with matching name.</returns>
        public override KryptonTaskDialogCommand this[string name]
        {
            get
            {
                if (!string.IsNullOrEmpty(name))
                {
                    foreach (KryptonTaskDialogCommand item in this)
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
