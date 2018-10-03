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
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class KryptonRibbonGroupGalleryDesigner : ComponentDesigner, IKryptonDesignObject
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonGroupGallery _ribbonGallery;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _deleteGalleryVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
        private ToolStripMenuItem _enabledMenu;
        private ToolStripMenuItem _maximumSizeMenu;
        private ToolStripMenuItem _maximumLMenu;
        private ToolStripMenuItem _maximumMMenu;
        private ToolStripMenuItem _maximumSMenu;
        private ToolStripMenuItem _minimumSizeMenu;
        private ToolStripMenuItem _minimumLMenu;
        private ToolStripMenuItem _minimumMMenu;
        private ToolStripMenuItem _minimumSMenu;
        private ToolStripMenuItem _moveFirstMenu;
        private ToolStripMenuItem _movePreviousMenu;
        private ToolStripMenuItem _moveNextMenu;
        private ToolStripMenuItem _moveLastMenu;
        private ToolStripMenuItem _deleteGalleryMenu;
        private bool _visible;
        private bool _enabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupGalleryDesigner class.
        /// </summary>
        public KryptonRibbonGroupGalleryDesigner()
        {
        }
        #endregion

        #region Public
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            Debug.Assert(component != null);

            // Validate the parameter reference
            if (component == null) throw new ArgumentNullException("component");

            // Let base class do standard stuff
            base.Initialize(component);

            // Cast to correct type
            _ribbonGallery = (KryptonRibbonGroupGallery)component;
            _ribbonGallery.GalleryDesigner = this;

            // Update designer properties with actual starting values
            Visible = _ribbonGallery.Visible;
            Enabled = _ribbonGallery.Enabled;

            // Update visible/enabled to always be showing/enabled at design time
            _ribbonGallery.Visible = true;
            _ribbonGallery.Enabled = true;

            // Tell the embedded gallery it is in design mode
            _ribbonGallery.Gallery.InRibbonDesignMode = true;

            // Hook into events
            _ribbonGallery.DesignTimeContextMenu += new MouseEventHandler(OnContextMenu);

            // Get access to the services
            _designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            // We need to know when we are being removed/changed
            _changeService.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);
        }

        /// <summary>
        /// Gets the design-time verbs supported by the component that is associated with the designer.
        /// </summary>
        public override DesignerVerbCollection Verbs
        {
            get
            {
                UpdateVerbStatus();
                return _verbs;
            }
        }

        /// <summary>
        /// Gets and sets if the object is enabled.
        /// </summary>
        public bool DesignEnabled 
        { 
            get { return Enabled; }
            set { Enabled = value; }
        }

        /// <summary>
        /// Gets and sets if the object is visible.
        /// </summary>
        public bool DesignVisible 
        {
            get { return Visible; }
            set { Visible = value; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Releases all resources used by the component. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    // Unhook from events
                    _ribbonGallery.DesignTimeContextMenu -= new MouseEventHandler(OnContextMenu);
                    _changeService.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
                }
            }
            finally
            {
                // Must let base class do standard stuff
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Adjusts the set of properties the component exposes through a TypeDescriptor.
        /// </summary>
        /// <param name="properties">An IDictionary containing the properties for the class of the component.</param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);

            // Setup the array of properties we override
            Attribute[] attributes = new Attribute[0];
            string[] strArray = new string[] { "Visible", "Enabled" };

            // Adjust our list of properties
            for (int i = 0; i < strArray.Length; i++)
            {
                PropertyDescriptor descrip = (PropertyDescriptor)properties[strArray[i]];
                if (descrip != null)
                    properties[strArray[i]] = TypeDescriptor.CreateProperty(typeof(KryptonRibbonGroupGalleryDesigner), descrip, attributes);
            }
        }
        #endregion

        #region Internal
        internal bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        internal bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        #endregion

        #region Implementation
        private void ResetVisible()
        {
            Visible = true;
        }

        private bool ShouldSerializeVisible()
        {
            return !Visible;
        }

        private void ResetEnabled()
        {
            Enabled = true;
        }

        private bool ShouldSerializeEnabled()
        {
            return !Enabled;
        }

        private void UpdateVerbStatus()
        {
            // Create verbs first time around
            if (_verbs == null)
            {
                _verbs = new DesignerVerbCollection();
                _toggleHelpersVerb = new DesignerVerb("Toggle Helpers", new EventHandler(OnToggleHelpers));
                _moveFirstVerb = new DesignerVerb("Move Gallery First", new EventHandler(OnMoveFirst));
                _movePrevVerb = new DesignerVerb("Move Gallery Previous", new EventHandler(OnMovePrevious));
                _moveNextVerb = new DesignerVerb("Move Gallery Next", new EventHandler(OnMoveNext));
                _moveLastVerb = new DesignerVerb("Move Gallery Last", new EventHandler(OnMoveLast));
                _deleteGalleryVerb = new DesignerVerb("Delete Gallery", new EventHandler(OnDeleteGallery));
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, 
                                                     _moveNextVerb, _moveLastVerb, _deleteGalleryVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;

            if ((_ribbonGallery != null) && 
                (_ribbonGallery.Ribbon != null) && 
                _ribbonGallery.RibbonGroup.Items.Contains(_ribbonGallery))
            {
                moveFirst = (_ribbonGallery.RibbonGroup.Items.IndexOf(_ribbonGallery) > 0);
                movePrev = (_ribbonGallery.RibbonGroup.Items.IndexOf(_ribbonGallery) > 0);
                moveNext = (_ribbonGallery.RibbonGroup.Items.IndexOf(_ribbonGallery) < (_ribbonGallery.RibbonGroup.Items.Count - 1));
                moveLast = (_ribbonGallery.RibbonGroup.Items.IndexOf(_ribbonGallery) < (_ribbonGallery.RibbonGroup.Items.Count - 1));
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
                _ribbonGallery.Ribbon.InDesignHelperMode = !_ribbonGallery.Ribbon.InDesignHelperMode;
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) &&
                (_ribbonGallery.Ribbon != null) &&
                 _ribbonGallery.RibbonGroup.Items.Contains(_ribbonGallery))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupGallery MoveFirst");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonGallery.RibbonGroup)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the gallery
                    KryptonRibbonGroup ribbonGroup = _ribbonGallery.RibbonGroup;
                    ribbonGroup.Items.Remove(_ribbonGallery);
                    ribbonGroup.Items.Insert(0, _ribbonGallery);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    if (transaction != null)
                        transaction.Commit();
                }
            }
        }

        private void OnMovePrevious(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) &&
                (_ribbonGallery.Ribbon != null) &&
                 _ribbonGallery.RibbonGroup.Items.Contains(_ribbonGallery))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupGallery MovePrevious");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonGallery.RibbonGroup)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the gallery
                    KryptonRibbonGroup ribbonGroup = _ribbonGallery.RibbonGroup;
                    int index = ribbonGroup.Items.IndexOf(_ribbonGallery) - 1;
                    index = Math.Max(index, 0);
                    ribbonGroup.Items.Remove(_ribbonGallery);
                    ribbonGroup.Items.Insert(index, _ribbonGallery);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    if (transaction != null)
                        transaction.Commit();
                }
            }
        }

        private void OnMoveNext(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) &&
                (_ribbonGallery.Ribbon != null) &&
                 _ribbonGallery.RibbonGroup.Items.Contains(_ribbonGallery))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupGallery MoveNext");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonGallery.RibbonGroup)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the gallery
                    KryptonRibbonGroup ribbonGroup = _ribbonGallery.RibbonGroup;
                    int index = ribbonGroup.Items.IndexOf(_ribbonGallery) + 1;
                    index = Math.Min(index, ribbonGroup.Items.Count - 1);
                    ribbonGroup.Items.Remove(_ribbonGallery);
                    ribbonGroup.Items.Insert(index, _ribbonGallery);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    if (transaction != null)
                        transaction.Commit();
                }
            }
        }

        private void OnMoveLast(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) &&
                (_ribbonGallery.Ribbon != null) &&
                 _ribbonGallery.RibbonGroup.Items.Contains(_ribbonGallery))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupGallery MoveLast");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonGallery.RibbonGroup)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the gallery
                    KryptonRibbonGroup ribbonGroup = _ribbonGallery.RibbonGroup;
                    ribbonGroup.Items.Remove(_ribbonGallery);
                    ribbonGroup.Items.Insert(ribbonGroup.Items.Count, _ribbonGallery);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    if (transaction != null)
                        transaction.Commit();
                }
            }
        }

        private void OnDeleteGallery(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) &&
                (_ribbonGallery.Ribbon != null) &&
                 _ribbonGallery.RibbonGroup.Items.Contains(_ribbonGallery))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupGallery DeleteGallery");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonGallery.RibbonGroup)["Items"];

                    // Remove the ribbon group from the ribbon tab
                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the gallery from the group
                    _ribbonGallery.RibbonGroup.Items.Remove(_ribbonGallery);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonGallery);

                    RaiseComponentChanged(propertyItems, null, null);
                    RaiseComponentChanged(null, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    if (transaction != null)
                        transaction.Commit();
                }
            }
        }

        private void OnEnabled(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
            {
                PropertyDescriptor propertyEnabled = TypeDescriptor.GetProperties(_ribbonGallery)["Enabled"];
                bool oldValue = (bool)propertyEnabled.GetValue(_ribbonGallery);
                bool newValue = !oldValue;
                _changeService.OnComponentChanged(_ribbonGallery, null, oldValue, newValue);
                propertyEnabled.SetValue(_ribbonGallery, newValue);
            }
        }

        private void OnVisible(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
            {
                PropertyDescriptor propertyVisible = TypeDescriptor.GetProperties(_ribbonGallery)["Visible"];
                bool oldValue = (bool)propertyVisible.GetValue(_ribbonGallery);
                bool newValue = !oldValue;
                _changeService.OnComponentChanged(_ribbonGallery, null, oldValue, newValue);
                propertyVisible.SetValue(_ribbonGallery, newValue);
            }
        }

        private void OnMaxLarge(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
            {
            _changeService.OnComponentChanged(_ribbonGallery, null, _ribbonGallery.MaximumSize, GroupItemSize.Large);
                _ribbonGallery.MaximumSize = GroupItemSize.Large;
            }
        }

        private void OnMaxMedium(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonGallery, null, _ribbonGallery.MaximumSize, GroupItemSize.Medium);
                _ribbonGallery.MaximumSize = GroupItemSize.Medium;
            }
        }

        private void OnMaxSmall(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonGallery, null, _ribbonGallery.MaximumSize, GroupItemSize.Small);
                _ribbonGallery.MaximumSize = GroupItemSize.Small;
            }
        }

        private void OnMinLarge(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonGallery, null, _ribbonGallery.MinimumSize, GroupItemSize.Large);
                _ribbonGallery.MinimumSize = GroupItemSize.Large;
            }
        }

        private void OnMinMedium(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonGallery, null, _ribbonGallery.MinimumSize, GroupItemSize.Medium);
                _ribbonGallery.MinimumSize = GroupItemSize.Medium;
            }
        }

        private void OnMinSmall(object sender, EventArgs e)
        {
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonGallery, null, _ribbonGallery.MinimumSize, GroupItemSize.Small);
                _ribbonGallery.MinimumSize = GroupItemSize.Small;
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if ((_ribbonGallery != null) && (_ribbonGallery.Ribbon != null))
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, new EventHandler(OnToggleHelpers));
                    _visibleMenu = new ToolStripMenuItem("Visible", null, new EventHandler(OnVisible));
                    _enabledMenu = new ToolStripMenuItem("Enabled", null, new EventHandler(OnEnabled));
                    _maximumLMenu = new ToolStripMenuItem("Large", null, new EventHandler(OnMaxLarge));
                    _maximumMMenu = new ToolStripMenuItem("Medium", null, new EventHandler(OnMaxMedium));
                    _maximumSMenu = new ToolStripMenuItem("Small", null, new EventHandler(OnMaxSmall));
                    _maximumSizeMenu = new ToolStripMenuItem("Maximum Size");
                    _maximumSizeMenu.DropDownItems.AddRange(new ToolStripItem[] { _maximumLMenu, _maximumMMenu, _maximumSMenu });
                    _minimumLMenu = new ToolStripMenuItem("Large", null, new EventHandler(OnMinLarge));
                    _minimumMMenu = new ToolStripMenuItem("Medium", null, new EventHandler(OnMinMedium));
                    _minimumSMenu = new ToolStripMenuItem("Small", null, new EventHandler(OnMinSmall));
                    _minimumSizeMenu = new ToolStripMenuItem("Minimum Size");
                    _minimumSizeMenu.DropDownItems.AddRange(new ToolStripItem[] { _minimumLMenu, _minimumMMenu, _minimumSMenu });
                    _moveFirstMenu = new ToolStripMenuItem("Move Gallery First", Properties.Resources.MoveFirst, new EventHandler(OnMoveFirst));
                    _movePreviousMenu = new ToolStripMenuItem("Move Gallery Previous", Properties.Resources.MovePrevious, new EventHandler(OnMovePrevious));
                    _moveNextMenu = new ToolStripMenuItem("Move Gallery Next", Properties.Resources.MoveNext, new EventHandler(OnMoveNext));
                    _moveLastMenu = new ToolStripMenuItem("Move Gallery Last", Properties.Resources.MoveLast, new EventHandler(OnMoveLast));
                    _deleteGalleryMenu = new ToolStripMenuItem("Delete Gallery", Properties.Resources.delete2, new EventHandler(OnDeleteGallery));
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, _enabledMenu, _maximumSizeMenu, _minimumSizeMenu, new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _deleteGalleryMenu });
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonGallery.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = Visible;
                _enabledMenu.Checked = Enabled;
                _maximumLMenu.Checked = (_ribbonGallery.MaximumSize == GroupItemSize.Large);
                _maximumMMenu.Checked = (_ribbonGallery.MaximumSize == GroupItemSize.Medium);
                _maximumSMenu.Checked = (_ribbonGallery.MaximumSize == GroupItemSize.Small);
                _minimumLMenu.Checked = (_ribbonGallery.MinimumSize == GroupItemSize.Large);
                _minimumMMenu.Checked = (_ribbonGallery.MinimumSize == GroupItemSize.Medium);
                _minimumSMenu.Checked = (_ribbonGallery.MinimumSize == GroupItemSize.Small);
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;

                // Show the context menu
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonGallery.Ribbon.ViewRectangleToPoint(_ribbonGallery.GalleryView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }
        #endregion
    }
}
