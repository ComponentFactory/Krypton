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
    /// Implementation for the close button for mdi child form.
    /// </summary>
    public class ButtonSpecMdiChildClose : ButtonSpecMdiChildFixed
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecMdiChildClose class.
        /// </summary>
        /// <param name="ribbon">Reference to owning ribbon control.</param>
        public ButtonSpecMdiChildClose(KryptonRibbon ribbon)
            : base(PaletteButtonSpecStyle.PendantClose)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;
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
            // Cannot be seen if not attached to an mdi child window and cannot be seen
            // if the window is not maximized and so needing the pendant buttons
            if ((MdiChild == null) || !CommonHelper.IsFormMaximized(MdiChild))
                return false;

            // Have all buttons been turned off?
            if (!MdiChild.ControlBox)
                return false;

            return true;
        }

        /// <summary>
        /// Gets the button enabled state.
        /// </summary>
        /// <param name="palette">Palette to use for inheriting values.</param>
        /// <returns>Button enabled state.</returns>
        public override ButtonEnabled GetEnabled(IPalette palette)
        {
            return ButtonEnabled.True;
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

        #region Protected Overrides
        /// <summary>
        /// Raises the Click event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            // Only if associated view is enabled to we perform an action
            if (GetViewEnabled())
            {
                if (!_ribbon.InDesignMode)
                {
                    MdiChild.Close();

                    // Let base class fire any other attached events
                    base.OnClick(e);
                }
            }
        }
        #endregion
    }
}
