using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace QuickPlot
{
    public class Figure
    {
        // keep the subplot list private. Activate plots individually by calling SubPlot()
        private readonly List<Plot> subplots = new List<Plot>();

        // the active plot
        public Plot plot;

        // configuration objects are okay to be public
        public FigureSettings.Padding padding = new FigureSettings.Padding();
        public FigureSettings.Colors colors = new FigureSettings.Colors();

        /// <summary>
        /// Create a Figure (which contains a Plot)
        /// </summary>
        public Figure()
        {
            Clear();
        }

        public void Subplot(int nRows, int nCols, int subPlotNumber, int rowSpan = 1, int colSpan = 1)
        {
            // activate the plot with this configuration
            foreach (Plot subplot in subplots)
            {
                // TODO: activate the subplot and return
            }

            // clear the original (default) plot if it exists
            if (subplots.Count() == 1)
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

        private void Render(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentException("bmp must not be null");

            using (Graphics gfx = Graphics.FromImage(bmp))
                gfx.Clear(colors.background);

            foreach (Plot subplot in subplots)
            {
                // determine render area based on figure-level padding and subplot spacing settings
                RectangleF renderArea = subplot.subplotPosition.GetRectangle(bmp.Width, bmp.Height);
                float padLeft, padRight, padBottom, padTop;
                padLeft = (subplot.subplotPosition.leftFrac == 0) ? padding.edges : padding.horizontal;
                padRight = (subplot.subplotPosition.rightFrac == 1) ? padding.edges : padding.horizontal;
                padBottom = (subplot.subplotPosition.botFrac == 1) ? padding.edges : padding.vertical;
                padTop = (subplot.subplotPosition.topFrac == 0) ? padding.edges : padding.vertical;
                renderArea = Tools.RectangleShrinkBy(renderArea, padLeft, padRight, padBottom, padTop);

                // pass the full bitmap into the subplot and it will render inside the renderArea
                subplot.Render(bmp, renderArea);
            }
        }

        /// <summary>
        /// Render the figure onto a new Bitmap of a given size
        /// </summary>
        public Bitmap GetBitmap(int width, int height)
        {
            if (width < 1 || height < 1)
                throw new ArgumentException("width and height must be >1");

            Bitmap bmp = new Bitmap(width, height);
            Render(bmp);
            return bmp;
        }

        /// <summary>
        /// Render the figure onto an existing Bitmap (slightly faster)
        /// </summary>
        public Bitmap GetBitmap(Bitmap existingBitmap)
        {
            if (existingBitmap == null)
                throw new ArgumentException("existingBitmap must not be null");

            Render(existingBitmap);
            return existingBitmap;
        }

        /// <summary>
        /// Save the figure as an image file
        /// </summary>
        public void Save(string filePath, int width, int height)
        {
            Bitmap bmp = GetBitmap(width, height);
            bmp.Save(filePath);
            Debug.WriteLine($"Saved {System.IO.Path.GetFullPath(filePath)}");
        }
    }
}
