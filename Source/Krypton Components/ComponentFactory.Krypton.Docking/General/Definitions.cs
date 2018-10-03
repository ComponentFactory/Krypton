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
    #region Interface IDockingElement
    /// <summary>
    /// Interface exposed by elements within the docking hierarchy.
    /// </summary>
    public interface IDockingElement : IEnumerable<IDockingElement>
    {
        /// <summary>
        /// Gets and sets the name of the docking element.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a comma separated list of names leading to this element.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Resolve the provided path.
        /// </summary>
        /// <param name="path">Comma separated list of names to resolve.</param>
        /// <returns>IDockingElement reference if path was resolved with success; otherwise null.</returns>
        IDockingElement ResolvePath(string path);

        /// <summary>
        /// Gets and sets access to the parent docking element.
        /// </summary>
        IDockingElement Parent { get; set; }

        /// <summary>
        /// Propogates an action request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="action">Action that is requested to be performed.</param>
        /// <param name="uniqueNames">Array of unique names of the pages the action relates to.</param>
        void PropogateAction(DockingPropogateAction action, string[] uniqueNames);

        /// <summary>
        /// Propogates an action request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="action">Action that is requested to be performed.</param>
        /// <param name="pages">Array of pages the action relates to.</param>
        void PropogateAction(DockingPropogateAction action, KryptonPage[] pages);

        /// <summary>
        /// Propogates an action request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="action">Action that is requested to be performed.</param>
        /// <param name="value">Integer value associated with the request.</param>
        void PropogateAction(DockingPropogateAction action, int value);

        /// <summary>
        /// Propogates a boolean state request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Boolean state that is requested to be recovered.</param>
        /// <param name="uniqueName">Unique name of the page the request relates to.</param>
        /// <returns>True/False if state is known; otherwise null indicating no information available.</returns>
        bool? PropogateBoolState(DockingPropogateBoolState state, string uniqueName);

        /// <summary>
        /// Propogates an integer state request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Integer state that is requested to be recovered.</param>
        /// <param name="value">Value discovered from matching </param>
        void PropogateIntState(DockingPropogateIntState state, ref int value);

        /// <summary>
        /// Propogates a request for drag targets down the hierarchy of docking elements.
        /// </summary>
        /// <param name="floatingWindow">Reference to window being dragged.</param>
        /// <param name="dragData">Set of pages being dragged.</param>
        /// <param name="targets">Collection of drag targets.</param>
        void PropogateDragTargets(KryptonFloatingWindow floatingWindow,
                                  PageDragEndData dragData,
                                  DragTargetList targets);

        /// <summary>
        /// Propogates a page request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Request that should result in a page reference if found.</param>
        /// <param name="uniqueName">Unique name of the page the request relates to.</param>
        /// <returns>Reference to page that matches the request; otherwise null.</returns>
        KryptonPage PropogatePageState(DockingPropogatePageState state, string uniqueName);

        /// <summary>
        /// Propogates a page list request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Request that should result in pages collection being modified.</param>
        /// <param name="pages">Pages collection for modification by the docking elements.</param>
        void PropogatePageList(DockingPropogatePageList state, KryptonPageCollection pages);

        /// <summary>
        /// Propogates a workspace cell list request down the hierarchy of docking elements.
        /// </summary>
        /// <param name="state">Request that should result in the cells collection being modified.</param>
        /// <param name="cells">Cells collection for modification by the docking elements.</param>
        void PropogateCellList(DockingPropogateCellList state, KryptonWorkspaceCellList cells);

        /// <summary>
        /// Find the docking location of the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page.</param>
        /// <returns>Enumeration value indicating docking location.</returns>
        DockingLocation FindPageLocation(string uniqueName);
        
        /// <summary>
        /// Find the docking element that contains the named page.
        /// </summary>
        /// <param name="uniqueName">Unique name of the page.</param>
        /// <returns>IDockingElement reference if page is found; otherwise null.</returns>
        IDockingElement FindPageElement(string uniqueName);

        /// <summary>
        /// Find the docking element that contains the location specific store page for the named page.
        /// </summary>
        /// <param name="location">Location to be searched.</param>
        /// <param name="uniqueName">Unique name of the page to be found.</param>
        /// <returns>IDockingElement reference if store page is found; otherwise null.</returns>
        IDockingElement FindStorePageElement(DockingLocation location, string uniqueName);

        /// <summary>
        /// Find a floating docking element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable floating element is required.</param>
        /// <returns>KryptonDockingFloating reference if found; otherwise false.</returns>
        KryptonDockingFloating FindDockingFloating(string uniqueName);

        /// <summary>
        /// Find a edge docked element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable docking edge element is required.</param>
        /// <returns>KryptonDockingEdgeDocked reference if found; otherwise false.</returns>
        KryptonDockingEdgeDocked FindDockingEdgeDocked(string uniqueName);

        /// <summary>
        /// Find a edge auto hidden element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable auto hidden edge element is required.</param>
        /// <returns>KryptonDockingEdgeAutoHidden reference if found; otherwise false.</returns>
        KryptonDockingEdgeAutoHidden FindDockingEdgeAutoHidden(string uniqueName);

        /// <summary>
        /// Find a workspace element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable workspace element is required.</param>
        /// <returns>KryptonDockingWorkspace reference if found; otherwise false.</returns>
        KryptonDockingWorkspace FindDockingWorkspace(string uniqueName);

        /// <summary>
        /// Find a navigator element by searching the hierarchy.
        /// </summary>
        /// <param name="uniqueName">Named page for which a suitable navigator element is required.</param>
        /// <returns>KryptonDockingNavigator reference if found; otherwise false.</returns>
        KryptonDockingNavigator FindDockingNavigator(string uniqueName);
        
        /// <summary>
        /// Saves docking configuration information using a provider xml writer.
        /// </summary>
        /// <param name="xmlWriter">Xml writer object.</param>
        void SaveElementToXml(XmlWriter xmlWriter);

        /// <summary>
        /// Loads docking configuration information using a provider xml reader.
        /// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="pages">Collection of available pages.</param>
        void LoadElementFromXml(XmlReader xmlReader, KryptonPageCollection pages);

        /// <summary>
        /// Gets the number of child docking elements.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the docking element at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>Docking element at specified index.</returns>
        IDockingElement this[int index] { get; }

        /// <summary>
        /// Gets the docking element with the specified name.
        /// </summary>
        /// <param name="name">Name of element.</param>
        /// <returns>Docking element with specified name.</returns>
        IDockingElement this[string name] { get; }
    }
    #endregion

    #region Interface IFloatingMessages
    /// <summary>
    /// Interface exposed by elements that provide floating messages.
    /// </summary>
    public interface IFloatingMessages
    {
        /// <summary>
        /// The WM_KEYDOWN message has occured.
        /// </summary>
        /// <returns>True to eat message; otherwise false.</returns>
        bool OnKEYDOWN(ref Message m);

        /// <summary>
        /// The WM_MOUSEMOVE message has occured.
        /// </summary>
        void OnMOUSEMOVE();

        /// <summary>
        /// The WM_LBUTTONUP message has occured.
        /// </summary>
        void OnLBUTTONUP();
    }
    #endregion

    #region Enum DockingEdge
    /// <summary>
    /// Specifies a docking edge of a control.
    /// </summary>
    public enum DockingEdge
    {
        /// <summary>Specifies the left edge of a control.</summary>
        Left,

        /// <summary>Specifies the right edge of a control.</summary>
        Right,

        /// <summary>Specifies the top edge of a control.</summary>
        Top,

        /// <summary>Specifies the bottom edge of a control.</summary>
        Bottom
    }
    #endregion

    #region Enum DockingCloseAction
    /// <summary>
    /// Specifies the action to take when a docking close is required.
    /// </summary>
    public enum DockingCloseRequest
    {
        /// <summary>Specifies no action be taken.</summary>
        None,

        /// <summary>Specifies the named page be removed from the docking hierarchy.</summary>
        RemovePage,

        /// <summary>Specifies the named page be removed from the docking hierarchy and then disposed.</summary>
        RemovePageAndDispose,

        /// <summary>Specifies the named page be hidden.</summary>
        HidePage,
    }
    #endregion

    #region Enum DockingLocation
    /// <summary>
    /// Specifies the current docking location of a page.
    /// </summary>
    public enum DockingLocation
    {
        /// <summary>Specifies the page is auto hidden against a control edge.</summary>
        AutoHidden,

        /// <summary>Specifies the page is docked against a control edge.</summary>
        Docked,

        /// <summary>Specifies the page is inside a floating window.</summary>
        Floating,

        /// <summary>Specifies the page is inside a standalone workspace.</summary>
        Workspace,

        /// <summary>Specifies the page is inside a standalone navigator.</summary>
        Navigator,

        /// <summary>Specifies the page is part of a custom extension.</summary>
        Custom,

        /// <summary>Specifies the page is not inside the docking hierarchy.</summary>
        None,
    }
    #endregion

    #region Enum DockingAutoHiddenShowState
    /// <summary>
    /// Specifies the sliding state of a docked auto hidden page.
    /// </summary>
    public enum DockingAutoHiddenShowState
    {
        /// <summary>
        /// Specifies the auto hidden page has become hidden.
        /// </summary>
        Hidden,

        /// <summary>
        /// Specifies the auto hidden page is sliding out into view.
        /// </summary>
        SlidingOut,

        /// <summary>
        /// Specifies the auto hidden page is sliding back to be hidden.
        /// </summary>
        SlidingIn,

        /// <summary>
        /// Specifies the auto hidden page is fully showing.
        /// </summary>
        Showing,
    }
    #endregion

    #region Enum DockingPropogateAction
    /// <summary>
    /// Specifies a docking propogate action.
    /// </summary>
    public enum DockingPropogateAction
    {
        /// <summary>Specifies a null operation.</summary>
        Null,

        /// <summary>Specifies a multi-part update is starting.</summary>
        StartUpdate,

        /// <summary>Specifies a multi-part update has ended.</summary>
        EndUpdate,

        /// <summary>Specifies all display elements of the named pages be shown.</summary>
        ShowPages,

        /// <summary>Specifies all display elements of all pages be shown.</summary>
        ShowAllPages,

        /// <summary>Specifies all display elements of the named pages be hidden.</summary>
        HidePages,

        /// <summary>Specifies all display elements of all pages be hidden.</summary>
        HideAllPages,

        /// <summary>Specifies the named pages are replaced with position placeholders.</summary>
        StorePages,

        /// <summary>Specifies all pages are replaced with position placeholders.</summary>
        StoreAllPages,

        /// <summary>Specifies the position placeholders are restored with actual pages.</summary>
        RestorePages,

        /// <summary>Specifies the auto hidden store pages should be removed for the named pages.</summary>
        ClearAutoHiddenStoredPages,

        /// <summary>Specifies the docked store pages should be removed for the named pages.</summary>
        ClearDockedStoredPages,

        /// <summary>Specifies the floating store pages should be removed for the named pages.</summary>
        ClearFloatingStoredPages,

        /// <summary>Specifies the filler store pages should be removed for the named pages.</summary>
        ClearFillerStoredPages,

        /// <summary>Specifies all stored pages should be removed for the named pages.</summary>
        ClearStoredPages,

        /// <summary>Specifies all stored pages should be removed.</summary>
        ClearAllStoredPages,

        /// <summary>Specifies all details of the named pages be removed.</summary>
        RemovePages,

        /// <summary>Specifies all details of the named pages be removed and the page disposed.</summary>
        RemoveAndDisposePages,

        /// <summary>Specifies all details of all pages be removed.</summary>
        RemoveAllPages,

        /// <summary>Specifies all details of all pages be removed and the pages disposed.</summary>
        RemoveAndDisposeAllPages,

        /// <summary>Specifies a loading operation is about to begin.</summary>
        Loading,

        /// <summary>Specifies a dockspace with matching ordering value reposition its controls.</summary>
        RepositionDockspace,

        /// <summary>Specifies the named string property has been updated.</summary>
        StringChanged,

        /// <summary>Specifies that debug output about the docking contents be output.</summary>
        DebugOutput
    }
    #endregion

    #region Enum DockingPropogateBoolState
    /// <summary>
    /// Specifies a docking propogate for boolean state.
    /// </summary>
    public enum DockingPropogateBoolState
    {
        /// <summary>Specifies active state for a named page.</summary>
        ContainsPage,

        /// <summary>Specifies store state for a named page.</summary>
        ContainsStorePage,

        /// <summary>Specifies showing state for a named page.</summary>
        IsPageShowing,
    }
    #endregion

    #region Enum DockingPropogateIntState
    /// <summary>
    /// Specifies a docking propogate for integer state.
    /// </summary>
    public enum DockingPropogateIntState
    {
        /// <summary>Specifies control ordering for dockspace controls.</summary>
        DockspaceOrder,
    }
    #endregion

    #region Enum DockingPropogatePageState
    /// <summary>
    /// Specifies a docking propogate for page references.
    /// </summary>
    public enum DockingPropogatePageState
    {
        /// <summary>Specifies a page referenced is required for the named page.</summary>
        PageForUniqueName,
    }
    #endregion

    #region Enum DockingPropogatePageList
    /// <summary>
    /// Specifies a docking propogate for page list.
    /// </summary>
    public enum DockingPropogatePageList
    {
        /// <summary>Specifies a list of all pages be created.</summary>
        All,

        /// <summary>Specifies a list of all docked pages be created.</summary>
        Docked,

        /// <summary>Specifies a list of all auto hidden pages be created.</summary>
        AutoHidden,

        /// <summary>Specifies a list of all floating pages be created.</summary>
        Floating,

        /// <summary>Specifies a list of all filler pages be created.</summary>
        Filler,
    }
    #endregion

    #region Enum DockingPropogateCellList
    /// <summary>
    /// Specifies a docking propogate for cell list.
    /// </summary>
    public enum DockingPropogateCellList
    {
        /// <summary>Specifies a list of all cells be created.</summary>
        All,

        /// <summary>Specifies a list of all docked cells be created.</summary>
        Docked,

        /// <summary>Specifies a list of all floating cells be created.</summary>
        Floating,

        /// <summary>Specifies a list of all workspace cells be created.</summary>
        Workspace,
    }
    #endregion
}
