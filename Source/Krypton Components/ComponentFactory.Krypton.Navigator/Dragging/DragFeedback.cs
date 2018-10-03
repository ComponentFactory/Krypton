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
using System.Diagnostics;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Base class for drag feedback implementations.
    /// </summary>
    public abstract class DragFeedback : IDisposable
    {
        #region Instance Fields
        private IPaletteDragDrop _paletteDragDrop;
        private IRenderer _renderer;
        private PageDragEndData _pageDragEndData;
        private DragTargetList _dragTargets;
        private bool _disposed;
        #endregion

        #region Identity
        /// <summary>
		/// Release resources.
		/// </summary>
        ~DragFeedback()
		{
			// Only dispose of resources once
			if (!IsDisposed)
			{
				// Only dispose of managed resources
				Dispose(false);
			}
		}

		/// <summary>
		/// Release managed and unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			// Only dispose of resources once
			if (!IsDisposed)
			{
				// Dispose of managed and unmanaged resources
				Dispose(true);
			}
		}

		/// <summary>
		/// Release unmanaged and optionally managed resources.
		/// </summary>
		/// <param name="disposing">Called from Dispose method.</param>
		protected virtual void Dispose(bool disposing)
		{
			// If called from explicit call to Dispose
			if (disposing)
			{
				// No need to call destructor once dispose has occured
				GC.SuppressFinalize(this);

                _pageDragEndData = null;
                _dragTargets = null;
			}

			// Mark as disposed
			_disposed = true;
		}

        /// <summary>
        /// Gets a value indicating if the view has been disposed.
        /// </summary>
        public bool IsDisposed
        {
            get { return _disposed; }
        }
        #endregion

        #region Public
        /// <summary>
        /// Called to initialize the implementation when dragging starts.
        /// </summary>
        /// <param name="paletteDragDrop">Drawing palette.</param>
        /// <param name="renderer">Drawing renderer.</param>
        /// <param name="pageDragEndData">Drag data associated with drag operation.</param>
        /// <param name="dragTargets">List of all drag targets.</param>
        public virtual void Start(IPaletteDragDrop paletteDragDrop,
                                  IRenderer renderer,
                                  PageDragEndData pageDragEndData, 
                                  DragTargetList dragTargets)
        {
            Debug.Assert(paletteDragDrop != null);
            Debug.Assert(renderer != null);
            Debug.Assert(pageDragEndData != null);
            Debug.Assert(dragTargets != null);

            _paletteDragDrop = paletteDragDrop;
            _renderer = renderer;
            _pageDragEndData = pageDragEndData;
            _dragTargets = dragTargets;
        }

        /// <summary>
        /// Called to request feedback be shown for the specified target.
        /// </summary>
        /// <param name="screenPt">Current screen point of mouse.</param>
        /// <param name="target">Target that needs feedback.</param>
        /// <returns>Updated drag target.</returns>
        public abstract DragTarget Feedback(Point screenPt, DragTarget target);

        /// <summary>
        /// Called to cleanup when dragging has finished.
        /// </summary>
        public virtual void Quit()
        {
            _pageDragEndData = null;
            _dragTargets = null;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the cached drawing palette.
        /// </summary>
        protected IPaletteDragDrop PaletteDragDrop
        {
            get { return _paletteDragDrop; }
        }

        /// <summary>
        /// Gets access to the cached drawing renderer.
        /// </summary>
        protected IRenderer Renderer
        {
            get { return _renderer; }
        }

        /// <summary>
        /// Gets access to the cached drag data.
        /// </summary>
        protected PageDragEndData PageDragEndData
        {
            get { return _pageDragEndData; }
        }

        /// <summary>
        /// Gets access to the cached drag target list.
        /// </summary>
        protected DragTargetList DragTargets
        {
            get { return _dragTargets; }
        }
       #endregion
    }
}
