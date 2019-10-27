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
    public partial class FormMultiY : Form
    {
        public FormMultiY()
        {
            InitializeComponent();
        }

        private void FormMultiY_Load(object sender, EventArgs e)
        {
            int pointCount = 100;
            double[] dataX = QuickPlot.Generate.Consecutative(pointCount);
            double[] data1 = QuickPlot.Generate.Sin(pointCount, mult: 0.01);
            double[] data2 = QuickPlot.Generate.Cos(pointCount, mult: 100);

            interactivePlot1.figure.plot.Scatter(dataX, data1, color: SkiaSharp.SKColors.Blue);
            interactivePlot1.figure.plot.YLabel("Primary Vertical Axis", color: SkiaSharp.SKColors.Blue);

            interactivePlot1.figure.plot.Scatter(dataX, data2, secondY: true, color: SkiaSharp.SKColors.Red);
            interactivePlot1.figure.plot.YLabel("Secondary Vertical Axis", secondY: true, color: SkiaSharp.SKColors.Red);
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            interactivePlot1.figure.plot.axes2.y.Zoom(1.5);
            interactivePlot1.Render();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            interactivePlot1.figure.plot.axes2.y.Zoom(0.5);
            interactivePlot1.Render();
        }

        private void btnAuto1_Click(object sender, EventArgs e)
        {
            interactivePlot1.figure.plot.AutoAxis(primaryY: true, secondaryY: false);
            interactivePlot1.Render();
        }

        private void btnAuto2_Click(object sender, EventArgs e)
        {
            interactivePlot1.figure.plot.AutoAxis(primaryY: false, secondaryY: true);
            interactivePlot1.Render();
        }
    }
}
