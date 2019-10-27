using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class Axis
    {
        public double low, high;
        public double span { get { return high - low; } }
        public double center { get { return (high + low) / 2.0; } }
        public bool isValid { get { return (low < high); } }
        public bool display = true; // TODO: replace this with layout fixed sizes

        public Axis(double low = 0, double high = 0)
        {
            Set(low, high);
        }

        public void Set(double? low, double? high)
        {
            this.low = low ?? this.low;
            this.high = high ?? this.high;
        }

        public void Pan(double delta)
        {
            low += delta;
            high += delta;
        }

        public void Zoom(double frac = 1, double? zoomTo = null)
        {
            zoomTo = zoomTo ?? center;
            double spanLeft = (double)zoomTo - low;
            double spanRight = high - (double)zoomTo;
            low = (double)zoomTo - spanLeft / frac;
            high = (double)zoomTo + spanRight / frac;
        }
    }
}
