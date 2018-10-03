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
    /// Implements the NavigatorMode.OutlookMini mode.
	/// </summary>
    internal class ViewBuilderOutlookMini : ViewBuilderOutlookBase
    {
        #region Instance Fields
        private ViewDrawNavOutlookMini _selectedButton;
        #endregion

        #region Public
        /// <summary>
        /// Gets the top level control of the source.
        /// </summary>
        public override Control SeparatorControl
        {
            get { return Navigator; }
        }

        /// <summary>
        /// Gets the box representing the minimum and maximum allowed splitter movement.
        /// </summary>
        public override Rectangle SeparatorMoveBox
        {
            get { return _viewLayout.ClientRectangle; }
        }

        /// <summary>
        /// Gets a value indicating if the mode is a tab strip style mode.
        /// </summary>
        public override bool IsTabStripMode
        {
            get { return true; }
        }

        /// <summary>
        /// Process a change in the selected page
        /// </summary>
        public override void SelectedPageChanged()
        {
            // If there is a selected page
            if (Navigator.SelectedPage != null)
            {
                // Remove any focus the mini button might have
                _selectedButton.HasFocus = false;
            }
            else
            {
                // If the navigator has focus then put it on the mini button
                _selectedButton.HasFocus = HasFocus;
            }

            // Update the selected button to show details of the newly selected page
            _selectedButton.Page = Navigator.SelectedPage;

            base.SelectedPageChanged();
        }

        /// <summary>
        /// Gets the appropriate popup page position for the current mode.
        /// </summary>
        /// <returns>Calculated PopupPagePosition</returns>
        public override PopupPagePosition GetPopupPagePosition()
        {
            if (Navigator.Outlook.Orientation == Orientation.Vertical)
                return PopupPagePosition.FarTop;
            else
                return PopupPagePosition.BelowNear;
        }

        /// <summary>
        /// Process a dialog key in a manner appropriate for the view.
        /// </summary>
        /// <param name="keyData">Key data.</param>
        /// <returns>True if the key eaten; otherwise false.</returns>
        public override bool ProcessDialogKey(Keys keyData)
        {
            // Find out which modifier keys are being pressed
            bool shift = ((keyData & Keys.Shift) == Keys.Shift);
            bool control = ((keyData & Keys.Control) == Keys.Control);

            // Extract just the key and not modifier keys
            Keys keyCode = (keyData & Keys.KeyCode);

            // There must be a selected page before any action can occur
            if (Navigator.SelectedPage != null)
            {
                switch (keyCode)
                {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.Right:
                        // Using a navigation key when the selected button has focus
                        if (HasFocus && _selectedButton.HasFocus)
                        {
                            // Shift focus to the currently selected page
                            if (Navigator.SelectedPage != null)
                            {
                                // Remove focus from the selected button
                                _selectedButton.HasFocus = false;

                                // We should have a stack view for the page
                                if (_pageStackLookup.ContainsKey(Navigator.SelectedPage))
                                {
                                    // Get the associated view element for the page
                                    ViewDrawNavCheckButtonBase checkButton = _pageStackLookup[Navigator.SelectedPage];

                                    // Focus is in the check button
                                    checkButton.HasFocus = true;
                                }

                                // Need to repaint to show the change
                                Navigator.PerformNeedPaint(true);
                                return true;
                            }
                        }
                        break;
                    case Keys.Space:
                    case Keys.Enter:
                        if (HasFocus)
                        {
                            // If the mini button does not have focus, give it focus
                            if (!_selectedButton.HasFocus)
                            {
                                // Remove focus from the selected button
                                _selectedButton.HasFocus = true;

                                // Shift focus from the currently selected page
                                if (Navigator.SelectedPage != null)
                                {
                                    // We should have a stack view for the page
                                    if (_pageStackLookup.ContainsKey(Navigator.SelectedPage))
                                    {
                                        // Get the associated view element for the page
                                        ViewDrawNavCheckButtonBase checkButton = _pageStackLookup[Navigator.SelectedPage];

                                        // Focus is not longer on the check button
                                        checkButton.HasFocus = false;
                                    }
                                }

                                // Need to repaint to show the change
                                Navigator.PerformNeedPaint(true);

                                return true;
                            }
                            else
                            {
                                // Mini button has focus, press the button
                                _selectedButton.PerformClick();
                            }
                        }
                        break;
                }
            }

            // Let the base class perform additional testing
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create the mode specific view hierarchy.
        /// </summary>
        /// <returns>View element to use as base of hierarchy.</returns>
        protected override ViewBase CreateView()
        {
            // Create the button used as the filler for the main area
            _selectedButton = new ViewDrawNavOutlookMini(Navigator, 
                                                         Navigator.SelectedPage, 
                                                         VisualOrientation.Left);

            // Set the correct initial orientation of the selected mini button
            _selectedButton.Orientation = (Navigator.Outlook.Orientation == Orientation.Vertical ?
                                           VisualOrientation.Left : VisualOrientation.Top);

            // Need to know when check button needs repainting
            _selectedButton.NeedPaint = NeedPaintDelegate;

            return base.CreateView();
        }

        /// <summary>
        /// Creates and returns the view element that laysout the main client area.
        /// </summary>
        /// <returns></returns>
        protected override ViewBase CreateMainLayout()
        {
            // Layout contains all the stack elements
            _viewLayout = new ViewLayoutOutlookMini(this);
            _viewLayout.PreferredSizeAll = true;
            return _viewLayout;
        }

        /// <summary>
        /// Gets the view element to use as the layout filler.
        /// </summary>
        /// <returns>ViewBase derived instance.</returns>
        protected override void SetLayoutFiller(ViewLayoutDocker viewLayout)
        {
            // Hide the selected page from showing up inside the layout
            viewLayout.Add(new ViewLayoutPageHide(Navigator), ViewDockStyle.Top);
            viewLayout.Add(_selectedButton, ViewDockStyle.Fill);
        }

        /// <summary>
        /// Add the check buttons for pages that should be on the overflow area.
        /// </summary>
        /// <param name="page">Reference to owning page.</param>
        /// <param name="checkOverflowOrient">Docking edge to dock against.</param>
        /// <param name="overflowInsertIndex">Index for inserting the new entry.</param>
        protected override void ReorderCheckButtonsOverflow(KryptonPage page,
                                                            VisualOrientation checkOverflowOrient,
                                                            ref int overflowInsertIndex)
        {
            // Do nothing, we never add the overflow buttons to the display
        }

        /// <summary>
        /// Discover if there are more buttons that can be moved from the overflow to the stack areas.
        /// </summary>
        /// <returns>True if more are available; otherwise false.</returns>
        protected override bool AreMoreButtons()
        {
            // Is there a visible overflow button that can be placed onto the stack?
            foreach (KryptonPage page in Navigator.Pages)
                if (page.LastVisibleSet && page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode))
                    return true;

            return false;
        }

        /// <summary>
        /// Gets the next overflow button to be moved to the stack area.
        /// </summary>
        /// <returns>Reference to button; otherwise false.</returns>
        protected override ViewDrawNavOutlookOverflow GetMoreOverflow()
        {
            // Find first visible page that is flagged for the overflow area
            foreach (KryptonPage page in Navigator.Pages)
                if (page.LastVisibleSet && page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode))
                    return (ViewDrawNavOutlookOverflow)_pageOverflowLookup[page];

            return null;
        }

        /// <summary>
        /// Updates the item that has the focus.
        /// </summary>
        protected override void UpdateSelectedPageFocus()
        {
            // If focus is coming into the control
            if (HasFocus)
            {
                // Set focus to the mini select button
                _selectedButton.HasFocus = true;

                // Need to repaint to show the change
                Navigator.PerformNeedPaint(true);
            }
            else
            {
                // If the focus is currently with the selected button
                if (_selectedButton.HasFocus)
                {
                    // Remove focus from the mini select button
                    _selectedButton.HasFocus = false;

                    // Need to repaint to show the change
                    Navigator.PerformNeedPaint(true);
                }
                else
                {
                    // If there is a page selected
                    if (Navigator.SelectedPage != null)
                    {
                        // We should have a stack view for the page
                        if (_pageStackLookup.ContainsKey(Navigator.SelectedPage))
                        {
                            // Get the associated view element for the page
                            ViewDrawNavCheckButtonBase checkButton = _pageStackLookup[Navigator.SelectedPage];

                            // Reflect focus is in the check button
                            checkButton.HasFocus = HasFocus;

                            // Need to repaint to show the change
                            Navigator.PerformNeedPaint(true);
                        }
                    }
                }
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
                    _selectedButton.Orientation = (Navigator.Outlook.Orientation == Orientation.Vertical ?
                                                   VisualOrientation.Left : VisualOrientation.Top);
                    break;
            }

            // Let base class continue with processing change
            base.OnViewBuilderPropertyChanged(sender, e);
        }
        #endregion
    }
}
