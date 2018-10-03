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
    /// Specifition for a button.
    /// </summary>
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    [ToolboxBitmap(typeof(ButtonSpec), "ToolboxBitmaps.KryptonButtonSpec.bmp")]
    [DefaultEvent("Click")]
    [DefaultProperty("Style")]
    public abstract class ButtonSpec : Component,
                                       IButtonSpecValues,
                                       ICloneable
    {
        #region Instance Fields
        private Image _image;
        private Image _toolTipImage;
        private Color _colorMap;
        private Color _imageTransparentColor;
        private Color _toolTipImageTransparentColor;
        private object _owner;
        private object _tag;
        private string _text;
        private string _extraText;
        private string _uniqueName;
        private string _toolTipTitle;
        private string _toolTipBody;
        private bool _allowInheritImage;
        private bool _allowInheritText;
        private bool _allowInheritExtraText;
        private bool _allowInheritToolTipTitle;
        private ViewBase _buttonSpecView;
        private LabelStyle _toolTipStyle;
        private KryptonCommand _command;
        private PaletteButtonStyle _style;
        private PaletteButtonOrientation _orientation;
        private PaletteButtonSpecStyle _type;
        private PaletteRelativeEdgeAlign _edge;
        private CheckButtonImageStates _imageStates;
        private ContextMenuStrip _contextMenuStrip;
        private KryptonContextMenu _kryptonContextMenu;
        #endregion
        
        #region Events
        /// <summary>
        /// Occurs whenever a button specification property has changed.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the component is clicked.")]
        public event EventHandler Click;

        /// <summary>
        /// Occurs whenever a button specification property has changed.
        /// </summary>
        [Category("ButtonSpec")]
        [Description("Occurs when a button specification property has changed.")]
        public event PropertyChangedEventHandler ButtonSpecPropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpec class.
		/// </summary>
        public ButtonSpec()
		{
            _image = null;
            _toolTipImage = null;
            _colorMap = Color.Empty;
            _imageTransparentColor = Color.Empty;
            _toolTipImageTransparentColor = Color.Empty;
            _text = string.Empty;
            _extraText = string.Empty;
            _uniqueName = CommonHelper.UniqueString;
            _toolTipTitle = string.Empty;
            _toolTipBody = string.Empty;
            _allowInheritImage = true;
            _allowInheritText = true;
            _allowInheritExtraText = true;
            _allowInheritToolTipTitle = true;
            _toolTipStyle = LabelStyle.ToolTip;
            _style = PaletteButtonStyle.Inherit;
            _orientation = PaletteButtonOrientation.Inherit;
            _type = PaletteButtonSpecStyle.Generic;
            _edge = PaletteRelativeEdgeAlign.Inherit;
            _imageStates = new CheckButtonImageStates();
            _imageStates.NeedPaint = new NeedPaintHandler(OnImageStateChanged);
            _contextMenuStrip = null;
            _kryptonContextMenu = null;
            _buttonSpecView = null;
        }

		/// <summary>
		/// Returns a string that represents the current defaulted state.
		/// </summary>
		/// <returns>A string that represents the current defaulted state.</returns>
		public override string ToString()
		{
			if (!IsDefault)
				return "Modified";

			return string.Empty;
		}

        /// <summary>
        /// Make a clone of this object.
        /// </summary>
        /// <returns>New instance.</returns>
        public virtual object Clone()
        {
            ButtonSpec clone = (ButtonSpec)Activator.CreateInstance(base.GetType());
            clone.Image = Image;
            clone.ImageTransparentColor = ImageTransparentColor;
            clone.Text = Text;
            clone.ExtraText = ExtraText;
            clone.ToolTipImage = ToolTipImage;
            clone.ToolTipImageTransparentColor = ToolTipImageTransparentColor;
            clone.ToolTipTitle = ToolTipTitle;
            clone.ToolTipBody = ToolTipBody;
            clone.ToolTipStyle = ToolTipStyle;
            clone.UniqueName = UniqueName;
            clone.AllowInheritImage = AllowInheritImage;
            clone.AllowInheritText = AllowInheritText;
            clone.AllowInheritExtraText = AllowInheritExtraText;
            clone.AllowInheritToolTipTitle = AllowInheritToolTipTitle;
            clone.ColorMap = ColorMap;
            clone.Style = Style;
            clone.Orientation = Orientation;
            clone.Edge = Edge;
            clone.ContextMenuStrip = ContextMenuStrip;
            clone.KryptonContextMenu = KryptonContextMenu;
            clone.KryptonCommand = KryptonCommand;
            clone.Owner = Owner;
            clone.Tag = Tag;
            return clone;
        }
        #endregion

        #region IsDefault
        /// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public virtual bool IsDefault
		{
			get
			{
                return (_imageStates.IsDefault &&
                        (Image == null) &&
                        (ToolTipImage == null) &&
                        (ColorMap == Color.Empty) &&
                        (ImageTransparentColor == Color.Empty) &&
                        (ToolTipImageTransparentColor == Color.Empty) &&
                        (Text == string.Empty) &&
						(ExtraText == string.Empty) &&
                        (ToolTipTitle == string.Empty) &&
                        (ToolTipBody == string.Empty) &&
                        (ToolTipStyle == LabelStyle.ToolTip) &&
                        (Style == PaletteButtonStyle.Inherit) &&
                        (Orientation == PaletteButtonOrientation.Inherit) &&
                        (Edge == PaletteRelativeEdgeAlign.Inherit) &&
                        (ContextMenuStrip == null) &&
                        (AllowInheritImage == true) &&
                        (AllowInheritText == true) &&
                        (AllowInheritExtraText == true) &&
                        (AllowInheritToolTipTitle == true));
			}
		}
		#endregion

        #region Image
        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Button image.")]
        public Image Image
        {
            get { return _image; }

            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnButtonSpecPropertyChanged("Image");
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

        #region ImageTransparentColor
        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Button image transparent color.")]
        [KryptonDefaultColorAttribute()]
        public Color ImageTransparentColor
        {
            get { return _imageTransparentColor; }

            set
            {
                if (_imageTransparentColor != value)
                {
                    _imageTransparentColor = value;
                    OnButtonSpecPropertyChanged("ImageTransparentColor");
                }
            }
        }

        private bool ShouldSerializeImageTransparentColor()
        {
            return ImageTransparentColor != Color.Empty;
        }

        /// <summary>
        /// Resets the ImageTransparentColor property to its default value.
        /// </summary>
        public void ResetImageTransparentColor()
        {
            ImageTransparentColor = Color.Empty;
        }
        #endregion

        #region ImageStates
        /// <summary>
        /// Gets access to the state specific images for the button.
        /// </summary>
        [Category("Appearance")]
        [Description("State specific images for the button.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ButtonImageStates ImageStates
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
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Button text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Text
        {
            get { return _text; }

            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnButtonSpecPropertyChanged("Text");
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
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Button extra text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ExtraText
        {
            get { return _extraText; }

            set
            {
                if (_extraText != value)
                {
                    _extraText = value;
                    OnButtonSpecPropertyChanged("ExtraText");
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

        #region ToolTipImage
        /// <summary>
        /// Gets and sets the button tooltip image.
        /// </summary>
        [Localizable(true)]
        [Category("ToolTip")]
        [Description("Button tooltip image.")]
        [DefaultValue(null)]
        public Image ToolTipImage
        {
            get { return _toolTipImage; }

            set
            {
                if (_toolTipImage != value)
                {
                    _toolTipImage = value;
                    OnButtonSpecPropertyChanged("ToolTipImage");
                }
            }
        }

        private bool ShouldSerializeToolTipImage()
        {
            return ToolTipImage != null;
        }

        /// <summary>
        /// Resets the ToolTipImage property to its default value.
        /// </summary>
        public void ResetToolTipImage()
        {
            ToolTipImage = null;
        }
        #endregion

        #region ToolTipImageTransparentColor
        /// <summary>
        /// Gets and sets the tooltip image transparent color.
        /// </summary>
        [Localizable(true)]
        [Category("ToolTip")]
        [Description("Button image transparent color.")]
        [KryptonDefaultColorAttribute()]
        public Color ToolTipImageTransparentColor
        {
            get { return _toolTipImageTransparentColor; }

            set
            {
                if (_toolTipImageTransparentColor != value)
                {
                    _toolTipImageTransparentColor = value;
                    OnButtonSpecPropertyChanged("ToolTipImageTransparentColor");
                }
            }
        }

        private bool ShouldSerializeToolTipImageTransparentColor()
        {
            return ToolTipImageTransparentColor != Color.Empty;
        }

        /// <summary>
        /// Resets the ToolTipImageTransparentColor property to its default value.
        /// </summary>
        public void ResetToolTipImageTransparentColor()
        {
            ToolTipImageTransparentColor = Color.Empty;
        }
        #endregion

        #region ToolTipTitle
        /// <summary>
        /// Gets and sets the button title tooltip text.
        /// </summary>
        [Localizable(true)]
        [Category("ToolTip")]
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
                    OnButtonSpecPropertyChanged("ToolTipTitle");
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

        #region ToolTipBody
        /// <summary>
        /// Gets and sets the button body tooltip text.
        /// </summary>
        [Localizable(true)]
        [Category("ToolTip")]
        [Description("Button tooltip body text.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        public string ToolTipBody
        {
            get { return _toolTipBody; }

            set
            {
                if (_toolTipBody != value)
                {
                    _toolTipBody = value;
                    OnButtonSpecPropertyChanged("ToolTipBody");
                }
            }
        }

        private bool ShouldSerializeToolTipBody()
        {
            return ToolTipBody != string.Empty;
        }

        /// <summary>
        /// Resets the ToolTipBody property to its default value.
        /// </summary>
        public void ResetToolTipBody()
        {
            ToolTipBody = string.Empty;
        }
        #endregion

        #region ToolTipStyle
        /// <summary>
        /// Gets and sets the tooltip label style.
        /// </summary>
        [Category("ToolTip")]
        [Description("Button tooltip label style.")]
        [DefaultValue(typeof(LabelStyle), "Tooltip")]
        public LabelStyle ToolTipStyle
        {
            get { return _toolTipStyle; }
            set { _toolTipStyle = value; }
        }

        private bool ShouldSerializeToolTipStyle()
        {
            return ToolTipStyle != LabelStyle.ToolTip;
        }

        /// <summary>
        /// Resets the ToolTipStyle property to its default value.
        /// </summary>
        public void ResetToolTipStyle()
        {
            ToolTipStyle = LabelStyle.ToolTip;
        }
        #endregion

        #region UniqueName
        /// <summary>
        /// Gets and sets the unique name of the ButtonSpec.
        /// </summary>
        [Category("Data")]
        [Description("The unique name of the ButtonSpec.")]
        public string UniqueName
        {
            get { return _uniqueName; }
            set { _uniqueName = value;}
        }

        /// <summary>
        /// Resets the UniqueName property to its default value.
        /// </summary>
        public void ResetUniqueName()
        {
            _uniqueName = CommonHelper.UniqueString;
        }
        #endregion

        #region AllowInheritImage
        /// <summary>
        /// Gets and sets if the button image be inherited if defined as null.
        /// </summary>
        [Localizable(true)]
        [Category("Inherit")]
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
                    OnButtonSpecPropertyChanged("Image");
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
        [Localizable(true)]
        [Category("Inherit")]
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
                    OnButtonSpecPropertyChanged("Text");
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
        [Localizable(true)]
        [Category("Inherit")]
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
                    OnButtonSpecPropertyChanged("ExtraText");
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
        /// Gets and sets if the button tooltip title be inherited if defined as empty.
        /// </summary>
        [Localizable(true)]
        [Category("Inherit")]
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
                    OnButtonSpecPropertyChanged("ToolTipTitle");
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

        #region AllowComponent
        /// <summary>
        /// Gets a value indicating if the component is allowed to be selected at design time.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool AllowComponent
        {
            get { return true; }
        }
        #endregion

        #region ColorMap
        /// <summary>
        /// Gets and sets image color to remap to container foreground.
        /// </summary>
        [Localizable(true)]
        [Category("Appearance")]
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
                    OnButtonSpecPropertyChanged("ColorMap");
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

        #region Style
        /// <summary>
        /// Gets and sets the button style.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Button style.")]
        [DefaultValue(typeof(PaletteButtonStyle), "Inherit")]
        public PaletteButtonStyle Style
        {
            get { return _style; }

            set
            {
                if (_style != value)
                {
                    _style = value;
                    OnButtonSpecPropertyChanged("Style");
                }
            }
        }

        private bool ShouldSerializeStyle()
        {
            return (Style != PaletteButtonStyle.Inherit);
        }

        /// <summary>
        /// Resets the Style property to its default value.
        /// </summary>
        public void ResetStyle()
        {
            Style = PaletteButtonStyle.Inherit;
        }
        #endregion

        #region Orientation
        /// <summary>
        /// Gets and sets the button orientation.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Defines the button orientation.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteButtonOrientation Orientation
        {
            get { return _orientation; }

            set
            {
                if (_orientation != value)
                {
                    _orientation = value;
                    OnButtonSpecPropertyChanged("Orientation");
                }
            }
        }

        private bool ShouldSerializeOrientation()
        {
            return (Orientation != PaletteButtonOrientation.Inherit);
        }

        /// <summary>
        /// Resets the Orientation property to its default value.
        /// </summary>
        public void ResetOrientation()
        {
            Orientation = PaletteButtonOrientation.Inherit;
        }
        #endregion

        #region Edge
        /// <summary>
        /// Gets and sets the header edge to display the button against.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("The header edge to display the button against.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public PaletteRelativeEdgeAlign Edge
        {
            get { return _edge; }

            set
            {
                if (_edge != value)
                {
                    _edge = value;
                    OnButtonSpecPropertyChanged("Edge");
                }
            }
        }

        private bool ShouldSerializeEdge()
        {
            return (Edge != PaletteRelativeEdgeAlign.Inherit);
        }

        private void ResetEdge()
        {
            Edge = PaletteRelativeEdgeAlign.Inherit;
        }
        #endregion

        #region ContextMenuStrip
        /// <summary>
        /// Gets and sets the context menu strip for the button.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("ContextMenuStrip to show when the button is pressed.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(null)]
        public ContextMenuStrip ContextMenuStrip
        {
            get { return _contextMenuStrip; }
            set { _contextMenuStrip = value; }
        }
        #endregion

        #region KryptonContextMenu
        /// <summary>
        /// Gets and sets the krypton context menu for the button.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("KryptonContextMenu to show when the button is pressed.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(null)]
        public KryptonContextMenu KryptonContextMenu
        {
            get { return _kryptonContextMenu; }
            set { _kryptonContextMenu = value; }
        }
        #endregion

        #region KryptonCommand
        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the button.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue(null)]
        public virtual KryptonCommand KryptonCommand
        {
            get { return _command; }

            set
            {
                if (_command != value)
                {
                    if (_command != null)
                        _command.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);

                    _command = value;
                    OnButtonSpecPropertyChanged("KryptonCommand");

                    if (_command != null)
                        _command.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);
                }
            }
        }
        #endregion

        #region Owner
        /// <summary>
        /// Gets and sets user-defined data associated with the object.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Owner
        {
            get { return _owner; }
            set { _owner = value; }
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
            set { _tag = value;  }
        }
        #endregion

        #region CopyFrom
        /// <summary>
        /// Value copy from the provided source to ourself.
        /// </summary>
        /// <param name="source">Source instance.</param>
        public virtual void CopyFrom(ButtonSpec source)
        {
            // Copy class specific values
            Image = source.Image;
            ImageTransparentColor = source.ImageTransparentColor;
            ImageStates.CopyFrom(source.ImageStates);
            Text = source.Text;
            ExtraText = source.ExtraText;
            AllowInheritImage = source.AllowInheritImage;
            AllowInheritText = source.AllowInheritText;
            AllowInheritExtraText = source.AllowInheritExtraText;
            ColorMap = source.ColorMap;
            Style = source.Style;
            Orientation = source.Orientation;
            Edge = source.Edge;
            ProtectedType = source.ProtectedType;
        }
        #endregion

        #region PerformClick
        /// <summary>
        /// Generates a Click event for the control.
        /// </summary>
        public void PerformClick()
        {
            PerformClick(EventArgs.Empty);
        }

        /// <summary>
        /// Generates a Click event for the control.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        public void PerformClick(EventArgs e)
        {
            OnClick(e);
        }
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button image.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <param name="state">State for which an image is needed.</param>
        /// <returns>Button image.</returns>
        public virtual Image GetImage(IPalette palette, PaletteState state)
        {
            Image image = null;

            // Prefer to get image from the command first
            if (KryptonCommand != null)
                return KryptonCommand.ImageSmall;

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
            else
                return palette.GetButtonSpecImage(_type, state);
        }

        /// <summary>
        /// Gets the image transparent color.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetImageTransparentColor(IPalette palette)
        {
            if (KryptonCommand != null)
                return KryptonCommand.ImageTransparentColor;
            else if (ImageTransparentColor != Color.Empty)
                return ImageTransparentColor;
            else
                return palette.GetButtonSpecImageTransparentColor(_type);
        }

        /// <summary>
        /// Gets the button short text.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Short text string.</returns>
        public virtual string GetShortText(IPalette palette)
        {
            if (KryptonCommand != null)
                return KryptonCommand.Text;
            else if ((Text.Length > 0) || !AllowInheritText)
                return Text;
            else
                return palette.GetButtonSpecShortText(_type);
        }

        /// <summary>
        /// Gets the button long text.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Long text string.</returns>
        public virtual string GetLongText(IPalette palette)
        {
            if (KryptonCommand != null)
                return KryptonCommand.ExtraText;
            if ((ExtraText.Length > 0) || !AllowInheritExtraText)
                return ExtraText;
            else
                return palette.GetButtonSpecLongText(_type);
        }

        /// <summary>
        /// Gets the button tooltip title text.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Tooltip title string.</returns>
        public virtual string GetToolTipTitle(IPalette palette)
        {
            if (!string.IsNullOrEmpty(ToolTipTitle) || !AllowInheritToolTipTitle)
                return ToolTipTitle;
            else
                return palette.GetButtonSpecToolTipTitle(_type);
        }

        /// <summary>
        /// Gets the color to remap from the image to the container foreground.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetColorMap(IPalette palette)
        {
            if (ColorMap != Color.Empty)
                return ColorMap;
            else
                return palette.GetButtonSpecColorMap(_type);
        }

        /// <summary>
        /// Gets the button style.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button style.</returns>
        public virtual ButtonStyle GetStyle(IPalette palette)
        {
            if (Style != PaletteButtonStyle.Inherit)
                return ConvertToButtonStyle(Style);
            else
                return ConvertToButtonStyle(palette.GetButtonSpecStyle(_type));
        }

        /// <summary>
        /// Gets the button orienation.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button orientation.</returns>
        public virtual ButtonOrientation GetOrientation(IPalette palette)
        {
            if (Orientation != PaletteButtonOrientation.Inherit)
                return ConvertToButtonOrientation(Orientation);
            else
                return ConvertToButtonOrientation(palette.GetButtonSpecOrientation(_type));
        }

        /// <summary>
        /// Gets the edge for the button.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button edge.</returns>
        public virtual RelativeEdgeAlign GetEdge(IPalette palette)
        {
            if (Edge != PaletteRelativeEdgeAlign.Inherit)
                return ConvertToRelativeEdgeAlign(Edge);
            else
                return ConvertToRelativeEdgeAlign(palette.GetButtonSpecEdge(_type));
        }

        /// <summary>
        /// Gets the button location.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button location.</returns>
        public virtual HeaderLocation GetLocation(IPalette palette)
        {
            return HeaderLocation.PrimaryHeader;
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public abstract ButtonEnabled GetEnabled(IPalette palette);

        /// <summary>
        /// Sets the current view associated with the button spec.
        /// </summary>
        /// <param name="view">View element reference.</param>
        public virtual void SetView(ViewBase view)
        {
            _buttonSpecView = view;
        }

        /// <summary>
        /// Get the current view associated with the button spec.
        /// </summary>
        /// <returns>View element reference.</returns>
        public virtual ViewBase GetView()
        {
            return _buttonSpecView;
        }

        /// <summary>
        /// Gets a value indicating if the associated view is enabled.
        /// </summary>
        /// <returns>True if enabled; otherwise false.</returns>
        public bool GetViewEnabled()
        {
            if (_buttonSpecView == null)
                return false;
            else
                return (_buttonSpecView.State != PaletteState.Disabled);
        }

        /// <summary>
        /// Gets the button visible value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibiliy.</returns>
        public abstract bool GetVisible(IPalette palette);

        /// <summary>
        /// Gets the button checked state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button checked state.</returns>
        public abstract ButtonCheckState GetChecked(IPalette palette);
        #endregion

        #region Protected
        /// <summary>
        /// Generates the Click event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected void GenerateClick(EventArgs e)
        {
            if (Click != null)
                Click(this, e);

            // If we have an attached command then execute it
            if (KryptonCommand != null)
                KryptonCommand.PerformExecute();
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnClick(EventArgs e)
        {
            // Only if associated view is enabled do we perform the click
            if (GetViewEnabled())
            {
                if (Click != null)
                    Click(this, e);

                // If we have an attached command then execute it
                if (KryptonCommand != null)
                    KryptonCommand.PerformExecute();
            }
        }

        /// <summary>
        /// Raises the ButtonSpecPropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the appearance property that has changed.</param>
        protected virtual void OnButtonSpecPropertyChanged(string propertyName)
        {
            if (ButtonSpecPropertyChanged != null)
                ButtonSpecPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Handles a change in the property of an attached command.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
        protected virtual void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Text":
                    OnButtonSpecPropertyChanged("Text");
                    break;
                case "ExtraText":
                    OnButtonSpecPropertyChanged("ExtraText");
                    break;
                case "ImageSmall":
                    OnButtonSpecPropertyChanged("Image");
                    break;
                case "ImageTransparentColor":
                    OnButtonSpecPropertyChanged("ImageTransparentColor");
                    break;
            }
        }

        /// <summary>
        /// Gets and sets the actual type of the button.
        /// </summary>
        protected PaletteButtonSpecStyle ProtectedType
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Convert from palette specific edge alignment to resolved edge alignment.
        /// </summary>
        /// <param name="paletteRelativeEdgeAlign">Palette specific edge alignment.</param>
        /// <returns>Resolved edge alignment.</returns>
        protected RelativeEdgeAlign ConvertToRelativeEdgeAlign(PaletteRelativeEdgeAlign paletteRelativeEdgeAlign)
        {
            switch (paletteRelativeEdgeAlign)
            {
                case PaletteRelativeEdgeAlign.Near:
                    return RelativeEdgeAlign.Near;
                case PaletteRelativeEdgeAlign.Far:
                    return RelativeEdgeAlign.Far;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return RelativeEdgeAlign.Far;
            }
        }

        /// <summary>
        /// Convert from palette specific button orientation to resolved button orientation.
        /// </summary>
        /// <param name="paletteButtonOrientation">Palette specific button orientation.</param>
        /// <returns>Resolved button orientation.</returns>
        protected ButtonOrientation ConvertToButtonOrientation(PaletteButtonOrientation paletteButtonOrientation)
        {
            switch (paletteButtonOrientation)
            {
                case PaletteButtonOrientation.Auto:
                    return ButtonOrientation.Auto;
                case PaletteButtonOrientation.FixedBottom:
                    return ButtonOrientation.FixedBottom;
                case PaletteButtonOrientation.FixedLeft:
                    return ButtonOrientation.FixedLeft;
                case PaletteButtonOrientation.FixedRight:
                    return ButtonOrientation.FixedRight;
                case PaletteButtonOrientation.FixedTop:
                    return ButtonOrientation.FixedTop;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return ButtonOrientation.Auto;
            }
        }

        /// <summary>
        /// Convert from palette specific button style to resolved button style.
        /// </summary>
        /// <param name="paletteButtonStyle">Palette specific button style.</param>
        /// <returns>Resolve button style.</returns>
        protected ButtonStyle ConvertToButtonStyle(PaletteButtonStyle paletteButtonStyle)
        {
            switch (paletteButtonStyle)
            {
                case PaletteButtonStyle.Standalone:
                    return ButtonStyle.Standalone;
                case PaletteButtonStyle.Alternate:
                    return ButtonStyle.Alternate;
                case PaletteButtonStyle.LowProfile:
                    return ButtonStyle.LowProfile;
                case PaletteButtonStyle.ButtonSpec:
                    return ButtonStyle.ButtonSpec;
                case PaletteButtonStyle.BreadCrumb:
                    return ButtonStyle.BreadCrumb;
                case PaletteButtonStyle.Cluster:
                    return ButtonStyle.Cluster;
                case PaletteButtonStyle.NavigatorStack:
                    return ButtonStyle.NavigatorStack;
                case PaletteButtonStyle.NavigatorOverflow:
                    return ButtonStyle.NavigatorOverflow;
                case PaletteButtonStyle.NavigatorMini:
                    return ButtonStyle.NavigatorMini;
                case PaletteButtonStyle.InputControl:
                    return ButtonStyle.InputControl;
                case PaletteButtonStyle.ListItem:
                    return ButtonStyle.ListItem;
                case PaletteButtonStyle.Form:
                    return ButtonStyle.Form;
                case PaletteButtonStyle.FormClose:
                    return ButtonStyle.FormClose;
                case PaletteButtonStyle.Command:
                    return ButtonStyle.Command;
                case PaletteButtonStyle.Custom1:
                    return ButtonStyle.Custom1;
                case PaletteButtonStyle.Custom2:
                    return ButtonStyle.Custom2;
                case PaletteButtonStyle.Custom3:
                    return ButtonStyle.Custom3;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return ButtonStyle.Standalone;
            }
        }
        #endregion

        #region Implementation
        private void OnImageStateChanged(object sender, NeedLayoutEventArgs e)
        {
            OnButtonSpecPropertyChanged("Image");
        }
        #endregion
    }
}
