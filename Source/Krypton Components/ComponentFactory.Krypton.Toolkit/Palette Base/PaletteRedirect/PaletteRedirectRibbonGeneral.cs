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
    /// Redirect general ribbon values based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectRibbonGeneral : PaletteRedirect
    {
        #region Instance Fields
        private IPaletteRibbonGeneral _disabled;
        private IPaletteRibbonGeneral _normal;
        private IPaletteRibbonGeneral _pressed;
        private IPaletteRibbonGeneral _tracking;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectRibbonGeneral class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectRibbonGeneral(IPalette target)
            : this(target, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectRibbonGeneral class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabled">Redirection for disabled state requests.</param>
        /// <param name="normal">Redirection for normal state requests.</param>
        /// <param name="pressed">Redirection for pressed state requests.</param>
        /// <param name="tracking">Redirection for tracking state requests.</param>
        public PaletteRedirectRibbonGeneral(IPalette target,
                                            IPaletteRibbonGeneral disabled,
                                            IPaletteRibbonGeneral normal,
                                            IPaletteRibbonGeneral pressed,
                                            IPaletteRibbonGeneral tracking
                                           )
            : base(target)
		{
            // Remember state specific inheritance
            _disabled = disabled;
            _normal = normal;
            _pressed = pressed;
            _tracking = tracking;
        }
		#endregion

        #region RibbonGeneral
        /// <summary>
        /// Gets the dark disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color  GetRibbonDisabledDark(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonDisabledDark(state);
            else
                return Target.GetRibbonDisabledDark(state);
        }

        /// <summary>
        /// Gets the light disabled color used for ribbon glyphs.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonDisabledLight(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonDisabledLight(state);
            else
                return Target.GetRibbonDisabledLight(state);
        }

        /// <summary>
        /// Gets the color for the dialog launcher dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupDialogDark(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonGroupDialogDark(state);
            else
                return Target.GetRibbonGroupDialogDark(state);
        }

        /// <summary>
        /// Gets the color for the dialog launcher light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupDialogLight(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonGroupDialogLight(state);
            else
                return Target.GetRibbonGroupDialogLight(state);
        }

        /// <summary>
        /// Gets the color for the group separator dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupSeparatorDark(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonGroupSeparatorDark(state);
            else
                return Target.GetRibbonGroupSeparatorDark(state);
        }

        /// <summary>
        /// Gets the color for the group separator light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonGroupSeparatorLight(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonGroupSeparatorLight(state);
            else
                return Target.GetRibbonGroupSeparatorLight(state);
        }

        /// <summary>
        /// Gets the color for the minimize bar dark.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonMinimizeBarDark(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonMinimizeBarDark(state);
            else
                return Target.GetRibbonMinimizeBarDark(state);
        }

        /// <summary>
        /// Gets the color for the minimize bar light.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonMinimizeBarLight(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonMinimizeBarLight(state);
            else
                return Target.GetRibbonMinimizeBarLight(state);
        }

        /// <summary>
        /// Gets the color for the tab separator.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTabSeparatorColor(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonTabSeparatorColor(state);
            else
                return Target.GetRibbonTabSeparatorColor(state);
        }

        /// <summary>
        /// Gets the color for the tab context separators.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTabSeparatorContextColor(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonTabSeparatorContextColor(state);
            else
                return Target.GetRibbonTabSeparatorContextColor(state);
        }

        /// <summary>
        /// Gets the font for the ribbon text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetRibbonTextFont(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonTextFont(state);
            else
                return Target.GetRibbonTextFont(state);
        }

        /// <summary>
        /// Gets the rendering hint for the ribbon font.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public override PaletteTextHint GetRibbonTextHint(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonTextHint(state);
            else
                return Target.GetRibbonTextHint(state);
        }

        /// <summary>
        /// Gets the color for the extra QAT button dark content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonQATButtonDark(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonQATButtonDark(state);
            else
                return Target.GetRibbonQATButtonDark(state);
        }

        /// <summary>
        /// Gets the color for the extra QAT button light content color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonQATButtonLight(PaletteState state)
        {
            IPaletteRibbonGeneral inherit = GetInherit(state);

            if (inherit != null)
                return inherit.GetRibbonQATButtonLight(state);
            else
                return Target.GetRibbonQATButtonLight(state);
        }
        #endregion

        #region Implementation
        private IPaletteRibbonGeneral GetInherit(PaletteState state)
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
