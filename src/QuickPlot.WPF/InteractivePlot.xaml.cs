using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace QuickPlot.WPF
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class InteractivePlot : UserControl
    {
        public readonly Figure figure = new Figure();

        public InteractivePlot()
        {
            InitializeComponent();
            lblVersion1.Content = $"QuickPlot {typeof(Figure).Assembly.GetName().Version}";
            lblVersion2.Content = $"QuickPlot.WPF {typeof(InteractivePlot).Assembly.GetName().Version}";
        }

        SKSize figureSize;
        float scaleFactor;

        private void OnPaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            scaleFactor = (float)PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;
            figureSize = new SKSize(e.Info.Width / scaleFactor, e.Info.Height / scaleFactor);
            figure.Render(e.Surface.Canvas, figureSize, plotEngagedWithMouse);
        }

        #region mouse interaction

        Plot plotEngagedWithMouse = null;

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(this);
            SKPoint location = new SKPoint((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            plotEngagedWithMouse = figure.PlotUnderMouse(figureSize, location);
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
            SKPoint location = new SKPoint((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            if (plotEngagedWithMouse != null)
            {
                plotEngagedWithMouse.MouseMove(currentLocation: location);
                skiaElement.InvalidateVisual();
            }
            else
            {
                var plotUnderMouse = figure.PlotUnderMouse(figureSize, location);
                Cursor = (plotUnderMouse == null) ? Cursors.Arrow : Cursors.Hand;
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(this);
            SKPoint location = new SKPoint((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            if (plotEngagedWithMouse != null)
            {
                plotEngagedWithMouse.MouseUp(upLocation: location);
                if (e.ChangedButton == MouseButton.Middle)
                {
                    plotEngagedWithMouse.AutoAxis();
                    skiaElement.InvalidateVisual();
                }
                plotEngagedWithMouse = null;
            }
            ReleaseMouseCapture();
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var position = e.GetPosition(this);
            SKPoint location = new SKPoint((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            double zoom = (e.Delta > 0) ? 1.15 : 0.85;
            figure.PlotUnderMouse(figureSize, location)?.axes.Zoom(zoom, zoom);
            skiaElement.InvalidateVisual();
        }

        #endregion
    }
}