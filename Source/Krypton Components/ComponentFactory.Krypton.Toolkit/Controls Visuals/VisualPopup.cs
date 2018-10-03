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
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Base class used for implementation of popup controls.
	/// </summary>
	[ToolboxItem(false)]
	[DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class VisualPopup : ContainerControl
	{
        #region Instance Fields
        private bool _layoutDirty;
        private bool _refresh;
        private bool _refreshAll;
        private SimpleCall _refreshCall;
        private ViewManager _viewManager;
        private IRenderer _renderer;
        private NeedPaintHandler _needPaintDelegate;
        private EventHandler _dismissedDelegate;
        private VisualPopupShadow _shadow;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the VisualPopup class.
        /// </summary>
        /// <param name="shadow">Does the popup need a shadow effect.</param>
        public VisualPopup(bool shadow)
            : this(new ViewManager(), null, shadow)
        {
        }

        /// <summary>
        /// Initialize a new instance of the VisualPopup class.
		/// </summary>
        /// <param name="renderer">Drawing renderer.</param>
        /// <param name="shadow">Does the popup need a shadow effect.</param>
        public VisualPopup(IRenderer renderer,
                           bool shadow)
            : this(new ViewManager(), renderer, shadow)
        {
        }

        /// <summary>
        /// Initialize a new instance of the VisualPopup class.
		/// </summary>
        /// <param name="viewManager">View manager instance for managing view display.</param>
        /// <param name="renderer">Drawing renderer.</param>
        /// <param name="shadow">Does the popup need a shadow effect.</param>
        public VisualPopup(ViewManager viewManager,
                           IRenderer renderer,
                           bool shadow)
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
			//	False - Selectable
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

            // Cannot select control by using mouse to click it
            SetStyle(ControlStyles.Selectable, false);

            // Cache incoming references
            _renderer = renderer;
            _viewManager = viewManager;

            // Setup the need paint delegate
            _needPaintDelegate = new NeedPaintHandler(OnNeedPaint);

            // Setup the invokes
            _refreshCall = new SimpleCall(OnPerformRefresh);

            // Default other properties
            _layoutDirty = true;
            _refresh = true;

            // Create the shadow control
            if (shadow)
                _shadow = new VisualPopupShadow();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remove our show instance
                if (_shadow != null)
                {
                    _shadow.Dispose();
                    _shadow = null;
                }
            }

            base.Dispose(disposing);

            // Remove the active view, so the last view element to 
            // have mouse input has a chance to cleanup processing
            if (ViewManager != null)
            {
                ViewManager.ActiveView = null;
                ViewManager.Dispose();
            }

            // Do we have a delegate to fire when popup is dismissed?
            if (_dismissedDelegate != null)
            {
                _dismissedDelegate(this, EventArgs.Empty);
                _dismissedDelegate = null;
            }
        }
		#endregion

        #region Public
        /// <summary>
        /// Show the popup using the provided rectangle as the screen rect.
        /// </summary>
        /// <param name="screenRect">Screen rectangle for showing the popup.</param>
        public virtual void Show(Rectangle screenRect)
        {
            // Update the screen position
            SetBounds(screenRect.X, screenRect.Y, 
                      screenRect.Width, screenRect.Height);

            // If we have a shadow then update it now
            if (_shadow != null)
                _shadow.Show(screenRect);

            // Show the window without activating it (i.e. do not take focus)
            PI.ShowWindow(this.Handle, PI.SW_SHOWNOACTIVATE);

            // Use manager to track mouse/keyboard input and to dismiss the window
            VisualPopupManager.Singleton.StartTracking(this);
        }

        /// <summary>
        /// Show the popup with the given size but relative to the provided parent rectangle.
        /// </summary>
        /// <param name="parentScreenRect">Screen rectangle of parent area.</param>
        /// <param name="popupSize">Size of the popup to show.</param>
        public virtual void Show(Rectangle parentScreenRect, Size popupSize)
        {
            // Show the requested popup below the parent screen rectangle
            Show(CalculateBelowPopupRect(parentScreenRect, popupSize));
        }

        /// <summary>
        /// Define the drawing paths for the shadow.
        /// </summary>
        /// <param name="path1">Outer path.</param>
        /// <param name="path2">Middle path.</param>
        /// <param name="path3">Inside path.</param>
        public virtual void DefineShadowPaths(GraphicsPath path1,
                                              GraphicsPath path2,
                                              GraphicsPath path3)
        {
            if (_shadow != null)
                _shadow.DefinePaths(path1, path2, path3);
            else
            {
                path1.Dispose();
                path2.Dispose();
                path3.Dispose();
            }
        }

        /// <summary>
        /// Should a mouse down at the provided point cause an end to popup tracking.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to end tracking; otherwise false.</returns>
        public virtual bool DoesCurrentMouseDownEndAllTracking(Message m, Point pt)
        {
            bool endTracking = !ClientRectangle.Contains(pt);

            // The mouse is not over our client area but the focus is
            if (endTracking && ContainsFocus)
            {
                // Get the window handle of the window under this screen point
                Point screenPt = PointToScreen(pt);
                PI.POINT screenPIPt = new PI.POINT();
                screenPIPt.x = screenPt.X;
                screenPIPt.y = screenPt.Y;
                IntPtr hWnd = PI.WindowFromPoint(screenPIPt);

                // Assuming we got back a valid window handle
                if (hWnd != IntPtr.Zero)
                {
                    StringBuilder className = new StringBuilder(256);
                    int length = PI.GetClassName(hWnd, className, className.Capacity);

                    // If we got back a valid name
                    if (length > 0)
                    {
                        // If let the message occur as it is being pressed on a combo box 
                        // drop down list and so it will process the message appropriately
                        if (className.ToString() == "ComboLBox")
                            endTracking = false;
                    }
                }
            }

            return endTracking;
        }

        /// <summary>
        /// Should a mouse down at the provided point allow tracking to continue.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to continue tracking; otherwise false.</returns>
        public virtual bool DoesCurrentMouseDownContinueTracking(Message m, Point pt)
        {
            return !DoesCurrentMouseDownEndAllTracking(m, pt);
        }

        /// <summary>
        /// Should a mouse down at the provided point cause it to become the current tracking popup.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to become current; otherwise false.</returns>
        public virtual bool DoesStackedClientMouseDownBecomeCurrent(Message m, Point pt)
        {
            return ClientRectangle.Contains(pt);
        }

        /// <summary>
        /// Should the mouse down be eaten when the tracking has been ended.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Screen coordinates point.</param>
        /// <returns>True to eat message; otherwise false.</returns>
        public virtual bool DoesMouseDownGetEaten(Message m, Point pt)
        {
            return false;
        }

        /// <summary>
        /// Is a change in active window to this popup when it is current allowed.
        /// </summary>
        public virtual bool AllowBecomeActiveWhenCurrent
        {
            get { return true; }
        }

        /// <summary>
        /// Should the mouse move at provided screen point be allowed.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to alow; otherwise false.</returns>
        public virtual bool AllowMouseMove(Message m, Point pt)
        {
            // If we have the focus then we always allow the mouse move
            if (ContainsFocus)
                return true;
            else
            {
                // If the mouse is over this popup then allow
                return RectangleToScreen(ClientRectangle).Contains(pt);
            }
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
        /// Gets the resolved palette to actually use when drawing.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual IPalette GetResolvedPalette()
        {
            return null;
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
            set { _renderer = value; }
        }

        /// <summary>
        /// Fires the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void PerformNeedPaint(bool needLayout)
        {
            OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));
        }

        /// <summary>
        /// Gets and sets the delegate to fire when the popup is dismissed.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public EventHandler DismissedDelegate
        {
            get { return _dismissedDelegate; }
            set { _dismissedDelegate = value; }
        }

        /// <summary>
        /// Gets a value indicating if the keyboard is passed to this popup.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual bool KeyboardInert
        {
            get { return false; }
        }

        /// <summary>
        /// Gets access to the view manager of the popup.
        /// </summary>
        /// <returns></returns>
        public ViewManager GetViewManager()
        {
            return ViewManager;
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
        /// Gets access to the need paint delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintDelegate
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _needPaintDelegate; }
        }
        #endregion

        #region Protected Virtual
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
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected virtual void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");

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
        #endregion

        #region Protected Override
        /// <summary>
        /// Gets the creation parameters.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Parent = IntPtr.Zero;
                cp.Style = unchecked((int)PI.WS_POPUP);
                cp.ExStyle = PI.WS_EX_TOPMOST + PI.WS_EX_TOOLWINDOW;
                return cp;
            }
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
                        ViewManager.Layout(Renderer);

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
            if (!IsDisposed)
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

                    // Ask the view to repaint the visual structure
                    ViewManager.Paint(Renderer, e);

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

            // Do not call base class! Prevent capture of the mouse
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

            // Do not call base class! Prevent capture of the mouse
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
        /// Processes a dialog key.
        /// </summary>
        /// <param name="keyData">One of the Keys values that represents the key to process.</param>
        /// <returns>true if the key was processed; otherwise false.</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // If the user pressed the escape key
                if (keyData == Keys.Escape)
                {
                    // Kill ourself
                    Dispose();

                    // Yes, we processed the message
                    return true;
                }
            }

            return base.ProcessDialogKey(keyData);
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
                // If the user pressed the escape key
                if (e.KeyData == Keys.Escape)
                {
                    // Kill ourself
                    Dispose();
                }
                else
                {
                    // Do we have a manager for processing key messages?
                    if (ViewManager != null)
                        ViewManager.KeyDown(e);
                }
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
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows Message to process.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case PI.WM_MOUSEACTIVATE:
                    // Prevent the popup window becoming active just because the user has
                    // pressed the mouse over the window, so return NOACTIVATE as result.
                    m.Result = (IntPtr)PI.MA_NOACTIVATE;
                    return;
            }

            base.WndProc(ref m);
        }
        #endregion

        #region Implementation
        private Rectangle CalculateBelowPopupRect(Rectangle parentScreenRect, Size popupSize)
        {
            // Get the screen that the parent rectangle is mostly within, this is the
            // screen we will attempt to place the entire popup within
            Screen screen = Screen.FromRectangle(parentScreenRect);

            Point popupLocation = new Point(parentScreenRect.X, parentScreenRect.Bottom);

            // Is there enough room below the parent for the entire popup height?
            if ((parentScreenRect.Bottom + popupSize.Height) <= screen.WorkingArea.Bottom)
            {
                // Place the popup below the parent
                popupLocation.Y = parentScreenRect.Bottom;
            }
            else
            {
                // Is there enough room above the parent for the enture popup height?
                if ((parentScreenRect.Top - popupSize.Height) >= screen.WorkingArea.Top)
                {
                    // Place the popup above the parent
                    popupLocation.Y = parentScreenRect.Top - popupSize.Height;
                }
                else
                {
                    // Cannot show entire popup above or below, find which has most space
                    int spareAbove = parentScreenRect.Top - screen.WorkingArea.Top;
                    int spareBelow = screen.WorkingArea.Bottom - parentScreenRect.Bottom;

                    // Place it in the area with the most space
                    if (spareAbove > spareBelow)
                        popupLocation.Y = screen.WorkingArea.Top;
                    else
                        popupLocation.Y = parentScreenRect.Bottom;
                }
            }

            // Prevent the popup from being off the left side of the screen
            if (popupLocation.X < screen.WorkingArea.Left)
                popupLocation.X = screen.WorkingArea.Left;

            // Preven the popup from being off the right size of the screen
            if ((popupLocation.X + popupSize.Width) > screen.WorkingArea.Right)   
                popupLocation.X = screen.WorkingArea.Right - popupSize.Width;

            return new Rectangle(popupLocation, popupSize);
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
        #endregion
    }
}
