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
    /// Storage for tab button palette settings.
    /// </summary>
    public class KryptonPaletteTabButtons : Storage
    {
        #region Instance Fields
        private KryptonPaletteTabButton _tabCommon;
        private KryptonPaletteTabButton _tabHighProfile;
        private KryptonPaletteTabButton _tabStandardProfile;
        private KryptonPaletteTabButton _tabLowProfile;
        private KryptonPaletteTabButton _tabDock;
        private KryptonPaletteTabButton _tabDockAutoHidden;
        private KryptonPaletteTabButton _tabOneNote;
        private KryptonPaletteTabButton _tabCustom1;
        private KryptonPaletteTabButton _tabCustom2;
        private KryptonPaletteTabButton _tabCustom3;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteTabButtons class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteTabButtons(PaletteRedirect redirector,
                                       NeedPaintHandler needPaint)
        {
            Debug.Assert(redirector != null);

            // Create the button style specific and common palettes
            _tabCommon = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabHighProfile, PaletteBorderStyle.TabHighProfile, PaletteContentStyle.TabHighProfile, needPaint);
            _tabHighProfile = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabHighProfile, PaletteBorderStyle.TabHighProfile, PaletteContentStyle.TabHighProfile, needPaint);
            _tabStandardProfile = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabStandardProfile, PaletteBorderStyle.TabStandardProfile, PaletteContentStyle.TabStandardProfile, needPaint);
            _tabLowProfile = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabLowProfile, PaletteBorderStyle.TabLowProfile, PaletteContentStyle.TabLowProfile, needPaint);
            _tabDock = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabDock, PaletteBorderStyle.TabDock, PaletteContentStyle.TabDock, needPaint);
            _tabDockAutoHidden = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabDockAutoHidden, PaletteBorderStyle.TabDockAutoHidden, PaletteContentStyle.TabDockAutoHidden, needPaint);
            _tabOneNote = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabOneNote, PaletteBorderStyle.TabOneNote, PaletteContentStyle.TabOneNote, needPaint);
            _tabCustom1 = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabCustom1, PaletteBorderStyle.TabCustom1, PaletteContentStyle.TabCustom1, needPaint);
            _tabCustom2 = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabCustom2, PaletteBorderStyle.TabCustom2, PaletteContentStyle.TabCustom2, needPaint);
            _tabCustom3 = new KryptonPaletteTabButton(redirector, PaletteBackStyle.TabCustom3, PaletteBorderStyle.TabCustom3, PaletteContentStyle.TabCustom3, needPaint);

            // Create redirectors for inheriting from style specific to style common
            PaletteRedirectTriple redirectCommon = new PaletteRedirectTriple(redirector, 
                                                                             _tabCommon.StateDisabled, _tabCommon.StateNormal,
                                                                             _tabCommon.StatePressed, _tabCommon.StateTracking,
                                                                             _tabCommon.StateSelected,_tabCommon.OverrideFocus);
            // Inform the button style to use the new redirector
            _tabHighProfile.SetRedirector(redirectCommon);
            _tabStandardProfile.SetRedirector(redirectCommon);
            _tabLowProfile.SetRedirector(redirectCommon);
            _tabDock.SetRedirector(redirectCommon);
            _tabDockAutoHidden.SetRedirector(redirectCommon);
            _tabOneNote.SetRedirector(redirectCommon);
            _tabCustom1.SetRedirector(redirectCommon);
            _tabCustom2.SetRedirector(redirectCommon);
            _tabCustom3.SetRedirector(redirectCommon);
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
                return _tabCommon.IsDefault &&
                       _tabHighProfile.IsDefault &&
                       _tabStandardProfile.IsDefault &&
                       _tabLowProfile.IsDefault &&
                       _tabDock.IsDefault &&
                       _tabDockAutoHidden.IsDefault &&
                       _tabOneNote.IsDefault &&
                       _tabCustom1.IsDefault &&
                       _tabCustom2.IsDefault &&
                       _tabCustom3.IsDefault;
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
            common.StateCommon.BackStyle = PaletteBackStyle.TabHighProfile;
            common.StateCommon.BorderStyle = PaletteBorderStyle.TabHighProfile;
            common.StateCommon.ContentStyle = PaletteContentStyle.TabHighProfile;
            _tabHighProfile.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.TabStandardProfile;
            common.StateCommon.BorderStyle = PaletteBorderStyle.TabStandardProfile;
            common.StateCommon.ContentStyle = PaletteContentStyle.TabStandardProfile;
            _tabStandardProfile.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.TabLowProfile;
            common.StateCommon.BorderStyle = PaletteBorderStyle.TabLowProfile;
            common.StateCommon.ContentStyle = PaletteContentStyle.TabLowProfile;
            _tabLowProfile.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.TabDock;
            common.StateCommon.BorderStyle = PaletteBorderStyle.TabDock;
            common.StateCommon.ContentStyle = PaletteContentStyle.TabDock;
            _tabDock.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.TabDockAutoHidden;
            common.StateCommon.BorderStyle = PaletteBorderStyle.TabDockAutoHidden;
            common.StateCommon.ContentStyle = PaletteContentStyle.TabDockAutoHidden;
            _tabDockAutoHidden.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.TabOneNote;
            common.StateCommon.BorderStyle = PaletteBorderStyle.TabOneNote;
            common.StateCommon.ContentStyle = PaletteContentStyle.TabOneNote;
            _tabOneNote.PopulateFromBase();
        }
        #endregion

        #region TabCommon
        /// <summary>
        /// Gets access to the common appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabCommon
        {
            get { return _tabCommon; }
        }

        private bool ShouldSerializeTabCommon()
        {
            return !_tabCommon.IsDefault;
        }
        #endregion

        #region TabHighProfile
        /// <summary>
        /// Gets access to the High Profile appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining High Profile appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabHighProfile
        {
            get { return _tabHighProfile; }
        }

        private bool ShouldSerializeTabHighProfile()
        {
            return !_tabHighProfile.IsDefault;
        }
        #endregion

        #region TabStandardProfile
        /// <summary>
        /// Gets access to the Standard Profile appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Standard Profile appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabStandardProfile
        {
            get { return _tabStandardProfile; }
        }

        private bool ShouldSerializeTabStandardProfile()
        {
            return !_tabStandardProfile.IsDefault;
        }
        #endregion

        #region TabLowProfile
        /// <summary>
        /// Gets access to the LowProfile appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining LowProfile appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabLowProfile
        {
            get { return _tabLowProfile; }
        }

        private bool ShouldSerializeTabLowProfile()
        {
            return !_tabLowProfile.IsDefault;
        }
        #endregion

        #region TabDock
        /// <summary>
        /// Gets access to the Dock appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Dock appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabDock
        {
            get { return _tabDock; }
        }

        private bool ShouldSerializeTabDock()
        {
            return !_tabDock.IsDefault;
        }
        #endregion

        #region TabDockAutoHidden
        /// <summary>
        /// Gets access to the Dock AutoHidden appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Dock AutoHidden appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabDockAutoHidden
        {
            get { return _tabDockAutoHidden; }
        }

        private bool ShouldSerializeTabDockAutoHidden()
        {
            return !_tabDockAutoHidden.IsDefault;
        }
        #endregion

        #region TabOneNote
        /// <summary>
        /// Gets access to the OneNote appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining OneNote appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabOneNote
        {
            get { return _tabOneNote; }
        }

        private bool ShouldSerializeTabOneNote()
        {
            return !_tabOneNote.IsDefault;
        }
        #endregion

        #region TabCustom1
        /// <summary>
        /// Gets access to the Custom1 appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Custom1 appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabCustom1
        {
            get { return _tabCustom1; }
        }

        private bool ShouldSerializeTabCustom1()
        {
            return !_tabCustom1.IsDefault;
        }
        #endregion

        #region TabCustom2
        /// <summary>
        /// Gets access to the Custom2 appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Custom2 appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabCustom2
        {
            get { return _tabCustom2; }
        }

        private bool ShouldSerializeTabCustom2()
        {
            return !_tabCustom2.IsDefault;
        }
        #endregion

        #region TabCustom3
        /// <summary>
        /// Gets access to the Custom3 appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Custom3 appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteTabButton TabCustom3
        {
            get { return _tabCustom3; }
        }

        private bool ShouldSerializeTabCustom3()
        {
            return !_tabCustom3.IsDefault;
        }
        #endregion
    }
}
