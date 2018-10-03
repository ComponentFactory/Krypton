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
using System.Windows.Forms;
using System.IO;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Specialise the generic collection with type specific rules for item accessor.
    /// </summary>
    public class DragTargetProviderCollection : TypedCollection<IDragTargetProvider> {};

    /// <summary>
    /// Manage a dragging operation.
    /// </summary>
    public class DragManager : IDragPageNotify,
                               IDisposable
    {
        #region Static Fields
        private static Cursor _validCursor;
        private static Cursor _invalidCursor;
        #endregion

        #region Instance Fields
        private IPalette _dragPalette;
        private IPalette _localPalette;
        private IRenderer _dragRenderer;
        private PaletteMode _paletteMode;
        private PaletteRedirect _redirector;
        private PaletteDragDrop _stateCommon;
        private DragTargetProviderCollection _targetProviders;
        private PageDragEndData _pageDragEndData;
        private DragFeedback _dragFeedback;
        private DragTargetList _dragTargets;
        private DragTarget _currentTarget;
        private bool _documentCursor;
        private bool _dragging;
        private bool _disposed;
        #endregion

        #region Identity
		/// <summary>
	    /// Initializes a static fields of the TargetManager class.
	    /// </summary>
        static DragManager()
	    {
            using (MemoryStream ms = new MemoryStream(Properties.Resources.DocumentValid))
                _validCursor = new Cursor(ms);

            using (MemoryStream ms = new MemoryStream(Properties.Resources.DocumentInvalid))
                _invalidCursor = new Cursor(ms);
        }

        /// <summary>
        /// Initialize a new instance of the DragManager class.
        /// </summary>
        public DragManager()
        {
            _redirector = new PaletteRedirect();
            _stateCommon = new PaletteDragDrop(null, null);
            _paletteMode = PaletteMode.Global;
            _targetProviders = new DragTargetProviderCollection();
            _dragTargets = new DragTargetList();
            _documentCursor = false;
        }

		/// <summary>
		/// Release resources.
		/// </summary>
        ~DragManager()
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
                ClearDragFeedback();
			}

            ClearTargets();

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
        /// Gets access to the common navigator appearance entries.
        /// </summary>
        public PaletteDragDrop StateCommon
        {
            get { return _stateCommon; }
        }

        /// <summary>
        /// Gets or sets the palette to be applied.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get { return _paletteMode; }
            
            set 
            {
                if (_paletteMode != value)
                {
                    switch (value)
                    {
                        case PaletteMode.Custom:
                            // Do nothing, you must assign a palette to the 
                            // 'Palette' property in order to get the custom mode
                            break;
                        default:
                            _paletteMode = value;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets and sets the custom palette implementation.
        /// </summary>
        public IPalette Palette
        {
            get { return _localPalette; }
         
            set 
            {
                if (_localPalette != value)
                {
                    _localPalette = value;
                    if (_localPalette == null)
                        _paletteMode = PaletteMode.Global;
                    else
                        _paletteMode = PaletteMode.Custom;
                }
            }
        }

        /// <summary>
        /// Gets access to the collection of target providers.
        /// </summary>
        public DragTargetProviderCollection DragTargetProviders
        {
            get { return _targetProviders; }
        }

        /// <summary>
        /// Gets a value indicating if dragging is currently occuring.
        /// </summary>
        public bool IsDragging
        {
            get { return _dragging; }
        }

        /// <summary>
        /// Gets and sets a value indicating if document cursors should be used during dragging.
        /// </summary>
        public bool DocumentCursor
        {
            get { return _documentCursor; }
            
            set 
            {
                if (IsDragging)
                    throw new InvalidOperationException("Cannot update DocumentCursor property during dragging operation.");
                else
                    _documentCursor = value;
            }
        }

        /// <summary>
        /// Occurs when dragging starts.
        /// </summary>
        /// <param name="screenPt">Mouse screen point at start of drag.</param>
        /// <param name="dragEndData">Data to be dropped at destination.</param>
        /// <returns>True if dragging waas started; otherwise false.</returns>
        public virtual bool DragStart(Point screenPt, PageDragEndData dragEndData)
        {
            if (IsDisposed)
                throw new InvalidOperationException("Cannot DragStart when instance have been disposed.");

            if (IsDragging)
                throw new InvalidOperationException("Cannot DragStart when already performing dragging operation.");

            if (dragEndData == null)
                throw new ArgumentNullException("Cannot provide an empty DragEndData.");

            // Generate drag targets from the set of target provides
            ClearTargets();
            foreach (IDragTargetProvider provider in DragTargetProviders)
                _dragTargets.AddRange(provider, dragEndData);

            // We only drag if we have at least one page and one target
            _dragging = ((_dragTargets.Count > 0) && (dragEndData.Pages.Count > 0));

            // Do we really need to start dragging?
            if (_dragging)
            {
                // We cache the palette/renderer at start of drag and use the same ones for the
                // whole duration of the drag as changing drawing info during drag would be hard!
                ResolvePaletteRenderer();

                // Cache page data for duration of dragging operation
                _pageDragEndData = dragEndData;

                // Create correct drag feedback class and start it up
                ResolveDragFeedback();
                _dragFeedback.Start(_stateCommon, _dragRenderer, _pageDragEndData, _dragTargets);
            }
            else
                ClearTargets();

            return _dragging;
        }

        /// <summary>
        /// Occurs on dragging movement.
        /// </summary>
        /// <param name="screenPt">Latest screen point during dragging.</param>
        public virtual void DragMove(Point screenPt)
        {
            if (IsDisposed)
                throw new InvalidOperationException("Cannot DragMove when instance have been disposed.");

            if (!IsDragging)
                throw new InvalidOperationException("Cannot DragMove when DragStart has not been called.");

            // Different feedback objects implement visual feeback differently and so only the feedback
            // instance knows the correct target to use for the given screen point and drag data.
            _currentTarget = _dragFeedback.Feedback(screenPt, _currentTarget);

            // Check if we need a cursor to indicate the drag state
            UpdateCursor();
        }

        /// <summary>
        /// Occurs when dragging ends because of dropping.
        /// </summary>
        /// <param name="screenPt">Ending screen point when dropping.</param>
        /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
        public virtual bool DragEnd(Point screenPt)
        {
            if (IsDisposed)
                throw new InvalidOperationException("Cannot DragEnd when instance have been disposed.");

            if (!IsDragging)
                throw new InvalidOperationException("Cannot DragEnd when DragStart has not been called.");

            // Different feedback objects implement visual feeback differently and so only the feedback
            // instance knows the correct target to use for the given screen point and drag data.
            _currentTarget = _dragFeedback.Feedback(screenPt, _currentTarget);

            // Remove visual feedback
            _dragFeedback.Quit();

            // Inform target it needs to perform the drop action
            bool ret = false;
            if (_currentTarget != null)
                ret = _currentTarget.PerformDrop(screenPt, _pageDragEndData);

            ClearTargets();
            RestoreCursor();
            EndDragging();

            return ret;
        }

        /// <summary>
        /// Occurs when dragging quits.
        /// </summary>
        public virtual void DragQuit()
        {
            if (IsDisposed)
                throw new InvalidOperationException("Cannot DragQuit when instance have been disposed.");

            if (!IsDragging)
                throw new InvalidOperationException("Cannot DragQuit when DragStart has not been called.");

            // Remove visual feedback
            _dragFeedback.Quit();

            ClearTargets();
            RestoreCursor();
            EndDragging();
        }

        /// <summary>
        /// Occurs when a page drag is about to begin and allows it to be cancelled.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        /// <param name="navigator">Navigator instance associated with source; can be null.</param>
        /// <param name="e">Event arguments indicating list of pages being dragged.</param>
        public virtual void PageDragStart(object sender, KryptonNavigator navigator, PageDragCancelEventArgs e)
        {
            e.Cancel = !DragStart(e.ScreenPoint, new PageDragEndData(sender, navigator, e.Pages));
        }

        /// <summary>
        /// Occurs when the mouse moves during the drag operation.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        /// <param name="e">Event arguments containing the new screen point of the mouse.</param>
        public virtual void PageDragMove(object sender, PointEventArgs e)
        {
            DragMove(e.Point);
        }

        /// <summary>
        /// Occurs when drag operation completes with pages being dropped.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        /// <param name="e">Event arguments containing the new screen point of the mouse.</param>
        /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
        public virtual bool PageDragEnd(object sender, PointEventArgs e)
        {
            return DragEnd(e.Point);
        }

        /// <summary>
        /// Occurs when dragging pages has been cancelled.
        /// </summary>
        /// <param name="sender">Source of the page drag; can be null.</param>
        public virtual void PageDragQuit(object sender)
        {
            DragQuit();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Create the actual drop data based on the proposed data provided.
        /// </summary>
        /// <param name="dropData">Proposed drop data.</param>
        /// <returns>Actual drop data</returns>
        protected virtual PageDragEndData CreateDropData(PageDragEndData dropData)
        {
            return dropData;
        }

        /// <summary>
        /// Update the displayed cursor to reflect the current dragging state.
        /// </summary>
        protected virtual void UpdateCursor()
        {
            // Should we update cursor to reflect document dragging?
            if (IsDragging && DocumentCursor)
            {
                if (_pageDragEndData.Navigator != null)
                {
                    if (_currentTarget == null)
                        _pageDragEndData.Navigator.Cursor = _invalidCursor;
                    else
                        _pageDragEndData.Navigator.Cursor = _validCursor;
                }
            }
        }

        /// <summary>
        /// Restore the displayed cursor back to null.
        /// </summary>
        protected virtual void RestoreCursor()
        {
            if (IsDragging)
            {
                if (_pageDragEndData.Navigator != null)
                    _pageDragEndData.Navigator.Cursor = null;
            }
        }
        #endregion

        #region Implementation
        private void ResolvePaletteRenderer()
        {
            // Resolve the correct palette instance to use
            switch (_paletteMode)
            {
                case PaletteMode.Custom:
                    _dragPalette = _localPalette;
                    break;
                default:
                    _dragPalette = KryptonManager.GetPaletteForMode(_paletteMode);
                    break;
            }

            // Update redirector to point at the resolved palette
            _redirector.Target = _dragPalette;

            // Inherit the state common values from resolved palette
            _stateCommon.SetInherit(_dragPalette);

            // Get the renderer associated with the palette
            _dragRenderer = _dragPalette.GetRenderer();
        }

        private void ResolveDragFeedback()
        {
            ClearDragFeedback();

            // Start with the provided value
            PaletteDragFeedback dragFeedback = _stateCommon.GetDragDropFeedback();

            // Should never be 'inherit'
            if (dragFeedback == PaletteDragFeedback.Inherit)
                dragFeedback = PaletteDragFeedback.Rounded;

            // Check if the rounded style is possible
            if (dragFeedback == PaletteDragFeedback.Rounded)
            {
                // Rounded feedback uses a per-pixel alpha blending and so we need to be on a machine that supports
                // more than 256 colors and also allows the layered windows feature. If not then revert to sqaures
                if ((OSFeature.Feature.GetVersionPresent(OSFeature.LayeredWindows) == null) || (CommonHelper.ColorDepth() <= 8))
                    dragFeedback = PaletteDragFeedback.Square;
            }

            switch (dragFeedback)
            {
                case PaletteDragFeedback.Rounded:
                case PaletteDragFeedback.Square:
                    _dragFeedback = new DragFeedbackDocking(dragFeedback);
                    break;
                default:
                    _dragFeedback = new DragFeedbackSolid();
                    break;
            }
        }

        private void ClearDragFeedback()
        {
            if (_dragFeedback != null)
            {
                _dragFeedback.Dispose();
                _dragFeedback = null;
            }
        }

        private void ClearTargets()
        {
            if (_dragTargets != null)
            {
                // Dispose the targets to ensure references are removed to prevent memory leaks
                foreach (DragTarget target in _dragTargets)
                    target.Dispose();

                _dragTargets.Clear();
            }

            _currentTarget = null;
        }

        private void EndDragging()
        {
            _dragPalette = null;
            _dragRenderer = null;
            _pageDragEndData = null;
            _dragging = false;
        }
        #endregion
    }
}
