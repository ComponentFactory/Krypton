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
    internal class KryptonRibbonTabDesigner : ComponentDesigner
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonTab _ribbonTab;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _addGroupVerb;
        private DesignerVerb _clearGroupsVerb;
        private DesignerVerb _deleteTabVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
        private ToolStripMenuItem _moveFirstMenu;
        private ToolStripMenuItem _movePreviousMenu;
        private ToolStripMenuItem _moveNextMenu;
        private ToolStripMenuItem _moveLastMenu;
        private ToolStripMenuItem _addGroupMenu;
        private ToolStripMenuItem _clearGroupsMenu;
        private ToolStripMenuItem _deleteTabMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonTabDesigner class.
		/// </summary>
        public KryptonRibbonTabDesigner()
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
            _ribbonTab = (KryptonRibbonTab)component;
            _ribbonTab.DesignTimeAddGroup += new EventHandler(OnAddGroup);
            _ribbonTab.DesignTimeContextMenu += new MouseEventHandler(OnContextMenu);

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
                compound.AddRange(_ribbonTab.Groups);
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
                    // Kill the menu strip
                    if (_cms != null)
                    {
                        _cms.Dispose();
                        _cms = null;
                    }

                    // Unhook from events
                    _ribbonTab.DesignTimeAddGroup -= new EventHandler(OnAddGroup);
                    _ribbonTab.DesignTimeContextMenu -= new MouseEventHandler(OnContextMenu);
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
                _moveFirstVerb = new DesignerVerb("Move First", new EventHandler(OnMoveFirst));
                _movePrevVerb = new DesignerVerb("Move Previous", new EventHandler(OnMovePrevious));
                _moveNextVerb = new DesignerVerb("Move Next", new EventHandler(OnMoveNext));
                _moveLastVerb = new DesignerVerb("Move Last", new EventHandler(OnMoveLast));
                _addGroupVerb = new DesignerVerb("Add Group", new EventHandler(OnAddGroup));
                _clearGroupsVerb = new DesignerVerb("Clear Groups", new EventHandler(OnClearGroups));
                _deleteTabVerb = new DesignerVerb("Delete Tab", new EventHandler(OnDeleteTab));
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, _moveNextVerb, _moveLastVerb, _addGroupVerb, _clearGroupsVerb, _deleteTabVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;
            bool clearGroups = false;

            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                moveFirst = (_ribbonTab.Ribbon.RibbonTabs.IndexOf(_ribbonTab) > 0);
                movePrev = (_ribbonTab.Ribbon.RibbonTabs.IndexOf(_ribbonTab) > 0);
                moveNext = (_ribbonTab.Ribbon.RibbonTabs.IndexOf(_ribbonTab) < (_ribbonTab.Ribbon.RibbonTabs.Count - 1));
                moveLast = (_ribbonTab.Ribbon.RibbonTabs.IndexOf(_ribbonTab) < (_ribbonTab.Ribbon.RibbonTabs.Count - 1));
                clearGroups = (_ribbonTab.Groups.Count > 0);
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
            _clearGroupsVerb.Enabled = clearGroups;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if ((_ribbonTab != null) && (_ribbonTab.Ribbon != null))
                _ribbonTab.Ribbon.InDesignHelperMode = !_ribbonTab.Ribbon.InDesignHelperMode;
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonTab MoveFirst");

                try
                {
                    // Get access to the RibbonTabs property
                    MemberDescriptor propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)["RibbonTabs"];

                    RaiseComponentChanging(propertyTabs);

                    // Move position of the tab
                    KryptonRibbon ribbon = _ribbonTab.Ribbon;
                    ribbon.RibbonTabs.Remove(_ribbonTab);
                    ribbon.RibbonTabs.Insert(0, _ribbonTab);
                    ribbon.SelectedTab = _ribbonTab;
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyTabs, null, null);
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
            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonTab MoveNext");

                try
                {
                    // Get access to the RibbonTabs property
                    MemberDescriptor propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)["RibbonTabs"];

                    RaiseComponentChanging(propertyTabs);

                    // Move position of the tab
                    KryptonRibbon ribbon = _ribbonTab.Ribbon;
                    int index = ribbon.RibbonTabs.IndexOf(_ribbonTab) - 1;
                    index = Math.Max(index, 0);
                    ribbon.RibbonTabs.Remove(_ribbonTab);
                    ribbon.RibbonTabs.Insert(index, _ribbonTab);
                    ribbon.SelectedTab = _ribbonTab;
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyTabs, null, null);
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
            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonTab MovePrevious");

                try
                {
                    // Get access to the RibbonTabs property
                    MemberDescriptor propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)["RibbonTabs"];

                    RaiseComponentChanging(propertyTabs);

                    // Move position of the tab
                    KryptonRibbon ribbon = _ribbonTab.Ribbon;
                    int index = ribbon.RibbonTabs.IndexOf(_ribbonTab) + 1;
                    index = Math.Min(index, ribbon.RibbonTabs.Count - 1);
                    ribbon.RibbonTabs.Remove(_ribbonTab);
                    ribbon.RibbonTabs.Insert(index, _ribbonTab);
                    ribbon.SelectedTab = _ribbonTab;
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyTabs, null, null);
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
            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonTab MoveLast");

                try
                {
                    // Get access to the RibbonTabs property
                    MemberDescriptor propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)["RibbonTabs"];

                    RaiseComponentChanging(propertyTabs);

                    // Move position of the tab
                    KryptonRibbon ribbon = _ribbonTab.Ribbon;
                    ribbon.RibbonTabs.Remove(_ribbonTab);
                    ribbon.RibbonTabs.Insert(ribbon.RibbonTabs.Count, _ribbonTab);
                    ribbon.SelectedTab = _ribbonTab;
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyTabs, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    if (transaction != null)
                        transaction.Commit();
                }
            }
        }

        private void OnAddGroup(object sender, EventArgs e)
        {
            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonTab AddGroup");

                try
                {
                    // Get access to the Groups property
                    MemberDescriptor propertyGroups = TypeDescriptor.GetProperties(_ribbonTab)["Groups"];

                    RaiseComponentChanging(propertyGroups);

                    // Get designer to create the new group component
                    KryptonRibbonGroup group = (KryptonRibbonGroup)_designerHost.CreateComponent(typeof(KryptonRibbonGroup));
                    _ribbonTab.Groups.Add(group);

                    RaiseComponentChanged(propertyGroups, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    if (transaction != null)
                        transaction.Commit();
                }
            }
        }

        private void OnClearGroups(object sender, EventArgs e)
        {
            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonTab ClearGroups");

                try
                {
                    // Get access to the Groups property
                    MemberDescriptor propertyGroups = TypeDescriptor.GetProperties(_ribbonTab)["Groups"];

                    RaiseComponentChanging(propertyGroups);

                    // Need access to host in order to delete a component
                    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                    // We need to remove all the groups from the tab
                    for (int i = _ribbonTab.Groups.Count - 1; i >= 0; i--)
                    {
                        KryptonRibbonGroup group = _ribbonTab.Groups[i];
                        _ribbonTab.Groups.Remove(group);
                        host.DestroyComponent(group);
                    }

                    RaiseComponentChanged(propertyGroups, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    if (transaction != null)
                        transaction.Commit();
                }
            }
        }

        private void OnDeleteTab(object sender, EventArgs e)
        {
            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonTab DeleteTab");

                try
                {
                    // Get access to the RibbonTabs property
                    MemberDescriptor propertyTabs = TypeDescriptor.GetProperties(_ribbonTab.Ribbon)["RibbonTabs"];

                    // Remove the ribbon tab from the ribbon
                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyTabs);

                    // Remove the page from the ribbon
                    _ribbonTab.Ribbon.RibbonTabs.Remove(_ribbonTab);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonTab);

                    RaiseComponentChanged(propertyTabs, null, null);
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
            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                _changeService.OnComponentChanged(_ribbonTab, null, _ribbonTab.Visible, !_ribbonTab.Visible);
                _ribbonTab.Visible = !_ribbonTab.Visible;
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our tab is being removed
            if (e.Component == _ribbonTab)
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all the groups from the tab
                for (int i = _ribbonTab.Groups.Count - 1; i >= 0; i--)
                {
                    KryptonRibbonGroup group = _ribbonTab.Groups[i];
                    _ribbonTab.Groups.Remove(group);
                    host.DestroyComponent(group);
                }
            }
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if ((_ribbonTab != null) &&
                (_ribbonTab.Ribbon != null) &&
                 _ribbonTab.Ribbon.RibbonTabs.Contains(_ribbonTab))
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, new EventHandler(OnToggleHelpers));
                    _visibleMenu = new ToolStripMenuItem("Visible", null, new EventHandler(OnVisible));
                    _moveFirstMenu = new ToolStripMenuItem("Move First", Properties.Resources.MoveFirst, new EventHandler(OnMoveFirst));
                    _movePreviousMenu = new ToolStripMenuItem("Move Previous", Properties.Resources.MovePrevious, new EventHandler(OnMovePrevious));
                    _moveNextMenu = new ToolStripMenuItem("Move Next", Properties.Resources.MoveNext, new EventHandler(OnMoveNext));
                    _moveLastMenu = new ToolStripMenuItem("Move Last", Properties.Resources.MoveLast, new EventHandler(OnMoveLast));
                    _addGroupMenu = new ToolStripMenuItem("Add Group", Properties.Resources.KryptonRibbonGroup, new EventHandler(OnAddGroup));
                    _clearGroupsMenu = new ToolStripMenuItem("Clear Groups", null, new EventHandler(OnClearGroups));
                    _deleteTabMenu = new ToolStripMenuItem("Delete Tab", Properties.Resources.delete2, new EventHandler(OnDeleteTab));
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _addGroupMenu, new ToolStripSeparator(),
                                                              _clearGroupsMenu, new ToolStripSeparator(),
                                                              _deleteTabMenu });

                    _addGroupMenu.ImageTransparentColor = Color.Magenta;
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonTab.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = _ribbonTab.Visible;
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;
                _clearGroupsMenu.Enabled = _clearGroupsVerb.Enabled;

                // Convert from ribbon to screen coordinates
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonTab.Ribbon.ViewRectangleToPoint(_ribbonTab.TabView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }
        #endregion
    }
}
