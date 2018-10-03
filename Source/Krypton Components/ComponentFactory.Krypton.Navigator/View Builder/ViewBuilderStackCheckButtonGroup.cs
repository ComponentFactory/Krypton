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
using System.Diagnostics;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
    /// Implements the NavigatorMode.StackCheckButtonGroup mode.
	/// </summary>
    internal class ViewBuilderStackCheckButtonGroup : ViewBuilderStackCheckButtonBase
    {
        #region Instance Fields
        private ViewDrawDocker _viewGroup;
        #endregion

        #region Public Overrides
        /// <summary>
        /// Gets the ButtonSpec associated with the provided view element.
        /// </summary>
        /// <param name="element">Element to search against.</param>
        /// <returns>Reference to ButtonSpec; otherwise null.</returns>
        public override ButtonSpec ButtonSpecFromView(ViewBase element)
        {
            // Check base class for page specific button specs
            return base.ButtonSpecFromView(element);
        }

        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public override void UpdateStatePalettes()
        {
            IPaletteBack back;
            IPaletteBorder border;

            // If whole navigator is disabled then all of view is disabled
            bool enabled = Navigator.Enabled;

            // If there is no selected page
            if (Navigator.SelectedPage == null)
            {
                // Then use the states defined in the navigator itself
                if (Navigator.Enabled)
                {
                    back = Navigator.StateNormal.HeaderGroup.Back;
                    border = Navigator.StateNormal.HeaderGroup.Border;
                }
                else
                {
                    back = Navigator.StateDisabled.HeaderGroup.Back;
                    border = Navigator.StateDisabled.HeaderGroup.Border;
                }
            }
            else
            {
                // Use states defined in the selected page
                if (Navigator.SelectedPage.Enabled)
                {
                    back = Navigator.SelectedPage.StateNormal.HeaderGroup.Back;
                    border = Navigator.SelectedPage.StateNormal.HeaderGroup.Border;
                }
                else
                {
                    back = Navigator.SelectedPage.StateDisabled.HeaderGroup.Back;
                    border = Navigator.SelectedPage.StateDisabled.HeaderGroup.Border;

                    // If page is disabled then all of view should look disabled
                    enabled = false;
                }
            }

            // Update the view canvas with correct palette sources
            _viewGroup.SetPalettes(back, border);
            _viewGroup.Enabled = enabled;

            // Let base class perform common actions
            base.UpdateStatePalettes();
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Create the mode specific view hierarchy.
        /// </summary>
        /// <returns>View element to use as base of hierarchy.</returns>
        protected override ViewBase CreateStackCheckButtonView()
        {
            // Let base class do common stuff first
            base.CreateStackCheckButtonView();

            // Add the layout docker inside the border of the group
            _viewLayout = new ViewLayoutDocker();

            // Cache the border edge palette to use
            PaletteBorderEdge buttonEdgePalette = (Navigator.Enabled ? Navigator.StateNormal.BorderEdge :
                                                                       Navigator.StateDisabled.BorderEdge);

            // Create the scrolling viewport and pass in the _viewLayout as the content to scroll
            _viewScrollViewport = new ViewLayoutScrollViewport(Navigator, _viewLayout, buttonEdgePalette, null, 
                                                               PaletteMetricPadding.None, PaletteMetricInt.None,
                                                               VisualOrientation.Top, RelativePositionAlign.Near,
                                                               Navigator.Stack.StackAnimation, 
                                                               (Navigator.Stack.StackOrientation == Orientation.Vertical),
                                                               NeedPaintDelegate);

            // Reparent the child panel that contains the actual pages, into the child control
            _viewScrollViewport.MakeParent(Navigator.ChildPanel);

            // Create the top level group view
            _viewGroup = new ViewDrawDocker(Navigator.StateNormal.HeaderGroup.Back,
                                            Navigator.StateNormal.HeaderGroup.Border);

            // Fill the group with the scrolling viewport
            _viewGroup.Add(_viewScrollViewport, ViewDockStyle.Fill);

            // Put the old root as the filler inside the group
            _viewLayout.Add(_oldRoot, ViewDockStyle.Fill);

            // Define the top level view to become the new root
            return _viewGroup;
        }

        /// <summary>
        /// Destruct the mode specific view hierarchy.
        /// </summary>
        protected override void DestructStackCheckButtonView()
        {
            // Put the child panel back into the navigator
            _viewScrollViewport.RevertParent(Navigator, Navigator.ChildPanel);

            // Dispose of resources
            _viewGroup.Dispose();
            _viewGroup.Clear();

            // Let base class do common stuff
            base.DestructStackCheckButtonView();
        }
        #endregion
    }
}
