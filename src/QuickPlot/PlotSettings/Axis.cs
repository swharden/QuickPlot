using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class Axis
    {
        public double low, high;
        public double span { get { return high - low; } }

        public Axis(double low = -1, double high = 1)
        {
            Set(low, high);
        }

        public void Set(double? low, double? high)
        {
            this.low = low ?? this.low;
            this.high = high ?? this.high;
        }

        public void Pan(double delta)
        {
            low += delta;
            high += delta;
        }
    }
}
