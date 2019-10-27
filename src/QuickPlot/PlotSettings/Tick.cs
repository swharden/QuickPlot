using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    struct Tick
    {
        public double value;
        public string label;

        public Tick(double value, string label)
        {
            this.value = value;
            this.label = label;
        }
    }
}
