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
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class MenuCheckBoxController : GlobalId,
                                            IMouseController,
                                            IKeyController,
                                            ISourceController,
                                            IContextMenuTarget
	{
		#region Instance Fields
        private bool _mouseOver;
        private bool _mouseReallyOver;
        private bool _highlight;
        private bool _mouseDown;
        private ViewBase _target;
        private ViewDrawMenuCheckBox _menuCheckBox;
        private NeedPaintHandler _needPaint;
        private ViewContextMenuManager _viewManager;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the check box has been clicked.
        /// </summary>
        public event EventHandler Click;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the MenuCheckBoxController class.
		/// </summary>
        /// <param name="viewManager">Owning view manager instance.</param>
        /// <param name="target">Target for state changes.</param>
        /// <param name="checkBox">Drawing element that owns check box display.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public MenuCheckBoxController(ViewContextMenuManager viewManager,
                                      ViewBase target,
                                      ViewDrawMenuCheckBox checkBox,
                                      NeedPaintHandler needPaint)
		{
            Debug.Assert(viewManager != null);
            Debug.Assert(target != null);
            Debug.Assert(checkBox != null);
            Debug.Assert(needPaint != null);

            _viewManager = viewManager;
            _target = target;
            _menuCheckBox = checkBox;
            NeedPaint = needPaint;
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
            HighlightState();
        }

        /// <summary>
        /// This target should clear any active display.
        /// </summary>
        public virtual void ClearTarget()
        {
            NormalState();
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
            // Only interested in enabled items
            if (_menuCheckBox.ItemEnabled)
                return Control.IsMnemonic(charCode, _menuCheckBox.ItemText);
            else
                return false;
        }

        /// <summary>
        /// Activate the item because of a mnemonic key press.
        /// </summary>
        public void MnemonicActivate()
        {
            // Only interested in enabled items
            if (_menuCheckBox.ItemEnabled)
                PressMenuCheckBox(true);
        }

        /// <summary>
        /// Gets the view element that should be used when this target is active.
        /// </summary>
        /// <returns>View element to become active.</returns>
        public ViewBase GetActiveView()
        {
            return _target;
        }

        /// <summary>
        /// Get the client rectangle for the display of this target.
        /// </summary>
        public Rectangle ClientRectangle
        {
            get { return _target.ClientRectangle; }
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
            if (!_mouseOver && _menuCheckBox.ItemEnabled)
            {
                _mouseReallyOver = _target.ClientRectangle.Contains(c.PointToClient(Control.MousePosition));
                _mouseOver = true;
                ViewManager.SetTarget(this, true);
                UpdateTarget();
            }
		}

		/// <summary>
		/// Mouse has moved inside the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
		{
            if (_menuCheckBox.ItemEnabled)
                _mouseReallyOver = true;
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
            if ((button == MouseButtons.Left) && _menuCheckBox.ItemEnabled)
            {
                _mouseDown = true;
                UpdateTarget();
            }

            return false;
		}

		/// <summary>
		/// Mouse button has been released in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button released.</param>
        public virtual void MouseUp(Control c, Point pt, MouseButtons button)
		{
            if (_mouseDown && (button == MouseButtons.Left))
            {
                _mouseDown = false;
                UpdateTarget();
                PressMenuCheckBox(false);
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
            if (!_target.ContainsRecurse(next))
            {
                _mouseOver = false;
                _mouseReallyOver = false;
                _mouseDown = false;
                ViewManager.ClearTarget(this);
                UpdateTarget();
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

            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Space:
                    // Only interested in enabled items
                    if (_menuCheckBox.ItemEnabled)
                        PressMenuCheckBox(true);
                    break;
                case Keys.Tab:
                    _viewManager.KeyTab(e.Shift);
                    break;
                case Keys.Home:
                    _viewManager.KeyHome();
                    break;
                case Keys.End:
                    _viewManager.KeyEnd();
                    break;
                case Keys.Up:
                    _viewManager.KeyUp();
                    break;
                case Keys.Down:
                    _viewManager.KeyDown();
                    break;
                case Keys.Left:
                    _viewManager.KeyLeft(true);
                    break;
                case Keys.Right:
                    _viewManager.KeyRight();
                    break;
            }
        }

        /// <summary>
        /// Key has been pressed.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        public virtual void KeyPress(Control c, KeyPressEventArgs e)
        {
            Debug.Assert(c != null);
            Debug.Assert(e != null);

            // Validate incoming references
            if (c == null) throw new ArgumentNullException("c");
            if (e == null) throw new ArgumentNullException("e");

            _viewManager.KeyMnemonic(e.KeyChar);
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
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void LostFocus(Control c)
        {
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
		/// Fires the NeedPaint event.
		/// </summary>
		public void PerformNeedPaint()
		{
			OnNeedPaint();
		}
		#endregion

        #region Private
        private ViewContextMenuManager ViewManager
        {
            get { return _viewManager; }
        }

        private void PressMenuCheckBox(bool keyboard)
        {
            if (keyboard)
            {
                _menuCheckBox.ViewDrawContent.ElementState = PaletteState.Pressed;
                PerformNeedPaint();
                Application.DoEvents();
            }

            // Should we automatically try and close the context menu stack
            if (_menuCheckBox.KryptonContextMenuCheckBox.AutoClose)
            {
                // Is the menu capable of being closed?
                if (_menuCheckBox.CanCloseMenu)
                {
                    // Ask the original context menu definition, if we can close
                    CancelEventArgs cea = new CancelEventArgs();
                    _menuCheckBox.Closing(cea);

                    if (!cea.Cancel)
                    {
                        // Close the menu from display and pass in the item clicked as the reason
                        _menuCheckBox.Close(new CloseReasonEventArgs(ToolStripDropDownCloseReason.ItemClicked));
                    }
                }
            }

            // Do we need to automatically change the checked/checkstate?
            if (_menuCheckBox.KryptonContextMenuCheckBox.AutoCheck)
            {
                // Grab current state from command or ourself
                CheckState state = (_menuCheckBox.KryptonContextMenuCheckBox.KryptonCommand == null ? 
                                    _menuCheckBox.KryptonContextMenuCheckBox.CheckState : 
                                    _menuCheckBox.KryptonContextMenuCheckBox.KryptonCommand.CheckState);

                // Change state based on the current state
                switch (state)
                {
                    case CheckState.Unchecked:
                        state = CheckState.Checked;
                        break;
                    case CheckState.Checked:
                        state = (_menuCheckBox.KryptonContextMenuCheckBox.ThreeState ? CheckState.Indeterminate : CheckState.Unchecked);
                        break;
                    case CheckState.Indeterminate:
                        state = CheckState.Unchecked;
                        break;
                }

                // Update correct target with new state
                if (_menuCheckBox.KryptonContextMenuCheckBox.KryptonCommand != null)
                    _menuCheckBox.KryptonContextMenuCheckBox.KryptonCommand.CheckState = state;
                else
                    _menuCheckBox.KryptonContextMenuCheckBox.CheckState = state;    
                
                // Update visual appearance to reflect new state
                _menuCheckBox.ViewDrawCheckBox.CheckState = state;
            }

            if (Click != null)
                Click(this, EventArgs.Empty);

            if (keyboard)
            {
                UpdateTarget();
                PerformNeedPaint();
            }
        }

        private void OnNeedPaint()
        {
            if (_needPaint != null)
                _needPaint(this, new NeedLayoutEventArgs(false));
        }

        private void HighlightState()
        {
            _highlight = true;
            UpdateTarget();
        }

        private void NormalState()
        {
            _highlight = false;
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            // Find new state for drawing the check box
            PaletteState state = (_menuCheckBox.ItemEnabled ? PaletteState.Normal : PaletteState.Disabled);
            if (_mouseOver)
            {
                if (_mouseDown)
                    state = PaletteState.Pressed;
                else
                    state = PaletteState.Tracking;
            }

            switch (state)
            {
                case PaletteState.Normal:
                    _menuCheckBox.ViewDrawCheckBox.Tracking = false;
                    _menuCheckBox.ViewDrawCheckBox.Pressed = false;
                    break;
                case PaletteState.Tracking:
                    _menuCheckBox.ViewDrawCheckBox.Tracking = true;
                    _menuCheckBox.ViewDrawCheckBox.Pressed = false;
                    break;
                case PaletteState.Pressed:
                    _menuCheckBox.ViewDrawCheckBox.Tracking = false;
                    _menuCheckBox.ViewDrawCheckBox.Pressed = true;
                    break;
            }

            bool applyFocus = (_highlight && !_mouseReallyOver);
            _menuCheckBox.KryptonContextMenuCheckBox.OverrideNormal.Apply = applyFocus;
            _menuCheckBox.KryptonContextMenuCheckBox.OverrideDisabled.Apply = applyFocus;
            _menuCheckBox.ViewDrawContent.ElementState = state;
            PerformNeedPaint();
        }
        #endregion
    }
}
