using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class Layout
    {
        // user can reach in and configure these options
        float yLabelWidth = 20;
        float yScaleWidth = 40;
        float y2LabelWidth = 0;
        float y2ScaleWidth = 0;
        float titleHeight = 20;
        float xLabelHeight = 20;
        float xScaleHeight = 20;

        // these are calculated internally
        public RectangleF plotRect { get; private set; }
        public RectangleF dataRect { get; private set; }

        public RectangleF yLabelRect { get; private set; }
        public RectangleF yScaleRect { get; private set; }
        public RectangleF y2LabelRect { get; private set; }
        public RectangleF y2ScaleRect { get; private set; }
        public RectangleF titleRect { get; private set; }
        public RectangleF xLabelRect { get; private set; }
        public RectangleF xScaleRect { get; private set; }

        public Layout()
        {
        }

        /// <summary>
        /// recalculate all layout rectangles based on a new plot rectangle
        /// </summary>
        public void Update(RectangleF plotRect)
        {
            titleRect = new RectangleF(plotRect.X, plotRect.Y, plotRect.Width, titleHeight);

            yLabelRect = new RectangleF(plotRect.X, plotRect.Y, yLabelWidth, plotRect.Height);
            yScaleRect = new RectangleF(plotRect.X + yLabelRect.Width, plotRect.Y, yScaleWidth, plotRect.Height);

            y2LabelRect = new RectangleF(plotRect.Right - y2LabelWidth, plotRect.Y, y2LabelWidth, plotRect.Height);
            y2ScaleRect = new RectangleF(y2LabelRect.Left - y2ScaleWidth, plotRect.Y, y2ScaleWidth, plotRect.Height);

            xLabelRect = new RectangleF(plotRect.X, plotRect.Bottom - xLabelHeight, plotRect.Width, xLabelHeight);
            xScaleRect = new RectangleF(plotRect.X, xLabelRect.Top - xLabelHeight, plotRect.Width, xScaleHeight);

            // shrink dataRect to its final size
            dataRect = plotRect;
            dataRect = Tools.RectangleShrinkBy(dataRect, 
                    left: yLabelRect.Width + yScaleRect.Width,
                    right: y2LabelRect.Width + y2ScaleRect.Width,
                    bottom: xLabelRect.Height + xScaleRect.Height,
                    top: titleRect.Height
                );

            // shrink labels and scales to match dataRect
            yLabelRect = Tools.RectangleShrinkMatchVertical(yLabelRect, dataRect);
            yScaleRect = Tools.RectangleShrinkMatchVertical(yScaleRect, dataRect);
            y2LabelRect = Tools.RectangleShrinkMatchVertical(y2LabelRect, dataRect);
            y2ScaleRect = Tools.RectangleShrinkMatchVertical(y2ScaleRect, dataRect);
            xLabelRect = Tools.RectangleShrinkMatchHorizontal(xLabelRect, dataRect);
            xScaleRect = Tools.RectangleShrinkMatchHorizontal(xScaleRect, dataRect);


        }
    }
}
