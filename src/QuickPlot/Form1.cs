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
            Render();
        }

        public void Render()
        {
            var bench = new Benchmark();
            if (cbMultiple.Checked)
                pictureBox1.Image = PlotMultiple(pictureBox1.Width, pictureBox1.Height);
            else
                pictureBox1.Image = PlotSingle(pictureBox1.Width, pictureBox1.Height);
            this.Text = bench.GetMessage();
        }

        public Bitmap PlotSingle(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics gfx = Graphics.FromImage(bmp);
            var dim = new Dimensions(bmp.Width, bmp.Height);
            dim.ShrinkAll(5);

            return bmp;
        }

        public Bitmap PlotMultiple(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics gfx = Graphics.FromImage(bmp);

            var dim = new Dimensions(bmp.Width, bmp.Height);
            dim.ShrinkAll(5);

            Bitmap title = GDI.Outline(GDI.TextImage("Awesome Plot"));
            PointF titleLocation = new PointF(dim.left + (dim.Width - title.Width) / 2, dim.top);
            gfx.DrawImage(title, titleLocation);
            dim.Shrink(top: title.Height);

            Bitmap yLabel = GDI.Outline(GDI.RotateLeft(GDI.TextImage("Vertical Units")));
            gfx.DrawImage(yLabel, dim.left, dim.top + (dim.Height - yLabel.Height) / 2);
            dim.Shrink (left: yLabel.Width);

            Bitmap xLabel = GDI.Outline(GDI.TextImage("Horizontal Units"));
            PointF xLabelLocation = new PointF(dim.left + (dim.Width - xLabel.Width) / 2, dim.bot - xLabel.Height);
            gfx.DrawImage(xLabel, xLabelLocation);
            dim.Shrink(bot: xLabel.Height);

            Bitmap xTicks = GDI.Outline(GDI.Scalebar(dim.Width, 30));
            PointF xTicksLocation = new PointF(dim.left, dim.bot - xTicks.Height);
            gfx.DrawImage(xTicks, xTicksLocation);
            Bitmap xTickMult = GDI.Outline(GDI.TextImage("xMult", 10, false));
            PointF xTicksMultLocation = new PointF(dim.right - xTickMult.Width, dim.bot);
            gfx.DrawImage(xTickMult, xTicksMultLocation);
            dim.Shrink(bot: xTicks.Height);

            Bitmap yTicks = GDI.Outline(GDI.Scalebar(50, dim.Height));
            PointF yTicksLocation = new PointF(dim.left, dim.top);
            gfx.DrawImage(yTicks, yTicksLocation);
            dim.Shrink(left: yTicks.Width);
            Bitmap yTickMult = GDI.Outline(GDI.TextImage("yMult", 10, false));
            PointF yTicksMultLocation = new PointF(dim.left, dim.top - yTickMult.Height);
            gfx.DrawImage(yTickMult, yTicksMultLocation);

            var remainingRect = new Rectangle(dim.left, dim.top, dim.Width - 1, dim.Height - 1);
            string message = $"data area: {dim.Width} x {dim.Height}";
            Bitmap dataPlot = GDI.Outline(GDI.Message(dim.Width, dim.Height, message));
            PointF dataPlotLocation = new PointF(dim.left, dim.top);
            gfx.DrawImage(dataPlot, dataPlotLocation);

            return bmp;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Render();
        }
    }
}
