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

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Extend storage for the split container with background and border information combined with separator information.
	/// </summary>
	public class PaletteSplitContainer : PaletteDouble
	{
		#region Instance Fields
        private PaletteSeparatorPadding _separator;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteSplitContainer class.
		/// </summary>
        /// <param name="inheritSplitContainer">Source for inheriting back/border defaulted values.</param>
        /// <param name="inheritSeparator">Source for inheriting separator defaulted values.</param>
        /// <param name="inheritMetric">Source for inheriting separator metric values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteSplitContainer(IPaletteDouble inheritSplitContainer,
                                     IPaletteDouble inheritSeparator,
                                     IPaletteMetric inheritMetric,
                                     NeedPaintHandler needPaint)
            : base(inheritSplitContainer, needPaint)
		{
			// Create the embedded separator palette information
            _separator = new PaletteSeparatorPadding(inheritSeparator, inheritMetric, needPaint);
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
						Separator.IsDefault);
			}
		}
		#endregion

        #region Border
        /// <summary>
        /// Gets access to the border palette details.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PaletteBorder Border
        {
            get { return base.Border; }
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
			get { return _separator; }
		}

		private bool ShouldSerializeSeparator()
		{
			return !_separator.IsDefault;
		}
		#endregion
	}
}
