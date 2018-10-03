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
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Base class used for implementation of actual controls.
	/// </summary>
	[ToolboxItem(false)]
	[DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public abstract class VisualControlBase : Control, 
                                              IKryptonDebug
    {
        #region Static Field
        private static MethodInfo _miPTB;
        #endregion

        #region Instance Fields
        private bool _layoutDirty;
        private bool _refresh;
        private bool _refreshAll;
        private bool _paintTransparent;
        private bool _evalTransparent;
        private bool _globalEvents;
        private int _dirtyPaletteCounter;
        private IPalette _localPalette;
		private IPalette _palette;
        private IRenderer _renderer;
		private PaletteRedirect _redirector;
		private PaletteMode _paletteMode;
		private ViewManager _viewManager;
        private SimpleCall _refreshCall;
        private SimpleCall _layoutCall;
        private NeedPaintHandler _needPaintDelegate;
        private NeedPaintHandler _needPaintPaletteDelegate;
        private KryptonContextMenu _kryptonContextMenu;
        #endregion

		#region Events
		/// <summary>
		/// Occurs when the palette changes.
		/// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the value of the Palette property is changed.")]
		public event EventHandler PaletteChanged;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the VisualControl class.
		/// </summary>
        protected VisualControlBase()
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

			// We need to repaint entire control whenever resized
			SetStyle(ControlStyles.ResizeRedraw, true);

            // Yes, we want to be drawn double buffered by default
            DoubleBuffered = true;

            // Setup the invokes
            _refreshCall = new SimpleCall(OnPerformRefresh);
            _layoutCall = new SimpleCall(OnPerformLayout);

            // Setup the need paint delegate
            _needPaintDelegate = new NeedPaintHandler(OnNeedPaint);
            _needPaintPaletteDelegate = new NeedPaintHandler(OnPaletteNeedPaint);

			// Must layout before first draw attempt
			_layoutDirty = true;
            _evalTransparent = true;
            _dirtyPaletteCounter = 1;

            // Set the palette and renderer to the defaults as specified by the manager
            _localPalette = null;
            SetPalette(KryptonManager.CurrentGlobalPalette);
            _paletteMode = PaletteMode.Global;

            // Create constant target for resolving palette delegates
            _redirector = CreateRedirector();

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
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPaletteNeedPaint);
                    _palette.ButtonSpecChanged -= new EventHandler(OnButtonSpecChanged);
                    _palette.BasePaletteChanged -= new EventHandler(OnBaseChanged);
                    _palette.BaseRendererChanged -= new EventHandler(OnBaseChanged);
                }

                UnattachGlobalEvents();
                if (ViewManager != null)
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
        /// Gets and sets the KryptonContextMenu to show when right clicked.
        /// </summary>
        [Category("Behavior")]
        [Description("The shortcut menu to show when the user right-clicks the page.")]
        [DefaultValue(null)]
        public virtual KryptonContextMenu KryptonContextMenu
        {
            get { return _kryptonContextMenu; }

            set
            {
                if (_kryptonContextMenu != value)
                {
                    if (_kryptonContextMenu != null)
                    {
                        _kryptonContextMenu.Closed -= new ToolStripDropDownClosedEventHandler(OnContextMenuClosed);
                        _kryptonContextMenu.Disposed -= new EventHandler(OnKryptonContextMenuDisposed);
                    }

                    _kryptonContextMenu = value;

                    if (_kryptonContextMenu != null)
                    {
                        _kryptonContextMenu.Closed += new ToolStripDropDownClosedEventHandler(OnContextMenuClosed);
                        _kryptonContextMenu.Disposed += new EventHandler(OnKryptonContextMenuDisposed);
                    }
                }
            }
        }

        /// <summary>
        /// Fires the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void PerformNeedPaint(bool needLayout)
        {
            OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));
        }

        /// <summary>
        /// Check if the layout is dirty and if so perform the layout now.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void CheckPerformLayout()
        {
            // Cannot process a disposed control, is the layout dirty?
            if (!IsDisposed && !Disposing && _layoutDirty)
            {
                PerformLayout();

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
                }
            }
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
        /// Gets the ViewManager instance.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ViewManager GetViewManager()
        {
            return _viewManager;
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

        /// <summary>
        /// Gets and sets the dirty palette counter.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DirtyPaletteCounter
        {
            get { return _dirtyPaletteCounter; }
            set { _dirtyPaletteCounter = value; }
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

        /// <summary>
        /// Gets the view associated with the provided client location.
        /// </summary>
        /// <param name="pt">Point to lookup.</param>
        /// <returns>ViewBase associated with the point.</returns>
        public ViewBase ViewFromPoint(Point pt)
        {
            if ((ViewManager != null) && (ViewManager.Root != null))
                return ViewManager.Root.ViewFromPoint(pt);

            return null;
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
            get { return _needPaintDelegate; }
        }

        /// <summary>
        /// Gets access to the need paint palette delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintPaletteDelegate
        {
            get { return _needPaintPaletteDelegate; }
        }

        /// <summary>
        /// Force the control to perform a krypton layout to calculate size and positioning.
        /// </summary>
        /// <returns>True if layout was p</returns>
        protected bool ForceViewLayout()
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing)
            {
                // Do we have a manager to use for laying out?
                if (ViewManager != null)
                {
                    // Ask the view to peform a layout
                    ViewManager.Layout(_renderer);

                    return true;
                }
            }

            // Failed to perform a layout cycle
            return false;
        }

        /// <summary>
        /// Request a layout be performed before any painting occurs.
        /// </summary>
        protected void InvokeLayout()
        {
            // We want a layout to occur but not until the message loop
            // is spun. So this will happen before any painting because
            // paint messages only occur when the message queue is empty.
            BeginInvoke(_layoutCall);
        }

        /// <summary>
        /// Mark the layout as being dirty and needing to be performed.
        /// </summary>
        protected void MarkLayoutDirty()
        {
            _layoutDirty = true;
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
        /// Perform background painting with the provided default values.
        /// </summary>
        /// <param name="g">Graphics reference for drawing.</param>
        /// <param name="backBrush">Brush to use when painting.</param>
        /// <param name="backRect">Client area to paint.</param>
        protected virtual void PaintBackground(Graphics g, Brush backBrush, Rectangle backRect)
        {
            g.FillRectangle(backBrush, backRect);
        }

		/// <summary>
		/// Gets a value indicating is processing of mnemonics should be allowed.
		/// </summary>
		/// <returns>True to allow; otherwise false.</returns>
		protected bool CanProcessMnemonic()
		{
			Control c = this;

			// Test each control in parent chain
			while (c != null)
			{
				// Control must be visible and enabled
				if (!c.Visible || !c.Enabled)
					return false;

				// Move up one level
				c = c.Parent;
			}

			// Every control in chain is visible and enabled, so allow mnemonics
			return true;
		}
        #endregion

        #region Protected Virtual
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
        /// Raises the PaletteChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnPaletteChanged(EventArgs e)
        {
            // Update the redirector with latest palette
            Redirector.Target = _palette;

            // Need to recalculate anything relying on the palette
            DirtyPaletteCounter++;
            
            // A new palette source means we need to layout and redraw
            OnNeedPaint(Palette, new NeedLayoutEventArgs(true));

            if (PaletteChanged != null)
                PaletteChanged(this, e);
        }

        /// <summary>
        /// Processes a notification from palette of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected virtual void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // Need to recalculate anything relying on the palette
            DirtyPaletteCounter++;
            OnNeedPaint(sender, e);
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected virtual void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");

            // Never try and redraw or layout when disposed are trying to dispose
            if (!IsDisposed && !Disposing)
            {
                // Change in setting means we need to evaluate transparent painting
                _evalTransparent = true;

                // If required, layout the control
                if (e.NeedLayout && !_layoutDirty)
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
        }

        /// <summary>
        /// Create the redirector instance.
        /// </summary>
        /// <returns>PaletteRedirect derived class.</returns>
        protected virtual PaletteRedirect CreateRedirector()
        {
            return new PaletteRedirect(_palette);
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
        /// Raises the RightToLeftChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            // Need to recalculate anything relying on the palette
            DirtyPaletteCounter++;

            // Need relayout to reflect change of layout
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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
            {
                // Do we have a manager to use for painting?
                if (ViewManager != null)
                {
                    // If the layout is dirty, then perform a layout now
                    if (_layoutDirty)
                    {
                        Size beforeSize = ClientSize;
                        
                        PerformLayout();
                        
                        // Did the layout cause a change in the size of the control?
                        if ((beforeSize.Width < ClientSize.Width) ||
                            (beforeSize.Height < ClientSize.Height))
                        {
                            // Have to reset the _refresh before calling need paint otherwise
                            // it will not create another invalidate or invoke call as necessary
                            _refresh = false;
                            _refreshAll = false;
                            PerformNeedPaint(false);
                        }
                    }

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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
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
            if (!IsDisposed && !Disposing)
            {
                // Do we have a manager for processing source messages?
                if (ViewManager != null)
                    ViewManager.LostFocus();
            }

			// Let base class fire standard event
			base.OnLostFocus(e);
		}

        /// <summary>
        /// Occurs when the global palette has been changed.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected virtual void OnGlobalPaletteChanged(object sender, EventArgs e)
        {
            // We only care if we are using the global palette
            if (PaletteMode == PaletteMode.Global)
            {
                // Update ourself with the new global palette
                _localPalette = null;
                SetPalette(KryptonManager.CurrentGlobalPalette);
                Redirector.Target = _palette;

                // Need to recalculate anything relying on the palette
                DirtyPaletteCounter++;

                // A new palette source means we need to layout and redraw
                OnNeedPaint(Palette, new NeedLayoutEventArgs(true));

                // Must raise event to change palette in redirector
                OnPaletteChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Occurs when a user preference has changed.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event details.</param>
        protected virtual void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            // Need to recalculate anything relying on the palette
            DirtyPaletteCounter++;
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg">A Message, passed by reference, that represents the window message to process.</param>
        /// <param name="keyData">One of the Keys values that represents the key to process.</param>
        /// <returns>True is handled; otherwise false.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // If we have a defined context menu then need to check for matching shortcut
            if (KryptonContextMenu != null)
                if (KryptonContextMenu.ProcessShortcut(keyData))
                    return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            // We need to snoop the need to show a context menu
            if (m.Msg == PI.WM_CONTEXTMENU)
            {
                // Only interested in overriding the behavior when we have a krypton context menu...
                if (KryptonContextMenu != null)
                {
                    // Extract the screen mouse position (if might not actually be provided)
                    Point mousePt = new Point(PI.LOWORD(m.LParam), PI.HIWORD(m.LParam));

                    // If keyboard activated, the menu position is centered
                    if (((int)((long)m.LParam)) == -1)
                        mousePt = new Point(Width / 2, Height / 2);
                    else
                    {
                        mousePt = PointToClient(mousePt);

                        // Mouse point up and left 1 pixel so that the mouse overlaps the top left corner
                        // of the showing context menu just like it happens for a ContextMenuStrip.
                        mousePt.X -= 1;
                        mousePt.Y -= 1;
                    }

                    // If the mouse posiiton is within our client area
                    if (ClientRectangle.Contains(mousePt))
                    {
                        // Show the context menu
                        KryptonContextMenu.Show(this, PointToScreen(mousePt));

                        // We eat the message!
                        return;
                    }
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Called when a context menu has just been closed.
        /// </summary>
        protected virtual void ContextMenuClosed()
        {
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
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPaletteNeedPaint);
                    _palette.ButtonSpecChanged -= new EventHandler(OnButtonSpecChanged);
                    _palette.BasePaletteChanged -= new EventHandler(OnBaseChanged);
                    _palette.BaseRendererChanged -= new EventHandler(OnBaseChanged);
                }

                // Remember the new palette
                _palette = palette;

                // Get the renderer associated with the palette
                _renderer = _palette.GetRenderer();

                // Hook to new palette events
                if (_palette != null)
                {
                    _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPaletteNeedPaint);
                    _palette.ButtonSpecChanged += new EventHandler(OnButtonSpecChanged);
                    _palette.BasePaletteChanged += new EventHandler(OnBaseChanged);
                    _palette.BaseRendererChanged += new EventHandler(OnBaseChanged);
                }
            }
        }

        private void OnBaseChanged(object sender, EventArgs e)
        {
            // Change in base renderer or base palette require we fetch the latest renderer
            _renderer = _palette.GetRenderer();
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
                // Request the background be painted in the system colors
                PaintBackground(e.Graphics, SystemBrushes.Control, ClientRectangle);
			}
		}

        private void OnPerformRefresh()
        {
            // If we still need to perform the refresh
            if (_refresh)
            {
                // Perform the requested refresh now to force repaint
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

        private void OnPerformLayout()
        {
            // Then non of the control is visible, so perform manual request
            // for a layout to ensure that child controls can be resized
            PerformLayout();

            // Make sure we refresh the display to show the change
            BeginInvoke(_refreshCall);
        }

        private void OnContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            // Get the actual strip instance
            ContextMenuStrip cms = base.ContextMenuStrip;

            // Make sure it has the correct renderer
            cms.Renderer = CreateToolStripRenderer();
        }

        private void OnKryptonContextMenuDisposed(object sender, EventArgs e)
        {
            // When the current krypton context menu is disposed, we should remove 
            // it to prevent it being used again, as that would just throw an exception 
            // because it has been disposed.
            KryptonContextMenu = null;
        }

        private void OnContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            ContextMenuClosed();
        }
        #endregion
    }
}
