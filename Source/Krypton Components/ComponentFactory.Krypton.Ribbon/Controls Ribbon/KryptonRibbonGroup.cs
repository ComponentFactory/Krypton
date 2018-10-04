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
    /// Represents a single ribbon group.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroup), "ToolboxBitmaps.KryptonRibbonGroup.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DefaultEvent("DialogBoxLauncherClick")]
    [DefaultProperty("TextLine1")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonRibbonGroup : Component
    {
        #region Static Fields
        private static readonly Image _defaultGroupImage = Properties.Resources.GroupImageDefault;
        #endregion

        #region Instance Fields
        private object _tag;
        private bool _visible;
        private bool _allowCollapsed;
        private bool _isCollapsed;
        private Image _image;
        private string _textLine1;
        private string _textLine2;
        private string _keyTipGroup;
        private string _keyTipDialogLauncher;
        private bool _dialogBoxLauncher;
        private bool _showingAsPopup;
        private int _minimumWidth;
        private int _maximumWidth;
        private KryptonRibbon _ribbon;
        private KryptonRibbonTab _ribbonTab;
        private KryptonRibbonGroupContainerCollection _ribbonGroupItems;
        private ViewBase _groupView;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the dialog box launcher button is clicked.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the dialog box launcher button is clicked.")]
        public event EventHandler DialogBoxLauncherClick;

        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs after the value of a property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the design time wants to add a triple.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddTriple;

        /// <summary>
        /// Occurs when the design time wants to add a lines.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddLines;

        /// <summary>
        /// Occurs when the design time wants to add a separator.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddSeparator;
        
        /// <summary>
        /// Occurs when the design time wants to add a gallery.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddGallery;
        
        /// <summary>
        /// Occurs when the design time context menu is requested.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event MouseEventHandler DesignTimeContextMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonTab class.
        /// </summary>
        public KryptonRibbonGroup()
        {
            // Default fields
            _image = _defaultGroupImage;
            _textLine1 = "Group";
            _textLine2 = string.Empty;
            _keyTipGroup = "G";
            _keyTipDialogLauncher = "D";
            _visible = true;
            _allowCollapsed = true;
            _dialogBoxLauncher = true;
            _isCollapsed = false;
            _minimumWidth = -1;
            _maximumWidth = -1;

            // Create collection for holding child items
            _ribbonGroupItems = new KryptonRibbonGroupContainerCollection();
            _ribbonGroupItems.Clearing += new EventHandler(OnRibbonGroupItemsClearing);
            _ribbonGroupItems.Cleared += new EventHandler(OnRibbonGroupItemsCleared);
            _ribbonGroupItems.Inserted += new TypedHandler<KryptonRibbonGroupContainer>(OnRibbonGroupItemsInserted);
            _ribbonGroupItems.Removed += new TypedHandler<KryptonRibbonGroupContainer>(OnRibbonGroupItemsRemoved);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of per-group-container resources
                foreach (KryptonRibbonGroupContainer container in Items)
                    container.Dispose();
            }

            base.Dispose(disposing);
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

            internal set
            {
                _ribbon = value;

                // Forward the reference to all children (just in case the children
                // are added before the this object is added to the owner)
                foreach (KryptonRibbonGroupItem item in _ribbonGroupItems)
                    item.Ribbon = value;
            }
        }

        /// <summary>
        /// Gets access to the owning ribbon tab.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonRibbonTab RibbonTab
        {
            get { return _ribbonTab; }

            internal set
            {
                _ribbonTab = value;

                // Forward the reference to all children (just in case the children
                // are added before the this object is added to the owner)
                foreach (KryptonRibbonGroupItem item in _ribbonGroupItems)
                    item.RibbonTab = value;
            }
        }

        /// <summary>
        /// Gets and sets the display text line 1 for the ribbon group.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group display text line 1.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        [DefaultValue("Group")]
        public string TextLine1
        {
            get { return _textLine1; }

            set
            {
                // We never allow an empty text value
                if (string.IsNullOrEmpty(value))
                    value = "Group";

                if (value != _textLine1)
                {
                    _textLine1 = value;
                    OnPropertyChanged("TextLine1");
                }
            }
        }

        /// <summary>
        /// Gets and sets the display text line 2 for the ribbon group.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group display text line 2.")]
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
        /// Gets and sets the key tip used when group is collapsed.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group key tip used when collapsed.")]
        [DefaultValue("G")]
        public string KeyTipGroup
        {
            get { return _keyTipGroup; }

            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "G";

                _keyTipGroup = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets and sets the key tip used for group dialog box launcher.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Ribbon group key tip used for dialog box launcher.")]
        [DefaultValue("D")]
        public string KeyTipDialogLauncher
        {
            get { return _keyTipDialogLauncher; }

            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "D";

                _keyTipDialogLauncher = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets and sets the ribbon group image when collapsed.
        /// </summary>
        [Bindable(true)]
        [Localizable(true)]
        [Category("Appearance")]
        [Description("Group image when collapsed.")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Image Image
        {
            get { return _image; }

            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged("Image");
                }
            }
        }

        private bool ShouldSerializeImage()
        {
            return Image != _defaultGroupImage;
        }

        /// <summary>
        /// Gets and sets the visible state of the ribbon group.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the ribbon group is visible or hidden.")]
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
        /// Gets and sets the display of a dialog box launcher button.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the ribbon group has a dialog box launcher button.")]
        [DefaultValue(true)]
        public bool DialogBoxLauncher
        {
            get { return _dialogBoxLauncher; }

            set
            {
                if (value != _dialogBoxLauncher)
                {
                    _dialogBoxLauncher = value;
                    OnPropertyChanged("DialogBoxLauncher");
                }
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if the ribbon group is allowed to be collapsed.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the ribbon group is allowed to be collapsed.")]
        [DefaultValue(true)]
        public bool AllowCollapsed
        {
            get { return _allowCollapsed; }

            set
            {
                if (value != _allowCollapsed)
                {
                    _allowCollapsed = value;
                    OnPropertyChanged("AllowCollapsed");
                }
            }
        }

        /// <summary>
        /// Gets and sets the minimum width allowed, with -1 removing this limitation
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Ribbon group minimum width, with -1 removing this limitation.")]
        [DefaultValue(-1)]
        public int MinimumWidth
        {
            get { return _minimumWidth; }

            set
            {
                if (value != _minimumWidth)
                {
                    _minimumWidth = value;
                    OnPropertyChanged("MinimumWidth");
                }
            }
        }

        /// <summary>
        /// Gets and sets the maximum width allowed, with -1 removing this limitation
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Ribbon group maximum width, with -1 removing this limitation.")]
        [DefaultValue(-1)]
        public int MaximumWidth
        {
            get { return _maximumWidth; }

            set
            {
                if (value != _maximumWidth)
                {
                    _maximumWidth = value;
                    OnPropertyChanged("MaximumWidth");
                }
            }
        }

        /// <summary>
        /// Gets the collection of ribbon group items.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of ribbon group items.")]
        [MergableProperty(false)]
        [Editor("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainerCollectionEditor, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonRibbonGroupContainerCollection Items
        {
            get { return _ribbonGroupItems; }
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

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase GroupView
        {
            get { return _groupView; }
            set { _groupView = value; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the DialogBoxLauncherClick event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        internal protected virtual void OnDialogBoxLauncherClick(EventArgs e)
        {
            // Perform processing that is common to any action that would dismiss
            // any popup controls such as the showing minimized group popup
            if (Ribbon!= null)
                Ribbon.ActionOccured();

            if (DialogBoxLauncherClick != null)
                DialogBoxLauncherClick(this, e);
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
        internal bool IsCollapsed
        {
            get { return _isCollapsed; }
            set { _isCollapsed = value; }
        }

        internal bool ShowingAsPopup
        {
            get { return _showingAsPopup; }
            set { _showingAsPopup = value; }
        }

        internal void OnDesignTimeAddTriple()
        {
            if (DesignTimeAddTriple != null)
                DesignTimeAddTriple(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddLines()
        {
            if (DesignTimeAddLines != null)
                DesignTimeAddLines(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddSeparator()
        {
            if (DesignTimeAddSeparator != null)
                DesignTimeAddSeparator(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddGallery()
        {
            if (DesignTimeAddGallery != null)
                DesignTimeAddGallery(this, EventArgs.Empty);
        }

        internal void OnDesignTimeContextMenu(MouseEventArgs e)
        {
            if (DesignTimeContextMenu != null)
                DesignTimeContextMenu(this, e);
        }

        internal bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Ask the containers to check for command key processing
            foreach (KryptonRibbonGroupContainer container in Items)
                if (container.Visible && container.ProcessCmdKey(ref msg, keyData))
                    return true;

            return false;
        }
        #endregion

        #region Private
        private void OnRibbonGroupItemsClearing(object sender, EventArgs e)
        {
            // Remove the back references
            foreach (KryptonRibbonGroupContainer container in _ribbonGroupItems)
            {
                container.Ribbon = null;
                container.RibbonTab = null;
                container.RibbonGroup = null;
            }
        }

        private void OnRibbonGroupItemsCleared(object sender, EventArgs e)
        {
            // Only need to update display if this tab is selected
            if ((_ribbon != null) && (_ribbonTab != null) && (_ribbon.SelectedTab == _ribbonTab))
                _ribbon.PerformNeedPaint(true);
        }

        private void OnRibbonGroupItemsInserted(object sender, TypedCollectionEventArgs<KryptonRibbonGroupContainer> e)
        {
            // Setup the back references
            e.Item.Ribbon = _ribbon;
            e.Item.RibbonTab = _ribbonTab;
            e.Item.RibbonGroup = this;

            // Only need to update display if this tab is selected and the group is visible
            if ((_ribbon != null) && (_ribbonTab != null) && (_ribbon.SelectedTab == _ribbonTab))
                _ribbon.PerformNeedPaint(true);
        }

        private void OnRibbonGroupItemsRemoved(object sender, TypedCollectionEventArgs<KryptonRibbonGroupContainer> e)
        {
            // Remove the back references
            e.Item.Ribbon = null;
            e.Item.RibbonTab = null;
            e.Item.RibbonGroup = null;

            // Only need to update display if this tab is selected and the group was visible
            if ((_ribbon != null) && (_ribbonTab != null) && (_ribbon.SelectedTab == _ribbonTab))
                _ribbon.PerformNeedPaint(true);
        }
        #endregion
    }
}
