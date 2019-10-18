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
        public SKColor backgroundColor = SKColor.Parse("#FFFFFF");

        public Figure()
        {
            Clear();
        }

        #region subplot management

        private readonly List<Plot> subplots = new List<Plot>();
        public Plot plot;

        public Plot Subplot(
            int nRows, int nCols, int subPlotNumber, 
            int rowSpan = 1, int colSpan = 1,
            Plot sharex = null, Plot sharey = null
            )
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
                subplotPosition = new PlotSettings.SubplotPosition(nRows, nCols, subPlotNumber, rowSpan, colSpan),
            };

            newPlot.ShareAxis(sharex, sharey);

            subplots.Add(newPlot);
            plot = subplots.Last();
            return newPlot;
        }

        public void Clear()
        {
            subplots.Clear();
            Subplot(1, 1, 1);
        }

        public Plot PlotAtPoint(SKSize figureSize, SKPoint point)
        {
            foreach (Plot subplot in subplots)
            {
                SKRect rect = SubplotRect(figureSize, subplot);
                if (rect.Contains(point))
                    return subplot;
            }

            return null;
        }

        private List<Plot> SubplotsSharingAxes(Plot sourcePlot)
        {
            List<Plot> plotsSharingAxes = new List<Plot>();
            foreach (Plot subplot in subplots)
            {
                if ((subplot.axes.x == sourcePlot.axes.x) || (subplot.axes.y == sourcePlot.axes.y))
                    if (!plotsSharingAxes.Contains(subplot))
                        plotsSharingAxes.Add(subplot);
            }
            return plotsSharingAxes;
        }

        #endregion

        #region rendering

        private Stopwatch stopwatchRender = Stopwatch.StartNew();
        public void Render(SKCanvas canvas, SKSize figureSize, Plot onlySubplot = null)
        {
            stopwatchRender.Restart();

            if (onlySubplot is null)
                canvas.Clear(backgroundColor);

            List<Plot> plotsToRender = (onlySubplot is null) ? subplots : SubplotsSharingAxes(onlySubplot);
            foreach (Plot subplot in plotsToRender)
            {
                SKRect plotRect = SubplotRect(figureSize, subplot);
                canvas.Save();
                canvas.ClipRect(plotRect);
                canvas.Clear(backgroundColor);
                subplot.Render(canvas, plotRect);
                canvas.Restore();
            }

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
            renderArea = renderArea.ShrinkBy(padLeft, padRight, padBottom, padTop);

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
    }
}
