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
	/// Storage for palette ribbon tab states.
	/// </summary>
    public class KryptonPaletteRibbonTab : Storage
    {
        #region Instance Fields
        private PaletteRibbonDoubleInheritRedirect _stateInherit;
        private PaletteRibbonDouble _stateCommon;
        private PaletteRibbonDouble _stateNormal;
        private PaletteRibbonDouble _stateTracking;
        private PaletteRibbonDouble _stateCheckedNormal;
        private PaletteRibbonDouble _stateCheckedTracking;
        private PaletteRibbonDouble _stateContextTracking;
        private PaletteRibbonDouble _stateContextCheckedNormal;
        private PaletteRibbonDouble _stateContextCheckedTracking;
        private PaletteRibbonDouble _overrideFocus;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonTab class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonTab(PaletteRedirect redirect,
                                       NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateInherit = new PaletteRibbonDoubleInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonTab, PaletteRibbonTextStyle.RibbonTab);
            _stateCommon = new PaletteRibbonDouble(_stateInherit, _stateInherit, needPaint);
            _stateNormal = new PaletteRibbonDouble(_stateCommon, _stateCommon, needPaint);
            _stateTracking = new PaletteRibbonDouble(_stateCommon, _stateCommon, needPaint);
            _stateCheckedNormal = new PaletteRibbonDouble(_stateCommon, _stateCommon, needPaint);
            _stateCheckedTracking = new PaletteRibbonDouble(_stateCommon, _stateCommon, needPaint);
            _stateContextTracking = new PaletteRibbonDouble(_stateCommon, _stateCommon, needPaint);
            _stateContextCheckedNormal = new PaletteRibbonDouble(_stateCommon, _stateCommon, needPaint);
            _stateContextCheckedTracking = new PaletteRibbonDouble(_stateCommon, _stateCommon, needPaint);
            _overrideFocus = new PaletteRibbonDouble(_stateInherit, _stateInherit, needPaint);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
            _stateInherit.SetRedirector(redirect);
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
                       _stateNormal.IsDefault &&
                       _stateTracking.IsDefault &&
                       _stateCheckedNormal.IsDefault &&
                       _stateCheckedTracking.IsDefault &&
                       _stateContextTracking.IsDefault &&
                       _stateContextCheckedNormal.IsDefault &&
                       _stateContextCheckedTracking.IsDefault &&
                       _overrideFocus.IsDefault;
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
            _stateNormal.PopulateFromBase(PaletteState.Normal);
            _stateTracking.PopulateFromBase(PaletteState.Tracking);
            _stateCheckedNormal.PopulateFromBase(PaletteState.CheckedNormal);
            _stateCheckedTracking.PopulateFromBase(PaletteState.CheckedTracking);
            _stateContextTracking.PopulateFromBase(PaletteState.ContextTracking);
            _stateContextCheckedNormal.PopulateFromBase(PaletteState.ContextCheckedNormal);
            _stateContextCheckedTracking.PopulateFromBase(PaletteState.ContextCheckedTracking);
            _overrideFocus.PopulateFromBase(PaletteState.FocusOverride);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common ribbon tab appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon tab appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDouble StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        #endregion
    
        #region StateNormal
        /// <summary>
        /// Gets access to the normal ribbon tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDouble StateNormal
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
        /// Gets access to the tracking ribbon tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tracking ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDouble StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }
        #endregion

        #region StateCheckedNormal
        /// <summary>
        /// Gets access to the checked normal ribbon tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining checked normal ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDouble StateCheckedNormal
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
        /// Gets access to the checked tracking ribbon tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining checked tracking ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDouble StateCheckedTracking
        {
            get { return _stateCheckedTracking; }
        }

        private bool ShouldSerializeStateCheckedTracking()
        {
            return !_stateCheckedTracking.IsDefault;
        }
        #endregion

        #region StateContextTracking
        /// <summary>
        /// Gets access to the context tracking ribbon tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context tracking ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDouble StateContextTracking
        {
            get { return _stateContextTracking; }
        }

        private bool ShouldSerializeStateContextTracking()
        {
            return !_stateContextTracking.IsDefault;
        }
        #endregion

        #region StateContextCheckedNormal
        /// <summary>
        /// Gets access to the checked normal ribbon tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining checked normal ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDouble StateContextCheckedNormal
        {
            get { return _stateContextCheckedNormal; }
        }

        private bool ShouldSerializeStateContextCheckedNormal()
        {
            return !_stateContextCheckedNormal.IsDefault;
        }
        #endregion

        #region StateContextCheckedTracking
        /// <summary>
        /// Gets access to the context checked tracking ribbon tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context checked tracking ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDouble StateContextCheckedTracking
        {
            get { return _stateContextCheckedTracking; }
        }

        private bool ShouldSerializeStateContextCheckedTracking()
        {
            return !_stateContextCheckedTracking.IsDefault;
        }
        #endregion

        #region StateContextCheckedTracking
        /// <summary>
        /// Gets access to the focus overrides for ribbon tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining focus ribbon tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonDouble OverrideFocus
        {
            get { return _overrideFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_overrideFocus.IsDefault;
        }
        #endregion
    }
}
