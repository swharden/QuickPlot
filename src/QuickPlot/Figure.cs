using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    public class Figure
    {

        public Figure()
        {
        }

        public enum RenderFramework { GDI, Skia, ImageSharp };

        public System.Drawing.Bitmap GetBitmap(int width, int height, RenderFramework framework = RenderFramework.GDI)
        {
            using (var benchmark = new Benchmark())
            {
                if (framework == RenderFramework.GDI)
                {
                    return Render.BitmapGDI(this, width, height);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
