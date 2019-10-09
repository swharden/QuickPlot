using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickPlot.WPF
{
    /// <summary>
    /// Interaction logic for InteractivePlot.xaml
    /// </summary>
    public partial class InteractivePlot : UserControl
    {
        public readonly QuickPlot.Figure figure = new Figure();

        public InteractivePlot()
        {
            InitializeComponent();
        }

        private int scaledWidth;
        private int scaledHeight;
        private float scaleFactor;

        private System.Drawing.Size scaledSize { get { return new System.Drawing.Size(scaledWidth, scaledHeight); } }

        private void CanvasPlot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                scaledWidth = (int)(canvasPlot.ActualWidth * gfx.DpiX / 96);
                scaledHeight = (int)(canvasPlot.ActualHeight * gfx.DpiY / 96);
                scaleFactor = gfx.DpiX / 96;
            }

            Render();
        }

        private bool IsDesignerMode { get { return (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime); } }

        private bool currentlyRendering = false;
        public void Render(bool interactive = false)
        {
            if (!(interactive && currentlyRendering))
            {
                currentlyRendering = true;
                Bitmap bmp = figure.GetBitmap(scaledWidth, scaledHeight);
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                ((System.Drawing.Bitmap)bmp).Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                bmpImage.StreamSource = stream;
                bmpImage.EndInit();
                imagePlot.Source = bmpImage;
                currentlyRendering = false;
            }
        }

        #region mouse interaction

        QuickPlot.Plot plotEngagedWithMouse = null;

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(this);
            System.Drawing.Point location = new System.Drawing.Point((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            plotEngagedWithMouse = figure.PlotUnderMouse(scaledSize, location);
            if (plotEngagedWithMouse != null)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    Cursor = Cursors.SizeAll;
                    plotEngagedWithMouse.MouseDown(location, left: true);
                }
                else if (e.ChangedButton == MouseButton.Right)
                {
                    Cursor = Cursors.SizeAll;
                    plotEngagedWithMouse.MouseDown(location, right: true);
                }
                else if (e.ChangedButton == MouseButton.Middle)
                {
                    Cursor = Cursors.Cross;
                    plotEngagedWithMouse.MouseDown(location, middle: true);
                }
            }

            CaptureMouse();
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);
            System.Drawing.Point location = new System.Drawing.Point((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            if (plotEngagedWithMouse != null)
            {
                plotEngagedWithMouse.MouseMove(currentLocation: location);
                Render(interactive: true);
            }
            else
            {
                var plotUnderMouse = figure.PlotUnderMouse(scaledSize, location);
                Cursor = (plotUnderMouse == null) ? Cursors.Arrow : Cursors.Hand;
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(this);
            System.Drawing.Point location = new System.Drawing.Point((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            if (plotEngagedWithMouse != null)
            {
                plotEngagedWithMouse.MouseUp(upLocation: location);
                if (e.ChangedButton == MouseButton.Middle)
                {
                    plotEngagedWithMouse.AutoAxis();
                    Render();
                }
                plotEngagedWithMouse = null;
            }
            ReleaseMouseCapture();
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var position = e.GetPosition(this);
            System.Drawing.Point location = new System.Drawing.Point((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            double zoom = (e.Delta > 0) ? 1.15 : 0.85;
            figure.PlotUnderMouse(scaledSize, location)?.axes.Zoom(zoom, zoom);
            Render();
        }

        #endregion
    }
}
