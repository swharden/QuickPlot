﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace QuickPlot
{
    public static class Tools
    {
        public static Color IndexedColor10(int index)
        {
            string[] plottableColors10 = new string[] {
                "#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd",
                "#8c564b", "#e377c2", "#7f7f7f", "#bcbd22", "#17becf"
            };
            return ColorTranslator.FromHtml(plottableColors10[index % plottableColors10.Length]);
        }

        public static void TestImageLines(Bitmap bmp, int count = 1_000, bool antiAlias = true, int seed = 0)
        {
            Random rand = new Random(seed);

            Graphics gfx = Graphics.FromImage(bmp);
            gfx.SmoothingMode = (antiAlias) ? System.Drawing.Drawing2D.SmoothingMode.AntiAlias : System.Drawing.Drawing2D.SmoothingMode.None;
            gfx.Clear(Color.DarkBlue);

            for (int i = 0; i < count; i++)
            {
                Point pt1 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                Point pt2 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                gfx.DrawLine(Pens.LightSteelBlue, pt1, pt2);
            }
        }
    }
}
