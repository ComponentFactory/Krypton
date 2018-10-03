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
    internal class KryptonButtonActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonButton _button;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonButtonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonButtonActionList(KryptonButtonDesigner owner) 
            : base(owner.Component)
        {
            // Remember the button instance
            _button = owner.Component as KryptonButton;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the button style.
        /// </summary>
        public ButtonStyle ButtonStyle
        {
            get { return _button.ButtonStyle; }
           
            set
            {
                if (_button.ButtonStyle != value)
                {
                    _service.OnComponentChanged(_button, null, _button.ButtonStyle, value);
                    _button.ButtonStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            get { return _button.Orientation; }
            
            set 
            {
                if (_button.Orientation != value)
                {
                    _service.OnComponentChanged(_button, null, _button.Orientation, value);
                    _button.Orientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button text.
        /// </summary>
        public string Text
        {
            get { return _button.Values.Text; }
            
            set 
            {
                if (_button.Values.Text != value)
                {
                    _service.OnComponentChanged(_button, null, _button.Values.Text, value);
                    _button.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra button text.
        /// </summary>
        public string ExtraText
        {
            get { return _button.Values.ExtraText; }
            
            set 
            {
                if (_button.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_button, null, _button.Values.ExtraText, value);
                    _button.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        public Image Image
        {
            get { return _button.Values.Image; }

            set
            {
                if (_button.Values.Image != value)
                {
                    _service.OnComponentChanged(_button, null, _button.Values.Image, value);
                    _button.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _button.PaletteMode; }
            
            set 
            {
                if (_button.PaletteMode != value)
                {
                    _service.OnComponentChanged(_button, null, _button.PaletteMode, value);
                    _button.PaletteMode = value;
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
            if (_button != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("ButtonStyle", "Style", "Appearance", "Button style"));
                actions.Add(new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Button orientation"));
                actions.Add(new DesignerActionHeaderItem("Values"));
                actions.Add(new DesignerActionPropertyItem("Text", "Text", "Values", "Button text"));
                actions.Add(new DesignerActionPropertyItem("ExtraText", "ExtraText", "Values", "Button extra text"));
                actions.Add(new DesignerActionPropertyItem("Image", "Image", "Values", "Button image"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
