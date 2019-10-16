using NUnit.Framework;
using System.Diagnostics;
using System.Reflection;

namespace QuickPlot.Tests.NUnitTests
{
    [TestFixture]
    class Subplot
    {
        private readonly int width = 600;
        private readonly int height = 400;

        [Test]
        public void Subplot_Default()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);

            // by default (no calls to SubPlot) there is a single plot
            var figure = new QuickPlot.Figure();
            figure.plot.Scatter(xs, ys);
            figure.Save(width, height, MethodBase.GetCurrentMethod().Name + ".png");
        }

        [Test]
        public void Subplot_TwoStackedHorizontally()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);

            var figure = new QuickPlot.Figure();
            figure.Subplot(1, 2, 1);
            figure.plot.Scatter(xs, ys);
            figure.Subplot(1, 2, 2);
            figure.plot.Scatter(xs, ys);

            figure.Save(width, height, MethodBase.GetCurrentMethod().Name + ".png");
        }

        [Test]
        public void Subplot_TwoStackedVertically()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);

            var figure = new QuickPlot.Figure();
            figure.Subplot(2, 1, 1);
            figure.plot.Scatter(xs, ys);
            figure.Subplot(2, 1, 2);
            figure.plot.Scatter(xs, ys);

            figure.Save(width, height, MethodBase.GetCurrentMethod().Name + ".png");
        }

        [Test]
        public void Subplot_FourInGrid()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);

            var figure = new QuickPlot.Figure();
            figure.Subplot(2, 2, 1);
            figure.plot.Scatter(xs, ys);
            figure.Subplot(2, 2, 2);
            figure.plot.Scatter(xs, ys);
            figure.Subplot(2, 2, 3);
            figure.plot.Scatter(xs, ys);
            figure.Subplot(2, 2, 4);
            figure.plot.Scatter(xs, ys);

            figure.Save(width, height, MethodBase.GetCurrentMethod().Name + ".png");
        }

        [Test]
        public void Subplot_TwoOverOne()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);

            var figure = new QuickPlot.Figure();
            figure.Subplot(2, 2, 1);
            figure.plot.Scatter(xs, ys);
            figure.Subplot(2, 2, 2);
            figure.plot.Scatter(xs, ys);
            figure.Subplot(2, 2, 3, colSpan: 2);
            figure.plot.Scatter(xs, ys);

            figure.Save(width, height, MethodBase.GetCurrentMethod().Name + ".png");
        }

        [Test]
        public void Subplot_TwoBesideOne()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);

            var figure = new QuickPlot.Figure();
            figure.Subplot(2, 2, 1, rowSpan: 2);
            figure.plot.Scatter(xs, ys);
            figure.Subplot(2, 2, 2);
            figure.plot.Scatter(xs, ys);
            figure.Subplot(2, 2, 4);
            figure.plot.Scatter(xs, ys);

            figure.Save(width, height, MethodBase.GetCurrentMethod().Name + ".png");
        }
    }
}
