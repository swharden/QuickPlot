using System;
using System.Collections.Generic;
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
        public Edge edge;
        public FontSettings fs;

        public Axis(Edge edge)
        {
            this.edge = edge;
            fs = new FontSettings(10);
            low = -10;
            high = 10;
        }

        public void Set(double low, double high)
        {
            this.low = low;
            this.high = high;
        }
    }
}
