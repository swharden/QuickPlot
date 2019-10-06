using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace QuickPlot.WinForms
{
    public partial class InteractivePlot : UserControl
    {
        public readonly QuickPlot.Figure figure = new QuickPlot.Figure();

        public InteractivePlot()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += pictureBox1_MouseWheel;
            figure.colors.background = SystemColors.Control;
            Render();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Render();
        }

        bool currentlyRendering = false;
        public void Render(bool interactive = false)
        {
            if (pictureBox1.Width < 1 || pictureBox1.Height < 1)
                return;

            if (interactive && currentlyRendering) // allow skipping frames
                return;

            if (Process.GetCurrentProcess().ProcessName == "devenv")
            {
                pictureBox1.Image = QuickPlot.Tools.DesignerModeBitmap(pictureBox1.Size);
            }
            else
            {
                currentlyRendering = true;
                pictureBox1.Image = figure.GetBitmap(pictureBox1.Width, pictureBox1.Height);
                if (interactive)
                    Application.DoEvents();
                currentlyRendering = false;
            }
        }

        #region mouse interaction

        QuickPlot.Plot plotEngagedWithMouse = null;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            plotEngagedWithMouse = figure.PlotUnderMouse(pictureBox1.Size, e.Location);
            if (plotEngagedWithMouse != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    pictureBox1.Cursor = Cursors.SizeAll;
                    plotEngagedWithMouse.MouseDown(e.Location, left: true);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    pictureBox1.Cursor = Cursors.NoMove2D;
                    plotEngagedWithMouse.MouseDown(e.Location, right: true);
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    pictureBox1.Cursor = Cursors.Cross;
                    plotEngagedWithMouse.MouseDown(e.Location, middle: true);
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (plotEngagedWithMouse != null)
            {
                plotEngagedWithMouse.MouseUp(upLocation: e.Location);
                if (e.Button == MouseButtons.Middle)
                {
                    plotEngagedWithMouse.AutoAxis();
                    Render();
                }
                plotEngagedWithMouse = null;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (plotEngagedWithMouse != null)
            {
                plotEngagedWithMouse.MouseMove(currentLocation: e.Location);
                Render(interactive: true);
            }
            else
            {
                var plotUnderMouse = figure.PlotUnderMouse(pictureBox1.Size, e.Location);
                pictureBox1.Cursor = (plotUnderMouse == null) ? Cursors.Arrow : Cursors.Cross;
            }
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            double zoom = (e.Delta > 0) ? 1.15 : 0.85;
            figure.PlotUnderMouse(pictureBox1.Size, e.Location)?.axes.Zoom(zoom, zoom);
            Render();
        }

        #endregion
    }
}
