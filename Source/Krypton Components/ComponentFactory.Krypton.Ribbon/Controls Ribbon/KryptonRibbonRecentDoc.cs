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
    /// Represents a single recent document entry in the ribbon application button menu.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonRecentDoc), "ToolboxBitmaps.KryptonRibbonRecentDoc.png")]
    [DefaultProperty("Text")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonRibbonRecentDoc : Component
    {
        #region Instance Fields
        private Image _image;
        private Color _imageTransparentColor;
        private string _text;
        private string _extraText;
        private object _tag;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the recent document item is clicked.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the recent document item is clicked.")]
        public event EventHandler Click;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonRecentDoc class.
        /// </summary>
        public KryptonRibbonRecentDoc()
        {
            // Default fields
            _text = "Recent Document";
            _extraText = string.Empty;
            _imageTransparentColor = Color.Empty;
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the main text for the recent document entry.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Main text for the recent document entry.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Recent Document")]
        public string Text
        {
            get { return _text; }

            set
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                    value = "Recent Document";

                if (value != _text)
                    _text = value;
            }
        }

        /// <summary>
        /// Gets and sets the extra text for the recent document entry.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Extra text for the recent document entry.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("")]
        public string ExtraText
        {
            get { return _extraText; }

            set
            {
                if (value != _extraText)
                    _extraText = value;
            }
        }

        /// <summary>
        /// Gets and sets the recent document image.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Image for the recent document entry.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(null)]
        public Image Image
        {
            get { return _image; }

            set
            {
                if (_image != value)
                    _image = value;
            }
        }

        /// <summary>
        /// Gets and sets the image color to make transparent.
        /// </summary>
        [Category("Appearance")]
        [Description("Image color to make transparent.")]
        [DefaultValue(typeof(Color), "")]
        [Localizable(true)]
        [Bindable(true)]
        public Color ImageTransparentColor
        {
            get { return _imageTransparentColor; }

            set
            {
                if (value != _imageTransparentColor)
                    _imageTransparentColor = value;
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
                    _tag = value;
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
        }
        #endregion
    }
}
