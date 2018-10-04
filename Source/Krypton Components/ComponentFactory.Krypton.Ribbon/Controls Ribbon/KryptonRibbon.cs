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
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Ribbon control presents a tabbed set of user options.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(KryptonRibbon), "ToolboxBitmaps.KryptonRibbon.bmp")]
    [DefaultEvent("SelectedTabChanged")]
    [DefaultProperty("RibbonTabs")]
    [Designer("ComponentFactory.Krypton.Ribbon.KryptonRibbonDesigner, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e")]
    [DesignerCategory("code")]
    [Description("Ribbon control presents a tabbed set of user options.")]
    [Docking(DockingBehavior.Never)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class KryptonRibbon : VisualSimple,
                                 IMessageFilter
    {
        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpecAny instances.
        /// </summary>
        public class RibbonButtonSpecAnyCollection : ButtonSpecCollection<ButtonSpecAny> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the RibbonButtonSpecAnyCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public RibbonButtonSpecAnyCollection(KryptonRibbon owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Static Fields
        private static readonly Size _keyTipInflateSize = new Size(8, 30);
        private static MethodInfo _containerSelect;
        #endregion

        #region Instance Fields
        // Private
        private CalculatedValues _calculatedValues;
        private NeedPaintHandler _needPaintGroups;
        private VisualPopupMinimized _minimizedPopup;
        private KryptonContextMenu _kcm;
        private EventHandler _kcmFinishDelegate;
        private IntPtr _keyboardFocusWindow;
        private bool _keyboardFocusCaret;
        private bool _keyboardMode;
        private bool _keyboardKeyTips;
        private bool _designHelpers;
        private bool _invalidateOnResize;
        private bool _uxthemeNotAvailable;
        private bool _lostFocusLosesKeyboard;
        private bool _ignoreRestoreFocus;
        private bool _altDown;
        private int _altUpCount;
        private int _keyboardAltUpCount;
        private ViewBase _focusView;
        private KeyTipControl _keyTipControlE;
        private KeyTipControl _keyTipControlD;
        private KeyTipMode _keyTipMode;
        private Control _keyTipRelative;
        private Button _hiddenFocusTarget;

        // View Elements
        private ViewDrawRibbonPanel _drawPanel;
        private ViewDrawPanel _drawMinimizedPanel;
        private ViewLayoutDocker _rootDocker;
        private ViewLayoutDocker _ribbonDocker;
        private ViewDrawRibbonComposition _compositionArea;
        private ViewDrawRibbonCaptionArea _captionArea;
        private ViewLayoutRibbonTabsArea _tabsArea;
        private ViewLayoutRibbonGroupsArea _groupsArea;
        private ViewDrawRibbonQATBorder _qatBelowRibbon;
        private ViewLayoutRibbonQATFromRibbon _qatBelowContents;
        private ViewDrawRibbonMinimizeBar _minimizeBar;

        // User ButtonSpecs
        private RibbonButtonSpecAnyCollection _buttonSpecs;

        // Palettes
        private PaletteRibbonStyles _ribbonStyles;
        private PaletteRibbonRedirect _stateCommon;
        private PaletteRibbonDisabled _stateDisabled;
        private PaletteRibbonNormal _stateNormal;
        private PaletteRibbonAppGroupTab _stateTracking;
        private PaletteRibbonAppButton _statePressed;
        private PaletteRibbonGroupAreaTab _stateCheckedNormal;
        private PaletteRibbonJustTab _stateCheckedTracking;
        private PaletteRibbonJustGroup _stateContextNormal;
        private PaletteRibbonGroupTab _stateContextTracking;
        private PaletteRibbonGroupAreaTab _stateContextCheckedNormal;
        private PaletteRibbonJustTab _stateContextCheckedTracking;
        private PaletteRibbonFocus _overrideFocus;
        private PaletteBackInheritRedirect _backPanelInherit;

        // Properties
        private bool _minimizedMode;
        private bool _qatUserChange;
        private bool _allowButtonSpecToolTips;
        private bool _allowFormIntegrate;
        private bool _allowMinimizedChange;
        private bool _showMinimizeButton;
        private string _selectedContext;
        private Size _hideRibbonSize;
        private QATLocation _qatLocation;
        private ButtonStyle _groupButtonStyle;
        private ButtonStyle _groupClusterButtonStyle;
        private ButtonStyle _groupDialogButtonStyle;
        private ButtonStyle _groupCollapsedButtonStyle;
        private ButtonStyle _qatButtonStyle;
        private ButtonStyle _scrollerStyle;
        private PaletteBackStyle _backStyle;
        private PaletteBackStyle _backInactiveStyle;
        private KryptonRibbonTab _minSelectedTab;
        private KryptonRibbonTab _selectedTab;
        private KryptonRibbonTabCollection _ribbonTabs;
        private KryptonRibbonContextCollection _ribbonContexts;
        private KryptonRibbonQATButtonCollection _qatButtons;
        private RibbonShortcuts _ribbonShortcuts;
        private RibbonStrings _ribbonStrings;
        private RibbonAppButton _ribbonAppButton;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the selected tab changes.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the selected tab changes.")]
        public event EventHandler SelectedTabChanged;

        /// <summary>
        /// Occurs when the selected context changes.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the selected context changes.")]
        public event EventHandler SelectedContextChanged;

        /// <summary>
        /// Occurs when the application button menu is opening.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when application button menu is opening but not displayed as yet.")]
        public event CancelEventHandler AppButtonMenuOpening;

        /// <summary>
        /// Occurs when the application button menu is opended.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the application button menu is fully opened for display.")]
        public event EventHandler AppButtonMenuOpened;

        /// <summary>
        /// Occurs when the application button menu is about to close.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the application button menu is about to close.")]
        public event CancelEventHandler AppButtonMenuClosing;

        /// <summary>
        /// Occurs when the application button menu has been closed.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the application button menu has been closed.")]
        public event ToolStripDropDownClosedEventHandler AppButtonMenuClosed;

        /// <summary>
        /// Occurs when the ribbon context menu is about to be shown.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the ribbon context menu is about to be shown.")]
        public event EventHandler<ContextMenuArgs> ShowRibbonContextMenu;

        /// <summary>
        /// Occurs when the quick access toolbar customize menu is about to be shown.
        /// </summary>
        [Category("Ribbon")]
        [Description("Occurs when the quick access toolbar customize menu is about to be shown.")]
        public event EventHandler<ContextMenuArgs> ShowQATCustomizeMenu;

        /// <summary>
        /// Occurs when the MinimizedMode property has changed value.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs when the MinimizedMode property has changed value.")]
        public event EventHandler MinimizedModeChanged;

        /// <summary>
        /// Occurs add design time when the user requests a tab be added.
        /// </summary>
        [Category("Design Time Only")]
        [Description("Occurs add design time when the user requests a tab be added.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public event EventHandler DesignTimeAddTab;
        #endregion

        #region Identity
        static KryptonRibbon()
        {
            // Cache access to the internal 'Select' method of the ContainerControl
            _containerSelect = typeof(ContainerControl).GetMethod("Select",
                                                                  BindingFlags.Instance |
                                                                  BindingFlags.NonPublic);
        }

        /// <summary>
        /// Initialize a new instance of the KryptonRibbon class.
        /// </summary>
        public KryptonRibbon()
        {
            // Ribbon cannot take the focus
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.ResizeRedraw, false);

            CreateRibbonCollections();
            CreateButtonSpecs();
            CreateStorageObjects();
            CreateViewManager();
            CreateInternal();
            AssignDefaultFields();

            // Snoop windows messages to handle command keys such as CTRL+F1 to 
            // toggle minimized mode and also when to exit keyboard access mode
            Application.AddMessageFilter(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remember to unhook otherwise memory cannot be garbage collected
                Application.RemoveMessageFilter(this);

                // Prevent the removing of child controls from causing a 
                // layout that then causes the children to be added again!
                SuspendLayout();

                // Caption area must have chance to unintegrated from the custom chrome
                _captionArea.Dispose();

                // Must get the tabs to dispose as it holds event hooks to other windows/controls
                _tabsArea.Dispose();

                // Dispose of the cached krypton context menu
                if (_kcm != null)
                {
                    _kcm.Close();
                    _kcm.Dispose();
                    _kcm = null;
                }

                // Dispose of per-tab resources
                foreach (KryptonRibbonTab tab in RibbonTabs)
                    tab.Dispose();

                ResumeLayout();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public Hidden Properties
        /// <summary>
        /// Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }

        /// <summary>
        /// Gets and sets the automatic resize of the control to fit contents.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            
            set 
            { 
                // Can only ever set the auto size to true
                if (value)
                    base.AutoSize = value; 
            }
        }

        /// <summary>
        /// Gets and sets the auto size mode.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override AutoSizeMode AutoSizeMode
        {
            get { return base.AutoSizeMode; }

            set 
            { 
                // Can only ever set to grow and shrink automatically
                if (value == AutoSizeMode.GrowAndShrink)
                    base.AutoSizeMode = value; 
            }
        }

        /// <summary>
        /// Gets and sets the context menu associated with the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ContextMenuStrip ContextMenuStrip
        {
            get { return base.ContextMenuStrip; }
            set { base.ContextMenuStrip = value; }
        }

        /// <summary>
        /// Gets and sets which control border to dock control against.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DockStyle Dock
        {
            get { return base.Dock; }

            set
            {
                // Can only ever set the property to dock at the top
                if (value == DockStyle.Top)
                    base.Dock = value;
            }
        }

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Point Location
        {
            get { return base.Location; }
            set { base.Location = value; }
        }

        /// <summary>
        /// Gets or sets the space between controls.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Margin
        {
            get { return base.Margin; }
            set { base.Margin = value; }
        }

        /// <summary>
        /// Gets or sets the size that is the upper limit that GetPreferredSize can specify.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MaximumSize
        {
            get { return base.MaximumSize; }
            set { base.MaximumSize = value; }
        }

        /// <summary>
        /// Gets or sets the size that is the lower limit that GetPreferredSize can specify.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set { base.MinimumSize = value; }
        }

        /// <summary>
        /// Gets or sets padding within the control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        #endregion

        #region Public Exposed Properties
        /// <summary>
        /// Gets or sets if the ribbon is allowed to override form chrome by integrating instead with operating system chrome.
        /// </summary>
        [Category("Visuals")]
        [Description("Is ribbon is allowed to override form chrome by integrating instead with operating system chrome.")]
        [DefaultValue(true)]
        public bool AllowFormIntegrate
        {
            get { return _allowFormIntegrate; }
            set
            {
                if (_allowFormIntegrate != value)
                {
                    _allowFormIntegrate = value;
                    CaptionArea.PerformFormChromeCheck();
                }
            }
        }

        /// <summary>
        /// Gets or sets if the user is allowed to change the minimized mode.
        /// </summary>
        [Category("Visuals")]
        [Description("Determines if the user is allowed to change the minimized mode.")]
        [DefaultValue(true)]
        public bool AllowMinimizedChange
        {
            get { return _allowMinimizedChange; }
            set { _allowMinimizedChange = value; }
        }

        /// <summary>
        /// Gets and sets a value indicating if tooltips should be displayed for button specs.
        /// </summary>
        [Category("Visuals")]
        [Description("Should tooltips be displayed for button specs.")]
        [DefaultValue(false)]
        public bool AllowButtonSpecToolTips
        {
            get { return _allowButtonSpecToolTips; }
            set { _allowButtonSpecToolTips = value; }
        }

        /// <summary>
        /// Gets access to the common ribbon appearance that other states can override.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }

        /// <summary>
        /// Gets access to the disabled ribbon appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining disabled ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDisabled StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }

        /// <summary>
        /// Gets access to the normal ribbon appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining normal ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonNormal StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the tracking ribbon appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining tracking ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonAppGroupTab StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the pressed ribbon appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining pressed ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonAppButton StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }

        /// <summary>
        /// Gets access to the tracking checked normal appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining checked normal ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonGroupAreaTab StateCheckedNormal
        {
            get { return _stateCheckedNormal; }
        }

        private bool ShouldSerializeStateCheckedNormal()
        {
            return !_stateCheckedNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the tracking checked tracking appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining checked tracking ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonJustTab StateCheckedTracking
        {
            get { return _stateCheckedTracking; }
        }

        private bool ShouldSerializeStateCheckedTracking()
        {
            return !_stateCheckedTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the context normal appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining context normal ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonJustGroup StateContextNormal
        {
            get { return _stateContextNormal; }
        }

        private bool ShouldSerializeStateContextNormal()
        {
            return !_stateContextNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the context tracking appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining context tracking ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonGroupTab StateContextTracking
        {
            get { return _stateContextTracking; }
        }

        private bool ShouldSerializeStateContextTracking()
        {
            return !_stateContextTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the checked context normal appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining checked context normal ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonGroupAreaTab StateContextCheckedNormal
        {
            get { return _stateContextCheckedNormal; }
        }

        private bool ShouldSerializeStateContextCheckedNormal()
        {
            return !_stateContextCheckedNormal.IsDefault;
        }

        /// <summary>
        /// Gets access to the checked context tracking appearance.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining checked context tracking ribbon appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonJustTab StateContextCheckedTracking
        {
            get { return _stateContextCheckedTracking; }
        }

        private bool ShouldSerializeStateContextCheckedTracking()
        {
            return !_stateContextCheckedTracking.IsDefault;
        }

        /// <summary>
        /// Gets access to the ribbon appearance when it has focus.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining ribbon appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonFocus OverrideFocus
        {
            get { return _overrideFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_overrideFocus.IsDefault;
        }

        /// <summary>
        /// Gets the collection of button specifications.
        /// </summary>
        [Category("Values")]
        [Description("Collection of button specifications.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RibbonButtonSpecAnyCollection ButtonSpecs
        {
            get { return _buttonSpecs; }
        }

        /// <summary>
        /// Gets the collection of ribbon tabs.
        /// </summary>
        [Category("Values")]
        [Description("Collection of ribbon tabs.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonRibbonTabCollection RibbonTabs
        {
            get { return _ribbonTabs; }
        }

        /// <summary>
        /// Gets and sets the currently selected tab.
        /// </summary>
        [Category("Values")]
        [Description("Currently selected ribbon tab.")]
        public KryptonRibbonTab SelectedTab
        {
            get { return _selectedTab; }

            set
            {
                if ((value != null) &&                          // Cannot remove selection
                    ((value.Visible) || InDesignHelperMode) &&  // Tab must be visible
                    RibbonTabs.Contains(value) &&               // Tab must be in our collection
                    TabIsContextValid(value))                   // Context tab must be in current context selection
                {
                    if (_selectedTab != value)
                    {
                        if (!IsInitializing || !RealMinimizedMode)
                        {
                            _selectedTab = value;
                            OnSelectedTabChanged(EventArgs.Empty);
                            PerformNeedPaint(true);
                        }

                        if (!IsInitializing && RealMinimizedMode)
                        {
                            // If we still have a selected tab after the change event has been processed
                            if (_selectedTab != null)
                            {
                                // Remember this tab as the last one selected for use when leaving minimized mode
                                _minSelectedTab = value;

                                // First time around we show the popup, next time we update
                                if (_minimizedPopup == null)
                                    ShowMinimizedPopup();
                                else
                                    UpdateMinimizedPopup();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reset the SelectedTab to the default value.
        /// </summary>
        public void ResetSelectedTab()
        {
            // Update selection to match ribbon settings
            ValidateSelectedTab();
        }

        /// <summary>
        /// Gets and sets the common separated list of selected context names.
        /// </summary>
        [Category("Values")]
        [Description("Common separated list of selected context names.")]
        [DefaultValue("")]
        public string SelectedContext
        {
            get { return _selectedContext; }

            set
            {
                // Always maintain a value reference
                if (value == null)
                    value = string.Empty;

                if (_selectedContext != value)
                {
                    // Note the change in selected context
                    _selectedContext = value;
                    PerformNeedPaint(true);
                    OnSelectedContextChanged(EventArgs.Empty);

                    // Update selection to match ribbon settings
                    ValidateSelectedTab();
                }
            }
        }

        private bool ShouldSerializeSelectedContext()
        {
            return !string.IsNullOrEmpty(_selectedContext);
        }

        /// <summary>
        /// Reset the SelectedContext to the default value.
        /// </summary>
        private void ResetSelectedContext()
        {
            SelectedContext = string.Empty;
        }

        /// <summary>
        /// Gets the collection of ribbon context definitions.
        /// </summary>
        [Category("Values")]
        [Description("Collection of ribbon context definitions.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonRibbonContextCollection RibbonContexts
        {
            get { return _ribbonContexts; }
        }

        /// <summary>
        /// Gets the collection of ribbon quick access toolbar buttons.
        /// </summary>
        [Category("Values")]
        [Description("Collection of ribbon quick access toolbar buttons.")]
        [MergableProperty(false)]
        [Editor("ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButtonCollectionEditor, ComponentFactory.Krypton.Ribbon, Version=4.6.0.0, Culture=neutral, PublicKeyToken=a87e673e9ecb6e8e", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonRibbonQATButtonCollection QATButtons
        {
            get { return _qatButtons; }
        }

        /// <summary>
        /// Gets the set of ribbon shortcuts.
        /// </summary>
        [Category("Values")]
        [Description("Collection of ribbon shortcuts.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RibbonShortcuts RibbonShortcuts
        {
            get { return _ribbonShortcuts; }
        }

        /// <summary>
        /// Gets the set of ribbon display strings.
        /// </summary>
        [Category("Values")]
        [Description("Collection of ribbon strings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public RibbonStrings RibbonStrings
        {
            get { return _ribbonStrings; }
        }

        /// <summary>
        /// Gets the set of ribbon application button display strings.
        /// </summary>
        [Category("Values")]
        [Description("Collection of ribbon app button settings.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public RibbonAppButton RibbonAppButton
        {
            get { return _ribbonAppButton; }
        }

        /// <summary>
        /// Gets the styles for various ribbon elements.
        /// </summary>
        [Category("Visuals")]
        [Description("Styles for various ribbon elements.")]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonStyles RibbonStyles
        {
            get { return _ribbonStyles; }
        }

        private bool ShouldSerializeRibbonStyles()
        {
            return !_ribbonStyles.IsDefault;
        }

        /// <summary>
        /// Gets and sets the vertical and horizontal minimum sizes at which the ribbon hides itself. 
        /// </summary>
        [Category("Values")]
        [Description("Vertical and horizontal minimum sizes at which the ribbon hides itself.")]
        [DefaultValue(typeof(Size), "300,250")]
        public Size HideRibbonSize
        {
            get { return _hideRibbonSize; }

            set
            {
                if (_hideRibbonSize != value)
                {
                    _hideRibbonSize = value;
                    _tabsArea.CheckRibbonSize();
                }
            }
        }

        /// <summary>
        /// Reset the HideRibbonSize to the default value.
        /// </summary>
        private void ResetHideRibbonSize()
        {
            HideRibbonSize = new Size(300, 250);
        }

        /// <summary>
        /// Gets and sets a value indicating if the ribbon is in minimized mode.
        /// </summary>
        [Localizable(true)]
        [Category("Values")]
        [Description("Is the ribbon in minimized mode.")]
        [DefaultValue(false)]
        public bool MinimizedMode
        {
            get { return _minimizedMode; }

            set
            {
                if (_minimizedMode != value)
                {
                    _minimizedMode = value;

                    // Only show the minimize bar if in minimized mode and not showing the QAT below the ribbon
                    _minimizeBar.Visible = (RealMinimizedMode && (QATLocation != QATLocation.Below));

                    if (RealMinimizedMode)
                    {
                        // Remove groups view from main display and place inside minimized panel
                        _ribbonDocker.Remove(_groupsArea);

                        // Put the groups view back into the main display
                        if (!_drawMinimizedPanel.Contains(_groupsArea))
                            _drawMinimizedPanel.Add(_groupsArea);

                        // Need to move any child controls out of the client area so they are not shown,
                        // this can have when the QAT is below the ribbon and then the child controls that
                        // contain the per tab details will still be visible in that area.
                        foreach (Control child in Controls)
                            child.SetBounds(-child.Width, -child.Height, child.Width, child.Height);

                        // Update selection to match ribbon settings
                        _minSelectedTab = _selectedTab;
                        ValidateSelectedTab();

                        // Must layout to effect changes
                        _tabsArea.RecreateButtons();
                        PerformNeedPaint(true);
                    }
                    else
                    {
                        using (ScreenObscurer obscurer = new ScreenObscurer(_minimizedPopup, DesignMode))
                        {
                            // Remove any showing popup for the minimized area
                            KillMinimizedPopup();

                            // Remove groups from minimized panel
                            _drawMinimizedPanel.Remove(_groupsArea);

                            // Put the groups view back into the main display
                            if (!_ribbonDocker.Contains(_groupsArea))
                            {
                                _ribbonDocker.Insert(0, _groupsArea);
                                _ribbonDocker.SetDock(_groupsArea, ViewDockStyle.Fill);
                            }

                            // If there is a remembered selected tab then try and reapply it as the new selected tab
                            if (_minSelectedTab != null)
                            {
                                // Check the remembered entry is actually valid
                                if (_minSelectedTab.Visible &&
                                    RibbonTabs.Contains(_minSelectedTab) &&
                                    TabIsContextValid(_minSelectedTab))
                                {
                                    SelectedTab = _minSelectedTab;
                                }

                                _minSelectedTab = null;
                            }

                            // Ensure that selected tab is valid
                            ValidateSelectedTab();

                            // Must layout to effect changes
                            _tabsArea.RecreateButtons();
                            PerformNeedPaint(true);

                            // Allow the ribbon to be layed out and painted before we 
                            // remove the control obscurer the reduces flicker when switching.
                            Application.DoEvents();
                        }
                    }

                    // Raises change event
                    OnMinimizedModeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the MinimizedMode property to its default value.
        /// </summary>
        public void ResetMinimizedMode()
        {
            MinimizedMode = false;
        }

        /// <summary>
        /// Gets and sets the display method for the quick access toolbar.
        /// </summary>
        [Localizable(true)]
        [Category("Values")]
        [Description("Determines how the quick access toolbar is displayed.")]
        [DefaultValue(typeof(QATLocation), "Above")]
        public QATLocation QATLocation
        {
            get { return _qatLocation; }

            set
            {
                if (_qatLocation != value)
                {
                    _qatLocation = value;

                    using (ScreenObscurer obscurer = new ScreenObscurer(this, DesignMode))
                    {
                        // Only show the minimize bar if in minimized mode 
                        // and not showing the QAT below the ribbon
                        _minimizeBar.Visible = (RealMinimizedMode && (QATLocation != QATLocation.Below));

                        // Update the fullbar version of the QAT
                        _qatBelowRibbon.Visible = (_qatLocation == QATLocation.Below);

                        // Update the minibar versions of the QAT
                        _captionArea.UpdateQAT();

                        // Must layout to effect changes
                        PerformLayout();
                        Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Resets the QATLocation property to its default value.
        /// </summary>
        public void ResetQATLocation()
        {
            QATLocation = QATLocation.Above;
        }

        /// <summary>
        /// Gets and sets a value indicating if user is allowed to change the QAT entries.
        /// </summary>
        [Category("Values")]
        [Description("Is the user allowed to change the quick access toolbar entries.")]
        [DefaultValue(true)]
        public bool QATUserChange
        {
            get { return _qatUserChange; }
            set { _qatUserChange = value; }
        }

        /// <summary>
        /// Resets the QATUserChange property to its default value.
        /// </summary>
        public void ResetQATUserChange()
        {
            QATUserChange = true;
        }

        /// <summary>
        /// Gets and sets a value indicating if a minimize/expand button appears on the ribbon tab ara.
        /// </summary>
        [Category("Values")]
        [Description("Does a minimize/expand button appear on the ribbon tab ara.")]
        [DefaultValue(true)]
        public bool ShowMinimizeButton
        {
            get { return _showMinimizeButton; }
            
            set 
            {
                if (_showMinimizeButton != value)
                {
                    _showMinimizeButton = value;
                    _tabsArea.RecreateButtons();
                    PerformNeedPaint(true);
                }
            }
        }

        /// <summary>
        /// Resets the ShowMinimizeButton property to its default value.
        /// </summary>
        public void ResetShowMinimizeButton()
        {
            ShowMinimizeButton = true;
        }

        /// <summary>
        /// Gets access to the ToolTipManager used for displaying tool tips.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ToolTipManager ToolTipManager
        {
            get { return _tabsArea.ToolTipManager; }
        }

        /// <summary>
        /// Gets the collection of controls contained within the control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ControlCollection Controls
        {
            get { return base.Controls; }
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool InDesignMode
        {
            get { return DesignMode; }
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool InDesignHelperMode
        {
            get { return InDesignMode && _designHelpers; }

            set
            {
                // Change mode
                _designHelpers = value;

                // Change might cause the selected tab to no longer be allowed
                ValidateSelectedTab();

                // Must layout to effect change
                PerformNeedPaint(true);
            }
        }

        /// <summary>
        /// Gets a value indicating if currently in keyboard mode.
        /// </summary>
        [Browsable(false)]
        public bool KeyboardMode
        {
            get { return _keyboardMode; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Toggles into and out off keyboard mode.
        /// </summary>
        public void ToggleKeyboardMode()
        {
            // Invert keyboard mode
            _keyboardMode = !_keyboardMode;

            if (_keyboardMode)
            {
                // Remember number of ALT key ups at time of entering keyboard mode
                _keyboardAltUpCount = _altUpCount;

                // Find current focus location
                _keyboardFocusWindow = PI.GetFocus();

                // Hide caret from appearing in the source window
                if (_keyboardFocusWindow != IntPtr.Zero)
                    _keyboardFocusCaret = PI.HideCaret(_keyboardFocusWindow);

                // Take the focus for ourself
                PI.SetFocus(this.Handle);

                // We are entering key tips mode
                _keyboardKeyTips = true;

                // Create the control for showing the initial set of key tips for the root items
                SetKeyTips(GenerateKeyTipsAtTopLevel(), KeyTipMode.Root);
            }
            else
            {
                // Remove keytips from display
                KillKeyboardKeyTips();

                if (!IgnoreRestoreFocus)
                {
                    if (_keyboardFocusWindow != IntPtr.Zero)
                    {
                        // Put focus back to original source
                        PI.SetFocus(_keyboardFocusWindow);

                        // If the caret was showing when we stole focus, then put it back
                        if (_keyboardFocusCaret)
                            PI.ShowCaret(_keyboardFocusWindow);
                    }
                }

                // Remove focus from current view
                FocusView = null;

                IgnoreRestoreFocus = false;
            }
        }

        /// <summary>
        /// Filters out a message before it is dispatched.
        /// </summary>
        /// <param name="m">The message to be dispatched. You cannot modify this message.</param>
        /// <returns>true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool PreFilterMessage(ref Message m)
        {
            // Prevent interception of messages during design time or after we have died
            if (!IsDisposed && !DesignMode)
            {
                switch (m.Msg)
                {
                    case PI.WM_LBUTTONDOWN:
                    case PI.WM_MBUTTONDOWN:
                    case PI.WM_RBUTTONDOWN:
                    case PI.WM_NCLBUTTONDOWN:
                    case PI.WM_NCMBUTTONDOWN:
                    case PI.WM_NCRBUTTONDOWN:
                        // Pressing the mouse in keyboard mode always kills keyboard mode
                        KillKeyboardMode();
                        break;
                    case PI.WM_SYSKEYUP:
                        CheckForAltUp();
                        break;
                    case PI.WM_KEYDOWN:
                    case PI.WM_SYSKEYDOWN:
                        // Only interested if we are usable
                        if (Visible && Enabled)
                        {
                            // Only interested if the owning form is usable and has the focus
                            Form ownerForm = FindForm();
                            if ((ownerForm != null) && ownerForm.Visible && ownerForm.Enabled && ownerForm.ContainsFocus)
                            {
                                // Extract the keys being pressed
                                Keys keys = ((Keys)((int)m.WParam.ToInt64()));

                                // If the user standard combination ALT + F4
                                if ((keys == Keys.F4) && ((Control.ModifierKeys & Keys.Alt) == Keys.Alt))
                                {
                                    // Attempt to close the form
                                    ownerForm.Close();
                                    return true;
                                }
                                else
                                {
                                    // Give ourself a chance to process any shortcuts
                                    return ProcessCmdKey(ref m, keys | Control.ModifierKeys);
                                }
                            }
                        }
                        break;
                    case PI.WM_MOUSEWHEEL:
                        // Only interested if we are usable and not in minimized mode or keyboard mode
                        if (Visible && Enabled && !RealMinimizedMode && !_keyboardMode && !InDesignMode)
                        {
                            // Only interested is the owning form is usable and has the focus
                            Form ownerForm = FindForm();
                            if ((ownerForm != null) && ownerForm.Visible && ownerForm.Enabled && ownerForm.ContainsFocus)
                            {
                                // Extract the x and y mouse position from message
                                Point pt = new Point();
                                pt.X = PI.LOWORD((int)m.LParam);
                                pt.Y = PI.HIWORD((int)m.LParam);

                                // Only interested if over the ribbon control
                                if (ClientRectangle.Contains(PointToClient(pt)))
                                {
                                    short delta = (short)PI.HIWORD((int)m.WParam.ToInt64());
                                    _tabsArea.LayoutTabs.ProcessMouseWheel(delta < 0);
                                    return true;
                                }
                            }
                        }
                        break;
                }
            }
            return false;
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool DesignerGetHitTest(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return false;

            // Get the view the mouse is currently over
            ViewBase mouseView = ViewManager.Root.ViewFromPoint(pt);

            // Do we match of the views we always allow?
            bool matchView = (mouseView != null) && (mouseView.Parent != null) &&
                             ((mouseView is ViewDrawRibbonScrollButton) ||
                              (mouseView is ViewDrawRibbonDesignBase) ||
                              (mouseView.Parent is ViewDrawRibbonDesignBase));

            // If the mouse is over a scroll button or a component then take the mouse
            return matchView || (DesignerComponentFromPoint(pt) != null);
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public void DesignerMouseLeave()
        {
            // Simulate the mouse leaving the control so that the tracking
            // element that thinks it has the focus is informed it does not
            OnMouseLeave(EventArgs.Empty);
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        /// <param name="view">ViewBase reference.</param>
        /// <returns>Mouse point.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public Point ViewRectangleToPoint(ViewBase view)
        {
            Rectangle screenRect = view.OwningControl.RectangleToScreen(view.ClientRectangle);
            return new Point(screenRect.Left, screenRect.Bottom);
        }

        /// <summary>
        /// Internal design time method.
        /// </summary>
        /// <param name="pt">Mouse location.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public Component DesignerComponentFromPoint(Point pt)
        {
            // Ignore call as view builder is already destructed
            if (IsDisposed)
                return null;

            // Get the view the mouse is currently over
            ViewBase mouseView = ViewManager.Root.ViewFromPoint(pt);

            if (mouseView is ViewDrawRibbonGroupDateTimePicker)
                return ((ViewDrawRibbonGroupDateTimePicker)mouseView).Component;
            else
                return ViewManager.ComponentFromPoint(pt);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the Initialized event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            // If no selected tab is designated at startup then we choose the first visible tab
            if (SelectedTab == null)
                ResetSelectedTab();

            // Let base class generate event
            base.OnInitialized(e);
        }

        /// <summary>
        /// Raises the HandleCreated event. 
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Size and position of the application button and context titles will not
            // be correct in the caption area until the control handle has been created
            PerformNeedPaint(true);
        }

        /// <summary>
        /// Creates a new instance of the control collection for the control.
        /// </summary>
        /// <returns>A new instance of KryptonNavigatorControlCollection assigned to the control.</returns>
        protected override ControlCollection CreateControlsInstance()
        {
            // Create a navigator specific control collection
            return new KryptonReadOnlyControls(this);
        }

		/// <summary>
		/// Raises the Layout event.
		/// </summary>
        /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed && !Disposing)
            {
                if (TabsArea != null)
                    TabsArea.AppButtonVisibleChanged();

                if (CaptionArea != null)
                    CaptionArea.AppButtonVisibleChanged();
            }

            // If we have a parent form then ask it to check for a change in composition height
            if (_captionArea.KryptonForm != null)
                _captionArea.KryptonForm.RecalculateComposition();

            base.OnLayout(levent);
        }

        /// <summary>
        /// Process Windows-based messages.
        /// </summary>
        /// <param name="m">A Windows-based message.</param>
        protected override void WndProc(ref Message m)
        {
            // Only interested in intercepting the hit testing
            if (m.Msg == PI.WM_NCHITTEST)
            {
                // Extract the screen point for the hit test
                Point screenPoint = new Point((int)m.LParam.ToInt64());

                // Convert to a client point
                Point clientPoint = PointToClient(screenPoint);

                if (_compositionArea.Visible)
                {
                    if (_compositionArea.ClientRectangle.Contains(clientPoint))
                    {
                        // By returning transparent the hit test gets sent the window behind it which 
                        // will be the form itself, and so the existing form level hit testing will 
                        // occur that handles the min/max/close app button etc...
                        m.Result = (IntPtr)PI.HTTRANSPARENT;
                        return;
                    }
                }

                if ((_tabsArea != null) && (_tabsArea.LayoutTabs != null) && (_tabsArea.LayoutTabs.GetViewForSpare != null))
                {
                    // Conver the spare tabs area from child control coordinates to ribbon control coordinates
                    Rectangle spareRect =_tabsArea.LayoutTabs.GetViewForSpare.ClientRectangle;
                    spareRect.Offset(_tabsArea.TabsContainerControl.ChildControl.Location);
                    
                    // If the point is over the spare area of the tabs then treat that area transparent so 
                    // that the form processing can then treat it as a caption area of the actual owning form
                    if (spareRect.Contains(clientPoint))
                    {
                        m.Result = (IntPtr)PI.HTTRANSPARENT;
                        return;
                    }
                }

            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Processes a dialog key.
        /// </summary>
        /// <param name="keyData">One of the Keys values that represents the key to process.</param>
        /// <returns>True is handled; otherwise false.</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // When in keyboard mode...
            if (_focusView != null)
            {
                // We pass movements keys onto the view
                switch (keyData)
                {
                    case Keys.Tab | Keys.Shift:
                    case Keys.Tab:
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Space:
                    case Keys.Enter:
                        // Any navigation keys remove the keytips
                        KillKeyboardKeyTips();

                        _focusView.KeyDown(new KeyEventArgs(keyData));
                        return true;
                }
            }
            else
            {
                // Not in keyboard mode but user pressing the tab key
                switch (keyData)
                {
                    case Keys.Tab | Keys.Shift:
                    case Keys.Tab:
                        // If one of our child controls has the focus
                        if (ContainsFocus)
                        {
                            // Move focus forward/background until out of the ribbon
                            SelectNonRibbonControl(keyData == Keys.Tab);
                            return true;
                        }
                        break;
                }
            }

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
            CheckForAltUp();
            CheckForAltDown();

            // If user presses the escape key and we are showing a context menu strip
            // then we do not want to process the key as the context menu strip handling
            // should use that key to dismiss the context menu itself.
            if (keyData == Keys.Escape)
            {
                if (VisualPopupManager.Singleton.IsShowingCMS)
                    return false;
            }

            // If the user presses escape when in keyboard mode
            if (_keyboardMode && (keyData == Keys.Escape))
            {
                // If showing a popup menu then let the escape key be processed
                // by the VisualPopupManager and so used to dismiss the popup
                if (VisualPopupManager.Singleton.IsTracking)
                    return false;

                // If showing key tips...
                if (_keyboardKeyTips && (KeyTipMode != KeyTipMode.Root))
                {
                    // Revert back top key tips for root items
                    SetKeyTips(GenerateKeyTipsAtTopLevel(), KeyTipMode.Root);
                }
                else
                {
                    // Turn off keyboard mode altogether
                    ToggleKeyboardMode();
                }

                return true;
            }

            // Check for toggling keyboard access to the ribbon
            if ((RibbonShortcuts.ToggleKeyboardAccess1 == keyData) ||
                (RibbonShortcuts.ToggleKeyboardAccess2 == keyData))
            {
                // Cannot begin/end tooltips when showing the app menu
                if (!VisualPopupManager.Singleton.IsTracking)
                {
                    if (InKeyboardMode)
                    {
                        // Only when the ALT key has been released do we exit keyboard mode
                        if (_keyboardAltUpCount != _altUpCount)
                            ToggleKeyboardMode();
                    }
                    else
                        ToggleKeyboardMode();
        
                    return true;
                }
            }

            // Check for toggling minimized mode key combination (default = Ctrl+F1)
            if ((RibbonShortcuts.ToggleMinimizeMode == keyData) && AllowMinimizedChange)
            {
                MinimizedMode = !MinimizedMode;
                return true;
            }

            // Check if a shortcut is triggered on the application button context menu
            if (RibbonAppButton.AppButtonMenuItems.ProcessShortcut(keyData))
            {
                ActionOccured();
                return true;
            }

            // Remove any ALT key from the key tips processing
            Keys processData = keyData & ~Keys.Alt;

            // Ask the tabs to check for command key processing
            foreach (KryptonRibbonTab tab in RibbonTabs)
                if (tab.Visible && tab.ProcessCmdKey(ref msg, processData))
                    return true;

            // Check each quick access toolbar button
            foreach (IQuickAccessToolbarButton qatButton in QATButtons)
            {
                // Only interested in visible and enabled buttons
                if (qatButton.GetVisible() && qatButton.GetEnabled())
                {
                    // Does the key combination match the shortcut?
                    Keys shortcut = qatButton.GetShortcutKeys();
                    if ((shortcut != Keys.None) && (shortcut == keyData))
                    {
                        // Click the button and finish processing
                        qatButton.PerformClick();
                        return true;
                    }
                }
            }

            // If we want to intercept key pressed for use with key tips
            if (InKeyboardMode && InKeyTipsMode)
            {
                char key = (char)processData;

                // We only want letters and digits and not control keys such as arrow left/right
                if (char.IsLetterOrDigit(key))
                {
                    _keyTipControlE.AppendKeyPress(key);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Processes a notification from palette storage of a button spec change.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnButtonSpecChanged(object sender, EventArgs e)
        {
            // Recreate all the button specs with new values
            _tabsArea.RecreateButtons();

            // Let base class perform standard processing
            base.OnButtonSpecChanged(sender, e);
        }

		/// <summary>
		/// Raises the MouseDown event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (ViewManager != null)
                    ViewManager.MouseDown(e, new Point(e.X, e.Y));
            }

            // Do not call base class! Prevent capture of the mouse
        }

        /// <summary>
        /// Raises the MouseUp event.
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // Do we have a manager for processing mouse messages?
                if (ViewManager != null)
                    ViewManager.MouseUp(e, new Point(e.X, e.Y));
            }

            // Do not call base class! Prevent context menu from appearing by default
        }

        /// <summary>
        /// Raises the GotFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            ViewBase newFocus = null;

            if (SelectedTab != null)
                newFocus = _tabsArea.LayoutTabs.GetViewForRibbonTab(SelectedTab);
            else if (!RealMinimizedMode)
                newFocus = _tabsArea.LayoutTabs.GetViewForFirstRibbonTab();
            
            // If no tab to select, then use the application button
            if (newFocus == null)
            {
                if (TabsArea.LayoutAppButton.Visible)
                    newFocus = TabsArea.LayoutAppButton.AppButton;
                else if (TabsArea.LayoutAppTab.Visible)
                    newFocus = TabsArea.LayoutAppTab.AppTab;
            }

            // Give focus to the target view
            FocusView = newFocus;

            // Change in focus means a change in appearance
            PerformNeedPaint(true);
            Refresh();
        }

        /// <summary>
        /// Raises the LostFocus event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            // By default losing the focus causes the keyboard mode to be ended
            if (LostFocusLosesKeyboard)
            {
                // Remove focus from current view
                FocusView = null;

                // Losing the focus in keyboard mode, always loses keyboard mode
                KillKeyboardMode();
                VisualPopupManager.Singleton.EndAllTracking();
            }
            else
            {
                // Reset the flag back to default state
                LostFocusLosesKeyboard = true;
            }

            // Change in focus means a change in appearance
            PerformNeedPaint(true);
            Refresh();
        }

        /// <summary>
        /// Raises the KeyPress event.
        /// </summary>
        /// <param name="e">An KeyPressEventArgs that contains the event data.</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // If we want to intercept key pressed for use with key tips
            if (_keyboardMode && _keyboardKeyTips)
                _keyTipControlE.AppendKeyPress(char.ToUpper(e.KeyChar));
            else
                base.OnKeyPress(e);
        }

        /// <summary>
        /// Raises the Resize event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            // If the application is not themed (Windows Classic) then already repaint
            if (_invalidateOnResize || !IsAppThemed)
                Invalidate();

            base.OnResize(e);
        }

        /// <summary>
        /// Raises the PaintBackground event.
        /// </summary>
        /// <param name="pevent">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // If we are using the composition area
            if (_compositionArea.Visible)
            {
                // Cause an empty bitmap to be created and blited to the background,
                // strange though it might sound this creates an empty area and gets
                // rid of any drawing artifacts in that area.
                AccurateText.DrawCompositionGlowingText(pevent.Graphics, string.Empty, Font, 
                                                        ClientRectangle, PaletteState.Normal, 
                                                        Color.Transparent, false);

                // Exclude the composition area from the rest of the background painting
                pevent.Graphics.SetClip(new Rectangle(_compositionArea.ClientLocation, new Size(_compositionArea.ClientWidth, _compositionArea.CompHeight)), CombineMode.Exclude);
            }

            base.OnPaintBackground(pevent);
        }

        /// <summary>
        /// Perform background painting with the provided default values.
        /// </summary>
        /// <param name="g">Graphics reference for drawing.</param>
        /// <param name="backBrush">Brush to use when painting.</param>
        /// <param name="backRect">Client area to paint.</param>
        protected override void PaintBackground(Graphics g, Brush backBrush, Rectangle backRect)
        {
            // If we are using the composition area
            if (_compositionArea.Visible)
            {
                // Copy the current clipping
                Region oldClip = g.Clip.Clone();

                // Exclude the composition area from the background painting
                g.SetClip(new Rectangle(_compositionArea.ClientLocation, new Size(_compositionArea.ClientWidth, _compositionArea.CompHeight)), CombineMode.Exclude);

                // Fill background
                g.FillRectangle(backBrush, backRect);

                // Dispose of the combined clip and use the old one again
                g.Clip.Dispose();
                g.Clip = oldClip;
            }
            else
                g.FillRectangle(backBrush, backRect);
        }

        /// <summary>
        /// Processes a notification from palette storage of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected override void OnNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // When in minimized mode...
            if (RealMinimizedMode)
            {
                // And we have a popup showing...
                if (_minimizedPopup != null)
                {
                    // Then ensure the popup gets painted as well
                    _minimizedPopup.PerformNeedPaint(e.NeedLayout);
                }
            }

            // Let base class perform usual painting
            base.OnNeedPaint(sender, e);
        }

        /// <summary>
        /// Raises the Enabled event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            PerformNeedPaint(true);
            base.OnEnabledChanged(e);
        }
        
        /// <summary>
        /// Raises the SelectedTabChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnSelectedTabChanged(EventArgs e)
        {
            // Need to recalculate anything relying on the palette
            DirtyPaletteCounter++;

            if (SelectedTabChanged != null)
                SelectedTabChanged(this, e);
        }

        /// <summary>
        /// Raises the SelectedContextChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnSelectedContextChanged(EventArgs e)
        {
            if (SelectedContextChanged != null)
                SelectedContextChanged(this, e);
        }

        /// <summary>
        /// Raises the ShowRibbonContextMenu event.
        /// </summary>
        /// <param name="e">A ContextMenuArgs containing event data.</param>
        protected virtual void OnShowRibbonContextMenu(ContextMenuArgs e)
        {
            if (ShowRibbonContextMenu != null)
                ShowRibbonContextMenu(this, e);
        }

        /// <summary>
        /// Raises the ShowQATCustomizeMenu event.
        /// </summary>
        /// <param name="e">A ContextMenuArgs containing event data.</param>
        protected virtual void OnShowQATCustomizeMenu(ContextMenuArgs e)
        {
            if (ShowQATCustomizeMenu != null)
                ShowQATCustomizeMenu(this, e);
        }

        /// <summary>
        /// Raises the AppButtonMenuOpening event.
        /// </summary>
        /// <param name="e">A CancelEventArgs containing the event data.</param>
        internal protected virtual void OnAppButtonMenuOpening(CancelEventArgs e)
        {
            if (AppButtonMenuOpening != null)
                AppButtonMenuOpening(this, e);
        }

        /// <summary>
        /// Raises the AppButtonMenuOpened event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        internal protected virtual void OnAppButtonMenuOpened(EventArgs e)
        {
            if (AppButtonMenuOpened != null)
                AppButtonMenuOpened(this, e);
        }

        /// <summary>
        /// Raises the AppButtonMenuClosing event.
        /// </summary>
        /// <param name="e">A CancelEventArgs containing the event data.</param>
        internal protected virtual void OnAppButtonMenuClosing(CancelEventArgs e)
        {
            if (AppButtonMenuClosing != null)
                AppButtonMenuClosing(this, e);
        }

        /// <summary>
        /// Raises the AppButtonMenuClosed event.
        /// </summary>
        /// <param name="e">An ToolStripDropDownClosedEventArgs containing the event data.</param>
        internal protected virtual void OnAppButtonMenuClosed(ToolStripDropDownClosedEventArgs e)
        {
            if (AppButtonMenuClosed != null)
                AppButtonMenuClosed(this, e);
        }

        /// <summary>
        /// Raises the MinimizedModeChanged event.
        /// </summary>
        /// <param name="e">An EventArgs containing event data.</param>
        protected virtual void OnMinimizedModeChanged(EventArgs e)
        {
            if (MinimizedModeChanged != null)
                MinimizedModeChanged(this, e);
        }
        #endregion

        #region Internal
        internal void OnDesignTimeAddTab()
        {
            if (DesignTimeAddTab != null)
                DesignTimeAddTab(this, EventArgs.Empty);
        }

        internal bool RealMinimizedMode
        {
            get { return MinimizedMode && !InDesignMode; }
        }

        internal ViewRibbonManager ViewRibbonManager
        {
            get { return ViewManager as ViewRibbonManager; }
        }

        internal ViewDrawRibbonPanel MainPanel
        {
            get { return _drawPanel; }
        }

        internal ViewLayoutRibbonTabsArea TabsArea
        {
            get { return _tabsArea; }
        }

        internal ViewLayoutRibbonGroupsArea GroupsArea
        {
            get { return _groupsArea; }
        }

        internal ViewDrawRibbonCaptionArea CaptionArea
        {
            get { return _captionArea; }
        }

        internal CalculatedValues CalculatedValues
        {
            get { return _calculatedValues; }
        }

        internal PaletteBackStyle BackStyle
        {
            get { return _backStyle; }
            
            set 
            { 
                _backStyle = value;
                UpdateBackStyle();
            }
        }

        internal PaletteBackStyle BackInactiveStyle
        {
            get { return _backInactiveStyle; }
            set 
            { 
                _backInactiveStyle = value;
                UpdateBackStyle();
            }
        }

        internal void UpdateBackStyle()
        {
            // Default to using the BackStyle property
            PaletteBackStyle backStyle = BackStyle;

            // Walk up the parnet chain looking for the owning form
            Form f = null;
            Control c = this;
            while (c.Parent != null)
            {
                c = c.Parent;
                if (c is Form)
                {
                    f = (Form)c;
                    break;
                }
            }

            if (f != null)
            {
                KryptonForm kryptonForm = f as KryptonForm;
                if (kryptonForm != null)
                {
                    if (!kryptonForm.WindowActive)
                        backStyle = BackInactiveStyle;
                }
                else if (!f.ContainsFocus)
                    backStyle = BackInactiveStyle;
            }

            if (_groupsArea.BackStyle != backStyle)
            {
                _backPanelInherit.Style = backStyle;
                _groupsArea.BackStyle = backStyle;
                _tabsArea.RecreateButtons();
                PerformNeedPaint(true);
            }
        }

        internal ButtonStyle ScrollerStyle
        {
            get { return _scrollerStyle; }

            set
            {
                if (_scrollerStyle != value)
                {
                    _scrollerStyle = value;
                    StateCommon.RibbonScroller.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        internal ButtonStyle GroupButtonStyle
        {
            get { return _groupButtonStyle; }

            set
            {
                if (_groupButtonStyle != value)
                {
                    _groupButtonStyle = value;
                    StateCommon.RibbonGroupButton.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        internal ButtonStyle GroupClusterButtonStyle
        {
            get { return _groupClusterButtonStyle; }

            set
            {
                if (_groupClusterButtonStyle != value)
                {
                    _groupClusterButtonStyle = value;
                    StateCommon.RibbonGroupClusterButton.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        internal ButtonStyle GroupCollapsedButtonStyle
        {
            get { return _groupCollapsedButtonStyle; }

            set
            {
                if (_groupCollapsedButtonStyle != value)
                {
                    _groupCollapsedButtonStyle = value;
                    StateCommon.RibbonGroupCollapsedButton.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        internal ButtonStyle GroupDialogButtonStyle
        {
            get { return _groupDialogButtonStyle; }

            set
            {
                if (_groupDialogButtonStyle != value)
                {
                    _groupDialogButtonStyle = value;
                    StateCommon.RibbonGroupDialogButton.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        internal Rectangle ClientRectangleWithoutComposition
        {
            get
            {
                // Start with actual client rect
                Rectangle rect = ClientRectangle;

                // If showing a composition area at top (represents area under the Aero caption bar chrome)
                if (_compositionArea.Visible)
                {
                    // Remove the composition area from client rect
                    rect.Y += _compositionArea.ClientHeight;
                    rect.Height -= _compositionArea.ClientHeight;
                }

                return rect;
            }
        }

        internal void TestForAppButtonDoubleClick()
        {
            TabsArea.TestForAppButtonDoubleClick();
        }

        internal void HideFocus(Control c)
        {
            // Keep going till be find a known parent control
            while (c != null)
            {
                // If inside a view control...
                if (c is ViewLayoutRibbonScrollPort.RibbonViewControl)
                {
                    // Use the hide control inside the view control
                    ((ViewLayoutRibbonScrollPort.RibbonViewControl)c).HideFocus();
                    break;
                }

                // If inside a popup group, because a collapsed group has been expanded
                if (c is VisualPopupGroup)
                {
                    // Use the hide control inside the view control
                    ((VisualPopupGroup)c).HideFocus();
                    break;
                }

                // Climb control tree
                c = c.Parent;
            }

            // Last resort, use the hidden one in the ribbon
            if (c == null)
                _hiddenFocusTarget.Focus();
        }

        internal KryptonForm FindKryptonForm()
        {
            Control c = this;
            while (c != null)
            {
                if (c is Form)
                    break;

                c = c.Parent;
            }

            // Caller is only interested in the KryptonForm parent
            if ((c != null) && (c is KryptonForm))
                return c as KryptonForm;
            else
                return null;
        }

        internal ButtonStyle QATButtonStyle
        {
            get { return _qatButtonStyle; }

            set
            {
                if (_qatButtonStyle != value)
                {
                    _qatButtonStyle = value;
                    StateCommon.RibbonQATButton.SetStyles(value);
                    PerformNeedPaint(true);
                }
            }
        }

        internal void DisplayQATCustomizeMenu(Rectangle screenRectangle,
                                              ViewLayoutRibbonQATContents contents,
                                              EventHandler finishDelegate)
        {
            // Ensure cached krypton context menu is ready for use
            ResetCachedKryptonContextMenu();

            // Add heading at top of the context menu
            KryptonContextMenuHeading heading = new KryptonContextMenuHeading();
            heading.Text = RibbonStrings.CustomizeQuickAccessToolbar;
            _kcm.Items.Add(heading);

            // Create a container for a set of individual menu items
            KryptonContextMenuItems menuItems = new KryptonContextMenuItems();
            _kcm.Items.Add(menuItems);

            // Is user allowed to change the QAT entries?
            if (QATUserChange)
            {
                // Add an entry for each quick access toolbar button
                foreach (IQuickAccessToolbarButton qatButton in QATButtons)
                {
                    KryptonContextMenuItem menuItem = new KryptonContextMenuItem();
                    menuItem.Text = qatButton.GetText();
                    menuItem.Checked = qatButton.GetVisible();
                    menuItem.Click += new EventHandler(OnQATCustomizeClick);
                    menuItem.Tag = QATButtons.IndexOf(qatButton);
                    menuItems.Items.Add(menuItem);
                }
            }

            // Do we need to allow the QAT location to be inverted?
            if (QATLocation != QATLocation.Hidden)
            {
                KryptonContextMenuItem showQAT = new KryptonContextMenuItem();
                if (QATLocation == QATLocation.Above)
                    showQAT.Text = RibbonStrings.ShowBelowRibbon;
                else
                    showQAT.Text = RibbonStrings.ShowAboveRibbon;
                showQAT.Click += new EventHandler(OnInvertQATLocation);

                // Add into the context menu
                if (menuItems.Items.Count > 0)
                    menuItems.Items.Add(new KryptonContextMenuSeparator());

                menuItems.Items.Add(showQAT);
            }

            if (AllowMinimizedChange)
            {
                // Allow the ribbon to be minimized
                KryptonContextMenuItem minimize = new KryptonContextMenuItem();
                minimize.Text = RibbonStrings.Minimize;
                minimize.Checked = MinimizedMode;
                minimize.Click += new EventHandler(OnInvertMinimizeMode);

                // Add into the context menu
                if (menuItems.Items.Count > 0)
                    menuItems.Items.Add(new KryptonContextMenuSeparator());

                menuItems.Items.Add(minimize);
            }

            // Give developers a change to modify the customize menu or even cancel it
            ContextMenuArgs args = new ContextMenuArgs(_kcm);
            OnShowQATCustomizeMenu(args);
            
            // If not cancelled, then show it
            if (!args.Cancel && (args.KryptonContextMenu != null) && CommonHelper.ValidKryptonContextMenu(_kcm))
            {
                // Cache the finish delegate to call when the menu is closed
                _kcmFinishDelegate = finishDelegate;

                // Show at the bottom of the button on the left hand side
                VisualPopupManager.Singleton.EndAllTracking();
                _kcm.Show(new Point(screenRectangle.X, screenRectangle.Bottom + 1));
            }
            else
            {
                // if not showing the menu then fire completion delegate right away
                if (finishDelegate != null)
                    finishDelegate(this, EventArgs.Empty);
            }
        }

        internal void DisplayQATOverflowMenu(Rectangle screenRectangle,
                                             ViewLayoutRibbonQATContents contents,
                                             EventHandler finishDelegate)
        {
            // Create the popup window for the group
            VisualPopupQATOverflow popupGroup = new VisualPopupQATOverflow(this, contents, Renderer);

            // Ask the popup to show itself relative to ourself
            popupGroup.ShowCalculatingSize(screenRectangle, finishDelegate);
        }

        internal void DisplayRibbonContextMenu(MouseEventArgs e)
        {
            // Ensure cached krypton context menu is ready for use
            ResetCachedKryptonContextMenu();

            // Create a container for a set of individual menu items
            KryptonContextMenuItems menuItems = new KryptonContextMenuItems();
            _kcm.Items.Add(menuItems);

            // Do we need to allow the QAT location to be inverted?
            if (QATLocation != QATLocation.Hidden)
            {
                KryptonContextMenuItem showQAT = new KryptonContextMenuItem();
                if (QATLocation == QATLocation.Above)
                    showQAT.Text = RibbonStrings.ShowQATBelowRibbon;
                else
                    showQAT.Text = RibbonStrings.ShowQATAboveRibbon;
                showQAT.Click += new EventHandler(OnInvertQATLocation);

                // Add into the context menu
                menuItems.Items.Add(showQAT);
            }

            if (AllowMinimizedChange)
            {
                // Allow the ribbon to be minimized
                KryptonContextMenuItem minimize = new KryptonContextMenuItem();
                minimize.Text = RibbonStrings.Minimize;
                minimize.Checked = MinimizedMode;
                minimize.Click += new EventHandler(OnInvertMinimizeMode);

                // Add into the context menu
                menuItems.Items.Add(new KryptonContextMenuSeparator());
                menuItems.Items.Add(minimize);
            }

            // Give developers a change to modify the context menu or even cancel it
            ContextMenuArgs args = new ContextMenuArgs(_kcm);
            OnShowRibbonContextMenu(args);
            
            // If not cancelled, then show it
            if (!args.Cancel && (args.KryptonContextMenu != null) && CommonHelper.ValidKryptonContextMenu(_kcm))
            {
                // Show at location we were provided, but need to convert to screen coordinates
                VisualPopupManager.Singleton.EndAllTracking();
                _kcm.Show(PointToScreen(new Point(e.X, e.Y)));
            }
        }

        internal ViewBase GetFirstQATView()
        {
            switch (QATLocation)
            {
                case QATLocation.Above:
                    return _captionArea.VisibleQAT.GetFirstQATView();
                case QATLocation.Below:
                    return _qatBelowContents.GetFirstQATView();
                case QATLocation.Hidden:
                    return null;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        internal ViewBase GetLastQATView()
        {
            switch (QATLocation)
            {
                case QATLocation.Above:
                    return _captionArea.VisibleQAT.GetLastQATView();
                case QATLocation.Below:
                    return _qatBelowContents.GetLastQATView();
                case QATLocation.Hidden:
                    return null;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return null;
            }
        }

        internal ViewBase GetNextQATView(ViewBase qatView, bool tab)
        {
            ViewBase view = null;

            switch (QATLocation)
            {
                case QATLocation.Above:
                    view = _captionArea.VisibleQAT.GetNextQATView(qatView);
                    break;
                case QATLocation.Below:
                    view = _qatBelowContents.GetNextQATView(qatView);
                    break;
            }

            // Get the first near edge button (the last near button is the leftmost one!)
            if (view == null)
                view = TabsArea.ButtonSpecManager.GetLastVisibleViewButton(PaletteRelativeEdgeAlign.Near);

            if (view == null)
            {
                if (tab && (SelectedTab != null))
                {
                    // Get the currently selected tab page
                    view = TabsArea.LayoutTabs.GetViewForRibbonTab(SelectedTab);
                }
                else
                {
                    // Get the first visible tab page
                    view = TabsArea.LayoutTabs.GetViewForFirstRibbonTab();
                }
            }

            // Move across to any far defined buttons
            if (view == null)
                view = TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Far);

            // Move across to any inherit defined buttons
            if (view == null)
                view = TabsArea.ButtonSpecManager.GetFirstVisibleViewButton(PaletteRelativeEdgeAlign.Inherit);

            // Move back to the application button/tab
            if (view == null)
            {
                if (TabsArea.LayoutAppButton.Visible)
                    view = TabsArea.LayoutAppButton.AppButton;
                else if (TabsArea.LayoutAppTab.Visible)
                    view = TabsArea.LayoutAppTab.AppTab;
            }

            return view;
        }

        internal ViewBase GetPreviousQATView(ViewBase qatView)
        {
            ViewBase view = null;

            switch (QATLocation)
            {
                case QATLocation.Above:
                    view = _captionArea.VisibleQAT.GetPreviousQATView(qatView);
                    break;
                case QATLocation.Below:
                    view = _qatBelowContents.GetPreviousQATView(qatView);
                    break;
            }

            // Move back to the application button/tab
            if (view == null)
            {
                if (TabsArea.LayoutAppButton.Visible)
                    view = TabsArea.LayoutAppButton.AppButton;
                else if (TabsArea.LayoutAppTab.Visible)
                    view = TabsArea.LayoutAppTab.AppTab;
            }

            return view;
        }

        internal bool InKeyboardMode
        {
            get { return _keyboardMode; }
        }

        internal bool InKeyTipsMode
        {
            get { return _keyboardKeyTips; }
        }

        internal void KillKeyboardMode()
        {
            // If in keyboard mode, then exit the mode
            if (_keyboardMode)
                ToggleKeyboardMode();
        }

        internal void KillKeyboardKeyTips()
        {
            if (_keyboardKeyTips)
            {
                KeyTipMode = KeyTipMode.Root;
                _keyboardKeyTips = false;

                if (_keyTipControlE != null)
                {
                    _keyTipControlE.Dispose();
                    _keyTipControlE = null;
                }

                if (_keyTipControlD != null)
                {
                    _keyTipControlD.Dispose();
                    _keyTipControlD = null;
                }
            }
        }

        internal void KillMinimizedPopup()
        {
            // Is there a minimized popup showing?
            if (_minimizedPopup != null)
            {
                // Unhook events from popup
                _minimizedPopup.Disposed -= new EventHandler(OnMinimizedPopupDisposed);

                // Kill all the showing popups
                VisualPopupManager.Singleton.EndAllTracking();
                _minimizedPopup = null;

                // Update selection to match ribbon settings
                ValidateSelectedTab();
            }
        }

        internal void RestorePreviousFocus()
        {
            if (_keyboardFocusWindow != IntPtr.Zero)
            {
                // Put focus back to original source
                PI.SetFocus(_keyboardFocusWindow);

                // If the caret was showing when we stole focus, then put it back
                if (_keyboardFocusCaret)
                    PI.ShowCaret(_keyboardFocusWindow);
            }
        }

        internal void ActionOccured()
        {
            // If showing the popup in minimized mode, then remove it gracefully
            if (_minimizedPopup != null)
                KillMinimizedPopup();
            else
            {
                // Just remove all popups in the popup stack
                VisualPopupManager.Singleton.EndAllTracking();
            }

            // Exit any keyboard mode and return focus to original source
            KillKeyboardMode();
        }

        internal void UpdateQAT()
        {
            if (_captionArea != null)
                _captionArea.UpdateQAT();
        }

        internal KeyTipMode KeyTipMode
        {
            get { return _keyTipMode; }

            set
            {
                _keyTipMode = value;

                switch (_keyTipMode)
                {
                    case KeyTipMode.PopupGroup:
                        _keyTipRelative = VisualPopupManager.Singleton.TrackingByType(typeof(VisualPopupGroup));
                        break;
                    case KeyTipMode.PopupMinimized:
                        _keyTipRelative = VisualPopupManager.Singleton.TrackingByType(typeof(VisualPopupMinimized));
                        break;
                    case KeyTipMode.PopupQATOverflow:
                        _keyTipRelative = VisualPopupManager.Singleton.TrackingByType(typeof(VisualPopupQATOverflow));
                        break;
                    case KeyTipMode.SelectedGroups:
                    case KeyTipMode.Root:
                    default:
                        _keyTipRelative = this;
                        break;
                }
            }
        }

        internal void SetKeyTips(KeyTipInfoList keyTipList, KeyTipMode newMode)
        {
            if (_keyTipControlE != null)
                _keyTipControlE.Dispose();

            if (_keyTipControlD != null)
                _keyTipControlD.Dispose();

            _keyTipControlE = new KeyTipControl(this, keyTipList, false);
            _keyTipControlD = new KeyTipControl(this, keyTipList, true);
            _keyTipMode = newMode;
        }

        internal void AppendKeyTipPress(char key)
        {
            if (InKeyboardMode && InKeyTipsMode && (_keyTipControlE != null))
                _keyTipControlE.AppendKeyPress(char.ToUpper(key));
        }

        internal KeyTipInfoList GenerateKeyTipsAtTopLevel()
        {
            // Make sure all the elements in current ribbon have been synched 
            // and created so that the generated contents are accurate
            Refresh();

            KeyTipInfoList keyTipList = new KeyTipInfoList();

            // Add the application button/tab
            if (_tabsArea.LayoutAppButton.Visible)
                keyTipList.Add(_tabsArea.GetAppButtonKeyTip());
            
            if (_tabsArea.LayoutAppTab.Visible)
                keyTipList.Add(_tabsArea.GetAppTabKeyTip());

            // Add the quick access toolbar buttons
            if (QATLocation == QATLocation.Above)
                keyTipList.AddRange(_captionArea.VisibleQAT.GetQATKeyTips());
            else
                keyTipList.AddRange(_qatBelowContents.GetQATKeyTips(null));

            // Add the tab headers
            keyTipList.AddRange(_tabsArea.GetTabKeyTips());

            return keyTipList;
        }

        internal KeyTipInfoList GenerateKeyTipsForSelectedTab()
        {
            // Make sure all the elements in current tab have been synched 
            // and created so that the generated contents are accurate
            Refresh();

            KeyTipInfoList keyTipList = new KeyTipInfoList();

            // There should be a selected page
            if (SelectedTab != null)
                keyTipList.AddRange(GroupsArea.ViewGroups.GetGroupKeyTips(SelectedTab));

            return keyTipList;
        }

        internal Rectangle ToolTipScreenRectangle
        {
            get
            {
                // If currently showing a popup
                if (VisualPopupManager.Singleton.CurrentPopup != null)
                {
                    // We are only interested in popup groups the popup ribbon itself
                    Control c = VisualPopupManager.Singleton.CurrentPopup;
                    if ((c is VisualPopupGroup) || (c is VisualPopupMinimized))
                        return c.RectangleToScreen(c.ClientRectangle);

                    // Check the stacked popups to see if any of those are of interest
                    foreach (VisualPopup popup in VisualPopupManager.Singleton.StackedPopups)
                        if ((popup is VisualPopupGroup) || (popup is VisualPopupMinimized))
                            return popup.RectangleToScreen(popup.ClientRectangle);
                }

                return RectangleToScreen(ClientRectangle);
            }
        }


        internal Point ViewPointToScreen(Point pt)
        {
            if (VisualPopupManager.Singleton.CurrentPopup != null)
                return VisualPopupManager.Singleton.CurrentPopup.PointToScreen(pt);
            else
                return PointToScreen(pt);
        }

        internal Rectangle ViewRectangleToScreen(ViewBase view)
        {
            return view.OwningControl.RectangleToScreen(view.ClientRectangle);
        }

        internal Rectangle KeyTipToScreen(ViewBase view)
        {
            return view.OwningControl.RectangleToScreen(view.ClientRectangle);
        }

        internal ViewBase FocusView
        {
            get { return _focusView; }

            set
            {
                // Only interested in changes of focus
                if (_focusView != value)
                {
                    // Remove focus from existing view
                    if (_focusView != null)
                        _focusView.LostFocus(this);

                    _focusView = value;

                    // Add focus to the new view
                    if (_focusView != null)
                        _focusView.GotFocus(this);
                }
            }
        }

        internal bool LostFocusLosesKeyboard
        {
            get { return _lostFocusLosesKeyboard; }
            set { _lostFocusLosesKeyboard = value; }
        }

        internal bool IgnoreRestoreFocus
        {
            get { return _ignoreRestoreFocus; }
            set { _ignoreRestoreFocus = value; }
        }

        internal PaletteRibbonShape RibbonShape
        {
            get { return StateCommon.RibbonGeneral.GetRibbonShape(); }
        }

        internal PaletteRedirect GetRedirector()
        {
            return Redirector;
        }

        internal Control GetControllerControl(Control c)
        {
            // Keep searching till we get to the top of the hierarchy
            while (c != null)
            {
                // If the control is a well known control for use by controllers
                if ((c is KryptonRibbon) ||
                    (c is VisualPopupGroup) ||
                    (c is VisualPopupMinimized))
                    return c;

                // Move up a level
                c = c.Parent;
            }

            return c;
        }

        internal void MinimizedKeyDown(Keys keyData)
        {
            // When in keyboard mode...
            if (_focusView != null)
            {
                // We pass movements keys onto the view
                switch (keyData)
                {
                    case Keys.Tab | Keys.Shift:
                    case Keys.Tab:
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Space:
                    case Keys.Enter:
                        // Any navigation keys remove the keytips
                        KillKeyboardKeyTips();
                        _focusView.KeyDown(new KeyEventArgs(keyData));
                        break;
                }
            }
        }
        #endregion

        #region Private Identity
        private void AssignDefaultFields()
        {
            _designHelpers = true;

            AllowButtonSpecToolTips = false;
            AllowMinimizedChange = true;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Dock = DockStyle.Top;
            HideRibbonSize = new Size(300, 250);
            MinimizedMode = false;
            ScrollerStyle = ButtonStyle.Standalone;
            ShowMinimizeButton = true;
            QATLocation = QATLocation.Above;
            QATUserChange = true;
            AllowFormIntegrate = true;
            LostFocusLosesKeyboard = true;

            BackStyle = PaletteBackStyle.PanelClient;
            BackInactiveStyle = PaletteBackStyle.PanelRibbonInactive;
            GroupButtonStyle = ButtonStyle.ButtonSpec;
            GroupClusterButtonStyle = ButtonStyle.Cluster;
            GroupCollapsedButtonStyle = ButtonStyle.Alternate;
            GroupDialogButtonStyle = ButtonStyle.ButtonSpec;
            QATButtonStyle = ButtonStyle.ButtonSpec;
            ScrollerStyle = ButtonStyle.Standalone;
        }

        private void CreateInternal()
        {
            _calculatedValues = new CalculatedValues(this);

            // On Vista and above we always invalidate the control on a resize
            _invalidateOnResize = (Environment.OSVersion.Version.Major >= 6);

            // Create a hidden button not inside the visible area, so acts as a target for
            // giving the focus when we do not want the focus showing on a child control
            _hiddenFocusTarget = new Button();
            _hiddenFocusTarget.TabStop = false;
            _hiddenFocusTarget.Location = new Point(-_hiddenFocusTarget.Width, -_hiddenFocusTarget.Height);
            CommonHelper.AddControlToParent(this, _hiddenFocusTarget);
        }

        private void CreateRibbonCollections()
        {
            _ribbonContexts = new KryptonRibbonContextCollection();
            _ribbonContexts.Clearing += new EventHandler(OnRibbonContextsClearing);
            _ribbonContexts.Cleared += new EventHandler(OnRibbonContextsCleared);
            _ribbonContexts.Inserted += new TypedHandler<KryptonRibbonContext>(OnRibbonContextsInserted);
            _ribbonContexts.Removed += new TypedHandler<KryptonRibbonContext>(OnRibbonContextsRemoved);

            _ribbonTabs = new KryptonRibbonTabCollection();
            _ribbonTabs.Clearing += new EventHandler(OnRibbonTabsClearing);
            _ribbonTabs.Cleared += new EventHandler(OnRibbonTabsCleared);
            _ribbonTabs.Inserted += new TypedHandler<KryptonRibbonTab>(OnRibbonTabsInserted);
            _ribbonTabs.Removed += new TypedHandler<KryptonRibbonTab>(OnRibbonTabsRemoved);

            _qatButtons = new KryptonRibbonQATButtonCollection();
            _qatButtons.Clearing += new EventHandler(OnRibbonQATButtonsClearing);
            _qatButtons.Cleared += new EventHandler(OnRibbonQATButtonsCleared);
            _qatButtons.Inserted += new TypedHandler<Component>(OnRibbonQATButtonsInserted);
            _qatButtons.Removed += new TypedHandler<Component>(OnRibbonQATButtonsRemoved);
        }

        private void CreateButtonSpecs()
        {
            _buttonSpecs = new RibbonButtonSpecAnyCollection(this);
        }

        private void CreateStorageObjects()
        {
            _ribbonShortcuts = new RibbonShortcuts();
            _ribbonStrings = new RibbonStrings();
            _ribbonAppButton = new RibbonAppButton(this);
            _ribbonStyles = new PaletteRibbonStyles(this, NeedPaintPaletteDelegate);
            _stateCommon = new PaletteRibbonRedirect(Redirector, PaletteBackStyle.PanelClient, NeedPaintPaletteDelegate);
            _stateDisabled = new PaletteRibbonDisabled(_stateCommon, NeedPaintPaletteDelegate);
            _stateNormal = new PaletteRibbonNormal(_stateCommon, NeedPaintPaletteDelegate);
            _stateTracking = new PaletteRibbonAppGroupTab(_stateCommon, NeedPaintPaletteDelegate);
            _statePressed = new PaletteRibbonAppButton(_stateCommon, NeedPaintPaletteDelegate);
            _stateCheckedNormal = new PaletteRibbonGroupAreaTab(_stateCommon, NeedPaintPaletteDelegate);
            _stateCheckedTracking = new PaletteRibbonJustTab(_stateCommon, NeedPaintPaletteDelegate);
            _stateContextNormal = new PaletteRibbonJustGroup(_stateCommon, NeedPaintPaletteDelegate);
            _stateContextTracking = new PaletteRibbonGroupTab(_stateCommon, NeedPaintPaletteDelegate);
            _stateContextCheckedNormal = new PaletteRibbonGroupAreaTab(_stateCommon, NeedPaintPaletteDelegate);
            _stateContextCheckedTracking = new PaletteRibbonJustTab(_stateCommon, NeedPaintPaletteDelegate);
            _overrideFocus = new PaletteRibbonFocus(Redirector, NeedPaintPaletteDelegate);
        }

        private void CreateViewManager()
        {
            // Setup the need paint delegate
            _needPaintGroups = new NeedPaintHandler(OnNeedPaintMinimizedGroups);

            // Create direct access to the redirector for panel background
            _backPanelInherit = new PaletteBackInheritRedirect(Redirector, PaletteBackStyle.PanelClient);

            // Create the background panel for the entire ribbon area and the groups area when minimized
            _drawPanel = new ViewDrawRibbonPanel(this, _backPanelInherit, NeedPaintDelegate);
            _drawMinimizedPanel = new ViewDrawPanel(_backPanelInherit);

            // Create layout docker for the entire ribbon control
            _rootDocker = new ViewLayoutDocker();

            // Docker for the area below the composition element
            _ribbonDocker = new ViewLayoutDocker();

            // Create the area used to host the caption of glass window
            _compositionArea = new ViewDrawRibbonComposition(this, NeedPaintDelegate);
            _compositionArea.Visible = false;

            // Create caption area which is used if custom chrome cannot perform task
            _captionArea = new ViewDrawRibbonCaptionArea(this, Redirector, _compositionArea, NeedPaintDelegate);

            // Create tabs area containing the tabs, pendant buttons etc...
            _tabsArea = new ViewLayoutRibbonTabsArea(this, Redirector, _captionArea, _captionArea.ContextTitles, NeedPaintDelegate);
            _tabsArea.PaintBackground += new PaintEventHandler(OnTabsAreaPaintBackground);

            // Create groups area containing the groups of the selected tab
            _groupsArea = new ViewLayoutRibbonGroupsArea(this, Redirector, _needPaintGroups);

            // Create the quick access toolbar for when below the ribbon
            _qatBelowContents = new ViewLayoutRibbonQATFromRibbon(this, NeedPaintDelegate, true);
            _qatBelowRibbon = new ViewDrawRibbonQATBorder(this, NeedPaintDelegate, false);
            _qatBelowRibbon.Visible = false;
            _qatBelowRibbon.Add(_qatBelowContents);

            // Separator used at bottom of tabs when ribbon is minized
            _minimizeBar = new ViewDrawRibbonMinimizeBar(StateCommon.RibbonGeneral);
            _minimizeBar.Visible = false;

            // Connect up the various view elements
            _drawPanel.Add(_ribbonDocker);
            _ribbonDocker.Add(_groupsArea, ViewDockStyle.Fill);
            _ribbonDocker.Add(_minimizeBar, ViewDockStyle.Bottom);
            _ribbonDocker.Add(_qatBelowRibbon, ViewDockStyle.Bottom);
            _ribbonDocker.Add(_tabsArea, ViewDockStyle.Top);
            _ribbonDocker.Add(_captionArea, ViewDockStyle.Top);

            // The root contains the composition at the top and fills out with the panel areas
            _rootDocker.Add(_drawPanel, ViewDockStyle.Fill);
            _rootDocker.Add(_compositionArea, ViewDockStyle.Top);

            // Final construction steps
            _captionArea.HookToolTipHandling();
            _tabsArea.HookToolTipHandling();

            // Create the view manager instance
            ViewManager = new ViewRibbonManager(this, _groupsArea.ViewGroups, _rootDocker, false, NeedPaintDelegate);
        }
        #endregion

        #region Private
        private void CheckForAltUp()
        {
            if (_altDown)
            {
                if ((Control.ModifierKeys & Keys.Alt) != Keys.Alt)
                {
                    _altDown = false;
                    _altUpCount++;
                }
            }
        }

        private void CheckForAltDown()
        {
            if (!_altDown)
            {
                if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    _altDown = true;
                }
            }
        }

        private bool IsAppThemed
        {
            get
            {
                // If an exception was thrown last time, then always false
                if (_uxthemeNotAvailable)
                    return false;
                else
                {
                    try
                    {
                        // Not all operating systems have Uxtheme.dll for these calls
                        return PI.IsAppThemed() && PI.IsThemeActive();
                    }
                    catch
                    {
                        // Is platform invoke not available, then definitely not themed
                        _uxthemeNotAvailable = true;
                        return false;
                    }
                }
            }
        }

        private bool TabIsContextValid(KryptonRibbonTab tab)
        {
            // If the tab is not part of a context, then it is context valid or if 
            // in design mode then all tabs are valid as the context is ignored
            if (string.IsNullOrEmpty(tab.ContextName) || InDesignHelperMode)
                return true;
            else
            {
                // If there is a selected context to test against...
                if (!string.IsNullOrEmpty(SelectedContext))
                {
                    // Get list of all valid contexts
                    string[] contexts = SelectedContext.Split(',');

                    // If the tab context name is one of the selected contexts, then fine
                    foreach (string context in contexts)
                        if (context == tab.ContextName)
                            return true;
                }

                return false;
            }
        }

        private void ValidateSelectedTab()
        {
            KryptonRibbonTab newSelection = null;

            // If not minimized...
            if (!RealMinimizedMode)
            {
                if ((SelectedTab == null) ||                            // If there is no selection...
                    (!SelectedTab.Visible && !InDesignHelperMode) ||    // Or the selection is no longer visible
                    !RibbonTabs.Contains(SelectedTab) ||                // Or the selection is not part of our tab collection...
                    !TabIsContextValid(SelectedTab))                    // Or the selection is not part of the context...
                {
                    // Search for a non-context tab to select
                    foreach (KryptonRibbonTab tab in RibbonTabs)
                        if (string.IsNullOrEmpty(tab.ContextName))
                        {
                            // Only interested in visible tabs 
                            if (tab.Visible)
                            {
                                // Make it the new selection
                                newSelection = tab;
                                break;
                            }
                        }

                    // Still nothing found?
                    if (newSelection == null)
                    {
                        // Search for a context tab
                        foreach (KryptonRibbonTab tab in RibbonTabs)
                            if (!string.IsNullOrEmpty(tab.ContextName))
                            {
                                // Only interested in visible tabs that are part of the context selection
                                if (tab.Visible && TabIsContextValid(tab))
                                {
                                    // Make it the new selection
                                    newSelection = tab;
                                    break;
                                }
                            }
                    }
                }
                else
                {
                    // Current tab selection is valid, continue to use it
                    newSelection = SelectedTab;
                }
            }

            // Newly calculated tab different to current selection?
            if (SelectedTab != newSelection)
            {
                _selectedTab = newSelection;
                OnSelectedTabChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }

        private bool SelectNonRibbonControl(bool forward)
        {
            // Find the control in our hierarchy that has the focus
            Control focus = CommonHelper.GetControlWithFocus(this);

            // If nothing has the focus then we cannot perform processing
            if (focus != null)
            {
                // Get the owning form because we want to search all controls in the
                // form hierarchy and not just the controls in our own hierarchy
                Control form = focus.FindForm();

                // If we cannot find an owning form
                if (form == null)
                {
                    // Walk up the parent chain until we reach the top
                    form = focus;
                    while (form.Parent != null)
                        form = form.Parent;
                }

                // Start searching from the current focus control
                Control next = focus;

                // Have we wrapped around the end yet?
                bool wrapped = false;

                do
                {
                    // Find the next control in sequence
                    next = form.GetNextControl(next, forward);

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
                        // We never give focus to ourself
                        if ((next != this) && !Contains(next))
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
                while (next != focus);
            }

            // We cannot select the next page control
            return false;
        }

        private void OnRibbonContextsClearing(object sender, EventArgs e)
        {
            // Unhook from all the context instances
            foreach(KryptonRibbonContext context in RibbonContexts)
                context.PropertyChanged -= new PropertyChangedEventHandler(OnContextPropertyChanged);
        }

        private void OnRibbonContextsCleared(object sender, EventArgs e)
        {
            // Layout now the collection has been cleared down
            _captionArea.UpdateVisible();
            PerformNeedPaint(true);
        }

        private void OnRibbonContextsInserted(object sender, TypedCollectionEventArgs<KryptonRibbonContext> e)
        {
            // Hook into property changes for the context
            e.Item.PropertyChanged += new PropertyChangedEventHandler(OnContextPropertyChanged);
            _captionArea.UpdateVisible();
            PerformNeedPaint(true);
        }

        private void OnRibbonContextsRemoved(object sender, TypedCollectionEventArgs<KryptonRibbonContext> e)
        {
            // Remove context instance hook
            e.Item.PropertyChanged -= new PropertyChangedEventHandler(OnContextPropertyChanged);
            _captionArea.UpdateVisible();
            PerformNeedPaint(true);
        }

        private void OnContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Layout to show the context change
            PerformNeedPaint(true);
        }

        private void OnRibbonTabsClearing(object sender, EventArgs e)
        {
            // Remove all the back references
            foreach (KryptonRibbonTab tab in RibbonTabs)
            {
                // Unhook from tab property change event
                tab.PropertyChanged -= new PropertyChangedEventHandler(OnTabPropertyChanged);

                // Remove back reference
                tab.Ribbon = null;
            }
        }

        private void OnRibbonTabsCleared(object sender, EventArgs e)
        {
            // Do not remember a tab this is now removed
            _minSelectedTab = null;

            // Update selection to match ribbon settings
            ValidateSelectedTab();

            // Display not updated until a layout occurs
            PerformNeedPaint(true);
        }

        private void OnRibbonTabsInserted(object sender, TypedCollectionEventArgs<KryptonRibbonTab> e)
        {
            // Setup the back reference from tab to ribbon control
            e.Item.Ribbon = this;

            // Need to monitor tab in case its properties change
            e.Item.PropertyChanged += new PropertyChangedEventHandler(OnTabPropertyChanged);

            // Update selection to match ribbon settings
            ValidateSelectedTab();

            // Display not updated until a layout occurs
            PerformNeedPaint(true);
        }

        private void OnRibbonTabsRemoved(object sender, TypedCollectionEventArgs<KryptonRibbonTab> e)
        {
            // Unhook from tab property change event
            e.Item.PropertyChanged -= new PropertyChangedEventHandler(OnTabPropertyChanged);

            // Remove the backreference
            e.Item.Ribbon = null;

            // Do not remember a tab this is now removed
            if (_minSelectedTab == e.Item)
                _minSelectedTab = null;

            // Update selection to match ribbon settings
            ValidateSelectedTab();

            // Display not updated until a layout occurs
            PerformNeedPaint(true);
        }

        private void OnTabPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Visible":
                    // Update selection to match ribbon settings
                    ValidateSelectedTab();

                    // Display not updated until a layout occurs
                    PerformNeedPaint(true);
                    break;
            }
        }

        private void OnRibbonQATButtonsClearing(object sender, EventArgs e)
        {
            // Stop tracking changes in button properties
            foreach (IQuickAccessToolbarButton qatButton in QATButtons)
                qatButton.PropertyChanged -= new PropertyChangedEventHandler(OnQATButtonPropertyChanged);
        }

        private void OnRibbonQATButtonsCleared(object sender, EventArgs e)
        {
            // Display not updated until a layout occurs
            PerformNeedPaint(true);

            // Inform the caption area it might need to repaint the integrated QAT
            _captionArea.QATButtonsChanged();
        }

        private void OnRibbonQATButtonsInserted(object sender, TypedCollectionEventArgs<Component> e)
        {
            IQuickAccessToolbarButton qatButton = e.Item as IQuickAccessToolbarButton;
            Debug.Assert(qatButton != null);

            // Setup the back reference from tab to ribbon control
            if (qatButton != null)
                qatButton.SetRibbon(this);

            // Track changes in button properties
            qatButton.PropertyChanged += new PropertyChangedEventHandler(OnQATButtonPropertyChanged);

            // Display not updated until a layout occurs
            PerformNeedPaint(true);

            // Inform the caption area it might need to repaint the integrated QAT
            _captionArea.QATButtonsChanged();
        }

        private void OnRibbonQATButtonsRemoved(object sender, TypedCollectionEventArgs<Component> e)
        {
            IQuickAccessToolbarButton qatButton = e.Item as IQuickAccessToolbarButton;
            Debug.Assert(qatButton != null);

            // Stop tracking changes in button properties
            qatButton.PropertyChanged -= new PropertyChangedEventHandler(OnQATButtonPropertyChanged);

            // Remove the backreference
            if (qatButton != null)
                qatButton.SetRibbon(null);
            
            // Display not updated until a layout occurs
            PerformNeedPaint(true);

            // Inform the caption area it might need to repaint the integrated QAT
            _captionArea.QATButtonsChanged();
        }

        private void OnNeedPaintMinimizedGroups(object sender, NeedLayoutEventArgs e)
        {
            // When in minimized mode...
            if (RealMinimizedMode)
            {
                // And we have a popup showing...
                if (_minimizedPopup != null)
                {
                    // Then pass the paint request to the popup control
                    _minimizedPopup.PerformNeedPaint(e.NeedLayout);
                }
            }
            else
            {
                // Not minimized, so paint ourself as normal
                OnNeedPaint(this, e);
            }
        }

        private void OnQATButtonPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Changein button property requires a layout effect change
            PerformNeedPaint(true);

            // If the buttons are integrated into caption area then needs laying out as well
            _captionArea.QATButtonsChanged();
        }

        private void OnInvertQATLocation(object sender, EventArgs e)
        {
            // Remove any popups, the customize context menu can appear because
            // the use selected the QAT overflow button causing a popup to show
            VisualPopupManager.Singleton.EndAllTracking();

            // End any keyboard mode
            KillKeyboardMode();

            if (QATLocation == QATLocation.Above)
                QATLocation = QATLocation.Below;
            else
                QATLocation = QATLocation.Above;
        }

        private void OnInvertMinimizeMode(object sender, EventArgs e)
        {
            // Remove any popups, the customize context menu can appear because
            // the use selected the QAT overflow button causing a popup to show
            VisualPopupManager.Singleton.EndAllTracking();

            // End any keyboard mode
            KillKeyboardMode();

            MinimizedMode = !MinimizedMode;
        }

        private void ShowMinimizedPopup()
        {
            // Need to recalculate anything relying on the palette
            DirtyPaletteCounter++;

            // Create a popup control with the minimized panel as the view
            ViewRibbonMinimizedManager popupManager = new ViewRibbonMinimizedManager(this, _groupsArea.ViewGroups, _drawMinimizedPanel, true, _needPaintGroups);
            _minimizedPopup = new VisualPopupMinimized(this, popupManager, _captionArea, Renderer);
            _minimizedPopup.Disposed += new EventHandler(OnMinimizedPopupDisposed);
            popupManager.Attach(_minimizedPopup, _drawMinimizedPanel);

            // Show the groups area as a popup!
            _minimizedPopup.Show(_tabsArea, _drawMinimizedPanel);
        }

        private void UpdateMinimizedPopup()
        {
            // Update the screen location of popup to reflect a change in selected tab
            _minimizedPopup.Show(_tabsArea, _drawMinimizedPanel);
        }

        private void OnMinimizedPopupDisposed(object sender, EventArgs e)
        {
            // We no longer have a popup showing
            _minimizedPopup = null;

            // Enforce tab selection rules now that the popup is no longer present
            ValidateSelectedTab();

            // Repaint to show changes
            PerformNeedPaint(true);
        }

        private void OnQATCustomizeClick(object sender, EventArgs e)
        {
            // Remove any popups, the customize context menu can appear because
            // the use selected the QAT overflow button causing a popup to show
            VisualPopupManager.Singleton.EndAllTracking();

            // End any keyboard mode
            KillKeyboardMode();

            // Cast to correct type
            KryptonContextMenuItem menuItem = (KryptonContextMenuItem)sender;

            // Find index of the item to toggle
            int index = (int)menuItem.Tag;

            // Double check the index is still valid
            if ((index >= 0) && (index < QATButtons.Count))
            {
                // Get access to the indexed entry
                IQuickAccessToolbarButton qatButton = (IQuickAccessToolbarButton)QATButtons[index];

                // Invert the visible state
                qatButton.SetVisible(!qatButton.GetVisible());

                // Need a layout to see the change
                _captionArea.UpdateQAT();
                PerformNeedPaint(true);
            }
        }

        private void ResetCachedKryptonContextMenu()
        {
            // First time around we need to create the strip
            if (_kcm == null)
            {
                _kcm = new KryptonContextMenu();
                _kcm.Closed += new ToolStripDropDownClosedEventHandler(OnKryptonContextMenuClosed);
            }

            // Remove any existing items
            _kcm.Items.Clear();
        }

        private void OnKryptonContextMenuClosed(object sender, EventArgs e)
        {
            // Fire any associated finish delegate
            if (_kcmFinishDelegate != null)
            {
                _kcmFinishDelegate(this, e);
                _kcmFinishDelegate = null;
            }
        }

        private void OnTabsAreaPaintBackground(object sender, PaintEventArgs e)
        {
            _drawPanel.PaintRectangle(e.Graphics, e.ClipRectangle, false, sender as Control);
        }
        #endregion
    }
}
