using System;

namespace ConsoleDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            QuickPlot.Figure figure = new QuickPlot.Figure();
            figure.Save(640, 480, "console.png");
        }
    }
}
