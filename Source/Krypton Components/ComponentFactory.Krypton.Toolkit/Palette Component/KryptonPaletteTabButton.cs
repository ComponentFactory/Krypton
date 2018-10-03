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
	/// Storage of palette tab button states.
	/// </summary>
    public class KryptonPaletteTabButton : Storage
    {
        #region Instance Fields
        private PaletteTabTripleRedirect _stateFocus;
        private PaletteTabTripleRedirect _stateCommon;
        private PaletteTabTriple _stateDisabled;
        private PaletteTabTriple _stateNormal;
        private PaletteTabTriple _stateTracking;
        private PaletteTabTriple _statePressed;
        private PaletteTabTriple _stateSelected;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteTabButton class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="backStyle">Background style.</param>
        /// <param name="borderStyle">Border style.</param>
        /// <param name="contentStyle">Content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteTabButton(PaletteRedirect redirect,
                                       PaletteBackStyle backStyle,
                                       PaletteBorderStyle borderStyle,
                                       PaletteContentStyle contentStyle,
                                       NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateFocus = new PaletteTabTripleRedirect(redirect, backStyle, borderStyle, contentStyle, needPaint);
            _stateCommon = new PaletteTabTripleRedirect(redirect, backStyle, borderStyle, contentStyle, needPaint);
            _stateDisabled = new PaletteTabTriple(_stateCommon, needPaint);
            _stateNormal = new PaletteTabTriple(_stateCommon, needPaint);
            _stateTracking = new PaletteTabTriple(_stateCommon, needPaint);
            _statePressed = new PaletteTabTriple(_stateCommon, needPaint);
            _stateSelected = new PaletteTabTriple(_stateCommon, needPaint);
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
        public void PopulateFromBase()
        {
            // Populate only the designated styles
            _stateFocus.PopulateFromBase(PaletteState.FocusOverride);
            _stateDisabled.PopulateFromBase(PaletteState.Disabled);
            _stateNormal.PopulateFromBase(PaletteState.Normal);
            _stateTracking.PopulateFromBase(PaletteState.Tracking);
            _statePressed.PopulateFromBase(PaletteState.Pressed);
            _stateSelected.PopulateFromBase(PaletteState.CheckedNormal);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common tab appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common tab appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTripleRedirect StateCommon
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
        /// Gets access to the disabled tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTriple StateDisabled
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
        /// Gets access to the normal tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTriple StateNormal
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
        /// Gets access to the hot tracking tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining hot tracking tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTriple StateTracking
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
        /// Gets access to the pressed tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTriple StatePressed
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
        /// Gets access to the normal tab appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal tab appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTriple StateSelected
        {
            get { return _stateSelected; }
        }

        private bool ShouldSerializeStateSelected()
        {
            return !_stateSelected.IsDefault;
        }
        #endregion

        #region OverrideFocus
        /// <summary>
        /// Gets access to the tab appearance when it has focus.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining tab appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteTabTripleRedirect OverrideFocus
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
