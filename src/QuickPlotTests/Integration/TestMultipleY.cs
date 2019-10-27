using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QuickPlotTests.Integration
{
    public class TestMultipleY
    {
        [Test]
        public void Test_MultipleY()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ysSmall = QuickPlot.Generate.Sin(100, mult: .01);
            double[] ysBig = QuickPlot.Generate.Cos(100, mult: 100);

            var figure = new QuickPlot.Figure();
            figure.plot.Scatter(xs, ysSmall, color: SkiaSharp.SKColors.Blue);
            figure.plot.Scatter(xs, ysBig, secondY: true, color: SkiaSharp.SKColors.Red);

            figure.plot.YLabel("Primary Y", color: SkiaSharp.SKColors.Blue);
            figure.plot.YLabel("Secondary Y", secondY: true, color: SkiaSharp.SKColors.Red);
            figure.plot.XLabel("Horizontal Axis");
            figure.plot.Title("Twin Y Axis Demo");

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }
    }
}
