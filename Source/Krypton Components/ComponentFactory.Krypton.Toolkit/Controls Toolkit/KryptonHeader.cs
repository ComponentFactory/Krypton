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
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Provides a description for a section of your form.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonHeader), "ToolboxBitmaps.KryptonHeader.bmp")]
    [DefaultEvent("Paint")]
	[DefaultProperty("Text")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonHeaderDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Display a descriptive caption.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonHeader : VisualSimpleBase
	{
        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAny instances.
        /// </summary>
        public class HeaderButtonSpecCollection : ButtonSpecCollection<ButtonSpecAny> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the HeaderButtonSpecCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public HeaderButtonSpecCollection(KryptonHeader owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion
        
        #region Instance Fields
        private bool _allowButtonSpecToolTips;
        private HeaderStyle _style;
		private HeaderValues _headerValues;
        private VisualOrientation _orientation;
        private ViewDrawDocker _drawDocker;
		private ViewDrawContent _drawContent;
        private PaletteHeaderRedirect _stateCommon;
        private PaletteTripleMetric _stateDisabled;
        private PaletteTripleMetric _stateNormal;
        private HeaderButtonSpecCollection _buttonSpecs;
        private ButtonSpecManagerDraw _buttonManager;
        private VisualPopupToolTip _visualPopupToolTip;
        private ToolTipManager _toolTipManager;
        #endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the KryptonHeader class.
		/// </summary>
		public KryptonHeader()
		{
			// The header cannot take the focus
			SetStyle(ControlStyles.Selectable, false);

			// Set default values
            _style = HeaderStyle.Primary;
            _orientation = VisualOrientation.Top;
            _allowButtonSpecToolTips = false;

			// Create storage objects
            _headerValues = new HeaderValues(NeedPaintDelegate);
            _headerValues.TextChanged += new EventHandler(OnHeaderTextChanged);
            _buttonSpecs = new HeaderButtonSpecCollection(this);

			// Create the palette storage
            _stateCommon = new PaletteHeaderRedirect(Redirector, PaletteBackStyle.HeaderPrimary, PaletteBorderStyle.HeaderPrimary, PaletteContentStyle.HeaderPrimary, NeedPaintDelegate);
            _stateDisabled = new PaletteTripleMetric(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteTripleMetric(_stateCommon, NeedPaintDelegate);

			// Our view contains background and border with content inside
			_drawDocker = new ViewDrawDocker(_stateNormal.Back, _stateNormal.Border, null);
			_drawContent = new ViewDrawContent(_stateNormal.Content, _headerValues, Orientation);
            _drawDocker.Add(_drawContent, ViewDockStyle.Fill);

			// Create the view manager instance
			ViewManager = new ViewManager(this, _drawDocker);

            // Create button specification collection manager
            _buttonManager = new ButtonSpecManagerDraw(this, Redirector, _buttonSpecs, null,
                                                       new ViewDrawDocker[] { _drawDocker },
                                                       new IPaletteMetric[] { _stateCommon },
                                                       new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetPrimary },
                                                       new PaletteMetricPadding[] { PaletteMetricPadding.HeaderButtonPaddingPrimary },
                                                       new GetToolStripRenderer(CreateToolStripRenderer),
                                                       NeedPaintDelegate);

            // Create the manager for handling tooltips
            _toolTipManager = new ToolTipManager();
            _toolTipManager.ShowToolTip += new EventHandler<ToolTipEventArgs>(OnShowToolTip);
            _toolTipManager.CancelToolTip += new EventHandler(OnCancelToolTip);
            _buttonManager.ToolTipManager = _toolTipManager;

            // We want to be auto sized by default, but not the property default!
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
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
        /// Gets and sets the automatic resize of the control to fit contents.
        /// </summary>
        [Browsable(true)]
        [Localizable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        /// <summary>
        /// Gets and sets the auto size mode.
        /// </summary>
        [DefaultValue(typeof(AutoSizeMode), "GrowAndShrink")]
        public new AutoSizeMode AutoSizeMode
        {
            get { return base.AutoSizeMode; }
            set { base.AutoSizeMode = value; }
        }

        /// <summary>
        /// Gets and sets the internal padding space.
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        /// <summary>
		/// Gets or sets the text associated with this control. 
		/// </summary>
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public override string Text
		{
			get
			{
				// Map onto the heading property from the values
				return _headerValues.Heading;
			}
				
			set
			{
				// Map onto the heading property from the values
				_headerValues.Heading = value;
			}
		}

		private bool ShouldSerializeText()
		{
			// Never serialize, let the header values serialize instead
			return false;
		}

		/// <summary>
		/// Resets the Text property to its default value.
		/// </summary>
		public override void ResetText()
		{
			// Map onto the heading property from the values
			_headerValues.ResetHeading();
		}

        /// <summary>
        /// Gets and sets the visual orientation of the control.
        /// </summary>
        [Category("Visuals")]
        [Description("Visual orientation of the control.")]
        [DefaultValue(typeof(VisualOrientation), "Top")]
        public virtual VisualOrientation Orientation
		{
            get { return _orientation; }

			set
			{
                if (_orientation != value)
				{
                    _orientation = value;
                    
                    // Update the associated visual elements that are effected
					_drawDocker.Orientation = value;
					_drawContent.Orientation = value;
                    _buttonManager.RecreateButtons();

                    PerformNeedPaint(true);
				}
			}
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
        public HeaderButtonSpecCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
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
		/// Gets and sets the header style.
		/// </summary>
		[Category("Visuals")]
		[Description("Header style.")]
        [DefaultValue(typeof(HeaderStyle), "Primary")]
		public HeaderStyle HeaderStyle
		{
			get { return _style; }

			set
			{
				if (_style != value)
				{
					_style = value;
                    _stateCommon.SetStyles(_style);

					// Update the drawing to reflect style change
					switch (_style)
					{
                        case HeaderStyle.Primary:
                            _buttonManager.SetDockerMetrics(_drawDocker, _stateCommon, 
                                                            PaletteMetricInt.HeaderButtonEdgeInsetPrimary,
                                                            PaletteMetricPadding.HeaderButtonPaddingPrimary);
							break;
                        case HeaderStyle.Secondary:
                            _buttonManager.SetDockerMetrics(_drawDocker, _stateCommon,
                                                            PaletteMetricInt.HeaderButtonEdgeInsetSecondary,
                                                            PaletteMetricPadding.HeaderButtonPaddingSecondary);
                            break;
                        case HeaderStyle.DockActive:
                            _buttonManager.SetDockerMetrics(_drawDocker, _stateCommon,
                                                            PaletteMetricInt.HeaderButtonEdgeInsetDockActive,
                                                            PaletteMetricPadding.HeaderButtonPaddingDockActive);
                            break;
                        case HeaderStyle.DockInactive:
                            _buttonManager.SetDockerMetrics(_drawDocker, _stateCommon,
                                                            PaletteMetricInt.HeaderButtonEdgeInsetDockInactive,
                                                            PaletteMetricPadding.HeaderButtonPaddingDockInactive);
                            break;
                        case HeaderStyle.Form:
                            _buttonManager.SetDockerMetrics(_drawDocker, _stateCommon,
                                                            PaletteMetricInt.HeaderButtonEdgeInsetForm,
                                                            PaletteMetricPadding.HeaderButtonPaddingForm);
                            break;
                        case HeaderStyle.Calendar:
                            _buttonManager.SetDockerMetrics(_drawDocker, _stateCommon,
                                                            PaletteMetricInt.HeaderButtonEdgeInsetCalendar,
                                                            PaletteMetricPadding.HeaderButtonPaddingCalendar);
                            break;
                        case HeaderStyle.Custom1:
                            _buttonManager.SetDockerMetrics(_drawDocker, _stateCommon,
                                                            PaletteMetricInt.HeaderButtonEdgeInsetCustom1,
                                                            PaletteMetricPadding.HeaderButtonPaddingCustom1);
                            break;
                        case HeaderStyle.Custom2:
                            _buttonManager.SetDockerMetrics(_drawDocker, _stateCommon,
                                                            PaletteMetricInt.HeaderButtonEdgeInsetCustom2,
                                                            PaletteMetricPadding.HeaderButtonPaddingCustom2);
                            break;
                        default:
                            // Should never happen!
                            Debug.Assert(false);
                            break;
                    }

					PerformNeedPaint(true);
				}
			}
		}

        private void ResetHeaderStyle()
        {
            HeaderStyle = HeaderStyle.Primary;
        }

        private bool ShouldSerializeHeaderStyle()
        {
            return (HeaderStyle != HeaderStyle.Primary);
        }

		/// <summary>
		/// Gets access to the header content.
		/// </summary>
		[Category("Visuals")]
		[Description("Header values")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public HeaderValues Values
		{
			get { return _headerValues; }
		}

		private bool ShouldSerializeValues()
		{
			return !_headerValues.IsDefault;
		}

        /// <summary>
        /// Gets access to the common header appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common header appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteHeaderRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        
        /// <summary>
		/// Gets access to the disabled header appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining disabled header appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleMetric StateDisabled
		{
			get { return _stateDisabled; }
		}

		private bool ShouldSerializeStateDisabled()
		{
			return !_stateDisabled.IsDefault;
		}

		/// <summary>
		/// Gets access to the normal header appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal header appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleMetric StateNormal
		{
			get { return _stateNormal; }
		}

		private bool ShouldSerializeStateNormal()
		{
			return !_stateNormal.IsDefault;
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
        /// Gets access to the ToolTipManager used for displaying tool tips.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTipManager ToolTipManager
        {
            get { return _toolTipManager; }
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
			{
				_drawDocker.SetPalettes(_stateNormal.Back, _stateNormal.Border);
				_drawContent.SetPalette(_stateNormal.Content);
			}
			else
			{
				_drawDocker.SetPalettes(_stateDisabled.Back, _stateDisabled.Border);
				_drawContent.SetPalette(_stateDisabled.Content);
			}

            _drawDocker.Enabled = Enabled;
            _drawContent.Enabled = Enabled;

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
			get { return new Size(240, 30); }
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

        #region Implementation
        private void OnHeaderTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
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
