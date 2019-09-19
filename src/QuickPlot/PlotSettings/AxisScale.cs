using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    class AxisScale
    {
        public bool showFrame = true;
        public SKColor color = SKColors.Black;
        public SKPaint paint
        {
            get
            {
                SKPaint paint = new SKPaint
                {
                    Color = color
                };
                return paint;
            }
        }

        public double[] tickPositions;
        public string[] tickLabels;

        public void GenerateTicks(double low, double high)
        {
            // TODO: this tick drawing code is just a placeholder.

            double span = high - low;
            double spacing = span / 10;

            List<double> ticks = new List<double>();
            List<string> labels = new List<string>();
            for (double tickPosition = low; tickPosition <= high; tickPosition += spacing)
            {
                ticks.Add(tickPosition);
                labels.Add(tickPosition.ToString());
            }
            tickPositions = ticks.ToArray();
            tickLabels = labels.ToArray();
        }
    }
}
