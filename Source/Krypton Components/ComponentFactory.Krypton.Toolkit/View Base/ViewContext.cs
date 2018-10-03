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
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Encapsulates common context for view layout and render operations.
	/// </summary>
    public class ViewContext : GlobalId,
                               IDisposable
	{
		#region Instance Fields
        private ViewManager _manager;
		private Control _control;
        private Control _alignControl;
		private Graphics _graphics;
		private Control _topControl;
		private IRenderer _renderer;
        private bool _disposeGraphics;
        private bool _disposeManager;
		#endregion

		#region Identity
		/// <summary>
		/// Initialize a new instance of the ViewContext class.
		/// </summary>
        /// <param name="manager">Reference to the view manager.</param>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="alignControl">Control used for aligning elements.</param>
        /// <param name="renderer">Rendering provider.</param>
        public ViewContext(ViewManager manager,
                           Control control, 
                           Control alignControl, 
                           IRenderer renderer)
            : this(manager, control, alignControl, null, renderer)
        {
        }

        /// <summary>
		/// Initialize a new instance of the ViewContext class.
		/// </summary>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="alignControl">Control used for aligning elements.</param>
        /// <param name="graphics">Graphics instance for drawing.</param>
		/// <param name="renderer">Rendering provider.</param>
        public ViewContext(Control control,
                           Control alignControl,
                           Graphics graphics,
                           IRenderer renderer)
            : this(null, control, alignControl, graphics, renderer)
        {
        }

        /// <summary>
		/// Initialize a new instance of the ViewContext class.
		/// </summary>
        /// <param name="manager">Reference to the view manager.</param>
        /// <param name="control">Control associated with rendering.</param>
        /// <param name="alignControl">Control used for aligning elements.</param>
        /// <param name="graphics">Graphics instance for drawing.</param>
		/// <param name="renderer">Rendering provider.</param>
        public ViewContext(ViewManager manager,
                           Control control,
                           Control alignControl,
                           Graphics graphics,
						   IRenderer renderer)
		{
            // Use the manager is provided, otherwise create a temporary one with a null view
            if (manager != null)
                _manager = manager;
            else
            {
                _manager = new ViewManager(control, new ViewLayoutNull());
                _disposeManager = true;
            }

            // Cache initial values
            _control = control;
            _alignControl = alignControl;
			_graphics = graphics;
			_renderer = renderer;
		}

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        public void Dispose()
        {
            // Is there a graphics instance that might need disposed?
            if (_graphics != null)
            {
                // Only dispose if we created it
                if (_disposeGraphics)
                    _graphics.Dispose();

                _graphics = null;
            }

            // Is there a manager instance that might need disposed?
            if (_manager != null)
            {
                // Only dispose if we created it
                if (_disposeManager)
                    _manager.Dispose();

                _manager = null;
            }
        }
		#endregion

		#region Public
        /// <summary>
        /// Gets the owning view manager.
        /// </summary>
        public ViewManager ViewManager
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _manager; }
        }

        /// <summary>
		/// Gets and sets the owning control associated with rendering.
		/// </summary>
        public Control Control
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _control; }
            set { _control = value; }
		}

        /// <summary>
        /// Gets and sets the control to use when aligning elements.
        /// </summary>
        public Control AlignControl
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _alignControl; }
            set { _alignControl = value; }
        }

		/// <summary>
		/// Gets the graphics instance used for rendering operations.
		/// </summary>
		public Graphics Graphics
		{
			get 
            {
                // Do we need to create the graphics instance?
                if (_graphics == null)
                {
                    // If the control has been created...
                    if (Control.IsHandleCreated)
                    {
                        // Get the graphics instance from the control
                        _graphics = Control.CreateGraphics();
                    }
                    else
                    {
                        // ...otherwise create a graphics that is not
                        // tied to the control itself as we do not want
                        // to force the control to be created.
                        _graphics = Graphics.FromHwnd(IntPtr.Zero);
                    }

                    // We need to dispose of it later
                    _disposeGraphics = true;
                }

                return _graphics;
            }
		}

		/// <summary>
		/// Gets the owning top level control associated with rendering.
		/// </summary>
		public Control TopControl
		{
			get 
            {
                // If this is the first need for the top control...
                if (_topControl == null)
                {
                    // Cache the top most owning control
                    _topControl = _control.TopLevelControl;

                    // If no top level control was found...
                    // (this happens at design time)
                    if (_topControl == null)
                    {
                        // Start searching from the control
                        Control parentControl = _control;

                        // Climb the parent chain to the top
                        while (parentControl.Parent != null)
                        {
                            // Stop at the first Form instance found
                            if (parentControl is Form)
                                break;

                            parentControl = parentControl.Parent;
                        }

                        // Use the top most control found
                        _topControl = parentControl;
                    }
                }

                return _topControl; 
            }
		}

		/// <summary>
		/// Gets access to the renderer provider.
		/// </summary>
        public IRenderer Renderer
		{
            [System.Diagnostics.DebuggerStepThrough]
            get { return _renderer; }
		}
		#endregion
    }
}
