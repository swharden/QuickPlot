using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QuickPlotTests.Integration
{
    public class TestLayout
    {
        [Test]
        public void Test_Layout_Default()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);
            var figure = new QuickPlot.Figure();
            figure.plot.Scatter(xs, ys);

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }

        [Test]
        public void Test_Layout_CustomLabels()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);
            var figure = new QuickPlot.Figure();
            figure.plot.Scatter(xs, ys);
            figure.plot.axes2.y.display = true;

            figure.plot.Title("Label Display Test");
            figure.plot.XLabel("Primary Horizontal Axis");
            figure.plot.YLabel("Primary Axis");
            figure.plot.YLabel("Secondary Axis");

            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }

        [Test]
        public void Test_Layout_CustomLabelSizes()
        {
            double[] xs = QuickPlot.Generate.Consecutative(100);
            double[] ys = QuickPlot.Generate.Random(100);
            var figure = new QuickPlot.Figure();
            figure.plot.Scatter(xs, ys);
            figure.plot.axes2.y.display = true;

            figure.plot.Title("Label Display Test", fontSize: 48);
            figure.plot.XLabel("Primary Horizontal Axis", fontSize: 36);
            figure.plot.YLabel("Primary Axis", fontSize: 36);
            figure.plot.YLabel("Secondary Axis", fontSize: 36);
            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }
    }
}
