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
    internal class ViewDrawMenuLinkLabel : ViewComposite
    {
        #region Instance Fields
        private IContextMenuProvider _provider;
        private KryptonContextMenuLinkLabel _linkLabel;
        private FixedContentValue _contentValues;
        private ViewDrawContent _drawContent;
        private ViewLayoutDocker _outerDocker;
        private ViewLayoutDocker _innerDocker;
        private KryptonCommand _cachedCommand;
        private bool _itemEnabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuLinkLabel class.
		/// </summary>
        /// <param name="provider">Reference to provider.</param>
        /// <param name="linkLabel">Reference to owning link label entry.</param>
        public ViewDrawMenuLinkLabel(IContextMenuProvider provider,
                                     KryptonContextMenuLinkLabel linkLabel)
		{
            _provider = provider;
            _linkLabel = linkLabel;

            // Create fixed storage of the content values
            _contentValues = new FixedContentValue(linkLabel.Text,
                                                   linkLabel.ExtraText,
                                                   linkLabel.Image,
                                                   linkLabel.ImageTransparentColor);

            // Decide on the enabled state of the display
            _itemEnabled = provider.ProviderEnabled;

            // Give the heading object the redirector to use when inheriting values
            linkLabel.SetPaletteRedirect(provider.ProviderRedirector);

            // Create the content for the actual heading text/image
            _drawContent = new ViewDrawContent(linkLabel.OverrideFocusNotVisited, _contentValues, VisualOrientation.Top);
            _drawContent.UseMnemonic = true;
            _drawContent.Enabled = _itemEnabled;

            // Place label link in the center of the area but inside some separator to add spacing
            _innerDocker = new ViewLayoutDocker();
            _innerDocker.Add(_drawContent, ViewDockStyle.Fill);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Right);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Left);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Top);
            _innerDocker.Add(new ViewLayoutSeparator(1), ViewDockStyle.Bottom);

            // Use outer docker so that any extra space not needed is used by the null
            _outerDocker = new ViewLayoutDocker();
            _outerDocker.Add(_innerDocker, ViewDockStyle.Top);
            _outerDocker.Add(new ViewLayoutNull(), ViewDockStyle.Fill);

            // Use context menu specific version of the link label controller
            MenuLinkLabelController mllc = new MenuLinkLabelController(provider.ProviderViewManager, _drawContent, this, provider.ProviderNeedPaintDelegate);
            mllc.Click += new EventHandler(OnClick);
            _drawContent.MouseController = mllc;
            _drawContent.KeyController = mllc;

            // Add docker as the composite content
            Add(_outerDocker);

            // Want to know when a property changes whilst displayed
            _linkLabel.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);

            // We need to know if a property of the command changes
            if (_linkLabel.KryptonCommand != null)
            {
                _cachedCommand = _linkLabel.KryptonCommand;
                _linkLabel.KryptonCommand.PropertyChanged += new PropertyChangedEventHandler(OnCommandPropertyChanged);
            }
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMenuLinkLabel:" + Id;
		}
		#endregion

        #region ItemText
        /// <summary>
        /// Gets the short text value of the menu item.
        /// </summary>
        public string ItemText
        {
            get { return _contentValues.GetShortText(); }
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
                    return _linkLabel.Image;
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
                    return _linkLabel.ImageTransparentColor;
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
                    return _linkLabel.Text;
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
                    return _linkLabel.ExtraText;
            }
        }
        #endregion

        #region Focused
        /// <summary>
        /// Sets if the link label is currently focused.
        /// </summary>
        public bool Focused
        {
            set 
            {
                _linkLabel.OverrideFocusNotVisited.Apply = value;
                _linkLabel.OverridePressedFocus.Apply = value;
            }
        }
        #endregion

        #region Pressed
        /// <summary>
        /// Gets and sets if the link label is currently pressed.
        /// </summary>
        public bool Pressed
        {
            set 
            {
                _drawContent.SetPalette(value ? _linkLabel.OverridePressedFocus : 
                                                _linkLabel.OverrideFocusNotVisited);
            }
        }
        #endregion

        #region KryptonContextMenuLinkLabel
        /// <summary>
        /// Gets access to the actual link label definiton.
        /// </summary>
        public KryptonContextMenuLinkLabel KryptonContextMenuLinkLabel
        {
            get { return _linkLabel; }
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
            _itemEnabled = _provider.ProviderEnabled;

            // Update with enabled state
            _drawContent.Enabled = _itemEnabled;

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
                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
                case "KryptonCommand":
                    // Unhook from any existing command
                    if (_cachedCommand != null)
                        _cachedCommand.PropertyChanged -= new PropertyChangedEventHandler(OnCommandPropertyChanged);

                    // Hook into the new command
                    _cachedCommand = _linkLabel.KryptonCommand;
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
                    // Update to show new state
                    _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(true));
                    break;
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            _linkLabel.PerformClick();
        }
        #endregion
    }
}
