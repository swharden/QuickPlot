using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    public class Plot
    {
        public Settings.Padding padding;
        public Settings.Labels labels;

        public Bitmap Render(Bitmap bmp, Graphics gfx, Rectangle rect)
        {
            // render the current plot within the rectangle of the larger image
            gfx.FillRectangle(Brushes.White, rect);
            //gfx.DrawRectangle(Pens.Black, rect);
            gfx.DrawString($"{labels.top}",
                 new Font(FontFamily.GenericMonospace, 10),
                 Brushes.Black, rect.X, rect.Y);
            return bmp;
        }

        private void RenderLabels()
        {

        }
    }
}
