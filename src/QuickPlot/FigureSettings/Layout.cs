using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.FigureSettings
{
    public static class Layout
    {
        /// <summary>
        /// Return the region of the figure the subplot should render inside of
        /// </summary>
        public static SKRect GetRectangle(SKCanvas canvas, PlotSettings.SubplotPosition subplotPosition, Padding padding)
        {
            // calculate the rectangle of the subplot in the figure
            float figureWidth = canvas.LocalClipBounds.Width;
            float figureHeight = canvas.LocalClipBounds.Height;

            float spWidth = figureWidth / subplotPosition.nCols;
            float spHeight = figureHeight / subplotPosition.nRows;

            float col = (subplotPosition.subPlotNumber - 1) % subplotPosition.nCols;
            float row = (subplotPosition.subPlotNumber - 1) / subplotPosition.nCols;

            float left = col * spWidth;
            float top = row * spHeight;
            float width = spWidth * subplotPosition.colSpan;
            float height = spHeight * subplotPosition.rowSpan;

            SKRect rect = new SKRect(left, top, left + width, top + height);

            // add between-plot padding to every frame
            rect.Left += padding.horizontal;
            rect.Right -= padding.horizontal;
            rect.Top += padding.vertical;
            rect.Bottom -= padding.vertical;

            // add edge padding to the top
            if (row == 0)
                rect.Top += padding.edges;

            // add edge padding to the right
            if ((row + subplotPosition.rowSpan) == subplotPosition.nRows)
                rect.Bottom -= padding.edges;

            // add edge padding to the left
            if (col == 0)
                rect.Left += padding.edges;

            // add edge padding to the right
            if ((col + subplotPosition.colSpan) == subplotPosition.nCols)
                rect.Right -= padding.edges;

            return rect;
        }
    }
}
