using System;
using System.Collections.Generic;
using System.Text;
using QuickPlot.PlotSettings;
using SkiaSharp;

namespace QuickPlot.Plottables
{
    public class Scatter : Plottable
    {
        double[] xs, ys;

        public Scatter(double[] xs, double[] ys, Style style)
        {
            this.xs = xs;
            this.ys = ys;
            this.style = style;

            if (xs == null || ys == null)
                throw new ArgumentException("xs and ys cannot be null");

            if (xs.Length == 0 || ys.Length == 0)
                throw new ArgumentException("xs and ys must have at least 1 element");

            if (xs.Length != ys.Length)
                throw new ArgumentException("xs and ys must have the same length");
        }

        public override AxisLimits GetDataArea()
        {
            AxisLimits lim = new AxisLimits(xs[0], ys[0]);
            for (int i=1; i<xs.Length; i++)
                lim.Expand(xs[i], ys[i]);
            return lim;
        }

        public override void Render(SKCanvas canvas, PlotSettings.Axes axes)
        {
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;

                // todo: create a SKPoint array to reduce lookups

                // draw lines
                // todo: should SKPath be used instead of drawing individual lines?
                paint.Color = style.lineColor;
                for (int i = 1; i < xs.Length; i++)
                {
                    SKPoint pt1 = axes.GetPixel(xs[i - 1], ys[i - 1]);
                    SKPoint pt2 = axes.GetPixel(xs[i], ys[i]);
                    canvas.DrawLine(pt1, pt2, paint);
                }

                // draw markers
                paint.Color = style.markerColor;
                for (int i = 0; i < xs.Length; i++)
                {
                    SKPoint pt = axes.GetPixel(xs[i], ys[i]);
                    canvas.DrawCircle(pt, style.markerSize, paint);
                }
            }
        }
    }
}
