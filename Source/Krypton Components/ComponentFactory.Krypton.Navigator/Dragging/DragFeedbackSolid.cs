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
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Provides drag feedback as solid windows overlaying hot areas.
    /// </summary>
    public class DragFeedbackSolid : DragFeedback
    {
        #region Instance Fields
        private DropSolidWindow _solid;
        #endregion

        #region Identity
		/// <summary>
		/// Release unmanaged and optionally managed resources.
		/// </summary>
		/// <param name="disposing">Called from Dispose method.</param>
		protected override void Dispose(bool disposing)
		{
            if (_solid != null)
            {
                _solid.Dispose();
                _solid = null;
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
                // Create and show a window without it taking focus
                _solid = new DropSolidWindow(PaletteDragDrop, Renderer);
                _solid.SetBounds(0, 0, 1, 1, BoundsSpecified.All);
                _solid.ShowWithoutActivate();
                _solid.Refresh();
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
            // If the current target no longer matches the new point, we need a new target.
            if ((target != null) && !target.IsMatch(screenPt, PageDragEndData))
                target = null;

            // Only find a new target if we do not already have a target
            if (target == null)
                target = FindTarget(screenPt, PageDragEndData);

            if (_solid != null)
                _solid.SolidRect = (target != null) ? target.DrawRect : Rectangle.Empty;

            return target;
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

            base.Quit();
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Find the target the first matches the provided screen point.
        /// </summary>
        /// <param name="screenPt">Point in screen coordinates.</param>
        /// <param name="dragEndData">Data to be dropped at destination.</param>
        /// <returns>First target that matches; otherwise null.</returns>
        protected virtual DragTarget FindTarget(Point screenPt, PageDragEndData dragEndData)
        {
            // Ask each target in turn if they are a match for the given screen point
            foreach (DragTarget target in DragTargets)
                if (target.IsMatch(screenPt, dragEndData))
                    return target;

            // Nothing matches
            return null;
        }
        #endregion
    }
}
