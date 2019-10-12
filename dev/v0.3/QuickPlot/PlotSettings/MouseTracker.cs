using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class MouseTracker
    {
        public SKPoint leftDown, leftNow;
        public SKPoint rightDown, rightNow;
        public SKPoint leftDownDelta { get { return new SKPoint(leftNow.X - leftDown.X, leftNow.Y - leftDown.Y); } }
        public SKPoint rightDownDelta { get { return new SKPoint(rightNow.X - rightDown.X, rightNow.Y - rightDown.Y); } }

        public MouseTracker()
        {
            LeftUp();
        }

        public void LeftDown(SKPoint downLocation)
        {
            leftDown = downLocation;
            leftNow = downLocation;
        }

        public void RightDown(SKPoint downLocation)
        {
            rightDown = downLocation;
            rightNow = downLocation;
        }

        public void LeftLocation(SKPoint currentLocation)
        {
            leftNow = currentLocation;
        }

        public void RightLocation(SKPoint currentLocation)
        {
            rightNow = currentLocation;
        }

        public void LeftUp()
        {
            leftDown = new SKPoint(-1, -1);
            leftNow = new SKPoint(-1, -1);
        }

        public void RightUp()
        {
            rightDown = new SKPoint(-1, -1);
            rightNow = new SKPoint(-1, -1);
        }
    }
}
