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
    internal class KryptonTreeViewActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonTreeView _treeView;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonTreeViewActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonTreeViewActionList(KryptonTreeViewDesigner owner)
            : base(owner.Component)
        {
            // Remember the tree view instance
            _treeView = owner.Component as KryptonTreeView;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the syle used for tree items.
        /// </summary>
        public ButtonStyle ItemStyle
        {
            get { return _treeView.ItemStyle; }

            set
            {
                if (_treeView.ItemStyle != value)
                {
                    _service.OnComponentChanged(_treeView, null, _treeView.ItemStyle, value);
                    _treeView.ItemStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the background drawing style.
        /// </summary>
        public PaletteBackStyle BackStyle
        {
            get { return _treeView.BackStyle; }

            set
            {
                if (_treeView.BackStyle != value)
                {
                    _service.OnComponentChanged(_treeView, null, _treeView.BackStyle, value);
                    _treeView.BackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the border drawing style.
        /// </summary>
        public PaletteBorderStyle BorderStyle
        {
            get { return _treeView.BorderStyle; }

            set
            {
                if (_treeView.BorderStyle != value)
                {
                    _service.OnComponentChanged(_treeView, null, _treeView.BorderStyle, value);
                    _treeView.BorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the selection mode.
        /// </summary>
        public bool Sorted
        {
            get { return _treeView.Sorted; }

            set
            {
                if (_treeView.Sorted != value)
                {
                    _service.OnComponentChanged(_treeView, null, _treeView.Sorted, value);
                    _treeView.Sorted = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _treeView.PaletteMode; }
            
            set 
            {
                if (_treeView.PaletteMode != value)
                {
                    _service.OnComponentChanged(_treeView, null, _treeView.PaletteMode, value);
                    _treeView.PaletteMode = value;
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
            if (_treeView != null)
            {
                // Add the list of tree view specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("BackStyle", "Back Style", "Appearance", "Style used to draw background."));
                actions.Add(new DesignerActionPropertyItem("BorderStyle", "Border Style", "Appearance", "Style used to draw the border."));
                actions.Add(new DesignerActionPropertyItem("ItemStyle", "Item Style", "Appearance", "How to display tree items."));
                actions.Add(new DesignerActionHeaderItem("Behavior"));
                actions.Add(new DesignerActionPropertyItem("Sorted", "Sorted", "Behavior", "Should items be sorted according to string."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
