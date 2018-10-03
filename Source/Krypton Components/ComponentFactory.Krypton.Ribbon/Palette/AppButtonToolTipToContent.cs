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
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Map application button tooltip values to content values.
    /// </summary>
    internal class AppButtonToolTipToContent : IContentValues
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AppButtonToolTipToContent class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        public AppButtonToolTipToContent(KryptonRibbon ribbon)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;
        }
        #endregion

        #region HasContent
        /// <summary>
        /// Gets a value indicating if the mapping produces any content.
        /// </summary>
        public bool HasContent
        {
            get
            {
                return (GetImage(PaletteState.Normal) != null) ||
                        !string.IsNullOrEmpty(GetShortText()) ||
                        !string.IsNullOrEmpty(GetLongText());
            }
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
        {
            return _ribbon.RibbonAppButton.AppButtonToolTipImage;
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            return _ribbon.RibbonAppButton.AppButtonToolTipImageTransparentColor;
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText()
        {
            return _ribbon.RibbonAppButton.AppButtonToolTipTitle;
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText()
        {
            return _ribbon.RibbonAppButton.AppButtonToolTipBody;
        }
        #endregion
    }
}
