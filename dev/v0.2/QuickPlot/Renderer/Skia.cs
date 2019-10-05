using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Renderer
{
    public static class Skia
    {
        public static Bitmap Render(Bitmap bmp, Rectangle rect, Plot plt)
        {
            // To add Skia support:
            //   - draw onto the Bitmap that was passed-in, then return it.
            //   - the Plot passed-in has all the styling, scale, and data required to create the plot.
            //   - Crete any methods you want to render it. There's no inheritance, so you don't have to mimic how GDI does it.
            //   - Note that the bitmap returned gets assigned to a picturebox.Image

            return bmp;
        }
    }
}
