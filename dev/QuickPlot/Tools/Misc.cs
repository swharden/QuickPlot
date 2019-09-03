using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Tools
{
    public static class Misc
    {
        public static Color RandomColor
        {
            get
            {
                Random rand = new Random();
                Color color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                return color;
            }
        }

        public static Rectangle Contract(Rectangle rect, int x, int y)
        {
            rect.X += x;
            rect.Width -= x * 2;
            rect.Y += y;
            rect.Height -= y * 2;
            return rect;
        }
    }
}
