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
    /// Represents a ribbon group cluster button.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupClusterButton), "ToolboxBitmaps.KryptonRibbonGroupClusterButton.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupClusterButtonDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("Click")]
    [DefaultProperty("ButtonType")]
    public class KryptonRibbonGroupClusterButton : KryptonRibbonGroupItem
    {
        #region Static Fields
        private static readonly Image _defaultButtonImageSmall = Properties.Resources.ButtonImageSmall;
        #endregion

        #region Instance Fields
        private bool _enabled;
        private bool _visible;
        private bool _checked;
        private string _textLine;
        private string _keyTip;
        private string _toolTipTitle;
        private string _toolTipBody;
        private Image _toolTipImage;
        private Image _imageSmall;
        private Color _toolTipImageTransparentColor;
        private LabelStyle _toolTipStyle;
        private Keys _shortcutKeys;
        private GroupItemSize _itemSizeMax;
        private GroupItemSize _itemSizeMin;
        private GroupItemSize _itemSizeCurrent;
        private KryptonCommand _command;
        private GroupButtonType _buttonType;
        private ContextMenuStrip _contextMenuStrip;
        private KryptonContextMenu _kryptonContextMenu;
        private EventHandler _kcmFinishDelegate;
        private ViewBase _clusterButtonView;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the button is clicked.")]
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the drop down button type is pressed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the drop down button type is pressed.")]
        public event EventHandler<ContextMenuArgs> DropDown;

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
        /// Initialise a new instance of the KryptonRibbonGroupClusterButton class.
        /// </summary>
        public KryptonRibbonGroupClusterButton()
        {
            // Default fields
            _enabled = true;
            _visible = true;
            _checked = false;
            _textLine = string.Empty;
            _keyTip = "B";
            _shortcutKeys = Keys.None;
            _itemSizeMax = GroupItemSize.Medium;
            _itemSizeMin = GroupItemSize.Small;
            _itemSizeCurrent = GroupItemSize.Medium;
            _imageSmall = _defaultButtonImageSmall;
            _buttonType = GroupButtonType.Push;
            _contextMenuStrip = null;
            _kryptonContextMenu = null;
            _toolTipImageTransparentColor = Color.Empty;
            _toolTipTitle = string.Empty;
            _toolTipBody = string.Empty;
            _toolTipStyle = LabelStyle.SuperTip;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the display text line for the button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Button display text line.")]
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
        /// Gets and sets the key tip for the ribbon group cluster button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group cluster button key tip.")]
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
        /// Gets and sets the small button image.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Small button image.")]
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
        /// Gets and sets the visible state of the cluster button.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the cluster button is visible or hidden.")]
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
        /// Make the ribbon group visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon group hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the group entry.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group button is enabled.")]
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
        /// Gets and sets the checked state of the group entry.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group button is checked.")]
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
        /// Gets and sets the operation of the group button.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines how the group button operation.")]
        [DefaultValue(typeof(GroupButtonType), "Push")]
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
        [Description("Shortcut key combination to fire click event of the cluster button.")]
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
        /// Gets and sets the tooltip label style for group cluster button.
        /// </summary>
        [Category("Appearance")]
        [Description("Tooltip style for the group cluster button.")]
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
        /// Gets and sets the context strip for showing when the button is pressed.
        /// </summary>
        [Category("Behavior")]
        [Description("Context menu strip to be shown when the button is pressed.")]
        [DefaultValue(null)]
        public ContextMenuStrip ContextMenuStrip
        {
            get { return _contextMenuStrip; }
            
            set 
            {
                if (value != _contextMenuStrip)
                {
                    _contextMenuStrip = value;
                    OnPropertyChanged("ContextMenuStrip");
                }
            }
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu for showing when the button is pressed.
        /// </summary>
        [Category("Behavior")]
        [Description("KryptonContextMenu to be shown when the button is pressed.")]
        [DefaultValue(null)]
        public KryptonContextMenu KryptonContextMenu
        {
            get { return _kryptonContextMenu; }

            set
            {
                if (value != _kryptonContextMenu)
                {
                    _kryptonContextMenu = value;
                    OnPropertyChanged("KryptonContextMenu");
                }
            }
        }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the cluster button.")]
        [DefaultValue(null)]
        public KryptonCommand KryptonCommand
        {
            get { return _command; }

            set
            {
                if (_command != value)
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
            return new ViewDrawRibbonGroupClusterButton(ribbon, this, needPaint);
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
        public ViewBase ClusterButtonView
        {
            get { return _clusterButtonView; }
            set { _clusterButtonView = value; }
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
                        if (KryptonContextMenu != null)
                        {
                            ContextMenuArgs contextArgs = new ContextMenuArgs(KryptonContextMenu);

                            // Generate an event giving a chance for the krypton context menu strip to 
                            // be shown to be provided/modified or the action even to be cancelled
                            if (DropDown != null)
                                DropDown(this, contextArgs);

                            // If user did not cancel and there is still a krypton context menu strip to show
                            if (!contextArgs.Cancel && (contextArgs.KryptonContextMenu != null))
                            {
                                Rectangle screenRect = Rectangle.Empty;

                                // Convert the view for the button into screen coordinates
                                if ((Ribbon != null) && (ClusterButtonView != null))
                                    screenRect = Ribbon.ViewRectangleToScreen(ClusterButtonView);

                                if (CommonHelper.ValidKryptonContextMenu(contextArgs.KryptonContextMenu))
                                {
                                    // Cache the finish delegate to call when the menu is closed
                                    _kcmFinishDelegate = finishDelegate;

                                    // Show at location we were provided, but need to convert to screen coordinates
                                    contextArgs.KryptonContextMenu.Closed += new ToolStripDropDownClosedEventHandler(OnKryptonContextMenuClosed);
                                    if (contextArgs.KryptonContextMenu.Show(this, new Point(screenRect.X, screenRect.Bottom + 1)))
                                        fireDelegate = false;
                                }
                            }
                        }
                        else if (ContextMenuStrip != null)
                        {
                            ContextMenuArgs contextArgs = new ContextMenuArgs(ContextMenuStrip);

                            // Generate an event giving a chance for the context menu strip to be
                            // shown to be provided/modified or the action even to be cancelled
                            if (DropDown != null)
                                DropDown(this, contextArgs);

                            // If user did not cancel and there is still a context menu strip to show
                            if (!contextArgs.Cancel && (contextArgs.ContextMenuStrip != null))
                            {
                                Rectangle screenRect = Rectangle.Empty;

                                // Convert the view for the button into screen coordinates
                                if ((Ribbon != null) && (ClusterButtonView != null))
                                    screenRect = Ribbon.ViewRectangleToScreen(ClusterButtonView);

                                if (CommonHelper.ValidContextMenuStrip(contextArgs.ContextMenuStrip))
                                {
                                    // Do not fire the delegate in this routine, wait for the popup manager to show it
                                    fireDelegate = false;

                                    //...show the context menu below and at th left of the button
                                    VisualPopupManager.Singleton.ShowContextMenuStrip(contextArgs.ContextMenuStrip,
                                                                                      new Point(screenRect.X, screenRect.Bottom + 1),
                                                                                      finishDelegate);
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
                    if (KryptonContextMenu != null)
                        if (KryptonContextMenu.ProcessShortcut(keyData))
                            return true;

                    if (ContextMenuStrip != null)
                        if (CommonHelper.CheckContextMenuForShortcut(ContextMenuStrip, ref msg, keyData))
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
        }
        #endregion
    }
}
