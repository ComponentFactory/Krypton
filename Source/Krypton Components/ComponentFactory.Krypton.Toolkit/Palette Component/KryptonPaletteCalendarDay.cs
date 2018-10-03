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
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace ComponentFactory.Krypton.Toolkit
{
	/// <summary>
	/// Storage of palette calendar day states.
	/// </summary>
    public class KryptonPaletteCalendarDay : Storage
    {
        #region Instance Fields
        private PaletteTripleRedirect _stateFocus;
        private PaletteTripleRedirect _stateBolded;
        private PaletteTripleRedirect _stateToday;
        private PaletteTripleRedirect _stateCommon;
        private PaletteTriple _stateDisabled;
        private PaletteTriple _stateNormal;
        private PaletteTriple _stateTracking;
        private PaletteTriple _statePressed;
        private PaletteTriple _stateCheckedNormal;
        private PaletteTriple _stateCheckedTracking;
        private PaletteTriple _stateCheckedPressed;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteCalendarDay class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteCalendarDay(PaletteRedirect redirect,
                                         NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateFocus = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
            _stateBolded = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
            _stateToday = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
            _stateCommon = new PaletteTripleRedirect(redirect, PaletteBackStyle.ButtonCalendarDay, PaletteBorderStyle.ButtonCalendarDay, PaletteContentStyle.ButtonCalendarDay, needPaint);
            _stateDisabled = new PaletteTriple(_stateCommon, needPaint);
            _stateNormal = new PaletteTriple(_stateCommon, needPaint);
            _stateTracking = new PaletteTriple(_stateCommon, needPaint);
            _statePressed = new PaletteTriple(_stateCommon, needPaint);
            _stateCheckedNormal = new PaletteTriple(_stateCommon, needPaint);
            _stateCheckedTracking = new PaletteTriple(_stateCommon, needPaint);
            _stateCheckedPressed = new PaletteTriple(_stateCommon, needPaint);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _stateFocus.SetRedirector(redirect);
            _stateBolded.SetRedirector(redirect);
            _stateToday.SetRedirector(redirect);
            _stateCommon.SetRedirector(redirect);
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
                return _stateCommon.IsDefault &&
                       _stateFocus.IsDefault &&
                       _stateBolded.IsDefault &&
                       _stateToday.IsDefault &&
                       _stateDisabled.IsDefault &&
                       _stateNormal.IsDefault &&
                       _stateTracking.IsDefault &&
                       _statePressed.IsDefault &&
                       _stateCheckedNormal.IsDefault &&
                       _stateCheckedTracking.IsDefault &&
                       _stateCheckedPressed.IsDefault;
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        public void PopulateFromBase()
        {
            // Populate only the designated styles
            _stateFocus.PopulateFromBase(PaletteState.FocusOverride);
            _stateBolded.PopulateFromBase(PaletteState.BoldedOverride);
            _stateToday.PopulateFromBase(PaletteState.BoldedOverride);
            _stateDisabled.PopulateFromBase(PaletteState.Disabled);
            _stateNormal.PopulateFromBase(PaletteState.Normal);
            _stateTracking.PopulateFromBase(PaletteState.Tracking);
            _statePressed.PopulateFromBase(PaletteState.Pressed);
            _stateCheckedNormal.PopulateFromBase(PaletteState.CheckedNormal);
            _stateCheckedTracking.PopulateFromBase(PaletteState.CheckedTracking);
            _stateCheckedPressed.PopulateFromBase(PaletteState.CheckedPressed);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common calendar day appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common calendar day appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect StateCommon
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
        /// Gets access to the disabled calendar day appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled calendar day appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateDisabled
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
        /// Gets access to the normal calendar day appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal calendar day appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }
        #endregion

        #region StateTracking
        /// <summary>
        /// Gets access to the hot tracking calendar day appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking calendar day appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }
        #endregion

        #region StatePressed
        /// <summary>
        /// Gets access to the pressed calendar day appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed calendar day appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }
        #endregion

        #region StateCheckedNormal
        /// <summary>
        /// Gets access to the normal checked calendar day appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal checked calendar day appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateCheckedNormal
        {
            get { return _stateCheckedNormal; }
        }

        private bool ShouldSerializeStateCheckedNormal()
        {
            return !_stateCheckedNormal.IsDefault;
        }
        #endregion

        #region StateCheckedTracking
        /// <summary>
        /// Gets access to the hot tracking checked calendar day appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking checked calendar day appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateCheckedTracking
        {
            get { return _stateCheckedTracking; }
        }

        private bool ShouldSerializeStateCheckedTracking()
        {
            return !_stateCheckedTracking.IsDefault;
        }
        #endregion

        #region StateCheckedPressed
        /// <summary>
        /// Gets access to the pressed checked calendar day appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed checked calendar day appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTriple StateCheckedPressed
        {
            get { return _stateCheckedPressed; }
        }

        private bool ShouldSerializeStateCheckedPressed()
        {
            return !_stateCheckedPressed.IsDefault;
        }
        #endregion

        #region OverrideFocus
        /// <summary>
        /// Gets access to the calendar day appearance when it has focus.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining calendar day appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect OverrideFocus
        {
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_stateFocus.IsDefault;
        }
        #endregion

        #region OverrideBolded
        /// <summary>
        /// Gets access to the calendar day appearance when it has bolded days.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining calendar day appearance when it has bolded days.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect OverrideBolded
        {
            get { return _stateBolded; }
        }

        private bool ShouldSerializeOverrideBolded()
        {
            return !_stateBolded.IsDefault;
        }
        #endregion

        #region OverrideToday
        /// <summary>
        /// Gets access to the calendar day appearance when it is today.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining calendar day appearance when it is today.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTripleRedirect OverrideToday
        {
            get { return _stateToday; }
        }

        private bool ShouldSerializeOverrideToday()
        {
            return !_stateToday.IsDefault;
        }
        #endregion
    }
}
