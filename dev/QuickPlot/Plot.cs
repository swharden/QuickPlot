using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{

    public class Plot
    {
        public Settings.Axes axes;

        /// <summary>
        /// A Plot contains a data area, scales, and labels.
        /// A plot does not render itself (it is GDI-free)
        /// </summary>
        public Plot()
        {
            axes = new Settings.Axes();
        }
    }
}
