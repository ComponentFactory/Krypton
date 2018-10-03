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
    internal class KryptonBreadCrumbActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonBreadCrumb _breadCrumb;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonBreadCrumbActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonBreadCrumbActionList(KryptonBreadCrumbDesigner owner)
            : base(owner.Component)
        {
            // Remember the bread crumb control instance
            _breadCrumb = owner.Component as KryptonBreadCrumb;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets the background drawing style.
        /// </summary>
        public PaletteBackStyle ControlBackStyle
        {
            get { return _breadCrumb.ControlBackStyle; }

            set
            {
                if (_breadCrumb.ControlBackStyle != value)
                {
                    _service.OnComponentChanged(_breadCrumb, null, _breadCrumb.ControlBackStyle, value);
                    _breadCrumb.ControlBackStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the border drawing style.
        /// </summary>
        public PaletteBorderStyle ControlBorderStyle
        {
            get { return _breadCrumb.ControlBorderStyle; }

            set
            {
                if (_breadCrumb.ControlBorderStyle != value)
                {
                    _service.OnComponentChanged(_breadCrumb, null, _breadCrumb.ControlBorderStyle, value);
                    _breadCrumb.ControlBorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the crumb drawing style.
        /// </summary>
        public ButtonStyle CrumbButtonStyle
        {
            get { return _breadCrumb.CrumbButtonStyle; }

            set
            {
                if (_breadCrumb.CrumbButtonStyle != value)
                {
                    _service.OnComponentChanged(_breadCrumb, null, _breadCrumb.CrumbButtonStyle, value);
                    _breadCrumb.CrumbButtonStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _breadCrumb.PaletteMode; }
            
            set 
            {
                if (_breadCrumb.PaletteMode != value)
                {
                    _service.OnComponentChanged(_breadCrumb, null, _breadCrumb.PaletteMode, value);
                    _breadCrumb.PaletteMode = value;
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
            if (_breadCrumb != null)
            {
                // Add the list of bread crumb specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("ControlBackStyle", "Back Style", "Appearance", "Background drawing style."));
                actions.Add(new DesignerActionPropertyItem("ControlBorderStyle", "Border Style", "Appearance", "Border drawing style."));
                actions.Add(new DesignerActionPropertyItem("CrumbButtonStyle", "Crumb Style", "Appearance", "Crumb drawing style."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
