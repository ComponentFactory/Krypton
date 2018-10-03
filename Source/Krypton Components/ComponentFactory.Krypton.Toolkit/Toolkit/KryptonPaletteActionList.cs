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
    internal class KryptonPaletteActionList : DesignerActionList
    {
        #region Instance Fields
        private KryptonPalette _palette;
        private IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonPaletteActionList(KryptonPaletteDesigner owner)
            : base(owner.Component)
        {
            // Remember the panel instance
            _palette = owner.Component as KryptonPalette;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
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
            if (_palette != null)
            {
                // Add the list of panel specific actions
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb("Reset to Defaults", new EventHandler(OnResetClick)), "Actions"));
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb("Populate from Base", new EventHandler(OnPopulateClick)), "Actions"));
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb("Import from Xml file...", new EventHandler(OnImportClick)), "Actions"));
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb("Export to Xml file...", new EventHandler(OnExportClick)), "Actions"));
            }

            return actions;
        }
        #endregion

        #region Implementation
        private void OnResetClick(object sender, EventArgs e)
        {
            if (_palette != null)
            {
                if (MessageBox.Show("Are you sure you want to reset the palette?",
                                    "Palette Reset",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _palette.ResetToDefaults(false);
                    _service.OnComponentChanged(_palette, null, null, null);
                }
            }
        }

        private void OnPopulateClick(object sender, EventArgs e)
        {
            if (_palette != null)
            {
                if (MessageBox.Show("Are you sure you want to populate from the base?",
                                    "Populate From Base",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _palette.PopulateFromBase(false);
                    _service.OnComponentChanged(_palette, null, null, null);
                }
            }
        }

        private void OnImportClick(object sender, EventArgs e)
        {
            if (_palette != null)
            {
                _palette.Import();
                _service.OnComponentChanged(_palette, null, null, null);
            }
        }

        private void OnExportClick(object sender, EventArgs e)
        {
            if (_palette != null)
                _palette.Export();
        }
        #endregion
    }
}
