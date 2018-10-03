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
    internal class KryptonRibbonGroupTripleDesigner : ComponentDesigner
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonGroupTriple _ribbonTriple;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _addButtonVerb;
        private DesignerVerb _addColorButtonVerb;
        private DesignerVerb _addCheckBoxVerb;
        private DesignerVerb _addRadioButtonVerb;
        private DesignerVerb _addLabelVerb;
        private DesignerVerb _addCustomControlVerb;
        private DesignerVerb _addTextBoxVerb;
        private DesignerVerb _addMaskedTextBoxVerb;
        private DesignerVerb _addRichTextBoxVerb;
        private DesignerVerb _addComboBoxVerb;
        private DesignerVerb _addNumericUpDownVerb;
        private DesignerVerb _addDomainUpDownVerb;
        private DesignerVerb _addDateTimePickerVerb;
        private DesignerVerb _addTrackBarVerb;
        private DesignerVerb _clearItemsVerb;
        private DesignerVerb _deleteTripleVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
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
        private ToolStripMenuItem _moveToGroupMenu;
        private ToolStripMenuItem _addButtonMenu;
        private ToolStripMenuItem _addColorButtonMenu;
        private ToolStripMenuItem _addCheckBoxMenu;
        private ToolStripMenuItem _addRadioButtonMenu;
        private ToolStripMenuItem _addLabelMenu;
        private ToolStripMenuItem _addCustomControlMenu;
        private ToolStripMenuItem _addTextBoxMenu;
        private ToolStripMenuItem _addMaskedTextBoxMenu;
        private ToolStripMenuItem _addRichTextBoxMenu;
        private ToolStripMenuItem _addComboBoxMenu;
        private ToolStripMenuItem _addNumericUpDownMenu;
        private ToolStripMenuItem _addDomainUpDownMenu;
        private ToolStripMenuItem _addDateTimePickerMenu;
        private ToolStripMenuItem _addTrackBarMenu;
        private ToolStripMenuItem _clearItemsMenu;
        private ToolStripMenuItem _deleteTripleMenu;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupTripleDesigner class.
        /// </summary>
        public KryptonRibbonGroupTripleDesigner()
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
            _ribbonTriple = (KryptonRibbonGroupTriple)component;
            _ribbonTriple.DesignTimeAddButton += new EventHandler(OnAddButton);
            _ribbonTriple.DesignTimeAddColorButton += new EventHandler(OnAddColorButton);
            _ribbonTriple.DesignTimeAddCheckBox += new EventHandler(OnAddCheckBox);
            _ribbonTriple.DesignTimeAddRadioButton += new EventHandler(OnAddRadioButton);
            _ribbonTriple.DesignTimeAddLabel += new EventHandler(OnAddLabel);
            _ribbonTriple.DesignTimeAddCustomControl += new EventHandler(OnAddCustomControl);
            _ribbonTriple.DesignTimeAddTextBox += new EventHandler(OnAddTextBox);
            _ribbonTriple.DesignTimeAddMaskedTextBox += new EventHandler(OnAddMaskedTextBox);
            _ribbonTriple.DesignTimeAddRichTextBox += new EventHandler(OnAddRichTextBox);
            _ribbonTriple.DesignTimeAddComboBox += new EventHandler(OnAddComboBox);
            _ribbonTriple.DesignTimeAddNumericUpDown += new EventHandler(OnAddNumericUpDown);
            _ribbonTriple.DesignTimeAddDomainUpDown += new EventHandler(OnAddDomainUpDown);
            _ribbonTriple.DesignTimeAddDateTimePicker += new EventHandler(OnAddDateTimePicker);
            _ribbonTriple.DesignTimeAddTrackBar += new EventHandler(OnAddTrackBar);
            _ribbonTriple.DesignTimeContextMenu += new MouseEventHandler(OnContextMenu);

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
                compound.AddRange(_ribbonTriple.Items);
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
                    _ribbonTriple.DesignTimeAddButton -= new EventHandler(OnAddButton);
                    _ribbonTriple.DesignTimeAddColorButton -= new EventHandler(OnAddColorButton);
                    _ribbonTriple.DesignTimeAddCheckBox -= new EventHandler(OnAddCheckBox);
                    _ribbonTriple.DesignTimeAddRadioButton -= new EventHandler(OnAddRadioButton);
                    _ribbonTriple.DesignTimeAddLabel -= new EventHandler(OnAddLabel);
                    _ribbonTriple.DesignTimeAddCustomControl -= new EventHandler(OnAddCustomControl);
                    _ribbonTriple.DesignTimeAddTextBox -= new EventHandler(OnAddTextBox);
                    _ribbonTriple.DesignTimeAddMaskedTextBox -= new EventHandler(OnAddMaskedTextBox);
                    _ribbonTriple.DesignTimeAddRichTextBox -= new EventHandler(OnAddRichTextBox);
                    _ribbonTriple.DesignTimeAddComboBox -= new EventHandler(OnAddComboBox);
                    _ribbonTriple.DesignTimeAddNumericUpDown -= new EventHandler(OnAddNumericUpDown);
                    _ribbonTriple.DesignTimeAddDomainUpDown -= new EventHandler(OnAddDomainUpDown);
                    _ribbonTriple.DesignTimeAddDateTimePicker -= new EventHandler(OnAddDateTimePicker);
                    _ribbonTriple.DesignTimeAddTrackBar -= new EventHandler(OnAddTrackBar);
                    _ribbonTriple.DesignTimeContextMenu -= new MouseEventHandler(OnContextMenu);
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
                _moveFirstVerb = new DesignerVerb("Move Triple First", new EventHandler(OnMoveFirst));
                _movePrevVerb = new DesignerVerb("Move Triple Previous", new EventHandler(OnMovePrevious));
                _moveNextVerb = new DesignerVerb("Move Triple Next", new EventHandler(OnMoveNext));
                _moveLastVerb = new DesignerVerb("Move Triple Last", new EventHandler(OnMoveLast));
                _addButtonVerb = new DesignerVerb("Add Button", new EventHandler(OnAddButton));
                _addColorButtonVerb = new DesignerVerb("Add Color Button", new EventHandler(OnAddColorButton));
                _addCheckBoxVerb = new DesignerVerb("Add CheckBox", new EventHandler(OnAddCheckBox));
                _addRadioButtonVerb = new DesignerVerb("Add RadioButton", new EventHandler(OnAddRadioButton));
                _addLabelVerb = new DesignerVerb("Add Label", new EventHandler(OnAddLabel));
                _addCustomControlVerb = new DesignerVerb("Add Custom Control", new EventHandler(OnAddCustomControl));
                _addTextBoxVerb = new DesignerVerb("Add TextBox", new EventHandler(OnAddTextBox));
                _addMaskedTextBoxVerb = new DesignerVerb("Add MaskedTextBox", new EventHandler(OnAddMaskedTextBox));
                _addRichTextBoxVerb = new DesignerVerb("Add RichTextBox", new EventHandler(OnAddRichTextBox));
                _addComboBoxVerb = new DesignerVerb("Add ComboBox", new EventHandler(OnAddComboBox));
                _addNumericUpDownVerb = new DesignerVerb("Add NumericUpDown", new EventHandler(OnAddNumericUpDown));
                _addDomainUpDownVerb = new DesignerVerb("Add DomainUpDown", new EventHandler(OnAddDomainUpDown));
                _addDateTimePickerVerb = new DesignerVerb("Add DateTimePicker", new EventHandler(OnAddDateTimePicker));
                _addTrackBarVerb = new DesignerVerb("Add TrackBar", new EventHandler(OnAddTrackBar));
                _clearItemsVerb = new DesignerVerb("Clear Items", new EventHandler(OnClearItems));
                _deleteTripleVerb = new DesignerVerb("Delete Triple", new EventHandler(OnDeleteTriple));
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, _moveNextVerb, _moveLastVerb, 
                                                     _addButtonVerb, _addColorButtonVerb, _addCheckBoxVerb, _addComboBoxVerb, _addCustomControlVerb, _addDateTimePickerVerb, _addDomainUpDownVerb, _addLabelVerb, _addNumericUpDownVerb, _addRadioButtonVerb, _addRichTextBoxVerb, _addTextBoxVerb, _addTrackBarVerb, _addMaskedTextBoxVerb, _clearItemsVerb, _deleteTripleVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;
            bool add = false;
            bool clearItems = false;

            if ((_ribbonTriple != null) && 
                (_ribbonTriple.Ribbon != null) && 
                _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                moveFirst = (_ribbonTriple.RibbonGroup.Items.IndexOf(_ribbonTriple) > 0);
                movePrev = (_ribbonTriple.RibbonGroup.Items.IndexOf(_ribbonTriple) > 0);
                moveNext = (_ribbonTriple.RibbonGroup.Items.IndexOf(_ribbonTriple) < (_ribbonTriple.RibbonGroup.Items.Count - 1));
                moveLast = (_ribbonTriple.RibbonGroup.Items.IndexOf(_ribbonTriple) < (_ribbonTriple.RibbonGroup.Items.Count - 1));
                add = (_ribbonTriple.Items.Count < 3);
                clearItems = (_ribbonTriple.Items.Count > 0);
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
            _addButtonVerb.Enabled = add;
            _addColorButtonVerb.Enabled = add;
            _addCheckBoxVerb.Enabled = add;
            _addRadioButtonVerb.Enabled = add;
            _addLabelVerb.Enabled = add;
            _addCustomControlVerb.Enabled = add;
            _addTextBoxVerb.Enabled = add;
            _addMaskedTextBoxVerb.Enabled = add;
            _addRichTextBoxVerb.Enabled = add;
            _addComboBoxVerb.Enabled = add;
            _addNumericUpDownVerb.Enabled = add;
            _addDomainUpDownVerb.Enabled = add;
            _addDateTimePickerVerb.Enabled = add;
            _addTrackBarVerb.Enabled = add;
            _clearItemsVerb.Enabled = clearItems;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if ((_ribbonTriple != null) && (_ribbonTriple.Ribbon != null))
                _ribbonTriple.Ribbon.InDesignHelperMode = !_ribbonTriple.Ribbon.InDesignHelperMode;
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple MoveFirst");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    KryptonRibbonGroup ribbonGroup = _ribbonTriple.RibbonGroup;
                    ribbonGroup.Items.Remove(_ribbonTriple);
                    ribbonGroup.Items.Insert(0, _ribbonTriple);
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
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple MovePrevious");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    KryptonRibbonGroup ribbonGroup = _ribbonTriple.RibbonGroup;
                    int index = ribbonGroup.Items.IndexOf(_ribbonTriple) - 1;
                    index = Math.Max(index, 0);
                    ribbonGroup.Items.Remove(_ribbonTriple);
                    ribbonGroup.Items.Insert(index, _ribbonTriple);
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
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple MoveNext");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    KryptonRibbonGroup ribbonGroup = _ribbonTriple.RibbonGroup;
                    int index = ribbonGroup.Items.IndexOf(_ribbonTriple) + 1;
                    index = Math.Min(index, ribbonGroup.Items.Count - 1);
                    ribbonGroup.Items.Remove(_ribbonTriple);
                    ribbonGroup.Items.Insert(index, _ribbonTriple);
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
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple MoveLast");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the triple
                    KryptonRibbonGroup ribbonGroup = _ribbonTriple.RibbonGroup;
                    ribbonGroup.Items.Remove(_ribbonTriple);
                    ribbonGroup.Items.Insert(ribbonGroup.Items.Count, _ribbonTriple);
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
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddButton");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a button item
                    KryptonRibbonGroupButton button = (KryptonRibbonGroupButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupButton));
                    _ribbonTriple.Items.Add(button);

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
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddColorButton");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a button item
                    KryptonRibbonGroupColorButton button = (KryptonRibbonGroupColorButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupColorButton));
                    _ribbonTriple.Items.Add(button);

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

        private void OnAddCheckBox(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddCheckBox");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a check box item.
                    KryptonRibbonGroupCheckBox checkBox = (KryptonRibbonGroupCheckBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupCheckBox));
                    _ribbonTriple.Items.Add(checkBox);

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

        private void OnAddRadioButton(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddRadioButton");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a radio button item.
                    KryptonRibbonGroupRadioButton radioButton = (KryptonRibbonGroupRadioButton)_designerHost.CreateComponent(typeof(KryptonRibbonGroupRadioButton));
                    _ribbonTriple.Items.Add(radioButton);

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

        private void OnAddLabel(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddLabel");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a label item
                    KryptonRibbonGroupLabel label = (KryptonRibbonGroupLabel)_designerHost.CreateComponent(typeof(KryptonRibbonGroupLabel));
                    _ribbonTriple.Items.Add(label);

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

        private void OnAddCustomControl(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddCustomControl");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a custom control item
                    KryptonRibbonGroupCustomControl cc = (KryptonRibbonGroupCustomControl)_designerHost.CreateComponent(typeof(KryptonRibbonGroupCustomControl));
                    _ribbonTriple.Items.Add(cc);

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

        private void OnAddTextBox(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddTextBox");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a textbox item
                    KryptonRibbonGroupTextBox tb = (KryptonRibbonGroupTextBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupTextBox));
                    _ribbonTriple.Items.Add(tb);

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

        private void OnAddTrackBar(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddTrackBar");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a trackbar item
                    KryptonRibbonGroupTrackBar tb = (KryptonRibbonGroupTrackBar)_designerHost.CreateComponent(typeof(KryptonRibbonGroupTrackBar));
                    _ribbonTriple.Items.Add(tb);

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

        private void OnAddMaskedTextBox(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddMaskedTextBox");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a textbox item
                    KryptonRibbonGroupMaskedTextBox mtb = (KryptonRibbonGroupMaskedTextBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupMaskedTextBox));
                    _ribbonTriple.Items.Add(mtb);

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

        private void OnAddRichTextBox(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddRichTextBox");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a richtextbox item
                    KryptonRibbonGroupRichTextBox rtb = (KryptonRibbonGroupRichTextBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupRichTextBox));
                    _ribbonTriple.Items.Add(rtb);

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

        private void OnAddComboBox(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddComboBox");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a combobox item
                    KryptonRibbonGroupComboBox cc = (KryptonRibbonGroupComboBox)_designerHost.CreateComponent(typeof(KryptonRibbonGroupComboBox));
                    _ribbonTriple.Items.Add(cc);

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

        private void OnAddNumericUpDown(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddNumericUpDown");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a numeric up-down item
                    KryptonRibbonGroupNumericUpDown cc = (KryptonRibbonGroupNumericUpDown)_designerHost.CreateComponent(typeof(KryptonRibbonGroupNumericUpDown));
                    _ribbonTriple.Items.Add(cc);

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

        private void OnAddDomainUpDown(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddDomainUpDown");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a domain up-down item
                    KryptonRibbonGroupDomainUpDown cc = (KryptonRibbonGroupDomainUpDown)_designerHost.CreateComponent(typeof(KryptonRibbonGroupDomainUpDown));
                    _ribbonTriple.Items.Add(cc);

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

        private void OnAddDateTimePicker(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple AddDateTimePicker");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Get designer to create a domain up-down item
                    KryptonRibbonGroupDateTimePicker cc = (KryptonRibbonGroupDateTimePicker)_designerHost.CreateComponent(typeof(KryptonRibbonGroupDateTimePicker));
                    _ribbonTriple.Items.Add(cc);

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
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple ClearItems");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Need access to host in order to delete a component
                    IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                    // We need to remove all the items from the triple group
                    for (int i = _ribbonTriple.Items.Count - 1; i >= 0; i--)
                    {
                        KryptonRibbonGroupItem item = _ribbonTriple.Items[i];
                        _ribbonTriple.Items.Remove(item);
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

        private void OnDeleteTriple(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple DeleteTriple");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)["Items"];

                    // Remove the ribbon group from the ribbon tab
                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the triple from the group
                    _ribbonTriple.RibbonGroup.Items.Remove(_ribbonTriple);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonTriple);

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
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.Visible, !_ribbonTriple.Visible);
                _ribbonTriple.Visible = !_ribbonTriple.Visible;
            }
        }

        private void OnMaxLarge(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MaximumSize, GroupItemSize.Large);
                _ribbonTriple.MaximumSize = GroupItemSize.Large;
            }
        }

        private void OnMaxMedium(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MaximumSize, GroupItemSize.Medium);
                _ribbonTriple.MaximumSize = GroupItemSize.Medium;
            }
        }

        private void OnMaxSmall(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MaximumSize, GroupItemSize.Small);
                _ribbonTriple.MaximumSize = GroupItemSize.Small;
            }
        }

        private void OnMinLarge(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MinimumSize, GroupItemSize.Large);
                _ribbonTriple.MinimumSize = GroupItemSize.Large;
            }
        }

        private void OnMinMedium(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MinimumSize, GroupItemSize.Medium);
                _ribbonTriple.MinimumSize = GroupItemSize.Medium;
            }
        }

        private void OnMinSmall(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                _changeService.OnComponentChanged(_ribbonTriple, null, _ribbonTriple.MinimumSize, GroupItemSize.Small);
                _ribbonTriple.MinimumSize = GroupItemSize.Small;
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our triple is being removed
            if (e.Component == _ribbonTriple)
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all items from the triple group
                for (int j = _ribbonTriple.Items.Count - 1; j >= 0; j--)
                {
                    KryptonRibbonGroupItem item = _ribbonTriple.Items[j] as KryptonRibbonGroupItem;
                    _ribbonTriple.Items.Remove(item);
                    host.DestroyComponent(item);
                }
            }
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, new EventHandler(OnToggleHelpers));
                    _visibleMenu = new ToolStripMenuItem("Visible", null, new EventHandler(OnVisible));
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
                    _moveFirstMenu = new ToolStripMenuItem("Move Triple First", Properties.Resources.MoveFirst, new EventHandler(OnMoveFirst));
                    _movePreviousMenu = new ToolStripMenuItem("Move Triple Previous", Properties.Resources.MovePrevious, new EventHandler(OnMovePrevious));
                    _moveNextMenu = new ToolStripMenuItem("Move Triple Next", Properties.Resources.MoveNext, new EventHandler(OnMoveNext));
                    _moveLastMenu = new ToolStripMenuItem("Move Triple Last", Properties.Resources.MoveLast, new EventHandler(OnMoveLast));
                    _moveToGroupMenu = new ToolStripMenuItem("Move Triple To Group");
                    _addButtonMenu = new ToolStripMenuItem("Add Button", Properties.Resources.KryptonRibbonGroupButton, new EventHandler(OnAddButton));
                    _addColorButtonMenu = new ToolStripMenuItem("Add Color Button", Properties.Resources.KryptonRibbonGroupColorButton, new EventHandler(OnAddColorButton));
                    _addCheckBoxMenu = new ToolStripMenuItem("Add CheckBox", Properties.Resources.KryptonRibbonGroupCheckBox, new EventHandler(OnAddCheckBox));
                    _addRadioButtonMenu = new ToolStripMenuItem("Add RadioButton", Properties.Resources.KryptonRibbonGroupRadioButton, new EventHandler(OnAddRadioButton));
                    _addLabelMenu = new ToolStripMenuItem("Add Label", Properties.Resources.KryptonRibbonGroupLabel, new EventHandler(OnAddLabel));
                    _addCustomControlMenu = new ToolStripMenuItem("Add Custom Control", Properties.Resources.KryptonRibbonGroupCustomControl, new EventHandler(OnAddCustomControl));
                    _addTextBoxMenu = new ToolStripMenuItem("Add TextBox", Properties.Resources.KryptonRibbonGroupTextBox, new EventHandler(OnAddTextBox));
                    _addMaskedTextBoxMenu = new ToolStripMenuItem("Add MaskedTextBox", Properties.Resources.KryptonRibbonGroupMaskedTextBox, new EventHandler(OnAddMaskedTextBox));
                    _addRichTextBoxMenu = new ToolStripMenuItem("Add RichTextBox", Properties.Resources.KryptonRibbonGroupRichTextBox, new EventHandler(OnAddRichTextBox));
                    _addComboBoxMenu = new ToolStripMenuItem("Add ComboBox", Properties.Resources.KryptonRibbonGroupComboBox, new EventHandler(OnAddComboBox));
                    _addNumericUpDownMenu = new ToolStripMenuItem("Add NumericUpDown", Properties.Resources.KryptonRibbonGroupNumericUpDown, new EventHandler(OnAddNumericUpDown));
                    _addDomainUpDownMenu = new ToolStripMenuItem("Add DomainUpDown", Properties.Resources.KryptonRibbonGroupDomainUpDown, new EventHandler(OnAddDomainUpDown));
                    _addDateTimePickerMenu = new ToolStripMenuItem("Add DateTimePicker", Properties.Resources.KryptonRibbonGroupDateTimePicker, new EventHandler(OnAddDateTimePicker));
                    _addTrackBarMenu = new ToolStripMenuItem("Add TrackBar", Properties.Resources.KryptonRibbonGroupTrackBar, new EventHandler(OnAddTrackBar));
                    _clearItemsMenu = new ToolStripMenuItem("Clear Items", null, new EventHandler(OnClearItems));
                    _deleteTripleMenu = new ToolStripMenuItem("Delete Triple", Properties.Resources.delete2, new EventHandler(OnDeleteTriple));                    
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, _maximumSizeMenu, _minimumSizeMenu, new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _moveToGroupMenu, new ToolStripSeparator(),
                                                              _addButtonMenu, _addColorButtonMenu, _addCheckBoxMenu, _addComboBoxMenu, _addCustomControlMenu, _addDateTimePickerMenu, _addDomainUpDownMenu, _addLabelMenu, _addNumericUpDownMenu, _addRadioButtonMenu, _addRichTextBoxMenu, _addTextBoxMenu, _addTrackBarMenu, _addMaskedTextBoxMenu, new ToolStripSeparator(),
                                                              _clearItemsMenu, new ToolStripSeparator(),
                                                              _deleteTripleMenu});

                    // Ensure add images have correct transparent background
                    _addButtonMenu.ImageTransparentColor = Color.Magenta;
                    _addColorButtonMenu.ImageTransparentColor = Color.Magenta;
                    _addCheckBoxMenu.ImageTransparentColor = Color.Magenta;
                    _addRadioButtonMenu.ImageTransparentColor = Color.Magenta;
                    _addLabelMenu.ImageTransparentColor = Color.Magenta;
                    _addCustomControlMenu.ImageTransparentColor = Color.Magenta;
                    _addTextBoxMenu.ImageTransparentColor = Color.Magenta;
                    _addMaskedTextBoxMenu.ImageTransparentColor = Color.Magenta;
                    _addRichTextBoxMenu.ImageTransparentColor = Color.Magenta;
                    _addComboBoxMenu.ImageTransparentColor = Color.Magenta;
                    _addNumericUpDownMenu.ImageTransparentColor = Color.Magenta;
                    _addDomainUpDownMenu.ImageTransparentColor = Color.Magenta;
                    _addDateTimePickerMenu.ImageTransparentColor = Color.Magenta;
                    _addTrackBarMenu.ImageTransparentColor = Color.Magenta;
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update sub menu options available for the 'Move To Group'
                UpdateMoveToGroup();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonTriple.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = _ribbonTriple.Visible;
                _maximumLMenu.Checked = (_ribbonTriple.MaximumSize == GroupItemSize.Large);
                _maximumMMenu.Checked = (_ribbonTriple.MaximumSize == GroupItemSize.Medium);
                _maximumSMenu.Checked = (_ribbonTriple.MaximumSize == GroupItemSize.Small);
                _minimumLMenu.Checked = (_ribbonTriple.MinimumSize == GroupItemSize.Large);
                _minimumMMenu.Checked = (_ribbonTriple.MinimumSize == GroupItemSize.Medium);
                _minimumSMenu.Checked = (_ribbonTriple.MinimumSize == GroupItemSize.Small);
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;
                _moveToGroupMenu.Enabled = (_moveToGroupMenu.DropDownItems.Count > 0);
                _addButtonMenu.Enabled = _addButtonVerb.Enabled;
                _addColorButtonMenu.Enabled = _addColorButtonVerb.Enabled;
                _addCheckBoxMenu.Enabled = _addCheckBoxVerb.Enabled;
                _addRadioButtonMenu.Enabled = _addRadioButtonVerb.Enabled;
                _addLabelMenu.Enabled = _addLabelVerb.Enabled;
                _addCustomControlMenu.Enabled = _addCustomControlVerb.Enabled;
                _addTextBoxMenu.Enabled = _addTextBoxVerb.Enabled;
                _addMaskedTextBoxMenu.Enabled = _addMaskedTextBoxVerb.Enabled;
                _addRichTextBoxMenu.Enabled = _addRichTextBoxVerb.Enabled;
                _addComboBoxMenu.Enabled = _addComboBoxVerb.Enabled;
                _addNumericUpDownMenu.Enabled = _addNumericUpDownVerb.Enabled;
                _addDomainUpDownMenu.Enabled = _addDomainUpDownVerb.Enabled;
                _addDateTimePickerMenu.Enabled = _addDateTimePickerVerb.Enabled;
                _addTrackBarMenu.Enabled = _addTrackBarVerb.Enabled;
                _clearItemsMenu.Enabled = _clearItemsVerb.Enabled;

                // Show the context menu
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonTriple.Ribbon.ViewRectangleToPoint(_ribbonTriple.TripleView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }

        private void UpdateMoveToGroup()
        {
            // Remove any existing child items
            _moveToGroupMenu.DropDownItems.Clear();

            if (_ribbonTriple.Ribbon != null)
            {
                // Create a new item per group in the same ribbon tab
                foreach (KryptonRibbonGroup group in _ribbonTriple.RibbonTab.Groups)
                {
                    // Cannot move to ourself, so ignore outself
                    if (group != _ribbonTriple.RibbonGroup)
                    {
                        // Create menu item for the group
                        ToolStripMenuItem groupMenuItem = new ToolStripMenuItem();
                        groupMenuItem.Text = group.TextLine1 + " " + group.TextLine2;
                        groupMenuItem.Tag = group;

                        // Hook into selection of the menu item
                        groupMenuItem.Click += new EventHandler(OnMoveToGroup);

                        // Add to end of the list of options
                        _moveToGroupMenu.DropDownItems.Add(groupMenuItem);
                    }
                }
            }
        }

        private void OnMoveToGroup(object sender, EventArgs e)
        {
            if ((_ribbonTriple != null) &&
                (_ribbonTriple.Ribbon != null) &&
                 _ribbonTriple.RibbonGroup.Items.Contains(_ribbonTriple))
            {
                // Cast to correct type
                ToolStripMenuItem groupMenuItem = (ToolStripMenuItem)sender;

                // Get access to the destination tab
                KryptonRibbonGroup destination = (KryptonRibbonGroup)groupMenuItem.Tag;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupTriple MoveTripleToGroup");

                try
                {
                    // Get access to the Groups property
                    MemberDescriptor oldItems = TypeDescriptor.GetProperties(_ribbonTriple.RibbonGroup)["Items"];
                    MemberDescriptor newItems = TypeDescriptor.GetProperties(destination)["Items"];

                    // Remove the ribbon tab from the ribbon
                    RaiseComponentChanging(null);
                    RaiseComponentChanging(oldItems);
                    RaiseComponentChanging(newItems);

                    // Remove group from current group
                    _ribbonTriple.RibbonGroup.Items.Remove(_ribbonTriple);

                    // Append to the new destination group
                    destination.Items.Add(_ribbonTriple);

                    RaiseComponentChanged(newItems, null, null);
                    RaiseComponentChanged(oldItems, null, null);
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
        #endregion
    }
}
