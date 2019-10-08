using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

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

        public override PlotSettings.AxisLimits GetDataArea()
        {
            PlotSettings.AxisLimits lim = new PlotSettings.AxisLimits(xs[0], ys[0]);
            for (int i = 1; i < xs.Length; i++)
                lim.Expand(xs[i], ys[i]);
            return lim;
        }

        public override void Render(Graphics gfx, PlotSettings.Axes axes)
        {
            gfx.Clip = new Region(axes.GetDataRect());

            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            Pen pen = new Pen(style.lineColor);
            Brush brush = new SolidBrush(style.markerColor);

            // draw lines
            for (int i = 1; i < xs.Length; i++)
            {
                PointF pt1 = axes.GetPixel(xs[i - 1], ys[i - 1]);
                PointF pt2 = axes.GetPixel(xs[i], ys[i]);

                try
                {
                    gfx.DrawLine(pen, pt1, pt2);
                }
                catch
                {
                    Debug.WriteLine($"scatter crashed drawing line {i}");
                }
            }

            // draw markers
            for (int i = 0; i < xs.Length; i++)
            {
                PointF pt = axes.GetPixel(xs[i], ys[i]);
                gfx.FillEllipse(brush, pt.X - style.markerSize, pt.Y - style.markerSize, style.markerSize * 2, style.markerSize * 2);
            }

            gfx.ResetClip();
        }
    }
}
