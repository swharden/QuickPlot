using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Subplot
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

    public class Plot
    {
        public SubplotPosition subplotPos;

        public Plot()
        {
            subplotPos = new SubplotPosition(1, 1, 1);
        }
    }
}
