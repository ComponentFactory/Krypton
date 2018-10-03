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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonGroupBoxActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonGroupBox _groupBox;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonGroupBoxDesigner class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonGroupBoxActionList(KryptonGroupBoxDesigner owner)
            : base(owner.Component)
        {
            // Remember the control instance
            _groupBox = owner.Component as KryptonGroupBox;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the group background style.
        /// </summary>
        public PaletteBackStyle GroupBackStyle
        {
            get { return _groupBox.GroupBackStyle; }
            
            set 
            {
                if (_groupBox.GroupBackStyle != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.GroupBackStyle, value);
                    _groupBox.GroupBackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the group border style.
        /// </summary>
        public PaletteBorderStyle GroupBorderStyle
        {
            get { return _groupBox.GroupBorderStyle; }
            
            set 
            {
                if (_groupBox.GroupBorderStyle != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.GroupBorderStyle, value);
                    _groupBox.GroupBorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the group box label style.
        /// </summary>
        public LabelStyle CaptionStyle
        {
            get { return _groupBox.CaptionStyle; }

            set
            {
                if (_groupBox.CaptionStyle != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.CaptionStyle, value);
                    _groupBox.CaptionStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the caption edge.
        /// </summary>
        public VisualOrientation CaptionEdge
        {
            get { return _groupBox.CaptionEdge; }

            set
            {
                if (_groupBox.CaptionEdge != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.CaptionEdge, value);
                    _groupBox.CaptionEdge = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the caption overlap.
        /// </summary>
        public double CaptionOverlap
        {
            get { return _groupBox.CaptionOverlap; }

            set
            {
                if (_groupBox.CaptionOverlap != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.CaptionOverlap, value);
                    _groupBox.CaptionOverlap = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _groupBox.PaletteMode; }
            
            set 
            {
                if (_groupBox.PaletteMode != value)
                {
                    _service.OnComponentChanged(_groupBox, null, _groupBox.PaletteMode, value);
                    _groupBox.PaletteMode = value;
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
            if (_groupBox != null)
            {
                // Add the list of panel specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("GroupBackStyle", "Back style", "Appearance", "Background style"));
                actions.Add(new DesignerActionPropertyItem("GroupBorderStyle", "Border style", "Appearance", "Border style"));
                actions.Add(new DesignerActionPropertyItem("CaptionStyle", "Caption style", "Appearance", "Caption style"));
                actions.Add(new DesignerActionPropertyItem("CaptionEdge", "Caption edge", "Appearance", "Caption edge"));
                actions.Add(new DesignerActionPropertyItem("CaptionOverlap", "Caption overlap", "Appearance", "Caption overlap"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}
