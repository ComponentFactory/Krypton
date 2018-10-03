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
    internal class RibbonRecentDocsShortcutToContent : RibbonRecentDocsEntryToContent
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonRecentDocsShortcutToContent class.
        /// </summary>
        /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
        /// <param name="ribbonRecentDocEntryText">Source for ribbon recent document entry settings.</param>
        public RibbonRecentDocsShortcutToContent(PaletteRibbonGeneral ribbonGeneral,
                                                 IPaletteRibbonText ribbonRecentDocEntryText)
            : base(ribbonGeneral, ribbonRecentDocEntryText)
        {
        }
        #endregion

        #region IPaletteContent
        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public override PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
        {
            return PaletteTextHotkeyPrefix.Show;
        }
        #endregion
    }
}
