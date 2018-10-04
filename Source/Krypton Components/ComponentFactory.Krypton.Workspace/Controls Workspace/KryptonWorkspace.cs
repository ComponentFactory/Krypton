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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
    /// Layout a hierarchy of KryptonNavigator instances.
	/// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonWorkspace), "ToolboxBitmaps.KryptonWorkspace.bmp")]
    [DefaultEvent("WorkspaceCellAdded")]
    [DefaultProperty("Root")]
    [Designer("ComponentFactory.Krypton.Workspace.KryptonWorkspaceDesigner, ComponentFactory.Krypton.Workspace, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Layout a hierarchy of KryptonNavigator instances.")]
    [Docking(DockingBehavior.Ask)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonWorkspace : VisualContainerControl,
                                    IDragTargetProvider
    {
        #region Classes
        /// <summary>
        /// Temporary layout information associated with a workspace child.
        /// </summary>
        internal struct LayoutInfo
        {
            /// <summary>Interface for recovering information from a workspace item.</summary>
            public IWorkspaceItem WorkspaceItem;

            /// <summary>Cached requested size for the layout direction.</summary>
            public StarNumber CacheStarSize;

            /// <summary>Cached requested minimum size in layout direction.</summary>
            public int CacheMinSize;

            /// <summary>Cached requested maximum size in layout direction.</summary>
            public int CacheMaxSize;

            /// <summary>Does the workspace item want to be visible.</summary>
            public bool WorkspaceVisible;

            /// <summary>Has space been allocated for the workspace item.</summary>
            public bool AllocatedSpace;

            /// <summary>Display rectangle for positioning the item.</summary>
            public Rectangle DisplayRect;

            /// <summary>Space allocated for item.</summary>
            public int DisplaySpace;

        }
        #endregion

        #region Type Definintions
        internal class WorkspaceItemToSeparator : Dictionary<IWorkspaceItem, ViewDrawWorkspaceSeparator> { };
        internal class SeparatorToWorkspaceItem : Dictionary<ViewDrawWorkspaceSeparator, IWorkspaceItem> { };
        internal class SeparatorList : List<ViewDrawWorkspaceSeparator> { };
        internal class CellList : List<KryptonWorkspaceCell> { };
        internal class PageList : List<KryptonPage> { };
        internal class ControlList : List<Control> { };
        #endregion

        #region Instance Fields
        // Internal fields
        private ViewDrawPanel _drawPanel;
        private SeparatorStyle _separatorStyle;
        private WorkspaceItemToSeparator _workspaceToSeparator;
        private CellPageNotify _cellPageNotify;
        private DragManager _dragManager;
        private KryptonPage[] _dragPages;
        private NeedPaintHandler _separatorNeedPaint;
        private int _suspendWorkspace;
        private int _suspendActivePageChangedEvent;
        private int _cacheCellCount;
        private int _cacheCellVisibleCount;

        // Exposed fields
        private KryptonWorkspaceSequence _root;
        private PaletteSplitContainerRedirect _stateCommon;
        private PaletteSplitContainer _stateDisabled;
        private PaletteSplitContainer _stateNormal;
        private PaletteSeparatorPadding _stateTracking;
        private PaletteSeparatorPadding _statePressed;
        private WorkspaceMenus _paletteMenus;
        private CompactFlags _compactFlags;
        private IDragPageNotify _dragPageNotify;
        private KryptonWorkspaceCell _maximizedCell;
        private KryptonWorkspaceCell _activeCell;
        private KryptonPage _activePage;
        private bool _allowResizing;
        private bool _allowPageDrag;
        private bool _showMaximizeButton;
        private int _splitterWidth;

        // Page level context menu items
        private KryptonContextMenuItems _menuItems;
        private KryptonContextMenuSeparator _menuSeparator1;
        private KryptonContextMenuItem _menuClose;
        private KryptonContextMenuItem _menuCloseAllButThis;
        private KryptonContextMenuSeparator _menuSeparator2;
        private KryptonContextMenuItem _menuMoveNext;
        private KryptonContextMenuItem _menuMovePrevious;
        private KryptonContextMenuSeparator _menuSeparator3;
        private KryptonContextMenuItem _menuSplitVert;
        private KryptonContextMenuItem _menuSplitHorz;
        private KryptonContextMenuSeparator _menuSeparator4;
        private KryptonContextMenuItem _menuMaximizeRestore;
        private KryptonContextMenuSeparator _menuSeparator5;
        private KryptonContextMenuItem _menuRebalance;
        private KryptonWorkspaceCell _menuCell;
        private KryptonPage _menuPage;
        #endregion

        #region Events
        /// <summary>
        /// Occurs after the number of cells has changed.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs after the number of cells has changed.")]
        public event EventHandler CellCountChanged;

        /// <summary>
        /// Occurs after the number of visible cells has changed.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs after the number of visible cells has changed.")]
        public event EventHandler CellVisibleCountChanged;

        /// <summary>
        /// Occurs when a new KryptonWorkspaceCell instance is about to be added to the workspace.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when a new KryptonWorkspaceCell instance is about to be added to the workspace.")]
        public event EventHandler<WorkspaceCellEventArgs> WorkspaceCellAdding;

        /// <summary>
        /// Occurs when an existing KryptonWorkspaceCell instance has been removed from the workspace.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when an existing KryptonWorkspaceCell instance has been removed from the workspace.")]
        public event EventHandler<WorkspaceCellEventArgs> WorkspaceCellRemoved;

        /// <summary>
        /// Occurs when the active cell value has changed.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when the active cell value has changed.")]
        public event EventHandler<ActiveCellChangedEventArgs> ActiveCellChanged;

        /// <summary>
        /// Occurs when the active page value has changed.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when the active page value has changed.")]
        public event EventHandler<ActivePageChangedEventArgs> ActivePageChanged;

        /// <summary>
        /// Occurs when the maximized cell value has changed.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when the maximized cell value has changed.")]
        public event EventHandler MaximizedCellChanged;

        /// <summary>
        /// Occurs when the workspace information is saving.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when workspace layout information is saving.")]
        public event EventHandler<XmlSavingEventArgs> GlobalSaving;

        /// <summary>
        /// Occurs when the workspace information is loading.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when workspace layout information is loading.")]
        public event EventHandler<XmlLoadingEventArgs> GlobalLoading;

        /// <summary>
        /// Occurs when the workspace cell page information is saving.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when workspace cell page information is saving.")]
        public event EventHandler<PageSavingEventArgs> PageSaving;

        /// <summary>
        /// Occurs when the workspace cell page information is loading.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when the workspace cell page information is loading.")]
        public event EventHandler<PageLoadingEventArgs> PageLoading;

        /// <summary>
        /// Occurs when the workspace cell page is loading but there is no existing matching page.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when the workspace cell page is loading but there is no existing matching page.")]
        public event EventHandler<RecreateLoadingPageEventArgs> RecreateLoadingPage;

        /// <summary>
        /// Occurs when the loading process have completed and there are unmatched pages.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when the loading process have completed and there are unmatched pages.")]
        public event EventHandler<PagesUnmatchedEventArgs> PagesUnmatched;

        /// <summary>
        /// Occurs just before a page drag operation is started.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs just before a page drag operation is started.")]
        public event EventHandler<PageDragCancelEventArgs> BeforePageDrag;

        /// <summary>
        /// Occurs after a page drag operation has finished/aborted.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs after a page drag operation has finished/aborted.")]
        public event EventHandler<PageDragEndEventArgs> AfterPageDrag;

        /// <summary>
        /// Occurs when a page is being dropped.
        /// </summary>
        [Category("Workspace")]
        [Description("Occurs when a page is being dropped.")]
        public event EventHandler<PageDropEventArgs> PageDrop;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonWorkspace class.
		/// </summary>
        public KryptonWorkspace()
		{
            _root = new KryptonWorkspaceSequence();
            _root.PropertyChanged += new PropertyChangedEventHandler(OnChildrenPropertyChanged);
            _root.MaximizeRestoreClicked += new EventHandler(OnChildrenMaximizeRestoreClicked);
            _root.WorkspaceControl = this;
            _paletteMenus = new WorkspaceMenus(this);
            _workspaceToSeparator = new WorkspaceItemToSeparator();
            _cellPageNotify = new CellPageNotify(this);
            _compactFlags = CompactFlags.All;
            _allowResizing = true;
            _allowPageDrag = true;
            _showMaximizeButton = true;
            _splitterWidth = 5;
            _cacheCellCount = 0;
            _cacheCellVisibleCount = 0;
            _separatorNeedPaint = new NeedPaintHandler(OnSeparatorNeedsPaint);

            // Create the palette storage
            _stateCommon = new PaletteSplitContainerRedirect(Redirector, PaletteBackStyle.PanelClient,
                                                             PaletteBorderStyle.ControlClient, PaletteBackStyle.SeparatorLowProfile,
                                                             PaletteBorderStyle.SeparatorLowProfile, NeedPaintDelegate);

            _stateDisabled = new PaletteSplitContainer(_stateCommon, _stateCommon.Separator, _stateCommon.Separator, NeedPaintDelegate);
            _stateNormal = new PaletteSplitContainer(_stateCommon, _stateCommon.Separator, _stateCommon.Separator, NeedPaintDelegate);
            _stateTracking = new PaletteSeparatorPadding(_stateCommon.Separator, _stateCommon.Separator, NeedPaintDelegate);
            _statePressed = new PaletteSeparatorPadding(_stateCommon.Separator, _stateCommon.Separator, NeedPaintDelegate);

            // Create view element
            _drawPanel = new ViewDrawPanel(_stateNormal.Back);

            // Create the view manager instance
            ViewManager = new ViewManager(this, _drawPanel);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Allow the children to be removed during dispose
                ((KryptonReadOnlyControls)Controls).AllowRemoveInternal = true;

                // Recurse down the hierarchy and dispose each cell/sequence
                SuspendLayout();
                _root.Dispose();

                // Ensure any drag manager has a chance to cleanup
                if (_dragManager != null)
                {
                    _dragManager.Dispose();
                    _dragManager = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Bindable(false)]
        [DefaultValue(false)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control can accept data that the user drags onto it.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Bindable(false)]
        public override bool AllowDrop
        {
            get { return base.AllowDrop; }
            set { base.AllowDrop = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Bindable(false)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// Gets or sets the foreground color for the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Gets and sets the KryptonContextMenu to show when right clicked.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override KryptonContextMenu KryptonContextMenu
        {
            get { return base.KryptonContextMenu; }
            set { base.KryptonContextMenu = value; }
        }

        /// <summary>
        /// Gets the collection of controls contained within the control.
        /// </summary>
        [Browsable(false)]
        [Bindable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Control.ControlCollection Controls
        {
            get { return base.Controls; }
        }

        /// <summary>
        /// Gets and sets the active cell.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonWorkspaceCell ActiveCell
        {
            get { return _activeCell; }
            
            set 
            { 
                // You cannot set the active cell to 'null' as if we have any cells then one of them
                // must always be the active cell. Active just means the primary cell in the workspace.
                if (value != null)
                    SetActiveCell(value);
            }
        }

        /// <summary>
        /// Gets and the active page.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public KryptonPage ActivePage
        {
            get { return _activePage; }
        }

        /// <summary>
        /// Gets and sets the compacting options to be applied.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines the compacting options to be applied.")]
        [DefaultValue(typeof(CompactFlags), "All")]
        public CompactFlags CompactFlags
        {
            get { return _compactFlags; }

            set
            {
                if (_compactFlags != value)
                {
                    _compactFlags = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the cell to maximize inside the client area.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines which cell should become maximized and so take up the whole client area.")]
        [DefaultValue(null)]
        public KryptonWorkspaceCell MaximizedCell
        {
            get { return _maximizedCell; }

            set
            {
                if (_maximizedCell != value)
                {
                    _maximizedCell = value;
                    OnMaximizedCellChanged(EventArgs.Empty);
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the thickness of the splitters.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines the thickness of the splitters.")]
        [Localizable(true)]
        [DefaultValue(typeof(int), "5")]
        public int SplitterWidth
        {
            get { return _splitterWidth; }

            set
            {
                // Only interested in changes of value
                if (_splitterWidth != value)
                {
                    // Cannot assign a value of less than zero
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("SplitterWidth", "Value cannot be less than zero");

                    // Use new width of the splitter area
                    _splitterWidth = value;

                    // Update to reflect the change
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets if the user can use separators to resize workspace items.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines if the user can use separators to resize workspace items.")]
        [DefaultValue(true)]
        public bool AllowResizing
        {
            get { return _allowResizing; }
            
            set
            {
                if (_allowResizing != value)
                {
                    _allowResizing = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the container background style.
        /// </summary>
        [Category("Visuals")]
        [Description("Container background style.")]
        [DefaultValue(typeof(PaletteBackStyle), "PanelClient")]
        public PaletteBackStyle ContainerBackStyle
        {
            get { return _stateCommon.BackStyle; }

            set
            {
                if (_stateCommon.BackStyle != value)
                {
                    _stateCommon.BackStyle = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets and sets the separator style.
        /// </summary>
        [Category("Visuals")]
        [Description("Separator style.")]
        [DefaultValue(typeof(SeparatorStyle), "Low Profile")]
        public SeparatorStyle SeparatorStyle
        {
            get { return _separatorStyle; }

            set
            {
                if (_separatorStyle != value)
                {
                    _separatorStyle = value;
                    _stateCommon.Separator.SetStyles(_separatorStyle);

                    // Update all the separators to match control state
                    PaletteMetricPadding metricPadding = CommonHelper.SeparatorStyleToMetricPadding(_separatorStyle);
                    foreach (ViewDrawWorkspaceSeparator separator in _drawPanel)
                        separator.MetricPadding = metricPadding;

                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets access to the common split container appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common split container appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSplitContainerRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled split container appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled split container appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSplitContainer StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal split container appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal split container appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSplitContainer StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the hot tracking separator appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSeparatorPadding StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed separator appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed separator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteSeparatorPadding StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the properties for managing the workspace context menus.
        /// </summary>
        [Category("Visuals")]
        [Description("Properties for managing the workspace context menus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public WorkspaceMenus ContextMenus
        {
            get { return _paletteMenus; }
        }

        private bool ShouldSerializeWorkspaceMenus()
        {
            return !_paletteMenus.IsDefault;
        }

        /// <summary>
        /// Gets access to the root sequence.
        /// </summary>
        [Category("Visuals")]
        [Description("Root sequence.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonWorkspaceSequence Root
        {
            get { return _root; }
        }

        /// <summary>
        /// Gets or sets the default setting for allowing the dragging of cells.
        /// </summary>
        [Category("Visuals")]
        [Description("Defines the default setting for allowing the dragging of cells.")]
        [DefaultValue(true)]
        public bool AllowPageDrag
        {
            get { return _allowPageDrag; }
            set { _allowPageDrag = value; }
        }

        /// <summary>
        /// Gets or sets if the maximized/restore button is displayed. 
        /// </summary>
        [Category("Visuals")]
        [Description("Determines if the maximized/restore button is displayed.")]
        [DefaultValue(true)]
        public bool ShowMaximizeButton
        {
            get { return _showMaximizeButton; }

            set
            {
                if (_showMaximizeButton != value)
                {
                    _showMaximizeButton = value;
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Gets the number of of defined pages in the workspace hierarchy.
        /// </summary>
        /// <returns>Number of pages.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PageCount
        {
            get
            {
                int count = 0;

                // Iterate over the workspace hierarchy looking for cells
                KryptonWorkspaceCell cell = FirstCell();
                while (cell != null)
                {
                    count += cell.Pages.Count;
                    cell = NextCell(cell);
                }

                return count;
            }
        }

        /// <summary>
        /// Gets the number of of defined visible pages in the workspace hierarchy.
        /// </summary>
        /// <returns>Number of visible pages.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PageVisibleCount
        {
            get
            {
                int count = 0;

                // Iterate over the workspace hierarchy looking for cells
                KryptonWorkspaceCell cell = FirstVisibleCell();
                while (cell != null)
                {
                    count += cell.Pages.VisibleCount;
                    cell = NextVisibleCell(cell);
                }

                return count;
            }
        }

        /// <summary>
        /// Gets an array of all the pages within the workspace,
        /// </summary>
        /// <returns>Array of all workspace pages.</returns>
        public KryptonPage[] AllPages()
        {
            List<KryptonPage> pages = new List<KryptonPage>();

            KryptonWorkspaceCell cell = FirstCell();
            while (cell != null)
            {
                foreach (KryptonPage page in cell.Pages)
                    pages.Add(page);

                cell = NextCell(cell);
            }
            
            return pages.ToArray();
        }

        /// <summary>
        /// Gets an array of all the visible pages within the workspace,
        /// </summary>
        /// <returns>Array of all workspace visible pages.</returns>
        public KryptonPage[] AllVisiblePages()
        {
            List<KryptonPage> pages = new List<KryptonPage>();

            KryptonWorkspaceCell cell = FirstVisibleCell();
            while (cell != null)
            {
                foreach (KryptonPage page in cell.Pages)
                    if (page.LastVisibleSet)
                        pages.Add(page);

                cell = NextVisibleCell(cell);
            }

            return pages.ToArray();
        }

        /// <summary>
        /// Gets the number of of defined cells in the workspace hierarchy.
        /// </summary>
        /// <returns>Number of cells.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CellCount
        {
            get
            {
                int count = 0;
                
                // Iterate over the workspace hierarchy looking for cells
                KryptonWorkspaceCell cell = FirstCell();
                while (cell != null)
                {
                    count++;
                    cell = NextCell(cell);
                }

                return count;
            }
        }

        /// <summary>
        /// Gets the number of of defined visible cells in the workspace hierarchy.
        /// </summary>
        /// <returns>Number of visible cells.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CellVisibleCount
        {
            get
            {
                int count = 0;

                // Iterate over the workspace hierarchy looking for cells
                KryptonWorkspaceCell cell = FirstVisibleCell();
                while (cell != null)
                {
                    count++;
                    cell = NextVisibleCell(cell);
                }

                return count;
            }
        }

        /// <summary>
        /// Return reference to first workspace cell.
        /// </summary>
        /// <returns>First cell;otherwise null.</returns>
        public KryptonWorkspaceCell FirstCell()
        {
            return RecursiveFindCellInSequence(Root, true, false);
        }

        /// <summary>
        /// Return reference to last workspace cell.
        /// </summary>
        /// <returns>Last cell;otherwise null.</returns>
        public KryptonWorkspaceCell LastCell()
        {
            return RecursiveFindCellInSequence(Root, false, false);
        }

        /// <summary>
        /// Return reference to next cell in hierarchy starting from specified cell.
        /// </summary>
        /// <param name="current">Starting cell.</param>
        /// <returns>Next cell;otherwise null.</returns>
        public KryptonWorkspaceCell NextCell(KryptonWorkspaceCell current)
        {
            // Get parent of the provided cell
            KryptonWorkspaceSequence sequence = current.WorkspaceParent as KryptonWorkspaceSequence;

            // Must have a valid parent sequence
            if (sequence != null)
                return RecursiveFindCellInSequence(sequence, current, true, false);
            else
                return null;
        }

        /// <summary>
        /// Return reference to previous cell in hierarchy starting from specified cell.
        /// </summary>
        /// <param name="current">Starting cell.</param>
        /// <returns>Previous cell;otherwise null.</returns>
        public KryptonWorkspaceCell PreviousCell(KryptonWorkspaceCell current)
        {
            // Get parent of the provided cell
            KryptonWorkspaceSequence sequence = current.WorkspaceParent as KryptonWorkspaceSequence;

            // Must have a valid parent sequence
            if (sequence != null)
                return RecursiveFindCellInSequence(sequence, current, false, false);
            else
                return null;
        }

        /// <summary>
        /// Determine if reference to cell exists in current hierarchy.
        /// </summary>
        /// <returns>True if it exists; otherwise false.</returns>
        public bool IsCellPresent(KryptonWorkspaceCell cell)
        {
            return RecursiveSearchCellInSequence(Root, cell);
        }

        /// <summary>
        /// Return reference to first visible workspace cell.
        /// </summary>
        /// <returns>First cell;otherwise null.</returns>
        public KryptonWorkspaceCell FirstVisibleCell()
        {
            return RecursiveFindCellInSequence(Root, true, true);
        }

        /// <summary>
        /// Return reference to last visible workspace cell.
        /// </summary>
        /// <returns>Last cell;otherwise null.</returns>
        public KryptonWorkspaceCell LastVisibleCell()
        {
            return RecursiveFindCellInSequence(Root, false, true);
        }

        /// <summary>
        /// Return reference to next visible cell in hierarchy starting from specified cell.
        /// </summary>
        /// <param name="current">Starting cell.</param>
        /// <returns>Next cell;otherwise null.</returns>
        public KryptonWorkspaceCell NextVisibleCell(KryptonWorkspaceCell current)
        {
            // Get parent of the provided cell
            KryptonWorkspaceSequence sequence = current.WorkspaceParent as KryptonWorkspaceSequence;

            // Must have a valid parent sequence
            if (sequence != null)
                return RecursiveFindCellInSequence(sequence, current, true, true);
            else
                return null;
        }

        /// <summary>
        /// Return reference to previous visible cell in hierarchy starting from specified cell.
        /// </summary>
        /// <param name="current">Starting cell.</param>
        /// <returns>Previous cell;otherwise null.</returns>
        public KryptonWorkspaceCell PreviousVisibleCell(KryptonWorkspaceCell current)
        {
            // Get parent of the provided cell
            KryptonWorkspaceSequence sequence = current.WorkspaceParent as KryptonWorkspaceSequence;

            // Must have a valid parent sequence
            if (sequence != null)
                return RecursiveFindCellInSequence(sequence, current, false, true);
            else
                return null;
        }

        /// <summary>
        /// Find the cell that contains the provided page.
        /// </summary>
        /// <param name="page">Page to search for.</param>
        /// <returns>Cell containing page;otherwise null.</returns>
        public KryptonWorkspaceCell CellForPage(KryptonPage page)
        {
            // Default to having not found the page
            KryptonWorkspaceCell ret = null;

            // Do we have a valid page to look for?
            if (page != null)
            {
                // Start search from first cell
                KryptonWorkspaceCell examine = FirstCell();

                // Keep going till all cells examined
                while (examine != null)
                {
                    // Is this page inside the this cell?
                    if (examine.Pages.Contains(page))
                    {
                        // Exit with found group
                        ret = examine;
                        break;
                    }

                    // Move to next cell in sequence
                    examine = NextCell(examine);
                }
            }

            return ret;
        }

        /// <summary>
        /// Find the cell that has a page with the contained unique name.
        /// </summary>
        /// <param name="uniqueName">Unique name to search for.</param>
        /// <returns>Cell containing unique name;otherwise null.</returns>
        public KryptonWorkspaceCell CellForUniqueName(string uniqueName)
        {
            // Do we have a valid string to search for?
            if (!string.IsNullOrEmpty(uniqueName))
            {
                // Scan all the cells in turn
                KryptonWorkspaceCell cell = FirstCell();
                while (cell != null)
                {
                    foreach (KryptonPage page in cell.Pages)
                        if (uniqueName == page.UniqueName)
                            return cell;

                    // Move to next cell in sequence
                    cell = NextCell(cell);
                }
            }

            return null;
        }

        /// <summary>
        /// Find the page with the contained unique name.
        /// </summary>
        /// <param name="uniqueName">Unique name to search for.</param>
        /// <returns>Page containing unique name;otherwise null.</returns>
        public KryptonPage PageForUniqueName(string uniqueName)
        {
            // Do we have a valid string to search for?
            if (!string.IsNullOrEmpty(uniqueName))
            {
                // Scan all the cells in turn
                KryptonWorkspaceCell cell = FirstCell();
                while (cell != null)
                {
                    foreach (KryptonPage page in cell.Pages)
                        if (uniqueName == page.UniqueName)
                            return page;

                    // Move to next cell in sequence
                    cell = NextCell(cell);
                }
            }

            return null;
        }

        /// <summary>
        /// Set the visible state of all the pages in the workspace to hidden.
        /// </summary>
        public void HideAllPages()
        {
            UpdateAllPagesVisible(false, null);
        }

        /// <summary>
        /// Set the visible state of all the pages in the workspace to hidden.
        /// </summary>
        /// <param name="excludeType">Pages of this type are excluded from being updated.</param>
        public void HideAllPages(Type excludeType)
        {
            UpdateAllPagesVisible(false, excludeType);
        }

        /// <summary>
        /// Set the visible state of all the pages in the workspace to showing.
        /// </summary>
        public void ShowAllPages()
        {
            UpdateAllPagesVisible(true, null);
        }

        /// <summary>
        /// Set the visible state of all the pages in the workspace to showing.
        /// </summary>
        /// <param name="excludeType">Pages of this type are excluded from being updated.</param>
        public void ShowAllPages(Type excludeType)
        {
            UpdateAllPagesVisible(true, excludeType);
        }

        /// <summary>
        /// Can the provided page be closed using the same logic as the close button on the cell.
        /// </summary>
        /// <param name="page">Page to test.</param>
        /// <returns>True if it can be closed; otherwise false.</returns>
        public bool CanClosePage(KryptonPage page)
        {
            // If we can find a cell in our workspace that contains the page...
            KryptonWorkspaceCell cellForPage = CellForPage(page);
            if (cellForPage != null)
            {
                // Only only close it if the returned action is not 'None'
                return (cellForPage.Button.CloseButtonAction != CloseButtonAction.None);
            }
            else
                return false;
        }

        /// <summary>
        /// Can the provided page be moved to the next cell.
        /// </summary>
        /// <param name="page">Page to test.</param>
        /// <returns>True if it can be moved to the next cell; otherwise false.</returns>
        public bool CanMovePageNext(KryptonPage page)
        {
            // If we can find a cell in our workspace that contains the page...
            KryptonWorkspaceCell cellForPage = CellForPage(page);
            if (cellForPage != null)
                return (NextCell(cellForPage) != null);
            else
                return false;
        }

        /// <summary>
        /// Can the provided page be moved to the previous cell.
        /// </summary>
        /// <param name="page">Page to test.</param>
        /// <returns>True if it can be moved to the previous cell; otherwise false.</returns>
        public bool CanMovePagePrevious(KryptonPage page)
        {
            // If we can find a cell in our workspace that contains the page...
            KryptonWorkspaceCell cellForPage = CellForPage(page);
            if (cellForPage != null)
                return (PreviousCell(cellForPage) != null);
            else
                return false;
        }

        /// <summary>
        /// Close the page using the same logic as the close button on the cell. 
        /// </summary>
        /// <param name="page">Page to close.</param>
        public void ClosePage(KryptonPage page)
        {
            if (CanClosePage(page))
            {
                KryptonWorkspaceCell cellForPage = CellForPage(page);
                cellForPage.PerformCloseAction(page);
            }
        }

        /// <summary>
        /// Close all the pages except the one provided using the same logic as the close button on the cell. 
        /// </summary>
        /// <param name="page">Page to close.</param>
        public void CloseAllButThisPage(KryptonPage page)
        {
            // Create a list of all pages in the workspace
            KryptonPageCollection pages = CopyToPageCollection();

            // Attempt a close of all pages except the provided one
            foreach (KryptonPage closePage in pages)
                if ((closePage != page) && CanClosePage(closePage))
                {
                    KryptonWorkspaceCell cellForPage = CellForPage(closePage);
                    cellForPage.PerformCloseAction(closePage);
                }
        }

        /// <summary>
        /// Move the provided page to the next cell.
        /// </summary>
        /// <param name="page">Page to move.</param>
        /// <param name="selectPage">Should page be selected once moved.</param>
        public void MovePageNext(KryptonPage page, bool selectPage)
        {
            if (CanMovePageNext(page))
            {
                // Prevent ActivePageChanged when only moving it to another cell
                SuspendActivePageChangedEvent();

                // Grab the cells involved in page movement
                KryptonWorkspaceCell cellForPage = CellForPage(page);
                KryptonWorkspaceCell nextCell = NextCell(cellForPage);

                // Does the cell with the page currently have the focus?
                bool hadFocus = cellForPage.ContainsFocus;

                // Move the page across
                cellForPage.Pages.Remove(page);
                nextCell.Pages.Add(page);

                // Remove any maximized cell setting
                if (MaximizedCell != null)
                    MaximizedCell = null;

                // If requested we can ensure the page is selected in the new cell 
                // but only if the target cell is allowed to have selected tabs
                if (selectPage && nextCell.AllowTabSelect)
                    nextCell.SelectedPage = page;

                ResumeActivePageChangedEvent();

                // If the cell that the page was moved from had the focus then set focus to follow the page
                if (selectPage && nextCell.AllowTabSelect && hadFocus)
                    CellForPage(page).Select();
            }
        }

        /// <summary>
        /// Move the provided page to the previous cell.
        /// </summary>
        /// <param name="page">Page to move.</param>
        /// <param name="selectPage">Should page be selected once moved.</param>
        public void MovePagePrevious(KryptonPage page, bool selectPage)
        {
            if (CanMovePagePrevious(page))
            {
                // Prevent ActivePageChanged when only moving it to another cell
                SuspendActivePageChangedEvent();

                // Grab the cells involved in page movement
                KryptonWorkspaceCell cellForPage = CellForPage(page);
                KryptonWorkspaceCell previousCell = PreviousCell(cellForPage);

                // Does the cell with the page currently have the focus?
                bool hadFocus = cellForPage.ContainsFocus;

                // Move the page across
                cellForPage.Pages.Remove(page);
                previousCell.Pages.Add(page);

                // If requested we can ensure the page is selected in the new cell 
                // but only if the target cell is allowed to have selected tabs
                if (selectPage && previousCell.AllowTabSelect)
                    previousCell.SelectedPage = page;

                // Remove any maximized cell setting
                if (MaximizedCell != null)
                    MaximizedCell = null;

                ResumeActivePageChangedEvent();

                // If the cell that the page was moved from had the focus then set focus to follow the page
                if (selectPage && previousCell.AllowTabSelect && hadFocus)
                    CellForPage(page).Select();
            }
        }

        /// <summary>
        /// Rebalance the star sized items by setting them all to the same 50*,50* value.
        /// </summary>
        public void ApplyRebalance()
        {
            ApplyResizing(Root, "50*", "50*", true, false);
        }

        /// <summary>
        /// Apply new sizing values to each cell and sequence in the workspace hierarchy.
        /// </summary>
        /// <param name="sequence">Root sequence to begin changes from.</param>
        /// <param name="newWidth">New width for items.</param>
        /// <param name="newHeight">New height for items.</param>
        /// <param name="applyStar">Should new width/height be applied to star sized items.</param>
        /// <param name="applyFixed">Should new width/height be applied to fixed sized items.</param>
        public void ApplyResizing(KryptonWorkspaceSequence sequence,
                                  string newWidth,
                                  string newHeight,
                                  bool applyStar,
                                  bool applyFixed)
        {
            bool needLayout = false;

            // Process each item in the sequence
            foreach (Component c in sequence.Children)
            {
                // We can only modify items with the expected interface
                IWorkspaceItem item = c as IWorkspaceItem;
                if (item != null)
                {
                    StarSize itemSize = item.WorkspaceStarSize;
                    
                    // Should the new width be applied?
                    if ((itemSize.StarWidth.UsingStar && applyStar) ||
                        (!itemSize.StarWidth.UsingStar && applyFixed))
                    {
                        if (!itemSize.StarWidth.Value.Equals(newWidth))
                        {
                            itemSize.StarWidth.Value = newWidth;
                            needLayout = true;
                        }
                    }

                    // Should the new height be applied?
                    if ((itemSize.StarHeight.UsingStar && applyStar) ||
                        (!itemSize.StarHeight.UsingStar && applyFixed))
                    {
                        if (!itemSize.StarHeight.Value.Equals(newHeight))
                        {
                            itemSize.StarHeight.Value = newHeight;
                            needLayout = true;
                        }
                    }
                }

                // If the item is itself a sequence
                if (c is KryptonWorkspaceSequence)
                {
                    // Recurse into processing the sequence as well
                    ApplyResizing(c as KryptonWorkspaceSequence,
                                  newWidth, newHeight,
                                  applyStar, applyFixed);
                }
            }

            if (needLayout)
                PerformNeedPaint(true);
        }

        /// <summary>
        /// Move all pages into a new single cell that occupies the entire client area.
        /// </summary>
        public void ApplySingleCell()
        {
            ApplySingleCell(true);
        }

        /// <summary>
        /// Move all pages into a new single cell that occupies the entire client area.
        /// </summary>
        /// <param name="createCellIfNoPages">If there are no pages found should a new root cell be created.</param>
        public void ApplySingleCell(bool createCellIfNoPages)
        {
            // Record the focus state before the changes occur
            bool hasFocus = ContainsFocus;
            Control activePageFocus = (hasFocus ? GetActiverPageControlWithFocus() : null);

            // Do not generate active page change event during method
            SuspendActivePageChangedEvent();

            // Remove any maximized cell setting
            if (MaximizedCell != null)
                MaximizedCell = null;

            // Remember the active page before we rearrange
            KryptonPage activePage = ActivePage;

            // Remove all workspace items and return list of pages
            PageList pages = ClearToPageList();

            // Do we need to create a root cell?
            if (createCellIfNoPages || pages.Count > 0)
            {
                // Create a new cell with entire list of pages as the only workspace item
                KryptonWorkspaceCell cell = new KryptonWorkspaceCell();
                cell.Pages.AddRange(pages.ToArray());
                Root.Children.Add(cell);

                // Make sure the same page is active as it was before the change
                // but only if the target cell is allowed to have selected tabs
                if ((activePage != null) && cell.AllowTabSelect)
                    cell.SelectedPage = activePage;

                // Put focus back to the active page that had it before hand
                if (hasFocus)
                {
                    // Make sure the controls and pages have been added to the display
                    PerformLayout();
                    
                    // Do we set focus back to the control that had it before?
                    if (activePageFocus != null)
                        activePageFocus.Select();
                    else
                        cell.Select();
                }
            }

            ResumeActivePageChangedEvent();
        }

        /// <summary>
        /// Arrange existing cells into a square like grid.
        /// </summary>
        public void ApplyGridCells()
        {
            ApplyGridCells(true);
        }

        /// <summary>
        /// Arrange existing cells into a square like grid.
        /// </summary>
        /// <param name="createCellIfEmpty">Create new cells to fill blank areas of grid.</param>
        public void ApplyGridCells(bool createCellIfEmpty)
        {
            ApplyGridCells(createCellIfEmpty, Orientation.Vertical);
        }

        /// <summary>
        /// Arrange existing cells into a square like grid.
        /// </summary>
        /// <param name="createCellIfEmpty">Create new cells to fill blank areas of grid.</param>
        /// <param name="rootOrientation">Orientation of the root sequence. Vertical creates a list of rows; Horizontal a list of columns.</param>
        public void ApplyGridCells(bool createCellIfEmpty,
                                   Orientation rootOrientation)
        {
            // Create a list of all the cells in the workspace
            CellList cells = CopyToCellList();

            // Find the number of items per sequence to get a square like grid
            int sequenceItems = Math.Max(1, (int)Math.Ceiling(Math.Sqrt(cells.Count)));
            ApplyGridCells(createCellIfEmpty, rootOrientation, sequenceItems);
        }

        /// <summary>
        /// Arrange existing cells into a square like grid.
        /// </summary>
        /// <param name="createCellIfEmpty">Create new cells to fill blank areas of grid.</param>
        /// <param name="rootOrientation">Orientation of the root sequence. Vertical creates a list of rows; Horizontal a list of columns.</param>
        /// <param name="sequenceItems">Number of items per counter orientation sequence.</param>
        public void ApplyGridCells(bool createCellIfEmpty,
                                   Orientation rootOrientation,
                                   int sequenceItems)
        {
            // Record the focus state before the changes occur
            bool hasFocus = ContainsFocus;
            Control activePageFocus = (hasFocus ? GetActiverPageControlWithFocus() : null);

            // Do not generate active page change event during method
            SuspendActivePageChangedEvent();

            // Remove any maximized cell setting
            if (MaximizedCell != null)
                MaximizedCell = null;

            // Remember the active cell before we rearrange
            KryptonWorkspaceCell activeCell = ActiveCell;

            // Must be at least 1 item per sequence
            sequenceItems = Math.Max(1, sequenceItems);

            // Remove entire workspace hierarchy and return list of cells
            CellList cells = ClearToCellList();

            // Do we need to layout/create any cells?
            if (createCellIfEmpty || cells.Count > 0)
            {
                // Change root direction to that specified
                Root.Orientation = rootOrientation;

                if (cells.Count == 0)
                {
                    // If no cells then do we need to create a new empty cell?
                    if (createCellIfEmpty)
                        Root.Children.Add(new KryptonWorkspaceCell());
                }
                else
                {
                    // Root sequences should layout in opposite direction to the root
                    Orientation sequenceDirection = (Root.Orientation == Orientation.Vertical) ? Orientation.Horizontal : Orientation.Vertical;

                    // Create sequences until we have no more cells left
                    while (cells.Count > 0)
                    {
                        // Add a maximum of sequenceItems to the sequence
                        KryptonWorkspaceSequence sequence = new KryptonWorkspaceSequence(sequenceDirection);
                        for (int j = 0; j < sequenceItems; j++)
                        {
                            // If no cells then do we need to create a cell?
                            if (createCellIfEmpty || cells.Count > 0)
                            {
                                KryptonWorkspaceCell cell;

                                // If there are no more cells then create one now
                                if (cells.Count == 0)
                                    sequence.Children.Add(new KryptonWorkspaceCell());
                                else
                                {
                                    cell = cells[0];
                                    cells.RemoveAt(0);
                                    sequence.Children.Add(cell);
                                }
                            }
                        }

                        Root.Children.Add(sequence);
                    }
                }
            }

            // Ensure the active cell is put back the way it started
            if (activeCell != null)
            {
                // Layout is needed to ensure the cells are added as child controls
                PerformLayout();
                ActiveCell = activeCell;

                // Put focus back to the cell or page content that had it before
                if (hasFocus)
                {
                    if (activePageFocus != null)
                        activePageFocus.Select();
                    else
                        ActiveCell.Select();
                }
            }

            ResumeActivePageChangedEvent();
        }

        /// <summary>
        /// Move each page into its own cell and arrange then in a square like grid.
        /// </summary>
        public void ApplyGridPages()
        {
            ApplyGridPages(true);
        }

        /// <summary>
        /// Move each page into its own cell and arrange then in a square like grid.
        /// </summary>
        /// <param name="createCellIfNoPages">If there are no pages found should a new root cell be created.</param>
        public void ApplyGridPages(bool createCellIfNoPages)
        {
            ApplyGridPages(createCellIfNoPages, Orientation.Vertical);
        }

        /// <summary>
        /// Move each page into its own cell and arrange then in a square like grid.
        /// </summary>
        /// <param name="createCellIfNoPages">If there are no pages found should a new root cell be created.</param>
        /// <param name="rootOrientation">Orientation of the root sequence. Vertical creates a list of rows; Horizontal a list of columns.</param>
        public void ApplyGridPages(bool createCellIfNoPages,
                                  Orientation rootOrientation)
        {
            // Create a list of all the pages in the workspace
            PageList pages = CopyToPageList();

            // Find the number of items per sequence to get a square like grid
            int sequenceItems = Math.Max(1, (int)Math.Ceiling(Math.Sqrt(pages.Count)));
            ApplyGridPages(createCellIfNoPages, rootOrientation, sequenceItems);
        }

        /// <summary>
        /// Move each page into its own cell and arrange then in a square like grid.
        /// </summary>
        /// <param name="createCellIfNoPages">If there are no pages found should a new root cell be created.</param>
        /// <param name="rootOrientation">Orientation of the root sequence. Vertical creates a list of rows; Horizontal a list of columns.</param>
        /// <param name="sequenceItems">Number of items per counter orientation sequence.</param>
        public void ApplyGridPages(bool createCellIfNoPages,
                                  Orientation rootOrientation,
                                  int sequenceItems)
        {
            // Record the focus state before the changes occur
            bool hasFocus = ContainsFocus;
            Control activePageFocus = (hasFocus ? GetActiverPageControlWithFocus() : null);

            // Do not generate active page change event during method
            SuspendActivePageChangedEvent();

            // Remove any maximized cell setting
            if (MaximizedCell != null)
                MaximizedCell = null;

            // Remember the active page before we rearrange
            KryptonPage activePage = ActivePage;
            KryptonWorkspaceCell newActiveCell = null;

            // Must be at least 1 item per sequence
            sequenceItems = Math.Max(1, sequenceItems);

            // Remove all workspace items and return list of pages
            PageList pages = ClearToPageList();

            // Do we need to create any cells?
            if (createCellIfNoPages || pages.Count > 0)
            {
                // Change root direction to that specified
                Root.Orientation = rootOrientation;

                if (pages.Count == 0)
                {
                    // If no pages then do we need to create a cell?
                    if (createCellIfNoPages)
                        Root.Children.Add(new KryptonWorkspaceCell());
                }
                else
                {
                    // Root sequences should layout in opposite direction to the root
                    Orientation sequenceDirection = (Root.Orientation == Orientation.Vertical) ? Orientation.Horizontal : Orientation.Vertical;

                    // Create sequences until we have no more pages left
                    while (pages.Count > 0)
                    {
                        // Add a maximum of sequenceItems to the sequence
                        KryptonWorkspaceSequence sequence = new KryptonWorkspaceSequence(sequenceDirection);
                        for (int j = 0; j < sequenceItems; j++)
                        {
                            // If no pages then do we need to create a cell?
                            if (createCellIfNoPages || pages.Count > 0)
                            {
                                KryptonWorkspaceCell cell = new KryptonWorkspaceCell();

                                // Add the first cell in the list to the cell
                                if (pages.Count > 0)
                                {
                                    cell.Pages.Add(pages[0]);
                                    pages.RemoveAt(0);

                                    // Note the new cell that has the previously active page
                                    if (cell.Pages.Contains(activePage))
                                        newActiveCell = cell;
                                }

                                sequence.Children.Add(cell);
                            }
                        }

                        Root.Children.Add(sequence);
                    }
                }
            }

            // Make active the new cell that contains the page that was active before the update
            if (newActiveCell != null)
            {
                // Layout is needed to ensure the cells are added as child controls
                PerformLayout();
                ActiveCell = newActiveCell;

                // Put focus back to the cell or page content that had it before
                if (hasFocus)
                {
                    if (activePageFocus != null)
                        activePageFocus.Select();
                    else
                        ActiveCell.Select();
                }
            }

            ResumeActivePageChangedEvent();
        }

        /// <summary>
        /// Remove all pages from all the cells.
        /// </summary>
        public void ClearAllPages()
        {
            KryptonWorkspaceCell cell = FirstCell();
            while (cell != null)
            {
                cell.Pages.Clear();
                cell = NextCell(cell);
            }
        }

        /// <summary>
        /// Gets and sets the interface for receiving page drag notifications.
        /// </summary>
        [Browsable(false)]
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
        public virtual DragTargetList GenerateDragTargets(PageDragEndData dragEndData)
        {
            return GenerateDragTargets(dragEndData, KryptonPageFlags.All);
        }

        /// <summary>
        /// Generate a list of drag targets that are relevant to the provided end data.
        /// </summary>
        /// <param name="dragEndData">Pages data being dragged.</param>
        /// <param name="allowFlags">Only drop pages that have one of these flags set.</param>
        /// <returns>List of drag targets.</returns>
        public virtual  DragTargetList GenerateDragTargets(PageDragEndData dragEndData, KryptonPageFlags allowFlags)
        {
            DragTargetList targets = new DragTargetList();

            int visibleCells = 0;
            int numPages = 0;

            if (MaximizedCell != null)
            {
                // Generate targets for maximized cell only
                visibleCells = CellVisibleCount;
                numPages += MaximizedCell.Pages.VisibleCount;
                GenerateCellDragTargets(MaximizedCell, targets, allowFlags);
            }
            else
            {
                // Generate targets for each visible cell
                KryptonWorkspaceCell cell = FirstVisibleCell();
                while (cell != null)
                {
                    if (cell.WorkspaceVisible)
                    {
                        visibleCells++;
                        numPages += cell.Pages.VisibleCount;
                        GenerateCellDragTargets(cell, targets, allowFlags);
                    }

                    cell = NextVisibleCell(cell);
                }
            }

            // We do not allow docking at control edges if there are no pages and we are compacting away empty cells. As that would
            // result in compacting away the empty cell and leaving just the dropped created cell. Instead it would be better to allow
            // just the transfer to the original empty cell.
            bool preventEdges = ((numPages == 0) && (!DesignMode && (CompactFlags & CompactFlags.RemoveEmptyCells) == CompactFlags.RemoveEmptyCells));

            // If the root sequence is visible then there must be at least one cell showing
            Rectangle screenRect = RectangleToScreen(ClientRectangle);
            if ((visibleCells > 0) && !preventEdges)
            {
                // With a single visible cell there is no need for edge dockers on the whole workspace as the edge docking
                // against the single cell will perform the same action anyway.
                if (visibleCells > 1)
                {
                    // Generate targets for the four control edges
                    Rectangle[] rectsDraw = SubdivideRectangle(screenRect, 3, int.MaxValue);
                    Rectangle[] rectsHot = SubdivideRectangle(screenRect, 10, 20);

                    // Must insert at start of target list as they are higher priority than cell targets
                    targets.Insert(0, new DragTargetWorkspaceEdge(screenRect, rectsHot[0], rectsDraw[0], DragTargetHint.EdgeLeft | DragTargetHint.ExcludeCluster, this, allowFlags));
                    targets.Insert(1, new DragTargetWorkspaceEdge(screenRect, rectsHot[1], rectsDraw[1], DragTargetHint.EdgeRight | DragTargetHint.ExcludeCluster, this, allowFlags));
                    targets.Insert(2, new DragTargetWorkspaceEdge(screenRect, rectsHot[2], rectsDraw[2], DragTargetHint.EdgeTop | DragTargetHint.ExcludeCluster, this, allowFlags));
                    targets.Insert(3, new DragTargetWorkspaceEdge(screenRect, rectsHot[3], rectsDraw[3], DragTargetHint.EdgeBottom | DragTargetHint.ExcludeCluster, this, allowFlags));
                }
            }
            else
            {
                // No cell showing so need to show as a transfer into the whole control
                targets.Insert(0, new DragTargetWorkspaceEdge(screenRect, screenRect, screenRect, DragTargetHint.Transfer | DragTargetHint.ExcludeCluster, this, allowFlags));
            }
            
            return targets;
        }

        /// <summary>
        /// Saves workspace layout information into an array of bytes using Unicode Encoding.
        /// </summary>
        /// <returns>Array of created bytes.</returns>
        public byte[] SaveLayoutToArray()
        {
            return SaveLayoutToArray(Encoding.Unicode);
        }

        /// <summary>
        /// Saves workspace layout information into an array of bytes.
        /// </summary>
        /// <param name="encoding">Required encoding.</param>
        /// <returns>Array of created bytes.</returns>
        public byte[] SaveLayoutToArray(Encoding encoding)
        {
            // Save into the file stream
            MemoryStream ms = new MemoryStream();
            SaveLayoutToStream(ms, encoding);
            ms.Close();

            // Return an array of bytes that contain the streamed XML
            return ms.GetBuffer();
        }

        /// <summary>
        /// Saves workspace layout information into a named file using Unicode Encoding.
        /// </summary>
        /// <param name="filename">Name of file to save to.</param>
        public void SaveLayoutToFile(string filename)
        {
            SaveLayoutToFile(filename, Encoding.Unicode);
        }

        /// <summary>
        /// Saves workspace layout information into a named file.
        /// </summary>
        /// <param name="filename">Name of file to save to.</param>
        /// <param name="encoding">Required encoding.</param>
        public void SaveLayoutToFile(string filename, Encoding encoding)
        {
            // Create/Overwrite existing file
            FileStream fs = new FileStream(filename, FileMode.Create);

            try
            {
                // Save into the file stream
                SaveLayoutToStream(fs, encoding);
            }
            catch
            {
                fs.Close();
            }
        }

        /// <summary>
        /// Saves workspace layout information into a stream object.
        /// </summary>
        /// <param name="stream">Stream object.</param>
        /// <param name="encoding">Required encoding.</param>
        public void SaveLayoutToStream(Stream stream, Encoding encoding)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(stream, encoding);

            // Use indenting for readability
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();
            SaveLayoutToXml(xmlWriter);
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        /// <summary>
        /// Saves workspace layout information using a provider xml writer.
        /// </summary>
        /// <param name="xmlWriter">Xml writer object.</param>
        public void SaveLayoutToXml(XmlWriter xmlWriter)
        {
            // Remember the current culture setting
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            try
            {
                // Associate a version number with the root element so that future versions of the code
                // will be able to be backwards compatible or at least recognise incompatible versions
                xmlWriter.WriteStartElement("KW");
                xmlWriter.WriteAttributeString("V", "1");

                // Remember which page was the active one
                if (ActivePage != null)
                    xmlWriter.WriteAttributeString("A", ActivePage.UniqueName);
                else
                    xmlWriter.WriteAttributeString("A", "(null)");

                // Give event handlers chance to embed custom data
                xmlWriter.WriteStartElement("CGD");
                OnGlobalSaving(new XmlSavingEventArgs(this, xmlWriter));
                xmlWriter.WriteEndElement();

                // The root saves itself and all children recursively
                Root.SaveToXml(this, xmlWriter);

                // Terminate the root element and document        
                xmlWriter.WriteEndElement();
            }
            finally
            {
                // Put back the old culture before exiting routine
                Thread.CurrentThread.CurrentCulture = culture;
            }
        }

		/// <summary>
		/// Loads workspace layout information from given array of bytes.
		/// </summary>
		/// <param name="buffer">Array of source bytes.</param>
        public void LoadLayoutFromArray(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            LoadLayoutFromStream(ms);
            ms.Close();
        }

		/// <summary>
		/// Loads workspace layout information from given filename.
		/// </summary>
		/// <param name="filename">Name of file to read from.</param>
        public void LoadLayoutFromFile(string filename)
        {
            // Open existing file
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			
			try
			{
				// Load from the file stream
				LoadLayoutFromStream(fs);		
			}
			catch
			{
				// Must remember to close
				fs.Close();
			}
        }

		/// <summary>
		/// Loads workspace layout information from given stream object.
		/// </summary>
		/// <param name="stream">Stream object.</param>
        public void LoadLayoutFromStream(Stream stream)
        {
            XmlTextReader xmlReader = new XmlTextReader(stream);
            xmlReader.WhitespaceHandling = WhitespaceHandling.None;
            xmlReader.MoveToContent();

            // Use existing method to load from xml
            LoadLayoutFromXml(xmlReader);
            xmlReader.Close();			
        }

		/// <summary>
		/// Loads workspace layout information using the provided xml reader.
		/// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        public void LoadLayoutFromXml(XmlReader xmlReader)
        {
            LoadLayoutFromXml(xmlReader, CopyToPageCollection());
        }
        
		/// <summary>
		/// Loads workspace layout information using the provided xml reader.
		/// </summary>
        /// <param name="xmlReader">Xml reader object.</param>
        /// <param name="availablePages">List of pages available for use when loading.</param>
        public void LoadLayoutFromXml(XmlReader xmlReader, KryptonPageCollection availablePages)
        {
            // Remember the current culture setting
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            try
            {
                // Double check this has the correct element name
                if (xmlReader.Name != "KW")
                    throw new ArgumentException("Root element must be named 'KW'");

                // Load the format version number
                string version = xmlReader.GetAttribute("V");
                string activePageUniqueName = xmlReader.GetAttribute("A");

                // Convert format version from string to double
                int formatVersion = (int)Convert.ToDouble(version);
                
                // We can only load 1 upward version formats
                if (formatVersion < 1)
                    throw new ArgumentException("Can only load Version 1 and upwards of KryptonWorkspace persisted data.");

                ScreenObscurer obscurer = new ScreenObscurer();

                try
                {
                    // Cover up the entire workspace area so controls being added/removed do not show as redrawn
                    obscurer.Cover(this);

                    // Prevent change events because of adding pages and changing active pages within cells
                    SuspendActivePageChangedEvent();

                    // Prevent compacting and reposition of children
                    BeginInit();

                    // Create a look for all the existing pages so loading can potentially reuse them
                    UniqueNameToPage existingPages = BuildUniqueNameDictionary(availablePages);

                    // Remove all existing contents
                    _root.Children.Clear();

                    // Read to custom data element
                    if (!xmlReader.Read())
                        throw new ArgumentException("An element was expected but could not be read in.");

                    if (xmlReader.Name != "CGD")
                        throw new ArgumentException("Expected 'CGD' element was not found.");

                    bool finished = xmlReader.IsEmptyElement;

                    // Give handlers chance to reload custom saved data
                    OnGlobalLoading(new XmlLoadingEventArgs(this, xmlReader));

                    // Read everything until we get the end of custom data marker
                    while (!finished)
                    {
                        // Check it has the expected name
                        if (xmlReader.NodeType == XmlNodeType.EndElement)
                            finished = (xmlReader.Name == "CGD");

                        if (!finished)
                        {
                            if (!xmlReader.Read())
                                throw new ArgumentException("An element was expected but could not be read in.");
                        }
                    }

                    // Read the next well known element
                    if (!xmlReader.Read())
                        throw new ArgumentException("An element was expected but could not be read in.");

                    // Is it the expected element?
                    if (xmlReader.Name != "WS")
                        throw new ArgumentException("Element 'WS' was expected but not found.");

                    // Reload the root sequence
                    Root.LoadFromXml(this, xmlReader, existingPages);

                    // Move past the end element
                    if (!xmlReader.Read())
                        throw new ArgumentException("Could not read in next expected node.");

                    // Check it has the expected name
                    if (xmlReader.NodeType != XmlNodeType.EndElement)
                        throw new ArgumentException("EndElement expected but not found.");

                    // Are there any unmatched pages?
                    if (existingPages.Count > 0)
                    {
                        // Create list of the pages unmatched with loaded info
                        List<KryptonPage> unmatched = new List<KryptonPage>();
                        foreach (KryptonPage page in existingPages.Values)
                            unmatched.Add(page);

                        // Allow the unmatched pages to be processed by event handlers
                        OnPagesUnmatched(new PagesUnmatchedEventArgs(this, unmatched));                    
                    }

                    // Update incoming collection with new entries because of RecreateLoadingPage events
                    foreach (KryptonPage page in existingPages.Values)
                        if (!availablePages.Contains(page))
                            availablePages.Add(page);
                }
                finally
                {
                    // Allow normal operation
                    EndInit();

                    // Restore active cell
                    KryptonWorkspaceCell activeCell = CellForUniqueName(activePageUniqueName);
                    if (activeCell != null)
                        ActiveCell = activeCell;

                    // Turn back on change events now that everything has settled down
                    ResumeActivePageChangedEvent();

                    // Layout will automatically remove unwanted controls and layout new controls
                    PerformLayout();

                    // Remove workspace cover so redrawing can now occur with the finished new layout
                    obscurer.Uncover();
                    obscurer.Dispose();
                }
            }
            finally
            {
                // Put back the old culture before exiting routine
                Thread.CurrentThread.CurrentCulture = culture;
            }
        }

        /// <summary>
        /// Write cell details to xml during save process.
        /// </summary>
        /// <param name="xmlWriter">XmlWriter to use for saving.</param>
        /// <param name="cell">Reference to cell.</param>
        public virtual void WriteCellElement(XmlWriter xmlWriter, KryptonWorkspaceCell cell)
        {
            xmlWriter.WriteAttributeString("UN", cell.UniqueName);
            xmlWriter.WriteAttributeString("S", cell.WorkspaceStarSize.PersistString);
            xmlWriter.WriteAttributeString("NM", cell.NavigatorMode.ToString());
            CommonHelper.TextToXmlAttribute(xmlWriter, "UM", CommonHelper.BoolToString(cell.UseMnemonic), "True");
            CommonHelper.TextToXmlAttribute(xmlWriter, "ATF", CommonHelper.BoolToString(cell.AllowTabFocus), "False");
            CommonHelper.TextToXmlAttribute(xmlWriter, "APD", CommonHelper.BoolToString(cell.AllowPageDrag), "True");
            CommonHelper.TextToXmlAttribute(xmlWriter, "AR", CommonHelper.BoolToString(cell.AllowResizing), "False");
            CommonHelper.TextToXmlAttribute(xmlWriter, "E", CommonHelper.BoolToString(cell.Enabled), "True");
            CommonHelper.TextToXmlAttribute(xmlWriter, "V", CommonHelper.BoolToString(cell.LastVisibleSet), "True");
            CommonHelper.TextToXmlAttribute(xmlWriter, "DOR", CommonHelper.BoolToString(cell.DisposeOnRemove), "True");
            CommonHelper.TextToXmlAttribute(xmlWriter, "MINS", CommonHelper.SizeToString(cell.MinimumSize), "0, 0");
            CommonHelper.TextToXmlAttribute(xmlWriter, "MAXS", CommonHelper.SizeToString(cell.MaximumSize), "0, 0");

            // Remember which page was the active one
            if (cell.SelectedPage != null)
                xmlWriter.WriteAttributeString("SP", cell.SelectedPage.UniqueName);
            else
                xmlWriter.WriteAttributeString("SP", "(null)");
        }

        /// <summary>
        /// Read cell details from xml during load process.
        /// </summary>
        /// <param name="xmlReader">XmlReader to use for loading.</param>
        /// <param name="cell">Reference to cell.</param>
        /// <returns>Unique name of the selected page inside the cell.</returns>
        public virtual string ReadCellElement(XmlReader xmlReader, KryptonWorkspaceCell cell)
        {
            // Grab the mandatory attributes
            cell.UniqueName = xmlReader.GetAttribute("UN");
            cell.WorkspaceStarSize.PersistString = xmlReader.GetAttribute("S");
            string selectedPageUniqueName = xmlReader.GetAttribute("SP");
            cell.NavigatorMode = (NavigatorMode)Enum.Parse(typeof(NavigatorMode), xmlReader.GetAttribute("NM"));
            cell.UseMnemonic = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "UM", "True"));
            cell.AllowTabFocus = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "ATF", "False"));
            cell.AllowPageDrag = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "APD", "True"));
            cell.AllowResizing = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "AR", "False"));
            cell.Enabled = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "E", "True"));
            cell.Visible = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "V", "True"));
            cell.DisposeOnRemove = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "DOR", "True"));
            cell.MinimumSize = CommonHelper.StringToSize(CommonHelper.XmlAttributeToText(xmlReader, "MINS", "0, 0"));
            cell.MaximumSize = CommonHelper.StringToSize(CommonHelper.XmlAttributeToText(xmlReader, "MAXS", "0, 0"));
            return selectedPageUniqueName;
        }

        /// <summary>
        /// Write sequence details to xml during save process.
        /// </summary>
        /// <param name="xmlWriter">XmlWriter to use for saving.</param>
        /// <param name="sequence">Reference to sequence.</param>
        public virtual void WriteSequenceElement(XmlWriter xmlWriter, KryptonWorkspaceSequence sequence)
        {
            xmlWriter.WriteAttributeString("UN", sequence.UniqueName);
            xmlWriter.WriteAttributeString("S", sequence.WorkspaceStarSize.PersistString);
            xmlWriter.WriteAttributeString("D", sequence.Orientation.ToString());
        }

        /// <summary>
        /// Read sequence details from xml during load process.
        /// </summary>
        /// <param name="xmlReader">XmlReader to use for loading.</param>
        /// <param name="sequence">Reference to sequence.</param>
        public virtual void ReadSequenceElement(XmlReader xmlReader, KryptonWorkspaceSequence sequence)
        {
            sequence.UniqueName = xmlReader.GetAttribute("UN");
            sequence.WorkspaceStarSize.PersistString = xmlReader.GetAttribute("S");
            sequence.Orientation = (Orientation)Enum.Parse(typeof(Orientation), xmlReader.GetAttribute("D"));
        }

        /// <summary>
        /// Write page details to xml during save process.
        /// </summary>
        /// <param name="xmlWriter">XmlWriter to use for saving.</param>
        /// <param name="page">Reference to page.</param>
        public virtual void WritePageElement(XmlWriter xmlWriter, KryptonPage page)
        {
            // Write values that can be stored as attributes
            CommonHelper.TextToXmlAttribute(xmlWriter, "T", page.Text);
            CommonHelper.TextToXmlAttribute(xmlWriter, "TT", page.TextTitle);
            CommonHelper.TextToXmlAttribute(xmlWriter, "TD", page.TextDescription);
            CommonHelper.TextToXmlAttribute(xmlWriter, "TTB", page.ToolTipBody);
            CommonHelper.TextToXmlAttribute(xmlWriter, "TTITC", CommonHelper.ColorToString(page.ToolTipImageTransparentColor));
            CommonHelper.TextToXmlAttribute(xmlWriter, "TTS", page.ToolTipStyle.ToString(), "ToolTip");
            CommonHelper.TextToXmlAttribute(xmlWriter, "TTT", page.ToolTipTitle);
            CommonHelper.TextToXmlAttribute(xmlWriter, "UN", page.UniqueName);
            CommonHelper.TextToXmlAttribute(xmlWriter, "E", CommonHelper.BoolToString(page.Enabled), "True");
            CommonHelper.TextToXmlAttribute(xmlWriter, "V", CommonHelper.BoolToString(page.LastVisibleSet), "True");
            CommonHelper.TextToXmlAttribute(xmlWriter, "MINS", CommonHelper.SizeToString(page.MinimumSize), "50, 50");
            CommonHelper.TextToXmlAttribute(xmlWriter, "MAXS", CommonHelper.SizeToString(page.MaximumSize), "0, 0");
            CommonHelper.TextToXmlAttribute(xmlWriter, "AHSS", CommonHelper.SizeToString(page.AutoHiddenSlideSize), "150, 150");
            CommonHelper.TextToXmlAttribute(xmlWriter, "F", page.Flags.ToString());

            // Write out images as child elements
            CommonHelper.ImageToXmlCData(xmlWriter, "IS", page.ImageSmall);
            CommonHelper.ImageToXmlCData(xmlWriter, "IM", page.ImageMedium);
            CommonHelper.ImageToXmlCData(xmlWriter, "IL", page.ImageLarge);
            CommonHelper.ImageToXmlCData(xmlWriter, "TTI", page.ToolTipImage);
        }

        /// <summary>
        /// Read page details from xml during load process.
        /// </summary>
        /// <param name="xmlReader">XmlReader to use for loading.</param>
        /// <param name="uniqueName">Unique name of page being loaded.</param>
        /// <param name="existingPages">Set of existing pages.</param>
        /// <returns>Reference to page to be added into the workspace cell.</returns>
        public virtual KryptonPage ReadPageElement(XmlReader xmlReader,
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
                // Read values that can be stored as attributes
                page.Text = CommonHelper.XmlAttributeToText(xmlReader, "T");
                page.TextTitle = CommonHelper.XmlAttributeToText(xmlReader, "TT");
                page.TextDescription = CommonHelper.XmlAttributeToText(xmlReader, "TD");
                page.ToolTipBody = CommonHelper.XmlAttributeToText(xmlReader, "TTB");
                page.ToolTipImageTransparentColor = CommonHelper.StringToColor(CommonHelper.XmlAttributeToText(xmlReader, "TTITC"));
                page.ToolTipStyle = (LabelStyle)Enum.Parse(typeof(LabelStyle), CommonHelper.XmlAttributeToText(xmlReader, "TTS", "ToolTip"));
                page.ToolTipTitle = CommonHelper.XmlAttributeToText(xmlReader, "TTT");
                page.UniqueName = CommonHelper.XmlAttributeToText(xmlReader, "UN");
                page.Enabled = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "E", "True"));
                page.Visible = CommonHelper.StringToBool(CommonHelper.XmlAttributeToText(xmlReader, "V", "True"));
                page.MinimumSize = CommonHelper.StringToSize(CommonHelper.XmlAttributeToText(xmlReader, "MINS", "50, 50"));
                page.MaximumSize = CommonHelper.StringToSize(CommonHelper.XmlAttributeToText(xmlReader, "MAXS", "0, 0"));
                page.AutoHiddenSlideSize = CommonHelper.StringToSize(CommonHelper.XmlAttributeToText(xmlReader, "AHSS", "150, 150"));
                page.Flags = int.Parse(CommonHelper.XmlAttributeToText(xmlReader, "F", page.Flags.ToString()));
            }

            // Read the next Element
            if (!xmlReader.Read())
                throw new ArgumentException("An element was expected but could not be read in.");

            if (page != null)
            {
                // Load any optional images that are present
                page.ImageSmall = ReadOptionalImageElement(xmlReader, "IS");
                page.ImageMedium = ReadOptionalImageElement(xmlReader, "IM");
                page.ImageLarge = ReadOptionalImageElement(xmlReader, "IL");
                page.ToolTipImage = ReadOptionalImageElement(xmlReader, "TTI");
            }
            else
            {
                // Read past the optional images
                ReadOptionalImageElement(xmlReader, "IS");
                ReadOptionalImageElement(xmlReader, "IM");
                ReadOptionalImageElement(xmlReader, "IL");
                ReadOptionalImageElement(xmlReader, "TTI");
            }

            return page;
        }
        
        /// <summary>
        /// Raises the GlobalSaving event.
        /// </summary>
        /// <param name="e">Event data.</param>
        public virtual void OnGlobalSaving(XmlSavingEventArgs e)
        {
            if (GlobalSaving != null)
                GlobalSaving(this, e);
        }

        /// <summary>
        /// Raises the GlobalLoading event.
        /// </summary>
        /// <param name="e">Event data.</param>
        public virtual void OnGlobalLoading(XmlLoadingEventArgs e)
        {
            if (GlobalLoading != null)
                GlobalLoading(this, e);
        }

        /// <summary>
        /// Raises the PageSaving event.
        /// </summary>
        /// <param name="e">Event data.</param>
        public virtual void OnPageSaving(PageSavingEventArgs e)
        {
            if (PageSaving != null)
                PageSaving(this, e);
        }

        /// <summary>
        /// Raises the PageLoading event.
        /// </summary>
        /// <param name="e">Event data.</param>
        public virtual void OnPageLoading(PageLoadingEventArgs e)
        {
            if (PageLoading != null)
                PageLoading(this, e);
        }

        /// <summary>
        /// Raises the RecreateLoadingPage event.
        /// </summary>
        /// <param name="e">Event data.</param>
        public virtual void OnRecreateLoadingPage(RecreateLoadingPageEventArgs e)
        {
            if (RecreateLoadingPage != null)
                RecreateLoadingPage(this, e);
        }

        /// <summary>
        /// Raises the PagesUnmatched event.
        /// </summary>
        /// <param name="e">Event data.</param>
        public virtual void OnPagesUnmatched(PagesUnmatchedEventArgs e)
        {
            if (PagesUnmatched != null)
                PagesUnmatched(this, e);
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SuspendWorkspaceLayout()
        {
            _suspendWorkspace++;
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResumeWorkspaceLayout()
        {
            _suspendWorkspace--;

            // Must perform immediate layout to ensure the internal state reflects 
            // any changes to the workspace hierarchy that might well have changed
            PerformLayout();
        }

        /// <summary>
        /// Output debug information about the workspace hierarchy.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void DebugOutput()
        {
            Root.DebugOutput(1);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Change has occured in the hierarchy of children.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Arguments associated with the event.</param>
        protected void OnChildrenPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Request to toggle the maximized state.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Arguments associated with the event.</param>
        protected void OnChildrenMaximizeRestoreClicked(object sender, EventArgs e)
        {
            KryptonWorkspaceCell cell = sender as KryptonWorkspaceCell;
            if (cell != null)
            {
                if (MaximizedCell == cell)
                    MaximizedCell = null;
                else
                    MaximizedCell = cell;
            }
        }
        #endregion

        #region Protected Overrides
        /// <summary>
        /// Creates a new instance of the control collection for the control.
        /// </summary>
        /// <returns>A new instance of KryptonNavigatorControlCollection assigned to the control.</returns>
        protected override ControlCollection CreateControlsInstance()
        {
            // User should never adds controls directly to collection, only via the workspace hierarchy
            return new KryptonReadOnlyControls(this);
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(250, 250); }
        }

        /// <summary>
        /// Activates a child control. Optionally specifies the direction in the tab order to select the control from.
        /// </summary>
        /// <param name="directed">true to specify the direction of the control to select; otherwise, false.</param>
        /// <param name="forward">true to move forward in the tab order; false to move backward in the tab order.</param>
        protected override void Select(bool directed, bool forward)
        {
            if (!directed)
            {
                if (ActiveCell != null)
                    ActiveCell.Select();
            }
            else
                base.Select(directed, forward);
        }

		/// <summary>
		/// Processes a dialog key.
		/// </summary>
		/// <returns>true if key processed; otherwise false.</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Only if enabled and with an active cell/page are we able to do something with keys
            if (Enabled && (ActiveCell != null) && (ActivePage != null))
            {
                if (keyData.Equals(ContextMenus.ShortcutClose))
                {
                    ClosePage(ActivePage);
                    return true;
                }
                else if (keyData.Equals(ContextMenus.ShortcutCloseAllButThis))
                {
                    CloseAllButThisPage(ActivePage);
                    return true;
                }
                else if (keyData.Equals(ContextMenus.ShortcutMoveNext))
                {
                    MovePageNext(ActivePage, true);
                    return true;
                }
                else if (keyData.Equals(ContextMenus.ShortcutMovePrevious))
                {
                    MovePagePrevious(ActivePage, true);
                    return true;
                }
                else if (keyData.Equals(ContextMenus.ShortcutSplitVertical))
                {
                    if (ActiveCell.Pages.VisibleCount > 1)
                    {
                        PageSplitDirection(ActiveCell, ActivePage, Orientation.Vertical);
                        return true;
                    }
                }
                else if (keyData.Equals(ContextMenus.ShortcutSplitHorizontal))
                {
                    if (ActiveCell.Pages.VisibleCount > 1)
                    {
                        PageSplitDirection(ActiveCell, ActivePage, Orientation.Horizontal);
                        return true;
                    }
                }
                else if (keyData.Equals(ContextMenus.ShortcutRebalance))
                {
                    if (CellVisibleCount > 1)
                    {
                        ApplyRebalance();
                        return true;
                    }
                }
                else if (keyData.Equals(ContextMenus.ShortcutMaximizeRestore))
                {
                    if (MaximizedCell != null)
                        MaximizedCell = null;
                    else if (ActiveCell != null)
                        MaximizedCell = ActiveCell;
                    return true;
                }
            }

            // We have not intercepted key press
            return base.ProcessDialogKey(keyData);
        }
        
        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            // If we have an active cell then shift focus to it
            if (ActiveCell != null)
                ActiveCell.Select();

            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises the EnabledChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            // Push correct palettes into the view
            if (Enabled)
                _drawPanel.SetPalettes(_stateNormal.Back);
            else
                _drawPanel.SetPalettes(_stateDisabled.Back);

            _drawPanel.Enabled = Enabled;

            // Update all the separators to match control state
            foreach (ViewDrawWorkspaceSeparator separator in _drawPanel)
                separator.Enabled = Enabled;

            // Change in enabled state requires a layout and repaint
            PerformNeedPaint(true);

            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // If layout has not been suspended
            if (_suspendWorkspace == 0)
            {
                // Base class will layout the base panel then after we layout the separators
                base.OnLayout(levent);

                // Perform compacting before laying out the results of the compact
                // (Do not compact at design time as it prevents setting up the hierarchy)
                if (!DesignMode && CompactFlags != CompactFlags.None)
                {
                    Root.Compact(CompactFlags);
                    if ((CompactFlags & CompactFlags.AtLeastOneVisibleCell) == CompactFlags.AtLeastOneVisibleCell)
                    {
                        // Do not enforce the flag at design time
                        if (!DesignMode)
                            CompactAtLeastOneVisibleCell();
                    }
                }

                // Ensure any maximized cell is actually still a valid setting
                if (MaximizedCell != null)
                {
                    if (!MaximizedCell.WorkspaceVisible || !IsCellPresent(MaximizedCell))
                        MaximizedCell = null;
                    else
                        SetActiveCell(MaximizedCell);
                }

                // Lists for the layout processing to populate with instances still needed
                SeparatorList separators = new SeparatorList();
                ControlList controls = new ControlList();

                // Layout child controls according to the need for a maximized cell or not
                using (ViewLayoutContext layoutContext = new ViewLayoutContext(this, Renderer))
                {
                    if (MaximizedCell != null)
                        LayoutSequenceMaximized(Root, ClientRectangle, controls, layoutContext);
                    else
                        LayoutSequenceNonMaximized(Root, ClientRectangle, controls, separators, layoutContext);
                }

                // Remove all view separators no longer needed
                for (int i = _drawPanel.Count - 1; i >= 0; i--)
                {
                    ViewDrawWorkspaceSeparator separator = _drawPanel[i] as ViewDrawWorkspaceSeparator;
                    if (!separators.Contains(separator))
                    {
                        _drawPanel.Remove(separator);
                        _workspaceToSeparator.Remove(separator.WorkspaceItem);
                    }
                }

                // Remove all controls no longer needed
                for (int i = Controls.Count - 1; i >= 0; i--)
                    if (!controls.Contains(Controls[i]))
                    {
                        Control c = Controls[i];
                        ((KryptonReadOnlyControls)Controls).RemoveInternal(c);

                        // If the control has the expected interface
                        IWorkspaceItem item = c as IWorkspaceItem;
                        if (item != null)
                        {
                            // Does the item want to be disposed on removal?
                            if (item.DisposeOnRemove)
                                c.Dispose();
                        }

                        // Generate event so users can reverse actions taken when cell was added
                        if (c is KryptonWorkspaceCell)
                        {
                            KryptonWorkspaceCell cell = c as KryptonWorkspaceCell;
                            OnWorkspaceCellRemoved(new WorkspaceCellEventArgs(cell));
                            cell.DragPageNotify = null;
                        }
                    }

                int cellCount = 0;
                int cellVisibleCount = 0;
                KryptonWorkspaceCell order = FirstCell();
                while (order != null)
                {
                    if (order.LastVisibleSet)
                        cellVisibleCount++;

                    // Tabbing order should match ordering from the workspace hierarchy
                    // (the order within the Controls collection might not match the workspace hierarchy ordering)
                    order.TabIndex = cellCount++;

                    order = NextCell(order);
                }

                // If active page changes are suspended then do no update active cell now
                if (!IsActivePageChangedEventSuspended)
                {
                    // We need to find a new active cell if we do not currently have a setting or if we have a 
                    // setting but that cell is no longer present or the cell is present but not actually visible
                    if ((ActiveCell == null) || !Controls.Contains(ActiveCell) || !ActiveCell.WorkspaceVisible)
                        SetActiveCellRaw(FirstVisibleCell());
                }

                // If we have a maximized cell then ensure it has focus and not some other cell
                if ((MaximizedCell != null) && !MaximizedCell.ContainsFocus && ContainsFocus)
                    MaximizedCell.Select();

                // Fire event if number of cells has changed
                if (cellCount != _cacheCellCount)
                {
                    _cacheCellCount = cellCount;
                    OnCellCountChanged(EventArgs.Empty);
                }

                // Fire event if number of visible cells has changed
                if (cellVisibleCount != _cacheCellVisibleCount)
                {
                    _cacheCellVisibleCount = cellVisibleCount;
                    OnCellVisibleCountChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            // We need to snoop the need to show a context menu
            if (m.Msg == PI.WM_CONTEXTMENU)
            {
                // We never allow our ContextMenuStrip/KryptonContextMenu to show if there are cells 
                // displayed, we only want the context menus showing if there are no cells at all displayed
                if (CellVisibleCount > 0)
                    return;
            }

            if (!IsDisposed)
                base.WndProc(ref m);
        }

        /// <summary>
        /// Work out if this control needs to use Invoke to force a repaint.
        /// </summary>
        protected override bool EvalInvokePaint
        {
            get
            {
                // Always use an invoke to request a repaint and perform layout. Sometimes the entire client area is covered
                // by a child control and so a regular repaint method will not occur because non of the client area is visible.
                return true;
            }
        }
        #endregion

        #region Protected Virtual
        /// <summary>
        /// Raises the CellCountChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCellCountChanged(EventArgs e)
        {
            if (CellCountChanged != null)
                CellCountChanged(this, e);
        }

        /// <summary>
        /// Raises the CellVisibleCountChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnCellVisibleCountChanged(EventArgs e)
        {
            if (CellVisibleCountChanged != null)
                CellVisibleCountChanged(this, e);
        }

        /// <summary>
        /// Raises the WorkspaceCellAdding event.
        /// </summary>
        /// <param name="e">An WorkspaceCellEventArgs containing the event data.</param>
        protected virtual void OnWorkspaceCellAdding(WorkspaceCellEventArgs e)
        {
            NewCellInitialize(e.Cell);
            
            if (WorkspaceCellAdding != null)
                WorkspaceCellAdding(this, e);
        }

        /// <summary>
        /// Raises the WorkspaceCellRemoved event.
        /// </summary>
        /// <param name="e">An WorkspaceCellEventArgs containing the event data.</param>
        protected virtual void OnWorkspaceCellRemoved(WorkspaceCellEventArgs e)
        {
            if (WorkspaceCellRemoved != null)
                WorkspaceCellRemoved(this, e);

            ExistingCellDetach(e.Cell);
        }

        /// <summary>
        /// Raises the ActiveCellChanged event.
        /// </summary>
        /// <param name="e">An ActiveCellChangedEventArgs containing the event data.</param>
        protected virtual void OnActiveCellChanged(ActiveCellChangedEventArgs e)
        {
            if (ActiveCellChanged != null)
                ActiveCellChanged(this, e);
        }

        /// <summary>
        /// Raises the ActivePageChanged event.
        /// </summary>
        /// <param name="e">An ActivePageChangedEventArgs containing the event data.</param>
        protected virtual void OnActivePageChanged(ActivePageChangedEventArgs e)
        {
            // Do not generate event when its firing is suspended
            if (!IsActivePageChangedEventSuspended)
            {
                if (ActivePageChanged != null)
                    ActivePageChanged(this, e);
            }
        }

        /// <summary>
        /// Raises the MaximizedCellChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        protected virtual void OnMaximizedCellChanged(EventArgs e)
        {
            if (MaximizedCellChanged != null)
                MaximizedCellChanged(this, e);
        }

        /// <summary>
        /// Raises the BeforePageDrag event.
        /// </summary>
        /// <param name="de">A PageDragCancelEventArgs containing event details.</param>
        protected virtual void OnBeforePageDrag(PageDragCancelEventArgs de)
        {
            if (BeforePageDrag != null)
                BeforePageDrag(this, de);
        }

        /// <summary>
        /// Raises the AfterPageDrag event.
        /// </summary>
        /// <param name="e">A PageDragEndEventArgs containing event details.</param>
        protected virtual void OnAfterPageDrag(PageDragEndEventArgs e)
        {
            if (AfterPageDrag != null)
                AfterPageDrag(this, e);
        }

        /// <summary>
        /// Raises the PageDrop event.
        /// </summary>
        /// <param name="e">A v containing event details.</param>
        internal protected virtual void OnPageDrop(PageDropEventArgs e)
        {
            if (PageDrop != null)
                PageDrop(this, e);
        }

        /// <summary>
        /// Initialize a new cell.
        /// </summary>
        /// <param name="cell">Cell being added to the control.</param>
        protected virtual void NewCellInitialize(KryptonWorkspaceCell cell)
        {
            cell.Enter += new EventHandler(OnCellEnter);
            cell.SelectedPageChanged += new EventHandler(OnCellSelectedPageChanged);
            cell.ShowContextMenu += new EventHandler<ShowContextMenuArgs>(OnCellShowContextMenu);
            cell.CtrlTabWrap += new EventHandler<CtrlTabCancelEventArgs>(OnCellCtrlTabWrap);
        }

        /// <summary>
        /// Detach an existing cell.
        /// </summary>
        /// <param name="cell">Cell being removed from the control.</param>
        protected virtual void ExistingCellDetach(KryptonWorkspaceCell cell)
        {
            cell.CtrlTabWrap -= new EventHandler<CtrlTabCancelEventArgs>(OnCellCtrlTabWrap);
            cell.ShowContextMenu -= new EventHandler<ShowContextMenuArgs>(OnCellShowContextMenu);
            cell.SelectedPageChanged -= new EventHandler(OnCellSelectedPageChanged);
            cell.Enter -= new EventHandler(OnCellEnter);
        }
        #endregion

        #region Internal
        internal bool SeparatorCanMove(ViewDrawWorkspaceSeparator separator)
        {
            // If layout is suspended then we cannot access the hierarchy of 
            // items so just return true for the duration of suspension
            if (_suspendWorkspace > 0)
                return true;

            // The global setting takes priority
            if (!AllowResizing)
                return false;

            // Find the items before and after the separator
            IWorkspaceItem after;
            IWorkspaceItem before;
            SeparatorToItems(separator, out after, out before);

            // Are both items allowed to be resized by the user?
            // (at design time we can get null references)
            if ((after == null) || (before == null) || 
                !before.WorkspaceAllowResizing || 
                !after.WorkspaceAllowResizing)
                return false;

            // How far can the separator move before and after the current position
            int moveBefore;
            int moveAfter;
            SeparatorToMovement(separator, after, before, out moveBefore, out moveAfter);

            // If there is no movement allowed before or after then cannot move separator
            if ((moveBefore == 0) && (moveAfter == 0))
                return false;

            // Not excluded so allow it to occur
            return true;
        }

        internal Rectangle SeparatorMoveBox(ViewDrawWorkspaceSeparator separator)
        {
            // Find the items before and after the separator
            IWorkspaceItem after;
            IWorkspaceItem before;
            SeparatorToItems(separator, out after, out before);

            // At design time we can get null references
            if ((after == null) || (before == null))
                return Rectangle.Empty;

            // How far can the separator move before and after the current position
            int moveBefore;
            int moveAfter;
            SeparatorToMovement(separator, after, before, out moveBefore, out moveAfter);

            if (separator.Orientation == Orientation.Horizontal)
                return new Rectangle(separator.ClientLocation.X, separator.ClientLocation.Y - moveBefore, 
                                     separator.ClientWidth, moveBefore + moveAfter);
            else
                return new Rectangle(separator.ClientLocation.X - moveBefore, separator.ClientLocation.Y, 
                                     moveBefore + moveAfter, separator.ClientHeight);
        }

        internal void SeparatorMoved(ViewDrawWorkspaceSeparator separator,
                                     Point mouse, 
                                     Point splitter)
        {
            // Get the sequence that contains the items moved
            KryptonWorkspaceSequence parentSequence = (KryptonWorkspaceSequence)separator.WorkspaceItem.WorkspaceParent;

            // Find the items before and after the separator
            IWorkspaceItem after;
            IWorkspaceItem before;
            SeparatorToItems(separator, out after, out before);

            // At design time we can get null references
            if ((after != null) && (before != null))
            {
                // Find the direction dependant change
                int offset;
                if (separator.Orientation == Orientation.Vertical)
                    offset = splitter.X - separator.ClientLocation.X;
                else
                    offset = splitter.Y - separator.ClientLocation.Y;

                // Only need to process if a change has occured
                if (offset != 0)
                {
                    // Update the sizing value for each item in the sequence
                    for (int i = 0; i < parentSequence.Children.Count; i++)
                    {
                        // Can only process IWorkspaceItem items
                        IWorkspaceItem item = parentSequence.Children[i] as IWorkspaceItem;
                        if (item != null)
                        {
                            // Get direction specific numbers
                            int directionActualSize;
                            StarNumber directionStarSize;
                            if (separator.Orientation == Orientation.Vertical)
                            {
                                directionActualSize = item.WorkspaceActualSize.Width;
                                directionStarSize = item.WorkspaceStarSize.StarWidth;
                            }
                            else
                            {
                                directionActualSize = item.WorkspaceActualSize.Height;
                                directionStarSize = item.WorkspaceStarSize.StarHeight;
                            }

                            // The default new star value is the pixel size
                            int newStar = directionActualSize;

                            // The before and after items are adjusted by the splitter offset
                            if (item == before)
                                newStar += offset;
                            if (item == after)
                                newStar -= offset;

                            // Is this a fixed size item?
                            if (!directionStarSize.UsingStar)
                                directionStarSize.Value = newStar.ToString();
                            else
                                directionStarSize.Value = newStar.ToString() + "*";
                        }
                    }
                }

                PerformNeedPaint(true);
            }
        }

        internal void InternalPageDragStart(object sender, KryptonNavigator navigator, PageDragCancelEventArgs e)
        {
            // Generate event allowing the DragPageNotify setting to be updated before the
            // actual drag processing occurs. You can even cancel the drag entirely.
            e.Cancel = !AllowPageDrag;
            OnBeforePageDrag(e);
            if (!e.Cancel)
            {
                // Update with any changes made by the event
                List<KryptonPage> list = new List<KryptonPage>();
                foreach (KryptonPage p in e.Pages)
                    list.Add(p);
                _dragPages = list.ToArray();

                if (DragPageNotify != null)
                    DragPageNotify.PageDragStart(sender, navigator, e);
                else
                {
                    // Create drag mananager the first time it is needed
                    if (_dragManager == null)
                    {
                        _dragManager = new DragManager();
                        _dragManager.DragTargetProviders.Add(this);
                    }

                    // Calling DragStart will cause the drag targets to be created from the target providers
                    e.Cancel = !_dragManager.DragStart(e.ScreenPoint, new PageDragEndData(sender as KryptonNavigator, e.Pages));
                }
            }
        }

        internal void InternalPageDragMove(KryptonNavigator sender, PointEventArgs e)
        {
            if (DragPageNotify != null)
                DragPageNotify.PageDragMove(sender, e);
            else if (_dragManager != null)
                _dragManager.DragMove(e.Point);
        }

        internal bool InternalPageDragEnd(KryptonNavigator sender, PointEventArgs e)
        {
            // Do not generate active page changes during drop processing
            SuspendActivePageChangedEvent();

            bool ret = false;
            if (DragPageNotify != null)
                ret = DragPageNotify.PageDragEnd(sender, e);
            else if (_dragManager != null)
                ret = _dragManager.DragEnd(e.Point);

            // Now all changes are made we allow the changes to occur
            ResumeActivePageChangedEvent();
            OnAfterPageDrag(new PageDragEndEventArgs(true, _dragPages));
            _dragPages = null;
            return ret;
        }

        internal void InternalPageDragQuit(KryptonNavigator sender)
        {
            if (DragPageNotify != null)
                DragPageNotify.PageDragQuit(sender);
            else if (_dragManager != null)
                _dragManager.DragQuit();

            OnAfterPageDrag(new PageDragEndEventArgs(false, _dragPages));
            _dragPages = null;
        }
        #endregion

        #region Implementation
        private void LayoutSequenceMaximized(KryptonWorkspaceSequence seq,
                                             Rectangle client,
                                             ControlList controls,
                                             ViewLayoutContext layoutContext)
        {
            // Inform the sequence of the client rectangle it occupies
            seq.WorkspaceActualSize = client.Size;

            // Position each cell in turn, starting with first cell in workspace hierarchy
            KryptonWorkspaceCell cell = FirstCell();
            while (cell != null)
            {
                // Update the maximize/restore button spec per-cell
                cell.MaximizeRestoreButton.Visible = ShowMaximizeButton;
                cell.MaximizeRestoreButton.Type = PaletteButtonSpecStyle.WorkspaceRestore;

                // The target cell takes up all the space and all others are make zero sized so they are hidden
                if (cell == MaximizedCell)
                    cell.SetBounds(0, 0, client.Size.Width, client.Size.Height);                    
                else
                    cell.SetBounds(0, 0, 0, 0);

                // If not already a child control then it should be!
                if (!Controls.Contains(cell))
                {
                    cell.DragPageNotify = _cellPageNotify;
                    OnWorkspaceCellAdding(new WorkspaceCellEventArgs(cell));
                    ((KryptonReadOnlyControls)Controls).AddInternal(cell);
                }

                // Note that this control is still needed
                controls.Add(cell);

                // Move to next cell in workspace hierarchy
                cell = NextCell(cell);
            }
        }

        private void LayoutSequenceNonMaximized(KryptonWorkspaceSequence seq, 
                                                Rectangle client,
                                                ControlList controls,
                                                SeparatorList separators,
                                                ViewLayoutContext layoutContext)
        {
            // Inform the sequence of the client rectangle it occupies
            seq.WorkspaceActualSize = client.Size;

            // Find orientation specific length that needs allocating
            int availableSpace = (seq.Orientation == Orientation.Vertical) ? client.Height : client.Width;

            // Temporary structures used to cache info during calculations
            LayoutInfo[] info = new LayoutInfo[seq.Children.Count];

            int visibleChildren = 0;
            int visibleStarChildren = 0;
            double starTotal = 0.0f;
            
            // Pass #1, Determine which children are visible and cache sizing information
            for (int i = 0; i < seq.Children.Count; i++)
            {
                info[i].WorkspaceItem = seq.Children[i] as IWorkspaceItem;

                // Can only work with items that have an IWorkspaceItem interface
                if (info[i].WorkspaceItem != null)
                {
                    // Should the workspace item be visible?
                    info[i].WorkspaceVisible = info[i].WorkspaceItem.WorkspaceVisible;

                    // Only need to cache information for visible items
                    if (info[i].WorkspaceVisible)
                    {
                        // Count number of visible children
                        visibleChildren++;

                        // Cache the sizing information for our specific layout direction
                        if (seq.Orientation == Orientation.Vertical)
                        {
                            info[i].CacheStarSize = info[i].WorkspaceItem.WorkspaceStarSize.StarHeight;
                            info[i].CacheMinSize = info[i].WorkspaceItem.WorkspaceMinSize.Height;
                            info[i].CacheMaxSize = info[i].WorkspaceItem.WorkspaceMaxSize.Height;
                        }
                        else
                        {
                            info[i].CacheStarSize = info[i].WorkspaceItem.WorkspaceStarSize.StarWidth;
                            info[i].CacheMinSize = info[i].WorkspaceItem.WorkspaceMinSize.Width;
                            info[i].CacheMaxSize = info[i].WorkspaceItem.WorkspaceMaxSize.Width;
                        }

                        // Ensure the maximum is never less than the minimum
                        if ((info[i].CacheMaxSize > 0) && (info[i].CacheMinSize > 0))
                            info[i].CacheMaxSize = Math.Max(info[i].CacheMaxSize, info[i].CacheMinSize);

                        // Need to total up the star values
                        if (info[i].CacheStarSize.UsingStar)
                        {
                            starTotal += info[i].CacheStarSize.StarSize;
                            visibleStarChildren++;
                        }
                    }
                }
            }

            // Reduce available space by that needed for a splitter between each visible item
            availableSpace = Math.Max(0, availableSpace - Math.Max(0, visibleChildren - 1) * SplitterWidth);

            // Pass #2, Allocate space to fixed size items
            bool displayMaximizeButton = (visibleChildren > 1) && ShowMaximizeButton;
            for (int i = 0; i < seq.Children.Count; i++)
            {
                // Can only work with items that have an IWorkspaceItem interface
                if (info[i].WorkspaceItem != null)
                {
                    if (info[i].WorkspaceVisible && !info[i].CacheStarSize.UsingStar)
                    {
                        // Allocate all requested size to the item
                        info[i].AllocatedSpace = true;

                        // If the fixed size is positive then use the value itself
                        int displaySpace;
                        if (info[i].CacheStarSize.FixedSize >= 0)
                            displaySpace = info[i].CacheStarSize.FixedSize;
                        else
                        {
                            // Otherwise we get the preferred size of the item in sequence orientation
                            if (seq.Orientation == Orientation.Vertical)
                                displaySpace = info[i].WorkspaceItem.WorkspacePreferredSize.Height;
                            else
                                displaySpace = info[i].WorkspaceItem.WorkspacePreferredSize.Width;
                        }

                        info[i].DisplaySpace = displaySpace;
                        availableSpace -= displaySpace;

                        // visibleChildren is used to count items left to process
                        visibleChildren--;
                    }
                }

                // Update the maximize/restore button spec per-cell
                KryptonWorkspaceCell cell = seq.Children[i] as KryptonWorkspaceCell;
                if (cell != null)
                {
                    cell.MaximizeRestoreButton.Visible = displayMaximizeButton;
                    cell.MaximizeRestoreButton.Type = PaletteButtonSpecStyle.WorkspaceMaximize;
                }
            }

            // Any more items left to allocate space for?
            if (visibleChildren > 0)
            {
                // Pass #3, Check if items with a min/max need to have those min/max values enforced
                for (int i = 0; i < seq.Children.Count; i++)
                {
                    // Can only work with items that have an IWorkspaceItem interface
                    if (info[i].WorkspaceItem != null)
                    {
                        if (info[i].WorkspaceVisible && !info[i].AllocatedSpace)
                        {
                            // Only interested in items that have defined minimum or maximum
                            if ((info[i].CacheMinSize > 0) || (info[i].CacheMaxSize > 0))
                            {
                                // Calculate space based on the items star setting
                                int itemSpace = availableSpace;
                                if (visibleStarChildren > 1)
                                    itemSpace = (int)(availableSpace / starTotal * info[i].CacheStarSize.StarSize);

                                // If the calculation is less than the minimum
                                if ((info[i].CacheMinSize > 0) && (itemSpace < info[i].CacheMinSize))
                                {
                                    // Enfore minimum setting
                                    info[i].AllocatedSpace = true;
                                    info[i].DisplaySpace = info[i].CacheMinSize;

                                    // Reduce by the amount we have just allocated
                                    starTotal -= info[i].CacheStarSize.StarSize;
                                    availableSpace -= info[i].DisplaySpace;

                                    visibleStarChildren--;
                                    visibleChildren--;
                                }
                                else if ((info[i].CacheMaxSize > 0) && (itemSpace > info[i].CacheMaxSize))
                                {
                                    // Enfore maximum setting
                                    info[i].AllocatedSpace = true;
                                    info[i].DisplaySpace = info[i].CacheMaxSize;

                                    // Reduce by the amount we have just allocated
                                    starTotal -= info[i].CacheStarSize.StarSize;
                                    availableSpace -= info[i].DisplaySpace;

                                    visibleStarChildren--;
                                    visibleChildren--;
                                }
                            }
                        }
                    }
                }

                // Any more items left to allocate space for?
                if (visibleChildren > 0)
                {
                    // Pass #4, Allocate space to remaining entries based on their star values
                    for (int i = 0; i < seq.Children.Count; i++)
                    {
                        // Can only work with items that have an IWorkspaceItem interface
                        if (info[i].WorkspaceItem != null)
                        {
                            if (info[i].WorkspaceVisible && !info[i].AllocatedSpace)
                            {
                                int itemSpace = availableSpace;

                                // If not the last item then find correct proportional size
                                if (visibleStarChildren > 1)
                                {
                                    itemSpace = (int)(availableSpace / starTotal * info[i].CacheStarSize.StarSize);
                                    visibleStarChildren--;
                                }

                                // Assign calculated space
                                info[i].DisplaySpace = itemSpace;

                                // Reduce by the amount we have just allocated
                                starTotal -= info[i].CacheStarSize.StarSize;
                                availableSpace -= itemSpace;
                            }
                        }
                    }
                }
            }

            // Pass #5, Create display rectangles based on space allocated to each item
            int offset = 0;
            bool first = true;
            for (int i = 0; i < seq.Children.Count; i++)
            {
                // Can only work with items that have an IWorkspaceItem interface
                if (info[i].WorkspaceItem != null)
                {
                    // If no separator is associated with workspace, then create one now
                    ViewDrawWorkspaceSeparator viewSeparator;
                    if (!_workspaceToSeparator.TryGetValue(info[i].WorkspaceItem, out viewSeparator))
                    {
                        // Create a view for the separator area
                        viewSeparator = new ViewDrawWorkspaceSeparator(this, info[i].WorkspaceItem, seq.Orientation);
                        viewSeparator.Enabled = Enabled;

                        // Need a controller that operates the movement
                        SeparatorController separatorController = new SeparatorController(viewSeparator, viewSeparator, true, true, _separatorNeedPaint);
                        viewSeparator.Source = viewSeparator;
                        viewSeparator.MouseController = separatorController;
                        viewSeparator.KeyController = separatorController;
                        viewSeparator.SourceController = separatorController;

                        // Add separator to view so it gets drawn and mouse events
                        _drawPanel.Add(viewSeparator);

                        // Add separator to the lookup so we correctly remove it when no longer needed
                        _workspaceToSeparator.Add(info[i].WorkspaceItem, viewSeparator);
                    }

                    // Add to list of separators still needed
                    separators.Add(viewSeparator);

                    if (info[i].WorkspaceVisible)
                    {
                        // If this is not the first item
                        if (!first)
                        {
                            // Calculate the display rect for the separator
                            if (seq.Orientation == Orientation.Vertical)
                            {
                                viewSeparator.Orientation = Orientation.Horizontal;
                                layoutContext.DisplayRectangle = new Rectangle(client.X, client.Y + offset, client.Width, SplitterWidth);
                            }
                            else
                            {
                                viewSeparator.Orientation = Orientation.Vertical;
                                layoutContext.DisplayRectangle = new Rectangle(client.X + offset, client.Y, SplitterWidth, client.Height);
                            }

                            // Ask the separator to position itself
                            viewSeparator.Layout(layoutContext);
                            viewSeparator.Visible = true;

                            // Move over the splitter
                            offset += SplitterWidth;
                        }
                        else
                        {
                            // Do not show separator before first item
                            viewSeparator.Visible = false;
                        }

                        // Calculate the display rect for the item
                        if (seq.Orientation == Orientation.Vertical)
                            info[i].DisplayRect = new Rectangle(client.X, client.Y + offset, client.Width, info[i].DisplaySpace);
                        else
                            info[i].DisplayRect = new Rectangle(client.X + offset, client.Y, info[i].DisplaySpace, client.Height);

                        // Move over the cell
                        offset += info[i].DisplaySpace;
                        first = false;
                    }
                    else
                        viewSeparator.Visible = false;
                }
            }

            // Pass #6, Position children and recurse into child sequences
            for (int i = 0; i < seq.Children.Count; i++)
            {
                // Can only work with items that have an IWorkspaceItem interface
                if (info[i].WorkspaceItem != null)
                {
                    // Is the child an actual control
                    Control control = seq.Children[i] as Control;
                    if (control != null)
                    {
                        // If not already a child control then it should be!
                        if (!Controls.Contains(control))
                        {
                            // Generate event so users can update the cell before it is displayed
                            if (control is KryptonWorkspaceCell)
                            {
                                KryptonWorkspaceCell cell = control as KryptonWorkspaceCell;
                                cell.DragPageNotify = _cellPageNotify;
                                OnWorkspaceCellAdding(new WorkspaceCellEventArgs(cell));
                            }

                            ((KryptonReadOnlyControls)Controls).AddInternal(control);
                        }

                        // Note that this control is still needed
                        controls.Add(control);

                        // Only position visible children
                        if (info[i].WorkspaceVisible)
                        {
                            // Position the child control
                            control.SetBounds(info[i].DisplayRect.X,
                                              info[i].DisplayRect.Y,
                                              info[i].DisplayRect.Width,
                                              info[i].DisplayRect.Height);
                        }
                    }
                    else
                    {
                        // Only position visible children
                        if (info[i].WorkspaceVisible)
                        {
                            // If the item is a sequence, then position its contents inside the allocated area
                            if (seq.Children[i] is KryptonWorkspaceSequence)
                                LayoutSequenceNonMaximized(seq.Children[i] as KryptonWorkspaceSequence,
                                                           info[i].DisplayRect,
                                                           controls,
                                                           separators,
                                                           layoutContext);
                        }
                        else
                        {
                            // Ensure we mark all the controls contained in the sequence as still needed
                            LayoutSequenceIsHidden(seq.Children[i] as KryptonWorkspaceSequence, controls);
                        }
                    }
                }
            }
        }


        private void LayoutSequenceIsHidden(KryptonWorkspaceSequence sequence, ControlList controls)
        {
            // Process all children of the sequence
            for (int i = 0; i < sequence.Children.Count; i++)
            {
                // Is the child an actual control
                Control control = sequence.Children[i] as Control;
                if (control != null)
                {
                    // Note that this control is still needed
                    controls.Add(control);
                }
                else
                {
                    // If not a control then it must be a sequence
                    if (sequence.Children[i] is KryptonWorkspaceSequence)
                    {
                        // Ensure we mark all the controls contained in the sequence as still needed
                        LayoutSequenceIsHidden(sequence.Children[i] as KryptonWorkspaceSequence, controls);
                    }
                }
            }
        }
        
        private bool RecursiveSearchCellInSequence(KryptonWorkspaceSequence sequence,
                                                   KryptonWorkspaceCell target)
        {
            // Scan all sequence children looking for a matching cell
            for (int i = 0; i < sequence.Children.Count; i++)
            {
                // If we find the target, then done
                if (sequence.Children[i] == target)
                    return true;

                // If child is a sequence then recurse into it
                KryptonWorkspaceSequence child = sequence.Children[i] as KryptonWorkspaceSequence;
                if ((child != null) && RecursiveSearchCellInSequence(child, target))
                    return true;
            }

            return false;
        }

        private KryptonWorkspaceCell RecursiveFindCellInSequence(KryptonWorkspaceSequence sequence,
                                                                 Component target, 
                                                                 bool forwards,
                                                                 bool onlyVisible)
        {
            int count = sequence.Children.Count;
            int index = sequence.Children.IndexOf(target);

            // Are we look for entries after the provided one?
            if (forwards)
            {
                for (int i = index + 1; i < count; i++)
                {
                    if (sequence.Children[i] is KryptonWorkspaceCell)
                    {
                        // Check that the visible state of the cell matches the find requirements
                        KryptonWorkspaceCell cell = sequence.Children[i] as KryptonWorkspaceCell;
                        if (!onlyVisible || cell.WorkspaceVisible)
                            return cell;
                    }
                    else if (sequence.Children[i] is KryptonWorkspaceSequence)
                    {
                        // Search inside the sequence for the first leaf in the specified direction
                        KryptonWorkspaceCell ret = RecursiveFindCellInSequence(sequence.Children[i] as KryptonWorkspaceSequence, forwards, onlyVisible);
                        if (ret != null)
                            return ret;
                    }
                }
            }
            else
            {
                // Now try each entry before that given
                for (int i = index - 1; i >= 0; i--)
                {
                    if (sequence.Children[i] is KryptonWorkspaceCell)
                    {
                        // Check that the visible state of the cell matches the find requirements
                        KryptonWorkspaceCell cell = sequence.Children[i] as KryptonWorkspaceCell;
                        if (!onlyVisible || cell.WorkspaceVisible)
                            return cell;
                    }
                    else if (sequence.Children[i] is KryptonWorkspaceSequence)
                    {
                        // Search inside the sequence for the first leaf in the specified direction
                        KryptonWorkspaceCell ret = RecursiveFindCellInSequence(sequence.Children[i] as KryptonWorkspaceSequence, forwards, onlyVisible);
                        if (ret != null)
                            return ret;
                    }
                }
            }

            // Still no luck, try our own parent
            if (sequence.WorkspaceParent != null)
                return RecursiveFindCellInSequence(sequence.WorkspaceParent as KryptonWorkspaceSequence, sequence, forwards, onlyVisible);
            else
                return null;
        }

        private KryptonWorkspaceCell RecursiveFindCellInSequence(KryptonWorkspaceSequence sequence, 
                                                                 bool forwards,
                                                                 bool onlyVisible)
        {
            int count = sequence.Children.Count;

            for (int i = 0; i < count; i++)
            {
                // Index depends on which direction we are processing
                int index = (forwards == true) ? i : (sequence.Children.Count - i - 1);

                if (sequence.Children[index] is KryptonWorkspaceCell)
                {
                    // Check that the visible state of the cell matches the find requirements
                    KryptonWorkspaceCell cell = sequence.Children[index] as KryptonWorkspaceCell;
                    if (!onlyVisible || cell.WorkspaceVisible)
                        return cell;
                }
                else if (sequence.Children[index] is KryptonWorkspaceSequence)
                {
                    // Search inside the sequence 
                    KryptonWorkspaceCell ret = RecursiveFindCellInSequence(sequence.Children[index] as KryptonWorkspaceSequence, forwards, onlyVisible);
                    if (ret != null)
                        return ret;
                }
            }

            // Still no luck
            return null;
        }

        private void SeparatorToItems(ViewDrawWorkspaceSeparator separator,
                                      out IWorkspaceItem after,
                                      out IWorkspaceItem before)
        {
            // Workspace item after the separator (to the right or below)
            after = separator.WorkspaceItem;

            // Workspace item before the separator (to the left or above)
            KryptonWorkspaceSequence beforeSequence = (KryptonWorkspaceSequence)after.WorkspaceParent;

            // Previous items might be invisible and so search till we find the visible one we expect
            before = null;
            for (int i = beforeSequence.Children.IndexOf(after) - 1; i >= 0; i--)
            {
                IWorkspaceItem item = beforeSequence.Children[i] as IWorkspaceItem;
                if ((item != null) && item.WorkspaceVisible)
                {
                    before = item;
                    break;
                }
            }
        }

        private void SeparatorToMovement(ViewDrawWorkspaceSeparator separator,
                                         IWorkspaceItem after,
                                         IWorkspaceItem before,
                                         out int moveBefore,
                                         out int moveAfter)
        {
            // Get the actual pixel size of the two items
            int afterSize = (separator.Orientation == Orientation.Vertical) ? after.WorkspaceActualSize.Width : after.WorkspaceActualSize.Height;
            int beforeSize = (separator.Orientation == Orientation.Vertical) ? before.WorkspaceActualSize.Width : before.WorkspaceActualSize.Height;

            // Get the min/max values for the two items
            int afterMin = (separator.Orientation == Orientation.Vertical) ? after.WorkspaceMinSize.Width : after.WorkspaceMinSize.Height;
            int beforeMin = (separator.Orientation == Orientation.Vertical) ? before.WorkspaceMinSize.Width : before.WorkspaceMinSize.Height;
            int afterMax = (separator.Orientation == Orientation.Vertical) ? after.WorkspaceMaxSize.Width : after.WorkspaceMaxSize.Height;
            int beforeMax = (separator.Orientation == Orientation.Vertical) ? before.WorkspaceMaxSize.Width : before.WorkspaceMaxSize.Height;

            // How far can the separator move to shrink the before item
            moveBefore = beforeSize - beforeMin;
            if (afterMax > 0)
                moveBefore = Math.Min(moveBefore, afterMax - afterSize);

            // How far can the separator move to shrink the after item
            moveAfter = afterSize - afterMin;
            if (beforeMax > 0)
                moveAfter = Math.Min(moveAfter, beforeMax - beforeSize);
        }

        private CellList CopyToCellList()
        {
            // Make a list of all the pages in workspace without removing any
            CellList cells = new CellList();
            KryptonWorkspaceCell cell = FirstCell();
            while (cell != null)
            {
                cells.Add(cell);
                cell = NextCell(cell);
            }

            return cells;
        }

        private CellList ClearToCellList()
        {
            // Make a list of all the pages in workspace without removing any
            CellList cells = CopyToCellList();

            // Remove existing workspace hierarchy
            Root.Children.Clear();

            return cells;
        }

        private PageList CopyToPageList()
        {
            // Make list of all pages inside all cells
            PageList pages = new PageList();
            KryptonWorkspaceCell cell = FirstCell();
            while (cell != null)
            {
                pages.AddRange(cell.Pages);
                cell = NextCell(cell);
            }

            return pages;
        }

        private KryptonPageCollection CopyToPageCollection()
        {
            // Make list of all pages inside all cells
            KryptonPageCollection pages = new KryptonPageCollection();
            KryptonWorkspaceCell cell = FirstCell();
            while (cell != null)
            {
                foreach(KryptonPage page in cell.Pages)
                    pages.Add(page);

                cell = NextCell(cell);
            }

            return pages;
        }

        private PageList ClearToPageList()
        {
            // Remove all pages from all cells add then to a list
            PageList pages = new PageList();
            KryptonWorkspaceCell cell = FirstCell();
            while (cell != null)
            {
                pages.AddRange(cell.Pages);
                cell.Pages.Clear();
                cell = NextCell(cell);
            }

            // Remove existing workspace hierarchy
            Root.Children.Clear();

            return pages;
        }

        private void UpdateAllPagesVisible(bool visible, Type excludeType)
        {
            // Iterate over the workspace hierarchy looking for cells
            KryptonWorkspaceCell cell = FirstCell();
            while (cell != null)
            {
                if (visible)
                    cell.ShowAllPages(excludeType);
                else
                    cell.HideAllPages(excludeType);

                cell = NextCell(cell);
            }
        }

        private void GenerateCellDragTargets(KryptonWorkspaceCell cell,
                                             DragTargetList targets,
                                             KryptonPageFlags allowFlags)
        {
            Rectangle screenRect = cell.RectangleToScreen(cell.ClientRectangle);
            Rectangle[] rectsHot = SubdivideRectangle(screenRect, 3, int.MaxValue);

            // If the cell has no pages and we compact away empty cells then do not allow the user to drop at edges
            // of the cell as this will only cause the exsiting cell to be compacted away and leave the new drop in
            // exactly the place as the original cell. The same effect is to just transfer the page into the cell
            if ((cell.Pages.VisibleCount > 0) || (!DesignMode && (CompactFlags & CompactFlags.RemoveEmptyCells) == 0))
            {
                // Generate targets for the four control edges
                targets.Add(new DragTargetWorkspaceCellEdge(screenRect, rectsHot[0], rectsHot[0], DragTargetHint.EdgeLeft, this, cell, allowFlags));
                targets.Add(new DragTargetWorkspaceCellEdge(screenRect, rectsHot[1], rectsHot[1], DragTargetHint.EdgeRight, this, cell, allowFlags));
                targets.Add(new DragTargetWorkspaceCellEdge(screenRect, rectsHot[2], rectsHot[2], DragTargetHint.EdgeTop, this, cell, allowFlags));
                targets.Add(new DragTargetWorkspaceCellEdge(screenRect, rectsHot[3], rectsHot[3], DragTargetHint.EdgeBottom, this, cell, allowFlags));
            }

            // Generate target for transferring page into the cell
            targets.Add(new DragTargetWorkspaceCellTransfer(screenRect, rectsHot[4], screenRect, this, cell, allowFlags));
        }

        private Rectangle[] SubdivideRectangle(Rectangle area, 
                                               int divisor,
                                               int maxLength)
        {
            int length = Math.Min(area.Width / divisor, Math.Min(area.Height / divisor, maxLength));

            // Find the left, right, top, bottom, center rectangles
            return new Rectangle[]{ new Rectangle(area.X, area.Y, length, area.Height),
                                    new Rectangle(area.Right - length, area.Y, length, area.Height),
                                    new Rectangle(area.X, area.Y, area.Width, length),
                                    new Rectangle(area.X, area.Bottom - length, area.Width, length),
                                    new Rectangle(area.X + length, area.Y + length, 
                                                  area.Width - length * 2, area.Height - length * 2)};
        }

        private void CompactAtLeastOneVisibleCell()
        {
            // If there are no visible cells found in entire hierarchy
            if (!Root.Children.ContainsVisibleCell)
                Root.Children.Add(new KryptonWorkspaceCell());
        }

        private void SetActiveCell(KryptonWorkspaceCell newCell)
        {
            // Check that we actually contain this cell
            if ((_activeCell != newCell) && Controls.Contains(newCell))
            {
                SetActiveCellRaw(newCell);
            }
        }

        private void SetActiveCellRaw(KryptonWorkspaceCell newCell)
        {
            if (_activeCell != newCell)
            {
                // If there is a maximized cell and it is no longer the active cell then we need 
                // to remove the maximized cell setting to the newly active cell can be seen
                if ((MaximizedCell != null) && (MaximizedCell != newCell))
                    MaximizedCell = null;

                KryptonWorkspaceCell oldCell = _activeCell;
                _activeCell = newCell;
                OnActiveCellChanged(new ActiveCellChangedEventArgs(oldCell, _activeCell));

                // Find the new active page
                KryptonPage page = ActivePage;
                if (_activeCell != null)
                    page = _activeCell.SelectedPage;

                if (_activePage != page)
                {
                    KryptonPage oldPage = _activePage;
                    _activePage = page;
                    OnActivePageChanged(new ActivePageChangedEventArgs(oldPage, _activePage));
                }
            }
        }

        private void OnCellEnter(object sender, EventArgs e)
        {
            KryptonWorkspaceCell cell = (KryptonWorkspaceCell)sender;
            ActiveCell = cell;
        }

        private void OnCellSelectedPageChanged(object sender, EventArgs e)
        {
            if (!IsActivePageChangedEventSuspended)
            {
                // If change occured on the active cell
                KryptonWorkspaceCell cell = (KryptonWorkspaceCell)sender;
                if (cell == ActiveCell)
                {
                    if (cell.SelectedPage != _activePage)
                    {
                        KryptonPage oldPage = _activePage;
                        _activePage = cell.SelectedPage;
                        OnActivePageChanged(new ActivePageChangedEventArgs(oldPage, _activePage));
                    }
                }
            }
        }

        private void OnCellShowContextMenu(object sender, ShowContextMenuArgs e)
        {
            // Do we customize the context menu of the page header?
            if (ContextMenus.ShowContextMenu && !e.Cancel)
            {
                // Cache page and cell the menu is for
                _menuPage = e.Item;
                _menuCell = CellForPage(_menuPage);

                // Create menu items the first time they are needed
                if (_menuItems == null)
                {
                    // Create individual items
                    _menuSeparator1 = new KryptonContextMenuSeparator();
                    _menuClose = new KryptonContextMenuItem(ContextMenus.TextClose, new EventHandler(OnPageClose), ContextMenus.ShortcutClose);
                    _menuCloseAllButThis = new KryptonContextMenuItem(ContextMenus.TextCloseAllButThis, new EventHandler(OnPageCloseAllButThis), ContextMenus.ShortcutCloseAllButThis);
                    _menuSeparator2 = new KryptonContextMenuSeparator();
                    _menuMoveNext = new KryptonContextMenuItem(ContextMenus.TextMoveNext, new EventHandler(OnPageMoveNext), ContextMenus.ShortcutMoveNext);
                    _menuMovePrevious = new KryptonContextMenuItem(ContextMenus.TextMovePrevious, new EventHandler(OnPageMovePrevious), ContextMenus.ShortcutMovePrevious);
                    _menuSeparator3 = new KryptonContextMenuSeparator();
                    _menuSplitVert = new KryptonContextMenuItem(ContextMenus.TextSplitVertical, new EventHandler(OnPageSplitVert), ContextMenus.ShortcutSplitVertical);
                    _menuSplitHorz = new KryptonContextMenuItem(ContextMenus.TextSplitHorizontal, new EventHandler(OnPageSplitHorz), ContextMenus.ShortcutSplitHorizontal);
                    _menuSeparator4 = new KryptonContextMenuSeparator();
                    _menuMaximizeRestore = new KryptonContextMenuItem(ContextMenus.TextMaximize, new EventHandler(OnPageMaximizeRestore), ContextMenus.ShortcutMaximizeRestore);
                    _menuSeparator5 = new KryptonContextMenuSeparator();
                    _menuRebalance = new KryptonContextMenuItem(ContextMenus.TextRebalance, new EventHandler(OnPageRebalance), ContextMenus.ShortcutRebalance);

                    // Add items inside an items collection (apart from separator1 which is only added if required)
                    _menuItems = new KryptonContextMenuItems(new KryptonContextMenuItemBase[] { _menuClose,
                                                                                                _menuCloseAllButThis,
                                                                                                _menuSeparator2,
                                                                                                _menuMoveNext, _menuMovePrevious, 
                                                                                                _menuSeparator3,
                                                                                                _menuSplitVert, _menuSplitHorz,
                                                                                                _menuSeparator4,
                                                                                                _menuMaximizeRestore,
                                                                                                _menuSeparator5,
                                                                                                _menuRebalance});
                }

                // Ensure we have a krypton context menu if not already present
                if (e.KryptonContextMenu == null)
                    e.KryptonContextMenu = new KryptonContextMenu();

                // Update the individual menu options
                _menuClose.Visible = CanClosePage(_menuPage);
                _menuCloseAllButThis.Visible = (PageVisibleCount > 1);
                _menuMoveNext.Visible = CanMovePageNext(_menuPage);
                _menuMovePrevious.Visible = CanMovePagePrevious(_menuPage);
                _menuSplitVert.Visible = (_menuCell.Pages.VisibleCount > 1);
                _menuSplitHorz.Visible = _menuSplitVert.Visible;
                _menuMaximizeRestore.Text = (MaximizedCell == null ? ContextMenus.TextMaximize : ContextMenus.TextRestore);
                _menuRebalance.Visible = (CellVisibleCount > 1);
                
                // Only need the top separator if there is already something in the menu and we need to add at least one entry
                _menuSeparator1.Visible = (e.KryptonContextMenu.Items.Count > 0) && 
                                          (_menuClose.Visible || _menuCloseAllButThis.Visible || _menuMoveNext.Visible || 
                                           _menuMovePrevious.Visible || _menuSplitVert.Visible || _menuSplitHorz.Visible || 
                                           _menuRebalance.Visible);

                // Decide if we need the rest of the separators
                _menuSeparator2.Visible = _menuClose.Visible && (_menuMoveNext.Visible || _menuMovePrevious.Visible);
                _menuSeparator3.Visible = (_menuClose.Visible || _menuSeparator2.Visible) && (_menuSplitVert.Visible || _menuSplitHorz.Visible);
                _menuSeparator5.Visible = (_menuClose.Visible || _menuSeparator2.Visible || _menuSeparator3.Visible) && _menuRebalance.Visible;

                if (!e.KryptonContextMenu.Items.Contains(_menuItems))
                    e.KryptonContextMenu.Items.AddRange(new KryptonContextMenuItemBase[] { _menuSeparator1, _menuItems });

                // Need to know when menu is removed so we can undo our actions
                e.KryptonContextMenu.Closed += new ToolStripDropDownClosedEventHandler(OnCellClosedContextMenu);

                // Show the menu!
                e.Cancel = false;
            }
        }

        private void OnCellClosedContextMenu(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // Unhook from context menu
            KryptonContextMenu contextMenu = (KryptonContextMenu)sender;
            contextMenu.Closed -= new ToolStripDropDownClosedEventHandler(OnCellClosedContextMenu);

            // Remove our menu items as we only want them to be inside the currently showing context menu
            contextMenu.Items.Remove(_menuSeparator1);
            contextMenu.Items.Remove(_menuItems);

            // Must unreference the page/cell so they can be gargabe collected
            if (e.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
                ClearContextMenuCache();
        }

        private void OnCellCtrlTabWrap(object sender, CtrlTabCancelEventArgs e)
        {
            // Prevent the cell from wrapping around when ctrl+tabbing
            e.Cancel = true;

            // Remember the starting cell for the search
            KryptonWorkspaceCell cell = (KryptonWorkspaceCell)sender;
            KryptonWorkspaceCell next = cell;

            // There should always be a cell sending this event, but just in case!
            if (cell != null)
            {
                do
                {
                    // Find the next cell in sequence
                    next = (e.Forward ? NextVisibleCell(next) : PreviousVisibleCell(next));

                    // Do we need to wrap around?
                    if (next == null)
                        next = (e.Forward ? FirstVisibleCell() : LastVisibleCell());

                    // There should always be a valid cell to find, but just in case!
                    if (next == null)
                        break;

                    // If we manage to select a page then make its cell active and we are done
                    if ((e.Forward && next.SelectNextPage(null, false)) ||
                        (!e.Forward && next.SelectPreviousPage(null, false)))
                    {
                        ActiveCell = next;
                        ActiveCell.Select();
                        return;
                    }

                } while (next != cell);
            }
        }

        private void OnSeparatorNeedsPaint(object sender, NeedLayoutEventArgs e)
        {
            if (e.InvalidRect.IsEmpty)
                PerformNeedPaint(false);
            else
                Invalidate(e.InvalidRect, false);
        }

        private void OnPageClose(object sender, EventArgs e)
        {
            ClosePage(_menuPage);
            ClearContextMenuCache();
        }

        private void OnPageCloseAllButThis(object sender, EventArgs e)
        {
            CloseAllButThisPage(_menuPage);
            ClearContextMenuCache();
        }

        private void OnPageMoveNext(object sender, EventArgs e)
        {
            MovePageNext(_menuPage, true);
            ClearContextMenuCache();
        }

        private void OnPageMovePrevious(object sender, EventArgs e)
        {
            MovePagePrevious(_menuPage, true);
            ClearContextMenuCache();
        }

        private void OnPageSplitVert(object sender, EventArgs e)
        {
            PageSplitDirection(_menuCell, _menuPage, Orientation.Vertical);
        }

        private void OnPageSplitHorz(object sender, EventArgs e)
        {
            PageSplitDirection(_menuCell, _menuPage, Orientation.Horizontal);
        }

        private void OnPageMaximizeRestore(object sender, EventArgs e)
        {
            if (MaximizedCell != null)
                MaximizedCell = null;
            else
                MaximizedCell = _menuCell;
        }

        private void OnPageRebalance(object sender, EventArgs e)
        {
            ApplyRebalance();
        }

        private void PageSplitDirection(KryptonWorkspaceCell cell,
                                        KryptonPage page,
                                        Orientation orientation)
        {
            bool hadFocus = cell.ContainsFocus;

            // Prevent ActivePageChanged when only moving it to another cell
            SuspendActivePageChangedEvent();

            // Get the parent sequence the cell is inside
            KryptonWorkspaceSequence parentSequence = cell.WorkspaceParent as KryptonWorkspaceSequence;
            if (parentSequence != null)
            {
                // Find position of cell inside its parent sequence
                int index = parentSequence.Children.IndexOf(cell);

                // Create a new cell and move the context page into it
                KryptonWorkspaceCell newCell = new KryptonWorkspaceCell();
                cell.Pages.Remove(page);
                newCell.Pages.Add(page);

                // Give new cell the same sizing as the existing cell it comes from
                newCell.StarSize = cell.StarSize;

                // If parent is already in same direction as the split...
                if (parentSequence.Orientation == orientation)
                {
                    //...just add the new cell after the existing one
                    parentSequence.Children.Insert(index + 1, newCell);
                }
                else
                {
                    // Split is in opposite direction so create a new sequence to replace the existing cell
                    KryptonWorkspaceSequence newSequence = new KryptonWorkspaceSequence(orientation);

                    // Put the same size into the sequence as was in the original cell
                    newSequence.StarSize = cell.StarSize;

                    // Move the existing cell and the new cell into the new sequence
                    parentSequence.Children.Remove(cell);
                    newSequence.Children.Add(cell);
                    newSequence.Children.Add(newCell);

                    // Put new sequence in place of where the cell was
                    parentSequence.Children.Insert(index, newSequence);
                }
            }

            // If the cell that the page was moved from had the focus then set focus to follow the page
            if (hadFocus)
            {
                PerformLayout();
                CellForPage(page).Select();
            }

            ResumeActivePageChangedEvent();
            ClearContextMenuCache();
        }

        private void ClearContextMenuCache()
        {
            _menuPage = null;
            _menuCell = null;
        }

        private void SuspendActivePageChangedEvent()
        {
            _suspendActivePageChangedEvent++;
        }

        private void ResumeActivePageChangedEvent()
        {
            _suspendActivePageChangedEvent--;
        }

        private bool IsActivePageChangedEventSuspended
        {
            get { return (_suspendActivePageChangedEvent > 0); }
        }

        private Control GetActiverPageControlWithFocus()
        {
            if (ActivePage != null)
                return GetControlWithFocus(ActivePage);
            else
                return null;
        }

        private Control GetControlWithFocus(Control control)
        {
            // Does the provided control have the focus?
            if (control.Focused)
                return control;
            else
            {
                // Check each child hierarchy in turn
                foreach (Control child in control.Controls)
                    if (child.ContainsFocus)
                        return GetControlWithFocus(child);

                return null;
            }
        }

        private UniqueNameToPage BuildUniqueNameDictionary(KryptonPageCollection pages)
        {
            UniqueNameToPage dict = new UniqueNameToPage();

            // Add each page that has a non-null unique name but only add the same unique name once
            foreach (KryptonPage page in pages)
                if (!string.IsNullOrEmpty(page.UniqueName) && 
                    !dict.ContainsKey(page.UniqueName))
                    dict.Add(page.UniqueName, page);

            return dict;
        }

        private Image ReadOptionalImageElement(XmlReader xmlReader, string name)
        {
            Image retImage = null;

            // Is the optional element present?
            if (xmlReader.Name == name)
            {
                // Move to the contained CData element
                if (!xmlReader.Read())
                    throw new ArgumentException("An element was expected but could not be read in.");

                // Load the image from the elements contained data
                retImage = CommonHelper.XmlCDataToImage(xmlReader);

                // Read past the end of optional element                   
                if (!xmlReader.Read())
                    throw new ArgumentException("An element was expected but could not be read in.");
            }

            return retImage;
        }
        #endregion
    }
}
