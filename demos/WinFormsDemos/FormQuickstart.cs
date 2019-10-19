using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsDemos
{
    public partial class FormQuickstart : Form
    {
        public FormQuickstart()
        {
            InitializeComponent();
        }

        private void FormQuickstart_Load(object sender, EventArgs e)
        {
            int pointCount = 100;
            double[] dataX = QuickPlot.Generate.Consecutative(pointCount);

            //double[] data1 = QuickPlot.Generate.RandomWalk(pointCount, seed: 1);
            //double[] data2 = QuickPlot.Generate.RandomWalk(pointCount, seed: 2);

            double[] data1 = QuickPlot.Generate.Sin(pointCount);
            double[] data2 = QuickPlot.Generate.Cos(pointCount);

            interactivePlot1.figure.plot.Scatter(dataX, data1);
            interactivePlot1.figure.plot.Scatter(dataX, data2);
        }
    }
}
