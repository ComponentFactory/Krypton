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
	/// Storage for palette ribbon group collapsed text states.
	/// </summary>
    public class KryptonPaletteRibbonGroupCollapsedText : Storage
    {
        #region Instance Fields
        private PaletteRibbonTextInheritRedirect _stateInherit;
        private PaletteRibbonText _stateCommon;
        private PaletteRibbonText _stateNormal;
        private PaletteRibbonText _stateTracking;
        private PaletteRibbonText _stateContextNormal;
        private PaletteRibbonText _stateContextTracking;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupCollapsedText class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupCollapsedText(PaletteRedirect redirect,
                                                      NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateInherit = new PaletteRibbonTextInheritRedirect(redirect, PaletteRibbonTextStyle.RibbonGroupCollapsedText);
            _stateCommon = new PaletteRibbonText(_stateInherit, needPaint);
            _stateNormal = new PaletteRibbonText(_stateCommon, needPaint);
            _stateTracking = new PaletteRibbonText(_stateCommon, needPaint);
            _stateContextNormal = new PaletteRibbonText(_stateCommon, needPaint);
            _stateContextTracking = new PaletteRibbonText(_stateCommon, needPaint);
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
                       _stateContextNormal.IsDefault &&
                       _stateContextTracking.IsDefault;
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
            _stateContextNormal.PopulateFromBase(PaletteState.ContextNormal);
            _stateContextTracking.PopulateFromBase(PaletteState.ContextTracking);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common ribbon group collapsed text appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon group collapsed text appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonText StateCommon
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
        /// Gets access to the normal ribbon group collapsed text appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal ribbon group collapsed text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonText StateNormal
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
        /// Gets access to the tracking ribbon group collapsed text appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tracking ribbon group collapsed text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonText StateTracking
        {
            get { return _stateTracking; }
        }

        private bool ShouldSerializeStateTracking()
        {
            return !_stateTracking.IsDefault;
        }
        #endregion

        #region StateContextNormal
        /// <summary>
        /// Gets access to the context normal ribbon group collapsed text appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context normal ribbon group collapsed text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonText StateContextNormal
        {
            get { return _stateContextNormal; }
        }

        private bool ShouldSerializeStateContextNormal()
        {
            return !_stateContextNormal.IsDefault;
        }
        #endregion

        #region StateContextTracking
        /// <summary>
        /// Gets access to the context tracking ribbon group collapsed text appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context tracking ribbon group collapsed text appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonText StateContextTracking
        {
            get { return _stateContextTracking; }
        }

        private bool ShouldSerializeStateContextTracking()
        {
            return !_stateContextTracking.IsDefault;
        }
        #endregion
    }
}
