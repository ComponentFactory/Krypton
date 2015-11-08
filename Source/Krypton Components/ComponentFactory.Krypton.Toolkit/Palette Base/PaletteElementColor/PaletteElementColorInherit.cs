// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 4.5.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Provide inheritance of palette element colors.
	/// </summary>
    public abstract class PaletteElementColorInherit : GlobalId,
                                                       IPaletteElementColor
    {
        #region IPaletteElementColor
        /// <summary>
        /// Gets the first color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetElementColor1(PaletteState state);

        /// <summary>
        /// Gets the second color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetElementColor2(PaletteState state);

        /// <summary>
        /// Gets the third color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetElementColor3(PaletteState state);

        /// <summary>
        /// Gets the fourth color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetElementColor4(PaletteState state);

        /// <summary>
        /// Gets the fifth color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public abstract Color GetElementColor5(PaletteState state);
        #endregion
    }
}
