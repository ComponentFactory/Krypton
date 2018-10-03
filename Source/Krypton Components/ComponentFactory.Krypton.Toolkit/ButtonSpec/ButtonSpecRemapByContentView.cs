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
    public class ButtonSpecRemapByContentView : ButtonSpecRemapByContentBase
    {
        #region Instance Fields
        private ViewDrawContent _foreground;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecRemapByContentView class.
        /// </summary>
        /// <param name="target">Initial palette target for redirection.</param>
        /// <param name="buttonSpec">Reference to button specification.</param>
        public ButtonSpecRemapByContentView(IPalette target,
                                            ButtonSpec buttonSpec)
            : base(target, buttonSpec)
        {
        }
		#endregion

        #region Foreground
        /// <summary>
        /// Gets and sets the foreground to use for color map redirection.
        /// </summary>
        public ViewDrawContent Foreground
        {
            get { return _foreground; }
            set { _foreground = value; }
        }
        #endregion

        #region PaletteContent
        /// <summary>
        /// Gets the palette content to use for remapping.
        /// </summary>
        public override IPaletteContent PaletteContent 
        {
            get
            {
                if (_foreground != null)
                    return _foreground.GetPalette();
                else
                    return null;
            }
        }
        #endregion

        #region PaletteState
        /// <summary>
        /// Gets the state of the remapping area
        /// </summary>
        public override PaletteState PaletteState 
        {
            get { return _foreground.State; }
        }
        #endregion
    }
}
