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
        Figure fig = new Figure();

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
            Render();
        }

        public void Render()
        {
            pictureBox1.Image = fig.GetBitmap(pictureBox1.Width, pictureBox1.Height);
        }
    }
}
