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
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Win32;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Navigator control allows a variety of methods for moving around a collection of pages.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(KryptonNavigator), "ToolboxBitmaps.KryptonNavigator.bmp")]
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Pages")]
    [Designer("ComponentFactory.Krypton.Navigator.KryptonNavigatorDesigner, ComponentFactory.Krypton.Navigator, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Allows navigation between pages.")]
    [Docking(DockingBehavior.Ask)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonNavigator : VisualSimple,
                                    IDragTargetProvider
    {
        #region Static Fields
        private readonly static int WM_KEYUP = 0x0101;
        private static MethodInfo _containerSelect;
        #endregion

        #region Instance Fields
        private NavigatorMode _mode;
        private KryptonGroupPanel _childPanel; 
        private ViewBuilderBase _viewBuilder;
        private KryptonPage _selectedPage;
        private KryptonPageCollection _pages;
        private PaletteBackStyle _pageBackStyle;
        private NeedPaintHandler _needPagePaint;
        private KryptonContextMenu _kcm;
        private VisualPopupPage _visualPopupPage;
        private VisualPopupToolTip _visualPopupToolTip;
        private ToolTipManager _toolTipManager;
        private ToolTipManager _hoverManager;
        private IDragPageNotify _dragPageNotify;
        private KryptonPage[] _dragPages;
        private bool _forcedLayout;
        private bool _layingOut;
        private bool _useMnemonic;
        private bool _borrowedChildPanel;
        private bool _pageDragging;
        private bool _ignorePageVisibleChange;
        private bool _allowPageDrag;
        private bool _allowPageReorder;
        private bool _allowTabFocus;
        private bool _allowTabSelect;
        private bool _tabHoverStarted;
        private int _cachePageCount;
        private int _cachePageVisibleCount;

        // Palette storage objects
        private PaletteNavigatorRedirect _stateCommon;
        private PaletteNavigator _stateNormal;
		private PaletteNavigator _stateDisabled;
        private PaletteNavigatorOtherEx _stateTracking;
        private PaletteNavigatorOtherEx _statePressed;
        private PaletteNavigatorOther _stateSelected;
        private PaletteNavigatorOtherRedirect _stateFocus;
        private NavigatorBar _bar;
        private NavigatorButton _button;
        private NavigatorGroup _group;
        private NavigatorHeader _header;
        private NavigatorOutlook _outlook;
        private NavigatorPanel _panel;
        private NavigatorPopupPages _popupPages;
        private NavigatorStack _stack;
        private NavigatorToolTips _toolTips;
        #endregion

		#region Events
		/// <summary>
		/// Occurs after the selected page changes.
		/// </summary>
		[Category("Navigator Selection")]
		[Description("Occurs when the SelectedPage property is changed.")]
		public event EventHandler SelectedPageChanged;

        /// <summary>
        /// Occurs before a page is selected.
        /// </summary>
        [Category("Navigator Selection")]
        [Description("Occurs before a page is selected.")]
        public event EventHandler<KryptonPageCancelEventArgs> Selecting;

        /// <summary>
		/// Occurs after a page is selected.
		/// </summary>
        [Category("Navigator Selection")]
		[Description("Occurs after a page is selected.")]
        public event EventHandler<KryptonPageEventArgs> Selected;

        /// <summary>
        /// Occurs before a page is deselected.
        /// </summary>
        [Category("Navigator Selection")]
        [Description("Occurs before a page is deselected.")]
        public event EventHandler<KryptonPageCancelEventArgs> Deselecting;
        
        /// <summary>
		/// Occurs after a page is deselected.
		/// </summary>
        [Category("Navigator Selection")]
		[Description("Occurs after a page is deselected.")]
        public event EventHandler<KryptonPageEventArgs> Deselected;

        /// <summary>
        /// Occurs when the previous action occurs.
        /// </summary>
        [Category("Navigator Actions")]
        [Description("Occurs when the previous action occurs.")]
        public event EventHandler<DirectionActionEventArgs> PreviousAction;

        /// <summary>
        /// Occurs when the next action occurs.
        /// </summary>
        [Category("Navigator Actions")]
        [Description("Occurs when the next action occurs.")]
        public event EventHandler<DirectionActionEventArgs> NextAction;

        /// <summary>
        /// Occurs when the context action occurs.
        /// </summary>
        [Category("Navigator Actions")]
        [Description("Occurs when the context action occurs.")]
        public event EventHandler<ContextActionEventArgs> ContextAction;

        /// <summary>
        /// Occurs when the close action occurs.
        /// </summary>
        [Category("Navigator Actions")]
        [Description("Occurs when the close action occurs.")]
        public event EventHandler<CloseActionEventArgs> CloseAction;

        /// <summary>
        /// Occurs when the context action occurs.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when the drop down button is clicked in Outlook mode.")]
        public event EventHandler<KryptonContextMenuEventArgs> OutlookDropDown;

        /// <summary>
        /// Occurs when a page is about to be shown as a popup.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when a page is about to be shown as a popup.")]
        public event EventHandler<PopupPageEventArgs> DisplayPopupPage;

        /// <summary>
        /// Occurs when a page is about to be shown as a popup.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when you right click a page header and requests a context menu for display.")]
        public event EventHandler<ShowContextMenuArgs> ShowContextMenu;

        /// <summary>
        /// Occurs after the number of pages has changed.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs after the number of pages has changed.")]
        public event EventHandler TabCountChanged;

        /// <summary>
        /// Occurs after the number of visible pages has changed.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs after the number of visible pages has changed.")]
        public event EventHandler TabVisibleCountChanged;

        /// <summary>
        /// Occurs when the mouse clicks a page tab.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when the mouse clicks a page tab.")]
        public event EventHandler<KryptonPageEventArgs> TabClicked;

        /// <summary>
        /// Occurs when the mouse double clicks a page tab.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when the mouse double clicks a page tab.")]
        public event EventHandler<KryptonPageEventArgs> TabDoubleClicked;

        /// <summary>
        /// Occurs when the left mouse clicks the primary header.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when the left mouse clicks the primary header.")]
        public event EventHandler PrimaryHeaderLeftClicked;

        /// <summary>
        /// Occurs when the right mouse clicks the primary header.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when the right mouse clicks the primary header.")]
        public event EventHandler PrimaryHeaderRightClicked;

        /// <summary>
        /// Occurs when the mouse double clicks the primary header.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when the mouse double clicks the primary header.")]
        public event EventHandler PrimaryHeaderDoubleClicked;

        /// <summary>
        /// Occurs just before a page is reordered.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs just before a page is reordered.")]
        public event EventHandler<PageReorderEventArgs> BeforePageReorder;

        /// <summary>
        /// Occurs just before a page drag operation is started.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs just before a page drag operation is started.")]
        public event EventHandler<PageDragCancelEventArgs> BeforePageDrag;

        /// <summary>
        /// Occurs after a page drag operation has finished/aborted.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs after a page drag operation has finished/aborted.")]
        public event EventHandler<PageDragEndEventArgs> AfterPageDrag;

        /// <summary>
        /// Occurs when a page is being dropped.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when a page is being dropped.")]
        public event EventHandler<PageDropEventArgs> PageDrop;

        /// <summary>
        /// Occurs when control tabbing is starting.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when control tabbing is starting.")]
        public event EventHandler<CtrlTabCancelEventArgs> CtrlTabStart;

        /// <summary>
        /// Occurs when control tabbing is about to wrap around pages.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when control tabbing is about to wrap around pages.")]
        public event EventHandler<CtrlTabCancelEventArgs> CtrlTabWrap;

        /// <summary>
        /// Occurs when the mouse starts hovering over a tab.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when the mouse starts hovering over a tab.")]
        public event EventHandler<KryptonPageEventArgs> TabMouseHoverStart;

        /// <summary>
        /// Occurs when mouse hovering over a tab ends.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when mouse hovering over a tab ends.")]
        public event EventHandler TabMouseHoverEnd;

        /// <summary>
        /// Occurs when the user moves a tab to a new indexed position.
        /// </summary>
        [Category("Navigator")]
        [Description("Occurs when the user moves a tab to a new indexed position.")]
        public event EventHandler<TabMovedEventArgs> TabMoved;

        internal event PropertyChangedEventHandler ViewBuilderPropertyChanged;
		#endregion

		#region Identity
        static KryptonNavigator()
        {
            // Cache access to the internal 'Select' method of the ContainerControl
            _containerSelect = typeof(ContainerControl).GetMethod("Select",
                                                                  BindingFlags.Instance |
                                                                  BindingFlags.NonPublic);
        }

		/// <summary>
		/// Initialize a new instance of the KryptonNavigator class.
		/// </summary>
		public KryptonNavigator()
		{
            // We act as a container for child controls
            SetStyle(ControlStyles.ContainerControl, true);

            // Size of control is not related to a font, so fixed in size
            SetStyle(ControlStyles.FixedHeight | ControlStyles.FixedWidth, true);

            AssignDefaultFields();
            CreatePageCollection();
            CreateStorageObjects();
            CreateChildControl();
            CreateViewManager();
            CreateInternalObjects();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remove any associated popups
                DismissPopups();

                // If there anything to dispose?
                if (_viewBuilder != null)
                {
                    // Pull down the current view builder hierarchy
                    _viewBuilder.Destruct();
                    _viewBuilder = null;
                }

                // Dispose of the cached context menu
                if (_kcm != null)
                {
                    _kcm.Close();
                    _kcm.Dispose();
                    _kcm = null;
                }

                // Dispose of the buttons instances that have dispose interfaces
                _button.CloseButton.Dispose();
                _button.ContextButton.Dispose();
                _button.NextButton.Dispose();
                _button.PreviousButton.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

		#region Public Properties
        /// <summary>
        /// Gets and sets the name of the control.
        /// </summary>
        [Browsable(false)]
        public new string Name
        {
            get { return base.Name; }

            set
            {
                base.Name = value;
                _childPanel.Name = value + ".Panel";
            }
        }

        /// <summary>
        /// Gets and sets the automatic resize of the control to fit contents.
        /// </summary>
        [Browsable(true)]
        [Localizable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.All)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

		/// <summary>
		/// Gets the collection of pages in this navigator control.
		/// </summary>
		[Category("Visuals")]
		[Description("Collection of pages in the navigator control.")]
		[MergableProperty(false)]
        [Editor("ComponentFactory.Krypton.Navigator.NavigatorPageCollectionEditor, ComponentFactory.Krypton.Navigator, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public KryptonPageCollection Pages
		{
			get { return _pages; }
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
		/// Gets or sets the index of the currently-selected page.
		/// </summary>
		[Browsable(false)]
		[Category("Behavior")]
		[Description("Index of the currently-selected page.")]
		[DefaultValue(-1)]
		public int SelectedIndex
		{
			get 
            {
                // Return -1 if there is no selection
                if (SelectedPage == null)
                    return -1;
                else
                    return _pages.IndexOf(SelectedPage);
            }

			set
			{
				// Only interested in changes of value
                if (SelectedIndex != value)
				{
                    // If tab selection is disabled then prevent setting the selection
                    if (!AllowTabSelect)
                        throw new ArgumentOutOfRangeException("Cannot select a tab when AllowTabSelect=False");

					// Range check the index
					if ((value < 0) || (value >= Pages.Count))
						throw new ArgumentOutOfRangeException("value", "Index out of range");

                    // Can only select a page that is visible
                    if (!Pages[value].LastVisibleSet)
                        throw new ArgumentNullException("value", "Cannot select a page that is not visible");

                    // Request the change by changing the SelectedPage
                    SelectedPage = _pages[value];
				}
			}
		}

		/// <summary>
		/// Gets or sets the currently-selected page.
		/// </summary>
		[Category("Behavior")]
		[Description("Reference to the currently-selected page.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public KryptonPage SelectedPage
		{
			get { return _selectedPage; }

			set
			{
				// Only interested in changes of value
                if (_selectedPage != value)
				{
                    // If tab selection is disabled then prevent setting the selection
                    if (!AllowTabSelect)
                        throw new ArgumentOutOfRangeException("Cannot select a tab when AllowTabSelect=False");
                    
                    // You cannot remove the selection entirely by using null
					if (value == null)
						throw new ArgumentOutOfRangeException("value", "Value cannot be null");

                    // Check the page is in the pages collection
                    if (Pages.Contains(value))
                    {
                        // Can only select a page that is visible
                        if (!value.LastVisibleSet)
                            throw new ArgumentNullException("value", "Cannot select a page that is not visible");

                        // Change of selected page means we get rid of any showing popup page
                        DismissPopups();

                        // Is there a current selection?
                        if (_selectedPage != null)
                        {
                            // Create event information
                            KryptonPageCancelEventArgs e1 = new KryptonPageCancelEventArgs(_selectedPage, Pages.IndexOf(_selectedPage));

                            // Give event handlers a chance to cancel the deselection of the current page
                            OnDeselecting(e1);

                            // Do we need to cancel the change?
                            if (e1.Cancel)
                                return;
                        }

                        // Create event information
                        KryptonPageCancelEventArgs e2 = new KryptonPageCancelEventArgs(value, Pages.IndexOf(value));

                        // Give event handlers a chance to cancel the selection of the new page
                        OnSelecting(e2);

                        // Do we need to cancel the change?
                        if (e2.Cancel)
                            return;

                        // Cache the current selection
                        KryptonPage oldSelected = _selectedPage;

                        // Use the new selection
                        _selectedPage = value;

                        // If there was an old selection, generate event to show it is deselected
                        if (oldSelected != null)
                            OnDeselected(new KryptonPageEventArgs(oldSelected, Pages.IndexOf(oldSelected)));

                        // For the new selection, generate event to show it is selected
                        OnSelected(new KryptonPageEventArgs(_selectedPage, Pages.IndexOf(_selectedPage)));

                        // Generate the event that can be data bound
                        OnSelectedPageChanged(EventArgs.Empty);
                    }
                }
			}
		}

        /// <summary>
        /// Gets access to the bar specific settings.
        /// </summary>
        [Category("Visuals (Modes)")]
        [Description("Overrides for defining bar settings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorBar Bar
        {
            get { return _bar; }
        }

        private bool ShouldSerializeBar()
        {
            return !_bar.IsDefault;
        }

        /// <summary>
        /// Gets access to the stack specific settings.
        /// </summary>
        [Category("Visuals (Modes)")]
        [Description("Overrides for defining stack settings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorStack Stack
        {
            get { return _stack; }
        }

        private bool ShouldSerializeStack()
        {
            return !_stack.IsDefault;
        }

        /// <summary>
        /// Gets access to the outlook mode specific settings.
        /// </summary>
        [Category("Visuals (Modes)")]
        [Description("Overrides for defining outlook mode settings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorOutlook Outlook
        {
            get { return _outlook; }
        }

        private bool ShouldSerializeOutlook()
        {
            return !_outlook.IsDefault;
        }

        /// <summary>
        /// Gets access to button specifications and fixed button logic.
        /// </summary>
        [Category("Visuals (Modes)")]
        [Description("Button specifications and fixed button logic.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorButton Button
        {
            get { return _button; }
        }

        private bool ShouldSerializeButtons()
        {
            return !_button.IsDefault;
        }

        /// <summary>
        /// Gets access to the group specific settings.
        /// </summary>
        [Category("Visuals (Modes)")]
        [Description("Overrides for defining group settings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorGroup Group
        {
            get { return _group; }
        }

        private bool ShouldSerializeGroups()
        {
            return !_group.IsDefault;
        }

        /// <summary>
        /// Gets access to the header specific settings.
        /// </summary>
        [Category("Visuals (Modes)")]
        [Description("Overrides for defining header settings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorHeader Header
        {
            get { return _header; }
        }

        private bool ShouldSerializeHeaders()
        {
            return !_header.IsDefault;
        }

        /// <summary>
        /// Gets access to the panels specific settings.
        /// </summary>
        [Category("Visuals (Modes)")]
        [Description("Overrides for defining panel settings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorPanel Panel
        {
            get { return _panel; }
        }

        private bool ShouldSerializePanels()
        {
            return !_panel.IsDefault;
        }

        /// <summary>
        /// Gets access to the popup page specific settings.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining popup page settings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorPopupPages PopupPages
        {
            get { return _popupPages; }
        }

        private bool ShouldSerializePopupPages()
        {
            return !_popupPages.IsDefault;
        }

        /// <summary>
        /// Gets access to the tooltip specific settings.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tooltip settings.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigatorToolTips ToolTips
        {
            get { return _toolTips; }
        }

        private bool ShouldSerializeToolTips()
        {
            return !_toolTips.IsDefault;
        }

        /// <summary>
        /// Gets access to the common navigator appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common navigator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteNavigatorRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

		/// <summary>
		/// Gets access to the disabled navigator appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining disabled navigator appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteNavigator StateDisabled
		{
			get { return _stateDisabled; }
		}

		private bool ShouldSerializeStateDisabled()
		{
			return !_stateDisabled.IsDefault;
		}

		/// <summary>
		/// Gets access to the normal navigator appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining normal navigator appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteNavigator StateNormal
		{
			get { return _stateNormal; }
		}

		private bool ShouldSerializeStateNormal()
		{
			return !_stateNormal.IsDefault;
		}

        /// <summary>
		/// Gets access to the tracking navigator appearance entries.
		/// </summary>
		[Category("Visuals")]
		[Description("Overrides for defining tracking navigator appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PaletteNavigatorOtherEx StateTracking
		{
			get { return _stateTracking; }
		}

        private bool ShouldSerializeStateTracking()
		{
            return !_stateTracking.IsDefault;
		}

        /// <summary>
        /// Gets access to the pressed navigator appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed navigator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteNavigatorOtherEx StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the selected navigator appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining selected navigator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteNavigatorOther StateSelected
        {
            get { return _stateSelected; }
        }

        private bool ShouldSerializeStateSelected()
        {
            return !_stateSelected.IsDefault;
        }

        /// <summary>
        /// Gets access to the focus navigator appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining focus navigator appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteNavigatorOtherRedirect OverrideFocus
        {
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_stateFocus.IsDefault;
        }

        /// <summary>
		/// Gets and sets the display mode.
		/// </summary>
		[Category("Visuals")]
		[Description("Display mode of the control instance.")]
        [DefaultValue(typeof(NavigatorMode), "Bar - Tab - Group")]
		public NavigatorMode NavigatorMode
		{
			get { return _mode; }

			set
			{
				if (_mode != value)
				{
                    // Ignore change in mode as the view builder is already destructed
                    if (!IsDisposed && (_viewBuilder != null))
                    {
                        // Ask the view builder to pull down current view
                        _viewBuilder.Destruct();

                        _mode = value;

                        // Ask the view builder to create new view based on new mode
                        _viewBuilder = ViewBuilderBase.CreateViewBuilder(_mode);
                        _viewBuilder.Construct(this, ViewManager, Redirector);

                        // Need to layout the new view
                        if (!IsInitializing)
                            PerformNeedPaint(true);
                    }
				}
			}
		}

		/// <summary>
		/// Resets the Mode property to its default value.
		/// </summary>
		public void ResetNavigatorMode()
		{
            NavigatorMode = NavigatorMode.BarTabGroup;
		}

        /// <summary>
        /// Gets and sets the page background style.
        /// </summary>
        [Category("Visuals")]
        [Description("Page back style.")]
        [DefaultValue(typeof(PaletteBackStyle), "ControlClient")]
        public PaletteBackStyle PageBackStyle
        {
            get { return _pageBackStyle; }

            set
            {
                if (_pageBackStyle != value)
                {
                    _pageBackStyle = value;
                    OnViewBuilderPropertyChanged("PageBackStyle");
                }
            }
        }

        /// <summary>
        /// Gets or sets the default setting for allowing the page dragging from of the navigator.
        /// </summary>
        [Category("Behavior")]
        [Description("Defines the default setting for allowing page dragging from the navigator.")]
        [DefaultValue(false)]
        public bool AllowPageDrag
        {
            get { return _allowPageDrag; }
            set { _allowPageDrag = value; }
        }

        /// <summary>
        /// Gets or sets the default setting for allowing the page reordering using the mouse.
        /// </summary>
        [Category("Behavior")]
        [Description("Defines the default setting for allowing page reordering using the mouse.")]
        [DefaultValue(true)]
        public bool AllowPageReorder
        {
            get { return _allowPageReorder; }
            set { _allowPageReorder = value; }
        }

        /// <summary>
        /// Gets or sets if the tab headers are allowed to take the focus.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if the tab headers are allowed to take the focus.")]
        [DefaultValue(true)]
        public bool AllowTabFocus
        {
            get { return _allowTabFocus; }
            
            set 
            {
                if (_allowTabFocus != value)
                {
                    _allowTabFocus = value;

                    // If the tabs themselves are not allowed to be selected then the whole of 
                    // the Navigator cannot become the selected item. Without this a mouse down
                    // on a page header would cause focus to be sent to the Navigator and then
                    // automatically shifted elsewhere causing selecting problems.
                    SetStyle(ControlStyles.Selectable, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets if the tab headers can be selected by the users.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines if the tab headers can be selected by the users.")]
        [DefaultValue(true)]
        public bool AllowTabSelect
        {
            get { return _allowTabSelect; }

            set
            {
                if (_allowTabSelect != value)
                {
                    _allowTabSelect = value;

                    // If no longer allow a selected page and we have a selected page
                    if (!_allowTabSelect && (SelectedPage != null))
                    {
                        // Change to selection means we remove any showing popup page
                        DismissPopups();

                        // Generate event to show it is now deselected
                        OnDeselected(new KryptonPageEventArgs(SelectedPage, SelectedIndex));

                        // There is no longer a selected page
                        _selectedPage = null;

                        // Generate the event that can be data bound
                        OnSelectedPageChanged(EventArgs.Empty);
                        PerformNeedPaint(true);
                    }
                    else if (_allowTabSelect && (SelectedPage == null))
                    {
                        // Change to selection means we remove any showing popup page
                        DismissPopups();

                        // Select the first valid page as selection is now allowed
                        SelectFirstAvailablePage(); 
                        PerformNeedPaint(true);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether mnemonics select pages and button specs.
        /// </summary>
        [Category("Appearance")]
        [Description("Defines if mnemonic characters select pages and button specs.")]
        [DefaultValue(true)]
        public bool UseMnemonic
        {
            get { return _useMnemonic; }
            set { _useMnemonic = value; }
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
        /// Gets access to the ToolTipManager used for displaying tool tips.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTipManager ToolTipManager
        {
            get { return _toolTipManager; }
        }
        #endregion

        #region Public Methods
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
        public virtual DragTargetList GenerateDragTargets(PageDragEndData dragEndData, KryptonPageFlags allowFlags)
        {
            DragTargetList targets = new DragTargetList();

            // Generate target for the entire navigator client area
            targets.Add(new DragTargetNavigatorTransfer(RectangleToScreen(ClientRectangle), this, allowFlags));

            return targets;
        }

        /// <summary>
        /// Dismiss any showing popup page.
        /// </summary>
        public void DismissPopups()
        {
            // If there is a popup tooltip showing
            if (_visualPopupToolTip != null)
                VisualPopupManager.Singleton.EndPopupTracking(_visualPopupToolTip);

            // If there is a popup page showing
            if (_visualPopupPage != null)
                VisualPopupManager.Singleton.EndPopupTracking(_visualPopupPage);
        }

        /// <summary>
        /// Set the visible state of all the pages in the navigator to hidden.
        /// </summary>
        public void HideAllPages()
        {
            UpdateAllPagesVisible(false, null);
        }

        /// <summary>
        /// Set the visible state of all the pages in the navigator to hidden.
        /// </summary>
        /// <param name="excludeType">Ignore pages of the specific type.</param>
        public void HideAllPages(Type excludeType)
        {
            UpdateAllPagesVisible(false, excludeType);
        }

        /// <summary>
        /// Set the visible state of all the pages in the navigator to showing.
        /// </summary>
        public void ShowAllPages()
        {
            UpdateAllPagesVisible(true, null);
        }

        /// <summary>
        /// Set the visible state of all the pages in the navigator to showing.
        /// </summary>
        /// <param name="excludeType">Ignore pages of the specific type.</param>
        public void ShowAllPages(Type excludeType)
        {
            UpdateAllPagesVisible(true, excludeType);
        }

        /// <summary>
        /// Gets the KryptonPage associated with the provided point.
        /// This only works if the point intercepts a page header such as a tab header or check button.
        /// </summary>
        /// <param name="pt">Point in client co-ordinates.</param>
        /// <returns>KryptonPage or null.</returns>
        public KryptonPage PageFromPoint(Point pt)
        {
            if (ViewManager.Root != null)
            {
                // Get the view associated with the point
                ViewBase view = ViewManager.Root.ViewFromPoint(pt);

                // Climb the view hierarchy towards the root
                while (view != null)
                {
                    // If the view is associated with a page then return that page
                    if ((view.Component != null) && (view.Component is KryptonPage))
                        return (KryptonPage)view.Component;

                    view = view.Parent;
                }
            }

            return null;
        }

        /// <summary>
        /// Select the next page to the currently selected one.
        /// </summary>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <returns>True if a new page was selected; otherwise false.</returns>
        public bool SelectNextPage(bool wrap)
        {
            // Perform view specific page selection
            return _viewBuilder.SelectNextPage(wrap);
        }

        /// <summary>
        /// Select the next page to the one provided.
        /// </summary>
        /// <param name="page">Starting page for search.</param>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <returns>True if a new page was selected; otherwise false.</returns>
        public bool SelectNextPage(KryptonPage page, bool wrap)
        {
            // Perform view specific page selection
            return _viewBuilder.SelectNextPage(page, wrap, false);
        }

        /// <summary>
        /// Select the previous page to the currently selected one.
        /// </summary>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <returns>True if a new page was selected; otherwise false.</returns>
        public bool SelectPreviousPage(bool wrap)
        {
            // Perform view specific page selection
            return _viewBuilder.SelectPreviousPage(wrap);
        }

        /// <summary>
        /// Select the previous page to the one provided.
        /// </summary>
        /// <param name="page">Starting page for search.</param>
        /// <param name="wrap">Wrap around end of collection to the start.</param>
        /// <returns>True if a new page was selected; otherwise false.</returns>
        public bool SelectPreviousPage(KryptonPage page, bool wrap)
        {
            // Perform view specific page selection
            return _viewBuilder.SelectPreviousPage(page, wrap, false);
        }

        /// <summary>
        /// Generates a CloseAction event for a Navigator. 
        /// </summary>
        /// <returns>Returns the action that was performed.</returns>
        public CloseButtonAction PerformCloseAction()
        {
            return OnCloseAction(SelectedPage);
        }

        /// <summary>
        /// Generates a CloseAction event for a Navigator. 
        /// </summary>
        /// <param name="page">Page to perform close action on.</param>
        /// <returns>Returns the action that was performed.</returns>
        public CloseButtonAction PerformCloseAction(KryptonPage page)
        {
            return OnCloseAction(page);
        }

        /// <summary>
        /// Generates a ContextAction event for a Navigator. 
        /// </summary>
        public void PerformContextAction()
        {
            OnContextAction();
        }

        /// <summary>
        /// Generates a PreviousAction event for a Navigator. 
        /// </summary>
        /// <returns>Returns the action that was performed.</returns>
        public DirectionButtonAction PerformPreviousAction()
        {
            return OnPreviousAction();
        }

        /// <summary>
        /// Generates a NextAction event for a Navigator. 
        /// </summary>
        /// <returns>Returns the action that was performed.</returns>
        public DirectionButtonAction PerformNextAction()
        {
            return OnNextAction();
        }

        /// <summary>
        /// Fires the NeedPaint event and also repaints the selected page.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        public void PerformNeedPagePaint(bool needLayout)
        {
            OnNeedPagePaint(this, new NeedLayoutEventArgs(needLayout));
        }

        /// <summary>
        /// Gets the child panel used for displaying actual pages.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public KryptonGroupPanel ChildPanel
        {
            get { return _childPanel; }
        }

        /// <summary>
        /// Called by the designer to hit test a point.
        /// </summary>
        /// <param name="pt">Point to be tested.</param>
        /// <returns>True if a hit otherwise false.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool DesignerGetHitTest(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return false;

            // Ask the current view for a decision
            return _viewBuilder.DesignerGetHitTest(pt);
        }

        /// <summary>
        /// Called by the designer to get the component associated with the point.
        /// </summary>
        /// <param name="pt">Point to be tested.</param>
        /// <returns>Component associated with point or null.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Component DesignerComponentFromPoint(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return null;

            // Ask the current view for a decision
            return ViewManager.ComponentFromPoint(pt);
        }

        /// <summary>
        /// Called by the designer to indicate that the mouse has left the control.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void DesignerMouseLeave()
        {
            // Simulate the mouse leaving the control so that the tracking
            // element that thinks it has the focus is informed it does not
            OnMouseLeave(EventArgs.Empty);
        }

        /// <summary>
        /// Output debug information about the navigator.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void DebugOutput()
        {
            Console.WriteLine("Navigator Count:{0}", Pages.Count);

            foreach (KryptonPage page in Pages)
                Console.WriteLine("  Page Text:{0} Visible:{1}", page.Text, page.LastVisibleSet);
        }        
        #endregion

		#region Protected Overrides
		/// <summary>
		/// Creates a new instance of the control collection for the control.
		/// </summary>
		/// <returns>A new instance of KryptonNavigatorControlCollection assigned to the control.</returns>
		protected override ControlCollection CreateControlsInstance()
		{
            //  User should never adds controls directly to collection, only via the pages collection
            return new KryptonReadOnlyControls(this);
		}

		/// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnEnabledChanged(EventArgs e)
		{
			// Change in enabled state requires a layout and repaint
			PerformNeedPaint(true);
            
            // Let base class fire standard event
            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Let base class generate standard event
                base.OnInitialized(e);

                // Ask the view builder to pull down current view
                _viewBuilder.Destruct();

                // Ask the view builder to create new view based on new mode
                _viewBuilder = ViewBuilderBase.CreateViewBuilder(_mode);
                _viewBuilder.Construct(this, ViewManager, Redirector);

                if (LayoutOnInitialized)
                {
                    // Force a layout now that initialization is complete
                    OnLayout(new LayoutEventArgs(null, null));
                }
            }
        }

        /// <summary>
        /// Raises the Resize event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            // Let base class raise events
            base.OnResize(e);

            // We must have a layout calculation
            ForceControlLayout();
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            // We should have a view builder at all times
            if (_viewBuilder != null)
            {
                // If we are not allowed to have the focus then push it forward
                if (!_viewBuilder.CanFocus || !AllowTabFocus)
                {
                    // When pressing down the ctrl key to tab around we only set focus in the page itself.
                    // Otherwise user might be shift+tabbing around and we need to move outside the page.
                    if (CommonHelper.IsCtrlKeyPressed)
                        SelectNextPageControl(true, true);
                    else
                        SelectNextPageControl(!CommonHelper.IsShiftKeyPressed, false);
                }
                else
                    _viewBuilder.GotFocus();
            }

            // Let base class perform standard processing
            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            // We should have a view builder at all times
            if (_viewBuilder != null)
                _viewBuilder.LostFocus();

            // Let base class perform standard processing
            base.OnLostFocus(e);
        }

        /// <summary>
        /// Raises the MouseDown event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Only pressing the left mouse button can select the control
            if (e.Button == MouseButtons.Left)
            {
                // Get the element that is underneath the mouse
                ViewBase element = ViewManager.Root.ViewFromPoint(new Point(e.X, e.Y));

                // Ask the view builder if pressing the element needs to give us focus
                if (_viewBuilder.GiveNavigatorFocus(element))
                    Focus();
            }

            // Let base class perform standard processing
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Work out if this control needs to use Invoke to force a repaint.
        /// </summary>
        /// <returns>True to use Invoke; false to use Invalidate.</returns>
        protected override bool EvalInvokePaint
        {
            get
            {
                // If there is a panel holding the pages and that panel takes up the entire
                // client area then we need to use Invoke to force a refresh, that ensures 
                // that layout changes are actually processed when non of the client of the 
                // navigator is visible.
                return ((ChildPanel != null) && (ChildPanel.ClientRectangle == ClientRectangle));
            }
        }

        /// <summary>
        /// Previews a keyboard message.
        /// </summary>
        /// <param name="m">A Message that represents the window message to process.</param>
        /// <returns>true if the message was processed by the control; otherwise false.</returns>
        protected override bool ProcessKeyPreview(ref Message m)
        {
            // If the TAB key has just been released...
            if ((m.Msg == WM_KEYUP) && ((int)m.WParam.ToInt64() == (int)Keys.Tab))
            {
                //...and the SHIFT is also pressed
                if (CommonHelper.IsShiftKeyPressed)
                {
                    // If the focus has been moved to a page that does not have the focus
                    if ((SelectedPage != null) && !SelectedPage.ContainsFocus)
                    {
                        // We need to force another TAB+SHIFT to move the focus backwards
                        foreach (KryptonPage page in Pages)
                            if ((SelectedPage != page) && (page.ContainsFocus))
                            {
                                SelectNextPageControl(false, false);
                                break;
                            }
                    }
                }
            }

            // Let base class perform standard processing
            return base.ProcessKeyPreview(ref m);
        }

        /// <summary>
        /// Processes a dialog key.
        /// </summary>
        /// <param name="keyData">One of the Keys values that represents the key to process.</param>
        /// <returns>true if the key was processed by the control; otherwise, false.</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Find out which modifier keys are being pressed
            bool shift = ((keyData & Keys.Shift) == Keys.Shift);
            bool control = ((keyData & Keys.Control) == Keys.Control);
            bool alt = ((keyData & Keys.Alt) == Keys.Alt);

            // Extract just the key and not modifier keys
            Keys keyCode = (keyData & Keys.KeyCode);

            bool handled = false;

            // Process keys without modifiers
            switch (keyCode)
            {
                case Keys.Tab:
                    // We are not interested if the ALT key is pressed
                    if (!alt)
                    {
                        // If the CONTROL key is not being pressed then it is a standard
                        // tabbing around controls action which we need to implement ourself
                        if (!control)
                        {
                            // Mimic form level logic for selecting the next control but 
                            // always prevent movement to anything but the selected page
                            handled = SelectNextPageControl(!shift, false);
                        }
                        else
                        {
                            // CONTROL tabbing around the pages in the navigator 
                            // is handled in a view specific way
                            handled = _viewBuilder.ProcessDialogKey(keyData);
                        }
                    }
                    break;
            }

            // Let the view builder perform view specific actions
            if (!handled)
                handled = _viewBuilder.ProcessDialogKey(keyData);

            // If we did not handle the key then give it to the base class
            if (handled)
                return handled;
            else
                return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg">A Message, passed by reference, that represents the window message to process.</param>
        /// <param name="keyData">One of the Keys values that represents the key to process.</param>
        /// <returns>True is handled; otherwise false.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // If we have a selected page...
            if (SelectedPage != null)
            {
                if (SelectedPage.KryptonContextMenu != null)
                    if (SelectedPage.KryptonContextMenu.ProcessShortcut(keyData))
                        return true;

                if (SelectedPage.ContextMenuStrip != null)
                    if (CommonHelper.CheckContextMenuForShortcut(SelectedPage.ContextMenuStrip, ref msg, keyData))
                        return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Processes a mnemonic character.
        /// </summary>
        /// <param name="charCode">The mnemonic character entered.</param>
        /// <returns>true if the mnemonic was processsed; otherwise, false.</returns>
        protected override bool ProcessMnemonic(char charCode)
        {
            // If the button manager wants to process mnemonic characters and
            // this control is currently visible and enabled then...
            if (UseMnemonic && CanProcessMnemonic())
            {
                // Ask the view builder if the mnemonic can be used
                if (_viewBuilder.ProcessMnemonic(charCode))
                    return true;
            }

            // No match found, let base class do standard processing
            return base.ProcessMnemonic(charCode);
        }

        /// <summary>
		/// Gets the default size of the control.
		/// </summary>
		protected override Size DefaultSize
		{
			get { return new Size(250, 150); }
		}

        /// <summary>
        /// Update global event attachments.
        /// </summary>
        /// <param name="attach">True if attaching; otherwise false.</param>
        protected override void UpdateGlobalEvents(bool attach)
        {
            if (_childPanel != null)
            {
                if (attach)
                    _childPanel.AttachGlobalEvents();
                else
                    _childPanel.UnattachGlobalEvents();
            }

            base.UpdateGlobalEvents(attach);
        }
        #endregion

		#region Protected Raise Events
		/// <summary>
		/// Raises the Deselecting event.
		/// </summary>
		/// <param name="e">A KryptonPageCancelEventArgs containing event details.</param>
		protected virtual void OnDeselecting(KryptonPageCancelEventArgs e)
		{
			if (Deselecting != null)
				Deselecting(this, e);
		}

		/// <summary>
		/// Raises the Selecting event.
		/// </summary>
		/// <param name="e">A KryptonPageCancelEventArgs containing event details.</param>
		protected virtual void OnSelecting(KryptonPageCancelEventArgs e)
		{
			if (Selecting != null)
				Selecting(this, e);
		}

		/// <summary>
		/// Raises the Deselected event.
		/// </summary>
		/// <param name="e">A KryptonPageEventArgs containing event details.</param>
		protected virtual void OnDeselected(KryptonPageEventArgs e)
		{
			if (Deselected != null)
				Deselected(this, e);
		}

		/// <summary>
		/// Raises the Selected event.
		/// </summary>
		/// <param name="e">A KryptonPageEventArgs containing event details.</param>
		protected virtual void OnSelected(KryptonPageEventArgs e)
		{
			if (Selected != null)
				Selected(this, e);
		}

        /// <summary>
        /// Raises the BeforePageReorder event.
        /// </summary>
        /// <param name="de">A PageDragCancelEventArgs containing event details.</param>
        internal protected virtual void OnBeforePageReorder(PageReorderEventArgs de)
        {
            if (BeforePageReorder != null)
                BeforePageReorder(this, de);
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
        /// <param name="e">A EventArgs containing event details.</param>
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
		/// Raises the SelectedPageChanged event.
		/// </summary>
		/// <param name="e">An EventArgs containing event details.</param>
		protected virtual void OnSelectedPageChanged(EventArgs e)
		{
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Inform view builder of change
                _viewBuilder.SelectedPageChanged();

                // Request a layout is needed
                PerformNeedPaint(true);

                // If there is a selected page
                if (SelectedPage != null)
                {
                    // Then push it to the top of the z-order so it is visible
                    ChildPanel.Controls.SetChildIndex(SelectedPage, 0);

                    // If focus is with the navigator but not on the newly selected page...
                    if (ContainsFocus && !SelectedPage.ContainsFocus)
                    {
                        // ...and focus must be within the selected page if we have focus at all
                        if (!AllowTabFocus || !Focused)
                        {
                            // Then push focus to the first control on the new page
                            SelectNextPageControl(true, true);
                        }
                    }
                }

                // If the control size needs to change when a different page is selected
                if (AutoSize)
                    PerformLayout();

                if (SelectedPageChanged != null)
                    SelectedPageChanged(this, e);
            }
		}

        /// <summary>
        /// Raises and processes the PreviousAction event.
        /// </summary>
        /// <returns>Returns the action that was performed.</returns>
        protected virtual DirectionButtonAction OnPreviousAction()
        {
            DirectionButtonAction dba = DirectionButtonAction.None;

            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Create the event arguments
                DirectionActionEventArgs e = new DirectionActionEventArgs(SelectedPage,
                                                                          SelectedIndex,
                                                                          Button.PreviousButtonAction);

                if (PreviousAction != null)
                    PreviousAction(this, e);

                // Return the actual action performed
                dba = e.Action;

                // Ask the view to perform requested action on the view
                _viewBuilder.PerformPreviousAction(e.Action, e.Item);
            }

            return dba;
        }

        /// <summary>
        /// Raises and processes the NextAction event.
        /// </summary>
        /// <returns>Returns the action that was performed.</returns>
        protected virtual DirectionButtonAction OnNextAction()
        {
            DirectionButtonAction dba = DirectionButtonAction.None;

            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Create the event arguments
                DirectionActionEventArgs e = new DirectionActionEventArgs(SelectedPage,
                                                                          SelectedIndex,
                                                                          Button.NextButtonAction);

                if (NextAction != null)
                    NextAction(this, e);

                // Return the actual action performed
                dba = e.Action;

                // Ask the view to perform requested action on the view
                _viewBuilder.PerformNextAction(e.Action, e.Item);
            }

            return dba;
        }

        /// <summary>
        /// Raises and processes the ContextAction event.
        /// </summary>
        protected virtual void OnContextAction()
        {
            // Ask the context button spec to fire and perform default action
        }

        /// <summary>
        /// Raises athe CloseAction event.
        /// </summary>
        /// <param name="e">An CloseActionEventArgs containing the event args.</param>
        protected virtual void OnCloseAction(CloseActionEventArgs e)
        {
            if (CloseAction != null)
                CloseAction(this, e);
        }

        /// <summary>
        /// Should the OnInitialized call perform layout.
        /// </summary>
        protected virtual bool LayoutOnInitialized
        {
            get { return true; }
        }

        /// <summary>
        /// Raises and processes the CloseAction event.
        /// </summary>
        /// <param name="page">Page that is requested to be closed.</param>
        /// <returns>Returns the action that was performed.</returns>
        protected virtual CloseButtonAction OnCloseAction(KryptonPage page)
        {
            CloseButtonAction cba = CloseButtonAction.None;

            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Do not perform any action at design time
                if (!DesignMode)
                {
                    if (Pages.Contains(page))
                    {
                        // Create the event arguments
                        CloseActionEventArgs e = new CloseActionEventArgs(page,
                                                                          Pages.IndexOf(page),
                                                                          Button.CloseButtonAction);

                        if (CloseAction != null)
                            CloseAction(this, e);

                        // Return the action we processed
                        cba = e.Action;

                        // Process the requested action
                        switch (e.Action)
                        {
                            case CloseButtonAction.None:
                                // Do nothing
                                break;
                            case CloseButtonAction.RemovePage:
                                // If the page still exists after the event then remove it
                                if (Pages.Contains(e.Item))
                                    Pages.Remove(e.Item);
                                break;
                            case CloseButtonAction.RemovePageAndDispose:
                                // If the page still exists after the event
                                if (Pages.Contains(e.Item))
                                {
                                    // Remove it from the page collection
                                    Pages.Remove(e.Item);

                                    // Dispose of its resources
                                    e.Item.Dispose();
                                }
                                break;
                            case CloseButtonAction.HidePage:
                                // If the page still exists after the event then hide it
                                if (Pages.Contains(e.Item))
                                    e.Item.Hide();
                                break;
                            default:
                                // Should never happen!
                                Debug.Assert(false);
                                break;
                        }
                    }
                }
            }

            return cba;
        }

        /// <summary>
        /// Raises the TabCountChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event args.</param>
        protected virtual void OnTabCountChanged(EventArgs e)
        {
            if (TabCountChanged != null)
                TabCountChanged(this, e);
        }

        /// <summary>
        /// Raises the TabVisibleCountChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event args.</param>
        internal protected virtual void OnTabVisibleCountChanged(EventArgs e)
        {
            if (TabVisibleCountChanged != null)
                TabVisibleCountChanged(this, e);
        }

        /// <summary>
        /// Raises the TabClicked event.
        /// </summary>
        /// <param name="e">An KryptonPageEventArgs containing the event args.</param>
        internal protected virtual void OnTabClicked(KryptonPageEventArgs e)
        {
            if (TabClicked != null)
                TabClicked(this, e);
        }

        /// <summary>
        /// Raises athe TabDoubleClicked event.
        /// </summary>
        /// <param name="e">An KryptonPageEventArgs containing the event args.</param>
        internal protected virtual void OnTabDoubleClicked(KryptonPageEventArgs e)
        {
            if (TabDoubleClicked != null)
                TabDoubleClicked(this, e);
        }

        /// <summary>
        /// Raises athe PrimaryHeaderLeftClicked event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event args.</param>
        internal protected virtual void OnPrimaryHeaderLeftClicked(EventArgs e)
        {
            if (PrimaryHeaderLeftClicked != null)
                PrimaryHeaderLeftClicked(this, e);
        }

        /// <summary>
        /// Raises athe PrimaryHeaderRightClicked event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event args.</param>
        internal protected virtual void OnPrimaryHeaderRightClicked(EventArgs e)
        {
            if (PrimaryHeaderRightClicked != null)
                PrimaryHeaderRightClicked(this, e);
        }

        /// <summary>
        /// Raises athe PrimaryHeaderDoubleClicked event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event args.</param>
        internal protected virtual void OnPrimaryHeaderDoubleClicked(EventArgs e)
        {
            if (PrimaryHeaderDoubleClicked != null)
                PrimaryHeaderDoubleClicked(this, e);
        }

        /// <summary>
        /// Raises the OutlookDropDown event.
        /// </summary>
        /// <param name="kcm">Context menu about to be displayed.</param>
        internal protected virtual void OnOutlookDropDown(KryptonContextMenu kcm)
        {
            if (OutlookDropDown != null)
                OutlookDropDown(this, new KryptonContextMenuEventArgs(SelectedPage,
                                                                      SelectedIndex,
                                                                      kcm));
        }

        /// <summary>
        /// Raises the DisplayPopupPage event.
        /// </summary>
        /// <param name="e">A PopupPageEventArgs containing event data.</param>
        internal protected virtual void OnDisplayPopupPage(PopupPageEventArgs e)
        {
            if (DisplayPopupPage != null)
                DisplayPopupPage(this, e);
        }

        /// <summary>
        /// Raises the ShowContextMenu event.
        /// </summary>
        /// <param name="e">A ShowContextMenuArgs containing event data.</param>
        internal protected virtual void OnShowContextMenu(ShowContextMenuArgs e)
        {
            if (ShowContextMenu != null)
                ShowContextMenu(this, e);
        }

        /// <summary>
        /// Raises the CtrlTabStart event.
        /// </summary>
        /// <param name="e">An CtrlTabCancelEventArgs containing event details.</param>
        internal protected virtual void OnCtrlTabStart(CtrlTabCancelEventArgs e)
        {
            if (CtrlTabStart != null)
                CtrlTabStart(this, e);
        }

        /// <summary>
        /// Raises the CtrlTabWrap event.
        /// </summary>
        /// <param name="e">An CtrlTabCancelEventArgs containing event details.</param>
        internal protected virtual void OnCtrlTabWrap(CtrlTabCancelEventArgs e)
        {
            if (CtrlTabWrap != null)
                CtrlTabWrap(this, e);
        }

        /// <summary>
        /// Raises the TabMouseHoverStart event.
        /// </summary>
        /// <param name="e">An KryptonPageEventArgs containing event details.</param>
        protected virtual void OnTabMouseHoverStart(KryptonPageEventArgs e)
        {
            if (TabMouseHoverStart != null)
                TabMouseHoverStart(this, e);
        }

        /// <summary>
        /// Raises the TabMouseHoverEnd event.
        /// </summary>
        /// <param name="e">An EventArgs containing event details.</param>
        protected virtual void OnTabMouseHoverEnd(EventArgs e)
        {
            if (TabMouseHoverEnd != null)
                TabMouseHoverEnd(this, e);
        }

        /// <summary>
        /// Raises the TabMoved event.
        /// </summary>
        /// <param name="e">An TabMovedEventArgs containing event details.</param>
        internal protected virtual void OnTabMoved(TabMovedEventArgs e)
        {
            if (TabMoved != null)
                TabMoved(this, e);
        }

        /// <summary>
        /// Raises the ViewBuilderPropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property that has changed.</param>
        internal protected virtual void OnViewBuilderPropertyChanged(string propertyName)
        {
            if (ViewBuilderPropertyChanged != null)
                ViewBuilderPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Protected
        /// <summary>
        /// Force the layout logic to size and position the panels.
        /// </summary>
        protected void ForceControlLayout()
        {
            // Usually the layout will not occur if currently initializing but
            // we need to force the layout processing because overwise the size
            // of the panel controls will not have been calculated when controls
            // are added to the panels. That would then cause problems with
            // anchor controls as they would then resize incorrectly.
            if (!IsInitialized)
            {
                _forcedLayout = true;
                OnLayout(new LayoutEventArgs(null, null));
                _forcedLayout = true;
            }
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">A LayoutEventArgs containing the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Remember if we are inside a layout cycle
            _layingOut = true;
            base.OnLayout(levent);
            _layingOut = false;
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
                // If already showing a context menu, because right clicking a tab can do 
                // so, then we do not want to show the navigator defined context menu
                if (VisualPopupManager.Singleton.IsShowingCMS || (VisualPopupManager.Singleton.CurrentPopup != null))
                    return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
		/// Processes a notification from palette storage of a paint and optional layout required that involves the selected page.
		/// </summary>
		/// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected virtual void OnNeedPagePaint(object sender, NeedLayoutEventArgs e)
		{
            // Is there a selected page?
            if (SelectedPage != null)
            {
                // Then need to repaint the page as well
                SelectedPage.Invalidate();
            }

            // Pass request onto standard handler
            OnNeedPaint(sender, e);
		}        
        #endregion

        #region Internal
        internal ViewBuilderBase ViewBuilder
        {
            get { return _viewBuilder; }
        }

        internal ButtonSpecCollectionBase FixedSpecs
        {
            get { return _button.FixedSpecs; }
        }

        internal PaletteRedirect InternalRedirector
        {
            get { return Redirector; }
        }

        internal void InternalForceViewLayout()
        {
            ForceViewLayout();
        }

        internal ToolTipManager HoverManager
        {
            get { return _hoverManager; }
        }

        internal bool InternalDesignMode
        {
            get { return DesignMode; }
        }

        internal bool InternalCanLayout
        {
            get
            {
                return (IsInitialized || 
                        _forcedLayout || 
                        (DesignMode && (ViewManager != null)));
            }
        }

        internal bool PreviousActionValid
        {
            get
            {
                // If no selected page then there are no visible pages
                if (SelectedPage != null)
                {
                    // Search from start for any page that is visible
                    for (int i = 0; i < SelectedIndex; i++)
                        if (Pages[i].LastVisibleSet && Pages[i].Enabled)
                            return true;
                }

                return false;
            }
        }

        internal bool NextActionValid
        {
            get
            {
                // If no selected page then there are no visible pages
                if (SelectedPage != null)
                {
                    // Search from end for any page that is visible
                    for (int i = Pages.Count - 1; i > SelectedIndex; i--)
                        if (Pages[i].LastVisibleSet && Pages[i].Enabled)
                            return true;
                }

                return false;
            }
        }

        internal KryptonPage FirstActionPage()
        {
            // Search from start of collection to the end
            for (int i = 0; i < Pages.Count; i++)
                if (Pages[i].LastVisibleSet && Pages[i].Enabled)
                    return Pages[i];

            // Nothing visible in entire collection
            return null;
        }

        internal KryptonPage LastActionPage()
        {
            // Search backwards from end of collection to start
            for (int i = Pages.Count - 1; i >= 0; i++)
                if (Pages[i].LastVisibleSet && Pages[i].Enabled)
                    return Pages[i];

            // Nothing visible in entire collection
            return null;
        }

        internal KryptonPage PreviousActionPage(KryptonPage page)
        {
            Debug.Assert(page != null);

            // Get the index of the page
            int pos = Pages.IndexOf(page);

            // Search backwards towards start of pages collection
            for (int i = pos - 1; i >= 0; i--)
                if (Pages[i].LastVisibleSet && Pages[i].Enabled)
                    return Pages[i];

            // Nothing visible before provided page
            return null;
        }

        internal KryptonPage NextActionPage(KryptonPage page)
        {
            Debug.Assert(page != null);

            // Get the index of the page
            int pos = Pages.IndexOf(page);

            // Search towards end of pages collection
            for (int i = pos + 1; i < Pages.Count; i++)
                if (Pages[i].LastVisibleSet && Pages[i].Enabled)
                    return Pages[i];

            // Nothing visible after provided page
            return null;
        }

        internal PopupPagePosition ResolvePopupPagePosition()
        {
            PopupPagePosition position = PopupPages.Position;

            if (position == PopupPagePosition.ModeAppropriate)
                position = _viewBuilder.GetPopupPagePosition();

            return position;
        }

        internal void ShowPopupPage(KryptonPage page,
                                    ViewBase relative,
                                    EventHandler finishDelegate)
        {
            Debug.Assert(page != null);
            Debug.Assert(relative != null);
            
            bool delayDelegate = false;

            // We must have a page and relative element in order to show popup
            if (!DesignMode && (page != null) && (relative != null))
            {
                // Do not show if in the 'Never' show mode
                if (_popupPages.AllowPopupPages != PopupPageAllow.Never)
                {
                    // Check other allow options
                    if ((_popupPages.AllowPopupPages != PopupPageAllow.OnlyOutlookMiniMode) ||
                        (NavigatorMode == NavigatorMode.OutlookMini))
                    {
                        // Do not need to fire delegate until popup page is dismissed
                        delayDelegate = true;

                        // Create the popup window for the group
                        _visualPopupPage = new VisualPopupPage(this, page, Renderer);

                        // We need to know when disposed so the pressed state can be reversed
                        _visualPopupPage.DismissedDelegate = finishDelegate;
                        _visualPopupPage.Disposed += new EventHandler(OnVisualPopupPageDisposed);

                        // Get the client rectangle for the appropriate relative element
                        Rectangle clientRect = (PopupPages.Element == PopupPageElement.Item ?
                                                relative.ClientRectangle : ClientRectangle);

                        // Ask the popup to show itself relative to ourself
                        _visualPopupPage.ShowCalculatingSize(RectangleToScreen(clientRect));
                    }
                }
            }

            if (!delayDelegate && (finishDelegate != null))
                finishDelegate(this, EventArgs.Empty);
        }

        internal bool IsChildPanelBorrowed
        {
            get { return _borrowedChildPanel; }
        }

        internal void BorrowChildPanel()
        {
            if (!IsChildPanelBorrowed)
            {
                // Must cast to the correct type in order to access the 
                // internal method that allows a child control to be removed
                ((KryptonReadOnlyControls)Controls).RemoveInternal(ChildPanel);
                _borrowedChildPanel = true;
            }
        }

        internal void ReturnChildPanel()
        {
            if (IsChildPanelBorrowed)
            {
                // Must cast to the correct type in order to access the 
                // internal method that allows a child control to be added
                ((KryptonReadOnlyControls)Controls).AddInternal(ChildPanel);
                _borrowedChildPanel = false;
            }
        }

        internal void InternalDragStart(DragStartEventCancelArgs e, KryptonPage page)
        {
            if (DesignMode)
                e.Cancel = true;
            else
            {
                // Should not already be dragging, but if we are then ensure correct sequence of calls
                if (_pageDragging)
                {
                    _pageDragging = false;
                    if (DragPageNotify != null)
                        DragPageNotify.PageDragQuit(this);
                }

                // Create a list of the pages being dragged
                if (page != null)
                    _dragPages = new KryptonPage[] { page };
                else
                {
                    // Providing 'null' means we want all the pages
                    List<KryptonPage> list = new List<KryptonPage>();
                    foreach (KryptonPage p in Pages)
                        list.Add(p);

                    _dragPages = list.ToArray();
                }

                // Do any of the dragging pages have a flag set saying they can be dragged?
                bool allowPageDrag = false;
                foreach(KryptonPage p in _dragPages)
                    if (p.AreFlagsSet(KryptonPageFlags.AllowPageDrag))
                    {
                        allowPageDrag = true;
                        break;
                    }

                // Generate event allowing the DragPageNotify setting to be updated before the
                // actual drag processing occurs. You can even cancel the drag entirely.
                PageDragCancelEventArgs de = new PageDragCancelEventArgs(e.Point, e.Offset, e.Control, _dragPages);
                de.Cancel = (!AllowPageDrag || !allowPageDrag);
                OnBeforePageDrag(de);
                if (!de.Cancel)
                {
                    // Update with any changes made by the event
                    List<KryptonPage> list = new List<KryptonPage>();
                    foreach (KryptonPage p in de.Pages)
                        list.Add(p);
                    _dragPages = list.ToArray();

                    if (DragPageNotify != null)
                    {
                        // Give the notify interface a chance to reject the attempt to drag
                        DragPageNotify.PageDragStart(this, this, de);
                        _pageDragging = !de.Cancel;
                    }
                }

                e.Cancel = de.Cancel;
            }
        }

        internal void InternalDragMove(PointEventArgs e)
        {
            if (_pageDragging && (DragPageNotify != null))
                DragPageNotify.PageDragMove(this, e);
        }

        internal void InternalDragEnd(PointEventArgs e)
        {
            if (_pageDragging && (DragPageNotify != null))
            {
                if (DragPageNotify.PageDragEnd(this, e))
                {
                    // Success, so remove the pages from the navigator
                    foreach (KryptonPage page in _dragPages)
                        if (Pages.Contains(page))
                            Pages.Remove(page);
                }
            }

            _pageDragging = false;
            OnAfterPageDrag(new PageDragEndEventArgs(true, _dragPages));
            _dragPages = null;
        }

        internal void InternalDragQuit()
        {
            if (_pageDragging && (DragPageNotify != null))
                DragPageNotify.PageDragQuit(this);

            _pageDragging = false;
            OnAfterPageDrag(new PageDragEndEventArgs(false, _dragPages));
            _dragPages = null;
        }
        #endregion

        #region Private Identity
        private void AssignDefaultFields()
        {
            // Assign the default values
            _mode = NavigatorMode.BarTabGroup;
            _pageBackStyle = PaletteBackStyle.ControlClient;
            _allowPageReorder = true;
            _allowTabFocus = true;
            _allowTabSelect = true;
            _useMnemonic = true;
        }

        private void CreatePageCollection()
        {
            // Create page collection and monitor changes
            _pages = new KryptonPageCollection();
            _pages.Inserted += new TypedHandler<KryptonPage>(OnPageInserted);
            _pages.Removing += new TypedHandler<KryptonPage>(OnPageRemoving);
            _pages.Removed += new TypedHandler<KryptonPage>(OnPageRemoved);
            _pages.Clearing += new EventHandler(OnPageClearing);
            _pages.Cleared += new EventHandler(OnPageCleared);

            // Init fields used to notice a change in the page/page visible counts
            _cachePageCount = 0;
            _cachePageVisibleCount = 0;
        }

        private void CreateStorageObjects()
        {
            // Create the page print specific delegate
            _needPagePaint = new NeedPaintHandler(OnNeedPagePaint);

            // Create state storage objects
            _stateCommon = new PaletteNavigatorRedirect(this, Redirector, _needPagePaint);
            _stateDisabled = new PaletteNavigator(_stateCommon, _needPagePaint);
            _stateNormal = new PaletteNavigator(_stateCommon, _needPagePaint);
            _stateTracking = new PaletteNavigatorOtherEx(_stateCommon, _needPagePaint);
            _statePressed = new PaletteNavigatorOtherEx(_stateCommon, _needPagePaint);
            _stateSelected = new PaletteNavigatorOther(_stateCommon, _needPagePaint);
            _stateFocus = new PaletteNavigatorOtherRedirect(Redirector, Redirector, Redirector, 
                                                            Redirector, Redirector, _needPagePaint);

            // Create other storage objects
            _bar = new NavigatorBar(this, NeedPaintDelegate);
            _button = new NavigatorButton(this, NeedPaintDelegate);
            _group = new NavigatorGroup(this, NeedPaintDelegate);
            _header = new NavigatorHeader(this, NeedPaintDelegate);
            _outlook = new NavigatorOutlook(this, NeedPaintDelegate);
            _panel = new NavigatorPanel(this, NeedPaintDelegate);
            _popupPages = new NavigatorPopupPages(this, NeedPaintDelegate);
            _stack = new NavigatorStack(this, NeedPaintDelegate);
            _toolTips = new NavigatorToolTips(this, NeedPaintDelegate);

            // Need to know when the context button is about to show a context menu, so we
            // can then populate it with the correct set of values dependant on the current pages
            _button.ContextButton.KryptonContextMenu.Opening += new CancelEventHandler(OnOpeningContextMenu);
        }

        private void CreateViewManager()
        {
            // Create the view manager instance
            ViewManager = new ViewManager(this, new ViewLayoutPageShow(this));

            // Ask the view builder to create the view for the default mode
            _viewBuilder = ViewBuilderBase.CreateViewBuilder(_mode);
            _viewBuilder.Construct(this, ViewManager, Redirector);

            // We need to know when the layout cycle is starting/ending
            ViewManager.LayoutBefore += new EventHandler(OnViewManagerLayoutBefore);
            ViewManager.LayoutAfter += new EventHandler(OnViewManagerLayoutAfter);
        }

        private void CreateChildControl()
        {
            // Create the internal panel used for containing content
            _childPanel = new KryptonGroupPanel(this, 
                                                _stateCommon.HeaderGroup,
                                                _stateDisabled.HeaderGroup, 
                                                _stateNormal.HeaderGroup,
                                                new NeedPaintHandler(OnGroupPanelPaint));

            // Make sure the panel back style always mimics our back style
            _childPanel.PanelBackStyle = PaletteBackStyle.ControlClient;

            // We need to know whenever a control is removed from the child panel
            _childPanel.ControlRemoved += new ControlEventHandler(OnChildPanelControlRemoved);

            // Add panel to the controls collection
            ((KryptonReadOnlyControls)Controls).AddInternal(_childPanel);
        }

        private void CreateInternalObjects()
        {
            // Create the manager for handling tooltips
            _toolTipManager = new ToolTipManager();
            _toolTipManager.ShowToolTip += new EventHandler<ToolTipEventArgs>(OnShowToolTip);
            _toolTipManager.CancelToolTip += new EventHandler(OnCancelToolTip);

            // Create the manager for handling hovering
            _hoverManager = new ToolTipManager();
            _hoverManager.ShowInterval = 400;
            _hoverManager.CloseInterval = 400;
            _hoverManager.ShowToolTip += new EventHandler<ToolTipEventArgs>(OnStartHover);
            _hoverManager.CancelToolTip += new EventHandler(OnEndHover);
        }
        #endregion

        #region Private Page Handling
        private void OnPageInserted(object sender, TypedCollectionEventArgs<KryptonPage> e)
		{
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Change to page collection means we remove the popup page
                DismissPopups();

                // Hook into page events
                e.Item.VisibleChanged += new EventHandler(OnPageVisibleChanged);
                e.Item.EnabledChanged += new EventHandler(OnPageEnabledChanged);
                e.Item.AppearancePropertyChanged += new PropertyChangedEventHandler(OnPageAppearanceChanged);
                e.Item.FlagsChanged += new KryptonPageFlagsEventHandler(OnPageFlagsChanged);

                // Make the page inherit palette values from the navigator
                e.Item.SetInherit(this, StateCommon, StateDisabled, StateNormal,
                                        StateTracking, StatePressed, StateSelected, 
                                        OverrideFocus);

                // Remove the page from any existing parent control
                if (e.Item.Parent != null)
                    e.Item.Parent.Controls.Remove(e.Item);

                // Add the page into the child panel
                ChildPanel.Controls.Add(e.Item);

                // If there is no current selection and the new page is visible and we are 
                // allowed to have a selected page then it should become the selection
                if ((SelectedPage == null) && e.Item.LastVisibleSet && AllowTabSelect)
                    SelectedPage = e.Item;

                PageCollectionChanged();
            }
        }

        private void OnPageRemoving(object sender, TypedCollectionEventArgs<KryptonPage> e)
		{
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Change to page collection means we remove the popup page
                DismissPopups();

                // Is the selected page being removed?
                if (SelectedPage == e.Item)
                    SelectNextAvailablePage(e.Item);
            }
		}

        private void OnPageRemoved(object sender, TypedCollectionEventArgs<KryptonPage> e)
		{
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Stop the page inheriting palette values from the navigator
                e.Item.ResetInherit(this);

                // Unhook from page events
                e.Item.FlagsChanged -= new KryptonPageFlagsEventHandler(OnPageFlagsChanged);
                e.Item.AppearancePropertyChanged -= new PropertyChangedEventHandler(OnPageAppearanceChanged);
                e.Item.VisibleChanged -= new EventHandler(OnPageVisibleChanged);
                e.Item.EnabledChanged -= new EventHandler(OnPageEnabledChanged);

                // Remove page from the child panel
                if (ChildPanel.Controls.Contains(e.Item))
                    ChildPanel.Controls.Remove(e.Item);

                PageCollectionChanged();
            }
        }

		private void OnPageClearing(object sender, EventArgs e)
		{
            // If there is a page currently selected
            if (SelectedPage != null)
            {
                // Change to page collection means we remove the popup page
                DismissPopups();

                // Generate event to show it is now deselected
                OnDeselected(new KryptonPageEventArgs(SelectedPage, SelectedIndex));

                // There is no longer a selected page
                _selectedPage = null;

                // Generate the event that can be data bound
                OnSelectedPageChanged(EventArgs.Empty);
            }

            foreach (KryptonPage page in Pages)
            {
                // Stop the page inheriting palette values from the navigator
                page.ResetInherit(this);

                // Unhook from page events
                page.FlagsChanged -= new KryptonPageFlagsEventHandler(OnPageFlagsChanged);
                page.AppearancePropertyChanged -= new PropertyChangedEventHandler(OnPageAppearanceChanged);
                page.VisibleChanged -= new EventHandler(OnPageVisibleChanged);
                page.EnabledChanged -= new EventHandler(OnPageEnabledChanged);
            }
        }

        private void OnPageCleared(object sender, EventArgs e)
        {
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // If there are any child controls, remove them
                if (ChildPanel.Controls.Count > 0)
                    ChildPanel.Controls.Clear();

                PageCollectionChanged();
            }
        }

        private void OnPageVisibleChanged(object sender, EventArgs e)
        {
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                if (!IsChildPanelBorrowed && !_ignorePageVisibleChange)
                {
                    // Change to page visibility means we remove the popup page
                    DismissPopups();

                    // Cast to correct type
                    KryptonPage page = (KryptonPage)sender;

                    // Is this page in our collection and a child control
                    if (Pages.Contains(page) && ChildPanel.Controls.Contains(page))
                    {
                        // Are we allowed to have a selected page?
                        if (AllowTabSelect)
                        {
                            // If the page is becoming visible
                            if (page.LastVisibleSet)
                            {
                                // If there is no current page selected, then it must become selected
                                if (SelectedPage == null)
                                    SelectedPage = page;
                            }
                            else
                            {
                                // If page is the selected one
                                if (SelectedPage == page)
                                    SelectNextAvailablePage(SelectedPage);
                            }
                        }

                        // Inform view builder of a change in page visibility
                        _viewBuilder.PageVisibleStateChanged(page);
                    }

                    CheckForPageCountEvents();
                }
            }
        }

        private void OnPageEnabledChanged(object sender, EventArgs e)
        {
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Change to page enabled state means we remove the popup page
                DismissPopups();

                // Inform the view builder that a page has changed enabled state
                if (_viewBuilder != null)
                {
                    KryptonPage page = (KryptonPage)sender;
                    _viewBuilder.PageEnabledStateChanged(page);
                }
            }
        }

        private void OnPageAppearanceChanged(object sender, PropertyChangedEventArgs e)
        {
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Cast to correct type
                KryptonPage page = (KryptonPage)sender;

                // If the page is the currently selected one...
                if (SelectedPage == page)
                {
                    // And a change in a palette setting has occured...
                    if (e.PropertyName == "Palette")
                    {
                        // ...then need to repaint and layout to effect change
                        OnNeedPagePaint(page, new NeedLayoutEventArgs(true));
                    }
                }

                // Inform the current view builder of possible appearance change
                _viewBuilder.PageAppearanceChanged(page, e.PropertyName);
            }
        }

        private void OnPageFlagsChanged(object sender, KryptonPageFlagsEventArgs e)
        {
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Change to page flags state means we remove the popup page
                DismissPopups();

                // Cast to correct type
                KryptonPage page = (KryptonPage)sender;

                // Inform the current view builder of flags change
                _viewBuilder.PageFlagsChanged(page, e.Flags);
            }
        }

        private void PageCollectionChanged()
        {
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Inform view builder of a change in pages collections
                _viewBuilder.PageCollectionChanged();
                CheckForPageCountEvents();
            }
        }

        private void CheckForPageCountEvents()
        {
            // Has the number of pages changed?
            if (_cachePageCount != _pages.Count)
            {
                _cachePageCount = _pages.Count;
                OnTabCountChanged(EventArgs.Empty);
            }

            // Has the number of visible pages changed?
            if (_cachePageVisibleCount != _pages.VisibleCount)
            {
                _cachePageVisibleCount = _pages.VisibleCount;
                OnTabVisibleCountChanged(EventArgs.Empty);
            }
        }

        private void SelectFirstAvailablePage()
        {
            KryptonPage newSelection = null;
            KryptonPage firstEnabled = null;
            KryptonPage firstDisabled = null;

            // Process all pages
            for (int i = 0; i < Pages.Count; i++)
            {
                // Get the page to examine
                KryptonPage next = Pages[i];

                // Can only select a visible page
                if (next.LastVisibleSet)
                {
                    // Track the first found enabled and disabled pages found
                    if (next.Enabled && (firstEnabled == null))
                        firstEnabled = next;
                    else if (!next.Enabled && (firstDisabled == null))
                        firstDisabled = next;

                    // Create event information
                    KryptonPageCancelEventArgs args = new KryptonPageCancelEventArgs(next, Pages.IndexOf(next));

                    // Disabled pages default to not becoming selected
                    args.Cancel = !next.Enabled;

                    // Give event handlers a chance to cancel the selection of the new page
                    OnSelecting(args);

                    // Does this page want the selection?
                    if (!args.Cancel)
                    {
                        newSelection = next;
                        break;
                    }
                }
            }

            // If no page wants the new selection
            if (newSelection == null)
            {
                // Then force to the first enabled page
                if (firstEnabled != null)
                    newSelection = firstEnabled;
                else
                {
                    // Nothing enabled, so force to first disabled page
                    if (firstDisabled != null)
                        newSelection = firstDisabled;
                }
            }

            // Use new selection
            _selectedPage = newSelection;

            // If a new selection was made
            if (SelectedPage != null)
            {
                // Generate event to show it is now selected
                OnSelected(new KryptonPageEventArgs(SelectedPage, SelectedIndex));
            }

            // Generate the event that can be data bound
            OnSelectedPageChanged(EventArgs.Empty);
        }

        private void SelectNextAvailablePage(KryptonPage begin)
        {
            // Generate event to show it is now deselected
            OnDeselected(new KryptonPageEventArgs(begin, Pages.IndexOf(begin)));

            KryptonPage newSelection = null;
            KryptonPage firstEnabled = null;
            KryptonPage firstDisabled = null;

            // Start the search by moving forwards
            bool forward = true;

            // Start searching from the provided page
            KryptonPage start = begin;

            // Process all pages except the current one to find available pape
            for (int i = 0; i < Pages.Count - 1; i++)
            {
                KryptonPage next;

                // Are we already at the last page in the pages collection?
                if (Pages.IndexOf(start) == (Pages.Count - 1))
                {
                    // Then need to reverse searching direction
                    forward = false;

                    // Next page is the one before the beginning page
                    next = Pages[Pages.IndexOf(begin) - 1];
                }
                else
                {
                    // Otherwise just move to the next page in sequence
                    next = Pages[Pages.IndexOf(start) + (forward ? 1 : -1)];
                }

                // Can only select a visible page
                if (next.LastVisibleSet)
                {
                    // Track the first found enabled and disabled pages found
                    if (next.Enabled && (firstEnabled == null))
                        firstEnabled = next;
                    else if (!next.Enabled && (firstDisabled == null))
                        firstDisabled = next;

                    // Create event information
                    KryptonPageCancelEventArgs args = new KryptonPageCancelEventArgs(next, Pages.IndexOf(next));

                    // Disabled pages default to not becoming selected
                    args.Cancel = !next.Enabled;

                    // Give event handlers a chance to cancel the selection of the new page
                    OnSelecting(args);

                    // Does this page want the selection?
                    if (!args.Cancel)
                    {
                        newSelection = next;
                        break;
                    }
                }

                // Move forward a page for next loop
                start = next;
            }

			// If no page wants the new selection
			if (newSelection == null)
			{
				// Then force to the first enabled page
				if (firstEnabled != null)
					newSelection = firstEnabled;
				else
				{
					// Nothing enabled, so force to first disabled page
					if (firstDisabled != null)
						newSelection = firstDisabled;
				}
			}

            // Use new selection
            _selectedPage = newSelection;

            // If a new selection was made
            if (SelectedPage != null)
            {
                // Generate event to show it is now selected
                OnSelected(new KryptonPageEventArgs(SelectedPage, SelectedIndex));
            }

            // Generate the event that can be data bound
            OnSelectedPageChanged(EventArgs.Empty);
        }

        private void UpdateAllPagesVisible(bool visible, Type excludeType)
        {
            // Is it safe to perform any processing?
            if (!IsDisposed && (_viewBuilder != null) && !IsChildPanelBorrowed)
            {
                // Do we need to make any changes in page visibility?
                if ((visible && (Pages.VisibleCount != Pages.Count)) || (!visible && (Pages.VisibleCount > 0)))
                {
                    // Do not allow page visible change event processing because it causes visual changes
                    // when we want to make all the changes in one go before showign the visual update
                    _ignorePageVisibleChange = true;

                    // Change to page visibility means we remove the popup page
                    DismissPopups();

                    // Update all the pages with the same visible state
                    foreach (KryptonPage page in Pages)
                    {
                        // Only update pages that do not match the incoming type
                        if ((excludeType == null) || !(page.GetType() == excludeType))
                        {
                            page.Visible = visible;
                            _viewBuilder.PageVisibleStateChanged(page);
                        }
                    }

                    _ignorePageVisibleChange = false;

                    // If tab selection is allowed then generate appropriate events
                    if (AllowTabSelect)
                    {
                        if (visible)
                            SelectFirstAvailablePage();
                        else
                        {
                            // Generate events to indicate that the selected has been removed
                            OnDeselected(new KryptonPageEventArgs(SelectedPage, SelectedIndex));
                            _selectedPage = null;
                            OnSelectedPageChanged(EventArgs.Empty);
                        }
                    }

                    CheckForPageCountEvents();
                    PerformNeedPaint(true);
                }
            }
        }
        #endregion

        #region Private
        private void OnOpeningContextMenu(object sender, CancelEventArgs e)
        {
            // Ignore call as view builder is already destructed
            if (!IsDisposed && (_viewBuilder != null))
            {
                if (DesignMode)
                {
                    // Never show the context menu at design time
                    e.Cancel = true;
                }
                else
                {
                    // Get access to the menu items for selecting a page
                    KryptonContextMenu contextMenu = (KryptonContextMenu)sender;

                    // Kill any existing contents and add a items collection for the page entries
                    contextMenu.Items.Clear();
                    KryptonContextMenuItems contextMenuItems = new KryptonContextMenuItems();
                    contextMenu.Items.Add(contextMenuItems);

                    // Process each page for those that need adding to context strip
                    int menuItems = 0;
                    foreach (KryptonPage page in Pages)
                    {
                        // We always add the currently selected page and 
                        // any other that is both visible and enabled
                        if ((page == SelectedPage) || (page.LastVisibleSet && page.Enabled))
                        {
                            // Add a vertical break after every 20 items
                            if ((menuItems > 0) && (menuItems % 20) == 0)
                            {
                                KryptonContextMenuSeparator vertBreak = new KryptonContextMenuSeparator();
                                vertBreak.Horizontal = false;
                                contextMenuItems.Items.Add(vertBreak);
                            }

                            // Create a menu item for the page
                            KryptonContextMenuItem pageMenuItem = new KryptonContextMenuItem(page.GetTextMapping(Button.ContextMenuMapText),
                                                                                             page.GetImageMapping(Button.ContextMenuMapImage),
                                                                                             new EventHandler(OnContextMenuClick));

                            // Should the item be enabled?
                            pageMenuItem.Enabled = page.Enabled;

                            // The selected page should be checked
                            pageMenuItem.Checked = (page == SelectedPage);

                            // Use tag to store a back reference to the page
                            pageMenuItem.Tag = page;

                            // Add to end of the strip
                            contextMenuItems.Items.Add(pageMenuItem);
                            menuItems++;
                        }
                    }

                    // Create the event arguments
                    ContextActionEventArgs cae = new ContextActionEventArgs(SelectedPage,
                                                                            SelectedIndex,
                                                                            Button.ContextButtonAction,
                                                                            contextMenu);

                    if (ContextAction != null)
                        ContextAction(this, cae);

                    // Process the requested action
                    switch (cae.Action)
                    {
                        case ContextButtonAction.SelectPage:
                            // Do nothing, allow context menu to be shown
                            break;
                        default:
                            // Cancel the showing of the context menu
                            e.Cancel = true;
                            break;
                    }
                }
            }
        }

        private void OnShowToolTip(object sender, ToolTipEventArgs e)
        {
            if (!IsDisposed && (_viewBuilder != null))
            {
                // Do not show tooltips when the form we are in does not have focus
                Form topForm = FindForm();
                if ((topForm != null) && !topForm.ContainsFocus)
                    return;

                // Never show tooltips are design time
                if (!DesignMode)
                {
                    IContentValues sourceContent = null;
                    LabelStyle toolTipStyle = LabelStyle.ToolTip;

                    // Find the page associated with the tooltip request
                    KryptonPage toolTipPage = _viewBuilder.PageFromView(e.Target);

                    // If the tooltip is for a krypton page header
                    if (toolTipPage != null)
                    {
                        // Are we allowed to show page related tooltips
                        if (_toolTips.AllowPageToolTips)
                        {
                            // Create a helper object to provide tooltip values
                            PageToToolTipMapping pageMapping = new PageToToolTipMapping(toolTipPage,
                                                                                       _toolTips.MapImage,
                                                                                       _toolTips.MapText,
                                                                                       _toolTips.MapExtraText);

                            // Is there actually anything to show for the tooltip
                            if (pageMapping.HasContent)
                            {
                                sourceContent = pageMapping;
                                toolTipStyle = toolTipPage.ToolTipStyle;
                            }
                        }
                    }
                    else
                    {
                        // Find the button spec associated with the tooltip request
                        ButtonSpec buttonSpec = _viewBuilder.ButtonSpecFromView(e.Target);

                        // If the tooltip is for a button spec
                        if (buttonSpec != null)
                        {
                            // Are we allowed to show page related tooltips
                            if (_toolTips.AllowButtonSpecToolTips)
                            {
                                // Create a helper object to provide tooltip values
                                ButtonSpecToContent buttonSpecMapping = new ButtonSpecToContent(Redirector, buttonSpec);

                                // Is there actually anything to show for the tooltip
                                if (buttonSpecMapping.HasContent)
                                {
                                    sourceContent = buttonSpecMapping;
                                    toolTipStyle = buttonSpec.ToolTipStyle;
                                }
                            }
                        }
                    }

                    if (sourceContent != null)
                    {
                        // Remove any currently showing tooltip
                        if (_visualPopupToolTip != null)
                            _visualPopupToolTip.Dispose();

                        // Create the actual tooltip popup object
                        _visualPopupToolTip = new VisualPopupToolTip(Redirector,
                                                                     sourceContent,
                                                                     Renderer,
                                                                     PaletteBackStyle.ControlToolTip,
                                                                     PaletteBorderStyle.ControlToolTip,
                                                                     CommonHelper.ContentStyleFromLabelStyle(toolTipStyle));

                        _visualPopupToolTip.Disposed += new EventHandler(OnVisualPopupToolTipDisposed);

                        // Show relative to the provided screen point
                        _visualPopupToolTip.ShowCalculatingSize(e.ScreenPt);
                    }
                }
            }
        }

        private void OnCancelToolTip(object sender, EventArgs e)
        {
            // Remove any currently showing tooltip
            if (_visualPopupToolTip != null)
                _visualPopupToolTip.Dispose();
        }

        private void OnStartHover(object sender, ToolTipEventArgs e)
        {
            if (!IsDisposed && (_viewBuilder != null))
            {
                // We do not provide hover support when the form does not have the focus
                Form topForm = FindForm();
                if ((topForm != null) && !topForm.ContainsFocus)
                    return;

                // Never generate hover events at design time
                if (!DesignMode)
                {
                    // Find the page associated with the hover request
                    KryptonPage hoverPage = _viewBuilder.PageFromView(e.Target);
                    if (hoverPage != null)
                    {
                        OnTabMouseHoverStart(new KryptonPageEventArgs(hoverPage, Pages.IndexOf(hoverPage)));
                        _tabHoverStarted = true;
                    }
                }
            }
        }

        private void OnEndHover(object sender, EventArgs e)
        {
            // Only notify the end of the hover if we have generated a start
            if (_tabHoverStarted)
            {
                OnTabMouseHoverEnd(EventArgs.Empty);
                _tabHoverStarted = false;
            }
        }

        private void OnVisualPopupToolTipDisposed(object sender, EventArgs e)
        {
            // Unhook events from the specific instance that generated event
            VisualPopupToolTip popupToolTip = (VisualPopupToolTip)sender;
            popupToolTip.Disposed -= new EventHandler(OnVisualPopupToolTipDisposed);

            // Not showing a popup page any more
            _visualPopupToolTip = null;
        }

        private void OnVisualPopupPageDisposed(object sender, EventArgs e)
        {
            // Unhook events from the specific instance that generated event
            VisualPopupPage popupPage = (VisualPopupPage)sender;
            popupPage.Disposed -= new EventHandler(OnVisualPopupPageDisposed);

            // Not showing a popup page any more
            _visualPopupPage = null;
        }

        private void OnChildPanelControlRemoved(object sender, ControlEventArgs e)
        {
            // If a krypton page is being removed
            if (e.Control is KryptonPage)
            {
                // Cast to correct type
                KryptonPage page = (KryptonPage)e.Control;

                // If the page is still in the pages collection
                if (Pages.Contains(page))
                {
                    // Then remove it, because a page must be in the page collection and a
                    // child control or neither. But should never be in a half state of being
                    // in the pages collection but not a child control.
                    Pages.Remove(page);
                }
            }
        }

        private void OnGroupPanelPaint(object sender, NeedLayoutEventArgs e)
        {
            // If the child panel is layout out but not because we are, then it must be
            // laying out because a child has changed visibility/size/etc. If we are an
            // AutoSize control then we need to ensure we layout as well to change size.
            if (e.NeedLayout && !_layingOut && AutoSize)
                PerformNeedPaint(true);
        }

        internal bool SelectNextPageControl(bool forward, bool onlyCurrentPage)
        {
            // Find the control in our hierarchy that has the focus
            Control focus = CommonHelper.GetControlWithFocus(this);

            // If nothing has the focus then we cannot perform processing
            if (focus != null)
            {
                Control rootControl;

                if (onlyCurrentPage)
                    rootControl = SelectedPage;
                else
                {
                    // Get the owning form because we want to search all controls in the
                    // form hierarchy and not just the controls in our own hierarchy
                    rootControl = focus.FindForm();

                    // If we cannot find an owning form
                    if (rootControl == null)
                    {
                        // Walk up the parent chain until we reach the top
                        rootControl = focus;
                        while (rootControl.Parent != null)
                            rootControl = rootControl.Parent;
                    }
                }

                if (rootControl != null)
                {
                    // Start searching from the current focus control
                    Control next = focus;

                    // Have we wrapped around the end yet?
                    bool wrapped = false;

                    do
                    {
                        // Find the next control in sequence
                        next = rootControl.GetNextControl(next, forward);

                        // If no more controls found, then finished
                        if (next == null)
                        {
                            // If already wrapped around end of list then must be finished
                            if (wrapped)
                                return false;

                            // Keep going from the start
                            wrapped = true;
                        }
                        else
                        {
                            // We can only be the next control if we accept the focus
                            if ((next != this) || ((next == this) && _viewBuilder.CanFocus))
                            {
                                // Is the next control inside ourself as a container?
                                bool nextInside = Contains(next);

                                // Cannot select a control if that control is on an unselected krypton page
                                if (!NextOnUnselectedKryptonPage(next))
                                {
                                    // Cannot select an inside control when in a tab strip mode
                                    if (!(nextInside && _viewBuilder.IsTabStripMode))
                                    {
                                        // If the control is not inside this navigator instance or if it 
                                        // is inside this naviagator but it is also inside the selected page 
                                        // then maybe it can be selected.
                                        if (!nextInside || (nextInside && (SelectedPage != null) && SelectedPage.Contains(next)))
                                        {
                                            // If the next control is allowed to become selected 
                                            // and allowed to be selected because of a tab action
                                            if (next.CanSelect && next.TabStop)
                                            {
                                                // Is the next control a container control?
                                                if (next is ContainerControl)
                                                {
                                                    // If the source control of the next/previous is inside the container
                                                    // then we do not want to stop at the container itself as that would 
                                                    // just put the focus straight back into the container. So keep going.
                                                    if (!next.Contains(focus))
                                                    {
                                                        // We need to call the protected select method in order to have 
                                                        // it perform an internal select of the first/last ordered item
                                                        _containerSelect.Invoke(next, new object[] { true, forward });
                                                        return true;
                                                    }
                                                }
                                                else
                                                {
                                                    // Select the actual control
                                                    next.Select();
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    while (next != focus);
                }
            }

            // We cannot select the next page control
            return false;
        }

        private bool NextOnUnselectedKryptonPage(Control next)
        {
            do
            {
                // Is this control actually a KryptonPage?
                if (next is KryptonPage)
                {
                    // Cast to the correct type
                    KryptonPage page = (KryptonPage)next;

                    // If the page is inside a krypton container that is a navigator instance
                    if ((page.KryptonParentContainer != null) && (page.KryptonParentContainer is KryptonNavigator))
                    {
                        // Cast to correct type
                        KryptonNavigator nav = (KryptonNavigator)page.KryptonParentContainer;

                        // Tell the caller if the original control is inside a page that is unselected
                        return (nav.SelectedPage != page);
                    }
                }

                // Move up the chain one level
                next = next.Parent;

            // Keep going until we reach the top of the parent chain
            } while (next != null);

            // Did not find the control is on a KryptonPage, so definitely not on an unselected KryptonPage
            return false;
        }

        private void OnViewManagerLayoutBefore(object sender, EventArgs e)
        {
            // Tell the view to perform pre layout actions
            ViewBuilder.PreLayout();
        }

        private void OnViewManagerLayoutAfter(object sender, EventArgs e)
        {
            // Tell the view to perform post layout actions
            ViewBuilder.PostLayout();
        }

        private void ResetCachedKryptonContextMenu()
        {
            // First time around we need to create the context menu
            if (_kcm == null)
                _kcm = new KryptonContextMenu();

            // Remove any existing items
            _kcm.Items.Clear();
        }

        private void OnContextMenuClick(object sender, EventArgs e)
        {
            // Cast to correct type
            KryptonContextMenuItem menuItem = (KryptonContextMenuItem)sender;

            // Get the page this menu item referencs
            KryptonPage page = (KryptonPage)menuItem.Tag;

            // Try and select the page if we are allowed selected pages
            if (AllowTabSelect)
                SelectedPage = page;
        }
        #endregion
    }
}
