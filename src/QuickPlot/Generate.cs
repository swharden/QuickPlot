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

        public static double[] RandomWalk(int count, double mult = 1, double offset = 0, int? seed = null)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException("count must be >1");
            double[] values = Random(count, mult, offset, seed);
            for (int i = 1; i < values.Length; i++)
                values[i] += values[i - 1] - mult / 2;
            return values;
        }

        public static double[] Consecutative(int count, double mult = 1, double offset = 0)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count can't be negative");
            return Enumerable.Range(0, count).Select(x => (double)x * mult + offset).ToArray();
        }

        public static double[] Sin(int pointCount, double oscillations = 1, double offset = 0, double mult = 1, double shiftOscillations = 0)
        {
            if (pointCount < 0)
                throw new ArgumentOutOfRangeException("pointCount can't be negative");
            double sinScale = 1;
            if (pointCount > 1)
                sinScale = 2 * Math.PI * oscillations / (pointCount - 1);
            else
                sinScale = 1;
            return Enumerable.Range(0, pointCount)
                .Select(x => mult * Math.Sin(sinScale * x + shiftOscillations * Math.PI * 2) + offset)
                .ToArray();
        }

        public static double[] Cos(int pointCount, double oscillations = 1, double offset = 0, double mult = 1, double shiftOscillations = 0)
        {
            return Sin(pointCount, oscillations, offset, mult, shiftOscillations + 0.25);
        }
    }
}
