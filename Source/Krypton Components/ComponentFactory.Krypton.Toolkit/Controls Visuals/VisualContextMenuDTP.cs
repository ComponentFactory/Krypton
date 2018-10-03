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
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Specialized version of the visual context menu that knows about the KryptonDateTimePicker drop down button.
    /// </summary>
    public class VisualContextMenuDTP : VisualContextMenu
    {
        #region Instance Fields
        private Rectangle _dropScreenRect;
        #endregion

        #region Identity
        /// <summary>
        ///  Initialize a new instance of the VisualContextMenuDTP class.
        /// </summary>
        /// <param name="contextMenu">Originating context menu instance.</param>
        /// <param name="palette">Local palette setting to use initially.</param>
        /// <param name="paletteMode">Palette mode setting to use initially.</param>
        /// <param name="redirector">Redirector used for obtaining palette values.</param>
        /// <param name="redirectorImages">Redirector used for obtaining images.</param>
        /// <param name="items">Collection of context menu items to be displayed.</param>
        /// <param name="enabled">Enabled state of the context menu.</param>
        /// <param name="keyboardActivated">Was the context menu activate by a keyboard action.</param>
        /// <param name="dropScreenRect">Screen rectangle of the drop down button.</param>
        public VisualContextMenuDTP(KryptonContextMenu contextMenu,
                                    IPalette palette,
                                    PaletteMode paletteMode,
                                    PaletteRedirect redirector,
                                    PaletteRedirectContextMenu redirectorImages,
                                    KryptonContextMenuCollection items,
                                    bool enabled,
                                    bool keyboardActivated,
                                    Rectangle dropScreenRect)
            : base(contextMenu, palette, paletteMode, redirector, redirectorImages, 
                   items, enabled, keyboardActivated)
        {
            _dropScreenRect = dropScreenRect;
        }
        #endregion

        #region Public
        /// <summary>
        /// Should the mouse down be eaten when the tracking has been ended.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Screen coordinates point.</param>
        /// <returns>True to eat message; otherwise false.</returns>
        public override bool DoesMouseDownGetEaten(Message m, Point pt)
        {
            // If the user dismissed the context menu by clicking down on the drop down button of
            // the KryptonDateTimePicker then eat the down message to prevent the down press from
            // opening the menu again.
            return _dropScreenRect.Contains(new Point(pt.X, pt.Y));
        }
        #endregion
    }
}
