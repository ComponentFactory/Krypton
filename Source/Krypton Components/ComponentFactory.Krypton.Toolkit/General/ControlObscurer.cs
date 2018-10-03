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
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{    
    /// <summary>
    /// Used to obscrure an area of the screen to hide form changes underneath.
    /// </summary>
    [ToolboxItem(false)]
    public class ScreenObscurer : IDisposable
    {
        #region ObscurerForm
        private class ObscurerForm : Form
        {
            #region Identity
            public ObscurerForm()
            {
                // Prevent automatic positioning of the window
                StartPosition = FormStartPosition.Manual;
                Location = new Point(-int.MaxValue, -int.MaxValue);
                Size = Size.Empty;

                // We do not want any window chrome
                FormBorderStyle = FormBorderStyle.None;

                // We do not want a taskbar entry for this temporary window
                ShowInTaskbar = false;
            }
            #endregion

            #region Public
            public void ShowForm(Rectangle screenRect)
            {
                // Our initial position should overlay exactly the container
                SetBounds(screenRect.X, 
                          screenRect.Y,
                          screenRect.Width, 
                          screenRect.Height);

                // Show the window without activating it (i.e. do not take focus)
                PI.ShowWindow(Handle, (short)PI.SW_SHOWNOACTIVATE);
            }
            #endregion

            #region Protected
            /// <summary>
            /// Raises the PaintBackground event.
            /// </summary>
            /// <param name="e">An PaintEventArgs containing the event data.</param>
            protected override void OnPaintBackground(PaintEventArgs e)
            {
                // We do nothing, so the area underneath shows through
            }

            /// <summary>
            /// Raises the Paint event.
            /// </summary>
            /// <param name="e">An PaintEventArgs containing the event data.</param>
            protected override void OnPaint(PaintEventArgs e)
            {
                // We do nothing, so the area underneath shows through
            }
            #endregion
        }
        #endregion

        #region Static Fields
        private ObscurerForm _obscurer;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ControlObscurer class.
        /// </summary>
        public ScreenObscurer()
        {
            // First time needed, create the top level obscurer window
            if (_obscurer == null)
                _obscurer = new ObscurerForm();
        }

        /// <summary>
        /// Initialize a new instance of the ControlObscurer class.
        /// </summary>
        /// <param name="f">Form to obscure.</param>
        /// <param name="designMode">Is the source in design mode.</param>
        public ScreenObscurer(Form f, bool designMode)
        {
            // Check the incoming form is valid
            if ((f != null) && !f.IsDisposed && !designMode)
            {
                // First time needed, create the top level obscurer window
                if (_obscurer == null)
                    _obscurer = new ObscurerForm();

                // We need a control to work with!
                if (f != null)
                    _obscurer.ShowForm(f.Bounds);
            }
        }

        /// <summary>
        /// Initialize a new instance of the ControlObscurer class.
        /// </summary>
        /// <param name="c">Control to obscure.</param>
        /// <param name="designMode">Is the source in design mode.</param>
        public ScreenObscurer(Control c, bool designMode)
        {
            // Check the incoming control is valid
            if ((c != null) && !c.IsDisposed && !designMode)
            {
                // First time needed, create the top level obscurer window
                if (_obscurer == null)
                    _obscurer = new ObscurerForm();

                // We need a control to work with!
                if (c != null)
                    _obscurer.ShowForm(c.RectangleToScreen(c.ClientRectangle));
            }
        }

        /// <summary>
        /// Use the obscurer to cover the provided control.
        /// </summary>
        /// <param name="f">Form to obscure.</param>
        public void Cover(Form f)
        {
            // Check the incoming form is valid
            if ((f != null) && !f.IsDisposed)
            {
                // Show over top of the provided form
                if (_obscurer != null)
                    _obscurer.ShowForm(f.Bounds);
            }
        }

        /// <summary>
        /// Use the obscurer to cover the provided control.
        /// </summary>
        /// <param name="c">Control to obscure.</param>
        public void Cover(Control c)
        {
            // Check the incoming control is valid
            if ((c != null) && !c.IsDisposed)
            {
                // Show over top of the provided control
                if (_obscurer != null)
                    _obscurer.ShowForm(c.RectangleToScreen(c.ClientRectangle));
            }
        }

        /// <summary>
        /// If covering an area then uncover it now.
        /// </summary>
        public void Uncover()
        {
            if (_obscurer != null)
                _obscurer.Hide();
        }

        /// <summary>
        /// Hide the obscurer from display.
        /// </summary>
        public void Dispose()
        {
            if (_obscurer != null)
            {
                _obscurer.Hide();
                _obscurer.Dispose();
                _obscurer = null;
            }
        }
        #endregion
    }
}
