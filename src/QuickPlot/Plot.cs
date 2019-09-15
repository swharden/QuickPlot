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

        public Plot()
        {

        }

        public void Render(SKCanvas canvas, SKRect rect)
        {
            // render this plot inside the rectangle of the canvas
            using (var paint = new SKPaint())
            {
                // draw a rectangle around the plot area
                paint.Color = SKColors.Black;
                paint.Style = SKPaintStyle.Stroke;
                canvas.DrawRect(rect, paint);
            }

            if (axes == null)
                AutoAxis();

            // modify the scale based on what the mouse is doing
            axes.SetRect(rect);

            // apply mouse adjustments to the scale
            PlotSettings.Axes axesAfterMouse = new PlotSettings.Axes(axes);
            axesAfterMouse.PanPixels(mouse.leftDownDelta);
            axesAfterMouse.ZoomPixels(mouse.rightDownDelta);

            // modify the scale based on what the mouse adjustments did
            axesAfterMouse.SetRect(rect);

            // draw inside a clipping rectangle
            canvas.Save();
            canvas.ClipRect(rect);
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
