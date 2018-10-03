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
	/// Storage for palette track bar states.
	/// </summary>
    public class KryptonPaletteTrackBar : Storage
    {
        #region Instance Fields
        private PaletteTrackBarRedirect _stateCommon;
        private PaletteTrackBarRedirect _stateFocus;
        private PaletteTrackBarStates _stateDisabled;
        private PaletteTrackBarStates _stateNormal;
        private PaletteTrackBarPositionStates _stateTracking;
        private PaletteTrackBarPositionStates _statePressed;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteTrackbar class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteTrackBar(PaletteRedirect redirect,
                                      NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateCommon = new PaletteTrackBarRedirect(redirect, needPaint);
            _stateFocus = new PaletteTrackBarRedirect(redirect, needPaint);
            _stateDisabled = new PaletteTrackBarStates(_stateCommon, needPaint);
            _stateNormal = new PaletteTrackBarStates(_stateCommon, needPaint);
            _stateTracking = new PaletteTrackBarPositionStates(_stateCommon, needPaint);
            _statePressed = new PaletteTrackBarPositionStates(_stateCommon, needPaint);
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
            _stateFocus.SetRedirector(redirect);
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
                       _stateDisabled.IsDefault &&
                       _stateNormal.IsDefault &&
                       _stateTracking.IsDefault &&
                       _statePressed.IsDefault;
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
            _stateDisabled.PopulateFromBase(PaletteState.Disabled);
            _stateNormal.PopulateFromBase(PaletteState.Normal);
            _stateTracking.PopulateFromBase(PaletteState.Tracking);
            _statePressed.PopulateFromBase(PaletteState.Pressed);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common track bar appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common track bar appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarRedirect StateCommon
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
        /// Gets access to the disabled track bar appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled track bar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarStates StateDisabled
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
        /// Gets access to the normal track bar appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal track bar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarStates StateNormal
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
        /// Gets access to the tracking track bar appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tracking track bar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarPositionStates StateTracking
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
        /// Gets access to the pressed track bar appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed track bar appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarPositionStates StatePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeStatePressed()
        {
            return !_statePressed.IsDefault;
        }
        #endregion

        #region OverrideFocus
        /// <summary>
        /// Gets access to the track bar appearance when it has focus.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining track bar appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTrackBarRedirect OverrideFocus
        {
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_stateFocus.IsDefault;
        }
        #endregion    
    }
}
