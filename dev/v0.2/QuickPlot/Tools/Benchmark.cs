using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Tools
{
    public class Benchmark
    {
        System.Diagnostics.Stopwatch stopwatch;

        public Benchmark()
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public double milliseconds { get { return stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency; } }

        public double hz { get { return 1000.0 / milliseconds; } }

        public override string ToString()
        {
            return string.Format("{0:0.00} ms ({1:0.00} Hz)", hz, 1000.0 / hz);
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

        public void Restart()
        {
            stopwatch.Restart();
        }
    }
}
