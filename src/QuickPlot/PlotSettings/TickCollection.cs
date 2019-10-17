using SkiaSharp;
using System;
using System.Collections.Generic;
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

    class TickSpacing
    {
        private int divisions;
        private readonly double[] divBy = new double[] { 2, 2, 2.5 }; // dividing from 10 yields 5, 2.5, and 1.
        private double range;

        public double low { get; private set; }
        public double high { get; private set; }
        public double spacing { get; private set; }
        public double firstTick => low - (low % spacing);
        public int tickCount => (int)(range / spacing);

        private TickSpacing() // closed default constructor
        {
        }

        public TickSpacing(double low, double high, int maxTickCount)
        {
            this.low = low;
            this.high = high;

            range = high - low;
            spacing = Math.Pow(10, (int)Math.Log10(range)); // Start from Power of 10 close to range
            divisions = 0;

            while (tickCount <= maxTickCount)
            {
                IncreaseDensity();

                if (divisions > 1000)
                    throw new ArgumentException("Divisions count reach 1000");
            }
        }

        public void IncreaseDensity()
        {
            spacing /= divBy[divisions % divBy.Length];
            divisions++;
        }

        public void DecreaseDensity()
        {
            divisions--;

            // This check can be removed if initiate division with big number like (1_000 * divBy.Length + 1) 
            // that guaranty what it never be negative
            if (divisions < 0)
                divisions += divBy.Length;

            spacing *= divBy[divisions % divBy.Length];
        }
    }

    /* The TickCollection class stores tick styling settings (font, size, etc) and calculates ideal tick positions.
     * It is useful to two both in the same class because ideal tick density depends on things like font and font size.
     * 
     * When the time comes, consider splitting into NumericTickCollection and DatetimeTickCollection.
     */
    public class TickCollection
    {
        public int length = 3;
        public SKPaint paint = new SKPaint();
        //public Font font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular);
        //public Brush brush = new SolidBrush(Color.Black);

        List<Tick> ticks;
        public SKSize biggestTickLabelSize;

        public readonly Side side;

        public TickCollection(Side side)
        {
            this.side = side;
            ticks = new List<Tick>();
        }

        public void FindBestTickDensity(double low, double high, SKRect dataRect)
        {
            // Start by using a too-high tick density (tick labels will overlap)
            // then decrease density until tick labels no longer overlap
            int verticalTickCount = (int)(dataRect.Height / 8);
            int horizontalTickCount = (int)(dataRect.Width / 8 * 3);
            int startingTickCount = (side == Side.left || side == Side.right) ? verticalTickCount : horizontalTickCount;
            TickSpacing ts = new TickSpacing(low, high, startingTickCount);

            for (int i = 0; i < 10; i++)
            {
                Recalculate(ts);
                if (!TicksOverlap(dataRect))
                    break;
                else
                    ts.DecreaseDensity();
            }
        }

        private void Recalculate(TickSpacing ts)
        {
            ticks.Clear();

            float maxTickWidth = 0;
            for (double value = ts.firstTick; value < ts.high; value += ts.spacing)
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
                double totalTickHeight = biggestTickLabelSize.Height * ticks.Count;
                return (totalTickHeight > dataRect.Height);
            }
            else if ((side == Side.bottom) || (side == Side.top))
            {
                double totalTickWidth = biggestTickLabelSize.Width * ticks.Count;
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
    }
}
