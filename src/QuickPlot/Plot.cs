using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QuickPlot
{
    public class Plot
    {
        public readonly PlotSettings.Axes axes = new PlotSettings.Axes();
        public readonly PlotSettings.Axes axes2 = new PlotSettings.Axes();

        public readonly PlotSettings.Layout layout = new PlotSettings.Layout();

        public PlotSettings.SubplotPosition subplotPosition = new PlotSettings.SubplotPosition(1, 1, 1);
        
        public PlotSettings.Label title, yLabel, xLabel, y2Label;
        public PlotSettings.TickCollection yTicks, xTicks, y2Ticks;

        private List<Plottables.Plottable> plottables = new List<Plottables.Plottable>();

        public Plot()
        {
            title = new PlotSettings.Label { text = "Title", fontSize = 16, weight = SKFontStyleWeight.Bold };
            yLabel = new PlotSettings.Label { text = "Vertical Label", fontSize = 16, weight = SKFontStyleWeight.SemiBold };
            xLabel = new PlotSettings.Label { text = "Horzontal Label", fontSize = 16, weight = SKFontStyleWeight.SemiBold };
            y2Label = new PlotSettings.Label { text = null, fontSize = 16, weight = SKFontStyleWeight.SemiBold };

            yTicks = new PlotSettings.TickCollection(PlotSettings.Side.left);
            xTicks = new PlotSettings.TickCollection(PlotSettings.Side.bottom);
            y2Ticks = new PlotSettings.TickCollection(PlotSettings.Side.right);
        }

        #region add or remove plottables

        public void Scatter(double[] xs, double[] ys, Style style = null)
        {
            if (style is null)
                style = new Style(colorIndex: plottables.Count);
            var scatterPlot = new Plottables.Scatter(xs, ys, style);
            plottables.Add(scatterPlot);
        }

        public void Clear()
        {
            plottables.Clear();
        }

        #endregion

        #region axis management

        private List<Plottables.Plottable> GetPlottableList(bool secondaryAxis = false)
        {
            List<Plottables.Plottable> plist = new List<Plottables.Plottable>();
            foreach (Plottables.Plottable plottable in plottables)
                if (plottable.style.secondY == secondaryAxis)
                    plist.Add(plottable);
            return plist;
        }

        public void AutoAxis(double marginX = .1, double marginY = .1)
        {
            // auto axis for primary XY
            var primaryPlottables = GetPlottableList(false);
            if (primaryPlottables.Count > 0)
            {
                axes.Set(primaryPlottables[0].GetDataArea());
                for (int i = 1; i < primaryPlottables.Count; i++)
                    axes.Expand(primaryPlottables[i].GetDataArea());
            }
            axes.Zoom(1 - marginX, 1 - marginY);

            // auto axis for secondary XY
            var secondaryPlottables = GetPlottableList(true);
            if (secondaryPlottables.Count > 0)
            {
                axes2.Set(secondaryPlottables[0].GetDataArea());
                for (int i = 1; i < secondaryPlottables.Count; i++)
                    axes2.Expand(secondaryPlottables[i].GetDataArea());
            }
            axes2.Zoom(1 - marginX, 1 - marginY);
            axes2.x = axes.x;
        }

        public void ShareX(Plot source)
        {
            axes.x = (source is null) ? new PlotSettings.Axis(axes.x.low, axes.x.high) : axes.x = source.axes.x;
        }

        public void ShareY(Plot source)
        {
            axes.y = (source is null) ? new PlotSettings.Axis(axes.y.low, axes.y.high) : axes.y = source.axes.y;
        }

        #endregion

        #region rendering

        public void Render(SKCanvas canvas, SKRect plotRect)
        {
            // this is a chicken-and-egg problem
            UpdateLayout(plotRect); // guess dimensions
            UpdateTicks(); // guess tick density
            UpdateLayout(plotRect); // refine layout based on new ticks
            UpdateTicks(); // refine ticks based on new layout

            // now that the layout is optimized, draw everything
            DrawBackground(canvas);
            DrawTicks(canvas);
            DrawPlottables(canvas);
            DrawLabels(canvas);
            DrawFrame(canvas);
        }

        private void UpdateTicks()
        {
            if (!axes.x.isValid || !axes.y.isValid)
                AutoAxis();

            axes.SetDataRect(layout.dataRect);
            axes2.SetDataRect(layout.dataRect);

            yTicks.Generate(axes.y.low, axes.y.high, layout.dataRect);
            y2Ticks.Generate(axes2.y.low, axes2.y.high, layout.dataRect);
            xTicks.Generate(axes.x.low, axes.x.high, layout.dataRect);

            if (yTicks.biggestTickLabelSize.Width > layout.yScaleWidth)
                layout.yScaleWidth = yTicks.biggestTickLabelSize.Width;
        }

        private void UpdateLayout(SKRect plotRect)
        {
            // TODO: currently there is no way to manually define these values. They are always auto-detected.
            float textPadPx = 3;

            layout.titleHeight = (title.text is null) ? 0 : title.fontSize + textPadPx;
            layout.yLabelWidth = (yLabel.text is null) ? 0 : yLabel.fontSize + textPadPx;
            layout.y2LabelWidth = (y2Label.text is null) ? 0 : y2Label.fontSize + textPadPx;
            layout.xLabelHeight = (xLabel.text is null) ? 0 : xLabel.fontSize + textPadPx;

            layout.yScaleWidth = (!axes.y.isValid) ? 0 : yTicks.biggestTickLabelSize.Width + textPadPx;
            layout.y2ScaleWidth = (!axes2.y.isValid) ? 0 : y2Ticks.biggestTickLabelSize.Width + textPadPx;
            layout.xScaleHeight = (!axes.x.isValid) ? 0 : xTicks.biggestTickLabelSize.Height + textPadPx;

            layout.Update(plotRect);
        }

        private void DrawBackground(SKCanvas canvas)
        {
            var fillPaint = new SKPaint();
            fillPaint.Color = SKColor.Parse("#FFFFFF");
            //fillPaint.Color = Tools.RandomColor(); // useful for assessing when plots are redrawn
            canvas.DrawRect(layout.dataRect, fillPaint);
        }

        private void DrawPlottables(SKCanvas canvas)
        {
            canvas.Save();
            canvas.ClipRect(axes.GetDataRect());
            foreach (var primaryPlottable in GetPlottableList(false))
                primaryPlottable.Render(canvas, axes);
            foreach (var secondaryPlottable in GetPlottableList(true))
                secondaryPlottable.Render(canvas, axes2);
            canvas.Restore();
        }

        private void DrawTicks(SKCanvas canvas)
        {
            yTicks.Render(canvas, axes);
            y2Ticks.Render(canvas, axes2);
            xTicks.Render(canvas, axes);
        }

        private void DrawLabels(SKCanvas canvas)
        {
            int pathExtend = 500;

            if (title.text != null)
            {
                SKPath titlePath = new SKPath();
                SKPaint titlePaint = title.MakePaint();
                titlePath.MoveTo(layout.titleRect.Left - pathExtend, layout.titleRect.Top + titlePaint.TextSize);
                titlePath.LineTo(layout.titleRect.Right + pathExtend, layout.titleRect.Top + titlePaint.TextSize);
                canvas.DrawTextOnPath(title.text, titlePath, 0, 0, titlePaint);
            }

            if (yLabel.text != null)
            {
                SKPath yLabelPath = new SKPath();
                SKPaint yLabelPaint = yLabel.MakePaint();
                yLabelPath.MoveTo(layout.yLabelRect.Left + yLabelPaint.TextSize, layout.yLabelRect.Bottom + pathExtend);
                yLabelPath.LineTo(layout.yLabelRect.Left + yLabelPaint.TextSize, layout.yLabelRect.Top - pathExtend);
                canvas.DrawTextOnPath(yLabel.text, yLabelPath, 0, 0, yLabelPaint);
            }

            if (y2Label.text != null)
            {
                SKPath y2LabelPath = new SKPath();
                SKPaint y2LabelPaint = y2Label.MakePaint();
                y2LabelPath.MoveTo(layout.y2LabelRect.Right - y2LabelPaint.TextSize, layout.y2LabelRect.Top - pathExtend);
                y2LabelPath.LineTo(layout.y2LabelRect.Right - y2LabelPaint.TextSize, layout.y2LabelRect.Bottom + pathExtend);
                canvas.DrawTextOnPath(y2Label.text, y2LabelPath, 0, 0, y2LabelPaint);
            }

            if (xLabel.text != null)
            {
                SKPath xLabelPath = new SKPath();
                SKPaint xLabelPaint = xLabel.MakePaint();
                xLabelPath.MoveTo(layout.xLabelRect.Left - pathExtend, layout.xLabelRect.Top + xLabelPaint.TextSize);
                xLabelPath.LineTo(layout.xLabelRect.Right + pathExtend, layout.xLabelRect.Top + xLabelPaint.TextSize);
                canvas.DrawTextOnPath(xLabel.text, xLabelPath, 0, 0, xLabelPaint);
            }
        }

        private void DrawFrame(SKCanvas canvas)
        {
            SKPaint layoutFramePaint = new SKPaint()
            {
                Color = SKColor.Parse("#000000"),
                Style = SKPaintStyle.Stroke,
                IsAntialias = false
            };

            SKRect outline = layout.dataRect;
            outline.Right -= 1;
            outline.Bottom -= 1;
            canvas.DrawRect(outline, layoutFramePaint);
        }

        #endregion
    }
}
