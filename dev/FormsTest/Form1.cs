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

                fig.Subplot(0);
                fig.plot.labels.top = "plot one";
                fig.plot.labels.left = "vertical units";
                fig.plot.labels.bottom = "horizontal units";

                fig.Subplot(1);
                fig.plot.labels.top = "plot two";
                fig.plot.labels.left = "vertical units";
                fig.plot.labels.bottom = "horizontal units";

                fig.Subplot(2);
                fig.plot.labels.top = "plot three";
                fig.plot.labels.left = "vertical units";
                fig.plot.labels.bottom = "horizontal units";

                fig.Subplot(3);
                fig.plot.labels.top = "plot four";
                fig.plot.labels.left = "vertical units";
                fig.plot.labels.bottom = "horizontal units";

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
    }
}
