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

        public AxisLimits(Axes source)
        {
            xLow = source.x.low;
            xHigh = source.x.high;
            yLow = source.y.low;
            yHigh = source.y.high;
        }

        public override string ToString()
        {
            return $"AxisLimits: x1={xLow}, x2={xHigh}, y1={yLow}, y2={yHigh}";
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
