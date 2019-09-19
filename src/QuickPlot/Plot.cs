using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot
{
    public class Plot
    {
        public PlotSettings.SubplotPosition subplotPosition;

        public List<Plottables.Plottable> plottables = new List<Plottables.Plottable>();
        public PlotSettings.Axes axes;
        public PlotSettings.MouseTracker mouse = new PlotSettings.MouseTracker();
        PlotSettings.Layout layout = new PlotSettings.Layout();

        private PlotSettings.AxisLabels axisLabels = new PlotSettings.AxisLabels();
        private PlotSettings.AxisScales axisScales = new PlotSettings.AxisScales();

        public Plot()
        {

        }

        public void Render(SKCanvas canvas, SKRect rect)
        {

            if (axes == null)
                AutoAxis();

            layout.Tighten(rect);
            axes.SetRect(layout.data);

            //layout.RenderDebuggingGuides(canvas);

            // update the scale, apply mouse adjustments, then update the scale again
            PlotSettings.Axes axesAfterMouse = new PlotSettings.Axes(axes);
            axesAfterMouse.PanPixels(mouse.leftDownDelta);
            axesAfterMouse.ZoomPixels(mouse.rightDownDelta);
            axesAfterMouse.SetRect(layout.data);

            // draw inside a clipping rectangle
            canvas.Save();
            canvas.ClipRect(layout.data);
            for (int i = 0; i < plottables.Count; i++)
            {
                plottables[i].Render(canvas, axesAfterMouse);
            }
            canvas.Restore();

            axisLabels.Render(layout, canvas);
            axisScales.Render(layout, axesAfterMouse, canvas);
        }

        public void Scatter(double[] xs, double[] ys, Style style = null)
        {
            if (style == null)
                style = new Style(plottables.Count);
            var scatterPlot = new Plottables.Scatter(xs, ys, style);
            plottables.Add(scatterPlot);
        }

        public void AutoAxis(double marginX = .1, double marginY = .1)
        {
            if (axes == null)
                axes = new PlotSettings.Axes();
            if (plottables.Count > 0)
            {
                axes.Set(plottables[0].GetDataArea());
                for (int i = 1; i < plottables.Count; i++)
                    axes.Expand(plottables[i].GetDataArea());
            }

            axes.Zoom(1 - marginX, 1 - marginY);
        }

        public void XLabel(string text)
        {
            axisLabels.bottom.text = text;
        }

        public void YLabel(string text)
        {
            axisLabels.left.text = text;
        }

        public void Y2Label(string text)
        {
            axisLabels.right.text = text;
        }

        public void Title(string text)
        {
            axisLabels.top.text = text;
        }
    }
}
