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
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Manage a collection of button specs for placing within a collection of docking views.
    /// </summary>
    public abstract class ButtonSpecManagerBase : GlobalId
    {
        #region Type Definitions
        internal class ButtonSpecLookup : Dictionary<ButtonSpec, ButtonSpecView> { };
        internal class ListSpacers : List<ViewLayoutMetricSpacer> { };
        #endregion

        #region Instance Fields
        private ToolTipManager _toolTipManager;
        private PaletteRedirect _redirector;
        private ButtonSpecCollectionBase _variableSpecs;
        private ButtonSpecCollectionBase _fixedSpecs;
        private IPaletteMetric[] _viewMetrics;
        private PaletteMetricInt[] _viewMetricIntOutside;
        private PaletteMetricInt[] _viewMetricIntInside;
        private PaletteMetricPadding[] _viewMetricPaddings;
        private ListSpacers[] _viewSpacers;
        private ButtonSpecLookup _specLookup;
        private GetToolStripRenderer _getRenderer;
        private bool _useMnemonic;
        private Control _control;
        private NeedPaintHandler _needPaint;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecManagerBase class.
        /// </summary>
        /// <param name="control">Control that owns the button manager.</param>
        /// <param name="redirector">Palette redirector.</param>
        /// <param name="variableSpecs">Variable set of button specifications.</param>
        /// <param name="fixedSpecs">Fixed set of button specifications.</param>
        /// <param name="viewMetrics">Array of target metric providers.</param>
        /// <param name="viewMetricIntOutside">Array of target metrics for outside spacer size.</param>
        /// <param name="viewMetricIntInside">Array of target metrics for inside spacer size.</param>
        /// <param name="viewMetricPaddings">Array of target metrics for button padding.</param>
        /// <param name="getRenderer">Delegate for returning a tool strip renderer.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ButtonSpecManagerBase(Control control,
                                     PaletteRedirect redirector,
                                     ButtonSpecCollectionBase variableSpecs,
                                     ButtonSpecCollectionBase fixedSpecs,
                                     IPaletteMetric[] viewMetrics,
                                     PaletteMetricInt[] viewMetricIntOutside,
                                     PaletteMetricInt[] viewMetricIntInside,
                                     PaletteMetricPadding[] viewMetricPaddings,
                                     GetToolStripRenderer getRenderer,
                                     NeedPaintHandler needPaint)
        {
            Debug.Assert(control != null);
            Debug.Assert(redirector != null);
            Debug.Assert(getRenderer != null);

            // Store the provided paint notification delegate
            NeedPaint = needPaint;

            // Remember references
            _control = control;
            _redirector = redirector;
            _variableSpecs = variableSpecs;
            _fixedSpecs = fixedSpecs;
            _viewMetrics = viewMetrics;
            _viewMetricIntOutside = viewMetricIntOutside;
            _viewMetricIntInside = viewMetricIntInside;
            _viewMetricPaddings = viewMetricPaddings;
            _getRenderer = getRenderer;

            if (_viewMetrics != null)
                _viewSpacers = new ListSpacers[_viewMetrics.Length];

            // Default state
            _useMnemonic = true;

            // Create the lookup instance
            _specLookup = new ButtonSpecLookup();

            // If there is a variable collection to monitor
            if (_variableSpecs != null)
            {
                // Need to hook into changes in the button collection
                _variableSpecs.Inserted += new EventHandler<ButtonSpecEventArgs>(OnButtonSpecInserted);
                _variableSpecs.Removed += new EventHandler<ButtonSpecEventArgs>(OnButtonSpecRemoved);
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the owning control.
        /// </summary>
        public Control Control
        {
            get { return _control; }
        }

        /// <summary>
        /// Gets and sets the associated tooltip manager.
        /// </summary>
        public ToolTipManager ToolTipManager
        {
            get { return _toolTipManager; }
            set { _toolTipManager = value; }
        }

        /// <summary>
        /// Gets and sets the need paint delegate for notifying paint requests.
        /// </summary>
        public NeedPaintHandler NeedPaint
        {
            get { return _needPaint; }
            set { _needPaint = value; }
        }

        /// <summary>
        /// Gets an array containing references of all the current views.
        /// </summary>
        public ButtonSpecView[] ButtonSpecViews
        {
            get
            {
                // Create the actual array for holding the references
                ButtonSpecView[] array = new ButtonSpecView[_specLookup.Count];

                int i=0;
                foreach (ButtonSpecView view in _specLookup.Values)
                    array[i++] = view;

                return array;
            }
        }

        /// <summary>
        /// Perform once only construction.
        /// </summary>
        public void Construct()
        {
            // Only add button spacers if we have metrics to control them
            if (_viewMetrics != null)
            {
                // Add button spacers into each of the view dockers
                for (int i = 0; i < _viewMetrics.Length; i++)
                {
                    // Get access to the matching docker/metrics/metric triple
                    IPaletteMetric viewMetric = _viewMetrics[i];
                    PaletteMetricInt viewMetricIntOutside = _viewMetricIntOutside[i];

                    // Create storage for the spacers
                    _viewSpacers[i] = new ListSpacers();

                    // Always create the outside edge spacers
                    ViewLayoutMetricSpacer spacerL1 = new ViewLayoutMetricSpacer(viewMetric, viewMetricIntOutside);
                    ViewLayoutMetricSpacer spacerR1 = new ViewLayoutMetricSpacer(viewMetric, viewMetricIntOutside);
                    spacerL1.Visible = spacerR1.Visible = false;

                    // Add the spacers to the docker instance
                    AddSpacersToDocker(i, spacerL1, spacerR1);

                    // Remember the spacers for future reference
                    _viewSpacers[i].AddRange(new ViewLayoutMetricSpacer[] { spacerL1, spacerR1 });

                    if (UseInsideSpacers)
                    {
                        PaletteMetricInt viewMetricIntInside = _viewMetricIntInside[i];

                        // Create the inside edge spacers
                        ViewLayoutMetricSpacer spacerL2 = new ViewLayoutMetricSpacer(viewMetric, viewMetricIntInside);
                        ViewLayoutMetricSpacer spacerR2 = new ViewLayoutMetricSpacer(viewMetric, viewMetricIntInside);
                        spacerL2.Visible = spacerR2.Visible = false;

                        // Add them into the view docker instance
                        AddSpacersToDocker(i, spacerL2, spacerR2);

                        // Remember the spacers for future reference
                        _viewSpacers[i].AddRange(new ViewLayoutMetricSpacer[] { spacerL2, spacerR2 });
                    }
                }
            }
        }

        /// <summary>
        /// Destruct the previously created views.
        /// </summary>
        public void Destruct()
        {
            // If we are monitoring a variable collection of button specs
            if (_variableSpecs != null)
            {
                // Unhook from button collection events
                _variableSpecs.Inserted -= new EventHandler<ButtonSpecEventArgs>(OnButtonSpecInserted);
                _variableSpecs.Removed -= new EventHandler<ButtonSpecEventArgs>(OnButtonSpecRemoved);
            }

            // Destruct each of the button views
            RemoveAll();
        }

        /// <summary>
        /// Gets and sets the use of mnemonics.
        /// </summary>
        public bool UseMnemonic
        {
            get { return _useMnemonic; }
            set { _useMnemonic = value; }
        }

        /// <summary>
        /// Requests that all the buttons be recreated.
        /// </summary>
        public void RecreateButtons()
        {
            RecreateAll();
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Requests that all the buttons have state refreshed.
        /// </summary>
        /// <returns>True if a state change was made.</returns>
        public bool RefreshButtons()
        {
            return RefreshButtons(false);
        }

        /// <summary>
        /// Requests that all the buttons have state refreshed.
        /// </summary>
        /// <param name="composition">Composition value for the spec view.</param>
        /// <returns>True if a state change was made.</returns>
        public bool RefreshButtons(bool composition)
        {
            bool changed = false;

            // Find all the button views
            foreach (ButtonSpecView buttonView in _specLookup.Values)
            {
                // Ensure the button is updated in correct visual state
                changed |= buttonView.UpdateVisible();
                changed |= buttonView.UpdateEnabled();
                changed |= buttonView.UpdateChecked();
                buttonView.DrawButtonSpecOnComposition = composition;
            }

            return changed;
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The mnemonic character entered.</param>
        /// <returns>true if the mnemonic was processsed; otherwise, false.</returns>
        public bool ProcessMnemonic(char charCode)
        {
            if (UseMnemonic)
            {
                // Check each button specification in turn
                foreach (ButtonSpecView buttonView in _specLookup.Values)
                {
                    // Only interested in buttons that are visible and enabled
                    if (buttonView.ViewCenter.Visible && buttonView.ViewButton.Enabled)
                    {
                        // If either the short or long text matches the mnemonic then fire
                        if (Control.IsMnemonic(charCode, buttonView.ButtonSpec.GetShortText(_redirector)) ||
                            Control.IsMnemonic(charCode, buttonView.ButtonSpec.GetLongText(_redirector)))
                        {
                            // Fire the button click event and eat mnemonic character
                            buttonView.ButtonSpec.PerformClick();
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Update the palette for a specified docker view.
        /// </summary>
        /// <param name="viewDocker">Target docker view.</param>
        /// <param name="viewMetric">New metric source.</param>
        public void SetDockerMetrics(ViewBase viewDocker,
                                     IPaletteMetric viewMetric)
        {
            // If we are applying padding metrics
            if (_viewMetrics != null)
            {
                // Get the index of the specified docker
                int i = DockerIndex(viewDocker);

                // If we found a matching entry
                if (i >= 0)
                {
                    // Update metric palette
                    _viewMetrics[i] = viewMetric;

                    // Update the the metric for all the spacers associated with this docker
                    foreach (ViewLayoutMetricSpacer spacer in _viewSpacers[i])
                        spacer.SetMetrics(viewMetric);

                    // Need a repaint and layout to show change
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Update the metric details for a specified docker view.
        /// </summary>
        /// <param name="viewDocker">Target docker view.</param>
        /// <param name="viewMetric">New metric source.</param>
        /// <param name="viewMetricInt">New border edge metric.</param>
        /// <param name="viewMetricPadding">New button border metric.</param>
        public void SetDockerMetrics(ViewBase viewDocker,
                                     IPaletteMetric viewMetric,
                                     PaletteMetricInt viewMetricInt,
                                     PaletteMetricPadding viewMetricPadding)
        {
            // If we are applying padding metrics
            if (_viewMetrics != null)
            {
                // Get the index of the specified docker
                int i = DockerIndex(viewDocker);

                // If we found a matching entry
                if (i >= 0)
                {
                    // Update metric values
                    _viewMetrics[i] = viewMetric;
                    _viewMetricPaddings[i] = viewMetricPadding;

                    // Update the the metric for all the spacers associated with this docker
                    foreach (ViewLayoutMetricSpacer spacer in _viewSpacers[i])
                        spacer.SetMetrics(viewMetric, viewMetricInt);

                    // Need a repaint and layout to show change
                    PerformNeedPaint(true);
                }

                // Find all the buttons on this view docker
                foreach (ButtonSpecView buttonView in _specLookup.Values)
                {
                    // Is this button placed inside the target view docker?
                    if (buttonView.ViewCenter.Parent == viewDocker)
                    {
                        // Then update this view with the new metric
                        buttonView.ViewCenter.MetricPadding = viewMetricPadding;
                    }
                }
            }
        }

        /// <summary>
        /// Get the display rectangle of the provided button.
        /// </summary>
        /// <param name="buttonSpec">Button specification.</param>
        /// <returns>Display rectangle.</returns>
        public Rectangle GetButtonRectangle(ButtonSpec buttonSpec)
        {
            // Find all the buttons on this view docker
            foreach (ButtonSpecView buttonView in _specLookup.Values)
                if (buttonView.ButtonSpec == buttonSpec)
                    return buttonView.ViewButton.ClientRectangle;

            return Rectangle.Empty;
        }

        /// <summary>
        /// Gets the ButtonSpec associated with the provided view element.
        /// </summary>
        /// <param name="element">Element to search against.</param>
        /// <returns>Reference to ButtonSpec; otherwise null.</returns>
        public ButtonSpec ButtonSpecFromView(ViewBase element)
        {
            // Search for a button spec that contains this element
            foreach (ButtonSpecView buttonView in _specLookup.Values)
                if (buttonView.ViewCenter.ContainsRecurse(element))
                    return buttonView.ButtonSpec;

            // No match
            return null;
        }

        /// <summary>
        /// Is the provided over a part of the view that wants the mouse.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        /// <returns>True if the view wants the mouse position; otherwise false.</returns>
        public bool DesignerGetHitTest(Point pt)
        {
            // Search all buttons for any that contain the provided point
            foreach (ButtonSpecView buttonView in _specLookup.Values)
                if (buttonView.ViewButton.Visible &&
                    buttonView.ViewButton.Enabled &&
                    buttonView.ViewButton.ClientRectangle.Contains(pt))
                    return true;

            return false;
        }

        /// <summary>
        /// Get a tool strip renderer appropriate for the hosting control.
        /// </summary>
        /// <returns></returns>
        public ToolStripRenderer RenderToolStrip()
        {
            return _getRenderer();
        }

        /// <summary>
        /// Requests a repaint and optional layout be performed.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        public void PerformNeedPaint(bool needLayout)
        {
            PerformNeedPaint(this, needLayout);
        }

        /// <summary>
        /// Requests a repaint and optional layout be performed.
        /// </summary>
        /// <param name="sender">Source of the paint event.</param>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        public void PerformNeedPaint(object sender, bool needLayout)
        {
            // Fire the actual event
            OnNeedPaint(sender, needLayout);
        }

        /// <summary>
        /// Find the ButtonSpec definition associated with the provided button view.
        /// </summary>
        /// <param name="viewButton">View to use when searching.</param>
        /// <returns>ButtonSpec reference if found; otherwise null.</returns>
        public virtual ButtonSpec GetButtonSpecFromView(ViewDrawButton viewButton)
        {
            foreach (ButtonSpecView specView in _specLookup.Values)
                if (specView.ViewButton == viewButton)
                    return specView.ButtonSpec;

            return null;
        }

        /// <summary>
        /// Gets the view for the first visible and enabled button spec of the defined edge.
        /// </summary>
        /// <param name="align">Edge of buttons caller is interested in searching.</param>
        /// <returns>ViewDrawButton reference; otherwise false.</returns>
        public virtual ViewDrawButton GetFirstVisibleViewButton(PaletteRelativeEdgeAlign align)
        {
            foreach (ButtonSpecView specView in _specLookup.Values)
            {
                // Is the button actually visible/enabled
                if (specView.ViewCenter.Visible && 
                    specView.ViewButton.Enabled)
                {
                    if (specView.ButtonSpec.Edge == align)
                        return specView.ViewButton;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the view for the next visible and enabled button spec of the defined edge.
        /// </summary>
        /// <param name="align">Edge of buttons caller is interested in searching.</param>
        /// <param name="current">Current button that is the marker for searching.</param>
        /// <returns>ViewDrawButton reference; otherwise false.</returns>
        public virtual ViewDrawButton GetNextVisibleViewButton(PaletteRelativeEdgeAlign align,
                                                               ViewDrawButton current)
        {
            bool found = false;
            foreach (ButtonSpecView specView in _specLookup.Values)
            {
                if (!found)
                    found = (specView.ViewButton == current);
                else
                {
                    // Is the button actually visible/enabled
                    if (specView.ViewCenter.Visible &&
                        specView.ViewButton.Enabled)
                    {
                        if (specView.ButtonSpec.Edge == align)
                            return specView.ViewButton;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the view for the previous visible and enabled button spec of the defined edge.
        /// </summary>
        /// <param name="align">Edge of buttons caller is interested in searching.</param>
        /// <param name="current">Current button that is the marker for searching.</param>
        /// <returns>ViewDrawButton reference; otherwise false.</returns>
        public virtual ViewDrawButton GetPreviousVisibleViewButton(PaletteRelativeEdgeAlign align,
                                                                   ViewDrawButton current)
        {
            ButtonSpecView[] specLookups = new ButtonSpecView[_specLookup.Count];
            _specLookup.Values.CopyTo(specLookups, 0);

            bool found = false;
            for (int i = _specLookup.Count - 1; i >= 0; i--)
            {
                ButtonSpecView specView = (ButtonSpecView)specLookups[i];

                if (!found)
                    found = (specView.ViewButton == current);
                else
                {
                    // Is the button actually visible/enabled
                    if (specView.ViewCenter.Visible &&
                        specView.ViewButton.Enabled)
                    {
                        if (specView.ButtonSpec.Edge == align)
                            return specView.ViewButton;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the view for the last visible and enabled button spec of the defined edge.
        /// </summary>
        /// <param name="align">Edge of buttons caller is interested in searching.</param>
        /// <returns>ViewDrawButton reference; otherwise false.</returns>
        public virtual ViewDrawButton GetLastVisibleViewButton(PaletteRelativeEdgeAlign align)
        {
            ButtonSpecView[] specLookups = new ButtonSpecView[_specLookup.Count];
            _specLookup.Values.CopyTo(specLookups, 0);

            for(int i =_specLookup.Count - 1; i>=0; i--)
            {
                ButtonSpecView specView = (ButtonSpecView)specLookups[i];

                // Is the button actually visible/enabled
                if (specView.ViewCenter.Visible &&
                    specView.ViewButton.Enabled)
                {
                    if (specView.ButtonSpec.Edge == align)
                        return specView.ViewButton;
                }
            }

            return null;
        }

        /// <summary>
        /// Create a palette redirector for remapping button spec colors.
        /// </summary>
        /// <param name="redirector">Base palette class.</param>
        /// <param name="buttonSpec">ButtonSpec instance.</param>
        /// <returns>Palette redirector for the button spec instance.</returns>
        public virtual PaletteRedirect CreateButtonSpecRemap(PaletteRedirect redirector,
                                                             ButtonSpec buttonSpec)
        {
            return new ButtonSpecRemapByContentView(redirector, buttonSpec);
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Gets a value indicating if inside edge spacers are required.
        /// </summary>
        protected virtual bool UseInsideSpacers
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the number of dockers.
        /// </summary>
        /// <returns>Number of dockers.</returns>
        protected abstract int DockerCount { get; }

        /// <summary>
        /// Gets the index of the provided docker.
        /// </summary>
        /// <param name="viewDocker">View docker reference.</param>
        /// <returns>Index of docker; otherwise -1.</returns>
        protected abstract int DockerIndex(ViewBase viewDocker);

        /// <summary>
        /// Gets the docker at the specified index.
        /// </summary>
        /// <param name="i">Index.</param>
        /// <returns>View docker reference; otherwise null.</returns>
        protected abstract ViewBase IndexDocker(int i);

        /// <summary>
        /// Gets the orientation of the docker at the specified index.
        /// </summary>
        /// <param name="i">Index.</param>
        /// <returns>VisualOrientation value.</returns>
        protected abstract VisualOrientation DockerOrientation(int i);

        /// <summary>
        /// Gets the element that represents the foreground color.
        /// </summary>
        /// <param name="i">Index.</param>
        /// <returns>View content instance.</returns>
        protected abstract ViewDrawContent GetDockerForeground(int i);

        /// <summary>
        /// Add a view element to a docker.
        /// </summary>
        /// <param name="i">Index of view docker.</param>
        /// <param name="dockStyle">Dock style for placement.</param>
        /// <param name="view">Actual view to add.</param>
        /// <param name="usingSpacers">Are view spacers being used.</param>
        protected abstract void AddViewToDocker(int i, ViewDockStyle dockStyle, ViewBase view, bool usingSpacers);

        /// <summary>
        /// Add the spacing views into the indexed docker.
        /// </summary>
        /// <param name="i">Index of docker.</param>
        /// <param name="spacerL">Spacer for the left side.</param>
        /// <param name="spacerR">Spacer for the right side.</param>
        protected abstract void AddSpacersToDocker(int i, ViewLayoutMetricSpacer spacerL, ViewLayoutMetricSpacer spacerR);

        /// <summary>
        /// Perform final steps now that the button spec has been created.
        /// </summary>
        /// <param name="buttonSpec">ButtonSpec instance.</param>
        /// <param name="buttonView">Associated ButtonSpecView instance.</param>
        /// <param name="viewDockerIndex">Index of view docker button is placed onto.</param>
        protected virtual void ButtonSpecCreated(ButtonSpec buttonSpec, 
                                                 ButtonSpecView buttonView, 
                                                 int viewDockerIndex)
        {
            // Cast the remapping palette to the correct type
            ButtonSpecRemapByContentView remapPalette = (ButtonSpecRemapByContentView)buttonView.RemapPalette;
            
            // Update button with the foreground used for color mapping
            remapPalette.Foreground = GetDockerForeground(viewDockerIndex);
        }

        /// <summary>
        /// Create the button spec view appropriate for the button spec.
        /// </summary>
        /// <param name="redirector">Redirector for acquiring palette values.</param>
        /// <param name="viewPaletteMetric">Target metric providers.</param>
        /// <param name="viewMetricPadding">Target metric padding.</param>
        /// <param name="buttonSpec">ButtonSpec instance.</param>
        /// <returns>ButtonSpecView derived class.</returns>
        protected virtual ButtonSpecView CreateButtonSpecView(PaletteRedirect redirector,
                                                              IPaletteMetric viewPaletteMetric,
                                                              PaletteMetricPadding viewMetricPadding,
                                                              ButtonSpec buttonSpec)
        {
            return new ButtonSpecView(redirector,
                                      viewPaletteMetric,
                                      viewMetricPadding,
                                      this,
                                      buttonSpec);
        }

        /// <summary>
        /// Raises the NeedPaint event.
        /// </summary>
        /// <param name="sender">Source of the paint event.</param>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        protected virtual void OnNeedPaint(object sender, bool needLayout)
        {
            if (_needPaint != null)
                _needPaint(sender, new NeedLayoutEventArgs(needLayout));
        }
        #endregion

        #region Implementation
        private void RecreateAll()
        {
            RemoveAll();
            CreateAll();
        }

        private void RemoveAll()
        {
            // Destruct all the current views
            foreach (ButtonSpecView buttonView in _specLookup.Values)
                RemoveButtonSpec(buttonView.ButtonSpec);

            // All views are destroyed so clear down lookup
            _specLookup.Clear();
        }

        private void CreateAll()
        {
            // Count how many visible buttons are on each edge of each docker
            int[] nearCounts = new int[DockerCount];
            int[] farCounts = new int[DockerCount];

            // Create from the variable and the fixed collections
            CreateFromCollection(_variableSpecs, ref nearCounts, ref farCounts);
            CreateFromCollection(_fixedSpecs, ref nearCounts, ref farCounts);

            // If we are applying padding metrics
            if (_viewMetrics != null)
            {
                // Update the visible state of the edge spacers
                for (int i = 0; i < _viewMetrics.Length; i++)
                {
                    ViewBase viewDocker = IndexDocker(i);

                    // Only enable the spacer if there is at least one visible button on that edge
                    bool farVisible = (farCounts[i] > 0);
                    bool nearVisible = (nearCounts[i] > 0);

                    // The outside spacers are always present
                    _viewSpacers[i][0].Visible = nearVisible;
                    _viewSpacers[i][1].Visible = farVisible;

                    if (UseInsideSpacers)
                    {
                        _viewSpacers[i][2].Visible = nearVisible;
                        _viewSpacers[i][3].Visible = farVisible;
                    }
                }
            }
        }

        private void CreateFromCollection(ButtonSpecCollectionBase specs,
                                          ref int[] nearCounts,
                                          ref int[] farCounts)
        {
            // If there is a collection to examine
            if (specs != null)
            {
                // Create views for all the button specifications
                foreach (ButtonSpec buttonSpec in specs.Enumerate())
                {
                    // Add view for the button spec
                    ButtonSpecView view = AddButtonSpec(buttonSpec);

                    if (view != null)
                    {
                        // Only interested in visible buttons
                        if (buttonSpec.GetVisible(_redirector))
                        {
                            // Get the index of the target view docker
                            int dockerIndex = GetTargetDockerIndex(buttonSpec.GetLocation(_redirector));

                            // The edge determines which count to use
                            if (buttonSpec.GetEdge(_redirector) == RelativeEdgeAlign.Far)
                                farCounts[dockerIndex]++;
                            else
                                nearCounts[dockerIndex]++;
                        }
                    }
                }
            }
        }

        private ButtonSpecView AddButtonSpec(ButtonSpec buttonSpec)
        {
            // Find the docker index that is the target for the button spec
            int viewDockerIndex = GetTargetDockerIndex(buttonSpec.GetLocation(_redirector));

            IPaletteMetric viewPaletteMetric = null;
            PaletteMetricPadding viewMetricPadding = PaletteMetricPadding.None;

            // Are we applying metrics
            if ((_viewMetrics != null) &&
                (_viewMetrics.Length > viewDockerIndex) &&
                (_viewMetricPaddings.Length > viewDockerIndex))
            {
                viewPaletteMetric = _viewMetrics[viewDockerIndex];
                viewMetricPadding = _viewMetricPaddings[viewDockerIndex];

                // Create an instance to manage the individual button spec
                ButtonSpecView buttonView = CreateButtonSpecView(_redirector, viewPaletteMetric, viewMetricPadding, buttonSpec);

                // Add a lookup from the button spec to the button spec view
                _specLookup.Add(buttonSpec, buttonView);

                // Update the button with the same orientation as the view header
                buttonView.ViewButton.Orientation = CalculateOrientation(DockerOrientation(viewDockerIndex),
                                                                         buttonSpec.GetOrientation(_redirector));

                buttonView.ViewCenter.Orientation = DockerOrientation(viewDockerIndex);

                // Insert the button view into the docker
                AddViewToDocker(viewDockerIndex, GetDockStyle(buttonSpec), buttonView.ViewCenter, (_viewMetrics != null));

                // Perform any last construction steps for button spec
                ButtonSpecCreated(buttonSpec, buttonView, viewDockerIndex);

                // Hook in to the button spec change event
                buttonSpec.ButtonSpecPropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);

                return buttonView;
            }

            return null;
        }

        private void RemoveButtonSpec(ButtonSpec buttonSpec)
        {
            // Unhook from button spec events
            buttonSpec.ButtonSpecPropertyChanged -= new PropertyChangedEventHandler(OnPropertyChanged);

            // Get the button view from the button spec
            ButtonSpecView buttonView = _specLookup[buttonSpec];

            if (buttonView != null)
            {
                // Remove the view that was created for the button from its header
                if ((buttonView.ViewCenter.Parent != null) &&
                     buttonView.ViewCenter.Parent.Contains(buttonView.ViewCenter))
                {
                    buttonView.ViewCenter.Parent.Remove(buttonView.ViewCenter);
                }

                // Pull down the view for the button
                buttonView.Destruct();
            }
        }

        private void OnButtonSpecInserted(object sender, ButtonSpecEventArgs e)
        {
            RecreateAll();
            PerformNeedPaint(true);
        }

        private void OnButtonSpecRemoved(object sender, ButtonSpecEventArgs e)
        {
            RecreateAll();
            PerformNeedPaint(true);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Edge":
                case "Location":
                case "Orientation":
                case "Type":
                    RecreateAll();
                    PerformNeedPaint(true);
                    break;
            }
        }

        private int GetTargetDockerIndex(HeaderLocation location)
        {
            switch (location)
            {
                case HeaderLocation.PrimaryHeader:
                    return 0;
                case HeaderLocation.SecondaryHeader:
                    return 1;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    break;
            }

            return -1;
        }

        private ViewDockStyle GetDockStyle(ButtonSpec spec)
        {
            return (spec.GetEdge(_redirector) == RelativeEdgeAlign.Near ? ViewDockStyle.Left : ViewDockStyle.Right);
        }

        private VisualOrientation CalculateOrientation(VisualOrientation viewOrientation,
                                                       ButtonOrientation buttonOrientation)
        {
            switch (buttonOrientation)
            {
                case ButtonOrientation.FixedBottom:
                    return VisualOrientation.Bottom;
                case ButtonOrientation.FixedLeft:
                    return VisualOrientation.Left;
                case ButtonOrientation.FixedRight:
                    return VisualOrientation.Right;
                case ButtonOrientation.FixedTop:
                    return VisualOrientation.Top;
                case ButtonOrientation.Auto:
                default:
                    return viewOrientation;
            }
        }
        #endregion
    }
}
