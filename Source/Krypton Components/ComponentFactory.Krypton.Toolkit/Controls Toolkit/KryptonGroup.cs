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
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Group related controls together with Krypton Toolkit styling.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonGroup), "ToolboxBitmaps.KryptonGroup.bmp")]
    [DefaultEvent("Paint")]
	[DefaultProperty("GroupBackStyle")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonGroupDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Enables you to group collections of controls.")]
    [Docking(DockingBehavior.Ask)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonGroup : VisualControlContainment
	{
		#region Instance Fields
		private ViewDrawDocker _drawDocker;
        private PaletteDoubleRedirect _stateCommon;
        private PaletteDouble _stateDisabled;
		private PaletteDouble _stateNormal;
        private ViewLayoutFill _layoutFill;
        private KryptonGroupPanel _panel;
        private bool _forcedLayout;
        private bool _layingOut;
        #endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the KryptonGroup class.
		/// </summary>
		public KryptonGroup()
		{            
            // Create the palette storage
            _stateCommon = new PaletteDoubleRedirect(Redirector, PaletteBackStyle.ControlClient, PaletteBorderStyle.ControlClient, NeedPaintDelegate);
            _stateDisabled = new PaletteDouble(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteDouble(_stateCommon, NeedPaintDelegate);

            // Create the internal panel used for containing content
            _panel = new KryptonGroupPanel(this, _stateCommon, _stateDisabled, _stateNormal, new NeedPaintHandler(OnGroupPanelPaint));

            // Make sure the panel back style always mimics our back style
            _panel.PanelBackStyle = PaletteBackStyle.ControlClient;

            // Create the element that fills the remainder space and remembers fill rectange
            _layoutFill = new ViewLayoutFill(_panel);

            // Create view for the control border and background
            _drawDocker = new ViewDrawDocker(_stateNormal.Back, _stateNormal.Border);
            _drawDocker.Add(_layoutFill, ViewDockStyle.Fill);

			// Create the view manager instance
            ViewManager = new ViewManager(this, _drawDocker);

            // We want to default to shrinking and growing (base class defaults to GrowOnly)
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Add panel to the controls collection
            ((KryptonReadOnlyControls)Controls).AddInternal(_panel);
        }
		#endregion

		#region Public
        /// <summary>
        /// Gets and sets the name of the control.
        /// </summary>
        [Browsable(false)]
        public new string Name
        {
            get { return base.Name; }

            set
            {
                base.Name = value;
                _panel.Name = value + ".Panel";
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
        /// Gets access to the internal panel that contains group content.
        /// </summary>
        [Localizable(false)]
        [Category("Appearance")]
        [Description("The internal panel that contains group content.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonGroupPanel Panel
        {
            get { return _panel; }
        }

        /// <summary>
		/// Gets and sets the border style.
		/// </summary>
		[Category("Visuals")]
		[Description("Border style.")]
		public PaletteBorderStyle GroupBorderStyle
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

        private bool ShouldSerializeGroupBorderStyle()
        {
            return (GroupBorderStyle != PaletteBorderStyle.ControlClient);
        }

        private void ResetGroupBorderStyle()
        {
            GroupBorderStyle = PaletteBorderStyle.ControlClient;
        }
        
        /// <summary>
		/// Gets and sets the background style.
		/// </summary>
		[Category("Visuals")]
		[Description("Background style.")]
		public PaletteBackStyle GroupBackStyle
		{
			get { return _stateCommon.BackStyle; }

			set
			{
                if (_stateCommon.BackStyle != value)
				{
                    _stateCommon.BackStyle = value;
                    _panel.PanelBackStyle = value;
                    PerformNeedPaint(true);
				}
			}
		}

        private bool ShouldSerializeGroupBackStyle()
        {
            return (GroupBackStyle != PaletteBackStyle.ControlClient);
        }

        private void ResetGroupBackStyle()
        {
            GroupBackStyle = PaletteBackStyle.ControlClient;
        }

        /// <summary>
        /// Gets access to the common group appearance entries that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common group appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        
        /// <summary>
		/// Gets access to the disabled group appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining disabled group appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble StateDisabled
		{
			get { return _stateDisabled; }
		}

		private bool ShouldSerializeStateDisabled()
		{
			return !_stateDisabled.IsDefault;
		}

		/// <summary>
		/// Gets access to the normal group appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal group appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble StateNormal
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
            // Do we have a manager to ask for a preferred size?
            if (ViewManager != null)
            {
                // Ask the view to peform a layout
                Size retSize = ViewManager.GetPreferredSize(Renderer, proposedSize);

                // Apply the maximum sizing
                if (MaximumSize.Width > 0)  retSize.Width = Math.Min(MaximumSize.Width, retSize.Width);
                if (MaximumSize.Height > 0) retSize.Height = Math.Min(MaximumSize.Height, retSize.Width);

                // Apply the minimum sizing
                if (MinimumSize.Width > 0)  retSize.Width = Math.Max(MinimumSize.Width, retSize.Width);
                if (MinimumSize.Height > 0) retSize.Height = Math.Max(MinimumSize.Height, retSize.Height);

                return retSize;
            }
            else
            {
                // Fall back on default control processing
                return base.GetPreferredSize(proposedSize);
            }
        }
        
        /// <summary>
		/// Gets the rectangle that represents the display area of the control.
		/// </summary>
		public override Rectangle DisplayRectangle
		{
			get
			{
                // Ensure that the layout is calculated in order to know the remaining display space
                ForceViewLayout();

                // The inside panel is the client rectangle size
                return new Rectangle(Panel.Location, Panel.Size);
			}
		}

        /// <summary>
        /// Fix the control to a particular palette state.
        /// </summary>
        /// <param name="state">Palette state to fix.</param>
        public virtual void SetFixedState(PaletteState state)
        {
            // Request fixed state from the view
            _drawDocker.FixedState = state;
            _panel.SetFixedState(state);
        }
		#endregion

        #region Protected
        /// <summary>
        /// Force the layout logic to size and position the panels.
        /// </summary>
        protected void ForceControlLayout()
        {
            // Usually the layout will not occur if currently initializing but
            // we need to force the layout processing because overwise the size
            // of the panel controls will not have been calculated when controls
            // are added to the panels. That would then cause problems with
            // anchor controls as they would then resize incorrectly.
            if (!IsInitialized)
            {
                _forcedLayout = true;
                OnLayout(new LayoutEventArgs(null, null));
                _forcedLayout = false;
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Creates a new instance of the control collection for the KryptonGroup.
        /// </summary>
        /// <returns>A new instance of Control.ControlCollection assigned to the control.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new KryptonReadOnlyControls(this);
        }

        /// <summary>
        /// Raises the HandleCreated event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            // Let base class do standard stuff
            base.OnHandleCreated(e);

            // We need a layout to occur before any painting
            InvokeLayout();
        }

        /// <summary>
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            // Let base class raise events
            base.OnInitialized(e);

            // Force a layout now that initialization is complete
            OnLayout(new LayoutEventArgs(null, null));
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

			// Change in enabled state requires a layout and repaint
			PerformNeedPaint(true);

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the Resize event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            // Let base class raise events
            base.OnResize(e);

            // We must have a layout calculation
            ForceControlLayout();
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">An EventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Remember if we are inside a layout cycle
            _layingOut = true;

            // Let base class calulcate fill rectangle
            base.OnLayout(levent);

            // Only use layout logic if control is fully initialized or if being forced
            // to allow a relayout or if in design mode.
            if (IsInitialized || _forcedLayout || (DesignMode && (_panel != null)))
            {
                Rectangle fillRect = _layoutFill.FillRect;
                _panel.SetBounds(fillRect.X, fillRect.Y, fillRect.Width, fillRect.Height);
            }

            _layingOut = false;
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            if (IsInitialized || !e.NeedLayout)
            {
                // As the contained group panel is using our palette storage
                // we also need to pass on any paint request to it as well
                _panel.PerformNeedPaint(e.NeedLayout);
            }
            else
                ForceControlLayout();

            base.OnNeedPaint(sender, e);
        }
        #endregion

        #region Implementation
        private void OnGroupPanelPaint(object sender, NeedLayoutEventArgs e)
        {
            // If the child panel is layout out but not because we are, then it must be
            // laying out because a child has changed visibility/size/etc. If we are an
            // AutoSize control then we need to ensure we layout as well to change size.
            if (e.NeedLayout && !_layingOut && AutoSize)
                PerformNeedPaint(true);
        }
        #endregion
    }
}
