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
    /// Implement storage for a ribbon focus palette. 
	/// </summary>
    public class PaletteRibbonFocus : PaletteMetricRedirect
	{
		#region Instance Fields
        private PaletteRibbonDouble _ribbonTab;
        private PaletteRibbonDoubleInheritRedirect _ribbonTabInherit;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteRibbonFocus class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Paint delegate.</param>
        public PaletteRibbonFocus(PaletteRedirect redirect,
                                  NeedPaintHandler needPaint)
            : base(redirect)
		{
			Debug.Assert(redirect != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Create the redirection instances
            _ribbonTabInherit = new PaletteRibbonDoubleInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonTab, PaletteRibbonTextStyle.RibbonTab);
			
			// Create storage that maps onto the inherit instances
            _ribbonTab = new PaletteRibbonDouble(_ribbonTabInherit, _ribbonTabInherit, needPaint);
        }
		#endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public override void SetRedirector(PaletteRedirect redirect)
        {
            base.SetRedirector(redirect);
            _ribbonTabInherit.SetRedirector(redirect);
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
                return RibbonTab.IsDefault;
            }
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

        #region Protected
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
