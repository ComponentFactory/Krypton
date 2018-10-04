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
    /// Combines button functionality with the styling features of the Krypton Toolkit.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonButton), "ToolboxBitmaps.KryptonButton.bmp")]
    [DefaultEvent("Click")]
	[DefaultProperty("Text")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonButtonDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Raises an event when the user clicks it.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonButton : VisualSimpleBase, IButtonControl, IContentValues
	{
		#region Instance Fields
        private ViewDrawButton _drawButton;
        private ButtonStyle _style;
		private ButtonValues _buttonValues;
		private ButtonController _buttonController;
        private VisualOrientation _orientation;
        private PaletteTripleRedirect _stateCommon;
        private PaletteTriple _stateDisabled;
        private PaletteTriple _stateNormal;
        private PaletteTriple _stateTracking;
        private PaletteTriple _statePressed;
		private PaletteTripleRedirect _stateDefault;
		private PaletteTripleRedirect _stateFocus;
        private PaletteTripleOverride _overrideFocus;
		private PaletteTripleOverride _overrideNormal;
		private PaletteTripleOverride _overrideTracking;
		private PaletteTripleOverride _overridePressed;
        private IKryptonCommand _command;
		private DialogResult _dialogResult;
        private bool _isDefault;
		private bool _useMnemonic;
        private bool _wasEnabled;
		#endregion

        #region Events
        /// <summary>
        /// Occurs when the value of the KryptonCommand property changes.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the KryptonCommand property changes.")]
        public event EventHandler KryptonCommandChanged;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonButton class.
		/// </summary>
        public KryptonButton()
        {
            // We generate click events manually, suppress default
            // production of them by the base Control class
            SetStyle(ControlStyles.StandardClick |
                     ControlStyles.StandardDoubleClick, false);

            // Set default button properties
            _style = ButtonStyle.Standalone;
            _dialogResult = DialogResult.None;
            _orientation = VisualOrientation.Top;
            _useMnemonic = true;

            // Create content storage
            _buttonValues = CreateButtonValues(NeedPaintDelegate);
            _buttonValues.TextChanged += new EventHandler(OnButtonTextChanged);

            // Create the palette storage
            _stateCommon = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
            _stateDisabled = new PaletteTriple(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteTriple(_stateCommon, NeedPaintDelegate);
            _stateTracking = new PaletteTriple(_stateCommon, NeedPaintDelegate);
            _statePressed = new PaletteTriple(_stateCommon, NeedPaintDelegate);
            _stateDefault = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
            _stateFocus = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);

            // Create the override handling classes
            _overrideFocus = new PaletteTripleOverride(_stateFocus, _stateNormal,  PaletteState.FocusOverride);
            _overrideNormal = new PaletteTripleOverride(_stateDefault, _overrideFocus, PaletteState.NormalDefaultOverride);
            _overrideTracking = new PaletteTripleOverride(_stateFocus, _stateTracking, PaletteState.FocusOverride);
            _overridePressed = new PaletteTripleOverride(_stateFocus, _statePressed, PaletteState.FocusOverride);

            // Create the view button instance
            _drawButton = new ViewDrawButton(_stateDisabled,
                                             _overrideNormal,
                                             _overrideTracking,
                                             _overridePressed,
                                             new PaletteMetricRedirect(Redirector),
                                             this,
                                             Orientation,
                                             UseMnemonic);

            // Only draw a focus rectangle when focus cues are needed in the top level form
            _drawButton.TestForFocusCues = true;

            // Create a button controller to handle button style behaviour
            _buttonController = new ButtonController(_drawButton, NeedPaintDelegate);

            // Assign the controller to the view element to treat as a button
            _drawButton.MouseController = _buttonController;
            _drawButton.KeyController = _buttonController;
            _drawButton.SourceController = _buttonController;

            // Need to know when user clicks the button view or mouse selects it
            _buttonController.Click += new MouseEventHandler(OnButtonClick);
            _buttonController.MouseSelect += new MouseEventHandler(OnButtonSelect);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawButton);
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
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
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
				// Map onto the button property from the values
				return _buttonValues.Text;
			}

			set
			{
				// Map onto the button property from the values
				_buttonValues.Text = value;
			}
		}

		private bool ShouldSerializeText()
		{
			// Never serialize, let the button values serialize instead
			return false;
		}

		/// <summary>
		/// Resets the Text property to its default value.
		/// </summary>
		public override void ResetText()
		{
			// Map onto the button property from the values
			_buttonValues.ResetText();
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
                    _drawButton.Orientation = value;
                    
                    PerformNeedPaint(true);
				}
			}
		}

		/// <summary>
		/// Gets and sets the button style.
		/// </summary>
		[Category("Visuals")]
		[Description("Button style.")]
		public ButtonStyle ButtonStyle
		{
			get { return _style; }

			set
			{
				if (_style != value)
				{
					_style = value;
                    SetStyles(_style);
					PerformNeedPaint(true);
				}
			}
		}

        private bool ShouldSerializeButtonStyle()
        {
            return (ButtonStyle != ButtonStyle.Standalone);
        }

        private void ResetButtonStyle()
        {
            ButtonStyle = ButtonStyle.Standalone;
        }

		/// <summary>
		/// Gets access to the button content.
		/// </summary>
		[Category("Visuals")]
		[Description("Button values")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ButtonValues Values
		{
			get { return _buttonValues; }
		}

		private bool ShouldSerializeValues()
		{
			return !_buttonValues.IsDefault;
		}

        /// <summary>
        /// Gets access to the common button appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common button appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        
        /// <summary>
		/// Gets access to the disabled button appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining disabled button appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateDisabled
		{
			get { return _stateDisabled; }
		}

		private bool ShouldSerializeStateDisabled()
		{
			return !_stateDisabled.IsDefault;
		}

		/// <summary>
		/// Gets access to the normal button appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal button appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateNormal
		{
			get { return _stateNormal; }
		}

		private bool ShouldSerializeStateNormal()
		{
			return !_stateNormal.IsDefault;
		}

		/// <summary>
		/// Gets access to the hot tracking button appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining hot tracking button appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateTracking
		{
			get { return _stateTracking; }
		}

		private bool ShouldSerializeStateTracking()
		{
			return !_stateTracking.IsDefault;
		}

		/// <summary>
		/// Gets access to the pressed button appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining pressed button appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StatePressed
		{
			get { return _statePressed; }
		}

		private bool ShouldSerializeStatePressed()
		{
			return !_statePressed.IsDefault;
		}

		/// <summary>
		/// Gets access to the normal button appearance when default.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal button appearance when default.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteTripleRedirect OverrideDefault
		{
			get { return _stateDefault; }
		}

		private bool ShouldSerializeOverrideDefault()
		{
			return !_stateDefault.IsDefault;
		}

		/// <summary>
		/// Gets access to the button appearance when it has focus.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining button appearance when it has focus.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteTripleRedirect OverrideFocus
		{
			get { return _stateFocus; }
		}

		private bool ShouldSerializeOverrideFocus()
		{
			return !_stateFocus.IsDefault;
		}

		/// <summary>
		/// Gets or sets the value returned to the parent form when the button is clicked.
		/// </summary>
		[Category("Behavior")]
		[Description("The dialog-box result produced in a modal form by clicking the button.")]
		[DefaultValue(typeof(DialogResult), "None")]
		public DialogResult DialogResult
		{
			get { return _dialogResult; }
			set { _dialogResult = value; }
		}

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the button.")]
        [DefaultValue(null)]
        public virtual IKryptonCommand KryptonCommand
        {
            get { return _command; }

            set
            {
                if (_command != value)
                {
                    if (_command != null)
                        _command.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);
                    else
                        _wasEnabled = Enabled;

                    _command = value;
                    OnKryptonCommandChanged(EventArgs.Empty);

                    if (_command != null)
                        _command.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);
                    else
                        Enabled = _wasEnabled;
                }
            }
        }

		/// <summary>
		/// Notifies a control that it is the default button so that its appearance and behavior is adjusted accordingly. 
		/// </summary>
		/// <param name="value">true if the control should behave as a default button; otherwise false.</param>
		public void NotifyDefault(bool value)
		{
			if (!ViewDrawButton.IsFixed && (_isDefault != value))
			{
				// Remember new default status
				_isDefault = value;

				// Decide if the default overrides should be applied
                _overrideNormal.Apply = value;

				// Change in deault state requires a layout and repaint
				PerformNeedPaint(true);
			}
		}

		/// <summary>
		/// Generates a Click event for the control.
		/// </summary>
		public void PerformClick()
		{
			if (CanSelect)
				OnClick(EventArgs.Empty);
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
					_drawButton.UseMnemonic = value;
					PerformNeedPaint(true);
				}
			}
		}

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            if (state == PaletteState.NormalDefaultOverride)
            {
                // Setup the overrides correctly to match state
                _overrideFocus.Apply = true;
                _overrideNormal.Apply = true;

                // Must pass a proper drawing state to the view
                state = PaletteState.Normal;
            }

            // Request fixed state from the view
            _drawButton.FixedState = state;
        }

		/// <summary>
		/// Determins the IME status of the object when selected.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get { return base.ImeMode; }
			set { base.ImeMode = value; }
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
                return _buttonValues.GetShortText();
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
                return _buttonValues.GetLongText();
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
                return _buttonValues.GetImage(state);
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
                return _buttonValues.GetImageTransparentColor(state);
        }
        #endregion
        
        #region Protected Overrides
		/// <summary>
		/// Gets the default size of the control.
		/// </summary>
		protected override Size DefaultSize
		{
			get { return new Size(90, 25); }
		}

		/// <summary>
		/// Gets the default Input Method Editor (IME) mode supported by this control.
		/// </summary>
		protected override ImeMode DefaultImeMode
		{
			get { return ImeMode.Disable; }
		}

		/// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			// Change in enabled state requires a layout and repaint
			PerformNeedPaint(true);

			// Let base class fire standard event
			base.OnEnabledChanged(e);
		}

		/// <summary>
		/// Raises the GotFocus event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnGotFocus(EventArgs e)
		{
            if (!ViewDrawButton.IsFixed)
            {
                // Apply the focus overrides
                _overrideFocus.Apply = true;
                _overrideTracking.Apply = true;
                _overridePressed.Apply = true;

                // Change in focus requires a repaint
                PerformNeedPaint(false);
            }

			// Let base class fire standard event
			base.OnGotFocus(e);
		}

		/// <summary>
		/// Raises the LostFocus event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnLostFocus(EventArgs e)
		{
            if (!ViewDrawButton.IsFixed)
            {
                // Apply the focus overrides
                _overrideFocus.Apply = false;
                _overrideTracking.Apply = false;
                _overridePressed.Apply = false;

                // Change in focus requires a repaint
                PerformNeedPaint(false);
            }

			// Let base class fire standard event
			base.OnLostFocus(e);
		}

		/// <summary>
		/// Raises the Click event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnClick(EventArgs e)
		{
			// Find the form this button is on
			Form owner = FindForm();

			// If we find a valid owner
			if (owner != null)
			{
				// Update owner with our dialog result setting
				owner.DialogResult = DialogResult;
			}

			// Let base class fire standard event
			base.OnClick(e);

            // If we have an attached command then execute it
            if (KryptonCommand != null)
                KryptonCommand.PerformExecute();
		}

		/// <summary>
		/// Processes a mnemonic character.
		/// </summary>
		/// <param name="charCode">The mnemonic character entered.</param>
		/// <returns>true if the mnemonic was processsed; otherwise, false.</returns>
        protected override bool ProcessMnemonic(char charCode)
		{
			// Are we allowed to process mnemonics?
			if (UseMnemonic && CanProcessMnemonic())
			{
				// Does the button primary text contain the mnemonic?
				if (Control.IsMnemonic(charCode, Values.Text))
				{
					// Perform default action for a button, click it!
					PerformClick();
					return true;
				}
			}

			// No match found, let base class do standard processing
			return base.ProcessMnemonic(charCode);
		}

        /// <summary>
        /// Called when a context menu has just been closed.
        /// </summary>
        protected override void ContextMenuClosed()
        {
            _buttonController.RemoveFixed();
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Update the state objects to reflect the new button style.
        /// </summary>
        /// <param name="buttonStyle">New button style.</param>
        protected virtual void SetStyles(ButtonStyle buttonStyle)
        {
            _stateCommon.SetStyles(buttonStyle);
            _stateDefault.SetStyles(buttonStyle);
            _stateFocus.SetStyles(buttonStyle);
        }

        /// <summary>
        /// Creates a values storage object appropriate for control.
        /// </summary>
        /// <returns>Set of button values.</returns>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        protected virtual ButtonValues CreateButtonValues(NeedPaintHandler needPaint)
        {
            return new ButtonValues(needPaint);
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
                Enabled = KryptonCommand.Enabled;

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
                case "Text":
                case "ExtraText":
                case "ImageSmall":
                case "ImageTransparentColor":
                    PerformNeedPaint(true);
                    break;
            }
        }

        /// <summary>
        /// Gets access to the view element for the button.
        /// </summary>
        protected virtual ViewDrawButton ViewDrawButton
        {
            get { return _drawButton; }
        }
        #endregion

        #region Implementation
        private void OnButtonTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
        }
        
        private void OnButtonClick(object sender, MouseEventArgs e)
		{
			// Raise the standard click event
			OnClick(EventArgs.Empty);

			// Raise event to indicate it was a mouse activated click
			OnMouseClick(e);
		}

		private void OnButtonSelect(object sender, MouseEventArgs e)
		{
			// Take the focus if allowed
			if (CanFocus)
				Focus();
		}
        #endregion
    }
}
