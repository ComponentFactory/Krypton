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
    /// Base class for implementation of various check button modes.
	/// </summary>
    internal abstract class ViewBuilderItemBase : ViewBuilderBase
    {
        #region Instance Fields
        protected PageToNavCheckItem _pageLookup;
        protected ButtonSpecManagerBase _buttonManager;
        protected ViewDrawPanel _drawPanel;
        protected ViewDrawCanvas _drawGroup;
        protected ViewLayoutDocker _layoutBarDocker;
        protected ViewLayoutBar _layoutBar;
        protected ViewLayoutViewport _layoutBarViewport;
        protected ViewBase _newRoot;
        protected ViewBase _oldRoot;
        private bool _hasFocus;
        private int _events;
        #endregion

		#region Public
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

			// Get the current root element
			_oldRoot = ViewManager.Root;

            // Create and initialize all objects
            CreateCheckItemView();
            CreateButtonSpecManager();
            CreateNavCheckItems();
            UpdateCheckItemStyle();
            UpdateOrientation();
            UpdateStatePalettes();
            PostCreate();

            // Force buttons to be recreated in the headers
            if (_buttonManager != null)
                _buttonManager.RecreateButtons();
            
            // Canvas becomes the new root
            ViewManager.Root = _newRoot;

			// Need to monitor changes in the enabled state
			Navigator.EnabledChanged += new EventHandler(OnNavigatorEnabledChanged);
            Navigator.RightToLeftChanged += new EventHandler(OnNavigatorRightToLeftChanged);
		}

        /// <summary>
        /// Destruct the previously created view.
        /// </summary>
        public override void Destruct()
        {
            DestructNavCheckItems();
            DestructButtonSpecManager();
            DestructCheckItemView();

            // Unhook from navigator enabled event
            Navigator.EnabledChanged -= new EventHandler(OnNavigatorEnabledChanged);
            Navigator.RightToLeftChanged -= new EventHandler(OnNavigatorRightToLeftChanged);

            // Put the old root back again
            ViewManager.Root = _oldRoot;

            // Let base class perform common operations
            base.Destruct();
        }

        /// <summary>
        /// Process a change in the selected page
        /// </summary>
        public override void SelectedPageChanged()
        {
            // Remember the newly selected page
            ViewBase selected = null;

            // Make sure only the selected page is checked
            foreach (ViewBase child in _layoutBar)
            {
                INavCheckItem checkItem = (INavCheckItem)child;

                // Should this check button be selected
                if (Navigator.SelectedPage == checkItem.Page)
                {
                    checkItem.HasFocus = _hasFocus;
                    checkItem.Checked = true;
                    selected = child;
                }
                else
                {
                    checkItem.Checked = false;
                    checkItem.HasFocus = false;
                }
            }

            // If we found a selected page
            if (selected != null)
            {
                // Make sure the layout is uptodate
                Navigator.CheckPerformLayout();

                // Get the client rectangle of the check button
                Rectangle buttonRect = selected.ClientRectangle;

                // Ask the viewport to bring this rectangle into view
                _layoutBarViewport.BringIntoView(buttonRect);
            }

            UpdateButtonsAndPalette();

            // Let base class do standard work
            base.SelectedPageChanged();
        }

        /// <summary>
        /// Change has occured to the collection of pages.
        /// </summary>
        public override void PageCollectionChanged()
        {
            // Let base class do standard work
            base.PageCollectionChanged();
        }

        /// <summary>
        /// Process a change in the visible state for a page.
        /// </summary>
        /// <param name="page">Page that has changed visible state.</param>
        public override void PageVisibleStateChanged(KryptonPage page)
        {
            // It is possible the lookup has not been created yet
            if (_pageLookup != null)
            {
                // Sometimes the page is noticed as changed in visibility before the
                // page has been processed and has a view added, so need to check lookup
                if (_pageLookup.ContainsKey(page))
                {
                    // Reflect new state in the check button
                    _pageLookup[page].View.Visible = page.LastVisibleSet;

                    // Need to repaint to show the change
                    Navigator.PerformNeedPaint(true);
                }
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
            if (_pageLookup != null)
            {
                // Sometimes the page is noticed as changed in enabled state before the
                // page has been processed and has a view added, so need to check lookup
                if (_pageLookup.ContainsKey(page))
                {
                    // Reflect new state in the check button
                    _pageLookup[page].View.Enabled = page.Enabled;

                    // Need to repaint to show the change
                    Navigator.PerformNeedPaint(true);
                }
            }

            // Let base class do standard work
            base.PageEnabledStateChanged(page);
        }

        /// <summary>
        /// Gets the KryptonPage associated with the provided view element.
        /// </summary>
        /// <param name="element">Element to search against.</param>
        /// <returns>Reference to KryptonPage; otherwise null.</returns>
        public override KryptonPage PageFromView(ViewBase element)
        {
            if (_pageLookup != null)
            {
                foreach (KeyValuePair<KryptonPage, INavCheckItem> pair in _pageLookup)
                    if (pair.Value.View == element)
                        return pair.Key;
            }

            return null;
        }

        /// <summary>
        /// Gets the ButtonSpec associated with the provided view element.
        /// </summary>
        /// <param name="element">Element to search against.</param>
        /// <returns>Reference to ButtonSpec; otherwise null.</returns>
        public override ButtonSpec ButtonSpecFromView(ViewBase element)
        {
            // Check the set of navgator level button specs
            ButtonSpec bs = (_buttonManager != null ? _buttonManager.ButtonSpecFromView(element) : null);

            // Check each page level button spec
            if ((bs == null) && (_pageLookup != null))
            {
                foreach (KeyValuePair<KryptonPage, INavCheckItem> pair in _pageLookup)
                {
                    bs = pair.Value.ButtonSpecFromView(element);
                    if (bs != null)
                        break;
                }
            }

            return bs;
        }

        /// <summary>
        /// Notification that a krypton page appearance property has changed.
        /// </summary>
        /// <param name="page">Page that has changed.</param>
        /// <param name="property">Name of property that has changed.</param>
        public override void PageAppearanceChanged(KryptonPage page, string property)
        {
            Debug.Assert(page != null);
            Debug.Assert(property != null);

            switch (property)
            {
                case "Text":
                case "TextTitle":
                case "TextDescription":
                case "ImageSmall":
                case "ImageMedium":
                case "ImageLarge":
                    Navigator.PerformNeedPaint(true);
                    break;
            }

            // Let base class do standard work
            base.PageAppearanceChanged(page, property);
        }

        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public override void UpdateStatePalettes()
        {
            PaletteNavigator paletteState;
            PaletteNavigatorRedirect paletteCommon;
            
            // If whole navigator is disabled then all views are disabled
            bool enabled = Navigator.Enabled;

            // If there is no selected page
            if (Navigator.SelectedPage == null)
            {
                // Then use the states defined in the navigator itself
                paletteCommon = Navigator.StateCommon;

                if (Navigator.Enabled)
                    paletteState = Navigator.StateNormal;
                else
                    paletteState = Navigator.StateDisabled;
            }
            else
            {
                // Use states defined in the selected page
                paletteCommon = Navigator.SelectedPage.StateCommon;

                if (Navigator.SelectedPage.Enabled)
                    paletteState = Navigator.SelectedPage.StateNormal;
                else
                {
                    paletteState = Navigator.SelectedPage.StateDisabled;

                    // If page is disabled then all of view should look disabled
                    enabled = false;
                }
            }

            // Only update the group if we have one
            if (_drawGroup != null)
            {
                _drawGroup.SetPalettes(paletteState.HeaderGroup.Back, 
                                       paletteState.HeaderGroup.Border);

                _drawGroup.Enabled = enabled;
            }

            // Only update the panel if we have one
            if (_drawPanel != null)
            {
                _drawPanel.SetPalettes(paletteState.Back);
                _drawPanel.Enabled = Navigator.Enabled;
            }

            // Update metrics from state common
            _layoutBar.SetMetrics(paletteCommon.Bar);
            _layoutBarViewport.SetMetrics(paletteCommon.Bar);

            if (_buttonManager != null)
                _buttonManager.SetDockerMetrics(_layoutBarDocker, paletteCommon.Bar);

            // Let base class perform common actions
            base.UpdateStatePalettes();
        }

        /// <summary>
        /// Gets the screen coorindates for showing a context action menu.
        /// </summary>
        /// <returns>Point in screen coordinates.</returns>
        public override Point GetContextShowPoint()
        {
            if (_buttonManager != null)
            {
                // Get the display rectange of the context button
                Rectangle rect = _buttonManager.GetButtonRectangle(Navigator.Button.ContextButton);

                // We want the context menu to show just below the button
                Point pt = new Point(rect.Left, rect.Bottom + 3);

                // Convert from control coordinates to screen coordinates
                return Navigator.PointToScreen(pt);
            }
            else
                return base.GetContextShowPoint();
        }

        /// <summary>
        /// Is the provided over a part of the view that wants the mouse.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        /// <returns>True if the view wants the mouse position; otherwise false.</returns>
        public override bool DesignerGetHitTest(Point pt)
        {
            // Check if any of the button specs want the point
            if ((_buttonManager != null) && _buttonManager.DesignerGetHitTest(pt))
                return true;

            // Check that the point is into the bar viewport
            if (_layoutBarViewport.ClientRectangle.Contains(pt))
            {
                // Check if any of the bar items wants the point
                foreach (ViewBase item in _layoutBar)
                    if (item.ClientRectangle.Contains(pt))
                        return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the appropriate display location for the button.
        /// </summary>
        /// <param name="buttonSpec">ButtonSpec instance.</param>
        /// <returns>HeaderLocation value.</returns>
        public override HeaderLocation GetFixedButtonLocation(ButtonSpecNavFixed buttonSpec)
        {
            // This mode only has a single location, the button bar
            return HeaderLocation.PrimaryHeader;
        }

        /// <summary>
        /// Calculate the enabled state of the next button based on the required action.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <returns>Enabled state of the button.</returns>
        public override ButtonEnabled NextActionEnabled(DirectionButtonAction action)
        {
            // Our mode appropriate action is always to move the bar positoin
            if (action == DirectionButtonAction.ModeAppropriateAction)
                action = DirectionButtonAction.MoveBar;

            // Only we know how to calculate the moving bar action
            if (action == DirectionButtonAction.MoveBar)
                return (_layoutBarViewport.CanScrollNext ? ButtonEnabled.True : ButtonEnabled.False);
            else
                return base.NextActionEnabled(action);
        }

        /// <summary>
        /// Peform the next button action requested.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <param name="page">Selected page at time of action request.</param>
        public override void PerformNextAction(DirectionButtonAction action, KryptonPage page)
        {
            // Our mode appropriate action is always to move the bar positoin
            if (action == DirectionButtonAction.ModeAppropriateAction)
                action = DirectionButtonAction.MoveBar;

            // Only we know how to actually move the bar
            if (action == DirectionButtonAction.MoveBar)
            {
                // Tell the viewport to shift to next area
                _layoutBarViewport.MoveNext();

                if (_buttonManager != null)
                    _buttonManager.RecreateButtons();

                // Need to layout and paint to effect change
                Navigator.PerformNeedPaint(true);
            }
            else
                base.PerformNextAction(action, page);
        }

        /// <summary>
        /// Calculate the enabled state of the previous button based on the required action.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <returns>Enabled state of the button.</returns>
        public override ButtonEnabled PreviousActionEnabled(DirectionButtonAction action)
        {
            // Our mode appropriate action is always to move the bar positoin
            if (action == DirectionButtonAction.ModeAppropriateAction)
                action = DirectionButtonAction.MoveBar;

            // Only we know how to calculate the moving bar action
            if (action == DirectionButtonAction.MoveBar)
                return (_layoutBarViewport.CanScrollPrevious ? ButtonEnabled.True : ButtonEnabled.False);
            else
                return base.PreviousActionEnabled(action);
        }

        /// <summary>
        /// Peform the previous button action requested.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <param name="page">Selected page at time of action request.</param>
        public override void PerformPreviousAction(DirectionButtonAction action, KryptonPage page)
        {
            // Our mode appropriate action is always to move the bar positoin
            if (action == DirectionButtonAction.ModeAppropriateAction)
                action = DirectionButtonAction.MoveBar;

            // Only we know how to actually move the bar
            if (action == DirectionButtonAction.MoveBar)
            {
                // Tell the viewport to shift to previous area
                _layoutBarViewport.MovePrevious();

                if (_buttonManager != null)
                    _buttonManager.RecreateButtons();

                // Need to layout and paint to effect change
                Navigator.PerformNeedPaint(true);
            }
            else
                base.PerformPreviousAction(action, page);
        }

        /// <summary>
        /// Perform post layout operations.
        /// </summary>
        public override void PostLayout()
        {
            RefreshButtons();
        }

        /// <summary>
        /// Gets a value indicating if the view can accept the focus.
        /// </summary>
        public override bool CanFocus
        {
            get { return true; }
        }

        /// <summary>
        /// Occurs when the navigator takes the focus.
        /// </summary>
        public override void GotFocus() 
        {
            // The navigator has the focus
            _hasFocus = true;

            // Make sure the selected page displays with focus indication
            UpdateSelectedPageFocus();

            // If there is a selected page
            if (Navigator.SelectedPage != null)
                BringPageIntoView(Navigator.SelectedPage);
        }

        /// <summary>
        /// Occurs when the navigator loses the focus.
        /// </summary>
        public override void LostFocus() 
        {
            // Navigator no longer has the focus
            _hasFocus = false;

            // Remove focus indication from the selected page
            UpdateSelectedPageFocus();
        }

        /// <summary>
        /// Should this element cause the navigator to gain the focus.
        /// </summary>
        /// <param name="element">Element that is being activated.</param>
        /// <returns>True to give navigator the focus; otherwise false.</returns>
        public override bool GiveNavigatorFocus(ViewBase element)
        {
            // Only need to take the focus if we do not already have it
            if (!_hasFocus)
            {
                // Keep searching up the element tree for the type of interest
                while (element != null)
                {
                    // If pressing on a check button then we take the focus
                    if ((element is INavCheckItem) && Navigator.AllowTabSelect)
                        return true;

                    // Move up a level
                    element = element.Parent;
                }
            }

            return false;
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
                // Check for keys without modifiers
                switch (keyCode)
                {
                    case Keys.Tab:
                        // Using a CONTROL tab means selecting another page
                        if (control)
                        {
                            // Are we allowed to perform a Ctrl+Tab change in selection
                            CtrlTabCancelEventArgs ce = new CtrlTabCancelEventArgs(!shift);
                            Navigator.OnCtrlTabStart(ce);

                            if (!ce.Cancel)
                            {
                                bool changed;
                                if (!shift)
                                    changed = SelectNextPage(Navigator.SelectedPage, true, true);
                                else
                                    changed = SelectPreviousPage(Navigator.SelectedPage, true, true);
                            }
                        }
                        return true;
                }

                // Check for keys with modifiers
                switch(keyData)
                {
                    case Keys.Home:
                        if (_hasFocus)
                        {
                            SelectNextPage(null, false, false);
                            return true;
                        }
                        break;
                    case Keys.End:
                        if (_hasFocus)
                        {
                            SelectPreviousPage(null, false, false);
                            return true;
                        }
                        break;
                    case Keys.Up:
                        if (_hasFocus)
                        {
                            // Can only use Up arrow when on a vertical bar
                            if (!BarHorizontal)
                                SelectPreviousPage(Navigator.SelectedPage, false, false);
                            return true;
                        }
                        break;
                    case Keys.Down:
                        if (_hasFocus)
                        {
                            // Can only use Down arrow when on a vertical bar
                            if (!BarHorizontal)
                                SelectNextPage(Navigator.SelectedPage, false, false);
                            return true;
                        }
                        break;
                    case Keys.Right:
                        if (_hasFocus)
                        {
                            // Can only use Right arrow when on a horizontal bar
                            if (BarHorizontal)
                            {
                                // Reverse the direction if working RightToLeft
                                if (Navigator.RightToLeft != RightToLeft.Yes)
                                    SelectNextPage(Navigator.SelectedPage, false, false);
                                else
                                    SelectPreviousPage(Navigator.SelectedPage, false, false);
                            }
                            return true;
                        }
                        break;
                    case Keys.Left:
                        if (_hasFocus)
                        {
                            // Can only use Right arrow when on a horizontal bar
                            if (BarHorizontal)
                            {
                                // Reverse the direction if working RightToLeft
                                if (Navigator.RightToLeft != RightToLeft.Yes)
                                    SelectPreviousPage(Navigator.SelectedPage, false, false);
                                else
                                    SelectNextPage(Navigator.SelectedPage, false, false);
                            }
                            return true;
                        }
                        break;
                    case Keys.Space:
                    case Keys.Enter:
                        if (_hasFocus)
                            KeyPressedPageView();
                        break;
                }
            }

            // Last of all check for a shortcut to the action buttons
            return CheckActionShortcuts(keyData);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create the view hierarchy for this view mode.
        /// </summary>
        protected virtual void CreateCheckItemView()
        {
            // Hook into the events from created view elements
            _layoutBarViewport.AnimateStep += new EventHandler(OnViewportAnimation);
        }

        /// <summary>
        /// Create a manager for handling the button specifications.
        /// </summary>
        protected virtual void CreateButtonSpecManager()
        {
            // Create button specification collection manager
            _buttonManager = new ButtonSpecNavManagerLayoutBar(Navigator, Redirector, Navigator.Button.ButtonSpecs, Navigator.FixedSpecs,
                                                               new ViewLayoutDocker[] { _layoutBarDocker },
                                                               new IPaletteMetric[] { Navigator.StateCommon.Bar },
                                                               new PaletteMetricInt[] { PaletteMetricInt.BarButtonEdgeInside },
                                                               new PaletteMetricInt[] { PaletteMetricInt.BarButtonEdgeOutside },
                                                               new PaletteMetricPadding[] { PaletteMetricPadding.BarButtonPadding },
                                                               new GetToolStripRenderer(Navigator.CreateToolStripRenderer),
                                                               NeedPaintDelegate);

            // Hook up the tooltip manager so that tooltips can be generated
            _buttonManager.ToolTipManager = Navigator.ToolTipManager;
        }

        /// <summary>
        /// Perform post create tasks.
        /// </summary>
        protected virtual void PostCreate() { }

        /// <summary>
        /// Destruct the button manager instance.
        /// </summary>
        protected virtual void DestructButtonSpecManager()
        {
            if (_buttonManager != null)
            {
                // Unhook from the paint event
                _buttonManager.NeedPaint = null;

                // Cleanup the button manager events and processing
                _buttonManager.Destruct();
            }
        }

        /// <summary>
        /// Destruct the view hierarchy for this mode.
        /// </summary>
        protected virtual void DestructCheckItemView()
        {
            // Unhook from events
            _layoutBarViewport.AnimateStep -= new EventHandler(OnViewportAnimation);

            // Remove the old root from the canvas
            if (_drawPanel != null)
                _drawPanel.Clear();
        }

        /// <summary>
        /// Create a new check item with initial settings.
        /// </summary>
        /// <param name="page">Page for which the check button is to be created.</param>
        /// <param name="orientation">Initial orientation of the check button.</param>
        protected virtual INavCheckItem CreateCheckItem(KryptonPage page,
                                                        VisualOrientation orientation)
        {
            // Create a check button view element
            ViewDrawNavCheckButtonBar checkButton = new ViewDrawNavCheckButtonBar(Navigator, page, orientation);

            // Convert the button orientation to the appropriate visual orientations
            VisualOrientation orientBackBorder = ConvertButtonBorderBackOrientation();
            VisualOrientation orientContent = ConvertButtonContentOrientation();

            // Set the correct initial orientation of the button
            checkButton.SetOrientation(orientBackBorder, orientContent);

            return checkButton;
        }

        /// <summary>
        /// Gets access to the collection of pages.
        /// </summary>
        protected PageToNavCheckItem PageLookup
        {
            get { return _pageLookup; }
        }

        /// <summary>
        /// Update the bar orientation.
        /// </summary>
        protected abstract void UpdateOrientation();

        /// <summary>
        /// Update the orientation of the individual items.
        /// </summary>
        protected void UpdateItemOrientation()
        {
            // Convert the button orientation to the appropriate visual orientations
            VisualOrientation orientBackBorder = ConvertButtonBorderBackOrientation();
            VisualOrientation orientContent = ConvertButtonContentOrientation();

            // Apply to each of the buttons
            foreach (ViewBase child in _layoutBar)
            {
                INavCheckItem checkItem = (INavCheckItem)child;
                checkItem.SetOrientation(orientBackBorder, orientContent);
            }

            // Tell the layout bar about item orientation
            _layoutBar.ItemOrientation = orientContent;
        }

        /// <summary>
        /// Gets the visual orientation of the check butttons border and background.
        /// </summary>
        /// <returns>Visual orientation.</returns>
        protected virtual VisualOrientation ConvertButtonBorderBackOrientation()
        {
            return ResolveButtonContentOrientation(Navigator.Bar.BarOrientation);
        }

        /// <summary>
        /// Gets the visual orientation of the check butttons content.
        /// </summary>
        /// <returns>Visual orientation.</returns>
        protected virtual VisualOrientation ConvertButtonContentOrientation()
        {
            return ResolveButtonContentOrientation(Navigator.Bar.BarOrientation);
        }

        /// <summary>
        /// Convert the item orientation using the requested parent orientation.
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns>Visual orientation.</returns>
        protected VisualOrientation ResolveButtonContentOrientation(VisualOrientation orientation)
        {
            switch (Navigator.Bar.ItemOrientation)
            {
                case ButtonOrientation.Auto:
                    // Depends on the bar orientation
                    switch (orientation)
                    {
                        case VisualOrientation.Top:
                        case VisualOrientation.Bottom:
                            return VisualOrientation.Top;
                        case VisualOrientation.Left:
                            if (CommonHelper.GetRightToLeftLayout(Navigator) &&
                                (Navigator.RightToLeft == RightToLeft.Yes))
                                return VisualOrientation.Right;
                            else
                                return VisualOrientation.Left;
                        case VisualOrientation.Right:
                            if (CommonHelper.GetRightToLeftLayout(Navigator) &&
                                (Navigator.RightToLeft == RightToLeft.Yes))
                                return VisualOrientation.Left;
                            else
                                return VisualOrientation.Right;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            return VisualOrientation.Top;
                    }
                case ButtonOrientation.FixedTop:
                    return VisualOrientation.Top;
                case ButtonOrientation.FixedBottom:
                    return VisualOrientation.Bottom;
                case ButtonOrientation.FixedLeft:
                    return VisualOrientation.Left;
                case ButtonOrientation.FixedRight:
                    return VisualOrientation.Right;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return VisualOrientation.Top;
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
                case "BarAnimation":
                    _layoutBarViewport.AnimateChange = Navigator.Bar.BarAnimation;
                    break;
                case "BarMinimumHeight":
                    _layoutBar.BarMinimumHeight = Navigator.Bar.BarMinimumHeight;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "BarMultiline":
                    _layoutBar.BarMultiline = Navigator.Bar.BarMultiline;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "ItemAlignment":
                    _layoutBar.ItemAlignment = Navigator.Bar.ItemAlignment;
                    _layoutBarViewport.Alignment = Navigator.Bar.ItemAlignment;
                    if (_buttonManager != null)
                        _buttonManager.RecreateButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "ItemMinimumSize":
                    _layoutBar.ItemMinimumSize = Navigator.Bar.ItemMinimumSize;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "ItemMaximumSize":
                    _layoutBar.ItemMaximumSize = Navigator.Bar.ItemMaximumSize;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "ItemOrientationBar":
                    UpdateItemOrientation();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "ItemSizing":
                    _layoutBar.BarItemSizing = Navigator.Bar.ItemSizing;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "PreviousButtonDisplay":
                case "PreviousButtonAction":
                case "NextButtonDisplay":
                case "NextButtonAction":
                case "ContextButtonDisplay":
                case "CloseButtonDisplay":
                case "ButtonDisplayLogic":
                    if (_buttonManager != null)
                        _buttonManager.RecreateButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "CheckButtonStyleBar":
                    UpdateCheckItemStyle();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "PageButtonSpecInset":
                case "PageButtonSpecPadding":
                    Navigator.PerformNeedPaint(true);
                    break;
                default:
                    // We do not recognise the property, let base process it
                    base.OnViewBuilderPropertyChanged(sender, e);
                    break;
            }
        }
        #endregion

		#region Implementation
        private void CreateNavCheckItems()
        {
            // Maintain lookup between page and check button that represents it
            _pageLookup = new PageToNavCheckItem();

            // Convert the button orientation to the appropriate visual orientation
            VisualOrientation orientation = ConvertButtonBorderBackOrientation();

            // Create a check button to represent each krypton page
            foreach (KryptonPage page in Navigator.Pages)
            {
                // Create the draw view element for the check item
                INavCheckItem checkItem = CreateCheckItem(page, orientation);

                // Provide the drag rectangle when requested for this button
                checkItem.ButtonDragRectangle += new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkItem.ButtonDragOffset += new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);

                // Need to know when check button needs repainting
                checkItem.NeedPaint = NeedPaintDelegate;

                // Set the initial state
                checkItem.View.Visible = page.LastVisibleSet;
                checkItem.View.Enabled = page.Enabled;
                checkItem.Checked = (Navigator.SelectedPage == page);

                // Add to lookup dictionary
                _pageLookup.Add(page, checkItem);

                // Add to the bar layout element for positioning
                _layoutBar.Add(checkItem.View);
            }

            // Need to monitor changes in the page collection to reflect in layout bar
            Navigator.Pages.Inserted += new TypedHandler<KryptonPage>(OnItemPageInserted);
            Navigator.Pages.Removed += new TypedHandler<KryptonPage>(OnItemPageRemoved);
            Navigator.Pages.Cleared += new EventHandler(OnItemPagesCleared);
            _events++;
        }

        private void DestructNavCheckItems()
        {
            // Unhook from monitoring the pages collection
            _events--;
            Navigator.Pages.Inserted -= new TypedHandler<KryptonPage>(OnItemPageInserted);
            Navigator.Pages.Removed -= new TypedHandler<KryptonPage>(OnItemPageRemoved);
            Navigator.Pages.Cleared -= new EventHandler(OnItemPagesCleared);

            // Must clean up buttons in way that removes all event hooks
            DestructCheckButtons();
        }

        private void UpdateCheckItemStyle()
        {
            Navigator.StateCommon.CheckButton.SetStyles(Navigator.Bar.CheckButtonStyle);
            Navigator.OverrideFocus.CheckButton.SetStyles(Navigator.Bar.CheckButtonStyle);

            // Update each individual button with the new style for remapping page level button specs
            if (PageLookup != null)
                foreach (KeyValuePair<KryptonPage, INavCheckItem> pair in PageLookup)
                {
                    if (pair.Value is ViewDrawNavCheckButtonBar)
                    {
                        ViewDrawNavCheckButtonBar pageButton = (ViewDrawNavCheckButtonBar)pair.Value;
                        if (pageButton.ButtonSpecManager != null)
                            pageButton.ButtonSpecManager.SetRemapTarget(Navigator.Bar.TabStyle);
                    }
                    else if (pair.Value is ViewDrawNavRibbonTab)
                    {
                        ViewDrawNavRibbonTab pageRibbon = (ViewDrawNavRibbonTab)pair.Value;
                        if (pageRibbon.ButtonSpecManager != null)
                            pageRibbon.ButtonSpecManager.SetRemapTarget(Navigator.Bar.TabStyle);
                    }
                }
        }

        private void UpdateButtonsAndPalette()
        {
            // Update the use the correct enabled/disabled palette
            UpdateStatePalettes();

            // Ensure buttons are recreated to reflect different page
            if (_buttonManager != null)
                _buttonManager.RecreateButtons();
        }

        private void UpdateSelectedPageFocus()
        {
            // If there is a page selected
            if (Navigator.SelectedPage != null)
            {
                // We should have a view for representing the page
                if (_pageLookup.ContainsKey(Navigator.SelectedPage))
                {
                    // Get the associated view element for the page
                    INavCheckItem checkItem = _pageLookup[Navigator.SelectedPage];

                    // Reflect focus is in the check button
                    checkItem.HasFocus = _hasFocus;

                    // Need to repaint to show the change
                    Navigator.PerformNeedPaint(true);
                }
            }
        }

        private void DestructCheckButtons()
        {
            // Must tell each check button it is no longer required
            foreach (ViewBase child in _layoutBar)
            {
                INavCheckItem checkItem = (INavCheckItem)child;

                // Must unhook from events
                checkItem.ButtonDragRectangle -= new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkItem.ButtonDragOffset -= new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);
                checkItem.NeedPaint = null;
            
                // Dispose of element gracefully
                child.Dispose();
            }

            // Remove all check button view elements
            _layoutBar.Clear();

            // Remove all associations from the lookup dictionary
            _pageLookup.Clear();
        }

        private void RefreshButtons()
        {
            if (_buttonManager != null)
            {
                // Ensure the buttons have latest enabled/checked etc state
                if (_buttonManager.RefreshButtons())
                {
                    // Need another layout cycle to reflect the change in button state
                    Navigator.InternalForceViewLayout();
                }
            }
        }

        private void BringPageIntoView(KryptonPage page)
        {
            // Remember the view for the requested page
            ViewBase viewPage = null;

            // Make sure only the selected page is checked
            foreach (ViewBase child in _layoutBar)
            {
                INavCheckItem checkItem = (INavCheckItem)child;

                // Should this check button be selected
                if (Navigator.SelectedPage == checkItem.Page)
                {
                    viewPage = child;
                    break;
                }
            }

            // If we found a matching view
            if (viewPage != null)
            {
                // Ask the viewport to bring this rectangle of the view
                _layoutBarViewport.BringIntoView(viewPage.ClientRectangle);
            }
        }

        private bool BarHorizontal
        {
            get
            {
                return (Navigator.Bar.BarOrientation == VisualOrientation.Top) ||
                       (Navigator.Bar.BarOrientation == VisualOrientation.Bottom);
            }
        }

        private void OnItemPagesCleared(object sender, EventArgs e)
        {
            if (!Navigator.IsDisposed && (_events > 0))
            {
                // Pull down all the check button view elements
                DestructCheckButtons();

                // Need to repaint to show the change
                Navigator.PerformNeedPaint(true);
            }
        }

        private void OnItemPageRemoved(object sender, TypedCollectionEventArgs<KryptonPage> e)
        {
            if (!Navigator.IsDisposed && (_events > 0))
            {
                // Get the associated check button view element
                INavCheckItem checkItem = _pageLookup[e.Item];

                // Must unhook from events
                checkItem.ButtonDragRectangle -= new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkItem.ButtonDragOffset -= new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);
                checkItem.NeedPaint = null;

                // Tell the checkbutton it is not longer required
                checkItem.View.Dispose();

                // Remove the check button from the layout bar
                _layoutBar.Remove(checkItem.View);

                // Remove association from the lookup dictionary
                _pageLookup.Remove(e.Item);

                // Need to repaint to show the change
                Navigator.PerformNeedPaint(true);
            }
        }

        private void OnItemPageInserted(object sender, TypedCollectionEventArgs<KryptonPage> e)
        {
            if (!Navigator.IsDisposed && (_events > 0))
            {
                // Create the draw view element for the check button and provide page it represents
                INavCheckItem checkItem = CreateCheckItem(e.Item, ConvertButtonBorderBackOrientation());

                // Provide the drag rectangle when requested for this button
                checkItem.ButtonDragRectangle += new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkItem.ButtonDragOffset += new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);

                // Need to know when check button needs repainting
                checkItem.NeedPaint = NeedPaintDelegate;

                // Set the initial state
                checkItem.View.Visible = e.Item.LastVisibleSet;
                checkItem.View.Enabled = e.Item.Enabled;
                checkItem.Checked = (Navigator.SelectedPage == e.Item);

                // Add to lookup dictionary
                _pageLookup.Add(e.Item, checkItem);

                // Add to the bar layout element for positioning
                _layoutBar.Insert(e.Index, checkItem.View);

                // Need to repaint to show the change
                Navigator.PerformNeedPaint(true);
            }
        }
        
        private void OnNavigatorEnabledChanged(object sender, EventArgs e)
		{
            UpdateStatePalettes();
		}

        private void OnNavigatorRightToLeftChanged(object sender, EventArgs e)
        {
            UpdateItemOrientation();
        }

        private void OnViewportAnimation(object sender, EventArgs e)
        {
            Navigator.PerformNeedPaint(true);
        }

        private void OnCheckButtonDragRect(object sender, ButtonDragRectangleEventArgs e)
        {
            // Cast incoming reference to the actual button view
            INavCheckItem reorderItem = (INavCheckItem)sender;

            e.PreDragOffset = (Navigator.AllowPageReorder && reorderItem.Page.AreFlagsSet(KryptonPageFlags.AllowPageReorder));
            Rectangle dragRect = Rectangle.Union(e.DragRect, _layoutBarViewport.ClientRectangle);
            dragRect.Inflate(new Size(10, 10));
            e.DragRect = dragRect;
        }

        private void OnCheckButtonDragOffset(object sender, ButtonDragOffsetEventArgs e)
        {
            // Cast incoming reference to the actual button view
            INavCheckItem reorderItem = (INavCheckItem)sender;
            ViewBase reorderView = reorderItem.View;

            // Scan the collection of children
            bool foundReorderView = false;
            VisualOrientation orientation = ConvertButtonBorderBackOrientation();
            foreach (KryptonPage page in Navigator.Pages)
            {
                // If the mouse is over this button
                ViewBase childView = (ViewBase)_pageLookup[page];
                if (childView.ClientRectangle.Contains(e.PointOffset))
                {
                    // Only interested if mouse over a different check button
                    if (childView != reorderView)
                    {
                        Rectangle childRect = childView.ClientRectangle;

                        if (foundReorderView)
                        {
                            if ((orientation == VisualOrientation.Left) || (orientation == VisualOrientation.Right))
                            {
                                int shrink = childRect.Height - Math.Min(childRect.Height, reorderView.ClientHeight);
                                childRect.Y += shrink;
                                childRect.Height -= shrink;
                            }
                            else
                            {
                                int shrink = childRect.Width - Math.Min(childRect.Width, reorderView.ClientWidth);
                                childRect.X += shrink;
                                childRect.Width -= shrink;
                            }

                            // Ensure that when we are placed in the 'after' position the mouse is still over
                            // ourself as the moved button. Otherwise we just end up toggling back and forth.
                            if (childRect.Contains(e.PointOffset))
                            {
                                KryptonPage movePage = PageFromView(reorderView);
                                KryptonPage targetPage = PageFromView(childView);
                                PageReorderEventArgs reorder = new PageReorderEventArgs(movePage, targetPage, false);

                                // Give event handlers a chance to cancel this reorder
                                Navigator.OnBeforePageReorder(reorder);
                                if (!reorder.Cancel)
                                {
                                    Navigator.Pages.MoveAfter(movePage, targetPage);
                                    RecreateView();
                                    Navigator.PerformLayout();
                                    Navigator.Refresh();
                                    Navigator.OnTabMoved(new TabMovedEventArgs(movePage, Navigator.Pages.IndexOf(movePage)));
                                }
                            }
                        }
                        else
                        {
                            if ((orientation == VisualOrientation.Left) || (orientation == VisualOrientation.Right))
                                childRect.Height = Math.Min(childRect.Height, reorderView.ClientHeight);
                            else
                                childRect.Width = Math.Min(childRect.Width, reorderView.ClientWidth);

                            // Ensure that when we are placed in the 'before' position the mouse is still over
                            // ourself as the moved button. Otherwise we just end up toggling back and forth.
                            if (childRect.Contains(e.PointOffset))
                            {
                                KryptonPage movePage = PageFromView(reorderView);
                                KryptonPage targetPage = PageFromView(childView);
                                PageReorderEventArgs reorder = new PageReorderEventArgs(movePage, targetPage, true);

                                // Give event handlers a chance to cancel this reorder
                                Navigator.OnBeforePageReorder(reorder);
                                if (!reorder.Cancel)
                                {
                                    Navigator.Pages.MoveBefore(movePage, PageFromView(childView));
                                    RecreateView();
                                    Navigator.PerformLayout();
                                    Navigator.Refresh();
                                    Navigator.OnTabMoved(new TabMovedEventArgs(movePage, Navigator.Pages.IndexOf(movePage)));
                                }
                            }
                        }

                        break;
                    }
                }

                foundReorderView = (childView == reorderView);
            }
        }

        private void RecreateView()
        {
            // Remove existing page buttons
            _layoutBar.Clear();

            // Reorder the layout bar children to match the pages ordering
            foreach (KryptonPage page in Navigator.Pages)
            {
                INavCheckItem checkItem = _pageLookup[page];
                _layoutBar.Add(checkItem.View);
            }
        }
        #endregion
	}
}
