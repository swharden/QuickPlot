using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickPlot.Demos
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void BtnBenchOneKLines_Click(object sender, EventArgs e)
        {
            using (var frm = new Benchmarks.OneThousandLines())
            {
                frm.ShowDialog();
            }
        }
    }
}
