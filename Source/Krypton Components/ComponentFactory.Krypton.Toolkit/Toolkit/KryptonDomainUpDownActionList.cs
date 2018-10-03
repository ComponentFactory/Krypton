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
    internal class KryptonDomainUpDownActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonDomainUpDown _domainUpDown;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDomainUpDownActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonDomainUpDownActionList(KryptonDomainUpDownDesigner owner)
            : base(owner.Component)
        {
            // Remember the text box instance
            _domainUpDown = owner.Component as KryptonDomainUpDown;

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
            get { return _domainUpDown.PaletteMode; }
            
            set 
            {
                if (_domainUpDown.PaletteMode != value)
                {
                    _service.OnComponentChanged(_domainUpDown, null, _domainUpDown.PaletteMode, value);
                    _domainUpDown.PaletteMode = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the input control style.
        /// </summary>
        public InputControlStyle InputControlStyle
        {
            get { return _domainUpDown.InputControlStyle; }

            set
            {
                if (_domainUpDown.InputControlStyle != value)
                {
                    _service.OnComponentChanged(_domainUpDown, null, _domainUpDown.InputControlStyle, value);
                    _domainUpDown.InputControlStyle = value;
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
            if (_domainUpDown != null)
            {
                // Add the list of label specific actions
                actions.Add(new DesignerActionHeaderItem("Appearance"));
                actions.Add(new DesignerActionPropertyItem("InputControlStyle", "Style", "Appearance", "DomainUpDown display style."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
