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
    /// Implement storage for a ribbon palette. 
	/// </summary>
    public class PaletteRibbonRedirect : PaletteMetricRedirect
	{
		#region Instance Fields
        // Storage
        private PaletteRibbonBack _ribbonAppButton;
        private PaletteRibbonBack _ribbonAppMenuInner;
        private PaletteRibbonBack _ribbonAppMenuOuter;
        private PaletteRibbonBack _ribbonAppMenuDocs;
        private PaletteRibbonText _ribbonAppMenuDocsTitle;
        private PaletteRibbonText _ribbonAppMenuDocsEntry;
        private PaletteRibbonGeneral _ribbonGeneral;
        private PaletteRibbonBack _ribbonGroupArea;
        private PaletteRibbonText _ribbonGroupButtonText;
        private PaletteRibbonText _ribbonGroupCheckBoxText;
        private PaletteRibbonBack _ribbonGroupCollapsedBack;
        private PaletteRibbonBack _ribbonGroupCollapsedBorder;
        private PaletteRibbonBack _ribbonGroupCollapsedFrameBack;
        private PaletteRibbonBack _ribbonGroupCollapsedFrameBorder;
        private PaletteRibbonText _ribbonGroupCollapsedText;
        private PaletteRibbonBack _ribbonGroupNormalBorder;
        private PaletteRibbonDouble _ribbonGroupNormalTitle;
        private PaletteRibbonImages _ribbonImages;
        private PaletteRibbonText _ribbonGroupRadioButtonText;
        private PaletteRibbonText _ribbonGroupLabelText;
        private PaletteRibbonDouble _ribbonTab;
        private PaletteRibbonBack _ribbonQATFullbar;
        private PaletteRibbonBack _ribbonQATMinibarActive;
        private PaletteRibbonBack _ribbonQATMinibarInactive;
        private PaletteRibbonBack _ribbonQATOverflow;

        // Redirection
        private PaletteRibbonBackInheritRedirect _ribbonAppButtonInherit;
        private PaletteRibbonBackInheritRedirect _ribbonAppMenuOuterInherit;
        private PaletteRibbonBackInheritRedirect _ribbonAppMenuInnerInherit;
        private PaletteRibbonBackInheritRedirect _ribbonAppMenuDocsInherit;
        private PaletteRibbonTextInheritRedirect _ribbonAppMenuDocsTitleInherit;
        private PaletteRibbonTextInheritRedirect _ribbonAppMenuDocsEntryInherit;
        private PaletteRibbonGeneralInheritRedirect _ribbonGeneralInherit;
        private PaletteRibbonBackInheritRedirect _ribbonGroupAreaInherit;
        private PaletteRibbonTextInheritRedirect _ribbonGroupCheckBoxTextInherit;
        private PaletteRibbonTextInheritRedirect _ribbonGroupButtonTextInherit;
        private PaletteRibbonBackInheritRedirect _ribbonGroupCollapsedBackInherit;
        private PaletteRibbonBackInheritRedirect _ribbonGroupCollapsedBorderInherit;
        private PaletteRibbonBackInheritRedirect _ribbonGroupCollapsedFrameBackInherit;
        private PaletteRibbonBackInheritRedirect _ribbonGroupCollapsedFrameBorderInherit;
        private PaletteRibbonTextInheritRedirect _ribbonGroupCollapsedTextInherit;
        private PaletteRibbonBackInheritRedirect _ribbonGroupNormalBorderInherit;
        private PaletteRibbonDoubleInheritRedirect _ribbonGroupNormalTitleInherit;
        private PaletteRibbonTextInheritRedirect _ribbonGroupRadioButtonTextInherit;
        private PaletteRibbonTextInheritRedirect _ribbonGroupLabelTextInherit;
        private PaletteRibbonDoubleInheritRedirect _ribbonTabInherit;
        private PaletteRibbonBackInheritRedirect _ribbonQATFullbarInherit;
        private PaletteRibbonBackInheritRedirect _ribbonQATMinibarInherit;
        private PaletteRibbonBackInheritRedirect _ribbonQATOverflowInherit;

        // Style Redirection
        private PaletteTripleRedirect _groupButtonInherit;
        private PaletteTripleRedirect _groupClusterButtonInherit;
        private PaletteTripleRedirect _groupCollapsedButtonInherit;
        private PaletteTripleRedirect _groupDialogButtonInherit;
        private PaletteTripleRedirect _keyTipInherit;
        private PaletteTripleRedirect _qatButtonInherit;
        private PaletteTripleRedirect _scrollerInherit;
        #endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the PaletteDoubleRedirect class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="panelBackStyle">Initial background style.</param>
        /// <param name="needPaint">Paint delegate.</param>
        public PaletteRibbonRedirect(PaletteRedirect redirect,
									 PaletteBackStyle panelBackStyle,
                                     NeedPaintHandler needPaint)
            : base(redirect)
		{
			Debug.Assert(redirect != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

			// Create the style redirection instances
            _groupButtonInherit = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
            _groupClusterButtonInherit = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
            _groupCollapsedButtonInherit = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
            _groupDialogButtonInherit = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
            _keyTipInherit = new PaletteTripleRedirect(redirect, PaletteBackStyle.ControlToolTip, PaletteBorderStyle.ControlToolTip, PaletteContentStyle.LabelKeyTip, needPaint);
            _qatButtonInherit = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
            _scrollerInherit = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);

            // Create the redirection instances
            _ribbonAppButtonInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonAppButton);
            _ribbonAppMenuInnerInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonAppMenuInner);
            _ribbonAppMenuOuterInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonAppMenuOuter);
            _ribbonAppMenuDocsInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonAppMenuDocs);
            _ribbonAppMenuDocsTitleInherit = new PaletteRibbonTextInheritRedirect(redirect, PaletteRibbonTextStyle.RibbonAppMenuDocsTitle);
            _ribbonAppMenuDocsEntryInherit = new PaletteRibbonTextInheritRedirect(redirect, PaletteRibbonTextStyle.RibbonAppMenuDocsEntry);
            _ribbonGeneralInherit = new PaletteRibbonGeneralInheritRedirect(redirect);
            _ribbonGroupAreaInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupArea);
            _ribbonGroupButtonTextInherit = new PaletteRibbonTextInheritRedirect(redirect, PaletteRibbonTextStyle.RibbonGroupButtonText);
            _ribbonGroupCheckBoxTextInherit = new PaletteRibbonTextInheritRedirect(redirect, PaletteRibbonTextStyle.RibbonGroupCheckBoxText);
            _ribbonGroupCollapsedBackInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedBack);
            _ribbonGroupCollapsedBorderInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedBorder);
            _ribbonGroupCollapsedFrameBackInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack);
            _ribbonGroupCollapsedFrameBorderInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBorder);
            _ribbonGroupCollapsedTextInherit = new PaletteRibbonTextInheritRedirect(redirect, PaletteRibbonTextStyle.RibbonGroupCollapsedText);
            _ribbonGroupNormalBorderInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupNormalBorder);
            _ribbonGroupNormalTitleInherit = new PaletteRibbonDoubleInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupNormalTitle, PaletteRibbonTextStyle.RibbonGroupNormalTitle);
            _ribbonGroupRadioButtonTextInherit = new PaletteRibbonTextInheritRedirect(redirect, PaletteRibbonTextStyle.RibbonGroupRadioButtonText);
            _ribbonGroupLabelTextInherit = new PaletteRibbonTextInheritRedirect(redirect, PaletteRibbonTextStyle.RibbonGroupLabelText);
            _ribbonTabInherit = new PaletteRibbonDoubleInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonTab, PaletteRibbonTextStyle.RibbonTab);
            _ribbonQATFullbarInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonQATFullbar);
            _ribbonQATMinibarInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonQATMinibar);
            _ribbonQATOverflowInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonQATOverflow);        

			// Create storage that maps onto the inherit instances
            _ribbonAppButton = new PaletteRibbonBack(_ribbonAppButtonInherit, needPaint);
            _ribbonAppMenuInner = new PaletteRibbonBack(_ribbonAppMenuInnerInherit, needPaint);
            _ribbonAppMenuOuter = new PaletteRibbonBack(_ribbonAppMenuOuterInherit, needPaint);
            _ribbonAppMenuDocs = new PaletteRibbonBack(_ribbonAppMenuDocsInherit, needPaint);
            _ribbonAppMenuDocsTitle = new PaletteRibbonText(_ribbonAppMenuDocsTitleInherit, needPaint);
            _ribbonAppMenuDocsEntry = new PaletteRibbonText(_ribbonAppMenuDocsEntryInherit, needPaint);
            _ribbonGeneral = new PaletteRibbonGeneral(_ribbonGeneralInherit, needPaint);
            _ribbonGroupArea = new PaletteRibbonBack(_ribbonGroupAreaInherit, needPaint);
            _ribbonGroupButtonText = new PaletteRibbonText(_ribbonGroupButtonTextInherit, needPaint);
            _ribbonGroupCheckBoxText = new PaletteRibbonText(_ribbonGroupCheckBoxTextInherit, needPaint);
            _ribbonGroupCollapsedBack = new PaletteRibbonBack(_ribbonGroupCollapsedBackInherit, needPaint);
            _ribbonGroupCollapsedBorder = new PaletteRibbonBack(_ribbonGroupCollapsedBorderInherit, needPaint);
            _ribbonGroupCollapsedFrameBack = new PaletteRibbonBack(_ribbonGroupCollapsedFrameBackInherit, needPaint);
            _ribbonGroupCollapsedFrameBorder = new PaletteRibbonBack(_ribbonGroupCollapsedFrameBorderInherit, needPaint);
            _ribbonGroupCollapsedText = new PaletteRibbonText(_ribbonGroupCollapsedTextInherit, needPaint);
            _ribbonGroupNormalBorder = new PaletteRibbonBack(_ribbonGroupNormalBorderInherit, needPaint);
            _ribbonGroupNormalTitle = new PaletteRibbonDouble(_ribbonGroupNormalTitleInherit, _ribbonGroupNormalTitleInherit, needPaint);
            _ribbonGroupRadioButtonText = new PaletteRibbonText(_ribbonGroupRadioButtonTextInherit, needPaint);
            _ribbonGroupLabelText = new PaletteRibbonText(_ribbonGroupLabelTextInherit, needPaint);
            _ribbonTab = new PaletteRibbonDouble(_ribbonTabInherit, _ribbonTabInherit, needPaint);
            _ribbonQATFullbar = new PaletteRibbonBack(_ribbonQATFullbarInherit, needPaint);
            _ribbonQATMinibarActive = new PaletteRibbonBack(_ribbonQATMinibarInherit, needPaint);
            _ribbonQATMinibarInactive = new PaletteRibbonBack(_ribbonQATMinibarInherit, needPaint);
            _ribbonQATOverflow = new PaletteRibbonBack(_ribbonQATOverflowInherit, needPaint);
            _ribbonImages = new PaletteRibbonImages(redirect, NeedPaintDelegate);
        }
		#endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public override void SetRedirector(PaletteRedirect redirect)
        {
            base.SetRedirector(redirect);
            _groupButtonInherit.SetRedirector(redirect);
            _groupClusterButtonInherit.SetRedirector(redirect);
            _groupCollapsedButtonInherit.SetRedirector(redirect);
            _groupDialogButtonInherit.SetRedirector(redirect);
            _keyTipInherit.SetRedirector(redirect);
            _qatButtonInherit.SetRedirector(redirect);
            _scrollerInherit.SetRedirector(redirect);
            _ribbonAppButtonInherit.SetRedirector(redirect);
            _ribbonAppMenuInnerInherit.SetRedirector(redirect);
            _ribbonAppMenuOuterInherit.SetRedirector(redirect);
            _ribbonAppMenuDocsInherit.SetRedirector(redirect);
            _ribbonAppMenuDocsTitleInherit.SetRedirector(redirect);
            _ribbonAppMenuDocsEntryInherit.SetRedirector(redirect);
            _ribbonGeneralInherit.SetRedirector(redirect);
            _ribbonGroupAreaInherit.SetRedirector(redirect);
            _ribbonGroupCheckBoxTextInherit.SetRedirector(redirect);
            _ribbonGroupNormalBorderInherit.SetRedirector(redirect);
            _ribbonGroupNormalTitleInherit.SetRedirector(redirect);
            _ribbonGroupButtonTextInherit.SetRedirector(redirect);
            _ribbonGroupCollapsedBackInherit.SetRedirector(redirect);
            _ribbonGroupCollapsedBorderInherit.SetRedirector(redirect);
            _ribbonGroupCollapsedFrameBackInherit.SetRedirector(redirect);
            _ribbonGroupCollapsedFrameBorderInherit.SetRedirector(redirect);
            _ribbonGroupCollapsedTextInherit.SetRedirector(redirect);
            _ribbonGroupRadioButtonTextInherit.SetRedirector(redirect);
            _ribbonGroupLabelTextInherit.SetRedirector(redirect);
            _ribbonTabInherit.SetRedirector(redirect);
            _ribbonQATFullbarInherit.SetRedirector(redirect);
            _ribbonQATMinibarInherit.SetRedirector(redirect);
            _ribbonQATOverflowInherit.SetRedirector(redirect);
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
                return (RibbonAppButton.IsDefault &&
                        RibbonAppMenuOuter.IsDefault &&
                        RibbonAppMenuInner.IsDefault &&
                        RibbonAppMenuDocs.IsDefault &&
                        RibbonAppMenuDocsTitle.IsDefault &&
                        RibbonAppMenuDocsEntry.IsDefault &&
                        RibbonGeneral.IsDefault &&
                        RibbonGroupArea.IsDefault &&
                        RibbonGroupCheckBoxText.IsDefault &&
                        RibbonGroupNormalBorder.IsDefault &&
                        RibbonGroupNormalTitle.IsDefault &&
                        RibbonGroupButtonText.IsDefault &&
                        RibbonGroupCollapsedBorder.IsDefault &&
                        RibbonGroupCollapsedBack.IsDefault &&
                        RibbonGroupCollapsedFrameBorder.IsDefault &&
                        RibbonGroupCollapsedFrameBack.IsDefault &&
                        RibbonGroupCollapsedText.IsDefault &&
                        RibbonGroupRadioButtonText.IsDefault &&
                        RibbonGroupLabelText.IsDefault &&
                        RibbonImages.IsDefault &&
                        RibbonTab.IsDefault &&
                        RibbonQATFullbar.IsDefault &&
                        RibbonQATMinibarActive.IsDefault &&
                        RibbonQATMinibarInactive.IsDefault &&
                        RibbonQATOverflow.IsDefault);
            }
		}
		#endregion

        #region RibbonAppButton
        /// <summary>
        /// Gets access to the application button palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining application button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonAppButton
        {
            get { return _ribbonAppButton; }
        }

        private bool ShouldSerializeRibbonAppButton()
        {
            return !_ribbonAppButton.IsDefault;
        }
        #endregion

        #region RibbonAppMenuOuter
        /// <summary>
        /// Gets access to the application button menu outer palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining application button menu outer appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonAppMenuOuter
        {
            get { return _ribbonAppMenuOuter; }
        }

        private bool ShouldSerializeRibbonAppMenuOuter()
        {
            return !_ribbonAppMenuOuter.IsDefault;
        }
        #endregion

        #region RibbonAppMenuInner
        /// <summary>
        /// Gets access to the application button menu inner palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining application button menu inner appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonAppMenuInner
        {
            get { return _ribbonAppMenuInner; }
        }

        private bool ShouldSerializeRibbonAppMenuInner()
        {
            return !_ribbonAppMenuInner.IsDefault;
        }
        #endregion

        #region RibbonAppMenuDocs
        /// <summary>
        /// Gets access to the application button menu recent docs palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining application button menu recent docs appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonAppMenuDocs
        {
            get { return _ribbonAppMenuDocs; }
        }

        private bool ShouldSerializeRibbonAppMenuDocs()
        {
            return !_ribbonAppMenuDocs.IsDefault;
        }
        #endregion

        #region RibbonAppMenuDocsTitle
        /// <summary>
        /// Gets access to the application button menu recent documents title.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining application button menu recent documents title.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonText RibbonAppMenuDocsTitle
        {
            get { return _ribbonAppMenuDocsTitle; }
        }

        private bool ShouldSerializeRibbonAppMenuDocsTitle()
        {
            return !_ribbonAppMenuDocsTitle.IsDefault;
        }
        #endregion

        #region RibbonAppMenuDocsEntry
        /// <summary>
        /// Gets access to the application button menu recent documents entry.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining application button menu recent documents entry.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonText RibbonAppMenuDocsEntry
        {
            get { return _ribbonAppMenuDocsEntry; }
        }

        private bool ShouldSerializeRibbonAppMenuDocsEntry()
        {
            return !_ribbonAppMenuDocsEntry.IsDefault;
        }
        #endregion

        #region RibbonGeneral
        /// <summary>
        /// Gets access to the ribbon general palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining general ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonGeneral RibbonGeneral
        {
            get { return _ribbonGeneral; }
        }

        private bool ShouldSerializeRibbonGeneral()
        {
            return !_ribbonGeneral.IsDefault;
        }
        #endregion

        #region RibbonGroupArea
        /// <summary>
        /// Gets access to the ribbon group area palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group area appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonGroupArea
        {
            get { return _ribbonGroupArea; }
        }

        private bool ShouldSerializeRibbonGroupArea()
        {
            return !_ribbonGroupArea.IsDefault;
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

        #region RibbonGroupLabelText
        /// <summary>
        /// Gets access to the ribbon group label text palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon group label text appearance.")]
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

        #region RibbonImages
        /// <summary>
        /// Gets access to the ribbon images overrides.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon images.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonImages RibbonImages
        {
            get { return _ribbonImages; }
        }

        private bool ShouldSerializeRibbonImages()
        {
            return !_ribbonImages.IsDefault;
        }
        #endregion

        #region RibbonTab
        /// <summary>
        /// Gets access to the ribbon tab palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonDouble RibbonTab
        {
            get { return _ribbonTab; }
        }

        private bool ShouldSerializeRibbonTab()
        {
            return !_ribbonTab.IsDefault;
        }
        #endregion

        #region RibbonQATFullbar
        /// <summary>
        /// Gets access to the ribbon quick access toolbar in full mode palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon quick access toolbar in full mode.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonQATFullbar
        {
            get { return _ribbonQATFullbar; }
        }

        private bool ShouldSerializeRibbonQATFullbar()
        {
            return !_ribbonQATFullbar.IsDefault;
        }
        #endregion

        #region RibbonQATMinibarActive
        /// <summary>
        /// Gets access to the ribbon quick access toolbar in mini mode palette details when form active.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon quick access toolbar in mini mode when form active.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonQATMinibarActive
        {
            get { return _ribbonQATMinibarActive; }
        }

        private bool ShouldSerializeRibbonQATMinibarActive()
        {
            return !_ribbonQATMinibarActive.IsDefault;
        }
        #endregion

        #region RibbonQATMinibarInactive
        /// <summary>
        /// Gets access to the ribbon quick access toolbar in mini mode palette details when form inactive.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon quick access toolbar in mini mode when form inactive.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonQATMinibarInactive
        {
            get { return _ribbonQATMinibarInactive; }
        }

        private bool ShouldSerializeRibbonQATMinibarInactive()
        {
            return !_ribbonQATMinibarInactive.IsDefault;
        }
        #endregion

        #region RibbonQATOverflow
        /// <summary>
        /// Gets access to the ribbon quick access toolbar overflow palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon quick access toolbar overflow.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PaletteRibbonBack RibbonQATOverflow
        {
            get { return _ribbonQATOverflow; }
        }

        private bool ShouldSerializeRibbonQATOverflow()
        {
            return !_ribbonQATOverflow.IsDefault;
        }
        #endregion

        #region Protected
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

        #region Internal
        internal PaletteTripleRedirect RibbonGroupButton
        {
            get { return _groupButtonInherit; }
        }

        internal PaletteTripleRedirect RibbonGroupClusterButton
        {
            get { return _groupClusterButtonInherit; }
        }

        internal PaletteTripleRedirect RibbonGroupCollapsedButton
        {
            get { return _groupCollapsedButtonInherit; }
        }

        internal PaletteTripleRedirect RibbonGroupDialogButton
        {
            get { return _groupDialogButtonInherit; }
        }

        internal PaletteTripleRedirect RibbonKeyTip
        {
            get { return _keyTipInherit; }
        }

        internal PaletteTripleRedirect RibbonQATButton
        {
            get { return _qatButtonInherit; }
        }

        internal PaletteTripleRedirect RibbonScroller
        {
            get { return _scrollerInherit; }
        }
        #endregion
    }
}
