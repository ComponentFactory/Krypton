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
	/// Storage for palette form states.
	/// </summary>
    public class KryptonPaletteForm : KryptonPaletteDouble3
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonPaletteForm class.
		/// </summary>
        /// <param name="redirect">Redirector to inherit values from.</param>
        /// <param name="backStyle">Background style.</param>
        /// <param name="borderStyle">Border style.</param>
        /// <param name="needPaint">Delegate for notifying paint requests.</param>
        public KryptonPaletteForm(PaletteRedirect redirect,
                                  PaletteBackStyle backStyle,
                                  PaletteBorderStyle borderStyle,
                                  NeedPaintHandler needPaint) 
            : base(redirect, backStyle, borderStyle, needPaint)
		{
        }
        #endregion

        #region StateCommon
        /// <summary>
        /// Gets access to the common control appearance that other states can override.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining common control appearance that other states can override.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDoubleRedirect StateCommon
        {
            get { return _stateCommon; }
        }

        private bool ShouldSerializeStateCommon()
        {
            return !_stateCommon.IsDefault;
        }
        #endregion
    
        #region StateInactive
        /// <summary>
        /// Gets access to the inactive form appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining inactive form appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble StateInactive
        {
            get { return _stateDisabled; }
        }

        private bool ShouldSerializeStateInactive()
        {
            return !_stateDisabled.IsDefault;
        }
        #endregion

        #region StateActive
        /// <summary>
        /// Gets access to the active form appearance entries.
        /// </summary>
        [KryptonPersist]
        [Category("Visuals")]
        [Description("Overrides for defining active form appearance.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaletteDouble StateActive
        {
            get { return _stateNormal; }
        }

        private bool ShouldSerializeStateActive()
        {
            return !_stateNormal.IsDefault;
        }
        #endregion
    }
}
