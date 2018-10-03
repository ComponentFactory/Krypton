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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Implement storage for a tree node triple.
	/// </summary>
    public class PaletteTreeNodeTripleRedirect : Storage                                            
	{
		#region Instance Fields
        private PaletteTripleRedirect _nodeRedirect;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteTreeNodeTripleRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
        /// <param name="contentStyle">Initial content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteTreeNodeTripleRedirect(PaletteRedirect redirect,
									         PaletteBackStyle backStyle,
									         PaletteBorderStyle borderStyle,
                                             PaletteContentStyle contentStyle,
                                             NeedPaintHandler needPaint)
		{
            Debug.Assert(redirect != null);
            _nodeRedirect = new PaletteTripleRedirect(redirect, backStyle, borderStyle, contentStyle, needPaint);
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
                return _nodeRedirect.IsDefault;
			}
		}
		#endregion

        #region Node
        /// <summary>
        /// Gets the node appearance overrides.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining node appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Node
        {
            get { return _nodeRedirect; }
        }

        private bool ShouldSerializeItem()
        {
            return !_nodeRedirect.IsDefault;
        }
        #endregion
    }
}
