using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    class AxisScales
    {
        public AxisScale left = new AxisScale();
        public AxisScale right = new AxisScale();
        public AxisScale bottom = new AxisScale();
        public AxisScale top = new AxisScale();

        public void Render(Layout layout, Axes axes, SKCanvas canvas)
        {
            DrawTicks(layout, axes, canvas);
            DrawFrame(layout, canvas);
        }

        private void DrawTicks(Layout layout, Axes axes, SKCanvas canvas)
        {
            // TODO: this tick drawing code is just a placeholder.

            float tickLength = 3;

            left.GenerateTicks(axes.y.low, axes.y.high);
            foreach (double tickPosition in left.tickPositions)
            {
                float yPos = axes.GetPixel(0, tickPosition).Y;
                SKPoint tickRight = new SKPoint(layout.scaleLeft.Right, yPos);
                SKPoint tickLeft = new SKPoint(layout.scaleLeft.Right - tickLength, yPos);
                canvas.DrawLine(tickLeft, tickRight, left.paint);
            }

            right.GenerateTicks(axes.y.low, axes.y.high);
            foreach (double tickPosition in right.tickPositions)
            {
                float yPos = axes.GetPixel(0, tickPosition).Y;
                SKPoint tickRight = new SKPoint(layout.scaleRight.Left + tickLength, yPos);
                SKPoint tickLeft = new SKPoint(layout.scaleRight.Left, yPos);
                canvas.DrawLine(tickLeft, tickRight, right.paint);
            }

            bottom.GenerateTicks(axes.x.low, axes.x.high);
            foreach (double tickPosition in bottom.tickPositions)
            {
                float xPos = axes.GetPixel(tickPosition, 0).X;
                SKPoint tickTop = new SKPoint(xPos, layout.scaleBottom.Top);
                SKPoint tickBottom = new SKPoint(xPos, layout.scaleBottom.Top + tickLength);
                canvas.DrawLine(tickTop, tickBottom, bottom.paint);
            }

            top.GenerateTicks(axes.x.low, axes.x.high);
            foreach (double tickPosition in top.tickPositions)
            {
                float xPos = axes.GetPixel(tickPosition, 0).X;
                SKPoint tickTop = new SKPoint(xPos, layout.scaleTop.Bottom - tickLength);
                SKPoint tickBottom = new SKPoint(xPos, layout.scaleTop.Bottom);
                canvas.DrawLine(tickTop, tickBottom, top.paint);
            }

        }

        private void DrawFrame(Layout layout, SKCanvas canvas)
        {
            if (left.showFrame)
                canvas.DrawLine(layout.scaleLeft.Right, layout.scaleLeft.Bottom, layout.scaleLeft.Right, layout.scaleLeft.Top, left.paint);

            if (right.showFrame)
                canvas.DrawLine(layout.scaleRight.Left, layout.scaleRight.Bottom, layout.scaleRight.Left, layout.scaleRight.Top, right.paint);

            if (bottom.showFrame)
                canvas.DrawLine(layout.scaleBottom.Left, layout.scaleBottom.Top, layout.scaleBottom.Right, layout.scaleBottom.Top, bottom.paint);

            if (top.showFrame)
                canvas.DrawLine(layout.scaleTop.Left, layout.scaleTop.Bottom, layout.scaleTop.Right, layout.scaleTop.Bottom, top.paint);
        }
    }
}
