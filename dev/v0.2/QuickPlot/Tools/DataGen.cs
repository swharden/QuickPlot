using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Tools
{
    public static class DataGen
    {
        public static double[] RandomDoubles(int pointCount, int? randomSeed = null, double multiplier = 1, double offset = 0)
        {
            Random rand = (randomSeed == null) ? new Random() : new Random((int)randomSeed);
            double[] data = new double[pointCount];
            for (int i = 0; i < data.Length; i++)
                data[i] = rand.NextDouble() * multiplier + offset;
            return data;
        }
    }
}
