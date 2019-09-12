using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot
{
    public static class Generate
    {
        private static Random SeededRandom(int? seed = null)
        {
            if (seed == null)
                return new Random();
            else
                return new Random((int)seed);
        }

        public static double[] Random(int count, double mult = 1, double offset = 0, int? seed = null)
        {
            Random rand = SeededRandom(seed);
            double[] values = new double[count];
            for (int i = 0; i < values.Length; i++)
                values[i] = rand.NextDouble() * mult + offset;
            return values;
        }

        public static double[] Consecutative(int count, double mult = 1, double offset = 0)
        {
            double[] values = new double[count];
            for (int i = 0; i < values.Length; i++)
                values[i] = i * mult + offset;
            return values;
        }
    }
}
