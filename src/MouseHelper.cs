using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;
namespace AstrOrzPlayer
{
    class MouseHelper
    {
        private static System.Windows.Point mousePosition;   
        public static int CheckCount;  
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool GetCursorPos(out MPoint mpt);
        public static System.Windows.Point GetMousePoint()
        {
            MPoint mpt = new MPoint();
            GetCursorPos(out mpt);
            System.Windows.Point p = new System.Windows.Point(mpt.X, mpt.Y);
            return p;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct MPoint
        {
            public int X;
            public int Y;
            public MPoint(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        public static bool HaveMove()
        {
            System.Windows.Point point = GetMousePoint();
            if (point == mousePosition) return false;
            mousePosition = point; return true;
        }
    }
}
