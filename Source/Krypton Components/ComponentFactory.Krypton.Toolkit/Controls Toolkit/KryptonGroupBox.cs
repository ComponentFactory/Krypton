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
    /// Display frame around a group of related controls with an optional caption.
	/// </summary>
	[ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonGroupBox), "ToolboxBitmaps.KryptonGroupBox.bmp")]
    [DefaultEvent("Paint")]
	[DefaultProperty("ValuesPrimary")]
    [Designer("ComponentFactory.Krypton.Toolkit.KryptonGroupBoxDesigner, ComponentFactory.Krypton.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Display frame around a group of related controls with an optional caption.")]
    [Docking(DockingBehavior.Ask)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonGroupBox : VisualControlContainment
    {
        #region Instance Fields
        private LabelStyle _captionStyle;
		private VisualOrientation _captionEdge;
        private ButtonOrientation _captionOrientation;
		private CaptionValues _captionValues;
        private ViewDrawGroupBoxDocker _drawDocker;
        private ViewDrawContent _drawContent;
        private ViewLayoutFill _layoutFill;
        private KryptonGroupPanel _panel;
        private PaletteGroupBoxRedirect _stateCommon;
        private PaletteGroupBox _stateDisabled;
        private PaletteGroupBox _stateNormal;
        private ScreenObscurer _obscurer;
        private EventHandler _removeObscurer;
        private bool _forcedLayout;
        private bool _captionVisible;
        private bool _ignoreLayout;
        private bool _layingOut;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonGroupBox class.
		/// </summary>
        public KryptonGroupBox()
		{
            // Set default values
            _captionStyle = LabelStyle.GroupBoxCaption;
			_captionEdge = VisualOrientation.Top;
            _captionOrientation = ButtonOrientation.Auto;
            _captionVisible = true;

			// Create storage objects
            _captionValues = new CaptionValues(NeedPaintDelegate);
            _captionValues.TextChanged += new EventHandler(OnValuesTextChanged);

			// Create the palette storage
            _stateCommon = new PaletteGroupBoxRedirect(Redirector, NeedPaintDelegate);
            _stateDisabled = new PaletteGroupBox(_stateCommon, NeedPaintDelegate);
            _stateNormal = new PaletteGroupBox(_stateCommon, NeedPaintDelegate);

            // Create the internal panel used for containing content
            _panel = new KryptonGroupPanel(this, _stateCommon, _stateDisabled, _stateNormal, new NeedPaintHandler(OnGroupPanelPaint));

            // Make sure the panel back style always mimics our back style
            _panel.PanelBackStyle = PaletteBackStyle.ControlGroupBox;

            _drawContent = new ViewDrawContent(_stateNormal.Content, _captionValues, VisualOrientation.Top);

			// Create view for the control border and background
            _drawDocker = new ViewDrawGroupBoxDocker(_stateNormal.Back, _stateNormal.Border);

            // Create the element that fills the remainder space and remembers fill rectange
            _layoutFill = new ViewLayoutFill(_panel);

			// Add caption into the docker with initial dock edges defined
            _drawDocker.Add(_drawContent, ViewDockStyle.Top);
            _drawDocker.Add(_layoutFill, ViewDockStyle.Fill);

			// Create the view manager instance
			ViewManager = new ViewManager(this, _drawDocker);

            // We want to default to shrinking and growing (base class defaults to GrowOnly)
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Create the delegate used when we need to ensure obscurer is removed
            _removeObscurer = new EventHandler(OnRemoveObscurer);

            // Need to prevent the AddInternal from causing a layout, otherwise the
            // layout will probably try to measure text which causes the handle for the
            // control to be created which means the handle is created at the wrong time
            // and so child controls are not added properly in the future! (for the TabControl
            // at the very least).
            _ignoreLayout = true;

            // Add panel to the controls collection
            ((KryptonReadOnlyControls)Controls).AddInternal(_panel);
            
            _ignoreLayout = false;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remove any cached obscurer
                if (_obscurer != null)
                {
                    try
                    {
                        _obscurer.Uncover();
                        _obscurer.Dispose();
                        _obscurer = null;
                    }
                    catch { }
                }
            }

            base.Dispose(disposing);
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
		/// Gets or sets the text associated with this control. 
		/// </summary>
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public override string Text
		{
			get
			{
				// Map onto the heading property from the values
				return _captionValues.Heading;
			}

			set
			{
				// Map onto the heading property from the values
				_captionValues.Heading = value;
			}
		}

		private bool ShouldSerializeText()
		{
			// Never serialize, let the values serialize instead
			return false;
		}

		/// <summary>
		/// Resets the Text property to its default value.
		/// </summary>
		public override void ResetText()
		{
			// Map onto the heading property from the values
			_captionValues.ResetHeading();
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
        /// Gets and the sets the percentage of overlap for the caption and group area.
        /// </summary>
        [Category("Visuals")]
        [Description("The percentage the caption should overlap the group area.")]
        [TypeConverter(typeof(OpacityConverter))]
        [DefaultValue((double)0.5)]
        public double CaptionOverlap
        {
            get { return _drawDocker.CaptionOverlap; }

            set
            {
                if (_drawDocker.CaptionOverlap != value)
                {

                    // Enforce limits on the value between 0 and 1 (0% and 100%)
                    value = Math.Max(Math.Min(value, 1.0), 0.0);
                    _drawDocker.CaptionOverlap = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
		/// Gets and sets the border style.
		/// </summary>
		[Category("Visuals")]
		[Description("Border style.")]
        [DefaultValue(typeof(PaletteBorderStyle), "ControlGroupBox")]
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

        private void ResetGroupBorderStyle()
        {
            GroupBorderStyle = PaletteBorderStyle.ControlGroupBox;
        }

        private bool ShouldSerializeGroupBorderStyle()
        {
            return (GroupBorderStyle != PaletteBorderStyle.ControlGroupBox);
        }
        
        /// <summary>
		/// Gets and sets the background style.
		/// </summary>
		[Category("Visuals")]
		[Description("Background style.")]
        [DefaultValue(typeof(PaletteBackStyle), "ControlGroupBox")]
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

        private void ResetGroupBackStyle()
        {
            GroupBackStyle = PaletteBackStyle.ControlGroupBox;
        }

        private bool ShouldSerializeGroupBackStyle()
        {
            return (GroupBackStyle != PaletteBackStyle.ControlGroupBox);
        }
        
        /// <summary>
		/// Gets and sets the caption style.
		/// </summary>
		[Category("Visuals")]
		[Description("Caption style.")]
        [DefaultValue(typeof(LabelStyle), "GroupBoxCaption")]
        public LabelStyle CaptionStyle
		{
			get { return _captionStyle; }

			set
			{
				if (_captionStyle != value)
				{
					_captionStyle = value;
                    _stateCommon.ContentStyle = CommonHelper.ContentStyleFromLabelStyle(_captionStyle);
                    PerformNeedPaint(true);
				}
			}
		}

        private void ResetCaptionStyle()
        {
            CaptionStyle = LabelStyle.GroupBoxCaption;
        }

        private bool ShouldSerializeCaptionStyle()
        {
            return (CaptionStyle != LabelStyle.GroupBoxCaption);
        }
        
		/// <summary>
        /// Gets and sets the position of the caption.
		/// </summary>
		[Category("Visuals")]
		[Description("Edge position of the caption.")]
		[DefaultValue(typeof(VisualOrientation), "Top")]
		public VisualOrientation CaptionEdge
		{
			get { return _captionEdge; }

			set
			{
				if (_captionEdge != value)
				{
					_captionEdge = value;
                    switch (_captionEdge)
                    {
                        case VisualOrientation.Top:
                            if (_captionOrientation == ButtonOrientation.Auto)
                                _drawContent.Orientation = VisualOrientation.Top;

                                _drawDocker.SetDock(_drawContent, ViewDockStyle.Top);
                            break;
                        case VisualOrientation.Bottom:
                            if (_captionOrientation == ButtonOrientation.Auto)
                                _drawContent.Orientation = VisualOrientation.Top;

                            _drawDocker.SetDock(_drawContent, ViewDockStyle.Bottom);
                            break;
                        case VisualOrientation.Left:
                            if (_captionOrientation == ButtonOrientation.Auto)
                                _drawContent.Orientation = VisualOrientation.Left;

                            _drawDocker.SetDock(_drawContent, ViewDockStyle.Left);
                            break;
                        case VisualOrientation.Right:
                            if (_captionOrientation == ButtonOrientation.Auto)
                                _drawContent.Orientation = VisualOrientation.Right;

                            _drawDocker.SetDock(_drawContent, ViewDockStyle.Right);
                            break;
                    }

                    PerformNeedPaint(true);
				}
			}
		}

        /// <summary>
        /// Gets and sets the orientation of the caption.
        /// </summary>
        [Category("Visuals")]
        [Description("Orientation of the caption.")]
        [DefaultValue(typeof(ButtonOrientation), "Auto")]
        public ButtonOrientation CaptionOrientation
        {
            get { return _captionOrientation; }

            set
            {
                if (_captionOrientation != value)
                {
                    _captionOrientation = value;
                    switch (_captionOrientation)
                    {
                        case ButtonOrientation.FixedTop:
                            _drawContent.Orientation = VisualOrientation.Top;
                            break;
                        case ButtonOrientation.FixedBottom:
                            _drawContent.Orientation = VisualOrientation.Bottom;
                            break;
                        case ButtonOrientation.FixedLeft:
                            _drawContent.Orientation = VisualOrientation.Left;
                            break;
                        case ButtonOrientation.FixedRight:
                            _drawContent.Orientation = VisualOrientation.Right;
                            break;
                        case ButtonOrientation.Auto:
                            switch (_captionEdge)
                            {
                                case VisualOrientation.Top:
                                case VisualOrientation.Bottom:
                                    _drawContent.Orientation = VisualOrientation.Top;
                                    break;
                                case VisualOrientation.Left:
                                    _drawContent.Orientation = VisualOrientation.Left;
                                    break;
                                case VisualOrientation.Right:
                                    _drawContent.Orientation = VisualOrientation.Right;
                                    break;
                            }
                            break;
                    }

                    PerformNeedPaint(true);
                }
            }
        }
        
        /// <summary>
		/// Gets and sets the caption visibility.
		/// </summary>
		[Category("Visuals")]
		[Description("Caption visibility.")]
		[DefaultValue(true)]
		public bool CaptionVisible
		{
            get { return _captionVisible; }

			set
			{
                if (_captionVisible != value)
				{
                    _captionVisible = value;
                    ReapplyVisible();
					PerformNeedPaint(true);
				}
			}
		}

        /// <summary>
        /// Gets access to the common header group appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common header group appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGroupBoxRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        
        /// <summary>
		/// Gets access to the disabled header group appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining disabled header group appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGroupBox StateDisabled
		{
			get { return _stateDisabled; }
		}

		private bool ShouldSerializeStateDisabled()
		{
			return !_stateDisabled.IsDefault;
		}

		/// <summary>
		/// Gets access to the normal header group appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal header group appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteGroupBox StateNormal
		{
			get { return _stateNormal; }
		}

		private bool ShouldSerializeStateNormal()
		{
			return !_stateNormal.IsDefault;
		}

		/// <summary>
		/// Gets access to the caption content.
		/// </summary>
		[Category("Visuals")]
		[Description("Caption values")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CaptionValues Values
		{
			get { return _captionValues; }
		}

        private bool ShouldSerializeValuesPrimary()
		{
			return !_captionValues.IsDefault;
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
        /// Creates a new instance of the control collection for the KryptonHeaderGroup.
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
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs containing the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Remember if we are inside a layout cycle
            _layingOut = true;

            // Must ignore any layout caused by the AddInternal in the constructor,
            // otherwise layout processing causes the controls handle to be constructed
            // (because it tries to measure text for the headers) and so child controls
            // are not added correctly when inside a TabControl. Bonkers but true.
            if (!_ignoreLayout)
            {
                // Let base class calulcate fill rectangle
                base.OnLayout(levent);

                // Only use layout logic if control is fully initialized or if being forced
                // to allow a relayout or if in design mode.
                if (IsInitialized || _forcedLayout || (DesignMode && (_panel != null)))
                {
                    Rectangle fillRect = _layoutFill.FillRect;

                    _panel.SetBounds(fillRect.X,
                                     fillRect.Y,
                                     fillRect.Width,
                                     fillRect.Height);
                }
            }

            _layingOut = false;
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
                _drawContent.SetPalette(_stateNormal.Content);
                _drawDocker.SetPalettes(_stateNormal.Back, _stateNormal.Border);
			}
			else
			{
                _drawContent.SetPalette(_stateDisabled.Content);
                _drawDocker.SetPalettes(_stateDisabled.Back, _stateNormal.Border);
			}

            _drawContent.Enabled = Enabled;
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

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == PI.WM_WINDOWPOSCHANGING)
            {
                // First time around we need to create the obscurer
                if (_obscurer == null)
                    _obscurer = new ScreenObscurer();

                // Obscure the display area of the control
                if (!IsDisposed && IsHandleCreated && !DesignMode)
                    _obscurer.Cover(this);

                // Just in case the WM_WINDOWPOSCHANGED does not occur we can 
                // ensure the obscurer is removed using this async delegate call
                BeginInvoke(_removeObscurer);
            }

            if (m.Msg == PI.WM_WINDOWPOSCHANGED)
            {
                // Uncover from the covered area
                if (_obscurer != null)
                    _obscurer.Uncover();
            }

            base.WndProc(ref m);
        }
        #endregion

        #region Internal
        internal Component DesignerComponentFromPoint(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return null;

            // Ask the current view for a decision
            return ViewManager.ComponentFromPoint(pt);
        }

        internal void DesignerMouseLeave()
        {
            // Simulate the mouse leaving the control so that the tracking
            // element that thinks it has the focus is informed it does not
            OnMouseLeave(EventArgs.Empty);
        }
        #endregion

		#region Implementation
        private void OnRemoveObscurer(object sender, EventArgs e)
        {
            if (_obscurer != null)
                _obscurer.Uncover();
        }

        private void OnValuesTextChanged(object sender, EventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
        }

        private void OnGroupPanelPaint(object sender, NeedLayoutEventArgs e)
        {
            // If the child panel is layout out but not because we are, then it must be
            // laying out because a child has changed visibility/size/etc. If we are an
            // AutoSize control then we need to ensure we layout as well to change size.
            if (e.NeedLayout && !_layingOut && AutoSize)
                PerformNeedPaint(true);
        }

        private void ReapplyVisible()
        {
            _drawContent.Visible = _captionVisible;
        }
        #endregion

        #region Implementation Static
        private static int PaddingEdgeNeeded(int padding, int client)
        {
            // If the padding value is less than that allocated to children
            if (padding < client)
            {
                // Then no additional padding is needed, because the children
                // overlap all of the padding edge anyway
                return 0;
            }
            else
            {
                // Then we need only the extra space between the client and the
                // padding edge, as the rest is overlaped by the children
                return padding - client;
            }
        }
        #endregion
    }
}
