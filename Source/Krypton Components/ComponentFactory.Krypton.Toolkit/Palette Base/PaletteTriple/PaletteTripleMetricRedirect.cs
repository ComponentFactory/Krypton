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
    /// Redirect storage for a triple palette with palette metrics.
	/// </summary>
	public class PaletteTripleMetricRedirect : PaletteTripleRedirect,
                                               IPaletteMetric
	{
		#region Instance Fields
        private PaletteRedirect _redirect;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTripleMetricRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="backStyle">Style for the background.</param>
        /// <param name="borderStyle">Style for the border.</param>
        /// <param name="contentStyle">Style for the content.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTripleMetricRedirect(PaletteRedirect redirect,
                                           PaletteBackStyle backStyle,
                                           PaletteBorderStyle borderStyle,
                                           PaletteContentStyle contentStyle,
                                           NeedPaintHandler needPaint)
			: base(redirect,
                   backStyle,
                   borderStyle,
                   contentStyle,
                   needPaint)
		{
			Debug.Assert(redirect != null);

            // Remember the redirect reference
            _redirect = redirect;
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
            _redirect = redirect;
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
            // Pass onto the inheritance
            return _redirect.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public virtual InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            // Pass onto the inheritance
            return _redirect.GetMetricBool(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public virtual Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            // Pass onto the inheritance
            return _redirect.GetMetricPadding(state, metric);
        }
        #endregion
    }
}
