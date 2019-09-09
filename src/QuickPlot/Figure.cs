using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace QuickPlot
{
    public class Figure
    {
        public readonly List<Subplot.Plot> subplots = new List<Subplot.Plot>();
        public Subplot.Plot plot;

        public Figure()
        {
            Subplot(1, 1, 1);
        }

        public void Subplot(int nRows, int nCols, int subPlotNumber, int rowSpan = 1, int colSpan = 1)
        {
            while (subplots.Count < subPlotNumber)
                subplots.Add(new Subplot.Plot());

            // activate this plot
            plot = subplots[subPlotNumber - 1];

            // update its position
            plot.subplotPos = new Subplot.SubplotPosition(nRows, nCols, subPlotNumber, rowSpan, colSpan);
        }

        public void Save(string filePath = "QuickPlot.png", int quality = 90)
        {
            SKImageInfo imageInfo = new SKImageInfo(600, 400, SKColorType.Rgba8888, SKAlphaType.Premul);
            using (SKSurface surface = SKSurface.Create(imageInfo))
            {
                SKCanvas canvas = surface.Canvas;
                Render(canvas);

                filePath = System.IO.Path.GetFullPath(filePath);
                using (System.IO.Stream fileStream = System.IO.File.OpenWrite(filePath))
                {
                    SKImage snap = surface.Snapshot();
                    SKData encoded = snap.Encode(SKEncodedImageFormat.Jpeg, quality);
                    encoded.SaveTo(fileStream);
                    Console.WriteLine($"Saved: {filePath}");
                }
            }
        }

        public void Render(SKCanvas canvas)
        {
            canvas.Clear(new SKColor(0, 0, 0));

            var subplotRectangles = QuickPlot.Subplot.LayoutManager.SubPlotRectangles(subplots, canvas);

            for (int i=0; i<subplots.Count; i++)
            {
                canvas.DrawRect(subplotRectangles[i], Tools.IndexedPaint(i));
                using (var paint = new SKPaint())
                {
                    paint.TextSize = 16;
                    paint.IsAntialias = true;
                    paint.Color = new SKColor(0, 0, 0);
                    paint.TextAlign = SKTextAlign.Left;
                    paint.FakeBoldText = true;
                    Console.WriteLine(subplotRectangles[i]);
                    canvas.DrawText($" subplot {i + 1}", subplotRectangles[i].Left, subplotRectangles[i].Top + 16, paint);
                }
            }
        }
    }
}
