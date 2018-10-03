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

namespace ComponentFactory.Krypton.Workspace
{
    /// <summary>
    /// Target one of the four sides of the workspace control.
    /// </summary>
    public class DragTargetWorkspaceEdge : DragTargetWorkspace
    {
        #region Instance Fields
        private VisualOrientation _edge;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DragTargetWorkspaceEdge class.
        /// </summary>
        /// <param name="screenRect">Rectangle for screen area.</param>
        /// <param name="hotRect">Rectangle for hot area.</param>
        /// <param name="drawRect">Rectangle for draw area.</param>
        /// <param name="hint">Target hint which should be one of the edges.</param>
        /// <param name="workspace">Control instance for drop.</param>
        /// <param name="allowFlags">Only drop pages that have one of these flags defined.</param>
        public DragTargetWorkspaceEdge(Rectangle screenRect,
                                       Rectangle hotRect,
                                       Rectangle drawRect,
                                       DragTargetHint hint,
                                       KryptonWorkspace workspace,
                                       KryptonPageFlags allowFlags)
            : base(screenRect, hotRect, drawRect, hint, workspace, allowFlags)
        {
            // Find the orientation by looking for a matching hint (we need to exclude flags from the hint enum)
            switch (hint & DragTargetHint.ExcludeFlags)
            {
                case DragTargetHint.Transfer:
                case DragTargetHint.EdgeLeft:
                    _edge = VisualOrientation.Left;
                    break;
                case DragTargetHint.EdgeRight:
                    _edge = VisualOrientation.Right;
                    break;
                case DragTargetHint.EdgeTop:
                    _edge = VisualOrientation.Top;
                    break;
                case DragTargetHint.EdgeBottom:
                    _edge = VisualOrientation.Bottom;
                    break;
                default:
                    Debug.Assert(false);
                    throw new ArgumentOutOfRangeException("Hint must be an edge value.");
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the dragging edge.
        /// </summary>
        public VisualOrientation Edge
        {
            get { return _edge; }
        }

        /// <summary>
        /// Perform the drop action associated with the target.
        /// </summary>
        /// <param name="screenPt">Position in screen coordinates.</param>
        /// <param name="data">Data to pass to the target to process drop.</param>
        /// <returns>Drop was performed and the source can perform any removal of pages as required.</returns>
        public override bool PerformDrop(Point screenPt, PageDragEndData data)
        {
            // Transfer the dragged pages into a new cell
            KryptonWorkspaceCell cell = new KryptonWorkspaceCell();
            KryptonPage page = ProcessDragEndData(Workspace, cell, data);

            // If no pages are transferred then we do nothing and no longer need cell instance
            if (page == null)
                cell.Dispose();
            else
            {
                // If the root is not the same direction as that needed for the drop then...
                bool dropHorizontal = (Edge == VisualOrientation.Left) || (Edge == VisualOrientation.Right);
                if ((dropHorizontal && (Workspace.Root.Orientation == Orientation.Vertical)) ||
                    (!dropHorizontal && (Workspace.Root.Orientation == Orientation.Horizontal)))
                {
                    // Create a new sequence and place all existing root items into it
                    KryptonWorkspaceSequence sequence = new KryptonWorkspaceSequence(Workspace.Root.Orientation);
                    for (int i = Workspace.Root.Children.Count - 1; i >= 0; i--)
                    {
                        Component child = Workspace.Root.Children[i];
                        Workspace.Root.Children.RemoveAt(i);
                        sequence.Children.Insert(0, child);
                    }

                    // Put the new sequence in the root so all items are now grouped together
                    Workspace.Root.Children.Add(sequence);

                    // Switch the direction of the root
                    if (Workspace.Root.Orientation == Orientation.Horizontal)
                        Workspace.Root.Orientation = Orientation.Vertical;
                    else
                        Workspace.Root.Orientation = Orientation.Horizontal;
                }

                // Add to the start or the end of the root sequence?
                if ((Edge == VisualOrientation.Left) || (Edge == VisualOrientation.Top))
                    Workspace.Root.Children.Insert(0, cell);
                else
                    Workspace.Root.Children.Add(cell);

                // Make the last page transfer the newly selected page of the cell
                if (page != null)
                {
                    // Does the cell allow the selection of tabs?
                    if (cell.AllowTabSelect)
                        cell.SelectedPage = page;

                    // Need to layout so the new cell has been added as a child control and 
                    // therefore can receive the focus we want to give it immediately afterwards
                    Workspace.PerformLayout();

                    if (!cell.IsDisposed)
                    {
                        // Without this DoEvents() call the dropping of multiple pages in a complex arrangement causes an exception for
                        // a complex reason that is hard to work out (i.e. I'm not entirely sure). Something to do with using select to
                        // change activation is causing the source workspace control to dispose to earlier.
                        Application.DoEvents();
                        cell.Select();
                    }
                }
            }

            return true;
        }
        #endregion
    }
}
