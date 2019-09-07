using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{
    /// <summary>
    /// The PlotLayout class manages locations of labels, scales, and the data area for a single plot.
    /// It is intended to be instantiated during a render and modified based on settings
    /// such as whether certain labels are shown and how large they are.
    /// </summary>
    public class PlotLayout
    {
        public readonly Region plotArea;
        public readonly Region title, labelX, labelY, labelY2;
        public readonly Region scaleX, scaleY, scaleY2;
        public readonly Region data;

        public PlotLayout(Bitmap bmp, Graphics gfx, Rectangle rect)
        {
            plotArea = new Region(bmp, gfx, rect);

            Rectangle rectZero = new Rectangle(0, 0, 0, 0);

            title = new Region(bmp, gfx, rectZero);
            labelX = new Region(bmp, gfx, rectZero);
            labelY = new Region(bmp, gfx, rectZero);
            labelY2 = new Region(bmp, gfx, rectZero);

            scaleX = new Region(bmp, gfx, rectZero);
            scaleY = new Region(bmp, gfx, rectZero);
            scaleY2 = new Region(bmp, gfx, rectZero);

            data = new Region(bmp, gfx, rectZero);
        }

        public void LabelRegions()
        {
            int transparency = 200;

            plotArea.Label("", Color.LightGray, transparency);

            title.Label("title", Color.Gray, transparency);
            labelX.Label("labelX", Color.Gray, transparency);
            labelY.Label("labelY", Color.Gray, transparency);
            labelY2.Label("labelY2", Color.Gray, transparency);

            scaleX.Label("scaleX", Color.Green, transparency);
            scaleY.Label("scaleY", Color.Green, transparency);
            scaleY2.Label("scaleY2", Color.Green, transparency);

            data.Label("data", Color.Magenta, transparency);
        }

        public void ShrinkToRemoveOverlaps()
        {
            scaleX.MatchX(data);
            labelX.MatchX(data);
            scaleY.MatchY(data);
            labelY.MatchY(data);
            scaleY2.MatchY(data);
            labelY2.MatchY(data);
        }
    }
}
