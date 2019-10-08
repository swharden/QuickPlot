using System;
using System.Collections.Generic;
using System.Drawing;
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

        public double spacing { get; private set; }
        public double firstTick { get; private set; }

        public TickSpacing(double low, double high, int maxTickCount)
        {
            Initiailize(low, high, maxTickCount);
        }

        public void Initiailize(double low, double high, int maxTickCount)
        {
            double range = high - low;
            int exponent = (int)Math.Log10(range);
            spacing = Math.Pow(10, exponent); // start at a multiple of 10
            divisions = 0;

            while (true)
            {
                IncreaseDensity(low, high);

                double tickCount = (int)(range / spacing);
                if (tickCount > maxTickCount)
                    break;

                if (divisions > 1000)
                    throw new ArgumentException();
            }
        }

        public void IncreaseDensity(double low, double high)
        {
            spacing /= divBy[divisions % divBy.Length];
            divisions += 1;
            double offset = Math.Abs(low % spacing);
            firstTick = (low < 0) ? low + offset : low - offset;
        }

        public void DecreaseDensity(double low, double high)
        {
            spacing *= divBy[divisions % divBy.Length];
            divisions = (divisions == 0) ? divBy.Length - 1 : divisions - 1;
            double offset = Math.Abs(low % spacing);
            firstTick = (low < 0) ? low + offset : low - offset;
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
        public Font font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular);
        public Brush brush = new SolidBrush(Color.Black);

        List<Tick> ticks;
        public SizeF biggestTickLabelSize;

        public readonly Side side;

        public TickCollection(Side side)
        {
            this.side = side;
            ticks = new List<Tick>();
        }

        public void FindBestTickDensity(double low, double high, RectangleF dataRect, Graphics gfx)
        {
            // Start by using a too-high tick density (tick labels will overlap)
            // then decrease density until tick labels no longer overlap
            int verticalTickCount = (int)(dataRect.Height / 8);
            int horizontalTickCount = (int)(dataRect.Width / 8*3);
            int startingTickCount = (side == Side.left || side == Side.right) ? verticalTickCount : horizontalTickCount;
            TickSpacing ts = new TickSpacing(low, high, startingTickCount);

            for (int i = 0; i < 10; i++)
            {
                Recalculate(ts, low, high, gfx);
                if (!TicksOverlap(dataRect))
                    break;
                else
                    ts.DecreaseDensity(low, high);
            }
        }

        private void Recalculate(TickSpacing ts, double low, double high, Graphics gfx)
        {
            ticks.Clear();
            biggestTickLabelSize = new SizeF(0, 0);
            for (double value = ts.firstTick; value < high; value += ts.spacing)
            {
                string label = Math.Round(value, 10).ToString();
                ticks.Add(new Tick(value, label));
                SizeF labelSize = gfx.MeasureString(label, font);
                biggestTickLabelSize.Width = Math.Max(biggestTickLabelSize.Width, labelSize.Width);
                biggestTickLabelSize.Height = Math.Max(biggestTickLabelSize.Height, labelSize.Height);
            }

            // add extra padding to the label to make spacing more comfortable
            biggestTickLabelSize.Width += 5;
            biggestTickLabelSize.Height += 5;
        }

        private bool TicksOverlap(RectangleF dataRect)
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

        public void Render(Graphics gfx, Axes axes)
        {
            if (side == Side.left)
                RenderLeft(gfx, axes);
            else if (side == Side.bottom)
                RenderBottom(gfx, axes);
            else
                throw new NotImplementedException();
        }

        private void RenderBottom(Graphics gfx, Axes axes)
        {
            RectangleF dataRect = axes.GetDataRect();
            StringFormat sf = Tools.StringFormat(Tools.AlignHoriz.center, Tools.AlignVert.top);

            foreach (Tick tick in ticks)
            {
                if ((tick.value >= axes.x.low) && (tick.value <= axes.x.high))
                {
                    float xPixel = axes.GetPixel(tick.value, 0).X;
                    gfx.DrawLine(Pens.LightGray, xPixel, dataRect.Bottom, xPixel, dataRect.Top);
                    gfx.DrawLine(Pens.Black, xPixel, dataRect.Bottom, xPixel, dataRect.Bottom + length);
                    gfx.DrawString(tick.label, font, brush, new PointF(xPixel, dataRect.Bottom + length), sf);
                }
            }
        }

        private void RenderLeft(Graphics gfx, Axes axes)
        {
            RectangleF dataRect = axes.GetDataRect();
            StringFormat sf = Tools.StringFormat(Tools.AlignHoriz.right, Tools.AlignVert.center);

            foreach (Tick tick in ticks)
            {
                if ((tick.value >= axes.y.low) && (tick.value <= axes.y.high))
                {
                    float yPixel = axes.GetPixel(0, tick.value).Y;
                    gfx.DrawLine(Pens.LightGray, dataRect.Left, yPixel, dataRect.Right, yPixel);
                    gfx.DrawLine(Pens.Black, dataRect.Left, yPixel, dataRect.Left - length, yPixel);
                    gfx.DrawString(tick.label, font, brush, new PointF(dataRect.Left - length, yPixel), sf);
                }
            }
        }
    }
}
