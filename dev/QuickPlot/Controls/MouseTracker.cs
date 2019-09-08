using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Controls
{
    public static class MouseTracker
    {
        public static Plot PlotUnderMouse(Figure fig, Point mousePos)
        {
            foreach (Plot plot in fig.subplots)
            {
                if (plot.layout.data.ContainsPoint(mousePos))
                    return plot;
            }
            return null;
        }

        public static void ZoomToPixel(Plot plt, Point pt1, Point pt2)
        {
            int xMin = Math.Min(pt1.X, pt2.X) - plt.layout.data.Point.X;
            int xMax = Math.Max(pt1.X, pt2.X) - plt.layout.data.Point.X;
            int yMin = Math.Min(pt1.Y, pt2.Y) - plt.layout.data.Point.Y;
            int yMax = Math.Max(pt1.Y, pt2.Y) - plt.layout.data.Point.Y;

            double x1 = plt.axes.axisX.GetLocation(xMin, plt.layout.data.Width);
            double x2 = plt.axes.axisX.GetLocation(xMax, plt.layout.data.Width);
            double y1 = plt.axes.axisX.GetLocation(yMin, plt.layout.data.Height);
            double y2 = plt.axes.axisX.GetLocation(yMax, plt.layout.data.Height);

            plt.axes.Set(x1, x2, y1, y2);
        }
    }
}
