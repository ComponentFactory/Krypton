// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 4.5.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

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
        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ShowWindow")]
        private static extern int ShowWindowInvoke(IntPtr hWnd, short cmdShow);

        [SecuritySafeCritical]
        internal static int ShowWindow(IntPtr hWnd, short cmdShow)
        {
            return ShowWindowInvoke(hWnd, cmdShow);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDC")]
        private static extern IntPtr GetDCInvoke(IntPtr hWnd);

        [SecuritySafeCritical]
        internal static IntPtr GetDC(IntPtr hWnd)
        {
            return GetDCInvoke(hWnd);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "UpdateLayeredWindow")]
        private static extern bool UpdateLayeredWindowInvoke(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [SecuritySafeCritical]
        internal static bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags)
        {
            return UpdateLayeredWindowInvoke(hwnd, hdcDst, ref pptDst, ref psize, hdcSrc, ref pprSrc, crKey, ref pblend, dwFlags);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC")]
        private static extern int ReleaseDCInvoke(IntPtr hWnd, IntPtr hDC);

        [SecuritySafeCritical]
        internal static int ReleaseDC(IntPtr hWnd, IntPtr hDC)
        {
            return ReleaseDCInvoke(hWnd, hDC);
        }
        #endregion

        #region Static Gdi32
        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleDC")]
        private static extern IntPtr CreateCompatibleDCInvoke(IntPtr hDC);

        [SecuritySafeCritical]
        internal static IntPtr CreateCompatibleDC(IntPtr hDC)
        {
            return CreateCompatibleDCInvoke(hDC);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SelectObject")]
        private static extern IntPtr SelectObjectInvoke(IntPtr hDC, IntPtr hObject);

        [SecuritySafeCritical]
        internal static IntPtr SelectObject(IntPtr hDC, IntPtr hObject)
        {
            return SelectObjectInvoke(hDC, hObject);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject")]
        private static extern IntPtr DeleteObjectInvoke(IntPtr hObject);

        [SecuritySafeCritical]
        internal static IntPtr DeleteObject(IntPtr hObject)
        {
            return DeleteObjectInvoke(hObject);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteDC")]
        private static extern bool DeleteDCInvoke(IntPtr hDC);

        [SecuritySafeCritical]
        internal static bool DeleteDC(IntPtr hDC)
        {
            return DeleteDCInvoke(hDC);
        }
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
