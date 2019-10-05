using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{
    /// <summary>
    /// This class helps determine positioning of Subplots in a Figure
    /// </summary>
    public static class FigureLayout
    {
        /// <summary>
        /// Return a list of subplot rectangles
        /// </summary>
        public static List<Rectangle> GetSubplotlotRectangles(Size figSize, List<Plot> plots, int verticalSpacing = 5, int horizontalSpacing = 5)
        {
            int maxRow = 1;
            int maxCol = 1;
            foreach (var plot in plots)
            {
                maxRow = Math.Max(plot.subplotSettings.rowIndex + 1, maxRow);
                maxCol = Math.Max(plot.subplotSettings.colIndex + 1, maxCol);
            }

            List<Rectangle> rects = new List<Rectangle>();
            for (int i = 0; i < plots.Count; i++)
            {
                Region region = new Region(plots[i].subplotSettings.GetDimensions(figSize, maxRow, maxCol));
                region.ShrinkBy(top: verticalSpacing, bottom: verticalSpacing);
                region.ShrinkBy(left: horizontalSpacing, right: horizontalSpacing);

                rects.Add(region.rect);
            }
            return rects;
        }
    }
}
