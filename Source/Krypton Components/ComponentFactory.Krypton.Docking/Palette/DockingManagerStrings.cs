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
using System.Drawing;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
	/// Storage for docking managee strings.
	/// </summary>
    public class DockingManagerStrings : Storage
    {
        #region Static Fields
        private static readonly string _defaultTextAutoHide = "Auto Hide";
        private static readonly string _defaultTextClose = "Close";
        private static readonly string _defaultTextCloseAllButThis = "Close All But This";
        private static readonly string _defaultTextDock = "Dock";
        private static readonly string _defaultTextFloat = "Float";
        private static readonly string _defaultTextHide = "Hide";
        private static readonly string _defaultTextTabbedDocument = "Tabbed Document";
        private static readonly string _defaultTextWindowLocation = "Window Position";
        #endregion

        #region Instance Fields
        private string _textAutoHide;
        private string _textClose;
        private string _textCloseAllButThis;
        private string _textDock;
        private string _textFloat;
        private string _textHide;
        private string _textTabbedDocument;
        private string _textWindowLocation;
        #endregion

        #region Events
        /// <summary>
        /// Occurs whenever a property has changed value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockingManagerStrings class.
		/// </summary>
        /// <param name="docking">Reference to owning docking manager.</param>
        public DockingManagerStrings(KryptonDockingManager docking)
            : base()
		{
            // Default values
            _textAutoHide = _defaultTextAutoHide;
            _textClose = _defaultTextClose;
            _textCloseAllButThis = _defaultTextCloseAllButThis;
            _textDock = _defaultTextDock;
            _textFloat = _defaultTextFloat;
            _textHide = _defaultTextHide;
            _textTabbedDocument = _defaultTextTabbedDocument;
            _textWindowLocation = _defaultTextWindowLocation;
        }
		#endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        [Browsable(false)]
        public override bool IsDefault
        {
            get
            {
                return (_textAutoHide.Equals(_defaultTextAutoHide) &&
                        _textClose.Equals(_defaultTextClose) &&
                        _textDock.Equals(_defaultTextDock) &&
                        _textFloat.Equals(_defaultTextFloat) &&
                        _textHide.Equals(_defaultTextHide) &&
                        _textTabbedDocument.Equals(_defaultTextTabbedDocument) &&
                        _textWindowLocation.Equals(_defaultTextWindowLocation));

            }
        }
        #endregion

        #region TextAutoHide
        /// <summary>
        /// Gets and sets the text to use for the auto hide button tooltip.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the auto hide button tooltip.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Auto Hide")]
        [Localizable(true)]
        public string TextAutoHide
        {
            get { return _textAutoHide; }
            
            set 
            {
                if (_textAutoHide != value)
                {
                    _textAutoHide = value;
                    OnPropertyChanged("TextAutoHide");
                }
            }
        }

        /// <summary>
        /// Resets the TextAutoHide property to its default value.
        /// </summary>
        public void ResetTextAutoHide()
        {
            TextAutoHide = _defaultTextAutoHide;
        }
        #endregion

        #region TextClose
        /// <summary>
        /// Gets and sets the text to use for the close button tooltip.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the close button tooltip.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Close")]
        [Localizable(true)]
        public string TextClose
        {
            get { return _textClose; }
            
            set 
            {
                if (_textClose != value)
                {
                    _textClose = value;
                    OnPropertyChanged("TextClose");
                }
            }
        }

        /// <summary>
        /// Resets the TextClose property to its default value.
        /// </summary>
        public void ResetTextClose()
        {
            TextClose = _defaultTextClose;
        }
        #endregion

        #region TextCloseAllButThis
        /// <summary>
        /// Gets and sets the text to use for the 'close all but this' button tooltip.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the 'close all but this' button tooltip.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Close All But This")]
        [Localizable(true)]
        public string TextCloseAllButThis
        {
            get { return _textCloseAllButThis; }

            set
            {
                if (_textCloseAllButThis != value)
                {
                    _textCloseAllButThis = value;
                    OnPropertyChanged("TextCloseAllButThis");
                }
            }
        }

        /// <summary>
        /// Resets the TextCloseAllButThis property to its default value.
        /// </summary>
        public void ResetTextCloseAllButThis()
        {
            TextCloseAllButThis = _defaultTextCloseAllButThis;
        }
        #endregion

        #region TextDock
        /// <summary>
        /// Gets and sets the text to use for the dock menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the dock menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Dock")]
        [Localizable(true)]
        public string TextDock
        {
            get { return _textDock; }

            set
            {
                if (_textDock != value)
                {
                    _textDock = value;
                    OnPropertyChanged("TextDock");
                }
            }
        }

        /// <summary>
        /// Resets the TextDock property to its default value.
        /// </summary>
        public void ResetTextDock()
        {
            TextDock = _defaultTextDock;
        }
        #endregion

        #region TextFloat
        /// <summary>
        /// Gets and sets the text to use for the float menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the float menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Float")]
        [Localizable(true)]
        public string TextFloat
        {
            get { return _textFloat; }

            set
            {
                if (_textFloat != value)
                {
                    _textFloat = value;
                    OnPropertyChanged("TextFloat");
                }
            }
        }

        /// <summary>
        /// Resets the TextFloat property to its default value.
        /// </summary>
        public void ResetTextFloat()
        {
            TextFloat = _defaultTextDock;
        }
        #endregion

        #region TextHide
        /// <summary>
        /// Gets and sets the text to use for the hide menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the hide menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Hide")]
        [Localizable(true)]
        public string TextHide
        {
            get { return _textHide; }

            set
            {
                if (_textHide != value)
                {
                    _textHide = value;
                    OnPropertyChanged("TextHide");
                }
            }
        }

        /// <summary>
        /// Resets the TextHide property to its default value.
        /// </summary>
        public void ResetTextHide()
        {
            TextHide = _defaultTextDock;
        }
        #endregion

        #region TextTabbedDocument
        /// <summary>
        /// Gets and sets the text to use for the tabbed document menu item.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the tabbed document menu item.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Tabbed Document")]
        [Localizable(true)]
        public string TextTabbedDocument
        {
            get { return _textTabbedDocument; }

            set
            {
                if (_textTabbedDocument != value)
                {
                    _textTabbedDocument = value;
                    OnPropertyChanged("TextTabbedDocument");
                }
            }
        }

        /// <summary>
        /// Resets the TextTabbedDocument property to its default value.
        /// </summary>
        public void ResetTextTabbedDocument()
        {
            TextTabbedDocument = _defaultTextTabbedDocument;
        }
        #endregion

        #region TextWindowLocation
        /// <summary>
        /// Gets and sets the text to use for the drop down button tooltip.
        /// </summary>
        [Category("Visuals")]
        [Description("Text to use for the drop down button tooltip.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Window Position")]
        [Localizable(true)]
        public string TextWindowLocation
        {
            get { return _textWindowLocation; }
            
            set
            {
                if (_textWindowLocation != value)
                {
                    _textWindowLocation = value;
                    OnPropertyChanged("TextWindowLocation");
                }
            }
        }

        /// <summary>
        /// Resets the TextWindowLocation property to its default value.
        /// </summary>
        public void ResetTextWindowLocation()
        {
            TextWindowLocation = _defaultTextWindowLocation;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property that has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
