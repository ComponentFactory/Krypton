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
	/// Storage for palette ribbon group area states.
	/// </summary>
    public class KryptonPaletteRibbonGroupArea : Storage
    {
        #region Instance Fields
        private PaletteRibbonBackInheritRedirect _stateInherit;
        private PaletteRibbonBack _stateCommon;
        private PaletteRibbonBack _stateCheckedNormal;
        private PaletteRibbonBack _stateContextCheckedTracking;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonGroupArea class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonGroupArea(PaletteRedirect redirect,
                                             NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonGroupArea);
            _stateCommon = new PaletteRibbonBack(_stateInherit, needPaint);
            _stateCheckedNormal = new PaletteRibbonBack(_stateCommon, needPaint);
            _stateContextCheckedTracking = new PaletteRibbonBack(_stateCommon, needPaint);
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
                       _stateCheckedNormal.IsDefault &&
                       _stateContextCheckedTracking.IsDefault;
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
            _stateCheckedNormal.PopulateFromBase(PaletteState.CheckedNormal);
            _stateContextCheckedTracking.PopulateFromBase(PaletteState.ContextCheckedNormal);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common ribbon application button appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon application button appearance that other states can override.")]
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

        #region StateCheckedNormal
        /// <summary>
        /// Gets access to the checked ribbon group area appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining checked ribbon group area appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateCheckedNormal
        {
            get { return _stateCheckedNormal; }
        }

        private bool ShouldSerializeStateCheckedNormal()
        {
            return !_stateCheckedNormal.IsDefault;
        }
        #endregion

        #region StateContextCheckedNormal
        /// <summary>
        /// Gets access to the context checked ribbon group area appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining context checked ribbon group area appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateContextCheckedNormal
        {
            get { return _stateContextCheckedTracking; }
        }

        private bool ShouldSerializeStateContextCheckedNormal()
        {
            return !_stateContextCheckedTracking.IsDefault;
        }
        #endregion
    }
}
