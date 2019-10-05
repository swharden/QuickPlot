﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace QuickPlot
{
    public class Figure
    {
        public readonly List<Plot> subplots = new List<Plot>();
        public Plot plot;

        private FigureSettings.Padding padding = new FigureSettings.Padding();

        public Figure()
        {
            Subplot(1, 1, 1);
        }

        public void Subplot(int nRows, int nCols, int subPlotNumber, int rowSpan = 1, int colSpan = 1)
        {
            // ensure we have enough plots to get to this subplots
            while (subplots.Count < subPlotNumber)
                subplots.Add(new Plot());

            // activate this subplot
            plot = subplots[subPlotNumber - 1];

            // note the position of this subplot in the bigger figure (pixel-independent)
            plot.subplotPosition = new PlotSettings.SubplotPosition(nRows, nCols, subPlotNumber, rowSpan, colSpan);
        }

        private void Render(Bitmap bmp)
        {
            // render onto the given bitmap
            if (bmp == null)
                throw new ArgumentException("bmp must not be null");

            // clear the plot
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.White);
            }

            // draw each plottable
            foreach (Plot subplot in subplots)
            {
                // use figure-level settings to determine the plot area for each subplot
                RectangleF subplotRect = FigureSettings.Layout.GetRectangle(bmp, subplot.subplotPosition, padding);
                subplot.Render(bmp, subplotRect);

                // outline the plottable
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.DrawRectangle(Pens.Black, subplotRect.X, subplotRect.Y, subplotRect.Width, subplotRect.Height);
                }
            }
        }

        public Bitmap GetBitmap(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Render(bmp);
            return bmp;
        }

        public Bitmap GetBitmap(Bitmap existingBitmap)
        {
            if (existingBitmap == null)
                throw new ArgumentException("existingBitmap must not be null");

            Render(existingBitmap);
            return existingBitmap;
        }

        public void Save(string filePath, int width, int height)
        {
            if (width < 1 || height < 1)
                throw new ArgumentException("width and height must be >0");

            Bitmap bmp = GetBitmap(width, height);
            bmp.Save(filePath);

            Debug.WriteLine($"Saved {System.IO.Path.GetFullPath(filePath)}");
        }
    }
}
