using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            plotA.title.text = "plot A";

            var plotB = interactivePlot1.figure.Subplot(3, 2, 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Sin(50));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Cos(50));
            plotB.title.text = "plot B";

            var plotC = interactivePlot1.figure.Subplot(3, 2, 3, colSpan: 2);
            double[] x = QuickPlot.Generate.Consecutative(CenterSubplotPointsCount);
            double[] y1 = QuickPlot.Generate.Random(CenterSubplotPointsCount, seed: 0);
            double[] y2 = QuickPlot.Generate.Random(CenterSubplotPointsCount, seed: 1);
            interactivePlot1.figure.plot.Scatter(x, y1);
            interactivePlot1.figure.plot.Scatter(x, y2);
            plotC.title.text = "plot C";

            var plotD = interactivePlot1.figure.Subplot(3, 2, 5);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));
            plotD.title.text = "plot D";

            var plotE = interactivePlot1.figure.Subplot(3, 2, 6);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));
            plotE.title.text = "plot E";

            // connect axes
            plotA.ShareX(plotE);
            plotB.ShareY(plotE);
            plotD.ShareY(plotE);
            plotD.ShareX(plotE);

            // after a few seconds disconnect axes
            Task.Run(() =>
            {
                Debug.WriteLine("plots A, B, and D are connected to E");
                Task.Delay(5_000).Wait();

                plotA.ShareX(null);
                Debug.WriteLine("plot A axes reset");
                Task.Delay(5_000).Wait();

                plotB.ShareY(null);
                Debug.WriteLine("plot B axes reset");
                Task.Delay(5_000).Wait();

                plotD.ShareX(null);
                plotD.ShareY(null);
                Debug.WriteLine("plot D axes reset");
                Task.Delay(5_000).Wait();

                plotA.ShareX(plotE);
                plotB.ShareY(plotE);
                plotD.ShareY(plotE);
                plotD.ShareX(plotE);
                Debug.WriteLine("original axes reconnected");
                Task.Delay(5_000).Wait();
            });
        }
    }
}
