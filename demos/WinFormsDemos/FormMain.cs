using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDemos
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            DemoLayout();
        }

        public void DemoLayout()
        {
            int CenterSubplotPointsCount = 1000;

            interactivePlot1.figure.Clear();

            var plotA = interactivePlot1.figure.Subplot(3, 2, 1);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));

            var plotB = interactivePlot1.figure.Subplot(3, 2, 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Sin(50));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Cos(50));

            interactivePlot1.figure.Subplot(3, 2, 3, colSpan: 2);
            double[] x = QuickPlot.Generate.Consecutative(CenterSubplotPointsCount);
            double[] y1 = QuickPlot.Generate.Random(CenterSubplotPointsCount, seed: 0);
            double[] y2 = QuickPlot.Generate.Random(CenterSubplotPointsCount, seed: 1);
            interactivePlot1.figure.plot.Scatter(x, y1);
            interactivePlot1.figure.plot.Scatter(x, y2);

            var plotC = interactivePlot1.figure.Subplot(3, 2, 5);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));

            var plotD = interactivePlot1.figure.Subplot(3, 2, 6, sharex: plotA, sharey: plotB);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));

            plotC.ShareAxis(plotD, plotD);
            Task.Run(() => {
                Task.Delay(10_000).Wait();
                plotC.UnShareAxis(true, true);
                Task.Delay(10_000).Wait();
                plotA.UnShareAxis();
                Task.Delay(10_000).Wait();
                plotB.UnShareAxis();
            });
        }
    }
}
