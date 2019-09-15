using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class AxisLimits
    {
        public double xLow, xHigh, yLow, yHigh;

        public AxisLimits(double initialX, double initialY)
        {
            Set(initialX, initialY);
        }

        public void Set(double x, double y)
        {
            xLow = x;
            xHigh = x;
            yLow = y;
            yHigh = y;
        }

        public void Expand(double x, double y)
        {
            xLow = Math.Min(xLow, x);
            xHigh = Math.Max(xHigh, x);
            yLow = Math.Min(yLow, y);
            yHigh = Math.Max(yHigh, y);
        }
    }
}
