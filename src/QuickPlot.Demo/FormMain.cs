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

namespace QuickPlot.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblStatus.Text = "";
        }

        private void BtnHeadless_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            QuickPlot.Render.RenderAndSave(1920, 1080);
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            string message = string.Format("headless demo completed in {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
            lblStatus.Text = message;
        }

        private void BtnRender_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            formsPlot1.Refresh();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            string message = string.Format("single render completed in {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
            lblStatus.Text = message;
        }

        private void BtnBenchmark_Click(object sender, EventArgs e)
        {
            int benchmarkCount = 50;
            btnBenchmark.Enabled = false;
            lblStatus.Text = $"rendering {benchmarkCount} times...";
            Application.DoEvents();
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i=0; i< benchmarkCount; i++)
                formsPlot1.Refresh();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            elapsedSec /= benchmarkCount;
            string message = string.Format("mean render time: {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
            lblStatus.Text = message;
            btnBenchmark.Enabled = true;
        }
    }
}
