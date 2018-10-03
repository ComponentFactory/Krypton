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
    internal class KryptonRibbonGroupColorButtonDesigner : ComponentDesigner
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonGroupColorButton _ribbonColorButton;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _deleteButtonVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
        private ToolStripMenuItem _enabledMenu;
        private ToolStripMenuItem _checkedMenu;
        private ToolStripMenuItem _typeMenu;
        private ToolStripMenuItem _typePushMenu;
        private ToolStripMenuItem _typeCheckMenu;
        private ToolStripMenuItem _typeDropDownMenu;
        private ToolStripMenuItem _typeSplitMenu;
        private ToolStripMenuItem _moveFirstMenu;
        private ToolStripMenuItem _movePreviousMenu;
        private ToolStripMenuItem _moveNextMenu;
        private ToolStripMenuItem _moveLastMenu;
        private ToolStripMenuItem _deleteButtonMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupColorButtonDesigner class.
        /// </summary>
        public KryptonRibbonGroupColorButtonDesigner()
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
            _ribbonColorButton = (KryptonRibbonGroupColorButton)component;
            _ribbonColorButton.DesignTimeContextMenu += new MouseEventHandler(OnContextMenu);

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
                    _ribbonColorButton.DesignTimeContextMenu -= new MouseEventHandler(OnContextMenu);
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
                _moveFirstVerb = new DesignerVerb("Move Color Button First", new EventHandler(OnMoveFirst));
                _movePrevVerb = new DesignerVerb("Move Color Button Previous", new EventHandler(OnMovePrevious));
                _moveNextVerb = new DesignerVerb("Move Color Button Next", new EventHandler(OnMoveNext));
                _moveLastVerb = new DesignerVerb("Move Color Button Last", new EventHandler(OnMoveLast));
                _deleteButtonVerb = new DesignerVerb("Delete Color Button", new EventHandler(OnDeleteButton));
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, 
                                                     _moveNextVerb, _moveLastVerb, _deleteButtonVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;

            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;
                moveFirst = (items.IndexOf(_ribbonColorButton) > 0);
                movePrev = (items.IndexOf(_ribbonColorButton) > 0);
                moveNext = (items.IndexOf(_ribbonColorButton) < (items.Count - 1));
                moveLast = (items.IndexOf(_ribbonColorButton) < (items.Count - 1));
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
                _ribbonColorButton.Ribbon.InDesignHelperMode = !_ribbonColorButton.Ribbon.InDesignHelperMode;
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupColorButton MoveFirst");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonColorButton.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the button
                    items.Remove(_ribbonColorButton);
                    items.Insert(0, _ribbonColorButton);
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
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupColorButton MovePrevious");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonColorButton.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    int index = items.IndexOf(_ribbonColorButton) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonColorButton);
                    items.Insert(index, _ribbonColorButton);
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
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupColorButton MoveNext");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonColorButton.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    int index = items.IndexOf(_ribbonColorButton) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonColorButton);
                    items.Insert(index, _ribbonColorButton);
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
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupColorButton MoveLast");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonColorButton.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    items.Remove(_ribbonColorButton);
                    items.Insert(items.Count, _ribbonColorButton);
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

        private void OnDeleteButton(object sender, EventArgs e)
        {
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupColorButton DeleteButton");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonColorButton.RibbonContainer)["Items"];

                    // Remove the ribbon group from the ribbon tab
                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the button from the group
                    items.Remove(_ribbonColorButton);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonColorButton);

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
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonColorButton, null, _ribbonColorButton.Visible, !_ribbonColorButton.Visible);
                _ribbonColorButton.Visible = !_ribbonColorButton.Visible;
            }
        }

        private void OnEnabled(object sender, EventArgs e)
        {
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonColorButton, null, _ribbonColorButton.Enabled, !_ribbonColorButton.Enabled);
                _ribbonColorButton.Enabled = !_ribbonColorButton.Enabled;
            }
        }

        private void OnChecked(object sender, EventArgs e)
        {
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonColorButton, null, _ribbonColorButton.Checked, !_ribbonColorButton.Checked);
                _ribbonColorButton.Checked = !_ribbonColorButton.Checked;
            }
        }

        private void OnTypePush(object sender, EventArgs e)
        {
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonColorButton, null, _ribbonColorButton.ButtonType, GroupButtonType.Push);
                _ribbonColorButton.ButtonType = GroupButtonType.Push;
            }
        }

        private void OnTypeCheck(object sender, EventArgs e)
        {
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonColorButton, null, _ribbonColorButton.ButtonType, GroupButtonType.Check);
                _ribbonColorButton.ButtonType = GroupButtonType.Check;
            }
        }

        private void OnTypeDropDown(object sender, EventArgs e)
        {
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonColorButton, null, _ribbonColorButton.ButtonType, GroupButtonType.DropDown);
                _ribbonColorButton.ButtonType = GroupButtonType.DropDown;
            }
        }

        private void OnTypeSplit(object sender, EventArgs e)
        {
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                _changeService.OnComponentChanged(_ribbonColorButton, null, _ribbonColorButton.ButtonType, GroupButtonType.Split);
                _ribbonColorButton.ButtonType = GroupButtonType.Split;
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if ((_ribbonColorButton != null) && (_ribbonColorButton.Ribbon != null))
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, new EventHandler(OnToggleHelpers));
                    _visibleMenu = new ToolStripMenuItem("Visible", null, new EventHandler(OnVisible));
                    _enabledMenu = new ToolStripMenuItem("Enabled", null, new EventHandler(OnEnabled));
                    _checkedMenu = new ToolStripMenuItem("Checked", null, new EventHandler(OnChecked));
                    _typePushMenu = new ToolStripMenuItem("Push", null, new EventHandler(OnTypePush));
                    _typeCheckMenu = new ToolStripMenuItem("Check", null, new EventHandler(OnTypeCheck));
                    _typeDropDownMenu = new ToolStripMenuItem("DropDown", null, new EventHandler(OnTypeDropDown));
                    _typeSplitMenu = new ToolStripMenuItem("Split", null, new EventHandler(OnTypeSplit));
                    _typeMenu = new ToolStripMenuItem("Type");
                    _typeMenu.DropDownItems.AddRange(new ToolStripItem[] { _typePushMenu, _typeCheckMenu, _typeDropDownMenu, _typeSplitMenu });
                    _moveFirstMenu = new ToolStripMenuItem("Move Color Button First", Properties.Resources.MoveFirst, new EventHandler(OnMoveFirst));
                    _movePreviousMenu = new ToolStripMenuItem("Move Color Button Previous", Properties.Resources.MovePrevious, new EventHandler(OnMovePrevious));
                    _moveNextMenu = new ToolStripMenuItem("Move Color Button Next", Properties.Resources.MoveNext, new EventHandler(OnMoveNext));
                    _moveLastMenu = new ToolStripMenuItem("Move Color Button Last", Properties.Resources.MoveLast, new EventHandler(OnMoveLast));
                    _deleteButtonMenu = new ToolStripMenuItem("Delete Color Button", Properties.Resources.delete2, new EventHandler(OnDeleteButton));
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, _enabledMenu, _checkedMenu, _typeMenu, new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _deleteButtonMenu });
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonColorButton.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = _ribbonColorButton.Visible;
                _enabledMenu.Checked = _ribbonColorButton.Enabled;
                _checkedMenu.Checked = _ribbonColorButton.Checked;
                _typePushMenu.Checked = (_ribbonColorButton.ButtonType == GroupButtonType.Push);
                _typeCheckMenu.Checked = (_ribbonColorButton.ButtonType == GroupButtonType.Check);
                _typeDropDownMenu.Checked = (_ribbonColorButton.ButtonType == GroupButtonType.DropDown);
                _typeSplitMenu.Checked = (_ribbonColorButton.ButtonType == GroupButtonType.Split);
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;

                // Show the context menu
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonColorButton.Ribbon.ViewRectangleToPoint(_ribbonColorButton.ColorButtonView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }

        private TypedRestrictCollection<KryptonRibbonGroupItem> ParentItems
        {
            get
            {
                if (_ribbonColorButton.RibbonContainer is KryptonRibbonGroupTriple)
                {
                    KryptonRibbonGroupTriple triple = (KryptonRibbonGroupTriple)_ribbonColorButton.RibbonContainer;
                    return triple.Items;
                }
                else if (_ribbonColorButton.RibbonContainer is KryptonRibbonGroupLines)
                {
                    KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonColorButton.RibbonContainer;
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
