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
    internal class KryptonManagerActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonManager _manager;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonManagerActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonManagerActionList(KryptonManagerDesigner owner)
            : base(owner.Component)
        {
            // Remember the panel instance
            _manager = owner.Component as KryptonManager;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the global palette mode.
        /// </summary>
        public PaletteModeManager GlobalPaletteMode
        {
            get { return _manager.GlobalPaletteMode; }

            set
            {
                if (_manager.GlobalPaletteMode != value)
                {
                    _service.OnComponentChanged(_manager, null, _manager.GlobalPaletteMode, value);
                    _manager.GlobalPaletteMode = value;
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

            // This can be null when deleting a component instance at design time
            if (_manager != null)
            {
                // Add the list of panel specific actions
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("GlobalPaletteMode", "Global Palette", "Visuals", "Global palette setting"));
            }

            return actions;
        }
        #endregion
    }
}
