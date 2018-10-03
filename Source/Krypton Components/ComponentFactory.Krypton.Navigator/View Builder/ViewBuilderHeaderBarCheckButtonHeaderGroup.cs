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
    /// Implements the NavigatorMode.HeaderBarCheckButtonHeaderGroup mode.
	/// </summary>
    internal class ViewBuilderHeaderBarCheckButtonHeaderGroup : ViewBuilderHeaderBarCheckButtonBase
    {
        #region Instance Fields
        private ViewDrawDocker _viewGroup;
        private ViewletHeaderGroup _headerGroup;
        #endregion

        #region Public
        /// <summary>
        /// Gets a value indicating if the mode is a tab strip style mode.
        /// </summary>
        public override bool IsTabStripMode
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the ButtonSpec associated with the provided view element.
        /// </summary>
        /// <param name="element">Element to search against.</param>
        /// <returns>Reference to ButtonSpec; otherwise null.</returns>
        public override ButtonSpec ButtonSpecFromView(ViewBase element)
        {
            // Always check base class first
            ButtonSpec bs = base.ButtonSpecFromView(element);

            // Call onto the contained header group implementation
            if (bs == null)
                bs = _headerGroup.ButtonSpecFromView(element);
            
            return bs;
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
            
            // Construct the viewlet instance
            _headerGroup = new ViewletHeaderGroup(Navigator, Redirector, NeedPaintDelegate);

            // Create and initialize the standard header group view elements
            _viewGroup = _headerGroup.Construct(_oldRoot);

            // Add the extra bar header alongside the standard primary and secondary headers
            _viewGroup.Insert(0, _viewHeadingBar);
            _viewGroup.SetDock(_viewHeadingBar, ViewDockStyle.Top);

            // Define the new root for the view hieararchy
            _newRoot = _viewGroup;

            // Must call the base class to perform common actions
            base.CreateCheckItemView();
        }

        /// <summary>
        /// Perform post create tasks.
        /// </summary>
        protected override void PostCreate()
        {
            // Ask the header group to finish the create phase
            _headerGroup.PostCreate();

            // Let base class perform standard actions
            base.PostCreate();
        }

        /// <summary>
        /// Destruct the view hierarchy for this mode.
        /// </summary>
        protected override void DestructCheckItemView()
        {
            // Must remember to get the header group to destruct itself
            _headerGroup.Destruct();

            // Must call the base class to perform common actions
            base.DestructCheckItemView();

            _viewGroup.Dispose();
        }

        /// <summary>
        /// Create a manager for handling the button specifications.
        /// </summary>
        protected override void CreateButtonSpecManager()
        {
            // Do nothing, buttons are placed on the header groups and not the bar
        }

        /// <summary>
        /// Process the change in a property that might effect the view builder.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Property changed details.</param>
        protected override void OnViewBuilderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Let the header group process the property
            _headerGroup.ViewBuilderPropertyChanged(e);

            // Let the base process it as well
            base.OnViewBuilderPropertyChanged(sender, e);
        }

        /// <summary>
        /// Update the bar orientation.
        /// </summary>
        protected override void UpdateOrientation()
        {
            switch (Navigator.Header.HeaderPositionBar)
            {
                case VisualOrientation.Top:
                    _viewGroup.SetDock(_viewHeadingBar, ViewDockStyle.Top);
                    _viewHeadingBar.Orientation = VisualOrientation.Top;
                    _layoutBarDocker.Orientation = VisualOrientation.Top;
                    _layoutBarViewport.Orientation = VisualOrientation.Top;
                    break;
                case VisualOrientation.Bottom:
                    _viewGroup.SetDock(_viewHeadingBar, ViewDockStyle.Bottom);
                    _viewHeadingBar.Orientation = VisualOrientation.Top;
                    _layoutBarDocker.Orientation = VisualOrientation.Top;
                    _layoutBarViewport.Orientation = VisualOrientation.Top;
                    break;
                case VisualOrientation.Left:
                    _viewGroup.SetDock(_viewHeadingBar, ViewDockStyle.Left);
                    _viewHeadingBar.Orientation = VisualOrientation.Left;
                    _layoutBarDocker.Orientation = VisualOrientation.Right;
                    _layoutBarViewport.Orientation = VisualOrientation.Right;
                    break;
                case VisualOrientation.Right:
                    _viewGroup.SetDock(_viewHeadingBar, ViewDockStyle.Right);
                    _viewHeadingBar.Orientation = VisualOrientation.Left;
                    _layoutBarDocker.Orientation = VisualOrientation.Right;
                    _layoutBarViewport.Orientation = VisualOrientation.Right;
                    break;
            }

            _layoutBar.Orientation = Navigator.Header.HeaderPositionBar;
            _layoutBarViewport.Orientation = Navigator.Header.HeaderPositionBar;
        }
        #endregion

        #region Public Overrides
        /// <summary>
        /// Process a change in the selected page
        /// </summary>
        public override void SelectedPageChanged()
        {
            // Let base class perform common actions
            base.SelectedPageChanged();

            // Ask the header group to update the 
            _headerGroup.UpdateButtons();
        }

        /// <summary>
        /// Change has occured to the collection of pages.
        /// </summary>
        public override void PageCollectionChanged()
        {
            UpdateStatePalettes();
            _headerGroup.UpdateButtons();

            // Let base class do standard work
            base.PageCollectionChanged();
        }

        /// <summary>
        /// Process a change in the visible state for a page.
        /// </summary>
        /// <param name="page">Page that has changed visible state.</param>
        public override void PageVisibleStateChanged(KryptonPage page)
        {
            // If is possible the header group has not been created yet
            if (_headerGroup != null)
            {
                // Ensure buttons are recreated to reflect different previous/next visibility
                _headerGroup.UpdateButtons();
            }

            // Let base class do standard work
            base.PageVisibleStateChanged(page);
        }

        /// <summary>
        /// Process a change in the enabled state for a page.
        /// </summary>
        /// <param name="page">Page that has changed enabled state.</param>
        public override void PageEnabledStateChanged(KryptonPage page)
        {
            if (_headerGroup != null)
            {
                // If the page we are showing has changed
                if (page == Navigator.SelectedPage)
                {
                    // Update to use the correct enabled/disabled palette
                    UpdateStatePalettes();
                    _headerGroup.UpdateButtons();

                    // Need to repaint to show the change
                    Navigator.PerformNeedPaint(true);
                }
            }

            // Let base class do standard work
            base.PageEnabledStateChanged(page);
        }

        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public override void UpdateStatePalettes()
        {
            // Update palettes for the header group
            _headerGroup.UpdateStatePalettes();

            // Let base class do standard work
            base.UpdateStatePalettes();
        }

        /// <summary>
        /// Gets the screen coorindates for showing a context action menu.
        /// </summary>
        /// <returns>Point in screen coordinates.</returns>
        public override Point GetContextShowPoint()
        {
            // Ask the header group for screen point of context button
            return _headerGroup.GetContextShowPoint();
        }

        /// <summary>
        /// Is the provided over a part of the view that wants the mouse.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        /// <returns>True if the view wants the mouse position; otherwise false.</returns>
        public override bool DesignerGetHitTest(Point pt)
        {
            // Check if any of the button specs want the point
            if (_headerGroup.DesignerGetHitTest(pt))
                return true;

            // Let base class search individual stack items
            return base.DesignerGetHitTest(pt);
        }

        /// <summary>
        /// Calculate the enabled state of the next button based on the required action.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <returns>Enabled state of the button.</returns>
        public override ButtonEnabled NextActionEnabled(DirectionButtonAction action)
        {
            // Ask the header group to update the action
            action = _headerGroup.NextActionEnabled(action);

            // Let base class perform basic action calculations
            return base.NextActionEnabled(action);
        }

        /// <summary>
        /// Peform the next button action requested.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <param name="page">Selected page at time of action request.</param>
        public override void PerformNextAction(DirectionButtonAction action, KryptonPage page)
        {
            // Ask the header group to update the action
            action = _headerGroup.NextActionEnabled(action);

            // Let base class perform basic actions
            base.PerformNextAction(action, page);
        }

        /// <summary>
        /// Calculate the enabled state of the previous button based on the required action.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <returns>Enabled state of the button.</returns>
        public override ButtonEnabled PreviousActionEnabled(DirectionButtonAction action)
        {
            // Ask the header group to update the action
            action = _headerGroup.PreviousActionEnabled(action);

            // Let base class perform basic action calculations
            return base.PreviousActionEnabled(action);
        }

        /// <summary>
        /// Peform the previous button action requested.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <param name="page">Selected page at time of action request.</param>
        public override void PerformPreviousAction(DirectionButtonAction action, KryptonPage page)
        {
            // Ask the header group to update the action
            action = _headerGroup.PreviousActionEnabled(action);

            // Let base class perform basic actions
            base.PerformPreviousAction(action, page);
        }
        #endregion
    }
}
