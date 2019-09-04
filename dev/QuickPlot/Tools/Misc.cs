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

        public static StringFormat StringFormat(AlignHoriz h = AlignHoriz.center, AlignVert v = AlignVert.center)
        {
            StringFormat sf = new StringFormat();

            switch (h)
            {
                case AlignHoriz.left:
                    sf.Alignment = StringAlignment.Near;
                    break;
                case AlignHoriz.right:
                    sf.Alignment = StringAlignment.Far;
                    break;
                case AlignHoriz.center:
                    sf.Alignment = StringAlignment.Center;
                    break;
            }

            switch (v)
            {
                case AlignVert.top:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case AlignVert.bottom:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case AlignVert.center:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
            }

            return sf;
        }
    }
}
