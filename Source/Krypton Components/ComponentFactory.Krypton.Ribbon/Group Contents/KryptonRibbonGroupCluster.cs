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
using System.Collections;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Represents a ribbon group container that displays a cluster of buttons.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupCluster), "ToolboxBitmaps.KryptonRibbonGroupCluster.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupClusterDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Visible")]
    public class KryptonRibbonGroupCluster : KryptonRibbonGroupContainer
    {
        #region Instance Fields
        private KryptonRibbonGroupClusterCollection _ribbonClusterItems;
        private GroupItemSize _itemSizeMax;
        private GroupItemSize _itemSizeMin;
        private GroupItemSize _itemSizeCurrent;
        private ViewBase _clusterView;
        private bool _visible;
        #endregion

        #region Events
        /// <summary>
        /// Occurs after the value of a property has changed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs after the value of a property has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the design time wants to add a button.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddButton;

        /// <summary>
        /// Occurs when the design time wants to add a color button.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddColorButton;
        
        /// <summary>
        /// Occurs when the design time context menu is requested.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event MouseEventHandler DesignTimeContextMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupCluster class.
        /// </summary>
        public KryptonRibbonGroupCluster()
        {
            // Default fields
            _itemSizeMax = GroupItemSize.Medium;
            _itemSizeMin = GroupItemSize.Small;
            _itemSizeCurrent = GroupItemSize.Medium;
            _visible = true;

            // Create collection for holding triple items
            _ribbonClusterItems = new KryptonRibbonGroupClusterCollection();
            _ribbonClusterItems.Clearing += new EventHandler(OnRibbonGroupClusterClearing);
            _ribbonClusterItems.Cleared += new EventHandler(OnRibbonGroupClusterCleared);
            _ribbonClusterItems.Inserted += new TypedHandler<KryptonRibbonGroupItem>(OnRibbonGroupClusterInserted);
            _ribbonClusterItems.Removed += new TypedHandler<KryptonRibbonGroupItem>(OnRibbonGroupClusterRemoved);
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets access to the owning ribbon control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override KryptonRibbon Ribbon
        {
            get { return base.Ribbon; }
            
            set 
            { 
                base.Ribbon = value;

                // Forward the reference to all children (just in case the children
                // are added before the this object is added to the owner)
                foreach (KryptonRibbonGroupItem item in _ribbonClusterItems)
                    item.Ribbon = value;
            }
        }

        /// <summary>
        /// Gets access to the owning ribbon tab.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override KryptonRibbonTab RibbonTab
        {
            get { return base.RibbonTab; }

            set 
            { 
                base.RibbonTab = value;

                // Forward the reference to all children (just in case the children
                // are added before the this object is added to the owner)
                foreach (KryptonRibbonGroupItem item in _ribbonClusterItems)
                    item.RibbonTab = value;
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the button cluster container.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the button cluster is visible or hidden.")]
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

                    // Update all contained elements to reflect the same sizing
                    foreach (IRibbonGroupItem item in Items)
                        item.ItemSizeMaximum = _itemSizeMax;

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

                    // Update all contained elements to reflect the same sizing
                    foreach (IRibbonGroupItem item in Items)
                        item.ItemSizeMinimum = _itemSizeMin;

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
        /// Return the spacing gap between the provided previous item and this item.
        /// </summary>
        /// <param name="previousItem">Previous item.</param>
        /// <returns>Pixel gap between previous item and this item.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int ItemGap(IRibbonGroupItem previousItem)
        {
            // We always want 3 pixels space between previous item and us
            return 3;
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
            return new ViewLayoutRibbonGroupCluster(ribbon, this, needPaint);
        }

        /// <summary>
        /// Gets the collection of ribbon group button cluster items.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of ribbon group button cluster items.")]
        [MergableProperty(false)]
        [Editor("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupClusterCollectionEditor, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonRibbonGroupClusterCollection Items
        {
            get { return _ribbonClusterItems; }
        }

        /// <summary>
        /// Gets an array of all the contained components.
        /// </summary>
        /// <returns>Array of child components.</returns>
        public override Component[] GetChildComponents()
        {
            Component[] array = new Component[Items.Count];
            _ribbonClusterItems.CopyTo(array, 0);
            return array;
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase ClusterView
        {
            get { return _clusterView; }
            set { _clusterView = value; }
        }
        #endregion

        #region Protected
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
        internal void OnDesignTimeAddButton()
        {
            if (DesignTimeAddButton != null)
                DesignTimeAddButton(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddColorButton()
        {
            if (DesignTimeAddColorButton != null)
                DesignTimeAddColorButton(this, EventArgs.Empty);
        }

        internal void OnDesignTimeContextMenu(MouseEventArgs e)
        {
            if (DesignTimeContextMenu != null)
                DesignTimeContextMenu(this, e);
        }

        internal override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Ask the containers to check for command key processing
            foreach (KryptonRibbonGroupItem item in Items)
                if (item.Visible && item.ProcessCmdKey(ref msg, keyData))
                    return true;

            return false;
        }
        #endregion

        #region Private
        private void OnRibbonGroupClusterClearing(object sender, EventArgs e)
        {
            // Remove the back references
            foreach (IRibbonGroupItem item in _ribbonClusterItems)
            {
                item.Ribbon = null;
                item.RibbonTab = null;
                item.RibbonContainer = null;
            }
        }

        private void OnRibbonGroupClusterCleared(object sender, EventArgs e)
        {
            // Only need to update display if this tab is selected
            if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
                Ribbon.PerformNeedPaint(true);
        }

        private void OnRibbonGroupClusterInserted(object sender, TypedCollectionEventArgs<KryptonRibbonGroupItem> e)
        {
            // Setup the back references
            e.Item.Ribbon = Ribbon;
            e.Item.RibbonTab = RibbonTab;
            e.Item.RibbonContainer = this;

            // Force the child item to the fixed lines sizing
            e.Item.ItemSizeMaximum = ItemSizeMaximum;
            e.Item.ItemSizeMinimum = ItemSizeMinimum;
            e.Item.ItemSizeCurrent = ItemSizeCurrent;

            // Only need to update display if this tab is selected and the group is visible
            if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
                Ribbon.PerformNeedPaint(true);
        }

        private void OnRibbonGroupClusterRemoved(object sender, TypedCollectionEventArgs<KryptonRibbonGroupItem> e)
        {
            // Remove the back references
            e.Item.Ribbon = null;
            e.Item.RibbonTab = null;
            e.Item.RibbonContainer = null;

            // Only need to update display if this tab is selected and the group was visible
            if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
                Ribbon.PerformNeedPaint(true);
        }
        #endregion
    }
}
