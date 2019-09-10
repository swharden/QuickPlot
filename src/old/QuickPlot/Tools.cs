using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace QuickPlot
{
    public static class Tools
    {
        public static SKColor RandomColor(byte alpha = 128, int? seed = null)
        {
            Random rand = (seed == null) ? new Random() : new Random((int)seed);

            return new SKColor(
                    red: (byte)(rand.NextDouble() * 255),
                    green: (byte)(rand.NextDouble() * 255),
                    blue: (byte)(rand.NextDouble() * 255),
                    alpha: alpha
                );
        }

        public static SKColor IndexedColor10(int index)
        {
            string[] plottableColors10 = new string[] {
                "#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd",
                "#8c564b", "#e377c2", "#7f7f7f", "#bcbd22", "#17becf"
            };
            return SKColor.Parse(plottableColors10[index % plottableColors10.Length]);
        }

        public static SKPaint RandomPaint()
        {
            SKPaint paint = new SKPaint { Color = RandomColor() };
            return paint;
        }

        public static SKPaint IndexedPaint(int index)
        {
            SKPaint paint = new SKPaint { Color = IndexedColor10(index) };
            return paint;
        }

        public static SKPaint BlackPaint
        {
            get
            {
                SKColor color = new SKColor(0, 0, 0, 255);
                SKPaint paint = new SKPaint { Color = color };
                return paint;
            }
        }
    }
}
