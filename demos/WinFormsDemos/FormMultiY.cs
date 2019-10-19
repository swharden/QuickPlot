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

            //double[] data1 = QuickPlot.Generate.RandomWalk(pointCount, seed: 1);
            //double[] data2 = QuickPlot.Generate.RandomWalk(pointCount, seed: 2);

            double[] data1 = QuickPlot.Generate.Sin(pointCount, mult: 0.01);
            double[] data2 = QuickPlot.Generate.Cos(pointCount, mult: 100);

            var style1 = new QuickPlot.Style();
            style1.paint.Color = SkiaSharp.SKColors.Blue;
            interactivePlot1.figure.plot.Scatter(dataX, data1, style1);
            interactivePlot1.figure.plot.yLabel.text = "Primary Vertical Axis";
            interactivePlot1.figure.plot.yLabel.fontColor = SkiaSharp.SKColors.Blue;

            var style2 = new QuickPlot.Style();
            style2.paint.Color = SkiaSharp.SKColors.Red;
            style2.secondY = true;
            interactivePlot1.figure.plot.Scatter(dataX, data2, style2);
            interactivePlot1.figure.plot.y2Label.text = "Secondary Vertical Axis";
            interactivePlot1.figure.plot.y2Label.fontColor = SkiaSharp.SKColors.Red;
        }
    }
}
