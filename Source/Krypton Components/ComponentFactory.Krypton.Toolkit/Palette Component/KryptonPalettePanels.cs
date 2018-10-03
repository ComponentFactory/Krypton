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
    /// Storage for panel palette settings.
    /// </summary>
    public class KryptonPalettePanels : Storage
    {
        #region Instance Fields
        private KryptonPalettePanel _panelCommon;
        private KryptonPalettePanel _panelClient;
        private KryptonPalettePanel _panelAlternate;
        private KryptonPalettePanel _panelRibbonInactive;
        private KryptonPalettePanel _panelCustom1;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPalettePanels class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPalettePanels(PaletteRedirect redirector,
                                      NeedPaintHandler needPaint)
        {
            Debug.Assert(redirector != null);

            // Create the button style specific and common palettes
            _panelCommon = new KryptonPalettePanel(redirector, PaletteBackStyle.PanelClient, needPaint);
            _panelClient = new KryptonPalettePanel(redirector, PaletteBackStyle.PanelClient, needPaint);
            _panelAlternate = new KryptonPalettePanel(redirector, PaletteBackStyle.PanelAlternate, needPaint);
            _panelRibbonInactive = new KryptonPalettePanel(redirector, PaletteBackStyle.PanelRibbonInactive, needPaint);
            _panelCustom1 = new KryptonPalettePanel(redirector, PaletteBackStyle.PanelCustom1, needPaint);

            // Create redirectors for inheriting from style specific to style common
            PaletteRedirectBack redirectCommon = new PaletteRedirectBack(redirector, _panelCommon.StateDisabled, _panelCommon.StateNormal);

            // Inform the button style to use the new redirector
            _panelClient.SetRedirector(redirectCommon);
            _panelAlternate.SetRedirector(redirectCommon);
            _panelRibbonInactive.SetRedirector(redirectCommon);
            _panelCustom1.SetRedirector(redirectCommon);
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
                return _panelCommon.IsDefault &&
                       _panelClient.IsDefault &&
                       _panelAlternate.IsDefault &&
                       _panelRibbonInactive.IsDefault &&
                       _panelCustom1.IsDefault;
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
            common.StateCommon.BackStyle = PaletteBackStyle.PanelClient;
            _panelClient.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.PanelAlternate;
            _panelAlternate.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.PanelRibbonInactive;
            _panelRibbonInactive.PopulateFromBase();
        }
        #endregion

        #region PanelCommon
        /// <summary>
        /// Gets access to the common panel appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common panel appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPalettePanel PanelCommon
        {
            get { return _panelCommon; }
        }

        private bool ShouldSerializePanelCommon()
        {
            return !_panelCommon.IsDefault;
        }
        #endregion

        #region PanelClient
        /// <summary>
        /// Gets access to the client panel appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining a client panel appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPalettePanel PanelClient
        {
            get { return _panelClient; }
        }

        private bool ShouldSerializePanelClient()
        {
            return !_panelClient.IsDefault;
        }
        #endregion

        #region PanelAlternate
        /// <summary>
        /// Gets access to the alternate panel appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining alternate panel appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPalettePanel PanelAlternate
        {
            get { return _panelAlternate; }
        }

        private bool ShouldSerializePanelAlternate()
        {
            return !_panelAlternate.IsDefault;
        }
        #endregion

        #region PanelRibbonInactive
        /// <summary>
        /// Gets access to the ribbon inactive panel appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ribbon inactive panel appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPalettePanel PanelRibbonInactive
        {
            get { return _panelRibbonInactive; }
        }

        private bool ShouldSerializePanelRibbonInactive()
        {
            return !_panelRibbonInactive.IsDefault;
        }
        #endregion

        #region PanelCustom1
        /// <summary>
        /// Gets access to the first custom panel appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining the first custom panel appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPalettePanel PanelCustom1
        {
            get { return _panelCustom1; }
        }

        private bool ShouldSerializePanelCustom1()
        {
            return !_panelCustom1.IsDefault;
        }
        #endregion
    }
}
