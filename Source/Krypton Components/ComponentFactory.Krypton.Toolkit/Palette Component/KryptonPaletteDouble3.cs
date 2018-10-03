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
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Base storage class for palette double (background/border) that expose three states.
	/// </summary>
    public abstract class KryptonPaletteDouble3 : Storage
    {
        #region Instance Fields
        internal PaletteDoubleRedirect _stateCommon;
        internal PaletteDouble _stateDisabled;
        internal PaletteDouble _stateNormal;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of KryptonPaletteDouble3 KryptonPaletteControl class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="backStyle">Background style.</param>
        /// <param name="borderStyle">Border style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteDouble3(PaletteRedirect redirect,
                                     PaletteBackStyle backStyle,
                                     PaletteBorderStyle borderStyle,
                                     NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateCommon = new PaletteDoubleRedirect(redirect, backStyle, borderStyle, needPaint);
            _stateDisabled = new PaletteDouble(_stateCommon, needPaint);
            _stateNormal = new PaletteDouble(_stateCommon, needPaint);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _stateCommon.SetRedirector(redirect);
        }
        #endregion

		#region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
			get
			{
                return _stateCommon.IsDefault &&
                       _stateDisabled.IsDefault &&
                       _stateNormal.IsDefault;
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            // Populate only the designated styles
            _stateDisabled.PopulateFromBase(PaletteState.Disabled);
            _stateNormal.PopulateFromBase(PaletteState.Normal);
        }
        #endregion
    }
}
