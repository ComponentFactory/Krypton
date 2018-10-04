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
    [ToolboxBitmap(typeof(KryptonBorderEdge), "ToolboxBitmaps.KryptonBorderEdge.bmp")]
    [DefaultEvent("Paint")]                             
    [DefaultProperty("Orientation")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonBorderEdgeDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Displays a vertical or horizontal border edge.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonBorderEdge : VisualControlBase
	{
		#region Instance Fields
        private Orientation _orientation;
        private PaletteBorderInheritRedirect _borderRedirect;
        private PaletteBorderEdgeRedirect _stateCommon;
        private PaletteBorderEdge _stateDisabled;
        private PaletteBorderEdge _stateNormal;
        private PaletteBorderEdge _stateCurrent;
        private PaletteState _state;
        private ViewDrawPanel _drawPanel;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the KryptonBorderEdge class.
		/// </summary>
        public KryptonBorderEdge()
		{
			// The label cannot take the focus
			SetStyle(ControlStyles.Selectable, false);

			// Set default label style
            _orientation = Orientation.Horizontal;

            // Create the palette storage
            _borderRedirect = new PaletteBorderInheritRedirect(Redirector, PaletteBorderStyle.ControlClient);
            _stateCommon = new PaletteBorderEdgeRedirect(_borderRedirect, NeedPaintDelegate);
            _stateDisabled = new PaletteBorderEdge(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteBorderEdge(_stateCommon, NeedPaintDelegate);
            _stateCurrent = _stateNormal;
            _state = PaletteState.Normal;

            // Our view contains just a simple canvas that covers entire client area
            _drawPanel = new ViewDrawPanel(_stateNormal);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawPanel);

            // We want to be auto sized by default, but not the property default!
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
		#endregion

		#region Public
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// Gets or sets the foreground color for the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Gets or sets the tab order of the KryptonSplitterPanel within its KryptonSplitContainer.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can give the focus to this KryptonSplitterPanel using the TAB key.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
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
        /// Gets and sets the auto size mode.
        /// </summary>
        [Category("Layout")]
        [Description("Specifies if the control grows and shrinks to fit the contents exactly.")]
        [DefaultValue(typeof(AutoSizeMode), "GrowAndShrink")]
        public AutoSizeMode AutoSizeMode
        {
            get { return base.GetAutoSizeMode(); }

            set
            {
                if (value != base.GetAutoSizeMode())
                {
                    base.SetAutoSizeMode(value);

                    // Only perform an immediate layout if
                    // currently performing auto size operations
                    if (AutoSize)
                        PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the border style.
        /// </summary>
        [Category("Visuals")]
        [Description("Border style.")]
        public PaletteBorderStyle BorderStyle
        {
            get { return _borderRedirect.Style; }

            set
            {
                if (_borderRedirect.Style != value)
                {
                    _borderRedirect.Style = value;
                    PerformNeedPaint(true);
                }
            }
        }

        private void ResetBorderStyle()
        {
            BorderStyle = PaletteBorderStyle.ControlClient;
        }

        private bool ShouldSerializeBorderStyle()
        {
            return (BorderStyle != PaletteBorderStyle.ControlClient);
        }
        
        /// <summary>
		/// Gets and sets the orientation of the border edge used to determine sizing.
		/// </summary>
		[Category("Visuals")]
		[Description("Orientation of border edge used to determine sizing.")]
		[DefaultValue(typeof(Orientation), "Horizontal")]
		public virtual Orientation Orientation
		{
			get { return _orientation; }

			set
			{
				if (_orientation != value)
				{
					_orientation = value;
					PerformNeedPaint(true);
				}
			}
		}

        /// <summary>
        /// Gets access to the common border edge appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common border edge appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBorderEdgeRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled border edge appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled border edge appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBorderEdge StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal border edge appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal border edge appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteBorderEdge StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Get the preferred size of the control based on a proposed size.
        /// </summary>
        /// <param name="proposedSize">Starting size proposed by the caller.</param>
        /// <returns>Calculated preferred size.</returns>
        public override Size GetPreferredSize(Size proposedSize)
        {
            // Let base class continue with standard calculations
            proposedSize = base.GetPreferredSize(proposedSize);

            // Do we need to apply the border width?
            if (AutoSize)
            {
                if (Orientation == Orientation.Horizontal)
                    proposedSize.Height = _stateCurrent.GetBorderWidth(_state);
                else
                    proposedSize.Width = _stateCurrent.GetBorderWidth(_state);
            }

            return proposedSize;
        }

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            // Request fixed state from the view
            _drawPanel.FixedState = state;
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(50, 50); }
        }

        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Cache the new state
            if (Enabled)
            {
                _stateCurrent = _stateNormal;
                _state = PaletteState.Normal;
            }
            else
            {
                _stateCurrent = _stateDisabled;
                _state = PaletteState.Disabled;
            }

            // Push correct palettes into the view
            _drawPanel.SetPalettes(_stateCurrent);

            // Update with latest enabled state
            _drawPanel.Enabled = Enabled;

            // Change in enabled state requires a layout and repaint
            PerformNeedPaint(true);

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }
        #endregion
    }
}
