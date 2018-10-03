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
using ComponentFactory.Krypton.Workspace;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Provides display and docking functionality for a group of auto hidden pages.
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [DesignTimeVisible(false)]
    public class KryptonDockingAutoHiddenGroup : DockingElementClosedCollection
    {
        #region Instance Fields
        private DockingEdge _edge;
        private KryptonAutoHiddenGroup _autoHiddenGroup;
        private int _cacheCellVisibleCount;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks a page header and so requests it be shown.
        /// </summary>
        public event EventHandler<KryptonPageEventArgs> PageClicked;

        /// <summary>
        /// Occurs when the user hovers the mouse over a page in the group.
        /// </summary>
        public event EventHandler<KryptonPageEventArgs> PageHoverStart;

        /// <summary>
        /// Occurs when the hover over a page ends.
        /// </summary>
        public event EventHandler<EventArgs> PageHoverEnd;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonDockingAutoHiddenGroup class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        /// <param name="edge">Docking edge being managed.</param>
        public KryptonDockingAutoHiddenGroup(string name, DockingEdge edge)
            : base(name)
        {
            _edge = edge;

            // Create a control that will draw tabs for auto hidden pages
            _autoHiddenGroup = new KryptonAutoHiddenGroup(edge);
            _autoHiddenGroup.StoringPage += new EventHandler<UniqueNameEventArgs>(OnAutoHiddenGroupStoringPage);
            _autoHiddenGroup.TabClicked += new EventHandler<KryptonPageEventArgs>(OnAutoHiddenGroupTabClicked);
            _autoHiddenGroup.TabMouseHoverStart += new EventHandler<KryptonPageEventArgs>(OnAutoHiddenGroupHoverStart);
            _autoHiddenGroup.TabMouseHoverEnd += new EventHandler(OnAutoHiddenGroupHoverEnd);
            _autoHiddenGroup.TabVisibleCountChanged += new EventHandler(OnAutoHiddenGroupTabVisibleCountChanged);
            _autoHiddenGroup.Disposed += new EventHandler(OnAutoHiddenGroupDisposed);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets the docking edge this element is managing.
        /// </summary>
        public DockingEdge Edge
        {
            get { return _edge; }
        }

        /// <summary>
        /// Gets the control this element is managing.
        /// </summary>
        public KryptonAutoHiddenGroup AutoHiddenGroupControl
        {
            get { return _autoHiddenGroup; }
        }

        /// <summary>
        /// Gets the sibling docked edge.
        /// </summary>
        public KryptonDockingEdgeDocked EdgeDockedElement
        {
            get
            {
                // Scan up the parent chain to get the edge we are expected to be inside
                KryptonDockingEdge dockingEdge = GetParentType(typeof(KryptonDockingEdge)) as KryptonDockingEdge;
                if (dockingEdge != null)
                {
                    // Extract the expected fixed name of the docked edge element
                    return dockingEdge["Docked"] as KryptonDockingEdgeDocked;
                }

                return null;
            }
        }

        /// <summary>
        /// Add a KryptonPage to the end of the auto hidden group.
        /// </summary>
        /// <param name="page">KryptonPage to be added.</param>
        public void Append(KryptonPage page)
        {
            // Use existing array adding method to prevent duplication of code
            Append(new KryptonPage[] { page });
        }

        /// <summary>
        /// Add the KryptonPage array to the end of the auto hidden group.
        /// </summary>
        /// <param name="pages">Array of KryptonPage's to be added.</param>
        public void Append(KryptonPage[] pages)
        {
            // Demand that pages are not already present
            DemandPagesNotBePresent(pages);

            if (pages != null)
                AppendPagesToControl(pages);
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
                case DockingPropogateAction.ShowPages:
                case DockingPropogateAction.HidePages:
                    {
                        bool newVisible = (action == DockingPropogateAction.ShowPages);
                        foreach (string uniqueName in uniqueNames)
                        {
                            // Update visible state of pages that are not placeholders
                            KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
                            if ((page != null) && !(page is KryptonStorePage))
                                page.Visible = newVisible;
                        }
                    }
                    break;
                case DockingPropogateAction.ShowAllPages:
                    AutoHiddenGroupControl.ShowAllPages(typeof(KryptonStorePage));
                    break;
                case DockingPropogateAction.HideAllPages:
                    AutoHiddenGroupControl.HideAllPages(typeof(KryptonStorePage));
                    break;
                case DockingPropogateAction.RemovePages:
                case DockingPropogateAction.RemoveAndDisposePages:
                    foreach (string uniqueName in uniqueNames)
                    {
                        // Only remove the actual page and not placeholders
                        KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
                        if ((page != null) && !(page is KryptonStorePage))
                        {
                            AutoHiddenGroupControl.Pages.Remove(page);

                            if (action == DockingPropogateAction.RemoveAndDisposePages)
                                page.Dispose();
                        }
                    }
                    break;
                case DockingPropogateAction.Loading:
                    // Remove all pages including store pages
                    AutoHiddenGroupControl.Pages.Clear();
                    break;
                case DockingPropogateAction.RemoveAllPages:
                case DockingPropogateAction.RemoveAndDisposeAllPages:
                    for (int i = AutoHiddenGroupControl.Pages.Count - 1; i >= 0; i--)
                    {
                        // Only remove the actual page and not placeholders
                        KryptonPage page = AutoHiddenGroupControl.Pages[i];
                        if ((page != null) && !(page is KryptonStorePage))
                        {
                            AutoHiddenGroupControl.Pages.RemoveAt(i);

                            if (action == DockingPropogateAction.RemoveAndDisposeAllPages)
                                page.Dispose();
                        }
                    }
                    break;
                case DockingPropogateAction.StorePages:
                    AutoHiddenGroupControl.StorePages(uniqueNames);
                    break;
                case DockingPropogateAction.StoreAllPages:
                    AutoHiddenGroupControl.StoreAllPages();
                    break;
                case DockingPropogateAction.ClearAutoHiddenStoredPages:
                case DockingPropogateAction.ClearStoredPages:
                    foreach (string uniqueName in uniqueNames)
                    {
                        // Only remove a matching placeholder page
                        KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
                        if ((page != null) && (page is KryptonStorePage))
                            AutoHiddenGroupControl.Pages.Remove(page);
                    }
                    break;
                case DockingPropogateAction.ClearAllStoredPages:
                    for(int i=AutoHiddenGroupControl.Pages.Count-1; i>=0; i--)
                    {
                        // Only remove a placeholder paged
                        KryptonPage page = AutoHiddenGroupControl.Pages[i];
                        if ((page != null) && (page is KryptonStorePage))
                            AutoHiddenGroupControl.Pages.RemoveAt(i);
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
                    AutoHiddenGroupControl.RestorePages(pages);
                    break;
            }

            // Let base class perform standard processing
            base.PropogateAction(action, pages);
        }

        /// <summary>
        /// Propogates a boolean state request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Boolean state that is requested to be recovered.</param>
        /// <param name="uniqueName">Unique name of the page the request relates to.</param>
        /// <returns>True/False if state is known; otherwise null indicating no information available.</returns>
        public override bool? PropogateBoolState(DockingPropogateBoolState state, string uniqueName)
        {
            switch (state)
            {
                case DockingPropogateBoolState.ContainsPage:
                    {
                        // Return definitive answer 'true' if the group controls contains the named page (but not for a placeholder)
                        KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
                        if ((page != null) && !(page is KryptonStorePage))
                            return true;
                    }
                    break;
                case DockingPropogateBoolState.ContainsStorePage:
                    {
                        // Return definitive answer 'true' if the group controls contains a store page for the unique name.
                        KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
                        if ((page != null) && (page is KryptonStorePage))
                            return true;
                    }
                    break;
                case DockingPropogateBoolState.IsPageShowing:
                    {
                        // If requested page exists then return the visible state of the page (but not for a placeholder)
                        KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
                        if ((page != null) && !(page is KryptonStorePage))
                            return page.LastVisibleSet;
                    }
                    break;
            }

            // Let base class perform standard processing
            return base.PropogateBoolState(state, uniqueName);
        }

        /// <summary>
        /// Propogates a page request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Request that should result in a page reference if found.</param>
        /// <param name="uniqueName">Unique name of the page the request relates to.</param>
        /// <returns>Reference to page that matches the request; otherwise null.</returns>
        public override KryptonPage PropogatePageState(DockingPropogatePageState state, string uniqueName)
        {
            switch (state)
            {
                case DockingPropogatePageState.PageForUniqueName:
                    {
                        // If we have the page (stored via a proxy) then return the actual page reference (but not for a placeholder)
                        KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
                        if ((page != null) && (page is KryptonAutoHiddenProxyPage))
                            return ((KryptonAutoHiddenProxyPage)page).Page;
                    }
                    break;
            }

            // Let base class perform standard processing
            return base.PropogatePageState(state, uniqueName);
        }

        /// <summary>
        /// Propogates a page list request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Request that should result in pages collection being modified.</param>
        /// <param name="pages">Pages collection for modification by the docking elements.</param>
        public override void PropogatePageList(DockingPropogatePageList state, KryptonPageCollection pages)
        {
            switch (state)
            {
                case DockingPropogatePageList.All:
                case DockingPropogatePageList.AutoHidden:
                    for (int i = AutoHiddenGroupControl.Pages.Count - 1; i >= 0; i--)
                    {
                        // Only add real pages and not just placeholders
                        KryptonPage page = AutoHiddenGroupControl.Pages[i];
                        if ((page != null) && !(page is KryptonStorePage))
                        {
                            // Remember the real page is inside a proxy!
                            KryptonAutoHiddenProxyPage proxyPage = (KryptonAutoHiddenProxyPage)page;
                            pages.Add(proxyPage.Page);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Find the docking location of the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page.</param>
        /// <returns>Enumeration value indicating docking location.</returns>
        public override DockingLocation FindPageLocation(string uniqueName)
        {
            KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
            if ((page != null) && !(page is KryptonStorePage))
                return DockingLocation.AutoHidden;
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
            KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
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
            if (location == DockingLocation.AutoHidden)
            {
                KryptonPage page = AutoHiddenGroupControl.Pages[uniqueName];
                if ((page != null) && (page is KryptonStorePage))
                    return this;
            }

            return null;
        }

        /// <summary>
        /// Return an array of the visible pages that are inside the auto hidden group.
        /// </summary>
        /// <returns>Array of page references.</returns>
        public KryptonPage[] VisiblePages()
        {
            List<KryptonPage> pages = new List<KryptonPage>();

            // Only interested in visible pages that are not placeholders
            foreach (KryptonPage page in AutoHiddenGroupControl.Pages)
                if ((page is KryptonAutoHiddenProxyPage) && page.LastVisibleSet)
                {
                    // Add the actual page this proxy wraps
                    KryptonAutoHiddenProxyPage proxyPage = (KryptonAutoHiddenProxyPage)page;
                    pages.Add(proxyPage.Page);
                }

            return pages.ToArray();
        }        
        
        /// <summary>
        /// Saves docking configuration information using a provider xml writer.
        /// </summary>
        /// <param name="xmlWriter">Xml writer object.</param>
        public override void SaveElementToXml(XmlWriter xmlWriter)
        {
            KryptonDockingManager manager = DockingManager;

            // Output docking manager element
            xmlWriter.WriteStartElement(XmlElementName);
            xmlWriter.WriteAttributeString("N", Name);
            xmlWriter.WriteAttributeString("C", AutoHiddenGroupControl.Pages.Count.ToString());

            // Output an element per page
            foreach (KryptonPage page in AutoHiddenGroupControl.Pages)
            {
                // Are we allowed to save the page?
                if (page.AreFlagsSet(KryptonPageFlags.AllowConfigSave))
                {
                    xmlWriter.WriteStartElement("KP");
                    xmlWriter.WriteAttributeString("UN", page.UniqueName);
                    xmlWriter.WriteAttributeString("V", CommonHelper.BoolToString(page.LastVisibleSet));
                    xmlWriter.WriteAttributeString("S", CommonHelper.BoolToString(page is KryptonStorePage));

                    // Give event handlers a chance to save custom data with the page
                    xmlWriter.WriteStartElement("CPD");
                    DockPageSavingEventArgs args = new DockPageSavingEventArgs(manager, xmlWriter, page);
                    manager.RaisePageSaving(args);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteFullEndElement();
                }
            }

            // Terminate the workspace element
            xmlWriter.WriteFullEndElement();
        }

        /// <summary>
        /// Loads docking configuration information using a provider xml reader.
        /// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="pages">Collection of available pages for adding.</param>
        public override void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages)
        {
            // Let base class load the pages into the group
            base.LoadElementFromXml(xmlReader, pages);

            // Determine the correct visible state of the control
            if (AutoHiddenGroupControl.Pages.VisibleCount == 0)
            {
                _cacheCellVisibleCount = 0;
                AutoHiddenGroupControl.Visible = false;
            }
            else
            {
                _cacheCellVisibleCount = 1;
                AutoHiddenGroupControl.Visible = true;
            }

            // If loading did not create any pages then kill ourself as not needed
            if (AutoHiddenGroupControl.Pages.Count == 0)
                AutoHiddenGroupControl.Dispose();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the PageClicked event.
        /// </summary>
        /// <param name="e">An KryptonPageEventArgs containing the event data.</param>
        protected virtual void OnPageClicked(KryptonPageEventArgs e)
        {
            if (PageClicked != null)
                PageClicked(this, e);
        }

        /// <summary>
        /// Raises the PageHoverStart event.
        /// </summary>
        /// <param name="e">An KryptonPageEventArgs containing the event data.</param>
        protected virtual void OnPageHoverStart(KryptonPageEventArgs e)
        {
            if (PageHoverStart != null)
                PageHoverStart(this, e);
        }

        /// <summary>
        /// Raises the PageHoverEnd event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnPageHoverEnd(EventArgs e)
        {
            if (PageHoverEnd != null)
                PageHoverEnd(this, e);
        }

        /// <summary>
        /// Gets the xml element name to use when saving.
        /// </summary>
        protected override string XmlElementName
        {
            get { return "DAHG"; }
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
            KryptonDockingManager manager = DockingManager;

            // Is it the expected xml element name?
            if (xmlReader.Name != "KP")
                throw new ArgumentException("Element name 'KP' was expected but found '" + xmlReader.Name + "' instead.");

            // Get the unique name of the page
            string uniqueName = xmlReader.GetAttribute("UN");
            string boolStore = xmlReader.GetAttribute("S");
            string boolVisible = xmlReader.GetAttribute("V");

            KryptonPage page = null;

            // If the entry is for just a placeholder...
            if (CommonHelper.StringToBool(boolStore))
            {
                // Recreate the requested store page and append
                page = new KryptonStorePage(uniqueName, "AutoHiddenGroup");
                AutoHiddenGroupControl.Pages.Add(page);
            }
            else
            {
                // Can we find a provided page to match the incoming layout?
                page = pages[uniqueName];
                if (page == null)
                {
                    // Generate event so developer can create and supply the page now
                    RecreateLoadingPageEventArgs args = new RecreateLoadingPageEventArgs(uniqueName);
                    manager.RaiseRecreateLoadingPage(args);
                    if (!args.Cancel)
                    {
                        page = args.Page;

                        // Add recreated page to the looking dictionary
                        if ((page != null) && (pages[page.UniqueName] == null))
                            pages.Add(page);
                    }
                }

                if (page != null)
                {
                    // Use the loaded visible state
                    page.Visible = CommonHelper.StringToBool(boolVisible);

                    // Create a proxy around the page and append it
                    KryptonAutoHiddenProxyPage proxyPage = new KryptonAutoHiddenProxyPage(page);
                    AutoHiddenGroupControl.Pages.Add(proxyPage);
                }
            }

            if (!xmlReader.Read())
                throw new ArgumentException("An element was expected but could not be read in.");

            if (xmlReader.Name != "CPD")
                throw new ArgumentException("Expected 'CPD' element was not found");

            bool finished = xmlReader.IsEmptyElement;

            // Generate event so custom data can be loaded and/or the page to be added can be modified
            DockPageLoadingEventArgs pageLoading = new DockPageLoadingEventArgs(manager, xmlReader, page);
            manager.RaisePageLoading(pageLoading);

            // Read everything until we get the end of custom data marker
            while (!finished)
            {
                // Check it has the expected name
                if (xmlReader.NodeType == XmlNodeType.EndElement)
                    finished = (xmlReader.Name == "CPD");

                if (!finished)
                {
                    if (!xmlReader.Read())
                        throw new ArgumentException("An element was expected but could not be read in.");
                }
            }

            if (!xmlReader.Read())
                throw new ArgumentException("An element was expected but could not be read in.");
        }
        #endregion

        #region Implementation
        private void AppendPagesToControl(KryptonPage[] pages)
        {
            // Make a list of all the 'store' pages being added
            List<string> uniqueNames = new List<string>();
            foreach (KryptonPage page in pages)
                if (page is KryptonStorePage)
                    uniqueNames.Add(page.UniqueName);

            // We only allow a single 'store' page in this docking location at a time
            if (uniqueNames.Count > 0)
                DockingManager.PropogateAction(DockingPropogateAction.ClearAutoHiddenStoredPages, uniqueNames.ToArray());

            // Non-store pages need to be wrapped in a proxy appropriate for the auto hidden control
            for (int i = 0; i < pages.Length; i++)
                if (!(pages[i] is KryptonStorePage))
                    pages[i] = new KryptonAutoHiddenProxyPage(pages[i]);

            // Add the proxy pages so that we can still use the actual pages instances elsewhere
            AutoHiddenGroupControl.Pages.AddRange(pages);
        }

        private void OnAutoHiddenGroupStoringPage(object sender, UniqueNameEventArgs e)
        {
            // We only allow a single 'store' page in this docking location at a time
            DockingManager.PropogateAction(DockingPropogateAction.ClearAutoHiddenStoredPages, new string[] {e.UniqueName});
        }

        private void OnAutoHiddenGroupTabClicked(object sender, KryptonPageEventArgs e)
        {
            // The auto hidden group contains proxy pages and not the real pages
            KryptonAutoHiddenProxyPage proxyPage = (KryptonAutoHiddenProxyPage)e.Item;
            OnPageClicked(new KryptonPageEventArgs(proxyPage.Page, e.Index));
        }

        private void OnAutoHiddenGroupHoverStart(object sender, KryptonPageEventArgs e)
        {
            // The auto hidden group contains proxy pages and not the real pages
            KryptonAutoHiddenProxyPage proxyPage = (KryptonAutoHiddenProxyPage)e.Item;
            OnPageHoverStart(new KryptonPageEventArgs(proxyPage.Page, e.Index));
        }

        private void OnAutoHiddenGroupHoverEnd(object sender, EventArgs e)
        {
            OnPageHoverEnd(e);
        }

        private void OnAutoHiddenGroupTabVisibleCountChanged(object sender, EventArgs e)
        {
            if (AutoHiddenGroupControl.Pages.VisibleCount == 0)
            {
                if (_cacheCellVisibleCount > 0)
                {
                    _cacheCellVisibleCount = 0;
                    AutoHiddenGroupControl.Visible = false;
                }
            }
            else
            {
                if (_cacheCellVisibleCount == 0)
                {
                    _cacheCellVisibleCount = 1;
                    AutoHiddenGroupControl.Visible = true;
                }
            }
        }
        private void OnAutoHiddenGroupDisposed(object sender, EventArgs e)
        {
            // Unhook from events so the control can be garbage collected
            AutoHiddenGroupControl.StoringPage -= new EventHandler<UniqueNameEventArgs>(OnAutoHiddenGroupStoringPage);
            AutoHiddenGroupControl.TabClicked -= new EventHandler<KryptonPageEventArgs>(OnAutoHiddenGroupTabClicked);
            AutoHiddenGroupControl.TabMouseHoverStart -= new EventHandler<KryptonPageEventArgs>(OnAutoHiddenGroupHoverStart);
            AutoHiddenGroupControl.TabMouseHoverEnd -= new EventHandler(OnAutoHiddenGroupHoverEnd);
            AutoHiddenGroupControl.TabVisibleCountChanged -= new EventHandler(OnAutoHiddenGroupTabVisibleCountChanged);
            AutoHiddenGroupControl.Disposed -= new EventHandler(OnAutoHiddenGroupDisposed);

            // Events are generated from the parent docking manager
            KryptonDockingManager dockingManager = DockingManager;
            if (dockingManager != null)
            {
                // Allow the auto hidden group to be customized by event handlers
                AutoHiddenGroupEventArgs groupArgs = new AutoHiddenGroupEventArgs(AutoHiddenGroupControl, this);
                dockingManager.RaiseAutoHiddenGroupRemoved(groupArgs);
            }

            // Generate event so interested parties know this element and associated window have been disposed
            Dispose();
        }
        #endregion
    }
}
