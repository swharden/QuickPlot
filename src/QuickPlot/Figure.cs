using SkiaSharp;
using System;
using System.Diagnostics;

namespace QuickPlot
{
    public class Figure
    {
        Random rand = new Random();
        Stopwatch stopwatchRender = Stopwatch.StartNew();

        public Figure()
        {

        }

        public void Render(SKCanvas canvas, int width, int height)
        {
            stopwatchRender.Restart();

            var paint = new SKPaint
            {
                Color = new SKColor(255, 255, 255, 100),
                IsAntialias = true
            };

            int lineCount = 1000;
            canvas.Clear(SKColor.Parse("#003366"));
            for (int i = 0; i < lineCount; i++)
            {
                SKPoint pt1 = Tools.randomPoint(width, height, rand);
                SKPoint pt2 = Tools.randomPoint(width, height, rand);
                canvas.DrawLine(pt1, pt2, paint);
            }

            stopwatchRender.Stop();
        }

        public string RenderBenchmarkMessage
        {
            get
            {
                double elapsedSec = (double)stopwatchRender.ElapsedTicks / Stopwatch.Frequency;
                string message = string.Format("rendered in {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
                return message;
            }
        }

        public void Save(int width, int height, string fileName, int quality = 100)
        {
            string filePath = System.IO.Path.GetFullPath(fileName);

            // create a canvas 
            SKImageInfo imageInfo = new SKImageInfo(width, height);
            SKSurface surface = SKSurface.Create(imageInfo);

            // render onto the canvas
            Render(surface.Canvas, width, height);

            // save a snapshot
            using (System.IO.Stream fileStream = System.IO.File.OpenWrite(filePath))
            {
                SKImage snap = surface.Snapshot();
                SKData encoded = snap.Encode(SKEncodedImageFormat.Png, quality: quality);
                encoded.SaveTo(fileStream);
                Debug.WriteLine($"saved {filePath}");
            }
        }
    }
}
