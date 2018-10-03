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
    internal class KryptonRibbonGroupClusterDesigner : ComponentDesigner
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonGroupCluster _ribbonCluster;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _addButtonVerb;
        private DesignerVerb _addColorButtonVerb;
        private DesignerVerb _clearItemsVerb;
        private DesignerVerb _deleteClusterVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
        private ToolStripMenuItem _moveFirstMenu;
        private ToolStripMenuItem _movePreviousMenu;
        private ToolStripMenuItem _moveNextMenu;
        private ToolStripMenuItem _moveLastMenu;
        private ToolStripMenuItem _addButtonMenu;
        private ToolStripMenuItem _addColorButtonMenu;
        private ToolStripMenuItem _clearItemsMenu;
        private ToolStripMenuItem _deleteClusterMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupClusterDesigner class.
		/// </summary>
        public KryptonRibbonGroupClusterDesigner()
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
            _ribbonCluster = (KryptonRibbonGroupCluster)component;
            _ribbonCluster.DesignTimeAddButton += new EventHandler(OnAddButton);
            _ribbonCluster.DesignTimeAddColorButton += new EventHandler(OnAddColorButton);
            _ribbonCluster.DesignTimeContextMenu += new MouseEventHandler(OnContextMenu);

            // Get access to the services
            _designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            // We need to know when we are being removed/changed
            _changeService.ComponentRemoving += new ComponentEventHandler(OnComponentRemoving);
            _changeService.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents
        {
            get
            {
                ArrayList compound = new ArrayList(base.AssociatedComponents);
                compound.AddRange(_ribbonCluster.Items);
                return compound;
            }
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
                    _ribbonCluster.DesignTimeAddButton -= new EventHandler(OnAddButton);
                    _ribbonCluster.DesignTimeContextMenu -= new MouseEventHandler(OnContextMenu);
                    _changeService.ComponentRemoving -= new ComponentEventHandler(OnComponentRemoving);
                    _changeService.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
                }
            }
            finally
            {
                // Must let base class do standard stuff
                base.Dispose(disposing);
            }
        }
        #endregion

        #region Implementation
        private void UpdateVerbStatus()
        {
            // Create verbs first time around
            if (_verbs == null)
            {
                _verbs = new DesignerVerbCollection();
                _toggleHelpersVerb = new DesignerVerb("Toggle Helpers", new EventHandler(OnToggleHelpers));
                _moveFirstVerb = new DesignerVerb("Move Cluster First", new EventHandler(OnMoveFirst));
                _movePrevVerb = new DesignerVerb("Move Cluster Previous", new EventHandler(OnMovePrevious));
                _moveNextVerb = new DesignerVerb("Move Cluster Next", new EventHandler(OnMoveNext));
                _moveLastVerb = new DesignerVerb("Move Cluster Last", new EventHandler(OnMoveLast));
                _addButtonVerb = new DesignerVerb("Add Button", new EventHandler(OnAddButton));
                _addColorButtonVerb = new DesignerVerb("Add Color Button", new EventHandler(OnAddColorButton));
                _clearItemsVerb = new DesignerVerb("Clear Items", new EventHandler(OnClearItems));
                _deleteClusterVerb = new DesignerVerb("Delete Cluster", new EventHandler(OnDeleteCluster));
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, _moveNextVerb, _moveLastVerb, 
                                                     _addButtonVerb, _addColorButtonVerb, _clearItemsVerb, _deleteClusterVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;
            bool clearItems = false;

            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Cast container to the correct type
                KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonCluster.RibbonContainer;

                moveFirst = (lines.Items.IndexOf(_ribbonCluster) > 0);
                movePrev = (lines.Items.IndexOf(_ribbonCluster) > 0);
                moveNext = (lines.Items.IndexOf(_ribbonCluster) < (lines.Items.Count - 1));
                moveLast = (lines.Items.IndexOf(_ribbonCluster) < (lines.Items.Count - 1));
                clearItems = (_ribbonCluster.Items.Count > 0);
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
            _clearItemsVerb.Enabled = clearItems;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
                _ribbonCluster.Ribbon.InDesignHelperMode = !_ribbonCluster.Ribbon.InDesignHelperMode;
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Cast container to the correct type
                KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonCluster.RibbonContainer;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCluster MoveFirst");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(lines)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the cluster
                    lines.Items.Remove(_ribbonCluster);
                    lines.Items.Insert(0, _ribbonCluster);
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
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Cast container to the correct type
                KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonCluster.RibbonContainer;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCluster MovePrevious");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(lines)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the cluster
                    int index = lines.Items.IndexOf(_ribbonCluster) - 1;
                    index = Math.Max(index, 0);
                    lines.Items.Remove(_ribbonCluster);
                    lines.Items.Insert(index, _ribbonCluster);
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
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Cast container to the correct type
                KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonCluster.RibbonContainer;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCluster MoveNext");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(lines)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the cluster
                    int index = lines.Items.IndexOf(_ribbonCluster) + 1;
                    index = Math.Min(index, lines.Items.Count - 1);
                    lines.Items.Remove(_ribbonCluster);
                    lines.Items.Insert(index, _ribbonCluster);
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
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Cast container to the correct type
                KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonCluster.RibbonContainer;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCluster MoveLast");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(lines)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the cluster
                    lines.Items.Remove(_ribbonCluster);
                    lines.Items.Insert(lines.Items.Count, _ribbonCluster);
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

        private void OnAddButton(object sender, EventArgs e)
        {
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCluster AddButton");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonCluster)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a cluster button item
                    KryptonRibbonGroupClusterButton button = (KryptonRibbonGroupClusterButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupClusterButton));
                    _ribbonCluster.Items.Add(button);

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

        private void OnAddColorButton(object sender, EventArgs e)
        {
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCluster AddColorButton");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonCluster)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a cluster color button item
                    KryptonRibbonGroupClusterColorButton button = (KryptonRibbonGroupClusterColorButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupClusterColorButton));
                    _ribbonCluster.Items.Add(button);

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

        private void OnClearItems(object sender, EventArgs e)
        {
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCluster ClearItems");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonCluster)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Need access to host in order to delete a component
                    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                    // We need to remove all the buttons from the cluster group
                    for (int i = _ribbonCluster.Items.Count - 1; i >= 0; i--)
                    {
                        KryptonRibbonGroupItem item = _ribbonCluster.Items[i];
                        _ribbonCluster.Items.Remove(item);
                        host.DestroyComponent(item);
                    }

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

        private void OnDeleteCluster(object sender, EventArgs e)
        {
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Cast container to the correct type
                KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonCluster.RibbonContainer;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple DeleteTriple");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(lines)["Items"];

                    // Remove the ribbon group from the ribbon tab
                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the cluster from the lines
                    lines.Items.Remove(_ribbonCluster);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonCluster);

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

        private void OnVisible(object sender, EventArgs e)
        {
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonCluster, null, _ribbonCluster.Visible, !_ribbonCluster.Visible);
                _ribbonCluster.Visible = !_ribbonCluster.Visible;
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our cluster is being removed
            if (e.Component == _ribbonCluster)
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all items from the cluster
                for (int j = _ribbonCluster.Items.Count - 1; j >= 0; j--)
                {
                    IRibbonGroupItem item = _ribbonCluster.Items[j] as IRibbonGroupItem;

                    if (item != null)
                    {
                        _ribbonCluster.Items.Remove(item);
                        host.DestroyComponent(item as Component);
                    }
                    else
                    {
                        IRibbonGroupContainer container = _ribbonCluster.Items[j] as IRibbonGroupContainer;
                        _ribbonCluster.Items.Remove(container);
                        host.DestroyComponent(container as Component);
                    }
                }
            }
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if ((_ribbonCluster != null) && (_ribbonCluster.Ribbon != null))
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, new EventHandler(OnToggleHelpers));
                    _visibleMenu = new ToolStripMenuItem("Visible", null, new EventHandler(OnVisible));
                    _moveFirstMenu = new ToolStripMenuItem("Move Cluster First", Properties.Resources.MoveFirst, new EventHandler(OnMoveFirst));
                    _movePreviousMenu = new ToolStripMenuItem("Move Cluster Previous", Properties.Resources.MovePrevious, new EventHandler(OnMovePrevious));
                    _moveNextMenu = new ToolStripMenuItem("Move Cluster Next", Properties.Resources.MoveNext, new EventHandler(OnMoveNext));
                    _moveLastMenu = new ToolStripMenuItem("Move Cluster Last", Properties.Resources.MoveLast, new EventHandler(OnMoveLast));
                    _addButtonMenu = new ToolStripMenuItem("Add Button", Properties.Resources.KryptonRibbonGroupClusterButton, new EventHandler(OnAddButton));
                    _addColorButtonMenu = new ToolStripMenuItem("Add Color Button", Properties.Resources.KryptonRibbonGroupClusterColorButton, new EventHandler(OnAddColorButton));
                    _clearItemsMenu = new ToolStripMenuItem("Clear Items", null, new EventHandler(OnClearItems));
                    _deleteClusterMenu = new ToolStripMenuItem("Delete Cluster", Properties.Resources.delete2, new EventHandler(OnDeleteCluster));
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _addButtonMenu, _addColorButtonMenu, new ToolStripSeparator(),
                                                              _clearItemsMenu, new ToolStripSeparator(),
                                                              _deleteClusterMenu });

                    // Ensure add images have correct transparent background
                    _addButtonMenu.ImageTransparentColor = Color.Magenta;
                    _addColorButtonMenu.ImageTransparentColor = Color.Magenta;
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonCluster.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = _ribbonCluster.Visible;
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;
                _clearItemsMenu.Enabled = _clearItemsVerb.Enabled;

                // Show the context menu
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonCluster.Ribbon.ViewRectangleToPoint(_ribbonCluster.ClusterView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }
        #endregion    
    }
}
