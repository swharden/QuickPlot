using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{

    public class MouseTracker
    {
        public SKPoint now = new SKPoint(0, 0);

        public SKPoint leftDown = new SKPoint(0, 0);
        public SKPoint rightDown = new SKPoint(0, 0);
        public SKPoint middleDown = new SKPoint(0, 0);

        public SKPoint leftDelta { get { return new SKPoint(now.X - leftDown.X, now.Y - leftDown.Y); } }
        public SKPoint rightDelta { get { return new SKPoint(now.X - rightDown.X, now.Y - rightDown.Y); } }
        public SKPoint middleDelta { get { return new SKPoint(now.X - middleDown.X, now.Y - middleDown.Y); } }

        public bool leftButtonIsDown { get { return (leftDown.X != 0 || leftDown.Y != 0); } }
        public bool rightButtonIsDown { get { return (rightDown.X != 0 || rightDown.Y != 0); } }
        public bool centerButtonIsDown { get { return (middleDown.X != 0 || middleDown.Y != 0); } }

        public SKRect lastRenderArea;

        public MouseTracker()
        {

        }
    }
}
