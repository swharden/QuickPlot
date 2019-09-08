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

        private enum GraphicsFramework { GDI, Skia };
        private GraphicsFramework renderMethod = GraphicsFramework.GDI;

        /// <summary>
        /// Create a Figure (which contains one or more plots)
        /// </summary>
        public Figure()
        {
            Subplot(1, 1, 1);
        }

        /// <summary>
        /// Activate a subplot (and tweak its position in the Figure)
        /// </summary>
        public void Subplot(int plotNumber, int? row = null, int? column = null, int? rowSpan = null, int? colSpan = null)
        {
            while (plots.Count < plotNumber)
                plots.Add(new Plot());

            plot = plots[plotNumber - 1];

            if (row != null)
                plot.subplotSettings.rowIndex = (int)row - 1;
            if (column != null)
                plot.subplotSettings.colIndex = (int)column - 1;
            if (rowSpan != null)
                plot.subplotSettings.rowSpan = (int)rowSpan;
            if (colSpan != null)
                plot.subplotSettings.colSpan = (int)colSpan;
        }

        /// <summary>
        /// Render the figure onto an existing Bitmap
        /// </summary>
        public Bitmap Render(Bitmap bmp)
        {
            // determine the layout of Plots in the Figure (requires bmp dimensions)
            List<Rectangle> subplotRects = Settings.FigureLayout.GetSubplotlotRectangles(bmp.Size, plots);

            // render each plot inside its layout rectangle
            switch (renderMethod)
            {
                case GraphicsFramework.GDI:
                    using (var gfx = Graphics.FromImage(bmp))
                    {
                        gfx.Clear(Color.White);
                        for (int i = 0; i < plots.Count; i++)
                            Renderer.GDI.Render(bmp, gfx, subplotRects[i], plots[i]);
                    }
                    break;

                case GraphicsFramework.Skia:
                    for (int i = 0; i < plots.Count; i++)
                        Renderer.Skia.Render(bmp, subplotRects[i], plots[i]);
                    break;

                default:
                    throw new NotImplementedException();
            }

            return bmp;
        }

        /// <summary>
        /// Render the figure onto a new Bitmap
        /// </summary>
        public Bitmap Render(int width, int height)
        {
            // create a bitmap of a certain size and render onto it
            Bitmap bmp = new Bitmap(width, height);
            return Render(bmp);
        }
    }
}
