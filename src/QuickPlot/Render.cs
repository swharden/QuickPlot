using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    class Render
    {
        public static System.Drawing.Bitmap BitmapGDI(Figure fig, int width, int height)
        {
            return TestImage.Dimensions(width, height);
        }
    }
}
