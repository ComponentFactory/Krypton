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
    internal class KryptonColorButtonActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonColorButton _colorButton;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonColorButtonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonColorButtonActionList(KryptonColorButtonDesigner owner) 
            : base(owner.Component)
        {
            // Remember the button instance
            _colorButton = owner.Component as KryptonColorButton;

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
            get { return _colorButton.ButtonStyle; }
           
            set
            {
                if (_colorButton.ButtonStyle != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.ButtonStyle, value);
                    _colorButton.ButtonStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual button orientation.
        /// </summary>
        public VisualOrientation ButtonOrientation
        {
            get { return _colorButton.ButtonOrientation; }
            
            set 
            {
                if (_colorButton.ButtonOrientation != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.ButtonOrientation, value);
                    _colorButton.ButtonOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual drop down position.
        /// </summary>
        public VisualOrientation DropDownPosition
        {
            get { return _colorButton.DropDownPosition; }

            set
            {
                if (_colorButton.DropDownPosition != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.DropDownPosition, value);
                    _colorButton.DropDownPosition = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual drop down orientation.
        /// </summary>
        public VisualOrientation DropDownOrientation
        {
            get { return _colorButton.DropDownOrientation; }

            set
            {
                if (_colorButton.DropDownOrientation != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.DropDownOrientation, value);
                    _colorButton.DropDownOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the splitter or drop down functionality.
        /// </summary>
        public bool Splitter
        {
            get { return _colorButton.Splitter; }

            set
            {
                if (_colorButton.Splitter != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.Splitter, value);
                    _colorButton.Splitter = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button text.
        /// </summary>
        public string Text
        {
            get { return _colorButton.Values.Text; }
            
            set 
            {
                if (_colorButton.Values.Text != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.Values.Text, value);
                    _colorButton.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra button text.
        /// </summary>
        public string ExtraText
        {
            get { return _colorButton.Values.ExtraText; }
            
            set 
            {
                if (_colorButton.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.Values.ExtraText, value);
                    _colorButton.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        public Image Image
        {
            get { return _colorButton.Values.Image; }

            set
            {
                if (_colorButton.Values.Image != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.Values.Image, value);
                    _colorButton.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _colorButton.PaletteMode; }
            
            set 
            {
                if (_colorButton.PaletteMode != value)
                {
                    _service.OnComponentChanged(_colorButton, null, _colorButton.PaletteMode, value);
                    _colorButton.PaletteMode = value;
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
            if (_colorButton != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("Splitter", "Splitter", "Appearance", "Splitter of DropDown"));
                actions.Add(new DesignerActionPropertyItem("ButtonStyle", "ButtonStyle", "Appearance", "Button style"));
                actions.Add(new DesignerActionPropertyItem("ButtonOrientation", "ButtonOrientation", "Appearance", "Button orientation"));
                actions.Add(new DesignerActionPropertyItem("DropDownPosition", "DropDownPosition", "Appearance", "DropDown position"));
                actions.Add(new DesignerActionPropertyItem("DropDownOrientation", "DropDownOrientation", "Appearance", "DropDown orientation"));
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
