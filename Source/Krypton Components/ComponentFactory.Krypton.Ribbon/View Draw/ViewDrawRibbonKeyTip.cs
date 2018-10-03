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
    /// <summary>
    /// View for drawing an individual key tip.
    /// </summary>
    internal class ViewDrawRibbonKeyTip : ViewDrawDocker,
                                          IContentValues
    {
        #region Instance Fields
        private KeyTipInfo _keyTipInfo;
        private ViewDrawContent _drawContent;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonKeyTip class.
        /// </summary>
        /// <param name="keyTipInfo">Key tip information to display.</param>
        /// <param name="paletteBack">Background palette for appearance.</param>
        /// <param name="paletteBorder">Border palette for appearance.</param>
        /// <param name="paletteContent">Content palette for appearance.</param>
        public ViewDrawRibbonKeyTip(KeyTipInfo keyTipInfo,
                                    IPaletteBack paletteBack,
                                    IPaletteBorder paletteBorder,
                                    IPaletteContent paletteContent)
            : base(paletteBack, paletteBorder)
        {
            _keyTipInfo = keyTipInfo;

            // Create view for the key tip text
            _drawContent = new ViewDrawContent(paletteContent, this, VisualOrientation.Top);

            // Add content as filler for ourself
            Add(_drawContent, ViewDockStyle.Fill);
        }
        #endregion

        #region KeyTipInfo
        /// <summary>
        /// Gets the associated key tip info.
        /// </summary>
        public KeyTipInfo KeyTipInfo
        {
            get { return _keyTipInfo; }
        }
        #endregion

        #region IContent
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText()
        {
            return _keyTipInfo.KeyString;
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText()
        {
            return string.Empty;
        }
        #endregion
    }
}
