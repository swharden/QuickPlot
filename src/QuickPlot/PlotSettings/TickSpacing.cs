using System;

namespace QuickPlot.PlotSettings
{
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
}
