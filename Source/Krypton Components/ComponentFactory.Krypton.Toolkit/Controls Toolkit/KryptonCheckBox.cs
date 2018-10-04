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
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Display check box with text and images with the styling features of the Krypton Toolkit
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonCheckBox), "ToolboxBitmaps.KryptonCheckBox.bmp")]
    [DefaultEvent("CheckedChanged")]
	[DefaultProperty("Text")]
    [DefaultBindingProperty("CheckState")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonCheckBoxDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Allow user to set or clear the associated option.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonCheckBox : VisualSimpleBase, IContentValues
	{
		#region Instance Fields
		private LabelStyle _style;
		private LabelValues _labelValues;
        private VisualOrientation _orientation;
        private CheckBoxController _controller;
        private ViewLayoutDocker _layoutDocker;
        private ViewLayoutCenter _layoutCenter;
        private ViewDrawCheckBox _drawCheckBox;
        private ViewDrawContent _drawContent;
		private PaletteContentInheritRedirect _paletteCommonRedirect;
        private PaletteRedirectCheckBox _paletteCheckBoxImages;
        private PaletteContent _stateCommon;
        private PaletteContent _stateDisabled;
		private PaletteContent _stateNormal;
        private PaletteContent _stateFocus;
        private PaletteContentInheritOverride _overrideNormal;
        private KryptonCommand _command;
        private CheckBoxImages _images;
        private VisualOrientation _checkPosition;
        private CheckState _checkState;
        private CheckState _wasCheckState;
        private bool _wasEnabled;
        private bool _checked;
        private bool _threeState;
        private bool _autoCheck;
        private bool _useMnemonic;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the control is double clicked with the mouse.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler DoubleClick;

        /// <summary>
        /// Occurs when the control is mouse double clicked with the mouse.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler MouseDoubleClick;

        /// <summary>
        /// Occurs when the value of the ImeMode property is changed.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler ImeModeChanged;

        /// <summary>
        /// Occurs when the value of the Checked property has changed.
        /// </summary>
        [Category("Misc")]
        [Description("Occurs whenever the Checked property has changed.")]
        public event EventHandler CheckedChanged;

        /// <summary>
        /// Occurs when the value of the CheckState property has changed.
        /// </summary>
        [Category("Misc")]
        [Description("Occurs whenever the CheckState property has changed.")]
        public event EventHandler CheckStateChanged;

        /// <summary>
        /// Occurs when the value of the KryptonCommand property changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the KryptonCommand property changes.")]
        public event EventHandler KryptonCommandChanged;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonCheckBox class.
		/// </summary>
        public KryptonCheckBox()
		{
            // Turn off standard click and double click events, we do that manually
			SetStyle(ControlStyles.StandardClick |
					 ControlStyles.StandardDoubleClick, false);
					 
			// Set default properties
            _style = LabelStyle.NormalControl;
            _orientation = VisualOrientation.Top;
            _checkPosition = VisualOrientation.Left;
            _checked = false;
            _threeState = false;
            _checkState = CheckState.Unchecked;
            _useMnemonic = true;
            _autoCheck = true;

			// Create content storage
            _labelValues = new LabelValues(NeedPaintDelegate);
            _labelValues.TextChanged += new EventHandler(OnCheckBoxTextChanged);
            _images = new CheckBoxImages(NeedPaintDelegate);

			// Create palette redirector
            _paletteCommonRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalControl);
            _paletteCheckBoxImages = new PaletteRedirectCheckBox(Redirector, _images);

			// Create the palette provider
            _stateCommon = new PaletteContent(_paletteCommonRedirect, NeedPaintDelegate);
            _stateDisabled = new PaletteContent(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteContent(_stateCommon, NeedPaintDelegate);
            _stateFocus = new PaletteContent(_paletteCommonRedirect, NeedPaintDelegate);

            // Override the normal values with the focus, when the control has focus
            _overrideNormal = new PaletteContentInheritOverride(_stateFocus, _stateNormal, PaletteState.FocusOverride, false);

			// Our view contains background and border with content inside
            _drawContent = new ViewDrawContent(_overrideNormal, this, VisualOrientation.Top);
            _drawContent.UseMnemonic = _useMnemonic;

            // Only draw a focus rectangle when focus cues are needed in the top level form
            _drawContent.TestForFocusCues = true;

            // Create the check box image drawer and place inside element so it is always centered
            _drawCheckBox = new ViewDrawCheckBox(_paletteCheckBoxImages);
            _drawCheckBox.CheckState = _checkState;
            _layoutCenter = new ViewLayoutCenter();
            _layoutCenter.Add(_drawCheckBox);

            // Place check box on the left and the label in the remainder
            _layoutDocker = new ViewLayoutDocker();
            _layoutDocker.Add(_layoutCenter, ViewDockStyle.Left);
            _layoutDocker.Add(_drawContent, ViewDockStyle.Fill);

            // Need a controller for handling mouse input
            _controller = new CheckBoxController(_drawCheckBox, _layoutDocker, NeedPaintDelegate);
            _controller.Click += new EventHandler(OnControllerClick);
            _controller.Enabled = true;
            _layoutDocker.MouseController = _controller;
            _layoutDocker.KeyController = _controller;

            // Change the layout to match the inital right to left setting and orientation
            UpdateForOrientation();

			// Create the view manager instance
            ViewManager = new ViewManager(this, _layoutDocker);

			// We want to be auto sized by default, but not the property default!
			AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
		}
		#endregion

		#region Public
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
        /// Gets and sets the mode for when auto sizing.
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
				// Map onto the text property from the label values
				return _labelValues.Text;
			}
				
			set
			{
				// Map onto the text property from the label values
				_labelValues.Text = value;
			}
		}

		private bool ShouldSerializeText()
		{
			// Never serialize, let the label values serialize instead
			return false;
		}

		/// <summary>
		/// Resets the Text property to its default value.
		/// </summary>
		public override void ResetText()
		{
			// Map onto the text property from the label values
			_labelValues.ResetText();
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

					// Update the associated visual element that is effected
					_drawContent.Orientation = value;

                    // Update the layout according to the new orientation value
                    UpdateForOrientation();

                    PerformNeedPaint(true);
				}
			}
		}

        /// <summary>
        /// Gets and sets the position of the check box.
        /// </summary>
        [Category("Visuals")]
        [Description("Visual position of the check box.")]
        [DefaultValue(typeof(VisualOrientation), "Left")]
        [Localizable(true)]
        public virtual VisualOrientation CheckPosition
        {
            get { return _checkPosition; }

            set
            {
                if (_checkPosition != value)
                {
                    _checkPosition = value;

                    // Update the layout according to the new orientation value
                    UpdateForOrientation();

                    PerformNeedPaint(true);
                }
            }
        }
        
        /// <summary>
		/// Gets and sets the label style.
		/// </summary>
		[Category("Visuals")]
		[Description("Label style.")]
		public LabelStyle LabelStyle
		{
			get { return _style; }

			set
			{
				if (_style != value)
				{
					_style = value;
                    SetLabelStyle(_style);
					PerformNeedPaint(true);
				}
			}
		}

        private void ResetLabelStyle()
        {
            LabelStyle = LabelStyle.NormalControl;
        }

        private bool ShouldSerializeLabelStyle()
        {
            return (LabelStyle != LabelStyle.NormalControl);
        }
        
        /// <summary>
		/// Gets access to the label content.
		/// </summary>
		[Category("Visuals")]
		[Description("Label values")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public LabelValues Values
		{
			get { return _labelValues; }
		}

		private bool ShouldSerializeValues()
		{
			return !_labelValues.IsDefault;
		}

        /// <summary>
        /// Gets access to the image value overrides.
        /// </summary>
        [Category("Visuals")]
        [Description("Image value overrides.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CheckBoxImages Images
        {
            get { return _images; }
        }

        private bool ShouldSerializeImages()
        {
            return !_images.IsDefault;
        }

        /// <summary>
        /// Gets access to the common label appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common label appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

		/// <summary>
		/// Gets access to the disabled label appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining disabled label appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteContent StateDisabled
		{
			get { return _stateDisabled; }
		}

		private bool ShouldSerializeStateDisabled()
		{
			return !_stateDisabled.IsDefault;
		}

		/// <summary>
		/// Gets access to the normal label appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal label appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteContent StateNormal
		{
			get { return _stateNormal; }
		}

		private bool ShouldSerializeStateNormal()
		{
			return !_stateNormal.IsDefault;
		}

        /// <summary>
        /// Gets access to the label appearance when it has focus.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining label appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideFocus
        {
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_stateFocus.IsDefault;
        }

        /// <summary>
        /// Gets or sets a value indicating whether an ampersand is included in the text of the control. 
        /// </summary>
        [Category("Appearance")]
        [Description("When true the first character after an ampersand will be used as a mnemonic.")]
        [DefaultValue(true)]
        public bool UseMnemonic
        {
            get { return _useMnemonic; }

            set
            {
                if (_useMnemonic != value)
                {
                    _useMnemonic = value;
                    _drawContent.UseMnemonic = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the component is in the checked state.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates if the component is in the checked state.")]
        [DefaultValue(false)]
        [Bindable(true)]
        public bool Checked
        {
            get { return _checked; }

            set
            {
                if (_checked != value)
                {
                    // Store new values
                    _checked = value;
                    _checkState = (_checked ? CheckState.Checked : CheckState.Unchecked);

                    // Generate events
                    OnCheckedChanged(EventArgs.Empty);
                    OnCheckStateChanged(EventArgs.Empty);

                    // Repaint
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the check box is automatically changed state when clicked. 
        /// </summary>
        [Category("Behavior")]
        [Description("Causes the check box to automatically change state when clicked.")]
        [DefaultValue(true)]
        public bool AutoCheck
        {
            get { return _autoCheck; }
            set { _autoCheck = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating if the component allows three states instead of two.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if the component allows three states instead of two.")]
        [DefaultValue(false)]
        public bool ThreeState
        {
            get { return _threeState; }

            set
            {
                if (_threeState != value)
                {
                    _threeState = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the checked state of the component.
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the checked state of the component.")]
        [DefaultValue(typeof(CheckState), "Unchecked")]
        [Bindable(true)]
        public CheckState CheckState
        {
            get { return _checkState; }

            set
            {
                if (_checkState != value)
                {
                    // Store new values
                    _checkState = value;
                    bool newChecked = (_checkState != CheckState.Unchecked);
                    bool checkedChanged = (_checked != newChecked);
                    _checked = newChecked;

                    // Generate events
                    if (checkedChanged)
                        OnCheckedChanged(EventArgs.Empty);

                    OnCheckStateChanged(EventArgs.Empty);

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the check button.")]
        [DefaultValue(null)]
        public virtual KryptonCommand KryptonCommand
        {
            get { return _command; }

            set
            {
                if (_command != value)
                {
                    if (_command != null)
                        _command.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);
                    else
                    {
                        _wasEnabled = Enabled;
                        _wasCheckState = CheckState;
                    }

                    _command = value;
                    OnKryptonCommandChanged(EventArgs.Empty);

                    if (_command != null)
                        _command.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);
                    else
                    {
                        Enabled = _wasEnabled;
                        CheckState = _wasCheckState;
                    }
                }
            }
        }

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="focus">Focus state for display.</param>
        /// <param name="enabled">Enabled state for display.</param>
        /// <param name="tracking">Tracking state for display.</param>
        /// <param name="pressed">Pressed state for display.</param>
        public virtual void SetFixedState(bool focus,
                                          bool enabled,
                                          bool tracking,
                                          bool pressed)
        {
            // Prevent controller from changing drawing state
            _controller.Enabled = false;
            
            // Request fixed state from the view
            _overrideNormal.Apply = focus;
            _drawContent.FixedState = (enabled ? PaletteState.Normal : PaletteState.Disabled);
            _drawCheckBox.Enabled = enabled;
            _drawCheckBox.Tracking = tracking;
            _drawCheckBox.Pressed = pressed;
        }
        #endregion

        #region IContentValues
        /// <summary>
        /// Gets the content short text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetShortText()
        {
            if (KryptonCommand != null)
                return KryptonCommand.Text;
            else
                return _labelValues.GetShortText();
        }

        /// <summary>
        /// Gets the content long text.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLongText()
        {
            if (KryptonCommand != null)
                return KryptonCommand.ExtraText;
            else
                return _labelValues.GetLongText();
        }

        /// <summary>
        /// Gets the content image.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Image value.</returns>
        public Image GetImage(PaletteState state)
        {
            if (KryptonCommand != null)
                return KryptonCommand.ImageSmall;
            else
                return _labelValues.GetImage(state);
        }

        /// <summary>
        /// Gets the image color that should be transparent.
        /// </summary>
        /// <param name="state">The state for which the image is needed.</param>
        /// <returns>Color value.</returns>
        public Color GetImageTransparentColor(PaletteState state)
        {
            if (KryptonCommand != null)
                return KryptonCommand.ImageTransparentColor;
            else
                return _labelValues.GetImageTransparentColor(state);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the DoubleClick event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnDoubleClick(EventArgs e)
        {
            if (DoubleClick != null)
                DoubleClick(this, e);
        }

        /// <summary>
        /// Raises the MouseDoubleClick event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnMouseDoubleClick(EventArgs e)
        {
            if (MouseDoubleClick != null)
                MouseDoubleClick(this, e);
        }

        /// <summary>
        /// Raises the ImeModeChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnMouseImeModeChanged(EventArgs e)
        {
            if (ImeModeChanged != null)
                ImeModeChanged(this, e);
        }

        /// <summary>
        /// Raises the CheckedChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanged != null)
                CheckedChanged(this, e);
        }

        /// <summary>
        /// Raises the CheckStateChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCheckStateChanged(EventArgs e)
        {
            // Update the checked state that is drawn
            _drawCheckBox.CheckState = _checkState;

            if (CheckStateChanged != null)
                CheckStateChanged(this, e);

            // If there is a command associated then update with new state
            if (KryptonCommand != null)
                KryptonCommand.CheckState = CheckState;
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            if (!_drawContent.IsFixed)
            {
                // Apply the focus overrides
                _overrideNormal.Apply = true;

                // Change in focus requires a repaint
                PerformNeedPaint(false);
            }

            // Let base class fire standard event
            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises the KryptonCommandChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnKryptonCommandChanged(EventArgs e)
        {
            if (KryptonCommandChanged != null)
                KryptonCommandChanged(this, e);

            // Use the values from the new command
            if (KryptonCommand != null)
            {
                Enabled = KryptonCommand.Enabled;
                CheckState = KryptonCommand.CheckState;
            }

            // Redraw to update the text/extratext/image properties
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Handles a change in the property of an attached command.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PropertyChangedEventArgs that contains the event data.</param>
        protected virtual void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Enabled":
                    Enabled = KryptonCommand.Enabled;
                    break;
                case "CheckState":
                    CheckState = KryptonCommand.CheckState;
                    break;
                case "Text":
                case "ExtraText":
                case "ImageSmall":
                case "ImageTransparentColor":
                    PerformNeedPaint(true);
                    break;
            }
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            if (!_drawContent.IsFixed)
            {
                // Apply the focus overrides
                _overrideNormal.Apply = false;

                // Change in focus requires a repaint
                PerformNeedPaint(false);
            }

            // Let base class fire standard event
            base.OnLostFocus(e);
        }

        /// <summary>
        /// Raises the CheckStateChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            if (AutoCheck)
            {
                // Change state based on the current state
                switch (CheckState)
                {
                    case CheckState.Unchecked:
                        CheckState = CheckState.Checked;
                        break;
                    case CheckState.Checked:
                        CheckState = (ThreeState ? CheckState.Indeterminate : CheckState.Unchecked);
                        break;
                    case CheckState.Indeterminate:
                        CheckState = CheckState.Unchecked;
                        break;
                }
            }

            base.OnClick(e);

            // If we have an attached command then execute it
            if (KryptonCommand != null)
                KryptonCommand.PerformExecute();
        }

        /// <summary>
        /// Update the view elements based on the requested label style.
        /// </summary>
        /// <param name="style">New label style.</param>
        protected virtual void SetLabelStyle(LabelStyle style)
        {
            _paletteCommonRedirect.Style = CommonHelper.ContentStyleFromLabelStyle(style);
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The mnemonic character entered.</param>
        /// <returns>true if the mnemonic was processsed; otherwise, false.</returns>
        protected override bool ProcessMnemonic(char charCode)
        {
            // Are we allowed to process mneonics?
            if (UseMnemonic && AutoCheck && CanProcessMnemonic())
            {
                // Does the button primary text contain the mnemonic?
                if (Control.IsMnemonic(charCode, Values.Text))
                {
                    // If we don't have the focus, then take it
                    if (!ContainsFocus)
                        Focus();

                    // Generating a click event will automatically transition the state
                    OnClick(EventArgs.Empty);
                    return true;
                }
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
				_drawContent.SetPalette(_overrideNormal);
			else
				_drawContent.SetPalette(_stateDisabled);

            _drawContent.Enabled = Enabled;
            _drawCheckBox.Enabled = Enabled;
            
            // Need to relayout to reflect the change in state
            MarkLayoutDirty();

			// Let base class fire standard event
			base.OnEnabledChanged(e);
		}

        /// <summary>
        /// Raises the RightToLeftChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            // Orientation and right to left are interconnected
            UpdateForOrientation();
            base.OnRightToLeftChanged(e);
        }

		/// <summary>
		/// Gets the default size of the control.
		/// </summary>
		protected override Size DefaultSize
		{
			get { return new Size(90, 25); }
		}

        /// <summary>
        /// Work out if this control needs to paint transparent areas.
        /// </summary>
        /// <returns>True if paint required; otherwise false.</returns>
        protected override bool EvalTransparentPaint()
        {
            // Always need to draw the background because always transparent
            return true;
        }
		#endregion

        #region Implementation
        private void OnCheckBoxTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
        }

        private void OnControllerClick(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void UpdateForOrientation()
        {
            // Should we display as right to left?
            ViewDockStyle dockStyle;
            switch (CheckPosition)
            {
                default:
                case VisualOrientation.Left:
                    switch (Orientation)
                    {
                        default:
                        case VisualOrientation.Top:
                            if (RightToLeft == RightToLeft.Yes)
                                dockStyle = ViewDockStyle.Right;
                            else
                                dockStyle = ViewDockStyle.Left;
                            break;
                        case VisualOrientation.Bottom:
                            if (RightToLeft == RightToLeft.Yes)
                                dockStyle = ViewDockStyle.Left;
                            else
                                dockStyle = ViewDockStyle.Right;
                            break;
                        case VisualOrientation.Left:
                            dockStyle = ViewDockStyle.Bottom;
                            break;
                        case VisualOrientation.Right:
                            dockStyle = ViewDockStyle.Top;
                            break;
                    }
                    break;
                case VisualOrientation.Right:
                    switch (Orientation)
                    {
                        default:
                        case VisualOrientation.Top:
                            if (RightToLeft == RightToLeft.Yes)
                                dockStyle = ViewDockStyle.Left;
                            else
                                dockStyle = ViewDockStyle.Right;
                            break;
                        case VisualOrientation.Bottom:
                            if (RightToLeft == RightToLeft.Yes)
                                dockStyle = ViewDockStyle.Right;
                            else
                                dockStyle = ViewDockStyle.Left;
                            break;
                        case VisualOrientation.Left:
                            dockStyle = ViewDockStyle.Top;
                            break;
                        case VisualOrientation.Right:
                            dockStyle = ViewDockStyle.Bottom;
                            break;
                    }
                    break;
                case VisualOrientation.Top:
                    switch (Orientation)
                    {
                        default:
                        case VisualOrientation.Top:
                            dockStyle = ViewDockStyle.Top;
                            break;
                        case VisualOrientation.Bottom:
                            dockStyle = ViewDockStyle.Bottom;
                            break;
                        case VisualOrientation.Left:
                            dockStyle = ViewDockStyle.Left;
                            break;
                        case VisualOrientation.Right:
                            dockStyle = ViewDockStyle.Right;
                            break;
                    }
                    break;
                case VisualOrientation.Bottom:
                    switch (Orientation)
                    {
                        default:
                        case VisualOrientation.Top:
                            dockStyle = ViewDockStyle.Bottom;
                            break;
                        case VisualOrientation.Bottom:
                            dockStyle = ViewDockStyle.Top;
                            break;
                        case VisualOrientation.Left:
                            dockStyle = ViewDockStyle.Right;
                            break;
                        case VisualOrientation.Right:
                            dockStyle = ViewDockStyle.Left;
                            break;
                    }
                    break;
            }


            // Update docking position of check box to match orientation
            _layoutDocker.SetDock(_layoutCenter, dockStyle);
        }
        #endregion
    }
}
