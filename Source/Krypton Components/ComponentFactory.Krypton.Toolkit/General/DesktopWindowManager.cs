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
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Helper routines for interacting with the Desktop Window Manager.
    /// </summary>
    public class DWM
    {
        #region Static Methods
        /// <summary>
        /// Is composition currently enabled for the deskop.
        /// </summary>
        public static bool IsCompositionEnabled
        {
            get
            {
                // Desktop composition is only available on Vista upwards
                if (Environment.OSVersion.Version.Major < 6)
                    return false;
                else
                {
                    // Ask the desktop window manager is composition is currently enabled
                    bool compositionEnabled = false;
                    PI.DwmIsCompositionEnabled(ref compositionEnabled);
                    return compositionEnabled;
                }
            }
        }

        /// <summary>
        /// Change the distance the frame extends into the client area.
        /// </summary>
        /// <param name="hWnd">Window handle of form.</param>
        /// <param name="padding">Distance for each form edge.</param>
        public static void ExtendFrameIntoClientArea(IntPtr hWnd, Padding padding)
        {
            Debug.Assert(hWnd != null);

            // Cerate structure that contains distances for each edge
            PI.MARGINS margins = new PI.MARGINS();
            margins.leftWidth = padding.Left;
            margins.topHeight = padding.Top;
            margins.rightWidth = padding.Right;
            margins.bottomHeight = padding.Bottom;

            // Request change from the desktop window manager
            PI.DwmExtendFrameIntoClientArea(hWnd, ref margins);
        }
        #endregion
    }
}
