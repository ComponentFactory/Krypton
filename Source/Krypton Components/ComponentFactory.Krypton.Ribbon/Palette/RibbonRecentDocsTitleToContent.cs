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
    internal class RibbonRecentDocsTitleToContent : RibbonToContent
    {
        #region Static Fields
        private static readonly Padding _titlePadding = new Padding(5, 3, 5, 1);
        #endregion

        #region Instance Fields
        private IPaletteRibbonText _ribbonRecentTitleText;
        private Font _shortTextFont;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonRecentDocsToContent class.
        /// </summary>
        /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
        /// <param name="ribbonRecentTitleText">Source for ribbon recent document title settings.</param>
        public RibbonRecentDocsTitleToContent(PaletteRibbonGeneral ribbonGeneral,
                                         IPaletteRibbonText ribbonRecentTitleText)
            : base(ribbonGeneral)
        {
            Debug.Assert(ribbonRecentTitleText != null);
            _ribbonRecentTitleText = ribbonRecentTitleText;
        }

        /// <summary>
        /// Remove any cached resources.
        /// </summary>
        public void Dispose()
        {
            if (_shortTextFont != null)
                _shortTextFont.Dispose();
        }
        #endregion

        #region IPaletteContent
        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextH(PaletteState state)
        {
            return PaletteRelativeAlign.Near;
        }

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state)
        {
            return _ribbonRecentTitleText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state)
        {
            return _ribbonRecentTitleText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state)
        {
            return _ribbonRecentTitleText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state)
        {
            return _ribbonRecentTitleText.GetRibbonTextColor(state);
        }

        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetContentPadding(PaletteState state)
        {
            return _titlePadding;
        }

        /// <summary>
        /// Gets the font for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public override Font GetContentShortTextFont(PaletteState state)
        {
            if (_shortTextFont != null)
                _shortTextFont.Dispose();

            _shortTextFont = new Font(RibbonGeneral.GetRibbonTextFont(state), FontStyle.Bold);
            return _shortTextFont;
        }
        #endregion
    }
}
