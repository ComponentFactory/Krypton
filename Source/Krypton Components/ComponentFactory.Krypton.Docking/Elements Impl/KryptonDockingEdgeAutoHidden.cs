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
using System.IO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Provides auto hidden docking funtionality against a specific control edge.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockingEdgeAutoHidden : DockingElementClosedCollection
    {
        #region Static Fields
        private static readonly int CLIENT_MINIMUM = 22;
        #endregion

        #region Instance Fields
        private Control _control;
        private DockingEdge _edge;
        private KryptonAutoHiddenPanel _panel;
        private KryptonAutoHiddenSlidePanel _slidePanel;
        private bool _panelEventFired;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockingEdgeAutoHidden class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        /// <param name="control">Reference to control that is being managed.</param>
        /// <param name="edge">Docking edge being managed.</param>
        public KryptonDockingEdgeAutoHidden(string name, Control control, DockingEdge edge)
            : base(name)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            _control = control;
            _edge = edge;
            _panelEventFired = false;

            // Create and add the panel used to host auto hidden groups
            _panel = new KryptonAutoHiddenPanel(edge);
            _panel.AutoSize = true;
            _panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _panel.Dock = DockingHelper.DockStyleFromDockEdge(edge, false);
            _panel.Disposed += new EventHandler(OnPanelDisposed);

            // Create the panel that slides into/out of view to show selected auto host entry
            _slidePanel = new KryptonAutoHiddenSlidePanel(control, edge, _panel);
            _slidePanel.SplitterMoveRect += new EventHandler<SplitterMoveRectMenuArgs>(OnSlidePanelSeparatorMoveRect);
            _slidePanel.SplitterMoved += new SplitterEventHandler(OnSlidePanelSeparatorMoved);
            _slidePanel.SplitterMoving += new SplitterCancelEventHandler(OnSlidePanelSeparatorMoving);
            _slidePanel.PageCloseClicked += new EventHandler<UniqueNameEventArgs>(OnSlidePanelPageCloseClicked);
            _slidePanel.PageAutoHiddenClicked += new EventHandler<UniqueNameEventArgs>(OnSlidePanelPageAutoHiddenClicked);
            _slidePanel.PageDropDownClicked += new EventHandler<CancelDropDownEventArgs>(OnSlidePanelPageDropDownClicked);
            _slidePanel.AutoHiddenShowingStateChanged += new EventHandler<AutoHiddenShowingStateEventArgs>(OnSlidePanelAutoHiddenShowingStateChanged);
            _slidePanel.Disposed += new EventHandler(OnSlidePanelDisposed);

            Control.Controls.Add(_panel);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the control this element is managing.
        /// </summary>
        public Control Control
        {
            get { return _control; }
        }

        /// <summary>
        /// Gets the docking edge this element is managing.
        /// </summary>
        public DockingEdge Edge
        {
            get { return _edge; }
        }

        /// <summary>
        /// Create and add a new auto hidden group instance to the correct edge of the owning control.
        /// </summary>
        /// <returns>Reference to docking element that handles the new auto hidden group.</returns>
        public KryptonDockingAutoHiddenGroup AppendAutoHiddenGroup()
        {
            // Generate a unique string by creating a GUID
            return AppendAutoHiddenGroup(CommonHelper.UniqueString);
        }

        /// <summary>
        /// Create and add a new auto hidden group instance to the correct edge of the owning control.
        /// </summary>
        /// <param name="name">Initial name of the group element.</param>
        /// <returns>Reference to docking element that handles the new auto hidden group.</returns>
        public KryptonDockingAutoHiddenGroup AppendAutoHiddenGroup(string name)
        {
            return CreateAndInsertAutoHiddenGroup(Count, name);
        }

        /// <summary>
        /// Create and insert a new auto hidden group instance to the correct edge of the owning control.
        /// </summary>
        /// <param name="index">Insertion index.</param>
        /// <returns>Reference to docking element that handles the new auto hidden group.</returns>
        public KryptonDockingAutoHiddenGroup InsertAutoHiddenGroup(int index)
        {
            // Generate a unique string by creating a GUID
            return CreateAndInsertAutoHiddenGroup(index, CommonHelper.UniqueString);
        }

        /// <summary>
        /// Create and insert a new auto hidden group instance to the correct edge of the owning control.
        /// </summary>
        /// <param name="index">Insertion index.</param>
        /// <param name="name">Initial name of the group element.</param>
        /// <returns>Reference to docking element that handles the new auto hidden group.</returns>
        public KryptonDockingAutoHiddenGroup InsertAutoHiddenGroup(int index, string name)
        {
            return CreateAndInsertAutoHiddenGroup(index, name);
        }

        /// <summary>
        /// Propogates an action request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="action">Action that is requested to be performed.</param>
        /// <param name="uniqueNames">Array of unique names of the pages the action relates to.</param>
        public override void PropogateAction(DockingPropogateAction action, string[] uniqueNames)
        {
            switch (action)
            {
                case DockingPropogateAction.HidePages:
                case DockingPropogateAction.RemovePages:
                case DockingPropogateAction.RemoveAndDisposePages:
                case DockingPropogateAction.StorePages:
                    // Ask the sliding panel to remove its display if an incoming name matches
                    foreach (string uniqueName in uniqueNames)
                        _slidePanel.HideUniqueName(uniqueName);
                    break;
                case DockingPropogateAction.Loading:
                case DockingPropogateAction.HideAllPages:
                case DockingPropogateAction.RemoveAllPages:
                case DockingPropogateAction.RemoveAndDisposeAllPages:
                case DockingPropogateAction.StoreAllPages:
                    // Remove any slide out page
                    _slidePanel.HideUniqueName();
                    break;
                case DockingPropogateAction.StringChanged:
                    // Pushed changed strings to the tooltips
                    KryptonDockingManager dockingManager = DockingManager;
                    if (dockingManager != null)
                    {
                        _slidePanel.DockspaceControl.PinTooltip = dockingManager.Strings.TextDock;
                        _slidePanel.DockspaceControl.CloseTooltip = dockingManager.Strings.TextClose;
                        _slidePanel.DockspaceControl.DropDownTooltip = dockingManager.Strings.TextWindowLocation;
                    }
                    break;
            }

            // Let base class perform standard processing
            base.PropogateAction(action, uniqueNames);
        }

        /// <summary>
        /// Propogates an action request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="action">Action that is requested to be performed.</param>
        /// <param name="pages">Array of pages the action relates to.</param>
        public override void PropogateAction(DockingPropogateAction action, KryptonPage[] pages)
        {
            switch (action)
            {
                case DockingPropogateAction.RestorePages:
                    // Ask the sliding panel to remove its display if an incoming name matches
                    foreach (KryptonPage page in pages)
                        _slidePanel.HideUniqueName(page.UniqueName);
                    break;
            }

            // Let base class perform standard processing
            base.PropogateAction(action, pages);
        }

        /// <summary>
        /// Find a edge auto hidden element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable auto hidden edge element is required.</param>
        /// <returns>KryptonDockingEdgeAutoHidden reference if found; otherwise false.</returns>
        public override KryptonDockingEdgeAutoHidden FindDockingEdgeAutoHidden(string uniqueName)
        {
            return this;
        }

        /// <summary>
        /// Slide the specified page into view and optionally select.
        /// </summary>
        /// <param name="page">Page to slide into view.</param>
        /// <param name="select">True to select the page; otherwise false.</param>
        public void SlidePageOut(KryptonPage page, bool select)
        {
            SlidePageOut(page.UniqueName, select);
        }

        /// <summary>
        /// Slide the specified page into view and optionally select.
        /// </summary>
        /// <param name="uniqueName">Name of page to slide into view.</param>
        /// <param name="select">True to select the page; otherwise false.</param>
        public void SlidePageOut(string uniqueName, bool select)
        {
            // Search each of our AutoHiddenGroup entries
            for (int i = 0; i < Count; i++)
            {
                KryptonDockingAutoHiddenGroup ahg = this[i] as KryptonDockingAutoHiddenGroup;
                if (ahg != null)
                {
                    // If the target page is inside this group
                    KryptonPage page = ahg.AutoHiddenGroupControl.Pages[uniqueName];
                    if (page != null)
                    {
                        // Request the sliding panel slide itself into view with the provided page
                        KryptonAutoHiddenProxyPage proxyPage = (KryptonAutoHiddenProxyPage)page;
                        _slidePanel.SlideOut(proxyPage.Page, ahg.AutoHiddenGroupControl, select);
                        break;
                    }
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the xml element name to use when saving.
        /// </summary>
        protected override string XmlElementName 
        {
            get { return "DEAH"; }
        }

        /// <summary>
        /// Perform docking element specific actions for loading a child xml.
        /// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="pages">Collection of available pages.</param>
        /// <param name="child">Optional reference to existing child docking element.</param>
        protected override void LoadChildDockingElement(XmlReader xmlReader,
                                                        KryptonPageCollection pages,
                                                        IDockingElement child)
        {
            if (child != null)
                child.LoadElementFromXml(xmlReader, pages);
            else
            {
                // Create a new auto hidden group and then reload it
                KryptonDockingAutoHiddenGroup autoHiddenGroup = AppendAutoHiddenGroup(xmlReader.GetAttribute("N"));
                autoHiddenGroup.LoadElementFromXml(xmlReader, pages);
            }
        }
        #endregion

        #region Implementation
        private KryptonDockingAutoHiddenGroup CreateAndInsertAutoHiddenGroup(int index, string name)
        {
            // Create the new auto hidden group instance and add into our collection
            KryptonDockingAutoHiddenGroup groupElement = new KryptonDockingAutoHiddenGroup(name, Edge);
            groupElement.PageClicked += new EventHandler<KryptonPageEventArgs>(OnDockingAutoHiddenGroupClicked);
            groupElement.PageHoverStart += new EventHandler<KryptonPageEventArgs>(OnDockingAutoHiddenGroupHoverStart);
            groupElement.PageHoverEnd += new EventHandler<EventArgs>(OnDockingAutoHiddenGroupHoverEnd);
            groupElement.Disposed += new EventHandler(OnDockingAutoHiddenGroupDisposed);
            InternalInsert(index, groupElement);

            // Events are generated from the parent docking manager
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                // The hosting panel/sliding panel dockspace/separator are not shown until the first group is added, so only 
                // generate the events for allowing customization of the when there is a chance they will become displayed.
                if (!_panelEventFired)
                {
                    AutoHiddenGroupPanelEventArgs panelArgs = new AutoHiddenGroupPanelEventArgs(_panel, this);
                    DockspaceEventArgs dockspaceArgs = new DockspaceEventArgs(_slidePanel.DockspaceControl, null);
                    DockspaceSeparatorEventArgs separatorArgs = new DockspaceSeparatorEventArgs(_slidePanel.SeparatorControl, null);
                    dockingManager.RaiseAutoHiddenGroupPanelAdding(panelArgs);
                    dockingManager.RaiseDockspaceAdding(dockspaceArgs);
                    dockingManager.RaiseDockspaceSeparatorAdding(separatorArgs);
                    _panelEventFired = true;
                }

                // Allow the auto hidden group to be customized by event handlers
                AutoHiddenGroupEventArgs groupArgs = new AutoHiddenGroupEventArgs(groupElement.AutoHiddenGroupControl, groupElement);
                dockingManager.RaiseAutoHiddenGroupAdding(groupArgs);
            }

            // Append to the child collection of groups
            _panel.Controls.Add(groupElement.AutoHiddenGroupControl);
            _panel.Controls.SetChildIndex(groupElement.AutoHiddenGroupControl, (_panel.Controls.Count - 1) - index);

            // When adding the first group the panel will not be visible and so we need to 
            // force the calculation of a new size so it makes itself visible.
            _panel.PerformLayout();

            return groupElement;
        }

        private void OnDockingAutoHiddenGroupDisposed(object sender, EventArgs e)
        {
            // Cast to correct type and unhook event handlers so garbage collection can occur
            KryptonDockingAutoHiddenGroup groupElement = (KryptonDockingAutoHiddenGroup)sender;
            groupElement.PageClicked -= new EventHandler<KryptonPageEventArgs>(OnDockingAutoHiddenGroupClicked);
            groupElement.PageHoverStart -= new EventHandler<KryptonPageEventArgs>(OnDockingAutoHiddenGroupHoverStart);
            groupElement.PageHoverEnd -= new EventHandler<EventArgs>(OnDockingAutoHiddenGroupHoverEnd);
            groupElement.Disposed -= new EventHandler(OnDockingAutoHiddenGroupDisposed);

            // Remove the element from our child collection as it is no longer valid
            InternalRemove(groupElement);
        }

        private void OnPanelDisposed(object sender, EventArgs e)
        {
            // Unhook from events so the control can be garbage collected
            _panel.Disposed -= new EventHandler(OnPanelDisposed);

            // Events are generated from the parent docking manager
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                // Only generate the removed event if we have already fired the adding event.
                if (_panelEventFired)
                {
                    AutoHiddenGroupPanelEventArgs panelArgs = new AutoHiddenGroupPanelEventArgs(_panel, this);
                    dockingManager.RaiseAutoHiddenGroupPanelRemoved(panelArgs);
                }
            }

            // Make sure the sliding panel is also disposed
            if (!_slidePanel.IsDisposed)
                _slidePanel.Dispose();
        }

        private void OnSlidePanelDisposed(object sender, EventArgs e)
        {
            // Unhook from events so the control can be garbage collected
            _slidePanel.SplitterMoveRect -= new EventHandler<SplitterMoveRectMenuArgs>(OnSlidePanelSeparatorMoveRect);
            _slidePanel.SplitterMoved -= new SplitterEventHandler(OnSlidePanelSeparatorMoved);
            _slidePanel.SplitterMoving -= new SplitterCancelEventHandler(OnSlidePanelSeparatorMoving);
            _slidePanel.PageCloseClicked -= new EventHandler<UniqueNameEventArgs>(OnSlidePanelPageCloseClicked);
            _slidePanel.PageAutoHiddenClicked -= new EventHandler<UniqueNameEventArgs>(OnSlidePanelPageAutoHiddenClicked);
            _slidePanel.PageDropDownClicked -= new EventHandler<CancelDropDownEventArgs>(OnSlidePanelPageDropDownClicked);
            _slidePanel.Disposed -= new EventHandler(OnPanelDisposed);

            // Events are generated from the parent docking manager
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                // Only generate the removed event if we have already fired the adding event.
                if (_panelEventFired)
                {
                    DockspaceEventArgs dockspaceArgs = new DockspaceEventArgs(_slidePanel.DockspaceControl, null);
                    DockspaceSeparatorEventArgs separatorArgs = new DockspaceSeparatorEventArgs(_slidePanel.SeparatorControl, null);
                    dockingManager.RaiseDockspaceRemoved(dockspaceArgs);
                    dockingManager.RaiseDockspaceSeparatorRemoved(separatorArgs);
                }
            }

            // Make sure the groups panel is also disposed
            if (!_panel.IsDisposed)
                _panel.Dispose();
        }

        private void OnDockingAutoHiddenGroupClicked(object sender, KryptonPageEventArgs e)
        {
            // Request the sliding panel slide itself into view with the provided page
            KryptonDockingAutoHiddenGroup dockingGroup = (KryptonDockingAutoHiddenGroup)sender;
            _slidePanel.SlideOut(e.Item, dockingGroup.AutoHiddenGroupControl, true);
        }

        private void OnDockingAutoHiddenGroupHoverStart(object sender, KryptonPageEventArgs e)
        {
            // Request the sliding panel slide itself into view with the provided page
            KryptonDockingAutoHiddenGroup dockingGroup = (KryptonDockingAutoHiddenGroup)sender;
            _slidePanel.SlideOut(e.Item, dockingGroup.AutoHiddenGroupControl, false);
        }

        private void OnDockingAutoHiddenGroupHoverEnd(object sender, EventArgs e)
        {
            // Request the sliding panel slide itself out of view when appropriate
            // (will not retract whilst the mouse is over the slide out dockspace)
            // (will not retract whilst slide out dockspace has the focus)
            _slidePanel.SlideIn();
        }

        private void OnSlidePanelSeparatorMoved(object sender, SplitterEventArgs e)
        {
            _slidePanel.UpdateSize(e.SplitX, e.SplitY);
        }

        private void OnSlidePanelSeparatorMoving(object sender, SplitterCancelEventArgs e)
        {
        }

        private void OnSlidePanelSeparatorMoveRect(object sender, SplitterMoveRectMenuArgs e)
        {
            // Cast to correct type and grab associated dockspace control
            KryptonDockspaceSeparator separatorControl = (KryptonDockspaceSeparator)sender;
            KryptonDockspace dockspaceControl = _slidePanel.DockspaceControl;
            KryptonPage page = _slidePanel.Page;

            // Events are generated from the parent docking manager
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                // Allow the movement rectangle to be modified by event handlers
                AutoHiddenSeparatorResizeEventArgs autoHiddenSeparatorResizeRectArgs = new AutoHiddenSeparatorResizeEventArgs(separatorControl, dockspaceControl, page, FindMovementRect(e.MoveRect));
                dockingManager.RaiseAutoHiddenSeparatorResize(autoHiddenSeparatorResizeRectArgs);
                e.MoveRect = autoHiddenSeparatorResizeRectArgs.ResizeRect;
            }
        }

        private void OnSlidePanelPageCloseClicked(object sender, UniqueNameEventArgs e)
        {
            // Generate event so that the close action is handled for the named page
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
                dockingManager.CloseRequest(new string[] { e.UniqueName });
        }

        private void OnSlidePanelPageAutoHiddenClicked(object sender, UniqueNameEventArgs e)
        {
            // Generate event so that the auto hidden is switched to docked is handled for the group that contains the named page
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
                dockingManager.SwitchAutoHiddenGroupToDockedCellRequest(e.UniqueName);
        }

        private void OnSlidePanelPageDropDownClicked(object sender, CancelDropDownEventArgs e)
        {
            // Generate event so that the appropriate context menu options are preseted and actioned
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
                e.Cancel = !dockingManager.ShowPageContextMenuRequest(e.Page, e.KryptonContextMenu);
        }

        private void OnSlidePanelAutoHiddenShowingStateChanged(object sender, AutoHiddenShowingStateEventArgs e)
        {
            // Generate event so that the appropriate context menu options are preseted and actioned
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
                dockingManager.RaiseAutoHiddenShowingStateChanged(e);
        }

        private Rectangle FindMovementRect(Rectangle moveRect)
        {
            // We never allow the entire client area to covered, so reduce by a fixed size
            Size innerSize = Control.ClientRectangle.Size;
            innerSize.Width -= CLIENT_MINIMUM;
            innerSize.Height -= CLIENT_MINIMUM;

            // Adjust for any showing auto hidden panels at the edges
            foreach (Control child in Control.Controls)
            {
                if (child.Visible && child is KryptonAutoHiddenPanel)
                {
                    switch (child.Dock)
                    {
                        case DockStyle.Left:
                        case DockStyle.Right:
                            innerSize.Width -= child.Width;
                            break;
                        case DockStyle.Top:
                        case DockStyle.Bottom:
                            innerSize.Height -= child.Height;
                            break;
                    }
                }
            }

            // How much can we reduce the width/height of the dockspace to reach the minimum
            Size dockspaceMinimum = _slidePanel.DockspaceControl.MinimumSize;
            int reduceWidth = Math.Max(_slidePanel.DockspaceControl.Width - dockspaceMinimum.Width, 0);
            int reduceHeight = Math.Max(_slidePanel.DockspaceControl.Height - dockspaceMinimum.Height, 0);

            // How much can we expand the width/height of the dockspace to reach the inner minimum
            int expandWidth = Math.Max(innerSize.Width - _slidePanel.Width, 0);
            int expandHeight = Math.Max(innerSize.Height - _slidePanel.Height, 0);

            // Create movement rectangle based on the initial rectangle and the allowed range
            Rectangle retRect = Rectangle.Empty;
            switch (Edge)
            {
                case DockingEdge.Left:
                    retRect = new Rectangle(moveRect.X - reduceWidth, moveRect.Y, moveRect.Width + reduceWidth + expandWidth, moveRect.Height);
                    break;
                case DockingEdge.Right:
                    retRect = new Rectangle(moveRect.X - expandWidth, moveRect.Y, moveRect.Width + reduceWidth + expandWidth, moveRect.Height);
                    break;
                case DockingEdge.Top:
                    retRect = new Rectangle(moveRect.X, moveRect.Y - reduceHeight, moveRect.Width, moveRect.Height + reduceHeight + expandHeight);
                    break;
                case DockingEdge.Bottom:
                    retRect = new Rectangle(moveRect.X, moveRect.Y - expandHeight, moveRect.Width, moveRect.Height + reduceHeight + expandHeight);
                    break;
            }

            // We do not allow negative width/height
            retRect.Width = Math.Max(retRect.Width, 0);
            retRect.Height = Math.Max(retRect.Height, 0);

            return retRect;
        }
        #endregion
    }
}
