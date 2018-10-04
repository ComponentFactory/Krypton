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
using System.Collections;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Represents a ribbon group separator.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupGallery), "ToolboxBitmaps.KryptonGallery.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupGalleryDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Visible")]
    public class KryptonRibbonGroupGallery : KryptonRibbonGroupContainer
    {
        #region Static Fields
        private static readonly Image _defaultButtonImageLarge = Properties.Resources.ButtonImageLarge;
        #endregion

        #region Instance Fields
        private bool _visible;
        private bool _enabled;
        private string _textLine1;
        private string _textLine2;
        private Image _imageLarge;
        private string _keyTip;
        private NeedPaintHandler _viewPaintDelegate;
        private KryptonGallery _gallery;
        private KryptonGallery _lastGallery;
        private IKryptonDesignObject _designer;
        private Control _lastParentControl;
        private ViewBase _galleryView;
        private GroupItemSize _itemSizeMax;
        private GroupItemSize _itemSizeMin;
        private GroupItemSize _itemSizeCurrent;
        private int _largeItemCount;
        private int _mediumItemCount;
        private int _itemCount;
        private int _dropButtonItemWidth;
        private Image _toolTipImage;
        private Color _toolTipImageTransparentColor;
        private LabelStyle _toolTipStyle;
        private string _toolTipTitle;
        private string _toolTipBody;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the ImageList property changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the ImageList property changes.")]
        public event EventHandler ImageListChanged;

        /// <summary>
        /// Occurs when the value of the SelectedIndex property changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the SelectedIndex property changes.")]
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Occurs when the user is tracking over a color.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user is tracking over an image.")]
        public event EventHandler<ImageSelectEventArgs> TrackingImage;

        /// <summary>
        /// Occurs when the user invokes the drop down menu.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when user invokes the drop down menu.")]
        public event EventHandler<GalleryDropMenuEventArgs> GalleryDropMenu;
        
        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs after the value of a property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the control receives focus.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler GotFocus;

        /// <summary>
        /// Occurs when the control loses focus.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler LostFocus;

        /// <summary>
        /// Occurs when the design time context menu is requested.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event MouseEventHandler DesignTimeContextMenu;

        internal event EventHandler MouseEnterControl;
        internal event EventHandler MouseLeaveControl;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupGallery class.
        /// </summary>
        public KryptonRibbonGroupGallery()
        {
            // Default fields
            _visible = true;
            _enabled = true;
            _keyTip = "X";
            _itemSizeMax = GroupItemSize.Large;
            _itemSizeMin = GroupItemSize.Small;
            _itemSizeCurrent = GroupItemSize.Large;
            _largeItemCount = 9;
            _mediumItemCount = 3;
            _dropButtonItemWidth = 9;
            _imageLarge = _defaultButtonImageLarge;
            _textLine1 = "Gallery";
            _textLine2 = string.Empty;
            _toolTipImageTransparentColor = Color.Empty;
            _toolTipTitle = string.Empty;
            _toolTipBody = string.Empty;
            _toolTipStyle = LabelStyle.SuperTip;

            // Create the actual text box control and set initial settings
            _gallery = new KryptonGallery();
            _gallery.AlwaysActive = false;
            _gallery.TabStop = false;
            _gallery.InternalPreferredItemSize = new Size(_largeItemCount, 1);

            // Hook into events to expose via this container
            _gallery.SelectedIndexChanged += new EventHandler(OnGallerySelectedIndexChanged);
            _gallery.ImageListChanged += new EventHandler(OnGalleryImageListChanged);
            _gallery.TrackingImage += new EventHandler<ImageSelectEventArgs>(OnGalleryTrackingImage);
            _gallery.GalleryDropMenu += new EventHandler<GalleryDropMenuEventArgs>(OnGalleryGalleryDropMenu);
            _gallery.GotFocus += new EventHandler(OnGalleryGotFocus);
            _gallery.LostFocus += new EventHandler(OnGalleryLostFocus);

            // Ensure we can track mouse events on the gallery
            MonitorControl(_gallery);
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Access to the actual embedded KryptonGallery instance.
        /// </summary>
        [Description("Access to the actual embedded KryptonGallery instance.")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonGallery Gallery
        {
            get { return _gallery; }
        }

        /// <summary>
        /// Gets the collection of drop down ranges.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of drop down ranges")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonGalleryRangeCollection DropButtonRanges
        {
            get { return _gallery.DropButtonRanges; }
        }

        /// <summary>
        /// Gets and sets if scrolling is animated or a jump straight to target..
        /// </summary>
        [Category("Visuals")]
        [Description("Determines if scrolling is animated or a jump straight to target.")]
        [DefaultValue(true)]
        public bool SmoothScrolling
        {
            get { return _gallery.SmoothScrolling; }
            
            set 
            { 
                _gallery.SmoothScrolling = value;
                OnPropertyChanged("SmoothScrolling");
            }
        }

        /// <summary>
        /// Gets access to the collection of images for display and selection.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of images for display and selection.")]
        public ImageList ImageList
        {
            get { return _gallery.ImageList; }

            set
            {
                if (_gallery.ImageList != value)
                {
                    _gallery.ImageList = value;
                    OnPropertyChanged("ImageList");
                }
            }
        }

        /// <summary>
        /// Gets access to the collection of images for display and selection.
        /// </summary>
        [Category("Visuals")]
        [Description("The index of the selected image.")]
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get { return _gallery.SelectedIndex; }
            
            set 
            {
                if (_gallery.SelectedIndex != value)
                {
                    _gallery.SelectedIndex = value;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }

        /// <summary>
        /// Gets and sets the number of horizontal items when in large setting.
        /// </summary>
        [Category("Visuals")]
        [Description("Number of horizontal displayed items when in large setting.")]
        [DefaultValue(9)]
        public int LargeItemCount
        {
            get { return _largeItemCount; }

            set
            {
                if (_largeItemCount != value)
                {
                    _largeItemCount = value;

                    // Ensure the large count can never be less than the medium count
                    if (_largeItemCount < _mediumItemCount)
                        _mediumItemCount = _largeItemCount;

                    OnPropertyChanged("LargeItemCount");
                }
            }
        }

        /// <summary>
        /// Gets and sets the number of horizontal items when in medium setting.
        /// </summary>
        [Category("Visuals")]
        [Description("Number of horizontal displayed items when in medium setting.")]
        [DefaultValue(3)]
        public int MediumItemCount
        {
            get { return _mediumItemCount; }

            set
            {
                if (_mediumItemCount != value)
                {
                    _mediumItemCount = value;

                    // Ensure the medium count can never be more than the large count
                    if (_mediumItemCount > _largeItemCount)
                        _largeItemCount = _mediumItemCount;

                    OnPropertyChanged("MediumItemCount");
                }
            }
        }

        /// <summary>
        /// Gets and sets the number of horizontal displayed items when showing drop menu from the large button.
        /// </summary>
        [Category("Visuals")]
        [Description("Number of horizontal displayed items when showing drop menu from the large button.")]
        [DefaultValue(9)]
        public int DropButtonItemWidth
        {
            get { return _dropButtonItemWidth; }

            set
            {
                if (_dropButtonItemWidth != value)
                {
                    value = Math.Max(1, value);
                    _dropButtonItemWidth = value;
                    OnPropertyChanged("DropButtonItemWidth");
                }
            }
        }

        /// <summary>
        /// Gets and sets the maximum number of lines items for the drop down menu.
        /// </summary>
        [Category("Visuals")]
        [Description("Maximum number of line items for the drop down menu.")]
        [DefaultValue(128)]
        public int DropMaxItemWidth
        {
            get { return _gallery.DropMaxItemWidth; }
            
            set 
            {
                if (_gallery.DropMaxItemWidth != value)
                {
                    _gallery.DropMaxItemWidth = value;
                    OnPropertyChanged("DropMaxItemWidth");
                }
            }
        }

        /// <summary>
        /// Gets and sets the minimum number of lines items for the drop down menu.
        /// </summary>
        [Category("Visuals")]
        [Description("Minimum number of line items for the drop down menu.")]
        [DefaultValue(3)]
        public int DropMinItemWidth
        {
            get { return _gallery.DropMinItemWidth; }
            
            set 
            {
                if (_gallery.DropMinItemWidth != value)
                {
                    _gallery.DropMinItemWidth = value;
                    OnPropertyChanged("DropMinItemWidth");
                }
            }
        }

        /// <summary>
        /// Gets and sets the associated context menu strip.
        /// </summary>
        [Category("Behavior")]
        [Description("The shortcut to display when the user right-clicks the control.")]
        [DefaultValue(null)]
        public ContextMenuStrip ContextMenuStrip
        {
            get { return _gallery.ContextMenuStrip; }
            set { _gallery.ContextMenuStrip = value; }
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu for showing when the gallery is right clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("KryptonContextMenu to be shown when the gallery is right clicked.")]
        [DefaultValue(null)]
        public KryptonContextMenu KryptonContextMenu
        {
            get { return _gallery.KryptonContextMenu; }
            set { _gallery.KryptonContextMenu = value; }
        }

        /// <summary>
        /// Gets and sets the key tip for the ribbon group gallery.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group gallery key tip.")]
        [DefaultValue("X")]
        public string KeyTip
        {
            get { return _keyTip; }

            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "X";

                _keyTip = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets and sets the large button image.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Large gallery button image.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image ImageLarge
        {
            get { return _imageLarge; }

            set
            {
                if (_imageLarge != value)
                {
                    _imageLarge = value;
                    OnPropertyChanged("ImageLarge");
                }
            }
        }

        private bool ShouldSerializeImageLarge()
        {
            return ImageLarge != _defaultButtonImageLarge;
        }

        /// <summary>
        /// Gets and sets the display gallery text line 1 for the button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Gallery button display text line 1.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Gallery")]
        public string TextLine1
        {
            get { return _textLine1; }

            set
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                    value = "Gallery";

                if (value != _textLine1)
                {
                    _textLine1 = value;
                    OnPropertyChanged("TextLine1");
                }
            }
        }

        /// <summary>
        /// Gets and sets the display gallery text line 2 for the button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Gallery button display text line 2.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("")]
        public string TextLine2
        {
            get { return _textLine2; }

            set
            {
                if (value != _textLine2)
                {
                    _textLine2 = value;
                    OnPropertyChanged("TextLine2");
                }
            }
        }

        /// <summary>
        /// Gets and sets the tooltip label style for group button.
        /// </summary>
        [Category("Appearance")]
        [Description("Tooltip style for the group button.")]
        [DefaultValue(typeof(LabelStyle), "SuperTip")]
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
        /// Gets and sets the visible state of the group gallery.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group gallery is visible or hidden.")]
        [DefaultValue(true)]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool Visible
        {
            get { return _visible; }

            set
            {
                if (value != _visible)
                {
                    _visible = value;
                    OnPropertyChanged("Visible");
                }
            }
        }

        /// <summary>
        /// Make the ribbon group gallery visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon group gallery hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the group gallery.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group gallery is enabled.")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return _enabled; }
            
            set 
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }

        /// <summary>
        /// Gets and sets the maximum allowed size of the gallery.
        /// </summary>
        [Category("Visuals")]
        [Description("Maximum size of the gallery.")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(GroupItemSize), "Large")]
        [RefreshProperties(RefreshProperties.All)]
        public GroupItemSize MaximumSize
        {
            get { return ItemSizeMaximum; }
            set { ItemSizeMaximum = value; }
        }

        /// <summary>
        /// Gets and sets the minimum allowed size of the gallery.
        /// </summary>
        [Category("Visuals")]
        [Description("Minimum size of the gallery.")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(GroupItemSize), "Small")]
        [RefreshProperties(RefreshProperties.All)]
        public GroupItemSize MinimumSize
        {
            get { return ItemSizeMinimum; }
            set { ItemSizeMinimum = value; }
        }

        /// <summary>
        /// Gets and sets the maximum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeMaximum 
        {
            get { return _itemSizeMax; }

            set
            {
                if (_itemSizeMax != value)
                {
                    _itemSizeMax = value;
                    OnPropertyChanged("ItemSizeMaximum");
                }
            }
        }

        /// <summary>
        /// Gets and sets the minimum allowed size of the item.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeMinimum
        {
            get { return _itemSizeMin; }

            set
            {
                if (_itemSizeMin != value)
                {
                    _itemSizeMin = value;
                    OnPropertyChanged("ItemSizeMinimum");
                }
            }
        }

        /// <summary>
        /// Gets and sets the current item size.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GroupItemSize ItemSizeCurrent
        {
            get { return _itemSizeCurrent; }
            
            set 
            {
                _itemSizeCurrent = value;

                switch (value)
                {
                    case GroupItemSize.Large:
                        _gallery.InternalPreferredItemSize = new Size(InternalItemCount, 1);
                        break;
                    case GroupItemSize.Medium:
                        _gallery.InternalPreferredItemSize = new Size(MediumItemCount, 1);
                        break;
                }

                OnPropertyChanged("ItemSizeCurrent");
            }
        }

        /// <summary>
        /// Creates an appropriate view element for this item.
        /// </summary>
        /// <param name="ribbon">Reference to the owning ribbon control.</param>
        /// <param name="needPaint">Delegate for notifying changes in display.</param>
        /// <returns>ViewBase derived instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ViewBase CreateView(KryptonRibbon ribbon,
                                            NeedPaintHandler needPaint)
        {
            return new ViewDrawRibbonGroupGallery(ribbon, this, needPaint);
        }

        /// <summary>
        /// Gets and sets the associated designer.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public IKryptonDesignObject GalleryDesigner
        {
            get { return _designer; }
            set { _designer = value; }
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase GalleryView
        {
            get { return _galleryView; }
            set { _galleryView = value; }
        }
        #endregion

        #region Internal
        internal Control LastParentControl
        {
            get { return _lastParentControl; }
            set { _lastParentControl = value; }
        }

        internal KryptonGallery LastGallery
        {
            get { return _lastGallery; }
            set { _lastGallery = value; }
        }

        internal NeedPaintHandler ViewPaintDelegate
        {
            get { return _viewPaintDelegate; }
            set { _viewPaintDelegate = value; }
        }

        internal void OnDesignTimeContextMenu(MouseEventArgs e)
        {
            if (DesignTimeContextMenu != null)
                DesignTimeContextMenu(this, e);
        }

        internal int InternalItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; }
        }

        internal override LabelStyle InternalToolTipStyle
        {
            get { return ToolTipStyle; }
        }

        internal override Image InternalToolTipImage
        {
            get { return ToolTipImage; }
        }

        internal override Color InternalToolTipImageTransparentColor
        {
            get { return ToolTipImageTransparentColor; }
        }

        internal override string InternalToolTipTitle
        {
            get { return ToolTipTitle; }
        }

        internal override string InternalToolTipBody
        {
            get { return ToolTipBody; }
        }

        internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return false;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the ImageListChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnImageListChanged(EventArgs e)
        {
            if (ImageListChanged != null)
                ImageListChanged(this, e);
        }

        /// <summary>
        /// Raises the SelectedIndexChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }

        /// <summary>
        /// Raises the SelectedIndexChanged event.
        /// </summary>
        /// <param name="e">An ImageSelectEventArgs containing the event data.</param>
        protected virtual void OnTrackingImage(ImageSelectEventArgs e)
        {
            if (TrackingImage != null)
                TrackingImage(this, e);
        }

        /// <summary>
        /// Raises the GalleryDropMenu event.
        /// </summary>
        /// <param name="e">An GalleryDropMenuEventArgs containing the event data.</param>
        protected virtual void OnGalleryDropMenu(GalleryDropMenuEventArgs e)
        {
            if (GalleryDropMenu != null)
                GalleryDropMenu(this, e);
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnGotFocus(EventArgs e)
        {
            if (GotFocus != null)
                GotFocus(this, e);
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnLostFocus(EventArgs e)
        {
            if (LostFocus != null)
                LostFocus(this, e);
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

        #region Implementation
        private void MonitorControl(KryptonGallery c)
        {
            c.MouseEnter += new EventHandler(OnControlEnter);
            c.MouseLeave += new EventHandler(OnControlLeave);
        }

        private void UnmonitorControl(KryptonGallery c)
        {
            c.MouseEnter -= new EventHandler(OnControlEnter);
            c.MouseLeave -= new EventHandler(OnControlLeave);
        }

        private void OnControlEnter(object sender, EventArgs e)
        {
            if (MouseEnterControl != null)
                MouseEnterControl(this, e);
        }

        private void OnControlLeave(object sender, EventArgs e)
        {
            if (MouseLeaveControl != null)
                MouseLeaveControl(this, e);
        }

        private void OnGalleryImageListChanged(object sender, EventArgs e)
        {
            OnImageListChanged(e);
        }

        private void OnGallerySelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(e);
        }

        private void OnGalleryTrackingImage(object sender, ImageSelectEventArgs e)
        {
            OnTrackingImage(e);
        }

        private void OnGalleryGalleryDropMenu(object sender, GalleryDropMenuEventArgs e)
        {
            OnGalleryDropMenu(e);
        }

        private void OnGalleryGotFocus(object sender, EventArgs e)
        {
            OnGotFocus(e);
        }

        private void OnGalleryLostFocus(object sender, EventArgs e)
        {
            OnLostFocus(e);
        }
        #endregion
    }
}
