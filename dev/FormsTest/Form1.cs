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

        private void GeneratePlot()
        {
            if (pictureBox1.Image == null)
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            using (var bench = new QuickPlot.Tools.Benchmark())
            {
                var fig = new QuickPlot.Figure();
                fig.plot.labels.top = "plot one";

                fig.SubPlot(1);
                fig.plot.labels.top = "plot two";

                pictureBox1.Image = fig.Render((Bitmap)pictureBox1.Image);
                Text = bench.GetMessage();
            }
        }

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = null; // force creation of a new bitmap later
            GeneratePlot();
        }
    }
}
