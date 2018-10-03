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
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
    /// Implements the NavigatorMode.OutlookFull mode.
	/// </summary>
    internal class ViewBuilderOutlookFull : ViewBuilderOutlookBase
    {
        #region Instance Fields
        private ViewLayoutOutlookFull _viewOutlook;
        #endregion

        #region Public
        /// <summary>
        /// Gets the top level control of the source.
        /// </summary>
        public override Control SeparatorControl
        {
            get { return _viewOutlook.ViewControl.ChildControl; }
        }

        /// <summary>
        /// Gets the box representing the minimum and maximum allowed splitter movement.
        /// </summary>
        public override Rectangle SeparatorMoveBox
        {
            get { return _viewOutlook.ViewControl.ChildControl.ClientRectangle; }
        }

        /// <summary>
        /// Gets a value indicating if the mode is a tab strip style mode.
        /// </summary>
        public override bool IsTabStripMode
        {
            get { return false; }
        }

        /// <summary>
        /// Process a change in the selected page
        /// </summary>
        public override void SelectedPageChanged()
        {
            // If we have a selected page then we need to bring it into view
            if (Navigator.SelectedPage != null)
            {
                // We should have a view for representing the page
                if (_pageStackLookup.ContainsKey(Navigator.SelectedPage))
                {
                    // Get the check button used to represent the selected page
                    ViewDrawNavCheckButtonBase selected = _pageStackLookup[Navigator.SelectedPage];

                    // Can only bring page into view if actually visible
                    if (selected.Visible)
                    {
                        // Get the client rectangle of the check button
                        Rectangle buttonRect = selected.ClientRectangle;

                        // Ask the viewport to bring this rectangle into view
                        _viewOutlook.BringIntoView(buttonRect);
                    }
                }
            }

            // Let base class perform common actions
            base.SelectedPageChanged();
        }

        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public override void UpdateStatePalettes()
        {
            PaletteBorderEdge buttonEdge;

            // If whole navigator is disabled then all of view is disabled
            bool enabled = Navigator.Enabled;

            // If there is no selected page
            if (Navigator.SelectedPage == null)
            {
                // Then use the states defined in the navigator itself
                if (Navigator.Enabled)
                    buttonEdge = Navigator.StateNormal.BorderEdge;
                else
                    buttonEdge = Navigator.StateDisabled.BorderEdge;
            }
            else
            {
                // Use states defined in the selected page
                if (Navigator.SelectedPage.Enabled)
                    buttonEdge = Navigator.SelectedPage.StateNormal.BorderEdge;
                else
                    buttonEdge = Navigator.SelectedPage.StateDisabled.BorderEdge;
            }

            // Update the main view elements
            _viewOutlook.Enabled = enabled;
            _viewOutlook.SetPalettes(buttonEdge);

            // Let base class perform common actions
            base.UpdateStatePalettes();
        }

        /// <summary>
        /// Is the provided over a part of the view that wants the mouse.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        /// <returns>True if the view wants the mouse position; otherwise false.</returns>
        public override bool DesignerGetHitTest(Point pt)
        {
            if (base.DesignerGetHitTest(pt))
                return true;

            // Check that the point is into the stack scrolling viewport
            if (_viewOutlook.ClientRectangle.Contains(pt))
            {
                // Get the control that owns the view layout
                Control owningControl = _viewLayout.OwningControl;

                // Convert incoming point from navigator to owning control
                pt = owningControl.PointToClient(Navigator.PointToScreen(pt));

                // Check if any of the stack check buttons want the point
                foreach (ViewBase item in _viewLayout)
                    if (item is ViewDrawNavOutlookStack)
                        if (item.ClientRectangle.Contains(pt))
                            return true;

                // Check if any of the overflow check buttons want the point
                foreach (ViewBase item in _viewOverflowLayout)
                    if (item is ViewDrawNavOutlookOverflow)
                        if (item.ClientRectangle.Contains(pt))
                            return true;
            }

            return false;
        }

        /// <summary>
        /// Destruct the previously created view.
        /// </summary>
        public override void Destruct()
        {
            // Unhook from events
            _viewOutlook.AnimateStep -= new EventHandler(OnViewportAnimation);

            // Put the child panel back into the navigator
            _viewOutlook.RevertParent(Navigator, Navigator.ChildPanel);

            // Let base class perform common operations
            base.Destruct();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Creates and returns the view element that laysout the main client area.
        /// </summary>
        /// <returns></returns>
        protected override ViewBase CreateMainLayout()
        {
            // Layout contains all the stack elements
            _viewLayout = new ViewLayoutDocker();
            _viewLayout.PreferredSizeAll = true;

            // Cache the border edge palette to use
            PaletteBorderEdge buttonEdgePalette = (Navigator.Enabled ? Navigator.StateNormal.BorderEdge :
                                                                       Navigator.StateDisabled.BorderEdge);

            // Create the scrolling viewport and pass in the _viewLayout as the content to scroll
            _viewOutlook = new ViewLayoutOutlookFull(this, Navigator, _viewLayout, buttonEdgePalette, null, PaletteMetricPadding.None,
                                                     PaletteMetricInt.None, VisualOrientation.Top, RelativePositionAlign.Near, false,
                                                     (Navigator.Outlook.Orientation == Orientation.Vertical), NeedPaintDelegate);

            // Reparent the child panel that contains the actual pages, into the child control
            _viewOutlook.MakeParent(Navigator.ChildPanel);

            return _viewOutlook;
        }

        /// <summary>
        /// Create an overflow check button.
        /// </summary>
        /// <param name="page">Page to associate the check button with.</param>
        /// <param name="checkButtonOrient">Orientation of the check button.</param>
        /// <param name="dockFar">Docking position of the check button.</param>
        /// <returns></returns>
        protected override ViewDrawNavOutlookOverflow CreateOverflowItem(KryptonPage page, 
                                                                         VisualOrientation checkButtonOrient,
                                                                         ViewDockStyle dockFar)
        {
            // Let base class create the actual check button
            ViewDrawNavOutlookOverflow checkButton = base.CreateOverflowItem(page, checkButtonOrient, dockFar);

            // Add to the end of the overflow collection
            _viewOverflowLayout.Add(checkButton, dockFar);

            return checkButton;
        }

        /// <summary>
        /// Allow operations to occur after main construct actions.
        /// </summary>
        protected override void PostConstruct()
        {
            // Hook into the viewport animation steps
            _viewOutlook.AnimateStep += new EventHandler(OnViewportAnimation);
            base.PostConstruct();
        }

        /// <summary>
        /// Bring the specified page into view within the viewport.
        /// </summary>
        /// <param name="page">Page to bring into view.</param>
        protected override void BringPageIntoView(KryptonPage page)
        {
            // Remember the view for the requested page
            ViewDrawNavCheckButtonBase viewPage = null;

            // Make sure only the selected page is checked
            foreach (ViewDrawNavCheckButtonBase child in _pageStackLookup.Values)
            {
                // Should this check button be selected
                if (Navigator.SelectedPage == child.Page)
                {
                    viewPage = child;
                    break;
                }
            }

            // If we found a matching view
            if (viewPage != null)
            {
                // Ask the viewport to bring this rectangle of the view
                _viewOutlook.BringIntoView(viewPage.ClientRectangle);
            }
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
                case "OrientationOutlook":
                    _viewOutlook.VerticalViewport = (Navigator.Outlook.Orientation == Orientation.Vertical);
                    break;
            }

            // Let base class perform other actions
            base.OnViewBuilderPropertyChanged(sender, e);
        }
        #endregion

        #region Implementation
        private void OnViewportAnimation(object sender, EventArgs e)
        {
            Navigator.PerformNeedPaint(true);
        }
        #endregion
    }
}
