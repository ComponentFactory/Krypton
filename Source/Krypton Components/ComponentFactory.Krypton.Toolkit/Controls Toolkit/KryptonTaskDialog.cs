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
using System.Media;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Represents a task dialog for presenting different options to the user.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonTaskDialog), "ToolboxBitmaps.KryptonTaskDialog.bmp")]
    [DefaultEvent("PropertyChanged")]
    [DesignerCategory("code")]
    [Description("Displays a task dialog for presenting different options to the user.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonTaskDialog : Component, INotifyPropertyChanged
    {
        #region Instance Fields
        private VisualTaskDialog _taskDialog;
        private string _windowTitle;
        private string _mainInstruction;
        private string _content;
        private Image _customIcon;
        private MessageBoxIcon _icon;
        private KryptonTaskDialogCommandCollection _radioButtons;
        private KryptonTaskDialogCommandCollection _commandButtons;
        private KryptonTaskDialogCommand _defaultRadioButton;
        private TaskDialogButtons _commonButtons;
        private TaskDialogButtons _defaultButton;
        private MessageBoxIcon _footerIcon;
        private Image _customFooterIcon;
        private string _footerText;
        private string _footerHyperlink;
        private string _checkboxText;
        private bool _checkboxState;
        private bool _allowDialogClose;
        private object _tag;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the users clicks the footer hyperlink.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the users clicks the footer hyperlink.")]
        public event EventHandler FooterHyperlinkClicked;

        /// <summary>
        /// Occurs when a property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        ///  Initialize a new instance of the KryptonTaskDialog class.
        /// </summary>
        public KryptonTaskDialog()
        {
            _radioButtons = new KryptonTaskDialogCommandCollection();
            _commandButtons = new KryptonTaskDialogCommandCollection();
            _commonButtons = TaskDialogButtons.OK;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_taskDialog != null)
                {
                    _taskDialog.Dispose();
                    _taskDialog = null;
                }
            }

            base.Dispose(disposing);
        }        
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the caption of the window.
        /// </summary>
        [Category("Appearance")]
        [Description("Caption of the window.")]
        [DefaultValue("")]
        [Localizable(true)]
        [Bindable(true)]
        public string WindowTitle
        {
            get { return _windowTitle; }
            
            set 
            {
                if (_windowTitle != value)
                {
                    _windowTitle = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("WindowTitle"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the principal text.
        /// </summary>
        [Category("Appearance")]
        [Description("Principal text.")]
        [DefaultValue("")]
        [Localizable(true)]
        [Bindable(true)]
        public string MainInstruction
        {
            get { return _mainInstruction; }

            set
            {
                if (_mainInstruction != value)
                {
                    _mainInstruction = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("MainInstruction"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra text.
        /// </summary>
        [Category("Appearance")]
        [Description("Extra text.")]
        [DefaultValue("")]
        [Localizable(true)]
        [Bindable(true)]
        public string Content
        {
            get { return _content; }

            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Content"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the predefined icon.
        /// </summary>
        [Category("Appearance")]
        [Description("Predefined icon.")]
        [DefaultValue(typeof(MessageBoxIcon), "None")]
        public MessageBoxIcon Icon
        {
            get { return _icon; }

            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Icon"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the custom icon.
        /// </summary>
        [Category("Appearance")]
        [Description("Custom icon.")]
        [DefaultValue(null)]
        public Image CustomIcon
        {
            get { return _customIcon; }

            set
            {
                if (_customIcon != value)
                {
                    _customIcon = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CustomIcon"));
                }
            }
        }

        /// <summary>
        /// Gets access to the collection of radio button definitions.
        /// </summary>
        [Category("Appearance")]
        [Description("Collection of radio button definitions.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public KryptonTaskDialogCommandCollection RadioButtons
        {
            get { return _radioButtons; }
        }

        /// <summary>
        /// Gets access to the collection of command button definitions.
        /// </summary>
        [Category("Appearance")]
        [Description("Collection of command button definitions.")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public KryptonTaskDialogCommandCollection CommandButtons
        {
            get { return _commandButtons; }
        }

        /// <summary>
        /// Gets and sets the common dialog buttons.
        /// </summary>
        [Category("Appearance")]
        [Description("Common dialog buttons.")]
        [DefaultValue(typeof(TaskDialogButtons), "OK")]
        public TaskDialogButtons CommonButtons
        {
            get { return _commonButtons; }

            set
            {
                if (_commonButtons != value)
                {
                    _commonButtons = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CommonButtons"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the default radio button.
        /// </summary>
        [Category("Appearance")]
        [Description("Default radio button.")]
        [DefaultValue(typeof(TaskDialogButtons), "None")]
        public KryptonTaskDialogCommand DefaultRadioButton
        {
            get { return _defaultRadioButton; }

            set
            {
                if (_defaultRadioButton != value)
                {
                    _defaultRadioButton = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("DefaultRadioButton"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the default common button.
        /// </summary>
        [Category("Appearance")]
        [Description("Default Common button.")]
        [DefaultValue(typeof(TaskDialogButtons), "None")]
        public TaskDialogButtons DefaultButton
        {
            get { return _defaultButton; }

            set
            {
                if (_defaultButton != value)
                {
                    _defaultButton = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("DefaultButton"));
                }
            }
        }
        
        /// <summary>
        /// Gets and sets the predefined footer icon.
        /// </summary>
        [Category("Appearance")]
        [Description("Predefined footer icon.")]
        [DefaultValue(typeof(MessageBoxIcon), "None")]
        public MessageBoxIcon FooterIcon
        {
            get { return _footerIcon; }

            set
            {
                if (_footerIcon != value)
                {
                    _footerIcon = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("FooterIcon"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the custom footer icon.
        /// </summary>
        [Category("Appearance")]
        [Description("Custom footer icon.")]
        [DefaultValue(null)]
        public Image CustomFooterIcon
        {
            get { return _customFooterIcon; }

            set
            {
                if (_customFooterIcon != value)
                {
                    _customFooterIcon = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CustomFooterIcon"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the footer text.
        /// </summary>
        [Category("Appearance")]
        [Description("Footer text.")]
        [DefaultValue("")]
        [Localizable(true)]
        [Bindable(true)]
        public string FooterText
        {
            get { return _footerText; }

            set
            {
                if (_footerText != value)
                {
                    _footerText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("FooterText"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the footer hyperlink.
        /// </summary>
        [Category("Appearance")]
        [Description("Footer hyperlink.")]
        [DefaultValue("")]
        [Localizable(true)]
        [Bindable(true)]
        public string FooterHyperlink
        {
            get { return _footerHyperlink; }

            set
            {
                if (_footerHyperlink != value)
                {
                    _footerHyperlink = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("FooterHyperlink"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the Checkbox text.
        /// </summary>
        [Category("Appearance")]
        [Description("Checkbox text.")]
        [DefaultValue("")]
        [Localizable(true)]
        [Bindable(true)]
        public string CheckboxText
        {
            get { return _checkboxText; }

            set
            {
                if (_checkboxText != value)
                {
                    _checkboxText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CheckboxText"));
                }
            }
        }

        /// <summary>
        /// Gets and sets the Checkbox text.
        /// </summary>
        [Category("Appearance")]
        [Description("Checkbox state.")]
        [DefaultValue(false)]
        [Localizable(true)]
        [Bindable(true)]
        public bool CheckboxState
        {
            get { return _checkboxState; }

            set
            {
                if (_checkboxState != value)
                {
                    _checkboxState = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("CheckboxState"));
                }
            }
        }

        /// <summary>
        /// Gets and sets if the window can be closed.
        /// </summary>
        [Category("Appearance")]
        [Description("Can the user close the window.")]
        [DefaultValue(false)]
        public bool AllowDialogClose
        {
            get { return _allowDialogClose; }

            set
            {
                if (_allowDialogClose != value)
                {
                    _allowDialogClose = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("AllowDialogClose"));
                }
            }
        }

        /// <summary>
        /// Gets and sets user-defined data associated with the object.
        /// </summary>
        [Category("Data")]
        [Description("User-defined data associated with the object.")]
        [TypeConverter(typeof(StringConverter))]
        [Bindable(true)]
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private void ResetTag()
        {
            Tag = null;
        }

        private bool ShouldSerializeTag()
        {
            return (Tag != null);
        }

        /// <summary>
        /// Shows the task dialog as a modal dialog box with the currently active window set as its owner.
        /// </summary>
        /// <returns>One of the DialogResult values.</returns>
        public DialogResult ShowDialog()
        {
            return ShowDialog(Control.FromHandle(PI.GetActiveWindow()));
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements IWin32Window that represents the top-level window that will own the modal dialog box.</param>
        /// <returns>One of the DialogResult values.</returns>
        public DialogResult ShowDialog(IWin32Window owner)
        {
            // Remove any exising task dialog that is showing
            if (_taskDialog != null)
                _taskDialog.Dispose();

            // Create visual form to show our defined task properties
            _taskDialog = new VisualTaskDialog(this);

            if (owner == null)
                _taskDialog.StartPosition = FormStartPosition.CenterScreen;
            else
                _taskDialog.StartPosition = FormStartPosition.CenterParent;

            // Return result of showing the task dialog
            return _taskDialog.ShowDialog(owner);
        }

        /// <summary>
        /// Show a task dialog using the specified values as content.
        /// </summary>
        /// <param name="windowTitle">Caption of the window.</param>
        /// <param name="mainInstruction">Principal text.</param>
        /// <param name="content">Extra text.</param>
        /// <param name="icon">Predefined icon.</param>
        /// <param name="commonButtons">Common buttons.</param>
        /// <returns>One of the DialogResult values.</returns>
        public static DialogResult Show(string windowTitle,
                                        string mainInstruction,
                                        string content,
                                        MessageBoxIcon icon,
                                        TaskDialogButtons commonButtons)
        {
            // Create a temporary task dialog for storing definition whilst showing
            using (KryptonTaskDialog taskDialog = new KryptonTaskDialog())
            {
                // Store incoming values
                taskDialog.WindowTitle = windowTitle;
                taskDialog.MainInstruction = mainInstruction;
                taskDialog.Content = content;
                taskDialog.Icon = icon;
                taskDialog.CommonButtons = commonButtons;

                // Show as a modal dialog
                return taskDialog.ShowDialog();
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the PropertyFooterHyperlinkClickedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnFooterHyperlinkClicked(EventArgs e)
        {
            if (FooterHyperlinkClicked != null)
                FooterHyperlinkClicked(this, e);
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

        #region Internal
        internal void RaiseFooterHyperlinkClicked()
        {
            OnFooterHyperlinkClicked(EventArgs.Empty);
        }
        #endregion
    }
}
