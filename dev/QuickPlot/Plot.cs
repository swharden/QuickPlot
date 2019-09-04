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
            gfx.FillRectangle(Brushes.White, rect);
            //gfx.DrawRectangle(Pens.Black, rect);
            RenderLabels(bmp, gfx, rect);
            return bmp;
        }

        private void RenderLabels(Bitmap bmp, Graphics gfx, Rectangle rect)
        {
            // title
            gfx.DrawString($"{labels.top}",
                font: new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                brush: Brushes.Black,
                x: rect.X + (rect.Width / 2),
                y: rect.Y,
                format: Tools.Misc.StringFormat(AlignHoriz.center, AlignVert.top)
                );

            // lower axis label
            gfx.DrawString($"{labels.bottom}",
                font: new Font(FontFamily.GenericSansSerif, 10),
                brush: Brushes.Black,
                x: rect.X + (rect.Width / 2),
                y: rect.Y + rect.Height,
                format: Tools.Misc.StringFormat(AlignHoriz.center, AlignVert.bottom)
                );

            // left axis label
            gfx.RotateTransform(-90);
            gfx.DrawString(labels.left,
                font: new Font(FontFamily.GenericSansSerif, 10),
                brush: Brushes.Black,
                x: -rect.Y - (rect.Height / 2),
                y: rect.X,
                format: Tools.Misc.StringFormat(AlignHoriz.center, AlignVert.top)
                );
            gfx.ResetTransform();

            // right axis label
            gfx.RotateTransform(-90);
            gfx.DrawString(labels.right,
                font: new Font(FontFamily.GenericSansSerif, 10),
                brush: Brushes.Black,
                x: -rect.Y - (rect.Height / 2),
                y: rect.X + rect.Width,
                format: Tools.Misc.StringFormat(AlignHoriz.center, AlignVert.bottom)
                );
            gfx.ResetTransform();
        }
    }
}
