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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Implement storage for other navigator appearance states.
	/// </summary>
    public class PaletteNavigatorOtherEx : PaletteNavigatorOther
    {
        #region Instance Fields
        private PaletteSeparatorPadding _paletteSeparator;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorOtherEx class.
		/// </summary>
        /// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorOtherEx(PaletteNavigatorRedirect redirect,
                                       NeedPaintHandler needPaint)
            : base(redirect, needPaint)
		{
            // Create the palette storage
            _paletteSeparator = new PaletteSeparatorPadding(redirect.Separator, redirect.Separator, needPaint);
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
                return base.IsDefault && 
                       _paletteSeparator.IsDefault;
            }
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        /// <param name="inheritNavigator">Source for inheriting.</param>
        public override void SetInherit(PaletteNavigator inheritNavigator)
        {
            _paletteSeparator.SetInherit(inheritNavigator.Separator);
            base.SetInherit(inheritNavigator);
        }
        #endregion

        #region Separator
        /// <summary>
        /// Get access to the overrides for defining separator appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSeparatorPadding Separator
        {
            get { return _paletteSeparator; }
        }

        private bool ShouldSerializeSeparator()
        {
            return !_paletteSeparator.IsDefault;
        }
        #endregion
    }
}
