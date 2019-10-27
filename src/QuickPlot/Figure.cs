using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace QuickPlot
{
    public class Figure
    {
        public FigureSettings.FigureStyle style = new FigureSettings.FigureStyle();

        public Figure()
        {
            Reset();
        }

        #region subplot management

        public readonly List<Plot> subplots = new List<Plot>();
        public Plot plot;

        public Plot Subplot(
            int nRows, int nCols, int subPlotNumber,
            int rowSpan = 1, int colSpan = 1
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

            subplots.Add(newPlot);
            plot = subplots.Last();
            return newPlot;
        }

        public void Reset()
        {
            subplots.Clear();
            Subplot(1, 1, 1);
        }

        public Plot GetSubplotAtPoint(SKSize figureSize, SKPoint point)
        {
            foreach (Plot subplot in subplots)
            {
                SKRect rect = GetSubplotRect(figureSize, subplot);
                if (rect.Contains(point))
                    return subplot;
            }

            return null;
        }

        #endregion

        #region rendering

        public void Render(SKCanvas canvas, SKSize figureSize, Plot onlySubplot = null)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (onlySubplot is null)
            {
                // render everything
                canvas.Clear(style.bgColor);
                foreach (Plot plot in subplots)
                    plot.Render(canvas, GetSubplotRect(figureSize, plot));
            }
            else
            {
                // only render the given subplot and plots with linked axes
                foreach (Plot plot in subplots)
                {
                    if (plot == onlySubplot || plot.axes.x == onlySubplot.axes.x || plot.axes.y == onlySubplot.axes.y)
                    {
                        // redraw only inside the rectangle of that plot
                        SKRect plotRect = GetSubplotRect(figureSize, plot);
                        canvas.Save();
                        canvas.ClipRect(plotRect);
                        canvas.Clear(style.bgColor);
                        plot.Render(canvas, plotRect);
                        canvas.Restore();
                    }
                }
            }

            stopwatch.Stop();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            string message = string.Format("rendered in {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
        }

        private SKRect GetSubplotRect(SKSize figureSize, Plot subplot)
        {
            SKRect renderArea = subplot.subplotPosition.GetRectangle(figureSize);

            float padLeft, padRight, padBottom, padTop;
            padLeft = (subplot.subplotPosition.leftFrac == 0) ? style.edges : style.horizontal;
            padRight = (subplot.subplotPosition.rightFrac == 1) ? style.edges : style.horizontal;
            padBottom = (subplot.subplotPosition.botFrac == 1) ? style.edges : style.vertical;
            padTop = (subplot.subplotPosition.topFrac == 0) ? style.edges : style.vertical;
            renderArea = renderArea.ShrinkBy(padLeft, padRight, padBottom, padTop);

            return renderArea;
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
