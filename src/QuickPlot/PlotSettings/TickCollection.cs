using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QuickPlot.PlotSettings
{
    /* The TickCollection class stores tick styling settings (font, size, etc) and calculates ideal tick positions.
     * It is useful to do both in the same class because ideal tick density depends on things like font and font size.
     */
    public class TickCollection
    {
        public int length = 3;
        public SKPaint paint = new SKPaint();
        readonly List<Tick> ticks;
        public SKSize biggestTickLabelSize;
        public readonly Side side;
        TickSpacing ts;

        // set these to adjust the density of auto-generated ticks
        public float tickLabelPaddingX = (float)1.3;
        public float tickLabelPaddingY = (float)2.5;

        // set this to manually define tick spacing
        public double? fixedSpacing = null; 

        // set this to disable adjustments of tick density (useful when mouse panning)
        public bool lockTickDensity = false;

        public SKColor yTickColor, xTickColor, gridColor;
        public TickCollection(Side side)
        {
            this.side = side;
            ticks = new List<Tick>();

            yTickColor = SKColors.Black;
            xTickColor = SKColors.Black;
            gridColor = SKColors.LightGray;
        }

        public void Generate(double low, double high, SKRect dataRect)
        {
            if (low == high)
            {
                ticks.Clear();
                return;
            }

            if (lockTickDensity && ts != null)
            {
                GenerateTickList(low, high, ts.spacing);
                return;
            }
            else if (fixedSpacing != null)
            {
                if (fixedSpacing <= 0)
                    throw new ArgumentException("fixedSpacing must be >0");
                GenerateTickList(low, high, (double)fixedSpacing);
                return;
            }
            else
            {
                // Start by using a too-high tick density (tick labels will overlap)
                if (side == Side.left || side == Side.right)
                    ts = new TickSpacing(low, high, (int)(dataRect.Height / 8));
                else
                    ts = new TickSpacing(low, high, (int)(dataRect.Width / 24));

                // then decrease density until tick labels no longer overlap
                for (int i = 0; i < 10; i++)
                {
                    GenerateTickList(ts.low, ts.high, ts.spacing);
                    if (!TicksOverlap(dataRect))
                        break;
                    else
                        ts.DecreaseDensity();
                }
            }
        }

        private void GenerateTickList(double low, double high, double spacing)
        {
            double firstTick = low - (low % spacing);

            ticks.Clear();
            float maxTickWidth = 0;
            for (double value = firstTick; value < high; value += spacing)
            {
                string label = Math.Round(value, 10).ToString();
                ticks.Add(new Tick(value, label));
                maxTickWidth = Math.Max(maxTickWidth, paint.MeasureText(label + " "));
            }
            biggestTickLabelSize = new SKSize(maxTickWidth, paint.FontMetrics.CapHeight);

            // add extra padding to the label to make spacing more comfortable
            biggestTickLabelSize.Width *= tickLabelPaddingX;
            biggestTickLabelSize.Height *= tickLabelPaddingY;
        }

        private bool TicksOverlap(SKRect dataRect)
        {
            if ((side == Side.left) || (side == Side.right))
            {
                int totalTickHeight = (int)(biggestTickLabelSize.Height * ticks.Count);
                return (totalTickHeight > dataRect.Height);
            }
            else if ((side == Side.bottom) || (side == Side.top))
            {
                int totalTickWidth = (int)(biggestTickLabelSize.Width * ticks.Count);
                return (totalTickWidth > dataRect.Width);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void Render(SKCanvas canvas, Axes axes)
        {
            if (side == Side.left)
                RenderLeft(canvas, axes);
            else if (side == Side.bottom)
                RenderBottom(canvas, axes);
            else if (side == Side.right)
                RenderRight(canvas, axes);
            else
                throw new NotImplementedException();
        }

        private void RenderBottom(SKCanvas canvas, Axes axes)
        {
            var xTickPaint = new SKPaint()
            {
                IsAntialias = true,
                TextSize = 12,
                TextAlign = SKTextAlign.Center,
                Color = xTickColor
            };

            var gridPaint = new SKPaint()
            {
                IsAntialias = true,
                Color = gridColor
            };

            SKRect dataRect = axes.GetDataRect();
            foreach (Tick tick in ticks)
            {
                if ((tick.value >= axes.x.low) && (tick.value <= axes.x.high))
                {
                    float xPixel = axes.GetPixel(tick.value, 0).X;
                    SKPoint dataTop = new SKPoint(xPixel, dataRect.Top);
                    SKPoint dataBot = new SKPoint(xPixel, dataRect.Bottom);
                    SKPoint tickBot = new SKPoint(xPixel, dataRect.Bottom + length);
                    canvas.DrawLine(dataBot, tickBot, xTickPaint);
                    canvas.DrawText(tick.label, xPixel, tickBot.Y + xTickPaint.TextSize, xTickPaint);
                    canvas.DrawLine(dataBot, dataTop, gridPaint);
                }
            }
        }

        private void RenderLeft(SKCanvas canvas, Axes axes)
        {
            var yTickPaint = new SKPaint()
            {
                IsAntialias = true,
                TextSize = 12,
                TextAlign = SKTextAlign.Right,
                Color = yTickColor
            };

            var gridPaint = new SKPaint()
            {
                IsAntialias = true,
                Color = gridColor
            };

            SKRect dataRect = axes.GetDataRect();
            foreach (Tick tick in ticks)
            {
                if ((tick.value >= axes.y.low) && (tick.value <= axes.y.high))
                {
                    float yPixel = axes.GetPixel(0, tick.value).Y;
                    SKPoint dataLeft = new SKPoint(dataRect.Left, yPixel);
                    SKPoint dataRight = new SKPoint(dataRect.Right, yPixel);
                    SKPoint tickLeft = new SKPoint(dataRect.Left - length, yPixel);
                    canvas.DrawLine(tickLeft, dataLeft, yTickPaint);
                    canvas.DrawText(tick.label, tickLeft.X - 3, yPixel + yTickPaint.TextSize * 0.35f, yTickPaint);
                    canvas.DrawLine(dataLeft, dataRight, gridPaint);
                }
            }
        }

        private void RenderRight(SKCanvas canvas, Axes axes)
        {
            var yTickPaint = new SKPaint()
            {
                IsAntialias = true,
                TextSize = 12,
                TextAlign = SKTextAlign.Left,
                Color = yTickColor
            };

            SKRect dataRect = axes.GetDataRect();
            foreach (Tick tick in ticks)
            {
                if ((tick.value >= axes.y.low) && (tick.value <= axes.y.high))
                {
                    float yPixel = axes.GetPixel(0, tick.value).Y;
                    SKPoint tickLeft = new SKPoint(dataRect.Right, yPixel);
                    SKPoint tickRight = new SKPoint(dataRect.Right + length, yPixel);
                    canvas.DrawLine(tickLeft, tickRight, yTickPaint);
                    canvas.DrawText(tick.label, tickRight.X + 3, yPixel + yTickPaint.TextSize * 0.35f, yTickPaint);
                }
            }
        }
    }
}
