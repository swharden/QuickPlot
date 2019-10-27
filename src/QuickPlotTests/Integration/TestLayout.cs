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

            figure.plot.title.text = "Label Display Test";
            figure.plot.xLabel.text = "Primary Horizontal Axis";
            figure.plot.yLabel.text = "Primary Axis";
            figure.plot.y2Label.text = "Secondary Axis";

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

            figure.plot.title.text = "Label Display Test";
            figure.plot.title.fontSize = 48;

            figure.plot.xLabel.text = "Primary Horizontal Axis";
            figure.plot.xLabel.fontSize = 36;

            figure.plot.yLabel.text = "Primary Axis";
            figure.plot.yLabel.fontSize = 36;

            figure.plot.y2Label.text = "Secondary Axis";
            figure.plot.y2Label.fontSize = 36;


            Tools.SaveFig(figure, MethodBase.GetCurrentMethod().Name);
        }
    }
}
