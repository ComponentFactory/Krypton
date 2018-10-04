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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Represents a ribbon group cluster color button.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupClusterColorButton), "ToolboxBitmaps.KryptonRibbonGroupClusterColorButton.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupClusterColorButtonDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("SelectedColorChanged")]
    [DefaultProperty("SelectedColor")]
    public class KryptonRibbonGroupClusterColorButton : KryptonRibbonGroupItem
    {
        #region Static Fields
        private static readonly Image _defaultButtonImageSmall = Properties.Resources.ButtonColorImageSmall;
        #endregion

        #region Instance Fields
        private bool _enabled;
        private bool _visible;
        private bool _checked;
        private bool _autoRecentColors;
        private bool _visibleThemes;
        private bool _visibleStandard;
        private bool _visibleRecent;
        private bool _visibleNoColor;
        private bool _visibleMoreColors;
        private string _textLine;
        private string _keyTip;
        private string _toolTipTitle;
        private string _toolTipBody;
        private Rectangle _selectedRect;
        private Color _selectedColor;
        private Color _emptyBorderColor;
        private Image _toolTipImage;
        private Image _imageSmall;
        private Color _toolTipImageTransparentColor;
        private LabelStyle _toolTipStyle;
        private Keys _shortcutKeys;
        private KryptonCommand _command;
        private GroupItemSize _itemSizeMax;
        private GroupItemSize _itemSizeMin;
        private GroupItemSize _itemSizeCurrent;
        private GroupButtonType _buttonType;
        private EventHandler _kcmFinishDelegate;
        private ViewBase _clusterColorButtonView;
        private ColorScheme _schemeThemes;
        private ColorScheme _schemeStandard;
        private int _maxRecentColors;
        private List<Color> _recentColors;
        
        // Context menu items
        private KryptonContextMenu _kryptonContextMenu;
        private KryptonContextMenuSeparator _separatorTheme, _separatorStandard, _separatorRecent;
        private KryptonContextMenuHeading _headingTheme, _headingStandard, _headingRecent;
        private KryptonContextMenuColorColumns _colorsTheme, _colorsStandard, _colorsRecent;
        private KryptonContextMenuSeparator _separatorNoColor;
        private KryptonContextMenuItems _itemsNoColor;
        private KryptonContextMenuItem _itemNoColor;
        private KryptonContextMenuSeparator _separatorMoreColors;
        private KryptonContextMenuItems _itemsMoreColors;
        private KryptonContextMenuItem _itemMoreColors;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the color button is clicked.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the color button is clicked.")]
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the drop down color button type is pressed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the drop down color button type is pressed.")]
        public event EventHandler<ContextMenuArgs> DropDown;

        /// <summary>
        /// Occurs when the SelectedColor property changes value.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the SelectedColor property changes value.")]
        public event EventHandler<ColorEventArgs> SelectedColorChanged;

        /// <summary>
        /// Occurs when the user is tracking over a color.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when user is tracking over a color.")]
        public event EventHandler<ColorEventArgs> TrackingColor;

        /// <summary>
        /// Occurs when the user selects the more colors option.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when user selects the more colors option.")]
        public event CancelEventHandler MoreColors;

        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs after the value of a property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the design time context menu is requested.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event MouseEventHandler DesignTimeContextMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupClusterColorButton class.
        /// </summary>
        public KryptonRibbonGroupClusterColorButton()
        {
            // Default fields
            _enabled = true;
            _visible = true;
            _checked = false;
            _autoRecentColors = true;
            _keyTip = "B";
            _textLine = string.Empty;
            _shortcutKeys = Keys.None;
            _selectedColor = Color.Red;
            _emptyBorderColor = Color.DarkGray;
            _selectedRect = new Rectangle(0, 12, 16, 4);
            _schemeThemes = ColorScheme.OfficeThemes;
            _schemeStandard = ColorScheme.OfficeStandard;
            _visibleThemes = true;
            _visibleStandard = true;
            _visibleRecent = true;
            _visibleNoColor = true;
            _visibleMoreColors = true;
            _itemSizeMax = GroupItemSize.Medium;
            _itemSizeMin = GroupItemSize.Small;
            _itemSizeCurrent = GroupItemSize.Medium;
            _imageSmall = _defaultButtonImageSmall;
            _buttonType = GroupButtonType.Split;
            _toolTipImageTransparentColor = Color.Empty;
            _toolTipTitle = string.Empty;
            _toolTipBody = string.Empty;
            _toolTipStyle = LabelStyle.SuperTip;
            _maxRecentColors = 10;
            _recentColors = new List<Color>();

            // Create the context menu items
            _kryptonContextMenu = new KryptonContextMenu();
            _separatorTheme = new KryptonContextMenuSeparator();
            _headingTheme = new KryptonContextMenuHeading("Theme Colors");
            _colorsTheme = new KryptonContextMenuColorColumns(ColorScheme.OfficeThemes);
            _separatorStandard = new KryptonContextMenuSeparator();
            _headingStandard = new KryptonContextMenuHeading("Standard Colors");
            _colorsStandard = new KryptonContextMenuColorColumns(ColorScheme.OfficeStandard);
            _separatorRecent = new KryptonContextMenuSeparator();
            _headingRecent = new KryptonContextMenuHeading("Recent Colors");
            _colorsRecent = new KryptonContextMenuColorColumns(ColorScheme.None);
            _separatorNoColor = new KryptonContextMenuSeparator();
            _itemNoColor = new KryptonContextMenuItem("&No Color", Properties.Resources.ButtonNoColor, new EventHandler(OnClickNoColor));
            _itemsNoColor = new KryptonContextMenuItems();
            _itemsNoColor.Items.Add(_itemNoColor);
            _separatorMoreColors = new KryptonContextMenuSeparator();
            _itemMoreColors = new KryptonContextMenuItem("&More Colors...", new EventHandler(OnClickMoreColors));
            _itemsMoreColors = new KryptonContextMenuItems();
            _itemsMoreColors.Items.Add(_itemMoreColors);
            _kryptonContextMenu.Items.AddRange(new KryptonContextMenuItemBase[] { _separatorTheme, _headingTheme, _colorsTheme, 
                                                                                  _separatorStandard, _headingStandard, _colorsStandard,
                                                                                  _separatorRecent, _headingRecent, _colorsRecent,
                                                                                  _separatorNoColor, _itemsNoColor,
                                                                                  _separatorMoreColors, _itemsMoreColors});
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the selected color.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Selected color.")]
        [DefaultValue(typeof(Color), "Red")]
        public Color SelectedColor
        {
            get { return _selectedColor; }

            set
            {
                if (value != _selectedColor)
                {
                    _selectedColor = value;
                    UpdateRecentColors(_selectedColor);
                    OnSelectedColorChanged(_selectedColor);
                    OnPropertyChanged("SelectedColor");
                }
            }
        }

        /// <summary>
        /// Gets and sets the selected color block when selected color is empty.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Border color of selected block when selected color is empty.")]
        [DefaultValue(typeof(Color), "DarkGray")]
        public Color EmptyBorderColor
        {
            get { return _emptyBorderColor; }

            set
            {
                if (value != _emptyBorderColor)
                {
                    _emptyBorderColor = value;
                    OnPropertyChanged("EmptyBorderColor");
                }
            }
        }

        /// <summary>
        /// Gets and sets the selected color drawing rectangle.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Description("Selected color drawing rectangle.")]
        [DefaultValue(typeof(Rectangle), "0,12,16,4")]
        public Rectangle SelectedRect
        {
            get { return _selectedRect; }

            set
            {
                _selectedRect = value;
                OnPropertyChanged("SelectedRect");
            }
        }

        /// <summary>
        /// Gets and sets the display text line for the color button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Color button display text line.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("")]
        public string TextLine
        {
            get { return _textLine; }

            set
            {
                if (value != _textLine)
                {
                    _textLine = value;
                    OnPropertyChanged("TextLine");
                }
            }
        }

        /// <summary>
        /// Gets and sets the key tip for the ribbon group cluster color button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group cluster color button key tip.")]
        [DefaultValue("B")]
        public string KeyTip
        {
            get { return _keyTip; }

            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "B";

                _keyTip = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets and sets the small color button image.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Small color button image.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image ImageSmall
        {
            get { return _imageSmall; }

            set
            {
                if (_imageSmall != value)
                {
                    _imageSmall = value;
                    OnPropertyChanged("ImageSmall");
                }
            }
        }

        private bool ShouldSerializeImageSmall()
        {
            return ImageSmall != _defaultButtonImageSmall;
        }

        /// <summary>
        /// Gets and sets the maximum number of recent colors to store and display.
        /// </summary>
        [Category("Behavior")]
        [Description("Determine the maximum number of recent colors to store and display.")]
        [DefaultValue(10)]
        public int MaxRecentColors
        {
            get { return _maxRecentColors; }

            set
            {
                if (value != _maxRecentColors)
                {
                    _maxRecentColors = value;
                    OnPropertyChanged("MaxRecentColors");
                }
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the themes color set.
        /// </summary>
        [Category("Behavior")]
        [Description("Determine the visible state of the themes color set.")]
        [DefaultValue(true)]
        public bool VisibleThemes
        {
            get { return _visibleThemes; }

            set
            {
                if (value != _visibleThemes)
                {
                    _visibleThemes = value;
                    OnPropertyChanged("VisibleThemes");
                }
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the standard color set.
        /// </summary>
        [Category("Behavior")]
        [Description("Determine the visible state of the standard color set.")]
        [DefaultValue(true)]
        public bool VisibleStandard
        {
            get { return _visibleStandard; }

            set
            {
                if (value != _visibleStandard)
                {
                    _visibleStandard = value;
                    OnPropertyChanged("VisibleStandard");
                }
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the recent color set.
        /// </summary>
        [Category("Behavior")]
        [Description("Determine the visible state of the recent color set.")]
        [DefaultValue(true)]
        public bool VisibleRecent
        {
            get { return _visibleRecent; }

            set
            {
                if (value != _visibleRecent)
                {
                    _visibleRecent = value;
                    OnPropertyChanged("VisibleRecent");
                }
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the no color menu item.
        /// </summary>
        [Category("Behavior")]
        [Description("Determine if the 'No Color' menu item is used.")]
        [DefaultValue(true)]
        public bool VisibleNoColor
        {
            get { return _visibleNoColor; }

            set
            {
                if (value != _visibleNoColor)
                {
                    _visibleNoColor = value;
                    OnPropertyChanged("VisibleNoColor");
                }
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the more colors menu item.
        /// </summary>
        [Category("Behavior")]
        [Description("Determine if the 'More Colors...' menu item is used.")]
        [DefaultValue(true)]
        public bool VisibleMoreColors
        {
            get { return _visibleMoreColors; }

            set
            {
                if (value != _visibleMoreColors)
                {
                    _visibleMoreColors = value;
                    OnPropertyChanged("VisibleMoreColors");
                }
            }
        }

        /// <summary>
        /// Gets and sets if the recent colors should be automatically updated.
        /// </summary>
        [Category("Behavior")]
        [Description("Should recent colors be automatically updated.")]
        [DefaultValue(true)]
        public bool AutoRecentColors
        {
            get { return _autoRecentColors; }

            set
            {
                if (value != _autoRecentColors)
                {
                    _autoRecentColors = value;
                    OnPropertyChanged("AutoRecentColors");
                }
            }
        }

        /// <summary>
        /// Gets and sets the color scheme for the themes color set.
        /// </summary>
        [Category("Behavior")]
        [Description("Color scheme to use for the themes color set.")]
        [DefaultValue(typeof(ColorScheme), "OfficeThemes")]
        public ColorScheme SchemeThemes
        {
            get { return _schemeThemes; }

            set
            {
                if (value != _schemeThemes)
                {
                    _schemeThemes = value;
                    OnPropertyChanged("SchemeThemes");
                }
            }
        }

        /// <summary>
        /// Gets and sets the color scheme for the standard color set.
        /// </summary>
        [Category("Behavior")]
        [Description("Color scheme to use for the standard color set.")]
        [DefaultValue(typeof(ColorScheme), "OfficeStandard")]
        public ColorScheme SchemeStandard
        {
            get { return _schemeStandard; }

            set
            {
                if (value != _schemeStandard)
                {
                    _schemeStandard = value;
                    OnPropertyChanged("SchemeStandard");
                }
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the cluster color button.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the cluster color button is visible or hidden.")]
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
        /// Make the ribbon color button visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon color button hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the group color button.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group color button is enabled.")]
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
                }
            }
        }

        /// <summary>
        /// Gets and sets the checked state of the group color button.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group color button is checked.")]
        [DefaultValue(false)]
        public bool Checked
        {
            get { return _checked; }

            set
            {
                if (value != _checked)
                {
                    _checked = value;
                    OnPropertyChanged("Checked");
                }
            }
        }

        /// <summary>
        /// Gets and sets the operation of the group color button.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines how the group color button operation.")]
        [DefaultValue(typeof(GroupButtonType), "Split")]
        public GroupButtonType ButtonType
        {
            get { return _buttonType; }

            set
            {
                if (value != _buttonType)
                {
                    _buttonType = value;
                    OnPropertyChanged("ButtonType");
                }
            }
        }

        /// <summary>
        /// Gets and sets the shortcut key combination.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Shortcut key combination to fire click event of the cluster color button.")]
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
        /// Gets and sets the tooltip label style for group color cluster button.
        /// </summary>
        [Category("Appearance")]
        [Description("Tooltip style for the group color cluster button.")]
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
        /// Gets and sets the set of recent colors.
        /// </summary>
        [Category("Appearance")]
        [Description("Collection of recent colors.")]
        public Color[] RecentColors
        {
            get { return _recentColors.ToArray(); }

            set
            {
                ClearRecentColors();

                // You cannot add an empty collection
                if (value != null)
                    _recentColors.AddRange(value);
            }
        }

        /// <summary>
        /// Clear the recent colors setting.
        /// </summary>
        public void ClearRecentColors()
        {
            _recentColors.Clear();
        }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the color button.")]
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
                }
            }
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
                // We can never be bigger than medium
                if (value == GroupItemSize.Large)
                    value = GroupItemSize.Medium;

                if (_itemSizeMax != value)
                {
                    _itemSizeMax = value;

                    if (_itemSizeMax == GroupItemSize.Small)
                        _itemSizeMin = GroupItemSize.Small;

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
                // We can never be bigger than medium
                if (value == GroupItemSize.Large)
                    value = GroupItemSize.Medium;

                if (_itemSizeMin != value)
                {
                    _itemSizeMin = value;

                    if (_itemSizeMin == GroupItemSize.Medium)
                        _itemSizeMax = GroupItemSize.Medium;

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
                if (_itemSizeCurrent != value)
                {
                    _itemSizeCurrent = value;
                    OnPropertyChanged("ItemSizeCurrent");
                }
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
            return new ViewDrawRibbonGroupClusterColorButton(ribbon, this, needPaint);
        }

        /// <summary>
        /// Generates a Click event for a button.
        /// </summary>
        public void PerformClick()
        {
            PerformClick(null);
        }

        /// <summary>
        /// Generates a Click event for a button.
        /// </summary>
        /// <param name="finishDelegate">Delegate fired during event processing.</param>
        public void PerformClick(EventHandler finishDelegate)
        {
            OnClick(finishDelegate);
        }

        /// <summary>
        /// Generates a DropDown event for a button.
        /// </summary>
        public void PerformDropDown()
        {
            PerformDropDown(null);
        }

        /// <summary>
        /// Generates a DropDown event for a button.
        /// </summary>
        /// <param name="finishDelegate">Delegate fired during event processing.</param>
        public void PerformDropDown(EventHandler finishDelegate)
        {
            OnDropDown(finishDelegate);
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase ClusterColorButtonView
        {
            get { return _clusterColorButtonView; }
            set { _clusterColorButtonView = value; }
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
            switch (e.PropertyName)
            {
                case "Text":
                    OnPropertyChanged("TextLine");
                    break;
                case "ImageSmall":
                    OnPropertyChanged("ImageSmall");
                    break;
                case "Enabled":
                    OnPropertyChanged("Enabled");
                    break;
                case "Checked":
                    OnPropertyChanged("Checked");
                    break;
            }
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="finishDelegate">Delegate fired during event processing.</param>
        protected virtual void OnClick(EventHandler finishDelegate)
        {
            bool fireDelegate = true;

            if (!Ribbon.InDesignMode)
            {
                // Events only occur when enabled
                if (Enabled)
                {
                    // A check button should always toggle state
                    if (ButtonType == GroupButtonType.Check)
                    {
                        // Push back the change to the attached command
                        if (KryptonCommand != null)
                            KryptonCommand.Checked = !KryptonCommand.Checked;
                        else
                            Checked = !Checked;
                    }

                    // In showing a popup we fire the delegate before the click so that the
                    // minimized popup is removed out of the way before the event is handled
                    // because if the event shows a dialog then it would appear behind the popup
                    if (VisualPopupManager.Singleton.CurrentPopup != null)
                    {
                        // Do we need to fire a delegate stating the click processing has finished?
                        if (fireDelegate && (finishDelegate != null))
                            finishDelegate(this, EventArgs.Empty);

                        fireDelegate = false;
                    }

                    // Generate actual click event
                    if (Click != null)
                        Click(this, EventArgs.Empty);

                    // Clicking the button should execute the associated command
                    if (KryptonCommand != null)
                        KryptonCommand.PerformExecute();
                }
            }

            // Do we need to fire a delegate stating the click processing has finished?
            if (fireDelegate && (finishDelegate != null))
                finishDelegate(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the DropDown event.
        /// </summary>
        /// <param name="finishDelegate">Delegate fired during event processing.</param>
        protected virtual void OnDropDown(EventHandler finishDelegate)
        {
            bool fireDelegate = true;

            if (!Ribbon.InDesignMode)
            {
                // Events only occur when enabled
                if (Enabled)
                {
                    if ((ButtonType == GroupButtonType.DropDown) ||
                        (ButtonType == GroupButtonType.Split))
                    {
                        if (_kryptonContextMenu != null)
                        {
                            UpdateContextMenu();

                            ContextMenuArgs contextArgs = new ContextMenuArgs(_kryptonContextMenu);

                            // Generate an event giving a chance for the krypton context menu strip to 
                            // be shown to be provided/modified or the action even to be cancelled
                            if (DropDown != null)
                                DropDown(this, contextArgs);

                            // If user did not cancel and there is still a krypton context menu strip to show
                            if (!contextArgs.Cancel && (contextArgs.KryptonContextMenu != null))
                            {
                                Rectangle screenRect = Rectangle.Empty;

                                // Convert the view for the button into screen coordinates
                                if ((Ribbon != null) && (ClusterColorButtonView != null))
                                    screenRect = Ribbon.ViewRectangleToScreen(ClusterColorButtonView);

                                if (CommonHelper.ValidKryptonContextMenu(contextArgs.KryptonContextMenu))
                                {
                                    // Cache the finish delegate to call when the menu is closed
                                    _kcmFinishDelegate = finishDelegate;

                                    // Decide which separators are needed
                                    DecideOnVisible(_separatorTheme, _colorsTheme);
                                    DecideOnVisible(_separatorStandard, _colorsStandard);
                                    DecideOnVisible(_separatorRecent, _colorsRecent);
                                    DecideOnVisible(_separatorNoColor, _itemsNoColor);
                                    DecideOnVisible(_separatorMoreColors, _itemsMoreColors);

                                    // Monitor relevant events inside the context menu
                                    HookContextMenuEvents(_kryptonContextMenu.Items, true);

                                    // Show at location we were provided, but need to convert to screen coordinates
                                    contextArgs.KryptonContextMenu.Closed += new ToolStripDropDownClosedEventHandler(OnKryptonContextMenuClosed);
                                    if (contextArgs.KryptonContextMenu.Show(this, new Point(screenRect.X, screenRect.Bottom + 1)))
                                        fireDelegate = false;
                                }
                            }
                        }
                    }
                }
            }

            // Do we need to fire a delegate stating the click processing has finished?
            if (fireDelegate && (finishDelegate != null))
                finishDelegate(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the SelectedColorChanged event.
        /// </summary>
        /// <param name="selectedColor">New selected color.</param>
        protected virtual void OnSelectedColorChanged(Color selectedColor)
        {
            if (SelectedColorChanged != null)
                SelectedColorChanged(this, new ColorEventArgs(selectedColor));
        }

        /// <summary>
        /// Raises the TrackingColor event.
        /// </summary>
        /// <param name="e">An ColorEventArgs that contains the event data.</param>
        protected virtual void OnTrackingColor(ColorEventArgs e)
        {
            if (TrackingColor != null)
                TrackingColor(this, e);
        }

        /// <summary>
        /// Raises the MoreColors event.
        /// </summary>
        /// <param name="e">An CancelEventArgs that contains the event data.</param>
        protected virtual void OnMoreColors(CancelEventArgs e)
        {
            if (MoreColors != null)
                MoreColors(this, e);
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

        #region Internal
        internal void OnDesignTimeContextMenu(MouseEventArgs e)
        {
            if (DesignTimeContextMenu != null)
                DesignTimeContextMenu(this, e);
        }

        internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Only interested in key processing if this button definition 
            // is enabled and itself and all parents are also visible
            if (Enabled && ChainVisible)
            {
                // Do we have a shortcut definition for ourself?
                if (ShortcutKeys != Keys.None)
                {
                    // Does it match the incoming key combination?
                    if (ShortcutKeys == keyData)
                    {
                        // Button type determines what event to fire
                        switch (ButtonType)
                        {
                            case GroupButtonType.Push:
                            case GroupButtonType.Check:
                                PerformClick();
                                return true;
                            case GroupButtonType.DropDown:
                            case GroupButtonType.Split:
                                PerformDropDown();
                                return true;
                            default:
                                // Should never happen!
                                Debug.Assert(false);
                                break;
                        }

                        return true;
                    }
                }

                // Check the types that have a relevant context menu strip
                if ((ButtonType == GroupButtonType.DropDown) ||
                    (ButtonType == GroupButtonType.Split))
                {
                    if (_kryptonContextMenu != null)
                        if (_kryptonContextMenu.ProcessShortcut(keyData))
                            return true;
                }
            }

            return false;
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
        #endregion

        #region Implementation
        private void HookContextMenuEvents(KryptonContextMenuCollection collection, bool hook)
        {
            // Search for items of interest
            foreach (KryptonContextMenuItemBase item in collection)
            {
                // Hook into color events
                if (item is KryptonContextMenuColorColumns)
                {
                    KryptonContextMenuColorColumns columns = (KryptonContextMenuColorColumns)item;
                    columns.SelectedColor = _selectedColor;

                    if (hook)
                    {
                        columns.TrackingColor += new EventHandler<ColorEventArgs>(OnColumnsTrackingColor);
                        columns.SelectedColorChanged += new EventHandler<ColorEventArgs>(OnColumnsSelectedColorChanged);
                    }
                    else
                    {
                        columns.TrackingColor -= new EventHandler<ColorEventArgs>(OnColumnsTrackingColor);
                        columns.SelectedColorChanged -= new EventHandler<ColorEventArgs>(OnColumnsSelectedColorChanged);
                    }
                }
            }
        }

        private void UpdateRecentColors(Color color)
        {
            // Do we need to update the recent colors collection?
            if (AutoRecentColors)
            {
                // We do not add to recent colors if it is inside another color columns 
                foreach (KryptonContextMenuItemBase item in _kryptonContextMenu.Items)
                {
                    // Only interested in the non-recent colors color columns
                    if ((item is KryptonContextMenuColorColumns) && (item != _colorsRecent))
                    {
                        // Cast to correct type
                        KryptonContextMenuColorColumns colors = (KryptonContextMenuColorColumns)item;

                        // We do not change the theme or standard entries if they are not to be used
                        if (((item == _colorsTheme) && !VisibleThemes) ||
                            ((item == _colorsStandard) && !VisibleStandard))
                            continue;

                        // If matching color found, do not add to recent colors
                        if (colors.ContainsColor(color))
                            return;
                    }
                }

                // If this color valid and so possible to become a recent color
                if ((color != null) && !color.Equals(Color.Empty))
                {
                    bool found = false;
                    foreach (Color recentColor in _recentColors)
                        if (recentColor.Equals(color))
                        {
                            found = true;
                            break;
                        }

                    // If the color is not already part of the recent colors
                    if (!found)
                    {
                        // Add to start of the list
                        _recentColors.Insert(0, color);

                        // Enforce the maximum number of recent colors
                        if (_recentColors.Count > MaxRecentColors)
                            _recentColors.RemoveRange(MaxRecentColors, _recentColors.Count - MaxRecentColors);
                    }
                }
            }
        }

        private void UpdateContextMenu()
        {
            // Update visible state based of properties
            _separatorTheme.Visible = _headingTheme.Visible = _colorsTheme.Visible = _visibleThemes;
            _separatorStandard.Visible = _headingStandard.Visible = _colorsStandard.Visible = _visibleStandard;
            _separatorRecent.Visible = _headingRecent.Visible = _colorsRecent.Visible = (_visibleRecent && (_recentColors.Count > 0));
            _itemsNoColor.Visible = _visibleNoColor;
            _itemsMoreColors.Visible = _visibleMoreColors;

            // Define the display strings
            _headingTheme.Text = Ribbon.RibbonStrings.ThemeColors;
            _headingStandard.Text = Ribbon.RibbonStrings.StandardColors;
            _headingRecent.Text = Ribbon.RibbonStrings.RecentColors;
            _itemNoColor.Text = Ribbon.RibbonStrings.NoColor;
            _itemMoreColors.Text = Ribbon.RibbonStrings.MoreColors;

            // Define the colors used in the first two color schemes
            _colorsTheme.ColorScheme = SchemeThemes;
            _colorsStandard.ColorScheme = SchemeStandard;

            // Define the recent colors
            if (_recentColors.Count == 0)
                _colorsRecent.SetCustomColors(null);
            else
            {
                // Create an array of color arrays
                Color[][] colors = new Color[_recentColors.Count][];

                // Each column is just a single color
                for (int i = 0; i < _recentColors.Count; i++)
                    colors[i] = new Color[] { _recentColors[i] };

                _colorsRecent.SetCustomColors(colors);
            }

            // Should the no color entry be checked?
            _itemNoColor.Checked = _selectedColor.Equals(Color.Empty);
        }

        private void DecideOnVisible(KryptonContextMenuItemBase visible, KryptonContextMenuItemBase target)
        {
            bool previous = false;

            // Only search if the target itself is visible
            if (target.Visible)
            {
                // Check all items before the target
                foreach (KryptonContextMenuItemBase item in _kryptonContextMenu.Items)
                {
                    // Finish when we reach the target
                    if (item == target)
                        break;

                    // We do not consider existing separators
                    if (!((item is KryptonContextMenuSeparator) ||
                          (item is KryptonContextMenuHeading)))
                    {
                        // If the previous item is visible, then make the parameter visible
                        if (item.Visible)
                        {
                            previous = true;
                            break;
                        }
                    }
                }
            }

            visible.Visible = previous;
        }

        private void OnColumnsTrackingColor(object sender, ColorEventArgs e)
        {
            OnTrackingColor(new ColorEventArgs(e.Color));
        }

        private void OnColumnsSelectedColorChanged(object sender, ColorEventArgs e)
        {
            SelectedColor = e.Color;
        }

        private void OnClickNoColor(object sender, EventArgs e)
        {
            SelectedColor = Color.Empty;
        }

        private void OnClickMoreColors(object sender, EventArgs e)
        {
            // Give user a chance to cancel showing the standard more colors dialog
            CancelEventArgs cea = new CancelEventArgs();
            OnMoreColors(cea);

            // If not instructed to cancel then...
            if (!cea.Cancel)
            {
                // Use a standard color dialog for the selection of custom colors
                ColorDialog cd = new ColorDialog();
                cd.Color = SelectedColor;
                cd.FullOpen = true;

                // Only if user selected a value do we want to use it
                if (cd.ShowDialog() == DialogResult.OK)
                    SelectedColor = cd.Color;
            }
        }

        private void OnKryptonContextMenuClosed(object sender, EventArgs e)
        {
            KryptonContextMenu kcm = (KryptonContextMenu)sender;
            kcm.Closed -= new ToolStripDropDownClosedEventHandler(OnKryptonContextMenuClosed);

            // Fire any associated finish delegate
            if (_kcmFinishDelegate != null)
            {
                _kcmFinishDelegate(this, e);
                _kcmFinishDelegate = null;
            }

            // Unhook from item events
            HookContextMenuEvents(_kryptonContextMenu.Items, false);
        }
        #endregion
    }
}
