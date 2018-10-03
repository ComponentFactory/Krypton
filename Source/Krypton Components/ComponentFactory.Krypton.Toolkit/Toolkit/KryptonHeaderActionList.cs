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
    internal class KryptonHeaderActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonHeader _header;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonHeaderActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonHeaderActionList(KryptonHeaderDesigner owner)
            : base(owner.Component)
        {
            // Remember the header instance
            _header = owner.Component as KryptonHeader;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the header style.
        /// </summary>
        public HeaderStyle HeaderStyle
        {
            get { return _header.HeaderStyle; }
            
            set 
            {
                if (_header.HeaderStyle != value)
                {
                    _service.OnComponentChanged(_header, null, _header.HeaderStyle, value);
                    _header.HeaderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the visual orientation.
        /// </summary>
        public VisualOrientation Orientation
        {
            get { return _header.Orientation; }
            
            set 
            {
                if (_header.Orientation != value)
                {
                    _service.OnComponentChanged(_header, null, _header.Orientation, value);
                    _header.Orientation = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the header text.
        /// </summary>
        public string Heading
        {
            get { return _header.Values.Heading; }
            
            set 
            {
                if (_header.Values.Heading != value)
                {
                    _service.OnComponentChanged(_header, null, _header.Values.Heading, value);
                    _header.Values.Heading = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the header description text.
        /// </summary>
        public string Description
        {
            get { return _header.Values.Description; }
            
            set 
            {
                if (_header.Values.Description != value)
                {
                    _service.OnComponentChanged(_header, null, _header.Values.Description, value);
                    _header.Values.Description = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the header image.
        /// </summary>
        public Image Image
        {
            get { return _header.Values.Image; }
            
            set 
            {
                if (_header.Values.Image != value)
                {
                    _service.OnComponentChanged(_header, null, _header.Values.Image, value);
                    _header.Values.Image = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _header.PaletteMode; }
            
            set 
            {
                if (_header.PaletteMode != value)
                {
                    _service.OnComponentChanged(_header, null, _header.PaletteMode, value);
                    _header.PaletteMode = value;
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
            if (_header != null)
            {
                // Add the list of header specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("HeaderStyle", "Style", "Appearance", "Header style"));
                actions.Add(new DesignerActionPropertyItem("Orientation", "Orientation", "Appearance", "Header orientation"));
                actions.Add(new DesignerActionHeaderItem("Values"));
                actions.Add(new DesignerActionPropertyItem("Heading", "Heading", "Values", "Heading text"));
                actions.Add(new DesignerActionPropertyItem("Description", "Description", "Values", "Header description text"));
                actions.Add(new DesignerActionPropertyItem("Image", "Image", "Values", "Heading image"));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
