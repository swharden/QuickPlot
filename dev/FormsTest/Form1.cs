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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GeneratePlot();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            GeneratePlot();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GeneratePlot();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = cbRun.Checked;
        }

        private double GeneratePlot()
        {
            if (pictureBox1.Image == null)
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            double timeMsec;

            using (var bench = new QuickPlot.Tools.Benchmark())
            {
                var fig = new QuickPlot.Figure();

                // create several plots
                for (int i = 0; i < 4; i++)
                {
                    fig.Subplot(i + 1);
                    fig.plot.axes.labelTitle.text = (cbTitle.Checked) ? "title" : null;
                    fig.plot.axes.labelY.text = (cbYLabel.Checked) ? "vertical units" : null;
                    fig.plot.axes.labelY2.text = (cbY2Label.Checked) ? "more vertical units" : null;
                    fig.plot.axes.labelX.text = (cbXLabel.Checked) ? "horizontal units" : null;
                    fig.plot.axes.enableX = cbXScale.Checked;
                    fig.plot.axes.enableY = cbYScale.Checked;
                    fig.plot.axes.enableY2 = cbY2Scale.Checked;
                    fig.plot.advancedSettings.showLayout = cbShowLayout.Checked;
                }

                // tweak subplot positions
                fig.Subplot(1, 1, 1);
                fig.Subplot(2, 1, 2);
                fig.Subplot(3, 1, 3);
                fig.Subplot(4, 2, 1, 3, 1); // extra-wide

                pictureBox1.Image = fig.Render((Bitmap)pictureBox1.Image);
                lblStatus.Text = bench.GetMessage();
                timeMsec = bench.elapsedMilliseconds;
            }

            return timeMsec;
        }

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = null; // force creation of a new bitmap later
            GeneratePlot();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            double[] times = new double[1_000];

            toolStripProgressBar1.Visible = true;
            lblStatus.Visible = false;
            toolStripProgressBar1.Value = 0;
            Application.DoEvents();
            for (int i = 0; i < times.Length; i++)
            {
                times[i] = GeneratePlot();
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
            GeneratePlot();
        }

        private void CbXLabel_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePlot();
        }

        private void CbYLabel_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePlot();
        }

        private void CbY2Label_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePlot();
        }

        private void CbYScale_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePlot();
        }

        private void CbXScale_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePlot();
        }

        private void CbY2Scale_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePlot();
        }

        private void CbShowLayout_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePlot();
        }
    }
}
