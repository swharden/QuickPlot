using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class MouseTracker
    {
        public SKPoint leftDown, leftNow;
        public SKPoint leftDownDelta { get { return new SKPoint(leftNow.X - leftDown.X, leftNow.Y - leftDown.Y); } }

        public MouseTracker()
        {
            LeftUp();
        }

        public void LeftDown(SKPoint downLocation)
        {
            leftDown = downLocation;
            leftNow = downLocation;
        }

        public void LeftLocation(SKPoint currentLocation)
        {
            leftNow = currentLocation;
        }

        public void LeftUp()
        {
            leftDown = new SKPoint(-1, -1);
            leftNow = new SKPoint(-1, -1);
        }
    }
}
