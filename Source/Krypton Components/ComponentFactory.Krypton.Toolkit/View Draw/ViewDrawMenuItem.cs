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
using System.ComponentModel;

namespace ComponentFactory.Krypton.Toolkit
{
	internal class ViewDrawMenuItem : ViewDrawCanvas
    {
        #region Static Fields
        private static Image _empty16x16;
        #endregion

        #region Instance Fields
        private IContextMenuProvider _provider;
        private KryptonContextMenuItem _menuItem;
        private ViewDrawMenuImageCanvas _imageCanvas;
        private ViewDrawMenuSeparator _splitSeparator;
        private ViewDrawContent _imageContent;
        private ViewDrawMenuItemContent _textContent;
        private FixedContentValue _fixedImage;
        private VisualContextMenu _contextMenu;
        private ViewDrawMenuItemContent _shortcutContent;
        private ViewDrawMenuItemContent _subMenuContent;
        private FixedContentValue _fixedTextExtraText;
        private KryptonCommand _cachedCommand;
        private bool _imageColumn;
        private bool _standardStyle;
        private bool _itemEnabled;
        private bool _hasSubMenu;
        #endregion

        #region Identity
        static ViewDrawMenuItem()
        {
            _empty16x16 = Properties.Resources.Empty16x16;
        }

        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuItem class.
		/// </summary>
        /// <param name="provider">Provider of context menu information.</param>
        /// <param name="menuItem">Menu item definition.</param>
        /// <param name="columns">Containing columns.</param>
        /// <param name="standardStyle">Draw items with standard or alternate style.</param>
        /// <param name="imageColumn">Draw an image background for the item images.</param>
        public ViewDrawMenuItem(IContextMenuProvider provider,
                                KryptonContextMenuItem menuItem,
                                ViewLayoutStack columns,
                                bool standardStyle,
                                bool imageColumn)
            : base(menuItem.StateNormal.ItemHighlight.Back,
                   menuItem.StateNormal.ItemHighlight.Border,
                   menuItem.StateNormal.ItemHighlight,
                   PaletteMetricPadding.ContextMenuItemHighlight,
                   VisualOrientation.Top)
		{
            // Remember values
            _provider = provider;
            _menuItem = menuItem;
            _imageColumn = imageColumn;
            _standardStyle = standardStyle;

            // Give the item object the redirector to use when inheriting values
            _menuItem.SetPaletteRedirect(provider);

            // Create a stack of horizontal items inside the item
            ViewLayoutDocker docker = new ViewLayoutDocker();

            // Decide on the enabled state of the display
            _itemEnabled = provider.ProviderEnabled && ResolveEnabled;
            PaletteContextMenuItemState menuItemState = (_itemEnabled ? _menuItem.StateNormal : _menuItem.StateDisabled);

            // Calculate the image to show inside in the image column
            Image itemColumnImage = ResolveImage;
            Color itemImageTransparent = ResolveImageTransparentColor;

            // If no image found then...
            if (itemColumnImage != null)
            {
                // Ensure we have a fixed size if we are showing an image column
                if (_imageColumn)
                {
                    itemColumnImage = _empty16x16;
                    itemImageTransparent = Color.Magenta;
                }

                switch (ResolveCheckState)
                {
                    case CheckState.Checked:
                        itemColumnImage = provider.ProviderImages.GetContextMenuCheckedImage();
                        itemImageTransparent = Color.Empty;
                        break;
                    case CheckState.Indeterminate:
                        itemColumnImage = provider.ProviderImages.GetContextMenuIndeterminateImage();
                        itemImageTransparent = Color.Empty;
                        break;
                }
            }

            // Column Image
            PaletteTripleJustImage justImage = (ResolveChecked ? _menuItem.StateChecked.ItemImage : menuItemState.ItemImage);
            _fixedImage = new FixedContentValue(null, null, itemColumnImage, itemImageTransparent);
            _imageContent = new ViewDrawContent(justImage.Content, _fixedImage, VisualOrientation.Top);
            _imageCanvas = new ViewDrawMenuImageCanvas(justImage.Back, justImage.Border, 0, false);
            _imageCanvas.Add(_imageContent);
            docker.Add(new ViewLayoutCenter(_imageCanvas), ViewDockStyle.Left);
            _imageContent.Enabled = _itemEnabled;

            // Text/Extra Text
            PaletteContentJustText menuItemStyle = (standardStyle ? menuItemState.ItemTextStandard : menuItemState.ItemTextAlternate);
            _fixedTextExtraText = new FixedContentValue(ResolveText, ResolveExtraText, null, Color.Empty);
            _textContent = new ViewDrawMenuItemContent(menuItemStyle, _fixedTextExtraText, 1);
            docker.Add(_textContent, ViewDockStyle.Fill);
            _textContent.Enabled = _itemEnabled;
            
            // Shortcut
            if (_menuItem.ShowShortcutKeys)
            {
                string shortcutString = _menuItem.ShortcutKeyDisplayString;
                if (string.IsNullOrEmpty(shortcutString))
                    shortcutString = (_menuItem.ShortcutKeys != Keys.None) ? new KeysConverter().ConvertToString(_menuItem.ShortcutKeys) : string.Empty;
                if (shortcutString.Length > 0)
                {
                    _shortcutContent = new ViewDrawMenuItemContent(menuItemState.ItemShortcutText, new FixedContentValue(shortcutString, null, null, Color.Empty), 2);
                    docker.Add(_shortcutContent, ViewDockStyle.Right);
                    _shortcutContent.Enabled = _itemEnabled;
                }
            }

            // Add split item separator
            _splitSeparator = new ViewDrawMenuSeparator(menuItemState.ItemSplit);
            docker.Add(_splitSeparator, ViewDockStyle.Right);
            _splitSeparator.Enabled = _itemEnabled;
            _splitSeparator.Draw = (_menuItem.Items.Count > 0) && _menuItem.SplitSubMenu;

            // SubMenu Indicator
            _hasSubMenu = (_menuItem.Items.Count > 0);
            _subMenuContent = new ViewDrawMenuItemContent(menuItemState.ItemImage.Content, new FixedContentValue(null, null, (!_hasSubMenu ? _empty16x16 : provider.ProviderImages.GetContextMenuSubMenuImage()), (_menuItem.Items.Count == 0 ? Color.Magenta : Color.Empty)), 3);
            docker.Add(new ViewLayoutCenter(_subMenuContent), ViewDockStyle.Right);
            _subMenuContent.Enabled = _itemEnabled;
            
            Add(docker);

            // Add a controller for handing mouse and keyboard events
            MenuItemController mic = new MenuItemController(provider.ProviderViewManager, this, provider.ProviderNeedPaintDelegate);
            MouseController = mic;
            KeyController = mic;

            // Want to know when a property changes whilst displayed
            _menuItem.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
            
            // We need to know if a property of the command changes
            if (_menuItem.KryptonCommand != null)
            {
                _cachedCommand = _menuItem.KryptonCommand;
                _menuItem.KryptonCommand.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);
            }
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMenuItem:" + Id;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unhook from events
                _menuItem.PropertyChanged -= new PropertyChangedEventHandler(OnPropertyChanged);

