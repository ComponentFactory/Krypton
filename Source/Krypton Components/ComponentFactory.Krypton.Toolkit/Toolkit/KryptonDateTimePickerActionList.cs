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
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonDateTimePickerActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonDateTimePicker _dateTimePicker;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDateTimePickerActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonDateTimePickerActionList(KryptonDateTimePickerDesigner owner)
            : base(owner.Component)
        {
            // Remember the bread crumb control instance
            _dateTimePicker = owner.Component as KryptonDateTimePicker;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the display format.
        /// </summary>
        public DateTimePickerFormat Format
        {
            get { return _dateTimePicker.Format; }

            set
            {
                if (_dateTimePicker.Format != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.Format, value);
                    _dateTimePicker.Format = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the display of up/down buttons.
        /// </summary>
        public bool ShowUpDown
        {
            get { return _dateTimePicker.ShowUpDown; }

            set
            {
                if (_dateTimePicker.ShowUpDown != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.ShowUpDown, value);
                    _dateTimePicker.ShowUpDown = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the display of a check box.
        /// </summary>
        public bool ShowCheckBox
        {
            get { return _dateTimePicker.ShowCheckBox; }

            set
            {
                if (_dateTimePicker.ShowCheckBox != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.ShowCheckBox, value);
                    _dateTimePicker.ShowCheckBox = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the checked state of the check box.
        /// </summary>
        public bool Checked
        {
            get { return _dateTimePicker.Checked; }

            set
            {
                if (_dateTimePicker.Checked != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.Checked, value);
                    _dateTimePicker.Checked = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _dateTimePicker.PaletteMode; }
            
            set 
            {
                if (_dateTimePicker.PaletteMode != value)
                {
                    _service.OnComponentChanged(_dateTimePicker, null, _dateTimePicker.PaletteMode, value);
                    _dateTimePicker.PaletteMode = value;
                }
            }
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_dateTimePicker != null)
            {
                // Add the list of bread crumb specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("Format", "Format", "Appearance", "Decide what to display in the edit portion of the control"));
                actions.Add(new DesignerActionPropertyItem("ShowUpDown", "ShowUpDown", "Appearance", "Display up and down buttons for modifying dates and times"));
                actions.Add(new DesignerActionPropertyItem("ShowCheckBox", "ShowCheckBox", "Appearance", "Display a check box allowing the user to set the value is null"));
                actions.Add(new DesignerActionPropertyItem("Checked", "Checked", "Appearance", "Is the current value null"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
