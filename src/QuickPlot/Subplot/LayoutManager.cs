using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Subplot
{
    public static class LayoutManager
    {
        public static List<SKRect> SubPlotRectangles(List<Plot> subplots, SKCanvas canvas, int padding = 5)
        {
            int nRows = 1;
            int nCols = 1;

            foreach (var subplot in subplots)
            {
                nRows = Math.Max(subplot.subplotPos.nRows, nRows);
                nCols = Math.Max(subplot.subplotPos.nCols, nCols);
            }

            float spWidth = canvas.LocalClipBounds.Width / nCols;
            float spHeight = canvas.LocalClipBounds.Height / nRows;

            List<SKRect> rects = new List<SKRect>();
            foreach (var plot in subplots)
            {
                float col = (plot.subplotPos.subPlotNumber - 1) % nCols;
                float row = (plot.subplotPos.subPlotNumber - 1) / nCols;

                float left = col * spWidth;
                float top = row * spHeight;
                float width = spWidth * plot.subplotPos.colSpan;
                float height = spHeight * plot.subplotPos.rowSpan;

                SKRect rect = new SKRect(left, top, left + width, top + height);

                // add between-plot padding
                rect.Left += padding;
                rect.Right -= padding;
                rect.Top += padding;
                rect.Bottom -= padding;

                // add padding around edges of the frame
                if (row == 0)
                    rect.Top += padding;
                if ((row + plot.subplotPos.rowSpan) == nRows)
                    rect.Bottom -= padding;
                if (col == 0)
                    rect.Left += padding;
                if ((col + plot.subplotPos.colSpan) == nCols)
                    rect.Right -= padding;

                rects.Add(rect);
            }

            return rects;
        }
    }
}
