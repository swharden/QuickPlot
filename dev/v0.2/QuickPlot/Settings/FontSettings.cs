using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{
    public class FontSettings
    {
        public float fontSize;
        public string fontFamily;
        public bool bold;
        public Color color;

        public FontSettings(float fontSize = 12, string fontFamily = "Segoe UI", bool bold = false, Color? color = null)
        {
            this.fontSize = fontSize;
            this.fontFamily = fontFamily;
            this.bold = bold;
            this.color = (color == null) ? Color.Black : (Color)color;
        }

        public Font Font
        {
            get
            {
                FontStyle fs = (bold) ? FontStyle.Bold : FontStyle.Regular;
                return new Font(fontFamily, fontSize, fs);
            }
        }

        public Brush Brush
        {
            get
            {
                return new SolidBrush(Color.Black);
            }
        }

        public Pen Pen
        {
            get
            {
                return new Pen(color);
            }
        }
    }
}
