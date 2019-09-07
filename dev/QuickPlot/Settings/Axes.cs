using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{
    public class Axes
    {
        public AxisLabel labelTitle, labelX, labelY, labelY2;

        // controls whether scales are plotted
        public bool enableX = true;
        public bool enableY = true;
        public bool enableY2 = false;

        public Axes()
        {
            labelTitle = new AxisLabel(fontSize: 12, bold: true);
            labelX = new AxisLabel(fontSize: 10);
            labelY = new AxisLabel(fontSize: 10);
            labelY2 = new AxisLabel(fontSize: 10);
        }
    }
}
