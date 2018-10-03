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
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Manages a view presentation for a control display surface.
	/// </summary>
    public class ViewManager : GlobalId,
                               IDisposable
    {
        #region Instance Fields
        private ViewBase _root;
		private ViewBase _activeView;
		private bool _mouseCaptured;
        private bool _doNotLayoutControls;
        private bool _outputDebug;
        private Control _control;
        private Control _alignControl;
        private int _layoutCounter;
        private int _paintCounter;
        private long _outputStart;
        #endregion

        #region Events
        /// <summary>
        /// Occurs just before the layout cuycle.
        /// </summary>
        public event EventHandler LayoutBefore;

        /// <summary>
        /// Occurs just after the layout cuycle.
        /// </summary>
        public event EventHandler LayoutAfter;

        /// <summary>
        /// Occurs when the mouse down event is processed.
        /// </summary>
        public event MouseEventHandler MouseDownProcessed;

        /// <summary>
        /// Occurs when the mouse up event is processed.
        /// </summary>
        public event MouseEventHandler MouseUpProcessed;

        /// <summary>
        /// Occurs when the mouse up event is processed.
        /// </summary>
        public event PointHandler DoubleClickProcessed;
        #endregion

        #region Identity
        /// <summary>
		/// Initialize a new instance of the ViewManager class.
		/// </summary>
        public ViewManager()
        {
        }

        /// <summary>
		/// Initialize a new instance of the ViewManager class.
		/// </summary>
        /// <param name="control">Owning control.</param>
        /// <param name="root">Root of the view hierarchy.</param>
        public ViewManager(Control control, ViewBase root)
		{
			_root = root;
            _root.OwningControl = control;
            _control = control;
            _alignControl = control;
		}

        /// <summary>
        /// Clean up any resources.
        /// </summary>
        public virtual void Dispose()
        {
            // Dispose of the associated element hierarchy
            if (_root != null)
                _root.Dispose();
        }
        #endregion

		#region Public Properties
        /// <summary>
        /// Attach the view manager to provided control and root element.
        /// </summary>
        /// <param name="control">Owning control.</param>
        /// <param name="root">Root of the view hierarchy.</param>
        public void Attach(Control control, ViewBase root)
        {
            _root = root;
            _root.OwningControl = control;
            _control = control;
            _alignControl = control;
        }

		/// <summary>
		/// Gets and sets the view root.
		/// </summary>
		public ViewBase Root
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _root; }
			
			set 
			{
				Debug.Assert(value != null);
				_root = value;
                _root.OwningControl = _control;
			}
		}

        /// <summary>
        /// Control owning the view manager.
        /// </summary>
        public Control Control
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _control; }
            set { _control = value; }
        }

        /// <summary>
        /// Control used to align view elements.
        /// </summary>
        public Control AlignControl
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _alignControl; }
            set { _alignControl = value; }
        }

        /// <summary>
        /// Should child controls be layed out during layout calls.
        /// </summary>
        public bool DoNotLayoutControls
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _doNotLayoutControls; }
            set { _doNotLayoutControls = value; }
        }

        /// <summary>
        /// Should debug information be output during layout and paint cycles.
        /// </summary>
        public bool OutputDebug
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _outputDebug; }
            set { _outputDebug = value; }
        }
        #endregion

		#region GetPreferredSize
		/// <summary>
		/// Discover the preferred size of the view.
		/// </summary>
		/// <param name="renderer">Renderer provider.</param>
        /// <param name="proposedSize">The custom-sized area for a control.</param>
        public virtual Size GetPreferredSize(IRenderer renderer,
                                             Size proposedSize)
		{
            if ((renderer == null) || (Root == null))
                return Size.Empty;

            Size retSize = Size.Empty;

            // Short circuit for a disposed control
            if (!_control.IsDisposed)
            {
                // Create a layout context for calculating size and positioning
                using (ViewLayoutContext context = new ViewLayoutContext(this,
                                                                         _control,
                                                                         _alignControl,
                                                                         renderer,
                                                                         proposedSize))
                {
                    retSize = Root.GetPreferredSize(context);
                }
            }

            if (_outputDebug)
            {
                Console.WriteLine("Id:{0} GetPreferredSize Type:{1} Ret:{2} Proposed:{3}",
                    Id, 
                    _control.GetType().ToString(), 
                    retSize,
                    proposedSize);
            }

            return retSize;
        }
		#endregion

        #region EvalTransparentPaint
        /// <summary>
		/// Perform a layout of the view.
		/// </summary>
		/// <param name="renderer">Renderer provider.</param>
        /// <returns>True if it contains transparent painting.</returns>
        public bool EvalTransparentPaint(IRenderer renderer)
        {
            Debug.Assert(renderer != null);
            Debug.Assert(Root != null);

            // Validate incoming reference
            if (renderer == null) throw new ArgumentNullException("renderer");

            // Create a layout context for calculating size and positioning
            using (ViewContext context = new ViewContext(this,
                                                         _control, 
                                                         _alignControl, 
                                                          renderer))
            {
                // Ask the view to perform operation
                return Root.EvalTransparentPaint(context);
            }
        }
        #endregion

        #region ActiveView
        /// <summary>
        /// Gets and sets the active view element.
        /// </summary>
        public ViewBase ActiveView
        {
            get { return _activeView; }

            set
            {
                // Is there a change in the view?
                if (value != _activeView)
                {
                    // Inform old element that mouse is leaving
                    if (_activeView != null)
                        _activeView.MouseLeave(value);

                    _activeView = value;

                    // Inform new element that mouse is entering
                    if (_activeView != null)
                        _activeView.MouseEnter();
                }
            }
        }
        #endregion

        #region ComponentFromPoint
        /// <summary>
        /// Is the provided point associated with a component.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        /// <returns>Component referece; otherwise false.</returns>
        public virtual Component ComponentFromPoint(Point pt)
        {
            // Find the view element associated with the point
            ViewBase target = Root.ViewFromPoint(pt);

            // Climb parent chain looking for the first element that has a component
            while (target != null)
            {
                if (target.Component != null)
                    return target.Component;

                target = target.Parent;
            }

            return null;
        }        
        #endregion

        #region MouseCaptured
        /// <summary>
        /// Gets and sets a value indicating if the mouse is capturing input.
        /// </summary>
        public bool MouseCaptured
        {
            get { return _mouseCaptured; }
            set { _mouseCaptured = value; }
        }
        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the view.
        /// </summary>
        /// <param name="renderer">Renderer provider.</param>
        public virtual void Layout(IRenderer renderer)
        {
            Debug.Assert(renderer != null);
            Debug.Assert(Root != null);

            // Do nothing if the control is disposed
            if (!_control.IsDisposed)
            {
                // Create a layout context for calculating size and positioning
                using (ViewLayoutContext context = new ViewLayoutContext(this,
                                                                         _control,
                                                                         _alignControl,
                                                                         renderer))
                {
                    Layout(context);
                }
            }
        }

        /// <summary>
		/// Perform a layout of the view.
		/// </summary>
        /// <param name="context">View context for layout operation.</param>
		public virtual void Layout(ViewLayoutContext context)
		{
            Debug.Assert(context != null);
            Debug.Assert(context.Renderer != null);
            Debug.Assert(Root != null);

            // Do nothing if the control is disposed
            if (!context.Control.IsDisposed)
            {
                if (_outputDebug)
                    PI.QueryPerformanceCounter(ref _outputStart);

                // Validate incoming references
                if (context.Renderer == null) throw new ArgumentNullException("renderer");

                // If someone is interested, tell them the layout cycle to beginning
                if (LayoutBefore != null)
                    LayoutBefore(this, EventArgs.Empty);

                // Ask the view to perform a layout
                Root.Layout(context);

                // If someone is interested, tell them the layout cycle has finished
                if (LayoutAfter != null)
                    LayoutAfter(this, EventArgs.Empty);

                if (_outputDebug)
                {
                    long outputEnd = 0;
                    PI.QueryPerformanceCounter(ref outputEnd);
                    long outputDiff = outputEnd - _outputStart;

                    Console.WriteLine("Id:{0} Layout Type:{1} Elapsed:{2} Rect:{3}",
                        Id, 
                        context.Control.GetType().ToString(),
                        outputDiff.ToString(),
                        context.DisplayRectangle);

                }

                // Maintain internal counters for measuring perf
                _layoutCounter++;
            }
        }
		#endregion

		#region Paint
		/// <summary>
		/// Perform a paint of the view.
		/// </summary>
		/// <param name="renderer">Renderer provider.</param>
		/// <param name="e">A PaintEventArgs that contains the event data.</param>
        public virtual void Paint(IRenderer renderer, PaintEventArgs e)
        {
            Debug.Assert(renderer != null);
            Debug.Assert(e != null);

            // Validate incoming references
            if (renderer == null) throw new ArgumentNullException("renderer");
            if (e == null) throw new ArgumentNullException("e");

            // Do nothing if the control is disposed or inside a layout call
            if (!_control.IsDisposed)
            {
                // Create a render context for drawing the view
                using (RenderContext context = new RenderContext(this,
                                                                 _control,
                                                                 _alignControl,
                                                                 e.Graphics,
                                                                 e.ClipRectangle,
                                                                 renderer))
                {
                    Paint(context);
                }
            }
        }

		/// <summary>
		/// Perform a paint of the view.
		/// </summary>
        /// <param name="context">Renderer context.</param>
        public virtual void Paint(RenderContext context)
		{
            Debug.Assert(context != null);
			Debug.Assert(Root != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // Do nothing if the control is disposed or inside a layout call
            if (!_control.IsDisposed)
            {
                if (_outputDebug)
                    PI.QueryPerformanceCounter(ref _outputStart);
                
                // Ask the view to paint itself
                Root.Render(context);

                if (_outputDebug)
                {
                    long outputEnd = 0;
                    PI.QueryPerformanceCounter(ref outputEnd);
                    long outputDiff = outputEnd - _outputStart;

                    Console.WriteLine("Id:{0} Paint Type:{1} Elapsed: {2}",
                        Id, 
                        _control.GetType().ToString(),
                        outputDiff.ToString());
                }
            }

            // Maintain internal counters for measuring perf
            _paintCounter++;
        }
		#endregion

		#region Mouse
		/// <summary>
		/// Perform mouse movement handling.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
        /// <param name="rawPt">The actual point provided from the windows message.</param>
		public virtual void MouseMove(MouseEventArgs e, Point rawPt)
		{
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");
            
            Point pt = new Point(e.X, e.Y);

			// Set the correct active view from the point
            UpdateViewFromPoint(_control, pt);

			// Tell current view of mouse movement
			if (ActiveView != null)
                ActiveView.MouseMove(rawPt);
		}

		/// <summary>
		/// Perform mouse down processing.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
        /// <param name="rawPt">The actual point provided from the windows message.</param>
        public virtual void MouseDown(MouseEventArgs e, Point rawPt)
		{
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");
            
            Point pt = new Point(e.X, e.Y);

			// Set the correct active view from the point
            UpdateViewFromPoint(_control, pt);

			// Tell current view of mouse down
            if (ActiveView != null)
                MouseCaptured = ActiveView.MouseDown(rawPt, e.Button);

            // Generate event to indicate the view manager has processed a mouse down
            PerformMouseDownProcessed(e);
		}

        /// <summary>
		/// Perform mouse up processing.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
        /// <param name="rawPt">The actual point provided from the windows message.</param>
        public virtual void MouseUp(MouseEventArgs e, Point rawPt)
		{
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");
            
            Point pt = new Point(e.X, e.Y);

			// Set the correct active view from the point
            UpdateViewFromPoint(_control, pt);

			// Tell current view of mouse up
            if (ActiveView != null)
                ActiveView.MouseUp(rawPt, e.Button);

			// Release any capture of the mouse
            MouseCaptured = false;

            // Generate event to indicate the view manager has processed a mouse up
            PerformMouseUpProcessed(e);
        }

        
        /// <summary>
		/// Perform mouse leave processing.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        public virtual void MouseLeave(EventArgs e)
		{
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");
            
            // If there is an active element
            if (ActiveView != null)
			{
                // Remove active view
                ActiveView = null;

				// No capture is in place anymore
                MouseCaptured = false;
			}
		}

        /// <summary>
        /// Perform double click processing.
        /// </summary>
        /// <param name="pt">Control coordinates point.</param>
        public virtual void DoubleClick(Point pt)
        {
            // If there is an active element
            if (ActiveView != null)
                ActiveView.DoubleClick(pt);

            // Generate event to indicate the view manager has processed a mouse up
            if (DoubleClickProcessed != null)
                DoubleClickProcessed(this, pt);
        }

        /// <summary>
        /// Raises the MouseDownProcessed event.
        /// </summary>
        /// <param name="e">A MouseEventArgs containing the event data.</param>
        public void PerformMouseDownProcessed(MouseEventArgs e)
        {
            if (MouseDownProcessed != null)
                MouseDownProcessed(this, e);
        }

        /// <summary>
        /// Raises the MouseUpProcessed event.
        /// </summary>
        /// <param name="e">A MouseEventArgs containing the event data.</param>
        public void PerformMouseUpProcessed(MouseEventArgs e)
        {
            if (MouseUpProcessed != null)
                MouseUpProcessed(this, e);
        }
        #endregion

        #region Key
        /// <summary>
        /// Perform key down handling.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public virtual void KeyDown(KeyEventArgs e)
        {
            // Tell current view of key event
            if (ActiveView != null)
                ActiveView.KeyDown(e);
            else if (_root != null)
                _root.KeyDown(e);
        }

        /// <summary>
        /// Perform key press handling.
        /// </summary>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        public virtual void KeyPress(KeyPressEventArgs e)
        {
            // Tell current view of key event
            if (ActiveView != null)
                ActiveView.KeyPress(e);
            else if (_root != null)
                _root.KeyPress(e);
        }

        /// <summary>
        /// Perform key up handling.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        public virtual void KeyUp(KeyEventArgs e)
        {
            // Tell current view of key event
            if (ActiveView != null)
                MouseCaptured = ActiveView.KeyUp(e);
            else if (_root != null)
                MouseCaptured = _root.KeyUp(e);
        }
        #endregion

        #region Source
        /// <summary>
        /// Perform got focus handling.
        /// </summary>
        public virtual void GotFocus()
        {
            // Tell current view of source event
            if (ActiveView != null)
                ActiveView.GotFocus(_control);
            else if (_root != null)
                _root.GotFocus(_control);
        }

        /// <summary>
        /// Perform lost focus handling.
        /// </summary>
        public virtual void LostFocus()
        {
            // Tell current view of source event
            if (ActiveView != null)
                ActiveView.LostFocus(_control);
            else if (_root != null)
                _root.LostFocus(_control);
        }
        #endregion

        #region ResetCounters
        /// <summary>
        /// Reset the internal counters.
        /// </summary>
        public void ResetCounters()
        {
            _layoutCounter = 0;
            _paintCounter = 0;
        }

        /// <summary>
        /// Gets the number of layout cycles performed since last reset.
        /// </summary>
        public int LayoutCounter
        {
            get { return _layoutCounter; }
        }

        /// <summary>
        /// Gets the number of paint cycles performed since last reset.
        /// </summary>
        public int PaintCounter
        {
            get { return _paintCounter; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Update the active view based on the mouse position.
        /// </summary>
        /// <param name="control">Source control.</param>
        /// <param name="pt">Point within the source control.</param>
        protected virtual void UpdateViewFromPoint(Control control, Point pt)
		{
			// Can only change view if not captured
            if (!MouseCaptured)
			{
                // Update the active view with that found under the mouse position
				ActiveView = Root.ViewFromPoint(pt);
			}
		}
		#endregion
    }
}
