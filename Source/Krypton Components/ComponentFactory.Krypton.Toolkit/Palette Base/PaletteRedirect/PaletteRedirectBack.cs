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
    /// Redirect background based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectBack : PaletteRedirect
    {
        #region Instance Fields
        private IPaletteBack _disabled;
        private IPaletteBack _normal;
        private IPaletteBack _pressed;
        private IPaletteBack _tracking;
        private IPaletteBack _checkedNormal;
        private IPaletteBack _checkedPressed;
        private IPaletteBack _checkedTracking;
        private IPaletteBack _focusOverride;
        private IPaletteBack _normalDefaultOverride;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectBack class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectBack(IPalette target)
            : this(target, null, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectBack class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        public PaletteRedirectBack(IPalette target,
                                   IPaletteBack disabled,
                                   IPaletteBack normal)
            : this(target, disabled, normal, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectBack class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        /// <param name="checkedNormal">Redirection for checked normal state requests.</param>
        /// <param name="checkedPressed">Redirection for checked pressed state requests.</param>
        /// <param name="checkedTracking">Redirection for checked tracking state requests.</param>
        /// <param name="focusOverride">Redirection for focus override state requests.</param>
        /// <param name="normalDefaultOverride">Redirection for normal default override state requests.</param>
        public PaletteRedirectBack(IPalette target,
                                   IPaletteBack disabled,
                                   IPaletteBack normal,
                                   IPaletteBack pressed,
                                   IPaletteBack tracking,
                                   IPaletteBack checkedNormal,
                                   IPaletteBack checkedPressed,
                                   IPaletteBack checkedTracking,
                                   IPaletteBack focusOverride,
                                   IPaletteBack normalDefaultOverride)
            : base(target)
		{
            // Remember state specific inheritance
            _disabled = disabled;
            _normal = normal;
            _pressed = pressed;
            _tracking = tracking;
            _checkedNormal = checkedNormal;
            _checkedPressed = checkedPressed;
            _checkedTracking = checkedTracking;
            _focusOverride = focusOverride;
            _normalDefaultOverride = normalDefaultOverride;
        }
		#endregion

        #region SetRedirectStates
        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        public virtual void SetRedirectStates(IPaletteBack disabled,
                                              IPaletteBack normal)
        {
            _disabled = disabled;
            _normal = normal;
        }
        #endregion

        #region ResetRedirectStates
        /// <summary>
        /// Reset the redirection states to null.
        /// </summary>
        public virtual void ResetRedirectStates()
        {
            _disabled = null;
            _normal = null;
            _pressed = null;
            _tracking = null;
            _checkedNormal = null;
            _checkedPressed = null;
            _checkedTracking = null;
            _focusOverride = null;
            _normalDefaultOverride = null;
        }
        #endregion

        #region Back
        /// <summary>
        /// Gets a value indicating if background should be drawn.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetBackDraw(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackDraw(state);
            else
                return Target.GetBackDraw(style, state);
        }

        /// <summary>
        /// Gets the graphics drawing hint for the background.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBackGraphicsHint(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackGraphicsHint(state);
            else
                return Target.GetBackGraphicsHint(style, state);
        }

        /// <summary>
        /// Gets the first background color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor1(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackColor1(state);
            else
                return Target.GetBackColor1(style, state);
        }

        /// <summary>
        /// Gets the second back color.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackColor2(state);
            else
                return Target.GetBackColor2(style, state);
        }

        /// <summary>
        /// Gets the color background drawing style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackColorStyle(state);
            else
                return Target.GetBackColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBackColorAlign(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackColorAlign(state);
            else
                return Target.GetBackColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color background angle.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBackColorAngle(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackColorAngle(state);
            else
                return Target.GetBackColorAngle(style, state);
        }

        /// <summary>
        /// Gets a background image.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBackImage(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackImage(state);
            else
                return Target.GetBackImage(style, state);
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBackImageStyle(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackImageStyle(state);
            else
                return Target.GetBackImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image alignment.
        /// </summary>
        /// <param name="style">Background style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBackImageAlign(PaletteBackStyle style, PaletteState state)
        {
            IPaletteBack inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBackImageAlign(state);
            else
                return Target.GetBackImageAlign(style, state);
        }
        #endregion

        #region Implementation
        private IPaletteBack GetInherit(PaletteState state)
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
                case PaletteState.CheckedNormal:
                    return _checkedNormal;
                case PaletteState.CheckedPressed:
                    return _checkedPressed;
                case PaletteState.CheckedTracking:
                    return _checkedTracking;
                case PaletteState.FocusOverride:
                    return _focusOverride;
                case PaletteState.NormalDefaultOverride:
                    return _normalDefaultOverride;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }
        #endregion
    }
}
