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
    internal class KryptonRibbonGroupDomainUpDownDesigner : ComponentDesigner, IKryptonDesignObject
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonGroupDomainUpDown _ribbonDomainUpDown;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _deleteDomainUpDownVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
        private ToolStripMenuItem _moveFirstMenu;
        private ToolStripMenuItem _movePreviousMenu;
        private ToolStripMenuItem _moveNextMenu;
        private ToolStripMenuItem _moveLastMenu;
        private ToolStripMenuItem _deleteDomainUpDownMenu;
        private bool _visible;
        private bool _enabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupDomainUpDownDesigner class.
        /// </summary>
        public KryptonRibbonGroupDomainUpDownDesigner()
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
            _ribbonDomainUpDown = (KryptonRibbonGroupDomainUpDown)component;
            _ribbonDomainUpDown.DomainUpDownDesigner = this;

            // Update designer properties with actual starting values
            Visible = _ribbonDomainUpDown.Visible;
            Enabled = _ribbonDomainUpDown.Enabled;

            // Update visible/enabled to always be showing/enabled at design time
            _ribbonDomainUpDown.Visible = true;
            _ribbonDomainUpDown.Enabled = true;

            // Tell the embedded domain up-down control it is in design mode
            _ribbonDomainUpDown.DomainUpDown.InRibbonDesignMode = true;

            // Hook into events
            _ribbonDomainUpDown.DesignTimeContextMenu += new MouseEventHandler(OnContextMenu);

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
                    _ribbonDomainUpDown.DesignTimeContextMenu -= new MouseEventHandler(OnContextMenu);
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
                    properties[strArray[i]] = TypeDescriptor.CreateProperty(typeof(KryptonRibbonGroupDomainUpDownDesigner), descrip, attributes);
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
                _moveFirstVerb = new DesignerVerb("Move DomainUpDown First", new EventHandler(OnMoveFirst));
                _movePrevVerb = new DesignerVerb("Move DomainUpDown Previous", new EventHandler(OnMovePrevious));
                _moveNextVerb = new DesignerVerb("Move DomainUpDown Next", new EventHandler(OnMoveNext));
                _moveLastVerb = new DesignerVerb("Move DomainUpDown Last", new EventHandler(OnMoveLast));
                _deleteDomainUpDownVerb = new DesignerVerb("Delete DomainUpDown", new EventHandler(OnDeleteDomainUpDown));
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, 
                                                     _moveNextVerb, _moveLastVerb, _deleteDomainUpDownVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;

            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
            {
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;
                moveFirst = (items.IndexOf(_ribbonDomainUpDown) > 0);
                movePrev = (items.IndexOf(_ribbonDomainUpDown) > 0);
                moveNext = (items.IndexOf(_ribbonDomainUpDown) < (items.Count - 1));
                moveLast = (items.IndexOf(_ribbonDomainUpDown) < (items.Count - 1));
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
                _ribbonDomainUpDown.Ribbon.InDesignHelperMode = !_ribbonDomainUpDown.Ribbon.InDesignHelperMode;
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupDomainUpDown MoveFirst");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    items.Remove(_ribbonDomainUpDown);
                    items.Insert(0, _ribbonDomainUpDown);
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
            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupDomainUpDown MovePrevious");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    int index = items.IndexOf(_ribbonDomainUpDown) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonDomainUpDown);
                    items.Insert(index, _ribbonDomainUpDown);
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
            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupDomainUpDown MoveNext");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    int index = items.IndexOf(_ribbonDomainUpDown) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonDomainUpDown);
                    items.Insert(index, _ribbonDomainUpDown);
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
            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupDomainUpDown MoveLast");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    items.Remove(_ribbonDomainUpDown);
                    items.Insert(items.Count, _ribbonDomainUpDown);
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

        private void OnDeleteDomainUpDown(object sender, EventArgs e)
        {
            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupDomainUpDown DeleteDomainUpDown");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonDomainUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the numeric up-down from the group
                    items.Remove(_ribbonDomainUpDown);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonDomainUpDown);

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
            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
            {
                PropertyDescriptor propertyEnabled = TypeDescriptor.GetProperties(_ribbonDomainUpDown)["Enabled"];
                bool oldValue = (bool)propertyEnabled.GetValue(_ribbonDomainUpDown);
                bool newValue = !oldValue;
                _changeService.OnComponentChanged(_ribbonDomainUpDown, null, oldValue, newValue);
                propertyEnabled.SetValue(_ribbonDomainUpDown, newValue);
            }
        }

        private void OnVisible(object sender, EventArgs e)
        {
            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
            {
                PropertyDescriptor propertyVisible = TypeDescriptor.GetProperties(_ribbonDomainUpDown)["Visible"];
                bool oldValue = (bool)propertyVisible.GetValue(_ribbonDomainUpDown);
                bool newValue = !oldValue;
                _changeService.OnComponentChanged(_ribbonDomainUpDown, null, oldValue, newValue);
                propertyVisible.SetValue(_ribbonDomainUpDown, newValue);
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if ((_ribbonDomainUpDown != null) && (_ribbonDomainUpDown.Ribbon != null))
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, new EventHandler(OnToggleHelpers));
                    _visibleMenu = new ToolStripMenuItem("Visible", null, new EventHandler(OnVisible));
                    _moveFirstMenu = new ToolStripMenuItem("Move DomainUpDown First", Properties.Resources.MoveFirst, new EventHandler(OnMoveFirst));
                    _movePreviousMenu = new ToolStripMenuItem("Move DomainUpDown Previous", Properties.Resources.MovePrevious, new EventHandler(OnMovePrevious));
                    _moveNextMenu = new ToolStripMenuItem("Move DomainUpDown Next", Properties.Resources.MoveNext, new EventHandler(OnMoveNext));
                    _moveLastMenu = new ToolStripMenuItem("Move DomainUpDown Last", Properties.Resources.MoveLast, new EventHandler(OnMoveLast));
                    _deleteDomainUpDownMenu = new ToolStripMenuItem("Delete DomainUpDown", Properties.Resources.delete2, new EventHandler(OnDeleteDomainUpDown));
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _deleteDomainUpDownMenu });
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonDomainUpDown.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = Visible;
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;

                // Show the context menu
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonDomainUpDown.Ribbon.ViewRectangleToPoint(_ribbonDomainUpDown.DomainUpDownView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }

        private TypedRestrictCollection<KryptonRibbonGroupItem> ParentItems
        {
            get
            {
                if (_ribbonDomainUpDown.RibbonContainer is KryptonRibbonGroupTriple)
                {
                    KryptonRibbonGroupTriple triple = (KryptonRibbonGroupTriple)_ribbonDomainUpDown.RibbonContainer;
                    return triple.Items;
                }
                else if (_ribbonDomainUpDown.RibbonContainer is KryptonRibbonGroupLines)
                {
                    KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonDomainUpDown.RibbonContainer;
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
