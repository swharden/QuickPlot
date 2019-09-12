using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuickPlot
{
    public class Figure
    {
        private List<Plot> subplots = new List<Plot>();
        public Plot plot;

        public FigureSettings.Padding padding = new FigureSettings.Padding();

        public Figure()
        {
            Subplot(1, 1, 1);
        }

        public void Subplot(int nRows, int nCols, int subPlotNumber, int rowSpan = 1, int colSpan = 1)
        {
            while (subplots.Count < subPlotNumber)
                subplots.Add(new Plot());

            // activate this plot
            plot = subplots[subPlotNumber - 1];

            // update its position
            plot.subplotPosition = new PlotSettings.SubplotPosition(nRows, nCols, subPlotNumber, rowSpan, colSpan);
        }

        public void Render(SKSurface surface)
        {
            surface.Canvas.Clear(SKColors.Blue);
            foreach (Plot plt in subplots)
            {
                SKRect rect = FigureSettings.Layout.GetRectangle(surface, plt.subplotPosition, padding);
                plt.Render(surface, rect);
            }
        }

        public void Save(string filePath, int width = 640, int height = 480, int quality = 80)
        {
            filePath = System.IO.Path.GetFullPath(filePath);

            var info = new SKImageInfo(width, height);
            using (SKSurface surface = SKSurface.Create(info))
            {
                Render(surface);
                using (System.IO.Stream fileStream = System.IO.File.OpenWrite(filePath))
                {
                    // TODO: support BMP, PNG, TIF, GIF, and JPG
                    SKImage snap = surface.Snapshot();
                    SKData encoded = snap.Encode(SKEncodedImageFormat.Jpeg, quality);
                    encoded.SaveTo(fileStream);
                    Debug.WriteLine($"Wrote: {filePath}");
                }
            }
        }
    }
}
