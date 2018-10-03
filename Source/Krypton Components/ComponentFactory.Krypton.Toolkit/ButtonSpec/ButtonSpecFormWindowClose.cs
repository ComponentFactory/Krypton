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
using System.Reflection;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Implementation for the fixed close button for krypton form.
    /// </summary>
    public class ButtonSpecFormWindowClose : ButtonSpecFormFixed
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ButtonSpecFormWindowClose class.
        /// </summary>
        /// <param name="form">Reference to owning krypton form instance.</param>
        public ButtonSpecFormWindowClose(KryptonForm form)
            : base(form, PaletteButtonSpecStyle.FormClose)
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

            // Have all buttons been turned off?
            if (!KryptonForm.ControlBox)
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
                // If we do not provide an inert form
                if (!KryptonForm.InertForm)
                {
                    // Only if the mouse is still within the button bounds do we perform action
                    MouseEventArgs mea = (MouseEventArgs)e;
                    if (GetView().ClientRectangle.Contains(mea.Location))
                    {
                        PropertyInfo pi = typeof(Form).GetProperty("CloseReason",
                                                                    BindingFlags.Instance |
                                                                    BindingFlags.SetProperty |
                                                                    BindingFlags.NonPublic);

                        // Update form with the reason for the close
                        pi.SetValue(KryptonForm, CloseReason.UserClosing, null);

                        // Convert screen position to LPARAM format of WM_SYSCOMMAND message
                        Point screenPos = Control.MousePosition;
                        IntPtr lParam = (IntPtr)(PI.MAKELOWORD(screenPos.X) |
                                                 PI.MAKEHIWORD(screenPos.Y));

                        // Request the form be closed down
                        KryptonForm.SendSysCommand(PI.SC_CLOSE, lParam);

                        // Let base class fire any other attached events
                        base.OnClick(e);
                    }
                }
            }
        }
        #endregion
    }
}
