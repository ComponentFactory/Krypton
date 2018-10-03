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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Workspace;

namespace ComponentFactory.Krypton.Docking
{
    /// <summary>
    /// Extends the KryptonWorkspace with common functionality shared by various docking implementations.
    /// </summary>
    public abstract class KryptonSpace : KryptonWorkspace
    {
        #region Type Declarations
        /// <summary>
        /// Lookup between a cell and the cell state.
        /// </summary>
        protected class CellToCachedState : Dictionary<KryptonWorkspaceCell, CachedCellState> { };

        /// <summary>
        /// State cached per-cell within the workspace.
        /// </summary>
        protected class CachedCellState
        {
            #region Instance Fields
            private KryptonWorkspaceCell _cell;
            private bool _focusState;
            private KryptonPage _selectedPage;
            private ButtonSpecNavigator _closeButtonSpec;
            private ButtonSpecNavigator _pinButtonSpec;
            private ButtonSpecNavigator _dropDownButtonSpec;
            #endregion

            /// <summary>
            /// Gets and sets the workspace cell for which this state relates.
            /// </summary>
            public KryptonWorkspaceCell Cell
            {
                get { return _cell; }
                set { _cell = value; }
            }

            /// <summary>
            /// Gets and sets the focus state of the cell.
            /// </summary>
            public bool FocusState 
            {
                get { return _focusState; }
                set { _focusState = value; }
            }

            /// <summary>
            /// Gets and sets the selected page.
            /// </summary>
            public KryptonPage SelectedPage
            {
                get { return _selectedPage; }
                set { _selectedPage = value; }
            }

            /// <summary>
            /// Gets and sets the button spec used to represent a close button.
            /// </summary>
            public ButtonSpecNavigator CloseButtonSpec
            {
                get { return _closeButtonSpec; }
                set { _closeButtonSpec = value; }
            }

            /// <summary>
            /// Gets and sets the button spec used to represent a pin button.
            /// </summary>
            public ButtonSpecNavigator PinButtonSpec
            {
                get { return _pinButtonSpec; }
                set { _pinButtonSpec = value; }
            }

            /// <summary>
            /// Gets and sets the button spec used to represent a drop down button.
            /// </summary>
            public ButtonSpecNavigator DropDownButtonSpec
            {
                get { return _dropDownButtonSpec; }
                set { _dropDownButtonSpec = value; }
            }
        }
        #endregion

        #region Instance Fields
        private CellToCachedState _lookupCellState;
        private EventHandler _focusUpdate;
        private EventHandler _visibleUpdate;
        private bool _awaitingFocusUpdate;
        private bool _awaitingVisibleUpdate;
        private bool _autoHiddenHost;
        private bool _setFocus;
        private string _closeTooltip;
        private string _pinTooltip;
        private string _dropDownTooltip;
        private string _storeName;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the focus moves to be inside the KryptonWorkspaceCell instance.
        /// </summary>
        [Category("DockableWorkspace")]
        [Description("Occurs when the focus moves to be inside the KryptonWorkspaceCell instance.")]
        public event EventHandler<WorkspaceCellEventArgs> CellGainsFocus;

        /// <summary>
        /// Occurs when the focus moves away from inside the KryptonWorkspaceCell instance.
        /// </summary>
        [Category("DockableWorkspace")]
        [Description("Occurs when the focus moves away from inside the KryptonWorkspaceCell instance.")]
        public event EventHandler<WorkspaceCellEventArgs> CellLosesFocus;

        /// <summary>
        /// Occurs when a page is being inserted into a cell.
        /// </summary>
        [Category("DockableWorkspace")]
        [Description("Occurs when a page is being inserted into a cell.")]
        public event EventHandler<KryptonPageEventArgs> CellPageInserting;

        /// <summary>
        /// Occurs when a page requests that it be closed.
        /// </summary>
        [Category("DockableWorkspace")]
        [Description("Occurs when a page requests that it be closed.")]
        public event EventHandler<UniqueNameEventArgs> PageCloseClicked;

        /// <summary>
        /// Occurs when a page requests that it be auto hidden state switched.
        /// </summary>
        [Category("DockableWorkspace")]
        [Description("Occurs when a page requests that it be auto hidden state switched.")]
        public event EventHandler<UniqueNameEventArgs> PageAutoHiddenClicked;

