using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QuickPlot
{
    public class Plot
    {
        public PlotSettings.SubplotPosition subplotPosition = new PlotSettings.SubplotPosition(1, 1, 1);
        private List<Plottables.Plottable> plottables = new List<Plottables.Plottable>();
        public readonly PlotSettings.Layout layout = new PlotSettings.Layout();
        public readonly PlotSettings.Axes axes = new PlotSettings.Axes();
        public readonly PlotSettings.Axes axes2 = new PlotSettings.Axes();
        public PlotSettings.Label title, yLabel, xLabel, y2Label;
        public PlotSettings.TickCollection yTicks, xTicks, y2Ticks;

        public Plot()
        {
            title = new PlotSettings.Label { text = "Title", fontSize = 16, weight = SKFontStyleWeight.Bold };
            yLabel = new PlotSettings.Label { text = "Vertical Label", fontSize = 16, weight = SKFontStyleWeight.SemiBold };
            xLabel = new PlotSettings.Label { text = "Horzontal Label", fontSize = 16, weight = SKFontStyleWeight.SemiBold };
            y2Label = new PlotSettings.Label { text = "Vertical Label Too", fontSize = 16, weight = SKFontStyleWeight.SemiBold };

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
            // update plot-level layout with the latest plot dimensions
            layout.Update(plotRect);

            if (!axes.x.isValid || !axes.y.isValid)
                AutoAxis();

            axes.SetDataRect(layout.dataRect);
            axes2.SetDataRect(layout.dataRect);

            // draw the graphics
            yTicks.Generate(axes.y.low, axes.y.high, layout.dataRect);
            y2Ticks.Generate(axes2.y.low, axes2.y.high, layout.dataRect);
            xTicks.Generate(axes.x.low, axes.x.high, layout.dataRect);
            if (yTicks.biggestTickLabelSize.Width > layout.yScaleWidth)
            {
                Debug.WriteLine("increasing Y scale width to prevent overlapping with label Y label");
                layout.yScaleWidth = yTicks.biggestTickLabelSize.Width;
                layout.Update(plotRect);
            }

            var fillPaint = new SKPaint();
            fillPaint.Color = SKColor.Parse("#FFFFFF");
            //fillPaint.Color = Tools.RandomColor(); // useful for assessing when plots are redrawn
            canvas.DrawRect(layout.dataRect, fillPaint);

            yTicks.Render(canvas, axes);
            y2Ticks.Render(canvas, axes2);
            xTicks.Render(canvas, axes);

            canvas.Save();
            canvas.ClipRect(axes.GetDataRect());

            foreach (var primaryPlottable in GetPlottableList(false))
                primaryPlottable.Render(canvas, axes);
            foreach (var secondaryPlottable in GetPlottableList(true))
                secondaryPlottable.Render(canvas, axes2);

            canvas.Restore();

            //RenderLayoutDebug(canvas);
            RenderLabels(canvas);
        }

        private void RenderLayoutDebug(SKCanvas canvas)
        {
            SKPaint paintTitle = new SKPaint()
            {
                Color = SKColor.Parse("#55000000"),
                IsAntialias = true
            };

            SKPaint paintLabel = new SKPaint()
            {
                Color = SKColor.Parse("#550000FF"),
                IsAntialias = true
            };

            SKPaint paintScale = new SKPaint()
            {
                Color = SKColor.Parse("#5500FF00"),
                IsAntialias = true
            };

            SKPaint paintData = new SKPaint()
            {
                Color = SKColor.Parse("#55FF0000"),
                IsAntialias = true
            };

            canvas.DrawRect(layout.titleRect, paintTitle);

            canvas.DrawRect(layout.yLabelRect, paintLabel);
            canvas.DrawRect(layout.y2LabelRect, paintLabel);
            canvas.DrawRect(layout.xLabelRect, paintLabel);

            canvas.DrawRect(layout.yScaleRect, paintScale);
            canvas.DrawRect(layout.y2ScaleRect, paintScale);
            canvas.DrawRect(layout.xScaleRect, paintScale);

            canvas.DrawRect(layout.dataRect, paintData);
        }

        private void RenderLabels(SKCanvas canvas)
        {
            int pathExtend = 500;

            SKPath titlePath = new SKPath();
            SKPaint titlePaint = title.MakePaint();
            titlePath.MoveTo(layout.titleRect.Left - pathExtend, layout.titleRect.Top + titlePaint.TextSize);
            titlePath.LineTo(layout.titleRect.Right + pathExtend, layout.titleRect.Top + titlePaint.TextSize);
            canvas.DrawTextOnPath(title.text, titlePath, 0, 0, titlePaint);

            SKPath yLabelPath = new SKPath();
            SKPaint yLabelPaint = yLabel.MakePaint();
            yLabelPath.MoveTo(layout.yLabelRect.Left + yLabelPaint.TextSize, layout.yLabelRect.Bottom + pathExtend);
            yLabelPath.LineTo(layout.yLabelRect.Left + yLabelPaint.TextSize, layout.yLabelRect.Top - pathExtend);
            canvas.DrawTextOnPath(yLabel.text, yLabelPath, 0, 0, yLabelPaint);

            SKPath y2LabelPath = new SKPath();
            SKPaint y2LabelPaint = y2Label.MakePaint();
            y2LabelPath.MoveTo(layout.y2LabelRect.Right - y2LabelPaint.TextSize, layout.y2LabelRect.Top - pathExtend);
            y2LabelPath.LineTo(layout.y2LabelRect.Right - y2LabelPaint.TextSize, layout.y2LabelRect.Bottom + pathExtend);
            canvas.DrawTextOnPath(y2Label.text, y2LabelPath, 0, 0, y2LabelPaint);

            SKPath xLabelPath = new SKPath();
            SKPaint xLabelPaint = xLabel.MakePaint();
            xLabelPath.MoveTo(layout.xLabelRect.Left - pathExtend, layout.xLabelRect.Top + xLabelPaint.TextSize);
            xLabelPath.LineTo(layout.xLabelRect.Right + pathExtend, layout.xLabelRect.Top + xLabelPaint.TextSize);
            canvas.DrawTextOnPath(xLabel.text, xLabelPath, 0, 0, xLabelPaint);


            // Outline the data area

            SKPaint layoutFramePaint = new SKPaint()
            {
                Color = SKColor.Parse("#000000"),
                Style = SKPaintStyle.Stroke,
                IsAntialias = false
            };
            canvas.DrawRect(layout.dataRect, layoutFramePaint);
        }

        #endregion
    }
}
