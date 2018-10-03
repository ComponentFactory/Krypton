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
	/// Implement redirected storage for common navigator appearance.
	/// </summary>
    public class PaletteNavigatorRedirect : PaletteDoubleMetricRedirect
	{
        #region Instance Fields
        private PalettePageRedirect _palettePageRedirect;
        private PaletteNavigatorHeaderGroupRedirect _paletteHeaderGroupRedirect;
        private PaletteTripleRedirect _paletteCheckButton;
        private PaletteTripleRedirect _paletteOverflowButton;
        private PaletteTripleRedirect _paletteMiniButton;
        private PaletteTabTripleRedirect _paletteTab;
        private PaletteBarRedirect _paletteBarRedirect;
        private PaletteBorderInheritRedirect _paletteBorderEdgeInheritRedirect;
        private PaletteBorderEdgeRedirect _paletteBorderEdgeRedirect;
        private PaletteSeparatorPaddingRedirect _paletteSeparator;
        private PaletteRibbonTabContentRedirect _paletteRibbonTab;
        private PaletteRibbonGeneralNavRedirect _paletteRibbonGeneral;
        private PaletteMetrics _paletteMetrics;
        #endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the PaletteNavigatorNormabled class.
		/// </summary>
        /// <param name="navigator">Reference to owning navigator.</param>
		/// <param name="redirect">Inheritence redirection instance.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorRedirect(KryptonNavigator navigator,
                                        PaletteRedirect redirect,
                                        NeedPaintHandler needPaint)
            : this(navigator, redirect, redirect, redirect, 
                              redirect, redirect, redirect,
                              redirect, redirect, redirect,
                              redirect, redirect, redirect,
                              redirect, redirect, redirect,
                              redirect, needPaint)
        {
        }

		/// <summary>
		/// Initialize a new instance of the PaletteNavigatorNormabled class.
		/// </summary>
        /// <param name="navigator">Reference to owning navigator.</param>
        /// <param name="redirectNavigator">Inheritence redirection for navigator level.</param>
        /// <param name="redirectNavigatorPage">Inheritence redirection for page level.</param>
        /// <param name="redirectNavigatorHeaderGroup">Inheritence redirection for header groups level.</param>
        /// <param name="redirectNavigatorHeaderPrimary">Inheritence redirection for primary header.</param>
        /// <param name="redirectNavigatorHeaderSecondary">Inheritence redirection for secondary header.</param>
        /// <param name="redirectNavigatorHeaderBar">Inheritence redirection for bar header.</param>
        /// <param name="redirectNavigatorHeaderOverflow">Inheritence redirection for bar header.</param>
        /// <param name="redirectNavigatorCheckButton">Inheritence redirection for check button.</param>
        /// <param name="redirectNavigatorOverflowButton">Inheritence redirection for overflow button.</param>
        /// <param name="redirectNavigatorMiniButton">Inheritence redirection for check button.</param>
        /// <param name="redirectNavigatorBar">Inheritence redirection for bar.</param>
        /// <param name="redirectNavigatorBorderEdge">Inheritence redirection for border edge.</param>
        /// <param name="redirectNavigatorSeparator">Inheritence redirection for separator.</param>
        /// <param name="redirectNavigatorTab">Inheritence redirection for tab.</param>
        /// <param name="redirectNavigatorRibbonTab">Inheritence redirection for ribbon tab.</param>
        /// <param name="redirectNavigatorRibbonGeneral">Inheritence redirection for ribbon general.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteNavigatorRedirect(KryptonNavigator navigator,
                                        PaletteRedirect redirectNavigator,
                                        PaletteRedirect redirectNavigatorPage,
                                        PaletteRedirect redirectNavigatorHeaderGroup,
                                        PaletteRedirect redirectNavigatorHeaderPrimary,
                                        PaletteRedirect redirectNavigatorHeaderSecondary,
                                        PaletteRedirect redirectNavigatorHeaderBar,
                                        PaletteRedirect redirectNavigatorHeaderOverflow,
                                        PaletteRedirect redirectNavigatorCheckButton,
                                        PaletteRedirect redirectNavigatorOverflowButton,
                                        PaletteRedirect redirectNavigatorMiniButton,
                                        PaletteRedirect redirectNavigatorBar,
                                        PaletteRedirect redirectNavigatorBorderEdge,
                                        PaletteRedirect redirectNavigatorSeparator,
                                        PaletteRedirect redirectNavigatorTab,
                                        PaletteRedirect redirectNavigatorRibbonTab,
                                        PaletteRedirect redirectNavigatorRibbonGeneral,
                                        NeedPaintHandler needPaint)
            : base(redirectNavigator, PaletteBackStyle.PanelClient,
                   PaletteBorderStyle.ControlClient, needPaint)
		{
            // Create the palette storage
            _palettePageRedirect = new PalettePageRedirect(redirectNavigatorPage, needPaint);
            _paletteHeaderGroupRedirect = new PaletteNavigatorHeaderGroupRedirect(redirectNavigatorHeaderGroup, redirectNavigatorHeaderPrimary, redirectNavigatorHeaderSecondary, redirectNavigatorHeaderBar, redirectNavigatorHeaderOverflow, needPaint);
            _paletteCheckButton = new PaletteTripleRedirect(redirectNavigatorCheckButton, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
            _paletteOverflowButton = new PaletteTripleRedirect(redirectNavigatorOverflowButton, PaletteBackStyle.ButtonNavigatorOverflow, PaletteBorderStyle.ButtonNavigatorOverflow, PaletteContentStyle.ButtonNavigatorOverflow, needPaint);
            _paletteMiniButton = new PaletteTripleRedirect(redirectNavigatorMiniButton, PaletteBackStyle.ButtonNavigatorMini, PaletteBorderStyle.ButtonNavigatorMini, PaletteContentStyle.ButtonNavigatorMini, needPaint);
            _paletteBarRedirect = new PaletteBarRedirect(redirectNavigatorBar, needPaint);
            _paletteBorderEdgeInheritRedirect = new PaletteBorderInheritRedirect(redirectNavigatorBorderEdge, PaletteBorderStyle.ControlClient);
            _paletteBorderEdgeRedirect = new PaletteBorderEdgeRedirect(_paletteBorderEdgeInheritRedirect, needPaint);
            _paletteSeparator = new PaletteSeparatorPaddingRedirect(redirectNavigatorSeparator, PaletteBackStyle.SeparatorHighInternalProfile, PaletteBorderStyle.SeparatorHighInternalProfile, needPaint);
            _paletteTab = new PaletteTabTripleRedirect(redirectNavigatorTab, PaletteBackStyle.TabHighProfile, PaletteBorderStyle.TabHighProfile, PaletteContentStyle.TabHighProfile, needPaint);
            _paletteRibbonTab = new PaletteRibbonTabContentRedirect(redirectNavigatorRibbonTab, needPaint);
            _paletteRibbonGeneral = new PaletteRibbonGeneralNavRedirect(redirectNavigatorRibbonGeneral, needPaint);
            _paletteMetrics = new PaletteMetrics(navigator, needPaint);
        }
		#endregion

        #region RedirectBorderEdge
        /// <summary>
        /// Update the redirector for the border edge.
        /// </summary>
        public PaletteRedirect RedirectBorderEdge
        {
            set { _paletteBorderEdgeInheritRedirect.SetRedirector(value); }
        }
        #endregion

        #region RedirectRibbonGeneral
        /// <summary>
        /// Update the redirector for the ribbon general.
        /// </summary>
        public PaletteRedirect RedirectRibbonGeneral
        {
            set { _paletteRibbonGeneral.SetRedirector(value); }
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
                        _palettePageRedirect.IsDefault &&
                        _paletteHeaderGroupRedirect.IsDefault &&
                        _paletteCheckButton.IsDefault &&
                        _paletteOverflowButton.IsDefault &&
                        _paletteMiniButton.IsDefault &&
                        _paletteBarRedirect.IsDefault &&
                        _paletteBorderEdgeRedirect.IsDefault &&
                        _paletteSeparator.IsDefault &&
                        _paletteTab.IsDefault &&
                        _paletteRibbonTab.IsDefault &&
                        _paletteRibbonGeneral.IsDefault &&
                        _paletteMetrics.IsDefault);
            }
        }
        #endregion

        #region Bar
        /// <summary>
        /// Gets access to the bar appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining bar appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBarRedirect Bar
        {
            get { return _paletteBarRedirect; }
        }

        private bool ShouldSerializeBar()
        {
            return !_paletteBarRedirect.IsDefault;
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
        public PaletteTripleRedirect CheckButton
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
        public PaletteTripleRedirect OverflowButton
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
        public PaletteTripleRedirect MiniButton
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
        public PaletteNavigatorHeaderGroupRedirect HeaderGroup
        {
            get { return _paletteHeaderGroupRedirect; }
        }

        private bool ShouldSerializeHeaderGroup()
        {
            return !_paletteHeaderGroupRedirect.IsDefault;
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
            get { return _palettePageRedirect.Back; }
        }

        private bool ShouldSerializePage()
        {
            return !_palettePageRedirect.Back.IsDefault;
        }
        #endregion

        #region BorderEdge
        /// <summary>
        /// Gets access to the border edge appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining border edge appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBorderEdgeRedirect BorderEdge
        {
            get { return _paletteBorderEdgeRedirect; }
        }

        private bool ShouldSerializeBorderEdge()
        {
            return !_paletteBorderEdgeRedirect.IsDefault;
        }
        #endregion

        #region Metrics
        /// <summary>
        /// Gets access to the metrics entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining metric entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteMetrics Metrics
        {
            get { return _paletteMetrics; }
        }

        private bool ShouldSerializeMetrics()
        {
            return !_paletteMetrics.IsDefault;
        }
        #endregion

        #region Separator
        /// <summary>
        /// Get access to the overrides for defining separator appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSeparatorPaddingRedirect Separator
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
        public PaletteTabTripleRedirect Tab
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
        public PaletteRibbonTabContentRedirect RibbonTab
        {
            get { return _paletteRibbonTab; }
        }

        private bool ShouldSerializeRibbonTab()
        {
            return !_paletteRibbonTab.IsDefault;
        }
        #endregion

        #region RibbonGeneral
        /// <summary>
        /// Gets access to the ribbon general appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon general appearance entries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonGeneralNavRedirect RibbonGeneral
        {
            get { return _paletteRibbonGeneral; }
        }

        private bool ShouldSerializeRibbonGeneral()
        {
            return !_paletteRibbonGeneral.IsDefault;
        }
        #endregion

        #region IPaletteMetric
        /// <summary>
        /// Gets an integer metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Integer value.</returns>
        public override int GetMetricInt(PaletteState state, PaletteMetricInt metric)
        {
            switch (metric)
            {
                case PaletteMetricInt.PageButtonInset:
                    if (_paletteMetrics.PageButtonSpecInset != -1)
                        return _paletteMetrics.PageButtonSpecInset;
                    break;
            }

            // Pass onto the inheritance
            return base.GetMetricInt(state, metric);
        }

        /// <summary>
        /// Gets a padding metric value.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <param name="metric">Requested metric.</param>
        /// <returns>Padding value.</returns>
        public override Padding GetMetricPadding(PaletteState state, PaletteMetricPadding metric)
        {
            switch (metric)
            {
                case PaletteMetricPadding.PageButtonPadding:
                    if (!_paletteMetrics.PageButtonSpecPadding.Equals(CommonHelper.InheritPadding))
                        return _paletteMetrics.PageButtonSpecPadding;
                    break;
            }

            // Pass onto the inheritance
            return base.GetMetricPadding(state, metric);
        }
        #endregion

        #region Internal
        internal PalettePageRedirect PalettePage
        {
            get { return _palettePageRedirect; }
        }

        internal PaletteBorderStyle BorderEdgeStyle
        {
            set { _paletteBorderEdgeInheritRedirect.Style = value; }
        }
        #endregion    
    }
}
