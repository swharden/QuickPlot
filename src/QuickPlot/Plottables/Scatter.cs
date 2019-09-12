using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.Plottables
{
    public class Scatter : Plottable
    {
        double[] xs, ys;

        public Scatter(double[] xs, double[] ys)
        {
            this.xs = xs;
            this.ys = ys;

            if (xs == null || ys == null)
                throw new ArgumentException("xs and ys cannot be null");

            if (xs.Length == 0 || ys.Length == 0)
                throw new ArgumentException("xs and ys must have at least 1 element");

            if (xs.Length != ys.Length)
                throw new ArgumentException("xs and ys must have the same length");
        }
    }
}
