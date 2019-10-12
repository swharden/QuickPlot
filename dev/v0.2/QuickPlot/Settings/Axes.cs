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
        public Axis axisX, axisY, axisY2;

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

            axisX = new Axis(Axis.Edge.bottom);
            axisY = new Axis(Axis.Edge.left);
            axisY2 = new Axis(Axis.Edge.right);
        }

        public void Set(double x1, double x2, double y1, double y2)
        {
            axisX.low = x1;
            axisX.high = x2;
            axisY.low = y1;
            axisY.high = y2;
        }
    }
}
