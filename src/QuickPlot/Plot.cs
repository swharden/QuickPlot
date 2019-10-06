using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace QuickPlot
{
    /* The Plot object holds details about a plot (data, axes information, etc.) but never stores plot size.
     * Plot size is only passed-in when Render() is called.
     */

    public class Plot
    {
        // keep the plottable list private.
        private List<Plottables.Plottable> plottables = new List<Plottables.Plottable>();

        // configuration objects are okay to be public
        public PlotSettings.SubplotPosition subplotPosition = new PlotSettings.SubplotPosition(1, 1, 1);
        public PlotSettings.Colors colors = new PlotSettings.Colors();
        public readonly PlotSettings.Layout layout = new PlotSettings.Layout();

        public PlotSettings.Label title, yLabel, xLabel, y2Label;

        // axes can be public, and null means it hasnt been set up
        public PlotSettings.Axes axes;

        // keep private?
        private PlotSettings.MouseTracker mouse = new PlotSettings.MouseTracker();

        public Plot()
        {
            title = new PlotSettings.Label { text = "Title", fontSize = 16, bold = true };
            yLabel = new PlotSettings.Label { text = "Vertical Label" };
            xLabel = new PlotSettings.Label { text = "Horzontal Label" };
            y2Label = new PlotSettings.Label { text = "Vertical Too" };
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
            axesAfterMouse.SetRect((RectangleF)renderArea);
            return axesAfterMouse;
        }

        public void Render(Bitmap bmp, RectangleF renderArea)
        {

            // updating the layout will calculate sizes for all the render components
            layout.Update(renderArea);

            // make axes aware of new data area dimensions
            if (axes == null)
                AutoAxis();
            axes.SetRect(layout.dataRect);
            var axesAfterMouse = AxesAfterMouse(layout.dataRect);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.FillRectangle(new SolidBrush(colors.background), Rectangle.Round(layout.dataRect));
                RenderLabels(gfx, debugColors: false);
                RenderTicksAndGrid(gfx, axesAfterMouse);
                for (int i = 0; i < plottables.Count; i++)
                    plottables[i].Render(gfx, axesAfterMouse);
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

        private void RenderTicksAndGrid(Graphics gfx, PlotSettings.Axes axes)
        {
            // determine the ticks
            //TODO: calculate these better
            double[] xTickPositions = { 0, 10, 20, 30, 40, 50 };
            double[] yTickPositions = { -1, -.5, 0, .5, 1 };

            int tickLength = 3;
            Font tickFont = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular);
            Brush tickBrush = new SolidBrush(Color.Black);
            StringFormat xTickSf = Tools.StringFormat(Tools.AlignHoriz.center, Tools.AlignVert.top);
            StringFormat yTickSf = Tools.StringFormat(Tools.AlignHoriz.right, Tools.AlignVert.center);

            foreach (double xTickPosition in xTickPositions)
            {
                if ((xTickPosition >= axes.x.low) && (xTickPosition <= axes.x.high))
                {
                    float xPixel = axes.GetPixel(xTickPosition, 0).X;
                    gfx.DrawLine(Pens.LightGray, xPixel, layout.dataRect.Bottom, xPixel, layout.dataRect.Top);
                    gfx.DrawLine(Pens.Black, xPixel, layout.dataRect.Bottom, xPixel, layout.dataRect.Bottom + tickLength);
                    gfx.DrawString(xTickPosition.ToString(), tickFont, tickBrush, new PointF(xPixel, layout.dataRect.Bottom + tickLength), xTickSf);
                }
            }

            foreach (double yTickPosition in yTickPositions)
            {
                if ((yTickPosition >= axes.y.low) && (yTickPosition <= axes.y.high))
                {
                    float yPixel = axes.GetPixel(0, yTickPosition).Y;
                    gfx.DrawLine(Pens.LightGray, layout.dataRect.Left, yPixel, layout.dataRect.Right, yPixel);
                    gfx.DrawLine(Pens.Black, layout.dataRect.Left, yPixel, layout.dataRect.Left - tickLength, yPixel);
                    gfx.DrawString(yTickPosition.ToString(), tickFont, tickBrush, new PointF(layout.dataRect.Left - tickLength, yPixel), yTickSf);
                }
            }

        }

        public void MouseDown(Point downLocation, bool left = false, bool right = false, bool middle = false)
        {
            mouse.leftDown = (left) ? downLocation : new Point(0, 0);
            mouse.rightDown = (right) ? downLocation : new Point(0, 0);
            mouse.middleDown = (middle) ? downLocation : new Point(0, 0);
        }

        public void MouseUp(Point upLocation)
        {
            axes = AxesAfterMouse();
            mouse.leftDown = new Point(0, 0);
            mouse.rightDown = new Point(0, 0);
            mouse.middleDown = new Point(0, 0);
        }

        public void MouseMove(Point currentLocation)
        {
            mouse.now = currentLocation;
        }
    }
}
