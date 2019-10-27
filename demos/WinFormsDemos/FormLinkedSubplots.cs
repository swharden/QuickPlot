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
    public partial class FormLinkedSubplots : Form
    {
        public FormLinkedSubplots()
        {
            InitializeComponent();
            DemoLayout();
        }

        QuickPlot.Plot plotA, plotB, plotC, plotD, plotE;

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnShare.BackColor = (btnShare.BackColor == Color.Yellow) ? SystemColors.Control : Color.Yellow;
        }

        private void FormLinkedSubplots_Load(object sender, EventArgs e)
        {

        }

        public void DemoLayout()
        {
            int CenterSubplotPointsCount = 1000;

            interactivePlot1.figure.Reset();

            plotA = interactivePlot1.figure.Subplot(3, 2, 1);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));
            plotA.Title("plot A: shared X");

            plotB = interactivePlot1.figure.Subplot(3, 2, 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Sin(50));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Cos(50));
            plotB.Title("plot B: shared Y");

            plotC = interactivePlot1.figure.Subplot(3, 2, 3, colSpan: 2);
            double[] x = QuickPlot.Generate.Consecutative(CenterSubplotPointsCount);
            double[] y1 = QuickPlot.Generate.Random(CenterSubplotPointsCount, seed: 0);
            double[] y2 = QuickPlot.Generate.Random(CenterSubplotPointsCount, seed: 1);
            interactivePlot1.figure.plot.Scatter(x, y1);
            interactivePlot1.figure.plot.Scatter(x, y2);
            plotC.Title("plot C");

            plotD = interactivePlot1.figure.Subplot(3, 2, 5);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));
            plotD.Title("plot D: shared X and Y");

            plotE = interactivePlot1.figure.Subplot(3, 2, 6);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));
            plotE.Title("plot E: source axes");
        }

        private void btnUnshare_Click(object sender, EventArgs e)
        {
            plotA.ShareX(null);
            plotB.ShareY(null);
            plotD.ShareY(null);
            plotD.ShareX(null);
            interactivePlot1.Render();
        }

        private void btnShare_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btnShare.BackColor = SystemColors.Control;

            plotA.ShareX(plotE);
            plotB.ShareY(plotE);
            plotD.ShareY(plotE);
            plotD.ShareX(plotE);
            interactivePlot1.Render();
        }
    }
}
