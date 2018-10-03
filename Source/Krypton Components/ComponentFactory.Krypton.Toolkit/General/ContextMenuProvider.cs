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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Implementation class used to provide context menu details to view elements.
    /// </summary>
    public class ContextMenuProvider : IContextMenuProvider
    {
        #region Instance Fields
        private bool _enabled;
        private ViewLayoutStack _viewColumns;
        private ViewContextMenuManager _viewManager;
        private PaletteContextMenuRedirect _stateCommon;
        private PaletteContextMenuItemState _stateDisabled;
        private PaletteContextMenuItemState _stateNormal;
        private PaletteContextMenuItemStateHighlight _stateHighlight;
        private PaletteContextMenuItemStateChecked _stateChecked;
        private PaletteRedirectContextMenu _redirectorImages;
        private IPalette _palette;
        private PaletteMode _paletteMode;
        private PaletteRedirect _redirector;
        private NeedPaintHandler _needPaintDelegate;
        private IContextMenuProvider _parent;
        private Nullable<ToolStripDropDownCloseReason> _closeReason;
        private KryptonContextMenuPositionH _showHorz;
        private KryptonContextMenuPositionV _showVert;
        private bool _canCloseMenu;
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
        /// <param name="provider">Original provider.</param>
        /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
        /// <param name="viewManager">Reference to view manager.</param>
        /// <param name="viewColumns">Columns view element.</param>
        public ContextMenuProvider(IContextMenuProvider provider,
                                   ViewContextMenuManager viewManager,
                                   ViewLayoutStack viewColumns,
                                   NeedPaintHandler needPaintDelegate)
        {
            _parent = provider;
            _enabled = provider.ProviderEnabled;
            _canCloseMenu = provider.ProviderCanCloseMenu;
            _viewManager = viewManager;
            _viewColumns = viewColumns;
            _stateCommon = provider.ProviderStateCommon;
            _stateDisabled = provider.ProviderStateDisabled;
            _stateNormal = provider.ProviderStateNormal;
            _stateHighlight = provider.ProviderStateHighlight;
            _stateChecked = provider.ProviderStateChecked;
            _redirectorImages = provider.ProviderImages;
            _palette = provider.ProviderPalette;
            _paletteMode = provider.ProviderPaletteMode;
            _redirector = provider.ProviderRedirector;
            _needPaintDelegate = needPaintDelegate;
            _showHorz = provider.ProviderShowHorz;
            _showVert = provider.ProviderShowVert;
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuProvider class.
        /// </summary>
        /// <param name="contextMenu">Originating context menu instance.</param>
        /// <param name="viewManager">Reference to view manager.</param>
        /// <param name="viewColumns">Columns view element.</param>
        /// <param name="palette">Local palette setting to use initially.</param>
        /// <param name="paletteMode">Palette mode setting to use initially.</param>
        /// <param name="redirector">Redirector used for obtaining palette values.</param>
        /// <param name="redirectorImages">Redirector used for obtaining images.</param>
        /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
        /// <param name="enabled">Enabled state of the context menu.</param>
        public ContextMenuProvider(KryptonContextMenu contextMenu,
                                   ViewContextMenuManager viewManager,
                                   ViewLayoutStack viewColumns,
                                   IPalette palette,
                                   PaletteMode paletteMode,
                                   PaletteRedirect redirector,
                                   PaletteRedirectContextMenu redirectorImages,
                                   NeedPaintHandler needPaintDelegate,
                                   bool enabled)
        {
            _showHorz = KryptonContextMenuPositionH.Left;
            _showVert = KryptonContextMenuPositionV.Below;

            _enabled = enabled;
            _viewManager = viewManager;
            _viewColumns = viewColumns;
            _stateCommon = contextMenu.StateCommon;
            _stateDisabled = contextMenu.StateDisabled;
            _stateNormal = contextMenu.StateNormal;
            _stateHighlight = contextMenu.StateHighlight;
            _stateChecked = contextMenu.StateChecked;
            _redirectorImages = redirectorImages;
            _palette = palette;
            _paletteMode = paletteMode;
            _redirector = redirector;
            _needPaintDelegate = needPaintDelegate;
            _canCloseMenu = true;
        }

        /// <summary>
        /// Initialize a new instance of the ContextMenuProvider class.
        /// </summary>
        /// <param name="viewManager">Reference to view manager.</param>
        /// <param name="viewColumns">Columns view element.</param>
        /// <param name="palette">Local palette setting to use initially.</param>
        /// <param name="paletteMode">Palette mode setting to use initially.</param>
        /// <param name="stateCommon">State used to provide common palette values.</param>
        /// <param name="stateNormal">State used to provide normal palette values.</param>
        /// <param name="stateDisabled">State used to provide disabled palette values.</param>
        /// <param name="stateHighlight">State used to provide highlight palette values.</param>
        /// <param name="stateChecked">State used to provide checked palette values.</param>
        /// <param name="redirector">Redirector used for obtaining palette values.</param>
        /// <param name="redirectorImages">Redirector used for obtaining images.</param>
        /// <param name="needPaintDelegate">Delegate for requesting paint changes.</param>
        /// <param name="enabled">Enabled state of the context menu.</param>
        public ContextMenuProvider(ViewContextMenuManager viewManager,
                                   ViewLayoutStack viewColumns,
                                   IPalette palette,
                                   PaletteMode paletteMode,
                                   PaletteContextMenuRedirect stateCommon,
                                   PaletteContextMenuItemState stateDisabled,
                                   PaletteContextMenuItemState stateNormal,
                                   PaletteContextMenuItemStateHighlight stateHighlight,
                                   PaletteContextMenuItemStateChecked stateChecked,
                                   PaletteRedirect redirector,
                                   PaletteRedirectContextMenu redirectorImages,
                                   NeedPaintHandler needPaintDelegate,
                                   bool enabled)
        {
            _showHorz = KryptonContextMenuPositionH.Left;
            _showVert = KryptonContextMenuPositionV.Below;

            _enabled = enabled;
            _viewManager = viewManager;
            _viewColumns = viewColumns;
            _stateCommon = stateCommon;
            _stateDisabled = stateDisabled;
            _stateNormal = stateNormal;
            _stateHighlight = stateHighlight;
            _stateChecked = stateChecked;
            _redirectorImages = redirectorImages;
            _palette = palette;
            _paletteMode = paletteMode;
            _redirector = redirector;
            _needPaintDelegate = needPaintDelegate;
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
                Close(this, e);
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
            if (HasParentProvider)
                return _parent.ProviderShowSubMenuFixed(menuItem);
            else
                return false;
        }

        /// <summary>
        /// Should the sub menu be shown at fixed screen location for this menu item.
        /// </summary>
        /// <param name="menuItem">Menu item that needs to show sub menu.</param>
        /// <returns>Screen rectangle to use as display rectangle.</returns>
        public Rectangle ProviderShowSubMenuFixedRect(KryptonContextMenuItem menuItem)
        {
            if (HasParentProvider)
                return _parent.ProviderShowSubMenuFixedRect(menuItem);
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
