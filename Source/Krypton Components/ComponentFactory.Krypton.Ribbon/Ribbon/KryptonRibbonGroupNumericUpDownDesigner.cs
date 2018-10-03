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
    internal class KryptonRibbonGroupNumericUpDownDesigner : ComponentDesigner, IKryptonDesignObject
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonGroupNumericUpDown _ribbonNumericUpDown;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _deleteNumericUpDownVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
        private ToolStripMenuItem _moveFirstMenu;
        private ToolStripMenuItem _movePreviousMenu;
        private ToolStripMenuItem _moveNextMenu;
        private ToolStripMenuItem _moveLastMenu;
        private ToolStripMenuItem _deleteNumericUpDownMenu;
        private bool _visible;
        private bool _enabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupNumericUpDownDesigner class.
        /// </summary>
        public KryptonRibbonGroupNumericUpDownDesigner()
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
            _ribbonNumericUpDown = (KryptonRibbonGroupNumericUpDown)component;
            _ribbonNumericUpDown.NumericUpDownDesigner = this;

            // Update designer properties with actual starting values
            Visible = _ribbonNumericUpDown.Visible;
            Enabled = _ribbonNumericUpDown.Enabled;

            // Update visible/enabled to always be showing/enabled at design time
            _ribbonNumericUpDown.Visible = true;
            _ribbonNumericUpDown.Enabled = true;

            // Tell the embedded numeric up-down control it is in design mode
            _ribbonNumericUpDown.NumericUpDown.InRibbonDesignMode = true;

            // Hook into events
            _ribbonNumericUpDown.DesignTimeContextMenu += new MouseEventHandler(OnContextMenu);

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
                    _ribbonNumericUpDown.DesignTimeContextMenu -= new MouseEventHandler(OnContextMenu);
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
                    properties[strArray[i]] = TypeDescriptor.CreateProperty(typeof(KryptonRibbonGroupNumericUpDownDesigner), descrip, attributes);
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
                _moveFirstVerb = new DesignerVerb("Move NumericUpDown First", new EventHandler(OnMoveFirst));
                _movePrevVerb = new DesignerVerb("Move NumericUpDown Previous", new EventHandler(OnMovePrevious));
                _moveNextVerb = new DesignerVerb("Move NumericUpDown Next", new EventHandler(OnMoveNext));
                _moveLastVerb = new DesignerVerb("Move NumericUpDown Last", new EventHandler(OnMoveLast));
                _deleteNumericUpDownVerb = new DesignerVerb("Delete NumericUpDown", new EventHandler(OnDeleteNumericUpDown));
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, 
                                                     _moveNextVerb, _moveLastVerb, _deleteNumericUpDownVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;

            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
            {
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;
                moveFirst = (items.IndexOf(_ribbonNumericUpDown) > 0);
                movePrev = (items.IndexOf(_ribbonNumericUpDown) > 0);
                moveNext = (items.IndexOf(_ribbonNumericUpDown) < (items.Count - 1));
                moveLast = (items.IndexOf(_ribbonNumericUpDown) < (items.Count - 1));
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
                _ribbonNumericUpDown.Ribbon.InDesignHelperMode = !_ribbonNumericUpDown.Ribbon.InDesignHelperMode;
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupNumericUpDown MoveFirst");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonNumericUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    items.Remove(_ribbonNumericUpDown);
                    items.Insert(0, _ribbonNumericUpDown);
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
            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupNumericUpDown MovePrevious");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonNumericUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    int index = items.IndexOf(_ribbonNumericUpDown) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonNumericUpDown);
                    items.Insert(index, _ribbonNumericUpDown);
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
            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupNumericUpDown MoveNext");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonNumericUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    int index = items.IndexOf(_ribbonNumericUpDown) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonNumericUpDown);
                    items.Insert(index, _ribbonNumericUpDown);
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
            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupNumericUpDown MoveLast");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonNumericUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the numeric up-down
                    items.Remove(_ribbonNumericUpDown);
                    items.Insert(items.Count, _ribbonNumericUpDown);
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

        private void OnDeleteNumericUpDown(object sender, EventArgs e)
        {
            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupNumericUpDown DeleteNumericUpDown");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonNumericUpDown.RibbonContainer)["Items"];

                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the numeric up-down from the group
                    items.Remove(_ribbonNumericUpDown);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonNumericUpDown);

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
            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
            {
                PropertyDescriptor propertyEnabled = TypeDescriptor.GetProperties(_ribbonNumericUpDown)["Enabled"];
                bool oldValue = (bool)propertyEnabled.GetValue(_ribbonNumericUpDown);
                bool newValue = !oldValue;
                _changeService.OnComponentChanged(_ribbonNumericUpDown, null, oldValue, newValue);
                propertyEnabled.SetValue(_ribbonNumericUpDown, newValue);
            }
        }

        private void OnVisible(object sender, EventArgs e)
        {
            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
            {
                PropertyDescriptor propertyVisible = TypeDescriptor.GetProperties(_ribbonNumericUpDown)["Visible"];
                bool oldValue = (bool)propertyVisible.GetValue(_ribbonNumericUpDown);
                bool newValue = !oldValue;
                _changeService.OnComponentChanged(_ribbonNumericUpDown, null, oldValue, newValue);
                propertyVisible.SetValue(_ribbonNumericUpDown, newValue);
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if ((_ribbonNumericUpDown != null) && (_ribbonNumericUpDown.Ribbon != null))
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, new EventHandler(OnToggleHelpers));
                    _visibleMenu = new ToolStripMenuItem("Visible", null, new EventHandler(OnVisible));
                    _moveFirstMenu = new ToolStripMenuItem("Move NumericUpDown First", Properties.Resources.MoveFirst, new EventHandler(OnMoveFirst));
                    _movePreviousMenu = new ToolStripMenuItem("Move NumericUpDown Previous", Properties.Resources.MovePrevious, new EventHandler(OnMovePrevious));
                    _moveNextMenu = new ToolStripMenuItem("Move NumericUpDown Next", Properties.Resources.MoveNext, new EventHandler(OnMoveNext));
                    _moveLastMenu = new ToolStripMenuItem("Move NumericUpDown Last", Properties.Resources.MoveLast, new EventHandler(OnMoveLast));
                    _deleteNumericUpDownMenu = new ToolStripMenuItem("Delete NumericUpDown", Properties.Resources.delete2, new EventHandler(OnDeleteNumericUpDown));
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _deleteNumericUpDownMenu });
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonNumericUpDown.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = Visible;
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;

                // Show the context menu
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonNumericUpDown.Ribbon.ViewRectangleToPoint(_ribbonNumericUpDown.NumericUpDownView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }

        private TypedRestrictCollection<KryptonRibbonGroupItem> ParentItems
        {
            get
            {
                if (_ribbonNumericUpDown.RibbonContainer is KryptonRibbonGroupTriple)
                {
                    KryptonRibbonGroupTriple triple = (KryptonRibbonGroupTriple)_ribbonNumericUpDown.RibbonContainer;
                    return triple.Items;
                }
                else if (_ribbonNumericUpDown.RibbonContainer is KryptonRibbonGroupLines)
                {
                    KryptonRibbonGroupLines lines = (KryptonRibbonGroupLines)_ribbonNumericUpDown.RibbonContainer;
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
