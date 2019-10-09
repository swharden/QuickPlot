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

        private void CanvasPlot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                scaledWidth = (int)(canvasPlot.ActualWidth * gfx.DpiX / 96);
                scaledHeight = (int)(canvasPlot.ActualHeight * gfx.DpiY / 96);
            }

            Render(skipIfCurrentlyRendering: false);
        }

        private bool IsDesignerMode { get { return (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime); } }

        private bool currentlyRendering = false;
        public void Render(bool skipIfCurrentlyRendering = false, bool lowQuality = false)
        {
            if (!(skipIfCurrentlyRendering && currentlyRendering))
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
    }
}
