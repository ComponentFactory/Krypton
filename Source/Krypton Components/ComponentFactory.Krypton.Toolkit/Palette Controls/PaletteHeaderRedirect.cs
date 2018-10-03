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
    /// Redirect storage for a header state.
	/// </summary>
    public class PaletteHeaderRedirect : PaletteTripleMetricRedirect
	{
		#region Instance Fields
        private PaletteRedirect _redirect;
        private Padding _buttonPadding;
        private int _buttonEdgeInset;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteHeaderRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="backStyle">Style for the background.</param>
        /// <param name="borderStyle">Style for the border.</param>
        /// <param name="contentStyle">Style for the content.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteHeaderRedirect(PaletteRedirect redirect,
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

            // Set default value for padding property
            _buttonPadding = CommonHelper.InheritPadding;
            _buttonEdgeInset = -1;
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
                        ButtonPadding.Equals(CommonHelper.InheritPadding) &&
                        (ButtonEdgeInset == -1));
            }
		}
		#endregion

        #region ButtonEdgeInset
        /// <summary>
        /// Gets the sets how far to inset buttons from the header edge.
        /// </summary>
        [Category("Visuals")]
        [Description("How far to inset buttons from the header edge.")]
        [DefaultValue(-1)]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public int ButtonEdgeInset
        {
            get { return _buttonEdgeInset; }

            set
            {
                if (_buttonEdgeInset != value)
                {
                    _buttonEdgeInset = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the ButtonEdgeInset to the default value.
        /// </summary>
        public void ResetButtonEdgeInset()
        {
            ButtonEdgeInset = -1;
        }
        #endregion

        #region ButtonPadding
        /// <summary>
        /// Gets and sets the padding used around each button on the header.
        /// </summary>
        [Category("Visuals")]
        [Description("Padding used around each button on the header.")]
        [DefaultValue(typeof(Padding), "-1,-1,-1,-1")]
        [RefreshPropertiesAttribute(RefreshProperties.All)]
        public Padding ButtonPadding
        {
            get { return _buttonPadding; }

            set
            {
                if (_buttonPadding != value)
                {
                    _buttonPadding = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Reset the ButtonPadding to the default value.
        /// </summary>
        public void ResetButtonPadding()
        {
            ButtonPadding = CommonHelper.InheritPadding;
        }
        #endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public override int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            // Is this the metric we provide?
            if ((metric == PaletteMetricInt.HeaderButtonEdgeInsetPrimary) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetSecondary) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetDockInactive) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetDockActive) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetForm) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetInputControl) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetCustom1) ||
                (metric == PaletteMetricInt.HeaderButtonEdgeInsetCustom2))
            {
                // If the user has defined an actual value to use
                if (ButtonEdgeInset != -1)
                    return ButtonEdgeInset;
            }

            // Pass onto the inheritance
            return _redirect.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
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
        public override Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            // Is this the metric we provide?
            if ((metric == PaletteMetricPadding.HeaderButtonPaddingPrimary) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingSecondary) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingDockInactive) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingDockActive) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingForm) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingInputControl) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingCustom1) ||
                (metric == PaletteMetricPadding.HeaderButtonPaddingCustom2))
            {
                // If the user has defined an actual value to use
                if (!ButtonPadding.Equals(CommonHelper.InheritPadding))
                    return ButtonPadding;
            }

            // Pass onto the inheritance
            return _redirect.GetMetricPadding(state, metric);
        }
        #endregion
    }
}
