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
    /// Redirect back/border based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectDouble : PaletteRedirect
    {
        #region Instance Fields
        private IPaletteDouble _disabled;
        private IPaletteDouble _normal;
        private IPaletteDouble _pressed;
        private IPaletteDouble _tracking;
        private IPaletteDouble _checkedNormal;
        private IPaletteDouble _checkedPressed;
        private IPaletteDouble _checkedTracking;
        private IPaletteDouble _focusOverride;
        private IPaletteDouble _normalDefaultOverride;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectDouble class.
        /// </summary>
        public PaletteRedirectDouble()
            : this(null, null, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectDouble class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectDouble(IPalette target)
            : this(target, null, null, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectDouble class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        public PaletteRedirectDouble(IPalette target,
                                     IPaletteDouble disabled,
                                     IPaletteDouble normal)
            : this(target, disabled, normal, null, null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectDouble class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        public PaletteRedirectDouble(IPalette target,
                                     IPaletteDouble disabled,
                                     IPaletteDouble normal,
                                     IPaletteDouble pressed,
                                     IPaletteDouble tracking)
            : this(target, disabled, normal, pressed, tracking, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectDouble class.
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
        public PaletteRedirectDouble(IPalette target,
                                     IPaletteDouble disabled,
                                     IPaletteDouble normal,
                                     IPaletteDouble pressed,
                                     IPaletteDouble tracking,
                                     IPaletteDouble checkedNormal,
                                     IPaletteDouble checkedPressed,
                                     IPaletteDouble checkedTracking,
                                     IPaletteDouble focusOverride,
                                     IPaletteDouble normalDefaultOverride)
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
        public virtual void SetRedirectStates(IPaletteDouble disabled,
                                              IPaletteDouble normal)
        {
            _disabled = disabled;
            _normal = normal;
            _pressed = null;
            _tracking = null;
        }

        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        public virtual void SetRedirectStates(IPaletteDouble disabled,
                                              IPaletteDouble normal,
                                              IPaletteDouble pressed,
                                              IPaletteDouble tracking)
        {
            _disabled = disabled;
            _normal = normal;
            _pressed = pressed;
            _tracking = tracking;
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackDraw(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackGraphicsHint(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColor1(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColor2(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColorStyle(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColorAlign(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackColorAngle(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackImage(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackImageStyle(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBack.GetBackImageAlign(state);
            else
                return Target.GetBackImageAlign(style, state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderDraw(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderDrawBorders(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderGraphicsHint(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColor1(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColor2(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColorStyle(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColorAlign(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderColorAngle(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderWidth(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderRounding(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderImage(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderImageStyle(state);
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
            IPaletteDouble inherit = GetInherit(state);

            if (inherit != null)
                return inherit.PaletteBorder.GetBorderImageAlign(state);
            else
                return Target.GetBorderImageAlign(style, state);
        }    
        #endregion

        #region Implementation
        private IPaletteDouble GetInherit(PaletteState state)
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
