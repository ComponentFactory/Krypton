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
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Provides drag feedback as a set of docking indicators.
    /// </summary>
    public class DragFeedbackDocking : DragFeedback
    {
        #region Classes
        private class DockCluster : IDisposable
        {
            #region Type Definitons
            private class HintToTarget : Dictionary<DragTargetHint, DragTarget> { };
            #endregion

            #region Instance Fields
            private IPaletteDragDrop _paletteDragDrop;
            private IRenderer _renderer;
            private bool _excludeCluster;
            private Rectangle _screenRect;
            private Rectangle _drawRect;
            private HintToTarget _hintToTarget;
            private IDropDockingIndicator _indicators;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the DockCluster class.
            /// </summary>
            /// <param name="paletteDragDrop">Drawing palette.</param>
            /// <param name="renderer">Drawing renderer.</param>
            /// <param name="target">Initial target for the cluster.</param>
            public DockCluster(IPaletteDragDrop paletteDragDrop,
                               IRenderer renderer,
                               DragTarget target)
            {
                _paletteDragDrop = paletteDragDrop;
                _renderer = renderer;
                _screenRect = target.ScreenRect;
                _drawRect = target.DrawRect;
                _hintToTarget = new HintToTarget();
                _hintToTarget.Add(target.Hint & DragTargetHint.ExcludeFlags, target);
                _excludeCluster = (target.Hint & DragTargetHint.ExcludeCluster) == DragTargetHint.ExcludeCluster;
            }

            /// <summary>
            /// Release resources.
            /// </summary>
            public void Dispose()
            {
                _hintToTarget.Clear();
                _hintToTarget = null;

                if (_indicators != null)
                {
                    IDisposable dispose = _indicators as IDisposable;
                    dispose.Dispose();
                    _indicators = null;
                }
            }
            #endregion

            #region Public
            /// <summary>
            /// Gets the screen rectangle this cluster works for.
            /// </summary>
            public Rectangle ScreenRect
            {
                get { return _screenRect; }
            }

            /// <summary>
            /// Gets the drawing rectangle this cluster works for.
            /// </summary>
            public Rectangle DrawRect
            {
                get { return _drawRect; }
            }

            /// <summary>
            /// Gets a value indicating if the cluster is exlusive to the current contents.
            /// </summary>
            public bool ExcludeCluster
            {
                get { return _excludeCluster; }
            }

            /// <summary>
            /// Add the new target to the cluster.
            /// </summary>
            /// <param name="target">Target to add into cluster.</param>
            public void Add(DragTarget target)
            {
                // Find the hint that excludes extra flags
                DragTargetHint hint = target.Hint & DragTargetHint.ExcludeFlags;

                // Can only add one of each hint value
                if (!_hintToTarget.ContainsKey(hint))
                {
                    _hintToTarget.Add(hint, target);

                    // Make sure the drawing rectangle encloses all targets
                    _drawRect = Rectangle.Union(_drawRect, target.DrawRect);
                }
            }

            /// <summary>
            /// Update visual feedback based on the current screen position of the mouse.
            /// </summary>
            /// <param name="screenPt">Latest mouse screen position.</param>
            /// <param name="dragFeedback">Type of drag feedback required.</param>
            public DragTarget Feedback(Point screenPt, PaletteDragFeedback dragFeedback)
            {
                if (ScreenRect.Contains(screenPt))
                {
                    // Create the docking indicators the first time needed
                    if (_indicators == null)
                    {
                        switch (dragFeedback)
                        {
                            case PaletteDragFeedback.Rounded:
                                _indicators = new DropDockingIndicatorsRounded(_paletteDragDrop, _renderer,
                                                                               _hintToTarget.ContainsKey(DragTargetHint.EdgeLeft),
                                                                               _hintToTarget.ContainsKey(DragTargetHint.EdgeRight),
                                                                               _hintToTarget.ContainsKey(DragTargetHint.EdgeTop),
                                                                               _hintToTarget.ContainsKey(DragTargetHint.EdgeBottom),
                                                                               _hintToTarget.ContainsKey(DragTargetHint.Transfer));
                                break;
                            case PaletteDragFeedback.Square:
                            default:
                                _indicators = new DropDockingIndicatorsSquare(_paletteDragDrop, _renderer,
                                                                              _hintToTarget.ContainsKey(DragTargetHint.EdgeLeft),
                                                                              _hintToTarget.ContainsKey(DragTargetHint.EdgeRight),
                                                                              _hintToTarget.ContainsKey(DragTargetHint.EdgeTop),
                                                                              _hintToTarget.ContainsKey(DragTargetHint.EdgeBottom),
                                                                              _hintToTarget.ContainsKey(DragTargetHint.Transfer));
                                break;
                        }
                    }

                    // Ensure window is displayed in correct location
                    _indicators.ShowRelative(ScreenRect);

                    // Hit test against indicators and update display
                    switch (_indicators.ScreenMouseMove(screenPt))
                    {
                        case 0x0040:
                            return _hintToTarget[DragTargetHint.EdgeLeft];
                        case 0x0080:
                            return _hintToTarget[DragTargetHint.EdgeRight];
                        case 0x0100:
                            return _hintToTarget[DragTargetHint.EdgeTop];
                        case 0x0200:
                            return _hintToTarget[DragTargetHint.EdgeBottom];
                        case 0x0400:
                            return _hintToTarget[DragTargetHint.Transfer];
                        default:
                            // Mouse is not over any of the targets
                            return null;
                    }
                }
                else
                {
                    if (_indicators != null)
                    {
                        _indicators.MouseReset();
                        _indicators.Hide();
                    }

                    // Mouse is not over any of the targets
                    return null;
                }
            }
            #endregion
        }
        #endregion

        #region Type Definitons
        private class DockClusterList : List<DockCluster> { };
        #endregion

        #region Instance Fields
        private DropSolidWindow _solid;
        private DockClusterList _clusters;
        private PaletteDragFeedback _dragFeedback;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockCluster class.
        /// </summary>
        /// <param name="dragFeedback">Type of drag feedback required.</param>
        public DragFeedbackDocking(PaletteDragFeedback dragFeedback)
        {
            _clusters = new DockClusterList();
            _dragFeedback = dragFeedback;
        }

        /// <summary>
		/// Release unmanaged and optionally managed resources.
		/// </summary>
		/// <param name="disposing">Called from Dispose method.</param>
		protected override void Dispose(bool disposing)
		{
            if (disposing)
            {
                if (_solid != null)
                {
                    _solid.Dispose();
                    _solid = null;
                }

                ClearClusters();
            }

            base.Dispose(disposing);
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
        public override void Start(IPaletteDragDrop paletteDragDrop,
                                   IRenderer renderer,
                                   PageDragEndData pageDragEndData, 
                                   DragTargetList dragTargets)
        {
            base.Start(paletteDragDrop, renderer, pageDragEndData, dragTargets);

            if (_solid == null)
            {
                // Create and show a solid feedback window without it taking focus
                _solid = new DropSolidWindow(PaletteDragDrop, Renderer);
                _solid.SetBounds(0, 0, 1, 1, BoundsSpecified.All);
                _solid.ShowWithoutActivate();
                _solid.Refresh();
            }

            ClearClusters();

            // Create clusters of related drag targets
            foreach (DragTarget target in dragTargets)
            {
                // Check if the target is actually able to drop inside itself
                if (target.IsMatch(target.HotRect.Location, pageDragEndData))
                {
                    // Find the existing cluster for the targets screen rectangle
                    DockCluster cluster = FindTargetCluster(target);

                    // Is the target allowed to be added to the found cluster (if there is one found)
                    if ((cluster == null) || cluster.ExcludeCluster || ((target.Hint & DragTargetHint.ExcludeCluster) == DragTargetHint.ExcludeCluster))
                        _clusters.Add(new DockCluster(PaletteDragDrop, Renderer, target));
                    else
                        cluster.Add(target);
                }
            }
        }

        /// <summary>
        /// Called to request feedback be shown for the specified target.
        /// </summary>
        /// <param name="screenPt">Current screen point of mouse.</param>
        /// <param name="target">Target that needs feedback.</param>
        /// <returns>Updated drag target.</returns>
        public override DragTarget Feedback(Point screenPt, DragTarget target)
        {
            DragTarget matchTarget = null;
            
            // Update each cluster so it shows/hides docking indicators based on mouse position
            foreach (DockCluster cluster in _clusters)
            {
                DragTarget clusterTarget = cluster.Feedback(screenPt, _dragFeedback);

                // We use the first matching target found in a cluster
                if ((clusterTarget != null) && (matchTarget == null))
                    matchTarget = clusterTarget;
            }

            // Update the solid feedback rectangle with area of the specific target
            if (_solid != null)
                _solid.SolidRect = (matchTarget != null) ? matchTarget.DrawRect : Rectangle.Empty;

            return matchTarget;
        }

        /// <summary>
        /// Called to cleanup when dragging has finished.
        /// </summary>
        public override void Quit()
        {
            if (_solid != null)
            {
                _solid.Dispose();
                _solid = null;
            }

            ClearClusters();
            
            base.Quit();
        }
        #endregion

        #region Implementation
        private void ClearClusters()
        {
            // Must dispose each cluster as they contain unmanaged resources
            foreach (DockCluster cluster in _clusters)
                cluster.Dispose();

            _clusters.Clear();
        }

        private DockCluster FindTargetCluster(DragTarget target)
        {
            foreach (DockCluster cluster in _clusters)
                if (!cluster.ExcludeCluster && cluster.ScreenRect.Equals(target.ScreenRect))
                    return cluster;

            return null;
        }

        private DragTarget FindTarget(Point screenPt, PageDragEndData dragEndData)
        {
            // Nothing matches
            return null;
        }
        #endregion
    }
}
