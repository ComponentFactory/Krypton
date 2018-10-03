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
    /// Base class for implementation of 'Stack - CheckButton' modes.
	/// </summary>
    internal abstract class ViewBuilderStackCheckButtonBase : ViewBuilderBase
	{
        #region Type Definitons
        protected class PageToButtonEdge : Dictionary<KryptonPage, ViewDrawBorderEdge> { };
        #endregion
        
        #region Instance Fields
        protected ViewLayoutPageShow _oldRoot;
        protected ViewLayoutDocker _viewLayout;
        protected ViewLayoutScrollViewport _viewScrollViewport;
        private PageToNavCheckButton _pageLookup;
        private PageToButtonEdge _buttonEdgeLookup;
        private bool _hasFocus;
        private bool _events;
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
			_oldRoot = (ViewLayoutPageShow)ViewManager.Root;

            // Create and initialize all objects
            ViewManager.Root = CreateStackCheckButtonView();
            CreateNavCheckButtons();
            UpdateCheckButtonStyle();
            PostConstruct();

			// Need to monitor changes in the enabled state
			Navigator.EnabledChanged += new EventHandler(OnEnabledChanged);
            Navigator.AutoSizeChanged += new EventHandler(OnAutoSizeChanged);
        }

        /// <summary>
        /// Gets a value indicating if the mode is a tab strip style mode.
        /// </summary>
        public override bool IsTabStripMode 
        {
            get { return false; }
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
                foreach (KeyValuePair<KryptonPage, ViewDrawNavCheckButtonBase> pair in _pageLookup)
                    if (pair.Value == element)
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
            // Check each page level button spec
            if (_pageLookup != null)
            {
                foreach (KeyValuePair<KryptonPage, ViewDrawNavCheckButtonBase> pair in _pageLookup)
                {
                    ButtonSpec bs = pair.Value.ButtonSpecFromView(element);
                    if (bs != null)
                        return bs;
                }
            }

            return null;
        }

        /// <summary>
        /// Process a change in the selected page
        /// </summary>
        public override void SelectedPageChanged()
        {
            // Set correct ordering and dock setting
            ReorderCheckButtons();

            // If we have a selected page then we need to bring it into view
            if (Navigator.SelectedPage != null)
            {
                // We should have a view for representing the page
                if (_pageLookup.ContainsKey(Navigator.SelectedPage))
                {
                    // Get the check button used to represent the selected page
                    ViewDrawNavCheckButtonBase selected = _pageLookup[Navigator.SelectedPage];

                    // Make sure the layout is uptodate
                    Navigator.CheckPerformLayout();

                    // Get the client rectangle of the check button
                    Rectangle buttonRect = selected.ClientRectangle;

                    // Ask the viewport to bring this rectangle into view
                    _viewScrollViewport.BringIntoView(buttonRect);
                }
            }

            // Update to use the correct enabled/disabled palette
            UpdateStatePalettes();

            // Let base class perform common actions
            base.SelectedPageChanged();
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
                    _pageLookup[page].Visible = page.LastVisibleSet;
                    _buttonEdgeLookup[page].Visible = page.LastVisibleSet;

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
                    UpdateStatePalettes();
                    _pageLookup[page].Enabled = page.Enabled;

                    // Need to repaint to show the change
                    Navigator.PerformNeedPaint(true);
                }
            }

            // Let base class do standard work
            base.PageEnabledStateChanged(page);
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

            // We are only interested if the page is visible
            if (page.LastVisibleSet)
            {
                switch (property)
                {
                    case "Text":
                    case "TextTitle":
                    case "TextDescription":
                    case "ImageSmall":
                    case "ImageMedium":
                    case "ImageLarge":
                        // Need to layout and paint to effect change
                        PerformNeedPagePaint(true);
                        break;
                }
            }

            // Let base class do standard work
            base.PageAppearanceChanged(page, property);
        }

        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public override void UpdateStatePalettes()
        {
            if (_pageLookup != null)
            {
                PaletteBorderEdge buttonEdge;

                // If whole navigator is disabled then all of view is disabled
                bool enabled = Navigator.Enabled;
                bool checkEnabled = enabled;

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
                    {
                        buttonEdge = Navigator.SelectedPage.StateDisabled.BorderEdge;

                        // If page is disabled then all of view should look disabled
                        checkEnabled = false;
                    }
                }

                // Update each of the button edge palettes
                foreach (ViewDrawBorderEdge view in _buttonEdgeLookup.Values)
                {
                    view.Enabled = checkEnabled;
                    view.SetPalettes(buttonEdge);
                }

                // Update the border edge palette used by the scrolling viewport
                _viewScrollViewport.Enabled = enabled;
                _viewScrollViewport.SetPalettes(buttonEdge);
            }

            // Let base class perform common actions
            base.UpdateStatePalettes();
        }

		/// <summary>
		/// Destruct the previously created view.
		/// </summary>
		public override void Destruct()
		{
			// Unhook from events
            _viewScrollViewport.AnimateStep -= new EventHandler(OnViewportAnimation);
            Navigator.EnabledChanged -= new EventHandler(OnEnabledChanged);
            Navigator.AutoSizeChanged -= new EventHandler(OnAutoSizeChanged);

            // Pull down the view hierarchy
            DestructNavCheckButtons();
            DestructStackCheckButtonView();

			// Put the old root back again
			ViewManager.Root = _oldRoot;

			// Let base class perform common operations
			base.Destruct();
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
                    if (element is ViewDrawNavCheckButtonStack)
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
                switch (keyCode)
                {
                    case Keys.Tab:
                        // Using a CONTROL tab means selecting the another page
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
                            // Can only use Up arrow when on a vertical stack
                            if (Navigator.Stack.StackOrientation == Orientation.Vertical)
                                SelectPreviousPage(Navigator.SelectedPage, false, false);
                            return true;
                        }
                        break;
                    case Keys.Down:
                        if (_hasFocus)
                        {
                            // Can only use Down arrow when on a vertical stack
                            if (Navigator.Stack.StackOrientation == Orientation.Vertical)
                                SelectNextPage(Navigator.SelectedPage, false, false);
                            return true;
                        }
                        break;
                    case Keys.Right:
                        if (_hasFocus)
                        {
                            // Can only use Right arrow when on a horizontal stack
                            if (Navigator.Stack.StackOrientation == Orientation.Horizontal)
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
                            // Can only use Right arrow when on a horizontal stack
                            if (Navigator.Stack.StackOrientation == Orientation.Horizontal)
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

            // We do not eat the key
            return false;
        }

        /// <summary>
        /// Is the provided over a part of the view that wants the mouse.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        /// <returns>True if the view wants the mouse position; otherwise false.</returns>
        public override bool DesignerGetHitTest(Point pt)
        {
            // Check that the point is into the stack scrolling viewport
            if (_viewScrollViewport.ClientRectangle.Contains(pt))
            {
                // Get the control that owns the view layout
                Control owningControl = _viewLayout.OwningControl;

                // Convert incoming point from navigator to owning control
                pt = owningControl.PointToClient(Navigator.PointToScreen(pt));

                // Check if any of the stack check buttons want the point
                foreach (ViewBase item in _viewLayout)
                    if (item is ViewDrawNavCheckButtonStack)
                        if (item.ClientRectangle.Contains(pt))
                            return true;
            }

            return false;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create the mode specific view hierarchy.
        /// </summary>
        /// <returns>View element to use as base of hierarchy.</returns>
        protected virtual ViewBase CreateStackCheckButtonView()
        {
            // Set the initial preferred direction for the selected page
            _oldRoot.SetMinimumAsPreferred(!Navigator.AutoSize);

            // Derived class should return something more usefull!
            return null;
        }

        /// <summary>
        /// Destruct the mode specific view hierarchy.
        /// </summary>
        protected virtual void DestructStackCheckButtonView()
        {
            // Reset the preferred direction handling to original setting
            _oldRoot.SetMinimumAsPreferred(false);
        }

        /// <summary>
        /// Allow operations to occur after main construct actions.
        /// </summary>
        protected virtual void PostConstruct()
        {
            // Hook into the viewport animation steps
            _viewScrollViewport.AnimateStep += new EventHandler(OnViewportAnimation);

            UpdateStatePalettes();
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
                case "BorderEdgeStyleStack":
                    Navigator.StateCommon.BorderEdgeStyle = Navigator.Stack.BorderEdgeStyle;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "CheckButtonStyleStack":
                    UpdateCheckButtonStyle();
                    ReorderCheckButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "StackAnimation":
                    _viewScrollViewport.AnimateChange = Navigator.Stack.StackAnimation;
                    break;
                case "StackOrientation":
                    // We only use minimum values if not calculating based on auto sizing
                    _oldRoot.SetMinimumAsPreferred(!Navigator.AutoSize);
                    _viewScrollViewport.VerticalViewport = (Navigator.Stack.StackOrientation == Orientation.Vertical);
                    ReorderCheckButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "StackAlignment":
                case "ItemOrientationStack":
                    ReorderCheckButtons();
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
        private void CreateNavCheckButtons()
        {
            // Maintain lookup between page and check button/button edge that represent it
            _pageLookup = new PageToNavCheckButton();
            _buttonEdgeLookup = new PageToButtonEdge();

            VisualOrientation checkButtonOrient = ResolveButtonOrientation();
            RelativePositionAlign alignment = Navigator.Stack.StackAlignment;
            Orientation stackOrient = Navigator.Stack.StackOrientation;
            Orientation buttonEdgeOrient = (stackOrient == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical);
            ViewDockStyle dockNear = (stackOrient == Orientation.Vertical ? ViewDockStyle.Top : ViewDockStyle.Left);
            ViewDockStyle dockFar = (stackOrient == Orientation.Vertical ? ViewDockStyle.Bottom : ViewDockStyle.Right);

            // Cache the border edge palette to use
            PaletteBorderEdge buttonEdgePalette = (Navigator.Enabled ? Navigator.StateNormal.BorderEdge : 
                                                                       Navigator.StateDisabled.BorderEdge);

            // Start stacking from the top/left if not explicitly set to be far aligned
            bool dockTopLeft = (alignment != RelativePositionAlign.Far);

            // Create a check button to represent each krypton page
            foreach (KryptonPage page in Navigator.Pages)
            {
                // Create the draw view element for the check button and provide page it represents
                ViewDrawNavCheckButtonStack checkButton = new ViewDrawNavCheckButtonStack(Navigator, page, checkButtonOrient);

                // Provide the drag rectangle when requested for this button
                checkButton.ButtonDragRectangle += new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkButton.ButtonDragOffset += new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);

                // Need to know when check button needs repainting
                checkButton.NeedPaint = NeedPaintDelegate;

                // Set the initial state
                checkButton.Visible = page.LastVisibleSet;
                checkButton.Enabled = page.Enabled;
                checkButton.Checked = (Navigator.SelectedPage == page);
                checkButton.Orientation = checkButtonOrient;

                // Create the border edge for use next to the check button
                ViewDrawBorderEdge buttonEdge = new ViewDrawBorderEdge(buttonEdgePalette, buttonEdgeOrient);
                buttonEdge.Visible = page.LastVisibleSet;

                // Add to lookup dictionary
                _pageLookup.Add(page, checkButton);
                _buttonEdgeLookup.Add(page, buttonEdge);

                // Add to the child collection with the correct docking style
                if (dockTopLeft)
                {
                    _viewLayout.Insert(1, checkButton);
                    _viewLayout.Insert(1, buttonEdge);
                    _viewLayout.SetDock(buttonEdge, dockNear);
                    _viewLayout.SetDock(checkButton, dockNear);
                }
                else
                {
                    _viewLayout.Add(buttonEdge, dockFar);
                    _viewLayout.Add(checkButton, dockFar);
                }

                // All entries after the selected page are docked at the bottom/right unless
                // we have been set to stack near or far, in which case we do not change.
                if (checkButton.Checked && (alignment == RelativePositionAlign.Center))
                    dockTopLeft = false;
            }

            // Need to monitor changes in the page collection to reflect in layout bar
            Navigator.Pages.Inserted += new TypedHandler<KryptonPage>(OnPageInserted);
            Navigator.Pages.Removed += new TypedHandler<KryptonPage>(OnPageRemoved);
            Navigator.Pages.Cleared += new EventHandler(OnPagesCleared);
            _events = true;
        }

        private void DestructNavCheckButtons()
        {
            // Unhook from monitoring the pages collection
            _events = false;
            Navigator.Pages.Inserted -= new TypedHandler<KryptonPage>(OnPageInserted);
            Navigator.Pages.Removed -= new TypedHandler<KryptonPage>(OnPageRemoved);
            Navigator.Pages.Cleared -= new EventHandler(OnPagesCleared);

            // Must clean up buttons in way that removes all event hooks
            DestructCheckButtons();
        }

        private void DestructCheckButtons()
        {
            // Must tell each check button it is no longer required
            foreach (ViewDrawNavCheckButtonBase checkButton in _pageLookup.Values)
            {
                // Must unhook from events
                checkButton.ButtonDragRectangle -= new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkButton.ButtonDragOffset -= new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);
                checkButton.NeedPaint = null;

                // Dispose of element gracefully
                checkButton.Dispose();

                // Remove it from the group view
                _viewLayout.Remove(checkButton);
            }

            // Must tell each border edge it is no longer required
            foreach (ViewDrawBorderEdge buttonEdge in _buttonEdgeLookup.Values)
            {
                // Dispose of element gracefully
                buttonEdge.Dispose();

                // Remove it from the group view
                _viewLayout.Remove(buttonEdge);
            }

            // Remove all associations from the lookup dictionarys
            _pageLookup.Clear();
            _buttonEdgeLookup.Clear();
        }

        private void UpdateCheckButtonStyle()
        {
            Navigator.StateCommon.CheckButton.SetStyles(Navigator.Stack.CheckButtonStyle);
            Navigator.OverrideFocus.CheckButton.SetStyles(Navigator.Stack.CheckButtonStyle);

            // Update each individual button with the new style for remapping page level button specs
            if (_pageLookup != null)
                foreach (KeyValuePair<KryptonPage, ViewDrawNavCheckButtonBase> pair in _pageLookup)
                    if (pair.Value.ButtonSpecManager != null)
                        pair.Value.ButtonSpecManager.SetRemapTarget(Navigator.Stack.CheckButtonStyle);
        }

        private void UpdateSelectedPageFocus()
        {
            // If there is a page selected
            if ((Navigator.SelectedPage != null) && (_pageLookup != null))
            {
                // We should have a view for representing the page
                if (_pageLookup.ContainsKey(Navigator.SelectedPage))
                {
                    // Get the associated view element for the page
                    ViewDrawNavCheckButtonBase checkButton = _pageLookup[Navigator.SelectedPage];

                    // Reflect focus is in the check button
                    checkButton.HasFocus = _hasFocus;

                    // Need to repaint to show the change
                    Navigator.PerformNeedPaint(true);
                }
            }
        }

        private void BringPageIntoView(KryptonPage page)
        {
            // Remember the view for the requested page
            ViewDrawNavCheckButtonBase viewPage = null;

            // Make sure only the selected page is checked
            foreach (ViewDrawNavCheckButtonBase child in _pageLookup.Values)
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
                _viewScrollViewport.BringIntoView(viewPage.ClientRectangle);
            }
        }

        private void OnPageInserted(object sender, TypedCollectionEventArgs<KryptonPage> e)
        {
            if (!Navigator.IsDisposed && _events)
            {
                // Create the draw view element for the check button and provide page it represents
                ViewDrawNavCheckButtonStack checkButton = new ViewDrawNavCheckButtonStack(Navigator, e.Item, ResolveButtonOrientation());

                // Provide the drag rectangle when requested for this button
                checkButton.ButtonDragRectangle += new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkButton.ButtonDragOffset += new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);

                // Need to know when check button needs repainting
                checkButton.NeedPaint = NeedPaintDelegate;

                // Set the initial state
                checkButton.Visible = e.Item.LastVisibleSet;
                checkButton.Enabled = e.Item.Enabled;
                checkButton.Checked = (Navigator.SelectedPage == e.Item);

                // Find the border edge palette to use
                PaletteBorderEdge buttonEdgePalette = (Navigator.Enabled ? Navigator.StateNormal.BorderEdge :
                                                                           Navigator.StateDisabled.BorderEdge);

                // Create the border edge for use next to the check button
                ViewDrawBorderEdge buttonEdge = new ViewDrawBorderEdge(buttonEdgePalette, Navigator.Stack.StackOrientation);
                buttonEdge.Visible = e.Item.LastVisibleSet;

                // Add to lookup dictionary
                _pageLookup.Add(e.Item, checkButton);
                _buttonEdgeLookup.Add(e.Item, buttonEdge);

                // Set correct ordering and dock setting
                ReorderCheckButtons();

                // Need to repaint to show the change
                Navigator.PerformNeedPaint(true);
            }
        }

        private void OnPageRemoved(object sender, TypedCollectionEventArgs<KryptonPage> e)
        {
            if (!Navigator.IsDisposed && _events)
            {
                // Get the associated check button view element
                ViewDrawNavCheckButtonBase checkButton = _pageLookup[e.Item];
                ViewDrawBorderEdge buttonEdge = _buttonEdgeLookup[e.Item];

                // Must unhook from events
                checkButton.ButtonDragRectangle -= new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkButton.ButtonDragOffset -= new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);
                checkButton.NeedPaint = null;

                // Tell the views they are no longer required
                checkButton.Dispose();
                buttonEdge.Dispose();

                // Remove the views from the group
                _viewLayout.Remove(checkButton);
                _viewLayout.Remove(buttonEdge);

                // Remove associations from the lookup dictionarys
                _pageLookup.Remove(e.Item);
                _buttonEdgeLookup.Remove(e.Item);

                // Set correct ordering and dock setting
                ReorderCheckButtons();

                // Need to repaint to show the change
                Navigator.PerformNeedPaint(true);
            }
        }

        private void OnPagesCleared(object sender, EventArgs e)
        {
            if (!Navigator.IsDisposed && _events)
            {
                // Pull down all the check button view elements
                DestructCheckButtons();

                // Need to repaint to show the change
                Navigator.PerformNeedPaint(true);
            }
        }

        private void ReorderCheckButtons()
        {
            // Clear out the child list
            _viewLayout.Clear();

            // Always add the filler as the first item
            _viewLayout.Add(_oldRoot);

            VisualOrientation checkButtonOrient = ResolveButtonOrientation();
            RelativePositionAlign alignment = Navigator.Stack.StackAlignment;
            Orientation stackOrient = Navigator.Stack.StackOrientation;
            Orientation buttonEdgeOrient = (stackOrient == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical);
            ViewDockStyle dockNear = (stackOrient == Orientation.Vertical ? ViewDockStyle.Top : ViewDockStyle.Left);
            ViewDockStyle dockFar = (stackOrient == Orientation.Vertical ? ViewDockStyle.Bottom : ViewDockStyle.Right);

            // Start stacking from the top/left if not explicitly set to be far aligned
            bool dockTopLeft = (alignment != RelativePositionAlign.Far);

            // Add back the pages in the order they are in collection
            foreach (KryptonPage page in Navigator.Pages)
            {
                // Check that a view element exists for the page
                if (_pageLookup.ContainsKey(page))
                {
                    // Get the associated view elements
                    ViewDrawNavCheckButtonBase checkButton = _pageLookup[page];
                    ViewDrawBorderEdge buttonEdge = _buttonEdgeLookup[page];

                    checkButton.Checked = (Navigator.SelectedPage == page);
                    checkButton.Orientation = checkButtonOrient;
                    checkButton.HasFocus = (_hasFocus && (Navigator.SelectedPage == page));
                    checkButton.Visible = page.LastVisibleSet;
                    buttonEdge.Visible = page.LastVisibleSet;
                    buttonEdge.Orientation = buttonEdgeOrient;

                    // Add to the child collection with the correct docking style
                    if (dockTopLeft)
                    {
                        _viewLayout.Insert(1, checkButton);
                        _viewLayout.Insert(1, buttonEdge);
                        _viewLayout.SetDock(buttonEdge, dockNear);
                        _viewLayout.SetDock(checkButton, dockNear);
                    }
                    else
                    {
                        _viewLayout.Add(buttonEdge, dockFar);
                        _viewLayout.Add(checkButton, dockFar);
                    }

                    // All entries after the selected page are docked at the bottom/right unless
                    // we have been set to stack near or far, in which case we do not change.
                    if (checkButton.Checked && (alignment == RelativePositionAlign.Center))
                        dockTopLeft = false;
                }
            }
        }

        private VisualOrientation ResolveButtonOrientation()
        {
            switch (Navigator.Stack.ItemOrientation)
            {
                case ButtonOrientation.Auto:
                    if (Navigator.Stack.StackOrientation == Orientation.Vertical)
                        return VisualOrientation.Top;
                    else
                        return VisualOrientation.Left;
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
        
        private void OnEnabledChanged(object sender, EventArgs e)
		{
            UpdateStatePalettes();
            Navigator.PerformLayout();
            Navigator.Invalidate();
        }

        private void OnAutoSizeChanged(object sender, EventArgs e)
        {
            // Only use minimum instead of preferred if not using AutoSize
            _oldRoot.SetMinimumAsPreferred(!Navigator.AutoSize);
        }

        private void OnViewportAnimation(object sender, EventArgs e)
        {
            Navigator.PerformNeedPaint(true);
        }

        private void OnCheckButtonDragRect(object sender, ButtonDragRectangleEventArgs e)
        {
            // Cast incoming reference to the actual check button view
            ViewDrawNavCheckButtonStack reorderItem = (ViewDrawNavCheckButtonStack)sender;

            e.PreDragOffset = (Navigator.AllowPageReorder && reorderItem.Page.AreFlagsSet(KryptonPageFlags.AllowPageReorder));
            Rectangle dragRect = Rectangle.Union(e.DragRect, _viewScrollViewport.ClientRectangle);
            dragRect.Inflate(new Size(15, 15));
            e.DragRect = dragRect;
        }

        private void OnCheckButtonDragOffset(object sender, ButtonDragOffsetEventArgs e)
        {
            // Cast incoming reference to the actual check button view
            ViewDrawNavCheckButtonStack reorderView = (ViewDrawNavCheckButtonStack)sender;

            // Scan the collection of children
            bool foundReorderView = false;
            Orientation stackOrient = Navigator.Stack.StackOrientation;
            foreach (KryptonPage page in Navigator.Pages)
            {
                // If the mouse is over this button
                ViewDrawNavCheckButtonStack childView = (ViewDrawNavCheckButtonStack)_pageLookup[page];
                if (childView.ClientRectangle.Contains(e.PointOffset))
                {
                    // Only interested if mouse over a different check button
                    if (childView != reorderView)
                    {
                        Rectangle childRect = childView.ClientRectangle;

                        if (foundReorderView)
                        {
                            if (stackOrient == Orientation.Vertical)
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
                                    Navigator.Pages.MoveAfter(movePage, PageFromView(childView));
                                    RecreateView();
                                    Navigator.PerformLayout();
                                    Navigator.Refresh();
                                    Navigator.OnTabMoved(new TabMovedEventArgs(movePage, Navigator.Pages.IndexOf(movePage)));
                                }
                            }
                        }
                        else
                        {
                            if (stackOrient == Orientation.Vertical)
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
            // Remove all the existing layout content except the old root at postiion 0
            ViewBase firstChild = _viewLayout[0];
            _viewLayout.Clear();
            _viewLayout.Add(firstChild);

            // Start stacking from the top/left if not explicitly set to be far aligned
            RelativePositionAlign alignment = Navigator.Stack.StackAlignment;
            Orientation stackOrient = Navigator.Stack.StackOrientation;
            ViewDockStyle dockNear = (stackOrient == Orientation.Vertical ? ViewDockStyle.Top : ViewDockStyle.Left);
            ViewDockStyle dockFar = (stackOrient == Orientation.Vertical ? ViewDockStyle.Bottom : ViewDockStyle.Right);

            bool dockTopLeft = (alignment != RelativePositionAlign.Far);

            foreach (KryptonPage page in Navigator.Pages)
            {
                // Grab the page associated view elements
                ViewDrawNavCheckButtonStack checkButton = (ViewDrawNavCheckButtonStack)_pageLookup[page];
                ViewDrawBorderEdge buttonEdge = (ViewDrawBorderEdge)_buttonEdgeLookup[page];

                // Add to the child collection with the correct docking style
                if (dockTopLeft)
                {
                    _viewLayout.Insert(1, checkButton);
                    _viewLayout.Insert(1, buttonEdge);
                    _viewLayout.SetDock(buttonEdge, dockNear);
                    _viewLayout.SetDock(checkButton, dockNear);
                }
                else
                {
                    _viewLayout.Add(buttonEdge, dockFar);
                    _viewLayout.Add(checkButton, dockFar);
                }

                // All entries after the selected page are docked at the bottom/right unless
                // we have been set to stack near or far, in which case we do not change.
                if (checkButton.Checked && (alignment == RelativePositionAlign.Center))
                    dockTopLeft = false;
            }
        }
        #endregion
	}
}
