using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    /// <summary>
    /// Contains position information about subplots in a Figure
    /// </summary>
    public class SubplotLayout
    {
        public List<Rectangle> rects = new List<Rectangle>();

        public void ArrangeGrid(Size figSize, int plotCount, int columns)
        {
            int rows = plotCount / columns;
            int subPlotWidth = figSize.Width / columns;
            int subPlotHeight = figSize.Height / rows;
            Size subPlotSize = new Size(subPlotWidth, subPlotHeight);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Point subPlotOrigin = new Point(col * subPlotSize.Width, row * subPlotSize.Height);
                    rects.Add(new Rectangle(subPlotOrigin, subPlotSize));
                }
            }
        }

        public void ArrangeCustom(Size figSize)
        {
            // create a special layout with 3 on top and 1 on the bottom
            int plotCount = 6;
            int columns = 3;
            int rows = plotCount / columns;
            int subPlotWidth = figSize.Width / columns;
            int subPlotHeight = figSize.Height / rows;
            Size subPlotSize = new Size(subPlotWidth, subPlotHeight);
            Size subPlotSizeTripleWide = new Size(subPlotWidth * 3, subPlotHeight);

            for (int col = 0; col < 3; col++)
                rects.Add(new Rectangle(new Point(col * subPlotSize.Width, 0), subPlotSize));

            Point subPlotOrigin = new Point(0, subPlotSize.Height);
            rects.Add(new Rectangle(subPlotOrigin, subPlotSizeTripleWide));
        }

        public void ShrinkAll(int px)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                Rectangle rect = rects[i];
                rect.X += px;
                rect.Width -= px * 2;
                rect.Y += px;
                rect.Height -= px * 2;
                rects[i] = rect;
            }
        }
    }
}
