using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot
{
    public class Plot
    {
        public PlotSettings.SubplotPosition subplotPosition;

        private List<Plottables.Plottable> plottables = new List<Plottables.Plottable>();

        public Plot()
        {

        }

        public void Render(SKSurface surface, SKRect rect)
        {
            using (var paint = new SKPaint())
            {
                paint.Color = Tools.IndexedColor10(subplotPosition.subPlotNumber);
                surface.Canvas.DrawRect(rect, paint);
            }
        }

        public void Scatter(double[] xs, double[] ys)
        {
            var scatterPlot = new Plottables.Scatter(xs, ys);
            plottables.Add(scatterPlot);
        }
    }
}
