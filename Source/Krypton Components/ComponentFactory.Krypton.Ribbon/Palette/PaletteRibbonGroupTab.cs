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
	/// Implement storage for a ribbon state.
	/// </summary>
    public class PaletteRibbonGroupTab : PaletteRibbonJustTab
	{
		#region Instance Fields
        private PaletteRibbonBack _ribbonGroupNormalBorder;
        private PaletteRibbonDouble _ribbonGroupNormalTitle;
        private PaletteRibbonBack _ribbonGroupCollapsedBorder;
        private PaletteRibbonBack _ribbonGroupCollapsedBack;
        private PaletteRibbonBack _ribbonGroupCollapsedFrameBorder;
        private PaletteRibbonBack _ribbonGroupCollapsedFrameBack;
        private PaletteRibbonText _ribbonGroupCollapsedText;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PaletteRibbonGroupTab class.
		/// </summary>
        /// <param name="inherit">Source for inheriting values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteRibbonGroupTab(PaletteRibbonRedirect inherit,
                                     NeedPaintHandler needPaint)
            : base(inherit, needPaint)
		{
			// Create storage that maps onto the inherit instances
            _ribbonGroupNormalBorder = new PaletteRibbonBack(inherit.RibbonGroupNormalBorder, needPaint);
            _ribbonGroupNormalTitle = new PaletteRibbonDouble(inherit.RibbonGroupNormalTitle, inherit.RibbonGroupNormalTitle, needPaint);
            _ribbonGroupCollapsedBorder = new PaletteRibbonBack(inherit.RibbonGroupCollapsedBorder, needPaint);
            _ribbonGroupCollapsedBack = new PaletteRibbonBack(inherit.RibbonGroupCollapsedBack, needPaint);
            _ribbonGroupCollapsedFrameBorder = new PaletteRibbonBack(inherit.RibbonGroupCollapsedFrameBorder, needPaint);
            _ribbonGroupCollapsedFrameBack = new PaletteRibbonBack(inherit.RibbonGroupCollapsedFrameBack, needPaint);
            _ribbonGroupCollapsedText = new PaletteRibbonText(inherit.RibbonGroupCollapsedText, needPaint);
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
                        RibbonGroupNormalBorder.IsDefault &&
                        RibbonGroupNormalTitle.IsDefault &&
                        RibbonGroupCollapsedBorder.IsDefault &&
                        RibbonGroupCollapsedBack.IsDefault &&
                        RibbonGroupCollapsedFrameBorder.IsDefault &&
                        RibbonGroupCollapsedFrameBack.IsDefault &&
                        RibbonGroupCollapsedText.IsDefault); 
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="state">The palette state to populate with.</param>
        public override void PopulateFromBase(PaletteState state)
        {
            base.PopulateFromBase(state);
            _ribbonGroupNormalBorder.PopulateFromBase(state);
            _ribbonGroupNormalTitle.PopulateFromBase(state);
            _ribbonGroupCollapsedBorder.PopulateFromBase(state);
            _ribbonGroupCollapsedBack.PopulateFromBase(state);
            _ribbonGroupCollapsedFrameBorder.PopulateFromBase(state);
            _ribbonGroupCollapsedFrameBack.PopulateFromBase(state);
            _ribbonGroupCollapsedText.PopulateFromBase(state);
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        public override void SetInherit(PaletteRibbonRedirect inherit)
        {
            base.SetInherit(inherit);
            _ribbonGroupNormalBorder.SetInherit(inherit.RibbonGroupNormalBorder);
            _ribbonGroupNormalTitle.SetInherit(inherit.RibbonGroupNormalTitle, inherit.RibbonGroupNormalTitle);
            _ribbonGroupCollapsedBorder.SetInherit(inherit.RibbonGroupCollapsedBorder);
            _ribbonGroupCollapsedBack.SetInherit(inherit.RibbonGroupCollapsedBack);
            _ribbonGroupCollapsedFrameBorder.SetInherit(inherit.RibbonGroupCollapsedFrameBorder);
            _ribbonGroupCollapsedFrameBack.SetInherit(inherit.RibbonGroupCollapsedFrameBack);
            _ribbonGroupCollapsedText.SetInherit(inherit.RibbonGroupCollapsedText);
        }
        #endregion

        #region RibbonGroupNormalBorder
        /// <summary>
        /// Gets access to the ribbon group normal border palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group normal border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupNormalBorder
        {
            get { return _ribbonGroupNormalBorder; }
        }

        private bool ShouldSerializeRibbonGroupNormalBorder()
        {
            return !_ribbonGroupNormalBorder.IsDefault;
        }
        #endregion

        #region RibbonGroupNormalTitle
        /// <summary>
        /// Gets access to the ribbon group normal title palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group normal title appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonDouble RibbonGroupNormalTitle
        {
            get { return _ribbonGroupNormalTitle; }
        }

        private bool ShouldSerializeRibbonGroupNormalTitle()
        {
            return !_ribbonGroupNormalTitle.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedBorder
        /// <summary>
        /// Gets access to the ribbon group collapsed border palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupCollapsedBorder
        {
            get { return _ribbonGroupCollapsedBorder; }
        }

        private bool ShouldSerializeRibbonGroupCollapsedBorder()
        {
            return !_ribbonGroupCollapsedBorder.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedBack
        /// <summary>
        /// Gets access to the ribbon group collapsed background palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed background appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupCollapsedBack
        {
            get { return _ribbonGroupCollapsedBack; }
        }

        private bool ShouldSerializeRibbonGroupCollapsedBack()
        {
            return !_ribbonGroupCollapsedBack.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedFrameBorder
        /// <summary>
        /// Gets access to the ribbon group collapsed frame border palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed frame border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupCollapsedFrameBorder
        {
            get { return _ribbonGroupCollapsedFrameBorder; }
        }

        private bool ShouldSerializeRibbonGroupCollapsedFrameBorder()
        {
            return !_ribbonGroupCollapsedFrameBorder.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedFrameBack
        /// <summary>
        /// Gets access to the ribbon group collapsed frame background palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed frame background appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupCollapsedFrameBack
        {
            get { return _ribbonGroupCollapsedFrameBack; }
        }

        private bool ShouldSerializeRibbonGroupCollapsedFrameBack()
        {
            return !_ribbonGroupCollapsedFrameBack.IsDefault;
        }
        #endregion

        #region RibbonGroupCollapsedText
        /// <summary>
        /// Gets access to the ribbon group collapsed text palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group collapsed text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonText RibbonGroupCollapsedText
        {
            get { return _ribbonGroupCollapsedText; }
        }

        private bool ShouldSerializeRibbonGroupCollapsedText()
        {
            return !_ribbonGroupCollapsedText.IsDefault;
        }
        #endregion
    }
}
