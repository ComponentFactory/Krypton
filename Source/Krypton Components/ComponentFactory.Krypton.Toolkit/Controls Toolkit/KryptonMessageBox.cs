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
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonMessageBox), "ToolboxBitmaps.KryptonMessageBox.bmp")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonMessageBox : KryptonForm
    {
        #region Types
        internal class HelpInfo
        {
            #region Instance Fields
            private string _helpFilePath;
            private string _keyword;
            private HelpNavigator _navigator;
            private object _param;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the HelpInfo class.
            /// </summary>
            public HelpInfo()
            {
            }

            /// <summary>
            /// Initialize a new instance of the HelpInfo class.
            /// </summary>
            /// <param name="helpFilePath">Value for HelpFilePath.</param>
            public HelpInfo(string helpFilePath)
            {
                _helpFilePath = helpFilePath;
            }

            /// <summary>
            /// Initialize a new instance of the HelpInfo class.
            /// </summary>
            /// <param name="helpFilePath">Value for HelpFilePath.</param>
            /// <param name="keyword">Value for Keyword</param>
            public HelpInfo(string helpFilePath, string keyword)
            {
                _helpFilePath = helpFilePath;
                _keyword = keyword;
            }

            /// <summary>
            /// Initialize a new instance of the HelpInfo class.
            /// </summary>
            /// <param name="helpFilePath">Value for HelpFilePath.</param>
            /// <param name="navigator">Value for Navigator</param>
            public HelpInfo(string helpFilePath, HelpNavigator navigator)
            {
                _helpFilePath = helpFilePath;
                _navigator = navigator;
            }

            /// <summary>
            /// Initialize a new instance of the HelpInfo class.
            /// </summary>
            /// <param name="helpFilePath">Value for HelpFilePath.</param>
            /// <param name="navigator">Value for Navigator</param>
            /// <param name="param">Value for Param</param>
            public HelpInfo(string helpFilePath, HelpNavigator navigator, object param)
            {
                _helpFilePath = helpFilePath;
                _navigator = navigator;
                _param = param;
            }
            #endregion

            #region Properties
            /// <summary>
            /// Gets the HelpFilePath property.
            /// </summary>
            public string HelpFilePath 
            { 
                get { return _helpFilePath; } 
            }

            /// <summary>
            /// Gets the Keyword property.
            /// </summary>
            public string Keyword
            {
                get { return _keyword; } 
            }

            /// <summary>
            /// Gets the Navigator property.
            /// </summary>
            public HelpNavigator Navigator
            {
                get { return _navigator; } 
            }

            /// <summary>
            /// Gets the Param property.
            /// </summary>
            public object Param
            {
                get { return _param; }
            }
            #endregion
        }

        [ToolboxItem(false)]
        internal class MessageButton : KryptonButton
        {
            #region Instance Fields
            private bool _ignoreAltF4;
            #endregion

            #region Identity
            /// <summary>
            /// Gets and sets the ignoring of Alt+F4
            /// </summary>
            public bool IgnoreAltF4
            {
                get { return _ignoreAltF4; }
                set { _ignoreAltF4 = value; }
            }
            #endregion

            #region Protected
            /// <summary>
            /// Processes Windows messages.
            /// </summary>
            /// <param name="m">The Windows Message to process. </param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case PI.WM_KEYDOWN:
                    case PI.WM_SYSKEYDOWN:
                        if (IgnoreAltF4)
                        {
                            // Extract the keys being pressed
                            Keys keys = ((Keys)((int)m.WParam.ToInt64()));

                            // If the user standard combination ALT + F4
                            if ((keys == Keys.F4) && ((Control.ModifierKeys & Keys.Alt) == Keys.Alt))
                            {
                                // Eat the message, so standard window proc does not close the window
                                return;
                            }
                        }
                        break;
                }

                base.WndProc(ref m);
            }
            #endregion
        }
        #endregion

        #region Static Fields
        private static readonly int GAP = 10;
        private static int _osMajorVersion;
        #endregion

        #region Instance Fields
        private string _text;
        private string _caption;
        private MessageBoxButtons _buttons;
        private MessageBoxIcon _icon;
        private MessageBoxDefaultButton _defaultButton;
        private MessageBoxOptions _options;
        private KryptonPanel _panelMessage;
        private KryptonPanel _panelMessageText;
        private KryptonWrapLabel _messageText;
        private KryptonPanel _panelMessageIcon;
        private PictureBox _messageIcon;
        private KryptonPanel _panelButtons;
        private MessageButton _button1;
        private MessageButton _button2;
        private MessageButton _button3;
        private KryptonBorderEdge borderEdge;
        private HelpInfo _helpInfo;
        #endregion

        #region Identity
        static KryptonMessageBox()
        {
            _osMajorVersion = Environment.OSVersion.Version.Major;
        }

        private KryptonMessageBox(string text, string caption,
                                  MessageBoxButtons buttons, MessageBoxIcon icon,
                                  MessageBoxDefaultButton defaultButton, MessageBoxOptions options,
                                  HelpInfo helpInfo)
        {
            // Store incoming values
            _text = text;
            _caption = caption;
            _buttons = buttons;
            _icon = icon;
            _defaultButton = defaultButton;
            _options = options;
            _helpInfo = helpInfo;

            // Create the form contents
            InitializeComponent();

            // Update contents to match requirements
            UpdateText();
            UpdateIcon();
            UpdateButtons();
            UpdateDefault();
            UpdateHelp();

            // Finally calculate and set form sizing
            UpdateSizing();
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
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text)
        {
            return InternalShow(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, null);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return InternalShow(owner, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, null);
        }

        /// <summary>
        /// Displays a message box with specified text and caption.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption)
        {
            return InternalShow(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, null);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text and caption.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, 
                                        string text, string caption)
        {
            return InternalShow(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, null);
        }

        /// <summary>
        /// Displays a message box with specified text, caption, and buttons.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, 
                                        MessageBoxButtons buttons)
        {
            return InternalShow(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, null);
        }
        
        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, and buttons.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, 
                                        string text, string caption, 
                                        MessageBoxButtons buttons)
        {
            return InternalShow(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0, null);
        }
        
        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return InternalShow(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, null);
        }
        
        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, 
                                        string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return InternalShow(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0, null);
        }
        
        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, and default button.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption,    
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton)
        {
            return InternalShow(null, text, caption, buttons, icon, defaultButton, 0, null);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, and default button.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, 
                                        string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton)
        {
            return InternalShow(owner, text, caption, buttons, icon, defaultButton, 0, null);
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return InternalShow(null, text, caption, buttons, icon, defaultButton, options, null);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, 
                                        string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return InternalShow(owner, text, caption, buttons, icon, defaultButton, options, null);
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="displayHelpButton">Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, 
                                        bool displayHelpButton)
        {
            return InternalShow(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton ? new HelpInfo() : null);
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, 
                                        string helpFilePath)
        {
            return InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath));
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, 
                                        string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, 
                                        string helpFilePath)
        {
            return InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath));
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and HelpNavigator.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, 
                                        string helpFilePath, HelpNavigator navigator)
        {
            return InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator));
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="keyword">The Help keyword to display when the user clicks the Help button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, 
                                        string helpFilePath, string keyword)
        {
            return InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, keyword));
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and HelpNavigator.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, 
                                        string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, 
                                        string helpFilePath, HelpNavigator navigator)
        {
            return InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator));
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="keyword">The Help keyword to display when the user clicks the Help button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, 
                                        string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, 
                                        string helpFilePath, string keyword)
        {
            return InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, keyword));
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption, 
                                        MessageBoxButtons buttons, MessageBoxIcon icon, 
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options, 
                                        string helpFilePath, HelpNavigator navigator, object param)
        {
            return InternalShow(null, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param));
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.
        /// </summary>
        /// <param name="owner">Owner of the modal dialog box.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton values that specifies the default button for the message box.</param>
        /// <param name="options">One of the System.Windows.Forms.MessageBoxOptions values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        /// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button.</param>
        /// <param name="navigator">One of the System.Windows.Forms.HelpNavigator values.</param>
        /// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner,
                                        string text, string caption,
                                        MessageBoxButtons buttons, MessageBoxIcon icon,
                                        MessageBoxDefaultButton defaultButton, MessageBoxOptions options,
                                        string helpFilePath, HelpNavigator navigator, object param)
        {
            return InternalShow(owner, text, caption, buttons, icon, defaultButton, options, new HelpInfo(helpFilePath, navigator, param));
        }
        #endregion

        #region Implementation
        private static DialogResult InternalShow(IWin32Window owner,
                                                 string text, string caption,
                                                 MessageBoxButtons buttons, 
                                                 MessageBoxIcon icon,
                                                 MessageBoxDefaultButton defaultButton, 
                                                 MessageBoxOptions options,
                                                 HelpInfo helpInfo)
        {
            // Check if trying to show a message box from a non-interactive process, this is not possible
            if (!SystemInformation.UserInteractive && ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == 0))
                throw new InvalidOperationException("Cannot show modal dialog when non-interactive");

            // Check if trying to show a message box from a service and the owner has been specified, this is not possible
            if ((owner != null) && ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != 0))
                throw new ArgumentException("Cannot show message box from a service with an owner specified", "options");

            // Check if trying to show a message box from a service and help information is specified, this is not possible
            if ((helpInfo != null) && ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != 0))
                throw new ArgumentException("Cannot show message box from a service with help specified", "options");

            // If help information provided or we are not a service/default desktop application then grab an owner for showing the message box
            IWin32Window showOwner = null;
            if ((helpInfo != null) || ((options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == 0))
            {
                // If do not have an owner passed in then get the active window and use that instead
                if (owner == null)
                    showOwner = Control.FromHandle(PI.GetActiveWindow());
                else
                    showOwner = owner;
            }

            // Show message box window as a modal dialog and then dispose of it afterwards
            using (KryptonMessageBox kmb = new KryptonMessageBox(text, caption, buttons, icon, defaultButton, options, helpInfo))
            {
                if (showOwner == null)
                    kmb.StartPosition = FormStartPosition.CenterScreen;
                else
                    kmb.StartPosition = FormStartPosition.CenterParent;

                return kmb.ShowDialog(showOwner);
            }
        }

        private void UpdateText()
        {
            Text = _caption;
            _messageText.Text = _text;
        }

        private void UpdateIcon()
        {
            switch (_icon)
            {
                case MessageBoxIcon.None:
                    _panelMessageIcon.Visible = false;
                    _panelMessageText.Left -= _messageIcon.Right;

                    // Windows XP and before will Beep, Vista and above do not!
                    if (_osMajorVersion < 6)
                        System.Media.SystemSounds.Beep.Play();
                    break;
                case MessageBoxIcon.Question:
                    _messageIcon.Image = Properties.Resources.help2;
                    System.Media.SystemSounds.Question.Play();
                    break;
                case MessageBoxIcon.Information:
                    _messageIcon.Image = Properties.Resources.information;
                    System.Media.SystemSounds.Asterisk.Play();
                    break;
                case MessageBoxIcon.Warning:
                    _messageIcon.Image = Properties.Resources.sign_warning;
                    System.Media.SystemSounds.Exclamation.Play();
                    break;
                case MessageBoxIcon.Error:
                    _messageIcon.Image = Properties.Resources.error;
                    System.Media.SystemSounds.Hand.Play();
                    break;
            }
        }

        private void UpdateButtons()
        {
            switch (_buttons)
            {
                case MessageBoxButtons.OK:
                    _button1.Text = KryptonManager.Strings.OK;
                    _button1.DialogResult = DialogResult.OK;
                    _button2.Visible = _button3.Visible = false;
                    break;
                case MessageBoxButtons.OKCancel:
                    _button1.Text = KryptonManager.Strings.OK;
                    _button2.Text = KryptonManager.Strings.Cancel;
                    _button1.DialogResult = DialogResult.OK;
                    _button2.DialogResult = DialogResult.Cancel;
                    _button3.Visible = false;
                    break;
                case MessageBoxButtons.YesNo:
                    _button1.Text = KryptonManager.Strings.Yes;
                    _button2.Text = KryptonManager.Strings.No;
                    _button1.DialogResult = DialogResult.Yes;
                    _button2.DialogResult = DialogResult.No;
                    _button3.Visible = false;
                    ControlBox = false;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    _button1.Text = KryptonManager.Strings.Yes;
                    _button2.Text = KryptonManager.Strings.No;
                    _button3.Text = KryptonManager.Strings.Cancel;
                    _button1.DialogResult = DialogResult.Yes;
                    _button2.DialogResult = DialogResult.No;
                    _button3.DialogResult = DialogResult.Cancel;
                    break;
                case MessageBoxButtons.RetryCancel:
                    _button1.Text = KryptonManager.Strings.Retry;
                    _button2.Text = KryptonManager.Strings.Cancel;
                    _button1.DialogResult = DialogResult.Retry;
                    _button2.DialogResult = DialogResult.Cancel;
                    _button3.Visible = false;
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    _button1.Text = KryptonManager.Strings.Abort;
                    _button2.Text = KryptonManager.Strings.Retry;
                    _button3.Text = KryptonManager.Strings.Ignore;
                    _button1.DialogResult = DialogResult.Abort;
                    _button2.DialogResult = DialogResult.Retry;
                    _button3.DialogResult = DialogResult.Ignore;
                    ControlBox = false;
                    break;
            }

            // Do we ignore the Alt+F4 on the buttons?
            if (!ControlBox)
            {
                _button1.IgnoreAltF4 = true;
                _button2.IgnoreAltF4 = true;
                _button3.IgnoreAltF4 = true;
            }
        }

        private void UpdateDefault()
        {
            switch (_defaultButton)
            {
                case MessageBoxDefaultButton.Button2:
                    _button2.Select();
                    break;
                case MessageBoxDefaultButton.Button3:
                    _button3.Select();
                    break;
            }
        }

        private void UpdateHelp()
        {
        }

        private void UpdateSizing()
        {
            Size messageSizing = UpdateMessageSizing();
            Size buttonsSizing = UpdateButtonsSizing();

            // Size of window is calculated from the client area
            ClientSize = new Size(Math.Max(messageSizing.Width, buttonsSizing.Width),
                                  messageSizing.Height + buttonsSizing.Height);
        }

        private Size UpdateMessageSizing()
        {
            // Update size of the message label but with a maximum width
            using (Graphics g = CreateGraphics())
            {
                // Find size of the label when it has a maximum length of 400
                _messageText.UpdateFont();
                Size messageSize = g.MeasureString(_text, _messageText.Font, 400).ToSize();

                // Work out DPI adjustment factor
                float factorX = g.DpiX > 96 ? (1.0f * g.DpiX / 96) : 1.0f;
                float factorY = g.DpiY > 96 ? (1.0f * g.DpiY / 96) : 1.0f;
                messageSize.Width = (int)((float)messageSize.Width * factorX);
                messageSize.Height = (int)((float)messageSize.Height * factorY);

                // Always add on ad extra 5 pixels as sometimes the measure size does not draw the last 
                // character it contains, this ensures there is always definitely enough space for it all
                messageSize.Width += 5;
                _messageText.Size = messageSize;
            }

            // Resize panel containing the message text
            Padding panelMessagePadding = _panelMessageText.Padding;
            _panelMessageText.Width = _messageText.Size.Width + panelMessagePadding.Horizontal;
            _panelMessageText.Height = _messageText.Size.Height + panelMessagePadding.Vertical;

            // Find size of icon area plus the text area added together
            Size panelSize = _panelMessageText.Size;
            if (_messageIcon.Image != null)
            {
                panelSize.Width += _panelMessageIcon.Width;
                panelSize.Height = Math.Max(panelSize.Height, _panelMessageIcon.Height);
            }

            // Enforce a minimum size for the message area
            panelSize = new Size(Math.Max(_panelMessage.Size.Width, panelSize.Width),
                                 Math.Max(_panelMessage.Size.Height, panelSize.Height));

            // Note that the width will be ignored in this update, but that is fine as 
            // it will be adjusted by the UpdateSizing method that is the caller.
            _panelMessage.Size = panelSize;
            return panelSize;
        }

        private Size UpdateButtonsSizing()
        {
            int numButtons = 1;
            
            // Button1 is always visible
            Size button1Size = _button1.GetPreferredSize(Size.Empty);
            Size maxButtonSize = new Size(button1Size.Width + GAP, button1Size.Height);

            // If Button2 is visible
            switch (_buttons)
            {
                case MessageBoxButtons.OKCancel:
                case MessageBoxButtons.YesNo:
                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.RetryCancel:
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        numButtons++;
                        Size button2Size = _button2.GetPreferredSize(Size.Empty);
                        maxButtonSize.Width = Math.Max(maxButtonSize.Width, button2Size.Width + GAP);
                        maxButtonSize.Height = Math.Max(maxButtonSize.Height, button2Size.Height);
                    }
                    break;
            }

            // If Button3 is visible
            switch (_buttons)
            {
                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        numButtons++;
                        Size button3Size = _button2.GetPreferredSize(Size.Empty);
                        maxButtonSize.Width = Math.Max(maxButtonSize.Width, button3Size.Width + GAP);
                        maxButtonSize.Height = Math.Max(maxButtonSize.Height, button3Size.Height);
                    }
                    break;
            }

            // Start positioning buttons 10 pixels from right edge
            int right = _panelButtons.Right - GAP;

            // If Button3 is visible
            switch (_buttons)
            {
                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        _button3.Location = new Point(right - maxButtonSize.Width, GAP);
                        _button3.Size = maxButtonSize;
                        right -= maxButtonSize.Width + GAP;
                    }
                    break;
            }

            // If Button2 is visible
            switch (_buttons)
            {
                case MessageBoxButtons.OKCancel:
                case MessageBoxButtons.YesNo:
                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.RetryCancel:
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        _button2.Location = new Point(right - maxButtonSize.Width, GAP);
                        _button2.Size = maxButtonSize;
                        right -= maxButtonSize.Width + GAP;
                    }
                    break;
            }

            // Button1 is always visible
            _button1.Location = new Point(right - maxButtonSize.Width, GAP);
            _button1.Size = maxButtonSize;

            // Size the panel for the buttons
            _panelButtons.Size = new Size((maxButtonSize.Width * numButtons) + GAP * (numButtons + 1), maxButtonSize.Height + GAP * 2);

            // Button area is the number of buttons with gaps between them and 10 pixels around all edges
            return new Size((maxButtonSize.Width * numButtons) + GAP * (numButtons + 1), maxButtonSize.Height + GAP * 2);
        }

        private void button_keyDown(object sender, KeyEventArgs e)
        {
            // Escape key kills the dialog if we allow it to be closed
            if ((e.KeyCode == Keys.Escape) && ControlBox)
                Close();
            else
            {
                // Pressing Ctrl+C should copy message text into the clipboard
                if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.C))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("---------------------------");
                    sb.AppendLine(_caption);
                    sb.AppendLine("---------------------------");
                    sb.AppendLine(_text);
                    sb.AppendLine("---------------------------");
                    sb.Append(_button1.Text);
                    sb.Append("   ");
                    if (_button2.Visible)
                    {
                        sb.Append(_button2.Text);
                        sb.Append("   ");
                        if (_button3.Visible)
                        {
                            sb.Append(_button3.Text);
                            sb.Append("   ");
                        }
                    }
                    sb.AppendLine("");
                    sb.AppendLine("---------------------------");

                    Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
                }
            }
        }

        private void InitializeComponent()
        {
            this._panelMessage = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this._panelMessageText = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this._messageText = new ComponentFactory.Krypton.Toolkit.KryptonWrapLabel();
            this._panelMessageIcon = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this._messageIcon = new System.Windows.Forms.PictureBox();
            this._panelButtons = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.borderEdge = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this._button3 = new ComponentFactory.Krypton.Toolkit.KryptonMessageBox.MessageButton();
            this._button1 = new ComponentFactory.Krypton.Toolkit.KryptonMessageBox.MessageButton();
            this._button2 = new ComponentFactory.Krypton.Toolkit.KryptonMessageBox.MessageButton();
            ((System.ComponentModel.ISupportInitialize)(this._panelMessage)).BeginInit();
            this._panelMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelMessageText)).BeginInit();
            this._panelMessageText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelMessageIcon)).BeginInit();
            this._panelMessageIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._messageIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelButtons)).BeginInit();
            this._panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelMessage
            // 
            this._panelMessage.AutoSize = true;
            this._panelMessage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._panelMessage.Controls.Add(this._panelMessageText);
            this._panelMessage.Controls.Add(this._panelMessageIcon);
            this._panelMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelMessage.Location = new System.Drawing.Point(0, 0);
            this._panelMessage.Name = "_panelMessage";
            this._panelMessage.Size = new System.Drawing.Size(156, 52);
            this._panelMessage.TabIndex = 0;
            // 
            // _panelMessageText
            // 
            this._panelMessageText.AutoSize = true;
            this._panelMessageText.Controls.Add(this._messageText);
            this._panelMessageText.Location = new System.Drawing.Point(42, 0);
            this._panelMessageText.Margin = new System.Windows.Forms.Padding(0);
            this._panelMessageText.Name = "_panelMessageText";
            this._panelMessageText.Padding = new System.Windows.Forms.Padding(5, 17, 5, 17);
            this._panelMessageText.Size = new System.Drawing.Size(88, 52);
            this._panelMessageText.TabIndex = 1;
            // 
            // _messageText
            // 
            this._messageText.AutoSize = false;
            this._messageText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._messageText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this._messageText.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this._messageText.Location = new System.Drawing.Point(5, 18);
            this._messageText.Margin = new System.Windows.Forms.Padding(0);
            this._messageText.Name = "_messageText";
            this._messageText.Size = new System.Drawing.Size(78, 15);
            this._messageText.Text = "Message Text";
            // 
            // _panelMessageIcon
            // 
            this._panelMessageIcon.AutoSize = true;
            this._panelMessageIcon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._panelMessageIcon.Controls.Add(this._messageIcon);
            this._panelMessageIcon.Location = new System.Drawing.Point(0, 0);
            this._panelMessageIcon.Margin = new System.Windows.Forms.Padding(0);
            this._panelMessageIcon.Name = "_panelMessageIcon";
            this._panelMessageIcon.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this._panelMessageIcon.Size = new System.Drawing.Size(42, 52);
            this._panelMessageIcon.TabIndex = 0;
            // 
            // _messageIcon
            // 
            this._messageIcon.BackColor = System.Drawing.Color.Transparent;
            this._messageIcon.Location = new System.Drawing.Point(10, 10);
            this._messageIcon.Margin = new System.Windows.Forms.Padding(0);
            this._messageIcon.Name = "_messageIcon";
            this._messageIcon.Size = new System.Drawing.Size(32, 32);
            this._messageIcon.TabIndex = 0;
            this._messageIcon.TabStop = false;
            // 
            // _panelButtons
            // 
            this._panelButtons.Controls.Add(this.borderEdge);
            this._panelButtons.Controls.Add(this._button3);
            this._panelButtons.Controls.Add(this._button1);
            this._panelButtons.Controls.Add(this._button2);
            this._panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelButtons.Location = new System.Drawing.Point(0, 52);
            this._panelButtons.Margin = new System.Windows.Forms.Padding(0);
            this._panelButtons.Name = "_panelButtons";
            this._panelButtons.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this._panelButtons.Size = new System.Drawing.Size(156, 26);
            this._panelButtons.TabIndex = 0;
            // 
            // borderEdge
            // 
            this.borderEdge.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.HeaderPrimary;
            this.borderEdge.Dock = System.Windows.Forms.DockStyle.Top;
            this.borderEdge.Location = new System.Drawing.Point(0, 0);
            this.borderEdge.Name = "borderEdge";
            this.borderEdge.Size = new System.Drawing.Size(156, 1);
            this.borderEdge.Text = "kryptonBorderEdge1";
            // 
            // _button3
            // 
            this._button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button3.AutoSize = true;
            this._button3.IgnoreAltF4 = false;
            this._button3.Location = new System.Drawing.Point(106, 0);
            this._button3.Margin = new System.Windows.Forms.Padding(0);
            this._button3.MinimumSize = new System.Drawing.Size(50, 26);
            this._button3.Name = "_button3";
            this._button3.Size = new System.Drawing.Size(50, 26);
            this._button3.TabIndex = 2;
            this._button3.Values.Text = "B3";
            this._button3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button_keyDown);
            // 
            // _button1
            // 
            this._button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button1.AutoSize = true;
            this._button1.IgnoreAltF4 = false;
            this._button1.Location = new System.Drawing.Point(6, 0);
            this._button1.Margin = new System.Windows.Forms.Padding(0);
            this._button1.MinimumSize = new System.Drawing.Size(50, 26);
            this._button1.Name = "_button1";
            this._button1.Size = new System.Drawing.Size(50, 26);
            this._button1.TabIndex = 0;
            this._button1.Values.Text = "B1";
            this._button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button_keyDown);
            // 
            // _button2
            // 
            this._button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._button2.AutoSize = true;
            this._button2.IgnoreAltF4 = false;
            this._button2.Location = new System.Drawing.Point(56, 0);
            this._button2.Margin = new System.Windows.Forms.Padding(0);
            this._button2.MinimumSize = new System.Drawing.Size(50, 26);
            this._button2.Name = "_button2";
            this._button2.Size = new System.Drawing.Size(50, 26);
            this._button2.TabIndex = 1;
            this._button2.Values.Text = "B2";
            this._button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button_keyDown);
            // 
            // KryptonMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(156, 78);
            this.Controls.Add(this._panelButtons);
            this.Controls.Add(this._panelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KryptonMessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this._panelMessage)).EndInit();
            this._panelMessage.ResumeLayout(false);
            this._panelMessage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panelMessageText)).EndInit();
            this._panelMessageText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._panelMessageIcon)).EndInit();
            this._panelMessageIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._messageIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panelButtons)).EndInit();
            this._panelButtons.ResumeLayout(false);
            this._panelButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
