﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickPlot.FigureSettings
{
    public static class Layout
    {
        /// <summary>
        /// Return the region of the figure the subplot should render inside of
        /// </summary>
        public static RectangleF GetRectangle(Bitmap bmp, PlotSettings.SubplotPosition subplotPosition, Padding padding)
        {
            // calculate the rectangle of the subplot in the figure
            float figureWidth = bmp.Width;
            float figureHeight = bmp.Height;

            float spWidth = figureWidth / subplotPosition.nCols;
            float spHeight = figureHeight / subplotPosition.nRows;

            float col = (subplotPosition.subPlotNumber - 1) % subplotPosition.nCols;
            float row = (subplotPosition.subPlotNumber - 1) / subplotPosition.nCols;

            float left = col * spWidth;
            float top = row * spHeight;
            float width = spWidth * subplotPosition.colSpan;
            float height = spHeight * subplotPosition.rowSpan;

            RectangleF rect = new RectangleF(left, top, width, height);

            // add between-plot padding to every frame
            rect.X += padding.horizontal;
            rect.Width -= padding.horizontal * 2;
            rect.Y += padding.vertical;
            rect.Height -= padding.vertical * 2;

            // add edge padding to the top
            if (row == 0)
            {
                rect.Y += padding.edges;
                rect.Height -= padding.edges;
            }

            // add edge padding to the left
            if (col == 0)
            {
                rect.X += padding.edges;
                rect.Width -= padding.edges;
            }

            // add edge padding to the bottom
            if ((row + subplotPosition.rowSpan) == subplotPosition.nRows)
            {
                rect.Height -= padding.edges;
            }

            // add edge padding to the right
            if ((col + subplotPosition.colSpan) == subplotPosition.nCols)
            {
                rect.Width -= padding.edges;
            }

            return rect;
        }
    }
}
