using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickPlotDemos
{
    public partial class FormInteractive : Form
    {
        public FormInteractive()
        {
            InitializeComponent();
        }

        private void FormInteractive_SizeChanged(object sender, EventArgs e)
        {
            Text = interactivePlot1.figure.RenderBenchmarkMessage;
        }
    }
}
