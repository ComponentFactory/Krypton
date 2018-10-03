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
	/// Storage for palette label states.
	/// </summary>
    public class KryptonPaletteLabel : Storage
    {
        #region Instance Fields
        private PaletteContentInheritRedirect _stateInherit;
        private PaletteContent _stateCommon;
        private PaletteContent _stateNormal;
        private PaletteContent _stateDisabled;
        private PaletteContent _stateFocus;
        private PaletteContent _stateVisited;
        private PaletteContent _stateNotVisited;
        private PaletteContent _statePressed;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteLabel class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="contentStyle">Content style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteLabel(PaletteRedirect redirect,
                                   PaletteContentStyle contentStyle,
                                   NeedPaintHandler needPaint) 
		{
            // Create the storage objects
            _stateInherit = new PaletteContentInheritRedirect(redirect, contentStyle);
            _stateCommon = new PaletteContent(_stateInherit, needPaint);
            _stateDisabled = new PaletteContent(_stateCommon, needPaint);
            _stateNormal = new PaletteContent(_stateCommon, needPaint);
            _stateFocus = new PaletteContent(_stateInherit, needPaint);
            _stateVisited = new PaletteContent(_stateInherit, needPaint);
            _stateNotVisited = new PaletteContent(_stateInherit, needPaint);
            _statePressed = new PaletteContent(_stateInherit, needPaint);
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
                       _stateDisabled.IsDefault &&
                       _stateNormal.IsDefault &&
                       _stateFocus.IsDefault &&
                       _stateVisited.IsDefault &&
                       _stateNotVisited.IsDefault &&
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
            _stateDisabled.PopulateFromBase(PaletteState.Disabled);
            _stateNormal.PopulateFromBase(PaletteState.Normal);
            _stateFocus.PopulateFromBase(PaletteState.FocusOverride);
            _stateVisited.PopulateFromBase(PaletteState.LinkVisitedOverride);
            _stateNotVisited.PopulateFromBase(PaletteState.LinkNotVisitedOverride);
            _statePressed.PopulateFromBase(PaletteState.LinkPressedOverride);
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common label appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common label appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent StateCommon
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
        /// Gets access to the disabled label appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining disabled label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent StateDisabled
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
        /// Gets access to the normal label appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining normal label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent StateNormal
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateNormal()
        {
            return !_stateNormal.IsDefault;
        }
        #endregion

        #region OverrideFocus
        /// <summary>
        /// Gets access to the label appearance when it has focus.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining label appearance when it has focus.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideFocus
        {
            get { return _stateFocus; }
        }

        private bool ShouldSerializeOverrideFocus()
        {
            return !_stateFocus.IsDefault;
        }
        #endregion

        #region OverrideVisited
        /// <summary>
        /// Gets access to normal state modifications when label has been visited.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for modifying normal state when label has been visited.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideVisited
        {
            get { return _stateVisited; }
        }

        private bool ShouldSerializeOverrideVisited()
        {
            return !_stateVisited.IsDefault;
        }
        #endregion

        #region OverrideNotVisited
        /// <summary>
        /// Gets access to normal state modifications when label has not been visited.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for modifying normal state when label has not been visited.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverrideNotVisited
        {
            get { return _stateNotVisited; }
        }

        private bool ShouldSerializeOverrideNotVisited()
        {
            return !_stateNotVisited.IsDefault;
        }
        #endregion

        #region OverridePressed
        /// <summary>
        /// Gets access to the pressed label appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining pressed label appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteContent OverridePressed
        {
            get { return _statePressed; }
        }

        private bool ShouldSerializeOverridePressed()
        {
            return !_statePressed.IsDefault;
        }
        #endregion
    }
}