                if (_cachedCommand != null)
                {
                    _cachedCommand.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);
                    _cachedCommand = null;
                }
            }

            base.Dispose(disposing);
        }		
        #endregion

        #region KryptonContextMenuItem
        /// <summary>
        /// Gets access to the context menu item we represent.
        /// </summary>
        public KryptonContextMenuItem KryptonContextMenuItem
        {
            get { return _menuItem; }
        }
        #endregion

        #region SplitSeparator
        /// <summary>
        /// Gets the view element used to draw the split separator.
        /// </summary>
        public ViewDrawMenuSeparator SplitSeparator
        {
            get { return _splitSeparator; }
        }
        #endregion

        #region ItemEnabled
        /// <summary>
        /// Gets the enabled state of the entire item and not for a particular view element.
        /// </summary>
        public bool ItemEnabled
        {
            get { return _itemEnabled; }
        }
        #endregion

        #region ItemText
        /// <summary>
        /// Gets the short text value of the menu item.
        /// </summary>
        public string ItemText
        {
            get { return _textContent.Values.GetShortText(); }
        }
        #endregion

        #region ItemExtraText
        /// <summary>
        /// Gets the long text value of the menu item.
        /// </summary>
        public string ItemExtraText
        {
            get { return _textContent.Values.GetLongText(); }
        }
        #endregion

        #region ResolveEnabled
        /// <summary>
        /// Resolves the correct enabled state to use from the menu item.
        /// </summary>
        public bool ResolveEnabled
        {
            get
            {
                if (_cachedCommand != null)
                    return _cachedCommand.Enabled;
                else
                    return _menuItem.Enabled;
            }
        }
        #endregion

        #region ResolveImage
        /// <summary>
        /// Resolves the correct image to use from the menu item.
        /// </summary>
        public Image ResolveImage
        {
            get
            {
                if (_cachedCommand != null)
                {
                    if (_menuItem.LargeKryptonCommandImage)
                        return _cachedCommand.ImageLarge;
                    else
                        return _cachedCommand.ImageSmall;
                }
                else
                    return _menuItem.Image;
            }
        }
        #endregion

        #region ResolveImageTransparentColor
        /// <summary>
        /// Resolves the correct image transparent color to use from the menu item.
        /// </summary>
        public Color ResolveImageTransparentColor
        {
            get
            {
                if (_cachedCommand != null)
                    return _cachedCommand.ImageTransparentColor;
                else
                    return _menuItem.ImageTransparentColor;
            }
        }
        #endregion

        #region ResolveText
        /// <summary>
        /// Resolves the correct text string to use from the menu item.
        /// </summary>
        public string ResolveText
        {
            get
            {
                if (_cachedCommand != null)
                    return _cachedCommand.Text;
                else
                    return _menuItem.Text;
            }
        }
        #endregion

        #region ResolveExtraText
        /// <summary>
        /// Resolves the correct extra text string to use from the menu item.
        /// </summary>
        public string ResolveExtraText
        {
            get
            {
                if (_cachedCommand != null)
                    return _cachedCommand.ExtraText;
                else
                    return _menuItem.ExtraText;
            }
        }
        #endregion

        #region ResolveChecked
        /// <summary>
        /// Resolves the correct checked to use from the menu item.
        /// </summary>
        public bool ResolveChecked
        {
            get
            {
                if (_cachedCommand != null)
                    return _cachedCommand.Checked;
                else
                    return _menuItem.Checked;
            }
        }
        #endregion

        #region ResolveCheckState
        /// <summary>
        /// Resolves the correct check state to use from the menu item.
        /// </summary>
        public CheckState ResolveCheckState
        {
            get
            {
                if (_cachedCommand != null)
                    return _cachedCommand.CheckState;
                else
                    return _menuItem.CheckState;
            }
        }
        #endregion

        #region PointInSubMenu
        /// <summary>
        /// Indicates whether the mouse point should show a sub menu.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool PointInSubMenu(Point pt)
        {
            // Do we have sub menu items defined?
            if (HasSubMenu)
            {
                // If menu item is split into regular button and sub menu areas
                if (_splitSeparator.Draw)
                {
                    // If mouse is inside or to the right of the slip indicator, 
                    // then a sub menu is required when the button is used
                    return (pt.X > _splitSeparator.ClientRectangle.X);
                }

                // Whole item is the sub menu area
                return true;
            }

            return false;
        }
        #endregion

        #region HasSubMenu
        /// <summary>
        /// Returns if the item shows a sub menu when selected.
        /// </summary>
        public bool HasSubMenu
        {
            get { return _hasSubMenu; }
        }
        #endregion

        #region CanCloseMenu
        /// <summary>
        /// Gets a value indicating if the menu is capable of being closed.
        /// </summary>
        public bool CanCloseMenu
        {
            get { return _provider.ProviderCanCloseMenu; }
        }
        #endregion

        #region Closing
        /// <summary>
        /// Raises the Closing event on the provider.
        /// </summary>
        /// <param name="cea">A CancelEventArgs containing the event data.</param>
        public void Closing(CancelEventArgs cea)
        {
            _provider.OnClosing(cea);
        }
        #endregion

        #region Close
        /// <summary>
        /// Raises the Close event on the provider.
        /// </summary>
        /// <param name="e">A CancelEventArgs containing the event data.</param>
        public void Close(CloseReasonEventArgs e)
        {
            _provider.OnClose(e);
        }
        #endregion

        #region DisposeContextMenu
        /// <summary>
        /// Request the showing context menu be disposed.
        /// </summary>
        public void DisposeContextMenu()
        {
            _provider.OnDispose(EventArgs.Empty);
        }
        #endregion

        #region HasParentMenu
        /// <summary>
        /// Gets a value indicating if the menu item has a parent menu.
        /// </summary>
        public bool HasParentMenu
        {
            get { return _provider.HasParentProvider; }
        }
        #endregion

        #region ShowSubMenu
        /// <summary>
        /// Ask the menu item to show the associated child collection as a menu.
        /// </summary>
        public void ShowSubMenu(bool keyboardActivated)
        {
            // Only need to show if not already doing so
            if ((_contextMenu == null) || (_contextMenu.IsDisposed))
            {
                // No need for the sub menu timer anymore, we are showing
                _provider.ProviderViewManager.SetTargetSubMenu((IContextMenuTarget)KeyController);

                // Only show a sub menu if there is one to be shown!
                if (HasSubMenu)
                {
                    // Create the actual control used to show the context menu
                    _contextMenu = new VisualContextMenu(_provider, _menuItem.Items, keyboardActivated);

                    // Need to know when the visual control is removed
                    _contextMenu.Disposed += new EventHandler(OnContextMenuDisposed);

                    // Get the screen rectangle for the drawing element
                    Rectangle menuDrawRect = this.OwningControl.RectangleToScreen(ClientRectangle);

                    // Should this menu item be shown at a fixed screen rectangle?
                    if (_provider.ProviderShowSubMenuFixed(_menuItem))
                    {
                        // Request the menu be shown at fixed screen rectangle
                        _contextMenu.ShowFixed(_provider.ProviderShowSubMenuFixedRect(_menuItem),
                                               _provider.ProviderShowHorz,
                                               _provider.ProviderShowVert);
                    }
                    else
                    {
                        // Request the menu be shown immediately
                        _contextMenu.Show(menuDrawRect,
                                          _provider.ProviderShowHorz,
                                          _provider.ProviderShowVert,
                                          true, false);
                    }
                }
            }
        }
        #endregion

        #region ClearSubMenu
        /// <summary>
        /// Remove any showing context menu.
        /// </summary>
        public void ClearSubMenu()
        {
            if (_contextMenu != null)
                VisualPopupManager.Singleton.EndPopupTracking(_contextMenu);
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

            // Always update to the latest correct check state
            if (_imageCanvas != null)
            {
                if (ResolveChecked)
                {
                    _imageCanvas.ElementState = PaletteState.CheckedNormal;
                    _imageCanvas.Enabled = ResolveEnabled;
                }
                else
                {
                    _imageCanvas.ElementState = PaletteState.Normal;
                    _imageCanvas.Enabled = true;
                }
            }

            PaletteDouble splitPalette;

            // Make sure we are using the correct palette for state
            switch (State)
            {
                default:
                case PaletteState.Normal:
                    SetPalettes(_menuItem.StateNormal.ItemHighlight.Back,
                                _menuItem.StateNormal.ItemHighlight.Border,
                                _menuItem.StateNormal.ItemHighlight);
                    splitPalette = _menuItem.StateNormal.ItemSplit;
                    break;
                case PaletteState.Disabled:
                    SetPalettes(_menuItem.StateDisabled.ItemHighlight.Back,
                                _menuItem.StateDisabled.ItemHighlight.Border,
                                _menuItem.StateDisabled.ItemHighlight);
                    splitPalette = _menuItem.StateDisabled.ItemSplit;
                    break;
                case PaletteState.Tracking:
                    SetPalettes(_menuItem.StateHighlight.ItemHighlight.Back,
                                _menuItem.StateHighlight.ItemHighlight.Border,
                                _menuItem.StateHighlight.ItemHighlight);
                    splitPalette = _menuItem.StateHighlight.ItemSplit;
                    break;
            }


            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // If we have image display
            if (_fixedImage != null)
            {
                Image itemColumnImage = ResolveImage;
                Color itemImageTransparent = ResolveImageTransparentColor;

                // If no image found then...
                if (itemColumnImage == null)
                {
                    // Ensure we have a fixed size if we are showing an image column
                    if (_imageColumn)
                    {
                        itemColumnImage = _empty16x16;
                        itemImageTransparent = Color.Magenta;
                    }

                    switch (ResolveCheckState)
                    {
                        case CheckState.Checked:
                            itemColumnImage = _provider.ProviderImages.GetContextMenuCheckedImage();
                            itemImageTransparent = Color.Empty;
                            break;
                        case CheckState.Indeterminate:
                            itemColumnImage = _provider.ProviderImages.GetContextMenuIndeterminateImage();
                            itemImageTransparent = Color.Empty;
                            break;
                    }
                }

                // Decide on the enabled state of the display
                _itemEnabled = _provider.ProviderEnabled && ResolveEnabled;
                PaletteContextMenuItemState menuItemState = (_itemEnabled ? _menuItem.StateNormal : _menuItem.StateDisabled);

                // Update palettes based on Checked state
                PaletteTripleJustImage justImage = (ResolveChecked ? _menuItem.StateChecked.ItemImage : menuItemState.ItemImage);
                if (_imageCanvas != null)
                    _imageCanvas.SetPalettes(justImage.Back, justImage.Border);

                // Update the Enabled state
                _imageContent.SetPalette(justImage.Content);
                _imageContent.Enabled = _itemEnabled;
                _textContent.Enabled = _itemEnabled;
                _splitSeparator.Enabled = _itemEnabled;
                _subMenuContent.Enabled = _itemEnabled;
                if (_shortcutContent != null)
                    _shortcutContent.Enabled = _itemEnabled;

                // Update the Text/ExtraText
                _fixedTextExtraText.ShortText = ResolveText;
                _fixedTextExtraText.LongText = ResolveExtraText;

                // Update the Image
                _fixedImage.Image = itemColumnImage;
                _fixedImage.ImageTransparentColor = itemImageTransparent;

            }

            if (_splitSeparator != null)
                _splitSeparator.SetPalettes(splitPalette.Back, splitPalette.Border);

            return base.GetPreferredSize(context);
        }

        /// <summary>
		/// Perform a layout of the elements.
		/// </summary>
		/// <param name="context">Layout context.</param>
		public override void Layout(ViewLayoutContext context)
		{
            Debug.Assert(context != null);
            ClientRectangle = context.DisplayRectangle;
            base.Layout(context);
        }
		#endregion

        #region Implementation
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Text":
                case "ExtraText":
                case "Enabled":
                case "Image":
                case "ImageTransparentColor":
                case "Checked":
                case "CheckState":
                case "ShortcutKeys":
                case "ShowShortcutKeys":
                case "LargeKryptonCommandImage":
                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
                case "KryptonCommand":
                    // Unhook from any existing command
                    if (_cachedCommand != null)
                        _cachedCommand.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);

                    // Hook into the new command
                    _cachedCommand = _menuItem.KryptonCommand;
                    if (_cachedCommand != null)
                        _cachedCommand.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);

                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
            }
        }
            
        private void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "Text":
                case "ExtraText":
                case "ImageSmall":
                case "ImageLarge":
                case "ImageTransparentColor":
                case "Enabled":
                case "Checked":
                case "CheckState":
                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
            }
        }

        private void OnContextMenuDisposed(object sender, EventArgs e)
        {
            // Should still be caching a reference to actual display control
            if (_contextMenu != null)
            {
                // Unhook from control, so it can be garbage collected
                _contextMenu.Disposed -= new EventHandler(OnContextMenuDisposed);

                // No longer need to cache reference
                _contextMenu = null;

                // Tell our view manager that we no longer show a sub menu
                _provider.ProviderViewManager.ClearTargetSubMenu((IContextMenuTarget)KeyController);
            }
        }
        #endregion
    }
}
