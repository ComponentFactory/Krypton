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
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class VisualPopupAppMenu : VisualPopup
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private IPalette _palette;
        private IPaletteBack _drawOutsideBack;
        private IPaletteBorder _drawOutsideBorder;
        private AppButtonMenuProvider _provider;
        private ViewDrawRibbonAppMenu _drawOutsideDocker;
        private ViewDrawRibbonAppMenuOuter _drawOutsideBacking;
        private ViewDrawRibbonAppMenuInner _drawInnerBacking;
        private ViewDrawRibbonAppButton _appButtonBottom;
        private ViewLayoutStack _viewColumns;
        private ViewLayoutDocker _viewButtonSpecDocker;
        private PaletteRedirect _redirector;
        private ButtonSpecManagerLayout _buttonManager;
        private Rectangle _rectAppButtonBottomHalf;
        private Rectangle _rectAppButtonTopHalf;
        #endregion

        #region Identity
        /// <summary>
        ///  Initialize a new instance of the VisualPopupAppMenu class.
        /// </summary>
        /// <param name="ribbon">Owning ribbon instance.</param>
        /// <param name="appButton">Originating app button instance.</param>
        /// <param name="palette">Local palette setting to use initially.</param>
        /// <param name="paletteMode">Palette mode setting to use initially.</param>
        /// <param name="redirector">Redirector used for obtaining palette values.</param>
        /// <param name="rectAppButtonTopHalf">Screen location of the upper half of the app button.</param>
        /// <param name="rectAppButtonBottomHalf">Screen location of the lower half of the app button.</param>
        /// <param name="keyboardActivated">Was the context menu activated by a keyboard action.</param>
        public VisualPopupAppMenu(KryptonRibbon ribbon,
                                  RibbonAppButton appButton,
                                  IPalette palette,
                                  PaletteMode paletteMode,
                                  PaletteRedirect redirector,
                                  Rectangle rectAppButtonTopHalf,
                                  Rectangle rectAppButtonBottomHalf,
                                  bool keyboardActivated)
            : base(true)
        {
            // Remember incoming state
            _redirector = redirector;
            _ribbon = ribbon;
            _rectAppButtonTopHalf = rectAppButtonTopHalf;
            _rectAppButtonBottomHalf = rectAppButtonBottomHalf;
            
            // Create the view manager instance with root element
            ViewManager = new ViewContextMenuManager(this, new ViewLayoutNull());

            // Set the initial resolved palette to the appropriate setting
            if (palette != null)
                SetPalette(palette);
            else
                SetPalette(KryptonManager.GetPaletteForMode(paletteMode));

            // Set of context menu columns
            _viewColumns = new ViewLayoutStack(true);

            // Create provider instance
            _provider = new AppButtonMenuProvider((ViewContextMenuManager)ViewManager,
                                                  _ribbon.RibbonAppButton.AppButtonMenuItems,
                                                  _viewColumns, palette, paletteMode, 
                                                  redirector, NeedPaintDelegate);

            _provider.Closing += new CancelEventHandler(OnProviderClosing);
            _provider.Close += new EventHandler<CloseReasonEventArgs>(OnProviderClose);
            _provider.Dispose += new EventHandler(OnProviderClose);

            CreateAppButtonBottom();
            CreateButtonSpecView();
            CreateContextMenuView(appButton);
            CreateRecentDocumentsView();
            CreateInnerBacking(CreateInsideCanvas());
            CreateOuterBacking();
            CreateOutsideDocker();
            CreateButtonManager(appButton);

            ViewManager.Root = _drawOutsideDocker;

            // With keyboard activate we select the first valid item
            if (keyboardActivated)
                ((ViewContextMenuManager)ViewManager).KeyDown();
        }

        private void CreateButtonSpecView()
        {
            // Layout docker used to contain the generated button specs
            _viewButtonSpecDocker = new ViewLayoutDocker();
        }

        private void CreateContextMenuView(RibbonAppButton appButton)
        {
            // Ask the top level collection to generate the child view elements
            KryptonContextMenuCollection topCollection = new KryptonContextMenuCollection();
            KryptonContextMenuItems topItems = new KryptonContextMenuItems();
            topItems.ImageColumn = false;
            topCollection.Add(topItems);
            foreach (KryptonContextMenuItemBase item in appButton.AppButtonMenuItems)
                topItems.Items.Add(item);
            topCollection.GenerateView(_provider, this, _viewColumns, true, true);
        }

        private void CreateRecentDocumentsView()
        {
            // Do we need to add the recent docs view?
            if (_ribbon.RibbonAppButton.AppButtonShowRecentDocs)
            {
                // Create a dummy vertical menu separator for separating recent documents from menu items
                KryptonContextMenuSeparator dummySep1 = new KryptonContextMenuSeparator();
                dummySep1.Horizontal = false;
                _viewColumns.Add(new ViewDrawMenuSeparator(dummySep1, _provider.ProviderStateCommon.Separator));
                _viewColumns.Add(new ViewLayoutSeparator(0, _ribbon.RibbonAppButton.AppButtonMinRecentSize.Height));

                // Use a layout that draws the background color of the recent docs area
                ViewDrawRibbonAppMenuDocs recentDocsBack = new ViewDrawRibbonAppMenuDocs(_ribbon);
                _viewColumns.Add(recentDocsBack);

                // Stack the document entries vertically
                ViewLayoutStack documentStack = new ViewLayoutStack(false);
                recentDocsBack.Add(documentStack);

                // Use fixed width separator to enforce a minimum width to column
                documentStack.Add(new ViewLayoutSeparator(_ribbon.RibbonAppButton.AppButtonMinRecentSize.Width, 0));

                // Add the recent document title
                documentStack.Add(new ViewDrawRibbonRecentDocs(_ribbon));

                // Followed by a horizontal separator
                KryptonContextMenuSeparator dummySep2 = new KryptonContextMenuSeparator();
                documentStack.Add(new ViewDrawMenuSeparator(dummySep2, _provider.ProviderStateCommon.Separator));
                documentStack.Add(new ViewLayoutSeparator(2));

                // Then generate an item per recent document entry
                int index = 1;
                foreach (KryptonRibbonRecentDoc recentDoc in _ribbon.RibbonAppButton.AppButtonRecentDocs)
                    documentStack.Add(new ViewDrawRibbonAppMenuRecentDec(_ribbon, _provider, recentDoc, _ribbon.RibbonAppButton.AppButtonMaxRecentSize.Width, NeedPaintDelegate, index++));

                // Add separator entry which is then used to fill remained space
                documentStack.Add(new ViewLayoutSeparator(1));

                // Update provider with element to use as the fixed size for submenus
                _provider.FixedViewBase = recentDocsBack;
            }
        }

        private ViewDrawCanvas CreateInsideCanvas()
        {
            ViewDrawCanvas mainBackground = new ViewDrawCanvas(_provider.ProviderStateCommon.ControlInner.Back, _provider.ProviderStateCommon.ControlInner.Border, VisualOrientation.Top);
            mainBackground.Add(_viewColumns);
            mainBackground.KeyController = new ContextMenuController((ViewContextMenuManager)ViewManager);
            return mainBackground;
        }

        private void CreateInnerBacking(ViewBase fillElement)
        {
            _drawInnerBacking = new ViewDrawRibbonAppMenuInner(_ribbon);
            _drawInnerBacking.Add(new ViewLayoutSeparator(2), ViewDockStyle.Top);
            _drawInnerBacking.Add(new ViewLayoutSeparator(2), ViewDockStyle.Bottom);
            _drawInnerBacking.Add(new ViewLayoutSeparator(2), ViewDockStyle.Left);
            _drawInnerBacking.Add(new ViewLayoutSeparator(2), ViewDockStyle.Right);
            _drawInnerBacking.Add(fillElement, ViewDockStyle.Fill);
        }

        private void CreateOuterBacking()
        {
            _drawOutsideBacking = new ViewDrawRibbonAppMenuOuter(_ribbon);
            _drawOutsideBacking.Add(_drawInnerBacking, ViewDockStyle.Fill);
            _drawOutsideBacking.Add(new ViewLayoutSeparator(14), ViewDockStyle.Top);
            _drawOutsideBacking.Add(new ViewLayoutSeparator(2), ViewDockStyle.Left);
            _drawOutsideBacking.Add(new ViewLayoutSeparator(2), ViewDockStyle.Right);
            _drawOutsideBacking.Add(new ViewLayoutSeparator(2), ViewDockStyle.Bottom);
            _drawOutsideBacking.Add(_viewButtonSpecDocker, ViewDockStyle.Bottom);
            _drawOutsideBacking.Add(new ViewLayoutSeparator(2), ViewDockStyle.Bottom);
        }

        private void CreateAppButtonBottom()
        {
            _appButtonBottom = new ViewDrawRibbonAppButton(_ribbon, true);
            _appButtonBottom.ElementState = PaletteState.Pressed;

            if (_ribbon.RibbonShape == PaletteRibbonShape.Office2010)
                _appButtonBottom.Visible = false;
        }

        private void CreateOutsideDocker()
        {
            _drawOutsideBack = new PaletteBackToPalette(_redirector, PaletteBackStyle.ControlRibbonAppMenu);
            _drawOutsideBorder = new PaletteBorderToPalette(_redirector, PaletteBorderStyle.ControlRibbonAppMenu);
            _drawOutsideDocker = new ViewDrawRibbonAppMenu(_drawOutsideBack, _drawOutsideBorder, _appButtonBottom, _rectAppButtonBottomHalf);
            _drawOutsideDocker.KeyController = new ContextMenuController((ViewContextMenuManager)ViewManager);
            _drawOutsideDocker.Add(_drawOutsideBacking, ViewDockStyle.Fill);
        }

        private void CreateButtonManager(RibbonAppButton appButton)
        {
            _buttonManager = new ButtonSpecManagerLayoutAppButton((ViewContextMenuManager)ViewManager,
                                                                  this, _redirector, appButton.AppButtonSpecs, null,
                                                                  new ViewLayoutDocker[] { _viewButtonSpecDocker },
                                                                  new IPaletteMetric[] { _ribbon.StateCommon },
                                                                  new PaletteMetricInt[] { PaletteMetricInt.None },
                                                                  new PaletteMetricPadding[] { PaletteMetricPadding.RibbonAppButton },
                                                                  new GetToolStripRenderer(CreateToolStripRenderer),
                                                                  new NeedPaintHandler(OnButtonSpecPaint));

            _buttonManager.RecreateButtons();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Must unhook from the palette paint event
                if (_palette != null)
                {
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPaletteNeedPaint);
                    _palette.BasePaletteChanged -= new EventHandler(OnBaseChanged);
                    _palette.BaseRendererChanged -= new EventHandler(OnBaseChanged);
                }

                if (_buttonManager != null)
                {
                    _buttonManager.Destruct();
                    _buttonManager = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Public
        /// <summary>
        /// Sets the reason for the context menu being closed.
        /// </summary>
        public Nullable<ToolStripDropDownCloseReason> CloseReason 
        {
            get { return _provider.ProviderCloseReason; }
        }

        /// <summary>
        /// Show the context menu relative to the provided screen rectangle.
        /// </summary>
        /// <param name="screenRect">Screen rectangle.</param>
        public override void Show(Rectangle screenRect)
        {
            // Find the preferred size of the context menu if it could be any size it likes
            Size preferredSize = CalculatePreferredSize();
            Rectangle preferredRect = new Rectangle(screenRect.Location, preferredSize);

            // Get the working area of the monitor that most of the screen rectangle is inside
            Rectangle workingArea = Screen.GetWorkingArea(preferredRect);

            // Limit size of context menu to the working area
            preferredSize.Width = Math.Min(workingArea.Width, preferredSize.Width);
            preferredSize.Height = Math.Min(workingArea.Height, preferredSize.Height);

            Point screenPt = Point.Empty;

            // Find the horizontal position relative to screen rectangle
            screenPt.X = screenRect.Left;
            screenPt.Y = screenRect.Bottom;

            // Limit location of context menu to the working area
            screenPt.X = Math.Max(screenPt.X, workingArea.X);
            screenPt.Y = Math.Max(screenPt.Y, workingArea.Y);

            if ((screenPt.X + preferredSize.Width) > workingArea.Right)
                screenPt.X = workingArea.Right - preferredSize.Width;

            if ((screenPt.Y + preferredSize.Height) > workingArea.Bottom)
                screenPt.Y = workingArea.Bottom - preferredSize.Height;

            // Call base class method that performs actual sizing and display of control
            base.Show(new Rectangle(screenPt, preferredSize));
        }

        /// <summary>
        /// Gets and sets the horizontal setting used to position the menu.
        /// </summary>
        public KryptonContextMenuPositionH ShowHorz
        {
            get { return _provider.ProviderShowHorz; }
            set { _provider.ProviderShowHorz = value; }
        }

        /// <summary>
        /// Gets and sets the vertical setting used to position the menu.
        /// </summary>
        public KryptonContextMenuPositionV ShowVert
        {
            get { return _provider.ProviderShowVert; }
            set { _provider.ProviderShowVert = value; }
        }

        /// <summary>
        /// Gets access to the view manager for the context menu.
        /// </summary>
        public ViewContextMenuManager ViewContextMenuManager
        {
            get { return (ViewContextMenuManager)ViewManager; }
        }

        /// <summary>
        /// Should a mouse down at the provided point cause an end to popup tracking.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to end tracking; otherwise false.</returns>
        public override bool DoesCurrentMouseDownEndAllTracking(Message m, Point pt)
        {
            if (_appButtonBottom.Visible)
            {
                // If mouse is over the top half of the button (which is in the non-client area of
                // the owner ribbon) then we do not want to end all tracking from this method. Otherwise
                // if the mouse is inside the bottom half of the button then we do end tracking!
                if (RectangleToClient(_rectAppButtonTopHalf).Contains(pt))
                    return false;
                else if (ClientRectangle.Contains(pt) && _appButtonBottom.ClientRectangle.Contains(pt))
                    return true;
            }

            return base.DoesCurrentMouseDownEndAllTracking(m, pt);
        }

        /// <summary>
        /// Should a mouse down at the provided point cause it to become the current tracking popup.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Client coordinates point.</param>
        /// <returns>True to become current; otherwise false.</returns>
        public override bool DoesStackedClientMouseDownBecomeCurrent(Message m, Point pt)
        {
            // Is this mouse over the application button area?
            if (ClientRectangle.Contains(pt) && _appButtonBottom.Visible && _appButtonBottom.ClientRectangle.Contains(pt))
                return false;
            else
                return ViewContextMenuManager.DoesStackedClientMouseDownBecomeCurrent(m, pt);
        }

        /// <summary>
        /// Should the mouse down be eaten when the tracking has been ended.
        /// </summary>
        /// <param name="m">Original message.</param>
        /// <param name="pt">Screen coordinates point.</param>
        /// <returns>True to eat message; otherwise false.</returns>
        public override bool DoesMouseDownGetEaten(Message m, Point pt)
        {
            // Is the point inside the area of the application button
            if (_rectAppButtonTopHalf.Contains(pt))
            {
                VisualPopupManager.Singleton.EndAllTracking();
                return true;
            }
            else
                return base.DoesMouseDownGetEaten(m, pt);
        }

        /// <summary>
        /// Gets the resolved palette to actually use when drawing.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override IPalette GetResolvedPalette()
        {
            return _palette;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets access to the palette redirector.
        /// </summary>
        protected PaletteRedirect Redirector
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _redirector; }
        }

        /// <summary>
        /// Raises the KeyDown event.
        /// </summary>
        /// <param name="e">A KeyEventArgs that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Cannot process a message for a disposed control
            if (!IsDisposed)
            {
                // If the user pressed the escape key
                if (e.KeyData == Keys.Escape)
                {
                    // Using the escape key should give a close reason of keyboard
                    _provider.ProviderCloseReason = ToolStripDropDownCloseReason.Keyboard;                    
                }
            }

            // Let base class fire events
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Raises the Layout event.
        /// </summary>
        /// <param name="levent">An EventArgs that contains the event data.</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            // Let base class calulcate fill rectangle
            base.OnLayout(levent);

            // Need a render context for accessing the renderer
            using (RenderContext context = new RenderContext(this, null, ClientRectangle, Renderer))
            {

                // Grab a path that is the outside edge of the border
                Rectangle borderRect = ClientRectangle;
                GraphicsPath borderPath1 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _drawOutsideBorder, VisualOrientation.Top, PaletteState.Normal);
                borderRect.Inflate(-1, -1);
                GraphicsPath borderPath2 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _drawOutsideBorder, VisualOrientation.Top, PaletteState.Normal);
                borderRect.Inflate(-1, -1);
                GraphicsPath borderPath3 = Renderer.RenderStandardBorder.GetOutsideBorderPath(context, borderRect, _drawOutsideBorder, VisualOrientation.Top, PaletteState.Normal);

                // Update the region of the popup to be the border path
                Region = new Region(borderPath1);

                // Inform the shadow to use the same paths for drawing the shadow
                DefineShadowPaths(borderPath1, borderPath2, borderPath3);
            }
        }

        /// <summary>
        /// Processes a notification from palette of a paint and optional layout required.
        /// </summary>
        /// <param name="sender">Source of notification.</param>
        /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
        protected virtual void OnPaletteNeedPaint(object sender, NeedLayoutEventArgs e)
        {
            // Need to recalculate anything relying on the palette
            OnNeedPaint(sender, e);
        }
        #endregion

        #region Implementation
        private Size CalculatePreferredSize()
        {
            // Prevent calculation of layout from performing a recursive layout
            SuspendLayout();

            try
            {
                // Find the preferred size which fits exactly the calculated contents size
                using (ViewLayoutContext context = new ViewLayoutContext(this, Renderer))
                    return ViewManager.Root.GetPreferredSize(context);
            }
            finally
            {
                // Must always have matching suspend/resume
                ResumeLayout();
            }
        }

        private void SetPalette(IPalette palette)
        {
            if (palette != _palette)
            {
                // Unhook from current palette events
                if (_palette != null)
                {
                    _palette.PalettePaint -= new EventHandler<PaletteLayoutEventArgs>(OnPaletteNeedPaint);
                    _palette.BasePaletteChanged -= new EventHandler(OnBaseChanged);
                    _palette.BaseRendererChanged -= new EventHandler(OnBaseChanged);
                }

                // Remember the new palette
                _palette = palette;

                // Update redirector to use palette as source for obtaining values
                _redirector.Target = _palette;

                // Get the renderer associated with the palette
                Renderer = _palette.GetRenderer();

                // Hook to new palette events
                if (_palette != null)
                {
                    _palette.PalettePaint += new EventHandler<PaletteLayoutEventArgs>(OnPaletteNeedPaint);
                    _palette.BasePaletteChanged += new EventHandler(OnBaseChanged);
                    _palette.BaseRendererChanged += new EventHandler(OnBaseChanged);
                }
            }
        }

        private void OnBaseChanged(object sender, EventArgs e)
        {
            // Change in base renderer or base palette require we fetch the latest renderer
            Renderer = _palette.GetRenderer();
        }

        private void OnButtonSpecPaint(object sender, NeedLayoutEventArgs e)
        {
            OnNeedPaint(sender, new NeedLayoutEventArgs(false));
        }

        private void OnProviderClosing(object sender, CancelEventArgs e)
        {
            if (_ribbon != null)
                _ribbon.OnAppButtonMenuClosing(e);
        }

        private void OnProviderClose(object sender, CloseReasonEventArgs e)
        {
            // Remove ourself from being shown
            VisualPopupManager.Singleton.EndPopupTracking(this);
        }

        private void OnProviderClose(object sender, EventArgs e)
        {
            // Unhook from event source
            IContextMenuProvider provider = (IContextMenuProvider)sender;
            _provider.Dispose -= new EventHandler(OnProviderClose);

            // Kill this poup window
            Dispose();
        }
        #endregion
    }
}
