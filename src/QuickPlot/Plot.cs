using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace QuickPlot
{
    public class Plot
    {
        public PlotSettings.SubplotPosition subplotPosition;
        public PlotSettings.Axes axes;
        public List<Plottables.Plottable> plottables = new List<Plottables.Plottable>();
        //public PlotSettings.Layout layout = new PlotSettings.Layout();

        public Plot()
        {
            subplotPosition = new PlotSettings.SubplotPosition(1, 1, 1);
        }

        public void Scatter(double[] xs, double[] ys, Style style = null)
        {
            if (style == null)
                style = new Style(plottables.Count);
            var scatterPlot = new Plottables.Scatter(xs, ys, style);
            plottables.Add(scatterPlot);
        }

        public void AutoAxis(double marginX = .1, double marginY = .1)
        {
            if (axes == null)
                axes = new PlotSettings.Axes();

            if (plottables.Count > 0)
            {
                axes.Set(plottables[0].GetDataArea());
                for (int i = 1; i < plottables.Count; i++)
                    axes.Expand(plottables[i].GetDataArea());
            }

            axes.Zoom(1 - marginX, 1 - marginY);
            Debug.WriteLine($"AutoAxis left us with: {axes}");
        }

        public void Render(Bitmap bmp, RectangleF rect)
        {
            if (axes == null)
                AutoAxis();

            axes.SetRect(rect);

            for (int i = 0; i < plottables.Count; i++)
            {
                plottables[i].Render(bmp, axes);
            }
        }
    }
}
