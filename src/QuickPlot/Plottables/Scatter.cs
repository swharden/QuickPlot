using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using QuickPlot.PlotSettings;
using SkiaSharp;

namespace QuickPlot.Plottables
{
    public class Scatter : Plottable
    {
        double[] xs, ys;
        Style style;

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
            PlotSettings.AxisLimits lim = new PlotSettings.AxisLimits(xs[0], ys[0]);
            for (int i = 1; i < xs.Length; i++)
                lim.Expand(xs[i], ys[i]);
            return lim;
        }

        public override void Render(SKCanvas canvas, PlotSettings.Axes axes)
        {
            canvas.Save();
            canvas.ClipRect(axes.GetDataRect());

            using (SKPath linePath = new SKPath())
            {
                linePath.MoveTo(axes.GetPixel(xs[0], ys[0]));
                // draw lines
                for (int i = 1; i < xs.Length; i++)
                {
                    linePath.LineTo(axes.GetPixel(xs[i], ys[i]));
                }
                style.paint.Style = SKPaintStyle.Stroke;
                canvas.DrawPath(linePath, style.paint);
            }
            // draw markers
            using (SKPath markerPath = new SKPath())
            {
                for (int i = 0; i < xs.Length; i++)
                {
                    SKPoint pt = axes.GetPixel(xs[i], ys[i]);
                    markerPath.AddCircle(pt.X, pt.Y, 3);
                }

                // using fill style produce memory leak's Stroke style works, but looks bad
                //style.paint.Style = SKPaintStyle.Fill;

                canvas.DrawPath(markerPath, style.paint);
            }
            canvas.Restore();
        }
    }
}
