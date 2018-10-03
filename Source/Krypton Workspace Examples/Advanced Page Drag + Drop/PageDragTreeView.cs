// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, PO Box 1504, 
//  Glen Waverley, Vic 3150, Australia and are supplied subject to licence terms.
// 
//  Version 4.6.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace AdvancedPageDragAndDrop
{
    /// <summary>
    /// TreeView customized to work with KryptonPage drag and drop.
    /// </summary>
    public class PageDragTreeView : TreeView,
                                    IDragTargetProvider
    {
        #region Classes
        public class DragTargetTreeViewTransfer : DragTarget
        {
            #region Instance Fields
            private PageDragTreeView _treeView;
            #endregion

            #region Identity
            /// <summary>
            /// Initialize a new instance of the DragTargetTreeViewTransfer class.
            /// </summary>
            /// <param name="rect">Rectangle for hot and draw areas.</param>
            /// <param name="navigator">Control instance for drop.</param>
            public DragTargetTreeViewTransfer(Rectangle rect,
                                              PageDragTreeView treeView)
                : base(rect, rect, rect, DragTargetHint.Transfer, KryptonPageFlags.All)
            {
                _treeView = treeView;
            }

            /// <summary>
            /// Release unmanaged and optionally managed resources.
            /// </summary>
            /// <param name="disposing">Called from Dispose method.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    _treeView = null;

                base.Dispose(disposing);
            }
            #endregion

            #region Public
            /// <summary>
            /// Is this target a match for the provided screen position.
            /// </summary>
            /// <param name="screenPt">Position in screen coordinates.</param>
            /// <param name="dragEndData">Data to be dropped at destination.</param>
            /// <returns>True if a match; otherwise false.</returns>
            public override bool IsMatch(Point screenPt, PageDragEndData dragEndData)
            {
                // Cannot drag back to ourself
                if ((dragEndData.Source != null) && 
                    (dragEndData.Source is PageDragTreeView) && 
                    (dragEndData.Source == _treeView))
                    return false;
                else
                    return base.IsMatch(screenPt, dragEndData);
            }

            /// <summary>
            /// Perform the drop action associated with the target.
            /// </summary>
            /// <param name="screenPt">Position in screen coordinates.</param>
            /// <param name="data">Data to pass to the target to process drop.</param>
            /// <returns>True if the drop was performed and the source can remove any pages.</returns>
            public override bool PerformDrop(Point screenPt, PageDragEndData data)
            {
                // Create a node for each page
                foreach (KryptonPage page in data.Pages)
                {
                    // Create node and populate with page details
                    TreeNode node = new TreeNode();
                    node.Text = page.Text;
                    node.ImageIndex = int.Parse((string)page.Tag);
                    node.SelectedImageIndex = node.ImageIndex;
                    node.Tag = page.Tag;

                    // Add to end of root nodes
                    _treeView.Nodes.Add(node);
                }

                // Take focus and select the last node added
                if (_treeView.Nodes.Count > 0)
                {
                    _treeView.SelectedNode = _treeView.Nodes[_treeView.Nodes.Count - 1];
                    _treeView.Select();
                }

                return true;
            }
            #endregion
        }
        #endregion

        #region Instance Fields
        private bool _movePages;
        private bool _dragging;
        private TreeNode _dragNode;
        private Rectangle _dragRect;
        private KryptonPage _dragPage;
        private IDragPageNotify _dragPageNotify;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PageDragTreeView class.
        /// </summary>
        public PageDragTreeView()
        {
            _movePages = true;
        }
        #endregion

        #region Public
        /// <summary>
        /// Determines if nodes are removed from successfully dragged away.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if nodes are removed from successfully dragged away.")]
        [DefaultValue(true)]
        public bool RemovePages
        {
            get { return _movePages; }
            set { _movePages = value; }
        }

        /// <summary>
        /// Gets and sets the interface for receiving page drag notifications.
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDragPageNotify DragPageNotify
        {
            get { return _dragPageNotify; }
            set { _dragPageNotify = value; }
        }

        /// <summary>
        /// Generate a list of drag targets that are relevant to the provided end data.
        /// </summary>
        /// <param name="dragEndData">Pages data being dragged.</param>
        /// <returns>List of drag targets.</returns>
        public DragTargetList GenerateDragTargets(PageDragEndData dragEndData)
        {
            DragTargetList targets = new DragTargetList();

            // Generate target for the entire navigator client area
            targets.Add(new DragTargetTreeViewTransfer(RectangleToScreen(ClientRectangle), this));

            return targets;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the MouseDown event.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Grab the node under the mouse
            Point pt = new Point(e.X, e.Y);
            TreeNode nodeDown = GetNodeAt(pt);

            // Try and ensure the node is selected on the mouse down
            if ((nodeDown != null) && (SelectedNode != nodeDown))
                SelectedNode = nodeDown;

            // Mouse down could be a prelude to a drag operation
            if (e.Button == MouseButtons.Left)
            {
                // Remember the node as a potential drag node
                _dragNode = nodeDown;

                // Create the rectangle that moving outside causes a drag operation
                _dragRect = new Rectangle(pt, Size.Empty);
                _dragRect.Inflate(SystemInformation.DragSize);
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the MouseMove event.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);

            // Are we monitoring for drag operations?
            if (_dragNode != null)
            {
                // If currently dragging
                if (Capture && _dragging)
                    PageDragMove(pt);
                else if (!_dragRect.Contains(pt))
                    PageDragStart(pt);
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Raises the MouseUp event.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_dragging)
            {
                if (e.Button == MouseButtons.Left)
                    PageDragEnd(new Point(e.X, e.Y));
                else
                    PageDragQuit();
            }

            // Any released mouse menas we have ended drag monitoring
            _dragNode = null;

            base.OnMouseUp(e);
        }
        #endregion

        #region Implementation
        private void PageDragStart(Point pt)
        {
            if (DragPageNotify != null)
            {
                // Create a page that will be dragged
                _dragPage = new KryptonPage();
                _dragPage.Text = _dragNode.Text;
                _dragPage.TextTitle = _dragNode.Text + " Title";
                _dragPage.TextDescription = _dragNode.Text + " Description";
                _dragPage.ImageSmall = ImageList.Images[int.Parse((string)_dragNode.Tag)];
                _dragPage.Tag = _dragNode.Tag;

                // Create a rich text box with some sample text inside
                KryptonRichTextBox rtb = new KryptonRichTextBox();
                rtb.Text = "This page (" + _dragPage.Text + ") contains a rich text box control as example content.";
                rtb.Dock = DockStyle.Fill;
                rtb.StateCommon.Border.Draw = InheritBool.False;

                // Add rich text box as the contents of the page
                _dragPage.Padding = new Padding(5);
                _dragPage.Controls.Add(rtb);

                // Give the notify interface a chance to reject the attempt to drag
                PageDragCancelEventArgs de = new PageDragCancelEventArgs(PointToScreen(pt), Point.Empty, this, new KryptonPage[] { _dragPage });
                DragPageNotify.PageDragStart(this, null, de);

                if (de.Cancel)
                {
                    // No longer need the temporary drag page
                    _dragPage.Dispose();
                    _dragPage = null;
                }
                else
                {
                    _dragging = true;
                    Capture = true;
                }
            }
        }

        private void PageDragMove(Point pt)
        {
            if (DragPageNotify != null)
                DragPageNotify.PageDragMove(this, new PointEventArgs(PointToScreen(pt)));
        }

        private void PageDragEnd(Point pt)
        {
            if (DragPageNotify != null)
            {
                // Let the target transfer the page across
                if (DragPageNotify.PageDragEnd(this, new PointEventArgs(PointToScreen(pt))))
                {
                    // Should we remove the page that can been transferred
                    if (RemovePages)
                        Nodes.Remove(_dragNode);
                }

                // Transfered the page to the target, so do not dispose it
                _dragPage = null;

                // No longer dragging
                _dragging = false;
                Capture = false;
            }
        }

        private void PageDragQuit()
        {
            if (DragPageNotify != null)
            {
                DragPageNotify.PageDragQuit(this);

                // Did not transfer the page to the target, so dispose it
                _dragPage.Dispose();
                _dragPage = null;

                // No longer dragging
                _dragging = false;
                Capture = false;
            }
        }
        #endregion
    }
}
