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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Implements the NavigatorMode.HeaderGroup view.
	/// </summary>
    internal class ViewBuilderHeaderGroup : ViewBuilderBase
	{
		#region Instance Fields
		private ViewBase _oldRoot;
        private ViewletHeaderGroup _headerGroup;
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

            // Construct the viewlet instance
            _headerGroup = new ViewletHeaderGroup(navigator, redirector, NeedPaintDelegate);

            // Create and initialize all objects
            ViewBase newRoot = _headerGroup.Construct(_oldRoot);
            _headerGroup.PostCreate();

			// Assign the new root
            ViewManager.Root = newRoot;

			// Need to monitor changes in the enabled state
			Navigator.EnabledChanged += new EventHandler(OnEnabledChanged);
		}

        /// <summary>
        /// Destruct the previously created view.
        /// </summary>
        public override void Destruct()
        {
            // Unhook from events
            Navigator.EnabledChanged -= new EventHandler(OnEnabledChanged);

            // Pull down the header group view hierarchy
            _headerGroup.Destruct();

            // Put the old root back again
            ViewManager.Root = _oldRoot;

            // Let base class do standard work
            base.Destruct();
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
            // There is no view for the page
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
            return _headerGroup.ButtonSpecFromView(element);
        }

        /// <summary>
        /// Process a change in the selected page
        /// </summary>
        public override void SelectedPageChanged()
        {
            UpdateStatePalettes();
            _headerGroup.UpdateButtons();

            // Let base class do standard work
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
        /// Notification that a krypton page appearance property has changed.
        /// </summary>
        /// <param name="page">Page that has changed.</param>
        /// <param name="property">Name of property that has changed.</param>
        public override void PageAppearanceChanged(KryptonPage page, string property)
        {
            Debug.Assert(page != null);
            Debug.Assert(property != null);

            // We are only interested if the selected page has changed
            if (page == Navigator.SelectedPage)
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
            // Ask the header group to update its palettes
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
            // Check if the header group wants the mouse
            return _headerGroup.DesignerGetHitTest(pt);
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
            }

            // Last of all check for a shortcut to the action buttons
            return CheckActionShortcuts(keyData);
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The mnemonic character entered.</param>
        /// <returns>true if the mnemonic was processsed; otherwise, false.</returns>
        public override bool ProcessMnemonic(char charCode)
        {
            // No mnemonic processing for a header group view
            return false;
        }
        #endregion

        #region Protected
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
        #endregion

		#region Implementation
        private void OnEnabledChanged(object sender, EventArgs e)
        {
            UpdateStatePalettes();
        }
        #endregion
	}
}
