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
            interactivePlot1.figure.Clear();

            interactivePlot1.figure.Subplot(3, 2, 1);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));

            interactivePlot1.figure.Subplot(3, 2, 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Sin(50));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Cos(50));
            int n = 10_000;
            interactivePlot1.figure.Subplot(3, 2, 3, colSpan: 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(n), QuickPlot.Generate.Random(n, seed: 0));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(n), QuickPlot.Generate.Random(n, seed: 1));

            interactivePlot1.figure.Subplot(3, 2, 5);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));

            interactivePlot1.figure.Subplot(3, 2, 6);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));
        }
    }
}
