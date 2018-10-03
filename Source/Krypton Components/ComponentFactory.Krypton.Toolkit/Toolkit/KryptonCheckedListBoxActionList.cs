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
    internal class KryptonCheckedListBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonCheckedListBox _checkedListBox;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCheckedListBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonCheckedListBoxActionList(KryptonCheckedListBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the list box instance
            _checkedListBox = owner.Component as KryptonCheckedListBox;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the syle used for list items.
        /// </summary>
        public ButtonStyle ItemStyle
        {
            get { return _checkedListBox.ItemStyle; }

            set
            {
                if (_checkedListBox.ItemStyle != value)
                {
                    _service.OnComponentChanged(_checkedListBox, null, _checkedListBox.ItemStyle, value);
                    _checkedListBox.ItemStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the background drawing style.
        /// </summary>
        public PaletteBackStyle BackStyle
        {
            get { return _checkedListBox.BackStyle; }

            set
            {
                if (_checkedListBox.BackStyle != value)
                {
                    _service.OnComponentChanged(_checkedListBox, null, _checkedListBox.BackStyle, value);
                    _checkedListBox.BackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the border drawing style.
        /// </summary>
        public PaletteBorderStyle BorderStyle
        {
            get { return _checkedListBox.BorderStyle; }

            set
            {
                if (_checkedListBox.BorderStyle != value)
                {
                    _service.OnComponentChanged(_checkedListBox, null, _checkedListBox.BorderStyle, value);
                    _checkedListBox.BorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the selection mode.
        /// </summary>
        public CheckedSelectionMode SelectionMode
        {
            get { return _checkedListBox.SelectionMode; }

            set
            {
                if (_checkedListBox.SelectionMode != value)
                {
                    _service.OnComponentChanged(_checkedListBox, null, _checkedListBox.SelectionMode, value);
                    _checkedListBox.SelectionMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the selection mode.
        /// </summary>
        public bool Sorted
        {
            get { return _checkedListBox.Sorted; }

            set
            {
                if (_checkedListBox.Sorted != value)
                {
                    _service.OnComponentChanged(_checkedListBox, null, _checkedListBox.Sorted, value);
                    _checkedListBox.Sorted = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the check on click setting.
        /// </summary>
        public bool CheckOnClick
        {
            get { return _checkedListBox.CheckOnClick; }

            set
            {
                if (_checkedListBox.CheckOnClick != value)
                {
                    _service.OnComponentChanged(_checkedListBox, null, _checkedListBox.CheckOnClick, value);
                    _checkedListBox.CheckOnClick = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _checkedListBox.PaletteMode; }
            
            set 
            {
                if (_checkedListBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_checkedListBox, null, _checkedListBox.PaletteMode, value);
                    _checkedListBox.PaletteMode = value;
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
            if (_checkedListBox != null)
            {
                // Add the list of list box specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("BackStyle", "Back Style", "Appearance", "Style used to draw background."));
                actions.Add(new DesignerActionPropertyItem("BorderStyle", "Border Style", "Appearance", "Style used to draw the border."));
                actions.Add(new DesignerActionPropertyItem("ItemStyle", "Item Style", "Appearance", "How to display list items."));
                actions.Add(new DesignerActionHeaderItem("Behavior"));
                actions.Add(new DesignerActionPropertyItem("SelectionMode", "Selection Mode", "Behavior", "Determines the selection mode."));
                actions.Add(new DesignerActionPropertyItem("Sorted", "Sorted", "Behavior", "Should items be sorted according to string."));
                actions.Add(new DesignerActionPropertyItem("CheckOnClick", "CheckOnClick", "Behavior", "Should clicking an item toggle its checked state."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
