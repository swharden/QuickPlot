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
        #region setup and rendering

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

        public void Render()
        {
            skiaElement.InvalidateVisual();
        }

        #endregion

        #region mouse interaction

        Plot plotEngagedWithMouse = null;
        readonly PlotSettings.MouseTracker mouse = new PlotSettings.MouseTracker();

        private void LockTickDensity(bool locked)
        {
            foreach (Plot subplot in figure.subplots)
            {
                subplot.xTicks.lockTickDensity = locked;
                subplot.yTicks.lockTickDensity = locked;
            }
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(this);
            SKPoint mousePoint = new SKPoint((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            plotEngagedWithMouse = figure.GetSubplotAtPoint(figureSize, mousePoint);

            if (plotEngagedWithMouse != null)
            {
                mouse.mouseDownLimits = new PlotSettings.AxisLimits(plotEngagedWithMouse.axes);

                if (e.ChangedButton == MouseButton.Left)
                {
                    LockTickDensity(true);
                    Cursor = Cursors.SizeAll;
                    mouse.leftDown = mousePoint;
                }
                else if (e.ChangedButton == MouseButton.Right)
                {
                    Cursor = Cursors.SizeAll;
                    mouse.rightDown = mousePoint;
                }
                else if (e.ChangedButton == MouseButton.Middle)
                {
                    Cursor = Cursors.Cross;
                    mouse.middleDown = mousePoint;
                }
            }

            CaptureMouse();
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);
            SKPoint mousePoint = new SKPoint((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            if (plotEngagedWithMouse != null)
            {
                mouse.now = mousePoint;
                plotEngagedWithMouse.axes.Set(mouse.mouseDownLimits);

                if (mouse.leftButtonIsDown)
                    plotEngagedWithMouse.axes.PanPixels(mouse.leftDelta.X, mouse.leftDelta.Y);

                if (mouse.rightButtonIsDown)
                    plotEngagedWithMouse.axes.ZoomPixels(mouse.rightDelta.X, mouse.rightDelta.Y);

                Render();
            }
            else
            {
                var plotUnderMouse = figure.GetSubplotAtPoint(figureSize, mousePoint);
                Cursor = (plotUnderMouse == null) ? Cursors.Arrow : Cursors.Hand;
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(this);
            SKPoint location = new SKPoint((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            if (plotEngagedWithMouse != null)
            {
                mouse.leftDown = new SKPoint(0, 0);
                mouse.rightDown = new SKPoint(0, 0);
                mouse.middleDown = new SKPoint(0, 0);
                LockTickDensity(false);

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
            SKPoint location = new SKPoint((int)(position.X * scaleFactor), (int)(position.Y * scaleFactor));

            double zoom = (e.Delta > 0) ? 1.15 : 0.85;
            figure.GetSubplotAtPoint(figureSize, location)?.axes.Zoom(zoom, zoom);
            Render();
        }

        #endregion
    }
}