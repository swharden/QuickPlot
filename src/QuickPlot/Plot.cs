using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace QuickPlot
{
    /* Plots never store plot size! 
     * Pixel dimensions are only passed-in when Render() is called.
     */

    /// <summary>
    /// The Plot object holds details about a plot (data, axes, styling, etc.)
    /// </summary>
    public class Plot
    {
        // keep the plottable list private.
        private List<Plottables.Plottable> plottables = new List<Plottables.Plottable>();

        // configuration objects are okay to be public
        public PlotSettings.SubplotPosition subplotPosition = new PlotSettings.SubplotPosition(1, 1, 1);
        public PlotSettings.Colors colors = new PlotSettings.Colors();
        public readonly PlotSettings.Layout layout = new PlotSettings.Layout();

        // make label and tick settings public so they can be customized
        public PlotSettings.Label title, yLabel, xLabel, y2Label;
        public PlotSettings.TickCollection yTicks, xTicks, y2Ticks;

        // axes can be public, and null means it hasnt been set up
        public PlotSettings.Axes axes;

        // keep private?
        private PlotSettings.MouseTracker mouse = new PlotSettings.MouseTracker();

        /// <summary>
        /// Create a new Plot
        /// </summary>
        public Plot()
        {
            title = new PlotSettings.Label { text = "Title", fontSize = 16, bold = true };
            yLabel = new PlotSettings.Label { text = "Vertical Label" };
            xLabel = new PlotSettings.Label { text = "Horzontal Label" };
            y2Label = new PlotSettings.Label { text = "" };

            yTicks = new PlotSettings.TickCollection(PlotSettings.Side.left);
            xTicks = new PlotSettings.TickCollection(PlotSettings.Side.bottom);
            y2Ticks = new PlotSettings.TickCollection(PlotSettings.Side.right);
        }

        /// <summary>
        /// Create a scatter plot from X and Y arrays of identical length
        /// </summary>
        public void Scatter(double[] xs, double[] ys, Style style = null)
        {
            if (style == null)
                style = new Style(plottables.Count);
            var scatterPlot = new Plottables.Scatter(xs, ys, style);
            plottables.Add(scatterPlot);
        }

        /// <summary>
        /// Automatically set axis limits to fit the data
        /// </summary>
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

        /// <summary>
        /// Return a new Axes after mouse panning and zooming
        /// </summary>
        private PlotSettings.Axes AxesAfterMouse(RectangleF? renderArea = null)
        {
            if (renderArea is null)
                renderArea = mouse.lastRenderArea;
            else
                mouse.lastRenderArea = (RectangleF)renderArea;

            var axesAfterMouse = new PlotSettings.Axes(axes);
            if (mouse.leftButtonIsDown)
                axesAfterMouse.PanPixels(mouse.leftDelta);
            if (mouse.rightButtonIsDown)
                axesAfterMouse.ZoomPixels(mouse.rightDelta);
            axesAfterMouse.SetDataRect((RectangleF)renderArea);
            return axesAfterMouse;
        }

        /// <summary>
        /// Render the plot inside a rectangle on an existing Bitmap
        /// </summary>
        public void Render(Bitmap bmp, RectangleF plotRect)
        {
            layout.Update(plotRect);

            // update class-level with the latest plot dimensions
            if (this.axes == null)
                AutoAxis();
            this.axes.SetDataRect(layout.dataRect);

            // update axes locally to reflect mouse manipulations
            var axes = AxesAfterMouse(layout.dataRect);

            // draw the graphics
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.FillRegion(new SolidBrush(colors.dataBackground), new Region(layout.dataRect));

                yTicks.FindBestTickDensity(axes.y.low, axes.y.high, layout.dataRect, gfx);
                xTicks.FindBestTickDensity(axes.x.low, axes.x.high, layout.dataRect, gfx);

                // increase the size of the vertical scale if it contains large labels
                if (yTicks.biggestTickLabelSize.Width > layout.yScaleWidth)
                {
                    Console.WriteLine("increasing width");
                    layout.yScaleWidth = yTicks.biggestTickLabelSize.Width;
                    layout.Update(plotRect);
                }

                yTicks.Render(gfx, axes);
                xTicks.Render(gfx, axes);

                for (int i = 0; i < plottables.Count; i++)
                    plottables[i].Render(gfx, axes);

                RenderLabels(gfx, debugColors: false);
            }
        }

        private void RenderLabels(Graphics gfx, bool debugColors = false)
        {
            if (debugColors)
            {
                Color titleColor = ColorTranslator.FromHtml("#55000000");
                Color labelColor = ColorTranslator.FromHtml("#550000FF");
                Color scaleColor = ColorTranslator.FromHtml("#5500FF00");
                Color dataColor = ColorTranslator.FromHtml("#55FF0000");
                gfx.DrawRectangle(new Pen(dataColor), Rectangle.Round(layout.plotRect));
                gfx.FillRectangle(new SolidBrush(titleColor), Rectangle.Round(layout.titleRect));
                gfx.FillRectangle(new SolidBrush(labelColor), Rectangle.Round(layout.yLabelRect));
                gfx.FillRectangle(new SolidBrush(scaleColor), Rectangle.Round(layout.yScaleRect));
                gfx.FillRectangle(new SolidBrush(labelColor), Rectangle.Round(layout.y2LabelRect));
                gfx.FillRectangle(new SolidBrush(scaleColor), Rectangle.Round(layout.y2ScaleRect));
                gfx.FillRectangle(new SolidBrush(labelColor), Rectangle.Round(layout.xLabelRect));
                gfx.FillRectangle(new SolidBrush(scaleColor), Rectangle.Round(layout.xScaleRect));
                gfx.DrawRectangle(new Pen(dataColor), Rectangle.Round(layout.dataRect));
            }

            // Title
            gfx.DrawString(
                    title.text, title.font, title.brush,
                    Tools.RectangleCenter(layout.titleRect),
                    Tools.StringFormat(Tools.AlignHoriz.center, Tools.AlignVert.center)
                );

            // Y1 Label
            gfx.RotateTransform(-90);
            gfx.DrawString(
                    yLabel.text, yLabel.font, yLabel.brush,
                    Tools.PointRotatedLeft(Tools.RectangleCenter(layout.yLabelRect)),
                    Tools.StringFormat(Tools.AlignHoriz.center, Tools.AlignVert.center)
                );
            gfx.ResetTransform();

            // Y2 Label
            gfx.RotateTransform(90);
            gfx.DrawString(
                    y2Label.text, y2Label.font, y2Label.brush,
                    Tools.PointRotatedRight(Tools.RectangleCenter(layout.y2LabelRect)),
                    Tools.StringFormat(Tools.AlignHoriz.center, Tools.AlignVert.center)
                );
            gfx.ResetTransform();

            // X Label
            gfx.DrawString(
                    xLabel.text, xLabel.font, xLabel.brush,
                    Tools.RectangleCenter(layout.xLabelRect),
                    Tools.StringFormat(Tools.AlignHoriz.center, Tools.AlignVert.center)
                );

            // Outline the data area
            gfx.DrawRectangle(Pens.Black, Rectangle.Round(layout.dataRect));
        }

        /// <summary>
        /// Indicate that a user control has started a click-drag
        /// </summary>
        public void MouseDown(Point downLocation, bool left = false, bool right = false, bool middle = false)
        {
            mouse.leftDown = (left) ? downLocation : new Point(0, 0);
            mouse.rightDown = (right) ? downLocation : new Point(0, 0);
            mouse.middleDown = (middle) ? downLocation : new Point(0, 0);
        }

        /// <summary>
        /// Indicate that a user control has ended a click-drag
        /// </summary>
        public void MouseUp(Point upLocation)
        {
            axes = AxesAfterMouse();
            mouse.leftDown = new Point(0, 0);
            mouse.rightDown = new Point(0, 0);
            mouse.middleDown = new Point(0, 0);
        }

        /// <summary>
        /// Indicate that a user control is click-dragging
        /// </summary>
        public void MouseMove(Point currentLocation)
        {
            mouse.now = currentLocation;
        }
    }
}
