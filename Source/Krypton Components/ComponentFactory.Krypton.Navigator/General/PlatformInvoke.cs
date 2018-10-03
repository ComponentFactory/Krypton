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

namespace ComponentFactory.Krypton.Navigator
{
    internal class PI
    {
        #region Constants
        internal const uint WS_POPUP = 0x80000000;
        internal const int WS_EX_TOPMOST = 0x00000008;
        internal const int WS_EX_TOOLWINDOW = 0x00000080;
        internal const int WS_EX_LAYERED = 0x00080000;
        internal const int WM_CONTEXTMENU = 0x007B;
        internal const int WM_NCHITTEST = 0x0084;
        internal const int SW_HIDE = 0;
        internal const int SW_SHOWNOACTIVATE = 4;
        internal const int ULW_ALPHA = 0x00000002;
        internal const int HTTRANSPARENT = -1;
		internal const byte AC_SRC_OVER  = 0x00;
        internal const byte AC_SRC_ALPHA = 0x01;
        #endregion

        #region Static User32
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int ShowWindow(IntPtr hWnd, short cmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        #endregion

        #region Static Gdi32
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool DeleteDC(IntPtr hDC);
        #endregion

        #region Static Methods
        internal static int LOWORD(IntPtr value)
        {
            int int32 = ((int)value.ToInt64() & 0xFFFF);
            return (int32 > 32767) ? int32 - 65536 : int32;
        }

        internal static int HIWORD(IntPtr value)
        {
            int int32 = (((int)value.ToInt64() >> 0x10) & 0xFFFF);
            return (int32 > 32767) ? int32 - 65536 : int32;
        }

        internal static int LOWORD(int value)
        {
            return (value & 0xFFFF);
        }

        internal static int HIWORD(int value)
        {
            return ((value >> 0x10) & 0xFFFF);
        }
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        internal struct SIZE
        {
            public int cx;
            public int cy;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }
        #endregion
    }
}
