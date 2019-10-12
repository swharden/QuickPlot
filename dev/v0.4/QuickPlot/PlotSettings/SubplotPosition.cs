using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class SubplotPosition
    {
        public readonly double widthFrac, heightFrac;
        public readonly double topFrac, leftFrac, botFrac, rightFrac;

        public bool isFullSize { get { return ((widthFrac == 1) && (heightFrac == 1)); } }

        /// <summary>
        /// Calculates the fractional area of a subplot given its position on an imaginary grid
        /// </summary>
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
            rightFrac = leftFrac + widthFrac;
            botFrac = topFrac + heightFrac;
        }

        /// <summary>
        /// Return the pixel area covered by this subplot
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
