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
        public PlotSettings.Axes axes = new PlotSettings.Axes();
        public PlotSettings.MouseTracker mouse = new PlotSettings.MouseTracker();

        public Plot()
        {

        }

        public void Render(SKCanvas canvas, SKRect rect)
        {
            // render this plot inside the rectangle of the canvas

            axes.SetRect(rect);

            PlotSettings.Axes axesAfterMouse = new PlotSettings.Axes(axes);
            axesAfterMouse.PanPixels(mouse.leftDownDelta);

            using (var paint = new SKPaint())
            {
                // draw a rectangle around the plot area
                paint.Color = SKColors.Black;
                paint.Style = SKPaintStyle.Stroke;
                canvas.DrawRect(rect, paint);
            }
            
            for (int i=0; i<plottables.Count; i++)
            {
                plottables[i].Render(canvas, axesAfterMouse);
            }
        }

        public void Scatter(double[] xs, double[] ys)
        {
            var scatterPlot = new Plottables.Scatter(xs, ys);
            plottables.Add(scatterPlot);
        }
    }
}
