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
    internal class ViewDrawMenuCheckBox : ViewComposite
    {
        #region Instance Fields
        private IContextMenuProvider _provider;
        private KryptonContextMenuCheckBox _checkBox;
        private FixedContentValue _contentValues;
        private ViewDrawContent _drawContent;
        private ViewDrawCheckBox _drawCheckBox;
        private ViewLayoutCenter _layoutCenter;
        private ViewLayoutDocker _outerDocker;
        private ViewLayoutDocker _innerDocker;
        private KryptonCommand _cachedCommand;
        private bool _itemEnabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuCheckBox class.
		/// </summary>
        /// <param name="provider">Reference to provider.</param>
        /// <param name="checkBox">Reference to owning check box entry.</param>
        public ViewDrawMenuCheckBox(IContextMenuProvider provider,
                                    KryptonContextMenuCheckBox checkBox)
		{
            _provider = provider;
            _checkBox = checkBox;

            // Create fixed storage of the content values
            _contentValues = new FixedContentValue(ResolveText,
                                                   ResolveExtraText,
                                                   ResolveImage,
                                                   ResolveImageTransparentColor);

            // Decide on the enabled state of the display
            _itemEnabled = provider.ProviderEnabled && ResolveEnabled;

            // Give the heading object the redirector to use when inheriting values
            _checkBox.SetPaletteRedirect(provider.ProviderRedirector);

            // Create the content for the actual heading text/image
            _drawContent = new ViewDrawContent((_itemEnabled ? (IPaletteContent)_checkBox.OverrideNormal : (IPaletteContent)_checkBox.OverrideDisabled), 
                                               _contentValues, VisualOrientation.Top);
            _drawContent.UseMnemonic = true;
            _drawContent.Enabled = _itemEnabled;

            // Create the check box image drawer and place inside element so it is always centered
            _drawCheckBox = new ViewDrawCheckBox(_checkBox.StateCheckBoxImages);
            _drawCheckBox.CheckState = ResolveCheckState;
            _drawCheckBox.Enabled = _itemEnabled;
            _layoutCenter = new ViewLayoutCenter();
            _layoutCenter.Add(_drawCheckBox);

            // Place the check box on the left of the available space but inside separators
            _innerDocker = new ViewLayoutDocker();
            _innerDocker.Add(_drawContent, ViewDockStyle.Fill);
            _innerDocker.Add(_layoutCenter, ViewDockStyle.Left);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Right);
            _innerDocker.Add(new ViewLayoutSeparator(3), ViewDockStyle.Left);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Top);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Bottom);

            // Use outer docker so that any extra space not needed is used by the null
            _outerDocker = new ViewLayoutDocker();
            _outerDocker.Add(_innerDocker, ViewDockStyle.Top);
            _outerDocker.Add(new ViewLayoutNull(), ViewDockStyle.Fill);

            // Use context menu specific version of the check box controller
            MenuCheckBoxController mcbc = new MenuCheckBoxController(provider.ProviderViewManager, _innerDocker, this, provider.ProviderNeedPaintDelegate);
            mcbc.Click += new EventHandler(OnClick);
            _innerDocker.MouseController = mcbc;
            _innerDocker.KeyController = mcbc;

            // Add docker as the composite content
            Add(_outerDocker);

            // Want to know when a property changes whilst displayed
            _checkBox.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);

            // We need to know if a property of the command changes
            if (_checkBox.KryptonCommand != null)
            {
                _cachedCommand = _checkBox.KryptonCommand;
                _checkBox.KryptonCommand.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);
            }
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMenuCheckBox:" + Id;
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
                _checkBox.PropertyChanged -= new PropertyChangedEventHandler(OnPropertyChanged);

                if (_cachedCommand != null)
                {
                    _cachedCommand.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);
                    _cachedCommand = null;
                }
            }

            base.Dispose(disposing);
        }
        #endregion

        #region ViewDrawCheckBox
        /// <summary>
        /// Gets access to the check box image drawing element.
        /// </summary>
        public ViewDrawCheckBox ViewDrawCheckBox
        {
            get { return _drawCheckBox; }
        }
        #endregion

        #region ViewDrawContent
        /// <summary>
        /// Gets access to the content drawing element.
        /// </summary>
        public ViewDrawContent ViewDrawContent
        {
            get { return _drawContent; }
        }
        #endregion

        #region ItemEnabled
        /// <summary>
        /// Gets the enabled state of the item.
        /// </summary>
        public bool ItemEnabled
        {
            get { return _itemEnabled; }
        }
        #endregion

        #region ItemText
        /// <summary>
        /// Gets the short text value of the check box item.
        /// </summary>
        public string ItemText
        {
            get { return _contentValues.GetShortText(); }
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
                    return _checkBox.Enabled;
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
                    return _cachedCommand.ImageSmall;
                else
                    return _checkBox.Image;
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
                    return _checkBox.ImageTransparentColor;
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
                    return _checkBox.Text;
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
                    return _checkBox.ExtraText;
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
                    return _checkBox.CheckState;
            }
        }
        #endregion

        #region KryptonContextMenuCheckBox
        /// <summary>
        /// Gets access to the actual check box definiton.
        /// </summary>
        public KryptonContextMenuCheckBox KryptonContextMenuCheckBox
        {
            get { return _checkBox; }
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

        #region Layout
        /// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Update text and image values
            _contentValues.ShortText = ResolveText;
            _contentValues.LongText = ResolveExtraText;
            _contentValues.Image = ResolveImage;
            _contentValues.ImageTransparentColor = ResolveImageTransparentColor;

            // Find new enabled state
            _itemEnabled = _provider.ProviderEnabled && ResolveEnabled;

            // Update with enabled state
            _drawContent.SetPalette(_itemEnabled ? (IPaletteContent)_checkBox.OverrideNormal : (IPaletteContent)_checkBox.OverrideDisabled);
            _drawContent.Enabled = _itemEnabled;
            _drawCheckBox.Enabled = _itemEnabled;

            // Update the checked state
            _drawCheckBox.CheckState = ResolveCheckState;

            return base.GetPreferredSize(context);
        }

        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // We take on all the available display area
            ClientRectangle = context.DisplayRectangle;

            // Let base class perform usual processing
            base.Layout(context);
        }
        #endregion

        #region Private
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Text":
                case "ExtraText":
                case "Image":
                case "ImageTransparentColor":
                case "Enabled":
                case "Checked":
                case "CheckState":
                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
                case "KryptonCommand":
                    // Unhook from any existing command
                    if (_cachedCommand != null)
                        _cachedCommand.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);

                    // Hook into the new command
                    _cachedCommand = _checkBox.KryptonCommand;
                    if (_cachedCommand != null)
                        _cachedCommand.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);

                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
            }
        }

        private void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Text":
                case "ExtraText":
                case "ImageSmall":
                case "ImageTransparentColor":
                case "Enabled":
                case "Checked":
                case "CheckState":
                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            _checkBox.PerformClick();
        }
        #endregion
    }
}
