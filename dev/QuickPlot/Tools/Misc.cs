using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Tools
{
    public static class Misc
    {
        public static Color RandomColor
        {
            get
            {
                Random rand = new Random();
                Color color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                return color;
            }
        }
    }
}
