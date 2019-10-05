using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class SubplotPosition
    {
        public readonly double widthFrac, heightFrac;
        public readonly double topFrac, leftFrac;

        public SubplotPosition(int nRows, int nCols, int subPlotNumber, int rowSpan = 1, int colSpan = 1)
        {
            if (subPlotNumber < 1)
                throw new ArgumentException("subplot numbers start at 1");

            widthFrac = (double)colSpan / nCols;
            heightFrac = (double)rowSpan / nRows;

            double columnIndex = (subPlotNumber - 1) % nCols;
            double rowIndex = (subPlotNumber - 1) / nCols;

            leftFrac = columnIndex * widthFrac;
            topFrac = rowIndex * heightFrac;
        }

        /// <summary>
        /// Return the area covered by this subplot
        /// </summary>
        public RectangleF GetRectangle(double widthPx, double heightPx)
        {
            return new RectangleF(
                    x: (float)(leftFrac * widthPx),
                    y: (float)(topFrac * heightPx),
                    width: (float)(widthFrac * widthPx),
                    height: (float)(heightFrac * heightPx)
                );
        }
    }
}
