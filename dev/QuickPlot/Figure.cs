using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    public class Figure
    {
        public List<Plot> plots = new List<Plot>();
        public Plot plot;

        public Figure()
        {
            SubPlot(0); // first plot is selected by default
        }

        public void SubPlot(int subPlotIndex)
        {
            // create subplots if needed
            while (plots.Count < subPlotIndex + 1)
                plots.Add(new Plot());

            // activate this index
            plot = plots[subPlotIndex];
        }

        public Bitmap Render(Bitmap bmp, int columns = 1)
        {
            // render onto an existing bitmap
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Tools.Misc.RandomColor);
                for (int i=0; i<plots.Count; i++)
                {
                    Rectangle subPlotRect = SubplotRect(bmp.Size, 2, 2, i);
                    subPlotRect = Tools.Misc.Contract(subPlotRect, 10, 10);
                    gfx.FillRectangle(Brushes.White, subPlotRect);
                    gfx.DrawRectangle(Pens.Black, subPlotRect);
                    gfx.DrawString($"{i}: {plots[i].labels.top}",
                         new Font(FontFamily.GenericMonospace, 10), 
                         Brushes.Black, subPlotRect.X, subPlotRect.Y);
                    Console.WriteLine(i);
                }
            }
            return bmp;
        }

        public Bitmap Render(int width, int height)
        {
            // create a bitmap of a certain size and render onto it
            Bitmap bmp = new Bitmap(width, height);
            return Render(bmp);
        }

        public Rectangle SubplotRect(Size figSize, int columns, int rows, int index)
        {
            int subPlotWidth = figSize.Width / columns;
            int subPlotHeight = figSize.Height / rows;
            Size subPlotSize = new Size(subPlotWidth, subPlotHeight);

            int column = index % columns;
            int row = index / rows;
            Point subPlotOrigin = new Point(column * subPlotSize.Width, row * subPlotSize.Height);

            return new Rectangle(subPlotOrigin, subPlotSize);
        }
    }
}
