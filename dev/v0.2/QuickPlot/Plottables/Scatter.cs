using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Plottables
{
    class Scatter : Plottable
    {
        double[] xs, ys;

        public Scatter(double[] xs, double[] ys, LineStyle ls, MarkerStyle ms)
        {
            if (xs == null || ys == null)
                throw new ArgumentException("xs and ys can not be null");

            if (xs.Length != ys.Length)
                throw new ArgumentException("xs and ys must have the same length");

            this.ls = ls;
            this.ms = ms;
            this.xs = xs;
            this.ys = ys;
        }

        public PointF[] GetPoints(Settings.Axes axes, Size bmpSize)
        {
            PointF[] points = new PointF[xs.Length];
            for (int i=0; i<xs.Length; i++)
            {
                float x = axes.axisX.GetPixel(xs[i], bmpSize.Width);
                float y = axes.axisY.GetPixel(ys[i], bmpSize.Height);
                points[i] = new PointF(x, y);
            }
            return points;
        }
    }
}
