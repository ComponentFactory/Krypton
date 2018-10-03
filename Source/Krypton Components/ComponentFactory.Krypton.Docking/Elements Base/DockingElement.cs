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
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Implements base docking element functionality.
    /// </summary>
    public abstract class DockingElement : Component, 
                                           IDockingElement
    {
        #region Instance Fields
        private string _name;
        private IDockingElement _parent;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the DockingElement class.
        /// </summary>
        /// <param name="name">Initial name of the element.</param>
        public DockingElement(string name)
        {
            // Do not allow null, use empty string instead
            _name = name ?? string.Empty;
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the name of the docking element.
        /// </summary>
        [Browsable(false)]
        public string Name 
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets a comma separated list of names leading to this element.
        /// </summary>
        [Browsable(false)]
        public string Path 
        {
            get
            {
                StringBuilder path = new StringBuilder();

                IDockingElement element = this;
                while (element != null)
                {
                    // Need to comma separate element names
                    if (path.Length > 0)
                        path.Insert(0, ',');

                    // Prepend the elements name
                    path.Insert(0, element.Name);

                    // Walk up the chain of elements
                    element = element.Parent;
                }

                return path.ToString();
            }
        }

        /// <summary>
        /// Resolve the provided path.
        /// </summary>
        /// <param name="path">Comma separated list of names to resolve.</param>
        /// <returns>IDockingElement reference if path was resolved with success; otherwise null.</returns>
        public virtual IDockingElement ResolvePath(string path)
        {
            // Cannot resolve a null reference
            if (path == null)
                throw new ArgumentNullException("path");

            // Path names cannot be zero length
            if (path.Length == 0)
                throw new ArgumentException("path");

            // Extract the first name in the path
            int comma = path.IndexOf(',');
            string firstName = (comma == -1 ? path : path.Substring(0, comma));

            // If the first name matches ourself...
            if (firstName == Name)
            {
                // If there are no other names then we are the target
                if (firstName.Length == path.Length)
                    return this;
                else
                {
                    // Extract the remainder of the path
                    string remainder = path.Substring(comma, path.Length - comma);

                    // Give each child a chance to resolve the remainder of the path
                    foreach (IDockingElement child in this)
                    {
                        IDockingElement ret = child.ResolvePath(remainder);
                        if (ret != null)
                            return ret;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets and sets access to the parent docking element.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IDockingElement Parent 
        {
            get { return _parent; }
            
            set 
            {
                // We do not allow the same name to occur twice in a collection (so check new parent collection)
                if ((value != null) && (value[Name] != null))
                    throw new ArgumentNullException("Parent provided already has our Name in its collection.");

                _parent = value; 
            }
        }

        /// <summary>
        /// Propogates an action request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="action">Action that is requested to be performed.</param>
        /// <param name="uniqueNames">Array of unique names of the pages the action relates to.</param>
        public virtual void PropogateAction(DockingPropogateAction action, string[] uniqueNames)
        {
            // Propogate the action request to all the child elements
            // (use reverse order so if element removes itself we still have a valid loop)
            for(int i = Count - 1; i >= 0; i--)
                this[i].PropogateAction(action, uniqueNames);
        }

        /// <summary>
        /// Propogates an action request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="action">Action that is requested to be performed.</param>
        /// <param name="pages">Array of pages the action relates to.</param>
        public virtual void PropogateAction(DockingPropogateAction action, KryptonPage[] pages)
        {
            // Propogate the action request to all the child elements
            // (use reverse order so if element removes itself we still have a valid loop)
            for (int i = Count - 1; i >= 0; i--)
                this[i].PropogateAction(action, pages);
        }

        /// <summary>
        /// Propogates an action request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="action">Action that is requested to be performed.</param>
        /// <param name="value">Integer value associated with the request.</param>
        public virtual void PropogateAction(DockingPropogateAction action, int value)
        {
            // Propogate the action request to all the child elements
            // (use reverse order so if element removes itself we still have a valid loop)
            for (int i = Count - 1; i >= 0; i--)
                this[i].PropogateAction(action, value);
        }

        /// <summary>
        /// Propogates a boolean state request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Boolean state that is requested to be recovered.</param>
        /// <param name="uniqueName">Unique name of the page the request relates to.</param>
        /// <returns>True/False if state is known; otherwise null indicating no information available.</returns>
        public virtual bool? PropogateBoolState(DockingPropogateBoolState state, string uniqueName)
        {   
            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                // If the child knows the exact answer then return it now
                bool? ret = this[i].PropogateBoolState(state, uniqueName);
                if (ret.HasValue)
                    return ret;
            }

            return null;
        }

        /// <summary>
        /// Propogates an integer state request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Integer state that is requested to be recovered.</param>
        /// <param name="value">Value discovered from matching </param>
        public virtual void PropogateIntState(DockingPropogateIntState state, ref int value)
        {
            // Propogate the request to all the child elements
            for (int i = Count - 1; i >= 0; i--)
                this[i].PropogateIntState(state, ref value);
        }

        /// <summary>
        /// Propogates a page request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Request that should result in a page reference if found.</param>
        /// <param name="uniqueName">Unique name of the page the request relates to.</param>
        /// <returns>Reference to page that matches the request; otherwise null.</returns>
        public virtual KryptonPage PropogatePageState(DockingPropogatePageState state, string uniqueName)
        {
            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                // If the child knows the answer then return it now
                KryptonPage page = this[i].PropogatePageState(state, uniqueName);
                if (page != null)
                    return page;
            }

            return null;
        }

        /// <summary>
        /// Propogates a page list request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Request that should result in pages collection being modified.</param>
        /// <param name="pages">Pages collection for modification by the docking elements.</param>
        public virtual void PropogatePageList(DockingPropogatePageList state, KryptonPageCollection pages)
        {
            // Propogate the action request to all the child elements
            // (use reverse order so if element removes itself we still have a valid loop)
            for (int i = Count - 1; i >= 0; i--)
                this[i].PropogatePageList(state, pages);
        }

        /// <summary>
        /// Propogates a workspace cell list request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Request that should result in the cells collection being modified.</param>
        /// <param name="cells">Cells collection for modification by the docking elements.</param>
        public virtual void PropogateCellList(DockingPropogateCellList state, KryptonWorkspaceCellList cells)
        {
            // Propogate the action request to all the child elements
            // (use reverse order so if element removes itself we still have a valid loop)
            for (int i = Count - 1; i >= 0; i--)
                this[i].PropogateCellList(state, cells);
        }

        /// <summary>
        /// Propogates a request for drag targets down the hierarchy of docking elements.
        /// </summary>
        /// <param name="floatingWindow">Reference to window being dragged.</param>
        /// <param name="dragData">Set of pages being dragged.</param>
        /// <param name="targets">Collection of drag targets.</param>
        public virtual void PropogateDragTargets(KryptonFloatingWindow floatingWindow,
                                                 PageDragEndData dragData,
                                                 DragTargetList targets)
        {
            // Propogate the request to all child elements
            foreach (IDockingElement child in this)
                child.PropogateDragTargets(floatingWindow, dragData, targets);
        }

        /// <summary>
        /// Find the docking location of the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page.</param>
        /// <returns>Enumeration value indicating docking location.</returns>
        public virtual DockingLocation FindPageLocation(string uniqueName)
        {
            // Default to not finding the page
            DockingLocation location = DockingLocation.None;

            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                location = this[i].FindPageLocation(uniqueName);
                if (location != DockingLocation.None)
                    break;
            }

            return location;
        }

        /// <summary>
        /// Find the docking element that contains the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page.</param>
        /// <returns>IDockingElement reference if page is found; otherwise null.</returns>
        public virtual IDockingElement FindPageElement(string uniqueName)
        {
            // Default to not finding the element
            IDockingElement dockingElement = null;

            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                dockingElement = this[i].FindPageElement(uniqueName);
                if (dockingElement != null)
                    break;
            }

            return dockingElement;
        }

        /// <summary>
        /// Find the docking element that contains the location specific store page for the named page.
        /// </summary>
        /// <param name="location">Location to be searched.</param>
        /// <param name="uniqueName">Unique name of the page to be found.</param>
        /// <returns>IDockingElement reference if store page is found; otherwise null.</returns>
        public virtual IDockingElement FindStorePageElement(DockingLocation location, string uniqueName)
        {
            // Default to not finding the element
            IDockingElement dockingElement = null;

            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                dockingElement = this[i].FindStorePageElement(location, uniqueName);
                if (dockingElement != null)
                    break;
            }

            return dockingElement;
        }

        /// <summary>
        /// Find a floating docking element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable floating element is required.</param>
        /// <returns>KryptonDockingFloating reference if found; otherwise false.</returns>
        public virtual KryptonDockingFloating FindDockingFloating(string uniqueName)
        {
            // Default to not finding the element
            KryptonDockingFloating floatingElement = null;

            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                floatingElement = this[i].FindDockingFloating(uniqueName);
                if (floatingElement != null)
                    break;
            }

            return floatingElement;
        }

        /// <summary>
        /// Find a edge docked element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable docking edge element is required.</param>
        /// <returns>KryptonDockingEdgeDocked reference if found; otherwise false.</returns>
        public virtual KryptonDockingEdgeDocked FindDockingEdgeDocked(string uniqueName)
        {
            // Default to not finding the element
            KryptonDockingEdgeDocked edgeDockedElement = null;

            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                edgeDockedElement = this[i].FindDockingEdgeDocked(uniqueName);
                if (edgeDockedElement != null)
                    break;
            }

            return edgeDockedElement;
        }

        /// <summary>
        /// Find a edge auto hidden element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable auto hidden edge element is required.</param>
        /// <returns>KryptonDockingEdgeAutoHidden reference if found; otherwise false.</returns>
        public virtual KryptonDockingEdgeAutoHidden FindDockingEdgeAutoHidden(string uniqueName)
        {
            // Default to not finding the element
            KryptonDockingEdgeAutoHidden edgeAutoHiddenElement = null;

            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                edgeAutoHiddenElement = this[i].FindDockingEdgeAutoHidden(uniqueName);
                if (edgeAutoHiddenElement != null)
                    break;
            }

            return edgeAutoHiddenElement;
        }

        /// <summary>
        /// Find a workspace element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable workspace element is required.</param>
        /// <returns>KryptonDockingWorkspace reference if found; otherwise false.</returns>
        public virtual KryptonDockingWorkspace FindDockingWorkspace(string uniqueName)
        {
            // Default to not finding the element
            KryptonDockingWorkspace workspaceElement = null;

            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                workspaceElement = this[i].FindDockingWorkspace(uniqueName);
                if (workspaceElement != null)
                    break;
            }

            return workspaceElement;
        }

        /// <summary>
        /// Find a navigator element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable navigator element is required.</param>
        /// <returns>KryptonDockingNavigator reference if found; otherwise false.</returns>
        public virtual KryptonDockingNavigator FindDockingNavigator(string uniqueName)
        {
            // Default to not finding the element
            KryptonDockingNavigator navigatorElement = null;

            // Search all child docking elements
            for (int i = 0; i < Count; i++)
            {
                navigatorElement = this[i].FindDockingNavigator(uniqueName);
                if (navigatorElement != null)
                    break;
            }

            return navigatorElement;
        }

        /// <summary>
        /// Saves docking configuration information using a provider xml writer.
        /// </summary>
        /// <param name="xmlWriter">Xml writer object.</param>
        public virtual void SaveElementToXml(XmlWriter xmlWriter)
        {
            // Output docking lement
            xmlWriter.WriteStartElement(XmlElementName);
            xmlWriter.WriteAttributeString("N", Name);
            xmlWriter.WriteAttributeString("C", Count.ToString());

            // Output an element per child
            foreach (IDockingElement child in this)
                child.SaveElementToXml(xmlWriter);

            // Terminate the workspace element
            xmlWriter.WriteFullEndElement();
        }

        /// <summary>
        /// Loads docking configuration information using a provider xml reader.
        /// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="pages">Collection of available pages for adding.</param>
        public virtual void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages)
        {
            // Is it the expected xml element name?
            if (xmlReader.Name != XmlElementName)
                throw new ArgumentException("Element name '" + XmlElementName + "' was expected but found '" + xmlReader.Name + "' instead.");

            // Grab the element attributes
            string elementName = xmlReader.GetAttribute("N");
            string elementCount = xmlReader.GetAttribute("C");

            // Check the name matches up
            if (elementName != Name)
                throw new ArgumentException("Attribute 'N' value '" + Name + "' was expected but found '" + elementName + "' instead.");

            // Let derived class perform element specific persistence
            LoadDockingElement(xmlReader, pages);

            // If there are children then move over them
            int count = int.Parse(elementCount);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    // Read to the next element
                    if (!xmlReader.Read())
                        throw new ArgumentException("An element was expected but could not be read in.");

                    // Find a child docking element with the matching name
                    IDockingElement child = this[xmlReader.GetAttribute("N")];

                    // Let derived class perform child element specific processing
                    LoadChildDockingElement(xmlReader, pages, child);
                }
            }

            // Read past this element to the end element
            if (!xmlReader.Read())
                throw new ArgumentException("An element was expected but could not be read in.");
        }

        /// <summary>
        /// Checks that the provided set of pages are not already present in the docking hierarchy.
        /// </summary>
        public void DemandPagesNotBePresent(KryptonPage[] pages)
        {
            // We need a docking manager in order to perfrom testing
            DemandDockingManager();

            // We always allow store pages but check that others are not already present in the docking hierarchy
            foreach (KryptonPage page in pages)
                if (!(page is KryptonStorePage) && DockingManager.ContainsPage(page))
                    throw new ArgumentOutOfRangeException("Cannot perform operation with a page that is already present inside docking hierarchy");
        }

        /// <summary>
        /// Checks that this element has access to a docking manager, throwing exception if not.
        /// </summary>
        public void DemandDockingManager()
        {
            if (!HasDockManager)
                throw new ApplicationException("Cannot perform this operation when there is no access to a KryptonDockingManager.");
        }

        /// <summary>
        /// Returns a value indicating if this docking element has access to a parent docking manager.
        /// </summary>
        [Browsable(false)]
        public bool HasDockManager
        {
            get { return (DockingManager != null); }
        }

        /// <summary>
        /// Finds the KryptonDockingManager instance that owns this part of the docking hieararchy.
        /// </summary>
        [Browsable(false)]
        public KryptonDockingManager DockingManager
        {
            get 
            {
                // Searching from this element upwards
                IDockingElement parent = this;
                while (parent != null)
                {
                    // If we find a match then we are done
                    if (parent is KryptonDockingManager)
                        return parent as KryptonDockingManager;

                    // Keep going up the parent chain
                    parent = parent.Parent;
                }

                // No match found
                return null;
            }
        }

        /// <summary>
        /// Search up the parent chain looking for the specified type of object.
        /// </summary>
        /// <param name="findType">Type of the instance we are searching for.</param>
        /// <returns>Object reference if found and it implements IDockingElemnet; othrtwise null.</returns>
        public IDockingElement GetParentType(Type findType)
        {
            // Searching from this element upwards
            IDockingElement parent = this;
            while (parent != null)
            {
                // If we find a match then we are done
                if (parent.GetType() == findType)
                    return parent as IDockingElement;

                // Keep going up the parent chain
                parent = parent.Parent;
            }

            // No match found
            return null;
        }

        /// <summary>
        /// Gets the number of child docking elements.
        /// </summary>
        [Browsable(false)]
        public virtual int Count 
        { 
            get { return 0; }
        }

        /// <summary>
        /// Gets the docking element at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>Docking element at specified index.</returns>
        public virtual IDockingElement this[int index] 
        { 
            get { return null; }
        }

        /// <summary>
        /// Gets the docking element with the specified name.
        /// </summary>
        /// <param name="name">Name of element.</param>
        /// <returns>Docking element with specified name.</returns>
        public virtual IDockingElement this[string name]
        {
            get { return null; }
        }

        /// <summary>
        /// Shallow enumerate over child docking elements.
        /// </summary>
        /// <returns>Enumerator instance.</returns>
        public virtual IEnumerator<IDockingElement> GetEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// Enumerate using non-generic interface.
        /// </summary>
        /// <returns>Enumerator instance.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the xml element name to use when saving.
        /// </summary>
        protected abstract string XmlElementName { get; }

        /// <summary>
        /// Perform docking element specific actions based on the loading xml.
        /// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="pages">Collection of available pages.</param>
        protected virtual void LoadDockingElement(XmlReader xmlReader, KryptonPageCollection pages)
        {
        }

        /// <summary>
        /// Perform docking element specific actions for loading a child xml.
        /// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="pages">Collection of available pages.</param>
        /// <param name="child">Optional reference to existing child docking element.</param>
        protected virtual void LoadChildDockingElement(XmlReader xmlReader, 
                                                       KryptonPageCollection pages, 
                                                       IDockingElement child)
        {
            if (child != null)
                child.LoadElementFromXml(xmlReader, pages);
            else
            {
                string nodeName = xmlReader.Name;

                do
                {
                    // Read past this element
                    if (!xmlReader.Read())
                        throw new ArgumentException("An element was expected but could not be read in.");

                    // Finished when we hit the end element matching the incoming one
                    if ((xmlReader.NodeType == XmlNodeType.EndElement) && (xmlReader.Name == nodeName))
                        break;

                } while (true);
            }
        }
        #endregion
    }
}
