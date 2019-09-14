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

namespace QuickPlot.Demos.Benchmarks
{
    public partial class OneThousandLines : Form
    {
        public OneThousandLines()
        {
            InitializeComponent();

            lblUsingGL.Text = interactivePlot1.IsUsingOpenGL ? "Using OpenGL: YES" : "Using OpenGL: NO";

            int pointCount = 100;
            double[] xs = Generate.Consecutative(pointCount, 1.0 / pointCount);

            interactivePlot1.fig.Subplot(2, 3, 1);
            interactivePlot1.fig.plot.Scatter(xs, Generate.Sin(pointCount));

            interactivePlot1.fig.Subplot(2, 3, 2);
            interactivePlot1.fig.plot.Scatter(xs, Generate.Cos(pointCount));

            interactivePlot1.fig.Subplot(2, 3, 3);
            interactivePlot1.fig.plot.Scatter(xs, Generate.Random(pointCount, seed: 0));

            interactivePlot1.fig.Subplot(2, 3, 4, 1, 3);
            interactivePlot1.fig.plot.Scatter(Generate.Random(pointCount, seed: 0), Generate.Random(pointCount, seed: 1));
        }

        private void OneThousandLines_Load(object sender, EventArgs e)
        {

        }

        private void BtnRender_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            interactivePlot1.Refresh();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            string message = string.Format("single render time: {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
            lblStatus.Text = message;
        }

        private void BtnBenchmark_Click(object sender, EventArgs e)
        {
            btnBenchmark.Enabled = false;
            int benchmarkCount = 100;
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i=0; i<benchmarkCount; i++)
            {
                interactivePlot1.Refresh();
            }
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            double meanSec = elapsedSec / benchmarkCount;
            string message = string.Format("mean render time: {0:0.00} ms ({1:0.00} Hz)", meanSec * 1000.0, 1 / meanSec);
            lblStatus.Text = message;
            btnBenchmark.Enabled = true;
        }
    }
}
