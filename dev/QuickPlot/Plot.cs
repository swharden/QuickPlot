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

            // determine size of tick labels
            int scaleSizeL = 50;
            int scaleSizeR = 50;
            int scaleSizeB = 20;

            // create rectangles for all the scales

            gfx.FillRectangle(Brushes.White, rect);
            Tools.Misc.MarkRectangle(bmp, gfx, rect, Color.Blue, "plot");
            RenderLabels(bmp, gfx, rect);
            rect = Tools.Misc.Contract(rect, 20);

            Rectangle rectScaleL = rect;
            rectScaleL.Width = scaleSizeL;
            Tools.Misc.MarkRectangle(bmp, gfx, rectScaleL, Color.Green, "scaleL");

            Rectangle rectScaleB = rect;
            rectScaleB.Width = scaleSizeR;
            rectScaleB.X += (rect.Width - rectScaleB.Width);
            Tools.Misc.MarkRectangle(bmp, gfx, rectScaleB, Color.Blue, "scaleB");

            Rectangle rectScaleR = rect;
            rectScaleR.Height = scaleSizeB;
            rectScaleR.Y = rect.Y + rect.Height - rectScaleR.Height;
            Tools.Misc.MarkRectangle(bmp, gfx, rectScaleR, Color.Red, "scaleR");

            // determine how big the data area is
            Rectangle rectData = rect;
            rectData = Tools.Misc.Contract(rectData, left: scaleSizeL, right: scaleSizeR, bottom: scaleSizeB);
            Tools.Misc.MarkRectangle(bmp, gfx, rectData, Color.Black, "data");

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
