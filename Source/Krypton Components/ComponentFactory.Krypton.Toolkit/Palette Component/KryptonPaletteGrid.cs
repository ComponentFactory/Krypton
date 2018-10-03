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
	/// Storage of palette grid states.
	/// </summary>
    public class KryptonPaletteGrid : Storage
    {
        #region Instance Fields
        private PaletteDataGridViewRedirect _stateCommon;
        private PaletteDataGridViewAll _stateDisabled;
        private PaletteDataGridViewAll _stateNormal;
        private PaletteDataGridViewHeaders _stateTracking;
        private PaletteDataGridViewHeaders _statePressed;
        private PaletteDataGridViewCells _stateSelected;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteGrid class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="gridStyle">Grid style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteGrid(PaletteRedirect redirect,
                                  GridStyle gridStyle,
                                  NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateCommon = new PaletteDataGridViewRedirect(redirect, needPaint);
            _stateDisabled = new PaletteDataGridViewAll(_stateCommon, needPaint);
            _stateNormal = new PaletteDataGridViewAll(_stateCommon, needPaint);
            _stateTracking = new PaletteDataGridViewHeaders(_stateCommon, needPaint);
            _statePressed = new PaletteDataGridViewHeaders(_stateCommon, needPaint);
            _stateSelected = new PaletteDataGridViewCells(_stateCommon, needPaint);
        }
        #endregion

        #region SetRedirector
        /// <summary>
        /// Update the redirector with new reference.
        /// </summary>
        /// <param name="redirect">Target redirector.</param>
        public void SetRedirector(PaletteRedirect redirect)
        {
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
                       _stateDisabled.IsDefault &&
                       _stateNormal.IsDefault &&
                       _stateTracking.IsDefault &&
                       _statePressed.IsDefault &&
                       _stateSelected.IsDefault;
            }
		}
		#endregion

        #region PopulateFromBase
        /// <summary>
        /// Populate values from the base palette.
        /// </summary>
        /// <param name="common">Reference to common settings.</param>
        /// <param name="gridStyle">Grid style to use for populating.</param>
        public void PopulateFromBase(KryptonPaletteCommon common,
                                     GridStyle gridStyle)
        {
            // Populate only the designated styles
            _stateDisabled.PopulateFromBase(common, PaletteState.Disabled, gridStyle);
            _stateNormal.PopulateFromBase(common, PaletteState.Normal, gridStyle);
            _stateTracking.PopulateFromBase(common, PaletteState.Tracking, gridStyle);
            _statePressed.PopulateFromBase(common, PaletteState.Pressed, gridStyle);
            _stateSelected.PopulateFromBase(common, PaletteState.CheckedNormal, gridStyle);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common grid appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common grid appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewRedirect StateCommon
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
        /// Gets access to the disabled grid appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled grid appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewAll StateDisabled
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
        /// Gets access to the normal grid appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal grid appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewAll StateNormal
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
        /// Gets access to the hot tracking grid appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking grid appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewHeaders StateTracking
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
        /// Gets access to the pressed grid appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed grid appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewHeaders StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }
        #endregion

        #region StateSelected
        /// <summary>
        /// Gets access to the selected grid appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining selected grid appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDataGridViewCells StateSelected
        {
            get { return _stateSelected; }
        }

        private bool ShouldSerializeStateSelected()
        {
            return !_stateSelected.IsDefault;
        }
        #endregion
    }
}
