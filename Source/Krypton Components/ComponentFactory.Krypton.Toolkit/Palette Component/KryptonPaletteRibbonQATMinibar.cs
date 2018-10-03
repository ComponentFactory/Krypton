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
	/// Storage for palette ribbon quick access bar mini version.
	/// </summary>
    public class KryptonPaletteRibbonQATMinibar : Storage
    {
        #region Instance Fields
        private PaletteRibbonBackInheritRedirect _stateInherit;
        private PaletteRibbonBack _stateCommon;
        private PaletteRibbonBack _stateActive;
        private PaletteRibbonBack _stateInactive;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteRibbonQATMinibar class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteRibbonQATMinibar(PaletteRedirect redirect,
                                              NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateInherit = new PaletteRibbonBackInheritRedirect(redirect, PaletteRibbonBackStyle.RibbonQATMinibar);
            _stateCommon = new PaletteRibbonBack(_stateInherit, needPaint);
            _stateActive = new PaletteRibbonBack(_stateCommon, needPaint);
            _stateInactive = new PaletteRibbonBack(_stateCommon, needPaint);
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
                       _stateActive.IsDefault &&
                       _stateInactive.IsDefault;
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
            _stateActive.PopulateFromBase(PaletteState.Normal);
            _stateInactive.PopulateFromBase(PaletteState.Disabled);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common ribbon quick access toolbar minibar values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common ribbon quick access minibar values.")]
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

        #region StateActive
        /// <summary>
        /// Gets access to the active ribbon quick access toolbar minibar values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining active ribbon quick access minibar values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateActive
        {
            get { return _stateActive; }
        }

        private bool ShouldSerializeStateActive()
        {
            return !_stateActive.IsDefault;
        }
        #endregion

        #region StateInactive
        /// <summary>
        /// Gets access to the inactive ribbon quick access toolbar minibar values.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining inactive ribbon quick access minibar values.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteRibbonBack StateInactive
        {
            get { return _stateInactive; }
        }

        private bool ShouldSerializeStateInactive()
        {
            return !_stateInactive.IsDefault;
        }
        #endregion
    }
}
