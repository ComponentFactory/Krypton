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

namespace ComponentFactory.Krypton.Ribbon
{
	/// <summary>
	/// Implement storage for a ribbon disabled state.
	/// </summary>
	public class PaletteRibbonDisabled : Storage
	{
		#region Instance Fields
        private PaletteRibbonText _ribbonGroupCheckBoxText;
        private PaletteRibbonText _ribbonGroupButtonText;
        private PaletteRibbonText _ribbonGroupLabelText;
        private PaletteRibbonText _ribbonGroupRadioButtonText;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteRibbonDisabled class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonDisabled(PaletteRibbonRedirect inherit,
                                     NeedPaintHandler needPaint)
		{
            // Create storage that maps onto the inherit instances
            _ribbonGroupCheckBoxText = new PaletteRibbonText(inherit.RibbonGroupCheckBoxText, needPaint);
            _ribbonGroupButtonText = new PaletteRibbonText(inherit.RibbonGroupButtonText, needPaint);
            _ribbonGroupLabelText = new PaletteRibbonText(inherit.RibbonGroupLabelText, needPaint);
            _ribbonGroupRadioButtonText = new PaletteRibbonText(inherit.RibbonGroupRadioButtonText, needPaint);
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
                return RibbonGroupCheckBoxText.IsDefault &&
                       RibbonGroupButtonText.IsDefault &&
                       RibbonGroupLabelText.IsDefault &&
                       RibbonGroupRadioButtonText.IsDefault;
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public virtual void PopulateFromBase(PaletteState state)
        {
            _ribbonGroupCheckBoxText.PopulateFromBase(state);
            _ribbonGroupButtonText.PopulateFromBase(state);
            _ribbonGroupLabelText.PopulateFromBase(state);
            _ribbonGroupRadioButtonText.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public virtual void SetInherit(PaletteRibbonRedirect inherit)
        {
            _ribbonGroupCheckBoxText.SetInherit(inherit.RibbonGroupCheckBoxText);
            _ribbonGroupButtonText.SetInherit(inherit.RibbonGroupButtonText);
            _ribbonGroupLabelText.SetInherit(inherit.RibbonGroupLabelText);
            _ribbonGroupRadioButtonText.SetInherit(inherit.RibbonGroupCheckBoxText);
        }
        #endregion

        #region RibbonGroupCheckBoxText
        /// <summary>
        /// Gets access to the ribbon group check box label palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group check box label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonText RibbonGroupCheckBoxText
        {
            get { return _ribbonGroupCheckBoxText; }
        }

        private bool ShouldSerializeRibbonGroupCheckBoxText()
        {
            return !_ribbonGroupCheckBoxText.IsDefault;
        }
        #endregion

        #region RibbonGroupButtonText
        /// <summary>
        /// Gets access to the ribbon group button text palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group button text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonText RibbonGroupButtonText
        {
            get { return _ribbonGroupButtonText; }
        }

        private bool ShouldSerializeRibbonGroupButtonText()
        {
            return !_ribbonGroupButtonText.IsDefault;
        }
        #endregion

        #region RibbonGroupLabelText
        /// <summary>
        /// Gets access to the ribbon group label label palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group label label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonText RibbonGroupLabelText
        {
            get { return _ribbonGroupLabelText; }
        }

        private bool ShouldSerializeRibbonGroupLabelText()
        {
            return !_ribbonGroupLabelText.IsDefault;
        }
        #endregion

        #region RibbonGroupRadioButtonText
        /// <summary>
        /// Gets access to the ribbon group radio button label palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group radio button label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonText RibbonGroupRadioButtonText
        {
            get { return _ribbonGroupRadioButtonText; }
        }

        private bool ShouldSerializeRibbonGroupRadioButtonText()
        {
            return !_ribbonGroupRadioButtonText.IsDefault;
        }
        #endregion

		#region Implementation
		/// <summary>
		/// Handle a change event from palette source.
		/// </summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="needLayout">True if a layout is also needed.</param>
        protected void OnNeedPaint(object sender, bool needLayout)
		{
			// Pass request from child to our own handler
			PerformNeedPaint(needLayout);
		}
		#endregion
	}
}
