using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace QuickPlot
{
    public class Plot
    {
        // keep the plottable list private.
        private List<Plottables.Plottable> plottables = new List<Plottables.Plottable>();

        // configuration objects are okay to be public
        public PlotSettings.SubplotPosition subplotPosition = new PlotSettings.SubplotPosition(1, 1, 1);
        public PlotSettings.Colors colors = new PlotSettings.Colors();

        // axes can be public, and null means it hasnt been set up
        public PlotSettings.Axes axes;

        public Plot()
        {
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

        public void Render(Bitmap bmp, RectangleF renderArea)
        {
            if (axes == null)
                AutoAxis();

            axes.SetRect(renderArea);

            // clear the render area
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.FillRectangle(new SolidBrush(colors.background), Rectangle.Round(renderArea));
            }

            // draw all the graphs
            for (int i = 0; i < plottables.Count; i++)
            {
                plottables[i].Render(bmp, axes);
            }

            // draw a frame around the figure
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.DrawRectangle(Pens.Black, Rectangle.Round(renderArea));
            }
        }
    }
}
