using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    public static class Render
    {
        public static void RandomLines(SKSurface surface, int lineCount = 1_000)
        {
            Random rand = new Random();

            var canvas = surface.Canvas;
            canvas.Clear(SKColor.Parse("#003366"));

            float width = canvas.LocalClipBounds.Width;
            float height = canvas.LocalClipBounds.Height;

            var paint = new SKPaint
            {
                Color = SKColor.Parse("#66FFFFFF"),
                StrokeWidth = 1,
                IsAntialias = true
            };

            for (int i = 0; i < lineCount; i++)
            {
                float x1 = (float)(rand.NextDouble() * width);
                float x2 = (float)(rand.NextDouble() * width);
                float y1 = (float)(rand.NextDouble() * height);
                float y2 = (float)(rand.NextDouble() * height);
                canvas.DrawLine(x1, y1, x2, y2, paint);
            }
        }

        public static void RenderAndSave(int width = 600, int height = 400, int lineCount = 1_000)
        {
            var imageInfo = new SKImageInfo(width, height);
            var surface = SKSurface.Create(imageInfo);
            RandomLines(surface, lineCount);

            string filePath = System.IO.Path.GetFullPath("demo.png");
            using (System.IO.Stream fileStream = System.IO.File.OpenWrite(filePath))
            {
                SKImage snap = surface.Snapshot();
                SKData encoded = snap.Encode(SKEncodedImageFormat.Png, 100);
                encoded.SaveTo(fileStream);
            }
            Console.WriteLine($"Saved: {filePath}");
        }
    }
}
