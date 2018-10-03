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
using System.Security;
using System.Security.Permissions;
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
	/// Process the mouse/keyboard/focus for the month calendar.
	/// </summary>
    public class MonthCalendarController : GlobalId,
                                           IMouseController,
                                           IKeyController,
                                           ISourceController,
                                           IContextMenuTarget
    {
        #region Instance Fields
        private KryptonContextMenuMonthCalendar _monthCalendar;
        private ViewContextMenuManager _viewManager;
        private ViewLayoutMonths _months;
        private NeedPaintHandler _needPaint;
        private DateTime _selectionStart;
        private bool _mouseOver;
        private bool _captured;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the MonthCalendarController class.
		/// </summary>
        /// <param name="monthCalendar">Reference to owning month calendar entry.</param>
        /// <param name="viewManager">Owning view manager instance.</param>
        /// <param name="months">Reference to view that manages the display of months.</param>
        /// <param name="needPaint">Delegate for handling repainting requests.</param>
        public MonthCalendarController(KryptonContextMenuMonthCalendar monthCalendar,
                                       ViewContextMenuManager viewManager,
                                       ViewLayoutMonths months, 
                                       NeedPaintHandler needPaint)
		{
            _monthCalendar = monthCalendar;
            _viewManager = viewManager;
            _months = months;
            _needPaint = needPaint;
            _mouseOver = false;
            _captured = false;
		}
        #endregion

        #region ContextMenuTarget Notifications
        /// <summary>
        /// Returns if the item shows a sub menu when selected.
        /// </summary>
        public virtual bool HasSubMenu
        {
            get { return false; }
        }

        /// <summary>
        /// This target should display as the active target.
        /// </summary>
        public virtual void ShowTarget()
        {
            _months.FocusDay = _monthCalendar.SelectionStart;
            _needPaint(this, new NeedLayoutEventArgs(false));
        }

        /// <summary>
        /// This target should clear any active display.
        /// </summary>
        public virtual void ClearTarget()
        {
            _months.FocusDay = null;
            _needPaint(this, new NeedLayoutEventArgs(false));
        }

        /// <summary>
        /// This target should show any appropriate sub menu.
        /// </summary>
        public void ShowSubMenu()
        {
        }

        /// <summary>
        /// This target should remove any showing sub menu.
        /// </summary>
        public void ClearSubMenu()
        {
        }

        /// <summary>
        /// Determine if the keys value matches the mnemonic setting for this target.
        /// </summary>
        /// <param name="charCode">Key code to test against.</param>
        /// <returns>True if a match is found; otherwise false.</returns>
        public bool MatchMnemonic(char charCode)
        {
            return false;
        }

        /// <summary>
        /// Activate the item because of a mnemonic key press.
        /// </summary>
        public void MnemonicActivate()
        {
        }

        /// <summary>
        /// Gets the view element that should be used when this target is active.
        /// </summary>
        /// <returns>View element to become active.</returns>
        public ViewBase GetActiveView()
        {
            return _months;
        }

        /// <summary>
        /// Get the client rectangle for the display of this target.
        /// </summary>
        public Rectangle ClientRectangle
        {
            get { return _months.ClientRectangle; }
        }

        /// <summary>
        /// Should a mouse down at the provided point cause the currently stacked context menu to become current.
        /// </summary>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to become current; otherwise false.</returns>
        public bool DoesStackedClientMouseDownBecomeCurrent(Point pt)
        {
            return true;
        }
        #endregion

        #region Mouse Notifications
        /// <summary>
        /// Mouse has entered the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void MouseEnter(Control c)
        {
            _mouseOver = true;

            if (ViewManager != null)
                ViewManager.SetTarget(this, true);
        }

        /// <summary>
        /// Mouse has moved inside the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
        {
            if (_mouseOver)
            {
                if (_captured)
                {
                    if (_selectionStart != DateTime.MinValue)
                    {
                        // Find the button (day entry) closest (date wise) to the point provided. 
                        // So if over a blank area or over a disabled day it gives the date that 
                        // would be appropriate to use as the end selection date.
                        DateTime selectEnd = _months.DayNearPoint(pt);

                        // Enforce the maximum number of days
                        DateTime selectStart = _selectionStart;
                        TimeSpan span = new TimeSpan(_months.Calendar.MaxSelectionCount - 1, 0, 0, 0);
                        if (selectEnd > selectStart)
                        {
                            if ((selectEnd - selectStart) > span)
                                selectEnd = selectStart + span;

                            _months.FocusDay = selectEnd;
                        }
                        else if (selectEnd < selectStart)
                        {
                            if ((selectStart - selectEnd) > span)
                                selectEnd = selectStart - span;

                            // Switch around so the begin is before the end
                            DateTime temp = selectEnd;
                            selectEnd = selectStart;
                            selectStart = temp;

                            _months.FocusDay = selectStart;
                        }

                        // Use the new seletion range
                        _months.Calendar.SetSelectionRange(selectStart, selectEnd);
                        _needPaint(_months, new NeedLayoutEventArgs(false));
                    }
                }
                else
                {
                    // Find the button (day entry) under the new mouse position
                    _months.TrackingDay = _months.DayFromPoint(pt, true);
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
            // Only interested in left mouse pressing down
            if (button == MouseButtons.Left)
            {
                // Capturing mouse input
                _captured = true;

                // Ensure the month calendar has the focus
                if ((c != null) && (c is KryptonMonthCalendar) && !c.ContainsFocus)
                    c.Focus();

                // Set the selection to be the day clicked
                DateTime? clickDay = _months.DayFromPoint(pt, false);
                if (clickDay != null)
                {
                    _months.Calendar.SetSelectionRange(clickDay.Value, clickDay.Value);
                    _months.FocusDay = clickDay.Value;
                    _months.AnchorDay = clickDay.Value;
                    _selectionStart = _months.Calendar.SelectionStart;
                    _needPaint(_months, new NeedLayoutEventArgs(true));
                }
                else
                    _selectionStart = DateTime.MinValue;
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
            // If the mouse is currently captured
            if (_captured)
            {
                // Not capturing mouse input anymore
                _captured = false;

                // Only interested in left mouse being released
                if (button == MouseButtons.Left)
                {
                    if ((_monthCalendar != null) && _monthCalendar.AutoClose && (_months.Provider != null))
                    {
                        // If the mouse was pressed down on a day entry, then we close down on mouse up
                        if (_selectionStart != DateTime.MinValue)
                        {
                            // Is the menu capable of being closed?
                            if (_months.Provider.ProviderCanCloseMenu)
                            {
                                // Ask the original context menu definition, if we can close
                                CancelEventArgs cea = new CancelEventArgs();
                                _months.Provider.OnClosing(cea);

                                if (!cea.Cancel)
                                {
                                    // Close the menu from display and pass in the item clicked as the reason
                                    _months.Provider.OnClose(new CloseReasonEventArgs(ToolStripDropDownCloseReason.ItemClicked));
                                }
                            }
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
            // Only if mouse is leaving all the children monitored by controller.
            if (_mouseOver && !_months.ContainsRecurse(next))
            {
                _mouseOver = false;
                _months.TrackingDay = null;

                if (ViewManager != null)
                    ViewManager.ClearTarget(this);
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
            if (c == null) throw new ArgumentNullException("c");
            if (e == null) throw new ArgumentNullException("e");

            if (_viewManager != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Tab:
                        _viewManager.KeyTab(e.Shift);
                        return;
                }
            }

            // Get the current focus date
            DateTime focusDate = (_months.FocusDay == null ? (_monthCalendar == null ? _months.Calendar.SelectionStart : _monthCalendar.SelectionStart) : _months.FocusDay.Value);
            DateTime anchorDate = (_months.AnchorDay == null ? (_monthCalendar == null ? _months.Calendar.SelectionStart : _monthCalendar.SelectionStart) : _months.AnchorDay.Value);

            // Use keyboard to modify the new focus date
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (e.Control)
                        focusDate = focusDate.AddMonths(-1);
                    else
                        focusDate = focusDate.AddDays(-1);
                    break;
                case Keys.Right:
                    if (e.Control)
                        focusDate = focusDate.AddMonths(1);
                    else
                        focusDate = focusDate.AddDays(1);
                    break;
                case Keys.Up:
                    focusDate = focusDate.AddDays(-7);
                    break;
                case Keys.Down:
                    focusDate = focusDate.AddDays(7);
                    break;
                case Keys.Home:
                    if (e.Control)
                    {
                        focusDate = focusDate.AddMonths(-1);
                        focusDate = new DateTime(focusDate.Year, focusDate.Month, 1);
                    }
                    else
                        focusDate = new DateTime(focusDate.Year, focusDate.Month, 1);
                    break;
                case Keys.End:
                    if (e.Control)
                    {
                        focusDate = focusDate.AddMonths(1);
                        focusDate = new DateTime(focusDate.Year, focusDate.Month, 1);
                        focusDate = focusDate.AddMonths(1).AddDays(-1);
                    }
                    else
                    {
                        focusDate = new DateTime(focusDate.Year, focusDate.Month, 1);
                        focusDate = focusDate.AddMonths(1).AddDays(-1);
                    }
                    break;
                case Keys.PageUp:
                    if (e.Control)
                        focusDate = focusDate.AddMonths(-1 * _months.Months);
                    else
                        focusDate = focusDate.AddMonths(-1);
                    break;
                case Keys.PageDown:
                    if (e.Control)
                        focusDate = focusDate.AddMonths(1 * _months.Months);
                    else
                        focusDate = focusDate.AddMonths(1);
                    break;
                case Keys.Enter:
                case Keys.Space:
                    if ((_monthCalendar != null) && _monthCalendar.AutoClose && (_months.Provider != null))
                    {
                        // Is the menu capable of being closed?
                        if (_months.Provider.ProviderCanCloseMenu)
                        {
                            // Ask the original context menu definition, if we can close
                            CancelEventArgs cea = new CancelEventArgs();
                            _months.Provider.OnClosing(cea);

                            if (!cea.Cancel)
                            {
                                // Close the menu from display and pass in the item clicked as the reason
                                _months.Provider.OnClose(new CloseReasonEventArgs(ToolStripDropDownCloseReason.Keyboard));
                            }
                        }
                    }
                    break;
            }

            // If the max selection count is 1 then always treat the new selection as the new focus 
            // day we have just calculated. If the shift key is not pressed then definitely treat as 
            // a single day selection.
            if ((_months.Calendar.MaxSelectionCount == 1) || !e.Shift)
            {
                _months.AnchorDay = focusDate;
                _months.FocusDay = focusDate;
                _months.Calendar.SetSelectionRange(focusDate, focusDate);

                if (_viewManager != null)
                    _needPaint(this, new NeedLayoutEventArgs(true));
            }
            else
            {
                DateTime startDate = _months.Calendar.SelectionStart;
                DateTime endDate = _months.Calendar.SelectionEnd;

                if (focusDate < anchorDate)
                {
                    // Cannot extend selection beyond the max selection count
                    if ((anchorDate - focusDate).Days >= _months.Calendar.MaxSelectionCount)
                        focusDate = anchorDate.AddDays(-(_months.Calendar.MaxSelectionCount - 1));

                    startDate = focusDate;
                    endDate = anchorDate;
                }
                else if (focusDate > anchorDate)
                {
                    // Cannot extend selection beyond the max selection count
                    if ((focusDate - anchorDate).Days >= _months.Calendar.MaxSelectionCount)
                        focusDate = anchorDate.AddDays(_months.Calendar.MaxSelectionCount - 1);

                    startDate = anchorDate;
                    endDate = focusDate;
                }

                _months.AnchorDay = anchorDate;
                _months.FocusDay = focusDate;
                _months.Calendar.SetSelectionRange(startDate, endDate);

                if (_viewManager != null)
                    _needPaint(this, new NeedLayoutEventArgs(true));
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
            return false;
        }
        #endregion

        #region Source Notifications
        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void GotFocus(Control c)
        {
            Debug.Assert(c != null);

            // Validate incoming references
            if (c == null) throw new ArgumentNullException("c");
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
        }
        #endregion

        #region Implementation
        private ViewContextMenuManager ViewManager
        {
            get { return _viewManager; }
        }
        #endregion
    }
}
