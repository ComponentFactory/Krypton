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
    /// Represents a ribbon group radio button.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupRadioButton), "ToolboxBitmaps.KryptonRibbonGroupRadioButton.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupRadioButtonDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultEvent("CheckedChanged")]
    [DefaultProperty("Checked")]
    public class KryptonRibbonGroupRadioButton : KryptonRibbonGroupItem
    {
        #region Instance Fields
        private bool _enabled;
        private bool _visible;
        private bool _checked;
        private bool _autoCheck;
        private Image _toolTipImage;
        private Color _toolTipImageTransparentColor;
        private LabelStyle _toolTipStyle;
        private string _textLine1;
        private string _textLine2;
        private string _keyTip;
        private string _toolTipTitle;
        private string _toolTipBody;
        private Keys _shortcutKeys;
        private ViewBase _radioButtonView;
        private GroupItemSize _itemSizeMax;
        private GroupItemSize _itemSizeMin;
        private GroupItemSize _itemSizeCurrent;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the radio button is clicked.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the radio button is clicked.")]
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the value of the Checked property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs whenever the Checked property has changed.")]
        public event EventHandler CheckedChanged;

        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs after the value of a property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the design time context menu is requested.
        /// </summary>
        [Category("Design Time")]
        [Description("Occurs when the design time context menu is requested.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event MouseEventHandler DesignTimeContextMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupRadioButton class.
        /// </summary>
        public KryptonRibbonGroupRadioButton()
        {
            // Default fields
            _enabled = true;
            _visible = true;
            _checked = false;
            _autoCheck = true;
            _shortcutKeys = Keys.None;
            _textLine1 = "RadioButton";
            _textLine2 = string.Empty;
            _keyTip = "R";
            _itemSizeMax = GroupItemSize.Large;
            _itemSizeMin = GroupItemSize.Small;
            _itemSizeCurrent = GroupItemSize.Large;
            _toolTipImageTransparentColor = Color.Empty;
            _toolTipTitle = string.Empty;
            _toolTipBody = string.Empty;
            _toolTipStyle = LabelStyle.SuperTip;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the display text line 1 for the radio button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Radio button display text line 1.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("RadioButton")]
        public string TextLine1
        {
            get { return _textLine1; }

            set
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                    value = "RadioButton";

                if (value != _textLine1)
                {
                    _textLine1 = value;
                    OnPropertyChanged("TextLine1");
                }
            }
        }

        /// <summary>
        /// Gets and sets the display text line 2 for the radio button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Radio button display text line 2.")]
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
        /// Gets and sets the key tip for the ribbon group radio button.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group radio button key tip.")]
        [DefaultValue("R")]
        public string KeyTip
        {
            get { return _keyTip; }

            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "R";

                _keyTip = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the radio button.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the radio button is visible or hidden.")]
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
        /// Make the ribbon group radio button visible.
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Make the ribbon group radio button hidden.
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Gets and sets the enabled state of the group radio button entry.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the group radio button is enabled.")]
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
        /// Gets or sets a value indicating if the radio button is in the checked state.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if the radio button is in the checked state.")]
        [DefaultValue(false)]
        public bool Checked
        {
            get { return _checked; }

            set
            {
                if (_checked != value)
                {
                    // Store new values
                    _checked = value;
                    OnPropertyChanged("Checked");

                    if (_checked)
                        AutoUpdateOthers();

                    // Generate events
                    OnCheckedChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if checking this radio button should unheck others in the same group.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if checking this radio button should uncheck others in the same group.")]
        [DefaultValue(true)]
        public bool AutoCheck
        {
            get { return _autoCheck; }
            
            set 
            {
                if (_autoCheck != value)
                {
                    _autoCheck = value;

                    if (_checked)
                        AutoUpdateOthers();
                }
            }
        }

        /// <summary>
        /// Gets and sets the shortcut key combination.
        /// </summary>
        [Localizable(true)]
        [Category("Behavior")]
        [Description("Shortcut key combination to fire click event of the radio button.")]
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
        /// Gets and sets the tooltip label style for group radio button.
        /// </summary>
        [Category("Appearance")]
        [Description("Tooltip style for the group radio button.")]
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
            return new ViewDrawRibbonGroupRadioButton(ribbon, this, needPaint);
        }

        /// <summary>
        /// Generates a Click event for a check box.
        /// </summary>
        public void PerformClick()
        {
            PerformClick(null);
        }

        /// <summary>
        /// Generates a Click event for a radio button.
        /// </summary>
        /// <param name="finishDelegate">Delegate fired during event processing.</param>
        public void PerformClick(EventHandler finishDelegate)
        {
            OnClick(finishDelegate);
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase RadioButtonView
        {
            get { return _radioButtonView; }
            set { _radioButtonView = value; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="finishDelegate">Delegate fired during event processing.</param>
        protected virtual void OnClick(EventHandler finishDelegate)
        {
            bool fireDelegate = true;

            if (!Ribbon.InDesignMode)
            {
                if (Enabled)
                {
                    // Make sure we become checked
                    if (!Checked)
                        Checked = true;

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
                }
            }

            // Do we need to fire a delegate stating the click processing has finished?
            if (fireDelegate && (finishDelegate != null))
                finishDelegate(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the CheckedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanged != null)
                CheckedChanged(this, e);
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
            // Only interested in key processing if this check box definition 
            // is enabled and itself and all parents are also visible
            if (Enabled && ChainVisible)
            {
                // Do we have a shortcut definition for ourself?
                if (ShortcutKeys != Keys.None)
                {
                    // Does it match the incoming key combination?
                    if (ShortcutKeys == keyData)
                    {
                        PerformClick();
                        return true;
                    }
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
        private void AutoUpdateOthers()
        {
            // Only uncheck others if we are checked and in auto check
            if (AutoCheck && Checked)
            {
                // If we are inside a ribbon container and a ribbon group
                if ((RibbonContainer != null) && (RibbonContainer.RibbonGroup != null))
                {
                    // Process each container inside the group
                    foreach (IRibbonGroupContainer container in RibbonContainer.RibbonGroup.Items)
                        AutoUpdateContainer(container);
                }
            }
        }

        private void AutoUpdateContainer(IRibbonGroupContainer Container)
        {
            // Process each component inside the container
            foreach (Component component in Container.GetChildComponents())
            {
                // If the component is itself a container...
                if (component is IRibbonGroupContainer)
                    AutoUpdateContainer(component as IRibbonGroupContainer);
                else
                {
                    // If this is another radio button...
                    if (component is KryptonRibbonGroupRadioButton)
                    {
                        KryptonRibbonGroupRadioButton radioButton = (KryptonRibbonGroupRadioButton)component;

                        // Do not process ourself!
                        if (radioButton != this)
                        {
                            // If the target is checked and allowed to be auto unchecked
                            if (radioButton.AutoCheck && radioButton.Checked)
                                radioButton.Checked = false;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
