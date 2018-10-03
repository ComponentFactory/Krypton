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
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Ribbon area that contains the context titles, minibar and top half of application button.
    /// </summary>
    internal class ViewDrawRibbonCaptionArea : ViewDrawDocker
    {
        #region Static Fields
        private static readonly int MIN_INTEGRATED_HEIGHT = 26;    // MiniBar, 16 image + 2 * (2 gap + 1 border + 2 border) 
        private static readonly int CAPTION_TEXT_GAPS = 10;        // 4 below and 6 above
        private static readonly int MIN_SELF_HEIGHT = 28;          // Min height to show application button and the mini bar and context tabs
        #endregion

        #region Instance Fields
        private KryptonRibbon _ribbon;
        private NeedPaintHandler _needPaintDelegate;
        private NeedPaintHandler _needIntegratedDelegate;
        private PaletteCaptionRedirect _redirect;
        private PaletteDoubleRedirect _redirectCaption;
        private ViewDrawRibbonComposition _compositionArea;
        private ViewLayoutRibbonAppButton _captionAppButton;
        private ViewLayoutRibbonAppButton _otherAppButton;
        private ViewLayoutSeparator _spaceInsteadOfAppButton;
        private ViewLayoutRibbonQATMini _captionQAT;
        private ViewLayoutRibbonQATMini _nonCaptionQAT;
        private ViewLayoutRibbonContextTitles _contextTiles;
        private ViewDrawRibbonCompoRightBorder _compRightBorder;
        private AppButtonController _appButtonController;
        private AppTabController _appTabController;
        private KryptonForm _kryptonForm;
        private bool _integrated;
        private bool _preventIntegration;
        private bool _compoRightInjected;
        private int _calculatedHeight;
        private Font _cacheRibbonFont;
        private int _cacheRibbonFontHeight;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonCaptionArea class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        /// <param name="redirect">Reference to redirector for palette settings.</param>
        /// <param name="compositionArea">Reference to the composition element.</param>
        /// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
        public ViewDrawRibbonCaptionArea(KryptonRibbon ribbon,
                                         PaletteRedirect redirect,
                                         ViewDrawRibbonComposition compositionArea,
                                         NeedPaintHandler needPaintDelegate)
        {
            Debug.Assert(ribbon != null);
            Debug.Assert(redirect != null);
            Debug.Assert(compositionArea != null);
            Debug.Assert(needPaintDelegate != null);

            // Remember incoming references
            _ribbon = ribbon;
            _compositionArea = compositionArea;
            _needPaintDelegate = needPaintDelegate;
            _needIntegratedDelegate = new NeedPaintHandler(OnIntegratedNeedPaint);

            // Create a special redirector for overriding the border setting
            _redirect = new PaletteCaptionRedirect(redirect);

            CreateViewElements();
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
                // Unhook from any current krypton form monitoring
                if (_kryptonForm != null)
                {
                    // Remove our elements from the custom chrome
                    if (_integrated)
                    {
                        _captionAppButton.OwnerForm = null;
                        _kryptonForm.AllowIconDisplay = true;
                        _kryptonForm.RevokeViewElement(_contextTiles, ViewDockStyle.Fill);
                        _kryptonForm.RevokeViewElement(_captionAppButton, ViewDockStyle.Left);
                        _kryptonForm.RevokeViewElement(_captionQAT, ViewDockStyle.Left);
                        _integrated = false;
                    }

                    _kryptonForm.ApplyCustomChromeChanged -= new EventHandler(OnFormChromeCheck);
                    _kryptonForm.ClientSizeChanged -= new EventHandler(OnFormChromeCheck);
                    _kryptonForm.WindowActiveChanged -= new EventHandler(OnWindowActiveChanged);
                    _kryptonForm = null;
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
            return "ViewDrawRibbonCaptionArea:" + Id;
        }
        #endregion

        #region AppButtonController
        /// <summary>
        /// Gets the single reference to the application button controller.
        /// </summary>
        public AppButtonController AppButtonController
        {
            get { return _appButtonController; }
        }
        #endregion

        #region AppTabController
        /// <summary>
        /// Gets the single reference to the application tab controller.
        /// </summary>
        public AppTabController AppTabController
        {
            get { return _appTabController; }
        }
        #endregion

        #region HookToolTipHandling
        /// <summary>
        /// Perform steps to generate a tooltip event when mouse is over the application button.
        /// </summary>
        public void HookToolTipHandling()
        {
            _captionAppButton.MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager, _captionAppButton, _captionAppButton.MouseController);
            _otherAppButton.MouseController = new ToolTipController(_ribbon.TabsArea.ButtonSpecManager.ToolTipManager, _otherAppButton, _otherAppButton.MouseController);
        }
        #endregion

        #region PreventIntegration
        /// <summary>
        /// Gets and sets the integration override value.
        /// </summary>
        public bool PreventIntegration
        {
            get { return _preventIntegration; }

            set
            {
                if (_preventIntegration != value)
                {
                    // Store new override value
                    _preventIntegration = value;

                    // Request the integration check be reapplied
                    OnFormChromeCheck(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region DrawBorderLast
        /// <summary>
        /// Gets the drawing of the border before or after children.
        /// </summary>
        public override bool DrawBorderLast
        {
            get
            {
                // We need to draw the border before contents, so that the application button
                // and any context information draw over the top of the border
                return false;
            }
        }
        #endregion

        #region AppButtonChanged
        /// <summary>
        /// Notify a change in the application button image.
        /// </summary>
        public void AppButtonChanged()
        {
            // Requests a repaint to show the change.
            OnAppButtonNeedPaint(this, new NeedLayoutEventArgs(false));
        }
        #endregion

        #region AppButtonChanged
        /// <summary>
        /// Update the visible state of the caption area based on integration, app button, contexts and qat location.
        /// </summary>
        public void UpdateVisible()
        {
            Visible = !_integrated &&
                      (_ribbon.RibbonAppButton.AppButtonVisible ||
                       (_ribbon.QATLocation == QATLocation.Above) ||
                       (_ribbon.RibbonContexts.Count > 0));
        }
        #endregion

        #region VisibleQAT
        /// <summary>
        /// Get the quick access toolbar view that is currently visible
        /// </summary>
        public ViewLayoutRibbonQATMini VisibleQAT
        {
            get
            {
                if (_integrated)
                    return _captionQAT;
                else
                    return _nonCaptionQAT;
            }
        }
        #endregion

        #region UpdateQAT
        /// <summary>
        /// Update display elements to reflect latest QAT setting.
        /// </summary>
        public void UpdateQAT()
        {
            bool before = _captionQAT.Visible;
            _captionQAT.Visible = _ribbon.Visible && (_ribbon.QATLocation == QATLocation.Above);
            _nonCaptionQAT.Visible = _ribbon.Visible && (_ribbon.QATLocation == QATLocation.Above);
            UpdateVisible();

            // A change in integrated caption visibility
            if (before != _captionQAT.Visible)
                QATButtonsChanged();
        }
        #endregion

        #region AppButtonVisibleChanged
        /// <summary>
        /// A change in the app button visibility needs to be processed.
        /// </summary>
        public void AppButtonVisibleChanged()
        {
            bool appButtonVisible = (_ribbon.RibbonAppButton.AppButtonVisible && (_ribbon.RibbonShape == PaletteRibbonShape.Office2007));
            if (_captionAppButton.Visible != appButtonVisible)
            {
                // Update visible state of the app button to reflect current state
                _captionAppButton.Visible = appButtonVisible;
                _spaceInsteadOfAppButton.Visible = !_captionAppButton.Visible;
                _otherAppButton.Visible = _captionAppButton.Visible;
                _captionQAT.OverlapAppButton = _captionAppButton.Visible;
                _nonCaptionQAT.OverlapAppButton = _captionAppButton.Visible;
                UpdateVisible();

                // Relayout and redraw to show the change
                OnAppButtonNeedPaint(this, new NeedLayoutEventArgs(true));
            }
        }
        #endregion

        #region QATButtonsChanged
        /// <summary>
        /// Notification that the collection of QAT buttons has changed.
        /// </summary>
        public void QATButtonsChanged()
        {
            // Do we need to layout and paint the custom chrome?
            if (UsingCustomChrome)
                OnIntegratedNeedPaint(this, new NeedLayoutEventArgs(true));
        }
        #endregion

        #region UsingCustomChrome
        /// <summary>
        /// Gets a value indicating if the ribbon is integrated into the custom chrome.
        /// </summary>
        public bool UsingCustomChrome
        {
            get { return _integrated; }
        }
        #endregion

        #region RedrawCustomChrome
        /// <summary>
        /// Causes the custom chrome to be repainted.
        /// </summary>
        /// <param name="layout">Is a layout required.</param>
        public void RedrawCustomChrome(bool layout)
        {
            if (UsingCustomChrome)
                _kryptonForm.PerformNeedPaint(layout);
        }
        #endregion

        #region DrawCaptionOnComposition
        /// <summary>
        /// Gets a value indicating if drawing on the composition element.
        /// </summary>
        public bool DrawCaptionOnComposition
        {
            get { return UsingCustomChrome && KryptonForm.ApplyComposition; }
        }
        #endregion

        #region KryptonForm
        /// <summary>
        /// Gets access to the integration form.
        /// </summary>
        public KryptonForm KryptonForm
        {
            get { return _kryptonForm; }
        }
        #endregion

        #region RealWindowBorders
        /// <summary>
        /// Gets the window borders of the krypton form.
        /// </summary>
        public Padding RealWindowBorders
        {
            get
            {
                if (_kryptonForm != null)
                    return _kryptonForm.RealWindowBorders;
                else
                    return Padding.Empty;
            }
        }

        #endregion

        #region ContextTitles
        /// <summary>
        /// Gets access to the layout view used for the context titles.
        /// </summary>
        public ViewLayoutRibbonContextTitles ContextTitles
        {
            get { return _contextTiles; }
        }
        #endregion

        #region PerformFormChromeCheck
        /// <summary>
        /// Redecide if the custom chrome and integration can occur.
        /// </summary>
        public void PerformFormChromeCheck()
        {
            // Update decision about integrating or providing caption functionality
            OnFormChromeCheck(null, EventArgs.Empty);
        }
        #endregion

        #region DoesClientMouseDownEndAllTracking
        /// <summary>
        /// Should a mouse down at the provided point cause an end to popup tracking.
        /// </summary>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to end tracking; otherwise false.</returns>
        public bool DoesCurrentMouseDownEndAllTracking(Point pt)
        {
            // If integrated into custom chrome...
            if (UsingCustomChrome)
            {
                // Convert point to the form coordinates
                Point formPt = _kryptonForm.PointToClient(pt);
                Padding formPadding =_kryptonForm.RealWindowBorders;
                formPt.X += formPadding.Left;
                formPt.Y += formPadding.Top;

                if (ContextTitles != null)
                {
                    // Search the context title elements for a match
                    foreach (ViewBase child in ContextTitles)
                        if ((child is ViewDrawRibbonContextTitle) && child.ClientRectangle.Contains(formPt))
                            return false;
                }
            }

            return true;
        }
        #endregion

        #region Layout
        /// <summary>
        /// Discover the preferred size of the element.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Enforce the minimum height
            Size preferredSize = base.GetPreferredSize(context);
            preferredSize.Height = Math.Max(_calculatedHeight, preferredSize.Height);

            return preferredSize;
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
        private void CreateViewElements()
        {
            // Create redirector for the accessing the background palette
            _redirectCaption =  new PaletteDoubleRedirect(_redirect, PaletteBackStyle.HeaderForm, PaletteBorderStyle.HeaderForm, NeedPaintDelegate);

            // Create a top half for use in KryptonForm and another for use inside this caption area
            _captionAppButton = new ViewLayoutRibbonAppButton(_ribbon, false);
            _otherAppButton = new ViewLayoutRibbonAppButton(_ribbon, false);

            // Connect up the application button controller to the two button elements
            _appButtonController = new AppButtonController(_ribbon);
            _appButtonController.Target1 = _captionAppButton.AppButton;
            _appButtonController.Target2 = _otherAppButton.AppButton;
            _appButtonController.NeedPaint += new NeedPaintHandler(OnAppButtonNeedPaint);
            _captionAppButton.MouseController = _appButtonController;
            _otherAppButton.MouseController = _appButtonController;
            _appTabController = new AppTabController(_ribbon);
            _appTabController.NeedPaint += new NeedPaintHandler(OnAppButtonNeedPaint);

            // When not showing the app button we show this spacer instead
            _spaceInsteadOfAppButton = new ViewLayoutSeparator(0);
            _spaceInsteadOfAppButton.Visible = false;

            // Quick access toolbar, minibar versions
            _captionQAT = new ViewLayoutRibbonQATMini(_ribbon, _needIntegratedDelegate);
            _nonCaptionQAT = new ViewLayoutRibbonQATMini(_ribbon, NeedPaintDelegate);
            
            // Layout needed to position and draw the context titles
            _contextTiles = new ViewLayoutRibbonContextTitles(_ribbon, this);
            _contextTiles.ReverseRenderOrder = true;

            // Create composition right border and attach to composition area
            _compRightBorder = new ViewDrawRibbonCompoRightBorder();
            _compositionArea.CompRightBorder = _compRightBorder;

            // Place app button on left side and fill remainder with context titles
            Add(_contextTiles, ViewDockStyle.Fill);
            Add(_nonCaptionQAT, ViewDockStyle.Left);
            Add(_otherAppButton, ViewDockStyle.Left);

            // Update base class to use correct palette interface
            base.SetPalettes(_redirectCaption.PaletteBack, _redirectCaption.PaletteBorder);
        }

        private void SetupParentMonitoring()
        {
            // We have to know when the parent of the ribbon changes so we can then hook
            // into monitoring the top level custom chrome control. We need information this
            // decide if we integrate with top chrome or show this control instead.
            _ribbon.ParentChanged += new EventHandler(OnRibbonParentChanged);
        }

        private void OnRibbonParentChanged(object sender, EventArgs e)
        {
            // Unhook from any current krypton form monitoring
            if (_kryptonForm != null)
            {
                _kryptonForm.ApplyCustomChromeChanged -= new EventHandler(OnFormChromeCheck);
                _kryptonForm.ClientSizeChanged -= new EventHandler(OnFormChromeCheck);
                _kryptonForm.WindowActiveChanged -= new EventHandler(OnWindowActiveChanged);
                _kryptonForm = null;
            }

            if (!_ribbon.IsDisposed && !_ribbon.Disposing)
            {
                // Find the new owning level form we are hosted inside
                Form ownerForm = _ribbon.Parent as Form;

                // Should always be inside a form, but you never know!
                if (ownerForm != null)
                {
                    // We only care if the owner form is a KryptonForm instance
                    if (ownerForm is KryptonForm)
                    {
                        _kryptonForm = ownerForm as KryptonForm;
                        _kryptonForm.Composition = _compositionArea;
                        _kryptonForm.ApplyCustomChromeChanged += new EventHandler(OnFormChromeCheck);
                        _kryptonForm.ClientSizeChanged += new EventHandler(OnFormChromeCheck);
                        _kryptonForm.WindowActiveChanged += new EventHandler(OnWindowActiveChanged);
                    }
                }

                // Update decision about integrating or providing caption functionality
                OnFormChromeCheck(null, EventArgs.Empty);
            }
        }

        private void OnFormChromeCheck(object sender, EventArgs e)
        {
            bool needLayout = false;
            bool integrated = false;

            // Are we inside a KryptonForm instance that is using custom chrome?
            if ((_kryptonForm != null) && _kryptonForm.ApplyCustomChrome)
            {
                // Ribbon must be placed at the top left of the forms client area
                if (_ribbon.Location == Point.Empty)
                {
                    // Find the height of the top caption area for the form
                    int height = _kryptonForm.RealWindowBorders.Top;

                    // Must be at least the minimum for the application button and spacing gap above it
                    if (height >= MIN_INTEGRATED_HEIGHT)
                        integrated = true;

                    // Update width of the separator used in place of the app button when app button not visible
                    _spaceInsteadOfAppButton.SeparatorSize = new Size(_kryptonForm.RealWindowBorders.Left, 0);
                }
            }

            if (_kryptonForm != null)
            {
                bool overrideIntegrated = integrated;

                // If told to prevent the integration, then prevent it now
                if (PreventIntegration && overrideIntegrated)
                    overrideIntegrated = false;

                // Is there a change in integrated requirements?
                if (overrideIntegrated != _integrated)
                {
                    // Do we need to inject our application button into the caption?
                    if (!_integrated)
                    {
                        _captionAppButton.OwnerForm = _kryptonForm;
                        _captionQAT.OwnerForm = _kryptonForm;
                        _kryptonForm.InjectViewElement(_captionQAT, ViewDockStyle.Left);
                        _kryptonForm.InjectViewElement(_spaceInsteadOfAppButton, ViewDockStyle.Left);
                        _kryptonForm.InjectViewElement(_captionAppButton, ViewDockStyle.Left);

                        // Only inject if not already present
                        if (!_compoRightInjected)
                        {
                            _kryptonForm.InjectViewElement(_compRightBorder, ViewDockStyle.Right);
                            _compoRightInjected = true;
                        }

                        _kryptonForm.InjectViewElement(_contextTiles, ViewDockStyle.Fill);
                    }
                    else
                    {
                        _captionAppButton.OwnerForm = null;
                        _captionQAT.OwnerForm = null;
                        _kryptonForm.RevokeViewElement(_contextTiles, ViewDockStyle.Fill);

                        // At runtime under vista we do not remove the compo right border
                        if (_ribbon.InDesignMode || 
                            (Environment.OSVersion.Version.Major < 6) || 
                            !DWM.IsCompositionEnabled)
                        {
                            _kryptonForm.RevokeViewElement(_compRightBorder, ViewDockStyle.Right);
                            _compoRightInjected = true;
                        }

                        _kryptonForm.RevokeViewElement(_captionAppButton, ViewDockStyle.Left);
                        _kryptonForm.RevokeViewElement(_spaceInsteadOfAppButton, ViewDockStyle.Left);
                        _kryptonForm.RevokeViewElement(_captionQAT, ViewDockStyle.Left);
                    }

                    _integrated = overrideIntegrated;
                    UpdateVisible();
                    needLayout = true;
                }

                _kryptonForm.AllowComposition = _ribbon.AllowFormIntegrate && !_ribbon.InDesignMode;
                
                bool newAllowIconDisplay = (!_integrated || !_ribbon.RibbonAppButton.AppButtonVisible);
                if (_kryptonForm.AllowIconDisplay != newAllowIconDisplay)
                {
                    _kryptonForm.AllowIconDisplay = newAllowIconDisplay;
                    needLayout = true;
                }
            }

            // If not integrated
            if (!_integrated)
            {
                // Get the font we used to draw the context tab text
                Font ribbonFont = _ribbon.StateCommon.RibbonGeneral.GetRibbonTextFont(PaletteState.Normal);

                // Can we use the cached font height?
                if (ribbonFont != _cacheRibbonFont)
                {
                    _cacheRibbonFont = ribbonFont;
                    _cacheRibbonFontHeight = ribbonFont.Height;
                }
             
                // Calculate the desired height of our own area
                int calculatedHeight = Math.Max(_cacheRibbonFontHeight + CAPTION_TEXT_GAPS, MIN_SELF_HEIGHT);

                // If a change in desired height then request layout to effect change
                if (_calculatedHeight != calculatedHeight)
                {
                    _calculatedHeight = calculatedHeight;
                    needLayout = true;
                }
            }

            if (needLayout)
            {
                PerformNeedPaint(true);

                if (_kryptonForm != null)
                {
                    _kryptonForm.RecreateMinMaxCloseButtons();
                    _kryptonForm.PerformNeedPaint(true);
                }
            }
        }

        private void OnWindowActiveChanged(object sender, EventArgs e)
        {
            if (_kryptonForm != null)
            {
                // When integrated into composition we need to repaint whenever the
                // owning form changes active status, as we are drawing the caption
                if (_kryptonForm.ApplyCustomChrome && _kryptonForm.ApplyComposition)
                    PerformNeedPaint(true);
            }
        }

        private void OnAppButtonNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // Redraw the ribbon control to show change
            PerformNeedPaint(e.NeedLayout);
            _ribbon.Refresh();

            // If we have integrated the button into the custom chrome caption area
            if (_integrated)
                _kryptonForm.PerformNeedPaint(e.NeedLayout);
        }

        private void OnIntegratedNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // If we have integrated the button into the custom chrome caption area
            if (_integrated)
                _kryptonForm.PerformNeedPaint(e.NeedLayout);
        }
        #endregion
    }
}
