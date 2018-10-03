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
	/// Implement storage for PaletteForm states.
	/// </summary>
	public class PaletteForm : PaletteDouble,
                               IPaletteMetric
	{
		#region Instance Fields
        private IPaletteMetric _inherit;
        private PaletteTripleMetric _paletteHeader;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteForm class.
		/// </summary>
        /// <param name="inheritForm">Source for inheriting palette defaulted values.</param>
        /// <param name="inheritHeader">Source for inheriting header defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteForm(PaletteFormRedirect inheritForm,
                           PaletteTripleMetricRedirect inheritHeader,
                           NeedPaintHandler needPaint)
            : base(inheritForm, needPaint)
		{
            Debug.Assert(inheritForm != null);
            Debug.Assert(inheritHeader != null);

            // Remember the inheritance
            _inherit = inheritForm;

			// Create the palette storage
            _paletteHeader = new PaletteTripleMetric(inheritHeader, needPaint);
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
                return (base.IsDefault && _paletteHeader.IsDefault);
			}
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        /// <param name="inheritHeader">Source for inheriting.</param>
        public void SetInherit(PaletteForm inheritHeader)
        {
            base.SetInherit(inheritHeader);
            _inherit = inheritHeader;
            _paletteHeader.SetInherit(inheritHeader.Header);
        }
        #endregion

        #region Header
        /// <summary>
		/// Gets access to the header appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining header appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleMetric Header
		{
            get { return _paletteHeader; }
		}

        private bool ShouldSerializeHeader()
		{
			return !_paletteHeader.IsDefault;
		}
		#endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            // Pass onto the inheritance
            return _inherit.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            // Pass onto the inheritance
            return _inherit.GetMetricBool(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            // Always pass onto the inheritance
            return _inherit.GetMetricPadding(state, metric);
        }
        #endregion
    }
}
