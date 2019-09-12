using System;

namespace QuickPlot.Cookbook
{
    class Program
    {
        static void Main(string[] args)
        {
            OneThousandLines();
        }

        static void OneThousandLines()
        {
            // generate some data
            double[] xs = Generate.Random(1000);
            double[] ys = Generate.Random(1000);

            var fig = new QuickPlot.Figure();

            fig.Subplot(2, 3, 1);
            fig.Subplot(2, 3, 2);
            fig.Subplot(2, 3, 3);
            fig.Subplot(2, 3, 4, 1, 3);

            fig.plot.Scatter(xs, ys);
            fig.Save("test.jpg");
        }
    }
}
