using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{
    public class AxisLabels
    {
        // labels for a single plot
        public AxisLabel title, x, y, y2;

        public AxisLabels()
        {
            title = new AxisLabel();
            x = new AxisLabel();
            y = new AxisLabel();
            y2 = new AxisLabel();
        }
    }

    public class AxisLabel
    {
        public string text;
        public float fontSize;
        public bool IsValid { get { return (text != null); } }

        public AxisLabel(string text = "label text", float fontSize = 12)
        {
            this.text = text;
            this.fontSize = fontSize;
        }
    }
}
