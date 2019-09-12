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
    public partial class OneThousandLinesGL : Form
    {
        public OneThousandLinesGL()
        {
            InitializeComponent();
            interactivePlot1.fig.Subplot(2, 3, 1);
            interactivePlot1.fig.Subplot(2, 3, 2);
            interactivePlot1.fig.Subplot(2, 3, 3);
            interactivePlot1.fig.Subplot(2, 3, 4, 1, 3);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            interactivePlot1.Refresh();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            string message = string.Format("single render time: {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
            lblStatus.Text = message;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            int benchmarkCount = 100;
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < benchmarkCount; i++)
            {
                interactivePlot1.Refresh();
            }
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            double meanSec = elapsedSec / benchmarkCount;
            string message = string.Format("mean render time: {0:0.00} ms ({1:0.00} Hz)", meanSec * 1000.0, 1 / meanSec);
            lblStatus.Text = message;
            button2.Enabled = true;
        }
    }
}
