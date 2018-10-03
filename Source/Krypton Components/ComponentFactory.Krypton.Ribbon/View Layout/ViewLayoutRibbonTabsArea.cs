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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Ribbon area that contains the tabs and pendant.
    /// </summary>
    internal class ViewLayoutRibbonTabsArea : ViewLayoutDocker
    {
        #region Type Definitions
        /// <summary>
        /// Collection for managing ButtonSpec fixed instances.
        /// </summary>
        public class RibbonButtonSpecFixedCollection : ButtonSpecCollection<ButtonSpec> 
        { 
            #region Identity
            /// <summary>
            /// Initialize a new instance of the RibbonButtonSpecFixedCollection class.
            /// </summary>
            /// <param name="owner">Reference to owning object.</param>
            public RibbonButtonSpecFixedCollection(KryptonRibbon owner)
                : base(owner)
            {
            }
            #endregion
        }
        #endregion

        #region Static Fields
        private static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);
        private static readonly int BUTTON_TAB_GAP_2007 = 5;
        private static readonly int BUTTON_TAB_GAP_2010 = 0;
        private static readonly int FAR_TAB_GAP = 1;
        private static readonly int SCROLL_SPEED = 12;
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private NeedPaintHandler _needPaintDelegate;
        private ViewLayoutRibbonTabs _layoutRibbonTabs;
        private ViewLayoutRibbonScrollPort _tabsViewport;
        private ViewLayoutRibbonAppButton _layoutAppButton;
        private ViewLayoutRibbonAppTab _layoutAppTab;
        private ViewLayoutSeparator _layoutAppButtonSep;
        private ViewLayoutRibbonSeparator _leftSeparator;
        private ViewLayoutRibbonSeparator _rightSeparator;
        private ViewDrawRibbonCaptionArea _captionArea;
        private ViewLayoutRibbonContextTitles _layoutContexts;
        private AppButtonController _appButtonController;
        private AppTabController _appTabController;
        private VisualPopupToolTip _visualPopupToolTip;
        private VisualPopupAppMenu _appMenu;
        private ToolTipManager _toolTipManager;
        private DateTime _lastAppButtonClick;

        // Fixed button specifications
        private RibbonButtonSpecFixedCollection _buttonSpecsFixed;
        private ButtonSpecMdiChildClose _buttonSpecClose;
        private ButtonSpecMdiChildRestore _buttonSpecRestore;
        private ButtonSpecMdiChildMin _buttonSpecMin;
        private ButtonSpecMinimizeRibbon _buttonSpecMinimize;
        private ButtonSpecExpandRibbon _buttonSpecExpand;
        private ButtonSpecManagerLayoutRibbon _buttonManager;

        // Monitoring the containing form and mdi status
        private Timer _invalidateTimer;
        private Form _formContainer;
        private Form _activeMdiChild;
        private int _paintCount;
        private bool _setVisible;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the background needs painting.
        /// </summary>
        public event PaintEventHandler PaintBackground;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonTabsArea class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="redirect">Reference to redirector for palette settings.</param>
        /// <param name="captionArea">Reference to the caption area.</param>
        /// <param name="layoutContexts">Reference to layout of the context area.</param>
        /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
        public ViewLayoutRibbonTabsArea(KryptonRibbon ribbon,
                                        PaletteRedirect redirect,
                                        ViewDrawRibbonCaptionArea captionArea,
                                        ViewLayoutRibbonContextTitles layoutContexts,
                                        NeedPaintHandler needPaintDelegate)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(redirect != null);
            Debug.Assert(layoutContexts != null);
            Debug.Assert(captionArea != null);
            Debug.Assert(needPaintDelegate != null);

            // Remember incoming references
            _ribbon = ribbon;
            _captionArea = captionArea;
            _appButtonController = captionArea.AppButtonController;
            _appTabController = captionArea.AppTabController;
            _layoutContexts = layoutContexts;
            _needPaintDelegate = needPaintDelegate;

            // Default other state
            _setVisible = true;
            _lastAppButtonClick = DateTime.MinValue;

            CreateController();
            CreateButtonSpecs();
            CreateViewElements(redirect);
            SetupParentMonitoring();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Remove any showing tooltip
                OnCancelToolTip(this, EventArgs.Empty);
            
                // Unhook from the parent control
                _ribbon.ParentChanged += new EventHandler(OnRibbonParentChanged);

                // Unhook from watching any top level window
                if (_formContainer != null)
                {
                    _formContainer.Deactivate -= new EventHandler(OnRibbonFormDeactivate);
                    _formContainer.Activated -= new EventHandler(OnRibbonFormActivated);
                    _formContainer.SizeChanged -= new EventHandler(OnRibbonFormSizeChanged);
                    _formContainer.MdiChildActivate -= new EventHandler(OnRibbonMdiChildActivate);
                    _formContainer = null;
                }

                // Unhook from watching any mdi child window
                if (_activeMdiChild != null)
                {
                    _activeMdiChild.SizeChanged -= new EventHandler(OnRibbonMdiChildSizeChanged);
                    _activeMdiChild = null;
                }

                // Destruct the button manager resources
                if (_buttonManager != null)
                {
                    _buttonManager.Destruct();
                    _buttonManager = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutRibbonTabsArea:" + Id;
        }
        #endregion

        #region HookToolTipHandling
        /// <summary>
        /// Perform steps to generate a tooltip event when mouse is over the application button.
        /// </summary>
        public void HookToolTipHandling()
        {
            _layoutAppButton.MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager, _layoutAppButton, _appButtonController);
            _layoutAppTab.MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager, _layoutAppTab, _appTabController);
        }
        #endregion

        #region CheckRibbonSize
        /// <summary>
        /// Check if the ribbon should be visible or hidden at its current size.
        /// </summary>
        public void CheckRibbonSize()
        {
            // Just in case we have already been disposed
            if (!_ribbon.IsDisposed)
            {
                // Find the top most form we are inside
                Form topForm = _ribbon.FindForm();

                // Just in case we have not been created as yet
                if (topForm != null)
                {
                    // Ignore the minimized state, as that would always cause the ribbon to be hidden
                    // when then causes it to be reshown when the window is restored. This creates a 
                    // flicker after the restore so prevent it from disappearing in the first case.
                    if (!CommonHelper.IsFormMinimized(topForm))
                    {
                        bool show = (topForm.ClientSize.Width >= _ribbon.HideRibbonSize.Width) &&
                                    (topForm.ClientSize.Height >= _ribbon.HideRibbonSize.Height);

                        // How we handle visibility differs in OS
                        if (Environment.OSVersion.Version.Major >= 6)
                        {
                            if (_ribbon.MainPanel.Visible != show)
                            {
                                _ribbon.MainPanel.Visible = show;
                                _captionArea.PreventIntegration = !show;

                                // Need to recalcualte the composition and so the client area that is turned into glass
                                if (_captionArea.KryptonForm != null)
                                    _captionArea.KryptonForm.RecalculateComposition();
                            }
                        }
                        else
                        {
                            if ((_ribbon.Visible != show) ||
                                (_setVisible != show))
                            {
                                _ribbon.Visible = show;
                                _setVisible = show;

                                // If using custom chrome
                                if (_captionArea.UsingCustomChrome)
                                {
                                    _paintCount = _captionArea.KryptonForm.PaintCount;
                                    _invalidateTimer.Start();
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region AppButtonVisibleChanged
        /// <summary>
        /// A change in the app button visibility needs to be processed.
        /// </summary>
        public void AppButtonVisibleChanged()
        {
            // Update visible state of the app button/tab to reflect current state
            _layoutAppButton.Visible = (_ribbon.RibbonAppButton.AppButtonVisible && (_ribbon.RibbonShape == PaletteRibbonShape.Office2007));
            _layoutAppTab.Visible = (_ribbon.RibbonAppButton.AppButtonVisible && (_ribbon.RibbonShape != PaletteRibbonShape.Office2007));
            _leftSeparator.SeparatorSize = (_ribbon.RibbonShape == PaletteRibbonShape.Office2007) ? new Size(BUTTON_TAB_GAP_2007, BUTTON_TAB_GAP_2007) : new Size(BUTTON_TAB_GAP_2010, BUTTON_TAB_GAP_2010);

            // If no app button then need separator to stop first tab being to close to the left edge
            _layoutAppButtonSep.Visible = !_layoutAppButton.Visible;
        }
        #endregion

        #region LayoutTabs
        /// <summary>
        /// Gets access to the view layout used for the individual ribbon tabs.
        /// </summary>
        public ViewLayoutRibbonTabs LayoutTabs
        {
            get { return _layoutRibbonTabs; }
        }
        #endregion

        #region LayoutAppButton
        /// <summary>
        /// Gets access to the view layout used for the appplication button.
        /// </summary>
        public ViewLayoutRibbonAppButton LayoutAppButton
        {
            get { return _layoutAppButton; }
        }
        #endregion

        #region LayoutAppTab
        /// <summary>
        /// Gets access to the view layout used for the appplication tab.
        /// </summary>
        public ViewLayoutRibbonAppTab LayoutAppTab
        {
            get { return _layoutAppTab; }
        }
        #endregion

        #region TabsContainerControl
        /// <summary>
        /// Gets access to the control that contains the tabs.
        /// </summary>
        public ViewLayoutControl TabsContainerControl
        {
            get { return _tabsViewport.ViewLayoutControl; }
        }
        #endregion

        #region GetAppButtonKeyTip
        public KeyTipInfo GetAppButtonKeyTip()
        {
            // Get the screen location of the bottom half of the button
            Rectangle buttonRect = _ribbon.RectangleToScreen(_layoutAppButton.ClientRectangle);

            // Alter the rect to account for it not being exactly half the button height
            buttonRect.Y -= 5;
            buttonRect.X += 2;

            // The keytip should be centered on the top center of the bottom half
            Point screenPt = new Point(buttonRect.Left + (buttonRect.Width / 2), buttonRect.Top);

            // Return key tip details
            return new KeyTipInfo(true, _ribbon.RibbonStrings.AppButtonKeyTip, screenPt,
                                  _layoutAppButton.ClientRectangle, _appButtonController);

        }
        #endregion

        #region GetAppTabKeyTip
        public KeyTipInfo GetAppTabKeyTip()
        {
            // Get the screen location of the bottom half of the button
            Rectangle buttonRect = _ribbon.RectangleToScreen(_layoutAppTab.ClientRectangle);

            // The keytip should be centered on the top center of the bottom half
            Point screenPt = new Point(buttonRect.Left + (buttonRect.Width / 2), buttonRect.Bottom + 2);

            // Return key tip details
            return new KeyTipInfo(true, _ribbon.RibbonStrings.AppButtonKeyTip, screenPt,
                                  _layoutAppTab.ClientRectangle, _appTabController);

        }
        #endregion

        #region GetGroupKeyTips
        /// <summary>
        /// Gets the array of group level key tips.
        /// </summary>
        /// <returns>Array of KeyTipInfo; otherwise null.</returns>
        public KeyTipInfo[] GetTabKeyTips()
        {
            KeyTipInfoList keyTips = new KeyTipInfoList();

            // Grab the list of key tips for all tab headers
            keyTips.AddRange(LayoutTabs.GetTabKeyTips());

            // Remove all those that do not intercept the scroll port the tabs are inside
            Rectangle scrollRect = new Rectangle(Point.Empty, _tabsViewport.ClientSize);
            for (int i = 0; i < keyTips.Count; i++)
                if (!scrollRect.Contains(keyTips[i].ClientRect))
                    keyTips[i].Visible = false;

            return keyTips.ToArray();
        }
        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Let base class perform standard layout
            base.Layout(context);

            // Ask the context titles to layout again to take into account any
            // change in the positions and sizes of the actual tabs that it mimics
            Rectangle temp = context.DisplayRectangle;
            context.DisplayRectangle = _layoutContexts.ClientRectangle;
            _layoutContexts.Layout(context);
            context.DisplayRectangle = temp;

            // If using custom chrome but not using the composition (which does not need an extra draw)
            if (_captionArea.UsingCustomChrome && !_captionArea.KryptonForm.ApplyComposition)
            {
                _paintCount = _captionArea.KryptonForm.PaintCount;
                _invalidateTimer.Start();
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Test if there has been a double click of the app button.
        /// </summary>
        /// <returns>True if a double click was detected and pressed.</returns>
        public void TestForAppButtonDoubleClick()
        {
            DateTime nowClick = DateTime.Now;
            TimeSpan elapsed = nowClick - _lastAppButtonClick;
            _lastAppButtonClick = nowClick;

            // If this is the second click within the double click time...
            if (elapsed.TotalMilliseconds < SystemInformation.DoubleClickTime)
            {
                // Office 2010 does not close on a double click
                if (_ribbon.RibbonShape != PaletteRibbonShape.Office2010)
                {
                    // Close down the associated application window
                    Form ownerForm = _ribbon.FindForm();
                    if (ownerForm != null)
                        ownerForm.Close();
                }
            }
        }

        /// <summary>
        /// Gets access to the tool tip manager.
        /// </summary>
        public ToolTipManager ToolTipManager
        {
            get { return _toolTipManager; }
        }

        /// <summary>
        /// Gets the button specification manager.
        /// </summary>
        public ButtonSpecManagerLayoutRibbon ButtonSpecManager
        {
            get { return _buttonManager; }
        }

        /// <summary>
        /// Recreate the button specifications.
        /// </summary>
        public void RecreateButtons()
        {
            if (_buttonManager != null)
                _buttonManager.RecreateButtons();
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the need paint delegate.
        /// </summary>
        protected NeedPaintHandler NeedPaintDelegate
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _needPaintDelegate; }
        }

        /// <summary>
        /// Fires a request to have painting/layout performed.
        /// </summary>
        /// <param name="needLayout">Does the palette change require a layout.</param>
        protected void PerformNeedPaint(bool needLayout)
        {
            _needPaintDelegate(this, new NeedLayoutEventArgs(needLayout));
        }
        #endregion

        #region Implementation
        private void CreateController()
        {
            // Use a controller to initiate context menu when using right mouse click
            RibbonTabsController controller = new RibbonTabsController(_ribbon);
            controller.ContextClick += new MouseEventHandler(OnContextClicked);
            MouseController = controller;
        }

        private void CreateButtonSpecs()
        {
            // Create fixed button specifications
            _buttonSpecsFixed = new RibbonButtonSpecFixedCollection(_ribbon);
            _buttonSpecClose = new ButtonSpecMdiChildClose(_ribbon);
            _buttonSpecRestore = new ButtonSpecMdiChildRestore(_ribbon);
            _buttonSpecMin = new ButtonSpecMdiChildMin(_ribbon);
            _buttonSpecMinimize = new ButtonSpecMinimizeRibbon(_ribbon);
            _buttonSpecExpand = new ButtonSpecExpandRibbon(_ribbon);
            _buttonSpecsFixed.AddRange(new ButtonSpec[] { _buttonSpecMinimize, _buttonSpecExpand, _buttonSpecMin, _buttonSpecRestore, _buttonSpecClose });
        }

        private void CreateViewElements(PaletteRedirect redirect)
        {
            // Layout for individual tabs inside the header
            _layoutRibbonTabs = new ViewLayoutRibbonTabs(_ribbon, NeedPaintDelegate);

            // Put inside a viewport so scrollers are used when tabs cannot be shrunk to fill space
            _tabsViewport = new ViewLayoutRibbonScrollPort(_ribbon, System.Windows.Forms.Orientation.Horizontal, _layoutRibbonTabs, true, SCROLL_SPEED, NeedPaintDelegate);
            _tabsViewport.TransparentBackground = true;
            _tabsViewport.PaintBackground += new PaintEventHandler(OnTabsPaintBackground);
            _layoutRibbonTabs.ParentControl = _tabsViewport.ViewLayoutControl.ChildControl;
            _layoutRibbonTabs.NeedPaintDelegate = _tabsViewport.ViewControlPaintDelegate;

            // We use a layout docker as a child to prevent buttons going to the left of the app button
            ViewLayoutDocker tabsDocker = new ViewLayoutDocker();

            // Place the tabs viewport as the fill inside ourself, the button specs will be placed 
            // to the left and right of this fill element automatically by the button manager below
            tabsDocker.Add(_tabsViewport, ViewDockStyle.Fill);

            // We need to draw the bottom half of the application button or a full app tab
            _layoutAppButton = new ViewLayoutRibbonAppButton(_ribbon, true);
            _layoutAppTab = new ViewLayoutRibbonAppTab(_ribbon);

            // Connect up the application button controller to the app button element
            _appButtonController.Target3 = _layoutAppButton.AppButton;
            _appButtonController.Click += new EventHandler(OnAppButtonClicked);
            _appButtonController.MouseReleased += new EventHandler(OnAppButtonReleased);
            _layoutAppButton.MouseController = _appButtonController;
            _layoutAppButton.SourceController = _appButtonController;
            _layoutAppButton.KeyController = _appButtonController;

            _appTabController.Target1 = _layoutAppTab.AppTab;
            _appTabController.Click += new EventHandler(OnAppButtonClicked);
            _appTabController.MouseReleased += new EventHandler(OnAppButtonReleased);
            _layoutAppTab.MouseController = _appTabController;
            _layoutAppTab.SourceController = _appTabController;
            _layoutAppTab.KeyController = _appTabController;

            // When the app button is not visible we need separator instead before start of first tab
            _layoutAppButtonSep = new ViewLayoutSeparator(5, 0);
            _layoutAppButtonSep.Visible = false;

            // Used separators around the tabs and the edge elements
            _rightSeparator = new ViewLayoutRibbonSeparator(FAR_TAB_GAP, true);
            _leftSeparator = new ViewLayoutRibbonSeparator(BUTTON_TAB_GAP_2007, true);

            // Place application button on left  and tabs as the filler (with some separators for neatness)
            Add(_rightSeparator, ViewDockStyle.Left);
            Add(_leftSeparator, ViewDockStyle.Left);
            Add(_layoutAppButton, ViewDockStyle.Left);
            Add(_layoutAppButtonSep, ViewDockStyle.Left);
            Add(_layoutAppTab, ViewDockStyle.Left);
            Add(tabsDocker, ViewDockStyle.Fill);

            // Create button specification collection manager
            PaletteRedirect aeroOverrideText = new PaletteRedirectRibbonAeroOverride(_ribbon, redirect);
            _buttonManager = new ButtonSpecManagerLayoutRibbon(_ribbon, aeroOverrideText, _ribbon.ButtonSpecs, _buttonSpecsFixed,
                                                               new ViewLayoutDocker[] { tabsDocker },
                                                               new IPaletteMetric[] { _ribbon.StateCommon },
                                                               new PaletteMetricInt[] { PaletteMetricInt.HeaderButtonEdgeInsetPrimary },
                                                               new PaletteMetricPadding[] { PaletteMetricPadding.RibbonButtonPadding },
                                                               new GetToolStripRenderer(_ribbon.CreateToolStripRenderer),
                                                               NeedPaintDelegate);

            // Create the manager for handling tooltips
            _toolTipManager = new ToolTipManager();
            _toolTipManager.ShowToolTip += new EventHandler<ToolTipEventArgs>(OnShowToolTip);
            _toolTipManager.CancelToolTip += new EventHandler(OnCancelToolTip);
            _buttonManager.ToolTipManager = _toolTipManager;
        }

        private void SetupParentMonitoring()
        {
            // We have to know when the parent of the ribbon changes so we can then hook
            // into monitoring the mdi active child status of the top level form, this is
            // required to get the pendant buttons to operate as needed.
            _ribbon.ParentChanged += new EventHandler(OnRibbonParentChanged);

            _invalidateTimer = new Timer();
            _invalidateTimer.Interval = 1;
            _invalidateTimer.Tick += new EventHandler(OnRedrawTick);
        }

        private void OnRibbonParentChanged(object sender, EventArgs e)
        {
            // Unhook from watching any top level window
            if (_formContainer != null)
            {
                _formContainer.Deactivate -= new EventHandler(OnRibbonFormDeactivate);
                _formContainer.Activated -= new EventHandler(OnRibbonFormActivated);
                _formContainer.SizeChanged -= new EventHandler(OnRibbonFormSizeChanged);
                _formContainer.MdiChildActivate -= new EventHandler(OnRibbonMdiChildActivate);
            }

            // Find the new top level form (which might be an mdi container)
            _formContainer = _ribbon.FindForm();

            // Monitor changes in active mdi child
            if (_formContainer != null)
            {
                _formContainer.MdiChildActivate += new EventHandler(OnRibbonMdiChildActivate);
                _formContainer.SizeChanged += new EventHandler(OnRibbonFormSizeChanged);
                _formContainer.Activated += new EventHandler(OnRibbonFormActivated);
                _formContainer.Deactivate += new EventHandler(OnRibbonFormDeactivate);
                _ribbon.UpdateBackStyle();
            }
        }

        private void OnRibbonFormActivated(object sender, EventArgs e)
        {
            _ribbon.ViewRibbonManager.Active();
            _ribbon.UpdateBackStyle();
        }

        private void OnRibbonFormDeactivate(object sender, EventArgs e)
        {
            _ribbon.ViewRibbonManager.Inactive();
            _ribbon.UpdateBackStyle();
        }

        private void OnRibbonFormSizeChanged(object sender, EventArgs e)
        {
            CheckRibbonSize();
        }

        private void OnRibbonMdiChildActivate(object sender, EventArgs e)
        {
            // Cast to correct type
            Form topForm = sender as Form;

            // Unhook from watching any previous mdi child
            if (_activeMdiChild != null)
                _activeMdiChild.SizeChanged -= new EventHandler(OnRibbonMdiChildSizeChanged);

            _activeMdiChild = topForm.ActiveMdiChild;

            // Start watching any new mdi child
            if (_activeMdiChild != null)
                _activeMdiChild.SizeChanged += new EventHandler(OnRibbonMdiChildSizeChanged);

            // Update the pendant buttons with reference to new child
            _buttonSpecClose.MdiChild = _activeMdiChild;
            _buttonSpecRestore.MdiChild = _activeMdiChild;
            _buttonSpecMin.MdiChild = _activeMdiChild;
            _buttonManager.RecreateButtons();
            PerformNeedPaint(true);

            // We never want the mdi child window to have a system menu, we provide the 
            // pendant buttons as part of the ribbon and so replace the need for it.
            PI.SetMenu(new HandleRef(_ribbon, topForm.Handle), NullHandleRef);

            if (_activeMdiChild != null)
            {
                uint windowStyle = PI.GetWindowLong(_activeMdiChild.Handle, -16);
                windowStyle |= PI.WS_SYSMENU;
                PI.SetWindowLong(_activeMdiChild.Handle, -16, windowStyle);
            }
        }

        private void OnRibbonMdiChildSizeChanged(object sender, EventArgs e)
        {
            // Update pendant buttons to reflect new child state
            _buttonManager.RecreateButtons();
            PerformNeedPaint(true);
        }

        private void OnRedrawTick(object sender, EventArgs e)
        {
            _invalidateTimer.Stop();

            if ((_captionArea != null) && 
                (_captionArea.KryptonForm != null) &&
                _captionArea.UsingCustomChrome)
            {
                if (_captionArea.KryptonForm.PaintCount == _paintCount)
                    _captionArea.RedrawCustomChrome(true);
            }
        }

        private void OnAppButtonReleased(object sender, EventArgs e)
        {
            // We do not operate the application button at design time
            if (!_ribbon.InDesignMode)
                TestForAppButtonDoubleClick();
        }

        private void OnAppButtonClicked(object sender, EventArgs e)
        {
            // We do not operate the application button at design time
            if (_ribbon.InDesignMode)
                OnAppMenuDisposed(this, EventArgs.Empty);
            else
            {
                // Give event handler a change to cancel the open request
                CancelEventArgs cea = new CancelEventArgs();
                _ribbon.OnAppButtonMenuOpening(cea);

                if (cea.Cancel)
                    OnAppMenuDisposed(this, EventArgs.Empty);
                else
                {
                    // Remove any minimized popup window from display
                    if (_ribbon.RealMinimizedMode)
                        _ribbon.KillMinimizedPopup();

                    // Give popups a change to cleanup
                    Application.DoEvents();

                    if (!_ribbon.InDesignMode && !_ribbon.IsDisposed)
                    {
                        Rectangle appRectTop;
                        Rectangle appRectBottom;
                        Rectangle appRectShow;

                        if (_ribbon.RibbonShape == PaletteRibbonShape.Office2007)
                        {
                            // Find screen location of the applicaton button lower half
                            Rectangle appButtonRect = _ribbon.RectangleToScreen(_layoutAppButton.AppButton.ClientRectangle);
                            appRectBottom = new Rectangle(appButtonRect.X, appButtonRect.Y + 22, appButtonRect.Width, appButtonRect.Height - 21);
                            appRectTop = new Rectangle(appRectBottom.X, appRectBottom.Y - 21, appRectBottom.Width, 21);
                            appRectShow = appRectBottom;
                        }
                        else
                        {
                            // Find screen location of the applicaton tab lower half
                            Rectangle appButtonRect = _ribbon.RectangleToScreen(_layoutAppTab.AppTab.ClientRectangle);
                            appRectBottom = Rectangle.Empty;
                            appRectTop = appButtonRect;
                            appRectShow = new Rectangle(appButtonRect.X, appButtonRect.Bottom - 1, appButtonRect.Width, 0);
                        }

                        // Create the actual control used to show the context menu
                        _appMenu = new VisualPopupAppMenu(_ribbon, _ribbon.RibbonAppButton,
                                                          _ribbon.Palette, _ribbon.PaletteMode,
                                                          _ribbon.GetRedirector(), 
                                                          appRectTop, appRectBottom,
                                                          _appButtonController.Keyboard);

                        // Need to know when the visual control is removed
                        _appMenu.Disposed += new EventHandler(OnAppMenuDisposed);

                        // Adjust the screen rect of the app button/tab, so we show half way down the button
                        appRectShow.X -= 3;
                        appRectShow.Height = 0;

                        // Request the menu be shown immediately
                        _appMenu.Show(appRectShow);

                        // Indicate the context menu is fully constructed and displayed
                        _ribbon.OnAppButtonMenuOpened(EventArgs.Empty);
                    }
                }
            }
        }

        private void OnAppMenuDisposed(object sender, EventArgs e)
        {
            // We always kill keyboard mode when the app button menu is removed
            _ribbon.KillKeyboardMode();

            // Remove the fixed 'pressed' state from the application button
            _appButtonController.RemoveFixed();
            _appTabController.RemoveFixed();

            // Should still be caching a reference to actual display control
            if (_appMenu != null)
            {
                // Unhook from control, so it can be garbage collected
                _appMenu.Disposed -= new EventHandler(OnAppMenuDisposed);

                // Discover the reason for the menu close
                ToolStripDropDownCloseReason closeReason = ToolStripDropDownCloseReason.AppFocusChange;
                if (_appMenu.CloseReason.HasValue)
                    closeReason = _appMenu.CloseReason.Value;

                // No longer need to cache reference
                _appMenu = null;

                // Notify event handlers the context menu has been closed and why it closed
                _ribbon.OnAppButtonMenuClosed(new ToolStripDropDownClosedEventArgs(closeReason));
            }
        }

        private void OnContextClicked(object sender, MouseEventArgs e)
        {
            if (!_ribbon.InDesignMode)
                _ribbon.DisplayRibbonContextMenu(e);
        }

        private void OnShowToolTip(object sender, ToolTipEventArgs e)
        {
            if (!_ribbon.IsDisposed)
            {
                // Do not show tooltips when the form we are in does not have focus
                Form topForm = _ribbon.FindForm();
                if ((topForm != null) && !topForm.ContainsFocus)
                    return;

                // Never show tooltips are design time
                if (!_ribbon.InDesignMode)
                {
                    IContentValues sourceContent = null;
                    LabelStyle toolTipStyle = LabelStyle.SuperTip;
                    Rectangle screenRect = new Rectangle(e.ScreenPt, new Size(1, 1));

                    // If the target is the application button
                    if ((e.Target is ViewLayoutRibbonAppButton) || (e.Target is ViewLayoutRibbonAppTab))
                    {
                        // Create a content that recovers values from a the ribbon for the app button/tab
                        AppButtonToolTipToContent appButtonContent = new AppButtonToolTipToContent(_ribbon);

                        // Is there actually anything to show for the tooltip
                        if (appButtonContent.HasContent)
                        {
                            sourceContent = appButtonContent;

                            // Grab the style from the app button settings
                            toolTipStyle = _ribbon.RibbonAppButton.AppButtonToolTipStyle;

                            // Display below the mouse cursor
                            screenRect.Height += SystemInformation.CursorSize.Height / 3 * 2;
                        }
                    }
                    else
                    {
                        // If the target is a QAT button
                        if (e.Target is ViewDrawRibbonQATButton)
                        {
                            // Cast to correct type
                            ViewDrawRibbonQATButton viewElement = (ViewDrawRibbonQATButton)e.Target;

                            // Create a content that recovers values from a IQuickAccessToolbarButton
                            QATButtonToolTipToContent qatButtonContent = new QATButtonToolTipToContent(viewElement.QATButton);

                            // Is there actually anything to show for the tooltip
                            if (qatButtonContent.HasContent)
                            {
                                sourceContent = qatButtonContent;

                                // Grab the style from the QAT button settings
                                toolTipStyle = viewElement.QATButton.GetToolTipStyle();

                                // Display below the mouse cursor
                                screenRect.Height += SystemInformation.CursorSize.Height / 3 * 2;
                            }
                        }
                        else
                        {
                            // If the target is a label
                            if ((e.Target.Parent != null) && (e.Target.Parent is ViewDrawRibbonGroupLabel))
                            {
                                // Cast to correct type
                                ViewDrawRibbonGroupLabel viewElement = (ViewDrawRibbonGroupLabel)e.Target.Parent;

                                // Create a content that recovers values from a KryptonRibbonGroupItem
                                GroupItemToolTipToContent groupItemContent = new GroupItemToolTipToContent(viewElement.GroupLabel);

                                // Is there actually anything to show for the tooltip
                                if (groupItemContent.HasContent)
                                {
                                    sourceContent = groupItemContent;

                                    // Grab the style from the group label settings
                                    toolTipStyle = viewElement.GroupLabel.ToolTipStyle;

                                    // Display below the bottom of the ribbon control
                                    Rectangle ribbonScreenRect = _ribbon.ToolTipScreenRectangle;
                                    screenRect.Y = ribbonScreenRect.Y;
                                    screenRect.Height = ribbonScreenRect.Height;
                                    screenRect.X = ribbonScreenRect.X + viewElement.ClientLocation.X;
                                    screenRect.Width = viewElement.ClientWidth;
                                }
                            }
                            else
                            {
                                // Is the target is a button or cluster button
                                if (e.Target is ViewDrawRibbonGroupButtonBackBorder)
                                {
                                    // Cast to correct type
                                    ViewDrawRibbonGroupButtonBackBorder viewElement = (ViewDrawRibbonGroupButtonBackBorder)e.Target;

                                    // Create a content that recovers values from a KryptonRibbonGroupItem
                                    GroupItemToolTipToContent groupItemContent = new GroupItemToolTipToContent(viewElement.GroupItem);

                                    // Is there actually anything to show for the tooltip
                                    if (groupItemContent.HasContent)
                                    {
                                        sourceContent = groupItemContent;

                                        // Grab the style from the group button/group cluster button settings
                                        toolTipStyle = viewElement.GroupItem.InternalToolTipStyle;

                                        // Display below the bottom of the ribbon control
                                        Rectangle ribbonScreenRect = _ribbon.ToolTipScreenRectangle;
                                        screenRect.Y = ribbonScreenRect.Y;
                                        screenRect.Height = ribbonScreenRect.Height;
                                        screenRect.X = ribbonScreenRect.X + viewElement.ClientLocation.X;
                                        screenRect.Width = viewElement.ClientWidth;
                                    }
                                }
                                else
                                {
                                    if (e.Target is ViewLayoutRibbonCheckBox)
                                    {
                                        // Cast to correct type
                                        ViewDrawRibbonGroupCheckBox viewElement = (ViewDrawRibbonGroupCheckBox)e.Target.Parent;

                                        // Create a content that recovers values from a KryptonRibbonGroupItem
                                        GroupItemToolTipToContent groupItemContent = new GroupItemToolTipToContent(viewElement.GroupCheckBox);

                                        // Is there actually anything to show for the tooltip
                                        if (groupItemContent.HasContent)
                                        {
                                            sourceContent = groupItemContent;

                                            // Grab the style from the group check box cluster button settings
                                            toolTipStyle = viewElement.GroupCheckBox.InternalToolTipStyle;

                                            // Display below the bottom of the ribbon control
                                            Rectangle ribbonScreenRect = _ribbon.ToolTipScreenRectangle;
                                            screenRect.Y = ribbonScreenRect.Y;
                                            screenRect.Height = ribbonScreenRect.Height;
                                            screenRect.X = ribbonScreenRect.X + viewElement.ClientLocation.X;
                                            screenRect.Width = viewElement.ClientWidth;
                                        }
                                    }
                                    else
                                    {
                                        if (e.Target is ViewLayoutRibbonRadioButton)
                                        {
                                            // Cast to correct type
                                            ViewDrawRibbonGroupRadioButton viewElement = (ViewDrawRibbonGroupRadioButton)e.Target.Parent;

                                            // Create a content that recovers values from a KryptonRibbonGroupItem
                                            GroupItemToolTipToContent groupItemContent = new GroupItemToolTipToContent(viewElement.GroupRadioButton);

                                            // Is there actually anything to show for the tooltip
                                            if (groupItemContent.HasContent)
                                            {
                                                sourceContent = groupItemContent;

                                                // Grab the style from the group radio button button settings
                                                toolTipStyle = viewElement.GroupRadioButton.InternalToolTipStyle;

                                                // Display below the bottom of the ribbon control
                                                Rectangle ribbonScreenRect = _ribbon.ToolTipScreenRectangle;
                                                screenRect.Y = ribbonScreenRect.Y;
                                                screenRect.Height = ribbonScreenRect.Height;
                                                screenRect.X = ribbonScreenRect.X + viewElement.ClientLocation.X;
                                                screenRect.Width = viewElement.ClientWidth;
                                            }
                                        }
                                        else
                                        {
                                            // Find the button spec associated with the tooltip request
                                            ButtonSpec buttonSpec = _buttonManager.ButtonSpecFromView(e.Target);

                                            // If the tooltip is for a button spec
                                            if (buttonSpec != null)
                                            {
                                                // Are we allowed to show page related tooltips
                                                if (_ribbon.AllowButtonSpecToolTips)
                                                {
                                                    // Create a helper object to provide tooltip values
                                                    ButtonSpecToContent buttonSpecMapping = new ButtonSpecToContent(_ribbon.GetRedirector(), buttonSpec);

                                                    // Is there actually anything to show for the tooltip
                                                    if (buttonSpecMapping.HasContent)
                                                    {
                                                        sourceContent = buttonSpecMapping;

                                                        // Grab the style from the button spec settings
                                                        toolTipStyle = buttonSpec.ToolTipStyle;

                                                        // Display below the mouse cursor
                                                        screenRect.Height += SystemInformation.CursorSize.Height / 3 * 2;
                                                    }
                                                }
                                            }
                                        }
                                    }
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
                        _visualPopupToolTip = new VisualPopupToolTip(_ribbon.GetRedirector(),
                                                                     sourceContent,
                                                                     _ribbon.Renderer,
                                                                     PaletteBackStyle.ControlToolTip,
                                                                     PaletteBorderStyle.ControlToolTip,
                                                                     CommonHelper.ContentStyleFromLabelStyle(toolTipStyle));

                        _visualPopupToolTip.Disposed += new EventHandler(OnVisualPopupToolTipDisposed);

                        // The popup tooltip control always adds on a border above/below so we negate that here.
                        screenRect.Height -= 20;

                        // Show relative to the provided screen rectangle
                        _visualPopupToolTip.ShowCalculatingSize(screenRect);
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

        private void OnVisualPopupToolTipDisposed(object sender, EventArgs e)
        {
            // Unhook events from the specific instance that generated event
            VisualPopupToolTip popupToolTip = (VisualPopupToolTip)sender;
            popupToolTip.Disposed -= new EventHandler(OnVisualPopupToolTipDisposed);

            // Not showing a popup page any more
            _visualPopupToolTip = null;
        }

        private void OnTabsPaintBackground(object sender, PaintEventArgs e)
        {
            if (PaintBackground != null)
                PaintBackground(sender, e);
        }
        #endregion
    }
}
