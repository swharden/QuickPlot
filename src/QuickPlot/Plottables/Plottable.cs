using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.Plottables
{
    public abstract class Plottable
    {
        public abstract void Render(SKCanvas canvas, PlotSettings.Axes axes);
        public abstract PlotSettings.AxisLimits GetDataArea();
    }
}
