using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickPlot.WinForms
{
    public partial class InteractivePlot: UserControl
    {
        public readonly QuickPlot.Figure figure = new QuickPlot.Figure();

        public InteractivePlot()
        {
            InitializeComponent();

            double[] xs = QuickPlot.Generate.Consecutative(20, 1.0 / 20);
            figure.plot.Scatter(xs, QuickPlot.Generate.Sin(xs.Length));
            figure.plot.Scatter(xs, QuickPlot.Generate.Cos(xs.Length));

            Render();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Render();
        }

        public void Render()
        {
            pictureBox1.Image = figure.GetBitmap(pictureBox1.Width, pictureBox1.Height);
        }
    }
}
