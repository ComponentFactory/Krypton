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
    internal class KryptonCheckBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonCheckBox _checkBox;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCheckBoxActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonCheckBoxActionList(KryptonCheckBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the checkbox instance
            _checkBox = owner.Component as KryptonCheckBox;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets a value indicating if the check box is checked.
        /// </summary>
        public bool Checked
        {
            get { return _checkBox.Checked; }

            set
            {
                if (_checkBox.Checked != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.Checked, value);
                    _checkBox.Checked = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the checked state.
        /// </summary>
        public CheckState CheckState
        {
            get { return _checkBox.CheckState; }

            set
            {
                if (_checkBox.CheckState != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.CheckState, value);
                    _checkBox.CheckState = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if the check box should be three state.
        /// </summary>
        public bool ThreeState
        {
            get { return _checkBox.ThreeState; }

            set
            {
                if (_checkBox.ThreeState != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.ThreeState, value);
                    _checkBox.ThreeState = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if the check box should be three state.
        /// </summary>
        public bool AutoCheck
        {
            get { return _checkBox.AutoCheck; }

            set
            {
                if (_checkBox.AutoCheck != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.AutoCheck, value);
                    _checkBox.AutoCheck = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the label style.
        /// </summary>
        public LabelStyle LabelStyle
        {
            get { return _checkBox.LabelStyle; }
            
            set 
            {
                if (_checkBox.LabelStyle != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.LabelStyle, value);
                    _checkBox.LabelStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            get { return _checkBox.Orientation; }
            
            set
            {
                if (_checkBox.Orientation != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.Orientation, value);
                    _checkBox.Orientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the checkbox text.
        /// </summary>
        public string Text
        {
            get { return _checkBox.Values.Text; }
            
            set 
            {
                if (_checkBox.Values.Text != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.Values.Text, value);
                    _checkBox.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra checkbox text.
        /// </summary>
        public string ExtraText
        {
            get { return _checkBox.Values.ExtraText; }
            
            set 
            {
                if (_checkBox.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.Values.ExtraText, value);
                    _checkBox.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the checkbox image.
        /// </summary>
        public Image Image
        {
            get { return _checkBox.Values.Image; }
            
            set 
            {
                if (_checkBox.Values.Image != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.Values.Image, value);
                    _checkBox.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _checkBox.PaletteMode; }
            
            set 
            {
                if (_checkBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_checkBox, null, _checkBox.PaletteMode, value);
                    _checkBox.PaletteMode = value;
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
            if (_checkBox != null)
            {
                // Add the list of checkbox specific actions
                actions.Add(new DesignerActionHeaderItem("Operation"));
                actions.Add(new DesignerActionPropertyItem("Checked", "Checked", "Operation", "Checked state"));
                actions.Add(new DesignerActionPropertyItem("AutoCheck", "AutoCheck", "Operation", "AutoCheck of other instances."));
                actions.Add(new DesignerActionPropertyItem("ThreeState", "ThreeState", "Operation", "ThreeState setting"));
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("LabelStyle", "Style", "Appearance", "Label style"));
                actions.Add(new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Visual orientation"));
                actions.Add(new DesignerActionHeaderItem("Values"));
                actions.Add(new DesignerActionPropertyItem("Text", "Text", "Values", "Checkbox text"));
                actions.Add(new DesignerActionPropertyItem("ExtraText", "ExtraText", "Values", "Checkbox extra text"));
                actions.Add(new DesignerActionPropertyItem("Image", "Image", "Values", "Checkbox image"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
