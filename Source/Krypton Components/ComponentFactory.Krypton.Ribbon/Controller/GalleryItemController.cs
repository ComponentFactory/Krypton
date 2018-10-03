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
	/// Process mouse events for a gallery item.
	/// </summary>
    internal class GalleryItemController : GlobalId,
                                           IMouseController,
                                           ISourceController,
                                           IKeyController
	{
		#region Instance Fields
        private ViewDrawRibbonGalleryItem _target;
        private ViewLayoutRibbonGalleryItems _layout;
        private NeedPaintHandler _needPaint;
        private Point _mousePoint;
        private bool _captured;
        private bool _mouseOver;
        #endregion

		#region Events
		/// <summary>
		/// Occurs when the mouse is used to left click the target.
		/// </summary>
		public event MouseEventHandler Click;
		#endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the GalleryItemController class.
		/// </summary>
		/// <param name="target">Target for state changes.</param>
        /// <param name="layout">Reference to layout of the image items.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public GalleryItemController(ViewDrawRibbonGalleryItem target,
                                     ViewLayoutRibbonGalleryItems layout,
                                     NeedPaintHandler needPaint)
		{
			Debug.Assert(target != null);
            Debug.Assert(layout != null);

            _mousePoint = CommonHelper.NullPoint;
			_target = target;
            _layout = layout;
            NeedPaint = needPaint;
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
            UpdateTargetState(c);
		}

		/// <summary>
		/// Mouse has moved inside the view.
		/// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        /// <param name="pt">Mouse position relative to control.</param>
        public virtual void MouseMove(Control c, Point pt)
		{
            // Track the mouse point
            _mousePoint = pt;

            // Update the visual state
            UpdateTargetState(pt);
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
                    // Only if the button is still pressed, do we generate a click
                    if (_target.ElementState == PaletteState.Pressed)
                    {
                        // Move back to hot tracking state, we have to do this
                        // before the click is generated because the click processing
                        // might change focus and so cause the MouseLeave to be
                        // called and change the state. If this was after the click
                        // then it would overwrite and lose that leave state change.
                        _target.ElementState = PaletteState.Tracking;

                        // Can only click if enabled
                        if (_target.Enabled)
                        {
                            // Generate a click event
                            OnClick(new MouseEventArgs(MouseButtons.Left, 1, pt.X, pt.Y, 0));

                            // Ensure when pressed the gallery becomes focused
                            if (!c.ContainsFocus)
                            {
                                if (c is KryptonGallery)
                                {
                                    KryptonGallery gallery = (KryptonGallery)c;
                                    if (gallery.Ribbon == null)
                                        gallery.Focus();
                                }
                                else
                                    c.Focus();
                            }
                        }
                    }

                    // Repaint to reflect new state
                    OnNeedPaint(true);
                }
                else
                {
                    // Update the visual state
                    UpdateTargetState(pt);
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
            if (!_target.ContainsRecurse(next))
            {
                // Mouse is no longer over the target
                _mouseOver = false;

                // Not tracking the mouse means a null value
                _mousePoint = CommonHelper.NullPoint; 

                // If leaving the view then cannot be capturing mouse input anymore
                _captured = false;

                // Update the visual state
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
        public void GotFocus(Control c)
        {
        }

        /// <summary>
        /// Source control has lost the focus.
        /// </summary>
        /// <param name="c">Reference to the source control instance.</param>
        public void LostFocus(Control c)
        {
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
                case Keys.Up:
                    _layout.TrackMoveUp();
                    break;
                case Keys.Down:
                    _layout.TrackMoveDown();
                    break;
                case Keys.Left:
                    _layout.TrackMoveLeft();
                    break;
                case Keys.Right:
                    _layout.TrackMoveRight();
                    break;
                case Keys.Home:
                    _layout.TrackMoveHome();
                    break;
                case Keys.End:
                    _layout.TrackMoveEnd();
                    break;
                case Keys.PageDown:
                    _layout.TrackMovePageDown();
                    break;
                case Keys.PageUp:
                    _layout.TrackMovePageUp();
                    break;
                case Keys.Enter:
                case Keys.Space:
                    // Can only click if enabled
                    if (_target.Enabled)
                    {
                        // Generate a click event
                        OnClick(new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    }
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

                // If capturing input....
                if (_captured)
                {
                    if (_target.ClientRectangle.Contains(pt))
                        newState = PaletteState.Pressed;
                    else
                        newState = PaletteState.Tracking;
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

            // If state has changed or change in (inside split area)
            if (_target.ElementState != newState)
            {
                if (newState == PaletteState.Tracking)
                    _target.Track();
                else
                    _target.Untrack();

                // Update target to reflect new state
                _target.ElementState = newState;

                // Redraw to show the change in visual state
                OnNeedPaint(true);
            }
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
		/// Raises the NeedPaint event.
		/// </summary>
		/// <param name="needLayout">Does the palette change require a layout.</param>
		protected virtual void OnNeedPaint(bool needLayout)
		{
            if (_needPaint != null)
                _needPaint(this, new NeedLayoutEventArgs(needLayout, _target.ClientRectangle));
		}
		#endregion
    }
}
