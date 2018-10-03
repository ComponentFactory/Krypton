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
    /// Implement storage for list box specific values.
	/// </summary>
    public class PaletteListStateRedirect : PaletteDoubleRedirect
                                            
	{
		#region Instance Fields
        private PaletteRedirect _redirect;
        private PaletteTripleRedirect _itemRedirect;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteListStateRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
		/// <param name="backStyle">Initial background style.</param>
		/// <param name="borderStyle">Initial border style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteListStateRedirect(PaletteRedirect redirect,
									    PaletteBackStyle backStyle,
									    PaletteBorderStyle borderStyle,
                                        NeedPaintHandler needPaint)
			: base(redirect, backStyle, borderStyle, needPaint)
		{
            Debug.Assert(redirect != null);

            // Remember the redirect reference
            _redirect = redirect;

            // Create the item redirector
            _itemRedirect = new PaletteTripleRedirect(redirect,
                                                      PaletteBackStyle.ButtonListItem,
                                                      PaletteBorderStyle.ButtonListItem,
                                                      PaletteContentStyle.ButtonListItem,
                                                      needPaint);
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
                return (base.IsDefault && _itemRedirect.IsDefault);
			}
		}
		#endregion

        #region Item
        /// <summary>
        /// Gets the item appearance overrides.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Item
        {
            get { return _itemRedirect; }
        }

        private bool ShouldSerializeItem()
        {
            return !_itemRedirect.IsDefault;
        }
        #endregion
    }
}
