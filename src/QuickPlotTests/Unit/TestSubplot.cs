using NUnit.Framework;
using System.Diagnostics;
using System.Reflection;

namespace QuickPlotTests.Unit
{
    [TestFixture]
    class TestSubplot
    {
        [Test]
        public void Subplot_Default()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);

            // by default (no calls to SubPlot) there is a single plot
            var figure = new QuickPlot.Figure();
            figure.plot.Scatter(xs, ys);

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
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

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
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

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
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

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
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

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
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

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }

        [Test]
        public void Subplot_NoSharedAxes()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys1 = QuickPlot.Generate.Sin(100);
            double[] ys2 = QuickPlot.Generate.Cos(100);

            var figure = new QuickPlot.Figure();

            var plot1 = figure.Subplot(2, 1, 1);
            var plot2 = figure.Subplot(2, 1, 2);

            plot1.Scatter(xs, ys1);
            plot2.Scatter(xs, ys2);

            plot1.axes.Set(-20, 120, -2, 2);

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }

        [Test]
        public void Subplot_SharedX()
        {
            // TODO: currently there is a problem because AutoAxis() is getting called during the render

            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys1 = QuickPlot.Generate.Sin(100);
            double[] ys2 = QuickPlot.Generate.Cos(100);

            var figure = new QuickPlot.Figure();

            var plot1 = figure.Subplot(2, 1, 1);
            var plot2 = figure.Subplot(2, 1, 2);

            plot1.Scatter(xs, ys1);
            plot2.Scatter(xs, ys2);

            plot1.ShareY(plot2);

            plot1.axes.Zoom(.5, .5);

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }
    }
}
