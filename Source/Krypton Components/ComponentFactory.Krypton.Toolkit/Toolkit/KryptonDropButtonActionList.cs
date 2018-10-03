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
    internal class KryptonDropButtonActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonDropButton _dropButton;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDropButtonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonDropButtonActionList(KryptonDropButtonDesigner owner) 
            : base(owner.Component)
        {
            // Remember the button instance
            _dropButton = owner.Component as KryptonDropButton;

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
            get { return _dropButton.ButtonStyle; }
           
            set
            {
                if (_dropButton.ButtonStyle != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.ButtonStyle, value);
                    _dropButton.ButtonStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual button orientation.
        /// </summary>
        public VisualOrientation ButtonOrientation
        {
            get { return _dropButton.ButtonOrientation; }
            
            set 
            {
                if (_dropButton.ButtonOrientation != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.ButtonOrientation, value);
                    _dropButton.ButtonOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual drop down position.
        /// </summary>
        public VisualOrientation DropDownPosition
        {
            get { return _dropButton.DropDownPosition; }

            set
            {
                if (_dropButton.DropDownPosition != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.DropDownPosition, value);
                    _dropButton.DropDownPosition = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual drop down orientation.
        /// </summary>
        public VisualOrientation DropDownOrientation
        {
            get { return _dropButton.DropDownOrientation; }

            set
            {
                if (_dropButton.DropDownOrientation != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.DropDownOrientation, value);
                    _dropButton.DropDownOrientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the splitter or drop down functionality.
        /// </summary>
        public bool Splitter
        {
            get { return _dropButton.Splitter; }

            set
            {
                if (_dropButton.Splitter != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.Splitter, value);
                    _dropButton.Splitter = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button text.
        /// </summary>
        public string Text
        {
            get { return _dropButton.Values.Text; }
            
            set 
            {
                if (_dropButton.Values.Text != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.Values.Text, value);
                    _dropButton.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra button text.
        /// </summary>
        public string ExtraText
        {
            get { return _dropButton.Values.ExtraText; }
            
            set 
            {
                if (_dropButton.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.Values.ExtraText, value);
                    _dropButton.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the button image.
        /// </summary>
        public Image Image
        {
            get { return _dropButton.Values.Image; }

            set
            {
                if (_dropButton.Values.Image != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.Values.Image, value);
                    _dropButton.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _dropButton.PaletteMode; }
            
            set 
            {
                if (_dropButton.PaletteMode != value)
                {
                    _service.OnComponentChanged(_dropButton, null, _dropButton.PaletteMode, value);
                    _dropButton.PaletteMode = value;
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
            if (_dropButton != null)
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
