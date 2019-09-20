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
                    Color = color,
                    IsAntialias = true
                };
                return paint;
            }
        }

        public double[] tickPositions;
        public string[] tickLabels;

        public void GenerateTicks(double low, double high, double pixelSpan, double pixelsPerTick)
        {
            // Determine the ideal tick density and create ticks (positions and labels)

            List<double> ticks = new List<double>();
            List<string> labels = new List<string>();

            double span = high - low;
            double idealTickCount = pixelSpan / pixelsPerTick;
            double step = Math.Pow(10, (int)Math.Log(span));
            double[] divisions = { 2, 2, 2.5 };
            double idealStep = span / idealTickCount;
            for (int i = 0; i < 100; i++)
            {
                if (step > idealStep)
                    step /= divisions[i % divisions.Length];
                else
                    break;
            }

            double firstTick;
            if (low < 0)
                firstTick = low + Math.Abs(low % step);
            else
                firstTick = low - Math.Abs(low % step) + step;

            for (double i = firstTick; i < high; i += step)
            {
                ticks.Add(i);
                labels.Add(Math.Round(i, 5).ToString());
                if (ticks.Count > 100)
                    break;
            }

            tickPositions = ticks.ToArray();
            tickLabels = labels.ToArray();
        }
    }
}
