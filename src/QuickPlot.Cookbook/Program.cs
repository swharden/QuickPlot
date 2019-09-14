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
            var fig = new QuickPlot.Figure();

            fig.Subplot(2, 3, 1);
            double[] xs = Generate.Consecutative(20, 1.0 / 20);
            fig.plot.Scatter(xs, Generate.Sin(xs.Length));

            fig.Subplot(2, 3, 2);
            fig.plot.Scatter(xs, Generate.Cos(xs.Length));

            fig.Subplot(2, 3, 3);
            fig.plot.Scatter(xs, Generate.Random(xs.Length));

            fig.Subplot(2, 3, 4, 1, 3);
            fig.plot.Scatter(Generate.Random(xs.Length), Generate.Random(xs.Length));

            fig.Save("test.png");
        }
    }
}
