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
    /// Implements the NavigatorMode.Skeleton view.
	/// </summary>
    internal class ViewBuilderPanel : ViewBuilderBase
	{
        #region Instance Fields
        private ViewBase _oldRoot;
        private ViewDrawPanel _drawPanel;
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

            // Create a canvas for the background
            _drawPanel = new ViewDrawPanel(Navigator.StateNormal.Back);

            // Put the exisint root into the canvas
            _drawPanel.Add(_oldRoot);

            // Update the child panel to have panel appearance
            Navigator.ChildPanel.PanelBackStyle = Navigator.Panel.PanelBackStyle;

            // Set the correct palettes based on enabled state and selected page
            UpdateStatePalettes();

            // Canvas becomes the new root
            ViewManager.Root = _drawPanel;

            // Need to monitor changes in the enabled state
            Navigator.EnabledChanged += new EventHandler(OnEnabledChanged);
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
            // There is no view for the page
            return null;
        }

        /// <summary>
        /// Process a change in the enabled state for a page.
        /// </summary>
        /// <param name="page">Page that has changed enabled state.</param>
        public override void PageEnabledStateChanged(KryptonPage page)
        {
            // If the page we are showing has changed
            if (page == Navigator.SelectedPage)
            {
                // Update the use the correct enabled/disabled palette
                UpdateStatePalettes();

                // Need to repaint to show the change
                Navigator.PerformNeedPaint(true);
            }
        }

        /// <summary>
        /// Ensure the correct state palettes are being used.
        /// </summary>
        public override void UpdateStatePalettes()
        {
            IPaletteBack back;

            // If whole navigator is disabled then all of view is disabled
            bool enabled = Navigator.Enabled;
            
            // If there is no selected page
            if (Navigator.SelectedPage == null)
            {
                // Then use the state defined in the navigator itself
                if (Navigator.Enabled)
                    back = Navigator.StateNormal.Back;
                else
                    back = Navigator.StateDisabled.Back;
            }
            else
            {
                // Use state defined in the selected page
                if (Navigator.SelectedPage.Enabled)
                    back = Navigator.SelectedPage.StateNormal.Back;
                else
                {
                    back = Navigator.SelectedPage.StateDisabled.Back;

                    // If page is disabled then all of view should look disabled
                    enabled = false;
                }
            }

            // Update the view canvas with correct palette source
            _drawPanel.SetPalettes(back);
            _drawPanel.Enabled = enabled;

            // Let base class perform common actions
            base.UpdateStatePalettes();
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The mnemonic character entered.</param>
        /// <returns>true if the mnemonic was processsed; otherwise, false.</returns>
        public override bool ProcessMnemonic(char charCode)
        {
            // No mnemonic processing for a panel view
            return false;
        }

		/// <summary>
		/// Destruct the previously created view.
		/// </summary>
		public override void Destruct()
		{
            // Unhook from events
            Navigator.EnabledChanged -= new EventHandler(OnEnabledChanged);

            // Update the child panel to have group appearance
            Navigator.ChildPanel.PanelBackStyle = Navigator.Group.GroupBackStyle;

            // Remove the old root from the canvas
            _drawPanel.Clear();

            // Put the old root back again
            ViewManager.Root = _oldRoot;

            // Let base class perform common operations
            base.Destruct();
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
            switch (e.PropertyName)
            {
                case "PanelBackStyle":
                    Navigator.ChildPanel.PanelBackStyle = Navigator.Panel.PanelBackStyle;
                    Navigator.StateCommon.BackStyle = Navigator.Panel.PanelBackStyle;
                    Navigator.PerformNeedPaint(true);
                    break;
                case "GroupBackStyle":
                    Navigator.StateCommon.HeaderGroup.BackStyle = Navigator.Group.GroupBackStyle;
                    Navigator.PerformNeedPaint(true);
                    break;
                default:
                    // Let base class handle other notifications
                    base.OnViewBuilderPropertyChanged(sender, e);
                    break;
            }
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
