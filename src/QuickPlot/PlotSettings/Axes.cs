using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class Axes
    {
        public Axis x, y;
        private SKRect rect;
        private double pixelsPerUnitX, pixelsPerUnitY;
        private double unitsPerPixelX, unitsPerPixelY;

        public Axes()
        {
            x = new Axis();
            y = new Axis();
            SetRect(new SKRect(0, 0, 0, 0));
        }

        public void Set(double? xLow, double? xHigh, double? yLow, double? yHigh)
        {
            x.low = xLow ?? x.low;
            x.high = xHigh ?? x.high;
            y.low = yLow ?? y.low;
            y.high = yHigh ?? y.high;
        }

        public void SetRect(SKRect rect)
        {
            this.rect = rect;

            if ((this.rect.Width == 0) || (this.rect.Height == 0))
                return;

            pixelsPerUnitX = rect.Width / x.span;
            pixelsPerUnitY = rect.Height / y.span;
            unitsPerPixelX = x.span / rect.Width;
            unitsPerPixelY = y.span / rect.Height;
        }

        public SKPoint GetPixel(double x, double y)
        {
            double pixelX = (x - this.x.low) * pixelsPerUnitX + rect.Left;
            double pixelY = rect.Bottom - (y - this.y.low) * pixelsPerUnitY;
            return new SKPoint((float)pixelX, (float)pixelY);
        }
    }

}
