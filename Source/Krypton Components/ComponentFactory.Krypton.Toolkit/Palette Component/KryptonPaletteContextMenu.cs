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
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Settings associated with context menus.
    /// </summary>
    public class KryptonPaletteContextMenu : Storage
    {
        #region Instance Fields
        private PaletteContextMenuRedirect _stateCommon;
        private PaletteContextMenuItemState _stateNormal;
        private PaletteContextMenuItemState _stateDisabled;
        private PaletteContextMenuItemStateHighlight _stateHighlight;
        private PaletteContextMenuItemStateChecked _stateChecked;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteContextMenu class.
        /// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteContextMenu(PaletteRedirect redirect,
                                           NeedPaintHandler needPaint)
        {
            // Create the storage objects
            _stateCommon = new PaletteContextMenuRedirect(redirect, needPaint);
            _stateNormal = new PaletteContextMenuItemState(_stateCommon);
            _stateDisabled = new PaletteContextMenuItemState(_stateCommon);
            _stateHighlight = new PaletteContextMenuItemStateHighlight(_stateCommon);
            _stateChecked = new PaletteContextMenuItemStateChecked(_stateCommon);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _stateCommon.SetRedirector(redirect);
        }
        #endregion

        #region IsDefault
        /// <summary>
        /// Gets a value indicating if all values are default.
        /// </summary>
        public override bool IsDefault
        {
            get
            {
                return _stateCommon.IsDefault &&
                       _stateNormal.IsDefault &&
                       _stateDisabled.IsDefault &&
                       _stateHighlight.IsDefault &&
                       _stateChecked.IsDefault;
            }
        }
        #endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="common">Reference to common settings.</param>
        public void PopulateFromBase(KryptonPaletteCommon common)
        {
            // Populate only the designated styles
            _stateCommon.PopulateFromBase(common, PaletteState.Normal);
            _stateDisabled.PopulateFromBase(common, PaletteState.Disabled);
            _stateNormal.PopulateFromBase(common, PaletteState.Normal);
            _stateHighlight.PopulateFromBase(common, PaletteState.Tracking);
            _stateChecked.PopulateFromBase(common, PaletteState.CheckedNormal);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        #endregion

        #region StateDisabled
        /// <summary>
        /// Gets access to the disabled appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemState StateDisabled
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateDisabled()
        {
            return !_stateDisabled.IsDefault;
        }
        #endregion

        #region StateNormal
        /// <summary>
        /// Gets access to the normal appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemState StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }
        #endregion

        #region StateHighlight
        /// <summary>
        /// Gets access to the highlight appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining highlight appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemStateHighlight StateHighlight
        {
            get { return _stateHighlight; }
        }

        private bool ShouldSerializeStateHighlight()
        {
            return !_stateHighlight.IsDefault;
        }
        #endregion

        #region StateChecked
        /// <summary>
        /// Gets access to the checked appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining checked appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContextMenuItemStateChecked StateChecked
        {
            get { return _stateChecked; }
        }

        private bool ShouldSerializeStateChecked()
        {
            return !_stateChecked.IsDefault;
        }
        #endregion
    }
}
