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

        private void RenderFrame(Bitmap bmp, Graphics gfx, Plot plot, Layout.SubImage sub, int padding = 5)
        {
            sub.ShrinkAll(padding);
            Rectangle rect = new Rectangle(sub.left, sub.top, sub.Width, sub.Height);
            gfx.DrawRectangle(Pens.Black, rect);

            FontFamily fmly = new FontFamily(System.Drawing.Text.GenericFontFamilies.Monospace);
            FontStyle style = FontStyle.Regular;
            float fontSize = 10;
            Font fnt = new Font(fmly, fontSize, style);
            Brush brsh = new SolidBrush(Color.Black);
            gfx.DrawString(plot.labels.top, fnt, brsh, sub.left, sub.top);
        }

        Layout.SubImage GetSubplotDims(int width, int height, int row = 0, int col = 0, int rows = 1, int cols = 1)
        {
            int subPlotHeight = height / rows;
            int subPlotWidth = width / cols;

            int left = row * subPlotWidth;
            int right = left + subPlotWidth;

            int top = col * subPlotHeight;
            int bottom = top + subPlotHeight;

            return new Layout.SubImage(left, right, bottom, top);
        }

        public Bitmap Render(Bitmap bmp)
        {
            // render onto an existing bitmap
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Tools.Misc.RandomColor);

                SubPlot(3); // ensure 4 plots exist
                plots[0].labels.top = "one";
                plots[1].labels.top = "two";
                plots[2].labels.top = "three";
                plots[3].labels.top = "four";

                RenderFrame(bmp, gfx, plots[0], GetSubplotDims(bmp.Width, bmp.Height, 0, 0, 2, 2));
                RenderFrame(bmp, gfx, plots[1], GetSubplotDims(bmp.Width, bmp.Height, 0, 1, 2, 2));
                RenderFrame(bmp, gfx, plots[2], GetSubplotDims(bmp.Width, bmp.Height, 1, 0, 2, 2));
                RenderFrame(bmp, gfx, plots[3], GetSubplotDims(bmp.Width, bmp.Height, 1, 1, 2, 2));
            }
            return bmp;
        }

        public Bitmap Render(int width, int height)
        {
            // create a bitmap of a certain size and render onto it
            Bitmap bmp = new Bitmap(width, height);
            return Render(bmp);
        }
    }
}
