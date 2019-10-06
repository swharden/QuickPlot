using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

/* MOUSE TRACKING NOTES
 * 
 * Mouse tracking is difficult because it spans so many levels (user control, figure, plot, and axes).
 * Mouse tracking is handled at the plot level because only a single plot is interacted with at a time with the mouse.
 * Mouse actions (pan and zoom) can only be applied at render, because that's the only time a plot knows its size in pixels.
 * 
 */

namespace QuickPlot.PlotSettings
{
    public class MouseTracker
    {
        public Point now = new Point(0, 0);

        public Point leftDown = new Point(0, 0);
        public Point rightDown = new Point(0, 0);
        public Point middleDown = new Point(0, 0);

        public Point leftDelta { get { return new Point(now.X - leftDown.X, now.Y - leftDown.Y); } }
        public Point rightDelta { get { return new Point(now.X - rightDown.X, now.Y - rightDown.Y); } }
        public Point middleDelta { get { return new Point(now.X - middleDown.X, now.Y - middleDown.Y); } }

        public bool leftButtonIsDown { get { return (leftDown.X != 0 || leftDown.Y != 0); } }
        public bool rightButtonIsDown { get { return (rightDown.X != 0 || rightDown.Y != 0); } }
        public bool centerButtonIsDown { get { return (middleDown.X != 0 || middleDown.Y != 0); } }

        public RectangleF lastRenderArea;

        public MouseTracker()
        {

        }
    }
}
