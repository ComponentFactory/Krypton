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
    /// Storage for header palette settings.
    /// </summary>
    public class KryptonPaletteHeaders : Storage
    {
        #region Instance Fields
        private KryptonPaletteHeader _headerCommon;
        private KryptonPaletteHeader _headerPrimary;
        private KryptonPaletteHeader _headerSecondary;
        private KryptonPaletteHeader _headerDockInactive;
        private KryptonPaletteHeader _headerDockActive;
        private KryptonPaletteHeader _headerCalendar;
        private KryptonPaletteHeader _headerForm;
        private KryptonPaletteHeader _headerCustom1;
        private KryptonPaletteHeader _headerCustom2;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteHeaders class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteHeaders(PaletteRedirect redirector,
                                       NeedPaintHandler needPaint)
        {
            Debug.Assert(redirector != null);
            // Create the button style specific and common palettes
            _headerCommon = new KryptonPaletteHeader(redirector, PaletteBackStyle.HeaderPrimary, PaletteBorderStyle.HeaderPrimary, PaletteContentStyle.HeaderPrimary, needPaint);
            _headerPrimary = new KryptonPaletteHeader(redirector, PaletteBackStyle.HeaderPrimary, PaletteBorderStyle.HeaderPrimary, PaletteContentStyle.HeaderPrimary, needPaint);
            _headerSecondary = new KryptonPaletteHeader(redirector, PaletteBackStyle.HeaderSecondary, PaletteBorderStyle.HeaderSecondary, PaletteContentStyle.HeaderSecondary, needPaint);
            _headerDockInactive = new KryptonPaletteHeader(redirector, PaletteBackStyle.HeaderDockInactive, PaletteBorderStyle.HeaderDockInactive, PaletteContentStyle.HeaderDockInactive, needPaint);
            _headerDockActive = new KryptonPaletteHeader(redirector, PaletteBackStyle.HeaderDockActive, PaletteBorderStyle.HeaderDockActive, PaletteContentStyle.HeaderDockActive, needPaint);
            _headerCalendar = new KryptonPaletteHeader(redirector, PaletteBackStyle.HeaderCalendar, PaletteBorderStyle.HeaderCalendar, PaletteContentStyle.HeaderCalendar, needPaint);
            _headerForm = new KryptonPaletteHeader(redirector, PaletteBackStyle.HeaderForm, PaletteBorderStyle.HeaderForm, PaletteContentStyle.HeaderForm, needPaint);
            _headerCustom1 = new KryptonPaletteHeader(redirector, PaletteBackStyle.HeaderCustom1, PaletteBorderStyle.HeaderCustom1, PaletteContentStyle.HeaderCustom1, needPaint);
            _headerCustom2 = new KryptonPaletteHeader(redirector, PaletteBackStyle.HeaderCustom2, PaletteBorderStyle.HeaderCustom2, PaletteContentStyle.HeaderCustom2, needPaint);

            // Create redirectors for inheriting from style specific to style common
            PaletteRedirectTripleMetric redirectCommon = new PaletteRedirectTripleMetric(redirector, 
                                                                                         _headerCommon.StateDisabled, _headerCommon.StateDisabled,
                                                                                         _headerCommon.StateNormal, _headerCommon.StateNormal);

            // Inform the button style to use the new redirector
            _headerPrimary.SetRedirector(redirectCommon);
            _headerSecondary.SetRedirector(redirectCommon);
            _headerDockInactive.SetRedirector(redirectCommon);
            _headerDockActive.SetRedirector(redirectCommon);
            _headerCalendar.SetRedirector(redirectCommon);
            _headerForm.SetRedirector(redirectCommon);
            _headerCustom1.SetRedirector(redirectCommon);
            _headerCustom2.SetRedirector(redirectCommon);
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
                return _headerCommon.IsDefault &&
                       _headerPrimary.IsDefault &&
                       _headerSecondary.IsDefault &&
                       _headerDockInactive.IsDefault &&
                       _headerDockActive.IsDefault &&
                       _headerCalendar.IsDefault &&
                       _headerForm.IsDefault &&
                       _headerCustom1.IsDefault &&
                       _headerCustom2.IsDefault;
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
            common.StateCommon.BackStyle = PaletteBackStyle.HeaderPrimary;
            common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderPrimary;
            common.StateCommon.ContentStyle = PaletteContentStyle.HeaderPrimary;
            _headerPrimary.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.HeaderSecondary;
            common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderSecondary;
            common.StateCommon.ContentStyle = PaletteContentStyle.HeaderSecondary;
            _headerSecondary.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.HeaderDockInactive;
            common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderDockInactive;
            common.StateCommon.ContentStyle = PaletteContentStyle.HeaderDockInactive;
            _headerDockInactive.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.HeaderDockActive;
            common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderDockActive;
            common.StateCommon.ContentStyle = PaletteContentStyle.HeaderDockActive;
            _headerDockActive.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.HeaderCalendar;
            common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderCalendar;
            common.StateCommon.ContentStyle = PaletteContentStyle.HeaderCalendar;
            _headerCalendar.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.HeaderForm;
            common.StateCommon.BorderStyle = PaletteBorderStyle.HeaderForm;
            common.StateCommon.ContentStyle = PaletteContentStyle.HeaderForm;
            _headerForm.PopulateFromBase();
        }
        #endregion

        #region HeaderCommon
        /// <summary>
        /// Gets access to the common header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeader HeaderCommon
        {
            get { return _headerCommon; }
        }

        private bool ShouldSerializeHeaderCommon()
        {
            return !_headerCommon.IsDefault;
        }
        #endregion

        #region HeaderPrimary
        /// <summary>
        /// Gets access to the primary header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining primary header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeader HeaderPrimary
        {
            get { return _headerPrimary; }
        }

        private bool ShouldSerializeHeaderPrimary()
        {
            return !_headerPrimary.IsDefault;
        }
        #endregion

        #region HeaderSecondary
        /// <summary>
        /// Gets access to the secondary header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining secondary header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeader HeaderSecondary
        {
            get { return _headerSecondary; }
        }

        private bool ShouldSerializeHeaderSecondary()
        {
            return !_headerSecondary.IsDefault;
        }
        #endregion

        #region HeaderDockInactive
        /// <summary>
        /// Gets access to the inactive dock header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining inactive dock header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeader HeaderDockInactive
        {
            get { return _headerDockInactive; }
        }

        private bool ShouldSerializeHeaderDockInactive()
        {
            return !_headerDockInactive.IsDefault;
        }
        #endregion

        #region HeaderDockActive
        /// <summary>
        /// Gets access to the active dock header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining active dock header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeader HeaderDockActive
        {
            get { return _headerDockActive; }
        }

        private bool ShouldSerializeHeaderDockActive()
        {
            return !_headerDockActive.IsDefault;
        }
        #endregion

        #region HeaderCalendar
        /// <summary>
        /// Gets access to the calendar header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining calendar header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeader HeaderCalendar
        {
            get { return _headerCalendar; }
        }

        private bool ShouldSerializeHeaderCalendar()
        {
            return !_headerCalendar.IsDefault;
        }
        #endregion

        #region HeaderForm
        /// <summary>
        /// Gets access to the main form header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining main form header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeader HeaderForm
        {
            get { return _headerForm; }
        }

        private bool ShouldSerializeHeaderForm()
        {
            return !_headerForm.IsDefault;
        }
        #endregion

        #region HeaderCustom1
        /// <summary>
        /// Gets access to the first custom header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining the first custom header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeader HeaderCustom1
        {
            get { return _headerCustom1; }
        }

        private bool ShouldSerializeHeaderCustom1()
        {
            return !_headerCustom1.IsDefault;
        }
        #endregion

        #region HeaderCustom2
        /// <summary>
        /// Gets access to the second custom header appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining the second custom header appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteHeader HeaderCustom2
        {
            get { return _headerCustom2; }
        }

        private bool ShouldSerializeHeaderCustom2()
        {
            return !_headerCustom2.IsDefault;
        }
        #endregion
    }
}
