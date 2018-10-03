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
    internal class KryptonRibbonGroupRadioButtonDesigner : ComponentDesigner
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonGroupRadioButton _ribbonRadioButton;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _deleteRadioButtonVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
        private ToolStripMenuItem _enabledMenu;
        private ToolStripMenuItem _checkedMenu;
        private ToolStripMenuItem _moveFirstMenu;
        private ToolStripMenuItem _movePreviousMenu;
        private ToolStripMenuItem _moveNextMenu;
        private ToolStripMenuItem _moveLastMenu;
        private ToolStripMenuItem _deleteRadioButtonMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupRadioButtonDesigner class.
        /// </summary>
        public KryptonRibbonGroupRadioButtonDesigner()
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
            _ribbonRadioButton = (KryptonRibbonGroupRadioButton)component;
            _ribbonRadioButton.DesignTimeContextMenu += new MouseEventHandler(OnContextMenu);

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
                    _ribbonRadioButton.DesignTimeContextMenu -= new MouseEventHandler(OnContextMenu);
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
                _moveFirstVerb = new DesignerVerb("Move RadioButton First", new EventHandler(OnMoveFirst));
                _movePrevVerb = new DesignerVerb("Move RadioButton Previous", new EventHandler(OnMovePrevious));
                _moveNextVerb = new DesignerVerb("Move RadioButton Next", new EventHandler(OnMoveNext));
                _moveLastVerb = new DesignerVerb("Move RadioButton Last", new EventHandler(OnMoveLast));
                _deleteRadioButtonVerb = new DesignerVerb("Delete RadioButton", new EventHandler(OnDeleteRadioButton));
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, 
                                                     _moveNextVerb, _moveLastVerb, _deleteRadioButtonVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;

            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;
                moveFirst = (items.IndexOf(_ribbonRadioButton) > 0);
                movePrev = (items.IndexOf(_ribbonRadioButton) > 0);
                moveNext = (items.IndexOf(_ribbonRadioButton) < (items.Count - 1));
                moveLast = (items.IndexOf(_ribbonRadioButton) < (items.Count - 1));
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
                _ribbonRadioButton.Ribbon.InDesignHelperMode = !_ribbonRadioButton.Ribbon.InDesignHelperMode;
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRadioButton MoveFirst");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRadioButton.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the radio button
                    items.Remove(_ribbonRadioButton);
                    items.Insert(0, _ribbonRadioButton);
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
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRadioButton MovePrevious");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRadioButton.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    int index = items.IndexOf(_ribbonRadioButton) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonRadioButton);
                    items.Insert(index, _ribbonRadioButton);
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
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRadioButton MoveNext");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRadioButton.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    int index = items.IndexOf(_ribbonRadioButton) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonRadioButton);
                    items.Insert(index, _ribbonRadioButton);
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
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRadioButton MoveLast");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRadioButton.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    items.Remove(_ribbonRadioButton);
                    items.Insert(items.Count, _ribbonRadioButton);
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

        private void OnDeleteRadioButton(object sender, EventArgs e)
        {
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRadioButton DeleteRadioButton");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRadioButton.RibbonContainer)["Items"];

                    // Remove the ribbon group from the ribbon tab
                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the radio button from the group
                    items.Remove(_ribbonRadioButton);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonRadioButton);

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
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonRadioButton, null, _ribbonRadioButton.Visible, !_ribbonRadioButton.Visible);
                _ribbonRadioButton.Visible = !_ribbonRadioButton.Visible;
            }
        }

        private void OnEnabled(object sender, EventArgs e)
        {
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonRadioButton, null, _ribbonRadioButton.Enabled, !_ribbonRadioButton.Enabled);
                _ribbonRadioButton.Enabled = !_ribbonRadioButton.Enabled;
            }
        }

        private void OnChecked(object sender, EventArgs e)
        {
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonRadioButton, null, _ribbonRadioButton.Checked, !_ribbonRadioButton.Checked);
                _ribbonRadioButton.Checked = !_ribbonRadioButton.Checked;
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if ((_ribbonRadioButton != null) && (_ribbonRadioButton.Ribbon != null))
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, new EventHandler(OnToggleHelpers));
                    _visibleMenu = new ToolStripMenuItem("Visible", null, new EventHandler(OnVisible));
                    _enabledMenu = new ToolStripMenuItem("Enabled", null, new EventHandler(OnEnabled));
                    _checkedMenu = new ToolStripMenuItem("Checked", null, new EventHandler(OnChecked));
                    _moveFirstMenu = new ToolStripMenuItem("Move RadioButton First", Properties.Resources.MoveFirst, new EventHandler(OnMoveFirst));
                    _movePreviousMenu = new ToolStripMenuItem("Move RadioButton Previous", Properties.Resources.MovePrevious, new EventHandler(OnMovePrevious));
                    _moveNextMenu = new ToolStripMenuItem("Move RadioButton Next", Properties.Resources.MoveNext, new EventHandler(OnMoveNext));
                    _moveLastMenu = new ToolStripMenuItem("Move RadioButton Last", Properties.Resources.MoveLast, new EventHandler(OnMoveLast));
                    _deleteRadioButtonMenu = new ToolStripMenuItem("Delete RadioButton", Properties.Resources.delete2, new EventHandler(OnDeleteRadioButton));
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, _enabledMenu, _checkedMenu,  new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _deleteRadioButtonMenu });
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonRadioButton.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = _ribbonRadioButton.Visible;
                _enabledMenu.Checked = _ribbonRadioButton.Enabled;
                _checkedMenu.Checked = _ribbonRadioButton.Checked;
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;

                // Show the context menu
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonRadioButton.Ribbon.ViewRectangleToPoint(_ribbonRadioButton.RadioButtonView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }

        private TypedRestrictCollection<KryptonRibbonGroupItem> ParentItems
        {
            get
            {
                if (_ribbonRadioButton.RibbonContainer is KryptonRibbonGroupTriple)
                {
                    KryptonRibbonGroupTriple triple = (KryptonRibbonGroupTriple)_ribbonRadioButton.RibbonContainer;
                    return triple.Items;
                }
                else if (_ribbonRadioButton.RibbonContainer is KryptonRibbonGroupLines)
                {
                    KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonRadioButton.RibbonContainer;
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
