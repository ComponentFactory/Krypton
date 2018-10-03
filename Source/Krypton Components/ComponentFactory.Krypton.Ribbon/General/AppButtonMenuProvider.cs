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
using System.Windows.Forms;
using System.ComponentModel;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Implementation class used to provide application button context menu details to view elements.
    /// </summary>
    public class AppButtonMenuProvider : IContextMenuProvider
    {
        #region Instance Fields
        private bool _enabled;
        private bool _canCloseMenu;
        private IPalette _palette;
        private PaletteMode _paletteMode;
        private PaletteRedirect _redirector;
        private IContextMenuProvider _parent;
        private ViewLayoutStack _viewColumns;
        private NeedPaintHandler _needPaintDelegate;
        private ViewContextMenuManager _viewManager;
        private KryptonContextMenuPositionH _showHorz;
        private KryptonContextMenuPositionV _showVert;
        private PaletteContextMenuRedirect _stateCommon;
        private PaletteContextMenuItemState _stateDisabled;
        private PaletteContextMenuItemState _stateNormal;
        private PaletteRedirectContextMenu _redirectorImages;
        private PaletteContextMenuItemStateHighlight _stateHighlight;
        private PaletteContextMenuItemStateChecked _stateChecked;
        private Nullable<ToolStripDropDownCloseReason> _closeReason;
        private KryptonContextMenuItemCollection _menuCollection;
        private ViewBase _fixedViewElement;
        #endregion

        #region Events
        /// <summary>
        /// Raises the Dispose event.
        /// </summary>
        public event EventHandler Dispose;

        /// <summary>
        /// Raises the Closing event.
        /// </summary>
        public event CancelEventHandler Closing;

        /// <summary>
        /// Raises the Close event.
        /// </summary>
        public event EventHandler<CloseReasonEventArgs> Close;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ContextMenuProvider class.
        /// </summary>
        /// <param name="viewManager">View manager used to organize keyboard events.</param>
        /// <param name="menuCollection">Top level set of menu items.</param>
        /// <param name="viewColumns">Stack used for adding new columns.</param>
        /// <param name="palette">Local palette setting to use initially.</param>
        /// <param name="paletteMode">Palette mode setting to use initially.</param>
        /// <param name="redirector">Redirector used for obtaining palette values.</param>
        /// <param name="needPaintDelegate">Delegate used to when paint changes occur.</param>
        public AppButtonMenuProvider(ViewContextMenuManager viewManager,
                                     KryptonContextMenuItemCollection menuCollection,
                                     ViewLayoutStack viewColumns,
                                     IPalette palette,
                                     PaletteMode paletteMode,
                                     PaletteRedirect redirector,
                                     NeedPaintHandler needPaintDelegate)
        {
            // Store incoming state
            _viewManager = viewManager;
            _menuCollection = menuCollection;
            _viewColumns = viewColumns;
            _palette = palette;
            _paletteMode = paletteMode;
            _redirector = redirector;
            _needPaintDelegate = needPaintDelegate;

            // Create all other state
            _parent = null;
            _enabled = true;
            _canCloseMenu = true;
            _showHorz = KryptonContextMenuPositionH.After;
            _showVert = KryptonContextMenuPositionV.Top;
            _stateCommon = new PaletteContextMenuRedirect(redirector, needPaintDelegate);
            _stateNormal = new PaletteContextMenuItemState(_stateCommon);
            _stateDisabled = new PaletteContextMenuItemState(_stateCommon);
            _stateHighlight = new PaletteContextMenuItemStateHighlight(_stateCommon);
            _stateChecked = new PaletteContextMenuItemStateChecked(_stateCommon);
            _redirectorImages = new PaletteRedirectContextMenu(redirector, new ContextMenuImages(needPaintDelegate));
        }
        #endregion

        #region FixedViewBase
        /// <summary>
        /// Gets and sets the view to use as the fixed sub menu area.
        /// </summary>
        public ViewBase FixedViewBase
        {
            get { return _fixedViewElement; }
            set { _fixedViewElement = value; }
        }
        #endregion

        #region IContextMenuProvider
        /// <summary>
        /// Fires the Dispose event.
        /// </summary>
        /// <param name="e">An EventArgs containing the event data.</param>
        public void OnDispose(EventArgs e)
        {
            if (Dispose != null)
                Dispose(this, e);
        }

        /// <summary>
        /// Fires the Closing event.
        /// </summary>
        /// <param name="cea">An CancelEventArgs containing the event data.</param>
        public void OnClosing(CancelEventArgs cea)
        {
            if (_parent != null)
                _parent.OnClosing(cea);
            else if (Closing != null)
                Closing(this, cea);
        }

        /// <summary>
        /// Fires the Close event.
        /// </summary>
        /// <param name="e">A CloseReasonEventArgs containing the event data.</param>
        public void OnClose(CloseReasonEventArgs e)
        {
            if (_parent != null)
                _parent.OnClose(e);
            else if (Close != null)
            {
                _closeReason = e.CloseReason;
                Close(this, e);
            }
        }

        /// <summary>
        /// Does this provider have a parent provider.
        /// </summary>
        public bool HasParentProvider 
        {
            get { return _parent != null; }
        }

        /// <summary>
        /// Is the entire context menu enabled.
        /// </summary>
        public bool ProviderEnabled
        {
            get { return _enabled; }
        }

        /// <summary>
        /// Is context menu capable of being closed.
        /// </summary>
        public bool ProviderCanCloseMenu 
        {
            get { return _canCloseMenu; }
        }

        /// <summary>
        /// Should the sub menu be shown at fixed screen location for this menu item.
        /// </summary>
        /// <param name="menuItem">Menu item that needs to show sub menu.</param>
        /// <returns>True if the sub menu should be a fixed size.</returns>
        public bool ProviderShowSubMenuFixed(KryptonContextMenuItem menuItem)
        {
            return ((FixedViewBase != null) && _menuCollection.Contains(menuItem));
        }

        /// <summary>
        /// The rectangle used for showing a fixed location for the sub menu.
        /// </summary>
        /// <param name="menuItem">Menu item that needs to show sub menu.</param>
        /// <returns>Screen rectangle to use as display rectangle.</returns>
        public Rectangle ProviderShowSubMenuFixedRect(KryptonContextMenuItem menuItem)
        {
            if (ProviderShowSubMenuFixed(menuItem))
            {
                Rectangle screenRect = _fixedViewElement.OwningControl.RectangleToScreen(_fixedViewElement.ClientRectangle);
                screenRect.Y++;
                screenRect.Width -= 3;
                screenRect.Height -= 4;
                return screenRect;
            }
            else
                return Rectangle.Empty;
        }

        /// <summary>
        /// Sets the reason for the context menu being closed.
        /// </summary>
        public Nullable<ToolStripDropDownCloseReason> ProviderCloseReason 
        { 
            get 
            {
                if (_parent != null)
                    return _parent.ProviderCloseReason;
                else
                    return _closeReason;
            }
            
            set
            {
                if (_parent != null)
                    _parent.ProviderCloseReason = value;
                else
                    _closeReason = value;
            }
        }

        /// <summary>
        /// Gets and sets the horizontal setting used to position the menu.
        /// </summary>
        public KryptonContextMenuPositionH ProviderShowHorz 
        {
            get { return _showHorz; }
            set { _showHorz = value; } 
        }

        /// <summary>
        /// Gets and sets the vertical setting used to position the menu.
        /// </summary>
        public KryptonContextMenuPositionV ProviderShowVert 
        { 
            get { return _showVert; }
            set { _showVert = value; } 
        }

        /// <summary>
        /// Gets access to the layout for context menu columns.
        /// </summary>
        public ViewLayoutStack ProviderViewColumns
        {
            get { return _viewColumns; }
        }

        /// <summary>
        /// Gets access to the context menu specific view manager.
        /// </summary>
        public ViewContextMenuManager ProviderViewManager
        {
            get { return _viewManager; }
        }

        /// <summary>
        /// Gets access to the context menu common state.
        /// </summary>
        public PaletteContextMenuRedirect ProviderStateCommon
        {
            get { return _stateCommon; }
        }

        /// <summary>
        /// Gets access to the context menu disabled state.
        /// </summary>
        public PaletteContextMenuItemState ProviderStateDisabled
        {
            get { return _stateDisabled; }
        }

        /// <summary>
        /// Gets access to the context menu normal state.
        /// </summary>
        public PaletteContextMenuItemState ProviderStateNormal
        {
            get { return _stateNormal; }
        }

        /// <summary>
        /// Gets access to the context menu highlight state.
        /// </summary>
        public PaletteContextMenuItemStateHighlight ProviderStateHighlight
        {
            get { return _stateHighlight; }
        }

        /// <summary>
        /// Gets access to the context menu checked state.
        /// </summary>
        public PaletteContextMenuItemStateChecked ProviderStateChecked
        {
            get { return _stateChecked; }
        }

        /// <summary>
        /// Gets access to the context menu images.
        /// </summary>
        public PaletteRedirectContextMenu ProviderImages
        {
            get { return _redirectorImages; }
        }

        /// <summary>
        /// Gets access to the custom palette.
        /// </summary>
        public IPalette ProviderPalette
        {
            get { return _palette; }
        }

        /// <summary>
        /// Gets access to the palette mode.
        /// </summary>
        public PaletteMode ProviderPaletteMode
        {
            get { return _paletteMode; }
        }

        /// <summary>
        /// Gets access to the context menu redirector.
        /// </summary>
        public PaletteRedirect ProviderRedirector
        {
            get { return _redirector; }
        }

        /// <summary>
        /// Gets a delegate used to indicate a repaint is required.
        /// </summary>
        public NeedPaintHandler ProviderNeedPaintDelegate
        {
            get { return _needPaintDelegate; }
        }
        #endregion
    }
}
