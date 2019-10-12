using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class InteractivePlot : UserControl
    {
        readonly Figure figure = new Figure();

        public InteractivePlot()
        {
            InitializeComponent();
            lblVersion1.Content = $"QuickPlot {typeof(Figure).Assembly.GetName().Version}";
            lblVersion2.Content = $"QuickPlot.WPF {typeof(InteractivePlot).Assembly.GetName().Version}";
        }

        public WriteableBitmap CreateImage(int width, int height)
        {
            return new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, BitmapPalettes.Halftone256Transparent);
        }

        private void OnPaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            var scale = (float)PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;
            figure.Render(e.Surface.Canvas, (int)(e.Info.Width / scale), (int)(e.Info.Height / scale));
        }
    }
}