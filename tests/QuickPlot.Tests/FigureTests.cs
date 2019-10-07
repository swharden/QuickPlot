using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace QuickPlotTests
{
    [TestClass]
    public class FigureTests
    {
        [TestMethod]
        public void Instantiate()
        {
            var fig = new QuickPlot.Figure();
        }

        [TestMethod]
        public void GetBitmap()
        {
            var fig = new QuickPlot.Figure();

            Bitmap bmpOut;

            bmpOut = fig.GetBitmap(1, 1);
            Assert.AreEqual(bmpOut.Width, 1);
            Assert.AreEqual(bmpOut.Height, 1);

            bmpOut = fig.GetBitmap(600, 400);
            Assert.AreEqual(bmpOut.Width, 600);
            Assert.AreEqual(bmpOut.Height, 400);

            Bitmap bmpIn = new Bitmap(321, 123);
            bmpOut = fig.GetBitmap(bmpIn);
            Assert.AreEqual(bmpIn, bmpOut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBitmap_BadArguments()
        {
            var fig = new QuickPlot.Figure();
            fig.GetBitmap(0, 0);
        }

        [TestMethod]
        public void Save()
        {
            var fig = new QuickPlot.Figure();
            fig.Save("test-1-1.bmp", 1, 1);
            fig.Save("test-600-400.bmp", 600, 400);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Save_BadArguments()
        {
            var fig = new QuickPlot.Figure();
            fig.Save("test-0-0.bmp", 0, 0);
        }

        [TestMethod]
        public void Clear()
        {
            
            var fig = new QuickPlot.Figure();

            // create a complicated multi-subplot figure

            fig.Subplot(2, 3, 1);
            double[] xs = QuickPlot.Generate.Consecutative(20, 1.0 / 20);
            fig.plot.Scatter(xs, QuickPlot.Generate.Sin(xs.Length));
            fig.plot.Scatter(xs, QuickPlot.Generate.Cos(xs.Length));

            fig.Subplot(2, 3, 2);
            fig.plot.Scatter(xs, QuickPlot.Generate.Cos(xs.Length));

            fig.Subplot(2, 3, 3);
            fig.plot.Scatter(xs, QuickPlot.Generate.Random(xs.Length, seed: 0));

            fig.Subplot(2, 3, 4, 1, 3);
            double[] randomXs = QuickPlot.Generate.Random(100, seed: 1);
            double[] randomYs = QuickPlot.Generate.Random(100, seed: 2);
            fig.plot.Scatter(randomXs, randomYs);

            // now clear it
            fig.Clear();

            fig.Save("test-clear.bmp", 600, 400);
        }
    }
}
