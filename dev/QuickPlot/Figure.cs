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

        /// <summary>
        /// Create a Figure (which contains one or more plots)
        /// </summary>
        public Figure()
        {
            Subplot(0);
        }

        /// <summary>
        /// Activate a subplot
        /// </summary>
        public void Subplot(int subPlotIndex)
        {
            while (plots.Count < subPlotIndex + 1)
                plots.Add(new Plot());

            plot = plots[subPlotIndex];
        }

        /// <summary>
        /// Render the figure onto an existing Bitmap
        /// </summary>
        public Bitmap Render(Bitmap bmp)
        {
            var layout = new Layout();
            //layout.Grid(bmp.Size, plots.Count, 2);
            layout.Special(bmp.Size);
            layout.ShrinkAll(10);

            // render each plot inside its layout rectangle
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.Gray);
                for (int i=0; i<plots.Count; i++)
                    plots[i].Render(bmp, gfx, layout.rects[i]);
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
