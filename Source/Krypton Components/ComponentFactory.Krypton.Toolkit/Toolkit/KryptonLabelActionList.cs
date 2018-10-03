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
    internal class KryptonLabelActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonLabel _label;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonLabelActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonLabelActionList(KryptonLabelDesigner owner)
            : base(owner.Component)
        {
            // Remember the label instance
            _label = owner.Component as KryptonLabel;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the label style.
        /// </summary>
        public LabelStyle LabelStyle
        {
            get { return _label.LabelStyle; }
            
            set 
            {
                if (_label.LabelStyle != value)
                {
                    _service.OnComponentChanged(_label, null, _label.LabelStyle, value);
                    _label.LabelStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            get { return _label.Orientation; }
            
            set
            {
                if (_label.Orientation != value)
                {
                    _service.OnComponentChanged(_label, null, _label.Orientation, value);
                    _label.Orientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the label text.
        /// </summary>
        public string Text
        {
            get { return _label.Values.Text; }
            
            set 
            {
                if (_label.Values.Text != value)
                {
                    _service.OnComponentChanged(_label, null, _label.Values.Text, value);
                    _label.Values.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the extra label text.
        /// </summary>
        public string ExtraText
        {
            get { return _label.Values.ExtraText; }
            
            set 
            {
                if (_label.Values.ExtraText != value)
                {
                    _service.OnComponentChanged(_label, null, _label.Values.ExtraText, value);
                    _label.Values.ExtraText = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the label image.
        /// </summary>
        public Image Image
        {
            get { return _label.Values.Image; }
            
            set 
            {
                if (_label.Values.Image != value)
                {
                    _service.OnComponentChanged(_label, null, _label.Values.Image, value);
                    _label.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _label.PaletteMode; }
            
            set 
            {
                if (_label.PaletteMode != value)
                {
                    _service.OnComponentChanged(_label, null, _label.PaletteMode, value);
                    _label.PaletteMode = value;
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
            if (_label != null)
            {
                // Add the list of label specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("LabelStyle", "Style", "Appearance", "Label style"));
                actions.Add(new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Visual orientation"));
                actions.Add(new DesignerActionHeaderItem("Values"));
                actions.Add(new DesignerActionPropertyItem("Text", "Text", "Values", "Label text"));
                actions.Add(new DesignerActionPropertyItem("ExtraText", "ExtraText", "Values", "Label extra text"));
                actions.Add(new DesignerActionPropertyItem("Image", "Image", "Values", "Label image"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
