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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
    /// Redirect storage for Navigator HeaderGroup states.
	/// </summary>
    public class PaletteNavigatorHeaderGroupRedirect : PaletteHeaderGroupRedirect
	{
		#region Instance Fields
		private PaletteHeaderPaddingRedirect _paletteHeaderBar;
        private PaletteHeaderPaddingRedirect _paletteHeaderOverflow;
        #endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the PaletteNavigatorHeaderGroupRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorHeaderGroupRedirect(PaletteRedirect redirect,
                                                   NeedPaintHandler needPaint)
            : this(redirect, redirect, redirect, redirect, redirect, needPaint)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteNavigatorHeaderGroupRedirect class.
		/// </summary>
        /// <param name="redirectHeaderGroup">Inheritence redirection for header group.</param>
        /// <param name="redirectHeaderPrimary">Inheritence redirection for primary header.</param>
        /// <param name="redirectHeaderSecondary">Inheritence redirection for secondary header.</param>
        /// <param name="redirectHeaderBar">Inheritence redirection for bar header.</param>
        /// <param name="redirectHeaderOverflow">Inheritence redirection for overflow header.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorHeaderGroupRedirect(PaletteRedirect redirectHeaderGroup,
                                                   PaletteRedirect redirectHeaderPrimary,
                                                   PaletteRedirect redirectHeaderSecondary,
                                                   PaletteRedirect redirectHeaderBar,
                                                   PaletteRedirect redirectHeaderOverflow,
                                                   NeedPaintHandler needPaint)
            : base(redirectHeaderGroup, redirectHeaderPrimary,
                   redirectHeaderSecondary, needPaint)
		{
            Debug.Assert(redirectHeaderBar != null);
            Debug.Assert(redirectHeaderOverflow != null);

            // Create the palette storage
            _paletteHeaderBar = new PaletteHeaderPaddingRedirect(redirectHeaderBar, PaletteBackStyle.HeaderSecondary, PaletteBorderStyle.HeaderSecondary, PaletteContentStyle.HeaderSecondary, needPaint);
            _paletteHeaderOverflow = new PaletteHeaderPaddingRedirect(redirectHeaderOverflow, PaletteBackStyle.ButtonNavigatorStack, PaletteBorderStyle.HeaderSecondary, PaletteContentStyle.HeaderSecondary, needPaint);
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
                return (base.IsDefault &&
                        HeaderBar.IsDefault &&
                        HeaderOverflow.IsDefault);
			}
		}
		#endregion

        #region HeaderBar
        /// <summary>
		/// Gets access to the bar header appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining bar header appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteHeaderPaddingRedirect HeaderBar
		{
            get { return _paletteHeaderBar; }
		}

        private bool ShouldSerializeHeaderBar()
		{
			return !_paletteHeaderBar.IsDefault;
		}
		#endregion

        #region HeaderOverflow
        /// <summary>
        /// Gets access to the overlow header appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining overflow header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteHeaderPaddingRedirect HeaderOverflow
        {
            get { return _paletteHeaderOverflow; }
        }

        private bool ShouldSerializeHeaderOverflow()
        {
            return !_paletteHeaderOverflow.IsDefault;
        }
        #endregion
    }
}
