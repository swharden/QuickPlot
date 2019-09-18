using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot
{
    public class Plot
    {
        public PlotSettings.SubplotPosition subplotPosition;

        public List<Plottables.Plottable> plottables = new List<Plottables.Plottable>();
        public PlotSettings.Axes axes;
        public PlotSettings.MouseTracker mouse = new PlotSettings.MouseTracker();
        PlotSettings.Layout layout = new PlotSettings.Layout();

        public Plot()
        {

        }

        public void Render(SKCanvas canvas, SKRect rect)
        {

            if (axes == null)
                AutoAxis();

            // draw the layout for debugging
            layout.Tighten(rect);
            layout.Render(canvas);

            // modify the scale based on what the mouse is doing
            axes.SetRect(layout.data);

            // apply mouse adjustments to the scale
            PlotSettings.Axes axesAfterMouse = new PlotSettings.Axes(axes);
            axesAfterMouse.PanPixels(mouse.leftDownDelta);
            axesAfterMouse.ZoomPixels(mouse.rightDownDelta);

            // modify the scale based on what the mouse adjustments did
            axesAfterMouse.SetRect(layout.data);

            // draw inside a clipping rectangle
            canvas.Save();
            canvas.ClipRect(layout.data);
            for (int i = 0; i < plottables.Count; i++)
            {
                plottables[i].Render(canvas, axesAfterMouse);
            }
            canvas.Restore();
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
        }
    }
}
