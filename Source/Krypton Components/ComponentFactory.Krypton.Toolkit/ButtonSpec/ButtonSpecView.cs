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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Create and manage the view for a ButtonSpec definition.
    /// </summary>
    public class ButtonSpecView : GlobalId,
                                  IContentValues
    {
        #region Instance Fields
        private PaletteRedirect _redirector;
        private ButtonSpecManagerBase _manager;
        private ButtonSpec _buttonSpec;
        private PaletteTripleRedirect _palette;
        private PaletteRedirect _remapPalette;
        private ViewDrawButton _viewButton;
        private ViewLayoutCenter _viewCenter;
        private EventHandler _finishDelegate;
        private ButtonController _controller;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecView class.
        /// </summary>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="paletteMetric">Source for metric values.</param>
        /// <param name="metricPadding">Padding metric for border padding.</param>
        /// <param name="manager">Reference to owning manager.</param>
        /// <param name="buttonSpec">Access</param>
        public ButtonSpecView(PaletteRedirect redirector,
                              IPaletteMetric paletteMetric,
                              PaletteMetricPadding metricPadding,
                              ButtonSpecManagerBase manager,
                              ButtonSpec buttonSpec)
        {
            Debug.Assert(redirector != null);
            Debug.Assert(manager != null);
            Debug.Assert(buttonSpec != null);

            // Remember references
            _redirector = redirector;
            _manager = manager;
            _buttonSpec = buttonSpec;
            _finishDelegate = new EventHandler(OnFinishDelegate);

            // Create delegate for paint notifications
            NeedPaintHandler needPaint = new NeedPaintHandler(OnNeedPaint);

            // Intercept calls from the button for color remapping and instead use
            // the button spec defined map and the container foreground color
            _remapPalette = _manager.CreateButtonSpecRemap(redirector, buttonSpec);

            // Use a redirector to get button values directly from palette
            _palette = new PaletteTripleRedirect(_remapPalette,
                                                 PaletteBackStyle.ButtonButtonSpec,
                                                 PaletteBorderStyle.ButtonButtonSpec,
                                                 PaletteContentStyle.ButtonButtonSpec,
                                                 needPaint);


            // Create the view for displaying a button
            _viewButton = new ViewDrawButton(_palette, _palette, _palette, _palette,
                                             paletteMetric, this, VisualOrientation.Top, false);

            // Associate the view with the source component (for design time support)
            if (buttonSpec.AllowComponent)
                _viewButton.Component = buttonSpec;

            // Use a view center to place button in centre of given space
            _viewCenter = new ViewLayoutCenter(paletteMetric, metricPadding, VisualOrientation.Top);
            _viewCenter.Add(_viewButton);

            // Create a controller for managing button behavior
            ButtonSpecViewControllers controllers = CreateController(_viewButton, needPaint, new MouseEventHandler(OnClick));
            _viewButton.MouseController = controllers.MouseController;
            _viewButton.SourceController = controllers.SourceController;
            _viewButton.KeyController = controllers.KeyController;

            // We need notifying whenever a button specification property changes
            _buttonSpec.ButtonSpecPropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);

            // Associate the button spec with the view that is drawing it
            _buttonSpec.SetView(_viewButton);

            // Finally update view with current button spec settings
            UpdateButtonStyle();
            UpdateVisible();
            UpdateEnabled();
            UpdateChecked();
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the owning manager.
        /// </summary>
        public ButtonSpecManagerBase Manager
        {
            get { return _manager; }
        }

        /// <summary>
        /// Gets access to the monitored button spec
        /// </summary>
        public ButtonSpec ButtonSpec
        {
            get { return _buttonSpec; }
        }

        /// <summary>
        /// Gets access to the view centering that contains the button.
        /// </summary>
        public ViewLayoutCenter ViewCenter
        {
            get { return _viewCenter; }
        }

        /// <summary>
        /// Gets access to the view centering that contains the button.
        /// </summary>
        public ViewDrawButton ViewButton
        {
            get { return _viewButton; }
        }

        /// <summary>
        /// Gets access to the remapping palette.
        /// </summary>
        public PaletteRedirect RemapPalette
        {
            get { return _remapPalette; }
        }

        /// <summary>
        /// Gets and sets the composition setting for the button.
        /// </summary>
        public bool DrawButtonSpecOnComposition
        {
            get { return _viewButton.DrawButtonComposition; }
            set { _viewButton.DrawButtonComposition = value; }
        }

        /// <summary>
        /// Requests a repaint and optional layout be performed.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        public void PerformNeedPaint(bool needLayout)
        {
            _manager.PerformNeedPaint(this, needLayout);
        }

        /// <summary>
        /// Update the button style to reflect new button style setting.
        /// </summary>
        public void UpdateButtonStyle()
        {
            _palette.SetStyles(_buttonSpec.GetStyle(_redirector));
        }

        /// <summary>
        /// Update view button to reflect new button visible setting.
        /// </summary>
        public bool UpdateVisible()
        {
            // Decide if the view should be visible or not
            bool prevVisible = _viewCenter.Visible;
            _viewCenter.Visible = _buttonSpec.GetVisible(_redirector);

            // Return if a change has occured
            return (prevVisible != _viewCenter.Visible);
        }

        /// <summary>
        /// Update view button to reflect new button enabled setting.
        /// </summary>
        /// <returns>True is a change in state has occured.</returns>
        public bool UpdateEnabled()
        {
            bool changed = false;

            // Remember the initial state
            ViewBase newDependant;
            bool newEnabled;

            switch (_buttonSpec.GetEnabled(_redirector))
            {
                case ButtonEnabled.True:
                    newDependant = null;
                    newEnabled = true;
                    break;
                case ButtonEnabled.False:
                    newDependant = null;
                    newEnabled = false;
                    break;
                case ButtonEnabled.Container:
                    newDependant = _viewCenter.Parent;
                    newEnabled = true;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    newDependant = null;
                    newEnabled = false;
                    break;
            }

            // Only make change if the values have changed
            if (newEnabled != _viewButton.Enabled)
            {
                _viewButton.Enabled = newEnabled;
                changed = true;
            }

            if (newDependant != _viewButton.DependantEnabledState)
            {
                _viewButton.DependantEnabledState = newDependant;
                changed = true;
            }

            return changed;
        }

        /// <summary>
        /// Update view button to reflect new button checked setting.
        /// </summary>
        /// <returns>True is a change in state has occured.</returns>
        public bool UpdateChecked()
        {
            // Remember the initial state
            bool newChecked;

            switch (_buttonSpec.GetChecked(_redirector))
            {
                case ButtonCheckState.NotCheckButton:
                case ButtonCheckState.Unchecked:
                    newChecked = false;
                    break;
                case ButtonCheckState.Checked:
                    newChecked = true;
                    break;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    newChecked = false;
                    break;
            }

            // Only make change if the value has changed
            if (newChecked != _viewButton.Checked)
            {
                _viewButton.Checked = newChecked;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Destruct the previously created views.
        /// </summary>
        public void Destruct()
        {
            // Unhook from events
            _buttonSpec.ButtonSpecPropertyChanged -= new PropertyChangedEventHandler(OnPropertyChanged);

            // Remove buttonspec/view association
            _buttonSpec.SetView(null);

            // Remove all view element resources
            _viewCenter.Dispose();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create a button controller for the view.
        /// </summary>
        /// <param name="viewButton">View to be controlled.</param>
        /// <param name="needPaint">Paint delegate.</param>
        /// <param name="clickHandler">Reference to click handler.</param>
        /// <returns>Controller instance.</returns>
        public virtual ButtonSpecViewControllers CreateController(ViewDrawButton viewButton,
                                                                  NeedPaintHandler needPaint,
                                                                  MouseEventHandler clickHandler)
        {
            // Create a standard button controller
            _controller = new ButtonController(viewButton, needPaint);
            _controller.BecomesFixed = true;
            _controller.Click += clickHandler;

            // If associated with a tooltip manager then pass mouse messages onto tooltip manager
            IMouseController mouseController = (IMouseController)_controller;
            if (Manager.ToolTipManager != null)
                mouseController = new ToolTipController(Manager.ToolTipManager, viewButton, _controller);

            // Return a collection of controllers
            return new ButtonSpecViewControllers(mouseController, _controller, _controller);
        }

        /// <summary>
        /// Processes the finish of the button being pressed.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnFinishDelegate(object sender, EventArgs e)
        {
            // Ask the button to remove the fixed pressed appearance
            _controller.RemoveFixed();
        }
        #endregion

        #region IContentValues
        /// <summary>
		/// Gets the content image.
		/// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
        {
            // Get value from button spec passing inheritence redirector
            return _buttonSpec.GetImage(_redirector, state);
        }

        /// <summary>
        /// Gets the content image transparent color.
        /// </summary>
        /// <param name="state">The state for which the image color is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            // Get value from button spec passing inheritence redirector
            return _buttonSpec.GetImageTransparentColor(_redirector);
        }

        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText()
        {
            // Get value from button spec passing inheritence redirector
            return _buttonSpec.GetShortText(_redirector);
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText()
        {
            // Get value from button spec passing inheritence redirector
            return _buttonSpec.GetLongText(_redirector);
        }
        #endregion

        #region Implementation
        private void OnClick(object sender, MouseEventArgs e)
        {
            // Never show a context menu in design mode
            if (!CommonHelper.DesignMode(_manager.Control))
            {
                // Fire the event handlers hooked into the button spec click event
                _buttonSpec.PerformClick(e);

                // Does the button spec define a krypton context menu?
                if ((_buttonSpec.KryptonContextMenu != null) && (ViewButton != null))
                {
                    // Convert from control coordinates to screen coordinates
                    Rectangle rect = ViewButton.ClientRectangle;
                    Point pt;

                    // If the button spec is on the chrome titlebar then find position manually
                    if (_manager.Control is Form)
                        pt = new Point(_manager.Control.Left + rect.Left, _manager.Control.Top + rect.Bottom + 3);
                    else
                        pt = _manager.Control.PointToScreen(new Point(rect.Left, rect.Bottom + 3));

                    // Show the context menu just below the view itself
                    _buttonSpec.KryptonContextMenu.Closed += new ToolStripDropDownClosedEventHandler(OnKryptonContextMenuClosed);
                    if (!_buttonSpec.KryptonContextMenu.Show(_buttonSpec, pt))
                    {
                        // Menu not being shown, so clean up
                        _buttonSpec.KryptonContextMenu.Closed -= new ToolStripDropDownClosedEventHandler(OnKryptonContextMenuClosed);

                        // Not showing a context menu, so remove the fixed view immediately
                        if (_finishDelegate != null)
                            _finishDelegate(this, EventArgs.Empty);
                    }
                }
                else if ((_buttonSpec.ContextMenuStrip != null) && (ViewButton != null))
                {
                    // Set the correct renderer for the menu strip
                    _buttonSpec.ContextMenuStrip.Renderer = _manager.RenderToolStrip();

                    // Convert from control coordinates to screen coordinates
                    Rectangle rect = ViewButton.ClientRectangle;
                    Point pt = _manager.Control.PointToScreen(new Point(rect.Left, rect.Bottom + 3));

                    // Show the context menu just below the view itself
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_buttonSpec.ContextMenuStrip, pt, _finishDelegate);
                }
                else
                {
                    // Not showing a context menu, so remove the fixed view immediately
                    if (_finishDelegate != null)
                        _finishDelegate(this, EventArgs.Empty);
                }
            }
            else
            {
                // Not showing a context menu, so remove the fixed view immediately
                if (_finishDelegate != null)
                    _finishDelegate(this, EventArgs.Empty);
            }
        }

        private void OnKryptonContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // Unhook from context menu event so it could garbage collected in the future
            KryptonContextMenu kcm = (KryptonContextMenu)sender;
            kcm.Closed -= new ToolStripDropDownClosedEventHandler(OnKryptonContextMenuClosed);

            // Remove the fixed button appearance
            OnFinishDelegate(sender, e);
        }

        private void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            PerformNeedPaint(e.NeedLayout);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Image":
                case "Text":
                case "ExtraText":
                case "ColorMap":
                    PerformNeedPaint(true);
                    break;
                case "Style":
                    UpdateButtonStyle();
                    PerformNeedPaint(true);
                    break;
                case "Visible":
                    UpdateVisible();
                    PerformNeedPaint(true);
                    break;
                case "Enabled":
                    UpdateEnabled();
                    PerformNeedPaint(true);
                    break;
                case "Checked":
                    UpdateChecked();
                    PerformNeedPaint(true);
                    break;
            }
        }
        #endregion
    }
}
