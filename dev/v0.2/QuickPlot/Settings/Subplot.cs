using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{
    public class Subplot
    {
        public int rowIndex, colIndex;
        public int rowSpan, colSpan;

        public Subplot(int row, int col, int rowSpan = 1, int colSpan = 1)
        {
            this.rowIndex = row - 1;
            this.colIndex = col - 1;
            this.rowSpan = rowSpan;
            this.colSpan = colSpan;
        }

        public Rectangle GetDimensions(Size figSize, int nRows, int nCols)
        {
            int subPlotWidth = figSize.Width / nCols;
            int subPlotHeight = figSize.Height / nRows;

            Point subPlotOrigin = new Point(colIndex * subPlotWidth, rowIndex * subPlotHeight);

            Size subPlotSize = new Size(subPlotWidth, subPlotHeight);

            subPlotSize.Width *= rowSpan;
            subPlotSize.Height *= colSpan;

            return new Rectangle(subPlotOrigin, subPlotSize);
        }
    }
}
