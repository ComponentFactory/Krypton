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
    internal class ViewDrawMenuCheckButton : ViewComposite
    {
        #region Instance Fields
        private IContextMenuProvider _provider;
        private KryptonContextMenuCheckButton _checkButton;
        private FixedContentValue _contentValues;
        private ViewDrawButton _drawButton;
        private ViewLayoutDocker _outerDocker;
        private ViewLayoutDocker _innerDocker;
        private KryptonCommand _cachedCommand;
        private bool _itemEnabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuCheckButton class.
		/// </summary>
        /// <param name="provider">Reference to provider.</param>
        /// <param name="checkButton">Reference to owning check button entry.</param>
        public ViewDrawMenuCheckButton(IContextMenuProvider provider,
                                       KryptonContextMenuCheckButton checkButton)
		{
            _provider = provider;
            _checkButton = checkButton;

            // Create fixed storage of the content values
            _contentValues = new FixedContentValue(ResolveText,
                                                   ResolveExtraText,
                                                   ResolveImage,
                                                   ResolveImageTransparentColor);

            // Decide on the enabled state of the display
            _itemEnabled = provider.ProviderEnabled && ResolveEnabled;

            // Give the heading object the redirector to use when inheriting values
            _checkButton.SetPaletteRedirect(provider.ProviderRedirector);

            // Create the view button instance
            _drawButton = new ViewDrawButton(checkButton.OverrideDisabled,
                                             checkButton.OverrideNormal,
                                             checkButton.OverrideTracking,
                                             checkButton.OverridePressed,
                                             new PaletteMetricRedirect(provider.ProviderRedirector),
                                             _contentValues,
                                             VisualOrientation.Top,
                                             true);

            // Add the checked specific palettes to the existing view button
            _drawButton.SetCheckedPalettes(checkButton.OverrideCheckedNormal,
                                           checkButton.OverrideCheckedTracking,
                                           checkButton.OverrideCheckedPressed);

            _drawButton.Enabled = _itemEnabled;
            _drawButton.Checked = ResolveChecked;

            // Place the check box on the left of the available space but inside separators
            _innerDocker = new ViewLayoutDocker();
            _innerDocker.Add(_drawButton, ViewDockStyle.Fill);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Right);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Left);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Top);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Bottom);

            // Use outer docker so that any extra space not needed is used by the null
            _outerDocker = new ViewLayoutDocker();
            _outerDocker.Add(_innerDocker, ViewDockStyle.Top);
            _outerDocker.Add(new ViewLayoutNull(), ViewDockStyle.Fill);

            // Use context menu specific version of the check box controller
            MenuCheckButtonController mcbc = new MenuCheckButtonController(provider.ProviderViewManager, _innerDocker, this, provider.ProviderNeedPaintDelegate);
            mcbc.Click += new EventHandler(OnClick);
            _innerDocker.MouseController = mcbc;
            _innerDocker.KeyController = mcbc;

            // Add docker as the composite content
            Add(_outerDocker);

            // Want to know when a property changes whilst displayed
            _checkButton.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);

            // We need to know if a property of the command changes
            if (_checkButton.KryptonCommand != null)
            {
                _cachedCommand = _checkButton.KryptonCommand;
                _checkButton.KryptonCommand.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);
            }
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMenuCheckButton:" + Id;
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
                _checkButton.PropertyChanged -= new PropertyChangedEventHandler(OnPropertyChanged);

                if (_cachedCommand != null)
                {
                    _cachedCommand.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);
                    _cachedCommand = null;
                }
            }

            base.Dispose(disposing);
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
                    return _checkButton.Enabled;
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
                    return _checkButton.Image;
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
                    return _checkButton.ImageTransparentColor;
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
                    return _checkButton.Text;
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
                    return _checkButton.ExtraText;
            }
        }
        #endregion

        #region ResolveChecked
        /// <summary>
        /// Resolves the correct checked state to use from the menu item.
        /// </summary>
        public bool ResolveChecked
        {
            get
            {
                if (_cachedCommand != null)
                    return _cachedCommand.Checked;
                else
                    return _checkButton.Checked;
            }
        }
        #endregion

        #region KryptonContextMenuCheckButton
        /// <summary>
        /// Gets access to the actual check button definiton.
        /// </summary>
        public KryptonContextMenuCheckButton KryptonContextMenuCheckButton
        {
            get { return _checkButton; }
        }
        #endregion

        #region ViewDrawButton
        /// <summary>
        /// Gets access to the view element that draws the button.
        /// </summary>
        public ViewDrawButton ViewDrawButton
        {
            get { return _drawButton; }
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
            _drawButton.Enabled = _itemEnabled;

            // Update the checked state
            _drawButton.Checked = ResolveChecked;

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
                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
                case "KryptonCommand":
                    // Unhook from any existing command
                    if (_cachedCommand != null)
                        _cachedCommand.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);

                    // Hook into the new command
                    _cachedCommand = _checkButton.KryptonCommand;
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
                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            _checkButton.PerformClick();
        }
        #endregion
    }
}
