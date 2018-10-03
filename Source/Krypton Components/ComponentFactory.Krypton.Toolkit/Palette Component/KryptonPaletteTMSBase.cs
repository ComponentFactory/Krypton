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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Base class for the palette TMS storage classes to derive from.
	/// </summary>
	public abstract class KryptonPaletteTMSBase : Storage
    {
        #region Instance Fields
        private KryptonInternalKCT _internalKCT;
        #endregion
        
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteKCTBase class.
		/// </summary>
        /// <param name="internalKCT">Reference to inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTMSBase(KryptonInternalKCT internalKCT,
                                       NeedPaintHandler needPaint)
		{
            Debug.Assert(internalKCT != null);

            _internalKCT = internalKCT;

            // Store the provided paint notification delegate
            NeedPaint = needPaint;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the internal class used to inherit values.
        /// </summary>
        internal KryptonInternalKCT InternalKCT
        {
            get { return _internalKCT; }
        }
        #endregion
    }
}
