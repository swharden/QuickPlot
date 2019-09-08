using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{

    /// <summary>
    /// Axis limits (and methods to modify them) for a single axis
    /// </summary>
    public class Axis
    {
        public enum Edge { top, bottom, left, right };

        public double low, high;
        public double span { get { return high - low; } }
        public Edge edge;
        public FontSettings fs;
        public Ticks ticks;

        public Axis(Edge edge)
        {
            this.edge = edge;
            fs = new FontSettings(10);
            low = -7;
            high = 7;
        }

        public void Set(double low, double high)
        {
            this.low = low;
            this.high = high;
        }

        public void GenerateTicks(int pixelSpan)
        {
            ticks = new Ticks(low, high, pixelSpan);
        }

        public float GetPixel(double value, int pixelSpan)
        {
            double pixelsPerUnit = pixelSpan / span;
            return (float)((value - low) * pixelsPerUnit);
        }

        public double GetLocation(float pixel, int pixelSpan)
        {
            double unitsPerPixel = span / pixelSpan;
            return low + unitsPerPixel * pixel;
        }
    }
}
