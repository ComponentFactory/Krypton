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
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class PI
    {
        #region Constants
        internal const uint WS_POPUP = 0x80000000;
        internal const uint WS_SYSMENU = 0x00080000;
        internal const int WM_NCLBUTTONDOWN = 0x00A1;
        internal const int WM_NCRBUTTONDOWN = 0x00A4;
        internal const int WM_NCMBUTTONDOWN = 0x00A7;
        internal const int WM_NCHITTEST = 0x0084;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        internal const int WM_SYSKEYDOWN = 0x0104;
        internal const int WM_SYSKEYUP = 0x0105;
        internal const int WM_LBUTTONDOWN = 0x0201;
        internal const int WM_RBUTTONDOWN = 0x0204;
        internal const int WM_MBUTTONDOWN = 0x0207;
        internal const int WM_MOUSEWHEEL = 0x020A;
        internal const int WM_GETTITLEBARINFOEX = 0x033F;
        internal const int WS_CLIPCHILDREN = 0x02000000;
        internal const int WS_EX_TOPMOST = 0x00000008;
        internal const int WS_EX_TOOLWINDOW = 0x00000080;
        internal const int WS_EX_LAYERED = 0x00080000;
        internal const int WS_EX_TRANSPARENT = 0x00000020;
        internal const int SW_HIDE = 0;
        internal const int SW_SHOWNOACTIVATE = 4;
        internal const int MESSAGE_BEEP_ERROR = 0x0010;
        internal const int HTTRANSPARENT = -1;
        internal const int HTCAPTION = 0x02;
        internal const int DTT_COMPOSITED = 8192;
        internal const int DTT_GLOWSIZE = 2048;
        internal const int DTT_TEXTCOLOR = 1;
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

        #region Static DwmApi
        [DllImport("dwmapi.dll, CharSet = CharSet.Auto")]
        internal static extern int DwmDefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr result);
        #endregion

        #region Static User32
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SetMenu(HandleRef hWnd, HandleRef hMenu);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint nLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ShowCaret(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int ShowWindow(IntPtr hWnd, short cmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool MessageBeep(int type);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern uint SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TITLEBARINFOEX lParam);
        #endregion

        #region Static Gdi32
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int ExcludeClipRect(IntPtr hDC, int x1, int y1, int x2, int y2);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateDIBSection(IntPtr hDC, BITMAPINFO pBMI, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern bool DeleteDC(IntPtr hDC);
        #endregion

        #region Static Uxtheme
        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        internal static extern bool IsAppThemed();

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        internal static extern bool IsThemeActive();

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        internal static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hDC, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions);
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct TITLEBARINFOEX
        {
            public uint cbSize;
            public RECT rcTitleBar;
            public uint dwTitleBar;
            public uint dwReserved;
            public uint dwMinButton;
            public uint dwMaxButton;
            public uint dwHelpButton;
            public uint dwCloseButton;
            public RECT rcReserved1;
            public RECT rcReserved2;
            public RECT rcMinButton;
            public RECT rcMaxButton;
            public RECT rcHelpButton;
            public RECT rcCloseButton;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct DTTOPTS
        {
            public int dwSize;
            public int dwFlags;
            public int crText;
            public int crBorder;
            public int crShadow;
            public int iTextShadowType;
            public POINT ptShadowOffset;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            public bool fApplyOverlay;
            public int iGlowSize;
            public int pfnDrawTextCallback;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class BITMAPINFO
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }
        #endregion
    }
}
