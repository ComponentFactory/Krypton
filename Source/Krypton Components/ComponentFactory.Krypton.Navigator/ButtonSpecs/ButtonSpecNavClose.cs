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

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Implementation for the fixed close button for navigator.
    /// </summary>
    public class ButtonSpecNavClose : ButtonSpecNavFixed
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecNavClose class.
        /// </summary>
        /// <param name="navigator">Reference to owning navigator instance.</param>
        public ButtonSpecNavClose(KryptonNavigator navigator)
            : base(navigator, PaletteButtonSpecStyle.Close)
        {
        }         
        #endregion

        #region IButtonSpecValues
        /// <summary>
        /// Gets the button visible value.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button visibiliy.</returns>
        public override bool GetVisible(IPalette palette)
        {
            switch (Navigator.Button.CloseButtonDisplay)
            {
                case ButtonDisplay.Hide:
                    // Always hide
                    return false;
                case ButtonDisplay.ShowDisabled:
                case ButtonDisplay.ShowEnabled:
                    // Always show
                    return true;
                case ButtonDisplay.Logic:
                    return true;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return false;
            }
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette)
        {
            switch (Navigator.Button.CloseButtonDisplay)
            {
                case ButtonDisplay.Hide:
                case ButtonDisplay.ShowDisabled:
                    // Always disabled
                    return ButtonEnabled.False;
                case ButtonDisplay.ShowEnabled:
                    // Always enabled
                    return ButtonEnabled.True;
                case ButtonDisplay.Logic:
                    // Only enabled if a page is selected
                    return (Navigator.SelectedPage != null) ? ButtonEnabled.True : ButtonEnabled.False;
                default:
                    // Should never happen!
                    Debug.Assert(false);
                    return ButtonEnabled.False;
            }
        }

        /// <summary>
        /// Gets the button checked state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button checked state.</returns>
        public override ButtonCheckState GetChecked(IPalette palette)
        {
            // Close button is never shown as checked
            return ButtonCheckState.NotCheckButton;
        }
        #endregion    
    }
}
