using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PlotRandomData();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = cbRun.Checked;
        }

        private void PlotRandomData()
        {
            // create a complex figure with several subplots containing random data

            formsPlot1.fig.Clear();

            int randomSeed = 0;
            for (int i = 0; i < 4; i++)
            {
                formsPlot1.fig.Subplot(i + 1);
                formsPlot1.fig.plot.axes.labelTitle.text = (cbTitle.Checked) ? "title" : null;
                formsPlot1.fig.plot.axes.labelY.text = (cbYLabel.Checked) ? "vertical units" : null;
                formsPlot1.fig.plot.axes.labelY2.text = (cbY2Label.Checked) ? "more vertical units" : null;
                formsPlot1.fig.plot.axes.labelX.text = (cbXLabel.Checked) ? "horizontal units" : null;
                formsPlot1.fig.plot.axes.enableX = cbXScale.Checked;
                formsPlot1.fig.plot.axes.enableY = cbYScale.Checked;
                formsPlot1.fig.plot.axes.enableY2 = cbY2Scale.Checked;
                formsPlot1.fig.plot.advancedSettings.showLayout = cbShowLayout.Checked;

                formsPlot1.fig.plot.Scatter(
                        xs: QuickPlot.Tools.DataGen.RandomDoubles(50, randomSeed++, 10, -5),
                        ys: QuickPlot.Tools.DataGen.RandomDoubles(50, randomSeed++, 10, -5)
                    );
                formsPlot1.fig.plot.Scatter(
                        xs: QuickPlot.Tools.DataGen.RandomDoubles(50, randomSeed++, 5, -5),
                        ys: QuickPlot.Tools.DataGen.RandomDoubles(50, randomSeed++, 5, -5)
                    );
            }

            formsPlot1.fig.Subplot(1, 1, 1);
            formsPlot1.fig.Subplot(2, 1, 2);
            formsPlot1.fig.Subplot(3, 1, 3);
            formsPlot1.fig.Subplot(4, 2, 1, 3, 1); // extra-wide

            formsPlot1.Render();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            double[] times = new double[100];

            toolStripProgressBar1.Visible = true;
            lblStatus.Visible = false;
            toolStripProgressBar1.Value = 0;
            Application.DoEvents();
            var b = new QuickPlot.Tools.Benchmark();
            for (int i = 0; i < times.Length; i++)
            {
                b.Restart();
                formsPlot1.Render();
                b.Stop();
                times[i] = b.hz;
                toolStripProgressBar1.Value = i * toolStripProgressBar1.Maximum / times.Length;
            }
            toolStripProgressBar1.Visible = false;
            lblStatus.Visible = true;

            double meanMsec = times.Sum() / times.Length;
            double meanSec = meanMsec / 1000.0;
            double meanHz = Math.Round(1.0 / meanSec, 3);
            lblStatus.Text = $"Rendered {times.Length} times in {Math.Round(times.Sum(), 3)} msec ({meanHz} Hz)";
        }

        private void CbTitle_CheckedChanged(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void CbXLabel_CheckedChanged(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void CbYLabel_CheckedChanged(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void CbY2Label_CheckedChanged(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void CbYScale_CheckedChanged(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void CbXScale_CheckedChanged(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void CbY2Scale_CheckedChanged(object sender, EventArgs e)
        {
            PlotRandomData();
        }

        private void CbShowLayout_CheckedChanged(object sender, EventArgs e)
        {
            PlotRandomData();
        }
    }
}
