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
    internal class ViewDrawMenuRadioButton: ViewComposite
    {
        #region Instance Fields
        private IContextMenuProvider _provider;
        private KryptonContextMenuRadioButton _radioButton;
        private FixedContentValue _contentValues;
        private ViewDrawContent _drawContent;
        private ViewDrawRadioButton _drawRadioButton;
        private ViewLayoutCenter _layoutCenter;
        private ViewLayoutDocker _outerDocker;
        private ViewLayoutDocker _innerDocker;
        private bool _itemEnabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawMenuRadioButton class.
		/// </summary>
        /// <param name="provider">Reference to provider.</param>
        /// <param name="radioButton">Reference to owning radio button entry.</param>
        public ViewDrawMenuRadioButton(IContextMenuProvider provider,
                                       KryptonContextMenuRadioButton radioButton)
		{
            _provider = provider;
            _radioButton = radioButton;

            // Create fixed storage of the content values
            _contentValues = new FixedContentValue(radioButton.Text,
                                                   radioButton.ExtraText,
                                                   radioButton.Image,
                                                   radioButton.ImageTransparentColor);

            // Decide on the enabled state of the display
            _itemEnabled = provider.ProviderEnabled && _radioButton.Enabled;

            // Give the heading object the redirector to use when inheriting values
            _radioButton.SetPaletteRedirect(provider.ProviderRedirector);

            // Create the content for the actual heading text/image
            _drawContent = new ViewDrawContent((_itemEnabled ? (IPaletteContent)_radioButton.OverrideNormal : (IPaletteContent)_radioButton.OverrideDisabled), 
                                               _contentValues, VisualOrientation.Top);
            _drawContent.UseMnemonic = true;
            _drawContent.Enabled = _itemEnabled;

            // Create the radio button image drawer and place inside element so it is always centered
            _drawRadioButton = new ViewDrawRadioButton(_radioButton.StateRadioButtonImages);
            _drawRadioButton.CheckState = _radioButton.Checked;
            _drawRadioButton.Enabled = _itemEnabled;
            _layoutCenter = new ViewLayoutCenter();
            _layoutCenter.Add(_drawRadioButton);

            // Place the radio button on the left of the available space but inside separators
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

            // Use context menu specific version of the radio button controller
            MenuRadioButtonController mrbc = new MenuRadioButtonController(provider.ProviderViewManager, _innerDocker, this, provider.ProviderNeedPaintDelegate);
            mrbc.Click += new EventHandler(OnClick);
            _innerDocker.MouseController = mrbc;
            _innerDocker.KeyController = mrbc;

            // We need to be notified whenever the checked state changes
            _radioButton.CheckedChanged += new EventHandler(OnCheckedChanged);

            // Add docker as the composite content
            Add(_outerDocker);
        }

		/// <summary>
		/// Obtains the String representation of this instance.
		/// </summary>
		/// <returns>User readable name of the instance.</returns>
		public override string ToString()
		{
			// Return the class name and instance identifier
            return "ViewDrawMenuRadioButton:" + Id;
		}

        /// <summary>
        /// Release unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing">Called from Dispose method.</param>
        protected override void Dispose(bool disposing)
        {
            // Unhook event handlers to prevent memory leak
            _radioButton.CheckedChanged -= new EventHandler(OnCheckedChanged);

            // Must call base class to finish disposing
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

        #region ViewDrawRadioButton
        /// <summary>
        /// Gets access to the radio button image drawing element.
        /// </summary>
        public ViewDrawRadioButton ViewDrawRadioButton
        {
            get { return _drawRadioButton; }
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

        #region ItemText
        /// <summary>
        /// Gets the short text value of the radio button item.
        /// </summary>
        public string ItemText
        {
            get { return _contentValues.GetShortText(); }
        }
        #endregion

        #region KryptonContextMenuRadioButton
        /// <summary>
        /// Gets access to the actual radio button definiton.
        /// </summary>
        public KryptonContextMenuRadioButton KryptonContextMenuRadioButton
        {
            get { return _radioButton; }
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
        private void OnCheckedChanged(object sender, EventArgs e)
        {
            _drawRadioButton.CheckState = _radioButton.Checked;
            _provider.ProviderNeedPaintDelegate(this, new NeedLayoutEventArgs(false));
        }

        private void OnClick(object sender, EventArgs e)
        {
            _radioButton.PerformClick();
        }
        #endregion
    }
}
