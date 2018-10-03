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
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Krypton object used inside list controls for providing content values.
    /// </summary>
    [ToolboxItem(false)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KryptonListItem : Component,
                                   IContentValues
    {
        #region Instance Fields
        private string _shortText;
        private string _longText;
        private Image _image;
        private Color _imageTransparentColor;
        private object _tag;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonListItem class.
        /// </summary>
        public KryptonListItem()
            : this("ListItem", null, null, Color.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonListItem class.
        /// </summary>
        /// <param name="shortText">Initial short text value.</param>
        public KryptonListItem(string shortText)
            : this(shortText, null, null, Color.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonListItem class.
        /// </summary>
        /// <param name="shortText">Initial short text value.</param>
        /// <param name="longText">Initial long text value.</param>
        public KryptonListItem(string shortText, string longText)
            : this(shortText, longText, null, Color.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonListItem class.
        /// </summary>
        /// <param name="shortText">Initial short text value.</param>
        /// <param name="longText">Initial long text value.</param>
        /// <param name="image">Initial image value.</param>
        public KryptonListItem(string shortText,
                               string longText,
                               Image image)
            : this(shortText, longText, image, Color.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonListItem class.
        /// </summary>
        /// <param name="shortText">Initial short text value.</param>
        /// <param name="longText">Initial long text value.</param>
        /// <param name="image">Initial image value.</param>
        /// <param name="imageTransparentColor">Initial transparent image color.</param>
        public KryptonListItem(string shortText,
                               string longText,
                               Image image,
                               Color imageTransparentColor)
        {
            _shortText = shortText;
            _longText = longText;
            _image = image;
            _imageTransparentColor = imageTransparentColor;
        }

        /// <summary>
        /// Gets the string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ShortText;
        }
        #endregion

        #region ShortText
        /// <summary>
        /// Gets and sets the short text.
        /// </summary>
        [Category("Appearance")]
        [Description("Main text.")]
        [Localizable(true)]
        public string ShortText
        {
            get { return _shortText; }
            
            set 
            {
                if (_shortText != value)
                {
                    _shortText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("ShortText"));
                }
            }
        }

        private bool ShouldSerializeShortText()
        {
            return !string.IsNullOrEmpty(_shortText);
        }
        #endregion

        #region LongText
        /// <summary>
        /// Gets and sets the long text.
        /// </summary>
        [Category("Appearance")]
        [Description("Supplementary text.")]
        [Localizable(true)]
        public string LongText
        {
            get { return _longText; }

            set 
            {
                if (_longText != value)
                {
                    _longText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LongText"));
                }
            }
        }

        private bool ShouldSerializeLongText()
        {
            return !string.IsNullOrEmpty(_longText);
        }
        #endregion

        #region Image
        /// <summary>
        /// Gets and sets the image.
        /// </summary>
        [Category("Appearance")]
        [Description("Image associated with item.")]
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

        private bool ShouldSerializeImage()
        {
            return (_image != null);
        }
        #endregion

        #region ImageTransparentColor
        /// <summary>
        /// Gets and sets the image transparent color.
        /// </summary>
        [Category("Appearance")]
        [Description("Color to treat as transparent in the Image.")]
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
            return (_imageTransparentColor != Color.Empty);
        }
        #endregion

        #region Tag
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
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText()
        {
            return _shortText;
        }

        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
        {
            return _image;
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return _imageTransparentColor;
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText()
        {
            return _longText;
        }
        #endregion

        #region Protected
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
}
