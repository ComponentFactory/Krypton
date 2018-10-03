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
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
    /// Redirection for KryptonContextMenuItem state values.
	/// </summary>
	public class PaletteContextMenuItemStateRedirect : Storage
	{
		#region Instance Fields
        private PaletteRedirectDouble _itemHighlight;
        private PaletteRedirectTriple _itemImage;
        private PaletteRedirectContent _itemShortcutText;
        private PaletteRedirectDouble _itemSplit;
        private PaletteRedirectContent _itemStandard;
        private PaletteRedirectContent _itemAlternate;

        private PaletteDoubleMetricRedirect _redirectItemHighlight;
        private PaletteTripleJustImageRedirect _redirectItemImage;
        private PaletteContentInheritRedirect _redirectItemShortcutText;
        private PaletteDoubleRedirect _redirectItemSplit;
        private PaletteContentInheritRedirect _redirectItemTextStandard;
        private PaletteContentInheritRedirect _redirectItemTextAlternate;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContextMenuItemStateRedirect class.
		/// </summary>
        public PaletteContextMenuItemStateRedirect()
		{
            _itemHighlight = new PaletteRedirectDouble();
            _itemImage = new PaletteRedirectTriple();
            _itemShortcutText = new PaletteRedirectContent();
            _itemSplit = new PaletteRedirectDouble();
            _itemStandard = new PaletteRedirectContent();
            _itemAlternate = new PaletteRedirectContent();

            _redirectItemHighlight = new PaletteDoubleMetricRedirect(_itemHighlight, PaletteBackStyle.ContextMenuItemHighlight, PaletteBorderStyle.ContextMenuItemHighlight);
            _redirectItemImage = new PaletteTripleJustImageRedirect(_itemImage, PaletteBackStyle.ContextMenuItemImage, PaletteBorderStyle.ContextMenuItemImage, PaletteContentStyle.ContextMenuItemImage);
            _redirectItemShortcutText = new PaletteContentInheritRedirect(_itemShortcutText, PaletteContentStyle.ContextMenuItemShortcutText);
            _redirectItemSplit = new PaletteDoubleRedirect(_itemSplit, PaletteBackStyle.ContextMenuSeparator, PaletteBorderStyle.ContextMenuSeparator);
            _redirectItemTextStandard = new PaletteContentInheritRedirect(_itemStandard, PaletteContentStyle.ContextMenuItemTextStandard);
            _redirectItemTextAlternate = new PaletteContentInheritRedirect(_itemAlternate, PaletteContentStyle.ContextMenuItemTextAlternate);
        }
		#endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public override bool IsDefault
        {
            get { return true; }
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="provider">Provider for acquiring context menu information.</param>
        public void SetRedirector(IContextMenuProvider provider)
        {
            _itemHighlight.Target = provider.ProviderStateCommon.ItemHighlight.GetRedirector();
            _itemImage.Target = provider.ProviderStateCommon.ItemImage.GetRedirector();
            _itemShortcutText.Target = provider.ProviderStateCommon.ItemShortcutTextRedirect.GetRedirector();
            _itemSplit.Target = provider.ProviderStateCommon.ItemSplit.GetRedirector();
            _itemStandard.Target = provider.ProviderStateCommon.ItemTextStandardRedirect.GetRedirector();
            _itemAlternate.Target = provider.ProviderStateCommon.ItemTextAlternateRedirect.GetRedirector();

            _itemHighlight.SetRedirectStates(provider.ProviderStateDisabled.ItemHighlight, provider.ProviderStateNormal.ItemHighlight);
            _itemImage.SetRedirectStates(provider.ProviderStateDisabled.ItemImage, provider.ProviderStateNormal.ItemImage);
            _itemShortcutText.SetRedirectStates(provider.ProviderStateDisabled.ItemShortcutText, provider.ProviderStateNormal.ItemShortcutText);
            _itemSplit.SetRedirectStates(provider.ProviderStateDisabled.ItemSplit, provider.ProviderStateNormal.ItemSplit, provider.ProviderStateHighlight.ItemSplit, provider.ProviderStateHighlight.ItemSplit);
            _itemStandard.SetRedirectStates(provider.ProviderStateDisabled.ItemTextStandard, provider.ProviderStateNormal.ItemTextStandard);
            _itemAlternate.SetRedirectStates(provider.ProviderStateDisabled.ItemTextAlternate, provider.ProviderStateNormal.ItemTextAlternate);
        }
        #endregion

        #region ItemHighlight
        /// <summary>
        /// Gets access to the item image highlight entries.
        /// </summary>
        public PaletteDoubleMetricRedirect ItemHighlight
        {
            get { return _redirectItemHighlight; }
        }
        #endregion

        #region ItemImage
        /// <summary>
        /// Gets access to the item image appearance entries.
        /// </summary>
        public PaletteTripleJustImageRedirect ItemImage
        {
            get { return _redirectItemImage; }
        }
        #endregion

        #region ItemShortcutText
        /// <summary>
        /// Gets access to the item shortcut text appearance entries.
        /// </summary>
        public PaletteContentInheritRedirect ItemShortcutText
        {
            get { return _redirectItemShortcutText; }
        }
        #endregion

        #region ItemSplit
        /// <summary>
        /// Gets access to the item split appearance entries.
        /// </summary>
        public PaletteDoubleRedirect ItemSplit
        {
            get { return _redirectItemSplit; }
        }
        #endregion

        #region ItemTextAlternate
        /// <summary>
        /// Gets access to the alternate item text appearance entries.
        /// </summary>
        public PaletteContentInheritRedirect ItemTextAlternate
        {
            get { return _redirectItemTextAlternate; }
        }
        #endregion

        #region ItemTextStandard
        /// <summary>
        /// Gets access to the standard item text appearance entries.
        /// </summary>
        public PaletteContentInheritRedirect ItemTextStandard
        {
            get { return _redirectItemTextStandard; }
        }
        #endregion
    }
}
