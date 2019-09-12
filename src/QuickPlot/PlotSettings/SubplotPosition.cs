using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class SubplotPosition
    {
        public int subPlotNumber;
        public int nRows, nCols;
        public int rowSpan, colSpan;

        public SubplotPosition(int nRows, int nCols, int subPlotNumber, int rowSpan = 1, int colSpan = 1)
        {
            if (subPlotNumber < 1)
                throw new ArgumentException("subplot numbers start at 1");

            this.subPlotNumber = subPlotNumber;
            this.nRows = nRows;
            this.nCols = nCols;
            this.rowSpan = rowSpan;
            this.colSpan = colSpan;
        }
    }
}
