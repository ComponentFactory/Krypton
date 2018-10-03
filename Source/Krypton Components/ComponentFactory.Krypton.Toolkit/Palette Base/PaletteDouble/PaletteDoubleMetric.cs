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
    /// Implement a double palette that exposes palette metrics.
	/// </summary>
	public class PaletteDoubleMetric : PaletteDouble, 
                                       IPaletteMetric
	{
		#region Instance Fields
        private PaletteDoubleMetricRedirect _inherit;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteDoubleMetric class.
		/// </summary>
        /// <param name="inherit">Source for palette defaulted values.</param>
        public PaletteDoubleMetric(PaletteDoubleMetricRedirect inherit)
            : this(inherit, null)
        {
        }

		/// <summary>
        /// Initialize a new instance of the PaletteDoubleMetric class.
		/// </summary>
        /// <param name="inherit">Source for palette defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteDoubleMetric(PaletteDoubleMetricRedirect inherit,
                                   NeedPaintHandler needPaint)
            : base(inherit, needPaint)
		{
            Debug.Assert(inherit != null);
            
            // Remember inheritance for metric values
            _inherit = inherit;
        }
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public void SetInherit(PaletteDoubleMetricRedirect inherit)
        {
            base.SetInherit(inherit);
            _inherit = inherit;
        }
        #endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public virtual int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            // Always pass onto the inheritance
            return _inherit.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public virtual InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            // Always pass onto the inheritance
            return _inherit.GetMetricBool(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public virtual Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            // Always pass onto the inheritance
            return _inherit.GetMetricPadding(state, metric);
        }
        #endregion
    }
}
