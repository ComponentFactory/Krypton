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
	/// Provide inheritance of palette ribbon text properties from source redirector.
	/// </summary>
    public class PaletteRibbonTextInheritRedirect : PaletteRibbonTextInherit
	{
		#region Instance Fields
		private PaletteRedirect _redirect;
        private PaletteRibbonTextStyle _styleText;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteRibbonTextInheritRedirect class.
		/// </summary>
        /// <param name="redirect">Source for inherit requests.</param>
        /// <param name="styleText">Ribbon item text style.</param>
        public PaletteRibbonTextInheritRedirect(PaletteRedirect redirect,
                                                PaletteRibbonTextStyle styleText)
		{
			Debug.Assert(redirect != null);

			_redirect = redirect;
            _styleText = styleText;
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

        #region StyleText
        /// <summary>
        /// Gets and sets the ribbon text style to use when inheriting.
        /// </summary>
        public PaletteRibbonTextStyle StyleText
        {
            get { return _styleText; }
            set { _styleText = value; }
        }
        #endregion

        #region IPaletteRibbonBack
        /// <summary>
        /// Gets the tab color for the item text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetRibbonTextColor(PaletteState state)      
        {
            return _redirect.GetRibbonTextColor(_styleText, state);
        }
        #endregion
    }
}
