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

        public PlotLayout(Rectangle rect)
        {
            plotArea = new Region(rect);

            title = new Region();
            labelX = new Region();
            labelY = new Region();
            labelY2 = new Region();

            scaleX = new Region();
            scaleY = new Region();
            scaleY2 = new Region();

            data = new Region();
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
