using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public enum Side { left, right, bottom, top };

    struct Tick
    {
        public double value;
        public string label;

        public Tick(double value, string label)
        {
            this.value = value;
            this.label = label;
        }
    }

    /* The TickSpacing class is used to determine ideal tick spacing for a given range and number of ticks.
     * Currently only numeric spacings are supported (10, 5, 2.5, 1)
     * Future efforts may create similar classes for ideal ticks for dates and times (years, months, days, hours, minutes) 
     */
    class TickSpacing
    {
        private int divisions;
        private readonly double[] divBy = new double[] { 2, 2, 2.5 }; // dividing from 10 yields 5, 2.5, and 1.

        public double low { get; private set; }
        public double high { get; private set; }
        public double spacing { get; private set; }
        public double firstTick => low - (low % spacing);
        public int tickCount => (int)(span / spacing);
        public double span => high - low;

        public TickSpacing(double low, double high, int maxTickCount)
        {
            this.low = low;
            this.high = high;

            spacing = Math.Pow(10, (int)Math.Log10(span)); // Start from Power of 10 close to range
            divisions = 0;

            while (tickCount <= maxTickCount)
                IncreaseDensity();
        }

        public void SetRange(double low, double high)
        {
            this.low = low;
            this.high = high;
        }

        public void IncreaseDensity()
        {
            spacing /= divBy[divisions % divBy.Length];
            divisions++;
            if (divisions > 1000)
                throw new OverflowException("Tick density is too high.");
        }

        public void DecreaseDensity()
        {
            divisions--;

            if (divisions < 0)
                divisions += divBy.Length;

            spacing *= divBy[divisions % divBy.Length];
        }

        public void SetDensity(double density)
        {
            spacing = density;
        }
    }

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

        public double? fixedSpacing = null; // set this to manually define tick spacing
        public bool lockTickDensity = false;

        public TickCollection(Side side)
        {
            this.side = side;
            ticks = new List<Tick>();
        }

        public void Generate(double low, double high, SKRect dataRect)
        {
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
                maxTickWidth = Math.Max(maxTickWidth, paint.MeasureText(label));
            }
            biggestTickLabelSize = new SKSize(maxTickWidth, paint.FontMetrics.CapHeight);

            // add extra padding to the label to make spacing more comfortable
            biggestTickLabelSize.Width += 5;
            biggestTickLabelSize.Height += 5;
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
            SKPaint paintTick = new SKPaint()
            {
                IsAntialias = true,
                TextSize = 12,
                TextAlign = SKTextAlign.Center,
                Color = SKColor.Parse("#FF000000")
            };

            SKPaint paintGrid = new SKPaint()
            {
                IsAntialias = true,
                Color = SKColor.Parse("#33000000")
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
                    canvas.DrawLine(dataBot, tickBot, paintTick);
                    canvas.DrawText(tick.label, xPixel, tickBot.Y + paintTick.TextSize, paintTick);
                    canvas.DrawLine(dataBot, dataTop, paintGrid);
                }
            }
        }

        private void RenderLeft(SKCanvas canvas, Axes axes)
        {
            SKPaint paintTick = new SKPaint()
            {
                IsAntialias = true,
                TextSize = 12,
                TextAlign = SKTextAlign.Right,
                Color = SKColor.Parse("#FF000000")
            };

            SKPaint paintGrid = new SKPaint()
            {
                IsAntialias = true,
                Color = SKColor.Parse("#33000000")
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
                    canvas.DrawLine(tickLeft, dataLeft, paintTick);
                    canvas.DrawText(tick.label, tickLeft.X - 3, yPixel + paintTick.TextSize * 0.35f, paintTick);
                    canvas.DrawLine(dataLeft, dataRight, paintGrid);
                }
            }
        }

        private void RenderRight(SKCanvas canvas, Axes axes)
        {
            SKPaint paintTick = new SKPaint()
            {
                IsAntialias = true,
                TextSize = 12,
                TextAlign = SKTextAlign.Left,
                Color = SKColor.Parse("#FF000000")
            };

            Debug.WriteLine($"ticksRight {ticks.Count}");

            SKRect dataRect = axes.GetDataRect();
            foreach (Tick tick in ticks)
            {
                if ((tick.value >= axes.y.low) && (tick.value <= axes.y.high))
                {
                    float yPixel = axes.GetPixel(0, tick.value).Y;
                    SKPoint tickLeft = new SKPoint(dataRect.Right, yPixel);
                    SKPoint tickRight = new SKPoint(dataRect.Right + length, yPixel);
                    canvas.DrawLine(tickLeft, tickRight, paintTick);
                    canvas.DrawText(tick.label, tickRight.X + 3, yPixel + paintTick.TextSize * 0.35f, paintTick);
                }
            }
        }
    }
}
