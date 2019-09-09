using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickstartConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            QuickPlot.Figure fig = new QuickPlot.Figure();
            fig.Subplot(2, 3, 1);
            fig.Subplot(2, 3, 2);
            fig.Subplot(2, 3, 3);
            fig.Subplot(2, 3, 4, 1, 3);
            fig.Save();
        }
    }
}
