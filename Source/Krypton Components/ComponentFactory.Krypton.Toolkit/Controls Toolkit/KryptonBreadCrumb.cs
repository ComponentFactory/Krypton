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
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Provides a flat navigation of hierarchical data.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonBreadCrumb), "ToolboxBitmaps.KryptonBreadCrumb.bmp")]
    [DefaultEvent("SelectedItemChanged")]
    [DefaultProperty("RootItem")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Flat navigation of hierarchical data.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonBreadCrumb : VisualSimpleBase,
                                     ISupportInitializeNotification
	{
        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAny instances.
        /// </summary>
        public class BreadCrumbButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the BreadCrumbButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public BreadCrumbButtonSpecCollection(KryptonBreadCrumb owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion
        
        #region Instance Fields
        private bool _initializing;
        private bool _initialized;
        private bool _allowButtonSpecToolTips;
        private bool _dropDownNavigaton;
        private ViewDrawDocker _drawDocker;
        private PaletteBreadCrumbRedirect _stateCommon;
        private PaletteBreadCrumbDoubleState _stateDisabled;
        private PaletteBreadCrumbDoubleState _stateNormal;
        private PaletteBreadCrumbState _stateTracking;
        private PaletteBreadCrumbState _statePressed;
        private BreadCrumbButtonSpecCollection _buttonSpecs;
        private ButtonSpecManagerDraw _buttonManager;
        private VisualPopupToolTip _visualPopupToolTip;
        private ToolTipManager _toolTipManager;
        private KryptonBreadCrumbItem _rootItem;
        private KryptonBreadCrumbItem _selectedItem;
        private ViewLayoutCrumbs _layoutCrumbs;
        private ButtonStyle _buttonStyle;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the control is initialized.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the control has been fully initialized.")]
        public event EventHandler Initialized;

        /// <summary>
        /// Occurs when the drop down portion of a bread crumb is pressed.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the drop down portion of a bread crumb is pressed.")]
        public event EventHandler<BreadCrumbMenuArgs> CrumbDropDown;

        /// <summary>
        /// Occurs when the drop down portion of the overflow buttonis pressed.
        /// </summary>
        [Category("Action")]
        [Description("Occurs when the drop down portion of the overflow button is pressed.")]
        public event EventHandler<ContextPositionMenuArgs> OverflowDropDown;

        /// <summary>
        /// Occurs when the value of the SelectedItem property changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the SelectedItem property changes.")]
        public event EventHandler SelectedItemChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonBreadCrumb class.
		/// </summary>
        public KryptonBreadCrumb()
		{
			// The bread crumb cannot take the focus
			SetStyle(ControlStyles.Selectable, false);

            // Set default values
            _selectedItem = null;
            _dropDownNavigaton = true;
            _buttonStyle = ButtonStyle.BreadCrumb;
            _rootItem = new KryptonBreadCrumbItem("Root");
            _rootItem.PropertyChanged += new PropertyChangedEventHandler(OnCrumbItemChanged);
            _allowButtonSpecToolTips = false;

			// Create storage objects
            _buttonSpecs = new BreadCrumbButtonSpecCollection(this);

			// Create the palette storage
            _stateCommon = new PaletteBreadCrumbRedirect(Redirector, NeedPaintDelegate);
            _stateDisabled = new PaletteBreadCrumbDoubleState(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteBreadCrumbDoubleState(_stateCommon, NeedPaintDelegate);
            _stateTracking = new PaletteBreadCrumbState(_stateCommon, NeedPaintDelegate);
            _statePressed = new PaletteBreadCrumbState(_stateCommon, NeedPaintDelegate);

			// Our view contains background and border with crumbs inside
            _layoutCrumbs = new ViewLayoutCrumbs(this, NeedPaintDelegate);
            _drawDocker = new ViewDrawDocker(_stateNormal.Back, _stateNormal.Border, null);
            _drawDocker.Add(_layoutCrumbs, ViewDockStyle.Fill);

			// Create the view manager instance
			ViewManager = new ViewManager(this, _drawDocker);

            // Create button specification collection manager
            _buttonManager = new ButtonSpecManagerDraw(this, Redirector, _buttonSpecs, null,
                                                       new ViewDrawDocker[] { _drawDocker },
                                                       new IPaletteMetric[] { _stateCommon },
                                                       new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetPrimary },
                                                       new PaletteMetricPadding[] { PaletteMetricPadding.None },
                                                       new GetToolStripRenderer(CreateToolStripRenderer),
                                                       NeedPaintDelegate);

            // Create the manager for handling tooltips
            _toolTipManager = new ToolTipManager();
            _toolTipManager.ShowToolTip += new EventHandler<ToolTipEventArgs>(OnShowToolTip);
            _toolTipManager.CancelToolTip += new EventHandler(OnCancelToolTip);
            _buttonManager.ToolTipManager = _toolTipManager;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remove any showing tooltip
                OnCancelToolTip(this, EventArgs.Empty);

                // Remember to pull down the manager instance
                _buttonManager.Destruct();
            }

            base.Dispose(disposing);
        }
		#endregion

		#region Public
        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        public virtual void BeginInit()
        {
            // Remember that fact we are inside a BeginInit/EndInit pair
            _initializing = true;
        }

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        public virtual void EndInit()
        {
            // We are now initialized
            _initialized = true;

            // We are no longer initializing
            _initializing = false;

            if (SelectedItem == null)
                SelectedItem = RootItem;

            OnNeedPaint(this, new NeedLayoutEventArgs(true));

            // Raise event to show control is now initialized
            OnInitialized(EventArgs.Empty);
        }

        /// <summary>
        /// Gets a value indicating if the control is initialized.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsInitialized
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _initialized; }
        }

        /// <summary>
        /// Gets a value indicating if the control is initialized.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsInitializing
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _initializing; }
        }
            
        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// Gets and sets the automatic resize of the control to fit contents.
        /// </summary>
        [Browsable(true)]
        [Localizable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether mnemonics will fire button spec buttons.
        /// </summary>
        [Category("Appearance")]
        [Description("Defines if mnemonic characters generate click events for button specs.")]
        [DefaultValue(true)]
        public bool UseMnemonic
        {
            get { return _buttonManager.UseMnemonic; }

            set
            {
                if (_buttonManager.UseMnemonic != value)
                {
                    _buttonManager.UseMnemonic = value;
                    PerformNeedPaint(true);
                }
            }
        }

		/// <summary>
		/// Gets the collection of button specifications.
		/// </summary>
        [Category("Visuals")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BreadCrumbButtonSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Gets and sets a value indicating if drop down buttons should allow navigation to children.
        /// </summary>
        [Category("Visuals")]
        [Description("Should drop down buttons allow navigation to children.")]
        [DefaultValue(true)]
        public bool DropDownNavigation
        {
            get { return _dropDownNavigaton; }
            
            set 
            {
                if (_dropDownNavigaton != value)
                {
                    _dropDownNavigaton = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _allowButtonSpecToolTips; }
            set { _allowButtonSpecToolTips = value; }
        }

		/// <summary>
		/// Gets and sets the background style for the control.
		/// </summary>
		[Category("Visuals")]
		[Description("Background style for the control.")]
		public PaletteBackStyle ControlBackStyle
		{
            get { return _stateCommon.BackStyle; }

            set
            {
                if (_stateCommon.BackStyle != value)
                {
                    _stateCommon.BackStyle = value;
                    PerformNeedPaint(true);
                }
            }
		}

        private bool ShouldSerializeControlBackStyle()
        {
            return (ControlBackStyle != PaletteBackStyle.PanelAlternate);
        }

        private void ResetControlBackStyle()
        {
            ControlBackStyle = PaletteBackStyle.PanelAlternate;
        }

        /// <summary>
        /// Gets and sets the button style for drawing each bread crumb.
        /// </summary>
        [Category("Visuals")]
        [Description("Button style used for drawing each bread crumb.")]
        public ButtonStyle CrumbButtonStyle
        {
            get { return _buttonStyle; }

            set
            {
                if (_buttonStyle != value)
                {
                    _buttonStyle = value;
                    _stateCommon.BreadCrumb.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeCrumbButtonStyle()
        {
            return (CrumbButtonStyle != ButtonStyle.BreadCrumb);
        }

        private void ResetCrumbButtonStyle()
        {
            CrumbButtonStyle = ButtonStyle.BreadCrumb;
        }

        /// <summary>
        /// Gets and sets the border style for the control.
        /// </summary>
        [Category("Visuals")]
        [Description("Border style for the control.")]
        [DefaultValue(typeof(PaletteBorderStyle), "Control - Client")]
        public PaletteBorderStyle ControlBorderStyle
        {
            get { return _stateCommon.BorderStyle; }

            set
            {
                if (_stateCommon.BorderStyle != value)
                {
                    _stateCommon.BorderStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private bool ShouldSerializeControlBorderStyle()
        {
            return (ControlBorderStyle != PaletteBorderStyle.ControlClient);
        }

        private void ResetControlBorderStyle()
        {
            ControlBorderStyle = PaletteBorderStyle.ControlClient;
        }

        /// <summary>
        /// Gets and sets the root bread crumb item.
        /// </summary>
        [Category("Data")]
        [Description("Root bread crumb item.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonBreadCrumbItem RootItem
        {
            get { return _rootItem; }
        }

        /// <summary>
        /// Gets and sets the selected bread crumb item.
        /// </summary>
        [Category("Data")]
        [Description("Currently selected bread crumb item.")]
        [DefaultValue(null)]
        public KryptonBreadCrumbItem SelectedItem
        {
            get { return _selectedItem; }
            
            set 
            {
                if (value != _selectedItem)
                {
                    // Check that the item has a chain that ends at our root item or is null
                    KryptonBreadCrumbItem temp = value;
                    while ((temp != null) && (temp != RootItem))
                        temp = temp.Parent;

                    if ((value != null) && (temp == null))
                        throw new ArgumentOutOfRangeException("value", "Item must be inside the RootItem hierarchy.");

                    _selectedItem = value;
                    OnSelectedItemChanged(EventArgs.Empty);
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets access to the common bread crumb appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common bread crumb appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBreadCrumbRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        
        /// <summary>
        /// Gets access to the disabled appearance entries.
		/// </summary>
		[Category("Visuals")]
        [Description("Overrides for defining disabled appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBreadCrumbDoubleState StateDisabled
		{
			get { return _stateDisabled; }
		}

		private bool ShouldSerializeStateDisabled()
		{
			return !_stateDisabled.IsDefault;
		}

		/// <summary>
        /// Gets access to the normal appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBreadCrumbDoubleState StateNormal
		{
			get { return _stateNormal; }
		}

		private bool ShouldSerializeStateNormal()
		{
			return !_stateNormal.IsDefault;
		}

        /// <summary>
        /// Gets access to the tracking bread crumb appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tracking bread crumb appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBreadCrumbState StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed bread crumb appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed bread crumb appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBreadCrumbState StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the ToolTipManager used for displaying tool tips.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTipManager ToolTipManager
        {
            get { return _toolTipManager; }
        }

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            // Request fixed state from the view
            _drawDocker.FixedState = state;
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool DesignerGetHitTest(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return false;

            // Check if any of the button specs want the point
            if ((_buttonManager != null) && _buttonManager.DesignerGetHitTest(pt))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public Component DesignerComponentFromPoint(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return null;

            // Ask the current view for a decision
            return ViewManager.ComponentFromPoint(pt);
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public void DesignerMouseLeave()
        {
            // Simulate the mouse leaving the control so that the tracking
            // element that thinks it has the focus is informed it does not
            OnMouseLeave(EventArgs.Empty);
        }
        #endregion

		#region Protected Overrides
        /// <summary>
        /// Create the redirector instance.
        /// </summary>
        /// <returns>PaletteRedirect derived class.</returns>
        protected override PaletteRedirect CreateRedirector()
        {
            return new PaletteRedirectBreadCrumb(base.CreateRedirector());
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The mnemonic character entered.</param>
        /// <returns>true if the mnemonic was processsed; otherwise, false.</returns>
        protected override bool ProcessMnemonic(char charCode)
        {
            // If the button manager wants to process mnemonic characters and
            // this control is currently visible and enabled then...
            if (UseMnemonic && CanProcessMnemonic())
            {
                // Pass request onto the button spec manager
                if (_buttonManager.ProcessMnemonic(charCode))
                    return true;
            }

            // No match found, let base class do standard processing
            return base.ProcessMnemonic(charCode);
        }

		/// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			// Push correct palettes into the view
			if (Enabled)
				_drawDocker.SetPalettes(_stateNormal.Back, _stateNormal.Border);
			else
				_drawDocker.SetPalettes(_stateDisabled.Back, _stateDisabled.Border);

            _drawDocker.Enabled = Enabled;

            // Update state to reflect change in enabled state
            _buttonManager.RefreshButtons();

			// Change in enabled state requires a layout and repaint
			PerformNeedPaint(true);

			// Let base class fire standard event
			base.OnEnabledChanged(e);
		}

        /// <summary>
		/// Gets the default size of the control.
		/// </summary>
		protected override Size DefaultSize
		{
			get { return new Size(200, 28); }
		}

        /// <summary>
        /// Processes a notification from palette storage of a button spec change.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnButtonSpecChanged(object sender, EventArgs e)
        {
            // Recreate all the button specs with new values
            _buttonManager.RecreateButtons();

            // Let base class perform standard processing
            base.OnButtonSpecChanged(sender, e);
        }
		#endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the CrumbDropDown event.
        /// </summary>
        /// <param name="e">An ContextPositionMenuArgs containing the event data.</param>
        internal protected virtual void OnCrumbDropDown(BreadCrumbMenuArgs e)
        {
            if (CrumbDropDown != null)
                CrumbDropDown(this, e);
        }

        /// <summary>
        /// Raises the OverflowDropDown event.
        /// </summary>
        /// <param name="e">An ContextPositionMenuArgs containing the event data.</param>
        internal protected virtual void OnOverflowDropDown(ContextPositionMenuArgs e)
        {
            if (OverflowDropDown != null)
                OverflowDropDown(this, e);
        }

        /// <summary>
        /// Raises the SelectedItemChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnSelectedItemChanged(EventArgs e)
        {
            if (SelectedItemChanged != null)
                SelectedItemChanged(this, e);
        }

        /// <summary>
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnInitialized(EventArgs e)
        {
            if (Initialized != null)
                Initialized(this, EventArgs.Empty);
        }
        #endregion

        #region Internal
        internal PaletteBreadCrumbRedirect GetStateCommon()
        {
            return _stateCommon;
        }

        internal PaletteRedirect GetRedirector()
        {
            return Redirector;
        }
        #endregion

        #region Implementation
        private void OnCrumbItemChanged(object sender, PropertyChangedEventArgs e)
        {
            // A change in the selected item hierarchy...
            if (e.PropertyName == "Items")
            {
                // And we have a selected item...
                if (SelectedItem != null)
                {
                    // Check that the current selected item has a chain that ends at our root
                    KryptonBreadCrumbItem temp = SelectedItem;
                    while ((temp != null) && (temp != RootItem))
                        temp = temp.Parent;

                    // If selected item is no longer valid, then reset back to null
                    if (temp == null)
                        SelectedItem = null;
               }
            }

            // Relayout and paint to reflect change in crumb settings
            PerformNeedPaint(true);
        }

        private void OnShowToolTip(object sender, ToolTipEventArgs e)
        {
            if (!IsDisposed)
            {
                // Do not show tooltips when the form we are in does not have focus
                Form topForm = FindForm();
                if ((topForm != null) && !topForm.ContainsFocus)
                    return;

                // Never show tooltips are design time
                if (!DesignMode)
                {
                    IContentValues sourceContent = null;
                    LabelStyle toolTipStyle = LabelStyle.ToolTip;

                    // Find the button spec associated with the tooltip request
                    ButtonSpec buttonSpec = _buttonManager.ButtonSpecFromView(e.Target);

                    // If the tooltip is for a button spec
                    if (buttonSpec != null)
                    {
                        // Are we allowed to show page related tooltips
                        if (AllowButtonSpecToolTips)
                        {
                            // Create a helper object to provide tooltip values
                            ButtonSpecToContent buttonSpecMapping = new ButtonSpecToContent(Redirector, buttonSpec);

                            // Is there actually anything to show for the tooltip
                            if (buttonSpecMapping.HasContent)
                            {
                                sourceContent = buttonSpecMapping;
                                toolTipStyle = buttonSpec.ToolTipStyle;
                            }
                        }
                    }

                    if (sourceContent != null)
                    {
                        // Remove any currently showing tooltip
                        if (_visualPopupToolTip != null)
                            _visualPopupToolTip.Dispose();

                        // Create the actual tooltip popup object
                        _visualPopupToolTip = new VisualPopupToolTip(Redirector,
                                                                     sourceContent,
                                                                     Renderer,
                                                                     PaletteBackStyle.ControlToolTip,
                                                                     PaletteBorderStyle.ControlToolTip,
                                                                     CommonHelper.ContentStyleFromLabelStyle(toolTipStyle));

                        _visualPopupToolTip.Disposed += new EventHandler(OnVisualPopupToolTipDisposed);

                        // Show relative to the provided screen rectangle
                        _visualPopupToolTip.ShowCalculatingSize(RectangleToScreen(e.Target.ClientRectangle));
                    }
                }
            }
        }

        private void OnCancelToolTip(object sender, EventArgs e)
        {
            // Remove any currently showing tooltip
            if (_visualPopupToolTip != null)
                _visualPopupToolTip.Dispose();
        }

        private void OnVisualPopupToolTipDisposed(object sender, EventArgs e)
        {
            // Unhook events from the specific instance that generated event
            VisualPopupToolTip popupToolTip = (VisualPopupToolTip)sender;
            popupToolTip.Disposed -= new EventHandler(OnVisualPopupToolTipDisposed);

            // Not showing a popup page any more
            _visualPopupToolTip = null;
        }
        #endregion
    }
}
