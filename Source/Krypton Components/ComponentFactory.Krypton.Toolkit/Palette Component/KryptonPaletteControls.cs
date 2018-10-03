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
    /// Storage for control palette settings.
    /// </summary>
    public class KryptonPaletteControls : Storage
    {
        #region Instance Fields
        private KryptonPaletteControl _controlCommon;
        private KryptonPaletteControl _controlClient;
        private KryptonPaletteControl _controlAlternate;
        private KryptonPaletteControl _controlGroupBox;
        private KryptonPaletteControl _controlToolTip;
        private KryptonPaletteControl _controlRibbon;
        private KryptonPaletteControl _controlRibbonAppMenu;
        private KryptonPaletteControl _controlCustom1;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteControls class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteControls(PaletteRedirect redirector,
                                        NeedPaintHandler needPaint)
        {
            Debug.Assert(redirector != null);

            // Create the button style specific and common palettes
            _controlCommon = new KryptonPaletteControl(redirector, PaletteBackStyle.ControlClient, PaletteBorderStyle.ControlClient, needPaint);
            _controlClient = new KryptonPaletteControl(redirector, PaletteBackStyle.ControlClient, PaletteBorderStyle.ControlClient, needPaint);
            _controlAlternate = new KryptonPaletteControl(redirector, PaletteBackStyle.ControlAlternate, PaletteBorderStyle.ControlAlternate, needPaint);
            _controlGroupBox = new KryptonPaletteControl(redirector, PaletteBackStyle.ControlGroupBox, PaletteBorderStyle.ControlGroupBox, needPaint);
            _controlToolTip = new KryptonPaletteControl(redirector, PaletteBackStyle.ControlToolTip, PaletteBorderStyle.ControlToolTip, needPaint);
            _controlRibbon = new KryptonPaletteControl(redirector, PaletteBackStyle.ControlRibbon, PaletteBorderStyle.ControlRibbon, needPaint);
            _controlRibbonAppMenu = new KryptonPaletteControl(redirector, PaletteBackStyle.ControlRibbonAppMenu, PaletteBorderStyle.ControlRibbonAppMenu, needPaint);
            _controlCustom1 = new KryptonPaletteControl(redirector, PaletteBackStyle.ControlCustom1, PaletteBorderStyle.ControlCustom1, needPaint);

            // Create redirectors for inheriting from style specific to style common
            PaletteRedirectDouble redirectCommon = new PaletteRedirectDouble(redirector, _controlCommon.StateDisabled, _controlCommon.StateNormal);

            // Inform the button style to use the new redirector
            _controlClient.SetRedirector(redirectCommon);
            _controlAlternate.SetRedirector(redirectCommon);
            _controlGroupBox.SetRedirector(redirectCommon);
            _controlToolTip.SetRedirector(redirectCommon);
            _controlRibbon.SetRedirector(redirectCommon);
            _controlRibbonAppMenu.SetRedirector(redirectCommon);
            _controlCustom1.SetRedirector(redirectCommon);
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
                return _controlCommon.IsDefault &&
                       _controlClient.IsDefault &&
                       _controlAlternate.IsDefault &&
                       _controlGroupBox.IsDefault &&
                       _controlToolTip.IsDefault &&
                       _controlRibbon.IsDefault &&
                       _controlRibbonAppMenu.IsDefault &&
                       _controlCustom1.IsDefault;
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
            common.StateCommon.BackStyle = PaletteBackStyle.ControlClient;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ControlClient;
            _controlClient.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ControlAlternate;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ControlAlternate;
            _controlAlternate.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ControlGroupBox;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ControlGroupBox;
            _controlGroupBox.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ControlToolTip;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ControlToolTip;
            _controlToolTip.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ControlRibbon;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ControlRibbon;
            _controlRibbon.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ControlRibbonAppMenu;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ControlRibbonAppMenu;
            _controlRibbonAppMenu.PopulateFromBase();
        }
        #endregion

        #region ControlCommon
        /// <summary>
        /// Gets access to the common control appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common control appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteControl ControlCommon
        {
            get { return _controlCommon; }
        }

        private bool ShouldSerializeControlCommon()
        {
            return !_controlCommon.IsDefault;
        }
        #endregion

        #region ControlClient
        /// <summary>
        /// Gets access to the client control appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining client control appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteControl ControlClient
        {
            get { return _controlClient; }
        }

        private bool ShouldSerializeControlClient()
        {
            return !_controlClient.IsDefault;
        }
        #endregion

        #region ControlAlternate
        /// <summary>
        /// Gets access to the alternate control appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining alternate control appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteControl ControlAlternate
        {
            get { return _controlAlternate; }
        }

        private bool ShouldSerializeControlAlternate()
        {
            return !_controlAlternate.IsDefault;
        }
        #endregion

        #region ControlGroupBox
        /// <summary>
        /// Gets access to the group box control appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining group box control appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteControl ControlGroupBox
        {
            get { return _controlGroupBox; }
        }

        private bool ShouldSerializeControlGroupBox()
        {
            return !_controlGroupBox.IsDefault;
        }
        #endregion

        #region ControlToolTip
        /// <summary>
        /// Gets access to the tooltip control appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tooltip control appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteControl ControlToolTip
        {
            get { return _controlToolTip; }
        }

        private bool ShouldSerializeControlToolTip()
        {
            return !_controlToolTip.IsDefault;
        }
        #endregion

        #region ControlRibbon
        /// <summary>
        /// Gets access to the control ribbon style appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining control ribbon style appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteControl ControlRibbon
        {
            get { return _controlRibbon; }
        }

        private bool ShouldSerializeControlRibbon()
        {
            return !_controlRibbon.IsDefault;
        }
        #endregion

        #region ControlRibbonAppMenu
        /// <summary>
        /// Gets access to the control ribbon application menu style appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining control ribbon application menu style appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteControl ControlRibbonAppMenu
        {
            get { return _controlRibbonAppMenu; }
        }

        private bool ShouldSerializeControlRibbonAppMenu()
        {
            return !_controlRibbonAppMenu.IsDefault;
        }
        #endregion

        #region ControlCustom1
        /// <summary>
        /// Gets access to the first custom control appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining the first custom control appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteControl ControlCustom1
        {
            get { return _controlCustom1; }
        }

        private bool ShouldSerializeControlCustom1()
        {
            return !_controlCustom1.IsDefault;
        }
        #endregion
    }
}
