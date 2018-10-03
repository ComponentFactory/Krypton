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
    /// Implements the NavigatorMode.HeaderBarCheckButtonGroup mode.
	/// </summary>
    internal class ViewBuilderHeaderBarCheckButtonGroup : ViewBuilderHeaderBarCheckButtonBase
    {
        #region Public
        /// <summary>
        /// Gets a value indicating if the mode is a tab strip style mode.
        /// </summary>
        public override bool IsTabStripMode
        {
            get { return false; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create the view hierarchy for this view mode.
        /// </summary>
        protected override void CreateCheckItemView()
        {
            // Create the view element that lays out the check buttons
            _layoutBar = new ViewLayoutBar(Navigator.StateCommon.Bar,
                                           PaletteMetricInt.CheckButtonGap,
                                           Navigator.Bar.ItemSizing,
                                           Navigator.Bar.ItemAlignment,
                                           Navigator.Bar.BarMultiline,
                                           Navigator.Bar.ItemMinimumSize,
                                           Navigator.Bar.ItemMaximumSize,
                                           Navigator.Bar.BarMinimumHeight,
                                           false);

            // Create the scroll spacer that restricts display
            _layoutBarViewport = new ViewLayoutViewport(Navigator.StateCommon.Bar,
                                                        PaletteMetricPadding.BarPaddingInside,
                                                        PaletteMetricInt.CheckButtonGap,
                                                        Navigator.Header.HeaderPositionBar,
                                                        Navigator.Bar.ItemAlignment,
                                                        Navigator.Bar.BarAnimation);
            _layoutBarViewport.Add(_layoutBar);

            // Create the button bar area docker
            _layoutBarDocker = new ViewLayoutDocker();
            _layoutBarDocker.Add(_layoutBarViewport, ViewDockStyle.Fill);

            // Place the bar inside a header style area
            _viewHeadingBar = new ViewDrawDocker(Navigator.StateNormal.HeaderGroup.HeaderBar.Back,
                                                 Navigator.StateNormal.HeaderGroup.HeaderBar.Border,
                                                 Navigator.StateNormal.HeaderGroup.HeaderBar,
                                                 PaletteMetricBool.None,
                                                 PaletteMetricPadding.HeaderGroupPaddingSecondary,
                                                 VisualOrientation.Top);

            _viewHeadingBar.Add(_layoutBarDocker, ViewDockStyle.Fill);

            // Create the docker used to layout contents of main panel and fill with group
            _drawPanelDocker = new ViewDrawDocker(Navigator.StateNormal.HeaderGroup.Back,
                                                  Navigator.StateNormal.HeaderGroup.Border,
                                                  Navigator.StateNormal.HeaderGroup,
                                                  PaletteMetricBool.HeaderGroupOverlay);

            _drawPanelDocker.Add(_oldRoot, ViewDockStyle.Fill);
            _drawPanelDocker.Add(_viewHeadingBar, ViewDockStyle.Top);
            _newRoot = _drawPanelDocker;

            // Must call the base class to perform common actions
            base.CreateCheckItemView();
        }

        /// <summary>
        /// Update the bar orientation.
        /// </summary>
        protected override void UpdateOrientation()
        {
            switch (Navigator.Header.HeaderPositionBar)
            {
                case VisualOrientation.Top:
                    _drawPanelDocker.SetDock(_viewHeadingBar, ViewDockStyle.Top);
                    _viewHeadingBar.Orientation = VisualOrientation.Top;
                    _layoutBarDocker.Orientation = VisualOrientation.Top;
                    _layoutBarViewport.Orientation = VisualOrientation.Top;
                    break;
                case VisualOrientation.Bottom:
                    _drawPanelDocker.SetDock(_viewHeadingBar, ViewDockStyle.Bottom);
                    _viewHeadingBar.Orientation = VisualOrientation.Top;
                    _layoutBarDocker.Orientation = VisualOrientation.Top;
                    _layoutBarViewport.Orientation = VisualOrientation.Top;
                    break;
                case VisualOrientation.Left:
                    _drawPanelDocker.SetDock(_viewHeadingBar, ViewDockStyle.Left);
                    _viewHeadingBar.Orientation = VisualOrientation.Left;
                    _layoutBarDocker.Orientation = VisualOrientation.Right;
                    _layoutBarViewport.Orientation = VisualOrientation.Right;
                    break;
                case VisualOrientation.Right:
                    _drawPanelDocker.SetDock(_viewHeadingBar, ViewDockStyle.Right);
                    _viewHeadingBar.Orientation = VisualOrientation.Left;
                    _layoutBarDocker.Orientation = VisualOrientation.Right;
                    _layoutBarViewport.Orientation = VisualOrientation.Right;
                    break;
            }

            _layoutBar.Orientation = Navigator.Header.HeaderPositionBar;
            _layoutBarViewport.Orientation = Navigator.Header.HeaderPositionBar;
        }
        #endregion
    }
}
