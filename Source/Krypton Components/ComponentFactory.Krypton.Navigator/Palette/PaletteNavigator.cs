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
	/// Implement storage for normal and disable navigator appearance.
	/// </summary>
    public class PaletteNavigator : PaletteDoubleMetric
    {
        #region Instance Fields
        private PalettePage _palettePage;
        private PaletteNavigatorHeaderGroup _paletteHeaderGroup;
        private PaletteTriple _paletteCheckButton;
        private PaletteTriple _paletteOverflowButton;
        private PaletteTriple _paletteMiniButton;
        private PaletteTabTriple _paletteTab;
        private PaletteBorderEdge _paletteBorderEdge;
        private PaletteSeparatorPadding _paletteSeparator;
        private PaletteRibbonTabContent _paletteRibbonTab;
        #endregion

        #region Identity
        /// <summary>
		/// Initialize a new instance of the PaletteNavigatorNormabled class.
		/// </summary>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigator(PaletteNavigatorRedirect redirect,
                                NeedPaintHandler needPaint)
            : base(redirect, needPaint)
		{
            // Create the palette storage
            _palettePage = new PalettePage(redirect.PalettePage, needPaint);
            _paletteHeaderGroup = new PaletteNavigatorHeaderGroup(redirect.HeaderGroup, redirect.HeaderGroup.HeaderPrimary, redirect.HeaderGroup.HeaderSecondary, redirect.HeaderGroup.HeaderBar, redirect.HeaderGroup.HeaderOverflow, needPaint);
            _paletteCheckButton = new PaletteTriple(redirect.CheckButton, needPaint);
            _paletteOverflowButton = new PaletteTriple(redirect.OverflowButton, needPaint);
            _paletteMiniButton = new PaletteTriple(redirect.MiniButton, needPaint);
            _paletteBorderEdge = new PaletteBorderEdge(redirect.BorderEdge, needPaint);
            _paletteSeparator = new PaletteSeparatorPadding(redirect.Separator, redirect.Separator, needPaint);
            _paletteTab = new PaletteTabTriple(redirect.Tab, needPaint);
            _paletteRibbonTab = new PaletteRibbonTabContent(redirect.RibbonTab.TabDraw, redirect.RibbonTab.TabDraw, redirect.RibbonTab.Content, needPaint);
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
                        _palettePage.IsDefault &&
                        _paletteHeaderGroup.IsDefault &&
                        _paletteCheckButton.IsDefault &&
                        _paletteOverflowButton.IsDefault &&
                        _paletteMiniButton.IsDefault &&
                        _paletteBorderEdge.IsDefault &&
                        _paletteSeparator.IsDefault &&
                        _paletteTab.IsDefault &&
                        _paletteRibbonTab.IsDefault);
			}
		}
		#endregion

        #region SetInherit
        /// <summary>
        /// Sets the inheritence parent.
        /// </summary>
        /// <param name="inheritNavigator">Source for inheriting.</param>
        public void SetInherit(PaletteNavigator inheritNavigator)
        {
            // Setup inheritance references for storage objects
            base.SetInherit(inheritNavigator);
            _palettePage.SetInherit(inheritNavigator.PalettePage);
            _paletteHeaderGroup.SetInherit(inheritNavigator.HeaderGroup);
            _paletteCheckButton.SetInherit(inheritNavigator.CheckButton);
            _paletteOverflowButton.SetInherit(inheritNavigator.OverflowButton);
            _paletteMiniButton.SetInherit(inheritNavigator.MiniButton);
            _paletteBorderEdge.SetInherit(inheritNavigator.BorderEdge);
            _paletteSeparator.SetInherit(inheritNavigator.Separator);
            _paletteTab.SetInherit(inheritNavigator.Tab);
            _paletteRibbonTab.SetInherit(inheritNavigator.RibbonTab.TabDraw, inheritNavigator.RibbonTab.TabDraw, inheritNavigator.RibbonTab.Content);
        }
        #endregion

        #region Back
        /// <summary>
        /// Gets access to the background palette details.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PaletteBack Back
        {
            get { return base.Back; }
        }


        /// <summary>
        /// Gets the background palette.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IPaletteBack PaletteBack
        {
            get { return base.PaletteBack; }
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

        #region Panel
        /// <summary>
        /// Gets access to the panel palette details.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining panel appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBack Panel
        {
            get { return base.Back; }
        }

        private bool ShouldSerializePanel()
        {
            return !base.Back.IsDefault;
        }
        #endregion

        #region CheckButton
        /// <summary>
        /// Gets access to the check button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining check button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple CheckButton
        {
            get { return _paletteCheckButton; }
        }

        private bool ShouldSerializeCheckButton()
        {
            return !_paletteCheckButton.IsDefault;
        }
        #endregion

        #region OverflowButton
        /// <summary>
        /// Gets access to the outlook overflow button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining outlook overflow button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple OverflowButton
        {
            get { return _paletteOverflowButton; }
        }

        private bool ShouldSerializeOverflowButton()
        {
            return !_paletteOverflowButton.IsDefault;
        }
        #endregion

        #region MiniButton
        /// <summary>
        /// Gets access to the outlook mini button appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining outlook mini button appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple MiniButton
        {
            get { return _paletteMiniButton; }
        }

        private bool ShouldSerializeMiniButton()
        {
            return !_paletteMiniButton.IsDefault;
        }
        #endregion

        #region HeaderGroup
        /// <summary>
        /// Gets access to the header group appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining header group appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteNavigatorHeaderGroup HeaderGroup
        {
            get { return _paletteHeaderGroup; }
        }

        private bool ShouldSerializeHeaderGroup()
        {
            return !_paletteHeaderGroup.IsDefault;
        }
        #endregion

        #region Page
        /// <summary>
        /// Gets access to the page appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining page appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBack Page
        {
            get { return _palettePage.Back; }
        }

        private bool ShouldSerializePage()
        {
            return !_palettePage.Back.IsDefault;
        }
        #endregion

        #region BorderEdge
        /// <summary>
        /// Gets access to the border edge appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining border edge appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBorderEdge BorderEdge
        {
            get { return _paletteBorderEdge; }
        }

        private bool ShouldSerializeBorderEdge()
        {
            return !_paletteBorderEdge.IsDefault;
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

        #region Tab
        /// <summary>
        /// Gets access to the tab appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tab appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTriple Tab
        {
            get { return _paletteTab; }
        }

        private bool ShouldSerializeTab()
        {
            return !_paletteTab.IsDefault;
        }
        #endregion

        #region RibbonTab
        /// <summary>
        /// Gets access to the ribbon tab appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon tab appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonTabContent RibbonTab
        {
            get { return _paletteRibbonTab; }
        }

        private bool ShouldSerializeRibbonTab()
        {
            return !_paletteRibbonTab.IsDefault;
        }
        #endregion

        #region Internal
        internal PalettePage PalettePage
        {
            get { return _palettePage; }
        }
        #endregion
    }
}
