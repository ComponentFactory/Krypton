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
    /// Redirect storage for headers within a HeaderGroup state.
	/// </summary>
    public class PaletteHeaderPaddingRedirect : PaletteHeaderButtonRedirect
	{
		#region Instance Fields
        private PaletteRedirect _redirect;
        private Padding _headerPadding;
        #endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the PaletteHeaderPaddingRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
		/// <param name="contentStyle">Initial content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteHeaderPaddingRedirect(PaletteRedirect redirect,
											PaletteBackStyle backStyle,
											PaletteBorderStyle borderStyle,
											PaletteContentStyle contentStyle,
                                            NeedPaintHandler needPaint)
            : base(redirect, backStyle, borderStyle, contentStyle, needPaint)
		{
            Debug.Assert(redirect != null);

            // Remember the redirect reference
            _redirect = redirect;

			// Set default value for padding property
            _headerPadding = CommonHelper.InheritPadding;
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
                return (base.IsDefault &&
                        HeaderPadding.Equals(CommonHelper.InheritPadding));
            }
		}
		#endregion

        #region HeaderPadding
        /// <summary>
        /// Gets and sets the padding used to inset the header within the HeaderGroup
		/// </summary>
		[Category("Visuals")]
        [Description("Padding used to inset the header within the HeaderGroup.")]
		[DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
		[RefreshPropertiesAttribute(RefreshProperties.All)]
		public Padding HeaderPadding
		{
			get { return _headerPadding; }

			set
			{
				if (_headerPadding != value)
				{
					_headerPadding = value;
					PerformNeedPaint(true);
				}
			}
		}

		/// <summary>
        /// Reset the HeaderPadding to the default value.
		/// </summary>
        public void ResetHeaderPadding()
		{
            HeaderPadding = CommonHelper.InheritPadding;
		}
		#endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            // Is this the metric we provide?
            if ((metric == PaletteMetricPadding.HeaderGroupPaddingPrimary) ||
                (metric == PaletteMetricPadding.HeaderGroupPaddingSecondary) ||
                (metric == PaletteMetricPadding.HeaderGroupPaddingDockInactive) ||
                (metric == PaletteMetricPadding.HeaderGroupPaddingDockActive))
            {
                // If the user has defined an actual value to use
                if (!HeaderPadding.Equals(CommonHelper.InheritPadding))
                    return HeaderPadding;
            }

            // Let base class perform its own testing
            return base.GetMetricPadding(state, metric);
        }
        #endregion
    }
}
