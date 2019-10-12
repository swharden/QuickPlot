using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot
{
    public static class Tools
    {
        public static SKPoint randomPoint(int width, int height, Random rand)
        {
            float x = (float)(width * rand.NextDouble());
            float y = (float)(height * rand.NextDouble());
            return new SKPoint(x, y);
        }
    }
}
