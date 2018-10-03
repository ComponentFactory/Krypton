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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Base class used for implementation of panel controls.
	/// </summary>
	[ToolboxItem(false)]
	[DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public abstract class VisualPanel : Panel,
										ISupportInitializeNotification,
                                        IKryptonDebug
	{
        #region Static Field
        private static MethodInfo _miPTB;
        #endregion

        #region Instance Fields
        private bool _initializing;
        private bool _initialized;
        private bool _refresh;
        private bool _refreshAll;
        private bool _layoutDirty;
        private bool _paintTransparent;
        private bool _evalTransparent;
        private bool _globalEvents;
        private Size _lastLayoutSize;
        private IPalette _localPalette;
        private IPalette _palette;
        private IRenderer _renderer;
        private PaletteRedirect _redirector;
		private PaletteMode _paletteMode;
		private ViewManager _viewManager;
        private SimpleCall _refreshCall;
        private NeedPaintHandler _needPaintDelegate;
        #endregion

		#region Events
		/// <summary>
		/// Occurs when the control is initialized.
		/// </summary>
        [Category("Behavior")]
        [Description("Occurs when the control has been fully initialized.")]
        public event EventHandler Initialized;

		/// <summary>
		/// Occurs when the palette changes.
		/// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the Palette property is changed.")]
        public event EventHandler PaletteChanged;
        #endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the VisualPanel class.
		/// </summary>
		protected VisualPanel()
		{
			#region Default ControlStyle Values
			// Default style values for Control are:-
			//	True  - AllPaintingInWmPaint
			//	False - CacheText
			//	False - ContainerControl
			//	False - EnableNotifyMessage
			//	False - FixedHeight
			//	False - FixedWidth
			//	False - Opaque
			//	False - OptimizedDoubleBuffer
			//	False - ResizeRedraw
			//	True  - Selectable
			//	True  - StandardClick
			//	True  - StandardDoubleClick
			//	False - SupportsTransparentBackColor
			//	False - UserMouse
			//	True  - UserPaint
			//	True  - UseTextForAccessibility
			#endregion

			// We use double buffering to reduce drawing flicker
			SetStyle(ControlStyles.OptimizedDoubleBuffer |
					 ControlStyles.AllPaintingInWmPaint |
					 ControlStyles.UserPaint, true);

            // We need to allow a transparent background
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            // We need to repaint entire control whenever resized
			SetStyle(ControlStyles.ResizeRedraw, true);

			// We act as a container for child controls
			SetStyle(ControlStyles.ContainerControl, true);

			// Cannot select a panel
			SetStyle(ControlStyles.Selectable, false);

            // Yes, we want to be drawn double buffered by default
            DoubleBuffered = true;

            // Setup the invoke used to refresh display
            _refreshCall = new SimpleCall(OnPerformRefresh);

            // Setup the need paint delegate
            _needPaintDelegate = new NeedPaintHandler(OnNeedPaint);

            // Must layout before first draw attempt
            _layoutDirty = true;
            _evalTransparent = true;
            _lastLayoutSize = Size.Empty;

            // Set the palette to the defaults as specified by the manager
            _localPalette = null;
            SetPalette(KryptonManager.CurrentGlobalPalette);
            _paletteMode = PaletteMode.Global;

            // Create constant target for resolving palette delegates
            _redirector = new PaletteRedirect(_palette);

            AttachGlobalEvents();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unhook from any current menu strip
                if (base.ContextMenuStrip != null)
                {
                    base.ContextMenuStrip.Opening -= new CancelEventHandler(OnContextMenuStripOpening);
                    base.ContextMenuStrip.Closed -= new ToolStripDropDownClosedEventHandler(OnContextMenuClosed);
                    base.ContextMenuStrip = null;
                }
                
                // Must unhook from the palette paint event
                if (_palette != null)
                {
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnNeedPaint);
                    _palette.ButtonSpecChanged -= new EventHandler(OnButtonSpecChanged);
                }

                UnattachGlobalEvents();
                ViewManager.Dispose();

                _palette = null;
                _renderer = null;
                _localPalette = null;
                Redirector.Target = null;
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

            // No need to layout the view during initialization
            SuspendLayout();
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

            // We always need a paint and layout
            OnNeedPaint(this, new NeedLayoutEventArgs(true));

            // Should layout once initialization is complete
            ResumeLayout(true);

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
        /// Gets or sets the ContextMenuStrip associated with this control.
        /// </summary>
        public override ContextMenuStrip ContextMenuStrip
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return base.ContextMenuStrip; }

            set
            {
                // Unhook from any current menu strip
                if (base.ContextMenuStrip != null)
                {
                    base.ContextMenuStrip.Opening -= new CancelEventHandler(OnContextMenuStripOpening);
                    base.ContextMenuStrip.Closed -= new ToolStripDropDownClosedEventHandler(OnContextMenuClosed);
                }

                // Let parent handle actual storage
                base.ContextMenuStrip = value;

                // Hook into the strip being shown (so we can set the correct renderer)
                if (base.ContextMenuStrip != null)
                {
                    base.ContextMenuStrip.Opening += new CancelEventHandler(OnContextMenuStripOpening);
                    base.ContextMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(OnContextMenuClosed);
                }
            }
        }

        /// <summary>
        /// Fires the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        public void PerformNeedPaint(bool needLayout)
        {
            OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));
        }

        /// <summary>
        /// Gets or sets the palette to be applied.
        /// </summary>
        [Category("Visuals")]
        [Description("Palette applied to drawing.")]
        public PaletteMode PaletteMode
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _paletteMode; }

            set
            {
                if (_paletteMode != value)
                {
                    // Action despends on new value
                    switch (value)
                    {
                        case PaletteMode.Custom:
                            // Do nothing, you must assign a palette to the 
                            // 'Palette' property in order to get the custom mode
                            break;
                        default:
                            // Use the new value
                            _paletteMode = value;

                            // Get a reference to the standard palette from its name
                            _localPalette = null;
                            SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));

                            // Must raise event to change palette in redirector
                            OnPaletteChanged(EventArgs.Empty);

                            // Need to layout again use new palette
                            PerformLayout();
                            break;
                    }
                }
            }
        }

        private bool ShouldSerializePaletteMode()
        {
            return (PaletteMode != PaletteMode.Global);
        }

        /// <summary>
        /// Resets the PaletteMode property to its default value.
        /// </summary>
        public void ResetPaletteMode()
        {
            PaletteMode = PaletteMode.Global;
        }

        /// <summary>
        /// Gets and sets the custom palette implementation.
        /// </summary>
        [Category("Visuals")]
        [Description("Custom palette applied to drawing.")]
        [DefaultValue(null)]
        public IPalette Palette
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _localPalette; }

            set
            {
                // Only interested in changes of value
                if (_localPalette != value)
                {
                    // Remember the starting palette
                    IPalette old = _localPalette;

                    // Use the provided palette value
                    SetPalette(value);

                    // If no custom palette is required
                    if (value == null)
                    {
                        // No custom palette, so revert back to the global setting
                        _paletteMode = PaletteMode.Global;

                        // Get the appropriate palette for the global mode
                        _localPalette = null;
                        SetPalette(KryptonManager.GetPaletteForMode(_paletteMode));
                    }
                    else
                    {
                        // No longer using a standard palette
                        _localPalette = value;
                        _paletteMode = PaletteMode.Custom;
                    }

                    // If real change has occured
                    if (old != _localPalette)
                    {
                        // Raise the change event
                        OnPaletteChanged(EventArgs.Empty);

                        // Need to layout again use new palette
                        PerformLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Resets the Palette property to its default value.
        /// </summary>
        public void ResetPalette()
        {
            PaletteMode = PaletteMode.Global;
        }

        /// <summary>
        /// Gets access to the current renderer.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IRenderer Renderer
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _renderer; }
        }

        /// <summary>
        /// Create a tool strip renderer appropriate for the current renderer/palette pair.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ToolStripRenderer CreateToolStripRenderer()
        {
            return Renderer.RenderToolStrip(GetResolvedPalette());
        }

		/// <summary>
		/// Gets or sets the background image displayed in the control.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		public override Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}

		/// <summary>
		/// Gets or sets the background image layout.
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		public override ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
		}

        /// <summary>
        /// Attach the control to global events.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AttachGlobalEvents()
        {
            if (!_globalEvents)
            {
                UpdateGlobalEvents(true);
                _globalEvents = true;
            }
        }

        /// <summary>
        /// Attach the control to global events.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void UnattachGlobalEvents()
        {
            if (_globalEvents)
            {
                UpdateGlobalEvents(false);
                _globalEvents = false;
            }
        }

        /// <summary>
        /// Called when a context menu has just been closed.
        /// </summary>
        protected virtual void ContextMenuClosed()
        {
        }
        #endregion

        #region Public Override
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
        /// Gets or sets the border style for the VisualPanel. 
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }
        #endregion

        #region Public IKryptonDebug
        /// <summary>
        /// Reset the internal counters.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void KryptonResetCounters()
        {
            ViewManager.ResetCounters();
        }

        /// <summary>
        /// Gets the number of layout cycles performed since last reset.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int KryptonLayoutCounter
        {
            get { return ViewManager.LayoutCounter; }
        }

        /// <summary>
        /// Gets the number of paint cycles performed since last reset.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int KryptonPaintCounter
        {
            get { return ViewManager.PaintCounter; }
        }
        #endregion

        #region Protected
        /// <summary>
		/// Gets and sets the ViewManager instance.
		/// </summary>
		protected ViewManager ViewManager
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _viewManager; }
			set { _viewManager = value; }
		}

		/// <summary>
		/// Gets access to the palette redirector.
		/// </summary>
		protected PaletteRedirect Redirector
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _redirector; }
		}

        /// <summary>
        /// Gets access to the need paint delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintDelegate
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _needPaintDelegate; }
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");

            // Change in setting means we need to evaluate transparent painting
            _evalTransparent = true;

            // If required, layout the control
            if (e.NeedLayout)
                _layoutDirty = true;

            if (IsHandleCreated && (!_refreshAll || !e.InvalidRect.IsEmpty))
            {
                // Always request the repaint immediately
                if (e.InvalidRect.IsEmpty)
                {
                    _refreshAll = true;
                    Invalidate();
                }
                else
                    Invalidate(e.InvalidRect);

                // Do we need to use an Invoke to force repaint?
                if (!_refresh && EvalInvokePaint)
                    BeginInvoke(_refreshCall);

                // A refresh is outstanding
                _refresh = true;
            }
        }

        /// <summary>
        /// Gets a value indicating if transparent paint is needed
        /// </summary>
        protected bool NeedTransparentPaint
        {
            get
            {
                // Do we need to evaluate the need for a tranparent paint
                if (_evalTransparent)
                {
                    _paintTransparent = EvalTransparentPaint();

                    // Answer is cached until paint values are changed
                    _evalTransparent = false;
                }

                return _paintTransparent;
            }
        }

        /// <summary>
        /// Gets the resolved palette to actually use when drawing.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IPalette GetResolvedPalette()
        {
            return _palette;
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnInitialized(EventArgs e)
        {
            if (Initialized != null)
                Initialized(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the PaletteChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnPaletteChanged(EventArgs e)
        {
            // Update the redirector with latest palette
            Redirector.Target = _palette;

            // A new palette source means we need to layout and redraw
            OnNeedPaint(Palette, new NeedLayoutEventArgs(true));

            if (PaletteChanged != null)
                PaletteChanged(this, e);
        }

        /// <summary>
        /// Work out if this control needs to paint transparent areas.
        /// </summary>
        /// <returns>True if paint required; otherwise false.</returns>
        protected virtual bool EvalTransparentPaint()
        {
            // Do we have a manager to use for asking about painting?
            if (ViewManager != null)
            {
                // Ask the view if it needs to paint transparent areas
                return ViewManager.EvalTransparentPaint(_renderer);
            }
            else
            {
                // If there is no view then do not transparent paint
                return false;
            }
        }

        /// <summary>
        /// Work out if this control needs to use Invoke to force a repaint.
        /// </summary>
        protected virtual bool EvalInvokePaint
        {
            get
            {
                // By default the paint can occur safely via a simple Invalidate() call,
                // but some controls might need to override this the entire client area can
                // be covered by child controls and so Invalidate() becomes redundant and the
                // control is never layed out.
                return false;
            }
        }

        /// <summary>
        /// Gets the control reference that is the parent for transparent drawing.
        /// </summary>
        protected virtual Control TransparentParent
        {
            get { return Parent; }
        }

        /// <summary>
        /// Processes a notification from palette storage of a button spec change.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnButtonSpecChanged(object sender, EventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");
        }

        /// <summary>
        /// Update global event attachments.
        /// </summary>
        /// <param name="attach">True if attaching; otherwise false.</param>
        protected virtual void UpdateGlobalEvents(bool attach)
        {
            if (attach)
            {
                KryptonManager.GlobalPaletteChanged += new EventHandler(OnGlobalPaletteChanged);
                SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);
            }
            else
            {
                KryptonManager.GlobalPaletteChanged -= new EventHandler(OnGlobalPaletteChanged);
                SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
		/// Gets the default size of the control.
		/// </summary>
		protected override Size DefaultSize
		{
			get { return new Size(100, 100); }
		}

        /// <summary>
        /// Raises the RightToLeftChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            OnNeedPaint(null, new NeedLayoutEventArgs(true));
            base.OnRightToLeftChanged(e);
        }

		/// <summary>
		/// Raises the Layout event.
		/// </summary>
        /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
		protected override void OnLayout(LayoutEventArgs levent)
		{
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager to use for laying out?
                if (ViewManager != null)
                {
                    // Prevent infinite loop by looping a maximum number of times
                    int max = 5;

                    do
                    {
                        // Layout cannot now be dirty
                        _layoutDirty = false;

                        // Ask the view to peform a layout
                        ViewManager.Layout(_renderer);

                    } while (_layoutDirty && (max-- > 0));

                    // Remember size when last layout was performed
                    _lastLayoutSize = Size;
                }
            }

			// Let base class layout child controls
			base.OnLayout(levent);
		}

		/// <summary>
		/// Raises the Paint event.
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager to use for painting?
                if (ViewManager != null)
                {
                    // If the layout is dirty, or the size of the control has changed 
                    // without a layout being performed, then perform a layout now
                    if (_layoutDirty && (!Size.Equals(_lastLayoutSize)))
                        PerformLayout();

                    // Draw the background as transparent, by drawing parent
                    PaintTransparentBackground(e);

                    // Ask the view to repaint the visual structure
                    ViewManager.Paint(_renderer, e);

                    // Request for a refresh has been serviced
                    _refresh = false;
                    _refreshAll = false;
                }
            }
		}

		/// <summary>
		/// Raises the MouseMove event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (ViewManager != null)
                    ViewManager.MouseMove(e, new Point(e.X, e.Y));
            }

			// Let base class fire events
			base.OnMouseMove(e);
		}

        /// <summary>
		/// Raises the MouseDown event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (ViewManager != null)
                    ViewManager.MouseDown(e, new Point(e.X, e.Y));
            }

			// Let base class fire events
			base.OnMouseDown(e);
		}

		/// <summary>
		/// Raises the MouseUp event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (ViewManager != null)
                    ViewManager.MouseUp(e, new Point(e.X, e.Y));
            }

			// Let base class fire events
			base.OnMouseUp(e);
		}

		/// <summary>
		/// Raises the MouseLeave event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (ViewManager != null)
                    ViewManager.MouseLeave(e);
            }

			// Let base class fire events
			base.OnMouseLeave(e);
		}

        /// <summary>
        /// Raises the DoubleClick event.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnDoubleClick(EventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (ViewManager != null)
                    ViewManager.DoubleClick(this.PointToClient(Control.MousePosition));
            }

            // Let base class fire events
            base.OnDoubleClick(e);
        }

        /// <summary>
        /// Raises the KeyDown event.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing key messages?
                if (ViewManager != null)
                    ViewManager.KeyDown(e);
            }

            // Let base class fire events
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Raises the KeyPress event.
        /// </summary>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing key messages?
                if (ViewManager != null)
                    ViewManager.KeyPress(e);
            }

            // Let base class fire events
            base.OnKeyPress(e);
        }

        /// <summary>
        /// Raises the KeyUp event.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing key messages?
                if (ViewManager != null)
                    ViewManager.KeyUp(e);
            }

            // Let base class fire events
            base.OnKeyUp(e);
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing source messages?
                if (ViewManager != null)
                    ViewManager.GotFocus();
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
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing source messages?
                if (ViewManager != null)
                    ViewManager.LostFocus();
            }

            // Let base class fire standard event
            base.OnLostFocus(e);
        }
        #endregion

		#region Implementation
        private void SetPalette(IPalette palette)
        {
            if (palette != _palette)
            {
                // Unhook from current palette events
                if (_palette != null)
                {
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnNeedPaint);
                    _palette.ButtonSpecChanged -= new EventHandler(OnButtonSpecChanged);
                }

                // Remember the new palette
                _palette = palette;

                // Get the renderer associated with the palette
                _renderer = _palette.GetRenderer();

                // Hook to new palette events
                if (_palette != null)
                {
                    _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnNeedPaint);
                    _palette.ButtonSpecChanged += new EventHandler(OnButtonSpecChanged);
                }
            }
        }

        private void PaintTransparentBackground(PaintEventArgs e)
		{
            // Get the parent control for transparent drawing purposes
            Control parent = TransparentParent;

            // Do we have a parent control and we need to paint background?
            if ((parent != null) && NeedTransparentPaint)
            {
                // Only grab the required reference once
                if (_miPTB == null)
                {
                    // Use reflection so we can call the Windows Forms internal method for painting parent background
                    _miPTB = typeof(Control).GetMethod("PaintTransparentBackground",
                                                       BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                                                       null, CallingConventions.HasThis,
                                                       new Type[] { typeof(PaintEventArgs), typeof(Rectangle), typeof(Region) },
                                                       null);
                }

                _miPTB.Invoke(this, new object[] { e, ClientRectangle, null });
            }
            else
            {
                // No parent information available, so just use a standard brush
                e.Graphics.FillRectangle(SystemBrushes.Control, ClientRectangle);
            }
		}

        private void OnPerformRefresh()
        {
            // If we still need to perform the refresh
            if (_refresh)
            {
                // Perform the requested paint of the control
                Refresh();

                // If the layout is still dirty after the refresh
                if (_layoutDirty)
                {
                    // Then non of the control is visible, so perform manual request
                    // for a layout to ensure that child controls can be resized
                    PerformLayout();

                    // Need another repaint to take the layout change into account
                    Refresh();
                }

                // Refresh request has been serviced
                _refresh = false;
                _refreshAll = false;
            }
        }

        private void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            // We only care if we are using the global palette
            if (PaletteMode == PaletteMode.Global)
            {
                // Update ourself with the new global palette
                _localPalette = null;
                SetPalette(KryptonManager.CurrentGlobalPalette);
                Redirector.Target = _palette;

                // A new palette source means we need to layout and redraw
                OnNeedPaint(Palette, new NeedLayoutEventArgs(true));
            }
        }

        private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            PerformNeedPaint(true);
        }

        private void OnContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            // Get the actual strip instance
            ContextMenuStrip cms = base.ContextMenuStrip;

            // Make sure it has the correct renderer
            cms.Renderer = CreateToolStripRenderer();
        }

        private void OnContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            ContextMenuClosed();
        }
        #endregion
    }
}
