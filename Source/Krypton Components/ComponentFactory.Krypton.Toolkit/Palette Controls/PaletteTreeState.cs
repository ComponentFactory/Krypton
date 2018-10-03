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
    /// Implement storage for background, border and node triple.
	/// </summary>
    public class PaletteTreeState : PaletteDouble
	{
		#region Instance Fields
        private PaletteTriple _nodeTriple;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTreeState class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="back">Reference to back storage.</param>
        /// <param name="border">Reference to border storage.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTreeState(PaletteTreeStateRedirect inherit,
                                PaletteBack back,
                                PaletteBorder border,
                                NeedPaintHandler needPaint)
            : base(inherit, back, border, needPaint)
		{
            _nodeTriple = new PaletteTriple(inherit.Node, needPaint);
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
                return (base.IsDefault && _nodeTriple.IsDefault);
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
            _nodeTriple.PopulateFromBase(state);
        }
        #endregion

        #region Node
        /// <summary>
		/// Gets the node appearance overrides.
		/// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining node appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteTriple Node
		{
			get { return _nodeTriple; }
		}

        private bool ShouldSerializeItem()
        {
            return !_nodeTriple.IsDefault;
        }
        #endregion
    }
}
