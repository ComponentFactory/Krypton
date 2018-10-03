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
    internal class KryptonListBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonListBox _listBox;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonListBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonListBoxActionList(KryptonListBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the list box instance
            _listBox = owner.Component as KryptonListBox;

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
            get { return _listBox.ItemStyle; }

            set
            {
                if (_listBox.ItemStyle != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.ItemStyle, value);
                    _listBox.ItemStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the background drawing style.
        /// </summary>
        public PaletteBackStyle BackStyle
        {
            get { return _listBox.BackStyle; }

            set
            {
                if (_listBox.BackStyle != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.BackStyle, value);
                    _listBox.BackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the border drawing style.
        /// </summary>
        public PaletteBorderStyle BorderStyle
        {
            get { return _listBox.BorderStyle; }

            set
            {
                if (_listBox.BorderStyle != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.BorderStyle, value);
                    _listBox.BorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the selection mode.
        /// </summary>
        public SelectionMode SelectionMode
        {
            get { return _listBox.SelectionMode; }

            set
            {
                if (_listBox.SelectionMode != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.SelectionMode, value);
                    _listBox.SelectionMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the selection mode.
        /// </summary>
        public bool Sorted
        {
            get { return _listBox.Sorted; }

            set
            {
                if (_listBox.Sorted != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.Sorted, value);
                    _listBox.Sorted = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _listBox.PaletteMode; }
            
            set 
            {
                if (_listBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_listBox, null, _listBox.PaletteMode, value);
                    _listBox.PaletteMode = value;
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
            if (_listBox != null)
            {
                // Add the list of list box specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("BackStyle", "Back Style", "Appearance", "Style used to draw background."));
                actions.Add(new DesignerActionPropertyItem("BorderStyle", "Border Style", "Appearance", "Style used to draw the border."));
                actions.Add(new DesignerActionPropertyItem("ItemStyle", "Item Style", "Appearance", "How to display list items."));
                actions.Add(new DesignerActionHeaderItem("Behavior"));
                actions.Add(new DesignerActionPropertyItem("SelectionMode", "Selection Mode", "Behavior", "Determines the selection mode."));
                actions.Add(new DesignerActionPropertyItem("Sorted", "Sorted", "Behavior", "Should items be sorted according to string."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
