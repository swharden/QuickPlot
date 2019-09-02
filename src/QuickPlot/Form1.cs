using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickPlot
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Render();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Image = null; // force re-lookup
        }

        public void Render()
        {
            if (pictureBox1.Image == null)
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            using (var bench = new Benchmark())
            {
                if (cbMultiple.Checked)
                    pictureBox1.Image = TestRender.PlotMultiple(pictureBox1.Image);
                else
                    pictureBox1.Image = TestRender.PlotSingle(pictureBox1.Image);

                Text = bench.GetMessage();
                Application.DoEvents();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Render();
        }
    }
}
