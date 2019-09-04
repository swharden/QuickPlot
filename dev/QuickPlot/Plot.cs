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
            Region plotRegion = new Region(bmp, gfx, rect);
            plotRegion.Label("plotArea", Color.Gray, 100);

            // determine size of tick labels
            int scaleSizeL = 50;
            int scaleSizeR = 50;
            int scaleSizeB = 20;

            // add a little padding to be pretty
            plotRegion.Contract(20);

            Region scaleLeftRegion = new Region(plotRegion);
            scaleLeftRegion.Width = scaleSizeL;
            scaleLeftRegion.Label("scaleL", Color.Green, 100);

            Region scaleRightRegion = new Region(plotRegion);
            scaleRightRegion.Width = scaleSizeR;
            scaleRightRegion.X = plotRegion.X2 - scaleSizeR; // TODO: TrimTo(right: 100)
            scaleRightRegion.Label("scaleR", Color.Orange, 100);

            Region scaleBottomRegion = new Region(plotRegion);
            scaleBottomRegion.Height = scaleSizeB;
            scaleBottomRegion.Y += plotRegion.Height - scaleSizeB;
            scaleBottomRegion.Label("scaleB", Color.Blue, 100);

            // determine how big the data area is
            Region dataRegion = new Region(plotRegion);
            dataRegion.Contract(left: scaleSizeL, right: scaleSizeR, bottom: scaleSizeB);
            dataRegion.Label("data", Color.Magenta, 100);

            Console.WriteLine(dataRegion);

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
