using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace QuickPlot
{
    public class Figure
    {
        public readonly List<Plot> subplots = new List<Plot>();
        public Plot plot;

        private FigureSettings.Padding padding = new FigureSettings.Padding();

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

            // clear the default plot if it exists
            if (subplots.Count() > 0)
                if (subplots.First().subplotPosition.widthFrac == 1 && subplots.First().subplotPosition.widthFrac == 1)
                    subplots.Clear();

            // if no plot with this configuration exists, create it, activate it, and size it
            subplots.Add(new Plot());
            plot = subplots.Last();
            plot.subplotPosition = new PlotSettings.SubplotPosition(nRows, nCols, subPlotNumber, rowSpan, colSpan);
        }

        public void Clear()
        {
            subplots.Clear();
            Subplot(1, 1, 1);
        }

        private void Render(Bitmap bmp)
        {
            // render onto the given bitmap
            if (bmp == null)
                throw new ArgumentException("bmp must not be null");

            // clear the plot
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(SystemColors.Control);
            }

            // draw each plottable
            foreach (Plot subplot in subplots)
            {
                // use figure-level settings to determine the plot area for each subplot
                //RectangleF subplotRect = FigureSettings.Layout.GetRectangle(bmp, subplot.subplotPosition, padding);

                subplot.Render(bmp);
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
