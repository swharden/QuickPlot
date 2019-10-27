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
        public double renderTimeMsec { get; private set; }

        /// <summary>
        /// A figure contains one or more plots (subplots)
        /// </summary>
        public Figure()
        {
            Reset();
        }

        #region subplot management

        public readonly List<Plot> subplots = new List<Plot>();
        public Plot plot;

        /// <summary>
        /// Create and/or activate a Plot in a multi-plot Figure
        /// </summary>
        public Plot Subplot(int nRows, int nCols, int subPlotNumber, int rowSpan = 1, int colSpan = 1)
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
            var newPlot = new Plot();
            newPlot.subplotPosition.Update(nRows, nCols, subPlotNumber, rowSpan, colSpan);
            subplots.Add(newPlot);
            plot = subplots.Last();
            return newPlot;
        }

        /// <summary>
        /// Clear all subplots (and data) and start over with a single full-size plot
        /// </summary>
        public void Reset()
        {
            subplots.Clear();
            Subplot(1, 1, 1);
        }

        /// <summary>
        /// Return the Plot at a given location of the Figure
        /// </summary>
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

        /// <summary>
        /// Draw the figure onto a SKCanvas
        /// </summary>
        public void Render(SKCanvas canvas, SKSize figureSize)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Every plot must be completely redrawn on every render because plots dont strictly stay inside their rectangles.
            canvas.Clear(style.bgColor);
            foreach (Plot plot in subplots)
                plot.Render(canvas, GetSubplotRect(figureSize, plot));

            stopwatch.Stop();
            renderTimeMsec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
        }

        /// <summary>
        /// Return the rectangle occupied by a subplot
        /// </summary>
        private SKRect GetSubplotRect(SKSize figureSize, Plot subplot)
        {
            // determine padding around the subplot (plot-plot padding is different than plot-edge padding)
            float padLeft, padRight, padBottom, padTop;
            padLeft = (subplot.subplotPosition.leftFrac == 0) ? style.edges : style.horizontal;
            padRight = (subplot.subplotPosition.rightFrac == 1) ? style.edges : style.horizontal;
            padBottom = (subplot.subplotPosition.botFrac == 1) ? style.edges : style.vertical;
            padTop = (subplot.subplotPosition.topFrac == 0) ? style.edges : style.vertical;

            SKRect renderArea = subplot.subplotPosition.GetRectangle(figureSize);
            renderArea = renderArea.ShrinkBy(padLeft, padRight, padBottom, padTop);
            return renderArea;
        }

        #endregion

        #region misc

        /// <summary>
        /// Render the Figure and save it as an image
        /// </summary>
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
