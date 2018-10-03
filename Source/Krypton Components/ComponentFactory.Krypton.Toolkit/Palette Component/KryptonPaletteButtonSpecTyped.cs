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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Overrides for defining typed button specifications.
    /// </summary>
    public class KryptonPaletteButtonSpecTyped : KryptonPaletteButtonSpecBase
    {
        #region Instance Fields
        private Image _image;
        private string _text;
        private string _extraText;
        private string _toolTipTitle;
        private Color _colorMap;
        private bool _allowInheritImage;
        private bool _allowInheritText;
        private bool _allowInheritExtraText;
        private bool _allowInheritToolTipTitle;
        private CheckButtonImageStates _imageStates;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteButtonSpecCommon class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        internal KryptonPaletteButtonSpecTyped(PaletteRedirect redirector)
            : base(redirector)
        {
            _image = null;
            _text = string.Empty;
            _extraText = string.Empty;
            _toolTipTitle = string.Empty;
            _colorMap = Color.Empty;
            _allowInheritImage = true;
            _allowInheritText = true;
            _allowInheritExtraText = true;
            _allowInheritToolTipTitle = true;
            _imageStates = new CheckButtonImageStates();
            _imageStates.NeedPaint = new NeedPaintHandler(OnImageStateChanged);
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
                return (base.IsDefault &&
                        _imageStates.IsDefault&&
                        (Image == null) &&
                        (Text == string.Empty) &&
                        (ExtraText == string.Empty) &&
                        (ToolTipTitle == string.Empty) &&
                        (ColorMap == Color.Empty) &&
                        (AllowInheritImage == true) &&
                        (AllowInheritText == true) &&
                        (AllowInheritExtraText == true) &&
                        (AllowInheritToolTipTitle == true));
            }
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="style">The style of the button spec instance.</param>
        public override void PopulateFromBase(PaletteButtonSpecStyle style)
        {
            base.PopulateFromBase(style);
            Image = Redirector.GetButtonSpecImage(style, PaletteState.Normal);
            ImageStates.ImageDisabled = Redirector.GetButtonSpecImage(style, PaletteState.Disabled);
            ImageStates.ImageNormal = Redirector.GetButtonSpecImage(style, PaletteState.Normal);
            ImageStates.ImageTracking = Redirector.GetButtonSpecImage(style, PaletteState.Tracking);
            ImageStates.ImagePressed = Redirector.GetButtonSpecImage(style, PaletteState.Pressed);
            ImageStates.ImageCheckedNormal = Redirector.GetButtonSpecImage(style, PaletteState.CheckedNormal);
            ImageStates.ImageCheckedTracking = Redirector.GetButtonSpecImage(style, PaletteState.CheckedTracking);
            ImageStates.ImageCheckedPressed = Redirector.GetButtonSpecImage(style, PaletteState.CheckedPressed);
            Text = Redirector.GetButtonSpecShortText(style);
            ExtraText = Redirector.GetButtonSpecLongText(style);
            ColorMap = Redirector.GetButtonSpecColorMap(style);
        }
        #endregion

        #region Image
        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button image.")]
        [DefaultValue(null)]
        public Image Image
        {
            get { return _image; }

            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeImage()
        {
            return Image != null;
        }

        /// <summary>
        /// Resets the Image property to its default value.
        /// </summary>
        public void ResetImage()
        {
            Image = null;
        }
        #endregion

        #region ImageStates
        /// <summary>
        /// Gets access to the state specific images for the button.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("State specific images for the button.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CheckButtonImageStates ImageStates
        {
            get { return _imageStates; }
        }

        private bool ShouldSerializeImageStates()
        {
            return !_imageStates.IsDefault;
        }
        #endregion

        #region Text
        /// <summary>
        /// Gets and sets the button text.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string Text
        {
            get { return _text; }

            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeText()
        {
            return Text != string.Empty;
        }

        /// <summary>
        /// Resets the Text property to its default value.
        /// </summary>
        public void ResetText()
        {
            Text = string.Empty;
        }
        #endregion

        #region ExtraText
        /// <summary>
        /// Gets and sets the button extra text.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button extra text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string ExtraText
        {
            get { return _extraText; }

            set
            {
                if (_extraText != value)
                {
                    _extraText = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeExtraText()
        {
            return ExtraText != string.Empty;
        }

        /// <summary>
        /// Resets the ExtraText property to its default value.
        /// </summary>
        public void ResetExtraText()
        {
            ExtraText = string.Empty;
        }
        #endregion

        #region ToolTipTitle
        /// <summary>
        /// Gets and sets the button tooltip title text.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Button tooltip title text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string ToolTipTitle
        {
            get { return _toolTipTitle; }

            set
            {
                if (_toolTipTitle != value)
                {
                    _toolTipTitle = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeToolTipTitle()
        {
            return ToolTipTitle != string.Empty;
        }

        /// <summary>
        /// Resets the ToolTipTitle property to its default value.
        /// </summary>
        public void ResetToolTipTitle()
        {
            ToolTipTitle = string.Empty;
        }
        #endregion

        #region ColorMap
        /// <summary>
        /// Gets and sets image color to remap to container foreground.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Image color to remap to container foreground.")]
        [KryptonDefaultColorAttribute()]
        public Color ColorMap
        {
            get { return _colorMap; }

            set
            {
                if (_colorMap != value)
                {
                    _colorMap = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeColorMap()
        {
            return ColorMap != Color.Empty;
        }

        /// <summary>
        /// Resets the ColorMap property to its default value.
        /// </summary>
        public void ResetColorMap()
        {
            ColorMap = Color.Empty;
        }
        #endregion

        #region AllowInheritImage
        /// <summary>
        /// Gets and sets if the button image be inherited if defined as null.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Should button image be inherited if defined as null.")]
        [DefaultValue(true)]
        public bool AllowInheritImage
        {
            get { return _allowInheritImage; }

            set
            {
                if (_allowInheritImage != value)
                {
                    _allowInheritImage = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the AllowInheritImage property to its default value.
        /// </summary>
        public void ResetAllowInheritImage()
        {
            AllowInheritImage = true;
        }
        #endregion

        #region AllowInheritText
        /// <summary>
        /// Gets and sets if the button text be inherited if defined as empty.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Should button text be inherited if defined as empty.")]
        [DefaultValue(true)]
        public bool AllowInheritText
        {
            get { return _allowInheritText; }

            set
            {
                if (_allowInheritText != value)
                {
                    _allowInheritText = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the AllowInheritText property to its default value.
        /// </summary>
        public void ResetAllowInheritText()
        {
            AllowInheritText = true;
        }
        #endregion

        #region AllowInheritExtraText
        /// <summary>
        /// Gets and sets if the button extra text be inherited if defined as empty.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Should button extra text be inherited if defined as empty.")]
        [DefaultValue(true)]
        public bool AllowInheritExtraText
        {
            get { return _allowInheritExtraText; }

            set
            {
                if (_allowInheritExtraText != value)
                {
                    _allowInheritExtraText = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the AllowInheritExtraText property to its default value.
        /// </summary>
        public void ResetAllowInheritExtraText()
        {
            AllowInheritExtraText = true;
        }
        #endregion

        #region AllowInheritToolTipTitle
        /// <summary>
        /// Gets and sets if the button tooltip title text be inherited if defined as empty.
        /// </summary>
        [KryptonPersist(false)]
        [Localizable(true)]
        [Category("Visuals")]
        [Description("Should button tooltip title text be inherited if defined as empty.")]
        [DefaultValue(true)]
        public bool AllowInheritToolTipTitle
        {
            get { return _allowInheritToolTipTitle; }

            set
            {
                if (_allowInheritToolTipTitle != value)
                {
                    _allowInheritToolTipTitle = value;
                    OnButtonSpecChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the AllowInheritToolTipTitle property to its default value.
        /// </summary>
        public void ResetAllowInheritToolTipTitle()
        {
            AllowInheritToolTipTitle = true;
        }
        #endregion

        #region IPaletteButtonSpec
        /// <summary>
        /// Gets the image to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <param name="state">State for which image is required.</param>
        /// <returns>Image value.</returns>
        public override Image GetButtonSpecImage(PaletteButtonSpecStyle style,
                                                 PaletteState state)
        {
            Image image = null;

            // Try and recover a state specific image
            switch (state)
            {
                case PaletteState.Disabled:
                    image = ImageStates.ImageDisabled;
                    break;
                case PaletteState.Normal:
                    image = ImageStates.ImageNormal;
                    break;
                case PaletteState.Pressed:
                    image = ImageStates.ImagePressed;
                    break;
                case PaletteState.Tracking:
                    image = ImageStates.ImageTracking;
                    break;
                case PaletteState.CheckedNormal:
                    image = ImageStates.ImageCheckedNormal;
                    break;
                case PaletteState.CheckedPressed:
                    image = ImageStates.ImageCheckedPressed;
                    break;
                case PaletteState.CheckedTracking:
                    image = ImageStates.ImageCheckedTracking;
                    break;
            }

            // Default to the image if no state specific image is found
            if (image == null)
                image = Image;

            if ((image != null) || !AllowInheritImage)
                return image;

            return base.GetButtonSpecImage(style, state);
        }

        /// <summary>
        /// Gets the short text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public override string GetButtonSpecShortText(PaletteButtonSpecStyle style)
        {
            if ((Text.Length > 0) || !AllowInheritText)
                return Text;
            else
                return base.GetButtonSpecShortText(style);
        }

        /// <summary>
        /// Gets the long text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public override string GetButtonSpecLongText(PaletteButtonSpecStyle style)
        {
            if ((ExtraText.Length > 0) || !AllowInheritExtraText)
                return ExtraText;
            else
                return base.GetButtonSpecLongText(style);
        }

        /// <summary>
        /// Gets the tooltip title text to display for the button.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>String value.</returns>
        public override string GetButtonSpecToolTipTitle(PaletteButtonSpecStyle style)
        {
            if ((ToolTipTitle.Length > 0) || !AllowInheritToolTipTitle)
                return ToolTipTitle;
            else
                return base.GetButtonSpecToolTipTitle(style);
        }

        /// <summary>
        /// Gets the color to remap from the image to the container foreground.
        /// </summary>
        /// <param name="style">Style of button spec.</param>
        /// <returns>Color value.</returns>
        public override Color GetButtonSpecColorMap(PaletteButtonSpecStyle style)
        {
            if (ColorMap != Color.Empty)
                return ColorMap;
            else
                return base.GetButtonSpecColorMap(style);
        }
        #endregion

        #region Implementation
        private void OnImageStateChanged(object sender, NeedLayoutEventArgs e)
        {
            OnButtonSpecChanged(sender, EventArgs.Empty);
        }
        #endregion
    }
}
