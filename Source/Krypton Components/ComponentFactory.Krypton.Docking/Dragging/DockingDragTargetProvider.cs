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
using System.Collections.Generic;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Provides the set of drag targets relevent to the set of pages being moved.
    /// </summary>
    public class DockingDragTargetProvider : IDragTargetProvider
    {
        #region Instance Fields
        private KryptonDockingManager _manager;
        private KryptonPageCollection _pages;
        private KryptonFloatingWindow _floatingWindow;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DragTargetNull class.
        /// </summary>
        /// <param name="manager">Reference to docking manager.</param>
        /// <param name="floatingWindow">Reference to window being dragged.</param>
        /// <param name="pages">Reference to collection of pages to drag.</param>
        public DockingDragTargetProvider(KryptonDockingManager manager, 
                                         KryptonFloatingWindow floatingWindow,
                                         KryptonPageCollection pages)
        {
            _manager = manager;
            _floatingWindow = floatingWindow;
            _pages = pages;
        }
        #endregion

        #region Public
        /// <summary>
        /// Generate a list of drag targets that are relevant to the provided end data.
        /// </summary>
        /// <param name="dragEndData">Pages data being dragged.</param>
        /// <returns>List of drag targets.</returns>
        public DragTargetList GenerateDragTargets(PageDragEndData dragEndData)
        {
            DragTargetList targets = new DragTargetList();

            // Generate the set of targets from the element hierarchy
            _manager.PropogateDragTargets(_floatingWindow, dragEndData, targets);

            // Must have at least one target
            if (targets.Count == 0)
                targets.Add(new DragTargetNull());

            return targets;
        }
        #endregion
    }
}
