using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QuickPlot
{
    public class Figure
    {
        Random rand = new Random();

        public FigureSettings.Padding padding = new FigureSettings.Padding();

        public Figure()
        {
            Clear();
        }

        #region subplot management

        private readonly List<Plot> subplots = new List<Plot>();
        public Plot plot;

        public void Subplot(int nRows, int nCols, int subPlotNumber, int rowSpan = 1, int colSpan = 1)
        {
            // activate the plot with this configuration
            foreach (Plot subplot in subplots)
            {
                // TODO: activate the subplot and return
            }

            // clear the original (default) plot if it exists
            if (subplots.Count == 1)
                if (subplots.First().subplotPosition.isFullSize)
                    subplots.Clear();

            // no plot with this configuration exists, so create it, add it to the list, and activate it
            var newPlot = new Plot
            {
                subplotPosition = new PlotSettings.SubplotPosition(nRows, nCols, subPlotNumber, rowSpan, colSpan)
            };
            subplots.Add(newPlot);
            plot = subplots.Last();
        }

        public void Clear()
        {
            subplots.Clear();
            Subplot(1, 1, 1);
        }

        #endregion

        #region rendering

        private Stopwatch stopwatchRender = Stopwatch.StartNew();
        public void Render(SKCanvas canvas, SKSize figureSize)
        {
            stopwatchRender.Restart();

            Console.WriteLine();
            canvas.Clear(SKColor.Parse("#DDDDDD"));
            foreach (Plot subplot in subplots)
                subplot.Render(canvas, SubplotRect(figureSize, subplot));

            stopwatchRender.Stop();
        }

        private SKRect SubplotRect(SKSize figureSize, Plot subplot)
        {
            SKRect renderArea = subplot.subplotPosition.GetRectangle(figureSize);

            float padLeft, padRight, padBottom, padTop;
            padLeft = (subplot.subplotPosition.leftFrac == 0) ? padding.edges : padding.horizontal;
            padRight = (subplot.subplotPosition.rightFrac == 1) ? padding.edges : padding.horizontal;
            padBottom = (subplot.subplotPosition.botFrac == 1) ? padding.edges : padding.vertical;
            padTop = (subplot.subplotPosition.topFrac == 0) ? padding.edges : padding.vertical;
            renderArea = Tools.RectShrinkBy(renderArea, padLeft, padRight, padBottom, padTop);

            return renderArea;
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

        #endregion

        #region misc

        public void Save(int width, int height, string fileName, int quality = 100)
        {
            string filePath = System.IO.Path.GetFullPath(fileName);

            // create a canvas 
            SKImageInfo imageInfo = new SKImageInfo(width, height);
            SKSurface surface = SKSurface.Create(imageInfo);

            // render onto the canvas
            Render(surface.Canvas, new SKSize(width, height));

            // save a snapshot
            using (System.IO.Stream fileStream = System.IO.File.OpenWrite(filePath))
            {
                SKImage snap = surface.Snapshot();
                SKData encoded = snap.Encode(SKEncodedImageFormat.Png, quality: quality);
                encoded.SaveTo(fileStream);
                Debug.WriteLine($"saved {filePath}");
            }
        }

        #endregion

        #region mouse
        public Plot PlotUnderMouse(SKSize figureSize, SKPoint currentLocation)
        {
            foreach (Plot subplot in subplots)
            {
                SKRect rect = SubplotRect(figureSize, subplot);
                if (rect.Contains(currentLocation))
                    return subplot;
            }

            return null;
        }
        #endregion
    }
}
