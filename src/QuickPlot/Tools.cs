using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot
{
    public static class Tools
    {
        public static SKColor IndexedColor10(int index)
        {
            string[] plottableColors10 = new string[] {
                "#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd",
                "#8c564b", "#e377c2", "#7f7f7f", "#bcbd22", "#17becf"
            };
            return SKColor.Parse(plottableColors10[index % plottableColors10.Length]);
        }
    }
}
