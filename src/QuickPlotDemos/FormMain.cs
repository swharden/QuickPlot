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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnSaveFig_Click(object sender, EventArgs e)
        {
            QuickPlot.Figure fig = new QuickPlot.Figure();
            fig.Save(600, 400, "test.png");
        }

        private void btnInteractive_Click(object sender, EventArgs e)
        {
            using (var frm = new FormInteractive())
                frm.ShowDialog();
        }
    }
}
