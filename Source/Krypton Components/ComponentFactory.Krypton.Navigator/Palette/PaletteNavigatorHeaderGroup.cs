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
	/// Implement storage for Navigator HeaderGroup states.
	/// </summary>
    public class PaletteNavigatorHeaderGroup : PaletteHeaderGroup
	{
		#region Instance Fields
        private PaletteTripleMetric _paletteHeaderBar;
        private PaletteTripleMetric _paletteHeaderOverflow;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteNavigatorHeaderGroup class.
		/// </summary>
        /// <param name="inheritHeaderGroup">Source for inheriting palette defaulted values.</param>
        /// <param name="inheritHeaderPrimary">Source for inheriting primary header defaulted values.</param>
        /// <param name="inheritHeaderSecondary">Source for inheriting secondary header defaulted values.</param>
        /// <param name="inheritHeaderBar">Source for inheriting bar header defaulted values.</param>
        /// <param name="inheritHeaderOverflow">Source for inheriting overflow header defaulted values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorHeaderGroup(PaletteHeaderGroupRedirect inheritHeaderGroup,
                                           PaletteHeaderPaddingRedirect inheritHeaderPrimary,
                                           PaletteHeaderPaddingRedirect inheritHeaderSecondary,
                                           PaletteHeaderPaddingRedirect inheritHeaderBar,
                                           PaletteHeaderPaddingRedirect inheritHeaderOverflow,
                                           NeedPaintHandler needPaint)
            : base(inheritHeaderGroup, inheritHeaderPrimary,
                   inheritHeaderSecondary, needPaint)
		{
            Debug.Assert(inheritHeaderBar != null);

			// Create the palette storage
            _paletteHeaderBar = new PaletteTripleMetric(inheritHeaderBar, needPaint);
            _paletteHeaderOverflow = new PaletteTripleMetric(inheritHeaderOverflow, needPaint);
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

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        /// <param name="inheritHeaderGroup">Source for inheriting.</param>
        public void SetInherit(PaletteNavigatorHeaderGroup inheritHeaderGroup)
        {
            base.SetInherit(inheritHeaderGroup);
            _paletteHeaderBar.SetInherit(inheritHeaderGroup.HeaderBar);
            _paletteHeaderOverflow.SetInherit(inheritHeaderGroup.HeaderOverflow);
        }
        #endregion

        #region HeaderBar
        /// <summary>
		/// Gets access to the bar header appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining bar header appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleMetric HeaderBar
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
        /// Gets access to the overflow header appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining overflow header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleMetric HeaderOverflow
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
