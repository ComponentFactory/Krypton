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
	/// Storage for palette ribbon group collapsed frame background states.
	/// </summary>
    public class KryptonPaletteRibbonGroupCollapsedFrameBack : Storage
    {
        #region Instance Fields
        private PaletteRibbonBackInheritRedirect _stateInherit;
        private PaletteRibbonBack _stateCommon;
        private PaletteRibbonBack _stateNormal;
        private PaletteRibbonBack _stateTracking;
        private PaletteRibbonBack _stateContextNormal;
        private PaletteRibbonBack _stateContextTracking;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupCollapsedFrameBack class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupCollapsedFrameBack(PaletteRedirect redirect,
                                                           NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupCollapsedFrameBack);
            _stateCommon = new PaletteRibbonBack(_stateInherit, needPaint);
            _stateNormal = new PaletteRibbonBack(_stateCommon, needPaint);
            _stateTracking = new PaletteRibbonBack(_stateCommon, needPaint);
            _stateContextNormal = new PaletteRibbonBack(_stateCommon, needPaint);
            _stateContextTracking = new PaletteRibbonBack(_stateCommon, needPaint);
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
        /// Gets access to the common ribbon group collapsed border appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon group collapsed border appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateCommon
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
        /// Gets access to the normal ribbon group collapsed border appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal ribbon group collapsed border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateNormal
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
        /// Gets access to the tracking ribbon group collapsed border appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tracking ribbon group collapsed border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateTracking
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
        /// Gets access to the context normal ribbon group collapsed border appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context normal ribbon group collapsed border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateContextNormal
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
        /// Gets access to the context tracking ribbon group collapsed border appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context tracking ribbon group collapsed border appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateContextTracking
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
