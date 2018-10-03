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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Docking
{
    internal class PI
    {
        #region Constants
        internal const uint WS_POPUP = 0x80000000;
        internal const uint WS_CLIPSIBLINGS = 0x04000000;
        internal const uint WS_CLIPCHILDREN = 0x02000000;
        internal const int WS_EX_TOOLWINDOW = 0x00000080;
        internal const int WS_EX_WINDOWEDGE = 0x00000100;
        internal const int WM_KILLFOCUS = 0x0008;
        internal const int WM_PAINT = 0x000F;
        internal const int WM_NCHITTEST = 0x0084;
        internal const int WM_NCLBUTTONDOWN = 0x00A1;
        internal const int WM_NCLBUTTONUP = 0x00A2;
        internal const int WM_NCLBUTTONDBLCLK = 0x00A3;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_SYSKEYDOWN = 0x0104;
        internal const int WM_MOUSEMOVE = 0x0200;
        internal const int WM_LBUTTONUP = 0x0202;
        internal const int WM_MOUSELEAVE = 0x02A3;
        internal const int VK_ESCAPE = 0x1B;
        internal const int TME_LEAVE = 0x00000002;
        internal const int HITTEST_CAPTION = 0x00000002;
        internal const int KEY_NONE = 0;
        internal const int KEY_DOWN = 1;
        internal const int KEY_TOGGLED = 2;
        #endregion

        #region Static User32
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern uint SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetKeyState(int keyCode);
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        internal class POINTC
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct TRACKMOUSEEVENTS
        {
            public uint cbSize;
            public uint dwFlags;
            public IntPtr hWnd;
            public uint dwHoverTime;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Is the specified key currently pressed down.
        /// </summary>
        /// <param name="key">Key to test.</param>
        /// <returns>True if pressed; otherwise false.</returns>
        public static bool IsKeyDown(Keys key)
        {
            return KEY_DOWN == (GetKeyState(key) & KEY_DOWN);
        }

        /// <summary>
        /// Is the specified key currently toggled.
        /// </summary>
        /// <param name="key">Key to test.</param>
        /// <returns>True if toggled; otherwise false.</returns>
        public static bool IsKeyToggled(Keys key)
        {
            return KEY_TOGGLED == (GetKeyState(key) & KEY_TOGGLED);
        }

        private static int GetKeyState(Keys key)
        {
            int state = KEY_NONE;

            short retVal = GetKeyState((int)key);

            if ((retVal & 0x8000) == 0x8000)
                state |= KEY_DOWN;

            if ((retVal & 1) == 1)
                state |= KEY_TOGGLED;

            return state;
        }

        #endregion
    }
}
