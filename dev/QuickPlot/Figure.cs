using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    /// <summary>
    /// The Figure class holds several subplots. 
    /// Activate and arrange subplots with Figure.Subplot().
    /// Plot stuff by interacting with Figure.Plot.
    /// </summary>
    public class Figure
    {
        public List<Plot> subplots = new List<Plot>();
        public Plot plot;

        /// <summary>
        /// Create a Figure (with one subplot)
        /// </summary>
        public Figure()
        {
            Subplot(1, 1, 1);
        }

        /// <summary>
        /// Activate a subplot (and set its position)
        /// </summary>
        public void Subplot(int subPlotNumber, int? row = null, int? column = null, int? rowSpan = null, int? colSpan = null)
        {
            while (subplots.Count < subPlotNumber)
                subplots.Add(new Plot());

            plot = subplots[subPlotNumber - 1];

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
            // to render with another framework (e.g., Skia) override this method.
            List<Rectangle> subplotRects = Settings.FigureLayout.GetSubplotlotRectangles(bmp.Size, subplots);
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.White);
                for (int i = 0; i < subplots.Count; i++)
                    Renderer.GDI.Render(bmp, gfx, subplotRects[i], subplots[i]);
            }
            return bmp;
        }

        /// <summary>
        /// Render the figure onto a new Bitmap
        /// </summary>
        public Bitmap Render(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            return Render(bmp);
        }
    }
}
