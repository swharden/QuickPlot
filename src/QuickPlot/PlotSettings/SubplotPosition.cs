using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    /// <summary>
    /// Figure-level subplot position information stored in a plot-level object
    /// </summary>
    public class SubplotPosition
    {
        public double widthFrac { get; private set; }
        public double heightFrac { get; private set; }
        public double topFrac { get; private set; }
        public double leftFrac { get; private set; }
        public double botFrac { get; private set; }
        public double rightFrac { get; private set; }
        public bool isFullSize { get { return ((widthFrac == 1) && (heightFrac == 1)); } }

        public SubplotPosition(int nRows, int nCols, int subPlotNumber, int rowSpan = 1, int colSpan = 1)
        {
            if (subPlotNumber < 1)
                throw new ArgumentException("subplot numbers start at 1");
            Update(nRows, nCols, subPlotNumber, rowSpan, colSpan);
        }

        public void Update(int nRows, int nCols, int subPlotNumber, int rowSpan, int colSpan)
        {
            widthFrac = (double)colSpan / nCols;
            heightFrac = (double)rowSpan / nRows;

            double columnIndex = (subPlotNumber - 1) % nCols;
            double rowIndex = (subPlotNumber - 1) / nCols;

            leftFrac = columnIndex * widthFrac;
            topFrac = rowIndex * heightFrac;
            rightFrac = leftFrac + widthFrac;
            botFrac = topFrac + heightFrac;
        }

        public SKRect GetRectangle(SKSize figureSize)
        {
            float left = (float)(leftFrac * figureSize.Width);
            float top = (float)(topFrac * figureSize.Height);
            float width = (float)(widthFrac * figureSize.Width);
            float height = (float)(heightFrac * figureSize.Height);

            return new SKRect(left, top, left + width, top + height);
        }
    }
}
