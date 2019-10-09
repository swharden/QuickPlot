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

namespace QuickPlotDemos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupMultiLayout();
            btnRender_Click(null, null);
        }

        private void SetupMultiLayout()
        {
            interactivePlot1.figure.Clear();

            interactivePlot1.figure.Subplot(3, 2, 1);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));

            interactivePlot1.figure.Subplot(3, 2, 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Sin(50));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Cos(50));

            interactivePlot1.figure.Subplot(3, 2, 3, colSpan: 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Random(50, seed: 0));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Random(50, seed: 1));

            interactivePlot1.figure.Subplot(3, 2, 5);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));

            interactivePlot1.figure.Subplot(3, 2, 6);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            interactivePlot1.Render();

            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Text = string.Format("Single render in {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
        }

        private void btnBenchmark_Click(object sender, EventArgs e)
        {
            btnRender.Enabled = false;
            btnBenchmark.Enabled = false;

            Stopwatch stopwatch = Stopwatch.StartNew();

            int renderCount = 20;
            for (int i=0; i< renderCount; i++)
            {
                interactivePlot1.Render();
            }

            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            elapsedSec /= renderCount;
            Text = string.Format("Mean render time: {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);

            btnRender.Enabled = true;
            btnBenchmark.Enabled = true;
        }
    }
}
