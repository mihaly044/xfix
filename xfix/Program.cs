using System;
using System.Runtime.InteropServices;
using System.Text;

namespace wfix
{
    class Program
    {
        public enum Gwl
        {
            //GwlWndproc = (-4),
            //GwlHinstance = (-6),
            //GwlHwndparent = (-8),
            //GwlStyle = (-16),
            GwlExstyle = (-20),
            //GwlUserdata = (-21),
            //GwlId = (-12)
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder buf, int nMaxCount);

        protected delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        
        [DllImport("user32.dll")]
        protected static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        protected static bool EnumTheWindows(IntPtr hWnd, IntPtr lParam)
        {
            var size = 256;
            var sb = new StringBuilder(size);
            GetClassName(hWnd, sb, size);
            if (sb.ToString() == "X")
            {
                SetWindowLong32(new HandleRef(null, hWnd), (int)Gwl.GwlExstyle, 0x00000040);

                return true;
            }
            return true;
        }
        static void Main(string[] args)
        {
            EnumWindows(EnumTheWindows, IntPtr.Zero);
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
