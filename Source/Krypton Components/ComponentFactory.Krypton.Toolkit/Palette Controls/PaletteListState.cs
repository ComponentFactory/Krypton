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
    /// Implement storage for border,background and contained triple.
	/// </summary>
    public class PaletteListState : PaletteDouble
	{
		#region Instance Fields
        private PaletteTriple _itemTriple;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteListState class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteListState(PaletteListStateRedirect inherit,
                                NeedPaintHandler needPaint)
            : base(inherit, needPaint)
		{
            _itemTriple = new PaletteTriple(inherit.Item, needPaint);
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
                return (base.IsDefault && _itemTriple.IsDefault);
			}
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">Which state to populate from.</param>
        public override void PopulateFromBase(PaletteState state)
        {
            base.PopulateFromBase(state);
            _itemTriple.PopulateFromBase(state);
        }
        #endregion

        #region Item
        /// <summary>
		/// Gets the item appearance overrides.
		/// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteTriple Item
		{
			get { return _itemTriple; }
		}

        private bool ShouldSerializeItem()
        {
            return !_itemTriple.IsDefault;
        }
        #endregion
    }
}
