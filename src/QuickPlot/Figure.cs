﻿using System;
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

        /// <summary>
        /// Add a subplot to the figure sized according to an imaginary grid of plots
        /// </summary>
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

        /// <summary>
        /// Delete existing subplots and re-initialize the figure with a single plot
        /// </summary>
        public void Clear()
        {
            subplots.Clear();
            Subplot(1, 1, 1);
        }

        /// <summary>
        /// Draw all plots onto the given Bitmap
        /// </summary>
        private void Render(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentException("bmp must not be null");

            using (Graphics gfx = Graphics.FromImage(bmp))
                gfx.Clear(colors.background);

            foreach (Plot subplot in subplots)
                subplot.Render(bmp, SubplotRectangle(bmp.Size, subplot));
        }

        /// <summary>
        /// Return the area on the figure where the subplot will be rendered
        /// </summary>
        private RectangleF SubplotRectangle(SizeF figureSize, Plot subplot)
        {
            RectangleF renderArea = subplot.subplotPosition.GetRectangle(figureSize.Width, figureSize.Height);
            float padLeft, padRight, padBottom, padTop;
            padLeft = (subplot.subplotPosition.leftFrac == 0) ? padding.edges : padding.horizontal;
            padRight = (subplot.subplotPosition.rightFrac == 1) ? padding.edges : padding.horizontal;
            padBottom = (subplot.subplotPosition.botFrac == 1) ? padding.edges : padding.vertical;
            padTop = (subplot.subplotPosition.topFrac == 0) ? padding.edges : padding.vertical;
            renderArea = Tools.RectangleShrinkBy(renderArea, padLeft, padRight, padBottom, padTop);
            return renderArea;
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

        /// <summary>
        /// Return the plot the mouse is hovering over (or null if it's not over one)
        /// </summary>
        public Plot PlotUnderMouse(SizeF figureSize, Point currentLocation)
        {
            foreach (Plot subplot in subplots)
            {
                RectangleF rect = SubplotRectangle(figureSize, subplot);
                if (rect.Contains(currentLocation))
                    return subplot;
            }

            return null;
        }
    }
}
