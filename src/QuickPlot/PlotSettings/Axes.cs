using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickPlot.PlotSettings
{

    public class Axes
    {
        public Axis x, y;
        private RectangleF rect;
        private double pixelsPerUnitX, pixelsPerUnitY;
        private double unitsPerPixelX, unitsPerPixelY;

        public Axes()
        {
            x = new Axis();
            y = new Axis();
            SetRect(new RectangleF(0, 0, 0, 0));
        }

        public Axes(Axes sourceAxes)
        {
            x = new Axis();
            y = new Axis();
            Set(sourceAxes.x.low, sourceAxes.x.high, sourceAxes.y.low, sourceAxes.y.high);
            SetRect(sourceAxes.rect);
        }

        public override string ToString()
        {
            return $"Axes: xLow={x.low}, xHigh={x.high}, yLow={y.low}, yHigh={y.high}";
        }

        public void Set(double? xLow, double? xHigh, double? yLow, double? yHigh)
        {
            x.low = xLow ?? x.low;
            x.high = xHigh ?? x.high;
            y.low = yLow ?? y.low;
            y.high = yHigh ?? y.high;
        }

        public void Set(AxisLimits limits)
        {
            x.low = limits.xLow;
            x.high = limits.xHigh;
            y.low = limits.yLow;
            y.high = limits.yHigh;
        }

        public void Expand(AxisLimits limits)
        {
            x.low = Math.Min(x.low, limits.xLow);
            x.high = Math.Max(x.high, limits.xHigh);
            y.low = Math.Min(y.low, limits.yLow);
            y.high = Math.Max(y.high, limits.yHigh);
        }

        public void Pan(double dX = 0, double dY = 0)
        {
            x.Pan(dX);
            y.Pan(dY);
        }

        public void PanPixels(PointF delta)
        {
            if (delta.X == 0 && delta.Y == 0)
                return;

            double dX = -delta.X * unitsPerPixelX;
            double dY = delta.Y * unitsPerPixelY;
            Pan(dX, dY);
        }

        public void Zoom(double fracX = 1, double fracY = 1)
        {
            x.Zoom(fracX);
            y.Zoom(fracY);
        }

        public void ZoomPixels(PointF delta)
        {
            if (delta.X == 0 && delta.Y == 0)
                return;

            double dX = delta.X / pixelsPerUnitX;
            double dY = -delta.Y / pixelsPerUnitY;
            double dXFrac = dX / (Math.Abs(dX) + x.span);
            double dYFrac = dY / (Math.Abs(dY) + y.span);
            Zoom(Math.Pow(10, dXFrac), Math.Pow(10, dYFrac));
        }

        public void SetRect(RectangleF rect)
        {
            this.rect = rect;

            if ((this.rect.Width == 0) || (this.rect.Height == 0))
                return;

            pixelsPerUnitX = rect.Width / x.span;
            pixelsPerUnitY = rect.Height / y.span;
            unitsPerPixelX = x.span / rect.Width;
            unitsPerPixelY = y.span / rect.Height;
        }

        public RectangleF GetRect()
        {
            return rect;
        }

        public PointF GetPixel(double unitX, double unitY)
        {
            double pixelX = (unitX - x.low) * pixelsPerUnitX + rect.Left;
            double pixelY = rect.Bottom - (unitY - y.low) * pixelsPerUnitY;
            return new PointF((float)pixelX, (float)pixelY);
        }
    }
}
