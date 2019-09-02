using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    public static class TestImage
    {
        public static Bitmap Dimensions(int width, int height, Color? bg = null, Color? fg = null)
        {
            bg = bg ?? Color.White;
            fg = fg ?? Color.Black;

            Bitmap bmp = new Bitmap(width, height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.Clear((Color)bg);
            gfx.DrawRectangle(new Pen((Color)fg), 0, 0, width - 1, height - 1);
            var font = new Font(FontFamily.GenericMonospace, 10);
            gfx.DrawString($"{width} x {height}", font, Brushes.Black, 0, 0);
            return bmp;
        }
    }
}
