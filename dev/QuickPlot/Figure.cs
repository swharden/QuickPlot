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
            // determine the layout of Plots in the Figure. 
            // This requires knowing Bitmap dimensions.
            var layout = new SubplotLayout();
            //layout.ArrangeGrid(bmp.Size, plots.Count, 2);
            layout.ArrangeCustom(bmp.Size);

            // adjust layout to accomodate inter-plot spacing
            layout.ShrinkAll(10);

            // render each plot inside its layout rectangle
            switch (renderMethod)
            {
                case GraphicsFramework.GDI:
                    using (var gfx = Graphics.FromImage(bmp))
                    {
                        gfx.Clear(Color.White);
                        for (int i = 0; i < plots.Count; i++)
                            Renderer.GDI.Render(bmp, gfx, layout.rects[i], plots[i]);
                    }
                    break;

                case GraphicsFramework.Skia:
                    for (int i = 0; i < plots.Count; i++)
                        Renderer.Skia.Render(bmp, layout.rects[i], plots[i]);
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
