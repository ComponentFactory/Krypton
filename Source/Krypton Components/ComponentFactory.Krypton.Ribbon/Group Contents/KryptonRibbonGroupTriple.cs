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
    /// Represents a ribbon group triple container.
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(KryptonRibbonGroupTriple), "ToolboxBitmaps.KryptonRibbonGroupTriple.bmp")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTripleDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    [DefaultProperty("Visible")]
    public class KryptonRibbonGroupTriple : KryptonRibbonGroupContainer
    {
        #region Instance Fields
        private KryptonRibbonGroupTripleCollection _ribbonTripleItems;
        private GroupItemSize _itemSizeMax;
        private GroupItemSize _itemSizeMin;
        private GroupItemSize _itemSizeCurrent;
        private RibbonItemAlignment _itemAlignment;
        private ViewBase _tripleView;
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
        /// Occurs when the design time wants to add a checkbox.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddCheckBox;

        /// <summary>
        /// Occurs when the design time wants to add a radio button.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddRadioButton;

        /// <summary>
        /// Occurs when the design time wants to add a label.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddLabel;

        /// <summary>
        /// Occurs when the design time wants to add a custom control.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddCustomControl;

        /// <summary>
        /// Occurs when the design time wants to add a text box.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddTextBox;

        /// <summary>
        /// Occurs when the design time wants to add a masked text box.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddMaskedTextBox;

        /// <summary>
        /// Occurs when the design time wants to add a rich text box.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddRichTextBox;

        /// <summary>
        /// Occurs when the design time wants to add a combobox.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddComboBox;
        
        /// <summary>
        /// Occurs when the design time wants to add a numeric up down.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddNumericUpDown;

        /// <summary>
        /// Occurs when the design time wants to add a domain up down.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddDomainUpDown;

        /// <summary>
        /// Occurs when the design time wants to add a date time picker.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddDateTimePicker;

        /// <summary>
        /// Occurs when the design time wants to add a track bar.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddTrackBar;

        /// <summary>
        /// Occurs when the design time context menu is requested.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event MouseEventHandler DesignTimeContextMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialise a new instance of the KryptonRibbonGroupTriple class.
        /// </summary>
        public KryptonRibbonGroupTriple()
        {
            // Default fields
            _visible = true;
            _itemSizeMax = GroupItemSize.Large;
            _itemSizeMin = GroupItemSize.Small;
            _itemSizeCurrent = GroupItemSize.Large;
            _itemAlignment = RibbonItemAlignment.Near;

            // Create collection for holding triple items
            _ribbonTripleItems = new KryptonRibbonGroupTripleCollection();
            _ribbonTripleItems.Clearing += new EventHandler(OnRibbonGroupTripleClearing);
            _ribbonTripleItems.Cleared += new EventHandler(OnRibbonGroupTripleCleared);
            _ribbonTripleItems.Inserted += new TypedHandler<KryptonRibbonGroupItem>(OnRibbonGroupTripleInserted);
            _ribbonTripleItems.Removed += new TypedHandler<KryptonRibbonGroupItem>(OnRibbonGroupTripleRemoved);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of per-item resources
                foreach (KryptonRibbonGroupItem item in Items)
                    item.Dispose();
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
        public override KryptonRibbon Ribbon
        {
            get { return base.Ribbon; }
            
            set 
            { 
                base.Ribbon = value;

                // Forward the reference to all children (just in case the children
                // are added before the this object is added to the owner)
                foreach (KryptonRibbonGroupItem item in _ribbonTripleItems)
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
                foreach (KryptonRibbonGroupItem item in _ribbonTripleItems)
                    item.RibbonTab = value;
            }
        }

        /// <summary>
        /// Gets and sets how to align items in the medium and small item sizes.
        /// </summary>
        [Category("Visuals")]
        [Description("How to align items in medium and small item sizes.")]
        [DefaultValue(typeof(RibbonItemAlignment), "Near")]
        public RibbonItemAlignment ItemAlignment
        {
            get { return _itemAlignment; }
            
            set             
            {
                if (_itemAlignment != value)
                {
                    _itemAlignment = value;
                    OnPropertyChanged("ItemAlignment");
                }
            }
        }

        /// <summary>
        /// Gets and sets the visible state of the triple group container.
        /// </summary>
        [Bindable(true)]
        [Category("Behavior")]
        [Description("Determines whether the triple group container is visible or hidden.")]
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
        /// Gets and sets the maximum allowed size of the container.
        /// </summary>
        [Category("Visuals")]
        [Description("Maximum size of items placed in the triple container.")]
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
        /// Gets and sets the minimum allowed size of the container.
        /// </summary>
        [Category("Visuals")]
        [Description("Minimum size of items placed in the triple container.")]
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

                    // Ensure the minimum size is always the same or smaller than the max
                    switch (_itemSizeMax)
                    {
                        case GroupItemSize.Medium:
                            if (_itemSizeMin == GroupItemSize.Large)
                                _itemSizeMin = GroupItemSize.Medium;
                            break;
                        case GroupItemSize.Small:
                            if (_itemSizeMin != GroupItemSize.Small)
                                _itemSizeMin = GroupItemSize.Small;
                            break;
                    }

                    // Update all contained elements to reflect the same sizing
                    foreach (IRibbonGroupItem item in Items)
                        item.ItemSizeMaximum = value;

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

                    // Ensure the maximum size is always the same or larger than the min
                    switch (_itemSizeMin)
                    {
                        case GroupItemSize.Large:
                            if (_itemSizeMax != GroupItemSize.Large)
                                _itemSizeMax = GroupItemSize.Large;
                            break;
                        case GroupItemSize.Medium:
                            if (_itemSizeMax == GroupItemSize.Small)
                                _itemSizeMax = GroupItemSize.Medium;
                            break;
                    }

                    // Update all contained elements to reflect the same sizing
                    foreach (IRibbonGroupItem item in Items)
                        item.ItemSizeMinimum = value;

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

                    // Update all contained elements to reflect the same sizing
                    foreach (IRibbonGroupItem item in Items)
                        item.ItemSizeCurrent = value;

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
            return new ViewLayoutRibbonGroupTriple(ribbon, this, needPaint);
        }

        /// <summary>
        /// Gets the collection of ribbon group triple items.
        /// </summary>
        [Category("Visuals")]
        [Description("Collection of ribbon group triple items.")]
        [MergableProperty(false)]
        [Editor("ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTripleCollectionEditor, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonRibbonGroupTripleCollection Items
        {
            get { return _ribbonTripleItems; }
        }

        /// <summary>
        /// Gets an array of all the contained components.
        /// </summary>
        /// <returns>Array of child components.</returns>
        public override Component[] GetChildComponents()
        {
            Component[] array = new Component[Items.Count];
            _ribbonTripleItems.CopyTo(array, 0);
            return array;
        }

        /// <summary>
        /// Internal design time properties.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public ViewBase TripleView
        {
            get { return _tripleView; }
            set { _tripleView = value; }
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

        internal void OnDesignTimeAddCheckBox()
        {
            if (DesignTimeAddCheckBox != null)
                DesignTimeAddCheckBox(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddRadioButton()
        {
            if (DesignTimeAddRadioButton != null)
                DesignTimeAddRadioButton(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddLabel()
        {
            if (DesignTimeAddLabel != null)
                DesignTimeAddLabel(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddCustomControl()
        {
            if (DesignTimeAddCustomControl != null)
                DesignTimeAddCustomControl(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddTextBox()
        {
            if (DesignTimeAddTextBox != null)
                DesignTimeAddTextBox(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddMaskedTextBox()
        {
            if (DesignTimeAddMaskedTextBox != null)
                DesignTimeAddMaskedTextBox(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddRichTextBox()
        {
            if (DesignTimeAddRichTextBox != null)
                DesignTimeAddRichTextBox(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddComboBox()
        {
            if (DesignTimeAddComboBox != null)
                DesignTimeAddComboBox(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddNumericUpDown()
        {
            if (DesignTimeAddNumericUpDown != null)
                DesignTimeAddNumericUpDown(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddDomainUpDown()
        {
            if (DesignTimeAddDomainUpDown != null)
                DesignTimeAddDomainUpDown(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddDateTimePicker()
        {
            if (DesignTimeAddDateTimePicker != null)
                DesignTimeAddDateTimePicker(this, EventArgs.Empty);
        }

        internal void OnDesignTimeAddTrackBar()
        {
            if (DesignTimeAddTrackBar != null)
                DesignTimeAddTrackBar(this, EventArgs.Empty);
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
                if (item.ProcessCmdKey(ref msg, keyData))
                    return true;

            return false;
        }
        #endregion

        #region Private
        private void OnRibbonGroupTripleClearing(object sender, EventArgs e)
        {
            // Remove the back references
            foreach (IRibbonGroupItem item in _ribbonTripleItems)
            {
                item.Ribbon = null;
                item.RibbonTab = null;
                item.RibbonContainer = null;
            }
        }

        private void OnRibbonGroupTripleCleared(object sender, EventArgs e)
        {
            // Only need to update display if this tab is selected
            if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
                Ribbon.PerformNeedPaint(true);
        }

        private void OnRibbonGroupTripleInserted(object sender, TypedCollectionEventArgs<KryptonRibbonGroupItem> e)
        {
            // Setup the back references
            e.Item.Ribbon = Ribbon;
            e.Item.RibbonTab = RibbonTab;
            e.Item.RibbonContainer = this;

            // Force the child item to have same sizing as the whole group
            e.Item.ItemSizeMaximum = ItemSizeMaximum;
            e.Item.ItemSizeMinimum = ItemSizeMinimum;
            e.Item.ItemSizeCurrent = ItemSizeCurrent;

            // Only need to update display if this tab is selected and the group is visible
            if ((Ribbon != null) && (RibbonTab != null) && (Ribbon.SelectedTab == RibbonTab))
                Ribbon.PerformNeedPaint(true);
        }

        private void OnRibbonGroupTripleRemoved(object sender, TypedCollectionEventArgs<KryptonRibbonGroupItem> e)
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
