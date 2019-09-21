using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuickPlot
{
    public class Figure
    {
        public List<Plot> subplots = new List<Plot>();
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
            plot = subplots[subPlotNumber - 1];
            plot.subplotPosition = new PlotSettings.SubplotPosition(nRows, nCols, subPlotNumber, rowSpan, colSpan);
        }

        public void Render(SKCanvas canvas)
        {
            canvas.Clear(SKColors.White);
            foreach (Plot plt in subplots)
            {
                SKRect subplotRect = FigureSettings.Layout.GetRectangle(canvas, plt.subplotPosition, padding);
                plt.Render(canvas, subplotRect);
            }
        }

        public void Save(string filePath, int width = 640, int height = 480, int quality = 95)
        {
            filePath = System.IO.Path.GetFullPath(filePath);

            var info = new SKImageInfo(width, height);
            using (SKSurface surface = SKSurface.Create(info))
            {
                Render(surface.Canvas);
                using (System.IO.Stream fileStream = System.IO.File.OpenWrite(filePath))
                {
                    // TODO: support BMP, PNG, TIF, GIF, and JPG
                    SKImage snap = surface.Snapshot();
                    SKData encoded = snap.Encode(SKEncodedImageFormat.Png, quality);
                    encoded.SaveTo(fileStream);
                    Debug.WriteLine($"Wrote: {filePath}");
                }
            }
        }

        public Plot GetPlotUnderCursor(SKCanvas canvas, SKPoint mouseLocation)
        {
            if (canvas != null)
            {
                foreach (Plot plt in subplots)
                {
                    SKRect subplotRect = FigureSettings.Layout.GetRectangle(canvas, plt.subplotPosition, padding);
                    if (subplotRect.Contains(mouseLocation))
                        return plt;
                }
            }
            return null;
        }

        /// <summary>
        /// Various configuration settings mostly for debugging and developer use
        /// </summary>
        public void Configure(bool? displayLayout = null)
        {
            foreach (Plot plt in subplots)
            {
                if (displayLayout != null)
                    plt.layout.display = (bool)displayLayout;
            }
        }
    }
}
