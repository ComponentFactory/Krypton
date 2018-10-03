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
	public class PaletteRibbonJustTab : Storage
	{
		#region Instance Fields
        private PaletteRibbonDouble _ribbonTab;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteRibbonJustTab class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonJustTab(PaletteRibbonRedirect inherit,
                                    NeedPaintHandler needPaint)
		{
            Debug.Assert(inherit != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

			// Create storage that maps onto the inherit instances
            _ribbonTab = new PaletteRibbonDouble(inherit.RibbonTab, inherit.RibbonTab, needPaint);
        }
		#endregion

        #region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
            get { return (RibbonTab.IsDefault); }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            _ribbonTab.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public virtual void SetInherit(PaletteRibbonRedirect inherit)
        {
            _ribbonTab.SetInherit(inherit.RibbonTab, inherit.RibbonTab);
        }
        #endregion

        #region RibbonTab
        /// <summary>
        /// Gets access to the ribbon tab palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonDouble RibbonTab
        {
            get { return _ribbonTab; }
        }

        private bool ShouldSerializeRibbonTab()
        {
            return !_ribbonTab.IsDefault;
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