        /// <summary>
        /// Occurs when a page requests that a drop down menu be shown.
        /// </summary>
        [Category("DockableWorkspace")]
        [Description("Occurs when a page drop down menu is requested from a header button.")]
        public event EventHandler<CancelDropDownEventArgs> PageDropDownClicked;

        /// <summary>
        /// Occurs when a page or set of pages have been double clicked.
        /// </summary>
        [Category("DockableWorkspace")]
        [Description("Occurs when a page or set of pages have been double clicked.")]
        public event EventHandler<UniqueNamesEventArgs> PagesDoubleClicked;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonSpace class.
        /// </summary>
        /// <param name="storeName">Name to associate with store pages.</param>
        public KryptonSpace(string storeName)
        {
            // We do not want there to always be at least one cell present
            CompactFlags &= ~CompactFlags.AtLeastOneVisibleCell;

            // Override default settings in the base
            ContextMenus.ShowContextMenu = false;
            ShowMaximizeButton = false;
            Size = new Size(200, 200);

            // Create lookup for per-cell cached state
            _lookupCellState = new CellToCachedState();

            // Create delegates so processing happens sync'd with the message queue
            _visibleUpdate = new EventHandler(OnVisibleUpdate);
            _focusUpdate = new EventHandler(OnFocusUpdate);
            _awaitingFocusUpdate = false;
            _autoHiddenHost = false;
            _closeTooltip = "Close";
            _pinTooltip = "Auto Hidden";
            _dropDownTooltip = "Window Position";
            _storeName = storeName;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _lookupCellState.Clear();

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the tooltip used for the close button.
        /// </summary>
        [Category("Dockable")]
        [Description("Tooltip for the close button.")]
        [DefaultValue("Close")]
        public string CloseTooltip
        {
            get { return _closeTooltip; }

            set
            {
                _closeTooltip = value;
                UpdateTooltips();
            }
        }

        /// <summary>
        /// Gets and sets the tooltip used for the pin button.
        /// </summary>
        [Category("Dockable")]
        [Description("Tooltip for the pin button.")]
        [DefaultValue("Auto Hidden")]
        public string PinTooltip
        {
            get { return _pinTooltip; }

            set
            {
                _pinTooltip = value;
                UpdateTooltips();
            }
        }

        /// <summary>
        /// Gets and sets the tooltips used for the drop down button.
        /// </summary>
        [Category("Dockable")]
        [Description("Tooltip for the drop down button.")]
        [DefaultValue("Window Position")]
        public string DropDownTooltip
        {
            get { return _dropDownTooltip; }

            set
            {
                _dropDownTooltip = value;
                UpdateTooltips();
            }
        }

        /// <summary>
        /// Gets the button spec type for the pin button.
        /// </summary>
        [Browsable(false)]
        public bool AutoHiddenHost
        {
            get { return _autoHiddenHost; }
            set { _autoHiddenHost = value; }
        }

        /// <summary>
        /// Requests the visible state be updated.
        /// </summary>
        public void UpdateVisible()
        {
            UpdateVisible(false);
        }

        /// <summary>
        /// Requests the visible state be updated.
        /// </summary>
        /// <param name="focus">Should the current cell be given the focus.</param>
        public void UpdateVisible(bool focus)
        {
            if (ApplyDockingVisibility)
            {
                // Cache focus update until invoke occurs
                _setFocus = focus;

                // No point requesting the update more than once
                if (!_awaitingVisibleUpdate && IsHandleCreated)
                {
                    // Async invoke ensures the delegate is called in sync with the message queue, this means that all the
                    // layout changes for the space control will be finished and so then we can update the visible state of
                    // the cells to reflect the tab visible changes.
                    BeginInvoke(_visibleUpdate);
                    _awaitingVisibleUpdate = true;
                }
            }           
        }

        /// <summary>
        /// Write page details to xml during save process.
        /// </summary>
        /// <param name="xmlWriter">XmlWriter to use for saving.</param>
        /// <param name="page">Reference to page.</param>
        public override void WritePageElement(XmlWriter xmlWriter, KryptonPage page)
        {
            CommonHelper.TextToXmlAttribute(xmlWriter, "UN", page.UniqueName);
            CommonHelper.TextToXmlAttribute(xmlWriter, "S", CommonHelper.BoolToString(page is KryptonStorePage));
            CommonHelper.TextToXmlAttribute(xmlWriter, "V", CommonHelper.BoolToString(page.LastVisibleSet), "True");
        }

        /// <summary>
        /// Read page details from xml during load process.
        /// </summary>
        /// <param name="xmlReader">XmlReader to use for loading.</param>
        /// <param name="uniqueName">Unique name of page being loaded.</param>
        /// <param name="existingPages">Set of existing pages.</param>
        /// <returns>Reference to page to be added into the workspace cell.</returns>
        public override KryptonPage ReadPageElement(XmlReader xmlReader,
                                                    string uniqueName,
                                                    UniqueNameToPage existingPages)
        {
            // If a matching page with the unique name already exists then use it, 
            // otherwise we need to create an entirely new page instance.
            KryptonPage page;
            if (existingPages.TryGetValue(uniqueName, out page))
                existingPages.Remove(uniqueName);
            else
            {
                // Use event to try and get a newly created page for use
                RecreateLoadingPageEventArgs args = new RecreateLoadingPageEventArgs(uniqueName);
                OnRecreateLoadingPage(args);
                if (!args.Cancel)
                {
                    page = args.Page;

                    // Add recreated page to the looking dictionary
                    if ((page != null) && !existingPages.ContainsKey(page.UniqueName))
                        existingPages.Add(page.UniqueName, page);
                }
            }

            if (page != null)
            {
                // If this is a store page then recreate as a store page type
                if (CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "S")))
                    page = new KryptonStorePage(page.UniqueName, _storeName);
                else
                {
                    // Only some values if the actual page and not if it is a store page
                    page.UniqueName = CommonHelper.XmlAttributeToText(xmlReader, "UN");
                    page.Visible = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "V", "True"));
                }
            }

            // Read past the page start element                 
            if (!xmlReader.Read())
                throw new ArgumentException("An element was expected but could not be read in.");

            return page;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets a value indicating if docking specific appearance should be applied.
        /// </summary>
        protected virtual bool ApplyDockingAppearance
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating if docking specific close action should be applied.
        /// </summary>
        protected virtual bool ApplyDockingCloseAction
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating if docking specific pin actions should be applied.
        /// </summary>
        protected virtual bool ApplyDockingPinAction
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating if docking specific drop down actions should be applied.
        /// </summary>
        protected virtual bool ApplyDockingDropDownAction
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating if docking specific visible changes should be applied.
        /// </summary>
        protected virtual bool ApplyDockingVisibility
        {
            get { return true; }
        }

        /// <summary>
        /// Raises the CellGainsFocus event.
        /// </summary>
        /// <param name="e">An WorkspaceCellEventArgs containing the event data.</param>
        protected virtual void OnCellGainsFocus(WorkspaceCellEventArgs e)
        {
            if (CellGainsFocus != null)
                CellGainsFocus(this, e);
        }

        /// <summary>
        /// Raises the CellLosesFocus event.
        /// </summary>
        /// <param name="e">An WorkspaceCellEventArgs containing the event data.</param>
        protected virtual void OnCellLosesFocus(WorkspaceCellEventArgs e)
        {
            if (CellLosesFocus != null)
                CellLosesFocus(this, e);
        }

        /// <summary>
        /// Raises the CellPageInserting event.
        /// </summary>
        /// <param name="e">An KryptonPageEventArgs containing the event data.</param>
        protected virtual void OnCellPageInserting(KryptonPageEventArgs e)
        {
            if (CellPageInserting != null)
                CellPageInserting(this, e);
        }

        /// <summary>
        /// Raises the PageCloseClicked event.
        /// </summary>
        /// <param name="e">An UniqueNameEventArgs containing the event data.</param>
        protected virtual void OnPageCloseClicked(UniqueNameEventArgs e)
        {
            if (PageCloseClicked != null)
                PageCloseClicked(this, e);
        }

        /// <summary>
        /// Raises the PageAutoHiddenClicked event.
        /// </summary>
        /// <param name="e">An UniqueNameEventArgs containing the event data.</param>
        protected virtual void OnPageAutoHiddenClicked(UniqueNameEventArgs e)
        {
            if (PageAutoHiddenClicked != null)
                PageAutoHiddenClicked(this, e);
        }

        /// <summary>
        /// Raises the PageDropDownClicked event.
        /// </summary>
        /// <param name="e">An CancelDropDownEventArgs containing the event data.</param>
        protected virtual void OnPageDropDownClicked(CancelDropDownEventArgs e)
        {
            if (PageDropDownClicked != null)
                PageDropDownClicked(this, e);
        }

        /// <summary>
        /// Raises the PagesDoubleClicked event.
        /// </summary>
        /// <param name="e">An UniqueNamesEventArgs containing the event data.</param>
        protected virtual void OnPagesDoubleClicked(UniqueNamesEventArgs e)
        {
            if (PagesDoubleClicked != null)
                PagesDoubleClicked(this, e);
        }

        /// <summary>
        /// Initialize a new cell.
        /// </summary>
        /// <param name="cell">Cell being added to the control.</param>
        protected override void NewCellInitialize(KryptonWorkspaceCell cell)
        {
            // Let base class perform event hooking and customizations
            base.NewCellInitialize(cell);

            // Should be apply docking specific appearance settings?
            if (ApplyDockingAppearance)
            {
                if (cell.Pages.VisibleCount == 1)
                    cell.NavigatorMode = NavigatorMode.HeaderGroup;
                else
                    cell.NavigatorMode = NavigatorMode.HeaderGroupTab;

                cell.Bar.BarMultiline = BarMultiline.Shrinkline;
                cell.Bar.BarOrientation = VisualOrientation.Bottom;
                cell.Bar.TabBorderStyle = TabBorderStyle.DockOutsize;
                cell.Bar.TabStyle = TabStyle.Dock;
                cell.Button.ButtonDisplayLogic = ButtonDisplayLogic.None;
                cell.Button.CloseButtonDisplay = ButtonDisplay.Hide;
                cell.Header.HeaderStylePrimary = HeaderStyle.DockInactive;
                cell.Header.HeaderVisibleSecondary = false;
                cell.Header.HeaderValuesPrimary.MapImage = MapKryptonPageImage.None;
                cell.ToolTips.AllowButtonSpecToolTips = true;
            }

            // Hook into cell specific events
            cell.ShowContextMenu += new EventHandler<ShowContextMenuArgs>(OnCellShowContextMenu);
            cell.SelectedPageChanged += new EventHandler(OnCellSelectedPageChanged);
            cell.PrimaryHeaderLeftClicked += new EventHandler(OnCellPrimaryHeaderLeftClicked);
            cell.PrimaryHeaderRightClicked += new EventHandler(OnCellPrimaryHeaderRightClicked);
            cell.PrimaryHeaderDoubleClicked += new EventHandler(OnCellPrimaryHeaderDoubleClicked);
            cell.TabDoubleClicked += new EventHandler<KryptonPageEventArgs>(OnCellTabDoubleClicked);
            cell.TabVisibleCountChanged += new EventHandler(OnCellTabVisibleCountChanged);
            cell.Pages.Inserting += new TypedHandler<KryptonPage>(OnCellPagesInserting);

            // Create and store per-cell cached state
            CachedCellState cellState = new CachedCellState();
            cellState.Cell = cell;
            _lookupCellState.Add(cell, cellState);
            UpdateCellActions(cell, cellState);

            // We need to know when the focus enters/leaves a cell so styles can be updated
            FocusMonitorControl(cell, true);

            // If there is already a selected page then ensure we process its selected state
            if (cell.SelectedPage != null)
                OnCellSelectedPageChanged(cell, EventArgs.Empty);

            // If the cell already have pages then raise inserting events for those pages
            if (cell.Pages.Count > 0)
                for (int i = cell.Pages.Count - 1; i >= 0; i--)
                    OnCellPageInserting(new KryptonPageEventArgs(cell.Pages[i], i));
        }

        /// <summary>
        /// Detach an existing cell.
        /// </summary>
        /// <param name="cell">Cell being removed from the control.</param>
        protected override void ExistingCellDetach(KryptonWorkspaceCell cell)
        {
            // Grab the per-cell cached state
            CachedCellState cellState = _lookupCellState[cell];

            // Remove all those event hooks used to monitor focus changes
            FocusMonitorControl(cell, false);

            // Unhook from events so the cell can be garbage colleced
            cell.ShowContextMenu -= new EventHandler<ShowContextMenuArgs>(OnCellShowContextMenu);
            cell.SelectedPageChanged -= new EventHandler(OnCellSelectedPageChanged);
            cell.PrimaryHeaderLeftClicked -= new EventHandler(OnCellPrimaryHeaderLeftClicked);
            cell.PrimaryHeaderRightClicked -= new EventHandler(OnCellPrimaryHeaderRightClicked);
            cell.PrimaryHeaderDoubleClicked -= new EventHandler(OnCellPrimaryHeaderDoubleClicked);
            cell.TabDoubleClicked -= new EventHandler<KryptonPageEventArgs>(OnCellTabDoubleClicked);
            cell.TabVisibleCountChanged -= new EventHandler(OnCellTabVisibleCountChanged);
            cell.Pages.Inserting -= new TypedHandler<KryptonPage>(OnCellPagesInserting);

            // Remove the per-cell cached state
            _lookupCellState.Remove(cell);

            // Let base class unhook from events and reverse other operations
            base.ExistingCellDetach(cell);
        }

        /// <summary>
        /// Update the cell settings to reflect the provided page, cell and page flag settings.
        /// </summary>
        /// <param name="cell">Reference to workspace cell that needs updating.</param>
        /// <param name="cellState">Reference to cell specific cached state.</param>
        protected virtual void UpdateCellActions(KryptonWorkspaceCell cell, CachedCellState cellState)
        {
            if (ApplyDockingDropDownAction)
            {
                // First time around we need to create the pin button spec
                if (cellState.DropDownButtonSpec == null)
                {
                    cellState.DropDownButtonSpec = new ButtonSpecNavigator();
                    cellState.DropDownButtonSpec.Type = PaletteButtonSpecStyle.DropDown;
                    cellState.DropDownButtonSpec.ToolTipTitle = DropDownTooltip;
                    cellState.DropDownButtonSpec.KryptonContextMenu = new KryptonContextMenu();
                    cellState.DropDownButtonSpec.KryptonContextMenu.Opening += new CancelEventHandler(OnCellDropDownOpening);
                    cell.Button.ButtonSpecs.Add(cellState.DropDownButtonSpec);
                }

                if (cell.SelectedPage == null)
                    cellState.DropDownButtonSpec.Visible = false;
                else
                    cellState.DropDownButtonSpec.Visible = cell.SelectedPage.AreFlagsSet(KryptonPageFlags.DockingAllowDropDown);
            }

            if (ApplyDockingPinAction)
            {
                // First time around we need to create the pin button spec
                if (cellState.PinButtonSpec == null)
                {
                    cellState.PinButtonSpec = new ButtonSpecNavigator();
                    cellState.PinButtonSpec.Type = (AutoHiddenHost ? PaletteButtonSpecStyle.PinHorizontal : PaletteButtonSpecStyle.PinVertical);
                    cellState.PinButtonSpec.ToolTipTitle = PinTooltip;
                    cellState.PinButtonSpec.Click += new EventHandler(OnCellAutoHiddenAction);
                    cell.Button.ButtonSpecs.Add(cellState.PinButtonSpec);
                }

                if (cell.SelectedPage == null)
                    cellState.PinButtonSpec.Visible = false;
                else
                {
                    if (AutoHiddenHost)
                        cellState.PinButtonSpec.Visible = cell.SelectedPage.AreFlagsSet(KryptonPageFlags.DockingAllowDocked);
                    else
                        cellState.PinButtonSpec.Visible = cell.SelectedPage.AreFlagsSet(KryptonPageFlags.DockingAllowAutoHidden);
                }
            }

            if (ApplyDockingCloseAction)
            {
                // First time around we need to create the close button spec
                if (cellState.CloseButtonSpec == null)
                {
                    cellState.CloseButtonSpec = new ButtonSpecNavigator();
                    cellState.CloseButtonSpec.Type = PaletteButtonSpecStyle.Close;
                    cellState.CloseButtonSpec.ToolTipTitle = CloseTooltip;
                    cellState.CloseButtonSpec.Click += new EventHandler(OnCellCloseAction);
                    cell.Button.ButtonSpecs.Add(cellState.CloseButtonSpec);
                }

                if (cell.SelectedPage == null)
                    cellState.CloseButtonSpec.Visible = false;
                else
                    cellState.CloseButtonSpec.Visible = cell.SelectedPage.AreFlagsSet(KryptonPageFlags.DockingAllowClose);
            }
        }
        #endregion

        #region Implementation
        private void FocusMonitorControl(Control c, bool adding)
        {
            // Hook/Unhook this control
            if (adding)
            {
                c.GotFocus += new EventHandler(OnFocusChanged);
                c.LostFocus += new EventHandler(OnFocusChanged);
                c.ControlAdded += new ControlEventHandler(OnFocusControlAdded);
                c.ControlRemoved += new ControlEventHandler(OnFocusControlRemoved);
            }
            else
            {
                c.GotFocus -= new EventHandler(OnFocusChanged);
                c.LostFocus -= new EventHandler(OnFocusChanged);
                c.ControlAdded -= new ControlEventHandler(OnFocusControlAdded);
                c.ControlRemoved -= new ControlEventHandler(OnFocusControlRemoved);
            }

            // Monitor all the child controls as well
            foreach (Control child in c.Controls)
                FocusMonitorControl(child, adding);
        }

        private void OnFocusControlAdded(object sender, ControlEventArgs e)
        {
            // Add focus monitoring to the control
            FocusMonitorControl(e.Control, true);
        }

        private void OnFocusControlRemoved(object sender, ControlEventArgs e)
        {
            // Remove focus monitoring from the control
            FocusMonitorControl(e.Control, true);
        }

        private void OnFocusChanged(object sender, EventArgs e)
        {
            // No point requesting the update more than once
            if (!_awaitingFocusUpdate && IsHandleCreated)
            {
                // Async invoke ensures the delegate is called in sync with the message queue, this means that all the
                // focus messages will have finished and the focus will have settled onto its new location. This is 
                // always true because SETFOCUS/KILLFOCUS messages are always 'send' and not 'post' messages.
                BeginInvoke(_focusUpdate);
                _awaitingFocusUpdate = true;
            }
        }

        private void OnFocusUpdate(object sender, EventArgs e)
        {
            // Should we apply docking specific appearance changes to reflect changes in focus?
            if (ApplyDockingAppearance)
            {
                CheckPerformLayout(false);
                KryptonWorkspaceCell cell = FirstCell();
                while (cell != null)
                {
                    // Use focus dependant header style
                    if (cell.ContainsFocus)
                    {
                        // Change in cell focus state?
                        if (!_lookupCellState[cell].FocusState)
                        {
                            _lookupCellState[cell].FocusState = true;
                            cell.Header.HeaderStylePrimary = HeaderStyle.DockActive;
                            OnCellGainsFocus(new WorkspaceCellEventArgs(cell));
                        }
                    }
                    else
                    {
                        // Change in cell focus state?
                        if (_lookupCellState[cell].FocusState)
                        {
                            _lookupCellState[cell].FocusState = false;
                            cell.Header.HeaderStylePrimary = HeaderStyle.DockInactive;
                            OnCellLosesFocus(new WorkspaceCellEventArgs(cell));
                        }
                    }

                    cell = NextCell(cell);
                }
            }

            // Allow another focus update to occur
            _awaitingFocusUpdate = false;
        }

        private void OnVisibleUpdate(object sender, EventArgs e)
        {
            if (ApplyDockingVisibility)
            {
                bool visibleChanged = false;
                KryptonWorkspaceCell cell = FirstCell();
                while (cell != null)
                {
                    // Cell if only visible if it has at least 1 visible page
                    bool newVisible = (cell.Pages.VisibleCount > 0);
                    visibleChanged |= (cell.Visible != newVisible);
                    cell.Visible = newVisible;

                    if (ApplyDockingAppearance && newVisible)
                    {
                        // Cell display mode depends on the number of tabs in the cell
                        if (cell.Pages.VisibleCount == 1)
                        {
                            if (cell.NavigatorMode == NavigatorMode.HeaderGroupTab)
                                cell.NavigatorMode = NavigatorMode.HeaderGroup;
                        }
                        else
                        {
                            if (cell.NavigatorMode == NavigatorMode.HeaderGroup)
                                cell.NavigatorMode = NavigatorMode.HeaderGroupTab;
                        }
                    }

                    cell = NextCell(cell);
                }

                // Allow another visibility update to occur
                _awaitingVisibleUpdate = false;

                // Any change in visible state requires a layout to show the changes
                if (visibleChanged)
                    PerformLayout();

                // If the control is requested to have the focus, then do so now
                if (_setFocus)
                {
                    Focus();
                    _setFocus = false;
                }
            }
        }

        private void OnCellShowContextMenu(object sender, ShowContextMenuArgs e)
        {
            // Make sure we have a menu for displaying
            if (e.KryptonContextMenu == null)
                e.KryptonContextMenu = new KryptonContextMenu();

            // Use event to allow customization of the context menu
            CancelDropDownEventArgs args = new CancelDropDownEventArgs(e.KryptonContextMenu, e.Item);
            args.Cancel = e.Cancel;
            OnPageDropDownClicked(args);
            e.Cancel = args.Cancel;
        }

        private void OnCellPrimaryHeaderLeftClicked(object sender, EventArgs e)
        {
            // Should we apply docking specific change of focus when the primary header is clicked?
            if (ApplyDockingAppearance)
            {
                // Set the focus into the active page
                KryptonWorkspaceCell cell = (KryptonWorkspaceCell)sender;
                if (cell.SelectedPage != null)
                    cell.SelectedPage.SelectNextControl(cell.SelectedPage, true, true, true, false);
            }
        }

        private void OnCellPrimaryHeaderRightClicked(object sender, EventArgs e)
        {
            // Should we apply docking specific change of focus when the primary header is clicked?
            if (ApplyDockingAppearance)
            {
                KryptonWorkspaceCell cell = (KryptonWorkspaceCell)sender;
                if (cell.SelectedPage != null)
                {
                    // Set the focus into the active page
                    cell.SelectedPage.SelectNextControl(cell.SelectedPage, true, true, true, false);

                    // Create and populate a context menu with the drop down set of options
                    KryptonContextMenu kcm = new KryptonContextMenu();
                    CancelDropDownEventArgs args = new CancelDropDownEventArgs(kcm, cell.SelectedPage);
                    OnPageDropDownClicked(args);

                    // Do we need to show a context menu
                    if (!args.Cancel && CommonHelper.ValidKryptonContextMenu(args.KryptonContextMenu))
                        args.KryptonContextMenu.Show(this, Control.MousePosition);
                }
            }
        }

        private void OnCellPrimaryHeaderDoubleClicked(object sender, EventArgs e)
        {
            // Should we apply docking specific change of focus when the primary header is clicked?
            if (ApplyDockingAppearance)
            {
                List<string> uniqueNames = new List<string>();

                // Create list of visible pages that are not placeholders
                KryptonWorkspaceCell cell = (KryptonWorkspaceCell)sender;
                foreach (KryptonPage page in cell.Pages)
                    if (page.LastVisibleSet && !(page is KryptonStorePage))
                        uniqueNames.Add(page.UniqueName);

                if (uniqueNames.Count > 0)
                    OnPagesDoubleClicked(new UniqueNamesEventArgs(uniqueNames.ToArray()));
            }
        }

        private void OnCellTabDoubleClicked(object sender, KryptonPageEventArgs e)
        {
            OnPagesDoubleClicked(new UniqueNamesEventArgs(new string[] { e.Item.UniqueName }));
        }

        private void OnCellTabVisibleCountChanged(object sender, EventArgs e)
        {
            if (ApplyDockingAppearance)
            {
                KryptonWorkspaceCell cell = FirstVisibleCell();
                while (cell != null)
                {
                    // Cell display mode depends on the number of tabs in the cell
                    if (cell.Pages.VisibleCount == 1)
                    {
                        if (cell.NavigatorMode == NavigatorMode.HeaderGroupTab)
                            cell.NavigatorMode = NavigatorMode.HeaderGroup;  
                    }
                    else
                    {
                        if (cell.NavigatorMode == NavigatorMode.HeaderGroup)
                            cell.NavigatorMode = NavigatorMode.HeaderGroupTab;
                    }

                    cell = NextVisibleCell(cell);
                }
            }

            UpdateVisible(_setFocus);
        }

        private void OnCellSelectedPageChanged(object sender, EventArgs e)
        {
            if (ApplyDockingCloseAction || ApplyDockingPinAction)
            {
                // Get access to the cached state for this cell
                KryptonWorkspaceCell cell = (KryptonWorkspaceCell)sender;
                CachedCellState cellState = _lookupCellState[cell];

                // Remove events on the old selected page
                if (cellState.SelectedPage != null)
                    cellState.SelectedPage.FlagsChanged -= new KryptonPageFlagsEventHandler(OnCellSelectedPageFlagsChanged);

                // Use the new setting
                cellState.SelectedPage = cell.SelectedPage;
                UpdateCellActions(cell, cellState);

                // Add events on the new selected page
                if (cellState.SelectedPage != null)
                    cellState.SelectedPage.FlagsChanged += new KryptonPageFlagsEventHandler(OnCellSelectedPageFlagsChanged);
            }
        }

        private void OnCellSelectedPageFlagsChanged(object sender, KryptonPageFlagsEventArgs e)
        {
            if (ApplyDockingCloseAction || ApplyDockingPinAction)
            {
                // Only need to process the change in flags if the page in question is a selected page
                KryptonPage page = (KryptonPage)sender;
                KryptonWorkspaceCell cell = CellForPage(page);
                if (cell.SelectedPage == page)
                    UpdateCellActions(cell, _lookupCellState[cell]);
            }
        }

        private void OnCellCloseAction(object sender, EventArgs e)
        {
            if (ApplyDockingCloseAction)
            {
                // Find the page associated with the cell that fired this button spec
                ButtonSpec buttonSpec = (ButtonSpec)sender;
                foreach(CachedCellState cellState in _lookupCellState.Values)
                    if (cellState.CloseButtonSpec == buttonSpec)
                    {
                        if (cellState.Cell.SelectedPage != null)
                            OnPageCloseClicked(new UniqueNameEventArgs(cellState.Cell.SelectedPage.UniqueName));
                        
                        break;
                    }
            }
        }

        private void OnCellAutoHiddenAction(object sender, EventArgs e)
        {
            if (ApplyDockingPinAction)
            {
                // Find the page associated with the cell that fired this button spec
                ButtonSpec buttonSpec = (ButtonSpec)sender;
                foreach(CachedCellState cellState in _lookupCellState.Values)
                    if (cellState.PinButtonSpec == buttonSpec)
                    {
                        if (cellState.Cell.SelectedPage != null)
                            OnPageAutoHiddenClicked(new UniqueNameEventArgs(cellState.Cell.SelectedPage.UniqueName));
                        
                        break;
                    }
            }
        }

        private void OnCellDropDownOpening(object sender, CancelEventArgs e)
        {
            if (ApplyDockingDropDownAction)
            {
                // Search for the cell that contains the button spec that has this context menu
                KryptonContextMenu kcm = (KryptonContextMenu)sender;
                foreach (CachedCellState cellState in _lookupCellState.Values)
                    if ((cellState.DropDownButtonSpec != null) && (cellState.DropDownButtonSpec.KryptonContextMenu == kcm))
                    {
                        if (cellState.Cell.SelectedPage != null)
                            OnPageDropDownClicked(new CancelDropDownEventArgs(cellState.DropDownButtonSpec.KryptonContextMenu, cellState.Cell.SelectedPage));

                        break;
                    }
            }
        }

        private void OnCellPagesInserting(object sender, TypedCollectionEventArgs<KryptonPage> e)
        {
            // Generate event so the docking element can decide on extra actions to be taken
            OnCellPageInserting(new KryptonPageEventArgs(e.Item, e.Index));
        }

        private void UpdateTooltips()
        {
            foreach (CachedCellState state in _lookupCellState.Values)
            {
                if (state.DropDownButtonSpec != null)
                    state.DropDownButtonSpec.ToolTipTitle = DropDownTooltip;

                if (state.PinButtonSpec != null)
                    state.PinButtonSpec.ToolTipTitle = PinTooltip;

                if (state.CloseButtonSpec != null)
                    state.CloseButtonSpec.ToolTipTitle = CloseTooltip;
            }
        }
        #endregion
    }
}
