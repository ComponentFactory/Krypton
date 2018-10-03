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
    /// Storage for check button palette settings.
    /// </summary>
    public class KryptonPaletteCheckButtons : Storage
    {
        #region Instance Fields
        private KryptonPaletteCheckButton _buttonCommon;
        private KryptonPaletteCheckButton _buttonStandalone;
        private KryptonPaletteCheckButton _buttonAlternate;
        private KryptonPaletteCheckButton _buttonLowProfile;
        private KryptonPaletteCheckButton _buttonButtonSpec;
        private KryptonPaletteCheckButton _buttonBreadCrumb;
        private KryptonPaletteCheckButton _buttonCalendarDay;
        private KryptonPaletteCheckButton _buttonCluster;
        private KryptonPaletteCheckButton _buttonGallery;
        private KryptonPaletteCheckButton _buttonNavigatorStack;
        private KryptonPaletteCheckButton _buttonNavigatorOverflow;
        private KryptonPaletteCheckButton _buttonNavigatorMini;
        private KryptonPaletteCheckButton _buttonInputControl;
        private KryptonPaletteCheckButton _buttonListItem;
        private KryptonPaletteCheckButton _buttonForm;
        private KryptonPaletteCheckButton _buttonFormClose;
        private KryptonPaletteCheckButton _buttonCommand;
        private KryptonPaletteCheckButton _buttonCustom1;
        private KryptonPaletteCheckButton _buttonCustom2;
        private KryptonPaletteCheckButton _buttonCustom3;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteButtons class.
        /// </summary>
        /// <param name="redirector">Palette redirector for sourcing inherited values.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        internal KryptonPaletteCheckButtons(PaletteRedirect redirector,
                                       NeedPaintHandler needPaint)
        {
            Debug.Assert(redirector != null);

            // Create the button style specific and common palettes
            _buttonCommon = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
            _buttonStandalone = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonStandalone, PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, needPaint);
            _buttonAlternate = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonAlternate, PaletteBorderStyle.ButtonAlternate, PaletteContentStyle.ButtonAlternate, needPaint);
            _buttonLowProfile = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonLowProfile, PaletteBorderStyle.ButtonLowProfile, PaletteContentStyle.ButtonLowProfile, needPaint);
            _buttonButtonSpec = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonButtonSpec, PaletteBorderStyle.ButtonButtonSpec, PaletteContentStyle.ButtonButtonSpec, needPaint);
            _buttonBreadCrumb = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonBreadCrumb, PaletteBorderStyle.ButtonBreadCrumb, PaletteContentStyle.ButtonBreadCrumb, needPaint);
            _buttonCalendarDay = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
            _buttonCluster = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonCluster, PaletteBorderStyle.ButtonCluster, PaletteContentStyle.ButtonCluster, needPaint);
            _buttonGallery = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonGallery, PaletteBorderStyle.ButtonGallery, PaletteContentStyle.ButtonGallery, needPaint);
            _buttonNavigatorStack = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonNavigatorStack, PaletteBorderStyle.ButtonNavigatorStack, PaletteContentStyle.ButtonNavigatorStack, needPaint);
            _buttonNavigatorOverflow = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonNavigatorOverflow, PaletteBorderStyle.ButtonNavigatorOverflow, PaletteContentStyle.ButtonNavigatorOverflow, needPaint);
            _buttonNavigatorMini = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonNavigatorMini, PaletteBorderStyle.ButtonNavigatorMini, PaletteContentStyle.ButtonNavigatorMini, needPaint);
            _buttonInputControl = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonInputControl, PaletteBorderStyle.ButtonInputControl, PaletteContentStyle.ButtonInputControl, needPaint);
            _buttonListItem = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonListItem, PaletteBorderStyle.ButtonListItem, PaletteContentStyle.ButtonListItem, needPaint);
            _buttonForm = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonForm, PaletteBorderStyle.ButtonForm, PaletteContentStyle.ButtonForm, needPaint);
            _buttonFormClose = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonFormClose, PaletteBorderStyle.ButtonFormClose, PaletteContentStyle.ButtonFormClose, needPaint);
            _buttonCommand = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonCommand, PaletteBorderStyle.ButtonCommand, PaletteContentStyle.ButtonCommand, needPaint);
            _buttonCustom1 = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonCustom1, PaletteBorderStyle.ButtonCustom1, PaletteContentStyle.ButtonCustom1, needPaint);
            _buttonCustom2 = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonCustom2, PaletteBorderStyle.ButtonCustom2, PaletteContentStyle.ButtonCustom2, needPaint);
            _buttonCustom3 = new KryptonPaletteCheckButton(redirector, PaletteBackStyle.ButtonCustom3, PaletteBorderStyle.ButtonCustom3, PaletteContentStyle.ButtonCustom3, needPaint);

            // Create redirectors for inheriting from style specific to style common
            PaletteRedirectTriple redirectCommon = new PaletteRedirectTriple(redirector, 
                                                                             _buttonCommon.StateDisabled, _buttonCommon.StateNormal,
                                                                             _buttonCommon.StatePressed, _buttonCommon.StateTracking, 
                                                                             _buttonCommon.StateCheckedNormal, _buttonCommon.StateCheckedPressed, 
                                                                             _buttonCommon.StateCheckedTracking,_buttonCommon.OverrideFocus, 
                                                                             _buttonCommon.OverrideDefault);
            // Inform the button style to use the new redirector
            _buttonStandalone.SetRedirector(redirectCommon);
            _buttonAlternate.SetRedirector(redirectCommon);
            _buttonLowProfile.SetRedirector(redirectCommon);
            _buttonButtonSpec.SetRedirector(redirectCommon);
            _buttonBreadCrumb.SetRedirector(redirectCommon);
            _buttonCalendarDay.SetRedirector(redirectCommon);
            _buttonCluster.SetRedirector(redirectCommon);
            _buttonGallery.SetRedirector(redirectCommon);
            _buttonNavigatorStack.SetRedirector(redirectCommon);
            _buttonNavigatorOverflow.SetRedirector(redirectCommon);
            _buttonNavigatorMini.SetRedirector(redirectCommon);
            _buttonInputControl.SetRedirector(redirectCommon);
            _buttonListItem.SetRedirector(redirectCommon);
            _buttonForm.SetRedirector(redirectCommon);
            _buttonFormClose.SetRedirector(redirectCommon);
            _buttonCommand.SetRedirector(redirectCommon);
            _buttonCustom1.SetRedirector(redirectCommon);
            _buttonCustom2.SetRedirector(redirectCommon);
            _buttonCustom3.SetRedirector(redirectCommon);
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
                return _buttonCommon.IsDefault &&
                       _buttonStandalone.IsDefault &&
                       _buttonAlternate.IsDefault &&
                       _buttonLowProfile.IsDefault &&
                       _buttonButtonSpec.IsDefault &&
                       _buttonBreadCrumb.IsDefault &&
                       _buttonCalendarDay.IsDefault &&
                       _buttonCluster.IsDefault &&
                       _buttonGallery.IsDefault &&
                       _buttonNavigatorStack.IsDefault &&
                       _buttonNavigatorOverflow.IsDefault &&
                       _buttonNavigatorMini.IsDefault &&
                       _buttonInputControl.IsDefault &&
                       _buttonListItem.IsDefault &&
                       _buttonForm.IsDefault &&
                       _buttonFormClose.IsDefault &&
                       _buttonCommand.IsDefault &&
                       _buttonCustom1.IsDefault &&
                       _buttonCustom2.IsDefault &&
                       _buttonCustom3.IsDefault;
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
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonStandalone;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonStandalone;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonStandalone;
            _buttonStandalone.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonAlternate;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonAlternate;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonAlternate;
            _buttonAlternate.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonLowProfile;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonLowProfile;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonLowProfile;
            _buttonLowProfile.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonButtonSpec;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonButtonSpec;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonButtonSpec;
            _buttonButtonSpec.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonBreadCrumb;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonBreadCrumb;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonBreadCrumb;
            _buttonBreadCrumb.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonCalendarDay;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonCalendarDay;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonCalendarDay;
            _buttonCalendarDay.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonNavigatorStack;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonNavigatorStack;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonNavigatorStack;
            _buttonNavigatorStack.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonNavigatorOverflow;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonNavigatorOverflow;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonNavigatorOverflow;
            _buttonNavigatorOverflow.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonNavigatorMini;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonNavigatorMini;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonNavigatorMini;
            _buttonNavigatorMini.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonInputControl;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonInputControl;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonInputControl;
            _buttonInputControl.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonListItem;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonListItem;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonListItem;
            _buttonListItem.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonForm;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonForm;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonForm;
            _buttonForm.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonFormClose;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonFormClose;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonFormClose;
            _buttonFormClose.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonCommand;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonCommand;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonCommand;
            _buttonCommand.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonCluster;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonCluster;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonCluster;
            _buttonCluster.PopulateFromBase();
            common.StateCommon.BackStyle = PaletteBackStyle.ButtonGallery;
            common.StateCommon.BorderStyle = PaletteBorderStyle.ButtonGallery;
            common.StateCommon.ContentStyle = PaletteContentStyle.ButtonGallery;
            _buttonGallery.PopulateFromBase();
        }
        #endregion

        #region ButtonCommon
        /// <summary>
        /// Gets access to the common inherited button appearance.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common inherited button appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonCommon
        {
            get { return _buttonCommon; }
        }

        private bool ShouldSerializeButtonCommon()
        {
            return !_buttonCommon.IsDefault;
        }
        #endregion

        #region ButtonStandalone
        /// <summary>
        /// Gets access to the Standalone appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Standalone appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonStandalone
        {
            get { return _buttonStandalone; }
        }

        private bool ShouldSerializeButtonStandalone()
        {
            return !_buttonStandalone.IsDefault;
        }
        #endregion

        #region ButtonAlternate
        /// <summary>
        /// Gets access to the Alternate appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Alternate appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonAlternate
        {
            get { return _buttonAlternate; }
        }

        private bool ShouldSerializeButtonAlternate()
        {
            return !_buttonAlternate.IsDefault;
        }
        #endregion

        #region ButtonLowProfile
        /// <summary>
        /// Gets access to the LowProfile appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining LowProfile appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonLowProfile
        {
            get { return _buttonLowProfile; }
        }

        private bool ShouldSerializeButtonLowProfile()
        {
            return !_buttonLowProfile.IsDefault;
        }
        #endregion

        #region ButtonButtonSpec
        /// <summary>
        /// Gets access to the ButtonSpec appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonSpec appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonButtonSpec
        {
            get { return _buttonButtonSpec; }
        }

        private bool ShouldSerializeButtonButtonSpec()
        {
            return !_buttonButtonSpec.IsDefault;
        }
        #endregion

        #region ButtonBreadCrumb
        /// <summary>
        /// Gets access to the BreadCrumb appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining BreadCrumb appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonBreadCrumb
        {
            get { return _buttonBreadCrumb; }
        }

        private bool ShouldSerializeButtonBreadCrumb()
        {
            return !_buttonBreadCrumb.IsDefault;
        }
        #endregion

        #region ButtonCalendarDay
        /// <summary>
        /// Gets access to the CalendarDay appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining CalendarDay appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonCalendarDay
        {
            get { return _buttonCalendarDay; }
        }

        private bool ShouldSerializeButtonCalendarDay()
        {
            return !_buttonCalendarDay.IsDefault;
        }
        #endregion

        #region ButtonCluster
        /// <summary>
        /// Gets access to the ButtonCluster appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonCluster appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonCluster
        {
            get { return _buttonCluster; }
        }

        private bool ShouldSerializeButtonCluster()
        {
            return !_buttonCluster.IsDefault;
        }
        #endregion

        #region ButtonGallery
        /// <summary>
        /// Gets access to the ButtonGallery appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonGallery appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonGallery
        {
            get { return _buttonGallery; }
        }

        private bool ShouldSerializeButtonGallery()
        {
            return !_buttonGallery.IsDefault;
        }
        #endregion

        #region ButtonNavigatorStack
        /// <summary>
        /// Gets access to the ButtonNavigatorStack appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonNavigatorStack appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonNavigatorStack
        {
            get { return _buttonNavigatorStack; }
        }

        private bool ShouldSerializeButtonNavigatorStack()
        {
            return !_buttonNavigatorStack.IsDefault;
        }
        #endregion

        #region ButtonNavigatorOverflow
        /// <summary>
        /// Gets access to the ButtonNavigatorOverflow appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonNavigatorOverflow appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonNavigatorOverflow
        {
            get { return _buttonNavigatorOverflow; }
        }

        private bool ShouldSerializeButtonNavigatorOverflow()
        {
            return !_buttonNavigatorOverflow.IsDefault;
        }
        #endregion

        #region ButtonNavigatorMini
        /// <summary>
        /// Gets access to the ButtonNavigatorMini appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonNavigatorMini appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonNavigatorMini
        {
            get { return _buttonNavigatorMini; }
        }

        private bool ShouldSerializeButtonNavigatorMini()
        {
            return !_buttonNavigatorMini.IsDefault;
        }
        #endregion

        #region ButtonInputControl
        /// <summary>
        /// Gets access to the ButtonInputControl appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonInputControl appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonInputControl
        {
            get { return _buttonInputControl; }
        }

        private bool ShouldSerializeButtonInputControl()
        {
            return !_buttonInputControl.IsDefault;
        }
        #endregion

        #region ButtonListItem
        /// <summary>
        /// Gets access to the ButtonListItem appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonListItem appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonListItem
        {
            get { return _buttonListItem; }
        }

        private bool ShouldSerializeButtonListItem()
        {
            return !_buttonListItem.IsDefault;
        }
        #endregion

        #region ButtonForm
        /// <summary>
        /// Gets access to the ButtonForm appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonForm appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonForm
        {
            get { return _buttonForm; }
        }

        private bool ShouldSerializeButtonForm()
        {
            return !_buttonForm.IsDefault;
        }
        #endregion

        #region ButtonFormClose
        /// <summary>
        /// Gets access to the ButtonFormClose appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonFormClose appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonFormClose
        {
            get { return _buttonFormClose; }
        }

        private bool ShouldSerializeButtonFormClose()
        {
            return !_buttonFormClose.IsDefault;
        }
        #endregion

        #region ButtonCommand
        /// <summary>
        /// Gets access to the ButtonCommand appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining ButtonCommand appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonCommand
        {
            get { return _buttonCommand; }
        }

        private bool ShouldSerializeButtonCommand()
        {
            return !_buttonCommand.IsDefault;
        }
        #endregion

        #region ButtonCustom1
        /// <summary>
        /// Gets access to the Custom1 appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Custom1 appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonCustom1
        {
            get { return _buttonCustom1; }
        }

        private bool ShouldSerializeButtonCustom1()
        {
            return !_buttonCustom1.IsDefault;
        }
        #endregion

        #region ButtonCustom2
        /// <summary>
        /// Gets access to the Custom2 appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Custom2 appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonCustom2
        {
            get { return _buttonCustom2; }
        }

        private bool ShouldSerializeButtonCustom2()
        {
            return !_buttonCustom2.IsDefault;
        }
        #endregion

        #region ButtonCustom3
        /// <summary>
        /// Gets access to the Custom3 appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining Custom3 appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KryptonPaletteCheckButton ButtonCustom3
        {
            get { return _buttonCustom3; }
        }

        private bool ShouldSerializeButtonCustom3()
        {
            return !_buttonCustom3.IsDefault;
        }
        #endregion
    }
}
