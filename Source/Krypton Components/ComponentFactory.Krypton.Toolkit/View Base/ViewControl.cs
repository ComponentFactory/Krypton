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
using System.Reflection;
using Microsoft.Win32;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Control that is contained inside an element to act as clipping of real controls.
    /// </summary>
    [ToolboxItem(false)]
    public class ViewControl : Control
    {
        #region Static Field
        private static MethodInfo _miPTB;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the background needs painting.
        /// </summary>
        public event PaintEventHandler PaintBackground;

        /// <summary>
        /// Occurs when the WM_NCHITTEST occurs.
        /// </summary>
        public event EventHandler<ViewControlHitTestArgs> WndProcHitTest;
        #endregion

        #region Instance Fields
        private VisualControl _rootControl;
        private VisualPopup _rootPopup;
        private ViewLayoutControl _viewLayout;
        private NeedPaintHandler _needPaintDelegate;
        private bool _transparentBackground;
        private bool _inDesignMode;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewControl class.
        /// </summary>
        /// <param name="rootControl">Top level visual control.</param>
        public ViewControl(VisualControl rootControl)
        {
            Debug.Assert(rootControl != null);

            // We use double buffering to reduce drawing flicker
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            // We need to repaint entire control whenever resized
            SetStyle(ControlStyles.ResizeRedraw, true);

            // We are not selectable
            SetStyle(ControlStyles.Selectable, false);

            // Default
            _transparentBackground = false;
            _inDesignMode = false;

            // Remember incoming references
            _rootControl = rootControl;

            // Create delegate so child elements can request a repaint
            _needPaintDelegate = new NeedPaintHandler(OnNeedPaint);
        }
        #endregion

        #region ViewLayoutControl
        /// <summary>
        /// Gets and sets access to the view layout control.
        /// </summary>
        public ViewLayoutControl ViewLayoutControl
        {
            get { return _viewLayout; }
            set { _viewLayout = value; }
        }
        #endregion

        #region UpdateParent
        /// <summary>
        /// Gets and sets the root control for point translation and message dispatch. 
        /// </summary>
        /// <param name="parent">Parent control.</param>
        public void UpdateParent(Control parent)
        {
            // Keep looking till we run out of parents
            while (parent != null)
            {
                // We can hook into a visual control derived class
                if (parent is VisualControl)
                {
                    _rootControl = (VisualControl)parent;
                    _rootPopup = null;
                    break;
                }

                // We can hook into a visual popup derived class
                if (parent is VisualPopup)
                {
                    _rootControl = null;
                    _rootPopup = (VisualPopup)parent;
                    break;
                }

                // Move up another level
                parent = parent.Parent;
            }
        }
        #endregion

        #region TransparentBackground
        /// <summary>
        /// Gets and sets if the background is transparent.
        /// </summary>
        public bool TransparentBackground
        {
            get { return _transparentBackground; }
            set { _transparentBackground = value; }
        }
        #endregion

        #region InDesignMode
        /// <summary>
        /// Gets and sets a value indicating if the control is in design mode.
        /// </summary>
        public bool InDesignMode
        {
            get { return _inDesignMode; }
            set { _inDesignMode = value; }
        }
        #endregion

        #region NeedPaintDelegate
        /// <summary>
        /// Gets access to the need paint delegate.
        /// </summary>
        public NeedPaintHandler NeedPaintDelegate
        {
            get { return _needPaintDelegate; }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !RootInstance.IsDisposed)
            {

                // Do we need to paint the background as the foreground of the parent
                if (TransparentBackground)
                    PaintTransparentBackground(e);

                // Give handles a change to draw the background
                if (PaintBackground != null)
                    PaintBackground(this, e);

                // Create a render context for drawing the view
                using (RenderContext context = new RenderContext(GetViewManager(),
                                                                 this,
                                                                 RootInstance,
                                                                 e.Graphics,
                                                                 e.ClipRectangle,
                                                                 Renderer))
                {
                    // Ask the view to paint itself
                    _viewLayout.ChildView.Render(context);
                }
            }
        }

        /// <summary>
        /// Raises the DoubleClick event.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnDoubleClick(EventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !RootInstance.IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (GetViewManager() != null)
                {
                    // Use the root controls view manager to process the event
                    GetViewManager().DoubleClick(this.PointToClient(Control.MousePosition));
                }
            }

            // Let base class fire events
            base.OnDoubleClick(e);
        }

        /// <summary>
        /// Raises the MouseMove event.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !RootInstance.IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (GetViewManager() != null)
                {
                    // Convert from control to parent control coordinates
                    Point rootPoint = RootInstance.PointToClient(PointToScreen(new Point(e.X, e.Y)));

                    // Use the root controls view manager to process the event
                    GetViewManager().MouseMove(new MouseEventArgs(e.Button,
                                                                  e.Clicks,
                                                                  rootPoint.X,
                                                                  rootPoint.Y,
                                                                  e.Delta),
                                               new Point(e.X, e.Y));
                }
            }

            // Let base class fire events
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the MouseDown event.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !RootInstance.IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (GetViewManager() != null)
                {
                    // Convert from control to parent control coordinates
                    Point rootPoint = RootInstance.PointToClient(PointToScreen(new Point(e.X, e.Y)));

                    // Use the root controls view manager to process the event
                    GetViewManager().MouseDown(new MouseEventArgs(e.Button,
                                                                  e.Clicks,
                                                                  rootPoint.X,
                                                                  rootPoint.Y,
                                                                  e.Delta),
                                               new Point(e.X, e.Y));
                }

                // If the root control does not have focus, then give it the focus now
                if (!RootInstance.ContainsFocus && RootInstance.CanSelect)
                {
                    // Do not change focus at design time because 
                    if (!InDesignMode)
                        RootInstance.Focus();
                }
            }

            // Let base class fire events
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the MouseUp event.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !RootInstance.IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (GetViewManager() != null)
                {
                    // Convert from control to parent control coordinates
                    Point rootPoint = RootInstance.PointToClient(PointToScreen(new Point(e.X, e.Y)));

                    // Use the root controls view manager to process the event
                    GetViewManager().MouseUp(new MouseEventArgs(e.Button,
                                                                e.Clicks,
                                                                rootPoint.X,
                                                                rootPoint.Y,
                                                                e.Delta),
                                             new Point(e.X, e.Y));
                }
            }

            // Let base class fire events
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Raises the MouseLeave event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !RootInstance.IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (GetViewManager() != null)
                {
                    // Use the root controls view manager to process the event
                    GetViewManager().MouseLeave(e);
                }
            }

            // Let base class fire events
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the KeyDown event.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !RootInstance.IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (GetViewManager() != null)
                    GetViewManager().KeyDown(e);
            }

            // Let base class fire events
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Raises the KeyPress event.
        /// </summary>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !RootInstance.IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (GetViewManager() != null)
                    GetViewManager().KeyPress(e);
            }

            // Let base class fire events
            base.OnKeyPress(e);
        }

        /// <summary>
        /// Raises the KeyUp event.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing && !RootInstance.IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (GetViewManager() != null)
                    GetViewManager().KeyUp(e);
            }

            // Let base class fire events
            base.OnKeyUp(e);
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected virtual void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            Debug.Assert(e != null);

            // Validate incoming reference
            if (e == null) throw new ArgumentNullException("e");

            if (IsHandleCreated)
            {
                // Always request the repaint immediately
                if (e.InvalidRect.IsEmpty)
                    Invalidate(true);
                else
                    Invalidate(e.InvalidRect, true);
            }
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            // Only interested in intercepting the hit testing
            if (m.Msg == PI.WM_NCHITTEST)
            {
                // Extract the screen point for the hit test
                Point screenPoint = new Point((int)m.LParam.ToInt64());

                // Generate event so message can be processed
                ViewControlHitTestArgs args = new ViewControlHitTestArgs(PointToClient(screenPoint));
                OnWndProcHitTest(args);

                if (!args.Cancel)
                {
                    m.Result = args.Result;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Raises the WndProcHitTest event.
        /// </summary>
        /// <param name="e">A ViewControlHitTestArgs containing the event data.</param>
        protected virtual void OnWndProcHitTest(ViewControlHitTestArgs e)
        {
            if (WndProcHitTest != null)
                WndProcHitTest(this, e);
        }
        #endregion

        #region Implementation
        private Control RootInstance
        {
            get
            {
                if (_rootControl != null)
                    return _rootControl;
                else if (_rootPopup != null)
                    return _rootPopup;
                else
                {
                    Debug.Assert(false);
                    return null;
                }
            }
        }

        private ViewManager GetViewManager()
        {
            if (_rootControl != null)
                return _rootControl.GetViewManager();
            else if (_rootPopup != null)
                return _rootPopup.GetViewManager();
            else
            {
                Debug.Assert(false);
                return null;
            }
        }

        private IRenderer Renderer
        {
            get
            {
                if (_rootControl != null)
                    return _rootControl.Renderer;
                else if (_rootPopup != null)
                    return _rootPopup.Renderer;
                else
                {
                    Debug.Assert(false);
                    return null;
                }
            }
        }

        private void PaintTransparentBackground(PaintEventArgs e)
        {
            // Get the parent control for transparent drawing purposes
            Control parent = Parent;

            // Do we have a parent control and we need to paint background?
            if (parent != null)
            {
                // Only grab the required reference once
                if (_miPTB == null)
                {
                    // Use reflection so we can call the Windows Forms internal method for painting parent background
                    _miPTB = typeof(Control).GetMethod("PaintTransparentBackground",
                                                       BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                                                       null, CallingConventions.HasThis,
                                                       new Type[] { typeof(PaintEventArgs), typeof(Rectangle), typeof(Region) },
                                                       null);
                }

                try
                {
                    _miPTB.Invoke(this, new object[] { e, ClientRectangle, null });
                }
                catch
                {
                    _miPTB = null;
                }
            }
        }
        #endregion
    }
}
