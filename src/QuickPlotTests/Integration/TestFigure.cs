using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QuickPlotTests.Integration
{
    public class TestFigure
    {
        [Test]
        public void Test_Figure_Clear()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            var figure = new QuickPlot.Figure();

            // crete a multi-panel subplot
            figure.Subplot(2, 1, 1);
            figure.plot.Scatter(xs, QuickPlot.Generate.Random(100, 10));
            figure.Subplot(2, 1, 2);
            figure.plot.Scatter(xs, QuickPlot.Generate.Random(100, 100));

            // start over
            figure.Clear();
            figure.plot.Scatter(xs, QuickPlot.Generate.Sin(100));

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }

        [Test]
        public void Test_Figure_Reset()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            var figure = new QuickPlot.Figure();

            // crete a multi-panel subplot
            figure.Subplot(2, 1, 1);
            figure.plot.Scatter(xs, QuickPlot.Generate.Random(100, 10));
            figure.Subplot(2, 1, 2);
            figure.plot.Scatter(xs, QuickPlot.Generate.Random(100, 100));

            // start over
            figure.Reset();
            figure.plot.Scatter(xs, QuickPlot.Generate.Sin(100));

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }

        [Test]
        public void Test_Figure_Style()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            var figure = new QuickPlot.Figure();

            figure.Subplot(2, 2, 1);
            figure.plot.Scatter(xs, QuickPlot.Generate.Random(100));
            figure.Subplot(2, 2, 2);
            figure.plot.Scatter(xs, QuickPlot.Generate.Random(100));
            figure.Subplot(2, 2, 3);
            figure.plot.Scatter(xs, QuickPlot.Generate.Sin(100));
            figure.Subplot(2, 2, 4);
            figure.plot.Scatter(xs, QuickPlot.Generate.Cos(100));

            figure.Style(bgColor: SkiaSharp.SKColors.Honeydew, edgePadding: 25,
                subplotPaddingHorizontal: 20, subplotPaddingVertical: 40);

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }
    }
}
