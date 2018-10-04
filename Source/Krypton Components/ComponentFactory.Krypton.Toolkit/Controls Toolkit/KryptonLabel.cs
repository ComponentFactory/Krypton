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
    /// Display text and images with the styling features of the Krypton Toolkit
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonLabel), "ToolboxBitmaps.KryptonLabel.bmp")]
    [DefaultEvent("Paint")]
	[DefaultProperty("Text")]
    [DefaultBindingProperty("Text")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonLabelDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Displays descriptive information.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonLabel : VisualSimpleBase, IContentValues
	{
		#region Instance Fields
		private LabelStyle _style;
		private LabelValues _labelValues;
        private VisualOrientation _orientation;
        private ViewDrawContent _drawContent;
		private PaletteContentInheritRedirect _paletteCommonRedirect;
        private PaletteContent _stateCommon;
        private PaletteContent _stateDisabled;
		private PaletteContent _stateNormal;
        private KryptonCommand _command;
        private bool _useMnemonic;
        private bool _enabledTarget;
        private bool _wasEnabled;
        private Control _target;
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
		/// Initialize a new instance of the KryptonLabel class.
		/// </summary>
		public KryptonLabel()
		{
			// The label cannot take the focus
			SetStyle(ControlStyles.Selectable, false);

			// Set default properties
            _style = LabelStyle.NormalControl;
            _useMnemonic = true;
            _orientation = VisualOrientation.Top;
            _target = null;
            _enabledTarget = true;

			// Create content storage
            _labelValues = new LabelValues(NeedPaintDelegate);
            _labelValues.TextChanged += new EventHandler(OnLabelTextChanged);

			// Create palette redirector
            _paletteCommonRedirect = new PaletteContentInheritRedirect(Redirector, PaletteContentStyle.LabelNormalControl);

			// Create the palette provider
            _stateCommon = new PaletteContent(_paletteCommonRedirect, NeedPaintDelegate);
            _stateDisabled = new PaletteContent(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteContent(_stateCommon, NeedPaintDelegate);

			// Our view contains background and border with content inside
			_drawContent = new ViewDrawContent(_stateNormal, this, VisualOrientation.Top);
            _drawContent.UseMnemonic = _useMnemonic;

			// Create the view manager instance
			ViewManager = new ViewManager(this, _drawContent);

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
        [Localizable(false)]
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

        private bool ShouldSerializeLabelStyle()
        {
            return (LabelStyle != LabelStyle.NormalControl);
        }

        private void ResetLabelStyle()
        {
            LabelStyle = LabelStyle.NormalControl;
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
        /// Gets access to the target for mnemonic and click actions.
        /// </summary>
        [Category("Visuals")]
        [Description("Target control for mnemonic and click actions.")]
        [DefaultValue(null)]
        public virtual Control Target
        {
            get { return _target; }
            set { _target = value; }
        }

        /// <summary>
        /// Gets and sets the associated KryptonCommand.
        /// </summary>
        [Category("Behavior")]
        [Description("Command associated with the label.")]
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
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            // Request fixed state from the view
            _drawContent.FixedState = state;
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
        /// Gets access to the view element for the label.
        /// </summary>
        protected virtual ViewDrawContent ViewDrawContent
        {
            get { return _drawContent; }
        }

        /// <summary>
        /// Gets and sets the enabled state of the target functionality.
        /// </summary>
        protected bool EnabledTarget
        {
            get { return _enabledTarget; }
            set { _enabledTarget = value; }
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
            if (UseMnemonic && CanProcessMnemonic())
            {
                // Does the button primary text contain the mnemonic?
                if (Control.IsMnemonic(charCode, Values.Text))
                {
                    // Is target functionality enabled?
                    if (EnabledTarget)
                    {
                        // Do we have a target that can take the focus
                        if ((Target != null) && Target.CanFocus)
                        {
                            Target.Focus();
                            return true;
                        }
                    }
                }
            }

            // No match found, let base class do standard processing
            return base.ProcessMnemonic(charCode);
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // Is target functionality enabled?
            if (EnabledTarget)
            {
                // Do we have a target that can take the focus
                if ((Target != null) && Target.CanFocus)
                    Target.Focus();
            }

            base.OnClick(e);
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
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			// Push correct palettes into the view
			if (Enabled)
				_drawContent.SetPalette(_stateNormal);
			else
				_drawContent.SetPalette(_stateDisabled);

            _drawContent.Enabled = Enabled;
            
            // Need to relayout to reflect the change in state
            MarkLayoutDirty();

			// Let base class fire standard event
			base.OnEnabledChanged(e);
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
        private void OnLabelTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
        }
        #endregion
    }
}
