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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Implement storage for a ribbon state.
	/// </summary>
    public class PaletteRibbonGroupAreaTab : PaletteRibbonJustTab
	{
		#region Instance Fields
        private PaletteRibbonBack _ribbonGroupArea;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteRibbonGroupAreaTab class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonGroupAreaTab(PaletteRibbonRedirect inherit,
                                         NeedPaintHandler needPaint)
            : base(inherit, needPaint)
		{
			// Create storage that maps onto the inherit instances
            _ribbonGroupArea = new PaletteRibbonBack(inherit.RibbonGroupArea, needPaint);
        }
		#endregion

        #region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
            get { return (base.IsDefault &&
                          RibbonGroupArea.IsDefault); }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public override void PopulateFromBase(PaletteState state)
        {
            base.PopulateFromBase(state);
            _ribbonGroupArea.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public override void SetInherit(PaletteRibbonRedirect inherit)
        {
            base.SetInherit(inherit);
            _ribbonGroupArea.SetInherit(inherit.RibbonGroupArea);
        }
        #endregion

        #region RibbonGroupArea
        /// <summary>
        /// Gets access to the ribbon group area palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group area appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupArea
        {
            get { return _ribbonGroupArea; }
        }

        private bool ShouldSerializeRibbonGroupArea()
        {
            return !_ribbonGroupArea.IsDefault;
        }
        #endregion
	}
}
