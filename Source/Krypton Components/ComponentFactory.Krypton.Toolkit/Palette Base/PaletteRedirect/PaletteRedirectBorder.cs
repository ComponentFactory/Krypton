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
    /// Redirect border based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectBorder : PaletteRedirect
    {
        #region Instance Fields
        private IPaletteBorder _disabled;
        private IPaletteBorder _normal;
        private IPaletteBorder _pressed;
        private IPaletteBorder _tracking;
        private IPaletteBorder _checkedNormal;
        private IPaletteBorder _checkedPressed;
        private IPaletteBorder _checkedTracking;
        private IPaletteBorder _focusOverride;
        private IPaletteBorder _normalDefaultOverride;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectBorder class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectBorder(IPalette target)
            : this(target, null, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectBorder class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        public PaletteRedirectBorder(IPalette target,
                                     IPaletteBorder disabled,
                                     IPaletteBorder normal)
            : this(target, disabled, normal, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectBorder class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        public PaletteRedirectBorder(IPalette target,
                                     IPaletteBorder disabled,
                                     IPaletteBorder normal,
                                     IPaletteBorder pressed,
                                     IPaletteBorder tracking)
            : this(target, disabled, normal, pressed, tracking, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectBorder class.
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
        public PaletteRedirectBorder(IPalette target,
                                     IPaletteBorder disabled,
                                     IPaletteBorder normal,
                                     IPaletteBorder pressed,
                                     IPaletteBorder tracking,
                                     IPaletteBorder checkedNormal,
                                     IPaletteBorder checkedPressed,
                                     IPaletteBorder checkedTracking,
                                     IPaletteBorder focusOverride,
                                     IPaletteBorder normalDefaultOverride)
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
        public virtual void SetRedirectStates(IPaletteBorder disabled,
                                              IPaletteBorder normal)
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

        #region Border
        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetBorderDraw(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderDraw(state);
            else
                return Target.GetBorderDraw(style, state);
        }

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderDrawBorders(state);
            else
                return Target.GetBorderDrawBorders(style, state);
        }

        /// <summary>
        /// Gets the graphics drawing hint for the border.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderGraphicsHint(state);
            else
                return Target.GetBorderGraphicsHint(style, state);
        }

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderColor1(state);
            else
                return Target.GetBorderColor1(style, state);
        }

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderColor2(state);
            else
                return Target.GetBorderColor2(style, state);
        }

        /// <summary>
        /// Gets the color border drawing style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBorderColorStyle(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderColorStyle(state);
            else
                return Target.GetBorderColorStyle(style, state);
        }

        /// <summary>
        /// Gets the color border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBorderColorAlign(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderColorAlign(state);
            else
                return Target.GetBorderColorAlign(style, state);
        }

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBorderColorAngle(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderColorAngle(state);
            else
                return Target.GetBorderColorAngle(style, state);
        }

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer width.</returns>
        public override int GetBorderWidth(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderWidth(state);
            else
                return Target.GetBorderWidth(style, state);
        }

        /// <summary>
        /// Gets the border corner rounding.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer rounding.</returns>
        public override int GetBorderRounding(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderRounding(state);
            else
                return Target.GetBorderRounding(style, state);
        }

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBorderImage(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderImage(state);
            else
                return Target.GetBorderImage(style, state);
        }

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBorderImageStyle(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderImageStyle(state);
            else
                return Target.GetBorderImageStyle(style, state);
        }

        /// <summary>
        /// Gets the image border alignment.
        /// </summary>
        /// <param name="style">Border style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBorderImageAlign(PaletteBorderStyle style, PaletteState state)
        {
            IPaletteBorder inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetBorderImageAlign(state);
            else
                return Target.GetBorderImageAlign(style, state);
        }    
        #endregion

        #region Implementation
        private IPaletteBorder GetInherit(PaletteState state)
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
