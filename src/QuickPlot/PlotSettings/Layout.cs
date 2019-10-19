using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class Layout
    {
        // user can reach in and configure these options
        public float yLabelWidth = 20;
        public float yScaleWidth = 40;
        public float y2LabelWidth = 20; // zero is too close to the edge
        public float y2ScaleWidth = 40;
        public float titleHeight = 20;
        public float xLabelHeight = 20;
        public float xScaleHeight = 20;

        // these are calculated internally
        public SKRect plotRect { get; private set; }
        public SKRect dataRect { get; private set; }

        public SKRect yLabelRect { get; private set; }
        public SKRect yScaleRect { get; private set; }
        public SKRect y2LabelRect { get; private set; }
        public SKRect y2ScaleRect { get; private set; }
        public SKRect titleRect { get; private set; }
        public SKRect xLabelRect { get; private set; }
        public SKRect xScaleRect { get; private set; }

        public Layout()
        {
        }

        public void Update(SKRect plotRect)
        {
            this.plotRect = plotRect;

            titleRect = new SKRect(plotRect.Left, plotRect.Top, plotRect.Right, plotRect.Top + titleHeight);

            yLabelRect = new SKRect(plotRect.Left, plotRect.Top, plotRect.Left + yLabelWidth, plotRect.Bottom);
            yScaleRect = new SKRect(yLabelRect.Right, plotRect.Top, yLabelRect.Right + yScaleWidth, plotRect.Bottom);

            y2LabelRect = new SKRect(plotRect.Right - y2LabelWidth, plotRect.Top, plotRect.Right, plotRect.Bottom);
            y2ScaleRect = new SKRect(y2LabelRect.Left - y2ScaleWidth, plotRect.Top, y2LabelRect.Left, plotRect.Bottom);

            xLabelRect = new SKRect(plotRect.Left, plotRect.Bottom - xLabelHeight, plotRect.Right, plotRect.Bottom);
            xScaleRect = new SKRect(plotRect.Left, xLabelRect.Top - xScaleHeight, plotRect.Right, xLabelRect.Top);

            // shrink dataRect to its final size
            dataRect = plotRect;
            dataRect = dataRect.ShrinkBy(
                    left: yLabelRect.Width + yScaleRect.Width,
                    right: y2LabelRect.Width + y2ScaleRect.Width,
                    bottom: xLabelRect.Height + xScaleRect.Height,
                    top: titleRect.Height
                );

            // shrink labels and scales to match dataRect
            yLabelRect = yLabelRect.MatchVert(dataRect);
            yScaleRect = yScaleRect.MatchVert(dataRect);
            y2LabelRect = y2LabelRect.MatchVert(dataRect);
            y2ScaleRect = y2ScaleRect.MatchVert(dataRect);
            xLabelRect = xLabelRect.MatchHoriz(dataRect);
            xScaleRect = xScaleRect.MatchHoriz(dataRect);
        }
    }
}
