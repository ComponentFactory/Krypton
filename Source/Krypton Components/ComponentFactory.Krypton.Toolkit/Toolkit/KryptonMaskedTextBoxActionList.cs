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
    internal class KryptonMaskedTextBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonMaskedTextBox _maskedTextBox;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonMaskedTextBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonMaskedTextBoxActionList(KryptonMaskedTextBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the text box instance
            _maskedTextBox = owner.Component as KryptonMaskedTextBox;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _maskedTextBox.PaletteMode; }
            
            set 
            {
                if (_maskedTextBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_maskedTextBox, null, _maskedTextBox.PaletteMode, value);
                    _maskedTextBox.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        public InputControlStyle InputControlStyle
        {
            get { return _maskedTextBox.InputControlStyle; }

            set
            {
                if (_maskedTextBox.InputControlStyle != value)
                {
                    _service.OnComponentChanged(_maskedTextBox, null, _maskedTextBox.InputControlStyle, value);
                    _maskedTextBox.InputControlStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input mask.
        /// </summary>
        public string Mask
        {
            get { return _maskedTextBox.Mask; }

            set
            {
                if (_maskedTextBox.Mask != value)
                {
                    _service.OnComponentChanged(_maskedTextBox, null, _maskedTextBox.Mask, value);
                    _maskedTextBox.Mask = value;
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
            if (_maskedTextBox != null)
            {
                // Add the list of label specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("InputControlStyle", "Style", "Appearance", "TextBox display style."));
                actions.Add(new DesignerActionHeaderItem("MaskedTextBox"));
                actions.Add(new DesignerActionPropertyItem("Mask", "Mask", "MaskedTextBox", "Input mask."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
