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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Redirect back/border/metrics based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectDoubleMetric : PaletteRedirectDouble
    {
        #region Instance Fields
        private IPaletteMetric _disabled;
        private IPaletteMetric _normal;
        private IPaletteMetric _pressed;
        private IPaletteMetric _tracking;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectDoubleMetric class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectDoubleMetric(IPalette target)
            : this(target, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectDoubleMetric class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="disableMetric">Redirection for disabled metric requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="normalMetric">Redirection for normal metric requests.</param>
        public PaletteRedirectDoubleMetric(IPalette target,
                                           IPaletteDouble disabled, IPaletteMetric disableMetric,
                                           IPaletteDouble normal, IPaletteMetric normalMetric)
            : base(target, disabled, normal)
        {
            // Remember state specific inheritance
            _disabled = disableMetric;
            _normal = normalMetric;
        }
		#endregion

        #region SetRedirectStates
        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="disableMetric">Redirection for disabled metric requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="normalMetric">Redirection for normal metric requests.</param>
        public void SetRedirectStates(IPaletteDouble disabled,
                                      IPaletteMetric disableMetric,
                                      IPaletteDouble normal,
                                      IPaletteMetric normalMetric)
        {
            base.SetRedirectStates(disabled, normal);

            _disabled = disableMetric;
            _normal = normalMetric;
            _pressed = null;
            _tracking = null;
        }

        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="disableMetric">Redirection for disabled metric requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="normalMetric">Redirection for normal metric requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="pressedMetric">Redirection for pressed metric requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        /// <param name="trackingMetric">Redirection for tracking metric requests.</param>
        public void SetRedirectStates(IPaletteDouble disabled,
                                      IPaletteMetric disableMetric,
                                      IPaletteDouble normal,
                                      IPaletteMetric normalMetric,
                                      IPaletteDouble pressed,
                                      IPaletteMetric pressedMetric,
                                      IPaletteDouble tracking,
                                      IPaletteMetric trackingMetric)
        {
            base.SetRedirectStates(disabled, normal, pressed, tracking);

            _disabled = disableMetric;
            _normal = normalMetric;
            _pressed = pressedMetric;
            _tracking = trackingMetric;
        }
        #endregion

        #region ResetRedirectStates
        /// <summary>
        /// Reset the redirection states to null.
        /// </summary>
        public override void ResetRedirectStates()
        {
            base.ResetRedirectStates();

            _disabled = null;
            _normal = null;
            _pressed = null;
            _tracking = null;
        }
        #endregion

        #region Metric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public override int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            IPaletteMetric inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetMetricInt(state, metric);
            else
                return Target.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a boolean metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetMetricBool(PaletteState state, PaletteMetricBool metric)
        {
            IPaletteMetric inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetMetricBool(state, metric);
            else
                return Target.GetMetricBool(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            IPaletteMetric inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetMetricPadding(state, metric);
            else
                return Target.GetMetricPadding(state, metric);
        }
        #endregion

        #region Implementation
        private IPaletteMetric GetInherit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return _disabled;
                case PaletteState.Normal:
                    return _normal;
                case PaletteState.Pressed:
                    return _pressed;
                case PaletteState.Tracking:
                    return _tracking;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }
        #endregion
    }
}
