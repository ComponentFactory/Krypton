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
    internal class DesignTextToContent : RibbonToContent
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DesignTextToContent class.
        /// </summary>
        /// <param name="ribbon">Reference to the owning ribbon control.</param>
        public DesignTextToContent(KryptonRibbon ribbon)
            : base(ribbon.StateCommon.RibbonGeneral)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;
        }
        #endregion

        #region IPaletteContent
        /// <summary>
        /// Gets the text trimming to use for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentShortTextTrim(PaletteState state)
        {
            return PaletteTextTrim.Character;
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public override PaletteRelativeAlign GetContentShortTextH(PaletteState state)
        {
            return PaletteRelativeAlign.Center;
        }

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor1(PaletteState state)
        {
            if (state == PaletteState.Normal)
                return _ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorLight(state);
            else
                return _ribbon.StateCommon.RibbonGroupButton.Content.GetContentShortTextColor1(state);
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentShortTextColor2(PaletteState state)
        {
            if (state == PaletteState.Normal)
                return _ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorLight(state);
            else
                return _ribbon.StateCommon.RibbonGroupButton.Content.GetContentShortTextColor1(state);
        }

        /// <summary>
        /// Gets the text trimming to use for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public override PaletteTextTrim GetContentLongTextTrim(PaletteState state)
        {
            return PaletteTextTrim.Character;
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor1(PaletteState state)
        {
            if (state == PaletteState.Normal)
                return _ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorLight(state);
            else
                return _ribbon.StateCommon.RibbonGroupButton.Content.GetContentShortTextColor1(state);
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetContentLongTextColor2(PaletteState state)
        {
            if (state == PaletteState.Normal)
                return _ribbon.StateCommon.RibbonGeneral.GetRibbonGroupSeparatorLight(state);
            else
                return _ribbon.StateCommon.RibbonGroupButton.Content.GetContentShortTextColor1(state);
        }
        #endregion
    }
}
