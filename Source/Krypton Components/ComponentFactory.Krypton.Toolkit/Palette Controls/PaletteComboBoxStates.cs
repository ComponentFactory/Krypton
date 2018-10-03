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
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Implement storage for just the combo part of a combo box state.
	/// </summary>
	public class PaletteComboBoxStates : Storage
	{
		#region Instance Fields
        private PaletteTriple _itemState;
        private PaletteInputControlTripleStates _comboBoxState;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteComboBoxStates class.
		/// </summary>
        /// <param name="inheritComboBox">Source for inheriting combo box values.</param>
        /// <param name="inheritItem">Source for inheriting item values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteComboBoxStates(IPaletteTriple inheritComboBox,
                                     IPaletteTriple inheritItem,
                                     NeedPaintHandler needPaint)
		{
            Debug.Assert(inheritComboBox != null);
            Debug.Assert(inheritItem != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create storage that maps onto the inherit instances
            _itemState = new PaletteTriple(inheritItem, needPaint);
            _comboBoxState = new PaletteInputControlTripleStates(inheritComboBox, needPaint);
		}
		#endregion

		#region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
			get 
			{
                return (ComboBox.IsDefault &&
                        Item.IsDefault);
			}
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        /// <param name="inheritComboBox">Source for inheriting combo box values.</param>
        /// <param name="inheritItem">Source for inheriting item values.</param>
        public void SetInherit(IPaletteTriple inheritComboBox,
                               IPaletteTriple inheritItem)
        {
            _comboBoxState.SetInherit(inheritComboBox);
            _itemState.SetInherit(inheritItem);
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Palette state to use when populating.</param>
        public void PopulateFromBase(PaletteState state)
        {
            _comboBoxState.PopulateFromBase(state);
            _itemState.PopulateFromBase(state);
        }
        #endregion

        #region ComboBox
        /// <summary>
        /// Gets access to the combo box appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining combo box appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteInputControlTripleStates ComboBox
        {
            get { return _comboBoxState; }
        }

        private bool ShouldSerializeComboBox()
        {
            return !_comboBoxState.IsDefault;
        }
        #endregion

        #region Item
        /// <summary>
        /// Gets access to the item appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple Item
        {
            get { return _itemState; }
        }

        private bool ShouldSerializeItem()
        {
            return !_itemState.IsDefault;
        }
        #endregion

		#region Implementation
		/// <summary>
		/// Handle a change event from palette source.
		/// </summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="needLayout">True if a layout is also needed.</param>
        protected void OnNeedPaint(object sender, bool needLayout)
		{
			// Pass request from child to our own handler
			PerformNeedPaint(needLayout);
		}
		#endregion
	}
}
