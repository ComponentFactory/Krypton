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
using System.Diagnostics;
using System.Collections.Generic;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class KeyTipControl : Form
    {
        #region Instance Fields
        private KryptonRibbon _ribbon;
        private List<ViewDrawRibbonKeyTip> _viewList;
        private string _prefix;
        private bool _showDisabled;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KeyTipControl class.
        /// </summary>
        /// <param name="ribbon">Reference to owning control instance.</param>
        /// <param name="keyTips">List of key tips.</param>
        /// <param name="showDisabled">True to show disabled entries, otherwise enabled.</param>
        public KeyTipControl(KryptonRibbon ribbon, 
                             KeyTipInfoList keyTips,
                             bool showDisabled)
        {
            _ribbon = ribbon;
            _showDisabled = showDisabled;

            // Update form properties so we do not have a border and do not show
            // in the task bar. We draw the background in Magenta and set that as
            // the transparency key so it is a show through window.
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TransparencyKey = Color.Magenta;
            
            // Disabled key tips are show semi-transparent
            if (_showDisabled)
                Opacity = 0.5f;

            // Define the initial set of key tips
            SetKeyTips(keyTips);
        }
        #endregion

        #region Public
        /// <summary>
        /// Define the set of key tips to display.
        /// </summary>
        /// <param name="keyTips">List of key tips.</param>
        public void SetKeyTips(KeyTipInfoList keyTips)
        {
            // Create a new list of key tip views
            _viewList = new List<ViewDrawRibbonKeyTip>();

            Rectangle enclosingRect = Rectangle.Empty;

            // Create a view per key tip definition
            foreach (KeyTipInfo keyTip in keyTips)
            {
                // Create the initial rect as enclosing just the single point
                if (enclosingRect.IsEmpty)
                    enclosingRect = new Rectangle(keyTip.ScreenPt, new Size(1, 1));
                else
                {
                    // Enlarge the rect to enclose the new point
                    if (keyTip.ScreenPt.X < enclosingRect.Left)
                    {
                        int diff = enclosingRect.Left - keyTip.ScreenPt.X;
                        enclosingRect.Width += diff;
                        enclosingRect.X -= diff;
                    }

                    if (keyTip.ScreenPt.X > enclosingRect.Right)
                        enclosingRect.Width += (keyTip.ScreenPt.X - enclosingRect.Right);

                    if (keyTip.ScreenPt.Y < enclosingRect.Top)
                    {
                        int diff = enclosingRect.Top - keyTip.ScreenPt.Y;
                        enclosingRect.Height += diff;
                        enclosingRect.Y -= diff;
                    }

                    if (keyTip.ScreenPt.Y > enclosingRect.Bottom)
                        enclosingRect.Height += (keyTip.ScreenPt.Y - enclosingRect.Bottom);
                }

                _viewList.Add(new ViewDrawRibbonKeyTip(keyTip,
                                                       _ribbon.StateCommon.RibbonKeyTip.Back,
                                                       _ribbon.StateCommon.RibbonKeyTip.Border,
                                                       _ribbon.StateCommon.RibbonKeyTip.Content));
            }

            // Inflate the enclosing rect to account for maximum expected key tip
            enclosingRect.Inflate(50, 50);

            // Remove any prefix characters
            _prefix = string.Empty;

            // Our position covers the enclosing rect
            SetBounds(enclosingRect.X,
                      enclosingRect.Y,
                      enclosingRect.Width,
                      enclosingRect.Height);

            StartTimer();
        }

        /// <summary>
        /// Process the incoming key as being pressed.
        /// </summary>
        /// <param name="key">Key data.</param>
        public void AppendKeyPress(char key)
        {
            // We only use uppercase characters
            key = char.ToUpper(key);

            // Find the new prefix with the additional key
            string newPrefix = _prefix + key;

            // Search for any keytip that is an exact match
            foreach (ViewDrawRibbonKeyTip viewKeyTip in _viewList)
                if (viewKeyTip.KeyTipInfo.KeyString.Equals(newPrefix))
                {
                    // Invoke the target
                    viewKeyTip.KeyTipInfo.KeyTipSelect(_ribbon);
                    return;
                }

            // Search to see if any keytip has this as a prefix
            bool found = false;
            foreach (ViewDrawRibbonKeyTip viewKeyTip in _viewList)
            {
                if (viewKeyTip.KeyTipInfo.KeyString.StartsWith(newPrefix))
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                // Append the character to the prefix string
                _prefix += key;

                // Hide ourself and then show again to force redraw
                PI.ShowWindow(Handle, (short)PI.SW_HIDE);

                // Use timer to force redraw
                StartTimer();
            }
            else
            {
                // Escape key is not considered an error character
                if (key != 27)
                {
                    // Issue a windows error sound
                    PI.MessageBeep(PI.MESSAGE_BEEP_ERROR);
                }
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the creation parameters.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Parent = IntPtr.Zero;
                cp.Style |= unchecked((int)PI.WS_POPUP);
                cp.ExStyle |= PI.WS_EX_TOPMOST + PI.WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        /// <summary>
        /// Raises the PaintBackground event.
        /// </summary>
        /// <param name="pevent">An PaintEventArgs containing the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Magenta is the transparent color
            pevent.Graphics.FillRectangle(Brushes.Magenta, pevent.ClipRectangle);
        }

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">An PaintEventArgs containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            using (ViewLayoutContext layoutContext = new ViewLayoutContext(this, _ribbon.Renderer))
                foreach (ViewDrawRibbonKeyTip viewKeyTip in _viewList)
                {
                    // Only interested in correct enabled state items
                    if ((_showDisabled && !viewKeyTip.KeyTipInfo.Enabled) ||
                        (!_showDisabled && viewKeyTip.KeyTipInfo.Enabled))
                    {
                        bool visible = viewKeyTip.KeyTipInfo.Visible;

                        // Only make the view visible if the key tip matches the prefix
                        if (visible && !string.IsNullOrEmpty(_prefix))
                            visible = viewKeyTip.KeyTipInfo.KeyString.StartsWith(_prefix);

                        // Update with latest enabled/visible state
                        viewKeyTip.Visible = visible;
                        viewKeyTip.Enabled = viewKeyTip.KeyTipInfo.Enabled;

                        // Find the requested size
                        Size viewSize = viewKeyTip.GetPreferredSize(layoutContext);

                        // Convert the requested screen point of key tip to client
                        Point clientPt = PointToClient(viewKeyTip.KeyTipInfo.ScreenPt);

                        // Center the child at the requested screen position
                        clientPt.X -= (viewSize.Width / 2);
                        clientPt.Y -= (viewSize.Height / 2);

                        // Position the child at the calculated position
                        layoutContext.DisplayRectangle = new Rectangle(clientPt, viewSize);
                        viewKeyTip.Layout(layoutContext);
                    }
                }

            using (RenderContext renderContext = new RenderContext(this, e.Graphics, e.ClipRectangle, _ribbon.Renderer))
                foreach (ViewDrawRibbonKeyTip viewKeyTip in _viewList)
                    if (viewKeyTip.Visible)
                        viewKeyTip.Render(renderContext);
        }
        #endregion

        #region Implementation
        private void StartTimer()
        {
            // Start timer to take care of re drawing the display
            Timer redrawTimer = new Timer();
            redrawTimer.Interval = 1;
            redrawTimer.Tick += new EventHandler(OnRedrawTick);
            redrawTimer.Start();
        }

        private void OnRedrawTick(object sender, EventArgs e)
        {
            Timer redrawTimer = (Timer)sender;
            redrawTimer.Stop();
            redrawTimer.Dispose();

            // Show the window and so cause it to be redrawn
            if (!IsDisposed && (Handle != IntPtr.Zero))
                PI.ShowWindow(Handle, (short)PI.SW_SHOWNOACTIVATE);
        }
        #endregion
    }
}
