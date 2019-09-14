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

        public static void DrawRandomLines(SKCanvas canvas, SKRect rectangle, int lineCount)
        {
            Random rand = new Random();
            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                paint.Color = SKColor.Parse("#55FFFFFF");
                for (int i=0; i<lineCount; i++)
                {
                    float dX1 = (float)(rectangle.Width * rand.NextDouble()) + rectangle.Left;
                    float dX2 = (float)(rectangle.Width * rand.NextDouble()) + rectangle.Left;

                    float dY1 = (float)(rectangle.Height * rand.NextDouble()) + rectangle.Top;
                    float dY2 = (float)(rectangle.Height * rand.NextDouble()) + rectangle.Top;

                    SKPoint pt1 = new SKPoint(dX1, dY1);
                    SKPoint pt2 = new SKPoint(dX2, dY2);

                    canvas.DrawLine(pt1, pt2, paint);
                }
            }
        }
    }
}
