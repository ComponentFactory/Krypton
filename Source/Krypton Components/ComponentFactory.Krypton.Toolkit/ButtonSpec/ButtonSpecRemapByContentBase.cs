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
    /// Redirect requests for image/text colors to remap.
    /// </summary>
    public abstract class ButtonSpecRemapByContentBase : PaletteRedirect
    {
        #region Instance Fields
        private ButtonSpec _buttonSpec;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecRemapByContentBase class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="buttonSpec">Reference to button specification.</param>
        public ButtonSpecRemapByContentBase(IPalette target,
                                            ButtonSpec buttonSpec)
            : base(target)
        {
            Debug.Assert(buttonSpec != null);
            _buttonSpec = buttonSpec;
        }
		#endregion

        #region PaletteContent
        /// <summary>
        /// Gets the palette content to use for remapping.
        /// </summary>
        public abstract IPaletteContent PaletteContent { get; }
        #endregion

        #region PaletteState
        /// <summary>
        /// Gets the state of the remapping area
        /// </summary>
        public abstract PaletteState PaletteState { get; }
        #endregion

        #region GetContentImageColorMap
        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorMap(PaletteContentStyle style, PaletteState state)
        {
            // If allowed to override then get the map color
            Color mapColor = OverrideImageColor(state);

            // If a map color provided then return is
            if ((mapColor != Color.Empty) && (PaletteContent != null))
                return mapColor;
            else
                return base.GetContentImageColorMap(style, state);
        }
        #endregion

        #region GetContentImageColorTo
        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentImageColorTo(PaletteContentStyle style, PaletteState state)
        {
            // If allowed to override then get the map color
            Color mapColor = OverrideImageColor(state);

            // If mapping occuring then return the target remap color
            if ((mapColor != Color.Empty)  && (PaletteContent != null))
            {
                PaletteState getState = PaletteState;

                // Honor the button disabled state
                if (state == PaletteState.Disabled)
                    getState = PaletteState.Disabled;

                return PaletteContent.GetContentShortTextColor1(getState);
            }
            else
                return base.GetContentImageColorTo(style, state);
        }
        #endregion

        #region GetContentShortTextColor1
        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // Do we need to override the text color
            if (OverrideTextColor(state) && (PaletteContent != null))
                return PaletteContent.GetContentShortTextColor1(state);
            else
                return base.GetContentShortTextColor1(style, state);
        }
        #endregion

        #region GetContentLongTextColor1
        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="style">Content style.</param>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
        {
            // Do we need to override the text color
            if (OverrideTextColor(state) && (PaletteContent != null))
                return PaletteContent.GetContentShortTextColor1(state);
            else
                return base.GetContentLongTextColor1(style, state);
        }
        #endregion

        #region Implementation
        private Color OverrideImageColor(PaletteState state)
        {
            // We only intercept if we have a content to use for redirection
            if (PaletteContent != null)
            {
                // We only override the normal/disabled states
                if ((state == PaletteState.Normal) ||
                    (state == PaletteState.Disabled))
                {
                    // Get the color map from the button spec
                    Color mapColor = _buttonSpec.GetColorMap(base.Target);

                    // If we are supposed to remap a color
                    if (mapColor != Color.Empty)
                    {
                        // Get the button style requested
                        ButtonStyle buttonStyle = _buttonSpec.GetStyle(base.Target);

                        // Only for ButtonSpec do we use the palette value
                        if (buttonStyle == ButtonStyle.ButtonSpec)
                            return mapColor;
                    }
                }
            }

            return Color.Empty;
        }

        private bool OverrideTextColor(PaletteState state)
        {
            // We are only interested in overriding the disabled or normal colors
            if (state == PaletteState.Normal)
            {
                // Get the button style requested
                ButtonStyle buttonStyle = _buttonSpec.GetStyle(base.Target);

                // If we are checking for button styles of ButtonSpec only, then do so
                if (buttonStyle == ButtonStyle.ButtonSpec)
                    return true;
            }

            return false;
        }
        #endregion
    }
}
