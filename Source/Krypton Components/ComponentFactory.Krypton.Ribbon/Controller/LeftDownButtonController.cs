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
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Provide button pressed when mouse down functionality.
    /// </summary>
    internal class LeftDownButtonController : GlobalId,
                                              IMouseController
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private bool _active;
        private bool _mouseOver;
        private bool _mouseDown;
        private bool _fixedPressed;
        private bool _hasFocus;
        private ViewBase _target;
        private NeedPaintHandler _needPaint;
        private Timer _updateTimer;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the button is pressed.
        /// </summary>
        public event MouseEventHandler Click;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the LeftDownButtonController class.
		/// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        /// <param name="target">Target for state changes.</param>
        /// <param name="needPaint">Delegate for notifying changes in display.</param>
        public LeftDownButtonController(KryptonRibbon ribbon,
                                        ViewBase target, 
                                        NeedPaintHandler needPaint)
		{
            Debug.Assert(ribbon != null);
            Debug.Assert(target != null);
            Debug.Assert(needPaint != null);

            _ribbon = ribbon;
            _target = target;
            _needPaint = needPaint;

            _updateTimer = new Timer();
            _updateTimer.Interval = 1;
            _updateTimer.Tick += new EventHandler(OnUpdateTimer);
        }
		#endregion

        #region Ribbon
        /// <summary>
        /// Gets access to the owning ribbon instance.
        /// </summary>
        public KryptonRibbon Ribbon
        {
            get { return _ribbon; }
        }
        #endregion

        #region Target
        /// <summary>
        /// Gets access to the target view for this controller.
        /// </summary>
        public ViewBase Target
        {
            get { return _target; }
        }
        #endregion

        #region HasFocus
        /// <summary>
        /// Gets and sets the focus flag.
        /// </summary>
        public bool HasFocus
        {
            get { return _hasFocus; }
            set { _hasFocus = value; }
        }
        #endregion

        #region IsFixed
        /// <summary>
        /// Is the controller fixed in the pressed state.
        /// </summary>
        public bool IsFixed
        {
            get { return _fixedPressed; }
        }
        #endregion

        #region SetFixed
        /// <summary>
        /// Fix the display of the button.
        /// </summary>
        public void SetFixed()
        {
            // Show the button as pressed, until told otherwise
            _fixedPressed = true;
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
                _mouseDown = false;

                // No longer in fixed state mode
                _fixedPressed = false;

                // Update appearance to reflect current state
                _updateTimer.Start();
            }
        }
        #endregion

        #region Mouse Notifications
        /// <summary>
        /// Mouse has entered the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void MouseEnter(Control c)
        {
            // Mouse is over the target
            _mouseOver = true;

            // Get the form we are inside
            KryptonForm ownerForm = _ribbon.FindKryptonForm();
            _active = ((ownerForm != null) && ownerForm.WindowActive) ||
                      VisualPopupManager.Singleton.IsTracking ||
                      _ribbon.InDesignMode ||
                      (CommonHelper.ActiveFloatingWindow != null);

            if (!_fixedPressed)
                _updateTimer.Start();
        }

        /// <summary>
        /// Mouse has moved inside the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
        {
            if (!_fixedPressed)
            {
                // Oops, we should really be in mouse over state
                if (!_mouseOver)
                {
                    _mouseOver = true;
                    _updateTimer.Start();
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
            // Is pressing down with the mouse when we are always active for showing changes
            _active = true;

            // Only interested in left mouse pressing down
            if (button == MouseButtons.Left)
            {
                _mouseDown = true;

                // If already in fixed mode, then ignore mouse down
                if (!_fixedPressed && _ribbon.Enabled)
                {
                    // Mouse is being pressed
                    UpdateTargetState();

                    // Fix the button to be displayed as pressed
                    SetFixed();

                    // Generate a click event
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                }
            }

            return true;
        }

        /// <summary>
        /// Mouse button has been released in the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        /// <param name="button">Mouse button released.</param>
        public virtual void MouseUp(Control c, Point pt, MouseButtons button)
        {
        }

        /// <summary>
        /// Mouse has left the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="next">Reference to view that is next to have the mouse.</param>
        public virtual void MouseLeave(Control c, ViewBase next)
        {
            // Mouse is no longer over the target
            _mouseOver = false;

            if (!_fixedPressed)
                _updateTimer.Start();
        }

        /// <summary>
        /// Left mouse button double click.
        /// </summary>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void DoubleClick(Point pt)
        {
        }

        /// <summary>
        /// Should the left mouse down be ignored when present on a visual form border area.
        /// </summary>
        public virtual bool IgnoreVisualFormLeftButtonDown
        {
            get { return false; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Set the correct visual state of the target.
        /// </summary>
        protected virtual void UpdateTargetState()
        {
            // By default the button is in the normal state
            PaletteState newState = PaletteState.Normal;

            // Only allow another state if the ribbon is enabled
            if (_ribbon.Enabled)
            {
                // Are we showing with the fixed state?
                if (_fixedPressed)
                    newState = PaletteState.Pressed;
                else
                {
                    // If being pressed
                    if (_mouseDown)
                        newState = PaletteState.Pressed;
                    else if (_mouseOver && _active)
                        newState = PaletteState.Tracking;
                }
            }

            // Only interested in changes of state
            if (_target.ElementState != newState)
            {
                // Update state
                _target.ElementState = newState;

                // Need to repaint just the target client area
                OnNeedPaint(false, _target.ClientRectangle);

                // Get the repaint to happen immediately
                if (!_ribbon.InKeyboardMode)
                    Application.DoEvents();
            }
        }

        /// <summary>
        /// Raises the NeedPaint event.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        /// <param name="invalidRect">Client area to be invalidated.</param>
        protected virtual void OnNeedPaint(bool needLayout,
                                           Rectangle invalidRect)
        {
            if (_needPaint != null)
                _needPaint(this, new NeedLayoutEventArgs(needLayout, invalidRect));
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">A MouseEventArgs containing the event data.</param>
        protected virtual void OnClick(MouseEventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }
        #endregion

        #region Implementation
        private void OnUpdateTimer(object sender, EventArgs e)
        {
            _updateTimer.Stop();
            UpdateTargetState();
        }
        #endregion
    }
}
