using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickPlot
{
    public class Generate
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
            if (count < 0)
                throw new ArgumentOutOfRangeException("count can't be negative");
            Random rand = SeededRandom(seed);
            return Enumerable.Range(0, count).Select(x => rand.NextDouble() * mult + offset).ToArray();
        }

        public static double[] Consecutative(int count, double mult = 1, double offset = 0)
        {
            double[] values = new double[count];
            for (int i = 0; i < values.Length; i++)
                values[i] = i * mult + offset;
            return values;
        }

        public static double[] Sin(int pointCount, double oscillations = 1, double offset = 0, double mult = 1, double phase = 0)
        {
            double sinScale = 2 * Math.PI * oscillations / pointCount;
            double[] ys = new double[pointCount];
            for (int i = 0; i < ys.Length; i++)
                ys[i] = Math.Sin(i * sinScale + phase * Math.PI * 2) * mult + offset;
            return ys;
        }

        public static double[] Cos(int pointCount, double oscillations = 1, double offset = 0, double mult = 1, double phase = 0)
        {
            double sinScale = 2 * Math.PI * oscillations / pointCount;
            double[] ys = new double[pointCount];
            for (int i = 0; i < ys.Length; i++)
                ys[i] = Math.Cos(i * sinScale + phase * Math.PI * 2) * mult + offset;
            return ys;
        }
    }
}
