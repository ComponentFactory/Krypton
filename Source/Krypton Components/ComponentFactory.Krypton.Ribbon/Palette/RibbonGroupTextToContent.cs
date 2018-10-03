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
    internal class RibbonGroupTextToContent : RibbonToContent
    {
        #region Instance Fields
        private IPaletteRibbonText _ribbonGroupText;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonGroupTextToContent class.
        /// </summary>
        /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
        /// <param name="ribbonGroupText">Source for ribbon group settings.</param>
        public RibbonGroupTextToContent(PaletteRibbonGeneral ribbonGeneral,
                                        IPaletteRibbonText ribbonGroupText)
            : base(ribbonGeneral)
        {
            Debug.Assert(ribbonGroupText != null);
            _ribbonGroupText = ribbonGroupText;
        }
        #endregion

        #region PaletteRibbonGroup
        /// <summary>
        /// Gets and sets the ribbon group palette to use.
        /// </summary>
        public IPaletteRibbonText PaletteRibbonGroup
        {
            get { return _ribbonGroupText; }
            set { _ribbonGroupText = value; }
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
            return _ribbonGroupText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state)
        {
            return _ribbonGroupText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state)
        {
            return _ribbonGroupText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state)
        {
            return _ribbonGroupText.GetRibbonTextColor(state);
        }
        #endregion
    }
}
