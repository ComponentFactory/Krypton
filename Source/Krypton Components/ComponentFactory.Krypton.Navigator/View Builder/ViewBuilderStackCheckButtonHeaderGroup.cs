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
    /// Implements the NavigatorMode.StackCheckButtonHeaderGroup mode.
	/// </summary>
    internal class ViewBuilderStackCheckButtonHeaderGroup : ViewBuilderStackCheckButtonBase
    {
        #region Instance Fields
        private ViewletHeaderGroup _headerGroup;
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
            ButtonSpec bs = base.ButtonSpecFromView(element);

            // Delegate lookup to the viewlet that has the button spec manager
            if (bs == null)
                bs = _headerGroup.ButtonSpecFromView(element);

            return bs;
        }

        /// <summary>
        /// Process a change in the selected page
        /// </summary>
        public override void SelectedPageChanged()
        {
            // Ask the header group to update the 
            _headerGroup.UpdateButtons();

            // Let base class perform common actions
            base.SelectedPageChanged();
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
            if (_headerGroup != null)
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

        #region Protected Overrides
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
        /// Create the mode specific view hierarchy.
        /// </summary>
        /// <returns>View element to use as base of hierarchy.</returns>
        protected override ViewBase CreateStackCheckButtonView()
        {
            // Let base class do common stuff first
            base.CreateStackCheckButtonView();

            // Layout contains all the stack elements
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

            // Create the header group and fill with the view layout
            _headerGroup = new ViewletHeaderGroup(Navigator, Redirector, NeedPaintDelegate);
            ViewBase newRoot = _headerGroup.Construct(_viewScrollViewport);

            // Put the old root as the filler inside stack elements
            _viewLayout.Add(_oldRoot, ViewDockStyle.Fill);

            return newRoot;
        }

        /// <summary>
        /// Allow operations to occur after main construct actions.
        /// </summary>
        protected override void PostConstruct()
        {
            // Ask the header group to finish the create phase
            _headerGroup.PostCreate();

            // Let base class perform standard actions
            base.PostConstruct();
        }

        /// <summary>
        /// Destruct the mode specific view hierarchy.
        /// </summary>
        protected override void DestructStackCheckButtonView()
        {
            // Put the child panel back into the navigator
            _viewScrollViewport.RevertParent(Navigator, Navigator.ChildPanel);

            // Destruct the header group viewlet
            _headerGroup.Destruct();

            // Let base class do common stuff
            base.DestructStackCheckButtonView();
        }
        #endregion
    }
}
