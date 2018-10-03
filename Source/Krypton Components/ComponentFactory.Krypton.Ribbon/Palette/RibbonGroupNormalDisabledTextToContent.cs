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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class RibbonGroupNormalDisabledTextToContent : RibbonToContent
    {
        #region Instance Fields
        private IPaletteRibbonText _ribbonGroupTextNormal;
        private IPaletteRibbonText _ribbonGroupTextDisabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonGroupNormalDisabledTextToContent class.
        /// </summary>
        /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
        /// <param name="ribbonGroupTextNormal">Source for ribbon group button normal settings.</param>
        /// <param name="ribbonGroupTextDisabled">Source for ribbon group button disabled settings.</param>
        public RibbonGroupNormalDisabledTextToContent(PaletteRibbonGeneral ribbonGeneral,
                                                      IPaletteRibbonText ribbonGroupTextNormal,
                                                      IPaletteRibbonText ribbonGroupTextDisabled)
            : base(ribbonGeneral)
        {
            Debug.Assert(ribbonGroupTextNormal != null);
            Debug.Assert(ribbonGroupTextDisabled != null);

            _ribbonGroupTextNormal = ribbonGroupTextNormal;
            _ribbonGroupTextDisabled = ribbonGroupTextDisabled;
        }
        #endregion

        #region IPaletteContent
        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state)
        {
            if (state == PaletteState.Disabled)
                return _ribbonGroupTextDisabled.GetRibbonTextColor(state);
            else
                return _ribbonGroupTextNormal.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state)
        {
            if (state == PaletteState.Disabled)
                return _ribbonGroupTextDisabled.GetRibbonTextColor(state);
            else
                return _ribbonGroupTextNormal.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state)
        {
            if (state == PaletteState.Disabled)
                return _ribbonGroupTextDisabled.GetRibbonTextColor(state);
            else
                return _ribbonGroupTextNormal.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state)
        {
            if (state == PaletteState.Disabled)
                return _ribbonGroupTextDisabled.GetRibbonTextColor(state);
            else
                return _ribbonGroupTextNormal.GetRibbonTextColor(state);
        }
        #endregion
    }
}
