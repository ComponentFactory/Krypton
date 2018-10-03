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
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Represents a single ribbon quick access toolbar entry.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonQATButton), "ToolboxBitmaps.KryptonRibbonQATButton.bmp")]
    [DefaultEvent("Click")]
    [DefaultProperty("Image")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonRibbonQATButton : Component,
                                          IQuickAccessToolbarButton
    {
        #region Static Fields
        private static readonly Image _defaultImage = Properties.Resources.QATButtonDefault;
        #endregion

        #region Instance Fields
        private object _tag;
        private Image _image;
        private Image _toolTipImage;
        private Color _toolTipImageTransparentColor;
        private LabelStyle _toolTipStyle;
        private bool _visible;
        private bool _enabled;
        private string _text;
        private string _toolTipTitle;
        private string _toolTipBody;
        private Keys _shortcutKeys;
        private KryptonCommand _command;
        private KryptonRibbon _ribbon;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the quick access toolbar button has been clicked.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonQATButton class.
        /// </summary>
        public KryptonRibbonQATButton()
        {
            // Default fields
            _image = _defaultImage;
            _visible = true;
            _enabled = true;
            _text = "QAT Button";
            _shortcutKeys = Keys.None;
            _toolTipImageTransparentColor = Color.Empty;
            _toolTipTitle = string.Empty;
            _toolTipBody = string.Empty;
            _toolTipStyle = LabelStyle.ToolTip;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the owning ribbon control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonRibbon Ribbon
        {
            get { return _ribbon; }
        }

        /// <summary>
        /// Gets and sets the application button image.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Values")]
        [Description("Application button image.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Image
        {
            get { return _image; }

            set
            {
                if (_image != value)
                {
                    if (value != null)
                    {
                        // The image must be 16x16 or less in order to be displayed on the
                        // quick access toolbar. So we reject anything bigger than 16x16.
                        if ((value.Width > 16) || (value.Height > 16))
                            throw new ArgumentOutOfRangeException("Image must be 16x16 or smaller.");
                    }

                    _image = value;
                    OnPropertyChanged("Image");

                    // Only need to update display if we are visible
                    if (Visible && (_ribbon != null))
                        _ribbon.PerformNeedPaint(false);
                }
            }
        }

        private bool ShouldSerializeImage()
        {
            return Image != _defaultImage;
        }

        /// <summary>
        /// Gets and sets the visible state of the ribbon quick access toolbar entry.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the ribbon quick access toolbar entry is visible or hidden.")]
        [DefaultValue(true)]
        public bool Visible
        {
            get { return _visible; }

            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    OnPropertyChanged("Visible");

                    // Must try and layout to show change
                    if (_ribbon != null)
                    {
                        _ribbon.PerformNeedPaint(true);
                        _ribbon.UpdateQAT();
                    }
                }
            }
        }

        /// <summary>
        /// Make the ribbon tab visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon tab hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the ribbon quick access toolbar entry.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the ribbon quick access toolbar entry is enabled.")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return _enabled; }

            set
            {
                if (value != _enabled)
                {
                    _enabled = value;
                    OnPropertyChanged("Enabled");

                    // Must try and paint to show change
                    if (Visible && (_ribbon != null))
                        _ribbon.PerformNeedPaint(false);
                }
            }
        }

        /// <summary>
        /// Gets and sets the display text of the quick access toolbar button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("QAT button text.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("QAT Button")]
        public string Text
        {
            get { return _text; }

            set
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                    value = "QAT Button";

                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets and sets the shortcut key combination.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Shortcut key combination to fire click event of the quick access toolbar button.")]
        public Keys ShortcutKeys
        {
            get { return _shortcutKeys; }
            set { _shortcutKeys = value; }
        }

        private bool ShouldSerializeShortcutKeys()
        {
            return (ShortcutKeys != Keys.None);
        }

        /// <summary>
        /// Resets the ShortcutKeys property to its default value.
        /// </summary>
        public void ResetShortcutKeys()
        {
            ShortcutKeys = Keys.None;
        }

        /// <summary>
        /// Gets and sets the tooltip label style for the quick access button.
        /// </summary>
        [Category("Appearance")]
        [Description("Tooltip style for the quick access toolbar button.")]
        [DefaultValue(typeof(LabelStyle), "ToolTip")]
        public LabelStyle ToolTipStyle
        {
            get { return _toolTipStyle; }
            set { _toolTipStyle = value; }
        }

        /// <summary>
        /// Gets and sets the image for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Display image associated ToolTip.")]
        [DefaultValue(null)]
        [Localizable(true)]
        public Image ToolTipImage
        {
            get { return _toolTipImage; }
            set { _toolTipImage = value; }
        }

        /// <summary>
        /// Gets and sets the color to draw as transparent in the ToolTipImage.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Color to draw as transparent in the ToolTipImage.")]
        [KryptonDefaultColorAttribute()]
        [Localizable(true)]
        public Color ToolTipImageTransparentColor
        {
            get { return _toolTipImageTransparentColor; }
            set { _toolTipImageTransparentColor = value; }
        }

        /// <summary>
        /// Gets and sets the title text for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Title text for use in associated ToolTip.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Localizable(true)]
        public string ToolTipTitle
        {
            get { return _toolTipTitle; }
            set { _toolTipTitle = value; }
        }

        /// <summary>
        /// Gets and sets the body text for the item ToolTip.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Body text for use in associated ToolTip.")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Localizable(true)]
        public string ToolTipBody
        {
            get { return _toolTipBody; }
            set { _toolTipBody = value; }
        }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the quick access toolbar button.")]
        [DefaultValue(null)]
        public KryptonCommand KryptonCommand
        {
            get { return _command; }

            set
            {
                if (_command != value)
                {
                    if (_command != null)
                        _command.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);

                    _command = value;
                    OnPropertyChanged("KryptonCommand");

                    if (_command != null)
                        _command.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);

                    // Only need to update display if we are visible
                    if (Visible && (_ribbon != null))
                        _ribbon.PerformNeedPaint(false);
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

        #region IQuickAccessToolbarButton
        /// <summary>
        /// Provides a back reference to the owning ribbon control instance.
        /// </summary>
        /// <param name="ribbon">Reference to owning instance.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void SetRibbon(KryptonRibbon ribbon)
        {
            _ribbon = ribbon;
        }

        /// <summary>
        /// Gets the entry image.
        /// </summary>
        /// <returns>Image value.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Image GetImage()
        {
            if (KryptonCommand != null)
                return KryptonCommand.ImageSmall;
            else
                return Image;
        }

        /// <summary>
        /// Gets the entry text.
        /// </summary>
        /// <returns>Text value.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string GetText()
        {
            if (KryptonCommand != null)
                return KryptonCommand.TextLine1;
            else
                return Text;
        }

        /// <summary>
        /// Gets the entry enabled state.
        /// </summary>
        /// <returns>Enabled value.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool GetEnabled()
        {
            if (KryptonCommand != null)
                return KryptonCommand.Enabled;
            else
                return Enabled;
        }

        /// <summary>
        /// Gets the entry shortcut keys state.
        /// </summary>
        /// <returns>ShortcutKeys value.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Keys GetShortcutKeys()
        {
            return ShortcutKeys;
        }

        /// <summary>
        /// Gets the entry visible state.
        /// </summary>
        /// <returns>Visible value.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool GetVisible()
        {
            return Visible;
        }

        /// <summary>
        /// Sets a new value for the visible state.
        /// </summary>
        /// <param name="visible"></param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void SetVisible(bool visible)
        {
            Visible = visible;
        }

        /// <summary>
        /// Gets the tooltip label style.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public LabelStyle GetToolTipStyle()
        {
            return ToolTipStyle;
        }

        /// <summary>
        /// Gets and sets the image for the item ToolTip.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Image GetToolTipImage()
        {
            return ToolTipImage;
        }

        /// <summary>
        /// Gets and sets the color to draw as transparent in the ToolTipImage.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Color GetToolTipImageTransparentColor()
        {
            return ToolTipImageTransparentColor;
        }

        /// <summary>
        /// Gets and sets the title text for the item ToolTip.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string GetToolTipTitle()
        {
            return ToolTipTitle;
        }

        /// <summary>
        /// Gets and sets the body text for the item ToolTip.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string GetToolTipBody()
        {
            return ToolTipBody;
        }

        /// <summary>
        /// Generates a Click event for a button.
        /// </summary>
        public void PerformClick()
        {
            OnClick(EventArgs.Empty);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Handles a change in the property of an attached command.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
        protected virtual void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool refresh = false;

            switch (e.PropertyName)
            {
                case "Text":
                    refresh = true;
                    OnPropertyChanged("Text");
                    break;
                case "ImageSmall":
                    refresh = true;
                    OnPropertyChanged("Image");
                    break;
                case "Enabled":
                    refresh = true;
                    OnPropertyChanged("Enabled");
                    break;
            }

            if (refresh)
            {
                // Only need to update display if we are visible
                if (Visible && (_ribbon != null))
                    _ribbon.PerformNeedPaint(false);
            }
        }
        
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnClick(EventArgs e)
        {
            // Perform processing that is common to any action that would dismiss
            // any popup controls such as the showing minimized group popup
            if (Ribbon != null)
                Ribbon.ActionOccured();

            if (Click != null)
                Click(this, e);

            // Clicking the button should execute the associated command
            if (KryptonCommand != null)
                KryptonCommand.PerformExecute();
        }

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
