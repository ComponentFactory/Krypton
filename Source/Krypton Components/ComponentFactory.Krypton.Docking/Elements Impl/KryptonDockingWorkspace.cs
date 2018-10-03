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
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Workspace;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Provides docking functionality by attaching to an existing KryptonDockableWorkspace
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockingWorkspace : KryptonDockingSpace
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockingWorkspace class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        public KryptonDockingWorkspace(string name)
            : this(name, "Workspace", new KryptonDockableWorkspace())
        {
        }

        /// <summary>
        /// Initialize a new instance of the KryptonDockingWorkspace class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        /// <param name="storeName">Name to use for storage pages.</param>
        /// <param name="workspace">Reference to workspace to manage.</param>
        public KryptonDockingWorkspace(string name,
                                       string storeName,
                                       KryptonDockableWorkspace workspace)
            : base(name, storeName)
        {
            if (workspace == null)
                throw new ArgumentNullException("workspace");

            SpaceControl = workspace;

            DockableWorkspaceControl.CellPageInserting += new EventHandler<KryptonPageEventArgs>(OnSpaceCellPageInserting);
            DockableWorkspaceControl.BeforePageDrag += new EventHandler<PageDragCancelEventArgs>(OnDockableWorkspaceBeforePageDrag);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the control this element is managing.
        /// </summary>
        public KryptonDockableWorkspace DockableWorkspaceControl
        {
            get { return (KryptonDockableWorkspace)SpaceControl; }
        }    

        /// <summary>
        /// Gets and sets access to the parent docking element.
        /// </summary>
        public override IDockingElement Parent 
        {
            set 
            {
                // Let base class perform standard processing
                base.Parent = value;

                // Generate event so the any dockable workspace customization can be performed.
                KryptonDockingManager dockingManager = DockingManager;
                if (dockingManager != null)
                {
                    DockableWorkspaceEventArgs args = new DockableWorkspaceEventArgs(DockableWorkspaceControl, this);
                    dockingManager.RaiseDockableWorkspaceAdded(args);
                }
            }
        }

        /// <summary>
        /// Show all display elements of the provided page.
        /// </summary>
        /// <param name="page">Reference to page that should be shown.</param>
        public void ShowPage(KryptonPage page)
        {
            // Cannot show a null reference
            if (page == null)
                throw new ArgumentNullException("page");

            ShowPages(new string[] { page.UniqueName });
        }

        /// <summary>
        /// Show all display elements of the provided page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page that should be shown.</param>
        public void ShowPage(string uniqueName)
        {
            // Cannot show a null reference
            if (uniqueName == null)
                throw new ArgumentNullException("uniqueName");

            ShowPages(new string[] { uniqueName });
        }

        /// <summary>
        /// Show all display elements of the provided pages.
        /// </summary>
        /// <param name="pages">Array of references to pages that should be shown.</param>
        public void ShowPages(KryptonPage[] pages)
        {
            // Cannot show a null reference
            if (pages == null)
                throw new ArgumentNullException("pages");

            if (pages.Length > 0)
            {
                string[] uniqueNames = new string[pages.Length];
                for (int i = 0; i < uniqueNames.Length; i++)
                {
                    // Cannot show a null page reference
                    if (pages[i] == null)
                        throw new ArgumentException("pages array contains a null page reference");

                    uniqueNames[i] = pages[i].UniqueName;
                }

                ShowPages(uniqueNames);
            }
        }

        /// <summary>
        /// Show all display elements of the provided pages.
        /// </summary>
        /// <param name="uniqueNames">Array of unique names of the pages that should be shown.</param>
        public void ShowPages(string[] uniqueNames)
        {
            // Cannot show a null reference
            if (uniqueNames == null)
                throw new ArgumentNullException("uniqueNames");

            if (uniqueNames.Length > 0)
            {
                // Cannot show a null or zero length unique name
                foreach (string uniqueName in uniqueNames)
                {
                    if (uniqueName == null)
                        throw new ArgumentNullException("uniqueNames array contains a null string reference");

                    if (uniqueName.Length == 0)
                        throw new ArgumentException("uniqueNames array contains a zero length string");
                }

                using (DockingMultiUpdate update = new DockingMultiUpdate(this))
                    base.PropogateAction(DockingPropogateAction.ShowPages, uniqueNames);
            }
        }

        /// <summary>
        /// Show all display elements of all pages.
        /// </summary>
        public void ShowAllPages()
        {
            using (DockingMultiUpdate update = new DockingMultiUpdate(this))
                base.PropogateAction(DockingPropogateAction.ShowAllPages, (string[])null);
        }

        /// <summary>
        /// Hide all display elements of the provided page.
        /// </summary>
        /// <param name="page">Reference to page that should be hidden.</param>
        public void HidePage(KryptonPage page)
        {
            // Cannot hide a null reference
            if (page == null)
                throw new ArgumentNullException("page");

            HidePages(new string[] { page.UniqueName });
        }

        /// <summary>
        /// Hide all display elements of the provided page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page that should be hidden.</param>
        public void HidePage(string uniqueName)
        {
            // Cannot hide a null reference
            if (uniqueName == null)
                throw new ArgumentNullException("uniqueName");

            if (uniqueName.Length > 0)
                HidePages(new string[] { uniqueName });
        }

        /// <summary>
        /// Hide all display elements of the provided pages.
        /// </summary>
        /// <param name="pages">Array of references to pages that should be hidden.</param>
        public void HidePages(KryptonPage[] pages)
        {
            // Cannot hide a null reference
            if (pages == null)
                throw new ArgumentNullException("pages");

            if (pages.Length > 0)
            {
                // Cannot hide a null page reference
                string[] uniqueNames = new string[pages.Length];
                for (int i = 0; i < uniqueNames.Length; i++)
                {
                    // Cannot show a null page reference
                    if (pages[i] == null)
                        throw new ArgumentException("pages array contains a null page reference");

                    uniqueNames[i] = pages[i].UniqueName;
                }

                HidePages(uniqueNames);
            }
        }

        /// <summary>
        /// Hide all display elements of the provided pages.
        /// </summary>
        /// <param name="uniqueNames">Array of unique names of the pages that should be hidden.</param>
        public void HidePages(string[] uniqueNames)
        {
            // Cannot hide a null reference
            if (uniqueNames == null)
                throw new ArgumentNullException("uniqueNames");

            if (uniqueNames.Length > 0)
            {
                // Cannot hide a null or zero length unique name
                foreach (string uniqueName in uniqueNames)
                {
                    if (uniqueName == null)
                        throw new ArgumentNullException("uniqueNames array contains a null string reference");

                    if (uniqueName.Length == 0)
                        throw new ArgumentException("uniqueNames array contains a zero length string");
                }

                using (DockingMultiUpdate update = new DockingMultiUpdate(this))
                    base.PropogateAction(DockingPropogateAction.HidePages, uniqueNames);
            }
        }

        /// <summary>
        /// Hide all display elements of all pages.
        /// </summary>
        public void HideAllPages()
        {
            using (DockingMultiUpdate update = new DockingMultiUpdate(this))
                base.PropogateAction(DockingPropogateAction.HideAllPages, (string[])null);
        }

        /// <summary>
        /// Remove the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page that should be removed.</param>
        /// <param name="disposePage">Should the page be disposed when removed.</param>
        public void RemovePage(string uniqueName, bool disposePage)
        {
            // Cannot remove a null reference
            if (uniqueName == null)
                throw new ArgumentNullException("uniqueName");

            // Unique names cannot be zero length
            if (uniqueName.Length == 0)
                throw new ArgumentException("uniqueName cannot be zero length");

            RemovePages(new string[] { uniqueName }, disposePage);
        }

        /// <summary>
        /// Remove the referenced pages.
        /// </summary>
        /// <param name="pages">Array of references to pages that should be removed.</param>
        /// <param name="disposePage">Should the page be disposed when removed.</param>
        public void RemovePages(KryptonPage[] pages, bool disposePage)
        {
            // Cannot remove a null reference
            if (pages == null)
                throw new ArgumentNullException("pages");

            if (pages.Length > 0)
            {
                // Cannot remove a null page reference
                string[] uniqueNames = new string[pages.Length];
                for (int i = 0; i < uniqueNames.Length; i++)
                {
                    // Cannot show a null page reference
                    if (pages[i] == null)
                        throw new ArgumentException("pages array contains a null page reference");

                    uniqueNames[i] = pages[i].UniqueName;
                }

                RemovePages(uniqueNames, disposePage);
            }
        }

        /// <summary>
        /// Remove the named pages.
        /// </summary>
        /// <param name="uniqueNames">Array of unique names of the pages that should be removed.</param>
        /// <param name="disposePage">Should the page be disposed when removed.</param>
        public void RemovePages(string[] uniqueNames, bool disposePage)
        {
            // Cannot remove a null reference
            if (uniqueNames == null)
                throw new ArgumentNullException("uniqueNames");

            if (uniqueNames.Length > 0)
            {
                // Cannot remove a null or zero length unique name
                foreach (string uniqueName in uniqueNames)
                {
                    if (uniqueName == null)
                        throw new ArgumentNullException("uniqueNames array contains a null string reference");

                    if (uniqueName.Length == 0)
                        throw new ArgumentException("uniqueNames array contains a zero length string");
                }

                // Remove page details from all parts of the hierarchy
                using (DockingMultiUpdate update = new DockingMultiUpdate(this))
                    base.PropogateAction(disposePage ? DockingPropogateAction.RemoveAndDisposePages : DockingPropogateAction.RemovePages, uniqueNames);
            }
        }

        /// <summary>
        /// Remove all pages.
        /// </summary>
        /// <param name="disposePage">Should the page be disposed when removed.</param>
        public void RemoveAllPages(bool disposePage)
        {
            // Remove all details about all pages from all parts of the hierarchy
            using (DockingMultiUpdate update = new DockingMultiUpdate(this))
                base.PropogateAction(disposePage ? DockingPropogateAction.RemoveAndDisposeAllPages : DockingPropogateAction.RemoveAllPages, (string[])null);
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
                case DockingPropogateAction.ShowAllPages:
                case DockingPropogateAction.HideAllPages:
                case DockingPropogateAction.RemoveAllPages:
                case DockingPropogateAction.RemoveAndDisposeAllPages:
                    // Ignore some global actions
                    break;
                default:
                    base.PropogateAction(action, uniqueNames);
                    break;
            }
        }

        /// <summary>
        /// Propogates a request for drag targets down the hierarchy of docking elements.
        /// </summary>
        /// <param name="floatingWindow">Reference to window being dragged.</param>
        /// <param name="dragData">Set of pages being dragged.</param>
        /// <param name="targets">Collection of drag targets.</param>
        public override void PropogateDragTargets(KryptonFloatingWindow floatingWindow,
                                                  PageDragEndData dragData,
                                                  DragTargetList targets)
        {
            // Create list of the pages that are allowed to be dropped into this workspace
            KryptonPageCollection pages = new KryptonPageCollection();
            foreach (KryptonPage page in dragData.Pages)
                if (page.AreFlagsSet(KryptonPageFlags.DockingAllowWorkspace))
                    pages.Add(page);

            // Do we have any pages left for dragging?
            if (pages.Count > 0)
            {
                DragTargetList workspaceTargets = DockableWorkspaceControl.GenerateDragTargets(new PageDragEndData(this, pages), KryptonPageFlags.DockingAllowWorkspace);
                targets.AddRange(workspaceTargets.ToArray());
            }
        }

        /// <summary>
        /// Find the docking location of the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page.</param>
        /// <returns>Enumeration value indicating docking location.</returns>
        public override DockingLocation FindPageLocation(string uniqueName)
        {
            KryptonPage page = DockableWorkspaceControl.PageForUniqueName(uniqueName);
            if ((page != null) && !(page is KryptonStorePage))
                return DockingLocation.Workspace;
            else
                return DockingLocation.None;
        }       
 
        /// <summary>
        /// Find the docking element that contains the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page.</param>
        /// <returns>IDockingElement reference if page is found; otherwise null.</returns>
        public override IDockingElement FindPageElement(string uniqueName)
        {
            KryptonPage page = DockableWorkspaceControl.PageForUniqueName(uniqueName);
            if ((page != null) && !(page is KryptonStorePage))
                return this;
            else
                return null;
        }

        /// <summary>
        /// Find the docking element that contains the location specific store page for the named page.
        /// </summary>
        /// <param name="location">Location to be searched.</param>
        /// <param name="uniqueName">Unique name of the page to be found.</param>
        /// <returns>IDockingElement reference if store page is found; otherwise null.</returns>
        public override IDockingElement FindStorePageElement(DockingLocation location, string uniqueName)
        {
            if (location == DockingLocation.Workspace)
            {
                KryptonPage page = DockableWorkspaceControl.PageForUniqueName(uniqueName);
                if ((page != null) && (page is KryptonStorePage))
                    return this;
            }

            return null;
        }

        /// <summary>
        /// Find a workspace element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable workspace element is required.</param>
        /// <returns>KryptonDockingWorkspace reference if found; otherwise false.</returns>
        public override KryptonDockingWorkspace FindDockingWorkspace(string uniqueName)
        {
            return this;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the proprogate action used to clear a store page for this implementation.
        /// </summary>
        protected override DockingPropogateAction ClearStoreAction
        {
            get { return DockingPropogateAction.ClearFillerStoredPages; }
        }

        /// <summary>
        /// Raises the type specific space control removed event determinated by the derived class.
        /// </summary>
        protected override void RaiseRemoved()
        {
            // Generate event so the any dockable workspace customization can be reversed.
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                DockableWorkspaceEventArgs args = new DockableWorkspaceEventArgs(DockableWorkspaceControl, this);
                dockingManager.RaiseDockableWorkspaceRemoved(args);
            }
        }
        
        /// <summary>
        /// Raises the type specific cell adding event determinated by the derived class.
        /// </summary>
        /// <param name="cell">Referecence to new cell being added.</param>
        protected override void RaiseCellAdding(KryptonWorkspaceCell cell)
        {
            // Generate event so the dockable workspace cell customization can be performed.
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                DockableWorkspaceCellEventArgs args = new DockableWorkspaceCellEventArgs(DockableWorkspaceControl, this, cell);
                dockingManager.RaiseDockableWorkspaceCellAdding(args);
            }
        }

        /// <summary>
        /// Raises the type specific cell removed event determinated by the derived class.
        /// </summary>
        /// <param name="cell">Referecence to an existing cell being removed.</param>
        protected override void RaiseCellRemoved(KryptonWorkspaceCell cell)
        {
            // Generate event so the dockable workspace cell customization can be reversed.
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                DockableWorkspaceCellEventArgs args = new DockableWorkspaceCellEventArgs(DockableWorkspaceControl, this, cell);
                dockingManager.RaiseDockableWorkspaceCellRemoved(args);
            }
        }

        /// <summary>
        /// Occurs when a page is dropped on the control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">A PageDropEventArgs containing the event data.</param>
        protected override void RaiseSpacePageDrop(object sender, PageDropEventArgs e)
        {
            // Use event to indicate the page is moving to a workspace and allow it to be cancelled
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                CancelUniqueNameEventArgs args = new CancelUniqueNameEventArgs(e.Page.UniqueName, false);
                dockingManager.RaisePageWorkspaceRequest(args);

                // Pass back the result of the event
                e.Cancel = args.Cancel;
            }
        }

        /// <summary>
        /// Gets the xml element name to use when saving.
        /// </summary>
        protected override string XmlElementName
        {
            get { return "DW"; }
        }
        #endregion    

        #region Implementation
        private void OnDockableWorkspaceBeforePageDrag(object sender, PageDragCancelEventArgs e)
        {
            // Validate the list of names to those that are still present in the dockspace
            List<KryptonPage> pages = new List<KryptonPage>();
            foreach (KryptonPage page in e.Pages)
                if (!(page is KryptonStorePage) && (DockableWorkspaceControl.CellForPage(page) != null))
                    pages.Add(page);

            // Only need to start docking dragging if we have some valid pages
            if (pages.Count != 0)
            {
                // Ask the docking manager for a IDragPageNotify implementation to handle the dragging operation
                KryptonDockingManager dockingManager = DockingManager;
                if (dockingManager != null)
                    dockingManager.DoDragDrop(e.ScreenPoint, e.ElementOffset, e.Control, e.Pages);
            }

            // Always take over docking
            e.Cancel = true;
        }
        #endregion
    }
}
