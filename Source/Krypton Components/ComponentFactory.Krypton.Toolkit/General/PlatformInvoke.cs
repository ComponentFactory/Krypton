// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2012. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 4.4.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class PI
    {
        #region Constants
        internal const uint WS_POPUP = 0x80000000;
        internal const uint WS_MINIMIZE = 0x20000000;
        internal const uint WS_MAXIMIZE = 0x01000000;
        internal const uint WS_VISIBLE = 0x10000000;
        internal const uint WS_BORDER = 0x00800000;
        internal const int PRF_CLIENT = 0x00000004;
        internal const int WS_EX_TOPMOST = 0x00000008;
        internal const int WS_EX_TOOLWINDOW = 0x00000080;
        internal const int WS_EX_LAYERED = 0x00080000;
        internal const int WS_EX_CLIENTEDGE = 0x00000200;
        internal const int SC_MINIMIZE = 0xF020;
        internal const int SC_MAXIMIZE = 0xF030;
        internal const int SC_CLOSE = 0xF060;
        internal const int SC_RESTORE = 0xF120;
        internal const int SW_SHOWNOACTIVATE = 4;
        internal const int WM_DESTROY = 0x0002;
        internal const int WM_NCDESTROY = 0x0082;
        internal const int WM_MOVE = 0x0003;
        internal const int WM_SETFOCUS = 0x0007;
        internal const int WM_KILLFOCUS = 0x0008;
        internal const int WM_SETREDRAW = 0x000B;
        internal const int WM_SETTEXT = 0x000C;
        internal const int WM_PAINT = 0x000F;
        internal const int WM_PRINTCLIENT = 0x0318;
        internal const int WM_CTLCOLOR = 0x0019;
        internal const int WM_ERASEBKGND = 0x0014;
        internal const int WM_MOUSEACTIVATE = 0x0021;
        internal const int WM_WINDOWPOSCHANGING = 0x0046;
        internal const int WM_WINDOWPOSCHANGED = 0x0047;
        internal const int WM_HELP = 0x0053;
        internal const int WM_NCCALCSIZE = 0x0083;
        internal const int WM_NCHITTEST = 0x0084;
        internal const int WM_NCPAINT = 0x0085;
        internal const int WM_NCACTIVATE = 0x0086;
        internal const int WM_NCMOUSEMOVE = 0x00A0;
        internal const int WM_NCLBUTTONDOWN = 0x00A1;
        internal const int WM_NCLBUTTONUP = 0x00A2;
        internal const int WM_NCLBUTTONDBLCLK = 0x00A3;
        internal const int WM_NCRBUTTONDOWN = 0x00A4;
        internal const int WM_NCMBUTTONDOWN = 0x00A7;
        internal const int WM_NCMBUTTONDBLCLK = 0x00A9;
        internal const int WM_SETCURSOR = 0x0020;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        internal const int WM_DEADCHAR = 0x0103;
        internal const int WM_SYSKEYDOWN = 0x0104;
        internal const int WM_SYSKEYUP = 0x0105;
        internal const int WM_SYSCHAR = 0x0106;
        internal const int WM_SYSDEADCHAR = 0x0107;
        internal const int WM_KEYLAST = 0x0108;
        internal const int WM_SYSCOMMAND = 0x0112;
        internal const int WM_HSCROLL = 0x0114;
        internal const int WM_VSCROLL = 0x0115;
        internal const int WM_INITMENU = 0x0116;
        internal const int WM_CTLCOLOREDIT = 0x0133;
        internal const int WM_MOUSEMOVE = 0x0200;
        internal const int WM_LBUTTONDOWN = 0x0201;
        internal const int WM_LBUTTONUP = 0x0202;
        internal const int WM_LBUTTONDBLCLK = 0x0203;
        internal const int WM_RBUTTONDOWN = 0x0204;
        internal const int WM_RBUTTONUP = 0x0205;
        internal const int WM_MBUTTONDOWN = 0x0207;
        internal const int WM_MBUTTONUP = 0x0208;
        internal const int WM_MOUSEWHEEL = 0x020A;
        internal const int WM_NCMOUSELEAVE = 0x02A2;
        internal const int WM_MOUSELEAVE = 0x02A3;
        internal const int WM_PRINT = 0x0317;
        internal const int WM_CONTEXTMENU = 0x007B;
        internal const int MA_NOACTIVATE = 0x03;
        internal const int EM_FORMATRANGE = 0x0439;
        internal const int SWP_NOSIZE = 0x0001;
        internal const int SWP_NOMOVE = 0x0002;
        internal const int SWP_NOZORDER = 0x0004;
        internal const int SWP_NOACTIVATE = 0x0010;
        internal const int SWP_FRAMECHANGED = 0x0020;
        internal const int SWP_NOOWNERZORDER = 0x0200;
        internal const int SWP_SHOWWINDOW = 0x0040;
        internal const int SWP_HIDEWINDOW = 0x0080;
        internal const int RDW_INVALIDATE = 0x0001;
        internal const int RDW_UPDATENOW = 0x0100;
        internal const int RDW_FRAME = 0x0400;
        internal const int DCX_WINDOW = 0x01;
        internal const int DCX_CACHE = 0x02;
        internal const int DCX_CLIPSIBLINGS = 0x10;
        internal const int DCX_INTERSECTRGN = 0x80;
        internal const int TME_LEAVE = 0x0002;
        internal const int TME_NONCLIENT = 0x0010;
        internal const int HTNOWHERE = 0x00;
        internal const int HTCLIENT = 0x01;
        internal const int HTCAPTION = 0x02;
        internal const int HTSYSMENU = 0x03;
        internal const int HTGROWBOX = 0x04;
        internal const int HTSIZE = 0x04;
        internal const int HTMENU = 0x05;
        internal const int HTLEFT = 0x0A;
        internal const int HTRIGHT = 0x0B;
        internal const int HTTOP = 0x0C;
        internal const int HTTOPLEFT = 0x0D;
        internal const int HTTOPRIGHT = 0x0E;
        internal const int HTBOTTOM = 0x0F;
        internal const int HTBOTTOMLEFT = 0x10;
        internal const int HTBOTTOMRIGHT = 0x11;
        internal const int HTBORDER = 0x12;
        internal const int HTHELP = 0x15;
        internal const int HTIGNORE = 0xFF;
        internal const int HTTRANSPARENT = -1;
        internal const int ULW_ALPHA = 0x00000002;
        internal const int DEVICE_BITSPIXEL = 12;
        internal const int DEVICE_PLANES = 14;
        internal const int SRCCOPY = 0xCC0020;
        internal const int GWL_STYLE = -16;
        internal const int DTM_SETMCCOLOR = 0x1006;
        internal const int DTT_COMPOSITED = 8192;
        internal const int DTT_GLOWSIZE = 2048;
        internal const int DTT_TEXTCOLOR = 1;
        internal const int MCSC_BACKGROUND = 0;
        internal const int PLANES = 14;
        internal const int BITSPIXEL = 12;
        internal const byte AC_SRC_OVER = 0x00;
        internal const byte AC_SRC_ALPHA = 0x01;
        internal const uint GW_HWNDFIRST = 0;
        internal const uint GW_HWNDLAST = 1;
        internal const uint GW_HWNDNEXT = 2;
        internal const uint GW_HWNDPREV = 3;
        internal const uint GW_OWNER = 4;
        internal const uint GW_CHILD = 5;
        internal const uint GW_ENABLEDPOPUP = 6;
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

        internal static int MAKELOWORD(int value)
        {
            return (value & 0xFFFF);
        }

        internal static int MAKEHIWORD(int value)
        {
            return ((value & 0xFFFF) << 0x10);
        }
        #endregion

        #region Static User32

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "PrintWindow")]
        private static extern bool PrintWindowInvoke(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [SecuritySafeCritical]
        internal static bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags)
        {
            return PrintWindowInvoke(hwnd, hDC, nFlags);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "VkKeyScan")]
        private static extern short VkKeyScanInvoke(char ch);

        [SecuritySafeCritical]
        internal static short VkKeyScan(char ch)
        {
            return VkKeyScanInvoke(ch);

        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "WindowFromPoint")]
        private static extern IntPtr WindowFromPointInvoke(PI.POINT pt);

        [SecuritySafeCritical]
        internal static IntPtr WindowFromPoint(PI.POINT pt)
        {
            return WindowFromPointInvoke(pt);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLongInvoke(IntPtr hWnd, int nIndex);

        [SecuritySafeCritical]
        internal static uint GetWindowLong(IntPtr hWnd, int nIndex)
        {
            return GetWindowLongInvoke(hWnd, nIndex);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLongInvoke(IntPtr hwnd, int nIndex, int nLong);

        [SecuritySafeCritical]
        internal static uint SetWindowLong(IntPtr hwnd, int nIndex, int nLong)
        {
            return SetWindowLongInvoke(hwnd, nIndex, nLong);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetActiveWindow")]
        private static extern IntPtr GetActiveWindowInvoke();

        [SecuritySafeCritical]
        internal static IntPtr GetActiveWindow()
        {
            return GetActiveWindowInvoke();
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ShowWindow")]
        private static extern int ShowWindowInvoke(IntPtr hWnd, short cmdShow);

        [SecuritySafeCritical]
        internal static int ShowWindow(IntPtr hWnd, short cmdShow)
        {
            return ShowWindowInvoke(hWnd, cmdShow);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetKeyState")]
        private static extern ushort GetKeyStateInvoke(int virtKey);

        [SecuritySafeCritical]
        internal static ushort GetKeyState(int virtKey)
        {
            return GetKeyStateInvoke(virtKey);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        private static extern uint SendMessageInvoke(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [SecuritySafeCritical]
        internal static uint SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
        {
            return SendMessageInvoke(hWnd, Msg, wParam, lParam);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowPos")]
        private static extern int SetWindowPosInvoke(IntPtr hWnd, IntPtr hWndAfter, int X, int Y, int Width, int Height, uint flags);

        [SecuritySafeCritical]
        internal static int SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int X, int Y, int Width, int Height, uint flags)
        {
            return SetWindowPosInvoke(hWnd, hWndAfter, X, Y, Width, Height, flags);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "RedrawWindow")]
        private static extern bool RedrawWindowInvoke(IntPtr hWnd, IntPtr rectUpdate, IntPtr hRgnUpdate, uint uFlags);

        [SecuritySafeCritical]
        internal static bool RedrawWindow(IntPtr hWnd, IntPtr rectUpdate, IntPtr hRgnUpdate, uint uFlags)
        {
            return RedrawWindowInvoke(hWnd, rectUpdate, hRgnUpdate, uFlags);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "RedrawWindow")]
        private static extern bool RedrawWindowInvoke(IntPtr hWnd, ref PI.RECT rectUpdate, IntPtr hRgnUpdate, uint uFlags);

        [SecuritySafeCritical]
        internal static bool RedrawWindow(IntPtr hWnd, ref PI.RECT rectUpdate, IntPtr hRgnUpdate, uint uFlags)
        {
            return RedrawWindowInvoke(hWnd, ref rectUpdate, hRgnUpdate, uFlags);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "TrackMouseEvent")]
        private static extern bool TrackMouseEventInvoke(ref TRACKMOUSEEVENTS tme);

        [SecuritySafeCritical]
        internal static bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme)
        {
            return TrackMouseEventInvoke(ref tme);
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
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDCEx")]
        private static extern IntPtr GetDCExInvoke(IntPtr hWnd, IntPtr hRgnClip, uint fdwOptions);

        [SecuritySafeCritical]
        internal static IntPtr GetDCEx(IntPtr hWnd, IntPtr hRgnClip, uint fdwOptions)
        {
            return GetDCExInvoke(hWnd, hRgnClip, fdwOptions);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowDC")]
        private static extern IntPtr GetWindowDCInvoke(IntPtr hwnd);

        [SecuritySafeCritical]
        internal static IntPtr GetWindowDC(IntPtr hwnd)
        {
            return GetWindowDCInvoke(hwnd);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowRect")]
        private static extern bool GetWindowRectInvoke(IntPtr hWnd, ref RECT rect);

        [SecuritySafeCritical]
        internal static bool GetWindowRect(IntPtr hWnd, ref RECT rect)
        {
            return GetWindowRectInvoke(hWnd, ref  rect);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC")]
        private static extern int ReleaseDCInvoke(IntPtr hWnd, IntPtr hDC);

        [SecuritySafeCritical]
        internal static int ReleaseDC(IntPtr hWnd, IntPtr hDC)
        {
            return ReleaseDCInvoke(hWnd, hDC);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DisableProcessWindowsGhosting")]
        private static extern void DisableProcessWindowsGhostingInvoke();

        [SecuritySafeCritical]
        internal static void DisableProcessWindowsGhosting()
        {
            DisableProcessWindowsGhostingInvoke();
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "AdjustWindowRectEx")]
        private static extern void AdjustWindowRectExInvoke(ref RECT rect, int dwStyle, bool hasMenu, int dwExSytle);

        [SecuritySafeCritical]
        internal static void AdjustWindowRectEx(ref RECT rect, int dwStyle, bool hasMenu, int dwExSytle)
        {
            AdjustWindowRectExInvoke(ref  rect, dwStyle, hasMenu, dwExSytle);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "MapWindowPoints")]
        private static extern int MapWindowPointsInvoke(IntPtr hWndFrom, IntPtr hWndTo, [In, Out]PI.POINTC pt, int cPoints);

        [SecuritySafeCritical]
        internal static int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out]PI.POINTC pt, int cPoints)
        {
            return MapWindowPointsInvoke(hWndFrom, hWndTo, pt, cPoints);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "TranslateMessage")]
        private static extern bool TranslateMessageInvoke([In] ref MSG lpMsg);

        [SecuritySafeCritical]
        internal static bool TranslateMessage([In] ref MSG lpMsg)
        {
            return TranslateMessageInvoke(ref lpMsg);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetClassName")]
        private static extern int GetClassNameInvoke(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [SecuritySafeCritical]
        internal static int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount)
        {
            return GetClassNameInvoke(hWnd, lpClassName, nMaxCount);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "BeginPaint")]
        private static extern IntPtr BeginPaintInvoke(IntPtr hwnd, ref PI.PAINTSTRUCT ps);

        [SecuritySafeCritical]
        internal static IntPtr BeginPaint(IntPtr hwnd, ref PI.PAINTSTRUCT ps)
        {
            return BeginPaintInvoke(hwnd, ref ps);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "EndPaint")]
        private static extern bool EndPaintInvoke(IntPtr hwnd, ref PI.PAINTSTRUCT ps);

        [SecuritySafeCritical]
        internal static bool EndPaint(IntPtr hwnd, ref PI.PAINTSTRUCT ps)
        {
            return EndPaintInvoke(hwnd, ref ps);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetClientRect")]
        private static extern bool GetClientRectInvoke(IntPtr hWnd, out RECT lpRect);

        [SecuritySafeCritical]
        internal static bool GetClientRect(IntPtr hWnd, out RECT lpRect)
        {
            return GetClientRectInvoke(hWnd, out lpRect);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "InflateRect")]
        private static extern bool InflateRectInvoke(ref RECT lprc, int dx, int dy);

        [SecuritySafeCritical]
        internal static bool InflateRect(ref RECT lprc, int dx, int dy)
        {
            return InflateRectInvoke(ref  lprc, dx, dy);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindow")]
        private static extern IntPtr GetWindowInvoke(IntPtr hWnd, uint uCmd);

        [SecuritySafeCritical]
        internal static IntPtr GetWindow(IntPtr hWnd, uint uCmd)
        {
            return GetWindowInvoke(hWnd, uCmd);
        }

        [SecurityCritical]
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "RegisterWindowMessage")]
        private static extern uint RegisterWindowMessageInvoke(string lpString);

        [SecuritySafeCritical]
        internal static uint RegisterWindowMessage(string lpString)
        {
            return RegisterWindowMessageInvoke(lpString);
        }
        #endregion

        #region Static Gdi32

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "BitBlt")]
        private static extern int BitBltInvoke(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [SecuritySafeCritical]
        internal static int BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop)
        {
            return BitBltInvoke(hDestDC, x, y, nWidth, nHeight, hSrcDC, xSrc, ySrc, dwRop);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleBitmap")]
        private static extern IntPtr CreateCompatibleBitmapInvoke(IntPtr hDC, int nWidth, int nHeight);

        [SecuritySafeCritical]
        internal static IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight)
        {
            return CreateCompatibleBitmapInvoke(hDC, nWidth, nHeight);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "ExcludeClipRect")]
        private static extern int ExcludeClipRectInvoke(IntPtr hDC, int x1, int y1, int x2, int y2);

        [SecuritySafeCritical]
        internal static int ExcludeClipRect(IntPtr hDC, int x1, int y1, int x2, int y2)
        {
            return ExcludeClipRectInvoke(hDC, x1, y1, x2, y2);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "IntersectClipRect")]
        private static extern int IntersectClipRectInvoke(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [SecuritySafeCritical]
        internal static int IntersectClipRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
        {
            return IntersectClipRectInvoke(hdc, nLeftRect, nTopRect, nRightRect, nBottomRect);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDeviceCaps")]
        private static extern int GetDeviceCapsInvoke(IntPtr hDC, int nIndex);

        [SecuritySafeCritical]
        internal static int GetDeviceCaps(IntPtr hDC, int nIndex)
        {
            return GetDeviceCapsInvoke(hDC, nIndex);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateDIBSection")]
        private static extern IntPtr CreateDIBSectionInvoke(IntPtr hDC, BITMAPINFO pBMI, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);

        [SecuritySafeCritical]
        internal static IntPtr CreateDIBSection(IntPtr hDC, BITMAPINFO pBMI, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset)
        {
            return CreateDIBSectionInvoke(hDC, pBMI, iUsage, ppvBits, hSection, dwOffset);
        }

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

        [SecurityCritical]
        [DllImport("gdi32.dll", EntryPoint = "SaveDC", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int SaveDCInvoke(HandleRef hDC);

        [SecuritySafeCritical]
        internal static int SaveDC(HandleRef hDC)
        {
            return SaveDCInvoke(hDC);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", EntryPoint = "RestoreDC", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern bool RestoreDCInvoke(HandleRef hDC, int nSavedDC);

        [SecuritySafeCritical]
        internal static bool RestoreDC(HandleRef hDC, int nSavedDC)
        {
            return RestoreDCInvoke(hDC, nSavedDC);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern bool GetViewportOrgExInvoke(HandleRef hDC, [In, Out]POINTC point);

        [SecuritySafeCritical]
        internal static bool GetViewportOrgEx(HandleRef hDC, [In, Out]POINTC point)
        {
            return GetViewportOrgExInvoke(hDC, point);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", EntryPoint = "CreateRectRgn", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr CreateRectRgnInvoke(int x1, int y1, int x2, int y2);

        [SecuritySafeCritical]
        internal static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
        {
            return CreateRectRgnInvoke(x1, y1, x2, y2);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int GetClipRgnInvoke(HandleRef hDC, HandleRef hRgn);

        [SecuritySafeCritical]
        internal static int GetClipRgn(HandleRef hDC, HandleRef hRgn)
        {
            return GetClipRgnInvoke(hDC, hRgn);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern bool SetViewportOrgExInvoke(HandleRef hDC, int x, int y, [In, Out]POINTC point);

        [SecuritySafeCritical]
        internal static bool SetViewportOrgEx(HandleRef hDC, int x, int y, [In, Out]POINTC point)
        {
            return SetViewportOrgExInvoke(hDC, x, y, point);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int GetRgnBoxInvoke(HandleRef hRegion, ref RECT clipRect);

        [SecuritySafeCritical]
        internal static int GetRgnBox(HandleRef hRegion, ref RECT clipRect)
        {
            return GetRgnBoxInvoke(hRegion, ref clipRect);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int CombineRgnInvoke(HandleRef hRgn, HandleRef hRgn1, HandleRef hRgn2, int nCombineMode);

        [SecuritySafeCritical]
        internal static int CombineRgn(HandleRef hRgn, HandleRef hRgn1, HandleRef hRgn2, int nCombineMode)
        {
            return CombineRgnInvoke(hRgn, hRgn1, hRgn2, nCombineMode);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int SelectClipRgnInvoke(HandleRef hDC, HandleRef hRgn);

        [SecuritySafeCritical]
        internal static int SelectClipRgn(HandleRef hDC, HandleRef hRgn)
        {
            return SelectClipRgnInvoke(hDC, hRgn);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int SelectClipRgnInvoke(IntPtr hDC, IntPtr hRgn);

        [SecuritySafeCritical]
        internal static int SelectClipRgn(IntPtr hDC, IntPtr hRgn)
        {
            return SelectClipRgnInvoke(hDC, hRgn);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern uint SetTextColorInvoke(IntPtr hdc, int crColor);

        [SecuritySafeCritical]
        internal static uint SetTextColor(IntPtr hdc, int crColor)
        {
            return SetTextColorInvoke(hdc, crColor);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern uint SetBkColorInvoke(IntPtr hdc, int crColor);

        [SecuritySafeCritical]
        internal static uint SetBkColor(IntPtr hdc, int crColor)
        {
            return SetBkColorInvoke(hdc, crColor);
        }

        [SecurityCritical]
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr CreateSolidBrushInvoke(int crColor);

        [SecuritySafeCritical]
        internal static IntPtr CreateSolidBrush(int crColor)
        {
            return CreateSolidBrushInvoke(crColor);
        }
        #endregion

        #region Static DwmApi
        [SecurityCritical]
        [DllImport("dwmapi.dll", CharSet = CharSet.Auto, EntryPoint = "DwmIsCompositionEnabled")]
        private static extern void DwmIsCompositionEnabledInvoke(ref bool enabled);

        [SecuritySafeCritical]
        internal static void DwmIsCompositionEnabled(ref bool enabled)
        {
            DwmIsCompositionEnabledInvoke(ref enabled);
        }

        [SecurityCritical]
        [DllImport("dwmapi.dll", CharSet = CharSet.Auto, EntryPoint = "DwmExtendFrameIntoClientArea")]
        private static extern int DwmExtendFrameIntoClientAreaInvoke(IntPtr hWnd, ref MARGINS pMarInset);

        [SecuritySafeCritical]
        internal static int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset)
        {
            return DwmExtendFrameIntoClientAreaInvoke(hWnd, ref pMarInset);
        }

        [SecurityCritical]
        [DllImport("dwmapi.dll", CharSet = CharSet.Auto, EntryPoint = "DwmDefWindowProc")]
        private static extern int DwmDefWindowProcInvoke(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr result);

        [SecuritySafeCritical]
        internal static int DwmDefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr result)
        {
            return DwmDefWindowProcInvoke(hWnd, msg, wParam, lParam, out result);
        }
        #endregion

        #region Static Ole32
        [SecurityCritical]
        [DllImport("ole32.dll", CharSet = CharSet.Auto, EntryPoint = "CoCreateGuid")]
        private static extern void CoCreateGuidInvoke(ref GUIDSTRUCT guid);

        [SecuritySafeCritical]
        internal static void CoCreateGuid(ref GUIDSTRUCT guid)
        {
            CoCreateGuidInvoke(ref guid);
        }
        #endregion

        #region Static Uxtheme
        [SecurityCritical]
        [DllImport("uxtheme.dll", CharSet = CharSet.Auto, EntryPoint = "IsAppThemed")]
        private static extern bool IsAppThemedInvoke();

        [SecuritySafeCritical]
        internal static bool IsAppThemed()
        {
            return IsAppThemedInvoke();
        }

        [SecurityCritical]
        [DllImport("uxtheme.dll", CharSet = CharSet.Auto, EntryPoint = "IsThemeActive")]
        private static extern bool IsThemeActiveInvoke();

        [SecuritySafeCritical]
        internal static bool IsThemeActive()
        {
            return IsThemeActiveInvoke();
        }

        [SecurityCritical]
        [DllImport("uxtheme.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowTheme")]
        private static extern int SetWindowThemeInvoke(IntPtr hWnd, String subAppName, String subIdList);

        [SecuritySafeCritical]
        internal static int SetWindowTheme(IntPtr hWnd, String subAppName, String subIdList)
        {
            return SetWindowThemeInvoke(hWnd, subAppName, subIdList);
        }

        [SecurityCritical]
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int DrawThemeTextExInvoke(IntPtr hTheme, IntPtr hDC, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions);

        [SecuritySafeCritical]
        internal static int DrawThemeTextEx(IntPtr hTheme, IntPtr hDC, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions)
        {
            return DrawThemeTextExInvoke(hTheme, hDC, iPartId, iStateId, text, iCharCount, dwFlags, ref pRect, ref pOptions);
        }
        #endregion

        #region Static Kernel32
        [SecurityCritical]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "QueryPerformanceCounter")]
        private static extern short QueryPerformanceCounterInvoke(ref long var);

        [SecuritySafeCritical]
        internal static short QueryPerformanceCounter(ref long var)
        {
            return QueryPerformanceCounterInvoke(ref  var);
        }

        [SecurityCritical]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "QueryPerformanceFrequency")]
        private static extern short QueryPerformanceFrequencyInvoke(ref long var);

        [SecuritySafeCritical]
        internal static short QueryPerformanceFrequency(ref long var)
        {
            return QueryPerformanceFrequencyInvoke(ref var);
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

        [StructLayout(LayoutKind.Sequential)]
        internal class POINTC
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct TRACKMOUSEEVENTS
        {
            public uint cbSize;
            public uint dwFlags;
            public IntPtr hWnd;
            public uint dwHoverTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct NCCALCSIZE_PARAMS
        {
            public RECT rectProposed;
            public RECT rectBeforeMove;
            public RECT rectClientBeforeMove;
            public int lpPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct GUIDSTRUCT
        {
            public ushort Data1;
            public ushort Data2;
            public ushort Data3;
            public ushort Data4;
            public ushort Data5;
            public ushort Data6;
            public ushort Data7;
            public ushort Data8;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MSG
        {
            public IntPtr hwnd;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
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

        [StructLayout(LayoutKind.Sequential)]
        internal struct PAINTSTRUCT
        {
            private IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CHARRANGE
        {
            public int cpMin;
            public int cpMax;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public RECT rc;
            public RECT rcPage;
            public CHARRANGE chrg;
        }
        #endregion
    }
}
