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
    /// Provide application button button functionality.
    /// </summary>
    internal class AppButtonController : GlobalId,
                                         IMouseController,
                                         ISourceController,
                                         IKeyController,
                                         IRibbonKeyTipTarget
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private ViewBase _target1;
        private ViewBase _target2;
        private ViewBase _target3;
        private bool _mouseOver;
        private bool _mouseDown;
        private bool _fixedPressed;
        private bool _hasFocus;
        private bool _keyboard;
        private Timer _updateTimer;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the button is pressed.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the button is released.
        /// </summary>
        public event EventHandler MouseReleased;

        /// <summary>
        /// Occurs when a change in button state requires a repaint.
        /// </summary>
        public event NeedPaintHandler NeedPaint;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the AppButtonController class.
		/// </summary>
        public AppButtonController(KryptonRibbon ribbon)
		{
            _ribbon = ribbon;
            _updateTimer = new Timer();
            _updateTimer.Interval = 1;
            _updateTimer.Tick += new EventHandler(OnUpdateTimer);
            _keyboard = false;
        }
		#endregion

        #region Public
        /// <summary>
        /// Gets and sets the first target element.
        /// </summary>
        public ViewBase Target1
        {
            get { return _target1; }
            set { _target1 = value; }
        }

        /// <summary>
        /// Gets and sets the second target element.
        /// </summary>
        public ViewBase Target2
        {
            get { return _target2; }
            set { _target2 = value; }
        }

        /// <summary>
        /// Gets and sets the third target element.
        /// </summary>
        public ViewBase Target3
        {
            get { return _target3; }
            set { _target3 = value; }
        }

        /// <summary>
        /// Gets a value indicating if the keyboard was used to request the menu.
        /// </summary>
        public bool Keyboard
        {
            get { return _keyboard; }
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
                // Mouse is being pressed
                _mouseDown = true;

                // If already in fixed mode, then ignore mouse down
                if (!_fixedPressed && _ribbon.Enabled)
                {
                    // Mouse is being pressed
                    UpdateTargetState();

                    // Show the button as pressed, until told otherwise
                    _fixedPressed = true;

                    // Generate a click event
                    _keyboard = false;
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                }
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
            // Only interested in left mouse going up
            if (button == MouseButtons.Left)
                OnMouseReleased(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
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
            get { return true; }
        }
        #endregion

        #region Focus Notifications
        /// <summary>
        /// Source control has got the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void GotFocus(Control c)
        {
            _hasFocus = true;

            if (!_fixedPressed)
                _updateTimer.Start();
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void LostFocus(Control c)
        {
            _hasFocus = false;

            if (!_fixedPressed)
                _updateTimer.Start();
        }
        #endregion

        #region Key Notifications
        /// <summary>
        /// Key has been pressed down.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public void KeyDown(Control c, KeyEventArgs e)
        {
            ViewBase newView = null;
            KryptonRibbon ribbon = (KryptonRibbon)c;

            switch (e.KeyData)
            {
                case Keys.Tab:
                case Keys.Right:
                    // Ask the ribbon to get use the first view for the qat
                    newView = ribbon.GetFirstQATView();

                    // Get the first near edge button (the last near button is the leftmost one!)
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetLastVisibleViewButton(PaletteRelativeEdgeAlign.Near);

                    if (newView == null)
                    {
                        if ((e.KeyData == Keys.Tab) && (ribbon.SelectedTab != null))
                        {
                            // Get the currently selected tab page
                            newView = ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab);
                        }
                        else
                        {
                            // Get the first visible tab page
                            newView = ribbon.TabsArea.LayoutTabs.GetViewForFirstRibbonTab();
                        }
                    }

                    // Move across to any far defined buttons
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                    // Move across to any inherit defined buttons
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);
                    break;
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    // Move across to any far defined buttons
                    newView = ribbon.TabsArea.ButtonSpecManager.GetLastVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                    // Move across to any inherit defined buttons
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetLastVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

                    if (newView == null)
                    {
                        if (e.KeyData != Keys.Left)
                        {
                            // Get the last control on the selected tab
                            newView = ribbon.GroupsArea.ViewGroups.GetLastFocusItem();

                            // Get the currently selected tab page
                            if (newView == null)
                            {
                                if (ribbon.SelectedTab != null)
                                    newView = ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab);
                                else
                                    newView = ribbon.TabsArea.LayoutTabs.GetViewForLastRibbonTab();
                            }
                        }
                        else
                        {
                            // Get the last visible tab page
                            newView = ribbon.TabsArea.LayoutTabs.GetViewForLastRibbonTab();
                        }
                    }

                    // Get the last near edge button (the first near button is the rightmost one!)
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Near);

                    // Get the last qat button
                    if (newView == null)
                        newView = ribbon.GetLastQATView();
                    break;
                case Keys.Space:
                case Keys.Enter:
                case Keys.Down:
                    // Show the button as pressed, until told otherwise
                    _fixedPressed = true;

                    // Mouse is being pressed
                    UpdateTargetState();

                    // Generate a click event
                    _keyboard = true;
                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
            }

            // If we have a new view to focus and it is not ourself...
            if ((newView != null) && (newView != _target1) && 
                (newView != _target2 && (newView != _target3)))
            {
                // If the new view is a tab then select that tab unless in minimized mode
                if ((newView is ViewDrawRibbonTab) && !ribbon.RealMinimizedMode)
                    ribbon.SelectedTab = ((ViewDrawRibbonTab)newView).RibbonTab;

                // Finally we switch focus to new view
                ribbon.FocusView = newView;
            }
        }

        /// <summary>
        /// Key has been pressed.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        public void KeyPress(Control c, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// Key has been released.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        /// <returns>True if capturing input; otherwise false.</returns>
        public bool KeyUp(Control c, KeyEventArgs e)
        {
            return false;
        }
        #endregion

        #region KeyTipSelect
        /// <summary>
        /// Perform actual selection of the item.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        public void KeyTipSelect(KryptonRibbon ribbon)
        {
            // We leave key tips usage whenever we use the application button
            ribbon.KillKeyboardKeyTips();

            // Show the button as pressed, until told otherwise
            _fixedPressed = true;

            // Mouse is being pressed
            UpdateTargetState();

            // Switch focus to ourself
            ribbon.FocusView =  ribbon.TabsArea.LayoutAppButton.AppButton;

            // Generate a click event
            _keyboard = true;
            OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }
        #endregion

        #region Protected
        /// <summary>
        /// Set the correct visual state of the target.
        /// </summary>
        protected void UpdateTargetState()
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
                    else if (_mouseOver || _hasFocus)
                        newState = PaletteState.Tracking;
                }
            }

            bool needPaint = false;

            // Update all the targets
            if ((_target1 != null) && (_target1.ElementState != newState))
            {
                _target1.ElementState = newState;
                needPaint = true;
            }

            if ((_target2 != null) && (_target2.ElementState != newState))
            {
                _target2.ElementState = newState;
                needPaint = true;
            }

            if ((_target3 != null) && (_target3.ElementState != newState))
            {
                _target3.ElementState = newState;
                needPaint = true;
            }

            if (needPaint)
            {
                if ((_target1 != null) && !_target1.ClientRectangle.IsEmpty) 
                    OnNeedPaint(false, _target1.ClientRectangle);

                if ((_target2 != null) && !_target2.ClientRectangle.IsEmpty) 
                    OnNeedPaint(false, _target2.ClientRectangle);

                if ((_target3 != null) && !_target3.ClientRectangle.IsEmpty) 
                    OnNeedPaint(false, _target3.ClientRectangle);

                // Get the repaint to happen immediately
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
            if (NeedPaint != null)
                NeedPaint(this, new NeedLayoutEventArgs(needLayout, invalidRect));
        }

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">A MouseEventArgs containing the event data.</param>
        protected virtual void OnClick(MouseEventArgs e)
        {
            if (Click != null)
                Click(this, e);

            _keyboard = false;
        }

        /// <summary>
        /// Raises the MouseReleased event.
        /// </summary>
        /// <param name="e">A MouseEventArgs containing the event data.</param>
        protected virtual void OnMouseReleased(MouseEventArgs e)
        {
            if (MouseReleased != null)
                MouseReleased(this, e);
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
