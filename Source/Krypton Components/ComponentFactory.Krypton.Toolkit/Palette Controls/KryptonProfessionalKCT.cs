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

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonProfessionalKCT : KryptonColorTable
    {
        #region Instance Fields
        private Color[] _colors;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonProfessionalKCT class.
        /// </summary>
        /// <param name="colors">Set of colors to customize with.</param>
        /// <param name="useSystemColors">Should be forced to use system colors.</param>
        /// <param name="palette">Reference to associated palette.</param>
        public KryptonProfessionalKCT(Color[] colors, 
                                      bool useSystemColors,
                                      IPalette palette)
            : base(palette)
        {
            Debug.Assert(colors != null);
            _colors = colors;
            UseSystemColors = useSystemColors;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the starting color of the gradient used in the Header1.
        /// </summary>
        public Color Header1Begin
        {
            get { return _colors[0]; }
        }

        /// <summary>
        /// Gets the end color of the gradient used in the Header1.
        /// </summary>
        public Color Header1End
        {
            get { return _colors[1]; }
        }
        #endregion
    }
}
