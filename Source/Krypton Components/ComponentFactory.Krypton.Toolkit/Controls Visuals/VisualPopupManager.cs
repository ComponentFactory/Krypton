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
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Manage the filtering of message for popup controls.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class VisualPopupManager : IMessageFilter
    {
        #region Type Declarations
        private class PopupStack : Stack<VisualPopup> {};
        #endregion

        #region Static Fields
        [ThreadStatic]
        private static VisualPopupManager _singleton;
        #endregion

        #region Instance Fields
        private PopupStack _stack;
        private VisualPopup _current;
        private IntPtr _activeWindow;
        private bool _filtering;
        private int _suspended;
        private bool _showingCMS;
        private ContextMenuStrip _cms;
        private EventHandler _cmsFinishDelegate;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the VisualPopupManager class.
        /// </summary>
        private VisualPopupManager()
        {
            _stack = new PopupStack();
        }
        #endregion

        #region Singleton
        /// <summary>
        /// Gets access to the single instance of the VisualPopupManager class.
        /// </summary>
        public static VisualPopupManager Singleton
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                if (_singleton == null)
                    _singleton = new VisualPopupManager();

                return _singleton;
            }
        }
        #endregion

        #region IsShowingCMS
        /// <summary>
        /// Gets a value indicating if currently showing a context menu strip.
        /// </summary>
        public bool IsShowingCMS
        {
            get { return _showingCMS; }
        }
        #endregion
        
        #region IsTracking
        /// <summary>
        /// Gets a value indicating if currently tracking a popup.
        /// </summary>
        public bool IsTracking
        {
            get { return (_current != null); }
        }
        #endregion

        #region CurrentPopup
        /// <summary>
        /// Gets the current visual popup being tracked.
        /// </summary>
        public VisualPopup CurrentPopup
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _current; }
        }
        #endregion

        #region StackedPopups
        /// <summary>
        /// Gets the stacked set of popups as an array.
        /// </summary>
        public VisualPopup[] StackedPopups
        {
            get { return _stack.ToArray(); }
        }
        #endregion

        #region TrackingByType
        /// <summary>
        /// Gets the popup that matches the proveded type.
        /// </summary>
        /// <param name="t">Type to find.</param>
        /// <returns>Matching instance; otherwise null.</returns>
        public Control TrackingByType(Type t)
        {
            if (IsTracking)
            {
                // Is the current popup matching the type?
                if (CurrentPopup.GetType() == t)
                    return CurrentPopup;

                // Check the stack items in reverse order for a match
                VisualPopup[] popups = _stack.ToArray();
                for (int i = popups.Length - 1; i >= 0; i--)
                    if (!popups[i].IsDisposed)
                        if (popups[i].GetType() == t)
                            return popups[i];
            }

            return null;
        }
        #endregion

        #region StartTracking
        /// <summary>
        /// Start tracking the provided popup.
        /// </summary>
        /// <param name="popup">Popup instance to track.</param>
        public void StartTracking(VisualPopup popup)
        {
            Debug.Assert(popup != null);
            Debug.Assert(!popup.IsDisposed);
            Debug.Assert(popup.IsHandleCreated);
            Debug.Assert(_suspended == 0);

            // The popup control must be valid
            if ((popup != null) && !popup.IsDisposed && popup.IsHandleCreated)
            {
                // Cannot start tracking when a popup menu is alive
                if (_suspended == 0)
                {
                    // If we already have a popup...
                    if (_current != null)
                    {
                        // Then stack it
                        _stack.Push(_current);
                    }
                    else
                    {
                        // Cache the currently active window
                        _activeWindow = PI.GetActiveWindow();

                        // For the first popup, we hook into message processing
                        FilterMessages(true);
                    }

                    // Store reference
                    _current = popup;
                }
            }
        }
        #endregion

        #region EndAllTracking
        /// <summary>
        /// Finish tracking all popups.
        /// </summary>
        public void EndAllTracking()
        {
            // Are we tracking a popup?
            if (_current != null)
            {
                // Kill the popup window
                if (!_current.IsDisposed)
                {
                    _current.Dispose();
                    _current = null;
                }

                // Is there anything stacked?
                while (_stack.Count > 0)
                {
                    // Pop back the popup
                    _current = _stack.Pop();

                    // Kill the popup
                    _current.Dispose();
                    _current = null;
                }

                // No longer need to filter
                FilterMessages(false);
            }
        }
        #endregion

        #region EndPopupTracking
        /// <summary>
        /// Finish tracking from the current back to and including the provided popup.
        /// </summary>
        /// <param name="popup">Popup that needs to be killed.</param>
        public void EndPopupTracking(VisualPopup popup)
        {
            // Are we tracking a popup?
            if (_current != null)
            {
                bool found = false;

                do
                {
                    // Is this the target?
                    found = (_current == popup);

                    // If possible then kill the current popup
                    if (!_current.IsDisposed)
                        _current.Dispose();

                    _current = null;

                    // If anything on stack, then it becomes the current one
                    if (_stack.Count > 0)
                        _current = _stack.Pop();
                }
                while (!found && (_current != null));

                // If we removed all the popups
                if (_current == null)
                {
                    // No longer need to filter
                    FilterMessages(false);
                }
            }
        }
        #endregion

        #region EndCurrentTracking
        /// <summary>
        /// Finish tracking the current popup.
        /// </summary>
        public void EndCurrentTracking()
        {
            // Are we tracking a popup?
            if (_current != null)
            {
                // Kill the popup window
                if (!_current.IsDisposed)
                    _current.Dispose();

                // Is there anything stacked?
                if (_stack.Count > 0)
                {
                    // Pop back and now track
                    _current = _stack.Pop();
                }
                else
                {
                    // No longer tracking any popup
                    _current = null;

                    // Last popup removed, so unhook from message processing
                    FilterMessages(false);
                }
            }
        }
        #endregion

        #region ShowContextMenuStrip
        /// <summary>
        /// Show the provided context strip in a way compatible with any popups.
        /// </summary>
        /// <param name="cms">Reference to ContextMenuStrip.</param>
        /// <param name="screenPt">Screen position for showing the context menu strip.</param>
        public void ShowContextMenuStrip(ContextMenuStrip cms,
                                         Point screenPt)
        {
            ShowContextMenuStrip(cms, screenPt, null);
        }

        /// <summary>
        /// Show the provided context strip in a way compatible with any popups.
        /// </summary>
        /// <param name="cms">Reference to ContextMenuStrip.</param>
        /// <param name="screenPt">Screen position for showing the context menu strip.</param>
        /// <param name="cmsFinishDelegate">Delegate to call when strip dismissed.</param>
        public void ShowContextMenuStrip(ContextMenuStrip cms, 
                                         Point screenPt,
                                         EventHandler cmsFinishDelegate)
        {
            Debug.Assert(cms != null);

            if (cms != null)
            {
                // Need to know when the context strip is removed
                cms.Closed += new ToolStripDropDownClosedEventHandler(OnCMSClosed);

                // Remember delegate to fire when context menu is dismissed
                _cmsFinishDelegate = cmsFinishDelegate;

                // Suspend processing of messages until context strip removed
                _suspended++;

                // Need to filter to prevent non-client mouse move from occuring
                FilterMessages(true);

                // We are showing a context menu strip
                _showingCMS = true;

                // Remember the strip reference for use in message processing
                _cms = cms;

                cms.Show(screenPt);
            }
        }
        #endregion

        #region PreFilterMessage
        /// <summary>
        /// Filters out a message before it is dispatched.
        /// </summary>
        /// <param name="m">The message to be dispatched. You cannot modify this message.</param>
        /// <returns>true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool PreFilterMessage(ref Message m)
        {
            // If we have suspended operation....
            if (_suspended > 0)
            {
                // Intercept the non-client mouse move to prevent the custom
                // chrome of the form from providing hot tracking feedback
                if (m.Msg == PI.WM_NCMOUSEMOVE)
                    return true;

                // A mouse move can occur because a context menu is showing with a popup also 
                // already showing. We suppress the mouse move to prevent tracking of the popup
                if (m.Msg == PI.WM_MOUSEMOVE)
                    return ProcessMouseMoveWithCMS(ref m);

                return false;
            }

            if (_current != null)
            {
                // If the popup has been become disposed
                if (_current.IsDisposed)
                {
                    EndCurrentTracking();
                    return false;
                }
                else
                {
                    // Get the active window
                    IntPtr activeWindow = PI.GetActiveWindow();

                    // Is there a change in active window?
                    if (activeWindow != _activeWindow)
                    {
                        // If the current window has become active, ask popup if that is allowed
                        if ((activeWindow == _current.Handle) && _current.AllowBecomeActiveWhenCurrent)
                            _activeWindow = _current.Handle;
                        else
                        {
                            bool focus = _current.ContainsFocus;

                            if (!focus)
                            {
                                VisualPopup[] popups = _stack.ToArray();

                                // For from last to first for any popup that has the focus
                                for (int i = popups.Length - 1; i >= 0; i--)
                                    if (!popups[i].IsDisposed)
                                        if (popups[i].ContainsFocus)
                                        {
                                            focus = true;
                                            break;
                                        }
                            }

                            // If the change in active window (focus) is not to the current
                            // or a stacked popup then we need to pull down the entire stack
                            // as focus has been shifted away from the use of any popup.
                            if (!focus)
                            {
                                EndAllTracking();
                                return false;
                            }
                        }
                    }
                }

                // We only intercept and handle keyboard and mouse messages
                if (!IsKeyOrMouseMessage(ref m))
                    return false;

                switch (m.Msg)
                {
                    case PI.WM_KEYDOWN:
                    case PI.WM_SYSKEYDOWN:
                        // If the popup is telling us to redirect keyboard to itself
                        if (!_current.KeyboardInert)
                        {
                            // If the focus is not inside the actual current tracking popup
                            // then we need to manually translate the message to ensure that
                            // KeyPress events occur for the current popup.
                            if (!_current.ContainsFocus)
                            {
                                PI.MSG msg = new PI.MSG();
                                msg.hwnd = m.HWnd;
                                msg.message = m.Msg;
                                msg.lParam = m.LParam;
                                msg.wParam = m.WParam;
                                PI.TranslateMessage(ref msg);
                            }
                            return ProcessKeyboard(ref m);
                        }
                        break;
                    case PI.WM_CHAR:
                    case PI.WM_KEYUP:
                    case PI.WM_DEADCHAR:
                    case PI.WM_SYSCHAR:
                    case PI.WM_SYSKEYUP:
                    case PI.WM_SYSDEADCHAR:
                        // If the popup is telling us to redirect keyboard to itself
                        if (!_current.KeyboardInert)
                            return ProcessKeyboard(ref m);
                        break;
                    case PI.WM_MOUSEMOVE:
                    case PI.WM_NCMOUSEMOVE:
                        return ProcessMouseMove(ref m);
                    case PI.WM_LBUTTONDOWN:
                    case PI.WM_RBUTTONDOWN:
                    case PI.WM_MBUTTONDOWN:
                        return ProcessClientMouseDown(ref m);
                    case PI.WM_NCLBUTTONDOWN:
                    case PI.WM_NCRBUTTONDOWN:
                    case PI.WM_NCMBUTTONDOWN:
                        return ProcessNonClientMouseDown(ref m);
                }
            }

            return false;
        }
        #endregion

        #region Implementation
        private bool ProcessKeyboard(ref Message m)
        {
            // If focus is not inside the current popup...
            if (!_current.ContainsFocus)
            {
                // ...then redirect the message to the popup so it can process all
                // keyboard input. We just send the message on by altering the handle
                // to the current popup and then suppress processing of current message.
                PI.SendMessage(_current.Handle, m.Msg, m.WParam, m.LParam);
                return true;
            }
            else
            {
                // Focus is inside the current popup, so let message be sent there
                return false;
            }
        }

        private bool ProcessClientMouseDown(ref Message m)
        {
            bool processed = false;
            
            // Convert the client position to screen point
            Point screenPt = CommonHelper.ClientMouseMessageToScreenPt(m);

            // Is this message for the current popup?
            if (m.HWnd == _current.Handle)
            {
                // Message is intended for the current popup which means we ask the popup if it
                // would like to kill the entire stack because it knows the mouse down should
                // cancel the showing of popups.
                if (_current.DoesCurrentMouseDownEndAllTracking(m, ScreenPtToClientPt(screenPt)))
                    EndAllTracking();
            }
            else
            {
                // If the current popup is not the intended recipient but the current popup knows
                // that the mouse down is safe because it is within the client area of itself, then
                // just let the message carry on as normal.
                if (_current.DoesCurrentMouseDownContinueTracking(m, ScreenPtToClientPt(screenPt)))
                    return processed;
                else
                {
                    // Mouse is not inside the client area of the current popup, so we are going to end all tracking
                    // unless we can find a popup that wants to become the current popup because the mouse happens to
                    // be other it, and it wants it.
                    VisualPopup[] popups = _stack.ToArray();

                    // Search from end towards the front, the last entry is the most recent 'Push'
                    for (int i = 0; i < popups.Length; i++)
                    {
                        // Ignore disposed popups
                        VisualPopup popup = popups[i];
                        if (!popup.IsDisposed)
                        {
                            // If the mouse down is inside the popup instance
                            if (popup.RectangleToScreen(popup.ClientRectangle).Contains(screenPt))
                            {
                                // Does this stacked popup want to become the current one?
                                if (popup.DoesStackedClientMouseDownBecomeCurrent(m, ScreenPtToClientPt(screenPt, popup.Handle)))
                                {
                                    // Kill the popups until back at the requested popup
                                    while ((_current != null) && (_current != popup))
                                    {
                                        _current.Dispose();
                                        if (_stack.Count > 0)
                                            _current = _stack.Pop();
                                    }
                                }

                                return processed;
                            }
                        }
                    }

                    // Do any of the current popups want the mouse down to be eaten?
                    if (_current != null)
                    {
                        processed = _current.DoesMouseDownGetEaten(m, screenPt);
                        if (!processed)
                        {
                            // Search from end towards the front, the last entry is the most recent 'Push'
                            for (int i = 0; i < popups.Length; i++)
                            {
                                // Ignore disposed popups
                                VisualPopup popup = popups[i];
                                if (!popup.IsDisposed)
                                {
                                    processed = popup.DoesMouseDownGetEaten(m, screenPt);
                                    if (processed)
                                        break;
                                }
                            }
                        }
                    }

                    // Mouse down not intercepted by any popup, so end tracking
                    EndAllTracking();
                }
            }

            return processed;
        }

        private bool ProcessNonClientMouseDown(ref Message m)
        {
            // Extract the x and y mouse position from message
            Point screenPt = new Point(PI.LOWORD((int)m.LParam), PI.HIWORD((int)m.LParam));

            // Ask the popup if this message causes the entire stack to be killed
            if (_current.DoesCurrentMouseDownEndAllTracking(m, ScreenPtToClientPt(screenPt)))
                EndAllTracking();

            // Do any of the current popups want the mouse down to be eaten?
            bool processed = false;
            if (_current != null)
            {
                processed = _current.DoesMouseDownGetEaten(m, screenPt);
                if (!processed)
                {
                    // Search from end towards the front, the last entry is the most recent 'Push'
                    VisualPopup[] popups = _stack.ToArray();
                    for (int i = 0; i < popups.Length; i++)
                    {
                        // Ignore disposed popups
                        VisualPopup popup = popups[i];
                        if (!popup.IsDisposed)
                        {
                            processed = popup.DoesMouseDownGetEaten(m, screenPt);
                            if (processed)
                                break;
                        }
                    }
                }
            }

            return processed;
        }

        private bool ProcessMouseMove(ref Message m)
        {
            // Is this message for a different window?
            if (m.HWnd != _current.Handle)
            {
                // Convert the client position to screen point
                Point screenPt = CommonHelper.ClientMouseMessageToScreenPt(m);

                // Ask the current popup if it allows the mouse move
                if (_current.AllowMouseMove(m, screenPt))
                    return false;

                // Ask each stacked entry if they allow the mouse move
                VisualPopup[] popups = _stack.ToArray();

                // Search from end towards the front, the last entry is the most recent 'Push'
                for (int i = popups.Length - 1; i >= 0; i--)
                    if (!popups[i].IsDisposed)
                        if (popups[i].AllowMouseMove(m, screenPt))
                            return false;

                // No popup allows the mouse move, so suppress it
                return true;
            }
            else
                return false;
        }

        private bool ProcessMouseMoveWithCMS(ref Message m)
        {
            if (_current == null)
                return false;

            // Convert the client position to screen point
            Point screenPt = CommonHelper.ClientMouseMessageToScreenPt(m);

            // Convert from a class to a structure
            PI.POINT screenPIPt = new PI.POINT();
            screenPIPt.x = screenPt.X;
            screenPIPt.y = screenPt.Y;

            // Get the window handle of the window under this screen point
            IntPtr hWnd = PI.WindowFromPoint(screenPIPt);

            // Is the window handle that of the currently tracking popup
            if (_current.Handle == hWnd)
                return true;

            // Search all the stacked popups for any that match the window handle
            VisualPopup[] popups = _stack.ToArray();
            for (int i = 0; i<popups.Length; i++)
                if (!popups[i].IsDisposed)
                    if (popups[i].Handle == hWnd)
                        return true;

            // Mouse move is not over a popup, so allow it
            return false;
        }

        private Point ScreenPtToClientPt(Point pt)
        {
            return ScreenPtToClientPt(pt, _current.Handle);
        }

        private Point ScreenPtToClientPt(Point pt, IntPtr handle)
        {
            PI.POINTC clientPt = new PI.POINTC();
            clientPt.x = pt.X;
            clientPt.y = pt.Y;

            // Negative positions are in the range 32767 -> 65535, 
            // so convert to actual int values for the negative positions
            if (clientPt.x >= 32767) clientPt.x = (clientPt.x - 65536);
            if (clientPt.y >= 32767) clientPt.y = (clientPt.y - 65536);

            // Convert a 0,0 point from client to screen to find offsetting
            PI.POINTC zeroPIPt = new PI.POINTC();
            zeroPIPt.x = 0;
            zeroPIPt.y = 0;
            PI.MapWindowPoints(IntPtr.Zero, handle, zeroPIPt, 1);

            // Adjust the client coordinate by the offset to get to screen
            clientPt.x += zeroPIPt.x;
            clientPt.y += zeroPIPt.y;

            // Return as a managed point type
            return new Point(clientPt.x, clientPt.y);
        }

        private bool IsKeyOrMouseMessage(ref Message m)
        {
            if ((m.Msg >= PI.WM_MOUSEMOVE) && (m.Msg <= PI.WM_MOUSEWHEEL))
                return true;
            if ((m.Msg >= PI.WM_NCMOUSEMOVE) && (m.Msg <= PI.WM_NCMBUTTONDBLCLK))
                return true;
            if ((m.Msg >= PI.WM_KEYDOWN) && (m.Msg <= PI.WM_KEYLAST))
                return true;

            return false;
        }

        private void FilterMessages(bool filter)
        {
            if (filter != _filtering)
            {
                if (filter)
                {
                    Application.AddMessageFilter(this);
                    _filtering = true;
                }
                else
                {
                    Application.RemoveMessageFilter(this);
                    _filtering = false;
                }
            }
        }

        private void OnCMSClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // Unhook event from object
            ContextMenuStrip cms = sender as ContextMenuStrip;
            cms.Closed -= new ToolStripDropDownClosedEventHandler(OnCMSClosed);

            // Revoke the suspended state
            _suspended--;

            // If we are filtering messages but no longer need to filter
            if (_filtering && (_current == null))
            {
                Application.RemoveMessageFilter(this);
                _filtering = false;
            }

            // No longer showing a context menu strip
            _cms = null;
            _showingCMS = false;

            // Do we fire a delegate to notify end of the strip?
            if (_cmsFinishDelegate != null)
            {
                _cmsFinishDelegate(this, e);
                _cmsFinishDelegate = null;
            }
        }
        #endregion
    }
}
