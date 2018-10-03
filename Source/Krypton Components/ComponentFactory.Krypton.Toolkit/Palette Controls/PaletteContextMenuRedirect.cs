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
    /// Redirect storage for KryptonContextMenu common values.
	/// </summary>
	public class PaletteContextMenuRedirect : Storage
	{
		#region Instance Fields
        private PaletteDoubleRedirect _paletteControlInner;
        private PaletteDoubleRedirect _paletteControlOuter;
        private PaletteTripleRedirect _paletteHeading;
        private PaletteDoubleMetricRedirect _paletteItemHighlight;
        private PaletteTripleJustImageRedirect _paletteItemImage;
        private PaletteDoubleRedirect _paletteItemImageColumn;
        private PaletteContentInheritRedirect _paletteItemShortcutTextRedirect;
        private PaletteContentJustShortText _paletteItemShortcutText;
        private PaletteDoubleRedirect _paletteItemSplit;
        private PaletteContentInheritRedirect _paletteItemTextAlternateRedirect;
        private PaletteContentJustText _paletteItemTextAlternate;
        private PaletteContentInheritRedirect _paletteItemTextStandardRedirect;
        private PaletteContentJustText _paletteItemTextStandard;
        private PaletteDoubleRedirect _paletteSeparator;
        #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteContextMenuRedirect class.
		/// </summary>
        /// <param name="redirect">Inheritence redirection.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public PaletteContextMenuRedirect(PaletteRedirect redirect,
                                          NeedPaintHandler needPaint)
		{
            Debug.Assert(redirect != null);

            // Create the palette storage
            _paletteControlInner = new PaletteDoubleRedirect(redirect, PaletteBackStyle.ContextMenuInner, PaletteBorderStyle.ContextMenuInner, needPaint);
            _paletteControlOuter = new PaletteDoubleRedirect(redirect, PaletteBackStyle.ContextMenuOuter, PaletteBorderStyle.ContextMenuOuter, needPaint);
            _paletteHeading = new PaletteTripleRedirect(redirect, PaletteBackStyle.ContextMenuHeading, PaletteBorderStyle.ContextMenuHeading, PaletteContentStyle.ContextMenuHeading, needPaint);
            _paletteItemHighlight = new PaletteDoubleMetricRedirect(redirect, PaletteBackStyle.ContextMenuItemHighlight, PaletteBorderStyle.ContextMenuItemHighlight, needPaint);
            _paletteItemImage = new PaletteTripleJustImageRedirect(redirect, PaletteBackStyle.ContextMenuItemImage, PaletteBorderStyle.ContextMenuItemImage, PaletteContentStyle.ContextMenuItemImage, needPaint);
            _paletteItemImageColumn = new PaletteDoubleRedirect(redirect, PaletteBackStyle.ContextMenuItemImageColumn, PaletteBorderStyle.ContextMenuItemImageColumn, needPaint);
            _paletteItemShortcutTextRedirect = new PaletteContentInheritRedirect(redirect, PaletteContentStyle.ContextMenuItemShortcutText);
            _paletteItemShortcutText = new PaletteContentJustShortText(_paletteItemShortcutTextRedirect, needPaint);
            _paletteItemSplit = new PaletteDoubleRedirect(redirect, PaletteBackStyle.ContextMenuItemSplit, PaletteBorderStyle.ContextMenuItemSplit, needPaint);
            _paletteItemTextAlternateRedirect = new PaletteContentInheritRedirect(redirect, PaletteContentStyle.ContextMenuItemTextAlternate);
            _paletteItemTextAlternate = new PaletteContentJustText(_paletteItemTextAlternateRedirect, needPaint);
            _paletteItemTextStandardRedirect = new PaletteContentInheritRedirect(redirect, PaletteContentStyle.ContextMenuItemTextStandard);
            _paletteItemTextStandard = new PaletteContentJustText(_paletteItemTextStandardRedirect, needPaint);
            _paletteSeparator = new PaletteDoubleRedirect(redirect, PaletteBackStyle.ContextMenuSeparator, PaletteBorderStyle.ContextMenuSeparator, needPaint);
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
                return (_paletteControlInner.IsDefault &&
                        _paletteControlOuter.IsDefault &&
                        _paletteHeading.IsDefault &&
                        _paletteItemHighlight.IsDefault &&
                        _paletteItemImage.IsDefault &&
                        _paletteItemImageColumn.IsDefault &&
                        _paletteItemShortcutText.IsDefault &&
                        _paletteItemSplit.IsDefault &&
                        _paletteItemTextAlternate.IsDefault &&
                        _paletteItemTextStandard.IsDefault &&
                        _paletteSeparator.IsDefault); 
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
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuInner;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuInner;
            _paletteControlInner.PopulateFromBase(state);
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuOuter;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuOuter;
            _paletteControlOuter.PopulateFromBase(state);
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuHeading;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuHeading;
            common.StateCommon.ContentStyle = PaletteContentStyle.ContextMenuHeading;
            _paletteHeading.PopulateFromBase(state);
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuItemImageColumn;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuItemImageColumn;
            _paletteItemImageColumn.PopulateFromBase(state);
            common.StateCommon.BackStyle = PaletteBackStyle.ContextMenuSeparator;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ContextMenuSeparator;
            _paletteSeparator.PopulateFromBase(state);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _paletteControlInner.SetRedirector(redirect);
            _paletteControlOuter.SetRedirector(redirect);
            _paletteHeading.SetRedirector(redirect);
            _paletteItemHighlight.SetRedirector(redirect);
            _paletteItemImage.SetRedirector(redirect);
            _paletteItemImageColumn.SetRedirector(redirect);
            _paletteItemShortcutTextRedirect.SetRedirector(redirect);
            _paletteItemSplit.SetRedirector(redirect);
            _paletteItemTextAlternateRedirect.SetRedirector(redirect);
            _paletteItemTextStandardRedirect.SetRedirector(redirect);
            _paletteSeparator.SetRedirector(redirect);
        }
        #endregion

        #region ControlInner
        /// <summary>
        /// Gets access to the inner control window appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining inner control window appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleRedirect ControlInner
        {
            get { return _paletteControlInner; }
        }

        private bool ShouldSerializeControlInner()
        {
            return !_paletteControlInner.IsDefault;
        }
        #endregion

        #region ControlOuter
        /// <summary>
        /// Gets access to the outer control window appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining outer control window appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleRedirect ControlOuter
        {
            get { return _paletteControlOuter; }
        }

        private bool ShouldSerializeControlOuter()
        {
            return !_paletteControlOuter.IsDefault;
        }
        #endregion

        #region Heading
        /// <summary>
		/// Gets access to the heading entry appearance entries.
		/// </summary>
        [KryptonPersist]
        [Category("Visuals")]
		[Description("Overrides for defining header entry appearance.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect Heading
		{
            get { return _paletteHeading; }
		}

        private bool ShouldSerializeHeading()
		{
            return !_paletteHeading.IsDefault;
		}
		#endregion

        #region ItemHighlight
        /// <summary>
        /// Gets access to the item highlight appearance entries.
        /// </summary>
        [Category("Visuals")]
        [Description("Overrides for defining item highlight appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleMetricRedirect ItemHighlight
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
        [Category("Visuals")]
        [Description("Overrides for defining item image appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleJustImageRedirect ItemImage
        {
            get { return _paletteItemImage; }
        }

        private bool ShouldSerializeItemImage()
        {
            return !_paletteItemImage.IsDefault;
        }
        #endregion

        #region ItemImageColumn
        /// <summary>
        /// Gets access to the item image column appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining item image column appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleRedirect ItemImageColumn
        {
            get { return _paletteItemImageColumn; }
        }

        private bool ShouldSerializeItemImageColumn()
        {
            return !_paletteItemImageColumn.IsDefault;
        }
        #endregion

        #region ItemShortcutText
        /// <summary>
        /// Gets access to the item shortcut text appearance entries.
        /// </summary>
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
        [Category("Visuals")]
        [Description("Overrides for defining item split appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleRedirect ItemSplit
        {
            get { return _paletteItemSplit; }
        }

        private bool ShouldSerializeItemItemSplit()
        {
            return !_paletteItemSplit.IsDefault;
        }
        #endregion

        #region ItemTextAlternate
        /// <summary>
        /// Gets access to the alternate item text appearance entries.
        /// </summary>
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

        #region Separator
        /// <summary>
        /// Gets access to the separator items appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining separator items appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleRedirect Separator
        {
            get { return _paletteSeparator; }
        }

        private bool ShouldSerializeSeparator()
        {
            return !_paletteSeparator.IsDefault;
        }
        #endregion

        #region Internal
        internal PaletteContentInheritRedirect ItemShortcutTextRedirect
        {
            get { return _paletteItemShortcutTextRedirect; }
        }

        internal PaletteContentInheritRedirect ItemTextStandardRedirect
        {
            get { return _paletteItemTextStandardRedirect; }
        }

        internal PaletteContentInheritRedirect ItemTextAlternateRedirect
        {
            get { return _paletteItemTextAlternateRedirect; }
        }
        #endregion
    }
}
