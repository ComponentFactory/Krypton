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
    /// Implements base functionality for NavigatorMode.Outlook modes.
	/// </summary>
    internal abstract class ViewBuilderOutlookBase : ViewBuilderBase,
                                                     ISeparatorSource
	{
        #region Type Definitons
        protected class PageToButtonEdge : Dictionary<KryptonPage, ViewDrawBorderEdge> { };

        /// <summary>
        /// Collection for managing ButtonSpecMdiChildFixed instances.
        /// </summary>
        protected class OutlookButtonSpecCollection : ButtonSpecCollection<ButtonSpec>
        {
            #region Identity
            /// <summary>
            /// Initialize a new instance of the OutlookButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public OutlookButtonSpecCollection(KryptonNavigator owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Static Fields
        private static readonly int _separatorLength = 7;
        private static Bitmap _moreButtons;
        private static Bitmap _fewerButtons;
        #endregion

        #region Instance Fields
        private KryptonContextMenu _kcm;
        private ViewLayoutPageShow _oldRoot;
        private ViewletHeaderGroupOutlook _headerGroup;
        private ViewDrawDocker _viewHeaderGroup;
        private ViewDrawDocker _viewOverflowBar;
        private ViewDrawBorderEdge _viewSeparatorEdge;
        private ViewDrawSeparator _viewSeparator;
        private ButtonSpecAny _specDropDown;
        private OutlookButtonSpecCollection _buttons;
        private ButtonSpecNavManagerLayoutBar _buttonManager;
        private PageToButtonEdge _buttonEdgeLookup;
        private SeparatorController _separatorController;
        private bool _hasFocus;
        private bool _events;

        /// <summary>Lookup between pages and stack buttons.</summary>
        protected PageToNavCheckButton _pageStackLookup;
        /// <summary>Layout element for the client area.</summary>
        protected ViewLayoutDocker _viewLayout;
        /// <summary>Layout element for the overflow area.</summary>
        protected ViewLayoutDocker _viewOverflowLayout;
        /// <summary>Lookup between pages and check buttons that represent the page.</summary>
        protected PageToNavCheckButton _pageOverflowLookup;
        #endregion

        #region Identity
        static ViewBuilderOutlookBase()
        {
            // Get the assembly that contains the bitmap resource
            Assembly myAssembly = Assembly.GetAssembly(typeof(ViewBuilderOutlookBase));

            // Get the resource streams containing the images
            Stream streamBlueUp = myAssembly.GetManifestResourceStream("ComponentFactory.Krypton.Navigator.Resources.BlueUp.bmp");
            Stream streamBlueDown= myAssembly.GetManifestResourceStream("ComponentFactory.Krypton.Navigator.Resources.BlueDown.bmp");

            // Load the bitmap from stream
            _moreButtons = new Bitmap(streamBlueUp, true);
            _fewerButtons = new Bitmap(streamBlueDown, true);
        }
        #endregion

        #region ISeparatorSource
        /// <summary>
        /// Gets the top level control of the source.
        /// </summary>
        public abstract Control SeparatorControl { get; }

        /// <summary>
        /// Gets the orientation of the separator.
        /// </summary>
        public Orientation SeparatorOrientation
        {
            get 
            { 
                if (Navigator.Outlook.Orientation == Orientation.Vertical)
                    return Orientation.Horizontal;
                else
                    return Orientation.Vertical;
            }
        }

        /// <summary>
        /// Can the separator be moved by the user.
        /// </summary>
        public bool SeparatorCanMove
        {
            get  
            { 
                return (GetShrinkStackItem() != null) || 
                       (GetExpandOverflowItem() != null); }
        }

        /// <summary>
        /// Gets the amount the splitter can be incremented.
        /// </summary>
        public int SeparatorIncrements
        {
            get { return 1; }
        }

        /// <summary>
        /// Gets the box representing the minimum and maximum allowed splitter movement.
        /// </summary>
        public abstract Rectangle SeparatorMoveBox { get; }

        /// <summary>
        /// Indicates the separator is moving.
        /// </summary>
        /// <param name="mouse">Current mouse position in client area.</param>
        /// <param name="splitter">Current position of the splitter.</param>
        /// <returns>True if movement should be cancelled; otherwise false.</returns>
        public bool SeparatorMoving(Point mouse, Point splitter)
        {
            bool layout = false;

            int mouseDelta;

            // Find how far the mouse has moved from the separator
            if (Navigator.Outlook.Orientation == Orientation.Vertical)
                mouseDelta = mouse.Y - _viewSeparator.ClientRectangle.Bottom;
            else
                mouseDelta = mouse.X - _viewSeparator.ClientRectangle.Right;

            // Is the mouse trying to reduce the size of the stacked area
            if (mouseDelta > 0)
            {
                // We want to shrink the last page in the stack first, so search list in reverse order
                for(int i=Navigator.Pages.Count-1; i>=0; i--)
                {
                    // Get access to the indexed page
                    KryptonPage page = Navigator.Pages[i];

                    // Is this page in the stack?
                    if (_pageStackLookup.ContainsKey(page))
                    {
                        // Get the page related view elements
                        ViewDrawNavCheckButtonBase checkButton = _pageStackLookup[page];
                        ViewDrawBorderEdge borderEdge = _buttonEdgeLookup[page];

                        // Only interested if the button is actually visible
                        if (checkButton.Visible)
                        {
                            int checkLength;

                            // Get the length of the check button
                            if (Navigator.Outlook.Orientation == Orientation.Vertical)
                                checkLength = checkButton.ClientHeight;
                            else
                                checkLength = checkButton.ClientWidth;

                            // If the check button border is showing
                            if (borderEdge.Visible)
                            {
                                // Add on the length of the border
                                if (Navigator.Outlook.Orientation == Orientation.Vertical)
                                    checkLength += borderEdge.ClientHeight;
                                else
                                    checkLength += borderEdge.ClientWidth;
                            }

                            // Should we move the stack item to the overflow bar...
                            if (mouseDelta >= checkLength)
                            {
                                // Update flag to say it should be on the overflow bar
                                checkButton.Page.SetFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);

                                // Need to layout to reflect change
                                layout = true;

                                // Reduce delta by the amount we have removed
                                mouseDelta -= checkLength;

                                // If no more space left, then finished
                                if (mouseDelta <= 0)
                                    break;
                            }
                        }
                    }
                }
            }

            // Is the mouse trying to increase the size of the stacked area
            if (mouseDelta < 0)
            {
                int mousePos;
                int separatorPos;
                int checkButtonPos;

                // Get the orientation specific values
                if (Navigator.Outlook.Orientation == Orientation.Vertical)
                {
                    mousePos = mouse.Y;
                    separatorPos = _viewSeparator.ClientLocation.Y;
                }
                else
                {
                    mousePos = mouse.X;
                    separatorPos = _viewSeparator.ClientLocation.X;
                }

                // Check if the mouse is high enough to cause a new overflow item to be shown
                foreach (ViewDrawNavCheckButtonBase checkButton in _pageOverflowLookup.Values)
                    if (checkButton.Visible)
                    {
                        // Get the orientation specific test value
                        if (Navigator.Outlook.Orientation == Orientation.Vertical)
                            checkButtonPos = checkButton.ClientHeight;
                        else
                            checkButtonPos = checkButton.ClientWidth;

                        if (mousePos < (separatorPos - checkButtonPos))
                        {
                            // Update flag to say it should not be on the overflow bar
                            checkButton.Page.ClearFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);

                            // Need to layout to reflect change
                            layout = true;

                            // Only bring one into view at a time
                            break;
                        }
                    }
            }

            if (layout)
                PerformNeedPaint(true);

            return false;
        }

        /// <summary>
        /// Indicates the separator has moved.
        /// </summary>
        /// <param name="mouse">Current mouse position in client area.</param>
        /// <param name="splitter">Current position of the splitter.</param>
        public void SeparatorMoved(Point mouse, Point splitter)
        {
        }

        /// <summary>
        /// Indicates the separator has not been moved.
        /// </summary>
        public void SeparatorNotMoved()
        {
        }
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
            ViewManager.Root = CreateView();
            CreateStackItems();
            CreateOverflowItems();
            CreateSeparatorController();
            CreateButtonManager();
            UpdateCheckButtonStyle();
            UpdateMiniButtonStyle();
            PostConstruct();

            // Force buttons to be recreated in the headers
            if (_buttonManager != null)
                _buttonManager.RecreateButtons();

			// Need to monitor changes in the enabled state
			Navigator.EnabledChanged += new EventHandler(OnEnabledChanged);
            Navigator.AutoSizeChanged += new EventHandler(OnAutoSizeChanged);
        }

        /// <summary>
        /// Gets the KryptonPage associated with the provided view element.
        /// </summary>
        /// <param name="element">Element to search against.</param>
        /// <returns>Reference to KryptonPage; otherwise null.</returns>
        public override KryptonPage PageFromView(ViewBase element)
        {
            if (_pageOverflowLookup != null)
            {
                foreach (KeyValuePair<KryptonPage, ViewDrawNavCheckButtonBase> pair in _pageOverflowLookup)
                    if (pair.Value == element)
                        return pair.Key;
            }

            if (_pageStackLookup != null)
            {
                foreach (KeyValuePair<KryptonPage, ViewDrawNavCheckButtonBase> pair in _pageStackLookup)
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
            // Delegate lookup to the viewlet that has the button spec manager
            ButtonSpec bs = (_buttonManager != null ? _headerGroup.ButtonSpecFromView(element) : null);

            // Check each page level button spec
            if ((bs == null) && (_pageStackLookup != null))
            {
                foreach (KeyValuePair<KryptonPage, ViewDrawNavCheckButtonBase> pair in _pageStackLookup)
                {
                    bs = pair.Value.ButtonSpecFromView(element);
                    if (bs != null)
                        break;
                }
            }

            return bs;
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
                if (_pageStackLookup.ContainsKey(Navigator.SelectedPage))
                {
                    // Get the check button used to represent the selected page
                    ViewDrawNavCheckButtonBase selected = _pageStackLookup[Navigator.SelectedPage];

                    // Can only bring page into view if actually visible
                    if (selected.Visible)
                    {
                        // Make sure the layout is uptodate
                        Navigator.CheckPerformLayout();
                    }
                }
            }

            // Update to use the correct enabled/disabled palette
            UpdateStatePalettes();

            // Ask the header group to update the 
            if (_headerGroup != null)
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
            
            if (_headerGroup != null)
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
            // Sometimes the routine is called before the views have been fully setup
            if ((_pageStackLookup != null) && _pageStackLookup.ContainsKey(page) &&
                (_pageOverflowLookup != null) && _pageOverflowLookup.ContainsKey(page))
            {
                bool showPageStack = page.LastVisibleSet && !page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode);
                bool showPageOverflow = page.LastVisibleSet && !showPageStack;
                
                // Reflect new state in the check button
                _pageStackLookup[page].Visible = showPageStack;
                _pageOverflowLookup[page].Visible = showPageOverflow;
                _buttonEdgeLookup[page].Visible = showPageStack;
            }

            // Ensure buttons are recreated to reflect different visible state
            if (_headerGroup != null)
                _headerGroup.UpdateButtons();

            // Need to repaint to show the change
            Navigator.PerformNeedPaint(true);

            // Let base class do standard work
            base.PageVisibleStateChanged(page);
        }

        /// <summary>
        /// Process a change in the enabled state for a page.
        /// </summary>
        /// <param name="page">Page that has changed enabled state.</param>
        public override void PageEnabledStateChanged(KryptonPage page)
        {
            // Reflect new state in the check button
            UpdateStatePalettes();

            // Sometimes the routine is called before the views have been fully setup
            if ((_pageStackLookup != null) && _pageStackLookup.ContainsKey(page) &&
                (_pageOverflowLookup != null) && _pageOverflowLookup.ContainsKey(page))
            {
                _pageStackLookup[page].Enabled = page.Enabled;
                _pageOverflowLookup[page].Enabled = page.Enabled;
            }

            // Ensure buttons are recreated to reflect different enabled state
            if (_headerGroup != null)
                _headerGroup.UpdateButtons();

            // Need to repaint to show the change
            Navigator.PerformNeedPaint(true);

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
        /// Notification that krypton page flags have changed.
        /// </summary>
        /// <param name="page">Page that has changed.</param>
        /// <param name="changed">Set of flags that have changed value.</param>
        public override void PageFlagsChanged(KryptonPage page, KryptonPageFlags changed)
        {
            // Any change to the overflow bar setting requires a layout to effect
            if ((changed & KryptonPageFlags.PageInOverflowBarForOutlookMode) == KryptonPageFlags.PageInOverflowBarForOutlookMode)
                PerformNeedPaint(true);

            // Let base class do standard work
            base.PageFlagsChanged(page, changed);
        }

        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public override void UpdateStatePalettes()
        {
            if (_buttonEdgeLookup != null)
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

                    // Update the separator view to use the navigator state objects
                    _viewSeparator.SetPalettes(Navigator.StateDisabled.Separator, Navigator.StateNormal.Separator,
                                               Navigator.StateTracking.Separator, Navigator.StatePressed.Separator,
                                               Navigator.StateDisabled.Separator, Navigator.StateNormal.Separator,
                                               Navigator.StateTracking.Separator, Navigator.StatePressed.Separator);

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

                    // Update the separator view to use the page state objects
                    _viewSeparator.SetPalettes(Navigator.SelectedPage.StateDisabled.Separator, Navigator.SelectedPage.StateNormal.Separator,
                                               Navigator.SelectedPage.StateTracking.Separator, Navigator.SelectedPage.StatePressed.Separator,
                                               Navigator.SelectedPage.StateDisabled.Separator, Navigator.SelectedPage.StateNormal.Separator,
                                               Navigator.SelectedPage.StateTracking.Separator, Navigator.SelectedPage.StatePressed.Separator);
                }

                // Update each of the border edge palettes
                foreach (ViewDrawBorderEdge view in _buttonEdgeLookup.Values)
                {
                    view.Enabled = checkEnabled;
                    view.SetPalettes(buttonEdge);
                }

                // Update the main view elements
                _viewOverflowBar.Enabled = enabled;
                _viewOverflowLayout.Enabled = enabled;

                // Update the fixed separator view elements
                _viewSeparatorEdge.Enabled = enabled;
                _viewSeparatorEdge.SetPalettes(buttonEdge);
                _viewSeparator.Enabled = enabled;

                // Update palettes for the header group
                if (_headerGroup != null)
                    _headerGroup.UpdateStatePalettes();
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
            Navigator.EnabledChanged -= new EventHandler(OnEnabledChanged);
            Navigator.AutoSizeChanged -= new EventHandler(OnAutoSizeChanged);

            // Unhook from monitoring the pages collection
            _events = false;
            Navigator.Pages.Inserted -= new TypedHandler<KryptonPage>(OnPageInserted);
            Navigator.Pages.Removed -= new TypedHandler<KryptonPage>(OnPageRemoved);
            Navigator.Pages.Cleared -= new EventHandler(OnPagesCleared);

            // Must clean up buttons in way that removes all event hooks
            DestructStackCheckButtons();
            DestructOverflowCheckButtons();

            // Reset the preferred direction handling to original setting
            _oldRoot.SetMinimumAsPreferred(false);

            // Destruct the header group viewlet
            _headerGroup.Destruct();

			// Put the old root back again
			ViewManager.Root = _oldRoot;

            // Dispose of the cached context menu
            if (_kcm != null)
            {
                _kcm.Close();
                _kcm.Dispose();
                _kcm = null;
            }

			// Let base class perform common operations
			base.Destruct();
        }

        /// <summary>
        /// Gets value indicating if the control has the focus.
        /// </summary>
        public bool HasFocus
        {
            get { return _hasFocus; }
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
            if (!HasFocus)
            {
                // Keep searching up the element tree for the type of interest
                while (element != null)
                {
                    // If pressing on a check button then we take the focus
                    if (element is ViewDrawNavCheckButtonBase)
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
                    case Keys.Home:
                        if (HasFocus)
                        {
                            SelectNextPage(null, false, false);
                            return true;
                        }
                        break;
                    case Keys.End:
                        if (HasFocus)
                        {
                            SelectPreviousPage(null, false, false);
                            return true;
                        }
                        break;
                    case Keys.Left:
                        if (HasFocus)
                        {
                            // Reverse the direction if working RightToLeft
                            if (Navigator.RightToLeft != RightToLeft.Yes)
                                SelectPreviousPage(Navigator.SelectedPage, false, false);
                            else
                                SelectNextPage(Navigator.SelectedPage, false, false);

                            return true;
                        }
                        break;
                    case Keys.Up:
                        if (HasFocus)
                        {
                            SelectPreviousPage(Navigator.SelectedPage, false, false);
                            return true;
                        }
                        break;
                    case Keys.Right:
                        if (HasFocus)
                        {
                            // Reverse the direction if working RightToLeft
                            if (Navigator.RightToLeft != RightToLeft.Yes)
                                SelectNextPage(Navigator.SelectedPage, false, false);
                            else
                                SelectPreviousPage(Navigator.SelectedPage, false, false);

                            return true;
                        }
                        break;
                    case Keys.Down:
                        if (HasFocus)
                        {
                            SelectNextPage(Navigator.SelectedPage, false, false);
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
        /// Select the next page to the one provided.
        /// </summary>
        /// <param name="page">Starting page for search.</param>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <param name="ctrlTab">Associated with a Ctrl+Tab action.</param>
        /// <returns>True if new page selected; otherwise false.</returns>
        public override bool SelectNextPage(KryptonPage page, 
                                            bool wrap,
                                            bool ctrlTab)
        {
            // There must be at least one page and allowed to select a page
            if ((Navigator.Pages.Count > 0) && Navigator.AllowTabSelect)
            {
                KryptonPage first;

                // If given a starting page, it must be in the pages collection, 
                // otherwise we start by searching from the first page onwards
                if ((page != null) && Navigator.Pages.Contains(page))
                {
                    first = NextOutlookActionPage(page);

                    // If at end of collection and wrapping is enabled then get the first page
                    if ((first == null) && wrap)
                        first = FirstOutlookActionPage();
                }
                else
                    first = FirstOutlookActionPage();

                // Next page to test is the first one 
                KryptonPage next = first;

                // Keep testing next pages until no more are left
                while (next != null)
                {
                    // Attempt to select the next page
                    Navigator.SelectedPage = next;

                    // If next page was selected, then all finished
                    if (Navigator.SelectedPage == next)
                        return true;
                    else
                    {
                        // Otherwise keep looking for another visible next page
                        next = NextOutlookActionPage(next);

                        // If we reached the end of the collection and we should wrap
                        if ((next == null) && wrap)
                        {
                            // Wrap around to the first page
                            next = FirstOutlookActionPage();
                        }

                        // If we are back at the first page we examined then we must have
                        // wrapped around collection and still found nothing, time to exit
                        if (next == first)
                            return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Select the previous page to the one provided.
        /// </summary>
        /// <param name="page">Starting page for search.</param>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <param name="ctrlTab">Associated with a Ctrl+Tab action.</param>
        /// <returns>True if new page selected; otherwise false.</returns>
        public override bool SelectPreviousPage(KryptonPage page, 
                                                bool wrap,
                                                bool ctrlTab)
        {
            // There must be at least one page and allowed to select a page
            if ((Navigator.Pages.Count > 0) && Navigator.AllowTabSelect)
            {
                KryptonPage first;

                // If given a starting page, it must be in the pages collection, 
                // otherwise we start by searching from the last page backwards
                if ((page != null) && Navigator.Pages.Contains(page))
                {
                    first = PreviousOutlookActionPage(page);

                    // If at start of collection and wrapping is enabled then get the last page
                    if ((first == null) && wrap)
                        first = LastOutlookActionPage();
                }
                else
                    first = LastOutlookActionPage();

                // Page to test is the first one 
                KryptonPage previous = first;

                // Keep testing previous pages until no more are left
                while (previous != null)
                {
                    // Attempt to select the previous page
                    Navigator.SelectedPage = previous;

                    // If previous page was selected, then all finished
                    if (Navigator.SelectedPage == previous)
                        return true;
                    else
                    {
                        // Otherwise keep looking for another visible previous page
                        previous = PreviousOutlookActionPage(previous);

                        // If we reached the start of the collection and we should wrap
                        if ((previous == null) && wrap)
                        {
                            // Wrap around to the last page
                            previous = LastOutlookActionPage();
                        }

                        // If we are back at the first page we examined then we must have
                        // wrapped around collection and still found nothing, time to exit
                        if (previous == first)
                            return false;
                    }
                }
            }

            return false;
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
            if ((_buttonManager != null) && _buttonManager.DesignerGetHitTest(pt))
                return true;

            // Check if any of the button specs want the point
            if (_headerGroup.DesignerGetHitTest(pt))
                return true;

            return false;
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

        /// <summary>
        /// Get a string that represents the visible state of the overflow butttons.
        /// </summary>
        /// <returns>State string.</returns>
        public string GetOverflowButtonStates()
        {
            string ret = string.Empty;

            // Sometimes the routine is called before the views have been fully setup
            if (_pageOverflowLookup != null)
            {
                // There swill be an overflow button per krypton page
                foreach (KryptonPage page in Navigator.Pages)
                {
                    // Double check that it exists in the lookup
                    if (_pageOverflowLookup.ContainsKey(page))
                    {
                        // Construct state by concatenating visible values
                        ret += (_pageOverflowLookup[page].Visible ? "T" : "F");
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Remove any shrinkage that has caused stacking items to be hidden from view.
        /// </summary>
        public void UnshrinkAppropriatePages()
        {
            // Sometimes the routine is called before the views have been fully setup
            if ((_pageStackLookup != null) && (_pageOverflowLookup != null))
            {
                // Make each visible krypton page have its check button and border edge visible unless
                // that page has the 'PageInOverflowBarForOutlookMode' flag set in which case it has
                // specifically requested to be placed on the overflow bar area.
                foreach (KryptonPage page in Navigator.Pages)
                {
                    if (_pageStackLookup.ContainsKey(page))
                    {
                        bool showPageStack = page.LastVisibleSet && !page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode);
                        _pageStackLookup[page].Visible = showPageStack;
                        _buttonEdgeLookup[page].Visible = showPageStack;

                        if (_pageOverflowLookup.ContainsKey(page))
                        {
                            bool showPageOverflow = page.LastVisibleSet && !showPageStack;
                            _pageOverflowLookup[page].Visible = showPageOverflow;
                        }
                    }
                }                    
            }
        }

        /// <summary>
        /// Request the stacking items be removed to allow the vertical scrollbar to be removed.
        /// </summary>
        /// <param name="shrinkage">Pixels that need freeing up to remove the vertical scrollbar.</param>
        /// <returns>True if a change was made; otherwise false.</returns>
        public bool ShrinkVertical(int shrinkage)
        {
            bool ret = false;

            // Sometimes the routine is called before the views have been fully setup
            if ((_pageStackLookup != null) && (_pageOverflowLookup != null))
            {
                // If we need to find any shrinkage
                if (shrinkage > 0)
                {
                    // Remove from display the stack item for each page
                    for (int i = Navigator.Pages.Count - 1; i >= 0; i--)
                    {
                        // Get the indexed page entry
                        KryptonPage page = Navigator.Pages[i];

                        // Only interested in hiding view items for pages that are visible
                        if (Navigator.Pages[i].LastVisibleSet)
                        {
                            // If the view item is not already hidden from view
                            if (_pageStackLookup.ContainsKey(page) && _pageStackLookup[page].Visible)
                            {
                                // We have made a change to the visible state of a view element
                                ret = true;

                                // Hide it from view and the associated border edge
                                _pageStackLookup[page].Visible = false;
                                _buttonEdgeLookup[page].Visible = false;

                                // Make the corresponding overflow item visible
                                _pageOverflowLookup[page].Visible = true;

                                // Reduce the shrinkage required by amount now hidden
                                shrinkage -= (_pageStackLookup[page].ClientHeight + _buttonEdgeLookup[page].ClientHeight);

                                // Have we provided all the shrinkage required?
                                if (shrinkage <= 0)
                                    break;
                            }
                        }
                    }
                }
            }

            return ret;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create the mode specific view hierarchy.
        /// </summary>
        /// <returns>View element to use as base of hierarchy.</returns>
        protected virtual ViewBase CreateView()
        {
            // Set the initial preferred direction for the selected page
            _oldRoot.SetMinimumAsPreferred(!Navigator.AutoSize);

            // Create the header group and fill with the view layout
            _headerGroup = new ViewletHeaderGroupOutlook(Navigator, Redirector, NeedPaintDelegate);
            _viewHeaderGroup = _headerGroup.Construct(CreateMainLayout());

            // Overflow buttons are placed inside the overflow layout view element
            _viewOverflowLayout = new ViewLayoutDocker();

            // Include invisible children in preferred size calculation
            _viewOverflowLayout.PreferredSizeAll = true;
            _viewOverflowLayout.Orientation = (Navigator.Outlook.Orientation == Orientation.Vertical ? VisualOrientation.Top : VisualOrientation.Left);

            // Use a separator to ensure a minimum size to the overflow area
            ViewLayoutSeparator sep = new ViewLayoutSeparator(0, 18);
            sep.Visible = false;
            _viewOverflowLayout.Add(sep, ViewDockStyle.Left);

            // Create the header that contains the overflow items
            _viewOverflowBar = new ViewDrawDocker(Navigator.StateNormal.HeaderGroup.HeaderOverflow.Back,
                                                  Navigator.StateNormal.HeaderGroup.HeaderOverflow.Border,
                                                  Navigator.StateNormal.HeaderGroup.HeaderOverflow,
                                                  PaletteMetricBool.None,
                                                  PaletteMetricPadding.HeaderGroupPaddingSecondary,
                                                  (Navigator.Outlook.Orientation == Orientation.Vertical ? VisualOrientation.Top : VisualOrientation.Left));

            // The only content of the bar is the layout with the actual contents
            _viewOverflowBar.Add(_viewOverflowLayout, ViewDockStyle.Fill);

            // Add the extra overflow header alongside the standard primary and secondary headers
            _viewHeaderGroup.Insert(0, _viewOverflowBar);
            _viewHeaderGroup.SetDock(_viewOverflowBar, (Navigator.Outlook.Orientation == Orientation.Vertical ? ViewDockStyle.Bottom : ViewDockStyle.Right));

            // Add the filler as the first content
            SetLayoutFiller(_viewLayout);

            return _viewHeaderGroup;
        }

        /// <summary>
        /// Creates and returns the view element that laysout the main client area.
        /// </summary>
        /// <returns></returns>
        protected abstract ViewBase CreateMainLayout();

        /// <summary>
        /// Gets the view element to use as the layout filler.
        /// </summary>
        /// <returns>ViewBase derived instance.</returns>
        protected virtual void SetLayoutFiller(ViewLayoutDocker viewLayout)
        {
            // Put the old root as the filler inside stack elements
            viewLayout.Add(_oldRoot, ViewDockStyle.Fill);
        }

        /// <summary>
        /// Create an overflow check button.
        /// </summary>
        /// <param name="page">Page to associate the check button with.</param>
        /// <param name="checkButtonOrient">Orientation of the check button.</param>
        /// <param name="dockFar">Docking position of the check button.</param>
        /// <returns></returns>
        protected virtual ViewDrawNavOutlookOverflow CreateOverflowItem(KryptonPage page,
                                                                        VisualOrientation checkButtonOrient,
                                                                        ViewDockStyle dockFar)
        {
            // Create the draw view element for the check button and provide page it represents
            ViewDrawNavOutlookOverflow checkButton = new ViewDrawNavOutlookOverflow(Navigator, page, checkButtonOrient);

            // Need to know when check button needs repainting
            checkButton.NeedPaint = NeedPaintDelegate;

            // Can we show the page as an overflow item?
            bool showPage = page.LastVisibleSet && !_pageStackLookup[page].Visible;

            // Set the initial state
            checkButton.Visible = showPage;
            checkButton.Enabled = page.Enabled;
            checkButton.Checked = (Navigator.SelectedPage == page);

            // Add to lookup dictionary
            _pageOverflowLookup.Add(page, checkButton);

            return checkButton;
        }

        /// <summary>
        /// Add the check buttons for pages that should be on the overflow area.
        /// </summary>
        /// <param name="page">Reference to owning page.</param>
        /// <param name="checkOverflowOrient">Docking edge to dock against.</param>
        /// <param name="overflowInsertIndex">Index for inserting the new entry.</param>
        protected virtual void ReorderCheckButtonsOverflow(KryptonPage page,
                                                           VisualOrientation checkOverflowOrient,
                                                           ref int overflowInsertIndex)
        {
            // Check that an overflow view element exists for the page
            if (_pageOverflowLookup.ContainsKey(page))
            {
                // Get the associated view element
                ViewDrawNavCheckButtonBase checkButton = _pageOverflowLookup[page];

                // Update checked state of the button
                checkButton.Checked = (Navigator.SelectedPage == page);

                // Update the button orientation
                checkButton.Orientation = checkOverflowOrient;

                // Should this check button be selected
                checkButton.HasFocus = (HasFocus && (Navigator.SelectedPage == page));

                // Add to end of the collection
                _viewOverflowLayout.Insert(overflowInsertIndex++, checkButton);
                _viewOverflowLayout.SetDock(checkButton, ViewDockStyle.Right);
            }
        }

        /// <summary>
        /// Discover if there are more buttons that can be moved from the overflow to the stack areas.
        /// </summary>
        /// <returns>True if more are available; otherwise false.</returns>
        protected virtual bool AreMoreButtons()
        {
            // Is there a visible overflow button that can be placed onto the stack?
            foreach (ViewBase child in _viewOverflowLayout)
                if (child.Visible && (child is ViewDrawNavOutlookOverflow))
                    return true;

            return false;
        }

        /// <summary>
        /// Gets the next overflow button to be moved to the stack area.
        /// </summary>
        /// <returns>Reference to button; otherwise false.</returns>
        protected virtual ViewDrawNavOutlookOverflow GetMoreOverflow()
        {
            // Find the first visible button on the overflow bar
            foreach (ViewBase child in _viewOverflowLayout)
                if (child.Visible && (child is ViewDrawNavOutlookOverflow))
                    return (ViewDrawNavOutlookOverflow)child;

            return null;
        }

        /// <summary>
        /// Allow operations to occur after main construct actions.
        /// </summary>
        protected virtual void PostConstruct()
        {
            // Need to monitor changes in the page collection to reflect in layout
            Navigator.Pages.Inserted += new TypedHandler<KryptonPage>(OnPageInserted);
            Navigator.Pages.Removed += new TypedHandler<KryptonPage>(OnPageRemoved);
            Navigator.Pages.Cleared += new EventHandler(OnPagesCleared);
            _events = true;

            // Ask the header group to finish the create phase
            _headerGroup.PostCreate();

            UpdateStatePalettes();
        }

        /// <summary>
        /// Bring the specified page into view within the viewport.
        /// </summary>
        /// <param name="page">Page to bring into view.</param>
        protected virtual void BringPageIntoView(KryptonPage page)
        {
            // Do nothing by default
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

            switch (e.PropertyName)
            {
                case "HeaderStyleOverflow":
                    UpdateStatePalettes();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "BorderEdgeStyleOutlook":
                    Navigator.StateCommon.BorderEdgeStyle = Navigator.Outlook.BorderEdgeStyle;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "CheckButtonStyleOutlook":
                    UpdateCheckButtonStyle();
                    ReorderCheckButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "OverflowButtonStyleOutlook":
                    _specDropDown.Style = CommonHelper.ButtonStyleToPalette(Navigator.Outlook.OverflowButtonStyle);
                    UpdateOverflowButtonStyle();
                    ReorderCheckButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "MiniButtonStyleOutlook":
                    UpdateMiniButtonStyle();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "OrientationOutlook":
                    // We only use minimum values if not calculating based on auto sizing
                    _oldRoot.SetMinimumAsPreferred(!Navigator.AutoSize);
                    _specDropDown.Orientation = (Navigator.Outlook.Orientation == Orientation.Vertical ? PaletteButtonOrientation.FixedTop : PaletteButtonOrientation.FixedLeft);
                    _viewOverflowBar.Orientation = (Navigator.Outlook.Orientation == Orientation.Vertical ? VisualOrientation.Top : VisualOrientation.Left);
                    _viewOverflowLayout.Orientation = (Navigator.Outlook.Orientation == Orientation.Vertical ? VisualOrientation.Top : VisualOrientation.Left);
                    _viewHeaderGroup.SetDock(_viewOverflowBar, (Navigator.Outlook.Orientation == Orientation.Vertical ? ViewDockStyle.Bottom : ViewDockStyle.Right));
                    ReorderCheckButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "ItemOrientationOutlook":
                    ReorderCheckButtons();
                    Navigator.PerformNeedPaint(true);
                    break;
                case "ShowDropDownButtonOutlook":
                    _specDropDown.Visible = Navigator.Outlook.ShowDropDownButton;
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

        /// <summary>
        /// Updates the item that has the focus.
        /// </summary>
        protected virtual void UpdateSelectedPageFocus()
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

                // We should have an overflow view for the page
                if (_pageOverflowLookup.ContainsKey(Navigator.SelectedPage))
                {
                    // Get the associated view element for the page
                    ViewDrawNavCheckButtonBase checkButton = _pageOverflowLookup[Navigator.SelectedPage];

                    // Reflect focus is in the check button
                    checkButton.HasFocus = HasFocus;

                    // Need to repaint to show the change
                    Navigator.PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets the first page that can be selected.
        /// </summary>
        /// <returns>Page for selection; otherwise null.</returns>
        protected KryptonPage FirstOutlookActionPage()
        {
            // Scan the pages in the stack
            foreach (ViewBase item in _viewLayout)
                if (item is ViewDrawNavOutlookStack)
                {
                    ViewDrawNavOutlookStack checkButton = (ViewDrawNavOutlookStack)item;
                    if (checkButton.Visible &&
                        checkButton.Page.LastVisibleSet &&
                        checkButton.Page.Enabled)
                        return checkButton.Page;
                }

            // Scan the pages in the overflow
            foreach (ViewBase item in _viewOverflowLayout)
                if (item is ViewDrawNavOutlookOverflow)
                {
                    ViewDrawNavOutlookOverflow checkButton = (ViewDrawNavOutlookOverflow)item;
                    if (checkButton.Visible &&
                        checkButton.Page.LastVisibleSet &&
                        checkButton.Page.Enabled)
                        return checkButton.Page;
                }

            // Cannot find a page that is possible to be selected
            return null;
        }

        /// <summary>
        /// Gets the last page that can be selected.
        /// </summary>
        /// <returns>Page for selection; otherwise null.</returns>
        protected KryptonPage LastOutlookActionPage()
        {
            // Scan the pages in the overflow (in reverse order)
            foreach (ViewBase item in _viewOverflowLayout.Reverse())
                if (item is ViewDrawNavOutlookOverflow)
                {
                    ViewDrawNavOutlookOverflow checkButton = (ViewDrawNavOutlookOverflow)item;
                    if (checkButton.Visible &&
                        checkButton.Page.LastVisibleSet &&
                        checkButton.Page.Enabled)
                        return checkButton.Page;
                }

            // Scan the pages in the stack (in reverse order)
            foreach (ViewBase item in _viewLayout.Reverse())
                if (item is ViewDrawNavOutlookStack)
                {
                    ViewDrawNavOutlookStack checkButton = (ViewDrawNavOutlookStack)item;
                    if (checkButton.Visible &&
                        checkButton.Page.LastVisibleSet &&
                        checkButton.Page.Enabled)
                        return checkButton.Page;
                }

            // Cannot find a page that is possible to be selected
            return null;
        }

        /// <summary>
        /// Find the next outlook action page based on a provided current page.
        /// </summary>
        /// <param name="page">Current page to work from.</param>
        /// <returns>New page that should be selected.</returns>
        protected KryptonPage NextOutlookActionPage(KryptonPage page)
        {
            bool found = false;

            // Scan the pages in the stack
            foreach (ViewBase item in _viewLayout)
                if (item is ViewDrawNavOutlookStack)
                {
                    ViewDrawNavOutlookStack checkButton = (ViewDrawNavOutlookStack)item;

                    // Only interested in visible check buttons
                    if (checkButton.Visible)
                    {
                        // If still looking for the provided page then check if this is it
                        if (!found)
                            found = (checkButton.Page == page);
                        else
                        {
                            // Already found the provided page, is this one capable of being selected?
                            if (checkButton.Page.LastVisibleSet && checkButton.Page.Enabled)
                                return checkButton.Page;
                        }
                    }
                }

            // Scan the pages in the overflow
            foreach (ViewBase item in _viewOverflowLayout)
                if (item is ViewDrawNavOutlookOverflow)
                {
                    ViewDrawNavOutlookOverflow checkButton = (ViewDrawNavOutlookOverflow)item;

                    // Only interested in visible check buttons
                    if (checkButton.Visible)
                    {
                        // If still looking for the provided page then check if this is it
                        if (!found)
                            found = (checkButton.Page == page);
                        else
                        {
                            // Already found the provided page, is this one capable of being selected?
                            if (checkButton.Page.LastVisibleSet && checkButton.Page.Enabled)
                                return checkButton.Page;
                        }
                    }
                }

            // Cannot find a page after the provided one that is selectable
            return null;
        }

        /// <summary>
        /// Find the previous outlook action page based on a provided current page.
        /// </summary>
        /// <param name="page">Current page to work from.</param>
        /// <returns>New page that should be selected.</returns>
        protected KryptonPage PreviousOutlookActionPage(KryptonPage page)
        {
            bool found = false;

            // Scan the pages in the overflow
            foreach (ViewBase item in _viewOverflowLayout.Reverse())
                if (item is ViewDrawNavOutlookOverflow)
                {
                    ViewDrawNavOutlookOverflow checkButton = (ViewDrawNavOutlookOverflow)item;

                    // Only interested in visible check buttons
                    if (checkButton.Visible)
                    {
                        // If still looking for the provided page then check if this is it
                        if (!found)
                            found = (checkButton.Page == page);
                        else
                        {
                            // Already found the provided page, is this one capable of being selected?
                            if (checkButton.Page.LastVisibleSet && checkButton.Page.Enabled)
                                return checkButton.Page;
                        }
                    }
                }

            // Scan the pages in the stack
            foreach (ViewBase item in _viewLayout.Reverse())
                if (item is ViewDrawNavOutlookStack)
                {
                    ViewDrawNavOutlookStack checkButton = (ViewDrawNavOutlookStack)item;

                    // Only interested in visible check buttons
                    if (checkButton.Visible)
                    {
                        // If still looking for the provided page then check if this is it
                        if (!found)
                            found = (checkButton.Page == page);
                        else
                        {
                            // Already found the provided page, is this one capable of being selected?
                            if (checkButton.Page.LastVisibleSet && checkButton.Page.Enabled)
                                return checkButton.Page;
                        }
                    }
                }

            // Cannot find a page before the provided one that is selectable
            return null;
        }
        #endregion

		#region Implementation
        private void CreateStackItems()
        {
            // Maintain lookup between page and stack check button/button edge that represent it
            _pageStackLookup = new PageToNavCheckButton();
            _buttonEdgeLookup = new PageToButtonEdge();

            VisualOrientation checkButtonOrient = ResolveStackButtonOrientation();
            Orientation stackOrient = Navigator.Outlook.Orientation;
            Orientation buttonEdgeOrient = (stackOrient == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical);
            ViewDockStyle dockFar = (stackOrient == Orientation.Vertical ? ViewDockStyle.Bottom : ViewDockStyle.Right);

            // Cache the border edge palette to use
            PaletteBorderEdge buttonEdgePalette = (Navigator.Enabled ? Navigator.StateNormal.BorderEdge : 
                                                                       Navigator.StateDisabled.BorderEdge);

            // Create the separator and its edge view
            _viewSeparatorEdge = new ViewDrawBorderEdge(Navigator.StateNormal.BorderEdge, buttonEdgeOrient);
            _viewSeparator = new ViewDrawSeparator(Navigator.StateDisabled.Separator, Navigator.StateNormal.Separator,
                                                   Navigator.StateTracking.Separator, Navigator.StatePressed.Separator,
                                                   Navigator.StateDisabled.Separator, Navigator.StateNormal.Separator,
                                                   Navigator.StateTracking.Separator, Navigator.StatePressed.Separator,
                                                   PaletteMetricPadding.SeparatorPaddingHighInternalProfile, buttonEdgeOrient);

            // Fix the length of the separator
            _viewSeparator.Length = _separatorLength;

            // Add to the end of the collection
            _viewLayout.Add(_viewSeparatorEdge, dockFar);
            _viewLayout.Add(_viewSeparator, dockFar);

            // Create a check button to represent each krypton page
            foreach (KryptonPage page in Navigator.Pages)
            {
                // Create the draw view element for the check button and provide page it represents
                ViewDrawNavOutlookStack checkButton = new ViewDrawNavOutlookStack(Navigator, page, checkButtonOrient);

                // Provide the drag rectangle when requested for this button
                checkButton.ButtonDragRectangle += new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkButton.ButtonDragOffset += new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);

                // Need to know when check button needs repainting
                checkButton.NeedPaint = NeedPaintDelegate;

                // Can we show the page as a stacking item?
                bool showPage = page.LastVisibleSet && !page.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode);

                // Set the initial state
                checkButton.Visible = showPage;
                checkButton.Enabled = page.Enabled;
                checkButton.Checked = (Navigator.SelectedPage == page);

                // Create the border edge for use next to the check button
                ViewDrawBorderEdge buttonEdge = new ViewDrawBorderEdge(buttonEdgePalette, buttonEdgeOrient);
                buttonEdge.Visible = showPage;

                // Add to lookup dictionary
                _pageStackLookup.Add(page, checkButton);
                _buttonEdgeLookup.Add(page, buttonEdge);

                // Add to the end of the collection
                _viewLayout.Add(buttonEdge, dockFar);
                _viewLayout.Add(checkButton, dockFar);
            }
        }

        private void CreateOverflowItems()
        {
            // Maintain lookup between page and overflow check button that represents it
            _pageOverflowLookup = new PageToNavCheckButton();

            VisualOrientation checkButtonOrient = ResolveOverflowButtonOrientation();
            ViewDockStyle dockFar = ViewDockStyle.Right;

            // Create a check button to represent each krypton page
            foreach (KryptonPage page in Navigator.Pages)
                CreateOverflowItem(page, checkButtonOrient, dockFar);
        }

        private void CreateSeparatorController()
        {
            // Create a separator controller to handle separator style behaviour
            _separatorController = new SeparatorController(this, _viewSeparator, false, false, NeedPaintDelegate);

            // Assign the controller to the view element to treat as a separator
            _viewSeparator.MouseController = _separatorController;
            _viewSeparator.KeyController = _separatorController;
            _viewSeparator.SourceController = _separatorController;
        }

        private void CreateButtonManager()
        {
            // Create a collection to hold button spec we want to display
            _buttons = new OutlookButtonSpecCollection(Navigator);

            // Create out drop down button specification
            _specDropDown = new ButtonSpecAny();
            _specDropDown.Type = PaletteButtonSpecStyle.DropDown;
            _specDropDown.Style = CommonHelper.ButtonStyleToPalette(Navigator.Outlook.OverflowButtonStyle);
            _specDropDown.Click += new EventHandler(OnDropDownClick);
            _specDropDown.Orientation = (Navigator.Outlook.Orientation == Orientation.Vertical ? PaletteButtonOrientation.FixedTop : PaletteButtonOrientation.FixedLeft);
            _specDropDown.Visible = Navigator.Outlook.ShowDropDownButton;

            // Add into the collection for display
            _buttons.Add(_specDropDown);

            // Create button specification collection manager
            _buttonManager = new ButtonSpecNavManagerLayoutBar(Navigator, Redirector, _buttons,
                                                               new ViewLayoutDocker[] { _viewOverflowLayout },
                                                               new IPaletteMetric[] { Navigator.StateCommon.Bar },
                                                               new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetInputControl },
                                                               new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetInputControl },
                                                               new PaletteMetricPadding[] { PaletteMetricPadding.None },
                                                               new GetToolStripRenderer(Navigator.CreateToolStripRenderer),
                                                               NeedPaintDelegate);
        }

        private void DestructStackCheckButtons()
        {
            // Remove the fixed separator views from the parent container
            _viewLayout.Remove(_viewSeparatorEdge);
            _viewLayout.Remove(_viewSeparator);

            // Dispose of the removed view elements
            _viewSeparatorEdge.Dispose();
            _viewSeparator.Dispose();

            // Must tell each check button it is no longer required
            foreach (ViewDrawNavCheckButtonBase checkButton in _pageStackLookup.Values)
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
            _pageStackLookup.Clear();
            _buttonEdgeLookup.Clear();
        }

        private void DestructOverflowCheckButtons()
        {
            // Must tell each check button it is no longer required
            foreach (ViewDrawNavCheckButtonBase checkButton in _pageOverflowLookup.Values)
            {
                // Must unhook from events
                checkButton.NeedPaint = null;

                // Dispose of element gracefully
                checkButton.Dispose();

                // Remove it from the group view
                _viewHeaderGroup.Remove(checkButton);
            }

            // Remove all associations from the lookup dictionary
            _pageOverflowLookup.Clear();
        }

        private void UpdateCheckButtonStyle()
        {
            Navigator.StateCommon.CheckButton.SetStyles(Navigator.Outlook.CheckButtonStyle);
            Navigator.OverrideFocus.CheckButton.SetStyles(Navigator.Outlook.CheckButtonStyle);

            // Update each individual button with the new style for remapping page level button specs
            if (_pageStackLookup != null)
                foreach (KeyValuePair<KryptonPage, ViewDrawNavCheckButtonBase> pair in _pageStackLookup)
                    if (pair.Value.ButtonSpecManager != null)
                        pair.Value.ButtonSpecManager.SetRemapTarget(Navigator.Outlook.CheckButtonStyle);
        }

        private void UpdateOverflowButtonStyle()
        {
            Navigator.StateCommon.OverflowButton.SetStyles(Navigator.Outlook.OverflowButtonStyle);
            Navigator.OverrideFocus.OverflowButton.SetStyles(Navigator.Outlook.OverflowButtonStyle);
        }

        private void UpdateMiniButtonStyle()
        {
            Navigator.StateCommon.MiniButton.SetStyles(Navigator.Outlook.Mini.MiniButtonStyle);
            Navigator.OverrideFocus.MiniButton.SetStyles(Navigator.Outlook.Mini.MiniButtonStyle);
        }

        private void OnPageInserted(object sender, TypedCollectionEventArgs<KryptonPage> e)
        {
            if (!Navigator.IsDisposed && _events)
            {
                // Create the view elements for the page
                ViewDrawNavOutlookStack checkButtonStack = new ViewDrawNavOutlookStack(Navigator, e.Item, ResolveStackButtonOrientation());
                ViewDrawNavOutlookOverflow checkButtonOverflow = new ViewDrawNavOutlookOverflow(Navigator, e.Item, ResolveOverflowButtonOrientation());

                // Provide the drag rectangle when requested for this button
                checkButtonStack.ButtonDragRectangle += new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkButtonStack.ButtonDragOffset += new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);

                // Need to know when check buttons need repainting
                checkButtonStack.NeedPaint = NeedPaintDelegate;
                checkButtonOverflow.NeedPaint = NeedPaintDelegate;

                // Can we show the page as a stacking item?
                bool showPageStack = e.Item.LastVisibleSet && !e.Item.AreFlagsSet(KryptonPageFlags.PageInOverflowBarForOutlookMode);
                bool showPageOverflow = e.Item.LastVisibleSet && !showPageStack;

                // Set the initial state
                checkButtonStack.Visible = showPageStack;
                checkButtonOverflow.Visible = showPageOverflow;
                checkButtonStack.Enabled = e.Item.Enabled;
                checkButtonOverflow.Enabled = e.Item.Enabled;
                checkButtonStack.Checked = (Navigator.SelectedPage == e.Item);
                checkButtonOverflow.Checked = (Navigator.SelectedPage == e.Item);

                // Find the border edge palette to use
                PaletteBorderEdge buttonEdgePalette = (Navigator.Enabled ? Navigator.StateNormal.BorderEdge :
                                                                           Navigator.StateDisabled.BorderEdge);

                // Create the border edge for use next to the check button
                ViewDrawBorderEdge buttonEdge = new ViewDrawBorderEdge(buttonEdgePalette, Navigator.Outlook.Orientation);
                buttonEdge.Visible = showPageStack;

                // Add to lookup dictionary
                _pageStackLookup.Add(e.Item, checkButtonStack);
                _pageOverflowLookup.Add(e.Item, checkButtonOverflow);
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
                ViewDrawNavCheckButtonBase checkButtonStack = _pageStackLookup[e.Item];
                ViewDrawNavCheckButtonBase checkButtonOverflow = _pageOverflowLookup[e.Item];
                ViewDrawBorderEdge buttonEdge = _buttonEdgeLookup[e.Item];

                // Remove event hooks
                checkButtonStack.ButtonDragRectangle -= new EventHandler<ButtonDragRectangleEventArgs>(OnCheckButtonDragRect);
                checkButtonStack.ButtonDragOffset -= new EventHandler<ButtonDragOffsetEventArgs>(OnCheckButtonDragOffset);

                // Remove the paint delegate so objects can be garbage collected
                checkButtonStack.NeedPaint = null;
                checkButtonOverflow.NeedPaint = null;

                // Remove the overflow entry, if it exists
                if (_viewOverflowLayout.Contains(checkButtonOverflow))
                    _viewOverflowLayout.Remove(checkButtonOverflow);

                // Remove the stack entry
                _viewLayout.Remove(checkButtonStack);
                _viewLayout.Remove(buttonEdge);

                // Tell the views they are no longer required
                checkButtonStack.Dispose();
                checkButtonOverflow.Dispose();
                buttonEdge.Dispose();

                // Remove associations from the lookup dictionarys
                _pageStackLookup.Remove(e.Item);
                _pageOverflowLookup.Remove(e.Item);
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
                DestructStackCheckButtons();
                DestructOverflowCheckButtons();

                // Set correct ordering and dock setting
                ReorderCheckButtons();

                // Need to repaint to show the change
                Navigator.PerformNeedPaint(true);
            }
        }

        private void ReorderCheckButtons()
        {
            // Clear out the stacking items
            _viewLayout.Clear();

            // Clear out the overflow items not part of the button manager
            for (int i = _viewOverflowLayout.Count - 1; i >= 0; i--)
                if (_viewOverflowLayout[i] is ViewDrawNavOutlookOverflow)
                    _viewOverflowLayout.RemoveAt(i);

            // Start inserting at start of the list
            int overflowInsertIndex = 0;

            // Always add the filler as the first item
            SetLayoutFiller(_viewLayout);

            VisualOrientation checkStackOrient = ResolveStackButtonOrientation();
            VisualOrientation checkOverflowOrient = ResolveOverflowButtonOrientation();
            Orientation stackOrient = Navigator.Outlook.Orientation;
            Orientation buttonEdgeOrient = (stackOrient == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical);
            ViewDockStyle dockFar = (stackOrient == Orientation.Vertical ? ViewDockStyle.Bottom : ViewDockStyle.Right);

            // Update the separator/drop down buttons with latest values
            _viewSeparatorEdge.Orientation = buttonEdgeOrient;
            _viewSeparator.Orientation = buttonEdgeOrient;

            // Add to the end of the collection
            _viewLayout.Add(_viewSeparatorEdge, dockFar);
            _viewLayout.Add(_viewSeparator, dockFar);

            // Add back the pages in the order they are in collection
            foreach (KryptonPage page in Navigator.Pages)
            {
                // Check that a stacking view element exists for the page
                if (_pageStackLookup.ContainsKey(page))
                {
                    // Get the associated view elements
                    ViewDrawNavCheckButtonBase checkButton = _pageStackLookup[page];
                    ViewDrawBorderEdge buttonEdge = _buttonEdgeLookup[page];

                    // Update checked state of the button
                    checkButton.Checked = (Navigator.SelectedPage == page);

                    // Update the button orientation
                    checkButton.Orientation = checkStackOrient;

                    // Should this check button be selected
                    checkButton.HasFocus = (HasFocus && (Navigator.SelectedPage == page));

                    // The button edge is the opposite of the stack orientation
                    buttonEdge.Orientation = buttonEdgeOrient;

                    // Add to end of the collection
                    _viewLayout.Add(buttonEdge, dockFar);
                    _viewLayout.Add(checkButton, dockFar);
                }

                ReorderCheckButtonsOverflow(page, checkOverflowOrient, ref overflowInsertIndex);
            }
        }

        private VisualOrientation ResolveStackButtonOrientation()
        {
            switch (Navigator.Outlook.ItemOrientation)
            {
                case ButtonOrientation.Auto:
                    if (Navigator.Outlook.Orientation == Orientation.Vertical)
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

        private VisualOrientation ResolveOverflowButtonOrientation()
        {
            if (Navigator.Outlook.Orientation == Orientation.Vertical)
                return VisualOrientation.Top;
            else
                return VisualOrientation.Left;
        }

        private ViewDrawNavCheckButtonBase GetShrinkStackItem()
        {
            // If there is a visible stack item, then we can always shrink it away
            foreach (ViewDrawNavCheckButtonBase checkButton in _pageStackLookup.Values)
                if (checkButton.Visible)
                    return checkButton;

            return null;
        }

        private ViewDrawNavCheckButtonBase GetExpandOverflowItem()
        {
            // If there is a overflow stack item, then we can always show it
            foreach (ViewDrawNavCheckButtonBase checkButton in _pageOverflowLookup.Values)
                if (checkButton.Visible)
                    return checkButton;

            return null;
        }

        private bool PageInTheStack(KryptonPage page)
        {
            // Get the overflow check button for the page
            ViewDrawNavOutlookOverflow checkButton = (ViewDrawNavOutlookOverflow)_pageOverflowLookup[page];

            // If not visible on the overflow bar then must be in the stack
            return !checkButton.Visible;
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

        private bool AreFewerButtons()
        {
            // Is there a stack button that can be placed onto the overflow bar instead?
            foreach (ViewBase child in _viewLayout)
                if (child.Visible && (child is ViewDrawNavOutlookStack))
                    return true;

            return false;
        }

        private void OnDropDownClick(object sender, EventArgs e)
        {
            if (!Navigator.InternalDesignMode)
            {
                // Ensure cached context menu is ready for use
                ResetCachedKryptonContextMenu();

                // Add the three standard entries
                KryptonContextMenuItem moreButtons = new KryptonContextMenuItem(Navigator.Outlook.TextMoreButtons, _moreButtons, new EventHandler(OnShowMoreClick));
                KryptonContextMenuItem fewerButtons = new KryptonContextMenuItem(Navigator.Outlook.TextFewerButtons, _fewerButtons, new EventHandler(OnShowFewerClick));
                KryptonContextMenuItem addRemoveButtons = new KryptonContextMenuItem(Navigator.Outlook.TextAddRemoveButtons);
                KryptonContextMenuItems addRemoveButtonItems = new KryptonContextMenuItems();
                addRemoveButtons.Items.Add(addRemoveButtonItems);

                // Setup the transparent color for the images
                moreButtons.ImageTransparentColor = Color.Magenta;
                fewerButtons.ImageTransparentColor = Color.Magenta;

                // Decide if the more/fewer buttons should be enabled/disabled
                moreButtons.Enabled = AreMoreButtons();
                fewerButtons.Enabled = AreFewerButtons();

                // Add items into the context menu
                _kcm.Items.Add(new KryptonContextMenuItems(new KryptonContextMenuItemBase[] { moreButtons, fewerButtons, addRemoveButtons }));

                // Add each page into the 'Add/Remove' item
                int visibleAddRemove = 0;
                foreach (KryptonPage page in Navigator.Pages)
                {
                    // Create a menu item for the page
                    KryptonContextMenuItem pageMenuItem = new KryptonContextMenuItem(page.GetTextMapping(Navigator.Button.ContextMenuMapText),
                                                                                     page.GetImageMapping(Navigator.Button.ContextMenuMapImage),
                                                                                     new EventHandler(OnPageAddRemoveClick));

                    // The selected page should be checked
                    pageMenuItem.Checked = page.Visible;
                    if (page.Visible)
                        visibleAddRemove++;

                    // Use tag to store a back reference to the page
                    pageMenuItem.Tag = page;

                    // Add to end of the strip
                    addRemoveButtonItems.Items.Add(pageMenuItem);
                }

                // Only enable the 'Add/Remove' if it has at least one drop down item
                addRemoveButtons.Enabled = (visibleAddRemove > 0);

                // Get the display rectange of the drop down button
                Rectangle rect = _buttonManager.GetButtonRectangle(_specDropDown);

                // Convert to screen coordinates
                Point pt = Navigator.PointToScreen(new Point(rect.Right + 3, rect.Top));

                // Let user modify the context menu before it is shown
                Navigator.OnOutlookDropDown(_kcm);

                // Give user a chance to select an item
                _kcm.Show(pt);
            }
        }

        private void OnShowMoreClick(object sender, EventArgs e)
        {
            // Cast to correct type
            ViewDrawNavOutlookOverflow checkButton = GetMoreOverflow();

            if (checkButton != null)
            {
                // Search for the page that is represented by this check button
                foreach (KryptonPage page in _pageOverflowLookup.Keys)
                    if (_pageOverflowLookup[page] == checkButton)
                    {
                        // Remove the flag that places this page on the overflow bar
                        page.ClearFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);

                        // Need layout to make change occur
                        PerformNeedPaint(true);
                        return;
                    }
            }
        }

        private void OnShowFewerClick(object sender, EventArgs e)
        {
            // Find the last visible button on the stack bar
            foreach (ViewBase child in _viewLayout.Reverse())
                if (child.Visible && (child is ViewDrawNavOutlookStack))
                {
                    // Cast to correct type
                    ViewDrawNavOutlookStack checkButton = (ViewDrawNavOutlookStack)child;

                    // Search for the page that is represented by this check button
                    foreach (KryptonPage page in _pageStackLookup.Keys)
                        if (_pageStackLookup[page] == checkButton)
                        {
                            // Set the flag that places this page on the overflow bar
                            page.SetFlags(KryptonPageFlags.PageInOverflowBarForOutlookMode);
                            return;
                        }
                }

            // Need layout to make change occur
            PerformNeedPaint(true);
        }

        private void OnPageAddRemoveClick(object sender, EventArgs e)
        {
            // Cast to correct type
            KryptonContextMenuItem menuItem = (KryptonContextMenuItem)sender;

            // Get the page this menu item is associated with
            KryptonPage page = (KryptonPage)menuItem.Tag;

            // Toggle the visible state of the page
            page.Visible = !page.Visible;
        }

        private void ResetCachedKryptonContextMenu()
        {
            // First time around we need to create the context menu
            if (_kcm == null)
                _kcm = new KryptonContextMenu();

            // Remove any existing items
            _kcm.Items.Clear();
        }

        private void OnCheckButtonDragRect(object sender, ButtonDragRectangleEventArgs e)
        {
            // Cast incoming reference to the actual check button view
            ViewDrawNavOutlookStack reorderItem = (ViewDrawNavOutlookStack)sender;

            e.PreDragOffset = (Navigator.AllowPageReorder && reorderItem.Page.AreFlagsSet(KryptonPageFlags.AllowPageReorder));
            Rectangle dragRect = Rectangle.Union(e.DragRect, _viewLayout.ClientRectangle);
            dragRect.Inflate(new Size(10, 10));
            e.DragRect = dragRect;

            // Constrain by the position of the separator (depends on the orientation)
            if (SeparatorOrientation == Orientation.Horizontal)
            {
                int reduce = _viewSeparator.ClientRectangle.Bottom - e.DragRect.Y;
                e.DragRect = new Rectangle(e.DragRect.X, e.DragRect.Y + reduce, e.DragRect.Width, e.DragRect.Height - reduce);
            }
            else
            {
                int reduce = _viewSeparator.ClientRectangle.Right - e.DragRect.X;
                e.DragRect = new Rectangle(e.DragRect.X + reduce, e.DragRect.Y, e.DragRect.Width - reduce, e.DragRect.Height);
            }
        }

        private void OnCheckButtonDragOffset(object sender, ButtonDragOffsetEventArgs e)
        {
            // Cast incoming reference to the actual check button view
            ViewDrawNavOutlookStack reorderView = (ViewDrawNavOutlookStack)sender;

            // Scan the collection of children
            bool foundReorderView = false;
            foreach (KryptonPage page in Navigator.Pages)
            {
                // If the mouse is over this button
                ViewDrawNavOutlookStack childView = (ViewDrawNavOutlookStack)_pageStackLookup[page];
                if (childView.Visible && childView.ClientRectangle.Contains(e.PointOffset))
                {
                    // Only interested if mouse over a different check button
                    if (childView != reorderView)
                    {
                        Rectangle childRect = childView.ClientRectangle;

                        if (foundReorderView)
                        {
                            if (SeparatorOrientation == Orientation.Horizontal)
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
                            if (SeparatorOrientation == Orientation.Horizontal)
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
            VisualOrientation checkButtonOrient = ResolveStackButtonOrientation();
            Orientation stackOrient = Navigator.Outlook.Orientation;
            Orientation buttonEdgeOrient = (stackOrient == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical);
            ViewDockStyle dockFar = (stackOrient == Orientation.Vertical ? ViewDockStyle.Bottom : ViewDockStyle.Right);

            // Remove all existing layout items
            _viewLayout.Clear();

            // Add to the end of the collection
            _viewLayout.Add(_viewSeparatorEdge, dockFar);
            _viewLayout.Add(_viewSeparator, dockFar);

            foreach (KryptonPage page in Navigator.Pages)
            {
                // Grab the page associated view elements
                ViewDrawNavOutlookStack checkButton = (ViewDrawNavOutlookStack)_pageStackLookup[page];
                ViewDrawBorderEdge buttonEdge = (ViewDrawBorderEdge)_buttonEdgeLookup[page];
                
                // Add to the end of the collection
                _viewLayout.Add(buttonEdge, dockFar);
                _viewLayout.Add(checkButton, dockFar);
            }
        }
        #endregion
    }
}
