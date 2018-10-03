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
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Process mouse events for a standard button.
	/// </summary>
    public class ButtonController : GlobalId,
                                    IMouseController,
                                    IKeyController,
                                    ISourceController
	{
		#region Instance Fields
		private bool _captured;
        private bool _mouseOver;
        private bool _nonClientAsNormal;
        private bool _fixedPressed;
        private bool _becomesFixed;
        private bool _becomesRightFixed;
        private bool _inSplitRectangle;
        private bool _dragging;
        private bool _allowDragging;
        private bool _clickOnDown;
        private bool _repeat;
        private bool _draggingAttempt;
        private bool _preDragOffset;
        private Point _mousePoint;
        private ViewBase _target;
		private NeedPaintHandler _needPaint;
        private Timer _repeatTimer;
        private Rectangle _splitRectangle;
        private Rectangle _dragRect;
        private object _tag;
        #endregion

		#region Events
		/// <summary>
		/// Occurs when the mouse is used to left click the target.
		/// </summary>
		public event MouseEventHandler Click;

        /// <summary>
        /// Occurs when the mouse is used to right click the target.
        /// </summary>
        public event MouseEventHandler RightClick;
        
        /// <summary>
		/// Occurs when the mouse is used to left select the target.
		/// </summary>
		public event MouseEventHandler MouseSelect;

        /// <summary>
        /// Occurs when start of drag operation occurs.
        /// </summary>
        public event EventHandler<DragStartEventCancelArgs> DragStart;

        /// <summary>
        /// Occurs when drag moves.
        /// </summary>
        public event EventHandler<PointEventArgs> DragMove;

        /// <summary>
        /// Occurs when drag ends.
        /// </summary>
        public event EventHandler<PointEventArgs> DragEnd;

        /// <summary>
        /// Occurs when drag quits.
        /// </summary>
        public event EventHandler DragQuit;

        /// <summary>
        /// Occurs when the drag rectangle for the button is required.
        /// </summary>
        public event EventHandler<ButtonDragRectangleEventArgs> ButtonDragRectangle;

        /// <summary>
        /// Occurs when the dragging inside the button drag rectangle.
        /// </summary>
        public event EventHandler<ButtonDragOffsetEventArgs> ButtonDragOffset;
        #endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the ButtonController class.
		/// </summary>
		/// <param name="target">Target for state changes.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public ButtonController(ViewBase target,
                                NeedPaintHandler needPaint)
		{
			Debug.Assert(target != null);

            _mousePoint = CommonHelper.NullPoint;
            _splitRectangle = CommonHelper.NullRectangle;
            _inSplitRectangle = false;
            _allowDragging = false;
            _dragging = false;
            _clickOnDown = false;
			_target = target;
            _repeat = false;
            NeedPaint = needPaint;
        }
		#endregion

        #region Tag
        /// <summary>
        /// Gets and sets the user data associated with the controller.
        /// </summary>
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        #endregion

        #region BecomesFixed
        /// <summary>
        /// Gets and sets if the button becomes fixed in pressed appearance when pressed.
        /// </summary>
        public bool BecomesFixed
        {
            get { return _becomesFixed; }
            set { _becomesFixed = value; }
        }
        #endregion

        #region BecomesRightFixed
        /// <summary>
        /// Gets and sets if the button becomes fixed in pressed appearance when pressed.
        /// </summary>
        public bool BecomesRightFixed
        {
            get { return _becomesRightFixed; }
            set { _becomesRightFixed = value; }
        }
        #endregion

        #region RemoveFixed
        /// <summary>
        /// Remove the fixed pressed mode.
        /// </summary>
        public void RemoveFixed()
        {
            if (_fixedPressed)
            {
                // Mouse no longer considered pressed down
                _captured = false;

                // No longer in fixed state mode
                _fixedPressed = false;

                // Update the visual state
                UpdateTargetState(Point.Empty);
            }
        }
        #endregion

        #region MousePoint
        /// <summary>
        /// Gets the current tracking mouse point.
        /// </summary>
        public Point MousePoint
        {
            get { return _mousePoint; }
        }
        #endregion

        #region AllowDragging
        /// <summary>
        /// Gets and sets if dragging is allowed.
        /// </summary>
        public bool AllowDragging
        {
            get { return _allowDragging; }
            set { _allowDragging = value; }
        }
        #endregion

        #region ClearDragRect
        /// <summary>
        /// Reset the dragging rect to prevent any dragging starting.
        /// </summary>
        public void ClearDragRect()
        {
            _dragRect = Rectangle.Empty;
        }
        #endregion
        
        #region ClickOnDown
        /// <summary>
        /// Gets and sets if the press down should cause the click.
        /// </summary>
        public bool ClickOnDown
        {
            get { return _clickOnDown; }
            set { _clickOnDown = value; }
        }
        #endregion

        #region SplitRectangle
        /// <summary>
        /// Gets and sets the area of the button which is split.
        /// </summary>
        public Rectangle SplitRectangle
        {
            get { return _splitRectangle; }
            set { _splitRectangle = value; }
        }
        #endregion

        #region NonClientAsNormal
        /// <summary>
        /// Gets and sets the drawing of a non client mouse position when pressed as normal.
        /// </summary>
        public bool NonClientAsNormal
        {
            get { return _nonClientAsNormal; }
            set { _nonClientAsNormal = value; }
        }
        #endregion

        #region Repeat
        /// <summary>
        /// Gets and sets the need for repeat clicks.
        /// </summary>
        public bool Repeat
        {
            get { return _repeat; }
            set { _repeat = value; }
        }
        #endregion

        #region Mouse Notifications
        /// <summary>
		/// Mouse has entered the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void MouseEnter(Control c)
		{
            // Is the controller allowed to track/click
            if (IsOperating)
            {
                // Mouse is over the target
                _mouseOver = true;

                // Update the visual state
                UpdateTargetState(c);
            }
		}

		/// <summary>
		/// Mouse has moved inside the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
		{
            // Is the controller allowed to track/click
            if (IsOperating)
            {
                // Track the mouse point
                _mousePoint = pt;

                // Update the visual state
                UpdateTargetState(pt);

                // If captured then we might want to handle dragging
                if (Captured)
                {
                    if (AllowDragging)
                    {
                        if (_dragging)
                            OnDragMove(_mousePoint);
                        else if (!_dragRect.IsEmpty && !_dragRect.Contains(_mousePoint))
                        {
                            if (!_draggingAttempt)
                            {
                                _draggingAttempt = true;
                                Point targetOrigin = _target.ClientLocation;
                                Point offset = new Point(_mousePoint.X - targetOrigin.X, _mousePoint.Y - targetOrigin.Y);
                                OnDragStart(_mousePoint, offset, c);
                            }
                        }
                    }

                    if (!_dragging && !_dragRect.IsEmpty && _preDragOffset)
                    {
                        ButtonDragOffsetEventArgs args = new ButtonDragOffsetEventArgs(pt);
                        OnButtonDragOffset(args);
                    }
                }
            }
		}

		/// <summary>
		/// Mouse button has been pressed in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button pressed down.</param>
		/// <returns>True if capturing input; otherwise false.</returns>
        public virtual bool MouseDown(Control c, Point pt, MouseButtons button)
		{
            // Is the controller allowed to track/click
            if (IsOperating)
            {
                // If the button is not enabled then we do nothing on a mouse down
                if (_target.Enabled)
                {
                    // Only interested in left mouse pressing down
                    if (button == MouseButtons.Left)
                    {
                        // Capturing mouse input
                        _captured = true;
                        _draggingAttempt = false;

                        // Use event to discover the rectangle that causes dragging to begin
                        ButtonDragRectangleEventArgs args = new ButtonDragRectangleEventArgs(pt);
                        OnButtonDragRectangle(args);
                        _dragRect = args.DragRect;
                        _preDragOffset = args.PreDragOffset;

                        if (!_fixedPressed)
                        {
                            // Update the visual state
                            UpdateTargetState(pt);

                            // Do we become fixed in the pressed state until RemoveFixed is called?
                            if (BecomesFixed)
                                _fixedPressed = true;

                            // Indicate that the mouse wants to select the elment
                            OnMouseSelect(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));

                            // Generate a click event if we generate click on mouse down
                            if (ClickOnDown)
                            {
                                OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));

                                // If we need to perform click repeats then use a timer...
                                if (Repeat)
                                {
                                    _repeatTimer = new Timer();
                                    _repeatTimer.Interval = SystemInformation.DoubleClickTime;
                                    _repeatTimer.Tick += new EventHandler(OnRepeatTimer);
                                    _repeatTimer.Start();
                                }
                            }
                        }
                    }
                    else if (button == MouseButtons.Right)
                    {
                        if (!_fixedPressed)
                        {
                            // Do we become fixed in the pressed state until RemoveFixed is called?
                            if (BecomesRightFixed)
                                _fixedPressed = true;

                            // Indicate the right mouse was used on the button
                            OnRightClick(new MouseEventArgs(MouseButtons.Right, 1, pt.X, pt.Y, 0));
                        }
                    }
                }
            }

			return _captured;
		}

		/// <summary>
		/// Mouse button has been released in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button released.</param>
        public virtual void MouseUp(Control c, Point pt, MouseButtons button)
		{
            // Is the controller allowed to track/click
            if (IsOperating)
            {
                // If the button is not enabled then we do nothing on a mouse down
                if (_target.Enabled)
                {
                    // Remove the repeat timer
                    if (_repeatTimer != null)
                    {
                        _repeatTimer.Stop();
                        _repeatTimer.Dispose();
                        _repeatTimer = null;
                    }

                    // If the mouse is currently captured
                    if (_captured)
                    {
                        // Not capturing mouse input anymore
                        _captured = false;

                        // Only interested in left mouse being released
                        if (button == MouseButtons.Left)
                        {
                            if (_dragging)
                                OnDragEnd(pt);

                            // Only if the button is still pressed, do we generate a click
                            if ((_target.ElementState == PaletteState.Pressed) ||
                                (_target.ElementState == (PaletteState.Pressed | PaletteState.Checked)))
                            {
                                if (!_fixedPressed)
                                {
                                    // Move back to hot tracking state, we have to do this
                                    // before the click is generated because the click processing
                                    // might change focus and so cause the MouseLeave to be
                                    // called and change the state. If this was after the click
                                    // then it would overwrite and lose that leave state change.
                                    _target.ElementState = PaletteState.Tracking;
                                }

                                // Can only click if enabled
                                if (_target.Enabled && !ClickOnDown)
                                {
                                    // Generate a click event
                                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                                }
                            }

                            // Repaint to reflect new state
                            OnNeedPaint(true);
                        }
                        else
                        {
                            if (_dragging)
                                OnDragQuit();

                            // Update the visual state
                            UpdateTargetState(pt);
                        }
                    }
                }
            }
		}

		/// <summary>
		/// Mouse has left the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        public virtual void MouseLeave(Control c, ViewBase next)
		{
            // Is the controller allowed to track/click
            if (IsOperating)
            {
                // Only if mouse is leaving all the children monitored by controller.
                if (!ViewIsPartOfButton(next))
                {
                    // Remove the repeat timer
                    if (_repeatTimer != null)
                    {
                        _repeatTimer.Stop();
                        _repeatTimer.Dispose();
                        _repeatTimer = null;
                    }

                    // Mouse is no longer over the target
                    _mouseOver = false;

                    // Do we need to update the visual state
                    if (!_fixedPressed)
                    {
                        // Not tracking the mouse means a null value
                        _mousePoint = CommonHelper.NullPoint; 

                        // If leaving the view then cannot be capturing mouse input anymore
                        _captured = false;

                        // End any current dragging operation
                        if (_dragging)
                            OnDragQuit();

                        // Update the visual state
                        UpdateTargetState(c);
                    }
                }
            }
		}

        /// <summary>
        /// Left mouse button double click.
        /// </summary>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void DoubleClick(Point pt)
        {
            // Do nothing
        }

        /// <summary>
        /// Should the left mouse down be ignored when present on a visual form border area.
        /// </summary>
        public virtual bool IgnoreVisualFormLeftButtonDown
        {
            get { return false; }
        }
        #endregion

        #region Key Notifications
        /// <summary>
        /// Key has been pressed down.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public virtual void KeyDown(Control c, KeyEventArgs e)
        {
            Debug.Assert(c != null);
            Debug.Assert(e != null);

            // Validate incoming references
            if (c == null)  throw new ArgumentNullException("c");
            if (e == null)  throw new ArgumentNullException("e");

            if (e.KeyCode == Keys.Space)
            {
                // Enter the captured mode and pretend mouse is over area
                _captured = true;
                _mouseOver = true;

                // Do we become fixed in the pressed state until RemoveFixed is called?
                if (BecomesFixed)
                    _fixedPressed = true;

                // Update target to reflect new state
                _target.ElementState = PaletteState.Pressed;

                // Redraw to show the change in visual state
                OnNeedPaint(true);
            }
        }

        /// <summary>
        /// Key has been pressed.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        public virtual void KeyPress(Control c, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// Key has been released.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public virtual bool KeyUp(Control c, KeyEventArgs e)
        {
            Debug.Assert(c != null);
            Debug.Assert(e != null);

            // Validate incoming references
            if (c == null) throw new ArgumentNullException("c");
            if (e == null) throw new ArgumentNullException("e");

            // If the user pressed the escape key
            if ((e.KeyCode == Keys.Escape) || (e.KeyCode == Keys.Space))
            {
                // If we are capturing mouse input
                if (_captured)
                {
                    // Release the mouse capture
                    c.Capture = false;
                    _captured = false;

                    // End any current dragging operation
                    if (_dragging)
                        OnDragQuit();

                    // Recalculate if the mouse is over the button area
                    _mouseOver = _target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));

                    if (e.KeyCode == Keys.Space)
                    {
                        // Can only click if enabled
                        if (_target.Enabled)
                        {
                            // Generate a click event
                            OnClick(new MouseEventArgs(MouseButtons.Left, 1, -1, -1, 0));
                        }
                    }

                    // Update the visual state
                    UpdateTargetState(c);
                }
            }
            
            return _captured;
        }
        #endregion

        #region Source Notifications
        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void GotFocus(Control c)
        {
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void LostFocus(Control c)
        {
            Debug.Assert(c != null);

            // Validate incoming references
            if (c == null) throw new ArgumentNullException("c");

            // If we are capturing mouse input
            if (_captured)
            {
                // Quit out of any dragging operation
                if (_dragging) 
                {
                    // Do not release capture!
                    OnDragQuit();
                }
                else
                {
                    // Release the mouse capture
                    c.Capture = false;
                    _captured = false;
                }

                // Recalculate if the mouse is over the button area
                _mouseOver = _target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));

                // Update the visual state
                UpdateTargetState(c);
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the need paint delegate for notifying paint requests.
        /// </summary>
        public NeedPaintHandler NeedPaint
        {
            get { return _needPaint; }

            set
            {
                // Warn if multiple sources want to hook their single delegate
                Debug.Assert(((_needPaint == null) && (value != null)) ||
                             ((_needPaint != null) && (value == null)));

                _needPaint = value;
            }
        }

        /// <summary>
        /// Gets access to the associated target of the controller.
        /// </summary>
        public ViewBase Target
        {
            get { return _target; }
        }

		/// <summary>
		/// Fires the NeedPaint event.
		/// </summary>
		public void PerformNeedPaint()
		{
			OnNeedPaint(false);
		}

		/// <summary>
		/// Fires the NeedPaint event.
		/// </summary>
		/// <param name="needLayout">Does the palette change require a layout.</param>
		public void PerformNeedPaint(bool needLayout)
		{
			OnNeedPaint(needLayout);
		}
		#endregion

		#region Protected
        /// <summary>
        /// Get a value indicating if the controller is operating
        /// </summary>
        protected virtual bool IsOperating
        {
            get { return true; }
            set { }
        }

        /// <summary>
        /// Gets a value indicating if the state is pressed only when over button.
        /// </summary>
        protected virtual bool IsOnlyPressedWhenOver
        {
            get { return true; }
            set { }
        }

        /// <summary>
        /// Gets a value indicating if mouse input is being captured.
        /// </summary>
        protected bool Captured
        {
            get { return _captured; }
            set { _captured = value; }
        }

        /// <summary>
        /// Discovers if the provided view is part of the button.
        /// </summary>
        /// <param name="next">View to investigate.</param>
        /// <returns>True is part of button; otherwise false.</returns>
        protected virtual bool ViewIsPartOfButton(ViewBase next)
        {
            return _target.ContainsRecurse(next);
        }

        /// <summary>
        /// Set the correct visual state of the target.
        /// </summary>
        /// <param name="c">Owning control.</param>
        protected void UpdateTargetState(Control c)
        {
            // Check we have a valid control to convert coordinates against
            if ((c != null) && !c.IsDisposed)
            {
                // Ensure control is inside a visible top level form
                Form f = c.FindForm();
                if ((f != null) && f.Visible)
                {
                    UpdateTargetState(c.PointToClient(Control.MousePosition));
                    return;
                }
            }

            UpdateTargetState(new Point(int.MaxValue, int.MaxValue));
        }

        /// <summary>
        /// Set the correct visual state of the target.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        protected virtual void UpdateTargetState(Point pt)
        {
            // By default the button is in the normal state
            PaletteState newState;

            // If the button is disabled then show as disabled
            if (!_target.Enabled)
                newState = PaletteState.Disabled;
            else
            {
                newState = PaletteState.Normal;

                if (_fixedPressed)
                    newState = PaletteState.Pressed;
                else
                {
                    // If capturing input....
                    if (_captured)
                    {
                        // Do we show the button as pressed only when over the button?
                        if (IsOnlyPressedWhenOver)
                        {
                            if (_target.ClientRectangle.Contains(pt))
                                newState = PaletteState.Pressed;
                            else
                            {
                                if (NonClientAsNormal)
                                    newState = PaletteState.Normal;
                                else
                                    newState = PaletteState.Tracking;
                            }
                        }
                        else
                        {
                            // Always show the button pressed, even when not over the button itself
                            newState = PaletteState.Pressed;
                        }
                    }
                    else
                    {
                        // Only hot tracking, so show tracking only if mouse over the target 
                        if (_mouseOver)
                            newState = PaletteState.Tracking;
                        else
                            newState = PaletteState.Normal;
                    }
                }
            }

            // If state has changed or change in (inside split area)
            bool inSplitRectangle = SplitRectangle.Contains(pt);
            if ((_target.ElementState != newState) || (inSplitRectangle != _inSplitRectangle))
            {
                // Update if the point is inside the split rectangle
                _inSplitRectangle = inSplitRectangle;

                // Update target to reflect new state
                _target.ElementState = newState;

                // Redraw to show the change in visual state
                OnNeedPaint(true);
            }
        }

        /// <summary>
        /// Raises the ButtonDragRectangle event.
        /// </summary>
        /// <param name="e">An ButtonDragRectangleEventArgs containing the event args.</param>
        protected virtual void OnButtonDragRectangle(ButtonDragRectangleEventArgs e)
        {
            if (ButtonDragRectangle != null)
                ButtonDragRectangle(this, e);
        }

        /// <summary>
        /// Raises the ButtonDragOffset event.
        /// </summary>
        /// <param name="e">An ButtonDragOffsetEventArgs containing the event args.</param>
        protected virtual void OnButtonDragOffset(ButtonDragOffsetEventArgs e)
        {
            if (ButtonDragOffset != null)
                ButtonDragOffset(this, e);
        }

        /// <summary>
        /// Raises the DragStart event.
        /// </summary>
        /// <param name="mousePt">Mouse point at time of event.</param>
        /// <param name="offset">Offset compared to target.</param>
        /// <param name="c">Control that is source of drag start.</param>
        protected virtual void OnDragStart(Point mousePt, Point offset, Control c)
        {
            // Convert point from client to screen coordinates
            mousePt = _target.OwningControl.PointToScreen(mousePt);
            DragStartEventCancelArgs ce = new DragStartEventCancelArgs(mousePt, offset, c);
            
            if (DragStart != null)
                DragStart(this, ce);

            // If event is not cancelled then allow dragging
            _dragging = !ce.Cancel;
        }

        /// <summary>
        /// Raises the DragMove event.
        /// </summary>
        /// <param name="mousePt">Mouse point at time of event.</param>
        protected virtual void OnDragMove(Point mousePt)
        {
            if (DragMove != null)
            {
                // Convert point from client to screen coordinates
                mousePt = _target.OwningControl.PointToScreen(mousePt);
                DragMove(this, new PointEventArgs(mousePt));
            }
        }

        /// <summary>
        /// Raises the DragEnd event.
        /// </summary>
        /// <param name="mousePt">Mouse point at time of event.</param>
        protected virtual void OnDragEnd(Point mousePt)
        {
            _dragging = false;
            if (DragEnd != null)
            {
                // Convert point from client to screen coordinates
                mousePt = _target.OwningControl.PointToScreen(mousePt);
                DragEnd(this, new PointEventArgs(mousePt));
            }
        }

        /// <summary>
        /// Raises the DragQuit event.
        /// </summary>
        protected virtual void OnDragQuit()
        {
            _dragging = false;
            if (DragQuit != null)
                DragQuit(this, EventArgs.Empty);
        }

		/// <summary>
		/// Raises the Click event.
		/// </summary>
		/// <param name="e">A MouseEventArgs containing the event data.</param>
		protected virtual void OnClick(MouseEventArgs e)
		{
			if (Click != null)
				Click(_target, e);
		}

        /// <summary>
        /// Raises the RightClick event.
        /// </summary>
        /// <param name="e">A MouseEventArgs containing the event data.</param>
        protected virtual void OnRightClick(MouseEventArgs e)
        {
            if (RightClick != null)
                RightClick(_target, e);
        }
        
        /// <summary>
		/// Raises the MouseSelect event.
		/// </summary>
		/// <param name="e">A MouseEventArgs containing the event data.</param>
		protected virtual void OnMouseSelect(MouseEventArgs e)
		{
			if (MouseSelect != null)
				MouseSelect(_target, e);
		}

		/// <summary>
		/// Raises the NeedPaint event.
		/// </summary>
		/// <param name="needLayout">Does the palette change require a layout.</param>
		protected virtual void OnNeedPaint(bool needLayout)
		{
            if (_needPaint != null)
                _needPaint(this, new NeedLayoutEventArgs(needLayout, _target.ClientRectangle));
		}
		#endregion

        #region Implementation
        private void OnRepeatTimer(object sender, EventArgs e)
        {
            // Modify subsequent repeat timing
            Timer t = (Timer)sender;
            t.Interval = Math.Max(SystemInformation.DoubleClickTime / 4, 100);
            OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }
        #endregion
    }
}
