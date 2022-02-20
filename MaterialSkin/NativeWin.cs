using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MaterialSkin
{
    internal static class NativeWin
    {

        #region " CONSTS "

        // Padding
        internal const int EM_SETRECT = 0xB3;

        // Cursor flickering fix
        internal const int WM_SETCURSOR = 0x0020;

        internal const int WM_MOUSEWHEEL = 0x20A;

        internal const int IDC_HAND = 32649;

        #endregion


        #region " ENUMS "

        /// <summary>
        /// Window Messages
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues"/>
        /// </summary>
        internal enum WM
        {
            /// <summary>
            /// WM_NCCALCSIZE
            /// </summary>
            NonClientCalcSize = 0x0083,
            /// <summary>
            /// WM_NCACTIVATE
            /// </summary>
            NonClientActivate = 0x0086,
            /// <summary>
            /// WM_NCLBUTTONDOWN
            /// </summary>
            NonClientLeftButtonDown = 0x00A1,
            /// <summary>
            /// WM_SYSCOMMAND
            /// </summary>
            SystemCommand = 0x0112,
            /// <summary>
            /// WM_MOUSEMOVE
            /// </summary>
            MouseMove = 0x0200,
            /// <summary>
            /// WM_LBUTTONDOWN
            /// </summary>
            LeftButtonDown = 0x0201,
            /// <summary>
            /// WM_LBUTTONUP
            /// </summary>
            LeftButtonUp = 0x0202,
            /// <summary>
            /// WM_LBUTTONDBLCLK
            /// </summary>
            LeftButtonDoubleClick = 0x0203,
            /// <summary>
            /// WM_RBUTTONDOWN
            /// </summary>
            RightButtonDown = 0x0204,
        }


        /// <summary>
        /// Hit Test Results
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest"/>
        /// </summary>
        internal enum HT
        {
            /// <summary>
            /// HTNOWHERE - Nothing under cursor
            /// </summary>
            None = 0,
            /// <summary>
            /// HTCAPTION - Titlebar
            /// </summary>
            Caption = 2,
            /// <summary>
            /// HTLEFT - Left border
            /// </summary>
            Left = 10,
            /// <summary>
            /// HTRIGHT - Right border
            /// </summary>
            Right = 11,
            /// <summary>
            /// HTTOP - Top border
            /// </summary>
            Top = 12,
            /// <summary>
            /// HTTOPLEFT - Top left corner
            /// </summary>
            TopLeft = 13,
            /// <summary>
            /// HTTOPRIGHT - Top right corner
            /// </summary>
            TopRight = 14,
            /// <summary>
            /// HTBOTTOM - Bottom border
            /// </summary>
            Bottom = 15,
            /// <summary>
            /// HTBOTTOMLEFT - Bottom left corner
            /// </summary>
            BottomLeft = 16,
            /// <summary>
            /// HTBOTTOMRIGHT - Bottom right corner
            /// </summary>
            BottomRight = 17,
        }

        /// <summary>
        /// Window Styles
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles"/>
        /// </summary>
        internal enum WS
        {
            /// <summary>
            /// WS_MINIMIZEBOX - Allow minimizing from taskbar
            /// </summary>
            MinimizeBox = 0x20000,
            /// <summary>
            /// WS_SIZEFRAME - Required for Aero Snapping
            /// </summary>
            SizeFrame = 0x40000,
            /// <summary>
            /// WS_SYSMENU - Trigger the creation of the system menu
            /// </summary>
            SysMenu = 0x80000,
        }



        /// <summary>
        /// Track Popup Menu Flags
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-trackpopupmenu"/>
        /// </summary>
        internal enum TPM
        {
            /// <summary>
            /// TPM_LEFTALIGN
            /// </summary>
            LeftAlign = 0x0000,
            /// <summary>
            /// TPM_RETURNCMD
            /// </summary>
            ReturnCommand = 0x0100,
        }

        #endregion


        #region " STRUCTS "

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;

            private RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            internal RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
            {
            }

            internal int Height
            {
                get
                {
                    return Bottom - Top;
                }
            }
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class LogFont
        {
            public int lfHeight = 0;
            public int lfWidth = 0;
            public int lfEscapement = 0;
            public int lfOrientation = 0;
            public int lfWeight = 0;
            public byte lfItalic = 0;
            public byte lfUnderline = 0;
            public byte lfStrikeOut = 0;
            public byte lfCharSet = 0;
            public byte lfOutPrecision = 0;
            public byte lfClipPrecision = 0;
            public byte lfQuality = 0;
            public byte lfPitchAndFamily = 0;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName = string.Empty;
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct BlendFunction
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;

            internal BlendFunction(byte alpha)
            {
                BlendOp = 0;
                BlendFlags = 0;
                AlphaFormat = 0;
                SourceConstantAlpha = alpha;
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct BitMapInfo
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


        #region Low Level Windows Methods

        #region " USER32.DLL "

        private const string USER32_DLL = "user32.dll";

        /// <summary>
        ///     Provides a single method to call either the 32-bit or 64-bit method based on the size of an <see cref="IntPtr"/> for getting the
        ///     Window Style flags.<br/>
        ///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlongptra">GetWindowLongPtr</see>
        /// </summary>
        internal static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLongPtr64(hWnd, nIndex);
            else
                return GetWindowLong(hWnd, nIndex);
        }

        /// <summary>
        ///     Provides a single method to call either the 32-bit or 64-bit method based on the size of an <see cref="IntPtr"/> for setting the
        ///     Window Style flags.<br/>
        ///     <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowlongptra">SetWindowLongPtr</see>
        /// </summary>
        internal static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return SetWindowLong(hWnd, nIndex, dwNewLong.ToInt32());
        }

        [DllImport(USER32_DLL, EntryPoint = "GetWindowLong")]
        internal static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport(USER32_DLL, EntryPoint = "GetWindowLongPtr")]
        internal static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport(USER32_DLL, EntryPoint = "SetWindowLong")]
        internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport(USER32_DLL, EntryPoint = "SetWindowLongPtr")]
        internal static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);


        [DllImport(USER32_DLL, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport(USER32_DLL, CharSet = CharSet.Auto)]
        internal static extern IntPtr SetCursor(IntPtr hCursor);

        
        #region " USER32.DLL - SendMessage overloads"

        [DllImport(USER32_DLL)]
        internal static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport(USER32_DLL, CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        [DllImport(USER32_DLL)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, ref Point lParam);

        [DllImport(USER32_DLL, EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        internal static extern int SendMessageRefRect(IntPtr hWnd, uint msg, int wParam, ref RECT rect);

        [DllImport(USER32_DLL, SetLastError = false)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport(USER32_DLL, CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #endregion " USER32.DLL - SendMessage overloads"


        [DllImport(USER32_DLL)]
        internal static extern bool ReleaseCapture();

        [DllImport(USER32_DLL)]
        internal static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        [DllImport(USER32_DLL)]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport(USER32_DLL, CharSet = CharSet.Auto)]
        internal static extern int DrawText(IntPtr hdc, string lpchText, int cchText, ref RECT lprc, NativeTextRenderer.TextFormatFlags dwDTFormat);

        [DllImport(USER32_DLL, EntryPoint = "DrawTextW")]
        internal static extern int DrawText(IntPtr hdc, [MarshalAs(UnmanagedType.LPWStr)] string str, int len, ref RECT rect, uint uFormat);

        #endregion " USER32.DLL "


        #region " GDI32.DLL "

        private const string GDI32_DLL = "gdi32.dll";

        [DllImport(GDI32_DLL, EntryPoint = "CreateRoundRectRgn")]
        internal static extern IntPtr CreateRoundRectRgn(
                int nLeftRect,     // x-coordinate of upper-left corner
                int nTopRect,      // y-coordinate of upper-left corner
                int nRightRect,    // x-coordinate of lower-right corner
                int nBottomRect,   // y-coordinate of lower-right corner
                int nWidthEllipse, // width of ellipse
                int nHeightEllipse // height of ellipse
                );

        [DllImport(GDI32_DLL)]
        internal static extern int SetBkMode(IntPtr hdc, int mode);

        [DllImport(GDI32_DLL)]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiObj);

        [DllImport(GDI32_DLL)]
        internal static extern int SetTextColor(IntPtr hdc, int color);

        [DllImport(GDI32_DLL, EntryPoint = "GetTextExtentPoint32W")]
        internal static extern int GetTextExtentPoint32(IntPtr hdc, [MarshalAs(UnmanagedType.LPWStr)] string str, int len, ref Size size);

        [DllImport(GDI32_DLL, EntryPoint = "GetTextExtentExPointW")]
        internal static extern bool GetTextExtentExPoint(IntPtr hDc, [MarshalAs(UnmanagedType.LPWStr)] string str, int nLength, int nMaxExtent, int[] lpnFit, int[] alpDx, ref Size size);

        [DllImport(GDI32_DLL, EntryPoint = "TextOutW")]
        internal static extern bool TextOut(IntPtr hdc, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string str, int len);

        [DllImport(GDI32_DLL)]
        internal static extern int SetTextAlign(IntPtr hdc, uint fMode);

        [DllImport(GDI32_DLL)]
        internal static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport(GDI32_DLL)]
        internal static extern bool DeleteObject(IntPtr hObject);

        [DllImport(GDI32_DLL, ExactSpelling = true, SetLastError = true)]
        internal static extern bool DeleteDC(IntPtr hdc);

        [DllImport(GDI32_DLL, CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateFontIndirect([In, MarshalAs(UnmanagedType.LPStruct)] LogFont lplf);

        [DllImport(GDI32_DLL, ExactSpelling = true)]
        internal static extern IntPtr AddFontMemResourceEx(byte[] pbFont, int cbFont, IntPtr pdv, out uint pcFonts);

        [DllImport(GDI32_DLL)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

        [DllImport(GDI32_DLL, EntryPoint = "GdiAlphaBlend")]
        internal static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, BlendFunction blendFunction);

        [DllImport(GDI32_DLL, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport(GDI32_DLL)]
        internal static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BitMapInfo pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        #endregion " GDI32.DLL "

        #endregion
    }
}
