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
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Provide inheritance of palette element colors from source redirector.
	/// </summary>
    public class PaletteElementColorInheritRedirect : PaletteElementColorInherit
	{
		#region Instance Fields
		private PaletteRedirect _redirect;
        private PaletteElement _element;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteElementColorInheritRedirect class.
		/// </summary>
        /// <param name="redirect">Source for inherit requests.</param>
        /// <param name="element">Element value..</param>
        public PaletteElementColorInheritRedirect(PaletteRedirect redirect,
                                                  PaletteElement element)
		{
			Debug.Assert(redirect != null);

			_redirect = redirect;
            _element = element;
		}
		#endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _redirect = redirect;
        }
        #endregion

        #region StyleBack
        /// <summary>
        /// Gets and sets the element to use when inheriting.
        /// </summary>
        public PaletteElement Element
        {
            get { return _element; }
            set { _element = value; }
        }
        #endregion

        #region IPaletteElementColor
        /// <summary>
        /// Gets the first color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor1(PaletteState state)
        {
            return _redirect.GetElementColor1(_element, state);
        }

        /// <summary>
        /// Gets the second color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor2(PaletteState state)
        {
            return _redirect.GetElementColor2(_element, state);
        }

        /// <summary>
        /// Gets the third color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor3(PaletteState state)
        {
            return _redirect.GetElementColor3(_element, state);
        }

        /// <summary>
        /// Gets the fourth color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor4(PaletteState state)
        {
            return _redirect.GetElementColor4(_element, state);
        }

        /// <summary>
        /// Gets the fifth color for the element.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetElementColor5(PaletteState state)
        {
            return _redirect.GetElementColor5(_element, state);
        }
        #endregion
    }
}
