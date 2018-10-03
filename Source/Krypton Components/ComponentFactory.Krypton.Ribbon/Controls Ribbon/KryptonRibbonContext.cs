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
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Represents a single context definition.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonContext), "ToolboxBitmaps.KryptonRibbonContext.bmp")]
    [DefaultProperty("ContextName")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonRibbonContext : Component
    {
        #region Instance Fields
        private string _contextName;
        private string _contextTitle;
        private Color _contextColor;
        private object _tag;
        #endregion

        #region Events
        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonContext class.
        /// </summary>
        public KryptonRibbonContext()
        {
            // Default fields
            _contextName = "Context";
            _contextTitle = "Context Tools";
            _contextColor = Color.Red;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the unique name of the context.
        /// </summary>
        [Category("Appearance")]
        [Description("Unique name of the context.")]
        [DefaultValue("Context")]
        public string ContextName
        {
            get { return _contextName; }
            
            set 
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                    value = "Context";

                if (value != _contextName)
                {
                    _contextName = value;
                    OnPropertyChanged("ContextName");
                }
            }
        }

        /// <summary>
        /// Gets and sets the display title for associated contextual tabs.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Display title for associated contextual tabs.")]
        [DefaultValue("Context")]
        public string ContextTitle
        {
            get { return _contextTitle; }

            set
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                    value = "Context Tools";

                if (value != _contextTitle)
                {
                    _contextTitle = value;
                    OnPropertyChanged("ContextTitle");
                }
            }
        }

        /// <summary>
        /// Gets and sets the display color for associated contextual tabs.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Display color for associated contextual tabs.")]
        [DefaultValue(typeof(Color), "Red")]
        public Color ContextColor
        {
            get { return _contextColor; }

            set
            {
                // We never allow a null or transparent color
                if ((value == null) || (value == Color.Transparent))
                    value = Color.Red;

                if (value != _contextColor)
                {
                    _contextColor = value;
                    OnPropertyChanged("ContextColor");
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

            set
            {
                if (value != _tag)
                {
                    _tag = value;
                    OnPropertyChanged("Tag");
                }
            }
        }

        private bool ShouldSerializeTag()
        {
            return (Tag != null);
        }

        private void ResetTag()
        {
            Tag = null;
        }
        #endregion

        #region Protected
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
    }
}
