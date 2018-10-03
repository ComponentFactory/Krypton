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
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    /// <summary>
    /// Implementation for the fixed mdi child pendant buttons.
    /// </summary>
    public abstract class ButtonSpecMdiChildFixed : ButtonSpec
    {
        #region Instance Fields
        private Form _mdiChild;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecMdiChildFixed class.
        /// </summary>
        /// <param name="fixedStyle">Fixed style to use.</param>
        public ButtonSpecMdiChildFixed(PaletteButtonSpecStyle fixedStyle)
        {
            // Fix the type
            ProtectedType = fixedStyle;
        }      
        #endregion   

        #region AllowComponent
        /// <summary>
        /// Gets a value indicating if the component is allowed to be selected at design time.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AllowComponent
        {
            get { return false; }
        }
        #endregion

        #region MdiChild
        /// <summary>
        /// Gets access to the owning krypton form.
        /// </summary>
        public Form MdiChild
        {
            get { return _mdiChild; }
            set { _mdiChild = value; }
        }
        #endregion

        #region ButtonSpecStype
        /// <summary>
        /// Gets and sets the actual type of the button.
        /// </summary>
        public PaletteButtonSpecStyle ButtonSpecType
        {
            get { return ProtectedType; }
            set { ProtectedType = value; }
        }
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button style.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button style.</returns>
        public override ButtonStyle GetStyle(IPalette palette)
        {
            return ButtonStyle.ButtonSpec;
        }
        #endregion
    }
}
