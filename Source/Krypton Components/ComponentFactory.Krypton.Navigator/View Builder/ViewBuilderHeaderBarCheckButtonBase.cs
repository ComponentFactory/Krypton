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
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
    /// Base class for implementation of 'HeaderBar - CheckButton' modes.
	/// </summary>
    internal abstract class ViewBuilderHeaderBarCheckButtonBase : ViewBuilderItemBase
    {
        #region Instance Fields
        protected ViewDrawDocker _drawPanelDocker;
        protected ViewDrawDocker _viewHeadingBar;
        #endregion

        #region Protected
		/// <summary>
		/// Construct the view appropriate for this builder.
		/// </summary>
		/// <param name="navigator">Reference to navigator instance.</param>
		/// <param name="manager">Reference to current manager.</param>
		/// <param name="redirector">Palette redirector.</param>
        public override void Construct(KryptonNavigator navigator,
                                       ViewManager manager,
                                       PaletteRedirect redirector)
        {
            // Let base class perform common operations
            base.Construct(navigator, manager, redirector);

            // Need to monitor changes in the enabled state
            Navigator.EnabledChanged += new EventHandler(OnEnabledChanged);
        }

        /// <summary>
        /// Destruct the previously created view.
        /// </summary>
        public override void Destruct()
        {
            // Unhook from events
            Navigator.EnabledChanged -= new EventHandler(OnEnabledChanged);

            // Let base class do standard work
            base.Destruct();
        }

        /// <summary>
        /// Create a manager for handling the button specifications.
        /// </summary>
        protected override void CreateButtonSpecManager()
        {
            // Create button specification collection manager
            _buttonManager = new ButtonSpecNavManagerLayoutHeaderBar(Navigator, Redirector, Navigator.Button.ButtonSpecs, Navigator.FixedSpecs,
                                                                     new ViewLayoutDocker[] { _layoutBarDocker },
                                                                     new IPaletteMetric[] { Navigator.StateCommon.Bar },
                                                                     new PaletteMetricInt[] { PaletteMetricInt.BarButtonEdgeInside },
                                                                     new PaletteMetricInt[] { PaletteMetricInt.BarButtonEdgeOutside },
                                                                     new PaletteMetricPadding[] { PaletteMetricPadding.BarButtonPadding },
                                                                     new GetToolStripRenderer(Navigator.CreateToolStripRenderer),
                                                                     NeedPaintDelegate,
                                                                     GetRemappingPaletteContent(),
                                                                     GetRemappingPaletteState());

            // Hook up the tooltip manager so that tooltips can be generated
            _buttonManager.ToolTipManager = Navigator.ToolTipManager;
        }

        /// <summary>
        /// Allow operations to occur after main construct actions.
        /// </summary>
        protected override void PostCreate()
        {
            SetHeaderStyle(_viewHeadingBar, Navigator.StateCommon.HeaderGroup.HeaderBar, Navigator.Header.HeaderStyleBar);
            _viewHeadingBar.Visible = Navigator.Header.HeaderVisibleBar;
            base.PostCreate();
        }

        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public override void UpdateStatePalettes()
        {
            PaletteNavigator paletteState;

            // If whole navigator is disabled then all views are disabled
            bool enabled = Navigator.Enabled;

            // If there is no selected page
            if (Navigator.SelectedPage == null)
            {
                if (Navigator.Enabled)
                    paletteState = Navigator.StateNormal;
                else
                    paletteState = Navigator.StateDisabled;
            }
            else
            {
                if (Navigator.SelectedPage.Enabled)
                    paletteState = Navigator.SelectedPage.StateNormal;
                else
                {
                    paletteState = Navigator.SelectedPage.StateDisabled;

                    // If page is disabled then all of view should look disabled
                    enabled = false;
                }
            }

            // Update with correct state specific palettes
            _viewHeadingBar.SetPalettes(paletteState.HeaderGroup.HeaderBar.Back,
                                        paletteState.HeaderGroup.HeaderBar.Border);

            // Update with correct enabled state
            _viewHeadingBar.Enabled = enabled;

            // Let base class perform common actions
            base.UpdateStatePalettes();
        }

        /// <summary>
        /// Process the change in a property that might effect the view builder.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Property changed details.</param>
        protected override void OnViewBuilderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HeaderStyleBar":
                    SetHeaderStyle(_viewHeadingBar, Navigator.StateCommon.HeaderGroup.HeaderBar, Navigator.Header.HeaderStyleBar);
                    UpdateStatePalettes();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "HeaderVisibleBar":
                    _viewHeadingBar.Visible = Navigator.Header.HeaderVisibleBar;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "HeaderPositionBar":
                    UpdateOrientation();
                    UpdateItemOrientation();
                    if (_buttonManager != null)
                        _buttonManager.RecreateButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                default:
                    // We do not recognise the property, let base process it
                    base.OnViewBuilderPropertyChanged(sender, e);
                    break;
            }
        }

        /// <summary>
        /// Gets the visual orientation of the check buttton.
        /// </summary>
        /// <returns>Visual orientation.</returns>
        protected override VisualOrientation ConvertButtonBorderBackOrientation()
        {
            return ResolveButtonContentOrientation(Navigator.Header.HeaderPositionBar);
        }

        /// <summary>
        /// Gets the visual orientation of the check butttons content.
        /// </summary>
        /// <returns>Visual orientation.</returns>
        protected override VisualOrientation ConvertButtonContentOrientation()
        {
            return ResolveButtonContentOrientation(Navigator.Header.HeaderPositionBar);
        }
        #endregion

        #region Implementation
        private void SetHeaderStyle(ViewDrawDocker drawDocker,
                                    PaletteTripleMetricRedirect palette,
                                    HeaderStyle style)
        {
            palette.SetStyles(style);

            if (_buttonManager != null)
            {
                switch (style)
                {
                    case HeaderStyle.Primary:
                        _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                        PaletteMetricInt.HeaderButtonEdgeInsetPrimary,
                                                        PaletteMetricPadding.HeaderButtonPaddingPrimary);
                        break;
                    case HeaderStyle.Secondary:
                        _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                        PaletteMetricInt.HeaderButtonEdgeInsetSecondary,
                                                        PaletteMetricPadding.HeaderButtonPaddingSecondary);
                        break;
                    case HeaderStyle.DockActive:
                        _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                        PaletteMetricInt.HeaderButtonEdgeInsetDockActive,
                                                        PaletteMetricPadding.HeaderButtonPaddingDockActive);
                        break;
                    case HeaderStyle.DockInactive:
                        _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                        PaletteMetricInt.HeaderButtonEdgeInsetDockInactive,
                                                        PaletteMetricPadding.HeaderButtonPaddingDockInactive);
                        break;
                    case HeaderStyle.Form:
                        _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                        PaletteMetricInt.HeaderButtonEdgeInsetForm,
                                                        PaletteMetricPadding.HeaderButtonPaddingForm);
                        break;
                    case HeaderStyle.Calendar:
                        _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                        PaletteMetricInt.HeaderButtonEdgeInsetCalendar,
                                                        PaletteMetricPadding.HeaderButtonPaddingCalendar);
                        break;
                    case HeaderStyle.Custom1:
                        _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                        PaletteMetricInt.HeaderButtonEdgeInsetCustom1,
                                                        PaletteMetricPadding.HeaderButtonPaddingCustom1);
                        break;
                    case HeaderStyle.Custom2:
                        _buttonManager.SetDockerMetrics(drawDocker, palette,
                                                        PaletteMetricInt.HeaderButtonEdgeInsetCustom2,
                                                        PaletteMetricPadding.HeaderButtonPaddingCustom2);
                        break;
                }
            }
        }

        private IPaletteContent GetRemappingPaletteContent()
        {
            if (Navigator.Enabled)
                return Navigator.StateNormal.HeaderGroup.HeaderBar.Content;
            else
                return Navigator.StateDisabled.HeaderGroup.HeaderBar.Content;
        }

        private PaletteState GetRemappingPaletteState()
        {
            if (Navigator.Enabled)
                return PaletteState.Normal;
            else
                return PaletteState.Disabled;
        }

        private void OnEnabledChanged(object sender, EventArgs e)
        {
            if (_buttonManager != null)
            {
                // Cast button manager to correct type
                ButtonSpecNavManagerLayoutHeaderBar headerBarBM = (ButtonSpecNavManagerLayoutHeaderBar)_buttonManager;

                // Update with newly calculated values
                headerBarBM.UpdateRemapping(GetRemappingPaletteContent(),
                                            GetRemappingPaletteState());
            }
        }
        #endregion
    }
}
