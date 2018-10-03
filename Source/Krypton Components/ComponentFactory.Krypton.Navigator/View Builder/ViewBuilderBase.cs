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
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Base class for view builder implementations.
	/// </summary>
	internal abstract class ViewBuilderBase
	{
        #region Type Definitons
        protected class PageToNavCheckItem : Dictionary<KryptonPage, INavCheckItem> { };
        protected class PageToNavCheckButton : Dictionary<KryptonPage, ViewDrawNavCheckButtonBase> { };
        #endregion

        #region Instance Fields
        private bool _constructed;
		private ViewManager _manager;
		private KryptonNavigator _navigator;
		private PaletteRedirect _redirector;
        private NeedPaintHandler _needPaintDelegate;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the ViewBuilderBase class.
		/// </summary>
        public ViewBuilderBase()
		{
			_constructed = false;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets access to the navigator instance.
		/// </summary>
		public KryptonNavigator Navigator
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _navigator; }
		}

		/// <summary>
		/// Gets access to the view manager instance.
		/// </summary>
		public ViewManager ViewManager
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _manager; }
		}

		/// <summary>
		/// Gets the palette redirector.
		/// </summary>
		public PaletteRedirect Redirector
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _redirector; }
		}

        /// <summary>
        /// Gets a value indicating if the mode is a tab strip style mode.
        /// </summary>
        public abstract bool IsTabStripMode { get; }
        #endregion

		#region Methods
		/// <summary>
		/// Construct the view appropriate for this builder.
		/// </summary>
		/// <param name="navigator">Reference to navigator instance.</param>
		/// <param name="manager">Reference to current manager.</param>
		/// <param name="redirector">Palette redirector.</param>
		public virtual void Construct(KryptonNavigator navigator, 
									  ViewManager manager,
									  PaletteRedirect redirector)
		{
			Debug.Assert(navigator != null);
			Debug.Assert(manager != null);
			Debug.Assert(redirector != null);
			Debug.Assert(_constructed == false);

            // Save provided references
			_navigator = navigator;
			_manager = manager;
			_redirector = redirector;
			_constructed = true;

            // Hook into the navigator events
            _navigator.ViewBuilderPropertyChanged += new PropertyChangedEventHandler(OnViewBuilderPropertyChanged);
		}

        /// <summary>
        /// Destruct the previously created view.
        /// </summary>
        public virtual void Destruct()
        {
            Debug.Assert(_constructed);
            Debug.Assert(_navigator != null);

            // Unhook from the navigator events
            _navigator.ViewBuilderPropertyChanged -= new PropertyChangedEventHandler(OnViewBuilderPropertyChanged);

            // No longer constructed
            _constructed = false;

            // Change of mode means we get rid of any showing popup page
            _navigator.DismissPopups();
        }

        /// <summary>
        /// Process a change in the selected page
        /// </summary>
        public virtual void SelectedPageChanged()
        {
        }

        /// <summary>
        /// Change has occured to the collection of pages.
        /// </summary>
        public virtual void PageCollectionChanged()
        {
        }

        /// <summary>
        /// Process a change in the visible state for a page.
        /// </summary>
        /// <param name="page">Page that has changed visible state.</param>
        public virtual void PageVisibleStateChanged(KryptonPage page)
        {
        }

        /// <summary>
        /// Process a change in the enabled state for a page.
        /// </summary>
        /// <param name="page">Page that has changed enabled state.</param>
        public virtual void PageEnabledStateChanged(KryptonPage page)
        {
        }

        /// <summary>
        /// Notification that a krypton page appearance property has changed.
        /// </summary>
        /// <param name="page">Page that has changed.</param>
        /// <param name="property">Name of property that has changed.</param>
        public virtual void PageAppearanceChanged(KryptonPage page, string property)
        {
        }

        /// <summary>
        /// Notification that krypton page flags have changed.
        /// </summary>
        /// <param name="page">Page that has changed.</param>
        /// <param name="changed">Set of flags that have changed value.</param>
        public virtual void PageFlagsChanged(KryptonPage page, KryptonPageFlags changed)
        {
        }

        /// <summary>
        /// Gets the KryptonPage associated with the provided view element.
        /// </summary>
        /// <param name="element">Element to search against.</param>
        /// <returns>Reference to KryptonPage; otherwise null.</returns>
        public abstract KryptonPage PageFromView(ViewBase element);

        /// <summary>
        /// Gets the ButtonSpec associated with the provided view element.
        /// </summary>
        /// <param name="element">Element to search against.</param>
        /// <returns>Reference to ButtonSpec; otherwise null.</returns>
        public abstract ButtonSpec ButtonSpecFromView(ViewBase element);

        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public virtual void UpdateStatePalettes()
        {
        }

        /// <summary>
        /// Gets the screen coorindates for showing a context action menu.
        /// </summary>
        /// <returns>Point in screen coordinates.</returns>
        public virtual Point GetContextShowPoint()
        {
            // Default to using the current mouse position
            Point pt = Control.MousePosition;

            // Show the context menu just below the mouse cursor
            return new Point(pt.X, pt.Y + 18);
        }

        /// <summary>
        /// Is the provided point over a part of the view that wants the mouse.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        /// <returns>True if the view wants the mouse position; otherwise false.</returns>
        public virtual bool DesignerGetHitTest(Point pt)
        {
            return false;
        }

        /// <summary>
        /// Gets the appropriate display location for the button.
        /// </summary>
        /// <param name="buttonSpec">ButtonSpec instance.</param>
        /// <returns>HeaderLocation value.</returns>
        public virtual HeaderLocation GetFixedButtonLocation(ButtonSpecNavFixed buttonSpec)
        {
            return buttonSpec.HeaderLocation;
        }

        /// <summary>
        /// Calculate the enabled state of the next button based on the required action.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <returns>Enabled state of the button.</returns>
        public virtual ButtonEnabled NextActionEnabled(DirectionButtonAction action)
        {
            // Process the requested action
            switch (action)
            {
                case DirectionButtonAction.None:
                case DirectionButtonAction.SelectPage:
                    // Only enabled if the count of visible pages to the left of current page is positive
                    return (Navigator.NextActionValid ? ButtonEnabled.True : ButtonEnabled.False);
                default:
                    // Action not supported so disable button
                    return ButtonEnabled.False;
            }
        }

        /// <summary>
        /// Peform the next button action requested.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <param name="page">Selected page at time of action request.</param>
        public virtual void PerformNextAction(DirectionButtonAction action, KryptonPage page)
        {
            // Process the requested action
            switch (action)
            {
                case DirectionButtonAction.None:
                    // Do nothing
                    break;
                case DirectionButtonAction.SelectPage:
                    // Select the page after the provided one
                    Navigator.SelectNextPage(page, false);
                    break;
            }
        }

        /// <summary>
        /// Calculate the enabled state of the previous button based on the required action.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <returns>Enabled state of the button.</returns>
        public virtual ButtonEnabled PreviousActionEnabled(DirectionButtonAction action)
        {
            // Process the requested action
            switch (action)
            {
                case DirectionButtonAction.None:
                case DirectionButtonAction.SelectPage:
                    // Only enabled if the count of visible pages to the left of current page is positive
                    return (Navigator.PreviousActionValid ? ButtonEnabled.True : ButtonEnabled.False);
                default:
                    // Action not supported so disable button
                    return ButtonEnabled.False;
            }
        }

        /// <summary>
        /// Peform the previous button action requested.
        /// </summary>
        /// <param name="action">Requested action.</param>
        /// <param name="page">Selected page at time of action request.</param>
        public virtual void PerformPreviousAction(DirectionButtonAction action, KryptonPage page)
        {
            // Process the requested action
            switch (action)
            {
                case DirectionButtonAction.None:
                    // Do nothing
                    break;
                case DirectionButtonAction.SelectPage:
                    // Select the page before the provided one
                    Navigator.SelectPreviousPage(page, false);
                    break;
            }
        }

        /// <summary>
        /// Perform pre layout operations.
        /// </summary>
        public virtual void PreLayout() { }

        /// <summary>
        /// Perform post layout operations.
        /// </summary>
        public virtual void PostLayout() { }

        /// <summary>
        /// Gets a value indicating if the view can accept the focus.
        /// </summary>
        public virtual bool CanFocus
        {
            get { return false; }
        }

        /// <summary>
        /// Occurs when the navigator takes the focus.
        /// </summary>
        public virtual void GotFocus() { }

        /// <summary>
        /// Occurs when the navigator loses the focus.
        /// </summary>
        public virtual void LostFocus() { }

        /// <summary>
        /// Should this element cause the navigator to gain the focus.
        /// </summary>
        /// <param name="element">Element that is being activated.</param>
        /// <returns>True to give navigator the focus; otherwise false.</returns>
        public virtual bool GiveNavigatorFocus(ViewBase element)
        {
            return false;
        }

        /// <summary>
        /// User has used the keyboard to select the currently selected page.
        /// </summary>
        public virtual void KeyPressedPageView()
        {
        }

        /// <summary>
        /// Gets the appropriate popup page position for the current mode.
        /// </summary>
        /// <returns>Calculated PopupPagePosition</returns>
        public virtual PopupPagePosition GetPopupPagePosition()
        {
            return PopupPagePosition.BelowNear;
        }

        /// <summary>
        /// Process a dialog key in a manner appropriate for the view.
        /// </summary>
        /// <param name="keyData">Key data.</param>
        /// <returns>True if the key eaten; otherwise false.</returns>
        public virtual bool ProcessDialogKey(Keys keyData)
        {
            return false;
        }

        /// <summary>
        /// Check the key data for a matching action button shortcut.
        /// </summary>
        /// <param name="keyData">Key data.</param>
        /// <returns>If if match found and key should be eaten; otherwise false.</returns>
        public bool CheckActionShortcuts(Keys keyData)
        {
            // By default no shortcut is applied
            bool handled = false;

            // Check for shortcut key combinations
            if (keyData == Navigator.Button.CloseButtonShortcut)
            {
                // Can only invoke action if there is a close button that is enabled
                if (Navigator.Button.CloseButton.GetVisible(Navigator.GetResolvedPalette()) &&
                    (Navigator.Button.CloseButton.GetEnabled(Navigator.GetResolvedPalette()) == ButtonEnabled.True))
                {
                    Navigator.PerformCloseAction();
                    handled = true;
                }
            }
            else if (keyData == Navigator.Button.ContextButtonShortcut)
            {
                // Can only invoke action if there is a context button that is enabled
                if (Navigator.Button.ContextButton.GetVisible(Navigator.GetResolvedPalette()) &&
                    (Navigator.Button.ContextButton.GetEnabled(Navigator.GetResolvedPalette()) == ButtonEnabled.True))
                {
                    Navigator.PerformContextAction();
                    handled = true;
                }
            }
            else if (keyData == Navigator.Button.PreviousButtonShortcut)
            {
                // Can only invoke action if there is a previous button that is enabled
                if (Navigator.Button.PreviousButton.GetVisible(Navigator.GetResolvedPalette()) &&
                    (Navigator.Button.PreviousButton.GetEnabled(Navigator.GetResolvedPalette()) == ButtonEnabled.True))
                {
                    Navigator.PerformPreviousAction();
                    handled = true;
                }
            }
            else if (keyData == Navigator.Button.NextButtonShortcut)
            {
                // Can only invoke action if there is a next button that is enabled
                if (Navigator.Button.NextButton.GetVisible(Navigator.GetResolvedPalette()) &&
                    (Navigator.Button.NextButton.GetEnabled(Navigator.GetResolvedPalette()) == ButtonEnabled.True))
                {
                    Navigator.PerformNextAction();
                    handled = true;
                }
            }

            return handled;
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The mnemonic character entered.</param>
        /// <returns>true if the mnemonic was processsed; otherwise, false.</returns>
        public virtual bool ProcessMnemonic(char charCode)
        {
            // There must be at least one page and allowed to select a page
            if ((Navigator.Pages.Count > 0) && Navigator.AllowTabSelect)
            {
                KryptonPage first;

                // Start searching from after the selected page onwards
                if (Navigator.SelectedPage != null)
                {
                    first = Navigator.NextActionPage(Navigator.SelectedPage);

                    // If at end of collection then get the first page
                    if (first == null)
                        first = Navigator.FirstActionPage();
                }
                else
                    first = Navigator.FirstActionPage();

                // Next page to test is the first one 
                KryptonPage next = first;

                // Keep testing next pages until no more are left
                while (next != null)
                {
                    // Does the mnemonic for the page match the keys
                    if (Control.IsMnemonic(charCode, next.Text))
                    {
                        // Attempt to select the next page
                        Navigator.SelectedPage = next;

                        // If next page was selected, then all finished
                        if (Navigator.SelectedPage == next)
                        {
                            // If we do not have the focus, then take it now
                            Navigator.Focus();

                            // Select the first control inside the page
                            Navigator.SelectNextPageControl(true, true);

                            return true;
                        }
                    }

                    // Otherwise keep looking for another visible next page
                    next = Navigator.NextActionPage(next);

                    // If we reached the end of the collection then wrap
                    if (next == null)
                    {
                        // Wrap around to the first page
                        next = Navigator.FirstActionPage();
                    }

                    // If we are back at the first page we examined then we must have
                    // wrapped around collection and still found nothing, time to exit
                    if (next == first)
                        break;
                }
            }

            return false;
        }

        /// <summary>
        /// Select the next page to the currently selected one.
        /// </summary>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <returns>True if new page selected; otherwise false.</returns>
        public virtual bool SelectNextPage(bool wrap)
        {
            // A page must be selected in order to find the previous one
            if (Navigator.SelectedPage != null)
                return SelectNextPage(Navigator.SelectedPage, wrap, false);
            else
                return false;
        }

        /// <summary>
        /// Select the next page to the one provided.
        /// </summary>
        /// <param name="page">Starting page for search.</param>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <param name="ctrlTab">Associated with a Ctrl+Tab action.</param>
        /// <returns>True if new page selected; otherwise false.</returns>
        public virtual bool SelectNextPage(KryptonPage page, 
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
                    first = Navigator.NextActionPage(page);

                    // If at end of collection and wrapping is enabled then get the first page
                    if ((first == null) && wrap)
                    {
                        // Are we allowed to wrap around?
                        CtrlTabCancelEventArgs ce = new CtrlTabCancelEventArgs(true);
                        Navigator.OnCtrlTabWrap(ce);

                        if (ce.Cancel)
                            return false;

                        first = Navigator.FirstActionPage();
                    }
                }
                else
                    first = Navigator.FirstActionPage();

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
                        next = Navigator.NextActionPage(next);

                        // If we reached the end of the collection and we should wrap
                        if ((next == null) && wrap)
                        {
                            // Are we allowed to wrap around?
                            CtrlTabCancelEventArgs ce = new CtrlTabCancelEventArgs(true);
                            Navigator.OnCtrlTabWrap(ce);

                            if (ce.Cancel)
                                return false;

                            // Wrap around to the first page
                            next = Navigator.FirstActionPage();
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
        /// Select the previous page to the currently selected one.
        /// </summary>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <returns>True if new page selected; otherwise false.</returns>
        public virtual bool SelectPreviousPage(bool wrap)
        {
            // A page must be selected in order to find the previous one
            if (Navigator.SelectedPage != null)
                return SelectPreviousPage(Navigator.SelectedPage, wrap, false);
            else
                return false;
        }

        /// <summary>
        /// Select the previous page to the one provided.
        /// </summary>
        /// <param name="page">Starting page for search.</param>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <param name="ctrlTab">Associated with a Ctrl+Tab action.</param>
        /// <returns>True if new page selected; otherwise false.</returns>
        public virtual bool SelectPreviousPage(KryptonPage page, 
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
                    first = Navigator.PreviousActionPage(page);

                    // If at start of collection and wrapping is enabled then get the last page
                    if ((first == null) && wrap)
                    {
                        // Are we allowed to wrap around?
                        CtrlTabCancelEventArgs ce = new CtrlTabCancelEventArgs(false);
                        Navigator.OnCtrlTabWrap(ce);

                        if (ce.Cancel)
                            return false;

                        first = Navigator.LastActionPage();
                    }
                }
                else
                    first = Navigator.LastActionPage();

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
                        previous = Navigator.PreviousActionPage(previous);

                        // If we reached the start of the collection and we should wrap
                        if ((previous == null) && wrap)
                        {
                            // Are we allowed to wrap around?
                            CtrlTabCancelEventArgs ce = new CtrlTabCancelEventArgs(false);
                            Navigator.OnCtrlTabWrap(ce);

                            if (ce.Cancel)
                                return false;

                            // Wrap around to the last page
                            previous = Navigator.Pages[Navigator.Pages.Count - 1];
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
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the need paint delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintDelegate
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                // Only create the delegate when it is first needed
                if (_needPaintDelegate == null)
                    _needPaintDelegate = new NeedPaintHandler(OnNeedPaint);

                return _needPaintDelegate;
            }
        }

        /// <summary>
        /// Requests a need paint be performed on the navigator.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        protected void PerformNeedPaint(bool needLayout)
        {
            OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));
        }

        /// <summary>
        /// Requests a need page paint be performed on the navigator.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        protected void PerformNeedPagePaint(bool needLayout)
        {
            OnNeedPagePaint(this, new NeedLayoutEventArgs(needLayout));
        }

        /// <summary>
		/// Perform a need paint on the navigator.
		/// </summary>
		/// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // Pass paint request onto the navigator control itself
            Navigator.PerformNeedPaint(e.NeedLayout);
        }

        /// <summary>
        /// Perform a need page paint on the navigator.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected void OnNeedPagePaint(object sender, NeedLayoutEventArgs e)
        {
            // Pass paint request onto the navigator control itself
            Navigator.PerformNeedPagePaint(e.NeedLayout);
        } 

        /// <summary>
        /// Process the change in a property that might effect the view builder.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Property changed details.</param>
        protected virtual void OnViewBuilderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PanelBackStyle":
                    Navigator.StateCommon.BackStyle = Navigator.Panel.PanelBackStyle;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "PageBackStyle":
                    Navigator.StateCommon.PalettePage.BackStyle = Navigator.PageBackStyle;
                    Navigator.PerformNeedPagePaint(true);
                    break;
                case "GroupBackStyle":
                    Navigator.ChildPanel.PanelBackStyle = Navigator.Group.GroupBackStyle;
                    Navigator.StateCommon.HeaderGroup.BackStyle = Navigator.Group.GroupBackStyle;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "GroupBorderStyle":
                    Navigator.StateCommon.HeaderGroup.BorderStyle = Navigator.Group.GroupBorderStyle;
                    Navigator.PerformNeedPaint(true);
                    break;
            }
        }
        #endregion

        #region Static Methods
        /// <summary>
		/// Create a new view builder appropriate for the provided mode.
		/// </summary>
		/// <param name="mode">Navigator mode of operation.</param>
		/// <returns>ViewBuild appropriate for mode.</returns>
		public static ViewBuilderBase CreateViewBuilder(NavigatorMode mode)
		{
			switch (mode)
			{
                case NavigatorMode.BarTabGroup:
                    return new ViewBuilderBarTabGroup();
                case NavigatorMode.BarTabOnly:
                    return new ViewBuilderBarTabOnly();
                case NavigatorMode.BarRibbonTabGroup:
                    return new ViewBuilderBarRibbonTabGroup();
                case NavigatorMode.BarRibbonTabOnly:
                    return new ViewBuilderBarRibbonTabOnly();
                case NavigatorMode.BarCheckButtonOnly:
                    return new ViewBuilderBarCheckButtonOnly();
                case NavigatorMode.BarCheckButtonGroupOnly:
                    return new ViewBuilderBarCheckButtonGroupOnly();
                case NavigatorMode.BarCheckButtonGroupInside:
                    return new ViewBuilderBarCheckButtonGroupInside();
                case NavigatorMode.BarCheckButtonGroupOutside:
                    return new ViewBuilderBarCheckButtonGroupOutside();
                case NavigatorMode.StackCheckButtonGroup:
                    return new ViewBuilderStackCheckButtonGroup();
                case NavigatorMode.StackCheckButtonHeaderGroup:
                    return new ViewBuilderStackCheckButtonHeaderGroup();
                case NavigatorMode.HeaderGroupTab:
                    return new ViewBuilderHeaderBarTabGroup();
                case NavigatorMode.HeaderBarCheckButtonOnly:
                    return new ViewBuilderHeaderBarCheckButtonOnly();
                case NavigatorMode.HeaderBarCheckButtonGroup:
                    return new ViewBuilderHeaderBarCheckButtonGroup();
                case NavigatorMode.HeaderBarCheckButtonHeaderGroup:
                    return new ViewBuilderHeaderBarCheckButtonHeaderGroup();
                case NavigatorMode.OutlookFull:
                   return new ViewBuilderOutlookFull();
               case NavigatorMode.OutlookMini:
                   return new ViewBuilderOutlookMini();
               case NavigatorMode.HeaderGroup:
					return new ViewBuilderHeaderGroup();
                case NavigatorMode.Group:
                    return new ViewBuilderGroup();
                case NavigatorMode.Panel:
                    return new ViewBuilderPanel();
                default:
					// Should never happen!
					Debug.Assert(false);
					throw new ArgumentOutOfRangeException("mode");
			}
		}
		#endregion
	}
}
