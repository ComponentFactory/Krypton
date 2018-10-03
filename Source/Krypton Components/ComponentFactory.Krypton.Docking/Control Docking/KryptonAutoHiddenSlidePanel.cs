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
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonPanel to work as a panel for hosting the display of a sliding in/out page.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonAutoHiddenSlidePanel : KryptonPanel, 
                                               IMessageFilter
    {
        #region Static Fields
        private static readonly int SLIDE_MINIMUM = 27;
        private static readonly int SLIDE_DISTANCE = 16;
        private static readonly int SLIDE_INTERVAL = 15;
        private static readonly int CLIENT_MINIMUM = 22;
        private static readonly int DISMISS_INTERVAL = 300;
        #endregion

        #region Instance Fields
        private Control _control;
        private DockingEdge _edge;
        private KryptonAutoHiddenPanel _panel;
        private KryptonAutoHiddenGroup _group;
        private KryptonDockspaceSlide _dockspaceSlide;
        private KryptonDockspaceSeparator _separator;
        private EventHandler _checkMakeHidden; 
        private KryptonPanel _inner;
        private KryptonPage _page;
        private Button _dummyTarget;
        private DockingAutoHiddenShowState _state;
        private Rectangle _startRect;
        private Rectangle _endRect;
        private Timer _slideTimer;
        private Timer _dismissTimer;
        private bool _dismissRunning;
        private IntPtr _mouseTrackWindow;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the separator is about to be moved and requests the rectangle of allowed movement.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the separator is about to be moved and requests the rectangle of allowed movement.")]
        public event EventHandler<SplitterMoveRectMenuArgs> SplitterMoveRect;
        
        /// <summary>
        /// Occurs when the separator move finishes and a move has occured.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the separator move finishes and a move has occured.")]
        public event SplitterEventHandler SplitterMoved;

        /// <summary>
        /// Occurs when the separator has moved to a new position.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the separator has moved to a new position.")]
        public event SplitterCancelEventHandler SplitterMoving;

        /// <summary>
        /// Occurs when the user clicks the close button for a page.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the user clicks the close button for a page.")]
        public event EventHandler<UniqueNameEventArgs> PageCloseClicked;

        /// <summary>
        /// Occurs when the user clicks the auto hidden button for a page.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the user clicks the auto hidden button for a page.")]
        public event EventHandler<UniqueNameEventArgs> PageAutoHiddenClicked;
        
        /// <summary>
        /// Occurs when a page requests that a drop down menu be shown.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when the user clicks the drop down button for a page.")]
        public event EventHandler<CancelDropDownEventArgs> PageDropDownClicked;

        /// <summary>
        /// Occurs when an auto hidden page showing state changes.
        /// </summary>
        [Category("Behavior")]
        [Description("Occurs when an auto hidden page showing state changes.")]
        public event EventHandler<AutoHiddenShowingStateEventArgs> AutoHiddenShowingStateChanged;        
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonAutoHiddenSlidePanel class.
        /// </summary>
        /// <param name="control">Reference to control that is being managed.</param>
        /// <param name="edge">Docking edge being managed.</param>
        /// <param name="panel">Reference to auto hidden panel for this edge.</param>
        public KryptonAutoHiddenSlidePanel(Control control, DockingEdge edge, KryptonAutoHiddenPanel panel)
        {
            _control = control;
            _edge = edge;
            _panel = panel;
            _state = DockingAutoHiddenShowState.Hidden;
            _checkMakeHidden = new EventHandler(OnCheckMakeHidden);

            // We need to a timer to automate sliding in and out
            _slideTimer = new Timer();
            _slideTimer.Interval = SLIDE_INTERVAL;
            _slideTimer.Tick += new EventHandler(OnSlideTimerTick);

            // Timer used to delay between notification of need to slide inwards and performing actual slide
            _dismissTimer = new Timer();
            _dismissTimer.Interval = DISMISS_INTERVAL;
            _dismissTimer.Tick += new EventHandler(OnDismissTimerTick);
            _dismissRunning = false;

            // Create inner panel that holds the actual dockspace and separator
            _dockspaceSlide = new KryptonDockspaceSlide();
            _dockspaceSlide.Dock = DockStyle.Fill;
            _dockspaceSlide.AutoHiddenHost = true;
            _dockspaceSlide.PageCloseClicked += new EventHandler<UniqueNameEventArgs>(OnDockspacePageCloseClicked);
            _dockspaceSlide.PageAutoHiddenClicked += new EventHandler<UniqueNameEventArgs>(OnDockspacePageAutoHiddenClicked);
            _dockspaceSlide.PageDropDownClicked += new EventHandler<CancelDropDownEventArgs>(OnDockspacePageDropDownClicked);
            
            _separator = new KryptonDockspaceSeparator(edge, true);
            _separator.SplitterMoving += new SplitterCancelEventHandler(OnDockspaceSeparatorMoving);
            _separator.SplitterMoved += new SplitterEventHandler(OnDockspaceSeparatorMoved);
            _separator.SplitterMoveRect += new EventHandler<SplitterMoveRectMenuArgs>(OnDockspaceSeparatorMoveRect);

            _inner = new KryptonPanel();
            _inner.Controls.AddRange(new Control[] { _dockspaceSlide, _separator });
            Controls.Add(_inner);

            // Do not show ourself until we are needed
            Visible = false;

            // Add a Button that is not showing and used to push focus away from the dockspace
            _dummyTarget = new Button();
            _dummyTarget.Location = new Point(-200, -200);
            _dummyTarget.Size = new Size(100, 100);
            Controls.Add(_dummyTarget);
            
            // Add ourself into the target control for docking
            control.SizeChanged += new EventHandler(OnControlSizeChanged);
            control.Controls.Add(this);

            // Need to peek at windows messages so we can determine if mouse is over the slide out panel
            Application.AddMessageFilter(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of the timer resources
                if (_slideTimer != null)
                {
                    _slideTimer.Stop();
                    _slideTimer.Dispose();
                    _slideTimer = null;
                }

                if (_dismissTimer != null)
                {
                    _dismissTimer.Stop();
                    _dismissTimer.Dispose();
                    _dismissTimer = null;
                    _dismissRunning = false;
                }

                // Remove cached references that might prevent those objects from being garbage collected
                _page = null;
                _group = null;

                // Remove ourself from the control we planted outself into
                _control.Controls.Remove(this);

                // Remove all the pages so that the pages have palette redirection reset
                _dockspaceSlide.ClearAllPages();

                // Unhook from events/static references to allow garbage collection
                _separator.SplitterMoving -= new SplitterCancelEventHandler(OnDockspaceSeparatorMoving);
                _separator.SplitterMoved -= new SplitterEventHandler(OnDockspaceSeparatorMoved);
                _separator.SplitterMoveRect -= new EventHandler<SplitterMoveRectMenuArgs>(OnDockspaceSeparatorMoveRect);
                _dockspaceSlide.CellLosesFocus -= new EventHandler<WorkspaceCellEventArgs>(OnDockspaceCellLosesFocus);
                _dockspaceSlide.PageCloseClicked -= new EventHandler<UniqueNameEventArgs>(OnDockspacePageCloseClicked);
                _dockspaceSlide.PageAutoHiddenClicked -= new EventHandler<UniqueNameEventArgs>(OnDockspacePageAutoHiddenClicked);
                _dockspaceSlide.PageDropDownClicked -= new EventHandler<CancelDropDownEventArgs>(OnDockspacePageDropDownClicked);
                Application.RemoveMessageFilter(this);
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets access to the KryptonDockspace control.
        /// </summary>
        public KryptonDockspace DockspaceControl
        {
            get { return _dockspaceSlide; }
        }

        /// <summary>
        /// Gets access to the KryptonSeparator control.
        /// </summary>
        public KryptonDockspaceSeparator SeparatorControl
        {
            get { return _separator; }
        }

        /// <summary>
        /// Gets access to the KryptonPage associated with the slide panel.
        /// </summary>
        public KryptonPage Page
        {
            get { return _page; }
        }

        /// <summary>
        /// Gets and sets the drag page notify interface associated with the embedded dockspace.
        /// </summary>
        public IDragPageNotify DragPageNotify
        {
            get { return _dockspaceSlide.DragPageNotify; }
            set { _dockspaceSlide.DragPageNotify = value; }
        }

        /// <summary>
        /// Remove from view any slide out page.
        /// </summary>
        public void HideUniqueName()
        {
            // If we are processing any page then instantly remove it
            if (_page != null)
                MakeHidden();
        }

        /// <summary>
        /// Remove from view the slide out page if it matches the unqiue name provided.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page to be hidden.</param>
        public void HideUniqueName(string uniqueName)
        {
            // If we are processing the provided page then instantly remove it
            if ((_page != null) && (_page.UniqueName == uniqueName))
                MakeHidden();
        }

        /// <summary>
        /// Requests the panel slide into view and display the provided page.
        /// </summary>
        /// <param name="page">Reference to page for display.</param>
        /// <param name="group">Reference to auto hidden group that displays the page.</param>
        /// <param name="select">Should the sliding out page become selected.</param>
        public void SlideOut(KryptonPage page, KryptonAutoHiddenGroup group, bool select)
        {
            // Check to see if we allowed to perform operations
            if (Disposing || IsDisposed)
                return;

            // Move to the hidden state
            switch (_state)
            {
                case DockingAutoHiddenShowState.Hidden:
                    // Nothing to do, already in state we require
                    break;
                case DockingAutoHiddenShowState.SlidingIn:
                    // If already showing indicated page (although currently sliding inwards)
                    if (page == _page)
                    {
                        // Switch to sliding out again
                        _state = DockingAutoHiddenShowState.SlidingOut;

                        // Are we requested to set focus to the sliding in dockspace?
                        if (select)
                        {
                            DockspaceControl.Select();
                            DockspaceControl.CellLosesFocus += new EventHandler<WorkspaceCellEventArgs>(OnDockspaceCellLosesFocus);
                        }
                        return;
                    }
                    else
                    {
                        // Different page, so move straight to hidden state
                        MakeHidden();
                    }
                    break;
                case DockingAutoHiddenShowState.SlidingOut:
                case DockingAutoHiddenShowState.Showing:
                    // If already showing indicated page (or in the process of showing) then do nothing
                    if (page == _page)
                    {
                        // Are we requested to set focus to the sliding in dockspace?
                        if (select)
                        {
                            DockspaceControl.Select();
                            DockspaceControl.CellLosesFocus += new EventHandler<WorkspaceCellEventArgs>(OnDockspaceCellLosesFocus);
                        }
                        return;
                    }
                    else
                    {
                        // Different page, so move straight to hidden state
                        MakeHidden();
                    }
                    break;
            }

            // Cache information about the page being displayed
            _page = page;
            _group = group;

            // Make sure we have a visible cell to update
            KryptonWorkspaceCell cell = DockspaceControl.FirstVisibleCell();
            if (cell == null)
            {
                cell = new KryptonWorkspaceCell();
                DockspaceControl.Root.Children.Add(cell);
            }

            // Replace any existing page with the new one
            DockspaceControl.ClearAllPages();
            cell.Pages.Add(page);
            DockspaceControl.PerformLayout();

            // Find the starting and ending rectangles for the slide operation
            CalculateStartAndEnd();

            // Set initial positions of ourself and the contained inner panel
            _inner.SetBounds(0, 0, _endRect.Width, _endRect.Height);
            SetBounds(_startRect.X, _startRect.Y, _startRect.Width, _startRect.Height);

            // Make sure we are at the top of the z-order and visible
            _control.Controls.SetChildIndex(this, 0);
            Visible = true;

            // Switch to new state and start animation timer
            _state = DockingAutoHiddenShowState.SlidingOut;
            AutoHiddenShowingStateEventArgs args = new AutoHiddenShowingStateEventArgs(_page, _state);
            _slideTimer.Start();

            // Are we requested to set focus to the sliding in dockspace?
            if (select)
            {
                DockspaceControl.Select();
                DockspaceControl.CellLosesFocus += new EventHandler<WorkspaceCellEventArgs>(OnDockspaceCellLosesFocus);
            }

            // Raises event to indicate change in auto hidden showing state
            OnAutoHiddenShowingStateChanged(args);
        }

        /// <summary>
        /// Requests the panel slide out of view.
        /// </summary>
        public void SlideIn()
        {
            // Check to see if we allowed to perform operations
            if (Disposing || IsDisposed)
                return;

            // Action to take depends on current state
            switch (_state)
            {
                case DockingAutoHiddenShowState.Hidden:
                    // Nothing to do, we are not showing
                    break;
                case DockingAutoHiddenShowState.SlidingIn:
                    // Nothing to do, already happening
                    break;
                case DockingAutoHiddenShowState.SlidingOut:
                case DockingAutoHiddenShowState.Showing:
                    // Pause before actually sliding in as another operation may negate the request
                    _dismissTimer.Stop();
                    _dismissTimer.Start();
                    _dismissRunning = true;
                    break;
            }
        }

        /// <summary>
        /// Update the size and position of the slide out panel.
        /// </summary>
        /// <param name="width">Delta width to apply.</param>
        /// <param name="height">Delta height to apply.</param>
        public void UpdateSize(int width, int height)
        {
            // Can only apply change when fully showing
            if (_state == DockingAutoHiddenShowState.Showing)
            {
                switch (_edge)
                {
                    case DockingEdge.Left:
                        _endRect.Width += width;
                        _inner.SetBounds(0, 0, _inner.Width + width, _inner.Height);
                        SetBounds(Left, Top, Width + width, Height);

                        // Update the page with the new size to use in the future
                        if (_page != null)
                            _page.AutoHiddenSlideSize = new Size(_page.AutoHiddenSlideSize.Width + width, _page.AutoHiddenSlideSize.Height);
                        break;
                    case DockingEdge.Right:
                        _endRect.X += width;
                        _endRect.Width -= width;
                        _inner.SetBounds(0, 0, _inner.Width - width, _inner.Height);
                        SetBounds(Left + width, Top, Width - width, Height);

                        // Update the page with the new size to use in the future
                        if (_page != null)
                            _page.AutoHiddenSlideSize = new Size(_page.AutoHiddenSlideSize.Width - width, _page.AutoHiddenSlideSize.Height);
                        break;
                    case DockingEdge.Top:
                        _endRect.Height += width;
                        _inner.SetBounds(0, 0, _inner.Width, _inner.Height + height);
                        SetBounds(Left, Top, Width, Height + height);

                        // Update the page with the new size to use in the future
                        if (_page != null)
                            _page.AutoHiddenSlideSize = new Size(_page.AutoHiddenSlideSize.Width, _page.AutoHiddenSlideSize.Height + height);
                        break;
                    case DockingEdge.Bottom:
                        _endRect.Y += height;
                        _endRect.Height -= height;
                        _inner.SetBounds(0, 0, _inner.Width, _inner.Height - height);
                        SetBounds(Left, Top + height, Width, Height - height);

                        // Update the page with the new size to use in the future
                        if (_page != null)
                            _page.AutoHiddenSlideSize = new Size(_page.AutoHiddenSlideSize.Width, _page.AutoHiddenSlideSize.Height - height);
                        break;
                }
            }
        }

		/// <summary>
		/// Filters out a message before it is dispatched.
		/// </summary>
		/// <param name="msg">The message to be dispatched. You cannot modify this message. </param>
		/// <returns>true to filter out; false otherwise.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool PreFilterMessage(ref Message msg)
        {
            Form parentForm = this.FindForm();
            Form parentMdi = (parentForm != null ? parentForm.MdiParent : null);

            // Only interested in snooping messages if....
            //    The Form we are inside is the active form                             AND
            //    If an MDI Child Form then we must be the active MDI Child Form        AND
            //    We are not in the hidden state                                        AND
            //    We have an associated auto hidden group control that is not disposed  AND
            //    We are not disposed
            if ((parentForm != null) && ((parentForm == Form.ActiveForm) || (parentMdi != null && parentMdi.ActiveMdiChild == parentForm)) &&
                parentForm.ContainsFocus && (_state != DockingAutoHiddenShowState.Hidden) && (_group != null) && !_group.IsDisposed && !IsDisposed)
            {
                switch (msg.Msg)
                {
                    case PI.WM_KEYDOWN:
                        // Pressing escape removes the auto hidden window
                        if ((int)msg.WParam == PI.VK_ESCAPE)
                        {
                            MakeHidden();
                            return true;
                        }
                        break;
                    case PI.WM_MOUSELEAVE:
                        // If the mouse is leaving a control then we start the dismiss timer so that a mouse move is required
                        // to cancel the mouse move and prevent the actual dismissal occuring. The exception to this is if the
                        // slide out dockspace has the focus, in which case we do nothing.
                        if (!_dismissRunning && !DockspaceControl.ContainsFocus)
                        {
                            _dismissTimer.Stop();
                            _dismissTimer.Start();
                            _dismissRunning = true;
                        }
                        break;
                    case PI.WM_MOUSEMOVE:
                        // Convert the mouse position into a screen location
                        Point screenPt = CommonHelper.ClientMouseMessageToScreenPt(msg);

                        // Is the mouse over ourself or over the associated auto hiiden group
                        if (RectangleToScreen(ClientRectangle).Contains(screenPt) ||
                            _group.RectangleToScreen(_group.ClientRectangle).Contains(screenPt))
                        {
                            // We do not dismiss while the mouse is over ourself
                            if (_dismissRunning)
                            {
                                _dismissTimer.Stop();
                                _dismissRunning = false;
                            }
                        }
                        else
                        {
                            // When mouse not over a relevant area we need to start the dismiss process 
                            // unless the slide out dockspace has the focus, in which case we do nothing.
                            if (!_dismissRunning && !DockspaceControl.ContainsFocus)
                            {
                                _dismissTimer.Stop();
                                _dismissTimer.Start();
                                _dismissRunning = true;
                            }
                        }

                        // If first message for this window then need to track to get the mouse leave
                        if (_mouseTrackWindow != msg.HWnd)
                        {
                            _mouseTrackWindow = msg.HWnd;
                            
                            // This structure needs to know its own size in bytes
                            PI.TRACKMOUSEEVENTS tme = new PI.TRACKMOUSEEVENTS();
                            tme.cbSize = (uint)Marshal.SizeOf(typeof(PI.TRACKMOUSEEVENTS));
                            tme.dwHoverTime = 100;
                            tme.dwFlags = (int)(PI.TME_LEAVE);
                            tme.hWnd = Handle;

                            // Call Win32 API to start tracking
                            PI.TrackMouseEvent(ref tme);
                        }
                        break;
                }
            }

            return false;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Leave event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected override void OnLeave(EventArgs e)
        {
            // We need to hide the window after the current windows message has been processed and
            // during its operation. Doing so can cause problems with the mouse down not working when
            // that is the cause of the leave event. Plus the focus might enter here again before the
            // next windows message.
            if (IsHandleCreated)
                BeginInvoke(_checkMakeHidden);

            base.OnLeave(e);
        }
        #endregion

        #region Implementation
        private void MakeHidden()
        {
            // Check to see if we allowed to perform operations
            if (!Disposing && !IsDisposed)
            {
                if (_state != DockingAutoHiddenShowState.Hidden)
                {
                    // Set state so timer processing does not perform any slide action
                    _state = DockingAutoHiddenShowState.Hidden;
                    AutoHiddenShowingStateEventArgs args = new AutoHiddenShowingStateEventArgs(_page, _state);

                    // Remove cached references
                    _page = null;
                    _group = null;

                    // No need for timers to be running or for our display
                    _slideTimer.Stop();
                    _dismissTimer.Stop();
                    _dismissRunning = false;
                    Visible = false;

                    // Move to correct z-order position
                    ResetChildIndex();

                    // If the dockspace has the focus we need to push focus elsewhere
                    if (DockspaceControl.ContainsFocus)
                    {
                        DockspaceControl.CellLosesFocus -= new EventHandler<WorkspaceCellEventArgs>(OnDockspaceCellLosesFocus);
                        _dummyTarget.Select();
                    }

                    // Remove all the pages so that the pages have palette redirection reset
                    DockspaceControl.ClearAllPages();

                    // Raises event to indicate change in auto hidden showing state
                    OnAutoHiddenShowingStateChanged(args);
                }
            }
        }

        private void MakeSlideIn()
        {
            // Check to see if we allowed to perform operations
            if (!Disposing && !IsDisposed)
            {
                // Switch to sliding inwards by changing state and starting slide timer
                _state = DockingAutoHiddenShowState.SlidingIn;
                AutoHiddenShowingStateEventArgs args = new AutoHiddenShowingStateEventArgs(_page, _state);
                _slideTimer.Start();

                // If the dockspace has the focus we need to push focus elsewhere
                if (DockspaceControl.ContainsFocus)
                {
                    DockspaceControl.CellLosesFocus -= new EventHandler<WorkspaceCellEventArgs>(OnDockspaceCellLosesFocus);
                    _dummyTarget.Select();
                }

                // Raises event to indicate change in auto hidden showing state
                OnAutoHiddenShowingStateChanged(args);
            }
        }

        private void CalculateStartAndEnd()
        {
            // Find the preferred size of the slider area by combining the separator and dockspace
            Size dockspacePreferred = _page.AutoHiddenSlideSize;
            Size separatorPreferred = _separator.GetPreferredSize(_control.Size);
            Size slideSize = new Size(separatorPreferred.Width + dockspacePreferred.Width, 
                                      separatorPreferred.Height + dockspacePreferred.Height);

            // Find the maximum allowed size based on the owning control client area reduced by a sensible minimum
            Size innerSize = _control.ClientRectangle.Size;
            innerSize.Width -= CLIENT_MINIMUM;
            innerSize.Height -= CLIENT_MINIMUM;

            // Adjust for any showing auto hidden panels at the edges
            foreach (Control child in _control.Controls)
            {
                if (child.Visible && child is KryptonAutoHiddenPanel)
                {
                    switch (child.Dock)
                    {
                        case DockStyle.Left:
                        case DockStyle.Right:
                            innerSize.Width -= child.Width;
                            break;
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            innerSize.Height -= child.Height;
                            break;
                    }
                }
            }

            // Enforce a sensible minimum and upper maximum equal to size of the control area
            slideSize.Width = Math.Min(Math.Max(SLIDE_MINIMUM, slideSize.Width), innerSize.Width);
            slideSize.Height = Math.Min(Math.Max(SLIDE_MINIMUM, slideSize.Height), innerSize.Height);

            // Actual start/end rectangles are calculated based on the docking edge
            switch (_edge)
            {
                case DockingEdge.Left:
                    _startRect = new Rectangle(_panel.Width, _panel.Top, 0, _panel.Height);
                    _endRect = new Rectangle(_panel.Width, _panel.Height, slideSize.Width, _panel.Height);
                    break;
                case DockingEdge.Right:
                    _startRect = new Rectangle(_panel.Left, _panel.Top, 0, _panel.Height);
                    _endRect = new Rectangle(_panel.Left - slideSize.Width, _panel.Height, slideSize.Width, _panel.Height);
                    break;
                case DockingEdge.Top:
                    _startRect = new Rectangle(_panel.Left, _panel.Height, _panel.Width, 0);
                    _endRect = new Rectangle(_panel.Left, _panel.Height, _panel.Width, slideSize.Height);
                    break;
                case DockingEdge.Bottom:
                    _startRect = new Rectangle(_panel.Left, _panel.Top, _panel.Width, 0);
                    _endRect = new Rectangle(_panel.Left, _panel.Top - slideSize.Height, _panel.Width, slideSize.Height);
                    break;
            }
        }

        private void ResetChildIndex()
        {
            // Find the child index of our matching panel
            int panelIndex = _control.Controls.IndexOf(_panel);

            // Position ourself just before the matching panel
            _control.Controls.SetChildIndex(this, panelIndex);
            _control.Controls.SetChildIndex(this, panelIndex);
        }

        private void OnCheckMakeHidden(object sender, EventArgs e)
        {
            // Do we still need to make ourself hidden?
            if (!ContainsFocus)
                MakeHidden();
        }

        private void OnSlideTimerTick(object sender, EventArgs e)
        {
            // Check to see if we allowed to perform operations
            if (Disposing || IsDisposed)
            {
                // Make sure the timer is disposed of correctly
                if (_slideTimer != null)
                {
                    _slideTimer.Stop();
                    _slideTimer.Dispose();
                    _slideTimer = null;
                }

                return;
            }

            // Action to take depends on current state
            switch (_state)
            {
                case DockingAutoHiddenShowState.Hidden:
                case DockingAutoHiddenShowState.Showing:
                    // No need for timer as sliding has finished
                    _slideTimer.Stop();
                    break;
                case DockingAutoHiddenShowState.SlidingOut:
                    {
                        bool finished = true;
                        Size newSlideSize = Size;
                        Point newSlideLocation = Location;
                        Point newInnerLocation = _inner.Location;

                        // Find the new size and location when sliding out from the edge
                        switch (_edge)
                        {
                            case DockingEdge.Left:
                                newSlideSize.Width = Math.Min(newSlideSize.Width + SLIDE_DISTANCE, _endRect.Width);
                                newInnerLocation.X = newSlideSize.Width - _inner.Width;
                                finished = (newSlideSize.Width == _endRect.Width);
                                break;
                            case DockingEdge.Right:
                                newSlideSize.Width = Math.Min(newSlideSize.Width + SLIDE_DISTANCE, _endRect.Width);
                                newSlideLocation.X = Math.Max(newSlideLocation.X - SLIDE_DISTANCE, _endRect.X);
                                finished = (newSlideSize.Width == _endRect.Width);
                                break;
                            case DockingEdge.Top:
                                newSlideSize.Height = Math.Min(newSlideSize.Height + SLIDE_DISTANCE, _endRect.Height);
                                newInnerLocation.Y = newSlideSize.Height - _inner.Height;
                                finished = (newSlideSize.Height == _endRect.Height);
                                break;
                            case DockingEdge.Bottom:
                                newSlideSize.Height = Math.Min(newSlideSize.Height + SLIDE_DISTANCE, _endRect.Height);
                                newSlideLocation.Y = Math.Max(newSlideLocation.Y - SLIDE_DISTANCE, _endRect.Y);
                                finished = (newSlideSize.Height == _endRect.Height);
                                break;
                        }

                        // Update position to reflect the change
                        _inner.SetBounds(newInnerLocation.X, newInnerLocation.Y, _endRect.Width, _endRect.Height);
                        SetBounds(newSlideLocation.X, newSlideLocation.Y, newSlideSize.Width, newSlideSize.Height);

                        if (finished)
                        {
                            // When finished we no longer need the timer and enter the showing state
                            _state = DockingAutoHiddenShowState.Showing;
                            AutoHiddenShowingStateEventArgs args = new AutoHiddenShowingStateEventArgs(_page, _state);
                            OnAutoHiddenShowingStateChanged(args);
                            _slideTimer.Stop();
                        }
                    }
                    break;
                case DockingAutoHiddenShowState.SlidingIn:
                    {
                        bool finished = true;
                        Size newSlideSize = Size;
                        Point newSlideLocation = Location;
                        Point newInnerLocation = _inner.Location;

                        // Find the new size and location when sliding inwards to the edge
                        switch (_edge)
                        {
                            case DockingEdge.Left:
                                newSlideSize.Width = Math.Max(newSlideSize.Width - SLIDE_DISTANCE, 0);
                                newInnerLocation.X = newSlideSize.Width - _inner.Width;
                                finished = (newSlideSize.Width == _startRect.Width);
                                break;
                            case DockingEdge.Right:
                                newSlideSize.Width = Math.Max(newSlideSize.Width - SLIDE_DISTANCE, 0);
                                newSlideLocation.X = Math.Min(newSlideLocation.X + SLIDE_DISTANCE, _startRect.X);
                                finished = (newSlideSize.Width == _startRect.Width);
                                break;
                            case DockingEdge.Top:
                                newSlideSize.Height = Math.Max(newSlideSize.Height - SLIDE_DISTANCE, 0);
                                newInnerLocation.Y = newSlideSize.Height - _inner.Height;
                                finished = (newSlideSize.Height == _startRect.Height);
                                break;
                            case DockingEdge.Bottom:
                                newSlideSize.Height = Math.Max(newSlideSize.Height - SLIDE_DISTANCE, 0);
                                newSlideLocation.Y = Math.Min(newSlideLocation.Y + SLIDE_DISTANCE, _startRect.Y);
                                finished = (newSlideSize.Height == _startRect.Height);
                                break;
                        }

                        // Update position to reflect the change
                        _inner.SetBounds(newInnerLocation.X, newInnerLocation.Y, _endRect.Width, _endRect.Height);
                        SetBounds(newSlideLocation.X, newSlideLocation.Y, newSlideSize.Width, newSlideSize.Height);

                        if (finished)
                            MakeHidden();
                    }
                    break;
            }
        }

        private void OnDismissTimerTick(object sender, EventArgs e)
        {
            // Check to see if we allowed to perform operations
            if (Disposing || IsDisposed)
            {
                // Make sure the timer is disposed of correctly
                if (_dismissTimer != null)
                {
                    _dismissTimer.Stop();
                    _dismissTimer.Dispose();
                    _dismissTimer = null;
                    _dismissRunning = false;
                }

                return;
            }

            // Always stop the timer, we only need to be notified once
            _dismissTimer.Stop();

            // Only process if the timer is expected to be running
            if (_dismissRunning)
            {
                // Always stop the timer, we only need to be notified once
                _dismissRunning = false;

                // Action to take depends on current state
                switch (_state)
                {
                    case DockingAutoHiddenShowState.Hidden:
                    case DockingAutoHiddenShowState.SlidingIn:
                        // No sliding required, nothing to do
                        break;
                    case DockingAutoHiddenShowState.SlidingOut:
                    case DockingAutoHiddenShowState.Showing:
                        MakeSlideIn();
                        break;
                }
            }
        }

        private void OnDockspaceCellLosesFocus(object sender, WorkspaceCellEventArgs e)
        {
            // Check to see if we allowed to perform operations
            if (Disposing || IsDisposed)
                return;

            // No longer need the lose focus because we have been notified
            DockspaceControl.CellLosesFocus -= new EventHandler<WorkspaceCellEventArgs>(OnDockspaceCellLosesFocus);

            // Action depends on the current state
            switch (_state)
            {
                case DockingAutoHiddenShowState.Hidden:
                case DockingAutoHiddenShowState.SlidingIn:
                    // No sliding required, nothing to do
                    break;
                case DockingAutoHiddenShowState.SlidingOut:
                case DockingAutoHiddenShowState.Showing:
                    MakeSlideIn();
                    break;
            }
        }

        private void OnControlSizeChanged(object sender, EventArgs e)
        {
            // Change in parent control size means we always hide
            MakeHidden();
        }

        private void OnDockspaceSeparatorMoving(object sender, SplitterCancelEventArgs e)
        {
            if (SplitterMoving != null)
                SplitterMoving(sender, e);
        }

        private void OnDockspaceSeparatorMoved(object sender, SplitterEventArgs e)
        {
            if (SplitterMoved != null)
                SplitterMoved(sender, e);
        }

        private void OnDockspaceSeparatorMoveRect(object sender, SplitterMoveRectMenuArgs e)
        {
            if (!_dockspaceSlide.ContainsFocus)
            {
                _dockspaceSlide.Select();
                Application.DoEvents();
            }

            if (SplitterMoveRect != null)
                SplitterMoveRect(sender, e);
        }

        private void OnDockspacePageCloseClicked(object sender, UniqueNameEventArgs e)
        {
            if (PageCloseClicked != null)
                PageCloseClicked(sender, e);
        }

        private void OnDockspacePageAutoHiddenClicked(object sender, UniqueNameEventArgs e)
        {
            if (PageAutoHiddenClicked != null)
                PageAutoHiddenClicked(sender, e);
        }

        private void OnDockspacePageDropDownClicked(object sender, CancelDropDownEventArgs e)
        {
            // Click the drop down button should cause the slide out to be focused so that it
            // does not slide back again if you move the mouse away from the slide out area
            if (_dockspaceSlide != null)
                _dockspaceSlide.Select();

            if (PageDropDownClicked != null)
                PageDropDownClicked(sender, e);
        }

        private void OnAutoHiddenShowingStateChanged(AutoHiddenShowingStateEventArgs e)
        {
            if (AutoHiddenShowingStateChanged != null)
                AutoHiddenShowingStateChanged(this, e);
        }
        #endregion
    }
}
