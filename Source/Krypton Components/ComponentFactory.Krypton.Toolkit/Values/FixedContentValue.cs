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
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Stores a text/extraText/Image triple of values as a content values source.
    /// </summary>
    public class FixedContentValue : IContentValues
    {
        #region Instance Fields
        private string _shortText;
        private string _longText;
        private Image _image;
        private Color _imageTransparentColor;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the FixedContentValue class.
        /// </summary>
        public FixedContentValue()
            : this(string.Empty, string.Empty, null, Color.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the FixedContentValue class.
        /// </summary>
        /// <param name="shortText">Initial short text value.</param>
        /// <param name="longText">Initial long text value.</param>
        /// <param name="image">Initial image value.</param>
        /// <param name="imageTransparentColor">Initial image transparent color value.</param>
        public FixedContentValue(string shortText, 
                                 string longText,
                                 Image image,
                                 Color imageTransparentColor)
        {
            _shortText = shortText;
            _longText = longText;
            _image = image;
            _imageTransparentColor = imageTransparentColor;
        }
        #endregion

        #region ShortText
        /// <summary>
        /// Gets and sets the short text.
        /// </summary>
        [Category("Appearance")]
        [Description("Main text.")]
        [Localizable(true)]
        [DefaultValue("")]
        public string ShortText
        {
            get { return _shortText; }
            set { _shortText = value; }
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
        [DefaultValue("")]
        public string LongText
        {
            get { return _longText; }
            set { _longText = value; }
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
            set { _image = value; }
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
            set { _imageTransparentColor = value; }
        }

        private bool ShouldSerializeImageTransparentColor()
        {
            return (_imageTransparentColor != Color.Empty);
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
    }
}
