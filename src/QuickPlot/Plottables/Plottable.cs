using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickPlot.Plottables
{

    public abstract class Plottable
    {
        public Style style;
        public abstract void Render(Graphics gfx, PlotSettings.Axes axes);
        public abstract PlotSettings.AxisLimits GetDataArea();
    }
}
