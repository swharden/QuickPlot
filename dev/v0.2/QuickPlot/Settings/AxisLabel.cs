using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{
    public class AxisLabel
    {
        public FontSettings fs;
        public string text;
        public bool IsValid { get { return (text != null); } }

        public AxisLabel(string text = "label text", float fontSize = 12, string fontFamily = "Segoe UI", bool bold = false, Color? color = null)
        {
            this.text = text;
            fs = new FontSettings(fontSize, fontFamily, bold, color);
        }
    }
}
