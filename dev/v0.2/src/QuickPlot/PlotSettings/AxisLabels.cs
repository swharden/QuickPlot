using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    class AxisLabels
    {
        public AxisLabel left = new AxisLabel();
        public AxisLabel right = new AxisLabel();
        public AxisLabel bottom = new AxisLabel();
        public AxisLabel top = new AxisLabel();

        public AxisLabels()
        {
            top.bold = true;
            left.paint.TextAlign = SKTextAlign.Right;
        }

        public void Render(Layout layout, SKCanvas canvas)
        {
            using (SKPath path = new SKPath())
            {
                path.MoveTo(layout.labelLeft.Right, layout.labelLeft.Bottom);
                path.LineTo(layout.labelLeft.Right, layout.labelLeft.Top);
                canvas.DrawTextOnPath(left.text, path, 0, 0, left.paint);
            }

            using (SKPath path = new SKPath())
            {
                path.MoveTo(layout.labelRight.Left, layout.labelRight.Top);
                path.LineTo(layout.labelRight.Left, layout.labelRight.Bottom);
                canvas.DrawTextOnPath(right.text, path, 0, 0, right.paint);
            }

            canvas.DrawText(bottom.text, layout.labelBottom.MidX, layout.labelBottom.Top + bottom.fontSize, bottom.paint);

            canvas.DrawText(top.text, layout.labelTop.MidX, layout.labelTop.Top + top.fontSize, top.paint);
        }
    }
}
