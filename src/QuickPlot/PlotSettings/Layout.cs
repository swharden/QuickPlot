using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{

    /* This class helps lay-out components of a plot and determine how big the data area is.
     * By default this is done using fixed sizes. However, padding can be customized after
     * measuring rendered fonts to "tighten" the axis padding to custom fonts and sizes.
     */
    public class Layout
    {
        public SKRect frame, scales, data;

        public SKRect labelLeft, labelRight, labelBottom, labelTop;
        public SKRect scaleLeft, scaleRight, scaleBottom, scaleTop;

        // TODO: consider a "padding" object (containing 4 floats) rather than so many individual numbers.
        public Padding labelPadding = new Padding() { left = 20, right = 20, bottom = 20, top = 20 };
        public Padding scalePadding = new Padding() { left = 20, right = 20, bottom = 20, top = 20 };

        public struct Padding
        {
            public float left, right, bottom, top;
        }

        public Layout()
        {

        }

        public Layout(SKRect frame)
        {
            Tighten(frame);
        }

        public void Tighten(SKRect frame)
        {
            // Determine how large the data area is. This requires knowing how large all the axis labels and scales are.

            this.frame = frame;
            SKRect remaining = frame;

            // axis labels
            labelLeft = ShrinkTo(remaining, left: labelPadding.left);
            labelRight = ShrinkTo(remaining, right: labelPadding.right);
            labelBottom = ShrinkTo(remaining, bottom: labelPadding.bottom);
            labelTop = ShrinkTo(remaining, top: labelPadding.top);
            scales = ShrinkBy(remaining, labelPadding);

            // scales
            scaleLeft = ShrinkTo(scales, left: scalePadding.left);
            scaleRight = ShrinkTo(scales, right: scalePadding.right);
            scaleBottom = ShrinkTo(scales, bottom: scalePadding.bottom);
            scaleTop = ShrinkTo(scales, top: scalePadding.top);
            data = ShrinkBy(scales, scalePadding);

            // adjust labels to match data area
            labelLeft.Top = data.Top;
            labelLeft.Bottom = data.Bottom;
            labelRight.Top = data.Top;
            labelRight.Bottom = data.Bottom;
            labelBottom.Left = data.Left;
            labelBottom.Right = data.Right;
            labelTop.Left = data.Left;
            labelTop.Right = data.Right;

            // adjust scales to match the data area
            scaleLeft.Top = data.Top;
            scaleLeft.Bottom = data.Bottom;
            scaleRight.Top = data.Top;
            scaleRight.Bottom = data.Bottom;
            scaleBottom.Left = data.Left;
            scaleBottom.Right = data.Right;
            scaleTop.Left = data.Left;
            scaleTop.Right = data.Right;
        }

        private SKRect ShrinkTo(SKRect rect, float? left = null, float? right = null, float? bottom = null, float? top = null)
        {
            if (left != null)
                rect.Right = rect.Left + (float)left;

            if (right != null)
                rect.Left = rect.Right - (float)right;

            if (bottom != null)
                rect.Top = rect.Bottom - (float)bottom;

            if (top != null)
                rect.Bottom = rect.Top + (float)top;

            return rect;
        }

        private SKRect ShrinkBy(SKRect rect, Padding padding)
        {
            rect.Left += padding.left;
            rect.Right -= padding.right;
            rect.Bottom -= padding.bottom;
            rect.Top += padding.top;
            return rect;
        }

        public void RenderDebuggingGuides(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint();

            paint.Color = new SKColor(0, 0, 0, 255);
            paint.IsStroke = true;
            canvas.DrawRect(frame, paint);
            paint.IsStroke = false;

            paint.Color = new SKColor(0, 0, 255, 40);
            canvas.DrawRect(labelLeft, paint);
            canvas.DrawRect(labelRight, paint);
            canvas.DrawRect(labelBottom, paint);
            canvas.DrawRect(labelTop, paint);

            paint.Color = new SKColor(0, 255, 0, 40);
            canvas.DrawRect(scaleLeft, paint);
            canvas.DrawRect(scaleRight, paint);
            canvas.DrawRect(scaleBottom, paint);
            canvas.DrawRect(scaleTop, paint);

            paint.Color = new SKColor(0, 0, 0, 255);
            paint.IsStroke = true;
            canvas.DrawRect(data, paint);
        }
    }
}
