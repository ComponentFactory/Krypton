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
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using ComponentFactory.Krypton.Toolkit.Properties;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Show a wait dialog during long operations.
    /// </summary>
    [ToolboxItem(false)]
    public partial class ModalWaitDialog : Form, IMessageFilter
    {
        #region Static Fields
        private const int DELAY_SHOWING = 500;
        private const int DELAY_SPIN = 75;
        private const int SPIN_ANGLE = 20;
        private static readonly Bitmap _hourGlass = Resources.HourGlass;
        #endregion

        #region Instance Fields
        private bool _startTimestamped;
        private DateTime _startTimestamp;
        private DateTime _spinTimestamp;
        private float _spinAngle;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ModalWaitDialog class. 
        /// </summary>
        public ModalWaitDialog()
        {
            InitializeComponent();

            // Remove redraw flicker by using double buffering
            SetStyle(ControlStyles.DoubleBuffer | 
                     ControlStyles.AllPaintingInWmPaint, true);

            // Hook into dispach of windows messages
            Application.AddMessageFilter(this);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Raises the OnPaint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs containing event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Let base class perform standard painting
            base.OnPaint(e);

            // Start drawing offset by 16 pixels from each edge
            e.Graphics.TranslateTransform(32, 32);

            // Number of degrees to rotate the image around
            e.Graphics.RotateTransform(_spinAngle);

            // Perform actual draw of the image
            e.Graphics.DrawImage(_hourGlass, 
                                 -16, -16, 
                                 _hourGlass.Width, 
                                 _hourGlass.Height);

            // Must return the graphics instance in same state provided
            e.Graphics.ResetTransform();
        }
        #endregion

        #region Public
        /// <summary>
        /// Called periodically to update the wait dialog.
        /// </summary>
        public void UpdateDialog()
        {
            // Remember the first time the update is called
            if (!_startTimestamped)
            {
                _startTimestamped = true;
                _startTimestamp = DateTime.Now;
            }
            else
            {
                // If the dialog has not been shown yet
                if (!Visible)
                {
                    // Has initial delay expired?
                    if (DateTime.Now.Subtract(_startTimestamp).TotalMilliseconds > DELAY_SHOWING)
                    {
                        // Make this dialog visible to the user
                        Show();

                        // Start the spin timing
                        _spinTimestamp = DateTime.Now;
                    }
                }
                else
                {
                    // Has the spin delay expired?
                    if (DateTime.Now.Subtract(_spinTimestamp).TotalMilliseconds > DELAY_SPIN)
                    {
                        // Increase the spin angle by one notch
                        _spinAngle = (_spinAngle + SPIN_ANGLE) % 360;
                        
                        // Request the spin image be redrawn
                        Invalidate();

                        // Start the next spin timing
                        _spinTimestamp = DateTime.Now;
                    }
                }
            }

            // Let any repainting or processing events be dispatched
            Application.DoEvents();
        }

        /// <summary>
        /// Process windows messages before they are dispatched.
        /// </summary>
        /// <param name="m">Message to process.</param>
        /// <returns>True to suppress message dispatch; false otherwise.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool PreFilterMessage(ref Message m)
        {
            // Prevent mouse messages from activating any application windows
            if (((m.Msg >= 0x0200) && (m.Msg <= 0x0209)) || 
                ((m.Msg >= 0x00A0) && (m.Msg <= 0x00A9)))  
            {
                // Discover target control for message
                if (Control.FromHandle(m.HWnd) != null)
                {
                    // Find the form that the control is inside
                    Form f = Control.FromHandle(m.HWnd).FindForm();

                    // If the message is for this dialog then let it be dispatched
                    if ((f != null) && (f == this))
                        return false;
                }

                // Eat message to prevent dispatch
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}
