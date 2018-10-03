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
	/// Process mouse events for a ribbon group radio button.
	/// </summary>
    internal class GroupRadioButtonController : GlobalId,
                                                IMouseController,
                                                ISourceController,
                                                IKeyController,
                                                IRibbonKeyTipTarget
	{
		#region Instance Fields
        private KryptonRibbon _ribbon;
        private ViewBase _targetMain;
        private ViewDrawRibbonGroupRadioButtonImage _targetImage;
        private NeedPaintHandler _needPaint;
        private bool _rightButtonDown;
        private bool _fixedPressed;
		private bool _captured;
        private bool _mouseOver;
        private bool _hasFocus;
		#endregion

		#region Events
		/// <summary>
		/// Occurs when a click portion is clicked.
		/// </summary>
		public event EventHandler Click;

        /// <summary>
        /// Occurs when the user right clicks the view.
        /// </summary>
        public event MouseEventHandler ContextClick;
        #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the GroupRadioButtonController class.
		/// </summary>
        /// <param name="ribbon">Source control instance.</param>
        /// <param name="targetMain">Target for main element changes.</param>
        /// <param name="targetImage">Target for image state changes.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public GroupRadioButtonController(KryptonRibbon ribbon,
                                          ViewBase targetMain,
                                          ViewDrawRibbonGroupRadioButtonImage targetImage,
                                          NeedPaintHandler needPaint)
		{
            Debug.Assert(ribbon != null);
            Debug.Assert(targetMain != null);
            Debug.Assert(targetImage != null);
            Debug.Assert(needPaint != null);

            _ribbon = ribbon;
            _targetMain = targetMain;
            _targetImage = targetImage;
            NeedPaint = needPaint;
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

        #region Mouse Notifications
        /// <summary>
		/// Mouse has entered the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void MouseEnter(Control c)
		{
            // Mouse is over the target
            _mouseOver = true;

            // Update the visual state
            if (!_fixedPressed)
                UpdateTargetState(c);
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

                // Update the visual state
                UpdateTargetState(pt);
            }

            // Remember the user has pressed the right mouse button down
            if (button == MouseButtons.Right)
                _rightButtonDown = true;
    			
            return _captured;
		}

        /// <summary>
        /// Mouse has moved inside the view.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
        {
            // Update the visual state
            UpdateTargetState(pt);
        }

		/// <summary>
		/// Mouse button has been released in the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
		/// <param name="button">Mouse button released.</param>
        public virtual void MouseUp(Control c, Point pt, MouseButtons button)
		{
            if (_captured)
            {
                // Not capturing mouse input anymore
                _captured = false;

                // Only interested in left mouse being released
                if (button == MouseButtons.Left)
                {
                    // Only if the button is still pressed, do we generate a click
                    if (_targetImage.Pressed)
                    {
                        // Move back to hot tracking state, we have to do this
                        // before the click is generated because the click processing
                        // might change focus and so cause the MouseLeave to be
                        // called and change the state. If this was after the click
                        // then it would overwrite and lose that leave state change.
                        _targetImage.Pressed = false;
                        _targetImage.Tracking = true;

                        // Can only click if enabled
                        if (_targetImage.Enabled)
                            OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));
                    }

                    // Repaint to reflect new state
                    OnNeedPaint(false);
                }
                else
                {
                    // Update the visual state
                    UpdateTargetState(pt);
                }
            }

            // If user is releasing the right mouse button
            if (button == MouseButtons.Right)
            {
                // And it was pressed over the tab
                if (_rightButtonDown)
                {
                    _rightButtonDown = false;

                    // Raises event so a context menu for the ribbon can be shown
                    OnContextClick(new MouseEventArgs(MouseButtons.Right, 1, pt.X, pt.Y, 0));
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
            // Mouse is no longer over the target
            _mouseOver = false;

            // Update the visual state
            if (!_fixedPressed)
            {
                // If leaving the view then cannot be capturing mouse input anymore
                _captured = false;

                UpdateTargetState(c);
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

        #region Focus Notifications
        /// <summary>
        /// Source control has got the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void GotFocus(Control c)
        {
            _hasFocus = true;
            UpdateTargetState(Point.Empty);
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public virtual void LostFocus(Control c)
        {
            _hasFocus = false;
            UpdateTargetState(Point.Empty);
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
            // Get the root control that owns the provided control
            c = _ribbon.GetControllerControl(c);

            if (c is KryptonRibbon)
                KeyDownRibbon(c as KryptonRibbon, e);
            else if (c is VisualPopupGroup)
                KeyDownPopupGroup(c as VisualPopupGroup, e);
            else if (c is VisualPopupMinimized)
                KeyDownPopupMinimized(c as VisualPopupMinimized, e);
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
            // Exit keyboard mode when you click the button spec
            ribbon.KillKeyboardMode();

            OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
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
        public ViewBase TargetMain
        {
            get { return _targetMain; }
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
        /// Gets a value indicating if mouse input is being captured.
        /// </summary>
        protected bool Captured
        {
            get { return _captured; }
            set { _captured = value; }
        }

        /// <summary>
        /// Set the correct visual state of the target.
        /// </summary>
        /// <param name="c">Owning control.</param>
        protected void UpdateTargetState(Control c)
        {
            if ((c == null) || c.IsDisposed)
                UpdateTargetState(new Point(int.MaxValue, int.MaxValue));
            else
                UpdateTargetState(c.PointToClient(Control.MousePosition));
        }

        /// <summary>
        /// Set the correct visual state of the target.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        protected void UpdateTargetState(Point pt)
        {
            bool tracking = false;
            bool pressed = false;

            // Can only be pressed or tracking if enabled
            if (_targetImage.Enabled)
            {
                // If capturing input....
                if (_captured)
                {
                    if (_fixedPressed || _targetMain.ClientRectangle.Contains(pt))
                        pressed = true;
                }
                else
                {
                    // Only hot tracking, so show tracking only if mouse over the target or has focus
                    if (_mouseOver || _hasFocus)
                        tracking = true;
                }
            }

            // If state has changed
            if ((_targetImage.Pressed != pressed) ||
                (_targetImage.Tracking != tracking))
            {
                _targetImage.Pressed = pressed;
                _targetImage.Tracking = tracking;

                // Redraw to show the change in visual state
                OnNeedPaint(false);
            }
        }

		/// <summary>
		/// Raises the Click event.
		/// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
		protected virtual void OnClick(EventArgs e)
		{
			if (Click != null)
				Click(_targetMain, e);
		}

        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">A MouseEventArgs containing the event data.</param>
        protected virtual void OnContextClick(MouseEventArgs e)
        {
            if (ContextClick != null)
                ContextClick(this, e);
        }

        /// <summary>
		/// Raises the NeedPaint event.
		/// </summary>
		/// <param name="needLayout">Does the palette change require a layout.</param>
		protected virtual void OnNeedPaint(bool needLayout)
		{
            if (_needPaint != null)
                _needPaint(this, new NeedLayoutEventArgs(needLayout, _targetMain.ClientRectangle));
		}
		#endregion

        #region Implementation
        private void KeyDownRibbon(KryptonRibbon ribbon, KeyEventArgs e)
        {
            ViewBase newView = null;

            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    // Get the previous focus item for the currently selected page
                    newView = ribbon.GroupsArea.ViewGroups.GetPreviousFocusItem(_targetMain);

                    // Got to the actual tab header
                    if (newView == null)
                        newView = ribbon.TabsArea.LayoutTabs.GetViewForRibbonTab(ribbon.SelectedTab);
                    break;
                case Keys.Tab:
                case Keys.Right:
                    // Get the next focus item for the currently selected page
                    newView = ribbon.GroupsArea.ViewGroups.GetNextFocusItem(_targetMain);

                    // Move across to any far defined buttons
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

                    // Move across to any inherit defined buttons
                    if (newView == null)
                        newView = ribbon.TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

                    // Rotate around to application button
                    if (newView == null)
                    {
                        if (ribbon.TabsArea.LayoutAppButton.Visible)
                            newView = ribbon.TabsArea.LayoutAppButton.AppButton;
                        else if (ribbon.TabsArea.LayoutAppTab.Visible)
                            newView = ribbon.TabsArea.LayoutAppTab.AppTab;
                    }                        
                    break;
                case Keys.Space:
                case Keys.Enter:
                    // Exit keyboard mode when you click the button spec
                    _ribbon.KillKeyboardMode();

                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
            }

            // If we have a new view to focus and it is not ourself...
            if ((newView != null) && (newView != TargetMain))
            {
                // If the new view is a tab then select that tab unless in minimized mode
                if ((newView is ViewDrawRibbonTab) && !ribbon.RealMinimizedMode)
                    ribbon.SelectedTab = ((ViewDrawRibbonTab)newView).RibbonTab;

                // Finally we switch focus to new view
                ribbon.FocusView = newView;
            }
        }

        private void KeyDownPopupGroup(VisualPopupGroup popupGroup, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    popupGroup.SetPreviousFocusItem();
                    break;
                case Keys.Tab:
                case Keys.Right:
                    popupGroup.SetNextFocusItem();
                    break;
                case Keys.Space:
                case Keys.Enter:
                    // Exit keyboard mode when you click the group button
                    _ribbon.KillKeyboardMode();

                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
            }
        }

        private void KeyDownPopupMinimized(VisualPopupMinimized popupMinimized, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Tab | Keys.Shift:
                case Keys.Left:
                    popupMinimized.SetPreviousFocusItem();
                    break;
                case Keys.Tab:
                case Keys.Right:
                    popupMinimized.SetNextFocusItem();
                    break;
                case Keys.Space:
                case Keys.Enter:
                    // Exit keyboard mode when you click the button spec
                    _ribbon.KillKeyboardMode();

                    OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
            }
        }
        #endregion
    }
}
