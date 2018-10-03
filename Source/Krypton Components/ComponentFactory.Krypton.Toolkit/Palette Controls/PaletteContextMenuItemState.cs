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
    /// Storage for KryptonContextMenuItem state values.
    /// </summary>
	public class PaletteContextMenuItemState : Storage
	{
		#region Instance Fields
        private PaletteDoubleMetric _paletteItemHighlight;
        private PaletteTripleJustImage _paletteItemImage;
        private PaletteContentJustShortText _paletteItemShortcutText;
        private PaletteDouble _paletteItemSplit;
        private PaletteContentJustText _paletteItemTextStandard;
        private PaletteContentJustText _paletteItemTextAlternate;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContextMenuItemState class.
        /// </summary>
        /// <param name="redirect">Redirector for inheriting values.</param>
        public PaletteContextMenuItemState(PaletteContextMenuRedirect redirect)
            : this(redirect.ItemHighlight, redirect.ItemImage,
                   redirect.ItemShortcutTextRedirect, redirect.ItemSplit,
                   redirect.ItemTextStandardRedirect, redirect.ItemTextAlternateRedirect)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteContextMenuItemState class.
        /// </summary>
        /// <param name="redirect">Redirector for inheriting values.</param>
        public PaletteContextMenuItemState(PaletteContextMenuItemStateRedirect redirect)
            : this(redirect.ItemHighlight, redirect.ItemImage,
                   redirect.ItemShortcutText, redirect.ItemSplit,
                   redirect.ItemTextStandard, redirect.ItemTextAlternate)
        {
        }

        /// <summary>
        /// Initialize a new instance of the PaletteContextMenuItemState class.
		/// </summary>
        /// <param name="redirectItemHighlight">Redirector for ItemHighlight.</param>
        /// <param name="redirectItemImage">Redirector for ItemImage.</param>
        /// <param name="redirectItemShortcutText">Redirector for ItemShortcutText.</param>
        /// <param name="redirectItemSplit">Redirector for ItemSplit.</param>
        /// <param name="redirectItemTextAlternate">Redirector for ItemTextStandard.</param>
        /// <param name="redirectItemTextStandard">Redirector for ItemTextAlternate.</param>
        public PaletteContextMenuItemState(PaletteDoubleMetricRedirect redirectItemHighlight,
                                           PaletteTripleJustImageRedirect redirectItemImage,
                                           PaletteContentInheritRedirect redirectItemShortcutText,
                                           PaletteDoubleRedirect redirectItemSplit,
                                           PaletteContentInheritRedirect redirectItemTextStandard,
                                           PaletteContentInheritRedirect redirectItemTextAlternate)
		{
            _paletteItemHighlight = new PaletteDoubleMetric(redirectItemHighlight);
            _paletteItemImage = new PaletteTripleJustImage(redirectItemImage);
            _paletteItemShortcutText = new PaletteContentJustShortText(redirectItemShortcutText);
            _paletteItemSplit = new PaletteDouble(redirectItemSplit);
            _paletteItemTextStandard = new PaletteContentJustText(redirectItemTextStandard);
            _paletteItemTextAlternate = new PaletteContentJustText(redirectItemTextAlternate);
        }
		#endregion

		#region IsDefault
		/// <summary>
		/// Gets a value indicating if all values are default.
		/// </summary>
		[Browsable(false)]
		public override bool IsDefault
		{
			get 
			{
                return (_paletteItemHighlight.IsDefault &&
                        _paletteItemImage.IsDefault &&
                        _paletteItemShortcutText.IsDefault &&
                        _paletteItemSplit.IsDefault &&
                        _paletteItemTextStandard.IsDefault &&
                        _paletteItemTextAlternate.IsDefault); 
			}
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="common">Reference to common settings.</param>
        /// <param name="state">State to inherit.</param>
        public void PopulateFromBase(KryptonPaletteCommon common,
                                     PaletteState state)
        {
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuItemHighlight;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuItemHighlight;
            _paletteItemHighlight.PopulateFromBase(state);
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuItemImage;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuItemImage;
            common.StateCommon.ContentStyle = PaletteContentStyle.ContextMenuItemImage;
            _paletteItemImage.PopulateFromBase(state);
            common.StateCommon.ContentStyle = PaletteContentStyle.ContextMenuItemShortcutText;
            _paletteItemShortcutText.PopulateFromBase(state);
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuSeparator;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuSeparator;
            _paletteItemSplit.PopulateFromBase(state);
            common.StateCommon.ContentStyle = PaletteContentStyle.ContextMenuItemTextStandard;
            _paletteItemTextStandard.PopulateFromBase(state);
            common.StateCommon.ContentStyle = PaletteContentStyle.ContextMenuItemTextAlternate;
            _paletteItemTextAlternate.PopulateFromBase(state);
        }
        #endregion

        #region ItemHighlight
        /// <summary>
        /// Gets access to the item highlight appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item highlight appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleMetric ItemHighlight
        {
            get { return _paletteItemHighlight; }
        }

        private bool ShouldSerializeItemHighlight()
        {
            return !_paletteItemHighlight.IsDefault;
        }
        #endregion

        #region ItemImage
        /// <summary>
        /// Gets access to the item image appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item image appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleJustImage ItemImage
        {
            get { return _paletteItemImage; }
        }

        private bool ShouldSerializeItemImage()
        {
            return !_paletteItemImage.IsDefault;
        }
        #endregion

        #region ItemShortcutText
        /// <summary>
        /// Gets access to the item shortcut text appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item shortcut text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContentJustShortText ItemShortcutText
        {
            get { return _paletteItemShortcutText; }
        }

        private bool ShouldSerializeItemShortcutText()
        {
            return !_paletteItemShortcutText.IsDefault;
        }
        #endregion

        #region ItemSplit
        /// <summary>
        /// Gets access to the item split appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item split appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble ItemSplit
        {
            get { return _paletteItemSplit; }
        }

        private bool ShouldSerializeItemSplit()
        {
            return !_paletteItemSplit.IsDefault;
        }
        #endregion

        #region ItemTextAlternate
        /// <summary>
        /// Gets access to the alternate item text appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining alternate item text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContentJustText ItemTextAlternate
        {
            get { return _paletteItemTextAlternate; }
        }

        private bool ShouldSerializeItemTextAlternate()
        {
            return !_paletteItemTextAlternate.IsDefault;
        }
        #endregion

        #region ItemTextStandard
        /// <summary>
        /// Gets access to the standard item text appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining standard item text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContentJustText ItemTextStandard
        {
            get { return _paletteItemTextStandard; }
        }

        private bool ShouldSerializeItemTextStandard()
        {
            return !_paletteItemTextStandard.IsDefault;
        }
        #endregion
    }
}
