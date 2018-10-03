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
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Implement redirected storage for page appearance.
	/// </summary>
    public class PalettePageRedirect : PaletteDoubleRedirect
	{
		#region Identity
		/// <summary>
        /// Initialize a new instance of the PalettePageRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PalettePageRedirect(PaletteRedirect redirect,
                                   NeedPaintHandler needPaint)
            : base(redirect, PaletteBackStyle.ControlClient,
                             PaletteBorderStyle.ControlClient, needPaint)
		{
		}
		#endregion

        #region Border
        /// <summary>
        /// Gets access to the border palette details.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteBorder Border
        {
            get { return base.Border; }
        }

        /// <summary>
        /// Gets the border palette.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IPaletteBorder PaletteBorder
        {
            get { return base.PaletteBorder; }
        }
        #endregion
    }
}
