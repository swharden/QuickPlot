using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{
    public class AxisLabel
    {
        public string text;
        public float fontSize;
        public string fontFamily;
        public bool bold;
        public bool IsValid { get { return (text != null); } }

        public AxisLabel(string text = "label text", float fontSize = 12, string fontFamily = "Segoe UI", bool bold = false)
        {
            this.text = text;
            this.fontSize = fontSize;
            this.fontFamily = fontFamily;
            this.bold = bold;
        }
    }
}
