using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlotTests
{
    [TestClass]
    public class Cookbook
    {
        private int imageWidth = 600;
        private int imageHeight = 400;

        [TestMethod]
        public void GenerateCookbook()
        {
            var fig = new QuickPlot.Figure();

            fig.Subplot(2, 3, 1);
            double[] xs = QuickPlot.Generate.Consecutative(20, 1.0 / 20);
            fig.plot.Scatter(xs, QuickPlot.Generate.Sin(xs.Length));
            fig.plot.Scatter(xs, QuickPlot.Generate.Cos(xs.Length));

            fig.Subplot(2, 3, 2);
            fig.plot.Scatter(xs, QuickPlot.Generate.Cos(xs.Length));

            fig.Subplot(2, 3, 3);
            fig.plot.Scatter(xs, QuickPlot.Generate.Random(xs.Length));

            fig.Subplot(2, 3, 4, 1, 3);
            double[] randomXs = QuickPlot.Generate.Random(100, seed: 0);
            double[] randomYs = QuickPlot.Generate.Random(100, seed: 1);
            fig.plot.Scatter(randomXs, randomYs);

            fig.Save("cookbook_example.png", imageWidth, imageHeight);
        }
    }
}
