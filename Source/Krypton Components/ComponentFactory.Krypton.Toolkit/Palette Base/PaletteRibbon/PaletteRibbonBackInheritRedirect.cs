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
	/// Provide inheritance of palette ribbon background properties from source redirector.
	/// </summary>
    public class PaletteRibbonBackInheritRedirect : PaletteRibbonBackInherit
	{
		#region Instance Fields
		private PaletteRedirect _redirect;
        private PaletteRibbonBackStyle _styleBack;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonBackInheritRedirect class.
		/// </summary>
        /// <param name="redirect">Source for inherit requests.</param>
        /// <param name="styleBack">Ribbon item background style.</param>
        public PaletteRibbonBackInheritRedirect(PaletteRedirect redirect,
                                                PaletteRibbonBackStyle styleBack)
		{
			Debug.Assert(redirect != null);

			_redirect = redirect;
            _styleBack = styleBack;
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
        /// Gets and sets the ribbon background style to use when inheriting.
        /// </summary>
        public PaletteRibbonBackStyle StyleBack
        {
            get { return _styleBack; }
            set { _styleBack = value; }
        }
        #endregion

        #region IPaletteRibbonBack
        /// <summary>
        /// Gets the method used to draw the background of a ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteRibbonBackStyle value.</returns>
        public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteState state)
        {
            return _redirect.GetRibbonBackColorStyle(_styleBack, state);
        }

        /// <summary>
        /// Gets the first background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor1(PaletteState state)
        {
            return _redirect.GetRibbonBackColor1(_styleBack, state);
        }

        /// <summary>
        /// Gets the second background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor2(PaletteState state)
        {
            return _redirect.GetRibbonBackColor2(_styleBack, state);
        }

        /// <summary>
        /// Gets the third background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor3(PaletteState state)
        {
            return _redirect.GetRibbonBackColor3(_styleBack, state);
        }

        /// <summary>
        /// Gets the fourth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor4(PaletteState state)
        {
            return _redirect.GetRibbonBackColor4(_styleBack, state);
        }

        /// <summary>
        /// Gets the fifth background color for the ribbon item.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonBackColor5(PaletteState state)
        {
            return _redirect.GetRibbonBackColor5(_styleBack, state);
        }
        #endregion
    }
}
