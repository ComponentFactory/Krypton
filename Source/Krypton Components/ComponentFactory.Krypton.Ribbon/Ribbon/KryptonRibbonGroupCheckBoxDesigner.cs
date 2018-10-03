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
    internal class KryptonRibbonGroupCheckBoxDesigner : ComponentDesigner
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonGroupCheckBox _ribbonCheckBox;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _deleteCheckBoxVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
        private ToolStripMenuItem _enabledMenu;
        private ToolStripMenuItem _autoCheckMenu;
        private ToolStripMenuItem _checkedMenu;
        private ToolStripMenuItem _threeStateMenu;
        private ToolStripMenuItem _moveFirstMenu;
        private ToolStripMenuItem _movePreviousMenu;
        private ToolStripMenuItem _moveNextMenu;
        private ToolStripMenuItem _moveLastMenu;
        private ToolStripMenuItem _deleteCheckBoxMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupCheckBoxDesigner class.
        /// </summary>
        public KryptonRibbonGroupCheckBoxDesigner()
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
            _ribbonCheckBox = (KryptonRibbonGroupCheckBox)component;
            _ribbonCheckBox.DesignTimeContextMenu += new MouseEventHandler(OnContextMenu);

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
                    _ribbonCheckBox.DesignTimeContextMenu -= new MouseEventHandler(OnContextMenu);
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
                _moveFirstVerb = new DesignerVerb("Move CheckBox First", new EventHandler(OnMoveFirst));
                _movePrevVerb = new DesignerVerb("Move CheckBox Previous", new EventHandler(OnMovePrevious));
                _moveNextVerb = new DesignerVerb("Move CheckBox Next", new EventHandler(OnMoveNext));
                _moveLastVerb = new DesignerVerb("Move CheckBox Last", new EventHandler(OnMoveLast));
                _deleteCheckBoxVerb = new DesignerVerb("Delete CheckBox", new EventHandler(OnDeleteCheckBox));
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, 
                                                     _moveNextVerb, _moveLastVerb, _deleteCheckBoxVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;

            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;
                moveFirst = (items.IndexOf(_ribbonCheckBox) > 0);
                movePrev = (items.IndexOf(_ribbonCheckBox) > 0);
                moveNext = (items.IndexOf(_ribbonCheckBox) < (items.Count - 1));
                moveLast = (items.IndexOf(_ribbonCheckBox) < (items.Count - 1));
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
                _ribbonCheckBox.Ribbon.InDesignHelperMode = !_ribbonCheckBox.Ribbon.InDesignHelperMode;
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCheckBox MoveFirst");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonCheckBox.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the check box
                    items.Remove(_ribbonCheckBox);
                    items.Insert(0, _ribbonCheckBox);
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
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCheckBox MovePrevious");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonCheckBox.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    int index = items.IndexOf(_ribbonCheckBox) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonCheckBox);
                    items.Insert(index, _ribbonCheckBox);
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
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCheckBox MoveNext");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonCheckBox.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    int index = items.IndexOf(_ribbonCheckBox) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonCheckBox);
                    items.Insert(index, _ribbonCheckBox);
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
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCheckBox MoveLast");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonCheckBox.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    items.Remove(_ribbonCheckBox);
                    items.Insert(items.Count, _ribbonCheckBox);
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

        private void OnDeleteCheckBox(object sender, EventArgs e)
        {
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupCheckBox DeleteCheckBox");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonCheckBox.RibbonContainer)["Items"];

                    // Remove the ribbon group from the ribbon tab
                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the check box from the group
                    items.Remove(_ribbonCheckBox);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonCheckBox);

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
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonCheckBox, null, _ribbonCheckBox.Visible, !_ribbonCheckBox.Visible);
                _ribbonCheckBox.Visible = !_ribbonCheckBox.Visible;
            }
        }

        private void OnEnabled(object sender, EventArgs e)
        {
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonCheckBox, null, _ribbonCheckBox.Enabled, !_ribbonCheckBox.Enabled);
                _ribbonCheckBox.Enabled = !_ribbonCheckBox.Enabled;
            }
        }

        private void OnAutoCheck(object sender, EventArgs e)
        {
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonCheckBox, null, _ribbonCheckBox.AutoCheck, !_ribbonCheckBox.AutoCheck);
                _ribbonCheckBox.AutoCheck = !_ribbonCheckBox.AutoCheck;
            }
        }

        private void OnThreeState(object sender, EventArgs e)
        {
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonCheckBox, null, _ribbonCheckBox.ThreeState, !_ribbonCheckBox.ThreeState);
                _ribbonCheckBox.ThreeState = !_ribbonCheckBox.ThreeState;
            }
        }

        private void OnChecked(object sender, EventArgs e)
        {
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonCheckBox, null, _ribbonCheckBox.Checked, !_ribbonCheckBox.Checked);
                _ribbonCheckBox.Checked = !_ribbonCheckBox.Checked;
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if ((_ribbonCheckBox != null) && (_ribbonCheckBox.Ribbon != null))
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, new EventHandler(OnToggleHelpers));
                    _visibleMenu = new ToolStripMenuItem("Visible", null, new EventHandler(OnVisible));
                    _enabledMenu = new ToolStripMenuItem("Enabled", null, new EventHandler(OnEnabled));
                    _autoCheckMenu = new ToolStripMenuItem("AutoCheck", null, new EventHandler(OnAutoCheck));
                    _checkedMenu = new ToolStripMenuItem("Checked", null, new EventHandler(OnChecked));
                    _threeStateMenu = new ToolStripMenuItem("ThreeState", null, new EventHandler(OnThreeState));
                    _moveFirstMenu = new ToolStripMenuItem("Move CheckBox First", Properties.Resources.MoveFirst, new EventHandler(OnMoveFirst));
                    _movePreviousMenu = new ToolStripMenuItem("Move CheckBox Previous", Properties.Resources.MovePrevious, new EventHandler(OnMovePrevious));
                    _moveNextMenu = new ToolStripMenuItem("Move CheckBox Next", Properties.Resources.MoveNext, new EventHandler(OnMoveNext));
                    _moveLastMenu = new ToolStripMenuItem("Move CheckBox Last", Properties.Resources.MoveLast, new EventHandler(OnMoveLast));
                    _deleteCheckBoxMenu = new ToolStripMenuItem("Delete CheckBox", Properties.Resources.delete2, new EventHandler(OnDeleteCheckBox));
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, _enabledMenu, _autoCheckMenu, _checkedMenu, _threeStateMenu, new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _deleteCheckBoxMenu });
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonCheckBox.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = _ribbonCheckBox.Visible;
                _enabledMenu.Checked = _ribbonCheckBox.Enabled;
                _autoCheckMenu.Checked = _ribbonCheckBox.AutoCheck;
                _checkedMenu.Checked = _ribbonCheckBox.Checked;
                _threeStateMenu.Checked = _ribbonCheckBox.ThreeState;
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;

                // Show the context menu
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonCheckBox.Ribbon.ViewRectangleToPoint(_ribbonCheckBox.CheckBoxView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }

        private TypedRestrictCollection<KryptonRibbonGroupItem> ParentItems
        {
            get
            {
                if (_ribbonCheckBox.RibbonContainer is KryptonRibbonGroupTriple)
                {
                    KryptonRibbonGroupTriple triple = (KryptonRibbonGroupTriple)_ribbonCheckBox.RibbonContainer;
                    return triple.Items;
                }
                else if (_ribbonCheckBox.RibbonContainer is KryptonRibbonGroupLines)
                {
                    KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonCheckBox.RibbonContainer;
                    return lines.Items;
                }
                else
                {
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
                }
            }
        }
        #endregion
    }
}
