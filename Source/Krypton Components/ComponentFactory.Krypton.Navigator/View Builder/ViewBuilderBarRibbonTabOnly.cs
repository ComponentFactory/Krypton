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
    /// Implements the NavigatorMode.BarRibbonTabOnly mode.
	/// </summary>
    internal class ViewBuilderBarRibbonTabOnly : ViewBuilderBarRibbonTabBase
    {
        #region Public
        /// <summary>
        /// Gets a value indicating if the mode is a tab strip style mode.
        /// </summary>
        public override bool IsTabStripMode
        {
            get { return true; }
        }

        /// <summary>
        /// User has used the keyboard to select the currently selected page.
        /// </summary>
        public override void KeyPressedPageView()
        {
            // If there is a currently selected page
            if (Navigator.SelectedPage != null)
            {
                // Grab the view for the page
                INavCheckItem checkItem = _pageLookup[Navigator.SelectedPage];

                // If the item also has the focus
                if (checkItem.HasFocus)
                {
                    // Then perform the click action for the button
                    checkItem.PerformClick();
                }
            }
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
                                           PaletteMetricInt.RibbonTabGap,
                                           Navigator.Bar.ItemSizing,
                                           Navigator.Bar.ItemAlignment,
                                           Navigator.Bar.BarMultiline,
                                           Navigator.Bar.ItemMinimumSize,
                                           Navigator.Bar.ItemMaximumSize,
                                           Navigator.Bar.BarMinimumHeight,
                                           Navigator.Bar.TabBorderStyle,
                                           true);

            // Create the scroll spacer that restricts display
            _layoutBarViewport = new ViewLayoutViewport(Navigator.StateCommon.Bar,
                                                        PaletteMetricPadding.BarPaddingTabs,
                                                        PaletteMetricInt.RibbonTabGap,
                                                        Navigator.Bar.BarOrientation,
                                                        Navigator.Bar.ItemAlignment,
                                                        Navigator.Bar.BarAnimation);
            _layoutBarViewport.Add(_layoutBar);

            // Create the button bar area docker
            _layoutBarDocker = new ViewLayoutDocker();
            _layoutBarDocker.Add(_layoutBarViewport, ViewDockStyle.Fill);

            // Add a separators for insetting items
            _layoutBarSeparatorFirst = new ViewLayoutSeparator(0);
            _layoutBarSeparatorLast = new ViewLayoutSeparator(0);
            _layoutBarDocker.Add(_layoutBarSeparatorFirst, ViewDockStyle.Left);
            _layoutBarDocker.Add(_layoutBarSeparatorLast, ViewDockStyle.Right);

            // Create the docker used to layout contents of main panel and fill with group
            _layoutPanelDocker = new ViewLayoutDocker();
            _layoutPanelDocker.Add(_layoutBarDocker, ViewDockStyle.Fill);
            _layoutPanelDocker.Add(new ViewLayoutPageHide(Navigator), ViewDockStyle.Top);

            // Create the top level panel and put a layout docker inside it
            _drawPanel = new ViewDrawPanel(Navigator.StateNormal.Back);
            _drawPanel.Add(_layoutPanelDocker);
            _newRoot = _drawPanel;

            // Must call the base class to perform common actions
            base.CreateCheckItemView();
        }
        #endregion
    }
}
