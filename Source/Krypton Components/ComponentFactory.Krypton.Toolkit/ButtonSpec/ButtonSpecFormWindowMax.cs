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

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Implementation for the fixed maximize button for krypton form.
    /// </summary>
    public class ButtonSpecFormWindowMax : ButtonSpecFormFixed
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecFormWindowMax class.
        /// </summary>
        /// <param name="form">Reference to owning krypton form instance.</param>
        public ButtonSpecFormWindowMax(KryptonForm form)
            : base(form, PaletteButtonSpecStyle.FormMax)
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
            // We do not show if the custom chrome is combined with composition,
            // in which case the form buttons are handled by the composition
            if (KryptonForm.ApplyComposition && KryptonForm.ApplyCustomChrome)
                return false;

            // The maximize button is never present on tool windows
            switch (KryptonForm.FormBorderStyle)
            {
                case FormBorderStyle.FixedToolWindow:
                case FormBorderStyle.SizableToolWindow:
                    return false;
            }

            // Have all buttons been turned off?
            if (!KryptonForm.ControlBox)
                return false;

            // Has the minimize/maximize buttons been turned off?
            if (!KryptonForm.MinimizeBox && !KryptonForm.MaximizeBox)
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
            // Has the maximize buttons been turned off?
            if (!KryptonForm.MaximizeBox)
                return ButtonEnabled.False;

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
                // If we do not provide an inert form
                if (!KryptonForm.InertForm)
                {
                    // Only if the mouse is still within the button bounds do we perform action
                    MouseEventArgs mea = (MouseEventArgs)e;
                    if (GetView().ClientRectangle.Contains(mea.Location))
                    {
                        // Toggle between maximized and restored
                        if (KryptonForm.WindowState == FormWindowState.Maximized)
                            KryptonForm.SendSysCommand(PI.SC_RESTORE);
                        else
                            KryptonForm.SendSysCommand(PI.SC_MAXIMIZE);

                        // Let base class fire any other attached events
                        base.OnClick(e);
                    }
                }
            }
        }
        #endregion
    }
}
