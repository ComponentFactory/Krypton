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
    /// Manage a list of drag targets.
    /// </summary>
    public class DragTargetList : List<DragTarget>
    {
        #region Public
        /// <summary>
        /// Add a list of drag targets from the provided interface.
        /// </summary>
        /// <param name="provider">Interface reference.</param>
        /// <param name="dragEndData">Pages data being dragged.</param>
        public void AddRange(IDragTargetProvider provider, PageDragEndData dragEndData)
        {
            if (provider != null)
            {
                DragTargetList targets = provider.GenerateDragTargets(dragEndData);
                if ((targets != null) && (targets.Count > 0))
                    AddRange(targets);
            }
        }
        #endregion
    };

    /// <summary>
    /// Base class for dragging target implementations.
    /// </summary>
    public abstract class DragTarget : IDisposable
    {
        #region Instance Fields
        private Rectangle _screenRect;
        private Rectangle _hotRect;
        private Rectangle _drawRect;
        private DragTargetHint _hint;
        private KryptonPageFlags _allowFlags;
        private bool _disposed;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DragTarget class.
        /// </summary>
        /// <param name="screenRect">Rectangle representing targets screen area.</param>
        /// <param name="hotRect">Rectangle representing targets hot area.</param>
        /// <param name="drawRect">Rectangle representing targets drawing area.</param>
        /// <param name="hint">Hint about the targets operation.</param>
        /// <param name="allowFlags">Only drop pages that have one of these flags defined.</param>
        public DragTarget(Rectangle screenRect,
                          Rectangle hotRect,
                          Rectangle drawRect,
                          DragTargetHint hint,
                          KryptonPageFlags allowFlags)
        {
            _screenRect = screenRect;
            _hotRect = hotRect;
            _drawRect = drawRect;
            _hint = hint;
            _allowFlags = allowFlags;
        }

        /// <summary>
		/// Release resources.
		/// </summary>
        ~DragTarget()
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
        /// Gets the rectangle representing the targets screen area.
        /// </summary>
        public Rectangle ScreenRect
        {
            get { return _screenRect; }
        }

        /// <summary>
        /// Gets the rectangle representing the targets hot area.
        /// </summary>
        public Rectangle HotRect
        {
            get { return _hotRect; }
        }

        /// <summary>
        /// Gets the rectangle representing the targets drawing area.
        /// </summary>
        public Rectangle DrawRect
        {
            get { return _drawRect; }
        }

        /// <summary>
        /// Gets the hint used to help the drag feedback.
        /// </summary>
        public DragTargetHint Hint
        {
            get { return _hint; }
        }

        /// <summary>
        /// Gets the flags of the pages allowed to be dropped.
        /// </summary>
        public KryptonPageFlags AllowFlags
        {
            get { return _allowFlags; }
        }

        /// <summary>
        /// Is this target a match for the provided screen position.
        /// </summary>
        /// <param name="screenPt">Position in screen coordinates.</param>
        /// <param name="dragEndData">Data to be dropped at destination.</param>
        /// <returns>True if a match; otherwise false.</returns>
        public virtual bool IsMatch(Point screenPt, PageDragEndData dragEndData)
        {
            // Default to matching if the mouse is inside the targets hot area
            return HotRect.Contains(screenPt);
        }

        /// <summary>
        /// Perform the drop action associated with the target.
        /// </summary>
        /// <param name="screenPt">Position in screen coordinates.</param>
        /// <param name="dragEndData">Data to be dropped at destination.</param>
        /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
        public abstract bool PerformDrop(Point screenPt, PageDragEndData dragEndData);
        #endregion

        #region Protected
        /// <summary>
        /// Process the drag pages in the context of a target navigator.
        /// </summary>
        /// <param name="target">Target navigator instance.</param>
        /// <param name="data">Dragged page data.</param>
        /// <returns>Last page to be transferred.</returns>
        protected KryptonPage ProcessDragEndData(KryptonNavigator target,
                                                 PageDragEndData data)
        {
            KryptonPage ret = null;

            // Add each source page to the target
            foreach (KryptonPage page in data.Pages)
            {
                // Only add the page if one of the allow flags is set
                if ((page.Flags & (int)AllowFlags) != 0)
                {
                    // Use event to allow decision on if the page should be dropped
                    // (or even swap the page for a different page to be dropped)
                    PageDropEventArgs e = new PageDropEventArgs(page);
                    target.OnPageDrop(e);

                    if (!e.Cancel && (e.Page != null))
                    {
                        target.Pages.Add(e.Page);
                        ret = e.Page;
                    }
                }
            }

            return ret;
        }
        #endregion
    }
}
