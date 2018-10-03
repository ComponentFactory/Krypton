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
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Draw the image/text of a recent document in the application menu.
    /// </summary>
    internal class ViewDrawRibbonAppMenuRecentDec : ViewDrawCanvas
	{
        #region Instance Fields
        private int _maxWidth;
        private string _shortcutText;
        private IContextMenuProvider _provider;
        private KryptonRibbonRecentDoc _recentDoc;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawRibbonAppMenuRecentDec class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon instance.</param>
        /// <param name="provider">Provider of context menu information.</param>
        /// <param name="recentDoc">Source rencent document instance.</param>
        /// <param name="maxWidth">Maximum width allowed for the item.</param>
        /// <param name="needPaintDelegate">Delegate for requesting paint updates.</param>
        /// <param name="index">Recent documet index.</param>
        public ViewDrawRibbonAppMenuRecentDec(KryptonRibbon ribbon,
                                              IContextMenuProvider provider,
                                              KryptonRibbonRecentDoc recentDoc,
                                              int maxWidth,
                                              NeedPaintHandler needPaintDelegate,
                                              int index)
            : base(provider.ProviderStateNormal.ItemHighlight.Back,
                   provider.ProviderStateNormal.ItemHighlight.Border,
                   provider.ProviderStateNormal.ItemHighlight,
                   PaletteMetricPadding.ContextMenuItemHighlight,
                   VisualOrientation.Top)
        {
            _maxWidth = maxWidth;
            _provider = provider;
            _recentDoc = recentDoc;
            _shortcutText = (index < 10 ? @"&" + index.ToString() : "A");

            // Use docker to organize horizontal items
            ViewLayoutDocker docker = new ViewLayoutDocker();

            // End of line gap
            docker.Add(new ViewLayoutSeparator(5), ViewDockStyle.Right);

            // Add the text/extraText/Image entry
            FixedContentValue entryContent = new FixedContentValue(recentDoc.Text, recentDoc.ExtraText, recentDoc.Image, recentDoc.ImageTransparentColor);
            RibbonRecentDocsEntryToContent entryPalette = new RibbonRecentDocsEntryToContent(ribbon.StateCommon.RibbonGeneral, ribbon.StateCommon.RibbonAppMenuDocsEntry);
            ViewDrawContent entryDraw = new ViewDrawContent(entryPalette, entryContent, VisualOrientation.Top);
            docker.Add(entryDraw, ViewDockStyle.Fill);

            // Shortcut to Content gap
            docker.Add(new ViewLayoutSeparator(5), ViewDockStyle.Left);

            // Add the shortcut column
            FixedContentValue shortcutContent = new FixedContentValue(_shortcutText, null, null, Color.Empty);
            RibbonRecentDocsShortcutToContent shortcutPalette = new RibbonRecentDocsShortcutToContent(ribbon.StateCommon.RibbonGeneral, ribbon.StateCommon.RibbonAppMenuDocsEntry);
            ViewDrawRibbonRecentShortcut shortcutDraw = new ViewDrawRibbonRecentShortcut(shortcutPalette, shortcutContent);
            docker.Add(shortcutDraw, ViewDockStyle.Left);

            // Start of line gap
            docker.Add(new ViewLayoutSeparator(3), ViewDockStyle.Left);

            // Attach a controller so menu item can be tracked and pressed
            RecentDocController controller = new RecentDocController(_provider.ProviderViewManager, this, needPaintDelegate);
            MouseController = controller;
            KeyController = controller;
            SourceController = controller;

            Add(docker);
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawRibbonAppMenuRecentDec:" + Id;
        }
        #endregion

		/// <summary>
		/// Discover the preferred size of the element.
		/// </summary>
		/// <param name="context">Layout context.</param>
        public override Size GetPreferredSize(ViewLayoutContext context)
        {
            Debug.Assert(context != null);

            // Validate incoming reference
            if (context == null) throw new ArgumentNullException("context");

            // Enforce the maximum width value
            Size preferredSize = base.GetPreferredSize(context);
            preferredSize.Width = Math.Min(_maxWidth, preferredSize.Width);
            return preferredSize;
        }

        #region Public
        /// <summary>
        /// Gets access to the originating recent doc definition.
        /// </summary>
        public KryptonRibbonRecentDoc RecentDoc
        {
            get { return _recentDoc; }
        }

        /// <summary>
        /// Gets access to the items shortcut text.
        /// </summary>
        public string ShortcutText
        {
            get { return _shortcutText; }
        }

        /// <summary>
        /// Gets a value indicating if the menu is capable of being closed.
        /// </summary>
        public bool CanCloseMenu
        {
            get { return _provider.ProviderCanCloseMenu; }
        }

        /// <summary>
        /// Raises the Closing event on the provider.
        /// </summary>
        /// <param name="cea">A CancelEventArgs containing the event data.</param>
        public void Closing(CancelEventArgs cea)
        {
            _provider.OnClosing(cea);
        }

        /// <summary>
        /// Raises the Close event on the provider.
        /// </summary>
        /// <param name="e">A CancelEventArgs containing the event data.</param>
        public void Close(CloseReasonEventArgs e)
        {
            _provider.OnClose(e);
        }

        /// <summary>
        /// Gets direct access to the context menu provider.
        /// </summary>
        public IContextMenuProvider Provider
        {
            get { return _provider; }
        }
        #endregion
    }
}
