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
    public class ButtonSpecRemapByContentCache : ButtonSpecRemapByContentBase
    {
        #region Instance Fields
        private IPaletteContent _paletteContent;
        private PaletteState _paletteState;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecRemapByContentCache class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="buttonSpec">Reference to button specification.</param>
        public ButtonSpecRemapByContentCache(IPalette target,
                                             ButtonSpec buttonSpec)
            : base(target, buttonSpec)
        {
        }
		#endregion

        #region SetPaletteContent
        /// <summary>
        /// Set the palette content to use for remapping.
        /// </summary>
        /// <param name="paletteContent">Palette for requesting foreground colors.</param>
        public void SetPaletteContent(IPaletteContent paletteContent)
        {
            _paletteContent = paletteContent;
        }
        #endregion

        #region SetPaletteState
        /// <summary>
        /// Set the palette state of the remapping element.
        /// </summary>
        /// <param name="paletteState">Palette state.</param>
        public void SetPaletteState(PaletteState paletteState)
        {
            _paletteState = paletteState;
        }
        #endregion

        #region PaletteContent
        /// <summary>
        /// Gets the palette content to use for remapping.
        /// </summary>
        public override IPaletteContent PaletteContent
        {
            get { return _paletteContent; }
        }
        #endregion

        #region PaletteState
        /// <summary>
        /// Gets the state of the remapping area
        /// </summary>
        public override PaletteState PaletteState
        {
            get { return _paletteState; }
        }
        #endregion
    }
}
