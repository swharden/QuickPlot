using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QuickPlotTests
{
    [TestClass]
    public class FigureTest
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
            fig.GetBitmap(1, 1);
            fig.GetBitmap(600, 400);
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
    }
}
