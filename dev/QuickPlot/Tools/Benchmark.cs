using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Tools
{
    public class Benchmark : IDisposable
    {
        System.Diagnostics.Stopwatch stopwatch;
        bool silent;

        public Benchmark(bool silent = false)
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            this.silent = silent;
        }

        public double elapsedMilliseconds
        {
            get
            {
                return stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
            }
        }

        public string GetMessage()
        {
            stopwatch.Stop();
            return string.Format("completed in {0:0.00} ms {1:0.00} Hz", elapsedMilliseconds, 1000.0 / elapsedMilliseconds);
        }

        public void Dispose()
        {
            stopwatch.Stop();
            if (!silent)
                Console.WriteLine(GetMessage());
        }
    }
}
