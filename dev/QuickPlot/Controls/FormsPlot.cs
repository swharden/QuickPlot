using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickPlot.Controls
{
    public partial class FormsPlot : UserControl
    {
        public Figure fig = new Figure();
        public bool IsVisualStudioDesigner { get { return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv"); } }

        public FormsPlot()
        {
            InitializeComponent();
            Render();
        }

        private void FormsPlot_Resize(object sender, EventArgs e)
        {
            Render(true);
        }

        public void Render(bool updateSize = false)
        {
            if (pictureBox1.Image == null)
                updateSize = true;

            if (updateSize)
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            try
            {
                pictureBox1.Image = fig.Render((Bitmap)pictureBox1.Image, SystemColors.Control);
            }
            catch
            {
                Console.WriteLine("Crashed generating image...");
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var plotUnderMouse = MouseTracker.PlotUnderMouse(fig, e.Location);

            if (plotUnderMouse == null)
            {
                pictureBox1.Cursor = Cursors.Arrow;
            }
            else
            {
                pictureBox1.Cursor = Cursors.Cross;
            }

            if ((mouseDownPoint.X >= 0) && (mouseDownBmp != null) && (plotUnderMouse == mouseDownPlot))
            {
                int xMax = Math.Max(mouseDownPoint.X, e.Location.X);
                int xMin = Math.Min(mouseDownPoint.X, e.Location.X);
                int yMax = Math.Max(mouseDownPoint.Y, e.Location.Y);
                int yMin = Math.Min(mouseDownPoint.Y, e.Location.Y);

                Point selTopLeft = new Point(xMin, yMin);
                Size selSize = new Size(xMax - xMin, yMax - yMin);
                Rectangle selRect = new Rectangle(selTopLeft, selSize);

                Color selColor = Color.FromArgb(100, 255, 0, 0);
                Brush selBrush = new SolidBrush(selColor);
                Pen selPen = new Pen(selColor);

                Bitmap selBmp = new Bitmap(mouseDownBmp);
                using (Graphics gfx = Graphics.FromImage(selBmp))
                {
                    gfx.FillRectangle(selBrush, selRect);
                    gfx.DrawRectangle(selPen, selRect.X, selRect.Y, selRect.Width - 1, selRect.Height - 1);
                }
                pictureBox1.Image = selBmp;
            }
        }

        private Point mouseDownPoint;
        Bitmap mouseDownBmp;
        Plot mouseDownPlot;

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            mouseDownPlot = MouseTracker.PlotUnderMouse(fig, e.Location);
            if (mouseDownPlot == null)
                return;

            mouseDownPoint = e.Location;
            mouseDownBmp = new Bitmap(pictureBox1.Image);
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseDownPlot == MouseTracker.PlotUnderMouse(fig, e.Location))
            {
                if (e.Button == MouseButtons.Left)
                {
                    MouseTracker.ZoomToPixel(mouseDownPlot, mouseDownPoint, e.Location);
                }

                if (e.Button == MouseButtons.Middle)
                {
                    mouseDownPlot.axes.Set(-10, 10, -10, 10);
                }
            }

            mouseDownPoint = new Point(-1, -1);
            mouseDownBmp = null;
            mouseDownPlot = null;
            Render();
        }
    }
}
