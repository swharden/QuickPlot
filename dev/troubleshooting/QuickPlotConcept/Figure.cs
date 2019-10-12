using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlotConcept
{
    public class Figure
    {
        Random rand = new Random();

        public void Render(SKCanvas canvas, int width, int height)
        {
            var paint = new SKPaint
            {
                Color = new SKColor(255, 255, 255, 100),
                IsAntialias = true
            };

            int lineCount = 1000;
            canvas.Clear(SKColor.Parse("#003366"));
            for (int i = 0; i < lineCount; i++)
            {
                canvas.DrawLine(randomPoint(width, height), randomPoint(width, height), paint);
            }
        }

        private SKPoint randomPoint(int width, int height)
        {
            float x = (float)(width * rand.NextDouble());
            float y = (float)(height * rand.NextDouble());
            return new SKPoint(x, y);
        }
    }
}
