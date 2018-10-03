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
    /// Redirect back/border/text ribbon values based on the incoming state of the request.
    /// </summary>
    public class PaletteRedirectRibbonDouble : PaletteRedirect
    {
        #region Instance Fields
        private IPaletteRibbonBack _disabledBack;
        private IPaletteRibbonBack _normalBack;
        private IPaletteRibbonBack _pressedBack;
        private IPaletteRibbonBack _trackingBack;
        private IPaletteRibbonBack _selectedBack;
        private IPaletteRibbonBack _focusOverrideBack;
        private IPaletteRibbonText _disabledText;
        private IPaletteRibbonText _normalText;
        private IPaletteRibbonText _pressedText;
        private IPaletteRibbonText _trackingText;
        private IPaletteRibbonText _selectedText;
        private IPaletteRibbonText _focusOverrideText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRedirectRibbonDouble class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        public PaletteRedirectRibbonDouble(IPalette target)
            : this(target, 
                   null, null, null, null, null, null,
                   null, null, null, null, null, null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteRedirectRibbonDouble class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="disabledBack">Redirection for back disabled state requests.</param>
        /// <param name="normalBack">Redirection for back normal state requests.</param>
        /// <param name="pressedBack">Redirection for back pressed state requests.</param>
        /// <param name="trackingBack">Redirection for back tracking state requests.</param>
        /// <param name="selectedBack">Redirection for selected states requests.</param>
        /// <param name="focusOverrideBack">Redirection for back focus override state requests.</param>
        /// <param name="disabledText">Redirection for text disabled state requests.</param>
        /// <param name="normalText">Redirection for text normal state requests.</param>
        /// <param name="pressedText">Redirection for text pressed state requests.</param>
        /// <param name="trackingText">Redirection for text tracking state requests.</param>
        /// <param name="selectedText">Redirection for text selected states requests.</param>
        /// <param name="focusOverrideText">Redirection for text focus override state requests.</param>
        public PaletteRedirectRibbonDouble(IPalette target,
                                           IPaletteRibbonBack disabledBack,
                                           IPaletteRibbonBack normalBack,
                                           IPaletteRibbonBack pressedBack,
                                           IPaletteRibbonBack trackingBack,
                                           IPaletteRibbonBack selectedBack,
                                           IPaletteRibbonBack focusOverrideBack,
                                           IPaletteRibbonText disabledText,
                                           IPaletteRibbonText normalText,
                                           IPaletteRibbonText pressedText,
                                           IPaletteRibbonText trackingText,
                                           IPaletteRibbonText selectedText,
                                           IPaletteRibbonText focusOverrideText
                                          )
            : base(target)
		{
            // Remember state specific inheritance
            _disabledBack = disabledBack;
            _normalBack = normalBack;
            _pressedBack = pressedBack;
            _trackingBack = trackingBack;
            _selectedBack = selectedBack;
            _focusOverrideBack = focusOverrideBack;
            _disabledText = disabledText;
            _normalText = normalText;
            _pressedText = pressedText;
            _trackingText = trackingText;
            _selectedText = selectedText;
            _focusOverrideText = focusOverrideText;
        }
		#endregion

        #region SetRedirectStates
        /// <summary>
        /// Set the redirection states.
        /// </summary>
        /// <param name="disabledBack">Redirection for back disabled state requests.</param>
        /// <param name="normalBack">Redirection for back normal state requests.</param>
        /// <param name="pressedBack">Redirection for back pressed state requests.</param>
        /// <param name="trackingBack">Redirection for back tracking state requests.</param>
        /// <param name="selectedBack">Redirection for selected states requests.</param>
        /// <param name="focusOverrideBack">Redirection for back focus override state requests.</param>
        /// <param name="disabledText">Redirection for text disabled state requests.</param>
        /// <param name="normalText">Redirection for text normal state requests.</param>
        /// <param name="pressedText">Redirection for text pressed state requests.</param>
        /// <param name="trackingText">Redirection for text tracking state requests.</param>
        /// <param name="selectedText">Redirection for text selected states requests.</param>
        /// <param name="focusOverrideText">Redirection for text focus override state requests.</param>
        public virtual void SetRedirectStates(IPaletteRibbonBack disabledBack,
                                              IPaletteRibbonBack normalBack,
                                              IPaletteRibbonBack pressedBack,
                                              IPaletteRibbonBack trackingBack,
                                              IPaletteRibbonBack selectedBack,
                                              IPaletteRibbonBack focusOverrideBack,
                                              IPaletteRibbonText disabledText,
                                              IPaletteRibbonText normalText,
                                              IPaletteRibbonText pressedText,
                                              IPaletteRibbonText trackingText,
                                              IPaletteRibbonText selectedText,
                                              IPaletteRibbonText focusOverrideText)
        {
            _disabledBack = disabledBack;
            _normalBack = normalBack;
            _pressedBack = pressedBack;
            _trackingBack = trackingBack;
            _selectedBack = selectedBack;
            _focusOverrideBack = focusOverrideBack;
            _disabledText = disabledText;
            _normalText = normalText;
            _pressedText = pressedText;
            _trackingText = trackingText;
            _selectedText = selectedText;
            _focusOverrideText = focusOverrideText;
        }
        #endregion

        #region ResetRedirectStates
        /// <summary>
        /// Reset the redirection states to null.
        /// </summary>
        public virtual void ResetRedirectStates()
        {
            _disabledBack = null;
            _normalBack = null;
            _pressedBack = null;
            _trackingBack = null;
            _selectedBack = null;
            _focusOverrideBack = null;
            _disabledText = null;
            _normalText = null;
            _pressedText = null;
            _trackingText = null;
            _selectedText = null;
            _focusOverrideText = null;
        }
        #endregion

        #region BackColorStyle
        /// <summary>
        /// Gets the background drawing style for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon back style requested.</param>
        /// <returns>Color value.</returns>
        public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColorStyle(state);
            else
                return Target.GetRibbonBackColorStyle(style, state);
        }
        #endregion

        #region BackColor1
        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor1(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor1(state);
            else
                return Target.GetRibbonBackColor1(style, state);
        }
        #endregion

        #region BackColor2
        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor2(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor2(state);
            else
                return Target.GetRibbonBackColor2(style, state);
        }
        #endregion

        #region BackColor3
        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor3(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor3(state);
            else
                return Target.GetRibbonBackColor3(style, state);
        }
        #endregion

        #region BackColor4
        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor4(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor4(state);
            else
                return Target.GetRibbonBackColor4(style, state);
        }
        #endregion

        #region BackColor5
        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor5(PaletteRibbonBackStyle style, PaletteState state)
        {
            IPaletteRibbonBack inherit = GetBackInherit(state);

            if (inherit != null)
                return inherit.GetRibbonBackColor5(state);
            else
                return Target.GetRibbonBackColor5(style, state);
        }
        #endregion

        #region TextColor
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="style">Style of the ribbon color requested.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTextColor(PaletteRibbonTextStyle style, PaletteState state)
        {
            IPaletteRibbonText inherit = GetTextInherit(state);

            if (inherit != null)
                return inherit.GetRibbonTextColor(state);
            else
                return Target.GetRibbonTextColor(style, state);
        }
        #endregion

        #region Implementation
        private IPaletteRibbonBack GetBackInherit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return _disabledBack;
                case PaletteState.Normal:
                    return _normalBack;
                case PaletteState.Pressed:
                    return _pressedBack;
                case PaletteState.Tracking:
                    return _trackingBack;
                case PaletteState.CheckedNormal:
                case PaletteState.CheckedPressed:
                case PaletteState.CheckedTracking:
                    return _selectedBack;
                case PaletteState.FocusOverride:
                    return _focusOverrideBack;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        private IPaletteRibbonText GetTextInherit(PaletteState state)
        {
            switch (state)
            {
                case PaletteState.Disabled:
                    return _disabledText;
                case PaletteState.Normal:
                    return _normalText;
                case PaletteState.Pressed:
                    return _pressedText;
                case PaletteState.Tracking:
                    return _trackingText;
                case PaletteState.CheckedNormal:
                case PaletteState.CheckedPressed:
                case PaletteState.CheckedTracking:
                    return _selectedText;
                case PaletteState.FocusOverride:
                    return _focusOverrideText;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }
        #endregion
    }
}
